<%@ Import Namespace ="iHRPCore.Com" %>
<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../Include/EmpHeaderSearch.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="PaySlip1.ascx.cs" Inherits="iHRPCore.MdlPR.PaySlip1" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="97%" border="0">
	<TR>
		<TD align="left" width="100%"><asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center"><uc1:empheadersearch id="EmpHeaderSearch1" runat="server" IsStatus="false"></uc1:empheadersearch></TD>
	</TR>
	<TR>
		<TD><asp:label id="Label35" CssClass="labelrequire" runat="server">Month</asp:label>&nbsp;
			<asp:textbox id="txtMonth" onblur="javascript:CheckMonthYear(this)" CssClass="input" runat="server"
				MaxLength="7" Width="100px"></asp:textbox>&nbsp;&nbsp;
		</TD>
	</TR>
	<TR>
		<TD align="center">&nbsp;
			<br>
		</TD>
	</TR>
	<TR>
		<TD align="center" height="10">&nbsp;
			<asp:linkbutton id="btnPrint" accessKey="P" runat="server" CssClass="btnPrint" ToolTip="ALT+P">Exports</asp:linkbutton>&nbsp;
			<asp:linkbutton id="btnSendMail" accessKey="E" runat="server" CssClass="btnSave" ToolTip="ALT+E">Send Mail</asp:linkbutton>&nbsp;
			<INPUT class="button" id="btnUpload" onclick="OpenWindowEmp('EmpID');" type="button" value="Upload Temp"
				name="btnView1">&nbsp;&nbsp;
		</TD>
	</TR>
	<TR>
		<TD align="center">&nbsp;
			<br>
		</TD>
	</TR>
	<TR style="DISPLAY: none">
		<TD align="center" height="10">
			<asp:RadioButtonList id="optIsNET" runat="server" Width="280px" RepeatDirection="Horizontal">
				<asp:ListItem Value="1" Selected="True">NET</asp:ListItem>
				<asp:ListItem Value="0">Gross</asp:ListItem>
			</asp:RadioButtonList>
			<asp:linkbutton id="btnView" accessKey="S" runat="server" CssClass="btnSave" ToolTip="ALT+S">View</asp:linkbutton>
			<INPUT class="button" id="btnView1" onclick="ViewTable();" type="button" value="View" name="btnView1">&nbsp;&nbsp;
			<INPUT class="button" id="cmdSendMail" onclick="CheckSendMail();" type="button" value="Send mail"
				name="btnExport"> &nbsp;&nbsp; <INPUT class="button" id="btnExport" onclick="CheckExport();" type="button" value="Export"
				name="btnExport">
		</TD>
	</TR>
	<TR style="DISPLAY: none">
		<TD style="HEIGHT: 18px" align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:label id="Label24" CssClass="label" runat="server">Unit = VND</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:label id="Label25" CssClass="label" runat="server" Font-Italic="True">Tax Advice</asp:label></TD>
	</TR>
	<TR>
		<TD align="center">
			<asp:label id="Label23" runat="server" CssClass="label" Font-Italic="True">Select file Template</asp:label>:
			<asp:DropDownList id="cboTemplate" runat="server"></asp:DropDownList>
		</TD>
	</TR>
	<TR>
		<TD align="center"></TD>
	</TR>
	<TR>
		<TD><SPAN id="Displays"></SPAN> <SPAN id="Loading"></SPAN>
		</TD>
	</TR>
	<TR style="DISPLAY: none">
		<TD align="center" height="5"><SPAN id="TotalRows"></SPAN></TD>
	</TR> <!-- end button for input form -->
	<TR id="trGrid" runat="server">
		<TD align="center"><!-- start grid for input form --> <!-- end grid for input form --></TD>
	</TR>
</TABLE>
<script language="javascript">
<!--
	
	function validform(){	
	
	if(document.getElementById('_ctl0_EmpHeaderSearch1_cboLevel1').value==""){
		GetAlertError(iTotal,DSAlert,"50021");
		document.getElementById('_ctl0_EmpHeaderSearch1_cboLevel1').focus();
		return false;
	}
	
	if(document.getElementById('_ctl0_txtMonth').value==""){
		GetAlertError(iTotal,DSAlert,"PS_0001");
		document.getElementById('_ctl0_txtMonth').focus();
		return false;
	}
	
	if(document.getElementById('_ctl0_cboTemplate').value==""){
		GetAlertError(iTotal,DSAlert,"50021");
		document.getElementById('_ctl0_cboTemplate').focus();
		return false;
	}
	
	return true;
}
function checkisnull(obj)
	{
		if(document.getElementById('_ctl0_' + obj).value=="")
			{
				GetAlertError(iTotal,DSAlert,"PS_0003");
				document.getElementById('_ctl0_' + obj).focus();
				return false;
			}
		else
			return true;
	}
