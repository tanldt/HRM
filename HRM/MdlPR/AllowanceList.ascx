<%@ Control Language="c#" AutoEventWireup="false" Codebehind="AllowanceList.ascx.cs" Inherits="iHRPCore.MdlPR.AllowanceList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="ColumnList" Src="../Include/ColumnList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../Include/EmpHeaderSearch.ascx" %>
<script language="javascript" src="../Include/common.js">
</script>
<script language="javascript">

</script>
<TABLE cellSpacing="0" cellPadding="0" width="99%" align="center" border="0">
	<TR>
		<TD noWrap align="left"><asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD noWrap align="center"><uc1:empheadersearch id="EmpHeaderSearch1" runat="server"></uc1:empheadersearch></TD>
	</TR>
	<TR>
		<TD align="center">
			<!-- start detail for list form -->
			<TABLE cellSpacing="0" cellPadding="1" width="100%" border="0">
				<TR>
					<TD width="11%"><asp:label id="Label3" CssClass="label" runat="server" Width="104px"> Type</asp:label></TD>
					<TD width="26%"><asp:dropdownlist id="cboAllowanceType" CssClass="combo" runat="server" Width="92%"></asp:dropdownlist></TD>
					<TD width="10%"><asp:label id="Label1" CssClass="label" runat="server">From Month</asp:label></TD>
					<TD noWrap width="10%"><asp:textbox id="txtFromDate" onblur="javascript:CheckMonthYear(this)" CssClass="input" runat="server"
							Width="75px" MaxLength="7"></asp:textbox></TD>
					<TD width="9%"><asp:label id="Label2" CssClass="label" runat="server">To Month</asp:label></TD>
					<TD noWrap width="14%"><asp:textbox id="txtToDate" onblur="javascript:CheckMonthYear(this)" CssClass="input" runat="server"
							Width="75px" MaxLength="7"></asp:textbox></TD>
				</TR>
			</TABLE>
			<!-- end button for list form --></TD>
	</TR>
	<TR>
		<TD vAlign="top" noWrap align="center" height="20">
			<HR align="center" width="95%">
		</TD>
	</TR>
	<TR> <!-- start button for list form -->
		<TD noWrap align="center">
			<TABLE id="tblButton" cellSpacing="0" cellPadding="0" width="98%" border="0">
				<TR vAlign="top" height="35">
					<TD vAlign="middle" width="15%"><asp:checkbox id="chkShowGrid" onclick="javascript:collapse('tblGrid');" CssClass="checkbox" runat="server"
							Visible="False" Checked="True" Text="Show grid"></asp:checkbox></TD>
					<TD align="center" width="*"><asp:linkbutton id="btnSearch" accessKey="E" CssClass="btnSearch" runat="server" ToolTip="Alt+ S">Search</asp:linkbutton>&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnDelete" accessKey="D" CssClass="btnDelete" runat="server" ToolTip="Alt+D">Delete</asp:linkbutton>&nbsp;&nbsp; 
						&nbsp;
						<asp:linkbutton id="btnExport" accessKey="E" CssClass="btnExport" runat="server" ToolTip="Alt+E">Export</asp:linkbutton></TD>
				</TR>
			</TABLE>
		</TD>
	</TR> <!-- end button for list form -->
	<TR>
		<TD align="center"><!-- start grid for list form -->
			<TABLE id="tblGrid" cellSpacing="0" cellPadding="1" width="99%" border="0">
				<TR>
					<TD id="tdPages" width="43%"><uc1:columnlist id="uctrlColumns" runat="server"></uc1:columnlist></TD>
					<TD id="tdSort" style="DISPLAY: none" align="right" width="*"><asp:label id="Label8" CssClass="labelRight" runat="server" Width="30px">  Col 1</asp:label><asp:dropdownlist id="cboCol1" CssClass="combo" runat="server" Width="80px"></asp:dropdownlist><asp:label id="Label10" CssClass="labelRight" runat="server" Width="30px">Col 2</asp:label>&nbsp;
						<asp:dropdownlist id="cboCol2" CssClass="combo" runat="server" Width="80px"></asp:dropdownlist><asp:label id="Label15" CssClass="labelRight" runat="server" Width="30px">Col 3</asp:label>&nbsp;
						<asp:dropdownlist id="cboCol3" CssClass="combo" runat="server" Width="80px"></asp:dropdownlist></TD>
				</TR>
				<TR id="trGrid"> <!-- start grid detail for list form -->
					<TD colSpan="2"><asp:datagrid id="grdAllowance" CssClass="grid" runat="server" Width="100%" PageSize="15" AllowSorting="True"
							AutoGenerateColumns="False" AllowPaging="True" CellPadding="0" BorderWidth="1px" BorderColor="#3366CC"
							BackColor="White">
							<FooterStyle CssClass="gridFooter"></FooterStyle>
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle CssClass="gridHeader"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="AllowanceID" HeaderText="ID">
									<HeaderStyle Width="7%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Select">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<HeaderTemplate>
										<asp:CheckBox id="chkSelectAll" runat="server" CssClass="checkbox" onclick="CheckAll('_ctl0_grdAllowance__ctl2_chkSelectAll','_ctl0_grdAllowance',3,1,'chkSelect')"></asp:CheckBox>
									</HeaderTemplate>
									<ItemTemplate>
										<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Seq">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<%# (grdAllowance.PageSize*grdAllowance.CurrentPageIndex) + Container.ItemIndex + 1%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn Visible="False" DataField="EmpID" SortExpression="EmpID" HeaderText="Emp ID"></asp:BoundColumn>
								<asp:BoundColumn DataField="EmpCode" SortExpression="EmpCode" HeaderText="Emp Code"></asp:BoundColumn>
								<asp:BoundColumn DataField="FullName" SortExpression="FullName" HeaderText="Employee">
									<HeaderStyle Width="25%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:ButtonColumn DataTextField="FromMonth" SortExpression="FromMonth" HeaderText="From" CommandName="EDIT">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:ButtonColumn>
								<asp:BoundColumn DataField="ToMonth" HeaderText="To">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="AllowanceType" SortExpression="AllowanceType" HeaderText="Type">
									<HeaderStyle Width="23%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Amount" HeaderText="Amount">
									<HeaderStyle Width="12%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Unit" SortExpression="Unit" HeaderText="Unit">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ToPR" HeaderText="PR">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="IsGross" HeaderText="Gross/NET">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD noWrap height="5">&nbsp;</TD>
	</TR>
</TABLE>
<script language="javascript">
<!--
function Search()
{	if(document.getElementById('_ctl0_txtFromDate').value!="" && document.getElementById('_ctl0_txtToDate').value!="")
	{
		if( IsSmallerOrEqual('01/'+document.getElementById('_ctl0_txtFromDate').value,'01/'+document.getElementById('_ctl0_txtToDate').value) == false)
		{		
			GetAlertError(iTotal, DSAlert, "0007");
			document.getElementById('_ctl0_txtFromDate').focus();
			return false;
		}
	}
	return true;
}
function checkdelete()
{
	if(GridCheck('_ctl0_grdAllowance',3,1,'chkSelect')==false)
	{
		GetAlertError(iTotal,DSAlert,"0001");
		return false;
	}
	if(confirm(GetAlertText(iTotal,DSAlert,"0002"))==false){
		return false;
	}
}
function checkKey(){	
	if(window.event.keyCode==13){		
		document.getElementById('_ctl0_btnRowNumber').click();		
		event.returnValue=false;
		event.cancel = true;		
	}
}
//-->
</script>
