<%@ Control Language="c#" AutoEventWireup="false" Codebehind="MonthlyKPISection.ascx.cs" Inherits="iHRPCore.MdlHR.MonthlyKPISection" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<script language="javascript" src="../Include/common.js"></script>
<script language="javascript">
		function ReturnKPIValue(SectionListID,KPIID,SectionCodeListID)
		{			
			var txtSectionList=document.getElementById('_ctl0_txtSectionList');						
			var txtKPIValue=document.getElementById('_ctl0_txtKPIValue');			
			var txtCodeList=document.getElementById('_ctl0_txtSectionCodeList');												
			var parentSectionList=opener.document.getElementById(SectionListID);									
			var parentKPIValue=opener.document.getElementById(KPIID);									
			var parentCodeList=opener.document.getElementById(SectionCodeListID);									
			parentSectionList.value=txtSectionList.value;			
			parentKPIValue.value=txtKPIValue.value;			
			parentCodeList.value=txtCodeList.value;			
			document.close();
		}
</script>
&nbsp;
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD style="HEIGHT: 17px" align="center" colSpan="5">
			<asp:label id="lblTitle" runat="server" CssClass="labelTitle">SELECT SECTION</asp:label></TD>
	</TR>
	<TR>
		<TD colSpan="2"><asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label></TD>
		<TD></TD>
		<TD></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD align="center">
			<table width="70%">
				<TR>
					<TD width="12%"><asp:label id="lblLevel1ID" CssClass="label" runat="server" Width="100%">Company</asp:label></TD>
					<TD width="35%"><asp:dropdownlist id="cboLevel1ID" CssClass="select" runat="server" Width="100%"></asp:dropdownlist></TD>
					<TD width="6%"></TD>
					<TD width="12%"><asp:label id="lblLevel2ID" CssClass="label" runat="server" Width="100%">Division</asp:label></TD>
					<TD width="35%"><asp:dropdownlist id="cboLevel2ID" CssClass="select" runat="server" Width="100%"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD>
						<asp:label id="lblLevel3ID" runat="server" CssClass="label" Width="100%">Department</asp:label></TD>
					<TD>
						<asp:dropdownlist id="cboLevel3ID" runat="server" CssClass="select" Width="100%"></asp:dropdownlist></TD>
					<TD></TD>
					<TD>
						<asp:label id="lblLevel4ID" runat="server" CssClass="label" Width="100%">Section</asp:label></TD>
					<TD>
						<asp:dropdownlist id="cboLevel4ID" runat="server" CssClass="select" Width="100%"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD>
						<asp:label id="lblCode" runat="server" CssClass="label">Code</asp:label></TD>
					<TD>
						<asp:textbox id="txtCode" runat="server" CssClass="input" Width="50%" MaxLength="20"></asp:textbox></TD>
					<TD></TD>
					<TD>
						<asp:label id="Label3" runat="server" CssClass="label">Name</asp:label></TD>
					<TD>
						<asp:textbox id="txtName" runat="server" CssClass="input" Width="100%" MaxLength="50"></asp:textbox></TD>
				</TR>
				<TR>
					<TD colSpan="5" align="center" style="HEIGHT: 26px">
						<asp:linkbutton id="btnSearch" accessKey="E" runat="server" CssClass="btnFilter" ToolTip="Alt+ E, Search">Search</asp:linkbutton>&nbsp;
						<asp:linkbutton id="btnAccept" runat="server" CssClass="btn" accessKey="A" ToolTip="Alt+A, Add selected items to Parent page">Accept</asp:linkbutton>&nbsp;
						<asp:linkbutton id="btnClose" accessKey="C" runat="server" CssClass="btn" ToolTip="Alt+C, Close page without add selected items to parent page">Close</asp:linkbutton>
					</TD>
				</TR>
			</table>
		</TD>
	</TR>
	<TR>
		<TD align="center" colSpan="5">
			<HR width="97%">
		</TD>
		<TD align="center"></TD>
	</TR>
	<TR>
		<TD align="center" colSpan="5">
			<asp:Panel id="pnlGrid" runat="server" Visible="False">
				<TABLE>
					<TR>
						<TD width="45%">
							<asp:label id="Label9" CssClass="labelData" runat="server" Width="100%"> Section list (xxx)</asp:label></TD>
						<TD align="center" width="10%"></TD>
						<TD width="45%">
							<asp:label id="Label1" CssClass="labelData" runat="server" Width="100%">Selected sections (xxx)</asp:label></TD>
					</TR>
					<TR>
						<TD vAlign="top" align="left" width="45%">
							<DIV style="DISPLAY: inline; OVERFLOW: auto; WIDTH: 100%; HEIGHT: 474px" ms_positioning="FlowLayout">
								<asp:datagrid id="dtgAll" CssClass="grid" runat="server" Width="90%" Visible="True" BackColor="White"
									BorderColor="#3366CC" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False" AllowSorting="True"
									ShowFooter="True">
									<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
									<ItemStyle HorizontalAlign="Center" CssClass="gridItem"></ItemStyle>
									<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
									<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
									<Columns>
										<asp:BoundColumn DataField="No" HeaderText="Seq">
											<HeaderStyle HorizontalAlign="Center" Width="5%" VerticalAlign="Middle"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="CompanyName" HeaderText="Company">
											<HeaderStyle HorizontalAlign="Center" Width="20%"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="DivisionName" HeaderText="Division">
											<HeaderStyle HorizontalAlign="Center" Width="20%"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="DepartmentName" HeaderText="Department">
											<HeaderStyle Width="20%"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="SectionName" HeaderText="Section">
											<HeaderStyle Width="30%"></HeaderStyle>
										</asp:BoundColumn>
										<asp:TemplateColumn>
											<HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
											<HeaderTemplate>
												<asp:CheckBox id="chkCheckAll" onclick="CheckAll('_ctl0_dtgAll__ctl1_chkCheckAll','_ctl0_dtgAll',2,0,'chkSectionSelect')"
													runat="server" CssClass="checkbox"></asp:CheckBox>
											</HeaderTemplate>
											<ItemTemplate>
												<asp:CheckBox id="chkSectionSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
											</ItemTemplate>
										</asp:TemplateColumn>
									</Columns>
									<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
								</asp:datagrid></DIV>
						</TD>
						<TD vAlign="top" align="center" width="10%">
							<asp:linkbutton id="cmdSelect" accessKey="S" CssClass="btn" runat="server" ToolTip="Alt+S, Select items">></asp:linkbutton><BR>
							<BR>
							<asp:linkbutton id="cmdRemove" accessKey="R" CssClass="btn" runat="server" ToolTip="Alt+R, Remove items"><</asp:linkbutton></TD>
						<TD vAlign="top" align="left" width="45%">
							<DIV style="DISPLAY: inline; OVERFLOW: auto; WIDTH: 394px; HEIGHT: 474px" ms_positioning="FlowLayout">
								<asp:datagrid id="dtgSelected" CssClass="grid" runat="server" Width="95%" Visible="True" BackColor="White"
									BorderColor="#3366CC" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False" AllowSorting="True"
									ShowFooter="True" PageSize="7">
									<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
									<ItemStyle HorizontalAlign="Center" CssClass="gridItem"></ItemStyle>
									<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
									<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
									<Columns>
										<asp:BoundColumn DataField="No" HeaderText="Seq">
											<HeaderStyle HorizontalAlign="Center" Width="5%" VerticalAlign="Middle"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="LSLevel3Code" HeaderText="Code">
											<HeaderStyle Width="25%"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="SectionName" HeaderText="Section">
											<HeaderStyle Width="35%"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn HeaderText="No of Emp">
											<HeaderStyle Width="30%"></HeaderStyle>
										</asp:BoundColumn>
										<asp:TemplateColumn>
											<HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
											<HeaderTemplate>
												<asp:CheckBox id="chkCheckAll" onclick="CheckAll('_ctl0_dtgSelected__ctl1_chkCheckAll','_ctl0_dtgSelected',2,0,'chkSectionSelect')"
													runat="server" CssClass="checkbox" Checked="True"></asp:CheckBox>
											</HeaderTemplate>
											<ItemTemplate>
												<asp:CheckBox id="chkSectionSelected" runat="server" CssClass="checkbox" Checked="True"></asp:CheckBox>
											</ItemTemplate>
										</asp:TemplateColumn>
									</Columns>
									<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
								</asp:datagrid></DIV>
						</TD>
					</TR>
				</TABLE>
			</asp:Panel>
			<INPUT id="txtKPIValue" type="hidden" name="Hidden1" runat="server"> <INPUT type="hidden" runat="server" id="txtSectionList" NAME="txtSectionList">
			<INPUT id="txtSectionCodeList" type="hidden" name="Hidden1" runat="server">
		</TD>
	</TR>
</TABLE>
