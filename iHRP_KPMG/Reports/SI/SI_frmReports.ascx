<%@ Control Language="c#" AutoEventWireup="false" Codebehind="SI_frmReports.ascx.cs" Inherits="iHRPCore.Reports.SI.SI_frmReports" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Import Namespace ="System.Data.SqlClient" %>
<%@ Import Namespace ="System.Data" %>
<%@ Import Namespace ="iHRPCore.Com"%>
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
	function ChangeCompanyC04NDS()
	{
		var all2 = document.getElementById("_ctl0:cboLSLevel1ID8").length;
		
		for(i=0; i<all2; i++)
		{	
			document.getElementById("_ctl0:cboLSLevel1ID8").remove(0);			
		};
		
		var strCompany = document.getElementById("_ctl0:cboLSCompanyID8").value; 
		document.getElementById("_ctl0:cboLSLevel1ID8").add(new Option('',''));
		for(i=0; i<<%=Total1%>;i++)
		{
			if (DS1[0][i]==strCompany)
			{
				document.getElementById("_ctl0:cboLSLevel1ID8").add(new Option(DS1[2][i],DS1[1][i]));
			};
		};
		
		all2 = document.getElementById("_ctl0:cboLSLevel1ID8").length;
		document.getElementById("_ctl0:cboLSLevel1ID8").selectedIndex = 0;
		
	}
	function ChangeCompany()
	{
	alert('aaaa');
		var all = document.getElementById("_ctl0:cboLSLevel2ID10").length;
		var all1 = document.getElementById("_ctl0:cboLSLevel3ID10").length;
		var all2 = document.getElementById("_ctl0:cboLSLevel1ID10").length;
		
		for(i=0; i<all2; i++)
		{	
			document.getElementById("_ctl0:cboLSLevel1ID10").remove(0);			
		};
		
		for(i=0; i<all; i++)
		{	
			document.getElementById("_ctl0:cboLSLevel2ID10").remove(0);			
		};
		for(i=0; i<all1; i++)
		{	
			document.getElementById("_ctl0:cboLSLevel3ID10").remove(0);			
		};
		var strCompany = document.getElementById("_ctl0:cboLSCompanyID10").value; 
		document.getElementById("_ctl0:cboLSLevel1ID10").add(new Option('',''));
		for(i=0; i<<%=Total1%>;i++)
		{
			if (DS1[0][i]==strCompany)
			{
				document.getElementById("_ctl0:cboLSLevel1ID10").add(new Option(DS1[2][i],DS1[1][i]));
			};
		};	
		
		all = document.getElementById("_ctl0:cboLSLevel1ID10").length;
		document.getElementById("_ctl0:cboLSLevel1ID10").selectedIndex = 0;
		ChangeLevel1();			
	}
	
	function ChangeLevel1()
	{
		
		var all = document.getElementById("_ctl0:cboLSLevel2ID10").length;
		var all1 = document.getElementById("_ctl0:cboLSLevel3ID10").length;
		
		for(i=0; i<all; i++)
		{	
			document.getElementById("_ctl0:cboLSLevel2ID10").remove(0);			
		};
		for(i=0; i<all1; i++)
		{	
			document.getElementById("_ctl0:cboLSLevel3ID10").remove(0);			
		};
		var strLevel1ID = document.getElementById("_ctl0:cboLSLevel1ID10").value; 
		document.getElementById("_ctl0:cboLSLevel2ID10").add(new Option('',''));
		
		for(i=0; i<<%=Total2%>;i++)
		{
			if (DS2[0][i]==strLevel1ID)
			{
				document.getElementById("_ctl0:cboLSLevel2ID10").add(new Option(DS2[2][i],DS2[1][i]));
			};
		};		
		all = document.getElementById("_ctl0:cboLSLevel2ID10").length;
		document.getElementById("_ctl0:cboLSLevel2ID10").selectedIndex = 0;
		document.getElementById("_ctl0:txtLevel1ID").value = strLevel1ID;
		document.getElementById("_ctl0:txtLevel2ID").value ="";
		//ChangeLevel2();			
	}
	
	function ChangeLevel2()
	{
		var all = document.getElementById("_ctl0:cboLSLevel3ID10").length;
		
		for(i=0; i<all; i++)
		{	
			document.getElementById("_ctl0:cboLSLevel3ID10").remove(0);			
		};
		var strLevel2ID = document.getElementById("_ctl0:cboLSLevel2ID10").value; 
		document.getElementById("_ctl0:cboLSLevel3ID10").add(new Option('',''));
		
		for(i=0; i<<%=Total3%>;i++)
		{
			if (DS3[0][i]==strLevel2ID)
			{
				document.getElementById("_ctl0:cboLSLevel3ID10").add(new Option(DS3[2][i],DS3[1][i]));
			};
		};		
		document.getElementById("_ctl0:txtLevel2ID").value = document.getElementById("_ctl0:cboLSLevel2ID10").value;
		ChangeLevel3();	
	}
	function ChangeLevel3()
	{	
		document.getElementById("_ctl0:txtLevel3ID").value = document.getElementById("_ctl0:cboLSLevel3ID10").value;
	}
