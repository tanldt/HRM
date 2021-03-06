using System;
using System.Data;
using System.Runtime.InteropServices;
using System.IO;
using System.Web;
using System.Collections;
using System.Globalization;
using Excel;
using System.Diagnostics;
//using Excel = Microsoft.Office.Interop.Excel;

namespace iHRPCore.APPComponent
{
	/// <summary>
	/// Summary description for ExcelReportUtils.
	/// Hỗ trợ xuất report Excel bằng dll có sẵn của .net
	/// V1.0, ngay tao: 18/09/2006
	/// </summary>
	public class ExcelReportUtils
	{
		HttpRequest _Request;
		HttpResponse _Respone;
		
		public ExcelReportUtils()
		{
			_Request = HttpContext.Current.Request;
			_Respone = HttpContext.Current.Response;
		}
		/*TuyenNN ghi chu: copyRows dung de copy 1 vung gioi han cua sheet nay sang 1 sheet nao do (co the la chinh no)
		 * sourceSheet: sheet co cac dong template
		 * targetSheet: sheet can thao tac
		 * pStartColumn: tu cot template can copy (VD: A)
		 * pEndColumn: den cot template can copy (VD: B)
		 * pStartRow: tu dong template can copy (VD: 1)
		 * pEndRow: den dong template can copy (VD: 5)
		 * pPosition: vi tri bat dau cua dong (tren sheet thuc thi) can copy den
		 */
		public void copyRows(Excel.Worksheet sourceSheet, Excel.Worksheet targetSheet, String pStartColumn, String pEndColumn, int pStartRow, int pEndRow, int pPosition)
		{
			Excel.Range sourceRow = sourceSheet.get_Range(pStartColumn+pStartRow, pEndColumn+pEndRow);
			Excel.Range targetRow = targetSheet.get_Range(pStartColumn+pPosition, pEndColumn+(pPosition+(pEndRow-pStartRow)));
			//copy tu row den row
			sourceRow.Copy(targetRow);		
			//set chieu cao cua cell
			int rowCount = sourceRow.Rows.Count;
			int columnCount = sourceRow.Columns.Count;
			Excel.Range sourceCell, targetCell;
			for (int i = 1; i <= rowCount; i++)
			{
				for (int j = 1; j <= columnCount; j++)
				{
					sourceCell = (Excel.Range)sourceRow.get_Item(i, j);
					targetCell = (Excel.Range)targetRow.get_Item(i, j);
					//String address = cell.get_Address(Type.Missing, Type.Missing, Excel.XlReferenceStyle.xlA1, Type.Missing, Type.Missing);
					targetCell.ColumnWidth = sourceCell.ColumnWidth;
				}
			}
		}
		
		/*TuyenNN ghi chu: copyRows dung de copy 1 vung gioi han cua sheet nay sang 1 sheet nao do (co the la chinh no)
		 * Ghi chu 2: vua copy tung cell mot, vua duyet Hashtable de replace gia tri cac parameter can thiet tren Excel
		 * sourceSheet: sheet co cac dong template
		 * targetSheet: sheet can thao tac
		 * pStartColumn: tu cot template can copy (VD: A)
		 * pEndColumn: den cot template can copy (VD: B)
		 * pStartRow: tu dong template can copy (VD: 1)
		 * pEndRow: den dong template can copy (VD: 5)
		 * pPosition: vi tri bat dau cua dong (tren sheet thuc thi) can copy den
		 * hashParameters: cac params can duyet de dien gia tri vao
		 */
		public void copyRowsAndReplace(Excel.Worksheet sourceSheet, Excel.Worksheet targetSheet, String pStartColumn, String pEndColumn, int pStartRow, int pEndRow, int pPosition, Hashtable hashParameters)
		{
			//Lay cac key name tu Hashtable hashParameters
			String[] keyName = new String[hashParameters.Count];
			IDictionaryEnumerator myEnumerator = hashParameters.GetEnumerator();
			int a=0;
			while(myEnumerator.MoveNext()){
				keyName[a]=myEnumerator.Key.ToString();
				a++;
			}
			//***end Lay cac key name tu Hashtable hashParameters

			Excel.Range sourceRow = sourceSheet.get_Range(pStartColumn+pStartRow, pEndColumn+pEndRow);
			Excel.Range targetRow = targetSheet.get_Range(pStartColumn+pPosition, pEndColumn+(pPosition+(pEndRow-pStartRow)));
			//copy tu row den row
			sourceRow.Copy(targetRow);		
			//set chieu cao cua cell
			int rowCount = sourceRow.Rows.Count;
			int columnCount = sourceRow.Columns.Count;
			Excel.Range sourceCell, targetCell;
			String strContentCell="";
			//scan tung cell
			for (int i = 1; i <= rowCount; i++)
			{
				for (int j = 1; j <= columnCount; j++)
				{
					sourceCell = (Excel.Range)sourceRow.get_Item(i, j);
					targetCell = (Excel.Range)targetRow.get_Item(i, j);
					targetCell.RowHeight = sourceCell.RowHeight; 
					targetCell.ColumnWidth = sourceCell.ColumnWidth;
					//scan qua cac key de so sanh co phai la noi dung cua cell hay ko
					try{
						strContentCell=@targetCell.Value2.ToString();
					}
					catch(Exception){
						strContentCell="";
					}
					
					if (strContentCell.IndexOf("#",0,strContentCell.Length) != -1) 
					{
						for (int z = 0; z < keyName.Length; z++) 
						{
							strContentCell = strContentCell.Replace(@"#" + keyName[z] + @"#", hashParameters[keyName[z]].ToString());
							targetCell.Value2=strContentCell;
						}
					}
				}
			}
		}
		
