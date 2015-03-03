<%@ Control Language="c#" AutoEventWireup="false" Codebehind="EmpHeader.ascx.cs" Inherits="iHRPCore.Include.EmpHeader" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
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
					<TD noWrap><asp:label id="Label7" CssClass="label" runat="server">Emp Code</asp:label></TD>
					<TD><asp:textbox id="txtEmpID" onkeypress="checkKey('btnSearchByID')" CssClass="input" runat="server"
							Width="70%"></asp:textbox><INPUT class="search" onclick="javascript:OpenWindowEmp('EmpID')" id="btnSearchByID" type="button"
							value="..."></TD>
					<TD noWrap><asp:label id="Label2" CssClass="label" runat="server"> Name</asp:label></TD>
					<TD><asp:textbox id="txtEmpName" onkeypress="checkKey('btnSearchByName')" CssClass="input" runat="server"
							Width="80%"></asp:textbox><INPUT class="search" onclick="javascript:OpenWindowEmp('EmpName')" id="btnSearchByName"
							type="button" value="..."></TD>
					<TD noWrap><asp:label id="Label1" CssClass="label" runat="server"> Joining date</asp:label></TD>
					<TD><asp:label id="lblStartDate" CssClass="labelData" runat="server" Width="95%"></asp:label></TD>
				</TR>
				<TR valign="top">
					<TD noWrap width="10%">
						<asp:label id="Label9" runat="server" CssClass="label">Company</asp:label></TD>
					<TD width="23%">
						<asp:label id="lblLevel1Name" runat="server" CssClass="labelData"></asp:label></TD>
					<TD noWrap width="9%">
						<asp:label id="Label4" runat="server" CssClass="label">Department</asp:label></TD>
					<TD width="23%">
						<asp:label id="lblLevel2Name" runat="server" CssClass="labelData"></asp:label></TD>
					<TD noWrap width="9%">
						<asp:label id="Label3" runat="server" CssClass="label">Group</asp:label></TD>
					<TD width="23%">
						<asp:label id="lblLevel3Name" runat="server" CssClass="labelData"></asp:label></TD>
				</TR>
				<TR vAlign="top">
					<TD style="HEIGHT: 3px" noWrap>
						<asp:label id="Label5" runat="server" CssClass="label">Function</asp:label></TD>
					<TD style="HEIGHT: 3px">
						<asp:label id="lblLocationName" runat="server" CssClass="labelData"></asp:label></TD>
					<TD style="HEIGHT: 3px" noWrap>
						<asp:label id="Label10" runat="server" CssClass="label">Employment Type</asp:label></TD>
					<TD style="HEIGHT: 3px">
						<asp:label id="lblEmpType" runat="server" CssClass="labelData"></asp:label></TD>
					<TD style="HEIGHT: 3px" noWrap>
						<asp:label id="Label8" runat="server" CssClass="label">Job title</asp:label></TD>
					<TD style="HEIGHT: 3px">
						<asp:label id="lblPosition" runat="server" CssClass="labelData"></asp:label></TD>
				</TR>
				<TR style="DISPLAY: none">
					<TD align="center" colSpan="6">
						<asp:label id="Label6" runat="server" CssClass="label">Customer group</asp:label><INPUT id="txtLengthEmpID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtLengthEmpID"
							runat="server">
						<asp:label id="lblCompanyName" runat="server" CssClass="labelData"></asp:label></TD>
				</TR>
			</TABLE> <!--End table body-->
		</TD>
		<TD width="9" background="images/table_15.jpg" height="9"><IMG height="9" alt="" src="images/table_15.jpg" width="9"></TD>
	</TR>
	<TR>
		<TD width="5" height="10"><IMG height="10" alt="" src="images/table_21.jpg" width="5"></TD>
		<TD background="images/table_23.jpg" height="10"><IMG height="10" alt="" src="images/table_23.jpg" width="10"></TD>
		<TD width="9" height="10"><IMG height="10" alt="" src="images/table_25.jpg" width="9"></TD>
	</TR>
</TABLE> <!--End table-->
<SCRIPT language="javascript">
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
		ShowDialog('FormPage.aspx?Ascx=' + strUrl)

	} 
	
</SCRIPT>
<%if (Session["EmpID"] == null && Request.Params["FunctionID"].Trim() != "152" && Request.Params["ModuleID"].Trim() != "SI"){%>
<SCRIPT language="javascript">	
	document.getElementById('btnSearchByID').click();
</SCRIPT>
<%} else {%>
<SCRIPT language="javascript">	
	document.getElementById("_ctl0:HR_EmpHeader:txtEmpID").focus();
</SCRIPT>
<%}%>
