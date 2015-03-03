<%@ Register TagPrefix="uc" TagName="EmpHeader" Src="../Include/EmpHeader.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="WorkingBackground.ascx.cs" Inherits="iHRPCore.MdlHR.WorkingBackground" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="97%" border="0">
	<TR>
		<TD><asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center"><uc:empheader id="HR_EmpHeader" runat="server"></uc:empheader></TD>
	</TR>
	<tr>
		<TD align="center"><!-- start detail for input form -->
			<TABLE cellSpacing="0" cellPadding="1" width="99%" border="0">
				<TR>
					<TD width="15%"><asp:label id="lblFromDate" runat="server" CssClass="labelRequire">From Date</asp:label></TD>
					<TD width="32%"><asp:textbox id="txtFromDate" onblur="JavaScript:CheckDate(this)" runat="server" CssClass="input"
							MaxLength="10" Width="80px"></asp:textbox>&nbsp;<INPUT class=btnCal onclick="javascript:popUpCalendar(<%= this.txtFromDate.ClientID %>)" type=button></TD>
					<TD width="4%"></TD>
					<TD width="15%"><asp:label id="lblToDate" runat="server" CssClass="labelRequire">To Date</asp:label></TD>
					<TD width="37%"><asp:textbox id="txtToDate" onblur="JavaScript:CheckDate(this)" runat="server" CssClass="input"
							MaxLength="10" Width="80px"></asp:textbox>&nbsp;<INPUT class=btnCal onclick="javascript:popUpCalendar(<%= this.txtToDate.ClientID %>)" type=button></TD>
				</TR>
				<TR>
					<TD width="15%"><asp:label id="lblWorkFor" runat="server" CssClass="labelRequire" Width="100%"> Company</asp:label></TD>
					<TD vAlign="top" width="32%"><asp:textbox id="txtWorkFor" runat="server" CssClass="input" MaxLength="100" Width="100%"></asp:textbox></TD>
					<TD width="4%"></TD>
					<TD width="15%"><asp:label id="lblPosition" runat="server" CssClass="label" Width="100%"> Job level</asp:label></TD>
					<TD vAlign="top" width="37%"><asp:textbox id="txtPosition" runat="server" CssClass="input" MaxLength="100" Width="100%"></asp:textbox></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblAddress" runat="server" CssClass="label" Width="100%">Address</asp:label></TD>
					<TD vAlign="top"><asp:textbox id="txtAddress" runat="server" CssClass="input" MaxLength="150" Width="100%"></asp:textbox></TD>
					<TD></TD>
					<TD><asp:label id="lblPhone" runat="server" CssClass="label">Telephone</asp:label></TD>
					<TD><asp:textbox id="txtTelephone" runat="server" CssClass="input" MaxLength="20" Width="80px"></asp:textbox></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblStartSalary" runat="server" CssClass="label">Start Salary</asp:label></TD>
					<TD><asp:textbox id="txtStartSalary" onblur="JavaScript:checkNumeric(this)" runat="server" CssClass="input"
							MaxLength="9" Width="80px"></asp:textbox></TD>
					<TD></TD>
					<TD><asp:label id="lblLastSalary" runat="server" CssClass="label">Last Salary</asp:label></TD>
					<TD><asp:textbox id="txtLastSalary" onblur="JavaScript:checkNumeric(this)" runat="server" CssClass="input"
							MaxLength="9" Width="80px"></asp:textbox></TD>
				</TR>
				<TR id="trAdContact" runat="server">
					<TD vAlign="top"><asp:checkbox id="chkAdContact" runat="server" CssClass="chkAdmin" Checked="True"></asp:checkbox><asp:label id="Label1" runat="server" CssClass="label" Width="100%">Contact Name</asp:label></TD>
					<TD vAlign="top"><asp:textbox id="txtContactName" style="POSITION: absolute" runat="server" CssClass="input" MaxLength="50"
							Width="100%"></asp:textbox></TD>
					<TD></TD>
					<TD><asp:label id="Label2" runat="server" CssClass="label">Contact Position</asp:label></TD>
					<TD vAlign="top"><asp:textbox id="txtContactPosition" style="POSITION: absolute" runat="server" CssClass="input"
							MaxLength="100" Width="100%"></asp:textbox></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblDuty" runat="server" CssClass="label" Width="100%">Duty</asp:label></TD>
					<TD vAlign="top" colSpan="4"><asp:textbox id="txtDuty" style="POSITION: absolute" runat="server" CssClass="input" MaxLength="100"
							Width="100%"></asp:textbox></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblChangeReason" runat="server" CssClass="label" Width="100%"> Change Reason</asp:label></TD>
					<TD vAlign="top" colSpan="4"><asp:textbox id="txtChangeReason" style="POSITION: absolute" runat="server" CssClass="input"
							MaxLength="100" Width="100%"></asp:textbox></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblNote" runat="server" CssClass="label" Width="100%">Note</asp:label></TD>
					<TD vAlign="top" colSpan="4"><asp:textbox id="txtNote" style="POSITION: absolute" runat="server" CssClass="input" MaxLength="255"
							Width="100%" Rows="2"></asp:textbox></TD>
				</TR>
			</TABLE> <!-- end button for input form -->
		</TD>
	</tr>
	<tr style="DISPLAY: none">
		<TD width="15%"><asp:textbox id="txtWorkingBackgroundID" runat="server" CssClass="input" Width="100%"></asp:textbox></TD>
	</tr>
	<TR>
		<TD vAlign="top" noWrap align="center" height="20">
			<HR align="center" width="95%">
		</TD>
	</TR>
	<TR>
		<TD noWrap align="center">
			<TABLE id="tblButton" cellSpacing="0" cellPadding="0" width="99%" border="0">
				<TR vAlign="top" height="30">
					<TD vAlign="middle" width="15%"><asp:checkbox id="chkShowGrid" accessKey="G" onclick="javascript:collapse('_ctl0_tdGrid');" runat="server"
							CssClass="checkbox" Checked="True" Text="Show grid" ToolTip="Alt+G"></asp:checkbox></TD>
					<TD align="center" width="*"><asp:linkbutton id="btnAddNew" accessKey="N" runat="server" CssClass="btnAddnew" ToolTip="Alt+N">AddNew</asp:linkbutton>&nbsp;&nbsp;
						<asp:linkbutton id="btnSave" accessKey="S" runat="server" CssClass="btnSave" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnList" accessKey="L" runat="server" CssClass="btnList" ToolTip="Alt + L">List</asp:linkbutton>&nbsp;&nbsp;
						<asp:linkbutton id="btnDelete" accessKey="D" runat="server" CssClass="btnDelete" ToolTip="Alt + D">Delete</asp:linkbutton>&nbsp;&nbsp;
						<asp:linkbutton id="btnExport" accessKey="E" runat="server" CssClass="btnExport" ToolTip="Alt+E, Export data in grid to excel file">Export</asp:linkbutton></TD>
				</TR>
			</TABLE>
		</TD>
	</TR> <!-- end button for input form -->
	<TR>
		<TD id="tdGrid" align="center" runat="server"><!-- start grid for input form -->
			<TABLE id="tblGrid" cellSpacing="0" cellPadding="1" width="98%" border="0">
				<TR>
					<TD width="40%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
					<TD align="right" width="*">&nbsp;&nbsp;&nbsp;
					</TD>
				</TR>
				<TR id="trGrid"> <!-- start grid detail for input form -->
					<TD colSpan="2"><asp:datagrid id="grdWorkingBackground" runat="server" CssClass="grid" Width="100%" BackColor="White"
							BorderColor="#3366CC" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False" AllowSorting="True">
							<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="WorkingBackgroundID" HeaderText="Seq">
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
								<asp:ButtonColumn DataTextField="FromDate" HeaderText="From Date" CommandName="EDIT"></asp:ButtonColumn>
								<asp:BoundColumn DataField="ToDate" HeaderText="To Date">
									<HeaderStyle Width="15%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="WorkFor" HeaderText="Company">
									<HeaderStyle Width="25%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Position" HeaderText="Job Level">
									<HeaderStyle Width="15%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ChangeReason" HeaderText="Change Reason">
									<HeaderStyle Width="20%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
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
function validform(){	
	if(checkisnull('HR_EmpHeader_txtEmpID')==false)  return false;
	if(checkisnull('txtFromDate')==false)  return false;
	if(checkisnull('txtToDate')==false)  return false;
	if(checkisnull('txtWorkFor')==false)  return false;
	//if(checkisnull('txtPosition')==false)  return false;
	if(document.getElementById('_ctl0_txtFromDate').value!="" ||document.getElementById('_ctl0_txtToDate').value!="")
	{
		if(FromSmallNow(document.getElementById('_ctl0_txtFromDate')) == false)
			{
						//alert('Date of birth must be less than now');
						GetAlertError(iTotal,DSAlert,"0011");
						document.getElementById('_ctl0_txtFromDate').focus();
						return false;
			}	
		if(FromSmallToDate(document.getElementById('_ctl0_txtFromDate'),document.getElementById('_ctl0_txtToDate')) == false)
		{
			GetAlertError(iTotal,DSAlert,"0007")
			document.getElementById('_ctl0_txtToDate').focus();
			return false;
		}
	}
	return true;
}
function checkdelete()
{
	if(GridCheck('_ctl0_grdWorkingBackground',2,1,'chkSelect')==false)
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
//-->
</script>
