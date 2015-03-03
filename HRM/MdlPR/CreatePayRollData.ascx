<%@ Control Language="c#" AutoEventWireup="false" Codebehind="CreatePayRollData.ascx.cs" Inherits="iHRPCore.CreatePayRollData" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../Include/EmpHeaderSearch.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ColumnList" Src="../Include/ColumnList.ascx" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD align="center">
			<TABLE id="Table2" cellSpacing="0" cellPadding="1" width="100%" border="0">
				<TBODY>
					<TR>
						<TD colSpan="3"><asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></TD>
					</TR>
					<TR>
						<TD colSpan="6" align="center">
							<uc1:EmpHeaderSearch id="EmpHeaderSearch1" runat="server"></uc1:EmpHeaderSearch></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 117px"><asp:label id="Label1" CssClass="labelRequire" Runat="server">Month</asp:label></TD>
						<TD><asp:textbox id="txtMonth" onblur="javascript:CheckMonthYear(this)" runat="server" CssClass="input"
								Width="208px"></asp:textbox></TD>
						<TD colSpan="4">&nbsp;</TD>
					</TR>
				</TBODY>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD align="center">
			<hr width="100%">
		</TD>
	</TR>
	<TR>
		<TD align="center">
			<asp:linkbutton id="btnSearch" accessKey="D" runat="server" CssClass="btnExport" ToolTip="Alt + D">Filter</asp:linkbutton>&nbsp;
			<asp:linkbutton id="btnSave" accessKey="S" runat="server" CssClass="btnSave" ToolTip="ALT+S">Create</asp:linkbutton>&nbsp;&nbsp;&nbsp;
			<asp:linkbutton id="btnExport" accessKey="E" runat="server" CssClass="btnExport" ToolTip="Alt+E, Xu?t dl trên lu?i ra file Excel"
				Visible="false"> Export</asp:linkbutton></TD>
	</TR>
	<tr align="center">
		<td>
			<table>
				<TR>
					<TD align="left"><asp:label id="Label7" runat="server" CssClass="labelrequire">Employee list</asp:label></TD>
					<TD align="center"></TD>
					<TD align="left"><asp:label id="Label8" runat="server" CssClass="labelrequire">Employees Assigned</asp:label></TD>
				</TR>
				<tr align="center">
					<td align="center"><asp:listbox id="lstListUser" runat="server" CssClass="combo" Width="232px" Font-Names="Arial"
							Font-Size="8pt" SelectionMode="Multiple" Height="145px"></asp:listbox></td>
					<td align="center">
						<P><BUTTON style="WIDTH: 25px" onclick="addAttribute()">&gt;</BUTTON><button style="WIDTH: 30px" onclick="addAll()">&gt;&gt;</button></P>
						<P><BUTTON style="WIDTH: 30px" onclick="delAll()">&lt;&lt;</BUTTON><BUTTON style="WIDTH: 25px" onclick="delAttribute()">&lt;</BUTTON></P>
					</td>
					<TD align="center"><asp:listbox id="lstListGroup" runat="server" CssClass="combo" Width="232px" Font-Names="Arial"
							Font-Size="8pt" SelectionMode="Multiple" Height="145px"></asp:listbox></TD>
				</tr>
			</table>
		</td>
	</tr>
	<TR style="DISPLAY: none">
		<TD align="left" height="21"><asp:textbox id="txtLstListGroup" Runat="server"></asp:textbox><asp:textbox id="txtListLen" Runat="server"></asp:textbox>
			<asp:textbox id="txtLstListUser" Runat="server"></asp:textbox>
		</TD>
	</TR>
	<TR style="DISPLAY: none">
		<TD><asp:textbox id="txtPageLoad" runat="server" Visible="False"></asp:textbox><asp:textbox id="txtHidenInfo" runat="server" Visible="False"></asp:textbox></TD>
	</TR>
</TABLE>
<script>
function ReturnEmpPopUp(strUserID, strUserName, strFGroup)
{
	document.getElementById("_ctl0_txtMonth").value=strUserID;		
	document.getElementById("_ctl0_lblUserName").innerText=strUserName;
	//document.getElementById("_ctl0_txtHidenInfo").value = strUserName;
}
function checkdelete()
{
	/*if(GridCheck('_ctl0_dtgList',3,1,'chkSelect')==false)
	{
		GetAlertError(iTotal,DSAlert,"0001");		
		return false;
	}
	if(confirm(GetAlertText(iTotal,DSAlert,"0002"))==false){
		return false;
	}*/
	if(document.getElementById('_ctl0_txtMonth').value=="")
	{
		GetAlertError(iTotal,DSAlert,"P0001");		
		document.getElementById('_ctl0_txtMonth').focus();
		return false;
	}
	//if(checkisnull('txtMonth')==false)  return false;
	//if(checkisnull('cboLSCompanyID')==false)  return false;
	return true;
	//document.getElementById("_ctl0_lblUserName").innerText = <%=Session["AccountInfo"]%>;
}
function checkisnull(obj)
{
	if(document.getElementById('_ctl0_' + obj).value=="")
		{
			GetAlertError(iTotal,DSAlert,"0003");		
			document.getElementById('_ctl0_' + obj).focus();
			return false;
		}
	else
		return true;
}
function validform()
{	
	if(document.getElementById('_ctl0_txtMonth').value=="")
	{
		GetAlertError(iTotal,DSAlert,"P0001");		
		document.getElementById('_ctl0_txtMonth').focus();
		return false;
	}
	//if(checkisnull('txtMonth')==false)  return false;
	//if(checkisnull('cboLSCompanyID')==false)  return false;
	
	var all=document.getElementById('_ctl0_lstListGroup').length;
	var sValue='';
	for(i=0; i<all; i++)
		{	
			sValue+=document.getElementById('_ctl0_lstListGroup').item(i).value+',';
		};	
	document.getElementById('_ctl0_txtLstListGroup').value=sValue

	var all1=document.getElementById('_ctl0_lstListUser').length;
	var sValue2='';
	for(i=0; i<all1; i++)
		{	
			sValue2+=document.getElementById('_ctl0_lstListUser').item(i).value+',';
		};	
	document.getElementById('_ctl0_txtLstListUser').value=sValue2

	return true;
}
</script>
<SCRIPT language="javascript">
createListObjects('_ctl0_lstListGroup','_ctl0_lstListUser')
</SCRIPT>
