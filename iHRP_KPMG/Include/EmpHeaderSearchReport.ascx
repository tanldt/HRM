<%@ Import Namespace ="System.Data.SqlClient" %>
<%@ Import Namespace ="System.Data" %>
<%@ Import Namespace ="iHRPCore.Com"%>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="EmpHeaderSearchReport.ascx.cs" Inherits="iHRPCore.Include.EmpHeaderSearchReport" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%
	string strLangID = Session["LangID"] == null?"VN":Session["LangID"].ToString().Trim();
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
		var all = document.getElementById("_ctl0:EmpHeaderSearchReport1:cboLevel2").length;
		var all1 = document.getElementById("_ctl0:EmpHeaderSearchReport1:cboLevel3").length;
		var all2 = document.getElementById("_ctl0:EmpHeaderSearchReport1:cboLevel1").length;
		for(i=0; i<all2; i++)
		{	
			document.getElementById("_ctl0:EmpHeaderSearchReport1:cboLevel1").remove(0);			
		};
		
		for(i=0; i<all; i++)
		{	
			document.getElementById("_ctl0:EmpHeaderSearchReport1:cboLevel2").remove(0);			
		};
		for(i=0; i<all1; i++)
		{	
			document.getElementById("_ctl0:EmpHeaderSearchReport1:cboLevel3").remove(0);			
		};
		var strCompany = document.getElementById("_ctl0:EmpHeaderSearchReport1:cboCompany").value; 
		document.getElementById("_ctl0:EmpHeaderSearchReport1:cboLevel1").add(new Option('',''));
		for(i=0; i<<%=Total1%>;i++)
		{
			if (DS1[0][i]==strCompany)
			{
				document.getElementById("_ctl0:EmpHeaderSearchReport1:cboLevel1").add(new Option(DS1[2][i],DS1[1][i]));
			};
		};		
		all = document.getElementById("_ctl0:EmpHeaderSearchReport1:cboLevel1").length;
		document.getElementById("_ctl0:EmpHeaderSearchReport1:cboLevel1").selectedIndex = 0;
		ChangeLevel1();			
	}
	
	function ChangeLevel1()
	{
		var all = document.getElementById("_ctl0:EmpHeaderSearchReport1:cboLevel2").length;
		var all1 = document.getElementById("_ctl0:EmpHeaderSearchReport1:cboLevel3").length;
		
		for(i=0; i<all; i++)
		{	
			document.getElementById("_ctl0:EmpHeaderSearchReport1:cboLevel2").remove(0);			
		};
		for(i=0; i<all1; i++)
		{	
			document.getElementById("_ctl0:EmpHeaderSearchReport1:cboLevel3").remove(0);			
		};
		var strLevel1ID = document.getElementById("_ctl0:EmpHeaderSearchReport1:cboLevel1").value; 
		document.getElementById("_ctl0:EmpHeaderSearchReport1:cboLevel2").add(new Option('',''));
		
		for(i=0; i<<%=Total2%>;i++)
		{
			if (DS2[0][i]==strLevel1ID)
			{
				document.getElementById("_ctl0:EmpHeaderSearchReport1:cboLevel2").add(new Option(DS2[2][i],DS2[1][i]));
			};
		};		
		all = document.getElementById("_ctl0:EmpHeaderSearchReport1:cboLevel2").length;
		document.getElementById("_ctl0:EmpHeaderSearchReport1:cboLevel2").selectedIndex = 0;
		document.getElementById("_ctl0:EmpHeaderSearchReport1:txtLevel1ID").value = strLevel1ID;
		ChangeLevel2();			
	}
	
	function ChangeLevel2()
	{
		var all = document.getElementById("_ctl0:EmpHeaderSearchReport1:cboLevel3").length;
		
		for(i=0; i<all; i++)
		{	
			document.getElementById("_ctl0:EmpHeaderSearchReport1:cboLevel3").remove(0);			
		};
		var strLevel2ID = document.getElementById("_ctl0:EmpHeaderSearchReport1:cboLevel2").value; 
		document.getElementById("_ctl0:EmpHeaderSearchReport1:cboLevel3").add(new Option('',''));
		
		for(i=0; i<<%=Total3%>;i++)
		{
			if (DS3[0][i]==strLevel2ID)
			{
				document.getElementById("_ctl0:EmpHeaderSearchReport1:cboLevel3").add(new Option(DS3[2][i],DS3[1][i]));
			};
		};		
		document.getElementById("_ctl0:EmpHeaderSearchReport1:txtLevel2ID").value = document.getElementById("_ctl0:EmpHeaderSearchReport1:cboLevel2").value;
		ChangeLevel3();	
	}
	function ChangeLevel3()
	{	
		document.getElementById("_ctl0:EmpHeaderSearchReport1:txtLevel3ID").value = document.getElementById("_ctl0:EmpHeaderSearchReport1:cboLevel3").value;
	}
