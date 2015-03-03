function IsDateSmaller(inputStr1, inputStr2, isEqual)
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
	if (yyyy2 > yyyy1)
		return true;
	else if (yyyy2 == yyyy1)
	{	
		if (mm2 == mm1) 
		{			
			if (dd2 > dd1) 
			{				
			return true;
			}				
			else if (dd2 == dd1)
			{
				if (isEqual)
					return true;
				else
					return false;
			}
			else				
				return false;
		}
		else
		{	
			if (mm2 > mm1) return true;
			else return false;
		}
	}
	else
		return false;
}

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
//Kiem tra mot so co 1 so le thap phan 
function  checkOneDecimalNumber(field)
{	
	var DecimalChar = ".";	
	if (field.value.indexOf(DecimalChar) != -1)
	{
		if(field.value.length == 3)
			return true;	
		else
			return false;				
	}
}
function CheckValidEmail(obj) 
{
	if(obj.value == "")
		return true;
	var email = obj.value;
	var filter  = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
	if (!filter.test(email)) 
	{
		GetAlertError(iTotal,DSAlert,"0058");
		//document.getElementById('_ctl0_' + obj).focus();
		obj.focus();
		return false;
	}
	return true;
}

//display object
function collapse(obj,isDisplay)
{
	if(isDisplay == null)
	{
		if (document.getElementById(obj).style.display=="none")
		{
			document.getElementById(obj).style.display="";
		}
		else
		{
			document.getElementById(obj).style.display="none";
		}
	}
	else
	{
		
		if (isDisplay ==false)
		{
			document.getElementById(obj).style.display="none";
		}
		else
		{
			document.getElementById(obj).style.display="";
		}
	}
	return false;
}
//use in list form
function showReportOnly()
{
	isDisplay = document.getElementById('_ctl0_chkShowGrid').checked;
	collapse('tdPages',isDisplay);
	collapse('trGrid',isDisplay);
	//if check multi sort
	if (isDisplay == true && document.getElementById('_ctl0_chkSort').checked == true)
		collapse('tdSort',isDisplay);
	else
		collapse('tdSort',!isDisplay);
}

function ShowDialog(formName,width,height)
{
	if(width == null || width < 100 ) width = Math.round((screen.width/3)*2) + 100;//900;
	if(width > screen.width ) width = screen.width - 100;//900;
	
	if(height == null || height < 100 ) height = Math.round((screen.height/3)*2);
	if(height > screen.height ) height = screen.height - 100;//900;
	
	l = 0;
	t = 0;

	objShowDialog = window.open(formName ,'Recipient','status=1,toolbar=0,scrollbars=1,resizable=1,alwaysRaised=Yes, top=' + t + ', left=' + l + ', width=' + width + ', height=' + height + ',1 ,align=center')	
	objShowDialog.focus();
}

function gshowModalDialog(formName,width,height)
{
	if(width == null || width < 100 ) width = Math.round((screen.width/3)*2);//900;
	if(width > screen.width ) width = screen.width - 100;//900;
	
	if(height == null || height < 100 ) height = Math.round((screen.height/3)*2);
	if(height > screen.height ) height = screen.height - 100;//900;
	
	l = 0;
	t = 0;
	//alert(width);
	//objShowDialog = window.showModalDialog(formName ,'Recipient','status=0,toolbar=0,scrollbars=1,resizable=yes,alwaysRaised=Yes, top=' + t + ', left=' + l + ', dialogWidth=' + width + ', dialogHeight=' + height + ',1 ,align=center')	
	objShowDialog = window.showModalDialog(formName ,'Recipient','status=0;scroll=1;resizable=yes;alwaysRaised=Yes; dialogHeight=' + height + 'px; dialogWidth=' + width + 'px;align=center')	
	//objShowDialog.focus();
}

//Phim tat: keycode = 13 # phim enter
function KeyDownHandler(strButtonName)
{
	// process only the Enter key
	if (event.keyCode == 13)
	{
		event.returnValue=false;
		event.cancel = true;
		document.getElementById().click(strButtonName);
	}
}

//Check grid checked
function GridCheck (GridName, BeginIndex, EndIndexExt, CtlCheckName)
{
	var i;
	var count;
	var NoItemCheck;
					
	count = document.getElementById(GridName).rows.length;		
	NoItemCheck = true;		
	if (count >1 )
	{
		for (i = BeginIndex; i <document.getElementById(GridName).rows.length + EndIndexExt ; i++)
		{	
			if (document.getElementById(GridName + "__ctl" + i + "_" + CtlCheckName) != null)
			{
				if (document.getElementById(GridName + "__ctl" + i + "_" + CtlCheckName).checked == true)
				{	
					NoItemCheck = false;
					break;
				}
			}
		}				
	}
	
	if (NoItemCheck)
	{
		return false;
	}
	else 
		return true;
}

//Check grid checked
function GridNoHeaderCheck (GridName, BeginIndex, EndIndexExt, CtlCheckName)
{
	var i;
	var count;
	var NoItemCheck;
					
	count = document.getElementById(GridName).rows.length;		
	NoItemCheck = true;		
	if (count > 0)
	{
		for (i = BeginIndex; i <document.getElementById(GridName).rows.length + EndIndexExt ; i++)
		{	
			if (document.getElementById(GridName + "__ctl" + i + "_" + CtlCheckName) != null)
			{
				if (document.getElementById(GridName + "__ctl" + i + "_" + CtlCheckName).checked == true)
				{	
					NoItemCheck = false;
					break;
				}
			}
		}				
	}
	
	if (NoItemCheck)
	{
		return false;
	}
	else 
		return true;
}

//CheckAll
//Ctl: checkbox checkAll, GridCtl: Checkbox cua grid
function CheckAll(Ctl, GridName, BeginIndex, EndIndexExt, GridCtl)
{
	if (document.getElementById(GridName) != null)
	{
		var value = document.getElementById(Ctl).checked;
		var i;
		count = document.getElementById(GridName).rows.length;	
		if (count >1 )
		{
			for (i=BeginIndex; i<document.getElementById(GridName).rows.length + EndIndexExt; i++)
			{
				if (document.getElementById(GridName + "__ctl" + i + "_" + GridCtl) != null)
				{
					//Tanldt:2006/08/29
					if (document.getElementById(GridName + "__ctl" + i + "_" + GridCtl).disabled == false) // <== Code nay tra ve neu la trang thai Checkbox la "disabled" thi` se kg check 
						document.Form1(GridName + "__ctl" + i + "_" + GridCtl).checked = value;
				}
			}
		}
	}
}

//Loai bo khoang trang
function trim(txt) {
    txt = txt.replace(/^(\s)+/, '');
    txt = txt.replace(/(\s)+$/, '');
   	return txt;
}

