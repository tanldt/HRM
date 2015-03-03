<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../Include/EmpHeaderSearch.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ManageHouseList.ascx.cs" Inherits="iHRPCore.MdlPR.ManageHouseList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
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
					<TD width="16%"><asp:label id="lblFromMonth" runat="server" CssClass="labelRequire">From month</asp:label></TD>
					<TD width="26%">&nbsp;
						<asp:textbox id="txtFromMonth" onblur="javascript:CheckMonthYear(this)" runat="server" CssClass="input"
							Width="75px" MaxLength="7"></asp:textbox></TD>
					<TD width="10%"></TD>
					<TD width="10%"><asp:label id="lblToDate" runat="server" CssClass="labelRequire">To month</asp:label></TD>
					<TD width="30%">&nbsp;
						<asp:textbox id="txtToMonth" onblur="javascript:CheckMonthYear(this)" runat="server" CssClass="input"
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
					<TD align="center" width="*"><asp:linkbutton id="btnSearch" accessKey="E" runat="server" CssClass="btnSearch" ToolTip="Alt+ S">Search</asp:linkbutton>&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnDelete" accessKey="D" runat="server" CssClass="btnDelete" ToolTip="Alt+D"
							Visible="False">Delete</asp:linkbutton>&nbsp;
						<asp:linkbutton id="btnExport" accessKey="E" runat="server" CssClass="btnExport" ToolTip="Alt+E">Export</asp:linkbutton></TD>
				</TR>
			</TABLE>
		</TD>
	</TR> <!-- end button for list form -->
	<TR>
		<TD align="center"><!-- start grid for list form -->
			<TABLE id="tblGrid" cellSpacing="0" cellPadding="1" width="99%" border="0">
				<TR>
					<TD id="tdPages" width="43%"><uc1:columnlist id="uctrlColumns" runat="server"></uc1:columnlist></TD>
					<TD id="tdSort" style="DISPLAY: none" align="right" width="*"><asp:label id="Label8" runat="server" CssClass="labelRight" Width="30px">  Col 1</asp:label><asp:dropdownlist id="cboCol1" runat="server" CssClass="combo" Width="80px"></asp:dropdownlist><asp:label id="Label10" runat="server" CssClass="labelRight" Width="30px">Col 2</asp:label>&nbsp;
						<asp:dropdownlist id="cboCol2" runat="server" CssClass="combo" Width="80px"></asp:dropdownlist><asp:label id="Label15" runat="server" CssClass="labelRight" Width="30px">Col 3</asp:label>&nbsp;
						<asp:dropdownlist id="cboCol3" runat="server" CssClass="combo" Width="80px"></asp:dropdownlist></TD>
				</TR>
				<TR id="trGrid"> <!-- start grid detail for list form -->
					<TD colSpan="2"><asp:datagrid id="grdData" runat="server" CssClass="grid" Width="100%" PageSize="20" AllowSorting="True"
							AutoGenerateColumns="False" AllowPaging="True" CellPadding="0" BorderWidth="1px" BorderColor="#3366CC"
							BackColor="White">
							<FooterStyle CssClass="gridFooter"></FooterStyle>
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle CssClass="gridHeader"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="ManageHouseID" HeaderText="ManageHouseID"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="EmpID" HeaderText="EmpID"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="&lt;b&gt;Seq&lt;/b&gt;">
									<HeaderStyle Width="3%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<%# Container.ItemIndex + 1 + grdData.PageSize*grdData.CurrentPageIndex%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="EmpCode" SortExpression="EmpCode" HeaderText="Emp Code">
									<HeaderStyle Width="13%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="EmpName" SortExpression="EmpName" HeaderText="Employee">
									<HeaderStyle Width="20%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:ButtonColumn Text="Select" DataTextField="FromMonth" HeaderText="Eff month" CommandName="EDIT"></asp:ButtonColumn>
								<asp:BoundColumn Visible="False" DataField="FromMonth" HeaderText="Eff month"></asp:BoundColumn>
								<asp:BoundColumn DataField="ToMonth" HeaderText="Exp month"></asp:BoundColumn>
								<asp:BoundColumn DataField="Amount" HeaderText="Contract worth">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Allowance" HeaderText="Allowance">
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CurrencyTypeID" HeaderText="Currency"></asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE> <!-- end grid for input form --><asp:textbox id="txtStrListID" runat="server" Visible="False"></asp:textbox></TD>
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
	if(document.getElementById('_ctl0_txtFromMonth').value=="")
	{
		GetAlertError(iTotal,DSAlert,"0003");
		document.getElementById('_ctl0_txtFromMonth').focus();
		return false;
	}
	if(document.getElementById('_ctl0_txtToMonth').value=="")
	{
		GetAlertError(iTotal,DSAlert,"0003");
		document.getElementById('_ctl0_txtToMonth').focus();
		return false;
	}
	if( document.getElementById('_ctl0_txtFromMonth').value!="" && document.getElementById('_ctl0_txtToMonth').value!="")
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
//-->
</script>
