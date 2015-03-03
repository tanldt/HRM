<%@ Register TagPrefix="uc1" TagName="EmpHeader" Src="../Include/EmpHeader.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="WorkingRecord.ascx.cs" Inherits="iHRPCore.MdlHR.WorkingRecord" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Import Namespace ="System.Data.SqlClient" %>
<%@ Import Namespace ="System.Data" %>
<%@ Import Namespace ="iHRPCore.Com"%>
<meta content="True" name="vs_showGrid">
<%
	string strLangID = Session["LangID"] == null?"VN":Session["LangID"].ToString().Trim();
	int Total2 = 0;
	int Total3 = 0;
	int Total1 = 0;
	
	DataTable rs2 = new DataTable();		
	DataTable rs3 = new DataTable();	
	DataTable rs1 = new DataTable();
	
	rs1 = clsCommon.GetDataTable("sp_GetDataComboLevel @TableName = 'LS_tblLevel1', @Language='" + strLangID + "'");
	rs2 = clsCommon.GetDataTable("sp_GetDataComboLevel @TableName = 'LS_tblLevel2', @Language='" + strLangID + "'");
	rs3 = clsCommon.GetDataTable("sp_GetDataComboLevel @TableName = 'LS_tblLevel3', @Language='" + strLangID + "'");	
	
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
			DS1[0][<%=i%>]="<%=rs1.Rows[i]["LSCompanyID"]%>";
			DS1[1][<%=i%>]="<%=rs1.Rows[i]["LSLevel1ID"]%>";
			DS1[2][<%=i%>]="<%=rs1.Rows[i]["Name"]%>";	
		<%}%>
		//array for level2
		var DS2 = new Array(3);
		DS2[0] = new Array(<%=Total2%>);
		DS2[1] = new Array(<%=Total2%>);
		DS2[2] = new Array(<%=Total2%>);	
		<% for(int i=0; i<Total2; i++) {%>		
			DS2[0][<%=i%>]="<%=rs2.Rows[i]["LSLevel1ID"]%>";
			DS2[1][<%=i%>]="<%=rs2.Rows[i]["LSLevel2ID"]%>";
			DS2[2][<%=i%>]="<%=rs2.Rows[i]["Name"]%>";	
		<%}%>		
		//array for level3
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
		var all = document.getElementById("_ctl0_cboLevel3ID").length;
		
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
		<TD align="center"><uc1:empheader id="HR_EmpHeader" runat="server"></uc1:empheader></TD>
	</TR>
	<TR>
		<TD align="center"><!-- start detail for input form -->
			<TABLE cellSpacing="0" cellPadding="1" width="99%" border="0">
				<TR>
					<TD width="14%"><asp:label id="lblEffDate" runat="server" CssClass="labelRequire" Width="100%">Effective Date</asp:label></TD>
					<TD noWrap width="18%"><asp:textbox id="txtFromDate" onblur="JavaScript:CheckDate(this)" runat="server" CssClass="input"
							Width="70px" MaxLength="10"></asp:textbox>&nbsp;<INPUT class=btnCal id=btnCalFromDate onclick="javascript:popUpCalendar(<%= this.txtFromDate.ClientID %>)" type=button></TD>
					<TD noWrap width="19%"><asp:label id="lblEndDate" runat="server" CssClass="label">End Date</asp:label>&nbsp;
						<asp:textbox id="txtToDate" onblur="JavaScript:CheckDate(this)" runat="server" CssClass="InputReadOnly"
							Width="70px" MaxLength="10" ReadOnly="True"></asp:textbox></TD>
					<TD width="2%"></TD>
					<TD width="2%"></TD>
					<TD width="15%"><asp:label id="lblStatusChange" runat="server" CssClass="labelRequire" Width="100%">Status change</asp:label></TD>
					<TD width="25%"><asp:dropdownlist id="cboLSStatusChangeID" runat="server" CssClass="select" Width="100%"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD><asp:checkbox id="ChkDecision" onclick="javascript:collapseDecision()" runat="server" CssClass="checkbox"
							Text="Is Decision?"></asp:checkbox></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
				</TR>
				<TR id="trDecision1" runat="server">
					<TD><asp:label id="lblDecisionNo" runat="server" CssClass="labelRequire" Width="100%">Decision No</asp:label></TD>
					<TD><asp:textbox id="txtDecisionNo" runat="server" CssClass="input" Width="100px" MaxLength="20"></asp:textbox></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD><asp:label id="Label20" runat="server" CssClass="label" Width="100%">Sign Date</asp:label></TD>
					<TD><asp:textbox id="txtSignDate" onblur="JavaScript:CheckDate(this)" runat="server" CssClass="input"
							Width="70%" MaxLength="10"></asp:textbox>&nbsp;<INPUT class=btnCal onclick="javascript:popUpCalendar(<%= this.txtSignDate.ClientID %>)" type=button></TD>
				</TR>
				<TR id="trDecision2" runat="server">
					<TD><asp:label id="Label13" runat="server" CssClass="label" Width="100%">Signer</asp:label></TD>
					<TD><asp:textbox id="txtSigner" runat="server" CssClass="input" Width="100px" MaxLength="50"></asp:textbox></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD><asp:label id="Label14" runat="server" CssClass="label" Width="100%">Post title of signer</asp:label></TD>
					<TD><asp:textbox id="txtSignerPosition" runat="server" CssClass="input" Width="70%" MaxLength="20"></asp:textbox></TD>
				</TR>
				<TR id="trDecision3" runat="server">
					<TD><asp:label id="Label7" runat="server" CssClass="label" Width="100%">Select File</asp:label></TD>
					<TD colSpan="2"><INPUT class="input" id="txtFile" style="WIDTH: 100%" tabIndex="1" type="file" size="13"
							name="File1" runat="server"></TD>
					<TD></TD>
					<TD></TD>
					<TD colSpan="2"><asp:hyperlink id="hpSelectFile" runat="server" CssClass="LinkLeft"></asp:hyperlink><asp:linkbutton id="btnDeleteFile" runat="server" CssClass="DeleteFile" ToolTip="Delete Image"></asp:linkbutton></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblNote" runat="server" CssClass="label" Width="100%">Note</asp:label></TD>
					<TD vAlign="top" colSpan="7"><asp:textbox id="txtNote" runat="server" CssClass="input" Width="100%" MaxLength="100"></asp:textbox></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD vAlign="middle" align="center" colSpan="2"><asp:label id="Label1" runat="server" CssClass="label" Width="100%">From</asp:label></TD>
					<TD></TD>
					<TD></TD>
					<TD vAlign="middle" align="center" colSpan="2"><asp:label id="Label2" runat="server" CssClass="label" Width="100%">To</asp:label></TD>
				</TR>
				<TR style="DISPLAY: none">
					<TD><asp:label id="lblCompany" runat="server" CssClass="label" Width="100%">Customer group</asp:label></TD>
					<TD colSpan="2"><asp:label id="lblCompanyData" runat="server" CssClass="labelData" Width="100%"></asp:label></TD>
					<TD></TD>
					<TD></TD>
					<TD colSpan="2"><asp:dropdownlist id="cboCompanyID" runat="server" CssClass="combo" Width="100%" onchange="ChangeCompany()"></asp:dropdownlist></TD>
				</TR>
				<TR id="trAdLocation" runat="server">
					<TD><asp:checkbox id="chkAdLocation" CssClass="chkAdmin" Runat="server"></asp:checkbox><asp:label id="Label6" runat="server" CssClass="label" Width="60%">Function</asp:label></TD>
					<TD colSpan="2"><asp:label id="lblFromLocationData" runat="server" CssClass="labelData" Width="100%"></asp:label></TD>
					<TD></TD>
					<TD></TD>
					<TD colSpan="2"><asp:dropdownlist id="cboLocationID" runat="server" CssClass="combo" Width="100%"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD><asp:label id="Label3" runat="server" CssClass="label" Width="100%">Company</asp:label></TD>
					<TD colSpan="2"><asp:label id="lblFromDivision" runat="server" CssClass="labelData" Width="100%"></asp:label></TD>
					<TD></TD>
					<TD></TD>
					<TD colSpan="2"><asp:dropdownlist id="cboLevel1ID" runat="server" CssClass="combo" Width="100%" onchange="ChangeLevel1()"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblFromDepartment" runat="server" CssClass="label" Width="100%">Department</asp:label></TD>
					<TD colSpan="2"><asp:label id="lblFromDepartmentData" runat="server" CssClass="labelData" Width="100%"></asp:label></TD>
					<TD></TD>
					<TD></TD>
					<TD colSpan="2"><asp:dropdownlist id="cboLevel2ID" runat="server" CssClass="combo" Width="100%" onchange="ChangeLevel2()"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblFromSection" runat="server" CssClass="label" Width="100%">Group</asp:label></TD>
					<TD colSpan="2"><asp:label id="lblFromSectionData" runat="server" CssClass="labelData" Width="100%"></asp:label></TD>
					<TD></TD>
					<TD></TD>
					<TD colSpan="2"><asp:dropdownlist id="cboLevel3ID" runat="server" CssClass="combo" Width="100%" onchange="ChangeLevel3()"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblFromPosition" runat="server" CssClass="label" Width="100%">Job title</asp:label></TD>
					<TD colSpan="2"><asp:label id="lblFromPositionData" runat="server" CssClass="labelData" Width="100%"></asp:label></TD>
					<TD></TD>
					<TD></TD>
					<TD colSpan="2"><asp:dropdownlist id="cboPositionID" runat="server" CssClass="combo" Width="100%"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD><asp:label id="Label12" runat="server" CssClass="label" Width="100%">Post Category</asp:label></TD>
					<TD colSpan="2"><asp:label id="lblFromJobTitle" runat="server" CssClass="labelData" Width="100%"></asp:label></TD>
					<TD></TD>
					<TD></TD>
					<TD colSpan="2"><asp:dropdownlist id="cboJobTitle" runat="server" CssClass="combo" Width="100%"></asp:dropdownlist></TD>
				</TR>
				<tr id="GradeRankJC1" runat="server">
					<td><asp:label id="Label9" runat="server" CssClass="label" Width="100%">Grade</asp:label></td>
					<TD colSpan="2"><asp:label id="lblFromGrade" runat="server" CssClass="labelData" Width="100%"></asp:label></TD>
					<TD></TD>
					<TD></TD>
					<TD colSpan="2"><asp:dropdownlist id="cboGrade" runat="server" CssClass="combo" Width="100%" onchange="ChangeLevelGrade()"></asp:dropdownlist></TD>
				</tr>
				<TR id="GradeRankJC2" runat="server">
					<TD><asp:label id="Label4" runat="server" CssClass="label" Width="100%">Rank</asp:label></TD>
					<TD colSpan="2"><asp:label id="lblFromRank" runat="server" CssClass="labelData" Width="100%"></asp:label></TD>
					<TD></TD>
					<TD></TD>
					<TD colSpan="2"><asp:dropdownlist id="cboRank" runat="server" CssClass="combo" Width="100%" onchange="ChangeRank()"></asp:dropdownlist></TD>
				</TR>
				<TR id="GradeRankJC3" runat="server">
					<TD><asp:label id="Label10" runat="server" CssClass="label" Width="100%">Charge rate</asp:label></TD>
					<TD colSpan="2"><asp:label id="lblFromJobCode" runat="server" CssClass="labelData" Width="100%"></asp:label></TD>
					<TD></TD>
					<TD></TD>
					<TD colSpan="2"><asp:textbox id="lblJobCodeID" runat="server" CssClass="input" Width="100px" MaxLength="20" ReadOnly="True"></asp:textbox></TD>
				</TR>
				<TR id="Tr1" runat="server">
					<TD><asp:label id="Label8" runat="server" CssClass="label" Width="100%">Dialogue rating</asp:label></TD>
					<TD colSpan="2"><asp:label id="lblDialogue" runat="server" CssClass="labelData" Width="100%"></asp:label></TD>
					<TD></TD>
					<TD></TD>
					<TD colSpan="2"><asp:textbox id="txtDialog" runat="server" CssClass="input" Width="100px" MaxLength="50"></asp:textbox></TD>
				</TR>
				<TR>
					<TD><asp:label id="Label11" runat="server" CssClass="label" Width="100%">Employment type</asp:label></TD>
					<TD colSpan="2"><asp:label id="lblFromEmpType" runat="server" CssClass="labelData" Width="100%"></asp:label></TD>
					<TD></TD>
					<TD></TD>
					<TD colSpan="2"><asp:dropdownlist id="cboEmpType" runat="server" CssClass="combo" Width="100%"></asp:dropdownlist></TD>
				</TR>
				<TR style="DISPLAY: none">
					<TD><asp:label id="Label5" runat="server" CssClass="label" Width="100%">Leave Level</asp:label></TD>
					<TD colSpan="2"><asp:label id="lblFromLeaveLevel" runat="server" CssClass="labelData" Width="100%"></asp:label></TD>
					<TD></TD>
					<TD></TD>
					<TD colSpan="2"><asp:dropdownlist id="cboLSLeaveLevelID" runat="server" CssClass="combo" Width="100%"></asp:dropdownlist></TD>
				</TR>
			</TABLE>
			<INPUT id="txtLevel1ID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtLevel1ID"
				runat="server"> <INPUT id="txtLevel2ID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtLevel2ID"
				runat="server"> <INPUT id="txtLevel3ID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtLevel3ID"
				runat="server"> <INPUT id="txtWorkingRecordID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1"
				name="txtWorkingRecordID" runat="server"> <INPUT id="txtRankID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtRankID"
				runat="server"><INPUT id="txtManagerID" style="WIDTH: 10px; HEIGHT: 22px" type="hidden" size="1" runat="server"></TD>
	</TR>
	<TR>
		<TD align="center">
			<HR align="center" width="95%">
		</TD>
	</TR>
	<TR>
		<TD noWrap align="center">
			<TABLE id="tblButton" cellSpacing="0" cellPadding="0" width="99%" border="0">
				<TR vAlign="top" height="30">
					<TD vAlign="middle" width="15%"><asp:checkbox id="chkShowGrid" accessKey="G" onclick="javascript:collapse('_ctl0_trGrid');" runat="server"
							CssClass="checkbox" Text="Show grid" ToolTip="Alt+G" Checked="True"></asp:checkbox></TD>
					<TD align="center" width="*"><asp:linkbutton id="btnAddNew" accessKey="N" runat="server" CssClass="btnAddnew" ToolTip="Alt+N">Add New</asp:linkbutton>&nbsp;
						<asp:linkbutton id="btnSave" accessKey="S" runat="server" CssClass="btnSave" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp;
						<asp:linkbutton id="btnDelete" accessKey="D" runat="server" CssClass="btnExport" ToolTip="Alt + D">Delete</asp:linkbutton>&nbsp;
						<asp:linkbutton id="btnList" accessKey="L" runat="server" CssClass="btnList" ToolTip="Alt + L"> List</asp:linkbutton>&nbsp;
						<asp:linkbutton id="btnImport" accessKey="I" runat="server" CssClass="btnImport" ToolTip="Alt+I, Nh?p dl t? file Excel cùng d?nh d?ng v?i file export"
							Visible="False"> Import</asp:linkbutton>&nbsp;
						<asp:linkbutton id="btnExport" accessKey="E" runat="server" CssClass="btnExport" ToolTip="Alt+E, Xu?t dl trên lu?i ra file Excel"> Export</asp:linkbutton></TD>
				</TR>
			</TABLE>
		</TD>
	</TR> <!-- end button for input form -->
	<TR id="trGrid" runat="server">
		<TD align="center"><!-- start grid for input form -->
			<TABLE id="tblGrid" cellSpacing="0" cellPadding="1" width="98%" border="0"> <!--				<TR>
					<TD width="40%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
					<TD align="right" width="*">sasa</TD>
				</TR>-->
				<TR id="trGrid1"> <!-- start grid detail for input form -->
					<TD colSpan="2"><asp:datagrid id="grdWorkingRecord" runat="server" CssClass="grid" Width="100%" BackColor="White"
							BorderColor="#3366CC" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False" AllowSorting="True">
							<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="WorkingRecordID" HeaderText="Seq">
									<HeaderStyle Width="5%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Select">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox" Enabled="false"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="&lt;b&gt;#&lt;/b&gt;">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<%# Container.ItemIndex + 1%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:ButtonColumn DataTextField="FromDate" HeaderText="Effective Date" CommandName="EDIT"></asp:ButtonColumn>
								<asp:BoundColumn DataField="StatusChange" HeaderText="Status Change">
									<HeaderStyle Width="12.5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Division" HeaderText="Company">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Department" HeaderText="Department">
									<HeaderStyle Width="15%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Section" HeaderText="Group">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Position" HeaderText="Job title">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Grade" HeaderText="Grade">
									<HeaderStyle Width="10%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="LevelWR" HeaderText="Level">
									<HeaderStyle Width="10%"></HeaderStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE> <!-- end grid for input form --></TD>
	</TR>
