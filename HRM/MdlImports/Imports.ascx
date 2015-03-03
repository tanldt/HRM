<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Imports.ascx.cs" Inherits="iHRPCore.Imports" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<script>
function CheckFileName(inputFileId)
{
	var obj=document.getElementById(inputFileId);
	if(!(obj.value.length>0))
	{
		alert("Please select file Excel to import!");
		return false;
	}
	else 
	{
		var arrName=obj.value.split('.');
		if(arrName.length>0)
		{
			if(arrName[arrName.length-1]=='xls') return true;
			else
			{
				alert("Please select file Excel!");
				obj.value='';
				return false;
			}
		}
		else
		{
			alert("Please select file Excel!");
			obj.value='';
			return false;	
		}
	}
}
function ViewTemplate()
{	
	window.open('./<%=strTemplate%>','ViewFile','status=1,toolbar=0,scrollbars=1,resizable=1,alwaysRaised=Yes, top=50, left=120, width=800, height=500,1 ,align=center');
	return false;
}
function CheckSave()
{
	if (document.getElementById('_ctl0_grdList') == null)
	{
		alert("Please select file Excel and view grid before click save!");
		return false;
	}
	else
	{
		if(GridCheck('_ctl0_grdList',2,1,'chkSelect')==false)
		{
			alert("Please select at least one record!");
			return false;
		}
		/*
		if(confirm('Các dòng du?c dánh d?u s? b? xoá?')==false)
		{
			return false;
		}
		*/
	}
}
function GridCheck (GridName, BeginIndex, EndIndexExt, CtlCheckName)
{
	var i;
	var count;
	var NoItemCheck;
					
	count = document.getElementById(GridName).rows.length;		
	NoItemCheck = true;		
	if (count >1 )
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
function checkAll(obj)
{
	value = obj.checked;					
	for (j=3; j<_ctl0_grdList.rows.length + 1; j++)
	{
		document.getElementById("_ctl0_grdList" + "__ctl" + j + "_" + "chkSelect").checked = value;
	}
}

</script>
<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
	<TR>
		<TD style="FONT-SIZE: 16px; FONT-WEIGHT: bold" align="center" height="20" colspan="2">IMPORT 
			DATA</TD>
	</TR>
	<TR>
		<TD align="center" colspan="2">
			<asp:Label id="lblErr" runat="server" Width="221px" ForeColor="Red"></asp:Label></TD>
	</TR>
	<TR>
		<TD align="center" colspan="2"></TD>
	</TR>
	<TR id="trTypeImport" runat="server">
		<TD align="right"><asp:label id="Label10" runat="server" Width="180px" CssClass="label">Type import&nbsp;&nbsp;&nbsp;&nbsp;</asp:label></TD>
		<TD><asp:radiobuttonlist id="optTypeImport" runat="server" CssClass="option" RepeatDirection="Horizontal">
				<asp:ListItem Value="Insert" Selected="True">Insert</asp:ListItem>
				<asp:ListItem Value="Update">Update</asp:ListItem>
			</asp:radiobuttonlist>
		</TD>
	</TR>
	<TR>
		<TD align="right"><asp:Label id="Label1" runat="server" Width="180px" CssClass="label">Please select file excel to import&nbsp;&nbsp;&nbsp;&nbsp;</asp:Label></TD>
		<TD><INPUT id="txtFile" style="HEIGHT: 22px" type="file" size="41" name="inputFile" runat="server"></TD>
	</TR>
	<TR>
		<TD align="center" colspan="2">
			<asp:linkbutton id="btnViewGrid" runat="server" Width="91px" CssClass="btnImport" Height="26px">View Grid</asp:linkbutton>
			<asp:linkbutton id="btnSaveGrid" runat="server" Width="91px" CssClass="btnImport" Height="26px">Save Grid</asp:linkbutton>
			<asp:linkbutton id="btnTemplate" runat="server" Width="95px" CssClass="btnImport" Height="26px">View Template</asp:linkbutton></TD>
	</TR>
	<tr>
		<td colspan="2">
			<asp:Label id="Label2" runat="server" ForeColor="SaddleBrown">Note: Column Date is format dd/mm/yyyy (ex: 16/02/2009)</asp:Label>
		</td>
	</tr>
	<TR>
		<TD colspan="2">
			<asp:datagrid id="grdList" runat="server" Width="100%" BorderColor="#3366CC" BorderWidth="1px"
				CssClass="grid" BackColor="White" CellPadding="0" AutoGenerateColumns="True">
				<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
				<ItemStyle CssClass="gridItem"></ItemStyle>
				<HeaderStyle CssClass="gridHeader"></HeaderStyle>
				<FooterStyle CssClass="gridFooter"></FooterStyle>
				<Columns>
					<asp:TemplateColumn HeaderText="Ch?n">
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<HeaderTemplate>
							<asp:CheckBox id="chkSelectAll" onclick="checkAll(this)" runat="server" CssClass="checkbox"></asp:CheckBox>
						</HeaderTemplate>
						<ItemTemplate>
							<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Error">
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="lblErrorDtg" runat="server" CssClass="label"></asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:datagrid></TD>
	</TR>
</TABLE>
&nbsp;&nbsp;
<asp:linkbutton id="btnImport" runat="server" Width="91px" CssClass="btnImport" Height="26px" Visible="False">Import</asp:linkbutton><BR>
<BR>
