<%@ Control Language="c#" AutoEventWireup="false" Codebehind="EmpListViewByStr.ascx.cs" Inherits="iHRPCore.Include.EmpListViewByStr" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="98%" border="0">
	<TR>
		<TD align="center"><asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></TD>
	</TR>
	<TR>
		<td>
			<asp:datagrid id="dtgGridSelected" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="0"
				AutoGenerateColumns="False" AllowSorting="True" PageSize="20" Width="100%" runat="server"
				CssClass="grid">
				<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
				<ItemStyle CssClass="gridItem"></ItemStyle>
				<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
				<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
				<Columns>
					<asp:BoundColumn Visible="False" DataField="EmpID">
						<HeaderStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Middle"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn HeaderText="&lt;b&gt;Seq&lt;/b&gt;">
						<HeaderStyle Width="5%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<%# Container.ItemIndex + 1%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Empl.ID">
						<HeaderStyle Width="10%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:HyperLink id="Hyperlink1" CssClass=Hlink Width="95%" Text='<%# DataBinder.Eval(Container, "DataItem.EmpCode") %>' Runat="server">
							</asp:HyperLink>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="EmpName" HeaderText="Full Name">
						<HeaderStyle Width="20%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="PositionName" HeaderText="Position">
						<HeaderStyle Width="30%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="StartDateStr" HeaderText="Start date">
						<HeaderStyle Width="13%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
			</asp:datagrid>
		</td>
	</TR>
	<tr>
		<td height="10" align="center"></td>
	</tr>
	<tr>
		<td align="center"><asp:linkbutton id="btnExport" accessKey="E" CssClass="btnExport" runat="server" ToolTip="Alt+E, Export data in grid to excel file">Export</asp:linkbutton></td>
	</tr>
</TABLE>
