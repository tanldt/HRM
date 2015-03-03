<%@ Import Namespace ="iHRPCore.Com"%>
<%@ Import Namespace ="System.Data" %>
<%@ Import Namespace ="System.Data.SqlClient" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ContractExtend.ascx.cs" Inherits="iHRPCore.MdlHR.ContractExtend" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<TABLE id="tblemp" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD vAlign="middle" align="left" colSpan="1" rowSpan="1"><asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center">
			<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="95%" border="0">
				<TR>
					<TD width="15%" colSpan="2"><asp:label id="Label1" runat="server" CssClass="label" ForeColor="Maroon" Font-Underline="True">Filter by</asp:label></TD>
					<TD width="5%" colSpan="1" rowSpan="1"></TD>
					<TD width="10%"></TD>
					<TD width="20%"></TD>
					<TD width="5%"></TD>
				</TR>
				<TR>
					<TD width="12%" colSpan="1" rowSpan="1"><asp:label id="Label7" runat="server" CssClass="label">Contract End on</asp:label></TD>
					<TD width="25%" colSpan="1" rowSpan="1"><asp:dropdownlist id="cboMonth" runat="server" CssClass="combo" Width="96px"></asp:dropdownlist>&nbsp;
						<asp:dropdownlist id="cboYear" runat="server" CssClass="combo" Width="66px"></asp:dropdownlist></TD>
					<TD width="5%" colSpan="1" rowSpan="1"></TD>
					<TD width="8%" colSpan="1" rowSpan="1"><asp:label id="Label8" runat="server" CssClass="label">Customer group</asp:label></TD>
					<TD width="15%" colSpan="1" rowSpan="1"><asp:dropdownlist id="cboCompanyID" runat="server" CssClass="combo" Width="100%" onchange="ChangeCompany()"></asp:dropdownlist></TD>
					<TD width="5%" colSpan="1" rowSpan="1"></TD>
				</TR>
				<TR>
					<TD colSpan="1" rowSpan="1"><asp:label id="Label4" runat="server" CssClass="label">Company</asp:label></TD>
					<TD colSpan="1" rowSpan="1"><asp:dropdownlist id="cboLevel1ID" runat="server" CssClass="combo" Width="100%" onchange="ChangeLevel1()"></asp:dropdownlist></TD>
					<TD></TD>
					<TD colSpan="1" rowSpan="1"><asp:label id="Label5" runat="server" CssClass="label"> Department</asp:label></TD>
					<TD width="15%" colSpan="1" rowSpan="1"><asp:dropdownlist id="cboLevel2ID" runat="server" CssClass="combo" Width="100%" onchange="ChangeLevel2()"></asp:dropdownlist></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD>
						<asp:label id="Label2" runat="server" CssClass="label"> Contract Type</asp:label></TD>
					<TD colSpan="4">
						<asp:checkboxlist id="chklistContractType" CssClass="checkbox" Runat="server" RepeatDirection="Horizontal"></asp:checkboxlist>
						<asp:checkbox id="chkTX4" style="DISPLAY:none" runat="server" CssClass="checkbox" Width="96px"
							Text="Th? vi?c" Checked="True"></asp:checkbox>
						<asp:checkbox id="chkTX6" style="DISPLAY:none" runat="server" CssClass="checkbox" Width="144px"
							Text="Ng?n h?n 1 nam" Checked="True"></asp:checkbox>
						<asp:checkbox id="chkTX7" style="DISPLAY:none" runat="server" CssClass="checkbox" ToolTip="Alt+G"
							Text="Ng?n h?n 2 nam" Checked="True" Width="136px"></asp:checkbox>
						<asp:checkbox id="Checkbox1" style="DISPLAY:none" CssClass="checkbox" runat="server" Width="144px"
							Checked="True" Text="Ng?n h?n 3 nam"></asp:checkbox>
						<asp:checkbox id="Checkbox2" style="DISPLAY:none" CssClass="checkbox" runat="server" Width="144px"
							Checked="True" Text="Không xác d?nh th?i h?n"></asp:checkbox></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD><INPUT id="txtLevel1ID" type="hidden" size="1" name="txtLevel1ID" runat="server">
						<INPUT id="txtLevel2ID" type="hidden" size="1" name="txtLevel2ID" runat="server">
						<INPUT id="txtLevel3ID" type="hidden" size="1" name="txtLevel3ID" runat="server"></TD>
					<TD></TD>
					<TD></TD>
					<TD width="15%"></TD>
					<TD></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD align="center">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="95%" border="0">
				<TR>
					<TD width="10%"><asp:label id="Label9" runat="server" CssClass="label" ForeColor="Maroon" Font-Underline="True">Save</asp:label></TD>
					<TD width="20%"></TD>
					<TD width="12%" colSpan="1" rowSpan="1"></TD>
					<TD width="20%"></TD>
					<TD width="5%"></TD>
					<TD width="10%" colSpan="1" rowSpan="1"></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD width="15%" colSpan="1" rowSpan="1"><asp:label id="lblSignDate" runat="server" CssClass="label" Width="100%">Sign Date</asp:label></TD>
					<TD width="20%" colSpan="1" rowSpan="1"><asp:textbox id="txtSignDate" onblur="JavaScript:CheckDate(this)" runat="server" CssClass="input"
							Width="70px" MaxLength="10"></asp:textbox>&nbsp;<INPUT class="btnCal" onclick="javascript:popUpCalendar(<%=this.txtSignDate.ClientID%>);" type="button"></TD>
					<TD><asp:label id="lblSigner" runat="server" CssClass="label">Signer</asp:label></TD>
					<TD width="20%" colSpan="1" rowSpan="1"><asp:textbox id="txtSigner" runat="server" CssClass="input" Width="100%" MaxLength="50"></asp:textbox></TD>
					<TD></TD>
					<TD width="10%" colSpan="1" rowSpan="1"><asp:label id="lblPosSigner" runat="server" CssClass="label" Width="100%"> Post Title</asp:label></TD>
					<TD><asp:textbox id="txtSignerPosition" runat="server" CssClass="input" Width="100%" MaxLength="100"></asp:textbox></TD>
				</TR>
				<TR>
					<TD width="10%">
						<asp:label id="Label3" CssClass="label" runat="server" Width="100%">Contract Type</asp:label></TD>
					<TD width="20%" colSpan="3">
						<asp:dropdownlist id="Dropdownlist1" CssClass="combo" runat="server" Width="100%" onchange="ChangeLevel1()"></asp:dropdownlist></TD>
					<TD></TD>
					<TD width="10%"></TD>
					<TD></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD align="center">
			<TABLE id="tblButton" cellSpacing="0" cellPadding="0" width="98%" border="0">
				<TR>
					<TD vAlign="middle" width="15%"></TD>
					<TD align="center"></TD>
					<TD align="right" width="15%"></TD>
				</TR>
				<TR vAlign="top">
					<TD vAlign="middle" width="15%"><asp:checkbox id="chkShowGrid" accessKey="G" onclick="javascript:collapse('_ctl0_dtgList');" runat="server"
							CssClass="checkbox" ToolTip="Alt+G" Text="Show grid" Checked="True"></asp:checkbox></TD>
					<TD align="center" width="*"><asp:linkbutton id="btnFilter" accessKey="N" runat="server" CssClass="button" ToolTip="Alt + F"
							CausesValidation="False">Filter</asp:linkbutton>&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnSave" accessKey="S" runat="server" CssClass="button" ToolTip="Alt + S" CausesValidation="False">Save</asp:linkbutton>&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnList" accessKey="L" runat="server" CssClass="btnList" ToolTip="Alt + L" CausesValidation="False"> List</asp:linkbutton>&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnExport" accessKey="E" runat="server" CssClass="btnExport" ToolTip="Alt+E">Export</asp:linkbutton></TD>
					<TD align="right" width="15%"></TD>
				</TR>
				<TR>
					<TD vAlign="middle" width="15%"></TD>
					<TD align="center" colSpan="1" rowSpan="1"></TD>
					<TD align="right" width="15%"></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD vAlign="top" align="center">
			<DIV style="OVERFLOW: auto; HEIGHT: 400px"><asp:datagrid id="dtgList" CssClass="grid" runat="server" Width="100%" AllowPaging="True" BackColor="White"
					BorderColor="#3366CC" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False" AllowSorting="True">
					<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
					<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
					<ItemStyle CssClass="gridItem"></ItemStyle>
					<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
					<Columns>
						<asp:BoundColumn Visible="False" DataField="EmpID" HeaderText="EmpID"></asp:BoundColumn>
						<asp:TemplateColumn HeaderText="Select">
							<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
							<HeaderTemplate>
								<asp:CheckBox id="chkSelectAll" Checked="True" runat="server" CssClass="checkbox" onclick="CheckAll('_ctl0_dtgList__ctl2_chkSelectAll','_ctl0_dtgList',3,1,'chkSelect')"></asp:CheckBox>
							</HeaderTemplate>
							<ItemTemplate>
								<asp:CheckBox id="chkSelect" Checked="true" Runat="server"></asp:CheckBox>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="#">
							<HeaderStyle Width="30px"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<ItemTemplate>
								<%# Container.ItemIndex + 1+ dtgList.PageSize* dtgList.CurrentPageIndex%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn DataField="EmpCode" HeaderText="Emp Code">
							<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="EmpName" HeaderText="Employee Name">
							<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
							<ItemStyle HorizontalAlign="Left"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn Visible="False" DataField="Position" HeaderText="PositionID">
							<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
							<ItemStyle HorizontalAlign="Left"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn Visible="False" DataField="LSLevel2Name" HeaderText="Department">
							<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
							<ItemStyle HorizontalAlign="Left"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="LSContractName" HeaderText="Contract">
							<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
							<ItemStyle HorizontalAlign="Left"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="EffectiveDate" HeaderText="Eff.Date">
							<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="ToDate" HeaderText="Exp. Date">
							<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
						</asp:BoundColumn>
					</Columns>
					<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
				</asp:datagrid></DIV>
		</TD>
	</TR>
