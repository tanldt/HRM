<%@ Import Namespace ="iHRPCore.Com"%>
<%@ Import Namespace ="System.Data" %>
<%@ Import Namespace ="System.Data.SqlClient" %>
<%@ Register TagPrefix="uc1" TagName="ColumnList" Src="../Include/ColumnList.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ListOfSalary_Popup.ascx.cs" Inherits="iHRPCore.MdlPR.ListOfSalary_Popup" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%
	string strLangID = Session["LangID"] == null?"EN":Session["LangID"].ToString().Trim();
	string sAccountLogin= Session["AccountLogin"]!=null?Session["AccountLogin"].ToString():"admin";
	string Ascx=Request.Params["Ascx"];		
	int Total1 = 0;
	int Total2 = 0;
	int Total3 = 0;
			
	DataTable rs1 = new DataTable();
	DataTable rs2 = new DataTable();		
	DataTable rs3 = new DataTable();	
	
	rs1 = clsCommon.GetDataTable("sp_GetDataComboLevel @TableName = 'LS_tblLevel1', @Language='" + strLangID + "',@UserGroupID='" + sAccountLogin + "'");
	rs2 = clsCommon.GetDataTable("sp_GetDataComboLevel @TableName = 'LS_tblLevel2', @Language='" + strLangID + "',@UserGroupID='" + sAccountLogin + "'");
	rs3 = clsCommon.GetDataTable("sp_GetDataComboLevel @TableName = 'LS_tblLevel3', @Language='" + strLangID + "',@UserGroupID='" + sAccountLogin + "'");
	
	Total1 = rs1.Rows.Count;
	Total2 = rs2.Rows.Count;
	Total3 = rs3.Rows.Count;		
%>
<script language="javascript">					
		var DS1 = new Array(3);
		DS1[0] = new Array(<%=Total1%>);
		DS1[1] = new Array(<%=Total1%>);
		DS1[2] = new Array(<%=Total1%>);	
		<% for(int i=0; i<Total1; i++) {%>		
			DS1[0][<%=i%>]="<%=rs1.Rows[i]["LSCompanyID"]%>";
			DS1[1][<%=i%>]="<%=rs1.Rows[i]["LSLevel1ID"]%>";
			DS1[2][<%=i%>]="<%=rs1.Rows[i]["Name"]%>";	
		<%}%>	
		
		var DS2 = new Array(3);
		DS2[0] = new Array(<%=Total2%>);
		DS2[1] = new Array(<%=Total2%>);
		DS2[2] = new Array(<%=Total2%>);	
		<% for(int i=0; i<Total2; i++) {%>		
			DS2[0][<%=i%>]="<%=rs2.Rows[i]["LSLevel1ID"]%>";
			DS2[1][<%=i%>]="<%=rs2.Rows[i]["LSLevel2ID"]%>";
			DS2[2][<%=i%>]="<%=rs2.Rows[i]["Name"]%>";	
		<%}%>		
		
		var DS3 = new Array(3);
		DS3[0] = new Array(<%=Total3%>);
		DS3[1] = new Array(<%=Total3%>);
		DS3[2] = new Array(<%=Total3%>);
		<% for(int i=0; i<Total3; i++) {%>		
			DS3[0][<%=i%>]="<%=rs3.Rows[i]["LSLevel2ID"]%>";
			DS3[1][<%=i%>]="<%=rs3.Rows[i]["LSLevel3ID"]%>";
			DS3[2][<%=i%>]="<%=rs3.Rows[i]["Name"]%>";	
		<%}%>	
