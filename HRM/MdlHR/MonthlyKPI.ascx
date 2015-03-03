<%@ Control Language="c#" AutoEventWireup="false" Codebehind="MonthlyKPI.ascx.cs" Inherits="iHRPCore.MdlHR.MonthlyKPI" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="98%" border="0">
	<TR>
		<TD colSpan="5"><asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD colSpan="5">
			<asp:label id="lblExistMonth" runat="server" CssClass="labelRequire" Width="100px">Existing Months</asp:label>
			<asp:DropDownList id="cboExistingMonths" runat="server" Width="104px" Height="22"></asp:DropDownList>
			<asp:label id="lblKPIInfo" runat="server" CssClass="label">Class</asp:label>
			<asp:textbox id="txtClass" runat="server" CssClass="input" Width="72px" MaxLength="7"></asp:textbox>
			<asp:linkbutton id="btnView" accessKey="V" runat="server" CssClass="btnExport" ToolTip="Alt+V, View Data">View</asp:linkbutton>
			<asp:label id="lblNewMonth" runat="server" CssClass="label" Width="80px">New month</asp:label>
			<asp:TextBox id="txtNewMonth" runat="server" Width="80px" Height="22px"></asp:TextBox>
			<asp:linkbutton id="btnCreate" accessKey="C" runat="server" CssClass="btnExport" Width="64px" ToolTip="Alt+C, Create new month">Create</asp:linkbutton></TD>
	</TR>
	<TR>
		<TD align="center" colSpan="5" style="HEIGHT: 31px">
		</TD>
	</TR>
	<TR>
		<TD align="center" colSpan="5">
			<TABLE id="tblGrid" cellSpacing="0" cellPadding="1" width="98%" border="0">
				<TR>
					<TD width="40%" style="HEIGHT: 20px"><asp:label id="Label13" CssClass="label" runat="server">Total rows</asp:label>&nbsp;
						<asp:label id="lblTotalRows" CssClass="labelData" runat="server" Width="35px">500</asp:label></TD>
					<TD align="right" width="*" style="HEIGHT: 20px">&nbsp;&nbsp;&nbsp;
					</TD>
				</TR>
				<TR id="trGridKPI">
					<TD colSpan="2"><asp:datagrid id="dtgKPI" CssClass="grid" runat="server" Width="100%" BackColor="White" BorderColor="#3366CC"
							BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False" AllowSorting="True" PageSize="7" Height="212">
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
							<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
							<Columns>
								<asp:BoundColumn DataField="No" HeaderText="Seq">
									<HeaderStyle HorizontalAlign="Center" Width="5%" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="KPI1" HeaderText="KPI 1">
									<HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="KPI2" HeaderText="KPI 2">
									<HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CAT1" HeaderText="CAT 1">
									<HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CAT2" HeaderText="CAT 2">
									<HeaderStyle Width="15%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="KPI Value">
									<HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:TextBox id="lblKPIValue" runat="server" Width="27px" BorderStyle="None" ReadOnly="True"></asp:TextBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Section List">
									<HeaderStyle HorizontalAlign="Center" Width="30%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:TextBox id="lblSectionList" runat="server" BorderStyle="None" ReadOnly="True"></asp:TextBox><INPUT id="txtSectionCodeList" type="hidden" runat="server">
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Select Section">
									<HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										&nbsp;
										<asp:Button id="btnSelectSection" runat="server" Text="..."></asp:Button>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD align="center" colSpan="5">
			<HR width="97%">
		</TD>
	</TR>
	<TR>
		<TD align="center" colSpan="5">
			<asp:CheckBox id="chkCopy" runat="server" CssClass="checkbox" Text="Copy to new month"></asp:CheckBox>&nbsp;<asp:linkbutton id="Linkbutton4" CssClass="btnSave" runat="server">Save</asp:linkbutton>&nbsp;&nbsp;<asp:linkbutton id="btnExport" accessKey="E" CssClass="btnExport" runat="server" ToolTip="Alt+E, Export data in grid to excel file">Export</asp:linkbutton></TD>
	</TR>
</TABLE>
<script language="javascript" src='../Include/common.js"'></script>
<script>
	function OpenWindowSection(SectionListID,KPIID,SectionCodeList)
	{	
		var obj=document.getElementById(SectionCodeList);				
		window.open("FormPage.aspx?Ascx=MdlHR/MonthlyKPISection.ascx&SectionListID="+SectionListID+"&KPIID="+KPIID+"&CodeListID="+SectionCodeList+"&CodeList="+obj.value,'Sections','status=1,toolbar=0,scrollbars=1,resizable=1,alwaysRaised=Yes, top=50, left=120, width=800, height=500,1 ,align=center',true);				
		return false;
	}
	function MyCheckDate(txtEffDateId)
	{
		var field=document.getElementById(txtEffDateId);		
		return CheckMonthYear(field);
	}	
	function ConfirmSave(chkCopyId,txtNewMonthId)
	{			
		var obj=document.getElementById(chkCopyId);
		if(obj.checked==true)
		{
			// VALID FORM
			var objValid=document.getElementById(txtNewMonthId);
			if(objValid.value=="") 
			{
				alert('Please input New Month to copy to.');
				return false;
			}
			else
			{
				if(CheckMonthYear(objValid)==false) return false;
			}
			
			var obj2=document.getElementById(txtNewMonthId);
			return confirm('Are you sure you want to copy data from this month to month '+obj2.value+'?');
		}
		else return confirm('Save data to this month?');
	}
</script>
