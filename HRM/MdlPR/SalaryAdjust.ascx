<%@ Register TagPrefix="uc1" TagName="EmpHeader" Src="../Include/EmpHeader.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="SalaryAdjust.ascx.cs" Inherits="iHRPCore.MdlPR.SalaryAdjust" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="97%" border="0">
	<TR>
		<TD align="left" width="100%"><asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center"><uc1:empheader id="HR_EmpHeader" runat="server"></uc1:empheader></TD>
	</TR>
	<TR>
		<TD align="center">
			<!-- start detail for input form -->
			<TABLE cellSpacing="0" cellPadding="1" width="99%" border="0">
				<TR>
					<TD width="14%" style="HEIGHT: 17px">
						<asp:label id="Label1" runat="server" CssClass="label">Add/Subtract</asp:label></TD>
					<TD width="23%" style="HEIGHT: 17px">
						<asp:radiobuttonlist id="optAddSubtract" runat="server" CssClass="option" RepeatDirection="Horizontal">
							<asp:ListItem Value="1" Selected="True">Add</asp:ListItem>
							<asp:ListItem Value="0">Subtract</asp:ListItem>
						</asp:radiobuttonlist>
					</TD>
					<TD width="8%" style="HEIGHT: 17px">
						<asp:label id="Label5" runat="server" CssClass="label">PIT Pay</asp:label></TD>
					<TD width="35%" style="HEIGHT: 17px">
						<asp:CheckBox id="chkPIT" runat="server" CssClass="checkbox"></asp:CheckBox></TD>
				</TR>
				<TR>
					<TD>
						<asp:label id="Label4" runat="server" CssClass="labelRequire" Width="100%"> Adjustment type</asp:label></TD>
					<TD>
						<asp:dropdownlist id="cboLSSalaryAdjustID" runat="server" CssClass="select" Width="90%"></asp:dropdownlist></TD>
					<TD>
						<asp:label id="Label2" runat="server" CssClass="labelRequire">Amount</asp:label></TD>
					<TD>
						<asp:textbox id="txtAmount" style="TEXT-ALIGN:right" onblur="javascript:checkNumeric(this)" runat="server"
							CssClass="input" Width="60px" MaxLength="8"></asp:textbox>&nbsp;
						<asp:DropDownList id="cboCurrencyTypeID" runat="server" CssClass="select"></asp:DropDownList>&nbsp;&nbsp; 
						&nbsp;
						<asp:label id="Label3" runat="server" CssClass="labelRequire">PR month</asp:label>
						<asp:textbox id="txtMonth" onblur="javascript:CheckMonthYear(this)" runat="server" CssClass="input"
							Width="55px" MaxLength="7"></asp:textbox></TD>
				</TR>
				<TR>
					<TD>
						<asp:label id="Label6" runat="server" CssClass="label">Note</asp:label></TD>
					<TD colSpan="3" valign="top">
						<asp:textbox id="txtNote" runat="server" CssClass="input" Width="100%" MaxLength="255"></asp:textbox></TD>
				</TR>
			</TABLE>
			<!-- end button for input form -->
		</TD>
	</TR>
	<TR>
		<TD vAlign="top" noWrap align="center" height="20">
			<HR align="center" width="95%">
			<asp:TextBox Runat="server" ID="txtSalaryAdjustID" style="DISPLAY:none"></asp:TextBox>
		</TD>
	</TR>
	<!-- start button for input form -->
	<TR>
		<TD noWrap align="center">
			<TABLE id="tblButton" cellSpacing="0" cellPadding="0" width="99%" border="0">
				<TR vAlign="top" height="30">
					<TD vAlign="middle" width="15%"><asp:checkbox id="chkShowGrid" onclick="javascript:collapse('_ctl0_trGrid');" CssClass="checkbox"
							runat="server" Text="Show grid" Checked="True" accessKey="G" ToolTip="Alt+G"></asp:checkbox></TD>
					<TD align="center" width="*"><asp:linkbutton id="btnAddNew" accessKey="N" CssClass="btnAddnew" runat="server" ToolTip="Alt+N">New record</asp:linkbutton>&nbsp;
						<asp:linkbutton id="btnSave" accessKey="S" CssClass="btnSave" runat="server" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnDelete" accessKey="D" CssClass="btnDelete" runat="server" ToolTip="Alt + D">Delete</asp:linkbutton>&nbsp;
						<asp:linkbutton id="btnImport" accessKey="I" runat="server" CssClass="btnImport" ToolTip="Alt+I, Import data from excel file same format with export file"
							Visible="False">Import</asp:linkbutton>&nbsp;&nbsp;
						<asp:linkbutton id="btnExport" accessKey="E" runat="server" CssClass="btnExport" ToolTip="Alt+E, Export data in grid to excel file">Export</asp:linkbutton></TD>
				</TR>
				<TR>
					<TD vAlign="middle" width="15%"></TD>
					<TD align="center"></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<!-- end button for input form -->
	<TR id="trGrid" runat="server">
		<TD align="center"><!-- start grid for input form -->
			<TABLE id="tblGrid" cellSpacing="0" cellPadding="1" width="98%" border="0">
				<TR>
					<TD width="40%">&nbsp;
						<asp:label id="Label7" runat="server" CssClass="label">Page rows</asp:label>&nbsp;
						<asp:textbox onkeypress="javascript:PageRow_Enter(this)" id="txtPageRows" runat="server" CssClass="input"
							Width="35px" MaxLength="3">20</asp:textbox>&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:label id="Label13" CssClass="label" runat="server">Total rows</asp:label>&nbsp;
						<asp:label id="lblTotalRows" CssClass="labelData" runat="server" Width="35px"></asp:label><INPUT id="btnRowNumber" style="DISPLAY: none" type="button" value="Row" name="btnRowNumber"
							runat="server"></TD>
					<TD align="right" width="*">&nbsp;&nbsp;&nbsp;
					</TD>
				</TR>
				<TR id="trGrid1"> <!-- start grid detail for input form -->
					<TD colspan="2"><asp:datagrid id="grdSalaryAdjust" CssClass="grid" runat="server" Width="100%" AllowSorting="True"
							AutoGenerateColumns="False" CellPadding="0" BorderWidth="1px" BorderColor="#3366CC" BackColor="White"
							AllowPaging="True" PageSize="20">
							<FooterStyle CssClass="gridFooter"></FooterStyle>
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle CssClass="gridHeader"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Select">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Seq">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<%# Container.ItemIndex + 1 + grdSalaryAdjust.PageSize*grdSalaryAdjust.CurrentPageIndex%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:ButtonColumn DataTextField="PRMonth" SortExpression="PRMonth" HeaderText="Month" CommandName="EDIT">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:ButtonColumn>
								<asp:BoundColumn DataField="SalaryAdjustType" HeaderText="Type">
									<HeaderStyle Width="30%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="AddSubtract" HeaderText="(+)/(-)">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Amount" HeaderText="Amount">
									<HeaderStyle Width="12%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CurrencyName" HeaderText="Currency"></asp:BoundColumn>
								<asp:BoundColumn DataField="PIT" HeaderText="PIT">
									<HeaderStyle Width="6%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Note" HeaderText="Note">
									<HeaderStyle Width="20%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE> <!-- end grid for input form -->
		</TD>
	</TR>