</script>
<script language="javascript">	
	function ChangeCompany()
	{
		var all = document.getElementById("_ctl0:cboLevel2").length;
		var all1 = document.getElementById("_ctl0:cboLevel3").length;
		var all2 = document.getElementById("_ctl0:cboLevel1").length;
		for(i=0; i<all2; i++)
		{	
			document.getElementById("_ctl0:cboLevel1").remove(0);			
		};
		
		for(i=0; i<all; i++)
		{	
			document.getElementById("_ctl0:cboLevel2").remove(0);			
		};
		for(i=0; i<all1; i++)
		{	
			document.getElementById("_ctl0:cboLevel3").remove(0);			
		};
		var strCompany = document.getElementById("_ctl0:cboCompany").value; 
		document.getElementById("_ctl0:cboLevel1").add(new Option('',''));
		for(i=0; i<<%=Total1%>;i++)
		{
			if (DS1[0][i]==strCompany)
			{
				document.getElementById("_ctl0:cboLevel1").add(new Option(DS1[2][i],DS1[1][i]));
			};
		};		
		all = document.getElementById("_ctl0:cboLevel1").length;
		document.getElementById("_ctl0:cboLevel1").selectedIndex = 0;
		document.Form1("_ctl0:Com").value = "1";
		//ChangeLevel1();			
	}
	
	function ChangeLevel1()
	{
		var all = document.getElementById("_ctl0:cboLevel2").length;
		var all1 = document.getElementById("_ctl0:cboLevel3").length;
		
		for(i=0; i<all; i++)
		{	
			document.getElementById("_ctl0:cboLevel2").remove(0);			
		};
		for(i=0; i<all1; i++)
		{	
			document.getElementById("_ctl0:cboLevel3").remove(0);			
		};
		var strLevel1ID = document.getElementById("_ctl0:cboLevel1").value; 
		document.getElementById("_ctl0:cboLevel2").add(new Option('',''));
		
		for(i=0; i<<%=Total2%>;i++)
		{
			if (DS2[0][i]==strLevel1ID)
			{
				document.getElementById("_ctl0:cboLevel2").add(new Option(DS2[2][i],DS2[1][i]));
			};
		};		
		all = document.getElementById("_ctl0:cboLevel2").length;
		document.getElementById("_ctl0:cboLevel2").selectedIndex = 0;
		document.getElementById("_ctl0:txtLevel1ID").value = strLevel1ID;
		document.getElementById("_ctl0:Level1").value = "1";
		//ChangeLevel2();			
	}
	
	function ChangeLevel2()
	{
		var all = document.getElementById("_ctl0:cboLevel3").length;
		
		for(i=0; i<all; i++)
		{	
			document.getElementById("_ctl0:cboLevel3").remove(0);			
		};
		var strLevel2ID = document.getElementById("_ctl0:cboLevel2").value; 
		document.getElementById("_ctl0:cboLevel3").add(new Option('',''));
		
		for(i=0; i<<%=Total3%>;i++)
		{
			if (DS3[0][i]==strLevel2ID)
			{
				document.getElementById("_ctl0:cboLevel3").add(new Option(DS3[2][i],DS3[1][i]));
			};
		};		
		document.getElementById("_ctl0:txtLevel2ID").value = document.getElementById("_ctl0:cboLevel2").value;
		document.getElementById("_ctl0:Level2").value = "1";
		//ChangeLevel3();	
	}
	function ChangeLevel3()
	{	
		document.getElementById("_ctl0:txtLevel3ID").value = document.getElementById("_ctl0:cboLevel3").value;
		document.getElementById("_ctl0:Level3").value = "1";
	}
