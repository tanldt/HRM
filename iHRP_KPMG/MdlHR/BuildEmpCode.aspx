<%@ Page language="c#" Codebehind="BuildEmpCode.aspx.cs" AutoEventWireup="false" Inherits="iHRPCore.MdlHR.BuildEmpCode" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BuildEmpCode</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="../Include/myStyles.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../Include/common.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="300" border="0">
				<TR>
					<TD colSpan="2"><asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD width="30%"><asp:label id="lblLevel1ID" CssClass="label" runat="server" Width="100%">Company</asp:label></TD>
					<TD width="70%"><asp:dropdownlist id="cboLSCompanyCode" CssClass="select" runat="server" Width="100%"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblJoinDate" CssClass="label" runat="server" Width="100%">Start Date</asp:label></TD>
					<TD><asp:textbox id="txtStartDate" CssClass="input" onblur="JavaScript:CheckDate(this)" runat="server"
							Width="76" MaxLength="10"></asp:textbox>&nbsp;<INPUT class="btnCal" onclick="ShowCalendar('txtStartDate')" type="button"></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblEmpCode" CssClass="label" runat="server" Width="100%">Emp Code</asp:label></TD>
					<TD><asp:textbox id="txtEmpCode" CssClass="input" runat="server" Width="76px" MaxLength="10" ReadOnly="True"></asp:textbox></TD>
				</TR>
				<TR>
					<TD colSpan="2">
						<HR width="97%">
					</TD>
				</TR>
				<TR>
					<TD align="center" colSpan="5"><asp:linkbutton id="btnBuild" accessKey="B" CssClass="btnaddnew" runat="server" ToolTip="ALT+B">Build Code</asp:linkbutton>&nbsp;&nbsp;
						<asp:linkbutton id="btnSave" accessKey="A" CssClass="btnsave" runat="server" ToolTip="ALT+A">Accept</asp:linkbutton></TD>
				</TR>
			</TABLE>
		</form>
		<script language="javascript">
<!--
function checkaccept(){
	if(document.getElementById('txtEmpCode').value=="")
		{
			alert('Employee code not left blank!');
			document.getElementById('txtEmpCode').focus();
			return false;
		}
	return true;	
}
function validform(){
	if(checkisnull('cboLSCompanyCode')==false)  return false;
	if(checkisnull('txtStartDate')==false)  return false;
	return true;
}
function checkisnull(obj)
	{
		if(document.getElementById(obj).value=="")
			{
				alert('Data not left blank, pls input data!');
				document.getElementById(obj).focus();
				return false;
			}
		else
			return true;
	}
//-->
		</script>
	</body>
</HTML>
