<%@ Register TagPrefix="uc1" TagName="EmpHeader" Src="../Include/EmpHeader.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="SalaryGrade.ascx.cs" Inherits="iHRPCore.MdlPR.SalaryGrade" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Import Namespace ="System.Data.SqlClient" %>
<%@ Import Namespace ="System.Data" %>
<%@ Import Namespace ="iHRPCore.Com"%>
<script language="javascript">	
	function ShowExcelSelectPage()
	{
		//window.open('./MdlPR/ExcelFileSelect.aspx?ID=<%=strID%>','SelectFile','status=1,toolbar=0,scrollbars=1,resizable=0,alwaysRaised=Yes, top=200, left=300, width=460, height=150,1 ,align=center');
		window.open('./MdlPR/ExcelFileSelect_view.aspx?ID=<%=strID%>','SelectFile','status=1,toolbar=0,scrollbars=1,resizable=0,alwaysRaised=Yes, top=20, left=30, width=800, height=600,1 ,align=center');
		return false;
	}
</script>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="97%" border="0">
	<TR>
		<TD align="left" width="100%"><asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center"><uc1:empheader id="HR_EmpHeader" runat="server"></uc1:empheader></TD>
	</TR>
	<TR>
		<TD align="center"><asp:radiobutton id="optCB" CssClass="labelRequire" Width="151px" Text="Thông tin lương cơ bản" Runat="server"
				GroupName="BCCC" AutoPostBack="True" Font-Names="Arial" Font-Size="8pt"></asp:radiobutton><asp:radiobutton id="optCD" CssClass="labelRequire" Width="151px" Text="Thông tin lương chức danh"
				Runat="server" GroupName="BCCC" AutoPostBack="True" Font-Names="Arial" Font-Size="8pt"></asp:radiobutton></TD>
	</TR>
	<TR>
		<TD align="center">
			<!-- start detail for input form -->
			<TABLE id="Table2" cellSpacing="0" cellPadding="1" width="99%" border="0">
				<TR id="TrCB" runat="server">
					<TD colSpan="4">
						<table cellSpacing="0" cellPadding="1" width="100%" border="0">
							<TR>
								<TD width="12%"><asp:label id="Label1" CssClass="label" runat="server">Số QĐ CB</asp:label></TD>
								<TD width="28%"><asp:textbox id="txtNoQDCB" CssClass="input" runat="server" MaxLength="255" Width="100%"></asp:textbox></TD>
								<TD width="15%"></TD>
								<TD width="35%"></TD>
							</TR>
							<TR>
								<TD width="12%"><asp:label id="Label4" CssClass="labelRequire" runat="server"> Mã LG CB</asp:label></TD>
								<TD width="28%"><asp:dropdownlist id="cboMaLGCB" CssClass="select" runat="server" Width="100%" onChange="ChangeSalGradeCB()"></asp:dropdownlist></TD>
								<TD width="15%">&nbsp;&nbsp;<asp:label id="Label9" CssClass="labelRequire" runat="server">Hệ số CB</asp:label></TD>
								<TD width="35%"><asp:textbox id="txtHesoCB" CssClass="input" runat="server" MaxLength="255" Width="50%" ReadOnly="True"></asp:textbox></TD>
							</TR>
							<TR>
								<TD><asp:label id="Label5" CssClass="labelRequire" runat="server">Mốc nâng</asp:label></TD>
								<TD><asp:textbox id="txtNangCB" onblur="JavaScript:CheckDate(this)" CssClass="input" runat="server"
										MaxLength="10" Width="81"></asp:textbox>&nbsp;<INPUT class="btnCal" onclick="ShowCalendar('_ctl0_txtNangCB')" type="button"></TD>
								<TD>&nbsp;&nbsp;<asp:label id="Label2" CssClass="label" runat="server">Hệ số PCCL</asp:label></TD>
								<TD><asp:textbox id="txtPCCL" onblur="javascript:checkNumeric(this)" CssClass="input" runat="server"
										MaxLength="255" Width="50%"></asp:textbox></TD>
							</TR>
							<TR>
								<TD><asp:label id="Label3" CssClass="labelRequire" runat="server">Ngày hưởng</asp:label></TD>
								<TD><asp:textbox id="txtHuongCB" onblur="JavaScript:CheckDate(this)" CssClass="input" runat="server"
										MaxLength="10" Width="81"></asp:textbox>&nbsp;<INPUT class="btnCal" onclick="ShowCalendar('_ctl0_txtHuongCB')" type="button"></TD>
								<TD>&nbsp;&nbsp;<asp:label id="Label10" CssClass="label" runat="server">Hệ số PCCV</asp:label></TD>
								<TD><asp:textbox id="txtPCCV" onblur="javascript:checkNumeric(this)" CssClass="input" runat="server"
										MaxLength="255" Width="50%"></asp:textbox></TD>
							</TR>
						</table>
					</TD>
				</TR>
				<TR id="TrCD" runat="server">
					<TD colSpan="4">
						<table cellSpacing="0" cellPadding="1" width="100%" border="0">
							<TR>
								<TD width="12%"><asp:label id="Label12" CssClass="label" runat="server">Số QĐ CD</asp:label></TD>
								<TD width="28%"><asp:textbox id="txtNoQDCD" CssClass="input" runat="server" MaxLength="255" Width="100%"></asp:textbox></TD>
								<TD width="15%"></TD>
								<TD width="35%"></TD>
							</TR>
							<TR>
								<TD width="12%"><asp:label id="Label14" CssClass="labelRequire" runat="server"> Mã LG CD</asp:label></TD>
								<TD width="28%"><asp:dropdownlist id="cboMaLGCD" CssClass="select" runat="server" Width="100%" onChange="ChangeSalGradeCD()"></asp:dropdownlist></TD>
								<TD width="15%">&nbsp;&nbsp;<asp:label id="Label15" CssClass="labelRequire" runat="server">Hệ số CD</asp:label></TD>
								<TD width="35%"><asp:textbox id="txtHesoCD" CssClass="input" runat="server" MaxLength="255" Width="50%" ReadOnly="True"></asp:textbox></TD>
							</TR>
							<TR>
								<TD><asp:label id="Label16" CssClass="labelRequire" runat="server">Mốc nâng</asp:label></TD>
								<TD><asp:textbox id="txtNangCD" onblur="JavaScript:CheckDate(this)" CssClass="input" runat="server"
										MaxLength="10" Width="81"></asp:textbox>&nbsp;<INPUT class="btnCal" onclick="ShowCalendar('_ctl0_txtNangCD')" type="button"></TD>
								<TD>&nbsp;&nbsp;<asp:label id="Label17" CssClass="label" runat="server">Hệ số PCVK</asp:label></TD>
								<TD><asp:textbox id="txtPCVK" onblur="javascript:checkNumeric(this)" CssClass="input" runat="server"
										MaxLength="255" Width="50%"></asp:textbox></TD>
							</TR>
							<TR>
								<TD><asp:label id="Label18" CssClass="labelRequire" runat="server">Ngày hưởng</asp:label></TD>
								<TD><asp:textbox id="txtHuongCD" onblur="JavaScript:CheckDate(this)" CssClass="input" runat="server"
										MaxLength="10" Width="81"></asp:textbox>&nbsp;<INPUT class="btnCal" onclick="ShowCalendar('_ctl0_txtHuongCD')" type="button"></TD>
								<TD>&nbsp;&nbsp;<asp:label id="Label19" CssClass="label" runat="server">Hệ số PCTN</asp:label></TD>
								<TD><asp:textbox id="txtPCTN" onblur="javascript:checkNumeric(this)" CssClass="input" runat="server"
										MaxLength="255" Width="50%"></asp:textbox></TD>
							</TR>
							<TR>
								<TD><asp:label id="Label20" CssClass="label" runat="server">Lương khác</asp:label></TD>
								<TD><asp:textbox id="txtSalaryOther" onblur="JavaScript:checkNumeric(this)" CssClass="input" runat="server"
										MaxLength="13" Width="100%"></asp:textbox></TD>
								<TD>&nbsp;&nbsp;<asp:label id="Label21" CssClass="labelRequire" runat="server">% Được hưởng</asp:label></TD>
								<TD><asp:textbox id="txtDcHuong" onblur="javascript:DcHuongChange(this)" CssClass="input" runat="server"
										MaxLength="4" Width="50%"></asp:textbox></TD>
							</TR>
						</table>
					</TD>
				</TR>
				<TR>
					<TD width="13%"><asp:label id="Label6" CssClass="label" runat="server">Ghi chú</asp:label></TD>
					<TD vAlign="top" width="87%" colSpan="3"><asp:textbox id="txtNote" CssClass="input" runat="server" MaxLength="255" Width="100%"></asp:textbox></TD>
				</TR>
			</TABLE>
			<!-- end button for input form --></TD>
	</TR>
	<TR>
		<TD vAlign="top" noWrap align="center" height="20">
			<HR align="center" width="95%">
			<INPUT id="txtID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtID"
				runat="server">
		</TD>
	</TR>
	<!-- start button for input form -->
	<TR>
		<TD noWrap align="center">
			<TABLE id="tblButton" cellSpacing="0" cellPadding="0" width="99%" border="0">
				<TR vAlign="top" height="30">
					<TD vAlign="middle" width="15%"><asp:checkbox id="chkShowGrid" accessKey="G" onclick="javascript:collapse('_ctl0_trGrid');" CssClass="checkbox"
							runat="server" ToolTip="Alt+G" Checked="True" Text="Hiển thị lưới"></asp:checkbox></TD>
					<TD align="center" width="*"><asp:linkbutton id="btnAddNew" accessKey="N" CssClass="btnAddnew" runat="server" ToolTip="Alt+N">Thêm mới</asp:linkbutton>&nbsp;
						<asp:linkbutton id="btnSave" accessKey="S" CssClass="btnSave" runat="server" ToolTip="ALT+S">Lưu</asp:linkbutton>&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnDelete" accessKey="D" CssClass="btnDelete" runat="server" ToolTip="Alt + D">Xoá</asp:linkbutton>&nbsp;
						<asp:linkbutton id="btnImport" accessKey="I" CssClass="btnImport" runat="server" ToolTip="Alt+I, Import data from excel file same format with export file">Import</asp:linkbutton>&nbsp;&nbsp;
						<asp:linkbutton id="btnExport" accessKey="E" CssClass="btnExport" runat="server" ToolTip="Alt+E, Export data in grid to excel file">Export</asp:linkbutton></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<!-- end button for input form -->
	<TR id="trGrid" runat="server">
		<TD align="center"><!-- start grid for input form -->
			<TABLE id="tblGrid" cellSpacing="0" cellPadding="1" width="98%" border="0">
				<TR>
					<TD width="40%">&nbsp;
						<asp:label id="Label7" CssClass="label" runat="server">Số hàng/trang</asp:label>&nbsp;
						<asp:textbox onkeypress="checkKey()" id="txtPageRows" CssClass="input" runat="server" MaxLength="3"
							Width="35px">20</asp:textbox>&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:label id="Label13" CssClass="label" runat="server">Tổng hàng</asp:label>&nbsp;
						<asp:label id="lblTotalRows" CssClass="labelData" runat="server" Width="35px"></asp:label><INPUT id="btnRowNumber" style="DISPLAY: none" type="button" value="Row" name="btnRowNumber"
							runat="server"></TD>
					<TD align="right" width="*">&nbsp;&nbsp;&nbsp;
					</TD>
				</TR>
				<TR id="trGrid1"> <!-- start grid detail for input form -->
					<TD colSpan="2"><asp:datagrid id="grdSalaryGrade" CssClass="grid" runat="server" Width="100%" AllowPaging="True"
							BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False"
							AllowSorting="True" PageSize="20">
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle CssClass="gridHeader"></HeaderStyle>
							<FooterStyle CssClass="gridFooter"></FooterStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="STT">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<%# Container.ItemIndex + 1%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="M&#227; LG CB">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton Runat="server" Enabled="true" ID="btnEdit" CssClass="hLink" CommandName="EDIT">
											<%# DataBinder.Eval(Container, "DataItem.MaLGCB")%>
										</asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="NoQDCB" HeaderText="Số QĐ">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="HesoCB" HeaderText="Hệ số CB">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="NangCB" HeaderText="Mốc n&#226;ng">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="HuongCB" HeaderText="Ng&#224;y hưởng">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PCCL" HeaderText="Hệ số PCCL">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PCCV" HeaderText="Hệ số PCCV">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="M&#227; LG CD">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton Runat="server" Enabled="true" ID="btnEditCD" CssClass="hLink" CommandName="EDIT">
											<%# DataBinder.Eval(Container, "DataItem.MaLGCD")%>
										</asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="NoQDCD" HeaderText="Số QĐ">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="HesoCD" HeaderText="Hệ số CD">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="NangCD" HeaderText="Mốc n&#226;ng">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="HuongCD" HeaderText="Ng&#224;y hưởng">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PCVK" HeaderText="Hệ số PCVK">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PCTN" HeaderText="Hệ số PCTN">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="SalaryOther" HeaderText="Lương kh&#225;c">
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="DcHuong" HeaderText="% Được hưởng">
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Chọn">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE> <!-- end grid for input form --></TD>
	</TR>
