<%@ Register TagPrefix="uc1" TagName="Top" Src="../Include/Top1.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="AbroadRecord.ascx.cs" Inherits="iHRPCore.MdlHR.AbroadRecord" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Import Namespace ="iHRPCore.Com"%>
<%@ Import Namespace ="System.Data" %>
<%@ Import Namespace ="System.Data.SqlClient" %>
<%
			string strLanguageID = Session["LangID"] == null?"EN":Session["LangID"].ToString().Trim();				
				string formName=Request.Params["Ascx"];				
				int iTotal=0;

				DataTable dtData=clsCommon.GetDataTable("LS_spfrmALERTMESSAGE @Activity='GetDataByID',@Ascx='" + formName + "'");
				iTotal=dtData.Rows.Count;		
				
						
%>
<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
	<TR>
		<TD align="center">
			<uc1:Top id="Top1" runat="server" Visible="False"></uc1:Top></TD>
	</TR>
	<TR>
		<TD align="center">
			<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="600" border="0">
				<TR>
					<TD></TD>
					<TD align="center" colSpan="3"><asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD><asp:label id="Label1" CssClass="labelRequire" runat="server">From Date</asp:label></TD>
					<TD><asp:textbox id="txtFromDate" onblur="javascript:CheckDate(this)" CssClass="input" runat="server"></asp:textbox><INPUT class="btnCal" onclick="javascript:popUpCalendar(<%=this.txtFromDate.ClientID%>);" type="button"></TD>
					<TD><asp:label id="Label2" CssClass="labelRequire" runat="server">To Date</asp:label></TD>
					<TD><asp:textbox id="txtToDate" onblur="javascript:CheckDate(this)" CssClass="input" runat="server"></asp:textbox><INPUT class="btnCal" onclick="javascript:popUpCalendar(<%=this.txtToDate.ClientID%>);" type="button"></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 12px"><asp:label id="Label3" CssClass="labelRequire" runat="server">Country</asp:label></TD>
					<TD style="HEIGHT: 12px"><asp:dropdownlist id="cboLSNationalityID" CssClass="combo" runat="server" Width="200px"></asp:dropdownlist></TD>
					<TD style="HEIGHT: 12px"><asp:label id="Label4" CssClass="labelRequire" runat="server">Reason</asp:label></TD>
					<TD style="HEIGHT: 12px"><asp:textbox id="txtReason" CssClass="input" runat="server" MaxLength="200"></asp:textbox></TD>
				</TR>
				<TR>
					<TD><asp:label id="Label5" CssClass="label" runat="server">Note</asp:label></TD>
					<TD colSpan="3"><asp:textbox id="txtNote" CssClass="input" runat="server" Width="470px" MaxLength="200"></asp:textbox></TD>
				</TR>
			</TABLE>
			<asp:textbox id="txtAbroadRecordID" CssClass="input" runat="server" Width="80px" MaxLength="10"
				Visible="False"></asp:textbox><asp:textbox id="txtEmpID" CssClass="input" runat="server" Width="80px" MaxLength="10" Visible="False"></asp:textbox></TD>
	</TR>
	<TR>
		<TD align="center"></TD>
	</TR>
	<TR>
		<TD align="center"><asp:linkbutton id="btnAddNew" accessKey="N" CssClass="btnAddNew" runat="server" ToolTip="Alt+N">Add New</asp:linkbutton>&nbsp;&nbsp;&nbsp;
			<asp:linkbutton id="btnSave" accessKey="S" CssClass="Button" runat="server" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp; 
			&nbsp;&nbsp;
			<asp:linkbutton id="btnDelete" accessKey="D" CssClass="btnExport" runat="server" ToolTip="Alt + D">Delete</asp:linkbutton>&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:linkbutton id="btnExport" accessKey="E" CssClass="btnExport" runat="server" ToolTip="Alt+E, Xuất dl trên lưới ra file Excel"> Export</asp:linkbutton>&nbsp;&nbsp;&nbsp;
			<asp:linkbutton id="btnClose" accessKey="C" CssClass="Button" runat="server" ToolTip="ALT+C">Close</asp:linkbutton></TD>
	</TR>
	<TR>
		<TD align="center"></TD>
	</TR>
	<TR>
		<TD align="center"><asp:datagrid id="dtgList" runat="server" Width="600px" BackColor="White" ShowFooter="True" CellPadding="0"
				AutoGenerateColumns="False" AllowSorting="True" BorderColor="#3366CC">
