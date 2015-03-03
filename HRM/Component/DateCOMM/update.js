var RelatedFolder=1;
var VirtualFolder='/';
var CurrentFolder='/';
var PageHost='http://';
var MasterWindow = parent;
var StartID = 0;
var CellClass =  new Array(  0, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3);

function ShowTime()
{
	var v = (new Date());
	var h, m, s;

	h = v.getHours();
	m = v.getMinutes();
	s = v.getSeconds();

	document.getElementById('Clock').innerHTML = v.toString().substr(4, 6).concat(', ').concat((h < 10) ? '0'.concat(h) : h).concat(':').concat((m < 10) ? '0'.concat(m) : m).concat(':').concat((s < 10) ? '0'.concat(s) : s);
}

function RefreshMe()
{
	parent.document.location.replace('../TTGDCK/DesktopModules/WebCK.GiaoDich/WebCK_Test.aspx');
}

function UpdateItem(iName)
{
	document.getElementById(iName).innerHTML = parent.frames[1].document.getElementById(iName).innerHTML;
}

function UpdatePage()
{
	var i, j, srow, drow, cell, as;
	var stab = parent.frames[1].document.getElementById('QuoteTable');
	var dtab = document.getElementById('QuoteTable');

	for (i = 2; i < stab.rows.length; i++)
	{
		srow = stab.rows[i];
		if (i >= dtab.rows.length) 
		{
			drow = dtab.insertRow(i);
			drow.height = 21;
			as = (i % 2) + 1;
			for (j = 0; j < srow.cells.length; j++) 
			{
				cell = drow.insertCell(j);
				cell.className = 'SQ'.concat(CellClass[j]).concat(as);
				if (j == 4)
				{
					if (srow.cells[j].innerHTML < 0)
						cell.className = (cell.className).concat('N');
					else if (srow.cells[j].innerHTML > 0)
						cell.className = (cell.className).concat('P');
				}
				cell.innerHTML = srow.cells[j].innerHTML;
				cell.abbr = 0;
			}
		}
		else
		{
			drow = dtab.rows[i];
			if (drow.cells[0].innerText != srow.cells[0].innerText)
			{
				for (j = 0; j < srow.cells.length; j++) 
				{
					drow.cells[j].title = '';
					drow.cells[j].innerHTML = srow.cells[j].innerHTML;
				}
			}
			else
			{
				for (j = 1; j < srow.cells.length; j++) 
				{
					if ((cell=drow.cells[j]).innerText == srow.cells[j].innerText)
					{
						if (cell.innerHTML.substr(0, 3) != '<B>')
							continue;

						if (parseInt(cell.abbr) > (new Date()).getTime())
							continue;

						cell.title = '';
						cell.innerHTML = cell.innerText;
					}
					else
					{
						cell.title = 'Prev: '.concat(cell.innerText);
						as = (i % 2) + 1;
						cell.className = 'SQ'.concat(CellClass[j]).concat(as);
						if (j == 4)
						{
							if (srow.cells[j].innerHTML < 0)
								cell.className = (cell.className).concat('N');
							else if (srow.cells[j].innerHTML > 0)
								cell.className = (cell.className).concat('P');
						}
						cell.innerHTML = '<B><BIG>'.concat(srow.cells[j].innerText).concat('</BIG></B>');
						cell.abbr = (new Date()).getTime() + 60000;
					}
				}
			}
		}
	}

	for (j = dtab.rows.length - 1; j >= i; j--)
	{
		dtab.deleteRow(dtab.rows[j].rowIndex);
	}

	UpdateItem('VNIndex');
	UpdateItem('ChangeIndex');
	UpdateItem('TotalValue');
	UpdateItem('TotalShare');
	UpdateItem('StockText');

	
 	var	curDate = new Date();
	if (curDate.getHours() == 9 && curDate.getDay() >= 1 && curDate.getDay() <=5)
	{
		if (curDate.getMinutes() <= 20)
		{
			
			dtab.rows[0].cells[2].innerText = 'Giá Dự kiến';
			dtab.rows[0].cells[3].innerText = 'KL';
		}

		if ((curDate.getMinutes() > 20) && (curDate.getMinutes() <=55))
		{
			
			dtab.rows[0].cells[2].innerText = 'Giá Khớp ';
			dtab.rows[0].cells[3].innerText = 'KL ';
		}
		
		if (curDate.getMinutes() >55)
		{
			dtab.rows[0].cells[2].innerText = 'Giá Dự kiến';
			dtab.rows[0].cells[3].innerText = 'KL Dự kiến';
		}
	}
	if (curDate.getHours() == 10 && curDate.getDay() >= 1 && curDate.getDay() <=5)
	{
		if (curDate.getMinutes() <= 30)
		{
			dtab.rows[0].cells[2].innerText = 'Giá Dự kiến';
			dtab.rows[0].cells[3].innerText = 'KL Dự kiến';
		}
		else
		{
			dtab.rows[0].cells[2].innerText = 'Giá Khớp ';
			dtab.rows[0].cells[3].innerText = 'KL ';
		}
	}

	if (curDate.getHours() < 9 && curDate.getDay() >= 1 && curDate.getDay() <=5)
			dtab.rows[0].cells[2].innerText = 'Giá Khớp ';
			dtab.rows[0].cells[3].innerText = 'KL';
	}
	


