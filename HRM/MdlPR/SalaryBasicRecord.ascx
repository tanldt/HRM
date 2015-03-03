<%@ Register TagPrefix="uc1" TagName="EmpHeader" Src="../Include/EmpHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ColumnList" Src="../Include/ColumnList.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="SalaryBasicRecord.ascx.cs" Inherits="iHRPCore.MdlPR.SalaryBasicRecord" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Import Namespace ="System.Data.SqlClient" %>
<%@ Import Namespace ="System.Data" %>
<%@ Import Namespace ="iHRPCore.Com"%>
<%
	string strLangID = Session["LangID"] == null?"EN":Session["LangID"].ToString().Trim();
	int Total1 = 0;
	int Total2 = 0;
			
	DataTable rs1 = new DataTable();
	DataTable rs2 = new DataTable();	
	
	rs1 = clsCommon.GetDataTable("sp_GetDataCombo @TableName = 'LS_tblBSGrade'");
	rs2 = clsCommon.GetDataTable("sp_GetDataCombo @TableName = 'LS_tblBSRank'");
	
	
	Total1 = rs1.Rows.Count;
	Total2 = rs2.Rows.Count;
%>
<SCRIPT language="javascript">					
		var DS1 = new Array(3);
		DS1[0] = new Array(<%=Total1%>);
		DS1[1] = new Array(<%=Total1%>);
		DS1[2] = new Array(<%=Total1%>);
		<% for(int i=0; i<Total1; i++) {%>		
			DS1[0][<%=i%>]="<%=rs1.Rows[i]["LSBSScaleID"]%>";
			DS1[1][<%=i%>]="<%=rs1.Rows[i]["LSBSGradeID"]%>";			
			<% if (strLangID=="EN") {%>
				DS1[2][<%=i%>]="<%=rs1.Rows[i]["Name"]%>";	
			<%} else{%>
				DS1[2][<%=i%>]="<%=rs1.Rows[i]["VNName"]%>";
			<%}%>
		<%}%>		
		
		var DS2 = new Array(4);
		DS2[0] = new Array(<%=Total2%>);
		DS2[1] = new Array(<%=Total2%>);
		DS2[2] = new Array(<%=Total2%>);
		DS2[3] = new Array(<%=Total2%>);
		<% for(int i=0; i<Total2; i++) {%>		
			DS2[0][<%=i%>]="<%=rs2.Rows[i]["LSBSGradeID"]%>";
			DS2[1][<%=i%>]="<%=rs2.Rows[i]["LSBSRankID"]%>";
			<% if (strLangID=="EN") {%>
				DS2[2][<%=i%>]="<%=rs2.Rows[i]["Name"]%>";	
			<%} else{%>
				DS2[2][<%=i%>]="<%=rs2.Rows[i]["VNName"]%>";
			<%}%>
			DS2[3][<%=i%>]="<%=rs2.Rows[i]["Coefficient"]%>";
		<%}%>						
</SCRIPT>
<script language="javascript">		
	function ChangeGrade()
	{
		var all = document.getElementById("_ctl0_cboLSBSRankID").length;		
		for(i=0; i<all; i++)
		{	
			document.getElementById("_ctl0_cboLSBSRankID").remove(0);			
		};
		
		var strGrade = document.getElementById("_ctl0_cboLSBSGradeID").value; 
		document.getElementById("_ctl0_cboLSBSRankID").add(new Option('',''));
		
		for(i=0; i<<%=Total2%>;i++)
		{
			if (DS2[0][i]==strGrade)
			{
				document.getElementById("_ctl0_cboLSBSRankID").add(new Option(DS2[2][i],DS2[1][i]));
			};
		};
		
		all = document.getElementById("_ctl0_cboLSBSRankID").length;
		document.getElementById("_ctl0_cboLSBSRankID").selectedIndex = 0;
		document.getElementById("_ctl0_txtLSBSGradeID").value = strGrade;
		ChangeRank();			
	}
	
	function ChangeRank()
	{
		
		document.getElementById("_ctl0_txtLSBSRankID").value = document.getElementById("_ctl0_cboLSBSRankID").value;
		var strRankID=document.getElementById("_ctl0_cboLSBSRankID").value;
		for(i=0; i<<%=Total2%>;i++)
		{
			//alert(strRankID);
			if (DS2[1][i]==strRankID)
			{				
				document.getElementById("_ctl0_txtSalCoef").value = DS2[3][i];
			};
		};
		//document.getElementById("_ctl0_txtRankID").value = strRankID;	
	}	
