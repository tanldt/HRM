<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Document.ascx.cs" Inherits="iHRPCore.MdlHR.Document" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc" TagName="EmpHeader" Src="../Include/EmpHeader.ascx" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="97%" border="0">
	<TR>
		<TD align="center"><asp:label id="lblErr" CssClass="lblErr" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD style="HEIGHT: 23px" align="center"><uc:empheader id="HR_EmpHeader" runat="server"></uc:empheader></TD>
	</TR>
	<TR>
		<TD style="HEIGHT: 23px" align="center"><asp:radiobuttonlist id="optType" onclick="javascript:MethodChange(this)" CssClass="option" runat="server"
				RepeatDirection="Horizontal" Width="158px">
				<asp:ListItem Value="0" Selected="True">Complex</asp:ListItem>
				<asp:ListItem Value="1">Simple</asp:ListItem>
			</asp:radiobuttonlist></TD>
	</TR>
	<TR>
		<TD align="center">
			<TABLE id="Table2" width="100%">
				<TR id="trComplex1">
					<TD width="15%"><asp:label id="lblDocumentType" CssClass="labelRequire" runat="server" Width="100%">Document type</asp:label></TD>
					<TD width="38%"><asp:dropdownlist id="cboLSDocumentID" CssClass="combo" runat="server" Width="95%" AutoPostBack="True"></asp:dropdownlist></TD>
					<TD width="15%"><asp:label id="lblEstablishPlace" CssClass="label" runat="server"> Establish Place</asp:label></TD>
					<TD width="38%"><asp:textbox id="txtEstablishPlace" CssClass="input" runat="server" Width="95%"></asp:textbox></TD>
				</TR>
				<TR id="trComplex2">
					<TD><asp:label id="lblDecisionNumber" CssClass="labelRequire" runat="server" Width="100%">Document No</asp:label></TD>
					<TD><asp:textbox id="txtDocumentNo" CssClass="input" runat="server" Width="95%"></asp:textbox></TD>
					<TD><asp:label id="lblSignDate" CssClass="label" runat="server">Sign date</asp:label></TD>
					<TD><asp:textbox id="txtSignDate" onblur="JavaScript:CheckDate(this)" CssClass="input" runat="server"
							Width="80%" MaxLength="10"></asp:textbox>&nbsp;<INPUT class="btnCal" onclick="ShowCalendar('_ctl0_txtSignDate')" type="button"></TD>
				</TR>
				<TR id="trComplex3">
					<TD><asp:label id="lblContent" CssClass="label" runat="server" Width="100%">Content</asp:label></TD>
					<TD colSpan="3"><asp:textbox id="txtContent" CssClass="input" runat="server" Width="98%"></asp:textbox></TD>
				</TR>
				<TR id="trComplex4">
					<TD><asp:label id="lblSubmitDate" CssClass="label" runat="server"> Submit Date</asp:label></TD>
					<TD><asp:textbox id="txtSubmitDate" onblur="JavaScript:CheckDate(this)" CssClass="input" runat="server"
							Width="80%" MaxLength="10"></asp:textbox>&nbsp;<INPUT class="btnCal" onclick="ShowCalendar('_ctl0_txtSubmitDate')" type="button"></TD>
					<TD id="tdAdToStamplbl" runat="server"><asp:checkbox id="chkAdToStamp" Runat="server"></asp:checkbox><asp:label id="lblToStamp" CssClass="label" runat="server">To Stamp</asp:label></TD>
					<TD id="tdAdToStamptxt" runat="server"><asp:dropdownlist id="cboToStamp" CssClass="combo" runat="server" Width="95%" AutoPostBack="True"></asp:dropdownlist></TD>
				</TR>
				<TR id="trComplex5">
					<TD><asp:label id="lblStorePlace" CssClass="label" runat="server">Store Place</asp:label></TD>
					<TD colSpan="3"><asp:textbox id="txtStorePlace" CssClass="input" runat="server" Width="98%"></asp:textbox></TD>
				</TR>
				<TR id="trComplex6">
					<TD><asp:label id="lblNote" CssClass="label" runat="server">Note</asp:label></TD>
					<TD colSpan="3"><asp:textbox id="txtNote" CssClass="input" runat="server" Width="98%"></asp:textbox></TD>
				</TR>
				<TR id="trSimple1" style="DISPLAY: none">
					<TD align="left" colSpan="4"><asp:checkboxlist id="ChkDocumentType" CssClass="checkbox" runat="server" Width="100%">
							<asp:ListItem Value="1">L&#253; lịch</asp:ListItem>
							<asp:ListItem Value="2">Học bạ</asp:ListItem>
							<asp:ListItem Value="3">Bằng Đại học</asp:ListItem>
							<asp:ListItem Value="4">Giấy kh&#225;m sức khỏe</asp:ListItem>
							<asp:ListItem Value="5">Chức nhận qu&#226;n sự</asp:ListItem>
							<asp:ListItem Value="6">Kh&#225;c</asp:ListItem>
						</asp:checkboxlist></TD>
				</TR>
				<TR id="trSimple2" style="DISPLAY: none">
					<TD width="10%"><asp:label id="lblStorePlace_S" CssClass="label" runat="server" Width="100%">Store Place</asp:label></TD>
					<TD colSpan="3"><asp:textbox id="txtStorePlace_S" CssClass="input" runat="server" Width="98%"></asp:textbox></TD>
				</TR>
				<TR>
					<TD align="center" colSpan="4">
						<HR align="center" width="100%">
						&nbsp;</TD>
				</TR>
				<TR>
					<TD align="center" colSpan="4"><asp:linkbutton id="btnAddnew" accessKey="N" CssClass="btnAddnew" runat="server" ToolTip="Alt+N">Add New</asp:linkbutton>&nbsp;&nbsp;
						<asp:linkbutton id="btnSave" accessKey="S" CssClass="btnSave" runat="server" ToolTip="ALT+S">Save</asp:linkbutton>&nbsp;&nbsp;
						<asp:linkbutton id="btnDelete" accessKey="D" CssClass="btnDelete" runat="server" ToolTip="Alt + D">Delete</asp:linkbutton>&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnList" accessKey="L" CssClass="btnList" runat="server" ToolTip="Alt + L">List</asp:linkbutton>&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="btnExport" accessKey="E" CssClass="btnExport" runat="server" ToolTip="Alt+E, Xuất dl trên lưới ra file Excel"> Export</asp:linkbutton></TD>
				</TR>
				<TR>
					<TD align="center" colSpan="4" height="10"></TD>
				</TR>
				<TR id="trList">
					<TD align="center" colSpan="4"><asp:datagrid id="dtgList" CssClass="grid" runat="server" Width="100%" AllowPaging="True" BackColor="White"
							BorderColor="#3366CC" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False" AllowSorting="True">
							<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
							<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
							<ItemStyle CssClass="gridItem"></ItemStyle>
							<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="&lt;b&gt;#&lt;/b&gt;">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<%# Container.ItemIndex + 1%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:ButtonColumn DataTextField="Type" HeaderText="Document Type">
									<HeaderStyle Width="20%"></HeaderStyle>
								</asp:ButtonColumn>
								<asp:BoundColumn HeaderText="Establish Place">
									<HeaderStyle Width="25%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn HeaderText="Sign date">
									<HeaderStyle Width="15%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn HeaderText="SubmitDate">
									<HeaderStyle Width="15%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn HeaderText="StorePlace">
									<HeaderStyle Width="15%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Select">
									<HeaderStyle Width="7%"></HeaderStyle>
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
	</TR> <!-- end button for input form --></TABLE>
