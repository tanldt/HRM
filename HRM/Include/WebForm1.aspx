<%@ Page language="c#" Codebehind="WebForm1.aspx.cs" AutoEventWireup="false" Inherits="iHRPCore.Include.WebForm1" %>
<%@ Register TagPrefix="uc1" TagName="TopModule" Src="TopModule.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Top" Src="Top.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WebForm1</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="myStyles.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="common.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table id="tblLogin" runat="server" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td width="15%"></td>
					<td width="34%">
						<asp:textbox id="Textbox1" runat="server" CssClass="input" MaxLength="150" Width="391px" Visible="False">Temp\</asp:textbox></td>
					<td width="2%"></td>
					<td width="15%"></td>
					<td width="34%"><INPUT class="input" id="txtPassword" runat=server type="password"></td>
				</tr>
			</table>
			<TABLE id="tblMain" runat="server" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD style="WIDTH: 106px" vAlign="top" background="IMAGES/atopback.gif"></TD>
					<TD vAlign="top" background="IMAGES/atopback.gif" width="100%">
						<asp:Label id="lblstrSQL" runat="server" ForeColor="White"></asp:Label></TD>
					<TD vAlign="top" align="right" background="IMAGES/atopback.gif"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 106px" align="center" background="IMAGES\atopmenubg.jpg"></TD>
					<TD align="left" background="IMAGES\atopmenubg.jpg"><asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></TD>
					<TD align="right" width="60" background="IMAGES\atopmenubg.jpg"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 106px" align="center" background="IMAGES\atopmenubg.jpg"><asp:label id="lblCompany" runat="server" CssClass="label">URL</asp:label></TD>
					<TD align="left" background="IMAGES\atopmenubg.jpg"><asp:textbox id="txtURL" runat="server" CssClass="input" Width="391px" MaxLength="150">Reports\</asp:textbox>&nbsp;(
						<asp:label id="Label2" runat="server" CssClass="label"> ex: Reports\HR\HR08NewHire.xls</asp:label>)
						<asp:button id="butDowloadFile" runat="server" Text="Open file"></asp:button>&nbsp;
						<asp:button id="butUndo" runat="server" Text="Undo"></asp:button></TD>
					<TD align="right" width="60" background="IMAGES\atopmenubg.jpg"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 106px" align="center" background="IMAGES\atopmenubg.jpg"><asp:label id="Label1" runat="server" CssClass="label">Select file</asp:label></TD>
					<TD align="left" background="IMAGES\atopmenubg.jpg"><INPUT class="input" id="txtFile" style="WIDTH: 375px; HEIGHT: 22px" tabIndex="0" type="file"
							size="43" name="txtFile" runat="server"><asp:checkbox id="CheckBox2" runat="server" Text="Upload Backup"></asp:checkbox><asp:linkbutton id="btnUpload" runat="server" CssClass="btnaddnew">Upload</asp:linkbutton></TD>
					<TD align="right" width="60" background="IMAGES\atopmenubg.jpg"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 106px" align="center" background="IMAGES\atopmenubg.jpg"></TD>
					<TD align="left" width="532" background="IMAGES\atopmenubg.jpg"></TD>
					<td align="right" width="60" background="IMAGES\atopmenubg.jpg"></td>
				</TR>
				<TR>
					<TD style="WIDTH: 106px" align="center" background="IMAGES\atopmenubg.jpg"><asp:checkbox id="CheckBox1" runat="server" Text="Advance" AutoPostBack="True"></asp:checkbox></TD>
					<TD align="left" width="532" background="IMAGES\atopmenubg.jpg"><asp:textbox id="txtDesc" runat="server" CssClass="input" Width="391px" MaxLength="150" Visible="False">Temp\</asp:textbox></TD>
					<TD align="right" width="60" background="IMAGES\atopmenubg.jpg"></TD>
				</TR>
				<TR>
					<TD align="left" background="IMAGES\atopmenubg.jpg" colSpan="3">
						<asp:label id="Label3" runat="server" CssClass="label">SQL script</asp:label></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 106px" align="center" background="IMAGES\atopmenubg.jpg">&nbsp;
						<asp:button id="butSQL" runat="server" Text="Exec SQL"></asp:button></TD>
					<TD align="left" width="532" background="IMAGES\atopmenubg.jpg"><asp:textbox id="txtSQL" runat="server" Width="692px" TextMode="MultiLine" Height="192px"></asp:textbox></TD>
					<TD align="right" width="60" background="IMAGES\atopmenubg.jpg"></TD>
				</TR>
				<TR runat="server" id="trGrid">
					<td><asp:button id="btnExport" runat="server" Text="Export"></asp:button></td>
					<TD colspan="2" background="IMAGES\atopmenubg.jpg">
						<div style="OVERFLOW: auto; WIDTH: 1328px; HEIGHT: 350px"><asp:DataGrid id="dtgGrid" runat="server" AllowPaging="True"></asp:DataGrid></div>
					</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 106px" align="center" background="IMAGES\atopmenubg.jpg"></TD>
					<TD align="left" width="532" background="IMAGES\atopmenubg.jpg"></TD>
					<TD align="right" width="60" background="IMAGES\atopmenubg.jpg"></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
