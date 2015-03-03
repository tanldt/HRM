<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ExportCustom.ascx.cs" Inherits="MdlPR.ExportCustom" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table id="stepList" cellSpacing="1" cellPadding="0" width="100%" runat="server">
	<tr>
		<td><asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></td>
	</tr>
	<tr>
		<td>
			<!--Select employees-->
			<TABLE id="SearchEmp" border="0" cellSpacing="0" cellPadding="0" width="100%">
				<TR>
					<TD align="center"><asp:button id="cmdAdd" runat="server" CssClass="btnAddnew" Text="New"></asp:button>&nbsp;
						<asp:button id="cmdDelete" runat="server" CssClass="btnAddnew" Text="Delete"></asp:button><br>
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
								<asp:BoundColumn Visible="False" DataField="ExportCustomID" HeaderText="ExportCustomID"></asp:BoundColumn>
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
								<asp:BoundColumn DataField="NameExport" HeaderText="Name Export">
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
			<TABLE id="Table2" border="0" cellSpacing="2" cellPadding="2" width="98%">
				<TR style="DISPLAY: none">
					<TD width="14%"><asp:label id="Label4" runat="server" CssClass="Labelrequire">Name</asp:label></TD>
					<TD><asp:textbox id="txtExportCustomID" runat="server" CssClass="input"></asp:textbox></TD>
				</TR>
				<TR>
					<TD width="14%"><asp:label id="lblDescription" runat="server" CssClass="Labelrequire">Name</asp:label></TD>
					<TD><asp:textbox id="txtNameExport" runat="server" CssClass="input" Width="100%" MaxLength="200"></asp:textbox></TD>
				</TR>
				<TR>
					<TD width="14%"><asp:label id="Label1" runat="server" CssClass="label">Note</asp:label></TD>
					<TD><asp:textbox id="txtNote" runat="server" CssClass="input" Width="100%" MaxLength="500"></asp:textbox></TD>
				</TR>
				<TR style="DISPLAY:none">
					<TD colSpan="2"><asp:datagrid id="dtgList" runat="server" HorizontalAlign="Center" AutoGenerateColumns="False"
							Width="100%" AllowSorting="false">
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle CssClass="gridHeader"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="Code" Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="Visible" Visible="False"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="&lt;b&gt;Seq&lt;/b&gt;">
									<ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
									<ItemTemplate>
										<%# Container.ItemIndex + 1 + dtgListMaster.PageSize*dtgListMaster.CurrentPageIndex%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Code" HeaderText="Item">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Export">
									<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
									<HeaderTemplate>
										<asp:Label Runat="server" ID="Label2">Column Seq</asp:Label>
									</HeaderTemplate>
									<ItemTemplate>
										<asp:textbox id="txtSeq" runat="server" CssClass="Input" Text='<%# DataBinder.Eval(Container, "DataItem.No") %>' Width="100%">
										</asp:textbox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Name">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<HeaderTemplate>
										<asp:Label Runat="server" ID="lblFormula">Column Name</asp:Label>
									</HeaderTemplate>
									<ItemTemplate>
										<asp:textbox id="txtName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Name") %>' CssClass="Input" Width="100%">
										</asp:textbox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Note">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<HeaderTemplate>
										<asp:Label Runat="server" ID="Label3">Visible</asp:Label>
									</HeaderTemplate>
									<ItemTemplate>
										<asp:CheckBox ID="chkVisible" Runat="server"></asp:CheckBox>
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
			<table>
				<TR>
					<TD align="left"><asp:label id="Label7" runat="server" CssClass="labelrequire">Available Fields</asp:label></TD>
					<TD align="center"></TD>
					<TD align="left"><asp:label id="Label8" runat="server" CssClass="labelrequire">Assigned Fields</asp:label></TD>
					<TD align="center"></TD>
				</TR>
				<tr align="center">
					<td align="center"><asp:listbox id="lstListComp" runat="server" CssClass="combo" Width="232px" Font-Names="Arial"
							Font-Size="8pt" SelectionMode="Multiple" Height="300px"></asp:listbox></td>
					<td align="center">
						<P><BUTTON style="WIDTH: 25px" onclick="addAttribute()">&gt;</BUTTON><button style="WIDTH: 30px" onclick="addAll()">&gt;&gt;</button></P>
						<P><BUTTON style="WIDTH: 30px" onclick="delAll()">&lt;&lt;</BUTTON><BUTTON style="WIDTH: 25px" onclick="delAttribute()">&lt;</BUTTON></P>
					</td>
					<TD align="center"><asp:listbox id="lstListGroup" runat="server" CssClass="combo" Width="232px" Font-Names="Arial"
							Font-Size="8pt" SelectionMode="Multiple" Height="300px"></asp:listbox></TD>
					<TD align="center">
						<img id="btnMoveUp" onclick="moveUp();" alt="Move selected fields up" title="Move selected fields up"
							src="Images/up.gif"><br>
						<br>
						<img id="btnMoveDown" onclick="moveDown();" alt="Move selected fields down" title="Move selected fields down"
							src="Images/down.gif">
					</TD>
				</tr>
			</table>
		</td>
	</tr>
	<TR style="DISPLAY: none">
		<TD align="left" height="21"><asp:textbox id="txtLstListGroup" Runat="server"></asp:textbox>
			<asp:textbox id="txtLstListGroupText" Runat="server"></asp:textbox>
			<asp:textbox id="txtListLen" Runat="server"></asp:textbox>
		</TD>
	</TR>
	<tr>
		<td align="center"><asp:button id="cmdSave" runat="server" CssClass="btnAddnew" Text="Save"></asp:button>&nbsp;
			<asp:button id="cmdCancel" runat="server" CssClass="btnAddnew" Text="Cancel"></asp:button>&nbsp;
		</td>
	</tr>
	<tr>
		<td></td>
	</tr>
</table>
<asp:label id="cursteplabel" runat="server" Visible="false"></asp:label>
<script type="text/javascript" src="Images/archive.js"></script>
<script type="text/javascript" src="Images/octopus.js"></script>
<SCRIPT language="javascript">

function moveUp() {
		res = moveSelectionsUp($('_ctl0_lstListGroup'), 'Please select at least one field from the Assigned fields to move');
	}

	function moveDown() {
		res = moveSelectionsDown($('_ctl0_lstListGroup'), 'Please select at least one field from the Assigned fields to move');
	}

function validform(){
	
	if(checkisnull('txtNameExport')==false)  return false;
	var all=document.getElementById('_ctl0_lstListGroup').length;
		var sValue='';
		var sText='';
		for(i=0; i<all; i++)
			{	
				sText += document.getElementById('_ctl0_lstListGroup').item(i).text + ',';
				sValue+= document.getElementById('_ctl0_lstListGroup').item(i).value+',';
			};	
		document.getElementById('_ctl0_txtLstListGroup').value=sValue;
		document.getElementById('_ctl0_txtLstListGroupText').value=sText;
				
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
<SCRIPT language="javascript">
createListObjects('_ctl0_lstListGroup','_ctl0_lstListComp')
</SCRIPT>
