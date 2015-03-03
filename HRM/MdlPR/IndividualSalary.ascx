<%@ Control Language="c#" AutoEventWireup="false" Codebehind="IndividualSalary.ascx.cs" Inherits="iHRPCore.MdlPR.IndividualSalary" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="EmpHeader" Src="../Include/EmpHeader.ascx" %>
<TABLE id="Table1" style="WIDTH: 100%" cellSpacing="0" cellPadding="0" width="300" border="0">
	<TR>
		<TD align="center"><asp:label id="lblErr" CssClass="labelRequire" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center"><uc1:empheader id="HR_EmpHeader" runat="server"></uc1:empheader></TD>
	</TR>
	<TR>
		<TD align="center">
			<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="98%" border="0">
				<TR>
					<TD><asp:label id="Label3" CssClass="label" runat="server">Month</asp:label>&nbsp;</TD>
					<TD><asp:textbox id="txtFromMonth" onblur="javascript:CheckMonthYear(this)" CssClass="input" runat="server"
							MaxLength="7" Width="100px"></asp:textbox></TD>
					<TD>&nbsp;
						<asp:label id="Label5" CssClass="label" runat="server">Salary Period</asp:label>&nbsp;</TD>
					<TD><asp:textbox id="txtSalPeriod" onblur="javascript:CheckMonthYear(this)" CssClass="input" runat="server"
							MaxLength="7" Width="100px"></asp:textbox></TD>
					<TD>&nbsp;
						<asp:linkbutton id="btnSave" accessKey="S" CssClass="btnSave" runat="server" ToolTip="ALT+S"> Calculate</asp:linkbutton></TD>
					<TD>&nbsp;
						<asp:linkbutton id="btnView" accessKey="Y" CssClass="btnSave" runat="server" ToolTip="ALT+I">View</asp:linkbutton></TD>
					<TD>&nbsp;
						<asp:linkbutton id="btnDelete" accessKey="Y" CssClass="btnDelete" runat="server" ToolTip="ALT+D">Delete</asp:linkbutton></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD align="center" height="10"></TD>
	</TR>
	<TR>
		<TD align="center"></TD>
	</TR>
	<TR>
		<TD style="HEIGHT: 291px" align="center">
			<DIV id="Div1" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 338px" runat="server"><asp:datagrid id="dtgList" runat="server" Width="100%" CellPadding="1" AutoGenerateColumns="False"
					AllowSorting="True" BorderColor="DodgerBlue">
					<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
					<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
					<Columns>
						<asp:BoundColumn DataField="LSSalaryItemID" Visible="False"></asp:BoundColumn>
						<asp:TemplateColumn HeaderText="Salary Item">
							<HeaderStyle Width="35%"></HeaderStyle>
							<ItemStyle HorizontalAlign="Left"></ItemStyle>
							<ItemTemplate>
								<asp:Label id="lblSalaryItem" CssClass=label Width="100%" Text='<%# DataBinder.Eval(Container, "DataItem.Name") %>' Runat="server">
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Fomular">
							<HeaderStyle Width="45%"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<ItemTemplate>
								<asp:TextBox id="txtFormular" style="OVERFLOW: auto" TextMode=MultiLine CssClass=input Width="300px" Text='<%# DataBinder.Eval(Container, "DataItem.Formula") %>' Runat="server">
								</asp:TextBox>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Value">
							<HeaderStyle Width="80px"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<ItemTemplate>
								<asp:TextBox id="txtValue" style="TEXT-ALIGN: right" Height="32px" onblur="javascript:checkNumber(this)" CssClass=input Width="80px" Text='<%# DataBinder.Eval(Container, "DataItem.Value") %>' Runat="server">
								</asp:TextBox>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Chọn">
							<HeaderStyle Width="30px"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<HeaderTemplate>
								<asp:CheckBox id="chkSelectAll" onclick="CheckAll('_ctl0_dtgList__ctl1_chkSelectAll','_ctl0_dtgList',2,1,'chkSelect')" runat="server" CssClass="gridFooter"></asp:CheckBox>
							</HeaderTemplate>
							<ItemTemplate>
								<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn DataField="AllowEdit" Visible="False"></asp:BoundColumn>
						<asp:BoundColumn DataField="AllowDel" Visible="False"></asp:BoundColumn>
					</Columns>
				</asp:datagrid></DIV>
		</TD>
	</TR>
	<TR>
		<TD align="left">&nbsp;</TD>
	</TR>
	<TR>
		<TD align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
	</TR>
</TABLE>
<script language="javascript">
function CheckSave()
{

	if(GridCheck('_ctl0_dtgList',2,1,'chkSelect')==false)
	{
		GetAlertTextPopUp('<%=GetText("LISTOFSALARY","COMFONESELECT")%>');
		return false;
	}

	return true;
}
function ShowPage()
{
	var Month = document.getElementById('<%=txtFromMonth.ClientID%>').value;
	var SalPeriod = document.getElementById('<%=txtSalPeriod.ClientID%>').value;
	window.open('./MdlPR/SumPayroll.aspx?MMYYYY='+ Month+ '&SalPeriod=' + SalPeriod + '&EmpCode=<%=strEmpID%>','SelectFile','status=1,toolbar=0,scrollbars=1,resizable=1,alwaysRaised=Yes, top=20, left=30, width=800, height=600,1 ,align=center');
	return false;
}
function checkdelete()
{
	if(GridCheck('_ctl0_dtgList',2,1,'chkSelect')==false)
	{
		GetAlertError(iTotal,DSAlert,"0001");				
		return false;
	}
	if(confirm(GetAlertText(iTotal,DSAlert,"0002"))==false){
		return false;
	}
}
</script>

