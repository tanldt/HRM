<%@ Import Namespace ="System.Data.SqlClient" %>
<%@ Import Namespace ="System.Data" %>
<%@ Import Namespace ="iHRPCore.Com"%>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="PayrollAdditional.ascx.cs" Inherits="iHRPCore.MdlPR.PayrollAdditional" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="EmpHeader" Src="../Include/EmpHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ColumnList" Src="../Include/ColumnList.ascx" %>
<%
	string strLangID = Session["LangID"] == null?"EN":Session["LangID"].ToString().Trim();
	string sAccountLogin= Session["AccountLogin"]!=null?Session["AccountLogin"].ToString():"admin";
	string Ascx=Request.Params["Ascx"];		
	int Total1 = 0;
	int Total2 = 0;
	int Total3 = 0;
			
	DataTable rs1 = new DataTable();
	DataTable rs2 = new DataTable();		
	DataTable rs3 = new DataTable();	
	
	rs1 = clsCommon.GetDataTable("sp_GetDataComboLevel @TableName = 'LS_tblLevel1', @Language='" + strLangID + "',@UserGroupID='" + sAccountLogin + "'");
	rs2 = clsCommon.GetDataTable("sp_GetDataComboLevel @TableName = 'LS_tblLevel2', @Language='" + strLangID + "',@UserGroupID='" + sAccountLogin + "'");
	rs3 = clsCommon.GetDataTable("sp_GetDataComboLevel @TableName = 'LS_tblLevel3', @Language='" + strLangID + "',@UserGroupID='" + sAccountLogin + "'");
	
	Total1 = rs1.Rows.Count;
	Total2 = rs2.Rows.Count;
	Total3 = rs3.Rows.Count;		
%>
<script language="javascript">					
		var DS1 = new Array(3);
		DS1[0] = new Array(<%=Total1%>);
		DS1[1] = new Array(<%=Total1%>);
		DS1[2] = new Array(<%=Total1%>);	
		<% for(int i=0; i<Total1; i++) {%>		
			DS1[0][<%=i%>]="<%=rs1.Rows[i]["LSCompanyID"]%>";
			DS1[1][<%=i%>]="<%=rs1.Rows[i]["LSLevel1ID"]%>";
			DS1[2][<%=i%>]="<%=rs1.Rows[i]["Name"]%>";	
		<%}%>	
		
		var DS2 = new Array(3);
		DS2[0] = new Array(<%=Total2%>);
		DS2[1] = new Array(<%=Total2%>);
		DS2[2] = new Array(<%=Total2%>);	
		<% for(int i=0; i<Total2; i++) {%>		
			DS2[0][<%=i%>]="<%=rs2.Rows[i]["LSLevel1ID"]%>";
			DS2[1][<%=i%>]="<%=rs2.Rows[i]["LSLevel2ID"]%>";
			DS2[2][<%=i%>]="<%=rs2.Rows[i]["Name"]%>";	
		<%}%>		
		
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
		var all = document.getElementById("_ctl0_cboLevel2ID").length;
		var all1 = document.getElementById("_ctl0_cboLevel3ID").length;
		var all2 = document.getElementById("_ctl0_cboLevel1ID").length;
		for(i=0; i<all2; i++)
		{	
			document.getElementById("_ctl0_cboLevel1ID").remove(0);			
		};
		
		for(i=0; i<all; i++)
		{	
			document.getElementById("_ctl0_cboLevel2ID").remove(0);			
		};
		for(i=0; i<all1; i++)
		{	
			document.getElementById("_ctl0_cboLevel3ID").remove(0);			
		};
		var strCompany = document.getElementById("_ctl0_cboCompanyID").value; 
		document.getElementById("_ctl0_cboLevel1ID").add(new Option('',''));
		for(i=0; i<<%=Total1%>;i++)
		{
			if (DS1[0][i]==strCompany)
			{
				document.getElementById("_ctl0_cboLevel1ID").add(new Option(DS1[2][i],DS1[1][i]));
			};
		};		
		all = document.getElementById("_ctl0_cboLevel1ID").length;
		document.getElementById("_ctl0_cboLevel1ID").selectedIndex = 0;
		ChangeLevel1();			
	}
	
	function ChangeLevel1()
	{
		var all = document.getElementById("_ctl0_cboLevel2ID").length;
		var all1 = document.getElementById("_ctl0_cboLevel3ID").length;
		
		for(i=0; i<all; i++)
		{	
			document.getElementById("_ctl0_cboLevel2ID").remove(0);			
		};
		for(i=0; i<all1; i++)
		{	
			document.getElementById("_ctl0_cboLevel3ID").remove(0);			
		};
		var strLevel1ID = document.getElementById("_ctl0_cboLevel1ID").value; 
		document.getElementById("_ctl0_cboLevel2ID").add(new Option('',''));
		
		for(i=0; i<<%=Total2%>;i++)
		{
			if (DS2[0][i]==strLevel1ID|| strLevel1ID=="")
			{
				document.getElementById("_ctl0_cboLevel2ID").add(new Option(DS2[2][i],DS2[1][i]));
			};
		};		
		all = document.getElementById("_ctl0_cboLevel2ID").length;
		document.getElementById("_ctl0_cboLevel2ID").selectedIndex = 0;
		document.getElementById("_ctl0_txtLevel1ID").value = strLevel1ID;
		ChangeLevel2();			
	}
	
	function ChangeLevel2()
	{
		var all = document.getElementById("_ctl0_cboLevel3ID").length;
		
		for(i=0; i<all; i++)
		{	
			document.getElementById("_ctl0_cboLevel3ID").remove(0);			
		};
		var strLevel2ID = document.getElementById("_ctl0_cboLevel2ID").value; 
		document.getElementById("_ctl0_cboLevel3ID").add(new Option('',''));
		
		for(i=0; i<<%=Total3%>;i++)
		{
			if (DS3[0][i]==strLevel2ID)
			{
				document.getElementById("_ctl0_cboLevel3ID").add(new Option(DS3[2][i],DS3[1][i]));
			};
		};		
		document.getElementById("_ctl0_txtLevel2ID").value = document.getElementById("_ctl0_cboLevel2ID").value;
		ChangeLevel3();	
	}
	function ChangeLevel3()
	{	
		document.getElementById("_ctl0_txtLevel3ID").value = document.getElementById("_ctl0_cboLevel3ID").value;
	}