//Ham goi su kien cua mot nut khac khi bam phim enter
function checkKey(strBtnClick) 
{
	if (document.getElementById(strBtnClick) == null)
		return;
	if(window.event.keyCode==13){		
		document.getElementById(strBtnClick).click();		
		event.returnValue=false;
		event.cancel = true;		
	}
}
function checkBit(field)
{
	if (field.value!=0 && field.value!=1)
	{
		GetAlertError(iTotal,DSAlert,"0051");
		field.value="";
		field.focus();
		return false;
	}
}
function checkScanCheck(field)
{
	if (field.value == "")
		return;		
	var re;   
	re = /,/g;
	var value = field.value.replace(re,'');
	if (isNaN(value))
	{
		GetAlertError(iTotal,DSAlert,"0013");
		field.value = "";
		field.focus();
		return;
	}
	//tanldt: Test yeu cau 1 so cho phai lon hon 0
	if (value < 0)
	{
		//alert("Must not a negative numeric or zero!");
		GetAlertError(iTotal,DSAlert,"0012");
		field.value = "";
		field.focus();
		return;
	}		
	if (field.value != 2 && field.value != 4 && field.value != 6)
	{		
		GetAlertError(iTotal,DSAlert,"TSPR_0001");
		field.value="";
		field.focus();
		return false;
	}
	FormatNumericWithSeparate(field);
}
function checkStandardDays(field)
{
	if (field.value == "")
		return;		
	var re;   
	re = /,/g;
	var value = field.value.replace(re,'');
	if (isNaN(value))
	{
		GetAlertError(iTotal,DSAlert,"0013");
		field.value = "";
		field.focus();
		return;
	}
	//tanldt: Test yeu cau 1 so cho phai lon hon 0
	if (value < 0)
	{
		//alert("Must not a negative numeric or zero!");
		GetAlertError(iTotal,DSAlert,"0012");
		field.value = "";
		field.focus();
		return;
	}		
	if (field.value < 1 || field.value>30)
	{		
		GetAlertError(iTotal,DSAlert,"TSPR_0002");
		field.value="";
		field.focus();
		return false;
	}
	FormatNumericWithSeparate(field);
}
function checkHourday(field)
{
if (field.value == "")
		return;		
	var re;   
	re = /,/g;
	var value = field.value.replace(re,'');
	if (isNaN(value))
	{
		GetAlertError(iTotal,DSAlert,"0013");
		field.value = "";
		field.focus();
		return;
	}
	//tanldt: Test yeu cau 1 so cho phai lon hon 0
	if (value < 0)
	{
		//alert("Must not a negative numeric or zero!");
		GetAlertError(iTotal,DSAlert,"0012");
		field.value = "";
		field.focus();
		return;
	}		
	if (field.value < 1 || field.value>24)
	{		
		GetAlertError(iTotal,DSAlert,"TSPR_0003");
		field.value="";
		field.focus();
		return false;
	}
	FormatNumericWithSeparate(field);
}
//Kiem tra kieu so
function checkNumeric(field,MaxValue)
{	
	if (field.value == "")
	{
		field.value='0';
		return;		
	}
	var re;   
	re = /,/g;
	var value = field.value.replace(re,'');
	if (isNaN(value))
	{
		GetAlertError(iTotal,DSAlert,"0013");
		field.value = "0";
		field.focus();
		return false;
	}
	//tanldt: Test yeu cau 1 so cho phai lon hon 0
	if (value < 0)
	{		
		//alert("Must not a negative numeric or zero!");
		GetAlertError(iTotal,DSAlert,"0012");
		field.value = "0";
		field.focus();
		return false;
	}if (value>MaxValue)
	{
		GetAlertTextPopUp(GetAlertText(iTotal,DSAlert,'0054').replace('@',MaxValue))
		//alert('');
		field.focus();
		return false;
	}
	FormatNumericWithSeparate(field);
}

//Kiem tra kieu so khong de lai so 0
function checkNumeric2(field,MaxValue)
{	
	if (field.value == "")
	{
		field.value='';
		return;		
	}
	var re;   
	re = /,/g;
	var value = field.value.replace(re,'');
	if (isNaN(value))
	{
		GetAlertError(iTotal,DSAlert,"0013");
		field.value = "";
		field.focus();
		return false;
	}
	//tanldt: Test yeu cau 1 so cho phai lon hon 0
	if (value < 0)
	{		
		//alert("Must not a negative numeric or zero!");
		GetAlertError(iTotal,DSAlert,"0012");
		field.value = "";
		field.focus();
		return false;
	}if (value>MaxValue)
	{
		GetAlertTextPopUp(GetAlertText(iTotal,DSAlert,'0054').replace('@',MaxValue))
		//alert('');
		field.focus();
		return false;
	}
	FormatNumericWithSeparate(field);
}
//Kiem tra kieu so > 0
function checkNumeric1(field)
{	
	if (field.value == "")
		return;		
	var re;   
	re = /,/g;
	var value = field.value.replace(re,'');
	if (isNaN(value))
	{
		GetAlertError(iTotal,DSAlert,"0013");
		field.value = "";
		field.focus();
		return;
	}
	//tanldt: Test yeu cau 1 so cho phai lon hon 0
	if (value < 1)
	{		
		//alert("Must not a negative numeric or zero!");
		GetAlertError(iTotal,DSAlert,"0012");
		field.value = "";
		field.focus();
		return;
	}
	FormatNumericWithSeparate(field);
}
//Kiem tra kieu so <cho phep am>
function checkNumber(field)
{	
	if (field.value == "")
		return;		
	var re;   
	re = /,/g;
	var value = field.value.replace(re,'');		
	if (isNaN(value))
	{
		GetAlertError(iTotal,DSAlert,"0013");
		field.value = "";
		field.focus();
		return;
	}	
	FormatNumericWithSeparate(field);
}
//Kiem tra kieu so
function checkNumericNotSeparate(field)
{	
	if (field.value == "")
		return;		
	var re;   
	re = /,/g;
	var value = field.value.replace(re,'');
	if (isNaN(value))
	{
		GetAlertError(iTotal,DSAlert,"0009");
		field.value = "";
		field.focus();
		return;
	}
	if (value < 0)
	{
		//alert("Must not a negative numeric or zero!");
		GetAlertError(iTotal,DSAlert,"0035");
		field.value = "";
		field.focus();
		return;
	}
}

//Chuyen thanh dang so binh thuong cua dinh dang money: 2,000 -> 2000
function ReplaceMoneyNumeric(field)
{
	return field.value.replace(/,/g,'');
}
//Chuyen thanh dang so binh thuong cua dinh dang money: 2,000 -> 2000
function ReplaceMoneyNumericFromValue(field)
{
	return field.replace(/,/g,'');
}
//dinh dang kieu money ',' (ex: 2,000,000.00)
function FormatNumericWithSeparate(field)
{
	var DecimalNumber = 1;
	var DecimalChar = ".";
	var SeperateChar = ",";
	var ValueString = parseFloat(field.value.replace(/,/g,'')).toString();
	//var ValueString = field.value;
	var ValueStringAfterDecimal = "";
	var ValueStringNew = "";	
	//if (field.value < 0)
	if (ValueString < 0)
	{
		ValueString = ValueString.substring(1, ValueString.length); //Bo dau tru				
	}		
	if (ValueString.indexOf(DecimalChar) != -1)
	{
		ValueStringAfterDecimal = ValueString.substring(ValueString.indexOf(DecimalChar), ValueString.indexOf(DecimalChar) + 2 + DecimalNumber) // Tinh luon dau cham
		//alert(ValueStringAfterDecimal);
		ValueString = ValueString.substring(0, ValueString.indexOf(DecimalChar))
	}

	if (ValueStringAfterDecimal.indexOf(SeperateChar) != -1)
	{
		GetAlertError(iTotal,DSAlert,"0035");
		field.value = "";
		field.focus();		
		return;
	}	
	ValueString = ValueString.replace(/,/g,'');
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
	//if (field.value < 0) 
	if (parseFloat(field.value.replace(/,/g,'')) < 0) 	
		ValueStringNew = "-" + ValueStringNew;
	
	field.value = ValueStringNew;
}
//Kiem tra kieu so lam tron theo 0.5
function checkNoRound05(field,MaxValue)
{	
	if (field.value == "")
		return true;
	var value = field.value;
	if (isNaN(value))
	{
		GetAlertError(iTotal,DSAlert,"0035");
		field.value = "";
		field.focus();
		return false;
	}
	if (value < 0)
	{
		GetAlertError(iTotal,DSAlert,"0035");
		field.value = "";
		field.focus();
		return false;
	}
	if (Math.round(parseFloat(value)/0.5) != (parseFloat(value)/0.5))
	{
		GetAlertError(iTotal,DSAlert,"0036");
		field.value = "";
		field.focus();
		return false;
	}if (value>=MaxValue)
	{
		GetAlertTextPopUp(GetAlertText(iTotal,DSAlert,'0054').replace('@',MaxValue))
		//alert('');
		field.focus();
		return false;
	}
	return true;
}

//Kiem tra kieu so lam tron theo 0.5 (0, 0.5, 1)
function DaycheckNoRound05(field)
{
	if (field.value == "")
		return true;
	var value = field.value;
	if (isNaN(value))
	{
		GetAlertError(iTotal,DSAlert,"0037");
		field.value = "";
		field.focus();
		return false;
	}
	if (parseFloat(value) != 0 && parseFloat(value) != 0.5 
		&& parseFloat(value) != 1)
	{
		//alert("Số hợp lệ là số có 1 trong 3 giá trị: 0; 0.5; 1");
		GetAlertError(iTotal,DSAlert,"0038");
		field.value = "";
		field.focus();
		return false;
	}
	field.value = parseFloat(value);
	return true;
}

