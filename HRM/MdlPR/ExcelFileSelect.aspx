<%@ Page language="c#" Codebehind="ExcelFileSelect.aspx.cs" AutoEventWireup="false" Inherits="iHRPCore.MdlPR.ExcelFileSelect" %>
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
	window.open('./../TemplateExcel/SalaryGradeImportTemplate_CB.xls','ViewFile','status=1,toolbar=0,scrollbars=1,resizable=1,alwaysRaised=Yes, top=50, left=120, width=800, height=500,1 ,align=center');
	return false;
}
		</script>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<link href="../Include/myStyles.css" type="text/css" rel="stylesheet">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:Panel id="Panel1" style="Z-INDEX: 103; LEFT: 8px; POSITION: absolute; TOP: 8px" runat="server"
				Width="432px" Height="112px" BorderColor="SteelBlue" BorderStyle="Outset" BorderWidth="1px"
				HorizontalAlign="Center">
<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
					<TR>
						<TD align="center">
							<asp:Label id="Label1" runat="server" Width="221px" CssClass="label">Please select an Excel file to import</asp:Label></TD>
					</TR>
					<TR>
						<TD><INPUT id="txtFile" style="WIDTH: 100%; HEIGHT: 22px" type="file" size="41" name="inputFile"
								runat="server"></TD>
					</TR>
				</TABLE>
<asp:linkbutton id="btnImport" runat="server" Height="26px" Width="91px" CssClass="btnImport">Import</asp:linkbutton>&nbsp;&nbsp; 
<asp:linkbutton id="btnTemplate" runat="server" Height="26" Width="91" CssClass="btnImport">View Template</asp:linkbutton><BR><BR>
<asp:Label id="lblErr" runat="server" Width="221px" CssClass="labelErr"></asp:Label>
			</asp:Panel>
		</form>
	</body>
</HTML>
