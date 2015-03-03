<%@ Import Namespace ="iHRPCore.Com"%>
<%@ Import Namespace ="System.Data" %>
<%@ Import Namespace ="System.Data.SqlClient" %>
<%@ Register TagPrefix="uc1" TagName="HeaderText_top" Src="Include/HeaderText_top.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderText_bottom" Src="Include/HeaderText_bottom.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Table1_top" Src="Include/Table1_top.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Table1_bottom" Src="Include/Table1_bottom.ascx" %>
<%@ Page language="c#" Codebehind="FormPage.aspx.cs" AutoEventWireup="false" Inherits="iHRPCore.FormPage1" %>
<%@ Register TagPrefix="uc1" TagName="Top1" Src="Include/Top1.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Top" Src="Include/Top.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Bottom_LS" Src="Include/Bottom_LS.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="EeekSoft.Web" Assembly="EeekSoft.Web.PopupWin" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Human resource and Payroll</title>
		<script language="javascript">
			
			var DSAlert = new Array(2);
			var iTotal=<%=iTotal%>;
			DSAlert[0] = new Array(<%=iTotal%>);
			DSAlert[1] = new Array(<%=iTotal%>);
			<% for(int i=0; i<iTotal; i++) {
			if ((dtData.Rows[i]["MessCode"].ToString()!= "CA_SM001") &&
				(dtData.Rows[i]["MessCode"].ToString()!= "SYS_SM002")&&
				(dtData.Rows[i]["MessCode"].ToString()!= "LA_SM002") &&
				(dtData.Rows[i]["MessCode"].ToString()!= "LA_SM004") &&
				(dtData.Rows[i]["MessCode"].ToString()!= "LR_SM002") &&
				(dtData.Rows[i]["MessCode"].ToString()!= "OR_SM002") &&
				(dtData.Rows[i]["MessCode"].ToString()!= "OR_SM004") &&
				(dtData.Rows[i]["MessCode"].ToString()!= "OR_SM006") &&
				(dtData.Rows[i]["MessCode"].ToString()!= "OA_SM002") &&
				(dtData.Rows[i]["MessCode"].ToString()!= "LR_SM002") &&
				(dtData.Rows[i]["MessCode"].ToString()!= "CA_SM002") &&
				(dtData.Rows[i]["MessCode"].ToString()!= "CA_SM003") &&
				(dtData.Rows[i]["MessCode"].ToString()!= "CA_SM004") &&
				(dtData.Rows[i]["MessCode"].ToString()!= "LA_SM002") &&
				(dtData.Rows[i]["MessCode"].ToString()!= "LA_SM004") &&
				(dtData.Rows[i]["MessCode"].ToString()!= "LR_SM002") &&
				(dtData.Rows[i]["MessCode"].ToString()!= "AA_SM003") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "AA_SM004") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "AA_SM005") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "AA_SM006") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "CA_SM001") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "CA_SM002") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "CA_SM003") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "CA_SM004") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "LA_SM001") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "LA_SM002") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "LA_SM003") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "LA_SM004") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "LC_SM001") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "LC_SM002") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "LR_SM001") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "LR_SM002") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "MT_SM001") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "MT_SM002") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "MT_SM003") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "MT_SM004") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "MT_SM005") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "MT_SM006") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "MT_SM007") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "MT_SM008") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "MT_SM009") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "MT_SM010") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "AA_SM002") &&
				(dtData.Rows[i]["MessCode"].ToString()!= "AA_SM008") && 							
				(dtData.Rows[i]["MessCode"].ToString()!= "AA_SM007") && 							
				(dtData.Rows[i]["MessCode"].ToString()!= "TR_SM001") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "TR_SM002") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "TR_SM003") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "TR_SM004") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "TR_SM005") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "TR_SM006") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "TR_SM007") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "TR_SM008") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "TR_SM009") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "TR_SM010") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "TR_SM011") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "TR_SM012") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "TR_SM013") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "TR_SM014") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "TR_SM015") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "TR_SM016") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "TR_SM017") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "TR_SM018") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "TR_SM019") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "TR_SM020") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "TR_SM202") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "TR_SM201") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "JB_SM01") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "JB_SM02") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "MT_SM070") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "MT_SM080") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "TR_SM020") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "TR_SM033") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "TR_SM035") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "TR_SM037") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "TR_SM039") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "MT_SM080") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "MT_SM080") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "MT_SM080") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "MT_SM080") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "MT_SM080") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "TR_SM0022") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "OR_SM002") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "OR_SM004") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "OR_SM006") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "RL_SM002") && 
				(dtData.Rows[i]["MessCode"].ToString()!= "LC_SM002")) {%>					
			DSAlert[0][<%=i%>]="<%=dtData.Rows[i]["MessCode"]%>";
			DSAlert[1][<%=i%>]="<%=dtData.Rows[i][strLanguageID]%>";	
		<%}}%>				
		</script>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Include/myStyles.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="Include/CheckDate.js"></script>
		<script language="javascript" src="Include/MyLibrary.js"></script>
		<script language="javascript" src="Include/common.js"></script>
		<%if(strLanguageID=="EN"){%>
		<script language="javascript" src="Component/DateCOMM/popcalendarEN.js"></script>
		<%} else {%>
		<script language="javascript" src="Component/DateCOMM/popcalendar.js"></script>
		<%}%>
		<script>
			function closeWindow()
			{				
				if (<%=Request.Params["FunctionID"]==null?"0":Request.Params["FunctionID"]%>=='9999')					
				{
						
					if (document.getElementById('_ctl0_txtflag').value=='1')
						{										
							opener.document.getElementById('_ctl0_btnTemp').click();
						}					
				}						
			}
		function GetStatusOfControl(status)
		{		
			if (status!='True')
			{
				alert(GetAlertText(iTotal,DSAlert,"0107"))
				return false;
			}
			return true;
		}
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" onunload="javascript:closeWindow()">
		<form id="Form1" method="post" runat="server">
			<TABLE class="Backgroud1" id="Table1" cellSpacing="0" cellPadding="0" width="100%" align="center"
				border="0">
				<TR vAlign="top"> <!-----left menu row 2------->
					<TD align="center" colSpan="2">
						<!-----detail info of page------->
						<TABLE class="Backgroud1" id="Table2" cellSpacing="2" cellPadding="0" width="100%" border="0">
							<tr style="DISPLAY: none">
								<td align="center"></td>
							</tr>
							<TR vAlign="middle" align="center">
								<TD style="DISPLAY: none" align="center" width="100%"><uc1:headertext_top id="HeaderText_top1" runat="server"></uc1:headertext_top><asp:label id="lblTitle" runat="server" CssClass="labelTitle">FORM TITLE</asp:label><uc1:headertext_bottom id="HeaderText_bottom1" runat="server"></uc1:headertext_bottom></TD>
							</TR>
							<TR vAlign="top">
								<TD align="center"><uc1:table1_top id="Table1_top1" runat="server"></uc1:table1_top><asp:placeholder id="acxHolder" runat="server"></asp:placeholder><uc1:table1_bottom id="Table1_bottom1" runat="server"></uc1:table1_bottom></TD>
							</TR>
						</TABLE>
						<!-----end detail info of page------->
						<cc1:popupwinanchor id="showTest" style="DISPLAY: none" runat="server" LinkedControl="btnShow" PopupToShow="popupTest"
							newmessage="News message text" newtext="" newtitle="Thông báo" changetexts="True"></cc1:popupwinanchor>
						<cc1:popupwin id="popupWin" runat="server" offsetx="150" showlink="True" visible="False" darkshadow="128, 0, 102"
							lightshadow="185, 170, 200" shadow="125, 90, 160" textcolor="0, 0, 3" gradientdark="210, 200, 220"
							colorstyle="Blue" dockmode="BottomLeft" windowscroll="False" windowsize="300, 200" height="100px"
							width="230px"></cc1:popupwin>
						<cc1:popupwin id="popupTest" title="First title" style="DISPLAY: none" runat="server" AutoShow="False"
							ActionType="RaiseEvents" Text="First text in new window" Message="First message" DockMode="BottomLeft"
							ColorStyle="Custom" DarkShadow="255, 0, 0" GradientDark="255, 228, 225" LightShadow="255, 0, 0"
							Shadow="255, 0, 0" TextColor="0, 0, 255" HideAfter="1500" GradientLight="255, 228, 225" ShowLink="False"></cc1:popupwin>
						<input id="btnShow" style="DISPLAY: none" type="button" value="ReShow Popup">
					</TD>
				</TR>
				<!--<TR vAlign="bottom">
					<TD align="center" height="20"><hr width="95%"> <INPUT class="btnClose" id="btnClose" accessKey="C" onclick="javascript:window.close();"
							type="button" value="Close" name="btnShowDialog"></TD>
				</TR>--></TABLE>
		</form>
	</body>
</HTML>