</script>
<!--table-->
<TABLE BORDER="0" CELLPADDING="0" CELLSPACING="0" width="100%">
	<TR>
		<TD WIDTH="5" HEIGHT="5">
			<IMG SRC="images/table_01.jpg" WIDTH="5" HEIGHT="5" ALT=""></TD>
		<TD background="images/table_03.jpg" HEIGHT="5">
			<IMG SRC="images/table_03.jpg" WIDTH="10" HEIGHT="5" ALT=""></TD>
		<TD WIDTH="9" HEIGHT="5">
			<IMG SRC="images/table_05.jpg" WIDTH="9" HEIGHT="5" ALT=""></TD>
	</TR>
	<TR>
		<TD background="images/table_11.jpg" WIDTH="5">&nbsp;</TD>
		<TD background="images/table_13.jpg" align="center">
			<!--table body-->
			<TABLE id="Table1" cellSpacing="0" cellPadding="1" width="100%" border="0">
				<TR>
					<TD width="10%"><asp:label id="Label7" CssClass="label" runat="server">Emp ID</asp:label></TD>
					<TD width="20%"><asp:textbox id="txtEmpID" onkeypress="checkKey('_ctl0_btnSearch')" CssClass="input" runat="server"
							Width="92%"></asp:textbox><%if(Request.Params["EmpID"] == null && Request.Params["EmpName"] == null){%><%}%></TD>
					<TD width="10%"><asp:label id="Label2" CssClass="label" runat="server">Emp Name</asp:label></TD>
					<TD width="24%"><asp:textbox id="txtEmpName" onkeypress="checkKey('_ctl0_btnSearch')" CssClass="input" runat="server"
							Width="95%"></asp:textbox><%if(Request.Params["EmpID"] == null && Request.Params["EmpName"] == null){%><%}%></TD>
					<TD width="10%"><asp:label id="Label1" CssClass="label" runat="server"> Customer group</asp:label></TD>
					<TD width="28%">
						<asp:dropdownlist onkeypress="checkKey('_ctl0_btnSearch')" id="cboCompany" onchange="ChangeCompany()"
							runat="server" CssClass="combo" Width="95%"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD><asp:label id="Label3" CssClass="label" runat="server">Company</asp:label></TD>
					<TD><asp:dropdownlist id="cboLevel1" CssClass="combo" onkeypress="checkKey('_ctl0_btnSearch')" runat="server"
							Width="92%" onchange="ChangeLevel1()"></asp:dropdownlist></TD>
					<TD><asp:label id="Label4" CssClass="label" runat="server"> Dept</asp:label></TD>
					<TD><asp:dropdownlist id="cboLevel2" CssClass="combo" onkeypress="checkKey('_ctl0_btnSearch')" runat="server"
							Width="95%" onchange="ChangeLevel2()"></asp:dropdownlist></TD>
					<TD><asp:label id="Label6" CssClass="label" runat="server"> Group</asp:label></TD>
					<TD><asp:dropdownlist id="cboLevel3" CssClass="combo" runat="server" onkeypress="checkKey('_ctl0_btnSearch')"
							Width="95%" onchange="ChangeLevel3()"></asp:dropdownlist></TD>
				</TR>
				<TR style="DISPLAY:none">
					<TD><asp:label id="Label5" CssClass="label" runat="server">Location</asp:label></TD>
					<TD><asp:dropdownlist id="cboLocation" CssClass="combo" runat="server" onkeypress="checkKey('_ctl0_btnSearch')"
							Width="95%"></asp:dropdownlist></TD>
					<TD><asp:label id="Label8" CssClass="label" runat="server">Position</asp:label></TD>
					<TD><asp:dropdownlist id="cboPosition" CssClass="combo" onkeypress="checkKey('_ctl0_btnSearch')" runat="server"
							Width="100%"></asp:dropdownlist></TD>
					<TD>
						<asp:label id="Label9" runat="server" CssClass="label">JobCode</asp:label></TD>
					<TD><asp:dropdownlist id="cboJobcode" CssClass="combo" onkeypress="checkKey('_ctl0_btnSearch')" runat="server"
							Width="100%" Visible="False"></asp:dropdownlist></TD>
				</TR>
				<TR runat="server" id="trStatus">
					<TD>
						<asp:label id="Label10" runat="server" CssClass="label"> Status</asp:label></TD>
					<TD colspan="5">
						<asp:RadioButtonList id="optStatus" runat="server" CssClass="option" Width="336px" RepeatDirection="Horizontal">
							<asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
							<asp:ListItem Value="0">Termination</asp:ListItem>
							<asp:ListItem Value="">All</asp:ListItem>
						</asp:RadioButtonList></TD>
				</TR>
				<TR style="DISPLAY: none">
					<TD></TD>
					<TD colSpan="5"></TD>
				</TR>
				<TR style="DISPLAY: none">
					<TD></TD>
					<TD colSpan="5"></TD>
				</TR>
				<TR style="DISPLAY:none">
					<td colspan="6" align="center">
						<INPUT type="hidden" runat="server" id="txtLevel2ID" style="WIDTH: 9px; HEIGHT: 22px" size="1">
						<INPUT type="hidden" runat="server" id="txtLevel3ID" style="WIDTH: 9px; HEIGHT: 22px" size="1">
						<INPUT id="txtLevel1ID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtLevel1ID"
							runat="server">
					</td>
				</TR>
			</TABLE>
			<script language="javascript">