		public void setSheetPageSetup(Excel.Worksheet sourceSheet, Excel.Worksheet targetSheet){
			try
			{
				//Orientation: kho giay ngang hay doc
				targetSheet.PageSetup.Orientation = sourceSheet.PageSetup.Orientation;
				//zoom to adjust size
				targetSheet.PageSetup.Zoom=sourceSheet.PageSetup.Zoom;
				//margin
				targetSheet.PageSetup.LeftMargin = sourceSheet.PageSetup.LeftMargin;
				targetSheet.PageSetup.RightMargin = sourceSheet.PageSetup.RightMargin;
				targetSheet.PageSetup.TopMargin = sourceSheet.PageSetup.TopMargin;
				targetSheet.PageSetup.BottomMargin = sourceSheet.PageSetup.BottomMargin;
				targetSheet.PageSetup.FooterMargin = sourceSheet.PageSetup.FooterMargin;
				targetSheet.PageSetup.HeaderMargin = sourceSheet.PageSetup.HeaderMargin;
				//footer
				targetSheet.PageSetup.RightFooter = sourceSheet.PageSetup.RightFooter;
				targetSheet.PageSetup.LeftFooter = sourceSheet.PageSetup.LeftFooter;
				targetSheet.PageSetup.CenterFooter = sourceSheet.PageSetup.RightFooter;
				//header
				targetSheet.PageSetup.RightHeader = sourceSheet.PageSetup.RightHeader;
				targetSheet.PageSetup.LeftHeader = sourceSheet.PageSetup.LeftHeader;
				targetSheet.PageSetup.CenterHeader = sourceSheet.PageSetup.RightHeader;
				//paper size
				targetSheet.PageSetup.PaperSize = sourceSheet.PageSetup.PaperSize;
			}
			catch (System.Exception e)
			{
				
			}
		}

