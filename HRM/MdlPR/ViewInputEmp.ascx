<%@ Register TagPrefix="uc1" TagName="EmpHeader" Src="../Include/EmpHeader.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ViewInputEmp.ascx.cs" Inherits="iHRPCore.MdlPR.ViewInputEmp" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="97%" border="0">
	<TR>
		<TD align="left" width="100%"><asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center"><uc1:empheader id="HR_EmpHeader" runat="server"></uc1:empheader></TD>
	</TR>
	<TR>
		<TD align="left"><asp:label id="Label1" CssClass="labelRequire" Runat="server">Month</asp:label><asp:textbox id="txtMonth" onblur="javascript:CheckMonthYear(this)" runat="server" CssClass="input"
				Width="100px"></asp:textbox><br>
		</TD>
	</TR>
	<TR>
		<TD noWrap align="center">
			<TABLE id="tblButton" cellSpacing="0" cellPadding="0" width="99%" border="0">
				<TR vAlign="top" height="30">
					<TD align="center" width="*"><asp:linkbutton id="btnView" runat="server" CssClass="btnSave">View</asp:linkbutton>&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnSave" runat="server" CssClass="btnSave">Save</asp:linkbutton>&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnDelete" runat="server" CssClass="btnDelete">Delete PIT</asp:linkbutton>&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnCreate" runat="server" CssClass="btnDelete">Create PIT</asp:linkbutton>&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnCal" runat="server" CssClass="btnDelete">Calculate PIT</asp:linkbutton>&nbsp;&nbsp;&nbsp;
					</TD>
				</TR>
			</TABLE>
		</TD>
	</TR> <!-- end button for input form -->
	<TR>
		<TD align="left"><asp:label id="Label2" Runat="server">Formula Type</asp:label>:
			<asp:label id="lblFormulaType" Runat="server" ForeColor="#ff0000" Font-Bold="True">Formula Type</asp:label></TD>
	</TR>
	<TR id="trGrid" runat="server">
		<TD align="center"><!-- start grid for input form -->
			<TABLE id="tblGrid" cellSpacing="0" cellPadding="1" width="98%" border="0">
				<TR id="trGrid1"> <!-- start grid detail for input form -->
					<TD colSpan="2"><asp:datagrid id="dtgListFormula" runat="server" Width="100%" HorizontalAlign="Center" AutoGenerateColumns="False"
							AllowSorting="false">
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle CssClass="gridHeader"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="LSSalaryItemDataID" Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="LSSalaryItemTaxID" Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="IsStatus" Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="IsCopy" Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="PRMonth" Visible="False"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="&lt;b&gt;Seq&lt;/b&gt;">
									<ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
									<ItemTemplate>
										<%# Container.ItemIndex + 1 + dtgListFormula.PageSize*dtgListFormula.CurrentPageIndex%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="LSSalaryItemDataCode" HeaderText="Salary Item">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="SalaryItem" HeaderText="Name">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Formula" HeaderText="Formula">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Value">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<HeaderTemplate>
										<asp:Label Runat="server" ID="lblValue">Value</asp:Label>
									</HeaderTemplate>
									<ItemTemplate>
										<asp:textbox id="txtValue" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Value") %>' Width="100%">
										</asp:textbox>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></TD>
				</TR>
				<tr>
					<td colSpan="2">
						<table width="100%">
							<tr>
								<td vAlign="top"><asp:label id="lblDescriptionSys" CssClass="label" Runat="server">Param system</asp:label><asp:datagrid id="dtgSysItem" runat="server" AutoGenerateColumns="False" AllowSorting="false">
										<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
										<ItemStyle CssClass="gridItem"></ItemStyle>
										<HeaderStyle CssClass="gridHeader"></HeaderStyle>
										<Columns>
											<asp:TemplateColumn HeaderText="&lt;b&gt;Seq&lt;/b&gt;">
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<%# Container.ItemIndex + 1 + dtgSysItem.PageSize*dtgSysItem.CurrentPageIndex%>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="LSSalaryItemDataCode" HeaderText="Salary Item">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="Name" HeaderText="Description">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="VNName" HeaderText="Description VN">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
											</asp:BoundColumn>
										</Columns>
									</asp:datagrid></td>
								<td vAlign="top"><asp:label id="Label6" CssClass="label" Runat="server">Users input values</asp:label><asp:datagrid id="dtgUserItem" runat="server" AllowSorting="false" AutoGenerateColumns="False">
										<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
										<ItemStyle CssClass="gridItem"></ItemStyle>
										<HeaderStyle CssClass="gridHeader"></HeaderStyle>
										<Columns>
											<asp:TemplateColumn HeaderText="&lt;b&gt;Seq&lt;/b&gt;">
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<%# Container.ItemIndex + 1 + dtgUserItem.PageSize*dtgUserItem.CurrentPageIndex%>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="LSSalaryItemDataCode" HeaderText="Salary Item">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="Name" HeaderText="Description">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
											</asp:BoundColumn>
										</Columns>
									</asp:datagrid></td>
							</tr>
						</table>
					</td>
				</tr>
			</TABLE> <!-- end grid for input form --></TD>
	</TR>
</TABLE>
<script>

function checkisnull(obj)
{
	if(document.getElementById('_ctl0_' + obj).value=="")
		{
			GetAlertError(iTotal,DSAlert,"0003");		
			document.getElementById('_ctl0_' + obj).focus();
			return false;
		}
	else
		return true;
}
function validform()
{	
	if(checkisnull('HR_EmpHeader_txtEmpID')==false)  return false;
	if(document.getElementById('_ctl0_txtMonth').value=="")
	{
		GetAlertError(iTotal,DSAlert,"P0001");		
		document.getElementById('_ctl0_txtMonth').focus();
		return false;
	}
	var txtMonth = document.getElementById('_ctl0_txtMonth').value;
	var checkPay = ViewInputEmp.CheckPayrollExists(txtMonth).value;
	if (checkPay == false )
	{
		alert("The system didn't find data of month " + txtMonth + ".");
		return false;
	}
	
	return true;
}
function DeletePIT()
{	
	if(checkisnull('HR_EmpHeader_txtEmpID')==false)  return false;
	if(document.getElementById('_ctl0_txtMonth').value=="")
	{
		GetAlertError(iTotal,DSAlert,"P0001");		
		document.getElementById('_ctl0_txtMonth').focus();
		return false;
	}
	var txtMonth = document.getElementById('_ctl0_txtMonth').value;
	var checkPay = ViewInputEmp.CheckPayrollExists(txtMonth).value;
	if (checkPay == false )
	{
		alert("The system didn't find data of month " + txtMonth + ".");
		return false;
	}
	if(confirm(GetAlertText(iTotal,DSAlert,"0002"))==false){
		return false;
	}
	
	return true;
}
</script>
