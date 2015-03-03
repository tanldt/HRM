<%@ Import Namespace ="iHRPCore.Com" %>
<%@ Page language="c#" Codebehind="SumPayroll3.aspx.cs" AutoEventWireup="false" Inherits="iHRPCore.MdlPR.SumPayroll3" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SumPayroll</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<style type="text/css">
		BODY { FONT-SIZE: 10px; COLOR: #000000; LINE-HEIGHT: 24px; FONT-FAMILY: Arial, Helvetica, sans-serif }
		TD { FONT-SIZE: 10px; COLOR: #000000; FONT-FAMILY: Arial, Helvetica, sans-serif }
		</style>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<script language="Javascript">
	<!--
function doprint() {
  //save existing user's info
  var h = factory.printing.header;
  var f = factory.printing.footer;
  //hide the button
  document.all("printbtn").style.visibility = 'hidden';
  //set header and footer to blank
  factory.printing.header = "";
  factory.printing.footer = "";
  //print page without prompt
  factory.printing.portrait = false;
  factory.printing.leftMargin = 0.5;
  factory.printing.rightMargin = 0.5;
  factory.DoPrint(false);
  //restore user's info
  factory.printing.header = h;
  factory.printing.footer = f;
  //show the print button
  document.all("printbtn").style.visibility = 'visible';
}
function ExportToExcell(){
		Form2.txtDocument.value = divGrid.innerHTML;
 		Form2.action="../Reports/DownloadToExcel.asp";
 		Form2.target='_blank';
		Form2.submit(); 
}	

function Back()
{
	self.history.back();
}	
//-->
		</script>
		<OBJECT id="factory" style="DISPLAY: none" codeBase="../scriptx/ScriptX.cab#Version=5,0,4,185"
			classid="clsid:1663ed61-23eb-11d2-b92f-008048fdd814" viewastext>
		</OBJECT>
		<form id="Form1" method="post" runat="server">
			<asp:Label id="MMYYYY" runat="server" />
			<asp:Label id="SalPeriod" runat="server" />
			<asp:Label id="Comp" runat="server" />
			<asp:Label id="L1" runat="server" />
			<asp:Label id="L2" runat="server" />
			<asp:Label id="L3" runat="server" />
			<br>
			<div id="printbtn" align="center"><input onclick="ExportToExcell()" type="button" value="Xuất ra Excel" name="idExport">
				<br>
			</div>
			<div id="divGrid">
				<span id="lblTitle" class="TextTitle" style="FONT-SIZE:14pt;FONT-FAMILY:Arial;HEIGHT:12px"
					runat="server">BẢNG TỔNG HỢP LƯƠNG THÁNG </span>
				<TABLE id="tblMaster" style="BORDER-COLLAPSE: collapse" cellSpacing="0" cellPadding="1"
					width="1500" border="1">
					<!-- Phan Header-->
					<TR bgcolor="#f5f7f6">
						<TD align="center" rowspan="2">Mã NV</TD>
						<TD align="center" rowspan="2" width="200">Họ tên</TD>
						<%
			if(tblMaster.Rows.Count > 0)
			{
				for (int i=0; i<tblMaster.Rows.Count; i++)
				{
					%>
						<TD align=center colspan='<%=tblMaster.Rows[i]["count"].ToString()%>'>
							<font class="label" style="FONT-WEIGHT:bold">
								<%=tblMaster.Rows[i]["Name"].ToString()%>
							</font>
						</TD>
						<%
				}
			}
			%>
					</TR>
					<TR bgcolor="#f5f7f6">
						<% 
			if(tblMaster.Rows.Count > 0)
			{
				for (int i=0; i<tblMaster.Rows.Count; i++)
				{
					// Phần Detail
					tblDetail = clsCommon.GetDataTable("LS_spfrmSalaryItem3 @Activity = 'DataPayroll_DetailID',@LanguageID = '" +strLanguage+ "',@LSSalaryItemTypeID = '" + tblMaster.Rows[i]["LSSalaryItemTypeID"].ToString() + "'");
					if(tblDetail.Rows.Count > 0)
					{
						for (int j=0; j<tblDetail.Rows.Count; j++)
						{
						strDetails += "," + tblDetail.Rows[j]["LSSalaryItemCode"].ToString();
				%>
						<TD><font class="label">
								<%=tblDetail.Rows[j]["Name"].ToString()%>
							</font>
						</TD>
						<%
						}
					}
				}
			}
			%>
					</TR>
					<!-- End Phan Header-->
					<!-- Phan chi tiet tung nhan vien-->
					<%
				string[] marrstrDetails = strDetails.Split(',');
				%>
					<%
				if (dtList != null)
				{
				if(dtList.Rows.Count > 0)
				{
					for (int iList=0; iList<dtList.Rows.Count; iList++)
					{
				%>
					<TR>
						<TD align="center"><%=dtList.Rows[iList]["EmpCode"].ToString()%></TD>
						<TD align="left"><%=dtList.Rows[iList]["EmpName"].ToString()%></TD>
						<% 
					if(marrstrDetails.Length > 0)
					{
						for (int j=1; j<marrstrDetails.Length; j++)
						{
					%>
						<td align="right"><font class="label">
								<%
							string strLSSalaryItemCode = marrstrDetails.GetValue(j).ToString().Trim();
							%>
								<%
								try
								{
								Response.Write(clsCommon.FormatNumericWithSeparate(Convert.ToDouble(dtList.Rows[iList][strLSSalaryItemCode].ToString())).Trim());
								}
								catch {}
								%>
							</font>
						</td>
						<%
						} // End For
					}// End if(tblMaster.Rows.Count > 0)
					%>
					</TR>
					<%
					} // End for (int iList=0; iList<dtList.Rows.Count; iList++)
				} //End if(dtList.Rows.Count > 0)
				}
				%>
					<!-- End Phan chi tiet tung nhan vien-->
				</TABLE>
			</div>
			<br>
			<asp:datagrid id="dtgList" runat="server" AutoGenerateColumns="False" Visible="False">
				<HeaderStyle BackColor="#CCCCCC"></HeaderStyle>
				<Columns>
					<asp:BoundColumn DataField="LSSalaryItemCode" HeaderText="Column Name"></asp:BoundColumn>
					<asp:BoundColumn DataField="Name" HeaderText="Column Description"></asp:BoundColumn>
				</Columns>
			</asp:datagrid>
		</form>
		<form id="Form2" method="post">
			<INPUT id="txtDocument" type="hidden" name="txtDocument">
		</form>
	</body>
</HTML>
