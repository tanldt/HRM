<%@ Control Language="c#" AutoEventWireup="false" Codebehind="EmpSearch1.ascx.cs" Inherits="iHRPCore.Include.EmpSearch1" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
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
		var all = document.getElementById("_ctl0:EmpSearch11:cboLevel2").length;
		var all1 = document.getElementById("_ctl0:EmpSearch11:cboLevel3").length;
		var all2 = document.getElementById("_ctl0:EmpSearch11:cboLevel1").length;
		for(i=0; i<all2; i++)
		{	
			document.getElementById("_ctl0:EmpSearch11:cboLevel1").remove(0);			
		};
		
		for(i=0; i<all; i++)
		{	
			document.getElementById("_ctl0:EmpSearch11:cboLevel2").remove(0);			
		};
		for(i=0; i<all1; i++)
		{	
			document.getElementById("_ctl0:EmpSearch11:cboLevel3").remove(0);			
		};
		var strCompany = document.getElementById("_ctl0:EmpSearch11:cboCompany").value; 
		document.getElementById("_ctl0:EmpSearch11:cboLevel1").add(new Option('',''));
		for(i=0; i<<%=Total1%>;i++)
		{
			if (DS1[0][i]==strCompany)
			{
				document.getElementById("_ctl0:EmpSearch11:cboLevel1").add(new Option(DS1[2][i],DS1[1][i]));
			};
		};		
		all = document.getElementById("_ctl0:EmpSearch11:cboLevel1").length;
		document.getElementById("_ctl0:EmpSearch11:cboLevel1").selectedIndex = 0;
		ChangeLevel1();			
	}
	
	function ChangeLevel1()
	{
		var all = document.getElementById("_ctl0:EmpSearch11:cboLevel2").length;
		var all1 = document.getElementById("_ctl0:EmpSearch11:cboLevel3").length;
		
		for(i=0; i<all; i++)
		{	
			document.getElementById("_ctl0:EmpSearch11:cboLevel2").remove(0);			
		};
		for(i=0; i<all1; i++)
		{	
			document.getElementById("_ctl0:EmpSearch11:cboLevel3").remove(0);			
		};
		var strLevel1ID = document.getElementById("_ctl0:EmpSearch11:cboLevel1").value; 
		document.getElementById("_ctl0:EmpSearch11:cboLevel2").add(new Option('',''));
		
		for(i=0; i<<%=Total2%>;i++)
		{
			if (DS2[0][i]==strLevel1ID)
			{
				document.getElementById("_ctl0:EmpSearch11:cboLevel2").add(new Option(DS2[2][i],DS2[1][i]));
			};
		};		
		all = document.getElementById("_ctl0:EmpSearch11:cboLevel2").length;
		document.getElementById("_ctl0:EmpSearch11:cboLevel2").selectedIndex = 0;
		document.getElementById("_ctl0:EmpSearch11:txtLevel1ID").value = strLevel1ID;
		ChangeLevel2();			
	}
	
	function ChangeLevel2()
	{
		var all = document.getElementById("_ctl0:EmpSearch11:cboLevel3").length;
		
		for(i=0; i<all; i++)
		{	
			document.getElementById("_ctl0:EmpSearch11:cboLevel3").remove(0);			
		};
		var strLevel2ID = document.getElementById("_ctl0:EmpSearch11:cboLevel2").value; 
		document.getElementById("_ctl0:EmpSearch11:cboLevel3").add(new Option('',''));
		
		for(i=0; i<<%=Total3%>;i++)
		{
			if (DS3[0][i]==strLevel2ID)
			{
				document.getElementById("_ctl0:EmpSearch11:cboLevel3").add(new Option(DS3[2][i],DS3[1][i]));
			};
		};		
		document.getElementById("_ctl0:EmpSearch11:txtLevel2ID").value = document.getElementById("_ctl0:EmpSearch11:cboLevel2").value;
		ChangeLevel3();	
	}
	function ChangeLevel3()
	{	
		document.getElementById("_ctl0:EmpSearch11:txtLevel3ID").value = document.getElementById("_ctl0:EmpSearch11:cboLevel3").value;
	}
