<%@ Register TagPrefix="uc1" TagName="ColumnList" Src="../Include/ColumnList.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="MonthlyTimesheet.ascx.cs" Inherits="iHRPCore.MdlTMS.RPT_MonthlyTimesheet" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../Include/EmpHeaderSearch.ascx" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD align="center"><asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center">
			<uc1:EmpHeaderSearch id="EmpHeaderSearch1" runat="server"></uc1:EmpHeaderSearch></TD>
	</TR>
	<tr>
		<TD align="center"><!-- start detail for input form -->
			<TABLE cellSpacing="0" cellPadding="1" width="99%" border="0">
				<TR>
					<TD width="10%"><asp:label id="lbl" CssClass="labelRequire" runat="server">Date</asp:label></TD>
					<TD width="40%"><asp:textbox id="txtFromDate" onblur="CheckDate(this)" CssClass="input" runat="server" MaxLength="10"
							Width="80px"></asp:textbox><INPUT class=btnCal onclick="javascript:popUpCalendar(<%=this.txtFromDate.ClientID%>);" type=button>&nbsp;-<asp:textbox id="txtToDate" onblur="CheckDate(this)" CssClass="input" runat="server" MaxLength="10"
							Width="80px"></asp:textbox><INPUT class=btnCal onclick="javascript:popUpCalendar(<%=this.txtToDate.ClientID%>);" type=button></TD>
					<TD width="10%"><asp:label id="lblLeaveType" CssClass="label" runat="server" Width="80px">LeaveType</asp:label></TD>
					<TD><asp:dropdownlist id="cboLSLeaveTypeCode" CssClass="select" runat="server" Width="100%"></asp:dropdownlist></TD>
				</TR>
			</TABLE> <!-- end button for input form -->
			<div style="DISPLAY:none">
				<asp:radiobuttonlist id="optLeave" runat="server" Width="264px" RepeatDirection="Horizontal">
					<asp:ListItem Value="1" Selected="True">Leave application</asp:ListItem>
					<asp:ListItem Value="0">Leave cancellation</asp:ListItem>
				</asp:radiobuttonlist>
			</div>
		</TD>
	</tr>
	<TR>
		<TD vAlign="top" noWrap align="center">
			<HR align="center" width="100%">
		</TD>
	</TR>
	<TR>
		<TD noWrap align="center">
			<TABLE id="tblButton" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR vAlign="top">
					<TD vAlign="middle" width="15%"></TD>
					<TD align="center" width="*"><asp:linkbutton id="btnSearch" CssClass="btnSearch" runat="server">Search</asp:linkbutton>&nbsp;
						<div style="DISPLAY:none">
							<asp:linkbutton id="Linkbutton1" accessKey="E" runat="server" CssClass="btnExport">Save</asp:linkbutton>&nbsp;
						</div>
						<asp:linkbutton id="Linkbutton2" accessKey="E" runat="server" CssClass="btnExport">Delete</asp:linkbutton>&nbsp;
						<asp:linkbutton id="btnExport" accessKey="E" CssClass="btnExport" runat="server" ToolTip="Alt+E, Export data in grid to excel file">Export</asp:linkbutton>&nbsp;
						<asp:linkbutton id="btnImport" accessKey="E" runat="server" CssClass="btnExport" Visible="False">Import</asp:linkbutton></TD>
				</TR>
				<TR>
					<TD id="tdPages" noWrap colSpan="2"><uc1:columnlist id="uctrlColumns" runat="server"></uc1:columnlist></TD>
				</TR>
			</TABLE>
			<asp:datagrid id="dtgList_Approval" CssClass="grid" runat="server" Width="100%" AllowPaging="True"
				BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False"
				AllowSorting="True" PageSize="15" HorizontalAlign="Center">
				<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
				<ItemStyle CssClass="gridItem"></ItemStyle>
				<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
				<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
				<Columns>
					<asp:BoundColumn Visible="False" DataField="LeaveRecordID" HeaderText="LeaveRecordID">
						<HeaderStyle Width="5%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="EmpID" HeaderText="EmpID"></asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="LSWorkPointID" HeaderText="LSWorkPointID"></asp:BoundColumn>
					<asp:TemplateColumn HeaderText="Seq">
						<ItemTemplate>
							<%# Container.ItemIndex + 1%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="EmpCode" SortExpression="EmpCode" HeaderText="ID">
						<ItemStyle HorizontalAlign="Center" Width="12%"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="NickName" HeaderText="Short Name">
						<ItemStyle Width="10%"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="EmpName" SortExpression="EmpName" HeaderText="Full Name">
						<ItemStyle Width="25%"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="LeaveTypeName" HeaderText="Leave Type">
						<ItemStyle Width="20%"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="FromDate" SortExpression="FromDate" HeaderText="From Date">
						<ItemStyle HorizontalAlign="Center" Width="12%"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="ToDate" SortExpression="ToDate" HeaderText="To Date">
						<ItemStyle HorizontalAlign="Center" Width="12%"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="LeaveTaken" SortExpression="LeaveTaken" HeaderText="Total">
						<ItemStyle HorizontalAlign="Center" Width="9%"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="StatusMsg" HeaderText="Status"></asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="JobCoverBy" HeaderText="JobCoverBy"></asp:BoundColumn>
					<asp:ButtonColumn Text="Edit" HeaderText="Edit" CommandName="EDIT">
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:ButtonColumn>
					<asp:TemplateColumn HeaderText="Select">
						<HeaderStyle Width="2%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<HeaderTemplate>
							<asp:CheckBox id="chkSelectAll" onclick="CheckAll('_ctl0_dtgList_Approval__ctl2_chkSelectAll','_ctl0_dtgList_Approval',3,1,'chkSelect')"
								runat="server" CssClass="gridFooter"></asp:CheckBox>
						</HeaderTemplate>
						<ItemTemplate>
							<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
			</asp:datagrid></TD>
	</TR>
	<!-- end button for input form -->
	<TR>
		<TD align="center"><!-- start grid for input form -->
			<TABLE id="tblGrid" cellSpacing="0" cellPadding="1" width="98%" border="0">
				<TR id="trGrid"> <!-- start grid detail for input form -->
					<TD colSpan="2"></TD>
				</TR>
			</TABLE> <!-- end grid for input form --></TD>
	</TR>
</TABLE>
<script>
	function CheckValid()
	{
		if (document.getElementById("_ctl0_txtFromDate").value == "")
		{
			//alert('Input from date to report');
			GetAlertError(iTotal,DSAlert,"LRL_0001");
			document.getElementById("_ctl0_txtFromDate").focus();
			return false;
		}
		if (document.getElementById("_ctl0_txtToDate").value == "")
		{
			//alert('Input to date to report');
			GetAlertError(iTotal,DSAlert,"LRL_0002");
			document.getElementById("_ctl0_txtToDate").focus();
			return false;
		}
		if (FromSmallToDate(document.getElementById("_ctl0_txtFromDate"),document.getElementById("_ctl0_txtToDate")) == false)
		{
			//alert("From date must less than to date");
			GetAlertError(iTotal,DSAlert,"0007");
			document.getElementById("_ctl0_txtToDate").focus();
			return false;
		}
	}
	function ShowExcelSelectPage()
	{		
		window.open('./MdlTMS/FileSelect.aspx?tpl=../TemplateExcel/Leave_FileSelect.xls&Store=TMS_spfrmLeave&Act=SaveImport','SelectFile','status=1,toolbar=0,scrollbars=1,resizable=0,alwaysRaised=Yes, top=20, left=30, width=800, height=600,1 ,align=center');
		return false;
	}
</script>
