<%@ Control Language="c#" AutoEventWireup="false" Codebehind="SalaryAdjustList.ascx.cs" Inherits="iHRPCore.MdlPR.SalaryAdjustList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="ColumnList" Src="../Include/ColumnList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../Include/EmpHeaderSearch.ascx" %>
<script language="javascript" src="../Include/common.js">
</script>
<script language="javascript">
</script>
<TABLE cellSpacing="0" cellPadding="0" width="99%" align="center" border="0">
	<TR>
		<TD noWrap align="left"><asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD noWrap align="center"><uc1:empheadersearch id="EmpHeaderSearch1" runat="server"></uc1:empheadersearch></TD>
	</TR>
	<TR>
		<TD align="center">
			<!-- start detail for list form -->
			<TABLE cellSpacing="0" cellPadding="1" width="100%" border="0">
				<TR>
					<TD width="11%"><asp:label id="Label1" CssClass="label" runat="server">Add/Sub</asp:label></TD>
					<TD width="26%"><asp:radiobuttonlist id="optAddSubtract" CssClass="option" runat="server" RepeatDirection="Horizontal">
							<asp:ListItem Value=" " Selected="True">All</asp:ListItem>
							<asp:ListItem Value="1">Add</asp:ListItem>
							<asp:ListItem Value="0">Subtract</asp:ListItem>
						</asp:radiobuttonlist></TD>
					<TD width="10%"><asp:label id="Label2" CssClass="label" runat="server"> Adj type</asp:label></TD>
					<TD width="186" style="WIDTH: 186px"><asp:dropdownlist id="cboLSSalaryAdjustCode" CssClass="select" runat="server" Width="120px"></asp:dropdownlist></TD>
					<TD width="200"><asp:label id="Label3" CssClass="label" runat="server">PR month</asp:label></TD>
					<TD width="14%"><asp:textbox id="txtPRMonth" onblur="javascript:CheckMonthYear(this)" CssClass="input" runat="server"
							Width="75px" MaxLength="7"></asp:textbox></TD>
				</TR>
			</TABLE>
			<!-- end button for list form --></TD>
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
					<TD align="center" width="*"><asp:linkbutton id="btnSearch" accessKey="E" CssClass="btnSearch" runat="server" ToolTip="Alt+ S">Search</asp:linkbutton>&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnDelete" accessKey="D" CssClass="btnDelete" runat="server" ToolTip="Alt+D">Delete</asp:linkbutton>&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnExport" accessKey="E" CssClass="btnExport" runat="server" ToolTip="Alt+E">Export</asp:linkbutton></TD>
				</TR>
			</TABLE>
		</TD>
	</TR> <!-- end button for list form -->
	<TR>
		<TD align="center"><!-- start grid for list form -->
			<TABLE id="tblGrid" cellSpacing="0" cellPadding="1" width="99%" border="0">
				<TR>
					<TD id="tdPages" width="43%">
						<uc1:ColumnList id="uctrlColumns" runat="server"></uc1:ColumnList>
					</TD>
					<TD id="tdSort" style="DISPLAY: none" align="right" width="*"><asp:label id="Label8" CssClass="labelRight" runat="server" Width="30px">  Col 1</asp:label><asp:dropdownlist id="cboCol1" CssClass="combo" runat="server" Width="80px"></asp:dropdownlist><asp:label id="Label7" CssClass="labelRight" runat="server" Width="30px">Col 2</asp:label>&nbsp;
						<asp:dropdownlist id="cboCol2" CssClass="combo" runat="server" Width="80px"></asp:dropdownlist><asp:label id="Label15" CssClass="labelRight" runat="server" Width="30px">Col 3</asp:label>&nbsp;
						<asp:dropdownlist id="cboCol3" CssClass="combo" runat="server" Width="80px"></asp:dropdownlist></TD>
				</TR>
				<TR id="trGrid"> <!-- start grid detail for list form -->
					<TD colSpan="2"><asp:datagrid id="grdSalaryAdjust" CssClass="grid" runat="server" Width="100%" BackColor="White"
							BorderColor="#3366CC" BorderWidth="1px" CellPadding="0" AllowPaging="True" AutoGenerateColumns="False"
							AllowSorting="True" PageSize="20">
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
										<%# Container.ItemIndex + 1 + grdSalaryAdjust.CurrentPageIndex*grdSalaryAdjust.PageSize%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn Visible="False" DataField="EmpID" SortExpression="EmpID" HeaderText="Emp ID">
									<HeaderStyle Width="10%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="EmpCode" SortExpression="EmpCode" HeaderText="Emp Code">
									<HeaderStyle Width="10%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="FullName" SortExpression="FullName" HeaderText="Employee">
									<HeaderStyle Width="200px"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:ButtonColumn DataTextField="PRMonth" HeaderText="Month" CommandName="EDIT">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:ButtonColumn>
								<asp:BoundColumn DataField="SalaryAdjustType" SortExpression="SalaryAdjustType" HeaderText="Type">
									<HeaderStyle Width="30%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="AddSubtract" SortExpression="AddSubtract" HeaderText="Add/Sub">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Amount" HeaderText="Amount">
									<HeaderStyle Width="12%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CurrencyName" HeaderText="Currency"></asp:BoundColumn>
								<asp:BoundColumn DataField="PIT" SortExpression="PIT" HeaderText="PIT">
									<HeaderStyle Width="6%"></HeaderStyle>
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
function checkdelete()
{
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
//-->
</script>
