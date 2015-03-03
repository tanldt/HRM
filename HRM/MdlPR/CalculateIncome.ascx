<%@ Import Namespace ="iHRPCore.Com"%>
<%@ Import Namespace ="System.Data" %>
<%@ Import Namespace ="System.Data.SqlClient" %>
<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../Include/EmpHeaderSearch.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="CalculateIncome.ascx.cs" Inherits="iHRPCore.MdlPR.CalculateIncome" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%
	string strLangID = Session["LangID"] == null?"EN":Session["LangID"].ToString().Trim();
	int Total2 = 0;
	int Total3 = 0;
	int Total1 = 0;
	
	DataTable rs2 = new DataTable();		
	DataTable rs3 = new DataTable();	
	DataTable rs1 = new DataTable();
	
	rs1 = clsCommon.GetDataTable("sp_GetDataCombo @TableName = 'LS_tblLevel1'");
	rs2 = clsCommon.GetDataTable("sp_GetDataCombo @TableName = 'LS_tblLevel2'");
	rs3 = clsCommon.GetDataTable("sp_GetDataCombo @TableName = 'LS_tblLevel3'");	
	
	Total1 = rs1.Rows.Count;
	Total2 = rs2.Rows.Count;
	Total3 = rs3.Rows.Count;		
%>
<script language="javascript">
		//array for level1
		var DS1 = new Array(3);
		DS1[0] = new Array(<%=Total1%>);
		DS1[1] = new Array(<%=Total1%>);
		DS1[2] = new Array(<%=Total1%>);
		<% for(int i=0; i<Total1; i++) {%>
			DS1[0][<%=i%>]="<%=rs1.Rows[i]["LSCompanyCode"]%>";
			DS1[1][<%=i%>]="<%=rs1.Rows[i]["LSLevel1Code"]%>";
			<% if (strLangID=="EN") {%>
				DS1[2][<%=i%>]="<%=rs1.Rows[i]["Name"]%>";
			<%} else{%>
				DS1[2][<%=i%>]="<%=rs1.Rows[i]["VNName"]%>";
			<%}%>
		<%}%>
		//array for level2
		var DS2 = new Array(3);
		DS2[0] = new Array(<%=Total2%>);
		DS2[1] = new Array(<%=Total2%>);
		DS2[2] = new Array(<%=Total2%>);
		<% for(int i=0; i<Total2; i++) {%>		
			DS2[0][<%=i%>]="<%=rs2.Rows[i]["LSLevel1Code"]%>";
			DS2[1][<%=i%>]="<%=rs2.Rows[i]["LSLevel2Code"]%>";
			<% if (strLangID=="EN") {%>
				DS2[2][<%=i%>]="<%=rs2.Rows[i]["Name"]%>";	
			<%} else{%>
				DS2[2][<%=i%>]="<%=rs2.Rows[i]["VNName"]%>";
			<%}%>	
		<%}%>		
		//array for level3
		var DS3 = new Array(3);
		DS3[0] = new Array(<%=Total3%>);
		DS3[1] = new Array(<%=Total3%>);
		DS3[2] = new Array(<%=Total3%>);
		<% for(int i=0; i<Total3; i++) {%>		
			DS3[0][<%=i%>]="<%=rs3.Rows[i]["LSLevel2Code"]%>";
			DS3[1][<%=i%>]="<%=rs3.Rows[i]["LSLevel3Code"]%>";
			<% if (strLangID=="EN") {%>
				DS3[2][<%=i%>]="<%=rs3.Rows[i]["Name"]%>";	
			<%} else{%>
				DS3[2][<%=i%>]="<%=rs3.Rows[i]["VNName"]%>";
			<%}%>	
		<%}%>	
