<%@ Control Language="c#" AutoEventWireup="false" Codebehind="EmpHeaderInfo.ascx.cs" Inherits="iHRPCore.Include.EmpHeaderInfo" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<!--table-->
<TABLE BORDER="0" CELLPADDING="0" CELLSPACING="0" width="100%">
	<TR>
		<TD WIDTH="5" HEIGHT="5">
			<IMG SRC="images/table_01.jpg" WIDTH="5" HEIGHT="5" ALT=""></TD>
		<TD background="images/table_03.jpg" HEIGHT="5">
			<IMG SRC="images/table_03.jpg" WIDTH="10" HEIGHT="5" ALT=""></TD>
		<TD WIDTH="9" HEIGHT="5">
			<IMG SRC="images/table_05.jpg" WIDTH="9" HEIGHT="5" ALT=""></TD>
	</TR>
	<TR>
		<TD background="images/table_11.jpg" WIDTH="5">
			<IMG SRC="images/table_11.jpg" WIDTH="5" HEIGHT="9" ALT=""></TD>
		<TD background="images/table_13.jpg" align="center">
			<!--table body-->
			<TABLE id="Table1" cellSpacing="0" cellPadding="1" width="100%" border="0">
				<TR>
					<TD noWrap width="10%"><asp:label id="Label7" runat="server" CssClass="label">Emp Code</asp:label></TD>
					<TD noWrap width="20%"><asp:textbox id="txtEmpID" runat="server" CssClass="input" Width="70%" ReadOnly="True"></asp:textbox></TD>
					<TD noWrap width="10%"><asp:label id="Label1" runat="server" CssClass="label"> Name</asp:label></TD>
					<TD noWrap width="33%"><asp:textbox id="txtEmpName" runat="server" CssClass="input" Width="80%" ReadOnly="True"></asp:textbox></TD>
					<TD noWrap width="9%"><asp:label id="Label2" runat="server" CssClass="label"> Joining date</asp:label></TD>
					<TD noWrap width="18%"><asp:label id="lblStartDate" runat="server" CssClass="labelData" Width="95%">01/01/2005</asp:label></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 22px">
						<asp:label id="Label5" runat="server" CssClass="label">Location</asp:label></TD>
					<TD style="HEIGHT: 22px">
						<asp:label id="lblLocationName" runat="server" CssClass="labelData"></asp:label></TD>
					<TD style="HEIGHT: 22px">
						<asp:label id="Label9" runat="server" CssClass="label"> Company</asp:label></TD>
					<TD style="HEIGHT: 22px"><asp:label id="lblLevel1Name" CssClass="labelData" runat="server"></asp:label></TD>
					<TD style="HEIGHT: 22px"><asp:label id="Label3" CssClass="label" runat="server"> Group</asp:label></TD>
					<TD>
						<asp:label id="lblLevel3Name" runat="server" CssClass="labelData"></asp:label></TD>
				</TR>
				<TR>
					<TD noWrap>
						<asp:label id="Label6" runat="server" CssClass="label"> Customer group</asp:label></TD>
					<TD noWrap>
						<asp:label id="lblCompanyName" runat="server" CssClass="labelData"></asp:label></TD>
					<TD noWrap><asp:label id="Label4" CssClass="label" runat="server">Department</asp:label></TD>
					<TD noWrap><asp:label id="lblLevel2Name" CssClass="labelData" runat="server"></asp:label></TD>
					<TD noWrap><asp:label id="Label8" CssClass="label" runat="server">Position</asp:label></TD>
					<TD noWrap><asp:label id="lblPosition" CssClass="labelData" runat="server"></asp:label></TD>
				</TR>
				<TR style="DISPLAY:none">
					<td colspan="6" align="center">
						<INPUT type="hidden" runat="server" id="txtLengthEmpID" style="WIDTH: 9px; HEIGHT: 22px"
							size="1" NAME="txtLengthEmpID">
					</td>
				</TR>
			</TABLE>
			<!--End table body-->
		</TD>
		<TD background="images/table_15.jpg" WIDTH="9" HEIGHT="9">
			<IMG SRC="images/table_15.jpg" WIDTH="9" HEIGHT="9" ALT=""></TD>
	</TR>
	<TR>
		<TD WIDTH="5" HEIGHT="10">
			<IMG SRC="images/table_21.jpg" WIDTH="5" HEIGHT="10" ALT=""></TD>
		<TD background="images/table_23.jpg" HEIGHT="10">
			<IMG SRC="images/table_23.jpg" WIDTH="10" HEIGHT="10" ALT=""></TD>
		<TD WIDTH="9" HEIGHT="10">
			<IMG SRC="images/table_25.jpg" WIDTH="9" HEIGHT="10" ALT=""></TD>
	</TR>
</TABLE>
