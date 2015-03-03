<%@ Register TagPrefix="uc1" TagName="IndividualHeader" Src="../Include/IndividualHeader.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="GrantAuthority.ascx.cs" Inherits="MdlSYS.GrantAuthority" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Import Namespace ="System.Data.SqlClient" %>
<%@ Import Namespace ="System.Data" %>
<%@ Import Namespace ="iHRPCore.Com"%>
<%@ Register TagPrefix="uc1" TagName="ColumnList" Src="../Include/ColumnList.ascx" %>
<%
	string strLangID = Session["LangID"] == null?"EN":Session["LangID"].ToString().Trim();
	int Total1 = 0;
	int Total2 = 0;	
			
	DataTable rs1 = new DataTable();		
	
	rs1 = clsCommon.GetDataTable("HR_clsCommon @Activity='getCboEmpHeader_UMS'");	
	
	Total1 = rs1.Rows.Count;
	
%>
<SCRIPT language="javascript">							
		var DS1 = new Array(3);
		DS1[0] = new Array(<%=Total1%>);
		DS1[1] = new Array(<%=Total1%>);
		DS1[2] = new Array(<%=Total1%>);
		
		
		<% for(int i=0; i<Total1; i++) {%>					
			DS1[0][<%=i%>]="<%=rs1.Rows[i]["ID"]%>";			
			DS1[1][<%=i%>]="<%=rs1.Rows[i]["EmpName"]%>";
			DS1[2][<%=i%>]="<%=rs1.Rows[i]["EmpCode"]%>";
		<%}%>						
		
</SCRIPT>
<script language="javascript">		
function ChangeEmp()
	{	
		var strEmpID=document.getElementById("_ctl0_cboEmpID").value;
		for(i=0; i<<%=Total1%>;i++)
		{
			//alert(strRankID);
			if (DS1[0][i]==strEmpID)
			{				
				document.getElementById("_ctl0_txtUserGroupName").value = DS1[1][i];
				
				if (document.getElementById("_ctl0_txtUserGroupID").title=="")
				{
					document.getElementById("_ctl0_txtUserGroupID").value = DS1[2][i];
				}
			};
		};
		
	}	