</script>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="97%" border="0">
	<TR>
		<TD align="center"><asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label><INPUT id="Hidden1" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtLevel2ID"
				runat="server"><INPUT id="txtLevel3ID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtLevel3ID"
				runat="server"><INPUT id="txtLevel1ID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtLevel1ID"
				runat="server"><INPUT id="hdbtnText" style="WIDTH: 19px; HEIGHT: 22px" type="hidden" size="1" value="Right"
				name="hdbtnText" runat="server"><INPUT id="hdCopy" style="WIDTH: 19px; HEIGHT: 22px" type="hidden" size="1" name="hdbtnText"
				runat="server"><INPUT id="hd131" style="WIDTH: 19px; HEIGHT: 22px" type="hidden" size="1" name="hdbtnText"
				runat="server"></TD>
	</TR>
	<tr id="trCompany" style="DISPLAY: none" runat="server">
		<td>
			<table width="100%">
				<tr>
					<td width="15%"><asp:label id="Label34" CssClass="label" runat="server" Width="76px">Company</asp:label></td>
					<td><asp:dropdownlist id="cboCompany" CssClass="combo" runat="server" Width="335px"></asp:dropdownlist></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr id="trC47" runat="server">
		<td>
			<table width="100%">
				<TR style="DISPLAY: none">
					<TD colSpan="2"><asp:label id="Label56" CssClass="labelData" runat="server">Input Information</asp:label></TD>
					<TD></TD>
					<TD></TD>
				</TR>
				<TR style="DISPLAY: none">
					<TD width="20%"><asp:label id="Label2" CssClass="label" runat="server" Width="100%">Sum of employee in previous month:</asp:label></TD>
					<TD width="20%"><asp:textbox id="txtSumEmpPrev7" onblur="checkNumeric(this)" CssClass="input" runat="server"
							Width="80px" MaxLength="15"></asp:textbox></TD>
					<TD width="25%"><asp:label id="Label35" CssClass="label" runat="server" Width="100%">Sum of wage_fund in previous month:</asp:label></TD>
					<TD width="30%"><asp:textbox id="txtWageFundPrev7" onblur="checkNumeric(this)" CssClass="input" runat="server"
							Width="80px" MaxLength="15"></asp:textbox></TD>
				</TR>
				<tr style="DISPLAY: none">
					<td colSpan="2"><asp:label id="Label58" CssClass="labelData" runat="server">Filter data</asp:label></td>
					<td></td>
					<TD></TD>
				</tr>
				<TR>
					<TD width="20%"><asp:label id="Label1" CssClass="labelRequire" runat="server" Width="65px">Month</asp:label></TD>
					<TD width="20%"><asp:textbox id="txtMonth7" onblur="CheckMonthYear(this)" CssClass="input" runat="server" Width="80px"
							MaxLength="7">01/2006</asp:textbox></TD>
					<TD width="25%"><asp:label id="Label59" CssClass="label" runat="server" Width="76px">Company</asp:label></TD>
					<TD width="30%"><asp:dropdownlist id="cboLSCompanyID7" CssClass="combo" runat="server" Width="100%"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD><asp:label id="Label4" CssClass="labelRequire" runat="server" Width="65px">Period</asp:label></TD>
					<TD><asp:textbox id="txtStage7" onblur="checkNumeric(this)" CssClass="input" runat="server" Width="80px"
							MaxLength="1">1</asp:textbox></TD>
					<TD></TD>
					<td></td>
				</TR>
				<tr>
					<TD width="20%"><asp:label id="lblPosSigner" CssClass="label" runat="server" Width="65px">PositionSigner</asp:label></TD>
					<TD width="20%"><asp:textbox id="txtPosSigner" CssClass="input" runat="server" Width="95%" MaxLength="500"></asp:textbox></TD>
					<TD width="25%"><asp:label id="lblSigner2" CssClass="label" runat="server" Width="65px">Signer</asp:label></TD>
					<TD width="30%"><asp:textbox id="txtSigner2" CssClass="input" runat="server" Width="100%" MaxLength="500"></asp:textbox></TD>
				</tr>
			</table>
		</td>
	</tr>
	<tr id="trC47a" runat="server">
		<td>
			<table width="100%">
				<TR>
					<TD width="15%"><asp:label id="Label66" CssClass="labelRequire" runat="server" Width="65px">Month</asp:label></TD>
					<TD width="25%"><asp:textbox id="txtMonth9" onblur="CheckMonthYear(this)" CssClass="input" runat="server" Width="80px"
							MaxLength="7">01/2006</asp:textbox></TD>
					<TD width="30%"><asp:label id="Label67" CssClass="label" runat="server" Width="76px">Company</asp:label></TD>
					<TD style="HEIGHT: 14px"><asp:dropdownlist id="cboLSCompanyID9" CssClass="combo" runat="server" Width="100%"></asp:dropdownlist></TD>
				</TR>
				<tr>
					<TD width="20%"><asp:label id="lblPosSign" CssClass="label" runat="server" Width="65px">PositionSigner</asp:label></TD>
					<TD width="20%"><asp:textbox id="txtPosSigner1" CssClass="input" runat="server" Width="95%" MaxLength="500"></asp:textbox></TD>
					<TD width="25%"><asp:label id="Label73" CssClass="label" runat="server" Width="65px">Signer</asp:label></TD>
					<TD width="30%"><asp:textbox id="txtSigner3" CssClass="input" runat="server" Width="100%" MaxLength="500"></asp:textbox></TD>
				</tr>
			</table>
		</td>
	</tr>
	<tr id="trC47b" runat="server">
		<td>
			<table width="7%" border="0">
				<tr>
					<td><asp:label id="Label47" CssClass="label" runat="server" Width="76px">Company</asp:label></td>
					<td colSpan="3"><asp:dropdownlist id="cboCompany5" CssClass="combo" runat="server" Width="250px"></asp:dropdownlist></td>
					<td colSpan="2"></td>
				</tr>
				<TR>
					<td><asp:label id="Label48" CssClass="labelRequire" runat="server" Width="80px">From Date</asp:label></td>
					<td><asp:textbox id="txtFromDate6" onblur="JavaScript:CheckMonthYear(this)" CssClass="input" runat="server"
							Width="75px" MaxLength="10"></asp:textbox></td>
					<td><asp:label id="Label49" CssClass="labelRequire" runat="server" Width="80px">To Date</asp:label></td>
					<td><asp:textbox id="txtToDate6" onblur="JavaScript:CheckMonthYear(this)" CssClass="input" runat="server"
							Width="75px" MaxLength="10"></asp:textbox></td>
					<td colSpan="2"></td>
				</TR>
			</table>
		</td>
	</tr>
	<TR id="trC45c" runat="server">
		<TD>
			<table width="100%">
				<tr>
					<td width="15%"><asp:label id="Label5" CssClass="label" runat="server" Width="76px">Location</asp:label></td>
					<td width="350"><asp:dropdownlist id="cboLocation" CssClass="combo" runat="server" Width="335px"></asp:dropdownlist></td>
					<td width="15%"></td>
					<TD></TD>
				</tr>
				<tr>
					<td width="15%"><asp:label id="Label41" CssClass="label" runat="server" Width="76px">Company</asp:label></td>
					<td width="350"><asp:dropdownlist id="cboCompany6" CssClass="combo" runat="server" Width="335px"></asp:dropdownlist></td>
					<td width="15%"></td>
					<TD></TD>
				</tr>
				<tr>
					<td><asp:label id="lblFromDate" CssClass="labelRequire" runat="server" Width="80px">From Date</asp:label></td>
					<td colSpan="3"><asp:textbox id="txtFromDate" onblur="CheckDate(this)" CssClass="input" runat="server" Width="75px"
							MaxLength="10">01/01/2006</asp:textbox><INPUT class="btnCal" onclick="ShowCalendar('_ctl0_txtFromDate')" type="button">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:label id="lblToDate" CssClass="labelRequire" runat="server" Width="80px">To Date</asp:label><asp:textbox id="txtToDate" onblur="CheckDate(this)" CssClass="input" runat="server" Width="75px"
							MaxLength="10">31/12/2006</asp:textbox><INPUT class="btnCal" onclick="ShowCalendar('_ctl0_txtToDate')" type="button"></td>
				</tr>
				<tr style="DISPLAY: none">
					<td colSpan="4"><asp:radiobuttonlist id="optIsType" CssClass="option" runat="server" Width="360px" RepeatDirection="Horizontal">
							<asp:ListItem Value="1" Selected="True">New employee</asp:ListItem>
							<asp:ListItem Value="2">Whole of employees</asp:ListItem>
						</asp:radiobuttonlist></td>
				</tr>
			</table>
		</TD>
	</TR>
	<TR id="trC45" runat="server">
		<TD>
			<table width="100%">
				<tr>
					<td style="DISPLAY: none" align="center"><asp:label id="Label29" CssClass="labelRequire" runat="server" Width="65px">Quý</asp:label><asp:textbox id="txtQuarter" onblur="checkNumeric(this)" CssClass="input" runat="server" Width="80px"
							MaxLength="4"></asp:textbox></td>
					<td><asp:label id="Label45" CssClass="label" runat="server" Width="76px">Company</asp:label></td>
					<td colSpan="6"><asp:dropdownlist id="cboCompany3" CssClass="combo" runat="server" Width="335px"></asp:dropdownlist></td>
				<tr>
					<td align="left" width="15%"><asp:label id="Label6" CssClass="labelRequire" runat="server" Width="65px">Year</asp:label></td>
					<td><asp:textbox id="txtYear" onblur="CheckYear(this)" CssClass="input" runat="server" Width="80px"
							MaxLength="4"></asp:textbox></td>
					<td><asp:label id="Label46" CssClass="labelRequire" runat="server" Width="80px">To Date</asp:label><asp:textbox id="txtToDate5" onblur="CheckDate(this)" CssClass="input" runat="server" Width="75px"
							MaxLength="10"></asp:textbox><INPUT class="btnCal" onclick="ShowCalendar('_ctl0_txtToDate5')" type="button"></td>
				</tr>
			</table>
		</TD>
	</TR>
	<TR id="trC02SBH" runat="server">
		<TD>
			<table width="100%">
				<tr>
					<td style="DISPLAY: none" align="center"><asp:label id="Label7" CssClass="label" runat="server" Width="65px">Quarter</asp:label><asp:textbox id="txtQuarter2" onblur="checkNumeric(this)" CssClass="input" runat="server" Width="80px"
							MaxLength="4"></asp:textbox></td>
					<td width="15%"><asp:label id="Label30" CssClass="labelRequire" runat="server" Width="65px">Month</asp:label></td>
					<td width="35%"><asp:textbox id="txtMonth2" onblur="CheckMonthYear(this)" CssClass="input" runat="server" Width="80px"
							MaxLength="7"></asp:textbox></td>
					<td width="20%"><asp:label id="Label63" CssClass="label" runat="server" Width="76px">Company</asp:label></td>
					<td><asp:dropdownlist id="cboLSCompanyID2" CssClass="combo" runat="server" Width="100%"></asp:dropdownlist></td>
					<td style="DISPLAY: none" align="left"><asp:label id="Label8" CssClass="labelRequire" runat="server" Width="65px">Year</asp:label><asp:textbox id="txtYear7" onblur="CheckYear(this)" CssClass="input" runat="server" Width="80px"
							MaxLength="4"></asp:textbox></td>
				</tr>
				<tr>
					<td width="15%"><asp:label id="lblPositionSigner" CssClass="label" runat="server" Width="65px">PositionSigner</asp:label></td>
					<td width="35%"><asp:textbox id="txtPositionSigner" CssClass="input" runat="server" Width="90%" MaxLength="500"></asp:textbox></td>
					<td width="15%"><asp:label id="lblSigner" CssClass="label" runat="server" Width="65px">Signer</asp:label></td>
					<td width="35%"><asp:textbox id="txtSignerBH" CssClass="input" runat="server" Width="100%" MaxLength="50"></asp:textbox></td>
				</tr>
			</table>
		</TD>
	</TR>
	<TR id="trToKhaiBH" runat="server">
		<TD>
			<table width="100%">
				<tr>
					<td width="15%"><asp:label id="Label9" CssClass="label" runat="server">From Date</asp:label></td>
					<td width="35%"><asp:textbox id="txtFromDate10" onblur="CheckDate(this)" CssClass="input" runat="server" Width="75px"
							MaxLength="10">01/01/2006</asp:textbox><INPUT class="btnCal" onclick="ShowCalendar('_ctl0_txtFromDate2')" type="button"></td>
					<td width="15%"><asp:label id="Label10" CssClass="label" runat="server">To Date</asp:label></td>
					<TD><asp:textbox id="txtToDate10" onblur="CheckDate(this)" CssClass="input" runat="server" Width="75px"
							MaxLength="10">31/12/2006</asp:textbox><INPUT class="btnCal" onclick="ShowCalendar('_ctl0_txtToDate2')" type="button"></TD>
				</tr>
				<TR>
					<TD><asp:label id="Label62" CssClass="label" runat="server" Width="76px">Cus.Group</asp:label></TD>
					<TD><asp:dropdownlist id="cboLSCompanyID10" CssClass="combo" runat="server" Width="95%" onchange="ChangeCompany()"></asp:dropdownlist></TD>
					<TD><asp:label id="Label11" CssClass="label" runat="server">Company</asp:label></TD>
					<TD><asp:dropdownlist id="cboLSLevel1ID10" CssClass="combo" runat="server" Width="95%" onchange="ChangeLevel1()"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD><asp:label id="Label12" CssClass="label" runat="server">Dept</asp:label></TD>
					<TD><asp:dropdownlist id="cboLSLevel2ID10" CssClass="combo" runat="server" Width="95%" onchange="ChangeLevel2()"></asp:dropdownlist></TD>
					<TD><asp:label id="Label13" CssClass="label" runat="server">Emp ID</asp:label></TD>
					<TD><asp:textbox id="txtEmpID" style="DISPLAY: none" CssClass="input" runat="server" Width="70%"></asp:textbox><asp:textbox id="txtEmpCode" CssClass="input" runat="server" Width="70%"></asp:textbox><INPUT class="search" id="btnSearchByID" onclick="javascript:OpenWindowEmp('EmpID')" type="button"
							value="..." name="btnSearchByID"></TD>
				</TR>
				<tr style="DISPLAY: none">
					<td><asp:dropdownlist id="cboLSLevel3ID10" CssClass="combo" runat="server" Width="95%"></asp:dropdownlist></td>
				</tr>
			</table>
		</TD>
	</TR>
	<TR id="trSalProcess" runat="server">
		<TD>
			<TABLE width="100%">
				<TR>
					<TD><asp:label id="Label20" CssClass="labelRequire" runat="server" Width="65px">Year</asp:label></TD>
					<TD><asp:textbox id="txtYear1" onblur="CheckYear(this)" CssClass="input" runat="server" Width="80px"
							MaxLength="4">2006</asp:textbox></TD>
					<TD><asp:label id="Label19" CssClass="label" runat="server" Width="76px">Location</asp:label></TD>
					<TD><asp:dropdownlist id="cboLocation1" CssClass="combo" runat="server" Width="95%"></asp:dropdownlist></TD>
				</TR>
				<tr>
					<td width="15%"><asp:label id="Label14" CssClass="label" runat="server">Division</asp:label></td>
					<td width="35%"><asp:dropdownlist id="cboLevel1_1" CssClass="combo" runat="server" Width="95%" onchange="ChangeLevel1_1()"></asp:dropdownlist></td>
					<td width="15%"><asp:label id="Label15" CssClass="label" runat="server">Dept</asp:label></td>
					<td><asp:dropdownlist id="cboLevel2_1" CssClass="combo" runat="server" Width="95%"></asp:dropdownlist></td>
				</tr>
			</TABLE>
		</TD>
	</TR>
	<TR id="trC46" runat="server">
		<TD>
			<table width="100%">
				<TR>
					<TD width="25%" colSpan="2"><asp:label id="Label44" CssClass="labelData" runat="server">Input Information</asp:label></TD>
					<TD align="left" width="10%"></TD>
					<TD width="15%"></TD>
					<TD width="10%"></TD>
					<TD width="25%"></TD>
				</TR>
				<tr>
					<td width="15%" colSpan="2"><asp:label id="Label51" CssClass="label" runat="server">Transfer Excess</asp:label></td>
					<td align="left" width="15%"><asp:textbox id="txtTransferExcess4" onblur="checkNumeric(this)" CssClass="input" runat="server"
							Width="76" MaxLength="20"></asp:textbox></td>
					<td colSpan="2"><asp:label id="Label52" CssClass="label" runat="server">Transfer Lack</asp:label></td>
					<TD><asp:textbox id="txtTransferLack4" onblur="checkNumeric(this)" CssClass="input" runat="server"
							Width="76" MaxLength="20"></asp:textbox></TD>
				</tr>
				<TR>
					<TD width="15%" colSpan="2"><asp:label id="Label65" CssClass="label" runat="server">Apply amount</asp:label></TD>
					<TD align="left" width="15%"><asp:textbox id="txtApplyAmount4" onblur="checkNumeric(this)" CssClass="input" runat="server"
							Width="76" MaxLength="20"></asp:textbox></TD>
					<TD colSpan="2"><asp:label id="Label64" CssClass="label" runat="server">Delay fine</asp:label></TD>
					<TD><asp:textbox id="txtDelayFine4" onblur="checkNumeric(this)" CssClass="input" runat="server" Width="76"
							MaxLength="20"></asp:textbox></TD>
				</TR>
				<TR>
					<TD width="15%" colSpan="2"><asp:label id="Label50" CssClass="label" runat="server">HI Fund</asp:label></TD>
					<TD align="left" width="15%"><asp:textbox id="txtFund4" onblur="checkNumeric(this)" CssClass="input" runat="server" Width="76"
							MaxLength="20"></asp:textbox></TD>
					<TD colSpan="2"></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD width="15%" colSpan="2"><asp:label id="Label53" CssClass="labelData" runat="server">Filter data</asp:label></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD colSpan="2"><asp:label id="Label54" CssClass="labelRequire" runat="server" Width="80px">From Month</asp:label></TD>
					<TD><asp:textbox id="txtFromMonth4" onblur="CheckMonthYear(this)" CssClass="input" runat="server"
							Width="76" MaxLength="10">01/2006</asp:textbox></TD>
					<TD colSpan="2"><asp:label id="Label55" CssClass="labelRequire" runat="server" Width="80px">To Month</asp:label></TD>
					<TD><asp:textbox id="txtToMonth4" onblur="CheckMonthYear(this)" CssClass="input" runat="server" Width="76"
							MaxLength="10">12/2006</asp:textbox></TD>
				</TR>
				<TR>
					<TD colSpan="2"><asp:label id="Label57" CssClass="label" runat="server" Width="76px">Company</asp:label></TD>
					<TD colSpan="4"><asp:dropdownlist id="cboLSCompanyID4" CssClass="combo" runat="server" Width="81%"></asp:dropdownlist></TD>
				</TR>
			</table>
		</TD>
	</TR>
	<TR id="trC04BH" runat="server">
		<TD>
			<table width="100%">
				<TR>
					<TD width="15%" colSpan="2"><asp:label id="Label40" CssClass="labelData" runat="server">Title of Report</asp:label></TD>
					<TD align="left" width="15%"></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
				</TR>
				<tr>
					<td width="15%"><asp:label id="lbl17" CssClass="labelRequire" runat="server" Width="65px">Year</asp:label></td>
					<td width="20%"><asp:textbox id="txtYear3" onblur="CheckYear(this)" CssClass="input" runat="server" Width="76"
							MaxLength="4">2006</asp:textbox></td>
					<td align="left" width="15%">&nbsp;
						<asp:label id="Label38" CssClass="label" runat="server" Width="65px">Month</asp:label></td>
					<td><asp:textbox id="txtMonth3" CssClass="input" runat="server" Width="76" MaxLength="10">1</asp:textbox></td>
					<TD><asp:label id="Lbl16" CssClass="label" runat="server" Width="65px">Quarter</asp:label></TD>
					<TD><asp:textbox id="txtQuarter3" onblur="CheckQuarterWhenNotNull(this)" CssClass="input" runat="server"
							Width="76" MaxLength="4">1</asp:textbox></TD>
				</tr>
				<TR>
					<TD width="15%" colSpan="3"><asp:label id="Label39" CssClass="labelData" runat="server">Filter data</asp:label></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
				</TR>
				<tr>
					<td><asp:label id="Label16" CssClass="labelRequire" runat="server" Width="80px">From Date</asp:label></td>
					<td><asp:textbox id="txtFromDate3" onblur="CheckDate(this)" CssClass="input" runat="server" Width="76"
							MaxLength="10">01/01/2006</asp:textbox><INPUT class="btnCal" onclick="ShowCalendar('_ctl0_txtFromDate3')" type="button"></td>
					<td><asp:label id="Label17" CssClass="labelRequire" runat="server" Width="80px">To Date</asp:label></td>
					<td><asp:textbox id="txtToDate3" onblur="CheckDate(this)" CssClass="input" runat="server" Width="76"
							MaxLength="10">31/12/2006</asp:textbox><INPUT class="btnCal" onclick="ShowCalendar('_ctl0_txtToDate3')" type="button"></td>
					<TD></TD>
					<TD></TD>
				</tr>
				<TR>
					<TD><asp:label id="Label43" CssClass="label" runat="server" Width="76px">Group</asp:label></TD>
					<TD><asp:dropdownlist id="cboGroup3" CssClass="combo" runat="server" Width="95px">
							<asp:ListItem Value="SICK">Ốm đau - 75%</asp:ListItem>
							<asp:ListItem Value="MTL">Thai sản - 100%</asp:ListItem>
						</asp:dropdownlist></TD>
					<TD><asp:label id="Label42" CssClass="label" runat="server" Width="76px">Company</asp:label></TD>
					<TD colSpan="3"><asp:dropdownlist id="cboLSCompanyID3" CssClass="combo" runat="server" Width="100%"></asp:dropdownlist></TD>
				</TR>
			</table>
		</TD>
	</TR>
	<TR id="trC04BH_NDS" runat="server">
		<TD>
			<table width="100%">
				<TR>
					<TD style="HEIGHT: 27px" width="15%" colSpan="2"><asp:label id="Label69" CssClass="labelData" runat="server">Title of Report</asp:label></TD>
					<TD style="HEIGHT: 27px" width="15%"></TD>
					<TD style="HEIGHT: 27px" width="20%"></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 27px" width="15%"><asp:label id="Label71" CssClass="label" runat="server">Aplly Amount</asp:label></TD>
					<TD style="HEIGHT: 27px" colSpan="2"><asp:textbox id="txtApplyAmount8" onblur="checkNumeric(this)" CssClass="input" runat="server"
							Width="76" MaxLength="20"></asp:textbox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:label id="lblNotYetAmount8" CssClass="label" runat="server" Width="100px">Not yet Amount</asp:label><asp:textbox id="txtNotYetAmount8" onblur="checkNumeric(this)" CssClass="input" runat="server"
							Width="76" MaxLength="20"></asp:textbox></TD>
					<TD style="HEIGHT: 27px" width="20%"></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 27px" width="15%"><asp:label id="Label70" CssClass="labelData" runat="server">Filter data</asp:label></TD>
					<TD style="HEIGHT: 27px"></TD>
					<TD style="HEIGHT: 27px" width="15%"></TD>
					<TD style="HEIGHT: 27px" width="20%"></TD>
				</TR>
				<tr>
					<td style="HEIGHT: 27px" width="15%"><asp:label id="Label61" CssClass="labelRequire" runat="server" Width="65px">Year</asp:label></td>
					<TD style="HEIGHT: 27px"><asp:textbox id="txtYear8" onblur="CheckYear(this)" CssClass="input" runat="server" Width="76"
							MaxLength="4">2006</asp:textbox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:label id="Label60" CssClass="labelRequire" runat="server" Width="100px">Stage</asp:label><asp:textbox id="txtQuarter8" onblur="checkNumeric(this)" CssClass="input" runat="server" Width="76"
							MaxLength="4">1</asp:textbox></TD>
					<TD style="HEIGHT: 27px" width="15%"><asp:label id="lblToDate8" CssClass="labelRequire" runat="server" Width="80px">To Date</asp:label></TD>
					<TD style="HEIGHT: 27px" width="20%"><asp:textbox id="txtToDate8" onblur="CheckDate(this)" CssClass="input" runat="server" Width="76"
							MaxLength="10">31/12/2006</asp:textbox><INPUT class="btnCal" onclick="ShowCalendar('_ctl0_txtToDate8')" type="button"></TD>
				</tr>
				<TR>
					<TD><asp:label id="Label68" CssClass="label" runat="server" Width="76px">Company</asp:label></TD>
					<TD><asp:dropdownlist id="cboLSCompanyID8" CssClass="combo" runat="server" Width="90%" onchange="ChangeCompanyC04NDS()"></asp:dropdownlist></TD>
					<TD><asp:label id="Label72" CssClass="label" runat="server">Division</asp:label></TD>
					<TD><asp:dropdownlist id="cboLSLevel1ID8" CssClass="combo" runat="server" Width="95%"></asp:dropdownlist></TD>
					<td></td>
				</TR>
			</table>
		</TD>
	</TR>
	<TR id="trSIProcess" runat="server">
		<TD>
			<table width="100%">
				<TR>
					<TD width="15%"><asp:label id="Label18" CssClass="labelrequire" runat="server">Emp ID</asp:label></TD>
					<TD width="30%"><asp:textbox id="txtEmpID1" CssClass="input" runat="server" Width="70%">00056</asp:textbox><INPUT class="search" id="btnSearchByID1" onclick="javascript:OpenWindowEmp('EmpID')" type="button"
							value="..." name="btnSearchByID1"></TD>
					<TD width="15%"><asp:label id="Label21" CssClass="labelrequire" runat="server">Signer</asp:label></TD>
					<TD><asp:textbox id="txtEmpID2" CssClass="input" runat="server" Width="70%">00056</asp:textbox><INPUT class="search" id="btnSearchByID2" onclick="javascript:OpenWindowSigner('EmpID')"
							type="button" value="..." name="btnSearchByID2"></TD>
				</TR>
			</table>
		</TD>
	</TR>
	<TR id="trRegisterLB" runat="server">
		<TD>
			<table width="100%">
				<tr style="DISPLAY: none">
					<td width="15%"><asp:label id="Label22" CssClass="labelRequire" runat="server" Width="65px">Year</asp:label></td>
					<td width="24%"><asp:textbox id="txtYear4" onblur="CheckYear(this)" CssClass="input" runat="server" Width="80px"
							MaxLength="4" Text="2005"></asp:textbox></td>
					<td width="13%"><asp:label id="Label23" CssClass="label" runat="server" Width="76px">Quarter</asp:label></td>
					<td><asp:textbox id="txtQuarter4" CssClass="input" runat="server" Width="80px" MaxLength="4"></asp:textbox></td>
				</tr>
				<tr>
					<td width="15%"><asp:label id="Label36" CssClass="labelRequire" runat="server" Width="65px">From date</asp:label></td>
					<td colSpan="3"><asp:textbox id="txtFromDate4" onblur="CheckDate(this)" CssClass="input" runat="server" Width="75px"
							MaxLength="10">01/01/2006</asp:textbox><INPUT class="btnCal" onclick="ShowCalendar('_ctl0_txtFromDate4')" type="button">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:label id="Label37" CssClass="labelRequire" runat="server" Width="80px">To Date</asp:label><asp:textbox id="txtToDate4" onblur="CheckDate(this)" CssClass="input" runat="server" Width="75px"
							MaxLength="10">31/12/2006</asp:textbox><INPUT class="btnCal" onclick="ShowCalendar('_ctl0_txtToDate4')" type="button">
					</td>
				</tr>
				<tr>
					<td colSpan="4"><asp:radiobuttonlist id="optIsType4" CssClass="option" runat="server" Width="400px" RepeatDirection="Horizontal">
							<asp:ListItem Value="1" Selected="True">New employee without Labour Book</asp:ListItem>
							<asp:ListItem Value="2">New employee</asp:ListItem>
						</asp:radiobuttonlist></td>
				</tr>
			</table>
		</TD>
	</TR>
	<TR id="trStateOfLabour" runat="server">
		<TD>
			<table width="100%" border="0">
				<TR>
					<TD noWrap align="center">
						<TABLE cellPadding="0" width="100%" border="0">
							<TR>
								<TD noWrap><asp:label id="Label24" CssClass="labelRequire" runat="server">Year</asp:label></TD>
								<TD noWrap><asp:textbox id="txtYear5" onblur="CheckYear(this)" CssClass="input" runat="server" Width="80px"
										MaxLength="4"></asp:textbox></TD>
								<TD noWrap><asp:label id="Label25" CssClass="label" runat="server">Stage</asp:label></TD>
								<TD noWrap><input id="optStage0" onclick="remain(this)" type="radio" CHECKED value="0" name="radioStage"
										runat="server">I <input id="optStage1" onclick="remain(this)" type="radio" value="1" name="radioStage" runat="server">II</TD>
								<TD noWrap><asp:label id="Label26" CssClass="label" runat="server">Signer</asp:label></TD>
								<TD noWrap width="100"><asp:textbox id="txtSigner" CssClass="input" runat="server" Width="100%">00056</asp:textbox></TD>
								<TD noWrap><INPUT class="search" id="btnSigner" onclick="javascript:OpenWindowSigner('EmpID')" type="button"
										value="..." name="btnSearchByID2"></TD>
							</TR>
							<tr>
								<td><asp:label id="Label32" CssClass="labelrequire" runat="server">Labour Expectation by Seft</asp:label></td>
								<td><asp:textbox id="txtBySelf" onblur="checkNumeric(this)" CssClass="input" runat="server" Width="80px"
										MaxLength="4">15</asp:textbox></td>
								<td colSpan="2"><asp:label id="Label33" CssClass="labelrequire" runat="server">Labour Expectation by Other</asp:label></td>
								<td><asp:textbox id="txtByOther" onblur="checkNumeric(this)" CssClass="input" runat="server" Width="80px"
										MaxLength="4">15</asp:textbox></td>
								<td></td>
								<td></td>
							</tr>
							<tr>
								<td><asp:label id="Label3" CssClass="label" runat="server" Width="76px">Company</asp:label></td>
								<td colSpan="6"><asp:dropdownlist id="cboCompany1" CssClass="combo" runat="server" Width="335px"></asp:dropdownlist></td>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</table>
		</TD>
	</TR>
	<TR id="trLeaveRecordByDepartment" runat="server">
		<TD>
			<table width="100%" border="0">
				<TR>
					<TD noWrap align="center">
						<TABLE style="WIDTH: 385px" cellPadding="0" border="0">
							<TR>
								<TD noWrap><asp:label id="Label27" CssClass="labelRequire" runat="server">Year</asp:label></TD>
								<TD noWrap><asp:textbox id="txtYear6" onblur="CheckYear(this)" CssClass="input" runat="server" Width="80px"
										MaxLength="4"></asp:textbox></TD>
								<TD noWrap><asp:label id="Label28" CssClass="label" runat="server">Language</asp:label></TD>
								<TD noWrap><input id="optLag0" onclick="remain(this)" type="radio" CHECKED value="0" name="optLag"
										runat="server">EN <input id="optLag1" onclick="remain(this)" type="radio" value="1" name="optLag" runat="server">VN</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</table>
		</TD>
	</TR>
	<TR id="trEmpRegisterLB" runat="server">
		<TD>
			<table width="100%">
				<TR align="center">
					<td width="15%"></td>
					<TD width="15%"><asp:label id="Label31" CssClass="labelrequire" runat="server">Emp ID</asp:label></TD>
					<TD width="35%"><asp:textbox id="txtEmpID8" CssClass="input" runat="server" Width="70%">00056</asp:textbox><INPUT class="search" id="btnSearchByID8" onclick="javascript:OpenWindowEmp('EmpID')" type="button"
							value="..." name="btnSearchByID8"></TD>
					<td></td>
				</TR>
			</table>
		</TD>
	</TR>
	<TR id="trOther" runat="server">
		<TD></TD>
	</TR>
	<TR>
		<TD>
			<HR width="100%" SIZE="1">
		</TD>
	</TR>
	<tr>
		<td align="center"><asp:linkbutton id="btnPrint" accessKey="P" runat="server" CssClass="btnPrint" ToolTip="ALT+P">Preview</asp:linkbutton><INPUT id="txtLevel2ID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtLevel2ID"
				runat="server">
			<asp:linkbutton id="btnExcel" accessKey="P" runat="server" CssClass="btnPrint" ToolTip="ALT+P" Visible="False">Excel</asp:linkbutton></td>
	</tr>
