<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Catalog.ascx.cs" Inherits="Web_DM.Catalog1" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<TABLE id="tblMain" cellSpacing="0" cellPadding="0" width="98%" border="0">
	<TR>
		<TD vAlign="top" align="center" colSpan="2" height="10"><asp:label id="lblErr" ForeColor="Red" CssClass="Label" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD vAlign="top" align="center" colSpan="2">
			<TABLE id="tblControl" cellSpacing="0" cellPadding="0" width="90%" align="center" border="0"
				runat="server">
			</TABLE>
		</TD>
	</TR>
	<TR vAlign="middle" height="35">
		<TD align="center" width="*"><asp:linkbutton id="btnSearch" CssClass="btnSearch" runat="server">Search</asp:linkbutton>&nbsp; 
			&nbsp;
			<asp:linkbutton id="btnUpdate" CssClass="btnSave" runat="server">Save</asp:linkbutton>&nbsp;&nbsp; 
			&nbsp;<asp:linkbutton id="btnAddNew" CssClass="btnAddnew" runat="server">New record</asp:linkbutton>&nbsp; 
			&nbsp;
			<asp:linkbutton id="btnDelete" CssClass="btnDelete" runat="server">Delete</asp:linkbutton>&nbsp;&nbsp;&nbsp;
			<asp:linkbutton id="btnExport" CssClass="btnExport" runat="server">Export</asp:linkbutton></TD>
	</TR>
	<TR>
		<TD style="HEIGHT: 23px" align="left" width="15%">
			<asp:checkbox id="chkSelectAll" onclick="CheckAll('_ctl0_chkSelectAll','_ctl0_grdList',3,1,'chkSelect')"
				runat="server" CssClass="checkbox" Text="Select all"></asp:checkbox>&nbsp;&nbsp; 
			&nbsp;<asp:label id="Label1" CssClass="Label" runat="server">Page Rows</asp:label>&nbsp;<asp:textbox onkeypress="checkKey()" id="txtSoDong" onblur="JavaScript:IsNumeric(this)" CssClass="input"
				Width="30px" MaxLength="4" Runat="server">50</asp:textbox>
			<asp:label id="Label2" CssClass="Label" runat="server">Total rows</asp:label>&nbsp;<asp:label id="lblTotalRows" CssClass="labeldata" runat="server" Width="25px">10</asp:label><INPUT id="btnNumberColumn" style="DISPLAY: none" type="button" value="Button" name="Button1"
				runat="server">
			<asp:checkbox style="DISPLAY:none" id="chkMultiSort" CssClass="Label" runat="server" Text="Multi Sort"></asp:checkbox></TD>
	<TR>
		<TD align="center" colSpan="2"><asp:datagrid id="grdList" runat="server" Width="100%" PageSize="15" AllowPaging="True" CellPadding="0"
				AllowSorting="True">
				<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
				<ItemStyle CssClass="gridItem"></ItemStyle>
				<HeaderStyle CssClass="gridHeader"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn Visible="False" HeaderText="&lt;b&gt;Seq&lt;/b&gt;">
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<%# Container.ItemIndex + 1 +  + grdList.PageSize * grdList.CurrentPageIndex%>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Right" CssClass="gridPage"></PagerStyle>
			</asp:datagrid></TD>
	</TR>
</TABLE>
<script language="javascript">
<!--
function checkisnull(obj)
	{
		if(document.getElementById('_ctl0_' + obj) != null && document.getElementById('_ctl0_' + obj).value=="")
			{
				GetAlertError(iTotal,DSAlert,"0003");
				document.getElementById('_ctl0_' + obj).focus();
				return false;
			}
		else
			return true;
	}
function checkKey()
{	
	if(window.event.keyCode==13){		
		document.getElementById('_ctl0_btnNumberColumn').click();		
		event.returnValue=false;
		event.cancel = true;		
	}
}

function CheckDelete()
{
	if(GridCheck('_ctl0_grdList',3,1,'chkSelect')==false)
	{			
		GetAlertError(iTotal,DSAlert,"0001");				
		return false;
	}
	if(confirm(GetAlertText(iTotal,DSAlert,"0002"))==false){
		return false;
	}
	
}
//-->
</script>