//Kiem tra so phan tram
function CheckPercent(field)
{
	if (field.value == "")
		return;
	var value = field.value;
	if (isNaN(value))
	{
		//alert("Không phải là dữ liệu số!");
		GetAlertError(iTotal,DSAlert,"0037");
		field.value = "";
		field.focus();
		return;
	}
	if (value < 0 || value > 100)
	{
		//alert("Số phải nằm trong khoảng từ 0 đến 100!");
		GetAlertError(iTotal,DSAlert,"0039");
		field.value = "";
		field.focus();
		return;
	}
}

function CheckPercent1(field)
{
	if (field.value == "")
		return;
	var value = field.value;
	if (isNaN(value))
	{
		//alert("Không phải là dữ liệu số!");
		GetAlertError(iTotal,DSAlert,"0037");
		field.value = "";
		field.focus();
		return;
	}
	if (Math.round(value) < 0 || Math.round(value) > 100)
	{
		//alert("Số phải nằm trong khoảng từ 0 đến 100!");
		GetAlertError(iTotal,DSAlert,"0039");
		field.value = "";
		field.focus();
		return;
	}
	field.value=Math.round(value);
}


function MonthOfDate(inputStr)
{
	var dd;
	var mm;
	var yyyy;
	var Month;
	
	var delim1 = inputStr.indexOf("/");
	var delim2 = inputStr.lastIndexOf("/");
	if (delim1 == delim2)
	{
		delim1 = inputStr.indexOf("-");
		delim2 = inputStr.lastIndexOf("-");	
		
		// lay ngay, thang, nam cua ngay1
		dd = parseInt(inputStr.substring(0,delim1),11)
		var MMM = parseInt(inputStr.substring(delim1 + 2,delim2),11)
		yyyy = parseInt(inputStr.substring(delim2 + 1,inputStr.length),11)
		if (MMM=='Jan'||MMM=='JAn'||MMM=='JAN'||MMM=='jan'||MMM=='jAn'||MMM=='jAN')
			mm='01'
		if (MMM=='Feb'||MMM=='FEb'||MMM=='FEB'||MMM=='feb'||MMM=='fEb'||MMM=='fEB')
			mm='02'
		if (MMM=='Mar'||MMM=='MAr'||MMM=='MAR'||MMM=='mar'||MMM=='mAr'||MMM=='mAR')
			mm='03'
		if (MMM=='Apr'||MMM=='APr'||MMM=='APR'||MMM=='apr'||MMM=='aPr'||MMM=='aPR')
			mm='04'
		if (MMM=='May'||MMM=='MAy'||MMM=='MAY'||MMM=='may'||MMM=='mAy'||MMM=='mAY')
			mm='05'
		if (MMM=='Jun'||MMM=='JUn'||MMM=='JUN'||MMM=='jun'||MMM=='jUn'||MMM=='jUN')
			mm='06'
		if (MMM=='Jul'||MMM=='JUl'||MMM=='JUL'||MMM=='jul'||MMM=='jUl'||MMM=='jUL')
			mm='07'
		if (MMM=='Aug'||MMM=='Aug'||MMM=='AUG'||MMM=='aug'||MMM=='aUg'||MMM=='aUG')
			mm='08'
		if (MMM=='Sep'||MMM=='SEp'||MMM=='SEP'||MMM=='sep'||MMM=='sEp'||MMM=='sEP')
			mm='09'
		if (MMM=='Oct'||MMM=='OCt'||MMM=='OCT'||MMM=='oct'||MMM=='oCt'||MMM=='oCT')
			mm='10'
		if (MMM=='Nov'||MMM=='NOv'||MMM=='NOV'||MMM=='nov'||MMM=='nOv'||MMM=='nOV')
			mm='11'
		if (MMM=='Dec'||MMM=='DEc'||MMM=='DEC'||MMM=='dec'||MMM=='dEc'||MMM=='dEC')
			mm='12'	
		Month = mm + '/' + yyyy;
		return Month  	
	}
	
	// lay ngay, thang, nam cua ngay1
	dd = parseInt(inputStr.substring(0,delim1),10)
	mm = parseInt(inputStr.substring(delim1 + 1,delim2),10)
	yyyy = parseInt(inputStr.substring(delim2 + 1,inputStr.length),10)
	Month = mm + '/' + yyyy;
	return Month  	

}
//Kiem tra so nguyen
function checkInteger(field,MaxValue)
{
	
	var value = field.value;
	
	if (isNaN(value))
	{	
		GetAlertError(iTotal,DSAlert,"0040");
		field.focus();
		field.value = "";
		return false;
	}
	else
	{
		if (value>MaxValue)
		{
		GetAlertTextPopUp(GetAlertText(iTotal,DSAlert,'0054').replace('@',MaxValue))
		//alert('');
		field.focus();
		return false;
		}
		if (value < 0)
		{
			//alert("Số không được là số âm!");
			GetAlertError(iTotal,DSAlert,"0010");
			field.focus();
			field.value = "";
			return false;
		}
		else
		{
			field.value = Math.round(field.value);
			FormatNumericWithSeparate(field);
			return true;
		} 
		if (value>=MaxValue)
		{
		GetAlertTextPopUp(GetAlertText(iTotal,DSAlert,'0054').replace('@',MaxValue))
		//alert('');
		field.focus();
		return false;
		}
	}
	
}

//Kiem tra so nguyen
function checkInt(field,MaxValue)
{
	var value = field.value;
	if (value!='')	
	{
		if (isNaN(value))
		{	
			GetAlertError(iTotal,DSAlert,"0040");
			field.focus();
			field.value = "";
			return false;
		}
		if (value < 1)
		{
			//alert("Số không được là số âm!");
			GetAlertError(iTotal,DSAlert,"0072");
			field.focus();
			field.value = "";
			return false;
		}
		if (value>MaxValue)
		{
			GetAlertTextPopUp(GetAlertText(iTotal,DSAlert,'0054').replace('@',MaxValue))			
			field.focus();
			return false;
		}		
		field.value = Math.round(field.value);			 
		FormatNumericWithSeparate(field);
		return true	;
	}
	return true	;
}

//Kiem tra so nguyen
function checkInt2(field,MaxValue)
{	var value = field.value;
	if (value!='')	
	{
		var value = field.value;
		if (value!='')	
		{
		if (isNaN(value))
		{	
			GetAlertError(iTotal,DSAlert,"0040");
			field.focus();
			field.value = "";
			return false;
		}
		if (value < 1)
		{
			//alert("Số không được là số âm!");
			GetAlertError(iTotal,DSAlert,"0072");
			field.focus();
			field.value = "";
			return false;
		}
		if (value>=MaxValue)
		{
			GetAlertTextPopUp(GetAlertText(iTotal,DSAlert,'0054').replace('@',MaxValue))			
			field.focus();
			return false;
		}		
		field.value = Math.round(field.value);			 
			//FormatNumericWithSeparate(field);
			return true	;
		}
		return true	;
	}
}

//Kiem tra gio phut dinh dang(hh:mm)
function formatHours(field)
{
	var nSep1, sHour, sMinute,nHour,nMinute;
	var checkstr = "0123456789";
	var DataTemp = "";
	var DataValue = "";
	var value,i;
	DataValue = field.value;
	if (DataValue.length == 0)
	{
		field.value = "";
		return false;
	}
	//Bo nhung ky tu khong phai ky tu so
	for (i = 0; i <DataValue.length; i++)
	{
		if (checkstr.indexOf(DataValue.substr(i,1)) >= 0) 
		{
			DataTemp = DataTemp + DataValue.substr(i,1);
		}
	}
	if (DataTemp.length != 4)
	{
		field.value = "";
		GetAlertError(iTotal,DSAlert,"0100");
		return false;
		
	}
	sHour = DataTemp.substring(0,2);
	sMinute = DataTemp.substring( 2,4 );
	if (isNaN(sHour) || isNaN(sMinute)) 
	{
		field.value = "";
		GetAlertError(iTotal,DSAlert,"0100");
		return false;
	}
	
	nHour = parseInt(sHour,10); nMinute = parseInt(sMinute,10);
	if ( nHour<0 || nMinute<0 )
	{
		field.value = "";
		GetAlertError(iTotal,DSAlert,"0100");
		return false;
	}
	if (nHour>= 24 || nMinute >=60)
	{
		field.value = "";
		GetAlertError(iTotal,DSAlert,"0100");
		return false;
	}	
	value = sHour + ":" + sMinute;
	field.value = value;
	return true;
}
//Kiem tra nam
function CheckYear(field)			
{
	var checkstr = "0123456789";
	var DateField = field;
	var DateValue = "";
	var DateTemp = "";
	var i;
	
	DateValue = DateField.value;
	if (DateValue.length == 0) return;
	for (i = 0; i < DateValue.length; i++) 
	{
		if (checkstr.indexOf(DateValue.substr(i,1)) >= 0) {
			DateTemp = DateTemp + DateValue.substr(i,1);	}
	}
	DateValue = DateTemp;
	
	if (DateValue.length == 2)
		DateValue = "20" + DateValue;
		
	if ((DateValue.length != 4 && DateValue.length != 0) || DateValue == 0 || DateValue < 1900)		
	{		 
		//alert("Năm không hợp lệ!");
		GetAlertError(iTotal,DSAlert,"0041");
		DateField.value = "";
		DateField.focus();
		return false;
	}
	DateField.value = DateValue;
	return true;
}

