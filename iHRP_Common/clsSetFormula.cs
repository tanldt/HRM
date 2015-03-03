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
	/// Summary description for clsSetFormula.
	/// </summary>
	public class clsSetFormula
	{
		public clsSetFormula()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region clsSetFormula Company
		public static DataTable GetCompanyList(string UserGroupID)
		{
			string strSql = "PR_spfrmSetFomula @Activity='GetCompanyList',@UserGroupID=N'" + UserGroupID + "'";
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
				cmd.CommandText = "PR_spfrmSetFomula";
   
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
		
						if (((DropDownList)dtgList.Items[i].FindControl("cboFormula")).SelectedValue=="")
							cmd.Parameters.Add("@SetFormulaID", SqlDbType.Int).Value=DBNull.Value;
						else
							cmd.Parameters.Add("@SetFormulaID", SqlDbType.Int).Value= ((DropDownList)dtgList.Items[i].FindControl("cboFormula")).SelectedValue;

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
