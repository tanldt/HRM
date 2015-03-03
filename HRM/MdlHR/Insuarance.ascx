<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../Include/EmpHeaderSearch.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ColumnList" Src="../Include/ColumnList.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Insuarance.ascx.cs" Inherits="iHRPCore.MdlHR.Insuarance" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="97%" border="0">
	<TR>
		<TD align="left" width="100%"><asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center"><uc1:empheadersearch id="EmpHeaderSearch1" runat="server"></uc1:empheadersearch></TD>
	</TR>
	<TR>
		<TD align="center"><!-- start detail for input form -->
			<TABLE cellSpacing="3" cellPadding="2" width="99%" border="0">
				<TR>
					<TD width="10%"><asp:label id="lblFromDate" CssClass="labelrequire" runat="server" Width="100%">From Date</asp:label></TD>
					<TD width="20%"><asp:textbox id="txtFromDate" onblur="JavaScript:CheckDate(this)" CssClass="input" runat="server"
							Width="62px" MaxLength="10"></asp:textbox>&nbsp;<INPUT class=btnCal onclick="javascript:popUpCalendar(<%= this.txtFromDate.ClientID %>)" type=button></TD>
					<TD width="5%"></TD>
					<TD width="10%"><asp:label id="lblToDate" CssClass="labelrequire" runat="server" Width="100%">To Date</asp:label></TD>
					<TD width="20%"><asp:textbox id="txtToDate" onblur="JavaScript:CheckDate(this)" CssClass="input" runat="server"
							Width="62px" MaxLength="10"></asp:textbox>&nbsp;<INPUT class=btnCal onclick="javascript:popUpCalendar(<%= this.txtToDate.ClientID %>)" tabIndex=0 type=button></TD>
					<TD width="5%"></TD>
					<TD width="14%"><asp:label id="lblMoney" CssClass="label" runat="server" Width="100%">Insuarance Money</asp:label></TD>
					<TD noWrap width="16%"><asp:dropdownlist id="cboInsMoney" CssClass="select" runat="server" Width="92px"></asp:dropdownlist></TD>
				</TR>
			</TABLE> <!-- end button for input form --><INPUT id="txtQualificationID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1"
				name="txtQualificationID" runat="server">
			<asp:textbox id="txtFlag" style="DISPLAY: none" runat="server" Width="6px"></asp:textbox></TD>
	</TR>
	<TR>
		<TD align="center"><asp:checkbox id="chkNotSetup" CssClass="checkbox" Checked="True" runat="server" Width="89px"
				Text="Not Setup"></asp:checkbox>&nbsp;&nbsp;&nbsp;
			<asp:checkbox id="chkPropose" CssClass="checkbox" runat="server" Width="89px" Text="Proposed"></asp:checkbox>&nbsp;&nbsp;&nbsp;
			<asp:checkbox id="chkBought" CssClass="checkbox" runat="server" Width="89px" Text="Bought"></asp:checkbox>&nbsp;&nbsp;&nbsp;
			<asp:checkbox id="chkAll" CssClass="checkbox" runat="server" Width="89px" Text="All"></asp:checkbox></TD>
	</TR>
	<TR>
		<TD style="HEIGHT: 3px" vAlign="top" noWrap align="center" height="3">
			<HR align="center" width="95%">
			<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="99%" border="0">
				<TR>
					<TD width="12%"><asp:label id="Label7" CssClass="label" runat="server" Width="100%">Số hợp đồng:</asp:label></TD>
					<TD><asp:textbox id="txtContractNo" CssClass="input" runat="server" Width="210px" MaxLength="100"
							Enabled="True"></asp:textbox></TD>
					<TD width="1%"></TD>
					<TD width="10%"></TD>
					<TD width="30%"></TD>
				</TR>
				<TR>
					<TD width="12%"><asp:label id="Label1" CssClass="label" runat="server" Width="100%">Title</asp:label></TD>
					<TD width="30%" colSpan="4"><asp:textbox id="txtTitle" CssClass="input" runat="server" Width="99%" MaxLength="100" Enabled="True"></asp:textbox></TD>
				</TR>
				<TR>
					<TD width="12%"><asp:label id="Label4" CssClass="label" runat="server" Width="100%">Text 3</asp:label></TD>
					<TD width="30%"><asp:textbox id="txtText3" CssClass="input" runat="server" Width="210px" MaxLength="100" Enabled="True"></asp:textbox></TD>
					<TD></TD>
					<TD><asp:label id="Label5" CssClass="label" runat="server" Width="100%">Text 4</asp:label></TD>
					<TD><asp:textbox id="txtText4" CssClass="input" runat="server" Width="208px" MaxLength="100" Enabled="True"></asp:textbox></TD>
				</TR>
				<TR>
					<TD><asp:label id="Label2" CssClass="label" runat="server" Width="100%">Text 1</asp:label></TD>
					<TD><asp:textbox id="txtText1" CssClass="input" runat="server" Width="210px" MaxLength="100" Enabled="True"></asp:textbox></TD>
					<TD></TD>
					<TD><asp:label id="Label6" CssClass="label" runat="server" Width="100%">Report Date</asp:label></TD>
					<TD><asp:textbox id="txtReportDate" onblur="JavaScript:CheckDate(this)" tabIndex="2" CssClass="input"
							runat="server" Width="102px" MaxLength="10"></asp:textbox>&nbsp;<INPUT class=btnCal onclick="javascript:popUpCalendar(<%= this.txtReportDate.ClientID %>)" tabIndex=3 type=button></TD>
				</TR>
				<TR>
					<TD><asp:label id="Label3" CssClass="label" runat="server" Width="100%">Text 2</asp:label></TD>
					<TD colSpan="4"><asp:textbox id="txtText2" CssClass="input" runat="server" Width="99%" MaxLength="200" Enabled="True"></asp:textbox></TD>
				</TR>
			</TABLE>
			<HR align="center" width="95%">
			&nbsp;&nbsp;&nbsp;</TD>
	</TR>
	<TR style="DISPLAY: none">
		<TD vAlign="top" noWrap align="center" height="20"><asp:textbox id="txtTEmpID" style="DISPLAY: none" runat="server" Width="11px"></asp:textbox><asp:textbox id="txtTEmpName" style="DISPLAY: none" runat="server" Width="11px"></asp:textbox><asp:textbox id="txtCompanyID" style="DISPLAY: none" runat="server" Width="10px"></asp:textbox><asp:textbox id="txtLevel1ID" style="DISPLAY: none" runat="server" Width="9px"></asp:textbox><asp:textbox id="txtLevel2ID" style="DISPLAY: none" runat="server" Width="9px"></asp:textbox><asp:textbox id="txtLevel3ID" style="DISPLAY: none" runat="server" Width="10px"></asp:textbox><asp:textbox id="txtPositionID" style="DISPLAY: none" runat="server" Width="9px"></asp:textbox><asp:textbox id="txtJobcodeID" style="DISPLAY: none" runat="server" Width="8px"></asp:textbox><asp:textbox id="txtLocationID" style="DISPLAY: none" runat="server" Width="9px"></asp:textbox><asp:textbox id="txtTStatus" style="DISPLAY: none" runat="server" Width="9px"></asp:textbox></TD>
	</TR>
	<TR>
		<TD noWrap align="center">
			<TABLE id="tblButton" cellSpacing="0" cellPadding="0" width="99%" border="0">
				<TR vAlign="top" height="30">
					<TD vAlign="middle" width="15%"><asp:checkbox id="chkShowGrid" accessKey="G" onclick="javascript:collapse('tblGrid');" CssClass="checkbox"
							runat="server" Text="Show grid" Checked="True" ToolTip="Alt+G"></asp:checkbox></TD>
					<TD align="center" width="*"><asp:linkbutton id="btnSearch" accessKey="V" CssClass="btnList" runat="server" ToolTip="Alt+S">Tìm kiếm</asp:linkbutton>&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnSave" accessKey="S" CssClass="btnSave" runat="server" ToolTip="ALT+S">Lưu</asp:linkbutton>&nbsp;&nbsp;
						<asp:linkbutton id="btnDelete" accessKey="D" CssClass="btnDelete" runat="server" ToolTip="Alt + D">Xoá</asp:linkbutton>&nbsp;&nbsp;
						<asp:linkbutton id="btnExport" accessKey="E" CssClass="btnExport" runat="server" ToolTip="Alt+E, Xuất dl trên lưới ra file Exel">Xuất dl</asp:linkbutton>&nbsp;&nbsp;
						<asp:linkbutton id="btnPrint" accessKey="P" runat="server" CssClass="btnPrint" ToolTip="Alt + D">In</asp:linkbutton></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD noWrap align="left">
			<uc1:ColumnList id="uctrlColumns" runat="server"></uc1:ColumnList></TD>
	</TR>
	<TR id="trGrid" runat="server">
		<TD align="center"><!-- start grid for input form -->
			<TABLE id="tblGrid" cellSpacing="0" cellPadding="1" width="98%" border="0">
				<!--				<TR>
					<TD width="40%" style="HEIGHT: 7px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
					<TD align="right" width="*" style="HEIGHT: 8px">sasa&nbsp;&nbsp;&nbsp;</TD>
				</TR>-->
				<TR> <!-- start grid detail for input form -->
					<TD colSpan="2"><asp:datagrid id="GrdInsuarance" runat="server" CssClass="grid" Width="100%" AllowPaging="True"
							BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False"
							AllowSorting="True">
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
							<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="&lt;b&gt;Seq&lt;/b&gt;">
									<HeaderStyle Width="2%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<%# Container.ItemIndex + 1 + GrdInsuarance.PageSize*GrdInsuarance.CurrentPageIndex%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="InsuranceID" HeaderText="Insuarance" Visible="False">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="EmpID" HeaderText="Emp ID" Visible="False">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="EmpCode" HeaderText="Emp Code">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="EmpName" HeaderText="Emp Name">
									<HeaderStyle Width="16%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="BirthDate" HeaderText="Year of birth">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PositionCode" HeaderText="Position">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="SectionCode" HeaderText="Department">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="InsuranceMoney" HeaderText="Insurance Rate">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText=" Is Proposed">
									<HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox id="chkIsProposed" runat="server" CssClass="checkbox"></asp:CheckBox>
										<asp:TextBox id="txtIsProposed1" style="WIDTH: 20px; HEIGHT: 22px; DISPLAY:none; " runat="server" value ='<%# DataBinder.Eval(Container, "DataItem.Pro") %>'>
										</asp:TextBox>
										<INPUT id="txtIsProposed" value='<%# DataBinder.Eval(Container, "DataItem.Pro") %>' style="WIDTH: 27px; HEIGHT: 22px" type="hidden" size="1" runat="server" NAME="Hidden1">
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Has bought">
									<HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox id="chkIsBought" runat="server" CssClass="checkbox"></asp:CheckBox>
										<asp:TextBox id="txtIsBought1" style="WIDTH: 20px; HEIGHT: 22px; DISPLAY:none; " runat="server" value ='<%# DataBinder.Eval(Container, "DataItem.Buy") %>'>
										</asp:TextBox>
										<INPUT id="txtIsBought" value='<%# DataBinder.Eval(Container, "DataItem.Buy") %>' style="WIDTH: 27px; HEIGHT: 22px" type="hidden" size="1" runat="server" NAME="txtIsBought">
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Select">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<HeaderTemplate>
										<asp:CheckBox id="chkSelectAll" onclick="CheckAll('_ctl0_GrdInsuarance__ctl2_chkSelectAll','_ctl0_GrdInsuarance',3,1,'chkSelect')" 
 runat="server" CssClass="gridFooter"></asp:CheckBox>
									</HeaderTemplate>
									<ItemTemplate>
										<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>
