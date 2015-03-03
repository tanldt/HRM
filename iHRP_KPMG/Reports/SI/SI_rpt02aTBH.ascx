<%@ Control Language="c#" AutoEventWireup="false" Codebehind="SI_rpt02aTBH.ascx.cs" Inherits="Reports.HR.SI_rpt02aTBH" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearchReport" Src="../../Include/EmpHeaderSearchReport.ascx" %>
<table width="100%">
	<TR>
		<TD align="center" colSpan="5"><asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></TD>
	</TR>
	<tr>
		<td align="center" colSpan="5"><uc1:EmpHeaderSearchReport id="EmpHeaderSearchReport1" runat="server" IsStatus="True"></uc1:EmpHeaderSearchReport></td>
	</tr>
	<TR style="DISPLAY:none">
		<TD width="15%">
			<P align="left"><asp:label id="Label1" runat="server" CssClass="label"> Title</asp:label></P>
		</TD>
		<TD colspan="4"><asp:textbox id="txtTitle" runat="server" CssClass="input" Width="80%">SI_rpt01aTBH </asp:textbox></TD>
	</TR>
	<TR style="DISPLAY:none">
		<TD width="15%">
			<asp:label id="Label4" CssClass="label" runat="server" Width="76px">Nhóm location</asp:label></TD>
		<TD width="10%" colSpan="2"><asp:dropdownlist id="cboLocation" runat="server" CssClass="combo" Width="100%"></asp:dropdownlist></TD>
		<TD width="15%"></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD width="15%">
			<asp:label id="Label16" CssClass="labelRequire" runat="server" Width="80px">From Date</asp:label></TD>
		<TD width="20%">
			<asp:textbox id="txtFromDate" onblur="CheckDate(this)" CssClass="input" runat="server" Width="76"
				MaxLength="10"></asp:textbox><INPUT class=btnCal onclick="javascript:popUpCalendar(<%= this.txtFromDate.ClientID %>)" type=button></TD>
		<TD width="15%">
			<asp:label id="Label17" CssClass="labelRequire" runat="server" Width="80px">To Date</asp:label></TD>
		<TD width="30%">
			<asp:textbox id="txtToDate" onblur="CheckDate(this)" CssClass="input" runat="server" Width="76"
				MaxLength="10"></asp:textbox><INPUT class=btnCal onclick="javascript:popUpCalendar(<%= this.txtToDate.ClientID %>)" type=button></TD>
		<TD></TD>
	</TR>
	<TR style="DISPLAY:none">
		<TD width="15%">
			<P align="left"><asp:label id="Label5" runat="server" CssClass="labelRequire">Month</asp:label></P>
		</TD>
		<TD width="20%"><asp:textbox id="txtDate" onblur="CheckMonthYear(this)" runat="server" CssClass="input" Width="80px"
				MaxLength="7"></asp:textbox></TD>
		<TD width="15%"></TD>
		<TD width="30%"><asp:label id="Label3" runat="server" CssClass="label" Width="76px"> Ð?a di?m</asp:label></TD>
		<td>&nbsp;&nbsp;&nbsp;</td>
	</TR>
	<TR>
		<TD width="15%">
			<asp:label id="Label2" CssClass="label" runat="server">Print date</asp:label></TD>
		<TD width="20%" colSpan="2">
			<asp:textbox id="txtPrintDate" onblur="CheckDate(this)" CssClass="input" runat="server" Width="80px"
				MaxLength="10"></asp:textbox></TD>
		<TD width="30%">
			<asp:linkbutton id="btnSearch" CssClass="btnSearch" runat="server"> Preview</asp:linkbutton></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD colSpan="5">
			<HR width="100%" SIZE="1">
		</TD>
	</TR>
	<TR>
		<TD colSpan="5">
		</TD>
	</TR>
</table>
<script language="javascript">
	function OpenWindowEmp()
	{
		ShowDialog('FormPage.aspx?Ascx=MdlHR/EmpPopup.ascx&MultiSelect=1&LastValue=' + document.getElementById("_ctl0_txtEmpList").value)
	} 
	
	function checkvalidSearch()
	{
		if (document.getElementById('_ctl0_txtDate').value == "")
		{
			GetAlertError(iTotal,DSAlert,"0003");
			document.getElementById('_ctl0_txtDate').focus();
			return false;
		}
		
		if (document.getElementById('_ctl0_EmpHeaderSearchReport1_cboCompany').value == "")
		{
			GetAlertError(iTotal,DSAlert,"0003");
			document.getElementById('_ctl0_EmpHeaderSearchReport1_cboCompany').focus();
			return false;
		}
	
		return true;		
	}
</script>
