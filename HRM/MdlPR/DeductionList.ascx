<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../Include/EmpHeaderSearch.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="DeductionList.ascx.cs" Inherits="iHRPCore.MdlPR.DeductionList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="ColumnList" Src="../Include/ColumnList.ascx" %>
<script language="javascript" src="../Include/common.js">
</script>
<script language="javascript">
</script>
<TABLE cellSpacing="0" cellPadding="0" width="99%" align="center" border="0">
	<TR>
		<TD noWrap align="left"><asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></TD>
	</TR>
	<TR>
		<TD noWrap align="center"><uc1:empheadersearch id="EmpHeaderSearch1" runat="server"></uc1:empheadersearch></TD>
	</TR>
	<TR>
		<TD align="center">
			<!-- start detail for list form -->
			<TABLE cellSpacing="0" cellPadding="1" width="99%" border="0">
				<TR>
					<TD width="15%"><asp:label id="Label3" runat="server" CssClass="label"> Deduction Type</asp:label></TD>
					<TD width="26%">
						<asp:dropdownlist id="cboLSDeductionCode" CssClass="combo" runat="server" Width="95%"></asp:dropdownlist></TD>
					<TD width="10%"><asp:label id="Label1" runat="server" CssClass="label">From Month</asp:label></TD>
					<TD width="30%" colSpan="3">
						<asp:textbox id="txtFromDate" onblur="javascript:CheckMonthYear(this)" CssClass="input" runat="server"
							Width="75px" MaxLength="7"></asp:textbox>&nbsp;&nbsp;
						<asp:label id="Label2" CssClass="label" runat="server">To Month</asp:label>
						<asp:textbox id="txtToDate" onblur="javascript:CheckMonthYear(this)" CssClass="input" runat="server"
							Width="75px" MaxLength="7"></asp:textbox></TD>
				</TR>
			</TABLE>
			<!-- end button for list form -->
		</TD>
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
					<TD align="center" width="*"><asp:linkbutton id="btnSearch" accessKey="E" runat="server" CssClass="btnSearch" ToolTip="Alt+ S">Search</asp:linkbutton>&nbsp; 
						&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnDelete" accessKey="D" runat="server" CssClass="btnDelete" ToolTip="Alt+D">Delete</asp:linkbutton>&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnExport" accessKey="E" runat="server" CssClass="btnExport" ToolTip="Alt+E">Export</asp:linkbutton></TD>
				</TR>
			</TABLE>
		</TD>
	</TR> <!-- end button for list form -->
	<TR>
		<TD align="center"><!-- start grid for list form -->
			<TABLE id="tblGrid" cellSpacing="0" cellPadding="1" width="99%" border="0">
				<TR>
					<TD width="43%" id="tdPages">
						<uc1:ColumnList id="uctrlColumns" runat="server"></uc1:ColumnList>
					</TD>
					<TD width="*" id="tdSort" align="right" style="DISPLAY: none">
						<asp:label id="Label8" runat="server" CssClass="labelRight" Width="30px">  Col 1</asp:label>
						<asp:dropdownlist id="cboCol1" runat="server" CssClass="combo" Width="80px"></asp:dropdownlist>
						<asp:label id="Label7" runat="server" CssClass="labelRight" Width="30px">Col 2</asp:label>&nbsp;
						<asp:dropdownlist id="cboCol2" runat="server" CssClass="combo" Width="80px"></asp:dropdownlist>
						<asp:label id="Label15" runat="server" CssClass="labelRight" Width="30px">Col 3</asp:label>&nbsp;
						<asp:dropdownlist id="cboCol3" runat="server" CssClass="combo" Width="80px"></asp:dropdownlist>
					</TD>
				</TR>
				<TR id="trGrid"> <!-- start grid detail for list form -->
					<TD colSpan="2">
						<asp:datagrid id="grdDeduction" CssClass="grid" runat="server" Width="100%" BackColor="White"
							BorderColor="#3366CC" BorderWidth="1px" CellPadding="0" AllowPaging="True" AutoGenerateColumns="False"
							AllowSorting="True" PageSize="20">
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle CssClass="gridHeader"></HeaderStyle>
							<FooterStyle CssClass="gridFooter"></FooterStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="DeductionID" HeaderText="DeductionID"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Seq">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<%# Container.ItemIndex + 1 + grdDeduction.PageSize*grdDeduction.CurrentPageIndex%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="EmpID" Visible="False" SortExpression="EmpID" HeaderText="Emp ID">
									<HeaderStyle Width="10%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="EmpCode" SortExpression="EmpCode" HeaderText="Emp Code">
									<HeaderStyle Width="15%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="FullName" SortExpression="FullName" HeaderText="Employee">
									<HeaderStyle Width="200px"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:ButtonColumn DataTextField="FromMonth" SortExpression="FromMonth" HeaderText="From" CommandName="EDIT">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<HeaderStyle Width="5%"></HeaderStyle>
								</asp:ButtonColumn>
								<asp:BoundColumn DataField="ToMonth" HeaderText="To">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="DeductionType" SortExpression="DeductionType" HeaderText="Type">
									<HeaderStyle Width="15%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="AmountPerMonth" HeaderText="Amount">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Unit" SortExpression="Unit" HeaderText="Unit">
									<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="IsGross" SortExpression="IsGross" HeaderText="Gross/NET">
									<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
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
			</TABLE> <!-- end grid for input form -->
		</TD>
	</TR>
</TABLE>
<script language="javascript">
<!--
function Search()
{
	/* if(!FromSmallToMonth(document.getElementById('_ctl0_txtFromDate'),document.getElementById('_ctl0_txtToDate')))
		{
				GetAlertError(iTotal,DSAlert,"0007");
				document.getElementById('_ctl0_txtFromDate').focus();
				return false;
		}*/
		return true;
}
function checkdelete()
{
	if(GridCheck('_ctl0_grdDeduction',3,1,'chkSelect')==false)
	{
		GetAlertTextPopUp('<%=GetText("COMMON","COMFONESELECT")%>');
		return false;
	}
	if(confirm('<%=GetText("COMMON","CONFIRMDELETE")%>')==false){
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
