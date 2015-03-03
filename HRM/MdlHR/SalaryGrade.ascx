<%@ Register TagPrefix="uc1" TagName="EmpHeader" Src="../Include/EmpHeader.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="SalaryGrade.ascx.cs" Inherits="iHRPCore.MdlHR.SalaryGrade" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Import Namespace ="System.Data.SqlClient" %>
<%@ Import Namespace ="System.Data" %>
<%@ Import Namespace ="iHRPCore.Com"%>
<TABLE id="MainTable" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD align="center" width="100%" colSpan="3"><asp:label id="lblErr" ForeColor="Red" runat="server" CssClass="lblErr"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center" width="100%" colSpan="3"><uc1:empheader id="HR_EmpHeader" runat="server"></uc1:empheader></TD>
	</TR>
	<TR>
		<TD align="right" width="45%"><asp:radiobutton id="opt1" CssClass="option" Checked="True" GroupName="option" Runat="server" Text="Basic Salary Information"></asp:radiobutton></TD>
		<td width="10%"></td>
		<td align="left" width="45%"><asp:radiobutton id="opt2" CssClass="option" GroupName="option" Runat="server" Text="Position Salary Information"></asp:radiobutton></td>
	</TR>
</TABLE>
<table width="100%">
	<tr>
		<td>
			<!-- Basic Salary Information-->
			<table id="tbl1" style="DISPLAY: block" cellSpacing="0" cellPadding="1" width="100%" border="0">
				<TR>
					<TD width="15%"><asp:label id="Label19" runat="server" CssClass="label">Effective date</asp:label></TD>
					<TD width="30%"><asp:textbox id="txtEffDate_Basic" runat="server" CssClass="input" Width="70px" MaxLength="10"></asp:textbox><INPUT class="btnCal" onclick="ShowCalendar('_ctl0_txtDOB')" type="button"></TD>
					<TD width="10%"></TD>
					<TD width="15%"><asp:label id="Label25" runat="server" CssClass="label">End date</asp:label></TD>
					<TD style="WIDTH: 227px" width="227"><asp:textbox id="txtEndDate_Basic" runat="server" CssClass="input" Width="70px" MaxLength="10"></asp:textbox><INPUT class="btnCal" onclick="ShowCalendar('_ctl0_txtDOB')" type="button"></TD>
				</TR>
				<TR>
					<TD width="15%"><asp:label id="Label1" runat="server" CssClass="label">Decision No</asp:label></TD>
					<TD width="30%"><asp:textbox id="txtDecision_Basic" runat="server" CssClass="input" Width="264px" MaxLength="255"></asp:textbox></TD>
					<TD width="10%"></TD>
					<TD width="15%"><asp:label id="Label9" runat="server" CssClass="label">Sign Date</asp:label></TD>
					<td style="WIDTH: 227px" width="227"><asp:textbox id="txtSignDate_Basic" runat="server" CssClass="input" Width="70px" MaxLength="10"></asp:textbox><INPUT class="btnCal" onclick="ShowCalendar('_ctl0_txtDOB')" type="button"></td>
				</TR>
				<TR>
					<TD width="15%"><asp:label id="Label26" runat="server" CssClass="label">Signer</asp:label></TD>
					<TD width="30%"><asp:textbox id="txtSigner_Basic" runat="server" CssClass="input" Width="264px" MaxLength="255"></asp:textbox></TD>
					<TD width="10%"></TD>
					<TD width="15%"><asp:label id="Label27" runat="server" CssClass="label">Pos of signer</asp:label></TD>
					<TD style="WIDTH: 227px" width="227"><asp:textbox id="txtPosOfSigner_Basic" runat="server" CssClass="input" Width="216px" MaxLength="255"></asp:textbox></TD>
				</TR>
				<TR>
					<TD><asp:label id="Label4" runat="server" CssClass="label"> Ver of Sal</asp:label></TD>
					<TD><asp:dropdownlist id="cboVerOfSal_Basic" runat="server" CssClass="select" Width="100%"></asp:dropdownlist></TD>
					<TD></TD>
					<TD><asp:label id="Label5" runat="server" CssClass="label">Rank of sal</asp:label></TD>
					<td style="WIDTH: 227px"><asp:dropdownlist id="cboRankOfSal_Basic" runat="server" CssClass="select" Width="100%"></asp:dropdownlist></td>
				</TR>
				<TR>
					<TD><asp:label id="Label8" runat="server" CssClass="label">Scale of sal</asp:label></TD>
					<TD><asp:textbox id="txtScaleOfSal_Basic" runat="server" CssClass="input" Width="100%" MaxLength="255"></asp:textbox></TD>
					<TD></TD>
					<TD><asp:label id="Label2" runat="server" CssClass="label"> Grade of sal</asp:label></TD>
					<td style="WIDTH: 227px"><asp:textbox id="txtGradeOfSal_Basic" runat="server" CssClass="input" Width="100%" MaxLength="255"></asp:textbox></td>
				</TR>
				<TR>
					<TD><asp:label id="Label11" runat="server" CssClass="label">Promotion date</asp:label></TD>
					<TD><asp:textbox id="txtPromotion_Basic" runat="server" CssClass="input" Width="70px" MaxLength="12"></asp:textbox><INPUT class="btnCal" onclick="ShowCalendar('_ctl0_txtDOB')" type="button"></TD>
					<TD></TD>
					<TD><asp:label id="Label10" runat="server" CssClass="labelRequire">Salary standard</asp:label></TD>
					<TD style="WIDTH: 227px">
						<asp:textbox id="txtSalStandard_Basic" runat="server" CssClass="input" Width="100%" MaxLength="255"></asp:textbox></TD>
				</TR>
				<TR>
					<TD><asp:label id="Label32" runat="server" CssClass="label">Allowance coef1</asp:label></TD>
					<TD><asp:textbox id="txtAllCofe1_Basic" runat="server" CssClass="input" Width="100%" MaxLength="255"></asp:textbox></TD>
					<TD></TD>
					<TD><asp:label id="Label33" runat="server" CssClass="label">Allowance coef2</asp:label></TD>
					<TD style="WIDTH: 227px"><asp:textbox id="txtAllCofe2_Basic" runat="server" CssClass="input" Width="100%" MaxLength="255"></asp:textbox></TD>
				</TR>
				<TR>
					<td><asp:label id="Label23" runat="server" CssClass="label">Note</asp:label></td>
					<TD style="WIDTH: 635px" colSpan="4"><asp:textbox id="txtNote_Basic" runat="server" CssClass="input" Width="100%" MaxLength="255"></asp:textbox></TD>
				</TR>
				<tr>
					<td style="WIDTH: 723px" colSpan="5"></td>
				</tr>
			</table>
			<!-- Basic Salary Information--></td>
	</tr>
	<tr>
		<td>
			<!-- Div LUONG CHUC DANH-->
			<table id="tbl2" style="DISPLAY: none" cellSpacing="0" cellPadding="1" width="100%" border="0">
				<TR>
					<TD width="15%"><asp:label id="Label28" runat="server" CssClass="label">Effective date</asp:label></TD>
					<TD width="30%"><asp:textbox id="txtEffDate_Pos" runat="server" CssClass="input" Width="70px" MaxLength="10"></asp:textbox><INPUT class="btnCal" onclick="ShowCalendar('_ctl0_txtDOB')" type="button"></TD>
					<TD width="10%"></TD>
					<TD width="15%"><asp:label id="Label29" runat="server" CssClass="label">End date</asp:label></TD>
					<TD width="30%"><asp:textbox id="txtEndDate_pos" runat="server" CssClass="input" Width="70px" MaxLength="10"></asp:textbox><INPUT class="btnCal" onclick="ShowCalendar('_ctl0_txtDOB')" type="button"></TD>
				</TR>
				<TR>
					<TD width="15%"><asp:label id="Label14" runat="server" CssClass="label">Decision No</asp:label></TD>
					<TD width="30%"><asp:textbox id="txtDecision_Pos" runat="server" CssClass="input" Width="264px" MaxLength="255"></asp:textbox></TD>
					<TD width="10%"></TD>
					<TD width="15%"><asp:label id="Label15" runat="server" CssClass="label">Sign Date</asp:label></TD>
					<td width="30%"><asp:textbox id="txtSignDate_Pos" runat="server" CssClass="input" Width="70px" MaxLength="10"></asp:textbox><INPUT class="btnCal" onclick="ShowCalendar('_ctl0_txtDOB')" type="button"></td>
				</TR>
				<TR>
					<TD width="15%"><asp:label id="Label30" runat="server" CssClass="label">Signer</asp:label></TD>
					<TD width="30%">
						<asp:textbox id="txtSigner_pos" runat="server" CssClass="input" Width="264px" MaxLength="255"></asp:textbox></TD>
					<TD width="10%"></TD>
					<TD width="15%"><asp:label id="Label31" runat="server" CssClass="label">Pos of signer</asp:label></TD>
					<TD width="30%"><asp:textbox id="txtPosSign_Pos" runat="server" CssClass="input" Width="199px" MaxLength="255"></asp:textbox></TD>
				</TR>
				<TR>
					<TD><asp:label id="Label6" runat="server" CssClass="label"> Ver of sal</asp:label></TD>
					<TD><asp:dropdownlist id="cboVerOfSal_Pos" runat="server" CssClass="select" Width="100%" onChange="ChangeSalGradeCB()"></asp:dropdownlist></TD>
					<TD>&nbsp;&nbsp;</TD>
					<TD><asp:label id="Label12" runat="server" CssClass="label">Rank of sal</asp:label></TD>
					<td>
						<asp:dropdownlist id="cboRankOfSal_Pos" runat="server" CssClass="select" Width="100%"></asp:dropdownlist></td>
				</TR>
				<TR>
					<TD>
						<asp:label id="Label16" runat="server" CssClass="label">Scale of sal</asp:label></TD>
					<TD><asp:textbox id="txtScaleOfSal_Pos" runat="server" CssClass="input" Width="100%" MaxLength="255"></asp:textbox></TD>
					<TD></TD>
					<TD><asp:label id="Label17" runat="server" CssClass="label">Grade of sal</asp:label></TD>
					<td><asp:textbox id="txtGradeOfSal_Pos" runat="server" CssClass="input" Width="100%" MaxLength="255"></asp:textbox></td>
				</TR>
				<TR>
					<TD><asp:label id="Label13" runat="server" CssClass="label">Promotion date</asp:label></TD>
					<TD>
						<asp:textbox id="txtProDate_Pos" runat="server" CssClass="input" Width="70px" MaxLength="12"></asp:textbox><INPUT class="btnCal" onclick="ShowCalendar('_ctl0_txtDOB')" type="button"></TD>
					<TD></TD>
					<TD>
						<asp:label id="Label22" runat="server" CssClass="labelRequire">Salary standard</asp:label></TD>
					<td>
						<asp:textbox id="txtSalStandard_Pos" runat="server" CssClass="input" Width="100%" MaxLength="255"></asp:textbox></td>
				</TR>
				<TR>
					<TD>
						<asp:label id="Label3" runat="server" CssClass="label">Salary percent</asp:label></TD>
					<TD>
						<asp:textbox id="txtSalPercent_Pos" runat="server" CssClass="input" Width="100%" MaxLength="255"></asp:textbox></TD>
					<TD></TD>
					<TD>
						<asp:label id="Label7" runat="server" CssClass="labelRequire">Other salary</asp:label></TD>
					<TD>
						<asp:textbox id="txtOtherSal_Pos" runat="server" CssClass="input" Width="100%" MaxLength="255"></asp:textbox></TD>
				</TR>
				<TR>
					<TD>
						<asp:label id="Label34" runat="server" CssClass="label">Allowance coef1</asp:label></TD>
					<TD>
						<asp:textbox id="txtAllCoef1_Pos" runat="server" CssClass="input" Width="100%" MaxLength="255"></asp:textbox></TD>
					<TD></TD>
					<TD>
						<asp:label id="Label35" runat="server" CssClass="label">Allowance coef2</asp:label></TD>
					<TD>
						<asp:textbox id="txtAllCofe2_Pos" runat="server" CssClass="input" Width="100%" MaxLength="255"></asp:textbox></TD>
				</TR>
				<TR>
					<td>
						<asp:label id="Label20" runat="server" CssClass="label">Note</asp:label></td>
					<TD colspan="4">
						<asp:textbox id="txtNote_Pos" runat="server" CssClass="input" Width="100%" MaxLength="255"></asp:textbox></TD>
				</TR>
				<!-- END DIV LUONG CHUC DANH --></table>
		</td>
	</tr>
