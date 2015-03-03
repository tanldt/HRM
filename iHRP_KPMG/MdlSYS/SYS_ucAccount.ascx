<%@ Control Language="c#" AutoEventWireup="false" Codebehind="SYS_ucAccount.ascx.cs" Inherits="iHRPCore.SYS_ucAccount" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="ColumnList" Src="../Include/ColumnList.ascx" %>
<%@ Import Namespace ="iHRPCore.Com"%>
<%@ Import Namespace ="System.Data" %>
<%@ Import Namespace ="System.Data.SqlClient" %>
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
					<TD width="15%"><asp:label id="lblEmpID" CssClass="labelRequire" runat="server">EmpID</asp:label></TD>
					<TD width="44%"><asp:dropdownlist id="cboEmpID" CssClass="combo" runat="server" Width="95%" onchange="Javascript:ChangeEmp();"></asp:dropdownlist></TD>
					<TD width="2%" style="DISPLAY:none"><INPUT class="search" id="btnSearch_Replace" onclick="javascript:OpenWindowEmp_Replace('EmpID')"
							type="button" value="..." name="btnSearchByID" runat="server"></TD>
					<TD width="18%" style="DISPLAY:none"></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD><asp:label id="Label1" CssClass="label" runat="server">Account Name</asp:label></TD>
					<TD><asp:textbox id="txtUserGroupName" CssClass="input" runat="server" Width="95%" ReadOnly="True"></asp:textbox></TD>
					<TD></TD>
					<TD><asp:label id="Label2" CssClass="labelRequire" runat="server">Account</asp:label></TD>
					<TD><asp:textbox id="txtUserGroupID" CssClass="input" runat="server"></asp:textbox></TD>
				</TR>
				<TR id='trPassAuto' runat="server" style="DISPLAY:none">
					<TD></TD>
					<TD>
						<asp:CheckBox id="ChkPassAuto" runat="server" Text="Auto generate password?" onclick="javascript:AutoGenPass(this)"></asp:CheckBox></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
				</TR>
				<TR id="trPass" runat="server">
					<TD>
						<asp:label id="Label3" runat="server" CssClass="labelRequire">Password</asp:label></TD>
					<TD>
						<asp:textbox id="txtPassword" runat="server" CssClass="input" Width="95%" TextMode="Password"></asp:textbox></TD>
					<TD></TD>
					<TD>
						<asp:label id="Label4" runat="server" CssClass="labelRequire">Confirm password</asp:label></TD>
					<TD><asp:textbox id="txtConfirmPass" CssClass="input" runat="server" TextMode="Password"></asp:textbox></TD>
				</TR>
				<TR>
					<TD><asp:label id="Label5" CssClass="label" runat="server">Copy permission from</asp:label></TD>
					<TD><asp:dropdownlist id="cboCopyPer" CssClass="combo" runat="server" Width="95%"></asp:dropdownlist></TD>
					<td></td>
					<td><asp:label id="Label6" CssClass="label" runat="server">(List of account)</asp:label></td>
					<td></td>
				</TR>
				<TR>
					<TD>
						<asp:label id="Label7" runat="server" CssClass="labelRequire">Customer group</asp:label></TD>
					<TD>
						<asp:dropdownlist id="cboLSCompanyID" runat="server" CssClass="combo" Width="95%"></asp:dropdownlist></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
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
		<TD style="HEIGHT: 18px" align="center"><asp:linkbutton id="btnSearch" accessKey="N" CssClass="btnSearch" runat="server" ToolTip="Alt+N"> Search</asp:linkbutton>&nbsp; 
			&nbsp;
			<asp:linkbutton id="btnAddnew" accessKey="N" CssClass="btnAddnew" runat="server" ToolTip="Alt+N"> Cancel</asp:linkbutton>&nbsp; 
			&nbsp;
			<asp:linkbutton id="btnSave" accessKey="S" CssClass="btnSave" runat="server" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp; 
			&nbsp;
			<asp:linkbutton id="btnDelete" accessKey="D" CssClass="btnDelete" runat="server" ToolTip="Alt + D">Delete</asp:linkbutton>&nbsp; 
			&nbsp;
			<asp:linkbutton id="btnResetPass" accessKey="E" CssClass="btnAddnew" runat="server" Visible="False"> Reset PWD</asp:linkbutton></TD>
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
					<asp:BoundColumn Visible="False" DataField="UserGroupID" HeaderText="EmpID">
						<HeaderStyle Width="20%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="FAdm" HeaderText="EmpID">
						<HeaderStyle Width="20%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="FValid" HeaderText="EmpID">
						<HeaderStyle Width="20%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="EmpID" HeaderText="EmpID">
						<HeaderStyle Width="20%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn HeaderText="Select">
						<HeaderStyle Width="5%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn Visible="False" HeaderText="Seq">
						<HeaderStyle Width="5%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<%# Container.ItemIndex + 1 + dtgList.PageSize*dtgList.CurrentPageIndex%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:ButtonColumn DataTextField="UserGroupID" SortExpression="UserGroupID" HeaderText="Account" CommandName="Edit">
						<HeaderStyle Width="12%"></HeaderStyle>
					</asp:ButtonColumn>
					<asp:BoundColumn DataField="UserGroupName" SortExpression="UserGroupName" HeaderText="Account Name">
						<HeaderStyle Width="20%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn HeaderText="Admin" Visible="False">
						<HeaderStyle Width="5%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:CheckBox id="chkAdm" onclick="javascript:AdminChange(this);" runat="server" CssClass="checkbox"></asp:CheckBox>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Browser">
						<HeaderStyle Width="5%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:CheckBox id="chkValid" runat="server" CssClass="checkbox"></asp:CheckBox>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
			</asp:datagrid></TD>
	</TR>
	<TR style="DISPLAY: none">
		<TD><asp:textbox id="txtPageLoad" Runat="server"></asp:textbox>
			<asp:TextBox ID="txtUserAccountID" Runat="server"></asp:TextBox>
			<asp:TextBox ID="txtFlag" Runat="server"></asp:TextBox>
		</TD>
	</TR>