<FooterStyle CssClass="gridFooter">
</FooterStyle>

<AlternatingItemStyle CssClass="gridAlter">
</AlternatingItemStyle>

<ItemStyle CssClass="gridItem">
</ItemStyle>

<HeaderStyle CssClass="gridHeader">
</HeaderStyle>

<Columns>
<asp:BoundColumn Visible="False" DataField="AbroadRecordID" HeaderText="ID">
<HeaderStyle Width="10%">
</HeaderStyle>
</asp:BoundColumn>
<asp:TemplateColumn HeaderText="Delete">
<HeaderStyle HorizontalAlign="Center" Width="5%">
</HeaderStyle>

<ItemStyle HorizontalAlign="Center">
</ItemStyle>

<HeaderTemplate>
							<asp:CheckBox id="chkSelectAll" runat="server" CssClass="checkbox" onclick="CheckAll('_ctl0_dtgList__ctl1_chkSelectAll','_ctl0_dtgList',2,0,'chkSelect')"></asp:CheckBox>
						
</HeaderTemplate>

<ItemTemplate>
							<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
						
</ItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn HeaderText="FromDate">
<HeaderStyle Width="10%">
</HeaderStyle>

<ItemStyle HorizontalAlign="Center">
</ItemStyle>

<ItemTemplate>
							<asp:LinkButton id=hpFromDate CssClass=Hlink Width="15%" Text='<%# DataBinder.Eval(Container, "DataItem.FromDate") %>' CommandName="Edit" Runat="server">
							</asp:LinkButton>
						
</ItemTemplate>
</asp:TemplateColumn>
<asp:BoundColumn DataField="ToDate" HeaderText="ToDate">
<HeaderStyle Width="10%">
</HeaderStyle>
</asp:BoundColumn>
<asp:BoundColumn DataField="LSNationalityName" HeaderText="Country">
<HeaderStyle Width="30%">
</HeaderStyle>
</asp:BoundColumn>
<asp:BoundColumn DataField="Reason" HeaderText="Reason">
<HeaderStyle Width="30%">
</HeaderStyle>
</asp:BoundColumn>
</Columns>
			</asp:datagrid></TD>
	</TR>
</TABLE>
<script language="javascript">
	var iTotal=<%=iTotal%>;
		var DSAlert = new Array(2);
			var iTotal=<%=iTotal%>;
			DSAlert[0] = new Array(<%=iTotal%>);
			DSAlert[1] = new Array(<%=iTotal%>);
			<% for(int i=0; i<iTotal; i++) {%>					
			DSAlert[0][<%=i%>]="<%=dtData.Rows[i]["MessCode"]%>";
			DSAlert[1][<%=i%>]="<%=dtData.Rows[i][strLanguageID]%>";	
		<%}%>			
</script>
<script language="javascript" src="../Include/common.js"></script>
<script language="javascript" src="./Include/MyLibrary.js"></script>
<script language="javascript" src="./Include/common.js"></script>
<script language="javascript">
<!--			
function checkdelete()
{
	//alert(document.getElementById('_ctl0_dtgList').rows.length); return false;
	if(GridCheck('_ctl0_dtgList',2,0,'chkSelect')==false)
	{			
		GetAlertError(iTotal,DSAlert,"0001");				
		return false;
	}
	if(confirm(GetAlertText(iTotal,DSAlert,"0002"))==false){
		return false;
	}	
}
function validform()
{	
	if(checkisnull('txtFromDate')==false)  return false;			
	if(checkisnull('txtToDate')==false)  return false;			
	if (FromSmallToDate(document.getElementById('_ctl0_txtToDate'),document.getElementById('_ctl0_txtFromDate') )> 0)
	{
		GetAlertError(iTotal,DSAlert,"0007");				
		return false;
	}
	if(checkisnull('cboLSNationalityID')==false)  return false;	
	if(checkisnull('txtReason')==false)  return false;			
	return true;
}
function checkisnull(obj)
{
	if(document.getElementById('_ctl0_' + obj).value=='')
	{	
		GetAlertError(iTotal,DSAlert,"0003");
		document.getElementById('_ctl0_' + obj).focus();
		return false;
	}
	else
	{
		return true;
	}
}

function CloseWindow()
{
	window.close();
}
//-->
</script>

