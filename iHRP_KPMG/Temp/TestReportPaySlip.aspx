<%@ Page language="c#" Codebehind="TestReportPaySlip.aspx.cs" AutoEventWireup="false" Inherits="iHRPCore.Temp.TestReportPaySlip" %>
<%@ Register TagPrefix="FPT" Namespace="FPTToolWeb.Control.DataGrids" Assembly="FPTToolWeb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WebForm2</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<style type="text/css">BODY { FONT-WEIGHT: normal; FONT-SIZE: 0.8em; WORD-SPACING: normal; TEXT-TRANSFORM: none; FONT-FAMILY: Verdana, Helvetica, sans-serif; LETTER-SPACING: normal; BACKGROUND-COLOR: #dddddd }
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
			<asp:button id="Button1" style="Z-INDEX: 100; LEFT: 8px; POSITION: absolute; TOP: 8px" runat="server"
				Text="Payslip"></asp:button>
			<asp:button id="cmdTestCV2" style="Z-INDEX: 111; LEFT: 56px; POSITION: absolute; TOP: 80px"
				runat="server" Text="Test 2"></asp:button>
			<asp:Button id="cmdXml" style="Z-INDEX: 110; LEFT: 320px; POSITION: absolute; TOP: 344px" runat="server"
				Text="XML"></asp:Button><asp:button id="cmdExportdtgPdf" style="Z-INDEX: 109; LEFT: 168px; POSITION: absolute; TOP: 344px"
				runat="server" Text="Datagrid PDF"></asp:button><asp:datagrid id="dtgList" style="Z-INDEX: 108; LEFT: 24px; POSITION: absolute; TOP: 392px" runat="server"
				AllowSorting="True" AutoGenerateColumns="False" CellPadding="0" BorderWidth="1px" BorderColor="#3366CC" BackColor="White" Width="100%">
				<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
				<ItemStyle CssClass="gridItem"></ItemStyle>
				<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
				<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
				<Columns>
					<asp:BoundColumn DataField="EmpName" HeaderText="NAME">
						<ItemStyle HorizontalAlign="Left"></ItemStyle>
					</asp:BoundColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
			</asp:datagrid><asp:button id="cmdPDF" style="Z-INDEX: 107; LEFT: 64px; POSITION: absolute; TOP: 344px" runat="server"
				Text="PDF"></asp:button><asp:button id="cmdExport" style="Z-INDEX: 106; LEFT: 24px; POSITION: absolute; TOP: 272px"
				runat="server" Text="Xuất dữ liệu"></asp:button><asp:textbox id="txtResult" style="Z-INDEX: 105; LEFT: 216px; POSITION: absolute; TOP: 72px"
				runat="server" Width="608px" Height="224px" TextMode="MultiLine"></asp:textbox><asp:button id="Button4" style="Z-INDEX: 104; LEFT: 16px; POSITION: absolute; TOP: 224px" runat="server"
				Text="Word"></asp:button><asp:button id="cmdCV" style="Z-INDEX: 103; LEFT: 8px; POSITION: absolute; TOP: 80px" runat="server"
				Text="CV"></asp:button><asp:button id="Button2" style="Z-INDEX: 101; LEFT: 24px; POSITION: absolute; TOP: 168px" runat="server"
				Text="Excel"></asp:button>&nbsp;
		</form>
	</body>
</HTML>
