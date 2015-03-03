<%@ Import Namespace ="System.Data.SqlClient" %>
<%@ Import Namespace ="System.Data" %>
<%@ Import Namespace ="iHRPCore.Com"%>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="DocumentComplex.ascx.cs" Inherits="iHRPCore.MdlHR.DocumentComplex" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc" TagName="EmpHeader" Src="../Include/EmpHeader.ascx" %>
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
		<TD align="center"><asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD style="HEIGHT: 23px" align="center"><uc:empheader id="HR_EmpHeader" runat="server"></uc:empheader></TD>
	</TR>
	<TR>
		<TD align="center">
			<TABLE id="Table2" width="100%">
				<TR id="trComplex1">
					<TD width="15%"><asp:label id="lblDocumentType" CssClass="labelRequire" runat="server" Width="100%">Document type</asp:label></TD>
					<TD width="38%"><asp:dropdownlist id="cboLSDocumentID" CssClass="combo" runat="server" Width="95%"></asp:dropdownlist></TD>
					<TD width="15%"><asp:label id="lblEstablishPlace" CssClass="label" runat="server"> Establish Place</asp:label></TD>
					<TD width="38%"><asp:textbox id="txtEstablishPlace" CssClass="input" runat="server" Width="95%"></asp:textbox></TD>
				</TR>
				<TR id="trComplex2">
					<TD><asp:label id="lblDecisionNumber" CssClass="labelRequire" runat="server" Width="100%">Document No</asp:label></TD>
					<TD><asp:textbox id="txtDocumentNo" CssClass="input" runat="server" Width="95%"></asp:textbox></TD>
					<TD><asp:label id="lblSignDate" CssClass="label" runat="server">Sign date</asp:label></TD>
					<TD><asp:textbox id="txtSignDate" onblur="JavaScript:CheckDate(this)" CssClass="input" runat="server"
							Width="80%" MaxLength="10"></asp:textbox>&nbsp;<INPUT class=btnCal onclick="javascript:popUpCalendar(<%=this.txtSignDate.ClientID%>);" type=button></TD>
				</TR>
				<TR id="trComplex3">
					<TD><asp:label id="lblContent" CssClass="label" runat="server" Width="100%">Content</asp:label></TD>
					<TD vAlign="top" colSpan="3"><asp:textbox id="txtContent" style="POSITION: absolute" CssClass="input" runat="server" Width="98%"></asp:textbox></TD>
				</TR>
				<TR id="trComplex4">
					<TD><asp:label id="lblSubmitDate" CssClass="label" runat="server"> Submit Date</asp:label></TD>
					<TD><asp:textbox id="txtSubmitDate" onblur="JavaScript:CheckDate(this)" CssClass="input" runat="server"
							Width="80%" MaxLength="10"></asp:textbox>&nbsp;<INPUT class=btnCal onclick="javascript:popUpCalendar(<%=this.txtSubmitDate.ClientID%>);" type=button></TD>
					<TD id="tdAdToStamplbl" runat="server"></TD>
					<TD id="tdAdToStamptxt" runat="server"></TD>
				</TR>
				<TR>
					<TD><asp:label id="Label1" CssClass="label" runat="server">Attach file </asp:label><asp:textbox id="txtAttachFile" onblur="JavaScript:CheckDate(this)" CssClass="input" Visible="False"
							Runat="server"></asp:textbox></TD>
					<TD vAlign="top" colSpan="3"><INPUT class="input" id="txtFile" style="WIDTH: 50%" tabIndex="1" type="file" size="13"
							name="File1" runat="server">
						<asp:hyperlink id="hpSelectFile" CssClass="LinkLeft" runat="server"></asp:hyperlink><asp:linkbutton id="btnDeleteFile" CssClass="DeleteFile" runat="server" Visible="False" ToolTip="Delete Image"></asp:linkbutton></TD>
				</TR>
				<TR id="trComplex5">
					<TD><asp:label id="lblStorePlace" CssClass="label" runat="server">Store Place</asp:label></TD>
					<TD vAlign="top" colSpan="3"><asp:textbox id="txtStorePlace" style="POSITION: absolute" CssClass="input" runat="server" Width="98%"></asp:textbox></TD>
				</TR>
				<TR id="trComplex6">
					<TD><asp:label id="lblNote" CssClass="label" runat="server">Note</asp:label></TD>
					<TD vAlign="top" colSpan="3"><asp:textbox id="txtNote" style="POSITION: absolute" CssClass="input" runat="server" Width="98%"></asp:textbox></TD>
				</TR>
				<TR>
					<TD align="center" colSpan="4">
						<HR align="center" width="100%">
						&nbsp;</TD>
				</TR>
				<TR>
					<TD align="center" colSpan="4"><asp:textbox id="txtEmpDocumentID" onblur="JavaScript:CheckDate(this)" CssClass="input" runat="server"
							Width="80px" MaxLength="10" Visible="False"></asp:textbox>&nbsp;
						<asp:linkbutton id="btnAddnew" accessKey="N" CssClass="btnAddnew" runat="server" ToolTip="Alt+N">Add New</asp:linkbutton>&nbsp;&nbsp;
						<asp:linkbutton id="btnSave" accessKey="S" CssClass="btnSave" runat="server" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp;&nbsp;
						<asp:linkbutton id="btnDelete" accessKey="D" CssClass="btnDelete" runat="server" ToolTip="Alt + D">Delete</asp:linkbutton>&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnList" accessKey="L" CssClass="btnList" runat="server" ToolTip="Alt + L">List</asp:linkbutton>&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnExport" accessKey="E" CssClass="btnExport" runat="server" ToolTip="Alt+E, Xu?t dl trên lu?i ra file Excel"> Export</asp:linkbutton></TD>
				</TR>
				<TR>
					<TD align="center" colSpan="4" height="10"></TD>
				</TR>
				<TR id="trList">
					<TD align="center" colSpan="4"><asp:datagrid id="dtgList" CssClass="grid" runat="server" Width="100%" AllowPaging="True" BackColor="White"
							BorderColor="#3366CC" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False" AllowSorting="True">
							<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="EmpDocumentID" HeaderText="ID"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Select">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="&lt;b&gt;#&lt;/b&gt;">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<%# Container.ItemIndex + 1%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:ButtonColumn DataTextField="LSDocumentCode" HeaderText="Document Type" CommandName="EDIT"></asp:ButtonColumn>
								<asp:BoundColumn DataField="EstablishPlace" HeaderText="Establish Place">
									<HeaderStyle Width="20%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="SignDate" HeaderText="Sign date">
									<HeaderStyle Width="10%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="SubmitDate" HeaderText="SubmitDate">
									<HeaderStyle Width="10%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="StorePlace" HeaderText="StorePlace">
									<HeaderStyle Width="20%"></HeaderStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE>
		</TD>
	</TR> <!-- end button for input form --></TABLE>
