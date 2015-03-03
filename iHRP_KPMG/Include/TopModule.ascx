<%@ Control Language="c#" AutoEventWireup="false" Codebehind="TopModule.ascx.cs" Inherits="iHRPCore.Pagelets.TopModule" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<SCRIPT language=JavaScript1.2 
src="<%=Request.ApplicationPath%>/Images/MenuTop/stm31.js" 
type=text/javascript>
</SCRIPT>
<table width="100%" align="center" cellpadding="0" cellspacing="0" border="0">
	<tr valign="top" height="20">
		<td width="30%">
			<SCRIPT language="JavaScript1.2" type="text/javascript">
				var somenu
				stm_bm(["ivvnair",400,"","<%=Request.ApplicationPath%>/Images/MenuTop/blank.gif",0,"","",0,0,0,0,10,1,0,0,""],this);
				stm_bp("p0",[0,4,0,0,1,4,16,0,100,"",-2,"",-2,90,1,1,"#999999","transparent","",3,0,0,"#677C99"]);
				stm_ai("p0i0",[0, '<%=strModuleCaption%>',"","",-1,-1,0,"","_self","","","<%=Request.ApplicationPath%>/Images/MenuTop/arrow.gif","<%=Request.ApplicationPath%>/Images/MenuTop/arrow1.gif",16,16,0,"","",0,0,0,0,1,"#ffffff",1,"#ffffff",1,"<%=Request.ApplicationPath%>/Images/MenuTop/menu/xp5___.gif","<%=Request.ApplicationPath%>/Images/MenuTop/xp6___.jpg",3,3,0,0,"#cccccc","#999999","#ffffff","red","bold 11px 'Tahoma','Arial'","bold 11px 'Tahoma','Arial'",0,0]);
				stm_bp("p1",[1,4,-1,1,0,4,5,0,100,"progid:DXImageTransform.Microsoft.Strips(Motion=rightdown,enabled=0,Duration=0.60)",19,"progid:DXImageTransform.Microsoft.Strips(Motion=leftup,enabled=0,Duration=0.60)",18,50,1,1,"#999999","transparent","",3,0,0,"#000000"]);
						<% for (j=0; j < tblModule.Rows.Count ; j++) { %>
				stm_aix("p1i0","p0i0",[0,'<%=tblModule.Rows[j]["ModuleName"]%>',"","",-1,-1,0,'<%=tblModule.Rows[j]["ModuleHome"]%>?ModuleID=<%=tblModule.Rows[j]["ModuleID"]%>&amp;FunctionDefault=<%=tblModule.Rows[j]["FunctionID"]%>',"_self",'<%=tblModule.Rows[j]["ModuleName"]%>',"","","<%=Request.ApplicationPath%>/Images/MenuTop/square.gif",5,5,0,"","",0,0,0,0,1,"#ffffff",1,"#ffffff",1,"<%=Request.ApplicationPath%>/Images/MenuTop/menuitembg.gif","<%=Request.ApplicationPath%>/Images/MenuTop/xp6___.jpg",3,3,1,1,"#cccccc","#999999","white","red","bold 8pt 'Tahoma','Arial'","bold 8pt 'Tahoma','Arial'"]);
						<% } %>
				stm_ep();
				stm_em();
			</SCRIPT>
		</td>
		<%if(tbl.Rows.Count > 0)
		  {
				for (i=0; i<tbl.Rows.Count; i++)
				{
					strCssClass = tbl.Rows[i]["cssClass"].ToString().Trim();
					if (tbl.Rows[i]["FunctionID"].ToString() == strParentID)
						strCssClass += "Selected";
		%>
		<td width="97" align="center" valign="middle"><a class='<%=strCssClass%>' href='<%=tbl.Rows[i]["Url"]%>?ModuleID=<%=strModuleID%>&amp;ParentID=<%=tbl.Rows[i]["FunctionID"]%>&amp;Ascx=<%=tbl.Rows[i]["Ascx"]%>&amp;FunctionID=<%=tbl.Rows[i]["FunctionDefault"]%>'>
				<%=tbl.Rows[i]["FunctionName"]%>
			</a>
		</td>
		<%		
				}
		   }
		%>
		</TD>
	</tr>
</table>

