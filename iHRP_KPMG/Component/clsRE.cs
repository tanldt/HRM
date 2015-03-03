using System;
using System.Data;
using System.Web.UI.WebControls;
using iHRPCore.Com;
using System.Web;
using System.Web.Caching;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;
using System.Data.SqlClient;
using System.Configuration;

namespace iHRPCore.REComponent
{
	/// <summary>
	/// Summary description for clsRE.
	/// </summary>
	public class clsRECanList
	{
		public static DataTable GetEmpIDLength()
		{
			DataTable dtb = clsCommon.GetDataTable("RE_clsCommon 'GetCanIDLength'");
			return dtb;
		}
		public static DataTable GetCanList(string strCanID, System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			DataTable dtb = clsCommon.GetDataTable("RE_clsCommon 'GetCanList',@CandidateID=N'" + strCanID + "',@UserGroupID=N'" + sAccountLogin + "'");
			return dtb;
		}
	}
	public class clsJobDesc
	{
		public static DataTable Load_dtgMajor(string pJobTitleID)
		{
			DataTable dtb = new DataTable();
			try
			{
				 dtb= clsCommon.GetDataTable("RE_spfrmJobDescription @Activity='getMajor',@LSJobTitleID='" + pJobTitleID +  "'");
				return dtb;
			}
			catch
			{
				return null;
			}
			finally
			{
				dtb.Dispose();
			}
		}
		public static DataTable Load_dtgLanguage(string pJobTitleID)
		{
			DataTable dtb= new DataTable();
			try
			{
				dtb= clsCommon.GetDataTable("RE_spfrmJobDescription @Activity='getLanguage',@LSJobTitleID='" + pJobTitleID +  "'");
				return dtb;
			}
			catch
			{
				return null;
			}
			finally
			{
				dtb.Dispose();
			}
		}

