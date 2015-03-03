<%@ Register TagPrefix="uc1" TagName="EmpHeader" Src="../Include/EmpHeader.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Overtime.ascx.cs" Inherits="iHRPCore.MdlPR.Overtime" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="97%" border="0">
	<TR>
		<TD align="left" width="100%"><asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center"><uc1:empheader id="HR_EmpHeader" runat="server"></uc1:empheader></TD>
	</TR>
	<TR>
		<TD align="center">
			<TABLE cellSpacing="0" cellPadding="1" width="99%" border="0">
				<TR>
					<TD width="10%"><asp:label id="Label16" runat="server" CssClass="labelrequire"> Date</asp:label></TD>
					<TD width="24%"><asp:textbox id="txtOTDate" onblur="JavaScript:CheckDate(this)" runat="server" CssClass="input"
							Width="80px" MaxLength="10"></asp:textbox>&nbsp;<INPUT class="btnCal" onclick="ShowCalendar('_ctl0_txtOTDate')" type="button"></TD>
					<TD width="13%"><asp:label id="Label2" runat="server" CssClass="labelrequire"> Time</asp:label></TD>
					<TD width="30%"><asp:textbox id="txtTimeFrom" onblur="javascript:CalHours(this)" runat="server" CssClass="input"
							Width="50px" MaxLength="5"></asp:textbox><asp:label id="Label1" runat="server" CssClass="labelCenter" Width="10px">-</asp:label><asp:textbox id="txtTimeTo" onblur="javascript:CalHours(this)" runat="server" CssClass="input"
							Width="50px" MaxLength="5"></asp:textbox></TD>
					<TD width="11%"><asp:label id="Label4" runat="server" CssClass="labelrequire"> No of Hours</asp:label></TD>
					<TD width="12%"><asp:textbox id="txtHours" onblur="javascript:checkNumeric(this)" runat="server" CssClass="input"
							Width="72px" MaxLength="4"></asp:textbox></TD>
				</TR>
				<TR>
					<TD><asp:label id="Label3" runat="server" CssClass="labelrequire"> Type</asp:label></TD>
					<TD><asp:dropdownlist id="cboOTTypeCode" runat="server" CssClass="combo" Width="130px"></asp:dropdownlist></TD>
					<TD><asp:label id="Label5" runat="server" CssClass="labelrequire">Charge budget</asp:label></TD>
					<TD><asp:dropdownlist id="cboChargeBudgetCode" runat="server" CssClass="combo" Width="165px"></asp:dropdownlist></TD>
					<TD><asp:label id="Label6" runat="server" CssClass="labelrequire">PR month</asp:label></TD>
					<TD><asp:textbox id="txtPRMonth" onblur="javascript:CheckMonthYear(this)" runat="server" CssClass="input"
							Width="72px" MaxLength="7"></asp:textbox></TD>
				</TR>
				<TR>
					<TD><asp:label id="Label9" runat="server" CssClass="label">Purpose</asp:label></TD>
					<TD valign="top"><asp:textbox id="txtPurpose" runat="server" style="POSITION: absolute" CssClass="input" Width="130px"
							MaxLength="200"></asp:textbox></TD>
					<TD><asp:label id="Label10" runat="server" CssClass="label">Note</asp:label></TD>
					<TD valign="top"><asp:textbox id="txtNote" runat="server" style="POSITION: absolute" CssClass="input" Width="165px"
							MaxLength="255"></asp:textbox></TD>
					<TD><asp:label id="Label7" runat="server" CssClass="label" Visible="False"> Amount</asp:label></TD>
					<TD><asp:textbox id="txtAmount" runat="server" CssClass="input" Width="72px" MaxLength="9" BorderColor="White"
							ReadOnly="True" Visible="False"></asp:textbox></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD vAlign="top" noWrap align="center" height="20">
			<HR align="center" width="95%">
			<INPUT id="txtOTID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtOTID"
				runat="server"><INPUT id="txtBasicSalary" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtBasicSalary"
				runat="server"></TD>
	</TR>
	<!-- start button for input form -->
	<TR>
		<TD noWrap align="center">
			<TABLE id="tblButton" cellSpacing="0" cellPadding="0" width="99%" border="0">
				<TR vAlign="top" height="30">
					<TD vAlign="middle" width="15%"><asp:checkbox id="chkShowGrid" accessKey="G" onclick="javascript:collapse('_ctl0_trGrid');" runat="server"
							CssClass="checkbox" ToolTip="Alt+G" Checked="True" Text="Show grid"></asp:checkbox></TD>
					<TD align="center" width="*"><asp:linkbutton id="btnAddNew" accessKey="N" runat="server" CssClass="btnAddnew" ToolTip="Alt+N">New record</asp:linkbutton>&nbsp;
						<asp:linkbutton id="btnSave" accessKey="S" runat="server" CssClass="btnSave" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp;&nbsp;
						<asp:linkbutton id="btnDelete" accessKey="D" runat="server" CssClass="btnDelete" ToolTip="Alt + D">Delete</asp:linkbutton>&nbsp;
						<asp:linkbutton id="butImport" accessKey="I" runat="server" CssClass="btnImport" ToolTip="Alt+I, Import data from excel file same format with export file">Import</asp:linkbutton>&nbsp;
						<asp:linkbutton id="butExport" accessKey="E" runat="server" CssClass="btnExport" ToolTip="Alt+E, Export data in grid to excel file">Export</asp:linkbutton></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<!-- end button for input form -->
	<TR id="trGrid" runat="server">
		<TD align="center"><!-- start grid for input form -->
			<TABLE id="tblGrid" cellSpacing="0" cellPadding="1" width="98%" border="0">
				<TR>
					<TD width="40%"><asp:label id="Label12" runat="server" CssClass="label">Page rows</asp:label>&nbsp;&nbsp;
						<asp:textbox id="txtPageRows" runat="server" CssClass="input" Width="35px" MaxLength="3"></asp:textbox>&nbsp;&nbsp;&nbsp;
						<asp:label id="Label13" runat="server" CssClass="label">Total rows</asp:label>&nbsp;
						<asp:label id="lblTotalRows" runat="server" CssClass="labelData" Width="35px"></asp:label></TD>
					<TD noWrap align="right">
						<asp:label id="Label8" runat="server" CssClass="label">From date</asp:label>&nbsp;
						<asp:textbox id="txtFromDate" onblur="JavaScript:CheckDate(this)" runat="server" CssClass="input"
							Width="70px"></asp:textbox>&nbsp;<INPUT class="btnCal" onclick="ShowCalendar('_ctl0_txtFromDate')" type="button">&nbsp;&nbsp;
						<asp:label id="Label14" runat="server" CssClass="label"> To date</asp:label>&nbsp;
						<asp:textbox id="txtTodate" runat="server" onblur="JavaScript:CheckDate(this)" CssClass="input"
							Width="70px"></asp:textbox>&nbsp;<INPUT class="btnCal" onclick="ShowCalendar('_ctl0_txtTodate')" type="button">&nbsp;
						<asp:linkbutton id="btnSearch" accessKey="E" runat="server" CssClass="btnSearch" ToolTip="Alt+ E, Search based on From date, To date, Type, PR month">Search</asp:linkbutton>&nbsp;
					</TD>
				</TR>
				<TR id="trGrid1"> <!-- start grid detail for input form -->
					<TD colSpan="2">
						<asp:datagrid id="grdOvertime" runat="server" CssClass="grid" Width="100%" BackColor="White" BorderColor="#3366CC"
							BorderWidth="1px" CellPadding="0" AllowPaging="True" AutoGenerateColumns="False" AllowSorting="True"
							PageSize="7">
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle CssClass="gridHeader"></HeaderStyle>
							<FooterStyle CssClass="gridFooter"></FooterStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="OTID" HeaderText="Seq">
									<HeaderStyle HorizontalAlign="Center" Width="5%" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="&lt;b&gt;Seq&lt;/b&gt;">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<%# Container.ItemIndex + 1%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Date">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton Runat="server" Enabled="true" ID="btnEdit" CssClass="hLink" CommandName="Edit">
											<%# DataBinder.Eval(Container, "DataItem.OTDate")%>
										</asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="EMPID" Visible="False" HeaderText="Emp ID">
									<HeaderStyle Width="9%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn HeaderText="From" DataField="TimeFrom">
									<HeaderStyle Width="7%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn HeaderText="To" DataField="TimeTo">
									<HeaderStyle Width="7%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn HeaderText="Purpose" DataField="Purpose">
									<HeaderStyle Width="20%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn HeaderText="Type" DataField="Type">
									<HeaderStyle Width="15%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn HeaderText="Hours" DataField="Hours">
									<HeaderStyle Width="8%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn HeaderText="Amount" DataField="Amount">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn HeaderText="PR month" DataField="PRMonth">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" HeaderText="Note" DataField="Note">
									<HeaderStyle Width="10%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Select">
									<HeaderStyle Width="8%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<HeaderTemplate>
										<asp:CheckBox id="chkSelectAll" runat="server" CssClass="checkbox" onclick="CheckAll('_ctl0_grdOvertime__ctl2_chkSelectAll','_ctl0_grdOvertime',3,1,'chkSelect')"></asp:CheckBox>
									</HeaderTemplate>
									<ItemTemplate>
										<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>
