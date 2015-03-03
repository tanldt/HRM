<%@ Import Namespace ="iHRPCore.Com"%>
<%@ Import Namespace ="System.Data" %>
<%@ Import Namespace ="System.Data.SqlClient" %>
<%@ Register TagPrefix="uc" TagName="EmpHeader" Src="../Include/EmpHeader.ascx"%>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="SocialActivity.ascx.cs" Inherits="iHRPCore.MdlHR.SocialActivity" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="Table2" style="HEIGHT: 129px" cellSpacing="1" cellPadding="1" width="98%" border="0">
	<TR>
		<TD align="center" colSpan="6"><uc:empheader id="HR_EmpHeader" runat="server"></uc:empheader></TD>
	</TR>
	<TR>
		<TD colSpan="6"><asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></TD>
	</TR>
	<TR>
		<TD colSpan="6"><asp:label id="lblPoliticsAndSociety" runat="server" CssClass="labelSubTitle">Politics and Society</asp:label></TD>
	</TR>
	<!-- Di Nuoc Ngoai -->
	<TR>
		<TD colSpan="6"><asp:checkbox id="chkShowAbroad" onclick="javascript:collapseGoAbroad()" runat="server" CssClass="checkbox"
				Text="Go Abroad"></asp:checkbox></TD>
	</TR>
	<TR id="trGoAbroad1" style="DISPLAY: none" runat="server">
		<TD colSpan="6">&nbsp;
			<asp:linkbutton id="btnGoAbroad" accessKey="Y" runat="server" CssClass="Detail" Width="56px"></asp:linkbutton></TD>
	</TR>
	<TR id="trGoAbroad2" style="DISPLAY: none" runat="server">
		<TD colSpan="6"><asp:datagrid id="dtgList" runat="server" Width="100%" BorderColor="#3366CC" AllowSorting="True"
				AutoGenerateColumns="False" CellPadding="0" BackColor="White">
				<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
				<ItemStyle CssClass="gridItem"></ItemStyle>
				<HeaderStyle CssClass="gridHeader"></HeaderStyle>
				<FooterStyle CssClass="gridFooter"></FooterStyle>
				<Columns>
					<asp:BoundColumn Visible="False" DataField="ResidenceID" HeaderText="AbroadRecordID">
						<HeaderStyle Width="10%"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="FromMonth" HeaderText="FromDate">
						<HeaderStyle Width="10%"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="ToMonth" HeaderText="ToDate">
						<HeaderStyle Width="10%"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="Address" HeaderText="NationalityID">
						<HeaderStyle Width="30%"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="District" HeaderText="Reason">
						<HeaderStyle Width="30%"></HeaderStyle>
					</asp:BoundColumn>
				</Columns>
			</asp:datagrid></TD>
	</TR>
	<TR>
		<TD colSpan="6"><asp:checkbox id="chkIsArmy" onclick="collapseArmy()" runat="server" CssClass="checkbox" Text="Is Army member?"></asp:checkbox></TD>
	</TR>
	<TR id="trArmy1" style="DISPLAY: none" runat="server">
		<TD width="25%"><asp:label id="lblLevel" runat="server" CssClass="labelrequire" Width="100%" ToolTip="Probation Date">Top Level</asp:label></TD>
		<TD></TD>
		<TD style="WIDTH: 169px"><asp:textbox id="txtArmyLevel" runat="server" CssClass="input" Width="177px" MaxLength="50"></asp:textbox></TD>
		<TD width="18%"></TD>
		<TD width="20%"><asp:label id="lblArmyPosition" runat="server" CssClass="labelrequire" Width="100%" ToolTip="Standard Date">Position</asp:label></TD>
		<TD><asp:textbox id="txtArmyPosition" runat="server" CssClass="input" Width="131px" MaxLength="50"></asp:textbox></TD>
	</TR>
	<TR id="trArmy2" style="DISPLAY: none" runat="server">
		<TD><asp:label id="lblArmyDivision" runat="server" CssClass="labelrequire" Width="100%">Division</asp:label></TD>
		<TD></TD>
		<TD style="WIDTH: 169px"><asp:textbox id="txtArmyDivision" runat="server" CssClass="input" MaxLength="50" width="176px"></asp:textbox></TD>
		<TD></TD>
		<TD><asp:label id="lblArmyDept" runat="server" CssClass="labelrequire" Width="100%">Department</asp:label></TD>
		<TD><asp:textbox id="txtArmyDept" runat="server" CssClass="input" MaxLength="50" width="100%"></asp:textbox></TD>
	</TR>
	<TR id="trArmy3" style="DISPLAY: none" runat="server">
		<TD width="25%"><asp:label id="lblDateInArmy" runat="server" CssClass="labelrequire" Width="100%" ToolTip="Probation Date">In Date</asp:label></TD>
		<TD></TD>
		<TD style="WIDTH: 169px"><asp:textbox id="txtDateInArmy" onblur="JavaScript:CheckDate(this)" runat="server" CssClass="input"
				Width="90px" MaxLength="10"></asp:textbox>&nbsp;<INPUT class="btnCal" onclick="javascript:popUpCalendar(<%=this.txtDateInArmy.ClientID%>);" type="button"></TD>
		<TD width="18%"></TD>
		<TD width="20%"><asp:label id="lblDateOutArmy" runat="server" CssClass="label" Width="100%" ToolTip="Standard Date">Out Date</asp:label></TD>
		<TD><asp:textbox id="txtDateOutArmy" onblur="JavaScript:CheckDate(this)" runat="server" CssClass="input"
				Width="90px" MaxLength="10"></asp:textbox>&nbsp;<INPUT class="btnCal" onclick="javascript:popUpCalendar(<%=this.txtDateOutArmy.ClientID%>);" type="button"></TD>
	</TR>
	<!-- Doan vien -->
	<TR>
		<TD colSpan="6"><asp:checkbox id="chkIsCU" onclick="collapsePartyMember()" runat="server" CssClass="checkbox"
				Text="Is party member?"></asp:checkbox></TD>
	</TR>
	<TR id="trPartyMember" style="DISPLAY: none" runat="server">
		<TD width="25%"><asp:label id="lblProbationDate" runat="server" CssClass="label" Width="100%" ToolTip="Probation Date">Probation Date</asp:label></TD>
		<TD></TD>
		<TD style="WIDTH: 169px"><asp:textbox id="txtCU_Probationdate" onblur="JavaScript:CheckDate(this)" runat="server" CssClass="input"
				Width="90px" MaxLength="10"></asp:textbox>&nbsp;<INPUT class="btnCal" onclick="javascript:popUpCalendar(<%=this.txtCU_Probationdate.ClientID%>);" type="button"></TD>
		<TD width="18%"></TD>
		<TD width="20%"><asp:label id="lblStandard" runat="server" CssClass="labelrequire" Width="100%" ToolTip="Standard Date">Standard Date</asp:label></TD>
		<TD><asp:textbox id="txtCU_StandardDate" onblur="JavaScript:CheckDate(this)" runat="server" CssClass="input"
				Width="90px" MaxLength="10"></asp:textbox>&nbsp;<INPUT class="btnCal" onclick="javascript:popUpCalendar(<%=this.txtCU_StandardDate.ClientID%>);" type="button"></TD>
	</TR>
	<TR id="trProcess" style="DISPLAY: none" runat="server">
		<TD><asp:label id="lblProcess" runat="server" CssClass="labelrequire" Width="100%">Process</asp:label></TD>
		<TD></TD>
		<TD colSpan="4"><asp:textbox id="txtCU_Process" runat="server" CssClass="input" MaxLength="100" width="512px"
				TextMode="MultiLine"></asp:textbox></TD>
	</TR>
	<!-- End Dang vien -->
	<!-- Doan Vien -->
	<TR>
		<TD colSpan="6"><asp:checkbox id="chkIsCYU" onclick="collapseUnionMember()" runat="server" CssClass="checkbox"
				Text="Is Union member?"></asp:checkbox></TD>
	</TR>
	<TR id="trUnionMember" style="DISPLAY: none" runat="server">
		<TD><asp:label id="lblTradeUnionist" runat="server" CssClass="labelrequire" Width="100%" ToolTip="Trade Unionist">Trade Unionist</asp:label></TD>
		<TD></TD>
		<TD style="WIDTH: 169px"><asp:textbox id="txtCYU_Date" onblur="JavaScript:CheckDate(this)" runat="server" CssClass="input"
				Width="90px" MaxLength="10"></asp:textbox>&nbsp;<INPUT class="btnCal" onclick="javascript:popUpCalendar(<%=this.txtCYU_Date.ClientID%>);" type="button"></TD>
		<TD></TD>
		<TD></TD>
		<TD></TD>
	</TR>
	<!-- End Doan Vien -->
	<!-- Cong Doan -->
	<TR>
		<TD colSpan="6"><asp:checkbox id="chkIsLBU" onclick="collapseLabourUnion()" runat="server" CssClass="checkbox"
				Text="Is Labour Union member?"></asp:checkbox></TD>
	</TR>
	<TR id="trLabourUnion" style="DISPLAY: none" runat="server">
		<TD width="10%"><asp:label id="lblLabourUnionDescript" runat="server" CssClass="labelrequire" Width="100%"> Description</asp:label></TD>
		<TD></TD>
		<TD width="90%" colSpan="4"><asp:textbox id="txtLBU_Description" runat="server" CssClass="input" MaxLength="100" width="512px"
				TextMode="MultiLine"></asp:textbox></TD>
	</TR>
	<!-- End Cong Doan -->
	<!-- Thuong Binh-->
	<TR>
		<TD colSpan="6"><asp:checkbox id="chkIsWoundedSoldier" onclick="collapseWoundedSoldier()" runat="server" CssClass="checkbox"
				Text="Is wounded soldier?"></asp:checkbox></TD>
	</TR>
	<TR id="trWoundedSoldier" style="DISPLAY: none" runat="server">
		<TD><asp:label id="lblSoldierStatus" runat="server" CssClass="labelrequire" Width="100%">Status</asp:label></TD>
		<TD></TD>
		<TD style="WIDTH: 169px"><asp:textbox id="txtSoldierStatus" runat="server" CssClass="input" MaxLength="50" width="176px"></asp:textbox></TD>
		<TD></TD>
		<TD><asp:label id="lblProfileSoldier" runat="server" CssClass="label" Width="100%">Profile No.</asp:label></TD>
		<TD><asp:textbox id="txtProfileSoldier" runat="server" CssClass="input" MaxLength="50" width="100%"></asp:textbox></TD>
	</TR>
	<TR id="trWoundedSoldier2" style="DISPLAY: none" runat="server">
		<TD><asp:label id="lblLevelSoldier" runat="server" CssClass="labelrequire" Width="100%">Level Wounded Soldier</asp:label></TD>
		<TD></TD>
		<TD style="WIDTH: 169px"><asp:textbox id="txtLevelSoldier" runat="server" CssClass="input" MaxLength="50" width="176px"></asp:textbox></TD>
		<TD></TD>
		<TD><asp:label id="lblWoundedSoldier" runat="server" CssClass="label" Width="100%" ToolTip="Wounded Soldier">Wounded Soldier Date</asp:label></TD>
		<TD style="WIDTH: 169px"><asp:textbox id="txtWoundedSoldierDate" onblur="JavaScript:CheckDate(this)" runat="server" CssClass="input"
				Width="90px" MaxLength="10"></asp:textbox>&nbsp;<INPUT class="btnCal" onclick="javascript:popUpCalendar(<%=this.txtWoundedSoldierDate.ClientID%>);" type="button"></TD>
	</TR>
	<TR id="trDescript" style="DISPLAY: none" runat="server">
		<TD><asp:label id="lblDescription" runat="server" CssClass="label" Width="100%"> Description</asp:label></TD>
		<TD></TD>
		<TD colSpan="4"><asp:textbox id="txtWoundDescript" runat="server" CssClass="input" MaxLength="100" width="512px"
				TextMode="MultiLine"></asp:textbox></TD>
	</TR>
	<!--T/G Che do cu -->
	<TR style="DISPLAY: none">
		<TD colSpan="6"><asp:checkbox id="chkIsRegime" onclick="collapseRegime()" runat="server" CssClass="checkbox" Text="Is Joining in old regime?"></asp:checkbox></TD>
	</TR>
	<TR id="trRegime" style="DISPLAY: none" runat="server">
		<TD width="10%"><asp:label id="lblRegime" runat="server" CssClass="labelrequire" Width="100%"> Description</asp:label></TD>
		<TD></TD>
		<TD width="90%" colSpan="4"><asp:textbox id="txtRegimeDescript" runat="server" CssClass="input" MaxLength="100" width="512"
				TextMode="MultiLine"></asp:textbox></TD>
	</TR>
	<TR>
		<TD align="center" colSpan="6">
			<HR width="100%" SIZE="1">
		</TD>
	</TR>
	<tr>
		<td align="center" colSpan="6">&nbsp;&nbsp;
			<asp:linkbutton id="btnSave" accessKey="S" runat="server" CssClass="btnSave" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:linkbutton id="btnList" accessKey="L" runat="server" CssClass="btnList" ToolTip="Alt + L">List</asp:linkbutton>&nbsp;&nbsp;&nbsp;</td>
	</tr>