</script>
<TABLE id="MainTable" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td>
			<TABLE id="tblHead" cellSpacing="0" cellPadding="0" width="95%" border="0">
				<TR>
					<TD align="center" width="100%" colSpan="3"><asp:label id="lblErr" ForeColor="Red" runat="server" CssClass="lblErr"></asp:label></TD>
				</TR>
				<TR>
					<TD align="center" width="100%" colSpan="3"><uc1:empheader id="HR_EmpHeader" runat="server"></uc1:empheader></TD>
				</TR>
			</TABLE>
			<table id="tblInfo" width="95%">
				<tr>
					<td>
						<!-- Div LUONG CHUC DANH-->
						<table id="tbl2" cellSpacing="0" cellPadding="1" width="100%" border="0">
							<TR>
								<TD width="15%"><asp:label id="lblEffDate" runat="server" CssClass="labelRequire">Effective Date</asp:label></TD>
								<TD width="20%"><asp:textbox id="txtEffDate" onblur="javascript:CheckDate(this)" runat="server" CssClass="input"
										Width="70px" MaxLength="10"></asp:textbox>&nbsp;<INPUT class=btnCal onclick="javascript:popUpCalendar(<%= this.txtEffDate.ClientID %>)" type=button></TD>
								<TD width="4%"></TD>
								<TD width="10%"><asp:label id="lblEndDate" runat="server" CssClass="label">End Date</asp:label></TD>
								<TD width="20%"><asp:textbox id="txtEndDate" onblur="javascript:CheckDate(this)" runat="server" CssClass="InputReadOnly"
										Width="70px" MaxLength="10" ReadOnly="True"></asp:textbox>
								</TD>
							</TR>
							<TR>
								<TD width="15%">
									<asp:label id="Label2" CssClass="labelRequire" runat="server">Actual Date</asp:label></TD>
								<TD width="20%">
									<asp:textbox id="txtActualDate" onblur="javascript:CheckDate(this)" CssClass="input" runat="server"
										MaxLength="10" Width="70px"></asp:textbox>&nbsp;<INPUT class=btnCal onclick="javascript:popUpCalendar(<%= this.txtEffDate.ClientID %>)" type=button></TD>
								<TD width="4%"></TD>
								<TD width="10%"></TD>
								<TD width="20%"></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 9px"><asp:checkbox id="chkAdAll2" runat="server" CssClass="chkAdmin" Checked="True"></asp:checkbox><asp:label id="lblAllowanceCoef2" runat="server" CssClass="label">Current salary</asp:label></TD>
								<TD style="HEIGHT: 9px"><asp:textbox id="txtOldSalary" onblur="javascript:checkNumeric(this)" runat="server" CssClass="InputReadOnly"
										Width="60%" MaxLength="12" ReadOnly="True"></asp:textbox></TD>
								<td style="HEIGHT: 9px"></td>
								<TD style="HEIGHT: 9px"><asp:checkbox id="chkAdAll1" runat="server" CssClass="chkAdmin" Checked="True"></asp:checkbox><asp:label id="lblAllowanceCoef1" runat="server" CssClass="labelRequire">Increase (%)</asp:label></TD>
								<TD style="HEIGHT: 9px"><asp:textbox id="txtRaisePercent" onblur="javascript:CheckRaisePercent(this, 1000)" runat="server"
										CssClass="input" Width="88px" MaxLength="5"></asp:textbox></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 9px"><asp:label id="Label1" runat="server" CssClass="labelRequire">New Salary</asp:label></TD>
								<TD style="HEIGHT: 9px"><asp:textbox id="txtNewSalary" onblur="javascript:checkNumeric(this)" runat="server" CssClass="input"
										Width="60%" MaxLength="9"></asp:textbox></TD>
								<TD style="HEIGHT: 9px"></TD>
								<TD style="HEIGHT: 9px"></TD>
								<TD style="HEIGHT: 9px"></TD>
							</TR>
							<TR>
								<TD><asp:label id="lblNote" runat="server" CssClass="label">Note</asp:label></TD>
								<TD colSpan="4"><asp:textbox id="txtNote" runat="server" CssClass="input" Width="100%" MaxLength="255" TextMode="MultiLine"
										Height="58px"></asp:textbox></TD>
							</TR> <!-- END DIV LUONG CHUC DANH --></table>
					</td>
				</tr>
			</table> <!-- end button for input form --></td>
	</tr> <!-- start button for input form -->
	<tr>
		<td>
			<HR align="center" width="100%">
		</td>
	</tr>
	<TR>
		<TD noWrap align="center">
			<TABLE id="tblButton" cellSpacing="0" cellPadding="0" width="99%" border="0">
				<TR vAlign="top" height="30">
					<TD vAlign="middle" width="15%"></TD>
					<TD align="center" width="*"><asp:linkbutton id="btnAddNew" accessKey="N" runat="server" CssClass="btnAddnew" ToolTip="Alt+N">New Record</asp:linkbutton>&nbsp;&nbsp;
						<asp:linkbutton id="btnSave" accessKey="S" runat="server" CssClass="btnSave" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp;&nbsp;&nbsp;<asp:linkbutton id="btnDelete" accessKey="D" runat="server" CssClass="btnExport" ToolTip="Alt + D">Delete</asp:linkbutton>&nbsp;&nbsp;
						<asp:linkbutton id="btnImport" accessKey="I" runat="server" CssClass="btnImport" ToolTip="Alt+I, Import data from excel file same format with export file">Import</asp:linkbutton>&nbsp;&nbsp;
						<asp:linkbutton id="btnExport" accessKey="E" runat="server" CssClass="btnExport" ToolTip="Alt+E, Export data in grid to excel file">Export</asp:linkbutton></TD>
				</TR>
			</TABLE>
		</TD>
	</TR> <!-- end button for input form -->
	<TR>
		<TD noWrap align="center">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD width="12%">
						<asp:checkbox id="chkSelectAll" onclick="CheckAll('_ctl0_chkSelectAll','_ctl0_grdAllowance',3,1,'chkSelect')"
							CssClass="checkbox" runat="server" Text="Select all"></asp:checkbox></TD>
					<TD>
						<uc1:ColumnList id="uctrlColumns" runat="server"></uc1:ColumnList></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR id="trGrid" runat="server">
		<TD align="center"><!-- start grid for input form -->
			<TABLE id="tblGrid" cellSpacing="0" cellPadding="1" width="98%" border="0">
				<TR id="trGrid1"> <!-- start grid detail for input form -->
					<TD colSpan="2"><asp:datagrid id="dtgList" runat="server" CssClass="grid" Width="100%" AllowPaging="True" BackColor="White"
							BorderColor="#3366CC" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False" AllowSorting="True"
							PageSize="15">
							<FooterStyle CssClass="gridFooter"></FooterStyle>
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle CssClass="gridHeader"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="BasicSalaryID" HeaderText="ID"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Select">
									<HeaderStyle HorizontalAlign="Center" Width="7%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox id="chkSelect" runat="server" Enabled="False" CssClass="checkbox"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn Visible="False" HeaderText="&lt;b&gt;Seq&lt;/b&gt;">
									<HeaderStyle Width="1%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<%# Container.ItemIndex + 1%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:ButtonColumn DataTextField="EffDate" HeaderText="Effective Date" CommandName="EDIT">
									<HeaderStyle Width="15%" HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
								</asp:ButtonColumn>
								<asp:BoundColumn DataField="EndDate" HeaderText="End Date">
									<HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="OldSalary" HeaderText="Current Salary">
									<HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="RaisePercent" HeaderText="Increase (%)">
									<HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="NewSalary" HeaderText="New Salary">
									<HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Note" HeaderText="Note">
									<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE> <!-- end grid for input form --><INPUT id="txtID" style="WIDTH: 96px; HEIGHT: 22px" type="hidden" size="10" name="txtLeavePolicyID"
				runat="server" Width="120px"><INPUT id="txtNewSalary_Hidden" style="WIDTH: 96px; HEIGHT: 22px" type="hidden" size="10"
				name="txtLeavePolicyID" runat="server" Width="120px"> <INPUT id="txtSelectedIndex" type="hidden" name="txtSelectedIndex" runat="server">
		</TD>
	</TR>
	<TR>
		<TD style="DISPLAY: none" align="center"><asp:textbox id="txtAttachFile" runat="server"></asp:textbox><INPUT id="txtBasicSalaryID" type="hidden" name="txtBasicSalaryID" runat="server"><INPUT id="txtLSBSRankID" type="hidden" name="txtLSBSRankID" runat="server"><INPUT id="txtLSBSGradeID" type="hidden" name="txtLSBSGradeID" runat="server"></TD>
	</TR>
