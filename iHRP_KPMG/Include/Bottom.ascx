<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Bottom.ascx.cs" Inherits="iHRPCore.Include.Bottom" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<SCRIPT language="javascript">
var text1="..:: FPT.iHRPCore ::.."
comeback=0
cometo=0
function dis()
{
window.status=text1;
/*
	if(comeback==1)
	{
		cometo--;
		if(cometo==0){comeback=0}
	} 
	else 
	{
		cometo++;
		if(cometo==text1.length){comeback=1}
	}
	window.status=text1.substring(0,cometo)+"|"
	if(cometo==text1.length){window.setTimeout("dis()",100);} else {window.setTimeout("dis()",100);}
*/
window.setTimeout("dis()",1000);
}
dis()
</SCRIPT>
<table width="100%" align="center" border="0" cellpadding="0" cellspacing="0">
	<tr>
		<td vAlign="top" background="./images/footer_01.jpg" height="30" align="center">
			Copyright ® FPT. All Rights Reserved.
		</td>
	</tr>
	<tr>
		<td background="./images/footer_02.jpg" height="10">&nbsp;
		</td>
	</tr>
</table>
