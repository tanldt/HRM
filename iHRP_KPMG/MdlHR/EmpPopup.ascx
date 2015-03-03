<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../Include/EmpHeaderSearch.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ColumnList" Src="../Include/ColumnList.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="EmpPopup.ascx.cs" Inherits="iHRPCore.MdlHR.EmpPopup" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="98%" border="0">
	<TR>
		<TD align="center"><asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center"><uc1:empheadersearch id="EmpHeaderSearch1" runat="server"></uc1:empheadersearch></TD>
	</TR>
	<TR>
		<TD vAlign="top" noWrap align="center" height="20">
			<HR align="center" width="100%">
		</TD>
	</TR>
	<TR>
		<TD noWrap align="center">
			<TABLE id="tblButton" cellSpacing="0" cellPadding="0" width="99%" border="0">
				<TR vAlign="top">
					<TD align="center" width="*" colSpan="2"><asp:linkbutton id="btnSearch" accessKey="S" runat="server" CssClass="btnSearch" ToolTip="Alt+S"
							DESIGNTIMEDRAGDROP="16"> Search</asp:linkbutton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:linkbutton id="butExport" accessKey="E" runat="server" CssClass="btnExport" ToolTip="Alt+E, Export data in grid to excel file"> Export</asp:linkbutton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<INPUT class="btnClose" id="btnClose1" accessKey="C" onclick="javascript:window.close();"
							type="button" value="Close" name="btnShowDialog" runat="server">
					</TD>
				<TR>
					<TD colSpan="2">
						<table width="100%">
							<TR>
								<TD align="center"><!-- start grid for list form -->
									<TABLE id="tblGrid" cellSpacing="0" cellPadding="1" width="99%" border="0">
										<TR>
											<TD id="tdPages" width="43%"><uc1:columnlist id="uctrlColumns" runat="server"></uc1:columnlist></TD>
											<TD id="tdSort" style="DISPLAY: none" align="right" width="*"><asp:label id="Label8" runat="server" CssClass="labelRight" Width="30px">  Col 1</asp:label><asp:dropdownlist id="cboCol1" runat="server" CssClass="combo" Width="80px"></asp:dropdownlist><asp:label id="Label10" runat="server" CssClass="labelRight" Width="30px">Col 2</asp:label>&nbsp;
												<asp:dropdownlist id="cboCol2" runat="server" CssClass="combo" Width="80px"></asp:dropdownlist><asp:label id="Label15" runat="server" CssClass="labelRight" Width="30px">Col 3</asp:label>&nbsp;
												<asp:dropdownlist id="cboCol3" runat="server" CssClass="combo" Width="80px"></asp:dropdownlist></TD>
										</TR>
										<TR id="trGrid"> <!-- start grid detail for list form -->
											<TD colSpan="2">
												<DIV id="divList" style="OVERFLOW: auto; HEIGHT: 140px" runat="server"><asp:datagrid id="dtgGridSelect" runat="server" CssClass="grid" Width="100%" BackColor="White"
														BorderColor="#3366CC" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False" AllowSorting="True" PageSize="20" AllowPaging="True">
														<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
														<ItemStyle CssClass="gridItem"></ItemStyle>
														<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
														<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
														<Columns>
															<asp:BoundColumn Visible="False" DataField="EmpID">
																<HeaderStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Middle"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn Visible="False" DataField="EmpCode">
																<HeaderStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Middle"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn Visible="False" DataField="EmpName" HeaderText="Emp Name">
																<HeaderStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Middle"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn Visible="False" DataField="StartDateStr" HeaderText="Ng&#224;y B&#208; v&#224;o LH">
																<HeaderStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Middle"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn Visible="False" DataField="VLastName" HeaderText="Last Name">
																<HeaderStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Middle"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn Visible="False" DataField="VFirstName" HeaderText="First Name">
																<HeaderStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Middle"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn Visible="False" DataField="LocationName" HeaderText="Location Name">
																<HeaderStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Middle"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn Visible="False" DataField="CompanyName" HeaderText="Company Name">
																<HeaderStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Middle"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn Visible="False" DataField="Level1Name" HeaderText="Company">
																<HeaderStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Middle"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn Visible="False" DataField="Level2Name" HeaderText="Department">
																<HeaderStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Middle"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn Visible="False" DataField="Level3Name" HeaderText="Group">
																<HeaderStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Middle"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn Visible="False" DataField="PositionName" HeaderText="Position">
																<HeaderStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Middle"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:BoundColumn>
															<asp:TemplateColumn HeaderText="&lt;b&gt;Seq&lt;/b&gt;">
																<HeaderStyle Width="5%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
																<ItemTemplate>
																	<%# Container.ItemIndex + 1 + dtgGridSelect.PageSize*dtgGridSelect.CurrentPageIndex%>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="EmpCode" HeaderText="Emp Code">
																<HeaderStyle Width="10%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
																<ItemTemplate>
																	<asp:LinkButton id=hpLink CssClass="Hlink" Width="95%" Text='<%# DataBinder.Eval(Container, "DataItem.EmpCode") %>' Runat="server" CommandName="hpLink">
																	</asp:LinkButton>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:BoundColumn DataField="EmpCode" HeaderText="Emp Code">
																<HeaderStyle Width="10%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="EmpName" SortExpression="EmpName" HeaderText="Emp Name">
																<HeaderStyle Width="17%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Left"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="Level1Name" SortExpression="Level1Name" HeaderText="Company">
																<HeaderStyle Width="7%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Left"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="Level2Name" SortExpression="Level2Name" HeaderText="Department">
																<HeaderStyle Width="13%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Left"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="Level3Name" SortExpression="Level3Name" HeaderText="Group">
																<HeaderStyle Width="13%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Left"></ItemStyle>
															</asp:BoundColumn>
															<asp:TemplateColumn Visible="False" HeaderText="Select">
																<HeaderStyle Width="7%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
																<HeaderTemplate>
																	<asp:CheckBox id="chkSelectAll" runat="server" CssClass="checkbox" onclick="CheckAll('_ctl0_dtgGridSelect__ctl2_chkSelectAll','_ctl0_dtgGridSelect',3,1,'chkSelect')"></asp:CheckBox>
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:CheckBox id="chkSelect" runat="server" CssClass="checkbox"></asp:CheckBox>
																</ItemTemplate>
															</asp:TemplateColumn>
														</Columns>
														<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
													</asp:datagrid></DIV>
											</TD>
										</TR>
										<tr id="trBtn" runat="server">
											<td align="center" colSpan="2"><asp:linkbutton id="btnSelect" accessKey="A" runat="server" CssClass="btnAddnew" ToolTip="Alt + A">Add</asp:linkbutton>&nbsp;&nbsp;&nbsp;
												<asp:linkbutton id="btnSelectAll" accessKey="D" runat="server" CssClass="btnAddnew" ToolTip="Alt+D"> Add all</asp:linkbutton>&nbsp;&nbsp;&nbsp;
												<asp:linkbutton id="btnRemove" accessKey="R" runat="server" CssClass="btnAddnew" ToolTip="Alt + R">Delete</asp:linkbutton>&nbsp;&nbsp;&nbsp;
												<asp:linkbutton id="btnRemoveAll" accessKey="L" runat="server" CssClass="btnAddnew" ToolTip="Alt+L"> Delete all</asp:linkbutton></td>
										</tr>
										<tr>
											<td height="10"></td>
										</tr>
										<tr id="trListSelected" runat="server" width="100%">
											<td align="center" colSpan="2">
												<table width="100%">
													<TR>
														<TD align="center">
															<DIV style="OVERFLOW: auto; HEIGHT: 140px"><asp:datagrid id="dtgGridSelected" runat="server" CssClass="grid" Width="100%" BackColor="White"
																	BorderColor="#3366CC" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False" AllowSorting="True" PageSize="20">
																	<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
																	<ItemStyle CssClass="gridItem"></ItemStyle>
																	<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
																	<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
																	<Columns>
																		<asp:BoundColumn Visible="False" DataField="EmpID">
																			<HeaderStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Middle"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Center"></ItemStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn Visible="False" DataField="EmpCode">
																			<HeaderStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Middle"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Center"></ItemStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn Visible="False" DataField="EmpName" HeaderText="Emp Name">
																			<HeaderStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Middle"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Center"></ItemStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn Visible="False" DataField="StartDateStr" HeaderText="Ng&#224;y B&#208; v&#224;o LH">
																			<HeaderStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Middle"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Center"></ItemStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn Visible="False" DataField="VLastName" HeaderText="Last Name">
																			<HeaderStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Middle"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Center"></ItemStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn Visible="False" DataField="VFirstName" HeaderText="First Name">
																			<HeaderStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Middle"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Center"></ItemStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn Visible="False" DataField="LocationName" HeaderText="Location Name">
																			<HeaderStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Middle"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Center"></ItemStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn Visible="False" DataField="CompanyName" HeaderText="Company Name">
																			<HeaderStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Middle"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Center"></ItemStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn Visible="False" DataField="Level1Name" HeaderText="Company">
																			<HeaderStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Middle"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Center"></ItemStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn Visible="False" DataField="Level2Name" HeaderText="Department">
																			<HeaderStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Middle"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Center"></ItemStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn Visible="False" DataField="Level3Name" HeaderText="Group">
																			<HeaderStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Middle"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Center"></ItemStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn Visible="False" DataField="PositionName" HeaderText="Position">
																			<HeaderStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Middle"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Center"></ItemStyle>
																		</asp:BoundColumn>
																		<asp:TemplateColumn HeaderText="&lt;b&gt;Seq&lt;/b&gt;">
																			<HeaderStyle Width="5%"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Center"></ItemStyle>
																			<ItemTemplate>
																				<%# Container.ItemIndex + 1 + dtgGridSelect.PageSize*dtgGridSelect.CurrentPageIndex%>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:BoundColumn DataField="EmpCode" SortExpression="EmpCode" HeaderText="Emp Code">
																			<HeaderStyle Width="10%"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Left"></ItemStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="EmpName" SortExpression="EmpName" HeaderText="Emp Name">
																			<HeaderStyle Width="17%"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Left"></ItemStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="Level1Name" SortExpression="Level1Name" HeaderText="Company">
																			<HeaderStyle Width="7%"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Left"></ItemStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="Level2Name" SortExpression="Level2Name" HeaderText="Department">
																			<HeaderStyle Width="13%"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Left"></ItemStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="Level3Name" SortExpression="Level3Name" HeaderText="Group">
																			<HeaderStyle Width="13%"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Left"></ItemStyle>
																		</asp:BoundColumn>
																		<asp:TemplateColumn HeaderText="Select">
																			<HeaderStyle Width="7%"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Center"></ItemStyle>
																			<HeaderTemplate>
																				<asp:CheckBox id="chkSelectAll1" runat="server" CssClass="checkbox" onclick="CheckAll('_ctl0_dtgGridSelected__ctl1_chkSelectAll1','_ctl0_dtgGridSelected',2,1,'chkSelect1')"></asp:CheckBox>
																			</HeaderTemplate>
																			<ItemTemplate>
																				<asp:CheckBox id="chkSelect1" runat="server" CssClass="checkbox"></asp:CheckBox>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																	</Columns>
																	<PagerStyle HorizontalAlign="Right" CssClass="gridPage" Mode="NumericPages"></PagerStyle>
																</asp:datagrid></DIV>
														</TD>
													</TR>
													<tr>
														<td align="center"><asp:textbox id="txtPageLoad" runat="server" Width="38px" Visible="False"></asp:textbox><asp:linkbutton id="btnChoose" accessKey="X" runat="server" CssClass="btnAddnew" ToolTip="Alt + S">Select</asp:linkbutton>&nbsp;&nbsp;&nbsp;
															<asp:linkbutton id="btnClose" accessKey="C" runat="server" CssClass="btnAddnew" ToolTip="Alt + C">Close</asp:linkbutton><INPUT id="txtWhereCondition" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtWhereCondition"
																runat="server"></td>
													</tr>
												</table>
											</td>
										</tr>
									</TABLE> <!-- end grid for input form --></TD>
							</TR>
						</table>
					</TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<!-- end button for input form --></TABLE>
