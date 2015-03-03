<%@ Register TagPrefix="uc1" TagName="ColumnList" Src="../Include/ColumnList.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="LevelSetup.ascx.cs" Inherits="iHRPCore.MdlHR.LevelSetup" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Import Namespace ="iHRPCore.Com"%>
<%@ Import Namespace ="System.Data" %>
<%@ Import Namespace ="System.Data.SqlClient" %>
<%
	string strLangID = Session["LangID"] == null?"EN":Session["LangID"].ToString().Trim();
	string sAccountLogin= Session["AccountLogin"]!=null?Session["AccountLogin"].ToString():"admin";
	string Level=Request.Params["tabid"];		
	int Total1 = 0;	
			
	DataTable rs1 = new DataTable();
	
	if (Level=="Level3")
	{
		rs1 = clsCommon.GetDataTable("LS_spfrmLevel3 @Activity='GetLevel'");	
	}else
	{
		rs1 = clsCommon.GetDataTable("LS_spfrmLevel2 @Activity='GetLevel'");
	}

	
	Total1 = rs1.Rows.Count;
	
%>
<script language="javascript">					
		var DS = new Array(1);
		DS[0] = new Array(<%=Total1%>);		
		<% for(int i=0; i<Total1; i++) {%>		
			DS[0][<%=i%>]="<%=rs1.Rows[i]["Code"]%>";			
		<%}%>			
</script>
<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
	<TR>
		<TD width="14%"></TD>
		<TD width="15%"><asp:label id="lblCode" runat="server" CssClass="labelRequire">Code</asp:label></TD>
		<TD width="57%"><asp:textbox id="txtCode" runat="server" CssClass="input" MaxLength="10" Width="100px"></asp:textbox></TD>
		<TD width="14%"></TD>
	</TR>
	<TR>
		<TD></TD>
		<TD><asp:label id="Label1" runat="server" CssClass="label">Name</asp:label></TD>
		<TD><asp:textbox id="txtName" runat="server" CssClass="input" MaxLength="150" Width="360px"></asp:textbox></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD></TD>
		<TD><asp:label id="Label2" runat="server" CssClass="labelRequire">VNName</asp:label></TD>
		<TD><asp:textbox id="txtVNName" runat="server" CssClass="input" MaxLength="150" Width="360px"></asp:textbox></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD></TD>
		<TD>
			<asp:label id="Label6" CssClass="labelRequire" runat="server">ShortName</asp:label></TD>
		<TD>
			<asp:textbox id="txtShortName" CssClass="input" runat="server" Width="360px" MaxLength="30"></asp:textbox></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD></TD>
		<TD>
			<asp:label id="Label5" runat="server" CssClass="labelRequire">Belong</asp:label></TD>
		<TD>
			<asp:ListBox id="lstLevelParent" runat="server" CssClass="combo" Width="360px"></asp:ListBox></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD></TD>
		<TD></TD>
		<TD>
			<asp:linkbutton id="btnDetail" accessKey="S" CssClass="Detail" runat="server" ToolTip="ALT+B"></asp:linkbutton></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD></TD>
		<TD><asp:label id="Label3" runat="server" CssClass="label">Note</asp:label></TD>
		<TD><asp:textbox id="txtNote" runat="server" CssClass="input" MaxLength="500" Width="360px"></asp:textbox></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD></TD>
		<TD>
			<asp:label id="Label7" CssClass="label" runat="server">Rank</asp:label></TD>
		<TD>
			<asp:textbox id="txtRank" CssClass="input" onblur="javascript:checkNumeric(this)" runat="server"
				Width="100px" MaxLength="10"></asp:textbox></TD>
		<TD></TD>
	</TR>
	<TR id='trLevelParent' runat="server">
		<TD></TD>
		<TD>
			<asp:label id="Label8" CssClass="label" runat="server">Level Parent</asp:label></TD>
		<TD>
			<asp:dropdownlist id="cboLevel3IDParent" CssClass="select" runat="server" Width="360px"></asp:dropdownlist></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD></TD>
		<TD><asp:label id="Label4" runat="server" CssClass="label">Used</asp:label></TD>
		<TD><asp:checkbox id="chkUsed" runat="server" CssClass="checkbox"></asp:checkbox></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD vAlign="middle" align="center" colSpan="4"><asp:linkbutton id="btnSearch" accessKey="N" runat="server" CssClass="btnSearch" ToolTip="Alt+S">Search</asp:linkbutton>&nbsp;&nbsp;&nbsp;
			<asp:linkbutton id="btnAddnew" accessKey="N" runat="server" CssClass="btnSave" ToolTip="Alt+N">Add new</asp:linkbutton>&nbsp;&nbsp;&nbsp;
			<asp:linkbutton id="btnSave" accessKey="S" runat="server" CssClass="btnSave" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp;&nbsp;&nbsp;
			<asp:linkbutton id="btnDelete" accessKey="D" runat="server" CssClass="btnDelete" ToolTip="Alt+D">Delete</asp:linkbutton>&nbsp;&nbsp;&nbsp;
			<asp:linkbutton id="btnExport" accessKey="D" runat="server" CssClass="btnExport" ToolTip="Alt+E">Export</asp:linkbutton></TD>
	</TR>
	<TR>
		<TD id="tdPages" noWrap colSpan="4"><uc1:columnlist id="uctrlColumns" runat="server"></uc1:columnlist></TD>
	</TR>
	<TR>
		<TD vAlign="middle" align="center" colSpan="4">
			<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="98%" border="0">
				<TR>
					<TD><asp:datagrid id="dtgLevel" runat="server" CssClass="grid" Width="100%" HorizontalAlign="Center"
							BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False"
							AllowSorting="True" AllowPaging="True">
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
							<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="No.">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<%# Container.ItemIndex + 1 + dtgLevel.PageSize*dtgLevel.CurrentPageIndex%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="ID" Visible="False">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Code" SortExpression="Code" HeaderText="Code">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Name" SortExpression="Name" HeaderText="Name">
									<HeaderStyle Width="40%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="VNName" SortExpression="VNName" HeaderText="Name(VN)">
									<HeaderStyle Width="40%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Used" HeaderText="Active">
									<HeaderStyle HorizontalAlign="Center" Width="40%" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Edit">
									<HeaderStyle HorizontalAlign="Center" Width="40%" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton CommandName="Edit" id="linkbutton1" runat="server" Text='Edit'></asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Select">
									<HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox id="chkSelect1" runat="server" CssClass="checkbox"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
						<INPUT id="txtParentCode" type="hidden" name="Hidden1" runat="server"> <INPUT id="txtParentName" type="hidden" name="Hidden1" runat="server">
						<INPUT id="txtLSLevel2ID" type="hidden" name="Hidden1" runat="server">
						<asp:TextBox id="txtAdd" style="DISPLAY:none" runat="server"></asp:TextBox>
					</TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>
