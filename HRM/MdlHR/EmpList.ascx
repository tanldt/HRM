<%@ Control Language="c#" AutoEventWireup="false" Codebehind="EmpList.ascx.cs" Inherits="iHRPCore.MdlHR.EmpList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="FPT" Namespace="FPTToolWeb.Control.DataGrids" Assembly="FPTToolWeb" %>
<%@ Register TagPrefix="uc1" TagName="ColumnList" Src="../Include/ColumnList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../Include/EmpHeaderSearch.ascx" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="98%" border="0">
	<TR>
		<TD align="center"><asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center"><uc1:empheadersearch id="EmpHeaderSearch1" runat="server"></uc1:empheadersearch></TD>
	</TR>
	<TR>
		<TD vAlign="top" noWrap align="center" height="5">&nbsp;
		</TD>
	</TR>
	<TR>
		<TD noWrap align="center">
			<TABLE id="tblButton" cellSpacing="0" cellPadding="0" width="99%" border="0">
				<TR vAlign="top" height="30">
					<TD align="center" width="*" colSpan="2"><asp:linkbutton id="btnSearch" accessKey="S" runat="server" CssClass="button" ToolTip="Alt+S">Search</asp:linkbutton>&nbsp;&nbsp; 
						&nbsp;
						<asp:linkbutton id="btnExport" accessKey="E" runat="server" CssClass="button" ToolTip="Alt+E, Export data in grid to excel file">Export</asp:linkbutton></TD>
				</TR>
				<TR>
					<TD id="tdPages" noWrap colSpan="2"><uc1:columnlist id="uctrlColumns" runat="server"></uc1:columnlist></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<!-- end button for input form -->
	<TR>
		<TD align="center"><!-- start grid for input form -->
			<TABLE id="tblGrid" cellSpacing="0" cellPadding="1" width="99%" border="0">
				<TR id="trGrid" align="center"> <!-- start grid detail for input form -->
					<TD><FPT:FPTDataGrid id="dtgList" runat="server" Width="100%" AllowPaging="True" AllowSorting="True"
							AutoGenerateColumns="False" CellPadding="0" BorderWidth="1px" PageSize="20" MouseOverCssClass="gridhover"
							ItemCssClass="gridItem" AltItemCssClass="gridAlter" MouseClickCssClass="gridClick" ShowMouse="True"
							FptGridRows="0" NewValues="String[] Array" AddEmptyHeaders="0" FptGridColumns="0" IsGroupby="False"
							FptGridHeader="False">
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="&lt;b&gt;Seq&lt;/b&gt;">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<%# Container.ItemIndex + 1 + dtgList.PageSize*dtgList.CurrentPageIndex%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn Visible="False" DataField="EmpID">
									<HeaderStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn SortExpression="EmpCode" HeaderText="EmpCode">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id=hpLink CssClass=Hlink Width="95%" Text='<%# DataBinder.Eval(Container, "DataItem.EmpCode") %>' CommandName="hpLink" Runat="server">
										</asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="EmpName" SortExpression="EmpName" HeaderText="Full Name">
									<HeaderStyle Width="25%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="VFirstName" SortExpression="VFirstName" HeaderText="First Name">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Level1Name" HeaderText="Company">
									<HeaderStyle Width="7%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Level2Name" HeaderText="Department">
									<HeaderStyle Width="14%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Level3Name" HeaderText="Group">
									<HeaderStyle Width="14%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="EmpTypeName" HeaderText="Employment Type">
									<HeaderStyle Width="14%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PositionName" HeaderText="Job title">
									<HeaderStyle Width="14%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
							<HeaderStyle HorizontalAlign="Center" CssClass="TaskGrid_Head"></HeaderStyle>
							<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
						</FPT:FPTDataGrid><asp:textbox id="txtPageLoad" runat="server" Visible="False"></asp:textbox></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>
<script language="javascript">
	function OpenBuildCode()
	{		
		window.open('FormPage.aspx?ModuleID=HR&ParentID=8&FunctionID=204&Ascx=MdlHR/BuildEmpCode.ascx&action=addnew&empid=', 'BuildCode', 'width=320,height=120,left=300,top=200,dependent');		
		return false;
	}
</script>
<%if (!Page.IsPostBack){%>
<script language="javascript">
		document.getElementById("_ctl0_btnSearch").click();
</script>
<%}%>
