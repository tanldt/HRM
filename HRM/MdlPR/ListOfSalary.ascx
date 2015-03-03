<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ListOfSalary.ascx.cs" Inherits="iHRPCore.MdlPR.ListOfSalary" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="Table1" style="WIDTH: 100%; HEIGHT: 261px" cellSpacing="0" cellPadding="0" width="100%"
	border="0">
	<tr>
		<td>
			<asp:label id="lblErr" CssClass="label" Runat="server" />
			
		</td>
	</tr>
	
	<TR>
		<TD style="HEIGHT: 341px">
			<DIV id="Div1" style="OVERFLOW: auto; WIDTH: 800px; HEIGHT: 438px" runat="server"><asp:datagrid id="dtgList" runat="server" BorderColor="DodgerBlue" AllowSorting="True" CellPadding="1"
					AutoGenerateColumns="False" Width="100%">
					<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
					<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
					<Columns>
						<asp:BoundColumn DataField="LSSalaryItemID" Visible="False"></asp:BoundColumn>
						<asp:TemplateColumn HeaderText="Salary Item">
							<HeaderStyle Width="120px"></HeaderStyle>
							<ItemStyle HorizontalAlign="Left"></ItemStyle>
							<ItemTemplate>
								<a href="javascript:PopUp_Open('<%# DataBinder.Eval(Container, "DataItem.LSSalaryItemID") %>');" class=Hlink><%# DataBinder.Eval(Container, "DataItem.LSSalaryItemCode") %></a>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Salary Item">
							<HeaderStyle Width="100px"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<ItemTemplate>
								<asp:Label id="lblSalaryItem" CssClass=label Width="100px" Text='<%# DataBinder.Eval(Container, "DataItem.Name") %>' Runat="server">
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Fomular">
							<HeaderStyle Width="200px"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<ItemTemplate>
								<asp:TextBox id="txtFormular" style="OVERFLOW: auto" TextMode=MultiLine ReadOnly=True CssClass=input Width="250px" Text='<%# DataBinder.Eval(Container, "DataItem.Formula") %>' Runat="server">
								</asp:TextBox>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn DataField="Value" HeaderText="Value">
							<HeaderStyle HorizontalAlign="Right" Width="50px"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="ComputationSeq" HeaderText="Comp Seq">
							<HeaderStyle Width="30px"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="RptOrdinal" HeaderText="Report Seq">
							<HeaderStyle Width="30px"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
						</asp:BoundColumn>
						<asp:TemplateColumn HeaderText="in screen">
							<HeaderStyle Width="30px"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<ItemTemplate>
								
								<%# DataBinder.Eval(Container, "DataItem.Visible") %>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn DataField="Rank" HeaderText="Seq">
							<HeaderStyle Width="30px"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="LSSalaryItemTypeCode" HeaderText="Salary Item Type">
							<HeaderStyle Width="100px"></HeaderStyle>
						</asp:BoundColumn>
						<asp:TemplateColumn HeaderText="Chọn">
							<HeaderStyle Width="30px"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<HeaderTemplate>
								<asp:CheckBox id="chkSelectAll" onclick="CheckAll('_ctl0_dtgList__ctl1_chkSelectAll','_ctl0_dtgList',2,1,'chkSelect')" runat="server" CssClass="gridFooter"></asp:CheckBox>
							</HeaderTemplate>
							<ItemTemplate>
								<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
				</asp:datagrid></DIV>
		</TD>
	</TR>
	<TR>
		<TD align="center"><asp:linkbutton id="btnAddnew" accessKey="E" runat="server" ToolTip="Alt + A" CssClass="btnExport">Addnew</asp:linkbutton>&nbsp;
			<asp:linkbutton id="Linkbutton1" accessKey="E" runat="server" ToolTip="Alt + U" CssClass="btnExport"
				Visible="False">Update</asp:linkbutton>&nbsp;
			<asp:linkbutton id="btnDelete" accessKey="E" runat="server" ToolTip="Alt + D" CssClass="btnExport">Delete</asp:linkbutton>&nbsp;</TD>
	</TR>
	<tr style="DISPLAY: none">
		<td><asp:button id=btnRefresh runat="server" Height="21px" Font-Names="Arial" Font-Size="8pt" CssClass="ButtonCommand" Text="Refresh"></asp:button></td>
	</tr>
</TABLE>
<script language="javascript">
function checkdelete()
{
	if(GridCheck('_ctl0_dtgList',2,1,'chkSelect')==false)
	{
		GetAlertTextPopUp('<%=GetText("COMMON","COMFONESELECT")%>');
		return false;
	}
	if(confirm('<%=GetText("COMMON","CONFIRMDELETE")%>')==false){
		return false;
	}
}
function PopUp_Addnew()
{			
	//ShowDialog('HR\Residence.aspx',700,430);
	ShowDialog('FormPage.aspx?ModuleID=PR&ParentID=68&FunctionID=546&Ascx=MdlPR/ListOfSalary_Popup.ascx',800,600);
}
function PopUp_Open(ID)
{			
	//ShowDialog('HR\Residence.aspx',700,430);
	ShowDialog('FormPage.aspx?ModuleID=PR&ParentID=68&FunctionID=546&Ascx=MdlPR/ListOfSalary_Popup.ascx&IDCode=' + ID,800,600);
}
</script>