</TABLE>
<SCRIPT type="text/javascript">	

function CheckSave()
{
	if (document.getElementById('_ctl0_HR_EmpHeader_cboEmployeeID').selectedIndex == 0)
	{
		GetAlertError(iTotal,DSAlert,"0074");	
		document.getElementById('_ctl0_HR_EmpHeader_cboEmployeeID').focus();
		return false;
	}

	if(!checkisnull('txtEffDate'))  
		return false;		
		
	// Check thoi gian EffDate hop le	
	var EffDate = document.getElementById('_ctl0_txtEffDate').value;		
	var StartDate = document.getElementById('_ctl0_HR_EmpHeader_lblStartDate').innerHTML;	
	if (StartDate != '' && FromSmallOrEqualToDateStr(StartDate,EffDate) == false)
	{		
		GetAlertError(iTotal,DSAlert,"0112");
		return false;		
	}
	
	var rowCount = document.getElementById("_ctl0_dtgList").rows.length;		
	var isValid = true;
	if(rowCount > 1)
	{
		for(var i = 1; i < document.getElementById("_ctl0_dtgList").rows.length - 1; i ++)
		{
			var k = i - 1;
			var str = '' + k;			
			if (str != document.getElementById('_ctl0_txtSelectedIndex').value)
			{
				var EffDateData = document.getElementById('_ctl0_dtgList').rows[i].cells[1].firstChild.innerHTML;																
				if (IsDateSmaller(EffDate, EffDateData))
				{
					isValid = false;
					break;
				}
			}
		}
		if(isValid == false)
		{
			GetAlertError(iTotal,DSAlert,"0110");
			document.getElementById('_ctl0_txtEffDate').focus();
			return false;
		}
	}
		
	if(!checkisnull('txtRaisePercent') || !checkisnull('txtNewSalary'))  
		return false;		
	
	return true;
}
	
