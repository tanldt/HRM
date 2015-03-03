<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../../Include/EmpHeaderSearch.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="TNCN_Khac.ascx.cs" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" Inherits="Reports.PIT.TNCN_Khac" %>
<table id="Report" width="100%" border="0">
	<tr>
		<td colSpan="2">
			<uc1:EmpHeaderSearch id="EmpHeaderSearch1" IsStatus="false" runat="server"></uc1:EmpHeaderSearch></td>
	</tr>
	<tr>
		<TD align="right"><asp:label id="cboReportTypeTitle" CssClass="labelrequire" runat="server" Width="100%">Report Type </asp:label></TD>
		<td><asp:dropdownlist id="cboReportType" CssClass="combo" runat="server" Width="200px">
				<asp:ListItem Value="" Selected="True"></asp:ListItem>
				<asp:ListItem Value="01-DK-TNCN">01-DK-TNCN</asp:ListItem>
			</asp:dropdownlist></td>
	</tr>
	<tr>
		<td align="right"><asp:label id="Label1" runat="server" CssClass="labelRequire">Join Date</asp:label></td>
		<td><asp:textbox id="txtFromDate" onblur="JavaScript:CheckDate(this)" runat="server" CssClass="input"
				MaxLength="10" Width="80px"></asp:textbox>
			&nbsp;<INPUT class=btnCal onclick="javascript:popUpCalendar(<%= this.txtFromDate.ClientID %>)" type=button>
			&nbsp; -
			<asp:textbox id="txtToDate" onblur="JavaScript:CheckDate(this)" runat="server" CssClass="input"
				MaxLength="10" Width="80px"></asp:textbox>&nbsp;<INPUT class=btnCal onclick="javascript:popUpCalendar(<%= this.txtToDate.ClientID %>)" type=button></td>
	</tr>
	<tr>
		<td colSpan="2" align=center>
			<asp:linkbutton id="btnView" runat="server" CssClass="btnSearch">Xem</asp:linkbutton>
		</td>
	</tr>
</table>
<script language="javascript">
<!--	
	function checkvalidSearch()
	{
		if(checkisnull('cboReportType')==false)  return false;
		if (document.getElementById('_ctl0_txtFromDate').value == "")
		{
			GetAlertError(iTotal,DSAlert,"0003");
			document.getElementById('_ctl0_txtFromDate').focus();
			return false;
		}				

		if (document.getElementById('_ctl0_txtToDate').value == "")
		{
			GetAlertError(iTotal,DSAlert,"0003");
			document.getElementById('_ctl0_txtToDate').focus();
			return false;
		}
		return true;		
	}
	function checkisnull(obj)
{
	if(document.getElementById('_ctl0_' + obj).value=="")
		{
			GetAlertError(iTotal,DSAlert,"0003");
			document.getElementById('_ctl0_' + obj).focus();
			return false;
		}
	else
		return true;
}
//-->
</script>