</TABLE>
<script language="javascript">
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
function validform()
{
	if (document.getElementById('_ctl0_chkIsArmy').checked)
	{
		if(checkisnull('txtArmyLevel')==false)  return false;	
		if(checkisnull('txtArmyPosition')==false)  return false;	
		if(checkisnull('txtArmyDivision')==false)  return false;	
		if(checkisnull('txtArmyDept')==false)  return false;	
		if(checkisnull('txtDateInArmy')==false)  return false;	
		//if(FromSmallNow(document.getElementById('_ctl0_txtDateInArmy')) == false)
		if(FromSmallNowDate(document.getElementById('_ctl0_txtDateInArmy')) == false)
		{
			GetAlertError(iTotal,DSAlert,"0014");
			document.getElementById('_ctl0_txtDateInArmy').focus();			
			return false;
		}
		if(document.getElementById('_ctl0_txtDateOutArmy').value!="")
		if (compareDates(document.getElementById('_ctl0_txtDateInArmy').value,document.getElementById('_ctl0_txtDateOutArmy').value )> 0)
		{
			GetAlertError(iTotal,DSAlert,"0018");				
			document.getElementById('_ctl0_txtDateOutArmy').focus();
			return false;
		}
	}
	
	if (document.getElementById('_ctl0_chkIsCU').checked)
	{
		if(checkisnull('txtCU_StandardDate')==false)  return false;	
		//if(FromSmallNow(document.getElementById('_ctl0_txtCU_StandardDate')) == false)
		if(FromSmallNowDate(document.getElementById('_ctl0_txtCU_StandardDate')) == false)
		{
			GetAlertError(iTotal,DSAlert,"0015");
			document.getElementById('_ctl0_txtCU_StandardDate').focus();
			return false;
		}
		if (compareDates(document.getElementById('_ctl0_txtCU_Probationdate').value,document.getElementById('_ctl0_txtCU_StandardDate').value )> 0)
		{
			GetAlertError(iTotal,DSAlert,"0016");				
			document.getElementById('_ctl0_txtCU_StandardDate').focus();
			return false;
		}
		if(checkisnull('txtCU_Process')==false)  return false;	
	}
	
	if (document.getElementById('_ctl0_chkIsCYU').checked)
	{
		if(checkisnull('txtCYU_Date')==false)  return false;	
		//if(FromSmallNow(document.getElementById('_ctl0_txtCYU_Date')) == false)
		if(FromSmallNowDate(document.getElementById('_ctl0_txtCYU_Date')) == false)
		
		{
			GetAlertError(iTotal,DSAlert,"0017");
			document.getElementById('_ctl0_txtCYU_Date').focus();
			return false;
		}
	}
		
	if (document.getElementById('_ctl0_chkIsLBU').checked)
	{
		if(checkisnull('txtLBU_Description')==false)  return false;	
	}
	
	
	if (document.getElementById('_ctl0_chkIsWoundedSoldier').checked)
	{
		if(checkisnull('txtSoldierStatus')==false)  return false;	
		if(checkisnull('txtLevelSoldier')==false)  return false;	
		if(FromSmallNowDate(document.getElementById('_ctl0_txtWoundedSoldierDate')) == false)
		//if(FromSmallOrEqualToDate(document.getElementById('_ctl0_txtWoundedSoldierDate'),ToDate) == true)
		{
			GetAlertError(iTotal,DSAlert,"0099");//0099
			document.getElementById('_ctl0_txtWoundedSoldierDate').focus();
			return false;
		}
		
	}
	if (document.getElementById('_ctl0_chkIsRegime').checked)
	{
		if(checkisnull('txtRegimeDescript')==false)  return false;	
	}
	return true;
}

