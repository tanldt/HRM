<%@ Register TagPrefix="uc1" TagName="EmpHeader" Src="../Include/EmpHeader.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Qualification.ascx.cs" Inherits="iHRPCore.MdlHR.Qualification" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Import Namespace ="System.Data.SqlClient" %>
<%@ Import Namespace ="System.Data" %>
<%@ Import Namespace ="iHRPCore.Com"%>
<%
	string strLanguageID = Session["LangID"] == null?"EN":Session["LangID"].ToString().Trim();
	string strTextField = strLanguage == "VN"?"VNName":"Name";	
	int TotalMajorLevel=0;
	int TotalSchool=0;
	int TotalFaculty=0;
	int TotalQualificationText=0;
	int TotalQualifiMajor=0;	
	
	DataTable rsMajorLevel = new DataTable();		
	DataTable rsSchool = new DataTable();	
	DataTable rsFaculty = new DataTable();
	DataTable rsQualificationText = new DataTable();
	DataTable rsQualifiMajor = new DataTable();
	
	
	rsMajorLevel = clsCommon.GetDataTable("sp_GetDataCombo @TableName='LS_tblMajorLevel',@Fields='LSMajorLevelID as [ID]," + strTextField + " as Name'");
	rsSchool = clsCommon.GetDataTable("sp_GetDataCombo @TableName='LS_tblSchool',@Fields='LSSchoolID as [ID]," + strTextField + " as Name'");
	rsFaculty = clsCommon.GetDataTable("sp_GetDataCombo @TableName='LS_tblFaculty',@Fields='LSFacultyID as [ID]," + strTextField + " as Name'");	
	rsQualificationText = clsCommon.GetDataTable("sp_GetDataCombo @TableName='LS_tblProfessionalLevel',@Fields='LSProfessionalLevelID as [ID]," + strTextField + " as Name'");	
	rsQualifiMajor = clsCommon.GetDataTable("sp_GetDataCombo @TableName='LS_tblQualifiMajor',@Fields='LSQualifiMajorID as [ID]," + strTextField + " as Name'");	
	
	
	TotalMajorLevel=rsMajorLevel.Rows.Count;
	TotalSchool=rsSchool.Rows.Count;
	TotalFaculty=rsFaculty.Rows.Count;
	TotalQualificationText=rsQualificationText.Rows.Count;
	TotalQualifiMajor=rsQualifiMajor.Rows.Count;
	
%>
<script language="javascript">
		//array for MajorLevel
		var DSMajorLevel = new Array(2);
		DSMajorLevel[0] = new Array(<%=TotalMajorLevel%>);
		DSMajorLevel[1] = new Array(<%=TotalMajorLevel%>);
		
		<% for(int i=0; i<TotalMajorLevel; i++) {%>					
			DSMajorLevel[0][<%=i%>]="<%=rsMajorLevel.Rows[i]["ID"]%>";
			DSMajorLevel[1][<%=i%>]="<%=rsMajorLevel.Rows[i]["Name"]%>";	
		<%}%>
		//array for School
		var DSSchool = new Array(2);
		DSSchool[0] = new Array(<%=TotalSchool%>);
		DSSchool[1] = new Array(<%=TotalSchool%>);
		
		<% for(int i=0; i<TotalSchool; i++) {%>		
			DSSchool[0][<%=i%>]="<%=rsSchool.Rows[i]["ID"]%>";			
			DSSchool[1][<%=i%>]="<%=rsSchool.Rows[i]["Name"]%>";	
		<%}%>	
		//vuonglm: array for QualifiMajor
		var DSQualifiMajor = new Array(2);
		DSQualifiMajor[0] = new Array(<%=TotalQualifiMajor%>);
		DSQualifiMajor[1] = new Array(<%=TotalQualifiMajor%>);
		
		<% for(int i=0; i<TotalQualifiMajor; i++) {%>		
			DSQualifiMajor[0][<%=i%>]="<%=rsQualifiMajor.Rows[i]["ID"]%>";			
			DSQualifiMajor[1][<%=i%>]="<%=rsQualifiMajor.Rows[i]["Name"]%>";	
		<%}%>	
		//vuonglm: array for QualificationText
		var DSQualificationText = new Array(2);
		DSQualificationText[0] = new Array(<%=TotalQualificationText%>);
		DSQualificationText[1] = new Array(<%=TotalQualificationText%>);
		
		<% for(int i=0; i<TotalQualificationText; i++) {%>		
			DSQualificationText[0][<%=i%>]="<%=rsQualificationText.Rows[i]["ID"]%>";			
			DSQualificationText[1][<%=i%>]="<%=rsQualificationText.Rows[i]["Name"]%>";	
		<%}%>	
		//array for Faculty
		var DSFaculty = new Array(2);
		DSFaculty[0] = new Array(<%=TotalFaculty%>);
		DSFaculty[1] = new Array(<%=TotalFaculty%>);
		
		<% for(int i=0; i<TotalFaculty; i++) {%>		
			DSFaculty[0][<%=i%>]="<%=rsFaculty.Rows[i]["ID"]%>";			
			DSFaculty[1][<%=i%>]="<%=rsFaculty.Rows[i]["Name"]%>";	
		<%}%>	
