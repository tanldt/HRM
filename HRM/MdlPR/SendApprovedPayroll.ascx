<%@ Control Language="c#" AutoEventWireup="false" Codebehind="SendApprovedPayroll.ascx.cs" Inherits="MdlPR.SendApprovedPayroll" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="Table2" style="WIDTH: 100%" cellSpacing="1" cellPadding="0" border="0">
	<TR>
		<TD><asp:label id="Label5" CssClass="labelRequire" runat="server"> Month</asp:label></TD>
		<TD><asp:textbox id="txtMonth" onblur="javascript:CheckMonthYear(this)" CssClass="input" runat="server"
				MaxLength="7"></asp:textbox></TD>
	</TR>
	<TR>
		<TD align="right"></TD>
		<TD></TD>
	</TR>
	<tr>
		<td align="center" colSpan="2"><asp:button id="cmdSearch" CssClass="btnExport" runat="server" Text="Search"></asp:button>&nbsp;&nbsp; 
			&nbsp;&nbsp;
			<asp:button id="cmdSave" CssClass="btnExport" runat="server" Text="Send"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:button id="cmdClose" CssClass="btnExport" runat="server" Text="Close"></asp:button>&nbsp;&nbsp;&nbsp;
		</td>
	</tr>
	<TR>
		<TD align="right"></TD>
		<TD></TD>
	</TR>
	<tr>
		<td colSpan="2"><asp:datagrid id="dtgList" CssClass="grid" runat="server" BackColor="White" BorderWidth="1px"
				CellPadding="0" AutoGenerateColumns="False" AllowSorting="True" PageSize="20" BorderColor="#3366CC" Width="100%">
				<FooterStyle CssClass="gridFooter"></FooterStyle>
				<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
				<ItemStyle CssClass="gridItem"></ItemStyle>
				<HeaderStyle CssClass="gridHeader"></HeaderStyle>
				<Columns>
					<asp:BoundColumn Visible="False" DataField="LSLevel1ID" HeaderText="LSLevel1ID"></asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="Month" HeaderText="Month"></asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="IsLock" HeaderText="IsLock"></asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="LSCompanyID" HeaderText="LSCompanyID"></asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="UserID" HeaderText="UserID"></asp:BoundColumn>
					<asp:TemplateColumn HeaderText="Select">
						<HeaderStyle Width="8%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<HeaderTemplate>
							<asp:CheckBox id="chkSelectAll" runat="server" CssClass="checkbox" onclick="CheckAll('_ctl0_dtgList__ctl1_chkSelectAll','_ctl0_dtgList',2,1,'chkSelect')"></asp:CheckBox>
						</HeaderTemplate>
						<ItemTemplate>
							<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="&lt;b&gt;Seq&lt;/b&gt;">
						<HeaderStyle Width="5%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<%# Container.ItemIndex + 1 + dtgList.PageSize*dtgList.CurrentPageIndex%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="Code" HeaderText="Company Code">
						<HeaderStyle Width="10%"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="Name" HeaderText="Company Name"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Status Payroll"></asp:BoundColumn>
					<asp:TemplateColumn HeaderText="Next Approved">
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
						<HeaderTemplate>
							<asp:Label Runat="server" ID="Label1">Next Approved</asp:Label>
						</HeaderTemplate>
						<ItemTemplate>
							<asp:DropDownList id="cboNextUserID" Width="150px" runat="server"></asp:DropDownList>&nbsp;
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
			</asp:datagrid></td>
	</tr>
</TABLE>
<SCRIPT language="javascript">

function checkvalidSearch()
{
	
	if (checkisnull("txtMonth")==false) return false;
	
	return true;		
}
function Lock()
{
	if (checkisnull("txtMonth")==false) return false;
	if(GridCheck('_ctl0_dtgList',2,1,'chkSelect')==false)
	{
		
		GetAlertError(iTotal,DSAlert,"0049");
		return false;
	}
	if (GridCheckData('_ctl0_dtgList',2,1,'chkSelect')==false){
		return false;
	}
	if(confirm(GetAlertText(iTotal,DSAlert,"PC_0002"))==false){
		return false;
	}
	return true;
}
function GridCheckData (GridName, BeginIndex, EndIndexExt, CtlCheckName)
{
	
	var i;
	var count;
	if (document.getElementById(GridName) != null)
	{
		count = document.getElementById(GridName).rows.length;		
		if (count >= 1 )
		{
			for (i = BeginIndex; i <document.getElementById(GridName).rows.length + EndIndexExt ; i++)
			{	
				if (document.getElementById(GridName + "__ctl" + i + "_" + CtlCheckName) != null)
				{
					if (document.getElementById(GridName + "__ctl" + i + "_" + CtlCheckName).checked == true)
					{	
						if (document.getElementById(GridName + "__ctl" + i + "_cboNextUserID").value=="")
						{
							GetAlertError(iTotal,DSAlert,"0003");
							document.getElementById(GridName + "__ctl" + i + "_cboNextUserID").focus(); 
							return false;
						}
					}
				}
			}
		}
	}
	return true;	
}
function GridCheck_ (GridName, BeginIndex, EndIndexExt, CtlCheckName)
{
	var i;
	var count;
	var NoItemCheck;
	if (document.getElementById(GridName) != null)
	{		
		count = document.getElementById(GridName).rows.length;		
		//alert(count);
		NoItemCheck = true;		
		if (count >= 1 )
		{		
			for (i = BeginIndex; i <document.getElementById(GridName).rows.length + EndIndexExt ; i++)
			{	
				if (document.getElementById(GridName + "__ctl" + i + "_" + CtlCheckName) != null)
				{
					if (document.getElementById(GridName + "__ctl" + i + "_" + CtlCheckName).checked == true)
					{				
						NoItemCheck = false;
						break;
					}
				}
			}				
		}
	}
	
	if (NoItemCheck)
	{
		return false;
	}
	else 
		return true;
}
</SCRIPT>
