<%@ Register TagPrefix="uc1" TagName="EmpHeader" Src="../Include/EmpHeader.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ManageHouse.ascx.cs" Inherits="iHRPCore.MdlPR.ManageHouse" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
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
				<TBODY>
					<TR>
						<TD width="14%"></TD>
						<TD vAlign="top" width="40%"></TD>
						<TD width="18%"></TD>
						<TD width="30%"></TD>
					</TR>
					<TR>
						<TD style="HEIGHT: 25px" width="15%"><asp:label id="lblMonth" runat="server" CssClass="labelRequire">Effective month</asp:label></TD>
						<TD style="HEIGHT: 25px" vAlign="top" width="40%"><asp:textbox id="txtFromMonth" onblur="javascript:CheckMonthYear(this)" runat="server" CssClass="input"
								MaxLength="7" Width="75px"></asp:textbox>&nbsp;&nbsp;&nbsp;</TD>
						<TD style="HEIGHT: 25px" width="11%"><asp:label id="Label2" runat="server" CssClass="label">Expired month</asp:label></TD>
						<TD style="HEIGHT: 25px" width="34%"><asp:textbox id="txtToMonth" onblur="javascript:CheckMonthYear(this)" runat="server" CssClass="input"
								MaxLength="7" Width="75px"></asp:textbox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<asp:label id="Label1" runat="server" CssClass="label">PIT</asp:label><asp:checkbox id="chkPIT" runat="server" Checked="True"></asp:checkbox><INPUT id="txtLangID" style="WIDTH: 52px; HEIGHT: 22px" type="hidden" size="3" runat="server"></TD>
					</TR>
					<TR>
						<TD style="HEIGHT: 25px" width="15%"><asp:label id="Label7" runat="server" CssClass="labelRequire"> Contract worth</asp:label></TD>
						<TD style="HEIGHT: 25px" vAlign="top" width="40%"><asp:textbox id="txtAmount" onblur="javascript:checkNumeric(this)" runat="server" CssClass="input"
								MaxLength="10" Width="75px"></asp:textbox><asp:dropdownlist id="cboCurrencyTypeID" runat="server" CssClass="combo">
								<asp:ListItem Value="USD">USD</asp:ListItem>
								<asp:ListItem Value="VND">VND</asp:ListItem>
							</asp:dropdownlist><asp:label id="Label4" runat="server" CssClass="label">Amount/Month</asp:label></TD>
						<TD style="HEIGHT: 25px" width="11%"><asp:label id="Label3" runat="server" CssClass="labelRequire"> Housing allowance</asp:label></TD>
						<TD style="HEIGHT: 25px" width="34%"><asp:textbox id="txtAllowance" onblur="javascript:checkNumeric(this)" runat="server" CssClass="input"
								MaxLength="10" Width="75px"></asp:textbox><asp:dropdownlist id="Dropdownlist1" runat="server" CssClass="combo" Visible="False">
								<asp:ListItem Value="USD">USD</asp:ListItem>
								<asp:ListItem Value="VND">VND</asp:ListItem>
							</asp:dropdownlist><asp:label id="Label6" runat="server" CssClass="label">Amount/Month</asp:label></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="txtPaySource" CssClass="labelRequire" runat="server">Pay source</asp:Label></TD>
						<TD colSpan="3">
							<asp:dropdownlist id="cboPaySource" CssClass="combo" runat="server">
								<asp:ListItem></asp:ListItem>
								<asp:ListItem Value="1">Personal</asp:ListItem>
								<asp:ListItem Value="0">Office</asp:ListItem>
							</asp:dropdownlist></TD>
					</TR>
					<TR>
						<TD><asp:label id="Label5" runat="server" CssClass="label">Address</asp:label></TD>
						<TD colSpan="3"><asp:textbox id="txtAddress" runat="server" CssClass="input" MaxLength="100" Width="100%"></asp:textbox></TD>
					</TR>
					<TR>
						<TD style="HEIGHT: 26px"><asp:label id="Label10" runat="server" CssClass="label">Note</asp:label></TD>
						<TD style="HEIGHT: 26px" colSpan="3"><asp:textbox id="txtNote" runat="server" CssClass="input" MaxLength="100" Width="100%"></asp:textbox></TD>
		</TD>
	</TR>
	<TR>
		<TD></TD>
		<TD colSpan="3"></TD>
	</TR>
