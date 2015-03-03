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
	/// Summary description for clsPRTemp1.
	/// </summary>
	public class clsPRTemp1
	{
		public clsPRTemp1()
		{
			//
			// TODO: Add constructor logic here
			//
		}
	}
	public class clsPRCreateNewPayroll
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
		public static string CopyPayroll(string strFromDate,string strToDate,string strCurMonth, string strFromMonth,string strFromSalPeriod)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PR_spfrmCreatePayroll";			
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "CreateNewPayroll";
				cmd.Parameters.Add("@PFromDate",SqlDbType.NVarChar,12).Value = strFromDate.Trim();
				cmd.Parameters.Add("@PToDate",SqlDbType.NVarChar,12).Value = strToDate.Trim();
				cmd.Parameters.Add("@Month",SqlDbType.NVarChar,12).Value = strCurMonth.Trim();
				cmd.Parameters.Add("@FromMonth",SqlDbType.NVarChar,12).Value = strFromMonth.Trim();
				cmd.Parameters.Add("@FromSalPeriod",SqlDbType.NVarChar,12).Value = strFromSalPeriod.Trim();
				cmd.ExecuteNonQuery();
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
		public static string CreatePayroll(string strFromDate,string strToDate,string strCurMonth, string strFromMonth,string strFromSalPeriod)
		{
			/*SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 800;
			cmd.CommandText = "PR_spfrmCreatePayroll";
			*/
			try
			{
				/*cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "CreateNewPayroll";
				cmd.Parameters.Add("@PFromDate",SqlDbType.NVarChar,12).Value = strFromDate.Trim();
				cmd.Parameters.Add("@PToDate",SqlDbType.NVarChar,12).Value = strToDate.Trim();
				cmd.Parameters.Add("@Month",SqlDbType.NVarChar,12).Value = strCurMonth.Trim();
				cmd.Parameters.Add("@FromMonth",SqlDbType.NVarChar,12).Value = strFromMonth.Trim();
				cmd.Parameters.Add("@FromSalPeriod",SqlDbType.NVarChar,12).Value = strFromSalPeriod.Trim();
				cmd.ExecuteNonQuery();
				sqlTran.Commit();
				*/
				clsCommon.GetDataTableTimeOut("PR_spfrmCreatePayroll @Activity = N'CreateNewPayroll',@PFromDate=N'" + strFromDate + "',@PToDate=N'" + strToDate.Trim() + "',@Month=N'" + strCurMonth.Trim() + "',@FromMonth=N'" + strFromMonth.Trim() + "',@FromSalPeriod=N'" + strFromSalPeriod.Trim() + "'");
				return "";
			}
			catch (Exception exp)
			{
				//sqlTran.Rollback();
				return exp.Message;
			}
			finally
			{
				//cmd.Dispose();
				//sqlTran.Dispose();
				//if(cnn.State == ConnectionState.Open) cnn.Close();
			}
		}

		public static string CreatePayroll1(string strFromDate,string strToDate,string strCurMonth, string strFromMonth,string strFromSalPeriod)
		{						
			try
			{				
				clsCommon.GetDataTableTimeOut("PR_spfrmCreatePayroll1 @Activity = N'CreateNewPayroll',@PFromDate=N'" + strFromDate + "',@PToDate=N'" + strToDate.Trim() + "',@Month=N'" + strCurMonth.Trim() + "',@FromMonth=N'" + strFromMonth.Trim() + "',@FromSalPeriod=N'" + strFromSalPeriod.Trim() + "'");
				return "";
			}
			catch (Exception exp)
			{
				//sqlTran.Rollback();
				return exp.Message;
			}
			finally
			{
				//cmd.Dispose();
				//sqlTran.Dispose();
				//if(cnn.State == ConnectionState.Open) cnn.Close();
			}
		}
		// creat table fo payback of direct hire
		public static string CreatePayroll1_Payback(string strFromDate,string strToDate,string strCurMonth, string strFromMonth,string strFromSalPeriod)
		{						
			try
			{				
				clsCommon.GetDataTableTimeOut("PR_spfrmCreatePayroll1_PayBack @Activity = N'CreateNewPayroll',@PFromDate=N'" + strFromDate + "',@PToDate=N'" + strToDate.Trim() + "',@Month=N'" + strCurMonth.Trim() + "',@FromMonth=N'" + strFromMonth.Trim() + "',@FromSalPeriod=N'" + strFromSalPeriod.Trim() + "'");
				return "";
			}
			catch (Exception exp)
			{
				//sqlTran.Rollback();
				return exp.Message;
			}
			finally
			{
				//cmd.Dispose();
				//sqlTran.Dispose();
				//if(cnn.State == ConnectionState.Open) cnn.Close();
			}
		}
		// creat table fo payback of Expat
		public static string CreatePayroll2_Payback(string strFromDate,string strToDate,string strCurMonth, string strFromMonth,string strFromSalPeriod)
		{						
			try
			{				
				clsCommon.GetDataTableTimeOut("PR_spfrmCreatePayroll2_PayBack @Activity = N'CreateNewPayroll',@PFromDate=N'" + strFromDate + "',@PToDate=N'" + strToDate.Trim() + "',@Month=N'" + strCurMonth.Trim() + "',@FromMonth=N'" + strFromMonth.Trim() + "',@FromSalPeriod=N'" + strFromSalPeriod.Trim() + "'");
				return "";
			}
			catch (Exception exp)
			{
				//sqlTran.Rollback();
				return exp.Message;
			}
			finally
			{
				//cmd.Dispose();
				//sqlTran.Dispose();
				//if(cnn.State == ConnectionState.Open) cnn.Close();
			}
		}
		// creat table fo payback of secondee
		public static string CreatePayroll3_Payback(string strFromDate,string strToDate,string strCurMonth, string strFromMonth,string strFromSalPeriod)
		{						
			try
			{				
				clsCommon.GetDataTableTimeOut("PR_spfrmCreatePayroll3_PayBack @Activity = N'CreateNewPayroll',@PFromDate=N'" + strFromDate + "',@PToDate=N'" + strToDate.Trim() + "',@Month=N'" + strCurMonth.Trim() + "',@FromMonth=N'" + strFromMonth.Trim() + "',@FromSalPeriod=N'" + strFromSalPeriod.Trim() + "'");
				return "";
			}
			catch (Exception exp)
			{
				//sqlTran.Rollback();
				return exp.Message;
			}
			finally
			{
				//cmd.Dispose();
				//sqlTran.Dispose();
				//if(cnn.State == ConnectionState.Open) cnn.Close();
			}
		}



		public static string CreatePayroll2(string strFromDate,string strToDate,string strCurMonth, string strFromMonth,string strFromSalPeriod)
		{			
			try
			{				
				clsCommon.GetDataTableTimeOut("PR_spfrmCreatePayroll2 @Activity = N'CreateNewPayroll',@PFromDate=N'" + strFromDate + "',@PToDate=N'" + strToDate.Trim() + "',@Month=N'" + strCurMonth.Trim() + "',@FromMonth=N'" + strFromMonth.Trim() + "',@FromSalPeriod=N'" + strFromSalPeriod.Trim() + "'");
				return "";
			}
			catch (Exception exp)
			{
				//sqlTran.Rollback();
				return exp.Message;
			}
			finally
			{
				//cmd.Dispose();
				//sqlTran.Dispose();
				//if(cnn.State == ConnectionState.Open) cnn.Close();
			}
		}

		public static string CreatePayroll3(string strFromDate,string strToDate,string strCurMonth, string strFromMonth,string strFromSalPeriod)
		{			
			try
			{				
				clsCommon.GetDataTableTimeOut("PR_spfrmCreatePayroll3 @Activity = N'CreateNewPayroll',@PFromDate=N'" + strFromDate + "',@PToDate=N'" + strToDate.Trim() + "',@Month=N'" + strCurMonth.Trim() + "',@FromMonth=N'" + strFromMonth.Trim() + "',@FromSalPeriod=N'" + strFromSalPeriod.Trim() + "'");
				return "";
			}
			catch (Exception exp)
			{
				//sqlTran.Rollback();
				return exp.Message;
			}
			finally
			{
				//cmd.Dispose();
				//sqlTran.Dispose();
				//if(cnn.State == ConnectionState.Open) cnn.Close();
			}
		}
	}
}
