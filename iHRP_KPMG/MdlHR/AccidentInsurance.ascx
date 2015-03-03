<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../Include/EmpHeaderSearch.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="AccidentInsurance.ascx.cs" Inherits="iHRPCore.MdlHR.AccidentInsurance" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="97%" border="0">
	<TR>
		<TD align="left" width="100%"><asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center"><uc1:empheadersearch id="EmpHeaderSearch1" runat="server"></uc1:empheadersearch></TD>
	</TR>
	<TR>
		<TD align="center"><!-- start detail for input form -->
			<TABLE cellSpacing="0" cellPadding="1" width="99%" border="0">
				<TR>
					<TD colSpan="5">
						<HR align="center" width="95%">
					</TD>
				</TR>
				<TR>
					<TD><asp:label id="lblEmpID" runat="server" CssClass="labelRequire" Width="100%"> EmpID</asp:label></TD>
					<TD><asp:textbox id="txtTEmpID" runat="server" CssClass="input" Width="101" MaxLength="30" AutoPostBack="True"></asp:textbox>&nbsp;<INPUT class="search" id="btnSearch" onclick="javascript:OpenWindowEmp('EmpID')" type="button"
							value="..." name="btnSearch"></TD>
					<TD colspan="3"></TD>
				</TR>
				<TR>
					<TD width="15%"><asp:label id="lblName" runat="server" CssClass="label" Width="100%"> Name</asp:label></TD>
					<TD width="20%"><asp:textbox id="txtEmpName" runat="server" CssClass="input" Width="186px" MaxLength="10" Enabled="False"></asp:textbox>&nbsp;</TD>
					<TD width="17%"></TD>
					<TD width="17%"><asp:label id="lblDivision" runat="server" CssClass="label" Width="100%"> Division</asp:label></TD>
					<TD width="35%"><asp:textbox id="txtDivision" runat="server" CssClass="input" Width="101px" MaxLength="50" Enabled="False"></asp:textbox></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblCost" runat="server" CssClass="label" Width="100%"> Cost</asp:label></TD>
					<TD><asp:textbox id="txtCost" runat="server" CssClass="input" Width="101px" MaxLength="10"></asp:textbox></TD>
					<TD></TD>
					<TD><asp:label id="lblSumMoney" runat="server" CssClass="label" Width="100%">Sum of Money</asp:label></TD>
					<TD><asp:textbox id="txtSumMoney" runat="server" CssClass="input" Width="101px" MaxLength="10"></asp:textbox></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblType" runat="server" CssClass="label" Width="100%">Type of Accident</asp:label></TD>
					<TD noWrap><asp:radiobutton id="optLabour" runat="server" Text="Labour" GroupName="a"></asp:radiobutton>&nbsp;&nbsp;&nbsp;
						<asp:radiobutton id="optTraffic" runat="server" Text="Traffic" GroupName="a"></asp:radiobutton></TD>
					<TD></TD>
					<TD><asp:label id="lblAccidentDate" runat="server" CssClass="label" Width="100%">Acc Date</asp:label></TD>
					<TD noWrap><asp:textbox id="txtAccDate" onblur="JavaScript:CheckDate(this)" runat="server" CssClass="input"
							Width="102" MaxLength="10"></asp:textbox>&nbsp;<INPUT class="btnCal" onclick="ShowCalendar('_ctl0_txtAccDate')" type="button"></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblReason" runat="server" CssClass="label" Width="100%"> Reason</asp:label></TD>
					<TD vAlign="top" colSpan="4"><asp:textbox id="txtReason" style="POSITION: absolute" runat="server" CssClass="input" Width="100%"
							MaxLength="100"></asp:textbox></TD>
				</TR>
				<TR>
					<TD colSpan="5">
						<HR align="center" width="95%">
					</TD>
				</TR>
				<TR>
					<TD><asp:label id="lblFromDate" CssClass="label" runat="server" Width="100%"> From Date</asp:label></TD>
					<TD><asp:textbox id="txtFromDate" onblur="JavaScript:CheckDate(this)" CssClass="input" runat="server"
							Width="102" MaxLength="10"></asp:textbox>&nbsp;<INPUT class="btnCal" onclick="ShowCalendar('_ctl0_txtFromDate')" type="button"></TD>
					<TD><asp:label id="lblToDate" CssClass="label" runat="server" Width="100%"> To Date</asp:label></TD>
					<TD><asp:textbox id="txtToDate" onblur="JavaScript:CheckDate(this)" CssClass="input" runat="server"
							Width="102" MaxLength="10"></asp:textbox>&nbsp;<INPUT class="btnCal" onclick="ShowCalendar('_ctl0_txtToDate')" type="button"></TD>
					<TD></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblReportDate" runat="server" CssClass="label" Width="100%"> Report Date</asp:label></TD>
					<TD><asp:textbox id="txtReportDate" onblur="JavaScript:CheckDate(this)" runat="server" CssClass="input"
							Width="102" MaxLength="10"></asp:textbox>&nbsp;<INPUT class="btnCal" onclick="ShowCalendar('_ctl0_txtReportDate')" type="button"></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
				</TR>
			</TABLE>
			<asp:textbox id="txtType" style="DISPLAY: none" runat="server" Width="10px"></asp:textbox></TD>
	</TR>
	<TR>
		<TD vAlign="top" noWrap align="center" height="20">
			<HR align="center" width="95%">
		</TD>
	</TR>
	<TR>
		<TD noWrap align="center">
			<TABLE id="tblButton" cellSpacing="0" cellPadding="0" width="99%" border="0">
				<TR vAlign="top" height="30">
					<TD vAlign="middle" width="15%"><asp:checkbox id="chkShowGrid" accessKey="G" onclick="javascript:collapse('_ctl0_trGrid');" runat="server"
							CssClass="checkbox" Text="Show grid" Checked="True" ToolTip="Alt+G"></asp:checkbox></TD>
					<TD align="center" width="*"><asp:linkbutton id="btnAddNew" accessKey="N" runat="server" CssClass="btnAddnew" ToolTip="Alt+N">Thêm mới</asp:linkbutton>&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnSave" accessKey="S" runat="server" CssClass="btnSave" ToolTip="ALT+S">Lưu</asp:linkbutton>&nbsp;&nbsp;
						<asp:linkbutton id="btnList" accessKey="L" runat="server" CssClass="btnList" ToolTip="Alt + L">Danh sách</asp:linkbutton>&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnDelete" accessKey="D" runat="server" CssClass="btnDelete" ToolTip="Alt + D">Xoá</asp:linkbutton>&nbsp;&nbsp;
						<asp:linkbutton id="btnPrint" accessKey="P" runat="server" CssClass="btnPrint" ToolTip="Alt + P">In</asp:linkbutton></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR id="trGrid" runat="server">
		<TD align="center"><!-- start grid for input form -->
			<TABLE id="tblGrid" cellSpacing="0" cellPadding="1" width="98%" border="0">
				<!--				<TR>
					<TD width="40%" style="HEIGHT: 7px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
					<TD align="right" width="*" style="HEIGHT: 8px">sasa&nbsp;&nbsp;&nbsp;</TD>
				</TR>-->
				<TR id="trGrid"> <!-- start grid detail for input form -->
					<TD colSpan="2"><asp:datagrid id="GrdAccInsurance" runat="server" CssClass="grid" Width="100%" AllowSorting="True"
							AutoGenerateColumns="False" CellPadding="0" BorderWidth="1px" BorderColor="#3366CC" BackColor="White"
							AllowPaging="True">
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
							<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="&lt;b&gt;Seq&lt;/b&gt;">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<%# Container.ItemIndex + 1 + GrdAccInsurance.PageSize*GrdAccInsurance.CurrentPageIndex%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="AccInsuranceID" HeaderText="Insuarance" Visible="False">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Emp Code">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton Runat="server" ID="btn" CssClass="hLink" CommandName="Edit">
											<%# DataBinder.Eval(Container, "DataItem.EmpID")%>
										</asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Cost" HeaderText="Cost">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="SumMoney" HeaderText="Sum Money">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Type" HeaderText="Type">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="AccDate" HeaderText="Accident Date">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Select">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
						</asp:datagrid><asp:textbox id="txtAccInsuranceID" style="DISPLAY: none" runat="server" Width="8px"></asp:textbox></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>
