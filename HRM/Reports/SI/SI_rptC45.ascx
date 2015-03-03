<%@ Control Language="c#" AutoEventWireup="false" Codebehind="SI_rptC45.ascx.cs" Inherits="Reports.HR.SI_rptC45" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearchReport" Src="../../Include/EmpHeaderSearchReport.ascx" %>
<table width="100%">
	<TR>
		<TD align="center" colSpan="7"><asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></TD>
	</TR>
	<tr>
		<td align="center" colSpan="7"><uc1:EmpHeaderSearchReport id="EmpHeaderSearchReport1" runat="server" IsStatus="True"></uc1:EmpHeaderSearchReport></td>
	</tr>
	<TR style="DISPLAY:none">
		<TD width="15%">
			<P align="left"><asp:label id="Label1" runat="server" CssClass="label"> Title</asp:label></P>
		</TD>
		<TD colspan="6"><asp:textbox id="txtTitle" runat="server" CssClass="input" Width="80%">SI_rptC45 </asp:textbox></TD>
	<TR>
		<TD width="15%">
			<P align="left">
				<asp:label id="Label3" CssClass="labelRequire" runat="server" Width="56px">Year</asp:label></P>
		</TD>
		<TD width="20%">
			<asp:textbox id="txtYear" onblur="CheckYear(this)" CssClass="input" runat="server" Width="80px"
				MaxLength="4"></asp:textbox></TD>
		<TD width="20%">
			<P align="left"><asp:label id="Label5" runat="server" CssClass="labelRequire">End date</asp:label></P>
		</TD>
		<TD width="135" style="WIDTH: 135px"><asp:textbox id="txtDate" onblur="CheckDate(this)" runat="server" CssClass="input" Width="80px"
				MaxLength="10"></asp:textbox></TD>
		<TD width="15%">
			<asp:label id="Label2" CssClass="label" runat="server">Print date</asp:label></TD>
		<TD width="30%">
			<asp:textbox id="txtPrintDate" onblur="CheckDate(this)" CssClass="input" runat="server" Width="80px"
				MaxLength="10"></asp:textbox></TD>
		<td>&nbsp;&nbsp;&nbsp;</td>
	</TR>
	<TR>
		<TD width="15%"></TD>
		<TD width="20%"></TD>
		<TD width="15%"></TD>
		<TD width="135" style="WIDTH: 135px">
			<asp:linkbutton id="btnSearch" CssClass="btnSearch" runat="server"> Preview</asp:linkbutton></TD>
		<TD></TD>
		<TD></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD colSpan="7">
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
		if (document.getElementById('_ctl0_txtYear').value == "")
		{
			GetAlertError(iTotal,DSAlert,"0041");
			document.getElementById('_ctl0_txtYear').focus();
			return false;
		}
		if (document.getElementById('_ctl0_txtDate').value == "")
		{
			GetAlertError(iTotal,DSAlert,"0003");
			document.getElementById('_ctl0_txtDate').focus();
			return false;
		}
		
/*		if (document.getElementById('_ctl0_EmpHeaderSearch1_cboCompany').value == "")
		{
			GetAlertError(iTotal,DSAlert,"0003");
			document.getElementById('_ctl0_EmpHeaderSearch1_cboCompany').focus();
			return false;
		}
*/		
		return true;		
	}
</script>
