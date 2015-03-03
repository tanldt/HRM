<%@ Import Namespace ="iHRPCore.HRComponent"%>
<%@ Import Namespace ="System.Data.SqlClient" %>
<%@ Import Namespace ="System.Data" %>
<%@ Import Namespace ="iHRPCore.Com"%>
<%@ Register TagPrefix="uc" TagName="EmpHeader" Src="../Include/EmpHeader.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Termination.ascx.cs" Inherits="iHRPCore.MdlHR.Termination" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="98%" border="0">
	<TR>
		<TD align="center"><asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center"><uc:empheader id="HR_EmpHeader" runat="server"></uc:empheader></TD>
	</TR>
	<TR>
		<TD align="center"><!-- start detail for input form -->
			<TABLE id="Table2" cellSpacing="0" cellPadding="1" width="100%" border="0">
				<TR>
					<TD width="3%"></TD>
					<TD width="20%"></TD>
					<TD width="27%"></TD>
					<TD width="3%"></TD>
					<TD width="20%"></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD><asp:label id="lblTerminationTypeID" runat="server" CssClass="labelRequire"> Resign code</asp:label></TD>
					<TD><asp:dropdownlist id="cboLSTerminationTypeID" runat="server" CssClass="combo" Width="100%"></asp:dropdownlist></TD>
					<TD></TD>
					<TD><asp:label id="lblReason" runat="server" CssClass="label"> Reason</asp:label></TD>
					<TD><asp:textbox id="txtReason" runat="server" CssClass="input" Width="100%" MaxLength="70"></asp:textbox></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD><asp:label id="lblNotificationDate" runat="server" CssClass="label">Notification date</asp:label></TD>
					<TD><asp:textbox id="txtInformDate" onblur="JavaScript:CheckDate(this)" runat="server" CssClass="input"
							Width="70px" MaxLength="10"></asp:textbox><INPUT class=btnCal id=btnCal onclick="javascript:popUpCalendar(<%= this.txtInformDate.ClientID %>)" type=button name=btnCalFromDate></TD>
					<TD></TD>
					<TD><asp:label id="lblLastWorkingDate" runat="server" CssClass="labelRequire">Last working date</asp:label></TD>
					<TD><asp:textbox id="txtLastWorkingDate" onblur="javascript:CheckLastDate(this)" runat="server" CssClass="input"
							Width="70px" MaxLength="10"></asp:textbox><INPUT class=btnCal id=Button3 onclick="javascript:popUpCalendar(<%= this.txtLastWorkingDate.ClientID %>)" type=button name=btnCalFromDate></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD><asp:label id="lblLastSalDate" runat="server" CssClass="labelRequire">Last Salary Date</asp:label></TD>
					<TD><asp:textbox id="txtLastSalDate" onblur="JavaScript:CheckDate(this)" runat="server" CssClass="input"
							Width="70px" MaxLength="10"></asp:textbox><INPUT class=btnCal id=Button1 onclick="javascript:popUpCalendar(<%= this.txtLastSalDate.ClientID %>)" type=button name=btnCalFromDate></TD>
					<TD></TD>
					<TD><asp:label id="lblInsuranceInfoDate" runat="server" CssClass="labelRequire">Insurance Info Date</asp:label></TD>
					<TD><asp:textbox id="txtInsuranceInfoDate" onblur="JavaScript:CheckDate(this)" runat="server" CssClass="input"
							Width="70px" MaxLength="10"></asp:textbox><INPUT class=btnCal id=Button2 onclick="javascript:popUpCalendar(<%= this.txtInsuranceInfoDate.ClientID %>)" type=button name=btnCalFromDate></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD><asp:label id="lblDecision" runat="server" CssClass="label"> Decision No</asp:label></TD>
					<TD><asp:textbox id="txtDecisionNo" runat="server" CssClass="input" Width="100%" MaxLength="20"></asp:textbox></TD>
					<TD></TD>
					<TD><asp:label id="lblSignDate" runat="server" CssClass="label">Sign date</asp:label></TD>
					<TD><asp:textbox id="txtSignDate" onblur="JavaScript:CheckDate(this)" runat="server" CssClass="input"
							Width="70px" MaxLength="10"></asp:textbox><INPUT class=btnCal id=Button4 onclick="javascript:popUpCalendar(<%= this.txtSignDate.ClientID %>)" type=button name=btnCalFromDate></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD><asp:label id="lblSigner" runat="server" CssClass="label"> Signer</asp:label></TD>
					<TD><asp:textbox id="txtSigner" runat="server" CssClass="input" Width="100%" MaxLength="255"></asp:textbox></TD>
					<TD></TD>
					<TD><asp:label id="lblSignerPosition" runat="server" CssClass="label"> Job Title of Signer</asp:label></TD>
					<TD><asp:textbox id="txtSignerPosition" runat="server" CssClass="input" Width="100%" MaxLength="255"></asp:textbox></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD>
						<asp:label id="Label2" CssClass="label" runat="server">Return card date</asp:label></TD>
					<TD>
						<asp:textbox id="txtReturnInsCardDate" onblur="JavaScript:CheckDate(this)" CssClass="input" runat="server"
							Width="70px" MaxLength="10"></asp:textbox><INPUT class=btnCal id=Button5 onclick="javascript:popUpCalendar(<%= this.txtReturnInsCardDate.ClientID %>)" type=button name=btnCalFromDate></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD><asp:label id="lblNote" runat="server" CssClass="label">Note</asp:label></TD>
					<TD colSpan="4"><asp:textbox id="txtNote" runat="server" CssClass="input" Width="100%" MaxLength="255"></asp:textbox><asp:textbox id="txtCreater" style="DISPLAY: none" runat="server"></asp:textbox><asp:textbox id="txtActive" style="DISPLAY: none" runat="server"></asp:textbox><asp:textbox id="txtIsReInstate" style="DISPLAY: none" runat="server"></asp:textbox><input id="txtTerminationID" type="hidden" name="txtTerminationID" runat="server">
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center" colSpan="6" height="20"><HR align="center" width="100%">
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 21px" align="center" colSpan="6">&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnSave" accessKey="S" runat="server" CssClass="btnSave" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnDelete" accessKey="D" runat="server" CssClass="btnExport" ToolTip="Alt + D">Delete</asp:linkbutton>&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnList" accessKey="L" runat="server" CssClass="btnList" ToolTip="Alt + L">List</asp:linkbutton>&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnTurnOver" accessKey="R" runat="server" CssClass="btnExport" ToolTip="Alt + R">Reinstate</asp:linkbutton>&nbsp;
						<asp:linkbutton accessKey="R" style="Z-INDEX: 0" id="Linkbutton1" CssClass="btnExport" runat="server"
							ToolTip="Alt + R">Cal F.Payment</asp:linkbutton>&nbsp;</TD>
				</TR>
				<TR id="trTurnOver" style="DISPLAY: none" runat="server">
					<TD vAlign="top" align="center" colSpan="6" height="20"><HR align="center" width="100%">
						<TABLE id="Table3" cellSpacing="0" cellPadding="1" width="100%" border="0">
							<TR>
								<TD width="15%"><asp:checkbox id="chkKeepEmpID" runat="server" CssClass="checkbox" Text="KeepOldID" checked="true"></asp:checkbox></TD>
								<TD width="15%"></TD>
								<TD width="15%"><asp:label id="Label1" runat="server" CssClass="label">Start date</asp:label></TD>
								<TD width="25%"><asp:textbox id="txtDateReInstate" onblur="JavaScript:CheckDate(this)" runat="server" CssClass="input"
										Width="80%"></asp:textbox>&nbsp;<INPUT class=btnCal id=Button7 onclick="javascript:popUpCalendar(<%= this.txtDateReInstate.ClientID %>)" type=button name=btnCalFromDate></TD>
								<td width="7%"></td>
								<TD width="15%"><asp:linkbutton id="btnSaveTurnOver" runat="server" CssClass="btnExport"> Reinstate</asp:linkbutton></TD>
							</TR>
							<TR>
								<TD vAlign="top" align="center" colSpan="7" height="20">
									<HR align="center" width="100%">
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<tr style="DISPLAY:none">
					<td><asp:textbox id="txtFromDateMax" runat="server" CssClass="input" Width="80%"></asp:textbox></td>
				</tr>
			</TABLE>
			&nbsp;&nbsp;&nbsp;
		</TD>
	</TR>
