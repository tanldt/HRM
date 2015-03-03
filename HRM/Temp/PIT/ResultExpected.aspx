<%@ Page language="c#" Codebehind="ResultExpected.aspx.cs" AutoEventWireup="false" Inherits="Temp.PIT.ResultExpected" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ResultExpected</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
		Màn hình kế hoạch kinh doanh của nhóm
			<table width="100%">
				<tr>
					<td colspan="2">
						<TABLE id="tblInfo" cellSpacing="0" cellPadding="1" width="98%" border="0">
							<TR>
								<TD>Company</TD>
								<TD>FPT</TD>
								<TD></TD>
								<td>Group Bussiness</td>
								<td>Phần mềm NS-Tiền lương</td>
							</TR>
							<TR>
								<TD><asp:label id="Label1" runat="server" CssClass="labelRequire" Width="100%">Year</asp:label></TD>
								<TD>
									<asp:DropDownList id="DropDownList1" runat="server">
										<asp:ListItem Value="2008">2008</asp:ListItem>
										<asp:ListItem Value="2009">2009</asp:ListItem>
										<asp:ListItem Value="2010">2010</asp:ListItem>
									</asp:DropDownList></TD>
							</TR>
							<TR>
								<TD><asp:label id="lblLastName" runat="server" CssClass="labelRequire" Width="100%">Month/Year</asp:label></TD>
								<TD><asp:textbox id="txtLastName" runat="server" CssClass="input" Width="100%" MaxLength="35"></asp:textbox></TD>
								<TD></TD>
								<TD><asp:label id="lblFirstName" runat="server" CssClass="labelRequire" Width="100%">Total turnover derived in period</asp:label></TD>
								<TD><asp:textbox id="txtFirstName" runat="server" CssClass="input" Width="100%" MaxLength="15"></asp:textbox></TD>
							</TR>
							<TR>
								<TD><asp:label id="Label11" runat="server" CssClass="label">Total expenses incured in period</asp:label></TD>
								<TD><asp:textbox id="txtPassNo" runat="server" CssClass="input" Width="87px" MaxLength="20"></asp:textbox>&nbsp;&nbsp;&nbsp;</TD>
								<TD></TD>
								<TD><asp:label id="Label12" runat="server" CssClass="label">Taxable income</asp:label></TD>
								<TD><asp:textbox id="txtIssueDate_Pass" onblur="JavaScript:CheckDate(this)" runat="server" CssClass="input"
										Width="80px" MaxLength="10"></asp:textbox><INPUT class=btnCal onclick="javascript:popUpCalendar(<%=this.txtIssueDate_Pass.ClientID%>);" type=button></TD>
							</TR>
							<TR>
								<TD vAlign="top"><asp:label id="lblNote" runat="server" CssClass="label" Width="100%">Note</asp:label></TD>
								<TD vAlign="top" colSpan="4"><asp:textbox id="txtNote" runat="server" CssClass="input" Width="100%" MaxLength="255"></asp:textbox></TD>
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
								<td>Year</td>
								<td>Month</td>
								<td>Total turnover derived</td>
								<td>Total expenses incured</td>
								<td>Edit</td>
								<td>delete</td>
								<td>Select</td>
							</tr>
							<tr>
								<td>1</td>
								<td>2008</td>
								<td>01</td>
								<td>2,000,000,000</td>
								<td>500,000,000</td>
								<td><a href="#"> Edit</a></td>
								<td><a href="#"> Delete</a></td>
								<td><input type="checkbox">
								</td>
							</tr>
							<tr>
								<td>2</td>
								<td>2008</td>
								<td>02</td>
								<td>2,000,000,000</td>
								<td>500,000,000</td>
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
