<%@ Register TagPrefix="uc1" TagName="EmpHeader" Src="../Include/EmpHeader.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="CreatePayRoll.ascx.cs" Inherits="iHRPCore.MdlPR.CreatePayRoll" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="Table1" style="WIDTH: 100%" cellSpacing="0" cellPadding="0" width="300" border="0">
	<TR>
		<TD align="center"><asp:label id="lblErr" CssClass="labelRequire" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center">
			<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="98%" border="0">
				<TR>
					<TD><asp:label id="Label3" CssClass="label" runat="server">Current Month</asp:label>&nbsp;&nbsp;&nbsp;<asp:textbox id="txtMonth" onblur="javascript:CheckMonthYear(this)" CssClass="input" runat="server"
							ReadOnly="True" MaxLength="7" Width="100px"></asp:textbox></TD>
					<TD></TD>
					<TD>&nbsp;
						<asp:label id="Label5" style="DISPLAY:none" CssClass="label" runat="server">Current Period</asp:label>&nbsp;</TD>
					<TD><asp:textbox id="txtSalPeriod" style="DISPLAY:none" CssClass="input" runat="server" ReadOnly="True"
							Width="100px"></asp:textbox>&nbsp;
						<asp:linkbutton id="btnNewPayroll" accessKey="S" runat="server" CssClass="btnSave" ToolTip="ALT+S">New PayRoll</asp:linkbutton></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD align="center" colSpan="4"></TD>
				</TR>
				<TR>
					<TD colSpan="4">
						<HR width="100%" color="#3366cc" SIZE="1">
					</TD>
				</TR>
				<TR style="DISPLAY: none">
					<TD height="10"><asp:label id="Label2" CssClass="labelRequire" runat="server">Copy From Month</asp:label></TD>
					<TD height="10"><asp:textbox id="txtFromMonth" onblur="javascript:CheckMonthYear(this)" CssClass="input" runat="server"
							MaxLength="7" Width="100px"></asp:textbox></TD>
					<TD height="10"><asp:label id="Label1" CssClass="labelRequire" runat="server"> Copy From Period</asp:label></TD>
					<TD height="10"><asp:dropdownlist id="cboSalPeriod" CssClass="select" runat="server" Width="100px">
							<asp:ListItem></asp:ListItem>
							<asp:ListItem Value="Kỳ 1">Kỳ 1</asp:ListItem>
							<asp:ListItem Value="Kỳ 2">Kỳ 2</asp:ListItem>
							<asp:ListItem Value="Kỳ 3">Kỳ 3</asp:ListItem>
						</asp:dropdownlist>&nbsp;
						<asp:linkbutton id="btnCopyPayroll" accessKey="S" runat="server" CssClass="btnSave" ToolTip="ALT+S">Copy PayRoll</asp:linkbutton></TD>
				</TR>
				<TR>
					<TD align="center" colSpan="4" height="10"></TD>
				</TR>
				<TR>
					<TD align="center" colSpan="4">&nbsp;&nbsp;</TD>
				</TR>
				<TR>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD align="center" height="10"><INPUT id="hdFromDate" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="hdFromDate"
				runat="server"><INPUT id="hdToDate" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="hdToDate"
				runat="server"></TD>
				
	</TR>
	<TR>
		<TD align="center"></TD>
	</TR>
	<TR>
		<TD align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
	</TR>
</TABLE>
<script language="javascript">
<!--
function CheckData()
{
	if (checkisnull("txtFromMonth","PR_0001")==false) return false;
	if (checkisnull("cboSalPeriod","PR_0002")==false) return false;		
	if(document.getElementById('_ctl0_txtFromMonth').value == document.getElementById('_ctl0_txtMonth').value &&
	'Kỳ ' + document.getElementById('_ctl0_cboSalPeriod').value == document.getElementById('_ctl0_txtSalPeriod').value)
	{		
		GetAlertError(iTotal,DSAlert,"PR_00012");
		document.getElementById('_ctl0_txtFromMonth').focus();
		return false;
	}
	return true;
}
function checkisnull(obj,pid){
	if(document.getElementById('_ctl0_' + obj).value=="")
		{
			GetAlertError(iTotal,DSAlert,pid);				
			document.getElementById('_ctl0_' + obj).focus();
			return false;
		}
	else
		return true;
}

//-->
</script>
