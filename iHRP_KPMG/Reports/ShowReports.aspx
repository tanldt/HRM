<%@ Register TagPrefix="cr" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=10.0.3300.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>
<%@ Page language="c#" Codebehind="ShowReports.aspx.cs" AutoEventWireup="false" Inherits="iHRPCore.UserServices.Reports.ShowReports" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Reports</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<P>
				<CR:CrystalReportViewer id="CrystalReportViewer1" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px"
					runat="server" AutoDataBind="true" Width="350px" Height="50px" PrintMode="ActiveX"></CR:CrystalReportViewer></P>
			<P>&nbsp;</P>
		</form>
	</body>
</HTML>
