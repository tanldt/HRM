<%@ Register TagPrefix="uc1" TagName="EmpHeader" Src="../Include/EmpHeader.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="unfrequentincome.ascx.cs" Inherits="MdlPR.unfrequentincome" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<script language="javascript" src="../Include/common.js">
</script>
<script language="javascript">
</script>
<TABLE cellSpacing="0" cellPadding="0" width="99%" align="center" border="0">
	<TR>
		<TD noWrap align="left"><asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD noWrap align="center"><uc1:empheader id="HR_EmpHeader" runat="server"></uc1:empheader></TD>
	</TR>
	<TR>
		<TD align="center">
			<TABLE cellSpacing="0" cellPadding="1" width="99%" border="0">
				<TR>
					<TD width="22%"><asp:label id="Label1" CssClass="labelrequire" runat="server">Unfrequent income type</asp:label></TD>
					<TD width="35%"><asp:dropdownlist id="cboUnfrequentIncomeTypeID" CssClass="select" runat="server" Width="85%"></asp:dropdownlist></TD>
					<TD width="10%"><asp:label id="Label6" CssClass="labelrequire" runat="server">Amount</asp:label></TD>
					<TD width="40%" colSpan="3"><asp:textbox id="txtAmount" onblur="javascript:checkNumeric(this)" style="TEXT-ALIGN: right"
							CssClass="input" runat="server" Width="81px" MaxLength="9"></asp:textbox><asp:dropdownlist id="cboCurrencyTypeID" CssClass="combo" runat="server">
							<asp:ListItem Value="USD">USD</asp:ListItem>
							<asp:ListItem Value="VND">VND</asp:ListItem>
						</asp:dropdownlist></TD>
					<TD width="10%"></TD>
					<TD width="15%"></TD>
					<TD width="20%"></TD>
				</TR>
				<TR>
					<td style="HEIGHT: 16px"><asp:label id="Label14" CssClass="labelrequire" runat="server">PR Month</asp:label></td>
					<TD style="HEIGHT: 16px"><asp:textbox id="txtMonth" onblur="javascript:CheckMonthYear(this)" CssClass="input" runat="server"
							Width="81px" MaxLength="7"></asp:textbox></TD>
					<TD style="HEIGHT: 16px"><asp:label id="Label3" CssClass="labelrequire" runat="server">Date</asp:label></TD>
					<TD style="HEIGHT: 16px"><asp:textbox id="txtDate" onblur="JavaScript:CheckDate(this)" CssClass="input" runat="server"
							Width="81" MaxLength="10"></asp:textbox>&nbsp;<INPUT class=btnCal onclick="javascript:popUpCalendar(<%= this.txtDate.ClientID %>)" type=button></TD>
					<TD style="HEIGHT: 16px"></TD>
					<td style="HEIGHT: 16px"></td>
				</TR>
				<TR>
					<td><asp:label id="Label19" CssClass="label" runat="server">Note</asp:label></td>
					<td colSpan="5"><asp:textbox id="txtNote" CssClass="input" runat="server" Width="98%" MaxLength="100"></asp:textbox></td>
				</TR>
			</TABLE>
			<!-- end button for list form --></TD>
	</TR>
	<TR>
		<TD vAlign="top" noWrap align="center" height="10">
			<HR align="center" width="95%">
			<INPUT id="hdBonusID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="hdBonusID"
				runat="server"></TD>
	</TR>
	<TR> <!-- start button for list form -->
		<TD noWrap align="center">
			<TABLE id="tblButton" cellSpacing="0" cellPadding="0" width="98%" border="0">
				<TR vAlign="middle" height="35">
					<TD vAlign="middle" width="15%"><asp:checkbox id="chkShowGrid" onclick="javascript:collapse('_ctl0_trGrid');" CssClass="checkbox"
							runat="server" Text="Show grid" Checked="True"></asp:checkbox></TD>
					<TD align="center" width="*"><asp:linkbutton id="btnAddNew" accessKey="A" CssClass="btnAddnew" runat="server" ToolTip="Alt+N">New record</asp:linkbutton>&nbsp;
						<asp:linkbutton id="btnSave" accessKey="S" CssClass="btnSave" runat="server" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp;
						<asp:linkbutton id="btnDelete" accessKey="D" CssClass="btnDelete" runat="server" ToolTip="Alt+D">Delete</asp:linkbutton>&nbsp;&nbsp;
						<asp:linkbutton id="btnExport" accessKey="E" CssClass="btnExport" runat="server" ToolTip="Alt+E, Export data in grid to excel file">Export</asp:linkbutton></TD>
				</TR>
			</TABLE>
		</TD>
	</TR> <!-- end button for list form -->
	<TR id="trGrid" runat="server">
		<TD align="center"><!-- start grid for input form -->
			<TABLE id="tblGrid1" cellSpacing="0" cellPadding="1" width="98%" border="0">
				<TR>
					<TD id="tdPages" width="43%"><asp:label id="Label4" CssClass="label" runat="server">Page rows</asp:label>&nbsp;&nbsp;
						<asp:textbox onkeypress="javascript:PageRow_Enter(this)" id="txtPageRows" CssClass="input" runat="server"
							Width="35px" MaxLength="3">20</asp:textbox>&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:label id="Label5" CssClass="label" runat="server">Total rows</asp:label>&nbsp;
						<asp:label id="lblTotalRows" CssClass="label" runat="server"></asp:label>&nbsp;&nbsp;<INPUT id="btnRowNumber" style="DISPLAY: none" type="button" value="Row" name="btnRowNumber"
							runat="server"></TD>
					<TD id="tdSort" style="DISPLAY: none" align="right" width="*"><asp:label id="Label8" CssClass="labelRight" runat="server" Width="30px">  Col 1</asp:label><asp:dropdownlist id="cboCol1" CssClass="combo" runat="server" Width="80px"></asp:dropdownlist><asp:label id="Label7" CssClass="labelRight" runat="server" Width="30px">Col 2</asp:label>&nbsp;
						<asp:dropdownlist id="cboCol2" CssClass="combo" runat="server" Width="80px"></asp:dropdownlist><asp:label id="Label15" CssClass="labelRight" runat="server" Width="30px">Col 3</asp:label>&nbsp;
						<asp:dropdownlist id="cboCol3" CssClass="combo" runat="server" Width="80px"></asp:dropdownlist></TD>
				</TR>
				<TR id="trGrid2"> <!-- start grid detail for input form -->
					<TD colSpan="2"><asp:datagrid id="grdData" CssClass="grid" runat="server" Width="100%" AllowPaging="True" PageSize="20"
							AllowSorting="True" AutoGenerateColumns="False" CellPadding="0" BorderWidth="1px" BorderColor="#3366CC"
							BackColor="White">
							<FooterStyle CssClass="gridFooter"></FooterStyle>
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle CssClass="gridHeader"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="UnfrequentIncomeID" HeaderText="UnfrequentIncomeID"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Select">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<HeaderTemplate>
										<asp:CheckBox id="chkSelectAll" runat="server" CssClass="checkbox" onclick="CheckAll('_ctl0_grdData__ctl2_chkSelectAll','_ctl0_grdData',3,1,'chkSelect')"></asp:CheckBox>
									</HeaderTemplate>
									<ItemTemplate>
										<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Seq">
									<HeaderStyle Width="4%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<%# Container.ItemIndex + 1 + grdData.PageSize*grdData.CurrentPageIndex%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn Visible="False" DataField="EmpCode" HeaderText="Emp Code">
									<HeaderStyle Width="10%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:ButtonColumn DataTextField="Date" HeaderText="Date" CommandName="EDIT">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:ButtonColumn>
								<asp:BoundColumn DataField="UnfrequentTypeName" HeaderText="Type">
									<HeaderStyle Width="15%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Month" HeaderText="PRMonth">
									<HeaderStyle Width="9%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Amount" HeaderText="Amount">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CurrencyTypeID" HeaderText="Currency">
									<HeaderStyle Width="10%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Note" HeaderText="Note">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE> <!-- end grid for input form --><INPUT id="txtUnfrequentIncomeID" type="hidden" name="txtBonusID" runat="server">
			<asp:TextBox id="txtStrListID" runat="server" Visible="False"></asp:TextBox></TD>
	</TR>
