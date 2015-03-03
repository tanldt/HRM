<%@ Control Language="c#" AutoEventWireup="false" Codebehind="OtherInfo.ascx.cs" Inherits="iHRPCore.MdlHR.OtherInfo" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="Address" Src="../Include/Address.ascx" %>
<%@ Register TagPrefix="uc" TagName="EmpHeader" Src="../Include/EmpHeader.ascx" %>
<%@ Import Namespace ="System.Data.SqlClient" %>
<%@ Import Namespace ="System.Data" %>
<%@ Import Namespace ="iHRPCore.Com"%>
<%
	string strLangID = Session["LangID"] == null?"EN":Session["LangID"].ToString().Trim();
	int Total1 = 0;
	
	DataTable rs1 = new DataTable();	
	rs1 = clsCommon.GetDataTable("sp_GetDataCombo @TableName = 'LS_tblDistrict'");	
	Total1 = rs1.Rows.Count;	
%>
<script language="javascript">
		//array for District
		var DS1 = new Array(3);
		DS1[0] = new Array(<%=Total1%>);
		DS1[1] = new Array(<%=Total1%>);
		DS1[2] = new Array(<%=Total1%>);	
		<% for(int i=0; i<Total1; i++) {%>		
			DS1[0][<%=i%>]="<%=rs1.Rows[i]["LSProvinceID"]%>";
			DS1[1][<%=i%>]="<%=rs1.Rows[i]["LSDistrictID"]%>";
			<% if (strLangID=="EN") {%>
				DS1[2][<%=i%>]="<%=rs1.Rows[i]["Name"]%>";	
			<%} else{%>
				DS1[2][<%=i%>]="<%=rs1.Rows[i]["VNName"]%>";
			<%}%>	
		<%}%>
</script>
<script language="javascript">
	function ChangeProvince()
	{
		var all = document.getElementById("_ctl0_cboDistrictID").length;
		
		for(i=0; i<all; i++)
		{	
			document.getElementById("_ctl0_cboDistrictID").remove(0);			
		};
		var strProvinceID = document.getElementById("_ctl0_cboLSProvinceID").value; 
		document.getElementById("_ctl0_cboDistrictID").add(new Option('',''));
		
		for(i=0; i<<%=Total1%>;i++)
		{
			if (DS1[0][i]==strProvinceID)
			{
				document.getElementById("_ctl0_cboDistrictID").add(new Option(DS1[2][i],DS1[1][i]));
			};
		};
		ChangeDistrict();
	}
	function ChangeDistrict()
	{
		document.getElementById("_ctl0_txtLSDistrictID").value = document.getElementById("_ctl0_cboDistrictID").value;
	}
