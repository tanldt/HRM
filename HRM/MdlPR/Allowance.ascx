<%@ Register TagPrefix="uc1" TagName="EmpHeader" Src="../Include/EmpHeader.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Allowance.ascx.cs" Inherits="iHRPCore.MdlPR.Allowance" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="97%" border="0">
	<TR>
		<TD align="left" width="100%"><asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center"><uc1:empheader id="HR_EmpHeader" runat="server"></uc1:empheader></TD>
	</TR>
	<TR>
		<TD align="center"><!-- start detail for input form -->
			<TABLE cellSpacing="0" cellPadding="1" width="99%" border="0">
				<TBODY>
					<TR>
						<!--<TD width="10%">
						<asp:label id="lblEffectivedate" runat="server" CssClass="labelRequire">Effective date</asp:label></TD>
					<TD vAlign="top" width="40%">
						<asp:textbox id="txtEffectivedate" onblur="javascript:CheckMonthYear(this)" runat="server" CssClass="input"
							MaxLength="7" Width="75px"></asp:textbox></TD>-->
						<TD width="11%"></TD>
						<TD width="34%"></TD>
					</TR>
					<TR>
						<TD width="15%"><asp:label id="lblMonth" CssClass="labelRequire" runat="server">From Month</asp:label></TD>
						<TD vAlign="top" width="40%"><asp:textbox id="txtFromMonth" onblur="javascript:CheckMonthYear(this)" CssClass="input" runat="server"
								Width="75px" MaxLength="7"></asp:textbox><asp:label id="Label11" runat="server" Width="10px"></asp:label>&nbsp;&nbsp;<asp:label id="Label2" CssClass="label" runat="server">To Month</asp:label>&nbsp;
							<asp:textbox id="txtToMonth" onblur="javascript:CheckMonthYear(this)" CssClass="input" runat="server"
								Width="75px" MaxLength="7"></asp:textbox></TD>
						<TD width="11%">
							<asp:label id="Label3" runat="server" CssClass="labelRequire"> Type</asp:label>
						</TD>
						<TD width="34%" id="tdMonth" runat="server">
							<asp:dropdownlist id="cboLSAllowanceID" runat="server" CssClass="combo" Width="90%"></asp:dropdownlist>
						</TD>
					</TR>
					<TR>
						<TD>
							<asp:label id="Label7" CssClass="labelRequire" runat="server"> Amount</asp:label>
						</TD>
						<TD>
							<asp:textbox id="txtAmountPerMonth" onblur="javascript:checkNumeric(this)" CssClass="input" runat="server"
								Width="66" MaxLength="10"></asp:textbox>&nbsp;
							<asp:label id="lblAmountDesc" CssClass="label" runat="server" Width="80px" Visible="False">USD</asp:label>
							<asp:dropdownlist id="cboCurrencyTypeID" runat="server" CssClass="combo">
								<asp:ListItem Value="USD">USD</asp:ListItem>
								<asp:ListItem Value="VND">VND</asp:ListItem>
							</asp:dropdownlist>
						</TD>
						<td id="tdDate" style="DISPLAY: none" runat="server"><asp:textbox id="txtDateFlex" onblur="JavaScript:CheckDate(this)" CssClass="input" runat="server"
								Width="80px" MaxLength="10"></asp:textbox>&nbsp;<INPUT class="btnCal" onclick="javascript:popUpCalendar(<%= this.txtDateFlex.ClientID %>)"  type="button"></td>
						<TD>
							<asp:label id="lblPayToSalary" runat="server" CssClass="label">Pay to salary</asp:label></TD>
						<TD>
							<asp:checkbox id="chkToPR" runat="server" Checked="True"></asp:checkbox>
							<asp:label id="lblToCashText" runat="server" Visible="False" ToolTip="Current/last month salary">To cash</asp:label>
							<asp:label id="lblToCash" runat="server" ToolTip="Current/last month salary"></asp:label>&nbsp;&nbsp;&nbsp; 
							&nbsp; &nbsp;&nbsp;
							<asp:label id="Label1" runat="server" CssClass="label">PIT</asp:label>
							<asp:checkbox id="chkPIT" runat="server" Checked="True"></asp:checkbox><INPUT id="txtLangID" style="WIDTH: 52px; HEIGHT: 22px" type="hidden" size="3" runat="server"></TD>
					</TR>
					<TR>
						<TD>
							<asp:label id="Label4" runat="server" CssClass="label">Tax exempt</asp:label></TD>
						<TD>
							<asp:textbox id="txtTaxExempt" onblur="javascript:checkNumeric(this)" runat="server" CssClass="input"
								MaxLength="10" Width="66"></asp:textbox>&nbsp;
							<asp:label id="Label5" runat="server" CssClass="label" Width="80px" Visible="False">VND</asp:label>
							<asp:dropdownlist id="cboExemptCurrencyTypeID" runat="server" CssClass="combo">
								<asp:ListItem Value="USD">USD</asp:ListItem>
								<asp:ListItem Value="VND">VND</asp:ListItem>
							</asp:dropdownlist></TD>
						<TD></TD>
						<TD>
							<asp:RadioButtonList id="optIsGross" runat="server" Width="150px" RepeatDirection="Horizontal">
								<asp:ListItem Value="1" Selected="True">Gross</asp:ListItem>
								<asp:ListItem Value="0">NET</asp:ListItem>
							</asp:RadioButtonList></TD>
						<TD></TD>
					</TR>
					<TR>
						<TD>
							<asp:label id="Label10" runat="server" CssClass="label">Note</asp:label></TD>
						<TD colspan="3">
							<asp:textbox id="txtNote" runat="server" CssClass="input" MaxLength="100" Width="100%"></asp:textbox></TD>
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
		<INPUT id="txtAllowanceID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtAllowanceID"
			runat="server">
	</TD>