</TABLE>
<SCRIPT language="javascript">
<!--

function validform(){	
	if(checkisnull('HR_EmpHeader_txtEmpID')==false){  	
		return false
	};
	if(checkisnull('cboUnfrequentIncomeTypeID')==false)  return false;
	if(checkisnull('txtAmount')==false)  return false;
	if(checkisnull('cboCurrencyTypeID')==false)  return false;
	if(checkisnull('txtMonth')==false)  return false;
	if(checkisnull('txtDate')==false)  return false;
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
	if(GridCheck('_ctl0_grdData',3,1,'chkSelect')==false)
	{
		GetAlertError(iTotal,DSAlert,"0001");				
		return false;
	}
	if(confirm(GetAlertText(iTotal,DSAlert,"0002"))==false){
		return false;
	}
}
function MethodChange(obj){	
	
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
		window.open('./MdlTMS/FileSelect.aspx?tpl=../TemplateExcel/Bonus_FileSelect.xls&Store=PR_spfrmBonus&Act=SaveImport','SelectFile','status=1,toolbar=0,scrollbars=1,resizable=0,alwaysRaised=Yes, top=20, left=30, width=800, height=600,1 ,align=center');
		return false;
	}
	
function PageRow_Enter(field)
{	
	var str = field.value;
	if (!checkInt(field, 9999))
	{	field.focus();
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
