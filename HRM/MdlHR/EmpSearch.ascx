<%@ Import Namespace ="System.Data.SqlClient" %>
<%@ Import Namespace ="System.Data" %>
<%@ Import Namespace ="iHRPCore.HRComponent"%>
<%@ Import Namespace ="iHRPCore.Com"%>
<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../Include/EmpHeaderSearch.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="EmpSearch.ascx.cs" Inherits="iHRPCore.MdlHR.EmpSearch" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%
	string strLangID = Session["LangID"] == null?"EN":Session["LangID"].ToString().Trim();
	int Total = 0;
	int Total1 = 0;
			
	DataTable rs = new DataTable();		
	rs = clsHREmpList.GetFunctionByMdlID("",strLangID);
	Total = rs.Rows.Count;
	DataTable rs1 = clsHREmpList.GetColumnByFunction("",strLangID);
	Total1 = rs1.Rows.Count;
%>
<script language="javascript">					
		var DS = new Array(3);
		DS[0] = new Array(<%=Total%>);
		DS[1] = new Array(<%=Total%>);
		DS[2] = new Array(<%=Total%>);	
		<% for(int i=0; i<Total; i++) {%>		
			DS[0][<%=i%>]="<%=rs.Rows[i]["TableID"]%>";
			DS[1][<%=i%>]="<%=rs.Rows[i]["TableName"]%>";
			DS[2][<%=i%>]="<%=rs.Rows[i]["ModuleID"]%>";
		<%}%>
		
		var DS1 = new Array(5)
		DS1[0] = new Array(<%=Total1%>);
		DS1[1] = new Array(<%=Total1%>);
		DS1[2] = new Array(<%=Total1%>);	
		DS1[3] = new Array(<%=Total1%>);	
		DS1[4] = new Array(<%=Total1%>);	
		<% for(int i=0; i<Total1; i++) {%>		
			DS1[0][<%=i%>]="<%=rs1.Rows[i]["TableID"]%>";
			DS1[1][<%=i%>]="<%=rs1.Rows[i]["ColumnID"]%>";
			DS1[2][<%=i%>]="<%=rs1.Rows[i]["ColumnName"]%>";
			DS1[3][<%=i%>]="<%=rs1.Rows[i]["Code"]%>";
			DS1[4][<%=i%>]="<%=rs1.Rows[i]["TypeInfo"]%>";
		<%}%>
		