//Kiem tra du lieu thang nam
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
		//alert("Định dạng dữ liệu kiểu MM/yyyy!");
		GetAlertError(iTotal,DSAlert,"0042");
		DateField.focus();	
		// HOPTD ADDED
		return false;
		//////////////
      }      
   }
   /* Error-message if err != 0 */
   else {
		DateField.value = "";
		DateField.focus();
		//alert("Định dạng dữ liệu kiểu MM/yyyy!");
		GetAlertError(iTotal,DSAlert,"0042");
		// HOPTD ADDED
		return false;
		//////////////
   }   
}
function Toddmmyyyy(field)
{	
	var MMM=field.value.substr(3,3)
	var mm=''
	if (MMM=='Jan'||MMM=='JAn'||MMM=='JAN'||MMM=='jan'||MMM=='jAn'||MMM=='jAN')
		mm='01'
	if (MMM=='Feb'||MMM=='FEb'||MMM=='FEB'||MMM=='feb'||MMM=='fEb'||MMM=='fEB')
		mm='02'
	if (MMM=='Mar'||MMM=='MAr'||MMM=='MAR'||MMM=='mar'||MMM=='mAr'||MMM=='mAR')
		mm='03'
	if (MMM=='Apr'||MMM=='APr'||MMM=='APR'||MMM=='apr'||MMM=='aPr'||MMM=='aPR')
		mm='04'
	if (MMM=='May'||MMM=='MAy'||MMM=='MAY'||MMM=='may'||MMM=='mAy'||MMM=='mAY')
		mm='05'
	if (MMM=='Jun'||MMM=='JUn'||MMM=='JUN'||MMM=='jun'||MMM=='jUn'||MMM=='jUN')
		mm='06'
	if (MMM=='Jul'||MMM=='JUl'||MMM=='JUL'||MMM=='jul'||MMM=='jUl'||MMM=='jUL')
		mm='07'
	if (MMM=='Aug'||MMM=='Aug'||MMM=='AUG'||MMM=='aug'||MMM=='aUg'||MMM=='aUG')
		mm='08'
	if (MMM=='Sep'||MMM=='SEp'||MMM=='SEP'||MMM=='sep'||MMM=='sEp'||MMM=='sEP')
		mm='09'
	if (MMM=='Oct'||MMM=='OCt'||MMM=='OCT'||MMM=='oct'||MMM=='oCt'||MMM=='oCT')
		mm='10'
	if (MMM=='Nov'||MMM=='NOv'||MMM=='NOV'||MMM=='nov'||MMM=='nOv'||MMM=='nOV')
		mm='11'
	if (MMM=='Dec'||MMM=='DEc'||MMM=='DEC'||MMM=='dec'||MMM=='dEc'||MMM=='dEC')
		mm='12'	 
	
	//Toddmmyyyy(field);
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
   DateValue = field.value.substr(0,2) + '/' + mm + '/' + field.value.substr(7,4);
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
	
   if (year <= 1900 )
   {
		err=18;
   }
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
      return 1;
   }
   else if(err=18)/* Error-message if err != 0 */
   {
		//alert('ddd');
		//DateField.focus();
		//GetAlertError(iTotal,DSAlert,"0030");
		return 0;
   }
   else {
		
		DateField.focus();
		GetAlertError(iTotal,DSAlert,"0031");
		return 0;
   }
}

function Toddmmyyyy_value(param_value)
{	
	if (param_value.length == 10)
		return param_value;
	
	var MMM=param_value.substr(3,3)
	var mm=''
	/*if (MMM=='Jan')
		mm='01'
	if (MMM=='Feb')
		mm='02'
	if (MMM=='Mar')
		mm='03'
	if (MMM=='Apr')
		mm='04'
	if (MMM=='May')
		mm='05'
	if (MMM=='Jun')
		mm='06'
	if (MMM=='Jul')
		mm='07'
	if (MMM=='Aug')
		mm='08'
	if (MMM=='Sep')
		mm='09'
	if (MMM=='Oct')
		mm='10'
	if (MMM=='Nov')
		mm='11'
	if (MMM=='Dec')
		mm='12'	 */
	if (MMM=='Jan'||MMM=='JAn'||MMM=='JAN'||MMM=='jan'||MMM=='jAn'||MMM=='jAN')
		mm='01'
	if (MMM=='Feb'||MMM=='FEb'||MMM=='FEB'||MMM=='feb'||MMM=='fEb'||MMM=='fEB')
		mm='02'
	if (MMM=='Mar'||MMM=='MAr'||MMM=='MAR'||MMM=='mar'||MMM=='mAr'||MMM=='mAR')
		mm='03'
	if (MMM=='Apr'||MMM=='APr'||MMM=='APR'||MMM=='apr'||MMM=='aPr'||MMM=='aPR')
		mm='04'
	if (MMM=='May'||MMM=='MAy'||MMM=='MAY'||MMM=='may'||MMM=='mAy'||MMM=='mAY')
		mm='05'
	if (MMM=='Jun'||MMM=='JUn'||MMM=='JUN'||MMM=='jun'||MMM=='jUn'||MMM=='jUN')
		mm='06'
	if (MMM=='Jul'||MMM=='JUl'||MMM=='JUL'||MMM=='jul'||MMM=='jUl'||MMM=='jUL')
		mm='07'
	if (MMM=='Aug'||MMM=='Aug'||MMM=='AUG'||MMM=='aug'||MMM=='aUg'||MMM=='aUG')
		mm='08'
	if (MMM=='Sep'||MMM=='SEp'||MMM=='SEP'||MMM=='sep'||MMM=='sEp'||MMM=='sEP')
		mm='09'
	if (MMM=='Oct'||MMM=='OCt'||MMM=='OCT'||MMM=='oct'||MMM=='oCt'||MMM=='oCT')
		mm='10'
	if (MMM=='Nov'||MMM=='NOv'||MMM=='NOV'||MMM=='nov'||MMM=='nOv'||MMM=='nOV')
		mm='11'
	if (MMM=='Dec'||MMM=='DEc'||MMM=='DEC'||MMM=='dec'||MMM=='dEc'||MMM=='dEC')
		mm='12'	 
	

   return param_value.substr(0,2) + '/' + mm + '/' + param_value.substr(7,4);
  
}
function ToddMMMyyyy(field)
{	
	var MMM=field.value.substr(3,2)
	//alert(MMM);
	var mm=''
	if (MMM=='01')
		mm='Jan'
	if (MMM=='02')
		mm='Feb'
	if (MMM=='03')
		mm='Mar'
	if (MMM=='04')
		mm='Apr'
	if (MMM=='05')
		mm='May'
	if (MMM=='06')
		mm='Jun'
	if (MMM=='07')
		mm='Jul'
	if (MMM=='08')
		mm='Aug'
	if (MMM=='09')
		mm='Sep'
	if (MMM=='10')
		mm='Oct'
	if (MMM=='11')
		mm='Nov'
	if (MMM=='12')
		mm='Dec'
	if (field.value.substr(0,2)!='')	
		field.value= field.value.substr(0,2) + '-' + mm + '-' + field.value.substr(6,4)			
}
function ToddMMMyyyy_value(param_value)
{	
	if (param_value.length == 12)
		return param_value;
		
	var MMM=param_value.substr(3,2)
	//alert(MMM);
	var mm=''
	if (MMM=='01')
		mm='Jan'
	if (MMM=='02')
		mm='Feb'
	if (MMM=='03')
		mm='Mar'
	if (MMM=='04')
		mm='Apr'
	if (MMM=='05')
		mm='May'
	if (MMM=='06')
		mm='Jun'
	if (MMM=='07')
		mm='Jul'
	if (MMM=='08')
		mm='Aug'
	if (MMM=='09')
		mm='Sep'
	if (MMM=='10')
		mm='Oct'
	if (MMM=='11')
		mm='Nov'
	if (MMM=='12')
		mm='Dec'
	if (param_value.substr(0,2)!='')	
		return  param_value.substr(0,2) + '-' + mm + '-' + param_value.substr(6,4)		
		
}

