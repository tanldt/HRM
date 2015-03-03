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
namespace iHRPCore.HRComponent
{
	using iHRPCore.Com;
	/// <summary>
	/// Summary description for clsHRTempThanh.
	/// </summary>
	/// 
	///
	public class clsLevel2
	{
		public static DataTable GetLevel1(string strLanguage)
		{
			DataTable dtb=clsCommon.GetDataTable("LS_spfrmLevel2 @Activity='GetLevelParent',@Language='" + strLanguage + "'");
			return dtb;
		}
		public static string getStrCodeName(string strCodeList)
		{
			try
			{
				DataRow drData = clsCommon.GetDataRow("LS_spfrmLevel2 @Activity='getcodeName',@strCodeName=N'" + strCodeList + "'");	
				return drData["strCodeName"].ToString();
			}
			catch(Exception ex)
			{
				return ex.ToString();
			}

		}
		public static DataTable GetLevel2()
		{
			DataTable dtb=clsCommon.GetDataTable("LS_spfrmLevel2 @Activity='GetLevel'");
			return dtb;
		}
		public static DataTable SearchBy(string sLevel2Code,string sName, string sVNName, string sShortName, string sNote, string sRank, string sLSLevel1ID,string sUsed)
		{
			DataTable dtb =clsCommon.GetDataTable("LS_spfrmLevel2 @Activity='SearchBy',@LSLevel2Code='" + sLevel2Code + "', @Name='" + sName + "', @VNName='" + sVNName + "',@ShortName='" + sShortName + "',@Rank='" + sRank + "',@Note='" + sNote + "', @Used='" + sUsed + "', @LSLevel1ID='" + sLSLevel1ID + "'");
			return dtb;
		}
		public static DataTable GetDataFromItem(string strLevel2ID)
		{
			DataTable dtb=clsCommon.GetDataTable("LS_spfrmLevel2 @Activity='GetDataFromItem',@LSLevel2ID=N'" + strLevel2ID + "'");  
			return dtb;
		}
		//cangtt 03012006
		public static DataTable GetDataFromItem_popup(string strLevel2ID,string strLanguage)
		{
			DataTable dtb=clsCommon.GetDataTable("LS_spfrmLevel2 @Activity='GetDataFromItem_popup',@LSLevel2ID=N'" + strLevel2ID + "',@Language='" + strLanguage + "'");
			return dtb;
		}
		public static DataTable GetDataFromItem_LevelSetup(string strLevel2ID,string strLanguage)
		{
			DataTable dtb=clsCommon.GetDataTable("LS_spfrmLevel2 @Activity='GetDataFromItem_LevelSetup',@LSLevel2ID=N'" + strLevel2ID + "',@Language='" + strLanguage + "'");
			return dtb;
		}
		public static string SaveLevel(string strList,string strLevel2ID,string strLevel2,string strShortName,string strName,string strVNName,string strNote,int Used,int iRank)
		{
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "LS_spfrmLevel2";				
				string[] arrList;

				arrList=strList.Trim().Split(',');
				
				for (int i = 0;i<= arrList.Length - 1;i++)
				{		
					string sLSLevel1Code=arrList.GetValue(i).ToString();
					if (! sLSLevel1Code.Equals(""))
					{
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity", SqlDbType.NVarChar,50).Value = "Save";		
						cmd.Parameters.Add("@LSLevel2ID", SqlDbType.NVarChar, 12).Value = strLevel2ID;
						cmd.Parameters.Add("@LSLevel2Code", SqlDbType.NVarChar, 12).Value = strLevel2;
						cmd.Parameters.Add("@ShortName", SqlDbType.NVarChar, 30).Value = strShortName;						
						cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = strName; 
						cmd.Parameters.Add("@VNName",SqlDbType.NVarChar,50).Value= strVNName; 
						cmd.Parameters.Add("@Note", SqlDbType.NVarChar,100).Value = strNote; 
						cmd.Parameters.Add("@Used",SqlDbType.Bit).Value=Used; 
						cmd.Parameters.Add("@Rank",SqlDbType.Int).Value=iRank; 
						cmd.Parameters.Add("@LSlevel1ID",SqlDbType.NVarChar,12).Value=sLSLevel1Code;   
						cmd.ExecuteNonQuery();																
					}
				}
				
				cmd.Transaction.Commit();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();	
				return "";
			}
			catch  (Exception exp)
			{								
				cmd.Transaction.Rollback();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				return exp.Message.Trim();
			}
		}

