<%@ Register TagPrefix="uc1" TagName="ColumnList" Src="../Include/ColumnList.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="SYS_ucPermissionLevel.ascx.cs" Inherits="iHRPCore.SYS.SYS_ucPermissionLevel" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Import Namespace ="System.Data.SqlClient" %>
<%@ Import Namespace ="System.Data" %>
<%@ Import Namespace ="iHRPCore.Com"%>
<%
	string strLangID = Session["LangID"] == null?"EN":Session["LangID"].ToString().Trim();
	string sAccountLogin= Session["AccountLogin"]!=null?Session["AccountLogin"].ToString():"admin";
	int Total2 = 0;
	int Total3 = 0;
	int Total1 = 0;
	
	DataTable rs2 = new DataTable();		
	DataTable rs3 = new DataTable();	
	DataTable rs1 = new DataTable();
	
	rs1 = clsCommon.GetDataTable("sp_GetDataComboLevel @TableName = 'LS_tblLevel1', @Language='" + strLangID + "',@UserGroupID='" + sAccountLogin + "'");
	rs2 = clsCommon.GetDataTable("sp_GetDataComboLevel @TableName = 'LS_tblLevel2', @Language='" + strLangID + "',@UserGroupID='" + sAccountLogin + "'");
	rs3 = clsCommon.GetDataTable("sp_GetDataComboLevel @TableName = 'LS_tblLevel3', @Language='" + strLangID + "',@UserGroupID='" + sAccountLogin + "'");
	
	Total1 = rs1.Rows.Count;
	Total2 = rs2.Rows.Count;
	Total3 = rs3.Rows.Count;
%>
<script language="javascript">
		//array for level1
		var DS1 = new Array(3);
		DS1[0] = new Array(<%=Total1%>);
		DS1[1] = new Array(<%=Total1%>);
		DS1[2] = new Array(<%=Total1%>);	
		<% for(int i=0; i<Total1; i++) {%>		
			DS1[0][<%=i%>]="<%=rs1.Rows[i]["LSCompanyID"]%>";
			DS1[1][<%=i%>]="<%=rs1.Rows[i]["LSLevel1ID"]%>";
			DS1[2][<%=i%>]="<%=rs1.Rows[i]["Name"]%>";	
		<%}%>
		//array for level2
		var DS2 = new Array(3);
		DS2[0] = new Array(<%=Total2%>);
		DS2[1] = new Array(<%=Total2%>);
		DS2[2] = new Array(<%=Total2%>);	
		<% for(int i=0; i<Total2; i++) {%>		
			DS2[0][<%=i%>]="<%=rs2.Rows[i]["LSLevel1ID"]%>";
			DS2[1][<%=i%>]="<%=rs2.Rows[i]["LSLevel2ID"]%>";
			DS2[2][<%=i%>]="<%=rs2.Rows[i]["Name"]%>";	
		<%}%>		
		//array for level3
		var DS3 = new Array(3);
		DS3[0] = new Array(<%=Total3%>);
		DS3[1] = new Array(<%=Total3%>);
		DS3[2] = new Array(<%=Total3%>);
		<% for(int i=0; i<Total3; i++) {%>		
			DS3[0][<%=i%>]="<%=rs3.Rows[i]["LSLevel2ID"]%>";
			DS3[1][<%=i%>]="<%=rs3.Rows[i]["LSLevel3ID"]%>";
			DS3[2][<%=i%>]="<%=rs3.Rows[i]["Name"]%>";	
		<%}%>	
