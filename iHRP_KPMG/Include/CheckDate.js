function CheckDate1(field){
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
   day = DateValue.substr(0,2);
   month = DateValue.substr(2,2);   
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
      DateField.value = day + seperator + month + seperator + year;
      if (DateField.value == "")  	
      {
		alert("Ngày không đúng định dạng!");
		DateField.focus();	
      }      
   }
   /* Error-message if err != 0 */
   else {
		DateField.value = "";
		DateField.focus();
		alert("Ngày không đúng định dạng!");      
   }
}

function CheckMonthYear(field){
var checkstr = "0123456789";
var DateField = field;
var Datevalue = "";
var DateTemp = "";
var seperator = "/";
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
   /* Always change date to 6 digits - string*/
   /* if year is entered as 2-digit / always assume 20xx */
   if (DateValue.length == 4) {
      DateValue = DateValue.substr(0,2) + '20' + DateValue.substr(2,2); }  
   
   if (DateValue.length != 6) {
      err = 19;}
   /* year is wrong if year = 0000 */
   year = DateValue.substr(2,4);
   if (year == 0) {
      err = 20;
   }
   /* Validation of month*/
   month = DateValue.substr(0,2);   
   if ((month < 1) || (month > 12)) {
      err = 21;
   }
 
   /* if 00 ist entered, no error, deleting the entry */
   if ((month == 0) && (year == 00)) {
      err = 0; month = ""; year = ""; seperator = "";
   }
   
   if (err == 0) {
      DateField.value = month + seperator + year;
      if (DateField.value == "")  	
      {
		alert("Vui lòng nhập theo dạng mm/yyyy!");
		DateField.focus();	
      }      
   }
   /* Error-message if err != 0 */
   else {
		DateField.value = "";
		DateField.focus();
		alert("Vui lòng nhập theo dạng mm/yyyy!");
   }
}

function CheckDate(field){
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
   month = DateValue.substr(2,2);
   if ((month < 1) || (month > 12)) {
      err = 21;
   }
   /* Validation of day*/
   day = DateValue.substr(0,2);
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
      DateField.value = day + seperator + month + seperator + year;
   }
   /* Error-message if err != 0 */
   else {
		alert("Ngày không đúng định dạng!");
		DateField.value = "";
		DateField.focus();
		
      //DateField.select();
	  //DateField.focus();
   }
}

