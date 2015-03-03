<%@ Control Language="c#" AutoEventWireup="false" Codebehind="NewEmployee.ascx.cs" Inherits="iHRPCore.MdlHR.NewEmployee" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Import Namespace ="System.Data.SqlClient" %>
<%@ Import Namespace ="System.Data" %>
<%@ Import Namespace ="iHRPCore.Com"%>
<%
	string strLangID = Session["LangID"] == null?"EN":Session["LangID"].ToString().Trim();
	string sAccountLogin= Session["AccountLogin"]!=null?Session["AccountLogin"].ToString():"admin";
	string Ascx=Request.Params["Ascx"];		
	int Total1 = 0;
	int Total2 = 0;
	int Total3 = 0;
			
	DataTable rs1 = new DataTable();
	DataTable rs2 = new DataTable();		
	DataTable rs3 = new DataTable();	
	
	rs1 = clsCommon.GetDataTable("sp_GetDataComboLevel @TableName = 'LS_tblLevel1', @Language='" + strLangID + "',@UserGroupID='" + sAccountLogin + "'");
	rs2 = clsCommon.GetDataTable("sp_GetDataComboLevel @TableName = 'LS_tblLevel2', @Language='" + strLangID + "',@UserGroupID='" + sAccountLogin + "'");
	rs3 = clsCommon.GetDataTable("sp_GetDataComboLevel @TableName = 'LS_tblLevel3', @Language='" + strLangID + "',@UserGroupID='" + sAccountLogin + "'");
	
	Total1 = rs1.Rows.Count;
	Total2 = rs2.Rows.Count;
	Total3 = rs3.Rows.Count;		
%>
<script language="javascript">					
		var DS1 = new Array(3);
		DS1[0] = new Array(<%=Total1%>);
		DS1[1] = new Array(<%=Total1%>);
		DS1[2] = new Array(<%=Total1%>);	
		<% for(int i=0; i<Total1; i++) {%>		
			DS1[0][<%=i%>]="<%=rs1.Rows[i]["LSCompanyID"]%>";
			DS1[1][<%=i%>]="<%=rs1.Rows[i]["LSLevel1ID"]%>";
			DS1[2][<%=i%>]="<%=rs1.Rows[i]["Name"]%>";	
		<%}%>	
		
		var DS2 = new Array(3);
		DS2[0] = new Array(<%=Total2%>);
		DS2[1] = new Array(<%=Total2%>);
		DS2[2] = new Array(<%=Total2%>);	
		<% for(int i=0; i<Total2; i++) {%>		
			DS2[0][<%=i%>]="<%=rs2.Rows[i]["LSLevel1ID"]%>";
			DS2[1][<%=i%>]="<%=rs2.Rows[i]["LSLevel2ID"]%>";
			DS2[2][<%=i%>]="<%=rs2.Rows[i]["Name"]%>";	
		<%}%>		
		
		var DS3 = new Array(3);
		DS3[0] = new Array(<%=Total3%>);
		DS3[1] = new Array(<%=Total3%>);
		DS3[2] = new Array(<%=Total3%>);
		<% for(int i=0; i<Total3; i++) {%>		
			DS3[0][<%=i%>]="<%=rs3.Rows[i]["LSLevel2ID"]%>";
			DS3[1][<%=i%>]="<%=rs3.Rows[i]["LSLevel3ID"]%>";
			DS3[2][<%=i%>]="<%=rs3.Rows[i]["Name"]%>";	
		<%}%>	
</script>
<script language="javascript">
function ChangeCompany()
	{
		var all = document.getElementById("_ctl0_cboLevel2ID").length;
		var all1 = document.getElementById("_ctl0_cboLevel3ID").length;
		var all2 = document.getElementById("_ctl0_cboLevel1ID").length;
		for(i=0; i<all2; i++)
		{	
			document.getElementById("_ctl0_cboLevel1ID").remove(0);			
		};
		
		for(i=0; i<all; i++)
		{	
			document.getElementById("_ctl0_cboLevel2ID").remove(0);			
		};
		for(i=0; i<all1; i++)
		{	
			document.getElementById("_ctl0_cboLevel3ID").remove(0);			
		};
		var strCompany = document.getElementById("_ctl0_cboCompanyID").value; 
		document.getElementById("_ctl0_cboLevel1ID").add(new Option('',''));
		for(i=0; i<<%=Total1%>;i++)
		{
			if (DS1[0][i]==strCompany)
			{
				document.getElementById("_ctl0_cboLevel1ID").add(new Option(DS1[2][i],DS1[1][i]));
			};
		};		
		all = document.getElementById("_ctl0_cboLevel1ID").length;
		document.getElementById("_ctl0_cboLevel1ID").selectedIndex = 0;
		ChangeLevel1();			
	}
	
	function ChangeLevel1()
	{
		var all = document.getElementById("_ctl0_cboLevel2ID").length;
		var all1 = document.getElementById("_ctl0_cboLevel3ID").length;
		
		for(i=0; i<all; i++)
		{	
			document.getElementById("_ctl0_cboLevel2ID").remove(0);			
		};
		for(i=0; i<all1; i++)
		{	
			document.getElementById("_ctl0_cboLevel3ID").remove(0);			
		};
		var strLevel1ID = document.getElementById("_ctl0_cboLevel1ID").value; 
		document.getElementById("_ctl0_cboLevel2ID").add(new Option('',''));
		
		for(i=0; i<<%=Total2%>;i++)
		{
			if (DS2[0][i]==strLevel1ID)
			{
				document.getElementById("_ctl0_cboLevel2ID").add(new Option(DS2[2][i],DS2[1][i]));
			};
		};		
		
		if (strLevel1ID=='')
		{
			for(i=0; i<<%=Total2%>;i++)
				document.getElementById("_ctl0_cboLevel2ID").add(new Option(DS2[2][i],DS2[1][i]));
		}
		all = document.getElementById("_ctl0_cboLevel2ID").length;
		document.getElementById("_ctl0_cboLevel2ID").selectedIndex = 0;
		document.getElementById("_ctl0_txtLevel1ID").value = strLevel1ID;		
		//ChangeLevel2();			
		document.getElementById("_ctl0_btnGenCode").click();
	}
	
	function ChangeLevel2()
	{				
		document.getElementById("_ctl0_txtLevel2ID").value = document.getElementById("_ctl0_cboLevel2ID").value;		
		/*if (document.getElementById("_ctl0_txtLevel2ID").value!='')
			document.getElementById("_ctl0_btnGenCode").click();*/
	}
	function ChangeLevel3()
	{	
		document.getElementById("_ctl0_txtLevel3ID").value = document.getElementById("_ctl0_cboLevel3ID").value;
	}