		public static string DeleteLevel(string strLevel2)
		{
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "LS_spfrmLevel2";
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity", SqlDbType.NVarChar,50).Value = "Delete";						
				cmd.Parameters.Add("@LSLevel2ID", SqlDbType.NVarChar, 12).Value = strLevel2;
				cmd.ExecuteNonQuery();											
				cmd.Transaction.Commit();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();	
				return "";
			}
			catch  (Exception exp)
			{	
				cmd.Transaction.Rollback();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				return exp.Message.Trim();
			}
		}

		// CODE THE TAI 
		// DELETE TO UPDATE FOR STORE PROCEDURE
		public static string DeleteUpdateLevel(string strLevel2)
		{
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "LS_spfrmLevel2";
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity", SqlDbType.NVarChar,50).Value = "DeleteUpdate";						
				cmd.Parameters.Add("@LSLevel2ID", SqlDbType.NVarChar, 12).Value = strLevel2;
				cmd.ExecuteNonQuery();											
				cmd.Transaction.Commit();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();	
				return "";
			}
			catch  (Exception exp)
			{								
				cmd.Transaction.Rollback();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				return exp.Message.Trim();
			}
		}

		// END CODE THE TAI
	}


	public class clsLevel3
	{
		public static DataTable GetLevel2(string strLanguage)
		{
			DataTable dtb=clsCommon.GetDataTable("LS_spfrmLevel3 @Activity='GetLevelParent',@Language='" + strLanguage + "'");
			return dtb;
		}
		public static string getStrCodeName(string strCodeList)
		{
			try
			{
				DataRow drData = clsCommon.GetDataRow("LS_spfrmLevel3 @Activity='getcodeName',@strCodeName=N'" + strCodeList + "'");
				return drData["strCodeName"].ToString();
			}
			catch(Exception ex)
			{
				return ex.ToString();
			}

		}
		public static DataTable SearchBy(string sLevel3Code,string sName, string sVNName, string sShortName, string sNote, string sRank, string sLSLevel2ID,string sUsed)
		{
			DataTable dtb =clsCommon.GetDataTable("LS_spfrmLevel3 @Activity='SearchBy',@LSLevel3Code='" + sLevel3Code + "', @Name='" + sName + "', @VNName='" + sVNName + "',@ShortName='" + sShortName + "',@Rank='" + sRank + "',@Note='" + sNote + "', @Used='" + sUsed + "', @LSLevel2ID='" + sLSLevel2ID + "'");
			return dtb;
		}
		public static DataTable GetLevel3()
		{
			DataTable dtb=clsCommon.GetDataTable("LS_spfrmLevel3 @Activity='GetLevel'");
			return dtb;
		}
		public static DataTable GetDataFromItem(string strLevel3ID)
		{
			DataTable dtb=clsCommon.GetDataTable("LS_spfrmLevel3 @Activity='GetDataFromItem',@LSLevel3ID=N'" + strLevel3ID + "'");  
			return dtb;
		}		
		//cangtt 03012006
		public static DataTable GetDataFromItem_popup(string strLevel2ID,string strLanguage)
		{
			DataTable dtb=clsCommon.GetDataTable("LS_spfrmLevel3 @Activity='GetDataFromItem_popup',@LSLevel3ID=N'" + strLevel2ID + "',@Language='" + strLanguage + "'");
			return dtb;
		}
		
		public static DataTable GetDataFromItem_LevelSetup(string LevelID,string strLanguage)
		{
			DataTable dtb=clsCommon.GetDataTable("LS_spfrmLevel3 @Activity='GetDataFromItem_LevelSetup',@LSLevel3ID=N'" + LevelID + "',@Language='" + strLanguage + "'");
			return dtb;	
		}
		public static DataTable getComboParent(string LevelID,string strLanguage)
		{
			DataTable dtb=clsCommon.GetDataTable("LS_spfrmLevel3 @Activity='get_combo',@LSLevel3ID=N'" + LevelID + "',@Language='" + strLanguage + "'");
			return dtb;
		}
		public static string SaveLevel(string strList,String strLevel3ID,string strLevel3,string strName,string strVNName,string strNote,int Used, string strShortName,int iRank, string strLevel3IDParent)
		{
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "LS_spfrmLevel3";
				string[] arrList;

				arrList=strList.Trim().Split(',');
				for (int i = 0;i<= arrList.Length - 1;i++)
				{
					string sLSLevel2Code=arrList.GetValue(i).ToString();
					if (! sLSLevel2Code.Equals(""))
					{
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity", SqlDbType.NVarChar,50).Value = "Save";						
						cmd.Parameters.Add("@LSLevel3ID", SqlDbType.NVarChar, 12).Value = strLevel3ID;
						cmd.Parameters.Add("@LSLevel3Code", SqlDbType.NVarChar, 12).Value = strLevel3;
						cmd.Parameters.Add("@ShortName", SqlDbType.NVarChar, 12).Value = strShortName;						
						cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = strName; 
						cmd.Parameters.Add("@VNName",SqlDbType.NVarChar,50).Value= strVNName; 
						cmd.Parameters.Add("@Note", SqlDbType.NVarChar,100).Value = strNote; 
						cmd.Parameters.Add("@Used",SqlDbType.Bit).Value=Used; 
						cmd.Parameters.Add("@Rank",SqlDbType.Int).Value=iRank; 
						cmd.Parameters.Add("@Level3IDParent", SqlDbType.NVarChar, 12).Value = strLevel3IDParent;
						cmd.Parameters.Add("@LSlevel2ID",SqlDbType.NVarChar,12).Value=sLSLevel2Code.Trim();   
						cmd.ExecuteNonQuery();											
					}
					
				}				
				cmd.Transaction.Commit();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();	
				return "";
			}
			catch  (Exception exp)
			{								
				cmd.Transaction.Rollback();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				return exp.Message.Trim();
			}
		}

		public static string DeleteLevel(string strLevel3)
		{
			string sErrMess="";
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "LS_spfrmLevel3";
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity", SqlDbType.NVarChar,50).Value = "Delete";						
				cmd.Parameters.Add("@LSLevel3ID", SqlDbType.NVarChar, 12).Value = strLevel3;
				cmd.Parameters.Add("@ReturnMess", SqlDbType.NVarChar,1000).Direction = ParameterDirection.InputOutput;
				cmd.ExecuteNonQuery();
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
			catch  (Exception exp)
			{								
				cmd.Transaction.Rollback();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				return exp.Message.Trim();
			}
		}
		// CODE THE TAI
		public static string DeleteUpdateLevel(string strLevel3)
		{
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "LS_spfrmLevel3";
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity", SqlDbType.NVarChar,50).Value = "DeleteUpdate";						
				cmd.Parameters.Add("@LSLevel3ID", SqlDbType.NVarChar, 12).Value = strLevel3;
				cmd.ExecuteNonQuery();											
				cmd.Transaction.Commit();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();	
				return "";
			}
			catch  (Exception exp)
			{								
				cmd.Transaction.Rollback();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				return exp.Message.Trim();
			}
		}

		// END CODE THE TAI

	}

	public class clsHRPersonal
	{
		/// <summary>
		/// Lay thong tin nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataRow</returns>
		public static DataRow GetDataByID(Object strEmpID)
		{
			try
			{
				DataRow iRow = clsCommon.GetDataRow("HR_spfrmEmpCV @Activity='GetDataByID',@EmpID = N'" + strEmpID + "'");
				if(iRow !=null)
					return iRow;
				else
					return null;
			}
			catch
			{
				return null;
			}
		}
		public static bool CheckIdentityCard(Object strEmpID,string strCardID)
		{
			try
			{
				DataRow iRow = clsCommon.GetDataRow("HR_spfrmEmpCV @Activity='CheckIdentityCard',@EmpID = N'" + strEmpID + "',@IDNo=N'" + strCardID.Trim() + "'");
				if(iRow !=null)
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
		/// Xay dung ma nhan vien
		/// </summary>
		/// <param name="strCompanyCode">Ma Cty</param>
		/// <param name="strSrartDate">Ngay vao Cty</param>
		/// <returns></returns>
		public static string BuildEmpCode(string strCompanyCode,string strSrartDate)
		{
			DataRow iRow = clsCommon.GetDataRow("HR_spfrmBuildEmp @Activity=N'BuildEmpCode',@LSCompanyID=N'" + strCompanyCode + "',@StartDate=N'" + strSrartDate.Trim() + "'");
			if(iRow != null)
				return iRow["EmpCode"].ToString().Trim();
			else
				return "";
		}
		public static DataRow GetDataEmpCodeID(Object strEmpID)
		{
			DataRow iRow = clsCommon.GetDataRow("HR_spfrmBuildEmp @Activity=N'GetDataByID',@EmpID=N'" + strEmpID + "'");
			if(iRow !=null)
				return iRow;
			else
				return null;
		}
	}
	/// <summary>
	/// Class danh cho Form PersonalRecord
	/// </summary>
	public class clsHRPersonalRecord
	{
		/// <summary>
		/// Lay thong tin nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataRow</returns>
		public static DataRow GetDataByID(Object strEmpID)
		{
			DataRow iRow = clsCommon.GetDataRow("HR_spfrmEmp @Activity='GetDataByID',@EmpID = N'" + strEmpID + "'");
			if(iRow !=null)
				return iRow;
			else
				return null;
		}
		public static DataRow GetPersonalRecordByEmpID (Object strEmpID)
		{
			DataRow iRow = clsCommon.GetDataRow("HR_spfrmEmp @Activity='GetPersonalRecordByEmpID',@EmpID = N'" + strEmpID + "'");
			if(iRow !=null)
				return iRow;
			else
				return null;
		}

		
		public static DataRow CheckValidData(Object strEmpID)
		{
			DataRow iRow = clsCommon.GetDataRow("HR_spfrmEmp @Activity='GetDataByID',@EmpID = N'" + strEmpID + "'");
			if(iRow !=null)
				return iRow;
			else
				return null;
		}
	}
	public class clsHROtherInfo
	{
		/// <summary>
		/// Lay thong tin nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataRow</returns>
		public static DataRow GetDataByID(Object strEmpID)
		{
			try
			{
				DataRow iRow = clsCommon.GetDataRow("HR_spfrmEmpOther @Activity='GetDataByID',@EmpID = N'" + strEmpID + "'");
				if(iRow !=null)
					return iRow;
				else
					return null;
			}
			catch
			{
				return null;
			}
		}
		/// <summary>
		/// Lay thong tin ho so nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataTable</returns>
		public static DataTable GetDocumentsByID(Object strEmpID)
		{
			try
			{
				DataTable dt = clsCommon.GetDataTable("HR_spfrmEmpOther @Activity='GetDataDocumentsByID',@EmpID = N'" + strEmpID + "'");
				return dt;
			}
			catch
			{
				return null;
			}
		}
		/// <summary>
		/// Cap nhat ho so cho nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma nhan vien</param>
		/// <param name="strDocumentsCode">Danh sach cac ho so</param>
		/// <returns>True/false</returns>
		public static bool UpdateDocuments(string strEmpID,string strDocumentsCode)
		{			
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "HR_spfrmEmpOther";
			//------Delete current document of employee
			cmd.Parameters.Clear();
			cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "DeleteDocuments";
			cmd.Parameters.Add("@EmpID",SqlDbType.NVarChar,12).Value = strEmpID.Trim();
			cmd.ExecuteNonQuery();
			//-------
			try
			{				
				//------------				
				string[] arrDocCode = strDocumentsCode.Trim().Split('$');
				//--------------
				for(int i=0;i<arrDocCode.Length-1;i++)
				{
					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Documents";
					cmd.Parameters.Add("@EmpID",SqlDbType.NVarChar,12).Value = strEmpID.Trim();
					cmd.Parameters.Add("@DocumentCode",SqlDbType.NVarChar,12).Value = arrDocCode.GetValue(i).ToString().Trim();
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

	}
	public class clsHRDiscipline
	{
		public static string DeleteFileAttach(string strID)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "HR_spfrmAWARDRECORD";
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "DeleteFileAttach";
				cmd.Parameters.Add("@ID",SqlDbType.NVarChar,12).Value=strID;
				cmd.ExecuteNonQuery();
				sqlTran.Commit();
				return "";
			}
			catch (Exception exp)
			{
				string strErr = exp.Message;
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
		/// <summary>
		/// Lay thong tin qua trinh lam viec cua nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataTable</returns>
		public static DataTable GetDataByEmpID(Object strEmpID, string sLanguage)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("HR_spfrmAWARDRECORD @Activity='GetDataByEmpID',@EmpID = N'" + strEmpID + "',@LanguageID='" + sLanguage + "'");
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
		/// <summary>
		/// Lay thong tin qua trinh lam viec cua nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataRow</returns>
		public static DataRow GetDataByIDAward(Object strID)
		{
			DataRow iRow = clsCommon.GetDataRow("HR_spfrmAWARDRECORD @Activity='GetDataByID',@AwardRecordID = '" + strID + "'");
			if(iRow !=null)
				return iRow;
			else
				return null;
		}
		public static DataRow GetDataByIDDiscipline(Object strID)
		{
			DataRow iRow = clsCommon.GetDataRow("HR_spfrmDisciplineRecord @Activity='GetDataByID',@DisciplineRecordID = '" + strID + "'");
			if(iRow !=null)
				return iRow;
			else
				return null;
		}
	}
	/// <summary>
	/// Class danh cho Form Working Record
	/// </summary>
	public class clsHRWorkingRecord
	{
		public static bool CheckValidEffectiveDate(Object strID,string strEffDate,string strWorkingID,string strActivity)
		{			
			if(strActivity=="AddNew")
			{
				DataRow iRow = clsCommon.GetDataRow("HR_spfrmWorkingRecord @Activity='CheckValidSave',@EmpID = N'" + strID + "',@FromDate='" + strEffDate + "'");
				if(iRow != null)
					return false; 
				else
					return true;
			}
			else
			{
				DataRow iRow = clsCommon.GetDataRow("HR_spfrmWorkingRecord @Activity='CheckValidUpdate',@EmpID = N'" + strID + "',@FromDate='" + strEffDate + "',@WorkingRecordID='" + strWorkingID+"'");
				if(iRow != null)
					return false; 
				else
					return true;
			}
			return false;
		}
		public static DataRow GetCurrentInfo(Object strID)
		{			
			DataRow iRow = clsCommon.GetDataRow("HR_spfrmWorkingRecord @Activity='GetDataCurrentInfo',@EmpID = N'" + strID + "'");
			if(iRow !=null)
				return iRow;
			else
				return null;			
		}
		public static string SaveData(string strEmpID,string strStatusID,string strFromDate,string strComID,string strLevel1ID,string strLevel2ID
			,string strLevel3ID,string strLocationID,string strPosID,string strLevelGradeID,string strRankID,string strJobCodeID,string strDecNo,string strSignDate,string strNote
			,string strSigner, string strSignerPosition,string strJobTitle,string strEmpType,int decision,string strAttachFile,string strWorkingRecord,string strActivity,string strLeaveLevel, string strDialogue)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "HR_spfrmWorkingRecord";
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = strActivity;
				cmd.Parameters.Add("@WorkingRecordID",SqlDbType.NVarChar,12).Value=strWorkingRecord;
				cmd.Parameters.Add("@EmpID",SqlDbType.NVarChar,15).Value = strEmpID.Trim();
				cmd.Parameters.Add("@LSStatusChangeID",SqlDbType.NVarChar,12).Value = strStatusID.Trim();
				cmd.Parameters.Add("@decision",SqlDbType.Bit).Value=decision;
				if(strFromDate.Trim()=="")
					cmd.Parameters.Add("@FromDate",SqlDbType.NVarChar,12).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@FromDate",SqlDbType.NVarChar,12).Value = strFromDate.Trim();
				if(strComID.Trim()=="")
					cmd.Parameters.Add("@LSCompanyID",SqlDbType.NVarChar,12).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@LSCompanyID",SqlDbType.NVarChar,12).Value = strComID.Trim();
				if(strLevel1ID.Trim()=="")
					cmd.Parameters.Add("@LSLevel1ID",SqlDbType.NVarChar,12).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@LSLevel1ID",SqlDbType.NVarChar,12).Value = strLevel1ID.Trim();
				if(strLevel2ID.Trim()=="")
					cmd.Parameters.Add("@LSLevel2ID",SqlDbType.NVarChar,12).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@LSLevel2ID",SqlDbType.NVarChar,12).Value = strLevel2ID.Trim();				
				if(strLevel3ID.Trim()=="")
					cmd.Parameters.Add("@LSLevel3ID",SqlDbType.NVarChar,12).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@LSLevel3ID",SqlDbType.NVarChar,12).Value = strLevel3ID.Trim();				
				if(strLocationID.Trim()=="")
					cmd.Parameters.Add("@LSLocationID",SqlDbType.NVarChar,12).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@LSLocationID",SqlDbType.NVarChar,12).Value = strLocationID.Trim();
				if(strPosID.Trim()=="")
					cmd.Parameters.Add("@LSPositionID",SqlDbType.NVarChar,12).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@LSPositionID",SqlDbType.NVarChar,12).Value = strPosID.Trim();				
				if(strLevelGradeID.Trim()=="")
					cmd.Parameters.Add("@LSGradeID",SqlDbType.NVarChar,12).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@LSGradeID",SqlDbType.NVarChar,12).Value = strLevelGradeID.Trim();				
				if(strRankID.Trim()=="")
					cmd.Parameters.Add("@LSRankID",SqlDbType.NVarChar,12).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@LSRankID",SqlDbType.NVarChar,12).Value = strRankID.Trim();				
				if(strJobCodeID.Trim()=="")
					cmd.Parameters.Add("@LSJobCodeID",SqlDbType.NVarChar,12).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@LSJobCodeID",SqlDbType.NVarChar,12).Value = strJobCodeID.Trim();				
				if(strDecNo.Trim()=="")
					cmd.Parameters.Add("@DecisionNo",SqlDbType.NVarChar,20).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@DecisionNo",SqlDbType.NVarChar,20).Value = strDecNo.Trim();
				if(strSignDate.Trim()=="")
					cmd.Parameters.Add("@SignDate",SqlDbType.NVarChar,12).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@SignDate",SqlDbType.NVarChar,12).Value = strSignDate.Trim();
				if (strSigner.Trim()=="")
					cmd.Parameters.Add("@Signer",SqlDbType.NVarChar,50).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@Signer",SqlDbType.NVarChar,50).Value = strSigner.Trim();
				if (strSignerPosition.Trim()=="")
					cmd.Parameters.Add("@SignerPosition",SqlDbType.NVarChar,100).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@SignerPosition",SqlDbType.NVarChar,100).Value = strSignerPosition.Trim();

				if (strEmpType.Trim()=="")
					cmd.Parameters.Add("@LSEmpTypeID",SqlDbType.NVarChar,12).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@LSEmpTypeID",SqlDbType.NVarChar,12).Value = strEmpType.Trim();

				if (strLeaveLevel.Trim()=="")
					cmd.Parameters.Add("@LSLeaveLevelID",SqlDbType.NVarChar,12).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@LSLeaveLevelID",SqlDbType.NVarChar,12).Value = strLeaveLevel.Trim();

				if (strJobTitle.Trim()=="")
					cmd.Parameters.Add("@LSJobTitleID",SqlDbType.NVarChar,12).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@LSJobTitleID",SqlDbType.NVarChar,12).Value = strJobTitle.Trim();
				

				if(strNote.Trim()=="")
					cmd.Parameters.Add("@Note",SqlDbType.NVarChar,255).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@Note",SqlDbType.NVarChar,255).Value = strNote.Trim();				

				if (strAttachFile.Trim()=="")
					cmd.Parameters.Add("@AttachFile",SqlDbType.NVarChar,255).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@AttachFile",SqlDbType.NVarChar,255).Value = strAttachFile.Trim();

				if(strDialogue.Trim()=="")
					cmd.Parameters.Add("@DialogueRating",SqlDbType.NVarChar,50).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@DialogueRating",SqlDbType.NVarChar,50).Value = strDialogue.Trim();	

				cmd.ExecuteNonQuery();
				sqlTran.Commit();
				return "";
			}
			catch (Exception exp)
			{
				string strErr = exp.Message;
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
		/// <summary>
		/// Lay thong tin qua trinh lam viec cua nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataTable</returns>
		public static DataTable GetDataByEmpID(Object strEmpID)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("HR_spfrmWorkingRecord @Activity='GetDataByEmpID',@EmpID = N'" + strEmpID + "'");
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
		/// <summary>
		/// Lay thong tin qua trinh lam viec cua nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataRow</returns>
		public static DataRow GetDataByID(Object strID)
		{
			DataRow iRow = clsCommon.GetDataRow("HR_spfrmWorkingRecord @Activity='GetDataByID',@WorkingRecordID = '" + strID + "'");
			if(iRow !=null)
				return iRow;
			else
				return null;
		}
		public static string ImportSalary(string pstrFilename)
		{			
			string mstr_FileName = pstrFilename;
			if (!File.Exists(pstrFilename))
			{
				return "File not found, Please check path of the filename again!";
			}			
			string mstr_PathFileName = mstr_FileName;
			//------------------

			string strConn;
			strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + mstr_PathFileName + ";Extended Properties=Excel 8.0;";			
			OleDbDataAdapter myCommand = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", strConn);
			DataSet myDataSet = new DataSet();
			myCommand.Fill(myDataSet, "ExcelData");
			//DataGrid1.DataSource = myDataSet.Tables["ExcelData"];
			//DataGrid1.DataBind();
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();			
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "HR_spfrmWorkingRecord";
			try
			{
				for(int i=0;i<myDataSet.Tables["ExcelData"].Rows.Count;i++)
				{
					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 50).Value = "ImportSalary";
					cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar,12).Value = myDataSet.Tables["ExcelData"].Rows[i][0].ToString().Trim();
					cmd.Parameters.Add("@FromDate", SqlDbType.NVarChar,12).Value = myDataSet.Tables["ExcelData"].Rows[i][2].ToString().Trim();
					cmd.Parameters.Add("@BasicSalary", SqlDbType.Money).Value = myDataSet.Tables["ExcelData"].Rows[i][1].ToString().Trim().Replace(",","");
					cmd.ExecuteNonQuery();
				}
				if (sqlTran != null ) sqlTran.Commit();
				myCommand.Dispose();				
				myDataSet.Dispose();
				return "";
			}
			catch(Exception exp)
			{
				if (sqlTran != null) sqlTran.Rollback();
				myCommand.Dispose();
				myDataSet.Dispose();
				return exp.Message;
			}
		}
	}
	public class clsHRProjectParticipation
	{
		/// <summary>
		/// Lay thong tin qua trinh lam viec cua nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataTable</returns>
		public static DataTable GetDataByEmpID(Object strEmpID)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("HR_spfrmPROJECTPARTICIPATION @Activity='GetDataByEmpID',@EmpID = N'" + strEmpID + "'");
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
		/// <summary>
		/// Lay thong tin qua trinh lam viec cua nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataRow</returns>
		public static DataRow GetDataByID(Object strID)
		{
			DataRow iRow = clsCommon.GetDataRow("HR_spfrmPROJECTPARTICIPATION @Activity='GetDataByID',@LSProjectParticipationID = '" + strID + "'");
			if(iRow !=null)
				return iRow;
			else
				return null;
		}
	}

	public class clsHRWorkingBackground
	{		
		/// <summary>
		/// Lay thong tin qua trinh lam viec cua nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataTable</returns>
		public static DataTable GetDataByEmpID(Object strEmpID)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("HR_spfrmWORKINGBACKGROUND @Activity='GetDataByEmpID',@EmpID = N'" + strEmpID + "'");
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
		/// <summary>
		/// Lay thong tin qua trinh lam viec cua nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataRow</returns>
		public static DataRow GetDataByID(Object strID)
		{
			DataRow iRow = clsCommon.GetDataRow("HR_spfrmWORKINGBACKGROUND @Activity='GetDataByID',@WorkingBackgroundID = " + strID);
			if(iRow !=null)
				return iRow;
			else
				return null;
		}
	}
	public class clsHRLifeInsuarance
	{		
		/// <summary>
		/// Lay thong tin bao hiem tai nan cua nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataTable</returns>
		public static DataTable GetDataAll(string Type,string strFrom,string strTo,string EmpID,string EmpName,string CompanyID,string Level1ID,string Level2ID,string Level3ID,string PositionID,string JobCodeID, string LocationID,string Status,string InsMoney)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("HR_spfrmInsuarance @Activity='GetDataAll',@Type='" + Type + "',@FromDate='" + strFrom + "',@ToDate='" + strTo + "',@TEmpID='" + EmpID + "',@TEmpName='" + EmpName + "',@CompanyID='" + CompanyID + "',@Level1ID='" + Level1ID + "',@Level2ID='" + Level2ID + "',@Level3ID='" + Level3ID + "',@PositionID='" + PositionID + "',@JobCodeID='" + JobCodeID + "',@LocationID='" + LocationID + "',@TStatus='" + Status + "',@InsMoney='" + InsMoney + "'");
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
		
		
		public static bool Delete(string strType,string InsuranceID)
		{
			try
			{
				clsCommon.GetDataTableHasID("HR_spfrmInsuarance @Activity='Delete',@InsuaranceID='" + InsuranceID + "',@Type='" + strType + "'");
				return true;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return false;
			}
		}
		public static bool SaveFromGrid(string strType,string From,string To,string EmpID,string strMoney,string InsuranceID)
		{
			try
			{
				clsCommon.GetDataTableHasID("HR_spfrmInsuarance @Activity='Save',@Type='" + strType + "',@FromDate='" + From + "',@ToDate='" + To + "',@TEmpID='" + EmpID + "',@InsMoney='" + strMoney + "',@InsuaranceID='" + InsuranceID + "'");
				return true;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return false;
			}
		}

	}
	
	public class clsHRAccInsuarance
	{		
		/// <summary>
		/// Lay thong tin bao hiem tai nan cua nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataTable</returns>
		public static DataTable GetDataAll()
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("HR_spfrmAccInsurance @Activity='GetDataAll'");
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
			DataRow iRow = clsCommon.GetDataRow("HR_spfrmAccInsurance @Activity='GetDataByID',@AccInsuranceID = " + strID);
			if(iRow !=null)
				return iRow;
			else
				return null;
		}
		public static DataTable GetEmpInfo(string strEmpID)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("HR_spfrmAccInsurance @Activity='GetEmpInfo',@TEmpID='" + strEmpID +"'");
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

	}
	public class clsHRQualification
	{		
		/// <summary>
		/// Lay thong tin bang cap viec cua nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataTable</returns>
		public static DataTable GetDataByEmpID(Object strEmpID)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("HR_spfrmQUALIFICATION @Activity='GetDataByEmpID',@EmpID = N'" + strEmpID + "'");
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
		/// <summary>
		/// Lay thong tin bang cap cua nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataRow</returns>
		public static DataRow GetDataByID(Object strID)
		{
			DataRow iRow = clsCommon.GetDataRow("HR_spfrmQUALIFICATION @Activity='GetDataByID',@QualificationID = " + strID);
			if(iRow !=null)
				return iRow;
			else
				return null;
		}
	}

	public class clsHRLanguageRecord
	{
		/// <summary>
		/// Lay thong tin ky nang nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataTable</returns>
		public static DataTable GetSkillsByID(Object strEmpID)
		{
			try
			{
				DataTable dt = clsCommon.GetDataTable("HR_spfrmLANGUAGE @Activity='GetDataSkillsByID',@EmpID = N'" + strEmpID + "'");
				return dt;
			}
			catch
			{
				return null;
			}
		}
		/// <summary>
		/// Cap nhat ho so cho nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma nhan vien</param>
		/// <param name="strDocumentsCode">Danh sach cac ho so</param>
		/// <returns>True/false</returns>
		public static bool UpdateSkills(string strEmpID,string strSkillID)
		{			
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "HR_spfrmLANGUAGE";
			//------Delete current skill of employee
			cmd.Parameters.Clear();
			cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "DeleteSkills";
			cmd.Parameters.Add("@EmpID",SqlDbType.NVarChar,12).Value = strEmpID.Trim();
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
					cmd.Parameters.Add("@EmpID",SqlDbType.NVarChar,12).Value = strEmpID.Trim();
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
		/// <summary>
		/// Lay thong tin ngoai ngu cua nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataTable</returns>
		public static DataTable GetDataByEmpID(Object strEmpID)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("HR_spfrmLANGUAGE @Activity='GetDataByEmpID',@EmpID = N'" + strEmpID + "'");
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
		public static bool CheckSave(Object strEmpID,string sValue)
		{
			DataTable dt=new DataTable();
			try
			{
				dt=clsCommon.GetDataTable("HR_spfrmLANGUAGE @Activity='CheckSave',@EmpID = N'" + strEmpID + "',@Value='" + sValue + "'");
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
		/// <summary>
		/// Lay thong tin qua trinh lam viec cua nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataRow</returns>
		public static DataRow GetDataByID(Object strID)
		{
			DataRow iRow = clsCommon.GetDataRow("HR_spfrmLANGUAGE @Activity='GetDataByID',@LanguageRecordID = " + strID);
			if(iRow !=null)
				return iRow;
			else
				return null;
		}
	}
	public class clsHRTempThanh
	{
		public clsHRTempThanh()
		{
			//
			// TODO: Add constructor logic here
			//
		}				
		public static string GetDataEmpIDByID(string strEmpID)
		{
			//clsCommon.GetDataRow("")
			return "";
		}
		
	}
	public class clsHRResident
	{
		/// <summary>
		/// Lay thong tin ngoai ngu cua nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataTable</returns>
		public static DataTable GetDataByEmpID(Object strEmpID, string Language)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("HR_spfrmRESIDENCE @Activity='GetDataByEmpID',@EmpID = N'" + strEmpID + "',@LanguageID='" + Language + "'");				
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
			DataRow iRow = clsCommon.GetDataRow("HR_spfrmRESIDENCE @Activity='GetDataByID',@ResidenceID = " + strID);
			if(iRow !=null)
				return iRow;
			else
				return null;
		}	
				
	}


	public class clsHREmpDocument
	{
		# region  Lay thong tin hồ sơ lưu trữ của nhân viên
		/// <summary>
		/// Lay thong tin hồ sơ lưu trữ của nhân viên
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataTable</returns>
		///
		public static DataTable GetDataByEmpID(Object strEmpID)
		{
			try
			{
				DataTable dt = clsCommon.GetDataTable("HR_spfrmEmpDocument @Activity='GetDataByEmpID',@EmpID = N'" + strEmpID + "'");
				return dt;
			}
			catch
			{
				return null;
			}
		}
		#endregion

		# region  Luu thong tin hồ sơ lưu trữ của nhân viên (dạng Simple)
		/// <summary>
		/// Cap nhat ho so cho nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma nhan vien</param>
		/// <param name="strDocumentsCode">Danh sach cac ho so</param>
		/// <returns>True/false</returns>
		public static bool UpdateEmpDocumentSimple(string strEmpID,string strLSDocumentID, string strStorePlace)
		{			
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "HR_spfrmEmpDocument";

			/*------Delete current document of employee
			cmd.Parameters.Clear();
			cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "DeleteDocumentByEmpID";
			cmd.Parameters.Add("@EmpID",SqlDbType.NVarChar,12).Value = strEmpID.Trim();
			//cmd.Parameters.Add("@EmpID",SqlDbType.NVarChar,12).Value = strEmpID.Trim();
			cmd.ExecuteNonQuery();
			//-------*/
			
			try
			{				
				//------------				
				string[] arrLSDocumentID = strLSDocumentID.Trim().Split('$');				
				
				//vuonglm
				string str="'";
				int j=0;
				for(j=0;j<arrLSDocumentID.Length-1;j++)
					str +=  arrLSDocumentID.GetValue(j).ToString().Trim()+ "','";
				str +=  "'";
				
				//------Delete current document of employee
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "DeleteDocumentByEmpID12";
				cmd.Parameters.Add("@EmpID",SqlDbType.NVarChar,12).Value = strEmpID.Trim();
				cmd.Parameters.Add("@Str",SqlDbType.NVarChar,255).Value = str.Trim();
				cmd.ExecuteNonQuery();
				//-------
				//vuonglm
				//--------------
				for(int i=0;i<arrLSDocumentID.Length-1;i++)
				{
					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "AddNew";
					cmd.Parameters.Add("@EmpID",SqlDbType.NVarChar,12).Value = strEmpID.Trim();
					cmd.Parameters.Add("@LSDocumentID",SqlDbType.NVarChar,12).Value = arrLSDocumentID.GetValue(i).ToString().Trim();
					cmd.Parameters.Add("@StorePlace",SqlDbType.NVarChar,255).Value = strStorePlace.Trim();					
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

		#endregion
	
		# region Lay thong tin 1 record theo mã
		/// <summary>
		/// Lay thong tin 1 record theo mã
		/// </summary>
		/// <param name="strEmpID">Ma EmpDocumentID</param>
		/// <returns>DataRow</returns>
		
		public static DataRow GetDataByID(Object strID)
		{
			DataRow iRow = clsCommon.GetDataRow("HR_spfrmEmpDocument @Activity='GetDataByID', @EmpDocumentID = " + strID);
			if(iRow !=null)
				return iRow;
			else
				return null;
		}

		#endregion
		
		# region Kiểm tra LSDocumentID exists
		/// <summary>
		/// Kiểm tra LSDocumentID exists
		/// </summary>
		/// <param name="strLSDocumentID, strEmpID, strActivity, strEmpDocumentID "</param>
		/// <returns>DataRow</returns>
		

		public static bool CheckExists(Object strEmpID,string strLSDocumentID, string strActivity, string strEmpDocumentID)
		{
			DataTable dt=new DataTable();
			try
			{
				dt=clsCommon.GetDataTable("HR_spfrmEmpDocument @Activity= '"+ strActivity + "' ,@EmpID = N'" + strEmpID + "',@EmpDocumentID = '" + strEmpDocumentID+ "', @LSDocumentID = '" + clsCommon.SafeDataString(strLSDocumentID) + "'");
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

	# endregion
	}

	public class clsHRAbroadRecord
	{
		# region  Lay thong tin về đi nước ngoài của nhân viên
		/// <summary>
		/// Lay thong tin hồ sơ lưu trữ của nhân viên
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataTable</returns>
		///
		public static DataTable GetDataByEmpID(Object strEmpID)
		{
			try
			{
				DataTable dt = clsCommon.GetDataTable("HR_spfrmAbroadRecord @Activity='GetDataByEmpID',@EmpID = N'" + strEmpID + "'");
				return dt;
			}
			catch
			{
				return null;
			}
		}
		#endregion

		# region Lay thong tin 1 record theo mã
		/// <summary>
		/// Lay thong tin 1 record theo mã
		/// </summary>
		/// <param name="strEmpID">Ma AbroadRecordID </param>
		/// <returns>DataRow</returns>
		
		public static DataRow GetDataByID(Object strID)
		{
			DataRow iRow = clsCommon.GetDataRow("HR_spfrmAbroadRecord @Activity='GetDataByID', @AbroadRecordID = '" + clsCommon.SafeDataString( strID) + "'" );
			if(iRow !=null)
				return iRow;
			else
				return null;
		}

		#endregion
	}

	public class clsHRSocialActivity
	{
		# region  Lay thong tin về hoạt động xã hội của 1 nv
		/// <summary>
		/// Lay thong tin về hoạt động xã hội của 1 nv
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataTable</returns>
		///
		public static DataRow GetDataByEmpID(Object strEmpID)
		{
			try
			{
				DataRow dt = clsCommon.GetDataRow("HR_spfrmSocialActivity @Activity='GetDataByEmpID',@EmpID = N'" + strEmpID + "'");
				return dt;
			}
			catch
			{
				return null;
			}
		}
		#endregion		
	}

	public class clsHRHealthCare
	{
		# region  Lay thong tin suc khoe của nhân viên
		/// <summary>
		/// Lay thong tin suc khoe của nhân viên
		/// </summary>
		/// <param name="strEmpID, strLanguageID">Ma so nhan vien, ngôn ngữ</param>
		/// <returns>DataTable</returns>
		///
		public static DataTable GetDataByEmpID(Object strEmpID, string strLanguageID)
		{
			try
			{
				DataTable dt = clsCommon.GetDataTable("HR_spfrmHealthcareRecord @Activity='GetDataByEmpID', @LanguageID = '" + strLanguageID + "', @EmpID = N'" + strEmpID + "'");
				return dt;
			}
			catch
			{
				return null;
			}
		}
		#endregion
			
		# region Lay thong tin 1 record theo mã
		/// <summary>
		/// Lay thong tin 1 record theo mã
		/// </summary>
		/// <param name="strHealthcareRecordID">Ma HealthcareRecordID</param>
		/// <returns>DataRow</returns>
		
		public static DataRow GetDataByID(Object strHealthcareRecordID)
		{
			DataRow iRow = clsCommon.GetDataRow("HR_spfrmHealthcareRecord @Activity='GetDataByID', @HealthcareRecordID = " + strHealthcareRecordID);
			if(iRow !=null)
				return iRow;
			else
				return null;
		}

		#endregion

		# region Kiểm tra OnDate exists
		/// <summary>
		/// Kiểm tra OnDate exists
		/// </summary>
		/// <param name="strOnDate, strEmpID, strActivity, strHealthcareRecordID"</param>
		/// <returns>DataRow</returns>
		

		public static bool CheckExists(Object strEmpID,string strOnDate, string strActivity, string strHealthcareRecordID)
		{
			DataTable dt=new DataTable();
			try
			{
				dt=clsCommon.GetDataTable("HR_spfrmHealthcareRecord @Activity= '" + strActivity + "', @EmpID = N'" + strEmpID + "', @HealthcareRecordID = '" + strHealthcareRecordID  + "', @OnDate = '" + clsCommon.SafeDataString(strOnDate) + "'");
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

		# endregion
	}
	public class clsHRContract
	{
		# region  Lay thong tin HĐ của nhân viên
		/// <summary>
		/// Lay thong tin HĐ của nhân viên
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien, ngôn ngữ</param>
		/// <returns>DataTable</returns>
		///
		public static DataTable GetDataByEmpID(Object strEmpID)
		{
			try
			{
				DataTable dt = clsCommon.GetDataTable("HR_spfrmContract @Activity='GetDataByEmpID', @EmpID = N'" + strEmpID + "'");
				return dt;
			}
			catch
			{
				return null;
			}
		}
		#endregion
			
		# region  Lay thong tin hiện tại của nhân viên
		/// <summary>
		/// Lay thong tin hiện tại của nhân viên
		/// </summary>
		/// <param name="strEmpID, strLanguage">Ma so nhan vien, ngôn ngữ</param>
		/// <returns>DataTable</returns>
		///
		public static DataRow  GetCurrentInfoByEmpID(Object strEmpID, string strLanguage)
		{
			try
			{
				DataRow dt = clsCommon.GetDataRow("HR_spfrmContract @Activity='GetCurrentInfoByEmpID', @languageID = '" + strLanguage + "', @EmpID = N'" + strEmpID + "'");
				return dt;
			}
			catch
			{
				return null;
			}
		}
		#endregion
		
		# region Lay thong tin 1 record theo mã
		/// <summary>
		/// Lay thong tin 1 record theo mã
		/// </summary>
		/// <param name="strContractID">Ma strContractID</param>
		/// <returns>DataRow</returns>
		
		public static DataRow GetDataByID(Object strContractID)
		{
			DataRow iRow = clsCommon.GetDataRow("HR_spfrmContract @Activity='GetDataByID', @ContractID = " + strContractID);
			if(iRow !=null)
				return iRow;
			else
				return null;
		}

		#endregion

		# region Kiểm tra EffectiveDate hợp lệ
		/// <summary>
		/// Kiểm tra OnDate exists
		/// </summary>
		/// <param name="strEffectiveDate, strEmpID, strActivity, strHealthcareRecordID"</param>
		/// <returns>DataRow</returns>
		

		public static bool CheckEffDate(Object strEmpID,string strEffectiveDate, string strActivity, string strContractID)
		{
			DataTable dt=new DataTable();
			try
			{
				dt=clsCommon.GetDataTable("HR_spfrmContract @Activity= '" + strActivity + "', @EmpID = N'" + strEmpID + "', @ContractID = '" + strContractID  + "', @EffectiveDate = '" + clsCommon.SafeDataString(strEffectiveDate) + "'");
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

		# endregion
	}
	
	public class clsHRContractExtend
	{
		# region  DS NV hết hạn HĐ
		/// <summary>
		/// Lay DS NV hết hạn HĐ
		/// </summary>
		/// <param name="strMM, strYYYY, strCompanyID, strLevel1ID, strLevel2ID"></param>
		/// <returns>DataTable</returns>
		///
		public static DataTable GetExpireContract(string strMM, string strYYYY, string strCompanyID, string strLevel1ID, string strLevel2ID, string strContractType)
		{
			try
			{
				string strSQL  = "";
				strSQL = "HR_spfrmContractExtend @Activity='GetExpireContract', @MM = '" + strMM +"', @YYYY = '" + strYYYY + "', @LSCompanyID  = N'" + clsCommon.SafeDataString(strCompanyID) 
					+ "', @LSLevel1ID =N'" + clsCommon.SafeDataString(strLevel1ID) + "', @LSLevel2ID = N'" + strLevel2ID + "', @strContractType = N'" + strContractType + "'";
				DataTable dt = clsCommon.GetDataTable(strSQL);
				return dt;
			}
			catch
			{
				return null;
			}
		}

		public static DataTable GetReminderContract()
		{
			try
			{
				string strSQL  = "";
				strSQL = "HR_spfrmContractExtend @Activity='GetReminderContract'";
				DataTable dt = clsCommon.GetDataTable(strSQL);
				return dt;
			}
			catch
			{
				return null;
			}
		}

		public static DataTable GetReminderContract(string strCompany, string strLevel1, string strLevel2, string strLevel3, string strLocation, System.Web.UI.Page pPage)
		{
			try
			{
				string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
				string strSQL  = "";
				strSQL = "HR_spfrmContractExtend @Activity='GetReminderContract', @UserGroupID=N'" + sAccountLogin + "', @LSLevel1ID=N'" + strLevel1 + "', @LSLevel2ID=N'" + strLevel2 + "', @LSLevel3ID=N'" + strLevel3 + "', @LSLocationID=N'" + strLocation + "'";
				DataTable dt = clsCommon.GetDataTable(strSQL);
				return dt;
			}
			catch
			{
				return null;
			}
		}
		#endregion
	}


	public class clsWFIssue
	{
		# region  Filter
		/// <summary>
		/// Lay DS NV hết hạn HĐ
		/// </summary>
		/// <param name="strMM, strYYYY, strCompanyID, strLevel1ID, strLevel2ID"></param>
		/// <returns>DataTable</returns>
		///
		
		public static DataTable Filter(string strEmpID, string strEmpName, string strCompany, string strLevel1, string strLevel2, string strLevel3, string strStatus, string strWhere,System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			try
			{
				string strSql = "HR_clsCommon @Activity='GetEmpList',@EmpCode=N'" + strEmpID + "', @EmpName=N'" + strEmpName
					+ "', @LSLevel1ID='" + strLevel1 + "', @LSLevel2ID='" + strLevel2 + "',@LSLevel3ID='" + strLevel3
					+ "',@LSCompanyID='" + strCompany + "',@UserGroupID='" + sAccountLogin + "'";				
				if (strStatus != "")
					strSql = strSql + ", @Status='" + strStatus + "'";
				if (strWhere!= "")
					strSql = strSql + ", @Where='" + strWhere + "'";
				DataTable dtb = clsCommon.GetDataTable(strSql);
				return dtb;
			}
			catch
			{
				return null;
			}
		}

		#endregion

		# region Impact DB
		public static string ImpactDB (string strActivity, string strIssueID, string strEmpID, string strYear, string strDate, string strLSIssueID, string strQuantity, string strUnit, string strNote, string strCreater)
		{			
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();			
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "WF_spfrmIssue";
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 50).Value = strActivity;
				cmd.Parameters.Add("@IssueID", SqlDbType.NVarChar,12).Value = strIssueID;
				cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar,12).Value = strEmpID;
				cmd.Parameters.Add("@LSIssueID", SqlDbType.NVarChar, 12).Value = strLSIssueID;
				cmd.Parameters.Add("@Year", SqlDbType.VarChar, 4).Value = strYear;
				cmd.Parameters.Add("@Date", SqlDbType.VarChar, 12).Value = strDate;					
				cmd.Parameters.Add("@Quantity", SqlDbType.SmallInt ).Value = strQuantity;
				if (strUnit != "")
					cmd.Parameters.Add("@Unit", SqlDbType.NVarChar, 20).Value = strUnit;					
				else
					cmd.Parameters.Add("@Unit", SqlDbType.NVarChar, 20).Value = DBNull.Value ;					
				if (strNote != "")
					cmd.Parameters.Add("@Note", SqlDbType.NVarChar, 255).Value = strNote;
				else
					cmd.Parameters.Add("@Note", SqlDbType.NVarChar, 255).Value = DBNull.Value ;					
				cmd.Parameters.Add("@Creater", SqlDbType.NVarChar, 50).Value = strCreater ;
				cmd.Parameters.Add("@ReturnMess", SqlDbType.NVarChar, 255).Value = "";
				cmd.Parameters["@ReturnMess"].Direction = ParameterDirection.InputOutput;
				cmd.ExecuteNonQuery();
				
				if (sqlTran != null ) sqlTran.Commit();
				return "";			
			}
			catch(Exception exp)
			{
				if (sqlTran != null) sqlTran.Rollback();
				return exp.Message ;
			}
		}
		#endregion

		# region  Lay thong tin cấp phát của nhân viên
		/// <summary>
		/// Lay thong tin cấp phát của nhân viên
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien, ngôn ngữ</param>
		/// <returns>DataTable</returns>
		///
		public static DataTable GetDataByEmpID(Object strEmpID)
		{
			try
			{
				DataTable dt = clsCommon.GetDataTable("WF_spfrmIssue @Activity='GetDataByEmpID', @EmpID = N'" + strEmpID + "'");
				return dt;
			}
			catch
			{
				return null;
			}
		}
		#endregion

		# region Lay thong tin 1 record theo mã
		/// <summary>
		/// Lay thong tin 1 record theo mã
		/// </summary>
		/// <param name="strContractID">Ma strContractID</param>
		/// <returns>DataRow</returns>
		
		public static DataRow GetDataByID(Object strIssueID)
		{
			DataRow iRow = clsCommon.GetDataRow("WF_spfrmIssue @Activity='GetDataByID', @IssueID = " + strIssueID);
			if(iRow !=null)
				return iRow;
			else
				return null;
		}

		#endregion

		# region Search 
		public static DataTable Search (string strActivity, string strEmpID, string strYear, string strDate, string strLSIssueTypeID, string strLSIssueID, string strQuantity, string strUnit, string strNote)
		{	
			DataTable rsData = new DataTable();
			SqlDataAdapter adapter = new SqlDataAdapter();

			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();			
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "WF_spfrmIssue";
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 50).Value = strActivity;
				cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar,12).Value = strEmpID;
				cmd.Parameters.Add("@LSIssueTypeID", SqlDbType.NVarChar, 12).Value = strLSIssueTypeID;
				cmd.Parameters.Add("@LSIssueID", SqlDbType.NVarChar, 12).Value = strLSIssueID;
				cmd.Parameters.Add("@Year", SqlDbType.VarChar, 4).Value = strYear;
				cmd.Parameters.Add("@Date", SqlDbType.VarChar, 12).Value = strDate;					
				if (strQuantity != "")
					cmd.Parameters.Add("@Quantity", SqlDbType.SmallInt ).Value = strQuantity;				
				else
					cmd.Parameters.Add("@Quantity", SqlDbType.SmallInt ).Value = DBNull.Value ;				
				cmd.Parameters.Add("@Unit", SqlDbType.NVarChar, 20).Value = strUnit;
				cmd.Parameters.Add("@Note", SqlDbType.NVarChar, 255).Value = strNote;
				cmd.Parameters.Add("@ReturnMess", SqlDbType.NVarChar, 255).Value = "";
				cmd.Parameters["@ReturnMess"].Direction = ParameterDirection.InputOutput;

				adapter.SelectCommand=cmd;
				adapter.Fill(rsData);
				
				if (sqlTran != null ) sqlTran.Commit();
				adapter.Dispose();
				return rsData;
			}
			catch(Exception exp)
			{
				adapter.Dispose();
				if (sqlTran != null) sqlTran.Rollback();
				return null ;
			}
		}
		#endregion

		# region LstIssue 
		public static DataTable LstIssue(string strEmpID, string strEmpName, string strCompany, string strLevel1, string strLevel2, string strLevel3, string strFromDate, string strToDate, string strYear, string strLSIssueTypeID, string strLSIssueID, System.Web.UI.Page  pPage)
		{	
			DataTable rsData = new DataTable();
			SqlDataAdapter adapter = new SqlDataAdapter();

			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();			
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			
			string strUserGroupID= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";

			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "WF_spfrmIssue";
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 50).Value = "LstIssue";
				cmd.Parameters.Add("@LSCompanyID", SqlDbType.NVarChar,12).Value = strCompany ;
				cmd.Parameters.Add("@LSLevel1ID", SqlDbType.NVarChar,12).Value = strLevel1 ;
				cmd.Parameters.Add("@LSLevel2ID", SqlDbType.NVarChar,12).Value = strLevel2 ;
				cmd.Parameters.Add("@LSLevel3ID", SqlDbType.NVarChar,12).Value = strLevel3 ;
				cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar,12).Value = strEmpID;
				cmd.Parameters.Add("@EmpName", SqlDbType.NVarChar,80).Value = strEmpName;
				cmd.Parameters.Add("@LSIssueTypeID", SqlDbType.NVarChar, 12).Value = strLSIssueTypeID;
				cmd.Parameters.Add("@LSIssueID", SqlDbType.NVarChar, 12).Value = strLSIssueID;
				cmd.Parameters.Add("@Year", SqlDbType.VarChar, 4).Value = strYear;
				cmd.Parameters.Add("@FromDate", SqlDbType.VarChar, 12).Value = strFromDate ;					
				cmd.Parameters.Add("@ToDate", SqlDbType.VarChar, 12).Value = strToDate;			
				cmd.Parameters.Add("@UserGroupID", SqlDbType.NVarChar, 50).Value = strUserGroupID;				
				cmd.Parameters.Add("@ReturnMess", SqlDbType.NVarChar, 255).Value = "";				
				adapter.SelectCommand=cmd;
				adapter.Fill(rsData);
				
				if (sqlTran != null ) sqlTran.Commit();
				adapter.Dispose();
				return rsData;
			}
			catch(Exception exp)
			{
				adapter.Dispose();
				if (sqlTran != null) sqlTran.Rollback();
				return null ;
			}
		}
		#endregion

		# region Statistic Issue 
		public static DataTable StatisticIssue(string strEmpID, string strEmpName, string strCompany, string strLevel1, string strLevel2, string strLevel3, string strFromDate, string strToDate, string strYear, string strLSIssueTypeID, string strLSIssueID, System.Web.UI.Page pPage)
		{	
			DataTable rsData = new DataTable();
			SqlDataAdapter adapter = new SqlDataAdapter();

			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();			
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
		
			string strUserGroupID= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
	
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "WF_spfrmIssue";
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 50).Value = "StatisticIssue";
				cmd.Parameters.Add("@LSCompanyID", SqlDbType.NVarChar,12).Value = strCompany ;
				cmd.Parameters.Add("@LSLevel1ID", SqlDbType.NVarChar,12).Value = strLevel1 ;
				cmd.Parameters.Add("@LSLevel2ID", SqlDbType.NVarChar,12).Value = strLevel2 ;
				cmd.Parameters.Add("@LSLevel3ID", SqlDbType.NVarChar,12).Value = strLevel3 ;
				cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar,12).Value = strEmpID;
				cmd.Parameters.Add("@EmpName", SqlDbType.NVarChar,80).Value = strEmpName;
				cmd.Parameters.Add("@LSIssueTypeID", SqlDbType.NVarChar, 12).Value = strLSIssueTypeID;
				cmd.Parameters.Add("@LSIssueID", SqlDbType.NVarChar, 12).Value = strLSIssueID;
				cmd.Parameters.Add("@Year", SqlDbType.VarChar, 4).Value = strYear;
				cmd.Parameters.Add("@FromDate", SqlDbType.VarChar, 12).Value = strFromDate ;					
				cmd.Parameters.Add("@ToDate", SqlDbType.VarChar, 12).Value = strToDate;			
				cmd.Parameters.Add("@UserGroupID", SqlDbType.NVarChar, 50).Value = strUserGroupID;	
				cmd.Parameters.Add("@ReturnMess", SqlDbType.NVarChar, 255).Value = "";				
				adapter.SelectCommand=cmd;
				adapter.Fill(rsData);
				
				if (sqlTran != null ) sqlTran.Commit();
				adapter.Dispose();
				return rsData;
			}
			catch(Exception exp)
			{
				adapter.Dispose();
				if (sqlTran != null) sqlTran.Rollback();
				return null ;
			}
		}
		#endregion
	}

	public class clsWFPublicIssue
	{	
		# region Impact DB
		/// <summary>
		/// Impact DB
		/// </summary>
		/// <param name></param>
		/// <returns>DataTable</returns>
		///
		public static DataTable ImpactDB (string strActivity, string strPublicIssueID, string strLSCompanyID, string strLSLevel1ID, string strLSLevel2ID, string strLSLevel3ID, string strYear, string strDate, string strLSIssueTypeID, string strLSIssueID, string strQuantity, string strUnit, string strReceiver, string strNote, string strCreater)
		{	
			DataTable rsData = new DataTable();
			SqlDataAdapter adapter = new SqlDataAdapter();

			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();			
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "WF_spfrmPublicIssue";
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 50).Value = strActivity;
				cmd.Parameters.Add("@PublicIssueID", SqlDbType.NVarChar,12).Value = strPublicIssueID;
				cmd.Parameters.Add("@LSCompanyID", SqlDbType.NVarChar,12).Value = strLSCompanyID;
				cmd.Parameters.Add("@LSLevel1ID", SqlDbType.NVarChar,12).Value = strLSLevel1ID;
				if (strLSLevel2ID != "")
					cmd.Parameters.Add("@LSLevel2ID", SqlDbType.NVarChar,12).Value = strLSLevel2ID;
				else
					cmd.Parameters.Add("@LSLevel2ID", SqlDbType.NVarChar,12).Value = DBNull.Value ;
				if (strLSLevel3ID != "")
					cmd.Parameters.Add("@LSLevel3ID", SqlDbType.NVarChar,12).Value = strLSLevel3ID;
				else
					cmd.Parameters.Add("@LSLevel3ID", SqlDbType.NVarChar,12).Value = DBNull.Value ;
				cmd.Parameters.Add("@LSIssueTypeID", SqlDbType.NVarChar, 12).Value = strLSIssueTypeID;
				cmd.Parameters.Add("@LSIssueID", SqlDbType.NVarChar, 12).Value = strLSIssueID;
				cmd.Parameters.Add("@Year", SqlDbType.VarChar, 4).Value = strYear;
				cmd.Parameters.Add("@Date", SqlDbType.VarChar, 12).Value = strDate;					
				cmd.Parameters.Add("@Receiver", SqlDbType.NVarChar, 80).Value = strReceiver;
				if (strQuantity != "")
					cmd.Parameters.Add("@Quantity", SqlDbType.SmallInt ).Value = strQuantity;
				else
					cmd.Parameters.Add("@Quantity", SqlDbType.SmallInt ).Value = DBNull.Value;
				if (strUnit != "")
					cmd.Parameters.Add("@Unit", SqlDbType.NVarChar, 20).Value = strUnit;					
				else
					cmd.Parameters.Add("@Unit", SqlDbType.NVarChar, 20).Value = DBNull.Value ;									
				if (strNote != "")
					cmd.Parameters.Add("@Note", SqlDbType.NVarChar, 255).Value = strNote;
				else
					cmd.Parameters.Add("@Note", SqlDbType.NVarChar, 255).Value = DBNull.Value ;					
				cmd.Parameters.Add("@Creater", SqlDbType.NVarChar, 50).Value = strCreater ;
				cmd.Parameters.Add("@ReturnMess", SqlDbType.NVarChar, 255).Value = "";
				cmd.Parameters["@ReturnMess"].Direction = ParameterDirection.InputOutput;
				

				adapter.SelectCommand = cmd;
				adapter.Fill(rsData); 
								
				if (sqlTran != null ) sqlTran.Commit();
				adapter.Dispose();
				return rsData;			
			}
			catch(Exception exp)
			{
				if (sqlTran != null) sqlTran.Rollback();
				adapter.Dispose();
				return null;
			}
		}
		#endregion

		# region  Lay thong tin cấp phát dùng chung : Load grid
		/// <summary>
		/// Lay thong tin cấp phát dùng chung
		/// </summary>
		/// <param name></param></param>
		/// <returns>DataTable</returns>
		///
		public static DataTable GetDataAll()
		{
			try
			{
				DataTable dt = clsCommon.GetDataTable("WF_spfrmPublicIssue @Activity='GetDataAll'");
				return dt;
			}
			catch
			{
				return null;
			}
		}
		#endregion

		# region Lay thong tin 1 record theo mã
		/// <summary>
		/// Lay thong tin 1 record theo mã
		/// </summary>
		/// <param name="strPublicIssueID">Ma strPublicIssueID</param>
		/// <returns>DataRow</returns>
		
		public static DataRow GetDataByID(Object strPublicIssueID)
		{
			DataRow iRow = clsCommon.GetDataRow("WF_spfrmPublicIssue @Activity='GetDataByID', @PublicIssueID = " + strPublicIssueID);
			if(iRow !=null)
				return iRow;
			else
				return null;
		}

		#endregion

		# region List of public issue by duration
		/// <summary>
		/// LstPublicIssue
		/// </summary>
		/// <param name></param>
		/// <returns>DataTable</returns>
		///
		public static DataTable LstPublicIssue (string strLSCompanyID, string strLSLevel1ID, string strLSLevel2ID, string strLSLevel3ID, string strFromDate, string strToDate, string strYear, string strLSIssueTypeID, string strLSIssueID, System.Web.UI.Page pPage)
		{	
			DataTable rsData = new DataTable();
			SqlDataAdapter adapter = new SqlDataAdapter();

			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();			
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "WF_spfrmPublicIssue";

			string strUserGroupID= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 50).Value = "LstPublicIssue";
				cmd.Parameters.Add("@LSCompanyID", SqlDbType.NVarChar,12).Value = strLSCompanyID;
				cmd.Parameters.Add("@LSLevel1ID", SqlDbType.NVarChar,12).Value = strLSLevel1ID;
				cmd.Parameters.Add("@LSLevel2ID", SqlDbType.NVarChar,12).Value = strLSLevel2ID;
				cmd.Parameters.Add("@LSLevel3ID", SqlDbType.NVarChar,12).Value = strLSLevel3ID;
				cmd.Parameters.Add("@LSIssueTypeID", SqlDbType.NVarChar, 12).Value = strLSIssueTypeID;
				cmd.Parameters.Add("@LSIssueID", SqlDbType.NVarChar, 12).Value = strLSIssueID;
				cmd.Parameters.Add("@Year", SqlDbType.VarChar, 4).Value = strYear;
				cmd.Parameters.Add("@FromDate", SqlDbType.VarChar, 12).Value = strFromDate ;					
				cmd.Parameters.Add("@ToDate", SqlDbType.VarChar, 12).Value = strToDate;			
				cmd.Parameters.Add("@UserGroupID", SqlDbType.NVarChar, 50).Value = strUserGroupID;
				cmd.Parameters.Add("@ReturnMess", SqlDbType.NVarChar, 255).Value = "";
				cmd.Parameters["@ReturnMess"].Direction = ParameterDirection.InputOutput;
				

				adapter.SelectCommand = cmd;
				adapter.Fill(rsData); 
								
				if (sqlTran != null ) sqlTran.Commit();
				adapter.Dispose();
				return rsData;			
			}
			catch(Exception exp)
			{
				if (sqlTran != null) sqlTran.Rollback();
				adapter.Dispose();
				return null;
			}
		}
		#endregion

		# region Statistic of public issue by duration
		/// <summary>
		/// StatisticPublicIssue
		/// </summary>
		/// <param name></param>
		/// <returns>DataTable</returns>
		///
		public static DataTable StatisticPublicIssue (string strLSCompanyID, string strLSLevel1ID, string strLSLevel2ID, string strLSLevel3ID, string strFromDate, string strToDate, string strYear, string strLSIssueTypeID, string strLSIssueID, System.Web.UI.Page pPage)
		{	
			DataTable rsData = new DataTable();
			SqlDataAdapter adapter = new SqlDataAdapter();

			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();			
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			
			string strUserGroupID= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";

			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "WF_spfrmPublicIssue";
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 50).Value = "StatisticPublicIssue";
				cmd.Parameters.Add("@LSCompanyID", SqlDbType.NVarChar,12).Value = strLSCompanyID;
				cmd.Parameters.Add("@LSLevel1ID", SqlDbType.NVarChar,12).Value = strLSLevel1ID;
				cmd.Parameters.Add("@LSLevel2ID", SqlDbType.NVarChar,12).Value = strLSLevel2ID;
				cmd.Parameters.Add("@LSLevel3ID", SqlDbType.NVarChar,12).Value = strLSLevel3ID;
				cmd.Parameters.Add("@LSIssueTypeID", SqlDbType.NVarChar, 12).Value = strLSIssueTypeID;
				cmd.Parameters.Add("@LSIssueID", SqlDbType.NVarChar, 12).Value = strLSIssueID;
				cmd.Parameters.Add("@Year", SqlDbType.VarChar, 4).Value = strYear;
				cmd.Parameters.Add("@FromDate", SqlDbType.VarChar, 12).Value = strFromDate ;					
				cmd.Parameters.Add("@ToDate", SqlDbType.VarChar, 12).Value = strToDate;			
				cmd.Parameters.Add("@UserGroupID", SqlDbType.NVarChar, 50).Value = strUserGroupID;
				cmd.Parameters.Add("@ReturnMess", SqlDbType.NVarChar, 255).Value = "";
				cmd.Parameters["@ReturnMess"].Direction = ParameterDirection.InputOutput;
				

				adapter.SelectCommand = cmd;
				adapter.Fill(rsData); 
								
				if (sqlTran != null ) sqlTran.Commit();
				adapter.Dispose();
				return rsData;			
			}
			catch(Exception exp)
			{
				if (sqlTran != null) sqlTran.Rollback();
				adapter.Dispose();
				return null;
			}
		}
		#endregion
		
	}
}
