<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearchReport" Src="../../Include/EmpHeaderSearchReport.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="SI_rptC04BH_100.ascx.cs" Inherits="Reports.HR.SI_rptC04BH_100" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table width="100%">
	<TR>
		<TD align="center" colSpan="5"><asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label></TD>
	</TR>
	<tr>
		<td align="center" colSpan="5"><uc1:empheadersearchreport id="EmpHeaderSearchReport1" runat="server" IsStatus="True"></uc1:empheadersearchreport></td>
	</tr>
	<TR style="DISPLAY: none">
		<TD width="15%">
			<P align="left"><asp:label id="Label1" CssClass="label" runat="server"> Tiêu đề</asp:label></P>
		</TD>
		<TD colSpan="4"><asp:textbox id="txtTitle" CssClass="input" runat="server" Width="80%">DANH SÁCH NGƯỜI LAO ĐỘNG ĐỀ NGHỊ HƯỞNG CHẾ ĐỘ ỐM ĐAU </asp:textbox></TD>
	<TR>
		<TD width="15%">
			<P align="left"><asp:label id="Label5" CssClass="labelRequire" runat="server">Tháng</asp:label></P>
		</TD>
		<TD width="20%"><asp:textbox id="txtDate" onblur="CheckMonthYear(this)" CssClass="input" runat="server" Width="80px"
				MaxLength="7"></asp:textbox></TD>
		<TD width="15%"><asp:label id="Label2" CssClass="label" runat="server">Ngày In</asp:label></TD>
		<TD width="30%"><asp:textbox id="txtPrintDate" onblur="CheckDate(this)" CssClass="input" runat="server" Width="80px"
				MaxLength="10"></asp:textbox></TD>
		<td>&nbsp;&nbsp;&nbsp;</td>
	</TR>
	<TR>
		<TD width="15%"></TD>
		<TD width="20%"></TD>
		<TD width="15%"></TD>
		<TD width="30%"><asp:linkbutton id="btnSearch" CssClass="btnSearch" runat="server"> Xuất Excel</asp:linkbutton></TD>
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
