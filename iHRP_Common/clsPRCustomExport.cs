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
	/// Summary description for clsPRCustomExport.
	/// </summary>
	public class clsPRCustomExport
	{
		public clsPRCustomExport()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public static void Save_ExportCustom1(string sName, string sNote, string[] sList, string[] sListText)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();			
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			try
			{
				
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "PR_spfrmExportCustom";
				#region Save Master
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "SaveMaster";				
				cmd.Parameters.Add("@NameExport",SqlDbType.NVarChar,200).Value = sName;	
				cmd.Parameters.Add("@Note",SqlDbType.NVarChar,500).Value = sNote;
				
				cmd.Parameters.Add("@ID", SqlDbType.Int).Direction = ParameterDirection.InputOutput;

				cmd.ExecuteNonQuery();
				int iID = clsDB.SafeDataInteger(cmd.Parameters["@ID"].Value);
				#endregion
				#region save detail
				for(int i=0;i<sList.Length;i++)
				{
					string sCode = sList[i].ToString();
					string sSeq = Convert.ToString(i + 1);
					string ssName = sListText[i].ToString();
					bool blVisible = true;
					if (blVisible == true)
					{
						if (ssName == "")
							ssName = "Column Name " + i.ToString();
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "SaveExportDetail";
					
						cmd.Parameters.Add("@ExportCustomID",SqlDbType.Int).Value = iID;
						cmd.Parameters.Add("@Code",SqlDbType.NVarChar,15).Value = sCode;
						cmd.Parameters.Add("@Seq",SqlDbType.Int).Value = sSeq;
						cmd.Parameters.Add("@CustomName",SqlDbType.NVarChar,200).Value = ssName;
						cmd.Parameters.Add("@Visible",SqlDbType.Bit).Value = blVisible;
				
						cmd.ExecuteNonQuery();
					}
				}
				#endregion
				sqlTran.Commit();
			}
			catch//(Exception ex)
			{
				sqlTran.Rollback();
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();
			}
			
		}
		public static void Edit_ExportCustom1(int iID,string sName, string sNote, string[] sList, string[] sListText)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();			
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			try
			{
				
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "PR_spfrmExportCustom";
				#region Save Master
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "EditMaster";
				cmd.Parameters.Add("@ExportCustomID",SqlDbType.Int).Value = iID;
				cmd.Parameters.Add("@NameExport",SqlDbType.NVarChar,200).Value = sName;	
				cmd.Parameters.Add("@Note",SqlDbType.NVarChar,500).Value = sNote;
				
				cmd.Parameters.Add("@ID", SqlDbType.Int).Direction = ParameterDirection.InputOutput;

				cmd.ExecuteNonQuery();

				#endregion
				#region Delete detail
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "DeleteDetail";
				cmd.Parameters.Add("@ExportCustomID",SqlDbType.Int).Value = iID;

				cmd.ExecuteNonQuery();

				#endregion
				#region save detail
				for(int i=0;i<sList.Length;i++)
				{
					string sCode = sList[i].ToString();
					string sSeq = Convert.ToString(i + 1);
					string ssName = sListText[i].ToString();

					if (ssName == "")
						ssName = "Column Name " + Convert.ToString(i + 1);

					if (sSeq == "")
						sSeq = Convert.ToString(i + 1);

					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "SaveExportDetail";
					
					cmd.Parameters.Add("@ExportCustomID",SqlDbType.Int).Value = iID;
					cmd.Parameters.Add("@Code",SqlDbType.NVarChar,15).Value = sCode;
					cmd.Parameters.Add("@Seq",SqlDbType.Int).Value = sSeq;
					cmd.Parameters.Add("@CustomName",SqlDbType.NVarChar,200).Value = ssName;
					cmd.Parameters.Add("@Visible",SqlDbType.Bit).Value = true;

					cmd.ExecuteNonQuery();
					//					}
				}
				#endregion
				sqlTran.Commit();
			}
			catch//(Exception ex)
			{
				sqlTran.Rollback();
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();
			}
			
		}
		public static void Save_ExportCustom(string sName, string sNote, DataGrid dtgList)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();			
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			try
			{
				
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "PR_spfrmExportCustom";
				#region Save Master
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "SaveMaster";				
				cmd.Parameters.Add("@NameExport",SqlDbType.NVarChar,200).Value = sName;	
				cmd.Parameters.Add("@Note",SqlDbType.NVarChar,500).Value = sNote;
				
				cmd.Parameters.Add("@ID", SqlDbType.Int).Direction = ParameterDirection.InputOutput;

				cmd.ExecuteNonQuery();
				int iID = clsDB.SafeDataInteger(cmd.Parameters["@ID"].Value);
				#endregion
				#region save detail
				for(int i=0;i<dtgList.Items.Count;i++)
				{
					string sCode = dtgList.Items[i].Cells[0].Text.Trim();
					string sSeq = ((TextBox)dtgList.Items[i].FindControl("txtSeq")).Text;
					string ssName = ((TextBox)dtgList.Items[i].FindControl("txtName")).Text;
					bool blVisible = ((CheckBox)dtgList.Items[i].FindControl("chkVisible")).Checked;
					if (blVisible == true)
					{
						if (ssName == "")
							ssName = "Column Name " + i.ToString();
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "SaveExportDetail";
					
						cmd.Parameters.Add("@ExportCustomID",SqlDbType.Int).Value = iID;
						cmd.Parameters.Add("@Code",SqlDbType.NVarChar,15).Value = sCode;
						cmd.Parameters.Add("@Seq",SqlDbType.Int).Value = sSeq;
						cmd.Parameters.Add("@CustomName",SqlDbType.NVarChar,200).Value = ssName;
						cmd.Parameters.Add("@Visible",SqlDbType.Bit).Value = blVisible;
				
						cmd.ExecuteNonQuery();
					}
				}
				#endregion
				sqlTran.Commit();
			}
			catch//(Exception ex)
			{
				sqlTran.Rollback();
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();
			}
			
		}
		public static void Edit_ExportCustom(int iID,string sName, string sNote, DataGrid dtgList)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();			
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			try
			{
				
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "PR_spfrmExportCustom";
				#region Save Master
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "EditMaster";
				cmd.Parameters.Add("@ExportCustomID",SqlDbType.Int).Value = iID;
				cmd.Parameters.Add("@NameExport",SqlDbType.NVarChar,200).Value = sName;	
				cmd.Parameters.Add("@Note",SqlDbType.NVarChar,500).Value = sNote;
				
				cmd.Parameters.Add("@ID", SqlDbType.Int).Direction = ParameterDirection.InputOutput;

				cmd.ExecuteNonQuery();

				#endregion
				#region save detail
				for(int i=0;i<dtgList.Items.Count;i++)
				{
					string sCode = dtgList.Items[i].Cells[0].Text.Trim();
					string sSeq = ((TextBox)dtgList.Items[i].FindControl("txtSeq")).Text;
					string ssName = ((TextBox)dtgList.Items[i].FindControl("txtName")).Text;
					bool blVisible = ((CheckBox)dtgList.Items[i].FindControl("chkVisible")).Checked;
//					if (blVisible == true)
//					{
						if (ssName == "")
							ssName = "Column Name " + Convert.ToString(i + 1);

					if (sSeq == "")
						sSeq = Convert.ToString(i + 1);

						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "SaveExportDetail";
					
						cmd.Parameters.Add("@ExportCustomID",SqlDbType.Int).Value = iID;
						cmd.Parameters.Add("@Code",SqlDbType.NVarChar,15).Value = sCode;
						cmd.Parameters.Add("@Seq",SqlDbType.Int).Value = sSeq;
						cmd.Parameters.Add("@CustomName",SqlDbType.NVarChar,200).Value = ssName;
						cmd.Parameters.Add("@Visible",SqlDbType.Bit).Value = blVisible;

						cmd.ExecuteNonQuery();
//					}
				}
				#endregion
				sqlTran.Commit();
			}
			catch//(Exception ex)
			{
				sqlTran.Rollback();
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();
			}
			
		}
		public static DataRow GetDataMasterByID(string ID)
		{
			DataRow dr = clsDB.GetDataRow("PR_spfrmExportCustom @Activity = 'GetDataByID',@ExportCustomID = " +ID );
			return dr;
		}
		public static DataTable GetEmpList(string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strPosition, string strJobCode, string strLocation, string strCompany, string strStatus,string sEmpTypeID, string StatusSearch)
		{
			string strSql = "PR_clsExportCustomEmp @Activity='GetEmpList',@EmpID=N'" + strEmpID + "', @EmpName=N'" + strEmpName
				+ "', @LSLevel1ID=N'" + strLevel1 + "', @LSLevel2ID=N'" + strLevel2 + "',@LSLevel3ID=N'" + strLevel3
				+ "',@LSPositionID='" + strPosition + "',@LSJobCodeID='" + strJobCode 
				+ "',@LSLocationID=N'" + strLocation + "',@LSCompanyID=N'" + strCompany + "',@UserGroupID=N'" + Mession.GlbUser + "',@LSEmpTypeID='" + sEmpTypeID + "',@StatusSearch = " + StatusSearch;
			if (strStatus != "")
				strSql = strSql + ", @Status='" + strStatus + "'";

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
				cmd.CommandText = "PR_clsExportCustomEmp";
   
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
		
						if (((DropDownList)dtgList.Items[i].FindControl("cboFormula")).SelectedValue=="")
							cmd.Parameters.Add("@ExportCustomID", SqlDbType.Int).Value=DBNull.Value;
						else
							cmd.Parameters.Add("@ExportCustomID", SqlDbType.Int).Value= ((DropDownList)dtgList.Items[i].FindControl("cboFormula")).SelectedValue;
		
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
	}
}
