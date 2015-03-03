using System;
using iHRPCore.Com;
using System.Data;
using System.Collections;
using iHRPCore.APPComponent;


namespace iHRPCore.Com
{
	/// <summary>
	/// Summary description for clsExcel.
	/// </summary>
	/// 
	public class clsExcel
	{
		
		public clsExcel()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		public static DataView getDataViewC47(string strCompany, string strSumEmpPrev, 
			string strWageFundPrev, string strThang, string strStage, ref Hashtable hstHeading, ref Hashtable hstEnding)
		{
			DataTable dataTable = clsCommon.GetDataTable("SI_sprptC47 @Date=N'"+strThang+"', @PrevLabourCount="+strSumEmpPrev+
				", @PrevSumSal="+strWageFundPrev+", @Stage="+strStage+", @LSCompanyID=N'"+strCompany+"'");
					
			DataView dataView = new DataView(dataTable);
			//lay du lieu heading
			hstHeading.Add("Month",strThang.Substring(3,2));
			hstHeading.Add("Year",strThang.Substring(7,4));
			hstHeading.Add("Stage", strStage);

			//lay du lieu ending	
			ExcelReportUtils objReport = new ExcelReportUtils();
			hstEnding.Add("PreTotalEmp",dataTable.Rows.Count!=0?objReport.formatNumber(dataTable.Rows[0]["PreTotalEmp"].ToString(),""):"");
			hstEnding.Add("PreTotalFund",dataTable.Rows.Count!=0?objReport.formatNumber(dataTable.Rows[0]["PreTotalFund"].ToString(),""):"");
			hstEnding.Add("TotalFund",dataTable.Rows.Count!=0?objReport.formatNumber(dataTable.Rows[0]["TotalFund"].ToString(),""):"");
			hstEnding.Add("TotalEmp",dataTable.Rows.Count!=0?objReport.formatNumber(dataTable.Rows[0]["TotalEmp"].ToString(),""):"");
			hstEnding.Add("FundSubmit",dataTable.Rows.Count!=0?objReport.formatNumber(dataTable.Rows[0]["FundSubmit"].ToString(),""):"");
			
			return dataView;
		}

		public static DataView getDataViewC47A(int month, int year, string fromDate, string toDate,string strCompany,
				ref Hashtable hstHeading, ref Hashtable hstEnding  )
        {
			DataTable dataTable =  clsCommon.GetDataTable("SI_sprptC47a @MM="+month+",@YYYY="+year+",@FromDate=N'"+fromDate+
									"',@ToDate=N'"+toDate+"',@LSCompanyID=N'"+strCompany+"'");
			DataView dataView = new DataView(dataTable);
			//lay du lieu heading						
			hstHeading.Add("Month",Convert.ToString(month));
			hstHeading.Add("Year",Convert.ToString(year));
			
			return dataView;
		}