		/*TuyenNN ghi chu: Export ra Excel 
		 * rootDir: thu muc chua file excel mau
		 * excelFileName: ten file excel mau
		 * objData: DataView can xuat
		 * hashHeading: Cac thong so can dat o Heading
		 * hashEnding: Cac thong so can dat o Ending
		 */ 
		public String exportExcel(String rootDir, String excelFileName, DataView objData, Hashtable hashHeading, Hashtable hashEnding)		
		{
			int TEMPLATE_SHEET_NUM=1;
			String NEW_FILE_NAME="";
			int START_HEADING=1;
			int END_HEADING=1;
			int START_ENDING=1;
			int END_ENDING=1;
			String START_COLUMN="";
			String END_COLUMN="";
			int DETAIL_ROW=1;
			int NUMBER_OF_COLUMNS=1;
			int NUMBER_OF_SUMALL_COLUMNS=1;
			int SUMALL_ROW=1;
			int NUMBER_OF_GROUP=1;
			int NUMBER_OF_SUM_GROUP=1;
			
			Excel.Application myExcelApp = new Excel.Application();
			Excel.Workbook excelWorkbook = null;
			
			String path = HttpContext.Current.Server.MapPath(@"~/Reports/ReportExcel/" + rootDir + "/" + excelFileName);
			String fullFileName = path;
			try
			{
				//Mo va tao file Excel
				myExcelApp.Visible = true;
				excelWorkbook = myExcelApp.Workbooks.Open(path,
					0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "",
					true, false, 0, true, false, false); 
				Excel.Sheets sheets = excelWorkbook.Sheets;

				//Lay sheet config cua workbook
				Excel.Worksheet configSheet = (Excel.Worksheet)sheets.get_Item(2);

				//Doc 1 so thong so can thiet tu sheet config
				TEMPLATE_SHEET_NUM = parseToInt(configSheet.get_Range("B1","B1").Text.ToString(),0);
				NEW_FILE_NAME = configSheet.get_Range("B2","B2").Text.ToString();
				START_HEADING = parseToInt(configSheet.get_Range("B3","B3").Text.ToString(),0);
				END_HEADING = parseToInt(configSheet.get_Range("B4","B4").Text.ToString(),0);
				START_ENDING = parseToInt(configSheet.get_Range("B5","B5").Text.ToString(),0);
				END_ENDING = parseToInt(configSheet.get_Range("B6","B6").Text.ToString(),0);
				START_COLUMN = configSheet.get_Range("B7","B7").Text.ToString();
				END_COLUMN = configSheet.get_Range("B8","B8").Text.ToString();
				DETAIL_ROW = parseToInt(configSheet.get_Range("B9","B9").Text.ToString(),0);
				SUMALL_ROW = parseToInt(configSheet.get_Range("B10","B10").Text.ToString(),0);
				NUMBER_OF_COLUMNS = parseToInt(configSheet.get_Range("D1","D1").Text.ToString(),0);
				NUMBER_OF_SUMALL_COLUMNS = parseToInt(configSheet.get_Range("G1","G1").Text.ToString(),0);
				NUMBER_OF_GROUP = parseToInt(configSheet.get_Range("I1","I1").Text.ToString(),0);
				NUMBER_OF_SUM_GROUP = parseToInt(configSheet.get_Range("I5","I5").Text.ToString(),0);
				//********************************************
				
				fullFileName = @"C:\" + NEW_FILE_NAME;
				//Check neu file da ton tai thi delete no di de minh con chep thanh file khac nua		
				if(File.Exists(fullFileName))
				{
					File.Delete(fullFileName);
				}
				//***Check neu file da ton tai thi delete

				//Sheet template
				Excel.Worksheet tempSheet = (Excel.Worksheet)sheets.get_Item(1);

				//Sheet thao tac: dung truoc sheet template
				Excel.Worksheet targetSheet = (Excel.Worksheet)sheets.Add(tempSheet,Type.Missing, 1, Type.Missing);
				this.setSheetPageSetup(tempSheet, targetSheet); //set page: margin, pager size, footer, header...
				targetSheet.Name = "My Sheet";
				targetSheet.Activate();
				//***end Sheet thao tac
				
				int currentRow=1;//bien nay se la row pionter trong suot qua trinh dien du lieu

				//heađing
				//this.copyRows(tempSheet, targetSheet, START_COLUMN, END_COLUMN, START_HEADING, END_HEADING, currentRow);
				this.copyRowsAndReplace(tempSheet, targetSheet, START_COLUMN, END_COLUMN, START_HEADING, END_HEADING, currentRow,hashHeading);
				currentRow = currentRow + (END_HEADING-START_HEADING + 1);
				//currentRow++;
				//***end heading

				//get column name
				String[] columnName = new String[objData.Table.Columns.Count];
				for(int k=0; k<objData.Table.Columns.Count; k++)
				{
					columnName[k] = objData.Table.Columns[k].ColumnName.ToString();
				}
				//***end column name

				//get column name on Excel
				String[] columnName_write_Excel = new String[NUMBER_OF_COLUMNS];
				String[] datatype_Of_Column = new String[NUMBER_OF_COLUMNS];
				int tempNum=2; //thong so bat dau tu D2, nen tempNum la so 2
				for(int z=0; z<NUMBER_OF_COLUMNS; z++)
				{
					columnName_write_Excel[z] = configSheet.get_Range("D"+tempNum,"D"+tempNum).Text.ToString();
					datatype_Of_Column[z] = configSheet.get_Range("E"+tempNum,"E"+tempNum).Text.ToString();
					tempNum++;
				}
				//***end get column name on Excel

				//get column sum all
				Double[] sumAllValue = new Double[NUMBER_OF_SUMALL_COLUMNS];
				int[] columnNumber = new int[NUMBER_OF_SUMALL_COLUMNS];
				tempNum=2;
				for(int z=0; z<NUMBER_OF_SUMALL_COLUMNS; z++)
				{
					columnNumber[z] = parseToInt(configSheet.get_Range("G"+tempNum,"G"+tempNum).Text.ToString(),0);
					tempNum++;
				}
				//*** end get column sum all
				
				//Lay len group
				int groupByCount = NUMBER_OF_GROUP;
				String[] groupBy = new String[groupByCount];
				int[] groupByTemp = new int[groupByCount];
				int[] groupSumPosition = new int[groupByCount];
				String[] groupBy_current = new String[groupByCount];
				String[] groupBy_old = new String[groupByCount];
				String[] groupBy_current_next = new String[groupByCount];
				String[] groupBy_old_next = new String[groupByCount];
				tempNum=2;
				for (int i = 0; i < groupByCount; i++) 
				{
					//Group boi cai gi?
					groupBy[i] = configSheet.get_Range("I"+tempNum,"I"+tempNum).Text.ToString();
					//Dong mau cua group
					groupByTemp[i] = Math.Abs(parseToInt(configSheet.get_Range("B"+ (11+i),"B" + (11+i)).Text.ToString(),0));
					//Vi tri dat group (tren, duoi), 0: tren(default), 1: duoi
					if(parseToDouble(configSheet.get_Range("B"+ (11+i),"B" + (11+i)).Text.ToString(),0)<0){
						groupSumPosition[i] = 1; //dat duoi
					}
					else{
						groupSumPosition[i] = 0; //dat tren
					}
					
					tempNum++;
				}
				//*** end lay len group
				
				//Lay len sum group
				int sumGroupCount = NUMBER_OF_SUM_GROUP;
				//sumGroupValue[][]: co 2 chieu dai: dem xem co bao nhieu phan tu can tinh sum,
				// va co bao nhieu loai phan group
				Double[,] sumGroupValue = new Double[groupByCount,sumGroupCount];
				double[,] sumElement = new double[groupByCount,sumGroupCount];
				int[] countRecords = new int[groupByCount];
				int[] sumGroupOf = new int[sumGroupCount];
				tempNum=6;
				for (int i = 0; i < sumGroupCount; i++) 
				{
					//bien sumAll: chua cac vi tri can tinh sum all
					sumGroupOf[i] = parseToInt(configSheet.get_Range("I"+ (tempNum+i),"I" + (tempNum+i)).Text.ToString(),0);
				}
				//*** end lay len sum group

				//start body of report
				Excel.Range myCell, myRange;
				Excel.Range[] hrGroup=new Excel.Range[groupByCount];
				String cellData="";
				String dataType="";
				for(int i=0; i< objData.Count; i++)
				{
					//phan group
					if (groupByCount > 0) 
					{
						//Dung vong for de tao cac group phan cach
						for (int ii = 0; ii < groupByCount; ii++) 
						{
							//Dau tien gan cho groupBy_old = -1
							if (i == 0) 
							{
								groupBy_old[ii] = "-1";
							}
							groupBy_current[ii] = objData[i][groupBy[ii]].ToString();
							if (!groupBy_old[ii].Equals(groupBy_current[ii])) 
							{
								this.copyRows(tempSheet, targetSheet, START_COLUMN, END_COLUMN, groupByTemp[ii], groupByTemp[ii], currentRow);
								//Luu tru lai row cua group nay
								hrGroup[ii] = targetSheet.get_Range(START_COLUMN+currentRow,END_COLUMN+currentRow);
								//--------------
								myCell = (Excel.Range)hrGroup[ii].Cells[1,1];
								myCell.Value2=groupBy_current[ii];
								for (int iii = ii; iii < groupByCount; iii++) 
								{
									//reset lai bien dem 0;
									countRecords[iii] = 0;
									//Reset lai bien old
									groupBy_old[iii] = groupBy_current[iii];
								}
								currentRow++;
							}
						}
					}
					//***end phan group
					
					//Dien du lieu chi tiet vo cac cell chi tiet
					this.copyRows(tempSheet, targetSheet, START_COLUMN, END_COLUMN, DETAIL_ROW, DETAIL_ROW, currentRow);
					myRange = targetSheet.get_Range(START_COLUMN+currentRow,END_COLUMN+currentRow);
					for(int h=0; h<NUMBER_OF_COLUMNS; h++)
					{
						//Phong truong hop column nay ko ton tai
						dataType=datatype_Of_Column[h];
						try{
							cellData=objData[i][columnName_write_Excel[h]].ToString();
						}
						catch(Exception e){
							cellData="";
						}
						
						if(dataType.Equals("Count_Number"))
						{ //dang so thu tu
							cellData= "" + (i+1);
						}
						else if(dataType.Equals("Number"))
						{
							cellData=parseToInt(cellData,0).ToString();
						}
						else if(dataType.Equals("Double"))
						{
							cellData=parseToDouble(cellData,0).ToString();
						}
						else if(dataType.Equals("MonthYear")){
							cellData="'" + getDateTimeString(cellData,"MM/yyyy");
						}
						else if(dataType.Equals("DayMonthYear")){
							cellData="'" + getDateTimeString(cellData,"dd/MM/yyyy");
						}
						else
						{
							cellData=cellData;
						}
						//Cell[1: dong so may trong range, h+1: cot so may trong range]
						myCell=(Excel.Range)myRange.Cells[1,h+1];
						if(dataType.Equals("String")){
							myCell.Value2= "'" + cellData;
						}
						else{
							myCell.Value2= cellData;
						}
					}
					//*** end Dien du lieu chi tiet vo cac cell chi tiet

					//Tinh sum all
					if (NUMBER_OF_SUMALL_COLUMNS > 0) 
					{
						for (int ii = 0; ii < NUMBER_OF_SUMALL_COLUMNS; ii++) 
						{
							//Neu vong for nay la first thi gan cac phan tu bang 0;
							if (i == 0) 
							{
								sumAllValue[ii] = 0;
							}
							sumAllValue[ii] += parseToDouble(objData[i][columnName_write_Excel[columnNumber[ii]-1]].ToString(),0);
							Console.WriteLine(sumAllValue[ii]);
						}
					}
					//***end Tinh sum all
					
					//Tinh sum theo group
					if (sumGroupCount > 0 && groupByCount > 0) 
					{
						//Thuc hien vong for theo so group (VD: group theo MSPHONGBAN, MSBOPHAN)
						//for (int ii = 0; ii < groupByCount; ii++) {
						for (int ii = groupByCount - 1; ii >= 0; ii--) 
						{
							//Tang bien dem
							countRecords[ii]++;
							//Reset lai 2 bien next
							groupBy_old_next[ii] = groupBy_old[ii];
							groupBy_current_next[ii] = "-1";
							if ((i + 1) < objData.Count) 
							{
								groupBy_current_next[ii]=objData[i+1][groupBy[ii]].ToString();
							}
							if (groupBy_current_next[ii] != groupBy_current[ii]) 
							{
								//neu vi tri dat groupsum o duoi
								if (groupSumPosition[ii] == 1) 
								{
									currentRow++;
									this.copyRows(tempSheet, targetSheet, START_COLUMN, END_COLUMN, groupByTemp[ii], groupByTemp[ii], currentRow);
									hrGroup[ii] = (Excel.Range)targetSheet.get_Range(START_COLUMN+currentRow,END_COLUMN+currentRow);;
								}
							}
							//Thuc hien vong for theo cac sum can tinh
							for (int xx = 0; xx < sumGroupCount; xx++) 
							{
								if (i == 0) 
								{
									sumElement[ii,xx] = 0;
								}
								sumElement[ii,xx] += parseToDouble(objData[i][columnName_write_Excel[sumGroupOf[xx]-1]].ToString(),0);
								if (groupBy_current_next[ii] != groupBy_current[ii]) 
								{
									if (i == 0) 
									{
										sumGroupValue[ii,xx] = 0;
									}
									//Ghi sum ra Excel
									myCell = (Excel.Range)hrGroup[ii].Cells[1,sumGroupOf[xx]];
									myCell.Value2=sumElement[ii,xx];
									sumElement[ii,xx] = 0;
								}
							}
						}
					}
					//*** end Tinh sum theo group

					currentRow++;
				}
				//***end body of report
				//write sum all
				if (NUMBER_OF_SUMALL_COLUMNS > 0) 
				{
					this.copyRows(tempSheet, targetSheet, START_COLUMN, END_COLUMN, SUMALL_ROW, SUMALL_ROW, currentRow);
					myRange = targetSheet.get_Range(START_COLUMN+currentRow,END_COLUMN+currentRow);
					for (int i = 0; i < NUMBER_OF_SUMALL_COLUMNS; i++) 
					{
						myCell=(Excel.Range)myRange.Cells[1,columnNumber[i]];
						myCell.Value2=sumAllValue[i];
					}
					currentRow++;
				}
				//***end write sum all

				//ending
				//currentRow++;
				//currentRow++;
				//this.copyRows(tempSheet, targetSheet, START_COLUMN, END_COLUMN, START_ENDING, END_ENDING, currentRow);
				this.copyRowsAndReplace(tempSheet, targetSheet, START_COLUMN, END_COLUMN, START_ENDING, END_ENDING, currentRow, hashEnding);
				currentRow = currentRow + (END_ENDING-START_ENDING + 1);

				//Sau khi dien du lieu xong, delete sheet template, config
				myExcelApp.DisplayAlerts=false;
				((Excel.Worksheet)sheets.get_Item(2)).Delete();
				((Excel.Worksheet)sheets.get_Item(2)).Delete();
				//*** end Sau khi dien du lieu xong, delete sheet template, config

				//Save file
				excelWorkbook.SaveAs(fullFileName, 
					Excel.XlFileFormat.xlXMLSpreadsheet, 
					Type.Missing, Type.Missing, Type.Missing, Type.Missing,
					Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, 
					Type.Missing, Type.Missing, Type.Missing, Type.Missing);
				//*** end Save file
			}
			catch(Exception ex)
			{
				Console.Write(ex.Message);
				fullFileName="";
			}
			finally 
			{
				
				//Close workbook, close app
//				excelWorkbook.Close(false, Type.Missing, Type.Missing);
//				myExcelApp.Visible=false;
//				myExcelApp.Quit();
//				// Make sure we release the reference to the underlying COM object
//				Marshal.ReleaseComObject(myExcelApp);
			
				//Truccm them vao de huy progress cua Excel
				foreach ( Workbook wb in myExcelApp.Workbooks)
				{
					foreach ( Excel.Worksheet ws in wb.Worksheets )
					{
						Marshal.ReleaseComObject(ws);
						//ws = null;
					}
					wb.Close(false, null, null);
					Marshal.ReleaseComObject(wb);
					//wb = null;
				}
				myExcelApp.Workbooks.Close();
				myExcelApp.Visible=false;
				myExcelApp.Quit();
				Marshal.ReleaseComObject(myExcelApp);
				myExcelApp=null;
				
				GC.Collect();
				GC.WaitForPendingFinalizers();
				GC.Collect();
				GC.WaitForPendingFinalizers();
				
				try
				{
					foreach (Process procArr in  Process.GetProcessesByName("EXCEL"))
					{
						procArr.Kill();
					}
				}
				catch (System.Exception e){}
				//*** Truccm them vao de huy progress cua Excel
			}
			return fullFileName;
		}

