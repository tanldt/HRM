<%@ Control Language="c#" AutoEventWireup="false" Codebehind="TMS_frmrptDiTre_VeSom.ascx.cs" Inherits="Reports.TMS.TMS_frmrptDiTre_VeSom" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../../Include/EmpHeaderSearch.ascx" %>
<table width="100%">
	<TR>
		<TD align="center" colSpan="5"><asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></TD>
	</TR>
	<tr>
		<td align="center" colSpan="5"><uc1:empheadersearch id="EmpHeaderSearch1" runat="server"></uc1:empheadersearch></td>
	</tr>
	<TR>
		<TD width="15%">
			<P align="left"><asp:label id="Label5" runat="server" CssClass="labelRequire">Tháng</asp:label></P>
		</TD>
		<TD width="20%"><asp:textbox id="txtMonth" onblur="CheckMonthYear(this)" runat="server" CssClass="input" Width="80px"
				MaxLength="7"></asp:textbox></TD>
		<TD width="15%"></TD>
		<TD width="30%">
			<asp:linkbutton id="btnSearch" CssClass="btnSearch" runat="server"> Xuất Excel</asp:linkbutton></TD>
		<td>&nbsp;&nbsp;&nbsp;</td>
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
		/*
		if (document.getElementById('_ctl0_EmpHeaderSearch1_cboLevel1').value == "")
		{
			GetAlertError(iTotal,DSAlert,"CTD_0001");
			document.getElementById('_ctl0_EmpHeaderSearch1_cboLevel1').focus();
			return false;
		}
		*/
		if (document.getElementById('_ctl0_txtMonth').value == "")
		{
			GetAlertError(iTotal,DSAlert,"0003");
			document.getElementById('_ctl0_txtMonth').focus();
			return false;
		}
		
		return true;		
	}
</script>
