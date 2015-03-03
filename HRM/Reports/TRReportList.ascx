<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../Include/EmpHeaderSearch.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="TRReportList.ascx.cs" Inherits="iHRPCore.Reports.TRReportList1" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<meta name="vs_snapToGrid" content="False">
<script language="javascript" src="../Include/common.js"></script>
<script>
function MyCheckDate(txtFromDateId)
{
	var objFromDate=document.getElementById(txtFromDateId);
	if(objFromDate.value!='')
		if(CheckDate(objFromDate)==false) return false
		else return true;	
	return true;
}
function MyShowCalendar(txtDateId)
{
	ShowCalendar(txtDateId);
	return false;
}
function InputName(txtManagerId,txtAssId,txtPos1,txtPos2)
{
	var obj=document.getElementById(txtManagerId);
	if(obj!=null)
		if(obj.value=='')
		{
			alert('Please enter the Signer 1 Name for this Report');
			obj.focus();
			return false;
		}
	obj=document.getElementById(txtAssId);
	if(obj!=null)
		if(obj.value=='')
		{
			alert('Please enter the Signer 2 Name for this Report');
			obj.focus();
			return false;
		}
	obj=document.getElementById(txtPos1);
	if(obj!=null)
		if(obj.value=='')
		{
			alert('Please enter the Position Of Singer 1 for this Report');
			obj.focus();
			return false;
		}
	obj=document.getElementById(txtPos2);
	if(obj!=null)
		if(obj.value=='')
		{
			alert('Please enter the Position Of Singer 2 for this Report');
			obj.focus();
			return false;
		}
	return true;
}
</script>
<TABLE id="Table2" cellSpacing="0" borderColorDark="#f5f5f5" cellPadding="0" width="630"
	borderColorLight="white" border="1">
	<TR>
		<TD align="center">
			<asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center">
			<uc1:EmpHeaderSearch id="EmpHeaderSearch1" runat="server"></uc1:EmpHeaderSearch></TD>
	</TR>
	<TR>
		<TD align="center">
			<HR width="100%" SIZE="1">
		</TD>
	</TR>
	<TR>
		<TD align="left">
			<asp:label id="Label8" runat="server" CssClass="label">From date</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:textbox id="txtFromDate" runat="server" CssClass="input" Width="101"></asp:textbox>
			<asp:imagebutton id="btnCalFromDate" ImageUrl="../Images/cal.gif" runat="server" CssClass="calendar"></asp:imagebutton>&nbsp; 
			- &nbsp;
			<asp:label id="Label1" runat="server" CssClass="label">To date</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:textbox id="txtToDate" runat="server" CssClass="input" Width="101"></asp:textbox>
			<asp:imagebutton id="btnCalToDate" ImageUrl="../Images/cal.gif" runat="server" CssClass="calendar"></asp:imagebutton>&nbsp;&nbsp;</TD>
	</TR>
