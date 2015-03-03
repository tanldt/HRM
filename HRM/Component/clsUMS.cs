using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using iHRPCore.Com;
using iHRPCore.TRComponent;
using GridSort;
using System.Configuration;

namespace iHRPCore.Component
{
	/// <summary>
	/// Summary description for clsUMS.
	/// </summary>
	public class clsUMS
	{	
		public static DataRow getAccountByID(string UserGroupID)
		{
			DataRow drData = clsCommon.GetDataRow("UMS_sptblUserAccount @Activity='GetByID',@UserGroupID='" + UserGroupID + "'");
			return drData;
		}
		public static bool ResetPass(string  strStoreName,string strKeyField,SqlDbType pType, int intSize, string strListID,string strListPass)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = strStoreName.Trim();
			//-------
			try
			{
				string[] arrID = strListID.Trim().Split('$');
				string[] arrPass = strListPass.Trim().Split('$');
				//--------------
				for(int i=0;i<arrID.Length-1;i++)
				{
					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "ResetPass";
					cmd.Parameters.Add("@Password",SqlDbType.NVarChar,50).Value = arrPass.GetValue(i).ToString().Trim();
					SqlParameter param = new SqlParameter();
					param.ParameterName = "@" + strKeyField.Trim();
					param.SqlDbType = pType;
					param.Size = intSize;				
					param.Value = arrID.GetValue(i).ToString().Trim();
					cmd.Parameters.Add(param);
					cmd.ExecuteNonQuery();
				}				
				sqlTran.Commit();
				return true;
			}
			catch (Exception exp)
			{
				string strErr = exp.Message;
				sqlTran.Rollback();
				return false;
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();
			}			
		}
		public static string getPassDefault(string UserGroupID)
		{
			DataRow drData = clsCommon.GetDataRow("HR_clsCommon @Activity='getPassDefault',@UserGroupID='" + UserGroupID + "'");
			return drData["PassDefault"].ToString();
		}
		
	}
}

	
	