</TABLE>
<SCRIPT language="javascript">
<!--
function validform(){	
	if(checkisnull('cboLSSalaryAdjustID')==false)  return false;
	if(checkisnull('txtAmount')==false)  return false;
	if(checkisnull('cboCurrencyTypeID')==false)  return false;	
	if(checkisnull('txtMonth')==false)  return false;			
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
function checkdelete(){
	if(GridCheck('_ctl0_grdSalaryAdjust',3,1,'chkSelect')==false)
	{
		GetAlertError(iTotal,DSAlert,"0001");
		return false;
	}
	if(confirm(GetAlertText(iTotal,DSAlert,"0002"))==false){
		return false;
	}
}
function checkKey(){	
	if(window.event.keyCode==13){		
		document.getElementById('_ctl0_btnRowNumber').click();		
		event.returnValue=false;
		event.cancel = true;		
	}
}
function ShowExcelSelectPage()
{
	window.open('./MdlTMS/FileSelect.aspx?tpl=../TemplateExcel/SalaryAdjust_FileSelect.xls&Store=PR_spfrmSalaryAdjust&Act=SaveImport','SelectFile','status=1,toolbar=0,scrollbars=1,resizable=0,alwaysRaised=Yes, top=20, left=30, width=800, height=600,1 ,align=center');
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
