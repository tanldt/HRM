<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ForeignerInfo.ascx.cs" Inherits="iHRPCore.MdlHR.ForeignerInfo" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc" TagName="EmpHeader" Src="../Include/EmpHeader.ascx" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="97%" border="0">
	<TR>
		<TD colSpan="8"><asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center" colSpan="8"><uc:empheader id="HR_EmpHeader" runat="server"></uc:empheader></TD>
	</TR>
	<TR>
		<TD noWrap align="left" colSpan="8"></TD>
	</TR>
	<TR>
		<TD colSpan="2"><asp:label id="lblWP_Title" CssClass="labelSubTitle" runat="server" Width="100%">Work Permit</asp:label></TD>
		<TD></TD>
		<TD colSpan="2"></TD>
		<TD></TD>
		<TD colSpan="2"></TD>
	</TR>
	<TR>
		<TD width="12%"><asp:label id="lblWP_Number" CssClass="labelRequire" runat="server" Width="100%">Number</asp:label></TD>
		<TD width="20%"><asp:textbox id="txtWP_Number" CssClass="input" runat="server" Width="101" MaxLength="30"></asp:textbox></TD>
		<TD width="2%"></TD>
		<TD width="12%"><asp:label id="lblWP_IssusedDate" CssClass="label" runat="server" Width="100%">Issued date</asp:label></TD>
		<TD width="20%"><asp:textbox id="txtWP_IssuedDate" onblur="JavaScript:CheckDate(this)" CssClass="input" runat="server"
				Width="80px" MaxLength="10"></asp:textbox>&nbsp;<INPUT class="btnCal" onclick="javascript:popUpCalendar(<%=this.txtWP_IssuedDate.ClientID%>);" type="button"></TD>
		<TD width="2%"></TD>
		<TD width="12%"><asp:label id="lblWP_EffectDate" CssClass="label" runat="server" Width="100%">Effect date</asp:label></TD>
		<TD width="20%"><asp:textbox id="txtWP_EffDate" onblur="JavaScript:CheckDate(this)" CssClass="input" runat="server"
				Width="80px" MaxLength="10"></asp:textbox>&nbsp;<INPUT class="btnCal" onclick="javascript:popUpCalendar(<%=this.txtWP_EffDate.ClientID%>);" type="button"></TD>
	</TR>
	<TR>
		<TD><asp:label id="lblWP_ExpiredDate" CssClass="label" runat="server" Width="100%">Expired date</asp:label></TD>
		<TD noWrap><asp:textbox id="txtWP_ExpiredDate" onblur="JavaScript:CheckDate(this)" CssClass="input" runat="server"
				Width="101px" MaxLength="10"></asp:textbox>&nbsp;<INPUT class="btnCal" onclick="javascript:popUpCalendar(<%=this.txtWP_ExpiredDate.ClientID%>);" type="button"></TD>
		<TD></TD>
		<TD><asp:label id="lblWP_IssuedPlace" CssClass="label" runat="server" Width="100%">Issued place</asp:label></TD>
		<TD noWrap colSpan="4" valign="top"><asp:textbox id="txtWP_IssuedPlace" style="POSITION: absolute" CssClass="input" runat="server"
				Width="100%" MaxLength="150"></asp:textbox></TD>
	</TR>
	<TR>
		<TD colSpan="8">
			<HR class="lineSeperate" width="100%">
		</TD>
	</TR>
	<TR>
		<TD><asp:label id="lblPass_Title" CssClass="labelSubTitle" runat="server" Width="100%">Passport</asp:label></TD>
		<TD></TD>
		<TD></TD>
		<TD></TD>
		<TD></TD>
		<TD></TD>
		<TD></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD><asp:label id="lblPass_Number" CssClass="labelRequire" runat="server" Width="100%">Number</asp:label></TD>
		<TD><asp:textbox id="txtPass_Number" CssClass="input" runat="server" Width="100px" MaxLength="30"></asp:textbox></TD>
		<TD></TD>
		<TD><asp:label id="lblPass_IssusedDate" CssClass="label" runat="server" Width="100%">Issued date</asp:label></TD>
		<TD><asp:textbox id="txtPass_IssuedDate" onblur="JavaScript:CheckDate(this)" CssClass="input" runat="server"
				Width="80px" MaxLength="10"></asp:textbox>&nbsp;<INPUT class="btnCal" onclick="javascript:popUpCalendar(<%=this.txtPass_IssuedDate.ClientID%>);" type="button"></TD>
		<TD></TD>
		<TD><asp:label id="lblPass_EffectDate" CssClass="label" runat="server" Width="100%">Effect date</asp:label></TD>
		<TD><asp:textbox id="txtPass_EffDate" onblur="JavaScript:CheckDate(this)" CssClass="input" runat="server"
				Width="80px" MaxLength="10"></asp:textbox>&nbsp;<INPUT class="btnCal" onclick="javascript:popUpCalendar(<%=this.txtPass_EffDate.ClientID%>);" type="button"></TD>
	</TR>
	<TR>
		<TD><asp:label id="lblPass_ExpiredDate" CssClass="label" runat="server" Width="100%">Expired date</asp:label></TD>
		<TD><asp:textbox id="txtPass_ExpiredDate" onblur="JavaScript:CheckDate(this)" CssClass="input" runat="server"
				Width="101px" MaxLength="10"></asp:textbox>&nbsp;<INPUT class="btnCal" onclick="javascript:popUpCalendar(<%=this.txtPass_ExpiredDate.ClientID%>);" type="button"></TD>
		<TD></TD>
		<TD><asp:label id="lblPass_IssuedPlace" CssClass="label" runat="server" Width="100%">Issued place</asp:label></TD>
		<TD colSpan="4" valign="top"><asp:textbox id="txtPass_IssuedPlace" CssClass="input" style="POSITION: absolute" runat="server"
				Width="100%" MaxLength="150" TextMode="SingleLine"></asp:textbox></TD>
	</TR>
	<TR>
		<TD colSpan="8">
			<HR class="lineSeperate" width="100%">
		</TD>
	</TR>
	<TR>
		<TD><asp:label id="lblVisa_Title" CssClass="labelSubTitle" runat="server" Width="100%">Visa</asp:label></TD>
		<TD></TD>
		<TD></TD>
		<TD></TD>
		<TD></TD>
		<TD></TD>
		<TD></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD><asp:label id="lblVisa_Number" CssClass="labelRequire" runat="server" Width="100%">Number</asp:label></TD>
		<TD><asp:textbox id="txtVisa_Number" CssClass="input" runat="server" Width="100px" MaxLength="30"></asp:textbox></TD>
		<TD></TD>
		<TD><asp:label id="lblVisa_IssusedDate" CssClass="label" runat="server" Width="100%">Issued date</asp:label></TD>
		<TD><asp:textbox id="txtVisa_IssuedDate" onblur="JavaScript:CheckDate(this)" CssClass="input" runat="server"
				Width="80px" MaxLength="10"></asp:textbox>&nbsp;<INPUT class="btnCal" onclick="javascript:popUpCalendar(<%=this.txtVisa_IssuedDate.ClientID%>);" type="button"></TD>
		<TD></TD>
		<TD><asp:label id="lblVisa_EffectDate" CssClass="label" runat="server" Width="100%">Effect date</asp:label></TD>
		<TD><asp:textbox id="txtVisa_EffDate" onblur="JavaScript:CheckDate(this)" CssClass="input" runat="server"
				Width="80px" MaxLength="10"></asp:textbox>&nbsp;<INPUT class="btnCal" onclick="javascript:popUpCalendar(<%=this.txtVisa_EffDate.ClientID%>);" type="button"></TD>
	</TR>
	<TR>
		<TD><asp:label id="lblVisa_ExpiredDate" CssClass="label" runat="server" Width="100%">Expired date</asp:label></TD>
		<TD><asp:textbox id="txtVisa_ExpiredDate" onblur="JavaScript:CheckDate(this)" CssClass="input" runat="server"
				Width="101px" MaxLength="10"></asp:textbox>&nbsp;<INPUT class="btnCal" onclick="javascript:popUpCalendar(<%=this.txtVisa_ExpiredDate.ClientID%>);" type="button"></TD>
		<TD></TD>
		<TD><asp:label id="lblVisa_IssuedPlace" CssClass="label" runat="server" Width="100%">Issued place</asp:label></TD>
		<TD colSpan="4" valign="top"><asp:textbox id="txtVisa_IssuedPlace" CssClass="input" style="POSITION: absolute" runat="server"
				Width="100%" MaxLength="150"></asp:textbox></TD>
	</TR>
	<TR>
		<TD align="center" colSpan="8">
			<HR class="lineSeperate" width="100%">
		</TD>
	</TR>
	<TR>
		<TD align="center" colSpan="8">&nbsp;&nbsp;
			<asp:linkbutton id="btnSave" accessKey="S" CssClass="btnSave" runat="server" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp;&nbsp;
			<asp:linkbutton id="btnList" accessKey="L" CssClass="btnList" runat="server" ToolTip="ALT+L"> List</asp:linkbutton></TD>
	</TR>
