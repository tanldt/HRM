<%@ Control Language="c#" AutoEventWireup="false" Codebehind="VariableBonus.ascx.cs" Inherits="iHRPCore.MdlPR.VariableBonus" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="ColumnList" Src="../Include/ColumnList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../Include/EmpHeaderSearch.ascx" %>
<script language="javascript" src="../Include/common.js">
</script>
<script language="javascript">
</script>
<TABLE cellSpacing="0" cellPadding="0" width="99%" align="center" border="0">
	<TR>
		<TD noWrap align="left"><asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center">
			<!-- start detail for list form -->
			<TABLE cellSpacing="0" cellPadding="1" width="99%" border="0">
				<TR>
					<TD width="11%"><asp:label id="Label1" runat="server" CssClass="label">Year</asp:label></TD>
					<TD width="26%"><asp:dropdownlist id="cboYear" runat="server" CssClass="select" AutoPostBack="True"></asp:dropdownlist></TD>
					<TD width="10%"></TD>
					<TD width="30%"><asp:checkbox id="chkSetup" accessKey="P" onclick="javascript:collapse('_ctl0_trSetup');" runat="server"
							CssClass="labelData" Text="Performance &amp; Bonus by Level" ToolTip="Alt+P"></asp:checkbox></TD>
					<TD width="9%"></TD>
					<TD width="14%"></TD>
				</TR>
			</TABLE>
			<!-- end button for list form --></TD>
	</TR>
	<TR id="trSetup" runat="server" style="DISPLAY: none">
		<TD noWrap align="center">
			<TABLE cellSpacing="0" cellPadding="1" width="99%" border="0">
				<TR>
					<TD width="45%"><asp:label id="Label2" runat="server" CssClass="labelData">Performance rating</asp:label></TD>
					<TD width="10%"></TD>
					<TD width="45%"><asp:label id="Label3" runat="server" CssClass="labelData">Bonus months</asp:label></TD>
				</TR>
				<TR>
					<TD vAlign="top"><asp:datagrid id="grdStaffLevel" runat="server" CssClass="grid" Width="100%" BackColor="White"
							BorderColor="#3366CC" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False" PageSize="4">
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="griditem"></ItemStyle>
							<HeaderStyle HorizontalAlign="Center" CssClass="gridheader"></HeaderStyle>
							<FooterStyle CssClass="gridFooter"></FooterStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="LSStaffLevelID" HeaderText="LSStaffLevelID"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Staff Level">
									<HeaderStyle Width="20%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton Runat="server" ID="btn" CssClass="Label" CommandName="Details">
											<%# DataBinder.Eval(Container, "DataItem.LevelType")%>
										</asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="No of months">
									<HeaderStyle Width="40%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:textbox id="txtNoOfMonth" MaxLength="3" onblur="javascript:checkNumeric(this)" Text='<%# DataBinder.Eval(Container, "DataItem.NoOfMonth")%>' CssClass="input" runat="server" Width="100px">
										</asp:textbox>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle CssClass="gridPage" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
					<TD vAlign="top" align="center"><br>
						<asp:linkbutton id="btnUpdate" accessKey="S" runat="server" CssClass="btnSave" ToolTip="ALT+S">Save</asp:linkbutton><br>
						<br>
						<asp:linkbutton id="Linkbutton3" accessKey="D" runat="server" CssClass="btnDelete" ToolTip="Alt+D"
							Visible="False">Delete</asp:linkbutton></TD>
					<TD vAlign="top"><asp:datagrid id="grdAppraisal" runat="server" CssClass="grid" Width="100%" BackColor="White"
							BorderColor="#3366CC" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False" PageSize="4">
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="griditem"></ItemStyle>
							<HeaderStyle HorizontalAlign="Center" CssClass="gridheader"></HeaderStyle>
							<FooterStyle CssClass="gridFooter"></FooterStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="LSAPPResultTypeCode" HeaderText="LSAPPResultTypeCode"></asp:BoundColumn>
								<asp:BoundColumn DataField="AppResult" HeaderText="Appraisal">
									<HeaderStyle Width="60%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Rating (%)">
									<HeaderStyle Width="40%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:textbox id="txtRate" MaxLength="5" onblur="javascript:checkNumeric(this)" CssClass="input" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Rate")%>' Width="100px"/>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle CssClass="gridPage" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD vAlign="top" colSpan="3" height="20">
						<hr width="100%">
						<INPUT id="txtID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtID"
							runat="server">
					</TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD noWrap align="center"><uc1:empheadersearch id="EmpHeaderSearch1" runat="server"></uc1:empheadersearch></TD>
	</TR>
	<TR>
		<TD vAlign="top" noWrap align="center" height="20">
			<HR align="center" width="95%">
		</TD>
	</TR>
	<TR> <!-- start button for list form -->
		<TD noWrap align="center">
			<TABLE id="tblButton" cellSpacing="0" cellPadding="0" width="98%" border="0">
				<TR vAlign="top" height="35">
					<TD vAlign="middle" width="15%"><asp:checkbox id="chkShowGrid" onclick="javascript:collapse('tblGrid');" runat="server" CssClass="checkbox"
							Text="Show grid" Checked="True"></asp:checkbox></TD>
					<TD align="center" width="*"><asp:linkbutton id="btnSearch" accessKey="E" runat="server" CssClass="btnSearch" ToolTip="Alt+ S">Search</asp:linkbutton>&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnCalculate" accessKey="C" runat="server" CssClass="Button" ToolTip="Alt+C">Calculate</asp:linkbutton>&nbsp;
						<asp:linkbutton id="btnSave" accessKey="S" runat="server" CssClass="btnSave" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp;
						<asp:linkbutton id="btnDelete" accessKey="D" runat="server" CssClass="btnDelete" ToolTip="Alt+D">Delete</asp:linkbutton>&nbsp;
						<asp:linkbutton id="btnExport" accessKey="E" runat="server" CssClass="btnExport" ToolTip="Alt+E">Export</asp:linkbutton></TD>
				</TR>
			</TABLE>
		</TD>
	</TR> <!-- end button for list form -->
	<TR>
		<TD align="center"><!-- start grid for list form -->
			<TABLE id="tblGrid" cellSpacing="0" cellPadding="1" width="99%" border="0">
				<TR>
					<TD id="tdPages" width="43%">
						<uc1:ColumnList id="uctrlColumns" runat="server"></uc1:ColumnList></TD>
					<TD id="tdSort" style="DISPLAY: none" align="right" width="*"><asp:label id="Label8" runat="server" CssClass="labelRight" Width="30px">  Col 1</asp:label><asp:dropdownlist id="cboCol1" runat="server" CssClass="combo" Width="80px"></asp:dropdownlist><asp:label id="Label7" runat="server" CssClass="labelRight" Width="30px">Col 2</asp:label>&nbsp;
						<asp:dropdownlist id="cboCol2" runat="server" CssClass="combo" Width="80px"></asp:dropdownlist><asp:label id="Label15" runat="server" CssClass="labelRight" Width="30px">Col 3</asp:label>&nbsp;
						<asp:dropdownlist id="cboCol3" runat="server" CssClass="combo" Width="80px"></asp:dropdownlist></TD>
				</TR>
				<TR id="trGrid"> <!-- start grid detail for list form -->
					<TD colSpan="2"><asp:datagrid id="grdBonus" runat="server" CssClass="grid" Width="100%" BackColor="White" BorderColor="#3366CC"
							BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False" PageSize="20" AllowSorting="True" AllowPaging="True">
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle CssClass="gridHeader"></HeaderStyle>
							<FooterStyle CssClass="gridFooter"></FooterStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="VariableBonusID" HeaderText="VariableBonusID"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Seq">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<%# Container.ItemIndex + 1 + grdBonus.PageSize*grdBonus.CurrentPageIndex%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="EmpID" SortExpression="EmpID" HeaderText="Emp ID">
									<HeaderStyle Width="8%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="FullName" SortExpression="FullName" HeaderText="Full Name">
									<HeaderStyle Width="23%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="JobRelated" SortExpression="JobRelated" HeaderText="Job related">
									<HeaderStyle Width="23%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Rate" HeaderText="Rate">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Months" HeaderText="Months">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Amount" HeaderText="Amount">
									<HeaderStyle Width="16%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Select">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Left" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE> <!-- end grid for input form --></TD>
	</TR>
</TABLE>
<script language="javascript">
<!--
function checkdelete()
{
	if(GridCheck('_ctl0_grdBonus',2,1,'chkSelect')==false)
	{
		alert('Please select at least one record before delete');
		return false;
	}
	if(confirm('Delete checked records, are you sure?')==false){
		return false;
	}
}
function checksave()
{
	if(GridCheck('_ctl0_grdBonus',2,1,'chkSelect')==false)
	{
		alert('Please select at least one record before save');
		return false;
	}
	if(confirm('save checked records, are you sure?')==false){
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
//-->
</script>