</script>
<script language="javascript">
	function ChangeCompany()
	{
		var al = document.getElementById("_ctl0_cboLevel1ID").length;
		var all = document.getElementById("_ctl0_cboLevel2ID").length;
		var all1 = document.getElementById("_ctl0_cboLevel3ID").length;
		
		for(i=0; i<al; i++)
		{	
			document.getElementById("_ctl0_cboLevel1ID").remove(0);			
		};
		for(i=0; i<all; i++)
		{	
			document.getElementById("_ctl0_cboLevel2ID").remove(0);			
		};
		for(i=0; i<all1; i++)
		{	
			document.getElementById("_ctl0_cboLevel3ID").remove(0);			
		};
		var strCompanyID = document.getElementById("_ctl0_cboCompanyID").value;
		document.getElementById("_ctl0_cboLevel1ID").add(new Option('',''));
		
		for(i=0; i<<%=Total1%>;i++)
		{
			if (DS1[0][i]==strCompanyID)
			{
				document.getElementById("_ctl0_cboLevel1ID").add(new Option(DS1[2][i],DS1[1][i]));
			};
		};
		al = document.getElementById("_ctl0_cboLevel1ID").length;
		document.getElementById("_ctl0_cboLevel1ID").selectedIndex = 0;
		ChangeLevel1();
	}
	function ChangeLevel1()
	{		
		var all = document.getElementById("_ctl0_cboLevel2ID").length;
		var all1 = document.getElementById("_ctl0_cboLevel3ID").length;
		
		for(i=0; i<all; i++)
		{	
			document.getElementById("_ctl0_cboLevel2ID").remove(0);			
		};
		for(i=0; i<all1; i++)
		{	
			document.getElementById("_ctl0_cboLevel3ID").remove(0);			
		};
		var strLevel1ID = document.getElementById("_ctl0_cboLevel1ID").value; 
		document.getElementById("_ctl0_cboLevel2ID").add(new Option('',''));
		
		for(i=0; i<<%=Total2%>;i++)
		{
			if (DS2[0][i]==strLevel1ID)
			{
				document.getElementById("_ctl0_cboLevel2ID").add(new Option(DS2[2][i],DS2[1][i]));
			};
		};		
		all = document.getElementById("_ctl0_cboLevel2ID").length;
		document.getElementById("_ctl0_cboLevel2ID").selectedIndex = 0;
		document.getElementById("_ctl0_txtLevel1ID").value = document.getElementById("_ctl0_cboLevel1ID").value;
		ChangeLevel2();			
	}
	
	function ChangeLevel2()
	{
		var all = document.getElementById("_ctl0_cboLevel2ID").length;
		
		for(i=0; i<all; i++)
		{	
			document.getElementById("_ctl0_cboLevel3ID").remove(0);			
		};
		var strLevel2ID = document.getElementById("_ctl0_cboLevel2ID").value; 
		document.getElementById("_ctl0_cboLevel3ID").add(new Option('',''));
		
		for(i=0; i<<%=Total3%>;i++)
		{
			if (DS3[0][i]==strLevel2ID)
			{
				document.getElementById("_ctl0_cboLevel3ID").add(new Option(DS3[2][i],DS3[1][i]));
			};
		};		
		document.getElementById("_ctl0_txtLevel2ID").value = document.getElementById("_ctl0_cboLevel2ID").value;
		ChangeLevel3();	
	}
	function ChangeLevel3()
	{
		document.getElementById("_ctl0_txtLevel3ID").value = document.getElementById("_ctl0_cboLevel3ID").value;
	}