</TABLE>
<SCRIPT language="javascript">
<!--
function validform(){	
	if(checkisnull('HR_EmpHeader_txtEmpID')==false)  return false;
	if(checkisnull('txtFromDate')==false)  return false;	
	if(checkisnull('cboLSStatusChangeID')==false)  return false;	
	if (document.getElementById('_ctl0_chkDecision').checked==true)
		{						
			if (checkisnull('txtDecisionNo')==false) return false;
			if (checkisnull('txtSignDate')==false) return false;
		}
	document.getElementById('<%= this.txtFromDate.ClientID%>').disabled = false;
	return true;
}
function checkdelete()
{
	if(GridCheck('_ctl0_grdWorkingRecord',2,1,'chkSelect')==false)
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
				//alert('Vui lòng di?n d?y d? d? li?u yêu c?u!');
				GetAlertError(iTotal,DSAlert,"0003");
				document.getElementById('_ctl0_' + obj).focus();
				return false;
			}
		else
			return true;
	}
//-->
</SCRIPT>
<%
	//string strLangID = Session["LangID"] == null?"VN":Session["LangID"].ToString().Trim();
	int Total5 = 0;
	//int Total6 = 0;
			
	DataTable rs5 = new DataTable();
	//DataTable rs6 = new DataTable();
	
	rs5 = clsCommon.GetDataTable("sp_GetDataComboRank @TableName = 'LS_tblRank'");
	//rs6 = clsCommon.GetDataTable("sp_GetDataCombo @TableName = 'LS_tblJobCode'");
	
	Total5 = rs5.Rows.Count;
	//Total6 = rs6.Rows.Count;		
