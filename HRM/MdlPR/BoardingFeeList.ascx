<%@ Control Language="c#" AutoEventWireup="false" Codebehind="BoardingFeeList.ascx.cs" Inherits="iHRPCore.MdlPR.BoardingFeeList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="EmpHeader" Src="../Include/EmpHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ColumnList" Src="../Include/ColumnList.ascx" %>
<%@ Import Namespace ="System.Data" %>
<%@ Import Namespace ="System.Data.SqlClient" %>
<%@ Import Namespace ="iHRPCore.Com"%>
<%
	int Total=0;
	DataTable rs2 = new DataTable();
	rs2 = clsCommon.GetDataTable("SELECT EmpID FROM HR_tblEmpCV");	
	Total=rs2.Rows.Count;
%>
<script language="javascript">
	var DS2 =new Array(1);
	DS2[0]=new Array(<%=Total%>)
	<% for (int i=0;i<Total;i++){%>
		DS2[0][<%=i%>]="<%=rs2.Rows[i]["EmpID"]%>";
	<%}
		rs2.Dispose();
	%>
</script>
<script language="javascript" src="../Include/common.js">
</script>
<script language="javascript">
function checkEmpID()
{
	var iCheck;
	for (i=0;i<<%=Total%>;i++)
	{		
		if(trim(DS2[0][i])==document.getElementById('_ctl0_HR_EmpHeader_txtEmpID').value)					
		{				
			return true;
		}		
	}
	alert('Mã NV ' + document.getElementById('_ctl0_HR_EmpHeader_txtEmpID').value + ' không t?n t?i');
	return false;
}

