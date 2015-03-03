<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearchReport" Src="../../Include/EmpHeaderSearchReport.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="SI_rptC47b_EN.ascx.cs" Inherits="Reports.HR.SI_rptC47b_EN" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
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
		<TD colspan="4"><asp:textbox id="txtTitle" runat="server" CssClass="input" Width="80%">SI_rptC47b </asp:textbox></TD>
	<TR>
		<TD width="15%">
			<P align="left"><asp:label id="Label5" runat="server" CssClass="labelRequire">From month</asp:label></P>
		</TD>
		<TD width="20%"><asp:textbox id="txtFromMonth" onblur="CheckMonthYear(this)" runat="server" CssClass="input"
				Width="80px" MaxLength="7"></asp:textbox></TD>
		<TD width="15%">
			<asp:label id="Label2" CssClass="labelRequire" runat="server">To month</asp:label></TD>
		<TD width="30%">
			<asp:textbox id="txtToMonth" onblur="CheckMonthYear(this)" CssClass="input" runat="server" Width="80px"
				MaxLength="10"></asp:textbox></TD>
		<td>&nbsp;&nbsp;&nbsp;</td>
	</TR>
	<TR>
		<TD width="15%">
			<asp:label id="Label3" CssClass="label" runat="server">Print date</asp:label></TD>
		<TD width="20%">
			<asp:textbox id="txtPrintDate" onblur="CheckDate1(this)" CssClass="input" runat="server" Width="80px"
				MaxLength="7"></asp:textbox></TD>
		<TD width="15%"></TD>
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
		if (document.getElementById('_ctl0_txtFromMonth').value == "")
		{
			GetAlertError(iTotal,DSAlert,"0003");
			document.getElementById('_ctl0_txtFromMonth').focus();
			return false;
		}
		if (document.getElementById('_ctl0_txtToMonth').value == "")
		{
			GetAlertError(iTotal,DSAlert,"0003");
			document.getElementById('_ctl0_txtToMonth').focus();
			return false;
		}
		//if(DeltaMonth1Month2(document.getElementById("_ctl0_txtFromMonth"),document.getElementById("_ctl0_txtToMonth"))<=0)
		if (FromSmallToMonth(document.getElementById("_ctl0_txtFromMonth"),document.getElementById("_ctl0_txtToMonth")) == false)
		{
			GetAlertError(iTotal,DSAlert,"SI_0035");
			document.getElementById("_ctl0_txtFromMonth").focus();
			return false
		}
				
		if (document.getElementById('_ctl0_txtPrintDate').value != "")
		{
			if (CheckDate1(document.getElementById('_ctl0_txtPrintDate'))== false)
			{
				GetAlertError(iTotal,DSAlert,"0030");
				document.getElementById('_ctl0_txtPrintDate').focus();
				return false;
			}
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
	
/*		if (document.getElementById('_ctl0_EmpHeaderSearch1_cboCompany').value == "")
		{
			GetAlertError(iTotal,DSAlert,"0003");
			document.getElementById('_ctl0_EmpHeaderSearch1_cboCompany').focus();
			return false;
		}
*/		
		return true;		
	}
	
	function DeltaMonth1Month2(field1, field2)
	{
			var value1 = field1.value;
			var value2 = field2.value;
			if (value1 == "" || value2 == "")
				return;
		year1 = value1.substr(3,4);
		month1 = value1.substr(0,2);
		year2 = value2.substr(3,4);
		month2 = value2.substr(0,2);
		var delta = (parseInt(year2,10) * 12 + parseInt(month2,10)) - (parseInt(year1,10) * 12 + parseInt(month1,10)) + 1;
		return delta;
   
	}
</script>
