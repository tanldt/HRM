<%@ Control Language="c#" AutoEventWireup="false" Codebehind="CanHeader.ascx.cs" Inherits="iHRPCore.Include.CanHeader" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%> <!--table-->
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
					<TD noWrap><asp:label id="lblCandidateID" CssClass="label" runat="server">Candidate ID</asp:label></TD>
					<TD><asp:textbox id="txtCandidateCode" onkeypress="checkKey('btnSearchByID')" CssClass="input" runat="server"
							Width="70%"></asp:textbox><INPUT class="search" onclick="javascript:OpenWindowEmp('CanID')" id="btnSearchByID" type="button"
							value="..."></TD>
					<TD noWrap><asp:label id="lblCandidateName" CssClass="label" runat="server">Candidate Name</asp:label></TD>
					<TD style="WIDTH: 288px"><asp:textbox id="txtCandidateName" onkeypress="checkKey('btnSearchByName')" CssClass="input"
							runat="server" Width="70%"></asp:textbox><INPUT class="search" onclick="javascript:OpenWindowEmp('CanName')" id="btnSearchByName"
							type="button" value="..."></TD>
					<TD noWrap><asp:label id="lblDOB" CssClass="label" runat="server"> Date of birth</asp:label></TD>
					<TD><asp:label id="lblDOB_Data" CssClass="labelData" runat="server" Width="100%">01/01/2005</asp:label></TD>
				</TR>
				<TR valign="top">
					<TD noWrap width="15%">
						<asp:label id="lblApplyForJobDate" runat="server" CssClass="label"> Apply for Job date</asp:label></TD>
					<TD width="20%">
						<asp:label id="lblApplyForJobDate_Data" CssClass="labelData" runat="server" Width="100%">01/01/2005</asp:label></TD>
					<TD noWrap width="15%">
						<asp:label id="lblExpectationJobTitle" runat="server" CssClass="label">Expectation Job title</asp:label></TD>
					<TD>
						<asp:label id="lblExpectationJobTitle_Data" runat="server" CssClass="labelData"></asp:label></TD>
					<TD noWrap width="10%">
						<asp:label id="lblStatus" runat="server" CssClass="label">Status</asp:label></TD>
					<TD width="15%"><asp:label id="lblStatus_Data" CssClass="labelData" runat="server" Width="100%"></asp:label></TD>
				</TR>
				<TR valign="top">
					<TD noWrap><asp:label id="lblMajor" CssClass="label" runat="server">Major</asp:label></TD>
					<TD><asp:label id="lblMajor_Data" CssClass="labelData" runat="server" Width="100%"></asp:label></TD>
					<TD noWrap><asp:label id="lblExperience" CssClass="label" runat="server"> Experience</asp:label></TD>
					<TD style="WIDTH: 288px">
						<asp:label id="lblExperience_Data" runat="server" CssClass="labelData" Width="100%"></asp:label></TD>
					<TD noWrap></TD>
					<TD></TD>
				</TR>
				<TR style="DISPLAY:none">
					<td colspan="6" align="center">
						<INPUT type="hidden" runat="server" id="txtLengthCanID" style="WIDTH: 9px; HEIGHT: 22px"
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
<SCRIPT language="javascript">
	function OpenWindowEmp(strField)
	{
		var strUrl;
		//Neu goi form popup dua vao ma ung vien
		if (strField == "CanID")
		{
			//Neu ma nhan vien trung voi ma nhan vien cua nhan vien dang duoc chon, mo trang popup
			if (trim(document.getElementById("_ctl0:RE_CanHeader:txtCandidateCode").value) == trim(document.getElementById("_ctl0:RE_CanHeader:txtCandidateCode").title))
			{
				strUrl = 'MdlRE/CanPopup.ascx&FunctionID=7001';
				
			}
			else
			{
				
				//Neu chieu dai ma nhan vien = chieu dai qui dinh cua ma nhan vien
				//if(parseInt(document.getElementById("_ctl0:HR_EmpHeader:txtEmpID").value.length) == parseInt(document.getElementById("_ctl0:HR_EmpHeader:txtLengthEmpID").value))
					//Truyen tham so return trong truong hop xac dinh ma nhan vien chinh xac
					strUrl = 'MdlRE/CanPopup.ascx&FunctionID=7001&CanID=' + trim(document.getElementById("_ctl0:RE_CanHeader:txtCandidateCode").value) + '&Return=1';
					//alert(strUrl);
				//else
				//	strUrl = 'MdlHR/EmpPopup.ascx&EmpID=' + trim(document.getElementById("_ctl0:HR_EmpHeader:txtEmpID").value);
			}
		}
		//Neu goi form popup dua vao ten ung vien
		else if (strField == "CanName")
		{
			if (trim(document.getElementById("_ctl0:RE_CanHeader:txtCandidateName").value) == trim(document.getElementById("_ctl0:RE_CanHeader:txtCandidateName").title))
				strUrl = 'MdlRE/CanPopup.ascx&FunctionID=7001';
			else
			{
				strUrl = 'MdlRE/CanPopup.ascx&FunctionID=7001&CanName=' + trim(document.getElementById("_ctl0:RE_CanHeader:txtCandidateName").value) + '&Return=1';
			}
		}
		if ('<%=Request.Params["IsTermination"]%>' != "")
			strUrl = strUrl + "&IsTermination=1"
		ShowDialog('FormPage.aspx?Ascx=' + strUrl)

	} 
	
</SCRIPT>
<%if (Session["CanID"] == null && Request.Params["FunctionID"].Trim() != "152" && Request.Params["ModuleID"].Trim() != "SI"){%>
<SCRIPT language="javascript">	
	document.getElementById('btnSearchByID').click();
</SCRIPT>
<%} else {%>
<SCRIPT language="javascript">	
	document.getElementById("_ctl0:RE_CanHeader:txtCandidateCode").focus();
</SCRIPT>
<%}%>
