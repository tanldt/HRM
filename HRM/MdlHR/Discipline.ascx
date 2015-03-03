<%@ Register TagPrefix="uc1" TagName="EmpHeader" Src="../Include/EmpHeader.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Discipline.ascx.cs" Inherits="iHRPCore.MdlHR.Discipline" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="FPT" Namespace="FPTToolWeb.Control.DataGrids" Assembly="FPTToolWeb" %>
<%@ Import Namespace ="System.Data.SqlClient" %>
<%@ Import Namespace ="System.Data" %>
<%@ Import Namespace ="iHRPCore.Com"%>
<script language="javascript">
function checkOpt()
{
	if(document.getElementById("_ctl0_optReward").checked==true )
	{
		document.getElementById("_ctl0_trDiscipline").style.display="none";
		document.getElementById("_ctl0_trAward").style.display="block";
	}else
	{
		document.getElementById("_ctl0_trDiscipline").style.display="block";
		document.getElementById("_ctl0_trAward").style.display="none";
	}
}
function collapseDecision()
{
	if (document.getElementById('_ctl0_chkDecision').checked==false)
	{		
		document.getElementById('_ctl0_trDecision1').style.display="none";
		document.getElementById('_ctl0_trDecision2').style.display="none";
		document.getElementById('_ctl0_trDecision3').style.display="none";
		document.getElementById('_ctl0_trDecision4').style.display="none";
	}
	else
	{
		document.getElementById('_ctl0_trDecision1').style.display="block";
		document.getElementById('_ctl0_trDecision2').style.display="block";
		document.getElementById('_ctl0_trDecision3').style.display="block";
		document.getElementById('_ctl0_trDecision4').style.display="block";
	}
}
</script>
<TABLE id="MainTable" cellSpacing="0" cellPadding="0" width="99%" border="0">
	<TR>
		<TD align="center" width="100%" colSpan="3"><asp:label id="lblErr" ForeColor="Red" runat="server" CssClass="lblErr"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center" width="100%" colSpan="3"><uc1:empheader id="HR_EmpHeader" runat="server"></uc1:empheader></TD>
	</TR>
	<TR>
		<TD align="right" width="45%"><asp:radiobutton id="optReward" CssClass="option" Checked="True" GroupName="option" Runat="server"
				Text="Reward"></asp:radiobutton></TD>
		<TD width="10%"></TD>
		<TD align="left" width="45%"><asp:radiobutton id="optDiscipline" CssClass="option" GroupName="option" Runat="server" Text="Discipline"></asp:radiobutton></TD>
	</TR>
	<TR>
		<TD align="right" width="45%" height="10"></TD>
		<TD width="10%" height="10"></TD>
		<TD align="left" width="45%" height="10"></TD>
	</TR>
	<TR>
		<TD colSpan="3">
			<TABLE id="tbl1" cellSpacing="0" cellPadding="1" width="100%" border="0">
				<TR id="trDiscipline" runat="server">
					<TD width="15%"><asp:label id="Label1" runat="server" CssClass="labelRequire"> Form</asp:label></TD>
					<TD width="30%"><asp:dropdownlist id="cboLSDisciplineMethodID" runat="server" CssClass="select" Width="100%"></asp:dropdownlist></TD>
					<TD width="10%"></TD>
					<TD width="15%"><asp:label id="Label9" runat="server" CssClass="labelRequire">Level</asp:label></TD>
					<TD width="30%"><asp:dropdownlist id="cboLSDisciplineLevelID" runat="server" CssClass="select" Width="100%"></asp:dropdownlist></TD>
				</TR>
				<TR id="trAward" runat="server">
					<TD width="15%"><asp:label id="Label7" runat="server" CssClass="labelRequire"> Form</asp:label></TD>
					<TD width="30%"><asp:dropdownlist id="cboLSAwardMethodID" runat="server" CssClass="select" Width="100%"></asp:dropdownlist></TD>
					<TD width="10%"></TD>
					<TD width="15%"><asp:label id="Label8" runat="server" CssClass="labelRequire">Level</asp:label></TD>
					<TD width="30%"><asp:dropdownlist id="cboLSAwardLevelID" runat="server" CssClass="select" Width="100%"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD width="15%"><asp:label id="Label4" runat="server" CssClass="labelRequire"> Reason</asp:label></TD>
					<TD width="80%" colSpan="4"><asp:textbox id="txtReason" runat="server" CssClass="input" Width="100%" MaxLength="255"></asp:textbox></TD>
				</TR>
				<TR>
					<TD width="15%"></TD>
					<TD width="80%" colSpan="4"><asp:checkbox id="ChkDecision" onclick="javascript:collapseDecision()" runat="server" CssClass="checkbox"
							Checked="True" Text="Is decision?"></asp:checkbox></TD>
				</TR>
				<TR id="trDecision1" runat="server">
					<TD width="15%"><asp:label id="Label11" runat="server" CssClass="labelRequire"> Document No.</asp:label></TD>
					<TD width="30%"><asp:textbox id="txtDecisionNo" runat="server" CssClass="input" Width="100%" MaxLength="255"></asp:textbox></TD>
					<TD width="10%"></TD>
					<TD width="15%"><asp:label id="Label22" runat="server" CssClass="labelRequire">Sign Date</asp:label></TD>
					<TD width="30%"><asp:textbox id="txtSignDate" onblur="javascript:CheckDate(this)" runat="server" CssClass="input"
							Width="90%"></asp:textbox><INPUT class="btnCal" onclick="javascript:popUpCalendar(<%= this.txtSignDate.ClientID %>)" type="button"></TD>
				</TR>
				<TR id="trDecision2" runat="server">
					<TD width="15%"><asp:label id="Label2" runat="server" CssClass="label"> Signer</asp:label></TD>
					<TD width="30%"><asp:textbox id="txtSigner" runat="server" CssClass="input" Width="100%" MaxLength="100"></asp:textbox></TD>
					<TD width="10%"></TD>
					<TD width="15%"><asp:label id="Label5" runat="server" CssClass="label">Post Title</asp:label></TD>
					<TD width="30%"><asp:textbox id="txtSignerPosition" runat="server" CssClass="input" Width="100%" MaxLength="100"></asp:textbox></TD>
				</TR>
				<TR id="trDecision3" runat="server">
					<TD width="15%"><asp:label id="Label6" runat="server" CssClass="label">Effective Date</asp:label></TD>
					<TD width="30%"><asp:textbox id="txtEffectiveDate" onblur="javascript:CheckDate(this)" runat="server" CssClass="input"
							Width="90%"></asp:textbox><INPUT class="btnCal" onclick="javascript:popUpCalendar(<%= this.txtEffectiveDate.ClientID %>)" type="button"></TD>
					<TD width="10%"></TD>
					<TD width="15%"><asp:label id="Label3" runat="server" CssClass="label">Attached file</asp:label></TD>
					<TD width="30%"><INPUT class="input" id="txtFile" style="WIDTH: 100%" tabIndex="1" type="file" size="13"
							name="File1" runat="server"></TD>
				</TR>
				<TR id="trDecision4" runat="server">
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD colSpan="2">
						<asp:HyperLink id="hpSelectFile" CssClass="LinkLeft" runat="server"></asp:HyperLink>
						<asp:LinkButton id="btnDeleteFile" CssClass="DeleteFile" runat="server" ToolTip="Delete Image" Visible="False"></asp:LinkButton></TD>
				</TR>
				<TR>
					<TD>
						<asp:label id="Label10" runat="server" CssClass="label">Description</asp:label></TD>
					<TD colSpan="4">
						<asp:textbox id="txtDescription" runat="server" CssClass="input" Width="100%" MaxLength="255"
							TextMode="MultiLine"></asp:textbox></TD>
				</TR>
				<TR>
					<TD><asp:label id="Label23" runat="server" CssClass="label">Note</asp:label></TD>
					<TD colSpan="4"><asp:textbox id="txtNote" runat="server" CssClass="input" Width="100%" MaxLength="255" TextMode="MultiLine"></asp:textbox></TD>
				</TR>
			</TABLE>
			<asp:TextBox id="txtAttachFile" style="DISPLAY:none" CssClass="input" runat="server"></asp:TextBox>
		</TD>
	</TR>
	<tr>
		<td align="center" colSpan="3">
			<hr>
			<INPUT id="txtAwardRecordID" style="WIDTH: 8px; HEIGHT: 22px" type="hidden" size="1" name="txtAwardRecordID"
				runat="server"><INPUT id="txtDisciplineRecordID" style="WIDTH: 8px; HEIGHT: 22px" type="hidden" size="1"
				name="txtDisciplineRecordID" runat="server"></td>
	</tr>
	<tr>
		<TD noWrap align="center" colspan="4">
			<TABLE id="tblButton" cellSpacing="0" cellPadding="0" width="99%" border="0">
				<TR vAlign="top" height="30">
					<TD vAlign="middle" width="15%">
						<asp:checkbox id="chkShowGrid" accessKey="G" onclick="javascript:collapse('_ctl0_tdGrid');" runat="server"
							CssClass="checkbox" ToolTip="Alt+G" Checked="True" Text="Show grid"></asp:checkbox></TD>
					<TD align="center" width="*">
						<asp:linkbutton id="btnAddNew" accessKey="N" runat="server" CssClass="btnAddnew" ToolTip="Alt+N">AddNew</asp:linkbutton>&nbsp;&nbsp;
						<asp:linkbutton id="btnSave" accessKey="S" runat="server" CssClass="btnSave" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnList" accessKey="L" runat="server" CssClass="btnList" ToolTip="Alt + L">List</asp:linkbutton>&nbsp;&nbsp;
						<asp:linkbutton id="btnDelete" accessKey="D" runat="server" CssClass="btnDelete" ToolTip="Alt + D">Delete</asp:linkbutton>&nbsp;&nbsp;
						<asp:linkbutton id="btnExport" accessKey="E" CssClass="btnExport" runat="server" ToolTip="Alt+E, Export data in grid to excel file">Export</asp:linkbutton></TD>
				</TR>
			</TABLE>
		</TD>
	</tr>
	<TR>
		<TD align="center" colSpan="3" height="10"></TD>
	</TR>
	<tr id="tdGrid" runat="server">
		<td colSpan="3"><FPT:FPTDataGrid id="dtgList" runat="server" Width="100%" AllowPaging="True" AllowSorting="True"
				AutoGenerateColumns="False" CellPadding="0" BorderWidth="1px" MouseOverCssClass="gridhover" ItemCssClass="gridItem"
				AltItemCssClass="gridAlter" MouseClickCssClass="gridClick" ShowMouse="True" FptGridRows="0" NewValues="String[] Array"
				AddEmptyHeaders="0" FptGridColumns="0" IsGroupby="False" FptGridHeader="False">
