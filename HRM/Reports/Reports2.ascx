<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../Include/EmpHeaderSearch.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Reports2.ascx.cs" Inherits="iHRPCore.Reports.Reports2" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<TABLE id="tblMain" cellSpacing="0" cellPadding="0" width="97%" border="0">
	<TR>
		<TD vAlign="top" align="center" height="10"><asp:label id="lblErr" ForeColor="Red" CssClass="Label" runat="server"></asp:label></TD>
	</TR>
	<TR style="DISPLAY: none">
		<TD vAlign="top" align="center"><uc1:empheadersearch id="EmpHeaderSearch1" runat="server" ></uc1:empheadersearch>
			<HR style="WIDTH: 81.98%; HEIGHT: 2px" width="81.98%" SIZE="2">
		</TD>
	</TR>
	<TR vAlign="top" height="10">
		<TD vAlign="top" align="center"></TD>
	</TR>
	<TR>
		<TD vAlign="top" align="center" height="10">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="70%" align="center" runat="server">
			</TABLE>
			<HR width="95%">
		</TD>
	</TR>
	<TR style="DISPLAY: none">
		<TD vAlign="top" align="left"><asp:checkbox id="chkShowGroup" accessKey="G" onclick="javascript:collapse('trGroup');" CssClass="checkbox"
				runat="server" Text="Show group" Checked="False" ToolTip="Alt+G"></asp:checkbox><asp:checkbox id="chkShowSort" accessKey="S" onclick="javascript:collapse('trSort');" CssClass="checkbox"
				runat="server" Text="Show sort" Checked="False" ToolTip="Alt+G"></asp:checkbox></TD>
	</TR>
	<TR id="trGroup" style="DISPLAY: none">
		<TD vAlign="top" align="center">
			<TABLE id="tblG" cellSpacing="2" cellPadding="1" width="100%" border="0">
				<TR>
					<TD><asp:label id="Label1" CssClass="label" runat="server">GroupBy</asp:label></TD>
					<TD><asp:dropdownlist id="grp1" runat="server" Width="140px"></asp:dropdownlist></TD>
					<TD><asp:label id="Label2" CssClass="label" runat="server">GroupBy</asp:label></TD>
					<TD><asp:dropdownlist id="grp2" runat="server" Width="140px"></asp:dropdownlist></TD>
					<TD><asp:label id="Label3" CssClass="label" runat="server">GroupBy</asp:label></TD>
					<TD><asp:dropdownlist id="grp3" runat="server" Width="140px"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD><asp:label id="Label6" CssClass="label" runat="server">Descript.</asp:label></TD>
					<TD><asp:dropdownlist id="des1" runat="server" Width="140px"></asp:dropdownlist></TD>
					<TD><asp:label id="Label7" CssClass="label" runat="server">Descript.</asp:label></TD>
					<TD><asp:dropdownlist id="des2" runat="server" Width="140px"></asp:dropdownlist></TD>
					<TD><asp:label id="Label8" CssClass="label" runat="server">Descript.</asp:label></TD>
					<TD><asp:dropdownlist id="des3" runat="server" Width="140px"></asp:dropdownlist></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR id="trSort" style="DISPLAY: none">
		<TD vAlign="top" align="center">
			<TABLE id="tblS" cellSpacing="2" cellPadding="1" width="100%" border="0">
				<TR>
					<TD style="HEIGHT: 16px"><asp:label id="Label11" CssClass="label" runat="server">Sort By</asp:label></TD>
					<TD style="HEIGHT: 16px"><asp:dropdownlist id="sort1" runat="server" Width="140px"></asp:dropdownlist></TD>
					<TD style="HEIGHT: 16px"><asp:label id="Label12" CssClass="label" runat="server">Sort By</asp:label></TD>
					<TD style="HEIGHT: 16px"><asp:dropdownlist id="sort2" runat="server" Width="140px"></asp:dropdownlist></TD>
					<TD style="HEIGHT: 16px"><asp:label id="Label13" CssClass="label" runat="server">Sort By</asp:label></TD>
					<TD style="HEIGHT: 16px"><asp:dropdownlist id="sort3" runat="server" Width="140px"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD><asp:label id="Label4" CssClass="label" runat="server" Visible="False">GroupBy</asp:label></TD>
					<TD><asp:dropdownlist id="grp4" runat="server" Width="85px" Visible="False"></asp:dropdownlist></TD>
					<TD><asp:label id="Label5" CssClass="label" runat="server" Visible="False">GroupBy</asp:label></TD>
					<TD><asp:dropdownlist id="grp5" runat="server" Width="85px" Visible="False"></asp:dropdownlist></TD>
					<TD><asp:label id="Label14" CssClass="label" runat="server" Visible="False">Sort By</asp:label></TD>
					<TD><asp:dropdownlist id="sort4" runat="server" Width="85px" Visible="False"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD><asp:label id="Label9" CssClass="label" runat="server" Visible="False">Descript.</asp:label></TD>
					<TD><asp:dropdownlist id="des4" runat="server" Width="85px" Visible="False"></asp:dropdownlist></TD>
					<TD><asp:label id="Label10" CssClass="label" runat="server" Visible="False">Descript.</asp:label></TD>
					<TD><asp:dropdownlist id="des5" runat="server" Width="85px" Visible="False"></asp:dropdownlist></TD>
					<TD><asp:label id="Label15" CssClass="label" runat="server" Visible="False">Sort By</asp:label></TD>
					<TD><asp:dropdownlist id="sort5" runat="server" Width="85px" Visible="False"></asp:dropdownlist></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR vAlign="top">
		<TD vAlign="top" align="center">
			<!--<HR width="95%">-->
			<asp:linkbutton id="btnPreview" CssClass="Button" runat="server">Print</asp:linkbutton><INPUT id="_ctl0_btnSearch" type="button" value="Button" style="DISPLAY: none">
		</TD>
	</TR>
</TABLE>
<SCRIPT language="javascript">
	/*
	if(window.event.keyCode==13){
		alert(window.event.keyCode);
		event.returnValue=false;
		event.cancel = true;
	}
	*/
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

</SCRIPT>

