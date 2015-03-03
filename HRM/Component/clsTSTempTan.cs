using System;
using System.Data;
using System.Web.UI.WebControls;
using iHRPCore.Com;
using System.Web;
using System.Web.Caching;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;

namespace iHRPCore.TSComponent
{
	/// <summary>
	/// Summary description for clsTSTempTan.
	/// </summary>
	public class clsTSEmpList
	{
		/// <summary>
		/// Lay thong tin khong hop le cua nhan viec
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataTable</returns>
		public static DataTable GetDataScanTimeToTime(string strFrom,string strTo,string CompanyID,string Level1ID,string Level2ID,string Level3ID,System.Web.UI.Page pPage)
		{
			DataTable dt = new DataTable();
			try
			{
				string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
				dt = clsCommon.GetDataTableTimeOut("TS_spfrmScanTime @Activity='GetInvalidcase',@FromDate='" + strFrom + "',@ToDate='" + strTo + 
					"',@CompanyID='" + CompanyID + 
					"',@Level1ID='" + Level1ID + 
					"',@Level2ID='" + Level2ID + "',@Level3ID='" + Level3ID + "',@UserGroupID='" + sAccountLogin + "'");
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
		/// Lay thong tin khong hop le cua nhan viec
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataTable</returns>
		public static DataTable GetDataScanTimeToTime_Save(string strFrom,string strTo,string CompanyID,string Level1ID,string Level2ID,string Level3ID)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("TS_spfrmScanTime @Activity='SaveInvalidcase',@FromDate='" + strFrom + "',@ToDate='" + strTo + "',@Level1ID='" + Level1ID + "',@Level2ID='" + Level2ID + "',@Level3ID='" + Level3ID + "'");
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
		/// Lay thong tin khong hop le cua nhan viec
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataTable</returns>
		public static DataTable GetDataScan_EditSTShort(string strFrom,string strTo,string CompanyID,string Level1ID,string Level2ID,string Level3ID,string EmpID,string EmpName, string Status,string StatusValid,string strLate,System.Web.UI.Page pPage)
		{
			DataTable dt = new DataTable();
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			try
			{
				dt = clsCommon.GetDataTable("TS_spfrmScanTime @Activity='GetAllData',@FromDate='" + strFrom +
					"',@ToDate='" + strTo + "',@CompanyID='" + CompanyID + "',@Level1ID='" + Level1ID +
					"',@Level2ID='" + Level2ID + "',@Level3ID='" + Level3ID + 
					"',@EmpID='" + EmpID + "',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +
					"',@StatusValid=N'" + StatusValid+
					"',@StatusLate=N'" + strLate+
					"',@UserGroupID='" + sAccountLogin + "'");
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
		/// Lay thong tin cac loai nghi phep
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataTable</returns>
		public static DataTable GetDataScan_LeaveRecord(string strEmpID)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("TS_spfrmLeaveRecord @Activity='LoadGrid',@EmpID='" + strEmpID +"'");
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
		/// Lay thong tin khong hop le cua nhan viec
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataTable</returns>
		public static DataTable GetDataScan_EditSTShort(string strFrom,string strTo,string CompanyID,
			string Level1ID,string Level2ID,string Level3ID,
			string EmpID,string EmpName,
			string Status,string StatusValid,
			string strCountTime,string  strOperatorST,string strScanNumber,
			string strOperatorRT,string  strRealTimes, System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("TS_spfrmScanTime @Activity='GetAllData',@FromDate='" + strFrom +
					"',@ToDate='" + strTo + 
					"',@CompanyID='" + CompanyID + 
					"',@Level1ID='" + Level1ID +
					"',@Level2ID='" + Level2ID + 
					"',@Level3ID='" + Level3ID + 
					"',@EmpID='" + EmpID + "',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +
					"',@CountTime=N'" + strCountTime +
					"',@OperatorST=N'" + strOperatorST +
					"',@ScanNumber=N'" + strScanNumber +
					"',@OperatorRT=N'" + strOperatorRT +
					"',@RealTimes=N'" + strRealTimes +
					"',@StatusValid=N'" + StatusValid+"',@UserGroupID='" + sAccountLogin + "'");
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
		/// Bao cao cham cong
		/// </summary>
		public static DataTable GetDataScan_Collect_Report(string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string strFirstName,string strLastName,string strPosition,string Status,string strMMYYYY,System.Web.UI.Page pPage)
		{
			//cangtt - 01052006 - thêm phân quy?n d? li?u
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				//string strIsCal = blIsCal.ToString().Replace("True","1").Replace("False","0");
				string strSQL = "TS_sprptChamCongThang" +
					" @LSCompanyID=N'" + CompanyID + 
					"',@LSLevel1ID=N'" + Level1ID +
					"',@LSLevel2ID=N'" + Level2ID + 
					"',@LSLevel3ID=N'" + Level3ID + 
					"',@EmpID=N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@FirstName=N'" + strFirstName + 
					"',@LastName=N'" + strLastName + 
					"',@LSPositionID=N'" + strPosition + 
					"',@Status=N'" + Status + 
					"',@MMYYYY=N'" + strMMYYYY + "'";

				SqlCommand cmd = new SqlCommand();
				string strTimeOut = ConfigurationSettings.AppSettings["pstrConnectionString"].Split(';')[1].Replace("Connect Timeout=","");
				SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"].Replace(strTimeOut,"36000"));
				//SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
				DataTable rsData = new DataTable();
				SqlDataAdapter adpAdapter = new SqlDataAdapter();
				try
				{
					SQLconn.Open();
					cmd.Connection = SQLconn;
					cmd.CommandType = CommandType.Text;
					cmd.CommandText = strSQL;
					cmd.CommandTimeout = 20000;
			
					adpAdapter.SelectCommand = cmd;				
					adpAdapter.Fill(rsData);

					cmd.Dispose();
					SQLconn.Close();
					SQLconn.Dispose();
					adpAdapter.Dispose();
					return rsData;
				}
				catch(Exception exp)
				{                
					cmd.Dispose();
					SQLconn.Close();
					SQLconn.Dispose();
					adpAdapter.Dispose();
					string strErr = exp.Message;
					return null;
				}
				//dt = clsCommon.GetDataTableHasID(strSQL);
				//"',@IsCal=" + strIsCal+ 
				//",@UserGroupID='" + sAccountLogin + "'");
				return rsData;
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
		/// Lay thong tin di tre - ve som (bao cao)
		/// </summary>
		/// <returns>DataTable</returns>
		public static DataTable GetData_DiTre_VeSom(string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string strFirstName,string strLastName,string strPosition,string Status,string strMMYYYY,System.Web.UI.Page pPage)
		{
			//cangtt - 01052006 - thêm phân quy?n d? li?u
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("TS_sprptDiTre_Vesom " +
					"@LSCompanyID=N'" + CompanyID + 
					"',@LSLevel1ID=N'" + Level1ID +
					"',@LSLevel2ID=N'" + Level2ID + 
					"',@LSLevel3ID=N'" + Level3ID + 
					"',@EmpID=N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@FirstName=N'" + strFirstName + 
					"',@LastName=N'" + strLastName + 
					"',@LSPositionID=N'" + strPosition + 
					"',@Status=N'" + Status +
					"',@MMYYYY=N'" + strMMYYYY +
					"',@UserGroupID='" + sAccountLogin + "'");
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
		/// Lay thong gan tong hop cong
		/// </summary>
		/// <returns>DataTable</returns>
		public static DataTable GetDataScan_Collect(string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status,string strMMYYYY, bool blIsCal,System.Web.UI.Page pPage)
		{
			//cangtt - 01052006 - thêm phân quy?n d? li?u
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				string strIsCal = blIsCal.ToString().Replace("True","1").Replace("False","0");
				dt = clsCommon.GetDataTableHasID("TS_spfrmCountTime @Activity='LoadDataCountTime" +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpIDSearch=N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +
					"',@MMYYYY=N'" + strMMYYYY +
					"',@IsCal=" + strIsCal+ ",@UserGroupID='" + sAccountLogin + "'");
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
		/// Lay thong gan tong hop cong
		/// </summary>
		/// <returns>DataTable</returns>
		public static DataTable GetDataScan_Collect2(string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status,string strMMYYYY, bool blIsCal,System.Web.UI.Page pPage)
		{
			//cangtt - 01052006 - thêm phân quy?n d? li?u
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				string strIsCal = blIsCal.ToString().Replace("True","1").Replace("False","0");
				dt = clsCommon.GetDataTableHasID("TS_spfrmCountTime @Activity='LoadDataCountTime2" +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpIDSearch=N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +
					"',@MMYYYY=N'" + strMMYYYY +
					"',@IsCal=" + strIsCal+ ",@UserGroupID='" + sAccountLogin + "'");
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
		/// Lay thong tin tu lich lam viec
		/// </summary>
		/// <returns>DataTable</returns>
		public static DataTable GetDataScan_TourOfLeave(string strScheduleCode,string strMonth,string strShift)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("TS_spfrmSchedule @Activity='Search',@ScheduleCode=N'" + strScheduleCode +
					"',@Month='" + strMonth +"',@LSShiftID=N'" + strShift + "'");
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
		/// Lay thong tin tu lich lam viec
		/// </summary>
		/// <returns>DataTable</returns>
		public static DataTable GetDataScan_WorkingSchedule(string strScheduleCode,string strWeek,string strShift)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("TS_spfrmSchedule @Activity='Search',@ScheduleCode=N'" + strScheduleCode.Replace("'","") +
					"',@Week='" + strWeek +"',@LSShiftID=N'" + strShift + "'");
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
		/// Lay thong tin tu lich lam viec
		/// </summary>
		/// <returns>DataTable</returns>
		public static DataTable GetData_AssignTourOfShift_Com(string strWeek,string strCompany,string strShift,System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("TS_spfrmShiftAssign @Activity='LoadByCompany',@Week='" + strWeek +
						"', @LSCompanyID=N'" + strCompany +
						"', @LSShiftID=N'" + strShift + "',@UserGroupID='" + sAccountLogin + "'");
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
		/// Lay thong tin tu lich lam viec
		/// </summary>
		/// <returns>DataTable</returns>
		public static DataTable GetData_AssignScheduleToComp(string strWeek,string strCompany,string strScheduleID)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("TS_spfrmScheduleAssign @Activity='LoadByCompany',@Week='" + strWeek +
					"', @LSCompanyID=N'" + strCompany +
					"', @ScheduleID='" + strScheduleID + "'");
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
		/// gan lich lam viec cho cong ty
		/// </summary>
		/// <returns>DataTable</returns>
		public static DataTable GetDataScan_AssignWorkingTour(string strYear,string strCompany)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("TS_spfrmScheduleAssign @Activity='LoadByCompany',@Year='" + strYear +
					"',@LSCompanyID=N'" + strCompany + "'");
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
		/// gan lich lam viec cho cong ty,Don vi, phong ban, to nhom
		/// </summary>
		/// <returns>DataTable</returns>
		public static DataTable GetDataScan_AssignWorkingTour_Organization(string strMonth,string strCompany,string strLevel1,string strLevel2,string strLevel3)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("TS_spfrmScheduleAssign @Activity='LoadByOrganization',@Month='" + strMonth +
					"',@LSCompanyID=N'" + strCompany +
					"',@LSLevel1ID=N'" + strLevel1 +
					"',@LSLevel2ID=N'" + strLevel2 +
					"',@LSLevel3ID=N'" + strLevel3 + "'");
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
		/// gan lich lam viec cho cong ty,Don vi, phong ban, to nhom
		/// </summary>
		/// <returns>DataTable</returns>
		public static DataTable GetDataScan_AssignWorkingTour_Emp(string strMonth,string strCompany,string strLevel1,string strLevel2,string strLevel3)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("TS_spfrmScheduleAssign @Activity='LoadByEmp',@Month='" + strMonth +
					"',@LSCompanyID=N'" + strCompany +
					"',@LSLevel1ID=N'" + strLevel1 +
					"',@LSLevel2ID=N'" + strLevel2 +
					"',@LSLevel3ID=N'" + strLevel3 + "'");
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
		/// gan lich lam viec cho cong ty,Don vi, phong ban, to nhom
		/// </summary>
		/// <returns>DataTable</returns>
		public static DataTable GetDataScan_AssignTourOfShift_Organization(string strWeek,string strCompany,string strLevel1,string strLevel2,string strLevel3)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("TS_spfrmShiftAssign @Activity='LoadByOrganization',@Week='" + strWeek +
					"',@LSCompanyID=N'" + strCompany +
					"',@LSLevel1ID=N'" + strLevel1 +
					"',@LSLevel2ID=N'" + strLevel2 +
					"',@LSLevel3ID=N'" + strLevel3 + "'");
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
		/// gan lich lam viec cho cong ty,Don vi, phong ban, to nhom
		/// </summary>
		/// <returns>DataTable</returns>
		public static DataTable GetDataScan_AssignScheduleToOrg(string strWeek, string strScheduleID,string strCompany,string strLevel1,string strLevel2,string strLevel3)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("TS_spfrmScheduleAssign @Activity='LoadByOrganization',@Week='" + strWeek +
					"',@ScheduleID=N'" + strScheduleID +
					"',@LSCompanyID=N'" + strCompany +
					"',@LSLevel1ID=N'" + strLevel1 +
					"',@LSLevel2ID=N'" + strLevel2 +
					"',@LSLevel3ID=N'" + strLevel3 + "'");
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
		/// gan lich lam viec cho cong ty,Don vi, phong ban, to nhom, EMp
		/// </summary>
		/// <returns>DataTable</returns>
		public static DataTable GetDataScan_AssignTourOfShift_Emp(string strWeek,string strCompany,string strLevel1,string strLevel2,string strLevel3
			, string strLSShiftID, string strEmpID, string strFullName)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("TS_spfrmShiftAssign @Activity='LoadByEmp',@Week='" + strWeek +
					"',@LSCompanyID=N'" + strCompany +
					"',@LSLevel1ID=N'" + strLevel1 +
					"',@LSLevel2ID=N'" + strLevel2 +
					"',@LSLevel3ID=N'" + strLevel3 + 
					"',@LSShiftID=N'" + strLSShiftID + 
					"',@EmpID=N'" + strEmpID + 
					"',@FullName=N'" + strFullName + 
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
		/// <summary>
		/// gan lich lam viec cho cong ty,Don vi, phong ban, to nhom, EMp
		/// </summary>
		/// <returns>DataTable</returns>
		public static DataTable GetDataScan_AssignScheduleToEmp(string strWeek, string strScheduleID,string strCompany,string strLevel1,string strLevel2,string strLevel3
			, string strLSShiftID, string strEmpID, string strFullName)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("TS_spfrmScheduleAssign @Activity='LoadByEmp',@Week='" + strWeek +
					"',@ScheduleID=N'" + strScheduleID +
					"',@LSCompanyID=N'" + strCompany +
					"',@LSLevel1ID=N'" + strLevel1 +
					"',@LSLevel2ID=N'" + strLevel2 +
					"',@LSLevel3ID=N'" + strLevel3 + 
					//"',@LSShiftID='" + strLSShiftID + 
					//"',@EmpID='" + strEmpID + 
					//"',@FullName='" + strFullName + 
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
		public static DataRow AssignWorkingTour_Organization_GetDataByID(Object strID)
		{
			DataRow iRow = clsCommon.GetDataRow("TS_spfrmScheduleAssign @Activity='GetDataByID',@ScheduleAssignID = " + strID);
			if(iRow !=null)
				return iRow;
			else
				return null;
		}
//		public static DataRow TourOfLeaveDetails_GetDataByID(Object strID)
//		{
//			DataRow iRow = clsCommon.GetDataRow("TS_spfrmSchedule @Activity='Search',@ScheduleCode = '" + strID+ "'");
//			if(iRow !=null)
//				return iRow;
//			else
//				return null;
//		}
		public static DataTable TourOfLeaveDetails_GetDataByID(string strScheduleCode)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("TS_spfrmSchedule @Activity='Search',@ScheduleCode='" + strScheduleCode + "'");
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
	}
}
