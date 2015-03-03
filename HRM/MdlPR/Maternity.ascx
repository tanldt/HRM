<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Maternity.ascx.cs" Inherits="iHRPCore.MdlPR.Maternity" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="97%" border="0">
	<TR>
		<TD align="left" width="100%">
			<asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center"></TD>
	</TR>
	<TR>
		<TD align="center" style="HEIGHT: 62px">
			<TABLE id="tblMain" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td>
						<asp:label id="EmpID" runat="server" CssClass="labelrequire">Emp ID</asp:label>&nbsp;</td>
					<TD>
						<asp:textbox id="txtEmpID" runat="server" CssClass="input" Width="75px" MaxLength="10" ReadOnly="True"></asp:textbox>&nbsp;<INPUT class="search" runat="server" id="btnReportTo" type="button" value="..." name="btnReportTo"></TD>
					<TD>&nbsp;&nbsp;
						<asp:label id="lblNote" CssClass="labelrequire" runat="server" Width="88px">No. of month</asp:label></TD>
					<TD></TD>
					<TD>
						<asp:textbox id="txtMonths" onblur="JavaScript:checkNumeric(this)" CssClass="input" runat="server"
							Width="75px" MaxLength="1"></asp:textbox></TD>
					<TD>&nbsp;
						<asp:label id="Label1" CssClass="labelrequire" runat="server" Width="88px">Salary Period</asp:label>&nbsp;</TD>
					<TD>
						<asp:textbox id="txtPRMonth" onblur="javascript:CheckMonthYear(this)" CssClass="input" runat="server"
							Width="75px" MaxLength="7"></asp:textbox></TD>
				</tr>
				<TR>
					<TD height="10"></TD>
					<TD height="10"></TD>
					<TD height="10"></TD>
					<TD height="10"></TD>
					<TD height="10"></TD>
					<TD height="10"></TD>
					<TD height="10"></TD>
				</TR>
				<TR>
					<TD align="center" colSpan="7">
						<asp:linkbutton id="btnAddNew" accessKey="N" CssClass="btnAddnew" runat="server" ToolTip="Alt+N">New record</asp:linkbutton>&nbsp;
						<asp:linkbutton id="btnSave" accessKey="S" CssClass="btnSave" runat="server" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp;&nbsp;
						<asp:linkbutton id="btnDelete" accessKey="S" CssClass="btnSave" runat="server" ToolTip="ALT+S">Delete</asp:linkbutton>&nbsp;&nbsp;
						<asp:linkbutton id="btnExport" accessKey="E" CssClass="btnExport" runat="server" ToolTip="Alt+E, Export data in grid to excel file">Export</asp:linkbutton></TD>
				</TR>
			</TABLE>
			&nbsp;&nbsp;<INPUT id="txtMaternityID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtMaternityID"
				runat="server">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
	</TR>
	<TR>
		<TD align="center" height="10"></TD>
	</TR>
	<TR>
		<TD align="center">
			<asp:datagrid id="grdMaternity" CssClass="grid" runat="server" Width="100%" AllowSorting="True"
				AllowPaging="True" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="0"
				AutoGenerateColumns="False" PageSize="20">
				<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
				<ItemStyle CssClass="gridItem"></ItemStyle>
				<HeaderStyle CssClass="gridHeader"></HeaderStyle>
				<FooterStyle CssClass="gridFooter"></FooterStyle>
				<Columns>
					<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID"></asp:BoundColumn>
					<asp:BoundColumn DataField="No" HeaderText="Seq">
						<HeaderStyle HorizontalAlign="Center" Width="5%" VerticalAlign="Middle"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn HeaderText="Emp ID">
						<HeaderStyle Width="5%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:LinkButton Runat="server" Enabled="true" ID="btnEdit" CssClass="hLink" CommandName="EDIT">
								<%# DataBinder.Eval(Container, "DataItem.EmpID")%>
							</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="FullName" HeaderText="Full Name">
						<HeaderStyle Width="23%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="Months" HeaderText="No. of Months">
						<HeaderStyle Width="10%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="PRMonth" HeaderText="Salary Period">
						<HeaderStyle Width="16%"></HeaderStyle>
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
			</asp:datagrid>
		</TD>
	</TR>
	<TR>
		<TD noWrap align="center"></TD>
	</TR> <!-- end button for input form -->
	<TR id="trGrid" runat="server">
		<TD align="center"><!-- start grid for input form --> <!-- end grid for input form --></TD>
	</TR>
</TABLE>
<script language="javascript">
<!--
function validform(){	
	if(document.getElementById('_ctl0_txtEmpID').value==""){
		alert('Please input employee ID!');
		document.getElementById('_ctl0_txtEmpID').focus();
		return false;
	}	
	if(document.getElementById('_ctl0_txtMonths').value==""){
		alert('Please input No. of months !');
		document.getElementById('_ctl0_txtMonths').focus();
		return false;
	}
	if(document.getElementById('_ctl0_txtPRMonth').value==""){
		alert('Please input salary Period !');
		document.getElementById('_ctl0_txtPRMonth').focus();
		return false;
	}
	return true;
}
function checkisnull(obj)
	{
		if(document.getElementById('_ctl0_' + obj).value=="")
			{
				alert('Please select salary period!');
				document.getElementById('_ctl0_' + obj).focus();
				return false;
			}
		else
			return true;
}
function checkdelete()
{
	if(GridCheck('_ctl0_grdMaternity',3,1,'chkSelect')==false)
	{
		alert('Please select at least one record before delete');
		return false;
	}
	if(confirm('Delete checked records, are you sure?')==false){
		return false;
	}
}
function ReturnEmpPopUp3(strEmpID, strEmpCode, strEmpName){	
	document.getElementById("_ctl0_txtEmpID").value = strEmpID;
}
function OpenWindowEmp(strField){
	ShowDialog('FormPage.aspx?Ascx=MdlHR/EmpPopup.ascx&Params=3');		
}
//-->
</script>