</script>
<script language="javascript">
	function ChangeModule()
	{
		var Value = trim(document.getElementById("_ctl0:cboModule").value);
		var all = document.getElementById("_ctl0_lstFunction").length;
		for(i=0; i<all; i++)
		{	
			document.getElementById("_ctl0_lstFunction").remove(0);			
		};
		var all1 = document.getElementById("_ctl0_lstColumnSelect").length;
		for(i=0; i<all1; i++)
		{	
			document.getElementById("_ctl0_lstColumnSelect").remove(0);			
		};
		for(i=0; i<<%=Total%>;i++)
		{
			if (trim(DS[2][i])==Value)
			{
				document.getElementById("_ctl0_lstFunction").add(new Option(DS[1][i],DS[0][i]));
			};
		};		
	}
	
	function ChangeFunctionList()
	{
		var all = document.getElementById("_ctl0_lstFunction").length;
		var all1 = document.getElementById("_ctl0_lstColumnSelect").length;
		for(i=0; i<all1; i++)
		{	
			document.getElementById("_ctl0_lstColumnSelect").remove(0);			
		};
		for(i=0; i<all; i++)
		{	
			if (document.getElementById("_ctl0_lstFunction").options[i].selected == true)
			{
				for(j=0; j<<%=Total1%>;j++)
				{
					if (DS1[0][j]==document.getElementById("_ctl0_lstFunction").options[i].value)
					{
						document.getElementById("_ctl0_lstColumnSelect").add(new Option(DS1[2][j],DS1[3][j]));
					};
				};		
			};
		};
	}
	
	function AddFunction()
	{
		var all = document.getElementById("_ctl0_lstColumnSelect").length;
		var i = 0;
		for(i=0; i<all; i++)
		{	
			if (document.getElementById("_ctl0_lstColumnSelect").options[i].selected == true)
			{
				var count = document.getElementById("tblColumnSelect").rows.length;
				if (count > 1)
				{
					index = 1;
					for (j=1; j<count; j++)
					{
						if (trim(document.getElementById("txtColumnID" + j).value) == trim(document.getElementById("_ctl0_lstColumnSelect").options[i].value))
						{
							index = 0;
							break;
						}
					}
					if (index == 0)
						continue;
				}
				InsertRow(document.getElementById("_ctl0_lstColumnSelect").options[i].text,document.getElementById("_ctl0_lstColumnSelect").options[i].value)
			};
		};
		return false;
	}
	
	function RemoveFunction()
	{
		return false;
	}
	
	function AddCondition()
	{
		if (trim(document.getElementById("_ctl0_txtInfo").value) == "")
		{
			alert("Please input information of filter condition!");
			document.getElementById("btnAddInfo").focus();
			return false;
		}
		var Result1 = "";//DataTextField
		var Result2 = "";//DataValueField
		//Kiem tra lstCondition: Neu la phan tu dau tien
		var strQuote = "";
		var strLinkCondition = "";
		var strLinkConditionCode = "";
		var strCondition = document.getElementById("_ctl0_cboCondition").value;
		if (document.getElementById("_ctl0_txtMultiSelect").value == "In")
		{
			document.getElementById("_ctl0_cboCondition").value = "In";
			strCondition = "In";
		}
		if (document.getElementById("_ctl0_optAnd").checked == true)
		{
			strLinkCondition = "and"
			strLinkConditionCode = "and";
		}
		else if (document.getElementById("_ctl0_optOr").checked == true)
		{
			strLinkCondition = "or"
			strLinkConditionCode = "or";
		}
		if (document.getElementById("_ctl0_lstCondition").length == 0)
		{
			if (document.getElementById("_ctl0_optBeginQuote").checked == true)
				strQuote = "(";
			else
				strQuote = "";
			Result1 = strLinkCondition + " " + strQuote + document.getElementById("_ctl0_txtCriteria").value + " "
					+ strCondition + " "
					+ "" + document.getElementById("_ctl0_txtInfo").value + "";
			if (document.getElementById("_ctl0_cboCondition").value != "Like")
				Result2 = strLinkConditionCode + " " + strQuote + document.getElementById("_ctl0_txtColumnName").value + " "
						+ strCondition + " "
						+ document.getElementById("_ctl0_txtValue").value;
			else
				Result2 = strLinkConditionCode + " " + strQuote + document.getElementById("_ctl0_txtColumnName").value + " "
					+ strCondition + " "
					+ "''%''" + " + " + document.getElementById("_ctl0_txtValue").value + " + " + "''%''";
		}
		else
		{
			if (document.getElementById("_ctl0_optBeginQuote").checked == true)
				strQuote = "(";
			else if (document.getElementById("_ctl0_optEndQuote").checked == true)
				strQuote = ")";
			else strQuote = "";
			Result1 = document.getElementById("_ctl0_txtCriteria").value + " "
					+ strCondition + " "
					+ " " + document.getElementById("_ctl0_txtInfo").value + " ";
			if (document.getElementById("_ctl0_cboCondition").value != "Like")
				Result2 = document.getElementById("_ctl0_txtColumnName").value + " "
					+ strCondition + " "
					+ document.getElementById("_ctl0_txtValue").value;
			else
			{
				Result2 = document.getElementById("_ctl0_txtColumnName").value + " "
					+ strCondition + " "
					+ "''%''" + " + " + document.getElementById("_ctl0_txtValue").value + " + " + "''%''";
			}
			if (strQuote == "(")
			{
				Result1 = strLinkCondition + " " + strQuote + Result1;
				Result2 = strLinkConditionCode + " " + strQuote + Result2;
			}
			else if(strQuote == ")")
			{
				Result1 = strLinkCondition + " " + Result1 + strQuote;
				Result2 = strLinkConditionCode + " " + Result2 + strQuote;
			}
			else
			{
				Result1 = strLinkCondition + " " + Result1;
				Result2 = strLinkConditionCode + " " + Result2;
			}
		}
		document.getElementById("_ctl0_lstCondition").add(new Option(Result1,Result2));
		return false;	
	}
	
	function RemoveCondition()
	{
		var count = document.getElementById("_ctl0_lstCondition").length - 1;
		do 
		{
			if (document.getElementById("_ctl0_lstCondition").options[count].selected == true)
			{
				document.getElementById("_ctl0_lstCondition").remove(count);
				count --;
			}
			count --;
		} while (count >= 0);
		
		return false;
	}
	
	function GetSelectColumn()
	{
		var count = document.getElementById("tblColumnSelect").rows.length;
		var strResult = "";
		for (i = 1; i< count; i++)
		{
			if (strResult == "")
				strResult = "," + document.getElementById("txtColumnID" + i).value;
			else
				strResult = strResult + "," + document.getElementById("txtColumnID" + i).value;
		}
		return strResult + ",";
	}
	
	function GetDisplayColumn()
	{
		var count = document.getElementById("tblColumnSelect").rows.length;
		var strResult = "";
		for (i = 1; i< count; i++)
		{
			if (document.getElementById("txtSequence" + i).value != "")
			{
				if (strResult == "")
					strResult = document.getElementById("txtColumnID" + i).value + "@@" + document.getElementById("txtSequence" + i).value + "@@" + document.getElementById("txtWidth" + i).value;
				else
					strResult = strResult + "@@" + document.getElementById("txtColumnID" + i).value + "@@" + document.getElementById("txtSequence" + i).value + "@@" + document.getElementById("txtWidth" + i).value;
			}
		}
		return strResult;
	}
	
	function GetAllGridColumn()
	{
		var count = document.getElementById("tblColumnSelect").rows.length;
		var strResult = "";
		for (i = 1; i< count; i++)
		{
			if (strResult == "")
				strResult = document.getElementById("txtColumnID" + i).value + "@@" + document.getElementById("hpColumnName" + i).innerText + "@@" + document.getElementById("txtSequence" + i).value + "@@" + document.getElementById("txtWidth" + i).value;
			else
				strResult = strResult + "@@" + document.getElementById("txtColumnID" + i).value + "@@" + document.getElementById("hpColumnName" + i).innerText + "@@" + document.getElementById("txtSequence" + i).value + "@@" + document.getElementById("txtWidth" + i).value;
		}
		return strResult;
	}
	
	function GetConditionView()
	{
		var count = document.getElementById("_ctl0_lstCondition").length;
		var strResult = "";
		for (i=0; i<count; i++)
		{
			if (strResult == "")
				strResult = document.getElementById("_ctl0_lstCondition").options[i].text;
			else
				strResult = strResult + "@@" + document.getElementById("_ctl0_lstCondition").options[i].text;
		}
		return strResult;
	}
	
	function GetConditionCode()
	{
		var count = document.getElementById("_ctl0_lstCondition").length;
		var strResult = "";
		for (i=0; i<count; i++)
		{
			if (strResult == "")
				strResult = document.getElementById("_ctl0_lstCondition").options[i].value;
			else
				strResult = strResult + "@@" + document.getElementById("_ctl0_lstCondition").options[i].value;
		}
		return strResult;
	}
	
	function GetSortCondition()
	{
		var strResult = "";
		for (i=1; i<=5; i++)
		{
			strResult = strResult + "@@" + document.getElementById("_ctl0_cboSort" + i).value + "@@" 
			+ document.getElementById("_ctl0_cboSort" + i).options[document.getElementById("_ctl0_cboSort" + i).selectedIndex].text
			+ "@@" + document.getElementById("_ctl0_cboSort" + i).title;
		}
		return strResult;
	}
	
	function SaveToReport()
	{
		if (CheckView() == false)
			return false;
		if (trim(document.getElementById("_ctl0_txtReportName").value) == "")
		{
			alert("Input report name to save!")
			document.getElementById("_ctl0_txtReportName").focus();
			return false;
		}
		alert("This tasking is being built. Waiting for next version!");
		return false;
	}
		
	function CheckView()
	{
		var strSelectColumn = GetSelectColumn();
		var strDisplayColumn = GetDisplayColumn();
		var strConditionView = GetConditionView();
		var strConditionCode = GetConditionCode();
		var strSortCondition = GetSortCondition();
		var strAllGridColumn = GetAllGridColumn();
		if (strDisplayColumn == "")
		{
			alert("Choose column to display before save");
			return false;
		}
		document.getElementById("_ctl0_txtSelectColumn").value = strSelectColumn;
		document.getElementById("_ctl0_txtDisplayColumn").value = strDisplayColumn;
		document.getElementById("_ctl0_txtConditionView").value = strConditionView;
		document.getElementById("_ctl0_txtConditionCode").value = strConditionCode;
		document.getElementById("_ctl0_txtSortCondition").value = strSortCondition;
		document.getElementById("_ctl0_txtAllGridColumn").value = strAllGridColumn;
	}
	
