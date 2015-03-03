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

namespace iHRPCore.REComponent
{
	using iHRPCore.Com;
	/// <summary>
	/// Summary description for clsRETempSon.
	/// </summary>
	public class clsRETempSon
	{
		public clsRETempSon()
		{
			//
			// TODO: Add constructor logic here
			//
		}
	}

	public class clsREPersonal
	{
		/// <summary>
		/// Get Candidate information
		/// </summary>
		/// <param name="strCandidateID"></param>
		/// <returns></returns>
		public static DataRow GetDataByID(Object strCandidateID)
		{
			try
			{
				DataRow iRow = clsCommon.GetDataRow("RE_spfrmCandidate @Activity='GetDataByID', @CandidateID = N'" + strCandidateID + "'");
				if(iRow != null)
					return iRow;
				else
					return null;
			}
			catch
			{
				return null;
			}
		}

		public static bool CheckIdentityCard(Object strCandidateID,string strCardID)
		{
			try
			{
				DataRow iRow = clsCommon.GetDataRow("RE_spfrmCandidate @Activity='CheckIdentityCard', @CandidateID = N'" + strCandidateID + "', @IDNo = N'" + strCardID.Trim() + "'");
				if(iRow != null)
					return true;
				else
					return false;
			}
			catch
			{
				return false;
			}
		}
		
		/// <summary>
		/// Update candidate information
		/// </summary>
		/// <param name="pstrSQL">Cau lenh Store Update va cac tham so</param>
		/// <returns></returns>
		public static bool UpdateData(string pstrSQL)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = pstrSQL;
			//-------
			try
			{
				cmd.ExecuteNonQuery();
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
		/// <summary>
		/// Update candidate information
		/// </summary>
		/// <param name="pstrSQL">Cau lenh Store Update va cac tham so</param>
		/// <returns></returns>
		public static string sUpdateData(string pstrSQL)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = pstrSQL;
			//-------
			try
			{
				cmd.ExecuteNonQuery();
				sqlTran.Commit();
				return "";
			}
			catch (Exception exp)
			{
				string strErr = exp.Message;
				sqlTran.Rollback();
				return strErr;
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();
			}
		}
	}

	public class clsREWorkingBackground
	{
		/// <summary>
		/// Lay thong tin qua trinh lam viec cua ung vien
		/// </summary>
		/// <param name="strCandidateID">Ma so ung vien</param>
		/// <returns>DataTable</returns>
		public static DataTable GetDataByCandidateID(Object strCandidateID,string strLanguage)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("RE_spfrmCandidateWorkingBkgr @Activity='GetDataByCandidateID', @CandidateID = N'" + strCandidateID + "',@LanguageID='strLanguage'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}

		public static DataRow GetDataByID(Object strID)
		{
			DataRow iRow = clsCommon.GetDataRow("RE_spfrmCandidateWorkingBkgr @Activity='GetDataByID', @CandidateWorkingBkgrID = " + strID);
			if(iRow != null)
				return iRow;
			else
				return null;
		}
	}

	public class clsREQualification
	{
		/// <summary>
		/// Lay thong tin bang cap cua ung vien
		/// </summary>
		/// <param name="strCandidateID">Ma so ung vien</param>
		/// <returns>DataTable</returns>
		public static DataTable GetDataByCandidateID(Object strCandidateID,string strLanguage)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("RE_spfrmCandidateQualification @Activity='GetDataByCandidateID', @CandidateID = N'" + strCandidateID + "',@LanguageID='strLanguage'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}
          }
		
		public static DataRow GetDataByID(Object strID)
		{
			DataRow iRow = clsCommon.GetDataRow("RE_spfrmCandidateQualification @Activity='GetDataByID', @CandidateQualificationID = " + strID);
			if(iRow !=null)
				return iRow;
			else
				return null;
		}

