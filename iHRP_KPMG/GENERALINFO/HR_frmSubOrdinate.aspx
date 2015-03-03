<%@ Page language="c#" Codebehind="HR_frmSubOrdinate.aspx.cs" AutoEventWireup="false" Inherits="HRWebServiceC.UserServices.GeneralInfo.HR_frmSubOrdinate" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>HR_frmSubOrdinate</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<style type="text/css" media="screen">.folder { PADDING-RIGHT: 0px; PADDING-LEFT: 0px; FONT-SIZE: 8pt; PADDING-BOTTOM: 0px; CURSOR: hand; COLOR: #000000; PADDING-TOP: 0px; FONT-FAMILY: arial; TEXT-DECORATION: none }
	.folder:hover { PADDING-RIGHT: 0px; PADDING-LEFT: 0px; FONT-SIZE: 8pt; PADDING-BOTTOM: 0px; CURSOR: hand; COLOR: #336699; PADDING-TOP: 0px; FONT-FAMILY: arial; TEXT-DECORATION: none }
	.leaf:hover { PADDING-RIGHT: 0px; PADDING-LEFT: 0px; FONT-SIZE: 8pt; PADDING-BOTTOM: 0px; CURSOR: hand; COLOR: #000000; PADDING-TOP: 0px; FONT-FAMILY: arial; TEXT-DECORATION: none }
	.leaf { PADDING-RIGHT: 0px; PADDING-LEFT: 0px; FONT-SIZE: 8pt; PADDING-BOTTOM: 0px; CURSOR: hand; COLOR: #000000; PADDING-TOP: 0px; FONT-FAMILY: arial; TEXT-DECORATION: none }
	.folder:hover { CURSOR: hand }
	.leaf:hover { CURSOR: hand }
		</style>
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
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE align="center" id="Table1" style="Z-INDEX: 102; LEFT: 8pt; WIDTH: 400px; POSITION: absolute; TOP: 8pt; HEIGHT: 27px"
				cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<TD align="center" bgColor="#ffffff"><asp:label id="lblTitle" runat="server" Font-Bold="True" ForeColor="Gray" Font-Size="12&#13;&#10;&#9;&#9;&#9;&#9;&#9;"
							Font-Names="Arial" Height="34px">NHÂN VIÊN CẤP DƯỚI</asp:label></TD>
				</TR>
				<TR>
					<TD vAlign="top"><asp:label id="myTree" runat="server"></asp:label></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
