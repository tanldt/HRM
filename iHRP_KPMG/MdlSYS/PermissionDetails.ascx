<%@ Control Language="c#" AutoEventWireup="false" Codebehind="PermissionDetails.ascx.cs" Inherits="iHRPCore.MdlSYS.PermissionDetails" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="98%" border="0">
	<TR>
		<TD align="center"><asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center">
			<table width="100%">
				<tr>
					<td width="10%"></td>
					<td width="20%"><asp:label id="Label1" CssClass="label" runat="server">Account/GroupID</asp:label></td>
					<td><asp:textbox id="Textbox16" CssClass="input" runat="server" MaxLength="10" Width="200px"></asp:textbox>
					</td>
					<td width="10%"></td>
				</tr>
				<TR>
					<td></td>
					<td><asp:label id="Label2" CssClass="label" runat="server">Account Name/Group Name</asp:label></td>
					<td><asp:textbox id="Textbox1" CssClass="input" runat="server" MaxLength="10" Width="100%"></asp:textbox>
					</td>
					<td></td>
				</TR>
			</table>
		</TD>
	</TR>
	<TR>
		<TD vAlign="top" noWrap align="center" height="20">
			<HR align="center" width="80%">
		</TD>
	</TR>
	<!-- end button for input form -->
	<TR>
		<TD align="left"><!-- start grid for input form -->
			<TABLE id="tblGrid" cellSpacing="0" cellPadding="1" width="98%" border="0">
				<TR>
					<TD width="10%"></TD>
					<TD>
						<asp:label id="Label3" runat="server" CssClass="label">Groups that user belong to or Users belong to group</asp:label></TD>
					<td width="10%"></td>
				</TR>
				<TR id="trGrid"> <!-- start grid detail for input form -->
					<td width="10%"></td>
					<TD><asp:datagrid id="dtgList" CssClass="grid" runat="server" Width="100%" BackColor="White" BorderColor="#3366CC"
							BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False" AllowSorting="True" PageSize="15"
							AllowPaging="True">
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
							<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="No">
									<HeaderStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Seq">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<%# Container.ItemIndex + 1%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="GroupID/Account ">
									<HeaderStyle Width="30%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:HyperLink id="hpLink" Width="95%" Runat="server"></asp:HyperLink>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn HeaderText="Group Name/Account Name">
									<HeaderStyle Width="50%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Select">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
									</ItemTemplate>
									<FooterTemplate>
										<asp:CheckBox id="chkSelectAll" runat="server" CssClass="checkbox"></asp:CheckBox>
									</FooterTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
					<td></td>
				</TR>
				<TR>
					<TD align="center" colSpan="3" height="10"></TD>
				</TR>
				<TR>
					<TD align="center" colspan="3">
						<asp:linkbutton id="btnSave" accessKey="S" runat="server" CssClass="btnSave" ToolTip="ALT+S">Save</asp:linkbutton></TD>
				</TR>
			</TABLE> <!-- end grid for input form --></TD>
	</TR>
</TABLE>