		public static bool SaveData(string pstrSQL)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = pstrSQL;
			//-------
			try
			{
				cmd.ExecuteNonQuery();
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

		public static bool UpdateData(string pstrSQL)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = pstrSQL;
			//-------
			try
			{
				cmd.ExecuteNonQuery();
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
	}

	public class clsREDocument
	{
		/// <summary>
		/// Lay tat ca ho so cua ung vien
		/// </summary>
		/// <param name="strCandidateID"></param>
		/// <returns></returns>
		public static DataTable GetDataByCandidateID(Object strCandidateID)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("RE_spfrmCandidateDocument @Activity='GetDataByCandidateID', @CandidateID = N'" + strCandidateID + "'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}

		public static Boolean isInTableDocumentCandidate(string strDocumentID, DataTable tblDocumentCandidate, ref DataRow drRet)
		{
			drRet = null;
			foreach (DataRow dr in tblDocumentCandidate.Rows)
			{
				if ( strDocumentID == dr["LSDocumentID"].ToString() )
				{
					drRet = dr;
					return true;
				}
			}
			return false; // not in
		}

		/// <summary>
		/// Xoa tat ca ho so cua ung vien co ma la CandidateID
		/// </summary>
		/// <param name="strCandidateID">Ma ung vien</param>
		public static bool DeleteDocumentsByCandidateID(Object strCandidateID)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "RE_spfrmCandidateDocument";
			//-------
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.VarChar,50).Value = "Delete";
				cmd.Parameters.Add("@CandidateID",SqlDbType.NVarChar,12).Value = strCandidateID;
				cmd.ExecuteNonQuery();
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

		public static bool SaveDocument(string pstrSQL)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = pstrSQL;
			//-------
			try
			{
				cmd.ExecuteNonQuery();
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

		public static bool DeleteAttachFile(string pstrSQL)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = pstrSQL;
			//-------
			try
			{
				cmd.ExecuteNonQuery();
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
	}

	public class clsRECandidateLanguageID
	{
		/// <summary>
		/// Lay thong tin ky nang ung vien
		/// </summary>
		/// <param name="strEmpID">Ma so ung vien</param>
		/// <returns>DataTable</returns>
		public static DataTable GetSkillsByID(Object strCanID)
		{
			try
			{
				DataTable dt = clsCommon.GetDataTable("RE_spfrmLANGUAGE @Activity='GetDataSkillsByID',@CandidateID = N'" + strCanID + "'");
				return dt;
			}
			catch
			{
				return null;
			}
		}

		public static DataTable GetDataByCanID(Object strCanID,string strLanguage)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("RE_spfrmLANGUAGE @Activity='GetDataByCanID',@CandidateID = N'" + strCanID + "',@LanguageID=N'"+strLanguage+"'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}

		}

		public static bool CheckSave(Object strCanID,string sValue)
		{
			DataTable dt=new DataTable();
			try
			{
				dt=clsCommon.GetDataTable("RE_spfrmLANGUAGE @Activity='CheckSave',@CandidateID = N'" + strCanID + "',@Value='" + sValue + "'");
				if (dt.Rows.Count>0) 
				{
					return false;
				}
				else
				{
					return true;
				}
			}
			catch(Exception exp)
			{
				return false;
			}
			finally
			{
				dt.Dispose();
			}
		}

		public static bool UpdateSkills(string strCanID,string strSkillID)
		{			
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "RE_spfrmLANGUAGE";
			//------Delete current skill of employee
			cmd.Parameters.Clear();
			cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "DeleteSkills";
			cmd.Parameters.Add("@CandidateCode",SqlDbType.NVarChar,12).Value = strCanID.Trim();
			cmd.ExecuteNonQuery();
			//-------
			try
			{				
				//------------				
				string[] arrDocCode = strSkillID.Trim().Split('$');
				//--------------
				for(int i=0;i<arrDocCode.Length-1;i++)
				{
					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Skills";
					cmd.Parameters.Add("@CandidateCode",SqlDbType.NVarChar,12).Value = strCanID.Trim();
					cmd.Parameters.Add("@SkillID",SqlDbType.NVarChar,12).Value = arrDocCode.GetValue(i).ToString().Trim();
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

		public static DataRow GetDataByID(Object strID)
		{
			DataRow iRow = clsCommon.GetDataRow("RE_spfrmLANGUAGE @Activity='GetDataByID',@CandidateLanguageID = " + strID);
			if(iRow !=null)
				return iRow;
			else
				return null;
		}
	}
 }
