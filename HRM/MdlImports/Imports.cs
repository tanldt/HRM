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
using iHRPCore.Com;

namespace HRMCore.Com
{
	/// <summary>
	/// Summary description for Imports.
	/// </summary>
	public class clsImports
	{
		public clsImports()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region Save du lieu from Datagird
		/// <summary>
		/// Tanldt
		/// 16/09/2005
		/// Insert or update data
		/// </summary>
		/// <param name="strActivity">'Save' or 'Update'</param>
		/// <param name="Page">Page</param>
		/// <param name="strProcedureName">Store Prodedure Name</param>
		/// <returns>string</returns>		
		public static string ImpactDB_DataGrid(string strParamUserAction,Object strUserAction,DataGrid dtgGrid,
			string strSelect,string strActivity,string strProcedureName, string strListID, string Tabid)
		{
			string strErrorID = "";
			DataTable dtObj = new DataTable() ;
			dtObj = clsCommon.GetDataTable("sp_Common @ObjName=N'" + strProcedureName + "'");

			DataTable dtMap = new DataTable() ;
			dtMap = clsCommon.GetDataTable("[IMP_clsConfig] @Activity = 'GetConfigMapID', @CodeID=" + Tabid);

			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();			
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = strProcedureName;
			try
			{
				for(int i=0;i<dtgGrid.Items.Count;i++)
				{
					string[] arrID = strListID.Trim().Split('$');
					if(((CheckBox)dtgGrid.Items[i].FindControl(strSelect)).Checked==true)
					{
						//						System.Web.SessionState Session["ssIDRows"] = "";
						//						Session["ssIDRows"] = i;
						try
						{
							cmd.Parameters.Clear();
							cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = strActivity.Trim();
							cmd.Parameters.Add(strParamUserAction,SqlDbType.NVarChar,12).Value = strUserAction;
							cmd.Parameters.Add("@ReturnMess", SqlDbType.NVarChar,1000).Direction = ParameterDirection.InputOutput;
							for(int j=0;j< dtMap.Rows.Count;j++)
							{
								string ColumnNameMap = dtMap.Rows[j]["ParamStored"].ToString().Trim();
								int ColumnIndex = clsCommon.SafeDataInteger(dtMap.Rows[j]["ColumnIndex"]) + 2;
								DataRow[] iRow = dtObj.Select("columnname ='@" + ColumnNameMap + "'");
								if(iRow.Length > 0)
								{
									SqlParameter param = new SqlParameter();
									param.ParameterName = iRow[0]["columnname"].ToString().Trim();
									param.SqlDbType = clsCommon.ConvertDataType(iRow[0]["datatype"].ToString().Trim());
									param.Size = clsCommon.SafeDataInteger(iRow[0]["size"].ToString().Trim());
									//Value
									string strValue = dtgGrid.Items[i].Cells[ColumnIndex].Text.Trim();

									if(param.SqlDbType==SqlDbType.Real)
										strValue = strValue.Replace(",","");

									if(strValue.Replace("&nbsp;","").Trim()=="")
										param.Value = DBNull.Value ;
									else
									{
										if(param.SqlDbType == SqlDbType.Bit)
											param.Value = (strValue=="True" || strValue=="1")?1:0;
										else if(param.SqlDbType == SqlDbType.Int)
											param.Value = strValue.Replace(",","");
										else
											param.Value = strValue; 
									}
									
									cmd.Parameters.Add(param);
								}
							}
							cmd.ExecuteNonQuery();
							// lay ket qua tra ve tu Store
							if (cmd.Parameters["@ReturnMess"].Value.ToString() != "") // co chuoi loi tra ve tu store 
								strErrorID += i.ToString() + "@" + cmd.Parameters["@ReturnMess"].Value.ToString() + "$";
						}												
						catch(Exception exp)
						{
							string a = exp.Message;
							strErrorID += i.ToString() + "@" + "Có lỗi xãy ra! không thể lưu." + "$";
							//return strErrorID;
						}
						//finally
						//{	
						//}
					}
				}
				
				if (sqlTran != null ) sqlTran.Commit();				
				
				if (strErrorID == "")
					return "Successful!";
				else
					return strErrorID;
				
			}
			catch(Exception exp)
			{
				//if (sqlTran != null) sqlTran.Rollback();				
				//return exp.Message;				
				return strErrorID;
			}
		}
		#endregion
	}
}