//Kiem tra du lieu ngay
function CheckDate(field){
if (Toddmmyyyy(field)==1)
return;
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
	
   if (year <= 1900 )
   {
		err=18;
   }
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
      ToddMMMyyyy(DateField);
      return true;
   }
   else if(err=18)/* Error-message if err != 0 */
   {
		DateField.value = "";
		DateField.focus();
		GetAlertError(iTotal,DSAlert,"0030");
		return false;
   }
   else {
		DateField.value = "";
		DateField.focus();
		GetAlertError(iTotal,DSAlert,"0031");
		return false;
   }
   
}

//Kiem tra du lieu ngay
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
	
   if (year <= 1900 )
   {
		err=18;
   }
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
      return true;
   }
   else if(err=18)/* Error-message if err != 0 */
   {
		DateField.focus();
		GetAlertError(iTotal,DSAlert,"0030");
		return false;
   }
   else {
		DateField.focus();
		GetAlertError(iTotal,DSAlert,"0031");
		return false;
   }
}
function CheckDDMM(field){
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
   if (field.value!='')
   DateValue = DateField.value + '/2006'; 
   else
   DateValue=field.value;
   
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
	
   if (year <= 1900 )
   {
		err=18;
   }
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
      DateField.value = day + seperator + month;
      return true;
   }
   else if(err=18)/* Error-message if err != 0 */
   {
		DateField.value = "";
		DateField.focus();
		GetAlertError(iTotal,DSAlert,"0030");
		return false;
   }
   else {
		DateField.value = "";
		DateField.focus();
		GetAlertError(iTotal,DSAlert,"0031");
		return false;
   }
   
}

function AddDaystoDate(value,count)
{
	//chi ap dung cho count <=28
	var day = value.substr(0,2);
	var month = value.substr(3,2);
	var year = value.substr(6,4);
	day = parseFloat(day) + count;	
	/* Validation leap-year / february / day */
	if ((year % 4 == 0) || (year % 100 == 0) || (year % 400 == 0)) 
	{
		leap = 1;
	}
	if ((month == 2) && (leap == 1) && (day > 29)) {
		month = parseFloat(month) + 1;
		day = parseFloat(day) - 29;
	}
	else if ((month == 2) && (leap != 1) && (day > 28)) {
		month = parseFloat(month) + 1;
		day = parseFloat(day) - 28;
	}
	/* Validation of other months */
	else if ((day > 31) && ((month == "01") || (month == "03") || (month == "05") || (month == "07") || (month == "08") || (month == "10") || (month == "12"))) {
		month = parseFloat(month) + 1;
		day = day - 31;
	}
	else if ((day > 30) && ((month == "04") || (month == "06") || (month == "09") || (month == "11"))) {
		month = month + 1;
		day = day - 30;
	}
	
	if (day < 10)
		day = "0" + day;
	if (month.length == 1)
		month = "0" + month;
	return day + "/" + month + "/" + year;
}

function AddMonthstoDate(value,count)
{
	value = Toddmmyyyy_value(value)
	var day = value.substr(0,2);
	var month = value.substr(3,2);
	var year = value.substr(6,4);
	count = parseFloat(count);
	month = parseFloat(month) + parseFloat(count);
	var delta = 0;
	while(month - 12 > 0)
	{
		delta = parseFloat(delta) + 1;
		month = month - 12;
	}
	year = parseFloat(year) + delta;
	if ((day + '').length == 1)
		day = "0" + day;
	if ((month + '').length == 1)
		month = "0" + month;
	//alert(ToddMMMyyyy_value(day + "/" + month + "/" + year));	
	return ToddMMMyyyy_value(day + "/" + month + "/" + year);
	
}

function AddYearstoDate(value,count)
{
	var day = value.substr(0,2);
	var month = value.substr(3,2);
	var year = value.substr(6,4);
	count = parseFloat(count);
	year = parseFloat(year) + parseFloat(count);
	if (day.length == 1)
		day = "0" + day;
	if (month.length == 1)
		month = "0" + month;
	return day + "/" + month + "/" + year;
}

//Chuyen du lieu ngay dang dd/mm/yyyy thanh mm/dd/yyyy
function ConvertMMDDYYYY(field)
{
	if (CheckDate(field) == false)
		return;
	var checkstr = "0123456789";
	var seperator = "/";
	var day;
	var month;
	var year;
	var i;
   var value = field.value;
   var valueTemp = "";
   /* Delete all chars except 0..9 */
   for (i = 0; i < value.length; i++) {
	  if (checkstr.indexOf(value.substr(i,1)) >= 0) {
	     valueTemp = valueTemp + value.substr(i,1);
	  }
   }
   value = valueTemp;
   year = value.substr(4,4);
   month = value.substr(2,2);
   day = value.substr(0,2);
   return month + seperator + day + seperator + year;
}

function DeltaTwoDay(value1, value2)
{
	if (value1 == "" || value2 == "")
		return;
    var checkstr = "0123456789";
	var seperator = "/";
	var i;
    var valueTemp1 = "";
    var valueTemp2 = "";
   /* Delete all chars except 0..9 */
   for (i = 0; i < value1.length; i++) {
	  if (checkstr.indexOf(value1.substr(i,1)) >= 0) {
	     valueTemp1 = valueTemp1 + value1.substr(i,1);
	  }
   }
   for (i = 0; i < value2.length; i++) {
	  if (checkstr.indexOf(value2.substr(i,1)) >= 0) {
	     valueTemp2 = valueTemp2 + value2.substr(i,1);
	  }
   }
   value1 = valueTemp1;
   value2 = valueTemp2;
   year1 = value1.substr(4,4);
   month1 = value1.substr(2,2);
   day1 = value1.substr(0,2);
   year2 = value2.substr(4,4);
   month2 = value2.substr(2,2);
   day2 = value2.substr(0,2);
   var delta = Date.UTC(parseInt(year2,10),parseInt(month2,10)-1,parseInt(day2,10)) - Date.UTC(parseInt(year1,10),parseInt(month1,10)-1,parseInt(day1,10));
   return ((delta/86400000) + 1)
}

