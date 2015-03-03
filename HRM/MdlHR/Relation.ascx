<%@ Register TagPrefix="uc" TagName="EmpHeader" Src="../Include/EmpHeader.ascx"%>
<%@ Register TagPrefix="uc1" TagName="Address" Src="../Include/Address.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Relation.ascx.cs" Inherits="iHRPCore.MdlHR.Relation" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<script language="jscript" src='../Include/common.js"'></script>
<script language="javascript">					
	function ShowEmpCompany()
	{	
		
		if (document.getElementById("_ctl0_chkSameCompany").checked == true)
		{			
			document.getElementById("_ctl0_trEmpID").style.display = "block" ;						
			document.getElementById("_ctl0_txtLastName").disabled=true;
			document.getElementById("_ctl0_txtFirstName").disabled=true;							
			document.getElementById('_ctl0_txtDOB').disabled=true;
			document.getElementById('_ctl0_cboGender').disabled=true;
			document.getElementById('_ctl0_cboStatus').disabled=true;
			document.getElementById('_ctl0_txtIDNo').disabled=true;
		}
		else 
		{				
			document.getElementById("_ctl0_trEmpID").style.display = "none" ;						
			document.getElementById("_ctl0_txtLastName").disabled=false;
			document.getElementById("_ctl0_txtFirstName").disabled=false;							
			document.getElementById('_ctl0_txtDOB').disabled=false;
			document.getElementById('_ctl0_cboGender').disabled=false;
			document.getElementById('_ctl0_cboStatus').disabled=false;
			document.getElementById('_ctl0_txtIDNo').disabled=false;
			document.getElementById("_ctl0_txtRelativeEmpID").value='';
			document.getElementById("_ctl0_txtLastName").value='';
			document.getElementById("_ctl0_txtFirstName").value='';
			document.getElementById('_ctl0_txtDOB').value='';
			document.getElementById('_ctl0_cboGender').value='';
			document.getElementById('_ctl0_cboStatus').value='';
			document.getElementById('_ctl0_txtIDNo').value='';
		}			
	}
	
	function ShowReduction()
	{
		if (document.getElementById("_ctl0_chkReductFamily").checked == true)
		{
			document.getElementById("_ctl0_trReduction").style.display="";
		}else
		{
			document.getElementById("_ctl0_trReduction").style.display="none";
		}
	}	
	function MyCheckDate(txtDOBID,Now,txtAgeID,lstDiffAgesId,cboLSRelationshipID)
	{		
		var field=document.getElementById(txtDOBID);		
		
		if (parseInt(field.value) <= 1900)		
		{	
			GetAlertError(iTotal,DSAlert,"0036");
			document.getElementById('_ctl0_txtDOB').value = "";
			document.getElementById('_ctl0_txtDOB').focus();				
			return;
		}
		if(field.value!="")
		{
			if(checkInt2(field)==true)
				return CalculateAge(txtDOBID,Now,txtAgeID,lstDiffAgesId,cboLSRelationshipID);
			else { document.getElementById(txtAgeID).value=''; return false; }
		}
		var obj=document.getElementById('_ctl0_txtFirstName');
		if(obj.value=="") { alert('Please enter First Name'); return false; }
		obj=document.getElementById('_ctl0_txtLastName');
		if(obj.value=="") { alert('Please enter Last Name'); return false; }
		obj=document.getElementById('_ctl0_cboLSRelationshipID');
		if(obj.value=="") { alert('Please select Relation type'); return false; }
	}
	function CalculateAge(DOBID,Now,AgeID,lstDiffAgesId,cboRelationId)
	{	
		var cboRelation=document.getElementById(cboRelationId);			
		var lstDA=document.getElementById(lstDiffAgesId);			
		var selectedCode=cboRelation.options[cboRelation.selectedIndex].value;
		
		Now=Now.split('/');
		Now=Now[2];
		
		var objDOB=document.getElementById(DOBID);
		var DOBRel='01/01/' + objDOB.value;
		var arrDOBRel=DOBRel.split('/');
		var iDOBRel=arrDOBRel[2]; // DOB RELATIVE YEAR
		
		var d=new Date(iDOBRel,arrDOBRel[1],arrDOBRel[0]);
		var dnow=new Date();		
		if(dnow<d)
		{
			GetAlertError(iTotal,DSAlert,"RE_0001");
			objDOB.value='';
			objDOB=document.getElementById(AgeID);
			objDOB.value='';
			return false;
		}	
		else
		{
			objDOB=document.getElementById(AgeID);
			objDOB.value=dnow.getFullYear()-d.getFullYear();
		}
				
		
		for(var i=0;i<lstDA.length;i++)
		{
			if(lstDA.options[i].value==selectedCode)
			{				
				if(lstDA.options[i].innerText!="")
				{
					var strDiffAgeAndDOB=lstDA.options[i].innerText;
					var arrDiffAge=strDiffAgeAndDOB.split(',');
					var iDiffAge=0;
					if(arrDiffAge[0]!="") iDiffAge=arrDiffAge[0]; // DIFFAGE
					
					var iDOB=arrDiffAge[1];					
					if(iDOB=="")
					{
						//GetAlertError(iTotal,DSAlert,"RE_0004");
						return true;
					}
					var arrDOB=iDOB.split('/');
					iDOB=arrDOB[2]; // EmpID YEAR
					arrDOB=iDOB.split(' ');
					iDOB=arrDOB[0];
					
					var obj=document.getElementById(DOBID);
					var DOBRelative=obj.value;
					arrDOB=DOBRelative.split('/');
					var iDOBRelative=arrDOB[2]; // DOB RELATIVE YEAR					
					
					if(iDiffAge>0)
					{
						if(iDOB-iDOBRelative<iDiffAge && iDiffAge!=0)
						{
							alert(GetAlertText(iTotal,DSAlert,"RE_0003") +iDOB+' - '+iDiffAge+' ( year '+(iDOB-iDiffAge)+' )'); 
							obj.value='';
							obj=document.getElementById(AgeID);
							obj.value='';
							return false;
						}
					}
					else
					{
						if(iDOB-iDOBRelative>iDiffAge && iDiffAge!=0)
						{
							alert(GetAlertText(iTotal,DSAlert,"RE_0002") + iDOB); 
							obj.value='';
							obj=document.getElementById(AgeID);
							obj.value='';
							return false;
						}
					}					
				}
				return true;
			}
		}							
		return true;
	}
	function MyOpenWindowEmp(strField)
	{
		var strUrl;
		//Neu goi form popup dua vao ma nhan vien
		if (strField == "EmpID")
		{
			//Neu ma nhan vien trung voi ma nhan vien cua nhan vien dang duoc chon, mo trang popup
			if (trim(document.getElementById("_ctl0:HR_EmpHeader:txtEmpID").value) == trim(document.getElementById("_ctl0:HR_EmpHeader:txtEmpID").title))
				strUrl = 'MdlHR/EmpPopup.ascx';
			else
			{
				//Neu chieu dai ma nhan vien = chieu dai qui dinh cua ma nhan vien
				if(parseInt(document.getElementById("_ctl0:HR_EmpHeader:txtEmpID").value.length) == parseInt(document.getElementById("_ctl0:HR_EmpHeader:txtLengthEmpID").value))
					//Truyen tham so return trong truong hop xac dinh ma nhan vien chinh xac
					strUrl = 'MdlHR/EmpPopup.ascx&EmpID=' + trim(document.getElementById("_ctl0:HR_EmpHeader:txtEmpID").value) + '&Return=1';
				else
					strUrl = 'MdlHR/EmpPopup.ascx&EmpID=' + trim(document.getElementById("_ctl0:HR_EmpHeader:txtEmpID").value);
			}
		}
		//Neu goi form popup dua vao ten nhan vien
		else if (strField == "EmpName")
		{
			if (trim(document.getElementById("_ctl0:HR_EmpHeader:txtEmpName").value) == trim(document.getElementById("_ctl0:HR_EmpHeader:txtEmpName").title))
				strUrl = 'MdlHR/EmpPopup.ascx';
			else
			{
				strUrl = 'MdlHR/EmpPopup.ascx&EmpName=' + trim(document.getElementById("_ctl0:HR_EmpHeader:txtEmpName").value);
			}
		}
		CurWindow = window.open('FormPage.aspx?Params=3&Ascx=' + strUrl,'SearchEmp','toolbar=0,status=no,menubar=no,scrollbars=1,resizable=yes,copyhistory=yes,width=650,height=500,align=top',true);
		CurWindow.focus();
		return false;
	} 
	
	function ReturnEmpPopUp3(strEmpID, strEmpCode, strEmpName)
	{
		var obj=document.getElementById('_ctl0_txtEmpIDHidden');						
		obj.value=strEmpID;
		document.getElementById('_ctl0_txtRelativeEmpCode').value=strEmpCode;
		obj=document.getElementById('_ctl0_btnSubmit');
		obj.click();
	}		
				
	function ValidForm(txtDOBId,Now,txtAgeID,lstDiffAgesId)
	{
			
	if(checkisnull('cboLSRelationshipID')==false)  return false;	
	
	
	if (document.getElementById("_ctl0_chkSameCompany").checked == true	)
	{
		if (checkisnull('txtRelativeEmpID')==false) return false;
	}else
	{
		if(checkisnull('txtLastName')==false)  return false;		
		if(checkisnull('txtFirstName')==false)  return false;
		if(checkisnull('txtDOB')==false)  return false;		
	}
			
	if(document.getElementById("_ctl0_chkReductFamily").checked == true)
	{
		
		if(checkisnull('txtReductionFrom')==false)  return false;		
		//if(checkisnull('txtReductionTo')==false)  return false;
		if (document.getElementById("_ctl0_txtReductionTo").value != "" && FromSmallToDate(document.getElementById('_ctl0_txtReductionFrom'),document.getElementById('_ctl0_txtReductionTo') ) == false)
		{
			GetAlertError(iTotal,DSAlert,"0007");			
			document.getElementById('_ctl0_txtReductionTo').focus();				
			return false;
		}
	}
	if(FromSmallNow(document.getElementById('_ctl0_txtIssueDate_IDNo')) == false)
		{
			GetAlertError(iTotal,DSAlert,"0005");
			document.getElementById('_ctl0_txtIssueDate_IDNo').focus();
			return false;
		}
	//Passport		
	if(FromSmallNow(document.getElementById('_ctl0_txtIssueDate_Pass')) == false)
		{
			GetAlertError(iTotal,DSAlert,"0005");
			document.getElementById('_ctl0_txtIssueDate_Pass').focus();
			return false;
		}
		if (!checkempty('txtIssueDate_Pass')&& !checkempty('txtEffectiveDate_Pass'))
		{
			
			if (FromSmallToDate(document.getElementById('_ctl0_txtEffectiveDate_Pass'),document.getElementById('_ctl0_txtIssueDate_Pass') )> 0)
			{
				GetAlertError(iTotal,DSAlert,"0020");				
				document.getElementById('txtEffectiveDate_Pass').focus();
				return false;
			}
		}
		//
		if (!checkempty('txtEffectiveDate_Pass')&& !checkempty('txtExpiredDate_Pass'))
		{		
			if (FromSmallToDate(document.getElementById('_ctl0_txtExpiredDate_Pass'),document.getElementById('_ctl0_txtEffectiveDate_Pass') )> 0)
			{
				GetAlertError(iTotal,DSAlert,"0021");				
				document.getElementById('_ctl0_txtEffectiveDate_Pass').focus();
				return false;
			}
		}
		
		//document.getElementById('_ctl0_txtYYYY').value = document.getElementById('_ctl0_txtDOB').value;
	}
	function checkempty(obj)
	{
		if(document.getElementById('_ctl0_' + obj).value=="")
			return true;			
		else
			return false;
	}
	function checkisnull(obj)
	{
		if(document.getElementById('_ctl0_' + obj).value=="")
			{
				//alert('Vui lòng di?n d?y d? d? li?u yêu c?u!');
				GetAlertError(iTotal,DSAlert,"0003");
				document.getElementById('_ctl0_' + obj).focus();
				return false;
			}
		else
			return true;
	}
	function checkdelete()
	{
		if(GridCheck('_ctl0_dtgList',3,1,'chkSelect')==false)
		{
		GetAlertError(iTotal,DSAlert,"0001");				
			return false;
		}
		if(confirm(GetAlertText(iTotal,DSAlert,"0002"))==false){
			return false;
		}
	}
				
