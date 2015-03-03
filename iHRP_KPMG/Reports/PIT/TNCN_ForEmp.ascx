<%@ Control Language="c#" AutoEventWireup="false" Codebehind="TNCN_ForEmp.ascx.cs" Inherits="iHRPCore.MdlPR.TNCN_ForEmp" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../../Include/EmpHeaderSearch.ascx" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD align="center"><asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label></TD>
	</TR>
	<tr>
		<TD align="center"><!-- start detail for input form -->
			<TABLE cellSpacing="0" cellPadding="1" width="100%" border="0">
				<TR style="DISPLAY: none">
					<TD></TD>
					<TD colSpan="5"><asp:radiobuttonlist id="optCollection" onclick="ChangeCollectionType()" runat="server" RepeatDirection="Horizontal"
							Width="424px">
							<asp:ListItem Value="1" Selected="True">Update for list of employees</asp:ListItem>
							<asp:ListItem Value="2">Update for all by condition</asp:ListItem>
						</asp:radiobuttonlist></TD>
				</TR>
				<TR id="trCondition" runat="server">
					<TD align="center" colSpan="6">
						<table width="100%">
							<tr>
								<td align="center"><uc1:empheadersearch id="EmpHeaderSearch1" runat="server"></uc1:empheadersearch></td>
							</tr>
						</table>
					</TD>
				</TR>
				<TR id="trListEmp" runat="server">
					<TD width="15%"><asp:label id="Label2" CssClass="labelRequire" runat="server" Width="100%">Employee List</asp:label></TD>
					<TD colSpan="5"><asp:textbox onkeypress="checkKey('btnSearchLstEmp')" id="txtEmpList" CssClass="input" runat="server"
							Width="450px" MaxLength="500"></asp:textbox>&nbsp;<INPUT class="search" id="btnSearchLstEmp" onclick="javascript:OpenWindowEmp()" type="button"
							value="..." name="btnSearchLstEmp"></TD>
				</TR>
				<tr>
					<td colSpan="6"><HR align="center" width="95%">
					</td>
				</tr>
				<TR style="display:none">
					<td style="HEIGHT: 16px"><asp:label id="Label5" CssClass="labelrequire" runat="server">Type Export</asp:label></td>
					<TD style="HEIGHT: 16px"><asp:dropdownlist id="cboTypeExport" runat="server" CssClass="combo">
							<asp:ListItem Value="xls">Excel</asp:ListItem>
							<asp:ListItem Value="doc">Word</asp:ListItem>
						</asp:dropdownlist></TD>
				</TR>
				<TR>
					<td style="HEIGHT: 16px"><asp:label id="Label35" CssClass="labelrequire" runat="server">Month</asp:label></td>
					<TD style="HEIGHT: 16px"><asp:textbox id="txtMonth" onblur="javascript:CheckMonthYear(this)" CssClass="input" runat="server"
							Width="100px" MaxLength="7"></asp:textbox></TD>
					<td style="HEIGHT: 16px"><asp:label id="Label1" CssClass="labelrequire" runat="server">Type Report</asp:label></td>
					<TD style="HEIGHT: 16px"><asp:dropdownlist id="cboReportMonth" runat="server" CssClass="combo">
							<asp:ListItem Selected="True"></asp:ListItem>
							<asp:ListItem Value="07-KK-TNCN">07-KK-TNCN</asp:ListItem>
						</asp:dropdownlist></TD>
					<td style="HEIGHT: 16px"><asp:linkbutton id="btnExportMonth" CssClass="btnPrint" runat="server">Export</asp:linkbutton></td>
					<td style="HEIGHT: 16px"></td>
				</TR>
				<TR>
					<td>&nbsp;</td>
					<TD colSpan="5"></TD>
				</TR>
				<TR style="display:none">
					<td><asp:label id="Label3" CssClass="labelrequire" runat="server">Year</asp:label></td>
					<TD><asp:textbox id="txtYear" onblur="javascript:CheckYear(this)" CssClass="input" runat="server"
							Width="100px" MaxLength="4"></asp:textbox></TD>
					<td><asp:label id="Label4" CssClass="labelrequire" runat="server">Type Report</asp:label></td>
					<TD><asp:dropdownlist id="cboReportYear" runat="server" CssClass="combo">
							<asp:ListItem Selected="True"></asp:ListItem>
							<asp:ListItem Value="07-KK-TNCN">07-KK-TNCN</asp:ListItem>
						</asp:dropdownlist></TD>
					<td><asp:linkbutton id="btnPrint" CssClass="btnPrint" runat="server">Export</asp:linkbutton></td>
					<td></td>
				</TR>
			</TABLE>
			<!-- end button for input form --></TD>
	</tr>
	<TR>
		<TD vAlign="top" noWrap align="center" height="20">
			<HR align="center" width="95%">
		</TD>
	</TR>
	<TR>
		<TD noWrap align="center">&nbsp;
			<asp:linkbutton id="btnSendMail" CssClass="btnSave" runat="server" Visible="False">Send Mail</asp:linkbutton>&nbsp;&nbsp;&nbsp;&nbsp;
		</TD>
	</TR>
	<!-- end button for input form --></TABLE>
