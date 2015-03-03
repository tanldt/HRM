<%@ Page language="c#" Codebehind="AddGroupBussiness.aspx.cs" AutoEventWireup="false" Inherits="Temp.PIT.AddGroupBussiness" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AddGroupBussiness</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			Màn hình lập nhóm kinh doanh
			<table width="100%">
				<tr>
					<td>
						Company
					</td>
					<td>
						<asp:DropDownList id="DropDownList1" runat="server">
							<asp:ListItem Value="FPT">FPT</asp:ListItem>
							<asp:ListItem Value="KPMG">KPMG</asp:ListItem>
						</asp:DropDownList>
					</td>
				</tr>
				<tr>
					<td>
						Group business Name
					</td>
					<td>
						<asp:TextBox id="Textbox3" runat="server"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td>
						Decription
					</td>
					<td>
						<asp:TextBox id="Textbox2" TextMode="MultiLine" runat="server" Width="639px" Height="64px"></asp:TextBox>
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
								<td>Group Business</td>
								<td>Input Personnel</td>
								<td>Input Expected turnover deriving</td>
								<td>Input&nbsp;result turnover deriving</td>
								<td>Edit</td>
								<td>delete</td>
								<td>Select</td>
							</tr>
							<tr>
								<td>1</td>
								<td>bất động sản</td>
								<td><a href="AddPersonnel.aspx"> Add</a></td>
								<td><a href="ResultExpected.aspx"> Add</a></td>
								<td><a href="ResultBusiness.aspx"> Add</a></td>
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