</script>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="98%" border="0">
	<TR>
		<TD colSpan="5"><asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center" colSpan="5"><uc:empheader id="HR_EmpHeader" runat="server"></uc:empheader></TD>
	</TR>
	<tr>
		<TD align="center"><!-- start detail for input form -->
			<TABLE id="tblInfo" cellSpacing="0" cellPadding="1" width="98%" border="0">
				<TR>
					<TD width="10%"><asp:label id="lblRelationID" runat="server" CssClass="labelRequire" Width="100%">Relationship</asp:label></TD>
					<TD width="38%"><asp:dropdownlist id="cboLSRelationshipID" runat="server" CssClass="select" Width="100%"></asp:dropdownlist></TD>
					<TD width="1%"></TD>
					<TD width="10%"></TD>
					<TD width="20%"><asp:checkbox id="chkSameCompany" runat="server" CssClass="checkbox" Text="Same Company?"></asp:checkbox></TD>
				</TR>
				<TR id="trEmpID" style="DISPLAY: none" runat="server">
					<td><asp:label id="Label14" runat="server" CssClass="labelRequire" Width="100%">Emp Code</asp:label></td>
					<td><asp:textbox id="txtRelativeEmpCode" runat="server" CssClass="input" Width="92%" MaxLength="255"
							ReadOnly="True"></asp:textbox><asp:button id="btnEmpPopup" runat="server" CssClass="search" Text="..." Height="20px"></asp:button></td>
					<td></td>
					<td></td>
					<td></td>
				</TR>
				<TR>
					<TD><asp:label id="lblLastName" runat="server" CssClass="labelRequire" Width="100%">Last Name</asp:label></TD>
					<TD><asp:textbox id="txtLastName" runat="server" CssClass="input" Width="100%" MaxLength="35"></asp:textbox></TD>
					<TD></TD>
					<TD><asp:label id="lblFirstName" runat="server" CssClass="labelRequire" Width="100%">First Name</asp:label></TD>
					<TD><asp:textbox id="txtFirstName" runat="server" CssClass="input" Width="100%" MaxLength="15"></asp:textbox></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblGender" runat="server" CssClass="label" Width="100%">Gender</asp:label></TD>
					<TD><asp:dropdownlist id="cboGender" runat="server" CssClass="select" Width="50px"></asp:dropdownlist><asp:label id="lblRelationStatus" runat="server" CssClass="label">Status</asp:label>&nbsp;<asp:dropdownlist id="cboStatus" runat="server" CssClass="select" Width="69px"></asp:dropdownlist>&nbsp;&nbsp;&nbsp;</TD>
					<TD></TD>
					<TD><asp:label id="Label4" runat="server" CssClass="labelRequire" Width="100%">YOB</asp:label></TD>
					<TD><asp:textbox id="txtDOB" runat="server" CssClass="input" Width="75px" MaxLength="4"></asp:textbox>&nbsp;<INPUT class="btnCal" style="DISPLAY: none" onclick="ShowCalendar('_ctl0_txtDOB')" type="button">&nbsp;
						<asp:label id="lblAge" runat="server" CssClass="label">Age</asp:label>&nbsp;
						<asp:textbox id="txtAge" runat="server" CssClass="input" Width="37px" ReadOnly="True"></asp:textbox></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblIDCardNo" runat="server" CssClass="label">ID No</asp:label></TD>
					<TD><asp:textbox id="txtIDNo" runat="server" CssClass="input" Width="87px" MaxLength="20"></asp:textbox>&nbsp;
						<asp:label id="Label9" runat="server" CssClass="label">Issue date</asp:label><asp:textbox id="txtIssueDate_IDNo" onblur="JavaScript:CheckDate(this)" runat="server" CssClass="input"
							Width="80px" MaxLength="10"></asp:textbox><INPUT class=btnCal onclick="javascript:popUpCalendar(<%=this.txtIssueDate_IDNo.ClientID%>);" type=button></TD>
					<TD></TD>
					<TD><asp:label id="Label10" runat="server" CssClass="label">Issue Place</asp:label></TD>
					<TD><asp:textbox id="txtIssuePlace_IDNo" runat="server" CssClass="input" Width="100%" MaxLength="150"></asp:textbox></TD>
				</TR>
				<TR>
					<TD colSpan="5">
						<HR width="100%" SIZE="1">
					</TD>
				</TR>
				<TR>
					<TD><asp:label id="Label11" runat="server" CssClass="label">ID Passport</asp:label></TD>
					<TD><asp:textbox id="txtPassNo" runat="server" CssClass="input" Width="87px" MaxLength="20"></asp:textbox>&nbsp;&nbsp;&nbsp;</TD>
					<TD></TD>
					<TD><asp:label id="Label12" runat="server" CssClass="label">Issue date</asp:label></TD>
					<TD><asp:textbox id="txtIssueDate_Pass" onblur="JavaScript:CheckDate(this)" runat="server" CssClass="input"
							Width="80px" MaxLength="10"></asp:textbox><INPUT class=btnCal onclick="javascript:popUpCalendar(<%=this.txtIssueDate_Pass.ClientID%>);" type=button></TD>
				</TR>
				<TR>
					<TD><asp:label id="Label13" runat="server" CssClass="label">Effective date</asp:label></TD>
					<TD><asp:textbox id="txtEffectiveDate_Pass" onblur="JavaScript:CheckDate(this)" runat="server" CssClass="input"
							Width="80px" MaxLength="10"></asp:textbox><INPUT class=btnCal onclick="javascript:popUpCalendar(<%=this.txtEffectiveDate_Pass.ClientID%>);" type=button></TD>
					<TD></TD>
					<TD><asp:label id="Label15" runat="server" CssClass="label">Expired date</asp:label></TD>
					<TD><asp:textbox id="txtExpiredDate_Pass" onblur="JavaScript:CheckDate(this)" runat="server" CssClass="input"
							Width="80px" MaxLength="10"></asp:textbox><INPUT class=btnCal onclick="javascript:popUpCalendar(<%=this.txtExpiredDate_Pass.ClientID%>);" type=button></TD>
				</TR>
				<TR>
					<TD colSpan="5">
						<HR width="100%" SIZE="1">
					</TD>
				</TR>
				<TR>
					<TD><asp:label id="Label7" runat="server" CssClass="label" Width="100%">Telephone</asp:label></TD>
					<TD><asp:textbox id="txtTelephone" runat="server" CssClass="input" Width="30%" MaxLength="100" Rows="2"></asp:textbox></TD>
					<TD></TD>
					<TD><asp:label id="Label8" runat="server" CssClass="label" Width="100%">Blood type</asp:label></TD>
					<TD><asp:dropdownlist id="cboLSBloodTypeID" runat="server" CssClass="combo" Width="100%">
							<asp:ListItem Value=""></asp:ListItem>
							<asp:ListItem Value="O">O</asp:ListItem>
							<asp:ListItem Value="A">A</asp:ListItem>
							<asp:ListItem Value="B">B</asp:ListItem>
							<asp:ListItem Value="AB">AB</asp:ListItem>
						</asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD><asp:label id="Label5" runat="server" CssClass="label" Width="100%">Address</asp:label></TD>
					<TD><asp:textbox id="txtAddress" runat="server" CssClass="input" Width="100%" MaxLength="150"></asp:textbox></TD>
					<TD></TD>
					<TD><asp:label id="Label6" runat="server" CssClass="label" Width="100%">Contact</asp:label></TD>
					<TD><asp:textbox id="txtContact" runat="server" CssClass="input" Width="100%" MaxLength="150"></asp:textbox></TD>
				</TR>
				<TR id="trAdBefore" style="DISPLAY: none" runat="server">
					<TD><asp:checkbox id="chkAdBefore" runat="server" CssClass="chkAdmin" Width="10%" Checked="True"></asp:checkbox><asp:label id="Label1" runat="server" CssClass="label" Width="100%">Before 75</asp:label></TD>
					<TD vAlign="top"><asp:textbox id="txtBefore75" style="POSITION: absolute" runat="server" CssClass="input" Width="100%"
							MaxLength="100"></asp:textbox></TD>
					<TD></TD>
					<TD><asp:label id="Label2" runat="server" CssClass="label" Width="100%">After 75</asp:label></TD>
					<TD vAlign="top"><asp:textbox id="txtAfter75" style="POSITION: absolute" runat="server" CssClass="input" Width="100%"
							MaxLength="100"></asp:textbox></TD>
				</TR>
				<TR>
					<TD><asp:label id="Label3" runat="server" CssClass="label" Width="100%">Occupation</asp:label></TD>
					<TD vAlign="top"><asp:textbox id="txtOccupation" style="POSITION: absolute" runat="server" CssClass="input" Width="100%"
							MaxLength="150"></asp:textbox></TD>
					<TD></TD>
					<TD><asp:label id="lblContact" runat="server" CssClass="label" Width="100%">Work Place</asp:label></TD>
					<TD vAlign="top"><asp:textbox id="txtWorkPlace" style="POSITION: absolute" runat="server" CssClass="input" Width="100%"
							MaxLength="255" Rows="2"></asp:textbox></TD>
				</TR>
				<TR>
					<TD vAlign="top"><asp:label id="lblNote" runat="server" CssClass="label" Width="100%">Note</asp:label></TD>
					<TD vAlign="top" colSpan="4"><asp:textbox id="txtNote" runat="server" CssClass="input" Width="100%" MaxLength="255"></asp:textbox></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD><asp:checkbox id="chkReductFamily" runat="server" CssClass="checkbox" Text="Deduction PIT?"></asp:checkbox></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
				</TR>
				<TR id="trReduction" style="DISPLAY: none" runat="server">
					<TD><asp:label id="Label16" runat="server" CssClass="label" Width="100%">Deduction From</asp:label></TD>
					<TD><asp:textbox id="txtReductionFrom" runat="server" onblur="JavaScript:CheckDate(this)" CssClass="input"
							Width="30%" MaxLength="255" Rows="2"></asp:textbox><INPUT class=btnCal onclick="javascript:popUpCalendar(<%=this.txtReductionFrom.ClientID%>);" type=button></TD>
					<TD></TD>
					<TD><asp:label id="Label17" runat="server" CssClass="label" Width="100%">Deduction To</asp:label></TD>
					<TD><asp:textbox id="txtReductionTo" runat="server" onblur="JavaScript:CheckDate(this)" CssClass="input"
							Width="50%" MaxLength="255" Rows="2"></asp:textbox><INPUT class=btnCal onclick="javascript:popUpCalendar(<%=this.txtReductionTo.ClientID%>);" type=button></TD>
				</TR>
				<TR>
				</TR>
			</TABLE> <!-- end button for input form --></TD>
	</tr>
	<TR>
		<TD vAlign="top" align="center" height="20">
			<HR align="center" width="95%">
			&nbsp;
			<asp:textbox id="txtYear" style="DISPLAY: none" runat="server"></asp:textbox></TD>
	</TR>
	<TR>
		<TD noWrap align="center">
			<TABLE id="tblButton" cellSpacing="0" cellPadding="0" width="99%" border="0">
				<TR vAlign="top" height="30">
					<TD vAlign="middle" width="15%"><asp:checkbox id="chkShowGrid" accessKey="G" onclick="javascript:collapse('tblGrid');" CssClass="checkbox"
							runat="server" Text="Show grid" Checked="True" ToolTip="Alt+G"></asp:checkbox></TD>
					<TD align="center" width="*"><asp:linkbutton id="btnAddNew" accessKey="N" runat="server" CssClass="btnAddnew" ToolTip="Alt+N"
							CausesValidation="False">Add New</asp:linkbutton>&nbsp;&nbsp;
						<asp:linkbutton id="btnSave" accessKey="S" CssClass="btnSave" runat="server" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp;&nbsp;
						<asp:linkbutton id="btnDelete" accessKey="D" runat="server" CssClass="btnExport" ToolTip="Alt + D"
							CausesValidation="False">Delete</asp:linkbutton>&nbsp;&nbsp;
						<asp:linkbutton id="btnList" accessKey="D" runat="server" CssClass="btnList" ToolTip="Alt + D" CausesValidation="False">List</asp:linkbutton>&nbsp;&nbsp;
						<asp:linkbutton id="btnExport" accessKey="E" CssClass="btnExport" runat="server" ToolTip="Alt+E, Export data in grid to excel file"> Export</asp:linkbutton></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<!-- end button for input form -->
	<TR>
		<TD align="center"><!-- start grid for input form -->
			<TABLE id="tblGrid" cellSpacing="0" cellPadding="1" width="98%" border="0">
				<TR id="trGrid"> <!-- start grid detail for input form -->
					<TD colSpan="2"><asp:datagrid id="dtgList" runat="server" CssClass="grid" Width="100%" BackColor="White" AllowSorting="True"
							AutoGenerateColumns="False" CellPadding="0" AllowPaging="True" BorderWidth="1px" BorderColor="#3366CC">
							<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle HorizontalAlign="Center" CssClass="gridItem"></ItemStyle>
							<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="RelativeID" HeaderText="RelativeID">
									<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Select">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox id="chkSelect" CssClass="checkbox" runat="server"></asp:CheckBox>
									</ItemTemplate>
									<FooterTemplate>
										<asp:CheckBox id="chkSelectAll" CssClass="checkbox" runat="server"></asp:CheckBox>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="&lt;b&gt;#&lt;/b&gt;">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<%# Container.ItemIndex + 1%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:ButtonColumn DataTextField="Name" HeaderText="Relationship" CommandName="LoadRelative">
									<HeaderStyle Width="20%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:ButtonColumn>
								<asp:BoundColumn DataField="FullName" HeaderText="Full Name">
									<HeaderStyle Width="30%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="DOB_" HeaderText="Year of birth" DataFormatString="{0:dd/MM/yyyy}">
									<HeaderStyle Width="20%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="StatusName" HeaderText="Status">
									<HeaderStyle Width="20%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE> <!-- end grid for input form --></TD>
	</TR>
	<TR>
		<TD style="DISPLAY: none" align="center"><asp:textbox id="txtRelativeEmpID" runat="server" CssClass="input" Width="92%" MaxLength="255"
				ReadOnly="True"></asp:textbox></TD>
	</TR>
</TABLE>
<asp:textbox id="txtRelationshipID" runat="server" Visible="False"></asp:textbox><asp:textbox id="txtRelativeID" runat="server" Visible="False"></asp:textbox><asp:textbox id="txtRelationType" runat="server" Visible="False"></asp:textbox><SELECT id="lstDiffAges" style="DISPLAY: none" size="2" name="lstDiffAges" runat="server">
	<OPTION></OPTION>
</SELECT>
<INPUT id="btnSubmit" style="VISIBILITY: hidden" type="submit" value="Submit" name="btnSubmit"
	runat="server"> <INPUT id="txtEmpIDHidden" type="hidden" name="Hidden1" runat="server"><INPUT id="txtYYYY" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtYYYY"
	runat="server">
<script language="javascript">
	ShowReduction();
</script>
