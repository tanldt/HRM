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
using System.Web;
//using MyDropDownList;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using iHRPCore.HRComponent;
using EeekSoft.Web;
using iHRPCore.Com;

namespace iHRPCore.HRComponent
{
	/// <summary>
	/// Summary description for clsHRTempKhanh.
	/// </summary>
	public class clsHRTempKhanh
	{
		public clsHRTempKhanh()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public static void SaveData(string strStoreName,string action,string refID,string fromdate,string todate,string language,string year,string employeeID,string RequestID)
		{
		/////////////
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = strStoreName;
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@FromDate",SqlDbType.NVarChar,12).Value =fromdate;
				cmd.Parameters.Add("@ToDate",SqlDbType.NVarChar,12).Value =todate;  
				cmd.Parameters.Add("@Language",SqlDbType.NVarChar,2).Value =language;  
				cmd.Parameters.Add("@Year",SqlDbType.NVarChar,12).Value =year;  
				cmd.Parameters.Add("@EmployeeID",SqlDbType.NVarChar,12).Value =employeeID;  
				cmd.Parameters.Add("@RequestID",SqlDbType.NVarChar,12).Value =RequestID;  
				cmd.Parameters.Add("@Action",SqlDbType.NVarChar,12).Value =action; 
				cmd.Parameters.Add("@RefLeaveRecordID",SqlDbType.NVarChar,12).Value =refID; 
				cmd.Parameters.Add("@LSWorkPoint",SqlDbType.NVarChar,12).Value =""; 
				cmd.Parameters.Add("@CreaterName",SqlDbType.NVarChar,12).Value =""; 
				cmd.ExecuteNonQuery();
				sqlTran.Commit();
				
			}
			catch (Exception exp)
			{
				string strErr = exp.Message;
				sqlTran.Rollback();
				
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();
			}
		////////////	
		}	
		public static DataTable LoadLeaveRecordByEmp(string strLanguage, string strEmpID, string strYear, string strCurUser)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb = clsCommon.GetDataTable("SI_spfrmTimeSheetSI '','','"+ strLanguage +"','"+ strYear  +"','"+ strEmpID  +"','','','','','GetAllData'");
					
			}
			catch
			{
				dtb = new DataTable();
			}
			return dtb;
		}
		public static DataTable LoadLeaveRecordByID(string strLanguage, string strEmpID, string strLeaveRecordID, string strRequestID,string year)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb = clsCommon.GetDataTable("SI_spfrmTimeSheetSI '','','"+strLanguage + "','2006','" + strEmpID   +"','"+ strRequestID +"','','','','FillEmpLRInfo'");

			}
			catch
			{
				dtb = new DataTable();
			}
			return dtb;
		}

		public static DataTable LoadLeaveDetails(string strLanguage, string strEmpID, string strFromDate, string strToDate, string strWorkPointID, string strLeaveRecordID, string strCurUser,string strYear)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb = clsCommon.GetDataTable("SI_spfrmTimeSheetSI '"+strFromDate+"','"+strToDate+"','"+strLanguage + "','2006','" + strEmpID +"','','"+ strLeaveRecordID +"','"+ strWorkPointID +"','"+ strCurUser  +"','loaddetail'");

			}
			catch
			{
				dtb = new DataTable();
			}
			return dtb;
		}

	
	}
/////////////////////////////////////////

}