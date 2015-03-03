<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Personal.ascx.cs" Inherits="iHRPCore.MdlHR.Personal" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="DateCOMM" Src="../Component/DateCOMM/DateCOMM.ascx" %>
<%@ Register TagPrefix="uc" TagName="EmpHeader" Src="../Include/EmpHeader.ascx" %>
<script language="javascript" src='../Include/common.js"'></script>
&nbsp;
<TABLE width="100%">
	<TR>
		<TD>
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="97%" border="0">
				<TBODY>
					<TR>
						<TD colSpan="5"><asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label></TD>
					</TR>
					<TR>
						<TD align="center" colSpan="5"><uc:empheader id="HR_EmpHeader" runat="server"></uc:empheader></TD>
					</TR>
					<TR>
						<TD><asp:label id="lblLastName" CssClass="labelRequire" runat="server">Last Name</asp:label></TD>
						<TD><asp:textbox id="txtVLastName" CssClass="input" runat="server" Width="100%" MaxLength="30"></asp:textbox></TD>
						<TD></TD>
						<TD rowSpan="4"><asp:image id="imgEmp" Width="70px" Runat="server" Height="80px"></asp:image></TD>
						<TD rowSpan="4"><INPUT class="input" id="fileUpload" style="WIDTH: 100%" onpropertychange="ShowImg()" tabIndex="1"
								type="file" size="13" name="File1" runat="server"><BR>
							<asp:button id="btnUpload" tabIndex="1" CssClass="buttoncommand" runat="server" Width="61px"
								Font-Size="8pt" Font-Names="Arial" Text="Upload"></asp:button><asp:label id="Label1" Width="73" Runat="server" Font-Size="8pt" Font-Names="Arial">(Size <                                                                                                                                                                                                                                                                                                  4 MB)</asp:label><BR>
							<asp:label id="lblError" runat="server" Font-Size="8pt" Font-Names="Arial" ForeColor="Red"
								Font-Bold="True"></asp:label></TD>
					</TR>
					<TR>
						<TD><asp:label id="lblFirstName" CssClass="labelRequire" runat="server" Width="100%">First Name</asp:label></TD>
						<TD><asp:textbox id="txtVFirstName" CssClass="input" runat="server" Width="100%" MaxLength="20"></asp:textbox></TD>
						<TD></TD>
						<TD></TD>
						<TD></TD>
					</TR>
					<TR>
						<TD><asp:label id="Label24" CssClass="label" runat="server" Width="100%">Last Name EN</asp:label></TD>
						<TD><asp:textbox id="txtELastName" CssClass="input" runat="server" Width="100%" MaxLength="20"></asp:textbox></TD>
						<TD></TD>
						<TD></TD>
						<TD></TD>
					</TR>
					<TR>
						<TD><asp:label id="Label25" CssClass="label" runat="server" Width="100%">First Name EN</asp:label></TD>
						<TD><asp:textbox id="txtEFirstName" CssClass="input" runat="server" Width="100%" MaxLength="20"></asp:textbox></TD>
						<TD></TD>
						<TD></TD>
						<TD></TD>
					</TR>
					<TR>
						<TD><asp:label id="lblNickName" CssClass="label" runat="server" Width="100%">Nick name</asp:label></TD>
						<TD><asp:textbox id="txtNickName" CssClass="input" runat="server" Width="100%" MaxLength="20"></asp:textbox></TD>
						<TD></TD>
						<TD>
							<asp:label id="Label32" runat="server" CssClass="label" Width="100%">Emp Code 2</asp:label></TD>
						<TD>
							<asp:textbox id="txtEmpCode2" runat="server" CssClass="input" MaxLength="15" Width="100%"></asp:textbox></TD>
					</TR>
					<TR>
						<TD><asp:label id="lblDOB" CssClass="label" runat="server" Width="100%">DOB</asp:label></TD>
						<TD><asp:textbox id="txtDOB" onblur="JavaScript:getAge(this)" CssClass="input" runat="server" Width="88px"
								MaxLength="12"></asp:textbox>&nbsp;<INPUT class=btnCal onclick="javascript:popUpCalendar(<%=  this.txtDOB.ClientID %>)" type=button>&nbsp;&nbsp;&nbsp;
							<asp:label id="lblAge" CssClass="label" runat="server">Age</asp:label>&nbsp;
							<asp:textbox id="txtAge" CssClass="input" runat="server" Width="45px" ReadOnly="True"></asp:textbox></TD>
						<TD></TD>
						<TD></TD>
						<TD></TD>
					</TR>
					<TR>
						<TD width="15%"><asp:label id="lblGender" CssClass="label" runat="server" Width="100%">Gender</asp:label></TD>
						<TD width="32%"><asp:dropdownlist id="cboGender" CssClass="combo" runat="server" Width="75px"></asp:dropdownlist></TD>
						<TD width="4%"></TD>
					</TR>
					<TR>
						<TD width="15%"><asp:label id="Label26" CssClass="labelRequire" runat="server" Width="100%">Joining Date</asp:label></TD>
						<TD width="32%"><asp:textbox id="txtStartDate" onblur="JavaScript:CheckDate(this)" runat="server" CssClass="input"
								Width="50%" MaxLength="10"></asp:textbox>&nbsp;<INPUT class=btnCal onclick="javascript:popUpCalendar(<%= this.txtStartDate.ClientID %>)" type=button></TD>
						<TD width="4%"></TD>
					</TR>
					<TR>
						<TD><asp:label id="lblBorn_ProvinceID" CssClass="label" runat="server" Width="100%">Place of birth</asp:label></TD>
						<TD align="center" colSpan="4">
							<TABLE width="100%">
								<TR>
									<TD width="2.5%"><asp:checkbox id="chkAdBorn_District" CssClass="chkAdmin" runat="server" Checked="True"></asp:checkbox></TD>
									<TD id="tdAdBorn_Districtlbl" width="10%" runat="server"><asp:label id="lblDistrict" CssClass="label" Runat="server">District</asp:label></TD>
									<TD id="tdAdBorn_Districtcbo" width="15%" runat="server"><asp:textbox id="txtBorn_District" CssClass="input" Width="100%" Runat="server"></asp:textbox></TD>
									<TD width="2.5%"><asp:checkbox id="chkAdBorn_LSProvinceID" CssClass="chkAdmin" runat="server" Checked="True"></asp:checkbox></TD>
									<TD id="tdAdBorn_LSProvinceIDlbl" width="15%" runat="server"><asp:label id="lblCityProvince" CssClass="label" Runat="server">City/Province</asp:label></TD>
									<TD id="tdAdBorn_LSProvinceIDcbo" width="15%" colSpan="3" runat="server"><asp:dropdownlist id="cboBorn_LSProvinceID" CssClass="combo" runat="server" Width="100%"></asp:dropdownlist></TD>
									<TD width="2.5%"></TD>
									<TD width="15%"></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD><asp:label id="lblNative_ProvinceID" CssClass="label" runat="server" Width="100%">Native Place</asp:label></TD>
						<TD colSpan="4">
							<TABLE width="100%">
								<TR>
									<TD width="2.5%"><asp:checkbox id="chkAdNative_District" CssClass="chkAdmin" runat="server" Checked="True"></asp:checkbox></TD>
									<TD id="tdAdNative_Districtlbl" width="10%" runat="server"><asp:label id="lblDistrict2" CssClass="label" Runat="server">District</asp:label></TD>
									<TD id="tdAdNative_Districttxt" width="15%" runat="server"><asp:textbox id="txtNative_District" CssClass="input" Runat="server" width="100%"></asp:textbox></TD>
									<TD width="2.5%"><asp:checkbox id="chkAdNative_LSProvinceID" CssClass="chkAdmin" runat="server" Checked="True"></asp:checkbox></TD>
									<TD id="tdAdNative_LSProvinceIDlbl" width="15%" runat="server"><asp:label id="lblCityProvince2" CssClass="label" Runat="server">City/Province</asp:label></TD>
									<TD id="tdAdNative_LSProvinceIDcbo" width="15%" colSpan="3" runat="server"><asp:dropdownlist id="cboNative_LSProvinceID" CssClass="combo" runat="server" Width="100%"></asp:dropdownlist></TD>
									<TD width="2.5%"></TD>
									<TD width="15%"></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR id="trAdAddress" runat="server">
						<TD><asp:checkbox id="ChkAdAddress" CssClass="chkAdmin" Runat="server"></asp:checkbox></TD>
						<TD><asp:label id="Label7" CssClass="label" runat="server" Width="100%"> Address</asp:label></TD>
						<TD colSpan="2"><asp:checkbox id="chkAdDistrinct" CssClass="chkAdmin" Runat="server"></asp:checkbox><asp:label id="lblT_Distrinct" CssClass="label" runat="server" Width="70%"> District</asp:label></TD>
						<TD><asp:checkbox id="chkAdProvince" CssClass="chkAdmin" Width="10%" Runat="server"></asp:checkbox><asp:label id="lblT_Province" CssClass="label" runat="server" Width="80%">Province</asp:label></TD>
					</TR>
					<TR id="trAdPermanent" runat="server">
						<TD><asp:checkbox id="chkAdPermanent" CssClass="chkAdmin" Runat="server"></asp:checkbox><asp:label id="lblPermanentAddress" CssClass="label" runat="server" Width="60%"> Permanent</asp:label></TD>
						<TD vAlign="top"><asp:textbox id="txtP_Address" style="POSITION: absolute" CssClass="input" runat="server" Width="98%"
								MaxLength="100"></asp:textbox></TD>
						<TD id="tdAdDistrincttxt" colSpan="2" runat="server"><asp:textbox id="txtP_District" CssClass="input" runat="server" Width="98%" MaxLength="15"></asp:textbox></TD>
						<TD id="tdAdProvincelbl" runat="server"><asp:dropdownlist id="cboP_LSProvinceID" CssClass="combo" runat="server" Width="100%"></asp:dropdownlist></TD>
					</TR>
					<TR id="trAdTemporary" runat="server">
						<TD><asp:checkbox id="chkAdTemporary" CssClass="chkAdmin" Runat="server"></asp:checkbox><asp:label id="lblTemporaryAddress" CssClass="label" runat="server" Width="60%"> Current</asp:label></TD>
						<TD vAlign="top"><asp:textbox id="txtT_Address" style="POSITION: absolute" CssClass="input" runat="server" Width="98%"
								MaxLength="100"></asp:textbox></TD>
						<TD id="tdAdDistrinctcbo" colSpan="2" runat="server"><asp:textbox id="txtT_District" CssClass="input" runat="server" Width="98%" MaxLength="15"></asp:textbox></TD>
						<TD id="tdAdProvincetxt" runat="server"><asp:dropdownlist id="cboT_LSProvinceID" CssClass="combo" runat="server" Width="100%"></asp:dropdownlist></TD>
					</TR>
					<TR>
						<TD><asp:label id="lblMaritalID" CssClass="label" runat="server" Width="100%">Marital Status</asp:label></TD>
						<TD><asp:dropdownlist id="cboLSMaritalID" CssClass="combo" runat="server" Width="100%"></asp:dropdownlist></TD>
						<TD></TD>
						<TD><asp:label id="lblNationID" CssClass="label" runat="server" Width="100%">Nationality</asp:label></TD>
						<TD><asp:dropdownlist id="cboLSNationalityID" CssClass="combo" runat="server" Width="100%"></asp:dropdownlist></TD>
					</TR>
					<TR>
						<TD><asp:label id="lblEthnicID" CssClass="label" runat="server" Width="100%">Ethnic</asp:label></TD>
						<TD><asp:dropdownlist id="cboLSEthnicID" CssClass="combo" runat="server" Width="100%"></asp:dropdownlist></TD>
						<TD></TD>
						<TD><asp:label id="lblReligionID" CssClass="label" runat="server" Width="100%">Religion</asp:label></TD>
						<TD><asp:dropdownlist id="cboLSReligionID" CssClass="combo" runat="server" Width="100%"></asp:dropdownlist></TD>
					</TR>
					<TR style="DISPLAY:none">
						<TD><asp:label id="lblSocialClass" CssClass="label" runat="server" Width="100%">Family Background</asp:label></TD>
						<TD><asp:dropdownlist id="cboLSFamilyBackgroundID" CssClass="combo" runat="server" Width="100%"></asp:dropdownlist></TD>
						<TD></TD>
						<TD><asp:label id="lblPersonalElement" CssClass="label" runat="server" Width="100%">Personal Background</asp:label></TD>
						<TD><asp:dropdownlist id="cboLSPersonalBackgroundID" CssClass="combo" runat="server" Width="100%"></asp:dropdownlist></TD>
					</TR>
					<TR>
						<TD colSpan="5">
							<HR width="97%">
						</TD>
					</TR>
					<TR>
						<TD><asp:label id="lblIDCardNo" CssClass="label" runat="server" Width="100%">ID Card No</asp:label></TD>
						<TD><asp:textbox id="txtIDNo" CssClass="input" runat="server" Width="100%" MaxLength="9"></asp:textbox></TD>
						<TD></TD>
						<TD><asp:label id="lblIssuedPlace" CssClass="label" runat="server" Width="100%">Place of Issue</asp:label></TD>
						<TD><asp:dropdownlist id="cboIDIssuedPlace" CssClass="combo" runat="server" Width="100%"></asp:dropdownlist></TD>
					</TR>
					<TR>
						<TD><asp:label id="lblIssuedDate" CssClass="label" runat="server" Width="100%">Issue Date</asp:label></TD>
						<TD><asp:textbox id="txtIDIssuedDate" onblur="JavaScript:CheckDate(this)" CssClass="input" runat="server"
								Width="75px"></asp:textbox>&nbsp;<INPUT class=btnCal onclick="javascript:popUpCalendar(<%= this.txtIDIssuedDate.ClientID %>)" type=button></TD>
						<TD></TD>
						<TD></TD>
						<TD></TD>
					</TR>
					<TR>
						<TD colSpan="5">
							<HR width="97%">
						</TD>
					</TR>
					<TR>
						<TD><asp:label id="Label15" CssClass="label" runat="server" Width="100%">Passport No</asp:label></TD>
						<TD><asp:textbox id="txtPass_Number" CssClass="input" runat="server" Width="100%" MaxLength="50"></asp:textbox></TD>
						<TD></TD>
						<TD><asp:label id="Label16" CssClass="label" runat="server" Width="100%">Place of Issue</asp:label></TD>
						<TD><asp:dropdownlist id="cboPass_IssuedPlace" CssClass="combo" runat="server" Width="100%"></asp:dropdownlist></TD>
					</TR>
					<TR>
						<TD><asp:label id="Label17" CssClass="label" runat="server" Width="100%">Issue Date</asp:label></TD>
						<TD><asp:textbox id="txtPass_IssuedDate" onblur="JavaScript:CheckDate(this)" CssClass="input" runat="server"
								Width="75px"></asp:textbox>&nbsp;<INPUT class=btnCal onclick="javascript:popUpCalendar(<%= this.txtPass_IssuedDate.ClientID %>)" type=button></TD>
						<TD></TD>
						<TD><asp:label id="Label18" CssClass="label" runat="server" Width="100%">Expire Date</asp:label></TD>
						<TD><asp:textbox id="txtPass_ExpiredDate" onblur="JavaScript:CheckDate(this)" CssClass="input" runat="server"
								Width="75px"></asp:textbox>&nbsp;<INPUT class=btnCal onclick="javascript:popUpCalendar(<%= this.txtPass_ExpiredDate.ClientID %>)" type=button></TD>
					</TR>
					<TR>
						<TD colSpan="5">
							<HR width="97%">
						</TD>
					</TR>
					<TR>
						<TD>
							<asp:label id="Label19" runat="server" CssClass="label" Width="100%">Emergency contact</asp:label></TD>
						<TD>
							<asp:textbox id="txtEmergencyContact" runat="server" CssClass="input" Width="100%" MaxLength="100"></asp:textbox></TD>
						<TD></TD>
						<TD><asp:label id="Label20" CssClass="label" runat="server" Width="100%">Blood Type</asp:label></TD>
						<TD><asp:dropdownlist id="cboLSBloodTypeID" CssClass="combo" runat="server" Width="100%">
								<asp:ListItem Value=""></asp:ListItem>
								<asp:ListItem Value="O">O</asp:ListItem>
								<asp:ListItem Value="A">A</asp:ListItem>
								<asp:ListItem Value="B">B</asp:ListItem>
								<asp:ListItem Value="AB">AB</asp:ListItem>
							</asp:dropdownlist></TD>
					</TR>
					<TR>
						<TD>
							<asp:label id="Label21" runat="server" CssClass="label" Width="100%">Mentor</asp:label></TD>
						<TD>
							<asp:textbox id="txtMentor" runat="server" CssClass="input" Width="100%" MaxLength="100"></asp:textbox></TD>
						<TD></TD>
						<TD><asp:label id="Label22" CssClass="label" runat="server" Width="100%">Performance Manager</asp:label></TD>
						<TD><asp:textbox id="txtPerformanceManager" CssClass="input" runat="server" Width="100%" MaxLength="100"></asp:textbox></TD>
					</TR>
					<TR>
						<TD>
							<asp:label id="Label23" runat="server" CssClass="label" Width="100%">Faculty</asp:label></TD>
						<TD>
							<asp:textbox id="txtFaculty" runat="server" CssClass="input" Width="100%" MaxLength="100"></asp:textbox></TD>
						<TD></TD>
						<TD></TD>
						<TD></TD>
					</TR>
					<TR>
						<TD colSpan="5">
							<HR width="97%">
						</TD>
					</TR>
					<TR>
						<TD><asp:label id="lblCultureLevelID" CssClass="label" runat="server" Width="100%">Education</asp:label></TD>
						<TD><asp:dropdownlist id="cboLSCultureLevelID" CssClass="combo" runat="server" Width="100%"></asp:dropdownlist></TD>
						<TD></TD>
						<TD><asp:label id="lblEmail" CssClass="label" runat="server" Width="100%">Email</asp:label></TD>
						<TD><asp:textbox id="txtEmail" CssClass="input" runat="server" Width="218px" MaxLength="50" onblur="CheckValidEmail(this)"></asp:textbox></TD>
					</TR>
					<TR>
						<TD><asp:label id="lblMajorLevelID" CssClass="label" runat="server" Width="100%">Major Level 1</asp:label></TD>
						<TD><asp:dropdownlist id="cboLSQualifiMajorID" CssClass="combo" runat="server" Width="100%"></asp:dropdownlist></TD>
						<TD></TD>
						<TD>
							<asp:label id="Label5" runat="server" CssClass="label" Width="100%">Degree 1</asp:label></TD>
						<TD>
							<asp:dropdownlist id="cboPassWithLevel" runat="server" CssClass="combo" Width="100%"></asp:dropdownlist></TD>
					</TR>
					<TR>
						<TD><asp:label id="Label27" CssClass="label" runat="server" Width="100%">Major Level 2</asp:label></TD>
						<TD><asp:dropdownlist id="cboLSQualifiMajorID2" CssClass="combo" runat="server" Width="100%"></asp:dropdownlist></TD>
						<TD></TD>
						<TD>
							<asp:label id="Label28" runat="server" CssClass="label" Width="100%">Degree 2</asp:label></TD>
						<TD>
							<asp:dropdownlist id="cboPassWithLevel2" runat="server" CssClass="combo" Width="100%"></asp:dropdownlist></TD>
					</TR>
					<TR>
						<TD>
							<asp:label id="Label6" runat="server" CssClass="label" Width="100%">Home phone</asp:label></TD>
						<TD>
							<asp:textbox id="txtHomePhone" runat="server" CssClass="input" Width="100%" MaxLength="30"></asp:textbox></TD>
						<TD></TD>
						<TD><asp:label id="lblMobile" CssClass="label" runat="server" Width="100%">Mobile phone</asp:label></TD>
						<TD><asp:textbox id="txtMobifone" CssClass="input" runat="server" Width="100%" MaxLength="30"></asp:textbox></TD>
					</TR>
					<TR>
						<TD colSpan="5">
							<HR width="97%">
						</TD>
					</TR>
					<TR>
						<TD>
							<asp:label id="Label8" runat="server" CssClass="label" Width="100%">Tax Code</asp:label></TD>
						<TD>
							<asp:textbox id="txtTaxCode" runat="server" CssClass="input" Width="100%" MaxLength="30"></asp:textbox></TD>
						<TD></TD>
						<TD><asp:label id="Label9" CssClass="label" runat="server" Width="100%">Cost Centre</asp:label></TD>
						<TD><asp:textbox id="txtCostCode" CssClass="input" runat="server" Width="100%" MaxLength="30"></asp:textbox></TD>
					</TR>
					<TR>
						<TD>
							<asp:label id="Label12" runat="server" CssClass="label" Width="100%">Currency type</asp:label></TD>
						<TD><asp:dropdownlist id="cboLSCurrencyTypeID" CssClass="combo" runat="server" Width="100%"></asp:dropdownlist></TD>
						<TD></TD>
						<TD></TD>
						<TD></TD>
					</TR>
					<TR>
						<TD>
							<asp:label id="Label11" runat="server" CssClass="label" Width="100%">Bank Branch</asp:label></TD>
						<TD>
							<asp:dropdownlist id="cboLSBankBranchID" runat="server" CssClass="combo" Width="100%"></asp:dropdownlist></TD>
						<TD></TD>
						<TD></TD>
						<TD></TD>
					</TR>
					<TR>
						<TD style="HEIGHT: 24px">
							<asp:label id="Label13" runat="server" CssClass="label" Width="100%">A/C No.</asp:label></TD>
						<TD style="HEIGHT: 24px">
							<asp:textbox id="txtAccountNo" runat="server" CssClass="input" MaxLength="30" Width="100%"></asp:textbox></TD>
						<TD style="HEIGHT: 24px"></TD>
						<TD style="HEIGHT: 24px">
							<asp:label id="Label14" runat="server" CssClass="label" Width="100%">A/C Name</asp:label></TD>
						<TD style="HEIGHT: 24px">
							<asp:textbox id="txtAccountName" runat="server" CssClass="input" MaxLength="100" Width="100%"></asp:textbox></TD>
					</TR>
					<TR>
						<TD>
							<asp:label id="Label29" runat="server" CssClass="label" Width="100%">Bank Branch 2</asp:label></TD>
						<TD>
							<asp:dropdownlist id="cboLSBankBranchID2" runat="server" CssClass="combo" Width="100%"></asp:dropdownlist></TD>
						<TD></TD>
						<TD></TD>
						<TD></TD>
					</TR>
					<TR>
						<TD>
							<asp:label id="Label30" runat="server" CssClass="label" Width="100%">A/C No 2</asp:label></TD>
						<TD>
							<asp:textbox id="txtAccountNo2" runat="server" CssClass="input" MaxLength="30" Width="100%"></asp:textbox></TD>
						<TD></TD>
						<TD>
							<asp:label id="Label31" runat="server" CssClass="label" Width="100%">A/C Name 2</asp:label></TD>
						<TD>
							<asp:textbox id="txtAccountName2" runat="server" CssClass="input" MaxLength="100" Width="100%"></asp:textbox></TD>
					</TR>
					<TR>
						<TD colSpan="5">
							<HR width="97%">
						</TD>
					</TR>
					<tr>
						<td colspan="5"><asp:label id="lblOtherInfomationTitle" runat="server" CssClass="labelSubTitle" Width="100%">Other Infomation</asp:label></td>
					</tr>
					<tr>
						<td colspan="5"><asp:checkbox id="chkInsurance" runat="server" CssClass="checkbox" Text="Join in Social Insurance?"></asp:checkbox></td>
					</tr>
					<tr>
						<td colspan="5"><asp:checkbox id="chkBHTN" runat="server" CssClass="checkbox" Text="Join in Unemployment Insurance?"></asp:checkbox></td>
					</tr>
					<tr>
						<td colspan="5"><asp:checkbox id="chkBHYT" runat="server" CssClass="checkbox" Text="Join in Health Insurance?"></asp:checkbox></td>
					</tr>
					<tr>
						<td colspan="5"><asp:checkbox id="chkLifeInsurance" runat="server" CssClass="checkbox" Text="Life Insurance?"></asp:checkbox></td>
					</tr>
					<tr>
						<td colspan="5"><asp:checkbox id="chkLabourUnion" runat="server" CssClass="checkbox" Text="Join in LabourUnion?"></asp:checkbox></td>
					</tr>
					<TR style="DISPLAY:none">
						<TD>
							<asp:label id="lblMajorID" runat="server" CssClass="label" Width="100%">Major</asp:label></TD>
						<TD>
							<asp:textbox id="txtMajor" runat="server" CssClass="input" Width="100%" MaxLength="50"></asp:textbox></TD>
						<TD></TD>
						<TD>
							<asp:label id="Label10" runat="server" CssClass="label" Width="100%">Currency type</asp:label></TD>
						<TD>
							<asp:dropdownlist id="Dropdownlist1" runat="server" CssClass="combo" Width="100%">
								<asp:ListItem></asp:ListItem>
								<asp:ListItem Value="USD">USD</asp:ListItem>
								<asp:ListItem Value="VND">VND</asp:ListItem>
							</asp:dropdownlist></TD>
					</TR>
					<TR style="DISPLAY: none">
						<TD colSpan="5">
							<HR width="100%">
						</TD>
					</TR>
					<TR style="DISPLAY: none">
						<TD><asp:label id="Label2" CssClass="label" runat="server" Width="100%">ScanCode</asp:label></TD>
						<TD><asp:textbox id="txtScanCode" CssClass="input" runat="server" Width="100%" MaxLength="9"></asp:textbox></TD>
						<TD></TD>
						<TD><asp:label id="Label4" CssClass="label" runat="server" Width="100%">Scan Time No</asp:label></TD>
						<TD><asp:textbox id="txtScanTimeNo" onblur="JavaScript:checkInteger(this)" CssClass="input" runat="server"
								Width="100%" MaxLength="1"></asp:textbox></TD>
					</TR>
					<TR style="DISPLAY: none">
						<TD><asp:label id="Label3" CssClass="label" runat="server" Width="100%">Shift</asp:label></TD>
						<TD><asp:dropdownlist id="cboLSShiftID" CssClass="combo" runat="server" Width="100%"></asp:dropdownlist></TD>
						<TD></TD>
						<TD></TD>
						<TD></TD>
					</TR> <!-- here --> <!-- end --></TR>
	<TR style="DISPLAY: none">
		<TD vAlign="top" align="center" colSpan="5"><asp:button id="btnTemp" runat="server"></asp:button></TD>
	</TR>
	<TR style="DISPLAY: none">
		<TD vAlign="top" align="center" colSpan="5" height="27"></TD>
	</TR>
	<TR id="trAdResidence" style="DISPLAY: none" runat="server">
		<TD vAlign="top" colSpan="5" height="27">
			<HR width="100%">
			<asp:checkbox id="chkAdResidence" CssClass="chkAdmin" runat="server" Checked="True"></asp:checkbox><asp:checkbox id="chkResidence" accessKey="G" onclick="javascript:collapseResidence()" CssClass="checkbox"
				runat="server" Text="Residence" Checked="True" ToolTip="Alt+G"></asp:checkbox>&nbsp;</TD>
	</TR>
	<TR id="trAdResidence2" style="DISPLAY: none" runat="server">
		<TD vAlign="top" colSpan="5" height="27"><asp:checkbox id="chkAdResidence2" CssClass="chkAdmin" runat="server" Checked="True"></asp:checkbox><asp:linkbutton id="btnAddnew_Residence" CssClass="Detail" runat="server" Visible="False"></asp:linkbutton></TD>
	</TR>
	<TR id="trAdResidence3" style="DISPLAY: none" runat="server">
		<TD vAlign="top" colSpan="5" height="27"><asp:datagrid id="dtgList" runat="server" Width="100%" Visible="False" BorderColor="#3366CC" AllowSorting="True"
				AutoGenerateColumns="False" CellPadding="0" BackColor="White">
				<FooterStyle CssClass="gridFooter"></FooterStyle>
				<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
				<ItemStyle CssClass="gridItem"></ItemStyle>
				<HeaderStyle CssClass="gridHeader"></HeaderStyle>
				<Columns>
					<asp:BoundColumn Visible="False" DataField="ResidenceID" HeaderText="Residence">
						<HeaderStyle Width="10%"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="FromMonth" HeaderText="FromMonth">
						<HeaderStyle Width="10%"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="ToMonth" HeaderText="ToMonth">
						<HeaderStyle Width="10%"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="Address" HeaderText="Address">
						<HeaderStyle Width="30%"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="District" HeaderText="District">
						<HeaderStyle Width="30%"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="Province" HeaderText="Province">
						<HeaderStyle Width="30%"></HeaderStyle>
					</asp:BoundColumn>
				</Columns>
			</asp:datagrid><asp:checkbox id="chkAdResidence3" CssClass="chkAdmin" runat="server" Checked="True"></asp:checkbox></TD>
	</TR>
	<TR id="trAdResidence4" style="DISPLAY: none" runat="server">
		<TD vAlign="top" colSpan="5" height="27"><asp:checkbox id="chkAdResidence4" CssClass="chkAdmin" runat="server" Checked="True"></asp:checkbox></TD>
	</TR>
	<TR>
		<TD vAlign="top" align="center" colSpan="5" height="27"><asp:linkbutton id="btnChangID" accessKey="C" CssClass="button" runat="server" ToolTip="ALT+C, Thay d?i mã NV">&nbsp;&nbsp;&nbsp;Change code&nbsp;&nbsp;&nbsp;</asp:linkbutton>&nbsp;&nbsp;&nbsp;
			<asp:linkbutton id="btnSave" accessKey="S" CssClass="button" runat="server" ToolTip="ALT+S">&nbsp;&nbsp;&nbsp;Save&nbsp;&nbsp;&nbsp;</asp:linkbutton>&nbsp;&nbsp;&nbsp;
			<asp:linkbutton id="btnList" accessKey="L" CssClass="button" runat="server" ToolTip="ALT+L">&nbsp;&nbsp;&nbsp;List&nbsp;&nbsp;&nbsp;</asp:linkbutton>&nbsp;</TD>
	</TR>
	</TD></TR></TABLE>