<SCRIPT language="javascript">
function validform()
{	
	if(checkisnull('<%=str%>')==false)  return false;	
	if(checkisnull('txtVNName')==false)  return false;			
	if(checkisnull('txtShortName')==false)  return false;			
	//alert(document.getElementById('_ctl0_txtParentCode').value);
	if(checkisnull__('txtParentCode')==false)  return false;	
	
	//if(checkisnull('lstLevelParent')==false) return false;
	return true;
}
function checkisnull(obj)
{
	if(document.getElementById('_ctl0_' + obj).value=='')
	{	
		GetAlertError(iTotal,DSAlert,"0003");
		document.getElementById('_ctl0_' + obj).focus();
		return false;
	}
	else
	{
		return true;
	}
}
function checkisnull__(obj)
{
	if(document.getElementById('_ctl0_' + obj).value=='' || document.getElementById('_ctl0_' + obj).value==',')
	{	
		//alert(document.getElementById('_ctl0_' + obj).value);
		GetAlertError(iTotal,DSAlert,"0003");
		document.getElementById('_ctl0_btnDetail').focus();
		return false;
	}
	else
	{
		return true;
	}
}
function CheckDelete()
{
	if (GridCheck('_ctl0_dtgLevel', 3, 1, 'chkSelect1') == false)
	{
		GetAlertError(iTotal,DSAlert,"0001");
		return false;
	}
}
//cangtt 03012005
function OpenWindow()
{	
	//kiem tra code trùng
	var i;
	if (document.getElementById("_ctl0_txtAdd").value=='True')
	{
	for(i=0; i<<%=Total1%>;i++)
		{		
			if (DS[0][i]==document.getElementById('_ctl0_txtCode').value)
			{
				GetAlertError(iTotal,DSAlert,"0053");
				document.getElementById('_ctl0_txtCode').focus();
				return false;
			}		
		}
	}
	
	//alert('FormPage.aspx?Ascx=MdlHR/LevelSetup_popup.ascx&tabid=<%=Request.Params["tabid"].ToString().Trim()%>&LSLevelCode=' + document.getElementById('_ctl0_txtCode').value + '&strCodeList='+ document.getElementById('_ctl0_txtParentCode').value+'&strNameList='+ document.getElementById('_ctl0_txtParentName').value)
	ShowDialog('FormPage.aspx?Ascx=MdlHR/LevelSetup_popup.ascx&tabid=<%=Request.Params["tabid"].ToString().Trim()%>&LSLevelCode=' + document.getElementById('_ctl0_txtCode').value + '&strCodeList='+ document.getElementById('_ctl0_txtParentCode').value+'&strLevel=<%= Request.Params["tabid"].ToString().Trim() %>',903,387);						
	return false;	
}
function AddList(obj)
{	
	var strValue = obj.value;
	var arr_Level=strValue.split(",");	
	
	 while (document.getElementById("_ctl0_lstLevelParent").options.length!=0)       
		document.getElementById("_ctl0_lstLevelParent").options.remove(0);
		
	for(i=0;i<arr_Level.length-1;i++)
	{	
		var sLevel=arr_Level[i];
		
		var levelCode=sLevel.substring(0,sLevel.indexOf('@'));		
		var LevelName=sLevel.substring(sLevel.indexOf('@')+1,sLevel.length);
		//alert(levelCode+'--'+LevelName);
		document.getElementById("_ctl0_lstLevelParent").add(new Option(levelCode+' -- '+LevelName,levelCode));
	}
}

</SCRIPT>
