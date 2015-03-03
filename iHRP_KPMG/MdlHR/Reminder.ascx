<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../Include/EmpHeaderSearch.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Reminder.ascx.cs" Inherits="iHRPCore.MdlHR.Reminder" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="FPT" Namespace="FPTToolWeb.Control.DataGrids" Assembly="FPTToolWeb" %>
<%@ Register TagPrefix="uc1" TagName="ColumnList" Src="../Include/ColumnList.ascx" %>
<%@ Register TagPrefix="FPT1" Namespace="FPTToolWeb.Control.DataGrids" Assembly="FPTToolWeb" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="98%" border="0">
	<TR>
		<TD align="center"><asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center"><uc1:empheadersearch id="EmpHeaderSearch1" runat="server"></uc1:empheadersearch></TD>
	</TR>
	<TR>
		<TD vAlign="top" noWrap align="center" height="5">
			<HR width="97%">
		</TD>
	</TR>
	<TR>
		<TD noWrap align="center">
			<TABLE id="tblButton" cellSpacing="0" cellPadding="0" width="99%" border="0">
				<TR>
					<TD id="tdPages" noWrap colSpan="2"><asp:label id="Label2" CssClass="labelSubTitle" runat="server">Contract</asp:label>&nbsp;</TD>
				</TR>
				<TR>
					<TD noWrap colSpan="2">
						<HR width="97%">
					</TD>
				</TR>
				<TR>
					<TD noWrap align="center" colSpan="2"><asp:linkbutton id="btnSearch" accessKey="S" CssClass="button" runat="server" ToolTip="Alt+S">Search</asp:linkbutton>&nbsp;
						<asp:linkbutton visible="False" id="btnExtend" accessKey="L" CssClass="btnList" runat="server" ToolTip="Alt + X"
							CausesValidation="False">Extend</asp:linkbutton>&nbsp;
						<asp:linkbutton id="btnExport" accessKey="E" CssClass="button" runat="server" ToolTip="Alt+E, Export data in grid to excel file">Export</asp:linkbutton></TD>
				</TR>
				<TR>
					<TD noWrap align="center" colSpan="2"></TD>
				</TR>
			</TABLE>
		</TD>
	</TR> <!-- end button for input form -->
	<TR>
		<TD align="center"><!-- start grid for input form -->
			<TABLE id="tblGrid" cellSpacing="0" cellPadding="1" width="99%" border="0">
				<TR id="trGrid" align="center"> <!-- start grid detail for input form -->
					<TD><asp:datagrid id="dtgListContract" CssClass="grid" runat="server" BorderColor="#3366CC" BackColor="White"
							Width="100%" AllowSorting="True" AutoGeneratecolumns="False" CellPadding="0" BorderWidth="1px"
							PageSize="15">
							<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="ContractID" HeaderText="ContractID"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Select">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox id="chkSelect" Checked="true" Runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn Visible="False" HeaderText="Seq">
									<HeaderStyle Width="30px"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<%# Container.ItemIndex + 1+ dtgListContract.PageSize* dtgListContract.CurrentPageIndex%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn Visible="True" DataField="EmpCode" HeaderText="Emp ID">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="ShortName" HeaderText="Short Name">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="EmpName" HeaderText="Full name">
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
									<ItemStyle HorizontalAlign="Center" ForeColor="Red" Width="80px"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ContractNo" HeaderText="Contract No">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD align="left"></TD>
	</TR>
	<TR>
		<TD align="left">
			<HR width="97%">
			<asp:label id="Label3" CssClass="labelSubTitle" runat="server">Birthday</asp:label>
			<HR width="97%">
		</TD>
	</TR>
	<TR>
		<TD align="left"></TD>
	</TR>
	<TR>
		<TD align="center">
			<TABLE id="tblBirth" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
				<TR>
					<TD align="center" width="2%">&nbsp;
						<asp:label id="Label7" Runat="server"> From date</asp:label>&nbsp;
						<asp:textbox id="txtFromDate" onblur="CheckddMM(this)" CssClass="input" Width="70px" Runat="server"></asp:textbox>&nbsp;
						<asp:label id="Label1" Runat="server">To date</asp:label>&nbsp;
						<asp:textbox id="txtToDate" onblur="CheckddMM(this)" CssClass="input" Width="70px" Runat="server"></asp:textbox>&nbsp;
						<asp:label id="Label8" Runat="server">Month</asp:label><asp:textbox id="txtMonthBirth" onblur="CheckMM2(this)" CssClass="input" runat="server" Width="75px"
							MaxLength="7"></asp:textbox>&nbsp;
						<asp:linkbutton id="btnView" accessKey="S" CssClass="button" runat="server" ToolTip="Alt+S">Search</asp:linkbutton><INPUT id="txtFromDateTemp" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtLevel1ID"
							runat="server"><INPUT id="txtToDateTemp" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtLevel1ID"
							runat="server"></TD>
				</TR>
				<TR>
					<TD width="2%"></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 20px" width="2%"></TD>
				</TR>
				<TR>
					<TD align="center"><!-- start grid for input form -->
						<TABLE id="tblGridBirth" cellSpacing="0" cellPadding="1" width="99%" border="0">
							<TR id="trGridBirth" align="center"> <!-- start grid detail for input form -->
								<TD><asp:datagrid id="dtgListBirth" CssClass="grid" runat="server" BorderColor="#3366CC" BackColor="White"
										Width="100%" AllowSorting="True" AutoGeneratecolumns="False" CellPadding="0" BorderWidth="1px"
										PageSize="15">
										<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
										<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
										<ItemStyle CssClass="gridItem"></ItemStyle>
										<HeaderStyle HorizontalAlign="Center" CssClass="gridHea?er"></HeaderStyle>
										<Columns>
											<asp:TemplateColumn HeaderText="&lt;b&gt;STT&lt;/b&gt;">
												<HeaderStyle Width="5%"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<%# Container.ItemIndex + 1 + dtgListBirth.PageSize*dtgListBirth.CurrentPageIndex%>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="EmpName" HeaderText="EmpName">
												<HeaderStyle HorizontalAlign="Center" Width="30%"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="DOB" HeaderText="DOB">
												<HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="Company" HeaderText="Company">
												<HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="Level1" HeaderText="Level1">
												<HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="Level2" HeaderText="Level2">
												<HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="Level3" HeaderText="Level3">
												<HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
											</asp:BoundColumn>
										</Columns>
										<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
									</asp:datagrid></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD align="center"><asp:textbox id="txtPageLoad" runat="server" Visible="False"></asp:textbox></TD>
	</TR>
