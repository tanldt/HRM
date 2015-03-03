<%@ Import Namespace ="System.Data.SqlClient" %>
<%@ Import Namespace ="System.Data" %>
<%@ Import Namespace ="iHRPCore.Com"%>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Language.ascx.cs" Inherits="iHRPCore.MdlHR.Language" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="EmpHeader" Src="../Include/EmpHeader.ascx" %>
<%@ Register TagPrefix="FPT" Namespace="FPTToolWeb.Control.DataGrids" Assembly="FPTToolWeb" %>
<%
		string strLangID = Session["LangID"] == null?"VN":Session["LangID"].ToString().Trim();
		int Total=0;
		DataTable rs = new DataTable();	
		rs = clsCommon.GetDataTable("sp_GetDataCombo @TableName = 'LS_tblLanguage'");	
		Total=rs.Rows.Count;
%>
<script language="javascript" src='../Include/common.js"'></script>
<script language="javascript">
		//array for level1
		var DS = new Array(3);
		DS[0] = new Array(<%=Total%>);
		DS[1] = new Array(<%=Total%>);
		DS[2] = new Array(<%=Total%>);
		<% for(int i=0; i<Total; i++) {%>		
			DS[0][<%=i%>]="<%=rs.Rows[i]["LSLanguageID"]%>";
			DS[2][<%=i%>]="<%=rs.Rows[i]["IsLanguage"]%>";
			<% if (strLangID=="EN") {%>
				DS[1][<%=i%>]="<%=rs.Rows[i]["Name"]%>";	
			<%} else{%>
				DS[1][<%=i%>]="<%=rs.Rows[i]["VNName"]%>";
			<%}			
			%>	
		<%}%>
		//array for level2
</script>
<script language="javascript">
	function LoadcboLanguage()
	{
		var all = document.getElementById("_ctl0_cboLanguageList").length;
		for(i=0; i<all; i++)
		{	
			document.getElementById("_ctl0_cboLanguageList").remove(0);			
		};
		document.getElementById("_ctl0_cboLanguageList").add(new Option('',''));
		if (document.getElementById("_ctl0_Dropdownlist1").SelectItem.Text == "Ngôn ngữ")
			for(i=0; i<<%=Total%>;i++)
			{
				if (DS[2][i]=="True")
				{
					document.getElementById("_ctl0_cboLanguageList").add(new Option(DS[1][i],DS[0][i]));
				};
			};
		//else if (document.getElementById("_ctl0_optComputing").checked == true)
		else if (document.getElementById("_ctl0_Dropdownlist1").SelectItem.Text == "Vi tính")
			for(i=0; i<<%=Total%>;i++)
			{
				if (DS[2][i]=="False")
				{
					document.getElementById("_ctl0_cboLanguageList").add(new Option(DS[1][i],DS[0][i]));
				};
			};
	}
</script>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="97%" border="0">
	<TR>
		<TD align="left" width="100%"><asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center"><uc1:empheader id="HR_EmpHeader" runat="server"></uc1:empheader></TD>
	</TR>
	<TR>
		<TD align="center"><!-- start detail for input form -->
			<TABLE cellSpacing="0" cellPadding="1" width="99%" border="0">
				<TR style="DISPLAY:none">
					<TD width="15%"><asp:textbox id="txtType" runat="server" Visible="False"></asp:textbox></TD>
					<TD width="47%">&nbsp;<asp:radiobutton id="optLanguage" onclick="LoadcboLanguage()" runat="server" GroupName="a" Text="Language"></asp:radiobutton>
						<asp:radiobutton id="optComputing" onclick="LoadcboLanguage()" runat="server" GroupName="a" Text="Computing"></asp:radiobutton></TD>
					<TD width="8%"></TD>
					<TD width="30%"></TD>
				</TR>
				<TR>
					<TD width="15%">
						<asp:label id="Label2" CssClass="labelData" runat="server" Width="100%">Field</asp:label></TD>
					<TD width="47%">
						<asp:dropdownlist id="Dropdownlist1" CssClass="combo" runat="server" Width="100%" AutoPostBack="True">
<asp:ListItem Value="0">Language</asp:ListItem>
<asp:ListItem Value="1">IT</asp:ListItem>
						</asp:dropdownlist></TD>
					<TD width="8%"></TD>
					<TD width="30%"><asp:label id="Label3" runat="server" CssClass="labelData" Width="100%">Skill and Ability</asp:label></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD><INPUT id="txtLanguageRecordID" style="HEIGHT: 22px" type="hidden" size="1" name="txtLanguageRecordID"
							runat="server"></TD>
					<TD></TD>
					<TD rowSpan="5">
						<DIV style="OVERFLOW: auto; WIDTH: 97%; HEIGHT: 220px"><asp:checkboxlist id="chkSkill" runat="server"></asp:checkboxlist></DIV>
					</TD>
				</TR>
				<TR>
					<TD><asp:label id="lblLanguage" runat="server" CssClass="labelRequire" Width="100%"> Name</asp:label></TD>
					<TD><asp:dropdownlist id="cboLanguageList" runat="server" CssClass="combo" Width="100%" onchange="ChangecboLanguage()"></asp:dropdownlist></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblLanguageLevel" runat="server" CssClass="labelRequire" Width="100%"> Level</asp:label></TD>
					<TD vAlign="top"><asp:dropdownlist id="cboLanguageLevelID" runat="server" CssClass="combo" Width="100%" onchange="ChangecboLanguage()"></asp:dropdownlist></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblNote" runat="server" CssClass="label" Width="100%">Note</asp:label></TD>
					<TD vAlign="top"><asp:textbox id="txtNote" onblur="ChangecboLanguage()" style="POSITION: absolute" runat="server"
							CssClass="input" Width="100%" MaxLength="100"></asp:textbox></TD>
				</TR>
				<TR>
					<TD colSpan="2">
						<TABLE id="Table3" cellSpacing="0" cellPadding="1" width="100%" border="0">
							<TR id="Tr1"> <!-- start grid detail for input form -->
								<TD colSpan="2">
									<DIV style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 150px" runat="server" id="DIV1">
										<FPT:FPTDataGrid id="grdLanguage" runat="server" Width="100%" AllowPaging="True" AllowSorting="True"
											AutoGenerateColumns="False" CellPadding="0" BorderWidth="1px" PageSize="7" MouseOverCssClass="gridhover"
											ItemCssClass="gridItem" AltItemCssClass="gridAlter" MouseClickCssClass="gridClick" ShowMouse="True" FptGridRows="0" NewValues="String[] Array" AddEmptyHeaders="0" FptGridColumns="0" IsGroupby="False" FptGridHeader="False">
