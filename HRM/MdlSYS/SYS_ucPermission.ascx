<%@ Control Language="c#" AutoEventWireup="false" Codebehind="SYS_ucPermission.ascx.cs" Inherits="iHRPCore.SYS_ucPermission" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<TD width="30%">
		<TD width="20%">
		<TD></TD>
	</tr>
	<TR>
		<TD colSpan="3">
			<table width="100%">
				<tr>
					<TD width="83" style="WIDTH: 83px"><asp:label id="Label1" CssClass="label" Width="88px" runat="server">Module</asp:label></TD>
					<TD width="272" style="WIDTH: 272px"><asp:dropdownlist id="dlModule" CssClass="combo" Width="288px" runat="server" AutoPostBack="True"
							Height="24px"></asp:dropdownlist></TD>
					<td>&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnSave" accessKey="N" CssClass="btnSearch" runat="server" ToolTip="Alt+N"> Save</asp:linkbutton>&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnAddnew" accessKey="N" CssClass="btnSearch" runat="server" ToolTip="Alt+N">Cancel</asp:linkbutton></td>
				</tr>
			</table>
		</TD>
	</TR>
	<tr>
		<td><asp:radiobutton id="optGroup" onclick="ChangeUserGroupType()" CssClass="option" Width="30%" runat="server"
				Checked="True" Text="Group" GroupName="UserGroupType"></asp:radiobutton><asp:radiobutton id="optUser" onclick="ChangeUserGroupType()" CssClass="option" runat="server" Text="User"
				GroupName="UserGroupType"></asp:radiobutton></td>
		<td></td>
	</tr>
	<TR vAlign="top">
		<TD vAlign="top">
			<table width="100%">
				<tr id="trListGroup" vAlign="top" runat="server">
					<td vAlign="top"><asp:listbox id="lstListGroup" CssClass="combo" Width="232px" runat="server" AutoPostBack="True"
							Font-Names="Arial" Font-Size="8pt" Height="145px"></asp:listbox></td>
				</tr>
				<tr id="trListUser" vAlign="top" runat="server">
					<td vAlign="top"><asp:listbox id="lsListUser" CssClass="combo" Width="232px" runat="server" AutoPostBack="True"
							Font-Names="Arial" Font-Size="8pt" Height="145px"></asp:listbox></td>
				</tr>
			</table>
		</TD>
		<td vAlign="top" colSpan="2">
			<table width="100%">
				<TR>
					<TD vAlign="top" align="right">
						<asp:CheckBox id="chkSelectAllView" onclick="CheckAll('_ctl0_chkSelectAllView','_ctl0_dgFunction',2,1,'chkView')"
							runat="server" CssClass="checkbox" Text="Select all" TextAlign="Left"></asp:CheckBox>&nbsp;&nbsp;
						<asp:CheckBox id="chkSelectAllEdit" onclick="CheckAll('_ctl0_chkSelectAllEdit','_ctl0_dgFunction',2,1,'chkEdit')"
							runat="server"></asp:CheckBox></TD>
				</TR>
				<tr>
					<td vAlign="top"><asp:datagrid id="dgFunction" CssClass="grid" Width="100%" runat="server" PageSize="7" AllowSorting="True"
							AutoGenerateColumns="False" CellPadding="0" BorderWidth="1px" BorderColor="#3366CC" BackColor="White">
							<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="FunctionID" HeaderText="FunctionID"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="FAdd" HeaderText="FEdit"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="FRun" HeaderText="FRun"></asp:BoundColumn>
								<asp:BoundColumn DataField="FunctionName" HeaderText="Function"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="View">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox id="chkView" onclick="Javascript:EditChange(this);" runat="server" CssClass="checkbox"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Edit">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox id="chkEdit" onclick="Javascript:EditChange(this);" runat="server" CssClass="checkbox"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
			</table>
		</td>
	</TR>
	<tr style="DISPLAY:none">
		<td colSpan="3"><asp:LinkButton Runat="server" ID="btnSearchData"></asp:LinkButton></td>
	</tr>
</TABLE>
<script>
	function EditChange(field)
	{
		var id = field.id;
		id = id.replace('_ctl0_dgFunction__ctl','');
		id = id.substring(id.lastIndexOf("_") + 1, id.length - id.lastIndexOf("_") + 2);
		var index;
		index = field.id.replace('_ctl0_dgFunction__ctl','');
		index=index.replace(("_" + id),'');
		if (document.getElementById('_ctl0_dgFunction__ctl' + index + '_chkEdit').checked)
		{
			document.getElementById('_ctl0_dgFunction__ctl' + index + '_chkView').checked=true;
		}
	}
	
	function ChangeUserGroupType()
	{
		if (document.getElementById("_ctl0_optGroup").checked == true)
		{
			document.getElementById("_ctl0_trListGroup").style.display = "";
			document.getElementById("_ctl0_trListUser").style.display = "none";
		}
		else if (document.getElementById("_ctl0_optUser").checked == true)
		{
			document.getElementById("_ctl0_trListGroup").style.display = "none";
			document.getElementById("_ctl0_trListUser").style.display = "";
		}
		//document.getElementById("_ctl0_btnSearchData").click();
		return false;
	}
	ChangeUserGroupType();
	
	function checkAddnew()
	{
		if(confirm(GetAlertText(iTotal,DSAlert,"0090"))==false){
			return true
		}
		else
		{			
			document.getElementById('_ctl0_btnSave').click();
			return false;
		}
	}
</script>
