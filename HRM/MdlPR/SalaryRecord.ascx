<%@ Import Namespace ="iHRPCore.Com"%>
<%@ Import Namespace ="System.Data" %>
<%@ Import Namespace ="System.Data.SqlClient" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="SalaryRecord.ascx.cs" Inherits="iHRPCore.MdlPR.SalaryRecord" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="EmpHeader" Src="../Include/EmpHeader.ascx" %>
<%
	string strLangID = Session["LangID"] == null?"EN":Session["LangID"].ToString().Trim();
	int Total1 = 0;
	int Total2 = 0;	
			
	DataTable rs1 = new DataTable();
	DataTable rs2 = new DataTable();			
	
	
	
%>
<SCRIPT language="javascript">	
		var DS1 = new Array(4);
		DS1[0] = new Array(<%=Total1%>);
		DS1[1] = new Array(<%=Total1%>);
		DS1[2] = new Array(<%=Total1%>);
		DS1[3] = new Array(<%=Total1%>);
		DS1[4] = new Array(<%=Total1%>);		
		<% for(int i=0; i<Total1; i++) {%>		
			DS1[0][<%=i%>]="<%=rs1.Rows[i]["LSSalaryScaleID"]%>";
			DS1[1][<%=i%>]="<%=rs1.Rows[i]["LSSalaryGradeID"]%>";			
			DS1[3][<%=i%>]="<%=rs1.Rows[i]["LSSalaryVersionID"]%>";			
			DS1[4][<%=i%>]="<%=rs1.Rows[i]["MonthIncreaseSal"]%>";			
			<% if (strLangID=="EN") {%>
				DS1[2][<%=i%>]="<%=rs1.Rows[i]["Name"]%>";	
			<%} else{%>
				DS1[2][<%=i%>]="<%=rs1.Rows[i]["VN_Name"]%>";
			<%}%>
		<%}%>		
						
		var DS2 = new Array(3);
		DS2[0] = new Array(<%=Total2%>);
		DS2[1] = new Array(<%=Total2%>);
		DS2[2] = new Array(<%=Total2%>);
		DS2[3] = new Array(<%=Total2%>);
		
		<% for(int i=0; i<Total2; i++) {%>					
			DS2[3][<%=i%>]="<%=rs2.Rows[i]["LSSalaryGradeID"]%>";
			DS2[0][<%=i%>]="<%=rs2.Rows[i]["LSSalaryRankID"]%>";
			<% if (strLangID=="EN") {%>
				DS2[1][<%=i%>]="<%=rs2.Rows[i]["Name"]%>";	
			<%} else{%>
				DS2[1][<%=i%>]="<%=rs2.Rows[i]["VN_Name"]%>";
			<%}%>
			DS2[2][<%=i%>]="<%=rs2.Rows[i]["BasicSalary"]%>";
		<%}%>						
		
</SCRIPT>
<script language="javascript">
function checkDate(fromDate, toDate)
{
	if(document.getElementById(fromDate).value!="" && document.getElementById(toDate).value!="")
	{
		if (FromSmallToDate(document.getElementById(fromDate),document.getElementById(toDate)) == false)
		{
			//alert("Notice date from must less or equal to notice date to!");
			GetAlertError(iTotal, DSAlert, "0033");
			document.getElementById(fromDate).focus();
			return false;
		}
	}
	return true;
}
function ChangeVersion()
	{
		var all = document.getElementById("_ctl0_cboLSSalaryGradeID").length;
		var all1 = document.getElementById("_ctl0_cboLSSalaryRankID").length;
		
		for(i=0; i<all; i++)
		{	
			document.getElementById("_ctl0_cboLSSalaryGradeID").remove(0);			
		};
		
		for(i=0; i<all1; i++)
		{	
			document.getElementById("_ctl0_cboLSSalaryRankID").remove(0);			
		};
		
		var strVersion = document.getElementById("_ctl0_cboLSSalaryVersionID").value; 
		document.getElementById("_ctl0_cboLSSalaryGradeID").add(new Option('',''));
		for(i=0; i<<%=Total1%>;i++)
		{
			if (DS1[3][i]==strVersion)
			{
				document.getElementById("_ctl0_cboLSSalaryGradeID").add(new Option(DS1[2][i],DS1[1][i]));
			};
		};
		all = document.getElementById("_ctl0_cboLSSalaryGradeID").length;
		document.getElementById("_ctl0_cboLSSalaryGradeID").selectedIndex = 0;
		ChangeGrade();	
	}
	function ChangeGrade()
	{
		document.getElementById("_ctl0_txtBasicSalary").value='';
		var all = document.getElementById("_ctl0_cboLSSalaryRankID").length;		
		for(i=0; i<all; i++)
		{	
			document.getElementById("_ctl0_cboLSSalaryRankID").remove(0);			
		};
		
		var strGrade = document.getElementById("_ctl0_cboLSSalaryGradeID").value; 
		document.getElementById("_ctl0_cboLSSalaryRankID").add(new Option('',''));
		
		for(i=0; i<<%=Total2%>;i++)
		{
			if (DS2[3][i]==strGrade)
			{
				document.getElementById("_ctl0_cboLSSalaryRankID").add(new Option(DS2[1][i],DS2[0][i]));
			};
		};
		
		for(i=0; i<<%=Total1%>;i++)
		{
			if (DS1[1][i]==strGrade)
			{
				document.getElementById("_ctl0_txtMonthIncreaseSal").value=DS1[4][i];
				CalIncreaseSal(DS1[4][i]);
			};
		};
		all = document.getElementById("_ctl0_cboLSSalaryRankID").length;
		document.getElementById("_ctl0_cboLSSalaryRankID").selectedIndex = 0;
		document.getElementById("_ctl0_txtLSSalaryGradeID").value = strGrade;
		ChangeRank();			
	}
