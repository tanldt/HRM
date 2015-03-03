<%@ Page language="c#" Codebehind="ResultBusiness.aspx.cs" AutoEventWireup="false" Inherits="Temp.PIT.ResultBusiness" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ResultExpected</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			Màn hình kết quả kinh doanh của nhóm
			<table width="100%">
				<tr>
					<td colspan="2">
						<TABLE id="tblInfo" cellSpacing="0" cellPadding="1" width="98%" border="0">
							<TR>
								<TD>Company</TD>
								<TD>FPT</TD>
								<TD></TD>
								<td>Group Bussiness</td>
								<td>Phần mềm NS-Tiền lương</td>
							</TR>
							<TR>
								<TD><asp:label id="Label1" runat="server" CssClass="labelRequire" Width="100%">Year</asp:label></TD>
								<TD>
									<asp:DropDownList id="DropDownList1" runat="server">
										<asp:ListItem Value="2008">2008</asp:ListItem>
										<asp:ListItem Value="2009">2009</asp:ListItem>
										<asp:ListItem Value="2010">2010</asp:ListItem>
									</asp:DropDownList></TD>
							</TR>
							<TR>
								<TD><asp:label id="lblLastName" runat="server" CssClass="labelRequire" Width="100%">Month/Year</asp:label></TD>
								<TD><asp:textbox id="txtLastName" runat="server" CssClass="input" Width="100%" MaxLength="35"></asp:textbox></TD>
								<TD></TD>
								<TD><asp:label id="lblFirstName" runat="server" CssClass="labelRequire" Width="100%">Note<br>
