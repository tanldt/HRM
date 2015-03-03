<%@ Register TagPrefix="uc" TagName="EmpHeader" Src="../Include/EmpHeader.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="PersonalRecord.ascx.cs" Inherits="iHRPCore.MdlHR.PersonalRecord" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Import Namespace ="System.Data.SqlClient" %>
<%@ Import Namespace ="System.Data" %>
<%@ Import Namespace ="iHRPCore.Com"%>
<TABLE id="Tablemail" cellSpacing="0" cellPadding="0" width="97%" border="0">
	<TR>
		<TD><asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center"><uc:empheader id="HR_EmpHeader" runat="server"></uc:empheader></TD>
	</TR>
	<TR>
		<TD align="center">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR vAlign="top">
					<TD width="17%"><asp:label id="lblStatus" runat="server" CssClass="label" Width="100%">Status change</asp:label></TD>
					<TD width="30%"><asp:dropdownlist id="cboLSStatusChangeID" runat="server" CssClass="combo" Width="100%"></asp:dropdownlist></TD>
					<TD width="2%"></TD>
					<TD style="WIDTH: 135px" width="135"><asp:label id="lblLockerNum" runat="server" CssClass="label" Width="100%">Card No</asp:label></TD>
					<TD noWrap width="37%"><asp:textbox id="txtScanCode" runat="server" CssClass="input" Width="75px" MaxLength="15"></asp:textbox>&nbsp;&nbsp;&nbsp;</TD>
				</TR>
				<TR style="DISPLAY: none">
					<TD width="17%"><asp:label id="Label2" runat="server" CssClass="label" Width="100%">Lọai chi phí</asp:label></TD>
					<TD width="30%"><asp:dropdownlist id="cboLSExpenseID" runat="server" CssClass="combo" Width="100%"></asp:dropdownlist></TD>
					<TD width="2%"></TD>
					<TD style="WIDTH: 135px" width="135"></TD>
					<TD noWrap width="37%"><asp:checkbox id="chkIsScan" CssClass="checkbox" Runat="server" TextAlign="Left" Text="CC tay?"></asp:checkbox></TD>
				</TR>
				<TR style="DISPLAY: none">
					<TD width="17%"><asp:label id="Label1" runat="server" CssClass="label" Width="96px"> T/cấp tiền ăn </asp:label></TD>
					<TD width="30%"><asp:radiobuttonlist id="optBoardingFeeRank" runat="server" Width="232px" RepeatDirection="Horizontal">
							<asp:ListItem Value="0" Selected="True">Kho&#225;n việc</asp:ListItem>
							<asp:ListItem Value="1">Mức 1</asp:ListItem>
							<asp:ListItem Value="2">Mức 2</asp:ListItem>
						</asp:radiobuttonlist></TD>
					<TD width="2%"></TD>
					<TD style="WIDTH: 135px" width="135"></TD>
					<TD noWrap width="37%"></TD>
				</TR>
				<TR vAlign="top">
					<TD style="HEIGHT: 17px" width="17%"><asp:label id="lblOldEmpCode" runat="server" CssClass="label" Width="100%">Old Emp Code</asp:label></TD>
					<TD style="HEIGHT: 17px" width="30%"><asp:label id="lblOldEmpCodeData" runat="server" CssClass="labelData" Width="100%"></asp:label></TD>
					<TD style="HEIGHT: 17px" width="4%"></TD>
					<TD style="WIDTH: 135px; HEIGHT: 17px" width="135"><asp:label id="lblStartDate" runat="server" CssClass="label" Width="100%">Joining Date</asp:label></TD>
					<TD style="HEIGHT: 17px"><asp:textbox id="txtStartDate" onblur="JavaScript:CheckDate(this)" runat="server" CssClass="input"
							Width="75px" MaxLength="10"></asp:textbox><INPUT class=btnCal onclick="javascript:popUpCalendar(<%= this.txtStartDate.ClientID %>);" type=button></TD>
				</TR>
				<TR id="trAdLevelGrade" vAlign="top" runat="server">
					<TD style="HEIGHT: 17px" width="17%"><asp:checkbox id="chkAdLevelGrade" CssClass="chkAdmin" Runat="server"></asp:checkbox><asp:label id="lblJobClass" runat="server" CssClass="label" Width="70%">Job class</asp:label></TD>
					<TD style="HEIGHT: 17px" width="30%"><asp:dropdownlist id="cboLSJobClassID" runat="server" CssClass="combo" Width="100%"></asp:dropdownlist></TD>
					<TD style="HEIGHT: 17px" width="4%"></TD>
					<TD style="WIDTH: 135px; HEIGHT: 17px" width="135"><asp:label id="lblWorkType" runat="server" CssClass="label" Width="100%">Work type</asp:label></TD>
					<TD style="HEIGHT: 17px" noWrap width="35%"><asp:dropdownlist id="cboLSWorkTypeID" runat="server" CssClass="combo" Width="100%"></asp:dropdownlist></TD>
				</TR>
				<TR vAlign="top">
					<TD><asp:label id="lblCompany" runat="server" CssClass="label" Width="100%">Cus.Group</asp:label></TD>
					<TD noWrap><asp:label id="lblCompanyData" runat="server" CssClass="labelData" Width="200px"></asp:label></TD>
					<TD></TD>
					<TD style="WIDTH: 135px"><asp:label id="lblDivision" runat="server" CssClass="label" Width="100%">Company</asp:label></TD>
					<TD noWrap><asp:label id="lblDivisionData" runat="server" CssClass="labelData" Width="200px"></asp:label></TD>
				</TR>
				<TR vAlign="top">
					<TD width="17%"><asp:label id="lblDepartment" runat="server" CssClass="label" Width="100%">Department</asp:label></TD>
					<TD noWrap width="200"><asp:label id="lblDepartmentData" runat="server" CssClass="labelData" Width="200px"></asp:label></TD>
					<TD width="2%"></TD>
					<TD style="WIDTH: 135px" width="135"><asp:label id="lblSection" runat="server" CssClass="label" Width="100%">Group</asp:label></TD>
					<TD noWrap><asp:label id="lblSectionData" runat="server" CssClass="labelData" Width="200px"></asp:label></TD>
				</TR>
				<TR vAlign="top">
					<TD style="HEIGHT: 1px"><asp:label id="lblLocation" runat="server" CssClass="label" Width="100%">Location</asp:label></TD>
					<TD style="HEIGHT: 1px"><asp:label id="lblLocationData" runat="server" CssClass="labelData"></asp:label></TD>
					<TD style="HEIGHT: 1px"></TD>
					<TD style="WIDTH: 135px; HEIGHT: 1px"><asp:label id="lblPosition" runat="server" CssClass="label" Width="100%">Post Title</asp:label></TD>
					<TD style="HEIGHT: 1px"><asp:label id="lblPositionData" runat="server" CssClass="labelData" Width="200px"></asp:label></TD>
				</TR>
				<TR vAlign="top">
					<TD><asp:label id="lblJobCode" runat="server" CssClass="label" Width="100%">Post Catagory</asp:label></TD>
					<TD><asp:label id="lblJobCodeData" runat="server" CssClass="labelData" Width="200px"></asp:label></TD>
					<TD></TD>
					<TD style="WIDTH: 135px"><asp:label id="lblEmpType" runat="server" CssClass="label" Width="100%">Nature of Emp</asp:label></TD>
					<TD><asp:label id="lblEmpTypeData" runat="server" CssClass="labelData" Width="200px"></asp:label></TD>
				</TR>
				<TR style="DISPLAY: none">
					<TD style="HEIGHT: 19px"><asp:label id="lblGrade" runat="server" CssClass="label" Width="100%">Grade</asp:label></TD>
					<TD style="HEIGHT: 19px"><asp:label id="lblGradeData" runat="server" CssClass="labelData" Width="200px"></asp:label></TD>
					<TD style="HEIGHT: 19px">AA</TD>
					<TD style="WIDTH: 135px; HEIGHT: 19px"></TD>
					<TD style="HEIGHT: 19px"></TD>
				</TR>
				<TR style="DISPLAY: none">
					<TD style="HEIGHT: 19px"><asp:label id="lblRank" runat="server" CssClass="label" Width="100%">Rank</asp:label></TD>
					<TD style="HEIGHT: 19px"><asp:label id="lblRankData" runat="server" CssClass="labelData" Width="200px"></asp:label></TD>
					<TD style="HEIGHT: 19px">AA</TD>
					<TD style="WIDTH: 135px; HEIGHT: 19px"><asp:label id="lblJobTitle" runat="server" CssClass="label" Width="100%">Job title</asp:label></TD>
					<TD style="HEIGHT: 19px"><asp:label id="lblJobTitleData" runat="server" CssClass="labelData" Width="200px"></asp:label></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD colSpan="5">
			<HR width="100%">
		</TD>
	</TR>
	<TR style="DISPLAY: none">
		<TD>
			<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="100%" border="0">
				<TR>
					<TD width="17%"><asp:label id="llblGradeSal" runat="server" CssClass="label" Width="100%">Grade of Sal</asp:label></TD>
					<TD width="30%"><asp:label id="lblGradeOfSalData" runat="server" CssClass="labelData" Width="100%"></asp:label></TD>
					<TD width="4%"></TD>
					<TD width="17%"></TD>
					<TD width="35%"></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblRankSal" runat="server" CssClass="label" Width="100%">Rank of Sal </asp:label></TD>
					<TD><asp:label id="lblRankOfSalData" runat="server" CssClass="labelData" Width="100%"></asp:label></TD>
					<TD></TD>
					<TD><asp:label id="lblSalCoef" runat="server" CssClass="label" Width="100%">Salary Coef</asp:label></TD>
					<TD><asp:label id="lblSalaryCoefData" runat="server" CssClass="labelData" Width="100%"></asp:label></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblSalarylbl" runat="server" CssClass="label" Width="100%">Salary </asp:label></TD>
					<TD><asp:label id="lblSalary" runat="server" CssClass="labelData" Width="100%"></asp:label></TD>
					<TD></TD>
					<TD><asp:label id="lblSalaryOther" runat="server" CssClass="label" Width="100%">Salary Other</asp:label></TD>
					<TD><asp:label id="lblSalaryOtherData" runat="server" CssClass="labelData" Width="100%"></asp:label></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR style="DISPLAY: none">
		<TD>
			<HR width="100%">
			<INPUT id="txtRankID" style="WIDTH: 9px" type="hidden" size="1" name="txtRankID" runat="server">
			<asp:textbox id="txtEmpID_ReportTo" style="DISPLAY: none" runat="server"></asp:textbox></TD>
	</TR>
	<TR>
		<TD align="center">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD><asp:label id="Label5" runat="server" CssClass="label" Width="100%">Supervisor</asp:label></TD>
					<TD colSpan="4"><asp:textbox id="txtEmpCode_ReportTo" runat="server" CssClass="input" Width="80px" MaxLength="12"
							ReadOnly="True"></asp:textbox>&nbsp;<INPUT class="search" id="btnReportTo" onclick="javascript:OpenWindowReportTo3()" type="button"
							value="...">&nbsp;
						<asp:textbox id="txtEmpIDReportToName" runat="server" CssClass="input" Width="168px" MaxLength="15"
							ReadOnly="True" BorderColor="Transparent" BackColor="Transparent"></asp:textbox></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblReportTo" runat="server" CssClass="label" Width="100%">Line Manager</asp:label></TD>
					<TD colSpan="4"><asp:textbox id="txtDeptHeadName" runat="server" CssClass="inputReadonly" Width="50%" MaxLength="15"
							ReadOnly="True"></asp:textbox></TD>
				</TR>
				<TR>
					<TD width="17%"><asp:label id="lblEmail" runat="server" CssClass="label" Width="100%">Email</asp:label></TD>
					<TD width="30%"><asp:textbox id="txtEmailComp" onblur="CheckValidEmail(this)" runat="server" CssClass="input"
							Width="100%" MaxLength="30"></asp:textbox></TD>
					<TD width="4%"></TD>
					<TD width="17%"><asp:label id="lblTelephone" runat="server" CssClass="label" Width="100%">Telephone</asp:label></TD>
					<TD width="35%"><asp:textbox id="txtTelephoneComp" runat="server" CssClass="input" Width="100%" MaxLength="30"></asp:textbox></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 19px" width="17%"><asp:label id="Label6" runat="server" CssClass="label" Width="100%">Job Description</asp:label></TD>
					<TD style="HEIGHT: 19px" width="30%" colSpan="4"><asp:textbox id="txtJobDescription" runat="server" CssClass="input" Width="100%" MaxLength="30"></asp:textbox></TD>
				</TR>
				<TR>
					<TD width="17%" colSpan="5">
						<HR width="100%">
					</TD>
				</TR>
				<TR>
					<TD width="17%"></TD>
					<TD width="30%"><asp:checkbox id="chkSendMail" runat="server" Text="HR review"></asp:checkbox></TD>
					<TD width="4%"></TD>
					<TD width="17%"><asp:label id="Label3" runat="server" CssClass="label" Width="100%">Current</asp:label></TD>
					<TD width="35%"><asp:label id="lblCurrent" runat="server" CssClass="label" Width="100%"></asp:label></TD>
				</TR>
				<TR>
					<TD width="17%" colSpan="5">
						<HR width="100%">
					</TD>
				</TR>
				<TR>
					<TD width="17%"></TD>
					<TD width="30%"><asp:checkbox id="chkPayroll" runat="server" Text="Finance verify"></asp:checkbox></TD>
					<TD width="4%"></TD>
					<TD width="17%"><asp:label id="Label4" runat="server" CssClass="label" Width="100%">Current</asp:label></TD>
					<TD width="35%"><asp:label id="lblPayRoll" runat="server" CssClass="label" Width="100%"></asp:label></TD>
				</TR>
				<TR>
					<TD width="17%"></TD>
					<TD width="30%"></TD>
					<TD width="4%"></TD>
					<TD width="17%" colSpan="2"></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD>
			<HR width="100%">
		</TD>
	</TR>
	<TR>
		<TD align="center">&nbsp;&nbsp;
			<asp:linkbutton id="btnSave" accessKey="S" runat="server" CssClass="btnsave" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp;&nbsp;
			<asp:linkbutton id="btnList" accessKey="l" runat="server" CssClass="btnlist" ToolTip="ALT+L"> List</asp:linkbutton></TD>
	</TR>
