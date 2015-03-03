<%@ Import Namespace="iHRPCore.REComponent"%>
<%@ Import Namespace ="iHRPCore.Com"%>
<%@ Import Namespace ="System.Data" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="RE_frmF04.ascx.cs" Inherits="Reports.RE.RE_frmF04" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<script language="javascript" src='../Include/common.js"'></script>
<script language="javascript" src="Component/DateCOMM/popcalendar.js"></script>
<asp:Label id="lblErr" runat="server" Width="416px"></asp:Label>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="97%" align="center" border="0">
	<TR id="RECOGNITION" runat="server">
		<TD>
			<TABLE id="Table3" width="100%">
				<TR>
					<TD style="WIDTH: 100px"><asp:label id="Label18" runat="server" CssClass="label">Candidate ID</asp:label></TD>
					<TD style="WIDTH: 257px"><asp:textbox id="txtCandidateID" runat="server" CssClass="input" Width="152px"></asp:textbox></TD>
					<TD style="WIDTH: 100px"><asp:label id="Label17" runat="server" CssClass="label">Candidate Name</asp:label></TD>
					<TD><asp:textbox id="txtCandidateName" runat="server" CssClass="input" Width="152px"></asp:textbox></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 100px"><asp:label id="Label12" runat="server" CssClass="labelRequire">Project</asp:label></TD>
					<TD style="WIDTH: 257px"><asp:dropdownlist id="cboProjectID" runat="server" CssClass="combo" Width="152px" onchange="ChangeProject()"></asp:dropdownlist></TD>
					<TD></TD>
					<TD></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>
<HR width="100%" SIZE="1">
<asp:linkbutton id="btnPrint" runat="server" CssClass="btnPrint">Xem</asp:linkbutton>
<SCRIPT language="javascript">
	function CheckView()
	{
		if (document.getElementById('_ctl0_cboProjectID').selectedIndex == 0)
		{
			GetAlertError(iTotal,DSAlert,"0003");
			document.getElementById('_ctl0_cboProjectID').focus();
			return false;
		}		
	}
</SCRIPT>