</TABLE>
<%
	string strLangID = Session["LangID"] == null?"EN":Session["LangID"].ToString().Trim();
	int Total2 = 0;
	int Total1 = 0;
	
	DataTable rs2 = new DataTable();	
	DataTable rs1 = new DataTable();
	
	rs1 = clsCommon.GetDataTable("sp_GetDataComboLevel @TableName = 'LS_tblLevel1', @Language='" + strLangID + "'");
	rs2 = clsCommon.GetDataTable("sp_GetDataComboLevel @TableName = 'LS_tblLevel2', @Language='" + strLangID + "'");	
	
	Total1 = rs1.Rows.Count;
	Total2 = rs2.Rows.Count;
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
</script>
<script language="javascript">
	function ChangeCompany()
	{
		var al = document.getElementById("_ctl0_cboLevel1ID").length;
		var all = document.getElementById("_ctl0_cboLevel2ID").length;
		
		for(i=0; i<al; i++)
		{	
			document.getElementById("_ctl0_cboLevel1ID").remove(0);			
		};
		for(i=0; i<all; i++)
		{	
			document.getElementById("_ctl0_cboLevel2ID").remove(0);			
		};
		
		var strCompanyID = document.getElementById("_ctl0_cboCompanyID").value;
		document.getElementById("_ctl0_cboLevel1ID").add(new Option('',''));
		
		for(i=0; i<<%=Total1%>;i++)
		{
			if (DS1[0][i]==strCompanyID)
			{
				document.getElementById("_ctl0_cboLevel1ID").add(new Option(DS1[2][i],DS1[1][i]));
			};
		};
		al = document.getElementById("_ctl0_cboLevel1ID").length;
		document.getElementById("_ctl0_cboLevel1ID").selectedIndex = 0;
		ChangeLevel1();
	}
	function ChangeLevel1()
	{
		var all = document.getElementById("_ctl0_cboLevel2ID").length;
		
		for(i=0; i<all; i++)
		{	
			document.getElementById("_ctl0_cboLevel2ID").remove(0);			
		};
		var strLevel1ID = document.getElementById("_ctl0_cboLevel1ID").value; 
		document.getElementById("_ctl0_cboLevel2ID").add(new Option('',''));
		
		for(i=0; i<<%=Total2%>;i++)
		{
			if (DS2[0][i]==strLevel1ID)
			{
				document.getElementById("_ctl0_cboLevel2ID").add(new Option(DS2[2][i],DS2[1][i]));
			};
		};		
		all = document.getElementById("_ctl0_cboLevel2ID").length;
		document.getElementById("_ctl0_cboLevel2ID").selectedIndex = 0;
		document.getElementById("_ctl0_txtLevel1ID").value = document.getElementById("_ctl0_cboLevel1ID").value;
		ChangeLevel2();			
	}
	
	function ChangeLevel2()
	{
		document.getElementById("_ctl0_txtLevel2ID").value = document.getElementById("_ctl0_cboLevel2ID").value;		
	}
	
function validform()
{
	if(GridCheck('_ctl0_dtgList',3,1,'chkSelect')==false)
	{
		GetAlertError(iTotal,DSAlert,"0049");
		return false;
	}
}
</script>