</TR>
<TR>
	<TD noWrap align="center">
		<TABLE id="tblButton" cellSpacing="0" cellPadding="0" width="99%" border="0">
			<TR vAlign="top" height="30">
				<TD vAlign="middle" width="15%">
					<asp:checkbox id="chkShowGrid" accessKey="G" onclick="javascript:collapse('_ctl0_trGrid');" runat="server"
						CssClass="checkbox" Checked="True" ToolTip="Alt+G" Text="Show grid"></asp:checkbox></TD>
				<TD align="center" width="*">
					<asp:linkbutton id="btnAddNew" accessKey="N" runat="server" CssClass="btnAddnew" ToolTip="Alt+N">New record</asp:linkbutton>&nbsp;&nbsp;&nbsp;
					<asp:linkbutton id="btnSave" accessKey="S" runat="server" CssClass="btnSave" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp;&nbsp;&nbsp;
					<asp:linkbutton id="btnDelete" accessKey="D" runat="server" CssClass="btnDelete" ToolTip="Alt + D">Delete</asp:linkbutton>&nbsp;&nbsp;&nbsp;
					<asp:linkbutton id="btnImport" accessKey="I" runat="server" CssClass="btnImport" ToolTip="Alt+I, Import data from excel file same format with export file"
						Visible="False">Import</asp:linkbutton>&nbsp;&nbsp;
					<asp:linkbutton id="btnExport" accessKey="E" runat="server" CssClass="btnExport" ToolTip="Alt+E, Export data in grid to excel file">Export</asp:linkbutton></TD>
			</TR>
		</TABLE>
	</TD>
