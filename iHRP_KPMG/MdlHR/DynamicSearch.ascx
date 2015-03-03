<%@ Control Language="c#" AutoEventWireup="false" Codebehind="DynamicSearch.ascx.cs" Inherits="iHRPCore.MdlHR.DynamicSearch" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../Include/EmpHeaderSearch.ascx" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="770" align="center" border="0">
	<TR>
		<TD align="left"><A href="Home.aspx"><font style="FONT-WEIGHT: bold; FONT-SIZE: 10px; FONT-FAMILY: Arial">Homepage</font></A></TD>
	</TR>
	<TR>
		<TD align="center">
			<uc1:EmpHeaderSearch id="EmpHeaderSearch1" runat="server"></uc1:EmpHeaderSearch></TD>
	</TR>
	<TR>
		<TD align="center">
			<HR width="100%" SIZE="1">
		</TD>
	</TR>
	<TR>
		<TD align="center">
			<asp:label id="lblError" runat="server" CssClass="lblErr"></asp:label></TD>
	</TR>
	<tr>
		<td>
			<table width="770">
				<tr>
					<td style="WIDTH: 136px" width="136">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:label id="Label1" Font-Size="8" Height="17px" Font-Name="Arial" Runat="server">Criteria</asp:label></td>
					<td width="250"><asp:dropdownlist id="cboCriteria" runat="server" Font-Names="Arial" Font-Size="8pt" Width="248px"
							AutoPostBack="True"></asp:dropdownlist></td>
					<td vAlign="middle" align="center" width="100" rowSpan="4"><asp:button id="cmdAdd" runat="server" Font-Bold="True" Width="79px" Height="22px" Text=">>"></asp:button>
						<P></P>
						<P><asp:button id="btRemove" runat="server" Font-Bold="True" Width="79px" Height="22px" Text="<<"></asp:button></P>
					</td>
					<td rowSpan="4"><asp:textbox id="txtFillCondition" runat="server" Width="246px" Height="94px" TextMode="MultiLine"
							ReadOnly="True"></asp:textbox></td>
				</tr>
				<TR>
					<TD style="WIDTH: 136px" width="136">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:label id="Label2" Font-Size="8" Height="17px" Font-Name="Arial" Runat="server">Condition</asp:label></TD>
					<TD width="250"><asp:dropdownlist id="cboCondition" runat="server" Font-Names="Arial" Font-Size="8pt" Width="249"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 136px" width="136">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:label id="Label3" Font-Size="8" Height="17px" Font-Name="Arial" Runat="server">Information</asp:label></TD>
					<TD width="250"><asp:dropdownlist id="cboInfo" Font-Names="Arial" Font-Size="8pt" Width="250" Height="22px" Runat="server"
							Visible="False"></asp:dropdownlist><asp:textbox id="txtDate" onblur="JavaScript:CheckDate1(this)" Font-Names="Arial" Font-Size="8pt"
							Width="250px" Height="21px" Runat="server" Visible="False" MaxLength="10"></asp:textbox><asp:textbox id="txtNumber" onblur="JavaScript:checkNumeric(this)" Font-Names="Arial" Font-Size="8pt"
							Width="250px" Height="21px" Runat="server" Visible="False"></asp:textbox><asp:textbox id="txtInfo" Font-Names="Arial" Font-Size="8pt" Width="250px" Height="21px" Runat="server"
							MaxLength="100"></asp:textbox>
						<div id="divBool" style="DISPLAY: none" runat="server"><asp:radiobutton id="rdTrue" Font-Names="Arial" Font-Size="8pt" Width="71" Height="20px" Runat="server"
								Text="True" GroupName="Bool" Checked="True"></asp:radiobutton><asp:radiobutton id="rdFalse" Font-Names="Arial" Font-Size="8pt" Width="71" Height="20" Runat="server"
								Text="False" GroupName="Bool"></asp:radiobutton></div>
					</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 136px" width="136">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:label id="Label4" Font-Size="8" Height="17px" Font-Name="Arial" Runat="server">Link</asp:label></TD>
					<TD width="250"><asp:radiobutton id="rdAnd" Font-Names="Arial" Font-Size="8pt" Width="71px" Height="20px" Runat="server"
							Text="Và" GroupName="Link" Checked="True"></asp:radiobutton><asp:radiobutton id="rdOr" Font-Names="Arial" Font-Size="8pt" Width="71px" Height="20px" Runat="server"
							Text="Hoặc" GroupName="Link"></asp:radiobutton></TD>
				</TR>
				<tr>
					<td style="WIDTH: 136px" align="left" colSpan="1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
						&nbsp;&nbsp;&nbsp;&nbsp;<asp:label id="Label5" Font-Size="8" Width="104px" Height="17px" Font-Name="Arial" Runat="server">Fields View</asp:label>
					</td>
					<td><asp:listbox id="lsbColumnsView" runat="server" Font-Names="Arial" Font-Size="8pt" Width="232px"
							Height="145px" SelectionMode="Multiple"></asp:listbox></td>
					<td vAlign="middle" align="left" colSpan="2">
						<P><asp:button id="cmdList" tabIndex="0" runat="server" Font-Names="Arial" Font-Size="8pt" Width="130px"
								Text="View" CssClass="buttoncommand"></asp:button>&nbsp;&nbsp;&nbsp;
							<asp:button id="cmdExport" tabIndex="0" runat="server" Font-Names="Arial" Font-Size="8pt" Width="130px"
								Text="Export data to Excel" CssClass="buttoncommand"></asp:button></P>
						<P>
							<asp:label id="lblCountEmp" runat="server" CssClass="lblErr"></asp:label><INPUT id="txtColType" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" runat="server"
								NAME="txtColType"></P>
					</td>
				<tr>
					<td colSpan="4">
						<asp:datagrid id="dtgEmp" runat="server" Font-Names="Arial" Font-Size="8pt" Width="750px" PageSize="20"
							AutoGenerateColumns="False" CellPadding="2" HorizontalAlign="Center" AllowSorting="True" BorderColor="#999999"
							AllowPaging="True">
							<AlternatingItemStyle Font-Size="8pt" Font-Names="Arial" BackColor="WhiteSmoke"></AlternatingItemStyle>
							<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" BackColor="#57A3D5"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="EmpCode" HeaderText="Empl.Code">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="EmpName" HeaderText="Full name">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="IDNo" HeaderText="ID No">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="DOBStr" HeaderText="DOB">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="StartDateStr" HeaderText="Start Date">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="GenderStr" HeaderText="Gender">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="LevelGradeVN" HeaderText="Level Grade">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="BasicSalary" HeaderText="BasicSalary">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="LocationNameVN" HeaderText="Location">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CompanyVN" HeaderText="Company">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PositionVN" HeaderText="Position">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="LSLevel1Code" HeaderText="Division">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Level2NameVN" HeaderText="Department">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Level3NameVN" HeaderText="Section">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="BankNameVN" HeaderText="Bank">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MaritalNameVN" HeaderText="Marital Status">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ActiveStr" HeaderText="Active">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="LastWorkingDateStr" HeaderText="Last working date">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
					</td>
				</tr>
			</table>
		</td>
	</tr>
</TABLE>
<script language="javascript">
	function CheckBeforeAddCondition()
	{
		if (document.getElementById('_ctl0_cboCriteria').value == "")
		{
			alert("Vui lòng chọn tiêu chí tìm kiếm!");
			document.getElementById('_ctl0_cboCriteria').focus();
			return false;
		}
		if (document.getElementById('_ctl0_cboCondition').value == "")
		{
			alert("Vui lòng chọn giá trị cho tiêu chí tìm kiếm");
			document.getElementById('_ctl0_cboCondition').focus();
			return false;
		}
		return true;
	}
</script>

