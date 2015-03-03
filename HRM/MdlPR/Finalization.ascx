<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../Include/EmpHeaderSearch.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ColumnList" Src="../Include/ColumnList.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Finalization.ascx.cs" Inherits="MdlPR.Finalization" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
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
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="90%" border="0">
				<TR>
					<TD width="14%">
						<P align="left"><asp:label id="Label5" CssClass="labelrequire" runat="server">Year</asp:label></P>
					</TD>
					<TD width="35%"><asp:textbox id="txtMonth" CssClass="input" runat="server" MaxLength="4" Width="80px" onblur="CheckYear1900(this);"></asp:textbox></TD>
					<TD width="2%"></TD>
					<TD width="14%">
						<asp:label id="Label1" runat="server" CssClass="label">Agv rate</asp:label></TD>
					<TD width="35%">
						<asp:textbox id="txtSeniority" onblur="CheckYear1900(this);" runat="server" CssClass="input"
							Width="80px" MaxLength="4"></asp:textbox></TD>
				</TR>
				<TR>
					<TD align="center" colSpan="5">
						<asp:RadioButtonList id="RadioButtonList1" runat="server" Width="272px" RepeatDirection="Horizontal">
							<asp:ListItem Value="0">Calculated</asp:ListItem>
							<asp:ListItem Value="1">Not yet</asp:ListItem>
						</asp:RadioButtonList></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD align="center">
			<TABLE cellSpacing="0" cellPadding="1" width="99%" border="0">
				<TR>
					<TD width="10%" colSpan="5">
						<P align="center">
							<asp:linkbutton id="btnSearch" runat="server" CssClass="btnSearch"> View</asp:linkbutton>&nbsp;&nbsp;&nbsp;
							<asp:linkbutton id="btnCalculate" runat="server" CssClass="btnSearch"> Calculate</asp:linkbutton>&nbsp;&nbsp;&nbsp;
							<asp:linkbutton id="btnSave" runat="server" CssClass="btnSearch"> Save</asp:linkbutton>&nbsp;&nbsp;&nbsp;
							<asp:linkbutton id="btnDelete" runat="server" CssClass="btnSearch"> Delete</asp:linkbutton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<asp:linkbutton id="btnExport" accessKey="E" runat="server" CssClass="btnExport" ToolTip="Alt+E, Export data in grid to excel file"> Export</asp:linkbutton></P>
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
						<DIV style="OVERFLOW: auto; WIDTH: 750px; HEIGHT: 400px"><asp:datagrid id="dtgList" CssClass="grid" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
								CellPadding="0" BorderWidth="1px" BorderColor="#3366CC" BackColor="White" HorizontalAlign="Center" AllowPaging="True" PageSize="15">
								<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
								<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
								<ItemStyle CssClass="gridItem"></ItemStyle>
								<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="Salary13thID" HeaderText="Salary13thID">
										<HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
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
									<asp:BoundColumn Visible="False" DataField="EmpID" SortExpression="EmpID" HeaderText="ID">
										<HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Year" SortExpression="Year" HeaderText="Year">
										<HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="EmpCode" SortExpression="EmpCode" HeaderText="ID">
										<HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="EmpName" SortExpression="EmpName" HeaderText="Full Name">
										<HeaderStyle Width="20%"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Department" SortExpression="Department" HeaderText="Department">
										<HeaderStyle Width="10%"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BaseSalary" SortExpression="BaseSalary" HeaderText="Joining Date">
										<HeaderStyle Width="10%"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Ter. Date">
										<HeaderStyle Width="10%"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="PIT Year Paid">
										<HeaderStyle Width="10%"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Average/Month"></asp:BoundColumn>
									<asp:BoundColumn HeaderText="Housing Year"></asp:BoundColumn>
									<asp:BoundColumn HeaderText="Housing Year (Adjust)"></asp:BoundColumn>
									<asp:BoundColumn HeaderText="PIT Finalization"></asp:BoundColumn>
									<asp:BoundColumn HeaderText="Finalization New"></asp:BoundColumn>
									<asp:BoundColumn HeaderText="Average/Month"></asp:BoundColumn>
									<asp:BoundColumn HeaderText="Difference"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Note">
										<HeaderStyle Width="15%"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:TextBox id="txtdtgListNote" Width="80" style="text-align:left" value='<%# DataBinder.Eval(Container, "DataItem.Note") %>' CssClass="input" Runat="server">
											</asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="Saved">
										<HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></DIV>
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
