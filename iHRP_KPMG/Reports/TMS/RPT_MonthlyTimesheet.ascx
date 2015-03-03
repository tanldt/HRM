<%@ Control Language="c#" AutoEventWireup="false" Codebehind="RPT_MonthlyTimesheet.ascx.cs" Inherits="MdlTMS.RPT_MonthlyTimesheet" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../../Include/EmpHeaderSearch.ascx" %>
<%@ Register TagPrefix="uc" TagName="EmpHeader" Src="../../Include/EmpHeader.ascx" %>
<%@ Import Namespace ="System.Data.SqlClient" %>
<%@ Import Namespace ="System.Data" %>
<%@ Import Namespace ="iHRPCore.Com"%>
<%@ Import Namespace ="iHRPCore.TMSComponent"%>
<%@ Register TagPrefix="uc1" TagName="EmpHeader" Src="../../Include/EmpHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="IndividualHeader" Src="../../Include/IndividualHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ColumnList" Src="../../Include/ColumnList.ascx" %>
<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
	<TR>
		<TD align="center" colSpan="2"><asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center" colSpan="2"><uc1:empheadersearch id="EmpHeaderSearch1" runat="server"></uc1:empheadersearch></TD>
	</TR>
	<tr>
		<td width="5%"><asp:label id="Label2" CssClass="labelRequire" runat="server" Width="50px">&nbsp;&nbsp;Month</asp:label></td>
		<td><asp:textbox id="txtMonthYear" onblur="CheckMonthYear(this)" CssClass="input" runat="server"
				Width="55px" MaxLength="7"></asp:textbox></td>
	</tr>
	<tr>
		<td colSpan="2"><asp:radiobuttonlist id="RadStatus" runat="server" Width="216px" Visible="False" RepeatDirection="Horizontal">
				<asp:ListItem Value="1" Selected="True">Set</asp:ListItem>
				<asp:ListItem Value="0">Not yet</asp:ListItem>
			</asp:radiobuttonlist></td>
	</tr>
	<tr>
		<td style="HEIGHT: 20px" align="center" colSpan="2">&nbsp;
			<asp:linkbutton id="btnSearch" accessKey="S" CssClass="btnAddnew" runat="server" ToolTip="ALT+F">View</asp:linkbutton>&nbsp;&nbsp;
			<asp:linkbutton id="btnSave" accessKey="N" CssClass="btnAddnew" runat="server" Visible="False" ToolTip="Alt+S"> Save</asp:linkbutton>&nbsp;&nbsp;
			<asp:linkbutton id="btnDelete" accessKey="N" CssClass="btnAddnew" runat="server" Visible="False"
				ToolTip="Alt+C">Delete</asp:linkbutton>&nbsp;&nbsp;
			<asp:linkbutton id="btnExport" accessKey="S" CssClass="btnAddnew" runat="server" ToolTip="ALT+E">Export</asp:linkbutton>&nbsp;
			<asp:linkbutton id="btnImport" accessKey="N" CssClass="btnAddnew" runat="server" Visible="False"
				ToolTip="Alt+C">Import</asp:linkbutton></td>
	</tr>
	<TR>
		<td style="HEIGHT: 5px" colSpan="2">
			<hr>
			<uc1:columnlist id="uctrlColumns" runat="server"></uc1:columnlist>
		</td>
	</TR>
	<TR>
		<TD style="HEIGHT: 22px" colSpan="2">
			<P><asp:datagrid id="dtgList" CssClass="grid" runat="server" Width="100%" AllowPaging="True" BackColor="White"
					BorderColor="#3366CC" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False" AllowSorting="True"
					PageSize="50">
					<FooterStyle CssClass="gridFooter"></FooterStyle>
					<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
					<ItemStyle CssClass="gridItem"></ItemStyle>
					<HeaderStyle CssClass="gridHeader" BackColor="#E0E0E0"></HeaderStyle>
					<Columns>
						<asp:TemplateColumn HeaderText="&lt;b&gt;Seq&lt;/b&gt;">
							<HeaderStyle Width="5%"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<ItemTemplate>
								<%# Container.ItemIndex + 1 + dtgList.PageSize*dtgList.CurrentPageIndex%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn Visible="False" DataField="EmpID"></asp:BoundColumn>
						<asp:BoundColumn DataField="EmpCode" HeaderText="EmpCode">
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="EmpName" HeaderText="Emp Name">
							<ItemStyle HorizontalAlign="Left"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="NormalWorking" HeaderText="NormalWorking"></asp:BoundColumn>
						<asp:BoundColumn DataField="PayOT150" HeaderText="OT 150%"></asp:BoundColumn>
						<asp:BoundColumn DataField="PayOT200" HeaderText="OT 200%"></asp:BoundColumn>
						<asp:BoundColumn DataField="PayOT300" HeaderText="OT 300%"></asp:BoundColumn>
						<asp:BoundColumn DataField="PayOT30" HeaderText="OT 30%"></asp:BoundColumn>
						<asp:BoundColumn DataField="TotalOT" HeaderText="Total OT"></asp:BoundColumn>
						<asp:BoundColumn DataField="CollectWorking" HeaderText="CollectWorking"></asp:BoundColumn>
					</Columns>
					<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
				</asp:datagrid></P>
		</TD>
	</TR>