</table>
<!-- end button for input form --> </TD></TR> 
<!-- start button for input form -->
<TR>
	<TD noWrap align="center">
		<TABLE id="tblButton" cellSpacing="0" cellPadding="0" width="99%" border="0">
			<TR vAlign="top" height="30">
				<TD vAlign="middle" width="15%"><asp:checkbox id="chkShowGrid" accessKey="G" CssClass="checkbox" runat="server" ToolTip="Alt+G"
						Checked="True" Text="Show Datagrid"></asp:checkbox></TD>
				<TD align="center" width="*"><asp:linkbutton id="btnAddNew" accessKey="N" CssClass="btnAddnew" runat="server" ToolTip="Alt+N">Add New</asp:linkbutton>&nbsp;
					<asp:linkbutton id="btnSave" accessKey="S" CssClass="btnSave" runat="server" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp;&nbsp;&nbsp;
					<asp:linkbutton id="btnDelete" accessKey="D" CssClass="btnExport" runat="server" ToolTip="Alt + D">Delete</asp:linkbutton>&nbsp;
					<asp:linkbutton id="btnImport" accessKey="I" CssClass="btnImport" runat="server" ToolTip="Alt+I, Import data from excel file same format with export file">Import</asp:linkbutton>&nbsp;&nbsp;
					<asp:linkbutton id="btnExport" accessKey="E" CssClass="btnExport" runat="server" ToolTip="Alt+E, Export data in grid to excel file">Export</asp:linkbutton></TD>
			</TR>
		</TABLE>
	</TD>
