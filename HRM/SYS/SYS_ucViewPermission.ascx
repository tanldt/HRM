<%@ Control Language="c#" AutoEventWireup="false" Codebehind="SYS_ucViewPermission.ascx.cs" Inherits="iHRPCore.SYS.SYS_ucViewPermission" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table width="100%" align="center">
	<TR>
		<TD align="center"><asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label></TD>
	</TR>
	<tr>
		<td style="HEIGHT: 33px" align="center"><asp:label id="lblTitle" CssClass="labelTitle" Runat="server">CHI TIẾT TÀI KHOẢN:</asp:label></td>
	</tr>
	<tr id="trAccountInfo" runat="server">
		<td align="center" height="10">
			<table width="80%">
				<tr>
					<td width="12%"><asp:label id="lblUserIDCaption" CssClass="label" Runat="server">Tài khoản</asp:label></td>
					<td width="20%"><asp:textbox id="txtEmpID" CssClass="input" Runat="server" MaxLength="20" ReadOnly="True" Width="100%"></asp:textbox></td>
					<td width="5%"></td>
					<TD width="13%"><asp:label id="Label1" CssClass="label" Runat="server">Tên tài khoản</asp:label></TD>
					<td width="30%"><asp:textbox id="txtEmpName" CssClass="input" Runat="server" MaxLength="20" ReadOnly="True" Width="100%"></asp:textbox></td>
					<td></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td align="center"><asp:label id="Label3" CssClass="label" Runat="server" Width="164px" ForeColor="Gray">Nhóm có các tài khoản</asp:label><asp:label id="Label4" CssClass="label" Runat="server" Width="164px" ForeColor="Gray">Tài khoản này là nhóm</asp:label></td>
	</tr>
	<tr>
		<td align="center"><asp:datagrid id="dgUserAccount" CssClass="grid" runat="server" Width="550px" BackColor="White"
				BorderColor="#3366CC" DataKeyField="UserGroupID" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False"
				AllowSorting="True" AllowPaging="false">
				<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
				<ItemStyle CssClass="gridItem"></ItemStyle>
				<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
				<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
				<Columns>
					<asp:BoundColumn Visible="False" DataField="UserGroupID" HeaderText="Tài khoản">
						<HeaderStyle Font-Size="8pt"></HeaderStyle>
						<ItemStyle Font-Size="8pt" HorizontalAlign="Left" Width="100px"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn Visible="False" HeaderText="Dept">
						<HeaderStyle Font-Size="8pt" Font-Bold="True"></HeaderStyle>
						<ItemStyle Font-Size="8pt" HorizontalAlign="Left" Width="130px"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="UserGroupID" HeaderText="Tài khoản">
						<HeaderStyle Font-Size="8pt"></HeaderStyle>
						<ItemStyle Font-Size="8pt" HorizontalAlign="Left" Width="100px"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="UserGroupName" HeaderText="Chi tiết">
						<HeaderStyle Font-Size="8pt"></HeaderStyle>
						<ItemStyle Font-Size="8pt" HorizontalAlign="Left" Width="130px"></ItemStyle>
					</asp:BoundColumn>
					<asp:ButtonColumn Text="Xoá" CommandName="Delete">
						<ItemStyle Font-Size="8pt" HorizontalAlign="Center" CssClass="btnDelete"></ItemStyle>
					</asp:ButtonColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
			</asp:datagrid></td>
	</tr>
	<TR>
		<TD align="center" height="10"></TD>
	</TR>
	<tr>
		<td align="center"><asp:label id="Label5" CssClass="label" Runat="server" Width="268px" ForeColor="Gray">Cập nhật quyền cho tài khoản/ nhóm</asp:label></td>
	</tr>
	<TR>
		<TD align="center" height="10"></TD>
	</TR>
	<tr>
		<td align="center">
			<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 400px"><asp:datagrid id="dgFunction" CssClass="grid" runat="server" Width="550px" BackColor="White" BorderColor="#3366CC"
					DataKeyField="FunctionID" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="false">
					<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
					<ItemStyle CssClass="gridItem"></ItemStyle>
					<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
					<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
					<Columns>
						<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="Lưu" CancelText="Huỷ" EditText="Sửa">
							<ItemStyle HorizontalAlign="Center" CssClass="btnSave"></ItemStyle>
						</asp:EditCommandColumn>
						<asp:BoundColumn Visible="False" DataField="FunctionNameE" HeaderText="Function name">
							<ItemStyle Font-Size="8pt" HorizontalAlign="Left" Width="100px"></ItemStyle>
						</asp:BoundColumn>
						<asp:TemplateColumn HeaderText="Tên chức năng">
							<ItemStyle Font-Size="8pt" Width="100px"></ItemStyle>
							<ItemTemplate>
								<asp:label id="Label6" Width=100 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FunctionNameE") %>'>
								</asp:label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn Visible="False" DataField="FunctionNameV" HeaderText="Function name in Viet">
							<HeaderStyle Font-Size="8pt"></HeaderStyle>
							<ItemStyle Font-Size="8pt" HorizontalAlign="Left" Width="100px"></ItemStyle>
						</asp:BoundColumn>
						<asp:TemplateColumn HeaderText="Tên chi tiết chức năng">
							<ItemStyle Font-Size="8pt" Width="100px"></ItemStyle>
							<ItemTemplate>
								<asp:label id="Label7" Width=100 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FunctionNameV") %>'>
								</asp:label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn Visible="False" DataField="FRun" HeaderText="Quyền chạy">
							<ItemStyle Font-Size="8pt" HorizontalAlign="Left" Width="100px"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn Visible="False" DataField="FAdd" HeaderText="Quyền cập nhật">
							<ItemStyle Font-Size="8pt" HorizontalAlign="Left" Width="100px"></ItemStyle>
						</asp:BoundColumn>
						<asp:TemplateColumn HeaderText="Quyền chạy">
							<ItemStyle Font-Size="8pt" HorizontalAlign="Center" Width="100px"></ItemStyle>
							<ItemTemplate>
								<asp:label id="lblFRun" Width=50 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FRun") %>'>
								</asp:label>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox id=txtFRun onblur="checkNumeric(this)" Height=20 style="TEXT-ALIGN:Center" runat="server" Font-Name="Arial" Font-Size="8pt" MaxLength=1 Width=50 Text='<%# DataBinder.Eval(Container, "DataItem.FRun")%>'>
								</asp:TextBox>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Quyền cập nhật">
							<ItemStyle Font-Size="8pt" HorizontalAlign="Center" Width="100px"></ItemStyle>
							<ItemTemplate>
								<asp:label id="lblFAdd" Width=50 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FAdd") %>'>
								</asp:label>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox id="txtFAdd" onblur="checkNumeric(this)" Height=20 style="TEXT-ALIGN:Center" runat="server" Font-Name="Arial" Font-Size="8pt" MaxLength=1 Width=50 Text='<%# DataBinder.Eval(Container, "DataItem.FAdd")%>'>
								</asp:TextBox>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:ButtonColumn Text="Xoá" CommandName="Delete">
							<ItemStyle Font-Size="8pt" HorizontalAlign="Center" CssClass="btnDelete"></ItemStyle>
						</asp:ButtonColumn>
						<asp:BoundColumn Visible="False" DataField="Specific" HeaderText="Specific">
							<ItemStyle Font-Size="8pt" HorizontalAlign="Left" Width="100px"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn Visible="False" DataField="ModuleName" HeaderText="ModuleName">
							<ItemStyle Font-Size="8pt" HorizontalAlign="Left" Width="100px"></ItemStyle>
						</asp:BoundColumn>
					</Columns>
					<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
				</asp:datagrid></div>
		</td>
	</tr>
</table>