</TABLE>
<script language="javascript">
function GridCheck_ (GridName, BeginIndex, EndIndexExt, CtlCheckName)
{
	var i;
	var count;
	var NoItemCheck;
					
	count = document.getElementById(GridName).rows.length;		
	//alert(count);
	NoItemCheck = true;		
	if (count >= 1 )
	{		
		for (i = BeginIndex; i <document.getElementById(GridName).rows.length + EndIndexExt ; i++)
		{	if (document.getElementById(GridName + "__ctl" + i + "_" + CtlCheckName).checked == true)
			{				
				NoItemCheck = false;
				break;
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
function ShowExcelSelectPage()
{		
	window.open('./MdlTMS/FileSelect.aspx?tpl=../TemplateExcel/TimeSheet_FileSelect.xls&Store=TMS_spfrmTimeSheet&Act=SaveImport','SelectFile','status=1,toolbar=0,scrollbars=1,resizable=0,alwaysRaised=Yes, top=20, left=30, width=800, height=600,1 ,align=center');
	return false;
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

function CheckCancel()
{
	if (document.getElementById('_ctl0_txtEmpID').value == "")
	{
		GetAlertError(iTotal,DSAlert,"0023");
		return false;
	}
	
	if(GridCheck('_ctl0_dtgOT',2,1,'chkSelect')==false)
	{			
		GetAlertError(iTotal,DSAlert,"0001");				
		return false;
	}
	if(confirm(GetAlertText(iTotal,DSAlert,"0002"))==false){
		return false;
	}			
}
function ViewCalendar(field, CtlName)
{
	var Prefix = (field.id).substring(field.id.length - (field.id.length - field.id.lastIndexOf("_")) + 1,0);
	ShowCalendar(Prefix + CtlName);
	return false;
}
function CheckSearch()
{	
	if (document.getElementById('_ctl0_txtMonthYear').value == "")
	{
		GetAlertError(iTotal,DSAlert,"0003");
		document.getElementById('_ctl0_txtMonthYear').focus();
		return false;
	}
	if(document.getElementById("_ctl0_EmpHeaderSearch1_cboLevel1").value=="")
	{   alert("You must choose a company!");
		return false;
	}
	return true;
}

function CheckSave()
{
	if(GridCheck_('_ctl0_dtgList',3,1,'chkSelect')==false)
	{		
		GetAlertError(iTotal,DSAlert,"0049");
		return false;
	}
}
function CheckExport()
{
	if(document.getElementById("_ctl0_EmpHeaderSearch1_cboLevel1").value=="")
	{   alert("You must choose a company!");
		return false;
	}
}
function CheckDelete()
{
	if(GridCheck_('_ctl0_dtgList',3,1,'chkSelect')==false)
	{
		
		GetAlertError(iTotal,DSAlert,"0049");
		return false;
	}
	return true;
}
</script>