</TABLE>
<script language="javascript">
<!--

	function validform()
	{
		// Check Working paper
		
		if ((!checkempty('txtWP_IssuedDate')||!checkempty('txtWP_EffDate')||!checkempty('txtWP_ExpiredDate')||!checkempty('txtWP_IssuedPlace')) && checkempty('txtWP_Number'))
		{
				GetAlertError(iTotal,DSAlert,"HR_0001");
				document.getElementById('_ctl0_txtWP_Number').focus();
				return false;
		}
		
		if(FromSmallNow(document.getElementById('_ctl0_txtWP_IssuedDate')) == false)
		{
			GetAlertError(iTotal,DSAlert,"0005");
			document.getElementById('_ctl0_txtWP_IssuedDate').focus();
			return false;
		}		
		if (!checkempty('txtWP_IssuedDate')&& !checkempty('txtWP_EffDate'))
		{
			if (FromSmallToDate(document.getElementById('_ctl0_txtWP_EffDate'),document.getElementById('_ctl0_txtWP_IssuedDate') )> 0)
			{
				GetAlertError(iTotal,DSAlert,"0020");				
				document.getElementById('_ctl0_txtWP_EffDate').focus();
				return false;
			}
		}
		
		if (!checkempty('txtWP_EffDate')&& !checkempty('txtWP_ExpiredDate'))
		{
			if (FromSmallToDate(document.getElementById('_ctl0_txtWP_ExpiredDate'),document.getElementById('_ctl0_txtWP_EffDate') )> 0)
			{
				GetAlertError(iTotal,DSAlert,"0021");				
				document.getElementById('_ctl0_txtWP_ExpiredDate').focus();
				return false;
			}
		}
		
		// Check passport
		
		if ((!checkempty('txtPass_IssuedDate')||!checkempty('txtPass_EffDate')||!checkempty('txtPass_ExpiredDate')||!checkempty('txtPass_IssuedPlace')) && checkempty('txtPass_Number'))
		{
				GetAlertError(iTotal,DSAlert,"HR_0002");
				document.getElementById('_ctl0_txtPass_Number').focus();
				return false;
		}
		if(FromSmallNow(document.getElementById('_ctl0_txtPass_IssuedDate')) == false)
		{
			GetAlertError(iTotal,DSAlert,"0005");
			document.getElementById('_ctl0_txtPass_IssuedDate').focus();
			return false;
		}
		if (!checkempty('txtPass_IssuedDate')&& !checkempty('txtPass_EffDate'))
		{
			
			if (FromSmallToDate(document.getElementById('_ctl0_txtPass_EffDate'),document.getElementById('_ctl0_txtPass_IssuedDate') )> 0)
			{
				GetAlertError(iTotal,DSAlert,"0020");				
				document.getElementById('_ctl0_txtPass_EffDate').focus();
				return false;
			}
		}
		
		if (!checkempty('txtPass_EffDate')&& !checkempty('txtPass_ExpiredDate'))
		{		
			if (FromSmallToDate(document.getElementById('_ctl0_txtPass_ExpiredDate'),document.getElementById('_ctl0_txtPass_EffDate') )> 0)
			{
				GetAlertError(iTotal,DSAlert,"0021");				
				document.getElementById('_ctl0_txtPass_ExpiredDate').focus();
				return false;
			}
		}
		
		// Check visa
		
		if ((!checkempty('txtVisa_IssuedDate')||!checkempty('txtVisa_EffDate')||!checkempty('txtVisa_ExpiredDate')||!checkempty('txtVisa_IssuedPlace')) && checkempty('txtVisa_Number'))
		{
				GetAlertError(iTotal,DSAlert,"HR_0003");
				document.getElementById('_ctl0_txtVisa_Number').focus();
				return false;
		}
		if(FromSmallNow(document.getElementById('_ctl0_txtVisa_IssuedDate')) == false)
		{
			GetAlertError(iTotal,DSAlert,"0005");
			document.getElementById('_ctl0_txtVisa_IssuedDate').focus();
			return false;
		}
		if (!checkempty('txtVisa_IssuedDate')&& !checkempty('txtVisa_EffDate'))
		{
			if (FromSmallToDate(document.getElementById('_ctl0_txtVisa_EffDate'),document.getElementById('_ctl0_txtVisa_IssuedDate') )> 0)
			{
				GetAlertError(iTotal,DSAlert,"0020");				
				document.getElementById('_ctl0_txtVisa_EffDate').focus();
				return false;
			}
		}
		
		if (!checkempty('txtVisa_EffDate')&& !checkempty('txtVisa_ExpiredDate'))
		{
			
			if (FromSmallToDate(document.getElementById('_ctl0_txtVisa_ExpiredDate'),document.getElementById('_ctl0_txtVisa_EffDate') )> 0)
			{
				GetAlertError(iTotal,DSAlert,"0021");				
				document.getElementById('_ctl0_txtVisa_ExpiredDate').focus();
				return false;
			}
		}
		return true;		
	}
	function checkisnull(obj)
	{
		if(document.getElementById('_ctl0_' + obj).value=="")
			{
				GetAlertError(iTotal,DSAlert,"0003");
				document.getElementById('_ctl0_' + obj).focus();
				return false;
			}
		else
			return true;
	}
	
	function checkempty(obj)
	{
		if(document.getElementById('_ctl0_' + obj).value=="")
			return true;			
		else
			return false;
	}
//-->
</script>
