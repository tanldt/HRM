<%@ Control Language="c#" AutoEventWireup="false" Codebehind="PayRollSchedule.ascx.cs" Inherits="iHRPCore.MdlPR.PayRollSchedule" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Import Namespace ="iHRPCore.Com"%>
<%@ Import Namespace ="System.Data" %>
<%@ Import Namespace ="System.Data.SqlClient" %>
<%
	string strLangID = Session["LangID"] == null?"EN":Session["LangID"].ToString().Trim();
	int Total2 = 0;
	int Total3 = 0;
	int Total1 = 0;
	
	DataTable rs2 = new DataTable();		
	DataTable rs3 = new DataTable();	
	DataTable rs1 = new DataTable();
	
	rs1 = clsCommon.GetDataTable("sp_GetDataCombo @TableName = 'LS_tblLevel1'");
	rs2 = clsCommon.GetDataTable("sp_GetDataCombo @TableName = 'LS_tblLevel2'");
	rs3 = clsCommon.GetDataTable("sp_GetDataCombo @TableName = 'LS_tblLevel3'");	
	
	Total1 = rs1.Rows.Count;
	Total2 = rs2.Rows.Count;
	Total3 = rs3.Rows.Count;		
%>
<script language="javascript">
		//array for level1
		var DS1 = new Array(3);
		DS1[0] = new Array(<%=Total1%>);
		DS1[1] = new Array(<%=Total1%>);
		DS1[2] = new Array(<%=Total1%>);	
		<% for(int i=0; i<Total1; i++) {%>		
			DS1[0][<%=i%>]="<%=rs1.Rows[i]["LSCompanyCode"]%>";
			DS1[1][<%=i%>]="<%=rs1.Rows[i]["LSLevel1Code"]%>";
			<% if (strLangID=="EN") {%>
				DS1[2][<%=i%>]="<%=rs1.Rows[i]["Name"]%>";	
			<%} else{%>
				DS1[2][<%=i%>]="<%=rs1.Rows[i]["VNName"]%>";
			<%}%>	
		<%}%>
		//array for level2
		var DS2 = new Array(3);
		DS2[0] = new Array(<%=Total2%>);
		DS2[1] = new Array(<%=Total2%>);
		DS2[2] = new Array(<%=Total2%>);	
		<% for(int i=0; i<Total2; i++) {%>		
			DS2[0][<%=i%>]="<%=rs2.Rows[i]["LSLevel1Code"]%>";
			DS2[1][<%=i%>]="<%=rs2.Rows[i]["LSLevel2Code"]%>";
			<% if (strLangID=="EN") {%>
				DS2[2][<%=i%>]="<%=rs2.Rows[i]["Name"]%>";	
			<%} else{%>
				DS2[2][<%=i%>]="<%=rs2.Rows[i]["VNName"]%>";
			<%}%>	
		<%}%>		
		//array for level3
		var DS3 = new Array(3);
		DS3[0] = new Array(<%=Total3%>);
		DS3[1] = new Array(<%=Total3%>);
		DS3[2] = new Array(<%=Total3%>);
		<% for(int i=0; i<Total3; i++) {%>		
			DS3[0][<%=i%>]="<%=rs3.Rows[i]["LSLevel2Code"]%>";
			DS3[1][<%=i%>]="<%=rs3.Rows[i]["LSLevel3Code"]%>";
			<% if (strLangID=="EN") {%>
				DS3[2][<%=i%>]="<%=rs3.Rows[i]["Name"]%>";	
			<%} else{%>
				DS3[2][<%=i%>]="<%=rs3.Rows[i]["VNName"]%>";
			<%}%>	
		<%}%>	
</script>
<script language="javascript">
	function ChangeCompany()
	{
		var al = document.getElementById("_ctl0_cboLevel1ID").length;
		var all = document.getElementById("_ctl0_cboLevel2ID").length;
		var all1 = document.getElementById("_ctl0_cboLevel3ID").length;
		
		for(i=0; i<al; i++)
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
		var strCompanyID = document.getElementById("_ctl0_cboCompanyID").value;
		document.getElementById("_ctl0_cboLevel1ID").add(new Option('',''));
		
		for(i=0; i<<%=Total1%>;i++)
		{
			if (DS1[0][i]==strCompanyID)
			{
				document.getElementById("_ctl0_cboLevel1ID").add(new Option(DS1[2][i],DS1[1][i]));
			};
		};
		al = document.getElementById("_ctl0_cboLevel1ID").length;
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
		all = document.getElementById("_ctl0_cboLevel2ID").length;
		document.getElementById("_ctl0_cboLevel2ID").selectedIndex = 0;
		document.getElementById("_ctl0_txtLevel1ID").value = document.getElementById("_ctl0_cboLevel1ID").value;
		ChangeLevel2();			
	}
	
	function ChangeLevel2()
	{
		var all = document.getElementById("_ctl0_cboLevel2ID").length;
		
		for(i=0; i<all; i++)
		{	
			document.getElementById("_ctl0_cboLevel3ID").remove(0);			
		};
		var strLevel2ID = document.getElementById("_ctl0_cboLevel2ID").value; 
		document.getElementById("_ctl0_cboLevel3ID").add(new Option('',''));
		
		for(i=0; i<<%=Total3%>;i++)
		{
			if (DS3[0][i]==strLevel2ID)
			{
				document.getElementById("_ctl0_cboLevel3ID").add(new Option(DS3[2][i],DS3[1][i]));
			};
		};		
		document.getElementById("_ctl0_txtLevel2ID").value = document.getElementById("_ctl0_cboLevel2ID").value;
		ChangeLevel3();	
	}
	function ChangeLevel3()
	{
		document.getElementById("_ctl0_txtLevel3ID").value = document.getElementById("_ctl0_cboLevel3ID").value;
	}
