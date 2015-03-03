<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Accident.ascx.cs" Inherits="iHRPCore.MdlHR.Accident" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc" TagName="EmpHeader" Src="../Include/EmpHeader.ascx" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="97%" border="0">
	<TBODY>
		<TR>
			<TD align="center"><asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></TD>
		</TR>
		<TR>
			<TD align="center"><uc:empheader id="HR_EmpHeader" runat="server"></uc:empheader></TD>
		</TR>
		<TR>
			<TD align="center">
				<table width="100%">
					<TR>
						<TD width="12%"><asp:label id="lblAccidentDate" runat="server" CssClass="label"> Accident Date</asp:label></TD>
						<TD width="38%">
							<asp:textbox id="Textbox3" CssClass="input" runat="server" Width="70px"></asp:textbox><INPUT class="btnCal" onclick="ShowCalendar('_ctl0_txtSignDate')" type="button">&nbsp;-&nbsp;
							<asp:textbox id="Textbox5" CssClass="input" runat="server" Width="70px"></asp:textbox><INPUT class="btnCal" onclick="ShowCalendar('_ctl0_txtSignDate')" type="button"></TD>
						<TD width="12%">
							<asp:linkbutton id="btnSearch" accessKey="E" CssClass="btnSearch" runat="server" Width="100%" ToolTip="Alt+E, Xuất dl trên lưới ra file Excel">Search</asp:linkbutton></TD>
						<TD width="38%"></TD>
					</TR>
					<TR>
						<TD colspan="4">
							<HR width="100%" SIZE="1">
						</TD>
					</TR>
					<TR>
						<TD width="12%">
							<asp:label id="Label1" CssClass="label" runat="server">Accident Type</asp:label></TD>
						<TD width="38%">
							<asp:dropdownlist id="cboLSAccidentTypeID" CssClass="combo" runat="server" Width="232px"></asp:dropdownlist></TD>
						<TD width="12%"></TD>
						<TD width="38%"></TD>
					</TR>
					<tr>
						<td width="12%"><asp:label id="lblDate" runat="server" CssClass="label"> Date</asp:label></td>
						<td width="38%">
							<asp:textbox id="Textbox1" CssClass="input" runat="server" Width="70px"></asp:textbox><INPUT class="btnCal" onclick="ShowCalendar('_ctl0_txtSignDate')" type="button"></td>
						<td width="12%">
							<P><asp:label id="lblDocumentNumber" runat="server" CssClass="label">Document No</asp:label></P>
						</td>
						<td width="38%"><asp:textbox id="txtDocumentNumber" runat="server" CssClass="input" Width="95%"></asp:textbox></td>
					</tr>
					<tr>
						<td><asp:label id="lblSituation" runat="server" CssClass="label" Width="100%">Situation</asp:label></td>
						<td><asp:textbox id="txtSituation" runat="server" CssClass="input" Width="95%"></asp:textbox></td>
						<td><asp:label id="lblRank" runat="server" CssClass="label">Rank</asp:label></td>
						<td><asp:textbox id="txtRank" runat="server" CssClass="input" Width="95%"></asp:textbox></td>
					</tr>
					<TR>
						<TD><asp:label id="lblLocation" runat="server" CssClass="label">Location</asp:label></TD>
						<TD colspan="3"><asp:textbox id="txtLocation" runat="server" CssClass="input" Width="98%" MaxLength="20" TextMode="MultiLine"></asp:textbox></TD>
					</TR>
					<TR>
						<TD><asp:label id="lblNote" runat="server" CssClass="label">Note</asp:label></TD>
						<TD colSpan="3"><asp:textbox id="txtNote" runat="server" CssClass="input" Width="98%"></asp:textbox></TD>
					</TR>
					<TR>
						<TD colSpan="4" align="center">
							<HR align="center" width="100%">
						</TD>
					</TR>
					<TR>
						<TD align="center" colSpan="4">
							<asp:linkbutton id="btnAddNew" accessKey="N" CssClass="btnAddnew" runat="server" ToolTip="Alt+N">Add New</asp:linkbutton>&nbsp;&nbsp;
							<asp:linkbutton id="btnSave" accessKey="S" CssClass="btnSave" runat="server" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp;&nbsp;
							<asp:linkbutton id="btnDelete" accessKey="D" CssClass="btnDelete" runat="server" ToolTip="Alt + D">Delete</asp:linkbutton>&nbsp;&nbsp;&nbsp;&nbsp;
							<asp:linkbutton id="btnList" accessKey="L" CssClass="btnList" runat="server" ToolTip="Alt + L">List</asp:linkbutton>&nbsp;&nbsp;&nbsp;
							<asp:linkbutton id="btnExport" accessKey="E" CssClass="btnExport" runat="server" ToolTip="Alt+E, Xuất dl trên lưới ra file Excel"> Export</asp:linkbutton></TD>
					</TR>
				</table>
			</TD>
		</TR>
		<tr>
			<TD align="center"><!-- start detail for input form -->
				<asp:datagrid id="dtgList" runat="server" CssClass="grid" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
					CellPadding="0" BorderWidth="1px" BorderColor="#3366CC" BackColor="White" AllowPaging="True">
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
						<asp:BoundColumn DataField="Date" HeaderText="Date">
							<HeaderStyle Width="15%"></HeaderStyle>
						</asp:BoundColumn>
						<asp:BoundColumn HeaderText="Number">
							<HeaderStyle Width="10%"></HeaderStyle>
						</asp:BoundColumn>
						<asp:BoundColumn HeaderText="Situation">
							<HeaderStyle Width="30%"></HeaderStyle>
						</asp:BoundColumn>
						<asp:BoundColumn HeaderText="Rank">
							<HeaderStyle Width="30%"></HeaderStyle>
						</asp:BoundColumn>
						<asp:TemplateColumn HeaderText="Select">
							<HeaderStyle Width="7%"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<ItemTemplate>
								<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
					<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
				</asp:datagrid> <!-- end button for input form --></TD>
		</tr>
	</TBODY>
</TABLE>