</script>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="97%" border="0">
	<TR>
		<TD><asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center"><uc:empheader id="HR_EmpHeader" runat="server"></uc:empheader></TD>
	</TR>
	<TR>
		<TD align="center">
			<TABLE id="Table6" cellSpacing="2" cellPadding="0" width="100%" border="0">
				<TR>
					<TD width="12.5%"></TD>
					<TD width="30%"><asp:label id="Label7" runat="server" CssClass="label" Width="100%"> Address</asp:label></TD>
					<TD id="tdAdDistrinctchk" width="15%" runat="server"><asp:checkbox id="chkAdDistrinct" CssClass="chkAdmin" Runat="server"></asp:checkbox><asp:label id="lblT_Distrinct" runat="server" CssClass="label" Width="70%"> District</asp:label></TD>
					<TD id="tdAdProvincechk" width="20%" runat="server"><asp:checkbox id="chkAdProvince" CssClass="chkAdmin" Width="10%" Runat="server"></asp:checkbox><asp:label id="lblT_Province" runat="server" CssClass="label" Width="80%">Province</asp:label></TD>
					<TD width="20%"><asp:label id="Label4" runat="server" CssClass="label"> Phone</asp:label></TD>
				</TR>
				<TR id="trAdPermanent" runat="server">
					<TD width="10%"><asp:checkbox id="chkAdPermanent" CssClass="chkAdmin" Runat="server"></asp:checkbox><asp:label id="lblPermanentAddress" runat="server" CssClass="label" Width="60%"> Permanent</asp:label></TD>
					<TD width="30%"><asp:textbox id="txtP_Address" runat="server" CssClass="InputReadOnly" Width="100%" ReadOnly="True"
							MaxLength="100"></asp:textbox></TD>
					<TD id="tdAdDistrincttxt" width="15%" runat="server"><asp:textbox id="txtP_District" runat="server" CssClass="InputReadOnly" Width="100%" ReadOnly="True"
							MaxLength="15"></asp:textbox></TD>
					<TD id="tdAdProvincelbl" width="20%" runat="server"><asp:dropdownlist id="cboP_LSProvinceID" runat="server" CssClass="combo" Width="100%"></asp:dropdownlist></TD>
					<TD width="20%"><asp:textbox id="txtP_Phone" runat="server" CssClass="InputReadOnly" Width="100%" MaxLength="30"></asp:textbox></TD>
				</TR>
				<TR id="trAdTemporary" runat="server">
					<TD><asp:checkbox id="chkAdTemporary" CssClass="chkAdmin" Runat="server"></asp:checkbox><asp:label id="lblTemporaryAddress" runat="server" CssClass="label" Width="60%"> Temporary</asp:label></TD>
					<TD><asp:textbox id="txtT_Address" runat="server" CssClass="InputReadOnly" Width="100%" ReadOnly="True"
							MaxLength="100"></asp:textbox></TD>
					<TD id="tdAdDistrinctcbo" runat="server"><asp:textbox id="txtT_District" runat="server" CssClass="InputReadOnly" Width="100%" ReadOnly="True"
							MaxLength="15"></asp:textbox></TD>
					<TD id="tdAdProvincetxt" runat="server"><asp:dropdownlist id="cboT_LSProvinceID" runat="server" CssClass="combo" Width="100%"></asp:dropdownlist></TD>
					<TD><asp:textbox id="txtT_Phone" runat="server" CssClass="InputReadOnly" Width="100%" MaxLength="30"></asp:textbox></TD>
				</TR>
				<TR id="trAdEmergency" runat="server">
					<TD><asp:checkbox id="chkAdEmergency" CssClass="chkAdmin" Runat="server"></asp:checkbox><asp:label id="lblEmergencyAddress" runat="server" CssClass="label" Width="60%"> Emergency</asp:label></TD>
					<TD vAlign="top"><asp:textbox id="txtE_Address" style="POSITION: absolute" runat="server" CssClass="input" Width="100%"
							MaxLength="100"></asp:textbox></TD>
					<TD id="tdAdDistrinctlbl" runat="server"><asp:textbox id="txtE_District" runat="server" CssClass="input" Width="100%" MaxLength="15"></asp:textbox></TD>
					<TD id="tdAdProvincecbo" runat="server"><asp:dropdownlist id="cboE_LSProvinceID" runat="server" CssClass="combo" Width="100%"></asp:dropdownlist></TD>
					<TD><asp:textbox id="txtE_Phone" runat="server" CssClass="input" Width="100%" MaxLength="30"></asp:textbox></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<td>
			<table width="100%" border="0">
				<TR>
					<TD width="11%" colSpan="1" rowSpan="1"><asp:label id="lblEmergencyContact" runat="server" CssClass="label" Width="100%">E. Contact</asp:label></TD>
					<TD vAlign="top" width="18%" colSpan="2">&nbsp;<asp:textbox id="txtE_ContactName" runat="server" CssClass="input" Width="100%" MaxLength="30"></asp:textbox></TD>
					<td width="6%"></td>
					<TD align="left" width="20%" colSpan="2"></TD>
					<TD align="center" width="10%"></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD width="11%"><asp:label id="lblProvince" runat="server" CssClass="label">Province</asp:label></TD>
					<TD vAlign="top" width="18%" colSpan="2">&nbsp;<asp:dropdownlist id="cboLSProvinceID" runat="server" CssClass="combo" Width="100%" onchange="ChangeProvince()"></asp:dropdownlist></TD>
					<TD width="6%"><asp:label id="lblDistrict" runat="server" CssClass="label">District</asp:label></TD>
					<TD align="left" width="20%" colSpan="2"><asp:dropdownlist id="cboDistrictID" runat="server" CssClass="combo" Width="100%" onchange="ChangeDistrict()"></asp:dropdownlist></TD>
					<TD align="center" width="10%"><asp:textbox id="txtLSDistrictID" runat="server" CssClass="input" Width="0px" MaxLength="15"></asp:textbox></TD>
					<TD></TD>
				</TR>
				<TR id="trAdSkill" runat="server">
					<TD><asp:checkbox id="chkAdSkill" CssClass="chkAdmin" Runat="server"></asp:checkbox><asp:label id="Label1" runat="server" CssClass="label">Skill</asp:label></TD>
					<TD vAlign="top" colSpan="7">&nbsp;<asp:textbox id="txtSkill" runat="server" CssClass="input" Width="100%" MaxLength="100"></asp:textbox></TD>
				</TR>
				<TR id="trAdHobby" runat="server">
					<TD><asp:checkbox id="chkAdHobby" CssClass="chkAdmin" Runat="server"></asp:checkbox><asp:label id="Label3" runat="server" CssClass="label">Hobby</asp:label></TD>
					<TD vAlign="top" colSpan="7">&nbsp;<asp:textbox id="txtHobby" runat="server" CssClass="input" Width="100%" MaxLength="100"></asp:textbox></TD>
				</TR>
				<TR>
					<TD><asp:label id="Label5" runat="server" CssClass="label">Health</asp:label></TD>
					<TD vAlign="top" colSpan="7">
						<table width="80%">
							<tr id="trAdHealthStatus" runat="server">
								<td colSpan="2"><asp:checkbox id="chkAdHealthStatus" CssClass="chkAdmin" Runat="server"></asp:checkbox>&nbsp;<asp:radiobuttonlist id="rdHealthStatus" runat="server" CssClass="option" Width="100%" RepeatDirection="Horizontal"
										RepeatColumns="3">
										<asp:ListItem Value="1">Weak</asp:ListItem>
										<asp:ListItem Value="2">Normal</asp:ListItem>
										<asp:ListItem Value="0" Selected="True">Strong</asp:ListItem>
									</asp:radiobuttonlist></td>
							</tr>
							<tr>
								<td width="30%"><asp:checkbox id="chkHealthMalformation" onclick="ChangeCheck(this, 'txtHealthMalformationNote')"
										runat="server" CssClass="checkbox" Text="Malformation" Checked="False"></asp:checkbox></td>
								<td><asp:textbox id="txtHealthMalformationNote" style="DISPLAY: none" runat="server" CssClass="input"
										Width="100%" MaxLength="100"></asp:textbox></td>
							</tr>
							<TR>
								<TD><asp:checkbox id="chkHealthInjury" onclick="ChangeCheck(this, 'txtHealthInjuryNote')" runat="server"
										CssClass="checkbox" Text="Injury"></asp:checkbox></TD>
								<td><asp:textbox id="txtHealthInjuryNote" style="DISPLAY: none" runat="server" CssClass="input" Width="100%"
										MaxLength="100"></asp:textbox></td>
							</TR>
						</table>
					</TD>
				</TR>
				<TR style="DISPLAY:none">
					<TD><asp:label id="lblTaxcode1" runat="server" CssClass="label">Tax code</asp:label></TD>
					<TD vAlign="top" colSpan="7">&nbsp;
						<asp:textbox id="txtTaxCode" runat="server" CssClass="input" MaxLength="20" width="184px"></asp:textbox></TD>
				</TR>
				<TR>
					<TD><asp:label id="Label2" runat="server" CssClass="label">Note</asp:label></TD>
					<TD vAlign="top" colSpan="7">&nbsp;
						<asp:textbox id="txtNote" style="POSITION: absolute" runat="server" CssClass="input" Width="100%"
							MaxLength="255"></asp:textbox></TD>
				</TR>
			</table>
		</td>
	</TR>
	<TR>
		<TD>
			<HR width="97%">
		</TD>
	</TR>
	<tr>
		<td>
			<table width="100%">
				<TR>
					<TD colSpan="2"><asp:checkbox id="chkPayByBank" onclick="javascript:collapsePay();" runat="server" CssClass="labelSubTitle"
							Text="Pay By Bank"></asp:checkbox></TD>
					<TD></TD>
					<TD><asp:textbox id="txtLSBankBranchID" runat="server" CssClass="input" Width="0px" MaxLength="15"></asp:textbox></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
				</TR>
				<TR id="trbank" style="DISPLAY: none" runat="server">
					<TD width="10%"><asp:label id="lblBank" runat="server" CssClass="label" Width="100%">Bank</asp:label></TD>
					<TD width="20%"><asp:dropdownlist id="cboLSBankID" runat="server" CssClass="combo" Width="100%" onchange="ChangeBank()"></asp:dropdownlist></TD>
					<TD width="1%"></TD>
					<TD width="12%"><asp:label id="lblAccountNo" runat="server" CssClass="label" Width="100%">Account No</asp:label></TD>
					<TD><asp:textbox id="txtAccountNo" runat="server" CssClass="input" Width="100%" MaxLength="20"></asp:textbox></TD>
					<TD width="1%"></TD>
					<TD width="12%"><asp:label id="lblAccountName" runat="server" CssClass="label" Width="100%"> Name</asp:label></TD>
					<TD vAlign="top" width="30%"><asp:textbox id="txtAccountName" style="POSITION: absolute" runat="server" CssClass="input" Width="100%"
							MaxLength="50"></asp:textbox></TD>
				</TR>
				<TR id="trbankbranch" style="DISPLAY: none" runat="server">
					<TD width="12%"><asp:label id="Label9" runat="server" CssClass="label" Width="100%">Branch</asp:label></TD>
					<TD width="20%" colSpan="5"><asp:dropdownlist id="cboBankBranchID" runat="server" CssClass="combo" Width="100%" onchange="ChangeBankBranch()"></asp:dropdownlist></TD>
					<TD width="12%"><asp:label id="Label17" runat="server" CssClass="label" Width="100%">Account code</asp:label></TD>
					<TD width="20%"><asp:textbox id="txtAccountCode" runat="server" CssClass="input" Width="100%" MaxLength="30"></asp:textbox></TD>
				</TR>
			</table>
			<HR width="100%">
		</td>
	</tr>
	<tr>
		<td>
			<table cellSpacing="2" cellPadding="2" width="100%" border="0">
				<TBODY>
					<TR>
						<TD colSpan="4"><asp:label id="lblOtherInfomationTitle" runat="server" CssClass="labelSubTitle" Width="100%">Other Infomation</asp:label></TD>
					</TR>
					<TR>
						<TD id="tdAdJobBeforeRecruitmentlbl" width="20%" runat="server"><asp:checkbox id="chkAdJobBeforeRecruitment" CssClass="chkAdmin" Runat="server"></asp:checkbox><asp:label id="lblJobBeforeRecruitment" runat="server" CssClass="label" Width="100%" ToolTip="Job Before Recruitment">Job Before Recruitment</asp:label></TD>
						<TD id="tdAdJobBeforeRecruitmenttxt" vAlign="top" width="29%" runat="server"><asp:textbox id="txtJobBeforeRecruitment" style="POSITION: absolute" runat="server" CssClass="input"
								Width="100%" MaxLength="50"></asp:textbox></TD>
						<TD id="tdAdLBNolbl" width="15%" runat="server"><asp:checkbox id="chkAdLBNo" CssClass="chkAdmin" Runat="server"></asp:checkbox><asp:label id="Label8" runat="server" CssClass="label">LB  No.</asp:label></TD>
						<TD id="tdAdLBNotxt" width="15%" runat="server"><asp:textbox id="txtLabourBookNo" runat="server" CssClass="input" MaxLength="20"></asp:textbox></TD>
					</TR>
					<TR>
						<TD id="tdAdPoliticalArgumentlbl" width="20%" runat="server"><asp:checkbox id="chkAdPoliticalArgument" CssClass="chkAdmin" Runat="server"></asp:checkbox><asp:label id="lblPoliticalArgument" runat="server" CssClass="label" Width="80%" ToolTip="Political Argument">Political Argument</asp:label></TD>
						<TD id="tdAdPoliticalArgumentcbo" width="29%" runat="server"><asp:dropdownlist id="cboLSPoliticalLevelID" runat="server" CssClass="combo" Width="100%"></asp:dropdownlist></TD>
						<TD id="tdAdCurrencyTypelbl" width="15%" runat="server"><asp:checkbox id="chkAdCurrencyType" CssClass="chkAdmin" Runat="server"></asp:checkbox><asp:label id="cboCurrencyType" runat="server" CssClass="label" Width="70%" ToolTip="Currency Type">Currency Type</asp:label></TD>
						<TD id="tdAdCurrencyTypecbo" width="15%" runat="server"><asp:dropdownlist id="cboLSCurrencyTypeID" runat="server" CssClass="combo" Width="100%"></asp:dropdownlist></TD>
					</TR>
					<TR>
						<TD id="tdAdConferedTitlelbl" runat="server"><asp:checkbox id="chkAdConferedTitle" CssClass="chkAdmin" Runat="server"></asp:checkbox><asp:label id="lblConferedTitle" runat="server" CssClass="label" Width="80%" ToolTip="Confered Title">Confered Title</asp:label></TD>
						<TD id="tdAdConferedTitlecbo" runat="server"><asp:dropdownlist id="cboLSHeroTitleID" runat="server" CssClass="combo" Width="100%"></asp:dropdownlist></TD>
						<TD colSpan="2"><asp:checkbox id="chkInsurance" runat="server" CssClass="checkbox" Text="Join in Insurance?"></asp:checkbox></TD>
					</TR>
					<TR>
						<TD id="tdAdTaxlbl" width="20%" runat="server"><asp:checkbox id="chkAdTax" CssClass="chkAdmin" Runat="server"></asp:checkbox></TD>
						<TD id="tdAdTaxtxt" width="29%" runat="server"></TD>
						<TD width="30%" colSpan="2"><asp:checkbox id="ChkLifeInsurance" runat="server" CssClass="checkbox" Text="Life Insurance?"></asp:checkbox></TD>
					</TR>
					<TR>
						<TD width="20%" colSpan="2">
							<asp:checkbox id="ChkLabourUnion" CssClass="checkbox" runat="server" Text="Besides there are other insurances"></asp:checkbox></TD>
						<TD width="30%" colSpan="2"></TD>
					</TR>
				</TBODY>
			</table>
		</td>
	</tr>
	<TR style="DISPLAY: none">
		<TD vAlign="middle" align="center"></TD>
	</TR>
	<TR>
		<TD align="center"><asp:linkbutton id="btnSave" accessKey="S" runat="server" CssClass="btnSave" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp;&nbsp;
			<asp:linkbutton id="btnList" accessKey="L" runat="server" CssClass="btnList" ToolTip="ALT+L"> List</asp:linkbutton></TD>
	</TR>
