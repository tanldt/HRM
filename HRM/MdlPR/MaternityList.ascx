<%@ Control Language="c#" AutoEventWireup="false" Codebehind="MaternityList.ascx.cs" Inherits="iHRPCore.MdlPR.MaternityList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="97%" border="0">
	<TR>
		<TD align="left" width="100%"><asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center"></TD>
	</TR>
	<TR>
		<TD style="HEIGHT: 62px" align="center">
			<TABLE id="tblMain" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<TD></TD>
					<td>&nbsp;</td>
					<TD>&nbsp;</TD>
					<TD>&nbsp;&nbsp;
					</TD>
					<TD></TD>
					<TD><asp:label id="Label1" CssClass="labelrequire" runat="server" Width="88px">Month/Year</asp:label></TD>
					<TD><asp:textbox id="txtPRMonth" onblur="javascript:CheckMonthYear(this)" CssClass="input" runat="server"
							MaxLength="7" Width="75px"></asp:textbox></TD>
					<TD>&nbsp;
						<asp:linkbutton id="btnSearch" accessKey="S" CssClass="btnSearch" runat="server" ToolTip="ALT+S">Search</asp:linkbutton>&nbsp;&nbsp;
						<asp:linkbutton id="btnExport" accessKey="E" CssClass="btnExport" runat="server" ToolTip="Alt+E, Export data in grid to excel file">Export</asp:linkbutton></TD>
				</tr>
				<TR>
					<TD align="center"></TD>
					<TD align="center" colSpan="7">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
				</TR>
			</TABLE>
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
	</TR>
	<TR>
		<TD align="center" height="10"></TD>
	</TR>
	<TR>
		<TD align="center"><asp:datagrid id="grdMaternity" CssClass="grid" runat="server" Width="100%" PageSize="20" AutoGenerateColumns="False"
				CellPadding="0" BorderWidth="1px" BorderColor="#3366CC" BackColor="White" AllowPaging="True" AllowSorting="True">
				<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
				<ItemStyle CssClass="gridItem"></ItemStyle>
				<HeaderStyle CssClass="gridHeader"></HeaderStyle>
				<FooterStyle CssClass="gridFooter"></FooterStyle>
				<Columns>
					<asp:BoundColumn Visible="False" DataField="EmpID" HeaderText="ID"></asp:BoundColumn>
					<asp:BoundColumn DataField="No" HeaderText="Seq">
						<HeaderStyle HorizontalAlign="Center" Width="5%" VerticalAlign="Middle"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn HeaderText="Emp ID">
						<HeaderStyle Width="5%" HorizontalAlign="Center"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:LinkButton Runat="server" Enabled="true" ID="btnEdit" CssClass="hLink" CommandName="EDIT">
								<%# DataBinder.Eval(Container, "DataItem.EmpID")%>
							</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="EmpName" HeaderText="Full Name">
						<HeaderStyle Width="23%" HorizontalAlign="Center"></HeaderStyle>
						<ItemStyle HorizontalAlign="left"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="JobRelatedEN" HeaderText="Job Related">
						<HeaderStyle Width="25%" HorizontalAlign="Center"></HeaderStyle>
						<ItemStyle HorizontalAlign="left"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="FromDate" HeaderText="From date">
						<HeaderStyle Width="10%" HorizontalAlign="Center"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="ToDate" HeaderText="To date">
						<HeaderStyle Width="10%" HorizontalAlign="Center"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Left" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
			</asp:datagrid></TD>
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
	if(document.getElementById('_ctl0_txtPRMonth').value==""){
		alert('Please input month !');
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
