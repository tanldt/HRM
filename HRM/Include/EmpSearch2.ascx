<%@ Import Namespace ="iHRPCore.Com"%>
<%@ Import Namespace ="System.Data" %>
<%@ Import Namespace ="System.Data.SqlClient" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="EmpSearch2.ascx.cs" Inherits="iHRPCore.Include.EmpSearch2" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%
	string strLangID = Session["LangID"] == null?"EN":Session["LangID"].ToString().Trim();
	int Total1 = 0;
	int Total2 = 0;
	int Total3 = 0;
			
	DataTable rs1 = new DataTable();
	DataTable rs2 = new DataTable();		
	DataTable rs3 = new DataTable();	
	
	rs1 = clsCommon.GetDataTable("sp_GetDataComboLevel @TableName = 'LS_tblLevel1', @Language='" + strLangID + "'");
	rs2 = clsCommon.GetDataTable("sp_GetDataComboLevel @TableName = 'LS_tblLevel2', @Language='" + strLangID + "'");
	rs3 = clsCommon.GetDataTable("sp_GetDataComboLevel @TableName = 'LS_tblLevel3', @Language='" + strLangID + "'");
	
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
			DS1[0][<%=i%>]="<%=rs1.Rows[i]["LSCompanyCode"]%>";
			DS1[1][<%=i%>]="<%=rs1.Rows[i]["LSLevel1Code"]%>";
			DS1[2][<%=i%>]="<%=rs1.Rows[i]["Name"]%>";	
		<%}%>	
		
		var DS2 = new Array(3);
		DS2[0] = new Array(<%=Total2%>);
		DS2[1] = new Array(<%=Total2%>);
		DS2[2] = new Array(<%=Total2%>);	
		<% for(int i=0; i<Total2; i++) {%>		
			DS2[0][<%=i%>]="<%=rs2.Rows[i]["LSLevel1Code"]%>";
			DS2[1][<%=i%>]="<%=rs2.Rows[i]["LSLevel2Code"]%>";
			DS2[2][<%=i%>]="<%=rs2.Rows[i]["Name"]%>";	
		<%}%>		
		
		var DS3 = new Array(3);
		DS3[0] = new Array(<%=Total3%>);
		DS3[1] = new Array(<%=Total3%>);
		DS3[2] = new Array(<%=Total3%>);
		<% for(int i=0; i<Total3; i++) {%>		
			DS3[0][<%=i%>]="<%=rs3.Rows[i]["LSLevel2Code"]%>";
			DS3[1][<%=i%>]="<%=rs3.Rows[i]["LSLevel3Code"]%>";
			DS3[2][<%=i%>]="<%=rs3.Rows[i]["Name"]%>";	
		<%}%>	
