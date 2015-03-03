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
	/// Summary description for clsTMS_StandardSheet.
	/// </summary>
	public class clsTMS_StandardSheet
	{
		public clsTMS_StandardSheet()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region Standard working Company
		public static DataTable GetCompanyList(string UserGroupID)
		{
			string strSql = "TMS_spfrmStandardWorking @Activity='GetCompanyList',@UserGroupID=N'" + UserGroupID + "'";
			DataTable dtb = clsDB.GetDataTable(strSql);
			return dtb;
		}
		public static string sImpact(DataGrid dtgList)
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
				cmd.CommandText = "TMS_spfrmStandardWorking";
   
				CheckBox obj = new CheckBox();
				for (int i = 0;i<= dtgList.Items.Count - 1;i++)
				{
					obj = (CheckBox) dtgList.Items[i].FindControl("chkSelect");					
					if (obj.Checked)
					{
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "Save";					

						if (dtgList.Items[i].Cells[0].Text.Trim()=="")
							cmd.Parameters.Add("@LSLevel1ID", SqlDbType.NVarChar).Value=DBNull.Value;
						else
							cmd.Parameters.Add("@LSLevel1ID", SqlDbType.NVarChar,12).Value= dtgList.Items[i].Cells[0].Text.Trim(); 
		
						//int sStandard = clsDB.SafeDataInteger(((TextBox)dtgList.Items[i].FindControl("txtStandardworking")).Text);
						string sStandard = clsDB.SafeDataString(((TextBox)dtgList.Items[i].FindControl("txtStandardworking")).Text);
						
						if (sStandard == "" || sStandard == "0")
							cmd.Parameters.Add("@StandardWorking", SqlDbType.Float).Value=DBNull.Value;
						else
							cmd.Parameters.Add("@StandardWorking", SqlDbType.Float).Value= sStandard;
		
						cmd.Parameters.Add("@ReturnMess", SqlDbType.NVarChar,1000).Direction = ParameterDirection.InputOutput;
						cmd.ExecuteNonQuery();							
					}
				}
				sErrMess= cmd.Parameters["@ReturnMess"].Value.ToString();
				cmd.Transaction.Commit();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();	
				if (sErrMess!="")
					return sErrMess;
				else
					return "";
				
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
		#endregion
		#region Emp
		public static DataTable GetEmpList(string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strPosition, string strJobCode, string strLocation, string strCompany, string strStatus,string sEmpTypeID)
		{
			string strSql = "TMS_spfrmStandardWorking @Activity='GetEmpList',@EmpID=N'" + strEmpID + "', @EmpName=N'" + strEmpName
				+ "', @LSLevel1ID=N'" + strLevel1 + "', @LSLevel2ID=N'" + strLevel2 + "',@LSLevel3ID=N'" + strLevel3
				+ "',@LSPositionID='" + strPosition + "',@LSJobCodeID='" + strJobCode 
				+ "',@LSLocationID=N'" + strLocation + "',@LSCompanyID=N'" + strCompany + "',@UserGroupID=N'" + Mession.GlbUser + "',@LSEmpTypeID='" + sEmpTypeID + "'";
			if (strStatus != "")
				strSql = strSql + ", @Status='" + strStatus + "'";

			DataTable dtb = clsDB.GetDataTable(strSql);
			return dtb;
		}

		public static string sImpactEmp(DataGrid dtgList)
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
				cmd.CommandText = "TMS_spfrmStandardWorking";
   
				CheckBox obj = new CheckBox();
				for (int i = 0;i<= dtgList.Items.Count - 1;i++)
				{
					obj = (CheckBox) dtgList.Items[i].FindControl("chkSelect");					
					if (obj.Checked)
					{
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "SaveEmp";					

						if (dtgList.Items[i].Cells[0].Text.Trim()=="")
							cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar).Value=DBNull.Value;
						else
							cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar,12).Value= dtgList.Items[i].Cells[0].Text.Trim(); 
		
						int sStandard = clsDB.SafeDataInteger(((TextBox)dtgList.Items[i].FindControl("txtStandardworking")).Text);
						if (sStandard == 0)
							cmd.Parameters.Add("@StandardWorking", SqlDbType.Int).Value=DBNull.Value;
						else
							cmd.Parameters.Add("@StandardWorking", SqlDbType.Int).Value= sStandard;
		
						cmd.Parameters.Add("@ReturnMess", SqlDbType.NVarChar,1000).Direction = ParameterDirection.InputOutput;
						cmd.ExecuteNonQuery();							
					}
				}
				sErrMess= cmd.Parameters["@ReturnMess"].Value.ToString();
				cmd.Transaction.Commit();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();	
				if (sErrMess!="")
					return sErrMess;
				else
					return "";
				
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
		#endregion
		#region Emp Type
		public static DataTable GetEmpStyleSheet(string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strPosition, string strJobCode, string strLocation, string strCompany, string strStatus,string sEmpTypeID, string StatusSearch)
		{
			string strSql = "TMS_spfrmStypeTimesheet @Activity='GetEmpList',@EmpID=N'" + strEmpID + "', @EmpName=N'" + strEmpName
				+ "', @LSLevel1ID=N'" + strLevel1 + "', @LSLevel2ID=N'" + strLevel2 + "',@LSLevel3ID=N'" + strLevel3
				+ "',@LSPositionID='" + strPosition + "',@LSJobCodeID='" + strJobCode 
				+ "',@LSLocationID=N'" + strLocation + "',@LSCompanyID=N'" + strCompany + "',@UserGroupID=N'" + Mession.GlbUser + "',@LSEmpTypeID='" + sEmpTypeID + "',@StatusSearch = " + StatusSearch;
			if (strStatus != "")
				strSql = strSql + ", @Status='" + strStatus + "'";

			DataTable dtb = clsDB.GetDataTable(strSql);
			return dtb;
		}

		public static string sImpactType(DataGrid dtgList)
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
				cmd.CommandText = "TMS_spfrmStypeTimesheet";
   
				CheckBox obj = new CheckBox();
				for (int i = 0;i<= dtgList.Items.Count - 1;i++)
				{
					obj = (CheckBox) dtgList.Items[i].FindControl("chkSelect");					
					if (obj.Checked)
					{
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "Save";					

						if (dtgList.Items[i].Cells[0].Text.Trim()=="")
							cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar).Value=DBNull.Value;
						else
							cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar,12).Value= dtgList.Items[i].Cells[0].Text.Trim(); 
		
						int sStandard = clsDB.SafeDataInteger(((DropDownList)dtgList.Items[i].FindControl("cboStyleSheet")).SelectedValue);
						if (sStandard == 0)
							cmd.Parameters.Add("@StyleSheet", SqlDbType.Int).Value=DBNull.Value;
						else
							cmd.Parameters.Add("@StyleSheet", SqlDbType.Int).Value= sStandard;
		
						cmd.Parameters.Add("@ReturnMess", SqlDbType.NVarChar,1000).Direction = ParameterDirection.InputOutput;
						cmd.ExecuteNonQuery();							
					}
				}
				sErrMess= cmd.Parameters["@ReturnMess"].Value.ToString();
				cmd.Transaction.Commit();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();	
				if (sErrMess!="")
					return sErrMess;
				else
					return "";
				
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
		#endregion
	}
}
