<%@ Import Namespace ="iHRPCore.Com"%>
<%@ Import Namespace ="System.Data" %>
<%@ Import Namespace ="System.Data.SqlClient" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Restructure.ascx.cs" Inherits="iHRPCore.MdlHR.Restructure" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
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
<script language="javascript">
	function ChangeFCompany()
	{
		var al = document.getElementById("_ctl0_cboFLevel1ID").length;
		var all = document.getElementById("_ctl0_cboFLevel2ID").length;
		var all1 = document.getElementById("_ctl0_cboFLevel3ID").length;
		
		for(i=0; i<al; i++)
		{	
			document.getElementById("_ctl0_cboFLevel1ID").remove(0);			
		};
		for(i=0; i<all; i++)
		{	
			document.getElementById("_ctl0_cboFLevel2ID").remove(0);			
		};
		for(i=0; i<all1; i++)
		{	
			document.getElementById("_ctl0_cboFLevel3ID").remove(0);			
		};
		var strCompanyID = document.getElementById("_ctl0_cboFCompanyID").value;
		document.getElementById("_ctl0_cboFLevel1ID").add(new Option('',''));
		
		for(i=0; i<<%=Total1%>;i++)
		{
			if (DS1[0][i]==strCompanyID)
			{
				document.getElementById("_ctl0_cboFLevel1ID").add(new Option(DS1[2][i],DS1[1][i]));
			};
		};
		al = document.getElementById("_ctl0_cboFLevel1ID").length;
		document.getElementById("_ctl0_cboFLevel1ID").selectedIndex = 0;
		ChangeFLevel1();
	}
	function ChangeFLevel1()
	{		
		var all = document.getElementById("_ctl0_cboFLevel2ID").length;
		var all1 = document.getElementById("_ctl0_cboFLevel3ID").length;
		
		for(i=0; i<all; i++)
		{	
			document.getElementById("_ctl0_cboFLevel2ID").remove(0);			
		};
		for(i=0; i<all1; i++)
		{	
			document.getElementById("_ctl0_cboFLevel3ID").remove(0);			
		};
		var strLevel1ID = document.getElementById("_ctl0_cboFLevel1ID").value; 
		document.getElementById("_ctl0_cboFLevel2ID").add(new Option('',''));
		
		for(i=0; i<<%=Total2%>;i++)
		{
			if (DS2[0][i]==strLevel1ID)
			{
				document.getElementById("_ctl0_cboFLevel2ID").add(new Option(DS2[2][i],DS2[1][i]));
			};
		};
		all = document.getElementById("_ctl0_cboFLevel2ID").length;
		document.getElementById("_ctl0_cboFLevel2ID").selectedIndex = 0;
		document.getElementById("_ctl0_txtFLevel1ID").value = document.getElementById("_ctl0_cboFLevel1ID").value;
		ChangeFLevel2();
	}
	
	function ChangeFLevel2()
	{
		var all = document.getElementById("_ctl0_cboFLevel2ID").length;
		
		for(i=0; i<all; i++)
		{	
			document.getElementById("_ctl0_cboFLevel3ID").remove(0);			
		};
		var strLevel2ID = document.getElementById("_ctl0_cboFLevel2ID").value; 
		document.getElementById("_ctl0_cboFLevel3ID").add(new Option('',''));
		
		for(i=0; i<<%=Total3%>;i++)
		{
			if (DS3[0][i]==strLevel2ID)
			{
				document.getElementById("_ctl0_cboFLevel3ID").add(new Option(DS3[2][i],DS3[1][i]));
			};
		};		
		document.getElementById("_ctl0_txtFLevel2ID").value = document.getElementById("_ctl0_cboFLevel2ID").value;
		ChangeFLevel3();	
	}
	function ChangeFLevel3()
	{
		document.getElementById("_ctl0_txtFLevel3ID").value = document.getElementById("_ctl0_cboFLevel3ID").value;
	}
