<%@ Control Language="c#" AutoEventWireup="false" Codebehind="AnniversaryBonus.ascx.cs" Inherits="MdlPR.AnniversaryBonus" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="ColumnList" Src="../Include/ColumnList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../Include/EmpHeaderSearch.ascx" %>
<TABLE cellSpacing="0" cellPadding="0" width="98%" border="0">
	<TR>
		<TD align="center">
			<asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label>
		</TD>
	</TR>
	<TR>
		<td align="center">
			<uc1:EmpHeaderSearch id="EmpHeaderSearch1" runat="server"></uc1:EmpHeaderSearch>
		</td>
	</TR>
	<TR>
		<TD align="center">
			<TABLE cellSpacing="0" cellPadding="1" width="99%" border="0">
				<TR>
					<TD width="10%">
						<P align="left"><asp:label id="lblMM" CssClass="label" runat="server">Month</asp:label></P>
					</TD>
					<TD width="10%"><asp:textbox id="txtMM" CssClass="input" runat="server" MaxLength="2" Width="50px" onblur="CheckMonth(this);"></asp:textbox></TD>
					<td width="10%">
						<asp:label id="lblYYYY" CssClass="labelrequire" runat="server">Year</asp:label>
					</td>
					<TD width="10%"><asp:textbox id="txtYYYY" CssClass="input" runat="server" MaxLength="4" Width="80px" onblur="CheckYear(this);"></asp:textbox></TD>
					<TD align="center">
						<asp:linkbutton id="btnSearch" CssClass="btnSearch" runat="server"> View</asp:linkbutton>&nbsp;&nbsp;
						<asp:linkbutton id="btnExport" accessKey="E" CssClass="btnExport" runat="server" ToolTip="Alt+E, Export data in grid to excel file"> Export</asp:linkbutton>
					</TD>
				</TR>
			</TABLE>
	<tr>
		<TD vAlign="top" noWrap align="center" height="20">
			<HR align="center" width="100%">
		</TD>
	</tr>
	<TR>
		<TD align="center">
			<TABLE cellSpacing="0" cellPadding="1" width="98%" border="0">
				<TR id="trColumnList">
					<TD align="left" colSpan="4">
						<uc1:ColumnList id="uctrlColumns" runat="server"></uc1:ColumnList>
					</TD>
				</TR>
				<TR id="trGrid">
					<TD colSpan="2">
						<asp:datagrid id="dtgList" CssClass="grid" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
							CellPadding="0" BorderWidth="1px" BorderColor="#3366CC" BackColor="White" HorizontalAlign="Center"
							AllowPaging="True" PageSize="15">
							<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="No.">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<%# Container.ItemIndex + 1 + dtgList.PageSize*dtgList.CurrentPageIndex%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="EmpCode" SortExpression="EmpCode" HeaderText="Emp Code">
									<HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="EmpName" SortExpression="EmpName" HeaderText="Full Name">
									<HeaderStyle Width="20%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Level2" SortExpression="Level2" HeaderText="Department">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="StartDateStr" SortExpression="StartDateStr" HeaderText="Joining Date">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Seniority" SortExpression="Seniority" HeaderText="Seniority">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Amount" SortExpression="Amount" HeaderText="Amount">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
					</TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>
<script type="text/javascript" language="javascript">
<!--
//kiem tra cac gia truoc khi Search
function checkvalidSearch()
{
	if (document.getElementById('_ctl0_txtYYYY').value == "")
	{
		GetAlertError(iTotal,DSAlert,"0003");
		document.getElementById('_ctl0_txtYYYY').focus();
		return false;
	}
	
	return true;				
}

-->
</script>