</script>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="98%" border="0">
	<TR>
		<TD align="center"><asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></TD>
	</TR>
	<tr>
		<td>
			<table width="100%">
				<tr height="1">
					<td width="11%"></td>
					<td width="40%"></td>
					<td width="4%"></td>
					<td></td>
				</tr>
				<TR>
					<TD><asp:label id="Label1" runat="server" CssClass="label">Module</asp:label></TD>
					<TD><asp:dropdownlist id="cboModule" runat="server" CssClass="combo" onchange="ChangeModule()" Width="100%"></asp:dropdownlist></TD>
					<TD></TD>
					<TD><asp:label id="Label2" runat="server" CssClass="label">Field to select</asp:label></TD>
				</TR>
				<TR>
					<TD><asp:label id="Label5" runat="server" CssClass="label">Function</asp:label></TD>
					<TD><asp:listbox id="lstFunction" runat="server" CssClass="ListBox" onchange="ChangeFunctionList()"
							Width="100%" SelectionMode="Multiple" Height="130px"></asp:listbox></TD>
					<TD></TD>
					<TD><asp:listbox id="lstColumnSelect" runat="server" CssClass="ListBox" Width="100%" SelectionMode="Multiple"
							Height="130px"></asp:listbox></TD>
				</TR>
				<TR>
					<TD colSpan="2">
						<HR width="100%">
					</TD>
					<TD vAlign="middle" align="center"></TD>
					<td align="center"><asp:linkbutton id="btnAddFunction" runat="server" CssClass="btn" Width="30px">V</asp:linkbutton><asp:linkbutton id="btnRemoveFunction" runat="server" CssClass="btn" Width="30px" Visible="False">X</asp:linkbutton></td>
				</TR>
				<TR>
					<TD colSpan="4">
						<table width="100%">
							<tr>
								<td width="51%">
									<TABLE id="Table2" width="100%">
										<TR>
											<TD width="22%"><asp:label id="Label3" runat="server" CssClass="label" Width="100%">Criteria</asp:label></TD>
											<TD><asp:textbox id="txtCriteria" runat="server" CssClass="input" Width="100%" ReadOnly="True"></asp:textbox></TD>
										</TR>
										<TR>
											<TD><asp:label id="Label4" runat="server" CssClass="label">Condition</asp:label></TD>
											<TD><asp:dropdownlist id="cboCondition" runat="server" CssClass="combo" Width="100%"></asp:dropdownlist></TD>
										</TR>
										<TR>
											<TD><asp:label id="Label6" runat="server" CssClass="label">Data</asp:label></TD>
											<TD><asp:textbox id="txtInfo" runat="server" CssClass="input" Width="92%" ReadOnly="True"></asp:textbox><INPUT class="search" id="btnAddInfo" onclick="javascript:OpenInfoFunction()" type="button"
													value="..." name="btnAddInfo"></TD>
										</TR>
										<TR>
											<TD><asp:label id="Label7" runat="server" CssClass="label">Link</asp:label></TD>
											<TD><asp:radiobutton id="optAnd" CssClass="option" Width="20%" Checked="True" Text="And" Runat="server"
													GroupName="Link"></asp:radiobutton><asp:radiobutton id="optOr" CssClass="option" Width="20%" Text="Or" Runat="server" GroupName="Link"></asp:radiobutton><INPUT id="txtCriteriaID" style="WIDTH: 8px; HEIGHT: 22px" type="hidden" size="1" name="txtCriteriaID"
													runat="server"><INPUT id="txtValue" style="WIDTH: 8px; HEIGHT: 22px" type="hidden" size="1" name="Hidden1"
													runat="server"><INPUT id="txtColumnName" style="WIDTH: 8px; HEIGHT: 22px" type="hidden" size="1" name="Hidden1"
													runat="server"><INPUT id="txtMultiSelect" style="WIDTH: 8px; HEIGHT: 22px" type="hidden" size="1" name="Hidden1"
													runat="server"></TD>
										</TR>
										<TR>
											<TD><asp:label id="Label8" runat="server" CssClass="label">Group condition</asp:label></TD>
											<TD><asp:radiobutton id="optBeginQuote" CssClass="option" Width="20%" Text="Begin" Runat="server" GroupName="Condition"></asp:radiobutton><asp:radiobutton id="optEndQuote" CssClass="option" Width="20%" Text="End" Runat="server" GroupName="Condition"></asp:radiobutton><asp:radiobutton id="optNone" CssClass="option" Width="20%" Checked="True" Text="None" Runat="server"
													GroupName="Condition"></asp:radiobutton><asp:linkbutton id="btnAddCondition" runat="server" CssClass="btn" Width="30px">V</asp:linkbutton><asp:linkbutton id="btnRemoveCondition" runat="server" CssClass="btn" Width="30px">X</asp:linkbutton></TD>
										</TR>
										<TR>
											<TD align="center" colSpan="2"></TD>
										</TR>
										<TR>
											<TD align="center" colSpan="2"><asp:listbox id="lstCondition" runat="server" CssClass="ListBox" Width="100%" Height="80px"></asp:listbox></TD>
										</TR>
									</TABLE>
								</td>
								<td width="4%"></td>
								<td vAlign="top"><asp:label id="Label17" runat="server" CssClass="label"> Field selected</asp:label>
									<DIV style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 180px">
										<table class="grid" id="tblColumnSelect" style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none"
											cellSpacing="0" cellPadding="0" width="100%" border="1">
											<tr>
												<td class="gridHeader" width="50%"><asp:label id="Label18" runat="server" CssClass="label">ColumnName</asp:label></td>
												<td class="gridHeader" width="25%"><asp:label id="Label19" runat="server" CssClass="label">Sequence</asp:label></td>
												<td class="gridHeader" width="25%"><asp:label id="Label20" runat="server" CssClass="label">Width</asp:label></td>
											</tr>
											<% for (int i=1; i<=tblTemp.Rows.Count; i++){%>
											<tr>
												<td width="50%"><a class=Hlink 
                        id="hpColumnName<%=i.ToString()%>" 
                        style="WIDTH: 100%; TEXT-ALIGN: left" 
                        onclick='return ChooseCriteria("<%=i.ToString()%>");' 
                        href='<%=tblTemp.Rows[i -1]["ColumnName"]%>' 
                        ><%=tblTemp.Rows[i -1]["ColumnName"]%></a></td>
												<td width="25%"><input class=input 
                        id="txtSequence<%=i.ToString()%>" 
                        onblur='txtSequenceChange(this,"<%=i.ToString()%>")' 
                        style="WIDTH: 100%; TEXT-ALIGN: center" type=text 
                        value='<%=tblTemp.Rows[i -1]["Sequence"]%>' 
                        > <input class=input 
                        id="txtColumnID<%=i.ToString()%>" 
                        style="WIDTH: 100%; TEXT-ALIGN: center" type=hidden 
                        value='<%=tblTemp.Rows[i -1]["ColumnID"]%>' 
                        >
												</td>
												<td width="25%"><input class=input 
                        id="txtWidth<%=i.ToString()%>" 
                        onblur='txtWidthChange(this,"<%=i.ToString()%>")' 
                        style="WIDTH: 100%; TEXT-ALIGN: center" type=text 
                        value='<%=tblTemp.Rows[i - 1]["Width"]%>' 
                        ></td>
											</tr>
											<%}
											tblTemp.Dispose();
											%>
										</table>
									</DIV>
								</td>
							</tr>
						</table>
					</TD>
				</TR>
				<tr>
					<td id="aaa" align="center"><asp:label id="Label9" runat="server" CssClass="label">Sort by</asp:label></td>
					<td colSpan="3"><asp:label id="Label12" runat="server" CssClass="label">Col 1</asp:label><asp:dropdownlist id="cboSort1" onchange="ChangeSort(1)" runat="server" CssClass="combo" Width="15.6%"></asp:dropdownlist><asp:label id="Label13" runat="server" CssClass="label">Col 2</asp:label><asp:dropdownlist id="cboSort2" runat="server" onchange="ChangeSort(2)" CssClass="combo" Width="15.6%"></asp:dropdownlist><asp:label id="Label14" runat="server" CssClass="label">Col 3</asp:label><asp:dropdownlist id="cboSort3" runat="server" onchange="ChangeSort(3)" CssClass="combo" Width="15.6%"></asp:dropdownlist><asp:label id="Label15" runat="server" CssClass="label">Col 4</asp:label><asp:dropdownlist id="cboSort4" runat="server" CssClass="combo" onchange="ChangeSort(4)" Width="15.6%"></asp:dropdownlist><asp:label id="Label16" runat="server" CssClass="label">Col 5</asp:label><asp:dropdownlist id="cboSort5" onchange="ChangeSort(5)" runat="server" CssClass="combo" Width="15.6%"></asp:dropdownlist></td>
				</tr>
				<TR style="DISPLAY: none">
					<TD><asp:label id="Label11" runat="server" CssClass="label">Select report</asp:label></TD>
					<TD colSpan="3"><asp:dropdownlist id="cboReportList" runat="server" CssClass="combo" Width="100%"></asp:dropdownlist></TD>
				</TR>
				<TR style="DISPLAY: none">
					<td><asp:label id="Label10" runat="server" CssClass="label">Report name</asp:label></td>
					<TD colSpan="3"><asp:textbox id="txtReportName" runat="server" CssClass="input" Width="100%" MaxLength="255"></asp:textbox></TD>
				</TR>
				<TR>
					<TD align="center" colSpan="4">
						<HR width="100%" SIZE="1">
					</TD>
				</TR>
				<tr>
					<td align="center" colSpan="4"><asp:linkbutton id="btnSaveReport" runat="server" CssClass="btn" Visible="False"> Save query</asp:linkbutton>&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnView" runat="server" CssClass="btn">View</asp:linkbutton><INPUT id="txtSelectColumn" style="WIDTH: 8px; HEIGHT: 22px" type="hidden" size="1" name="Hidden1"
							runat="server"><INPUT id="txtDisplayColumn" style="WIDTH: 8px; HEIGHT: 22px" type="hidden" size="1" name="Hidden1"
							runat="server"><INPUT id="txtConditionView" style="WIDTH: 8px; HEIGHT: 22px" type="hidden" size="1" name="Hidden1"
							runat="server"><INPUT id="txtConditionCode" style="WIDTH: 8px; HEIGHT: 22px" type="hidden" size="1" name="Hidden1"
							runat="server"><INPUT id="txtSortCondition" style="WIDTH: 8px; HEIGHT: 22px" type="hidden" size="1" name="Hidden1"
							runat="server"><INPUT id="txtAllGridColumn" style="WIDTH: 8px; HEIGHT: 22px" type="hidden" size="1" name="txtAllGridColumn"
							runat="server"></td>
				</tr>
			</table>
		</td>
	</tr>
