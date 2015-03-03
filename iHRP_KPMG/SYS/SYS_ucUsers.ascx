<%@ Control Language="c#" AutoEventWireup="false" Codebehind="SYS_ucUsers.ascx.cs" Inherits="iHRPCore.SYS.SYS_ucUsers" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE width="100%">
	<TR vAlign="top" borderColor="#ffffff">
		<td align="left" width="70%">
			<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 280px"><asp:datagrid id="dgUserAccount" AllowSorting="True" CellPadding="0" AutoGenerateColumns="False"
					BorderColor="#3366CC" Width="90%" runat="server" CssClass="grid" BorderWidth="1px" BackColor="White">
					<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
					<ItemStyle CssClass="gridItem"></ItemStyle>
					<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
					<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
					<Columns>
						<asp:TemplateColumn HeaderText="Select">
							<HeaderStyle Width="5%"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<HeaderTemplate>
								<asp:CheckBox id="chkSelectAll" onclick="CheckAll('_ctl0_dgUserAccount__ctl1_chkSelectAll','_ctl0_dgUserAccount',2,1,'chkSelect')"
									runat="server" CssClass="gridFooter"></asp:CheckBox>
							</HeaderTemplate>
							<ItemTemplate>
								<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn Visible="False" DataField="UserGroupID" HeaderText="Account">
							<ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
						</asp:BoundColumn>
						<asp:TemplateColumn HeaderText="Account">
							<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" Width="100px"></HeaderStyle>
							<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Center" Width="100px"></ItemStyle>
							<ItemTemplate>
								<asp:LinkButton id=hpAccount runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UserGroupID") %>'>
								</asp:LinkButton>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn DataField="UserGroupName" HeaderText="Account name">
							<ItemStyle HorizontalAlign="Left" Width="40%"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn Visible="False" DataField="FValid" HeaderText="Run web">
							<ItemStyle Width="70px"></ItemStyle>
						</asp:BoundColumn>
						<asp:TemplateColumn HeaderText="Run web">
							<ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
							<ItemTemplate>
								<asp:CheckBox id="chkValid" Enabled="False" Runat="server"></asp:CheckBox>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn Visible="False" DataField="FAdm" HeaderText="Admin">
							<ItemStyle Width="70px"></ItemStyle>
						</asp:BoundColumn>
						<asp:TemplateColumn HeaderText="Admin">
							<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Center" Width="15%"></ItemStyle>
							<ItemTemplate>
								<asp:CheckBox id="chkAdm" Enabled="False" Runat="server"></asp:CheckBox>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
					<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
				</asp:datagrid></div>
		</td>
		<TD vAlign="middle" borderColor="#ffffff" align="center">
			<asp:linkbutton id="btnDeletePass" CssClass="btnSYS" runat="server">Delete password</asp:linkbutton>
			<br>
			<asp:linkbutton id="btnValid" CssClass="btnSYS" runat="server">Run web</asp:linkbutton>
			<br>
			<asp:linkbutton id="btnAdmin" CssClass="btnSYS" runat="server">Admin right</asp:linkbutton>
			<br>
			<asp:linkbutton id="btnAddNew" CssClass="btnSYS" runat="server">Add account</asp:linkbutton>
			<br>
			<asp:linkbutton id="btnDeleteUser" CssClass="btnSYS" runat="server">Delete account</asp:linkbutton>
			<br>
			<asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label>
		</TD>
	</TR>
	<tr vAlign="top" borderColor="#ffffff">
		<td colSpan="2"><asp:label id="Label1" Font-Names="Arial" Font-Size="8pt" Runat="server" Font-Bold="True" ForeColor="#AFAFB8">Nhóm người dùng</asp:label></td>
	</tr>
	<tr>
		<td colSpan="2">
			<table width="100%">
				<TR>
					<TD vAlign="top" align="left" width="45%" colSpan="1" rowSpan="1"><div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 280px"><asp:datagrid id="dgGroups" AllowSorting="True" CellPadding="0" AutoGenerateColumns="False" BorderColor="#3366CC"
								Width="100%" runat="server" CssClass="grid" BorderWidth="1px" BackColor="White" PageSize="20">
								<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
								<ItemStyle CssClass="gridItem"></ItemStyle>
								<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
								<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="UserGroupID" HeaderText="Group ID">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="UserGroupName" Visible="False" HeaderText="Group name">
										<ItemStyle HorizontalAlign="Left" Width="85%"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Group name">
										<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" Width="100px"></HeaderStyle>
										<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Left" Width="100px"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnkGroup" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UserGroupName") %>'>
											</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Select">
										<ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
										<ItemTemplate>
											<asp:CheckBox id="chkChose" Runat="server"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
					<TD vAlign="top" align="center" width="10%" colSpan="1" rowSpan="1">
						<P>
							<asp:linkbutton id="btnChoose" CssClass="btn" runat="server">>></asp:linkbutton></P>
						<P>
							<asp:linkbutton id="btnDelete" CssClass="btn" runat="server"><<</asp:linkbutton></P>
						<P></P>
					</TD>
					<TD vAlign="top" align="left" width="45%" colSpan="1" rowSpan="1"><div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 280px"><asp:datagrid id="dgGroupsSelected" AllowSorting="True" CellPadding="0" AutoGenerateColumns="False"
								BorderColor="#3366CC" Width="100%" runat="server" CssClass="grid" AllowPaging="True" BorderWidth="1px" BackColor="White">
								<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
								<ItemStyle CssClass="gridItem"></ItemStyle>
								<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
								<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="UserGroupID" HeaderText="Group ID">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="UserGroupName" HeaderText="Group Name">
										<ItemStyle HorizontalAlign="Left" Width="85%"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Select">
										<ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
										<ItemTemplate>
											<asp:CheckBox id="chkRemove" Runat="server"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
			</table>
		</td>
	</tr>
	<TR vAlign="top" borderColor="#ffffff">
		<TD align="center" colSpan="2">
			<asp:linkbutton id="btnSaveUserGroup" CssClass="btnSYS" runat="server">Save Groups - Users</asp:linkbutton></TD>
	</TR>
	<TR vAlign="top" borderColor="#ffffff">
		<TD colSpan="2">
			<HR align="center" color="#6699cc" SIZE="1">
			&nbsp;</TD>
	</TR>
