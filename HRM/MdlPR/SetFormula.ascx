<%@ Control Language="c#" AutoEventWireup="false" Codebehind="SetFormula.ascx.cs" Inherits="MdlPR.SetFormula" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table id="stepList" cellSpacing="1" cellPadding="0" width="100%" runat="server">
	<tr>
		<td><asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></td>
	</tr>
	<tr>
		<td>
			<!--Select employees-->
			<TABLE id="SearchEmp" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD align="center"><asp:button id="cmdAdd" runat="server" Text="New" CssClass="btnAddnew"></asp:button>&nbsp;
						<asp:button id="cmdDelete" runat="server" Text="Delete" CssClass="btnAddnew"></asp:button><br>
					</TD>
				</TR>
				<TR>
					<TD><br>
						<asp:datagrid id="dtgListMaster" runat="server" HorizontalAlign="Center" AutoGenerateColumns="False"
							Width="100%">
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle CssClass="gridHeader"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="SetFormulaID" HeaderText="SetFormulaID"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Select">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox id="chkSelect" CssClass="checkbox" runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn Visible="False" HeaderText="&lt;b&gt;Seq&lt;/b&gt;">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<%# Container.ItemIndex + 1 + dtgListMaster.PageSize*dtgListMaster.CurrentPageIndex%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="NameFormula" HeaderText="Name Formula">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Note" HeaderText="Note">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Edit">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton Runat="server" ID='btnEdit' CommandName="Edit">Edit</asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></TD>
				</TR>
			</TABLE>
			<!--end Select employees--></td>
	</tr>