</TD></TR></TBODY></TABLE>
<script language="javascript">		
	
function validform(){	
	if(checkisnull('txtVLastName')==false)  return false;	
	if(checkisnull('txtVFirstName')==false)  return false;
	if(checkisnull('cboGender')==false)  return false;
	if(checkisnull('txtStartDate')==false)  return false;
	if(document.getElementById('_ctl0_txtDOB').value!="")
	{
		if(FromSmallNow(document.getElementById('_ctl0_txtDOB')) == false)
		{
			//alert('Date of birth must be less than now');
			GetAlertError(iTotal,DSAlert,"0004");
			document.getElementById('_ctl0_txtDOB').focus();
			return false;
		}		
	}
	if(document.getElementById('_ctl0_txtIDIssuedDate').value!="")
	{
		if(FromSmallNow(document.getElementById('_ctl0_txtIDIssuedDate')) == false)
		{
			//alert('Issue date must be less than now');
			GetAlertError(iTotal,DSAlert,"0005");
			document.getElementById('_ctl0_txtIDIssuedDate').focus();
			return false;
		}		
	}
	// joining date
	if(document.getElementById('_ctl0_txtStartDate').value!="")
	{
		if(FromSmallNow(document.getElementById('_ctl0_txtStartDate')) == false)
		{
			//alert('Date of birth must be less than now');
			GetAlertError(iTotal,DSAlert,"0004");
			document.getElementById('_ctl0_txtStartDate').focus();
			return false;
		}		
	}
	if (document.getElementById('_ctl0_txtScanTimeNo').value!="")
	{
		if(document.getElementById('_ctl0_txtScanTimeNo').value!="2" && document.getElementById('_ctl0_txtScanTimeNo').value!="4" && document.getElementById('_ctl0_txtScanTimeNo').value!="0" )
		{
			alert('S? l?n quét th? ph?i là 2 ho?c 4!');
			document.getElementById('_ctl0_txtScanTimeNo').focus();
			return false;
		}
	}
	
	return true;
}
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
</script>
<script language="javascript">
	function OpenBuildCode()
	{		
		window.open('FormPage.aspx?ModuleID=HR&ParentID=8&FunctionID=204&Ascx=MdlHR/BuildEmpCode.ascx&action=addnew&empid=' + document.getElementById("_ctl0_HR_EmpHeader_txtEmpID").value, 'BuildCode', 'width=320,height=520,left=300,top=200,dependent');		
		return false;
	}
	function OpenEditCode()
	{		
		window.open('FormPage.aspx?ModuleID=HR&ParentID=8&FunctionID=204&Ascx=MdlHR/BuildEmpCode.ascx&action=edit&empid=' + document.getElementById("_ctl0_HR_EmpHeader_txtEmpID").value, 'BuildCode', 'width=520,height=330,left=300,top=200,dependent');		
		//window.open('MdlHR/BuildEmpCode.aspx?action=edit&empid=' + document.getElementById("_ctl0_HR_EmpHeader_txtEmpID").value, 'BuildCode', 'width=320,height=200,left=300,top=200,dependent');
		return false;
	}
	function ShowImg()
	{		
		  if (document.getElementById ('_ctl0_fileUpload').value=='')
		  {		  
		  document.getElementById ('_ctl0_imgEmp').src='';
		  }
		  else
		  {		  
		  document.getElementById ('_ctl0_imgEmp').src =document.getElementById ('_ctl0_fileUpload').value;
		  }
	}
