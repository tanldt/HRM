<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../../Include/EmpHeaderSearch.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ColumnList" Src="../../Include/ColumnList.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="RPT_Customer_List.ascx.cs" Inherits="MdlSER.RPT_Customer_List" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table style="HEIGHT: 81px" width="99%" border="0">
	<TR>
		<TD style="WIDTH: 90px; HEIGHT: 22px"><asp:label id="lblCustomerGroup" Width="100%" runat="server"> Group</asp:label></TD>
		<TD style="WIDTH: 190px; HEIGHT: 22px"><asp:dropdownlist id="cboLSComapyID" Width="100%" runat="server" CssClass="select">
				<asp:ListItem></asp:ListItem>
			</asp:dropdownlist></TD>
		<TD style="WIDTH: 20px; HEIGHT: 22px"></TD>
		<TD style="WIDTH: 40px; HEIGHT: 22px" noWrap><asp:label id="lblCustomertype" Width="113px" runat="server"> Customer Type</asp:label></TD>
		<TD style="WIDTH: 190px; HEIGHT: 22px"><asp:dropdownlist id="cboCustomerTypeID" Width="100%" runat="server" CssClass="select">
				<asp:ListItem></asp:ListItem>
				<asp:ListItem Value="B">Group B</asp:ListItem>
				<asp:ListItem Value="C">Group C</asp:ListItem>
			</asp:dropdownlist></TD>
	</TR>
	<TR>
		<TD><asp:label id="lblCustomerCode" Width="100%" runat="server"> Code</asp:label></TD>
		<TD><asp:textbox id="txtLSLevel1Code" Width="100%" runat="server" CssClass="input"></asp:textbox></TD>
		<TD></TD>
		<TD noWrap><asp:label id="lblShortName" Width="100%" runat="server"> Short Name</asp:label></TD>
		<TD><asp:textbox id="txtShortName" Width="100%" runat="server" CssClass="input"></asp:textbox></TD>
	</TR>
	<tr>
		<td><asp:label id="lblName" Width="100%" runat="server">Name</asp:label></td>
		<td><asp:textbox id="txtName" Width="100%" runat="server" CssClass="input"></asp:textbox></td>
		<TD></TD>
		<td><asp:label id="lblNameVN" Width="100%" runat="server">Name (VN)</asp:label></td>
		<td><asp:textbox id="txtVNName" Width="100%" runat="server" CssClass="input"></asp:textbox></td>
	</tr>
	<TR>
		<TD style="HEIGHT: 2px" align="center" colSpan="5">
			<HR align="center" width="100%">
			&nbsp;&nbsp;
			<asp:linkbutton id="btnSearch" runat="server" CssClass="button">View</asp:linkbutton>&nbsp;
			<asp:linkbutton id="btnAddnew" runat="server" CssClass="button" Visible="False">Add new</asp:linkbutton>&nbsp;
			<asp:linkbutton id="btnDelete" runat="server" CssClass="button" Visible="False">Delete</asp:linkbutton>&nbsp;
			<asp:linkbutton id="btnExport" runat="server" CssClass="button">Export</asp:linkbutton>
			<asp:textbox id="txtStrLSLevel1ID" runat="server" Width="69px" CssClass="input" onChange="GetStatusOfControl('True')"
				Visible="False"></asp:textbox></TD>
	</TR>
	<TR>
		<TD style="HEIGHT: 10px" align="left" colSpan="5">&nbsp;
			<uc1:columnlist id="uctrlColumns" runat="server"></uc1:columnlist></TD>
	</TR>
	<TR>
		<TD colSpan="5"><asp:datagrid id="dtgList" Width="100%" runat="server" AllowPaging="True" AllowSorting="True"
				AutoGenerateColumns="False" CellPadding="0" BorderColor="WhiteSmoke" BackColor="White">
				<FooterStyle CssClass="gridFooter"></FooterStyle>
				<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
				<ItemStyle CssClass="gridItem"></ItemStyle>
				<HeaderStyle CssClass="gridHeader"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn HeaderText="Seq">
						<HeaderStyle Width="5%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<%# Container.ItemIndex + 1 + dtgList.PageSize*dtgList.CurrentPageIndex%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn Visible="False" DataField="LSLevel1ID"></asp:BoundColumn>
					<asp:ButtonColumn Visible="False" DataTextField="LSLevel1Code" SortExpression="LSLevel1Code" HeaderText="Code" CommandName="EDIT">
						<HeaderStyle Width="10%"></HeaderStyle>
						<ItemStyle Width="10%"></ItemStyle>
					</asp:ButtonColumn>
					<asp:BoundColumn DataField="LSLevel1Code" HeaderText="Code"></asp:BoundColumn>
					<asp:HyperLinkColumn DataTextField="Name" SortExpression="Name" HeaderText="Name">
						<HeaderStyle Width="10%"></HeaderStyle>
						<ItemStyle Width="10%"></ItemStyle>
					</asp:HyperLinkColumn>
					<asp:BoundColumn DataField="CustomerTypeName" SortExpression="CustomerTypeName" HeaderText="CusType">
						<HeaderStyle Width="18%"></HeaderStyle>
						<ItemStyle Width="18%"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="Agent" HeaderText="Agent">
						<HeaderStyle Width="20%"></HeaderStyle>
						<ItemStyle Width="20%"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="Address" HeaderText="Address">
						<HeaderStyle Width="20%"></HeaderStyle>
						<ItemStyle Width="20%"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="Phone" HeaderText="Phone">
						<HeaderStyle Width="10%"></HeaderStyle>
						<ItemStyle Width="10%"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="Fax" HeaderText="Fax"></asp:BoundColumn>
					<asp:BoundColumn DataField="Email" HeaderText="Email"></asp:BoundColumn>
					<asp:TemplateColumn Visible="False" HeaderText="Select">
						<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<HeaderTemplate>
							<asp:CheckBox id="chkSelectAll" runat="server" CssClass="checkbox" onclick="CheckAll('_ctl0_dtgList__ctl2_chkSelectAll','_ctl0_dtgList',3,1,'chkSelect')"></asp:CheckBox>
						</HeaderTemplate>
						<ItemTemplate>
							<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
			</asp:datagrid></TD>
	</TR>
</table>
<script>
	function CheckDelete()
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
</script>

