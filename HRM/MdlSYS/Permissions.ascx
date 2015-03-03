<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Permissions.ascx.cs" Inherits="iHRPCore.MdlSYS.Permissions" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="98%" border="0">
	<TR>
		<TD align="center"><asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center">
			<table width="100%">
				<tr>
					<td width="12%"><asp:label id="Label1" CssClass="label" runat="server">Account/Group</asp:label></td>
					<td width="35%"><asp:textbox id="txtEmpID" CssClass="input" runat="server" Width="89%" MaxLength="10"></asp:textbox><INPUT class="search" onclick="javascript:OpenWindowEmp()" type="button" value="...">
					</td>
					<td width="5%"></td>
					<td colSpan="2"><asp:checkbox id="chkCopy" onclick="DisplayCombo()" CssClass="checkbox" runat="server" Text="Copy from group"></asp:checkbox><asp:dropdownlist id="cboGroup" style="DISPLAY: none" CssClass="combo" runat="server" Width="100%"></asp:dropdownlist></td>
				</tr>
			</table>
		</TD>
	</TR>
	<TR>
		<TD vAlign="top" noWrap align="center" height="20">
			<HR align="center" width="100%">
		</TD>
	</TR>
	<TR>
		<TD noWrap align="center">
			<table width="100%">
				<TBODY>
					<tr>
						<td width="45%">
							<table width="100%">
								<tr>
									<td width="20%"><asp:label id="Label7" CssClass="label" runat="server">Module</asp:label></td>
									<td><asp:dropdownlist id="cboLevel2" CssClass="combo" runat="server" Width="100%"></asp:dropdownlist></td>
								</tr>
								<tr>
									<td><asp:label id="Label3" CssClass="label" runat="server">Group</asp:label></td>
									<td><asp:dropdownlist id="Dropdownlist1" CssClass="combo" runat="server" Width="100%"></asp:dropdownlist></td>
								</tr>
							</table>
						</td>
						<td align="center" width="5%" rowSpan="3"><asp:linkbutton id="Linkbutton7" CssClass="btn" runat="server">></asp:linkbutton><BR>
							<asp:linkbutton id="Linkbutton8" CssClass="btn" runat="server"><</asp:linkbutton></td>
						<td vAlign="top" rowSpan="3"><asp:label id="Label4" CssClass="label" runat="server">Assigned function</asp:label>
							<DIV style="OVERFLOW: auto; HEIGHT: 280px"><asp:datagrid id="dtgFunctionSelected" CssClass="grid" runat="server" Width="100%" BackColor="White"
									BorderColor="#3366CC" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False" AllowSorting="True">
									<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
									<ItemStyle CssClass="gridItem"></ItemStyle>
									<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
									<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
									<Columns>
										<asp:BoundColumn Visible="False" DataField="No">
											<HeaderStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Middle"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn HeaderText="Function">
											<HeaderStyle Width="55%"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
										</asp:BoundColumn>
										<asp:TemplateColumn HeaderText="View">
											<HeaderStyle Width="15%"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
											<ItemTemplate>
												<asp:CheckBox id="chkView" runat="server" CssClass="checkbox"></asp:CheckBox>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn HeaderText="Edit">
											<HeaderStyle Width="15%"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
											<ItemTemplate>
												<asp:CheckBox id="chkUpdate" runat="server" CssClass="checkbox"></asp:CheckBox>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn HeaderText="Delete">
											<HeaderStyle Width="14%"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
											<ItemTemplate>
												<asp:CheckBox id="Checkbox1" runat="server" CssClass="checkbox"></asp:CheckBox>
											</ItemTemplate>
										</asp:TemplateColumn>
									</Columns>
									<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
								</asp:datagrid></DIV>
						</td>
					</tr>
					<TR>
						<TD style="HEIGHT: 20px" width="35%"><asp:label id="Label2" CssClass="label" runat="server">Function list</asp:label></TD>
		</TD>
	</TR>
	<TR>
		<TD vAlign="top"><asp:listbox id="ListBox1" CssClass="ListBox" runat="server" Width="100%" Height="240px"></asp:listbox></TD>
	</TR>
	<TR>
		<TD vAlign="top"></TD>
		<TD vAlign="middle" align="center"></TD>
		<TD vAlign="top" align="center"><asp:linkbutton id="Linkbutton11" CssClass="btn" runat="server">Save</asp:linkbutton></TD>
	</TR>
</TABLE>
</TD></TR> 
<!-- end button for input form -->
<TR style="DISPLAY: none">
	<TD align="left"><!-- start grid for input form -->
		<TABLE id="tblGrid" cellSpacing="0" cellPadding="1" width="98%" border="0">
			<TR id="trGrid" align="center"> <!-- start grid detail for input form -->
				<td colSpan="10"></td>
				<TD></TD>
			</TR>
		</TABLE> <!-- end grid for input form --></TD>
</TR>
</TBODY></TABLE>
<script language="javascript">
	function OpenWindowEmp()
	{
		CurWindow = window.open('FormPage.aspx?Ascx=MdlSYS/AccountList.ascx','SearchEmp','toolbar=0,status=no,menubar=no,scrollbars=1,resizable=yes,copyhistory=yes,width=650,height=500,align=top');
		CurWindow.focus();
	} 
	function DisplayCombo()
	{
		if(document.getElementById("_ctl0:chkCopy").checked == true)
			document.getElementById("_ctl0:cboGroup").style.display = "";
		else
			document.getElementById("_ctl0:cboGroup").style.display = "none";
	}
</script>
