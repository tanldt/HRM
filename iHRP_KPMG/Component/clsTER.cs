using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.SessionState;
using iHRPCore.Com;
using System.Web.Mail;
using System.Net;
using System.IO;

namespace iHRPCore.TERComponent
{
	/// <summary>
	/// Summary description for clsTER.
	/// </summary>
	public class clsTERPrePayroll
	{
		public static DataTable GetEmpPrePayroll(string strEmpID)
		{
			DataTable dtb = clsCommon.GetDataTable("TER_spfrmPrePayCal @Activity='GetDataByID',@EmpID='" + strEmpID + "'");		
			if (dtb.Rows.Count == 0)
				dtb = clsCommon.GetDataTable("TER_spfrmPrePayCal @Activity='GetEmpDefaultData',@EmpID='" + strEmpID + "'");
			return dtb;
		}
		
		public static string DeleteEmpPrePayroll(string strEmpID, string strLastWorkingDate)
		{
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "TER_spfrmPrePayCal";
   
				cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "Delete";						
				cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = strEmpID;
				cmd.Parameters.Add("@LastWorkingDate", SqlDbType.NVarChar, 12).Value = strLastWorkingDate;
				cmd.ExecuteNonQuery();											
				cmd.Transaction.Commit();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();	
				return "";
			}
			catch  (Exception exp)
			{								
				cmd.Transaction.Rollback();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				return exp.Message.Trim();
			}
		}
	}

	public class clsTERPayroll
	{
		public static DataTable GetEmpPayroll(string strEmpID)
		{
			DataTable dtb = clsCommon.GetDataTable("TER_spfrmPayCal @Activity='GetDataByID',@EmpID='" + strEmpID + "'");		
			return dtb;
		}
		
		public static string DeleteEmpPayroll(string strEmpID, string strLastWorkingDate)
		{
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "TER_spfrmPayCal";
   
				cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "Delete";						
				cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = strEmpID;
				cmd.Parameters.Add("@LastWorkingDate", SqlDbType.NVarChar, 12).Value = strLastWorkingDate;
				cmd.ExecuteNonQuery();											
				cmd.Transaction.Commit();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();	
				return "";
			}
			catch  (Exception exp)
			{								
				cmd.Transaction.Rollback();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				return exp.Message.Trim();
			}
		}
	}
}