function CheckMonthYear(field){
var checkstr = "0123456789";
var DateField = field;
var Datevalue = "";
var DateTemp = "";
var seperator = "/";
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
   /* Always change date to 6 digits - string*/
   /* if year is entered as 2-digit / always assume 20xx */
   if (DateValue.length == 4) {
      DateValue = DateValue.substr(0,2) + '20' + DateValue.substr(2,2); }  
   
   if (DateValue.length != 6) {
      err = 19;}
   /* year is wrong if year = 0000 */
   year = DateValue.substr(2,4);
   if (year == 0) {
      err = 20;
   }
   /* Validation of month*/
   month = DateValue.substr(0,2);   
   if ((month < 1) || (month > 12)) {
      err = 21;
   }
 
   /* if 00 ist entered, no error, deleting the entry */
   if ((month == 0) && (year == 00)) {
      err = 0; month = ""; year = ""; seperator = "";
   }
   
   if (err == 0) {
      DateField.value = month + seperator + year;
      if (DateField.value == "")  	
      {
		alert("Vui lòng nhập đúng định dạng mm/yyyy!");
		DateField.focus();	
      }      
   }
   /* Error-message if err != 0 */
   else {
		DateField.value = "";
		DateField.focus();
		alert("Vui lòng nhập đúng định dạng mm/yyyy!");
   }
}
function trim(txt) {
    txt = txt.replace(/^(\s)+/, '');
    txt = txt.replace(/(\s)+$/, '');
   	return txt;
}
function validateTime(time1) {
	var time = time1.value;
	var iSepPos = time.indexOf(':');
	var sTimeStr = time;
	var sStr1 = new String;
	var sStr2 = new String;
	var sStr3 = new String;

	sStr1 = 'x';
	sStr2 = 'x';
	sStr3 = 'x';

	if (trim(time) == '') {
		return true;
	}

	if (iSepPos > 0) {
	    sStr1 = sTimeStr.substring(0,iSepPos);
	    sTimeStr = sTimeStr.substring(iSepPos + 1, sTimeStr.length);
	}

	iSepPos = sTimeStr.indexOf(':');
	if (iSepPos > 0) {
	    sStr2 = sTimeStr.substring(0,iSepPos);
	    sStr3 = sTimeStr.substring(iSepPos + 1, sTimeStr.length);
	}
	else {
	    sStr2 = sTimeStr;
	    sStr3 = '00';
	}

	var sHour = sStr1;
	var sMinute = sStr2;
	var sSecond = sStr3;

	if (isNaN(sHour)) {
		displayMessage('COMM',1030,time,'00:00:00');
	    return false;
	}
	if (isNaN(sMinute)) {
	    displayMessage('COMM',1030,time,'00:00:00');
	    return false;
	}
	if (isNaN(sSecond)) {
	    displayMessage('COMM',1030,time,'00:00:00');
	    return false;
	}

	if (sValid) {
	    if ((sHour < 0) || (sHour > 23)) {
	        displayMessage('COMM',1030,time,'00:00:00');
	        return false;
	    }
	    if ((sMinute < 0) || (sMinute > 59)) {
	        displayMessage('COMM',1030,time,'00:00:00');
	        return false;
	    }
	    if ((sSecond < 0) || (sSecond > 59)) {
	        displayMessage('COMM',1030,time,'00:00:00');
	        return false;
	    }
	}

	if (sValid) {
	    time = sHour + ':' + sMinute + ':' + sSecond;
	}
	return true;

}

function CheckFormat_HHMM(field)
{
	if (field.value == "" )
	{
		return
	}
	
	var err = 0;	
	var Err1 = GetAlertText(iTotal,DSAlert,"0033");	
			
	var strValue = field.value;	
	var strValue1
	var strValue2
	var strValue3
	
		
	if (strValue.length < 4)
		err = 1;
	else 
	{
			strValue1 = strValue.substr(0,2);

			if (strValue.length = 4)
				strValue3 = strValue.substr(2,2);
			if (strValue.length >= 5)
				strValue3 = strValue.substr(3,2);
	}
		
	if (isNaN (strValue1))
		err = 1;
	else
		if (parseInt(strValue1) < 0 || parseInt(strValue1) >= 24)
			err =1; 
		
	if (isNaN (strValue3))
		err = 1;
	else
		if (parseInt(strValue3) < 0 || parseInt(strValue3) > 59)
			err =1; 	

	if (err != 0 )
	{
		GetAlertTextPopUp(Err1);
		field.focus();
		field.value = "";
	}
	else
		field.value = strValue1 + ":" + strValue3
}

function CheckFormat_HHMM(field)
{
	if (field.value == "" )
	{
		return
	}
	var err = 0;	
	var Err1 = GetAlertText(iTotal,DSAlert,"0033");		
			
	var strValue = field.value;	
	var strValue1
	var strValue2
	var strValue3
	
		
	if (strValue.length < 4)
		err = 1;
	else 
	{
			strValue1 = strValue.substr(0,2);

			if (strValue.length = 4)
				strValue3 = strValue.substr(2,2);
			if (strValue.length >= 5)
				strValue3 = strValue.substr(3,2);
	}
		
	if (isNaN (strValue1))
		err = 1;
	else
		if (parseInt(strValue1) < 0 || parseInt(strValue1) >= 24)
			err =1; 
		
	if (isNaN (strValue3))
		err = 1;
	else
		if (parseInt(strValue3) < 0 || parseInt(strValue3) > 59)
			err =1; 	

	if (err != 0 )
	{
		GetAlertTextPopUp(Err1);
		field.focus();
		field.value = "";
	}
	else
		field.value = strValue1 + ":" + strValue3
}