</script>
<script language="javascript">
	function ChangeCompany()
	{		
		var all = document.getElementById("_ctl0_cboLSLevel2ID").length;
		var all1 = document.getElementById("_ctl0_cboLSLevel3ID").length;
		var all2 = document.getElementById("_ctl0_cboLSLevel1ID").length;
		
		for(i=0; i<all2; i++)
		{	
			document.getElementById("_ctl0_cboLSLevel1ID").remove(0);			
		};
		
		for(i=0; i<all; i++)
		{	
			document.getElementById("_ctl0_cboLSLevel2ID").remove(0);			
		};
		
		for(i=0; i<all1; i++)
		{	
			document.getElementById("_ctl0_cboLSLevel3ID").remove(0);			
		};
		var strCompany = document.getElementById("_ctl0_cboLSCompanyID").value; 
		document.getElementById("_ctl0_cboLSLevel1ID").add(new Option('',''));
		for(i=0; i<<%=Total1%>;i++)
		{
			if (DS1[0][i]==strCompany)
			{
				document.getElementById("_ctl0_cboLSLevel1ID").add(new Option(DS1[2][i],DS1[1][i]));
			};
		};		
		all = document.getElementById("_ctl0_cboLSLevel1ID").length;
		document.getElementById("_ctl0_cboLSLevel1ID").selectedIndex = 0;
		
		ChangeLevel1();			
	}
	
	function ChangeLevel1()
	{
		var all = document.getElementById("_ctl0_cboLSLevel2ID").length;
		var all1 = document.getElementById("_ctl0_cboLSLevel3ID").length;
		
		for(i=0; i<all; i++)
		{	
			document.getElementById("_ctl0_cboLSLevel2ID").remove(0);			
		};
		for(i=0; i<all1; i++)
		{	
			document.getElementById("_ctl0_cboLSLevel3ID").remove(0);			
		};
		var strLevel1ID = document.getElementById("_ctl0_cboLSLevel1ID").value; 
		document.getElementById("_ctl0_cboLSLevel2ID").add(new Option('',''));
		
		for(i=0; i<<%=Total2%>;i++)
		{
			if (DS2[0][i]==strLevel1ID)
			{
				document.getElementById("_ctl0_cboLSLevel2ID").add(new Option(DS2[2][i],DS2[1][i]));
			};
		};		
		all = document.getElementById("_ctl0_cboLSLevel2ID").length;
		document.getElementById("_ctl0_cboLSLevel2ID").selectedIndex = 0;
		document.getElementById("_ctl0_txtLevel1ID").value = strLevel1ID;
		ChangeLevel2();			
	}
	
	function ChangeLevel2()
	{
		var all = document.getElementById("_ctl0_cboLSLevel3ID").length;
		
		for(i=0; i<all; i++)
		{	
			document.getElementById("_ctl0_cboLSLevel3ID").remove(0);			
		};
		var strLevel2ID = document.getElementById("_ctl0_cboLSLevel2ID").value; 
		document.getElementById("_ctl0_cboLSLevel3ID").add(new Option('',''));
		
		for(i=0; i<<%=Total3%>;i++)
		{
			if (DS3[0][i]==strLevel2ID)
			{
				document.getElementById("_ctl0_cboLSLevel3ID").add(new Option(DS3[2][i],DS3[1][i]));
			};
		};		
		document.getElementById("_ctl0_txtLevel2ID").value = document.getElementById("_ctl0_cboLSLevel2ID").value;
		ChangeLevel3();	
	}
	function ChangeLevel3()
	{	
		document.getElementById("_ctl0_txtLevel3ID").value = document.getElementById("_ctl0_cboLSLevel3ID").value;
	}
