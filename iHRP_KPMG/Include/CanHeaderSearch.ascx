<%@ Control Language="c#" AutoEventWireup="false" Codebehind="CanHeaderSearch.ascx.cs" Inherits="iHRPCore.Include.CanHeaderSearch" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="Table1" cellSpacing="0" cellPadding="1" width="100%" border="0">
	<TR vAlign="top">
		<TD width="13%">
			<asp:label id="lblCandidateID" runat="server" CssClass="label">Candidate ID</asp:label></TD>
		<TD width="20%">
			<asp:textbox onkeypress="checkKey('btnSearchByID')" id="txtCandidateCode" runat="server" CssClass="input"
				Width="100%"></asp:textbox></TD>
		<TD width="2%"></TD>
		<TD width="15%">
			<asp:label id="lblCandidateName" runat="server" CssClass="label">Candidate Name</asp:label></TD>
		<TD width="20%">
			<asp:textbox onkeypress="checkKey('btnSearchByName')" id="txtCandidateName" runat="server" CssClass="input"
				Width="100%"></asp:textbox></TD>
		<TD width="2%"></TD>
		<TD width="10%"></TD>
		<TD width="15%"></TD>
	</TR>
	<TR vAlign="top">
		<TD style="HEIGHT: 26px" noWrap>
			<asp:label id="lblJobTitle" runat="server" CssClass="label">Pos Title</asp:label></TD>
		<TD style="HEIGHT: 26px">
			<asp:dropdownlist id="cboLSJobTitleID" runat="server" CssClass="combo" Width="100%"></asp:dropdownlist></TD>
		<TD style="HEIGHT: 26px"></TD>
		<TD style="WIDTH: 288px; HEIGHT: 26px">
			<asp:label id="lblDocumentStatus" runat="server" CssClass="label">Document Status</asp:label></TD>
		<TD style="HEIGHT: 26px" noWrap>
			<asp:dropdownlist id="cboDocumentStatus" runat="server" CssClass="combo" Width="100%">
				<asp:ListItem></asp:ListItem>
				<asp:ListItem Value="1">Submit</asp:ListItem>
				<asp:ListItem Value="0">Short Of</asp:ListItem>
			</asp:dropdownlist></TD>
		<TD style="HEIGHT: 26px" noWrap></TD>
		<TD></TD>
		<TD style="HEIGHT: 26px"></TD>
	</TR>
	<TR>
		<TD colSpan="8">
			<HR align="center" width="97%">
		</TD>
	</TR>
	<TR>
		<TD colSpan="8">
			<TABLE id="Table3" width="100%">
				<TR>
					<TD style="HEIGHT: 17px" width="13%">
						<asp:label id="Label1" CssClass="label" Width="100%" Runat="server">Criteria</asp:label></TD>
					<TD style="HEIGHT: 17px" width="35%">
						<asp:dropdownlist id="cboCriteria" runat="server" CssClass="combo" Width="100%" AutoPostBack="True"></asp:dropdownlist></TD>
					<TD width="3%" rowSpan="4"></TD>
					<TD vAlign="middle" align="center" width="13%" rowSpan="4">
						<asp:button id="cmdAdd" runat="server" Width="100%" Height="22px" Font-Bold="True" Text=">>"
							CssClass="btnSave"></asp:button>
						<P>
							<asp:button id="btRemove" runat="server" Width="100%" Height="22px" Font-Bold="True" Text="<<"
								CssClass="btnSave"></asp:button></P>
						<P>
							<asp:button id="btnRemoveAll" Width="100%" Runat="server" Height="22px" Text="Remove All" CssClass="btnSave"></asp:button></P>
					</TD>
					<TD width="3%" rowSpan="4"></TD>
					<TD width="35%" rowSpan="4">
						<asp:textbox id="txtFillCondition" runat="server" Width="100%" Height="94px" TextMode="MultiLine"
							ReadOnly="True" CssClass="input"></asp:textbox></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 136px; HEIGHT: 16px" width="136">
						<asp:label id="Label2" CssClass="label" Width="100%" Runat="server">Condition</asp:label></TD>
					<TD style="HEIGHT: 16px">
						<asp:dropdownlist id="cboCondition" runat="server" CssClass="combo" Width="100%"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD>
						<asp:label id="Label3" CssClass="label" Width="100%" Runat="server">Information</asp:label></TD>
					<TD>
						<asp:dropdownlist id="cboInfo" Width="250" Runat="server" Height="22px" Visible="False" Font-Size="8pt"
							Font-Names="Arial" CssClass="combo"></asp:dropdownlist>
						<asp:textbox id="txtDate" onblur="JavaScript:CheckDate(this)" Width="250px" Runat="server" Height="21px"
							Visible="False" MaxLength="10" Font-Size="8pt" Font-Names="Arial" CssClass="input"></asp:textbox>
						<asp:textbox id="txtNumber" onblur="JavaScript:checkNumeric(this)" Width="250px" Runat="server"
							Height="21px" Visible="False" Font-Size="8pt" Font-Names="Arial" CssClass="input"></asp:textbox>
						<asp:textbox id="txtInfo" Width="250px" Runat="server" Height="21px" MaxLength="100" Font-Size="8pt"
							Font-Names="Arial" CssClass="input"></asp:textbox>
						<DIV id="divBool" style="DISPLAY: none" runat="server">
							<asp:radiobutton id="rdTrue" Width="71" Runat="server" Height="20px" Text="True" GroupName="Bool"
								Checked="True" Font-Size="8pt" Font-Names="Arial"></asp:radiobutton>
							<asp:radiobutton id="rdFalse" Width="71" Runat="server" Height="20" Text="False" GroupName="Bool"
								Font-Size="8pt" Font-Names="Arial"></asp:radiobutton></DIV>
					</TD>
				</TR>
				<TR>
					<TD>
						<asp:label id="Label4" CssClass="label" Width="100%" Runat="server">Link</asp:label></TD>
					<TD>
						<asp:radiobutton id="rdAnd" CssClass="option" Width="71px" Runat="server" Height="20px" Text="And"
							GroupName="Link" Checked="True"></asp:radiobutton>
						<asp:radiobutton id="rdOr" CssClass="option" Width="71px" Runat="server" Height="20px" Text="Or"
							GroupName="Link"></asp:radiobutton><INPUT id="txtColType" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" name="txtColType"
							runat="server"></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>
<!--End table-->
<SCRIPT language="javascript">
	function CheckBeforeAddCondition()
	{
		
		if (document.getElementById('_ctl0:RE_CanHeaderSearch:cboCriteria').value == "")
		{
		
			GetAlertError(iTotal,DSAlert,"CL_0001");
			document.getElementById('_ctl0:RE_CanHeaderSearch:cboCriteria').focus();
			return false;
		}
		if (document.getElementById('_ctl0:RE_CanHeaderSearch:cboCondition').value == "")
		{
			
			GetAlertError(iTotal,DSAlert,"CL_0002");
			document.getElementById('_ctl0:RE_CanHeaderSearch:cboCondition').focus();
			return false;
		}
		return true;
	}
	
</SCRIPT>
<SCRIPT language="javascript">	
	document.getElementById("_ctl0:RE_CanHeaderSearch:txtCandidateCode").focus();
</SCRIPT>
