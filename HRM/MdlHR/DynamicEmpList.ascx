<%@ Control Language="c#" AutoEventWireup="false" Codebehind="DynamicEmpList.ascx.cs" Inherits="iHRPCore.MdlHR.DynamicEmpList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../Include/EmpHeaderSearch.ascx" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="770" align="center" border="0">
	<TR>
		<TD align="center"><uc1:empheadersearch id="EmpHeaderSearch1" runat="server"></uc1:empheadersearch></TD>
	</TR>
	<TR>
		<TD align="center">
			<HR width="100%" SIZE="1">
		</TD>
	</TR>
	<TR>
		<TD align="center"><asp:label id="lblError" runat="server" CssClass="lblErr"></asp:label></TD>
	</TR>
	<tr>
		<td>
			<table width="770">
				<tr>
					<td style="WIDTH: 136px; HEIGHT: 6px" width="136">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:label id="Label1" Font-Size="8" Height="17px" Font-Name="Arial" Runat="server">Criteria</asp:label></td>
					<td width="250" style="HEIGHT: 6px"><asp:dropdownlist id="cboCriteria" runat="server" Font-Size="8pt" Font-Names="Arial" Width="248px"
							AutoPostBack="True"></asp:dropdownlist></td>
					<td vAlign="middle" align="center" width="120" rowSpan="4"><asp:button id="cmdAdd" runat="server" Height="22px" Width="90px" Font-Bold="True" Text=">>"
							CssClass="btnSave"></asp:button>
						<P></P>
						<P><asp:button id="btRemove" runat="server" Height="22px" Width="90px" Font-Bold="True" Text="<<"
								CssClass="btnSave"></asp:button><BR>
							<BR>
							<asp:button id="btnRemoveAll" Height="22px" Runat="server" Width="90px" Text="Remove All" CssClass="btnSave"></asp:button></P>
					</td>
					<td rowSpan="4"><asp:textbox id="txtFillCondition" style="DISPLAY: none" runat="server" Height="94px" Width="246px"
							TextMode="MultiLine" ReadOnly="True"></asp:textbox><asp:textbox id="txtFillConditionText" runat="server" Height="94px" Width="246px" TextMode="MultiLine"
							ReadOnly="True"></asp:textbox></td>
				</tr>
				<TR>
					<TD style="WIDTH: 136px" width="136">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:label id="Label2" Font-Size="8" Height="17px" Font-Name="Arial" Runat="server">Condition</asp:label></TD>
					<TD width="250"><asp:dropdownlist id="cboCondition" runat="server" Font-Size="8pt" Font-Names="Arial" Width="249"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 136px" width="136">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:label id="Label3" Font-Size="8" Height="17px" Font-Name="Arial" Runat="server">Information</asp:label></TD>
					<TD width="250"><asp:dropdownlist id="cboInfo" Font-Size="8pt" Height="22px" Runat="server" Font-Names="Arial" Width="250"
							Visible="False"></asp:dropdownlist><asp:textbox id="txtDate" onblur="JavaScript:CheckDate1(this)" Font-Size="8pt" Height="21px"
							Runat="server" Font-Names="Arial" Width="250px" Visible="False" MaxLength="10"></asp:textbox><asp:textbox id="txtNumber" onblur="JavaScript:checkNumeric(this)" Font-Size="8pt" Height="21px"
							Runat="server" Font-Names="Arial" Width="250px" Visible="False"></asp:textbox><asp:textbox id="txtInfo" Font-Size="8pt" Height="21px" Runat="server" Font-Names="Arial" Width="250px"
							MaxLength="100"></asp:textbox>
						<div id="divBool" style="DISPLAY: none" runat="server"><asp:radiobutton id="rdTrue" Font-Size="8pt" Height="20px" Runat="server" Font-Names="Arial" Width="71"
								Text="True" GroupName="Bool" Checked="True"></asp:radiobutton><asp:radiobutton id="rdFalse" Font-Size="8pt" Height="20" Runat="server" Font-Names="Arial" Width="71"
								Text="False" GroupName="Bool"></asp:radiobutton></div>
					</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 136px" width="136">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:label id="Label4" Font-Size="8" Height="17px" Font-Name="Arial" Runat="server">Link</asp:label></TD>
					<TD width="250"><asp:radiobutton id="rdAnd" Font-Size="8pt" Height="20px" Runat="server" Font-Names="Arial" Width="71px"
							Text="And" GroupName="Link" Checked="True"></asp:radiobutton><asp:radiobutton id="rdOr" Font-Size="8pt" Height="20px" Runat="server" Font-Names="Arial" Width="71px"
							Text="Or" GroupName="Link"></asp:radiobutton></TD>
				</TR>
				<tr>
					<td style="WIDTH: 136px" align="left" colSpan="1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
						&nbsp;&nbsp;&nbsp;&nbsp;<asp:label id="Label5" Font-Size="8" Height="17px" Font-Name="Arial" Runat="server" Width="104px">Fields View</asp:label>
					</td>
					<td><asp:listbox id="lsbColumnsView" runat="server" Font-Size="8pt" Height="145px" Font-Names="Arial"
							Width="232px" SelectionMode="Multiple"></asp:listbox></td>
					<td vAlign="middle" align="left" colSpan="2">
						<P><asp:button id="cmdList" tabIndex="0" runat="server" Font-Size="8pt" Font-Names="Arial" Width="130px"
								Text="View"></asp:button>&nbsp;&nbsp;&nbsp;
							<asp:button id="cmdExport" tabIndex="0" runat="server" Font-Size="8pt" Font-Names="Arial" Width="130px"
								Text="Export data to Excel"></asp:button></P>
						<P><asp:label id="lblCountEmp" runat="server" CssClass="lblErr"></asp:label><INPUT id="txtColType" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" name="txtColType"
								runat="server"></P>
					</td>
				<tr>
					<td colSpan="4"><asp:datagrid id="dtgEmp" runat="server" CssClass="grid" Width="100%" PageSize="20" AutoGenerateColumns="False"
							CellPadding="0" AllowSorting="True" BorderColor="#3366CC" AllowPaging="True" BackColor="White" BorderWidth="1px">
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
							<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
							<Columns>
								<asp:BoundColumn DataField="EmpCode" HeaderText="EmpCode">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="EmpName" HeaderText="Full name">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="EmpCodeOld" HeaderText="Old Emp code">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="IDNo" HeaderText="ID No">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="NgayCapCMND" HeaderText="ID Issued date">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="DOBStr" HeaderText="DOB">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Born_Province_EN" HeaderText="PlaceOfBirth">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="GenderStr" HeaderText="Gender">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MaritalNameEN" HeaderText="Marital Status">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="StartDateStr" HeaderText="Start Date">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="LocationNameEN" HeaderText="Function">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CompanyShort" HeaderText="Cus. group">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Level1Short" HeaderText="Company">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Level2NameEN" HeaderText="Department">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Level3NameEN" HeaderText="Group">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PositionEN" HeaderText="Job title">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ContractType" HeaderText="Contract type">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ActiveStr" HeaderText="Active">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="LastWorkingDateStr" HeaderText="Last working date">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="SIBookNo" HeaderText="SI Book No">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="NewSIBookNo" HeaderText=" New SI Book No">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
			</table>
		</td>
	</tr>
</TABLE>
<script language="javascript">	
	function CheckBeforeAddCondition()
	{
		/*
		if (document.getElementById('_ctl0_txtDate').value != "")
		{	
			if (CheckDate(document.getElementById('_ctl0_txtDate')) == false)
			{											
				return false;
			}						
		}*/
		if (document.getElementById('_ctl0_cboCriteria').value == "")
		{
			GetAlertError(iTotal,DSAlert,"DEL_0001");
			document.getElementById('_ctl0_cboCriteria').focus();
			return false;
		}		
		if (document.getElementById('_ctl0_cboCondition').value == "")
		{					    
			GetAlertError(iTotal,DSAlert,"DEL_0002");
			document.getElementById('_ctl0_cboCondition').focus();
			return false;
		}
		return true;
	}	
</script>
