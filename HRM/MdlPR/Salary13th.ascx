<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Salary13th.ascx.cs" Inherits="iHRPCore.MdlPR.Salary13th" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="ColumnList" Src="../Include/ColumnList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../Include/EmpHeaderSearch.ascx" %>
<TABLE cellSpacing="0" cellPadding="0" width="98%" border="0">
	<TR>
		<TD align="center">
			<asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label>
		</TD>
	</TR>
	<TR>
		<td align="center">
			<uc1:EmpHeaderSearch id="EmpHeaderSearch1" runat="server"></uc1:EmpHeaderSearch>
		</td>
	</TR>
	<TR>
		<TD align="center">
			<TABLE cellSpacing="0" cellPadding="1" width="99%" border="0">
				<TR>
					<TD width="10%">
						<P align="left"><asp:label id="Label5" CssClass="labelrequire" runat="server">Month</asp:label></P>
					</TD>
					<TD width="10%"><asp:textbox id="txtMMYYYY" CssClass="input" runat="server" MaxLength="4" Width="80px" onblur="CheckYear1900(this);"></asp:textbox></TD>
					<TD align="center" colSpan="3">
						<asp:linkbutton id="btnSearch" CssClass="btnSearch" runat="server"> View</asp:linkbutton>&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnCalculate" CssClass="btnSearch" runat="server"> Calculate</asp:linkbutton>&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnSave" runat="server" CssClass="btnSearch"> Save</asp:linkbutton>&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnDelete" CssClass="btnSearch" runat="server"> Delete</asp:linkbutton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnExport" accessKey="E" CssClass="btnExport" runat="server" ToolTip="Alt+E, Export data in grid to excel file"> Export</asp:linkbutton>
					</TD>
				</TR>
			</TABLE>
	<tr>
		<TD vAlign="top" noWrap align="center" height="20">
			<HR align="center" width="100%">
		</TD>
	</tr>
	<TR>
		<TD align="center">
			<TABLE cellSpacing="0" cellPadding="1" width="98%" border="0">
				<TR id="trColumnList">
					<TD align="left" colSpan="4">
						<uc1:ColumnList id="uctrlColumns" runat="server"></uc1:ColumnList>
					</TD>
				</TR>
				<TR id="trGrid">
					<TD colSpan="2">
						<asp:datagrid id="dtgList" CssClass="grid" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
							CellPadding="0" BorderWidth="1px" BorderColor="#3366CC" BackColor="White" HorizontalAlign="Center"
							AllowPaging="True" PageSize="15">
							<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="FestivalBonusID"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="EmpID"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="NetTakeHome"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Select">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<HeaderTemplate>
										<asp:CheckBox id="chkSelectAll" runat="server" onclick="CheckAll('_ctl0_dtgList__ctl2_chkSelectAll','_ctl0_dtgList',3,1,'chkSelect')"
											CssClass="checkbox"></asp:CheckBox>
									</HeaderTemplate>
									<ItemTemplate>
										<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="No.">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<%# Container.ItemIndex + 1 + dtgList.PageSize*dtgList.CurrentPageIndex%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="EmpCode" SortExpression="EmpCode" HeaderText="Emp Code">
									<HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="EmpName" SortExpression="EmpName" HeaderText="Full Name">
									<HeaderStyle Width="20%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Level2" SortExpression="Level2" HeaderText="Department">
									<HeaderStyle Width="15%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="StartDate" SortExpression="StartDate" HeaderText="Joining Date">
									<HeaderStyle Width="12%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="AmountUSD" SortExpression="AmountUSD" HeaderText="Amount USD">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="AmountVND" SortExpression="AmountVND" HeaderText="Amount VND">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Note">
									<HeaderStyle Width="15%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:TextBox id="txtdtgListNote" Width="100%" style="text-align:left" value='<%# DataBinder.Eval(Container, "DataItem.Note") %>' CssClass="input" Runat="server">
										</asp:TextBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn Visible="False" DataField="Saved"></asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
					</TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>
<script type="text/javascript" language="javascript">
<!--

function validform()
	{
		if(document.getElementById("_ctl0:EmpHeaderSearch1:cboCompany").value=='')
		{
			GetAlertError(iTotal,DSAlert,"SI_SF03");
			document.getElementById("_ctl0:EmpHeaderSearch1:cboCompany").focus();
			return false;
		}
		//if(checkisnull('txtYear')==false)  return false;
		if (document.getElementById('_ctl0_txtYear').value == "")
		{
			GetAlertError(iTotal,DSAlert,"0003");
			document.getElementById('_ctl0_txtYear').focus();
			return false;
		}
		
	   				
		return true;
	}
	
//kiem tra cac gia truoc khi Search
function checkvalidSearch()
{
	if (document.getElementById('_ctl0_txtYear').value == "")
	{
		GetAlertError(iTotal,DSAlert,"0003");
		document.getElementById('_ctl0_txtYear').focus();
		return false;
	}
	
	return true;				
}
//kiem tra cac gia tri nhap truoc khi luu
function checksave()
{
	
	if(GridCheck('_ctl0_dtgList',3,1,'chkSelect')==false)
	{			
		GetAlertError(iTotal,DSAlert,"0001");				
		return false;
	}
		
	return true;
}
//kiem tra User co check chon truoc khi Delete
function checkdelete()
	{
		if(GridCheck('_ctl0_dtgList',3,1,'chkSelect')==false)
		{			
			GetAlertError(iTotal,DSAlert,"0001");				
			return false;
		}
		if(confirm(GetAlertText(iTotal,DSAlert,"0002"))==false){
			return false;
		}
		
	}
//-->
</script>