</TR> <!-- end button for input form -->
<TR id="trGrid" runat="server">
	<TD align="center"><!-- start grid for input form -->
		<TABLE id="tblGrid" cellSpacing="0" cellPadding="1" width="98%" border="0">
			<TR>
				<TD width="40%">
					<asp:label id="Label12" runat="server" CssClass="label">Page rows</asp:label>&nbsp;&nbsp;&nbsp;
					<asp:textbox onkeypress="javascript:PageRow_Enter(this)" id="txtPageRows" runat="server" CssClass="input"
						MaxLength="3" Width="35px">20</asp:textbox>&nbsp;&nbsp;&nbsp;
					<asp:label id="Label13" runat="server" CssClass="label">Total rows</asp:label>&nbsp;
					<asp:label id="lblTotalRows" runat="server" CssClass="labelData" Width="35px"></asp:label><INPUT id="btnRowNumber" style="DISPLAY: none" type="button" value="Row" name="btnRowNumber"
						runat="server"></TD>
				<TD align="right" width="*">&nbsp;
				</TD>
			</TR>
			<TR id="trGrid1"> <!-- start grid detail for input form -->
				<TD colSpan="2">
					<asp:datagrid id="grdAllowance" runat="server" CssClass="grid" Width="100%" BackColor="White"
						BorderColor="#3366CC" BorderWidth="1px" CellPadding="0" AllowPaging="True" AutoGenerateColumns="False"
						AllowSorting="True" DESIGNTIMEDRAGDROP="89">
						<FooterStyle CssClass="gridFooter"></FooterStyle>
						<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
						<ItemStyle CssClass="gridItem"></ItemStyle>
						<HeaderStyle CssClass="gridHeader"></HeaderStyle>
						<Columns>
							<asp:BoundColumn Visible="False" DataField="AllowanceID" HeaderText="ID">
								<HeaderStyle Width="7%"></HeaderStyle>
							</asp:BoundColumn>
							<asp:TemplateColumn HeaderText="Select">
								<HeaderStyle Width="5%"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<HeaderTemplate>
									<asp:CheckBox id="chkSelectAll" runat="server" CssClass="checkbox" onclick="CheckAll('_ctl0_grdAllowance__ctl2_chkSelectAll','_ctl0_grdAllowance',3,1,'chkSelect')"></asp:CheckBox>
								</HeaderTemplate>
								<ItemTemplate>
									<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Seq">
								<HeaderStyle Width="5%"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<%# (grdAllowance.PageSize*grdAllowance.CurrentPageIndex) + Container.ItemIndex + 1%>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:BoundColumn Visible="False" DataField="EmpID" HeaderText="Emp ID"></asp:BoundColumn>
							<asp:ButtonColumn DataTextField="FromMonth" HeaderText="From" CommandName="EDIT">
								<HeaderStyle Width="5%"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
							</asp:ButtonColumn>
							<asp:BoundColumn DataField="ToMonth" HeaderText="To">
								<HeaderStyle Width="10%"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
							</asp:BoundColumn>
							<asp:BoundColumn DataField="AllowanceType" HeaderText="Type">
								<HeaderStyle Width="23%"></HeaderStyle>
								<ItemStyle HorizontalAlign="Left"></ItemStyle>
							</asp:BoundColumn>
							<asp:BoundColumn DataField="Amount" HeaderText="Amount">
								<HeaderStyle Width="12%"></HeaderStyle>
								<ItemStyle HorizontalAlign="Right"></ItemStyle>
							</asp:BoundColumn>
							<asp:BoundColumn DataField="CurrencyName" HeaderText="Currency">
								<HeaderStyle Width="10%"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
							</asp:BoundColumn>
							<asp:BoundColumn DataField="ToPR" HeaderText="PR">
								<HeaderStyle Width="5%"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
							</asp:BoundColumn>
							<asp:BoundColumn DataField="PIT" HeaderText="PIT">
								<HeaderStyle Width="5%"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
							</asp:BoundColumn>
							<asp:BoundColumn DataField="IsGross" HeaderText="Gross/NET">
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

