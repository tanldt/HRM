<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../Include/EmpHeaderSearch.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="PIT_05A_BK_TNCN_BDK.ascx.cs" Inherits="Reports.PIT.PIT_05A_BK_TNCN_BDK" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table width="100%">
	<tr>
		<td colspan="4" align="center">
			<uc1:EmpHeaderSearch id="EmpHeaderSearch1" IsStatus="false" runat="server"></uc1:EmpHeaderSearch>
		</td>
	</tr>
	<tr>
		<td style="WIDTH: 128px">
			<asp:Label id="Label1" runat="server" CssClass="labelrequire">Month (mm/yyyy)</asp:Label></td>
		<td>
			<asp:TextBox id="txtMMYYYY" onblur="CheckMonthYear(this)" runat="server" CssClass="input"></asp:TextBox></td>
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
		if (document.getElementById('_ctl0_txtMMYYYY').value == "")
		{
			GetAlertError(iTotal,DSAlert,"0003");
			document.getElementById('_ctl0_txtMMYYYY').focus();
			return false;
		}	
		
		return true;		
	}
//-->
</script>
