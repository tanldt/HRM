<%@ Register TagPrefix="uc" TagName="EmpHeader" Src="../Include/EmpHeader.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="BorrowPassport.ascx.cs" Inherits="iHRPCore.MdlHR.BorrowPassport" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="97%" border="0">
	<TR>
		<TD><asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center"><uc:empheader id="HR_EmpHeader" runat="server"></uc:empheader></TD>
	</TR>
	<tr>
		<TD align="center"><!-- start detail for input form -->
			<TABLE cellSpacing="0" cellPadding="1" width="99%" border="0">
				<TR>
				</TR>
				<TR>
					<TD width="15%">
						<asp:label id="lblFromDate" runat="server" CssClass="labelRequire">From Date</asp:label></TD>
					<TD width="32%" valign="top">
						<asp:textbox id="txtFromDate" onblur="JavaScript:CheckDate(this)" runat="server" CssClass="input"
							Width="112px" MaxLength="10"></asp:textbox><INPUT class="btnCal" onclick="ShowCalendar('_ctl0_txtFromDate')" type="button"></TD>
					<TD width="4%"></TD>
					<TD width="15%">
						<asp:label id="lblToDate" runat="server" CssClass="labelRequire" Width="104px">To Date</asp:label></TD>
					<TD width="37%" valign="top">
						<asp:textbox id="txtToDate" onblur="JavaScript:CheckDate(this)" runat="server" CssClass="input"
							MaxLength="10" Width="112px"></asp:textbox><INPUT class="btnCal" onclick="ShowCalendar('_ctl0_txtFromDate')" type="button"></TD>
				</TR>
				<TR>
				</TR>
				<TR>
				</TR>
				<TR>
					<TD>
						<asp:label id="lblReason" runat="server" CssClass="label" Width="100%">Reason</asp:label></TD>
					<TD colSpan="4" valign="top">
						<asp:textbox id="txtHealth" runat="server" CssClass="input" Width="100%" MaxLength="500"></asp:textbox></TD>
				</TR>
				<TR>
				</TR>
				<TR>
					<TD>
						<asp:label id="lblNote" runat="server" CssClass="label" Width="100%">Note</asp:label></TD>
					<TD colSpan="4" valign="top">
						<asp:textbox id="txtNote" runat="server" CssClass="input" Width="100%" MaxLength="100" Rows="2"></asp:textbox></TD>
				</TR>
			</TABLE> <!-- end button for input form --><INPUT id="txtWorkingBackgroundID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1"
				name="txtWorkingBackgroundID" runat="server">
		</TD>
	</tr>
	<TR>
		<TD vAlign="top" noWrap align="center" height="20">
			<HR align="center" width="95%">
		</TD>
	</TR>
	<TR>
		<TD noWrap align="center">
			<TABLE id="tblButton" cellSpacing="0" cellPadding="0" width="99%" border="0">
				<TR vAlign="top" height="30">
					<TD vAlign="middle" width="15%">
						<asp:checkbox id="chkShowGrid" accessKey="G" onclick="javascript:collapse('_ctl0_tdGrid');" runat="server"
							CssClass="checkbox" ToolTip="Alt+G" Checked="True" Text="Show grid"></asp:checkbox></TD>
					<TD align="center" width="*">
						<asp:linkbutton id="btnAddNew" accessKey="N" runat="server" CssClass="btnAddnew" ToolTip="Alt+N">AddNew</asp:linkbutton>&nbsp;&nbsp;
						<asp:linkbutton id="btnSave" accessKey="S" runat="server" CssClass="btnSave" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnList" accessKey="L" runat="server" CssClass="btnList" ToolTip="Alt + L">List</asp:linkbutton>&nbsp;&nbsp;
						<asp:linkbutton id="btnDelete" accessKey="D" runat="server" CssClass="btnDelete" ToolTip="Alt + D">Delete</asp:linkbutton>&nbsp;&nbsp;
						<asp:linkbutton id="btnExport" accessKey="E" CssClass="btnExport" runat="server" ToolTip="Alt+E, Export data in grid to excel file">Export</asp:linkbutton></TD>
				</TR>
			</TABLE>
		</TD>
	</TR> <!-- end button for input form -->
	<TR>
		<TD runat="server" id="tdGrid" align="center"><!-- start grid for input form -->
			<TABLE id="tblGrid" cellSpacing="0" cellPadding="1" width="100%" border="0">
				<TR id="trGrid"> <!-- start grid detail for input form -->
					<TD colSpan="2">
						<asp:datagrid id="dtgList" runat="server" CssClass="grid" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
							CellPadding="0" BorderWidth="1px" BorderColor="#3366CC" BackColor="White">
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
							<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="&lt;b&gt;Seq&lt;/b&gt;">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<%# Container.ItemIndex + 1%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="From Date">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton Runat="server" ID="btn" CssClass="HLink" CommandName="Edit">
											<%# DataBinder.Eval(Container, "DataItem.FromDate")%>
										</asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn HeaderText="To Date">
									<HeaderStyle Width="15%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn HeaderText="Reason">
									<HeaderStyle Width="25%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Select">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE> <!-- end grid for input form --></TD>
	</TR>
</TABLE>