</TABLE>
<%	
	int Total2 = 0;
	
	DataTable rs2 = new DataTable();	
	rs2 = clsCommon.GetDataTable("sp_GetDataCombo @TableName = 'LS_tblBankBranch'");
	Total2 = rs2.Rows.Count;	
%>
<script language="javascript">
		//array for District
		var DS2 = new Array(3);
		DS2[0] = new Array(<%=Total2%>);
		DS2[1] = new Array(<%=Total2%>);
		DS2[2] = new Array(<%=Total2%>);	
		<% for(int i=0; i<Total2; i++) {%>		
			DS2[0][<%=i%>]="<%=rs2.Rows[i]["LSBankID"]%>";
			DS2[1][<%=i%>]="<%=rs2.Rows[i]["LSBankBranchID"]%>";
			<% if (strLangID=="EN") {%>
				DS2[2][<%=i%>]="<%=rs2.Rows[i]["Name"]%>";
			<%} else{%>
				DS2[2][<%=i%>]="<%=rs2.Rows[i]["VNName"]%>";
			<%}%>	
		<%}%>
</script>
<script language="javascript">
	function ChangeBank()
	{
		var all = document.getElementById("_ctl0_cboBankBranchID").length;
		
		for(i=0; i<all; i++)
		{	
			document.getElementById("_ctl0_cboBankBranchID").remove(0);			
		};
		var strBankID = document.getElementById("_ctl0_cboLSBankID").value; 
		document.getElementById("_ctl0_cboBankBranchID").add(new Option('',''));
		
		for(i=0; i<<%=Total2%>;i++)
		{
			if (DS2[0][i]==strBankID)
			{
				document.getElementById("_ctl0_cboBankBranchID").add(new Option(DS2[2][i],DS2[1][i]));
			}
		}
		ChangeBankBranch();		
	}
	function ChangeBankBranch()
	{
		document.getElementById("_ctl0_txtLSBankBranchID").value = document.getElementById("_ctl0_cboBankBranchID").value;		
	}
