<%@ Control Language="c#" AutoEventWireup="false" Codebehind="LoanList.ascx.cs" Inherits="iHRPCore.MdlPR.LoadList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../Include/EmpHeaderSearch.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ColumnList" Src="../Include/ColumnList.ascx" %>
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
			<TABLE cellSpacing="0" cellPadding="1" width="99%" border="0">
				<TR>
					<TD width="11%"><asp:label id="Label1" CssClass="label" runat="server"> Purpose</asp:label></TD>
					<TD width="26%"><asp:dropdownlist id="cboLSLoanPurposeCode" CssClass="select" runat="server" Width="90%"></asp:dropdownlist></TD>
					<TD align="right" width="10%"><asp:label id="Label2" CssClass="label" runat="server">From Month</asp:label>&nbsp;</TD>
					<TD width="30%" colSpan="3"><asp:textbox id="txtFromMonth" onblur="javascript:CheckMonthYear(this)" CssClass="input" runat="server"
							Width="75px" MaxLength="7"></asp:textbox>&nbsp;&nbsp;
						<asp:label id="Label3" CssClass="label" runat="server">To Month</asp:label>&nbsp;<asp:textbox id="txtToMonth" onblur="javascript:CheckMonthYear(this)" CssClass="input" runat="server"
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
					<TD id="tdPages" width="43%" colSpan="2" height="18">
						<uc1:columnlist id="uctrlColumns" runat="server"></uc1:columnlist></TD>
				</TR>
				<TR id="trGrid"> <!-- start grid detail for list form -->
					<TD colSpan="2"><asp:datagrid id="grdLoan" CssClass="grid" runat="server" Width="100%" PageSize="50" AllowSorting="True"
							AutoGenerateColumns="False" AllowPaging="True" CellPadding="0" BorderWidth="1px" BorderColor="#3366CC"
							BackColor="White">
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle CssClass="gridHeader"></HeaderStyle>
							<FooterStyle CssClass="gridFooter"></FooterStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="LoanID" HeaderText="ID"></asp:BoundColumn>
								<asp:BoundColumn DataField="EmpID" Visible="False" SortExpression="EmpID" HeaderText="Emp ID">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="&lt;b&gt;Seq&lt;/b&gt;">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<%# Container.ItemIndex + 1 + grdLoan.PageSize*grdLoan.CurrentPageIndex%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:ButtonColumn DataTextField="FromMonth" HeaderText="From" ItemStyle-CssClass="hLink" Text="From" CommandName="EDIT"></asp:ButtonColumn>
								<asp:BoundColumn DataField="EmpCode" SortExpression="EmpCode" HeaderText="Emp Code">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="EmpName" SortExpression="EmpName" HeaderText="Employee">
									<HeaderStyle Width="150px"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="LoanPurpose" SortExpression="LoanPurpose" HeaderText="Purpose">
									<HeaderStyle Width="20%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="LoanAmount" HeaderText="Amount">
									<HeaderStyle Width="12%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="InterestRate" HeaderText="Rate">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="SupportRate" HeaderText="Support">
									<HeaderStyle Width="8%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="NoOfMonth" SortExpression="NoOfMonth" HeaderText="Months">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Select">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<HeaderTemplate>
										<asp:CheckBox id="chkSelectAll" runat="server" CssClass="checkbox" onclick="CheckAll('_ctl0_grdLoan__ctl2_chkSelectAll','_ctl0_grdLoan',3,1,'chkSelect')"></asp:CheckBox>
									</HeaderTemplate>
									<ItemTemplate>
										<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE> <!-- end grid for input form --></TD>
	</TR>
</TABLE>
<script language="javascript">
<!--
function Search()
{
	 return true;
}
function checkdelete()
{
	if(GridCheck('_ctl0_grdLoan',3,1,'chkSelect')==false)
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
