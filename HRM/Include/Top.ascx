<%@ Register TagPrefix="uc1" TagName="TopModule" Src="TopModule.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopMenu" Src="TopMenu.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Top.ascx.cs" Inherits="iHRPCore.Pagelets.Top" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<TABLE cellSpacing="0" cellPadding="0" width="783" border="0">
	<TR>
		<TD rowSpan="2">
			<asp:LinkButton id="btnLogo" CssClass="btnLogo" runat="server"></asp:LinkButton></TD>
		<TD colSpan="2">
			<asp:LinkButton id="btnTop02" CssClass="btnTop02" runat="server"></asp:LinkButton></TD>
		<TD><IMG height="43" alt="" src="images/top-banner_03.gif" width="16"></TD>
		<TD vAlign="top" align="right" background="IMAGES\top-banner_04.gif" colSpan="2"><asp:linkbutton id="btnHome" runat="server" CssClass="btnHome"></asp:linkbutton><asp:linkbutton id="btnVN" runat="server" CssClass="btnVN"></asp:linkbutton><asp:linkbutton id="btnEN" runat="server" CssClass="btnEN"></asp:linkbutton>
			<!--<IMG SRC="images/top-banner_04.gif" WIDTH="200" HEIGHT="43" ALT="">--></TD>
	</TR>
	<TR>
		<TD><IMG height="27" alt="" src="images/top-banner_05.gif" width="52"></TD>
		<TD align="center" width="610" background="images/top-banner_06.gif" colSpan="3" height="27"><uc1:topmodule id="TopModule1" runat="server"></uc1:topmodule></TD>
		<td align="right" width="60" background="images/top-banner_06.gif" height="27">
			<asp:linkbutton id="btnLogout" runat="server" CssClass="btnLogout">Logout</asp:linkbutton>
		</td>
	</TR>
	<TR>
		<TD colspan="6" align=center>
			<uc1:TopMenu id="TopMenu1" runat="server"></uc1:TopMenu></TD>
	</TR>
	<TR>
		<TD><IMG height="1" alt="" src="images/spacer.gif" width="125"></TD>
		<TD><IMG height="1" alt="" src="images/spacer.gif" width="52"></TD>
		<TD><IMG height="1" alt="" src="images/spacer.gif" width="394"></TD>
		<TD><IMG height="1" alt="" src="images/spacer.gif" width="16"></TD>
		<TD><IMG height="1" alt="" src="images/spacer.gif" width="140"></TD>
		<TD><IMG height="1" alt="" src="images/spacer.gif" width="60"></TD>
	</TR>
</TABLE>
<script>
	function ChangeLanguage()
	{
		if ('<%=Session["LangID"]%>' == null || '<%=Session["LangID"]%>'  == "EN")
		{
			document.getElementById("Top1_btnLogout").innerHTML = "Logout"; 
		}
		else
		{
			document.getElementById("Top1_btnLogout").innerHTML = "Thoát"; 
		}
	}
	ChangeLanguage();
</script>

