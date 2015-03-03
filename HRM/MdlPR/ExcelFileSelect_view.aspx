<%@ Import Namespace ="System.Data.SqlClient" %>
<%@ Import Namespace ="System.Data" %>
<%@ Import Namespace ="iHRPCore.Com"%>
<%@ Page language="c#" Codebehind="ExcelFileSelect_view.aspx.cs" AutoEventWireup="false" Inherits="iHRPCore.MdlPR.ExcelFileSelect_view" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ExcelFileSelect</title>
		<script>
function CheckFileName(inputFileId)
{
	var obj=document.getElementById(inputFileId);
	if(!(obj.value.length>0))
	{
		alert('Please select a Excel file to Import.');
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
				alert('Please input a valid Excel file name.');
				obj.value='';
				return false;
			}
		}
		else
		{
			alert('Please input a valid Excel file name.');
			obj.value='';
			return false;	
		}
	}
}
function ViewTemplate()
{	
	window.open('./../TemplateExcel/<%=strTemplate%>','ViewFile','status=1,toolbar=0,scrollbars=1,resizable=1,alwaysRaised=Yes, top=50, left=120, width=800, height=500,1 ,align=center');
	return false;
}
function CheckAll(field)
{
	var value,j,id,ckName;
	value = field.checked;
	id = field.id;
	for (j=2; j<grdList.rows.length + 2; j++)
	{
		document.Form1("grdList:_ctl" + j + ":chkSelect").checked = value;
	}
}
		</script>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<LINK href="../Include/myStyles.css" type="text/css" rel="stylesheet">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:panel id="Panel1" style="Z-INDEX: 103; LEFT: 8px; POSITION: absolute; TOP: 8px" runat="server"
				HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Outset" BorderColor="SteelBlue"
				Width="100%">
				<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
					<TR>
						<TD align="center">
							<asp:Label id="lblErr" runat="server" Width="221px" CssClass="labelErr"></asp:Label></TD>
					</TR>
					<TR>
						<TD align="center">
							<asp:Label id="Label1" runat="server" Width="221px" CssClass="label">Please select an Excel file to import</asp:Label></TD>
					</TR>
					<TR>
						<TD align="center"><INPUT id="txtFile" style="WIDTH: 100%; HEIGHT: 22px" type="file" size="41" name="inputFile"
								runat="server"></TD>
					</TR>
					<TR>
						<TD align="center">
							<asp:linkbutton id="btnViewGrid" runat="server" Width="91px" CssClass="btnImport" Height="26px">View Grid</asp:linkbutton>
							<asp:linkbutton id="btnSaveGrid" runat="server" Width="91px" CssClass="btnImport" Height="26px">Save Grid</asp:linkbutton>
							<asp:linkbutton id="btnImport" runat="server" Width="91px" CssClass="btnImport" Height="26px">Import</asp:linkbutton>
							<asp:linkbutton id="btnTemplate" runat="server" Width="120px" CssClass="btnImport" Height="26">View Template</asp:linkbutton></TD>
					</TR>
					<TR>
						<TD>
							<asp:datagrid id="grdList" runat="server" Width="100%" BorderColor="#3366CC" BorderWidth="1px"
								CssClass="grid" AutoGenerateColumns="True" CellPadding="0" BackColor="White">
								<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
								<ItemStyle CssClass="gridItem"></ItemStyle>
								<HeaderStyle CssClass="gridHeader"></HeaderStyle>
								<FooterStyle CssClass="gridFooter"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="Chọn">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderTemplate>
											<asp:CheckBox id="chkSelectAll" onclick="CheckAll(this)"
												runat="server" CssClass="checkbox"></asp:CheckBox>
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
				</TABLE>&nbsp;&nbsp; 
<BR><BR></asp:panel></form>
	</body>
</HTML>
