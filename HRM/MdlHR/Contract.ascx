<%@ Import Namespace ="System.Data.SqlClient" %>
<%@ Import Namespace ="System.Data" %>
<%@ Import Namespace ="iHRPCore.Com"%>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Contract.ascx.cs" Inherits="iHRPCore.MdlHR.Contract" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc" TagName="EmpHeader" Src="../Include/EmpHeader.ascx" %>
<%
	string strLangID = Session["LangID"] == null?"EN":Session["LangID"].ToString().Trim();
	int Total1 = 0;
	DataTable rs1 = new DataTable();
	rs1 = clsCommon.GetDataTable("sp_GetDataCombo @TableName = 'LS_tblContractType'");
	Total1 = rs1.Rows.Count;
%>
<SCRIPT language="javascript">					
		var DS1 = new Array(2);
		DS1[0] = new Array(<%=Total1%>);
		DS1[1] = new Array(<%=Total1%>);
		<% for(int i=0; i<Total1; i++) {%>		
			DS1[0][<%=i%>]="<%=rs1.Rows[i]["LSContractTypeID"]%>";
			DS1[1][<%=i%>]="<%=rs1.Rows[i]["Duration"]%>";						
		<%}%>		
</SCRIPT>
<script language="javascript">
function ChangeContract()
	{	
	
		var strType = document.getElementById("_ctl0_cboLSContractTypeID").value; 
			
		for(i=0; i<<%=Total1%>;i++)
		{
			if (DS1[0][i]==strType)
			{
				document.getElementById("_ctl0_txtDuration").value=DS1[1][i];
				CalIncreaseSal(DS1[1][i]);
			
			};
		};	
		//		
	}
	function CalIncreaseSal()	
	{
		var value,count;
		value=document.getElementById("_ctl0_txtEffectiveDate").value; 
		count=document.getElementById("_ctl0_txtDuration").value; 
		
		if (value !='' && count!='')
		{			
			document.getElementById("_ctl0_txtToDate").value=AddMonthstoDate(value,count);			
			value = document.getElementById("_ctl0_txtToDate").value;
			document.getElementById("_ctl0_txtToDate").value=AddDaystoDate_(value,-1);
		}
		else if (count!='')
		{
			document.getElementById("_ctl0_txtToDate").value=value;
		}
		else
		{
			document.getElementById("_ctl0_txtToDate").value='';
		}			
	}
	function CheckDate_(field)
	{		
		CheckDate(field);
		CalIncreaseSal();			
		if (document.getElementById("_ctl0_txtToDate").value.length<10 && document.getElementById("_ctl0_txtToDate").value.length>2)
				document.getElementById("_ctl0_txtToDate").value='0' + document.getElementById("_ctl0_txtToDate").value
		
		
	}
		function ContractDate(field)
	{		
		CheckDate(field);
		CalIncreaseSal();
/*		document.getElementById("_ctl0_txtProbationFrom").value=document.getElementById("_ctl0_txtEffectiveDate").value;
		if (document.getElementById("_ctl0_txtProbationFrom").value!="")
		document.getElementById("_ctl0_txtProbationTo").value=AddMonthstoDate(document.getElementById("_ctl0_txtProbationFrom").value,2)	

		if (document.getElementById("_ctl0_txtProbationFrom").value!="" && document.getElementById("_ctl0_txtProbationTo").value!="")
			document.getElementById("_ctl0_txtNumOfDays").value = DeltaDate1Date2(document.getElementById("_ctl0_txtProbationFrom"), document.getElementById("_ctl0_txtProbationTo"));
*/	}

	function CalNumDay(field)
	{		
		CheckDate(field);

		//if (document.getElementById("_ctl0_txtProbationFrom").value!="" && document.getElementById("_ctl0_txtProbationTo").value!="")
			//document.getElementById("_ctl0_txtNumOfDays").value = DeltaDate1Date2(document.getElementById("_ctl0_txtProbationFrom"), document.getElementById("_ctl0_txtProbationTo"));
		if (document.getElementById("_ctl0_txtProbationFrom").value!="" && document.getElementById("_ctl0_txtNumOfDays").value!="")
			document.getElementById("_ctl0_txtProbationTo").value = AddDaystoDateFull(document.getElementById("_ctl0_txtProbationFrom").value, document.getElementById("_ctl0_txtNumOfDays").value)
	}
	
	function ProbationDate(field)
	{		
		var value = document.getElementById("_ctl0_txtNumOfDays").value;
		//var month;
		//month = value/30;

		checkNumeric(field);

		if (value!="")
		{
			document.getElementById("_ctl0_txtProbationFrom").value=document.getElementById("_ctl0_txtEffectiveDate").value;
			document.getElementById("_ctl0_txtProbationTo").value = AddDaystoDateFull(document.getElementById("_ctl0_txtProbationFrom"), value);
		}
	}

	function EndProbationDate(field)
	{		
		var value = document.getElementById("_ctl0_txtNumOfDays").value;
		//var month;
		//month = value/30;

		CheckDate(field);

		if (value!="")
		{
			//document.getElementById("_ctl0_txtProbationFrom").value=document.getElementById("_ctl0_txtEffectiveDate").value;
			document.getElementById("_ctl0_txtProbationTo").value = AddDaystoDateFull(document.getElementById("_ctl0_txtProbationFrom"), value);
		}
	}
	