</TABLE> <!-- end button for input form --> </TD></TR>
<TR>
	<TD vAlign="top" noWrap align="center" height="20">
		<HR align="center" width="95%">
		<INPUT id="txtManageHouseID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtManageHouseID"
			runat="server" DESIGNTIMEDRAGDROP="618">&nbsp;
		<asp:textbox id="txtStrListID" runat="server" Width="44px" Visible="False"></asp:textbox></TD>
</TR>
<TR>
	<TD noWrap align="center">
		<TABLE id="tblButton" cellSpacing="0" cellPadding="0" width="99%" border="0">
			<TR vAlign="top" height="30">
				<TD vAlign="middle" width="15%"><asp:checkbox id="chkShowGrid" accessKey="G" onclick="javascript:collapse('_ctl0_trGrid');" runat="server"
						CssClass="checkbox" Checked="True" Text="Show grid" ToolTip="Alt+G"></asp:checkbox></TD>
				<TD align="center" width="*"><asp:linkbutton id="btnAddNew" accessKey="N" runat="server" CssClass="btnAddnew" ToolTip="Alt+N">New record</asp:linkbutton>&nbsp;&nbsp;&nbsp;
					<asp:linkbutton id="btnSave" accessKey="S" runat="server" CssClass="btnSave" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp;&nbsp;&nbsp;
					<asp:linkbutton id="btnDelete" accessKey="D" runat="server" CssClass="btnDelete" ToolTip="Alt + D">Delete</asp:linkbutton>&nbsp;&nbsp;&nbsp;
					<asp:linkbutton id="btnExport" accessKey="E" runat="server" CssClass="btnExport" ToolTip="Alt+E, Export data in grid to excel file">Export</asp:linkbutton></TD>
			</TR>
		</TABLE>
	</TD>
</TR> <!-- end button for input form -->
<TR id="trGrid" runat="server">
	<TD align="center"><!-- start grid for input form -->
		<TABLE id="tblGrid" cellSpacing="0" cellPadding="1" width="98%" border="0">
			<TR>
				<TD width="40%"><asp:label id="Label12" runat="server" CssClass="label">Page rows</asp:label>&nbsp;&nbsp;&nbsp;
					<asp:textbox onkeypress="javascript:PageRow_Enter(this)" id="txtPageRows" runat="server" CssClass="input"
						MaxLength="3" Width="35px">20</asp:textbox>&nbsp;&nbsp;&nbsp;
					<asp:label id="Label13" runat="server" CssClass="label">Total rows</asp:label>&nbsp;
					<asp:label id="lblTotalRows" runat="server" CssClass="labelData" Width="35px"></asp:label><INPUT id="btnRowNumber" style="DISPLAY: none" type="button" value="Row" name="btnRowNumber"
						runat="server"></TD>
				<TD align="right" width="*">&nbsp;
				</TD>
			</TR>
			<TR id="trGrid1"> <!-- start grid detail for input form -->
				<TD colSpan="2"><asp:datagrid id="grdData" runat="server" CssClass="grid" Width="100%" DESIGNTIMEDRAGDROP="89"
						AllowSorting="True" AutoGenerateColumns="False" AllowPaging="True" CellPadding="0" BorderWidth="1px" BorderColor="#3366CC"
						BackColor="White" PageSize="20">
						<FooterStyle CssClass="gridFooter"></FooterStyle>
						<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
						<ItemStyle CssClass="gridItem"></ItemStyle>
						<HeaderStyle CssClass="gridHeader"></HeaderStyle>
						<Columns>
							<asp:BoundColumn Visible="False" DataField="ManageHouseID" HeaderText="ID">
								<HeaderStyle Width="7%"></HeaderStyle>
							</asp:BoundColumn>
							<asp:BoundColumn Visible="False" DataField="EmpID" HeaderText="Emp ID"></asp:BoundColumn>
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
								<HeaderStyle Width="5%"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<%# (grdData.PageSize*grdData.CurrentPageIndex) + Container.ItemIndex + 1%>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:ButtonColumn DataTextField="FromMonth" HeaderText="Eff Month" CommandName="EDIT">
								<HeaderStyle Width="5%"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
							</asp:ButtonColumn>
							<asp:BoundColumn DataField="ToMonth" HeaderText="Exp Month">
								<HeaderStyle Width="10%"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
							</asp:BoundColumn>
							<asp:BoundColumn DataField="Amount" HeaderText="Contract worth">
								<HeaderStyle Width="14%"></HeaderStyle>
								<ItemStyle HorizontalAlign="Right"></ItemStyle>
							</asp:BoundColumn>
							<asp:BoundColumn DataField="Allowance" HeaderText="Allowance">
								<HeaderStyle Width="12%"></HeaderStyle>
							</asp:BoundColumn>
							<asp:BoundColumn DataField="CurrencyName" HeaderText="Currency">
								<HeaderStyle Width="10%"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
							</asp:BoundColumn>
							<asp:BoundColumn DataField="Address" HeaderText="Address"></asp:BoundColumn>
							<asp:BoundColumn Visible="False" DataField="PIT" HeaderText="PIT">
								<HeaderStyle Width="5%"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
							</asp:BoundColumn>
							<asp:BoundColumn DataField="Note" HeaderText="Note">
								<HeaderStyle Width="14%"></HeaderStyle>
							</asp:BoundColumn>
						</Columns>
						<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
					</asp:datagrid></TD>
			</TR>
		</TABLE> <!-- end grid for input form --></TD>
