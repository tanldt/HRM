<%@ Page language="c#" Codebehind="Home.aspx.cs" AutoEventWireup="false" Inherits="iHRPCore.Home" %>
<%@ Register TagPrefix="uc1" TagName="Top" Src="Include/Top.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopModule" Src="Include/TopModule.ascx" %>
<%@ Register TagPrefix="FPT" Namespace="FPTToolWeb.HitCounter" Assembly="FPTToolWeb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>iHRP_KPMG</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href='<%= Request.ApplicationPath +"/Include/myStyles.css"%>' type=text/css rel=stylesheet>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">				
		<form id="Form1" method="post" runat="server">
			<TABLE WIDTH="770" BORDER="0" CELLPADDING="0" CELLSPACING="0" align="center" >				
				<TR>
					<TD COLSPAN="3">
						<IMG alt="" src="images/Home_01.jpg" >
					</TD>
				</TR>
				<TR>
					<!--<TD valign=top  background="images\1pixel.jpg">-->
						<td valign=top bgcolor=#bacee0>
						<TABLE WIDTH="169" BORDER="0" CELLPADDING="0" CELLSPACING="0" bgcolor="#ffffff">
							<TR>
								<TD background="images/tblRight_01.jpg" WIDTH="14" style="HEIGHT:13px"></TD>
								<TD background="images/tblRight_03.jpg" colspan="3" width="100%" style="HEIGHT:13px"></TD>
								<TD background="images/tblRight_05.jpg" WIDTH="14" style="HEIGHT:13px"></TD>
							</TR>
							<TR>
								<TD background="images/tblRight_11.jpg" WIDTH="14" height="100%"></TD>
								<TD width="100%" colspan="3">
									<%
													if (Session["AccountLogin"] == null){
												%>
									<!-- Nếu chưa Login-->
									<table cellpadding="0" cellspacing="0" width="100%">
										<tr style="display:none">
											<td height="30" width="40%">
												<%
																if (Session["LangID"] == null || Session["LangID"] == "EN"){
															%>
												<asp:label id="Label4" ForeColor="#2A5C8F" Font-Bold="True" runat="server" Font-Names="Arial"
													Font-Size="8pt">Company</asp:label>
												<%} else {%>
												<asp:label id="Label3" ForeColor="#2A5C8F" Font-Bold="True" runat="server" Font-Names="Arial"
													Font-Size="8pt">Công ty</asp:label>
												<%}%>
											</td>
											<td width="60%"><asp:dropdownlist id="cboCompany" runat="server" Width="100%" Height="22px"></asp:dropdownlist></td>
										</tr>
										<tr>
											<td width="40%">
												<%
																if (Session["LangID"] == null || Session["LangID"] == "EN"){
															%>
												<asp:label id="Label1" ForeColor="#2A5C8F" Font-Bold="True" runat="server" Font-Names="Arial"
													Font-Size="8pt">Login Name</asp:label>
												<%} else {%>
												<asp:label id="Label5" ForeColor="#2A5C8F" Font-Bold="True" runat="server" Font-Names="Arial"
													Font-Size="8pt">Tên đăng nhập</asp:label>
												<%}%>
											</td>
											<td width="60%"><asp:textbox id="txtUserName" style="FONT-SIZE: 8pt; FONT-FAMILY: Arial" Runat="server" Width="100%"
													Height="22px" CssClass="input"></asp:textbox></td>
										</tr>
										<tr>
											<td>
												<%
																if (Session["LangID"] == null || Session["LangID"] == "EN"){
												%>
												<asp:label id="Label2" ForeColor="#2A5C8F" Font-Bold="True" runat="server" Font-Names="Arial"
													Font-Size="8pt">Password</asp:label>
												<%} else {%>
												<asp:label id="Label6" ForeColor="#2A5C8F" Font-Bold="True" runat="server" Font-Names="Arial"
													Font-Size="8pt">Mật mã</asp:label>
												<%}%>
											</td>
											<td><asp:textbox id="txtPassword" style="FONT-SIZE: 8pt; FONT-FAMILY: Arial" Runat="server" Width="100%"
													TextMode="Password" Height="22px" CssClass="input"></asp:textbox></td>
										</tr>
										<tr>
											<td colspan="2" align="center">
												<asp:imagebutton id="btnLogin" runat="server" ImageUrl="IMAGES/Button_Login.gif"></asp:imagebutton></td>
										</tr>
										<tr>
											<td colspan="2">
												<asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></td>
										</tr>
									</table>
									<!-- End Nếu chưa Login-->
									<%} else{%>
									<asp:linkbutton id="btnLogout" runat="server" CssClass="btnLogout">Logout</asp:linkbutton>
									<%}%>
								</TD>
								<TD background="images/tblRight_15.jpg" WIDTH="14" height="100%"></TD>
							</TR>
							<TR>
								<TD>
									<IMG SRC="images/tblRight_21.jpg" WIDTH="14" HEIGHT="24" ALT=""></TD>
								<TD>
									<IMG SRC="images/tblRight_22.jpg" WIDTH="38" HEIGHT="24" ALT=""></TD>
								<TD background="images/tblRight_23.jpg" width="100%">&nbsp;</TD>
								<TD>
									<IMG SRC="images/tblRight_24.jpg" WIDTH="51" HEIGHT="24" ALT=""></TD>
								<TD>								
									<IMG SRC="images/tblRight_25.jpg" WIDTH="14" HEIGHT="24" ALT=""></TD>
							</TR>
						</TABLE>
						<TABLE WIDTH="169" BORDER="0" CELLPADDING="0" CELLSPACING="0">
							<tr>
								<td>
								<div style="text-align: left; width: 100%;">
										<div class="menuheader">Module</div>
										<div id="LeftMNav"><ul>	<%if(tblModule.Rows.Count > 0){
											for (i=0; i< tblModule.Rows.Count; i++){
										%><li><a title="<%=tblModule.Rows[i]["ModuleName"]%>" href='<%=tblModule.Rows[i]["ModuleHome"]%>?ModuleID=<%=tblModule.Rows[i]["ModuleID"]%>&amp;FunctionID=<%=tblModule.Rows[i]["FunctionID"]%>&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&amp;ParentID=<%=tblModule.Rows[i]["Parent"]%>&amp;Ascx=<%=tblModule.Rows[i]["Ascx"]%>'><span class="arw">&raquo;</span>&nbsp;<%=tblModule.Rows[i]["ModuleName"]%></a></li><% } }%>
										<li><div class=menufooter>&nbsp;</div></li>
										</ul>
										</div>
									</div>
								</td>
							</tr>
						</TABLE>
						
					</TD>
					<TD valign="top" align=left>
						<!-- Template Center-->						
						<IMG SRC="images/Background_Home.jpg" height="480" width=606 ALT=""></TD>
						<!-- End Template Center-->
					</TD>
				</TR>
				<TR>
					<TD COLSPAN="3" ><IMG SRC="images/Home_05.jpg" ALT=""></TD>
				</TR>
			</TABLE>
		</form>
		<script language="javascript">
	function CheckLogin()
	{
		if (document.Form1.txtUserName.value == "")
		{
			alert("Input your username!");
			document.Form1.txtUserName.focus();
			return false;
		}
		if (document.Form1.txtPassword.value == "")
		{
			alert("Input your password!");
			document.Form1.txtPassword.focus();
			return false;
		}
	}
		</script>
		<%
		if (Session["AccountLogin"] == null){
		%>
		<script>
			document.Form1.txtUserName.focus();
		</script>
		<%}%>
		<script>
	function ChangeLanguage()
	{
		if ('<%=Session["LangID"]%>' == null || '<%=Session["LangID"]%>'  == "EN")
		{
			document.getElementById("btnLogout").innerHTML = "Logout (" + '<%=Session["AccountLogin"]%>' + ")"; 
		}
		else
		{
			document.getElementById("btnLogout").innerHTML = "Thoát (" + '<%=Session["AccountLogin"]%>' + ")"; 
		}
	}
	function Open()
	{   if (document.Form1.Select1.value!="")
		window.open(document.Form1.Select1.value)
		
	}
	if (document.getElementById("btnLogout") != null)
		ChangeLanguage();
		</script>
	</body>
</HTML>