</script>
<script language="javascript">
<!--
function validform(){	
	if (document.getElementById('_ctl0_chkPayByBank').checked)
	{
		if(checkisnull('cboLSBankID')==false)  return false;			
		if(checkisnull('txtAccountNo')==false)  return false;		
	}			
	return true;
}
function checkisnull(obj)
{
	if(document.getElementById('_ctl0_' + obj).value=='')
	{	
		GetAlertError(iTotal,DSAlert,"0003");
		document.getElementById('_ctl0_' + obj).focus();
		return false;
	}
	else
	{
		return true;
	}
}
function collapsePay()
{
	if (document.getElementById('_ctl0_chkPayByBank').checked ==false)
	{
		document.getElementById('_ctl0_trbank').style.display="none";
		document.getElementById('_ctl0_trbankbranch').style.display="none";		
		document.getElementById('_ctl0_txtAccountName').value = "";
		document.getElementById('_ctl0_txtAccountNo').value = "";
		document.getElementById('_ctl0_cboLSBankID').value = "";
		document.getElementById('_ctl0_cboBankBranchID').value = "";
	}
	else
	{
		//cboLSBankID
		document.getElementById('_ctl0_trbank').style.display="block";
		document.getElementById('_ctl0_trbankbranch').style.display="block";
		document.getElementById('_ctl0_txtAccountName').value = document.getElementById('_ctl0_HR_EmpHeader_txtEmpName').value;		
	}	

}

