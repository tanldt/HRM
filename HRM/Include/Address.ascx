<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Address.ascx.cs" Inherits="iHRPCore.Include.Address" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table width="100%">
	<tr>
		<td width="10%"><asp:label id="Label7" CssClass="label" runat="server" Width="100%">Address</asp:label></td>
		<td width="23%"><asp:textbox id="txtP_Address" CssClass="input" runat="server" Width="100%" MaxLength="100"></asp:textbox></td>
		<td width="15%"><asp:label id="Label1" CssClass="label" runat="server" Width="100%">District</asp:label></td>
		<td width="21%"><asp:textbox id="Textbox1" CssClass="input" runat="server" Width="100%" MaxLength="100"></asp:textbox></td>
		<td width="10%"><asp:label id="Label2" CssClass="label" runat="server" Width="100%"> Province</asp:label></td>
		<td width="20%">
			<asp:dropdownlist id="cboProvinceID" Width="100%" runat="server" CssClass="combo" onchange="ChangeProvince()"></asp:dropdownlist></td>
	</tr>
</table>