function ViewTable() 
{
	if (validform() == true)
	{
		document.getElementById("Loading").innerHTML = "<IMG SRC='images/loading.gif'> Loading...";
		
		var cboCompany = document.getElementById('_ctl0_EmpHeaderSearch1_cboCompany').value;
		var cboLevel1 = document.getElementById('_ctl0_EmpHeaderSearch1_cboLevel1').value;
		var cboLevel2 = document.getElementById('_ctl0_EmpHeaderSearch1_cboLevel2').value;
		var cboLevel3 = document.getElementById('_ctl0_EmpHeaderSearch1_cboLevel3').value;
		var txtEmpID = document.getElementById('_ctl0_EmpHeaderSearch1_txtEmpID').value;
		var txtEmpName = document.getElementById('_ctl0_EmpHeaderSearch1_txtEmpName').value;
		
		var isNET = document.getElementById('_ctl0_optIsNET_0').value;
		var txtMonth = document.getElementById('_ctl0_txtMonth').value;
		var AcountLogin = document.getElementById('txtAcountLogin').value;

		PaySlip.GetData(txtMonth,cboCompany,cboLevel1,cboLevel2,cboLevel3,txtEmpID,txtEmpName,isNET,"EN",AcountLogin,ViewTable_CallBack);  
	}
}

function ViewTable_CallBack(response) 
{ 
	var dt = response.value; 
	
	if(dt!=null && typeof(dt) == "object" && dt!=null) 
	{
		var s = new Array();
		s[s.length] = "<table width='100%' cellspacing='1' cellpadding='1' bordercolor='#999999' border='1' style='border-color:#999999;font-family:Arial;border-collapse:collapse;'>";
		s[s.length] = "<tr class=gridHeader align=Center>"; 
		s[s.length] = "<td align=Center>No.</td><td>Emp Code</td><td >Full Name</td><td >Company</td>"; 
		s[s.length] = "<td>Email</td><td align=Center><INPUT type=\"checkbox\" id=\"chkCheckAll\" onclick=\"CheckAllGrid();\"></td>";
		s[s.length] = "<td>Status</td>"; 
		s[s.length] = "</tr>"; 
		for(var i=0;i<dt.Rows.length;i++) 
		{
			s[s.length] = "<tr>"; 
			s[s.length] = "<td class=GridItem align=Center>" + (i + 1) + "</td>";  
			s[s.length] = "<td style=\"display:none\"><INPUT type=\"text\" id=txtEmpID" + (i + 1) + " value=\"" + dt.Rows[i].EmpID + "\"></td>";  
			s[s.length] = "<td style=\"display:none\"><INPUT type=\"text\" id=txtMonth" + (i + 1) + " value=\"" + dt.Rows[i].Month + "\"></td>";  
			s[s.length] = "<td style=\"display:none\"><SPAN id=PaySlips" + (i + 1) + "></SPAN></td>";
			s[s.length] = "<td class=GridItem align=Center>" + dt.Rows[i].EmpCode + "</td>";  
			s[s.length] = "<td class=GridItem>" + dt.Rows[i].EmpName + "</td>"; 
			s[s.length] = "<td class=GridItem>" + dt.Rows[i].Level1+  "</td>"; 
			s[s.length] = "<td class=GridItem align=right><INPUT type=\"text\" onblur=\"CheckEmails(this)\" id=txtEmail" + (i + 1) + " value=\"" + dt.Rows[i].Email + "\"></td>"; 
			s[s.length] = "<td class=GridItem align=Center><INPUT type=\"checkbox\" id=\"chkCheck" + (i + 1) + "\"></td>"; 
			s[s.length] = "<td class=GridItem align=Center><SPAN id=Status" + (i + 1) + "></SPAN></td>";
			s[s.length] = "</tr>";  
		}
		s[s.length] = "</table>"; 
		document.getElementById("TotalRows").innerHTML = dt.Rows.length;
		document.getElementById("Loading").innerHTML = "";
		document.getElementById("Loading").innerHTML = s.join(""); 	 
	}	
	
	
}
function CheckEmails(email)
{
	//var email = va.value;
	
	var filter  = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
		if (!filter.test(email.value)) 
		{
			email.value = "";
			email.focus;
			GetAlertError(iTotal,DSAlert,"0058");
			return false;
		}
		
	return true;
}

