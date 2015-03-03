<%@ Page language="c#" Codebehind="ViewReports.aspx.cs" AutoEventWireup="false" Inherits="iHRPCore.Reports.SI.ViewReports" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ViewReports</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:Label id="Label1" style="Z-INDEX: 100; LEFT: 16px; POSITION: absolute; TOP: 16px" runat="server">LeaveDetailReport</asp:Label>
			<asp:Label id="Label20" style="Z-INDEX: 140; LEFT: 304px; POSITION: absolute; TOP: 400px" runat="server">Dept</asp:Label>
			<asp:TextBox id="txtDept" style="Z-INDEX: 139; LEFT: 344px; POSITION: absolute; TOP: 400px" runat="server"
				Width="50px">OP</asp:TextBox>
			<asp:Button id="Button7" style="Z-INDEX: 138; LEFT: 120px; POSITION: absolute; TOP: 472px" runat="server"
				Text="Show"></asp:Button>
			<asp:TextBox id="txtLg7" style="Z-INDEX: 137; LEFT: 80px; POSITION: absolute; TOP: 472px" runat="server"
				Width="24px">EN</asp:TextBox>
			<asp:Label id="Label19" style="Z-INDEX: 136; LEFT: 16px; POSITION: absolute; TOP: 472px" runat="server">Language</asp:Label>
			<asp:TextBox id="txtY3" style="Z-INDEX: 134; LEFT: 248px; POSITION: absolute; TOP: 400px" runat="server"
				Width="40px">2005</asp:TextBox>
			<asp:Label id="Label17" style="Z-INDEX: 133; LEFT: 208px; POSITION: absolute; TOP: 400px" runat="server">Year</asp:Label>
			<asp:TextBox id="txtM1" style="Z-INDEX: 131; LEFT: 168px; POSITION: absolute; TOP: 400px" runat="server"
				Width="24px">11</asp:TextBox>
			<asp:Label id="Label16" style="Z-INDEX: 130; LEFT: 120px; POSITION: absolute; TOP: 400px" runat="server">Month</asp:Label>
			<asp:Button id="Button6" style="Z-INDEX: 129; LEFT: 408px; POSITION: absolute; TOP: 400px" runat="server"
				Text="Show"></asp:Button>
			<asp:TextBox id="txtLg6" style="Z-INDEX: 128; LEFT: 80px; POSITION: absolute; TOP: 400px" runat="server"
				Width="24px">EN</asp:TextBox>
			<asp:Label id="Label15" style="Z-INDEX: 127; LEFT: 16px; POSITION: absolute; TOP: 400px" runat="server">Language</asp:Label>
			<asp:Button id="Button5" style="Z-INDEX: 125; LEFT: 120px; POSITION: absolute; TOP: 328px" runat="server"
				Text="Show"></asp:Button>
			<asp:TextBox id="txtLg5" style="Z-INDEX: 124; LEFT: 80px; POSITION: absolute; TOP: 328px" runat="server"
				Width="24px">EN</asp:TextBox>
			<asp:Label id="Label13" style="Z-INDEX: 123; LEFT: 16px; POSITION: absolute; TOP: 328px" runat="server">Language</asp:Label>
			<asp:Button id="Button4" style="Z-INDEX: 121; LEFT: 120px; POSITION: absolute; TOP: 256px" runat="server"
				Text="Show"></asp:Button>
			<asp:TextBox id="txtLg4" style="Z-INDEX: 120; LEFT: 80px; POSITION: absolute; TOP: 256px" runat="server"
				Width="24px">EN</asp:TextBox>
			<asp:Label id="Label11" style="Z-INDEX: 119; LEFT: 16px; POSITION: absolute; TOP: 256px" runat="server">Language</asp:Label>
			<asp:TextBox id="txtLg3" style="Z-INDEX: 117; LEFT: 80px; POSITION: absolute; TOP: 184px" runat="server"
				Width="24px">EN</asp:TextBox>
			<asp:Label id="Label9" style="Z-INDEX: 116; LEFT: 16px; POSITION: absolute; TOP: 184px" runat="server">Language</asp:Label>
			<asp:TextBox id="TextBox1" style="Z-INDEX: 110; LEFT: 176px; POSITION: absolute; TOP: 112px"
				runat="server" Width="24px">EN</asp:TextBox>
			<asp:Button id="Button3" style="Z-INDEX: 115; LEFT: 120px; POSITION: absolute; TOP: 184px" runat="server"
				Text="Show"></asp:Button>
			<asp:Button id="Button2" style="Z-INDEX: 113; LEFT: 216px; POSITION: absolute; TOP: 112px" runat="server"
				Text="Show"></asp:Button>
			<asp:Label id="Label7" style="Z-INDEX: 112; LEFT: 16px; POSITION: absolute; TOP: 112px" runat="server">Year</asp:Label>
			<asp:TextBox id="txtY2" style="Z-INDEX: 111; LEFT: 56px; POSITION: absolute; TOP: 112px" runat="server"
				Width="40px">2005</asp:TextBox>
			<asp:Label id="Label6" style="Z-INDEX: 102; LEFT: 16px; POSITION: absolute; TOP: 40px" runat="server">Year</asp:Label>
			<asp:TextBox id="txtLg2" style="Z-INDEX: 109; LEFT: 176px; POSITION: absolute; TOP: 112px" runat="server"
				Width="24px">EN</asp:TextBox>
			<asp:Label id="Label2" style="Z-INDEX: 101; LEFT: 16px; POSITION: absolute; TOP: 40px" runat="server">Year</asp:Label>
			<asp:TextBox id="txtY1" style="Z-INDEX: 103; LEFT: 56px; POSITION: absolute; TOP: 40px" runat="server"
				Width="40px">2005</asp:TextBox>
			<asp:Label id="Label3" style="Z-INDEX: 104; LEFT: 112px; POSITION: absolute; TOP: 40px" runat="server">Language</asp:Label>
			<asp:TextBox id="txtLg1" style="Z-INDEX: 105; LEFT: 176px; POSITION: absolute; TOP: 40px" runat="server"
				Width="24px">EN</asp:TextBox>
			<asp:Button id="Button1" style="Z-INDEX: 106; LEFT: 216px; POSITION: absolute; TOP: 40px" runat="server"
				Text="Show"></asp:Button>
			<asp:Label id="Label4" style="Z-INDEX: 107; LEFT: 16px; POSITION: absolute; TOP: 88px" runat="server">LeaveRecordByDepartment</asp:Label>
			<asp:Label id="Label5" style="Z-INDEX: 108; LEFT: 112px; POSITION: absolute; TOP: 112px" runat="server">Language</asp:Label>
			<asp:Label id="Label8" style="Z-INDEX: 114; LEFT: 16px; POSITION: absolute; TOP: 160px" runat="server">TMS_rptHeadcountBy_Location_Deparment.rpt</asp:Label>
			<asp:Label id="Label10" style="Z-INDEX: 118; LEFT: 16px; POSITION: absolute; TOP: 232px" runat="server">TMS_rptHeadcountBy_Position_Deparment.rpt</asp:Label>
			<asp:Label id="Label12" style="Z-INDEX: 122; LEFT: 16px; POSITION: absolute; TOP: 304px" runat="server">TMS_rptSummaryOfStaffByBuilding.rpt</asp:Label>
			<asp:Label id="Label14" style="Z-INDEX: 126; LEFT: 16px; POSITION: absolute; TOP: 376px" runat="server">TMS_rptWorkPoint.rpt</asp:Label>
			<asp:Label id="Label18" style="Z-INDEX: 135; LEFT: 16px; POSITION: absolute; TOP: 448px" runat="server">TMS_rptSignatureSalaryTable.rpt</asp:Label>
		</form>
	</body>
</HTML>