</table>
<table id="stepAdd" cellSpacing="1" cellPadding="0" width="100%" runat="server" visible="false">
	<tr>
		<td>
			<!--request-->
			<TABLE id="Table2" cellSpacing="2" cellPadding="2" width="98%" border="0">
				<TR style="DISPLAY: none">
					<TD width="14%"><asp:label id="Label4" runat="server" CssClass="Labelrequire">Name</asp:label></TD>
					<TD><asp:textbox id="txtSetFormulaID" runat="server" CssClass="input"></asp:textbox></TD>
				</TR>
				<TR>
					<TD width="14%"><asp:label id="lblDescription" runat="server" CssClass="Labelrequire">Name</asp:label></TD>
					<TD><asp:textbox id="txtNameFormula" runat="server" Width="100%" MaxLength="200" CssClass="input"></asp:textbox></TD>
				</TR>
				<TR>
					<TD width="14%"><asp:label id="Label1" runat="server" CssClass="label">Note</asp:label></TD>
					<TD><asp:textbox id="txtNote" runat="server" Width="100%" MaxLength="500" CssClass="input"></asp:textbox></TD>
				</TR>
				<TR>
					<TD width="14%"></TD>
					<TD>
						<asp:DropDownList id="cboFormula" runat="server"></asp:DropDownList>&nbsp;
						<asp:button id="cmdGetFor" runat="server" CssClass="btnAddnew" Text="Get Formula from"></asp:button>&nbsp;</TD>
				</TR>
				<TR>
					<TD colSpan="2"><asp:datagrid id="dtgListFormula" runat="server" HorizontalAlign="Center" AutoGenerateColumns="False"
							AllowSorting="false" Width="100%">
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle CssClass="gridHeader"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="LSSalaryItemDataID" Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="LSSalaryItemTaxID" Visible="False"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="&lt;b&gt;Seq&lt;/b&gt;">
									<ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
									<ItemTemplate>
										<%# Container.ItemIndex + 1 + dtgListFormula.PageSize*dtgListFormula.CurrentPageIndex%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="LSSalaryItemDataCode" HeaderText="Salary Item">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Formula">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<HeaderTemplate>
										<asp:Label Runat="server" ID="Label7">Name</asp:Label>
									</HeaderTemplate>
									<ItemTemplate>
										<asp:textbox id="txtName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Name") %>' TextMode=MultiLine CssClass="TextMode" Width="100px">
										</asp:textbox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Formula">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<HeaderTemplate>
										<asp:Label Runat="server" ID="Label5">Tax Type</asp:Label>
									</HeaderTemplate>
									<ItemTemplate>
										<asp:DropDownList id="cboTaxType" Width="150px" runat="server"></asp:DropDownList>&nbsp;
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Formula">
									<ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
									<HeaderTemplate>
										<asp:Label Runat="server" ID="Label2">Seq Formula</asp:Label>
									</HeaderTemplate>
									<ItemTemplate>
										<asp:textbox id="txtSeq" runat="server" CssClass="Input" Text='<%# DataBinder.Eval(Container, "DataItem.No") %>' Width="100%">
										</asp:textbox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Formula">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<HeaderTemplate>
										<asp:Label Runat="server" ID="lblFormula">Formula</asp:Label>
									</HeaderTemplate>
									<ItemTemplate>
										<asp:textbox id="txtFormula" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Formula") %>' TextMode=MultiLine CssClass="TextMode" Width="150px">
										</asp:textbox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Note">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<HeaderTemplate>
										<asp:Label Runat="server" ID="Label3">Note</asp:Label>
									</HeaderTemplate>
									<ItemTemplate>
										<asp:textbox id="txtsNote" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.sNote") %>' TextMode=MultiLine CssClass="TextMode" Width="100px">
										</asp:textbox>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></TD>
				</TR>
			</TABLE>
			<!--End Request--></td>
	</tr>
	<tr>
		<td align="center">
			<asp:button id="btnSaveC" runat="server" CssClass="btnAddnew" Text="Save &amp; Cont.."></asp:button>&nbsp;&nbsp;<asp:button id="cmdSave" runat="server" Text="Save &amp; Close" CssClass="btnAddnew"></asp:button>&nbsp;
			<asp:button id="cmdCancel" runat="server" Text="Cancel" CssClass="btnAddnew"></asp:button>&nbsp;
		</td>
	</tr>
	<tr>
		<td>
			<table width="100%">
				<tr>
					<td valign="top">
						<asp:label id="lblDescriptionSys" CssClass="label" Runat="server">Param system</asp:label>
						<asp:datagrid id="dtgSysItem" runat="server" AutoGenerateColumns="False" AllowSorting="false">
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle CssClass="gridHeader"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="&lt;b&gt;Seq&lt;/b&gt;">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<%# Container.ItemIndex + 1 + dtgSysItem.PageSize*dtgSysItem.CurrentPageIndex%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="LSSalaryItemDataCode" HeaderText="Salary Item">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Name" HeaderText="Description">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="VNName" HeaderText="Description VN">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
						</asp:datagrid>
					</td>
					<td valign="top">
						<asp:label id="Label6" CssClass="label" Runat="server">Users input values</asp:label>
						<asp:datagrid id="dtgUserItem" runat="server" AutoGenerateColumns="False" AllowSorting="false">
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle CssClass="gridHeader"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="&lt;b&gt;Seq&lt;/b&gt;">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<%# Container.ItemIndex + 1 + dtgUserItem.PageSize*dtgUserItem.CurrentPageIndex%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="LSSalaryItemDataCode" HeaderText="Salary Item">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Name" HeaderText="Description">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
						</asp:datagrid>
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>
<asp:label id="cursteplabel" runat="server" Visible="false"></asp:label>
<SCRIPT language="javascript">
<!--
function validform(){
	
	if(checkisnull('txtNameFormula')==false)  return false;
				
	return true;
}
function checkisnull(obj){
	if(document.getElementById('_ctl0_' + obj).value=="")
		{
			GetAlertError(iTotal,DSAlert,"0003");
			document.getElementById('_ctl0_' + obj).focus();
			return false;
		}
	else
		return true;
}
function checkdelete()
{
	if(GridCheck('_ctl0_dtgListMaster',2,1,'chkSelect')==false)
	{
		GetAlertError(iTotal,DSAlert,"0001");
		return false;
	}
	if(confirm(GetAlertText(iTotal,DSAlert,"0002"))==false){
		return false;
	}
}


//-->
</SCRIPT>