</TABLE>
<script language="javascript">
function InsertRow(strColumnName, strColumnID)
{
	document.getElementById("tblColumnSelect").insertRow();
	var countRow =document.getElementById("tblColumnSelect").rows.length;
	var nItemID = countRow - 1;
	document.getElementById("tblColumnSelect").rows(countRow-1).insertCell();
	document.getElementById("tblColumnSelect").rows(countRow-1).insertCell();
	document.getElementById("tblColumnSelect").rows(countRow-1).insertCell();
	
	var oNewNode = document.createElement("<a class=Hlink style='width:100%;' id='hpColumnName" + (nItemID) + "' onclick='return ChooseCriteria(" + nItemID + ");' style='TEXT-ALIGN: left>" + strColumnName + "</a>");
	document.getElementById("tblColumnSelect").rows(countRow-1).cells(0).appendChild(oNewNode);
	
	var oNewNode = document.createElement("<input id='txtSequence" + (nItemID) + "' type='text' onblur='txtSequenceChange(this," + nItemID + ")' value='" + nItemID + "' style='width:100%;' class='input' style='TEXT-ALIGN: center'/>");
	document.getElementById("tblColumnSelect").rows(countRow-1).cells(1).appendChild(oNewNode);
	var oNewNode = document.createElement("<input id='txtColumnID" + (nItemID) + "' type='hidden' style='width:100%;' class='input' style='TEXT-ALIGN: center'/>");
	document.getElementById("tblColumnSelect").rows(countRow-1).cells(1).appendChild(oNewNode);
	
	var oNewNode = document.createElement("<input id='txtWidth" + (nItemID) + "' type='text' onblur='txtWidthChange(this," + nItemID + ")' value='100' style='width:100%;' class='input' style='TEXT-ALIGN: center'/>");
	document.getElementById("tblColumnSelect").rows(countRow-1).cells(2).appendChild(oNewNode);
	
	document.getElementById("txtColumnID" + nItemID).value = strColumnID;
	document.getElementById("hpColumnName" + nItemID).innerText = strColumnName;
	document.getElementById("hpColumnName" + nItemID).href = strColumnName;
	txtSequenceChange(document.getElementById("txtSequence" + nItemID), nItemID);
	return false;
}