</script>
<TABLE id="Table1" style="WIDTH: 100%" cellSpacing="2" cellPadding="2" width="300" border="0">
	<TR>
		<TD align="left"><asp:label id="lblErr" runat="server" CssClass="label"></asp:label></TD>
	</TR>
	<TR>
		<TD align="left"><asp:radiobuttonlist id="optCollection" onclick="ChangeCollectionType()" runat="server" RepeatDirection="Horizontal"
				Width="424px">
				<asp:ListItem Value="1" Selected="True">Update for employee</asp:ListItem>
				<asp:ListItem Value="2">Update for list of employees</asp:ListItem>
			</asp:radiobuttonlist></TD>
	</TR>
	<TR id="trEmp" runat="server">
		<TD align="center"><uc1:empheader id="HR_EmpHeader" runat="server"></uc1:empheader></TD>
	</TR>
	<TR id="trListEmp" runat="server">
		<TD align="center">
			<TABLE id="Table2" style="WIDTH: 100%" cellSpacing="1" cellPadding="0" border="0">
				<TR>
					<TD align="left" width="15%"><asp:label id="Label3" runat="server" CssClass="labelRequire">Company</asp:label></TD>
					<TD width="30%"><asp:dropdownlist id="cboCompanyID" runat="server" CssClass="select" Width="100%" onchange="ChangeCompany()"></asp:dropdownlist></TD>
					<TD width="15%">&nbsp;<asp:label id="Label4" runat="server" CssClass="label">Division</asp:label></TD>
					<TD width="50%"><asp:dropdownlist id="cboLevel1ID" runat="server" CssClass="select" Width="100%" onchange="ChangeLevel1()"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD align="left"><asp:label id="Label6" runat="server" CssClass="label">Department</asp:label></TD>
					<TD><asp:dropdownlist id="cboLevel2ID" runat="server" CssClass="select" Width="100%" onchange="ChangeLevel2()"></asp:dropdownlist></TD>
					<TD>&nbsp;<asp:label id="Label7" runat="server" CssClass="label">Section</asp:label></TD>
					<TD><asp:dropdownlist id="cboLevel3ID" runat="server" CssClass="select" Width="100%" onchange="ChangeLevel3()"></asp:dropdownlist></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<tr>
		<td>
			<TABLE id="Table3" style="WIDTH: 100%" cellSpacing="1" cellPadding="0" border="0">
				<tr>
					<TD style="HEIGHT: 24px" width="15%"><asp:label id="Label2" runat="server" CssClass="labelRequire">Tháng tham chiếu</asp:label></TD>
					<TD style="HEIGHT: 24px" width="30%"><asp:textbox id="txtMonth" onblur="javascript:CheckMonth(this)" runat="server" CssClass="input"
							Width="90px" MaxLength="2"></asp:textbox></TD>
					<TD style="HEIGHT: 24px" width="15%"><asp:label id="Label8" runat="server" CssClass="labelRequire">Year</asp:label></TD>
					<TD style="HEIGHT: 24px" width="50%"><asp:textbox id="txtYear" onblur="javascript:CheckYear(this)" runat="server" CssClass="input"
							Width="96px" MaxLength="4"></asp:textbox></TD>
				</tr>
				<TR>
					<TD width="15%"><asp:label id="Label5" runat="server" CssClass="labelRequire"> Hệ số</asp:label></TD>
					<TD width="30%"><asp:textbox id="txtCoef" onblur="javascript:CheckNumeric(this)" runat="server" CssClass="input"
							Width="120px" MaxLength="7" Visible="False"></asp:textbox><asp:textbox id="txtCoefAdditional" onblur="javascript:checkNumeric(this,9999)" runat="server"
							CssClass="input" Width="90px" MaxLength="7"></asp:textbox></TD>
					<TD width="15%"><asp:label id="Label1" runat="server" CssClass="labelRequire"> Loại lương bổ sung</asp:label></TD>
					<TD width="50%"><asp:dropdownlist id="cboPayrollAdditionalType" runat="server" CssClass="Combo" Width="100%">
							<asp:ListItem Value="1">Lương bổ sung 6 th&#225;ng</asp:ListItem>
							<asp:ListItem Value="2">Lương bổ sung 12 th&#225;ng</asp:ListItem>
						</asp:dropdownlist></TD>
				</TR>
			</TABLE>
		</td>
	</tr>
	<TR>
		<TD></TD>
	</TR>
	<TR>
		<TD align="center"><asp:linkbutton id="btnCalulate" accessKey="E" runat="server" CssClass="btnExport" ToolTip="Alt + T"> Calculate</asp:linkbutton>&nbsp;
			<asp:linkbutton id="btnView" accessKey="E" runat="server" CssClass="btnExport">View</asp:linkbutton>&nbsp;
			<asp:linkbutton id="btnDelete" accessKey="E" runat="server" CssClass="btnExport">Delete</asp:linkbutton>&nbsp;
			<asp:linkbutton id="btnLock" accessKey="E" runat="server" CssClass="btnExport" Visible="False">Lock Salary</asp:linkbutton>&nbsp;
			<asp:linkbutton id="btnPrint" accessKey="E" runat="server" CssClass="btnExport" Visible="False">Print</asp:linkbutton>&nbsp;
			<asp:linkbutton id="btnExport" accessKey="E" runat="server" CssClass="btnExport" ToolTip="Alt+E">Export</asp:linkbutton>
		</TD>
	</TR>
	<TR id="trColumnList" style="DISPLAY: none" runat="server">
		<TD align="left" height="10"><uc1:columnlist id="uctrlColumns" runat="server"></uc1:columnlist></TD>
	</TR>
	<TR>
		<TD align="center"><asp:datagrid id="grdPayRoll" runat="server" CssClass="grid" Width="100%" BorderColor="#3366CC"
				PageSize="20" AllowSorting="True" AutoGenerateColumns="False" AllowPaging="True" CellPadding="0" BorderWidth="1px"
				BackColor="White">
				<FooterStyle CssClass="gridFooter"></FooterStyle>
				<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
				<ItemStyle CssClass="gridItem"></ItemStyle>
				<HeaderStyle CssClass="gridHeader"></HeaderStyle>
				<Columns>
					<asp:BoundColumn Visible="False" DataField="EmpID" HeaderText="EmpID">
						<HeaderStyle Width="9%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="PayrollAdditionalID" HeaderText="PayrollAdditionalID">
						<HeaderStyle Width="9%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn HeaderText="&lt;b&gt;Seq&lt;/b&gt;">
						<HeaderStyle Width="5%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<%# Container.ItemIndex + 1 + grdPayRoll.PageSize*grdPayRoll.CurrentPageIndex%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="YYYY" SortExpression="YYYY" HeaderText="Năm">
						<HeaderStyle Width="5%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="EmpCode" SortExpression="EmpCode" HeaderText="EmpCode">
						<HeaderStyle Width="5%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="EmpName" SortExpression="EmpName" HeaderText="Emp Name">
						<HeaderStyle Width="25%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Left"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="ContractName" SortExpression="ContractName" HeaderText="Contract Name">
						<HeaderStyle Width="10%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="NumMonth" HeaderText="Num Month">
						<HeaderStyle Width="7%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="CoefAdditional" SortExpression="CoefAdditional" HeaderText="Coef Additional">
						<HeaderStyle Width="5%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="PayrollGross" SortExpression="PayrollGross" HeaderText="Payroll Gross">
						<HeaderStyle Width="8%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="PayrollTax" SortExpression="PayrollTax" HeaderText="PayrollTax">
						<HeaderStyle Width="8%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="PayrollNet" SortExpression="PayrollNet" HeaderText="PayrollNet">
						<HeaderStyle Width="15%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn HeaderText="Select">
						<HeaderStyle Width="8%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<HeaderTemplate>
							<asp:CheckBox id="chkSelectAll" runat="server" CssClass="checkbox" onclick="CheckAll('_ctl0_grdPayRoll__ctl2_chkSelectAll','_ctl0_grdPayRoll',3,1,'chkSelect')"></asp:CheckBox>
						</HeaderTemplate>
						<ItemTemplate>
							<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
			</asp:datagrid></TD>
	</TR>
	<TR style="DISPLAY: none">
		<TD align="center"><INPUT id="txtLevel1ID" type="hidden" runat="server"><INPUT id="txtLevel2ID" type="hidden" runat="server"><INPUT id="txtLevel3ID" type="hidden" runat="server">
			<asp:textbox id="txtFromDate" runat="server"></asp:textbox><asp:textbox id="txtToDate" runat="server"></asp:textbox><asp:textbox id="txtPageLoad" runat="server" Visible="False"></asp:textbox></TD>
	</TR>