function getAge(field)
	{
		if (Toddmmyyyy(field)==1)
		return;
		var checkstr = "0123456789";
		var DateField = field;
		var Datevalue = "";
		var DateTemp = "";
		var seperator = "/";
		var day;
		var month;
		var year;
		var leap = 0;
		var err = 0;
		var i;
		err = 0;
		DateValue = DateField.value;
		if (DateValue =="")	return;
		/* Delete all chars except 0..9 */
		for (i = 0; i < DateValue.length; i++) {
			if (checkstr.indexOf(DateValue.substr(i,1)) >= 0) {
				DateTemp = DateTemp + DateValue.substr(i,1);
			}
		}
		DateValue = DateTemp;
		/* Always change date to 8 digits - string*/
		/* if year is entered as 2-digit / always assume 20xx */
		if (DateValue.length == 6) {
			DateValue = DateValue.substr(0,4) + '20' + DateValue.substr(4,2); }  
		   
		if (DateValue.length != 8) {
			err = 19;}
		/* year is wrong if year = 0000 */		
		year = DateValue.substr(4,4);		
		if (year < 1900 )
		{
			err=18;
		}
		if (year == 0) {
			err = 20;
		}
		/* Validation of month*/
		day = DateValue.substr(0,2);
		month = DateValue.substr(2,2);   
		if ((month < 1) || (month > 12)) {
			err = 21;
		}
		/* Validation of day*/
		   
		if (day < 1) {
			err = 22;
		}
		   
		 
		/* Validation leap-year / february / day */
		if ((year % 4 == 0) || (year % 100 == 0) || (year % 400 == 0)) {
			leap = 1;
		}
		if ((month == 2) && (leap == 1) && (day > 29)) {
			err = 23;
		}
		if ((month == 2) && (leap != 1) && (day > 28)) {
			err = 24;
		}
		/* Validation of other months */
		if ((day > 31) && ((month == "01") || (month == "03") || (month == "05") || (month == "07") || (month == "08") || (month == "10") || (month == "12"))) {
			err = 25;
		}
		if ((day > 30) && ((month == "04") || (month == "06") || (month == "09") || (month == "11"))) {
			err = 26;
		}
		/* if 00 ist entered, no error, deleting the entry */
		if ((day == 0) && (month == 0) && (year == 00)) {
			err = 0; day = ""; month = ""; year = ""; seperator = "";
		}
		/* if no error, write the completed date to Input-Field (e.g. 13.12.2001) */
		   
		if (err == 0) {
			DateField.value = day + seperator + month + seperator + year;
			if (DateField.value == "")  	
			{
				//alert("Ngày không dúng d?nh d?ng!");
				GetAlertError(iTotal,DSAlert,"0031");
				DateField.focus();	
				return;
			}      
		}		
		/* Error-message if err != 0 */
		else if (err=18)
		{
				DateField.value = "";
				DateField.focus();
				GetAlertError(iTotal,DSAlert,"0030");
				return; 
		}
		else {
				DateField.value = "";
				DateField.focus();
				GetAlertError(iTotal,DSAlert,"0031");
				return; 
		}	
		
		var arrDOBRel=field.value.split('/');
		var iDOBRel=arrDOBRel[2];
		var d=new Date(iDOBRel,arrDOBRel[1],arrDOBRel[0]);
		//alert(iDOBRel+' - '+ arrDOBRel[1] + ' - ' + arrDOBRel[0] + ':day:' + d);
		var dnow=new Date();				
		if(dnow<d)
		{
			GetAlertError(iTotal,DSAlert,"0004");
			field.value='';
			document.getElementById('_ctl0_txtAge').value='';			
			return false;
		}	
		else
		{
			
			document.getElementById('_ctl0_txtAge').value=dnow.getFullYear()-iDOBRel;
		}
		 ToddMMMyyyy(DateField);
}	
function collapseResidence()
{
		if (document.getElementById('_ctl0_chkResidence').checked==false)
	{		
		document.getElementById('_ctl0_trAdResidence2').style.display="none";		
		document.getElementById('_ctl0_trAdResidence3').style.display="none";		
	}
	else
	{
		document.getElementById('_ctl0_trAdResidence2').style.display="block";		
		document.getElementById('_ctl0_trAdResidence3').style.display="block";		
		
	}
}
function PopUp_Addnew()
{
		ShowDialog('FormPage.aspx?ModuleID=PR&ParentID=68&FunctionID=9999&Ascx=MdlHR/Residence.ascx',800,200);						
		return false;
}	
</script>