</script>
<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
	<tr>
		<TD align="center"><asp:label id="lblErr" CssClass="labelRequire" runat="server"></asp:label>&nbsp;
			<asp:label id="lblUpdate" runat="server" CssClass="labelRequire"></asp:label></TD>
	</tr>
	<TR>
		<TD align="center">
			<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="90%" border="0">
				<TR>
					<TD width="15%"><asp:label id="Label4" CssClass="labelRequire" runat="server"> Salary Item</asp:label></TD>
					<TD width="35%"><asp:textbox id="txtLSSalaryItemCode" CssClass="input" runat="server" MaxLength="15" Width="100%"></asp:textbox></TD>
					<TD width="25%"></TD>
					<TD style="HEIGHT: 20px" width="30%"></TD>
				</TR>
				<TR>
					<TD><asp:label id="Label6" CssClass="labelRequire" runat="server"> Name</asp:label></TD>
					<TD><asp:textbox id="txtName" CssClass="input" runat="server" MaxLength="150" Width="100%"></asp:textbox></TD>
					<TD><asp:label id="Label15" CssClass="labelRequire" runat="server"> Vietnam Name</asp:label></TD>
					<TD><asp:textbox id="txtVNName" CssClass="input" runat="server" MaxLength="150" Width="100%"></asp:textbox></TD>
				</TR>
				<TR>
					<TD><asp:label id="Label5" CssClass="label" runat="server">Fomular</asp:label></TD>
					<TD colSpan="4"><asp:textbox id="txtFormula" CssClass="input" runat="server" MaxLength="1000" Width="100%" TextMode="MultiLine"></asp:textbox></TD>
				</TR>
				<TR>
					<TD><asp:label id="Label1" CssClass="label" runat="server"> Value</asp:label></TD>
					<TD><asp:textbox id="txtValue" onblur="javascript:checkNumber(this)" CssClass="input" runat="server"
							MaxLength="9" Width="100%"></asp:textbox></TD>
					<TD><asp:label id="Label2" CssClass="labelRequire" runat="server"> Computiation Seq</asp:label></TD>
					<TD><asp:textbox id="txtComputationSeq" MaxLength="1" onblur="javascript:checkNumeric(this)" CssClass="input"
							runat="server" Width="100%"></asp:textbox></TD>
				</TR>
				<TR>
					<TD><asp:label id="Label7" CssClass="labelRequire" runat="server"> Sequence</asp:label></TD>
					<TD><asp:textbox id="txtRank" MaxLength="1" onblur="javascript:checkNumeric(this)" CssClass="input"
							runat="server" Width="100%"></asp:textbox></TD>
					<TD><asp:label id="Label3" CssClass="labelRequire" runat="server"> Report Seq</asp:label></TD>
					<TD style="HEIGHT: 15px"><asp:textbox MaxLength="2" id="txtRptOrdinal" onblur="javascript:checkNumeric(this)" CssClass="input"
							runat="server" Width="100%"></asp:textbox></TD>
				</TR>
				<TR>
					<TD><asp:label id="Label8" CssClass="label" runat="server"> Item Type</asp:label></TD>
					<TD><asp:dropdownlist id="cboLSSalaryItemTypeID" CssClass="select" runat="server" Width="100%"></asp:dropdownlist></TD>
					<TD></TD>
					<TD style="HEIGHT: 25px"></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD><asp:checkbox id="chkVisible" CssClass="checkbox" runat="server" Width="120px" Checked="True"
							Text="show in screen"></asp:checkbox>
							<div style="DISPLAY: none">
							<asp:checkbox id="chkUsed" CssClass="checkbox" runat="server" Checked="True" Text="Used"></asp:checkbox>
							</div>
							</TD>
					<TD>
						<asp:checkbox id="chkIsDefaultAssign" CssClass="checkbox" runat="server" Checked="True" Text="Default"></asp:checkbox>&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:label id="Label19" runat="server" CssClass="label">Kỳ lương</asp:label></TD>
					<TD style="HEIGHT: 25px">
						<asp:dropdownlist id="cboAssignSalPeriod" runat="server" CssClass="select" Width="100%">
							<asp:ListItem></asp:ListItem>
							<asp:ListItem Value="1">Kỳ 1</asp:ListItem>
							<asp:ListItem Value="2">Kỳ 2</asp:ListItem>
							<asp:ListItem Value="3">Kỳ 3</asp:ListItem>
						</asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD><asp:label id="Label17" CssClass="label" runat="server" Width="100%"> Note</asp:label></TD>
					<TD colSpan="4"><asp:textbox id="txtNote" CssClass="input" runat="server" MaxLength="150" Width="100%"></asp:textbox></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD colSpan="4"><asp:checkbox id="chkAssign" onclick="javascript:MethodChange(this)" CssClass="checkbox" runat="server"
							Text="Assign Salary Item to Employee"></asp:checkbox></TD>
				</TR>
				<TR id="tr1" style="DISPLAY: none" runat="server">
					<TD colSpan="5">
						<HR width="100%" SIZE="1">
						<INPUT id="txtLevel2ID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtLevel2ID"
							runat="server"> <INPUT id="txtLevel3ID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtLevel3ID"
							runat="server"> <INPUT id="txtLevel1ID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtLevel1ID"
							runat="server"> <INPUT id="Com" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="Com" runat="server">
						<INPUT id="Level1" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="Level1"
							runat="server"> <INPUT id="Level2" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="Level2"
							runat="server"> <INPUT id="Level3" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="Level3"
							runat="server"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
						&nbsp; <INPUT id="txtLSSalaryItemID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtDeductionID"
							runat="server"><INPUT id="txtChangeStatus" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtDeductionID"
							runat="server">
					</TD>
				</TR>
				<TR id="tr2" style="DISPLAY: none" runat="server">
					<TD><asp:label id="Label10" CssClass="label" runat="server">Tháng</asp:label></TD>
					<TD width="30%"><asp:textbox id="txtFromMonth" onblur="javascript:CheckMonthYear(this)" CssClass="input" runat="server"
							MaxLength="7" Width="100px"></asp:textbox></TD>
					<TD><asp:label id="Label9" CssClass="label" runat="server">Kỳ lương</asp:label></TD>
					<TD><asp:dropdownlist id="cboSalPeriod" CssClass="select" runat="server" Width="100%">
							<asp:ListItem></asp:ListItem>
							<asp:ListItem Value="1">Kỳ 1</asp:ListItem>
							<asp:ListItem Value="2">Kỳ 2</asp:ListItem>
							<asp:ListItem Value="3">Kỳ 3</asp:ListItem>
						</asp:dropdownlist></TD>
				</TR>
				<TR id="tr3" style="DISPLAY: none" runat="server">
					<TD><asp:label id="Label16" CssClass="label" runat="server">EmpID</asp:label></TD>
					<TD width="30%"><asp:textbox id="txtEmpID" CssClass="input" runat="server" MaxLength="12" Width="100px"></asp:textbox></TD>
					<TD><asp:label id="Label18" CssClass="label" runat="server">Location</asp:label></TD>
					<TD><asp:dropdownlist id="cboLocation" CssClass="select" runat="server" Width="100%"></asp:dropdownlist></TD>
				</TR>
				<TR id="tr4" style="DISPLAY: none" runat="server">
					<TD><asp:label id="Label11" CssClass="label" runat="server">Company</asp:label></TD>
					<TD><asp:dropdownlist onkeypress="checkKey('_ctl0_btnSearch')" id="cboCompany" CssClass="combo" runat="server"
							Width="95%" onchange="ChangeCompany()"></asp:dropdownlist></TD>
					<TD><asp:label id="Label12" CssClass="label" runat="server"> Division</asp:label></TD>
					<TD><asp:dropdownlist onkeypress="checkKey('_ctl0_btnSearch')" id="cboLevel1" CssClass="combo" runat="server"
							Width="100%" onchange="ChangeLevel1()"></asp:dropdownlist></TD>
				</TR>
				<TR id="tr5" style="DISPLAY: none" runat="server">
					<TD><asp:label id="Label13" CssClass="label" runat="server">Department</asp:label></TD>
					<TD><asp:dropdownlist onkeypress="checkKey('_ctl0_btnSearch')" id="cboLevel2" CssClass="combo" runat="server"
							Width="95%" onchange="ChangeLevel2()"></asp:dropdownlist></TD>
					<TD><asp:label id="Label14" CssClass="label" runat="server"> Section</asp:label></TD>
					<TD style="HEIGHT: 16px"><asp:dropdownlist onkeypress="checkKey('_ctl0_btnSearch')" id="cboLevel3" CssClass="combo" runat="server"
							Width="100%" onchange="ChangeLevel3()"></asp:dropdownlist></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD align="center"><asp:linkbutton id="btnFilter" accessKey="S" CssClass="btnSave" runat="server" ToolTip="ALT+S">Filter</asp:linkbutton>&nbsp;&nbsp;<asp:linkbutton id="btnSave" accessKey="S" CssClass="btnSave" runat="server" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp;
			<asp:linkbutton id="btnClose" accessKey="Y" CssClass="Button" runat="server" ToolTip="ALT+C">Close</asp:linkbutton></TD>
	</TR>
	<TR>
		<TD align="left" height="10">
			<uc1:ColumnList id="uctrlColumns" runat="server"></uc1:ColumnList></TD>
	</TR>
	<TR>
		<TD align="center"><asp:datagrid id="grdOvertime" CssClass="grid" runat="server" Width="100%" BorderColor="#3366CC"
				PageSize="10" AllowSorting="True" AutoGenerateColumns="False" AllowPaging="True" CellPadding="0" BorderWidth="1px"
				BackColor="White">
				<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
				<ItemStyle CssClass="gridItem"></ItemStyle>
				<HeaderStyle CssClass="gridHeader"></HeaderStyle>
				<FooterStyle CssClass="gridFooter"></FooterStyle>
				<Columns>
					<asp:BoundColumn DataField="EmpID" Visible="False" HeaderText="EmpID" />
					<asp:TemplateColumn HeaderText="&lt;b&gt;Seq&lt;/b&gt;">
						<HeaderStyle Width="5%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<%# Container.ItemIndex + 1%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="EmpCode" HeaderText="EmpCode">
						<HeaderStyle Width="7%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="Level1ShortName" HeaderText="Division">
						<HeaderStyle Width="25%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="Level2ShortName" HeaderText="Department">
						<HeaderStyle Width="25%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="Level3ShortName" HeaderText="Section">
						<HeaderStyle Width="25%"></HeaderStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn HeaderText="Select">
						<HeaderStyle Width="3%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<HeaderTemplate>
							<asp:CheckBox id="chkSelectAll" runat="server" CssClass="checkbox" onclick="CheckAll('_ctl0_grdOvertime__ctl2_chkSelectAll','_ctl0_grdOvertime',3,1,'chkSelect')"></asp:CheckBox>
						</HeaderTemplate>
						<ItemTemplate>
							<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
			</asp:datagrid></TD>
	</TR>
