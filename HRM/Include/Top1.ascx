<%@ Register TagPrefix="uc1" TagName="TopModule" Src="TopModule.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopMenu" Src="TopMenu.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Top1.ascx.cs" Inherits="iHRPCore.Pagelets.Top1" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<TABLE cellSpacing="0" cellPadding="0" width="770" border="0">
	<TR>
		<TD bgcolor="#2967b2" style="HEIGHT: 22px"><asp:imagebutton id="btn" runat="server" WIDTH="0px" ImageUrl="images/Banner_04.jpg" BorderWidth="0"></asp:imagebutton></TD>
		<TD style="HEIGHT: 22px"><asp:imagebutton id="imgbtnHome" runat="server" ImageUrl="..\Images\Banner_01.jpg"></asp:imagebutton></TD>
		<TD style="HEIGHT: 22px"><asp:imagebutton id="imgbtnEN" runat="server" ImageUrl="..\Images\Banner_02.jpg"></asp:imagebutton></TD>
		<TD style="HEIGHT: 22px"><asp:imagebutton id="imgbtnVN" runat="server" ImageUrl="..\Images\Banner_03.jpg"></asp:imagebutton></TD>
		<TD align="right" background="images/Banner_05.jpg" style="HEIGHT: 22px"><asp:linkbutton id="btnLogout" runat="server" ForeColor="white" Height="20" CssClass="btnLogout">Logout</asp:linkbutton>
		&nbsp;<asp:linkbutton id="btnChangePass" runat="server" ForeColor="white" Height="20" CssClass="btnLogout">[Change Pass]</asp:linkbutton> &nbsp;
		</TD>
	</TR>
	<TR>
		<TD colSpan="4"><asp:imagebutton id="imgbtnLogo" runat="server" ImageUrl="../images/Banner_06.jpg"></asp:imagebutton></TD>
		<TD><asp:imagebutton id="imgbtnTop02" runat="server" ImageUrl="..\Images\Banner_07.jpg"></asp:imagebutton></TD>
	</TR>
	<TR>
		<TD background="images/Banner_14.jpg" colSpan="5" height="22"><uc1:topmenu id="TopMenu1" runat="server"></uc1:topmenu></TD>
	</TR>
	<TR>
		<TD background="images/Banner_19.jpg" colSpan="5" height="12"></TD>
	</TR>
</TABLE>
<script>	
	function silent(){
		return false;
	}	
	//document.getElementById('Top1_imgbtnVN').focus();
	function ChangeLanguage()
	{
		if ('<%=Session["LangID"]%>' == null || '<%=Session["LangID"]%>'  == "EN")
		{
			document.getElementById("Top1_btnLogout").innerHTML = "Logout(<%=Session["AccountLogin"]%>)"; 
			
		}
		else
		{
			document.getElementById("Top1_btnLogout").innerHTML = "Thoát(<%=Session["AccountLogin"]%>)"; 			
		}
	}
	ChangeLanguage();
</script>