</script>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD>
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD align="center" width="15%" colSpan="5">
						<asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></TD>
				</TR>
				<TR>
					<TD width="15%" colSpan="5">
						<uc1:IndividualHeader id="IndividualHeader1" runat="server"></uc1:IndividualHeader></TD>
				</TR>
				<TR>
					<TD width="15%"><asp:label id="Label5" CssClass="labelRequire" runat="server"> Function</asp:label></TD>
					<TD width="36%" colSpan="4">
						<asp:dropdownlist id="cboAuthorityTypeID" runat="server" CssClass="combo" Width="95%"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD width="15%"><asp:label id="Label1" CssClass="labelrequire" runat="server">From date</asp:label></TD>
					<TD width="36%"><asp:textbox id="txtFromDate" onblur="javascript:CheckDate(this);" CssClass="input" runat="server"
							Width="70%"></asp:textbox><INPUT class=btnCal id=Button12 onclick="javascript:popUpCalendar(<%= this.txtFromDate.ClientID %>)" type=button name=btnDemandDate></TD>
					<TD width="2%"></TD>
					<TD width="15%"><asp:label id="Label2" CssClass="labelRequire" runat="server">To date</asp:label></TD>
					<TD><asp:textbox id="txtToDate" onblur="javascript:CheckDate(this);" CssClass="input" runat="server"
							Width="70%"></asp:textbox><INPUT class=btnCal id=Button13 onclick="javascript:popUpCalendar(<%= this.txtToDate.ClientID %>)" type=button name=btnDemandDate></TD>
				</TR>
				<TR>
					<TD width="15%"><asp:label id="lblEmpID" CssClass="labelRequire" runat="server">Assign To</asp:label></TD>
					<TD width="36%"><asp:dropdownlist id="cboAssignTo" CssClass="combo" runat="server" Width="95%"></asp:dropdownlist></TD>
					<TD width="2%"><INPUT class="search" id="btnSearch_Replace" onclick="javascript:OpenWindowEmp_Replace('EmpID')"
							type="button" value="..." name="btnSearchByID" runat="server"></TD>
					<TD width="15%"></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD>
						<asp:label id="Label7" runat="server" CssClass="label">Description</asp:label></TD>
					<TD colSpan="4">
						<asp:textbox id="txtDescription" runat="server" CssClass="input" Width="100%" TextMode="MultiLine"
							Height="80px"></asp:textbox></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD>
			<HR width="100%">
		</TD>
	</TR>
	<TR>
		<TD style="HEIGHT: 18px" align="center">
			&nbsp;&nbsp;&nbsp;
			<asp:linkbutton id="btnAddnew" accessKey="N" CssClass="btnAddnew" runat="server" ToolTip="Alt+N"> Cancel</asp:linkbutton>&nbsp; 
			&nbsp;
			<asp:linkbutton id="btnSave" accessKey="S" CssClass="btnSave" runat="server" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp; 
			&nbsp;
			<asp:linkbutton id="btnDelete" accessKey="D" CssClass="btnDelete" runat="server" ToolTip="Alt + D">Delete</asp:linkbutton>&nbsp;&nbsp;&nbsp;
			<asp:linkbutton id="btnExport" accessKey="D" runat="server" CssClass="btnDelete" ToolTip="Alt + D">Export</asp:linkbutton></TD>
	</TR>
	<TR>
		<TD align="left" height="10"><uc1:columnlist id="uctrlColumns" runat="server"></uc1:columnlist></TD>
	</TR>
	<TR>
		<TD align="left" height="10"><asp:CheckBox Text="Select all" id="chkSelectAll" onclick="CheckAll('_ctl0_chkSelectAll','_ctl0_dtgList',3,1,'chkSelect')"
				runat="server" CssClass="checkbox"></asp:CheckBox></TD>
	</TR>
	<TR>
		<TD><asp:datagrid id="dtgList" CssClass="grid" runat="server" Width="100%" BackColor="White" AllowPaging="True"
				BorderColor="#3366CC" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False" AllowSorting="True"
				PageSize="7">
				<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
				<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
				<ItemStyle CssClass="gridItem"></ItemStyle>
				<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
				<Columns>
					<asp:BoundColumn Visible="False" DataField="AuthorityID" HeaderText="AuthorityID"></asp:BoundColumn>
					<asp:TemplateColumn HeaderText="Select">
						<HeaderStyle Width="5%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn Visible="False" HeaderText="No.">
						<HeaderStyle Width="5%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<%# Container.ItemIndex + 1 + dtgList.PageSize*dtgList.CurrentPageIndex%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:ButtonColumn DataTextField="Fromdate" SortExpression="Fromdate" HeaderText="From date" CommandName="Edit">
						<HeaderStyle Width="12%"></HeaderStyle>
					</asp:ButtonColumn>
					<asp:BoundColumn DataField="ToDate" SortExpression="ToDate" HeaderText="To date">
						<HeaderStyle Width="20%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="AuthorityTypeID" SortExpression="AuthorityTypeID" HeaderText="AuthorityTypeID"></asp:BoundColumn>
					<asp:BoundColumn DataField="AssignTo" SortExpression="AssignTo" HeaderText="Assign To"></asp:BoundColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
			</asp:datagrid></TD>
	</TR>
	<TR style="DISPLAY: none">
		<TD><asp:textbox id="txtPageLoad" Runat="server"></asp:textbox>
			<asp:TextBox ID="txtAuthorityID" Runat="server"></asp:TextBox>
			<asp:TextBox id="txtEmpID" Runat="server"></asp:TextBox>
		</TD>
	</TR>
</TABLE>
<script language="javascript">
function checkSave()
{	
		if( checkisnull('cboAuthorityTypeID')==false) return false;
		if( checkisnull('cboAssignTo')==false) return false;
		if (checkisnull('txtFromDate')==false) return false;		
		if (checkisnull('txtToDate')==false) return false;		
		if (FromSmallToDate(document.getElementById("_ctl0_txtFromDate"),document.getElementById("_ctl0_txtToDate")) == false)
		{
			//alert("From date must less than to date");
			GetAlertError(iTotal,DSAlert,"0007");
			document.getElementById("_ctl0_txtFromDate").focus();
			return false;
		}		
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
function checkdelete()
{
	if(GridCheck('_ctl0_dtgList',3,1,'chkSelect')==false)
	{
		GetAlertError(iTotal,DSAlert,"0001");		
		return false;
	}
	if(confirm(GetAlertText(iTotal,DSAlert,"0002"))==false){
		return false;
	}
}
//Search Account
function OpenWindowEmp_Replace()
	{
		ShowDialog('FormPage.aspx?Ascx=MdlHR/EmpPopup.ascx&Params=4&GetAll=1');
	}	
function ReturnEmpPopUp4(strEmpID, strEmpCode, strEmpName, strStartDate)
	{
		document.getElementById("_ctl0_cboAssignTo").value = strEmpID;		
	}
</script>
