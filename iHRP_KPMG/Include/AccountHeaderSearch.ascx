<%@ Control Language="c#" AutoEventWireup="false" Codebehind="AccountHeaderSearch.ascx.cs" Inherits="iHRPCore.Include.AccountHeaderSearch" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="Table1" cellSpacing="0" cellPadding="1" width="99%" border="0">
	<TR>
		<TD width="11%"><asp:label id="Label7" CssClass="label" runat="server">Account/GroupID</asp:label></TD>
		<TD width="26%"><asp:textbox id="txtEmpID" CssClass="input" runat="server" Width="90%"></asp:textbox><%if(Request.Params["EmpID"] == null && Request.Params["EmpName"] == null){%><%}%></TD>
		<TD width="10%"><asp:label id="Label2" CssClass="label" runat="server">Name</asp:label></TD>
		<TD width="30%"><asp:textbox id="txtEmpName" CssClass="input" runat="server" Width="90%"></asp:textbox><%if(Request.Params["EmpID"] == null && Request.Params["EmpName"] == null){%><%}%></TD>
		<TD width="9%"></TD>
		<TD width="14%"></TD>
	</TR>
</TABLE>
<script language="javascript">
	function OpenWindowEmp()
	{
		//CurWindow = window.open('FormPage.aspx?Ascx=MdlHR/EmpPopup.ascx&EmpName=' + document.getElementById("_ctl0:AccountHeaderSearch1:txtEmpName").value,'SearchEmp','toolbar=0,status=no,menubar=no,scrollbars=1,resizable=yes,copyhistory=yes,width=650,height=500,align=top');
		CurWindow = window.open('FormPage.aspx?Ascx=MdlHR/EmpPopup.ascx','SearchEmp','toolbar=0,status=no,menubar=no,scrollbars=1,resizable=yes,copyhistory=yes,width=650,height=500,align=top');
		CurWindow.focus();
	} 
</script>