function txtSequenceChange(field, index)
{
	if (checkNumeric(field) == false)
		return;
	var strColumnID = document.getElementById("txtColumnID" + index).value;
	var strColumnName = document.getElementById("hpColumnName" + index).innerText;
	var Exist = -1;
	var count = document.getElementById("_ctl0_cboSort1").length;
	for (i=0; i<count; i++)
	{
		if(document.getElementById("_ctl0_cboSort1").options[i].value == strColumnID)
		{
			Exist = i;
			break;
		}
	}
	if (field.value != "")
	{
		if (Exist == -1) //Chua co trong cac combo sort
		{
			document.getElementById("_ctl0_cboSort1").add(new Option(strColumnName + " inc",strColumnID));
			document.getElementById("_ctl0_cboSort2").add(new Option(strColumnName + " inc",strColumnID));
			document.getElementById("_ctl0_cboSort3").add(new Option(strColumnName + " inc",strColumnID));
			document.getElementById("_ctl0_cboSort4").add(new Option(strColumnName + " inc",strColumnID));
			document.getElementById("_ctl0_cboSort5").add(new Option(strColumnName + " inc",strColumnID));
			document.getElementById("_ctl0_cboSort1").add(new Option(strColumnName + " desc",strColumnID));
			document.getElementById("_ctl0_cboSort2").add(new Option(strColumnName + " desc",strColumnID));
			document.getElementById("_ctl0_cboSort3").add(new Option(strColumnName + " desc",strColumnID));
			document.getElementById("_ctl0_cboSort4").add(new Option(strColumnName + " desc",strColumnID));
			document.getElementById("_ctl0_cboSort5").add(new Option(strColumnName + " desc",strColumnID));
		}
	}
	else
	{
		if (Exist != -1)
		{
			var index = count - 1;
			while (i <= index)
			{
				if(document.getElementById("_ctl0_cboSort1").options[i].value == strColumnID)
				{
					document.getElementById("_ctl0_cboSort1").remove(i);
					document.getElementById("_ctl0_cboSort2").remove(i);
					document.getElementById("_ctl0_cboSort3").remove(i);
					document.getElementById("_ctl0_cboSort4").remove(i);
					document.getElementById("_ctl0_cboSort5").remove(i);
					index = index - 1;
				}
				index = index - 1;
			}
		}
	}
	return false;
}

