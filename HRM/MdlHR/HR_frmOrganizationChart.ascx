<%@ Register TagPrefix="uc1" TagName="EmpHeaderSearch" Src="../Include/EmpHeaderSearch.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ColumnList" Src="../Include/ColumnList.ascx" %>
<%@ Register TagPrefix="FPT" Namespace="FPTToolWeb.Control.DataGrids" Assembly="FPTToolWeb" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="HR_frmOrganizationChart.ascx.cs" Inherits="iHRPCore.MdlHR.HR_frmOrganizationChart" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="98%" border="0">
	<TR>
		<TD align="center"><asp:label id="lblErr" runat="server" CssClass="lblErr"></asp:label></TD>
	</TR>
	<TR>
		<TD vAlign="top" noWrap align="center" height="5">&nbsp;
		</TD>
	</TR>
	<TR>
		<TD style="WIDTH: 610px" vAlign="top" align="left" rowSpan="2">
			<TABLE id="Table12" cellSpacing="0" cellPadding="0" align="center" border="0">
				<TR>
					<TD vAlign="top" align="left">
						<asp:label id="myTree" runat="server"></asp:label>
					</TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>
<script language="javascript">
	function OpenBuildCode()
	{		
		window.open('FormPage.aspx?ModuleID=HR&ParentID=8&FunctionID=204&Ascx=MdlHR/BuildEmpCode.ascx&action=addnew&empid=', 'BuildCode', 'width=320,height=120,left=300,top=200,dependent');		
		return false;
	}
</script>
<%if (!Page.IsPostBack){%>
<script language="javascript">
		//document.getElementById("_ctl0_btnSearch").click();
</script>
<%}%>
<script language="javascript" type="text/javascript">
    function toggle(node) {
    
      var nextDIV = node.nextSibling;
    
      while(nextDIV.nodeName != "DIV") {
        nextDIV = nextDIV.nextSibling;
      }
    
      if (nextDIV.style.display == 'none') {
    
        if (node.childNodes.length > 0) {
    
          if (node.childNodes.item(0).nodeName == "IMG") {
            node.childNodes.item(0).src = getImgDirectory(node.childNodes.item(0).src) + "minus.gif";
          }
        }
        nextDIV.style.display = 'block';
      }
      else {
    
        if (node.childNodes.length > 0) {
          if (node.childNodes.item(0).nodeName == "IMG") {
              node.childNodes.item(0).src = getImgDirectory(node.childNodes.item(0).src) + "plus.gif";
          }
        }
        nextDIV.style.display = 'none';
      }
    }
    
    function getImgDirectory(source) {
        return source.substring(0, source.lastIndexOf('/') + 1);
    }
    
    function selectLeaf(title, url) {
      alert("You just clicked on title = " + title + ":: url = " + url);
    }
</script>