</TABLE>
<script language="javascript">
function checkSave()
{
	var i;
	var count;
	var NoItemCheck;
					
	count = document.getElementById('_ctl0_dtgList').rows.length;		
	NoItemCheck = true;		
	if (count >1 )
	{
		for (i = 3; i <document.getElementById('_ctl0_dtgList').rows.length + 1 ; i++)
		{	if (document.getElementById("_ctl0_dtgList__ctl" + i + "_chkSelect").checked == true)
			{	
				NoItemCheck = false;
				break;
			}
		}				
	}
	
	if (NoItemCheck)
	{
		document.getElementById("_ctl0_txtFlag").value='1';	
		//alert('NoItemCheck');
		if( checkisnull('cboEmpID')==false) return false;
		if (checkisnull('txtUserGroupID')==false) return false;	
		if (checkisnull('txtPassword')==false) return false;	
		if (checkisnull('txtConfirmPass')==false) return false;	
		
		var value = document.getElementById("_ctl0_txtPassword").value;
		var valueConfirm = document.getElementById("_ctl0_txtConfirmPass").value;
			if (value != valueConfirm)
			{
				//alert("Input confirm password!");
				GetAlertError(iTotal,DSAlert,"Add0005");
				document.getElementById("_ctl0_txtConfirmPass").focus();
				return false;
			}
			if (document.getElementById("_ctl0_cboCopyPer").value==document.getElementById("_ctl0_cboEmpID").value)
			{
				GetAlertError(iTotal,DSAlert,"UCA_0002");
				document.getElementById("_ctl0_txtConfirmPass").focus();
				return false;
			}
		if (checkisnull('cboLSCompanyID')==false) return false;	
	}
	else 
	{
		document.getElementById("_ctl0_txtFlag").value='0';	
		//alert('ItemCheck');
	}
	
	if (document.getElementById("_ctl0_txtFlag").value='0' && document.getElementById('_ctl0_txtUserGroupID').value!="")
	{		
		document.getElementById("_ctl0_txtFlag").value='2'
		if( checkisnull('cboEmpID')==false) return false;
		if (checkisnull('txtUserGroupID')==false) return false;	
		var value = document.getElementById("_ctl0_txtPassword").value;
		var valueConfirm = document.getElementById("_ctl0_txtConfirmPass").value;
			if (value != valueConfirm)
			{
				//alert("Input confirm password!");
				GetAlertError(iTotal,DSAlert,"Add0005");
				document.getElementById("_ctl0_txtConfirmPass").focus();
				return false;
			}
			if (document.getElementById("_ctl0_cboCopyPer").value==document.getElementById("_ctl0_cboEmpID").value)
			{
				GetAlertError(iTotal,DSAlert,"UCA_0002");
				document.getElementById("_ctl0_txtConfirmPass").focus();
				return false;
			}
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
function checkReset()
{
	if(GridCheck('_ctl0_dtgList',3,1,'chkSelect')==false)
	{
		GetAlertError(iTotal,DSAlert,"0001");		
		return false;
	}
	if(confirm(GetAlertText(iTotal,DSAlert,"UCA_0001"))==false){
		return false;
	}
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
function checkAddnew()
{
	if (document.getElementById('_ctl0_txtUserGroupName').value!=document.getElementById('_ctl0_txtUserGroupName').title)
	{
		if(confirm(GetAlertText(iTotal,DSAlert,"0090"))==false){
			return true
		}else
		{			
			document.getElementById('_ctl0_btnSave').click();
			return false;
		}
	}
}
function AdminChange(field)
	{
		
			var index;
		index = field.id.replace('_ctl0_dtgList__ctl','');
		index=index.replace('_chkAdm','');
		
		if (field.checked)
		{
			document.getElementById('_ctl0_dtgList__ctl' + index + '_chkValid').checked=true;
		}
	
	}
function AutoGenPass(field)
{
	
	if (field.checked==true)
	{
		//alert(field.checked);
		document.getElementById('_ctl0_txtPassword').disabled=true;
		document.getElementById('_ctl0_txtConfirmPass').disabled=true;
		
	}else
	{
		document.getElementById('_ctl0_txtPassword').disabled=false;
		document.getElementById('_ctl0_txtConfirmPass').disabled=false;
	}
}
AutoGenPass(document.getElementById('_ctl0_ChkPassAuto'));

//Search Account
function OpenWindowEmp_Replace()
	{
		ShowDialog('FormPage.aspx?Ascx=MdlHR/EmpPopup.ascx&Params=4&GetAll=1');
	}	
function ReturnEmpPopUp4(strEmpID, strEmpCode, strEmpName, strStartDate)
	{
		document.getElementById("_ctl0_cboEmpID").value = strEmpID;
		//document.getElementById("_ctl0_txtUserGroupName").value = strEmpName;	
		
		//var sEmpID=document.getElementById("_ctl0_cboEmpID").value;
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
