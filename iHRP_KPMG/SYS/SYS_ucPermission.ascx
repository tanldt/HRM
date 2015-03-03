<%@ Control Language="c#" AutoEventWireup="false" Codebehind="SYS_ucPermission.ascx.cs" Inherits="iHRPCore.SYS.SYS_ucPermission" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table width="100%">
	<TR>
		<TD align="center" colSpan="2"><asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label></TD>
	</TR>
	<TR align="center">
		<TD align="center"><asp:label id="lbl1" CssClass="labelRequire" Runat="server">Module</asp:label>&nbsp;&nbsp;&nbsp;</TD>
		<td align="left" width="60%"><asp:dropdownlist id="dlModule" CssClass="combo" runat="server" AutoPostBack="True" Width="232px"></asp:dropdownlist></td>
	</TR>
	<TR align="center">
		<TD style="HEIGHT: 26px" align="center"><asp:label id="Label1" CssClass="labelRequire" Runat="server">User/User's group</asp:label></TD>
		<td style="HEIGHT: 26px" align="left"><asp:textbox id="txtUserAccount" CssClass="input" runat="server" Width="208px" ReadOnly="True"
				Height="22"></asp:textbox><asp:button id="cmdGetUserAccount" CssClass="search" runat="server" Width="25px" Height="22px"
				Text="..." Font-Names="Arial" Font-Bold="True" Font-Size="8pt"></asp:button><asp:linkbutton id="btnPermission" CssClass="btnSave" runat="server">Permission</asp:linkbutton></td>
	</TR>
	<TR align="center">
		<TD align="center" colSpan="2"><asp:radiobutton id="rdUser" CssClass="option" runat="server" Text="User" Enabled="False" GroupName="UserType"></asp:radiobutton>&nbsp;&nbsp;&nbsp;&nbsp; 
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:radiobutton id="rdGroup" CssClass="option" runat="server" Text="Group" Enabled="False" GroupName="UserType"></asp:radiobutton></TD>
	</TR>
	<TR>
		<TD align="center" colSpan="2"><INPUT id="txtUserID" style="WIDTH: 5px; HEIGHT: 22px" type="hidden" size="1" name="txtUserID"
				runat="server"><INPUT id="rdRightMod" style="WIDTH: 20px; HEIGHT: 19px" onclick="rdchange(this)" type="radio"
				value="rdRightMod" name="Right" runat="server">
			<asp:label id="Label2" CssClass="label" Runat="server">Module rights</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<INPUT id="rdRightFunc" style="WIDTH: 20px; HEIGHT: 19px" onclick="rdchange(this)" type="radio"
				CHECKED value="rdRightFunc" name="Right" runat="server">
			<asp:label id="Label3" CssClass="label" Runat="server">Function rights</asp:label></TD>
	</TR>
	<TR>
		<TD align="right" height="10"></TD>
		<TD align="left" height="10"></TD>
	</TR>
	<TR id="trFunction" align="center" runat="server">
		<TD style="HEIGHT: 270px" vAlign="top" colSpan="2">
			<div style="OVERFLOW: auto; WIDTH: 580px; HEIGHT: 270px"><asp:datagrid id="dgFunction" CssClass="grid" runat="server" Width="100%" BackColor="White" BorderColor="#3366CC"
					BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False" AllowSorting="True">
					<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
					<ItemStyle CssClass="gridItem"></ItemStyle>
					<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
					<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
					<Columns>
						<asp:TemplateColumn HeaderText="Details">
							<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" Width="10%" HorizontalAlign="Center"></HeaderStyle>
							<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Center" Width="10%"></ItemStyle>
							<ItemTemplate>
								<asp:LinkButton id="cmdSelect" CommandName="cmdSelect" CssClass="btn" Runat="server" Width="90%"
									Text="+"></asp:LinkButton>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn Visible="False" DataField="FunctionID" HeaderText="Function ID">
							<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
							<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Left" Width="100px"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="FunctionName" HeaderText="Function Name">
							<HeaderStyle Font-Size="8pt" Font-Names="Arial" Width="90%" Font-Bold="True"></HeaderStyle>
							<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Left" Width="100%"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn Visible="False" DataField="FApp" HeaderText="Approve">
							<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
							<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Left"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn Visible="False" DataField="FRun" HeaderText="View">
							<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
							<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Left"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn Visible="False" DataField="FDel" HeaderText="Delete">
							<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
							<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Left"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn Visible="False" DataField="FEdit" HeaderText="Edit">
							<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
							<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Left"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn Visible="False" DataField="FAdd" HeaderText="Add new">
							<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
							<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Left"></ItemStyle>
						</asp:BoundColumn>
						<asp:TemplateColumn HeaderText="Select">
							<HeaderStyle Width="5%"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<HeaderTemplate>
								<asp:CheckBox id="chkSelectAll" onclick="CheckAll('_ctl0_dgFunction__ctl1_chkSelectAll','_ctl0_dgFunction',2,1,'chkSelect')"
									runat="server" CssClass="gridFooter"></asp:CheckBox>
							</HeaderTemplate>
							<ItemTemplate>
								<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
					<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
				</asp:datagrid></div>
		</TD>
	</TR>
	<TR align="center">
		<TD colSpan="2" height="10"></TD>
	</TR>
	<TR align="center">
		<TD colSpan="2"><asp:label id="lblRightTitle" runat="server" Width="265px" Font-Names="Arial" ForeColor="Gray">Assign permission</asp:label></TD>
	</TR>
	<TR align="center">
		<TD colSpan="2"></TD>
	</TR>
	<TR>
		<TD align="center" colSpan="2"><INPUT id="chkView" onclick="javascript:check()" type="checkbox" CHECKED name="chkView"
				runat="server">
			<DIV style="DISPLAY: inline; FONT-SIZE: 8pt; WIDTH: 70px; FONT-FAMILY: Arial; HEIGHT: 15px"
				ms_positioning="FlowLayout">Xem</DIV> <!--<BR>--><INPUT id="chkDelete" style="DISPLAY: none" onclick="javascript:check()" type="checkbox"
				name="chkDelete" runat="server">
			<DIV style="DISPLAY: none; FONT-SIZE: 8pt; WIDTH: 70px; FONT-FAMILY: Arial; HEIGHT: 15px"
				ms_positioning="FlowLayout">Xóa</DIV> <!--<BR>--><INPUT id="chkUpdate" style="DISPLAY: none" onclick="javascript:check()" type="checkbox"
				name="chkUpdate" runat="server">
			<DIV style="DISPLAY: none; FONT-SIZE: 8pt; WIDTH: 70px; FONT-FAMILY: Arial; HEIGHT: 15px"
				ms_positioning="FlowLayout">Sửa</DIV>
			<BR>
			<INPUT id="chkAddNew" onclick="javascript:check()" type="checkbox" name="chkAddNew" runat="server">
			<DIV style="DISPLAY: inline; FONT-SIZE: 8pt; WIDTH: 70px; FONT-FAMILY: Arial; HEIGHT: 15px"
				ms_positioning="FlowLayout">Sửa</DIV>
			<BR>
			<INPUT id="chkApprove" style="DISPLAY: none" onclick="javascript:check()" type="checkbox"
				name="chkApprove" runat="server">
			<DIV style="DISPLAY: none; FONT-SIZE: 8pt; WIDTH: 70px; FONT-FAMILY: Arial; HEIGHT: 15px"
				ms_positioning="FlowLayout">Phê duyệt</DIV>
		</TD>
	</TR>
	<TR>
		<TD align="center" colSpan="2"><asp:linkbutton id="btnAddRight" CssClass="btnSYS" runat="server">Set Rights</asp:linkbutton>&nbsp;
			<asp:linkbutton id="btnDeleteAllRight" CssClass="btnSYS" runat="server">Delete Rights</asp:linkbutton>&nbsp;&nbsp;
			<asp:linkbutton id="btnSetupAgain" CssClass="btnSYS" runat="server">Set for functions</asp:linkbutton></TD>
	</TR>
	<TR style="DISPLAY: none" align="center">
		<TD colSpan="2"><asp:linkbutton id="btnSearch" accessKey="S" CssClass="btnSearch" runat="server" ToolTip="Alt+S">Search</asp:linkbutton><INPUT id="txtChoseFGroup" style="WIDTH: 12px; HEIGHT: 22px" type="hidden" size="1" name="txtChoseFGroup"
				runat="server"><INPUT id="txtChoseUserName" style="WIDTH: 12px; HEIGHT: 22px" type="hidden" size="1" name="txtChoseUserName"
				runat="server"><INPUT id="txtChoseUserID" style="WIDTH: 12px; HEIGHT: 22px" type="hidden" size="1" name="txtChoseUserID"
				runat="server"><INPUT id="txtFuncID" style="WIDTH: 12px; HEIGHT: 22px" type="hidden" size="1" name="txtFuncID"
				runat="server"><asp:label id="lblCurUserCaption" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray">
						Current user:</asp:label>
			<asp:label id="lblCurUser" runat="server" Width="146px" Font-Names="Arial" Font-Size="8pt"></asp:label></TD>
	</TR>