</TABLE>
<script language="javascript">

function ValidFormBirth()
{	
	var now = new Date();
	var sYear = now.getFullYear().toString();
	document.getElementById('_ctl0_txtFromDateTemp').value = document.getElementById('_ctl0_txtFromDate').value + '/' + sYear;//'/2008';
	document.getElementById('_ctl0_txtToDateTemp').value = document.getElementById('_ctl0_txtToDate').value + '/' + sYear;//'/2008';
	
	if(document.getElementById('_ctl0_txtFromDateTemp').value!="" ||document.getElementById('_ctl0_txtToDateTemp').value!="")
	{
		if(FromSmallToDate(document.getElementById('_ctl0_txtFromDateTemp'),document.getElementById('_ctl0_txtToDateTemp')) == false)
		{
			GetAlertError(iTotal,DSAlert,"0007")
			document.getElementById('_ctl0_txtToDate').focus();
			return false;
		}
	}
	
}
function CheckddMM(field)
{
	var checkstr = "0123456789";
	var DateField = field;
	var Datevalue = "";
	var DateTemp = "";
	var seperator = "/";
	var day;
	var month;
	var year;
	var leap = 0;
	var err = 0;
	var i;
	var now = new Date();
	var sYear = now.getFullYear().toString();
	err = 0;
	
	if (DateField.value=="") return false;
	DateValue = DateField.value + '/' + sYear;
	/* Delete all chars except 0..9 */
	for (i = 0; i < DateValue.length; i++) 
	{
		if (checkstr.indexOf(DateValue.substr(i,1)) >= 0) 
		{
			DateTemp = DateTemp + DateValue.substr(i,1);
		}
	}
	DateValue = DateTemp;
	/* Always change date to 8 digits - string*/
	/* if year is entered as 2-digit / always assume 20xx */
	if (DateValue.length == 6) 
	{
		DateValue = DateValue.substr(0,4) + '20' + DateValue.substr(4,2); 
	}  
	   
	if (DateValue.length != 8) 
	{
		err = 19;
	}
	/* year is wrong if year = 0000 */
	year = DateValue.substr(4,4);
	if (year == 0) 
	{
		err = 20;
	}
	/* Validation of month*/
	month = DateValue.substr(2,2);
	if ((month < 1) || (month > 12)? 
	{
		err = 21;
	}
	/* Validation of day*/
	day = DateValue.substr(0,2);
		
	if (year <= 1900 )
	{
			err=18;
	}
	if (day < 1) 
	{
		err = 22;
	}
	/* Validation leap-year / february / day */
	if ((year % 4 == 0) || (year % 100 == 0) || (year % 400 == 0)) 
	{
		leap = 1;
	}   
	if ((month == 2) && (leap == 1) && (day > 29)) 
	{
		err = 23;
	}
	if ((month == 2) && (leap != 1) && (day > 28)) 
	{
		err = 24;
	}
	/* Validation of other months */
	if ((day > 31) && ((month == "01") || (month == "03") || (month == "05") || (month == "07") || (month == "08") || (month == "10") || (month == "12"))) 
	{
		err = 25;
	}
	if ((day > 30) && ((month == "04") || (month == "06") || (month == "09") || (month == "11"))) 
	{
		err = 26;
	}
	/* if 00 ist entered, no error, deleting the entry */
	if ((day == 0) && (month == 0) && (year == 00)) 
	{
		err = 0; day = ""; month = ""; year = ""; seperator = "";
	}
	/* if no error, write the completed date to Input-Field (e.g. 13.12.2001) */
	if (err == 0) 
	{
		DateField.value = day + seperator + month;
		return true;
	}
	else if(err=18)/* Error-message if err != 0 */
	{
			DateField.value = "";
			DateField.focus();
			alert('Dữ liệu nhập không hợp lệ. Kiểm tra lại.');
			return false;
	}
	else 
	{
			DateField.value = "";
			DateField.focus();
			alert('Dữ liệu nhập không hợp lệ. Kiểm tra lại.');
			return false;
	}
}

function CheckMM2(field)
{
	if (isNaN(field.value) || parseInt(field.value,10) < 1 || parseInt(field.value,10) > 12)
	{	
		GetAlertError(iTotal,DSAlert,"1080");
		field.focus();
		field.value = "";
		return false;
	}
	return true;
}
function CheckMM(field)
{
	var checkstr = "0123456789";
	var DateField = field;
	var Datevalue = "";
	var DateTemp = "";
	var seperator = "/";
	var day;
	var month;
	var year;
	var leap = 0;
	var err = 0;
	var i;
	var now = new Date();
	var sYear = now.getFullYear().toString();
	err = 0;
	
	if (DateField.value=="") return false;
	DateValue = '01'+ '/' + DateField.value + '/' + sYear;
	/* Delete all chars except 0..9 */
	for (i = 0; i < DateValue.length; i++) 
	{
		if (checkstr.indexOf(DateValue.substr(i,1)) >= 0) 
		{
			DateTemp = DateTemp + DateValue.substr(i,1);
		}
	}
	DateValue = DateTemp;
	/* Always change date to 8 digits - string*/
	/* if year is entered as 2-digit / always assume 20xx */
	if (DateValue.length == 6) 
	{
		DateValue = DateValue.substr(0,4) + '20' + DateValue.substr(4,2); 
	}  
	   
	if (DateValue.length != 8) 
	{
		err = 19;
	}
	/* year is wrong if year = 0000 */
	year = DateValue.substr(4,4);
	if (year == 0) 
	{
		err = 20;
	}
	/* Validation of month*/
	month = DateValue.substr(2,2);
	if ((month < 1) || (month > 12)) 
	{
		err = 21;
	}
	/* Validation of day*/
	day = DateValue.substr(0,2);
		
	if (year <= 1900 )
	{
			err=18;
	}
	if (day < 1) 
	{
		err = 22;
	}
	/* Validation leap-year / february / day */
	if ((year % 4 == 0) || (year % 100 == 0) || (year % 400 == 0)) 
	{
		leap = 1;
	}   
	if ((month == 2) && (leap == 1) && (day > 29)) 
	{
		err = 23;
	}
	if ((month == 2) && (leap != 1) && (day > 28)) 
	{
		err = 24;
	}
	/* Validation of other months */
	if ((day > 31) && ((month == "01") || (month == "03") || (month == "05") || (month == "07") || (month == "08") || (month == "10") || (month == "12"))) 
	{
		err = 25;
	}
	if ((day > 30) && ((month == "04") || (month == "06") || (month == "09") || (month == "11"))) 
	{
		err = 26;
	}
	/* if 00 ist entered, no error, deleting the entry */
	if ((day == 0) && (month == 0) && (year == 00)) 
	{
		err = 0; day = ""; month = ""; year = ""; seperator = "";
	}
	/* if no error, write the completed date to Input-Field (e.g. 13.12.2001) */
	if (err == 0) 
	{
		DateField.value = parseInt(month,10);//day + seperator + month;
		return true;
	}
	else if(err=18)/* Error-message if err != 0 */
	{
			DateField.value = "";
			DateField.focus();
			alert('D? li?u nh?p không h?p l?. Ki?m tra l?i.');
			return false;
	}
	else 
	{
			DateField.value = "";
			DateField.focus();
			alert('D? li?u nh?p không h?p l?. Ki?m tra l?i.');
			return false;
	}
}

		
</script>
<%if (!Page.IsPostBack){%>
<script language="javascript">
		document.getElementById("_ctl0_btnSearch").click();
</script>
<%}%>
