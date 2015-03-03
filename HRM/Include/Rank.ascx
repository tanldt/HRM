<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Rank.ascx.cs" Inherits="iHRPCore.Include.Rank" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<TABLE id="Table1" style="WIDTH: 300px; HEIGHT: 328px" cellSpacing="1" cellPadding="1"
	width="300" border="1">
	<TR>
		<TD width="90%">
			<asp:ListBox id="ListBox1" runat="server" Width="100%" CssClass="combo" Height="324px"></asp:ListBox></TD>
		<TD vAlign="middle">
			<asp:linkbutton id="btnSelect" accessKey="N" runat="server" CssClass="btnSave" ToolTip="Alt+N">></asp:linkbutton><BR>
			<asp:linkbutton id="btnRemove" accessKey="N" runat="server" CssClass="btnSave" ToolTip="Alt+N"><</asp:linkbutton></TD>
	</TR>
</TABLE>
