<%@ Control Language="c#" AutoEventWireup="false" Codebehind="SI_rpt02SBH.ascx.cs" Inherits="Reports.HR.SI_rpt02SBH" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
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
			<P align="left"><asp:label id="Label1" runat="server" CssClass="label"> Tiêu d?</asp:label></P>
		</TD>
		<TD colspan="4"><asp:textbox id="txtTitle" runat="server" CssClass="input" Width="80%">SI_rptC45 </asp:textbox></TD>
	<TR>
		<TD width="15%">
			<P align="left"><asp:label id="Label5" runat="server" CssClass="labelRequire">Tháng</asp:label></P>
		</TD>
		<TD width="20%"><asp:textbox id="txtDate" onblur="CheckMonthYear(this)" runat="server" CssClass="input" Width="80px"
				MaxLength="7"></asp:textbox></TD>
		<TD width="15%">
			<asp:label id="Label2" CssClass="label" runat="server">Ngày In</asp:label></TD>
		<TD width="30%">
			<asp:textbox id="txtPrintDate" onblur="CheckDate(this)" CssClass="input" runat="server" Width="80px"
				MaxLength="10"></asp:textbox></TD>
		<td>&nbsp;&nbsp;&nbsp;</td>
	</TR>
	<TR>
		<TD width="15%">
			<asp:label id="Label3" CssClass="labelRequire" runat="server">Đợt</asp:label></TD>
		<TD width="20%">
			<asp:textbox id="txtStage" onblur="checkNumeric(this)" CssClass="input" runat="server" Width="80px"
				MaxLength="2">1</asp:textbox></TD>
		<TD width="15%"></TD>
		<TD width="30%">
			<asp:linkbutton id="btnSearch" CssClass="btnSearch" runat="server"> Xu?t Excel</asp:linkbutton></TD>
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
		if (document.getElementById('_ctl0_txtPrintDate').value != "")
		{
			CheckDate(document.getElementById('_ctl0_txtPrintDate'));
			
			//GetAlertError(iTotal,DSAlert,"0003");
			//document.getElementById('_ctl0_txtPrintDate').focus();
			//return false;
		}
		
		if (document.getElementById('_ctl0_txtStage').value == "" )
		{
			GetAlertError(iTotal,DSAlert,"0003");
			document.getElementById('_ctl0_txtStage').focus();
			return false;
		}
		/*if (document.getElementById('_ctl0_txtStage').value == 0 )
		{
			GetAlertError(iTotal,DSAlert,"0012");
			document.getElementById('_ctl0_txtStage').focus();
			return false;
		}*/
		if (document.getElementById("_ctl0_txtStage").value !=1 && document.getElementById("_ctl0_txtStage").value!=2 )
		{
			GetAlertError(iTotal, DSAlert, "SI_0011");
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
	
	function checkQuarter()
	{
		
		
/*		if (document.getElementById('_ctl0_EmpHeaderSearch1_cboCompany').value == "")
		{
			GetAlertError(iTotal,DSAlert,"0003");
			document.getElementById('_ctl0_EmpHeaderSearch1_cboCompany').focus();
			return false;
		}
*/		
			
	}
</script>