function validform(){					
	if (document.getElementById('<%=this.txtAmountPerMonth.ClientID%>').value=='')
	{
	 GetAlertError(iTotal,DSAlert,"AL_0001");	 
	 document.getElementById('<%=this.txtAmountPerMonth.ClientID%>').focus();
	 return false;
	}
	/*if (document.getElementById('_ctl0_optLSMethodID_1').checked==true || document.getElementById('_ctl0_optLSMethodID_2').checked==true )
	{		
		if (parseFloat(document.getElementById('<%=this.txtAmountPerMonth.ClientID%>').value.replace(/,/g,'')) > 100)
		{
			GetAlertError(iTotal,DSAlert,"0039");
			document.getElementById('<%=this.txtAmountPerMonth.ClientID%>').focus();
			return false;
		}
	}
	if (document.getElementById('_ctl0_optLSMethodID_2').checked==true)
	{
		if (document.getElementById('<%=this.txtDateFlex.ClientID%>').value=='')
		{
			GetAlertError(iTotal,DSAlert,"0003");
			document.getElementById('<%=this.txtDateFlex.ClientID%>').focus();
			return false;
		}
	}*/
	
	if (document.getElementById('<%=this.txtFromMonth.ClientID%>').value=='')
		{
			GetAlertError(iTotal,DSAlert,"AL_0002");
			document.getElementById('_ctl0_txtFromMonth').focus();
			return false;
		}	else if(!FromSmallToMonth(document.getElementById('_ctl0_txtFromMonth'),document.getElementById('_ctl0_txtToMonth')))
		{
				GetAlertError(iTotal,DSAlert,"0007");
				document.getElementById('_ctl0_txtFromMonth').focus();
				return false;
		}
	
	
	if (document.getElementById('<%=this.cboLSAllowanceID.ClientID%>').value=='')
	{
		GetAlertError(iTotal,DSAlert,"AL_0003");
		document.getElementById('_ctl0_cboLSAllowanceID').focus();
		return false;
	}					
	if (document.getElementById('_ctl0_txtToMonth').value != '')
	{
		if( IsSmallerOrEqual('01/'+document.getElementById('_ctl0_txtFromMonth').value,'01/'+document.getElementById('_ctl0_txtToMonth').value) == false)
		{		
			GetAlertError(iTotal, DSAlert, "0007");
			document.getElementById('_ctl0_txtFromMonth').focus();
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
	if(GridCheck('_ctl0_grdAllowance',3,1,'chkSelect')==false)
	{
		GetAlertError(iTotal,DSAlert,"0001");				
		return false;
	}
	if(confirm(GetAlertText(iTotal,DSAlert,"0002"))==false){
		return false;
	}
}
function MethodChange(obj){	
	var field=document.getElementById('_ctl0_txtAmountPerMonth')
	if(document.getElementById(obj.id + '_0').checked==true){
		document.getElementById('_ctl0_lblAmountDesc').innerText = " USD";
		document.getElementById('_ctl0_tdMonth').style.display = "block";
		document.getElementById('_ctl0_tdDate').style.display = "none";
		//document.getElementById('_ctl0_lblMonth').innerText = "From month";
		/*alert('không bi?t');				
		field.attachEvent("onchange",onchange_checkNumeric);		*/
	}
	else if(document.getElementById(obj.id + '_1').checked==true){
		document.getElementById('<%=this.lblAmountDesc.ClientID%>').innerText = " Percent";		
		
		document.getElementById('_ctl0_tdMonth').style.display = "block";
		document.getElementById('_ctl0_tdDate').style.display = "none";
		//document.getElementById('_ctl0_lblMonth').innerText = "From month";
		//field.attachEvent("onchange",onchange_CheckPercent);		
	}
	else{
		document.getElementById('_ctl0_lblAmountDesc').innerText = " Coef";
		document.getElementById('_ctl0_tdMonth').style.display = "block";
		document.getElementById('_ctl0_tdDate').style.display = "none";
		//document.getElementById('_ctl0_lblMonth').innerText = "From month";
		//field.attachEvent("onchange",onchange_CheckPercent);		
		
	}
}

function onchange_CheckPercent()
{	
	CheckPercent(document.getElementById('_ctl0_txtAmountPerMonth'))
}
function onchange_checkNumeric()
{	
	checkNumeric(document.getElementById('_ctl0_txtAmountPerMonth'))
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