</table>
<script language="javascript">
	function ReturnEmpPopUp(strUserID, strUserName, strFGroup)
	{
		document.getElementById("_ctl0_txtChoseUserID").value = strUserID;
		document.getElementById("_ctl0_txtChoseUserName").value = strUserName;
		document.getElementById("_ctl0_txtChoseFGroup").value = strFGroup;
		document.getElementById("_ctl0_btnSearch").click();
	}
	
	function OpenNewWindow()
	{
		ShowDialog('FormPage.aspx?Ascx=SYS/SYS_ucUserAccountList.ascx')
		return false;
	}
	
	function checkForm()
	{
		var value = document.Form1("_ctl0_dlModule").value;
		if (value == "")
		{
			//alert("Choose module for assigning permission!");
			GetAlertError(iTotal,DSAlert,"ucP00002");
			document.getElementById("_ctl0_cmdGetUserAccount").focus();
			return false;
		}
		value = document.Form1("_ctl0_txtUserID").value;
		if (value == "")
		{
			//alert("Choose user account/group account for assigning permission!");
			GetAlertError(iTotal,DSAlert,"ucP00003");
			document.getElementById("_ctl0_cmdGetUserAccount").focus();
			return false;
		}
		if (GridCheck('_ctl0_dgFunction', 2, 1, 'chkSelect') == false && 
			document.getElementById("_ctl0_rdRightFunc").check == true)
		{
			//alert("Must check at least one record to manipulate!");	
			GetAlertError(iTotal,DSAlert,"ucP00001");
			return false;
		}
		if (document.getElementById("_ctl0_chkView").checked == false &&
			document.getElementById("_ctl0_chkView").checked == false)
		{
			GetAlertError(iTotal,DSAlert,"ucP00004");
			document.getElementById("_ctl0_chkView").checked = true;
			return false;
		} 
		return true;
	}
	
	function rdchange(field)
	{
		var value = field.id;
		
		if(value == "_ctl0_rdRightMod")
		{
			//alert(document.getElementById("_ctl0_trFunction").style.display);
			//alert(document.getElementById("_ctl0_cmdAddRight").style.display);
			if (document.getElementById("_ctl0_trFunction").style.display == "")
				document.getElementById("_ctl0_trFunction").style.display = "none";
			//else
			//	document.getElementById("_ctl0_cmdAddRight").style.display = "";
		}
		else
		{
			if (document.getElementById("_ctl0_trFunction").style.display == "none") 
				document.getElementById("_ctl0_trFunction").style.display = "";
			//else 
			//	document.getElementById("_ctl0_cmdAddRight").style.display = "";
			document.getElementById("_ctl0_chkView").checked = false;
			document.getElementById("_ctl0_chkUpdate").checked = false;
			document.getElementById("_ctl0_chkDelete").checked = false;
			document.getElementById("_ctl0_chkApprove").checked = false;
			document.getElementById("_ctl0_chkAddNew").checked = false;
		}
	}
	function check()
	{
		if (document.getElementById("_ctl0_chkAddNew").checked)
		{
			document.getElementById("_ctl0_chkView").checked = true;
			document.getElementById("_ctl0_chkUpdate").checked = true;
			document.getElementById("_ctl0_chkDelete").checked = true;
		}
		if (!document.getElementById("_ctl0_chkAddNew").checked)
		{
			document.getElementById("_ctl0_chkUpdate").checked = false;
			document.getElementById("_ctl0_chkDelete").checked = false;
		}
		else if (document.getElementById("_ctl0_chkDelete").checked)
		{
			document.getElementById("_ctl0_chkView").checked = true;
			document.getElementById("_ctl0_chkUpdate").checked = true;
		}
		else if (document.getElementById("_ctl0_chkUpdate").checked)
		{
			document.getElementById("_ctl0_chkView").checked = true;
		}
		else if (document.getElementById("_ctl0_chkApprove").checked)
		{
			document.getElementById("_ctl0_chkView").checked = true;
		}
	}
	
	function OpenDetails()
	{
		if (document.getElementById("_ctl0_rdUser").checked == true)
			OpenDetails_U(trim(document.getElementById("_ctl0_txtUserID").value));
		else
			OpenDetails_G(trim(document.getElementById("_ctl0_txtUserID").value));
	}
	function OpenDetails_U(field)
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
	function ViewPermission()
	{
		if (trim(document.getElementById("_ctl0_txtUserAccount").value) != "")
			document.getElementById("_ctl0_btnPermission").visibility = true;
		else
			document.getElementById("_ctl0_btnPermission").visibility = false;
	}
	ViewPermission();	
</script>
