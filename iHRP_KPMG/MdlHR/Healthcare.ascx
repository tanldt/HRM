<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Healthcare.ascx.cs" Inherits="iHRPCore.MdlHR.Healthcare" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc" TagName="EmpHeader" Src="../Include/EmpHeader.ascx" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="97%" border="0">
	<TR>
		<TD><asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center"><uc:empheader id="HR_EmpHeader" runat="server"></uc:empheader></TD>
	</TR>
	<tr>
		<TD align="center"><!-- start detail for input form -->
			<TABLE cellSpacing="0" cellPadding="1" width="99%" border="0">
				<TR>
					<TD style="HEIGHT: 27px" width="17%" colSpan="1" rowSpan="1"><asp:label id="lblOndate" runat="server" CssClass="labelRequire"> OnDate</asp:label></TD>
					<TD style="HEIGHT: 27px" width="32%"><asp:textbox id="txtOnDate" onblur="JavaScript:CheckDate(this)" runat="server" CssClass="input"
							MaxLength="10" Width="112px"></asp:textbox>&nbsp;<INPUT class="btnCal" onclick="javascript:popUpCalendar(<%=this.txtOnDate.ClientID%>);"  type="button"></TD>
					<TD style="HEIGHT: 27px" width="4%"></TD>
					<TD style="HEIGHT: 27px" colSpan="2"><asp:radiobuttonlist id="optStatus" runat="server" CssClass="option" Width="347px" Height="16px" RepeatColumns="3"
							RepeatDirection="Horizontal">
							<asp:ListItem Value="1">weak</asp:ListItem>
							<asp:ListItem Value="2">Usual</asp:ListItem>
							<asp:ListItem Value="3" Selected="True">Healthy</asp:ListItem>
						</asp:radiobuttonlist></TD>
				</TR>
				<TR>
					<TD width="15%"><asp:label id="lblHeight" runat="server" CssClass="label" Width="100%"> Height</asp:label></TD>
					<TD vAlign="top" width="32%"><asp:textbox id="txtHeight" style="TEXT-ALIGN:right" runat="server" CssClass="input" onblur="javascript:checkNumeric(this);"
							MaxLength="3" Width="100%"></asp:textbox></TD>
					<TD width="4%"></TD>
					<TD width="15%"><asp:label id="lblWeight" runat="server" CssClass="label" Width="100%"> Weight</asp:label></TD>
					<TD vAlign="top" width="37%"><asp:textbox id="txtWeight" style="TEXT-ALIGN:right" runat="server" CssClass="input" onblur="javascript:checkNumeric(this);"
							MaxLength="5" Width="100%"></asp:textbox></TD>
				</TR>
				<TR>
					<TD width="15%">
						<asp:label id="Label1" CssClass="label" runat="server" Width="100%">Blood type</asp:label></TD>
					<TD vAlign="baseline" width="32%">
						<asp:dropdownlist id="cboLSBloodTypeID" CssClass="combo" runat="server" Width="100%">
							<asp:ListItem Value="O">O</asp:ListItem>
							<asp:ListItem Value="A">A</asp:ListItem>
							<asp:ListItem Value="B">B</asp:ListItem>
							<asp:ListItem Value="AB">AB</asp:ListItem>
						</asp:dropdownlist></TD>
					<TD width="4%"></TD>
					<TD width="15%"></TD>
					<TD vAlign="top" width="37%"></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 1px" width="15%"><asp:label id="lblHospital" runat="server" CssClass="label" Width="100%">Hospital</asp:label></TD>
					<TD style="HEIGHT: 1px" vAlign="top" width="32%" colSpan="4"><asp:dropdownlist id="cboLSHospitalID" runat="server" CssClass="combo" Width="100%"></asp:dropdownlist></TD>
				</TR>
				<TR>
				</TR>
				<TR>
				</TR>
				<TR>
					<TD><asp:label id="lblHealth" runat="server" CssClass="label" Width="100%">Health</asp:label></TD>
					<TD vAlign="top" colSpan="4"><asp:textbox id="txtHealth" runat="server" CssClass="input" MaxLength="255" Width="100%"></asp:textbox></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblResultby" runat="server" CssClass="label" Width="100%"> Result by</asp:label></TD>
					<TD vAlign="top" colSpan="4"><asp:textbox id="txtResultBy" runat="server" CssClass="input" MaxLength="50" Width="80%"></asp:textbox><asp:checkbox id="chkSpecial" accessKey="G" runat="server" CssClass="checkbox" ToolTip="Alt+G"
							Checked="True" Text="Special"></asp:checkbox></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblGrade" runat="server" CssClass="label" Width="100%">Grade</asp:label></TD>
					<TD vAlign="top" colSpan="4"><asp:textbox id="txtGrade" runat="server" CssClass="input" MaxLength="100" Width="100%" Rows="2"></asp:textbox></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblNote" runat="server" CssClass="label" Width="100%">Note</asp:label></TD>
					<TD vAlign="top" colSpan="4"><asp:textbox id="txtNote" runat="server" CssClass="input" MaxLength="100" Width="100%" Rows="2"></asp:textbox></TD>
				</TR>
			</TABLE> <!-- end button for input form -->
		</TD>
	</tr>
	<TR>
		<TD vAlign="top" noWrap align="center" height="20">
			<HR align="center" width="95%">
			<asp:textbox id="txtHealthCareRecordID" runat="server" CssClass="input" Width="0px"></asp:textbox>
		</TD>
	</TR>
	<TR>
		<TD noWrap align="center">
			<TABLE id="tblButton" cellSpacing="0" cellPadding="0" width="99%" border="0">
				<TR vAlign="top" height="30">
					<TD vAlign="middle" width="15%"><asp:checkbox id="chkShowGrid" accessKey="G" onclick="javascript:collapse('_ctl0_tdGrid');" runat="server"
							CssClass="checkbox" ToolTip="Alt+G" Checked="True" Text="Show grid"></asp:checkbox></TD>
					<TD align="center" width="*"><asp:linkbutton id="btnAddNew" accessKey="N" runat="server" CssClass="btnAddnew" ToolTip="Alt+N">AddNew</asp:linkbutton>&nbsp;&nbsp;
						<asp:linkbutton id="btnSave" accessKey="S" runat="server" CssClass="btnSave" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnList" accessKey="L" runat="server" CssClass="btnList" ToolTip="Alt + L">List</asp:linkbutton>&nbsp;&nbsp;
						<asp:linkbutton id="btnDelete" accessKey="D" runat="server" CssClass="btnDelete" ToolTip="Alt + D">Delete</asp:linkbutton>&nbsp;&nbsp;
						<asp:linkbutton id="btnExport" accessKey="E" runat="server" CssClass="btnExport" ToolTip="Alt+E, Export data in grid to excel file">Export</asp:linkbutton></TD>
				</TR>
			</TABLE>
		</TD>
	</TR> <!-- end button for input form -->
	<TR>
		<TD id="tdGrid" align="center" runat="server"><!-- start grid for input form -->
			<TABLE id="tblGrid" cellSpacing="0" cellPadding="1" width="98%" border="0">
				<TR id="trGrid"> <!-- start grid detail for input form -->
					<TD colSpan="2"><asp:datagrid id="dtgList" runat="server" CssClass="grid" Width="100%" BackColor="White" BorderColor="#3366CC"
							BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True">
							<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="HealthCareRecordID" HeaderText="HealthCareRecordID"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Select">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="&lt;b&gt;#&lt;/b&gt;">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<%# Container.ItemIndex + 1%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:ButtonColumn DataTextField="OnDate" HeaderText="Date" CommandName="EDIT"></asp:ButtonColumn>
								<asp:BoundColumn DataField="Status" HeaderText="Status">
									<HeaderStyle Width="15%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Height" HeaderText="Height">
									<HeaderStyle Width="25%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Weight" HeaderText="Weight">
									<HeaderStyle Width="15%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ResultBy" HeaderText="Result by">
									<HeaderStyle Width="20%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE> <!-- end grid for input form --></TD>
	</TR>
</TABLE>
<script language="javascript">
<!--
function validform(){
	if(checkisnull('txtOnDate')==false)  return false;
	return true;
}
function checkdelete()
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

function checkisnull(obj)
	{
		if(document.getElementById('_ctl0_' + obj).value=="")
			{
				GetAlertError(iTotal,DSAlert,"0003");
				document.getElementById('_ctl0_' + obj).focus();
				return false;
			}
		else
			return true;
	}
//-->
</script>