<AlternatingItemStyle CssClass="gridAlter">
</AlternatingItemStyle>

<FooterStyle HorizontalAlign="Center" CssClass="gridFooter">
</FooterStyle>

<ItemStyle CssClass="gridItem">
</ItemStyle>

<Columns>
<asp:BoundColumn Visible="False" DataField="LanguageRecordID" HeaderText="Seq">
<HeaderStyle HorizontalAlign="Center" Width="5%" VerticalAlign="Middle">
</HeaderStyle>

<ItemStyle HorizontalAlign="Center">
</ItemStyle>
</asp:BoundColumn>
<asp:TemplateColumn HeaderText="&lt;b&gt;#&lt;/b&gt;">
<HeaderStyle Width="5%">
</HeaderStyle>

<ItemStyle HorizontalAlign="Center">
</ItemStyle>

<ItemTemplate>
														<%# Container.ItemIndex + 1%>
													
</ItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn HeaderText="Certificate">
<HeaderStyle Width="40%">
</HeaderStyle>

<ItemStyle HorizontalAlign="Center">
</ItemStyle>

<ItemTemplate>
														<asp:LinkButton Runat="server" ID="btn" CssClass="Hlink" CommandName="Edit">
															<%# DataBinder.Eval(Container, "DataItem.Language")%>
														</asp:LinkButton>
													
</ItemTemplate>
</asp:TemplateColumn>
<asp:BoundColumn DataField="LanguageLevel" HeaderText="Level">
<HeaderStyle Width="55%">
</HeaderStyle>

<ItemStyle HorizontalAlign="Center">
</ItemStyle>
</asp:BoundColumn>
<asp:BoundColumn DataField="Typeof" HeaderText="Field">
<HeaderStyle Width="55%">
</HeaderStyle>

<ItemStyle HorizontalAlign="Center">
</ItemStyle>
</asp:BoundColumn>
<asp:TemplateColumn HeaderText="Select">
<HeaderStyle Width="40px">
</HeaderStyle>

<ItemStyle HorizontalAlign="Center">
</ItemStyle>

<ItemTemplate>
														<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
													
</ItemTemplate>
</asp:TemplateColumn>
</Columns>

<HeaderStyle HorizontalAlign="Center" CssClass="TaskGrid_Head">
</HeaderStyle>

<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages">
</PagerStyle>
										</FPT:FPTDataGrid></DIV>
								</TD>
							</TR>
						</TABLE>
					</TD>
					<TD></TD>
				</TR>
				<TR>
					<TD colSpan="2">
						<HR align="center" width="95%">
					</TD>
					<TD></TD>
					<TD>
						<HR align="center" width="95%">
					</TD>
				</TR>
				<TR>
					<TD align="center" colSpan="2"><asp:linkbutton id="btnAddNew" accessKey="N" runat="server" CssClass="btnAddnew" ToolTip="Alt+N"> Addnew</asp:linkbutton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnSave" accessKey="S" runat="server" CssClass="btnSave" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
						&nbsp;&nbsp;
						<asp:linkbutton id="btnDelete" accessKey="D" runat="server" CssClass="btnDelete" ToolTip="Alt + D">Delete</asp:linkbutton></TD>
					<TD></TD>
					<TD align="center"><asp:linkbutton id="btnSaveSkill" runat="server" CssClass="btnSave">Save</asp:linkbutton></TD>
				</TR>
				<TR style="DISPLAY: none">
					<TD align="center" colSpan="2"><asp:textbox id="txtLanguageID" runat="server" CssClass="input" Width="100%" MaxLength="100"></asp:textbox></TD>
					<TD></TD>
					<TD align="center"></TD>
				</TR>
			</TABLE> <!-- end button for input form --></TD>
	</TR>
	<!-- end button for input form --></TABLE>
<script language="javascript">
<!--
function validform(){	
	if(checkisnull('HR_EmpHeader_txtEmpID')==false)  return false;
	if(checkisnull('cboLanguageList')==false)  return false;		
	if(checkisnull('cboLanguageLevelID')==false)  return false;		
	return true;
}
function checkdelete()
{
	if(GridCheck('_ctl0_grdLanguage',3,1,'chkSelect')==false)
	{
		GetAlertError(iTotal,DSAlert,"0001");
		return false;
	}
	if(confirm(GetAlertText(iTotal,DSAlert,"0002"))==false){
		return false;
	}
}
//
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
	
function ChangecboLanguage()
{
	value = trim(document.getElementById("_ctl0_cboLanguageList").value);
	document.getElementById("_ctl0_txtLanguageID").value = value;
	//alert(document.getElementById("_ctl0_txtLanguageID").value);
}
//-->
</script>