function ChangeRank()
	{
		
		document.getElementById("_ctl0_txtLSRankID").value = document.getElementById("_ctl0_cboLSSalaryRankID").value;
		var strRankID=document.getElementById("_ctl0_cboLSSalaryRankID").value;
		for(i=0; i<<%=Total2%>;i++)
		{
			//alert(strRankID);
			if (DS2[0][i]==strRankID)
			{				
				document.getElementById("_ctl0_txtBasicSalary").value = DS2[2][i];
			};
		};
		document.getElementById("_ctl0_txtLSSalaryRankID").value = strRankID;	
	}
function CalIncreaseSal()	
	{
		var value,count;
		value=document.getElementById("_ctl0_txtActualDate").value; 
		count=document.getElementById("_ctl0_txtMonthIncreaseSal").value; 
		if (value !='' && count!='')
		{			
			document.getElementById("_ctl0_txtIncreaseDate").value=AddMonthstoDate(value,count);
		}
			
	}
	function CheckDate_(field)
	{
		CheckDate(field);
		CalIncreaseSal();	
	}	
</script>
<TABLE id="MainTable" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td>
			<TABLE id="tblHead" cellSpacing="0" cellPadding="0" width="95%" border="0">
				<TR>
					<TD align="center" width="100%" colSpan="3"><asp:label id="lblErr" ForeColor="Red" runat="server" CssClass="lblErr"></asp:label><asp:textbox id="txtMonthIncreaseSal" style="DISPLAY:none" CssClass="InputReadOnly" Runat="server"></asp:textbox></TD>
				</TR>
				<TR>
					<TD align="center" width="100%" colSpan="3"><uc1:empheader id="HR_EmpHeader" runat="server"></uc1:empheader></TD>
				</TR>
			</TABLE>
			<table id="tblInfo" width="95%">
				<tr>
					<td>
						<!-- Div LUONG CHUC DANH-->
						<table id="tbl2" cellSpacing="0" cellPadding="1" width="100%" border="0">
							<TR>
								<TD width="15%"><asp:label id="lblEffDate" runat="server" CssClass="labelRequire">Eff date</asp:label></TD>
								<TD width="33%"><asp:textbox id="txtFromDate" onblur="javascript:CheckDate(this)" runat="server" CssClass="input"
										MaxLength="10" Width="70px"></asp:textbox>&nbsp;<INPUT class=btnCal onclick="javascript:popUpCalendar(<%= this.txtFromDate.ClientID %>)" type=button></TD>
								<TD width="4%"></TD>
								<TD width="15%"><asp:label id="lblActualDate" runat="server" CssClass="labelRequire">Actual Date</asp:label></TD>
								<TD width="33%"><asp:textbox id="txtActualDate" onblur="javascript:CheckDate(this)" runat="server" CssClass="input"
										MaxLength="10" Width="70px"></asp:textbox>&nbsp;<INPUT class=btnCal onclick="javascript:popUpCalendar(<%= this.txtActualDate.ClientID %>)" type=button></TD>
							</TR>
							<TR>
								<TD width="15%"><asp:label id="lblIncreaseDate" runat="server" CssClass="label">Increase Date</asp:label></TD>
								<TD width="33%"><asp:textbox id="txtIncreaseDate" onblur="javascript:CheckDate(this)" runat="server" CssClass="input"
										MaxLength="10" Width="70px"></asp:textbox>&nbsp;<INPUT class=btnCal onclick="javascript:popUpCalendar(<%= this.txtIncreaseDate.ClientID %>)" type=button></TD>
								<TD width="4%"></TD>
								<TD width="15%" colSpan="2"><asp:radiobutton id="opt1" CssClass="option" Runat="server" GroupName="option" Checked="True" Text="No Decision"></asp:radiobutton>&nbsp;
									<asp:radiobutton id="opt2" CssClass="option" Runat="server" GroupName="option" Text="Have Decision"></asp:radiobutton><asp:label id="Label29" runat="server" CssClass="label" Visible="False">End date</asp:label><asp:textbox id="txtToDate" runat="server" CssClass="input" MaxLength="10" Width="16px" Visible="False"></asp:textbox></TD>
							</TR>
							<TR id="trDecision1" runat="server">
								<TD width="15%"><asp:label id="lblDecisionNo" runat="server" CssClass="labelRequire">Decision No</asp:label></TD>
								<TD vAlign="top" width="33%"><asp:textbox id="txtDecisionNo" style="POSITION: absolute" runat="server" CssClass="input" MaxLength="20"
										Width="100%"></asp:textbox></TD>
								<TD width="4%"></TD>
								<TD width="15%"><asp:label id="lblSignDate" runat="server" CssClass="label">Sign Date</asp:label></TD>
								<td width="33%"><asp:textbox id="txtSignDate" onblur="javascript:CheckDate(this)" runat="server" CssClass="input"
										MaxLength="10" Width="70px"></asp:textbox>&nbsp;<INPUT class=btnCal onclick="javascript:popUpCalendar(<%= this.txtSignDate.ClientID %>)" type=button></td>
							</TR>
							<TR id="trDecision2" runat="server">
								<TD><asp:label id="lblSigner" runat="server" CssClass="label">Signer</asp:label></TD>
								<TD vAlign="top"><asp:textbox id="txtSigner" style="POSITION: absolute" runat="server" CssClass="input" MaxLength="50"
										Width="100%"></asp:textbox></TD>
								<TD></TD>
								<TD><asp:label id="lblSignPosition" runat="server" CssClass="label">Pos of signer</asp:label></TD>
								<TD vAlign="top"><asp:textbox id="txtSignPosition" style="POSITION: absolute" runat="server" CssClass="input"
										MaxLength="50" Width="100%"></asp:textbox></TD>
							</TR>
							<TR id="trDecision3" runat="server">
								<TD><asp:label id="lblAttachFile" runat="server" CssClass="label">Attach File</asp:label></TD>
								<TD><INPUT class="input" id="txtFile" style="WIDTH: 210px" tabIndex="1" type="file" size="13"
										name="File1" runat="server"></TD>
								<TD></TD>
								<TD colSpan="2"><asp:hyperlink id="hpSelectFile" runat="server" CssClass="LinkLeft"></asp:hyperlink><asp:linkbutton id="btnDeleteFile" runat="server" CssClass="DeleteFile" Visible="False" ToolTip="Delete Image"></asp:linkbutton></TD>
							</TR>
							<TR style="DISPLAY:none">
								<TD><asp:label id="Label6" runat="server" CssClass="label"> Ver of sal</asp:label></TD>
								<TD><asp:dropdownlist id="cboLSSalaryVersionID" runat="server" CssClass="select" Width="100%" onChange="ChangeVersion()"></asp:dropdownlist></TD>
								<TD></TD>
								<TD><asp:label id="lblGradeOfSal" runat="server" CssClass="label">Grade of sal</asp:label></TD>
								<TD><asp:dropdownlist id="cboLSSalaryGradeID" runat="server" CssClass="select" Width="100%" onchange="ChangeGrade();"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD>
									<asp:label id="Label3" CssClass="labelRequire" runat="server">Salary For SI</asp:label></TD>
								<TD>
									<asp:textbox id="txtSalarySI" onblur="javascript:checkNumeric(this)" style="TEXT-ALIGN: right"
										CssClass="input" runat="server" Width="100%" MaxLength="10"></asp:textbox></TD>
								<TD></TD>
								<TD><asp:label id="lblSalaryPct" runat="server" CssClass="labelRequire">Percent salary</asp:label></TD>
								<TD><asp:textbox id="txtSalaryPct" style="TEXT-ALIGN:right" onblur="javascript:CheckPercent(this)"
										runat="server" CssClass="input" MaxLength="7" Width="100%"></asp:textbox></TD>
							</TR>
							<TR>
								<TD><asp:label id="lblSalStandard" runat="server" CssClass="labelRequire">Basic Salary</asp:label></TD>
								<TD>
									<asp:textbox id="txtBasicSalary" style="TEXT-ALIGN:right" runat="server" onblur="javascript:checkNumeric(this)"
										CssClass="input" MaxLength="10" Width="100%"></asp:textbox></TD>
								<TD></TD>
								<TD>
									<asp:label id="Label12" CssClass="labelRequire" runat="server" Width="100%">Currency BS</asp:label></TD>
								<TD>
									<asp:dropdownlist id="cboLSCurrencyTypeID" CssClass="combo" runat="server" Width="100%"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD>
									<asp:label id="lblOtherSalary1" CssClass="label" runat="server">Other salary 1</asp:label></TD>
								<TD>
									<asp:textbox id="txtOtherSalary1" onblur="javascript:checkNumeric(this)" CssClass="input" runat="server"
										Width="100%" MaxLength="10"></asp:textbox></TD>
								<TD></TD>
								<TD>
									<asp:label id="Label7" CssClass="label" runat="server" Width="100%">Currency 1</asp:label></TD>
								<TD>
									<asp:dropdownlist id="cboLSCurrencyTypeID1" CssClass="combo" runat="server" Width="100%"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 13px">
									<asp:label id="lblOtherSalary2" CssClass="label" runat="server">Other salary 2</asp:label></TD>
								<TD style="HEIGHT: 13px">
									<asp:textbox id="txtOtherSalary2" onblur="javascript:checkNumeric(this)" CssClass="input" runat="server"
										Width="100%" MaxLength="7"></asp:textbox></TD>
								<TD style="HEIGHT: 13px"></TD>
								<TD style="HEIGHT: 13px">
									<asp:label id="Label13" CssClass="label" runat="server" Width="100%">Currency 2</asp:label></TD>
								<TD style="HEIGHT: 13px">
									<asp:dropdownlist id="cboLSCurrencyTypeID2" CssClass="combo" runat="server" Width="100%"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD>
									<asp:label id="lblOtherSalary3" CssClass="label" runat="server">Other salary 3</asp:label></TD>
								<TD>
									<asp:textbox id="txtOtherSalary3" onblur="javascript:checkNumeric(this)" CssClass="input" runat="server"
										Width="100%" MaxLength="7"></asp:textbox></TD>
								<TD></TD>
								<TD>
									<asp:label id="Label14" CssClass="label" runat="server" Width="100%">Currency 3</asp:label></TD>
								<TD>
									<asp:dropdownlist id="cboLSCurrencyTypeID3" CssClass="combo" runat="server" Width="100%"></asp:dropdownlist></TD>
							</TR>
							<TR style="DISPLAY:none">
								<TD></TD>
								<TD>
									<asp:CheckBox id="chkIsUI" runat="server" Text="Unemployment Insurance?" Checked="True"></asp:CheckBox></TD>
								<TD></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD colspan="4">
									<asp:RadioButtonList id="optIsAfterTax" runat="server" Width="280px" RepeatDirection="Horizontal">
										<asp:ListItem Value="1" Selected="True">NET</asp:ListItem>
										<asp:ListItem Value="0">Gross</asp:ListItem>
									</asp:RadioButtonList></TD>
							</TR>
							<TR style="DISPLAY:none">
								<TD id="tdAdOtherSalary2lbl" runat="server"><asp:checkbox id="chkAdOtherSalary2" runat="server" CssClass="chkAdmin" Checked="True"></asp:checkbox></TD>
								<TD id="tdAdOtherSalary2txt" runat="server"></TD>
								<TD id="tdAdOtherSalary2cbo" runat="server"></TD>
								<TD id="tdAdOtherSalary3lbl" runat="server">
									<asp:checkbox id="chkAdOtherSalary3" CssClass="chkAdmin" runat="server" Checked="True"></asp:checkbox></TD>
								<TD id="tdAdOtherSalary3txt" runat="server"></TD>
							</TR>
							<TR style="DISPLAY: none">
								<TD>
									<asp:label id="lblVerOfSal" CssClass="label" runat="server" Visible="False"> Ver of sal</asp:label></TD>
								<TD></TD>
								<TD></TD>
								<TD><asp:radiobutton id="Radiobutton2" CssClass="option" Runat="server" GroupName="option" Checked="True"
										Text="khoán ngày" Visible="False"></asp:radiobutton></TD>
								<TD><asp:radiobutton id="Radiobutton1" CssClass="option" Runat="server" GroupName="option" Text="Khoán tháng"
										Visible="False"></asp:radiobutton></TD>
							</TR>
							<TR style="DISPLAY:none">
								<TD id="tdAdAll1lbl" runat="server"><asp:checkbox id="chkAdAll1" runat="server" CssClass="chkAdmin" Checked="True"></asp:checkbox><asp:label id="lblAllowanceCoef1" runat="server" CssClass="label">All. coef 1</asp:label></TD>
								<TD id="tdAdAll1txt" runat="server"><asp:textbox id="txtAllowanceCoef1" onblur="javascript:checkNumeric(this)" runat="server" CssClass="input"
										MaxLength="7" Width="100%"></asp:textbox></TD>
								<TD id="tdAdAll1cbo" runat="server"></TD>
								<TD id="tdAdAll2lbl" runat="server"><asp:checkbox id="chkAdAll2" runat="server" CssClass="chkAdmin" Checked="True"></asp:checkbox><asp:label id="lblAllowanceCoef2" runat="server" CssClass="label">All. coef 2</asp:label></TD>
								<TD id="tdAdAll2txt" runat="server"><asp:textbox id="txtAllowanceCoef2" onblur="javascript:checkNumeric(this)" runat="server" CssClass="input"
										MaxLength="7" Width="100%"></asp:textbox></TD>
							</TR>
							<TR style="DISPLAY:none">
								<TD id="tdAdAll3lbl" runat="server"><asp:checkbox id="chkAdAll3" runat="server" CssClass="chkAdmin" Checked="True"></asp:checkbox><asp:label id="lblAllowanceCoef3" runat="server" CssClass="label">All. coef 3</asp:label></TD>
								<TD id="tdAdAll3txt" runat="server"><asp:textbox id="txtAllowanceCoef3" onblur="javascript:checkNumeric(this)" runat="server" CssClass="input"
										MaxLength="7" Width="100%"></asp:textbox></TD>
								<TD id="tdAdAll3cbo" runat="server"></TD>
								<TD id="tdAdAll4lbl" runat="server"><asp:checkbox id="chkAdAll4" runat="server" CssClass="chkAdmin" Checked="True"></asp:checkbox><asp:label id="lblAllowanceCoef4" runat="server" CssClass="label">All. coef 4</asp:label></TD>
								<TD id="tdAdAll4txt" runat="server"><asp:textbox id="txtAllowanceCoef4" onblur="javascript:checkNumeric(this)" runat="server" CssClass="input"
										MaxLength="7" Width="100%"></asp:textbox></TD>
							</TR>
							<TR style="DISPLAY:none">
								<TD id="tdAdAll5lbl" runat="server"><asp:checkbox id="chkAdAll5" runat="server" CssClass="chkAdmin" Checked="True"></asp:checkbox><asp:label id="lblAllowanceCoef5" runat="server" CssClass="label">All. coef 5</asp:label></TD>
								<TD id="tdAdAll5txt" runat="server"><asp:textbox id="txtAllowanceCoef5" onblur="javascript:checkNumeric(this)" runat="server" CssClass="input"
										MaxLength="7" Width="100%"></asp:textbox></TD>
								<TD id="tdAdAll5cbo" runat="server"></TD>
								<TD id="tdAdAll6lbl" runat="server"><asp:checkbox id="chkAdAll6" runat="server" CssClass="chkAdmin" Checked="True"></asp:checkbox><asp:label id="lblAllowanceCoef6" runat="server" CssClass="label">All. coef 6</asp:label></TD>
								<TD id="tdAdAll6txt" runat="server"><asp:textbox id="txtAllowanceCoef6" onblur="javascript:checkNumeric(this)" runat="server" CssClass="input"
										MaxLength="7" Width="100%"></asp:textbox></TD>
							</TR>
							<TR style="DISPLAY:none">
								<TD id="tdAdAll7lbl" runat="server"><asp:checkbox id="chkAdAll7" runat="server" CssClass="chkAdmin" Checked="True"></asp:checkbox><asp:label id="lblAllowanceCoef7" runat="server" CssClass="label">All. coef 7</asp:label></TD>
								<TD id="tdAdAll7txt" runat="server"><asp:textbox id="txtAllowanceCoef7" onblur="javascript:checkNumeric(this)" runat="server" CssClass="input"
										MaxLength="7" Width="100%"></asp:textbox></TD>
								<TD id="tdAdAll7cbo" runat="server"></TD>
								<TD id="tdAdAll8lbl" runat="server"><asp:checkbox id="chkAdAll8" runat="server" CssClass="chkAdmin" Checked="True"></asp:checkbox><asp:label id="lblAllowanceCoef8" runat="server" CssClass="label">All. coef 8</asp:label></TD>
								<TD id="tdAdAll8txt" runat="server"><asp:textbox id="txtAllowanceCoef8" onblur="javascript:checkNumeric(this)" runat="server" CssClass="input"
										MaxLength="7" Width="100%"></asp:textbox></TD>
							</TR>
							<TR style="DISPLAY:none">
								<TD id="tdAdAll9lbl" runat="server"><asp:checkbox id="chkAdAll9" runat="server" CssClass="chkAdmin" Checked="True"></asp:checkbox><asp:label id="lblAllowanceCoef9" runat="server" CssClass="label">All. coef 7</asp:label></TD>
								<TD id="tdAdAll9txt" runat="server"><asp:textbox id="txtAllowanceCoef9" onblur="javascript:checkNumeric(this)" runat="server" CssClass="input"
										MaxLength="7" Width="100%"></asp:textbox></TD>
								<TD id="tdAdAll9cbo" runat="server"></TD>
								<TD id="tdAdAll10lbl" runat="server"><asp:checkbox id="chkAdAll10" runat="server" CssClass="chkAdmin" Checked="True"></asp:checkbox><asp:label id="lblAllowanceCoef10" runat="server" CssClass="label">All. coef 8</asp:label></TD>
								<TD id="tdAdAll10txt" runat="server"><asp:textbox id="txtAllowanceCoef10" onblur="javascript:checkNumeric(this)" runat="server" CssClass="input"
										MaxLength="7" Width="100%"></asp:textbox></TD>
							</TR>
							<TR>
								<TD><asp:label id="lblNote" runat="server" CssClass="label">Note</asp:label></TD>
								<TD colSpan="4"><asp:textbox id="txtNote" runat="server" CssClass="input" MaxLength="255" Width="100%" Height="50px"
										TextMode="MultiLine"></asp:textbox></TD>
							</TR> <!-- END DIV LUONG CHUC DANH --></table>
						<INPUT id="txtLSRankID" type="hidden" runat="server">
						<asp:TextBox id="txtEmpID1" runat="server" Visible="False">EmpID</asp:TextBox>
					</td>
				</tr>
			</table> <!-- end button for input form --></td>
	</tr> <!-- start button for input form -->
	<TR>
		<TD noWrap align="center">
			<TABLE id="tblButton" cellSpacing="0" cellPadding="0" width="99%" border="0">
				<TR vAlign="top" height="30">
					<TD vAlign="middle" width="15%"><asp:checkbox id="chkShowGrid" accessKey="G" onclick="javascript:collapse('_ctl0_trGrid');" runat="server"
							CssClass="checkbox" Checked="True" Text="Show Datagrid" ToolTip="Alt+G"></asp:checkbox></TD>
					<TD align="center" width="*"><asp:linkbutton id="btnAddNew" accessKey="N" runat="server" CssClass="btnAddnew" ToolTip="Alt+N">Add New</asp:linkbutton>&nbsp;
						<asp:linkbutton id="btnSave" accessKey="S" runat="server" CssClass="btnSave" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnDelete" accessKey="D" runat="server" CssClass="btnExport" ToolTip="Alt + D">Delete</asp:linkbutton>&nbsp;
						<asp:linkbutton id="btnImport" accessKey="I" runat="server" CssClass="btnImport" Visible="False"
							ToolTip="Alt+I, Import data from excel file same format with export file">Import</asp:linkbutton>&nbsp;&nbsp;
						<asp:linkbutton id="btnExport" accessKey="E" runat="server" CssClass="btnExport" ToolTip="Alt+E, Export data in grid to excel file">Export</asp:linkbutton></TD>
				</TR>
			</TABLE>
		</TD>
	</TR> <!-- end button for input form -->
	<TR id="trGrid" runat="server">
		<TD align="center"><!-- start grid for input form -->
			<TABLE id="tblGrid" cellSpacing="0" cellPadding="1" width="98%" border="0">
				<TR id="trGrid1"> <!-- start grid detail for input form -->
					<TD colSpan="2"><asp:datagrid id="dtgList" runat="server" CssClass="grid" Width="100%" AllowPaging="True" BackColor="White"
							BorderColor="#3366CC" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False" AllowSorting="True">
							<FooterStyle CssClass="gridFooter"></FooterStyle>
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle CssClass="gridHeader"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="SalaryRecordID">
									<HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Select">
									<HeaderStyle HorizontalAlign="Center" Width="7%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox id="chkSelect" Enabled="true" runat="server" CssClass="checkbox"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="&lt;b&gt;Seq&lt;/b&gt;">
									<HeaderStyle Width="4%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<%# Container.ItemIndex + 1%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:ButtonColumn DataTextField="FromDate" HeaderText="Effective Date" CommandName="EDIT">
									<HeaderStyle HorizontalAlign="Center" Width="12%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:ButtonColumn>
								<asp:BoundColumn DataField="IncreaseDate" HeaderText="Increase Date">
									<HeaderStyle HorizontalAlign="Center" Width="12%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="BasicSalary" HeaderText="Basic Salary">
									<HeaderStyle HorizontalAlign="Center" Width="12%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="OtherSalary1" HeaderText="Other Salary 1">
									<HeaderStyle HorizontalAlign="Center" Width="12%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="OtherSalary2" HeaderText="Other Salary 2">
									<HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="OtherSalary3" HeaderText="Other Salary 3">
									<HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="SalaryPct" HeaderText="Salary Percent">
									<HeaderStyle HorizontalAlign="Center" Width="9%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE> <!-- end grid for input form --></TD>
	</TR>
	<TR>
		<TD style="DISPLAY: none" align="center"><asp:textbox id="txtAttachFile" runat="server"></asp:textbox><INPUT id="txtSalaryRecordID" type="hidden" name="txtSalaryRecordID" runat="server"><INPUT id="txtLSSalaryRankID" type="hidden" runat="server"><INPUT id="txtLSSalaryGradeID" type="hidden" runat="server"></TD>
	</TR>
