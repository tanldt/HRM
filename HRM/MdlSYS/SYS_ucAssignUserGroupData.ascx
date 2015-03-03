<%@ Control Language="c#" AutoEventWireup="false" Codebehind="SYS_ucAssignUserGroupData.ascx.cs" Inherits="iHRPCore.SYS_ucAssignUserGroupData" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch1" Src="../Include/EmpHeaderSearch1.ascx" %>
<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../Include/EmpHeaderSearch.ascx" %>
<TABLE id="Table1" border="0" cellSpacing="6" cellPadding="0" width="100%">
	<TR style="DISPLAY: none">
		<TD class="labelRequire" colSpan="3"><uc1:empheadersearch id="EmpHeaderSearch1" runat="server"></uc1:empheadersearch></TD>
	</TR>
	<TR>
		<TD class="labelRequire" colSpan="3"><asp:label id="Label1" runat="server" CssClass="labelrequire" Width="87px">Group Data</asp:label><asp:dropdownlist id="cboGroupListID" runat="server" CssClass="combo" Width="250px"></asp:dropdownlist>&nbsp;
			<asp:linkbutton accessKey="N" id="btnSearch" runat="server" CssClass="btnSearch" ToolTip="Alt+N">Search</asp:linkbutton></TD>
	</TR>
	<TR>
		<TD align="left"><asp:label id="Label2" runat="server" CssClass="labelrequire">User list</asp:label></TD>
		<TD align="center"></TD>
		<TD align="left"><asp:label id="Label3" runat="server" CssClass="labelrequire">Users in group</asp:label></TD>
	</TR>
	<tr>
		<td align="center"><asp:listbox id="lstListUser" runat="server" CssClass="combo" Width="260px" Height="145px" Font-Size="8pt"
				Font-Names="Arial" SelectionMode="Multiple"></asp:listbox></td>
		<td align="center">
			<P><BUTTON style="WIDTH: 25px" onclick="addAttribute()">&gt;</BUTTON><button style="WIDTH: 30px" onclick="addAll()">&gt;&gt;</button></P>
			<P><BUTTON style="WIDTH: 30px" onclick="delAll()">&lt;&lt;</BUTTON><BUTTON style="WIDTH: 25px" onclick="delAttribute()">&lt;</BUTTON></P>
		</td>
		<TD align="center"><asp:listbox id="lstListGroup" runat="server" CssClass="combo" Width="260px" Height="145px" Font-Size="8pt"
				Font-Names="Arial" SelectionMode="Multiple"></asp:listbox></TD>
	</tr>
	<TR>
		<TD><asp:linkbutton accessKey="N" id="btnSave" runat="server" CssClass="btnSearch" ToolTip="Alt+N"> Save</asp:linkbutton>&nbsp;&nbsp;&nbsp;
			<asp:linkbutton accessKey="N" id="btnAddnew" runat="server" CssClass="btnSearch" ToolTip="Alt+N">Cancel</asp:linkbutton></TD>
		<TD align="center"></TD>
		<TD align="center"></TD>
	</TR>
	<TR style="DISPLAY: none">
		<TD colSpan="3"><asp:textbox id="txtLstListGroup" Runat="server"></asp:textbox><asp:textbox id="txtListLen" Runat="server"></asp:textbox></TD>
	</TR>
</TABLE>
<SCRIPT language="javascript">
function checkvalidSearch()
{
	if (document.getElementById('_ctl0_cboGroupListID').value == "")
	{
		GetAlertError(iTotal,DSAlert,"0003");
		document.getElementById('_ctl0_cboGroupListID').focus();
		return false;
	}				

	return true;		
}
function checkSave()
{
	var all=document.getElementById('_ctl0_lstListGroup').length;
	var sValue='';
	for(i=0; i<all; i++)
		{	
			sValue+=document.getElementById('_ctl0_lstListGroup').item(i).value+',';
		};	
	document.getElementById('_ctl0_txtLstListGroup').value=sValue
	return true;
}
function checkAddnew()
{
	var len=document.getElementById('_ctl0_lstListGroup').length;
	if (parseInt(document.getElementById('_ctl0_txtListLen').value)!=len)
	{
		if(confirm(GetAlertText(iTotal,DSAlert,"0090"))==false){
			return true
		}else
		{			
			document.getElementById('_ctl0_btnSave').click();
			return false;
		}
	}
}
</SCRIPT>
<SCRIPT language="javascript">
createListObjects('_ctl0_lstListGroup','_ctl0_lstListUser')
</SCRIPT>
&nbsp;
