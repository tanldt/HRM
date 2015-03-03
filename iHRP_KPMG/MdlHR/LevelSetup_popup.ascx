<%@ Control Language="c#" AutoEventWireup="false" Codebehind="LevelSetup_popup.ascx.cs" Inherits="iHRPCore.MdlHR.LevelSetup_popup" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<script language="javascript">
		function ReturnLevelValue()
		{						
			var txtParentName=document.getElementById('_ctl0_txtParentName');									
			var txtParentCode=document.getElementById('_ctl0_txtParentCode');												
			//alert(opener.document.getElementById('_ctl0_txtParentCode').value);
			opener.document.getElementById('_ctl0_txtParentCode').value=txtParentCode.value;												
			//alert(txtParentCode.value);
			//alert(txtParentName.value);
			opener.AddList(txtParentName);
			document.close();
			window.close();			
		}
</script>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR style="DISPLAY:none">
		<TD width="451" style="WIDTH: 451px"><asp:imagebutton id="imgbtnLogo" Visible="False" ImageUrl="../images/Banner_06.jpg" HEIGHT="66" runat="server"></asp:imagebutton><asp:imagebutton id="imgbtnTop02" Visible="False" ImageUrl="../images/Banner_10.jpg" HEIGHT="66"
				runat="server" Width="352px"></asp:imagebutton></TD>
		<TD vAlign="middle" align="center" width="64" style="WIDTH: 64px"></TD>
		<TD width="476" style="WIDTH: 476px"></TD>
	</TR>
	<TR>
		<TD width="451" style="WIDTH: 451px">
			<DIV style="OVERFLOW: auto; WIDTH: 97%; HEIGHT: 220px"><asp:datagrid id="dtgLevelParent" runat="server" CssClass="grid" Width="416px" AllowSorting="True"
					AutoGenerateColumns="False" CellPadding="0" BorderWidth="1px" BorderColor="#3366CC" BackColor="White" HorizontalAlign="Center"><AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
					<ItemStyle CssClass="gridItem"></ItemStyle>
					<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
					<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
					<Columns>
						<asp:BoundColumn DataField="ParentLevelCode" HeaderText="Code">
							<HeaderStyle HorizontalAlign="Center" Width="20%" VerticalAlign="Middle"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="ParentLevelName" HeaderText="Name">
							<HeaderStyle HorizontalAlign="Center" Width="75%" VerticalAlign="Middle"></HeaderStyle>
							<ItemStyle HorizontalAlign="Left"></ItemStyle>
						</asp:BoundColumn>
						<asp:TemplateColumn HeaderText="Ch?n">
							<HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<HeaderTemplate>
								<asp:CheckBox id="chkSelectAll" onclick="CheckAll('_ctl0_dtgLevelParent__ctl1_chkSelectAll','_ctl0_dtgLevelParent',2,1,'chkSelect')"
									runat="server" CssClass="gridFooter"></asp:CheckBox>
							</HeaderTemplate>
							<ItemTemplate>
								<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
					<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
				</asp:datagrid></DIV>
		</TD>
		<TD vAlign="middle" align="center" width="64" style="WIDTH: 64px"><asp:linkbutton id="btnSelectAll" accessKey="N" Visible="False" runat="server" CssClass="btnSave"
				ToolTip="Alt+N">>></asp:linkbutton><br>
			<asp:linkbutton id="btnSelect" accessKey="N" runat="server" CssClass="btnSave" ToolTip="Alt+N">></asp:linkbutton><br>
			<asp:linkbutton id="btnRemove" accessKey="N" runat="server" CssClass="btnSave" ToolTip="Alt+N"><</asp:linkbutton><br>
			<asp:linkbutton id="btnRemoveAll" accessKey="N" Visible="False" runat="server" CssClass="btnSave"
				ToolTip="Alt+N"><<</asp:linkbutton></TD>
		<TD width="476" style="WIDTH: 476px">
			<DIV style="OVERFLOW: auto; WIDTH: 97%; HEIGHT: 220px"><asp:datagrid id="dtgLevelParent_select" runat="server" CssClass="grid" Width="422px" AllowSorting="True"
					AutoGenerateColumns="False" CellPadding="0" BorderWidth="1px" BorderColor="#3366CC" BackColor="White" HorizontalAlign="Center">
					<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
					<ItemStyle CssClass="gridItem"></ItemStyle>
					<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
					<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
					<Columns>
						<asp:BoundColumn DataField="ParentLevelCode" HeaderText="Code">
							<HeaderStyle HorizontalAlign="Center" Width="20%" VerticalAlign="Middle"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="ParentLevelName" HeaderText="Name">
							<HeaderStyle HorizontalAlign="Center" Width="75%" VerticalAlign="Middle"></HeaderStyle>
							<ItemStyle HorizontalAlign="Left"></ItemStyle>
						</asp:BoundColumn>
						<asp:TemplateColumn HeaderText="Ch?n">
							<HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<HeaderTemplate>
								<asp:CheckBox id="chkSelectAll_" onclick="CheckAll('_ctl0_dtgLevelParent_select__ctl1_chkSelectAll_','_ctl0_dtgLevelParent_select',2,1,'chkSelect_')"
									runat="server" CssClass="gridFooter"></asp:CheckBox>
							</HeaderTemplate>
							<ItemTemplate>
								<asp:CheckBox id="chkSelect_" runat="server" CssClass="checkbox"></asp:CheckBox>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
					<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
				</asp:datagrid></DIV>
			<INPUT id="txtParentCode" type="hidden" name="Hidden1" runat="server"> <INPUT id="txtParentName" type="hidden" name="Hidden1" runat="server">
		</TD>
	</TR>
	<TR>
		<TD width="451" style="WIDTH: 451px"></TD>
		<TD vAlign="middle" align="center" width="64" style="WIDTH: 64px"><asp:linkbutton id="btnAccept" accessKey="N" runat="server" CssClass="btnSave" ToolTip="Alt+N">Accept</asp:linkbutton></TD>
		<TD width="476" style="WIDTH: 476px"></TD>
	</TR>
</TABLE>