<script language="javascript">
<!--
function validform(){
	if(checkisnull('cboLSDocumentID')==false)  return false;	
	if(checkisnull('txtDocumentNo')==false)  return false;
	if(FromSmallNow(document.getElementById('_ctl0_txtSignDate')) == false)
	{
		GetAlertError(iTotal,DSAlert,"0005");
		document.getElementById('_ctl0_txtSignDate').focus();			
		return false;
	}
	if (compareDates(document.getElementById('_ctl0_txtSignDate').value,document.getElementById('_ctl0_txtSubmitDate').value )> 0)
	{
		GetAlertError(iTotal,DSAlert,"0019");				
		document.getElementById('_ctl0_txtSubmitDate').focus();	
		return false;
	}
	/*if(FromSmallNow(document.getElementById('_ctl0_txtSignDate')) == false)
		{
			GetAlertError(iTotal,DSAlert,"0005");
			document.getElementById('_ctl0_txtSignDate').focus();
			return false;
		}
	if(FromSmallNow(document.getElementById('_ctl0_txtSubmitDate')) == false)
		{
			GetAlertError(iTotal,DSAlert,"0005");
			document.getElementById('_ctl0_txtSubmitDate').focus();
			return false;
		}*/
	if (!checkempty('txtSignDate') && !checkempty('txtSubmitDate'))
		{
			
			if (FromSmallToDate(document.getElementById('_ctl0_txtSubmitDate'),document.getElementById('_ctl0_txtSignDate') )> 0)
			{
				GetAlertError(iTotal,DSAlert,"0020");				
				document.getElementById('_ctl0_txtSubmitDate').focus();
				return false;
			}
		}
	return true;
}
function checkempty(obj)
	{
		if(document.getElementById('_ctl0_' + obj).value=="")
			return true;			
		else
			return false;
	}
function checkdelete()
{
	if(GridCheck('_ctl0_dtgList',3,1,'chkSelect')==false)
	{			
		GetAlertError(iTotal,DSAlert,"0001");				
		return false;
	}
	if(confirm(GetAlertText(iTotal,DSAlert,"0002"))==false){
		return false;
	}	
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
	function confirmDeleteFile()
	{
		if(confirm(GetAlertText(iTotal,DSAlert,"0006"))==false){
		return false;
		}
	}
//-->
</script>