<script language="javascript">
function FillCheck()
{
	/*if(document.getElementById('_ctl0_chkAll').checked == true)
	{
		document.getElementById('_ctl0_chkPropose').checked = true;
		document.getElementById('_ctl0_chkBought').checked = true;
	}
	else
	{
		document.getElementById('_ctl0_chkPropose').checked = false;
		document.getElementById('_ctl0_chkBought').checked = false;
	}*/
}
function checkvalidSave()
{
	if(GridCheck('_ctl0_GrdInsuarance',3,1,'chkSelect')==false)
		{
			GetAlertError(iTotal,DSAlert,"0049");		
			return false;
	}
	if(checkisnull('txtFromDate')==false) return false;
	if(checkisnull('txtToDate')==false) return false;
	if(document.getElementById('_ctl0_chkPropose').checked==false && document.getElementById('_ctl0_chkBought').checked==false)
	{
		//alert('Vui lòng chọn lưu danh sách đề xuất hay danh sách đã mua');
		GetAlertError(iTotal,DSAlert,"Ins_0004");
		return false;
	}
	if(FromSmallToDate(document.getElementById('_ctl0_txtFromDate'),document.getElementById('_ctl0_txtToDate')) == false)
		{
			//alert('Ngày bắt đầu phải trước ngày kết thúc');
			GetAlertError(iTotal,DSAlert,"Ins_0003");
			document.getElementById('_ctl0_txtToDate').focus();
			return false;
		}	
	if(document.getElementById('_ctl0_chkBought').checked == true)
	{
		if(checkisnull('cboInsMoney')==false) return false;
	}
	return true;
}
function checkvalidSearch()
{
	if(checkisnull('txtFromDate')==false) return false;
	if(checkisnull('txtToDate')==false) return false;	
	if(FromSmallToDate(document.getElementById('_ctl0_txtFromDate'),document.getElementById('_ctl0_txtToDate')) == false)
		{
			//alert('Ngày bắt đầu phải trước ngày kết thúc');
			GetAlertError(iTotal,DSAlert,"Ins_0003");
			document.getElementById('_ctl0_txtToDate').focus();
			return false;
		}	
	
	if (document.getElementById('_ctl0_chkNotSetup').checked==true)
		if (document.getElementById('_ctl0_chkPropose').checked==true||document.getElementById('_ctl0_chkBought').checked==true ||document.getElementById('_ctl0_chkAll').checked==true)
		{
		//alert("Vui lòng chỉ chọn loại tìm kiếm là : không thiết lập");
		GetAlertError(iTotal,DSAlert,"Ins_0002");
		return false;
		}
	if (document.getElementById('_ctl0_chkAll').checked==true)	
		if (document.getElementById('_ctl0_chkNotSetup').checked==true ||document.getElementById('_ctl0_chkPropose').checked==true||document.getElementById('_ctl0_chkBought').checked==true)
		{
		//alert("Vui lòng chỉ chọn loại tìm kiếm là : tất cả");
		GetAlertError(iTotal,DSAlert,"Ins_0001");
		return false;	
		}
	if (document.getElementById('_ctl0_chkPropose').checked==true)	
		if (document.getElementById('_ctl0_chkNotSetup').checked==true||document.getElementById('_ctl0_chkAll').checked==true)
		{
		//alert("Loại Đề xuất chỉ có thể kết hợp với loại Đã mua");
		GetAlertError(iTotal,DSAlert,"0093");
		return false;
		}
	if (document.getElementById('_ctl0_chkBought').checked==true)	
		if (document.getElementById('_ctl0_chkNotSetup').checked==true ||document.getElementById('_ctl0_chkAll').checked==true)
		{
		//alert("Loại Đề xuất chỉ có thể kết hợp với loại Đã mua");
		GetAlertError(iTotal,DSAlert,"0093");
		return false;
		}
	
	
	return true;		
}		