</TABLE>
<script language="javascript">

	function MethodChange(obj)
	{		
		if (obj.checked==true)
		{	
			document.getElementById('_ctl0_tr1').style.display = "block";
			document.getElementById('_ctl0_tr2').style.display = "block";
			document.getElementById('_ctl0_tr3').style.display = "block";
			document.getElementById('_ctl0_tr4').style.display = "block";
			document.getElementById('_ctl0_tr5').style.display = "block";
		}else
		{
			document.getElementById('_ctl0_tr1').style.display = "none"; 
			document.getElementById('_ctl0_tr2').style.display = "none";
			document.getElementById('_ctl0_tr3').style.display = "none";
			document.getElementById('_ctl0_tr4').style.display = "none";
			document.getElementById('_ctl0_tr5').style.display = "none";
		}		
	}
function CheckSave()
{
	if (document.getElementById('<%=txtLSSalaryItemCode.ClientID%>').value == "")
	{
		alert('<%=GetText("LISTOFSALARY","CONFLSSALARYITEMCODE")%>');
		document.getElementById('<%=txtLSSalaryItemCode.ClientID%>').focus();
		return false;
	}
	if (document.getElementById('<%=txtName.ClientID%>').value == "")
	{
		alert('<%=GetText("LISTOFSALARY","CONFNAME")%>');
		document.getElementById('<%=txtName.ClientID%>').focus();
		return false;
	}
	if (document.getElementById('<%=txtVNName.ClientID%>').value == "")
	{
		alert('<%=GetText("LISTOFSALARY","CONFVNNAME")%>');
		document.getElementById('<%=txtVNName.ClientID%>').focus();
		return false;
	}
	if (document.getElementById('<%=txtComputationSeq.ClientID%>').value == "")
	{
		alert('<%=GetText("LISTOFSALARY","CONFCOMPUTRIONSEQ")%>');
		document.getElementById('<%=txtComputationSeq.ClientID%>').focus();
		return false;
	}
	if (document.getElementById('<%=txtRptOrdinal.ClientID%>').value == "")
	{
		alert('<%=GetText("LISTOFSALARY","CONFRPTORDINAL")%>');
		document.getElementById('<%=txtRptOrdinal.ClientID%>').focus();
		return false;
	}
	if (document.getElementById('<%=txtRank.ClientID%>').value == "")
	{
		alert('<%=GetText("LISTOFSALARY","CONFRANK")%>');
		document.getElementById('<%=txtRank.ClientID%>').focus();
		return false;
	}
	//if (document.getElementById('<%=chkAssign.ClientID%>').checked == true)
	//{
		if(GridCheck('_ctl0_grdOvertime',3,1,'chkSelect')==true)
		{
			if (document.getElementById('<%=chkAssign.ClientID%>').checked == false)
			{
				alert('<%=GetText("LISTOFSALARY","CONFASSIGN")%>');
				document.getElementById('<%=chkAssign.ClientID%>').focus();
				return false;
			}
			if (document.getElementById('<%=txtFromMonth.ClientID%>').value == "")
			{
				alert('<%=GetText("LISTOFSALARY","CONFFROMMONTH")%>');
				document.getElementById('<%=txtFromMonth.ClientID%>').focus();
				return false;
			}
			if (document.getElementById('<%=cboSalPeriod.ClientID%>').value == "")
			{
				alert('<%=GetText("LISTOFSALARY","CONFSALPERIOD")%>');
				document.getElementById('<%=cboSalPeriod.ClientID%>').focus();
				return false;
			}
		}
	//}
	
	return true;
}

</script>
<script language="javascript">
function CheckFilter()
{
	if (document.getElementById('<%=chkAssign.ClientID%>').checked == false)
	{
		alert('<%=GetText("LISTOFSALARY","CONFASSIGN")%>');
		document.getElementById('<%=chkAssign.ClientID%>').focus();
		return false;
	}
	return true;
}
function PopUp_Addnew()
		{			
			//ShowDialog('HR\Residence.aspx',700,430);
			ShowDialog('FormPage.aspx?ModuleID=PR&ParentID=68&FunctionID=546&Ascx=MdlPR/ListOfSalary_Popup.ascx',800,600);
		}
function doRefreshParent()
{
	if(opener != null)
	{
		btn = eval("opener.document.getElementById('_ctl0_btnRefresh')");
		if(btn != null)
			btn.click();
		window.close();
	}
}

</script>

