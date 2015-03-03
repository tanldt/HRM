<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearchReport" Src="../../Include/EmpHeaderSearchReport.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="SI_rpt01TBH_EN.ascx.cs" Inherits="iHRPCore.Reports.SI_rpt01TBH_EN" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<TABLE id="tblMain" border="0" cellSpacing="0" cellPadding="0" width="97%">
	<TR>
		<TD height="10" vAlign="top" align="center"><asp:label id="lblErr" runat="server" CssClass="Label" ForeColor="Red"></asp:label></TD>
	</TR>
	<TR>
		<TD vAlign="top" align="center"><uc1:empheadersearchreport id="EmpHeaderSearchReport1" runat="server" IsStatus="True"></uc1:empheadersearchreport>
			<HR style="WIDTH: 100%; HEIGHT: 2px" SIZE="2" width="100%">
		</TD>
	</TR>
	<TR>
		<TD vAlign="top" align="center" height="10"></TD>
	</TR>
	<TR>
		<TD height="10" vAlign="top" align="center">
			<TABLE id="Table2" border="0" cellSpacing="8" cellPadding="0" width="90%" align="center"
				runat="server">
				<TR style="DISPLAY:none">
					<TD align="center" width="40%" colSpan="2"><asp:radiobuttonlist id="rdSelect" runat="server" RepeatDirection="Horizontal">
							<asp:ListItem Value="1" Selected="True">Offer letter</asp:ListItem>
							<asp:ListItem Value="2">C?ng tác viên</asp:ListItem>
							<asp:ListItem Value="3">HÐ Chính th?c</asp:ListItem>
						</asp:radiobuttonlist></TD>
				</TR>
				<TR>
					<TD width="40%" align="right"><asp:label id="Label5" runat="server" CssClass="labelRequire"> From date&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:label>&nbsp;</TD>
					<TD><asp:textbox onblur="JavaScript:CheckDate(this)" id="txtFromDate" runat="server" CssClass="input"
							Width="100px" MaxLength="10"></asp:textbox>&nbsp;<INPUT class=btnCal onclick="javascript:popUpCalendar(<%= this.txtFromDate.ClientID %>)" type=button>
					</TD>
				</TR>
				<TR>
					<TD align="right"><asp:label id="Label1" runat="server" CssClass="labelRequire">To date&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:label></TD>
					<TD><asp:textbox onblur="JavaScript:CheckDate(this)" id="txtToDate" runat="server" CssClass="input"
							Width="100px" MaxLength="10"></asp:textbox>&nbsp;<INPUT class=btnCal onclick="javascript:popUpCalendar(<%= this.txtToDate.ClientID %>)" type=button>
					</TD>
				</TR>
			</TABLE>
			<HR width="100%">
		</TD>
	</TR>
	<tr>
		<td align="center"><asp:linkbutton id="btnExport" runat="server" CssClass="btnExport"> Export</asp:linkbutton></td>
	</tr>
</TABLE>
<script language="javascript">
function validform()
{		
	if(checkisnull('txtFromDate')==false)  return false;
	if(checkisnull('txtToDate')==false)  return false;
	
	var tuNgay = document.getElementById("_ctl0_txtFromDate");
	var denNgay = document.getElementById("_ctl0_txtToDate");
	if ( tuNgay != "" && denNgay != "" )
	{
		if ( FromSmallToDate(tuNgay, denNgay) == false )
		{
			GetAlertError(iTotal,DSAlert,"0007");	
			return false;
		}
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
</script>