</TABLE>
<SCRIPT language="javascript">
	function checkEmail() 
	{
		var email = document.getElementById("_ctl0_txtEmailComp");
		var filter  = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
		if (!filter.test(email.value)) 
		{
			document.getElementById("_ctl0_txtEmailComp").focus;
			GetAlertError(iTotal,DSAlert,"0058");
			return false;
		}
	}
	function ReturnEmpPopUp3(strEmpID, strEmpCode, strEmpName)
	{
		document.getElementById("_ctl0_txtEmpIDReportToName").value = strEmpName;
		document.getElementById("_ctl0_txtEmpID_ReportTo").value = strEmpID;
		document.getElementById("_ctl0_txtEmpCode_ReportTo").value = strEmpCode;
	}
	function OpenWindowReportTo3(strField)
	{
		ShowDialog('FormPage.aspx?Ascx=MdlHR/EmpPopup.ascx&Params=3');
	}
	//vuonglm
	function ReturnEmpPopUp4(strEmpID, strEmpCode, strEmpName)
	{
		document.getElementById("_ctl0_Textbox1").value = strEmpName;
		document.getElementById("_ctl0_Textbox2").value = strEmpID;
	}
	function OpenWindowReportTo4(strField)
	{
		ShowDialog('FormPage.aspx?Ascx=MdlHR/EmpPopup.ascx&Params=4');
	}
	//vuonglm
	/*function OpenBuildCode()
	{
		window.open('MdlHR/BuildEmpCode.aspx?empid=' + document.getElementById("txtEmpID"), 'BuildCode', 'width=320,height=200,left=300,top=200,dependent');
	}*/
	function validform(){
	
		if((document.getElementById('_ctl0_txtStartDate').value!=document.getElementById('_ctl0_txtStartDate').title) )
		{
			if(confirm(GetAlertText(iTotal,DSAlert,"PRE_0001"))==false)
			{
				return false;
			}
		}		
		return true;
	}
</SCRIPT>