function CheckDelete()
{
	if(GridCheck('_ctl0_dtgList',3,1,'chkSelect')==false)
	{			
		GetAlertError(iTotal,DSAlert,"0001");				
		return false;
	}
	if(confirm(GetAlertText(iTotal,DSAlert,"0002"))==false)
		return false;		
}

function ShowExcelSelectPage()
{		
	window.open('./MdlPR/FileSelect.aspx?tpl=../TemplateExcel/BasicSalary_FileSelect.xls'
				+ '&Store=PR_spfrmBASICSALARY&Act=SaveImport','SelectFile','status=1,toolbar=0,scrollbars=1,resizable=0,alwaysRaised=Yes, top=20, left=30, width=800, height=600,1 ,align=center');
	return false;
}

function checkisnull(obj)
{
	if(document.getElementById('_ctl0_' + obj).value=="")
	{				
		GetAlertError(iTotal,DSAlert,"0003");				
		document.getElementById('_ctl0_' + obj).focus();
		return false;
	}
	return true;
}	

function CheckRaisePercent(field, maxValue)
{
	if (!MyCheckNumeric(field, maxValue))
		return false;
	// var temp = document.getElementById('_ctl0_' + 'txtNewSalary_Hidden').value.replace(',', '');	
	var temp = document.getElementById('_ctl0_' + 'txtOldSalary').value.replace(',', '');	
	temp = temp.replace(',', ''); 	
	var oldSalary = parseFloat(temp);	
	temp = field.value.replace(',', ''); 	
	var raisePercent = parseFloat(temp) / 100;		
	var newSalary = Math.round(oldSalary * (1 + raisePercent));		
	document.getElementById('_ctl0_' + 'txtNewSalary').value = newSalary;
	//document.getElementById('_ctl0_' + 'txtNewSalary').value = MyFormatNumeric(newSalary, 1);
}	

