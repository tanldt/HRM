<%@ Control Language="c#" AutoEventWireup="false" Codebehind="CreaterFooter.ascx.cs" Inherits="Include.CreaterFooter" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="50%" border="0">
	<TR id="trID" runat="server">
		<TD>&nbsp;
			<asp:LinkButton id="btnAuthor" runat="server" CssClass="btnAuthority"></asp:LinkButton></TD>
	</TR>
</TABLE>
<script>
function Reload()
{
	document.location = document.location;			
}
</script>
<script><asp:Literal id="ltlAlert" runat="server" EnableViewState="False"></asp:Literal></script>
