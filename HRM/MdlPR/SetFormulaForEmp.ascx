<%@ Control Language="c#" AutoEventWireup="false" Codebehind="SetFormulaForEmp.ascx.cs" Inherits="MdlPR.SetFormulaForEmp" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="ColumnList" Src="../Include/ColumnList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../Include/EmpHeaderSearch.ascx" %>
<%@ Register TagPrefix="uc" TagName="EmpHeader" Src="../Include/EmpHeader.ascx" %>
<%@ Import Namespace ="System.Data.SqlClient" %>
<%@ Import Namespace ="System.Data" %>
<%@ Import Namespace ="iHRPCore.Com"%>
<%@ Import Namespace ="iHRPCore.TMSComponent"%>
<%@ Register TagPrefix="uc1" TagName="EmpHeader" Src="../Include/EmpHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="IndividualHeader" Src="../Include/IndividualHeader.ascx" %>
<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
	<TR>
		<TD align="center" colSpan="2"><asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center" colSpan="2"><uc1:empheadersearch id="EmpHeaderSearch1" runat="server"></uc1:empheadersearch></TD>
	</TR>
	<tr>
		<td width="5%"></td>
		<td>&nbsp;
			<asp:radiobuttonlist id="RadStatus" runat="server" Width="216px" RepeatDirection="Horizontal">
				<asp:ListItem Value="1">Set</asp:ListItem>
				<asp:ListItem Value="0" Selected="True">Not yet</asp:ListItem>
			</asp:radiobuttonlist></td>
	</tr>
	<tr>
		<td style="HEIGHT: 20px" align="center" colSpan="2">&nbsp;
			<asp:linkbutton id="btnSearch" accessKey="S" CssClass="btnAddnew" runat="server" ToolTip="ALT+F">Search</asp:linkbutton>&nbsp;&nbsp;
			<asp:linkbutton id="btnSave" accessKey="N" CssClass="btnAddnew" runat="server" ToolTip="Alt+S"> Save</asp:linkbutton>&nbsp;&nbsp;
			<asp:linkbutton id="btnDelete" accessKey="N" CssClass="btnAddnew" runat="server" ToolTip="Alt+C">Delete</asp:linkbutton></td>
	</tr>
	<TR>
		<td style="HEIGHT: 5px" colSpan="2">
			<hr>
		</td>
	</TR>
	<TR>
		<TD style="HEIGHT: 22px" colSpan="2">
			<P><asp:datagrid id="dtgFormula" CssClass="grid" runat="server" Width="100%" PageSize="50" AllowPaging="True"
					BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False"
					AllowSorting="True">
					<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
					<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
					<ItemStyle CssClass="gridItem"></ItemStyle>
					<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader" BackColor="#E0E0E0"></HeaderStyle>
					<Columns>
						<asp:BoundColumn DataField="EmpID" Visible="False" HeaderText="EmpCode"></asp:BoundColumn>
						<asp:BoundColumn DataField="SetFormulaID" Visible="False" HeaderText="EmpCode"></asp:BoundColumn>
						<asp:TemplateColumn HeaderText="&lt;b&gt;Seq&lt;/b&gt;">
							<HeaderStyle Width="5%"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<ItemTemplate>
								<%# Container.ItemIndex + 1%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn DataField="EmpCode" HeaderText="EmpCode"></asp:BoundColumn>
						<asp:BoundColumn DataField="EmpName" HeaderText="Emp Name"></asp:BoundColumn>
						<asp:BoundColumn DataField="Level1Name" HeaderText="Company"></asp:BoundColumn>
						<asp:BoundColumn DataField="Level2Name" HeaderText="DeptName"></asp:BoundColumn>
						<asp:BoundColumn DataField="FormulaCom" HeaderText="Formula Company"></asp:BoundColumn>
						<asp:TemplateColumn HeaderText="Style Formula">
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<ItemTemplate>
								<asp:DropDownList id="cboFormula" runat="server">
									<asp:ListItem Value="1">1</asp:ListItem>
									<asp:ListItem Value="2">2</asp:ListItem>
									<asp:ListItem Value="3">3</asp:ListItem>
								</asp:DropDownList>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Select">
							<HeaderStyle Width="8%"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<HeaderTemplate>
								<asp:CheckBox id="chkSelectAll" runat="server" CssClass="checkbox" onclick="CheckAll('_ctl0_dtgFormula__ctl2_chkSelectAll','_ctl0_dtgFormula',3,1,'chkSelect')"></asp:CheckBox>
							</HeaderTemplate>
							<ItemTemplate>
								<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
					<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
				</asp:datagrid></P>
		</TD>
	</TR>
	<TR>
		<TD colSpan="2"></TD>
	</TR>
	<tr style="DISPLAY: none">
		<td colSpan="2"><asp:textbox id="txtEmpID" Runat="server"></asp:textbox></td>
	</tr>
	<TR style="DISPLAY: none">
		<TD colSpan="2"></TD>
	</TR>
</TABLE>
<script language="javascript">
	
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


function CheckSearch()
{
	return true;
}
function CheckDelete()
{
	if(GridCheck_('_ctl0_dtgFormula',3,1,'chkSelect')==false)
	{
		
		GetAlertError(iTotal,DSAlert,"0049");
		return false;
	}
	return true;
}

function CheckSave()
{
	if(GridCheck_('_ctl0_dtgFormula',3,1,'chkSelect')==false)
	{
		
		GetAlertError(iTotal,DSAlert,"0049");
		return false;
	}
	if (GridCheckData('_ctl0_dtgFormula',3,1,'chkSelect')==false){
		return false;
	}
	return true;
}
function GridCheckData (GridName, BeginIndex, EndIndexExt, CtlCheckName)
{
	
	var i;
	var count;
	count = document.getElementById(GridName).rows.length;		
	if (count >= 1 )
	{
		for (i = BeginIndex; i <document.getElementById(GridName).rows.length + EndIndexExt ; i++)
		{	if (document.getElementById(GridName + "__ctl" + i + "_" + CtlCheckName).checked == true)
			{	
				if (document.getElementById(GridName + "__ctl" + i + "_cboFormula").value=="")
				{
					GetAlertError(iTotal,DSAlert,"0003");
					document.getElementById(GridName + "__ctl" + i + "_cboFormula").focus(); 
					return false;
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
</script>
</SCRIPT>
