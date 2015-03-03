<%@ Control Language="c#" AutoEventWireup="false" Codebehind="SYS_ucUserAccountList.ascx.cs" Inherits="iHRPCore.SYS.SYS_ucUserAccountList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table width="100%">
	<TR>
		<TD align="center" height="35"><asp:label id="lblTitle" runat="server" Font-Size="12pt" Width="480px" Font-Names="Arial" ForeColor="Black"
				Font-Bold="True">LIST OF USER ACCOUNT</asp:label></TD>
	</TR>
	<tr bgColor="#cccc99">
		<td style="HEIGHT: 5px" bgColor="#ffffff" colSpan="1" rowSpan="1" align="center">
			<asp:label id="Label2" runat="server" Font-Names="Arial" Width="81" Font-Size="8pt" Height="18px"> Account</asp:label>
			<asp:TextBox id="txtUserID" runat="server" Font-Names="Arial" Width="152px" Font-Size="8pt" Height="22px"
				MaxLength="20"></asp:TextBox>
			<asp:linkbutton id="btnSearch" accessKey="S" runat="server" CssClass="btnSearch" ToolTip="Alt+S">Search</asp:linkbutton></td>
	</tr>
	<TR>
		<TD style="HEIGHT: 10px" align="center" bgColor="#ffffff"></TD>
	</TR>
	<tr>
		<TD align="center">
			<asp:datagrid id="dgUserAccountList" runat="server" CssClass="grid" Width="550" AllowPaging="True"
				AllowSorting="True" AutoGenerateColumns="False" CellPadding="0" BorderWidth="1px" BorderColor="#3366CC"
				BackColor="White">
				<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
				<ItemStyle CssClass="gridItem"></ItemStyle>
				<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
				<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
				<Columns>
					<asp:TemplateColumn HeaderText="Select">
						<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" Width="30px"></HeaderStyle>
						<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Center" Width="30px"></ItemStyle>
						<ItemTemplate>
							<asp:LinkButton id="hpLink" CommandName="hpLink" Width="50" Text="+" Runat="server" CssClass="btn"></asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="UserGroupID" HeaderText="Account">
						<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
						<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Left"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="UserGroupName" HeaderText="Full name">
						<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
						<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Left"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="FGroup" HeaderText="Is Group" Visible=False>
						<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
						<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Left"></ItemStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn Visible="False" HeaderText="Delete">
						<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
						<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:CheckBox id="ckSelect" runat="server"></asp:CheckBox>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
			</asp:datagrid></TD>
	</tr>
</table>
<script language="javascript">
	function ReturnEmpPopUp(strUserID, strUserName, strFGroup)
	{
		opener.ReturnEmpPopUp(strUserID, strUserName, strFGroup);
		window.close();
	} 
</script>
<script><asp:Literal id="ltlAlert" runat="server" EnableViewState="False"></asp:Literal></script>