%>
<SCRIPT language="javascript">					
		var DS5 = new Array(4);
		DS5[0] = new Array(<%=Total5%>);
		DS5[1] = new Array(<%=Total5%>);
		DS5[2] = new Array(<%=Total5%>);	
		DS5[3] = new Array(<%=Total5%>);	
		<% for(int i=0; i<Total5; i++) {%>		
			DS5[0][<%=i%>]="<%=rs5.Rows[i]["LSGradeID"]%>";
			DS5[1][<%=i%>]="<%=rs5.Rows[i]["LSRankID"]%>";			
			DS5[3][<%=i%>]="<%=rs5.Rows[i]["LSJobCodeID"]%>";
			<% if (strLangID=="EN") {%>
				DS5[2][<%=i%>]="<%=rs5.Rows[i]["Name"]%>";	
			<%} else{%>
				DS5[2][<%=i%>]="<%=rs5.Rows[i]["VNName"]%>";
			<%}			
			%>	
		<%}%>				
		
</SCRIPT>
<SCRIPT language="javascript">	
	function ChangeLevelGrade()
	{		
		var all = document.getElementById("_ctl0_cboRank").length;
		//var all1 = document.getElementById("_ctl0_cboJobCodeID").length;
			
		for(i=0; i<all; i++)
		{	
			document.getElementById("_ctl0_cboRank").remove(0);			
		};
		
		/*for(i=0; i<all1; i++)
		{	
			document.getElementById("_ctl0_cboJobCodeID").remove(0);			
		};
		*/
		var strLevelGradeID = document.getElementById("_ctl0_cboGrade").value; 
		
		document.getElementById("_ctl0_cboRank").add(new Option('',''));				
		for(i=0; i<<%=Total5%>;i++)
		{
			if (DS5[0][i]==strLevelGradeID)
			{
				document.getElementById("_ctl0_cboRank").add(new Option(DS5[2][i],DS5[1][i]));
			};
		};
		all = document.getElementById("_ctl0_cboRank").length;
		document.getElementById("_ctl0_cboRank").selectedIndex = 0;
		document.getElementById("_ctl0_lblJobCodeID").value ="";
		ChangeRank();
	}
	
	function ChangeRank()
	{
		var strRankID = document.getElementById("_ctl0_cboRank").value
		for(i=0; i<<%=Total5%>;i++)
		{
			if (DS5[1][i]==strRankID)
			{				
				document.getElementById("_ctl0_lblJobCodeID").value = DS5[3][i];
			};
		};
		document.getElementById("_ctl0_txtRankID").value = strRankID;	
	}
		
	function collapseDecision()
	{
		if (document.getElementById('_ctl0_chkDecision').checked==false)
		{		
			document.getElementById('_ctl0_trDecision1').style.display="none";
			document.getElementById('_ctl0_trDecision2').style.display="none";
			document.getElementById('_ctl0_trDecision3').style.display="none";
		}
		else
		{
			document.getElementById('_ctl0_trDecision1').style.display="block";
			document.getElementById('_ctl0_trDecision2').style.display="block";
			document.getElementById('_ctl0_trDecision3').style.display="block";
		}
	}
	function ReturnEmpPopUp3(strEmpID, strEmpCode, strEmpName)
	{
		document.getElementById("_ctl0_txtManger").value = strEmpName;
		document.getElementById("_ctl0_txtManagerID").value = strEmpID;
	}
	function OpenWindowManager()
	{
		ShowDialog('FormPage.aspx?Ascx=MdlHR/EmpPopup.ascx&Params=3');
	}
	function confirmDeleteFile()
	{
		if(confirm(GetAlertText(iTotal,DSAlert,"0006"))==false){
		return false;
		}
	}
	
	function EnableEffectiveDate()
	{
		if ('<%=Session["ssStatusWorking"]%>' == "Edit")
		{
			//document.getElementById('<%= this.txtFromDate.ClientID%>').disabled = true;
			document.getElementById("btnCalFromDate").style.display = "none";
		}
		else
		{
			//document.getElementById('<%= this.txtFromDate.ClientID%>').disabled = false;
			document.getElementById("btnCalFromDate").style.display = "";
		}
	}
	EnableEffectiveDate();
</SCRIPT>
