<%@ Control Language="c#" AutoEventWireup="false" Codebehind="SYS_ucAssignGroupToMaster.ascx.cs" Inherits="MdlSYS.SYS_ucAssignGroupToMaster" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="Table1" cellSpacing="6" cellPadding="0" width="100%" border="0">
	<TR>
		<TD class="labelRequire" colSpan="3"><asp:label id="Label1" Width="87px" runat="server" CssClass="labelrequire">Master Group</asp:label><asp:dropdownlist id="cboGroupListID" Width="250px" runat="server" CssClass="combo" AutoPostBack="True"></asp:dropdownlist>&nbsp;
			<asp:linkbutton id="btnSearch" accessKey="N" CssClass="btnSearch" runat="server" ToolTip="Alt+N">Search</asp:linkbutton></TD>
	</TR>
	<TR>
		<TD align="left"><asp:label id="Label2" runat="server" CssClass="labelrequire">Group</asp:label></TD>
		<TD align="center"></TD>
		<TD align="left"><asp:label id="Label3" runat="server" CssClass="labelrequire">Groups in Master group</asp:label></TD>
	</TR>
	<tr>
		<td align="center"><asp:listbox id="lstListUser" Width="260px" runat="server" CssClass="combo" SelectionMode="Multiple"
				Font-Names="Arial" Font-Size="8pt" Height="145px"></asp:listbox></td>
		<td align="center">
			<P><BUTTON style="WIDTH: 25px" onclick="addAttribute()" type="button">&gt;</BUTTON><button style="WIDTH: 30px" onclick="addAll()" type="button">&gt;&gt;</button></P>
			<P><BUTTON style="WIDTH: 30px" onclick="delAll()" type="button">&lt;&lt;</BUTTON><BUTTON style="WIDTH: 25px" onclick="delAttribute()" type="button">&lt;</BUTTON></P>
		</td>
		<TD align="center">
			<asp:listbox id="lstListGroup" CssClass="combo" runat="server" Width="260px" Height="145px" Font-Size="8pt"
				Font-Names="Arial" SelectionMode="Multiple"></asp:listbox></TD>
	</tr>
	<TR>
		<TD>
			<asp:linkbutton id="btnSave" accessKey="N" CssClass="btnSearch" runat="server" ToolTip="Alt+N"> Save</asp:linkbutton>&nbsp;&nbsp;&nbsp;
			<asp:linkbutton id="btnAddnew" accessKey="N" CssClass="btnSearch" runat="server" ToolTip="Alt+N">Cancel</asp:linkbutton></TD>
		<TD align="center"></TD>
		<TD align="center"></TD>
	</TR>
	<TR style="DISPLAY: none">
		<TD colSpan="3">
			<asp:textbox id="txtLstListGroup" Runat="server"></asp:textbox>
			<asp:textbox id="txtListLen" Runat="server"></asp:textbox></TD>
	</TR>
</TABLE>
<SCRIPT language="javascript">
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
