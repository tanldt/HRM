<%@ Page language="c#" Codebehind="ViewPolicy.aspx.cs" AutoEventWireup="false" Inherits="iHRPCore.ViewPolicy" %>
<%@ Register TagPrefix="uc1" TagName="Top" Src="Include/Top.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopModule" Src="Include/TopModule.ascx" %>
<%@ Register TagPrefix="FPT" Namespace="FPTToolWeb.HitCounter" Assembly="FPTToolWeb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Homepage</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link 
href='<%= Request.ApplicationPath +"/Include/myStyles.css"%>' type=text/css 
rel=stylesheet>
		<script language="javascript">
	var Root = "<%=Request.ApplicationPath%>";
		</script>
		<script language=javaScript 
src="<%=Request.ApplicationPath%>/CommonScript/TreeScripts/ua.js"></script>
		<script language=javaScript 
src="<%=Request.ApplicationPath%>/CommonScript/TreeScripts/ftiens4.js"></script>
		<script language=javascript 
src="<%=Request.ApplicationPath%>/CommonScript/stm31.js"></script>
		<script language=javascript 
src="<%=Request.ApplicationPath%>/CommonScript/parameter.js"></script>
		<LINK href="<%=Request.ApplicationPath%>/CommonScript/TreeScripts/TreeStyle.css" 