<AlternatingItemStyle CssClass="gridAlter">
</AlternatingItemStyle>

<FooterStyle HorizontalAlign="Center" CssClass="gridFooter">
</FooterStyle>

<ItemStyle CssClass="gridItem">
</ItemStyle>

<Columns>
<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
<HeaderStyle HorizontalAlign="Center" Width="3%" VerticalAlign="Middle">
</HeaderStyle>

<ItemStyle HorizontalAlign="Center">
</ItemStyle>
</asp:BoundColumn>
<asp:TemplateColumn HeaderText="Select">
<HeaderStyle Width="7%">
</HeaderStyle>

<ItemStyle HorizontalAlign="Center">
</ItemStyle>

<ItemTemplate>
							<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
						
</ItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn HeaderText="&lt;b&gt;#&lt;/b&gt;">
<HeaderStyle Width="5%">
</HeaderStyle>

<ItemStyle HorizontalAlign="Center">
</ItemStyle>

<ItemTemplate>
							<%# Container.ItemIndex + 1%>
						
</ItemTemplate>
</asp:TemplateColumn>
<asp:ButtonColumn DataTextField="Method" HeaderText="Form" CommandName="EDIT"></asp:ButtonColumn>
<asp:BoundColumn DataField="Reason" HeaderText="Reason">
<HeaderStyle Width="30%">
</HeaderStyle>
</asp:BoundColumn>
<asp:BoundColumn DataField="Level" HeaderText="Level">
<HeaderStyle Width="15%">
</HeaderStyle>
</asp:BoundColumn>
<asp:BoundColumn DataField="DecisionNo" HeaderText="Document No">
<HeaderStyle Width="15%">
</HeaderStyle>
</asp:BoundColumn>
</Columns>

