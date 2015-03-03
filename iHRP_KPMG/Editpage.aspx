<%@ Import Namespace ="iHRPCore.Com"%>
<%@ Import Namespace ="System.Data" %>
<%@ Import Namespace ="System.Data.SqlClient" %>
<%@ Register TagPrefix="uc1" TagName="Table1_bottom" Src="Include/Table1_bottom.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Table1_top" Src="Include/Table1_top.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderText_bottom" Src="Include/HeaderText_bottom.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderText_top" Src="Include/HeaderText_top.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CreaterFooter" Src="Include/CreaterFooter.ascx" %>
<%@ Page language="c#" Codebehind="Editpage.aspx.cs" AutoEventWireup="false" Inherits="iHRPCore.Editpage" validateRequest="false" %>
<%@ Register TagPrefix="uc1" TagName="Top" Src="Include/Top1.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftMenu" Src="Include/LeftMenu.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Bottom" Src="Include/Bottom.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Bottom_LS" Src="Include/Bottom_LS.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="EeekSoft.Web" Assembly="EeekSoft.Web.PopupWin" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Human Resource Management Systems</title>
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
		<%if(strLanguageID=="EN"){%>
		<script language="javascript" src="Component/DateCOMM/popcalendarEN.js"></script>
		<script language="javascript" src="Component/DateCOMM/popcalendar.js"></script>
		<%} else {%>
		<script language="javascript" src="Component/DateCOMM/popcalendar.js"></script>
		<%}%>
		<script language="javascript" src="Include/CheckDate.js"></script>
		<script language="javascript" src="Include/MyLibrary.js"></script>
		<script language="javascript" src="./Include/common.js"></script>
		<script language="javascript">
		function GetStatusOfControl(status)
		{		
			if (status!='True')
			{
				alert(GetAlertText(iTotal,DSAlert,"0107"))
				return false;
			}
			return true;
		}
  function sstchur_SmartScroller_GetCoords()
  {
    var scrollX, scrollY;
    if (document.all)
    {
      if (!document.documentElement.scrollLeft)
        scrollX = document.body.scrollLeft;
      else
        scrollX = document.documentElement.scrollLeft;

      if (!document.documentElement.scrollTop)
        scrollY = document.body.scrollTop;
      else
        scrollY = document.documentElement.scrollTop;
    }
    else
    {
      scrollX = window.pageXOffset;
      scrollY = window.pageYOffset;
    }
    document.forms["Form1"].scrollLeft.value = scrollX;
    document.forms["Form1"].scrollTop.value = scrollY;
  }

  function sstchur_SmartScroller_Scroll()
  {
    var x = document.forms["Form1"].scrollLeft.value;
    var y = document.forms["Form1"].scrollTop.value;
    window.scrollTo(x, y);
  }  
  window.onload = sstchur_SmartScroller_Scroll;
  window.onscroll = sstchur_SmartScroller_GetCoords;
  window.onclick = sstchur_SmartScroller_GetCoords;
  window.onkeypress = sstchur_SmartScroller_GetCoords;
		</script>
		<script language="javascript">
	function ShowForm(blnShowForm) 
	{
		if(blnShowForm != "1")
			return;
		//alert('FormPage.aspx' + location.search);
		//ShowDialog('FormPage.aspx' + location.search);
		if (document.getElementById("btnShowDialog").title !="Close")
		{
			//document.getElementById("btnShowDialog").style.display = "none";
			var ss = document.getElementById("btnShowDialog").title;
			document.getElementById("btnShowDialog").title=document.getElementById("btnShowDialog").value;
			document.getElementById("btnShowDialog").value=ss
			document.getElementById("tdLeft1").style.display = "";
			document.getElementById("trTop").style.display = "";
			document.getElementById("trBottom").style.display = "";
			document.getElementById("tdSetup").style.display = "";
			document.getElementById("tdSetup").accessKey="F";
		}
		else
		{
			var ss = document.getElementById("btnShowDialog").title;
			document.getElementById("btnShowDialog").title=document.getElementById("btnShowDialog").value;
			document.getElementById("btnShowDialog").value=ss
			document.getElementById("tdLeft1").style.display = "none";
			document.getElementById("trTop").style.display = "none";
			document.getElementById("trBottom").style.display = "none";
			document.getElementById("tdSetup").style.display = "none";
			document.getElementById("tdSetup").accessKey="Z";
			
		}
		
	}
		</script>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Include/myStyles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin=0 leftMargin=0 topMargin=0 
