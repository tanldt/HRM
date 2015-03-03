<%@ Control Language="c#" AutoEventWireup="false" Codebehind="SetFormulaForCom.ascx.cs" Inherits="MdlTMS.SetFormulaForCom" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
	<TR>
		<TD align="center" colSpan="2"><asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></TD>
	</TR>
	<tr>
		<td style="HEIGHT: 20px" align="center" colSpan="2">&nbsp;
			<asp:linkbutton id="btnSave" accessKey="N" runat="server" CssClass="btnAddnew" ToolTip="Alt+S"> Update</asp:linkbutton>&nbsp;&nbsp;
		</td>
	</tr>
	<TR>
		<td style="HEIGHT: 5px" colSpan="2">
			<hr>
		</td>
	</TR>
	<TR>
		<TD style="HEIGHT: 22px" colSpan="2">
			<P><asp:datagrid id="dtgList" runat="server" CssClass="grid" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
					CellPadding="0" BorderWidth="1px" BorderColor="#3366CC" BackColor="White" AllowPaging="True"
					PageSize="50">
					<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
					<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
					<ItemStyle CssClass="gridItem"></ItemStyle>
					<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader" BackColor="#E0E0E0"></HeaderStyle>
					<Columns>
						<asp:BoundColumn DataField="LSLevel1ID" Visible="False" HeaderText="LSLevel1ID"></asp:BoundColumn>
						<asp:BoundColumn DataField="SetFormulaID" Visible="False" HeaderText="EmpCode"></asp:BoundColumn>
						<asp:TemplateColumn HeaderText="&lt;b&gt;Seq&lt;/b&gt;">
							<HeaderStyle Width="5%"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<ItemTemplate>
								<%# Container.ItemIndex + 1%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn DataField="LSLevel1Code" HeaderText="Code"></asp:BoundColumn>
						<asp:BoundColumn DataField="Name" HeaderText="Company"></asp:BoundColumn>
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
								<asp:CheckBox id="chkSelectAll" runat="server" CssClass="checkbox" onclick="CheckAll('_ctl0_dtgList__ctl2_chkSelectAll','_ctl0_dtgList',3,1,'chkSelect')"></asp:CheckBox>
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
		<TD colSpan="2">
		</TD>
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
	if(GridCheck_('_ctl0_dtgList',3,1,'chkSelect')==false)
	{
		
		GetAlertError(iTotal,DSAlert,"0049");
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
	
	//if (GridCheckData('_ctl0_dtgList',3,1,'chkSelect')==false){
	//	return false;
	//}
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
						if (document.getElementById(GridName + "__ctl" + i + "_txtStandardworking").value=="")
						{
							GetAlertError(iTotal,DSAlert,"0003");
							document.getElementById(GridName + "__ctl" + i + "_txtStandardworking").focus(); 
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
</script>
</SCRIPT>