function collapseUnionMember()
{
	if (document.getElementById('_ctl0_chkIsCYU').checked ==false)
		document.getElementById('_ctl0_trUnionMember').style.display="none";
	else
		document.getElementById('_ctl0_trUnionMember').style.display="block";

}

function collapsePartyMember()
{
	if (document.getElementById('_ctl0_chkIsCU').checked ==false)
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
	if (document.getElementById('_ctl0_chkIsWoundedSoldier').checked ==false)
	{
		document.getElementById('_ctl0_trWoundedSoldier').style.display="none";
		document.getElementById('_ctl0_trWoundedSoldier2').style.display="none";
		document.getElementById('_ctl0_trDescript').style.display="none";
	}
	else
	{
		document.getElementById('_ctl0_trWoundedSoldier').style.display="block";
		document.getElementById('_ctl0_trWoundedSoldier2').style.display="block";
		document.getElementById('_ctl0_trDescript').style.display="block";
	}	

}
function collapseLabourUnion()
{
	if(document.getElementById('_ctl0_chkIsLBU').checked==false)
		document.getElementById('_ctl0_trLabourUnion').style.display="none";   
	else
		document.getElementById('_ctl0_trLabourUnion').style.display="block";   
}

function collapseArmy()
{
	if (document.getElementById('_ctl0_chkIsArmy').checked ==false)
	{
		document.getElementById('_ctl0_trArmy1').style.display="none";
		document.getElementById('_ctl0_trArmy2').style.display="none";
		document.getElementById('_ctl0_trArmy3').style.display="none";
	}
	else
	{
		document.getElementById('_ctl0_trArmy1').style.display="block";
		document.getElementById('_ctl0_trArmy2').style.display="block";
		document.getElementById('_ctl0_trArmy3').style.display="block";		
	}	

}
function collapseRegime()
{
	if(document.getElementById('_ctl0_chkIsRegime').checked==false)
		document.getElementById('_ctl0_trRegime').style.display="none";   
	else
		document.getElementById('_ctl0_trRegime').style.display="block";   
}
function collapseGoAbroad()
{
	if (document.getElementById('_ctl0_chkShowAbroad').checked==false)
	{		
		document.getElementById('_ctl0_trGoAbroad1').style.display="none";		
		document.getElementById('_ctl0_trGoAbroad2').style.display="none";		
	}
	else
	{
		document.getElementById('_ctl0_trGoAbroad1').style.display="block";		
		document.getElementById('_ctl0_trGoAbroad2').style.display="block";		
	}
}
function PopUp_Addnew()
{
	ShowDialog('FormPage.aspx?ModuleID=PR&ParentID=68&FunctionID=547&Ascx=MdlHR/AbroadRecord.ascx',700,330);						
	return false;
}
//-->
</script>
