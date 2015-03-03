<%@ Control Language="c#" AutoEventWireup="false" Codebehind="InputDataItems.ascx.cs" Inherits="iHRPCore.InputDataItems" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../Include/EmpHeaderSearch.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ColumnList" Src="../Include/ColumnList.ascx" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD align="center">
			<TABLE id="Table2" cellSpacing="0" cellPadding="1" width="100%" border="0">
				<TBODY>
					<TR>
						<TD colSpan="3"><asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label></TD>
					</TR>
					<TR>
						<TD align="center" colSpan="6"><uc1:empheadersearch id="EmpHeaderSearch1" runat="server" IsStatus="false"></uc1:empheadersearch></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 117px"><asp:label id="Label1" CssClass="labelRequire" Runat="server">Month</asp:label></TD>
						<TD><asp:textbox id="txtMonth" CssClass="input" onblur="javascript:CheckMonthYear(this)" runat="server"
								Width="208px"></asp:textbox></TD>
						<TD colSpan="4">&nbsp;</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 117px"><asp:label id="Label2" Runat="server">File Import</asp:label></TD>
						<TD><INPUT id="txtFile" style="HEIGHT: 22px" type="file" size="41" name="inputFile" runat="server"></TD>
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
		<TD align="center"><asp:linkbutton id="btnSearch" accessKey="D" CssClass="btnExport" runat="server" ToolTip="Alt + D">Search</asp:linkbutton>&nbsp;
			<asp:linkbutton id="btnImport" accessKey="S" CssClass="btnSave" runat="server">Import</asp:linkbutton>&nbsp;&nbsp;
			<asp:linkbutton id="btnExport" accessKey="E" CssClass="btnExport" runat="server">Export</asp:linkbutton></TD>
	</TR>
	<tr>
		<td>
			&nbsp;
		</td>
	</tr>
	<tr align="center">
		<td>
			<DIV style="WIDTH: 750px; HEIGHT: 300px; OVERFLOW: scroll">
				<asp:datagrid id="dtgList" CssClass="grid" runat="server" Width="100%" BackColor="White" BorderWidth="1px"
					CellPadding="0" AllowPaging="false" AutoGenerateColumns="true" AllowSorting="false" PageSize="20"
					BorderColor="#3366CC">
					<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
					<ItemStyle CssClass="gridItem"></ItemStyle>
					<HeaderStyle CssClass="gridHeader"></HeaderStyle>
					<FooterStyle CssClass="gridFooter"></FooterStyle>
					<Columns>
						<asp:TemplateColumn HeaderText="&lt;b&gt;Seq&lt;/b&gt;">
							<HeaderStyle Width="5%"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<ItemTemplate>
								<%# Container.ItemIndex + 1 + dtgList.PageSize*dtgList.CurrentPageIndex%>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
					<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
				</asp:datagrid>
			</DIV>
		</td>
	</tr>
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
	if(checkisnull('txtFile')==false)  return false;
	//if(checkisnull('cboLSCompanyID')==false)  return false;
	
	return true;
}
</script>
<SCRIPT language="javascript">
createListObjects('_ctl0_lstListGroup','_ctl0_lstListUser')
</SCRIPT>
