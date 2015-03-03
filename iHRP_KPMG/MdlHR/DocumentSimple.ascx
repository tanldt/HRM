<%@ Import Namespace ="iHRPCore.Com"%>
<%@ Import Namespace ="System.Data" %>
<%@ Import Namespace ="System.Data.SqlClient" %>
<%@ Register TagPrefix="uc" TagName="EmpHeader" Src="../Include/EmpHeader.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="DocumentSimple.ascx.cs" Inherits="iHRPCore.MdlHR.DocumentSimple" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%
		string strLangID = Session["LangID"] == null?"EN":Session["LangID"].ToString().Trim();
		int Total=0;
		DataTable rs = new DataTable();	
		rs = clsCommon.GetDataTable("sp_GetDataCombo @TableName = 'LS_tblLanguage'");	
		Total=rs.Rows.Count;
%>
<script language="javascript" src='../Include/common.js"'></script>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="97%" border="0">
	<TR>
		<TD align="center">
			<asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD style="HEIGHT: 23px" align="center">
			<uc:empheader id="HR_EmpHeader" runat="server"></uc:empheader></TD>
	</TR>
	<TR>
		<TD style="HEIGHT: 23px" align="center"></TD>
	</TR>
	<TR>
		<TD align="center">
			<TABLE id="Table2" width="100%">
				<TR id="trSimple1">
					<TD align="left" colSpan="4">
						<asp:checkboxlist id="ChkDocumentType" CssClass="checkbox" runat="server" Width="100%">
							<asp:ListItem Value="1">L&#253; lịch</asp:ListItem>
							<asp:ListItem Value="2">Học bạ</asp:ListItem>
							<asp:ListItem Value="3">Bằng Đại học</asp:ListItem>
							<asp:ListItem Value="4">Giấy kh&#225;m sức khỏe</asp:ListItem>
							<asp:ListItem Value="5">Chức nhận qu&#226;n sự</asp:ListItem>
							<asp:ListItem Value="6">Kh&#225;c</asp:ListItem>
						</asp:checkboxlist></TD>
				</TR>
				<TR id="trSimple2">
					<TD width="10%">
						<asp:label id="lblStorePlace_S" CssClass="label" runat="server" Width="100%">Store Place</asp:label></TD>
					<TD colSpan="3">
						<asp:textbox id="txtStorePlace" CssClass="input" runat="server" Width="98%"></asp:textbox></TD>
				</TR>
				<TR>
					<TD align="center" colSpan="4">
						<HR align="center" width="100%">
						&nbsp;</TD>
				</TR>
				<TR>
					<TD align="center" colSpan="4">&nbsp;&nbsp;
						<asp:linkbutton id="btnSave" accessKey="S" CssClass="btnSave" runat="server" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnList" accessKey="L" CssClass="btnList" runat="server" ToolTip="Alt + L">List</asp:linkbutton>&nbsp;&nbsp;&nbsp;</TD>
				</TR>
				<TR>
					<TD align="center" colSpan="4" height="10"></TD>
				</TR>
			</TABLE>
		</TD>
	</TR> <!-- end button for input form --></TABLE>
