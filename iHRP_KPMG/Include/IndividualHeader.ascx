<%@ Control Language="c#" AutoEventWireup="false" Codebehind="IndividualHeader.ascx.cs" Inherits="iHRPCore.Include.IndividualHeader" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
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
				<TR valign="top">
					<TD noWrap><asp:label id="Label7" CssClass="label" runat="server"> ID</asp:label></TD>
					<TD>
						<asp:label id="lblEmpID" runat="server" CssClass="labelData"></asp:label></TD>
					<TD noWrap><asp:label id="Label2" CssClass="label" runat="server">Full name</asp:label></TD>
					<TD>
						<asp:label id="lblEmpName" runat="server" CssClass="labelData"></asp:label></TD>
					<TD noWrap><asp:label id="Label1" CssClass="label" runat="server"> Joining date</asp:label></TD>
					<TD><asp:label id="lblStartDate" CssClass="labelData" runat="server" Width="95%"></asp:label></TD>
				</TR>
				<TR valign="top">
					<TD noWrap width="10%">
						<asp:label id="Label6" runat="server" CssClass="label"> Customer group</asp:label></TD>
					<TD width="16%">
						<asp:label id="lblCompanyName" runat="server" CssClass="labelData"></asp:label></TD>
					<TD noWrap width="9%">
						<asp:label id="Label5" runat="server" CssClass="label"> Company</asp:label></TD>
					<TD width="33%"><asp:label id="lblLevel1Name" CssClass="labelData" runat="server"></asp:label></TD>
					<TD noWrap width="9%">
						<asp:label id="Label9" runat="server" CssClass="label"> Dept</asp:label></TD>
					<TD width="20%"><asp:label id="lblLevel2Name" CssClass="labelData" runat="server"></asp:label></TD>
				</TR>
				<TR valign="top">
					<TD noWrap><asp:label id="Label3" CssClass="label" runat="server"> Group</asp:label></TD>
					<TD>
						<asp:label id="lblLevel3Name" runat="server" CssClass="labelData"></asp:label></TD>
					<TD noWrap>
						<asp:label id="Label8" runat="server" CssClass="label"> Post Category</asp:label></TD>
					<TD>
						<asp:label id="lblPosition" runat="server" CssClass="labelData"></asp:label></TD>
					<TD noWrap><asp:label id="Label4" CssClass="label" runat="server"> Location</asp:label></TD>
					<TD>
						<asp:label id="lblLocationName" runat="server" CssClass="labelData"></asp:label></TD>
				</TR>
				<TR style="DISPLAY:none">
					<td colspan="6" align="center">
						<asp:label id="lblSourceData" runat="server" CssClass="labelData"></asp:label>
						<asp:label id="lblSource" runat="server" CssClass="label">Source</asp:label>
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
<!--End table-->
<script language="javascript">
	function OpenWindowEmp(strField)
	{
		var strUrl;
		//Neu goi form popup dua vao ma nhan vien
		if (strField == "EmpID")
		{
			//Neu ma nhan vien trung voi ma nhan vien cua nhan vien dang duoc chon, mo trang popup
			if (trim(document.getElementById("_ctl0:HR_EmpHeader:txtEmpID").value) == trim(document.getElementById("_ctl0:HR_EmpHeader:txtEmpID").title))
				strUrl = 'MdlHR/EmpPopup.ascx&FunctionID=204';
			else
			{
				//Neu chieu dai ma nhan vien = chieu dai qui dinh cua ma nhan vien
				//if(parseInt(document.getElementById("_ctl0:HR_EmpHeader:txtEmpID").value.length) == parseInt(document.getElementById("_ctl0:HR_EmpHeader:txtLengthEmpID").value))
					//Truyen tham so return trong truong hop xac dinh ma nhan vien chinh xac
					strUrl = 'MdlHR/EmpPopup.ascx&FunctionID=204&EmpID=' + trim(document.getElementById("_ctl0:HR_EmpHeader:txtEmpID").value) + '&Return=1';
				//else
				//	strUrl = 'MdlHR/EmpPopup.ascx&EmpID=' + trim(document.getElementById("_ctl0:HR_EmpHeader:txtEmpID").value);
			}
		}
		//Neu goi form popup dua vao ten nhan vien
		else if (strField == "EmpName")
		{
			if (trim(document.getElementById("_ctl0:HR_EmpHeader:txtEmpName").value) == trim(document.getElementById("_ctl0:HR_EmpHeader:txtEmpName").title))
				strUrl = 'MdlHR/EmpPopup.ascx&FunctionID=204';
			else
			{
				strUrl = 'MdlHR/EmpPopup.ascx&FunctionID=204&EmpName=' + trim(document.getElementById("_ctl0:HR_EmpHeader:txtEmpName").value) + '&Return=1';
			}
		}
		if ('<%=Request.Params["IsTermination"]%>' != "")
			strUrl = strUrl + "&IsTermination=1"
		if ('<%=Request.Params["tabid"]%>' != "")
			strUrl = strUrl + "&tabid=1"
		ShowDialog('FormPage.aspx?Ascx=' + strUrl)

	} 
	
</script>