</TR>
</TBODY></TABLE>
<SCRIPT language="javascript">
<!--
function myCheckDate(strFromDate, strToDate)
{
	if(strFromDate!="" && strToDate!="")
	{	
		if (IsSmaller(strFromDate,strToDate) == false)
		{	
			//alert("Notice date from must less or equal to notice date to!");
			GetAlertError(iTotal, DSAlert, "0021");
			return false;
		}
	}
	return true;
}

function validform(){	
	if(checkisnull('txtFromMonth')==false)  return false;
	if(checkisnull('txtAmount')==false)  return false;	
	if(checkisnull('txtAllowance')==false)  return false;	
	if(checkisnull('cboCurrencyTypeID')==false)  return false;
	if(checkisnull('cboPaySource')==false)  return false;
	if(document.getElementById('<%=this.HR_EmpHeader.txtEmpID.ClientID%>').value=="" || document.getElementById('<%=this.HR_EmpHeader.txtEmpName.ClientID%>').value=="")
	{
		GetAlertError(iTotal,DSAlert,"0003");
		document.getElementById('<%=this.HR_EmpHeader.txtEmpID.ClientID%>').focus();
		return false;
	}
	
	if(parseFloat(document.getElementById('<%=this.txtAmount.ClientID%>').value.replace(/,/g,''))<  parseFloat(document.getElementById('<%=this.txtAllowance.ClientID%>').value.replace(/,/g,'')))
	{
		GetAlertError(iTotal,DSAlert,"PR_MH_001");
		document.getElementById('<%=this.txtAllowance.ClientID%>').focus();
		return false;	
	}
	var from="01/"+document.getElementById('<%=this.txtFromMonth.ClientID%>').value;
	var to="01/"+document.getElementById('<%=this.txtToMonth.ClientID%>').value;	
	if( myCheckDate(from,to)==false ){	
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
	var field=document.getElementById('_ctl0_txtAmount')
	if(document.getElementById(obj.id + '_0').checked==true){
		document.getElementById('_ctl0_tdMonth').style.display = "block";
		document.getElementById('_ctl0_tdDate').style.display = "none";
	}
	else if(document.getElementById(obj.id + '_1').checked==true){
		
		
		document.getElementById('_ctl0_tdMonth').style.display = "block";
		document.getElementById('_ctl0_tdDate').style.display = "none";
		//document.getElementById('_ctl0_lblMonth').innerText = "From month";
		//field.attachEvent("onchange",onchange_CheckPercent);		
	}
	else{
		//document.getElementById('_ctl0_lblAmountDesc').innerText = " Coef";
		document.getElementById('_ctl0_tdMonth').style.display = "block";
		document.getElementById('_ctl0_tdDate').style.display = "none";
		//document.getElementById('_ctl0_lblMonth').innerText = "From month";
		//field.attachEvent("onchange",onchange_CheckPercent);		
		
	}
}

function onchange_CheckPercent()
{	
	CheckPercent(document.getElementById('_ctl0_txtAmount'))
}
function onchange_checkNumeric()
{	
	checkNumeric(document.getElementById('_ctl0_txtAmount'))
}
/*
function checkKey()
{
	if(window.event.keyCode==13){		
		document.getElementById('_ctl0_btnRowNumber').click();		
		event.returnValue=false;
		event.cancel = true;		
	}
}
*/
function ShowExcelSelectPage()
	{
		
		window.open('./MdlTMS/FileSelect.aspx?tpl=../MdlPR/Allowance_FileSelect.xls&Store=PR_spfrmAllowance&Act=SaveImport','SelectFile','status=1,toolbar=0,scrollbars=1,resizable=0,alwaysRaised=Yes, top=20, left=30, width=800, height=600,1 ,align=center');
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