</TABLE>
<asp:Panel id="pnlTR" runat="server" Width="630px" Visible="False" HorizontalAlign="Left">
<asp:label id="Label2" CssClass="label" runat="server">Course Name</asp:label>&nbsp;&nbsp; 
<asp:textbox id="txtCourseName" CssClass="input" runat="server" Width="102"></asp:textbox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
<asp:label id="Label3" CssClass="label" runat="server">Supplier</asp:label>&nbsp;&nbsp;&nbsp; 
&nbsp; 
<asp:DropDownList id="cboSupplier" runat="server" Width="102px"></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
<asp:label id="Label4" CssClass="label" runat="server">Trainer</asp:label>&nbsp;&nbsp;&nbsp;&nbsp; 
<asp:textbox id="txtTrainer" CssClass="input" runat="server" Width="102"></asp:textbox>
<HR width="100%" SIZE="1">
<BR>
<TABLE id="Table4" cellSpacing="0" borderColorDark="#f5f5f5" cellPadding="8" width="630"
		borderColorLight="white" border="1">
		<TR>
			<TD style="WIDTH: 2px">
				<asp:Image id="Image84" runat="server" Width="24px" ImageUrl="../Images/List.jpg" Height="18px"></asp:Image></TD>
			<TD style="WIDTH: 249px">
				<asp:LinkButton id="btnRpt0102" runat="server">R01/02 - Training course participant history</asp:LinkButton></TD>
			<TD style="WIDTH: 1px">
				<asp:Image id="Image83" runat="server" Width="24px" ImageUrl="../Images/List.jpg" Height="18px"></asp:Image></TD>
			<TD>
				<asp:LinkButton id="btnRpt03" runat="server">R03 - Classroom training course history</asp:LinkButton></TD>
		</TR>
		<TR>
			<TD style="WIDTH: 2px">
				<asp:Image id="Image82" runat="server" Width="24px" ImageUrl="../Images/List.jpg" Height="18px"></asp:Image></TD>
			<TD style="WIDTH: 249px">
				<asp:LinkButton id="btnRpt04" runat="server">R04 - Training course Supplier history</asp:LinkButton></TD>
			<TD style="WIDTH: 1px"></TD>
			<TD></TD>
		</TR>
		<TR>
			<TD style="WIDTH: 2px">
				<asp:Image id="Image81" runat="server" Width="24px" ImageUrl="../Images/List.jpg" Height="18px"></asp:Image></TD>
			<TD style="WIDTH: 249px">
				<asp:LinkButton id="btnRpt051" runat="server">R05.1 - Training course's Attendance record</asp:LinkButton></TD>
			<TD style="WIDTH: 1px">
				<asp:Image id="Image80" runat="server" Width="24px" ImageUrl="../Images/List.jpg" Height="18px"></asp:Image></TD>
			<TD>
				<asp:LinkButton id="btnRpt052" runat="server">R05.2 - Public course participants list</asp:LinkButton></TD>
		</TR>
		<TR>
			<TD style="WIDTH: 2px">
				<asp:Image id="Image79" runat="server" Width="24px" ImageUrl="../Images/List.jpg" Height="18px"></asp:Image></TD>
			<TD style="WIDTH: 249px">
				<asp:LinkButton id="btnRpt061" runat="server">R06.1 - Workshop history</asp:LinkButton></TD>
			<TD style="WIDTH: 1px">
				<asp:Image id="Image78" runat="server" Width="24px" ImageUrl="../Images/List.jpg" Height="18px"></asp:Image></TD>
			<TD>
				<asp:LinkButton id="btnRpt062" runat="server">R06.2 - Outing History</asp:LinkButton></TD>
		</TR>
		<TR>
			<TD style="WIDTH: 2px">
				<asp:Image id="Image77" runat="server" Width="24px" ImageUrl="../Images/List.jpg" Height="18px"></asp:Image></TD>
			<TD style="WIDTH: 249px">
				<asp:LinkButton id="btnRpt063" runat="server">R06.3 - Workshop participants</asp:LinkButton></TD>
			<TD style="WIDTH: 1px">
				<asp:Image id="Image76" runat="server" Width="24px" ImageUrl="../Images/List.jpg" Height="18px"></asp:Image></TD>
			<TD>
				<asp:LinkButton id="btnRpt064" runat="server">R06.4 - Outing participants</asp:LinkButton></TD>
		</TR>
	</TABLE>