</TR>
<!-- end button for input form -->
<TR id="trGrid" runat="server">
	<TD align="center"><!-- start grid for input form -->
		<TABLE id="tblGrid" cellSpacing="0" cellPadding="1" width="98%" border="0">
			<TR>
				<TD width="40%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
				<TD align="right" width="*">&nbsp;&nbsp;&nbsp;
				</TD>
			</TR>
			<TR id="trGrid1"> <!-- start grid detail for input form -->
				<TD colSpan="2"><asp:datagrid id="dtgList" CssClass="grid" runat="server" Width="100%" AllowPaging="True" BackColor="White"
						BorderColor="#3366CC" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False" AllowSorting="True">
						<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
						<ItemStyle CssClass="gridItem"></ItemStyle>
						<HeaderStyle CssClass="gridHeader"></HeaderStyle>
						<FooterStyle CssClass="gridFooter"></FooterStyle>
						<Columns>
							<asp:TemplateColumn HeaderText="&lt;b&gt;Seq&lt;/b&gt;">
								<HeaderStyle Width="5%"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<%# Container.ItemIndex + 1%>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:BoundColumn Visible="False" DataField="Date" HeaderText="Effective date">
								<HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
							</asp:BoundColumn>
							<asp:ButtonColumn DataTextField="Date" HeaderText="Effective Date">
								<HeaderStyle Width="15%"></HeaderStyle>
							</asp:ButtonColumn>
							<asp:BoundColumn HeaderText="Decision No">
								<HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
							</asp:BoundColumn>
							<asp:BoundColumn HeaderText="Rank of Sal">
								<HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
							</asp:BoundColumn>
							<asp:BoundColumn HeaderText="Promotion date">
								<HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
							</asp:BoundColumn>
							<asp:BoundColumn HeaderText="Salary"></asp:BoundColumn>
							<asp:TemplateColumn HeaderText="Select">
								<HeaderStyle HorizontalAlign="Center" Width="7%"></HeaderStyle>
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
<script type="text/javascript">
	function showtable(object)
	{
	
		if(object=="_ctl0_opt1")
		{
			
			document.getElementById("tbl1").style.display="block";
			document.getElementById("tbl2").style.display="none";
		}
		if(object=="_ctl0_opt2")
		{
			
			document.getElementById("tbl2").style.display="block";
			document.getElementById("tbl1").style.display="none";
			
		}
		
	}
</script>
