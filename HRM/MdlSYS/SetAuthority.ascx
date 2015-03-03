<%@ Control Language="c#" AutoEventWireup="false" Codebehind="SetAuthority.ascx.cs" Inherits="MdlSYS.SetAuthority" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD align="center">
			<asp:label id="Label19" Width="100%" CssClass="label" runat="server" Font-Bold="True">SET AUTHORITY</asp:label></TD>
	</TR>
	<TR>
		<TD>
			<asp:label id="Label32" CssClass="labelSubTitle" runat="server" Font-Underline="True">Function:</asp:label></TD>
	</TR>
	<TR>
		<TD>
			<asp:datagrid id="dtgList" DESIGNTIMEDRAGDROP="70" BackColor="White" AllowPaging="True" BorderColor="#3366CC"
				BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False" AllowSorting="True" PageSize="7"
				Width="100%" CssClass="grid" runat="server">
				<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
				<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
				<ItemStyle CssClass="gridItem"></ItemStyle>
				<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
				<Columns>
					<asp:BoundColumn Visible="False" DataField="EmpID" HeaderText="EmpID"></asp:BoundColumn>
					<asp:TemplateColumn HeaderText="#">
						<HeaderStyle Width="5%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<%# Container.ItemIndex + 1 + dtgList.PageSize*dtgList.CurrentPageIndex%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:ButtonColumn DataTextField="EmpCode" SortExpression="EmpCode" HeaderText="ID" CommandName="Edit">
						<HeaderStyle Width="12%"></HeaderStyle>
					</asp:ButtonColumn>
					<asp:BoundColumn DataField="EmpName" SortExpression="EmpName" HeaderText="Full Name">
						<HeaderStyle Width="20%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Left"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="FromDate" SortExpression="FromDate" HeaderText="From Date"></asp:BoundColumn>
					<asp:BoundColumn DataField="ToDate" SortExpression="ToDate" HeaderText="To Date"></asp:BoundColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
			</asp:datagrid></TD>
	</TR>
	<TR>
		<TD></TD>
	</TR>
</TABLE>
<script>
function ReloadOpener()
{
	opener.document.location = opener.document.location;		
	window.close();
}
</script>
<script><asp:Literal id="ltlAlert" runat="server" EnableViewState="False"></asp:Literal></script>