function CheckAllGrid()
{
	var iTotalRows = document.getElementById("TotalRows").innerHTML;
	var check = document.getElementById("chkCheckAll").checked;
	for(var i=0;i<iTotalRows;i++) 
	{
		document.getElementById("chkCheck"+(i + 1)+"").checked = check;
	}
	return true;
}
function CheckSendMail()
{
	var iTotalRows = document.getElementById("TotalRows").innerHTML;
	var status = 0;
	if (iTotalRows != "")
	{
		for(var i=0;i<iTotalRows;i++) 
		{
		
			if (document.getElementById("chkCheck"+(i + 1)+"").checked)
			{
				if (document.getElementById("txtEmail"+(i + 1)+"").value == "")
				{
					document.getElementById("txtEmail"+(i + 1)+"").value = "";
					document.getElementById("txtEmail"+(i + 1)+"").focus;
					GetAlertError(iTotal,DSAlert,"0003");
					return false;
				}
				status = 1;
			}
		}
	}
	else
	{
		GetAlertError(iTotal,DSAlert,"0001");
		return false;
	}
	if (status == 0)
	{
		GetAlertError(iTotal,DSAlert,"0001");
		return false;
	}
	else
	{
		/* Da check xong, bat dau send mail*/
		document.getElementById("Displays").innerHTML = "<IMG SRC='images/loading.gif'> Sending...";
		for(var i=0;i<iTotalRows;i++) 
		{
			if (document.getElementById("chkCheck"+(i + 1)+"").checked)
			{
				var txtEmpID = document.getElementById("txtEmpID" + (i + 1) + "").value;
				var txtMonth = document.getElementById("txtMonth" + (i + 1) + "").value;
				var txtEmail = document.getElementById("txtEmail" + (i + 1) + "").value;
				var AcountLogin = document.getElementById('txtAcountLogin').value;
				var isNET = document.getElementById('_ctl0_optIsNET_0').value;
				var sIsNET = "Gross";
				if (isNET == 1)
					sIsNET = "NET";
				var svalue = document.getElementById("PaySlips" + (i + 1) + "").innerHTML;
				
				if (svalue == "")
				{
					
					svalue = PaySlip.GetPaySlipForEmp(txtEmpID,txtMonth,"EN",AcountLogin,sIsNET).value;  
				}
				
				if (svalue !="Error")
				{
					document.getElementById("PaySlips" + (i + 1)).innerHTML = svalue;
					svalue = PaySlip.SendmailForEmp(txtEmpID,txtEmail).value;
				}
				if (svalue =="Error")
				{
					document.getElementById("Status" + (i + 1)).innerHTML = "Error";
				}
				else
				{
					document.getElementById("Status" + (i + 1)).innerHTML = "Send successful";
				}
				
			}
		}
		document.getElementById("Displays").innerHTML = "";
	}
	return true;
}
function CheckExport()
{
	var iTotalRows = document.getElementById("TotalRows").innerHTML;
	var txtEmpID = "@";
	
	if (iTotalRows != "")
	{
		for(var i=0;i<iTotalRows;i++) 
		{
			if (document.getElementById("chkCheck"+(i + 1)+"").checked)
			{
				txtEmpID += document.getElementById("txtEmpID" + (i + 1) + "").value+ "@";
			}
		}
	}
	else
	{
		GetAlertError(iTotal,DSAlert,"0001");
		return false;
	}
	
	if (txtEmpID == "")
	{
		GetAlertError(iTotal,DSAlert,"0001");
		return false;
	}
	else
	{
		/* Da check xong, bat dau send mail*/
		document.getElementById("Displays").innerHTML = "<IMG SRC='images/loading.gif'> Exporting...";
		document.getElementById("_ctl0_Export").value = "";
		var sExport = "";
		var AcountLogin = document.getElementById('txtAcountLogin').value;
		var txtMonth = document.getElementById("_ctl0_txtMonth").value;
		var isNET = document.getElementById('_ctl0_optIsNET_0').value;
		var sIsNET = "Gross";
		if (isNET == 1)
			sIsNET = "NET";
		
		sExport = PaySlip.GetPaySlipForListEmp(txtEmpID,txtMonth,"EN",AcountLogin,sIsNET).value;  
		document.getElementById("_ctl0_Export").value = sExport;
		//document.getElementById("_ctl0_txtEx").value = sExport;
		document.getElementById("_ctl0_btnPrint").click();
		document.getElementById("Displays").innerHTML = "";
	}
	return true;
}

//-->
</script>
<SCRIPT language="javascript">
	function OpenWindowEmp(strField)
	{
		var strUrl;

		strUrl = 'MdlPR/UploadTempPayslip.ascx';
			
		ShowDialog('FormPage.aspx?Ascx=' + strUrl)

	} 
	
</SCRIPT>
<INPUT id="Export" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" runat="server"
	NAME="Export">
<asp:TextBox id="txtEx" Visible="False" runat="server" Width="100%" TextMode="MultiLine" Height="400px"></asp:TextBox>
