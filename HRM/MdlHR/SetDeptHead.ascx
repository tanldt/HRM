<%@ Register TagPrefix="uc1" TagName="ColumnList" Src="../Include/ColumnList.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="SetDeptHead.ascx.cs" Inherits="MdlTR.SetDeptHead" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD>
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD width="20%">
						<asp:checkbox id="chkSelectAll" accessKey="G" onclick="CheckAll('_ctl0_chkSelectAll','_ctl0_dtgList',2,1,'chkSelect')"
							CssClass="checkbox" runat="server" Text="Select All" ToolTip="Alt+G"></asp:checkbox></TD>
					<TD>
						<uc1:ColumnList id="uctrlColumns" runat="server" Visible="False"></uc1:ColumnList></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD>
			<asp:datagrid id="dtgList" HorizontalAlign="Center" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False"
				AllowSorting="True" PageSize="15" BorderColor="#3366CC" BackColor="White" Width="100%" CssClass="grid"
				runat="server">
				<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
				<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
				<ItemStyle CssClass="gridItem"></ItemStyle>
				<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
				<Columns>
					<asp:BoundColumn Visible="False" DataField="LSLevel1ID" HeaderText="LSLevel1ID"></asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="LSLevel2ID" HeaderText="LSLevel2ID"></asp:BoundColumn>
					<asp:TemplateColumn HeaderText="Select">
						<HeaderStyle Width="5%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:CheckBox id="chkSelect" CssClass="checkbox" runat="server"></asp:CheckBox>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="NameShow" SortExpression="NameShow" HeaderText="Department">
						<HeaderStyle Width="30%"></HeaderStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn HeaderText="Next Manager">
						<HeaderStyle Width="20%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:DropDownList Runat="server" ID="cboEmpID" CssClass="combo"></asp:DropDownList>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
			</asp:datagrid>
			<HR width="100%" SIZE="1">
		</TD>
	</TR>
	<TR>
		<TD align="center">
			<asp:linkbutton id="btnSave" accessKey="N" CssClass="btnSave" runat="server" ToolTip="Alt+N">Save</asp:linkbutton>&nbsp;
			<asp:linkbutton id="btnExport" accessKey="N" CssClass="btnSave" runat="server" ToolTip="Alt+N">Export</asp:linkbutton></TD>
	</TR>
</TABLE>
<script language="javascript">
function checkSave()
{
	if(GridCheck('_ctl0_dtgList',2,1,'chkSelect')==false)
	{
		GetAlertError(iTotal,DSAlert,"0049");		
		return false;
	}
}
</script>