</TABLE>
<script language="javascript">
function Lock()
{
	if(confirm(GetAlertText(iTotal,DSAlert,"PC_0002"))==false){
		return false;
	}
}
function checkdelete()
{
	if (document.getElementById('_ctl0_grdPayRoll') != null)
	{
		if(GridCheck('_ctl0_grdPayRoll',3,1,'chkSelect')==false)
		{
			GetAlertError(iTotal,DSAlert,"0001");				
			return false;
		}
		if(confirm(GetAlertText(iTotal,DSAlert,"0002"))==false){
			return false;
		}
	}
	else
		return false;
}
function Calculate()
{
//	if (checkisnull("cboCompanyID")==false) return false;

if (trim(document.getElementById("_ctl0_txtMonth").value) == "") 
	{
		//alert("Input from date!");
		GetAlertError(iTotal, DSAlert, "0003");
		document.getElementById("_ctl0_txtMonth").focus();
		return false;
	}

if (trim(document.getElementById("_ctl0_txtYear").value) == "") 
	{
		//alert("Input from date!");
		GetAlertError(iTotal, DSAlert, "0003");
		document.getElementById("_ctl0_txtYear").focus();
		return false;
	}

if (trim(document.getElementById("_ctl0_txtCoefAdditional").value) == "") 
	{
		//alert("Input from date!");
		GetAlertError(iTotal, DSAlert, "0003");
		document.getElementById("_ctl0_txtCoefAdditional").focus();
		return false;
	}


	return true;

}
function checkisnull(obj){
/*	if(document.getElementById('_ctl0_' + obj).value=="")
		{
			GetAlertError(iTotal,DSAlert,"0003");				
			document.getElementById('_ctl0_' + obj).focus();
			return false;
		}
	else
		return true;
*/		

}
function ChangeCollectionType()
	{
		if (document.getElementById("_ctl0_optCollection_0").checked == true)
		{
			document.getElementById("_ctl0_trEmp").style.display = "";
			document.getElementById("_ctl0_trListEmp").style.display = "none";
		}
		else
		{
			document.getElementById("_ctl0_trEmp").style.display = "none";
			document.getElementById("_ctl0_trListEmp").style.display = "";
		}
	}
</script>
<script>
	ChangeCollectionType();
</script>