		public Double parseToDouble(String str, Double valDef)
		{
			Double dValue=0;
			try
			{
				dValue = Double.Parse(str);
			}
			catch(Exception e)
			{	
				//Dung trong truong hop dem cac ky tu X
				if(str.Trim().Equals("X")){
					dValue=1;
				}
				else{
					dValue=valDef;
				}
			}
			return dValue;
		}
		public int parseToInt(String str, int valDef)
		{
			int iValue=0;
			try
			{
				iValue = int.Parse(str);
			}
			catch(Exception e)
			{
				iValue=valDef;
			}
			return iValue;
		}

		public string getDateTimeString(string strDateString, string strFormat)
		{
			String strDate="";
			try
			{
				//String strDefaultFormat = "dd/MM/yyyy HH:mm:ss tt";
				DateTime MyDateTime = DateTime.Parse(strDateString);
				//DateTime MyDateTime = DateTime.ParseExact(strDateString,strDefaultFormat,null);
				strDate = MyDateTime.ToString(strFormat);
			}
			catch(Exception e)
			{
				strDate="";
			}
			return strDate;
		}

		public String formatNumber(String strNumber, String defVal){
			string formattedNumber = "";
			try
			{
				formattedNumber= String.Format("{0:#,##0}", Convert.ToDecimal(strNumber));
			}
			catch (System.Exception e)
			{
				formattedNumber=defVal;
			}
			return formattedNumber;
		}
	}
}