</script>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD align="center">
			<TABLE id="Table2" cellSpacing="0" cellPadding="1" width="100%" border="0">
				<TBODY>
					<TR>
						<TD colSpan="3"><asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></TD>
					</TR>
					<TR>
						<TD><asp:label id="Label1" CssClass="labelRequire" Runat="server">User</asp:label></TD>
						<TD></TD>
						<TD colSpan="4"><asp:textbox id="txtUserGroupID" runat="server" CssClass="input" ReadOnly="True" Width="208px"></asp:textbox><asp:button class="search" id="cmdGetUserAccount" runat="server" Font-Names="Arial" Font-Bold="True"
								Font-Size="8pt" Text="..."></asp:button>&nbsp;
							<asp:label id="lblUserName" runat="server" CssClass="label"></asp:label></TD>
					</TR>
					<TR>
						<TD width="10%"><asp:label id="Label2" CssClass="labelRequire" Runat="server">Cus.Group</asp:label></TD>
						<TD width="2%"></TD>
						<TD width="40%"><asp:dropdownlist id="cboLSCompanyID" runat="server" CssClass="combo" Width="100%" onchange="ChangeCompany()"></asp:dropdownlist></TD>
						<TD align="right" width="10%"><asp:label id="Label3" CssClass="label" Runat="server">Company</asp:label></TD>
						<TD></TD>
						<TD width="30%"><asp:dropdownlist id="cboLSLevel1ID" runat="server" CssClass="combo" Width="100%" onchange="ChangeLevel1()"></asp:dropdownlist></TD>
					</TR>
					<TR>
						<TD><asp:label id="Label4" CssClass="label" Runat="server">Department</asp:label></TD>
						<TD></TD>
						<TD colSpan="4"><asp:dropdownlist id="cboLSLevel2ID" runat="server" CssClass="combo" Width="100%" onchange="ChangeLevel2()"></asp:dropdownlist></TD>
					</TR>
					<TR>
						<TD><asp:label id="Label5" CssClass="label" Runat="server">Group</asp:label></TD>
						<TD></TD>
						<TD colSpan="4"><asp:dropdownlist id="cboLSLevel3ID" runat="server" CssClass="combo" Width="100%" onchange="ChangeLevel3()"></asp:dropdownlist></TD>
					</TR>
					<TR style="DISPLAY: none">
						<TD><asp:label id="Label6" CssClass="label" Runat="server">Right</asp:label></TD>
						<td></td>
						<td colSpan="4"><asp:checkbox id="chkFTK" runat="server" CssClass="checkbox" Text="Time Keeper" Checked="True"></asp:checkbox>&nbsp;<asp:checkbox id="chkFApp" runat="server" CssClass="checkbox" Text="Approve" Checked="True"></asp:checkbox>&nbsp;
							<asp:checkbox id="chkFChk" runat="server" CssClass="checkbox" Text="Checker" Checked="True"></asp:checkbox>&nbsp;
							<asp:checkbox id="chkFHR" runat="server" CssClass="checkbox" Text="HR" Checked="True"></asp:checkbox></td>
		</TD>
	</TR>
	<TR>
		<TD></TD>
		<TD></TD>
		<TD><INPUT id="txtLevel3ID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtLevel3ID"
				runat="server"><INPUT id="txtLevel2ID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtLevel2ID"
				runat="server"><INPUT id="txtLevel1ID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtLevel1ID"
				runat="server"><INPUT id="txtPermissionLevelID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1"
				name="txtLevel1ID" runat="server"></TD>
	</TR>
</TABLE></TD></TR>
<TR>
	<TD align="center">
		<hr width="100%">
	</TD>
</TR>
<TR>
	<TD align="center"><asp:linkbutton id="btnAddNew" accessKey="N" runat="server" CssClass="btnAddnew" ToolTip="Alt+N">Add New</asp:linkbutton>&nbsp;
		<asp:linkbutton id="btnSearch" accessKey="D" runat="server" CssClass="btnExport" ToolTip="Alt + D">Filter</asp:linkbutton>&nbsp;
		<asp:linkbutton id="btnSave" accessKey="S" runat="server" CssClass="btnSave" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp;&nbsp;
		<asp:linkbutton id="btnExport" accessKey="E" runat="server" CssClass="btnExport" ToolTip="Alt+E, Xu?t dl trên lu?i ra file Excel"
			Visible="false"> Export</asp:linkbutton></TD>