</TABLE>
<INPUT id="remainfield" type="hidden" value="0" name="remainfield" runat="server">
<script language="javascript">
	function PrintReport()
	{
		/*var str_url;
		if (document.getElementById("_ctl0_trC04BH").style.display == "")
		{
			if (document.getElementById("_ctl0_txtFromDate3").value == "")
			{
				alert("Input from date to report!");
				document.getElementById("_ctl0_txtFromDate3").focus();
				return false;
			}	
			if (document.getElementById("_ctl0_txtToDate3").value == "")
			{
				alert("Input to date to report!");
				document.getElementById("_ctl0_txtToDate3").focus();
				return false;
			}
			if (FromSmallToDate(document.getElementById("_ctl0_txtFromDate3"),document.getElementById("_ctl0_txtToDate3")) == false)
			{
				alert("To date must more or equal to from date!");
				document.getElementById("_ctl0_txtFromDate3").focus();
				return false;
			}
			if (document.getElementById("_ctl0_txtYear3").value == "")
			{
				alert("Input year to report!");
				document.getElementById("_ctl0_txtYear3").focus();
				return false;
			}
			if (trim(document.getElementById("_ctl0_optQuarter3").value)!= "")	
			{
				if (CheckQuarter(document.getElementById("_ctl0_txtQuarter3")) == false)
				{
					alert("Vui lòng nhập thông tin Quý (1-->4).");
					document.getElementById("_ctl0_optQuarter3").focus();
					return false;
				}
			}
			
			if (trim(document.getElementById("_ctl0_txtMonth3").value)!= "")	
			{
				alert("Vui lòng nhập thông tin Tháng (1-->12, cách nhau bởi dấu phẩy).");
				document.getElementById("_ctl0_txtMonth3").focus();
				return false;
			}
		}
		else if (document.getElementById("_ctl0_trC47").style.display == "")
		{
			if (document.getElementById("_ctl0_txtMonth7").value == "")
			{
				alert("Input period to report!");
				document.getElementById("_ctl0_txtMonth7").focus();
				return false;
			}
			if (document.getElementById("_ctl0_txtStage7").value == "")
			{
				alert("Input period to report!");
				document.getElementById("_ctl0_txtStage7").focus();
				return false;
			}
			if (document.getElementById("_ctl0_txtSumEmpPrev7").value == "")
			{
				alert("Input sum of employee in previous month to report!");
				document.getElementById("_ctl0_txtSumEmpPrev7").focus();
				return false;
			}
			if (document.getElementById("_ctl0_txtWageFundPrev7").value == "")
			{
				alert("Input sum of wage_fund in previous month to report!");
				document.getElementById("_ctl0_txtWageFundPrev7").focus();
				return false;
			}
		}
		*/
		/*else if (document.getElementById("_ctl0_trC45").style.display == "")
		{
			if (document.getElementById("_ctl0_txtYear").value == "")
			{
				alert("Input year to report!");
				document.getElementById("_ctl0_txtYear").focus();
				return false;
			}
			if (trim(document.getElementById("_ctl0_txtQuarter").value)!= "")	
			{
				if (CheckQuarter(document.getElementById("_ctl0_txtQuarter")) == false)
				{
					document.getElementById("_ctl0_txtQuarter").focus();
					return false;
				}
			}
			str_url = "Reports/ShowReports.aspx?pRptName=SI/SI_rptC45.rpt&pStrPara=@Year;@Quarter;@CompanyID&pStrValue=" + trim(document.getElementById("_ctl0_txtYear").value) + ";" + trim(document.getElementById("_ctl0_txtQuarter").value)
				 + ";" + trim(document.getElementById("_ctl0_cboCompany").value);
		}
		else*//* if (document.getElementById("_ctl0_trC45c").style.display == "")
		{
			if (document.getElementById("_ctl0_txtFromDate").value == "")
			{
				alert("Input from date to report!");
				document.getElementById("_ctl0_txtFromDate").focus();
				return false;
			}
			if (document.getElementById("_ctl0_txtToDate").value == "")
			{
				alert("Input to date to report!");
				document.getElementById("_ctl0_txtToDate").focus();
				return false;
			}
			if (FromSmallToDate(document.getElementById("_ctl0_txtFromDate"),document.getElementById("_ctl0_txtToDate")) == false)
			{
				alert("To date must more or equal to from date!");
				document.getElementById("_ctl0_txtFromDate").focus();
				return false;
			}
			str_url = "Reports/ShowReports.aspx?pRptName=SI/SI_rptC45c.rpt&pStrPara=@LocationID;@FromDate;@ToDate;@IsType;@CompanyID&pStrValue=" + trim(document.getElementById("_ctl0_cboLocation").value) + ";" + trim(document.getElementById("_ctl0_txtFromDate").value) + ";" + trim(document.getElementById("_ctl0_txtToDate").value) 
				+ ";" + (document.getElementById("_ctl0_optIsType_0").checked?1:2)
				+ ";" + trim(document.getElementById("_ctl0_cboCompany").value);
		}
		else *//*if (document.getElementById("_ctl0_tr02_SBH").style.display == "")
		{
			/*if (document.getElementById("_ctl0_txtFromDate1").value == "")
			{
				alert("Input from date to report!");
				document.getElementById("_ctl0_txtFromDate1").focus();
				return false;
			}
			if (document.getElementById("_ctl0_txtToDate1").value == "")
			{
				alert("Input to date to report!");
				document.getElementById("_ctl0_txtToDate1").focus();
				return false;
			}
			if (FromSmallToDate(document.getElementById("_ctl0_txtFromDate1"),document.getElementById("_ctl0_txtToDate1")) == false)
			{
				alert("To date must more or equal to from date!");
				document.getElementById("_ctl0_txtFromDate1").focus();
				return false;
			}
			if (document.getElementById("_ctl0_txtYear7").value == "")
			{
				alert("Input year to report!");
				document.getElementById("_ctl0_txtYear7").focus();
				return false;
			}
			if (trim(document.getElementById("_ctl0_txtQuarter2").value)!= "")	
			{
				if (CheckQuarter(document.getElementById("_ctl0_txtQuarter2")) == false)
				{
					document.getElementById("_ctl0_txtQuarter2").focus();
					return false;
				}
			}*/
			/*if (trim(document.getElementById("_ctl0_txtMMYYYY1").value)== "")	
			{
				alert("Input month to report!");
				document.getElementById("_ctl0_txtMMYYYY1").focus();
				return false;
			}
			str_url = "Reports/ShowReports.aspx?pRptName=SI/SI_rpt02_SBH.rpt&pStrPara=@Year;@Quarter;@MMYYYY;@CompanyID&pStrValue=" 
				+ trim(document.getElementById("_ctl0_txtYear7").value) + ";" 
				+ trim(document.getElementById("_ctl0_txtQuarter2").value) + ";"
				+ trim(document.getElementById("_ctl0_txtMMYYYY1").value) + ";"
				+ trim(document.getElementById("_ctl0_cboCompany").value);
		}
		else*//*if (document.getElementById("_ctl0_trToKhaiBH").style.display == "")
		{
			/*if (document.getElementById("_ctl0_txtFromDate2").value == "")
			{
				alert("Input from date to report!");
				document.getElementById("_ctl0_txtFromDate2").focus();
				return false;
			}
			if (document.getElementById("_ctl0_txtToDate2").value == "")
			{
				alert("Input to date to report!");
				document.getElementById("_ctl0_txtToDate2").focus();
				return false;
			}*/
			/*if (FromSmallToDate(document.getElementById("_ctl0_txtFromDate2"),document.getElementById("_ctl0_txtToDate2")) == false)
			{
				alert("To date must more or equal to from date!");
				document.getElementById("_ctl0_txtFromDate2").focus();
				return false;
			}
			str_url = "Reports/ShowReports.aspx?pRptName=SI/SI_rptToKHaiCapSoBH.rpt&pStrPara=@Level1ID;@Level2ID;@FromDate;@ToDate;@EmpID&pStrValue=" 
			+ trim(document.getElementById("_ctl0_cboLevel1").value) + ";" + trim(document.getElementById("_ctl0_cboLevel2").value) 
			+ ";" + trim(document.getElementById("_ctl0_txtFromDate2").value) + ";" + trim(document.getElementById("_ctl0_txtToDate2").value) 
			+ ";" + trim(document.getElementById("_ctl0_txtEmpID").value);
		}
		else*//*if (document.getElementById("_ctl0_trSalProcess").style.display == "")
		{
			if (document.getElementById("_ctl0_txtYear1").value == "")
			{
				alert("Input year to report!");
				document.getElementById("_ctl0_txtYear1").focus();
				return false;
			}
			str_url = "Reports/ShowReports.aspx?pRptName=SI/SI_rptSISalByYear.rpt&pStrPara=@Year;@LocationID;@LSLevel1Code;@LSLevel2Code&pStrValue=" + trim(document.getElementById("_ctl0_txtYear1").value) + ";" + trim(document.getElementById("_ctl0_cboLocation1").value) + ";" + trim(document.getElementById("_ctl0_cboLevel1_1").value) + ";" + trim(document.getElementById("_ctl0_cboLevel2_1").value);
		}
		else*/ 
		/*else if (document.getElementById("_ctl0_trSIProcess").style.display == "")
		{
			if (document.getElementById("_ctl0_txtEmpID1").value == "")
			{
				alert("Choose employee to report!");
				document.getElementById("_ctl0_txtEmpID1").focus();
				return false;
			}
			if (document.getElementById("_ctl0_txtEmpID2").value == "")
			{
				alert("Choose signer report!");
				document.getElementById("_ctl0_txtEmpID2").focus();
				return false;
			}
			str_url = "Reports/ShowReports.aspx?pRptName=SI/SI_rptSIProcess1.rpt&pStrPara=@EmpID;@Signer;@EmpID1;@Signer1&pStrValue=" + trim(document.getElementById("_ctl0_txtEmpID1").value) + ";" + trim(document.getElementById("_ctl0_txtEmpID2").value) + 
					";" + trim(document.getElementById("_ctl0_txtEmpID1").value) + ";" + trim(document.getElementById("_ctl0_txtEmpID2").value);
		}
		else if (document.getElementById("_ctl0_trRegisterLB").style.display == "")
		{
			if (document.getElementById("_ctl0_txtFromDate4").value == "")
			{
				alert("Input from date to report!");
				document.getElementById("_ctl0_txtFromDate4").focus();
				return false;
			}
			if (document.getElementById("_ctl0_txtToDate4").value == "")
			{
				alert("Input to date to report!");
				document.getElementById("_ctl0_txtToDate4").focus();
				return false;
			}
			if (FromSmallToDate(document.getElementById("_ctl0_txtFromDate4"),document.getElementById("_ctl0_txtToDate4")) == false)
			{
				alert("To date must more or equal to from date!");
				document.getElementById("_ctl0_txtFromDate4").focus();
				return false;
			}
			str_url = "Reports/ShowReports.aspx?pRptName=SI/SI_rptCapSoLaoDong.rpt&pStrPara=@Year;@Quarter;@CompanyID;@DateFrom;@DateTo;@IsType&pStrValue=" 
			+ trim(document.getElementById("_ctl0_txtYear4").value) + ";" 
			+ trim(document.getElementById("_ctl0_txtQuarter4").value)
			+ ";" + trim(document.getElementById("_ctl0_cboCompany").value)
			+ ";" + trim(document.getElementById("_ctl0_txtFromDate4").value)
			+ ";" + trim(document.getElementById("_ctl0_txtToDate4").value)
			+ ";" + (document.getElementById("_ctl0_optIsType4_0").checked?1:2)
		}
		else if (document.getElementById("_ctl0_trStateOfLabour").style.display == "")
		{
			//QuocNV
			if (document.getElementById("_ctl0_txtBySelf").value == "")
			{
				alert("Input number of labour expectation by self!");
				document.getElementById("_ctl0_txtBySelf").focus();
				return false;
			}
			if (document.getElementById("_ctl0_txtByOther").value == "")
			{
				alert("Input number of labour expectation by other!");
				document.getElementById("_ctl0_txtByOther").focus();
				return false;
			}
			str_url = "Reports/ShowReports.aspx?pRptName=SI/SI_rptStateOfLabour.rpt&pStrPara=@Year;@Stage;@Signer;@RecruitBySelf;@RecruitByOther;@CompanyID&pStrValue=" 
			+ trim(document.getElementById("_ctl0_txtYear5").value) + ";" 
			+ trim(document.getElementById("_ctl0_remainfield").value) + ";" 
			+ trim(document.getElementById("_ctl0_txtSigner").value) + ";" 
			+ trim(document.getElementById("_ctl0_txtBySelf").value) + ";" 
			+ trim(document.getElementById("_ctl0_txtByOther").value) + ";"
			+ trim(document.getElementById("_ctl0_cboCompany1").value);
		}
		else if (document.getElementById("_ctl0_trLeaveRecordByDepartment").style.display == "")
		{
			//QuocNV
			language = trim(document.getElementById("_ctl0_remainfield").value)==0?"EN":"VN";
			str_url = "Reports/ShowReports.aspx?pRptName=SI/SI_rptLeaveRecordByDepartment.rpt&pStrPara=@Language;@Year&pStrValue=" + language + ";" + trim(document.getElementById("_ctl0_txtYear6").value);
		}
		else if (document.getElementById("_ctl0_trEmpRegisterLB").style.display == "")
		{
			if (document.getElementById("_ctl0_txtEmpID8").value == "")
			{
				alert("Choose employee to report!");
				document.getElementById("_ctl0_txtEmpID8").focus();
				return false;
			}
			str_url = "Reports/ShowReports.aspx?pRptName=SI/SI_rptRegisterLabourBook.rpt&pStrPara=@EmpID&pStrValue=" + trim(document.getElementById("_ctl0_txtEmpID8").value);
		}
		else
		{
			if ('<%=Request.Params["tabid"]%>' == 'C46')
			{
				str_url = "Reports/ShowReports.aspx?pRptName=SI/SI_rptC46.rpt";
			}
			else if ('<%=Request.Params["tabid"]%>' == 'C47a')
			{
				str_url = "Reports/ShowReports.aspx?pRptName=SI/SI_rptC47a.rpt";
			}				
			else if ('<%=Request.Params["tabid"]%>' == 'C47b')
			{
				str_url = "Reports/ShowReports.aspx?pRptName=SI/SI_rptC47b.rpt";
			}	
		}
*/
//		ShowDialog(str_url)
		return false;
	}
	function CheckReport()
	{
	
		if (document.getElementById("_ctl0_trC02SBH").style.display == "")
		{
			if (document.getElementById("_ctl0_txtMonth2").value == "")
			{
				GetAlertError(iTotal,DSAlert,"0003");
				document.getElementById("_ctl0_txtMonth2").focus();
				return false;
			}	
		}
		else if (document.getElementById("_ctl0_trC04BH").style.display == "")
		{
			if (document.getElementById("_ctl0_txtYear3").value == "")
			{
				GetAlertError(iTotal,DSAlert,"0003");
				document.getElementById("_ctl0_txtYear3").focus();
				return false;
			}	
			
			if (trim(document.getElementById("_ctl0_txtMonth3").value) == "" && trim(document.getElementById("_ctl0_txtQuarter3").value) == "")				
			{
				GetAlertError(iTotal,DSAlert,"SI_RP_0002");
				document.getElementById("_ctl0_txtMonth3").focus();
				return false;
			}
			if (document.getElementById("_ctl0_txtFromDate3").value == "")
			{
				//alert("Input from date to report!");
				GetAlertError(iTotal,DSAlert,"0003");
				document.getElementById("_ctl0_txtFromDate3").focus();
				return false;
			}	
			if (document.getElementById("_ctl0_txtToDate3").value == "")
			{
				//alert("Input to date to report!");
				GetAlertError(iTotal,DSAlert,"0003");
				document.getElementById("_ctl0_txtToDate3").focus();
				return false;
			}
			if (FromSmallToDate(document.getElementById("_ctl0_txtFromDate3"),document.getElementById("_ctl0_txtToDate3")) == false)
			{
				//alert("To date must more or equal to from date!");
				GetAlertError(iTotal,DSAlert,"0007");
				document.getElementById("_ctl0_txtFromDate3").focus();
				return false;
			}
		}
		else if (document.getElementById("_ctl0_trC04BH_NDS").style.display == "")
		{
			if (document.getElementById("_ctl0_txtYear8").value == "")
			{
				GetAlertError(iTotal,DSAlert,"0003");
				document.getElementById("_ctl0_txtYear8").focus();
				return false;
			}	
			
			if (document.getElementById("_ctl0_txtQuarter8").value == "")
			{
				GetAlertError(iTotal,DSAlert,"0003");
				document.getElementById("_ctl0_txtQuarter8").focus();
				return false;
			}	

			if (document.getElementById("_ctl0_txtToDate8").value == "")
			{
				GetAlertError(iTotal,DSAlert,"0003");
				document.getElementById("_ctl0_txtToDate8").focus();
				return false;
			}	
		}
		else if (document.getElementById("_ctl0_trC47").style.display == "")
		{
			if (document.getElementById("_ctl0_txtMonth7").value == "")
			{
				//alert("Input period to report!");
				GetAlertError(iTotal,DSAlert,"0003");
				document.getElementById("_ctl0_txtMonth7").focus();
				return false;
			}
			if (document.getElementById("_ctl0_txtStage7").value == "")
			{
				//alert("Input period to report!");
				GetAlertError(iTotal,DSAlert,"0003");
				document.getElementById("_ctl0_txtStage7").focus();
				return false;
			}
		}
		else if (document.getElementById("_ctl0_trC47a").style.display == "")
		{
			if (document.getElementById("_ctl0_txtMonth9").value == "")
			{
				//alert("Input period to report!");
				GetAlertError(iTotal,DSAlert,"0003");
				document.getElementById("_ctl0_txtMonth9").focus();
				return false;
			}
		}	
		else if(document.getElementById("_ctl0_trC45c").style.display!="none")
		{
				/*if(document.getElementById("_ctl0_cboLocation").value=="")
				{
					GetAlertError(iTotal,DSAlert,"0003");
					document.getElementById("_ctl0_cboLocation").focus();
					return false;
				}*/
				if(document.getElementById("_ctl0_txtFromDate6").value=="")
				{
					GetAlertError(iTotal,DSAlert,"0003");
					document.getElementById("_ctl0_txtFromDate6").focus();
					return false;
				}
				if(document.getElementById("_ctl0_txtToDate6").value=="")
				{
					GetAlertError(iTotal,DSAlert,"0003");
					document.getElementById("_ctl0_txtToDate6").focus();
					return false;
				}
		}
		else if (document.getElementById("_ctl0_trToKhaiBH").style.display!="none")
		{
			
			if (document.getElementById("_ctl0_txtFromDate10").value != "" &&document.getElementById("_ctl0_txtToDate10").value != "")
			{
				if(document.getElementById("_ctl0_txtFromDate10").value != document.getElementById("_ctl0_txtToDate10").value)
				{
					if (FromSmallToDate(document.getElementById("_ctl0_txtFromDate10"),document.getElementById("_ctl0_txtToDate10")) == false)
					{
						GetAlertError(iTotal,DSAlert,"0007");
						document.getElementById("_ctl0_txtFromDate10").focus();
						return false;
					}
				}
				
			}			
		}
		else if(document.getElementById("_ctl0_trC45").style.display!="none")
		{
				if(document.getElementById("_ctl0_txtYear").value=="")
				{
					GetAlertError(iTotal,DSAlert,"0003");
					document.getElementById("_ctl0_txtYear").focus();
					return false;
				}
				if(document.getElementById("_ctl0_txtToDate5").value=="")
				{
					GetAlertError(iTotal,DSAlert,"0003");
					document.getElementById("_ctl0_txtToDate5").focus();
					return false;
				}
		}
		else if(document.getElementById("_ctl0_trC47b").style.display!="none")
		{
				if(document.getElementById("_ctl0_txtFromDate6").value=="")
				{
					GetAlertError(iTotal,DSAlert,"0003");
						document.getElementById("_ctl0_txtFromDate6").focus();
					return false;
				}
				if(document.getElementById("_ctl0_txtToDate6").value=="")
				{
					GetAlertError(iTotal,DSAlert,"0003");
					document.getElementById("_ctl0_txtToDate6").focus();
					return false;
				}
				if(DeltaMonth1Month2(document.getElementById("_ctl0_txtFromDate6"),document.getElementById("_ctl0_txtToDate6"))<=0)
				{
					GetAlertError(iTotal,DSAlert,"SIBook07");
					document.getElementById("_ctl0_txtFromDate6").focus();
					return false
				}
		}
		return ;
		
	} 
	function DeltaMonth1Month2(field1, field2)
	{
			var value1 = field1.value;
			var value2 = field2.value;
			if (value1 == "" || value2 == "")
				return;
		year1 = value1.substr(3,4);
		month1 = value1.substr(0,2);
		year2 = value2.substr(3,4);
		month2 = value2.substr(0,2);
		var delta = (parseInt(year2,10) * 12 + parseInt(month2,10)) - (parseInt(year1,10) * 12 + parseInt(month1,10)) + 1;
		return delta;
   
	}
	function OpenWindowEmp(strField)
	{
		ShowDialog('FormPage.aspx?Ascx=MdlHR/EmpPopup.ascx&Params=3');
	}
	function OpenWindowSigner(strField)
	{
		ShowDialog('FormPage.aspx?Ascx=MdlHR/EmpPopup.ascx&Params=4');
	}
	function ReturnEmpPopUp3(strEmpID, strEmpCode, strEmpName)
	{
		if (document.getElementById("_ctl0_trToKhaiBH").style.display == "")
		{
			document.getElementById("_ctl0_txtEmpID").value = strEmpID;
			document.getElementById("_ctl0_txtEmpCode").value = strEmpCode;
		}
		else if (document.getElementById("_ctl0_trSIProcess").style.display == "")
		{
			document.getElementById("_ctl0_txtEmpID1").value = strEmpID;
		}
		else if (document.getElementById("_ctl0_trEmpRegisterLB").style.display == "")
		{
			document.getElementById("_ctl0_txtEmpID8").value = strEmpID;
		}
	}
	function ReturnEmpPopUp4(strEmpID, strEmpCode, strEmpName)
	{
		document.getElementById("_ctl0_txtEmpID2").value = strEmpID;
		document.getElementById("_ctl0_txtSigner").value = strEmpID;
	}
	
	function remain(obj)
	{
		document.getElementById("_ctl0_remainfield").value = obj.value;
	}
	
	function CheckQuarterWhenNotNull(field)
	{	
		if (field.value == "")
			return true;
		if (checkNumeric(field) == false)
			return false;
		value = field.value;
		if (value != "1" &&value != "2" && value != "3" && value != "4")
		{
			//alert("Quarter is invalid!");
			GetAlertError(iTotal,DSAlert,"0043");
			field.focus();
			return false;
		}
	}
</script>
