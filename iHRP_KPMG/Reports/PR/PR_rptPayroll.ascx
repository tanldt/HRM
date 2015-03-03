<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../../Include/EmpHeaderSearch.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="PR_rptPayroll.ascx.cs" Inherits="Reports.PR.PR_rptPayroll" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table width="100%">
	<TR>
		<TD align="center" colSpan="5"><asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></TD>
	</TR>
	<tr>
		<td align="center" colSpan="5"><uc1:empheadersearch id="EmpHeaderSearch1" runat="server"></uc1:empheadersearch></td>
	</tr>
	<TR style="DISPLAY: none">
		<TD width="15%">
			<P align="left"><asp:label id="Label1" runat="server" CssClass="label"> Tiêu đề</asp:label></P>
		</TD>
		<TD colSpan="4"><asp:textbox id="txtTitle" runat="server" CssClass="input" Width="80%">TS_rpt001</asp:textbox></TD>
	<TR>
		<TD width="15%">
			<P align="left"><asp:label id="Label5" runat="server" CssClass="labelRequire">Month</asp:label></P>
		</TD>
		<TD width="20%"><asp:textbox id="txtMonth" onblur="CheckMonthYear(this)" runat="server" CssClass="input" Width="80px"
				MaxLength="7"></asp:textbox></TD>
		<TD width="15%"><asp:radiobuttonlist id="optLanguage" runat="server" Width="204px" RepeatDirection="Horizontal">
				<asp:ListItem Value="1" Selected="True">Tiếng Việt</asp:ListItem>
				<asp:ListItem Value="2">Tiếng Anh</asp:ListItem>
			</asp:radiobuttonlist></TD>
		<TD width="30%"><asp:linkbutton id="btnView" runat="server" CssClass="btnSearch">View</asp:linkbutton></TD>
		<td>&nbsp;&nbsp;&nbsp;</td>
	</TR>
	<TR>
		<TD width="15%"></TD>
		<TD width="20%"></TD>
		<TD width="15%"></TD>
		<TD width="30%"></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD colSpan="5">
			<HR width="100%" SIZE="1">
		</TD>
	</TR>
	<TR>
		<TD colSpan="5"></TD>
	</TR>
</table>
<script language="javascript">
<!--	
	function checkvalidSearch()
	{
		if (document.getElementById('_ctl0_txtMonth').value == "")
		{
			GetAlertError(iTotal,DSAlert,"0003");
			document.getElementById('_ctl0_txtMonth').focus();
			return false;
		}				

		return true;		
	}
//-->
</script>
