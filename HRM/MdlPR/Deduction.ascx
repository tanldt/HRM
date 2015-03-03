<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Deduction.ascx.cs" Inherits="iHRPCore.MdlPR.Deduction" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="EmpHeader" Src="../Include/EmpHeader.ascx" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="97%" border="0">
	<TR>
		<TD align="left" width="100%"><asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center"><uc1:empheader id="HR_EmpHeader" runat="server"></uc1:empheader></TD>
	</TR>
	<TR>
		<TD align="center">
			<!-- start detail for input form -->
			<TABLE cellSpacing="0" cellPadding="1" width="99%" border="0">
				<TR>
					<TD width="15%">
						<asp:label id="lblMonth" CssClass="labelRequire" runat="server">From Month</asp:label></TD>
					<TD width="40%">
						<asp:textbox onblur="javascript:CheckMonthYear(this)" id="txtFromMonth" CssClass="input" runat="server"
							Width="70px" MaxLength="7"></asp:textbox>
						<asp:label id="Label11" runat="server" Width="10px"></asp:label>
						<asp:label id="Label2" CssClass="label" runat="server" Width="66px">To Month</asp:label>
						<asp:textbox onblur="javascript:CheckMonthYear(this)" id="txtToMonth" CssClass="input" runat="server"
							Width="70px" MaxLength="7"></asp:textbox></TD>
					<TD width="15%">
						<asp:label id="lblPayToSalary" CssClass="labelRequire" runat="server"> Amount</asp:label></TD>
					<TD id="tdMonth" width="30%" runat="server">
						<asp:textbox onblur="javascript:checkNumeric(this)" style="TEXT-ALIGN: right" id="txtAmountPerMonth"
							CssClass="input" runat="server" Width="71" MaxLength="8"></asp:textbox>
						<asp:dropdownlist id="cboCurrencyTypeID" CssClass="combo" runat="server">
							<asp:ListItem Value="USD">USD</asp:ListItem>
							<asp:ListItem Value="VND">VND</asp:ListItem>
						</asp:dropdownlist></TD>
					<TD id="tdDate" style="DISPLAY: none" runat="server"></TD>
				</TR>
				<TR>
					<TD>
						<asp:label id="Label3" CssClass="labelRequire" runat="server"> Type</asp:label></TD>
					<TD>
						<asp:dropdownlist id="cboLSDeductionID" CssClass="combo" runat="server" Width="95%"></asp:dropdownlist></TD>
					<TD>
						<asp:label id="Label1" CssClass="label" runat="server">PIT</asp:label></TD>
					<TD><asp:checkbox id="chkPIT" runat="server" Checked="True"></asp:checkbox></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 1px"></TD>
					<TD style="HEIGHT: 1px">
						<asp:RadioButtonList id="optIsGross" runat="server" Width="150px" RepeatDirection="Horizontal">
							<asp:ListItem Value="1" Selected="True">Gross</asp:ListItem>
							<asp:ListItem Value="0">NET</asp:ListItem>
						</asp:RadioButtonList></TD>
					<TD style="HEIGHT: 1px"><asp:label id="Label5" runat="server" CssClass="label">Pay to salary</asp:label></TD>
					<TD style="HEIGHT: 1px"><asp:checkbox id="chkToPR" runat="server" Checked="True"></asp:checkbox></TD>
				</TR>
				<TR>
					<TD><asp:label id="Label10" runat="server" CssClass="label">Note</asp:label></TD>
					<TD colSpan="3"><asp:textbox id="txtNote" runat="server" CssClass="input" Width="95%" MaxLength="255"></asp:textbox></TD>
				</TR>
				<TR style="DISPLAY:none">
					<TD>
						<asp:label id="Label4" CssClass="labelRequire" runat="server">Method</asp:label></TD>
					<TD colSpan="3">
						<asp:radiobuttonlist id="optLSMethodID" onclick="javascript:MethodChange(this)" CssClass="option" runat="server"
							Width="264px" RepeatDirection="Horizontal">
							<asp:ListItem Value="Cash" Selected="True">Cash</asp:ListItem>
							<asp:ListItem Value="SalaryP">% Salary</asp:ListItem>
							<asp:ListItem Value="Coef">Coefficient</asp:ListItem>
						</asp:radiobuttonlist>
						<asp:label id="lblAmountDesc" CssClass="label" runat="server">VND</asp:label></TD>
				</TR>
			</TABLE> <!-- end button for input form --></TD>
	</TR>
	<TR>
		<TD vAlign="top" noWrap align="center" height="20">
			<HR align="center" width="95%">
			&nbsp; <INPUT id="txtDeductionID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtDeductionID"
				runat="server">
		</TD>
	</TR> <!-- start button for input form -->
	<TR>
		<TD noWrap align="center">
			<TABLE id="tblButton" cellSpacing="0" cellPadding="0" width="99%" border="0">
				<TR vAlign="top" height="30">
					<TD vAlign="middle" width="15%"><asp:checkbox id="chkShowGrid" accessKey="G" onclick="javascript:collapse('_ctl0_trGrid');" runat="server"
							CssClass="checkbox" Checked="True" Text="Show grid" ToolTip="Alt+G"></asp:checkbox></TD>
					<TD align="center" width="*"><asp:linkbutton id="btnAddNew" accessKey="N" runat="server" CssClass="btnAddnew" ToolTip="Alt+N">New record</asp:linkbutton>&nbsp;
						<asp:linkbutton id="btnSave" accessKey="S" runat="server" CssClass="btnSave" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp; 
						&nbsp;
						<asp:linkbutton id="btnDelete" accessKey="D" runat="server" CssClass="btnDelete" ToolTip="Alt + D">Delete</asp:linkbutton>&nbsp;
						<asp:linkbutton id="btnImport" Visible="False" accessKey="I" runat="server" CssClass="btnImport"
							ToolTip="Alt+I, Import data from excel file same format with export file">Import</asp:linkbutton>&nbsp;
						<asp:linkbutton id="btnExport" accessKey="E" runat="server" CssClass="btnExport" ToolTip="Alt+E, Export data in grid to excel file">Export</asp:linkbutton></TD>
				</TR>
			</TABLE>
		</TD>
	</TR> <!-- end button for input form -->
	<TR id="trGrid" runat="server">
		<TD align="center"><!-- start grid for input form -->
			<TABLE id="tblGrid" cellSpacing="0" cellPadding="1" width="98%" border="0">
				<TR>
					<TD width="60%"><asp:label id="Label12" runat="server" CssClass="label">Page rows</asp:label>&nbsp;&nbsp;
						<asp:textbox onkeypress="javascript:PageRow_Enter(this)" id="txtPageRows" runat="server" CssClass="input"
							Width="35px" MaxLength="3">20</asp:textbox>&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:label id="Label13" runat="server" CssClass="label">Total rows</asp:label>&nbsp;
						<asp:label id="lblTotalRows" runat="server" CssClass="labelData" Width="35px"></asp:label><INPUT id="btnRowNumber" style="DISPLAY: none" type="button" value="Row" name="btnRowNumber"
							runat="server"></TD>
					<TD align="right" width="*">&nbsp;
					</TD>
				</TR>
				<TR id="trGrid1"> <!-- start grid detail for input form -->
					<TD colSpan="2"><asp:datagrid id="grdDeduction" runat="server" CssClass="grid" Width="100%" AllowSorting="True"
							AutoGenerateColumns="False" AllowPaging="True" CellPadding="0" BorderWidth="1px" BorderColor="#3366CC"
							BackColor="White">
							<FooterStyle CssClass="gridFooter"></FooterStyle>
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle CssClass="gridHeader"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="DeductionID" HeaderText="DeductionID"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Seq">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<%# (grdDeduction.PageSize*grdDeduction.CurrentPageIndex) + Container.ItemIndex + 1%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn Visible="False" DataField="EmpID" HeaderText="Emp ID"></asp:BoundColumn>
								<asp:ButtonColumn DataTextField="FromMonth" SortExpression="FromMonth" HeaderText="From" CommandName="EDIT">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:ButtonColumn>
								<asp:BoundColumn DataField="ToMonth" HeaderText="To">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="DeductionType" HeaderText="Type">
									<HeaderStyle Width="20%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="AmountPerMonth" HeaderText="Amount">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Unit" HeaderText="Unit">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="IsGross" HeaderText="Gross/NET">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Note" HeaderText="Note">
									<HeaderStyle Width="15%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Select">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<HeaderTemplate>
										<asp:CheckBox id="chkSelectAll" runat="server" CssClass="checkbox" onclick="CheckAll('_ctl0_grdDeduction__ctl2_chkSelectAll','_ctl0_grdDeduction',3,1,'chkSelect')"></asp:CheckBox>
									</HeaderTemplate>
									<ItemTemplate>
										<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
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
	
	if(checkisnull('txtFromMonth')==false)  return false;	
	if(checkisnull('cboLSDeductionID')==false)  return false;	
	if(checkisnull('txtAmountPerMonth')==false)  return false;
	if(!FromSmallToMonth(document.getElementById('_ctl0_txtFromMonth'),document.getElementById('_ctl0_txtToMonth')))
	{
			GetAlertError(iTotal,DSAlert,"0007");
			document.getElementById('_ctl0_txtFromMonth').focus();
			return false;
	}	
	if (document.getElementById('_ctl0_optLSMethodID_1').checked==true/* || document.getElementById('_ctl0_optLSMethodID_2').checked==true*/)
	{		
		if (parseFloat(document.getElementById('<%=this.txtAmountPerMonth.ClientID%>').value.replace(/,/g,'')) > 100)
		{
			GetAlertError(iTotal,DSAlert,"0039");
			document.getElementById('<%=this.txtAmountPerMonth.ClientID%>').focus();
			return false;
		}
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
	if(GridCheck('_ctl0_grdDeduction',3,1,'chkSelect')==false)
	{
		GetAlertError(iTotal,DSAlert,"0001");
		return false;
	}
	if(confirm(GetAlertText(iTotal,DSAlert,"0002"))==false){
		return false;
	}
}
function MethodChange(obj){	
	if(document.getElementById(obj.id + '_0').checked==true){
		document.getElementById('_ctl0_lblAmountDesc').innerText = " VND";
		document.getElementById('_ctl0_tdMonth').style.display = "block";
		document.getElementById('_ctl0_tdDate').style.display = "none";
		document.getElementById('_ctl0_lblMonth').innerText = "Từ tháng";
	}
	else if(document.getElementById(obj.id + '_1').checked==true){
		document.getElementById('_ctl0_lblAmountDesc').innerText = " % Lương";
		document.getElementById('_ctl0_tdMonth').style.display = "block";
		document.getElementById('_ctl0_tdDate').style.display = "none";
		document.getElementById('_ctl0_lblMonth').innerText = "Từ tháng";
	}
	else if(document.getElementById(obj.id + '_2').checked==true){
		document.getElementById('_ctl0_lblAmountDesc').innerText = " hệ số lương";
		document.getElementById('_ctl0_tdMonth').style.display = "block";
		document.getElementById('_ctl0_tdDate').style.display = "none";
		document.getElementById('_ctl0_lblMonth').innerText = "Từ tháng";
	}		
}
function checkKey()
{	
	if(window.event.keyCode==13){		
		document.getElementById('_ctl0_btnRowNumber').click();
		event.returnValue=false;
		event.cancel = true;		
	}
}
function ShowExcelSelectPage()
{
	window.open('./MdlTMS/FileSelect.aspx?tpl=../TemplateExcel/Deduction_FileSelect.xls&Store=PR_spfrmDEDUCTION&Act=SaveImport','SelectFile','status=1,toolbar=0,scrollbars=1,resizable=0,alwaysRaised=Yes, top=20, left=30, width=800, height=600,1 ,align=center');
	return false;
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
