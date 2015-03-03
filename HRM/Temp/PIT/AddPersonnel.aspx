<%@ Page language="c#" Codebehind="AddPersonnel.aspx.cs" AutoEventWireup="false" Inherits="Temp.PIT.AddPersonnel" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AddPersonnel</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table width="100%">
				<tr>
					<td colspan="2">
						<b>Màn hình thêm Nhân viên vào nhóm</b>
						<TABLE id="tblInfo" cellSpacing="0" cellPadding="1" width="98%" border="0">
							<TR>
								<TD>Company</TD>
								<TD>FPT</TD>
								<TD></TD>
								<td>Group Bussiness</td>
								<td>Phần mềm NS-Tiền lương</td>
							</TR>
							<TR>
								<TD colspan="2"><asp:checkbox id="Checkbox2" runat="server" CssClass="checkbox" Text="Same Company?"></asp:checkbox></TD>
								<TD></TD>
								<td><asp:label id="Label16" runat="server" CssClass="labelRequire" Width="100%">Emp Code</asp:label></td>
								<td><asp:textbox id="Textbox1" runat="server" CssClass="input" Width="92%" MaxLength="255" ReadOnly="True"></asp:textbox><asp:button id="Button5" runat="server" CssClass="search" Text="..." Height="20px"></asp:button></td>
							</TR>
							<TR>
								<TD><asp:label id="lblLastName" runat="server" CssClass="labelRequire" Width="100%">Last Name</asp:label></TD>
								<TD><asp:textbox id="txtLastName" runat="server" CssClass="input" Width="100%" MaxLength="35"></asp:textbox></TD>
								<TD></TD>
								<TD><asp:label id="lblFirstName" runat="server" CssClass="labelRequire" Width="100%">First Name</asp:label></TD>
								<TD><asp:textbox id="txtFirstName" runat="server" CssClass="input" Width="100%" MaxLength="15"></asp:textbox></TD>
							</TR>
							<TR>
								<TD><asp:label id="lblGender" runat="server" CssClass="label" Width="100%">Gender</asp:label></TD>
								<TD><asp:dropdownlist id="cboGender" runat="server" CssClass="select" Width="50px"></asp:dropdownlist><asp:label id="lblRelationStatus" runat="server" CssClass="label">Status</asp:label>&nbsp;<asp:dropdownlist id="cboStatus" runat="server" CssClass="select" Width="69px"></asp:dropdownlist>&nbsp;&nbsp;&nbsp;</TD>
								<TD></TD>
								<TD><asp:label id="Label4" runat="server" CssClass="labelRequire" Width="100%">YOB</asp:label></TD>
								<TD><asp:textbox id="txtDOB" runat="server" CssClass="input" Width="75px" MaxLength="4"></asp:textbox>&nbsp;<INPUT class="btnCal" style="DISPLAY: none" onclick="ShowCalendar('_ctl0_txtDOB')" type="button">&nbsp;
									<asp:label id="lblAge" runat="server" CssClass="label">Age</asp:label>&nbsp;
									<asp:textbox id="txtAge" runat="server" CssClass="input" Width="37px" ReadOnly="True"></asp:textbox></TD>
							</TR>
							<TR>
								<TD><asp:label id="lblIDCardNo" runat="server" CssClass="label">ID No</asp:label></TD>
								<TD><asp:textbox id="txtIDNo" runat="server" CssClass="input" Width="87px" MaxLength="20"></asp:textbox>&nbsp;
									<asp:label id="Label9" runat="server" CssClass="label">Issue date</asp:label><asp:textbox id="txtIssueDate_IDNo" onblur="JavaScript:CheckDate(this)" runat="server" CssClass="input"
										Width="80px" MaxLength="10"></asp:textbox><INPUT class=btnCal onclick="javascript:popUpCalendar(<%=this.txtIssueDate_IDNo.ClientID%>);" type=button></TD>
								<TD></TD>
								<TD><asp:label id="Label10" runat="server" CssClass="label">Issue Place</asp:label></TD>
								<TD><asp:textbox id="txtIssuePlace_IDNo" runat="server" CssClass="input" Width="100%" MaxLength="150"></asp:textbox></TD>
							</TR>
							<TR>
								<TD colSpan="5">
									<HR width="100%" SIZE="1">
								</TD>
							</TR>
							<TR>
								<TD><asp:label id="Label11" runat="server" CssClass="label">ID Passport</asp:label></TD>
								<TD><asp:textbox id="txtPassNo" runat="server" CssClass="input" Width="87px" MaxLength="20"></asp:textbox>&nbsp;&nbsp;&nbsp;</TD>
								<TD></TD>
								<TD><asp:label id="Label12" runat="server" CssClass="label">Issue date</asp:label></TD>
								<TD><asp:textbox id="txtIssueDate_Pass" onblur="JavaScript:CheckDate(this)" runat="server" CssClass="input"
										Width="80px" MaxLength="10"></asp:textbox><INPUT class=btnCal onclick="javascript:popUpCalendar(<%=this.txtIssueDate_Pass.ClientID%>);" type=button></TD>
							</TR>
							<TR>
								<TD><asp:label id="Label13" runat="server" CssClass="label">Effective date</asp:label></TD>
								<TD><asp:textbox id="txtEffectiveDate_Pass" onblur="JavaScript:CheckDate(this)" runat="server" CssClass="input"
										Width="80px" MaxLength="10"></asp:textbox><INPUT class=btnCal onclick="javascript:popUpCalendar(<%=this.txtEffectiveDate_Pass.ClientID%>);" type=button></TD>
								<TD></TD>
								<TD><asp:label id="Label15" runat="server" CssClass="label">Expired date</asp:label></TD>
								<TD><asp:textbox id="txtExpiredDate_Pass" onblur="JavaScript:CheckDate(this)" runat="server" CssClass="input"
										Width="80px" MaxLength="10"></asp:textbox><INPUT class=btnCal onclick="javascript:popUpCalendar(<%=this.txtExpiredDate_Pass.ClientID%>);" type=button></TD>
							</TR>
							<TR>
								<TD colSpan="5">
									<HR width="100%" SIZE="1">
								</TD>
							</TR>
							<TR>
								<TD><asp:label id="Label7" runat="server" CssClass="label" Width="100%">Telephone</asp:label></TD>
								<TD><asp:textbox id="txtTelephone" runat="server" CssClass="input" Width="30%" MaxLength="100" Rows="2"></asp:textbox></TD>
								<TD></TD>
								<TD><asp:label id="Label8" runat="server" CssClass="label" Width="100%">Blood type</asp:label></TD>
								<TD><asp:dropdownlist id="cboLSBloodTypeID" runat="server" CssClass="combo" Width="100%">
										<asp:ListItem Value="O">O</asp:ListItem>
										<asp:ListItem Value="A">A</asp:ListItem>
										<asp:ListItem Value="B">B</asp:ListItem>
										<asp:ListItem Value="AB">AB</asp:ListItem>
									</asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD><asp:label id="Label5" runat="server" CssClass="label" Width="100%">Address</asp:label></TD>
								<TD><asp:textbox id="txtAddress" runat="server" CssClass="input" Width="100%" MaxLength="150"></asp:textbox></TD>
								<TD></TD>
								<TD><asp:label id="Label6" runat="server" CssClass="label" Width="100%">Contact</asp:label></TD>
								<TD><asp:textbox id="txtContact" runat="server" CssClass="input" Width="100%" MaxLength="150"></asp:textbox></TD>
							</TR>
							<TR id="trAdBefore" style="DISPLAY: none" runat="server">
								<TD><asp:checkbox id="chkAdBefore" runat="server" CssClass="chkAdmin" Width="10%" Checked="True"></asp:checkbox><asp:label id="Label1" runat="server" CssClass="label" Width="100%">Before 75</asp:label></TD>
								<TD vAlign="top"><asp:textbox id="txtBefore75" style="POSITION: absolute" runat="server" CssClass="input" Width="100%"
										MaxLength="100"></asp:textbox></TD>
								<TD></TD>
								<TD><asp:label id="Label2" runat="server" CssClass="label" Width="100%">After 75</asp:label></TD>
								<TD vAlign="top"><asp:textbox id="txtAfter75" style="POSITION: absolute" runat="server" CssClass="input" Width="100%"
										MaxLength="100"></asp:textbox></TD>
							</TR>
							<TR>
								<TD><asp:label id="Label3" runat="server" CssClass="label" Width="100%">Occupation</asp:label></TD>
								<TD vAlign="top"><asp:textbox id="txtOccupation" style="POSITION: absolute" runat="server" CssClass="input" Width="100%"
										MaxLength="150"></asp:textbox></TD>
								<TD></TD>
								<TD><asp:label id="lblContact" runat="server" CssClass="label" Width="100%">Work Place</asp:label></TD>
								<TD vAlign="top"><asp:textbox id="txtWorkPlace" style="POSITION: absolute" runat="server" CssClass="input" Width="100%"
										MaxLength="255" Rows="2"></asp:textbox></TD>
							</TR>
							<TR>
								<TD vAlign="top"><asp:label id="lblNote" runat="server" CssClass="label" Width="100%">Note</asp:label></TD>
								<TD vAlign="top" colSpan="4"><asp:textbox id="txtNote" runat="server" CssClass="input" Width="100%" MaxLength="255"></asp:textbox></TD>
							</TR>
							<TR>
								<TD vAlign="top">
									<asp:checkbox id="Checkbox1" runat="server" Text="Team leader" CssClass="chkAdmin" Checked="True"></asp:checkbox></TD>
								<TD vAlign="top">The ratio of income
									<asp:textbox id="Textbox10" runat="server" CssClass="input" Width="100%" MaxLength="255"></asp:textbox>%</TD>
							</TR>
						</TABLE>
					</td>
				</tr>
				<tr>
					<td colspan="2" align="center">
						<asp:Button id="Button1" runat="server" Text="Add new"></asp:Button>&nbsp;
						<asp:Button id="Button2" runat="server" Text="Search"></asp:Button>&nbsp;
						<asp:Button id="Button3" runat="server" Text="Save"></asp:Button>&nbsp;
						<asp:Button id="Button4" runat="server" Text="Delete"></asp:Button>
					</td>
				</tr>
				<TR>
					<TD align="center" colSpan="2">
						<table width="100%" border="1">
							<tr bgcolor="silver">
								<td>Seq</td>
								<td>Full name</td>
								<td>Team leader</td>
								<td>Input Dependant</td>
								<td>Edit</td>
								<td>delete</td>
								<td>Select</td>
							</tr>
							<tr>
								<td>1</td>
								<td>Lê Tân</td>
								<td>X</td>
								<td><a href="AddDependant.aspx"> Add</a></td>
								<td><a href="#"> Edit</a></td>
								<td><a href="#"> Delete</a></td>
								<td><input type="checkbox">
								</td>
							</tr>
							<tr>
								<td>2</td>
								<td>Nguyen Van A</td>
								<td></td>
								<td><a href="AddDependant.aspx"> Add</a></td>
								<td><a href="#"> Edit</a></td>
								<td><a href="#"> Delete</a></td>
								<td><input type="checkbox">
								</td>
							</tr>
						</table>
					</TD>
				</TR>
				<TR>
					<TD align="center" colSpan="2"></TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>
