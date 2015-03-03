<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Salary.ascx.cs" Inherits="iHRPCore.MdlHR.Salary" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc" TagName="EmpHeader" Src="../Include/EmpHeader.ascx" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="98%" border="0">
	<TR>
		<TD colSpan="5"><asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center" colSpan="5"><uc:empheader id="HR_EmpHeader" runat="server"></uc:empheader></TD>
	</TR>
	<TR>
		<TD align="center"><!-- start detail for input form -->
			<TABLE cellSpacing="0" cellPadding="1" width="99%" border="0">
				<TR>
					<TD width="13%">
						<asp:label id="lblEffDate" runat="server" CssClass="labelRequire" Width="100%">Effective Date</asp:label></TD>
					<TD width="32%">
						<asp:textbox id="txtEffDate" runat="server" CssClass="input" Width="70%" MaxLength="10"></asp:textbox>
						<asp:imagebutton id="cmdCalEffDate" runat="server" ImageUrl="../Images/cal.gif"></asp:imagebutton></TD>
					<TD width="4%"></TD>
					<TD width="15%"></TD>
					<TD width="36%"></TD>
				</TR>
				<TR>
					<TD>
						<asp:label id="lblScale" runat="server" CssClass="label" Width="100%">Scale</asp:label></TD>
					<TD>
						<asp:dropdownlist id="cboScale" runat="server" CssClass="select" Width="70%">
							<asp:ListItem Value="Ngạch lương">Ngạch lương</asp:ListItem>
							<asp:ListItem Value="B&#236;nh thường">B&#236;nh thường</asp:ListItem>
						</asp:dropdownlist></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD>
						<asp:label id="lblGrade" runat="server" CssClass="label" Width="100%">Grade</asp:label></TD>
					<TD>
						<asp:dropdownlist id="cboGrade" runat="server" CssClass="select" Width="70%">
							<asp:ListItem Value="Bậc lương">Bậc lương</asp:ListItem>
							<asp:ListItem Value="B&#236;nh thường">B&#236;nh thường</asp:ListItem>
						</asp:dropdownlist></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD>
						<asp:label id="lblBasicSalary" runat="server" CssClass="labelRequire" Width="100%">Basic Salary</asp:label></TD>
					<TD>
						<asp:textbox id="txtBasicSalary" runat="server" CssClass="input" Width="70%" MaxLength="30"></asp:textbox>
						<asp:dropdownlist id="cboCurBasicSalary" runat="server" CssClass="select" Width="26%">
							<asp:ListItem Value="Tiền tệ">Tiền tệ</asp:ListItem>
						</asp:dropdownlist></TD>
					<TD></TD>
					<TD>
						<asp:label id="lblContractSalary" runat="server" CssClass="labelRequire" Width="100%" Visible="False">Contract Salary</asp:label></TD>
					<TD>
						<asp:textbox id="txtContractSalary" runat="server" CssClass="input" Width="70%" MaxLength="30"
							Visible="False"></asp:textbox>
						<asp:dropdownlist id="cboCurContractSalary" runat="server" CssClass="select" Width="26%" Visible="False">
							<asp:ListItem Value="Tiền tệ">Tiền tệ</asp:ListItem>
						</asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD colSpan="4"><INPUT id="optDec_N" onclick="optDecisionChange()" tabIndex="2" type="radio" value="optDec_N"
							name="IsDec" runat="server">
						<asp:label id="lblDec_N" runat="server" CssClass="option" Width="120px" Font-Names="Arial"
							Font-Size="8pt">No Decision</asp:label><INPUT id="optDec_Y" onclick="optDecisionChange()" tabIndex="3" type="radio" CHECKED value="optDec_Y"
							name="IsDec" runat="server">
						<asp:label id="lblDec_Y" runat="server" CssClass="option" Width="91px" Font-Names="Arial" Font-Size="8pt">With Decision</asp:label></TD>
				</TR>
				<TR>
					<TD>
						<asp:label id="lblDecisionNo" runat="server" CssClass="labelRequire" Width="100%">Decision No</asp:label></TD>
					<TD>
						<asp:textbox id="txtDecisionNo" runat="server" CssClass="input" Width="70%" MaxLength="10"></asp:textbox></TD>
					<TD></TD>
					<TD>
						<asp:label id="lblSignDate" runat="server" CssClass="labelRequire" Width="100%">Sign Date</asp:label></TD>
					<TD>
						<asp:textbox id="txtSignDate" runat="server" CssClass="input" Width="70%" MaxLength="10"></asp:textbox>
						<asp:imagebutton id="cmdCalSignDate" runat="server" ImageUrl="../Images/cal.gif" DESIGNTIMEDRAGDROP="184"></asp:imagebutton></TD>
				</TR>
				<TR>
					<TD>
						<asp:label id="lblSigner" runat="server" CssClass="labelRequire" Width="100%">Signer</asp:label></TD>
					<TD>
						<asp:textbox id="txtSigner" runat="server" CssClass="input" Width="70%" MaxLength="10"></asp:textbox></TD>
					<TD></TD>
					<TD>
						<asp:label id="lblSignerPos" runat="server" CssClass="label" Width="100%">Position of Signer</asp:label></TD>
					<TD>
						<asp:textbox id="txtSignerPos" runat="server" CssClass="input" Width="70%" MaxLength="10"></asp:textbox></TD>
				</TR>
				<TR>
					<TD>
						<asp:label id="lblNote" runat="server" CssClass="label" Width="100%">Note</asp:label></TD>
					<TD colSpan="4">
						<asp:textbox id="txtNote" runat="server" CssClass="input" Width="87.5%" MaxLength="255"></asp:textbox></TD>
				</TR>
			</TABLE> <!-- end button for input form -->
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
					<TD vAlign="middle" width="15%"><asp:checkbox id="chkShowGrid" onclick="javascript:collapse('tblGrid');" CssClass="checkbox" runat="server"
							Text="Show grid" Checked="True" accessKey="G" ToolTip="Alt+G"></asp:checkbox></TD>
					<TD align="center" width="*"><asp:linkbutton id="btnAddNew" accessKey="N" CssClass="btnAddnew" runat="server" ToolTip="Alt+N">AddNew</asp:linkbutton>&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnSave" accessKey="S" CssClass="btnSave" runat="server" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp;
						<asp:linkbutton id="btnDelete" accessKey="D" CssClass="btnDelete" runat="server" ToolTip="Alt + D">Delete</asp:linkbutton>&nbsp;
						<asp:linkbutton id="btnList" accessKey="L" CssClass="btnList" runat="server" ToolTip="Alt + L">List</asp:linkbutton>&nbsp;</TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<!-- end button for input form -->
	<TR>
		<TD align="center"><!-- start grid for input form -->
			<TABLE id="tblGrid" cellSpacing="0" cellPadding="1" width="98%" border="0">
				<TR>
					<TD width="40%"><asp:label id="Label12" CssClass="label" runat="server">Page rows</asp:label>&nbsp;&nbsp;
						<asp:textbox id="txtPageRows" CssClass="input" runat="server" Width="35px" MaxLength="3">20</asp:textbox>&nbsp;&nbsp;&nbsp;
						<asp:label id="Label13" CssClass="label" runat="server">Total rows</asp:label>&nbsp;
						<asp:label id="lblTotalRows" CssClass="labelData" runat="server" Width="35px">500</asp:label></TD>
					<TD align="right" width="*">&nbsp;
						<asp:linkbutton id="btnImport" accessKey="I" CssClass="btnImport" runat="server" ToolTip="Alt+I, Import data from excel file same format with export file">Import</asp:linkbutton>&nbsp;
						<asp:linkbutton id="btnExport" accessKey="E" CssClass="btnExport" runat="server" ToolTip="Alt+E, Export data in grid to excel file">Export</asp:linkbutton>
					</TD>
				</TR>
				<TR id="trGrid"> <!-- start grid detail for input form -->
					<TD colSpan="2"><asp:datagrid id="dtgSalary" CssClass="grid" runat="server" Width="100%" BackColor="White" BorderColor="#3366CC"
							BorderWidth="1px" CellPadding="0" AllowPaging="True" AutoGenerateColumns="False" AllowSorting="True" PageSize="7">
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle CssClass="gridHeader"></HeaderStyle>
							<FooterStyle CssClass="gridFooter"></FooterStyle>
							<Columns>
								<asp:BoundColumn DataField="No" HeaderText="Seq">
									<HeaderStyle HorizontalAlign="Center" Width="5%" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn HeaderText="Effective Date">
									<HeaderStyle Width="35%" HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn HeaderText="Basic Salary">
									<HeaderStyle Width="35%" HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn HeaderText="With Decision">
									<HeaderStyle Width="20%" HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Select">
									<HeaderStyle Width="5%" HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox id="Checkbox2" runat="server" CssClass="checkbox"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE> <!-- end grid for input form -->
		</TD>
	</TR>
</TABLE>
