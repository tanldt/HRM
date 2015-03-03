<%@ Control Language="c#" AutoEventWireup="false" Codebehind="SI_rptC66a_EN.ascx.cs" Inherits="Reports.HR.SI_rptC66a_EN" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearchReport" Src="../../Include/EmpHeaderSearchReport.ascx" %>
<table width="100%">
	<TR>
		<TD align="center" colSpan="5"><asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></TD>
	</TR>
	<tr>
		<td align="center" colSpan="5" style="HEIGHT: 6px"><uc1:EmpHeaderSearchReport id="EmpHeaderSearchReport1" runat="server" IsStatus="True"></uc1:EmpHeaderSearchReport></td>
	</tr>
	<TR style="DISPLAY:none">
		<TD width="15%">
			<P align="left"><asp:label id="Label1" runat="server" CssClass="label"> Title</asp:label></P>
		</TD>
		<TD colspan="4"><asp:textbox id="txtTitle" runat="server" CssClass="input" Width="80%">DANH SÁCH NGƯỜI LAO ĐỘNG ĐỀ NGHỊ HƯỞNG CHẾ ĐỘ ỐM ĐAU </asp:textbox></TD>
	<TR>
		<TD width="15%">
			<P align="left"><asp:label id="Label5" runat="server" CssClass="labelRequire">Month</asp:label></P>
		</TD>
		<TD width="20%"><asp:textbox id="txtDate" onblur="CheckMonthYear(this)" runat="server" CssClass="input" Width="80px"
				MaxLength="7"></asp:textbox></TD>
		<TD width="15%">
			<asp:label id="Label2" CssClass="label" runat="server">Print date</asp:label></TD>
		<TD width="30%">
			<asp:textbox id="txtPrintDate" onblur="CheckDate1(this)" CssClass="input" runat="server" Width="80px"
				MaxLength="10"></asp:textbox></TD>
		<td>&nbsp;&nbsp;&nbsp;</td>
	</TR>
	<TR>
		<TD width="15%" style="HEIGHT: 24px"><asp:label id="Label3" CssClass="labelRequire" runat="server">Stage</asp:label></TD>
		<TD width="20%" style="HEIGHT: 24px"><asp:textbox id="txtStage" onblur="checkStage(this)" CssClass="input" runat="server" Width="80px"
				MaxLength="1">1</asp:textbox></TD>
		<TD width="15%" style="HEIGHT: 24px"></TD>
		<TD width="30%" style="HEIGHT: 24px">
			<asp:linkbutton id="btnSearch" CssClass="btnSearch" runat="server"> Preview</asp:linkbutton></TD>
		<TD style="HEIGHT: 24px"></TD>
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
	function checkStage()
	{
		if (document.getElementById("_ctl0_txtStage").value !="" && document.getElementById("_ctl0_txtStage").value !=1 && document.getElementById("_ctl0_txtStage").value!=2 )
		{
			GetAlertError(iTotal, DSAlert, "SI_0011");
			document.getElementById("_ctl0_txtStage").focus();
			return false;
		}
	}
	function checkvalidSearch()
	{
		if (document.getElementById('_ctl0_txtDate').value == "")
		{
			GetAlertError(iTotal,DSAlert,"0003");
			document.getElementById('_ctl0_txtDate').focus();
			return false;
		}
		if (document.getElementById('_ctl0_txtStage').value == "")
		{
			GetAlertError(iTotal,DSAlert,"0003");
			document.getElementById('_ctl0_txtStage').focus();
			return false;
		}
		if (document.getElementById("_ctl0_txtStage").value !="" && document.getElementById("_ctl0_txtStage").value !=1 && document.getElementById("_ctl0_txtStage").value!=2 )
		{
			GetAlertError(iTotal, DSAlert, "SI_0011");
			document.getElementById("_ctl0_txtStage").focus();
			return false;
		}
		if (document.getElementById('_ctl0_EmpHeaderSearchReport1_cboCompany').value == "")
		{
			GetAlertError(iTotal,DSAlert,"0003");
			document.getElementById('_ctl0_EmpHeaderSearchReport1_cboCompany').focus();
			return false;
		}
		if (document.getElementById('_ctl0_EmpHeaderSearchReport1_cboLevel1').value == "")
		{
			GetAlertError(iTotal,DSAlert,"0003");
			document.getElementById('_ctl0_EmpHeaderSearchReport1_cboLevel1').focus();
			return false;
		}
		return true;		
	}
</script>
