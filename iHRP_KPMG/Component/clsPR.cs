using System;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Web.Caching;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.OleDb; 
using iHRPCore.Com;
namespace iHRPCore.PRComponent
{
	/// <summary>
	/// Summary description for clsPR.
	/// </summary>
	public class clsPR
	{
		public clsPR()
		{
			//
			// TODO: Add constructor logic here
			//
		}
	}
	public class clsPROvertime
	{
		public static bool InvalidHours(string strDate,string strTimeFrom)
		{
			DataRow iRow = clsCommon.GetDataRow("PR_spfrmOT @Activity='CheckOTHours',@OTDate='" + strDate + "',@TimeFrom='" + strTimeFrom + "'");
			if(iRow[0].ToString().Trim() !="")
				return true;
			else
				return false;
		}
		//KIEM TRA THOI GIAN LAM OT TRUNG NHAU TRONG NGAY
		public static bool ExistsHours(Object strEmpID, Object strID, string strDate,string strTimeFrom,string strTimeTo)
		{
			DataRow iRow = clsCommon.GetDataRow("PR_spfrmOT @Activity='GetDataByDateAndHours',@OTDate='" + strDate +
				"',@OTID = '" + strID + "',@EmpID=N'" + strEmpID + "',@TimeFrom='" + strTimeFrom + "',@TimeTo='" + strTimeTo + "'");
			if(iRow !=null)
				return true;
			else
				return false;
		}
		//KIEM TRA SO GIO LAM OT VUOT GOI HAN <NGAY THUONG 4H: NGAY THU 7 LE 12H>
		public static string UnRegularHours(Object strEmpID, Object strID, string strDate,string strTimeOT)
		{
			DataRow iRow = clsCommon.GetDataRow("PR_spfrmOT @Activity='GetDataSumHours',@OTDate='" + strDate + "',@OTID = '" + strID + "',@EmpID=N'" + strEmpID + "',@Hours='" + strTimeOT + "'");
			if(iRow !=null)
				return iRow["ReturnValue"].ToString().Trim();
			else
				return "Input Error, check again!";
		}
		/// <summary>
		/// Lay thong tin Overtime cua nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataTable</returns>
		public static DataTable GetDataByEmpID(Object strEmpID)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("PR_spfrmOT @Activity='GetDataByEmpID',@EmpID = N'" + strEmpID + "'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}

		}
		/// <summary>
		/// Lay tham so tinh luong
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataTable</returns>
		public static DataTable GetAllDataParamByID()
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("PR_spfrmPARAMETER @Activity='GetDataAll'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}

		}
		public static DataTable GetAllDataParamMinSalary()
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("PR_spfrmPARAMETER_MINSALARY @Activity='GetDataAll'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}

		}
		public static DataRow GetDataByID_paramMinsalary(string strFromDate)
		{
			DataRow iRow = clsCommon.GetDataRow("PR_spfrmPARAMETER_MINSALARY @Activity='GetDataByID',@FromDate = '" + strFromDate + "'");
			if(iRow !=null)
				return iRow;
			else
				return null;
		}		
		/// <summary>
		/// Lay thong tin Overtime cua nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataRow</returns>
		public static DataRow GetDataByID_param(string strFromDate)
		{
			DataRow iRow = clsCommon.GetDataRow("PR_spfrmPARAMETER @Activity='GetDataByID',@FromDate = '" + strFromDate + "'");
			if(iRow !=null)
				return iRow;
			else
				return null;
		}		
		/// <summary>
		/// Lay thong tin Overtime cua nhan vien theo ngay
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataTable</returns>
		public static DataTable GetDataByDate(Object strEmpID,string strFromDate,string strToDate)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("PR_spfrmOT @Activity='GetDataByDate',@EmpID = N'" + strEmpID + "',@FromDate=N'" + strFromDate + "',@ToDate=N'" + strToDate + "'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}

		}
		/// <summary>
		/// Lay thong tin Overtime cua nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataRow</returns>
		public static DataRow GetDataByID(Object strID)
		{
			DataRow iRow = clsCommon.GetDataRow("PR_spfrmOT @Activity='GetDataByID',@OTID = " + strID);
			if(iRow !=null)
				return iRow;
			else
				return null;
		}		
		public static DataTable GetListSearch(string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strPosition, string strJobCode, string strLocation,string strStatus, string strOTType, string strFromDate, string strToDate,string strChargeBudget,string strMMYYY)
		{
			DataTable dt = new DataTable();
			try
			{
				string strSql = "PR_spfrmOT @Activity='GetDataListSearch',@EmpID=N'" + strEmpID + "', @EmpName=N'" + strEmpName
					+ "', @LSLevel1Code='" + strLevel1 + "', @LSLevel2Code='" + strLevel2 + "',@LSLevel3Code='" + strLevel3
					+ "',@LSPositionCode='" + strPosition + "',@LSJobCodeCode='" + strJobCode
					+ "',@LSLocationCode='" + strLocation + "',@FromDate=N'" + strFromDate.Trim() + "',@ToDate=N'" + strToDate + "',@OTTypeCode=N'" + strOTType + "',@ChargeBudgetCode=N'" + strChargeBudget + "',@PRMonth=N'" + strMMYYY + "'";
				if (strStatus != "")
					strSql = strSql + ", @Status='" + strStatus + "'";
				dt = clsCommon.GetDataTableHasID(strSql);
				return dt;
			}
			catch
			{
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
	}
	/// <summary>
	/// Class allowance	
	/// </summary>
	public class clsPRAllowance
	{		
		/// <summary>
		/// Lay thong tin Allowance cua nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataTable</returns>
		public static DataTable GetDataByEmpID(Object strEmpID,Object strLangID)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("PR_spfrmALLOWANCE @Activity='GetDataByEmpID',@EmpID = N'" + strEmpID + "',@languageID='" + strLangID + "'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}

		}
		/// <summary>
		/// GetData for Foreign
		/// </summary>		
		public static DataTable GetDataForeignByEmpID(Object strEmpID)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("PR_spfrmALLOWANCE @Activity='GetDataForeignByEmpID',@EmpID = N'" + strEmpID + "'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}

		}
		/// <summary>
		/// Lay thong tin Alowance cua nhan vien theo ngay
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataTable</returns>
		public static DataTable GetDataByDate(Object strEmpID,string strFromDate,string strToDate)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("PR_spfrmALLOWANCE @Activity='GetDataByDate',@EmpID = N'" + strEmpID + "',@FromDate=N'" + strFromDate + "',@ToDate=N'" + strToDate + "'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}

		}
		/// <summary>
		/// Lay thong tin Allowance cua nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataRow</returns>
		public static DataRow GetDataByID(Object strID)
		{
			DataRow iRow = clsCommon.GetDataRow("PR_spfrmALLOWANCE @Activity='GetDataByID',@AllowanceID = " + strID);
			if(iRow !=null)
				return iRow;
			else
				return null;
		}
		public static DataTable GetListSearch(string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strPosition, string strJobCode, string strLocation,string strCompany,string strStatus, string strAllowanceType, string strFromDate, string strToDate,string strLanguageID,System.Web.UI.Page pPage)
			
		{
			//cangtt - 01052006 - cập nhật phần phân quyền dữ liệu
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			//--------------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				//,@UserGroupID='" + sAccountLogin + "'
				string strSql = "PR_spfrmALLOWANCELIST @Activity='GetDataListSearch',@EmpID=N'" + strEmpID + "', @EmpName=N'" + strEmpName
					+ "', @LSLevel1ID=N'" + strLevel1 + "', @LSLevel2ID=N'" + strLevel2 + "',@LSLevel3ID=N'" + strLevel3
					+ "',@LSPositionID=N'" + strPosition + "',@LSLocationID=N'" + strLocation + "',@FromDate=N'" + strFromDate.Trim() + "',@ToDate=N'" + strToDate + "',@LSAllowanceID=N'" + strAllowanceType + "',@UserGroupID='" + sAccountLogin + "',@LanguageID='" + strLanguageID + "'";
				if (strStatus != "")
					strSql = strSql + ", @Status='" + strStatus + "'";
				dt = clsCommon.GetDataTableHasID(strSql);
				return dt;
			}
			catch
			{
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		///FOREIGN
		public static DataTable GetListSearchForeign(string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strPosition, string strJobCode, string strLocation,string strCompany,string strStatus, string strAllowanceType, string strFromDate, string strToDate)
		{
			DataTable dt = new DataTable();
			try
			{
				string strSql = "PR_spfrmALLOWANCE @Activity='GetDataListSearchForeign',@EmpID=N'" + strEmpID + "', @EmpName=N'" + strEmpName
					+ "', @LSLevel1Code='" + strLevel1 + "', @LSLevel2Code='" + strLevel2 + "',@LSLevel3Code='" + strLevel3
					+ "',@LSPositionCode='" + strPosition + "',@LSJobCodeCode='" + strJobCode
					+ "',@LSLocationCode='" + strLocation + "',@FromDate=N'" + strFromDate.Trim() + "',@ToDate=N'" + strToDate + "',@LSAllowanceCode=N'" + strAllowanceType + "'";
				if (strStatus != "")
					strSql = strSql + ", @Status='" + strStatus + "'";
				dt = clsCommon.GetDataTableHasID(strSql);
				return dt;
			}
			catch
			{
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
	}
	///
	///	ALLOWANCE FOREIGNER
	///
	public class clsPRAllowanceForeign
	{
		/// <summary>
		/// Lay thong tin Allowance cua nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataTable</returns>
		public static DataTable GetDataByEmpID(Object strEmpID)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("PR_spfrmALLOWANCE @Activity='GetDataByEmpID',@EmpID = N'" + strEmpID + "'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}

		}
		/// <summary>
		/// Lay thong tin Alowance cua nhan vien theo ngay
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataTable</returns>
		public static DataTable GetDataByDate(Object strEmpID,string strFromDate,string strToDate)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("PR_spfrmALLOWANCE @Activity='GetDataByDate',@EmpID = N'" + strEmpID + "',@FromDate=N'" + strFromDate + "',@ToDate=N'" + strToDate + "'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}

		}
		/// <summary>
		/// Lay thong tin Allowance cua nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataRow</returns>
		public static DataRow GetDataByID(Object strID)
		{
			DataRow iRow = clsCommon.GetDataRow("PR_spfrmALLOWANCE @Activity='GetDataByID',@AllowanceID = " + strID);
			if(iRow !=null)
				return iRow;
			else
				return null;
		}
		public static DataTable GetListSearch(string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strPosition, string strJobCode, string strLocation,string strCompany,string strStatus, string strAllowanceType, string strFromDate, string strToDate)
		{
			DataTable dt = new DataTable();
			try
			{
				string strSql = "PR_spfrmALLOWANCE @Activity='GetDataListSearch',@EmpID=N'" + strEmpID + "', @EmpName=N'" + strEmpName
					+ "', @LSLevel1Code='" + strLevel1 + "', @LSLevel2Code='" + strLevel2 + "',@LSLevel3Code='" + strLevel3
					+ "',@LSPositionCode='" + strPosition + "',@LSJobCodeCode='" + strJobCode
					+ "',@LSLocationCode='" + strLocation + "',@FromDate=N'" + strFromDate.Trim() + "',@ToDate=N'" + strToDate + "',@LSAllowanceCode=N'" + strAllowanceType + "'";
				if (strStatus != "")
					strSql = strSql + ", @Status='" + strStatus + "'";
				dt = clsCommon.GetDataTableHasID(strSql);
				return dt;
			}
			catch
			{
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
	}
	/// <summary>
	/// Class deduction
	/// </summary>
	public class clsPRDeduction
	{		
		/// <summary>
		/// Lay thong tin deduction cua nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataTable</returns>
		public static DataTable GetDataByEmpID(Object strEmpID,Object strLanguageID)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("PR_spfrmDEDUCTION @Activity='GetDataByEmpID',@EmpID = N'" + strEmpID + "',@languageID='" + strLanguageID + "'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}

		}
		/// <summary>
		/// Lay thong tin deduction cua nhan vien theo ngay
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataTable</returns>
		public static DataTable GetDataByDate(Object strEmpID,string strFromDate,string strToDate)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("PR_spfrmDEDUCTION @Activity='GetDataByDate',@EmpID = N'" + strEmpID + "',@FromDate=N'" + strFromDate + "',@ToDate=N'" + strToDate + "'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}

		}
		/// <summary>
		/// Lay thong tin deduction cua nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataRow</returns>
		public static DataRow GetDataByID(Object strID)
		{
			DataRow iRow = clsCommon.GetDataRow("PR_spfrmDEDUCTION @Activity='GetDataByID',@DeductionID = '" + strID + "'");
			if(iRow !=null)
				return iRow;
			else
				return null;
		}
		public static DataTable GetListSearch(string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strPosition, string strJobCode, string strLocation,string strCompany,string strStatus, string strDeductionType, string strFromDate, string strToDate,string strLanguageID, System.Web.UI.Page pPage)
		{
			//cangtt - 01052006 - cập nhật phần phân quyền dữ liệu
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			//--------------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				string strSql = "PR_spfrmDEDUCTION @Activity='GetDataListSearch',@EmpID=N'" + strEmpID + "', @EmpName=N'" + strEmpName
					+ "', @LSLevel1Code='" + strLevel1 + "', @LSLevel2Code='" + strLevel2 + "',@LSLevel3Code='" + strLevel3
					+ "',@LSPositionCode='" + strPosition 
					+ "',@LSLocationCode='" + strLocation + "',@FromDate=N'" + strFromDate.Trim() + "',@ToDate=N'" + strToDate + "',@LSDeductionID=N'" + strDeductionType + "',@UserGroupID='" + sAccountLogin + "',@LanguageID='" + strLanguageID + "'";
				if (strStatus != "")
					strSql = strSql + ", @Status='" + strStatus + "'";
				dt = clsCommon.GetDataTableHasID(strSql);
				return dt;
			}
			catch
			{
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
	}
	/// <summary>
	/// CLASS SALARY ADJUST
	/// </summary>
	public class clsPRSalaryAdjust
	{		
		/// <summary>
		/// Lay thong tin salary adjust cua nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataTable</returns>
		public static DataTable GetDataByEmpID(Object strEmpID,Object strLanguageID)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("PR_spfrmSALARYADJUST @Activity='GetDataByEmpID',@EmpID = N'" + strEmpID + "',@LanguageID='" + strLanguageID + "'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}

		}
		/// <summary>
		/// Lay thong tin salary adjust cua nhan vien theo ngay
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataTable</returns>
		public static DataTable GetDataByDate(Object strEmpID,string strFromDate,string strToDate)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("PR_spfrmSALARYADJUST @Activity='GetDataByDate',@EmpID = N'" + strEmpID + "',@FromDate=N'" + strFromDate + "',@ToDate=N'" + strToDate + "'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}

		}
		/// <summary>
		/// Lay thong tin salary adjust cua nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataRow</returns>
		public static DataRow GetDataByID(string strID)
		{
			DataRow iRow = clsCommon.GetDataRow("PR_spfrmSALARYADJUST @Activity='GetDataByID',@SalaryAdjustID = '" + strID +"'");
			if(iRow !=null)
				return iRow;
			else
				return null;
		}
		/// <summary>
		/// LOC THONG TIN ADJUST SALARY
		/// </summary>
		public static DataTable GetListSearch(string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strPosition, string strJobCode, string strLocation,string strCompany,string strStatus, string strSalaryAdjustType, string strType, string strPRMonth,string strLanguageID,System.Web.UI.Page pPage)
		{
			//cangtt - 01052006 - cập nhật phần phân quyền dữ liệu
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			//--------------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				string strSql = "PR_spfrmSALARYADJUST @Activity='GetDataListSearch',@EmpID=N'" + strEmpID + "', @EmpName=N'" + strEmpName
					+ "', @LSLevel1ID='" + strLevel1 + "', @LSLevel2ID='" + strLevel2 + "',@LSLevel3ID='" + strLevel3
					+ "',@LSPositionID='" + strPosition + "',@LSLocationID='" + strLocation + "',@Month=N'" + strPRMonth.Trim() + "',@Type=N'" + strType + "',@UserGroupID='" + sAccountLogin + "',@LSSalaryAdjustID=N'" + strSalaryAdjustType + "',@languageID='" + strLanguageID + "'";
				if (strStatus != "")
					strSql = strSql + ", @Status='" + strStatus + "'";
				dt = clsCommon.GetDataTableHasID(strSql);
				return dt;
			}
			catch
			{
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
	}
	#region Phan cua Salary Grade
	/// <summary>
	/// CLASS SALARY Grade
	/// Tanldt
	/// 15/09/2005
	/// </summary>
	public class clsPRSalaryGrade
	{		
		/// <summary>
		/// Lay thong tin salary Grade cua nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataTable</returns>
		public static DataTable GetDataByEmpID(Object strEmpID,Object strIs_CB)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("PR_spfrmSALARYGrade @Activity='GetDataByEmpID',@EmpID = N'" + strEmpID + "',@Is_CB = " + strIs_CB);
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}

		}
		/// <summary>
		/// Lay thong tin salary Grade cua nhan vien theo ngay
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataTable</returns>
		public static DataTable GetDataByDate(Object strEmpID,string strFromDate,string strToDate)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("PR_spfrmSALARYGrade @Activity='GetDataByDate',@EmpID = N'" + strEmpID + "',@FromDate=N'" + strFromDate + "',@ToDate=N'" + strToDate + "'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}

		}
		/// <summary>
		/// Lay thong tin salary Grade cua nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataRow</returns>
		public static DataRow GetDataByID(Object strID)
		{
			DataRow iRow = clsCommon.GetDataRow("PR_spfrmSALARYGrade @Activity='GetDataByID',@ID = " + strID);
			if(iRow !=null)
				return iRow;
			else
				return null;
		}
		/// <summary>
		/// LOC THONG TIN Grade SALARY
		/// </summary>
		public static DataTable GetListSearch(string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strPosition, string strJobCode, string strLocation,string strCompany,string strStatus, string strSalaryGradeType, string strType, string strPRMonth)
		{
			DataTable dt = new DataTable();
			try
			{
				string strSql = "PR_spfrmSALARYGrade @Activity='GetDataListSearch',@EmpID=N'" + strEmpID + "', @EmpName=N'" + strEmpName
					+ "', @LSLevel1Code='" + strLevel1 + "', @LSLevel2Code='" + strLevel2 + "',@LSLevel3Code='" + strLevel3
					+ "',@LSPositionCode='" + strPosition + "',@LSJobCodeCode='" + strJobCode
					+ "',@LSLocationCode='" + strLocation + "',@PRMonth=N'" + strPRMonth.Trim() + "',@Type=N'" + strType + "',@LSSalaryGradeCode=N'" + strSalaryGradeType + "'";
				if (strStatus != "")
					strSql = strSql + ", @Status='" + strStatus + "'";
				dt = clsCommon.GetDataTableHasID(strSql);
				return dt;
			}
			catch
			{
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		#region phan import huy bo
//		public static string ImportSalaryGrade(string pstrFilename)
//		{			
//			string mstr_FileName = pstrFilename;
//			if (!File.Exists(pstrFilename))
//			{
//				return "File not found, Please check path of the filename again!";
//			}			
//			string mstr_PathFileName = mstr_FileName;
//			//------------------
//
//			string strConn;
//			strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + mstr_PathFileName + ";Extended Properties=Excel 8.0;";			
//			OleDbDataAdapter myCommand = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", strConn);
//			DataSet myDataSet = new DataSet();
//			myCommand.Fill(myDataSet, "ExcelData");
//			
//			SqlTransaction sqlTran;
//			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
//			cnn.Open();
//			sqlTran = cnn.BeginTransaction();			
//			SqlCommand cmd = new SqlCommand();
//			cmd.Transaction = sqlTran;
//			cmd.Connection = cnn;
//			
//			cmd.CommandType = CommandType.StoredProcedure;
//			cmd.CommandText = "PR_spfrmSALARYGrade";
//			int iCom = myDataSet.Tables["ExcelData"].Columns.Count;
//			try
//			{
//				for(int i=0;i<myDataSet.Tables["ExcelData"].Rows.Count;i++)
//				{
//					cmd.Parameters.Clear();
//					cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 50).Value = "SaveImport";
//					cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar,12).Value = myDataSet.Tables["ExcelData"].Rows[i][0].ToString().Trim();
//					cmd.Parameters.Add("@CheckDate", SqlDbType.NVarChar,12).Value = myDataSet.Tables["ExcelData"].Rows[i][1].ToString().Trim();
//					cmd.Parameters.Add("@Problem", SqlDbType.Bit).Value = myDataSet.Tables["ExcelData"].Rows[i][2].ToString().Trim();
//					cmd.Parameters.Add("@Result", SqlDbType.NVarChar,200).Value = myDataSet.Tables["ExcelData"].Rows[i][3].ToString().Trim();
//					cmd.ExecuteNonQuery();
//				}
//				if (sqlTran != null ) sqlTran.Commit();
//				myCommand.Dispose();				
//				myDataSet.Dispose();
//				return "Successful!";
//			}
//			catch(Exception exp)
//			{
//				if (sqlTran != null) sqlTran.Rollback();
//				myCommand.Dispose();
//				myDataSet.Dispose();
//				return exp.Message;
//			}
//		}
		#endregion
	}
	#endregion
	public class clsPRMaternity
	{
		/// <summary>
		/// Lay thong tin Maternity
		/// </summary>
		/// <returns>DataTable</returns>
		public static DataTable GetDataMaternity()
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("PR_spfrmMATERNITY @Activity='GetDataAll'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}

		}
		/// <summary>
		/// Search 
		/// </summary>
		/// <returns></returns>
		public static DataTable GetDataMaternityList(string strMonth)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("PR_spfrmMATERNITY @Activity='GetDataLeaveRecordInMonth',@PRMonth=N'" + strMonth + "'");				
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}

		}
		/// <summary>
		/// Lay thong tin Maternity cua nhan vien theo ngay
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataTable</returns>
		public static DataTable GetDataByDate(Object strEmpID,string strFromDate,string strToDate)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("PR_spfrmMATERNITY @Activity='GetDataByDate',@EmpID = N'" + strEmpID + "',@FromDate=N'" + strFromDate + "',@ToDate=N'" + strToDate + "'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}

		}
		/// <summary>
		/// Lay thong tin Maternity cua nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataRow</returns>
		public static DataRow GetDataByID(Object strID)
		{
			DataRow iRow = clsCommon.GetDataRow("PR_spfrmMATERNITY @Activity='GetDataByID',@ID = " + strID);
			if(iRow !=null)
				return iRow;
			else
				return null;
		}
	}
	/// <summary>
	/// Class Bonus
	/// </summary>
	public class clsPRBonus
	{		
		/// <summary>
		/// Lay thong tin bonus cua nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataTable</returns>
		public static DataTable GetDataByEmpID(Object strEmpID, string strLanguage)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("PR_spfrmBONUS @Activity='GetDataByEmpID',@EmpID = N'" + strEmpID + "',@languageID='" + strLanguage + "'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}

		}
		/// <summary>
		/// Lay thong tin deduction cua nhan vien theo ngay
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataTable</returns>
		public static DataTable GetDataByDate(Object strEmpID,string strFromDate,string strToDate)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("PR_spfrmBONUS @Activity='GetDataByDate',@EmpID = N'" + strEmpID + "',@FromDate=N'" + strFromDate + "',@ToDate=N'" + strToDate + "'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}

		}
		/// <summary>
		/// Lay thong tin deduction cua nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataRow</returns>
		public static DataRow GetDataByID(Object strID)
		{
			DataRow iRow = clsCommon.GetDataRow("PR_spfrmBONUS @Activity='GetDataByID',@BonusID = " + strID);
			if(iRow !=null)
				return iRow;
			else
				return null;
		}
		public static DataTable GetListSearch(string strEmpID, string strEmpName,string strCompanyID ,string strLevel1, string strLevel2, 
			string strLevel3,string strStatus
			, string strFromDate, string strToDate,string strBonusID,string strMMYYY,string strLanguage,System.Web.UI.Page pPage)
		{
			//cangtt - 01052006 - cập nhật phần phân quyền dữ liệu
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			//--------------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				string strSql = "PR_spfrmBONUS @Activity='GetDataListSearch',@EmpID=N'" + strEmpID + "', @EmpName=N'" + strEmpName
					+ "',@LSCompanyID='" + strCompanyID + "', @LSLevel1ID='" + strLevel1 + "', @LSLevel2ID='" + strLevel2 + "',@LSLevel3ID='" + strLevel3
					+ "',@FromDate=N'" + strFromDate.Trim() + "',@ToDate=N'" + strToDate + "',@LSBonusID=N'" + strBonusID + "',@PRMonth=N'" + strMMYYY + "',@UserGroupID='" + sAccountLogin + "',@LanguageID='" + strLanguage + "'";
				if (strStatus != "")
					strSql = strSql + ", @Status='" + strStatus + "'";
				dt = clsCommon.GetDataTable(strSql);
				return dt;
			}
			catch
			{
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
	}
	/// <summary>
	/// CLASS VARIABLE BONUS
	/// </summary>
	public class clsPRVariableBonus
	{
		/// <summary>
		/// UPDATE CRITERIA BONUS
		/// </summary>
		public static string UpdateCriteria(string strYear,string strStaffLevel,string strMonths, DataGrid grdAppResult)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PR_spfrmVARIABLEBONUS";
			cmd.Parameters.Clear();
			try
			{
				for(int i=0;i<grdAppResult.Items.Count;i++)
				{
					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "UpdateCriteriaBonus";
					cmd.Parameters.Add("@Year",SqlDbType.Int).Value = strYear.Trim();
					cmd.Parameters.Add("@StaffLevelCode",SqlDbType.NVarChar,12).Value = strStaffLevel.Trim();					
					cmd.Parameters.Add("@Months",SqlDbType.Real).Value = strMonths.Trim()==""?"0":strMonths.Trim();
					cmd.Parameters.Add("@ResultLevelCode",SqlDbType.NVarChar,12).Value = grdAppResult.Items[i].Cells[0].Text.Trim();
					string strRate = ((TextBox)grdAppResult.Items[i].FindControl("txtRate")).Text.Trim();
					cmd.Parameters.Add("@Rate",SqlDbType.Real).Value = strRate==""?"0":strRate;
					cmd.ExecuteNonQuery();
				}
				sqlTran.Commit();
				return "";
			}
			catch (Exception exp)
			{				
				sqlTran.Rollback();
				return exp.Message;
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();
			}
		}
		/// <summary>
		/// UPDATE CRITERIA BONUS
		/// </summary>
		public static string SaveDataVariableBonus(string strYear,DataGrid grdAppResult)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PR_spfrmVARIABLEBONUS";
			cmd.Parameters.Clear();
			try
			{
				for(int i=0;i<grdAppResult.Items.Count;i++)
				{
					if(((CheckBox)grdAppResult.Items[i].FindControl("chkSelect")).Checked==true)
					{
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Save";
						cmd.Parameters.Add("@Year",SqlDbType.Int).Value = strYear.Trim();
						cmd.Parameters.Add("@EmpID",SqlDbType.NVarChar,12).Value = grdAppResult.Items[i].Cells[2].Text.Trim();
						cmd.Parameters.Add("@JobRelated",SqlDbType.NVarChar,255).Value = grdAppResult.Items[i].Cells[4].Text.Trim();
						cmd.Parameters.Add("@Rate",SqlDbType.Real).Value = grdAppResult.Items[i].Cells[5].Text.Trim().Replace(",","");
						cmd.Parameters.Add("@Months",SqlDbType.Real).Value = grdAppResult.Items[i].Cells[6].Text.Trim().Replace(",","");
						cmd.Parameters.Add("@Amount",SqlDbType.Real).Value = grdAppResult.Items[i].Cells[7].Text.Trim().Replace(",","");
						cmd.ExecuteNonQuery();
					}
				}
				sqlTran.Commit();
				return "";
			}
			catch (Exception exp)
			{				
				sqlTran.Rollback();
				return exp.Message;
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();
			}
		}
		/// <summary>
		/// lAY THONG TIN TI LE THEO KET QUA DANH GIA
		/// </summary>
		/// <param name="strYear">NAM</param>
		/// <returns>DATATABLE</returns>
		public static DataTable GetDataAppByYear(Object strYear, string strLevelType)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("PR_spfrmVARIABLEBONUS @Activity='GetBonusRating',@Year = " + strYear + ",@StaffLevelCode=N'" + strLevelType.Trim() + "'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}

		}
		/// <summary>
		/// LAY THONG TIN THEO TUNG STAFF LEVEL
		/// </summary>
		/// <param name="strYear">NAM</param>
		/// <returns>DATATABLE</returns>
		public static DataTable GetDataStaffLevelByYear(Object strYear)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("PR_spfrmVARIABLEBONUS @Activity='GetStaffLevel',@Year = " + strYear);
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}

		}
		/// <summary>
		/// Lay thong tin bonus cua nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataTable</returns>
		public static DataTable GetDataByEmpID(Object strEmpID)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("PR_spfrmVARIABLEBONUS @Activity='GetDataByEmpID',@EmpID = N'" + strEmpID + "'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}

		}
		/// <summary>
		/// Lay thong tin deduction cua nhan vien theo ngay
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataTable</returns>
		public static DataTable GetDataByDate(Object strEmpID,string strFromDate,string strToDate)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("PR_spfrmVARIABLEBONUS @Activity='GetDataByDate',@EmpID = N'" + strEmpID + "',@FromDate=N'" + strFromDate + "',@ToDate=N'" + strToDate + "'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}

		}
		//TINH THUONG THEO CHI TIEU
		public static DataTable CalculateBonus(string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strPosition, string strJobCode, string strLocation,string strCompany,string strStatus, string strYear)
		{
			DataTable dt = new DataTable();
			try
			{
				string strSql = "PR_spfrmVARIABLEBONUS @Activity='CalculateBonus',@EmpID=N'" + strEmpID + "', @EmpName=N'" + strEmpName
					+ "', @LSLevel1Code='" + strLevel1 + "', @LSLevel2Code='" + strLevel2 + "',@LSLevel3Code='" + strLevel3
					+ "',@LSPositionCode='" + strPosition + "',@LSJobCodeCode='" + strJobCode
					+ "',@LSLocationCode='" + strLocation + "',@Year=" + strYear.Trim();
				if (strStatus != "")
					strSql = strSql + ", @Status='" + strStatus + "'";
				dt = clsCommon.GetDataTableHasID(strSql);
				return dt;
			}
			catch
			{
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		//TIM KIEM THONG TIN THUONG
		public static DataTable SearchDataBonus(string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strPosition, string strJobCode, string strLocation,string strCompany,string strStatus, string strYear)
		{
			DataTable dt = new DataTable();
			try
			{
				string strSql = "PR_spfrmVARIABLEBONUS @Activity='SearchBonusData',@EmpID=N'" + strEmpID + "', @EmpName=N'" + strEmpName
					+ "', @LSLevel1Code='" + strLevel1 + "', @LSLevel2Code='" + strLevel2 + "',@LSLevel3Code='" + strLevel3
					+ "',@LSPositionCode='" + strPosition + "',@LSJobCodeCode='" + strJobCode
					+ "',@LSLocationCode='" + strLocation + "',@Year=" + strYear.Trim();
				if (strStatus != "")
					strSql = strSql + ", @Status='" + strStatus + "'";
				dt = clsCommon.GetDataTableHasID(strSql);
				return dt;
			}
			catch
			{
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		/// <summary>
		/// Lay thong tin deduction cua nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataRow</returns>
		public static DataRow GetDataByID(Object strID)
		{
			DataRow iRow = clsCommon.GetDataRow("PR_spfrmVARIABLEBONUS @Activity='GetDataByID',@BonusID = " + strID);
			if(iRow !=null)
				return iRow;
			else
				return null;
		}
		public static DataTable GetListSearch(string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strPosition, string strJobCode, string strLocation,string strStatus
			, string strFromDate, string strToDate,string strBonusCode,string strMMYYY)
		{
			DataTable dt = new DataTable();
			try
			{
				string strSql = "PR_spfrmBONUS @Activity='GetDataListSearch',@EmpID=N'" + strEmpID + "', @EmpName=N'" + strEmpName
					+ "', @LSLevel1Code='" + strLevel1 + "', @LSLevel2Code='" + strLevel2 + "',@LSLevel3Code='" + strLevel3
					+ "',@LSPositionCode='" + strPosition + "',@LSJobCodeCode='" + strJobCode
					+ "',@LSLocationCode='" + strLocation + "',@FromDate=N'" + strFromDate.Trim() + "',@ToDate=N'" + strToDate + "',@LSBonusCode=N'" + strBonusCode + "',@PRMonth=N'" + strMMYYY + "'";
				if (strStatus != "")
					strSql = strSql + ", @Status='" + strStatus + "'";
				dt = clsCommon.GetDataTableHasID(strSql);
				return dt;
			}
			catch
			{
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
	}
	public class clsLeaveConvert
	{
		public static DataTable GetConvert(string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strPosition, string strJobCode, string strLocation, string strCompany, string strYear, int intStatus,string strRefMonth,string strPRMonth,string strMethodID,string strNote)
		{
			string strSql = "PR_spfrmLEAVECONVERT @Activity='GetDataAll',@EmpID=N'" + strEmpID + "', @EmpName=N'" + strEmpName
				+ "', @LSLevel1Code='" + strLevel1 + "', @LSLevel2Code='" + strLevel2 + "',@LSLevel3Code='" + strLevel3
				+ "',@LSPositionCode='" + strPosition + "',@LSJobCodeCode='" + strJobCode 
				+ "',@LSLocationCode='" + strLocation + "', @LSCompanyCode='" + strCompany + "',@Year='" + strYear + "',@Status='" + intStatus 
				+ "',@RefMonth='" + strRefMonth + "',@PRMonth='" + strPRMonth + "',@MethodID=N'" + strMethodID + "',@Note=N'=" + strNote + "'";
			DataTable dtb = clsCommon.GetDataTable(strSql);
			return dtb;
		}
		//XEM LAI THONG TIN CHUYEN PHEP
		public static DataTable SearchLeaveConvert(string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strPosition, string strJobCode, string strLocation, string strCompany, string strYear,string strMethodID)
		{
			string strSql = "PR_spfrmLEAVECONVERT @Activity='GetDataListSearch',@EmpID=N'" + strEmpID + "', @EmpName=N'" + strEmpName
				+ "', @LSLevel1Code='" + strLevel1 + "', @LSLevel2Code='" + strLevel2 + "',@LSLevel3Code='" + strLevel3
				+ "',@LSPositionCode='" + strPosition + "',@LSJobCodeCode='" + strJobCode 
				+ "',@LSLocationCode='" + strLocation + "', @LSCompanyCode='" + strCompany + "',@Year='" + strYear + "',@MethodID=N'" + strMethodID + "'";
			DataTable dtb = clsCommon.GetDataTable(strSql);
			return dtb;
		}
		//SAVE LEAVE CONVERT
		public static string SaveLeaveconvert(DataGrid grdLeaveConvert)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PR_spfrmLEAVECONVERT";
			cmd.Parameters.Clear();
			try
			{
				for(int i=0;i<grdLeaveConvert.Items.Count;i++)
				{
					if(((CheckBox)grdLeaveConvert.Items[i].FindControl("chkSelect")).Checked==true)
					{
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Save";
						cmd.Parameters.Add("@Year",SqlDbType.NVarChar,4).Value = grdLeaveConvert.Items[i].Cells[2].Text.Trim();
						cmd.Parameters.Add("@EmpID",SqlDbType.NVarChar,12).Value = grdLeaveConvert.Items[i].Cells[3].Text.Trim();
						cmd.Parameters.Add("@MethodID",SqlDbType.NVarChar,12).Value = grdLeaveConvert.Items[i].Cells[9].Text.Trim();
						cmd.Parameters.Add("@Days",SqlDbType.Real).Value = grdLeaveConvert.Items[i].Cells[7].Text.Trim();						
						cmd.Parameters.Add("@RefMonth",SqlDbType.NVarChar,7).Value = grdLeaveConvert.Items[i].Cells[6].Text.Trim();
						cmd.Parameters.Add("@PRMonth",SqlDbType.NVarChar,7).Value = grdLeaveConvert.Items[i].Cells[11].Text.Trim();
						cmd.Parameters.Add("@Amount",SqlDbType.Money).Value = grdLeaveConvert.Items[i].Cells[8].Text.Trim().Replace(",","");
						cmd.ExecuteNonQuery();
					}
				}
				sqlTran.Commit();
				return "";
			}
			catch (Exception exp)
			{				
				sqlTran.Rollback();
				return exp.Message;
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();
			}
		}

	}
	/// <summary>
	/// CLASS LOAN
	/// </summary>
	public class clsPRLoan
	{	
		public static string GetAmounAllowByEmpID(Object strEmpID)
		{
			DataRow iRow;
			try
			{
				iRow = clsCommon.GetDataRow("PR_spfrmLOAN @Activity='GetAmountAllowing',@EmpID = N'" + strEmpID + "'");
//				if(iRow["Result"].ToString().Trim() =="")
//				{
//					return iRow["Amount"].ToString();
//				}
//				else
//				{
//					return "Error";
//				}
				if(iRow["Emptype"].ToString().Trim() =="TX47")
				{
					return iRow["Emptype"].ToString();
				}
				else
				{
					return "Error";
				}
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return "";
			}
		}
		/// <summary>
		/// Lay thong tin VAY TIEN cua nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataTable</returns>
		public static DataTable GetDataByEmpID(Object strEmpID)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("PR_spfrmLOAN @Activity='GetDataByEmpID',@EmpID = N'" + strEmpID + "'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}

		}
		/// <summary>
		/// QUA TRINH TRA TIEN VAY
		/// </summary>
		/// <param name="strLoanID"> LAN VAY</param>
		/// <returns>DATATABLE</returns>
		public static DataTable GetDataByLoanID(Object strLoanID)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("PR_spfrmLOANDETAIL @Activity='GetDataByLoanID',@LoanID='" + strLoanID+"'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}

		}		
		/// <summary>
		/// Lay CHI TIET thong tin vay tien cua nhan vien
		/// </summary>
		/// <param name="strLoanID">Ma so vay</param>
		/// <returns>DataRow</returns>
		public static DataRow GetDataByID(Object strLoanID)
		{
			DataRow iRow = clsCommon.GetDataRow("PR_spfrmLOAN @Activity='GetDataByLoanID', @LoanID='"+strLoanID+"'");
			if(iRow !=null)
				return iRow;
			else
				return null;
		}
		//
		public static DataRow GetDataRemainedByID(Object strID)
		{
			DataRow iRow = clsCommon.GetDataRow("PR_spfrmLOAN @Activity='GetRemainedAmount',@LoanID = '" + strID + "'");
			if(iRow !=null)
				return iRow;
			else
				return null;
		}
		/// <summary>
		/// Lay thong tin VAY TIEN CHI TIET (LOAN) cua nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataRow</returns>
		public static DataRow GetDataByLoanDetailID(Object strLoanDetailID)
		{
			DataRow iRow = clsCommon.GetDataRow("PR_spfrmLoanDetail @Activity='GetDataByLoanDetailID',@LoanDetailID = " + strLoanDetailID);
			if(iRow !=null)
				return iRow;
			else
				return null;
		}
		public static DataTable GetListSearch(string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strCompany, string strStatus, string strLoanPurposeID, string strFromDate, string strToDate,System.Web.UI.Page pPage)
		{
			//cangtt - 01052006 - cập nhật phần phân quyền dữ liệu
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			//--------------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				string strSql = "PR_spfrmLoan @Activity='SearchData',@EmpID=N'" + strEmpID + "', @EmpName=N'" + strEmpName
					+ "', @LSLevel1ID='" + strLevel1 + "', @LSLevel2ID='" + strLevel2 + "',@LSLevel3ID='" + strLevel3
					+ "',@LSCompanyID='" + strCompany + "',@FromMonth=N'" + strFromDate.Trim() + "',@ToMonth=N'" + strToDate + "',@LSLoanPurposeID=N'" + strLoanPurposeID + "'";
				if (strStatus != "")
					strSql = strSql + ", @Status='" + strStatus + "'";
				dt = clsCommon.GetDataTableHasID(strSql);
				return dt;
			}
			catch
			{
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
	}
	//PHAN HE TINH LUONG
	public class clsPayroll
	{
		//AUDIT TRAIL
		public static DataTable GetChangeData(string strFromDate,string strToDate)
		{
			DataTable dt = new DataTable();
			try
			{
				string strSql = "MGT_frmAuditTrail @FromDate='" + strFromDate.Trim() + "',@ToDate=N'" + strToDate.Trim() + "'";
				dt = clsCommon.GetDataTableHasID(strSql);
				return dt;
			}
			catch
			{
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		//ALLOWANCE CHECKER
		public static DataTable GetDataNotApprove(string strFromDate,string strToDate)
		{
			DataTable dt = new DataTable();
			try
			{
				string strSql = "MGT_frmChecker @Activity = 'GetAllowanceDataNotApproved',@FromDate='" + strFromDate.Trim() + "',@ToDate=N'" + strToDate.Trim() + "'";
				dt = clsCommon.GetDataTableHasID(strSql);
				return dt;
			}
			catch
			{
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		//APPROVED ALLOWANCE
		public static string ApprovedAllowance(DataGrid grdData)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "MGT_frmChecker";			
			try
			{				
				for(int i=0;i<grdData.Items.Count;i++)				
				{
					if(((CheckBox)grdData.Items[i].FindControl("chkSelect")).Checked==true)
					{						
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "ApprovedAllowance";
						cmd.Parameters.Add("@ItemID",SqlDbType.Int,4).Value = grdData.Items[i].Cells[0].Text.Trim();
						cmd.ExecuteNonQuery();
					}					
				}
				sqlTran.Commit();
				return "";
			}
			catch (Exception exp)
			{
				sqlTran.Rollback();
				return exp.Message;
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();				
			}
		}
		//VIEW BANG LUONG THANG
		public static DataTable GetPayRollSchedule(string strPRMonth,string strEmpID,string strLevel1, string strLevel2
			,string strLevel3,string strCompany,string strEmpName, string strPosition, string strJobCode, string strLocation,string strStatus,string strEmpType,string strCurType)
		{		
			DataTable dt = new DataTable();
			try
			{
				string strSql="PR_spfrmGetIncomeSchedule ";
				switch (strEmpType.Trim())
				{
					case "0":
						strSql += " @Activity = 'GetDataNV',";
						break;
					case "1":
						strSql += " @Activity = 'GetDataQL',";
						break;
					case "2":
						strSql += " @Activity = 'GetDataRef',";
						break;
					case "3":
						strSql += " @Activity ='GetDataFR',";
						break;
				}
				strSql += " @EmpID=N'" + strEmpID + "', @EmpName=N'" + strEmpName
					+ "', @LSLevel1Code='" + strLevel1 + "', @LSLevel2Code='" + strLevel2 + "',@LSLevel3Code='" + strLevel3					
					+ "',@SalPeriod=N'" + strPRMonth.Trim() + "',@LSCompanyCode=N'" + strCompany
					+ "',@LSPositionCode=N'" + strPosition + "',@LSJobCodeCode=N'" + strJobCode	+ "',@LSLocationCode=N'" + strLocation + "',@CurrencyType=N'" + strCurType + "'";
				if (strStatus != "")
					strSql = strSql + ", @Status='" + strStatus + "'";
				dt = clsCommon.GetDataTableHasID(strSql);
				return dt;
			}
			catch
			{
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		//DANH SACH LUONG THAI SAN
		public static DataTable GetPayRollMeternity(string strPRMonth,string strEmpID,string strLevel1, string strLevel2
			,string strLevel3,string strCompany,string strEmpName, string strPosition, string strJobCode, string strLocation,string strStatus)
		{
			DataTable dt = new DataTable();
			try
			{
				string strSql = "PR_spfrmGetSalaryIncome @Activity='GetPayRollMaternity',@EmpID=N'" + strEmpID + "', @EmpName=N'" + strEmpName
					+ "', @LSLevel1Code='" + strLevel1 + "', @LSLevel2Code='" + strLevel2 + "',@LSLevel3Code='" + strLevel3					
					+ "',@SalPeriod=N'" + strPRMonth.Trim() + "',@LSCompanyCode=N'" + strCompany
					+ "',@LSPositionCode=N'" + strPosition + "',@LSJobCodeCode=N'" + strJobCode	+ "',@LSLocationCode=N'" + strLocation + "'";
				if (strStatus != "")
					strSql = strSql + ", @Status='" + strStatus + "'";
				dt = clsCommon.GetDataTableHasID(strSql);
				return dt;
			}
			catch
			{
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		//DANH SACH LUONG THANG 13
		public static DataTable GetPayRoll13th(string strPRMonth,string strEmpID,string strLevel1, string strLevel2
			,string strLevel3,string strCompany,string strEmpName, string strPosition, string strJobCode, string strLocation,string strStatus,string strEmpType,string strCurType)
		{
			DataTable dt = new DataTable();
			try
			{
				string strSql="PR_spfrmGetIncome13th ";
				switch (strEmpType.Trim())
				{
					case "0":
						strSql += " @Activity = 'GetDataNV',";
						break;
					case "1":
						strSql += " @Activity = 'GetDataQL',";
						break;
					case "2":
						strSql += " @Activity = 'GetDataRef',";
						break;
					case "3":
						strSql += " @Activity ='GetDataFR', @CurrencyType=N'" + strCurType + "',";
						break;
				}
				strSql += "@EmpID=N'" + strEmpID + "', @EmpName=N'" + strEmpName
					+ "', @LSLevel1Code='" + strLevel1 + "', @LSLevel2Code='" + strLevel2 + "',@LSLevel3Code='" + strLevel3					
					+ "',@SalPeriod=N'" + strPRMonth.Trim() + "',@LSCompanyCode=N'" + strCompany
					+ "',@LSPositionCode=N'" + strPosition + "',@LSJobCodeCode=N'" + strJobCode	+ "',@LSLocationCode=N'" + strLocation + "'";
				if (strStatus != "")
					strSql = strSql + ", @Status='" + strStatus + "'";
				dt = clsCommon.GetDataTableHasID(strSql);
				return dt;
			}
			catch
			{
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		//DANH SACH LUONG THANG 14
		public static DataTable GetPayRoll14th(string strPRMonth,string strEmpID,string strLevel1, string strLevel2
			,string strLevel3,string strCompany,string strEmpName, string strPosition, string strJobCode, string strLocation,string strStatus,string strEmpType,string strCurType)
		{
			DataTable dt = new DataTable();
			try
			{
				string strSql="PR_spfrmGetIncome14th ";
				switch (strEmpType.Trim())
				{
					case "0":
						strSql += " @Activity = 'GetDataNV',";
						break;
					case "1":
						strSql += " @Activity = 'GetDataQL',";
						break;
					case "2":
						strSql += " @Activity = 'GetDataRef',";
						break;
					case "3":
						strSql += " @Activity ='GetDataFR', @CurrencyType=N'" + strCurType + "',";
						break;
				}
				strSql += "@EmpID=N'" + strEmpID + "', @EmpName=N'" + strEmpName
					+ "', @LSLevel1Code='" + strLevel1 + "', @LSLevel2Code='" + strLevel2 + "',@LSLevel3Code='" + strLevel3					
					+ "',@SalPeriod=N'" + strPRMonth.Trim() + "',@LSCompanyCode=N'" + strCompany
					+ "',@LSPositionCode=N'" + strPosition + "',@LSJobCodeCode=N'" + strJobCode	+ "',@LSLocationCode=N'" + strLocation + "'";
				if (strStatus != "")
					strSql = strSql + ", @Status='" + strStatus + "'";
				dt = clsCommon.GetDataTableHasID(strSql);
				return dt;				
			}
			catch
			{
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		//THONG TIN PAY SLIP
		public static DataTable GetPaySlip(string strPRMonth,string strEmpID)
		{
			DataTable dt = new DataTable();
			try
			{
				string strSql = "PR_spfrmGetPaySlip @Activity='GetPaySlip',@EmpID=N'" + strEmpID + "',@SalPeriod=N'01/" + strPRMonth.Trim() + "'";
				dt = clsCommon.GetDataTableHasID(strSql);
				return dt;
			}
			catch
			{
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		//TINH LUONG THANG
		public static string CalculateIncome(string strPRMonth,string strEmpID,string strLevel1, string strLevel2
			,string strLevel3,string strCompany,string strEmpName, string strPosition, string strJobCode, string strLocation,string strStatus,string strEmpType,string strCurType)
		{
			DataTable dt = new DataTable();
			try
			{
				string strSql="";
				switch (strEmpType.Trim())
				{
					case "0":
						strSql = "PR_spfrmCalculateIncomeNV ";
						break;
					case "1":
						strSql = "PR_spfrmCalculateIncomeQL ";
						break;
					case "2":
						strSql = "PR_spfrmCalculateIncomeRef ";
						break;
					case "3":
						strSql = "PR_spfrmCalculateIncomeFR @CurrencyType=N'" + strCurType + "',";
						break;
				}
				strSql = strSql + " @EmpID=N'" + strEmpID + "', @EmpName=N'" + strEmpName
					+ "', @LSLevel1Code='" + strLevel1 + "', @LSLevel2Code='" + strLevel2 + "',@LSLevel3Code='" + strLevel3					
					+ "',@SalPeriod=N'" + strPRMonth.Trim() + "',@LSCompanyCode=N'" + strCompany
					+ "',@LSPositionCode=N'" + strPosition + "',@LSJobCodeCode=N'" + strJobCode	+ "',@LSLocationCode=N'" + strLocation + "'";					
				if (strStatus != "")
					strSql = strSql + ", @Status='" + strStatus + "'";
				dt = clsCommon.GetDataTableHasID(strSql);
				return "";
			}
			catch(Exception ex)
			{
				return ex.Message;
			}
			finally
			{
				dt.Dispose();
			}			
		}
		// TINH LUONG THAI SAN
		public static string CalculateMeternity(string strPRMonth,string strEmpID,string strLevel1, string strLevel2
			,string strLevel3,string strCompany,string strEmpName, string strPosition, string strJobCode, string strLocation,string strStatus)
		{
			DataTable dt = new DataTable();
			try
			{
				string strSql = "PR_spfrmCalculateMaternityPayroll @EmpID=N'" + strEmpID + "', @EmpName=N'" + strEmpName
					+ "', @LSLevel1Code='" + strLevel1 + "', @LSLevel2Code='" + strLevel2 + "',@LSLevel3Code='" + strLevel3					
					+ "',@SalPeriod=N'" + strPRMonth.Trim() + "',@LSCompanyCode=N'" + strCompany
					+ "',@LSPositionCode=N'" + strPosition + "',@LSJobCodeCode=N'" + strJobCode	+ "',@LSLocationCode=N'" + strLocation + "'";
				if (strStatus != "")
					strSql = strSql + ", @Status='" + strStatus + "'";
				
				dt = clsCommon.GetDataTableHasID(strSql);
				return "";
			}
			catch(Exception ex)
			{
				return ex.Message;
			}
			finally
			{
				dt.Dispose();
			}
		}
		//TINH LUONG THANG 13
		public static string CalculateInCome13th(string strPRMonth,string strEmpID,string strLevel1, string strLevel2
			,string strLevel3,string strCompany,string strEmpName, string strPosition, string strJobCode, string strLocation,string strStatus,string strEmpType,string strCurType)
		{
			DataTable dt = new DataTable();
			try
			{
				string strSql="";
				switch (strEmpType.Trim())
				{
					case "0":
						strSql = "PR_spfrmCalculateIncome13thNV ";
						break;
					case "1":
						strSql = "PR_spfrmCalculateIncome13thQL ";
						break;
					case "2":
						strSql = "PR_spfrmCalculateIncome13thRef ";
						break;
					case "3":
						strSql = "PR_spfrmCalculateIncome13thFR @CurrencyType=N'" + strCurType + "',";
						break;
				}
				strSql += " @EmpID=N'" + strEmpID + "', @EmpName=N'" + strEmpName
					+ "', @LSLevel1Code='" + strLevel1 + "', @LSLevel2Code='" + strLevel2 + "',@LSLevel3Code='" + strLevel3					
					+ "',@SalPeriod=N'" + strPRMonth.Trim() + "',@LSCompanyCode=N'" + strCompany
					+ "',@LSPositionCode=N'" + strPosition + "',@LSJobCodeCode=N'" + strJobCode	+ "',@LSLocationCode=N'" + strLocation + "'";
				if (strStatus != "")
					strSql = strSql + ", @Status='" + strStatus + "'";
				dt = clsCommon.GetDataTableHasID(strSql);
				return "";
			}
			catch(Exception ex)
			{
				return ex.Message;
			}
			finally
			{
				dt.Dispose();
			}
		}
		//TINH LUONG THANG 14
		public static string CalculateInCome14th(string strPRMonth,string strEmpID,string strLevel1, string strLevel2
			,string strLevel3,string strCompany,string strEmpName, string strPosition, string strJobCode, string strLocation,string strStatus,string strEmpType,string strCurType)
		{
			DataTable dt = new DataTable();
			try
			{
				string strSql="";
				switch (strEmpType.Trim())
				{
					case "0":
						strSql = "PR_spfrmCalculateIncome14thNV ";
						break;
					case "1":
						strSql = "PR_spfrmCalculateIncome14thQL ";
						break;
					case "2":
						strSql = "PR_spfrmCalculateIncome14thRef ";
						break;
					case "3":
						strSql = "PR_spfrmCalculateIncome14thFR @CurrencyType=N'" + strCurType + "',";
						break;
				}
				strSql += " @EmpID=N'" + strEmpID + "', @EmpName=N'" + strEmpName
					+ "', @LSLevel1Code='" + strLevel1 + "', @LSLevel2Code='" + strLevel2 + "',@LSLevel3Code='" + strLevel3					
					+ "',@SalPeriod=N'" + strPRMonth.Trim() + "',@LSCompanyCode=N'" + strCompany
					+ "',@LSPositionCode=N'" + strPosition + "',@LSJobCodeCode=N'" + strJobCode	+ "',@LSLocationCode=N'" + strLocation + "'";
				if (strStatus != "")
					strSql = strSql + ", @Status='" + strStatus + "'";
				dt = clsCommon.GetDataTableHasID(strSql);
				return "";
			}
			catch(Exception ex)
			{
				return ex.Message;
			}
			finally
			{
				dt.Dispose();
			}
		}
	}
	public class clsBoardingFeeList
	{
		public static DataTable GetListByMonth(string sMonth,string strlanguageID)
		{		
				return clsCommon.GetDataTable("PR_spfrmNONBOARDINGLIST @Activity='getDataByMonth',@PRMonth='" + sMonth + "',@LanguageID='" + strlanguageID + "'");			
		}
		public static DataRow CheckDataExist(string strEmpID,string strPRMonth)
		{
			return clsCommon.GetDataRow("PR_spfrmNONBOARDINGLIST @Activity='CheckDataExist',@EmpID='" + strEmpID + "',@PRMonth='" + strPRMonth + "'");
		}
	}
	public class clsSalaryRecord
	{
		public static DataTable GetDataByEmpID(object sEmpID,string sLanguageID)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("PR_spfrmSalary @Activity='GetDataByEmpID',@EmpID = N'" + sEmpID + "',@LanguageID='" + sLanguageID + "'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		public static bool CheckValidEffectiveDate(Object strID,string strEffDate,string strSalaryRecordID,string strActivity)
		{			
			if(strActivity=="AddNew")
			{
				DataRow iRow = clsCommon.GetDataRow("PR_spfrmSalary @Activity='CheckValidSave',@EmpID = N'" + strID + "',@FromDate='" + strEffDate + "'");
				if(iRow != null)
					return false; 
				else
					return true;
			}
			else
			{
				DataRow iRow = clsCommon.GetDataRow("PR_spfrmSalary @Activity='CheckValidUpdate',@EmpID = N'" + strID + "',@FromDate='" + strEffDate + "',@SalaryRecordID='" + strSalaryRecordID+"'");
				if(iRow != null)
					return false; 
				else
					return true;
			}
			return false;
		}
		public static DataRow GetDataByID(Object strID)
		{
			DataRow iRow = clsCommon.GetDataRow("PR_spfrmSalary @Activity='GetDataByID',@SalaryRecordID = '" + strID + "'");
			if(iRow !=null)
				return iRow;
			else
				return null;
		}
	}
	public class clsBSRecord
	{
		public static DataTable GetDataByEmpID(object sEmpID,string sLanguageID)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("PR_spfrmBasicSalary @Activity='GetDataByEmpID',@EmpID = N'" + sEmpID + "',@LanguageID='" + sLanguageID + "'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		public static bool CheckValidEffectiveDate(Object strID,string strEffDate,string strBasicSalaryID,string strActivity)
		{			
			if(strActivity=="AddNew")
			{
				DataRow iRow = clsCommon.GetDataRow("PR_spfrmBasicSalary @Activity='CheckValidSave',@EmpID = N'" + strID + "',@FromDate='" + strEffDate + "'");
				if(iRow != null)
					return false; 
				else
					return true;
			}
			else
			{
				DataRow iRow = clsCommon.GetDataRow("PR_spfrmBasicSalary @Activity='CheckValidUpdate',@EmpID = N'" + strID + "',@FromDate='" + strEffDate + "',@BasicSalaryID='" + strBasicSalaryID+"'");
				if(iRow != null)
					return false; 
				else
					return true;
			}
			return false;
		}
		public static DataRow GetDataByID(Object strID)
		{
			DataRow iRow = clsCommon.GetDataRow("PR_spfrmBasicSalary @Activity='GetDataByID',@BasicSalaryID = '" + strID + "'");
			if(iRow !=null)
				return iRow;
			else
				return null;
		}
	}
	public class clsPRCollection
	{
		public static DataRow LoadDatePR()
		{
			try
			{
				DataRow drData= clsCommon.GetDataRow("PR_spPayrollCollection 'LoadDatePR'");
				return drData;
			}
			catch(Exception exp)
			{
				return null;
			}
		}

		/// <summary>
		/// iHRP_HLHV: bang luong DirectHire
		/// </summary>
		/// <returns></returns>
		public static DataRow LoadDatePR1()
		{
			try
			{
				DataRow drData= clsCommon.GetDataRow("PR_spPayrollCollection1 'LoadDatePR'");
				return drData;
			}
			catch(Exception exp)
			{
				return null;
			}
		}
		//LoadCurMonth_Payback
		public static DataRow LoadDatePR1_Payback()
		{
			try
			{
				DataRow drData= clsCommon.GetDataRow("PR_spPayrollCollection1 'LoadCurMonth_Payback'");
				return drData;
			}
			catch(Exception exp)
			{
				return null;
			}
		}
		public static DataRow LoadDatePR2_Payback()
		{
			try
			{
				DataRow drData= clsCommon.GetDataRow("PR_spPayrollCollection2 'LoadCurMonth_Payback'");
				return drData;
			}
			catch(Exception exp)
			{
				return null;
			}
		}
		public static DataRow LoadDatePR3_Payback()
		{
			try
			{
				DataRow drData= clsCommon.GetDataRow("PR_spPayrollCollection3 'LoadCurMonth_Payback'");
				return drData;
			}
			catch(Exception exp)
			{
				return null;
			}
		}


		/// <summary>
		/// iHRP_HLHV: bang luong Expat
		/// </summary>
		/// <returns></returns>
		public static DataRow LoadDatePR2()
		{
			try
			{
				DataRow drData= clsCommon.GetDataRow("PR_spPayrollCollection2 'LoadDatePR'");
				return drData;
			}
			catch(Exception exp)
			{
				return null;
			}
		}
		/// <summary>
		/// iHRP_HLHV: bang luong Secondee
		/// </summary>
		/// <returns></returns>
		public static DataRow LoadDatePR3()
		{
			try
			{
				DataRow drData= clsCommon.GetDataRow("PR_spPayrollCollection3 'LoadDatePR'");
				return drData;
			}
			catch(Exception exp)
			{
				return null;
			}
		}


		public static string CalculateSalary(string sFromDate, string sToDate, string LSCompanyID, string LSLevel1ID, string LSLevel2ID, string LSLevel3ID, string sLanguageID)
		{
			string sReturnValue="";
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			SqlCommand cmd = new SqlCommand();
			cnn.Open();
			sqlTran = cnn.BeginTransaction();				
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PR_spPayrollCollection";
			cmd.CommandTimeout = 8000;
			try
			{			
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "CalculateSalary";
				cmd.Parameters.Add("@FromDate",SqlDbType.NVarChar,12).Value = sFromDate;
				cmd.Parameters.Add("@ToDate",SqlDbType.NVarChar,12).Value = sToDate;
				cmd.Parameters.Add("@LSCompanyID",SqlDbType.NVarChar,12).Value = LSCompanyID;
				cmd.Parameters.Add("@LSLevel1ID",SqlDbType.NVarChar,12).Value = LSLevel1ID;
				cmd.Parameters.Add("@LSLevel2ID",SqlDbType.NVarChar,12).Value = LSLevel2ID;					
				cmd.Parameters.Add("@LSLevel3ID",SqlDbType.NVarChar,12).Value = LSLevel3ID;
				cmd.Parameters.Add("@sError", SqlDbType.NVarChar,4000).Direction = ParameterDirection.InputOutput;
				cmd.ExecuteNonQuery();
				sReturnValue = cmd.Parameters["@sError"].Value.ToString().Trim();
				sqlTran.Commit();
				if (sReturnValue=="")
					return clsChangeLang.getStringAlert("PC_0004",sLanguageID);
				else
					return clsChangeLang.getStringAlert("PC_0003",sLanguageID);
			}
			catch (SqlException ex)
			{
				return clsChangeLang.getStringAlert("PC_0003",sLanguageID);
			}
			catch (Exception exp)
			{				
				sqlTran.Rollback();
				return exp.Message;
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();		
			}
		}
		public static string CalculateSalary_Payback1(string sMonth,string sFromDate, string sToDate, string LSCompanyID, string LSLevel1ID, string LSLevel2ID, string LSLevel3ID, string sLanguageID)
		{
			string sReturnValue="";
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			SqlCommand cmd = new SqlCommand();
			cnn.Open();
			sqlTran = cnn.BeginTransaction();				
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PR_spPayrollCollection1_Payback";
			cmd.CommandTimeout = 8000;
			try
			{			
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "CalculateSalary";
				//cmd.Parameters.Add("@Month",SqlDbType.NVarChar,12).Value = sMonth;
				cmd.Parameters.Add("@FromDate",SqlDbType.NVarChar,12).Value = sFromDate;
				cmd.Parameters.Add("@ToDate",SqlDbType.NVarChar,12).Value = sToDate;
				cmd.Parameters.Add("@LSCompanyID",SqlDbType.NVarChar,12).Value = LSCompanyID;
				cmd.Parameters.Add("@LSLevel1ID",SqlDbType.NVarChar,12).Value = LSLevel1ID;
				cmd.Parameters.Add("@LSLevel2ID",SqlDbType.NVarChar,12).Value = LSLevel2ID;					
				cmd.Parameters.Add("@LSLevel3ID",SqlDbType.NVarChar,12).Value = LSLevel3ID;
				cmd.Parameters.Add("@sError", SqlDbType.NVarChar,4000).Direction = ParameterDirection.InputOutput;
				cmd.ExecuteNonQuery();
				sReturnValue = cmd.Parameters["@sError"].Value.ToString().Trim();
				sqlTran.Commit();
				if (sReturnValue=="")
					return clsChangeLang.getStringAlert("PC_0004",sLanguageID);
				else
					return clsChangeLang.getStringAlert("PC_0003",sLanguageID);
			}
			catch (SqlException ex)
			{
				return clsChangeLang.getStringAlert("PC_0003",sLanguageID);
			}
			catch (Exception exp)
			{				
				sqlTran.Rollback();
				return exp.Message;
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();		
			}
		}
		//tinh payback cho Expa
		public static string CalculateSalary_Payback2(string sMonth,string sFromDate, string sToDate, string LSCompanyID, string LSLevel1ID, string LSLevel2ID, string LSLevel3ID, string sLanguageID)
		{
			string sReturnValue="";
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			SqlCommand cmd = new SqlCommand();
			cnn.Open();
			sqlTran = cnn.BeginTransaction();				
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PR_spPayrollCollection2_Payback";
			cmd.CommandTimeout = 8000;
			try
			{			
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "CalculateSalary";
				//cmd.Parameters.Add("@Month",SqlDbType.NVarChar,12).Value = sMonth;
				cmd.Parameters.Add("@FromDate",SqlDbType.NVarChar,12).Value = sFromDate;
				cmd.Parameters.Add("@ToDate",SqlDbType.NVarChar,12).Value = sToDate;
				cmd.Parameters.Add("@LSCompanyID",SqlDbType.NVarChar,12).Value = LSCompanyID;
				cmd.Parameters.Add("@LSLevel1ID",SqlDbType.NVarChar,12).Value = LSLevel1ID;
				cmd.Parameters.Add("@LSLevel2ID",SqlDbType.NVarChar,12).Value = LSLevel2ID;					
				cmd.Parameters.Add("@LSLevel3ID",SqlDbType.NVarChar,12).Value = LSLevel3ID;
				cmd.Parameters.Add("@sError", SqlDbType.NVarChar,4000).Direction = ParameterDirection.InputOutput;
				cmd.ExecuteNonQuery();
				sReturnValue = cmd.Parameters["@sError"].Value.ToString().Trim();
				sqlTran.Commit();
				if (sReturnValue=="")
					return clsChangeLang.getStringAlert("PC_0004",sLanguageID);
				else
					return clsChangeLang.getStringAlert("PC_0003",sLanguageID);
			}
			catch (SqlException ex)
			{
				return clsChangeLang.getStringAlert("PC_0003",sLanguageID);
			}
			catch (Exception exp)
			{				
				sqlTran.Rollback();
				return exp.Message;
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();		
			}
		}
		//tinh payback cho secondeee
		public static string CalculateSalary_Payback3(string sMonth,string sFromDate, string sToDate, string LSCompanyID, string LSLevel1ID, string LSLevel2ID, string LSLevel3ID, string sLanguageID)
		{
			string sReturnValue="";
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			SqlCommand cmd = new SqlCommand();
			cnn.Open();
			sqlTran = cnn.BeginTransaction();				
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PR_spPayrollCollection3_Payback";
			cmd.CommandTimeout = 8000;
			try
			{			
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "CalculateSalary";
				//cmd.Parameters.Add("@Month",SqlDbType.NVarChar,12).Value = sMonth;
				cmd.Parameters.Add("@FromDate",SqlDbType.NVarChar,12).Value = sFromDate;
				cmd.Parameters.Add("@ToDate",SqlDbType.NVarChar,12).Value = sToDate;
				cmd.Parameters.Add("@LSCompanyID",SqlDbType.NVarChar,12).Value = LSCompanyID;
				cmd.Parameters.Add("@LSLevel1ID",SqlDbType.NVarChar,12).Value = LSLevel1ID;
				cmd.Parameters.Add("@LSLevel2ID",SqlDbType.NVarChar,12).Value = LSLevel2ID;					
				cmd.Parameters.Add("@LSLevel3ID",SqlDbType.NVarChar,12).Value = LSLevel3ID;
				cmd.Parameters.Add("@sError", SqlDbType.NVarChar,4000).Direction = ParameterDirection.InputOutput;
				cmd.ExecuteNonQuery();
				sReturnValue = cmd.Parameters["@sError"].Value.ToString().Trim();
				sqlTran.Commit();
				if (sReturnValue=="")
					return clsChangeLang.getStringAlert("PC_0004",sLanguageID);
				else
					return clsChangeLang.getStringAlert("PC_0003",sLanguageID);
			}
			catch (SqlException ex)
			{
				return clsChangeLang.getStringAlert("PC_0003",sLanguageID);
			}
			catch (Exception exp)
			{				
				sqlTran.Rollback();
				return exp.Message;
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();		
			}
		}


		public static string CalculateSalary1(string sFromDate, string sToDate, string LSCompanyID, string LSLevel1ID, string LSLevel2ID, string LSLevel3ID, string sLanguageID)
		{
			string sReturnValue="";
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			SqlCommand cmd = new SqlCommand();
			cnn.Open();
			sqlTran = cnn.BeginTransaction();				
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PR_spPayrollCollection1";
			cmd.CommandTimeout = 8000;
			try
			{			
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "CalculateSalary";
				cmd.Parameters.Add("@FromDate",SqlDbType.NVarChar,12).Value = sFromDate;
				cmd.Parameters.Add("@ToDate",SqlDbType.NVarChar,12).Value = sToDate;
				cmd.Parameters.Add("@LSCompanyID",SqlDbType.NVarChar,12).Value = LSCompanyID;
				cmd.Parameters.Add("@LSLevel1ID",SqlDbType.NVarChar,12).Value = LSLevel1ID;
				cmd.Parameters.Add("@LSLevel2ID",SqlDbType.NVarChar,12).Value = LSLevel2ID;					
				cmd.Parameters.Add("@LSLevel3ID",SqlDbType.NVarChar,12).Value = LSLevel3ID;
				cmd.Parameters.Add("@sError", SqlDbType.NVarChar,4000).Direction = ParameterDirection.InputOutput;
				cmd.ExecuteNonQuery();
				sReturnValue = cmd.Parameters["@sError"].Value.ToString().Trim();
				sqlTran.Commit();
				if (sReturnValue=="")
					return clsChangeLang.getStringAlert("PC_0004",sLanguageID);
				else
					return clsChangeLang.getStringAlert("PC_0003",sLanguageID);
			}
			catch (SqlException ex)
			{
				return clsChangeLang.getStringAlert("PC_0003",sLanguageID);
			}
			catch (Exception exp)
			{				
				sqlTran.Rollback();
				return exp.Message;
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();		
			}
		}

		public static string CalculateSalary2(string sFromDate, string sToDate, string LSCompanyID, string LSLevel1ID, string LSLevel2ID, string LSLevel3ID, string sLanguageID)
		{
			string sReturnValue="";
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			SqlCommand cmd = new SqlCommand();
			cnn.Open();
			sqlTran = cnn.BeginTransaction();				
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PR_spPayrollCollection2";
			cmd.CommandTimeout = 8000;
			try
			{			
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "CalculateSalary";
				cmd.Parameters.Add("@FromDate",SqlDbType.NVarChar,12).Value = sFromDate;
				cmd.Parameters.Add("@ToDate",SqlDbType.NVarChar,12).Value = sToDate;
				cmd.Parameters.Add("@LSCompanyID",SqlDbType.NVarChar,12).Value = LSCompanyID;
				cmd.Parameters.Add("@LSLevel1ID",SqlDbType.NVarChar,12).Value = LSLevel1ID;
				cmd.Parameters.Add("@LSLevel2ID",SqlDbType.NVarChar,12).Value = LSLevel2ID;					
				cmd.Parameters.Add("@LSLevel3ID",SqlDbType.NVarChar,12).Value = LSLevel3ID;
				cmd.Parameters.Add("@sError", SqlDbType.NVarChar,4000).Direction = ParameterDirection.InputOutput;
				cmd.ExecuteNonQuery();
				sReturnValue = cmd.Parameters["@sError"].Value.ToString().Trim();
				sqlTran.Commit();
				if (sReturnValue=="")
					return clsChangeLang.getStringAlert("PC_0004",sLanguageID);
				else
					return clsChangeLang.getStringAlert("PC_0003",sLanguageID);
			}
			catch (SqlException ex)
			{
				return clsChangeLang.getStringAlert("PC_0003",sLanguageID);
			}
			catch (Exception exp)
			{				
				sqlTran.Rollback();
				return exp.Message;
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();		
			}
		}

		public static string CalculateSalary3(string sFromDate, string sToDate, string LSCompanyID, string LSLevel1ID, string LSLevel2ID, string LSLevel3ID, string sLanguageID)
		{
			string sReturnValue="";
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			SqlCommand cmd = new SqlCommand();
			cnn.Open();
			sqlTran = cnn.BeginTransaction();				
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PR_spPayrollCollection3";
			cmd.CommandTimeout = 8000;
			try
			{			
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "CalculateSalary";
				cmd.Parameters.Add("@FromDate",SqlDbType.NVarChar,12).Value = sFromDate;
				cmd.Parameters.Add("@ToDate",SqlDbType.NVarChar,12).Value = sToDate;
				cmd.Parameters.Add("@LSCompanyID",SqlDbType.NVarChar,12).Value = LSCompanyID;
				cmd.Parameters.Add("@LSLevel1ID",SqlDbType.NVarChar,12).Value = LSLevel1ID;
				cmd.Parameters.Add("@LSLevel2ID",SqlDbType.NVarChar,12).Value = LSLevel2ID;					
				cmd.Parameters.Add("@LSLevel3ID",SqlDbType.NVarChar,12).Value = LSLevel3ID;
				cmd.Parameters.Add("@sError", SqlDbType.NVarChar,4000).Direction = ParameterDirection.InputOutput;
				cmd.ExecuteNonQuery();
				sReturnValue = cmd.Parameters["@sError"].Value.ToString().Trim();
				sqlTran.Commit();
				if (sReturnValue=="")
					return clsChangeLang.getStringAlert("PC_0004",sLanguageID);
				else
					return clsChangeLang.getStringAlert("PC_0003",sLanguageID);
			}
			catch (SqlException ex)
			{
				return clsChangeLang.getStringAlert("PC_0003",sLanguageID);
			}
			catch (Exception exp)
			{				
				sqlTran.Rollback();
				return exp.Message;
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();		
			}
		}


		public static DataTable GetDataSalary(string sLSCompanyID, string sLSLevel1ID,string sLSLevel2ID, string sLSLevel3ID)
		{
			try
			{
				DataTable dtData= clsCommon.GetDataTable("PR_spPayrollCollection 'GetDataSalary',@LSCompanyID='" + sLSCompanyID + "',@LSLevel1ID='" + sLSLevel1ID + "',@LSLevel2ID='" + sLSLevel2ID + "',@LSLevel3ID='" + sLSLevel3ID + "'");
				return dtData;
			}
			catch (Exception ex)
			{
				return null;
			}			
		}

		public static DataTable GetDataSalary_HLHV1(string sLSCompanyID, string sLSLevel1ID,string sLSLevel2ID, string sLSLevel3ID, string sEmpCode, string sEmpName, string sMMYYYY)
		{
			try
			{
				DataTable dtData= clsCommon.GetDataTable("PR_spPayrollCollection1 'GetDataSalary',@LSCompanyID='" + sLSCompanyID + "',@LSLevel1ID='" + sLSLevel1ID + "',@LSLevel2ID='" + sLSLevel2ID + "',@LSLevel3ID='" + sLSLevel3ID + "',@EmpCode='" + sEmpCode + "',@EmpName='" + sEmpName + "'");
				return dtData;
			}
			catch (Exception ex)
			{
				return null;
			}			
		}
		public static DataTable GetDataSalary_Payback_HLHV1(string sLSCompanyID, string sLSLevel1ID,string sLSLevel2ID, string sLSLevel3ID, string sEmpCode, string sEmpName, string sMMYYYY)
		{
			try
			{
				DataTable dtData= clsCommon.GetDataTable("PR_spPayrollCollection1_Payback 'GetDataSalary',@LSCompanyID='" + sLSCompanyID + "',@LSLevel1ID='" + sLSLevel1ID + "',@LSLevel2ID='" + sLSLevel2ID + "',@LSLevel3ID='" + sLSLevel3ID + "',@EmpCode='" + sEmpCode + "',@EmpName='" + sEmpName + "'");
				return dtData;
			}
			catch (Exception ex)
			{
				return null;
			}	
		
		}
		public static DataTable GetDataSalary_Payback_HLHV2(string sLSCompanyID, string sLSLevel1ID,string sLSLevel2ID, string sLSLevel3ID, string sEmpCode, string sEmpName, string sMMYYYY)
		{
			try
			{
				DataTable dtData= clsCommon.GetDataTable("PR_spPayrollCollection2_Payback 'GetDataSalary',@LSCompanyID='" + sLSCompanyID + "',@LSLevel1ID='" + sLSLevel1ID + "',@LSLevel2ID='" + sLSLevel2ID + "',@LSLevel3ID='" + sLSLevel3ID + "',@EmpCode='" + sEmpCode + "',@EmpName='" + sEmpName + "'");
				return dtData;
			}
			catch (Exception ex)
			{
				return null;
			}	
		
		}
		//secondee
		public static DataTable GetDataSalary_Payback_HLHV3(string sLSCompanyID, string sLSLevel1ID,string sLSLevel2ID, string sLSLevel3ID, string sEmpCode, string sEmpName, string sMMYYYY)
		{
			try
			{
				DataTable dtData= clsCommon.GetDataTable("PR_spPayrollCollection3_Payback 'GetDataSalary',@LSCompanyID='" + sLSCompanyID + "',@LSLevel1ID='" + sLSLevel1ID + "',@LSLevel2ID='" + sLSLevel2ID + "',@LSLevel3ID='" + sLSLevel3ID + "',@EmpCode='" + sEmpCode + "',@EmpName='" + sEmpName + "'");
				return dtData;
			}
			catch (Exception ex)
			{
				return null;
			}	
		
		}

		public static DataTable GetDataSalary_HLHV2(string sLSCompanyID, string sLSLevel1ID,string sLSLevel2ID, string sLSLevel3ID, string sEmpCode, string sEmpName, string sMMYYYY)
		{
			try
			{
				DataTable dtData= clsCommon.GetDataTable("PR_spPayrollCollection2 'GetDataSalary',@LSCompanyID='" + sLSCompanyID + "',@LSLevel1ID='" + sLSLevel1ID + "',@LSLevel2ID='" + sLSLevel2ID + "',@LSLevel3ID='" + sLSLevel3ID + "',@EmpCode='" + sEmpCode + "',@EmpName='" + sEmpName + "'");
				return dtData;
			}
			catch (Exception ex)
			{
				return null;
			}			
		}

		public static DataTable GetDataSalary_HLHV3(string sLSCompanyID, string sLSLevel1ID,string sLSLevel2ID, string sLSLevel3ID, string sEmpCode, string sEmpName, string sMMYYYY)
		{
			try
			{
				DataTable dtData= clsCommon.GetDataTable("PR_spPayrollCollection3 'GetDataSalary',@LSCompanyID='" + sLSCompanyID + "',@LSLevel1ID='" + sLSLevel1ID + "',@LSLevel2ID='" + sLSLevel2ID + "',@LSLevel3ID='" + sLSLevel3ID + "',@EmpCode='" + sEmpCode + "',@EmpName='" + sEmpName + "'");
				return dtData;
			}
			catch (Exception ex)
			{
				return null;
			}			
		}

		/// <summary>
		/// Tanldt
		/// </summary>
		/// <param name="sLSCompanyID"></param>
		/// <param name="sLSLevel1ID"></param>
		/// <param name="sLSLevel2ID"></param>
		/// <param name="sLSLevel3ID"></param>
		/// <returns>Luong</returns>
		public static DataTable GetDataPayslip( string sEmpID,string sMonth, string sLangID, string sAccountID)
		{
			try
			{
				DataTable dtData= clsCommon.GetDataTable("PR_sprptPaySlip @EmpID = '"+sEmpID+"',@Language = '"+sLangID+"',@MMYYYY = '"+sMonth+"'");
				return dtData;
			}
			catch (Exception ex)
			{
				return null;
			}			
		}
		/// <summary>
		/// Tanldt
		/// </summary>
		/// <param name="sLSCompanyID"></param>
		/// <param name="sLSLevel1ID"></param>
		/// <param name="sLSLevel2ID"></param>
		/// <param name="sLSLevel3ID"></param>
		/// <returns>Luong</returns>
		public static DataTable GetDataListPayslip( string sEmpID,string sMonth, string sLangID, string sAccountID)
		{
			try
			{
				DataTable dtData= clsCommon.GetDataTable("[PR_sprptPaySlipListEmp] @ListEmpID = '"+sEmpID+"',@Language = '"+sLangID+"',@MMYYYY = '"+sMonth+"'");
				return dtData;
			}
			catch (Exception ex)
			{
				return null;
			}			
		}
		/// <summary>
		/// iHRP_HLHV bang luong DirectHire
		/// </summary>
		/// <param name="sLSCompanyID"></param>
		/// <param name="sLSLevel1ID"></param>
		/// <param name="sLSLevel2ID"></param>
		/// <param name="sLSLevel3ID"></param>
		/// <param name="sEmpCode"></param>
		/// <returns></returns>
		public static DataTable GetDataSalaryAll1(string sLSCompanyID, string sLSLevel1ID,string sLSLevel2ID, string sLSLevel3ID, string sEmpCode)
		{
			try
			{
				DataTable dtData= clsCommon.GetDataTable("PR_spPayrollCollection1 'GetDataSalaryAll',@LSCompanyID='" + sLSCompanyID + "',@LSLevel1ID='" + sLSLevel1ID + "',@LSLevel2ID='" + sLSLevel2ID + "',@LSLevel3ID='" + sLSLevel3ID + "',@EmpCode = '"+sEmpCode+"'");
				return dtData;
			}
			catch (Exception ex)
			{
				return null;
			}			
		}

		/// <summary>
		/// iHRP_HLHV bang luong Expat
		/// </summary>
		/// <param name="sLSCompanyID"></param>
		/// <param name="sLSLevel1ID"></param>
		/// <param name="sLSLevel2ID"></param>
		/// <param name="sLSLevel3ID"></param>
		/// <param name="sEmpCode"></param>
		/// <returns></returns>
		public static DataTable GetDataSalaryAll2(string sLSCompanyID, string sLSLevel1ID,string sLSLevel2ID, string sLSLevel3ID, string sEmpCode)
		{
			try
			{
				DataTable dtData= clsCommon.GetDataTable("PR_spPayrollCollection2 'GetDataSalaryAll',@LSCompanyID='" + sLSCompanyID + "',@LSLevel1ID='" + sLSLevel1ID + "',@LSLevel2ID='" + sLSLevel2ID + "',@LSLevel3ID='" + sLSLevel3ID + "',@EmpCode = '"+sEmpCode+"'");
				return dtData;
			}
			catch (Exception ex)
			{
				return null;
			}			
		}

		
		/// <summary>
		/// iHRP_HLHV bang luong Secondee
		/// </summary>
		/// <param name="sLSCompanyID"></param>
		/// <param name="sLSLevel1ID"></param>
		/// <param name="sLSLevel2ID"></param>
		/// <param name="sLSLevel3ID"></param>
		/// <param name="sEmpCode"></param>
		/// <returns></returns>
		public static DataTable GetDataSalaryAll3(string sLSCompanyID, string sLSLevel1ID,string sLSLevel2ID, string sLSLevel3ID, string sEmpCode)
		{
			try
			{
				DataTable dtData= clsCommon.GetDataTable("PR_spPayrollCollection3 'GetDataSalaryAll',@LSCompanyID='" + sLSCompanyID + "',@LSLevel1ID='" + sLSLevel1ID + "',@LSLevel2ID='" + sLSLevel2ID + "',@LSLevel3ID='" + sLSLevel3ID + "',@EmpCode = '"+sEmpCode+"'");
				return dtData;
			}
			catch (Exception ex)
			{
				return null;
			}			
		}

		public static string LockPR(string sLanguageID)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			SqlCommand cmd = new SqlCommand();
			cnn.Open();
			sqlTran = cnn.BeginTransaction();				
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PR_spPayrollCollection";
			try
			{			
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "LockPR";
				cmd.ExecuteNonQuery();
				sqlTran.Commit();
				return clsChangeLang.getStringAlert("PC_0001",sLanguageID);
			}
			catch (Exception exp)
			{				
				sqlTran.Rollback();
				return exp.Message;
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();		
			}
		}
//LockPR_Payback
		
		/// <summary>
		/// iHRP_HLHV: bang luong DirectHire
		/// </summary>
		/// <param name="sLanguageID"></param>
		/// <returns></returns>
		public static string LockPR1(string sLanguageID)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			SqlCommand cmd = new SqlCommand();
			cnn.Open();
			sqlTran = cnn.BeginTransaction();				
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PR_spPayrollCollection1";
			try
			{			
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "LockPR";
				cmd.ExecuteNonQuery();
				sqlTran.Commit();
				return clsChangeLang.getStringAlert("PC_0001",sLanguageID);
			}
			catch (Exception exp)
			{				
				sqlTran.Rollback();
				return exp.Message;
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();		
			}
		}

		/// <summary>
		/// iHRP_HLHV: bang luong Expat
		/// </summary>
		/// <param name="sLanguageID"></param>
		/// <returns></returns>
		public static string LockPR2(string sLanguageID)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			SqlCommand cmd = new SqlCommand();
			cnn.Open();
			sqlTran = cnn.BeginTransaction();				
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PR_spPayrollCollection2";
			try
			{			
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "LockPR";
				cmd.ExecuteNonQuery();
				sqlTran.Commit();
				return clsChangeLang.getStringAlert("PC_0001",sLanguageID);
			}
			catch (Exception exp)
			{				
				sqlTran.Rollback();
				return exp.Message;
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();		
			}
		}

		/// <summary>
		/// iHRP_HLHV: bang luong Secondee
		/// </summary>
		/// <param name="sLanguageID"></param>
		/// <returns></returns>
		public static string LockPR3(string sLanguageID)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			SqlCommand cmd = new SqlCommand();
			cnn.Open();
			sqlTran = cnn.BeginTransaction();				
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PR_spPayrollCollection3";
			try
			{			
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "LockPR";
				cmd.ExecuteNonQuery();
				sqlTran.Commit();
				return clsChangeLang.getStringAlert("PC_0001",sLanguageID);
			}
			catch (Exception exp)
			{				
				sqlTran.Rollback();
				return exp.Message;
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();		
			}
		}
		//lock table payback 1,2,3
		public static string LockPR1_Payback(string sLanguageID)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			SqlCommand cmd = new SqlCommand();
			cnn.Open();
			sqlTran = cnn.BeginTransaction();				
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PR_spPayrollCollection1";
			try
			{			
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "LockPR_Payback";
				cmd.ExecuteNonQuery();
				sqlTran.Commit();
				return clsChangeLang.getStringAlert("PC_0001",sLanguageID);
			}
			catch (Exception exp)
			{				
				sqlTran.Rollback();
				return exp.Message;
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();		
			}
		}

		public static string LockPR2_Payback(string sLanguageID)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			SqlCommand cmd = new SqlCommand();
			cnn.Open();
			sqlTran = cnn.BeginTransaction();				
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PR_spPayrollCollection2";
			try
			{			
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "LockPR_Payback";
				cmd.ExecuteNonQuery();
				sqlTran.Commit();
				return clsChangeLang.getStringAlert("PC_0001",sLanguageID);
			}
			catch (Exception exp)
			{				
				sqlTran.Rollback();
				return exp.Message;
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();		
			}
		}
		public static string LockPR3_Payback(string sLanguageID)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			SqlCommand cmd = new SqlCommand();
			cnn.Open();
			sqlTran = cnn.BeginTransaction();				
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PR_spPayrollCollection3";
			try
			{			
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "LockPR_Payback";
				cmd.ExecuteNonQuery();
				sqlTran.Commit();
				return clsChangeLang.getStringAlert("PC_0001",sLanguageID);
			}
			catch (Exception exp)
			{				
				sqlTran.Rollback();
				return exp.Message;
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();		
			}
		}







		public static string CheckIsPR(string sLanguage)
		{
			DataTable dtData = new DataTable();
			try
			{
				dtData= clsCommon.GetDataTable("PR_spPayrollCollection 'CheckIsPR'");
				if (dtData.Rows.Count<=0 )
					return clsChangeLang.getStringAlert("PC_0005",sLanguage);
				else 
					return "";
			}
			catch (Exception ex)
			{
				return clsChangeLang.getStringAlert("PC_0005",sLanguage);
			}
			finally
			{
				if (dtData != null)	dtData.Dispose();
			}
			
		}

		public static string CheckIsPR1(string sLanguage)
		{
			DataTable dtData = new DataTable();
			try
			{
				dtData= clsCommon.GetDataTable("PR_spPayrollCollection1 'CheckIsPR'");
				if (dtData.Rows.Count<=0 )
					return clsChangeLang.getStringAlert("PC_0005",sLanguage);
				else 
					return "";
			}
			catch (Exception ex)
			{
				return clsChangeLang.getStringAlert("PC_0005",sLanguage);
			}
			finally
			{
				if (dtData != null)	dtData.Dispose();
			}
			
		}
		//payback direct fire
		public static string CheckIsPR1_Payback(string sLanguage)
		{
			DataTable dtData = new DataTable();
			try
			{
				dtData= clsCommon.GetDataTable("PR_spPayrollCollection1_Payback 'CheckIsPR'");
				if (dtData.Rows.Count<=0 )
					return clsChangeLang.getStringAlert("PC_0005",sLanguage);
				else 
					return "";
			}
			catch (Exception ex)
			{
				return clsChangeLang.getStringAlert("PC_0005",sLanguage);
			}
			finally
			{
				if (dtData != null)	dtData.Dispose();
			}
			
		}
		//payback expat
		public static string CheckIsPR2_Payback(string sLanguage)
		{
			DataTable dtData = new DataTable();
			try
			{
				dtData= clsCommon.GetDataTable("PR_spPayrollCollection2_Payback 'CheckIsPR'");
				if (dtData.Rows.Count<=0 )
					return clsChangeLang.getStringAlert("PC_0005",sLanguage);
				else 
					return "";
			}
			catch (Exception ex)
			{
				return clsChangeLang.getStringAlert("PC_0005",sLanguage);
			}
			finally
			{
				if (dtData != null)	dtData.Dispose();
			}
			
		}
		//payback secondee
		public static string CheckIsPR3_Payback(string sLanguage)
		{
			DataTable dtData = new DataTable();
			try
			{
				dtData= clsCommon.GetDataTable("PR_spPayrollCollection3_Payback 'CheckIsPR'");
				if (dtData.Rows.Count<=0 )
					return clsChangeLang.getStringAlert("PC_0005",sLanguage);
				else 
					return "";
			}
			catch (Exception ex)
			{
				return clsChangeLang.getStringAlert("PC_0005",sLanguage);
			}
			finally
			{
				if (dtData != null)	dtData.Dispose();
			}
			
		}

		public static string CheckIsPR2(string sLanguage)
		{
			DataTable dtData = new DataTable();
			try
			{
				dtData= clsCommon.GetDataTable("PR_spPayrollCollection2 'CheckIsPR'");
				if (dtData.Rows.Count<=0 )
					return clsChangeLang.getStringAlert("PC_0005",sLanguage);
				else 
					return "";
			}
			catch (Exception ex)
			{
				return clsChangeLang.getStringAlert("PC_0005",sLanguage);
			}
			finally
			{
				if (dtData != null)	dtData.Dispose();
			}
			
		}
		public static string CheckIsPR3(string sLanguage)
		{
			DataTable dtData = new DataTable();
			try
			{
				dtData= clsCommon.GetDataTable("PR_spPayrollCollection3 'CheckIsPR'");
				if (dtData.Rows.Count<=0 )
					return clsChangeLang.getStringAlert("PC_0005",sLanguage);
				else 
					return "";
			}
			catch (Exception ex)
			{
				return clsChangeLang.getStringAlert("PC_0005",sLanguage);
			}
			finally
			{
				if (dtData != null)	dtData.Dispose();
			}
			
		}


		public static string CheckIsPRForCopy(string sLanguage,string strPeriod, string strMonth)
		{
			
			try
			{
				DataRow iRow = clsCommon.GetDataRow("PR_spfrmCreatePayroll @Activity='CheckExistingForCopy',@FromMonth='" + strMonth + "',@FromSalPeriod='" + strPeriod +"'");
				if (iRow["Result"].ToString().Trim() == "0" )
					return clsChangeLang.getStringAlert("PC_0015",sLanguage);
				else 
					return "";
			}
			catch (Exception ex)
			{
				return clsChangeLang.getStringAlert("PC_0015",sLanguage);
			}		
		}
	}



	public class clsPRCreatePayroll
	{
		public static DataRow LoadCurrentPeriod()
		{
			try
			{
				DataRow drData= clsCommon.GetDataRow("PR_spfrmCreatePayroll 'LoadCurrentPeriod'");
				return drData;
			}
			catch(Exception exp)
			{
				return null;
			}
		}
	}
	public class clsPRSalaryItem
	{
		public static bool CheckPermissionEdit(string strSalaryItemID)
		{
			try
			{
				DataRow drData= clsCommon.GetDataRow("LS_spfrmSalaryItem @Activity='GetDataByID',@LSSalaryItemID='" + strSalaryItemID + "'");
				if(drData != null)
				{
					if(drData["AllowEdit"].ToString().Trim().ToLower()=="true")
						return true;
					else
						return false;
				}
				else
					return false;
			}
			catch(Exception exp)
			{
				return false;
			}
		}
		public static bool CheckPermissionDelete(string strSalaryItemID)
		{
			try
			{
				DataRow drData= clsCommon.GetDataRow("LS_spfrmSalaryItem @Activity='GetDataByID',@LSSalaryItemID='" + strSalaryItemID + "'");
				if(drData != null)
				{
					if(drData["AllowDel"].ToString().Trim().ToLower()=="true")
						return true;
					else
						return false;
				}
				else
					return false;
			}
			catch(Exception exp)
			{
				return false;
			}
		}
	}

	public class clsPRPayrollAdditional
	{
		/*public static DataRow LoadDatePR()
		{
			try
			{
				DataRow drData= clsCommon.GetDataRow("PR_spPayrollCollection 'LoadDatePR'");
				return drData;
			}
			catch(Exception exp)
			{
				return null;
			}
		}
		*/
		public static string CalculatePayrollAdditional(string sEmpID,string sEmpName, string sMonth,string sYear, string iCoefAdditional,string sPayrollAdditionalType, string LSCompanyID, string LSLevel1ID, string LSLevel2ID, string LSLevel3ID, string sLanguageID)
		{
			string sReturnValue="";
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			SqlCommand cmd = new SqlCommand();
			cnn.Open();
			sqlTran = cnn.BeginTransaction();				
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PR_spfrmPayrollAdditional";
			cmd.CommandTimeout = 8000;
			try
			{			
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Save";
				cmd.Parameters.Add("@EmpID",SqlDbType.NVarChar,12).Value = sEmpID;
				cmd.Parameters.Add("@EmpName",SqlDbType.NVarChar,50).Value = sEmpName;
				cmd.Parameters.Add("@MM",SqlDbType.NVarChar,12).Value = sMonth;
				cmd.Parameters.Add("@YYYY",SqlDbType.NVarChar,12).Value = sYear;
				cmd.Parameters.Add("@CoefAdditional",SqlDbType.NVarChar,12).Value = iCoefAdditional;
				cmd.Parameters.Add("@PayrollAdditionalType",SqlDbType.NVarChar,12).Value = sPayrollAdditionalType;
				cmd.Parameters.Add("@LSCompanyID",SqlDbType.NVarChar,12).Value = LSCompanyID;
				cmd.Parameters.Add("@LSLevel1ID",SqlDbType.NVarChar,12).Value = LSLevel1ID;
				cmd.Parameters.Add("@LSLevel2ID",SqlDbType.NVarChar,12).Value = LSLevel2ID;					
				cmd.Parameters.Add("@LSLevel3ID",SqlDbType.NVarChar,12).Value = LSLevel3ID;
				cmd.Parameters.Add("@sError", SqlDbType.NVarChar,4000).Direction = ParameterDirection.InputOutput;
				cmd.ExecuteNonQuery();
				sReturnValue = cmd.Parameters["@sError"].Value.ToString().Trim();
				sqlTran.Commit();
				if (sReturnValue=="")
					return clsChangeLang.getStringAlert("PC_0004",sLanguageID);
				else
					return clsChangeLang.getStringAlert("PC_0003",sLanguageID);
			}
			catch (SqlException ex)
			{
				return clsChangeLang.getStringAlert("PC_0003",sLanguageID);
			}
			catch (Exception exp)
			{				
				sqlTran.Rollback();
				return exp.Message;
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();		
			}
		}
		
		public static DataTable GetDataSalary (string sEmpID,string sEmpName, string sMonth,string sYear, string iCoefAdditional,string sPayrollAdditionalType, string LSCompanyID, string LSLevel1ID, string LSLevel2ID, string LSLevel3ID)
			//(string sLSCompanyID, string sLSLevel1ID,string sLSLevel2ID, string sLSLevel3ID)
		{
			try
			{
				DataTable dtData= clsCommon.GetDataTable("PR_spfrmPayrollAdditional 'GetDataSalary',@EmpID=N'" + sEmpID + "',@EmpName=N'" + sEmpName + "',@MM='" + sMonth + "',@YYYY='" + sYear + "',@CoefAdditional='" + iCoefAdditional + "',@PayrollAdditionalType='" + sPayrollAdditionalType + "',@LSCompanyID='" + LSCompanyID + "',@LSLevel1ID='" + LSLevel1ID + "',@LSLevel2ID='" + LSLevel2ID + "',@LSLevel3ID='" + LSLevel3ID + "'");
				return dtData;
			}
			catch (Exception ex)
			{
				return null;
			}			
		}
	/*
		/// <summary>
		/// Tanldt
		/// </summary>
		/// <param name="sLSCompanyID"></param>
		/// <param name="sLSLevel1ID"></param>
		/// <param name="sLSLevel2ID"></param>
		/// <param name="sLSLevel3ID"></param>
		/// <returns>Luong</returns>
		public static DataTable GetDataSalaryAll(string sLSCompanyID, string sLSLevel1ID,string sLSLevel2ID, string sLSLevel3ID, string sEmpCode)
		{
			try
			{
				DataTable dtData= clsCommon.GetDataTable("PR_spPayrollCollection 'GetDataSalaryAll',@LSCompanyID='" + sLSCompanyID + "',@LSLevel1ID='" + sLSLevel1ID + "',@LSLevel2ID='" + sLSLevel2ID + "',@LSLevel3ID='" + sLSLevel3ID + "',@EmpCode = '"+sEmpCode+"'");
				return dtData;
			}
			catch (Exception ex)
			{
				return null;
			}			
		}
		public static string LockPR(string sLanguageID)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			SqlCommand cmd = new SqlCommand();
			cnn.Open();
			sqlTran = cnn.BeginTransaction();				
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PR_spPayrollCollection";
			try
			{			
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "LockPR";
				cmd.ExecuteNonQuery();
				sqlTran.Commit();
				return clsChangeLang.getStringAlert("PC_0001",sLanguageID);
			}
			catch (Exception exp)
			{				
				sqlTran.Rollback();
				return exp.Message;
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();		
			}
		}
		public static string CheckIsPR(string sLanguage)
		{
			DataTable dtData = new DataTable();
			try
			{
				dtData= clsCommon.GetDataTable("PR_spPayrollCollection 'CheckIsPR'");
				if (dtData.Rows.Count<=0 )
					return clsChangeLang.getStringAlert("PC_0005",sLanguage);
				else 
					return "";
			}
			catch (Exception ex)
			{
				return clsChangeLang.getStringAlert("PC_0005",sLanguage);
			}
			finally
			{
				if (dtData != null)	dtData.Dispose();
			}
			
		}
		public static string CheckIsPRForCopy(string sLanguage,string strPeriod, string strMonth)
		{
			
			try
			{
				DataRow iRow = clsCommon.GetDataRow("PR_spfrmCreatePayroll @Activity='CheckExistingForCopy',@FromMonth='" + strMonth + "',@FromSalPeriod='" + strPeriod +"'");
				if (iRow["Result"].ToString().Trim() == "0" )
					return clsChangeLang.getStringAlert("PC_0015",sLanguage);
				else 
					return "";
			}
			catch (Exception ex)
			{
				return clsChangeLang.getStringAlert("PC_0015",sLanguage);
			}		
		}
		*/
	}


	public class ClsPR_Report
	{
		#region Company Info
		public static DataTable GetData_ComInfo (string CompanyID, string strMonth, System.Web.UI.Page pPage,string strStage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("PR_sprptCommon @Activity = 'CompanyInfo', @Language='" + sLanguage +
					"',@LSCompanyID=N'" + CompanyID + 
					"',@Stage=N'" + strStage + 
					"',@MMYYYY=N'" + strMonth +
					"'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		#endregion
		#region GetClub1
		public static DataTable GetClub1 (string strCompany,string strLevel1,string strLevel2,string strLevel3,string strEmpID)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("PR_sprptCommon @Activity = 'GetClub1'" +
					",@LSCompanyID=N'" + strCompany + 
					"',@LSLevel1ID=N'" + strLevel1 + 
					"',@LSLevel2ID=N'" + strLevel2 + 
					"',@LSLevel3ID=N'" + strLevel3 + 
					"',@EmpID=N'" + strEmpID + 
					"'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		#endregion
		#region GetClub2
		public static DataTable GetClub2 (string strEmpID)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("PR_sprptCommon @Activity = 'GetClub2'" +
					",@EmpID=N'" + strEmpID + 
					"'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		#endregion
		#region GetClub3
		public static DataTable GetClub3 (string strCompany,string strLevel1,string strLevel2,string strLevel3,string strEmpID, string strEmpName, string strPostionID)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("PR_sprptCommon @Activity = 'GetClub3'" +
					",@LSCompanyID=N'" + strCompany + 
					"',@LSLevel1ID=N'" + strLevel1 + 
					"',@LSLevel2ID=N'" + strLevel2 + 
					"',@LSLevel3ID=N'" + strLevel3 + 
					"',@EmpID=N'" + strEmpID + 
					"',@EmpName=N'" + strEmpName + 
					"',@LSPosition=N'" + strPostionID + 
					"'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		#endregion
		#region PR_rpt02_ThuongQuy
		public static DataTable GetData_PR_rpt02_ThuongQuy (string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strMonth, string strStage,System.Web.UI.Page pPage, string VFirstName, string VLastName)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("PR_rpt06_ThuongQuy @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +					
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@VFirstName=N'" + VFirstName +
					"',@VLastname=N'" + VLastName +
					"',@Stage=" + strStage +
					",@Year=N'" + strMonth +
					"'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		#endregion



		#region PR_rptPCTrachNhiem
		public static DataTable GetData_PR_rptPCTrachNhiem (string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strMonth, System.Web.UI.Page pPage, string VFirstName, string VLastName)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("PR_rptPCTrachNhiem @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +					
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@VFirstName=N'" + VFirstName +
					"',@VLastname=N'" + VLastName +
					"',@MMYYYY=N'" + strMonth +					
					"',@UserGroupID=N'" + sAccountLogin +	
					"'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		#endregion
		#region PR_rptPCGiuaCa
		public static DataTable GetData_PR_rptPCGiuaCa (string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strMonth, System.Web.UI.Page pPage, string VFirstName, string VLastName)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("PR_rptPCGiuaCa @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +					
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@VFirstName=N'" + VFirstName +
					"',@VLastname=N'" + VLastName +
					"',@MMYYYY=N'" + strMonth +					
					"',@UserGroupID=N'" + sAccountLogin +	
					"'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		#endregion
		#region PR_rptPCKhoanChuyen
		public static DataTable GetData_PR_rptPCKhoanChuyen (string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strMonth, System.Web.UI.Page pPage, string VFirstName, string VLastName)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("PR_rptPCKhoanChuyen @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +					
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@VFirstName=N'" + VFirstName +
					"',@VLastname=N'" + VLastName +
					"',@MMYYYY=N'" + strMonth +					
					"',@UserGroupID=N'" + sAccountLogin +	
					"'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		#endregion




		#region PR_rpt12_TToanTHop
		public static DataTable GetData_PR_rpt12_TToanTHop (string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strMonth, System.Web.UI.Page pPage, string VFirstName, string VLastName)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("PR_sprptL02 @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +					
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@VFirstName=N'" + VFirstName +
					"',@VLastname=N'" + VLastName +
					"',@MMYYYY=N'" + strMonth +	
					"',@UserGroupID=N'" + sAccountLogin +	
					"'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		#endregion
		#region PR_rpt12_TToanTHop_PBan
		public static DataTable GetData_PR_rpt12_TToanTHop_b(string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strMonth, System.Web.UI.Page pPage, string VFirstName, string VLastName)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("PR_sprptL02_b @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +					
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@VFirstName=N'" + VFirstName +
					"',@VLastname=N'" + VLastName +
					"',@MMYYYY=N'" + strMonth +	
					"',@UserGroupID=N'" + sAccountLogin +	
					"'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		#endregion
		#region PR_rpt13_LDTLDonVi
		public static DataTable GetData_PR_rpt13_LDTLDonVi (string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strMonth, System.Web.UI.Page pPage, string VFirstName, string VLastName)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("PR_sprptL03 @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +					
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@VFirstName=N'" + VFirstName +
					"',@VLastname=N'" + VLastName +
					//"',@MMYYYY=N'" + strMonth +		
					"',@EffectiveDate=N'" + strMonth +		
					"',@UserGroupID=N'" + sAccountLogin +	
					"'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		#endregion
		#region PR_rptPCTrachNhiem
		public static DataTable GetData_PR_rptPCTrachNhiem(string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strDate, System.Web.UI.Page pPage, string VFirstName, string VLastName, string strType)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("PR_rptPCTrachNhiem @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +					
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@VFirstName=N'" + VFirstName +
					"',@VLastname=N'" + VLastName +
					//"',@MMYYYY=N'" + strMonth +					
					"',@EffectiveDate=N'" + strDate +					
					"',@Activity=N'" + strType +	
					"',@UserGroupID=N'" + sAccountLogin +	
					"'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		#endregion
		#region PR_rpt07_ThuongAmLich
		public static DataTable GetData_PR_rpt07_ThuongAmLich (string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strYear, System.Web.UI.Page pPage, string VFirstName, string VLastName)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("PR_rpt07_ThuongAmLich @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +					
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@VFirstName=N'" + VFirstName +
					"',@VLastname=N'" + VLastName +
					"',@Year=N'" + strYear +		
					"',@UserGroupID=N'" + sAccountLogin +	
					"'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		#endregion
		#region PR_rpt14_LuongCB_NV
		public static DataTable GetData_PR_rpt14_LuongCB_NV(string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strDate, System.Web.UI.Page pPage, string VFirstName, string VLastName)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("PR_rpt14_LuongCB_NV @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +					
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@VFirstName=N'" + VFirstName +
					"',@VLastname=N'" + VLastName +
					//"',@MMYYYY=N'" + strMonth +		
					"',@EffectiveDate=N'" + strDate +		
					"',@UserGroupID=N'" + sAccountLogin +	
					"'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		#endregion
		#region PR_rpt16_TangLuong
		public static DataTable PR_rpt16_TangLuong(string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strMonth, System.Web.UI.Page pPage, string VFirstName, string VLastName)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("PR_rpt16_TangLuong @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +					
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@VFirstName=N'" + VFirstName +
					"',@VLastname=N'" + VLastName +
					"',@MMYYYY=N'" + strMonth +		
					"',@UserGroupID=N'" + sAccountLogin +	
					"'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		#endregion
		#region PR_rpt16_TangLuong_DenHan
		public static DataTable PR_rpt16_TangLuong_DenHan(string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strEffectiveDate, string strNumMonths, System.Web.UI.Page pPage, string VFirstName, string VLastName)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("PR_rpt16_TangLuong_DenHan @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +					
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@VFirstName=N'" + VFirstName +
					"',@VLastname=N'" + VLastName +
					"',@EffectiveDate=N'" + strEffectiveDate +		
					"',@NumMonths=" + strNumMonths +		
					",@UserGroupID=N'" + sAccountLogin +	
					"'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		#endregion
		#region GetData_PR_rpt08_Thang13
		public static DataTable GetData_PR_rpt08_Thang13 (string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strYear, System.Web.UI.Page pPage, string VFirstName, string VLastName)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("PR_rpt08_Thang13 @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +					
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@VFirstName=N'" + VFirstName +
					"',@VLastname=N'" + VLastName +
					"',@Year=N'" + strYear +	
					"',@UserGroupID=N'" + sAccountLogin +	
					"'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		#endregion
		#region GetData_PR_rpt09_THThuong
		public static DataTable GetData_PR_rpt09_THThuong (string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strFromMonth, System.Web.UI.Page pPage, string VFirstName, string VLastName, string strToMonth)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("PR_rpt09_THThuong @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +					
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@VFirstName=N'" + VFirstName +
					"',@VLastname=N'" + VLastName +
					"',@FromMonth=N'" + strFromMonth +					
					"',@ToMonth=N'" + strToMonth +	
					"',@UserGroupID=N'" + sAccountLogin +	
					"'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		#endregion
		#region GetData_PR_rpt05_TamUngLuong
		public static DataTable GetData_PR_rpt05_TamUngLuong (string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strMonth, System.Web.UI.Page pPage, string VFirstName, string VLastName)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("PR_rpt05_TamUngLuong @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +					
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@VFirstName=N'" + VFirstName +
					"',@VLastname=N'" + VLastName +
					"',@MMYYYY=N'" + strMonth +		
					"',@UserGroupID=N'" + sAccountLogin +	
					"'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		#endregion
		#region GetData_PR_rpt03_ThuNhapDVThang
		public static DataTable GetData_PR_rpt03_ThuNhapDVThang (string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strFromMonth, System.Web.UI.Page pPage, string VFirstName, string VLastName, string strToMonth)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("PR_rpt03_ThuNhapDVThang @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +					
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@VFirstName=N'" + VFirstName +
					"',@VLastname=N'" + VLastName +
					"',@FromMonth=N'" + strFromMonth +					
					"',@ToMonth=N'" + strToMonth +		
					"',@UserGroupID=N'" + sAccountLogin +	
					"'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		#endregion
		#region GetData_PR_rpt03_ThuNhapNVThang
		public static DataTable GetData_PR_rpt03_ThuNhapNVThang (string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strFromMonth, System.Web.UI.Page pPage, string VFirstName, string VLastName, string strToMonth)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("PR_rpt03_ThuNhapNVThang @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +					
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@VFirstName=N'" + VFirstName +
					"',@VLastname=N'" + VLastName +
					"',@FromMonth=N'" + strFromMonth +					
					"',@ToMonth=N'" + strToMonth +	
					"',@UserGroupID=N'" + sAccountLogin +	
					"'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		#endregion
		#region GetData_PR_rpt_Thuong
		public static DataTable GetData_PR_rpt_Thuong(string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strYear, System.Web.UI.Page pPage, string VFirstName, string VLastName, string strLSBonusID)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("PR_rpt_Thuong @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +					
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@VFirstName=N'" + VFirstName +
					"',@VLastname=N'" + VLastName +
					"',@Year=N'" + strYear +	
					"',@LSBonusID=N'" + strLSBonusID +	
					"',@UserGroupID=N'" + sAccountLogin +	
					"'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		#endregion
		
		#region GetData_PR_rpt15_DSMuaCoPhan
		public static DataTable GetData_PR_rpt15_DSMuaCoPhan (string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strYear, System.Web.UI.Page pPage, string VFirstName, string VLastName)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("PR_rpt15_DSMuaCoPhan @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +					
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@VFirstName=N'" + VFirstName +
					"',@VLastname=N'" + VLastName +
					"',@Year=N'" + strYear +	
					"',@UserGroupID=N'" + sAccountLogin +	
					"'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		#endregion

		#region GetData_PR_rpt21_THThueNam
		public static DataTable GetData_PR_rpt21_THThueNam (string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strYear, System.Web.UI.Page pPage, string VFirstName, string VLastName)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("PR_sprptPITDetail_Excel @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +					
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@VFirstName=N'" + VFirstName +
					"',@VLastname=N'" + VLastName +
					"',@Year=N'" + strYear +		
					"',@UserGroupID=N'" + sAccountLogin +	
					"'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		#endregion
		#region GetData_PR_rpt22_CTThueTNNV
		public static DataTable GetData_PR_rpt22_CTThueTNNV (string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strMonth, System.Web.UI.Page pPage, string VFirstName, string VLastName)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("PR_rpt22_CTThueTNNV @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +					
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@VFirstName=N'" + VFirstName +
					"',@VLastname=N'" + VLastName +
					"',@MMYYYY=N'" + strMonth +		
					"',@UserGroupID=N'" + sAccountLogin +	
					"'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		#endregion
		#region GetData_PR_rpt23_ToKhaiNopThue
		public static DataTable GetData_PR_rpt23_ToKhaiNopThue (string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strMonth, System.Web.UI.Page pPage, string VFirstName, string VLastName)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("PR_rpt23_ToKhaiNopThue @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +					
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@VFirstName=N'" + VFirstName +
					"',@VLastname=N'" + VLastName +
					"',@MMYYYY=N'" + strMonth +		
					"',@UserGroupID=N'" + sAccountLogin +	
					"'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		#endregion
	}

}