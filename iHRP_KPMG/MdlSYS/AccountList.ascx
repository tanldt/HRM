<%@ Register TagPrefix="uc1" TagName="AccountHeaderSearch" Src="../Include/AccountHeaderSearch.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="AccountList.ascx.cs" Inherits="iHRPCore.MdlSYS.AccountList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../Include/EmpHeaderSearch.ascx" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="98%" border="0">
	<TR>
		<TD align="center"><asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></TD>
	</TR>
	<TR>
		<TD vAlign="top" noWrap align="center">
			<table>
				<TR>
					<TD width="12%"><asp:label id="Label1" runat="server" CssClass="label">Account/GroupID</asp:label></TD>
					<TD width="30%"><asp:textbox id="txtEmpID" runat="server" CssClass="input" Width="90%"></asp:textbox></TD>
					<td width="5%"><asp:label id="Label2" runat="server" CssClass="label">Name</asp:label></td>
					<TD width="33%"><asp:textbox id="txtEmpName" runat="server" CssClass="input" Width="100%"></asp:textbox></TD>
					<TD><asp:checkbox id="chkUser" runat="server" CssClass="checkbox" Text="User" BorderStyle="None"></asp:checkbox>&nbsp;&nbsp;
						<asp:checkbox id="chkGroup" runat="server" CssClass="checkbox" Text="Group"></asp:checkbox></TD>
				</TR>
			</table>
		</TD>
	</TR>
	<TR>
		<TD vAlign="top" noWrap align="center" height="20">
			<HR align="center" width="95%">
		</TD>
	</TR>
	<TR>
		<TD noWrap align="center">
			<TABLE id="tblButton" cellSpacing="0" cellPadding="0" width="99%" border="0">
				<TR vAlign="top" height="30">
					<TD align="center" width="*" colSpan="2"><asp:linkbutton id="btnSearch" accessKey="N" runat="server" CssClass="btnSearch" ToolTip="Alt+S">Search</asp:linkbutton>&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnSelect" accessKey="X" runat="server" CssClass="btn">Select</asp:linkbutton>&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnClose" accessKey="C" runat="server" CssClass="btn">Close</asp:linkbutton></TD>
				</TR>
				<TR>
					<TD colSpan="2">
						<table width="100%">
							<TR>
								<TD align="center"><!-- start grid for list form -->
									<TABLE id="tblGrid" cellSpacing="0" cellPadding="1" width="99%" border="0">
										<TR>
											<TD id="tdPages" width="43%"><asp:label id="Label4" runat="server" CssClass="label">Page rows</asp:label>&nbsp;&nbsp;
												<asp:textbox id="txtPageRows" runat="server" CssClass="input" Width="35px" MaxLength="3">20</asp:textbox>&nbsp;&nbsp;&nbsp;
												<asp:label id="Label7" runat="server" CssClass="label">Total rows</asp:label>&nbsp;
												<asp:label id="lblTotalRows" runat="server" CssClass="labelData">500</asp:label>&nbsp;&nbsp;
												<asp:checkbox id="chkSort" onclick="javascript:collapse('tdSort');" runat="server" CssClass="checkbox"
													Text="Multi Sort"></asp:checkbox></TD>
											<TD id="tdSort" style="DISPLAY: none" align="right" width="*"><asp:label id="Label8" runat="server" CssClass="labelRight" Width="30px">  Col 1</asp:label><asp:dropdownlist id="cboCol1" runat="server" CssClass="combo" Width="80px"></asp:dropdownlist><asp:label id="Label10" runat="server" CssClass="labelRight" Width="30px">Col 2</asp:label>&nbsp;
												<asp:dropdownlist id="cboCol2" runat="server" CssClass="combo" Width="80px"></asp:dropdownlist><asp:label id="Label15" runat="server" CssClass="labelRight" Width="30px">Col 3</asp:label>&nbsp;
												<asp:dropdownlist id="cboCol3" runat="server" CssClass="combo" Width="80px"></asp:dropdownlist></TD>
										</TR>
										<TR id="trGrid"> <!-- start grid detail for list form -->
											<TD colSpan="2">
												<DIV style="OVERFLOW: auto; HEIGHT: 250px"><asp:datagrid id="dtgList" runat="server" CssClass="grid" Width="100%" AllowPaging="True" BackColor="White"
														BorderColor="#3366CC" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False" AllowSorting="True" PageSize="20">
														<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
														<ItemStyle CssClass="gridItem"></ItemStyle>
														<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
														<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
														<Columns>
															<asp:BoundColumn Visible="False" DataField="No">
																<HeaderStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Middle"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn Visible="False" HeaderText="Account">
																<HeaderStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Middle"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:BoundColumn>
															<asp:TemplateColumn HeaderText="Account">
																<HeaderStyle Width="10%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
																<ItemTemplate>
																	<asp:LinkButton id="hpLink" Width="95%" Runat="server"></asp:LinkButton>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:BoundColumn HeaderText="Full Name">
																<HeaderStyle Width="20%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn HeaderText="Job related">
																<HeaderStyle Width="20%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:BoundColumn>
														</Columns>
														<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
													</asp:datagrid></DIV>
											</TD>
										</TR>
									</TABLE> <!-- end grid for input form --></TD>
							</TR>
						</table>
					</TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<!-- end button for input form --></TABLE>
