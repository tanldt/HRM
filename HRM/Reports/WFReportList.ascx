<%@ Control Language="c#" AutoEventWireup="false" Codebehind="WFReportList.ascx.cs" Inherits="iHRPCore.Reports.WFReportList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../Include/EmpHeaderSearch.ascx" %>
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
function SelectEquipment(cboEquipId)
{
	var obj=document.getElementById(cboEquipId);
	if(obj!=null)
	if(obj.value=='')
	{
		alert('Please select Equipment');
		obj.focus();
		return false;
	}
	return true;
}

</script>
<TABLE id="Table2" cellSpacing="0" borderColorDark="#f5f5f5" cellPadding="0" width="630"
	borderColorLight="white" border="1">
	<TR>
		<TD align="center"><asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center"><uc1:empheadersearch id="EmpHeaderSearch1" runat="server"></uc1:empheadersearch></TD>
	</TR>
	<TR>
		<TD align="center">
			<HR width="100%" SIZE="1">
		</TD>
	</TR>
	<TR>
		<TD align="left"><asp:label id="Label8" CssClass="label" runat="server" Width="64">From date</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:textbox id="txtFromDate" CssClass="input" runat="server" Width="102"></asp:textbox><asp:imagebutton id="btnCalFromDate" CssClass="calendar" runat="server" ImageUrl="../Images/cal.gif"></asp:imagebutton>&nbsp; 
			- &nbsp;
			<asp:label id="Label1" CssClass="label" runat="server">To date</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:textbox id="txtToDate" CssClass="input" runat="server" Width="101"></asp:textbox>
			<asp:imagebutton id="btnCalToDate" runat="server" CssClass="calendar" ImageUrl="../Images/cal.gif"></asp:imagebutton>&nbsp;&nbsp;<BR>
			<asp:label id="Label2" CssClass="label" runat="server" Width="64px">Equipment</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:DropDownList id="cboEquipment" runat="server" Width="104px"></asp:DropDownList></TD>
	</TR>
</TABLE>
<asp:Panel id="pnlTR" runat="server" Width="630px" HorizontalAlign="Left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 

<HR width="100%" SIZE="1">
<BR>
<TABLE id="Table4" cellSpacing="0" borderColorDark="#f5f5f5" cellPadding="8" width="630"
		borderColorLight="white" border="1">
		<TR>
			<TD style="WIDTH: 2px">
				<asp:Image id="Image84" runat="server" Width="24px" ImageUrl="../Images/List.jpg" Height="18px"></asp:Image></TD>
			<TD style="WIDTH: 249px">
				<asp:LinkButton id="btnRpt0102" runat="server">R01 - Thẻ kho</asp:LinkButton></TD>
			<TD style="WIDTH: 1px">
				<asp:Image id="Image83" runat="server" Width="24px" ImageUrl="../Images/List.jpg" Height="18px"></asp:Image></TD>
			<TD>
				<asp:LinkButton id="btnRpt03" runat="server">R02 - Vật dụng cấp phát (xe máy, điện thoại ...)</asp:LinkButton></TD>
		</TR>
		<TR>
			<TD style="WIDTH: 2px">
				<asp:Image id="Image82" runat="server" Width="24px" ImageUrl="../Images/List.jpg" Height="18px"></asp:Image></TD>
			<TD style="WIDTH: 249px">
				<asp:LinkButton id="btnRpt04" runat="server">R03 - Cấp thẻ nhân viên</asp:LinkButton></TD>
			<TD style="WIDTH: 1px">
				<asp:Image id="Image81" runat="server" Width="24px" ImageUrl="../Images/List.jpg" Height="18px"></asp:Image></TD>
			<TD>
				<asp:LinkButton id="btnRpt051" runat="server">R04 - Benefit Employee List</asp:LinkButton></TD>
		</TR>
		<TR>
			<TD style="WIDTH: 2px">
				<asp:Image id="Image80" runat="server" Width="24px" ImageUrl="../Images/List.jpg" Height="18px"></asp:Image></TD>
			<TD style="WIDTH: 249px">
				<asp:LinkButton id="btnRpt052" runat="server">R05 - Bảng theo dõi cấp phát đồng phục</asp:LinkButton></TD>
			<TD style="WIDTH: 1px">
				<asp:Image id="Image79" runat="server" Width="24px" ImageUrl="../Images/List.jpg" Height="18px"></asp:Image></TD>
			<TD>
				<asp:LinkButton id="btnRpt061" runat="server">R06 - DS NV điều chỉnh mức nhận trợ cấp đồng phục bằng tiền</asp:LinkButton></TD>
		</TR>
		<TR>
			<TD style="WIDTH: 2px">
				<asp:Image id="Image78" runat="server" Width="24px" ImageUrl="../Images/List.jpg" Height="18px"></asp:Image></TD>
			<TD style="WIDTH: 249px">
				<asp:LinkButton id="btnRpt062" runat="server">R07 - DS NV điều chỉnh mức nhận trợ cấp gửi xe bằng tiền</asp:LinkButton></TD>
			<TD style="WIDTH: 1px">
				<asp:Image id="Image1" runat="server" Width="24px" ImageUrl="../Images/List.jpg" Height="18px"></asp:Image></TD>
			<TD>
				<asp:LinkButton id="btnRptVoucherParking" runat="server">R08 - Voucher / Parking</asp:LinkButton></TD>
		</TR>
		<TR>
			<TD style="WIDTH: 2px">
				<asp:Image id="Image77" runat="server" Width="24px" ImageUrl="../Images/List.jpg" Height="18px"></asp:Image></TD>
			<TD style="WIDTH: 249px">
				<asp:LinkButton id="btnRpt063" runat="server">R09 - Chi tiết vật dụng, đồng phục ... được cấp phát của NV</asp:LinkButton></TD>
			<TD style="WIDTH: 1px">
				<asp:Image id="Image76" runat="server" Width="24px" ImageUrl="../Images/List.jpg" Height="18px"></asp:Image></TD>
			<TD>
				<asp:LinkButton id="btnRptHealthchecked" runat="server">R10 - Healthcheck</asp:LinkButton></TD>
		</TR>
	</TABLE></asp:Panel>
