<%@ Register TagPrefix="uc1" TagName="ColumnList" Src="../Include/ColumnList.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="SYS_ucGroup.ascx.cs" Inherits="MdlSYS.SYS_ucGroup" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table width="100%">
	<tr>
		<td align="center">
			<TABLE width="100%">
				<TR>
					<TD align="center" colSpan="2"><asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></TD>
				</TR>
				<tr>
					<td align="center">
						<TABLE width="70%">
							<TR>
								<TD width="30%"><asp:label id="lblGroupIDCaption" CssClass="labelRequire" Runat="server">Group ID</asp:label></TD>
								<TD><asp:textbox id="txtGroupID" CssClass="input" Runat="server" MaxLength="10" Width="70%"></asp:textbox></TD>
							</TR>
							<TR>
								<TD><asp:label id="lblGroupNameCaption" CssClass="labelRequire" Runat="server"> Group Name</asp:label></TD>
								<TD><asp:textbox id="txtGroupName" CssClass="input" Runat="server" MaxLength="50" Width="70%"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>
									<asp:label id="lblDesc" CssClass="label" Runat="server">Description</asp:label></TD>
								<TD>
									<asp:textbox id="txtDescription" CssClass="input" Runat="server" Width="90%" MaxLength="255"
										TextMode="MultiLine" Height="56px"></asp:textbox></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
				<TR>
					<TD align="center">
						<HR align="center" width="100%">
						&nbsp;</TD>
				</TR>
				<TR>
					<TD align="center">
						<table width="100%">
							<TBODY>
								<TR>
									<td width="15%"><asp:CheckBox id="chkSelectAll" Text="Select all" onclick="CheckAll('_ctl0_chkSelectAll','_ctl0_dgGroups',3,1,'chkSelect')"
											runat="server" CssClass="checkbox"></asp:CheckBox></td>
									<TD align="center" colSpan="2"><asp:linkbutton id="btnAddNew" accessKey="N" runat="server" CssClass="btnAddnew" ToolTip="Alt+N">New Record</asp:linkbutton>&nbsp;&nbsp;&nbsp;&nbsp;
										<asp:linkbutton id="btnSave" accessKey="R" runat="server" CssClass="btnSave" ToolTip="ALT+R">Save</asp:linkbutton>&nbsp;&nbsp;&nbsp;&nbsp;
										<asp:linkbutton id="btnDelete" accessKey="D" runat="server" CssClass="btnDelete" ToolTip="Alt + D">Delete</asp:linkbutton></TD>
								</TR>
								<TR>
									<TD align="left" colSpan="3">
										<uc1:ColumnList id="uctrlColumns" runat="server"></uc1:ColumnList></TD>
					</TD>
				</TR>
				<TR style="DISPLAY: none">
					<TD align="center" colSpan="3"><asp:linkbutton id="btnSearch" accessKey="F" runat="server" CssClass="btnSearch" ToolTip="F">Search</asp:linkbutton></TD>
				</TR>
			</TABLE>
		</td>
	</tr>
	<TR vAlign="top">
		<td style="HEIGHT: 310px" vAlign="top" borderColor="#ffffff" colSpan="3">
			<asp:datagrid id="dgGroups" runat="server" CssClass="grid" Width="100%" AllowPaging="True" AllowSorting="True"
				AutoGenerateColumns="False" CellPadding="0" BorderWidth="1px" BorderColor="#3366CC" BackColor="White">
				<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
				<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
				<ItemStyle CssClass="gridItem"></ItemStyle>
				<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
				<Columns>
					<asp:BoundColumn Visible="False" DataField="UserGroupID" HeaderText="GroupID">
						<ItemStyle HorizontalAlign="Left"></ItemStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn HeaderText="Select">
						<HeaderStyle Width="5%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="UserGroupID" HeaderText="ID">
						<ItemStyle Width="15%"></ItemStyle>
						<ItemTemplate>
							<asp:LinkButton id=hpLink CommandName=hpLink Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "UserGroupID") %>'>
							</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="UserGroupName" SortExpression="UserGroupName" HeaderText="Name">
						<ItemStyle HorizontalAlign="Left" Width="40%"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="Description" HeaderText="Description"></asp:BoundColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
			</asp:datagrid></td>
	</TR>
	<tr>
		<td align="center" colSpan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
		</td>
	</tr>
	<TR>
		<TD align="center" colSpan="3" height="10"></TD>
	</TR>
	<TR id="trAddNew" vAlign="top" runat="server">
		<TD vAlign="top" borderColor="#ffffff" align="center" colSpan="3"></TD>
	</TR>
</table>
<script language="javascript">
	function OpenWindowEmp()
	{
		ShowDialog('FormPage.aspx?Ascx=MdlHR/EmpPopup.ascx&Params=3');
	}
	
	function ReturnEmpPopUp3(strEmpID, strEmpCode, strEmpName)
	{
		document.getElementById("_ctl0_txtEmpID").value = strEmpID;
		document.getElementById("_ctl0_txtUserName").value = strEmpName;
		document.getElementById("_ctl0_txtUserID").value = strEmpID;
	}
	
	function CheckDelete()
	{
		if (GridCheck('_ctl0_dgGroups', 3, 1, 'chkSelect') == false)
		{
				GetAlertError(iTotal,DSAlert,"0001");		
				return false;
		}
		if(confirm(GetAlertText(iTotal,DSAlert,"0002"))==false){
		return false;
	}
	}
	
	function CheckSave()
	{
		var value;
		value = document.getElementById("_ctl0_txtGroupID").value;
		if (value == "")
		{
			//alert("Input group ID!");
			GetAlertError(iTotal,DSAlert,"ucG0001");
			document.getElementById("_ctl0_txtGroupID").focus();
			return false;
		}
		value = document.getElementById("_ctl0_txtGroupName").value;
		if (value == "")
		{
			//alert("Input group name!");
			GetAlertError(iTotal,DSAlert,"ucG0002");
			document.getElementById("_ctl0_txtGroupName").focus();
			return false;
		}
		return true;
	}
	
	function OpenDetails(field)
	{
		var strUrl = "SYS/SYS_ucViewPermission.ascx&TitleE=USER-GROUP DETAILIS&TitleV=USER-GROUP DETAILS&IsGroup=1&UserGroupID=" + field;
		ShowDialog('FormPage.aspx?Ascx=' + strUrl, screen.width -150, screen.height-100);
		return false;
	}
</script>
</TD></TR></TBODY></TABLE>
<script><asp:Literal id="ltlAlert" runat="server" EnableViewState="False"></asp:Literal></script>
