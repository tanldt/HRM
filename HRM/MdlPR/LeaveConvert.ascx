<%@ Register TagPrefix="uc1" TagName="ColumnList" Src="../Include/ColumnList.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="LeaveConvert.ascx.cs" Inherits="iHRPCore.MdlPR.LeaveConvert" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
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
			<TABLE cellSpacing="0" cellPadding="1" width="99%" border="0">
				<TR>
					<TD style="HEIGHT: 22px" width="11%"><asp:label id="Label1" CssClass="LabelRequire" runat="server"> Year</asp:label></TD>
					<TD style="HEIGHT: 22px" width="26%"><asp:textbox id="txtYear" onblur="javascript:CheckNumber(this)" CssClass="input" runat="server"
							Width="40px"></asp:textbox></TD>
					<TD style="HEIGHT: 22px" width="10%"><asp:label id="Label9" CssClass="LabelRequire" runat="server">Convert to</asp:label></TD>
					<TD style="HEIGHT: 22px" width="30%"><asp:dropdownlist id="cboMethodID" CssClass="select" runat="server" Width="80px">
							<asp:ListItem Selected="True"></asp:ListItem>
							<asp:ListItem Value="Cash">Cash</asp:ListItem>
							<asp:ListItem Value="Flex">Flex</asp:ListItem>
						</asp:dropdownlist></TD>
					<TD style="HEIGHT: 22px" width="9%"><asp:label id="Label2" CssClass="LabelRequire" runat="server">Ref month</asp:label></TD>
					<TD style="HEIGHT: 22px" width="14%"><asp:textbox id="txtRefMonth" onblur="javascript:CheckMonthYear(this)" CssClass="input" runat="server"
							Width="75px" MaxLength="7"></asp:textbox></TD>
				</TR>
				<TR>
					<TD><asp:label id="Label10" CssClass="label" runat="server">Days remain</asp:label></TD>
					<TD><asp:label id="lblRemain" CssClass="labelData" runat="server"></asp:label>&nbsp;
						<asp:label id="Label12" CssClass="label" runat="server">Carry next year</asp:label>&nbsp;
						<asp:label id="lblCarry" CssClass="labelData" runat="server"></asp:label></TD>
					<TD noWrap><asp:label id="Label11" CssClass="label" runat="server">Days convert</asp:label></TD>
					<TD>&nbsp;<asp:label id="lblConvert" CssClass="labelData" runat="server"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:label id="Label13" CssClass="label" runat="server">Amount</asp:label>&nbsp;
						<asp:label id="lblAmount" CssClass="labelData" runat="server"></asp:label><asp:label id="lblUnit" CssClass="label" runat="server">VND</asp:label></TD>
					<TD noWrap><asp:label id="Label3" CssClass="LabelRequire" runat="server">PR month</asp:label></TD>
					<TD><asp:textbox id="txtPRMonth" onblur="javascript:CheckMonthYear(this)" CssClass="input" runat="server"
							Width="75px" MaxLength="7"></asp:textbox></TD>
				</TR>
				<TR>
					<TD><asp:label id="Label19" CssClass="label" runat="server">Remarks</asp:label></TD>
					<TD colSpan="6"><asp:textbox id="txtNote" CssClass="input" runat="server" Width="97%"></asp:textbox></TD>
				</TR>
			</TABLE>
			<!-- end button for list form --></TD>
	</TR>
	<TR>
		<TD vAlign="top" noWrap align="center" height="10">
			<HR align="center" width="95%">
		</TD>
	</TR>
	<TR> <!-- start button for list form -->
		<TD noWrap align="center">
			<TABLE id="tblButton" cellSpacing="0" cellPadding="0" width="98%" border="0">
				<TR vAlign="middle" height="35">
					<TD vAlign="middle" width="15%"></TD>
					<TD align="center" width="*"><asp:linkbutton id="btnGetLeave" accessKey="E" CssClass="btnSearch" runat="server" ToolTip="Alt+ S"> Convert</asp:linkbutton>&nbsp;&nbsp;<asp:linkbutton id="btnSearch" accessKey="E" CssClass="btnSearch" runat="server" ToolTip="Alt+ S">Search</asp:linkbutton>&nbsp;&nbsp;
						<asp:linkbutton id="btnSave" accessKey="S" CssClass="btnSave" runat="server" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp;
						<asp:linkbutton id="btnDelete" accessKey="D" CssClass="btnDelete" runat="server" ToolTip="Alt+D">Delete</asp:linkbutton>&nbsp;
						<asp:linkbutton id="btnImport" accessKey="I" CssClass="btnImport" runat="server" ToolTip="Alt+I, Import data from excel file same format with export file">Import</asp:linkbutton>&nbsp;
						<asp:linkbutton id="Linkbutton2" accessKey="E" CssClass="btnExport" runat="server" ToolTip="Alt+E, Export data in grid to excel file">Export</asp:linkbutton></TD>
				</TR>
			</TABLE>
		</TD>
	</TR> <!-- end button for list form -->
	<TR>
		<TD align="center"><!-- start grid for input form -->
			<TABLE id="tblGrid" cellSpacing="0" cellPadding="1" width="98%" border="0">
				<TR>
					<TD id="tdPages" width="43%"><uc1:columnlist id="uctrlColumns" runat="server"></uc1:columnlist></TD>
					<TD id="tdSort" style="DISPLAY: none" align="right" width="*"><asp:label id="Label8" CssClass="labelRight" runat="server" Width="30px">  Col 1</asp:label><asp:dropdownlist id="cboCol1" CssClass="combo" runat="server" Width="80px"></asp:dropdownlist><asp:label id="Label7" CssClass="labelRight" runat="server" Width="30px">Col 2</asp:label>&nbsp;
						<asp:dropdownlist id="cboCol2" CssClass="combo" runat="server" Width="80px"></asp:dropdownlist><asp:label id="Label15" CssClass="labelRight" runat="server" Width="30px">Col 3</asp:label>&nbsp;
						<asp:dropdownlist id="cboCol3" CssClass="combo" runat="server" Width="80px"></asp:dropdownlist></TD>
				</TR>
				<TR id="trGrid"> <!-- start grid detail for input form -->
					<TD colSpan="2"><asp:datagrid id="grdLeaveConvert" CssClass="grid" runat="server" Width="100%" AllowPaging="True"
							ShowFooter="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="False" CellPadding="0" BorderWidth="1px"
							BorderColor="#3366CC" BackColor="White">
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle CssClass="gridHeader"></HeaderStyle>
							<FooterStyle CssClass="gridFooter"></FooterStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="LeaveConvertID" HeaderText="LeaveConvertID"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Seq">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<%# Container.ItemIndex + 1%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Year" SortExpression="Year" HeaderText="Year">
									<HeaderStyle Width="5%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="EmpID" SortExpression="EmpID" HeaderText="Emp ID">
									<HeaderStyle Width="10%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="EmpName" SortExpression="EmpName" HeaderText="Full name">
									<HeaderStyle Width="23%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="JobRelated" SortExpression="JobRelated" HeaderText="Job related">
									<HeaderStyle Width="25%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="RefMonth" SortExpression="RefMonth" HeaderText="Ref Month">
									<HeaderStyle Width="14%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="BalanceAlPrevYear" SortExpression="BalanceAlPrevYear" HeaderText="Convert">
									<HeaderStyle Width="8%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Amount" HeaderText="Amount" DataFormatString="{0:#,###}">
									<HeaderStyle Width="14%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MethodID" SortExpression="MethodID" HeaderText="Unit">
									<HeaderStyle Width="6%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="Note" HeaderText="Note"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PRMonth" HeaderText="PR Month"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Sel">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
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
	if(checkisnull('txtYear')==false)  return false;
	if(checkisnull('cboMethodID')==false)  return false;
	if(checkisnull('txtRefMonth')==false)  return false;	
	if(checkisnull('txtPRMonth')==false)  return false;
	return true;
}
function checkisnull(obj){
	if(document.getElementById('_ctl0_' + obj).value=="")
		{
			alert('Data not left blank, please input data!');
			document.getElementById('_ctl0_' + obj).focus();
			return false;
		}
	else
		return true;
}
function checkdelete(){
	if(GridCheck('_ctl0_grdLeaveConvert',3,1,'chkSelect')==false)
	{
		alert('Please select at least one record before delete');
		return false;
	}
	if(confirm('Delete checked records, are you sure?')==false){
		return false;
	}
}
//-->
</script>