(Total turnover derived in period)</asp:label></TD>
								<TD><asp:textbox id="txtFirstName" runat="server" CssClass="input" Width="100%" MaxLength="15"></asp:textbox></TD>
							</TR>
							
							<TR>
								<TD colspan=5>
									<table width=100%>
										<tr>
										<td>STT<br>
											(No.)</td>
										<td colspan=7>Chỉ tiêu<br>
											(Items)</td>
										<td colspan=4 >Số lượng/Số tiền<br>
											(Quantity/Amount)</td>
										</tr>
										<tr height=37>
										<td height=37>1</td>
										<td colspan=5 class=xl69 width=570 style='border-right:.5pt solid black;
										border-left:none;width:428pt'>Doanh thu bán hàng hóa và cung cấp dịch vụ
										<br>
											<font class="font7">( Turnover from selling goods and supplying services)</td>
										<td colspan=2 class=xl39 style='border-right:.5pt solid black;border-left:
										none'>[08]</td>
										<td colspan=4 class=xl58 style='border-right:1.0pt solid black;border-left:
										none'>&nbsp;</td>
										</tr>
										<tr height=38 style='mso-height-source:userset;height:28.5pt'>
										<td rowspan=5 height=233 class=xl53 style='border-bottom:.5pt solid black;
										height:174.75pt;border-top:none' x:num>2</td>
										<td class=xl29 style='border-top:none'>&nbsp;</td>
										<td colspan=4 class=xl45 width=530 style='width:398pt'>Các khoản giảm trừ
										doanh thu<br>
											<font class="font7">(Deductions of turnover)</td>
										<td colspan=2 class=xl41 style='border-right:.5pt solid black'>[09]</td>
										<td colspan=4 class=xl33 style='border-right:1.0pt solid black;border-left:
										none'>&nbsp;</td>
										</tr>
										<tr height=38 style='mso-height-source:userset;height:28.5pt'>
										<td height=38 class=xl30 style='height:28.5pt'>a</td>
										<td colspan=4 class=xl51 width=530 style='width:398pt'>Chiết khấu thương
										mại<br>
											<font class="font7">(commercial discount)</td>
										<td colspan=2 class=xl41 style='border-right:.5pt solid black'>[10]</td>
										<td colspan=4 class=xl33 style='border-right:1.0pt solid black;border-left:
										none'>&nbsp;</td>
										</tr>
										<tr height=39 style='mso-height-source:userset;height:29.25pt'>
										<td height=39 class=xl30 style='height:29.25pt'>b</td>
										<td colspan=4 class=xl45 width=530 style='width:398pt'>Giảm giá hàng
										bán<br>
											<font class="font7">(Bussiness discount)</td>
										<td colspan=2 class=xl41 style='border-right:.5pt solid black'>[11]</td>
										<td colspan=4 class=xl33 style='border-right:1.0pt solid black;border-left:
										none'>&nbsp;</td>
										</tr>
										<tr height=37>
										<td height=37 class=xl30 style='height:27.75pt'>c</td>
										<td colspan=4 class=xl45 width=530 style='width:398pt'>Giá trị hàng bán bị
										trả lại<br>
											<font class="font7">(Sale returns)</td>
										<td colspan=2 class=xl41 style='border-right:.5pt solid black'>[12]</td>
										<td colspan=4 class=xl33 style='border-right:1.0pt solid black;border-left:
										none'>&nbsp;</td>
										</tr>
										<tr height=81 style='mso-height-source:userset;height:60.75pt'>
										<td height=81 class=xl28 style='height:60.75pt'>d</td>
										<td colspan=4 class=xl45 width=530 style='width:398pt'>Thuế tiêu thụ đặc
										biệt, thuế xuất khẩu, thuế giá trị gia tăng theo phương pháp trực tiếp phải
										nộp<br>
											<font class="font7">(Special sales tax, Import-Export tax, Value Added Tax
										with direct method)<span style='mso-spacerun:yes'> </span></td>
										<td colspan=2 class=xl41 style='border-right:.5pt solid black'>[13]</td>
										<td colspan=4 class=xl33 style='border-right:1.0pt solid black;border-left:
										none'>&nbsp;</td>
										</tr>
										<tr height=39 style='mso-height-source:userset;height:29.25pt'>
										<td height=39 class=xl25 style='height:29.25pt;border-top:none' x:num>3</td>
										<td colspan=5 class=xl45 width=570 style='border-right:.5pt solid black;
										border-left:none;width:428pt'>Doanh thu hoạt động tài chính<br>
											<font class="font7">(Income from financial activities)</td>
										<td colspan=2 class=xl41 style='border-right:.5pt solid black;border-left:
										none'>[14]</td>
										<td colspan=4 class=xl33 style='border-right:1.0pt solid black;border-left:
										none'>&nbsp;</td>
										</tr>
										<tr height=38 style='mso-height-source:userset;height:28.5pt'>
										<td rowspan=4 height=152 class=xl53 style='border-bottom:.5pt solid black;
										height:114.0pt;border-top:none' x:num>4</td>
										<td class=xl31 style='border-top:none'>&nbsp;</td>
										<td colspan=4 class=xl51 width=530 style='width:398pt'>Chi phí sản xuất, kinh
										doanh hàng hóa, dịch vụ<br>
											<font class="font7">(Expenses from manufacturing or supplying services)</td>
										<td colspan=2 class=xl41 style='border-right:.5pt solid black'>[15]</td>
										<td colspan=4 class=xl33 style='border-right:1.0pt solid black;border-left:
										none'>&nbsp;</td>
										</tr>
										<tr>
										<td height=36 class=xl31 style='height:27.0pt;border-top:none'>a</td>
										<td colspan=4 class=xl45 width=530 style='width:398pt'>Giá vốn hàng bán<br>
											<font class="font7">(Cost of goods sold)</td>
										<td colspan=2 class=xl41 style='border-right:.5pt solid black'>[16]</td>
										<td colspan=4 class=xl33 style='border-right:1.0pt solid black;border-left:
										none'>&nbsp;</td>
										</tr>
										<tr height=38 style='mso-height-source:userset;height:28.5pt'>
										<td height=38 class=xl31 style='height:28.5pt;border-top:none'>b</td>
										<td colspan=4 class=xl45 width=530 style='width:398pt'>Chi phí bán hàng<br>
											<font class="font7">( Selling expense)</td>
										<td colspan=2 class=xl41 style='border-right:.5pt solid black'>[17]</td>
										<td colspan=4 class=xl33 style='border-right:1.0pt solid black;border-left:
										none'>&nbsp;</td>
										</tr>
										<tr height=40 style='mso-height-source:userset;height:30.0pt'>
										<td height=40 class=xl31 style='height:30.0pt;border-top:none'>c</td>
										<td colspan=4 class=xl45 width=530 style='width:398pt'>Chi phí quản lý<br>
											<font class="font7">(General and administration expenses)</td>
										<td colspan=2 class=xl41 style='border-right:.5pt solid black'>[18]</td>
										<td colspan=4 class=xl33 style='border-right:1.0pt solid black;border-left:
										none'>&nbsp;</td>
										</tr>
										<tr>
										<td rowspan=2 height=74 class=xl53 style='border-bottom:.5pt solid black;
										height:55.5pt;border-top:none' x:num>5</td>
										<td colspan=5 class=xl45 width=570 style='border-right:.5pt solid black;
										border-left:none;width:428pt'>Chi phí tài chính<br>
											<font class="font7">(Expenses fro financial activities)</td>
										<td colspan=2 class=xl41 style='border-right:.5pt solid black;border-left:
										none'>[19]</td>
										<td colspan=4 class=xl33 style='border-right:1.0pt solid black;border-left:
										none'>&nbsp;</td>
										</tr>
										<tr height=38 style='mso-height-source:userset;height:28.5pt'>
										<td colspan=5 height=38 class=xl55 width=570 style='border-right:.5pt solid black;
										height:28.5pt;border-left:none;width:428pt'>Trong đó: Chi phí lãi tiền vay
										dùng cho sản xuất kinh doanh<font class="font5"><br>
											<font class="font7">(of which:<span style='mso-spacerun:yes'> 
										</span>interest expense for manufacturing)</td>
										<td colspan=2 class=xl41 style='border-right:.5pt solid black;border-left:
										none'>[20]</td>
										<td colspan=4 class=xl33 style='border-right:1.0pt solid black;border-left:
										none'>&nbsp;</td>
										</tr>
										<tr height=38 style='mso-height-source:userset;height:28.5pt'>
										<td height=38 class=xl25 style='height:28.5pt;border-top:none' x:num>6</td>
										<td colspan=5 class=xl45 width=570 style='border-right:.5pt solid black;
										border-left:none;width:428pt'>Thu nhập chịu thuế từ hoạt động kinh
										doanh<br>
											<font class="font7">( Taxable income from business operating)</td>
										<td colspan=2 class=xl41 style='border-right:.5pt solid black;border-left:
										none'>[21]</td>
										<td colspan=4 class=xl33 style='border-right:1.0pt solid black;border-left:
										none'>&nbsp;</td>
										</tr>
										<tr height=39 style='mso-height-source:userset;height:29.25pt'>
										<td height=39 class=xl25 style='height:29.25pt;border-top:none' x:num>7</td>
										<td colspan=5 class=xl45 width=570 style='border-right:.5pt solid black;
										border-left:none;width:428pt'>Thu nhập khác<br>
											<font class="font7">( Other income)</td>
										<td colspan=2 class=xl41 style='border-right:.5pt solid black;border-left:
										none'>[22]</td>
										<td colspan=4 class=xl33 style='border-right:1.0pt solid black;border-left:
										none'>&nbsp;</td>
										</tr>
										<tr>
										<td height=36 class=xl25 style='height:27.0pt;border-top:none' x:num>8</td>
										<td colspan=5 class=xl45 width=570 style='border-right:.5pt solid black;
										border-left:none;width:428pt'>Chi phí khác<br>
											<font class="font7">(Other expense)</td>
										<td colspan=2 class=xl41 style='border-right:.5pt solid black;border-left:
										none'>[23]</td>
										<td colspan=4 class=xl33 style='border-right:1.0pt solid black;border-left:
										none'>&nbsp;</td>
										</tr>
										<tr height=39 style='mso-height-source:userset;height:29.25pt'>
										<td height=39 class=xl25 style='height:29.25pt;border-top:none' x:num>9</td>
										<td colspan=5 class=xl45 width=570 style='border-right:.5pt solid black;
										border-left:none;width:428pt'>Thu nhập chịu thuế khác<br>
											<font class="font7">( Other taxable income)</td>
										<td colspan=2 class=xl41 style='border-right:.5pt solid black;border-left:
										none'>[24]</td>
										<td colspan=4 class=xl33 style='border-right:1.0pt solid black;border-left:
										none'>&nbsp;</td>
										</tr>
										<tr height=40 style='mso-height-source:userset;height:30.0pt'>
										<td height=40 class=xl25 style='height:30.0pt;border-top:none' x:num>10</td>
										<td colspan=5 class=xl45 width=570 style='border-right:.5pt solid black;
										border-left:none;width:428pt'>Thu nhập chịu thuế ngoài Việt Nam<font
										class="font5"> <br>
											<font class="font7">( Outside-Vietnam Taxable Income)</td>
										<td colspan=2 class=xl41 style='border-right:.5pt solid black;border-left:
										none'>[25]</td>
										<td colspan=4 class=xl33 style='border-right:1.0pt solid black;border-left:
										none'>&nbsp;</td>
										</tr>
										<tr>
										<td height=36 class=xl27 style='height:27.0pt;border-top:none' x:num>11</td>
										<td colspan=5 class=xl48 width=570 style='border-right:.5pt solid black;
										border-left:none;width:428pt'>Tổng thu nhập chịu thuế phát sinh trong
										kỳ<br>
											<font class="font5"><span style='mso-spacerun:yes'> </span><font
										class="font7">( Total taxable income in period)</td>
										<td colspan=2 class=xl43 style='border-right:.5pt solid black;border-left:
										none'>[26]</td>
										<td colspan=4 class=xl36 style='border-right:1.0pt solid black;border-left:
										none'>&nbsp;</td>
										</tr>

									</table>
									
								</TD>
							</TR>
						</TABLE>
					</td>
				</tr>
				<tr>
					<td colspan="2" align="center">
						<asp:Button id="Button1" runat="server" Text="Add new"></asp:Button>&nbsp;
						<asp:Button id="Button2" runat="server" Text="Search"></asp:Button>&nbsp;
						<asp:Button id="Button3" runat="server" Text="Save"></asp:Button>&nbsp;
						<asp:Button id="Button4" runat="server" Text="Delete"></asp:Button>
					</td>
				</tr>
				<TR>
					<TD align="center" colSpan="2">
						<table width="100%" border="1">
							<tr bgcolor="silver">
								<td>Seq</td>
								<td>Year</td>
								<td>Month</td>
								<td>Total Income</td>
								<td>Total expenses incured</td>
								<td>Edit</td>
								<td>delete</td>
								<td>Select</td>
							</tr>
							<tr>
								<td>1</td>
								<td>2008</td>
								<td>01</td>
								<td>2,000,000,000</td>
								<td>500,000,000</td>
								<td><a href="#"> Edit</a></td>
								<td><a href="#"> Delete</a></td>
								<td><input type="checkbox">
								</td>
							</tr>
							<tr>
								<td>2</td>
								<td>2008</td>
								<td>02</td>
								<td>2,000,000,000</td>
								<td>500,000,000</td>
								<td><a href="#"> Edit</a></td>
								<td><a href="#"> Delete</a></td>
								<td><input type="checkbox">
								</td>
							</tr>
						</table>
					</TD>
				</TR>
				<TR>
					<TD align="center" colSpan="2"></TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>
