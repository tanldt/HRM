<%@ Register TagPrefix="uc1" TagName="EmpHeader" Src="../Include/EmpHeader.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Skill.ascx.cs" Inherits="iHRPCore.MdlHR.Skill" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table width="100%">
	<tr>
		<td align="center">
			<uc1:EmpHeader id="HR_EmpHeader" runat="server"></uc1:EmpHeader></td>
	</tr>
	<tr>
		<td align="center" width="100%">
			<table width="100%">
				<tr width="100%">
					<td width="16%">
						<asp:label id="Label5" Width="100%" runat="server" CssClass="label">Skill</asp:label></td>
					<td width="30%">
						<asp:dropdownlist id="cboPosition" Width="100%" runat="server" CssClass="select"></asp:dropdownlist></td>
					<td width="5%"></td>
					<td width="15%">
						<asp:label id="Label1" Width="100%" runat="server" CssClass="label">Level</asp:label></td>
					<td width="30%">
						<asp:textbox id="Textbox1" onblur="JavaScript:checkNumeric(this)" Width="100%" runat="server"
							CssClass="input" MaxLength="9"></asp:textbox></td>
				</tr>
				<tr>
					<td>
						<asp:label id="Label2" Width="100%" runat="server" CssClass="label">Certificate</asp:label></td>
					<td>
						<asp:textbox id="Textbox3" onblur="JavaScript:checkNumeric(this)" Width="100%" runat="server"
							CssClass="input" MaxLength="9"></asp:textbox></td>
					<td></td>
					<td>
						<asp:label id="Label3" Width="100%" runat="server" CssClass="label">Note</asp:label></td>
					<td>
						<asp:textbox id="Textbox2" onblur="JavaScript:checkNumeric(this)" Width="100%" runat="server"
							CssClass="input" MaxLength="9"></asp:textbox></td>
				</tr>
				<tr>
					<td colspan="5"><hr width="100%">
					</td>
				</tr>
				<TR>
					<TD align="center" colSpan="5">
						<asp:linkbutton id="btnAddNew" accessKey="N" runat="server" CssClass="btnAddnew" ToolTip="Alt+N">Add New</asp:linkbutton>&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnSave" accessKey="S" runat="server" CssClass="btnSave" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnDelete" accessKey="D" CssClass="btnExport" runat="server" ToolTip="Alt + D">Delete</asp:linkbutton>&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnList" accessKey="L" CssClass="btnList" runat="server" ToolTip="Alt + L"> List</asp:linkbutton>&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnExport" accessKey="E" runat="server" CssClass="btnExport" ToolTip="Alt+E, Xu?t dl trên lu?i ra file Excel">Export</asp:linkbutton></TD>
				</TR>
				<TR>
					<TD align="center" colSpan="5">
						<asp:datagrid id="dtgList" Width="100%" runat="server" CssClass="grid" AllowPaging="True" BackColor="White"
							BorderColor="#3366CC" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False" AllowSorting="True">
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
							<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="&lt;b&gt;Seq&lt;/b&gt;">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<%# Container.ItemIndex + 1%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Skill" HeaderText="Skill">
									<HeaderStyle Width="30%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn HeaderText="Level">
									<HeaderStyle Width="27%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn HeaderText="Certificate">
									<HeaderStyle Width="30%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Select">
									<HeaderStyle Width="7%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</table>
		</td>
	</tr>
</table>
