<%@ Import Namespace ="System.Data.SqlClient" %>
<%@ Import Namespace ="System.Data" %>
<%@ Import Namespace ="iHRPCore.Com"%>
<%@ Register TagPrefix="uc1" TagName="ColumnList" Src="../Include/ColumnList.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="MailTemplate.ascx.cs" Inherits="MdlSYS.MailTemplate" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="ExportTechnologies.WebControls.RTE" Assembly="ExportTechnologies.WebControls.RTE" %>
<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
	<TR>
		<TD width="15%"><asp:label id="lblCode" CssClass="label" runat="server">Code</asp:label></TD>
		<TD width="85%"><asp:textbox id="txtTemplateID" CssClass="inputReadOnly" runat="server" Width="100px" MaxLength="10"
				ReadOnly="True"></asp:textbox></TD>
		<TD width="0%"></TD>
	</TR>
	<TR>
		<TD><asp:label id="Label1" CssClass="label" runat="server">Description</asp:label></TD>
		<TD><asp:label id="lblDescription" CssClass="label" runat="server"></asp:label></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD><asp:label id="Title" CssClass="labelRequire" runat="server">Title</asp:label></TD>
		<TD><asp:textbox id="txtTitle" CssClass="input" runat="server" Width="360px" MaxLength="150"></asp:textbox></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD><asp:label id="Label5" CssClass="label" runat="server">Content</asp:label></TD>
		<TD><cc1:richtexteditor id="txtContent" runat="server" Width="100%" RTEResourcesUrl="mdlNews/RTE_Resources/" Height="200px"></cc1:richtexteditor></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD><asp:label id="Label3" CssClass="label" runat="server">Staff</asp:label></TD>
		<TD><asp:radiobuttonlist id="optStaff" runat="server" RepeatDirection="Horizontal">
				<asp:ListItem Value="0">To</asp:ListItem>
				<asp:ListItem Value="1">CC</asp:ListItem>
				<asp:ListItem Value="2">BCC</asp:ListItem>
				<asp:ListItem Value="2">Not</asp:ListItem>
			</asp:radiobuttonlist></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD><asp:label id="Label7" CssClass="label" runat="server">Supervisor</asp:label></TD>
		<TD><asp:radiobuttonlist id="optSupervisor" runat="server" RepeatDirection="Horizontal">
				<asp:ListItem Value="0">To</asp:ListItem>
				<asp:ListItem Value="1">CC</asp:ListItem>
				<asp:ListItem Value="2">BCC</asp:ListItem>
				<asp:ListItem Value="3">Not</asp:ListItem>
			</asp:radiobuttonlist></TD>
		<TD></TD>
	</TR>
	<TR id="trLevelParent" runat="server">
		<TD><asp:label id="Label8" CssClass="label" runat="server">Line Manager</asp:label></TD>
		<TD><asp:radiobuttonlist id="optLineManager" runat="server" RepeatDirection="Horizontal">
				<asp:ListItem Value="0">To</asp:ListItem>
				<asp:ListItem Value="1">CC</asp:ListItem>
				<asp:ListItem Value="2">BCC</asp:ListItem>
				<asp:ListItem Value="3">Not</asp:ListItem>
			</asp:radiobuttonlist></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD>
			<asp:label id="Label2" runat="server" CssClass="label">HR Group</asp:label></TD>
		<TD>
			<asp:radiobuttonlist id="optHR" runat="server" RepeatDirection="Horizontal">
				<asp:ListItem Value="0">To</asp:ListItem>
				<asp:ListItem Value="1">CC</asp:ListItem>
				<asp:ListItem Value="2">BCC</asp:ListItem>
				<asp:ListItem Value="3">Not</asp:ListItem>
			</asp:radiobuttonlist></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD vAlign="middle" align="center" colSpan="4">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:linkbutton id="btnSave" accessKey="S" CssClass="btnSave" runat="server" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:linkbutton id="btnAddnew" accessKey="D" CssClass="btnExport" runat="server" ToolTip="Alt+E"> Cancel</asp:linkbutton></TD>
	</TR>
	<TR>
		<TD id="tdPages" noWrap colSpan="4"><uc1:columnlist id="uctrlColumns" runat="server"></uc1:columnlist></TD>
	</TR>
	<TR>
		<TD vAlign="middle" align="center" colSpan="4">
			<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="98%" border="0">
				<TR>
					<TD><asp:datagrid id="dtgList" CssClass="grid" runat="server" Width="100%" AllowPaging="True" AllowSorting="True"
							AutoGenerateColumns="False" CellPadding="0" BorderWidth="1px" BorderColor="#3366CC" BackColor="White"
							HorizontalAlign="Center">
<FooterStyle HorizontalAlign="Center" CssClass="gridFooter">
</FooterStyle>

<AlternatingItemStyle CssClass="gridAlter">
</AlternatingItemStyle>

<ItemStyle CssClass="gridItem">
</ItemStyle>

<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader">
</HeaderStyle>

<Columns>
<asp:BoundColumn Visible="False" DataField="TemplateID" HeaderText="TemplateID">
<HeaderStyle Width="10%">
</HeaderStyle>

<ItemStyle HorizontalAlign="Center">
</ItemStyle>
</asp:BoundColumn>
<asp:TemplateColumn HeaderText="No.">
<HeaderStyle Width="5%">
</HeaderStyle>

<ItemStyle HorizontalAlign="Center">
</ItemStyle>

<ItemTemplate>
										<%# Container.ItemIndex + 1 + dtgList.PageSize*dtgList.CurrentPageIndex%>
									
</ItemTemplate>
</asp:TemplateColumn>
<asp:ButtonColumn DataTextField="TemplateID" SortExpression="TemplateID" HeaderText="Code" CommandName="edit">
<HeaderStyle Width="10%">
</HeaderStyle>

<ItemStyle HorizontalAlign="Center">
</ItemStyle>
</asp:ButtonColumn>
<asp:BoundColumn DataField="Title" SortExpression="Title" HeaderText="Title">
<HeaderStyle Width="40%">
</HeaderStyle>

<ItemStyle HorizontalAlign="Left">
</ItemStyle>
</asp:BoundColumn>
<asp:BoundColumn DataField="Staff" HeaderText="Staff">
<HeaderStyle Width="10%">
</HeaderStyle>

<ItemStyle HorizontalAlign="Left">
</ItemStyle>
</asp:BoundColumn>
<asp:BoundColumn DataField="Supervisor" HeaderText="Supervisor">
<HeaderStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Middle">
</HeaderStyle>

<ItemStyle HorizontalAlign="Left">
</ItemStyle>
</asp:BoundColumn>
<asp:BoundColumn DataField="LM" HeaderText="LM">
<HeaderStyle Width="10%">
</HeaderStyle>
</asp:BoundColumn>
<asp:BoundColumn DataField="HR" HeaderText="HR">
<HeaderStyle Width="10%">
</HeaderStyle>
</asp:BoundColumn>
</Columns>

<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages">
</PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>
<SCRIPT language="javascript">
function validform()
{	
	if(checkisnull('txtTemplateID')==false)  return false;	
		return true;
}
function checkisnull(obj)
{
	if(document.getElementById('_ctl0_' + obj).value=='')
	{	
		GetAlertError(iTotal,DSAlert,"0003");
		document.getElementById('_ctl0_' + obj).focus();
		return false;
	}
	else
	{
		return true;
	}
}

</SCRIPT>

