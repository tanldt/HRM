<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../Include/EmpHeaderSearch.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ColumnList" Src="../Include/ColumnList.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="UnfrequentIncomeType.ascx.cs" Inherits="MdlSER.UnfrequentIncomeType" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table style="HEIGHT: 81px" width="98%" border="0">
	<TR>
		<TD style="WIDTH: 90px; HEIGHT: 22px"></TD>
		<TD style="WIDTH: 50px; HEIGHT: 22px"><asp:label id="lblCode" CssClass="labelRequire" Width="100%" runat="server">Code</asp:label></TD>
		<TD style="WIDTH: 400px; HEIGHT: 22px"><asp:textbox id="txtCode" CssClass="input" Width="100%" runat="server"></asp:textbox><asp:textbox id="txtServiceID" runat="server" Visible="False">CusTypeID</asp:textbox></TD>
		<TD style="WIDTH: 100px; HEIGHT: 22px"></TD>
	</TR>
	<TR>
		<TD></TD>
		<TD><asp:label id="lblName" CssClass="labelRequire" Width="113px" runat="server">Name</asp:label></TD>
		<TD><asp:textbox id="txtName" CssClass="input" Width="100%" runat="server"></asp:textbox></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD></TD>
		<TD><asp:label id="lblNameVN" CssClass="labelRequire" Width="113px" runat="server">Name (VN)</asp:label></TD>
		<TD><asp:textbox id="txtVNName" CssClass="input" Width="100%" runat="server"></asp:textbox></TD>
		<TD></TD>
	</TR>
	<tr>
		<td></td>
		<TD><asp:label id="lblNote" Width="100%" runat="server">Note</asp:label></TD>
		<TD><asp:textbox id="txtNote" CssClass="input" Width="100%" runat="server"></asp:textbox></TD>
		<td></td>
	</tr>
	<TR>
		<TD style="HEIGHT: 21px"></TD>
		<TD style="HEIGHT: 21px"><asp:label id="lblActive" Width="100%" runat="server">Active</asp:label></TD>
		<TD style="HEIGHT: 21px"><asp:checkbox id="chkUsed" runat="server" Checked="True"></asp:checkbox></TD>
		<TD style="HEIGHT: 21px"></TD>
	</TR>
	<TR>
		<TD style="HEIGHT: 10px" align="center" colSpan="5">&nbsp;&nbsp;</TD>
	</TR>
	<TR>
		<TD style="HEIGHT: 21px" align="center" colSpan="5">&nbsp;
			<asp:linkbutton id="btnSearch" CssClass="button" runat="server">Search</asp:linkbutton>&nbsp;<asp:linkbutton id="btnSave" CssClass="button" runat="server">Save</asp:linkbutton>&nbsp;<asp:linkbutton id="btnAddnew" CssClass="button" runat="server">New record</asp:linkbutton>&nbsp;<asp:linkbutton id="btnFilter" CssClass="button" runat="server">Delete</asp:linkbutton></TD>
	</TR>
	<TR>
		<TD style="HEIGHT: 10px" align="left" colSpan="5"><uc1:columnlist id="uctrlColumns" runat="server"></uc1:columnlist></TD>
	</TR>
	<TR>
		<TD colSpan="5"><asp:datagrid id="dtgList" Width="100%" runat="server" BackColor="White" BorderColor="WhiteSmoke"
				CellPadding="0" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True">
				<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
				<ItemStyle CssClass="gridItem"></ItemStyle>
				<HeaderStyle CssClass="gridHeader"></HeaderStyle>
				<FooterStyle CssClass="gridFooter"></FooterStyle>
				<Columns>
					<asp:TemplateColumn HeaderText="Seq">
						<HeaderStyle Width="5%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<%# Container.ItemIndex + 1 + dtgList.PageSize*dtgList.CurrentPageIndex%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn Visible="False" DataField="ServiceID" HeaderText="ServiceID"></asp:BoundColumn>
					<asp:BoundColumn DataField="Code" SortExpression="Code" HeaderText="Code">
						<HeaderStyle Width="15%"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="Name" SortExpression="Name" HeaderText="Name">
						<HeaderStyle Width="30%"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="VNName" SortExpression="NVName" HeaderText="Name(VN)">
						<HeaderStyle Width="30%"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="Note" HeaderText="Note">
						<HeaderStyle Width="20%"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="Used" HeaderText="Active">
						<HeaderStyle Width="10%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundColumn>
					<asp:ButtonColumn Text="Edit" HeaderText="Edit" CommandName="EDIT"></asp:ButtonColumn>
					<asp:TemplateColumn HeaderText="Select">
						<HeaderStyle HorizontalAlign="Center" Width="1px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<HeaderTemplate>
							<asp:CheckBox id="chkSelectAll" runat="server" CssClass="checkbox" onclick="CheckAll('_ctl0_dtgList__ctl2_chkSelectAll','_ctl0_dtgList',3,1,'chkSelect')"></asp:CheckBox>
						</HeaderTemplate>
						<ItemTemplate>
							<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
			</asp:datagrid></TD>
	</TR>
</table>
<script language="javascript">
function validform(){	
	if(checkisnull('txtCode')==false)  return false;
	if(checkisnull('txtName')==false)  return false;
	if(checkisnull('txtVNName')==false)  return false;
	//if(checkisnull('txtStartDate')==false)  return false;	
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
	
function CheckDelete()
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
</script>
