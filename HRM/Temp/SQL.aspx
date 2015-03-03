<%@ Page language="c#" Codebehind="SQL.aspx.cs" AutoEventWireup="false" Inherits="iHRPCore.Temp.SQL" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SQL</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Include/myStyles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table width="100%">
				<tr>
					<td>
						<asp:Label id="Label1" runat="server" BackColor="White" BorderColor="White" ForeColor="White"></asp:Label></td>
				</tr>
				<tr>
					<td>
						<asp:TextBox id="txtSQL" runat="server" Width="100%" TextMode="MultiLine" Height="288px"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td align="left">
						<asp:Button id="btnExc" runat="server" Text="Execute"></asp:Button>
					</td>
				</tr>
				<TR>
					<TD align="left">
						<asp:Label id="lblResult" runat="server"></asp:Label></TD>
				</TR>
				<TR>
					<TD align="left">
						<div style="OVERFLOW: auto; WIDTH: 1328px; HEIGHT: 350px"><asp:DataGrid id="dgEnum" runat="server" Width="100%" ItemCssClass="gridItem" AltItemCssClass="gridAlter">
								<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
								<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
								<ItemStyle CssClass="gridItem"></ItemStyle>
								<HeaderStyle HorizontalAlign="Center" CssClass="TaskGrid_Head"></HeaderStyle>
							</asp:DataGrid></div>
					</TD>
				</TR>
				<tr>
					<td>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
