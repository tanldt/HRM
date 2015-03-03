<%@ Control Language="c#" AutoEventWireup="false" Codebehind="LoanDetail.ascx.cs" Inherits="iHRPCore.MdlPR.LoadDetail" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="EmpHeader" Src="../Include/EmpHeader.ascx" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="97%" border="0">
	<TR>
		<TD align="left" width="100%"><asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label><asp:label id="lblTotal" style="DISPLAY: none" runat="server" ForeColor="Black"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center"><uc1:empheader id="HR_EmpHeader" runat="server"></uc1:empheader></TD>
	</TR>
	<TR>
		<TD align="center">
			<!-- start detail for input form -->
			<TABLE cellSpacing="0" cellPadding="1" width="99%" border="0">
				<TR>
					<TD><asp:label id="Label4" runat="server" CssClass="labelRequire"> Loan time</asp:label></TD>
					<TD colSpan="3"><asp:dropdownlist id="cboLoanTime" runat="server" CssClass="combo" Width="73%" AutoPostBack="True"></asp:dropdownlist></TD>
					<TD><asp:label id="Label17" runat="server" CssClass="label">Status</asp:label></TD>
					<TD><asp:label id="lblStatus" runat="server" CssClass="labelData"></asp:label></TD>
				</TR>
				<TR>
					<TD width="14%"><asp:label id="Label2" runat="server" CssClass="label">Loan Amount</asp:label></TD>
					<TD width="27%"><asp:label id="lblLoanAmount" runat="server" CssClass="labelData"></asp:label>&nbsp;
						<asp:label id="lblCurrency" runat="server" CssClass="labelData">USD</asp:label></TD>
					<TD width="15%"><asp:label id="Label1" runat="server" CssClass="label">Paid</asp:label></TD>
					<TD width="16%"><asp:label id="lblPaid" runat="server" CssClass="labelData"></asp:label></TD>
					<TD width="13%"><asp:label id="Label5" runat="server" CssClass="label">Remained</asp:label></TD>
					<TD width="15%"><asp:label id="lblRemainedAmount" runat="server" CssClass="labelData"></asp:label></TD>
				</TR>
				<TR>
					<TD><asp:checkbox id="chkLastPay" accessKey="G" onclick="javascript:LastPay();" runat="server" CssClass="checkbox"
							Text="Last pay"></asp:checkbox></TD>
					<TD></TD>
					<TD><asp:label id="Label6" runat="server" CssClass="label">Interest rate</asp:label></TD>
					<TD><asp:label id="lblRate" runat="server" CssClass="labelData"></asp:label></TD>
					<TD><asp:label id="Label10" runat="server" CssClass="label">Support</asp:label></TD>
					<TD><asp:label id="lblSupport" runat="server" CssClass="labelData"></asp:label></TD>
				</TR>
				<TR>
					<TD><asp:label id="Label8" runat="server" CssClass="labelRequire">PR month</asp:label></TD>
					<TD><asp:textbox id="txtPRMonth" runat="server" CssClass="input" Width="80px" ReadOnly="True"></asp:textbox>&nbsp;&nbsp;&nbsp;</TD>
					<TD><asp:label id="Label9" runat="server" CssClass="labelRequire"> Pay Amount</asp:label></TD>
					<TD><asp:textbox id="txtMonthToPay" style="TEXT-ALIGN:right" onblur="javascript:AmountChange(this);"
							onfocus="javascript:CallTotal(this)" runat="server" CssClass="input" Width="80px"></asp:textbox></TD>
					<TD><!--<asp:label id="Label3" runat="server" CssClass="label">Pay in cash</asp:label>--></TD>
					<TD><!--<asp:textbox id="txtPayInCash" style="TEXT-ALIGN:right" onblur="javascript:CashChange(this);"
							onfocus="javascript:CallTotal(this)" runat="server" CssClass="input" Width="80px"></asp:textbox>--></TD>
				</TR>
				<TR>
					<TD><asp:label id="Note" runat="server" CssClass="label">Note</asp:label></TD>
					<TD colSpan="5"><asp:textbox id="txtNote" runat="server" CssClass="input" Width="500px" MaxLength="255"></asp:textbox></TD>
				</TR>
			</TABLE>
			<!-- end button for input form --></TD>
	</TR>
	<TR>
		<TD vAlign="top" noWrap align="center" height="20">
			<HR align="center" width="95%">
			<INPUT id="hdLoanDetailID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="hdLoanDetailID"
				runat="server"><INPUT id="hdLastPay" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="hdLastPay"
				runat="server"></TD>
	</TR>
	<!-- start button for input form -->
	<TR vAlign="top" height="30">
		<TD noWrap align="center"><asp:linkbutton id="btnSave" accessKey="S" runat="server" CssClass="btnSave" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp;&nbsp;&nbsp;<asp:linkbutton id="btnPayDetail" accessKey="Y" runat="server" CssClass="Button" ToolTip="ALT+Y">Loan</asp:linkbutton>&nbsp;&nbsp;
			<asp:linkbutton id="btnList" accessKey="L" runat="server" CssClass="btnList" ToolTip="Alt + L">Loan List</asp:linkbutton>&nbsp; 
			&nbsp;
			<asp:linkbutton id="btnExport" accessKey="E" runat="server" CssClass="btnExport" ToolTip="Alt+E, Export data in grid to excel file">Export</asp:linkbutton></TD>
		</TD></TR>
	<!-- end button for input form -->
	<TR>
		<TD align="center"><!-- start grid for input form -->
			<TABLE id="tblGrid" cellSpacing="0" cellPadding="1" width="98%" border="0">
				<TR id="trGrid"> <!-- start grid detail for input form -->
					<TD colSpan="2"><asp:datagrid id="grdLoanDetail" runat="server" CssClass="grid" Width="100%" PageSize="2" AllowSorting="True"
							AutoGenerateColumns="False" CellPadding="0" BorderWidth="1px" BorderColor="#3366CC" BackColor="White">
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle CssClass="gridHeader"></HeaderStyle>
							<FooterStyle CssClass="gridFooter"></FooterStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="LoanDetailID" HeaderText="LoanDetailID"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Seq">
									<HeaderStyle HorizontalAlign="Center" Width="5%" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<%# Container.ItemIndex + 1 + grdLoanDetail.PageSize*grdLoanDetail.CurrentPageIndex%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:ButtonColumn Text="From" DataTextField="MonthID" HeaderText="From" CommandName="EDIT">
									<ItemStyle CssClass="hLink"></ItemStyle>
								</asp:ButtonColumn>
								<asp:BoundColumn DataField="MonthAmount" HeaderText="Amount">
									<HeaderStyle Width="25%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Note" HeaderText="Note">
									<HeaderStyle Width="40%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE> <!-- end grid for input form --></TD>
	</TR>