</script>
<script language="javascript">	
	function ChangeCompany()
	{
		var all = document.getElementById("_ctl0:EmpHeaderSearch1:cboLevel2").length;
		var all1 = document.getElementById("_ctl0:EmpHeaderSearch1:cboLevel3").length;
		var all2 = document.getElementById("_ctl0:EmpHeaderSearch1:cboLevel1").length;
		for(i=0; i<all2; i++)
		{	
			document.getElementById("_ctl0:EmpHeaderSearch1:cboLevel1").remove(0);			
		};
		
		for(i=0; i<all; i++)
		{	
			document.getElementById("_ctl0:EmpHeaderSearch1:cboLevel2").remove(0);			
		};
		for(i=0; i<all1; i++)
		{	
			document.getElementById("_ctl0:EmpHeaderSearch1:cboLevel3").remove(0);			
		};
		var strCompany = document.getElementById("_ctl0:EmpHeaderSearch1:cboCompany").value; 
		document.getElementById("_ctl0:EmpHeaderSearch1:cboLevel1").add(new Option('',''));
		for(i=0; i<<%=Total1%>;i++)
		{
			if (DS1[0][i]==strCompany)
			{
				document.getElementById("_ctl0:EmpHeaderSearch1:cboLevel1").add(new Option(DS1[2][i],DS1[1][i]));
			};
		};		
		all = document.getElementById("_ctl0:EmpHeaderSearch1:cboLevel1").length;
		document.getElementById("_ctl0:EmpHeaderSearch1:cboLevel1").selectedIndex = 0;
		ChangeLevel1();			
	}
	
	function ChangeLevel1()
	{
		var all = document.getElementById("_ctl0:EmpHeaderSearch1:cboLevel2").length;
		var all1 = document.getElementById("_ctl0:EmpHeaderSearch1:cboLevel3").length;
		
		for(i=0; i<all; i++)
		{	
			document.getElementById("_ctl0:EmpHeaderSearch1:cboLevel2").remove(0);			
		};
		for(i=0; i<all1; i++)
		{	
			document.getElementById("_ctl0:EmpHeaderSearch1:cboLevel3").remove(0);			
		};
		var strLevel1ID = document.getElementById("_ctl0:EmpHeaderSearch1:cboLevel1").value; 
		document.getElementById("_ctl0:EmpHeaderSearch1:cboLevel2").add(new Option('',''));
		
		for(i=0; i<<%=Total2%>;i++)
		{
			if (DS2[0][i]==strLevel1ID)
			{
				document.getElementById("_ctl0:EmpHeaderSearch1:cboLevel2").add(new Option(DS2[2][i],DS2[1][i]));
			};
		};		
		all = document.getElementById("_ctl0:EmpHeaderSearch1:cboLevel2").length;
		document.getElementById("_ctl0:EmpHeaderSearch1:cboLevel2").selectedIndex = 0;
		document.getElementById("_ctl0:EmpHeaderSearch1:txtLevel1ID").value = strLevel1ID;
		ChangeLevel2();			
	}
	
	function ChangeLevel2()
	{
		var all = document.getElementById("_ctl0:EmpHeaderSearch1:cboLevel3").length;
		
		for(i=0; i<all; i++)
		{	
			document.getElementById("_ctl0:EmpHeaderSearch1:cboLevel3").remove(0);			
		};
		var strLevel2ID = document.getElementById("_ctl0:EmpHeaderSearch1:cboLevel2").value; 
		document.getElementById("_ctl0:EmpHeaderSearch1:cboLevel3").add(new Option('',''));
		
		for(i=0; i<<%=Total3%>;i++)
		{
			if (DS3[0][i]==strLevel2ID)
			{
				document.getElementById("_ctl0:EmpHeaderSearch1:cboLevel3").add(new Option(DS3[2][i],DS3[1][i]));
			};
		};		
		document.getElementById("_ctl0:EmpHeaderSearch1:txtLevel2ID").value = document.getElementById("_ctl0:EmpHeaderSearch1:cboLevel2").value;
		ChangeLevel3();	
	}
	function ChangeLevel3()
	{	
		document.getElementById("_ctl0:EmpHeaderSearch1:txtLevel3ID").value = document.getElementById("_ctl0:EmpHeaderSearch1:cboLevel3").value;
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
		<TD background="images/table_11.jpg" WIDTH="5">
			<IMG SRC="images/table_11.jpg" WIDTH="5" HEIGHT="9" ALT=""></TD>
		<TD background="images/table_13.jpg" align="center">
			<!--table body-->
			<TABLE id="Table1" cellSpacing="0" cellPadding="1" width="100%" border="0">
				<TR>
					<TD width="10%"><asp:label id="Label1" CssClass="label" runat="server"> Company</asp:label></TD>
					<TD width="40%"><%if(Request.Params["EmpID"] == null && Request.Params["EmpName"] == null){%>
						<asp:dropdownlist onkeypress="checkKey('_ctl0_btnSearch')" id="cboCompany" onchange="ChangeCompany()"
							runat="server" CssClass="combo" Width="95%"></asp:dropdownlist><%}%></TD>
					<TD width="10%"><asp:label id="Label4" CssClass="label" runat="server"> Dept</asp:label></TD>
					<TD width="40%"><%if(Request.Params["EmpID"] == null && Request.Params["EmpName"] == null){%><asp:dropdownlist id="cboLevel2" CssClass="combo" onkeypress="checkKey('_ctl0_btnSearch')" runat="server"
							Width="95%" onchange="ChangeLevel2()"></asp:dropdownlist><%}%></TD>
				</TR>
				<TR>
					<TD><asp:label id="Label3" CssClass="label" runat="server">Division</asp:label></TD>
					<TD><asp:dropdownlist id="cboLevel1" CssClass="combo" onkeypress="checkKey('_ctl0_btnSearch')" runat="server"
							Width="95%" onchange="ChangeLevel1()"></asp:dropdownlist></TD>
					<TD><asp:label id="Label6" CssClass="label" runat="server"> Section</asp:label></TD>
					<TD><asp:dropdownlist id="cboLevel3" CssClass="combo" runat="server" onkeypress="checkKey('_ctl0_btnSearch')"
							Width="95%" onchange="ChangeLevel3()"></asp:dropdownlist></TD>
				</TR>
				<TR style="DISPLAY:none">
					<td colspan="4" align="center">
						<INPUT type="hidden" runat="server" id="txtLevel2ID" style="WIDTH: 9px; HEIGHT: 22px" size="1"
							NAME="txtLevel2ID"> <INPUT type="hidden" runat="server" id="txtLevel3ID" style="WIDTH: 9px; HEIGHT: 22px" size="1"
							NAME="txtLevel3ID"> <INPUT id="txtLevel1ID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtLevel1ID"
							runat="server">
					</td>
				</TR>
			</TABLE>
			<!--End table body-->
		</TD>
		<TD background="images/table_15.jpg" WIDTH="9" HEIGHT="9">
			<IMG SRC="images/table_15.jpg" WIDTH="9" HEIGHT="9" ALT=""></TD>
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
<script language="javascript">
//	document.getElementById("_ctl0:EmpHeaderSearch1:txtEmpID").focus();
</script>
