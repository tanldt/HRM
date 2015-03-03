using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web;
using System.Web.Caching;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;
//using System.Data.SqlClient;
//using System.Configuration;

namespace iHRPCore
{
	/// <summary>
	/// Summary description for clsPayroll.
	/// </summary>
	public class clsReportPayroll
	{
		public clsReportPayroll()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		
		static public DataTable getData_Tax_02_KK(string sYear, string sMonth, string sReportName, string sEmpCode, string sEmpName, string sLSCompanyID,
			string sLSLevel1ID, string sLSLevel2ID, string sLSLevel3ID, string sLSPositionID, string sLSLocationID,string strStatus, System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			DataTable dtData= new DataTable();
			try
			{
				string sSQL="RPT_spfrmReportPIT_KK02 @Activity='" + sReportName + "', @Year='" + sYear + "', @Month='" + sMonth 
					+ "', @LSCompanyID=N'" + sLSCompanyID 
					+ "',@LSLevel1ID=N'" + sLSLevel1ID
					+ "', @UserGroupID=N'" + sAccountLogin + "'"; 											 
				if (strStatus != "")
					sSQL = sSQL + ", @Status='" + strStatus + "'";
				dtData= clsDB.GetDataTable(sSQL);
				return dtData;
			}
			catch
			{
				return null;
			}
			finally
			{
				dtData.Dispose();
			}
		}
		static public DataTable getData_Tax_02_KK_Quarter(string sYear, string sQuarter, string sReportName, string sEmpCode, string sEmpName, string sLSCompanyID,
			string sLSLevel1ID, string sLSLevel2ID, string sLSLevel3ID, string sLSPositionID, string sLSLocationID,string strStatus, System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			DataTable dtData= new DataTable();
			try
			{
				string sSQL="RPT_spfrmReportPIT_KK02_Quarter @Activity='" + sReportName + "', @Year='" + sYear + "', @Quarter='" + sQuarter 
					+ "', @LSCompanyID=N'" + sLSCompanyID 
					+ "',@LSLevel1ID=N'" + sLSLevel1ID
					+ "', @UserGroupID=N'" + sAccountLogin + "'"; 											 
				if (strStatus != "")
					sSQL = sSQL + ", @Status='" + strStatus + "'";
				dtData= clsDB.GetDataTable(sSQL);
				return dtData;
			}
			catch
			{
				return null;
			}
			finally
			{
				dtData.Dispose();
			}
		}
		static public DataTable getData_Tax_03_KK(string sYear, string sMonth, string sReportName, string sEmpCode, string sEmpName, string sLSCompanyID,
			string sLSLevel1ID, string sLSLevel2ID, string sLSLevel3ID, string sLSPositionID, string sLSLocationID,string strStatus, System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			DataTable dtData= new DataTable();
			try
			{
				string sSQL="RPT_spfrmReportPIT_KK03 @Activity='" + sReportName + "', @Year='" + sYear + "', @Month='" + sMonth 
					+ "', @LSCompanyID=N'" + sLSCompanyID 
					+ "',@LSLevel1ID=N'" + sLSLevel1ID
					+ "', @UserGroupID=N'" + sAccountLogin + "'"; 											 
				if (strStatus != "")
					sSQL = sSQL + ", @Status='" + strStatus + "'";
				dtData= clsDB.GetDataTable(sSQL);
				return dtData;
			}
			catch
			{
				return null;
			}
			finally
			{
				dtData.Dispose();
			}
		}
		static public DataTable getData_Tax_05_KK(string sYear, string sLSCompanyID,
			string sLSLevel1ID, System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			DataTable dtData= new DataTable();
			try
			{
				string sSQL="RPT_spfrmReportPIT_KK05 @Year='" + sYear
					+ "', @LSCompanyID=N'" + sLSCompanyID 
					+ "',@LSLevel1ID=N'" + sLSLevel1ID
					+ "', @UserGroupID=N'" + sAccountLogin + "'"; 											 
				dtData= clsDB.GetDataTable(sSQL);
				return dtData;
			}
			catch
			{
				return null;
			}
			finally
			{
				dtData.Dispose();
			}
		}
		static public DataTable getData_Tax_05A_BK(string sYear, string sLSCompanyID,
			string sLSLevel1ID, System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			DataTable dtData= new DataTable();
			try
			{
				string sSQL="RPT_spfrmReportPIT_05A_BK @Year='" + sYear
					+ "', @LSCompanyID=N'" + sLSCompanyID 
					+ "',@LSLevel1ID=N'" + sLSLevel1ID
					+ "', @UserGroupID=N'" + sAccountLogin + "'"; 											 
				dtData= clsDB.GetDataTable(sSQL);
				return dtData;
			}
			catch
			{
				return null;
			}
			finally
			{
				dtData.Dispose();
			}
		}
	}
}
