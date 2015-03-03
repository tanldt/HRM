<%@ Control Language="c#" AutoEventWireup="false" Codebehind="EmpHeader1.ascx.cs" Inherits="iHRPCore.Include.EmpHeader1" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
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
			<table cellSpacing="0" cellPadding="1" width="100%" border="0">
				<tr>
					<td>
						<TABLE id="Table1" cellSpacing="0" cellPadding="1" width="100%" border="0">
							<TR valign="top">
								<TD width="15%"><asp:label id="Label7" CssClass="label" runat="server">Emp ID</asp:label></TD>
								<TD width="30%"><asp:textbox id="txtEmpID" onkeypress="checkKey('btnSearchByID')" CssClass="input" runat="server"
										Width="70%"></asp:textbox><INPUT class="search" onclick="javascript:OpenWindowEmp('EmpID')" id="btnSearchByID" type="button"
										value="..."></TD>
								<TD width="15%"><asp:label id="Label2" CssClass="label" runat="server">Emp Name</asp:label></TD>
								<TD><asp:textbox id="txtEmpName" onkeypress="checkKey('btnSearchByName')" CssClass="input" runat="server"
										Width="80%"></asp:textbox><INPUT class="search" onclick="javascript:OpenWindowEmp('EmpName')" id="btnSearchByName"
										type="button" value="..."></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
				<tr>
					<td>
						<hr width="100%">
					</td>
				</tr>
			</table>
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
		ShowDialog('FormPage.aspx?Ascx=' + strUrl)

	} 
	
</script>
