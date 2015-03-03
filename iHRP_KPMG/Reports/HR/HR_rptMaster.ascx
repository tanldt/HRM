<%@ Control Language="c#" AutoEventWireup="false" Codebehind="HR_rptMaster.ascx.cs" Inherits="Reports.HR.HR_rptMaster" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../../Include/EmpHeaderSearch.ascx" %>
<table width="100%">
	<TR>
		<TD align="center" colSpan="5"><asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></TD>
	</TR>
	<tr>
		<td align="center" colSpan="5"><uc1:empheadersearch id="EmpHeaderSearch1" runat="server" IsStatus="false"></uc1:empheadersearch></td>
	</tr>
	<TR>
		<TD width="15%">
			<P align="left"><asp:label id="Label1" runat="server" CssClass="label"> Tiêu đề</asp:label></P>
		</TD>
		<TD colspan="4"><asp:textbox id="txtTitle" runat="server" CssClass="input" Width="80%">MASTER EMPLOYEE LIST</asp:textbox></TD>
	<TR>
		<TD width="15%">
			<P align="left"><asp:label id="Label5" runat="server" CssClass="labelRequire">Ngày</asp:label></P>
		</TD>
		<TD width="20%"><asp:textbox id="txtDate" onblur="CheckDate(this)" runat="server" CssClass="input" Width="80px"
				MaxLength="10"></asp:textbox></TD>
		<TD width="15%"></TD>
		<TD width="30%" style="DISPLAY:none"><asp:linkbutton id="btnSearch1" CssClass="btnSearch" runat="server"> Xuất Excel</asp:linkbutton></TD>
		<td>&nbsp;&nbsp;&nbsp;<INPUT class="button" id="_ctl0_btnSearch" onclick="FillTable();" type="button" value="Xuất DL"
				name="btnXem"></td>
	</TR>
	<TR>
		<TD colSpan="5">
			<HR width="100%" SIZE="1">
		</TD>
	</TR>
	<TR>
		<TD colSpan="5"><span id="Loading"></span><span id="Displays"></span> <INPUT id="Export" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" runat="server"
				NAME="Export">
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
function FillTable() 
{
	if (checkvalidSearch() == true)
	{
		document.getElementById("Displays").style.display = "";
		document.getElementById("Displays").innerHTML = "<IMG SRC='images/AJAX/spinner.gif'> Chương trình đang nạp báo cáo. Bạn có thể làm việc khác trong khi chờ đợi...";
		document.getElementById('_ctl0_btnSearch').disabled = true;
		var cboCompany = document.getElementById('_ctl0_EmpHeaderSearch1_cboCompany').value;
		var cboLevel1 = document.getElementById('_ctl0_EmpHeaderSearch1_cboLevel1').value;
		var cboLevel2 = document.getElementById('_ctl0_EmpHeaderSearch1_cboLevel2').value;
		var cboLevel3 = document.getElementById('_ctl0_EmpHeaderSearch1_cboLevel3').value;
		var txtEmpID = document.getElementById('_ctl0_EmpHeaderSearch1_txtEmpID').value;
		var txtName = document.getElementById('_ctl0_EmpHeaderSearch1_txtEmpName').value;
		var txtMonth = document.getElementById('_ctl0_txtDate').value;
		var txtTitle = document.getElementById('_ctl0_txtTitle').value;
		var optStatus = "";
		if (document.getElementById("_ctl0_EmpHeaderSearch1_optStatus_0").checked == true)
			optStatus = "1";
		else if (document.getElementById("_ctl0_EmpHeaderSearch1_optStatus_1").checked == true)
			optStatus = "0";
		var AcountLogin = document.getElementById('txtAcountLogin').value;
		HR_rptMaster.GetData(cboCompany,cboLevel1,cboLevel2,cboLevel3,txtEmpID,txtName,optStatus,txtMonth,txtTitle,
		"","","","VN",FillTable_CallBack);  
	}
	//document.getElementById("Displays").style.display = "";
	//document.getElementById("Displays").innerHTML = "<IMG SRC='images/AJAX/spinner.gif'> Chương trình đang nạp báo cáo. Bạn có thể làm việc khác trong khi chờ đợi...";
	//document.all("_ctl0_btnSearch").click()
	//document.getElementById("_ctl0_btnSearch").click();
	//document.getElementById("Displays").style.display = "none";
}
function FillTable_CallBack(response) 
{ 
	var s = response.value; 
	
	document.getElementById("_ctl0_Export").value = "";
	document.getElementById("_ctl0_Export").value = s;
	//document.getElementById("exportbutton").click();
	document.getElementById("_ctl0_btnSearch1").click();
	document.getElementById("Displays").innerHTML = "";
	document.getElementById("Displays").style.display = "none"
	document.getElementById('_ctl0_btnSearch').disabled = false;
	
}
</script>