</TR>
<tr align="center">
	<td>
		<table>
			<TR>
				<TD align="left"><asp:label id="Label7" runat="server" CssClass="labelrequire">User list</asp:label></TD>
				<TD align="center"></TD>
				<TD align="left"><asp:label id="Label8" runat="server" CssClass="labelrequire">Users Assigned</asp:label></TD>
			</TR>
			<tr align="center">
				<td align="center"><asp:listbox id="lstListUser" runat="server" CssClass="combo" Width="232px" Font-Names="Arial"
						Font-Size="8pt" SelectionMode="Multiple" Height="145px"></asp:listbox></td>
				<td align="center">
					<P><BUTTON style="WIDTH: 25px" onclick="addAttribute()" type="button">&gt;</BUTTON><button style="WIDTH: 30px" onclick="addAll()" type="button">&gt;&gt;</button></P>
					<P><BUTTON style="WIDTH: 30px" onclick="delAll()" type="button">&lt;&lt;</BUTTON><BUTTON style="WIDTH: 25px" onclick="delAttribute()" type="button">&lt;</BUTTON></P>
				</td>
				<TD align="center"><asp:listbox id="lstListGroup" runat="server" CssClass="combo" Width="232px" Font-Names="Arial"
						Font-Size="8pt" SelectionMode="Multiple" Height="145px"></asp:listbox></TD>
			</tr>
		</table>
	</td>
</tr>
<TR style="DISPLAY: none">
	<TD align="left" height="21"><asp:textbox id="txtLstListGroup" Runat="server"></asp:textbox><asp:textbox id="txtListLen" Runat="server"></asp:textbox><uc1:columnlist id="uctrlColumns" runat="server"></uc1:columnlist></TD>
</TR>
<TR style="DISPLAY: none">
	<TD><asp:datagrid id="dtgList" runat="server" CssClass="grid" Width="100%" PageSize="7" AllowSorting="True"
			AutoGenerateColumns="False" CellPadding="0" BorderWidth="1px" BorderColor="#3366CC" AllowPaging="True"
			BackColor="White">
			<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
			<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
			<ItemStyle CssClass="gridItem"></ItemStyle>
			<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
			<Columns>
				<asp:BoundColumn Visible="False" DataField="PermissionLevelID" HeaderText="PermissionLevelID">
					<HeaderStyle Width="20%"></HeaderStyle>
					<ItemStyle HorizontalAlign="Center"></ItemStyle>
				</asp:BoundColumn>
				<asp:TemplateColumn HeaderText="Seq">
					<HeaderStyle Width="5%"></HeaderStyle>
					<ItemStyle HorizontalAlign="Center"></ItemStyle>
					<ItemTemplate>
						<%# Container.ItemIndex + 1 + dtgList.PageSize*dtgList.CurrentPageIndex%>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Account">
					<HeaderStyle Width="12%"></HeaderStyle>
					<ItemStyle HorizontalAlign="Center"></ItemStyle>
					<ItemTemplate>
						<asp:LinkButton id=hpLink CssClass="Hlink" Width="95%" Text='<%# DataBinder.Eval(Container, "DataItem.UserGroupID") %>' Runat="server" CommandName="Edit">
						</asp:LinkButton>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:BoundColumn DataField="Company" HeaderText="Company">
					<HeaderStyle Width="20%"></HeaderStyle>
					<ItemStyle HorizontalAlign="Center"></ItemStyle>
				</asp:BoundColumn>
				<asp:BoundColumn DataField="Division" HeaderText="Department">
					<HeaderStyle Width="20%"></HeaderStyle>
					<ItemStyle HorizontalAlign="Center"></ItemStyle>
				</asp:BoundColumn>
				<asp:BoundColumn DataField="Department" HeaderText="Section">
					<HeaderStyle Width="20%"></HeaderStyle>
					<ItemStyle HorizontalAlign="Center"></ItemStyle>
				</asp:BoundColumn>
				<asp:BoundColumn Visible="False" DataField="Section" HeaderText="Section">
					<HeaderStyle Width="20%"></HeaderStyle>
					<ItemStyle HorizontalAlign="Center"></ItemStyle>
				</asp:BoundColumn>
				<asp:BoundColumn Visible="False" DataField="Right" HeaderText="Right">
					<HeaderStyle Width="20%"></HeaderStyle>
					<ItemStyle HorizontalAlign="Center"></ItemStyle>
				</asp:BoundColumn>
				<asp:TemplateColumn HeaderText="Select">
					<HeaderStyle Width="5%"></HeaderStyle>
					<ItemStyle HorizontalAlign="Center"></ItemStyle>
					<HeaderTemplate>
						<asp:CheckBox id="chkSelectAll" onclick="CheckAll('_ctl0_dtgList__ctl2_chkSelectAll','_ctl0_dtgList',3,1,'chkSelect')" 
 runat="server" CssClass="gridFooter"></asp:CheckBox>
					</HeaderTemplate>
					<ItemTemplate>
						<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
					</ItemTemplate>
				</asp:TemplateColumn>
			</Columns>
			<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
		</asp:datagrid><asp:textbox id="txtPageLoad" runat="server" Visible="False"></asp:textbox><asp:textbox id="txtHidenInfo" runat="server" Visible="False"></asp:textbox></TD>