</asp:Panel>
<asp:Panel id="pnlPruU" runat="server" Width="630px" Visible="False" HorizontalAlign="Left">
<asp:label id="Label5" CssClass="label" runat="server">Subject</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
<asp:textbox id="txtSubject" CssClass="input" runat="server" Width="102"></asp:textbox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
<asp:label id="Label6" CssClass="label" runat="server">Certificate</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
<asp:DropDownList id="cboCertificate" runat="server" Width="297px"></asp:DropDownList>
<asp:label id="Label12" CssClass="label" runat="server" Width="59px" DESIGNTIMEDRAGDROP="34">Signer 1</asp:label>&nbsp;&nbsp;&nbsp;&nbsp; 
<asp:textbox id="txtManager" CssClass="input" runat="server" Width="102" DESIGNTIMEDRAGDROP="35"></asp:textbox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
<asp:label id="Label13" CssClass="label" runat="server" Width="64px" DESIGNTIMEDRAGDROP="36">Position</asp:label>
<asp:textbox id="txtPosition1" CssClass="input" runat="server" Width="102" DESIGNTIMEDRAGDROP="37"></asp:textbox>&nbsp;<BR>
<asp:label id="Label15" CssClass="label" runat="server" Width="59px" DESIGNTIMEDRAGDROP="38">Signer 2</asp:label>&nbsp;&nbsp;&nbsp;&nbsp; 
<asp:textbox id="txtAssManager" CssClass="input" runat="server" Width="102" DESIGNTIMEDRAGDROP="39"></asp:textbox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
<asp:label id="Label14" CssClass="label" runat="server" Width="64px" DESIGNTIMEDRAGDROP="40">Position</asp:label>
<asp:textbox id="txtPosition2" CssClass="input" runat="server" Width="102" DESIGNTIMEDRAGDROP="41"></asp:textbox>
<HR width="100%" SIZE="1">

<TABLE id="Table5" cellSpacing="0" borderColorDark="#f5f5f5" cellPadding="8" width="630"
		borderColorLight="white" border="1">
		<TR>
			<TD style="WIDTH: 2px">
				<asp:Image id="Image47" runat="server" Width="24px" ImageUrl="../Images/List.jpg" Height="18px"></asp:Image></TD>
			<TD style="WIDTH: 249px">
				<asp:LinkButton id="btnRpt07" runat="server">R07 - PruU testing enrolment</asp:LinkButton></TD>
			<TD style="WIDTH: 1px"></TD>
			<TD></TD>
		</TR>
		<TR>
			<TD style="WIDTH: 2px">
				<asp:Image id="Image46" runat="server" Width="24px" ImageUrl="../Images/List.jpg" Height="18px"></asp:Image></TD>
			<TD style="WIDTH: 249px">
				<asp:LinkButton id="btnRpt081" runat="server">R08.1 - PruU testing history</asp:LinkButton></TD>
			<TD style="WIDTH: 1px">
				<asp:Image id="Image44" runat="server" Width="24px" ImageUrl="../Images/List.jpg" Height="18px"></asp:Image></TD>
			<TD>
				<asp:LinkButton id="btnRpt083" runat="server">R08.2 - PruU Certification Completion</asp:LinkButton></TD>
		</TR>
		<TR>
			<TD style="WIDTH: 2px">
				<asp:Image id="Image43" runat="server" Width="24px" ImageUrl="../Images/List.jpg" Height="18px"></asp:Image></TD>
			<TD style="WIDTH: 249px">
				<asp:LinkButton id="btnRpt091" runat="server">R09.1 - PruU testing Flex report (credit)</asp:LinkButton></TD>
			<TD style="WIDTH: 1px">
				<asp:Image id="Image42" runat="server" Width="24px" ImageUrl="../Images/List.jpg" Height="18px"></asp:Image></TD>
			<TD>
				<asp:LinkButton id="btnRpt092" runat="server">R09.2 - PruU testing Flex report (debit)</asp:LinkButton></TD>
		</TR>
		<TR>
			<TD style="WIDTH: 2px">
				<asp:Image id="Image41" runat="server" Width="24px" ImageUrl="../Images/List.jpg" Height="18px"></asp:Image></TD>
			<TD style="WIDTH: 249px">
				<asp:LinkButton id="btnRpt10" runat="server">R10 - Pru University Certificate</asp:LinkButton></TD>
			<TD style="WIDTH: 1px">
				<asp:Image id="Image2" runat="server" Width="24px" ImageUrl="../Images/List.jpg" Height="18px"></asp:Image></TD>
			<TD>
				<asp:LinkButton id="btnRpt101" runat="server">R11.1 - Pru University Grade Report</asp:LinkButton></TD>
		</TR>
		<TR>
			<TD style="WIDTH: 2px">
				<asp:Image id="Image1" runat="server" Width="24px" ImageUrl="../Images/List.jpg" Height="18px"></asp:Image></TD>
			<TD style="WIDTH: 249px">
				<asp:LinkButton id="btnRpt12" runat="server">R12 - PruU Quartly/Yearly report</asp:LinkButton></TD>
			<TD style="WIDTH: 1px"></TD>
			<TD></TD>
		</TR>
	</TABLE>
