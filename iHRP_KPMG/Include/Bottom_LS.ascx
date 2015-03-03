<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Bottom_LS.ascx.cs" Inherits="iHRPCore.Include.Bottom_LS" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<script language="javascript" src="../Include/common.js"></script>
<script language="javascript">
	function ShowSetup(sUrl) 
	{
		//alert(sUrl);
		ShowDialog(sUrl,750,450);
		return false;
	}
</script>
<table width="100%" border="0" cellpadding="0" cellspacing="0">
	<tr>
		<td align="left"><asp:Label id="Label1" runat="server">Setup:</asp:Label>&nbsp;
		<%if(tbl != null && tbl.Rows.Count > 0){%>
				<%for (i=0; i<tbl.Rows.Count; i++){%>
			<a class='<%=tbl.Rows[i]["cssClass"]%>' 
onclick="return ShowSetup('<%=tbl.Rows[i]["Url"]%>?ModuleID=<%=strModuleID%>&amp;ParentID=<%=strParentID%>&amp;FunctionID=<%=tbl.Rows[i]["FunctionID"]%>&amp;Ascx=<%=tbl.Rows[i]["Ascx"]%>&amp;&NotLeft=true&amp;&NotTop=true&amp;&NotBottom=true');" 
href='<%=tbl.Rows[i]["Url"]%>?ModuleID=<%=strModuleID%>&amp;ParentID=<%=strParentID%>&amp;FunctionID=<%=tbl.Rows[i]["FunctionID"]%>&amp;Ascx=<%=tbl.Rows[i]["Ascx"]%>'>
				<%if( i > 0){%>
				&nbsp;/&nbsp;<%}%>
				<%=tbl.Rows[i]["FunctionName"]%>
			<%}}%>
		
		</td>
		<td align="center"></td>
	</tr>
</table>