<script language="javascript">
function validform()
{
	if(checkisnull('txtTEmpID')==false) 
		return false;
	if(document.getElementById('_ctl0_txtAccDate').value!="")
	{
		if(FromSmallNow(document.getElementById('_ctl0_txtAccDate')) == false)
		{
			alert('Ngày bị tai nạn phải trước ngày hiện tại');
			document.getElementById('_ctl0_txtAccDate').focus();
			return false;
		}		
	}	
	return true;
}
function checkdelete()
{
	if(GridCheck('_ctl0_GrdAccInsurance',3,1,'chkSelect')==false)
	{
		alert('Vui lòng chọn tối thiểu 1 dòng trước khi xoá');
		return false;
	}
	if(confirm('Các dòng được đánh dấu sẽ bị xoá?')==false)
	{
		return false;
	}
	
}
function checkisnull(obj)
	{
		if(document.getElementById('_ctl0_' + obj).value=="")
			{
				alert('Vui lòng điền đầy đủ dữ liệu yêu cầu!');
				document.getElementById('_ctl0_' + obj).focus();
				return false;
			}
		else
			return true;
	}
function OpenWindowEmp(strField)
{
	ShowDialog('FormPage.aspx?Ascx=MdlHR/EmpPopup.ascx&Params=7');
}
	