function txtWidthChange(field, index)
{
	if (checkNumeric(field) == false)
		return;
	if (field.value == "")
		field.value = 100;
}
function ChooseCriteria(index)
{
	var strColumnID = document.getElementById("txtColumnID" + index).value;
	var strColumnName = document.getElementById("hpColumnName" + index).innerText;
	document.getElementById("_ctl0_txtCriteria").value = strColumnName;
	document.getElementById("_ctl0_txtCriteriaID").value = strColumnID;
	document.getElementById("_ctl0_txtInfo").value = "";
	document.getElementById("_ctl0_txtValue").value = "";
	document.getElementById("btnAddInfo").focus();
	return false;
}

function OpenInfoFunction()
{
	if (document.getElementById("_ctl0_txtCriteriaID").value == "")
	{
		alert("Choose column for making condition!");
		document.getElementById("tblColumnSelect").focus();
		return;
	}
	var formName = 'FormPage.aspx?Ascx=MdlHR/InfoEmpSearch.ascx&ColumnID=' + document.getElementById("_ctl0_txtCriteriaID").value;
	objShowDialog = window.open(formName ,'Recipient','status=1,toolbar=0,scrollbars=1,resizable=1,alwaysRaised=Yes, top=' + 400 + ', left=' + 500 + ', width=' + 450 + ', height=' + 220 + ',1 ,align=center')	
	objShowDialog.focus();
}