<script language="javascript">
function MethodChange(obj){	
		if(document.getElementById(obj.id + '_0').checked==true){
			document.getElementById('trComplex1').style.display = "block";			
			document.getElementById('trComplex2').style.display = "block";			
			document.getElementById('trComplex3').style.display = "block";			
			document.getElementById('trComplex4').style.display = "block";			
			document.getElementById('trComplex5').style.display = "block";			
			document.getElementById('trComplex6').style.display = "block";			
			document.getElementById('trSimple1').style.display = "none";			
			document.getElementById('trSimple2').style.display = "none";		
			document.getElementById('_ctl0_btnAddnew').style.display="block"		
			document.getElementById('_ctl0_btnDelete').style.display="block"		
			document.getElementById('_ctl0_btnExport').style.display="block"			
			document.getElementById('trList').style.display="block"
		}
		else if(document.getElementById(obj.id + '_1').checked==true){
			document.getElementById('trComplex1').style.display = "none";			
			document.getElementById('trComplex2').style.display = "none";			
			document.getElementById('trComplex3').style.display = "none";			
			document.getElementById('trComplex4').style.display = "none";			
			document.getElementById('trComplex5').style.display = "none";			
			document.getElementById('trComplex6').style.display = "none";			
			document.getElementById('trSimple1').style.display = "block";			
			document.getElementById('trSimple2').style.display = "block";			
			document.getElementById('_ctl0_btnAddnew').style.display="none"					
			document.getElementById('_ctl0_btnDelete').style.display="none"		
			document.getElementById('_ctl0_btnExport').style.display="none"		
			document.getElementById('trList').style.display="none"
		}
		else{
			document.getElementById('complex').style.display = "block";			
			document.getElementById('simple').style.display = "none";			
		}
	}
</script>