		public static DataTable GetSkillsByID (string pJobTitleID)
		{
			DataTable dt= new DataTable();
			try
			{
				 dt = clsCommon.GetDataTable("RE_spfrmJobDescription @Activity='GetDataSkillsByID',@LSJobTitleID = N'" + pJobTitleID + "'");
				return dt;
			}
			catch
			{
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		public static DataTable GetJobDutyByID (string pJobTitleID)
		{
			DataTable dt= new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("RE_spfrmJobDescription @Activity='GetDataJobDutyByID',@LSJobTitleID = N'" + pJobTitleID + "'");
				return dt;
			}
			catch
			{
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		public static DataTable getDataAll()
		{
			DataTable dtData= new DataTable();
			try
			{
				dtData= clsCommon.GetDataTable("RE_spfrmJobDescription @Activity='getDataAll'");
				return dtData;
			}
			catch
			{
				return null;
			}
			finally
			{
				dtData.Dispose();
			}
		}
		public static DataRow getDataByID(string pJobTitleID)
		{
			DataRow drData;
			try
			{
				drData= clsCommon.GetDataRow("RE_spfrmJobDescription @Activity='getDataByID', @LSJobTitleID='" + pJobTitleID + "'");
				return drData;
			}
			catch
			{
				return null;
			}			
		}
		public static void sImpact_Major(string[] arrMajor, string[] arrQualifiMajor , string pJobTitleID)
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
				cmd.CommandText = "RE_spfrmJOBDESCRIPTION";
			
				for(int i=0;i<arrMajor.Length-1;i++)
				{
					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Save_major";				
					cmd.Parameters.Add("@LSJobTitleID",SqlDbType.NVarChar,12).Value = pJobTitleID;				
					cmd.Parameters.Add("@LSQualifiMajorID",SqlDbType.NVarChar,12).Value = arrQualifiMajor.GetValue(i).ToString();				
					cmd.Parameters.Add("@LSMajorLevelID",SqlDbType.NVarChar,12).Value = arrMajor.GetValue(i).ToString();				
					cmd.ExecuteNonQuery();
				}
				sqlTran.Commit();
			}
			catch(Exception ex)
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
		public static void sImpact_Language(string[] arrLanguage, string[] arrlanguageLevel , string pJobTitleID)
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
				cmd.CommandText = "RE_spfrmJOBDESCRIPTION";
			
				for(int i=0;i<arrLanguage.Length-1;i++)
				{
					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Save_language";				
					cmd.Parameters.Add("@LSJobTitleID",SqlDbType.NVarChar,12).Value = pJobTitleID;				
					cmd.Parameters.Add("@LSLanguageID",SqlDbType.NVarChar,12).Value = arrLanguage.GetValue(i).ToString();				
					cmd.Parameters.Add("@LSLanguageLevelID",SqlDbType.NVarChar,12).Value = arrlanguageLevel.GetValue(i).ToString();				
					cmd.ExecuteNonQuery();
				}
				sqlTran.Commit();
			}
			catch(Exception ex)
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
		public static void sImpact_Skill(System.Web.UI.WebControls.CheckBoxList chkSkill, string pJobTitleID)
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
				cmd.CommandText = "RE_spfrmJOBDESCRIPTION";
				for(int i=0;i<chkSkill.Items.Count;i++)
				{
					if(chkSkill.Items[i].Selected)
					{
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Save_Skill";				
						cmd.Parameters.Add("@LSJobTitleID",SqlDbType.NVarChar,12).Value = pJobTitleID;				
						cmd.Parameters.Add("@LSEmpSkillID",SqlDbType.NVarChar,12).Value = chkSkill.Items[i].Value;									
						cmd.ExecuteNonQuery();
					}
					
				}
				sqlTran.Commit();
			}
			catch(Exception ex)
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
		public static void sImpact_Duty(System.Web.UI.WebControls.CheckBoxList chkDuty, string pJobTitleID)
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
				cmd.CommandText = "RE_spfrmJOBDESCRIPTION";
				for(int i=0;i<chkDuty.Items.Count;i++)
				{
					if(chkDuty.Items[i].Selected)
					{
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Save_duty";				
						cmd.Parameters.Add("@LSJobTitleID",SqlDbType.NVarChar,12).Value = pJobTitleID;				
						cmd.Parameters.Add("@LSJobDutyID",SqlDbType.NVarChar,12).Value = chkDuty.Items[i].Value;									
						cmd.ExecuteNonQuery();
					}
					
				}
				sqlTran.Commit();
			}
			catch(Exception ex)
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
	}
	public class clsREDemand
	{
		
		public static string DemandApprove(string sActivity, string sRecruitAmount, string sApprover, string sApproveComment)
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
				cmd.CommandText = "RE_spfrmDemand";
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Save_major";				
				cmd.Parameters.Add("@DemandID",SqlDbType.NVarChar,12).Value = sActivity;				
				cmd.Parameters.Add("@LSQualifiMajorID",SqlDbType.NVarChar,12).Value = sApprover;				
				cmd.Parameters.Add("@LSMajorLevelID",SqlDbType.NVarChar,12).Value = sApproveComment;				
				cmd.ExecuteNonQuery();			
				sqlTran.Commit();
				return "";
			}
			catch
			{
				return "";
			}
		}
		public static DataTable GetDataSourceByID(string pDemandID)
		{
			DataTable dt= new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("RE_spfrmDemand @Activity='GetDataSourceByID',@DemandID = N'" + pDemandID + "'");
				return dt;
			}
			catch
			{
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		public static DataTable Load_dtgMajor(string pJobTitleID)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb= clsCommon.GetDataTable("RE_spfrmDemand @Activity='getMajor',@DemandID='" + pJobTitleID +  "'");
				return dtb;
			}
			catch
			{
				return null;
			}
			finally
			{
				dtb.Dispose();
			}
		}
		public static DataTable Load_dtgLanguage(string pJobTitleID)
		{
			DataTable dtb= new DataTable();
			try
			{
				dtb= clsCommon.GetDataTable("RE_spfrmDemand @Activity='getLanguage',@DemandID='" + pJobTitleID +  "'");
				return dtb;
			}
			catch
			{
				return null;
			}
			finally
			{
				dtb.Dispose();
			}
		}
		public static DataTable GetSkillsByID (string pDemandID)
		{
			DataTable dt= new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("RE_spfrmDemand @Activity='GetDataSkillsByID',@DemandID = N'" + pDemandID + "'");
				return dt;
			}
			catch
			{
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		
		public static void sImpact_Major(string[] arrMajor, string[] arrQualifiMajor , string pDemandID)
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
				cmd.CommandText = "RE_spfrmDemand";
			
				for(int i=0;i<arrMajor.Length-1;i++)
				{
					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Save_major";				
					cmd.Parameters.Add("@DemandID",SqlDbType.NVarChar,12).Value = pDemandID;				
					cmd.Parameters.Add("@LSQualifiMajorID",SqlDbType.NVarChar,12).Value = arrQualifiMajor.GetValue(i).ToString();				
					cmd.Parameters.Add("@LSMajorLevelID",SqlDbType.NVarChar,12).Value = arrMajor.GetValue(i).ToString();				
					cmd.ExecuteNonQuery();
				}
				sqlTran.Commit();
			}
			catch(Exception ex)
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
		public static void sImpact_Language(string[] arrLanguage, string[] arrlanguageLevel , string pDemandID)
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
				cmd.CommandText = "RE_spfrmDemand";
			
				for(int i=0;i<arrLanguage.Length-1;i++)
				{
					string sLanguage=arrLanguage.GetValue(i).ToString();				
					string sLanguageLevel=arrlanguageLevel.GetValue(i).ToString();
					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Save_language";				
					cmd.Parameters.Add("@DemandID",SqlDbType.NVarChar,12).Value = pDemandID;				
					cmd.Parameters.Add("@LSLanguageID",SqlDbType.NVarChar,12).Value = sLanguage;
					cmd.Parameters.Add("@LSLanguageLevelID",SqlDbType.NVarChar,12).Value = sLanguageLevel;			
					cmd.ExecuteNonQuery();
				}
				sqlTran.Commit();
			}
			catch(Exception ex)
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
		public static void sImpact_Skill(System.Web.UI.WebControls.CheckBoxList chkSkill, string pDemandID)
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
				cmd.CommandText = "RE_spfrmDemand";
				for(int i=0;i<chkSkill.Items.Count;i++)
				{
					if(chkSkill.Items[i].Selected)
					{
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Save_Skill";				
						cmd.Parameters.Add("@DemandID",SqlDbType.NVarChar,12).Value = pDemandID;				
						cmd.Parameters.Add("@LSEmpSkillID",SqlDbType.NVarChar,12).Value = chkSkill.Items[i].Value;									
						cmd.ExecuteNonQuery();
					}
					
				}
				sqlTran.Commit();
			}
			catch(Exception ex)
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
		public static void sImpact_Source(System.Web.UI.WebControls.CheckBoxList chkSource, string pDemandID)
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
				cmd.CommandText = "RE_spfrmDemand";
				for(int i=0;i<chkSource.Items.Count;i++)
				{
					if(chkSource.Items[i].Selected)
					{
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Save_Source";				
						cmd.Parameters.Add("@DemandID",SqlDbType.NVarChar,12).Value = pDemandID;				
						cmd.Parameters.Add("@LSRecruitSourceID",SqlDbType.NVarChar,12).Value = chkSource.Items[i].Value;									
						cmd.ExecuteNonQuery();
					}					
				}
				sqlTran.Commit();
			}
			catch(Exception ex)
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
		public static DataRow getDataByID(string pDemandID)
		{
			DataRow drData;
			try
			{
				drData= clsCommon.GetDataRow("RE_spfrmDemand @Activity='getDataByID', @DemandID='" + pDemandID + "'");
				return drData;
			}
			catch
			{
				return null;
			}			
		}
		public static DataRow getDataCopyByID(string pDemandID)
		{
			DataRow drData;
			try
			{
				drData= clsCommon.GetDataRow("RE_spfrmDemand @Activity='getDataCopyByID', @DemandID='" + pDemandID + "'");
				return drData;
			}
			catch
			{
				return null;
			}			
		}
		public static DataRow getApproveInfo(string pDemandID)
		{
			try
			{
				DataRow drData= clsCommon.GetDataRow("RE_spfrmDemand @Activity='getApproveInfo',@DemandID='" + pDemandID + "'");
				return drData;
			}
			catch
			{
				return null;
			}
		}

		public static bool CheckDemandExists(string sDemandID)
		{
			DataTable dtData= new DataTable();
			try
			{
				dtData= clsCommon.GetDataTable("RE_spfrmDemand @Activity='CheckDemandExists',@DemandID='" + sDemandID + "'");
				if(dtData.Rows.Count>0)
				{
					return false;
				}
				else
				{
					return true;
				}
			}
			catch
			{	
				return true;
			}
		}
	}
	public class clsREProject
	{
		public static DataRow getDataByID(string pProjectID)
		{
			DataRow drData;
			try
			{
				drData= clsCommon.GetDataRow("RE_spfrmProject @Activity='getDataByID', @ProjectID='" + pProjectID + "'");
				return drData;
			}
			catch
			{
				return null;
			}			
		}
		public static void sImpact_Source(System.Web.UI.WebControls.CheckBoxList chkSource, string pProjectID)
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
				cmd.CommandText = "RE_spfrmProject";
				for(int i=0;i<chkSource.Items.Count;i++)
				{
					if(chkSource.Items[i].Selected)
					{
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Save_Source";				
						cmd.Parameters.Add("@ProjectID",SqlDbType.NVarChar,12).Value = pProjectID;				
						cmd.Parameters.Add("@LSRecruitSourceID",SqlDbType.NVarChar,12).Value = chkSource.Items[i].Value;									
						cmd.ExecuteNonQuery();
					}					
				}
				sqlTran.Commit();
			}
			catch(Exception ex)
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
		public static DataTable GetDataSourceByID(string pProjectID)		{
			DataTable dt= new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("RE_spfrmProject @Activity='GetDataSourceByID',@ProjectID = N'" + pProjectID + "'");
				return dt;
			}
			catch
			{
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		
		public static string CloseProject(string pProjectID, System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
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
				cmd.CommandText = "RE_spfrmProject";				
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "CloseProject";				
				cmd.Parameters.Add("@ProjectID",SqlDbType.NVarChar,12).Value = pProjectID;				
				cmd.Parameters.Add("@AccountLogin",SqlDbType.NVarChar,12).Value = sAccountLogin;									
				cmd.ExecuteNonQuery();				
				sqlTran.Commit();				
				return "";
			}
			catch(Exception ex)
			{
				sqlTran.Rollback();
				return ex.ToString();
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();
			}			
		}
	}
	public class clsREProjectTournament
	{
		public static DataRow LoadProjectInfo(string pProjectID)
		{
			DataRow drData;
			try
			{
				drData= clsCommon.GetDataRow("RE_spfrmProjectTournament @Activity='getProjectInfo', @ProjectID='" + pProjectID + "'");
				return drData;
			}
			catch
			{
				return null;
			}
		}
		public static DataRow LoadDemandInfo(string pDemandID, string strLanguage)
		{			
			DataRow drData;
			try
			{
				drData= clsCommon.GetDataRow("RE_spfrmProjectTournament @Activity='getDemandInfo', @DemandID='" + pDemandID + "',@LanguageID='" + strLanguage + "'");
				return drData;
			}
			catch
			{
				return null;
			}
		}
		public static DataRow getDataByID(string pProjectTournamentID)
		{
			DataRow drData;
			try
			{
				drData= clsCommon.GetDataRow("RE_spfrmProjectTournament @Activity='getDataByID', @ProjectTournamentID='" + pProjectTournamentID + "'");
				return drData;
			}
			catch
			{
				return null;
			}
		}
		public static DataTable getDataByProjectID(string pProjectID)
		{
			DataTable dt= new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("RE_spfrmProjectTournament @Activity='getDataByProjectID',@ProjectID = N'" + pProjectID + "'");
				return dt;
			}
			catch
			{
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
	}
	public class clsREProjectDemand
	{
		public static DataTable getDataByProjectID(string pProjectID)
		{
			DataTable dt= new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("RE_spfrmDemandProject @Activity='getDataByProjectID',@ProjectID = N'" + pProjectID + "'");
				return dt;
			}
			catch
			{
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		public static DataRow getDataByID(string pDemandID, string pProjectID)
		{
			DataRow drData;
			try
			{
				drData= clsCommon.GetDataRow("RE_spfrmDemandProject @Activity='getDataByID', @DemandID='" + pDemandID + "', @ProjectID='" + pProjectID + "'");
				return drData;
			}
			catch
			{
				return null;
			}
		}
		public static DataRow getDataByDemandCode(string pDemandCode,string strLangID, System.Web.UI.Page pPage)
		{
			
			DataRow drData;
				try
				{
					string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
					drData=clsCommon.GetDataRow("RE_spfrmDemandProject @Activity='getDataByDemandCode', @DemandCode=N'" + pDemandCode + "',@UserGroupID='" + sAccountLogin + "'");
					return drData;
				}
				catch
				{
					return null;
				}
			
		}
	}
	public class clsREProjectTourSubject
	{
		public static DataTable getDataByProjectTour(string sProjectTourID)
		{
			DataTable dtData= new DataTable();
			try
			{
				dtData= clsCommon.GetDataTable("RE_spfrmProjectTourSubject @Activity='getDataByProjectTour', @ProjectTournamentID='" + sProjectTourID + "'");
				return dtData;
			}
			catch
			{
				return null;
			}
			finally
			{
				dtData.Dispose();
			}

		}
		public static DataRow getDataByID(string sLSRecruitSubjectID, string sProjectTournamentID)
		{
			try
			{
				DataRow drData= clsCommon.GetDataRow("RE_spfrmProjectTourSubject @Activity='getDataByID', @LSRecruitSubjectID='" + sLSRecruitSubjectID + "', @ProjectTournamentID='" + sProjectTournamentID + "'");
				return drData;
			}
			catch
			{
				return null;
			}
			
		}
	}
	public class clsREProjectTourInterview
	{
		public static string SaveInterView(DataGrid dtgList, string sProjectTournamentID)
		{
			string strErr = "";
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);		
			SQLconn.Open();
			cmd.Connection = SQLconn;
			cmd.Transaction= SQLconn.BeginTransaction();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "RE_spfrmProjectTourInterview";
			CheckBox chkSelect = new CheckBox();
			
			try
			{
				for(int i=0;i<dtgList.Items.Count;i++)
				{
					chkSelect = (CheckBox)dtgList.Items[i].FindControl("chkSelect");			
					// CHECK IF SELECTED
					if(chkSelect.Checked==true)
					{						
							cmd.Parameters.Clear();
							cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Save";							
							cmd.Parameters.Add("@ProjectTournamentID",SqlDbType.NVarChar,12).Value = sProjectTournamentID;
							cmd.Parameters.Add("@LSInterviewContentID",SqlDbType.NVarChar,12).Value = dtgList.Items[i].Cells[0].Text.Trim();							
							cmd.ExecuteNonQuery();
							
					}
				}
				cmd.Transaction.Commit();
				return strErr;
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
			}
			catch (Exception exp)
			{
				strErr = exp.Message;
				cmd.Transaction.Rollback();			
			}
			chkSelect.Dispose();
			return strErr;
		}
		public static DataTable getDataByProjectTournamentID(string sProjectTournamentID,string sLangID)
		{
			DataTable dt= new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("RE_spfrmProjectTourInterview @Activity='getDataByProjectTournamentID',@ProjectTournamentID = N'" + sProjectTournamentID + "',@LanguageID='" + sLangID + "'");
				return dt;
			}
			catch
			{
				return null;
			}
			finally
			{
				dt.Dispose();
			}

		}

		public static string DeleteInterView(string sProjectTournamentID)
		{
			string strErr = "";
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);		
			SQLconn.Open();
			cmd.Connection = SQLconn;
			cmd.Transaction= SQLconn.BeginTransaction();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "RE_spfrmProjectTourInterview";
			try
			{				
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Delete";							
				cmd.Parameters.Add("@ProjectTournamentID",SqlDbType.NVarChar,12).Value = sProjectTournamentID;			
				cmd.ExecuteNonQuery();
				cmd.Transaction.Commit();			
				return "";
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
			}
			catch (Exception ex)
			{
				return ex.ToString();
				cmd.Transaction.Rollback();	
			}
		}
		
	}

	public class REProject
	{
		public static string DeleteProjectList(string strProjectID)
		{
			string strErr = "";
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);		
			SQLconn.Open();
			cmd.Connection = SQLconn;
			cmd.Transaction= SQLconn.BeginTransaction();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "RE_spfrmProjectList";
			try
			{				
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Delete";							
				cmd.Parameters.Add("@ProjectID",SqlDbType.NVarChar,12).Value = strProjectID;			
				cmd.ExecuteNonQuery();
				cmd.Transaction.Commit();			
				return "";
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
			}
			catch (Exception ex)
			{
				return ex.ToString();
				cmd.Transaction.Rollback();	
			}
		}
		//public static DataTable getProjectList(string strDemandID,string strLSJobTitleID,string strApproved,string strCompanyID,string strLevel1,string strLevel2,string strLevel3,string strFApply,string strClosed,string strExpireDateTo,string strExpireDateFrom,string strEffectiveDateTo,string strEffectiveDateFrom,string strLangID)
		public static DataTable getProjectList(string strProjectCode,string strDemandID,string LSJobTitleID,string strCompanyID,string strLevel1,string strLevel2,string strLevel3,string strFApply,string strClosed,string strRecruitDateTo,string strRecruitDateFrom,string strRegisterFrom,string strRegisterTo,string strStartDateFrom,string strStartDateTo,string strLanguage)
		{
			DataTable dt= new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("RE_spfrmProjectList @Activity='Search',@ProjectCode =N'"+strProjectCode+"',@DemandID ='"+strDemandID+"',@LSJobTitleID ='"+LSJobTitleID+"',@FApply ='"+strFApply+"',@LSCompanyID = N'" + strCompanyID + "',@LSLevel1ID = N'" + strLevel1 + "',@LSLevel2ID = N'" + strLevel2 + "',@LSLevel3ID = N'" + strLevel3 + "',@Closed = '"+strClosed+"',@RecruitTo='"+strRecruitDateTo+"',@RecruitFrom='"+strRecruitDateFrom+"',@RegisterFrom='"+ strRegisterFrom+"',@RegisterTo='"+ strRegisterTo+"',@StartDateFrom='"+strStartDateFrom+"',@StartDateTo='"+strStartDateTo+"',@Language='"+strLanguage+"'");
				return dt;
			}
			catch
			{
				return null;
			}
			finally
			{
				dt.Dispose();
			}

		}
	}
	public class REDemand
	{
		public static DataTable getDemandList(string strDemandID,string strLSJobTitleID,string strApproved,string strCompanyID,string strLevel1,string strLevel2,string strLevel3,string strFApply,string strClosed,string strExpireDateTo,string strExpireDateFrom,string strEffectiveDateTo,string strEffectiveDateFrom,string strLangID)
		{
			DataTable dt= new DataTable();
			try
			{
				
				dt = clsCommon.GetDataTable("RE_spfrmDemandList @DemandID=N'"+strDemandID+"',@ExpiryTo='"+ strExpireDateTo +"',@ExpiryFrom='"+strExpireDateFrom+"',@EffectiveFrom ='"+strEffectiveDateFrom+"',@EffectiveTo ='"+strEffectiveDateTo+"',@LSJobTitleID = N'"+strLSJobTitleID+"',@Approved = '"+strApproved+"',@Closed = '"+strClosed+"',@FApply ='"+strFApply+"',@LSCompanyID = N'" + strCompanyID + "',@LSLevel1ID = N'" + strLevel1 + "',@LSLevel2ID = N'" + strLevel2 + "',@LSLevel3ID = N'" + strLevel3 + "',@LanguageID = N'" + strLangID + "'");
				return dt;
			}
			catch
			{
				return null;
			}
			finally
			{
				dt.Dispose();
			}

		}
		public static string DeleteDemand(string sDemandID)
		{
			string strErr = "";
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);		
			SQLconn.Open();
			cmd.Connection = SQLconn;
			cmd.Transaction= SQLconn.BeginTransaction();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "RE_spfrmDEMAND";
			try
			{				
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Delete";							
				cmd.Parameters.Add("@DemandID",SqlDbType.NVarChar,12).Value = sDemandID;			
				cmd.ExecuteNonQuery();
				cmd.Transaction.Commit();			
				return "";
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
			}
			catch (Exception ex)
			{
				return ex.ToString();
				cmd.Transaction.Rollback();	
			}
		}
	}
	public class REListCanPass
	{
		public static DataTable getResult(string strProjectCode,string strCandidateID,string strDemandID,string strLangID)
		{
			DataTable dt= new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("RE_spfrmResult @ProjectCode=N'" + strProjectCode + "', @CandidateID = N'" + strCandidateID + "',@DemandID = N'" + strDemandID + "',@LanguageID='" + strLangID + "'");
				return dt;
			}
			catch
			{
				return null;
			}
			finally
			{
				//dt.Dispose();
			}

		}
		public static string UpdateProjectCandidate(string strProjectID,string strCandidateID,int iResult)
		{
			string sErrMess ="";
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
				cmd.CommandText = "RE_spfrmListCanPass";
				
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Update";				
				cmd.Parameters.Add("@ProjectID",SqlDbType.NVarChar,12).Value = strProjectID;
				cmd.Parameters.Add("@CandidateID",SqlDbType.NVarChar,12).Value = strCandidateID;
				cmd.Parameters.Add("@Result",SqlDbType.TinyInt,1).Value = iResult;
				cmd.ExecuteNonQuery();
				sErrMess= "";
					
				
				sqlTran.Commit();
				if (sErrMess!="")
					return sErrMess;
				else
					return "";
			}
			catch(Exception ex)
			{
				string strErr = ex.Message;
				sqlTran.Rollback();
				return sErrMess;
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();
			}			
		}
		public static string UpdateCandidate(string strCandidateID,string strNote,int iStatus)
		{
			string strErr = "";
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);		
			SQLconn.Open();
			cmd.Connection = SQLconn;
			cmd.Transaction= SQLconn.BeginTransaction();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "RE_spfrmCandidate";
			try
			{				
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "UpdateCanStatus";							
				cmd.Parameters.Add("@CandidateID",SqlDbType.NVarChar,12).Value = strCandidateID;
				cmd.Parameters.Add("@Note",SqlDbType.NVarChar,255).Value = strNote;
				cmd.Parameters.Add("@Status",SqlDbType.TinyInt,12).Value = iStatus;
				cmd.ExecuteNonQuery();
				cmd.Transaction.Commit();			
				return "";
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
			}
			catch (Exception ex)
			{
				return ex.ToString();
				cmd.Transaction.Rollback();	
			}
		}
		public static DataTable getListCanPass(string strCandidateCode,string strCandidateName,string strProjectCode,string strDemainID,string strLanguage,string strStatusResult, string strExamineFrom, string strExamineTo)		
		{
			DataTable dt= new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("RE_spfrmListCanPass @Activity='SearchForSetFavour',@CandidateCode=N'" + strCandidateCode + 
											 "',@DemandID=N'" + strDemainID + 
											 "',@CandidateName = N'" + strCandidateName + 
											 "',@ProjectCode = N'" + strProjectCode + 
											 "',@StatusResult = N'" + strStatusResult + 
											 "',@ExamineFrom = N'" + strExamineFrom + 
											 "',@ExamineTo = N'" + strExamineTo + 
											 "',@LanguageID='" + strLanguage + "'");
				return dt;
			}
			catch
			{
				return null;
			}
			finally
			{
				dt.Dispose();
			}

		}

		public static DataTable getListCanFavor(string strCandidateCode,string strCandidateName,string strProjectCode,string strDemainID,string strLanguage)
			//public static DataTable getListCanPass()
		{
			DataTable dt= new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("RE_spfrmListCanPass @Activity='SearchForListFavour',@CandidateCode=N'" + strCandidateCode + "',@DemandID=N'"+strDemainID+"',@CandidateName=N'"+strCandidateName+"',@ProjectCode=N'"+strProjectCode+"',@LanguageID='"+strLanguage+"'");
				return dt;
			}
			catch
			{
				return null;
			}
			finally
			{
				dt.Dispose();
			}

		}
	}

	public class REListCanReject
	{
		public static string UpdateListCanReject(string strCandidateID,string strNote,int iStatus,int iDocumentUsed)
		{
			string strErr = "";
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);		
			SQLconn.Open();
			cmd.Connection = SQLconn;
			cmd.Transaction= SQLconn.BeginTransaction();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "RE_spfrmListCanReject";
			try
			{				
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "UpdateCanReject";							
				cmd.Parameters.Add("@CandidateID",SqlDbType.NVarChar,12).Value = strCandidateID;
				cmd.Parameters.Add("@Note",SqlDbType.NVarChar,255).Value = strNote;
				cmd.Parameters.Add("@Status",SqlDbType.TinyInt,12).Value = iStatus;
				cmd.Parameters.Add("@DocumentUsed",SqlDbType.TinyInt,12).Value = iDocumentUsed;
				cmd.ExecuteNonQuery();
				cmd.Transaction.Commit();			
				return "";
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
			}
			catch (Exception ex)
			{
				return ex.ToString();
				cmd.Transaction.Rollback();	
			}
		}
		public static DataTable getListCanReject(string strCandidateCode,string strCandidateName,string strProjectCode,string strDemainID)
			//public static DataTable getListCanPass()
		{
			DataTable dt= new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("RE_spfrmListCanReject @Activity='SearchForReject',@CandidateCode=N'" + strCandidateCode + "',@DemainID=N'"+strDemainID+"',@CandidateName=N'"+strCandidateName+"',@ProjectCode=N'"+strProjectCode+"'");
				return dt;
			}
			catch
			{
				return null;
			}
			finally
			{
				dt.Dispose();
			}

		}
	}
	public class REPlan
	{
		public static DataTable checkQuota(string strYYYY,string strCompanyID,string strLevel1,string strLevel2,string strLevel3,string strLangID)
		{
			DataTable dt= new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("RE_spfrmPLAN @Activity='CheckQuota',@YYYY='" + strYYYY + "', @LSCompanyID = N'" + strCompanyID + "',@LSLevel1ID = N'" + strLevel1 + "',@LSLevel2ID = N'" + strLevel2 + "',@LSLevel3ID = N'" + strLevel3 + "',@LanguageID='" + strLangID + "'");
				return dt;
			}
			catch
			{
				return null;
			}
			finally
			{
				//dt.Dispose();
			}

		}
		public static DataTable getPlan(string strYYYY,string strCompanyID,string strLevel1,string strLevel2,string strLevel3,string strLangID)
		{
			DataTable dt= new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("RE_spfrmPLAN @Activity='SearchPlan',@YYYY='" + strYYYY + "', @LSCompanyID = N'" + strCompanyID + "',@LSLevel1ID = N'" + strLevel1 + "',@LSLevel2ID = N'" + strLevel2 + "',@LSLevel3ID = N'" + strLevel3 + "',@LanguageID='" + strLangID + "'");
				return dt;
			}
			catch
			{
				return null;
			}
			finally
			{
				//dt.Dispose();
			}

		}
		public static DataTable getPlanJobTitle(string strYYYY,string strCompanyID,string strLevel1,string strLevel2,string strLevel3,string strLangID)
		{
			DataTable dt= new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("RE_spfrmPLAN @Activity='SearchPlanJobTitle',@YYYY='" + strYYYY + "', @LSCompanyID = N'" + strCompanyID + "',@LSLevel1ID = N'" + strLevel1 + "',@LSLevel2ID = N'" + strLevel2 + "',@LSLevel3ID = N'" + strLevel3 + "',@LanguageID='" + strLangID + "'");
				return dt;
			}
			catch
			{
				return null;
			}
			finally
			{
				//dt.Dispose();
			}

		}
		public static DataTable getPlanHistory(string strYYYY,string strCompanyID,string strLevel1,string strLevel2,string strLevel3,string strLangID)
		{
			DataTable dt= new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("RE_spfrmPLAN @Activity='SearchPlanHistory',@YYYY='" + strYYYY + "', @LSCompanyID = N'" + strCompanyID + "',@LSLevel1ID = N'" + strLevel1 + "',@LSLevel2ID = N'" + strLevel2 + "',@LSLevel3ID = N'" + strLevel3 + "',@LanguageID='" + strLangID + "'");
				return dt;
			}
			catch
			{
				return null;
			}
			finally
			{
				//dt.Dispose();
			}

		}
		public static string sImpact_Plan(string strYYYY,string strLSCompanyID, string strLSLevel1ID, string strLSLevel2ID,string strLSLevel3ID,string strFinishDate, string strExpectationCost,string strExceedQuota,string strRecruitAmount,string strPlanID)
		{
			string sErrMess ="";
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
				cmd.CommandText = "RE_spfrmPLAN";
				
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Save";				
						cmd.Parameters.Add("@YYYY",SqlDbType.NVarChar,4).Value = strYYYY;
						cmd.Parameters.Add("@FinishDate",SqlDbType.NVarChar,12).Value = strFinishDate;
						cmd.Parameters.Add("@ExpectationCost",SqlDbType.Int,32).Value = Convert.ToInt32(strExpectationCost);
						cmd.Parameters.Add("@ExceedQuota",SqlDbType.Bit,1).Value = Convert.ToInt32(strExceedQuota);
						cmd.Parameters.Add("@RecruitTotal",SqlDbType.SmallInt,12).Value = Convert.ToInt32(strRecruitAmount==""?0:Convert.ToInt32(strRecruitAmount));
						cmd.Parameters.Add("@PlanID",SqlDbType.NVarChar,12).Value = strPlanID;
						
						if (strLSCompanyID!= "") cmd.Parameters.Add("@LSCompanyID", SqlDbType.NVarChar, 12).Value = strLSCompanyID ;
						if (strLSLevel1ID!="") cmd.Parameters.Add("@LSLevel1ID", SqlDbType.NVarChar, 12).Value = strLSLevel1ID;
						if (strLSLevel2ID!="") cmd.Parameters.Add("@LSLevel2ID", SqlDbType.NVarChar, 12).Value = strLSLevel2ID;
						if (strLSLevel3ID!="") cmd.Parameters.Add("@LSLevel3ID", SqlDbType.NVarChar, 12).Value = strLSLevel3ID;
						cmd.Parameters.Add("@ReturnMess", SqlDbType.NVarChar,1000).Direction = ParameterDirection.InputOutput;
						cmd.ExecuteNonQuery();
						sErrMess= cmd.Parameters["@ReturnMess"].Value.ToString();
					
				
				sqlTran.Commit();
				if (sErrMess!="")
					return sErrMess;
				else
					return "";
			}
			catch(Exception ex)
			{
				string strErr = ex.Message;
				sqlTran.Rollback();
				return sErrMess;
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();
			}			
		}

		public static string sUpdate_PlanQuota(string  strPlanID,string strExceedQuota)
		{
			string sErrMess ="";
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
				cmd.CommandText = "RE_spfrmPLAN";
				
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "UpdateQuota";				
				cmd.Parameters.Add("@ExceedQuota",SqlDbType.Bit,1).Value = Convert.ToInt32(strExceedQuota);
				cmd.Parameters.Add("@PlanID",SqlDbType.NVarChar,12).Value = strPlanID;
					
				cmd.Parameters.Add("@ReturnMess", SqlDbType.NVarChar,1000).Direction = ParameterDirection.InputOutput;
				cmd.ExecuteNonQuery();
				sErrMess= cmd.Parameters["@ReturnMess"].Value.ToString();
					
				
				sqlTran.Commit();
				if (sErrMess!="")
					return sErrMess;
				else
					return "";
			}
			catch(Exception ex)
			{
				string strErr = ex.Message;
				sqlTran.Rollback();
				return sErrMess;
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();
			}			
		}

		public static string sImpact_PlanJobTitle(string LSJobTitleID,string PlanID,string strRecruitAmount0, string strRecruitAmount1,string strRecruitAmount)
		{
			string sErrMess ="";
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
				cmd.CommandText = "RE_spfrmPLANJOBTITLE";
				
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Save";				
				cmd.Parameters.Add("@PlanID", SqlDbType.NVarChar, 12).Value = PlanID ;
				cmd.Parameters.Add("@LSJobTitleID", SqlDbType.NVarChar, 12).Value = LSJobTitleID ;	
				cmd.Parameters.Add("@RecruitAmount0",SqlDbType.SmallInt,12).Value = Convert.ToInt32(strRecruitAmount0==""?"0":strRecruitAmount0);
				cmd.Parameters.Add("@RecruitAmount1",SqlDbType.SmallInt,12).Value = Convert.ToInt32(strRecruitAmount1==""?"0":strRecruitAmount1);
				cmd.Parameters.Add("@RecruitAmount",SqlDbType.SmallInt,12).Value = Convert.ToInt32(strRecruitAmount==""?"0":strRecruitAmount);
						
				cmd.Parameters.Add("@ReturnMess", SqlDbType.NVarChar,1000).Direction = ParameterDirection.InputOutput;
				cmd.ExecuteNonQuery();
				sErrMess= cmd.Parameters["@ReturnMess"].Value.ToString();
					
				
				sqlTran.Commit();
				if (sErrMess!="")
					return sErrMess;
				else
					return "";
			}
			catch(Exception ex)
			{
				string strErr = ex.Message;
				sqlTran.Rollback();
				return sErrMess;
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();
			}			
		}

		public static string sDelete_PlanJobTitle(string LSJobTitleID,string PlanID)
		{
			string sErrMess ="";
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
				cmd.CommandText = "RE_spfrmPLANJOBTITLE";
				
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Delete";				
				cmd.Parameters.Add("@PlanID", SqlDbType.NVarChar, 12).Value = PlanID ;
				cmd.Parameters.Add("@LSJobTitleID", SqlDbType.NVarChar, 12).Value = LSJobTitleID ;	
				
						
				cmd.Parameters.Add("@ReturnMess", SqlDbType.NVarChar,1000).Direction = ParameterDirection.InputOutput;
				cmd.ExecuteNonQuery();
				sErrMess= cmd.Parameters["@ReturnMess"].Value.ToString();
					
				
				sqlTran.Commit();
				if (sErrMess!="")
					return sErrMess;
				else
					return "";
			}
			catch(Exception ex)
			{
				string strErr = ex.Message;
				sqlTran.Rollback();
				return sErrMess;
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();
			}			
		}
	}
	public class REQuota
	{
		public static DataTable getReQuota(string strYYYY,string strCompanyID,string strLevel1,string strLevel2,string strLevel3,string strLangID)
		{
			DataTable dt= new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("RE_spfrmLABOURQUOTA @Activity='Search',@YYYY='" + strYYYY + "', @LSCompanyID = N'" + strCompanyID + "',@LSLevel1ID = N'" + strLevel1 + "',@LSLevel2ID = N'" + strLevel2 + "',@LSLevel3ID = N'" + strLevel3 + "',@LanguageID='" + strLangID + "'");
				return dt;
			}
			catch
			{
				return null;
			}
			finally
			{
				dt.Dispose();
			}

		}
		public static DataTable getQuotaHistory(string strYYYY,string strCompanyID,string strLevel1,string strLevel2,string strLevel3,string strLangID)
		{
			DataTable dt= new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("RE_spfrmLABOURQUOTA @Activity='QuotaHistory',@YYYY='" + strYYYY + "', @LSCompanyID = N'" + strCompanyID + "',@LSLevel1ID = N'" + strLevel1 + "',@LSLevel2ID = N'" + strLevel2 + "',@LSLevel3ID = N'" + strLevel3 + "',@LanguageID='" + strLangID + "'");
				return dt;
			}
			catch
			{
				return null;
			}
			finally
			{
				dt.Dispose();
			}

		}
		public static string sImpact_LabourQuota(string strYYYY,string strLSCompanyID, string strLSLevel1ID, string strLSLevel2ID,string strLSLevel3ID,string strQuotaTotal,string strLabourQuotaID)
		{
			string sErrMess ="";
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
				cmd.CommandText = "RE_spfrmLABOURQUOTA";
				
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Save";				
				cmd.Parameters.Add("@YYYY", SqlDbType.NVarChar, 50).Value = strYYYY;
				cmd.Parameters.Add("@VersionDate", SqlDbType.NVarChar, 50).Value = System.DateTime.Today.ToString("dd")+"/"+System.DateTime.Today.ToString("MM")+"/"+System.DateTime.Today.ToString("yyyy");
				cmd.Parameters.Add("@FApply", SqlDbType.TinyInt, 1).Value = "1";
				cmd.Parameters.Add("@QuotaTotal", SqlDbType.NVarChar,12).Value = strQuotaTotal;
				cmd.Parameters.Add("@LabourQuotaID", SqlDbType.VarChar,12).Value = strLabourQuotaID;
		
				if (strLSCompanyID!= "") cmd.Parameters.Add("@LSCompanyID", SqlDbType.NVarChar, 12).Value = strLSCompanyID ;
				if (strLSLevel1ID!="") cmd.Parameters.Add("@LSLevel1ID", SqlDbType.NVarChar, 12).Value = strLSLevel1ID;
				if (strLSLevel2ID!="") cmd.Parameters.Add("@LSLevel2ID", SqlDbType.NVarChar, 12).Value = strLSLevel2ID;
				if (strLSLevel3ID!="") cmd.Parameters.Add("@LSLevel3ID", SqlDbType.NVarChar, 12).Value = strLSLevel3ID;
				cmd.Parameters.Add("@ReturnMess", SqlDbType.NVarChar,1000).Direction = ParameterDirection.InputOutput;
				cmd.ExecuteNonQuery();
				sErrMess= cmd.Parameters["@ReturnMess"].Value.ToString();
					
				
				sqlTran.Commit();
				if (sErrMess!="")
					return sErrMess;
				else
					return "";
			}
			catch(Exception ex)
			{
				string strErr = ex.Message;
				sqlTran.Rollback();
				return sErrMess;
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();
			}	
		}

		public static string sImpact_LabourQuotaDetail(string LabourQuotaID,string LSJobTitleID,string Amount)
		{
			string sErrMess ="";
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
				cmd.CommandText = "RE_spfrmLABOURQUOTADETAIL";
				
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Save";				
				cmd.Parameters.Add("@LabourQuotaID", SqlDbType.NVarChar, 12).Value = LabourQuotaID ;
				cmd.Parameters.Add("@LSJobTitleID", SqlDbType.NVarChar, 12).Value = LSJobTitleID ;	
				cmd.Parameters.Add("@Amount",SqlDbType.SmallInt,12).Value = Convert.ToInt32(Amount);
						
				cmd.Parameters.Add("@ReturnMess", SqlDbType.NVarChar,1000).Direction = ParameterDirection.InputOutput;
				cmd.ExecuteNonQuery();
				sErrMess= cmd.Parameters["@ReturnMess"].Value.ToString();
					
				
				sqlTran.Commit();
				if (sErrMess!="")
					return sErrMess;
				else
					return "";
			}
			catch(Exception ex)
			{
				string strErr = ex.Message;
				sqlTran.Rollback();
				return sErrMess;
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();
			}			
		}
	}
	
}