</script>
<table width="100%">
	<tr>
		<td>
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="99%" border="0">
				<TR>
					<TD colSpan="11"><asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD align="center" colSpan="11"><uc:empheader id="HR_EmpHeader" runat="server"></uc:empheader></TD>
				</TR>
				<tr>
					<TD width="20%"><asp:label id="lblContractType" CssClass="labelrequire" runat="server" Width="100%"> Type</asp:label></TD>
					<td width="1%"></td>
					<td width="20%" colSpan="5"><asp:dropdownlist id="cboLSContractTypeID" CssClass="combo" runat="server" Width="250px" onchange="javascript:ChangeContract();"></asp:dropdownlist></td>
					<td width="1%"></td>
					<TD width="10%"><asp:label id="lblContractNo" CssClass="labelrequire" runat="server" Width="100%"> No.</asp:label></TD>
					<td width="1%"></td>
					<TD width="20%"><asp:textbox id="txtContractNo" CssClass="input" runat="server" Width="127px" MaxLength="10"></asp:textbox></TD>
				</tr>
				<tr>
					<TD style="HEIGHT: 24px"><asp:label id="lblEffDate" CssClass="labelrequire" runat="server" Width="100%">Effective Date</asp:label></TD>
					<td style="HEIGHT: 24px"></td>
					<TD style="HEIGHT: 24px"><asp:textbox id="txtEffectiveDate" onblur="JavaScript:CheckDate_(this)" CssClass="input" runat="server"
							Width="80px" MaxLength="10"></asp:textbox>&nbsp;<INPUT class=btnCal onclick="javascript:popUpCalendar(<%=this.txtEffectiveDate.ClientID%>);" type=button></TD>
					<TD style="HEIGHT: 24px" width="1%"></TD>
					<TD style="HEIGHT: 24px" width="10%"><asp:label id="lblEndDate" CssClass="label" runat="server" Width="100%">End Date</asp:label></TD>
					<td style="HEIGHT: 24px"></td>
					<TD style="HEIGHT: 24px"><asp:textbox id="txtToDate" onblur="JavaScript:CheckDate(this)" CssClass="input" runat="server"
							Width="80px" MaxLength="10"></asp:textbox>&nbsp;<INPUT class=btnCal onclick="javascript:popUpCalendar(<%=this.txtToDate.ClientID%>);" type=button></TD>
					<td style="HEIGHT: 24px"></td>
					<td style="HEIGHT: 24px"></td>
					<td style="HEIGHT: 24px"></td>
					<td style="HEIGHT: 24px"><asp:textbox id="txtContractID" CssClass="input" runat="server" Width="0px" MaxLength="10"></asp:textbox></td>
				</tr>
				<TR id="trAdProbationFrom" runat="server">
					<TD><asp:checkbox id="chkAdProbationFrom" CssClass="chkAdmin" Runat="server"></asp:checkbox><asp:label id="lblProbationFrom" CssClass="label" runat="server" Width="80%">Probation from</asp:label></TD>
					<TD></TD>
					<TD><asp:textbox id="txtProbationFrom" onblur="JavaScript:CheckDate(this)" CssClass="input" runat="server"
							Width="80px" MaxLength="10"></asp:textbox>&nbsp;<INPUT class=btnCal onclick="javascript:popUpCalendar(<%=this.txtProbationFrom.ClientID%>);" type=button></TD>
					<TD></TD>
					<TD><asp:label id="lblProbationto" CssClass="label" runat="server" Width="100%"> to</asp:label></TD>
					<TD></TD>
					<TD><asp:textbox id="txtProbationTo" onblur="JavaScript:CheckDate(this)" CssClass="input" runat="server"
							Width="80px" MaxLength="10"></asp:textbox>&nbsp;<INPUT class=btnCal onclick="javascript:popUpCalendar(<%=this.txtProbationTo.ClientID%>);" type=button></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD><asp:textbox id="txtAttachFile" CssClass="input" runat="server" Width="0px"></asp:textbox></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblAttachFile" CssClass="label" runat="server" Width="100%">File Attach</asp:label></TD>
					<TD></TD>
					<TD colSpan="5"><INPUT class="input" id="txtFile" style="WIDTH: 250px" tabIndex="1" type="file" size="13"
							name="File1" runat="server"></TD>
					<TD></TD>
					<TD colSpan="3"><asp:hyperlink id="hpSelectFile" CssClass="LinkLeft" runat="server" Width="70px"></asp:hyperlink><asp:linkbutton id="btnDeleteFile" CssClass="DeleteFile" runat="server" Width="20px" Visible="False"
							ToolTip="Delete file"></asp:linkbutton></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblSignDate" CssClass="label" runat="server" Width="100%">Sign Date</asp:label></TD>
					<td width="2%"></td>
					<TD style="WIDTH: 198px" width="15%"><asp:textbox id="txtSignDate" onblur="JavaScript:CheckDate(this)" CssClass="input" runat="server"
							Width="80px" MaxLength="10"></asp:textbox>&nbsp;<INPUT class=btnCal onclick="javascript:popUpCalendar(<%=this.txtSignDate.ClientID%>);" type=button></TD>
					<TD width="1%"></TD>
					<TD><asp:label id="lblSigner" CssClass="label" runat="server" Width="100%">Signer</asp:label></TD>
					<td></td>
					<TD width="15%"><asp:textbox id="txtSigner" CssClass="input" runat="server" Width="100px" MaxLength="50"></asp:textbox></TD>
					<td></td>
					<TD width="5%"><asp:label id="lblPosSigner" CssClass="label" runat="server" Width="100%"> Signer PT</asp:label></TD>
					<td width="1%"></td>
					<TD width="15%"><asp:textbox id="txtSignerPosition" CssClass="input" runat="server" Width="127px" MaxLength="50"></asp:textbox></TD>
				</TR>
				<TR style="DISPLAY: none">
					<TD height="15"><asp:label id="lblPositonlbl" CssClass="label" runat="server" Width="100%">Position</asp:label></TD>
					<TD width="2%" height="15"></TD>
					<TD style="WIDTH: 198px" width="15%" height="15"><asp:label id="lblPosition" CssClass="labelData" runat="server" Width="100px"></asp:label></TD>
					<TD width="1%" height="15"></TD>
					<TD height="15"><asp:label id="lblJobTitlelbl" CssClass="label" runat="server" Width="100%">Job Title</asp:label></TD>
					<TD width="1%" height="15"></TD>
					<TD width="15%" height="15"><asp:label id="lblJobTitle" CssClass="labelData" runat="server" Width="100px"></asp:label></TD>
					<TD height="15"></TD>
					<TD width="5%" height="15"><asp:label id="lblLocationlbl" CssClass="label" runat="server" Width="100%">Location</asp:label></TD>
					<TD width="1%" height="15"></TD>
					<TD width="15%" height="15"><asp:label id="lblLocation" CssClass="labelData" runat="server" Width="100px"></asp:label></TD>
				</TR>
				<tr style="DISPLAY: none">
					<TD><asp:label id="Label9" CssClass="label" runat="server" Width="100%">Number of AL</asp:label></TD>
					<td width="2%"></td>
					<TD width="15%"><asp:textbox id="Textbox7" CssClass="input" runat="server" Width="100px" MaxLength="10"></asp:textbox></TD>
					<TD width="1%"></TD>
					<TD><asp:label id="Label10" CssClass="label" runat="server" Width="100%">Contract Sal</asp:label></TD>
					<td width="1%"></td>
					<TD width="15%"><asp:textbox id="Textbox8" CssClass="input" runat="server" Width="100px" MaxLength="10"></asp:textbox></TD>
					<td width="35%" colSpan="4"></td>
				</tr>
				<TR>
					<TD><asp:label id="Label1" CssClass="label" runat="server" Width="100%"> Description</asp:label></TD>
					<TD width="2%"></TD>
					<TD width="15%" colSpan="9"><asp:textbox id="txtNote" CssClass="input" runat="server" Width="510px" MaxLength="200"></asp:textbox></TD>
				</TR>
				<TR style="DISPLAY: none">
					<TD><asp:label id="lblSalarylbl" CssClass="label" runat="server" Width="100%">Salary </asp:label></TD>
					<TD width="2%"></TD>
					<TD width="15%" colSpan="9"><asp:textbox id="txtContractSalary" onblur="JavaScript:checkNumeric(this,1000000000)" style="TEXT-ALIGN: right"
							CssClass="input" runat="server" Width="100px"></asp:textbox></TD>
				</TR>
				<TR style="DISPLAY:none">
					<TD><asp:label id="Label2" CssClass="label" runat="server" Width="100%">Overhead</asp:label></TD>
					<TD style="HEIGHT: 5px" width="2%"></TD>
					<TD style="HEIGHT: 5px" width="15%"><asp:textbox id="txtOverHead" onblur="JavaScript:checkNumeric(this,1000000000)" style="TEXT-ALIGN: right"
							CssClass="input" runat="server" Width="100px" MaxLength="10"></asp:textbox></TD>
					<TD width="1%"></TD>
					<TD style="HEIGHT: 5px"><asp:label id="Label3" CssClass="label" runat="server" Width="100%">Fringe benefit-Cus.Group</asp:label></TD>
					<TD width="1%"></TD>
					<TD style="HEIGHT: 5px" width="15%"><asp:textbox id="txtFringeBenefit" style="TEXT-ALIGN: right" onblur="JavaScript:checkNumeric(this,1000000000)"
							CssClass="input" runat="server" Width="100px" MaxLength="10"></asp:textbox></TD>
					<TD height="15"></TD>
					<TD width="5%" height="15"><asp:label id="Label4" CssClass="label" runat="server" Width="100%">Housing benefit</asp:label></TD>
					<TD width="1%" height="15"></TD>
					<TD width="15%" height="15"><asp:textbox id="txtHousingBenefit" style="TEXT-ALIGN: right" onblur="JavaScript:checkNumeric(this,1000000000)"
							CssClass="input" runat="server" Width="100px" MaxLength="10"></asp:textbox></TD>
				</TR>
				<TR style="DISPLAY:none">
					<TD><asp:label id="Label5" CssClass="label" runat="server" Width="100%">Supplier</asp:label></TD>
					<TD style="HEIGHT: 5px" width="2%"></TD>
					<TD style="HEIGHT: 5px" width="15%"><asp:dropdownlist id="cboLSSupplierID" CssClass="combo" runat="server" Width="100%"></asp:dropdownlist></TD>
					<TD width="1%"></TD>
					<TD style="HEIGHT: 5px"><asp:label id="Label6" CssClass="label" runat="server" Width="100%">Currency</asp:label></TD>
					<TD width="1%"></TD>
					<TD style="HEIGHT: 5px" width="15%"><asp:dropdownlist id="cboLSCurrencyTypeID" CssClass="combo" runat="server" Width="100%"></asp:dropdownlist></TD>
					<TD height="15"></TD>
					<TD width="5%" height="15"></TD>
					<TD width="1%" height="15"></TD>
					<TD width="15%" height="15"><asp:checkbox id="chkPaySalary" accessKey="G" CssClass="checkbox" runat="server" ToolTip="Alt+G"
							Checked="True" Text="Pay on salary?"></asp:checkbox></TD>
				</TR>
				<TR>
					<TD>&nbsp;</TD>
					<TD style="HEIGHT: 5px" width="2%"></TD>
					<TD style="HEIGHT: 5px" width="15%"></TD>
					<TD width="1%"></TD>
					<TD style="HEIGHT: 5px"></TD>
					<TD width="1%"></TD>
					<TD style="HEIGHT: 5px" width="15%"></TD>
					<TD style="HEIGHT: 5px" width="35%" colSpan="4"></TD>
				</TR>
				<TR>
					<TD width="13%" colSpan="11">
						<TABLE id="Table3" style="DISPLAY: none" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD width="17%"><asp:label id="llblGradeSal" CssClass="label" runat="server" Width="100%">Grade of Sal</asp:label></TD>
								<TD width="30%"><asp:label id="lblGradeOfSalData" CssClass="labelData" runat="server" Width="100%"></asp:label></TD>
								<TD width="4%"></TD>
								<TD width="17%"></TD>
								<TD width="35%"></TD>
							</TR>
							<TR>
								<TD><asp:label id="lblRankSal" CssClass="label" runat="server" Width="100%">Rank of Sal </asp:label></TD>
								<TD><asp:label id="lblRankOfSalData" CssClass="labelData" runat="server" Width="100%"></asp:label></TD>
								<TD></TD>
								<TD><asp:label id="lblSalCoef" CssClass="label" runat="server" Width="100%">Salary Coef</asp:label></TD>
								<TD><asp:label id="lblSalaryCoefData" CssClass="labelData" runat="server" Width="100%"></asp:label></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD><asp:textbox id="txtDuration" runat="server"></asp:textbox></TD>
								<TD></TD>
								<TD><asp:label id="lblSalaryOther" CssClass="label" runat="server" Width="100%">Salary Other</asp:label></TD>
								<TD><asp:label id="lblSalaryOtherData" CssClass="labelData" runat="server" Width="100%"></asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<!-- end button for input form --></td>
	</tr>
	<TR>
		<TD vAlign="top" noWrap align="center">
			<HR align="center">
			<asp:textbox id="txtTemp" style="DISPLAY: none" runat="server"></asp:textbox></TD>
	</TR>
	<TR>
		<TD noWrap align="center">
			<TABLE id="tblButton" cellSpacing="0" cellPadding="0" width="98%" border="0">
				<TR vAlign="top" height="30">
					<TD vAlign="middle" width="15%"><asp:checkbox id="chkShowGrid" accessKey="G" onclick="javascript:collapse('tblGrid');" CssClass="checkbox"
							runat="server" ToolTip="Alt+G" Checked="True" Text="Show grid"></asp:checkbox></TD>
					<TD align="center" width="*"><asp:linkbutton id="btnAddNew" accessKey="N" CssClass="btnAddnew" runat="server" ToolTip="Alt+N"
							CausesValidation="False">Add New</asp:linkbutton>&nbsp;&nbsp;
						<asp:linkbutton id="btnSave" accessKey="S" CssClass="btnSave" runat="server" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp;&nbsp;
						<asp:linkbutton id="btnList" accessKey="L" CssClass="btnList" runat="server" ToolTip="Alt + L" CausesValidation="False"> List</asp:linkbutton>&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnDelete" accessKey="D" CssClass="btnDelete" runat="server" ToolTip="Alt + D"
							CausesValidation="False">Delete</asp:linkbutton>&nbsp;&nbsp;
						<asp:linkbutton id="btnExport" accessKey="E" CssClass="btnExport" runat="server" ToolTip="Alt+E, Export data in grid to excel file"> Export</asp:linkbutton></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<!-- end button for input form -->
	<TR>
		<TD align="center"><!-- start grid for input form -->
			<TABLE id="tblGrid" cellSpacing="0" cellPadding="1" width="98%" border="0">
				<TR id="trGrid"> <!-- start grid detail for input form -->
					<TD colSpan="2"><asp:datagrid id="dtgList" CssClass="grid" runat="server" Width="100%" AllowPaging="True" HorizontalAlign="Center"
							BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False"
							AllowSorting="True">
							<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="ContractID" HeaderText="ID"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="AttachFile" HeaderText="AttachFile"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Select">
									<HeaderStyle Width="7%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox id="chkSelect" CssClass="checkbox" runat="server" Enabled="False"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="&lt;b&gt;#&lt;/b&gt;">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<%# Container.ItemIndex + 1%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:ButtonColumn DataTextField="EffectiveDate" HeaderText="Effective Date" CommandName="EDIT"></asp:ButtonColumn>
								<asp:BoundColumn DataField="ContractTypeName" HeaderText="Contract Type">
									<HeaderStyle HorizontalAlign="Center" Width="22%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ContractNo" HeaderText="Contract No">
									<HeaderStyle HorizontalAlign="Center" Width="22%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="SignerPosition" HeaderText="Signer Position">
									<HeaderStyle Width="13%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Signer" HeaderText="Signer">
									<HeaderStyle Width="13%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:ButtonColumn Text="Contract" HeaderText="Print" CommandName="cmdIn5">
								</asp:ButtonColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE> <!-- end grid for input form FromSmallOrEqualToDate --></TD>
	</TR>
