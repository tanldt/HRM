<%@ Register TagPrefix="uc1" TagName="ColumnList" Src="../Include/ColumnList.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="PITIncomeTax.ascx.cs" Inherits="MdlPR.PITIncomeTax" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../Include/EmpHeaderSearch.ascx" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD align="center">
			<asp:label id="lblErr" CssClass="label" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center">&nbsp;</TD>
	</TR>
	<TR>
		<TD align="center">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="90%" border="0">
				<TR>
					<TD colspan="4">
						<uc1:EmpHeaderSearch id="EmpHeaderSearch1" runat="server"></uc1:EmpHeaderSearch></TD>
				</TR>
				<TR>
					<TD>
						<asp:label id="lblYear" Width="20%" runat="server" CssClass="label">Year</asp:label></TD>
					<TD>
						<asp:textbox id="txtYear" onblur="CheckYear(this);" Width="88px" runat="server" CssClass="input"
							MaxLength="10"></asp:textbox></TD>
					<TD>
						<asp:label id="Label1" CssClass="label" runat="server">Month/year add to salary</asp:label>&nbsp;
						<asp:textbox id="txtMMYYYY" onblur="CheckMonthYear(this)" CssClass="input" runat="server" Width="80px"
							MaxLength="7"></asp:textbox>
					</TD>
					<TD>
					</TD>
				</TR>
				<TR>
					<TD>&nbsp;</TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD align="center" colspan="3">
						<asp:linkbutton id="btnCalculate" accessKey="S" CssClass="btnSave" runat="server" ToolTip="ALT+S">Calculate</asp:linkbutton>&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnExport" accessKey="E" CssClass="btnSave" runat="server" ToolTip="Alt+E, Export data to excel file"
							Visible="False">Export</asp:linkbutton>
					</TD>
				</TR>
			</TABLE>
			<HR width="100%" SIZE="1">
		</TD>
	</TR>
	<TR>
		<TD align="center" style="HEIGHT: 8px">&nbsp;&nbsp;&nbsp;&nbsp;</TD>
	</TR>
</TABLE>
<script language="javascript">
function CheckData()
{
	if(checkisnull('txtYear')==false) return false;
	if(checkisnull('txtMMYYYY')==false) return false;
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

</script>
