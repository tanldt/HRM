<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Loan.ascx.cs" Inherits="iHRPCore.MdlPR.Loan" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="EmpHeader" Src="../Include/EmpHeader.ascx" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="97%" border="0">
	<TR>
		<TD style="HEIGHT: 19px" align="left" width="100%"><asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center"><uc1:empheader id="HR_EmpHeader" runat="server"></uc1:empheader></TD>
	</TR>
	<TR>
		<TD align="center">
			<!-- start detail for input form -->
			<TABLE cellSpacing="0" cellPadding="1" width="99%" border="0">
				<TR>
					<TD><asp:label id="Label4" CssClass="labelRequire" runat="server"> Purpose</asp:label></TD>
					<TD colSpan="5"><asp:dropdownlist id="cboLSLoanPurposeID" CssClass="select" runat="server" Width="49%"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD width="14%"><asp:label id="Label2" CssClass="labelRequire" runat="server">Loan Amount</asp:label></TD>
					<TD width="27%"><asp:textbox id="txtLoanAmount" style="TEXT-ALIGN:right" onblur="javascript:checkNumeric(this)"
							CssClass="input" runat="server" Width="75px" MaxLength="9"></asp:textbox>&nbsp;<asp:label id="lblCurrency" CssClass="labelData" runat="server">USD</asp:label></TD>
					<TD width="15%"><asp:label id="Label1" CssClass="label" runat="server">Paid</asp:label></TD>
					<TD width="16%"><asp:label id="lblPaid" CssClass="labelData" runat="server"></asp:label>&nbsp;
						<asp:label id="Label7" CssClass="labelData" runat="server">USD</asp:label></TD>
					<TD width="13%"><asp:label id="Label5" CssClass="label" runat="server">Remained</asp:label></TD>
					<TD width="15%"><asp:label id="lblRemainedAmount" CssClass="labelData" runat="server"></asp:label>&nbsp;
						<asp:label id="Label12" CssClass="labelData" runat="server">USD</asp:label></TD>
				</TR>
				<TR>
					<TD><asp:label id="Label3" CssClass="labelRequire" runat="server">Interest rate(%)</asp:label></TD>
					<TD><asp:textbox id="txtInterestRate" style="TEXT-ALIGN:right" onblur="javascript:CheckPercent(this)"
							CssClass="input" runat="server" Width="75px" MaxLength="4"></asp:textbox></TD>
					<TD><asp:label id="Label6" CssClass="labelRequire" runat="server">Co. Support(%)</asp:label></TD>
					<TD><asp:textbox id="txtSupportRate" style="TEXT-ALIGN:right" onblur="javascript:CheckPercent(this)"
							CssClass="input" runat="server" Width="65px" MaxLength="4"></asp:textbox></TD>
					<TD><asp:label id="Label13" CssClass="label" runat="server" Visible="False">Monthly amount</asp:label></TD>
					<TD><asp:label id="lblMonthly" CssClass="labelData" runat="server" Visible="False"></asp:label></TD>
				</TR>
				<TR>
					<TD><asp:label id="Label8" CssClass="labelRequire" runat="server">From month</asp:label></TD>
					<TD><asp:textbox id="txtFromMonth" onblur="javascript:CheckMonthYear(this)" CssClass="input" runat="server"
							Width="75px" MaxLength="7"></asp:textbox>&nbsp;
						<asp:label id="Label14" CssClass="label" runat="server" Visible="False">To</asp:label>&nbsp;
						<asp:label id="lblToMonth" CssClass="labelData" runat="server"></asp:label></TD>
					<TD><asp:label id="Label9" CssClass="labelRequire" runat="server">No of months</asp:label></TD>
					<TD><asp:textbox id="txtNoOfMonth" style="TEXT-ALIGN:right" onblur="javascript:checkInteger(this)"
							CssClass="input" runat="server" Width="65px" MaxLength="2"></asp:textbox></TD>
					<TD><asp:label id="Label10" CssClass="label" runat="server">Remained</asp:label></TD>
					<TD><asp:label id="lblRemainedMonth" CssClass="labelData" runat="server"></asp:label>&nbsp;
						<asp:label id="Label15" CssClass="labelData" runat="server">Month(s)</asp:label></TD>
				</TR>
				<TR>
					<TD><asp:label id="Label11" CssClass="label" runat="server">Reason</asp:label></TD>
					<TD colSpan="3"><asp:textbox id="txtReason" CssClass="input" runat="server" Width="95%" MaxLength="255"></asp:textbox>&nbsp;</TD>
					<TD><asp:label id="Label17" CssClass="label" runat="server">Status</asp:label></TD>
					<TD><asp:label id="lblStatus" CssClass="labelData" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD><asp:label id="Note" CssClass="label" runat="server">Remarks</asp:label></TD>
					<TD colSpan="4"><asp:textbox id="txtNote" CssClass="input" runat="server" Width="400px" MaxLength="255"></asp:textbox></TD>
					<TD><asp:label id="Label19" CssClass="label" runat="server">Deduction to PT</asp:label></TD>
					<TD><asp:CheckBox id="chkDeduction" runat="server" CssClass="checkbox"></asp:CheckBox></TD>
					<TD></TD>
				</TR>
			</TABLE>
			<!-- end button for input form --></TD>
	</TR>
	<TR>
		<TD vAlign="top" noWrap align="center" height="20">
			<HR align="center" width="95%">
			<INPUT id="hdLoanID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="hdLoanID"
				runat="server">
		</TD>
	</TR>
	<!-- start button for input form -->
	<TR>
		<TD noWrap align="center">
			<TABLE id="tblButton" cellSpacing="0" cellPadding="0" width="99%" border="0">
				<TR style="DISPLAY:none">
					<TD style="WIDTH: 84px" vAlign="middle" width="84"><asp:checkbox id="chkShowGrid" accessKey="G" onclick="javascript:collapse('tblGrid');" CssClass="checkbox"
							runat="server" Visible="False" ToolTip="Alt+G" Checked="True" Text="Show grid"></asp:checkbox></TD>
					<TD align="left"></TD>
				</TR>
				<TR vAlign="top" height="30">
					<TD align="center" width="*" colSpan="2"><asp:linkbutton id="btnAddNew" accessKey="N" CssClass="btnAddnew" runat="server" ToolTip="Alt+N">New record</asp:linkbutton>&nbsp;
						<asp:linkbutton id="btnSave" accessKey="S" CssClass="btnSave" runat="server" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp;
						<asp:linkbutton id="btnPayDetail" accessKey="Y" CssClass="Button" runat="server" ToolTip="ALT+Y">Pay detail</asp:linkbutton>&nbsp;
						<asp:linkbutton id="btnDelete" accessKey="D" CssClass="btnDelete" runat="server" ToolTip="Alt + D">Delete</asp:linkbutton>&nbsp;
						<asp:linkbutton id="btnList" accessKey="L" CssClass="btnList" runat="server" ToolTip="Alt + L">List</asp:linkbutton>&nbsp;
						<asp:linkbutton id="btnExport" accessKey="E" CssClass="btnExport" runat="server" ToolTip="Alt+E, Export data in grid to excel file">Export</asp:linkbutton></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<!-- end button for input form -->
	<TR>
		<TD align="center"><!-- start grid for input form -->
			<TABLE id="tblGrid" cellSpacing="0" cellPadding="1" width="98%" border="0">
				<TR>
					<TD width="40%">&nbsp;
						<asp:label id="Label16" runat="server" CssClass="label">Page rows</asp:label>&nbsp;
						<asp:textbox onkeypress="javascript:PageRow_Enter(this)" id="txtPageRows" runat="server" CssClass="input"
							Width="35px" MaxLength="3">20</asp:textbox>&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:label id="Label18" CssClass="label" runat="server">Total rows</asp:label>&nbsp;
						<asp:label id="lblTotalRows" CssClass="labelData" runat="server" Width="35px"></asp:label><INPUT id="btnRowNumber" style="DISPLAY: none" type="button" value="Row" name="btnRowNumber"
							runat="server"></TD>
					<TD align="right" width="*">&nbsp;&nbsp;&nbsp;
					</TD>
				</TR>
				<TR id="trGrid"> <!-- start grid detail for input form -->
					<TD colSpan="2"><asp:datagrid id="grdLoan" CssClass="grid" runat="server" Width="100%" AllowPaging="True" AllowSorting="True"
							AutoGenerateColumns="False" CellPadding="0" BorderWidth="1px" BorderColor="#3366CC" BackColor="White">
							<FooterStyle CssClass="gridFooter"></FooterStyle>
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle CssClass="gridHeader"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="LoanID" HeaderText="ID"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Select">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Seq">
									<HeaderStyle HorizontalAlign="Center" Width="5%" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<%# Container.ItemIndex + 1 + grdLoan.PageSize*grdLoan.CurrentPageIndex%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:ButtonColumn DataTextField="FromMonth" HeaderText="From" CommandName="EDIT">
									<ItemStyle CssClass="hLink"></ItemStyle>
								</asp:ButtonColumn>
								<asp:BoundColumn DataField="LoanPurpose" HeaderText="Purpose">
									<HeaderStyle Width="24%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="LoanAmount" HeaderText="Amount">
									<HeaderStyle Width="12%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="InterestRate" HeaderText="Rate">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="SupportRate" HeaderText="Support">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="NoOfMonth" HeaderText="Months">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
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
	
	//if(checkisnull('txtLoanAmount')==false)  return false;	
	if(parseFloat(document.getElementById('_ctl0_txtLoanAmount').value.replace(/,/g,''),10) > parseFloat(document.getElementById('_ctl0_txtLoanAmount').title.replace(/,/g,''),10)){
		//alert('Không th? cho vay s? ti?n quá quy d?nh!');
		GetAlertError(iTotal,DSAlert,"L_0001");
		return false;
	}	
	if(checkisnull('cboLSLoanPurposeID')==false)  return false;
	if(checkisnull('txtLoanAmount')==false)  return false;	
	if(checkisnull('txtFromMonth')==false)  return false;	
	if(checkisnull('txtNoOfMonth')==false)  return false;
	if(checkisnull('txtInterestRate')==false)  return false;
	if(checkisnull('txtSupportRate')==false)  return false;
	if (document.getElementById('_ctl0_txtNoOfMonth').value < 1)
		{
			GetAlertError(iTotal,DSAlert,"L_00022");		
			return false;
		}
				
	return true;
}
function checkisnull(obj){
	if(document.getElementById('_ctl0_' + obj).value=="")
		{
			GetAlertError(iTotal,DSAlert,"0003");
			document.getElementById('_ctl0_' + obj).focus();
			return false;
		}
	else
		return true;
}
function checkdelete()
{
	if(GridCheck('_ctl0_grdLoan',3,1,'chkSelect')==false)
	{
		GetAlertError(iTotal,DSAlert,"0001");
		return false;
	}
	if(confirm(GetAlertText(iTotal,DSAlert,"0002"))==false){
		return false;
	}
}

function PageRow_Enter(field)
{	
	var str = field.value;
	//checkInteger(field, 9999);
	if (!checkInt(field, 9999))
	{
		field.focus();
		return;
	}

	field.value = str;
		
	if(window.event.keyCode == 13){
		document.getElementById('_ctl0_btnRowNumber').click();
		event.returnValue=false;
		event.cancel = true;		
	}
}

//-->
</SCRIPT>
