<%@ Control Language="c#" AutoEventWireup="false" Codebehind="LeftMenu.ascx.cs" Inherits="iHRPCore.Pagelets.LeftMenu" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Import Namespace ="System.Data" %>
<table border="0" width="100%" cellpadding="0" cellspacing="0">
	<tr style="Display:none">
		<td>
			<INPUT id="txtCurMenu" style="WIDTH: 16px; HEIGHT: 22px" type="hidden" size="1" name="txtCurMenu"
					runat="server">
			<script language=javascript>
				document.getElementById("LeftMenu1_txtCurMenu").value = document.getElementById("txtCurMenu").value; 
			</script>
		</td>
	</tr>
	<%if(tbl != null && tbl.Rows.Count > 0){%>
	<%
	DataRow[] ArrayLevel1 = tbl.Select("MenuLevel = 1");
	DataRow[] ArrayLevel3 = tbl.Select("MenuLevel = 2");
	
	DataRow[] ArrayLevel2 = new DataRow[0];
	
	int iLength;
	if (ArrayLevel1.Length != 0)
	{
		iLength = ArrayLevel1.Length;
		ArrayLevel1 = ArrayLevel1;
	}
	else
	{
		iLength = ArrayLevel3.Length;
		ArrayLevel1 = ArrayLevel3;
	}

	for (int i = 0; i<iLength; i++)
	{
		
		strCssClass = ArrayLevel1[i]["cssClass"].ToString().Trim();
		if (ArrayLevel1[i]["FunctionID"].ToString() == strFunctionID)
			strCssClass += "Selected";
							
		ArrayLevel2 = tbl.Select("LevelParent = '" + ArrayLevel1[i]["FunctionID"].ToString().Trim() + "'");
	%>
	<tr>
		<td height="20" width="4%">
		</td>
		<td align="left" width="96%" background="IMAGES\leftmenu.jpg" width=146 height=23 style="padding-left: 17px ">
			<% if(ArrayLevel1[i]["Url"]!=System.DBNull.Value){ %>
			<!--Neu khong co cap 2-->
			<% if(ArrayLevel2.Length == 0){ %>
				<a class='<%=strCssClass%>' href='<%=ArrayLevel1[i]["Url"]%>?ModuleID=<%=strModuleID%>&amp;ParentID=<%=strParentID%>&amp;FunctionID=<%=ArrayLevel1[i]["FunctionID"]%>&amp;Ascx=<%=ArrayLevel1[i]["Ascx"]%>'>
					
					<%=ArrayLevel1[i]["FunctionName"]%>
				</a>
			<%} else {%>
				<a class='<%=strCssClass%>' onclick="return ChangeStatus('tbl<%=ArrayLevel1[i]["FunctionID"]%>')" href='<%=ArrayLevel1[i]["Url"]%>?ModuleID=<%=strModuleID%>&amp;ParentID=<%=strParentID%>&amp;FunctionID=<%=ArrayLevel1[i]["FunctionID"]%>&amp;Ascx=<%=ArrayLevel1[i]["Ascx"]%>'>
					
					<%=ArrayLevel1[i]["FunctionName"]%>
				</a>
			<%}%>
			<!---->
			<% } else { %>
			<!--Neu khong co cap 2-->
			<% if(ArrayLevel2.Length == 0){ %>
				<a class='<%=strCssClass%>'>
					
					<%=ArrayLevel1[i]["FunctionName"]%>
				</a>
			<%} else{%>
				<a class='<%=strCssClass%>' onclick="return ChangeStatus('tbl<%=ArrayLevel1[i]["FunctionID"]%>')">
					
					<%=ArrayLevel1[i]["FunctionName"]%>
				</a>
			<%}%>
			<%}%>
		</td>
	</tr>
	<!---->
	<%if ( ArrayLevel2.Length > 0) {%>
	<tr>
		<td colspan=2>
			<table id="tbl<%=ArrayLevel1[i]["FunctionID"]%>" style="display:none" border="0" width="100%" cellpadding="0" cellspacing="0">
				<%for (int j=0; j<ArrayLevel2.Length; j++)
				{
					strCssClass = ArrayLevel2[i]["cssClass"].ToString().Trim();
					if (ArrayLevel2[j]["FunctionID"].ToString() == strFunctionID)
						strCssClass += "Selected";
				
				%>
					<tr>
						<td height="20" width="10%">
						</td>
						<td align="left" width="*" background="IMAGES\leftmenu.jpg" width=146 height=23 style="padding-left: 17px ">
							<% if(ArrayLevel2[j]["Url"]!=System.DBNull.Value){ %>
							<a class='<%=strCssClass%>' href='<%=ArrayLevel2[j]["Url"]%>?ModuleID=<%=strModuleID%>&amp;ParentID=<%=strParentID%>&amp;FunctionID=<%=ArrayLevel2[j]["FunctionID"]%>&amp;Ascx=<%=ArrayLevel2[j]["Ascx"]%>'>
								
								<%=ArrayLevel2[j]["FunctionName"]%>
							</a>
							<% } else { %>
							<a class='<%=strCssClass%>'>
								
								<%=ArrayLevel2[j]["FunctionName"]%>
							</a>
							<% } %>
						</td>
					</tr>
				<%}%>
			</table>
			<!--Display phan tu dang duoc chon-->
			<%for (int j=0; j<ArrayLevel2.Length; j++)
			{
			%>
			<script language=javascript>
				if (document.getElementById("LeftMenu1_txtCurMenu").value == <%=ArrayLevel2[j]["FunctionID"]%>)
				{
					document.getElementById('tbl<%=ArrayLevel1[i]["FunctionID"]%>').style.display = "";
				}
			</script>
			<%}%>
		</td>
	</tr>
	<%}%>
	<!---->
	<%}}%>
</table>
<script language=javascript>
	function ChangeStatus(field)
	{
		collapse(field);
	}
</script>