</TABLE> <!-- end detail for input form -->
<script language="javascript">
function CheckValidLastWorkingDate(field)
{
	CheckDate(field);
}
function CheckDelete()
{
		if(confirm(GetAlertText(iTotal,DSAlert,"TER_0005"))==false){
		return false;
	}
}
function Reinstate()
{

	if (document.getElementById("_ctl0_txtActive").value == "0")
	{
		//alert('Nhân viên này chưa nghỉ việc');		
		GetAlertError(iTotal,DSAlert,"TER_0004");
		return false;
	}
	if (document.getElementById("_ctl0_trTurnOver").style.display == "none")
		document.getElementById("_ctl0_txtIsReInstate").value = "0";
	else
		document.getElementById("_ctl0_txtIsReInstate").value = "1";
		
		DisplayReInstate();		
	return false;
}
function DisplayReInstate()
	{
		if (document.getElementById("_ctl0_txtIsReInstate").value == "0")
			document.getElementById("_ctl0_trTurnOver").style.display = "";
		else
			document.getElementById("_ctl0_trTurnOver").style.display = "none";
	}
function CheckSave()
{
	//Loai nghỉ việc không được bỏ trống
	if(document.getElementById("_ctl0_cboLSTerminationTypeID").selectedIndex < 1)
	{
		//alert('Bạn chưa chọn Loại nghỉ việc.');
		GetAlertError(iTotal,DSAlert,"0003");
		document.getElementById("_ctl0_cboLSTerminationTypeID").focus();
		return false;
	}
	//Ngày làm việc cuối cùng không được bỏ trống
	if(document.getElementById("_ctl0_txtLastWorkingDate").value == "")
	{
		//alert('Bạn chưa chọn Ngày làm việc cuối cùng.');
		GetAlertError(iTotal,DSAlert,"0003");
		document.getElementById("_ctl0_txtLastWorkingDate").focus();
		return false;
	}
	var obj=document.getElementById('_ctl0_txtLastSalDate');
	if(obj.value=='')
		{
			//alert('Xin nhập ngày lãnh lương cuối!');
			GetAlertError(iTotal,DSAlert,"0003");
			obj.focus();
			return false;
		}
	//Ngày báo bảo hiểm không được bỏ trống
	if(document.getElementById("_ctl0_txtInsuranceInfoDate").value == "")
	{
		//alert('Bạn chưa chọn Ngày làm việc cuối cùng.');
		GetAlertError(iTotal,DSAlert,"0003");
		document.getElementById("_ctl0_txtLastWorkingDate").focus();
		return false;
	}	
	obj=document.getElementById('_ctl0_txtLastWorkingDate');
	var objInform=document.getElementById('_ctl0_txtInformDate');
	
	if(objInform.value!='' && obj.value!='')
	{
		if(FromSmallToDate(objInform, obj)==false)
		{
			//alert('Ngày làm việc cuối phải sau ngày báo nghỉ!');			
			GetAlertError(iTotal,DSAlert,"TER_0001");
			document.getElementById("_ctl0_txtInformDate").focus();
			return false;
		}
	}	
	obj=document.getElementById('_ctl0_txtInsuranceInfoDate');
	objInform=document.getElementById('_ctl0_txtLastWorkingDate');
	
	if(objInform.value!='' && obj.value!='')
	{
		if(FromSmallToDate(objInform,obj)==false)
		{
			//alert('Ngày báo bảo hiểm phải sau ngày làm việc cuối!');
			GetAlertError(iTotal,DSAlert,"TER_0010");
			document.getElementById("_ctl0_txtInsuranceInfoDate").focus();
			return false;
		}
	}	
	// kiem tra ngay last working date phai lon hon ngay hieu luc cuoi cung cua working record
	obj=document.getElementById('_ctl0_txtLastWorkingDate');
	objInform=document.getElementById('_ctl0_txtFromDateMax');	
	if(obj.value!='')
	{
		if(FromSmallToDate(objInform,obj)==false)
		{
			//alert('Last working date phải sau ngày hieu luc cuối!');
			GetAlertError(iTotal,DSAlert,"TER_006");
			document.getElementById("_ctl0_txtLastWorkingDate").focus();
			return false;
		}
	}		
	return true;
}
function OpenEditCode()
{		
	window.open('FormPage.aspx?ModuleID=HR&ParentID=8&FunctionID=204&Ascx=MdlHR/BuildEmpCode.ascx&action=edit&empid=' + document.getElementById("_ctl0_HR_EmpHeader_txtEmpID").value, 'BuildCode', 'width=520,height=200,left=300,top=200,dependent');		
	return false;
}
function SaveTurnOver()
{	
	if (document.getElementById('_ctl0_chkKeepEmpID').checked==1)
	{
		if(document.getElementById("_ctl0_txtDateReInstate").value == "")
		{			
			GetAlertError(iTotal,DSAlert,"TER_0008");
			document.getElementById("_ctl0_txtDateReInstate").focus();
			return false;
		}
	}
		if(confirm(GetAlertText(iTotal,DSAlert,"TER_0009"))==false){
		return false;
	}
	return true;
}
function collapseTer()
{
	if (document.getElementById('_ctl0_chkTerAllowance').checked ==false)
		document.getElementById('_ctl0_trTer').style.display="none";		
	else
		document.getElementById('_ctl0_trTer').style.display="block";
}
function collapseJobTitle()
{
	if (document.getElementById('_ctl0_chkJobTitleAllowance').checked ==false)
		document.getElementById('_ctl0_trJobTitle').style.display="none";		
	else
		document.getElementById('_ctl0_trJobTitle').style.display="block";
}
function collapseBeHave()
{
	if (document.getElementById('_ctl0_chkBehaveAllowance').checked ==false)
		document.getElementById('_ctl0_trBeHave').style.display="none";		
	else
		document.getElementById('_ctl0_trBeHave').style.display="block";
}
function collapseSenior()
{
	if (document.getElementById('_ctl0_chkSeniorAllowance').checked ==false)
		document.getElementById('_ctl0_trSenior').style.display="none";		
	else
		document.getElementById('_ctl0_trSenior').style.display="block";
}
function collapseOther()
{
	if (document.getElementById('_ctl0_chkC47Report').checked ==false)
		document.getElementById('_ctl0_trOther').style.display="none";		
	else
		document.getElementById('_ctl0_trOther').style.display="block";
}
function CheckLastDate(obj)
{
	var date="";
	CheckDate(obj);
	if(obj.value=="")
		document.getElementById('_ctl0_txtInsuranceInfoDate').value = "";
	else		
	{	
		date = Toddmmyyyy_value(obj.value);		
		document.getElementById('_ctl0_txtInsuranceInfoDate').value = AddDaystoDate(date,1);		
		ToddMMMyyyy(document.getElementById('_ctl0_txtInsuranceInfoDate'));
	}
}
function OpenNewWindow()
{
	objShowDialog = window.open("MdlTER/Empinfo.aspx" ,'Recipient','status=1,toolbar=0,scrollbars=1,resizable=1,alwaysRaised=Yes, top=5, left=10, width=500, height=300,1 ,align=center')	
	objShowDialog.focus();
	return false;
}
</script>
