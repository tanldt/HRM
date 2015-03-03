<%@ Control Language="c#" AutoEventWireup="false" Codebehind="SYS_ucChangePassword.ascx.cs" Inherits="iHRPCore.SYS.SYS_ucChangePassword" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table width="100%">
	<tr>
		<td align="center" colSpan="2"><asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></td>
	</tr>
	<TR>
		<TD style="HEIGHT: 147px" vAlign="middle" align="center" colSpan="2"><asp:panel id="Panel1" runat="server" Height="88pt" BackColor="WhiteSmoke">
				<TABLE>
					<TR style="DISPLAY: none">
						<TD style="HEIGHT: 25px" align="left">
							<asp:Label id="Label1" CssClass="Label" runat="server" Height="18px" Width="134">User name</asp:Label></TD>
						<TD style="HEIGHT: 25px" align="left">
							<asp:Label id="lblUserName" CssClass="labelData" runat="server" Width="144px"></asp:Label></TD>
					</TR>
					<TR>
						<TD style="HEIGHT: 25px" align="left">
							<asp:Label id="lblCurUserCaption" CssClass="Label" runat="server" Height="18px" Width="134">Current user</asp:Label></TD>
						<TD style="HEIGHT: 25px" align="left">
							<asp:Label id="lblCurUser" CssClass="labelData" runat="server" Width="144px"></asp:Label></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 112px" align="left" height="25">
							<asp:Label id="lblOldPassCaption" CssClass="labelRequire" runat="server" Width="134px">Old password</asp:Label></TD>
						<TD>
							<asp:TextBox id="txtOldPass" CssClass="input" runat="server" Font-Names="Arial" Font-Size="8pt"
								TextMode="Password" MaxLength="50"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 112px" align="left" height="25">
							<asp:Label id="lblNewPassCaption" CssClass="labelRequire" Height="16px" Width="97px" Font-Name="Arial"
								Runat="server">New password</asp:Label></TD>
						<TD align="left" height="25">
							<asp:TextBox id="txtNewPass" CssClass="input" Font-Names="Arial" Font-Size="8pt" TextMode="Password"
								MaxLength="50" Font-Name="Arial" Runat="server"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 112px" align="left" height="25">
							<asp:Label id="lblConfirmPassCaption" CssClass="labelRequire" Width="128px" Font-Name="Arial"
								Runat="server">Confirm new password</asp:Label></TD>
						<TD align="left" height="25">
							<asp:TextBox id="txtConfirmPass" CssClass="input" Font-Names="Arial" Font-Size="8pt" TextMode="Password"
								MaxLength="50" Font-Name="Arial" Runat="server"></asp:TextBox></TD>
					</TR>
				</TABLE>
			</asp:panel>
		</TD>
	</TR>
	<TR>
		<TD style="HEIGHT: 24px" colspan="2" align="center">&nbsp;
			<asp:linkbutton id="btnSave" accessKey="R" runat="server" CssClass="btnSave" ToolTip="ALT+R">Save</asp:linkbutton>&nbsp;&nbsp;&nbsp;
			<asp:linkbutton id="btnCancel" accessKey="C" runat="server" CssClass="btnDelete" ToolTip="Alt + C">Cancel</asp:linkbutton>&nbsp;&nbsp;&nbsp;
		</TD>
	</TR>
	<TR>
		<td style="HEIGHT: 23px" align="center" colSpan="2"><asp:label id="lblError" Font-Names="Arial" Font-Size="8pt" Font-Name="Arial" Runat="server"
				Font-Bold="True" ForeColor="red"></asp:label></td>
	</TR>
</table>
<script language="javascript">
			function Checkform()
			{
				var value;
				value = document.getElementById("_ctl0_txtNewPass").value;
				if (value == "")
				{
					GetAlertError(iTotal,DSAlert,"SCP_0002");
					document.getElementById("_ctl0_txtNewPass").focus();
					return false;
				}
				var valueConfirm = document.getElementById("_ctl0_txtConfirmPass").value;
				if (value != valueConfirm)
				{
					//alert("Confirm password does not conflict with new password!");
					GetAlertError(iTotal,DSAlert,"SCP_0003");
					document.getElementById("_ctl0_txtConfirmPass").value = "";
					document.getElementById("_ctl0_txtConfirmPass").focus();
					return false;
				}
				return true;
			}
</script>