onload="ShowForm(<%=blnShowForm%>)" rightMargin=0>
		<form id="Form1" method="post" runat="server">
			<cc1:popupwinanchor id="showTest" style="DISPLAY: none; Z-INDEX: 118; LEFT: 576px; POSITION: absolute; TOP: 80px"
				runat="server" LinkedControl="btnShow" PopupToShow="popupTest" newmessage="News message text" newtext=""
				newtitle="Message" changetexts="True"></cc1:popupwinanchor><cc1:popupwin id="popupTest" title="First title" style="DISPLAY: none; Z-INDEX: 120; LEFT: 616px; POSITION: absolute; TOP: 528px"
				runat="server" AutoShow="False" ActionType="RaiseEvents" Text="First text in new window" Message="First message" DockMode="BottomLeft" ColorStyle="Custom"
				DarkShadow="255, 0, 0" GradientDark="255, 228, 225" LightShadow="255, 0, 0" Shadow="255, 0, 0" TextColor="0, 0, 255" HideAfter="1500" GradientLight="255, 228, 225"
				ShowLink="False"></cc1:popupwin><cc1:popupwin id="popupWin" style="Z-INDEX: 105; LEFT: 296px; POSITION: absolute; TOP: 528px"
				runat="server" offsetx="150" showlink="True" visible="False" darkshadow="128, 0, 102" lightshadow="185, 170, 200" shadow="125, 90, 160"
				textcolor="0, 0, 3" gradientdark="210, 200, 220" colorstyle="Blue" dockmode="BottomLeft" windowscroll="False" windowsize="300, 200"
				height="100px" width="230px"></cc1:popupwin><input id="btnShow" style="DISPLAY: none" type="button" value="ReShow Popup">
			<TABLE class="Backgroud1" id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="778"
				align="center" border="0">
				<tr>
					<td style="DISPLAY: none" colSpan="2"><INPUT id="txtCurMenu" style="WIDTH: 16px; HEIGHT: 22px" type="hidden" size="1" name="txtCurMenu"
							runat="server"> <INPUT id="txtCurParent" style="WIDTH: 16px; HEIGHT: 22px" type="hidden" size="1" name="txtCurParent"
							runat="server">
					</td>
				</tr>
				<%if (Request.Params["NotTop"] == null){%>
				<TR id="trTop" vAlign="top" height="65"> <!-----header row 1------->
					<TD align="center" width="100%" colSpan="2"><uc1:top id="Top1" runat="server"></uc1:top></TD>
				</TR> <!-----end header------->
				<%}%>
				<TR vAlign="top"> <!-----left menu row 2----15%--->
					<%if (Request.Params["NotLeft"] == null){%>
					<TD id="tdLeft1" align="left" width="130" bgColor="#eaeeee" rowSpan="2"><uc1:leftmenu id="LeftMenu1" runat="server"></uc1:leftmenu></TD>
					<TD align="center" width="84%"><%} else{%>
					<TD align="center" width="100%" colSpan="2"><%}%>
						<!-----detail info of page------->
						<TABLE class="Backgroud1" id="Table2" height="100%" cellSpacing="0" cellPadding="0" width="100%"
							border="0">
							<TR vAlign="middle" align="center" height="30">
								<TD align="center" width="100%"><uc1:headertext_top id="HeaderText_top1" runat="server"></uc1:headertext_top><asp:label id="lblTitle" runat="server" CssClass="labelTitle">FORM TITLE</asp:label><uc1:headertext_bottom id="HeaderText_bottom1" runat="server"></uc1:headertext_bottom></TD>
							</TR>
							<TR vAlign="top">
								<TD align="center"><uc1:table1_top id="Table1_top1" runat="server"></uc1:table1_top><asp:placeholder id="acxHolder" runat="server"></asp:placeholder><uc1:table1_bottom id="Table1_bottom1" runat="server"></uc1:table1_bottom></TD>
							</TR>
						</TABLE>
						<!-----end detail info of page-------></TD>
				</TR>
				<%if (Request.Params["NotBottom"] == null){%>
				<%if (Request.Params["Popup"] == null){%>
				<TR vAlign="bottom">
					<%if (Request.Params["NotLeft"] == null){%>
					<TD noWrap height="20"><%} else{%>
					<TD noWrap colSpan="2" height="20"><%}%>
						<TABLE style='DISPLAY:none' cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD id="tdSetup"><uc1:bottom_ls id="Bottom_LS1" runat="server"></uc1:bottom_ls></TD>
								<TD align="right" width="10%"><input id="txtTemp" style="WIDTH: 0px; HEIGHT: 0px" disabled type="text">
									<INPUT id="btnShowDialog" title="Close" style="VISIBILITY: hidden; WIDTH: 53px; HEIGHT: 25px"
										accessKey="F" onclick="ShowForm(1);" type="button" value="Form">
								</TD>
							</TR>
						</TABLE>
						<uc1:CreaterFooter id="CreaterFooter1" runat="server"></uc1:CreaterFooter>
					</TD>
				</TR>
				<%}%>
				<TR id="trBottom" vAlign="top"> <!-----footer row 3------->
					<TD align="center" width="100%" bgColor="#628ebc" colSpan="2" height="30">
						<uc1:Bottom id="Bottom1" runat="server"></uc1:Bottom></TD>
				</TR> <!-----end footer------->
				<%}%>
			</TABLE>
			<input name="scrollLeft" runat="server" id="scrollLeft" type="hidden" value="0">
			<input name="scrollTop" runat="server" id="scrollTop" type="hidden" value="0"> <INPUT id="txtAcountLogin" style="WIDTH: 16px; HEIGHT: 22px" type="hidden" size="1" name="txtAcountLogin"
				runat="server">
		</form>
	</body>
</HTML>