function checkSearch()
{
	if (checkisnull('txtPRMonth','<Mã NV> không du?c tr?ng!')==false) return false;	
}
function validform()
{
	if (checkisnull('txtPRMonth','<Tháng> không du?c tr?ng!')==false) return false;
	if (checkisnull('HR_EmpHeader_txtEmpID','<Mã NV> không du?c tr?ng!')==false) return false;
	if (checkEmpID()==false) return false;
	return true;
}
function checkisnull(obj,strField){
	if(document.getElementById('_ctl0_' + obj).value=="")
		{
			alert(strField);
			document.getElementById('_ctl0_' + obj).focus();
			return false;
		}
	else
		return true;
}
function checkdelete()
{
	if(GridCheck('_ctl0_grdList',3,1,'chkSelect')==false)
	{
		alert('Please select at least one record before delete');
		return false;
	}
	if(confirm('Delete checked records, are you sure?')==false){
		return false;
	}
}
</script>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="99%" align="center" border="0">
	<TR>
		<TD noWrap align="left"><asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD noWrap align="left">
			<TABLE id="Table2" cellSpacing="0" cellPadding="1" width="99%" border="0">
				<TR>
					<TD style="WIDTH: 55px" width="55"><asp:label id="Label1" CssClass="labelrequire" runat="server" Width="40px">Month Period</asp:label></TD>
					<TD width="26%"><asp:textbox id="txtPRMonth" onblur="javascript:CheckMonthYear(this)" CssClass="input" runat="server"
							MaxLength="7" Width="75px"></asp:textbox>&nbsp;&nbsp;&nbsp;&nbsp;<asp:linkbutton id="btnSearch" accessKey="S" CssClass="btnSearch" runat="server" ToolTip="Alt+ S">Search</asp:linkbutton></TD>
					<TD width="10%"></TD>
					<TD noWrap width="30%"></TD>
					<TD width="9%"></TD>
					<TD noWrap width="14%"></TD>
				</TR>
			</TABLE>
			<HR align="center" width="95%">
		</TD>
	</TR>
	<TR>
		<TD noWrap align="center"><uc1:empheader id="HR_EmpHeader" runat="server"></uc1:empheader></TD>
	</TR>
	<TR>
		<TD align="center"><!-- start detail for list form --> <!-- end button for list form --></TD>
	</TR>
	<TR>
		<TD vAlign="top" noWrap align="center" height="20"></TD>
	</TR>
	<TR> <!-- start button for list form -->
		<TD noWrap align="center">
			<TABLE id="tblButton" cellSpacing="0" cellPadding="0" width="98%" border="0">
				<TR vAlign="top" height="35">
					<TD vAlign="middle" width="15%"><asp:checkbox id="chkShowGrid" onclick="javascript:collapse('tblGrid');" CssClass="checkbox" runat="server"
							Visible="False" Checked="True" Text="Show grid"></asp:checkbox></TD>
					<TD align="center" width="*"><asp:linkbutton id="btnSave" accessKey="S" CssClass="btnSave" runat="server" ToolTip="Alt+ S">Save</asp:linkbutton>&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnDelete" accessKey="D" CssClass="btnDelete" runat="server" ToolTip="Alt+D">Delete</asp:linkbutton>&nbsp;&nbsp; 
						&nbsp;
						<asp:linkbutton id="btnExport" accessKey="E" CssClass="btnExport" runat="server" ToolTip="Alt+E">Export</asp:linkbutton></TD>
				</TR>
			</TABLE>
		</TD>
	</TR> <!-- end button for list form -->
	<TR>
		<TD align="center"><!-- start grid for list form -->
			<TABLE id="tblGrid" cellSpacing="0" cellPadding="1" width="99%" border="0">
				<TR>
					<TD id="tdPages" width="43%"><uc1:columnlist id="uctrlColumns" runat="server"></uc1:columnlist></TD>
					<TD id="tdSort" style="DISPLAY: none" align="right" width="*"><asp:label id="Label8" CssClass="labelRight" runat="server" Width="30px">  Col 1</asp:label><asp:dropdownlist id="cboCol1" CssClass="combo" runat="server" Width="80px"></asp:dropdownlist><asp:label id="Label10" CssClass="labelRight" runat="server" Width="30px">Col 2</asp:label>&nbsp;
						<asp:dropdownlist id="cboCol2" CssClass="combo" runat="server" Width="80px"></asp:dropdownlist><asp:label id="Label15" CssClass="labelRight" runat="server" Width="30px">Col 3</asp:label>&nbsp;
						<asp:dropdownlist id="cboCol3" CssClass="combo" runat="server" Width="80px"></asp:dropdownlist></TD>
				</TR>
				<TR id="trGrid"> <!-- start grid detail for list form -->
					<TD colSpan="2"><asp:datagrid id="grdList" CssClass="grid" runat="server" Width="100%" PageSize="20" AllowSorting="True"
							AutoGenerateColumns="False" AllowPaging="True" CellPadding="0" BorderWidth="1px" BorderColor="#3366CC"
							BackColor="White">
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle CssClass="gridHeader"></HeaderStyle>
							<FooterStyle CssClass="gridFooter"></FooterStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
									<HeaderStyle Width="7%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Seq">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<%# (grdList.PageSize*grdList.CurrentPageIndex) + Container.ItemIndex + 1%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn SortExpression="EmpID" DataField="EmpID" HeaderText="Mã NV">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn HeaderText="Tên NV" DataField="EmpName">
									<HeaderStyle Width="25%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn HeaderText="Ðon v?" DataField="Level1"></asp:BoundColumn>
								<asp:BoundColumn HeaderText="Phòng ban" DataField="Level2"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Select">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<HeaderTemplate>
										<asp:CheckBox id="chkSelectAll" runat="server" CssClass="checkbox" onclick="CheckAll('_ctl0_grdList__ctl2_chkSelectAll','_ctl0_grdList',3,1,'chkSelect')"></asp:CheckBox>
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
	<TR>
		<TD noWrap height="5"></TD>
	</TR>
</TABLE>
<SCRIPT language="javascript">
<!--
function checkdelete()
{
	if(GridCheck('_ctl0_grdList',3,1,'chkSelect')==false)
	{
		alert('Please select at least one record before delete');
		return false;
	}
	if(confirm('Delete checked records, are you sure?')==false){
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
</SCRIPT>
