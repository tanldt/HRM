<%@ Control Language="c#" AutoEventWireup="false" Codebehind="LockPayroll.ascx.cs" Inherits="MdlPR.LockPayroll1" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="Table2" style="WIDTH: 100%" cellSpacing="1" cellPadding="0" border="0">
	<TR>
		<TD><asp:label id="Label5" runat="server" CssClass="labelRequire"> Month</asp:label></TD>
		<TD><asp:textbox id="txtMonth" onblur="javascript:CheckMonthYear(this)" runat="server" CssClass="input"
				MaxLength="7"></asp:textbox></TD>
	</TR>
	<TR>
		<TD align="right"></TD>
		<TD></TD>
	</TR>
	<tr>
		<td align="center" colspan="2">
			<asp:Button id="cmdSearch" runat="server" Text="Search" CssClass="btnExport"></asp:Button>&nbsp;&nbsp; 
			&nbsp;&nbsp;
			<asp:Button id="cmdSave" runat="server" Text="Clock" CssClass="btnExport"></asp:Button>
		</td>
	</tr>
	<TR>
		<TD align="right"></TD>
		<TD></TD>
	</TR>
	<tr>
		<td colspan="2">
			<asp:datagrid id="dtgList" runat="server" CssClass="grid" Width="100%" BorderColor="#3366CC" PageSize="20"
				AllowSorting="True" AutoGenerateColumns="False" CellPadding="0" BorderWidth="1px" BackColor="White">
				<FooterStyle CssClass="gridFooter"></FooterStyle>
				<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
				<ItemStyle CssClass="gridItem"></ItemStyle>
				<HeaderStyle CssClass="gridHeader"></HeaderStyle>
				<Columns>
					<asp:BoundColumn Visible="False" DataField="LSLevel1ID" HeaderText="LSLevel1ID"></asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="Month" HeaderText="Month"></asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="IsLock" HeaderText="IsLock"></asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="LSCompanyID" HeaderText="LSCompanyID"></asp:BoundColumn>
					<asp:TemplateColumn HeaderText="Select">
						<HeaderStyle Width="8%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<HeaderTemplate>
							<asp:CheckBox id="chkSelectAll" runat="server" CssClass="checkbox" onclick="CheckAll('_ctl0_dtgList__ctl1_chkSelectAll','_ctl0_dtgList',2,1,'chkSelect')"></asp:CheckBox>
						</HeaderTemplate>
						<ItemTemplate>
							<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="&lt;b&gt;Seq&lt;/b&gt;">
						<HeaderStyle Width="5%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<%# Container.ItemIndex + 1 + dtgList.PageSize*dtgList.CurrentPageIndex%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="Code" HeaderText="Company Code"></asp:BoundColumn>
					<asp:BoundColumn DataField="Name" HeaderText="Company Name"></asp:BoundColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
			</asp:datagrid>
		</td>
	</tr>
</TABLE>
<SCRIPT language="javascript">

function checkvalidSearch()
{
	
	if (checkisnull("txtMonth")==false) return false;
	
	return true;		
}
function Lock()
{
	if (checkisnull("txtMonth")==false) return false;
	if(confirm(GetAlertText(iTotal,DSAlert,"PC_0002"))==false){
		return false;
	}
	return true;
}
</SCRIPT>
