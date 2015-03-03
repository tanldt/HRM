<%@ Page language="c#" Codebehind="SYS_frmEmployeeList.aspx.cs" AutoEventWireup="false" Inherits="iHRPCore.SYS.SYS_frmEmployeeList" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SYS_frmEmployeeList</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Includes/myStyles.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../../Includes/common.js"></script>
		<script language="javascript" src="../../Includes/CheckDate.js"></script>
		<script language="javascript" src="../../Includes/CheckPositiveInteger.js"></script>
		<script language="javascript" src="../../Includes/Confirm_Delete.js"></script>
		<script language="javascript">
		function ChangeParentUMS(mEmpID, mVLName,mVMName,mVFName)
		{
			var value = mVLName + " " + mVFName;
			var mCtrName = "txtUserID";
			eval('txtUserID = opener.document.forms[0].' + mCtrName);
			if (txtUserID == null)
				return false;
			txtUserID.value = mEmpID;
			
			eval('txtEmpID = opener.document.forms[0].' + "txtEmpID");
			if (txtEmpID == null)
				return false;
			txtEmpID.value = mEmpID;
			
			eval('EMPLOYEENAME = opener.document.forms[0].' + "txtUserName");		
			if (EMPLOYEENAME == null) return false;
			EMPLOYEENAME.value = value;
			
			window.close();
		}
		</script>
		<script language="javascript">
function KeyDownHandler13_Cancel()
{
	// process only the Enter key
	if (event.keyCode == 13)
	{
		// cancel the default submit
		event.returnValue=false;
		event.cancel = true;
	}
}
		</script>
	</HEAD>
	<body onkeydown="KeyDownHandler13_Cancel()" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="500" align="center" border="0">
				<TR>
					<TD align="left" height="10">&nbsp;
						<asp:label id="Label1" runat="server" Font-Names="Arial" Width="80px" Font-Size="8pt">Mã nhân viên</asp:label><asp:textbox id="txtEmp_ID" runat="server" Font-Names="Arial" Width="152px" Font-Size="8pt" MaxLength="10"
							Height="20px"></asp:textbox>&nbsp;
						<asp:label id="Label2" runat="server" Font-Names="Arial" Width="81" Font-Size="8pt">Tên nhân viên</asp:label><asp:textbox id="txtEmpName" runat="server" Font-Names="Arial" Width="152px" Font-Size="8pt"
							MaxLength="20" Height="20px"></asp:textbox></TD>
				</TR>
				<TR>
					<TD align="center" height="10"></TD>
				</TR>
				<tr>
					<td align="center"><asp:button id="cmdWhere" runat="server" Font-Names="Arial" Width="89px" Font-Size="8pt" Height="24px"
							CssClass="ButtonCommand" Text="Lọc"></asp:button></td>
				</tr>
				<TR>
					<TD align="center" height="10"></TD>
				</TR>
				<tr>
					<td><asp:datagrid id="dtgEmpList" tabIndex="23" runat="server" Font-Names="Arial" Width="550px" Font-Size="8pt"
							PagerStyle-Mode="NumericPages" AutoGenerateColumns="False" CellPadding="2" HorizontalAlign="Center"
							AllowSorting="True" AllowPaging="True" BorderColor="#999999">
							<AlternatingItemStyle Font-Size="8pt" Font-Names="Arial" BackColor="#F6F6F7"></AlternatingItemStyle>
							<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" BackColor="#D3D3D7"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="Chọn">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" Width="25px"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Center" Width="25px"></ItemStyle>
									<ItemTemplate>
										<asp:Button id="cmdSelect" Width="25" Text="+" CssClass="btn" Runat="server"></asp:Button>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="EMPLOYEEID" HeaderText="M&#227; NV">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Center" Width="80px"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="VLASTNAME" HeaderText="Họ">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Justify" Width="80px"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="VMiddleName" HeaderText="T&#234;n l&#243;t">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Justify" Width="80px"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="VFIRSTNAME" HeaderText="T&#234;n">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Justify" Width="80px"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="LEVEL1_NAME" HeaderText="Ph&#242;ng ban">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Justify" Width="100px"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="LEVEL2_NAME" HeaderText="Xưởng/Kho/BP">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Justify" Width="100px"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