function ReturnEmpPopUp7(strEmpID, strEmpCode, strEmpName, strStartDate, strLastName, strFirstName, strLevel1Name)
{
	document.getElementById("_ctl0_txtTEmpID").value = strEmpID;
	document.getElementById("_ctl0_txtEmpName").value = strEmpName;
	if (strLevel1Name!="&nbsp;")
		document.getElementById("_ctl0_txtDivision").value = strLevel1Name;
}	
function ShowReport()
{
	if (document.getElementById('_ctl0_txtFromDate').value=="")
	{
		alert("Vui lòng nhập ngày bắt đầu xem");
		document.getElementById('_ctl0_txtFromDate').focus();
		return false;  
	}
	if (document.getElementById('_ctl0_txtToDate').value=="")
	{
		alert("Vui lòng nhập ngày kết thúc xem");
		document.getElementById('_ctl0_txtToDate').focus();
		return false;  
	}
	if(document.getElementById('_ctl0_txtFromDate')&& document.getElementById('_ctl0_txtToDate')) 
	{
		var objFromDate=document.getElementById('_ctl0_txtFromDate');
		var objToDate=document.getElementById('_ctl0_txtToDate');
		if (IsSmaller(objFromDate.value,objToDate.value)==false)
		{
		alert("Ngày bắt đầu xem phải nhỏ hơn ngày kết thúc xem");
		document.getElementById('_ctl0_txtFromDate').focus();
		return false;  
		}
	}
	var optStatus = 0;
	if (document.getElementById("_ctl0_EmpHeaderSearch1_optStatus_0").checked == true)
		optStatus = 1;
	else if (document.getElementById("_ctl0_EmpHeaderSearch1_optStatus_1").checked == true)
		optStatus = 2;
	/*if (document.getElementById("_ctl0_EmpHeaderSearch1_optStatus_2").checked == true)
		optStatus = 3;*/
	var str_url = "Reports/ShowReports.aspx?pRptName=HR/HR_rpt15AccInsurance.rpt&pStrPara=@Language;@Status;@ReportDate;@EmpID;@EmpName;@CompanyID;@Level1ID;@Level2ID;@Level3ID;@PositionID;@JobCodeID;@LocationID;@FromDate;@ToDate&pStrValue="
	+ trim('<%=strLanguage%>') 
	+ ";" + optStatus
	+ ";" + trim(document.getElementById("_ctl0_txtReportDate").value) 
	+ ";" + trim(document.getElementById("_ctl0_EmpHeaderSearch1_txtEmpID").value) 
	+ ";" + trim(document.getElementById("_ctl0_EmpHeaderSearch1_txtEmpName").value) 
	+ ";" + trim(document.getElementById("_ctl0_EmpHeaderSearch1_cboCompany").value) 
	+ ";" + trim(document.getElementById("_ctl0_EmpHeaderSearch1_cboLevel1").value) 
	+ ";" + trim(document.getElementById("_ctl0_EmpHeaderSearch1_cboLevel2").value) 
	+ ";" + trim(document.getElementById("_ctl0_EmpHeaderSearch1_cboLevel3").value) 
	+ ";" + trim(document.getElementById("_ctl0_EmpHeaderSearch1_cboPosition").value)
	+ ";" + trim(document.getElementById("_ctl0_EmpHeaderSearch1_cboJobcode").value) 
	+ ";" + trim(document.getElementById("_ctl0_EmpHeaderSearch1_cboLocation").value)
	+ ";" + trim(document.getElementById("_ctl0_txtFromDate").value)
	+ ";" + trim(document.getElementById("_ctl0_txtToDate").value);
	ShowDialog(str_url)
	return false; 
	}

</script>