//	document.getElementById("_ctl0:EmpHeaderSearchReport1:txtEmpID").focus();
			</script>
			<!--End table body-->
		</TD>
		<TD background="images/table_15.jpg" WIDTH="9">&nbsp;</TD>
	</TR>
	<TR>
		<TD WIDTH="5" HEIGHT="10">
			<IMG SRC="images/table_21.jpg" WIDTH="5" HEIGHT="10" ALT=""></TD>
		<TD background="images/table_23.jpg" HEIGHT="10">
			<IMG SRC="images/table_23.jpg" WIDTH="10" HEIGHT="10" ALT=""></TD>
		<TD WIDTH="9" HEIGHT="10">
			<IMG SRC="images/table_25.jpg" WIDTH="9" HEIGHT="10" ALT=""></TD>
	</TR>
</TABLE>
<!--End table-->
<script>
	/*function ChangeLanguage()
	{
		if ('<%=Session["LangID"]%>' == null || '<%=Session["LangID"]%>'  == "EN")
		{
			document.getElementById("_ctl0_EmpHeaderSearchReport1_optStatus_0").innerHTML = "Active"; 
			document.getElementById("_ctl0_EmpHeaderSearchReport1_optStatus_1").innerHTML = "Termination"; 
			document.getElementById("_ctl0_EmpHeaderSearchReport1_optStatus_2").innerHTML = "All"; 
		}
		else
		{
			document.getElementById("_ctl0_EmpHeaderSearchReport1_optStatus_0").value = "Hi?n di?n"; 
			document.getElementById("_ctl0_EmpHeaderSearchReport1_optStatus_1").value = "Ngh? vi?c"; 
			document.getElementById("_ctl0_EmpHeaderSearchReport1_optStatus_2").value = "T?t c?"; 
		}
	}
	ChangeLanguage();*/
</script>