</TABLE>
<SCRIPT language="javascript">
<!--
function validform(){
if (document.Form1("_ctl0_optCB").checked)
{
	if(checkisnull('cboMaLGCB')==false)  return false;
	if(checkisnull('txtHesoCB')==false)  return false;
	if(checkisnull('txtNangCB')==false)  return false;
	if(checkisnull('txtHuongCB')==false)  return false;
}
else
{
	if(checkisnull('cboMaLGCD')==false)  return false;
	if(checkisnull('txtHesoCD')==false)  return false;
	if(checkisnull('txtNangCD')==false)  return false;
	if(checkisnull('txtHuongCD')==false)  return false;
}
	return true;
}
function checkisnull(obj){
	if(document.getElementById('_ctl0_' + obj).value=="")
		{
			alert('Data not left blank, please input data!');
			document.getElementById('_ctl0_' + obj).focus();
			return false;
		}
	else
		return true;
}
function checkdelete(){
	if(GridCheck('_ctl0_grdSalaryGrade',3,1,'chkSelect')==false)
	{
		alert('Please select at least one record before delete');
		return false;
	}
	if(confirm('Delete checked records, are you sure?')==false){
		return false;
	}
}
function checkKey(){	
	if(window.event.keyCode==13){		
		document.getElementById('_ctl0_btnRowNumber').click();		
		event.returnValue=false;
		event.cancel = true;		
	}
}
function DcHuongChange(obj){
	checkNumeric(obj);
	/*
	if(trim(document.getElementById('_ctl0_txtCashPercent').value)=='')
		document.getElementById('_ctl0_txtCashPercent').value = '0';
	document.getElementById('_ctl0_txtFlexPercent').value = 100 - parseFloat(document.getElementById('_ctl0_txtCashPercent').value,10);
	*/
}
//-->
</SCRIPT>
