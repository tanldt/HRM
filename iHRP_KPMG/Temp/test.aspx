<%@ Page language="c#" Codebehind="test.aspx.cs" AutoEventWireup="false" Inherits="iHRPCore.Temp.test" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Test</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table align="center">
				<tr>
					<td colSpan="2">
						<TABLE id="Table2" cellSpacing="1" cellPadding="1" border="0">
							<TR>
								<TD style="WIDTH: 105px" height="27"></TD>
								<TD height="27">
									<asp:Label id="tblErr" runat="server"></asp:Label></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 105px" height="27"><asp:label id="Label5" runat="server" Font-Size="8pt" Font-Names="Arial" Height="16px" ForeColor="Maroon"
										Width="137px">Mã</asp:label></TD>
								<TD colSpan="1" height="27" rowSpan="1"><asp:textbox id="txtID" runat="server" Font-Size="8pt" Font-Names="Arial" Height="22" Width="112px"
										CssClass="inputflat" MaxLength="10"></asp:textbox>&nbsp;
								</TD>
							</TR>
							<TR>
								<TD style="WIDTH: 105px" height="27"><asp:label id="Label2" runat="server" Font-Size="8pt" Font-Names="Arial" Height="16px" ForeColor="Maroon"
										Width="137px"> Tên VN</asp:label></TD>
								<TD height="27"><asp:textbox id="txtNameVN" runat="server" Font-Size="8pt" Font-Names="Arial" Height="22" Width="314px"
										CssClass="inputflat" MaxLength="50"></asp:textbox></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 105px" height="27"><asp:label id="Label1" runat="server" Font-Size="8pt" Font-Names="Arial" Height="16px" ForeColor="Maroon"
										Width="137px"> Tên EN</asp:label></TD>
								<TD height="27"><asp:textbox id="txtNameEN" runat="server" Font-Size="8pt" Font-Names="Arial" Height="22" Width="314px"
										CssClass="inputflat" MaxLength="50"></asp:textbox></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 105px" height="27"><asp:label id="Label3" runat="server" Font-Size="8pt" Font-Names="Arial" Height="16px" Width="137px"> Ghi chú</asp:label></TD>
								<TD height="27"><asp:textbox id="txtNote" runat="server" Font-Size="8pt" Font-Names="Arial" Height="22px" Width="316px"
										CssClass="inputflat" MaxLength="200">Ghi ch&#250;</asp:textbox></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
				<tr>
					<td align="center" colSpan="2"><asp:button id="cmdAddNew" runat="server" Font-Size="8pt" Font-Names="Arial" Height="25px" Width="75px"
							CssClass="ButtonCommand" Text="Thêm mới"></asp:button>&nbsp;&nbsp;&nbsp;
						<asp:button id="cmdSave" runat="server" Font-Size="8pt" Font-Names="Arial" Height="25px" Width="75px"
							CssClass="ButtonCommand" Text="Lưu"></asp:button>&nbsp;&nbsp;&nbsp;
						<asp:button id="cmdDelete" runat="server" Font-Size="8pt" Font-Names="Arial" Height="25px" Width="75px"
							CssClass="ButtonCommand" Text="Xóa"></asp:button></td>
				</tr>
				<tr>
					<td colSpan="2"><asp:datagrid id="dtgList" runat="server" Width="100%" BackColor="White" BorderColor="#3366CC"
							BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False" AllowSorting="True">
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
							<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
							<Columns>
								<asp:BoundColumn DataField="No" HeaderText="No">
									<ItemStyle HorizontalAlign="Center" Width="12%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="LSAllowanceCode" HeaderText="ID">
									<ItemStyle HorizontalAlign="Center" Width="12%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="NAME" HeaderText="NAME">
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="VNNAME" HeaderText="VNNAME">
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Note" HeaderText="Note">
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