</script>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="97%" border="0">
	<TR>
		<TD align="center" width="100%" height="10"><asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center"></TD>
	</TR>
	<TR>
		<TD align="center" height="10"><!-- start detail for input form -->
			<TABLE cellSpacing="0" cellPadding="1" width="90%" border="0">
				<TR>
					<TD align="left"><asp:label id="lblCompany" runat="server" CssClass="label"> Company</asp:label>&nbsp; 
						&nbsp;</TD>
					<TD><asp:dropdownlist id="cboCompanyID" runat="server" CssClass="combo" onchange="ChangeCompany()" Width="100%"></asp:dropdownlist></TD>
					<TD>&nbsp;
						<asp:label id="Label12" runat="server" CssClass="label" Width="100%">Division</asp:label></TD>
					<TD><asp:dropdownlist id="cboLevel1ID" runat="server" CssClass="combo" onchange="ChangeLevel1()" Width="80%"></asp:dropdownlist></TD>
				</TR>
				<tr>
					<td style="HEIGHT: 19px" align="left"><asp:label id="lblFromDepartment" runat="server" CssClass="label">Department</asp:label>&nbsp; 
						&nbsp;</td>
					<td style="HEIGHT: 19px"><asp:dropdownlist id="cboLevel2ID" runat="server" CssClass="combo" onchange="ChangeLevel2()" Width="100%"></asp:dropdownlist></td>
					<td style="HEIGHT: 19px">&nbsp;
						<asp:label id="lblFromSection" runat="server" CssClass="label" Width="100%">Section</asp:label></td>
					<td style="HEIGHT: 19px"><asp:dropdownlist id="cboLevel3ID" runat="server" CssClass="combo" onchange="ChangeLevel3()" Width="80%"></asp:dropdownlist></td>
				</tr>
				<TR>
					<TD align="left"><asp:label id="Label1" runat="server" CssClass="labelrequire">Payroll Type</asp:label></TD>
					<TD><asp:dropdownlist id="cboTypeSalary" runat="server" Width="100%">
							<asp:ListItem Value="0" Selected="True">Payroll schedule</asp:ListItem>
							<asp:ListItem Value="1">Maternity Payroll</asp:ListItem>
							<asp:ListItem Value="2">13th Payroll</asp:ListItem>
							<asp:ListItem Value="3">Payroll &amp; variable bonus</asp:ListItem>
						</asp:dropdownlist></TD>
					<TD>&nbsp;<asp:label id="lblType" runat="server" CssClass="label" Width="100%">Employee Type</asp:label></TD>
					<TD><asp:dropdownlist id="cboEmpType" runat="server" CssClass="combo" Width="80%">
							<asp:ListItem Value="0">Prudential VN</asp:ListItem>
							<asp:ListItem Value="2">Ref Office</asp:ListItem>
						</asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD align="left" width="15%" height="24"><asp:label id="lblPeriod" runat="server" CssClass="labelrequire">Salary Period</asp:label></TD>
					<TD width="30%" height="24"><asp:textbox id="txtMonth" onblur="javascript:CheckMonthYear(this)" runat="server" CssClass="input"
							Width="75px" MaxLength="7"></asp:textbox></TD>
					<TD align="left" width="18%" height="24">&nbsp;
						<asp:label id="EmpID" CssClass="label" runat="server" Width="100%">Emp ID</asp:label></TD>
					<TD width="40%" height="24">
						<asp:textbox id="txtEmpID" CssClass="input" runat="server" Width="75px" MaxLength="10"></asp:textbox></TD>
				</TR>
				<TR>
					<td></td>
					<td></td>
					<td><asp:label id="lblCurrency" runat="server" CssClass="label" Width="98px" Visible="False">Currency Type</asp:label></td>
					<td><asp:dropdownlist id="cboCurrencyType" runat="server" CssClass="combo" onchange="ChangeLevel3()" Width="75px"
							Visible="False">
							<asp:ListItem Value="0">VND</asp:ListItem>
							<asp:ListItem Value="1">USD</asp:ListItem>
						</asp:dropdownlist></td>
				</TR>
			</TABLE>
			<INPUT id="txtLevel1ID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtLevel1ID"
				runat="server"> <INPUT id="txtLevel2ID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtLevel2ID"
				runat="server"> <INPUT id="txtLevel3ID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtLevel3ID"
				runat="server">
		</TD>
	</TR>
	<TR>
		<TD noWrap align="center" height="10"></TD>
	</TR>
	<TR>
		<TD style="HEIGHT: 19px" noWrap align="center" height="19"><asp:linkbutton id="btnCalculate" accessKey="S" runat="server" CssClass="btnSave" ToolTip="ALT+S">Calculate</asp:linkbutton>&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:linkbutton id="btnView" accessKey="S" runat="server" CssClass="btnsearch" ToolTip="ALT+S">View</asp:linkbutton>&nbsp; 
			&nbsp;&nbsp;
			<asp:linkbutton id="btnDelete" accessKey="S" runat="server" CssClass="btnDelete" ToolTip="ALT+S">Delete</asp:linkbutton>&nbsp; 
			&nbsp;&nbsp;&nbsp;
			<asp:linkbutton id="btnPrint" accessKey="S" runat="server" CssClass="btnPrint" ToolTip="ALT+P">Print</asp:linkbutton></TD>
	</TR>
	<TR>
		<TD noWrap align="center" height="10"></TD>
	</TR> <!-- end button for input form -->
	<TR>
		<TD noWrap align="center" height="10"></TD>
	</TR>
	<TR>
		<TD noWrap align="left">
			<!--
			<TABLE id="tblHeader" style="VISIBILITY: hidden; BORDER-COLLAPSE: collapse" cellSpacing="0"
				cellPadding="1" width="100%" border="1" runat="server">
				<tr bgColor="lightsteelblue" height="30">
					<td><asp:label id="Label2" runat="server" CssClass="labelData" Width="30px">Seq</asp:label></td>
					<td><asp:label id="Label3" runat="server" CssClass="labelData" Width="60px">Code</asp:label></td>
					<td><asp:label id="Label4" runat="server" CssClass="labelData" Width="200px">Employee</asp:label></td>
					<td><asp:label id="Label5" runat="server" CssClass="labelData" Width="70px">Basic per month</asp:label></td>
					<td><asp:label id="Label6" runat="server" CssClass="labelData" Width="50px">No. of day worked</asp:label></td>
					<td><asp:label id="Label7" runat="server" CssClass="labelData" Width="70px">Basic Pay</asp:label></td>
					<td><asp:label id="Label8" runat="server" CssClass="labelData" Width="70px">Total Salary</asp:label></td>
					<td><asp:label id="Label9" runat="server" CssClass="labelData" Width="70px">Total Deduction</asp:label></td>
					<td><asp:label id="Label10" runat="server" CssClass="labelData" Width="70px">Net Salary</asp:label></td>
					<td><asp:label id="Label11" runat="server" CssClass="labelData" Width="10px">Select</asp:label></td>
				</tr>
			</TABLE>
			--><asp:datagrid id="grdPayroll" runat="server" CssClass="grid" AllowPaging="True" BackColor="White"
				BorderColor="#3366CC" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False">
				<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
				<ItemStyle HorizontalAlign="Right" CssClass="gridItem"></ItemStyle>
				<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
				<FooterStyle CssClass="gridFooter"></FooterStyle>
				<Columns>
					<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID"></asp:BoundColumn>
					<asp:BoundColumn DataField="No" HeaderText="Seq">
						<ItemStyle Width="30px"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="AccountCode" HeaderText="Code">
						<ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="FullName" HeaderText="Employee">
						<ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="SalPerMonth" HeaderText="Basic per month" DataFormatString="{0:#,###}">
						<ItemStyle Width="70px"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="WorkingDay" HeaderText="No. of day worked" DataFormatString="{0:#,###.#}">
						<ItemStyle HorizontalAlign="center" Width="50px"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="BasicSalary" HeaderText="Basic Pay" DataFormatString="{0:#,###}">
						<ItemStyle Width="70px"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="TotalSalary" HeaderText="Total Salary" DataFormatString="{0:#,###}">
						<ItemStyle Width="70px"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="TotalDeduction" HeaderText="Total Deduction" DataFormatString="{0:#,###}">
						<ItemStyle Width="70px"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="NetSalary" HeaderText="NET Salary" DataFormatString="{0:#,###}">
						<ItemStyle Width="70px"></ItemStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn HeaderText="Select">
						<HeaderStyle Width="10px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
			</asp:datagrid></TD>
	</TR>
	<TR id="trGrid" runat="server">
		<TD align="center"><!-- start grid for input form --> <!-- end grid for input form --></TD>
	</TR>