</asp:Panel>
<asp:Panel id="pnlLOMA" runat="server" Width="630px" Visible="False" HorizontalAlign="Left">
<asp:label id="Label7" CssClass="label" runat="server">Course</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
<asp:DropDownList id="cboLomaCourseID" runat="server" Width="304px"></asp:DropDownList><BR>
<asp:label id="Label9" CssClass="label" runat="server">Certificate</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
<asp:DropDownList id="cboLOMACertificateID" runat="server" Width="304px"></asp:DropDownList>
<HR width="100%" SIZE="1">

<TABLE id="Table6" cellSpacing="0" borderColorDark="#f5f5f5" cellPadding="8" width="630"
		borderColorLight="white" border="1">
		<TR>
			<TD style="WIDTH: 2px">
				<asp:Image id="Image38" runat="server" Width="24px" ImageUrl="../Images/List.jpg" Height="18px"></asp:Image></TD>
			<TD style="WIDTH: 249px">
				<asp:LinkButton id="btnRpt133" runat="server">R13.1 - LOMA Enrolment</asp:LinkButton></TD>
			<TD style="WIDTH: 1px">
				<asp:Image id="Image40" runat="server" Width="24px" ImageUrl="../Images/List.jpg" Height="18px"></asp:Image></TD>
			<TD>
				<asp:LinkButton id="btnRpt131" runat="server">R13.2 - LOMA Testing history</asp:LinkButton></TD>
		</TR>
		<TR>
			<TD style="WIDTH: 2px">
				<asp:Image id="Image39" runat="server" Width="24px" ImageUrl="../Images/List.jpg" Height="18px"></asp:Image></TD>
			<TD style="WIDTH: 249px">
				<asp:LinkButton id="btnRpt132" runat="server"> R13.3 - LOMA Certification Completion</asp:LinkButton></TD>
			<TD style="WIDTH: 1px"></TD>
			<TD></TD>
		</TR>
		<TR>
			<TD style="WIDTH: 2px">
				<asp:Image id="Image37" runat="server" Width="24px" ImageUrl="../Images/List.jpg" Height="18px"></asp:Image></TD>
			<TD style="WIDTH: 249px">
				<asp:LinkButton id="btnRpt141" runat="server">R14.1 - LOMA testing Flex (credit)</asp:LinkButton></TD>
			<TD style="WIDTH: 1px">
				<asp:Image id="Image36" runat="server" Width="24px" ImageUrl="../Images/List.jpg" Height="18px"></asp:Image></TD>
			<TD>
				<asp:LinkButton id="btnRpt142" runat="server">R14.2 - LOMA testing Flex (debit)</asp:LinkButton></TD>
		</TR>
		<TR>
			<TD style="WIDTH: 2px">
				<asp:Image id="Image35" runat="server" Width="24px" ImageUrl="../Images/List.jpg" Height="18px"></asp:Image></TD>
			<TD style="WIDTH: 249px">
				<asp:LinkButton id="btnRpt15" runat="server">R15 - LOMA Yearly report</asp:LinkButton></TD>
			<TD style="WIDTH: 1px">
				<asp:Image id="Image34" runat="server" Width="24px" ImageUrl="../Images/List.jpg" Height="18px"></asp:Image></TD>
			<TD>
				<asp:LinkButton id="btnRpt16" runat="server">R16 - LOMA Suggestion course</asp:LinkButton></TD>
		</TR>
		<TR>
			<TD style="WIDTH: 2px">
				<asp:Image id="Image3" runat="server" Width="24px" ImageUrl="../Images/List.jpg" Height="18px"></asp:Image></TD>
			<TD style="WIDTH: 249px">
				<asp:LinkButton id="btnRpt102" runat="server">R11.2 - LOMA Grade Report</asp:LinkButton></TD>
			<TD style="WIDTH: 1px"></TD>
			<TD></TD>
		</TR>
	</TABLE>