<HeaderStyle HorizontalAlign="Center" CssClass="TaskGrid_Head">
</HeaderStyle>

<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages">
</PagerStyle>
			</FPT:FPTDataGrid></td>
	</tr>
</TABLE>
<script language="javascript">
function validform(){	
	if(checkisnull('HR_EmpHeader_txtEmpID')==false)  return false;
	
	if(document.getElementById("_ctl0_optReward").checked==true )
	{
		if(checkisnull('cboLSAwardMethodID')==false)  return false;
		if(checkisnull('cboLSAwardLevelID')==false)  return false;
	}
	else
	{
		if(checkisnull('cboLSDisciplineMethodID')==false)  return false;		
		if(checkisnull('cboLSDisciplineLevelID')==false)  return false;		
	}	
	
	if(checkisnull('txtReason')==false)  return false;
	
	if (document.getElementById('_ctl0_chkDecision').checked==1)
		{			
			if (checkisnull('txtDecisionNo')==false) return false;
			if (checkisnull('txtSignDate')==false) return false;
			if(document.getElementById('_ctl0_txtSignDate').value!="")
				{
					if(FromSmallNow(document.getElementById('_ctl0_txtSignDate')) == false)
					{
						//alert('Date of birth must be less than now');
						GetAlertError(iTotal,DSAlert,"0011");
						document.getElementById('_ctl0_txtSignDate').focus();
						return false;
					}					
				}
		}
	 if (document.getElementById('_ctl0_txtEffectiveDate').value!="")	
	 {
		if(FromSmallToDate(document.getElementById('_ctl0_txtSignDate'),document.getElementById('_ctl0_txtEffectiveDate')) == false)
							{
								GetAlertError(iTotal,DSAlert,"D_0001")
								document.getElementById('_ctl0_txtEffectiveDate').focus();
								return false;
							}		
	 }
	return true;
}
function checkdelete()
{
	if(GridCheck('_ctl0_dtgList',3,1,'chkSelect')==false)
	{
		//alert('Ph?i ch?n ít nh?t 1 dòng tru?c khi xóa!');
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
				//alert('Vui lòng di?n d?y d? d? li?u yêu c?u!!');
				GetAlertError(iTotal,DSAlert,"0003");
				document.getElementById('_ctl0_' + obj).focus();
				return false;
			}
		else
			return true;
	}
function CheckDeleteFile()
	{
		if(confirm(GetAlertText(iTotal,DSAlert,"0006"))==false){
		return false;}
	}
//-->
</script>

