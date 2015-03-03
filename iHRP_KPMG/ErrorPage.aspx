<%@ Page language="c#" Codebehind="ErrorPage.aspx.cs" AutoEventWireup="false" Inherits="iHRPCore.ErrorPage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>ErrorPage</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</head>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table align="center">
				<tr>
					<td height="200" align="center">
						<asp:label id="lblError" runat="server" ForeColor="Red" Font-Size="12pt" Font-Names="Arial"
							Font-Bold="True">
							You don't have permission for manipulating chose function.
							<br>We are very sorry for the inconvenience caused to you...</asp:label>
					</td>
				</tr>
				<tr>
					<td align="center">
						<asp:Button id="cmdTryAgain" runat="server" Font-Bold="True" Text="Try again!"></asp:Button>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