		public static DataView getDataViewC45(string strYear, string strCompany, string strToDate, ref Hashtable hstHeading, ref Hashtable hstEnding)
		{
			DataTable dataTable = clsCommon.GetDataTable("SI_sprptC45 @Year=N'"+strYear+"',"+
				"@LSCompanyID=N'"+strCompany+"',@ToDate=N'"+strToDate+"'");
			DataView dataView = new DataView(dataTable);
			//lay du lieu heading	
			hstHeading.Add("Year",strYear);

			return dataView;
		}
		public static DataView getDataViewC45C(string strLocation,string strFromDate,string strToDate,int isType,string strCompany,
			ref Hashtable hstHeading, ref Hashtable hstEnding)
		{
			DataTable dataTable = clsCommon.GetDataTable("SI_sprptC45c @Location=N'"+strLocation+"',@FromDate=N'"+strFromDate+"',@ToDate=N'"+
							strToDate+"',@IsType="+isType+",@LSCompanyID=N'"+strCompany+"'");
			
			DataView dataView = new DataView(dataTable);
			//lay du lieu heading	
			hstHeading.Add("FromDate",strFromDate);
			hstHeading.Add("ToDate",strToDate);
			//lay nam hien tai
			DateTime curDate = DateTime.Now;
			hstHeading.Add("Year",curDate.ToString("yyyy"));

			return dataView;
		}
		public static DataView getDataViewC04BH(string strQuater, string strMonth, string strYear,
							string strXGroup, string strFromDate, string strToDate, int intStatus, string strCompanyId, 
							ref Hashtable hstHeading, ref Hashtable hstEnding)
		{
			DataTable dataTable = clsCommon.GetDataTable("SI_sprptC04BH @Quarter=N'"+strQuater+"',@Month=N'"
					+strMonth+"',@Year=N'"+strYear+"',@XGroup=N'"+strXGroup+"',@FromDate=N'"+strFromDate+"',"
						+"@ToDate=N'"+strToDate+"',@Status="+intStatus+",@LSCompanyID=N'"+strCompanyId+"'");							
			//store da sap xem san, 
			DataView dataView = new DataView(dataTable);
			
			ExcelReportUtils objReport = new ExcelReportUtils();
			//lay gia tri cho Heading
			hstHeading.Add("Quarter",strQuater);
			hstHeading.Add("Year",strYear);
			hstHeading.Add("Loai",dataTable.Rows.Count!=0?dataTable.Rows[0]["XGroupName"]:"");
			hstHeading.Add("TotalFund",dataTable.Rows.Count!=0?objReport.formatNumber(dataTable.Rows[0]["TotalFund"].ToString(),""):"");
			hstHeading.Add("TotalEmpFemale",dataTable.Rows.Count!=0?objReport.formatNumber(dataTable.Rows[0]["TotalEmpFemale"].ToString(),""):"");
			hstHeading.Add("TotalEmp",dataTable.Rows.Count!=0?objReport.formatNumber(dataTable.Rows[0]["TotalEmp"].ToString(),""):"");
					
			return dataView;

		}
		public static DataView getDataViewC04BH_NDS(string strQuater,string strYear,string strFromDate,
					string strToDate,int intStatus,string strCompany, Double intApplyAmount, Double intNotYetAmount,
					ref Hashtable hstHeading, ref Hashtable hstEnding)
		{
			
			DataTable dataTable = clsCommon.GetDataTable("SI_sprptC04BH_NDS @Quarter=N'"+strQuater+"'"+
				",@Year=N'"+strYear+"',@FromDate=N'"+strFromDate+"',@ToDate=N'"+strToDate+"',"+
				"@Status="+intStatus+",@LSCompanyID=N'"+strCompany+"',@ApplyAmount="+intApplyAmount+",@NotYetAmount="+intNotYetAmount);
			DataView dataView = new DataView(dataTable);
			dataView.Sort="XGroup";
			
			Double lgDuongSuc = Convert.ToInt64(Math.Ceiling(intApplyAmount*0.03));
			Double lgTotalDuongSuc = lgDuongSuc+intNotYetAmount;
			ExcelReportUtils objReport = new ExcelReportUtils();
			//lay gia tri cho heading
			hstHeading.Add("Quarter",strQuater);
			hstHeading.Add("Year",strYear);
			hstHeading.Add("ApplyAmount",objReport.formatNumber(intApplyAmount.ToString(),"0"));
			hstHeading.Add("DuongSuc",objReport.formatNumber(lgDuongSuc.ToString(),"0"));
			hstHeading.Add("NotYetAmount",objReport.formatNumber(intNotYetAmount.ToString(),"0"));
			hstHeading.Add("TotalDuongSuc",objReport.formatNumber(lgTotalDuongSuc.ToString(),"0"));
		
			return dataView;
		}
		public static DataView getDataViewC02SBH(string strYear, string strQuarter,string strMonthYear,string strCompany,
						ref Hashtable hstHeading, ref Hashtable hstEnding)
		{
			DataTable dataTable = clsCommon.GetDataTable("SI_sprpt02_SBH @Year=N'"+strYear+"',@Quarter=N'"+strQuarter+"',"
				+"@MMYYYY=N'"+strMonthYear+"',@LSCompanyID=N'"+strCompany+"'");
			DataView dataView = new DataView(dataTable);

			//lay gia tri cho heading
			hstHeading.Add("MonthYear",strMonthYear);
			
			return dataView;
		}
		public static DataView getDataViewC46(string strFromMonth, string strToMonth, string strCompany, long  intTransferExcess,
					long intTransferLack, long intFund, long intDelayFine, long intApplyAmount, string strPrintDate,ref Hashtable hstHeading, ref Hashtable hstEnding)
		{
			DataTable dataTable = clsCommon.GetDataTable("SI_sprptC46 @FromMonth=N'"+strFromMonth+"',@ToMonth=N'"+strToMonth+"',"
					+"@LSCompanyID=N'"+strCompany+"',@TransferExcess="+intTransferExcess+",@TransferLack="+intTransferLack+","
					+"@Fund="+intFund+",@DelayFine="+intDelayFine+",@ApplyAmount="+intApplyAmount+",@PrintDate=N'"+strPrintDate+"'");	
	
			DataView dataView = new DataView(dataTable);

			String strValue="";
			if(strFromMonth == strToMonth)
			{
				strValue = strFromMonth.Substring(0,2);
			}
			else
			{
				switch(strToMonth.Substring(0,2))
				{
					case "01":
					case "02":
					case "03": strValue = "1";break;
					case "04":
					case "05":
					case "06": strValue = "2";break;
					case "07":
					case "08":
					case "09": strValue = "3";break;
					case "10":
					case "11":
					case "12": strValue = "4";break;

				}
			}
			String periodName = "";
			if (strFromMonth.Equals(strToMonth))
			{
				periodName="Tháng";
			} 
			else
			{
				periodName="Quý";
			}
			
			//add tiep heading
			hstHeading.Add("PeriodName",periodName);
			hstHeading.Add("PeriodValue",strValue);
			hstHeading.Add("Year",strToMonth.Length!=0?strToMonth.Substring(3,4):"");

			
			//lay gia tri cho Ending (params nay duoc format dang so 000,000 tren Excel vi vay ko format tren code nua)
			hstEnding.Add("BST",dataTable.Rows.Count!=0?dataTable.Rows[0]["BST"]:"");
			hstEnding.Add("BSG",dataTable.Rows.Count!=0?dataTable.Rows[0]["BSG"]:"");
			hstEnding.Add("DelayFine",dataTable.Rows.Count!=0?dataTable.Rows[0]["DelayFine"]:"");
			hstEnding.Add("ApplyAmount",dataTable.Rows.Count!=0?dataTable.Rows[0]["ApplyAmount"]:"");
			hstEnding.Add("TransferExcess",intTransferExcess);
			hstEnding.Add("TransferLack",intTransferLack);
			hstEnding.Add("Fund",intFund);

			return dataView;
		}
		public static DataView getDataViewC47B(string strCompany, string strFromDate, string strToDate,string strLanguage,ref Hashtable hstHeading, ref Hashtable hstEnding)
		{
			DataTable dataTable = clsCommon.GetDataTable("SI_sprptC47b @LSCompanyID=N'"+strCompany+"',"
				+"@FromDate=N'"+strFromDate+"',@ToDate=N'"+strToDate+"',@Language=N'"+strLanguage+"'");
			DataView dataView = new DataView(dataTable);
			return dataView;

		}
		public static DataView getDataViewToKhai(string strFromDate, string strToDate, string strCompany, string Level1ID,
									string strLevel2ID, string strEmpID,ref Hashtable hstHeading, ref Hashtable hstEnding)
		{
			DataTable dataTable = clsCommon.GetDataTable("SI_sprptToKhaiCapSoBH @FromDate=N'"+strFromDate+"',"
				+"@ToDate=N'"+strToDate+"',@LSCompanyID=N'"+strCompany+"',@Level1ID=N'"+Level1ID+"',"
				+"@Level2ID=N'"+strLevel2ID+"',@EmpID=N'"+strEmpID+"'");
			DataView dataView = new DataView(dataTable);
			
			return dataView;
		}
		public static Hashtable getTitleReport(string strLanguage, string strCompany, string strGroup,
							string strLevel1ID, string strModuleID)
		{
			

			DataTable dataTable = clsCommon.GetDataTable("sp_rptTitle @LangID=N'"+strLanguage+"',@CompanyID=N'"+strCompany+"',"
						+"@GroupID='"+strGroup+"',@Level1ID='"+strLevel1ID+"',@ModuleID='"+strModuleID+"'");
			DataRow dataRow = dataTable.Rows[0];
			
			
			Hashtable hash = new Hashtable();

			hash.Add("UnitName",dataRow["UnitName"]);
			hash.Add("Address",dataRow["Address"]);
			hash.Add("Phone",dataRow["Phone"]);
			hash.Add("Fax",dataRow["Fax"]);
			hash.Add("UnitCode",dataRow["UnitCode"]);
			hash.Add("KDBCode",dataRow["KCBCode"]);
			hash.Add("AccountNo",dataRow["AccountNo"]);
			hash.Add("LSBankID",dataRow["LSBankID"]);
			hash.Add("ControlLevel",dataRow["ControlLevel"]);
			

			return hash;
			
		}


		


	}
}
