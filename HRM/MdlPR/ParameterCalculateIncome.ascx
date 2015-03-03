<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ParameterCalculateIncome.ascx.cs" Inherits="iHRPCore.MdlPR.ParameterCalculateIncome" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="97%" border="0">
	<TR>
		<TD align="center" width="100%" height="10"><asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center"></TD>
	</TR>
	<TR>
		<TD style="HEIGHT: 102px" align="center" height="102"><!-- start detail for input form -->
			<TABLE id="Table2" cellSpacing="0" cellPadding="1" width="90%" border="0">
				<TR>
					<TD align="left" width="15%" height="24"><asp:label id="Label1" CssClass="labelrequire" runat="server">Ngày hieu luc</asp:label></TD>
					<TD width="30%" height="24"><asp:textbox id="txtFromDate" onblur="JavaScript:CheckDate(this)" CssClass="input" runat="server"
							Width="75px"></asp:textbox>&nbsp;<INPUT class=btnCal onclick="javascript:popUpCalendar(<%=this.txtFromDate.ClientID%>);" type=button></TD>
					<TD align="left" width="18%" height="24">&nbsp;<asp:label id="lblMinSalary" CssClass="labelrequire" runat="server">Min Salary</asp:label></TD>
					<TD width="40%" height="24"><asp:textbox id="txtMinSalary" onblur="javascript:checkNumeric(this);" CssClass="input" runat="server"
							Width="75px" MaxLength="10"></asp:textbox></TD>
				</TR>
				<TR>
					<TD align="left" width="15%" height="24"><asp:label id="lblBoardingFee" CssClass="label" runat="server"> Boarding Fee Fixed</asp:label></TD>
					<TD width="30%" height="24"><asp:textbox id="txtBoardingFeeFix" onblur="javascript:checkNumeric(this);" CssClass="input"
							runat="server" Width="75px" MaxLength="10"></asp:textbox></TD>
					<TD align="left" width="18%" height="24">&nbsp;
						<asp:label id="lblBoardingFeeRank1" CssClass="label" runat="server"> Boarding Fee 1</asp:label></TD>
					<TD width="40%" height="24">
						<asp:textbox id="txtBoardingFeeRank1" onblur="javascript:checkNumeric(this);" CssClass="input"
							runat="server" Width="75px" MaxLength="10"></asp:textbox></TD>
				</TR>
				<TR>
					<TD align="left" width="15%" height="24"><asp:label id="lblBoardingFeeRank2" CssClass="label" runat="server" Width="120px"> Boarding Fee 2</asp:label></TD>
					<TD width="30%" height="24">
						<asp:textbox id="txtBoardingFeeRank2" onblur="javascript:checkNumeric(this);" CssClass="input"
							runat="server" Width="75px" MaxLength="10"></asp:textbox></TD>
					<TD align="left" width="18%" height="24">&nbsp;
					</TD>
					<TD width="40%" height="24"><asp:textbox Visible="False" id="txtHygienceFee" onblur="javascript:checkNumeric(this);" CssClass="input"
							runat="server" Width="75px" MaxLength="10"></asp:textbox></TD>
				</TR>
				<TR style="DISPLAY: none">
					<TD align="left" width="15%" height="24"></TD>
					<TD width="30%" height="24"><asp:textbox id="txtHygienceFee1" onblur="javascript:checkNumeric(this);" CssClass="input" runat="server"
							Width="75px" MaxLength="10"></asp:textbox></TD>
					<TD align="left" width="18%" height="24"><asp:label id="lblBasicSalary" CssClass="labelrequire" runat="server" Visible="False"> Basic Salary</asp:label></TD>
					<TD width="40%" height="24"><asp:textbox id="txtBasicSalary" onblur="javascript:checkNumeric(this);" CssClass="input" runat="server"
							Width="75px" MaxLength="10" Visible="False"></asp:textbox></TD>
				</TR>
			</TABLE>
			<INPUT id="txtLevel1ID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtLevel1ID"
				runat="server"> <INPUT id="txtLevel2ID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtLevel2ID"
				runat="server"> <INPUT id="txtLevel3ID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtLevel3ID"
				runat="server">
		</TD>
	</TR>
	<TR>
		<TD noWrap align="center" height="10"></TD>
	</TR>
	<TR>
		<TD style="HEIGHT: 19px" noWrap align="center" height="19"><asp:linkbutton id="btnAddNew" accessKey="R" CssClass="btnDelete" runat="server" ToolTip="ALT+R">Add New</asp:linkbutton>&nbsp;&nbsp;
			<asp:linkbutton id="btnSave" accessKey="S" CssClass="btnSave" runat="server" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp;&nbsp;
			<asp:linkbutton id="btnDelete" accessKey="S" CssClass="btnSave" runat="server" ToolTip="ALT+S">Delete</asp:linkbutton></TD>
	</TR>
	<TR>
		<TD style="HEIGHT: 19px" noWrap align="center" height="19"></TD>
	</TR>
	<TR>
		<TD style="HEIGHT: 19px" noWrap align="center" height="19"><asp:datagrid id="grdOvertime" CssClass="grid" runat="server" Width="100%" BorderColor="#3366CC"
				AllowSorting="True" AutoGenerateColumns="False" AllowPaging="True" CellPadding="0" BorderWidth="1px" BackColor="White">
				<FooterStyle CssClass="gridFooter"></FooterStyle>
				<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
				<ItemStyle CssClass="gridItem"></ItemStyle>
				<HeaderStyle CssClass="gridHeader"></HeaderStyle>
				<Columns>
					<asp:BoundColumn Visible="False" DataField="FromDate" HeaderText="FromDate"></asp:BoundColumn>
					<asp:TemplateColumn HeaderText="&lt;b&gt;Seq&lt;/b&gt;">
						<HeaderStyle Width="5%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<%# Container.ItemIndex + 1%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Effective Date">
						<HeaderStyle Width="5%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:LinkButton Runat="server" Enabled="true" ID="btnEdit" CssClass="hLink" CommandName="Edit">
								<%# DataBinder.Eval(Container, "DataItem.FromDate") %>
							</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn Visible="False" DataField="HygienceFee" HeaderText="Hygiene fee 1">
						<HeaderStyle Width="9%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="HygienceFee1" HeaderText="Hygiene fee 2">
						<HeaderStyle Width="7%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="MinSalary" HeaderText="Min Salary">
						<HeaderStyle Width="20%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="BoardingFeeFix" HeaderText="Boarding fee fix">
						<HeaderStyle Width="15%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="BasicSalary" HeaderText="Basic Salary">
						<HeaderStyle Width="15%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="BoardingFeeRank1" HeaderText="Boarding fee Rank 1">
						<HeaderStyle Width="12%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="BoardingFeeRank2" HeaderText="Boarding fee Rank 2">
						<HeaderStyle Width="12%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn HeaderText="Select">
						<HeaderStyle Width="8%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<HeaderTemplate>
							<asp:CheckBox id="chkSelectAll" runat="server" CssClass="checkbox" onclick="CheckAll('_ctl0_grdOvertime__ctl2_chkSelectAll','_ctl0_grdOvertime',3,1,'chkSelect')"></asp:CheckBox>
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
<SCRIPT language="javascript">
<!--

function validform(){					
	if (document.getElementById('<%=this.txtFromDate.ClientID%>').value=='')
	{
	 alert('<%=GetText("COMMON","CONFDATE")%>');
	 document.getElementById('<%=this.txtFromDate.ClientID%>').focus();
	 return false;
	}
	if (document.getElementById('<%=this.txtMinSalary.ClientID%>').value=='')
	{
	 alert('<%=GetText("PARAMCALINCOME","CONFMINSALARY")%>');
	 document.getElementById('<%=this.txtMinSalary.ClientID%>').focus();
	 return false;
	}
/*	if (document.getElementById('<%=this.txtBasicSalary.ClientID%>').value=='')
	{
	 alert('<%=GetText("PARAMCALINCOME","CONFBASICSALARY")%>');
	 document.getElementById('<%=this.txtBasicSalary.ClientID%>').focus();
	 return false;
	}
*/
	return true;
}
function checkdelete()
{
	if(GridCheck('_ctl0_grdOvertime',3,1,'chkSelect')==false)
	{
		alert('<%=GetText("COMMON","COMFONESELECT")%>');
		return false;
	}
	if(confirm('<%=GetText("COMMON","CONFIRMDELETE")%>')==false){
		return false;
	}
}
//-->
</SCRIPT>
