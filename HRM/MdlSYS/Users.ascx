<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Users.ascx.cs" Inherits="iHRPCore.MdlSYS.Users" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../Include/EmpHeaderSearch.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AccountHeaderSearch" Src="../Include/AccountHeaderSearch.ascx" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="98%" border="0">
	<TR>
		<TD align="center"><asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center">
			<uc1:EmpHeaderSearch id="EmpHeaderSearch1" runat="server"></uc1:EmpHeaderSearch></TD>
	</TR>
	<TR>
		<TD align="center">
			<table width="100%">
				<TR>
					<TD width="11%"><asp:label id="Label7" CssClass="label" runat="server">Account</asp:label></TD>
					<TD width="26%"><asp:textbox id="txtEmpID" CssClass="input" runat="server" Width="77%"></asp:textbox><%if(Request.Params["EmpID"] == null && Request.Params["EmpName"] == null){%><INPUT class="search" type="button" onclick="javascript:OpenWindowEmp()" value="..."><%}%></TD>
					<TD width="10%"><asp:label id="Label2" CssClass="label" runat="server">General</asp:label></TD>
					<TD width="30%"><%if(Request.Params["EmpID"] == null && Request.Params["EmpName"] == null){%><%}%>
						<asp:CheckBox id="chkActivate" CssClass="checkbox" runat="server" Text="Activate"></asp:CheckBox>
						<asp:CheckBox id="Checkbox1" CssClass="checkbox" runat="server" Text="Admin"></asp:CheckBox></TD>
					<TD width="9%"></TD>
					<TD width="14%">
						<asp:linkbutton id="Linkbutton1" CssClass="btnSearch" runat="server">Search</asp:linkbutton></TD>
				</TR>
			</table>
		</TD>
	</TR>
	<TR>
		<TD vAlign="top" noWrap align="center" height="20">
			<HR align="center" width="100%">
		</TD>
	</TR>
	<!-- end button for input form -->
	<TR>
		<TD align="center">
			<table width="100%">
				<TR>
					<TD vAlign="top" align="center">
						<div style="OVERFLOW: auto; HEIGHT: 150px"><asp:datagrid id="dtgAccount" runat="server" CssClass="grid" Width="90%" AllowPaging="True" AllowSorting="True"
								AutoGenerateColumns="False" CellPadding="0" BorderWidth="1px" BorderColor="#3366CC" BackColor="White" PageSize="5">
								<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
								<ItemStyle CssClass="gridItem"></ItemStyle>
								<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
								<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="No">
										<HeaderStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Middle"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Account">
										<HeaderStyle Width="20%"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id=lnkLinkAccount Width="95%" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Content") %>'>
											</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn HeaderText="Full Name">
										<HeaderStyle Width="35%"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Activate">
										<HeaderStyle Width="15%"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:CheckBox id="chkRun" runat="server" CssClass="checkbox"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="System right">
										<HeaderStyle Width="15%"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:CheckBox id="chkAdmin" runat="server" CssClass="checkbox"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Select">
										<HeaderStyle Width="14%"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
										</ItemTemplate>
										<FooterTemplate>
											<asp:CheckBox id="chkSelectAll" runat="server" CssClass="checkbox"></asp:CheckBox>
										</FooterTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD align="center" height="10"></TD>
				</TR>
				<tr>
					<td align="center">
						<asp:linkbutton id="btnAddNew" CssClass="btnAddNew" runat="server">AddNew</asp:linkbutton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnSave" runat="server" CssClass="btnSave"> Save</asp:linkbutton>&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="Linkbutton4" CssClass="btn" runat="server">Reset pass</asp:linkbutton></td>
				</tr>
				<TR>
					<TD align="center" colSpan="3">
						<HR align="center" width="100%">
					</TD>
				</TR>
			</table>
		</TD>
	</TR>
	<tr>
		<td>
			<asp:CheckBox id="Checkbox2" CssClass="checkbox" runat="server" onclick="collapse('_ctl0_trGroups',null)"
				Text="Groups"></asp:CheckBox>
		</td>
	</tr>
	<TR id="trGroups" runat="server" style="DISPLAY:none">
		<TD align="center">
			<table width="100%">
				<TR>
					<TD vAlign="top" align="center"><div style="OVERFLOW: auto; HEIGHT: 150px"><asp:datagrid id="dtgGroup" runat="server" CssClass="grid" Width="90%" AllowPaging="True" AllowSorting="True"
								AutoGenerateColumns="False" CellPadding="0" BorderWidth="1px" BorderColor="#3366CC" BackColor="White" PageSize="5">
								<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
								<ItemStyle CssClass="gridItem"></ItemStyle>
								<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
								<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="No">
										<HeaderStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Middle"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="GroupID">
										<HeaderStyle Width="30%"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id=lnkLinkGroup Width="95%" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Content") %>'>
											</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn HeaderText="Group Name">
										<HeaderStyle Width="40%"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Activate">
										<HeaderStyle Width="15%"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:CheckBox id="Checkbox5" CssClass="checkbox" runat="server" Enabled="False"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Select">
										<HeaderStyle Width="14%"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:CheckBox id="Checkbox3" runat="server" CssClass="checkbox"></asp:CheckBox>
										</ItemTemplate>
										<FooterTemplate>
											<asp:CheckBox id="Checkbox4" runat="server" CssClass="checkbox"></asp:CheckBox>
										</FooterTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD align="center" height="10">
					</TD>
				</TR>
				<tr>
					<td align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="Linkbutton11" runat="server" CssClass="btn" ToolTip="Save groups">Save</asp:linkbutton></td>
				</tr>
			</table>
		</TD>
	</TR>
	<TR>
		<TD align="left"><!-- start grid for input form -->
			<TABLE id="tblGrid" cellSpacing="0" cellPadding="1" width="98%" border="0">
				<TR id="trGrid" align="center"> <!-- start grid detail for input form -->
					<td colSpan="10"></td>
					<TD></TD>
				</TR>
			</TABLE> <!-- end grid for input form --></TD>
	</TR>
</TABLE>
<script language="javascript">
	function OpenWindow()
	{
		CurWindow = window.open('FormPage.aspx?Ascx=MdlSYS/AddNewAccount.ascx','AddNewAccount','toolbar=0,status=no,menubar=no,scrollbars=1,resizable=yes,copyhistory=yes,width=800,height=200,align=top');
		CurWindow.focus();
		return false;
	} 
	function OpenPermissionDetails()
	{
		CurWindow = window.open('FormPage.aspx?Ascx=MdlSYS/PermissionDetails.ascx','PermissionDetails','toolbar=0,status=no,menubar=no,scrollbars=1,resizable=yes,copyhistory=yes,width=800,height=500,align=top');
		CurWindow.focus();
		return false;
	} 
</script>