</TR></TBODY></TABLE>
<script>
function ReturnEmpPopUp(strUserID, strUserName, strFGroup)
{
	document.getElementById("_ctl0_txtUserGroupID").value=strUserID;		
	document.getElementById("_ctl0_lblUserName").innerText=strUserName;
	//document.getElementById("_ctl0_txtHidenInfo").value = strUserName;
}
function checkdelete()
{
	/*if(GridCheck('_ctl0_dtgList',3,1,'chkSelect')==false)
	{
		GetAlertError(iTotal,DSAlert,"0001");		
		return false;
	}
	if(confirm(GetAlertText(iTotal,DSAlert,"0002"))==false){
		return false;
	}*/
	if(document.getElementById('_ctl0_txtUserGroupID').value=="")
	{
		GetAlertError(iTotal,DSAlert,"P0001");		
		document.getElementById('_ctl0_txtUserGroupID').focus();
		return false;
	}
	//if(checkisnull('txtUserGroupID')==false)  return false;
	if(checkisnull('cboLSCompanyID')==false)  return false;
	return true;
	//document.getElementById("_ctl0_lblUserName").innerText = <%=Session["AccountInfo"]%>;
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
	function validform()
	{	
		if(document.getElementById('_ctl0_txtUserGroupID').value=="")
		{
			GetAlertError(iTotal,DSAlert,"P0001");		
			document.getElementById('_ctl0_txtUserGroupID').focus();
			return false;
		}
		//if(checkisnull('txtUserGroupID')==false)  return false;
		if(checkisnull('cboLSCompanyID')==false)  return false;
		
		var all=document.getElementById('_ctl0_lstListGroup').length;
		var sValue='';
		for(i=0; i<all; i++)
			{	
				sValue+=document.getElementById('_ctl0_lstListGroup').item(i).value+',';
			};	
		document.getElementById('_ctl0_txtLstListGroup').value=sValue
		//if(checkisnull('txtLstListGroup')==false)  return false;
		

		/*if (document.getElementById('_ctl0_chkFTK').checked + '@' + document.getElementById('_ctl0_chkFApp').checked + '@' +document.getElementById('_ctl0_chkFChk').checked + '@' + document.getElementById('_ctl0_chkFHR').checked =='false@false@false@false')
		{			
			GetAlertError(iTotal,DSAlert,"PL_0001");
			return false;
		}*/
	//document.getElementById("_ctl0_lblUserName").innerText = Session["AccountInfo"];
		return true;
	}
</script>
<SCRIPT language="javascript">
createListObjects('_ctl0_lstListGroup','_ctl0_lstListUser')
</SCRIPT>