</TABLE>
<SCRIPT type="text/javascript">	
	function showtable(object)
	{
	
		if(object=="_ctl0_opt1")
		{
			if (document.getElementById('_ctl0_txtDecisionNo').value!="")
			{
				if (confirm(GetAlertText(iTotal,DSAlert,"SR_0001"))==true)
				{
					document.getElementById('_ctl0_txtDecisionNo').value="";
					document.getElementById('_ctl0_txtSignDate').value="";
					document.getElementById('_ctl0_txtSigner').value="";
					document.getElementById('_ctl0_txtSignPosition').value="";					
					document.getElementById('_ctl0_txtAttachFile').value="";										
				}
			}
			
			document.getElementById("_ctl0_trDecision1").style.display="none";
			document.getElementById("_ctl0_trDecision2").style.display="none";
			document.getElementById("_ctl0_trDecision3").style.display="none";
		}
		if(object=="_ctl0_opt2")
		{
			document.getElementById(object).checked=true;
			document.getElementById("_ctl0_trDecision1").style.display="";
			document.getElementById("_ctl0_trDecision2").style.display="";
			document.getElementById("_ctl0_trDecision3").style.display="";			
		}		
	}
	function checkSave()
	{
		if (document.getElementById("_ctl0_HR_EmpHeader_txtEmpID").title != document.getElementById("_ctl0_HR_EmpHeader_txtEmpID").value)
		{
			document.getElementById("_ctl0_HR_EmpHeader_txtEmpID").value = "";
		}
		if(checkisnull('HR_EmpHeader_txtEmpID')==false)  return false;
		if(checkisnull('txtFromDate')==false)  return false;
		if(checkisnull('txtActualDate')==false) return false;
		//if(checkisnull('txtIncreaseDate')==false) return false;		
		//if(checkisnull('cboLSSalaryRankID')==false)  return false;
		if(checkisnull('txtBasicSalary')==false)  return false;
		if(checkisnull('txtSalaryPct')==false)  return false;
		if(checkisnull('txtSalarySI')==false)  return false;
		if(checkisnull('cboLSCurrencyTypeID')==false)  return false;
		/*if(!checkDate('_ctl0_txtActualDate', '_ctl0_txtFromDate')){
			return false;
		}*/
		
		if( document.getElementById('_ctl0_txtActualDate').value!="" && document.getElementById('_ctl0_txtFromDate').value!="")
		{
			if( IsSmallerOrEqual(document.getElementById('_ctl0_txtActualDate').value,document.getElementById('_ctl0_txtFromDate').value) == false)
			{		
				GetAlertError(iTotal, DSAlert, "PR_0033");
				document.getElementById('_ctl0_txtFromDate').focus();
				return false;
			}
		}	
		/* LanHTD: 2006 11 28: Khong can kiem tra dieu kien nay
		if(FromSmallToDate(document.getElementById('_ctl0_txtFromDate'),document.getElementById('_ctl0_txtActualDate')) == false)
			{
								GetAlertError(iTotal,DSAlert,"SR_0002")
								document.getElementById('_ctl0_txtActualDate').focus();
								return false;
			}				
		if(FromSmallToDate(document.getElementById('_ctl0_txtActualDate'),document.getElementById('_ctl0_txtIncreaseDate')) == false)
			{
								GetAlertError(iTotal,DSAlert,"SR_0003")
								document.getElementById('_ctl0_txtIncreaseDate').focus();
								return false;
			}				
		*/
		if (document.getElementById('_ctl0_opt2').checked==true)
			{
				if (checkisnull('txtDecisionNo')==false) return false;
				if(document.getElementById('_ctl0_txtSignDate').value!="")
				{
					if(FromSmallNow(document.getElementById('_ctl0_txtSignDate')) == false)
					{
						//alert('Date of birth must be less than now');
						GetAlertError(iTotal,DSAlert,"0011");
						document.getElementById('_ctl0_txtSignDate').focus();
						return false;
					}		
				}
			}
		return true;
	}