</TABLE>
<script language="javascript">
<!--
function LastPay(){
	var blnLastPaid = document.getElementById('_ctl0_chkLastPay').checked;
	if(blnLastPaid == true)
	{
		document.getElementById('_ctl0_txtMonthToPay').value = document.getElementById('_ctl0_lblRemainedAmount').innerText;
		document.getElementById('_ctl0_lblTotal').innerText = document.getElementById('_ctl0_lblRemainedAmount').innerText;
		document.getElementById('_ctl0_txtPayInCash').value = "0";
	}
	else
	{
		document.getElementById('_ctl0_txtMonthToPay').value = document.getElementById('_ctl0_hdLastPay').value;
		document.getElementById('_ctl0_lblTotal').innerText = "0";
		document.getElementById('_ctl0_txtPayInCash').value = "0";		
	}
}
function CashChange(obj){
	//checkNumeric(obj);
	checkNumeric(obj);
	//alert(parseFloat(document.getElementById('_ctl0_lblTotal').innerText.replace(/,/g,''),10));
	if(document.getElementById('_ctl0_txtPayInCash').value=="")
		document.getElementById('_ctl0_txtPayInCash').value = "0";
	else
	{
		if(parseFloat(document.getElementById('_ctl0_lblTotal').innerText.replace(/,/g,''),10) > parseFloat(document.getElementById('_ctl0_txtPayInCash').value.replace(/,/g,''),10))
			document.getElementById('_ctl0_txtMonthToPay').value = 
				parseFloat(document.getElementById('_ctl0_lblTotal').innerText.replace(/,/g,''),10) -
				parseFloat(document.getElementById('_ctl0_txtPayInCash').value.replace(/,/g,''),10);
		else
		{
			document.getElementById('_ctl0_txtMonthToPay').value = "0";
		}	
	}
		
	if(document.getElementById('_ctl0_chkLastPay').checked == true){
		document.getElementById('_ctl0_txtMonthToPay').value =
		parseFloat(document.getElementById('_ctl0_lblRemainedAmount').innerText.replace(/,/g,''),10) -
		parseFloat(document.getElementById('_ctl0_txtPayInCash').value.replace(/,/g,''),10)		
	}
	FormatNumericWithSeparate(document.getElementById('_ctl0_txtMonthToPay'));
	FormatNumericWithSeparate(document.getElementById('_ctl0_txtPayInCash'));
}
function AmountChange(obj){
	//checkNumeric(obj);	
	checkNumeric(obj);
	if(document.getElementById('_ctl0_txtMonthToPay').value=="")
		document.getElementById('_ctl0_txtMonthToPay').value = "0"	;
	else
	{	
		if(parseFloat(document.getElementById('_ctl0_txtMonthToPay').value.replace(/,/g,'')) > parseFloat(document.getElementById('_ctl0_lblRemainedAmount').innerText.replace(/,/g,'')))
		{		
			GetAlertError(iTotal,DSAlert,"LD_0001");
			document.getElementById('_ctl0_txtMonthToPay').value = 
					parseFloat(document.getElementById('_ctl0_lblTotal').innerText.replace(/,/g,''),10) -
					parseFloat(document.getElementById('_ctl0_txtPayInCash').value.replace(/,/g,''),10);
			return false;
		}
			
		document.getElementById('_ctl0_lblTotal').innerText = document.getElementById('_ctl0_txtMonthToPay').value;
		/*
		if(parseFloat(document.getElementById('_ctl0_lblTotal').innerText.replace(/,/g,''),10) > parseFloat(document.getElementById('_ctl0_txtPayInCash').value.replace(/,/g,''),10))
				document.getElementById('_ctl0_txtMonthToPay').value = 
					parseFloat(document.getElementById('_ctl0_lblTotal').innerText.replace(/,/g,''),10) -
					parseFloat(document.getElementById('_ctl0_txtPayInCash').value.replace(/,/g,''),10);
		else
			document.getElementById('_ctl0_txtPayInCash').value = "0";
			*/
	}
		
		
	if(document.getElementById('_ctl0_chkLastPay').checked == true){
		document.getElementById('_ctl0_txtPayInCash').value =
		parseFloat(document.getElementById('_ctl0_lblRemainedAmount').innerText.replace(/,/g,''),10) -
		parseFloat(document.getElementById('_ctl0_txtMonthToPay').value.replace(/,/g,''),10)		
	}
	FormatNumericWithSeparate(document.getElementById('_ctl0_txtMonthToPay'));
	FormatNumericWithSeparate(document.getElementById('_ctl0_txtPayInCash'));
}
function CallTotal(obj)
{
	checkNumeric(obj);
	
	if(document.getElementById('_ctl0_txtPayInCash').value=="")
		document.getElementById('_ctl0_txtPayInCash').value = "0";
	if(document.getElementById('_ctl0_txtMonthToPay').value=="")
		document.getElementById('_ctl0_txtMonthToPay').value = "0"	;
		
	document.getElementById('_ctl0_lblTotal').innerText = 
		parseFloat(document.getElementById('_ctl0_txtMonthToPay').value.replace(/,/g,''),10) +
		parseFloat(document.getElementById('_ctl0_txtPayInCash').value.replace(/,/g,''),10)		
}
function validform()
{
	if (checkisnull('txtMonthToPay')==false) return false;	
	if (checkisnull('cboLoanTime')==false) return false;	
	if (checkisnull('txtPRMonth')==false) return false;	
	return;
	
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
//-->
</script>