function ChangeSort(index) 
{
	var ItemIndex = document.getElementById("_ctl0_cboSort" + index).selectedIndex;
	if ((Math.round(ItemIndex/2)) == (parseFloat(ItemIndex)/2))
		document.getElementById("_ctl0_cboSort" + index).title = "desc";
	else
		document.getElementById("_ctl0_cboSort" + index).title = "";
	
}

function OpenFrmResult()
{
	var formName = 'FormPage.aspx?Ascx=MdlHR/EmpSearchResult.ascx';
	objShowDialog = window.open(formName ,'Recipient','status=1,toolbar=0,scrollbars=1,resizable=1,alwaysRaised=Yes, top=' + 400 + ', left=' + 500 + ', width=' + 450 + ', height=' + 220 + ',1 ,align=center')	
	objShowDialog.focus();
	return false;
}
</script>
<script><asp:Literal id="ltlAlert" runat="server" EnableViewState="False"></asp:Literal></script>
<%if (!Page.IsPostBack) {%>
<script language=javascript>
//Khoi tao cho cboSort
document.getElementById("_ctl0_cboSort1").add(new Option("",""));
document.getElementById("_ctl0_cboSort2").add(new Option("",""));
document.getElementById("_ctl0_cboSort3").add(new Option("",""));
document.getElementById("_ctl0_cboSort4").add(new Option("",""));
document.getElementById("_ctl0_cboSort5").add(new Option("",""));
</script>
<%}%>

