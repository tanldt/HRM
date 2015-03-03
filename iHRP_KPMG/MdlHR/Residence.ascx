<%@ Register TagPrefix="uc1" TagName="Address" Src="../Include/Address.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Residence.ascx.cs" Inherits="iHRPCore.MdlHR.Residence" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
	<TR>
		<TD align="center">
			<asp:label id="lblTitle" runat="server" Width="480px" Font-Size="12pt" Font-Names="Arial" ForeColor="Black"
				Font-Bold="True">RESIDENCE LIST</asp:label></TD>
	</TR>
	<TR>
		<TD align="center">
			<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="80%" border="0" align="center">
				<TR>
					<TD colSpan="6">
						<asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></TD>
				</TR>
				<TR>
					<TD>
						<asp:label id="Label1" CssClass="label" runat="server">From month</asp:label></TD>
					<TD>
						<asp:textbox Width="100%" id="txtFromMonth" onblur="javascript:CheckMonthYear(this)" CssClass="input"
							runat="server"></asp:textbox></TD>
					<TD>
						<asp:label id="Label2" CssClass="label" runat="server">To month</asp:label></TD>
					<TD>
						<asp:textbox Width="100%" id="txtToMonth" onblur="javascript:CheckMonthYear(this)" CssClass="input"
							runat="server"></asp:textbox></TD>
					<td></td>
					<td></td>
				</TR>
				<TR>
					<TD width="10%">
						<asp:label id="Label3" runat="server" CssClass="label">Address</asp:label></TD>
					<TD width="20%">
						<asp:textbox id="txtAddress" runat="server" CssClass="input" Width="100%" MaxLength="100"></asp:textbox></TD>
					<TD width="10%">
						<asp:label id="Label4" runat="server" CssClass="label">District</asp:label></TD>
					<TD width="20%">
						<asp:textbox id="txtDistrict" runat="server" CssClass="input" Width="100%" MaxLength="100"></asp:textbox></TD>
					<TD width="10%">
						<asp:label id="Label5" runat="server" CssClass="label">Province</asp:label></TD>
					<TD width="20%">
						<asp:dropdownlist id="cboLSProvinceID" runat="server" CssClass="combo" Width="100%"></asp:dropdownlist></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD align="center">
			<asp:linkbutton id="btnAddNew" accessKey="N" runat="server" CssClass="btnAddnew" ToolTip="Alt+N">Add New</asp:linkbutton>&nbsp;
			<asp:linkbutton id="btnSave" accessKey="S" runat="server" CssClass="btnSave" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp;
			<asp:linkbutton id="btnDelete" accessKey="D" runat="server" CssClass="btnExport" ToolTip="Alt + D">Delete</asp:linkbutton>&nbsp;
			<asp:linkbutton id="btnExport" accessKey="E" runat="server" CssClass="btnExport" ToolTip="Alt+E, Xuất dl trên lưới ra file Excel"> Export</asp:linkbutton>&nbsp;
			<asp:linkbutton id="btnExit" accessKey="Y" CssClass="btnSave" runat="server" Width="96px" ToolTip="ALT+E">Exit</asp:linkbutton><INPUT id="txtResidenceID" style="WIDTH: 10px; HEIGHT: 22px" type="hidden" size="1" name="txtManagerID"
				runat="server"><INPUT id="txtflag" value="0" style="WIDTH: 10px; HEIGHT: 22px" type="hidden" size="1"
				name="txtManagerID" runat="server"></TD>
	</TR>
	<TR>
		<TD align="center">
			<asp:datagrid id="dtgList" runat="server" Width="700px" BackColor="White" CellPadding="0" AutoGenerateColumns="False"
				AllowSorting="True" BorderColor="#3366CC">
				<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
				<ItemStyle CssClass="gridItem"></ItemStyle>
				<HeaderStyle CssClass="gridHeader"></HeaderStyle>
				<FooterStyle CssClass="gridFooter"></FooterStyle>
				<Columns>
					<asp:BoundColumn Visible="False" DataField="ResidenceID" HeaderText="Residence">
						<HeaderStyle Width="10%"></HeaderStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn HeaderText="FromMonth">
						<HeaderStyle Width="10%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:LinkButton id=hpFromMonth CssClass=Hlink Width="15%" Text='<%# DataBinder.Eval(Container, "DataItem.FromMonth") %>' CommandName="edit" Runat="server">
							</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="ToMonth" HeaderText="ToMonth">
						<HeaderStyle Width="10%"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="Address" HeaderText="Address">
						<HeaderStyle Width="30%"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="District" HeaderText="District">
						<HeaderStyle Width="30%"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="Province" HeaderText="Province">
						<HeaderStyle Width="30%"></HeaderStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn HeaderText="Delete">
						<HeaderStyle Width="5%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<HeaderTemplate>
							<asp:CheckBox id="chkSelectAll" runat="server" CssClass="checkbox" onclick="CheckAll('_ctl0_dtgList__ctl1_chkSelectAll','_ctl0_dtgList',2,1,'chkSelect')"></asp:CheckBox>
						</HeaderTemplate>
						<ItemTemplate>
							<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:datagrid></TD>
	</TR>
</TABLE>
<script language="javascript">
function validform(){	
	if(checkisnull('txtFromMonth')==false)  return false;	
	if(!FromSmallToMonth(document.getElementById('_ctl0_txtFromMonth'),document.getElementById('_ctl0_txtToMonth')))
	{	
		alert('Tháng từ không được nhỏ hơn tháng đến');
		return false;
	}
	document.getElementById('_ctl0_txtflag').value='1';	
	return true;
}
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
	function checkdelete()
	{
		if(GridCheck('_ctl0_dtgList',2,1,'chkSelect')==false)
		{
			GetAlertError(iTotal,DSAlert,"0001");
			return false;
		}
		if(confirm(GetAlertText(iTotal,DSAlert,"0002"))==false){
			return false;
		}else{
		document.getElementById('_ctl0_txtflag').value='1';	
		return true;
		}
		
	}
	function exitWindow()
	{
		window.close();
	}
</script>