function checkdelete()
{
	if(GridCheck('_ctl0_dtgList',3,1,'chkSelect')==false)
	{			
		GetAlertError(iTotal,DSAlert,"0001");				
		return false;
	}
	if(confirm(GetAlertText(iTotal,DSAlert,"0002"))==false){
		return false;
	}
	
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
function checkDeleteAtt()
{
	if (confirm(GetAlertText(iTotal,DSAlert,"0006"))==false)
	{
		return false;
	}
}

function IsDateSmaller(inputStr1, inputStr2)
{
	var delim1 = inputStr1.indexOf("/")
	var delim2 = inputStr1.lastIndexOf("/")
	
	// lay ngay, thang, nam cua ngay1
	var dd1 = parseInt(inputStr1.substring(0,delim1),10)
	var mm1 = parseInt(inputStr1.substring(delim1 + 1,delim2),10)
	var yyyy1 = parseInt(inputStr1.substring(delim2 + 1,inputStr1.length),10)

	delim1 = inputStr2.indexOf("/")
	delim2 = inputStr2.lastIndexOf("/")

	// lay ngay, thang, nam cua ngay2
	dd2 = parseInt(inputStr2.substring(0,delim1),10)
	mm2 = parseInt(inputStr2.substring(delim1 + 1,delim2),10)
	yyyy2 = parseInt(inputStr2.substring(delim2 + 1,inputStr2.length),10)
	
	
	// ngay1 nho hon ngay 2 ?
	if (yyyy2 > yyyy1)
		return true;
	else if (yyyy2 == yyyy1)
	{
		if (mm2 == mm1) 
			{
				
				if (dd2 > dd1) 
				{				
				return true;
				}				
				else
				{				
				 return false;
				}
			}
		else
			{
				if (mm2 > mm1) return true;
				else return false;
			}
	}
	else
		return false;
}

if (document.getElementById('_ctl0_opt2').checked==true)
{
showtable('_ctl0_opt2');
}else
{
showtable('_ctl0_opt1');
}
</SCRIPT>