</TABLE>
<SCRIPT language="javascript">
<!--
function validform(){
	if(checkisnull('txtMonth')==false)  return false;	
	return true;
}
function checkisnull(obj)
	{
		if(document.getElementById('_ctl0_' + obj).value=="")
			{
				alert('Please select salary period!');
				document.getElementById('_ctl0_' + obj).focus();
				return false;
			}
		else
			return true;
	}
function checkdelete()
{
	/*if(GridNoHeaderCheck('_ctl0_grdPayroll',2,2,'chkSelect')==false)
	{
		alert('Please select at least one record before delete');
		return false;
	}*/
	if(confirm('Delete checked records, are you sure?')==false){
		return false;
	}
}
function ChangeSalary()
{
	//default
	document.getElementById('_ctl0_cboCurrencyType').style.display = "none";
	document.getElementById('_ctl0_txtMonth').style.display = "block";
	document.getElementById('_ctl0_lblPeriod').style.display = "block";
	if(document.getElementById('_ctl0_cboTypeSalary').value == "1") // thai san
	{
		//document.getElementById('_ctl0_cboEmpType').selectedindex = 0;
		document.getElementById('_ctl0_cboEmpType').style.display = "none";
		document.getElementById('_ctl0_lblType').style.display = "none";
	}
	else
	{
		if(document.getElementById('_ctl0_cboTypeSalary').value == "2") // 13th
		{
			document.getElementById('_ctl0_txtMonth').style.display = "none";
			document.getElementById('_ctl0_lblPeriod').style.display = "none";
		}
		else{
			document.getElementById('_ctl0_txtMonth').style.display = "block";
			document.getElementById('_ctl0_lblPeriod').style.display = "block";
		}
		document.getElementById('_ctl0_lblType').style.display = "block";
		document.getElementById('_ctl0_cboEmpType').style.display = "block";
	}
	ChangeType();
}
function ChangeType()
{
	if(document.getElementById('_ctl0_cboEmpType').value == "3") // foreigner
	{
		document.getElementById('_ctl0_cboCurrencyType').style.display = "block";
		document.getElementById('_ctl0_lblCurrency').style.display = "block";		
	}else{
		document.getElementById('_ctl0_cboCurrencyType').style.display = "none";
		document.getElementById('_ctl0_lblCurrency').style.display = "none";
	}
}
//-->
</SCRIPT>
