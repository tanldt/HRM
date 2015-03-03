<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../Include/EmpHeaderSearch.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="UnfrequentIncomeList.ascx.cs" Inherits="iHRPCore.MdlPR.UnfrequentIncomeList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
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
					<TD width="20%"><asp:label id="lblFromDate" runat="server" CssClass="labelRequire">From date</asp:label></TD>
					<TD width="26%"><asp:textbox id="txtFromDate" onblur="JavaScript:CheckDate(this)" runat="server" CssClass="input"
							Width="100px"></asp:textbox>&nbsp;<INPUT class="btnCal" onclick="javascript:popUpCalendar(<%= this.txtFromDate.ClientID %>)" type="button"></TD>
					<TD width="10%"></TD>
					<TD width="10%"><asp:label id="lblToDate" runat="server" CssClass="labelRequire">To date</asp:label></TD>
					<TD width="30%"><asp:textbox id="txtToDate" onblur="JavaScript:CheckDate(this)" runat="server" CssClass="input"
							Width="100px"></asp:textbox>&nbsp;<INPUT class="btnCal" onclick="javascript:popUpCalendar(<%= this.txtToDate.ClientID %>)" type="button"></TD>
				</TR>
				<TR vAlign="top">
					<TD><asp:label id="lblUNType" runat="server" CssClass="label">Unfrequent Income Type</asp:label></TD>
					<TD><asp:dropdownlist id="cboUnfrequentIncomeTypeID" runat="server" CssClass="combo" Width="176px"></asp:dropdownlist></TD>
					<TD></TD>
					<TD><asp:label id="lblPRMonth" runat="server" CssClass="label">PR month</asp:label></TD>
					<TD><asp:textbox id="txtMonth" onblur="javascript:CheckMonthYear(this)" runat="server" CssClass="input"
							Width="100px"></asp:textbox></TD>
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
					<TD align="center" width="*"><asp:linkbutton id="btnSearch" accessKey="E" runat="server" CssClass="btnSearch" ToolTip="Alt+ S">Search</asp:linkbutton>&nbsp;&nbsp;&nbsp;
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
					<TD width="43%" id="tdPages">
						<uc1:ColumnList id="uctrlColumns" runat="server"></uc1:ColumnList>
					</TD>
					<TD id="tdSort" style="DISPLAY: none" align="right" width="*"><asp:label id="Label8" runat="server" CssClass="labelRight" Width="30px">  Col 1</asp:label><asp:dropdownlist id="cboCol1" runat="server" CssClass="combo" Width="80px"></asp:dropdownlist><asp:label id="Label10" runat="server" CssClass="labelRight" Width="30px">Col 2</asp:label>&nbsp;
						<asp:dropdownlist id="cboCol2" runat="server" CssClass="combo" Width="80px"></asp:dropdownlist><asp:label id="Label15" runat="server" CssClass="labelRight" Width="30px">Col 3</asp:label>&nbsp;
						<asp:dropdownlist id="cboCol3" runat="server" CssClass="combo" Width="80px"></asp:dropdownlist></TD>
				</TR>
				<TR id="trGrid"> <!-- start grid detail for list form -->
					<TD colSpan="2">
						<asp:datagrid id="grdData" CssClass="grid" runat="server" Width="100%" PageSize="20" AllowSorting="True"
							AutoGenerateColumns="False" AllowPaging="True" CellPadding="0" BorderWidth="1px" BorderColor="#3366CC"
							BackColor="White">
							<FooterStyle CssClass="gridFooter"></FooterStyle>
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle CssClass="gridHeader"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="UnfrequentIncomeID" HeaderText="UnfrequentIncomeID"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="EmpID" HeaderText="EmpID"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" HeaderText="BonusID"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Date">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<HeaderTemplate>
										<asp:CheckBox id="chkSelectAll" runat="server" CssClass="checkbox" onclick="CheckAll('_ctl0_grdData__ctl2_chkSelectAll','_ctl0_grdData',3,1,'chkSelect')"></asp:CheckBox>
									</HeaderTemplate>
									<ItemTemplate>
										<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="&lt;b&gt;Seq&lt;/b&gt;">
									<HeaderStyle Width="3%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<%# Container.ItemIndex + 1 + grdData.PageSize*grdData.CurrentPageIndex%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:ButtonColumn DataTextField="Date" HeaderText="Date" CommandName="EDIT">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:ButtonColumn>
								<asp:BoundColumn DataField="EmpCode" SortExpression="EmpCode" HeaderText="Emp Code">
									<HeaderStyle Width="10%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="EmpName" SortExpression="EmpName" HeaderText="Employee">
									<HeaderStyle Width="15%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="UnfrequentIncomeTypeName" SortExpression="UnfrequentIncomeTypeName" HeaderText="Type">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Month" HeaderText="PR month">
									<HeaderStyle Width="10%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Amount" HeaderText="Amount">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CurrencyName" HeaderText="Currency">
									<HeaderStyle Width="5%"></HeaderStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE> <!-- end grid for input form -->
			<asp:TextBox id="txtStrListID" runat="server" Visible="False"></asp:TextBox></TD>
	</TR>
</TABLE>
<script language="javascript">
<!--
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
function checkKey(){	
	if(window.event.keyCode==13){		
		document.getElementById('_ctl0_btnRowNumber').click();		
		event.returnValue=false;
		event.cancel = true;		
	}
}
function goSearch()
{
	if(document.getElementById('_ctl0_txtFromDate').value=="")
	{
		GetAlertError(iTotal,DSAlert,"0003");
		document.getElementById('_ctl0_txtFromDate').focus();
		return false;
	}
	if(document.getElementById('_ctl0_txtToDate').value=="")
	{
		GetAlertError(iTotal,DSAlert,"0003");
		document.getElementById('_ctl0_txtToDate').focus();
		return false;
	}
	if(document.getElementById('_ctl0_txtFromDate').value!="" && document.getElementById('_ctl0_txtToDate').value!="")
	{
		if( IsSmallerOrEqual(document.getElementById('_ctl0_txtFromDate').value,document.getElementById('_ctl0_txtToDate').value) == false)
		{		
			GetAlertError(iTotal, DSAlert, "0007");
			document.getElementById('_ctl0_txtFromDate').focus();
			return false;
		}
	}
	return true;
}


//-->
</script>