</table>
<script language="javascript">

function validform(){
	<%if (Session["EmpID"] == null && Request.Params["FunctionID"].Trim() != "152" && Request.Params["ModuleID"].Trim() != "SI"){%>
		document.getElementById('btnSearchByID').click();
		return false;
	<%}%>
		if(checkisnull('HR_EmpHeader_txtEmpID')==false)  return false;
		if(checkisnull('cboLSContractTypeID')==false)  return false;	
		if(checkisnull('txtContractNo')==false)  return false;
		if(checkisnull('txtEffectiveDate')==false)  return false;		
	
	if(document.getElementById('_ctl0_txtEffectiveDate').value!="" && document.getElementById('_ctl0_txtToDate').value!="")
	{
		if (FromSmallToDate(document.getElementById("_ctl0_txtEffectiveDate"),document.getElementById("_ctl0_txtToDate")) == false)
			{
				//alert("Notice date from must less or equal to notice date to!");
				GetAlertError(iTotal, DSAlert, "0021");
				document.getElementById("_ctl0_txtEffectiveDate").focus();
				return false;
			}
	}	
	if(document.getElementById('_ctl0_txtProbationFrom').value!="" && document.getElementById('_ctl0_txtProbationTo').value!="")
	{
		if (FromSmallToDate(document.getElementById('_ctl0_txtProbationFrom'),document.getElementById('_ctl0_txtProbationTo') ) == false)
		{
			GetAlertError(iTotal,DSAlert,"0022");			
			document.getElementById('_ctl0_txtProbationTo').focus();				
			return false;
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
function confirmDeleteFile()
	{
		if(confirm(GetAlertText(iTotal,DSAlert,"0006"))==false){
		return false;
		}
	}
	function AddDaystoDate_(value,count)
{
	//chi ap dung cho count <=28
	value = Toddmmyyyy_value(value)
	
	var day = value.substr(0,2);
	var month = value.substr(3,2);
	var year = value.substr(6,4);
	var leap;
	day = parseFloat(day) + count;
	if (count<0)	
	{	
		if (day<=0)
			month = parseFloat(month) - 1;					
		if (parseFloat(month)==0)
			{
				month=12;
				year=parseFloat(year)-1;
			}
		/* Validation leap-year / february / day */
		if ((year % 4 == 0) || (year % 100 == 0) || (year % 400 == 0)) 
		{			
			leap = 1;
		}
		if ((month == 2) && (leap == 1) && (day <= 0)) {
			day = parseFloat(day) + 29;
		}
		else if ((month == 2) && (leap != 1) && (day <= 0)) {			
			day = parseFloat(day) + 28;
		}
		/* Validation of other months */
		else if (day <=0 && ((month == "01") || (month == "03") || (month == "05") || (month == "07") || (month == "08") || (month == "10") || (month == "12"))) {
			day = day + 31;
		}
		else if (day <=0  && ((month == "04") || (month == "06") || (month == "09") || (month == "11"))) {
			day = day + 30;
		}
		
		if (day.toString().length == 1)
		{			
			day = "0" + day.toString();
			
		}		
		if (month.toString().length == 1)
		{
			month = "0" + month.toString();
		}	
		
		//return day + "/" + month + "/" + year;
		return ToddMMMyyyy_value(day + "/" + month + "/" + year);
	}else
	{
		/* Validation leap-year / february / day */
		if ((year % 4 == 0) || (year % 100 == 0) || (year % 400 == 0)) 
		{
			leap = 1;
		}
		if ((month == 2) && (leap == 1) && (day > 29)) {
			month = parseFloat(month) + 1;
			day = parseFloat(day) - 29;
		}
		else if ((month == 2) && (leap != 1) && (day > 28)) {
			month = parseFloat(month) + 1;
			day = parseFloat(day) - 28;
		}
		/* Validation of other months */
		else if ((day > 31) && ((month == "01") || (month == "03") || (month == "05") || (month == "07") || (month == "08") || (month == "10") || (month == "12"))) {
			month = parseFloat(month) + 1;
			day = day - 31;
		}
		else if ((day > 30) && ((month == "04") || (month == "06") || (month == "09") || (month == "11"))) {
			month = month + 1;
			day = day - 30;
		}		
		if (day.toString().length == 1)
			day = "0" + day;
		if (month.toString().length == 1)
			month = "0" + month;
		//return day + "/" + month + "/" + year;
		//alert(ToddMMMyyyy_value(day + "/" + month + "/" + year));
		return ToddMMMyyyy_value(day + "/" + month + "/" + year);
	}
}
function checkNumeric(field,MaxValue)
{	
	if (field.value == "")
		return;		
	var re;   
	re = /,/g;
	var value = field.value.replace(re,'');
	if (isNaN(value))
	{
		GetAlertError(iTotal,DSAlert,"0013");
		field.value = "";
		field.focus();
		return false;
	}
	//tanldt: Test yeu cau 1 so cho phai lon hon 0
	if (value < 0)
	{		
		//alert("Must not a negative numeric or zero!");
		GetAlertError(iTotal,DSAlert,"0012");
		field.value = "";
		field.focus();
		return false;
	}if (value>MaxValue)
	{
		GetAlertTextPopUp(GetAlertText(iTotal,DSAlert,'0054').replace('@',MaxValue))
		//alert('');
		field.focus();
		return false;
	}
	FormatNumericWithSeparate(field);
}
</script>
