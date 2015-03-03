<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../Include/EmpHeaderSearch.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="PayrollCollection.ascx.cs" Inherits="iHRPCore.PayrollCollection" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="ColumnList" Src="../Include/ColumnList.ascx" %>
<TABLE id="Table1" style="WIDTH: 100%" cellSpacing="2" cellPadding="2" width="300" border="0">
	<TR>
		<TD align="left"><asp:label id="lblErr" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center">
			<TABLE id="Table2" style="WIDTH: 100%" cellSpacing="1" cellPadding="0" border="0">
				<TR>
					<TD colSpan="4"><uc1:empheadersearch id="EmpHeaderSearch1" runat="server" IsStatus="false"></uc1:empheadersearch></TD>
				</TR>
				<TR>
					<TD width="15%"><asp:label id="Label5" runat="server" CssClass="labelRequire"> Month</asp:label></TD>
					<TD width="30%"><asp:textbox id="txtMonth" onblur="javascript:CheckMonthYear(this)" runat="server" CssClass="input"
							Width="70%" MaxLength="7"></asp:textbox></TD>
					<TD width="15%">&nbsp;<asp:label id="Label1" style="DISPLAY: none" runat="server" CssClass="label"> Salary Period</asp:label></TD>
					<TD width="50%"><asp:textbox id="txtSalPeriod" style="DISPLAY: none" runat="server" CssClass="input" Width="70%"
							MaxLength="12" ReadOnly="True"></asp:textbox></TD>
				</TR>
			</TABLE>
			<asp:linkbutton id="btnCalulate" accessKey="E" runat="server" CssClass="btnExport" Visible="False"
				ToolTip="Alt + T"> Calculate</asp:linkbutton><asp:linkbutton id="btnDelete" accessKey="E" runat="server" CssClass="btnExport" Visible="False">Delete</asp:linkbutton><asp:linkbutton id="btnPrint" accessKey="E" runat="server" CssClass="btnExport" Visible="False">Print</asp:linkbutton><asp:linkbutton id="btnView" accessKey="E" runat="server" CssClass="btnExport" Visible="False">View</asp:linkbutton>&nbsp;&nbsp;
			<div style="DISPLAY:none">
				<INPUT class="button" id="btnXem" onclick="FillTable();" type="button" value="Calculate">&nbsp;&nbsp;&nbsp;&nbsp;
				<INPUT class="button" id="btnXem2" onclick="FillTable2();" type="button" value="Calculate">&nbsp;&nbsp;&nbsp;&nbsp;
				<INPUT class="button" id="btnGetdata" onclick="GetData();" type="button" value="GetData">&nbsp;&nbsp;&nbsp;&nbsp;
				<INPUT class="button" id="btnCalUser" onclick="CalUser();" type="button" value="CalUser">&nbsp;&nbsp;&nbsp;&nbsp;
			</div>
		</TD>
	</TR>
	<TR>
		<TD align="center">
			<INPUT class="button" id="btnCal" onclick="Calculate();" type="button" value="Calculate">&nbsp;&nbsp;&nbsp;&nbsp;
			<INPUT class="button" id="btnView1" onclick="ViewTable();" type="button" value="View">&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:linkbutton id="btnLock" runat="server" CssClass="btnExport">Send approved</asp:linkbutton>&nbsp;</TD>
	</TR>
	<tr>
		<td>
			<table id="tblMessage" style="DISPLAY:none">
				<tr>
					<td id="tdImg"></td>
					<td id="tdMessage" width="100%"></td>
					<td><a href="javascript:HideControl('tblMessage');" class="close"><IMG src="images/AJAX/x1.gif" class="IMG"></a></td>
				</tr>
			</table>
		</td>
	</tr>
	<TR>
		<TD>
			<DIV style="OVERFLOW: scroll; WIDTH: 750px; HEIGHT: 400px"><SPAN id="Loading"></SPAN><SPAN id="Displays"></SPAN></DIV>
		</TD>
	</TR>
	<TR id="trColumnList" style="DISPLAY: none" runat="server">
		<TD align="left" height="10"><uc1:columnlist id="uctrlColumns" runat="server"></uc1:columnlist></TD>
	</TR>
	<TR style="DISPLAY: none">
		<TD align="center">
			<DIV style="OVERFLOW: scroll; WIDTH: 750px; HEIGHT: 300px"><asp:datagrid id="grdPayRoll" runat="server" CssClass="grid" Width="100%" BorderColor="#3366CC"
					PageSize="20" AllowSorting="True" AutoGenerateColumns="False" AllowPaging="True" CellPadding="0" BorderWidth="1px" BackColor="White">
					<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
					<ItemStyle CssClass="gridItem"></ItemStyle>
					<HeaderStyle CssClass="gridHeader"></HeaderStyle>
					<FooterStyle CssClass="gridFooter"></FooterStyle>
					<Columns>
						<asp:BoundColumn DataField="EmpID" Visible="False" HeaderText="EmpID">
							<HeaderStyle Width="9%"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="Month" Visible="False" HeaderText="Month"></asp:BoundColumn>
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
						<asp:TemplateColumn HeaderText="&lt;b&gt;Seq&lt;/b&gt;">
							<HeaderStyle Width="5%"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<ItemTemplate>
								<%# Container.ItemIndex + 1 + grdPayRoll.PageSize*grdPayRoll.CurrentPageIndex%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn DataField="EmpName" HeaderText="FullName"></asp:BoundColumn>
						<asp:BoundColumn DataField="TotalIncome" HeaderText="Total Income"></asp:BoundColumn>
					</Columns>
					<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
				</asp:datagrid></DIV>
		</TD>
	</TR>
	<TR style="DISPLAY: none">
		<TD align="center"><INPUT id="txtLevel1ID" type="hidden" runat="server"><INPUT id="txtLevel2ID" type="hidden" runat="server"><INPUT id="txtLevel3ID" type="hidden" runat="server">
			<asp:textbox id="txtFromDate" runat="server"></asp:textbox><asp:textbox id="txtToDate" runat="server"></asp:textbox><asp:textbox id="txtPageLoad" runat="server" Visible="False"></asp:textbox></TD>
	</TR>
