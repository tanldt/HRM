<%@ Page language="c#" Codebehind="WebForm2.aspx.cs" AutoEventWireup="false" Inherits="iHRPCore.Temp.WebForm2" %>
<%@ Register TagPrefix="FPT" Namespace="FPTToolWeb.Control.DataGrids" Assembly="FPTToolWeb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>WebForm2</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<style type="text/css">
    BODY { FONT-WEIGHT: normal; FONT-SIZE: 0.8em; WORD-SPACING: normal; TEXT-TRANSFORM: none; FONT-FAMILY: Verdana, Helvetica, sans-serif; LETTER-SPACING: normal; BACKGROUND-COLOR: #dddddd }
    .header { FONT-WEIGHT: bold; TEXT-ALIGN: left }
    H1 { COLOR: #003366 }
    H2 { COLOR: #003366 }
    H3 { COLOR: #003366 }
    H4 { COLOR: #003366 }
    H5 { COLOR: #003366 }
    TH { COLOR: #003366 }
    THEAD { COLOR: #003366 }
    TFOOT { COLOR: #003366 }
    H1 { FONT-WEIGHT: 700; FONT-SIZE: 2em; WORD-SPACING: normal; TEXT-TRANSFORM: none; FONT-STYLE: normal; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif; LETTER-SPACING: normal; TEXT-DECORATION: none }
    H2 { FONT-WEIGHT: 700; FONT-SIZE: 1.75em; WORD-SPACING: normal; TEXT-TRANSFORM: none; FONT-STYLE: normal; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif; LETTER-SPACING: normal; TEXT-DECORATION: none }
    H3 { FONT-WEIGHT: 500; FONT-SIZE: 1.58em; WORD-SPACING: normal; TEXT-TRANSFORM: none; FONT-STYLE: normal; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif; LETTER-SPACING: normal; TEXT-DECORATION: none }
    H4 { FONT-WEIGHT: 500; FONT-SIZE: 1.33em; WORD-SPACING: normal; TEXT-TRANSFORM: none; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif; LETTER-SPACING: normal; TEXT-DECORATION: none }
    H5 { FONT-WEIGHT: 700; FONT-SIZE: 1em; WORD-SPACING: normal; TEXT-TRANSFORM: none; FONT-STYLE: normal; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif; LETTER-SPACING: normal; TEXT-DECORATION: none }
    DT { FONT-WEIGHT: 700; FONT-SIZE: 1em; WORD-SPACING: normal; TEXT-TRANSFORM: none; FONT-STYLE: normal; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif; LETTER-SPACING: normal; TEXT-DECORATION: none }
    H6 { FONT-WEIGHT: 700; FONT-SIZE: 0.8em; WORD-SPACING: normal; TEXT-TRANSFORM: none; FONT-STYLE: normal; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif; LETTER-SPACING: normal; TEXT-DECORATION: none }
    TFOOT { FONT-SIZE: 1em; WORD-SPACING: normal; TEXT-TRANSFORM: none; FONT-FAMILY: Arial, Helvetica, sans-serif; LETTER-SPACING: normal }
    THEAD { FONT-SIZE: 1em; WORD-SPACING: normal; TEXT-TRANSFORM: none; FONT-FAMILY: Arial, Helvetica, sans-serif; LETTER-SPACING: normal }
    TH { FONT-WEIGHT: bold; FONT-SIZE: 1em; WORD-SPACING: normal; VERTICAL-ALIGN: baseline; TEXT-TRANSFORM: none; FONT-FAMILY: Arial, Helvetica, sans-serif; LETTER-SPACING: normal }
    A:link { COLOR: #3333cc; TEXT-DECORATION: none }
    A:visited { COLOR: #333399; TEXT-DECORATION: none }
    A:active { COLOR: #333399; TEXT-DECORATION: none }
    A:hover { COLOR: #3333cc; TEXT-DECORATION: underline }
    SMALL { FONT-SIZE: 0.7em }
    BIG { FONT-SIZE: 1.17em }
    BLOCKQUOTE { FONT-FAMILY: Courier New, monospace }
    PRE { FONT-FAMILY: Courier New, monospace }
    UL LI { LIST-STYLE-TYPE: square }
    UL LI LI { LIST-STYLE-TYPE: disc }
    UL LI LI LI { LIST-STYLE-TYPE: circle }
    OL LI { LIST-STYLE-TYPE: decimal }
    OL OL LI { LIST-STYLE-TYPE: lower-alpha }
    OL OL OL LI { LIST-STYLE-TYPE: lower-roman }
    IMG { MARGIN-TOP: 5px; MARGIN-LEFT: 10px; MARGIN-RIGHT: 10px }
    </style>
</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<FPT:FPTDataGrid id="dgSales" style="Z-INDEX: 101; LEFT: 216px; POSITION: absolute; TOP: 184px" runat="server"
				CellPadding="2" HeaderStyle-CssClass="header" AutoGenerateColumns="False" IsGroup="True">
				<Columns>
					<asp:BoundColumn headertext="EmpID" DataField="EmpID"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="EmpName" DataField="EmpName"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="DOBStr" DataField="DOBStr"></asp:BoundColumn>
				</Columns>
			</FPT:FPTDataGrid>
			<object id="pingboxagtvnj2j7uo0⁗" type="application/x-shockwave-flash" data="http://wgweb.msg.yahoo.com/badge/Pingbox.swf" width="240" height="420"><param name="movie" value="http://wgweb.msg.yahoo.com/badge/Pingbox.swf" /><param name="allowScriptAccess" value="always" /><param name="flashvars" value="wid=xG4L_sa3Rnbxkvl4MxIs15yk" /></object>
		</form>
	</body>
</HTML>