</script>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="97%" border="0">
	<TR>
		<TD colSpan="5"><asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label></TD>
	</TR>
	<tr>
		<TD align="center"><!-- start detail for input form -->
			<TABLE cellSpacing="0" cellPadding="1" width="100%" border="0">
				<TR>
					<TD colSpan="2">
						<asp:label id="Label7" CssClass="labelSubTitle" runat="server">Filter/Current Information</asp:label></TD>
				</TR>
				<tr>
					<td>
						<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD width="15%" style="HEIGHT: 10px">
									<asp:label id="lblRelationID" runat="server" CssClass="label" Width="100%"> Company</asp:label></TD>
								<TD width="30%" style="HEIGHT: 10px">
									<asp:dropdownlist id="cboFCompanyID" runat="server" onchange="ChangeFCompany()" CssClass="combo" width="100%"></asp:dropdownlist></TD>
								<TD width="4%" style="HEIGHT: 10px"></TD>
								<TD width="15%" style="HEIGHT: 10px">
									<asp:label id="lblRelationEmpID" runat="server" CssClass="label" Width="100%"> Division</asp:label></TD>
								<TD width="36%" style="HEIGHT: 10px">
									<asp:dropdownlist id="cboFLevel1ID" runat="server" onchange="ChangeFLevel1()" CssClass="combo" width="100%"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD>
									<asp:label id="lblOtherCompanyID" runat="server" CssClass="label" Width="100%"> Department</asp:label></TD>
								<TD>
									<asp:dropdownlist id="cboFLevel2ID" runat="server" onchange="ChangeFLevel2()" CssClass="combo" width="100%"></asp:dropdownlist></TD>
								<TD></TD>
								<TD>
									<asp:label id="lblLastName" runat="server" CssClass="label" Width="100%"> Section</asp:label></TD>
								<TD>
									<asp:dropdownlist id="cboFLevel3ID" runat="server" onchange="ChangeFLevel3()" CssClass="combo" width="100%"></asp:dropdownlist></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
				<TR>
					<TD colSpan="5">
						<HR align="center" width="95%">
					</TD>
				</TR>
				<TR>
					<TD colSpan="5"><INPUT id="optRestructure" runat="server" onclick="javascript:ShowRow('trRestructure');"
							type="radio" CHECKED value="Restructure" name="optType">
						<asp:label id="Label15" runat="server" CssClass="labelSubTitle">Restructure Information</asp:label><INPUT id="txtLevel1ID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtLevel1ID"
							runat="server"><INPUT id="txtLevel2ID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtLevel2ID"
							runat="server"><INPUT id="txtLevel3ID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtLevel3ID"
							runat="server"></TD>
				</TR>
				<tr id="trRestructure">
					<td>
						<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD width="15%">
									<asp:label id="Label5" runat="server" CssClass="labelRequire" Width="100%">Effective date</asp:label></TD>
								<TD width="30%">
									<asp:textbox id="txtFromDate" onblur="JavaScript:CheckDate(this)" runat="server" CssClass="input"
										Width="80px" MaxLength="10"></asp:textbox>&nbsp;<INPUT class="btnCal" onclick="ShowCalendar('_ctl0_txtFromDate')" type="button"></TD>
								<TD width="4%"></TD>
								<TD width="15%">
									<asp:label id="lblStatusChange" runat="server" CssClass="labelRequire" Width="100%">Status change</asp:label></TD>
								<TD width="36%">
									<asp:dropdownlist id="cboStatusID" runat="server" CssClass="select" width="100%"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 9px">
									<asp:label id="Label1" runat="server" CssClass="label" Width="100%">Company</asp:label></TD>
								<TD style="HEIGHT: 9px">
									<asp:dropdownlist id="cboCompanyID" runat="server" onchange="ChangeCompany()" CssClass="combo" width="100%"></asp:dropdownlist></TD>
								<TD></TD>
								<TD style="HEIGHT: 9px">
									<asp:label id="Label4" runat="server" CssClass="label" Width="100%">Section</asp:label></TD>
								<TD style="HEIGHT: 9px">
									<asp:dropdownlist id="cboLevel3ID" runat="server" onchange="ChangeLevel3()" CssClass="combo" width="100%"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD>
									<asp:label id="Label21" runat="server" CssClass="label" Width="100%">Location</asp:label></TD>
								<TD>
									<asp:dropdownlist id="cboLocationID" runat="server" CssClass="combo" Width="100%" onchange="ChangeLevel1()"></asp:dropdownlist></TD>
								<TD></TD>
								<TD>
									<asp:label id="lblFromPosition" runat="server" CssClass="label" Width="100%">Position</asp:label></TD>
								<TD>
									<asp:dropdownlist id="cboPositionID" runat="server" CssClass="combo" Width="100%"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD>
									<asp:label id="Label2" runat="server" CssClass="label" Width="100%">Division</asp:label></TD>
								<TD>
									<asp:dropdownlist id="cboLevel1ID" runat="server" onchange="ChangeLevel1()" CssClass="combo" width="100%"></asp:dropdownlist></TD>
								<TD></TD>
								<TD>
									<asp:label id="lblFromJobCode" runat="server" CssClass="label" Width="100%">Job code</asp:label></TD>
								<TD>
									<asp:dropdownlist id="cboJobCodeID" runat="server" CssClass="combo" Width="100%"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD>
									<asp:label id="Label3" runat="server" CssClass="label" Width="100%">Department</asp:label></TD>
								<TD>
									<asp:dropdownlist id="cboLevel2ID" runat="server" onchange="ChangeLevel2()" CssClass="combo" width="100%"></asp:dropdownlist></TD>
								<TD></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD></TD>
								<TD></TD>
								<TD></TD>
								<TD></TD>
							</TR>
						</TABLE>
						&nbsp; <INPUT id="txtFLevel1ID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtFLevel1ID"
							runat="server"><INPUT id="txtFLevel2ID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtFLevel2ID"
							runat="server"><INPUT id="txtFLevel3ID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtFLevel3ID"
							runat="server">
					</td>
				</tr>
				<TR>
					<TD colSpan="5">
						<HR align="center" width="95%">
					</TD>
				</TR>
				<TR>
					<TD colSpan="5"><INPUT id="optQuickUpdate" runat="server" onclick="javascript:ShowRow('trQuickUpdate');"
							type="radio" value="QuickUpdate" name="optType">
						<asp:label id="Label14" runat="server" CssClass="labelSubTitle">Quick update for employees by filter</asp:label></TD>
				</TR>
				<tr id="trQuickUpdate" style="DISPLAY:none">
					<td>
						<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD width="15%"></TD>
								<TD width="30%">
									<asp:label id="Label10" runat="server" CssClass="label" Width="100%">Current</asp:label></TD>
								<TD width="4%"></TD>
								<TD width="15%"></TD>
								<TD width="36%">
									<asp:label id="Label11" runat="server" CssClass="label" Width="100%">Change to</asp:label></TD>
							</TR>
							<TR>
								<TD>
									<asp:label id="Label12" runat="server" CssClass="label" Width="100%">Job class</asp:label></TD>
								<TD>
									<asp:dropdownlist id="cboCJobClassID" runat="server" CssClass="combo" width="100%"></asp:dropdownlist></TD>
								<TD></TD>
								<TD>
									<asp:label id="Label16" runat="server" CssClass="label" Width="100%">Job class</asp:label></TD>
								<TD>
									<asp:dropdownlist id="cboJobClassID" runat="server" CssClass="combo" width="100%"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD>
									<asp:label id="Label13" CssClass="label" runat="server" Width="100%">Regional job code</asp:label></TD>
								<TD>
									<asp:dropdownlist id="cboCRegionalJobCodeID" runat="server" CssClass="combo" Width="100%"></asp:dropdownlist></TD>
								<TD></TD>
								<TD>
									<asp:label id="Label17" runat="server" CssClass="label" Width="100%">Regional job code</asp:label></TD>
								<TD>
									<asp:dropdownlist id="cboRegionalJobCodeID" runat="server" CssClass="combo" Width="100%"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD>
									<asp:label id="Label9" runat="server" CssClass="label" Width="100%">Level grade</asp:label></TD>
								<TD>
									<asp:dropdownlist id="cboCLevelGradeID" runat="server" CssClass="combo" width="100%"></asp:dropdownlist></TD>
								<TD></TD>
								<TD>
									<asp:label id="Label18" runat="server" CssClass="label" Width="100%">Level grade</asp:label></TD>
								<TD>
									<asp:dropdownlist id="cboLevelGradeID" runat="server" CssClass="combo" width="100%"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD>
									<asp:label id="Label6" runat="server" CssClass="label" Width="100%">Building block</asp:label></TD>
								<TD>
									<asp:dropdownlist id="cboCBuildingBlockID" runat="server" CssClass="combo" Width="100%"></asp:dropdownlist></TD>
								<TD></TD>
								<TD>
									<asp:label id="Label19" runat="server" CssClass="label" Width="100%">Building block</asp:label></TD>
								<TD>
									<asp:dropdownlist id="cboBuildingBlockID" runat="server" CssClass="combo" Width="100%"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD>
									<asp:label id="Label8" runat="server" CssClass="label" Width="100%">Rank</asp:label></TD>
								<TD>
									<asp:dropdownlist id="cboCRankID" runat="server" CssClass="combo" width="100%"></asp:dropdownlist></TD>
								<TD></TD>
								<TD>
									<asp:label id="Label20" runat="server" CssClass="label" Width="100%">Rank</asp:label></TD>
								<TD>
									<asp:dropdownlist id="cboRankID" runat="server" CssClass="combo" width="100%"></asp:dropdownlist></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE> <!-- end button for input form -->
		</TD>
	</tr>
	<TR>
		<TD noWrap align="center" height="20">
			<HR align="center" width="95%">
		</TD>
	</TR>
	<TR>
		<TD noWrap align="center" height="20">
			<asp:linkbutton id="btnSave" accessKey="S" runat="server" CssClass="btnSave" ToolTip="ALT+S">Save</asp:linkbutton></TD>
	</TR> <!-- end button for input form -->
</TABLE>
<script language="javascript">
<!--
function validform(){
	if(document.getElementById("_ctl0_optRestructure").checked == true)
	{
		if(checkisnull('txtFromDate')==false)  return false;
		if(checkisnull('cboStatusID')==false)  return false;
	}
	return true;
}
function checkisnull(obj)
	{
		if(document.getElementById('_ctl0_' + obj).value=="")
			{
				alert('Data not left blank, please input data!');
				document.getElementById('_ctl0_' + obj).focus();
				return false;
			}
		else
			return true;
	}
//-->
</script>
<SCRIPT language="javascript">
	function ShowRow(strType)
	{
		if (strType == "trRestructure")
		{
			document.getElementById("trRestructure").style.display = "";
			document.getElementById("trQuickUpdate").style.display = "none";
		}
		else if (strType == "trQuickUpdate")
		{
			document.getElementById("trQuickUpdate").style.display = "";
			document.getElementById("trRestructure").style.display = "none";
		}
	}
</SCRIPT>