</TABLE>
<script language="javascript">
	function CheckGrid(field)
	{
		var id = field.id;
		if (id == "_ctl0_btnChoose")
		{
			if (GridCheck('_ctl0_dgGroups', 2, 1, 'chkChose') == false)
			{
				//alert("Must check at least one group for adding!");	
				GetAlertError(iTotal,DSAlert,"ucU0004");
				return false;
			}
		}
		else if (id == "_ctl0_btnDelete")
		{
			if (GridCheck('_ctl0_dgGroupsSelected', 3, 1, 'chkRemove') == false)
			{
				//alert("Must check at least one group for removing!");
				GetAlertError(iTotal,DSAlert,"ucU0005");
				return false;
			}
		}
		else if (id == "_ctl0_btnSaveUserGroup")
		{
			if (GridCheck('_ctl0_dgUserAccount', 2, 1, 'chkSelect') == false)
			{
				//alert("Must check at least one account to manipulate!");	
				GetAlertError(iTotal,DSAlert,"ucU0001");
				return false;
			}
			/*if(document.getElementById("_ctl0_dgGroupsSelected").rows.length < 3)
			{
				alert("Must choose groups for adding user account!");	
				return false;
			}*/
		}
		else
		{
			if (GridCheck('_ctl0_dgUserAccount', 2, 1, 'chkSelect') == false)
			{
				//alert("Must check at least one account to manipulate!");
				GetAlertError(iTotal,DSAlert,"ucU0001");
				return false;
			}
		}
		if (id == "_ctl0_btnDeleteUser")
		{
			return Confirm_Delete();
		}
	}
	
	function OpenDetails(field)
	{
		var strUrl = "SYS/SYS_ucViewPermission.ascx&TitleE=USER-GROUP DETAILIS&TitleV=USER-GROUP DETAILS&IsUser=1&UserGroupID=" + field;
		ShowDialog('FormPage.aspx?Ascx=' + strUrl, screen.width -150, screen.height-100);
		return false;
	}
	
	function OpenDetails_G(field)
	{
		var strUrl = "SYS/SYS_ucViewPermission.ascx&TitleE=USER-GROUP DETAILIS&TitleV=USER-GROUP DETAILS&IsUser=1&UserGroupID=" + field;
		ShowDialog('FormPage.aspx?Ascx=' + strUrl, screen.width -150, screen.height-100);
		return false;
	}
	
</script>
