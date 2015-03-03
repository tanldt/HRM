<%@ Control Language="c#" AutoEventWireup="false" Codebehind="EmpSearchResult.ascx.cs" Inherits="iHRPCore.MdlHR.EmpSearchResult" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="98%" border="0">
	<TR>
		<TD align="center">
			<asp:DataGrid id="dtgList" runat="server" CssClass="grid" CellPadding="0" BorderWidth="1px"
				BorderColor="#3366CC" BackColor="White" Width="100%">
<AlternatingItemStyle CssClass="gridAlter">
</AlternatingItemStyle>

<ItemStyle CssClass="gridItem">
</ItemStyle>

<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader">
</HeaderStyle>

<FooterStyle HorizontalAlign="Center" CssClass="gridFooter">
</FooterStyle>

<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages">
</PagerStyle>
			</asp:DataGrid>
		</TD>
	</TR>
  <TR>
    <TD align=center height=10></TD></TR>
	<tr>
		<td align="center"><asp:linkbutton id="btnExport" accessKey="L" runat="server" CssClass="btnExport">Export</asp:linkbutton></td>
	</tr>
</TABLE>
