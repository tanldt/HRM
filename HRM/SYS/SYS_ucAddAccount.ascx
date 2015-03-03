<%@ Control Language="c#" AutoEventWireup="false" Codebehind="SYS_ucAddAccount.ascx.cs" Inherits="iHRPCore.SYS.SYS_ucAddAccount" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="ColumnList" Src="../Include/ColumnList.ascx" %>
<table width="100%">
	<tr>
		<td align="center">
			<TABLE width="60%">
				<TR>
					<TD align="center" colSpan="2"><asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label></TD>
				</TR>
				<TR id="trMBVEmp">
					<TD style="HEIGHT: 24px" width="30%"><asp:label id="lblUserIDCaption" Runat="server" Width="123px" CssClass="labelRequire">Empl. ID:</asp:label></TD>
					<TD style="HEIGHT: 24px"><asp:textbox id="txtEmpID" Runat="server" Width="162px" ReadOnly="True" MaxLength="20" CssClass="input"></asp:textbox><INPUT class="search" id="btnSearchByID" onclick="javascript:OpenWindowEmp()" type="button"
							value="..." name="btnSearchByID"></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblUserNameCaption" Runat="server" Width="123px" Font-Name="Arial" CssClass="label">Empl. Name:</asp:label></TD>
					<TD><asp:textbox id="txtUserName" Runat="server" Width="192" MaxLength="50" CssClass="input"></asp:textbox>&nbsp;&nbsp;</TD>
				</TR>
				<TR>
					<TD><asp:label id="Label3" Runat="server" Width="123px" CssClass="labelRequire">Account:</asp:label></TD>
					<TD><asp:textbox id="txtUserID" Runat="server" Width="192" MaxLength="20" CssClass="input"></asp:textbox></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblPasswordCaption" Runat="server" Width="123px" CssClass="labelRequire">Password:</asp:label></TD>
					<TD><asp:textbox id="txtPassword" Runat="server" Width="192" MaxLength="50" TextMode="Password" CssClass="input"></asp:textbox></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblConfirmPassCaption" Runat="server" Width="123" CssClass="labelRequire">Confirm password:</asp:label></TD>
					<TD><asp:textbox id="txtConfirmPass" Runat="server" Width="192" MaxLength="50" TextMode="Password"
							CssClass="input"></asp:textbox></TD>
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
		<TD align="left">
			<table width="100%">
				<tr>
					<td align="center"><asp:linkbutton id="btnAddNew" accessKey="N" CssClass="btnAddnew" runat="server" ToolTip="Alt+N">AddNew</asp:linkbutton>&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnSaveRecord" accessKey="R" CssClass="btnSave" runat="server" ToolTip="ALT+R">Save</asp:linkbutton>&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnDelete" accessKey="D" CssClass="btnDelete" runat="server" ToolTip="Alt + D">Delete</asp:linkbutton></td>
				</tr>
			</table>
			<uc1:ColumnList id="uctrlColumns" runat="server"></uc1:ColumnList>
		</TD>
	</TR>
	<TR style="DISPLAY:none">
		<TD align="center" colSpan="2">
			<asp:linkbutton id="btnSearch" accessKey="F" runat="server" CssClass="btnSearch" ToolTip="F">Search</asp:linkbutton></TD>
	</TR>
	<TR>
		<TD align="center"></TD>
	</TR>
	<TR vAlign="top" borderColor="#ffffff">
		<td><asp:datagrid id="dgUserAccount" runat="server" CssClass="grid" Width="100%" CellPadding="0" AutoGenerateColumns="False"
				AllowSorting="True" AllowPaging="True">
				<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
				<ItemStyle CssClass="gridItem"></ItemStyle>
				<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
				<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
				<Columns>
					<asp:TemplateColumn HeaderText="Select">
						<HeaderStyle Width="5%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<HeaderTemplate>
							<asp:CheckBox id="chkSelectAll" onclick="CheckAll('_ctl0_dgUserAccount__ctl2_chkSelectAll','_ctl0_dgUserAccount',3,1,'chkSelect')"
								runat="server" CssClass="gridFooter"></asp:CheckBox>
						</HeaderTemplate>
						<ItemTemplate>
							<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="UserGroupID" SortExpression="UserGroupID" HeaderText="Account">
						<ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="UserGroupName" SortExpression="UserGroupName" HeaderText="Empl. Name">
						<ItemStyle HorizontalAlign="Left"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="EmpCode" SortExpression="EmpCode" HeaderText="Empl. ID">
						<ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="FValid" HeaderText="Run web">
						<ItemStyle Width="70px"></ItemStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn HeaderText="Run web">
						<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
						<ItemTemplate>
							<asp:CheckBox id="chkValid" CssClass="checkbox" Enabled="False" Runat="server"></asp:CheckBox>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn Visible="False" DataField="FAdm" HeaderText="Admin">
						<ItemStyle Width="10%"></ItemStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn HeaderText="Admin">
						<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
						<ItemTemplate>
							<asp:CheckBox id="chkAdm" CssClass="checkbox" Enabled="False" Runat="server"></asp:CheckBox>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
			</asp:datagrid></td>
	</TR>
</table>
<script language="javascript">
	function OpenWindowEmp()
	{
		ShowDialog('FormPage.aspx?Ascx=MdlHR/EmpPopup.ascx&Params=3');
	}
	
	function ReturnEmpPopUp3(strEmpID, strEmpCode, strEmpName)
	{
		//document.getElementById("_ctl0_txtEmpID").value = strEmpID;
		document.getElementById("_ctl0_txtEmpID").value = strEmpCode;
		document.getElementById("_ctl0_txtUserName").value = strEmpName;
		document.getElementById("_ctl0_txtUserID").value = strEmpCode;
	}
	
	function CheckDelete()
	{
		if (GridCheck('_ctl0_dgUserAccount', 3, 1, 'chkSelect') == false)
		{
			//alert("Must check at least one record to save!");	
			GetAlertError(iTotal,DSAlert,"Add0001");
			return false;
		}
		return Confirm_Delete();
	}
	
	function CheckSave()
	{
		var value;
		value = document.getElementById("_ctl0_txtEmpID").value;
		if (value == "")
		{
			//alert("Choose employee for registering!");
			GetAlertError(iTotal,DSAlert,"Add0002");
			document.getElementById("btnSearchByID").focus();
			return false;
		}
		value = document.getElementById("_ctl0_txtUserID").value;
		if (value == "")
		{
			//alert("Input account!");
			GetAlertError(iTotal,DSAlert,"Add0003");
			document.getElementById("_ctl0_txtUserID").focus();
			return false;
		}
		value = document.getElementById("_ctl0_txtPassword").value;
		if (value == "")
		{
			//alert("Input password!");
			GetAlertError(iTotal,DSAlert,"Add0004");
			document.getElementById("_ctl0_txtPassword").focus();
			return false;
		}
		var valueConfirm = document.getElementById("_ctl0_txtConfirmPass").value;
		if (value != valueConfirm)
		{
			//alert("Input confirm password!");
			GetAlertError(iTotal,DSAlert,"Add0005");
			document.getElementById("_ctl0_txtConfirmPass").focus();
			return false;
		}
		return true;
	}
</script>
