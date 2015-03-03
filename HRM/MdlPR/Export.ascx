<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Export.ascx.cs" Inherits="MdlPR.Export" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../Include/EmpHeaderSearch.ascx" %>
<table width="100%">
	<tr>
		<td colspan="4" align="center">
			<uc1:EmpHeaderSearch id="EmpHeaderSearch1" IsStatus="false" runat="server"></uc1:EmpHeaderSearch>
		</td>
	</tr>
	<TR style="DISPLAY:none">
		<TD style="WIDTH: 128px; HEIGHT: 19px"></TD>
		<TD style="HEIGHT: 19px"></TD>
		<TD style="HEIGHT: 19px"><asp:label id="Label5" runat="server" CssClass="label">Order by</asp:label></TD>
		<TD style="HEIGHT: 19px">
			<asp:DropDownList id="cboOrderby" runat="server" CssClass="Combo"></asp:DropDownList>
			<asp:DropDownList id="cboSort" runat="server" CssClass="Combo">
				<asp:ListItem Value="Increase">Increase</asp:ListItem>
				<asp:ListItem Value="Decrease">Decrease</asp:ListItem>
			</asp:DropDownList></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 128px">
			<asp:Label id="Label2" runat="server" CssClass="labelrequire">Emp's Formula</asp:Label></TD>
		<TD>
			<asp:DropDownList id="cboFormula" runat="server" CssClass="combo"></asp:DropDownList></TD>
		<TD></TD>
		<TD></TD>
	</TR>
	<tr>
		<td style="WIDTH: 128px">
			<asp:Label id="Label1" runat="server" CssClass="labelrequire">Month (mm/yyyy)</asp:Label></td>
		<td>
			<asp:TextBox id="txtMMYYYY" onblur="CheckMonthYear(this)" runat="server" CssClass="input"></asp:TextBox></td>
		<td>
			<asp:Label id="Label3" runat="server" CssClass="labelrequire">Name Export</asp:Label></td>
		<td>
			<asp:DropDownList id="cboNameExport" runat="server" CssClass="combo"></asp:DropDownList></td>
	</tr>
	<TR>
		<TD style="WIDTH: 128px"></TD>
		<TD></TD>
		<TD></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 128px"></TD>
		<TD>
			<asp:Button id="cmdView" runat="server" CssClass="btnAddnew" Text="Export"></asp:Button></TD>
		<TD></TD>
		<TD></TD>
	</TR>
</table>
<script language="javascript">
<!--	
	function checkvalidSearch()
	{
	
		if (document.getElementById('_ctl0_cboFormula').value == "")
		{
			GetAlertError(iTotal,DSAlert,"0003");
			document.getElementById('_ctl0_cboFormula').focus();
			return false;
		}	
		if (document.getElementById('_ctl0_txtMMYYYY').value == "")
		{
			GetAlertError(iTotal,DSAlert,"0003");
			document.getElementById('_ctl0_txtMMYYYY').focus();
			return false;
		}	
		if (document.getElementById('_ctl0_cboNameExport').value == "")
		{
			GetAlertError(iTotal,DSAlert,"0003");
			document.getElementById('_ctl0_cboNameExport').focus();
			return false;
		}
		
		return true;		
	}
//-->
</script>