</asp:Panel>
<asp:Panel id="pnlGE" runat="server" Width="630px" Visible="False" HorizontalAlign="Left">
<asp:label id="Label10" CssClass="label" runat="server" Width="44px">Course</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
<asp:textbox id="txtCourseID" CssClass="input" runat="server" Width="102"></asp:textbox>&nbsp;&nbsp;&nbsp;&nbsp; 
<asp:label id="Label11" CssClass="label" runat="server" Width="80px">Course Type</asp:label>
<asp:DropDownList id="cboCourseType" runat="server" Width="102px">
		<asp:ListItem Selected="True"></asp:ListItem>
		<asp:ListItem Value="1">Training</asp:ListItem>
		<asp:ListItem Value="0">Public</asp:ListItem>
		<asp:ListItem Value="2">Workshop</asp:ListItem>
		<asp:ListItem Value="3">Outing</asp:ListItem>
		<asp:ListItem Value="4">PruU</asp:ListItem>
		<asp:ListItem Value="5">LOMA</asp:ListItem>
	</asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 

<HR width="100%" SIZE="1">

<TABLE id="Table1" cellSpacing="0" borderColorDark="#f5f5f5" cellPadding="8" width="630"
		borderColorLight="white" border="1">
		<TR>
			<TD style="WIDTH: 2px">
				<asp:Image id="Image21" runat="server" Width="24px" ImageUrl="../Images/List.jpg" Height="18px"></asp:Image></TD>
			<TD style="WIDTH: 249px">
				<asp:LinkButton id="btnRpt17" runat="server">R17 - Total training history by Employee</asp:LinkButton></TD>
			<TD style="WIDTH: 1px">
				<asp:Image id="Image22" runat="server" Width="24px" ImageUrl="../Images/List.jpg" Height="18px"></asp:Image></TD>
			<TD>
				<asp:LinkButton id="btnRpt18" runat="server">R18 - Total training course report</asp:LinkButton></TD>
		</TR>
		<TR>
			<TD style="WIDTH: 2px">
				<asp:Image id="Image23" runat="server" Width="24px" ImageUrl="../Images/List.jpg" Height="18px"></asp:Image></TD>
			<TD style="WIDTH: 249px">
				<asp:LinkButton id="btnRpt19" runat="server">R19 - Working day for Training</asp:LinkButton></TD>
			<TD style="WIDTH: 1px">
				<asp:Image id="Image24" runat="server" Width="24px" ImageUrl="../Images/List.jpg" Height="18px"></asp:Image></TD>
			<TD>
				<asp:LinkButton id="btnRpt20" runat="server">R20 - Headcount trained for year</asp:LinkButton></TD>
		</TR>
		<TR>
			<TD style="WIDTH: 2px">
				<asp:Image id="Image25" runat="server" Width="24px" ImageUrl="../Images/List.jpg" Height="18px"></asp:Image></TD>
			<TD style="WIDTH: 249px">
				<asp:LinkButton id="btnRpt21" runat="server">R21 - Department development plan</asp:LinkButton></TD>
			<TD style="WIDTH: 1px"></TD>
			<TD></TD>
		</TR>
	</TABLE>
</asp:Panel>