</script>
<script language="javascript">
	function ChangeSchool()
	{
		var strSchoolID = document.getElementById("_ctl0_cboLSSchoolID").value;
		if(strSchoolID=="")
		{
			document.getElementById("_ctl0_txtOtherSchool").value="";
			document.getElementById("_ctl0_txtOtherSchool").disabled=false;
			return;
		}
		for (i=0;i<<%=TotalSchool%>;i++)	
		{
			if (DSSchool[0][i]==strSchoolID)
			{
				document.getElementById("_ctl0_txtOtherSchool").value=DSSchool[1][i];
				document.getElementById("_ctl0_txtOtherSchool").disabled=true;
			}
		}
	}
	function ChangeMajorLevel()
	{
		var strmajorLevelID = document.getElementById("_ctl0_cboLSMajorLevelID").value;
		if(strmajorLevelID=="")
		{
			document.getElementById("_ctl0_txtOtherMajorLevel").value="";
			document.getElementById("_ctl0_txtOtherMajorLevel").disabled=false;
			return;
		}
		for (i=0;i<<%=TotalMajorLevel%>;i++)	
		{
			if (DSMajorLevel[0][i]==strmajorLevelID)
			{
				document.getElementById("_ctl0_txtOtherMajorLevel").value=DSMajorLevel[1][i];
				document.getElementById("_ctl0_txtOtherMajorLevel").disabled=true;
			}
		}
	}
	//vuonglm
	function ChangeQualifiMajor()
	{
		var strQualifiMajor = document.getElementById("_ctl0_cboDegree").value;
		if(strQualifiMajor=="")
		{
			document.getElementById("_ctl0_txtDegreeText").value="";
			document.getElementById("_ctl0_txtDegreeText").disabled=false;
			return;
		}
		for (i=0;i<<%=TotalQualifiMajor%>;i++)	
		{
			if (DSQualifiMajor[0][i]==strQualifiMajor)
			{
				document.getElementById("_ctl0_txtDegreeText").value=DSQualifiMajor[1][i];
				document.getElementById("_ctl0_txtDegreeText").disabled=true;
			}
		}
	}
	function ChangeQualification()
	{
		var strQualificationText = document.getElementById("_ctl0_cboLSProfessionalLevelID").value;
		if(strQualificationText=="")
		{
			document.getElementById("_ctl0_txtQualificationText").value="";
			document.getElementById("_ctl0_txtQualificationText").disabled=false;
			return;
		}
		for (i=0;i<<%=TotalQualificationText%>;i++)	
		{
			if (DSQualificationText[0][i]==strQualificationText)
			{
				document.getElementById("_ctl0_txtQualificationText").value=DSQualificationText[1][i];
				document.getElementById("_ctl0_txtQualificationText").disabled=true;
			}
		}
	}
	//vuonglm
	function ChangeFaculty()
	{
		var strFacultyID = document.getElementById("_ctl0_cboLSFacultyID").value;
		if(strFacultyID=="")
		{
			document.getElementById("_ctl0_txtOtherFaculty").value="";
			document.getElementById("_ctl0_txtOtherFaculty").disabled=false;
			return;
		}
		for (i=0;i<<%=TotalFaculty%>;i++)	
		{
			if (DSFaculty[0][i]==strFacultyID)
			{
				document.getElementById("_ctl0_txtOtherFaculty").value=DSFaculty[1][i];
				document.getElementById("_ctl0_txtOtherFaculty").disabled=true;
			}
		}
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
					<TD width="17%"><asp:label id="lblCertificateNo" runat="server" CssClass="labelRequire" Width="100%">Certificate Code</asp:label></TD>
					<TD width="30%"><asp:textbox id="txtCertificateCode" runat="server" CssClass="input" Width="80%" MaxLength="30"></asp:textbox></TD>
					<TD width="4%"></TD>
					<TD width="17%"><asp:label id="lblCertificateName" runat="server" CssClass="labelRequire" Width="100%">Certificate Name</asp:label></TD>
					<TD width="35%"><asp:textbox id="txtCertificateName" runat="server" CssClass="input" Width="100%" MaxLength="150"></asp:textbox></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblCertificateDate" runat="server" CssClass="label" Width="100%">Certificate Date</asp:label></TD>
					<TD><asp:textbox id="txtCertificateDate" onblur="JavaScript:CheckDate(this)" runat="server" CssClass="input"
							Width="80%" MaxLength="10"></asp:textbox>&nbsp;<INPUT class=btnCal onclick="javascript:popUpCalendar(<%= this.txtCertificateDate.ClientID %>);" type=button></TD>
					<TD></TD>
					<TD><asp:label id="lblIssuedPlace" runat="server" CssClass="label" Width="100%">Issued Place</asp:label></TD>
					<TD><asp:textbox id="txtIssuedPlace" runat="server" CssClass="input" Width="100%" MaxLength="100"></asp:textbox></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblFromMonth" runat="server" CssClass="label" Width="100%">From month</asp:label></TD>
					<TD><asp:textbox id="txtFromDate" onblur="javascript:CheckMonthYear(this)" runat="server" CssClass="input"
							Width="80%" MaxLength="10"></asp:textbox></TD>
					<TD></TD>
					<TD><asp:label id="lblToMonth" runat="server" CssClass="label" Width="100%">To month</asp:label></TD>
					<TD><asp:textbox id="txtToDate" onblur="javascript:CheckMonthYear(this)" runat="server" CssClass="input"
							Width="80%" MaxLength="10"></asp:textbox></TD>
				</TR>
				<TR id="trAdSchool" runat="server">
					<TD><asp:checkbox id="chkAdSchool" runat="server" CssClass="chkAdmin" Checked="True"></asp:checkbox><asp:label id="lblSchool" runat="server" CssClass="label" Width="70%">School</asp:label></TD>
					<TD noWrap><asp:dropdownlist id="cboLSSchoolID" runat="server" CssClass="select" Width="100%" onchange="ChangeSchool()"></asp:dropdownlist></TD>
					<TD vAlign="top" colSpan="3"><asp:textbox id="txtOtherSchool" style="POSITION: absolute" runat="server" CssClass="input" Width="100%"
							MaxLength="100"></asp:textbox></TD>
				</TR>
				<TR id="trDegree" runat="server">
					<TD><asp:label id="Label1" runat="server" CssClass="label" Width="70%">Degree</asp:label></TD>
					<TD noWrap><asp:dropdownlist id="cboDegree" runat="server" CssClass="select" Width="100%" onchange="ChangeQualifiMajor()"></asp:dropdownlist></TD>
					<TD vAlign="top" colSpan="3"><asp:textbox id="txtDegreeText" style="POSITION: absolute" runat="server" CssClass="input" Width="100%"
							MaxLength="100"></asp:textbox></TD>
				</TR>
				<TR runat="server">
					<TD style="HEIGHT: 24px"><asp:label id="Label4" runat="server" CssClass="label" Width="70%">Qualification</asp:label></TD>
					<TD style="HEIGHT: 24px" noWrap><asp:dropdownlist id="cboLSProfessionalLevelID" runat="server" CssClass="select" Width="100%" onchange="ChangeQualification()"></asp:dropdownlist></TD>
					<TD style="HEIGHT: 24px" vAlign="top" colSpan="3"><asp:textbox id="txtQualification" runat="server" CssClass="input" Width="100%" MaxLength="100"></asp:textbox></TD>
				</TR>
				<TR id="trAdFaculty" runat="server">
					<TD><asp:checkbox id="chkAdFaculty" runat="server" CssClass="chkAdmin" Checked="True"></asp:checkbox><asp:label id="Label5" runat="server" CssClass="label" Width="70%">Faculty</asp:label></TD>
					<TD noWrap><asp:dropdownlist id="cboLSFacultyID" runat="server" CssClass="select" Width="100%" onchange="ChangeFaculty()"></asp:dropdownlist></TD>
					<TD vAlign="top" colSpan="3"><asp:textbox id="txtOtherFaculty" style="POSITION: absolute" runat="server" CssClass="input"
							Width="100%" MaxLength="100"></asp:textbox></TD>
				</TR>
				<TR id="trAdMajorLevel" runat="server">
					<TD><asp:checkbox id="chkAdMajorLevel" runat="server" CssClass="chkAdmin" Checked="True"></asp:checkbox><asp:label id="Label6" runat="server" CssClass="label" Width="70%">Major Level</asp:label></TD>
					<TD noWrap><asp:dropdownlist id="cboLSMajorLevelID" runat="server" CssClass="select" Width="100%" onchange="ChangeMajorLevel()"></asp:dropdownlist></TD>
					<TD vAlign="top" colSpan="3"><asp:textbox id="txtOtherMajorLevel" style="POSITION: absolute" runat="server" CssClass="input"
							Width="100%" MaxLength="100"></asp:textbox></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblTrainingForm" runat="server" CssClass="label" Width="100%">Form of training</asp:label></TD>
					<TD noWrap><asp:dropdownlist id="cboLSTrainingFormID" runat="server" CssClass="select" Width="80%"></asp:dropdownlist></TD>
					<TD></TD>
					<TD></TD>
					<TD noWrap></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD vAlign="top" colSpan="4"><asp:checkbox id="chkisCompany" runat="server" CssClass="Checkbox" Text="Company pay?"></asp:checkbox></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 21px"><asp:label id="Label2" runat="server" CssClass="label" Width="100%">Training Place</asp:label></TD>
					<TD style="HEIGHT: 21px" vAlign="top" colSpan="4"><asp:radiobuttonlist id="optTrainPlace" onclick="ChangeTrainingPlace()" runat="server" Width="50%" RepeatDirection="Horizontal">
							<asp:ListItem Value="0" Selected="True">Local</asp:ListItem>
							<asp:ListItem Value="1">Overseas</asp:ListItem>
						</asp:radiobuttonlist></TD>
				</TR>
				<TR id="trLocal" runat="server">
					<TD style="HEIGHT: 21px"></TD>
					<TD style="HEIGHT: 21px" vAlign="top" colSpan="4"><asp:radiobuttonlist id="optionTrainingPlace_" onclick="ChangeTrainingPlace_()" runat="server" Width="50%"
							RepeatDirection="Horizontal">
							<asp:ListItem Value="0">Inhouse</asp:ListItem>
							<asp:ListItem Value="1">Mobile</asp:ListItem>
						</asp:radiobuttonlist></TD>
				</TR>
				<TR id="trOverseas" runat="server">
					<TD></TD>
					<TD vAlign="top" colSpan="4">
						<table width="100%">
							<tr>
								<td width="20%"><asp:label id="Label3" runat="server" CssClass="label" Width="100%">Nation</asp:label></td>
								<td width="25%"><asp:dropdownlist id="cboLSNationalityID" runat="server" CssClass="select" Width="95%"></asp:dropdownlist></td>
								<td width="20%"></td>
								<td></td>
							</tr>
						</table>
					</TD>
				</TR>
				<TR>
					<TD><asp:label id="lblNote" runat="server" CssClass="label" Width="100%">Note</asp:label></TD>
					<TD vAlign="top" colSpan="4"><asp:textbox id="txtNote" runat="server" CssClass="input" Width="100%" MaxLength="255"></asp:textbox></TD>
				</TR>
			</TABLE> <!-- end button for input form --><INPUT id="txtQualificationID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1"
				name="txtQualificationID" runat="server"> <INPUT id="txtTrainingPlace_" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtTrainingPlace"
				runat="server">
			<asp:textbox id="txtTrainingPlace" runat="server" Width="9px" BackColor="#F5F7F6" BorderStyle="None"
				ForeColor="#F5F7F6"></asp:textbox></TD>
	</TR>
	<TR>
		<TD vAlign="top" noWrap align="center" height="20">
			<HR align="center" width="100%">
		</TD>
	</TR>
	<TR>
		<TD noWrap align="center"><asp:linkbutton id="btnAddNew" accessKey="N" runat="server" CssClass="btnAddnew" ToolTip="Alt+N">Add New</asp:linkbutton>&nbsp;&nbsp;
			<asp:linkbutton id="btnSave" accessKey="S" runat="server" CssClass="btnSave" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp;&nbsp;
			<asp:linkbutton id="btnDelete" accessKey="D" runat="server" CssClass="btnDelete" ToolTip="Alt + D">Delete</asp:linkbutton>&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:linkbutton id="btnList" accessKey="L" runat="server" CssClass="btnList" ToolTip="Alt + L">List</asp:linkbutton>&nbsp;&nbsp;&nbsp;
			<asp:linkbutton id="btnExport" accessKey="E" runat="server" CssClass="btnExport" ToolTip="Alt+E, Xu?t dl trên lu?i ra file Excel"> Export</asp:linkbutton></TD>
		</TD></TR>
	<TR>
		<TD noWrap align="center" height="10"></TD>
	</TR>
	<TR id="trGrid" runat="server">
		<TD align="center"><!-- start grid for input form -->
			<TABLE id="tblGrid" cellSpacing="0" cellPadding="1" width="98%" border="0">
				<TR id="trGrid1"> <!-- start grid detail for input form -->
					<TD colSpan="2"><asp:datagrid id="dtgList" runat="server" CssClass="grid" Width="100%" BackColor="White" AllowSorting="True"
							AutoGenerateColumns="False" CellPadding="0" BorderWidth="1px" BorderColor="#3366CC" AllowPaging="True">
							<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="QualificationID" HeaderText="Seq">
									<HeaderStyle HorizontalAlign="Center" Width="5%" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Select">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="&lt;b&gt;#&lt;/b&gt;">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<%# Container.ItemIndex + 1%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:ButtonColumn DataTextField="CertificateCode" HeaderText="Certificate code" CommandName="EDIT"></asp:ButtonColumn>
								<asp:BoundColumn DataField="CertificateDate" HeaderText="Certificate Date">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CertificateName" HeaderText="Certificate Name">
									<HeaderStyle Width="20%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ProfessionalLevel" HeaderText="Qualification">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MajorLevel" HeaderText="Major Level">
									<HeaderStyle Width="25%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>
<script language="javascript">

function validform(){
	if(checkisnull('HR_EmpHeader_txtEmpID')==false)  return false;
	if(checkisnull('txtCertificateCode')==false)  return false;
	if(checkisnull('txtCertificateName')==false)  return false;
/*	if(document.getElementById('_ctl0_txtCertificateDate').value!="")
	{
		if(FromSmallNow(document.getElementById('_ctl0_txtCertificateDate')) == false)
		{
			alert('Certificate date must be less than now');
			document.getElementById('_ctl0_txtCertificateDate').focus();
			return false;
		}		
	}*/
	if(document.getElementById('_ctl0_txtFromDate').value!="" ||document.getElementById('_ctl0_txtToDate').value!="")
	{		
		if(FromSmallNowMonth(document.getElementById('_ctl0_txtToDate')) == false)
		{
			//Th?i gian t? ph?i nh? hon th?i gian hi?n t?i
			GetAlertError(iTotal,DSAlert,"0008");
			document.getElementById('_ctl0_txtToDate').focus();
			return false;
		}		
		if(FromSmallToMonth(document.getElementById('_ctl0_txtFromDate'),document.getElementById('_ctl0_txtToDate')) == false)
		{
			//alert('Th?i gian b?t d?u ph?i tru?c th?i gian k?t thúc');
			GetAlertError(iTotal,DSAlert,"0007");
			document.getElementById('_ctl0_txtToDate').focus();
			return false;
		}
	}
	//if(checkisnull('cboMajorLevelID')==false)  return false;		
	//if(checkisnull('cboMajorID')==false)  return false;		
	//alert(document.getElementById("_ctl0_txtTrainingPlace").value);
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
	
function ChangeTrainingPlace()
{
	if (document.getElementById("_ctl0_optTrainPlace_0").checked == true)
	{
		document.getElementById("_ctl0_trLocal").style.display = "";
		document.getElementById("_ctl0_trOverseas").style.display = "none";
		ChangeTrainingPlace_();
		
		
	}
	else
	{
		document.getElementById("_ctl0_trLocal").style.display = "none";
		document.getElementById("_ctl0_trOverseas").style.display = "";
		document.getElementById("_ctl0_txtTrainingPlace").value="0";		
		
		
	}	
}
function ChangeTrainingPlace_()
	{
		if (document.getElementById("_ctl0_optionTrainingPlace__0").checked == true)
		{
			document.getElementById("_ctl0_txtTrainingPlace").value="1";
		}else
		{
			document.getElementById("_ctl0_txtTrainingPlace").value="2";
		}	
	}
</script>
