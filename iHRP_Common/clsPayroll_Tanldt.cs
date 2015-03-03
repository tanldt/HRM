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
	public class clsPayroll
	{
		public clsPayroll()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static DataTable ViewDetailbyEmpID(string sMonth, string sEmpID)
		{
			DataTable dtSeq = new DataTable();
			try
			{
				dtSeq = clsDB.GetDataTable("PR_spCalPayroll @Activity = 'ViewDetailEmp',@Month ='" + sMonth + "',@EmpID ='" + sEmpID + "' ");
				
				return dtSeq;
			}
			catch //(Exception exp)
			{				
				//sReturnValue = exp.Message;
				return null;
			}
			finally
			{
				dtSeq.Dispose();
				//dtItemUser.Dispose();
			}
		}
		public static void MaketablePayroll(string sMonth)
		{
			clsDB.Exc_CommandText("PR_spfrmCreatePayrollData @Activity = N'CreateNewPayroll',@Month=N'" +sMonth + "'");
		}
		public static string CalulateItemSYS(string sMonth, string LSCompanyID, string LSLevel1ID, string LSLevel2ID
			, string LSLevel3ID, string EmpID, string EmpName, string sLanguageID, string sAccountID)
		{
			string sReturnValue="";
			DataTable dtItemSys = new DataTable();
			try
			{
				// Buoc 1: Tinh cac salary Item System
				//Lay du lieu SalaryItem SYSTEM
				dtItemSys = clsDB.GetDataTable("PR_spCalPayroll @Activity = 'GetSalaryItemSys'");
				// tinh du lieu salaryItem SYSTEM
				foreach(DataRow oDataRow in dtItemSys.Rows)	
				{
					string sLSSalaryItemDataID = oDataRow["LSSalaryItemDataID"].ToString();
					string sFormula = oDataRow["Formula"].ToString() + " " + oDataRow["ObjectName"].ToString() + " " + oDataRow["Criteria"].ToString();
					clsDB.Exc_CommandText("PR_spCalPayroll @Activity = 'CalSalaryItemSYS', @LSSalaryItemDataID = '"+sLSSalaryItemDataID+"', @Formula='"+sFormula
						+"',@Month ='" + sMonth + "',@LSCompanyID ='" + LSCompanyID + "',@LSLevel1ID ='" + LSLevel1ID + "',@LSLevel2ID ='" + LSLevel2ID
						+ "',@LSLevel3ID ='" + LSLevel3ID  + "',@EmpCode ='" + EmpID + "',@EmpName =N'" + EmpName + "',@UserGroupID ='" + sAccountID
						+"'");
				}
				return sReturnValue;
			}
			catch (Exception exp)
			{				
				sReturnValue = exp.Message;
				return sReturnValue;
			}
			finally
			{
				dtItemSys.Dispose();
			}
		}
		public static string CalulateItemUser(string sMonth, string LSCompanyID, string LSLevel1ID, string LSLevel2ID
			, string LSLevel3ID, string EmpID, string EmpName, string sLanguageID, string sAccountID
			, string SetFormula)
		{
			string sReturnValue="";
			DataTable dtSeq = new DataTable();
			DataTable dtItemUser = new DataTable();
			try
			{
				// Buoc 1: Tinh cac salary Item for user
				dtSeq = clsDB.GetDataTable("PR_spCalPayroll @Activity = 'GetComputationSeq',@Month ='" + sMonth + "' ");
				foreach(DataRow oDataRow in dtSeq.Rows)	
				{
					string sComputationSeq = oDataRow["ComputationSeq"].ToString();
					//string sLSSalaryItemDataID = oDataRow["LSSalaryItemDataID"].ToString();
					dtItemUser = clsDB.GetDataTable("PR_spCalPayroll @Activity = 'GetSalaryItemUser', @ComputationSeq = '"+sComputationSeq+"', @SetFormulaID='"+SetFormula
						+"',@Month ='" + sMonth + "',@LSCompanyID ='" + LSCompanyID + "',@LSLevel1ID ='" + LSLevel1ID + "',@LSLevel2ID ='" + LSLevel2ID
						+ "',@LSLevel3ID ='" + LSLevel3ID  + "',@EmpCode ='" + EmpID + "',@EmpName =N'" + EmpName + "',@UserGroupID ='" + sAccountID
						+"'");
					foreach(DataRow oRow in dtItemUser.Rows)	
					{
						string sLSSalaryItemDataID = oRow["LSSalaryItemDataID"].ToString();
						string sFormula = oRow["Formula"].ToString();
//						clsDB.Exc_CommandText("PR_spCalPayroll @Activity ='CalSalaryBy',@Month= '" + sMonth 
//							+ "',@LSSalaryItemDataID= '"+sLSSalaryItemDataID+"',@Formula= '"+sFormula
//							+ "'");

						clsDB.Exc_CommandText("PR_spCalPayroll @Activity ='CalSalaryItemUser',@Month= '" + sMonth 
							+ "',@LSSalaryItemDataID= '"+sLSSalaryItemDataID+"',@Formula= '"+sFormula
							+"',@EmpCode= '" + EmpID + "',@EmpName= N'" +EmpName + "',@LSCompanyID= N'" +  LSCompanyID 
							+ "',@LSLevel1ID= N'" +  LSLevel1ID + "',@LSLevel2ID= N'" +  LSLevel2ID + "',@LSLevel3ID= N'" + LSLevel3ID 
							+ "'");
					}
				}
				return sReturnValue;
			}
			catch (Exception exp)
			{				
				sReturnValue = exp.Message;
				return sReturnValue;
			}
			finally
			{
				dtSeq.Dispose();
				dtItemUser.Dispose();
			}
		}
		public static string CalulateItemUser(string sMonth, string LSCompanyID, string LSLevel1ID, string LSLevel2ID
			, string LSLevel3ID, string EmpID, string EmpName, string sLanguageID, string sAccountID, string SetFormula
			,string sLSSalaryItemDataID, string sFormula)
		{
			string sReturnValue="";
			try
			{
				clsDB.Exc_CommandText("PR_spCalPayroll @Activity ='CalSalaryItemUser',@Month= '" + sMonth 
					+ "',@LSSalaryItemDataID= '"+sLSSalaryItemDataID+"',@Formula= '"+sFormula
					+"',@EmpCode= '" + EmpID + "',@EmpName= N'" +EmpName + "',@LSCompanyID= N'" +  LSCompanyID 
					+ "',@LSLevel1ID= N'" +  LSLevel1ID + "',@LSLevel2ID= N'" +  LSLevel2ID + "',@LSLevel3ID= N'" + LSLevel3ID 
					+ "',@SetFormulaID= " +  SetFormula + "");
				return sReturnValue;
			}
			catch (Exception exp)
			{				
				sReturnValue = exp.Message;
				return sReturnValue;
			}
		}
		public static DataTable GetComputationSeq(string sMonth)
		{
			DataTable dtSeq = new DataTable();
			try
			{
				dtSeq = clsDB.GetDataTable("PR_spCalPayroll @Activity = 'GetComputationSeq',@Month ='" + sMonth + "' ");
				
				return dtSeq;
			}
			catch //(Exception exp)
			{				
				//sReturnValue = exp.Message;
				return null;
			}
			finally
			{
				dtSeq.Dispose();
				//dtItemUser.Dispose();
			}
		}
		public static DataTable GetSalaryItemUser(string sMonth, string LSCompanyID, string LSLevel1ID, string LSLevel2ID
			, string LSLevel3ID, string EmpID, string EmpName, string sLanguageID, string sAccountID, string SetFormula, string sComputationSeq)
		{
			DataTable dtItemUser = new DataTable();
			try
			{
				dtItemUser = clsDB.GetDataTable("PR_spCalPayroll @Activity = 'GetSalaryItemUser', @ComputationSeq = '"+sComputationSeq+"', @SetFormulaID='"+SetFormula
					+"',@Month ='" + sMonth + "',@LSCompanyID ='" + LSCompanyID + "',@LSLevel1ID ='" + LSLevel1ID + "',@LSLevel2ID ='" + LSLevel2ID
					+ "',@LSLevel3ID ='" + LSLevel3ID  + "',@EmpCode ='" + EmpID + "',@EmpName =N'" + EmpName + "',@UserGroupID ='" + sAccountID
					+"'");
				return dtItemUser;
			}
			catch //(Exception exp)
			{				
				return null;
			}
			finally
			{
				dtItemUser.Dispose();
			}
		}
		
		public static string CalculateSalary(string sMonth, string LSCompanyID, string LSLevel1ID, string LSLevel2ID
			, string LSLevel3ID, string EmpID, string EmpName, string sLanguageID, string sAccountID, string SetFormula)
		{
			string sReturnValue="";
			//SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			SqlCommand cmd = new SqlCommand();
			cnn.Open();
			//sqlTran = cnn.BeginTransaction();				
			//cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PR_spPayrollCollection";
			cmd.CommandTimeout = 8000;
			try
			{			
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "CalculateSalary";
				cmd.Parameters.Add("@Month",SqlDbType.NVarChar,7).Value = sMonth;
//				cmd.Parameters.Add("@ToDate",SqlDbType.NVarChar,12).Value = sToDate;
				cmd.Parameters.Add("@LSCompanyID",SqlDbType.NVarChar,12).Value = LSCompanyID;
				cmd.Parameters.Add("@LSLevel1ID",SqlDbType.NVarChar,12).Value = LSLevel1ID;
				cmd.Parameters.Add("@LSLevel2ID",SqlDbType.NVarChar,12).Value = LSLevel2ID;					
				cmd.Parameters.Add("@LSLevel3ID",SqlDbType.NVarChar,12).Value = LSLevel3ID;
				cmd.Parameters.Add("@EmpCode",SqlDbType.NVarChar,12).Value = EmpID;
				cmd.Parameters.Add("@EmpName",SqlDbType.NVarChar,200).Value = EmpName;
				cmd.Parameters.Add("@UserGroupID",SqlDbType.NVarChar,50).Value = sAccountID;
				cmd.Parameters.Add("@SetFormulaID",SqlDbType.NVarChar,50).Value = SetFormula;
				cmd.Parameters.Add("@sError", SqlDbType.NVarChar,4000).Direction = ParameterDirection.InputOutput;
				cmd.ExecuteNonQuery();
				sReturnValue = cmd.Parameters["@sError"].Value.ToString().Trim();
				//sqlTran.Commit();
//				if (sReturnValue=="")
//					return clsChangeLang.getStringAlert("PC_0004",sLanguageID);
//				else
//					return clsChangeLang.getStringAlert("PC_0003",sLanguageID);
				return sReturnValue;
			}
//			catch (SqlException ex)
//			{
//				//return clsChangeLang.getStringAlert("PC_0003",sLanguageID);
//				return "";
//			}
			catch (Exception exp)
			{				
				//sqlTran.Rollback();
				return exp.Message;
			}
			finally
			{
				cmd.Dispose();
				//sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();		
			}
		}
		public static string CalculateSalary(string sMonth, string LSCompanyID, string LSLevel1ID, string LSLevel2ID
			, string LSLevel3ID, string EmpID, string EmpName, string sLanguageID, string sAccountID)
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
				cmd.Parameters.Add("@Month",SqlDbType.NVarChar,7).Value = sMonth;
				//				cmd.Parameters.Add("@ToDate",SqlDbType.NVarChar,12).Value = sToDate;
				cmd.Parameters.Add("@LSCompanyID",SqlDbType.NVarChar,12).Value = LSCompanyID;
				cmd.Parameters.Add("@LSLevel1ID",SqlDbType.NVarChar,12).Value = LSLevel1ID;
				cmd.Parameters.Add("@LSLevel2ID",SqlDbType.NVarChar,12).Value = LSLevel2ID;					
				cmd.Parameters.Add("@LSLevel3ID",SqlDbType.NVarChar,12).Value = LSLevel3ID;
				cmd.Parameters.Add("@EmpCode",SqlDbType.NVarChar,12).Value = EmpID;
				cmd.Parameters.Add("@EmpName",SqlDbType.NVarChar,200).Value = EmpName;
				cmd.Parameters.Add("@UserGroupID",SqlDbType.NVarChar,50).Value = sAccountID;
				cmd.Parameters.Add("@sError", SqlDbType.NVarChar,4000).Direction = ParameterDirection.InputOutput;
				cmd.ExecuteNonQuery();
				sReturnValue = cmd.Parameters["@sError"].Value.ToString().Trim();
				sqlTran.Commit();
				//				if (sReturnValue=="")
				//					return clsChangeLang.getStringAlert("PC_0004",sLanguageID);
				//				else
				//					return clsChangeLang.getStringAlert("PC_0003",sLanguageID);
				return sReturnValue;
			}
				//			catch (SqlException ex)
				//			{
				//				//return clsChangeLang.getStringAlert("PC_0003",sLanguageID);
				//				return "";
				//			}
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
		public static string CalculateSalary(string sMonth, string EmpID, string sAccountID)
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
				cmd.Parameters.Add("@Month",SqlDbType.NVarChar,7).Value = sMonth;
				cmd.Parameters.Add("@EmpCode",SqlDbType.NVarChar,12).Value = EmpID;
				cmd.Parameters.Add("@UserGroupID",SqlDbType.NVarChar,50).Value = sAccountID;
				cmd.Parameters.Add("@sError", SqlDbType.NVarChar,4000).Direction = ParameterDirection.InputOutput;
				cmd.ExecuteNonQuery();
				sReturnValue = cmd.Parameters["@sError"].Value.ToString().Trim();
				sqlTran.Commit();
				//				if (sReturnValue=="")
				//					return clsChangeLang.getStringAlert("PC_0004",sLanguageID);
				//				else
				//					return clsChangeLang.getStringAlert("PC_0003",sLanguageID);
				return sReturnValue;
			}
				//			catch (SqlException ex)
				//			{
				//				//return clsChangeLang.getStringAlert("PC_0003",sLanguageID);
				//				return "";
				//			}
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
		public static DataTable GetDataSalary(string sMonth, string LSCompanyID, string LSLevel1ID, string LSLevel2ID
			, string LSLevel3ID, string EmpID, string EmpName, string sLanguageID, string sAccountID)
		{
			try
			{
				DataTable dtData= clsDB.GetDataTable("PR_spPayrollCollection 'GetDataSalary',@LSCompanyID='" + LSCompanyID 
					+ "',@LSLevel1ID='" + LSLevel1ID 
					+ "',@LSLevel2ID='" + LSLevel2ID 
					+ "',@LSLevel3ID='" + LSLevel3ID 
					+ "',@EmpCode='" + EmpID 
					+ "',@EmpName='" + EmpName 
					+ "',@UserGroupID='" + sAccountID 
					+ "',@Month='" + sMonth 
					+ "'");
				return dtData;
			}
			catch (Exception ex)
			{
				return null;
			}			
		}
		public static DataTable GetDataSalaryExport(string sMonth, string LSCompanyID, string LSLevel1ID, string LSLevel2ID
			, string LSLevel3ID, string EmpID, string EmpName, string sLanguageID, string sAccountID)
		{
			try
			{
				DataTable dtData= clsDB.GetDataTable("PR_spPayrollCollection 'GetDataExport',@LSCompanyID='" + LSCompanyID 
					+ "',@LSLevel1ID='" + LSLevel1ID 
					+ "',@LSLevel2ID='" + LSLevel2ID 
					+ "',@LSLevel3ID='" + LSLevel3ID 
					+ "',@EmpCode='" + EmpID 
					+ "',@EmpName='" + EmpName 
					+ "',@UserGroupID='" + sAccountID 
					+ "',@Month='" + sMonth 
					+ "'");
				return dtData;
			}
			catch (Exception ex)
			{
				return null;
			}			
		}
		public static DataTable GetCustomExport(string ExportCustomID)
		{
			return clsDB.GetDataTable("select * from PR_tblExportCustomDetail where Visible = 1 and ExportCustomID = " +ExportCustomID+ " order by Seq");
		}
		public static string sLockSalary(DataGrid dtgList)
		{
			string sErrMess="";
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			SQLconn.Open();
			try
			{
				
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "PR_spPayrollCollection";
   
				CheckBox obj = new CheckBox();
				for (int i = 0;i<= dtgList.Items.Count - 1;i++)
				{
					obj = (CheckBox) dtgList.Items[i].FindControl("chkSelect");					
					if (obj.Checked)
					{
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "SaveLock";					

						cmd.Parameters.Add("@LSCompanyID", SqlDbType.NVarChar,12).Value= Mession.GlbCusGroupID; 

						cmd.Parameters.Add("@LSLevel1ID", SqlDbType.NVarChar,12).Value= dtgList.Items[i].Cells[0].Text.Trim(); 
		
						cmd.Parameters.Add("@Month", SqlDbType.NVarChar,7).Value= dtgList.Items[i].Cells[1].Text.Trim(); 
		
						cmd.Parameters.Add("@sError", SqlDbType.NVarChar,1000).Direction = ParameterDirection.InputOutput;
						cmd.ExecuteNonQuery();							
					}
				}
				sErrMess= cmd.Parameters["@sError"].Value.ToString();
				cmd.Transaction.Commit();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();	
				if (sErrMess!="")
					return sErrMess;
				else
				{
					return "";
				}
				
			}
			catch (Exception exp)
			{
				string strErr = exp.Message;
				cmd.Transaction.Rollback();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				return strErr;
			}
		}
	}
}