type=text/css rel=StyleSheet>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="770" align="center" bgColor=#ffffff border="0">
				<TR>
					<TD colSpan="3"><IMG alt="" src="images/Home_01.jpg"></TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<TABLE cellSpacing="0" cellPadding="0" width="169" bgColor="#ffffff" border="0">
							<TR>
								<TD style="HEIGHT: 13px" width="14" background="images/tblRight_01.jpg"></TD>
								<TD style="HEIGHT: 13px" width="100%" background="images/tblRight_03.jpg" colSpan="3"></TD>
								<TD style="HEIGHT: 13px" width="14" background="images/tblRight_05.jpg"></TD>
							</TR>
							<TR>
								<TD width="14" background="images/tblRight_11.jpg" height="100%"></TD>
								<TD width="100%" colSpan="3">
									<%
													if (Session["AccountLogin"] == null){
												%>
									<!-- Nếu chưa Login-->
									<table cellSpacing="0" cellPadding="0" width="100%">
										<tr style="DISPLAY: none">
											<td width="40%" height="30">
												<%
																if (Session["LangID"] == null || Session["LangID"] == "EN"){
															%>
												<asp:label id="Label4" runat="server" ForeColor="#2A5C8F" Font-Bold="True" Font-Names="Arial"
													Font-Size="8pt">Company</asp:label>
												<%} else {%>
												<asp:label id="Label3" runat="server" ForeColor="#2A5C8F" Font-Bold="True" Font-Names="Arial"
													Font-Size="8pt">Công ty</asp:label>
												<%}%>
											</td>
											<td width="60%"><asp:dropdownlist id="cboCompany" runat="server" Width="100%" Height="22px"></asp:dropdownlist></td>
										</tr>
										<tr>
											<td width="40%" height="30">
												<%
																if (Session["LangID"] == null || Session["LangID"] == "EN"){
															%>
												<asp:label id="Label1" runat="server" ForeColor="#2A5C8F" Font-Bold="True" Font-Names="Arial"
													Font-Size="8pt">Account</asp:label>
												<%} else {%>
												<asp:label id="Label5" runat="server" ForeColor="#2A5C8F" Font-Bold="True" Font-Names="Arial"
													Font-Size="8pt">Tên đăng nhập</asp:label>
												<%}%>
											</td>
											<td><asp:textbox id="txtUserName" style="FONT-SIZE: 8pt; FONT-FAMILY: Arial" Width="100%" Height="22px"
													Runat="server" CssClass="input"></asp:textbox></td>
										</tr>
										<tr>
											<td>
												<%
																if (Session["LangID"] == null || Session["LangID"] == "EN"){
															%>
												<asp:label id="Label2" runat="server" ForeColor="#2A5C8F" Font-Bold="True" Font-Names="Arial"
													Font-Size="8pt">Password</asp:label>
												<%} else {%>
												<asp:label id="Label6" runat="server" ForeColor="#2A5C8F" Font-Bold="True" Font-Names="Arial"
													Font-Size="8pt">Mật mã</asp:label>
												<%}%>
											</td>
											<td><asp:textbox id="txtPassword" style="FONT-SIZE: 8pt; FONT-FAMILY: Arial" Width="100%" Height="22px"
													Runat="server" CssClass="input" TextMode="Password"></asp:textbox></td>
										</tr>
										<tr>
											<td align="center" colSpan="2"><asp:imagebutton id="btnLogin" runat="server" ImageUrl="IMAGES/Button_Login.gif"></asp:imagebutton></td>
										</tr>
										<tr>
											<td colSpan="2"><asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></td>
										</tr>
									</table>
									<!-- End Nếu chưa Login-->
									<%} else{%>
									<asp:linkbutton id="btnLogout" runat="server" CssClass="btnLogout">Logout</asp:linkbutton>
									<%}%>
								</TD>
								<TD width="14" background="images/tblRight_15.jpg" height="100%"></TD>
							</TR>
							<TR>
								<TD><IMG height="24" alt="" src="images/tblRight_21.jpg" width="14"></TD>
								<TD><IMG height="24" alt="" src="images/tblRight_22.jpg" width="38"></TD>
								<TD width="100%" background="images/tblRight_23.jpg">&nbsp;</TD>
								<TD><IMG height="24" alt="" src="images/tblRight_24.jpg" width="51"></TD>
								<TD><IMG height="24" alt="" src="images/tblRight_25.jpg" width="14"></TD>
							</TR>
						</TABLE>
						<TABLE cellSpacing="0" cellPadding="0" width="169" border="0">
							<tr>
								<td>
									<div style="WIDTH: 100%; TEXT-ALIGN: left">	<div class="menuheader">Modules</div><div id="LeftMNav"><ul>	<%if(tblModule.Rows.Count > 0){	for (i=0; i< tblModule.Rows.Count; i++){%><li><a title="<%=tblModule.Rows[i]["ModuleName"]%>" href='<%=tblModule.Rows[i]["ModuleHome"]%>?ModuleID=<%=tblModule.Rows[i]["ModuleID"]%>&amp;FunctionID=<%=tblModule.Rows[i]["FunctionID"]%>&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&amp;ParentID=<%=tblModule.Rows[i]["Parent"]%>&amp;Ascx=<%=tblModule.Rows[i]["Ascx"]%>'><span class="arw">&raquo;</span>&nbsp;<%=tblModule.Rows[i]["ModuleName"]%></a></li><% } }%>
										<li><div class="menuheader">Access times</div></li>
										</ul>
										</div>
									</div>
								</td>
							</tr>
						</TABLE>
					</TD>
					<TD vAlign="top" width="601" background="images/Background_Home_Policy.jpg"> <!-- Template Center-->
						<table>
							<tr>
								<td height="40" colspan="2"></td>
							</tr>
							<tr>
								<td width="320"><asp:label id="lblFormPolicy" Visible=false Runat="server">Form </asp:label></td>
								<td><asp:LinkButton Runat="server"  ID="btnPolicy"> Back </asp:LinkButton>&nbsp;&nbsp;
									<asp:LinkButton Runat="server" CssClass="btnForm" Visible=false  ID="btnForm"></asp:LinkButton>
								</td>
							</tr>
							<tr>
								<td colspan=2 height=20px> </td>
							</tr>
							<tr>
								<td align="left" colspan="2"><asp:datalist id="ListPolicy" Width="100%" Runat="server">
										<ItemTemplate>
											<TABLE id="tblFirstNews" cellSpacing="0" cellPadding="0" width="100%" border="0">
												<tr>
													<td width=150px></td>
													<td>
														<asp:HyperLink Font-Size=10pt ForeColor=#446A97 Target='<%# DataBinder.Eval(Container.DataItem,"Target") %>' id=hpSubject runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"FileAttach")%>'>
															<%# DataBinder.Eval(Container.DataItem,"PolicyName") %>
														</asp:HyperLink>
													</td>
												</tr>
											</TABLE>
										</ItemTemplate>
									</asp:datalist></td>
							</tr>
						</table>
					</TD>
					<!-- End Template Center--> </TD> 
					<!-- End Template Center--> </TD></TR>
				<TR>
					<TD colSpan="3"><IMG alt="" src="images/Home_05.jpg" width="770"></TD>
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
