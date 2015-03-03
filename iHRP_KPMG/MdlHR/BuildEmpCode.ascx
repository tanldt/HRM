<%@ Import Namespace ="System.Data.SqlClient" %>
<%@ Import Namespace ="System.Data" %>
<%@ Import Namespace ="iHRPCore.Com"%>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="BuildEmpCode.ascx.cs" Inherits="iHRPCore.MdlHR.BuildEmpCode1" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<script language="javascript" src="Component/DateCOMM/popcalendar.js"></script>
<TABLE id="Table1" height="100" cellSpacing="0" cellPadding="0" width="300" border="0">
	<TR>
		<TD colSpan="2"><asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD width="30%"><asp:label id="lblLevel1ID" CssClass="labelRequire" runat="server" Width="100%"> Cus.Group</asp:label></TD>
		<TD width="70%"><asp:dropdownlist id="cboLSCompanyCode" CssClass="select" runat="server" Width="100%" onchange="ChangeCompany()"></asp:dropdownlist></TD>
	</TR>
	<TR>
		<TD width="30%"><asp:label id="Label1" CssClass="labelRequire" runat="server" Width="100%"> Company</asp:label></TD>
		<TD width="70%"><asp:dropdownlist id="cboLSLevel1ID" CssClass="select" runat="server" Width="100%"></asp:dropdownlist></TD>
	</TR>
	<TR>
		<TD><asp:label id="lblJoinDate" CssClass="labelRequire" runat="server" Width="100%"> Joining date</asp:label></TD>
		<TD><asp:textbox id="txtStartDate" onblur="JavaScript:CheckDate(this)" CssClass="input" runat="server"
				Width="76" MaxLength="10"></asp:textbox>&nbsp;<INPUT class="btnCal" onclick="javascript:popUpCalendar(_ctl0_txtStartDate)" type="button"></TD>
	</TR>
	<TR>
		<TD><asp:label id="lblEmpCode" CssClass="labelRequire" runat="server" Width="100%">Emp Code</asp:label></TD>
		<TD><asp:textbox id="txtEmpCode" CssClass="input" runat="server" Width="76px" MaxLength="10" ReadOnly="True"></asp:textbox></TD>
	</TR>
	<TR>
		<TD colSpan="2">
			<HR width="97%">
		</TD>
	</TR>
	<TR>
		<TD align="center" colSpan="5"><asp:linkbutton id="btnBuild" accessKey="B" CssClass="btnBuildCode" runat="server" ToolTip="ALT+B"> Generate</asp:linkbutton>&nbsp;&nbsp;
			<asp:linkbutton id="btnSave" accessKey="A" CssClass="btnsave" runat="server" ToolTip="ALT+A"> Save</asp:linkbutton></TD>
	</TR>
</TABLE>
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
<script language="javascript">
<!--
function checkaccept(){
	if(document.getElementById('_ctl0_txtEmpCode').value=="")
		{
			GetAlertError(iTotal,DSAlert,"0023");
			document.getElementById('_ctl0_txtEmpCode').focus();
			return false;
		}
	return true;	
}
function validform(){
	if(checkisnull('cboLSCompanyCode')==false)  return false;
	if(checkisnull('cboLSLevel1ID')==false)  return false;
	if(checkisnull('txtStartDate')==false)  return false;
	if(checkisnull('txtEmpCode')==false)  return false;
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
//-->
</script>
