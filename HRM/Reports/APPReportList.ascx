<%@ Control Language="c#" AutoEventWireup="false" Codebehind="APPReportList.ascx.cs" Inherits="iHRPCore.Reports.APPReportList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../Include/EmpHeaderSearch.ascx" %>
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
			<asp:label id="Label8" runat="server" CssClass="label"> Year</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:textbox id="txtFromDate" onblur="CheckYear(this)" runat="server" CssClass="input" Width="101"></asp:textbox>&nbsp; 
			- &nbsp;
			<asp:label id="Label1" runat="server" CssClass="label"> Period</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:DropDownList id="cboPeriod" runat="server" Width="136px">
				<asp:ListItem Value="Mid. Year" Selected="True">Mid. Year</asp:ListItem>
				<asp:ListItem Value="Final Year">Final Year</asp:ListItem>
			</asp:DropDownList></TD>
	</TR>
</TABLE>
<asp:Panel id="pnlTR" runat="server" Width="630px" HorizontalAlign="Left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 

<HR width="100%" SIZE="1">
<BR>
<TABLE id="Table4" cellSpacing="0" borderColorDark="#f5f5f5" cellPadding="8" width="630"
		borderColorLight="white" border="1">
		<TR>
			<TD style="WIDTH: 2px">
				<asp:Image id="Image84" runat="server" Width="24px" Height="18px" ImageUrl="../Images/List.jpg"></asp:Image></TD>
			<TD style="WIDTH: 249px">
				<asp:LinkButton id="btnRpt0102" runat="server">R01 - Indicative rating by level-year</asp:LinkButton></TD>
			<TD style="WIDTH: 1px">
				<asp:Image id="Image83" runat="server" Width="24px" Height="18px" ImageUrl="../Images/List.jpg"></asp:Image></TD>
			<TD>
				<asp:LinkButton id="btnRpt03" runat="server">R02 - Overall indicative rating -by dept-year</asp:LinkButton></TD>
		</TR>
		<TR>
			<TD style="WIDTH: 2px">
				<asp:Image id="Image1" runat="server" Width="24px" Height="18px" ImageUrl="../Images/List.jpg"></asp:Image></TD>
			<TD style="WIDTH: 249px">
				<asp:LinkButton id="LinkButton1" runat="server">R03 - Total Company by Rating – Year</asp:LinkButton></TD>
			<TD style="WIDTH: 1px"></TD>
			<TD></TD>
		</TR>
	</TABLE></asp:Panel>