<SCRIPT language="javascript">
<!--
function CalHours(obj){	
	formatHours(obj)	
	if(trim(document.getElementById('_ctl0_txtTimeFrom').value) != "" && trim(document.getElementById('_ctl0_txtTimeTo').value) != ""){
		var strFHour = parseInt(document.getElementById('_ctl0_txtTimeFrom').value.substring(0,2),10);
		var strFMinute = parseInt(document.getElementById('_ctl0_txtTimeFrom').value.substring(3,5),10);
		var strTHour = parseInt(document.getElementById('_ctl0_txtTimeTo').value.substring(0,2),10);
		var strTMinute = parseInt(document.getElementById('_ctl0_txtTimeTo').value.substring(3,5),10);		
		
		var strFTime = strFHour*60 + strFMinute;
		var strTTime = strTHour*60 + strTMinute;
		if(strFTime > strTTime)
			strTTime = (strTHour + 24)*60 + strTMinute;		
		document.getElementById('_ctl0_txtHours').value = Math.round((strTTime - strFTime)/parseFloat(60)*10)/10;
		//document.getElementById('_ctl0_txtAmount').value = parseInt(document.getElementById('_ctl0_txtHours').value)
		//parseInt(document.getElementById('_ctl0_txtBasicSalary').value)/
	}
}
function validform(){
	if(checkisnull('HR_EmpHeader_txtEmpID')==false)  return false;
	if(checkisnull('txtOTDate')==false)  return false;
	if(checkisnull('txtTimeFrom')==false)  return false;
	if(checkisnull('txtTimeTo')==false)  return false;
	if(checkisnull('txtHours')==false)  return false;
	if(checkisnull('txtPRMonth')==false)  return false;
	if(checkisnull('cboOTTypeCode')==false)  return false;
	if(checkisnull('cboChargeBudgetCode')==false)  return false;
	return true;
}
function checkdelete()
{
	if(GridCheck('_ctl0_grdOvertime',3,1,'chkSelect')==false)
	{
		alert('Please select at least one record before delete');
		return false;
	}
	if(confirm('Delete checked records, are you sure?')==false){
		return false;
	}
}

function checkisnull(obj)
	{
		if(document.getElementById('_ctl0_' + obj).value=="")
			{
				alert('Data not left blank, please input data!');
				document.getElementById('_ctl0_' + obj).focus();
				return false;
			}
		else
			return true;
	}
//-->
</SCRIPT>
