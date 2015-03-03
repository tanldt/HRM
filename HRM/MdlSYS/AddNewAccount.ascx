<%@ Control Language="c#" AutoEventWireup="false" Codebehind="AddNewAccount.ascx.cs" Inherits="iHRPCore.MdlSYS.AddNewAccount" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="98%" border="0">
	<TR>
		<TD align="center"><asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center">
			<table width="100%">
				<tr>
					<td width="12%"><asp:label id="Label1" CssClass="label" runat="server">Empl.ID</asp:label></td>
					<td width="35%"><asp:textbox id="EmpID" CssClass="input" runat="server" MaxLength="10" Width="89%"></asp:textbox><INPUT class="search" onclick="javascript:OpenWindowEmp()" type="button" value="...">
					</td>
					<td width="5%"></td>
					<td width="12%"><asp:label id="Label3" CssClass="label" runat="server">Name</asp:label></td>
					<td width="35%"><asp:textbox id="EmpName" CssClass="input" runat="server" MaxLength="100" Width="89%"></asp:textbox><INPUT class="search" onclick="javascript:OpenWindowEmp()" type="button" value="..."></td>
				</tr>
				<TR>
					<TD>
						<asp:label id="Label2" runat="server" CssClass="label">Account Login</asp:label></TD>
					<TD>
						<asp:textbox id="Textbox3" runat="server" CssClass="input" Width="100%" MaxLength="20"></asp:textbox></TD>
					<TD></TD>
					<td>
						<asp:label id="Label6" runat="server" CssClass="label">General rights</asp:label></td>
					<td>
						<asp:CheckBox id="chkActivate" runat="server" CssClass="checkbox" Text="Activate"></asp:CheckBox>&nbsp;&nbsp;
						<asp:CheckBox id="chkAdmin" runat="server" CssClass="checkbox" Text="Admin"></asp:CheckBox></td>
				</TR>
				<TR>
					<TD>
						<asp:label id="Label4" runat="server" CssClass="label">Password</asp:label></TD>
					<TD>
						<asp:textbox id="Textbox1" runat="server" CssClass="input" Width="100%" MaxLength="10" TextMode="Password"></asp:textbox></TD>
					<TD></TD>
					<TD>
						<asp:label id="Label5" runat="server" CssClass="label">Confirm Pass</asp:label></TD>
					<TD>
						<asp:textbox id="Textbox2" runat="server" CssClass="input" Width="100%" MaxLength="10" TextMode="Password"></asp:textbox></TD>
				</TR>
			</table>
		</TD>
	</TR>
	<TR>
		<TD vAlign="top" noWrap align="center" height="20">
			<HR align="center" width="100%">
		</TD>
	</TR>
	<TR>
		<TD noWrap align="center">
			<TABLE id="tblButton" cellSpacing="0" cellPadding="0" width="99%" border="0">
				<TR vAlign="top" height="30">
					<TD colspan="2" align="center" width="*"><asp:linkbutton id="btnAddNew" accessKey="N" CssClass="btnAddnew" runat="server" ToolTip="Alt+N">AddNew</asp:linkbutton>&nbsp; 
						&nbsp;&nbsp;
						<asp:linkbutton id="btnSave" accessKey="S" CssClass="btnSave" runat="server" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp;&nbsp; 
						&nbsp;
						<asp:linkbutton id="btnDelete" accessKey="D" CssClass="btnDelete" runat="server" ToolTip="Alt + D">Delete</asp:linkbutton></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<!-- end button for input form -->
</TABLE>
<script language="javascript">
	function OpenWindowEmp()
	{
		CurWindow = window.open('FormPage.aspx?Ascx=MdlHR/EmpPopup.ascx&EmpID=' + document.getElementById("_ctl0:EmpHeaderSearch1:txtEmpID").value + '&EmpName=' + document.getElementById("_ctl0:EmpHeaderSearch1:txtEmpName").value,'SearchEmp','toolbar=0,status=no,menubar=no,scrollbars=1,resizable=yes,copyhistory=yes,width=650,height=500,align=top');
		CurWindow.focus();
	} 
</script>