</TABLE>
<SCRIPT language="javascript">
function Lock()
{
	/*if(confirm(GetAlertText(iTotal,DSAlert,"PC_0002"))==false){
		return false;
	}*/
	var Month = document.getElementById('<%=txtMonth.ClientID%>').value;
	window.open('./FormPage.aspx?Ascx=MdlPR/SendApprovedPayroll.ascx&MMYYYY='+Month,'SelectFile','status=1,toolbar=0,scrollbars=1,resizable=1,alwaysRaised=Yes, top=20, left=30, width=800, height=650,1 ,align=center');
	return false;
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

function checkisnull(obj){
	if(document.getElementById('_ctl0_' + obj).value=="")
		{
			GetAlertError(iTotal,DSAlert,"0003");				
			document.getElementById('_ctl0_' + obj).focus();
			return false;
		}
	else
		return true;
}
function ShowPage()
{
	var Month = document.getElementById('<%=txtMonth.ClientID%>').value;
	var SalPeriod = "";
	var CompanyID = "";
	var Level1ID = "";
	var Level2ID = "";
	var Level3ID = "";
	window.open('./MdlPR/SumPayroll.aspx?MMYYYY='+Month+'&SalPeriod=' +SalPeriod + '&Comp=' + CompanyID + '&L1=' +Level1ID+ '&L2=' +Level2ID+ '&L3=' +Level3ID+ '','SelectFile','status=1,toolbar=0,scrollbars=1,resizable=1,alwaysRaised=Yes, top=20, left=30, width=800, height=600,1 ,align=center');
	return false;
}
function checkvalidSearch()
{
	if (checkisnull("txtMonth")==false) return false;
	var txtMonth = document.getElementById('_ctl0_txtMonth').value;
	var checkPay = PayrollCollection.CheckPayrollExists(txtMonth).value;
	if (checkPay == false )
	{
		alert("Chưa có bảng lương tháng " + txtMonth + ". Vui lòng vào màn hình tạo bảng lương tháng " + txtMonth);
		return false;
	}
	
	if (checkisnull("EmpHeaderSearch1_cboCompany")==false) return false;
	if (checkisnull("EmpHeaderSearch1_cboLevel1")==false) return false;
	
	return true;		
}
function FillTable2() 
{
	if (checkvalidSearch() == true)
	{
		document.getElementById("Loading").innerHTML = "<IMG SRC='images/loading.gif'> Calculating...";
		var cboCompany = document.getElementById('_ctl0_EmpHeaderSearch1_cboCompany').value;
		var cboLevel1 = document.getElementById('_ctl0_EmpHeaderSearch1_cboLevel1').value;
		var cboLevel2 = document.getElementById('_ctl0_EmpHeaderSearch1_cboLevel2').value;
		var cboLevel3 = document.getElementById('_ctl0_EmpHeaderSearch1_cboLevel3').value;
		var txtEmpID = document.getElementById('_ctl0_EmpHeaderSearch1_txtEmpID').value;
		var txtEmpName = document.getElementById('_ctl0_EmpHeaderSearch1_txtEmpName').value;
		var txtMonth = document.getElementById('_ctl0_txtMonth').value;
		var AcountLogin = document.getElementById('txtAcountLogin').value;
		var dtFormula = PayrollCollection.GetDataFormula().value;  
		var SetFormulaID = "";
		var s = "";
		var checkPay = PayrollCollection.CheckPayrollExists(txtMonth).value;
		if (checkPay == false )
		{
			alert("Chưa có bảng lương tháng " + txtMonth + ". Vui lòng vào màn hình tạo bảng lương tháng " + txtMonth);
			return false;
		}
		
		if(dtFormula!=null && typeof(dtFormula) == "object") 
		{
			for(var i=0;i<dtFormula.Rows.length;i++) 
			{
				SetFormulaID = dtFormula.Rows[i].SetFormulaID;

				s += "" + PayrollCollection.Calulate(txtMonth,cboCompany,cboLevel1,cboLevel2,cboLevel3,txtEmpID,txtEmpName,"EN",AcountLogin,SetFormulaID).value;  
			}
		}
		document.getElementById("Loading").innerHTML = "";
		if (s == "")
			document.getElementById("Loading").innerHTML = "Calculate successful!"; 	 
		else
			document.getElementById("Loading").innerHTML = s; 	 
	}
}
function Calculate()
{
	var txtMonth = document.getElementById('_ctl0_txtMonth').value;
	var checkPay = PayrollCollection.CheckPayrollExists(txtMonth).value;
	if (checkPay == false )
	{
		alert("Payroll " + txtMonth + " does not exist.");
		return false;
	}
	else
	{
		GetData();
		CalUser();
		return true;
	}
}
function GetData() 
{
	if (checkvalidSearch() == true)
	{
		ShowControl('Waiting');
		document.getElementById("Loading").innerHTML = "<IMG SRC='images/loading.gif'> Calculating...";
		var cboCompany = document.getElementById('_ctl0_EmpHeaderSearch1_cboCompany').value;
		var cboLevel1 = document.getElementById('_ctl0_EmpHeaderSearch1_cboLevel1').value;
		var cboLevel2 = document.getElementById('_ctl0_EmpHeaderSearch1_cboLevel2').value;
		var cboLevel3 = document.getElementById('_ctl0_EmpHeaderSearch1_cboLevel3').value;
		var txtEmpID = document.getElementById('_ctl0_EmpHeaderSearch1_txtEmpID').value;
		var txtEmpName = document.getElementById('_ctl0_EmpHeaderSearch1_txtEmpName').value;
		var txtMonth = document.getElementById('_ctl0_txtMonth').value;
		var AcountLogin = document.getElementById('txtAcountLogin').value;

		var sSys = PayrollCollection.CalulateItemSys(txtMonth,cboCompany,cboLevel1,cboLevel2,cboLevel3,txtEmpID,txtEmpName,"EN",AcountLogin).value;  
		if (sSys == "")
		{
			document.getElementById("Loading").innerHTML = "Get data successful!"; 	 
		}
		else
		{
			document.getElementById("Loading").innerHTML = sSys;
		}
		HideControl('Waiting');
	}
}
function CalUser() 
{
	if (checkvalidSearch() == true)
	{
		ShowControl('Waiting');
		document.getElementById("Loading").innerHTML = "<IMG SRC='images/loading.gif'> Calculating...";
		var cboCompany = document.getElementById('_ctl0_EmpHeaderSearch1_cboCompany').value;
		var cboLevel1 = document.getElementById('_ctl0_EmpHeaderSearch1_cboLevel1').value;
		var cboLevel2 = document.getElementById('_ctl0_EmpHeaderSearch1_cboLevel2').value;
		var cboLevel3 = document.getElementById('_ctl0_EmpHeaderSearch1_cboLevel3').value;
		var txtEmpID = document.getElementById('_ctl0_EmpHeaderSearch1_txtEmpID').value;
		var txtEmpName = document.getElementById('_ctl0_EmpHeaderSearch1_txtEmpName').value;
		var txtMonth = document.getElementById('_ctl0_txtMonth').value;
		var AcountLogin = document.getElementById('txtAcountLogin').value;
		var s = PayrollCollection.CalulateItemUser(txtMonth,cboCompany,cboLevel1,cboLevel2,cboLevel3,txtEmpID,txtEmpName,"EN",AcountLogin,'').value;
		/*
		var dtFormula = PayrollCollection.GetDataFormula().value;  
		var SetFormulaID = "";
		var s = new Array();
		if(dtFormula!=null && typeof(dtFormula) == "object") 
		{
			for(var i=0;i<dtFormula.Rows.length;i++) 
			{
				SetFormulaID = dtFormula.Rows[i].SetFormulaID;
				
				//s[s.length] = "" + PayrollCollection.CalulateItemUser(txtMonth,cboCompany,cboLevel1,cboLevel2,cboLevel3,txtEmpID,txtEmpName,"EN",AcountLogin,SetFormulaID).value;  
			}
		}
			*/
		if (s == "")
			document.getElementById("Loading").innerHTML = "Calculate successful!"; 	 
		else
			document.getElementById("Loading").innerHTML = s; 	 
		
	
		HideControl('Waiting');
	}
}
function FillTable() 
{
	if (checkvalidSearch() == true)
	{
		ShowControl('Waiting');
		document.getElementById("Loading").innerHTML = "<IMG SRC='images/loading.gif'> Calculating...";
		var cboCompany = document.getElementById('_ctl0_EmpHeaderSearch1_cboCompany').value;
		var cboLevel1 = document.getElementById('_ctl0_EmpHeaderSearch1_cboLevel1').value;
		var cboLevel2 = document.getElementById('_ctl0_EmpHeaderSearch1_cboLevel2').value;
		var cboLevel3 = document.getElementById('_ctl0_EmpHeaderSearch1_cboLevel3').value;
		var txtEmpID = document.getElementById('_ctl0_EmpHeaderSearch1_txtEmpID').value;
		var txtEmpName = document.getElementById('_ctl0_EmpHeaderSearch1_txtEmpName').value;
		var txtMonth = document.getElementById('_ctl0_txtMonth').value;
		var AcountLogin = document.getElementById('txtAcountLogin').value;
		
		var sSys = PayrollCollection.CalulateItemSys(txtMonth,cboCompany,cboLevel1,cboLevel2,cboLevel3,txtEmpID,txtEmpName,"EN",AcountLogin).value;  
		if (sSys == "")
		{
			var dtFormula = PayrollCollection.GetDataFormula().value;  
			var dtSeq = PayrollCollection.GetComputationSeq(txtMonth).value;
			var SetFormulaID = "";
			var s = new Array();
			if(dtSeq!=null && typeof(dtSeq) == "object") 
			{
				for(var j=0;j<dtSeq.Rows.length;j++) 
				{
					if(dtFormula!=null && typeof(dtFormula) == "object") 
					{
						for(var i=0;i<dtFormula.Rows.length;i++) 
						{
							SetFormulaID = dtFormula.Rows[i].SetFormulaID;
							var sComputationSeq = dtSeq.Rows[j].ComputationSeq;
							var dtForUser = PayrollCollection.GetSalaryItemUser(txtMonth,cboCompany,cboLevel1,cboLevel2,cboLevel3,txtEmpID,txtEmpName,"EN",AcountLogin,SetFormulaID,sComputationSeq).value;  
							if(dtForUser!=null && typeof(dtForUser) == "object") 
							{
								for(var k=0;k<dtForUser.Rows.length;k++) 
								{
									var sLSSalaryItemDataID = dtForUser.Rows[k].LSSalaryItemDataID;
									var sFormula = dtForUser.Rows[k].Formula;
									s[s.length] = "" + PayrollCollection.CalulateItemUsers(txtMonth,cboCompany,cboLevel1,cboLevel2,cboLevel3,txtEmpID,txtEmpName,"EN",AcountLogin,SetFormulaID,sLSSalaryItemDataID,sFormula).value;  
								}
							}
							//s[s.length] = "" + PayrollCollection.CalulateItemUser(txtMonth,cboCompany,cboLevel1,cboLevel2,cboLevel3,txtEmpID,txtEmpName,"EN",AcountLogin,SetFormulaID).value;  
						}
					}
				}
			}
			//document.getElementById("Loading").innerHTML = "";
			if (s.join("") == "")
				document.getElementById("Loading").innerHTML = "Calculate successful!"; 	 
			else
				document.getElementById("Loading").innerHTML = s.join(""); 	 
			
		}
		else
		{
			document.getElementById("Loading").innerHTML = sSys;
		}
		HideControl('Waiting');
	}
}

function FillTable_CallBack(response) 
{ 
	var dt = response.value; 
	var txtMonth = document.getElementById('_ctl0_txtMonth').value;
	var AcountLogin = document.getElementById('txtAcountLogin').value;
	
	if(dt!=null && typeof(dt) == "object" && dt!=null) 
	{
		var s = new Array();
		s[s.length] = "<table width='100%' cellspacing='1' cellpadding='1' bordercolor='#999999' border='1' style='border-color:#999999;font-family:Arial;border-collapse:collapse;'>";
		s[s.length] = "<tr class=gridHeader align=Center>"; 
		s[s.length] = "<td align=Center>No.</td><td>Emp Code</td><td >Full Name</td><td >Company</td><td>Status Calculate</td>"; 
		s[s.length] = "</tr>"; 
		for(var i=0;i<dt.Rows.length;i++) 
		{
			s[s.length] = "<tr>"; 
			s[s.length] = "<td class=GridItem align=Center>" + (i + 1) + "</td>";  
			s[s.length] = "<td class=GridItem>" + dt.Rows[i].EmpCode + "</td>";  
			s[s.length] = "<td class=GridItem>" + dt.Rows[i].EmpName + "</td>"; 
			s[s.length] = "<td class=GridItem>" + dt.Rows[i].Level1+  "</td>"; 
			//s[s.length] = "<td class=GridItem>" + dt.Rows[i].BasicSalary + "</td>"; 
			//s[s.length] = "<td class=GridItem>" + dt.Rows[i].TotalIncome + "</td>"; 
			s[s.length] = "<td class=GridItem>" + PayrollCollection.CalPayroll(txtMonth,dt.Rows[i].EmpCode,AcountLogin).value + "</td>"; 
			//s[s.length] = "<td align='Center'><input type='text' readonly='readonly' class='input' style=width:40px;text-align:center' value='" + dt.Rows[i].Time5 + "' /></td>"; 
			//s[s.length] = "<td align='Center'><input type='text' readonly='readonly' class='input' style=width:40px;text-align:center' value='" + dt.Rows[i].Time6 + "' /></td>"; 
			s[s.length] = "</tr>";  
		}
		s[s.length] = "</table>"; 
		document.getElementById("Loading").innerHTML = "";
		document.getElementById("Loading").innerHTML = s.join(""); 	 
	}	
	
	
}
/*
Cách làm theo từng Emp, nhưng hơi chậm. Chuyển lại làm theo từng công thức
function FillTable() 
{
	if (checkvalidSearch() == true)
	{
		document.getElementById("Loading").innerHTML = "<IMG SRC='images/loading.gif'> Calculating...";
		
		var cboCompany = document.getElementById('_ctl0_EmpHeaderSearch1_cboCompany').value;
		var cboLevel1 = document.getElementById('_ctl0_EmpHeaderSearch1_cboLevel1').value;
		var cboLevel2 = document.getElementById('_ctl0_EmpHeaderSearch1_cboLevel2').value;
		var cboLevel3 = document.getElementById('_ctl0_EmpHeaderSearch1_cboLevel3').value;
		var txtEmpID = document.getElementById('_ctl0_EmpHeaderSearch1_txtEmpID').value;
		var txtEmpName = document.getElementById('_ctl0_EmpHeaderSearch1_txtEmpName').value;
		var txtMonth = document.getElementById('_ctl0_txtMonth').value;
		var AcountLogin = document.getElementById('txtAcountLogin').value;

		PayrollCollection.GetData(txtMonth,cboCompany,cboLevel1,cboLevel2,cboLevel3,txtEmpID,txtEmpName,"EN",AcountLogin,FillTable_CallBack);  
	}
}

function FillTable_CallBack(response) 
{ 
	var dt = response.value; 
	var txtMonth = document.getElementById('_ctl0_txtMonth').value;
	var AcountLogin = document.getElementById('txtAcountLogin').value;
	
	if(dt!=null && typeof(dt) == "object" && dt!=null) 
	{
		var s = new Array();
		s[s.length] = "<table width='100%' cellspacing='1' cellpadding='1' bordercolor='#999999' border='1' style='border-color:#999999;font-family:Arial;border-collapse:collapse;'>";
		s[s.length] = "<tr class=gridHeader align=Center>"; 
		s[s.length] = "<td align=Center>No.</td><td>Emp Code</td><td >Full Name</td><td >Company</td><td>Status Calculate</td>"; 
		s[s.length] = "</tr>"; 
		for(var i=0;i<dt.Rows.length;i++) 
		{
			s[s.length] = "<tr>"; 
			s[s.length] = "<td class=GridItem align=Center>" + (i + 1) + "</td>";  
			s[s.length] = "<td class=GridItem align=Center>" + dt.Rows[i].EmpCode + "</td>";  
			s[s.length] = "<td class=GridItem>" + dt.Rows[i].EmpName + "</td>"; 
			s[s.length] = "<td class=GridItem>" + dt.Rows[i].Level1+  "</td>"; 
			//s[s.length] = "<td class=GridItem>" + dt.Rows[i].BasicSalary + "</td>"; 
			//s[s.length] = "<td class=GridItem>" + dt.Rows[i].TotalIncome + "</td>"; 
			s[s.length] = "<td class=GridItem>" + PayrollCollection.CalPayroll(txtMonth,dt.Rows[i].EmpCode,AcountLogin).value + "</td>"; 
			//s[s.length] = "<td align='Center'><input type='text' readonly='readonly' class='input' style=width:40px;text-align:center' value='" + dt.Rows[i].Time5 + "' /></td>"; 
			//s[s.length] = "<td align='Center'><input type='text' readonly='readonly' class='input' style=width:40px;text-align:center' value='" + dt.Rows[i].Time6 + "' /></td>"; 
			s[s.length] = "</tr>";  
		}
		s[s.length] = "</table>"; 
		document.getElementById("Loading").innerHTML = "";
		document.getElementById("Loading").innerHTML = s.join(""); 	 
	}	
	
	
}
*/
function ViewTable() 
{
	if (checkvalidSearch() == true)
	{
		
		document.getElementById("Loading").innerHTML = "<IMG SRC='images/loading.gif'> Loading...";
		
		var cboCompany = document.getElementById('_ctl0_EmpHeaderSearch1_cboCompany').value;
		var cboLevel1 = document.getElementById('_ctl0_EmpHeaderSearch1_cboLevel1').value;
		var cboLevel2 = document.getElementById('_ctl0_EmpHeaderSearch1_cboLevel2').value;
		var cboLevel3 = document.getElementById('_ctl0_EmpHeaderSearch1_cboLevel3').value;
		var txtEmpID = document.getElementById('_ctl0_EmpHeaderSearch1_txtEmpID').value;
		var txtEmpName = document.getElementById('_ctl0_EmpHeaderSearch1_txtEmpName').value;
		var txtMonth = document.getElementById('_ctl0_txtMonth').value;
		var AcountLogin = document.getElementById('txtAcountLogin').value;

		PayrollCollection.GetData(txtMonth,cboCompany,cboLevel1,cboLevel2,cboLevel3,txtEmpID,txtEmpName,"EN",AcountLogin,ViewTable_CallBack);  
	}
}

function ViewTable_CallBack(response) 
{ 
	var dt = response.value; 
	
	if(dt!=null && typeof(dt) == "object" && dt!=null) 
	{
		var s = new Array();
		s[s.length] = "<table width='100%' cellspacing='1' cellpadding='1' bordercolor='#999999' border='1' style='border-color:#999999;font-family:Arial;border-collapse:collapse;'>";
		s[s.length] = "<tr class=gridHeader align=Center>"; 
		s[s.length] = "<td align=Center>No.</td><td>Emp Code</td><td >Full Name</td><td >Company</td><td>BasicSalary</td><td>Actual Salary</td><td>Total Income</td><td>Total Cost to Emp</td>"; 
		s[s.length] = "</tr>"; 
		for(var i=0;i<dt.Rows.length;i++) 
		{
			s[s.length] = "<tr>"; 
			s[s.length] = "<td class=GridItem align=Center>" + (i + 1) + "</td>";  
			s[s.length] = "<td class=GridItem align=Center>" + dt.Rows[i].EmpCode + "</td>";  
			s[s.length] = "<td class=GridItem>" + dt.Rows[i].EmpName + "</td>"; 
			s[s.length] = "<td class=GridItem>" + dt.Rows[i].Level1+  "</td>"; 
			s[s.length] = "<td class=GridItem align=right>" + dt.Rows[i].BasicSalary + "</td>"; 
			s[s.length] = "<td class=GridItem align=right>" + dt.Rows[i].ActualSalary + "</td>"; 
			s[s.length] = "<td class=GridItem align=right>" + dt.Rows[i].TotalIncome + "</td>"; 
			s[s.length] = "<td class=GridItem align=right>" + dt.Rows[i].TotalCost + "</td>"; 
			s[s.length] = "</tr>";  
		}
		s[s.length] = "</table>"; 
		document.getElementById("Loading").innerHTML = "";
		document.getElementById("Loading").innerHTML = s.join(""); 	 
	}	
	
	
}
// Status Indicator functions   
function ShowControl(c)
{
	//get the indicator
	var oElem = getElem(c);
	if (oElem == null) return;
	
	//show the loading indicator
	oElem.style.display = "block";
}

function HideControl(c)
{
	var oElem = getElem(c);
	if (oElem == null) return;
	oElem.style.display = "none";
} 
function getElem(Jb)
{
    return document.getElementById(Jb);
}
</SCRIPT>
<div id="Waiting" style="DISPLAY: none"><p class="nav-loading"><img src='images/AJAX/spinner.gif' class='IMG'>
		Please Waiting ...</p>
</div>