<script language="javascript">
	function validMonth(){	
	
		if(document.getElementById('_ctl0_txtEmpList').value==""){
			GetAlertError(iTotal,DSAlert,"0003");
			document.getElementById('_ctl0_txtEmpList').focus();
			return false;
		}

		if(document.getElementById('_ctl0_txtMonth').value==""){
			GetAlertError(iTotal,DSAlert,"PS_0001");
			document.getElementById('_ctl0_txtMonth').focus();
			return false;
		}

		if(document.getElementById('_ctl0_cboReportMonth').value==""){
			GetAlertError(iTotal,DSAlert,"50021");
			document.getElementById('_ctl0_cboReportMonth').focus();
			return false;
		}

		return true;
	}
		function validYear(){	
	
		if(document.getElementById('_ctl0_txtEmpList').value==""){
			GetAlertError(iTotal,DSAlert,"0003");
			document.getElementById('_ctl0_txtEmpList').focus();
			return false;
		}

		if(document.getElementById('_ctl0_txtYear').value==""){
			GetAlertError(iTotal,DSAlert,"PS_0001");
			document.getElementById('_ctl0_txtYear').focus();
			return false;
		}

		if(document.getElementById('_ctl0_cboReportYear').value==""){
			GetAlertError(iTotal,DSAlert,"50021");
			document.getElementById('_ctl0_cboReportYear').focus();
			return false;
		}

		return true;
	}
	function OpenWindowEmp()
	{
		
		ShowDialog('FormPage.aspx?Ascx=MdlHR/EmpPopup.ascx&MultiSelect=1&LastValue=' + document.getElementById("_ctl0_txtEmpList").value)
	} 
	
	function ChangeCollectionType()
	{
		if (document.getElementById("_ctl0_optCollection_0").checked == true)
		{
			document.getElementById("_ctl0_trListEmp").style.display = "";
			document.getElementById("_ctl0_trCondition").style.display = "none";
		}
		else
		{
			document.getElementById("_ctl0_trListEmp").style.display = "none";
			document.getElementById("_ctl0_trCondition").style.display = "";
		}
	}

	function CheckDateAtCompanyGrid(field)
	{
		CheckDate(field);
		if (field.value == "")
		{
			GetAlertError(iTotal,DSAlert,"BL_002");
			field.focus();
			return false;
		}
	}

	function checkSINO(field)
	{	
		if (field.value == "")
			return;		
		var re;   
		re = /,/g;
		var value = field.value.replace(re,'');
		if (isNaN(value))
		{
			GetAlertError(iTotal,DSAlert,"0013");
			field.value = "";
			field.focus();
			return;
		}
		//tanldt: Test yeu cau 1 so cho phai lon hon 0
		if (value < 1)
		{		
			//alert("Must not a negative numeric or zero!");
			GetAlertError(iTotal,DSAlert,"0012");
			field.value = "";
			field.focus();
			return;
		}
		//FormatNumericWithSeparate(field);
	}

	
	function enablePlace()
	{
		if (document.getElementById("_ctl0_chkChangePlace").checked)
		{
			document.getElementById("_ctl0_cboExaminingPlace").disabled = false;
			document.getElementById("_ctl0_cboExaminingPlace").value = "";
		}
		else
		{
			document.getElementById("_ctl0_cboExaminingPlace").disabled = true;
			document.getElementById("_ctl0_cboExaminingPlace").value = "";
		}
	}
	
	
	
	function GetValueEmpList(strEmpList)
	{
		document.getElementById("_ctl0_txtEmpList").value = strEmpList;
		document.getElementById("_ctl0_txtEmpList").focus();
	}
	
	function MessageComplete()
	{
		confirm("Complete!");
	}
	function OpenWindowEmps(strField)
	{
		var strUrl;

		strUrl = 'MdlPR/UploadTempPayslip.ascx';
			
		ShowDialog('FormPage.aspx?Ascx=' + strUrl)

	} 
</script>
<script>
	document.getElementById("_ctl0_txtEmpList").focus();
	ChangeCollectionType();
</script>
