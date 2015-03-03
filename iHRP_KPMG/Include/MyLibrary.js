/****************************************************************************************
* 1. isBlank(obj)
*		Kiem tra du lieu la Null thi se return True
* 2. CheckDate(obj)
*		Kiem tra du lieu kieu ngay theo dang dd/mm/yyyy
* 3. FromSmallToDate(objFromDate,objToDate)
*		Du lieu Ngay1 phai nho hon du lieu Ngay2
* 4. DispDate(obj)
*		Hien thi calendar de chon ngay thang nam
* 5. EmailCheck(emailStr) 
*       Kiem tra tinh hop le cua dia chi mail
* 6. function CheckValidDate(Field) //dd/mm/yyyy
*----------------------------------------------------------------------------------------
* Ngoai ra co cac function phu sau day:
* 1. isValidDate(strDate)
* 2. IsSmaller(inputStr1, inputStr2)
*****************************************************************************************/
//--------------------------------------------
/*********************************************
****  Kiem tra ngay hop le (dd/mm/yyyy)  *****
*********************************************/
function isValidDate(strDate)
{    
  var retval = 0    
  var aDDMMCCYY    
  var dtest    
  // Kiem tra dung format    
  if (/^(\d\d?-\d\d?-\d{4})|(\d\d?\/\d\d?\/\d{4})|(\d{8})$/.test(strDate))    
  {    
    if (/\//.test(strDate))
    {    
      aDDMMCCYY = strDate.split("/");    
    }    
    else    
    if (/-/.test(strDate))    
    {    
      aDDMMCCYY = strDate.split("-");    
    }    
    else    
    {    
      aDDMMCCYY = Array(strDate.substr(0,2), strDate.substr(2,2), strDate.substr(4,4))    
    }        
	dtest = new Date(aDDMMCCYY[1] + "/" + aDDMMCCYY[0] + "/" + aDDMMCCYY[2]);          
    if (dtest.getDate() != aDDMMCCYY[0] || dtest.getMonth() +1 != aDDMMCCYY[1] || dtest.getFullYear() != aDDMMCCYY[2])    
    {    
      retval = 2    
    }    
  }    
  else    
  {    
	retval = 1    
  }    
  return retval    
}    
/*********************************************
**** Neu du lieu NULL se return True *********
*********************************************/
function isBlank(obj)
{
	if (obj.length <1 || obj.value=="")
		return true;
	else
		return false;
}
/********************************
**** Goi ham kiem tra ngay ******
********************************/

//HaLT  11/08/04
/*function CheckDate(obj)
{    
	if (!isBlank(obj))
	{
		if (isValidDate(obj.value)!=0)    
		{
			alert(obj.value + " InValid!");    
//			obj.focus();
					return false;
		}    
	}else
	{
		alert("InValid Date!");    
		return false;
	}	
}
*/
/*********************************************
*****  Fromdate < ToDate return True    ******
**  Duoc Call boi function FromSmallToDate ***
*********************************************/
function IsSmaller(inputStr1, inputStr2)
{
	var delim1 = inputStr1.indexOf("/")
	var delim2 = inputStr1.lastIndexOf("/")
	
	// lay ngay, thang, nam cua ngay1
	var dd1 = parseInt(inputStr1.substring(0,delim1),10)
	var mm1 = parseInt(inputStr1.substring(delim1 + 1,delim2),10)
	var yyyy1 = parseInt(inputStr1.substring(delim2 + 1,inputStr1.length),10)

	delim1 = inputStr2.indexOf("/")
	delim2 = inputStr2.lastIndexOf("/")

	// lay ngay, thang, nam cua ngay2
	dd2 = parseInt(inputStr2.substring(0,delim1),10)
	mm2 = parseInt(inputStr2.substring(delim1 + 1,delim2),10)
	yyyy2 = parseInt(inputStr2.substring(delim2 + 1,inputStr2.length),10)

	// ngay1 nho hon ngay 2 ?
	if (yyyy2 >= yyyy1)
	{
		if (mm2 == mm1) 
			if (dd2 >= dd1) return true;
			else return false;
		else
			if (mm2 > mm1) return true;
			else return false;
	}
	else
		return false;
}
/******************************************
**** Kiem tra tu ngay nho hon Den ngay ****
**** objFromDate : Thanh phan cua form ****
**** objToDate : Thanh phan cua form   ****
******************************************/
function FromSmallToDate(objFromDate,objToDate)
{
	FromDate = objFromDate.value
	ToDate = objToDate.value
	
	if ((!isBlank(FromDate)) && (!isBlank(ToDate)))
	{
		if  (!IsSmaller(FromDate, ToDate))
		{
			//alert("FromDate must be less than ToDate!");
			//objToDate.focus()
			return false;
		}
		else
			return true;
	}
	else
		return true;
}

/******************************************
**** Kiem tra tu ngay nho hon Den ngay ****
**** objFromDate : Thanh phan cua form ****
**** objToDate : Thanh phan cua form   ****
******************************************/
function FromSmallToDateValue(strFromDate,strToDate)
{
	FromDate = strFromDate;
	ToDate = strToDate;
	if ((!isBlank(FromDate)) && (!isBlank(ToDate)))
	{
		if  (!IsSmaller(FromDate, ToDate))
		{
			//alert("FromDate must be less than ToDate!");
			//objToDate.focus()
			return false;
		}
		else
			return true;
	}
	else
		return true;
}

/*******************************************
******          CALENDAR             *******
******  File dinh kem Cal_Dialog.asp *******
*******************************************/

var oDialog = "Cal_Dialog.asp";  // Passes URL and filename of dialog box as a variable
var bDialogStatus = false;     // Indicates whether modeless dialog box is currently open
var sDate=""; 
var oSelected = ""

function DispDate(obj){
	sDate = obj.value;
	oSelected = obj
	window.showModalDialog(oDialog, window , "dialogHeight: 260px; dialogWidth: 200px;  center: Yes; help: No; resizable: No; status: No; scroll = no");
}
function fnUpdate()
{  
	oSelected.value = sDate;
}
/*************************************************
*******  Kiem tra du lieu kieu so ****************
**************************************************/
function IsNumeric(obj)
{
	if (!isBlank(obj))
	{
		if (Number(obj.value))
		{
			return true;		
		}
		else
		{
			alert('Invalid number!');
			obj.value = "";		
			obj.focus();
			return false;
		}
		return false;
	}
	return true;
}

function ShowDatemmDDyyyy(dt){
	var month,day,year;
   	month = dt.getMonth()+1;
   	day = dt.getDate();
   	year = dt.getFullYear();

   	if (month < 10) month = "0" + month;
   	if (day < 10) day = "0" + day;

   	return day + "/" + month + "/" + year;
}

function FirstDateOfWeek(dDate){	
	var offset;
	var dt = dDate;

	off= dt.getDay() - 1;
   	dt.setDate(dt.getDate() - offset);
	return dt;
}

function LastDateOfWeek(dDate){	
	var offset;
	var dt = dDate;

	off= 6 - dt.getDay() + 1;
   	dt.setDate(dt.getDate() + offset);
	return dt;
}

function FirstDateOfMonth(dDate){	
	var dt = dDate;
   	dt.setMonth(dt.getMonth());
   	dt.setDate(1);
	return dt;
}

function LastDateOfMonth(dDate){	
	var dt = dDate;
   	dt.setMonth(dt.getMonth() + 1 );
   	dt.setDate(1);
   	dt.setDate( dt.getDate() - 1);
	return dt;
}


function CheckEmail(emailStr) {
	var emailPat=/^(.+)(.+)$/;
	var specialChars="\\(\\)<>,;:\\\\\\\"\\.\\[\\]";
	var validChars="\[^\\s" + specialChars + "\]";
	var quotedUser="(\"[^\"]*\")";
	var ipDomainPat=/^\[(\d{1,3})\.(\d{1,3})\.(\d{1,3})\.(\d{1,3})\]$/;
	var atom=validChars + '+';
	var word="(" + atom + "|" + quotedUser + ")";
	var userPat=new RegExp("^" + word + "(\\." + word + ")*$");
	var domainPat=new RegExp("^" + atom + "(\\." + atom +")*$");
	var matchArray=emailStr.match(emailPat);
	
	if (matchArray==null) {
		alert("Email address seems incorrect (check  and .'s)");
		return false;
	}
	
	var user=matchArray[1];
	var domain=matchArray[2];

	if (user.match(userPat)==null) {
		alert("The username doesn't seem to be valid.");
		return false;
	}

	var IPArray=domain.match(ipDomainPat);
	
	if (IPArray!=null) {
		for (var i=1;i<=4;i++) {
		    if (IPArray[i]>255) {
		        alert("Destination IP address is invalid!");
				return false;
			}
		}
		return true;
	}

	var domainArray=domain.match(domainPat);
	if (domainArray==null) {
		alert("The domain name doesn't seem to be valid.");
	    return false;
	}

	var atomPat=new RegExp(atom,"g");
	var domArr=domain.match(atomPat);
	var len=domArr.length;
	if (domArr[domArr.length-1].length<2 || 
		domArr[domArr.length-1].length>3) {

		alert("The address must end in a three-letter domain, or two letter country.");
		return false;
	}

	if (len<2) {
		var errStr="This address is missing a hostname!";
		alert(errStr);
		return false;
	}
	return true;
}
function scrolltext(seed){
	var msg = "Welcome to Mercedes-Benz Customer group";
	var out = " ";
	var c = 1;
	if (seed > 100) {
		seed--;
		cmd="scrolltext(" + seed + ")";
		timerTwo=window.setTimeout(cmd,100);
	}
	else if (seed <= 100 && seed > 0) {
		for (c=0 ; c < seed ; c++) {
			out+=" ";
		}
		out+=msg;
		seed--;
		window.status=out;
		cmd="scrolltext(" + seed + ")";
		timerTwo=window.setTimeout(cmd,100);
	}
	else if (seed <= 0) {
		if (-seed < msg.length) {
			out+=msg.substring(-seed,msg.length);
			seed--;
			window.status=out;
			cmd="scrolltext(" + seed + ")";
			timerTwo=window.setTimeout(cmd,100);
		}
		else {
			window.status=" ";
			timerTwo=window.setTimeout("scrolltext(100)",75);
		}
	}
}
// -----------
function trim(txt) {
    txt = txt.replace(/^(\s)+/, '');
    txt = txt.replace(/(\s)+$/, '');
   	return txt;
}

function CheckValidDate(field){
var checkstr = "0123456789";
var DateField = field;
var Datevalue = "";
var DateTemp = "";
var seperator = "/";
var day;
var month;
var year;
var leap = 0;
var err = 0;
var i;
   err = 0;
   DateValue = DateField.value;
   if (DateValue =="")	return;
   /* Delete all chars except 0..9 */
   for (i = 0; i < DateValue.length; i++) {
	  if (checkstr.indexOf(DateValue.substr(i,1)) >= 0) {
	     DateTemp = DateTemp + DateValue.substr(i,1);
	  }
   }
   DateValue = DateTemp;
   /* Always change date to 8 digits - string*/
   /* if year is entered as 2-digit / always assume 20xx */
   if (DateValue.length == 6) {
      DateValue = DateValue.substr(0,4) + '20' + DateValue.substr(4,2); }  
   
   if (DateValue.length != 8) {
      err = 19;}
   /* year is wrong if year = 0000 */
   year = DateValue.substr(4,4);
   if (year == 0) {
      err = 20;
   }
   /* Validation of month*/
   day = DateValue.substr(2,2);
    month = DateValue.substr(0,2);
   if ((month < 1) || (month > 12)) {
      err = 21;
   }
   /* Validation of day*/  
   if (day < 1) {
     err = 22;
   }
   
 
   /* Validation leap-year / february / day */
   if ((year % 4 == 0) || (year % 100 == 0) || (year % 400 == 0)) {
      leap = 1;
   }
   if ((month == 2) && (leap == 1) && (day > 29)) {
      err = 23;
   }
   if ((month == 2) && (leap != 1) && (day > 28)) {
      err = 24;
   }
   /* Validation of other months */
   if ((day > 31) && ((month == "01") || (month == "03") || (month == "05") || (month == "07") || (month == "08") || (month == "10") || (month == "12"))) {
      err = 25;
   }
   if ((day > 30) && ((month == "04") || (month == "06") || (month == "09") || (month == "11"))) {
      err = 26;
   }
   /* if 00 ist entered, no error, deleting the entry */
   if ((day == 0) && (month == 0) && (year == 00)) {
      err = 0; day = ""; month = ""; year = ""; seperator = "";
   }
   /* if no error, write the completed date to Input-Field (e.g. 13.12.2001) */
   
   if (err == 0) {
      //DateField.value = day + seperator + month + seperator + year.substr(2,2);
      //DateField.value = day + seperator + month + seperator + year;
      DateField.value = month + seperator + day + seperator + year;
      if (DateField.value == "")  	
      {
		alert("Date is incorrect!");
		DateField.focus();	
      }      
   }
   /* Error-message if err != 0 */
   else {
		DateField.value = "";
		DateField.focus();
		alert("Date is incorrect!");
      //DateField.select();
	  //DateField.focus();
   }

}

//----------------------------

function formatDateForCompare(datestr) {
 	//var datefmt = platform.document.GLOBAL.DATE_FORMAT.value;    // DATE_FORMAT
	//var datesep = platform.document.GLOBAL.DATE_SEPERATOR.value;    // DATE_SEPERATOR
	//var datefmt ='dd/MM/yyyy'
	var datefmt ='MM/dd/yyyy'
	var datesep ='/'
  	var iSepPos = datestr.indexOf(datesep);
  	var sDateStr = datestr;
  	var sStr1 = new String;
  	var sStr2 = new String;
  	var sStr3 = new String;
 
  	if (iSepPos > 0) {
      	sStr1 = sDateStr.substring(0,iSepPos);
      	sDateStr = sDateStr.substring(iSepPos + 1, sDateStr.length);
  	}

  	iSepPos = sDateStr.indexOf(datesep);
  	if (iSepPos > 0) {
      	sStr2 = sDateStr.substring(0,iSepPos);
      	sStr3 = sDateStr.substring(iSepPos + 1, sDateStr.length);
  	}

	
	datefmt.replace(datesep, '/')  	
  	datefmt = trim(datefmt);
  	var sMonth = '';
  	var sDay = '';
  	var sYear = '';
   	
  	if ((datefmt == 'MM/dd/yyyy') || (datefmt == 'M/d/yy') || (datefmt == 'MM/dd/yy') || (datefmt == 'M/d/yyyy')) {
      	sMonth = sStr1;
      	sDay = sStr2;
      	sYear = sStr3;
  	}
  	if ((datefmt == 'dd/MM/yyyy') || (datefmt == 'd/M/yy') || (datefmt == 'dd/MM/yy')) {
      	sDay = sStr1;
      	sMonth = sStr2;
      	sYear = sStr3;
  	}
  	if (datefmt == 'yy/MM/dd') {
      	sYear = sStr1;
      	sMonth = sStr2;
      	sDay = sStr3;
  	}
		      
  	if (sYear < 100) {
     	if (sYear < 50) {
        		sYear = '20' + sYear; }
     	else {
        		sYear = '19' + sYear; }
  	}
  	if (sMonth < 10) {
       	if (sMonth.length < 2)
          	sMonth = '0' + sMonth;
       	}
  	if (sDay < 10) {
       	if (sDay.length < 2)
              sDay = '0' + sDay;
   	}
   	
  	return sYear + sMonth + sDay;
}


function compareDateToNow(date1) {
	
	var now = new Date();
  	var sMonth = (now.getMonth() + 1).toString();
  	var sDay = now.getDate().toString();
  	var sYear = now.getFullYear().toString();
  	var sDateStr = '';

  	if (sMonth < 10) {
       	if (sMonth.length < 2){
          	sMonth = '0' + sMonth;
       	}
     }
  	if (sDay < 10) {
       	if (sDay.length < 2) {
              sDay = '0' + sDay;
          }
   	}

   	sDateStr = sYear + sMonth + sDay;

	if (formatDateForCompare(date1) > sDateStr){
  		return 1;
  	}else if (formatDateForCompare(date1) == sDateStr){
  		return 0;
  	}else{
  		return -1;
  	}
}

function compareDates(date1, date2){
	if (parseInt(formatDateForCompare(date1)) > parseInt(formatDateForCompare(date2))) {
		return 1;
	}else if(parseInt(formatDateForCompare(date1)) == parseInt(formatDateForCompare(date2))) {
  		return 0;
  	}else {
  		return -1;
  	}
}

function checkMoney(field)
{
	var value = field.value;
	
	if (value.indexOf(".", value.indexOf(".") + 1) != -1)
	{
		alert("Numeric can not have more than one characters \".\" !");
		field.value = "";
		field.focus();
		return;
	}
	
	value = value.replace(",", "");
	value = value.replace(",", "");
	value = value.replace(",", "");
	value = value.replace(",", "");
	value = value.replace(",", "");
	value = value.replace(",", "");
	value = value.replace(",", "");
	value = value.replace(",", "");
	value = value.replace(",", "");
	value = value.replace(",", "");
	value = value.replace(",", "");

	if (isNaN(value))
	{
		alert("Not a numeric1!");
		field.value = "";
		field.focus();
		return;
	}
	
	if (value < 0)
	{
		alert("Must not a negative numeric!");
		field.value = "";
		field.focus();
		return;
	}
	
	FormatNumericWithSeparate(field);
}

function checkNumeric(field)
{
	var value = field.value;
	if (isNaN(value))
	{
		GetAlertError(iTotal,DSAlert,"0013");
		field.value = "";
		field.focus();		
	}
	if (value < 0)
	{
		GetAlertError(iTotal,DSAlert,"0012");
		field.value = "";
		field.focus();		
	}
}

//KhanhMT - 10/08/2004 - Formmat so ra dang co phan cach hang ngan
function FormatNumericWithSeparate(field)
{
	var DecimalNumber = 2;
	var DecimalChar = ".";
	var SeperateChar = ",";
	var ValueString = field.value;
	var ValueStringAfterDecimal = "";
	var ValueStringNew = "";

	if (field.value < 0) 
		ValueString = ValueString.substring(2, ValueString.length - 1); //Bo dau tru

	if (ValueString.indexOf(DecimalChar) != -1)
	{
		ValueStringAfterDecimal = ValueString.substring(ValueString.indexOf(DecimalChar), ValueString.indexOf(DecimalChar) + 1 + DecimalNumber) // Tinh luon dau cham
		ValueString = ValueString.substring(0, ValueString.indexOf(DecimalChar))
	}

	if (ValueStringAfterDecimal.indexOf(SeperateChar) != -1)
	{
		alert("Not a numeric!");
		field.value = "";
		field.focus();		
		return;
	}

	ValueString = ValueString.replace(",", "");
	ValueString = ValueString.replace(",", "");
	ValueString = ValueString.replace(",", "");
	ValueString = ValueString.replace(",", "");
	ValueString = ValueString.replace(",", "");
	ValueString = ValueString.replace(",", "");
	ValueString = ValueString.replace(",", "");
	ValueString = ValueString.replace(",", "");
	ValueString = ValueString.replace(",", "");
	ValueString = ValueString.replace(",", "");

	ValueStringNew = "";

	while (ValueString.length >3)
	{
		ValueStringNew = SeperateChar + ValueString.substring(ValueString.length - 3, ValueString.length) + ValueStringNew;
		ValueString = ValueString.substring(0, ValueString.length - 3);
	}
	if (DecimalNumber != 0)
		ValueStringNew = ValueString + ValueStringNew + ValueStringAfterDecimal;
	else
		ValueStringNew = ValueString + ValueStringNew;

	if (field.value < 0) 
		ValueStringNew = "- " + ValueStringNew;
	
	field.value = ValueStringNew;
}
function Confirm_Delete()
{
	if (confirm("Delete checked recored, are you sure?"))
		return true;
	else
		return false;
}