function collapseUnionMember()
{
	if (document.getElementById('_ctl0_chkUnionMember').checked ==false)
		document.getElementById('_ctl0_trUnionMember').style.display="none";
	else
		document.getElementById('_ctl0_trUnionMember').style.display="block";

}

function collapsePartyMember()
{
	if (document.getElementById('_ctl0_chkPartyMember').checked ==false)
	{
		document.getElementById('_ctl0_trPartyMember').style.display="none";
		document.getElementById('_ctl0_trProcess').style.display="none";
	}
	else
	{
		document.getElementById('_ctl0_trPartyMember').style.display="block";
		document.getElementById('_ctl0_trProcess').style.display="block";
	}	

}
function collapseWoundedSoldier()
{
	if (document.getElementById('_ctl0_chkWoundedSoldier').checked ==false)
	{
		document.getElementById('_ctl0_trWoundedSoldier').style.display="none";
		document.getElementById('_ctl0_trDescript').style.display="none";
	}
	else
	{
		document.getElementById('_ctl0_trWoundedSoldier').style.display="block";
		document.getElementById('_ctl0_trDescript').style.display="block";
	}	

}
function collapseLabourUnion()
{
	if(document.getElementById('_ctl0_chkLabourUnion').checked==false)
		document.getElementById('_ctl0_trLabourUnion').style.display="none";   
	else
		document.getElementById('_ctl0_trLabourUnion').style.display="block";   
}

function ChangeCheck(field, CtrlName)
{
	if (field.checked == true)
	{
		document.getElementById("_ctl0_" + CtrlName).style.display = "";
	}
	else
		document.getElementById("_ctl0_" + CtrlName).style.display = "none";
}
//-->
</script>