</script>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD align="center"><asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center">
			<TABLE id="Table2" cellSpacing="0" cellPadding="1" width="60%" border="0">
				<TR>
					<TD width="25%"><asp:label id="Label2" runat="server" CssClass="labelRequire" Width="100%"> Cus.Group</asp:label></TD>
					<TD></TD>
					<TD><asp:dropdownlist id="cboCompanyID" runat="server" CssClass="select" Width="100%" onchange="ChangeCompany()"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD width="25%"><asp:label id="lblCompany" runat="server" CssClass="labelRequire" Width="100%"> Company</asp:label></TD>
					<TD></TD>
					<TD><asp:dropdownlist id="cboLevel1ID" runat="server" CssClass="select" Width="100%" onchange="ChangeLevel1()"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD width="25%"><asp:label id="Label1" runat="server" CssClass="label" Width="100%">Department</asp:label></TD>
					<TD></TD>
					<TD><asp:dropdownlist id="cboLevel2ID" runat="server" CssClass="select" Width="100%" onchange="ChangeLevel2()"></asp:dropdownlist></TD>
				</TR>
				<TR style="DISPLAY: none">
					<TD width="25%"><asp:label id="Label3" runat="server" CssClass="labelRequire" Width="100%">Section</asp:label></TD>
					<TD></TD>
					<TD><asp:dropdownlist id="cboLevel3ID" runat="server" CssClass="select" Width="100%"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblJoinDate" runat="server" CssClass="labelRequire" Width="100%">Joining Date</asp:label></TD>
					<TD></TD>
					<TD><asp:textbox id="txtStartDate" onblur="JavaScript:CheckDate(this)" runat="server" CssClass="input"
							Width="50%" MaxLength="10"></asp:textbox><INPUT class=btnCal onclick="javascript:popUpCalendar(<%= this.txtStartDate.ClientID %>)" type=button></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblEmpCode" runat="server" CssClass="labelRequire" Width="100%">Emp Code</asp:label></TD>
					<TD></TD>
					<TD><asp:textbox id="txtEmpCode" style="TEXT-ALIGN: left" runat="server" CssClass="inputReadonly"
							Width="50%" MaxLength="15"></asp:textbox></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblGender" runat="server" CssClass="labelRequire" Width="100%">Gender</asp:label></TD>
					<TD></TD>
					<TD><asp:dropdownlist id="cboGender" runat="server" CssClass="combo" Width="50%"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblLastName" runat="server" CssClass="labelRequire" Width="100%">Last Name</asp:label></TD>
					<TD></TD>
					<TD><asp:textbox id="txtVLastName" runat="server" CssClass="input" Width="100%" MaxLength="30"></asp:textbox></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblFirstName" runat="server" CssClass="labelRequire" Width="100%">First Name</asp:label></TD>
					<TD></TD>
					<TD><asp:textbox id="txtVFirstName" runat="server" CssClass="input" Width="100%" MaxLength="20"></asp:textbox></TD>
				</TR>
				<tr style="display:none">
					<TD colspan="3"><asp:checkbox id="chkIsCheckIDNo" runat="server" CssClass="checkbox" Text="Check ID Card No.?"></asp:checkbox></TD>
				</tr>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD>
			<HR width="97%">
		</TD>
	</TR>
	<TR>
		<TD align="center"><asp:linkbutton id="btnSave" accessKey="A" runat="server" CssClass="btnsave" ToolTip="ALT+A">Save</asp:linkbutton>
			<asp:linkbutton id="btnGenCode" style="DISPLAY:none" accessKey="A" runat="server" CssClass="btnsave"
				ToolTip="ALT+A">Gen Code</asp:linkbutton></TD>
	</TR>
	<TR>
		<TD align="center"><INPUT id="txtLevel1ID" type="hidden" name="txtLevel1ID" runat="server"><INPUT id="txtLevel2ID" type="hidden" name="txtLevel2ID" runat="server"><INPUT id="txtLevel3ID" type="hidden" name="txtLevel3ID" runat="server"></TD>
	</TR>
</TABLE>
<script language="javascript">
function validform(){	
	if(checkisnull('cboCompanyID')==false)  return false;
	if(checkisnull('cboLevel1ID')==false)  return false;
	//if(checkisnull('cboLevel2ID')==false)  return false;
	if(checkisnull('txtStartDate')==false)  return false;
	if(checkisnull('cboGender')==false)  return false;
	if(checkisnull('txtEmpCode')==false)  return false;
	if(checkisnull('txtVLastName')==false)  return false;
	if(checkisnull('txtVFirstName')==false)  return false;
	
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
</script>