</script>
<!--table-->
<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD width="5" height="5"><IMG height="5" alt="" src="images/table_01.jpg" width="5"></TD>
		<TD background="images/table_03.jpg" height="5"><IMG height="5" alt="" src="images/table_03.jpg" width="10"></TD>
		<TD width="9" height="5"><IMG height="5" alt="" src="images/table_05.jpg" width="9"></TD>
	</TR>
	<TR>
		<TD width="5" background="images/table_11.jpg"><IMG height="9" alt="" src="images/table_11.jpg" width="5"></TD>
		<TD align="center" background="images/table_13.jpg">
			<!--table body-->
			<TABLE id="Table1" cellSpacing="0" cellPadding="1" width="100%" border="0">
				<TR>
					<TD width="8%"><asp:label id="Label7" runat="server" CssClass="label">Emp ID</asp:label></TD>
					<TD width="17%"><asp:textbox onkeypress="checkKey('_ctl0_btnSearch')" id="txtEmpID" runat="server" CssClass="input"
							Width="95%"></asp:textbox><%if(Request.Params["EmpID"] == null && Request.Params["EmpName"] == null){%><%}%></TD>
					<TD width="8%"><asp:label id="Label2" runat="server" CssClass="label">Emp Name</asp:label></TD>
					<TD width="27%"><asp:textbox onkeypress="checkKey('_ctl0_btnSearch')" id="txtEmpName" runat="server" CssClass="input"
							Width="95%"></asp:textbox><%if(Request.Params["EmpID"] == null && Request.Params["EmpName"] == null){%><%}%></TD>
					<TD width="8%"><asp:label id="Label1" runat="server" CssClass="label"> Company</asp:label></TD>
					<TD width="33%"><asp:dropdownlist onkeypress="checkKey('_ctl0_btnSearch')" id="cboCompany" runat="server" CssClass="combo"
							Width="95%" onchange="ChangeCompany()"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 24px"><asp:label id="Label3" runat="server" CssClass="label">Division</asp:label></TD>
					<TD style="HEIGHT: 24px"><asp:dropdownlist onkeypress="checkKey('_ctl0_btnSearch')" id="cboLevel1" runat="server" CssClass="combo"
							Width="95%" onchange="ChangeLevel1()"></asp:dropdownlist></TD>
					<TD><asp:label id="Label4" runat="server" CssClass="label"> Dept</asp:label></TD>
					<TD><asp:dropdownlist onkeypress="checkKey('_ctl0_btnSearch')" id="cboLevel2" runat="server" CssClass="combo"
							Width="95%" onchange="ChangeLevel2()"></asp:dropdownlist></TD>
					<TD><asp:label id="Label6" runat="server" CssClass="label"> Section</asp:label></TD>
					<TD><asp:dropdownlist onkeypress="checkKey('_ctl0_btnSearch')" id="cboLevel3" runat="server" CssClass="combo"
							Width="95%" onchange="ChangeLevel3()"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 24px"><asp:label id="Label10" runat="server" CssClass="label"> Status</asp:label></TD>
					<TD colSpan="5"><asp:radiobuttonlist id="optStatus" runat="server" CssClass="option" Width="336px" RepeatDirection="Horizontal">
							<asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
							<asp:ListItem Value="2">Termination</asp:ListItem>
							<asp:ListItem Value="">All</asp:ListItem>
						</asp:radiobuttonlist></TD>
				</TR>
				<TR style="DISPLAY: none">
					<td align="center" colSpan="6"><INPUT id="txtLevel2ID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtLevel2ID"
							runat="server"> <INPUT id="txtLevel3ID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtLevel3ID"
							runat="server"> <INPUT id="txtLevel1ID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtLevel1ID"
							runat="server">
					</td>
				</TR>
			</TABLE>
			<script language="javascript">
//	document.getElementById("_ctl0:EmpHeaderSearch1:txtEmpID").focus();
			</script>
			<!--End table body--></TD>
		<TD width="9" background="images/table_15.jpg" height="9"><IMG height="9" alt="" src="images/table_15.jpg" width="9"></TD>
	</TR>
	<TR>
		<TD width="5" height="10"><IMG height="10" alt="" src="images/table_21.jpg" width="5"></TD>
		<TD background="images/table_23.jpg" height="10"><IMG height="10" alt="" src="images/table_23.jpg" width="10"></TD>
		<TD width="9" height="10"><IMG height="10" alt="" src="images/table_25.jpg" width="9"></TD>
	</TR>
</TABLE>
<!--End table-->
