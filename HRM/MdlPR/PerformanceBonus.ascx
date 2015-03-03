<%@ Control Language="c#" AutoEventWireup="false" Codebehind="PerformanceBonus.ascx.cs" Inherits="iHRPCore.MdlPR.PerformanceBonus" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="ColumnList" Src="../Include/ColumnList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../Include/EmpHeaderSearch.ascx" %>
<TABLE cellSpacing="0" cellPadding="0" width="98%" border="0">
	<TR>
		<TD align="center"><asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></TD>
	</TR>
	<TR>
		<td align="center"><uc1:empheadersearch id="EmpHeaderSearch1" runat="server"></uc1:empheadersearch></td>
	</TR>
	<TR>
		<TD align="center">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="90%" border="0">
				<TR>
					<TD width="14%">
						<P align="left"><asp:label id="Label5" runat="server" CssClass="labelrequire">Month</asp:label></P>
					</TD>
					<TD width="35%"><asp:textbox id="txtMM" onblur="CheckMonth(this);" runat="server" CssClass="input" Width="80px"
							MaxLength="4"></asp:textbox></TD>
					<TD width="2%"></TD>
					<TD width="14%"><asp:label id="Label1" runat="server" CssClass="labelrequire">Seniority</asp:label></TD>
					<TD width="35%"><asp:textbox id="txtSeniority" runat="server" CssClass="input" onblur="javascript:checkNumeric(this,1000);"
							Width="80px" MaxLength="4"></asp:textbox><asp:label id="Label2" runat="server" CssClass="label">(Months)</asp:label></TD>
				</TR>
				<TR>
					<TD>
						<P align="left"><asp:label id="Label6" runat="server" CssClass="labelrequire">Bonus Year</asp:label></P>
					</TD>
					<TD><asp:textbox id="txtYYYY" onblur="CheckYear(this);" runat="server" CssClass="input" Width="80px"
							MaxLength="4"></asp:textbox></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD><asp:label id="Label3" runat="server" CssClass="labelrequire">Type</asp:label></TD>
					<TD><asp:dropdownlist id="cboLSPerformanceTypeID" runat="server" CssClass="select" Width="49%" onchange="ChangeSelect(this);"></asp:dropdownlist></TD>
					<TD></TD>
					<TD><asp:label id="Label4" runat="server" CssClass="label">% Bonus</asp:label></TD>
					<TD><asp:textbox id="txtBonusPercent" onblur="javascript:checkNumeric(this)" runat="server" CssClass="input"
							Width="80px" MaxLength="4"></asp:textbox></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD vAlign="top" noWrap align="center" height="20">
			<HR align="center" width="95%">
		</TD>
	</TR>
	<TR>
		<TD align="center">
			<TABLE cellSpacing="0" cellPadding="1" width="99%" border="0">
				<TR  vAlign="top" height="35">
					<TD width="10%" colSpan="5">
						<P align="center"><asp:linkbutton id="btnSearch" runat="server" CssClass="btnSearch"> View</asp:linkbutton>&nbsp;&nbsp;&nbsp;
							<asp:linkbutton id="btnCalculate" runat="server" CssClass="btnSearch"> Calculate</asp:linkbutton>&nbsp;&nbsp;&nbsp;
							<asp:linkbutton id="btnSave" runat="server" CssClass="btnSearch"> Save</asp:linkbutton>&nbsp;&nbsp;&nbsp;
							<asp:linkbutton id="btnDelete" runat="server" CssClass="btnSearch"> Delete</asp:linkbutton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<asp:linkbutton id="btnExport" accessKey="E" runat="server" CssClass="btnExport" ToolTip="Alt+E, Export data in grid to excel file"> Export</asp:linkbutton></P>
					</TD>
				</TR>
			</TABLE>
	<tr>
		
	</tr>
	<TR>
		<TD align="center">
			<TABLE cellSpacing="0" cellPadding="1" width="98%" border="0">
				<TR id="trColumnList">
					<TD align="left" colSpan="4"><uc1:columnlist id="uctrlColumns" runat="server"></uc1:columnlist></TD>
				</TR>
				<TR id="trGrid">
					<TD colSpan="2"><asp:datagrid id="dtgList" runat="server" CssClass="grid" Width="100%" PageSize="15" AllowPaging="True"
							HorizontalAlign="Center" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False"
							AllowSorting="True">
							<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
							
							<Columns>
							
								<asp:BoundColumn Visible="False" DataField="PerformanceBonusID"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="EmpID"></asp:BoundColumn>	
																								
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
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								
								<asp:BoundColumn DataField="StartDate" SortExpression="StartDate" HeaderText="Joining Date">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								
								<asp:BoundColumn DataField="Type" HeaderText="Type">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								
								<asp:BoundColumn DataField="BonusPercent" HeaderText="% Bonus">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								
								<asp:BoundColumn DataField="Seniority" HeaderText="Seniority">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								
								<asp:BoundColumn DataField="NetTakeHome" HeaderText="Net taken home">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								
								<asp:BoundColumn DataField="Amount" HeaderText="Bonus Amount">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								
								<asp:TemplateColumn HeaderText="Note">
									<HeaderStyle Width="15%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
									<ItemTemplate>
										<asp:TextBox id="txtdtgListNote" Width="80" style="text-align:left" value='<%# DataBinder.Eval(Container, "DataItem.Note") %>' CssClass="input" Runat="server">
										</asp:TextBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:BoundColumn Visible="False" DataField="Saved"></asp:BoundColumn>
									
									
								
								
							</Columns>
							<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>
<script language="javascript" type="text/javascript">
<!--
//Xu ly su kien chon Performance Type : A,B,C,D,E
function ChangeSelect(obj)
{		
	var combo = document.getElementById(obj.id);
	var textbox = document.getElementById('_ctl0_txtBonusPercent');	
	
	var i = combo.selectedIndex;	
	if (combo.options[i].text !='')	
		textbox.value = combo.options[i].value;					
	
}

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
