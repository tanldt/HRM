<%@ Register TagPrefix="uc1" TagName="ColumnList" Src="../../Include/ColumnList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../../Include/EmpHeaderSearch.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="RPT_ContractList.ascx.cs" Inherits="MdlSER.RPT_ContractList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table width="100%" align="center">
	<tr>
		<TD style="WIDTH: 75px"><asp:label id="lblGroup" ToolTip="Customer Group" runat="server" Width="100%"> Group</asp:label></TD>
		<TD style="WIDTH: 236px"><asp:dropdownlist id="cboLSCompanyID" runat="server" Width="100%" CssClass="select">
				<asp:ListItem></asp:ListItem>
				<asp:ListItem Value="1">KPMG (local)</asp:ListItem>
				<asp:ListItem Value="2">Finance</asp:ListItem>
			</asp:dropdownlist></TD>
		<TD></TD>
		<TD style="WIDTH: 57px"><asp:label id="Label5" ToolTip="Customer Type" runat="server" Width="100%">Type</asp:label></TD>
		<TD style="WIDTH: 236px"><asp:dropdownlist id="cboCustomerTypeID" runat="server" Width="100%" CssClass="select">
				<asp:ListItem></asp:ListItem>
				<asp:ListItem Value="1">Service</asp:ListItem>
				<asp:ListItem Value="2">Travel</asp:ListItem>
			</asp:dropdownlist></TD>
	</tr>
	<TR>
		<TD><asp:label id="lblCode" ToolTip="Customer Code" runat="server" Width="100%"> Cust Code</asp:label></TD>
		<TD><asp:textbox id="txtLSLevel1Code" runat="server" Width="100%" CssClass="input"></asp:textbox></TD>
		<TD></TD>
		<TD><asp:label id="lblName" ToolTip="Customer Name" runat="server" Width="100%"> Cust Name</asp:label></TD>
		<TD><asp:textbox id="txtLSLevel1Name" runat="server" Width="100%" CssClass="input"></asp:textbox></TD>
	</TR>
	<TR>
		<TD><asp:label id="txtContract" runat="server"> Contract Status</asp:label></TD>
		<TD><asp:dropdownlist id="cboContractStatus" runat="server" CssClass="select">
				<asp:ListItem></asp:ListItem>
				<asp:ListItem Value="1">Valid</asp:ListItem>
				<asp:ListItem Value="0">Expired</asp:ListItem>
			</asp:dropdownlist><asp:textbox id="txtStrListID" runat="server" Visible="False">txtStrListID</asp:textbox></TD>
		<TD></TD>
		<TD><asp:label id="txtDOSign" runat="server">Sign Date</asp:label></TD>
		<TD><asp:textbox id="txtSignDateFrom" onblur="JavaScript:CheckDate(this)" runat="server" Width="88px"
				CssClass="input" MaxLength="12"></asp:textbox>&nbsp;<INPUT class=btnCal onclick="javascript:popUpCalendar(<%=this.txtSignDateFrom.ClientID%>)" type=button>
			<asp:label id="Label9" runat="server">To</asp:label><asp:textbox id="txtSignDateTo" onblur="JavaScript:CheckDate(this)" runat="server" Width="88px"
				CssClass="input" MaxLength="12"></asp:textbox>&nbsp;<INPUT class=btnCal onclick="javascript:popUpCalendar(<%=this.txtSignDateTo.ClientID%>)" type=button></TD>
	</TR>
	<tr>
		<TD><asp:label id="Label6" runat="server">Contract Expiry</asp:label></TD>
		<TD><asp:textbox id="txtContractExpireFrom" onblur="JavaScript:CheckDate(this)" runat="server" Width="88px"
				CssClass="input" MaxLength="12"></asp:textbox>&nbsp;<INPUT class=btnCal onclick="javascript:popUpCalendar(<%=this.txtContractExpireFrom.ClientID%>)" type=button>
			<asp:label id="Label7" runat="server">To</asp:label><asp:textbox id="txtContractExpireTo" onblur="JavaScript:CheckDate(this)" runat="server" Width="88px"
				CssClass="input" MaxLength="12"></asp:textbox>&nbsp;<INPUT class=btnCal onclick="javascript:popUpCalendar(<%=this.txtContractExpireTo.ClientID%>)" type=button></TD>
		<TD></TD>
		<TD><asp:label id="Label4" runat="server" Width="100%">SAN expire</asp:label></TD>
		<TD><asp:textbox id="txtSANExpireFrom" onblur="JavaScript:CheckDate(this)" runat="server" Width="88px"
				CssClass="input" MaxLength="12"></asp:textbox>&nbsp;<INPUT class=btnCal onclick="javascript:popUpCalendar(<%=this.txtSANExpireFrom.ClientID%>)" type=button>
			<asp:label id="lblSanTo" runat="server">To</asp:label><asp:textbox id="txtSANExpireTo" onblur="JavaScript:CheckDate(this)" runat="server" Width="88px"
				CssClass="input" MaxLength="12"></asp:textbox>&nbsp;<INPUT class=btnCal onclick="javascript:popUpCalendar(<%=this.txtSANExpireTo.ClientID%>)" type=button></TD>
	</tr>
	<TR>
		<TD style="HEIGHT: 10px" align="center" colSpan="5">
			<HR align="center" width="100%">
		</TD>
	</TR>
	<TR>
		<TD align="center" colSpan="5"><asp:linkbutton id="btnAddNew" accessKey="N" ToolTip="Alt+N" runat="server" CssClass="btnAddnew"
				Visible="False">Add New</asp:linkbutton>&nbsp;
			<asp:linkbutton id="btnSearch" accessKey="N" ToolTip="Alt+N" runat="server" CssClass="btnAddnew">View</asp:linkbutton>&nbsp;
			<asp:linkbutton id="btnDelete" accessKey="N" ToolTip="Alt+N" runat="server" CssClass="btnAddnew"
				Visible="False">Delete</asp:linkbutton>&nbsp;
			<asp:linkbutton id="btnExport" accessKey="N" ToolTip="Alt+N" runat="server" CssClass="btnAddnew">Export</asp:linkbutton></TD>
	</TR>
	<TR>
		<TD colSpan="5"><uc1:columnlist id="uctrlColumns" runat="server"></uc1:columnlist></TD>
	</TR>
	<TR>
		<TD colSpan="5"><asp:datagrid id="dtgList" runat="server" Width="100%" CssClass="grid" BorderColor="White" BackColor="White"
				CellPadding="0" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True">
				<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
				<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
				<ItemStyle CssClass="gridItem"></ItemStyle>
				<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn HeaderText="Select">
						<HeaderStyle Width="8%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn Visible="False" DataField="ContractID" HeaderText="ContractID"></asp:BoundColumn>
					<asp:TemplateColumn HeaderText="Seq">
						<HeaderStyle Width="5%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<%# Container.ItemIndex + 1 + dtgList.PageSize*dtgList.CurrentPageIndex%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn Visible="False" DataField="LSLevel1ID" HeaderText="LSLevel1ID"></asp:BoundColumn>
					<asp:BoundColumn DataField="LSLevel1Name" SortExpression="LSLevel1Name" HeaderText="Customer">
						<HeaderStyle Width="16%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Left"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="ContractNo" HeaderText="ContractNo"></asp:BoundColumn>
					<asp:ButtonColumn Visible="False" DataTextField="ContractNo" SortExpression="ContractNo" HeaderText="ContractNo"
						CommandName="EDIT">
						<HeaderStyle Width="13%"></HeaderStyle>
					</asp:ButtonColumn>
					<asp:BoundColumn DataField="value" HeaderText="Value">
						<HeaderStyle Width="10%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="ContractDate" HeaderText="Contract Date">
						<HeaderStyle Width="21%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Left"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="SANDate" HeaderText="SAN Date">
						<HeaderStyle Width="21%"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="SignDate" HeaderText="SignDate">
						<HeaderStyle Width="9%"></HeaderStyle>
					</asp:BoundColumn>
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
