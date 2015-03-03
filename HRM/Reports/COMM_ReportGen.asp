<html>
<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<!--#include file = "rpt_Lib.asp"-->
<title>In bao cao</title>
</head>
<body>
<%
	Dim adUseClient
	Dim adOpenDynamic

	adUseClient = 3
	adOpenDynamic = 2
	Dim cn 		'as ADODB.Connection
	Dim rs  		'as ADODB.Recordset
	Dim Field			'as ADODB.Field
	Dim ObjectFactory		'as CrystalReports.ObjectFactory.2

	Dim HTMLViewer			'as CrystalReports.CrystalReportInteractiveViewer
	Dim Reportname			'as String
	Dim Path			'as String
	Dim iLen			'as Integer
	Dim viewer	
	
	
	Set cn = CreateObject("ADODB.Connection")
	Set rs = CreateObject("ADODB.Recordset")

	cn.Open (CONNECTION_STRING)
	rs.CursorType = adOpenDynamic
	rs.CursorLocation = adUseClient
	
	'Response.Write (request.queryString("report") & request.queryString("sql"))
	'Response.End
	'response.Write(CONNECTION_STRING)
	'response.end
	'Response.Write(request.queryString("sql"));' + " " + request("formula") + "='" request("Value") + "'")
	'Response.End
	set rs = cn.Execute(request.queryString("sql")); + " " + request("formula") + "=" + request("Value"))
	
	if not (rs.eof) then
		
		Reportname = "reports\" & request.queryString("report")
		Formula = request("formula")
		FormulaValue = request("Value")
		
		'Response.Write("aloafhakjsfh")
		'Response.Write(formula)
		'Response.Write(formulavalue)
		'response.end	
	
	call OpenReport(rs, Reportname,Formula,FormulaValue)
%>
	<!--#include file = "ReportComponents/ActiveXViewer.asp"-->
	<%else
		Response.Write "<script language=""javascript"">"
		Response.Write "alert('Khong tim thay du lieu.');"
		Response.Write "window.close();"
		Response.Write "</script>"	
	end if
	rs.close
	set rs = nothing
	%>

<%
	Function DestroyObjects(ByRef ObjectToDestroy)
		If isobject(ObjectToDestroy) then
			set ObjectToDestroy = nothing
			DestroyObjects = true
		Else
			DestroyObjects = false
		End if
	
	End Function
	
	DestroyObjects session("oPageEngine")
	DestroyObjects session("oRpt")
	DestroyObjects session("oApp")

%>

</body>
</html>