// Chi lay toi da 1 so thap phan thoi
function MyFormatNumeric(value, numDecimal)
{	
	var strValue = '' + value;	
	var index = strValue.indexOf('.', 0);
	if ((index != -1) && (strValue.length - index > parseInt(numDecimal) + 1))
	{
		strValue = strValue.substr(0, index + parseInt(numDecimal) + 1);
	}
	return strValue;
}

function MyCheckNumeric(field,MaxValue)
{	
	if (field.value == "")
		return false;		
	var re;   
	re = /,/g;
	var value = field.value.replace(re,'');
	if (isNaN(value))
	{
		GetAlertError(iTotal,DSAlert,"0013");
		field.value = "";
		field.focus();
		return false;
	}
	//tanldt: Test yeu cau 1 so cho phai lon hon 0
	if (value < 0)
	{		
		//alert("Must not a negative numeric or zero!");
		GetAlertError(iTotal,DSAlert,"0012");
		field.value = "";
		field.focus();
		return false;
	}if (value>MaxValue)
	{
		GetAlertTextPopUp(GetAlertText(iTotal,DSAlert,'0054').replace('@',MaxValue))
		//alert('');
		field.focus();
		return false;
	}
	FormatNumericWithSeparate(field);
	return true;
}

// FromDate < ToDate 
function IsDateSmaller(inputStr1, inputStr2)
{
	var delim1 = inputStr1.indexOf("/")
	var delim2 = inputStr1.lastIndexOf("/")
	
	// lay ngay, thang, nam cua ngay1
	var dd1 = parseInt(inputStr1.substring(0,delim1),10)
	var mm1 = parseInt(inputStr1.substring(delim1 + 1,delim2),10)
	var yyyy1 = parseInt(inputStr1.substring(delim2 + 1,inputStr1.length),10)

	delim1 = inputStr2.indexOf("/")
	delim2 = inputStr2.lastIndexOf("/")

	// lay ngay, thang, nam cua ngay2
	dd2 = parseInt(inputStr2.substring(0,delim1),10)
	mm2 = parseInt(inputStr2.substring(delim1 + 1,delim2),10)
	yyyy2 = parseInt(inputStr2.substring(delim2 + 1,inputStr2.length),10)
	
	
	// ngay1 nho hon ngay 2 ?
	if (yyyy2 > yyyy1)
		return true;
	else if (yyyy2 == yyyy1)
	{
		if (mm2 == mm1) 
			{
				
				if (dd2 >= dd1) 
				{				
				return true;
				}				
				else
				{				
				 return false;
				}
			}
		else
			{
				if (mm2 > mm1) return true;
				else return false;
			}
	}
	else
		return false;
}

</SCRIPT>