function checkdelete()
{
	if(GridCheck('_ctl0_GrdInsuarance',3,1,'chkSelect')==false)
	{			
		GetAlertError(iTotal,DSAlert,"0001");				
		return false;
	}
	if(confirm(GetAlertText(iTotal,DSAlert,"0002"))==false){
		return false;
	}	
}
function checkisnull(obj)
{
	if(document.getElementById('_ctl0_' + obj).value=="")
		{
			//alert('Vui lòng di?n d?y d? d? li?u yêu c?u!');
			GetAlertError(iTotal,DSAlert,"0003");
			document.getElementById('_ctl0_' + obj).focus();
			return false;
		}
	else
		return true;
}
function ShowReport()
{
	
	var optStatus = 0;
	if (document.getElementById('_ctl0_chkPropose').checked==false && document.getElementById('_ctl0_chkBought').checked==false) 
	{
		GetAlertError(iTotal,DSAlert,"0091");
		return false;
	}
	if (document.getElementById('_ctl0_chkPropose').checked==true && document.getElementById('_ctl0_chkBought').checked==true) 
	{
		//alert('Chỉ được chọn hoặc in những NV được đề xuất hoặc những NV đã tham gia BHTN');
		GetAlertError(iTotal,DSAlert,"0092");
		return false;
	}
	if (document.getElementById('_ctl0_chkNotSetup').checked==true || document.getElementById('_ctl0_chkAll').checked==true) 
	{
		//alert('Chỉ được chọn hoặc in những NV được đề xuất hoặc những NV đã tham gia BHTN');
		GetAlertError(iTotal,DSAlert,"0092");
		return false;
	}
	if(checkisnull('txtFromDate')==false) return false;
	if(checkisnull('txtToDate')==false) return false;
	if (document.getElementById('_ctl0_chkPropose').checked==true)
	{
		//if(checkisnull('cboInsMoney')==false) return false;
		if(checkisnull('txtTitle')==false) return false;
		if(checkisnull('txtText3')==false) return false;
		if(checkisnull('txtText4')==false) return false;
		if(checkisnull('txtText1')==false) return false;
		if(checkisnull('txtText2')==false) return false;
	}
	if (document.getElementById('_ctl0_chkBought').checked==true)
	{
		if(checkisnull('cboInsMoney')==false) return false;
		if(checkisnull('txtContractNo')==false) return false;
	}	
	if (document.getElementById("_ctl0_EmpHeaderSearch1_optStatus_0").checked == true)
		optStatus = 1;
	else if (document.getElementById("_ctl0_EmpHeaderSearch1_optStatus_1").checked == true)
		optStatus = 2;
	/*if (document.getElementById("_ctl0_EmpHeaderSearch1_optStatus_2").checked == true)
		optStatus = 3;*/
	if (document.getElementById('_ctl0_chkPropose').checked == true)
		var str_url = "Reports/ShowReports.aspx?pRptName=HR/HR_rpt13LifeInsuranceBefore.rpt&pStrPara=@Language;@Status;@FromDate;@ToDate;@EmpID;@EmpName;@CompanyID;@Level1ID;@Level2ID;@Level3ID;@PositionID;@JobCodeID;@LocationID;@InsMoney;@Title;@Text1;@Text2;@Text3;@Text4;@ReportDate&pStrValue=" 
		 + trim('<%=strLanguage%>') 
		 + ";" + optStatus 
		 + ";" + trim(document.getElementById("_ctl0_txtFromDate").value) 
		 + ";" + trim(document.getElementById("_ctl0_txtToDate").value) 
		 + ";" + trim(document.getElementById("_ctl0_EmpHeaderSearch1_txtEmpID").value) 
		 + ";" + trim(document.getElementById("_ctl0_EmpHeaderSearch1_txtEmpName").value) 
		 + ";" + trim(document.getElementById("_ctl0_EmpHeaderSearch1_cboCompany").value) 
		 + ";" + trim(document.getElementById("_ctl0_EmpHeaderSearch1_cboLevel1").value) 
		 + ";" + trim(document.getElementById("_ctl0_EmpHeaderSearch1_cboLevel2").value) 
		 + ";" + trim(document.getElementById("_ctl0_EmpHeaderSearch1_cboLevel3").value) 
		 + ";" + trim(document.getElementById("_ctl0_EmpHeaderSearch1_cboPosition").value) 
		 + ";" //+ trim(document.getElementById("_ctl0_EmpHeaderSearch1_cboJobcode").value) 
		 + ";" + trim(document.getElementById("_ctl0_EmpHeaderSearch1_cboLocation").value) 
		 + ";" + trim(document.getElementById("_ctl0_cboInsMoney").value)
		 + ";" + trim(document.getElementById("_ctl0_txtTitle").value)
		 + ";" + trim(document.getElementById("_ctl0_txtText1").value) 
		 + ";" + trim(document.getElementById("_ctl0_txtText2").value)
		 + ";" + trim(document.getElementById("_ctl0_txtText3").value) 
		 + ";" + trim(document.getElementById("_ctl0_txtText4").value)
		 + ";" + trim(document.getElementById("_ctl0_txtReportDate").value);
	else if (document.getElementById('_ctl0_chkBought').checked == true)
		str_url = "Reports/ShowReports.aspx?pRptName=HR/HR_rpt14LifeInsuranceAfter.rpt&pStrPara=@Language;@Status;@FromDate;@ToDate;@EmpID;@EmpName;@CompanyID;@Level1ID;@Level2ID;@Level3ID;@PositionID;@JobCodeID;@LocationID;@InsMoney;@ReportDate;@ContractNo&pStrValue=" 
		+ trim('<%=strLanguage%>') 
		+ ";" + optStatus 
		+ ";" + trim(document.getElementById("_ctl0_txtFromDate").value) 
		+ ";" + trim(document.getElementById("_ctl0_txtToDate").value) 
		+ ";" + trim(document.getElementById("_ctl0_EmpHeaderSearch1_txtEmpID").value) 
		+ ";" + trim(document.getElementById("_ctl0_EmpHeaderSearch1_txtEmpName").value) 
		+ ";" + trim(document.getElementById("_ctl0_EmpHeaderSearch1_cboCompany").value) 
		+ ";" + trim(document.getElementById("_ctl0_EmpHeaderSearch1_cboLevel1").value) 
		+ ";" + trim(document.getElementById("_ctl0_EmpHeaderSearch1_cboLevel2").value) 
		+ ";" + trim(document.getElementById("_ctl0_EmpHeaderSearch1_cboLevel3").value) 
		+ ";" + trim(document.getElementById("_ctl0_EmpHeaderSearch1_cboPosition").value) 
		+ ";"// + trim(document.getElementById("_ctl0_EmpHeaderSearch1_cboJobcode").value)
		+ ";" + trim(document.getElementById("_ctl0_EmpHeaderSearch1_cboLocation").value) 
		+ ";" + trim(document.getElementById("_ctl0_cboInsMoney").value)
		+ ";" + trim(document.getElementById("_ctl0_txtReportDate").value) 
		+ ";" + trim(document.getElementById("_ctl0_txtContractNo").value) ;
	ShowDialog(str_url)
	return false; 
}

</script>

