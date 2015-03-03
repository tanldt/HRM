<%@ Register TagPrefix="uc1" TagName="ColumnList" Src="../Include/ColumnList.ascx" %>
<%@ Import Namespace ="iHRPCore.Com"%>
<%@ Import Namespace ="System.Data" %>
<%@ Import Namespace ="System.Data.SqlClient" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="PayrollCollection_DirectHire.ascx.cs" Inherits="iHRPCore.MdlPR.PayrollCollection_DirectHire" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
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
		var all = document.getElementById("_ctl0_cboLevel2ID").length;
		var all1 = document.getElementById("_ctl0_cboLevel3ID").length;
		var all2 = document.getElementById("_ctl0_cboLevel1ID").length;
		for(i=0; i<all2; i++)
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
		var strCompany = document.getElementById("_ctl0_cboCompanyID").value; 
		document.getElementById("_ctl0_cboLevel1ID").add(new Option('',''));
		for(i=0; i<<%=Total1%>;i++)
		{
			if (DS1[0][i]==strCompany)
			{
				document.getElementById("_ctl0_cboLevel1ID").add(new Option(DS1[2][i],DS1[1][i]));
			};
		};		
		all = document.getElementById("_ctl0_cboLevel1ID").length;
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
		
		if (strLevel1ID=='')
		{
			for(i=0; i<<%=Total2%>;i++)
				document.getElementById("_ctl0_cboLevel2ID").add(new Option(DS2[2][i],DS2[1][i]));
		}
		all = document.getElementById("_ctl0_cboLevel2ID").length;
		document.getElementById("_ctl0_cboLevel2ID").selectedIndex = 0;
		document.getElementById("_ctl0_txtLevel1ID").value = strLevel1ID;
		ChangeLevel2();			
	}
	
	function ChangeLevel2()
	{
		var all = document.getElementById("_ctl0_cboLevel3ID").length;
		
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
<TABLE id="Table1" style="WIDTH: 100%" cellSpacing="2" cellPadding="2" width="300" border="0">
	<TR>
		<TD align="left"><asp:label id="lblErr" Visible="False" runat="server" CssClass="label"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center">
			<TABLE id="Table2" style="WIDTH: 100%" cellSpacing="1" cellPadding="0" border="0">
				<TR>
					<TD width="15%"><asp:label id="Label5" runat="server" CssClass="label"> Month</asp:label></TD>
					<TD width="30%"><asp:textbox id="txtMonth" onblur="javascript:CheckMonthYear(this)" runat="server" CssClass="input"
							MaxLength="7" Width="70%" ReadOnly="True"></asp:textbox></TD>
					<TD width="15%">&nbsp;<asp:label id="Label1" Visible="False" runat="server" CssClass="label"> Salary Period</asp:label></TD>
					<TD width="50%"><asp:textbox id="txtSalPeriod" Visible="False" runat="server" CssClass="input" MaxLength="12"
							Width="70%" ReadOnly="True"></asp:textbox></TD>
				</TR>
				<TR>
					<TD width="15%"><asp:label id="Label2" runat="server" CssClass="label">Emp ID</asp:label></TD>
					<TD width="30%"><asp:textbox id="txtEmpID" onblur="javascript:CheckMonthYear(this)" runat="server" CssClass="input"
							MaxLength="7" Width="70%" ReadOnly="True"></asp:textbox></TD>
					<TD width="15%"><asp:label id="Label8" runat="server" CssClass="label">Full Name</asp:label></TD>
					<TD width="50%"><asp:textbox id="txtEmpName" onblur="javascript:CheckMonthYear(this)" runat="server" CssClass="input"
							MaxLength="7" Width="70%" ReadOnly="True"></asp:textbox></TD>
				</TR>
				<TR>
					<TD align="left" width="15%"><asp:label id="Label3" runat="server" CssClass="labelRequire">Customer group</asp:label></TD>
					<TD><asp:dropdownlist id="cboCompanyID" runat="server" CssClass="select" Width="100%" onchange="ChangeCompany()"></asp:dropdownlist></TD>
					<TD>&nbsp;<asp:label id="Label4" runat="server" CssClass="label">Company</asp:label></TD>
					<TD><asp:dropdownlist id="cboLevel1ID" runat="server" CssClass="select" Width="100%" onchange="ChangeLevel1()"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD align="left"><asp:label id="Label6" runat="server" CssClass="label">Department</asp:label></TD>
					<TD><asp:dropdownlist id="cboLevel2ID" runat="server" CssClass="select" Width="100%" onchange="ChangeLevel2()"></asp:dropdownlist></TD>
					<TD>&nbsp;<asp:label id="Label7" runat="server" CssClass="label">Group</asp:label></TD>
					<TD><asp:dropdownlist id="cboLevel3ID" runat="server" CssClass="select" Width="100%" onchange="ChangeLevel3()"></asp:dropdownlist></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:linkbutton id="btnNewPayroll" accessKey="S" runat="server" CssClass="btnSave" ToolTip="ALT+S">Create</asp:linkbutton><asp:linkbutton id="btnCalulate" accessKey="E" runat="server" CssClass="btnExport" ToolTip="Alt + T"> Calculate</asp:linkbutton><asp:linkbutton id="btnView" accessKey="E" runat="server" CssClass="btnExport">View</asp:linkbutton><asp:linkbutton id="btnDelete" accessKey="E" runat="server" CssClass="btnExport">Delete</asp:linkbutton><asp:linkbutton id="btnLock" accessKey="E" runat="server" CssClass="btnExport">Lock Salary</asp:linkbutton><asp:linkbutton id="btnReCalculate" accessKey="E" runat="server" CssClass="btnExport">Re-calculate</asp:linkbutton><asp:linkbutton id="btnPrint" accessKey="E" runat="server" CssClass="btnExport">Print</asp:linkbutton></TD>
	</TR>
	<TR id="trColumnList" style="DISPLAY: none" runat="server">
		<TD align="left" height="10"><uc1:columnlist id="uctrlColumns" runat="server"></uc1:columnlist></TD>
	</TR>
	<TR>
		<TD align="center"><asp:datagrid id="grdPayRoll" runat="server" CssClass="grid" Width="100%" BorderColor="#3366CC"
				PageSize="20" AllowSorting="True" AutoGenerateColumns="False" AllowPaging="True" CellPadding="0" BorderWidth="1px"
				BackColor="White">
				<FooterStyle CssClass="gridFooter"></FooterStyle>
				<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
				<ItemStyle CssClass="gridItem"></ItemStyle>
				<HeaderStyle CssClass="gridHeader"></HeaderStyle>
				<Columns>
					<asp:BoundColumn Visible="False" DataField="EmpID"></asp:BoundColumn>
					<asp:TemplateColumn HeaderText="&lt;b&gt;No.&lt;/b&gt;">
						<HeaderStyle Width="5%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<%# Container.ItemIndex + 1 + grdPayRoll.PageSize*grdPayRoll.CurrentPageIndex%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="EmpCode" SortExpression="EmpCode" HeaderText="Emp Code">
						<HeaderStyle Width="5%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="EmpName" SortExpression="EmpName" HeaderText="Emp Name">
						<HeaderStyle Width="25%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Left"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="TongThuNhapUSD" SortExpression="TongThuNhapUSD" HeaderText="Gross Salary">
						<HeaderStyle Width="7%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="LuongThemGio" SortExpression="LuongThemGio" HeaderText="OT">
						<HeaderStyle Width="7%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="TienBHXHNV" SortExpression="TienBHXHNV" HeaderText="SI">
						<HeaderStyle Width="15%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="TienBHYTNV" SortExpression="TienBHYTNV" HeaderText="HI">
						<HeaderStyle Width="5%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="ThueThuNhapChiuThueUSD" SortExpression="ThueThuNhapChiuThueUSD" HeaderText="PIT">
						<HeaderStyle Width="5%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn HeaderText="Monthly Income Tax" Visible="False">
						<HeaderStyle Width="15%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn HeaderText="Deduction"></asp:BoundColumn>
					<asp:BoundColumn DataField="ThuNhapSauThueUSD" SortExpression="ThuNhapSauThueUSD" HeaderText="Net Salary"></asp:BoundColumn>
					<asp:TemplateColumn HeaderText="Select">
						<HeaderStyle Width="8%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<HeaderTemplate>
							<asp:CheckBox id="chkSelectAll" runat="server" CssClass="checkbox" onclick="CheckAll('_ctl0_grdPayRoll__ctl2_chkSelectAll','_ctl0_grdPayRoll',3,1,'chkSelect')"></asp:CheckBox>
						</HeaderTemplate>
						<ItemTemplate>
							<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
			</asp:datagrid></TD>
	</TR>
	<TR style="DISPLAY: none">
		<TD align="center"><INPUT id="txtLevel1ID" type="hidden" name="txtLevel1ID" runat="server"><INPUT id="txtLevel2ID" type="hidden" name="txtLevel2ID" runat="server"><INPUT id="txtLevel3ID" type="hidden" name="txtLevel3ID" runat="server">
			<asp:textbox id="txtFromDate" runat="server"></asp:textbox><asp:textbox id="txtToDate" runat="server"></asp:textbox><asp:textbox id="txtPageLoad" Visible="False" runat="server"></asp:textbox></TD>
	</TR>
</TABLE>
<script language="javascript">
function Lock()
{
	if(confirm(GetAlertText(iTotal,DSAlert,"PC_0002"))==false){
		return false;
	}
}
function checkdelete()
{
	if (document.getElementById('_ctl0_grdPayRoll') != null)
	{
		if(GridCheck('_ctl0_grdPayRoll',3,1,'chkSelect')==false)
		{
			GetAlertError(iTotal,DSAlert,"0001");				
			return false;
		}
		if(confirm(GetAlertText(iTotal,DSAlert,"0002"))==false){
			return false;
		}
	}
	else
		return false;
}
function Calculate()
{
	if (checkisnull("cboCompanyID")==false) return false;
}
function checkisnull(obj){
	if(document.getElementById('_ctl0_' + obj).value=="")
		{
			GetAlertError(iTotal,DSAlert,"0003");				
			document.getElementById('_ctl0_' + obj).focus();
			return false;
		}
	else
		return true;
}
function ShowPage()
{
	var Month = document.getElementById('<%=txtMonth.ClientID%>').value;
	var SalPeriod = '1';//document.getElementById('<%=txtSalPeriod.ClientID%>').value;
	var CompanyID = document.getElementById('<%=cboCompanyID.ClientID%>').value;
	var Level1ID = document.getElementById('<%=cboLevel1ID.ClientID%>').value;
	var Level2ID = document.getElementById('<%=cboLevel2ID.ClientID%>').value;
	var Level3ID = document.getElementById('<%=cboLevel3ID.ClientID%>').value;
	//alert('./MdlPR/SumPayroll.aspx?MMYYYY='+Month+'&SalPeriod=' +SalPeriod + '&Comp=' + CompanyID + '&L1=' +Level1ID+ '&L2=' +Level2ID+ '&L3=' +Level3ID+ '','SelectFile','status=1,toolbar=0,scrollbars=1,resizable=1,alwaysRaised=Yes, top=20, left=30, width=800, height=600,1 ,align=center');
	window.open('MdlPR/SumPayroll1.aspx?MMYYYY='+Month+'&SalPeriod=' +SalPeriod + '&Comp=' + CompanyID + '&L1=' +Level1ID+ '&L2=' +Level2ID+ '&L3=' +Level3ID+ '','SelectFile','status=1,toolbar=1,scrollbars=1,resizable=1,alwaysRaised=Yes, top=20, left=30, width=800, height=600,1 ,align=center');
	return false;
}
</script>
