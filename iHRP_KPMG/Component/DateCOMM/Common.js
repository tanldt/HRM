function setDDLToTXT(ddlName,txtName)
{
	var ddl = document.all(ddlName);
	var txt = document.all(txtName);	
	if (ddl.value == '')	
	{		
		txt.disabled = 0 ;		
		txt.value = "";
		txt.focus();
	}
	else
	{				
		txt.disabled = 1;		
		txt.value = ddl.options[ddl.selectedIndex].text;
	}
}



function validateEmail(email) {
	if (trim(email) == '') {
		return true;
	} else if (email.indexOf('@') == -1 || email.indexOf('.') == -1) {
	    displayMessage('COMM',1016);
	    return false;
	} else {
	    return true;
	}
}

/*function isInteger(numString) {
	if (isNaN(numString)) {
		return false;
	} else {
		return (numString.indexOf('.') > 0) ? false : true;
	}
}*/

// This function currently expects valid times in the form of xx:xx:xx,
// using a military (24-hour) clock
function validateTime(time) {

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
	    if ((sHour < 0) || (sHour > 24)) {
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


function validateMoney(amt, neg) {

  var cs  = platform.document.GLOBAL.CURRENCY_SYMBOL.value;    // Currency Symbol
  var ts  = platform.document.GLOBAL.THOUSANDS_SEPERATOR.value;    // Thousands Seperator
  var ds  = platform.document.GLOBAL.DECIMAL_SEPERATOR.value;    // Decimal Symbol

  if (trim(amt) == '') {
  	return true;
  }

  var moneyExp = new RegExp("^ *" + (neg ? "(-?) *" : "") + "(\\" + cs + "?) *(\\d{1,3})(\\" + ts + "?\\d{3})*(\\" + ds + "\\d\\d)? *$");

  if (moneyExp.test(amt)) {
    return true;
  } else {
  	displayMessage('COMM',1039, amt, cs, ts, ds);
    return false;
  }
}


function validateNumber(amt, neg) {

  var cs  = platform.document.GLOBAL.CURRENCY_SYMBOL.value;    // Currency Symbol
  var ts  = platform.document.GLOBAL.THOUSANDS_SEPERATOR.value;    // Thousands Seperator
  var ds  = platform.document.GLOBAL.DECIMAL_SEPERATOR.value;    // Decimal Symbol

  if (trim(amt) == '') {
  	return true;
  }

  var numberExp = new RegExp("^ *" + (neg ? "(-?) *" : "") + "(\\" + cs + "?) *(\\d{1,3})(\\" + ts + "?\\d{3})*(\\" + ds + "\\d\\d)? *$");

  if (numberExp.test(amt)) {
    return true;
  } else {
  	displayMessage('COMM',1040, amt, ts, ds);
    return false;
  }
}


function validateDate(datestr) {

	var datefmt = platform.document.GLOBAL.DATE_FORMAT.value;    // DATE_FORMAT
	var datesep = platform.document.GLOBAL.DATE_SEPERATOR.value;    // DATE_SEPERATOR
	var dateformatForDisplay = platform.document.GLOBAL.DATE_FORMAT_FOR_DISPLAY.value;    // DATE_SEPERATOR

	var iSepPos = datestr.indexOf(datesep);
	var sDateStr = datestr;
	var sStr1 = new String;
	var sStr2 = new String;
	var sStr3 = new String;
	var IsLeap = false;
	var iYear = 0;

	if (trim(datestr) == '') {
		return true;
	}

	if (datestr.length < 6) {
	    displayMessage('COMM',1019,datestr,dateformatForDisplay);
	    return false;
	}

	if (iSepPos > 0) {
	    sStr1 = sDateStr.substring(0,iSepPos);
	    sDateStr = sDateStr.substring(iSepPos + 1, sDateStr.length);
	}
	else {
	    displayMessage('COMM',1019,datestr,dateformatForDisplay);
	    return false;
	}
	iSepPos = sDateStr.indexOf(datesep);
	if (iSepPos > 0) {
	    sStr2 = sDateStr.substring(0,iSepPos);
	    sStr3 = sDateStr.substring(iSepPos + 1, sDateStr.length);
	}
	else {
	    displayMessage('COMM',1019,datestr,dateformatForDisplay);
	    return false;
	}

	while (datefmt.substr(datefmt.length-1,1)==' ') {
	   datefmt = datefmt.substr(0,datefmt.length-1);
	}

	var sMonth = '';
	var sDay = '';
	var sYear = '';
	if ((datefmt == 'MM/dd/yyyy') || (datefmt == 'M/d/yy') || (datefmt == 'MM/dd/yy') || (datefmt == 'M/d/yyyy')) {
	    sMonth = sStr1;
	    sDay = sStr2;
	    sYear = sStr3;
	}
	if ((datefmt == 'dd/MM/yyyy') || (datefmt == 'd/M/yy') || (datefmt == 'dd/MM/yy') || (datefmt == 'd/M/yyyy')) {
	    sDay = sStr1;
	    sMonth = sStr2;
	    sYear = sStr3;
	}
	if (datefmt == 'yy/MM/dd') {
	    sYear = sStr1;
	    sMonth = sStr2;
	    sDay = sStr3;
	}

	if ((((sYear.length - 0) > 4) || (((sYear.length - 0) < 4) && ((sYear.length - 0) != 2))) || ((sDay.length - 0) > 2) || ((sMonth.length - 0) > 2)    ) {
	    displayMessage('COMM',1019,datestr,dateformatForDisplay);
	    return false;
	}

	if (isNaN(sDay)) {
	    displayMessage('COMM',1019,datestr,dateformatForDisplay);
	    return false;
	}
	if (isNaN(sMonth)) {
	    displayMessage('COMM',1019,datestr,dateformatForDisplay);
	    return false;
	}
	if (isNaN(sYear)) {
	    displayMessage('COMM',1019,datestr,dateformatForDisplay);
	    return false;
	}

	iYear = iYear + sYear;

	if((iYear % 4) == 0) {
	   if((iYear % 100) == 0) {
	      if((iYear % 400) == 0) {
	         IsLeap = true;
	      }
	      else {
	         IsLeap = false;
	      }
	   }
	   else {
	      IsLeap = true;
	   }
	}
	else {
	   IsLeap = false;
	}

	if ((sMonth < 1) || (sMonth > 12)) {
		displayMessage('COMM',1019,datestr,dateformatForDisplay);
	    return false;
	}
	else {
		if ((sMonth == 1) || (sMonth == 3) || (sMonth == 5) || (sMonth == 7) || (sMonth == 8) || (sMonth == 10) || (sMonth == 12)) {
    		if ((sDay < 1) || (sDay > 31)) {
		    	displayMessage('COMM',1019,datestr,dateformatForDisplay);
		    	return false;
	    	}
		}
		if ((sMonth == 4) || (sMonth == 6) || (sMonth == 9) || (sMonth == 11)) {
			if ((sDay < 1) || (sDay > 30)) {
		    	displayMessage('COMM',1019,datestr,dateformatForDisplay);
		    	return false;
			}
		}
		if (sMonth == 2) {
	    	if (IsLeap == true) {
	        	if ((sDay < 1) || (sDay > 29)) {
	            	displayMessage('COMM',1019,datestr,dateformatForDisplay);
	    			return false;
	            }
	        }
	        else {
	        	if ((sDay < 1) || (sDay > 28)) {
	            	displayMessage('COMM',1019,datestr,dateformatForDisplay);
	    			return false;
				}
			}
		}
	}
 	return true;
}

// This function removes space from both ends of a string.
function trim(txt) {
    txt = txt.replace(/^(\s)+/, '');
    txt = txt.replace(/(\s)+$/, '');
   	return txt;
}

// This function checks for invalid characters and displays given error message.
function validCharacters(eObject, eMsgArea, eMsgID) {
	var aInvalidChars = new Array('~','!','%','^', '&', '*', '+', '=', '}', '{', '[', ']', '|', '\\', ':', '<', '>');
	var sValue        = eObject.value;
	var sInvalids = '';

	for (var i = 0; i < aInvalidChars.length; i++) {
		if (sValue.indexOf(aInvalidChars[i]) > -1) {
			sInvalids = sInvalids + aInvalidChars[i];
		}
	}

	if (sInvalids.length > 0) {
		displayMessage('COMM', 1106, sValue, sInvalids);
		return false;
	} else {
		return true;
	}
}

function checkBoxOnClick(formName, fieldNum){
	field = eval('document.'+formName+'.UF'+fieldNum);

	if (field.value == 0) {
		field.value = 1;
	} else {
		field.value = 0;
	}
}

function checkUFValues(UFform) {
	var field;
	var fieldName;
	var fieldType;
	var fieldValue;
	var mandatory;

	for (var x = 0; x < UFform.elements.length; x++) {

    	// get the field
    	field = UFform.elements[x];
		//fieldName = field.name.substring(0,3);
		fieldName = field.name;

		// if it's a user field
		if (fieldName.substring(0,2) == 'UF' && fieldName.substring(0,3) != 'UFM' && fieldName.indexOf('_') < 0) {
			fieldType = eval('UFform.' + fieldName + '_TYPE.value');
			mandatory = eval('UFform.' + fieldName + '_MANDATORY.value');
			if (fieldType == '6') {
				continue;
			} else {
				fieldValue = eval('UFform.' + fieldName + '.value');
			}

			// check for required fields
			if (mandatory == 1 && (fieldValue == '' || fieldValue == null) && fieldType != 7) {
				displayMessage('COMM', 1028);
				field.focus();
				return false;
			}

			// validation is only performed on date, numeric, and text area fields
			if (fieldType == '2') { // Date
				if (!(validateDate(fieldValue))) {
					field.focus();
					return false;
				}
			} else if (fieldType == '3') { // Numeric
				if (!(validateNumber(fieldValue, true))) {
					field.focus();
					return false;
				}
			} else if (fieldType == '4') { // text
				if (! validCharacters(field)) {
					field.focus();
					return false;
				}
			}  else if (fieldType == '5') { // TextArea
				if (fieldValue.length > 24000) {
					displayMessage('COMM', 1027);
					field.focus();
					return false;
				} else if (! validCharacters(field)) {
					field.focus();
					return false;
				}
			}
		}
	}

	return true;
}

function clearForm(formObj) {

	formObj.reset();
	var fld;

	for (var i=0; i < formObj.elements.length; i++) {
		fld = formObj.elements[i];

		if (
			(fld.type == 'hidden')		||
			(fld.type == 'text')		||
			(fld.type == 'textarea')	||
			(fld.type == 'password')
		   )
		{
			fld.value = '';
		}
		else if ((fld.type == 'select-one') || (fld.type == 'select-multiple'))
		{
			fld.selectedIndex = -1;
		}
		else if ((fld.type == 'checkbox') || (fld.type == 'radio'))
		{
			fld.checked = false;
		}
	}

}

function formatDateForCompare(datestr) {
 	var datefmt = platform.document.GLOBAL.DATE_FORMAT.value;    // DATE_FORMAT
	var datesep = platform.document.GLOBAL.DATE_SEPERATOR.value;    // DATE_SEPERATOR

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

  	datefmt = trim(datefmt.replace(datesep, '/'));
  	var sMonth = '';
  	var sDay = '';
  	var sYear = '';
  	if ((datefmt == 'MM/dd/yyyy') || (datefmt == 'M/d/yy') || (datefmt == 'MM/dd/yy')) {
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

function FirstDateOfWeek(dDate){	
	var offset;
	var dt = dDate;

	offset = dt.getDay() - 1;
   	dt.setDate(dt.getDate() - offset);
	return dt;
}

function LastDateOfWeek(dDate){	
	var offset;
	var dt = dDate;

	offset = 6 - dt.getDay() + 1;
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
function FirstDateOfYear(dDate){	
	var dt = dDate;
   	dt.setMonth(0);
   	dt.setDate(1);
	return dt;
}

function LastDateOfYear(dDate){	
	var dt = dDate;
   	dt.setMonth(11);
   	dt.setDate(31);
	return dt;
}

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
	
	if (aDDMMCCYY[2] >= 1900)
	{
		if (dtest.getDate() != aDDMMCCYY[0] || dtest.getMonth() +1 != aDDMMCCYY[1] || dtest.getFullYear() != aDDMMCCYY[2])    
		{    
			retval = 2    			
		}    
	}else{
		retval = 3
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
**** Auto Rsize the Multiline Textbox  ******
********************************/
function ResizeTextbox(objName){    
	eval('document.forms[0]' + '.all("' + objName + '")').setAttribute('rows',eval('document.forms[0]' + '.all("' + objName + '")').getAttribute('rows') + 1)
}    

/********************************
**** Get Email Address List******
********************************/
function AddAddress(Name, EMail,FullName ,Field1, Field2) {
	if (Field1 != null) eval('address = opener.document.forms[0].' + Field1);
	if (Field2 != null) eval('namelist = opener.document.forms[0].' + Field2);
	
	if (address == null) return;
	if (namelist == null) return;
	
	if (EMail.length == 0) return;
	
	if (address.value.length > 0){
		address.value += '; ';
	}	
	if (namelist.value.length > 0){
		namelist.value += '; ';
	}	

	
	if (EMail.indexOf(',') != -1 || EMail.indexOf(';') != -1)
	{	
		address.value += EMail;
		namelist.value += Name;
	}	
	else{
		address.value += EMail;
		namelist.value += Name;
	}

}

/**************************************
**** Show report***********************
option = 1: doi voi cac trang user control
       <> 1: binh thuong
**************************************/
function ShowReport(report, sql, param, value, option)
{
	width = screen.width - 20;
	height = screen.height - 100;
	l = (screen.width - 10 - width)/2;
	t = (screen.height -  10 - height)/2;	
	if (option == 1)
		opt = '';
	else
		opt = '../';
	FileWindow = window.open(opt + '../Report/COMM_ReportGen.asp?report=' + report + '&sql=' + sql + '&formula=' + param + '&Value=' + value ,'_blank','toolbar=0,scrollbars=1, alwaysRaised=Yes, top=' + t + ', left=' + l + ', width=' + width + ', height=' + height + ',1 ,align=center');
	FileWindow.focus();				
}

// This function currently expects valid date in the form of dd/mm/yy,


function ValidDate(op, dataType, val,val2) {

	val = eval('document.forms[0]' + '.all("' + val + '")')
	val2 = eval('document.forms[0]' + '.all("' + val2 + '")')
	op  = val.value
	
	if(val2.value != 'Ngày giao') return;
	if(val.value == '') return;
	
	
    function GetFullYear(year) {
        return (year + parseInt(val.century)) - ((year < val.cutoffyear) ? 0 : 100);
    }
    var num, cleanInput, m, exp;

    if (dataType == "Integer") {
        exp = /^\s*[-\+]?\d+\s*$/;
        if (op.match(exp) == null) 
            return null;
        num = parseInt(op, 10);
        return (isNaN(num) ? null : num);
    }
    else if(dataType == "Double") {
        exp = new RegExp("^\\s*([-\\+])?(\\d+)?(\\" + val.decimalchar + "(\\d+))?\\s*$");
        m = op.match(exp);
        if (m == null)
            return null;
        cleanInput = m[1] + (m[2].length>0 ? m[2] : "0") + "." + m[4];
        num = parseFloat(cleanInput);
        return (isNaN(num) ? null : num);            
    } 
    else if (dataType == "Currency") {
        exp = new RegExp("^\\s*([-\\+])?(((\\d+)\\" + val.groupchar + ")*)(\\d+)"
                        + ((val.digits > 0) ? "(\\" + val.decimalchar + "(\\d{1," + val.digits + "}))?" : "")
                        + "\\s*$");
        m = op.match(exp);
        if (m == null)
            return null;
        var intermed = m[2] + m[5] ;
        cleanInput = m[1] + intermed.replace(new RegExp("(\\" + val.groupchar + ")", "g"), "") + ((val.digits > 0) ? "." + m[7] : 0);
        num = parseFloat(cleanInput);
        return (isNaN(num) ? null : num);            
    }
    else if (dataType == "Date") {
		
        var yearFirstExp = new RegExp("^\\s*((\\d{4})|(\\d{2}))([-./])(\\d{1,2})\\4(\\d{1,2})\\s*$");
        m = op.match(yearFirstExp);
        
        var day, month, year;
        if (m != null && (m[2].length == 4 || val.dateorder == "ymd")) {
		    day = m[6];
            month = m[5];
            year = (m[2].length == 4) ? m[2] : GetFullYear(parseInt(m[3], 10))
        
        }
        else {
            if (val.dateorder == "ymd"){
                alert('Ngày không h?p l?');
                event.returnValue = false;		
                return;
            }						
            var yearLastExp = new RegExp("^\\s*(\\d{1,2})([-./])(\\d{1,2})\\2((\\d{4})|(\\d{2}))\\s*$");
            m = op.match(yearLastExp);
            if (m == null) {
				alert('Ngày không h?p l?');
                event.returnValue = false;		
                return;
            }
            if (val.dateorder == "mdy") {
                day = m[3];
                month = m[1];
            }
            else {
                day = m[1];
                month = m[3];
            }
            year = (m[5].length == 4) ? m[5] : GetFullYear(parseInt(m[6], 10))
        }
        month -= 1;
        var date = new Date(year, month, day);
		
		if (date.getFullYear()>=1900){
			if(typeof(date) == "object" && year == date.getFullYear() && month == date.getMonth() && day == date.getDate()){
				return date.valueOf()
			}else{
				alert('Ngày không h?p l?');
				event.returnValue = false;		
				return;
			}
		}else{
			alert('Ngày không h?p l?');
			event.returnValue = false;		
			return;			
        }
        
        //return (typeof(date) == "object" && year == date.getFullYear() && month == date.getMonth() && day == date.getDate()) ? date.valueOf() : false;
      
    }
    else {
       
        return op.toString();
        
    }
}

// This function currently expects valid times in the form of xx:xx:xx,
// using a military (24-hour) clock

function checkTime(time){
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

	if(sHour=='' || sMinute=='' || sSecond=='') {
		alert('Gi? ph?i có d?ng 00:00');
	    return false;
	}
	
	if (isNaN(sHour)) {
		alert('Gi? ph?i có d?ng 00:00');
	    return false;
	}
	if (isNaN(sMinute)) {
	    alert('Gi? ph?i có d?ng 00:00');
	    return false;
	}
	if (isNaN(sSecond)) {
		alert('Gi? ph?i có d?ng 00:00');
	    return false;
	}

	
	if ((sHour < 0) || (sHour > 24)) {
	    alert('Gi? không quá 24 !');
	    return false;
	}
	if ((sMinute < 0) || (sMinute > 59)) {
	    alert('Phút không quá 60 !');
	    return false;
	}
	if ((sSecond < 0) || (sSecond > 59)) {
		alert('Giây không quá 60 !');
	    return false;
	}

	time = sHour + ':' + sMinute + ':' + sSecond;
	return true;
}

function ShowAttachedFile(path,filename){
	if(filename ==''){
		alert('Chưa đính kèm toàn văn');
		return;
	}else{
		var width = screen.width;
		var height = screen.height;
		var l = 0 //(screen.width - 10 - width)/2;
		var t = 0 //(screen.height -  10 - height)/2;	
		FileWindow = window.open( path + filename ,'AttachedFile','toolbar=0,scrollbars=1, alwaysRaised=Yes, top=' + t + ', left=' + l + ', width=' + width + ', height=' + height + ',1 ,align=center');
		FileWindow.focus();
	}					
}

function ShowUploadWindow(objID,dirName){
	var width = screen.width;
	
	var height = screen.height;
	var l = (screen.width - 10 - width)/2;
	var t = (screen.height -  10 - height)/2;	
	FileWindow = window.open('../DesktopModules/UploadFile.aspx?ObjID=' + objID + '&ModuleName=' + dirName,'UploadFiles','toolbar=0,scrollbars=1, alwaysRaised=Yes, top=' + t + ', left=' + l + ', width=' + width + ', height=' + height/2 + ',1 ,align=center');
	FileWindow.focus();
}
//****************************************************

function validateEmail(email) {

	if (trim(email) == '') {
		return true;
	} else {
		var emailExp = new RegExp("^[\\w\\-\\.]+\\@[\\w\\-]+\\.[\\w\\-]+");

		if (emailExp.test(email))  {
			return true;
		} else {
			//displayMessage('COMM', 1016);
			return false;
		}
	}

}

function validCharacter(eText) {
	var aInvalidChars = new Array('~','!','%','^','$','#','&','*','+','=','}','{','[',']','|','\\',':','<','>');
	var sFilterValue = eval('document.forms[0].all("' + eText + '")').value;
	var sInvalids = '';
	
	for (var i = 0; i < aInvalidChars.length; i++) {
		if (sFilterValue.indexOf(aInvalidChars[i]) > -1) {
			sInvalids = sInvalids + aInvalidChars[i];
		}
	}
	if (sInvalids.length > 0) {
		alert('Có ký tự đặc biệt');		
		document.forms[0].all(eText).focus();		
		return false;
	} else {
		return true;
	}
}

function Change(obj, obj2)
{
	eval('document.forms[0].all("' + obj + '")').value = 'Y';

	strCheck = '13';	
	if (strCheck.indexOf(event.keyCode) != -1)
		eval('document.forms[0].all("' + obj2 + '")').focus();
}

function ShowHideDiv(objDiv, objImg){
	if (objDiv.style.display == ""){
		objDiv.style.display = "none";
		objImg.src="../../images/plus.gif";
	}else{
		objDiv.style.display = "";
		objImg.src="../../images/minus.gif";
	}
}


function isNumeric(objName)   
{   
	if (objName.length!=0)   
	{   
		if (isNaN(eval('document.forms[0].all("' + objName + '")').value)){   
			alert("Dữ liệu nhập phải là kiểu số"); 
			eval('document.forms[0].all("' + objName + '")').focus();
			return false;
		}	
	}   
}


/////////////////////////////////////////////////////////////////////////////////////////
/*
Description: Tuấn Anh thêm các hàm dưới đây
Date: 04/06/2004
*/

/////////////////////////////////////////////////////////////////////////////////////////
/* Construct for the object getParamURL */
var getParamURL = function(_url)
{
	this.URL = _url;
	this.URLls = function() {return this.URL;}
	this.getParameter = function(_para)
		{
			var strURL= this.URL;
			var strGet = _para + "=";
			var _return="";
			
			strURL = strURL.slice(strURL.indexOf("?")+1, strURL.length);
			pos = strURL.indexOf(strGet);
			i = pos + strGet.length;
			if (pos != -1){
				while (i<=strURL.length && strURL.charAt(i) != "&"){
					_return = _return + strURL.charAt(i);
					i++;
				}
			}else{
				_return = null;
			}
			return _return;
		}
}

/////////////////////////////////////////////////////////////////////////////////////////
/* Trim spaces in the left side of the string */
function LTrim(iStr)
{
	while (iStr.charCodeAt(0) <= 32){
		iStr=iStr.substr(1);
	}
	return iStr;
}

/////////////////////////////////////////////////////////////////////////////////////////
/* Trim spaces in the right side of the string */
function RTrim(iStr)
{
	while (iStr.charCodeAt(iStr.length - 1) <= 32){
		iStr=iStr.substr(0, iStr.length - 1);
	}
	return iStr;
}

/////////////////////////////////////////////////////////////////////////////////////////
function swapClass(obj, cls)
{	obj.className = cls; }

/////////////////////////////////////////////////////////////////////////////////////////
function statusBar(obj)
{	window.status=obj.outerText; }

/////////////////////////////////////////////////////////////////////////////////////////
function getCookieVal(offset) {
	var endstr = document.cookie.indexOf(";", offset);
	if (endstr == -1)
	endstr = document.cookie.length;
	return unescape(document.cookie.substring(offset, endstr));
}

/////////////////////////////////////////////////////////////////////////////////////////
function GetCookie (name) {
	var arg = name + "=";
	var alen = arg.length;
	var clen = document.cookie.length;
	var i = 0;
	while (i<clen) {
		var j = i + alen;
		if (document.cookie.substring(i,j) == arg)
		return getCookieVal(j);
		i = document.cookie.indexOf(" ", i) + 1;
		if (i=0)
		break;
	}
	return null;
}

/////////////////////////////////////////////////////////////////////////////////////////
function SetCookie (name, value) {
	var argv = SetCookie.arguments;
	var argc = SetCookie.arguments.length;
	var expires = (argc > 2) ? argv[2] : null;
	var path = (argc > 3) ? argv[3] : null;
	var domain = (argc > 4) ? argv[4] : null;
	var secure = (argc > 5) ? argv[5] : false;
	
	document.cookie = name + "=" + escape(value) + ((expires == null) ? "" : ("; expires=" + expires.toGMTString()))
						+ ((path == null) ? "" : ("; path=" + path)) + ((domain == null)? "" : ("; domain=" + domain))
						+ ((secure == null)? "; secure" : "");
}

/////////////////////////////////////////////////////////////////////////////////////////
function DeleteCookie (name) {
	var exp =  new Date();
	exp.setTime (exp.getTime()-1);
	var cval = GetCookie(name);
	document.cookie = name + "=" + cval + "; expires=" + exp.toGMTString();
}

/*
function compareDates(date1, date2){
	if (formatDateForCompare(date1) > formatDateForCompare(date2)) {
		return 1;
	}else if(formatDateForCompare(date1) == formatDateForCompare(date2)) {
  		return 0;
  	}else {
  		return -1;
  	}
}*/

/********************************
**** Get Email Address List******
********************************/
function AddAddress(Name, EMail,FullName ,Field1, Field2) {
	if (Field1 != null) eval('address = opener.document.forms[0].' + Field1);
	if (Field2 != null) eval('namelist = opener.document.forms[0].' + Field2);
	
	if (address == null) return;
	if (namelist == null) return;
	
	if (EMail.length == 0) return;
	
	if (address.value.length > 0){
		address.value += '; ';
	}	
	if (namelist.value.length > 0){
		namelist.value += '; ';
	}	

	
	if (EMail.indexOf(',') != -1 || EMail.indexOf(';') != -1)
	{	
		address.value += EMail;
		namelist.value += Name;
	}	
	else{
		address.value += EMail;
		namelist.value += Name;
	}
}

function checkDate(object)
{
		
	if (object.value!=""  && isDate(object.value,"dd/MM/yyyy")==false)
	{
		object.focus();
		object.value='';
		alert("Invalid Date!");
		//return false;
	}	
}	

function checkNumber(obj)
{
	if (!isBlank(obj))    
	{    
		if (isNaN(obj.value))    
		{		    			
			alert("Invalid Number!");
			obj.focus();
			return ;   
		}					
	}
}	



// ------------------------------------------------------------------
// These functions use the same 'format' strings as the 
// java.text.SimpleDateFormat class, with minor exceptions.
// The format string consists of the following abbreviations:
// 
// Field        | Full Form          | Short Form
// -------------+--------------------+-----------------------
// Year         | yyyy (4 digits)    | yy (2 digits), y (2 or 4 digits)
// Month        | MMM (name or abbr.)| MM (2 digits), M (1 or 2 digits)
// Day of Month | dd (2 digits)      | d (1 or 2 digits)
// Hour (1-12)  | hh (2 digits)      | h (1 or 2 digits)
// Hour (0-23)  | HH (2 digits)      | H (1 or 2 digits)
// Hour (0-11)  | KK (2 digits)      | K (1 or 2 digits)
// Hour (1-24)  | kk (2 digits)      | k (1 or 2 digits)
// Minute       | mm (2 digits)      | m (1 or 2 digits)
// Second       | ss (2 digits)      | s (1 or 2 digits)
// AM/PM        | a                  |
//
// NOTE THE DIFFERENCE BETWEEN MM and mm! Month=MM, not mm!
// Examples:
//  "MMM d, y" matches: January 01, 2000
//                      Dec 1, 1900
//                      Nov 20, 00
//  "M/d/yy"   matches: 01/20/00
//                      9/2/00
//  "MMM dd, yyyy hh:mm:ssa" matches: "January 01, 2000 12:30:45AM"
// ------------------------------------------------------------------

//var MONTH_NAMES=new Array('January','February','March','April','May','June','July','August','September','October','November','December','Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sep','Oct','Nov','Dec');
var MONTH_NAMES=new Array('Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sep','Oct','Nov','Dec','January','February','March','April','May','June','July','August','September','October','November','December');
function LZ(x) {return(x<0||x>9?"":"0")+x}

// ------------------------------------------------------------------
// isDate ( date_string, format_string )
// Returns true if date string matches format of format string and
// is a valid date. Else returns false.
// It is recommended that you trim whitespace around the value before
// passing it to this function, as whitespace is NOT ignored!
// ------------------------------------------------------------------
function isDate(val,format) {
	var date=getDateFromFormat(val,format);
	if (date==0) { return false; }
	return true;
	}

// -------------------------------------------------------------------
// compareDates(date1,date1format,date2,date2format)
//   Compare two date strings to see which is greater.
//   Returns:
//   1 if date1 is greater than date2
//   0 if date2 is greater than date1 of if they are the same
//  -1 if either of the dates is in an invalid format
// -------------------------------------------------------------------
function compareDates(date1,dateformat1,date2,dateformat2) {
	var d1=getDateFromFormat(date1,dateformat1);
	var d2=getDateFromFormat(date2,dateformat2);
	if (d1==0 || d2==0) {
		return -1;
		}
	else if (d1 > d2) {
		return 1;
		}
	return 0;
	}

// ------------------------------------------------------------------
// formatDate (date_object, format)
// Returns a date in the output format specified.
// The format string uses the same abbreviations as in getDateFromFormat()
// ------------------------------------------------------------------
function formatDate(date,format) {
	format=format+"";
	var result="";
	var i_format=0;
	var c="";
	var token="";
	var y=date.getYear()+"";
	var M=date.getMonth()+1;
	var d=date.getDate();
	var H=date.getHours();
	var m=date.getMinutes();
	var s=date.getSeconds();
	var yyyy,yy,MMM,MM,dd,hh,h,mm,ss,ampm,HH,H,KK,K,kk,k;
	// Convert real date parts into formatted versions
	var value=new Object();
	if (y.length < 4) {y=""+(y-0+1900);}
	value["y"]=""+y;
	value["yyyy"]=y;
	value["yy"]=y.substring(2,4);
	value["M"]=M;
	value["MM"]=LZ(M);
	value["MMM"]=MONTH_NAMES[M-1];
	value["d"]=d;
	value["dd"]=LZ(d);
	value["H"]=H;
	value["HH"]=LZ(H);
	if (H==0){value["h"]=12;}
	else if (H>12){value["h"]=H-12;}
	else {value["h"]=H;}
	value["hh"]=LZ(value["h"]);
	if (H>11){value["K"]=H-12;} else {value["K"]=H;}
	value["k"]=H+1;
	value["KK"]=LZ(value["K"]);
	value["kk"]=LZ(value["k"]);
	if (H > 11) { value["a"]="PM"; }
	else { value["a"]="AM"; }
	value["m"]=m;
	value["mm"]=LZ(m);
	value["s"]=s;
	value["ss"]=LZ(s);
	while (i_format < format.length) {
		c=format.charAt(i_format);
		token="";
		while ((format.charAt(i_format)==c) && (i_format < format.length)) {
			token += format.charAt(i_format++);
			}
		if (value[token] != null) { result=result + value[token]; }
		else { result=result + token; }
		}
	return result;
	}
	
// ------------------------------------------------------------------
// Utility functions for parsing in getDateFromFormat()
// ------------------------------------------------------------------
function _isInteger(val) {
	var digits="1234567890";
	for (var i=0; i < val.length; i++) {
		if (digits.indexOf(val.charAt(i))==-1) { return false; }
		}
	return true;
	}
function _getInt(str,i,minlength,maxlength) {
	for (var x=maxlength; x>=minlength; x--) {
		var token=str.substring(i,i+x);
		if (token.length < minlength) { return null; }
		if (_isInteger(token)) { return token; }
		}
	return null;
	}
	
// ------------------------------------------------------------------
// getDateFromFormat( date_string , format_string )
//
// This function takes a date string and a format string. It matches
// If the date string matches the format string, it returns the 
// getTime() of the date. If it does not match, it returns 0.
// ------------------------------------------------------------------
function getDateFromFormat(val,format) {
	val=val+"";
	format=format+"";
	var i_val=0;
	var i_format=0;
	var c="";
	var token="";
	var token2="";
	var x,y;
	var now=new Date();
	var year=now.getYear();
	var month=now.getMonth()+1;
	var date=now.getDate();
	var hh=now.getHours();
	var mm=now.getMinutes();
	var ss=now.getSeconds();
	var ampm="";
	
	while (i_format < format.length) {
		// Get next token from format string
		c=format.charAt(i_format);
		token="";
		while ((format.charAt(i_format)==c) && (i_format < format.length)) {
			token += format.charAt(i_format++);
			}
		// Extract contents of value based on format token
		if (token=="yyyy" || token=="yy" || token=="y") {
			if (token=="yyyy") { x=4;y=4; }
			if (token=="yy")   { x=2;y=2; }
			if (token=="y")    { x=2;y=4; }
			year=_getInt(val,i_val,x,y);
			if (year==null) { return 0; }
			i_val += year.length;
			if (year.length==2) {
				if (year > 70) { year=1900+(year-0); }
				else { year=2000+(year-0); }
				}
			}
		else if (token=="MMM"){
			month=0;
			for (var i=0; i<MONTH_NAMES.length; i++) {
				var month_name=MONTH_NAMES[i];
				if (val.substring(i_val,i_val+month_name.length).toLowerCase()==month_name.toLowerCase()) {
					month=i+1;
					if (month>12) { month -= 12; }
					i_val += month_name.length;
					break;
					}
				}
			if ((month < 1)||(month>12)){return 0;}
			}
		else if (token=="MM"||token=="M") {
			month=_getInt(val,i_val,token.length,2);
			if(month==null||(month<1)||(month>12)){return 0;}
			i_val+=month.length;}
		else if (token=="dd"||token=="d") {
			date=_getInt(val,i_val,token.length,2);
			if(date==null||(date<1)||(date>31)){return 0;}
			i_val+=date.length;}
		else if (token=="hh"||token=="h") {
			hh=_getInt(val,i_val,token.length,2);
			if(hh==null||(hh<1)||(hh>12)){return 0;}
			i_val+=hh.length;}
		else if (token=="HH"||token=="H") {
			hh=_getInt(val,i_val,token.length,2);
			if(hh==null||(hh<0)||(hh>23)){return 0;}
			i_val+=hh.length;}
		else if (token=="KK"||token=="K") {
			hh=_getInt(val,i_val,token.length,2);
			if(hh==null||(hh<0)||(hh>11)){return 0;}
			i_val+=hh.length;}
		else if (token=="kk"||token=="k") {
			hh=_getInt(val,i_val,token.length,2);
			if(hh==null||(hh<1)||(hh>24)){return 0;}
			i_val+=hh.length;hh--;}
		else if (token=="mm"||token=="m") {
			mm=_getInt(val,i_val,token.length,2);
			if(mm==null||(mm<0)||(mm>59)){return 0;}
			i_val+=mm.length;}
		else if (token=="ss"||token=="s") {
			ss=_getInt(val,i_val,token.length,2);
			if(ss==null||(ss<0)||(ss>59)){return 0;}
			i_val+=ss.length;}
		else if (token=="a") {
			if (val.substring(i_val,i_val+2).toLowerCase()=="am") {ampm="AM";}
			else if (val.substring(i_val,i_val+2).toLowerCase()=="pm") {ampm="PM";}
			else {return 0;}
			i_val+=2;}
		else {
			if (val.substring(i_val,i_val+token.length)!=token) {return 0;}
			else {i_val+=token.length;}
			}
		}
	// If there are any trailing characters left in the value, it doesn't match
	if (i_val != val.length) { return 0; }
	// Is date valid for month?
	if (month==2) {
		// Check for leap year
		if ( ( (year%4==0)&&(year%100 != 0) ) || (year%400==0) ) { // leap year
			if (date > 29){ return false; }
			}
		else { if (date > 28) { return false; } }
		}
	if ((month==4)||(month==6)||(month==9)||(month==11)) {
		if (date > 30) { return false; }
		}
	// Correct hours value
	if (hh<12 && ampm=="PM") { hh=hh-0+12; }
	else if (hh>11 && ampm=="AM") { hh-=12; }
	var newdate=new Date(year,month-1,date,hh,mm,ss);
	return newdate.getTime();
	}

//----------------------------------------------------------------------------------------
//----------- bat dau ham cua Nhu mi -------------------------------------
// getNonWorkDays(date1,date2,holidaylist,holidaywyearlist)
//Input
// date1,date2      : string		co dang mmm/dd/yyyy hoac mmm/dd/yyyy
// holidaylist      : list of holiday (without year) separated by " , "
// holidaywyearlist : list of holiday (with year) separated by  " , "
//Output
// Number of non-work days between date1 and date2 (inclusive) included weekend (Saturday and Sunday) and holidays
function getNonWorkDays(date1,date2,holidaylist,holidaywyearlist)
{
	var nDayMilli=1000*60*60*24;
	var nSaturday=6;
	var nSunday=0;
	var nEndWeekDay=6;
	
	var nFromDate,nToDate,nFromDay,nToDay;
	var nFromYear,nToYear;
	var nDiffDays,nTemp,i,j,n;
	var nWeekendDays,nHolidays;
	
	var dteFromDate,dteToDate;
	
	var aHoliday,aHolidayWOYear;

	date1 = date1.replace(/-/g,' ');
	date2 = date2.replace(/-/g,' ');
	holidaylist = holidaylist.replace(/-/g,' ');
	holidaywyearlist = holidaywyearlist.replace(/-/g,' ');

	date1 = date1.replace(/\//g,' ');
	date2 = date2.replace(/\//g,' ');
	holidaylist = holidaylist.replace(/\//g,' ');
	holidaywyearlist = holidaywyearlist.replace(/\//g,' ');

	//Get nFromDate, nToDate as number of days from 1/1/1970
	nFromDate=Math.round(Math.min(Date.parse(date1),Date.parse(date2))/nDayMilli);
	nToDate=Math.round(Math.max(Date.parse(date1),Date.parse(date2))/nDayMilli);
		
	//Number of weekend days (1 week has two day viz. Saturday and Sunday)
	nWeekendDays=Math.floor((nToDate-nFromDate)/7)*2;
	
	
	dteFromDate=new Date(nFromDate*nDayMilli);
	dteToDate=new Date(nToDate*nDayMilli);
	
	
	//Get the day of the week
	nFromDay=dteFromDate.getDay();
	nToDay=dteToDate.getDay();
	
	//Calculate remain number of weekend days
	i=nFromDay;
	while (i!=nToDay)
	{
		if ((i==nSaturday)||(i==nSunday)) nWeekendDays++;
		if (i==nEndWeekDay) i=0;else i++;
	}
	
	if ((nToDay==nSaturday)||(nToDay==nSunday)) nWeekendDays++;
	
	//Create a list of holidays
	nFromYear=dteFromDate.getFullYear();
	nToYear=dteToDate.getFullYear();
	aHolidayWOYear=holidaylist.split(", ");
	aHoliday=new Array();
	n=0;
	for (i=0;i<aHolidayWOYear.length;i++)
	{
		for (j=nFromYear;j<=nToYear;j++)
		{
			aHoliday[n]=aHolidayWOYear[i]+" "+j
			n++
		}
	}
	
	aHoliday=aHoliday.concat(holidaywyearlist.split(", "));
	
	//Calculate the number of holidays
	nHolidays=0;
	for (i=0;i<aHoliday.length;i++)
	{
		nTemp=Math.round(Date.parse(aHoliday[i])/nDayMilli);
		if ((nFromDate<=nTemp)&&(nToDate>=nTemp)) nHolidays++;
	}		
	return nWeekendDays+nHolidays;
}