function DeltaDate1Date2(field1, field2)
{
	if (CheckDate(field1) == false)
		return;
	if (CheckDate(field2) == false)
		return;
	var value1 = field1.value;
    var value2 = field2.value;
    if (value1 == "" || value2 == "")
		return;
    var checkstr = "0123456789";
	var seperator = "/";
	var i;
    var valueTemp1 = "";
    var valueTemp2 = "";
   /* Delete all chars except 0..9 */
   for (i = 0; i < value1.length; i++) {
	  if (checkstr.indexOf(value1.substr(i,1)) >= 0) {
	     valueTemp1 = valueTemp1 + value1.substr(i,1);
	  }
   }
   for (i = 0; i < value2.length; i++) {
	  if (checkstr.indexOf(value2.substr(i,1)) >= 0) {
	     valueTemp2 = valueTemp2 + value2.substr(i,1);
	  }
   }
   value1 = valueTemp1;
   value2 = valueTemp2;
   year1 = value1.substr(4,4);
   month1 = value1.substr(2,2);
   day1 = value1.substr(0,2);
   year2 = value2.substr(4,4);
   month2 = value2.substr(2,2);
   day2 = value2.substr(0,2);
   var delta = Date.UTC(parseInt(year2,10),parseInt(month2,10)-1,parseInt(day2,10)) - Date.UTC(parseInt(year1,10),parseInt(month1,10)-1,parseInt(day1,10));
   return ((delta/86400000) + 1)
}

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
	if (yyyy2 > yyyy1)
		return true;
	else if (yyyy2 == yyyy1)
	{
		if (mm2 == mm1) 
			{
				
				if (dd2 >= dd1) 
				{				
				return true;
				}				
				else
				{				
				 return false;
				}
			}
		else
			{
				if (mm2 > mm1) return true;
				else return false;
			}
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
//hanhntm kiem tra ngay rong moi thuc hien thoa tac so sanh 
     
	
	if (!isBlank(objFromDate.value))
	{
		FromDate = Toddmmyyyy_value(objFromDate.value);
	}
	else return true;
	if (!isBlank(objToDate.value))
	{

		ToDate = Toddmmyyyy_value(objToDate.value);
	}
	else return true;
	if ((!isBlank(FromDate)) && (!isBlank(ToDate)))
	{		
		if  (!IsSmaller(FromDate, ToDate))
		{
			//alert("FromDate must be less than ToDate!");
			//objToDate.focus()
		
			return false;
		}
		else
		{
			return true;
		
		}
	}
	else
	{
		
		return true;
	}
		
}
function FromSmallToDate2(FromDate,ToDate)
{
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
*
*** Kiem tra tu ngay nho hon hoac bang Den ngay ****
**** objFromDate : Thanh phan cua form ****
**** objToDate : Thanh phan cua form   ****
******************************************/
function FromSmallOrEqualToDate(objFromDate,objToDate)
{
	//FromDate = Toddmmyyyy_value(objFromDate.value)
	//ToDate = Toddmmyyyy_value(objToDate.value)
	if (!isBlank(objFromDate.value))
	{
		FromDate = Toddmmyyyy_value(objFromDate.value);
	}
	else return true;
	if (!isBlank(objToDate.value))
	{

		ToDate = Toddmmyyyy_value(objToDate.value);
	}
	else return true;
	if ((!isBlank(FromDate)) && (!isBlank(ToDate)))
	{
	
		if  (!IsSmallerOrEqual(FromDate, ToDate))
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
**** Kiem tra ngay nho hon ngay hien tai ****
**** objFromDate : Thanh phan cua form ****
******************************************/
function FromSmallNow(objFromDate)
{
	//FromDate = Toddmmyyyy_value(objFromDate.value)
	if (!isBlank(objFromDate.value))
	{
		FromDate = Toddmmyyyy_value(objFromDate.value);
	}
	else return true;
	
	var now = new Date();
  	var sMonth = (now.getMonth() + 1).toString();
  	var sDay = now.getDate().toString();
  	sDay=parseInt(sDay)+1;
//	sDay=parseInt(sDay);
  	var sYear = now.getFullYear().toString();
  	ToDate = sDay + '/' + sMonth + '/' + sYear;
  
  	
	if ((!isBlank(FromDate)) && (!isBlank(ToDate)))
	{
		if  (!IsSmaller(FromDate, ToDate))
		{			
			return false;
		}
		else
			return true;
	}
	else
		return true;
}

function FromSmallNowDate(objFromDate)
{
	//FromDate = Toddmmyyyy_value(objFromDate.value)
	if (!isBlank(objFromDate.value))
	{
		FromDate = Toddmmyyyy_value(objFromDate.value);
	}
	else return true;
	
	var now = new Date();
  	var sMonth = (now.getMonth() + 1).toString();
  	var sDay = now.getDate().toString();
  //	sDay=parseInt(sDay)+1;
	sDay=parseInt(sDay);
  	var sYear = now.getFullYear().toString();
  	ToDate = sDay + '/' + sMonth + '/' + sYear;
  
  	
	if ((!isBlank(FromDate)) && (!isBlank(ToDate)))
	{
		if  (!IsSmaller(FromDate, ToDate))
		{			
			return false;
		}
		else
			return true;
	}
	else
		return true;
}



/******************************************

**** Kiem tra ngay nho hon ngay hien tai ****
**** objFromDate : Thanh phan cua form ****
******************************************/
function FromSmallNowMonth(objFromMonth)
{
	FromDate = Toddmmyyyy_value('01/' + objFromMonth.value)
	var now = new Date();
  	var sMonth = (now.getMonth() + 1).toString();
  	var sDay = now.getDate().toString();
  	var sYear = now.getFullYear().toString();
  	ToDate = sDay + '/' + sMonth + '/' + sYear;
  	
  	
	if ((!isBlank(FromDate)) && (!isBlank(ToDate)))
	{
		if  (!IsSmaller(FromDate, ToDate))
		{			
			return false;
		}
		else
			return true;
	}
	else
		return true;
}
/******************************************
**** Kiem tra ngay nho hon ngay hien tai ****
**** objFromDate : Thanh phan cua form ****
******************************************/
function ToSmallNowMonth()
{
	alert('sss');
	/*FromDate = '01/' + objFromMonth.value	
	var now = new Date();
  	var sMonth = (now.getMonth() + 1).toString();
  	var sDay = now.getDate().toString();
  	var sYear = now.getFullYear().toString();
  	ToDate = sDay + '/' + sMonth + '/' + sYear;
  	
	if ((!isBlank(FromDate)) && (!isBlank(ToDate)))
	{
		alert('ddd');
		if  (!IsSmaller(ToDate,FromDate))
		{			
			return false;
		}
		else
			return true;
	}
	else
		return true;
		*/
}
/******************************************
**** Kiem tra thang nho hon thang den ****
**** objFromDate : Thanh phan cua form ****
******************************************/
function FromSmallToMonth(objFromMonth,objToMonth)
{
	//alert(objToMonth.value)
	var ToDate,FromDate
	FromDate = Toddmmyyyy_value('01/' + objFromMonth.value)
		if (objToMonth.value=='')
			ToDate = '01/01/2100'
		else
			ToDate = '01/'+ objToMonth.value
			
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
function CheckQuarter(field)
{
	if (checkNumeric(field) == false)
		return false;
	value = field.value;
	if (value != "1" &&value != "2" && value != "3" && value != "4")
	{
		//alert("Quarter is invalid!");
		GetAlertError(iTotal,DSAlert,"0043");
		field.focus();
		return false;
	}
}

function Confirm_Delete()
{
	
	if (confirm("Dòng được chọn sẽ bị xoá?"))
		return true;
	else
		return false;
}
function EmailCheck(obj) {
	emailStr = obj.value;
	if (emailStr == "")
		return true;
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
		alert("Địa chỉ email không hợp lệ (check  and .'s)");
		obj.value = "";
		return false;
	}
	
	var user=matchArray[1];
	var domain=matchArray[2];

	if (user.match(userPat)==null) {
		alert("Tên người dùng không hợp lệ");
		obj.value = "";
		return false;
	}

	var IPArray=domain.match(ipDomainPat);
	
	if (IPArray!=null) {
		for (var i=1;i<=4;i++) {
		    if (IPArray[i]>255) {
		        alert("Destination IP address is invalid!");
		        obj.value = "";
				return false;
			}
		}
		return true;
	}

	var domainArray=domain.match(domainPat);
	if (domainArray==null) {
		alert("The domain name doesn't seem to be valid.");
		obj.value = "";
	    return false;
	}

	var atomPat=new RegExp(atom,"g");
	var domArr=domain.match(atomPat);
	var len=domArr.length;
	if (domArr[domArr.length-1].length<2 || 
		domArr[domArr.length-1].length>3) {
		alert("The address must end in a three-letter domain, or two letter country.");
		obj.value = "";
		return false;
	}

	if (len<2) {
		var errStr="This address is missing a hostname!";
		obj.value = "";
		alert(errStr);
		return false;
	}
	return true;
}

//Show calendar
function ShowCalendar(field)
{	
	document.all[field].value = window.showModalDialog('Calendar.htm',document.all[field].value, 'dialogLeft:450px;dialogTop:300px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');	
	//return false;
}

function CheckScanCode(field)
{	
	if( field.value.length!=10 && field.value.length>0)
	{
		GetAlertError(iTotal,DSAlert,"0055");
		field.value="";
		field.focus();
	}
	//return false;
}

//Kiem tra Thang
function CheckMonth_123(field)			
{
	var checkstr = "0123456789";
	var DateField = field;
	var DateValue = "";
	var DateTemp = "";
	var i;
	alert("abcd");
	DateValue = DateField.value;
	//if (DateValue.length == 0) return false;
	for (i = 0; i < DateValue.length; i++) 
	{
		if (checkstr.indexOf(DateValue.substr(i,1)) >= 0) {
			DateTemp = DateTemp + DateValue.substr(i,1);	}
	}
	DateValue = DateTemp;
	
	if (DateValue > 12 || DateValue < 1)		
	{		 
		//alert("Nam không h?p l?!");
		GetAlertError(iTotal,DSAlert,"1080");
		field.value='';
		DateField.focus();
		return false
		
	}
	/*if(DateValue.length==1)
		DateValue='0'+DateValue;*/
	DateValue = '00'+DateValue;
		
	//DateField.value = '00'+DateValue;
	
}
function CheckMonth(field)			
{
	var checkstr = "0123456789";
	var DateField = field;
	var DateValue = "";
	var DateTemp = "";
	var i;
	
	DateValue = DateField.value;
	if (DateValue.length == 0) return;
	for (i = 0; i < DateValue.length; i++) 
	{
		if (checkstr.indexOf(DateValue.substr(i,1)) >= 0) {
			DateTemp = DateTemp + DateValue.substr(i,1);	}
	}
	DateValue = DateTemp;
	
	if (DateValue > 12 || DateValue < 1)		
	{
			 
		//alert("Nam không h?p l?!");
		GetAlertError(iTotal,DSAlert,"1080");
		DateField.focus();
	}
	if(DateValue.length==1)
		DateValue='0'+DateValue;
	DateField.value = DateValue;
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
   if(year < 1900){
		err = 20;
   }
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
		//alert("Định dạng dữ liệu kiểu: mm/yyyy!");
		GetAlertError(iTotal,DSAlert,"0042");
		DateField.focus();	
      }      
   }
   /* Error-message if err != 0 */
   else {
		DateField.value = "";
		DateField.focus();
		//alert("Định dạng dữ liệu kiểu: mm/yyyy!");
		GetAlertError(iTotal,DSAlert,"0042");
   }
}
// -- report 
function ShowReport(report, sql, param, value, path)
			{
				t = screen.height - 30;
				w = screen.width - 10;				
				sql=GetParamReport(sql);
				//FileWindow = window.open(path + '/Reports/COMM_ReportGen.asp?report=' + report + '&sql=' + sql + '&formula=' + param + '&Value=' + value ,'_blank','width=' + w + ', height=' + t + ', left=0, top=0, location=no, scrollbars=yes,resizable');								
				//alert('http://localhost/iHRPCore/Reports/COMM_ReportGen.asp?report=' + report + '&sql=' + sql + '&formula=' + param + '&Value=' + value);
				alert('http://localhost/iHRPCore/Reports/COMM_ReportGen.asp?report=' + report + '&sql=' + sql + '&formula=' + param + '&Value=' + value);
				
				FileWindow = window.open('http://localhost/iHRPCore/Reports/COMM_ReportGen.asp?report=' + report + '&sql=' + sql + '&formula=' + param + '&Value=' + value ,'_blank','width=' + w + ', height=' + t + ', left=0, top=0, location=no, scrollbars=yes,resizable');
				FileWindow.focus();
				return true;	
			}
function replaceChars(entry) {
		out = ","; // replace this
		add = ""; // with this
		temp = "" + entry; // temporary holder

		while (temp.indexOf(out)>-1) {
		pos= temp.indexOf(out);
		temp = "" + (temp.substring(0, pos) + add + 
		temp.substring((pos + out.length), temp.length));
		}
		return temp;
}
function GetParamReport(str)
{		
		for (i=0;i<window.document.forms(0).length-1;i++)
		{
			obj=window.document.forms(0).item(i);
			
			if (str.indexOf(obj.id) != -1 && obj.id!='')
			{				
			
				str=str.replace(obj.id,'N[' + obj.value);
			}
		}
		while (str.indexOf('"')!=-1)
		{
			str=str.replace('"',"'");
			str=str.replace("'N[","N'");
		}
		return str;
}	
/*function GetAlertError(iTotal,obj,sMess)
{		
	var sReturnValue="";
	var i;
	for (i=0;i<=iTotal;i++)
	{			
		if (obj[0][i]==sMess)
		{								
			alert(obj[1][i]);	
			return;		
		}
	}
	
}*/
function GetAlertError(iTotal,obj,sMess)
{		
	var sReturnValue="";
	var i;
	for (i=0;i<=iTotal;i++)
	{			
		if (obj[0][i]==sMess)
		{				
			snMsg=obj[1][i];			
			snTitle='Thông báo - Alert';			
			document.getElementById('btnShow').onclick();			
			return false;		
		}
	}
	return false;
}
function GetAlertTextPopUp(sMess)
{											
			snMsg=sMess;
			snTitle='Thông báo - Alert';			
			document.getElementById('btnShow').onclick();
}
function GetAlertText(iTotal,obj,sMess)
{	
	var sReturnValue="";
	var i;
	for (i=0;i<=iTotal;i++)
	{	
		if (obj[0][i]==sMess)			
		{
			return obj[1][i];	
			break;		
		}
			
	}	
}

/*Tanldt*/
/*
function chkSelect_OnClick(tableRow, checkBox, rowIndex)
{
	var bgColor;
	if(rowIndex%2 == 0)
		bgColor = "#ffffff";
	else
		bgColor = "#f5f5f5";
	if(checkBox.checked == true)
		tableRow.style.backgroundColor = "#b0c4de";
	else
		tableRow.style.backgroundColor = bgColor;
}


function chkSelectAll_OnClick(checkBox,dtgList)
{
	re = new RegExp('_chkSelect$')
	var j = 0;
	for(i = 0; i < document.forms[0].elements.length; i++) 
	{
		elm = document.forms[0].elements[i]
		if (elm.type == 'checkbox') 
		{
			if (re.test(elm.id)) 
			{
				elm.checked = checkBox.checked;
				var tableId = dtgList.toString() + "_row" + j.toString();
				if (tableId != "")
				{
					var rowIndex = tableId.substring(tableId.length - 1, tableId.length);
					chkSelect_OnClick(document.getElementById(tableId), elm, rowIndex);
				}
				j++;
			}
		}
		
	}	
}
*/
//How To Move Items Between Lists with JavaScript
var selectedList;
var availableList;
function createListObjects(sSelectedOptions,sAvailableOptions){
	availableList = document.getElementById(sAvailableOptions);
    selectedList = document.getElementById(sSelectedOptions);
}
function delAttribute(){
	//var selIndex = selectedList.selectedIndex;
	var selIndex;
	for(var i = selectedList.length-1;i >=0;i--){
	  if(selectedList.options[i].selected == true){
		selIndex=selectedList.options[i].index;
		if(selIndex < 0)
        return;
		availableList.appendChild(selectedList.options.item(selIndex))
		}
    }
    selectNone(selectedList,availableList);
    setSize(availableList,selectedList);
}
function addAttribute(){	
    //var addIndex = availableList.selectedIndex;
    var addIndex;
    for(var i = availableList.length-1;i >=0;i--){
		if(availableList.options[i].selected == true){
			addIndex=availableList.options[i].index;					
			if(addIndex < 0)
			return;
		selectedList.appendChild(availableList.options.item(addIndex));
			}
		}
		selectNone(selectedList,availableList);
		setSize(selectedList,availableList);
		    
}
function delAll(){
    var len = selectedList.length -1;
    for(i=len; i>=0; i--){
        availableList.appendChild(selectedList.item(i));
    }
    selectNone(selectedList,availableList);
    setSize(selectedList,availableList);
    
}
function addAll(){
    var len = availableList.length -1;
    for(i=len; i>=0; i--){
        selectedList.appendChild(availableList.item(i));
    }
    selectNone(selectedList,availableList);
    setSize(selectedList,availableList);
    
}
function selectNone(list1,list2){
    list1.selectedIndex = -1;
    list2.selectedIndex = -1;
    addIndex = -1;
    selIndex = -1;
}
function setSize(list1,list2){
    list1.size = getSize(list1);
    list2.size = getSize(list2);
}
function getSize(list){
    /* Mozilla ignores whitespace, IE doesn't - count the elements in the list */
    var len = list.childNodes.length;
    var nsLen = 0;
    //nodeType returns 1 for elements
    for(i=0; i<len; i++){
        if(list.childNodes.item(i).nodeType==1)
            nsLen++;
    }
    if(nsLen<2)
        return 2;
    else
        return nsLen;
}
function showSelected(){
    var optionList = document.getElementById("selectedOptions").options;
    var data = '';
    var len = optionList.length;
    for(i=0; i<len; i++){
        if(i>0)
            data += ',';
        data += optionList.item(i).value;
    }
    alert(data);
}
function DeltaHourToHour(field1,field2)
{
	var sHour1,sMinute1,sHour2,sMinute2;
	var DataTemp1, DataTemp2;
	DataTemp1 = field1.value;
	DataTemp2 = field2.value;
	var Time1, Time2;
	 
	sHour1 = DataTemp1.substring(0,2);
	sMinute1 = DataTemp1.substring(3,5);
	sHour2 = DataTemp2.substring(0,2);
	sMinute2 = DataTemp2.substring(3,5);
	Time1 = eval(sHour1)*60 + eval(sMinute1);
	Time2 = eval(sHour2)*60 + eval(sMinute2);
	return Time2 - Time1;
 
}
function DeltaValueHourToHour(field1,field2,isOffice,hhStart,mmStart,hhEnd,mmEnd, numManHourStandard)
{
	var sHour1,sMinute1,sHour2,sMinute2,sHoursOff1, sHoursOff2, sMinuteOff1, sMinuteOff2;
	var DataTemp1, DataTemp2;
	var return_value;
	return_value=0;
	DataTemp1 = field1.value;
	DataTemp2 = field2.value;
	var Time1, Time2;
	 
	//alert(DataTemp1 + '-' + DataTemp2 + '		' + hhStart + ':' + mmStart + '-' + hhEnd + ':' + mmEnd) 	 
	/*sHoursOff1='08';
	sMinuteOff1='30';
	sHoursOff2='17';
	sMinuteOff2='30';*/
	
	sHoursOff1=hhStart;
	sMinuteOff1=mmStart;
	sHoursOff2=hhEnd;
	sMinuteOff2=mmEnd;
	
	
	TimeOff1=eval(sHoursOff1)*60 + eval(sMinuteOff1);
	TimeOff2=eval(sHoursOff2)*60 + eval(sMinuteOff2);
	 
	sHour1 = DataTemp1.substring(0,2);
	sMinute1 = DataTemp1.substring(3,5);
	sHour2 = DataTemp2.substring(0,2);
	sMinute2 = DataTemp2.substring(3,5);
	Time1 = eval(sHour1)*60 + eval(sMinute1);
	Time2 = eval(sHour2)*60 + eval(sMinute2);

	if (isOffice==1) return Time2 - Time1;
/*	if (Time1<TimeOff1)
		return_value=return_value + (TimeOff1-Time1)
	if (Time1>TimeOff2)
		return_value=return_value + (Time1-TimeOff2)
		
	if (Time2<TimeOff1)
		return_value=return_value + (TimeOff1-Time2)
	if (Time2>TimeOff2)
		return_value=return_value + (Time2-TimeOff2)	
*/
	if (Time1 < TimeOff1)
	{
		if (Time2 <= TimeOff1)
			return_value = Time2 - Time1
		else if (Time2 < TimeOff2)
		{
			return_value = TimeOff1 - Time1
		}
		else // Time2 >= TimeOff2
			return_value = (TimeOff1 - Time1) + (Time2 - TimeOff2)
	}
	else if (Time1 < TimeOff2)
	{		
		if (Time2 > TimeOff2)
		{
			return_value = Time2 - TimeOff2;
		}
		else if(Time2 < Time1) // SonPQ added 06/09/2008: Lam qua dem
		{
			return_value = 24*60 + Time2 - TimeOff2;
		}
	}
	else	// Time1 >= TimeOff2
		return_value = Time2 - Time1;
		
	/*return_value = Time2 - Time1;
	if (return_value <= numManHourStandard * 60)
		return_value = 0;
	else
		return_value = return_value - numManHourStandard * 60;
	*/
	
	return return_value;
}
function checkOfficeHours(field1,isOffice)
{
	if (isOffice==1) return 0;
	
	var sHour1,sMinute1,sHour2,sMinute2,sHoursOff1, sHoursOff2, sMinuteOff1, sMinuteOff2;
	var DataTemp1, DataTemp2;
	var return_value;
	return_value=0;
	DataTemp1 = field1.value;	
	var Time1, Time2, TimeOff1, TimeOff2;
	 
	sHour1 = DataTemp1.substring(0,2);
	sMinute1 = DataTemp1.substring(3,5);	
	sHoursOff1='08';
	sMinuteOff1='30';
	sHoursOff2='17';
	sMinuteOff2='30';
	Time1 = eval(sHour1)*60 + eval(sMinute1);	
	TimeOff1=eval(sHoursOff1)*60 + eval(sMinuteOff1);
	TimeOff2=eval(sHoursOff2)*60 + eval(sMinuteOff2);
	//if ((TimeOff1 - Time1 < 0 && TimeOff1 - Time2 < 0)|| (TimeOff2 - Time1 > 0 && TimeOff2 - Time2>0) )
	//alert(TimeOff1 + '---' + Time1 + '---' + TimeOff2);
	if (Time1<TimeOff1)
		return_value=return_value + (TimeOff1-Time1)
	if (Time1>TimeOff2)
		return_value=return_value + (Time1-TimeOff2)
	
		return return_value;
	
}
function roundNumber(numberValue,rlength) {	
	var rnum = numberValue;	
	if (rnum > 8191 && rnum < 10485) {
		rnum = rnum-5000;
		var newnumber = Math.round(rnum*Math.pow(10,rlength))/Math.pow(10,rlength);
		//newnumber = newnumber+5000;
	} else {
		var newnumber = Math.round(rnum*Math.pow(10,rlength))/Math.pow(10,rlength);
	}
	return newnumber;
}
function FromSmallToMonthValue(objFromMonth,objToMonth)
{
	//alert(objToMonth.value)
	var ToDate,FromDate
	FromDate = Toddmmyyyy_value('01/' + objFromMonth)
		if (objToMonth=='')
			ToDate = '01/01/2100'
		else
			ToDate = '01/'+ objToMonth
			
	if ((!isBlank(FromDate)) && (!isBlank(ToDate)))
	{
		//alert(FromDate + '---' + ToDate);		
		if  (!IsSmallerOrEqual(FromDate, ToDate))
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
/*Cangtt - check value not used on list*/
function checkCboValue(Fields)
{
	if (Fields.selectedIndex!=-1)
	{
		var sText=Fields.options[Fields.selectedIndex].text;	
		
		var sCompare=sText.substring(sText.length-10,sText.length);	
		if (sCompare=='<not used>')
		{
			GetAlertTextPopUp('This selected value not used in list!')		
		}
	}
} 
function IsSmallerOrEqual(inputStr1, inputStr2)
{

	var FromDate = "";
	var ToDate = "";
	
	if (inputStr1.length > 10)
		FromDate = Toddmmyyyy_value(inputStr1);
	
	if (inputStr2.length > 10)	
		ToDate = Toddmmyyyy_value(inputStr2);

	var delim1 = FromDate.indexOf("/");
	var delim2 = FromDate.lastIndexOf("/");
	
	// lay ngay, thang, nam cua ngay1
	var dd1 = parseInt(FromDate.substring(0,delim1),10)
	var mm1 = parseInt(FromDate.substring(delim1 + 1,delim2),10)
	var yyyy1 = parseInt(FromDate.substring(delim2 + 1,FromDate.length),10)

	delim1 = ToDate.indexOf("/")
	delim2 = ToDate.lastIndexOf("/")

	// lay ngay, thang, nam cua ngay2
	var dd2 = parseInt(ToDate.substring(0,delim1),10)
	var mm2 = parseInt(ToDate.substring(delim1 + 1,delim2),10)
	var yyyy2 = parseInt(ToDate.substring(delim2 + 1,ToDate.length),10)

	// ngay1 nho hon ngay 2 ?
	if (yyyy2 > yyyy1)
		return true;
	else if (yyyy2 == yyyy1)
	{
		if (mm2 == mm1) 
			{
				if (dd2 >= dd1) 
				{
					return true;
				}				
				else
				{
					return false;
				}
			}
		else
			{
				if (mm2 > mm1) 
					return true;
				else 
					return false;
			}
	}
	else
		return false;
}

function PageRow_Enter(field)
{
	var str = field.value;
	//checkInteger(field, 9999);
	if (!checkInt(field, 9999))
	{
		field.focus();
		return;
	}

	field.value = str;
		
	if(window.event.keyCode == 13){
		document.getElementById('_ctl0_btnRowNumber').click();
		event.returnValue=false;
		event.cancel = true;		
	}
}
