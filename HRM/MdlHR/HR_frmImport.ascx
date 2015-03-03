<%@ Page language="c#" Codebehind="HR_frmImport.aspx.cs" AutoEventWireup="false" Inherits="iHRPCore.Webforms.HR.HR_frmImport" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Import</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="../Include/myStyles.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		function Check_ValidInputData()
		{
			if (document.getElementById("txtFile").value == "")
			{
				alert("Please select file to import");
				document.Form1("txtFile").focus();
				return false;
			}	
			return true;
		}		
		</script>
		<script language="javascript" src="../../Includes/MyLibrary.js"></script>
		<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="770" align="center" border="0">
				<TR>
					<TD align="left" colSpan="2"></TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" border="0">
							<TR>
								<TD align="center" height="15">
									<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<TR>
											<TD style="HEIGHT: 21px" align="center" height="21"><asp:label id="lblTitle" runat="server" Font-Names="Arial" Font-Size="12pt" Font-Bold="True">IMPORT</asp:label></TD>
										</TR>
										<TR>
											<TD style="HEIGHT: 17px" align="center" height="17">
												<P><asp:label id="lblError" runat="server" Font-Names="Arial" Font-Bold="True" Font-size="8pt"
														ForeColor="Red"></asp:label></P>
											</TD>
										</TR>
										<TR>
										<TR>
											<TD align="center">
												<TABLE id="Table3" width="600">
													<TR>
														<TD height="27"></TD>
														<TD height="27"></TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
										<TR>
											<TD align="center" colSpan="1" height="20" rowSpan="1"><asp:button id="cmdAddNew" runat="server" Font-Names="Arial" Font-Size="8pt" CssClass="buttoncommand"
													Text="Load Grid"></asp:button>&nbsp;&nbsp;&nbsp;
												<asp:button id="cmdSave" runat="server" Font-Names="Arial" Font-Size="8pt" CssClass="buttoncommand"
													Text="Lưu"></asp:button></TD>
										</TR>
										<TR>
											<TD vAlign="bottom" align="center" colSpan="1" height="27" rowSpan="1">
												<P><asp:label id="lblRowError" runat="server" Font-Names="Arial" Font-Bold="True" Font-size="8pt"
														ForeColor="Red"></asp:label></P>
											</TD>
										</TR>
										<TR>
											<TD align="center"></TD>
										</TR>
										<TR>
											<TD align="center" height="20"><asp:datagrid id="grdList" runat="server" CssClass="grid" AllowSorting="True" AutoGenerateColumns="False"
													CellPadding="0" BorderWidth="1px" BorderColor="#3366CC" BackColor="White" PageSize="20" Width="100%">
													<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
													<ItemStyle CssClass="gridItem"></ItemStyle>
													<HeaderStyle CssClass="gridHeader"></HeaderStyle>
													<FooterStyle CssClass="gridFooter"></FooterStyle>
													<Columns>
														<asp:BoundColumn DataField="EmpID" HeaderText="EmpID">
															<ItemStyle HorizontalAlign="Center"></ItemStyle>
														</asp:BoundColumn>
														<asp:BoundColumn DataField="FullName" HeaderText="FullName">
															<ItemStyle HorizontalAlign="Center"></ItemStyle>
														</asp:BoundColumn>
														<asp:BoundColumn DataField="Gender" HeaderText="Gender">
															<ItemStyle HorizontalAlign="Center"></ItemStyle>
														</asp:BoundColumn>
														<asp:BoundColumn DataField="DOB" HeaderText="DOB">
															<ItemStyle HorizontalAlign="Center"></ItemStyle>
														</asp:BoundColumn>
														<asp:BoundColumn DataField="MACV" HeaderText="MACV">
															<ItemStyle HorizontalAlign="Center"></ItemStyle>
														</asp:BoundColumn>
														<asp:BoundColumn DataField="MADV" HeaderText="MADV">
															<ItemStyle HorizontalAlign="Center"></ItemStyle>
														</asp:BoundColumn>
														<asp:BoundColumn DataField="MABP" HeaderText="MABP">
															<ItemStyle HorizontalAlign="Center"></ItemStyle>
														</asp:BoundColumn>
														<asp:BoundColumn DataField="NoiSinh" HeaderText="NoiSinh">
															<ItemStyle HorizontalAlign="Center"></ItemStyle>
														</asp:BoundColumn>
														<asp:BoundColumn DataField="Dienthoai" HeaderText="Dienthoai">
															<ItemStyle HorizontalAlign="Center"></ItemStyle>
														</asp:BoundColumn>
														<asp:BoundColumn DataField="DCThuongTru" HeaderText="DCThuongTru">
															<ItemStyle HorizontalAlign="Center"></ItemStyle>
														</asp:BoundColumn>
														<asp:BoundColumn DataField="DCLienLac" HeaderText="DCLienLac">
															<ItemStyle HorizontalAlign="Center"></ItemStyle>
														</asp:BoundColumn>
														<asp:BoundColumn DataField="TDVanhoa" HeaderText="TDVanhoa">
															<ItemStyle HorizontalAlign="Center"></ItemStyle>
														</asp:BoundColumn>
														<asp:BoundColumn DataField="DCLL" HeaderText="DCLL">
															<ItemStyle HorizontalAlign="Center"></ItemStyle>
														</asp:BoundColumn>
														<asp:BoundColumn DataField="LH" HeaderText="LH">
															<ItemStyle HorizontalAlign="Center"></ItemStyle>
														</asp:BoundColumn>
														<asp:BoundColumn DataField="LLNV" HeaderText="LLNV">
															<ItemStyle HorizontalAlign="Center"></ItemStyle>
														</asp:BoundColumn>
														<asp:BoundColumn DataField="LLTP" HeaderText="LLTP">
															<ItemStyle HorizontalAlign="Center"></ItemStyle>
														</asp:BoundColumn>
														<asp:BoundColumn DataField="CKBMTT" HeaderText="CKBMTT">
															<ItemStyle HorizontalAlign="Center"></ItemStyle>
														</asp:BoundColumn>
														<asp:TemplateColumn HeaderText="Chọn">
															<ItemStyle HorizontalAlign="Center"></ItemStyle>
															<ItemTemplate>
																<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
															</ItemTemplate>
														</asp:TemplateColumn>
													</Columns>
													<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
												</asp:datagrid></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD colSpan="2"></TD>
				</TR>
			</TABLE>
			&nbsp;
		</form>
		<SCRIPT language="javascript" src="../../Includes/Confirm_Delete.js"></SCRIPT>
		<SCRIPT language="javascript" src="../../Includes/CheckDate.js"></SCRIPT>
	</body>
</HTML>
