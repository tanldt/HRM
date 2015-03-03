<head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
</head>
<%
	
'---------------------Global Constants and Variables---------------------

	Session.Timeout=60
	Response.Expires = 0
	Response.Buffer = True
	Session.codepage = 65001
	
	Const CONNECTION_STRING = "driver={SQL SERVER};Server=Sanhhn;Database=iHRPCore;UID=sa;PWD=sa;" 'Trung tam thong tin
	
'	Const TenCongTy1 = "UY BAN QUAN BINH THANH"
'	Const TenCongTy2 = "VĂN PHÒNG QUAN BINH THANH"
	
%>

<%
sub OpenReport(iRecordSet, iReportname, iFormula, iFormulaValue)
	
	dim ObjectFactory
	dim i, j
	
	dim arrFormula 'chua cac formula se truyen vao reprot cach nhau bang dau ;
	dim arrFormulaValue 'chua cac formula value se truyen vao reprot cach nhau bang dau ;


	arrFormula = split(iFormula,";")
	arrFormulaValue = split(iFormulaValue,";")
	
	for i=0 to ubound(arrFormula) 'Ucase va bo khoang trang
		arrFormula(i) = UCase(trim(arrFormula(i)))
	next		

	Path = Request.ServerVariables("Path_Translated")
	
	
	While (Right(Path, 1) <> "\" And Len(Path) <> 0)
		iLen = Len(Path) - 1
		Path = Left(Path, iLen)
	Wend	
		
	if (isObject(session("oClientDoc"))) then
		set session("oClientDoc") = nothing
	end if

	Set ObjectFactory = CreateObject("CrystalReports.ObjectFactory.2")	
	
	Set session("oClientDoc") = ObjectFactory.CreateObject("CrystalClientDoc.ReportClientDocument")

	'response.Write("rassdk://" & path & iReportname)
	'response.end
	
	session("oClientDoc").Open ("rassdk://" & path & iReportname)	
	
	session("oClientDoc").DatabaseController.SetDataSource iRecordSet, session("oClientDoc").Database.Tables.Item(0).Name, "ADORecordSet"
	'response.Write("cu chuoi")
	'response.end
	'Set Formula = ObjectFactory.CreateObject("CrystalReports.Field")
	
	'session("oClientDoc").DataDefinition.FormulaField
	
	set ParamList = session("oClientDoc").DataDefController.DataDefinition.ParameterFields
		
	for i=0 to ParamList.count -1			
		SELECT CASE UCASE(ParamList.Item(i).name)				
				CASE "TENCONGTY1"
					call AddPara("TenCongTy1", TenCongTy1, i, ObjectFactory)					
				CASE "TENCONGTY2"
					call AddPara("TenCongTy2", TenCongTy2, i, ObjectFactory)
				CASE ELSE
					for j=0 to ubound(arrFormula) 
						IF (arrFormula(j) = Ucase(ParamList.Item(i).name)) then
							call AddPara(ParamList.Item(i).name, arrFormulaValue(j), i, ObjectFactory)
						end if					
					next	
					if (j= ubound(arrFormula) + 1) then
						call AddPara(ParamList.Item(i).name, "param chua truyen vao", i, ObjectFactory)
					end if
			END SELECT		
	next	
	'Response.End
	'Response.Write iReportname
	'Response.End	
	
end sub

Sub AddPara(P_PARA, P_VALUE, P_INDEX, objFactory)
	'This line creates an object to represent the collection of parameter fields that are contained in the report.
'Response.Write session("ParamCollection").Item(0).Name
	'Response.End
	Set Session("ParamCollection") = Session("oClientDoc").DataDefinition.ParameterFields

	'Create a reference to the parameter value to be set
'	Response.Write session("ParamCollection").Item(0).Name
'	Response.End
	Set session("ParamToChange") = session("ParamCollection").Item(P_INDEX)

	'This line creates a temporary variable to store the value to pass to the paraemter field. 
	' In this case it’s defined as a discrete value.

	Set session(P_PARA) = objFactory.CreateObject("CrystalReports.ParameterFieldDiscreteValue")
	session(P_PARA).Value =  P_VALUE
	
	'The following lines of code creates the Parameter Field object. 
	'From the Parameter collection which will be changing the value for

	Set session("TempParam") = objFactory.CreateObject("CrystalReports.ParameterField")
	session("ParamToChange").CopyTo session("TempParam")

	'This line sets the new current value for the Parameter.
	session("TempParam").CurrentValues.Add(session(P_PARA))

	'The ParameterFieldController is used to add, remove, and modify parameter fields in a report. 
	Set session("ParamController") = Session("oClientDoc").DataDefController.ParameterFieldController
	session("ParamController").Modify session("ParamToChange"), session("TempParam")
	
End Sub


sub Open(iReportname, iFormula, iFormulaValue)
	dim ObjectFactory
	dim i, j
	
	dim arrFormula 
	dim arrFormulaValue 
	
	arrFormula = split(iFormula,";")
	arrFormulaValue = split(iFormulaValue,";")
	
	for i=0 to ubound(arrFormula) 
		arrFormula(i) = UCase(trim(arrFormula(i)))		
	next		
	
	Path = Request.ServerVariables("Path_Translated")
	While (Right(Path, 1) <> "\" And Len(Path) <> 0)
		iLen = Len(Path) - 1
		Path = Left(Path, iLen)
	Wend
	
	
	if (isObject(session("oClientDoc"))) then
		set session("oClientDoc") = nothing
	end if
	
	
	
	Set ObjectFactory = CreateObject("CrystalReports.ObjectFactory.2")
	Set session("oClientDoc") = ObjectFactory.CreateObject("CrystalClientDoc.ReportClientDocument")

	session("oClientDoc").Open ("rassdk://" & path & iReportname)
	
	'Set Formula = ObjectFactory.CreateObject("CrystalReports.Field")
	
	'session("oClientDoc").DataDefinition.FormulaField
		
	'Response.Write(Formula.length)
	set ParamList = session("oClientDoc").DataDefController.DataDefinition.ParameterFields
		
	for i=0 to ParamList.count -1			
		SELECT CASE UCASE(ParamList.Item(i).name)
				CASE "TENCONGTY1"
					call AddPara("TenCongTy1", TenCongTy1, i, ObjectFactory)
				CASE "TENCONGTY2"
					call AddPara("TenCongTy2", TenCongTy2, i, ObjectFactory)
				CASE ELSE
					for j=0 to ubound(arrFormula) 
						IF (arrFormula(j) = Ucase(ParamList.Item(i).name)) then
							call AddPara(ParamList.Item(i).name, arrFormulaValue(j), i, ObjectFactory)
						end if					
					next	
					if (j= ubound(arrFormula) + 1) then
						call AddPara(ParamList.Item(i).name, "param chua truyen vao", i, ObjectFactory)
					end if
			END SELECT		
	next	
	
	'Response.Write iReportname
	'Response.End	
	
end sub




%>