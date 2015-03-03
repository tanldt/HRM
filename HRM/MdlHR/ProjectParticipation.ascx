<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ProjectParticipation.ascx.cs" Inherits="iHRPCore.MdlHR.ProjectParticipation" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc" TagName="EmpHeader" Src="../Include/EmpHeader.ascx" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="99%" border="0">
	<TR>
		<TD style="HEIGHT: 19px"><asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center"><uc:empheader id="HR_EmpHeader" runat="server"></uc:empheader></TD>
	</TR>
	<tr>
		<TD align="center"><!-- start detail for input form -->
			<TABLE cellSpacing="0" cellPadding="1" width="100%" border="0">
				<TBODY>
					<TR>
						<TD width="15%">
							<asp:label id="lblFromMonth" runat="server" CssClass="labelRequire">From Month</asp:label></TD>
						<TD width="32%">
							<asp:textbox id="txtFromMonth" onblur="javascript:CheckMonthYear(this)" runat="server" CssClass="input"
								Width="80px" MaxLength="10"></asp:textbox>&nbsp;
							<asp:label id="Label1" runat="server" CssClass="label" Width="100%">(mm/yyyy)</asp:label></TD>
						<TD width="4%"></TD>
						<TD width="15%">
							<asp:label id="lblToMonth" runat="server" CssClass="labelRequire">To Month</asp:label></TD>
						<TD width="37%">
							<asp:textbox id="txtToMonth" onblur="javascript:CheckMonthYear(this)" runat="server" CssClass="input"
								Width="80px" MaxLength="10"></asp:textbox>&nbsp;
							<asp:label id="Label2" runat="server" CssClass="label" Width="100%">(mm/yyyy)</asp:label></TD>
					</TR>
					<TR>
						<TD width="15%">
							<asp:label id="lblProjectCode" runat="server" CssClass="labelRequire" Width="100%">Project Code</asp:label></TD>
						<TD width="32%" valign="top">
							<asp:textbox id="txtProjectCode" style="POSITION: absolute" runat="server" CssClass="input" MaxLength="20"
								Width="100%"></asp:textbox></TD>
						<TD width="4%"></TD>
						<TD width="15%">
							<asp:label id="lblProjectName" runat="server" CssClass="label" Width="100%">Project Name</asp:label></TD>
						<TD width="37%" valign="top">
							<asp:textbox id="txtProjectName" style="POSITION: absolute" runat="server" CssClass="input" Width="100%"
								MaxLength="150"></asp:textbox></TD>
					</TR>
					<tr>
					</tr>
					<TR>
						<TD width="15%"><asp:label id="lblSponsoredby" CssClass="label" runat="server" Width="100%"> Main work</asp:label></TD>
						<TD width="32%" valign="top">
							<asp:textbox id="txtMainWork" style="POSITION: absolute" runat="server" CssClass="input" Width="100%"
								MaxLength="100"></asp:textbox></TD>
						<TD width="4%"></TD>
						<TD width="15%">
							<asp:label id="lblPosition" runat="server" CssClass="label" Width="100%">Job level</asp:label></TD>
						<TD width="37%" valign="top">
							<asp:textbox id="txtPosition" style="POSITION: absolute" runat="server" CssClass="input" Width="100%"
								MaxLength="100"></asp:textbox></TD>
					</TR>
					<TR>
						<TD style="HEIGHT: 11px">
							<asp:label id="lblMainWork" runat="server" CssClass="label">Description</asp:label></TD>
						<TD style="HEIGHT: 11px">
							<asp:textbox id="txtDescription" runat="server" CssClass="input" Width="100%" MaxLength="500"
								TextMode="MultiLine"></asp:textbox></TD>
						<TD style="HEIGHT: 11px"></TD>
						<TD style="HEIGHT: 11px">
							<asp:label id="lblNote" runat="server" CssClass="label">Note</asp:label></TD>
						<TD style="HEIGHT: 11px"><asp:textbox id="txtNote" runat="server" CssClass="input" Width="100%" MaxLength="255" TextMode="MultiLine"></asp:textbox></TD>
					</TR>
				</TBODY>
			</TABLE> <!-- end button for input form --><INPUT id="txtLSProjectParticipationID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden"
				size="1" name="txtWorkingBackgroundID" runat="server">
		</TD>
	</tr>
	<TR>
		<TD vAlign="top" noWrap align="center" height="20">
			<HR align="center" width="100%">
		</TD>
	</TR>
	<TR>
		<TD noWrap align="center">
			<TABLE id="tblButton" cellSpacing="0" cellPadding="0" width="99%" border="0">
				<TR vAlign="top">
					<TD vAlign="middle" width="15%">
						<asp:checkbox id="chkShowGrid" accessKey="G" onclick="javascript:collapse('_ctl0_tdGrid');" runat="server"
							CssClass="checkbox" ToolTip="Alt+G" Checked="True" Text="Show grid"></asp:checkbox></TD>
					<TD align="center" width="*">
						<asp:linkbutton id="btnAddNew" accessKey="N" runat="server" CssClass="btnAddnew" ToolTip="Alt+N">Add New</asp:linkbutton>&nbsp;&nbsp;
						<asp:linkbutton id="btnSave" accessKey="S" runat="server" CssClass="btnSave" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp;&nbsp;
						<asp:linkbutton id="btnDelete" accessKey="D" runat="server" CssClass="btnDelete" ToolTip="Alt + D">Delete</asp:linkbutton>&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnList" accessKey="L" runat="server" CssClass="btnList" ToolTip="Alt + L">List</asp:linkbutton>&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnExport" accessKey="E" CssClass="btnExport" runat="server" ToolTip="Alt+E, Xu?t dl trên lu?i ra file Excel"> Export</asp:linkbutton></TD>
				</TR>
			</TABLE>
		</TD>
	</TR> <!-- end button for input form -->
	<tr>
		<td height="10">
		</td>
	</tr>
	<TR>
		<TD runat="server" id="tdGrid" align="center"><!-- start grid for input form -->
			<TABLE id="tblGrid" cellSpacing="0" cellPadding="1" width="100%" border="0">
				<TR id="trGrid"> <!-- start grid detail for input form -->
					<Td>
						<asp:datagrid id="dtgList" runat="server" CssClass="grid" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
							CellPadding="0" BorderWidth="1px" BorderColor="#3366CC" BackColor="White" AllowPaging="True">
							<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="LSProjectParticipationID" HeaderText="LSProjectParticipationID">
									<HeaderStyle HorizontalAlign="Center" Width="5%" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
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
								<asp:ButtonColumn DataTextField="FromMonth" HeaderText="From Month" CommandName="EDIT"></asp:ButtonColumn>
								<asp:BoundColumn DataField="ToMonth" HeaderText="To Month">
									<HeaderStyle Width="12.5%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ProjectCode" HeaderText="Project Code">
									<HeaderStyle Width="20%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Position" HeaderText="Job level">
									<HeaderStyle Width="20%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MainWork" HeaderText="Main Work">
									<HeaderStyle Width="20%"></HeaderStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></Td>
				</TR>
			</TABLE> <!-- end grid for input form --></TD>
	</TR>
</TABLE>
<script language="javascript">
<!--
function validform(){	
	if(checkisnull('HR_EmpHeader_txtEmpID')==false)  return false;
	if(checkisnull('txtFromMonth')==false)  return false;
	if(checkisnull('txtToMonth')==false)  return false;		
	if(checkisnull('txtProjectCode')==false)  return false;	
	if(document.getElementById('_ctl0_txtFromMonth').value!="" ||document.getElementById('_ctl0_txtToMonth').value!="")
	{
		if(!FromSmallToMonth(document.getElementById('_ctl0_txtFromMonth'),document.getElementById('_ctl0_txtToMonth')))
		{
//			alert('Th?i gian t? không du?c l?n hon th?i gian d?n!');
			GetAlertError(iTotal,DSAlert,"0007");
			document.getElementById('_ctl0_txtToMonth').focus();
			return false;
		}
	}
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
