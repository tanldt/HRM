<%@ Page language="c#" Codebehind="TMS_rptChamCongThang1.aspx.cs" AutoEventWireup="false" Inherits="Reports.TMS.TMS_rptChamCongThang1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>TMS_rptChamCongThang1</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE id='PrntGrid' width="768" style="WIDTH: 768px; HEIGHT: 160px">
				<TR>
					<TD style="HEIGHT: 20px" align="center" colSpan="5"></TD>
				</TR>
				<TR>
					<TD align="center" colSpan="5">
						<asp:label id="lblErr" runat="server" CssClass="lblErr" Font-Bold="True">BẢNG CHẤM CÔNG THÁNG</asp:label></TD>
				</TR>
				<TR>
					<TD align="center" colSpan="5">
						<asp:Label id="Label3" runat="server"></asp:Label></TD>
				</TR>
				<TR>
					<TD align="center" colSpan="5"></TD>
				</TR>
				<TR>
					<TD colSpan="5">
						<asp:datagrid id="dtgList" runat="server" Width="760px" CellPadding="0" BorderWidth="1px" BorderColor="#3366CC"
							BackColor="White">
							<AlternatingItemStyle Font-Size="8pt" Font-Names="Arial" BackColor="#F6F6F7"></AlternatingItemStyle>
							<ItemStyle Font-Size="8pt" Font-Names="arial"></ItemStyle>
							<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" ForeColor="White"
								BackColor="LightGray"></HeaderStyle>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD colSpan="5">
						<P align="center">
							<asp:Label id="Label1" runat="server" Width="96px" Font-Bold="True">Trưởng đơn vị</asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<asp:Label id="Label2" runat="server" Font-Bold="True">Người chấm công</asp:Label></P>
					</TD>
				</TR>
			</TABLE>
		</form>
		<form id="FrmExcel" method="post">
			<INPUT id="txtDocument" type="hidden" name="txtDocument">
		</form>
		<script language="javascript">
function ExportToExcell(Frm, PrntGrid, Filename){
	var FrmExcel = document.all(Frm);
	var Grid = document.all(PrntGrid);
		FrmExcel.txtDocument.value = Grid.innerHTML;
 		FrmExcel.action = "../../DownloadToExcel.asp?fn="+Filename;
 		FrmExcel.target='_blank';
		FrmExcel.submit(); 
}
		</script>
		<script language="javascript">
self.close();
ExportToExcell('FrmExcel', 'PrntGrid', 'ChamCong')
		</script>
	</body>
</HTML>