<script language="javascript">
	function AddDetails(strEmpID, strEmpName, strStartDate, strLevel1Name, strLevel2Name, strPosition)
	{
		eval('txtEmpID = opener.document.forms[0].all("_ctl0:HR_EmpHeader:txtEmpID")');		
		eval('txtEmpName = opener.document.forms[0].all("_ctl0:HR_EmpHeader:txtEmpName")');		
		eval('lblStartDate = opener.document.forms[0].all("_ctl0_HR_EmpHeader_lblStartDate")');
		eval('lblLevel1Name = opener.document.forms[0].all("_ctl0_HR_EmpHeader_lblLevel1Name")');
		eval('lblLevel2Name = opener.document.forms[0].all("_ctl0_HR_EmpHeader_lblLevel2Name")');
		eval('lblPosition = opener.document.forms[0].all("_ctl0_HR_EmpHeader_lblPosition")');
		txtEmpID.value = strEmpID == "&nbsp;"?"":strEmpID;
		txtEmpName.value = strEmpName == "&nbsp;"?"":strEmpName;	
		lblStartDate.innerText = strStartDate == "&nbsp;"?"":strStartDate;
		lblLevel1Name.innerText = strLevel1Name == "&nbsp;"?"":strLevel1Name;
		lblLevel2Name.innerText = strLevel2Name == "&nbsp;"?"":strLevel2Name;
		lblPosition.innerText = strPosition == "&nbsp;"?"":strPosition;
		window.close();
		return false;
	}
	
	function ReloadOpener(strEmpID, strEmpName, strStartDate, strEmpCode, strLastName, strFirstName,
		strLocation, strCompany, strLevel1, strLevel2, strLevel3, strPosition)
	{
	
		if (opener.LoadDataFromPopup == null)
		{			
			opener.document.location = opener.document.location;
		}
		else
		{
			
			opener.LoadDataFromPopup(strEmpID, strEmpName, strStartDate, strEmpCode, strLastName, strFirstName,
			strLocation, strCompany, strLevel1, strLevel2, strLevel3, strPosition);
		}
		window.close();
	}
	
	function ReturnDataToOpener(strEmpList,strEmpListID)
	{
		opener.GetValueEmpList(strEmpList,strEmpListID);		
		window.close();
	}
	
	function LoadApprover(strEmpID, strEmpName)
	{
		eval('txtApprover = opener.document.forms[0].all("_ctl0_txtApproverName")');		
		eval('txtApproverID = opener.document.forms[0].all("_ctl0_txtApprover")');		
		txtApprover.value = strEmpID == "&nbsp;"?"":strEmpName;
		txtApproverID.value = strEmpName == "&nbsp;"?"":strEmpID;	
		window.close();
		return false;
	}
	
	function ReturnEmpPopUp(strParam, strEmpID, strEmpName, strStartDate, strEmpCode, strLastName, strFirstName, strLevel1Name)
	{
		var strOpenerFunction = "ReturnEmpPopUp" + strParam;		
		if (strParam == "3")
			opener.ReturnEmpPopUp3(strEmpID, strEmpCode, strEmpName);
		else if (strParam == "4")
			opener.ReturnEmpPopUp4(strEmpID, strEmpCode, strEmpName, strStartDate);
		else if (strParam == "6")
			opener.ReturnEmpPopUp4(strEmpID, strEmpCode, strEmpName, strStartDate, strLastName, strFirstName);
		else if (strParam == "7")
			opener.ReturnEmpPopUp7(strEmpID, strEmpCode, strEmpName, strStartDate, strLastName, strFirstName, strLevel1Name);
		window.close();
	}
	
	function CheckGrid(strGridName)
	{
		var strCtlName = "";
		if (strGridName == 'dtgGridSelect')
		{
			strCtlName = "chkSelect";
			strGridName = "_ctl0_dtgGridSelect";
			if (GridCheck(strGridName, 3, 1, strCtlName) == false)
			{
				//alert("Must check at least one record to move!");
				GetAlertError(iTotal,DSAlert,"EP_0001");
				return false;
			}
		}
		else
		{
			strCtlName = "chkSelect1";
			strGridName = "_ctl0_dtgGridSelected";
			if (GridCheck(strGridName, 2, 1, strCtlName) == false)
			{
				//alert("Must check at least one record to move!");	
				GetAlertError(iTotal,DSAlert,"EP_0001");
				return false;
			}
		}
	}
</script>
<script><asp:Literal id="ltlAlert" runat="server" EnableViewState="False"></asp:Literal></script>
<%if (!Page.IsPostBack){%>
<script language="javascript">		
		document.getElementById("_ctl0_btnSearch").click();
</script>
<%}%>
