<%@ Import Namespace ="System.Data.SqlClient" %>
<%@ Import Namespace ="System.Data" %>
<%@ Import Namespace ="iHRPCore.Com"%>
<%@ Import Namespace ="iHRPCore.TMSComponent"%>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="TNCN_Thang.ascx.cs" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" Inherits="Reports.PIT.TNCN_Thang" %>
<%@ Register TagPrefix="uc" TagName="EmpHeader" Src="../../Include/EmpHeaderSearch.ascx" %>
<%
	string strLangID = Session["LangID"] == null?"VN":Session["LangID"].ToString().Trim();	
	int Total1 = 0;	
	DataTable rs1 = new DataTable();	
	rs1 = clsCommon.GetDataTable("sp_GetDataComboLevel @TableName = 'LS_tblLevel1', @Language='" + strLangID + "'");	
	Total1 = rs1.Rows.Count;	
%>
<script language="javascript">
		//array for level1
		var DS1 = new Array(3);
		DS1[0] = new Array(<%=Total1%>);
		DS1[1] = new Array(<%=Total1%>);
		DS1[2] = new Array(<%=Total1%>);		
		<% for(int i=0; i<Total1; i++) {%>		
			DS1[0][<%=i%>]="<%=rs1.Rows[i]["LSCompanyID"]%>";			
			DS1[1][<%=i%>]="<%=rs1.Rows[i]["LSLevel1ID"]%>";
			DS1[2][<%=i%>]="<%=rs1.Rows[i]["Name"]%>";	
		<%}%>		
</script>
<table width="100%">
	<tr>
		<td>
			<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="0" width="99%">
				<TR>
					<TD colSpan="11"><asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></TD>
				</TR>
				<TR style="DISPLAY: none">
					<TD colSpan="11" align="center"><uc:empheader id="EmpHeaderSearch1" runat="server"></uc:empheader></TD>
				</TR>
				<tr>
					<TD width="12%"><asp:label id="Label1" runat="server" CssClass="labelrequire" Width="100%">Customer Group </asp:label></TD>
					<td width="15%"><asp:dropdownlist id="cboLSCompanyCode" runat="server" CssClass="select" Width="100%" onchange="ChangeCompany()"></asp:dropdownlist></td>
					<td width="5%"></td>
					<TD width="10%"><asp:label id="Label2" runat="server" CssClass="labelrequire" Width="100%">Company </asp:label></TD>
					<TD width="15%"><asp:dropdownlist id="cboLSLevel1ID" runat="server" CssClass="select" Width="100%" onchange="getValueLSLevel1ID()"></asp:dropdownlist></TD>
				</tr>
				<tr>
					<TD><asp:label id="cboReportTypeTitle" runat="server" CssClass="labelrequire" Width="100%">Report Type </asp:label></TD>
					<td><asp:dropdownlist id="cboReportType" runat="server" CssClass="combo" Width="100%">
							<asp:ListItem Value="" Selected="True"></asp:ListItem>
							<asp:ListItem Value="02-KK-TNCN">02-KK-TNCN</asp:ListItem>
							<asp:ListItem Value="03-KK-TNCN">03-KK-TNCN</asp:ListItem>
							<asp:ListItem Value="04-KK-TNCN">04-KK-TNCN</asp:ListItem>
							<asp:ListItem Value="07-KK-TNCN">07-KK-TNCN</asp:ListItem>
						</asp:dropdownlist></td>
					<td></td>
				</tr>
				<tr>
					<TD><asp:label id="Label4" runat="server" CssClass="labelrequire" Width="100%">Month </asp:label></TD>
					<TD>
						<asp:dropdownlist id="cboMonth" CssClass="combo" runat="server" Width="100%"></asp:dropdownlist></TD>
					<td width="5%"></td>
					<TD><asp:label id="Label3" runat="server" CssClass="labelrequire" Width="100%">Year </asp:label></TD>
					<TD><asp:dropdownlist id="cboYear" runat="server" CssClass="combo" Width="100%"></asp:dropdownlist></TD>
				</tr>
			</TABLE>
			<!-- end button for input form --></td>
	</tr>
	<TR>
		<TD vAlign="top" noWrap align="center">
			<HR align="center">
			<asp:textbox style="DISPLAY: none" id="txtLSLevel1ID" runat="server"></asp:textbox><asp:textbox style="DISPLAY: none" id="txtLSLevel1Name" runat="server"></asp:textbox></TD>
	</TR>
	<TR>
		<TD noWrap align="center">
			<TABLE id="tblButton" cellSpacing="0" cellPadding="0" width="98%" border="0">
				<TR vAlign="top" height="30">
					<TD align="center" width="*">
						<asp:linkbutton id="btnExport" accessKey="E" CssClass="btnList" runat="server" ToolTip="Alt + E"
							CausesValidation="False"> Export</asp:linkbutton>&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnExport2" accessKey="E" CssClass="btnList" runat="server" CausesValidation="False"
							ToolTip="Alt + E" Visible="False"> Export 2</asp:linkbutton>
					</TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
</table>
<script language="javascript">

function validform(){	
	if(checkisnull('cboLSCompanyCode')==false)  return false;	
	if(checkisnull('cboLSLevel1ID')==false)  return false;
	if(checkisnull('cboReportType')==false)  return false;
	if(checkisnull('cboMonth')==false)  return false;
	if(checkisnull('cboYear')==false)  return false;
	return true;
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

function ChangeCompany()
	{
		var al = document.getElementById("_ctl0_cboLSLevel1ID").length;
		
		for(i=0; i<al; i++)
		{	
			document.getElementById("_ctl0_cboLSLevel1ID").remove(0);			
		};
		
		var strCompanyID = document.getElementById("_ctl0_cboLSCompanyCode").value;		
		document.getElementById("_ctl0_cboLSLevel1ID").add(new Option('',''));		
		for(i=0; i< <%=Total1%>;i++)
		{	
			if (DS1[0][i]==strCompanyID)
			{
				document.getElementById("_ctl0_cboLSLevel1ID").add(new Option(DS1[2][i],DS1[1][i]));				
			};
		};		
		document.getElementById("_ctl0_cboLSLevel1ID").selectedIndex = 0;						
	}
ChangeCompany();
function getValueLSLevel1ID()
{
	document.getElementById("_ctl0_txtLSLevel1ID").value = document.getElementById("_ctl0_cboLSLevel1ID").value;	
	document.getElementById("_ctl0_txtLSLevel1Name").value = document.getElementById("_ctl0_cboLSLevel1ID")[document.getElementById("_ctl0_cboLSLevel1ID").selectedIndex].text;
}	
</script>