</script>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="97%" border="0">
	<TR>
		<TD align="left" width="100%"><asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center"></TD>
	</TR>
	<TR>
		<TD align="center"><!-- start detail for input form -->
			<TABLE cellSpacing="0" cellPadding="1" border="0">
				<TR>
					<TD></TD>
					<TD vAlign="top" colSpan="7"></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblNote" runat="server" CssClass="labelrequire" Width="88px">Salary Period</asp:label></TD>
					<TD></TD>
					<TD><asp:textbox id="txtMonth" onblur="javascript:CheckMonthYear(this)" runat="server" CssClass="input"
							Width="75px" MaxLength="7"></asp:textbox></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 27px"><asp:label id="lblCompany" runat="server" CssClass="label" Width="50px"> Company</asp:label></TD>
					<TD style="HEIGHT: 27px"></TD>
					<TD><asp:dropdownlist id="cboCompanyID" runat="server" CssClass="combo" Width="200px" onchange="ChangeCompany()"></asp:dropdownlist></TD>
					<TD style="HEIGHT: 27px"></TD>
					<TD style="HEIGHT: 27px"><asp:label id="Label3" runat="server" CssClass="label" Width="50px">Division</asp:label></TD>
					<TD style="HEIGHT: 27px"><asp:dropdownlist id="cboLevel1ID" runat="server" CssClass="combo" Width="200px" onchange="ChangeLevel1()"></asp:dropdownlist></TD>
					<TD style="HEIGHT: 27px"></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 23px"><asp:label id="lblFromDepartment" runat="server" CssClass="label" Width="50px">Department</asp:label></TD>
					<TD style="HEIGHT: 23px"></TD>
					<TD style="HEIGHT: 23px"><asp:dropdownlist id="cboLevel2ID" runat="server" CssClass="combo" Width="200px" onchange="ChangeLevel2()"></asp:dropdownlist></TD>
					<TD style="HEIGHT: 23px"></TD>
					<TD style="HEIGHT: 23px"><asp:label id="lblFromSection" runat="server" CssClass="label" Width="50px">Section</asp:label></TD>
					<TD style="HEIGHT: 23px"><asp:dropdownlist id="cboLevel3ID" runat="server" CssClass="combo" Width="200px" onchange="ChangeLevel3()"></asp:dropdownlist></TD>
					<TD style="HEIGHT: 23px"></TD>
				</TR>
				<TR>
					<TD height="20">
						<asp:label id="EmpID" runat="server" CssClass="labelrequire" Width="88px">Emp ID</asp:label></TD>
					<TD height="20"></TD>
					<TD height="20">
						<asp:textbox id="txtEmpID" onblur="javascript:CheckMonthYear(this)" runat="server" CssClass="input"
							Width="75px" MaxLength="10"></asp:textbox></TD>
					<TD height="20"></TD>
					<TD height="20"></TD>
					<TD height="20"></TD>
					<TD height="20"></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD align="center" colSpan="6"><asp:linkbutton id="btnCalculate" accessKey="S" runat="server" CssClass="btnSave" ToolTip="ALT+S">Calculate</asp:linkbutton>&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnPaySlip" accessKey="S" runat="server" CssClass="btnSave" ToolTip="ALT+S">Pay slip</asp:linkbutton>&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnSchedule" accessKey="S" runat="server" CssClass="btnSave" ToolTip="ALT+S">Pay Roll</asp:linkbutton></TD>
				</TR>
			</TABLE>
			<INPUT id="txtLevel1ID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtLevel1ID"
				runat="server"> <INPUT id="txtLevel2ID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtLevel2ID"
				runat="server"> <INPUT id="txtLevel3ID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtLevel3ID"
				runat="server">&nbsp;
		</TD>
	</TR>
	<TR>
		<TD noWrap align="center"></TD>
	</TR> <!-- end button for input form -->
	<TR id="trGrid" runat="server">
		<TD align="center"><!-- start grid for input form --> <!-- end grid for input form --></TD>
	</TR>
</TABLE>
<script language="javascript">
<!--
function validform(){
	if(checkisnull('txtMonth')==false)  return false;	
	return true;
}
function checkisnull(obj)
	{
		if(document.getElementById('_ctl0_' + obj).value=="")
			{
				alert('Please select salary period!');
				document.getElementById('_ctl0_' + obj).focus();
				return false;
			}
		else
			return true;
	}
//-->
</script>
