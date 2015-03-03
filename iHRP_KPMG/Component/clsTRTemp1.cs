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
using iHRPCore.Com;
namespace iHRPCore.TRComponent
{
	/// <summary>
	/// Summary description for clsTRTemp1.
	/// </summary>
	/// 
	public  class clsTRTrainingRequestEmp
	{
		#region contructor
		public clsTRTrainingRequestEmp()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#endregion

		#region GetData
		public static DataTable GeData(string pstrEmpID, string pstrLSTrainingCourseID, string pstrFromDate, string pstrToDate,string pstrVenue,string pstrCompany,string pstrLevel1,string pstrLevel2,string pstrSource,string pstrStatus, string pstrEmpCodeSearch, string pstrEmpNameSearch, string pstrLevel3,string sLSEmpTypeID, string sLocalExpat, string sLSLocationID)
		{
			DataTable dtb = new DataTable();
			string strSQL = "";
			strSQL = "TR_spfrmTrainingRequestEmp @Activity = 'GetData', @EmpID = '" + pstrEmpID + "', @TrainingCourseID='" + pstrLSTrainingCourseID + 
				"', @Venue='" + pstrVenue + "',@FromDate='" + pstrFromDate + "', @ToDate='" + pstrToDate + "', @LSCompanyID='" + pstrCompany + "', @Level1ID='" + pstrLevel1 + "', @Level2ID='" + pstrLevel2 + "', @LSRESourceID='" + pstrSource +  "', @StatusEmpSearch='" + pstrStatus +  "', @EmpCodeSearch ='" + pstrEmpCodeSearch +  "', @EmpNameSearch= N'" + pstrEmpNameSearch + "', @Level3ID ='" + pstrLevel3 + "',@LSEmpTypeID='" + sLSEmpTypeID + "',@LocalExpat='" + sLocalExpat + "', @LSLocationID='" + sLSLocationID + "'";
			try
			{
				dtb= clsCommon.GetDataTable(strSQL);
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

		#endregion
	}
	public class clsTRTrainingRequest
	{
		#region contructor
		public clsTRTrainingRequest()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#endregion

		#region lay thong tin cua phieu yeu cau yheo nhan vien 
		public static DataRow GetDataByEmpID(Object strEmpID)
		{
			DataRow iRow = clsCommon.GetDataRow("TR_spfrmTrainingRequest @Activity='GetDataByEmpID', @EmpID = N'" + strEmpID + "'");
			if(iRow !=null)
				return iRow;
			else
				return null;
		}
		#endregion

		#region Load_dtgRequestFee
		public static DataTable Load_dtgRequestFee(string pTrainingRequestID, string pLanguageID)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb= clsCommon.GetDataTable("TR_spfrmTrainingRequest @Activity='GetRequestFee', @TrainingRequestID='" + pTrainingRequestID +  "', @LanguageID = '" + pLanguageID + "'");
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
		#endregion

		#region Load_dtgRequestBudget
		public static DataTable Load_dtgRequestBudget(string pTrainingRequestID, string pLanguageID)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb= clsCommon.GetDataTable("TR_spfrmTrainingRequest @Activity='GetRequestBudget', @TrainingRequestID='" + pTrainingRequestID +  "', @LanguageID = '" + pLanguageID + "'");
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
		#endregion		
		#region getDataByID
		public static DataRow getDataByID(string pTrainingRequestID)
		{
			DataRow drData;
			try
			{
				drData= clsCommon.GetDataRow("TR_spfrmTrainingRequest @Activity='getDataByID', @TrainingRequestID='" + pTrainingRequestID + "'");
				return drData;
			}
			catch
			{
				return null;
			}			
		}
		#endregion

		#region getDataByRequestID
		public static DataRow getDataByRequestID(string pTrainingRequestID)
		{
			DataRow drData;
			try
			{
				drData= clsCommon.GetDataRow("TR_spfrmTrainingRequest @Activity='getDataByRequestID', @TrainingRequestID='" + pTrainingRequestID + "'");
				return drData;
			}
			catch
			{
				return null;
			}			
		}
		#endregion

		#region Load_dtgRequestEmp
		public static DataTable Load_dtgRequestEmp(string pTrainingRequestID)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb = clsCommon.GetDataTable("TR_spfrmTrainingRequest @Activity='getListEmp', @TrainingRequestID='" + pTrainingRequestID + "'");
				return dtb;
			}
			catch
			{
				return null;
			}			
		}
		public static DataTable Load_dtgRequestEmp_Add(string pTrainingRequestID)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb = clsCommon.GetDataTable("TR_spfrmTrainingRequest @Activity='getListEmp_Add', @TrainingRequestID='" + pTrainingRequestID + "'");
				return dtb;
			}
			catch
			{
				return null;
			}			
		}
		#endregion

		#region getScheduleDetails
		public static DataTable Load_dtgRequestSchedule(string pTrainingRequestID)
		{
			DataTable  dtb = new DataTable();
			try
			{
				dtb = clsCommon.GetDataTable("TR_spfrmTrainingRequest @Activity='getScheduleDetails', @TrainingRequestID='" + pTrainingRequestID + "'");
				return dtb;
			}
			catch
			{
				return null;
			}			
		}
		#endregion
		
		#region DeleteDetailsTemp
		public static string DeleteDetailsTemp(string strCurUser)
		{
			string strErr = "";
			try
			{
				clsCommon.GetDataTable("TR_spfrmTrainingRequest @Activity='DeleteDetailsTemp', @Creater='" + strCurUser + "'");
			}
			catch (Exception exp)
			{
				strErr = exp.Message.Trim();
			}
			return strErr;
		}
		#endregion

		#region LoadScheduleDetails

		public static DataTable LoadScheduleDetails(string strLanguage, string strEmpID, string strFromDate, string strToDate,string strTrainingRequestID,string strCurUser)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb = clsCommon.GetDataTable("TR_spfrmTrainingRequest @Activity='LoadScheduleDetails', @LanguageID='" 
					+ strLanguage + "', @EmpID=N'" + strEmpID + "', @FromDate='" + strFromDate + "', @ToDate='" + strToDate 
					+ "', @TrainingRequestID=N'" + strTrainingRequestID + "',@Creater='" + strCurUser + "'");
			}
			catch
			{
				dtb = new DataTable();
			}
			return dtb;
		}
		#endregion

		#region SaveScheduleDetailsTemp
		public static string SaveScheduleDetailsTemp(DataTable dtb, string strEmpID, string strFromDate, string strToDate, string strCurUser)
		{
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "TR_spfrmTrainingRequest";
   
				for (int i = 0;i< dtb.Rows.Count;i++)
				{
					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "SaveSDetailsTemp";						
					cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = strEmpID;
					cmd.Parameters.Add("@FromDate", SqlDbType.NVarChar, 12).Value = strFromDate;
					cmd.Parameters.Add("@ToDate", SqlDbType.NVarChar, 12).Value = strToDate;
					cmd.Parameters.Add("@DateID", SqlDbType.NVarChar, 12).Value = dtb.Rows[i]["DateID"].ToString().Trim();
					cmd.Parameters.Add("@LeaveTaken", SqlDbType.Real).Value = dtb.Rows[i]["LeaveTaken"].ToString().Trim();
					cmd.Parameters.Add("@Creater", SqlDbType.NVarChar, 20).Value = strCurUser;
					cmd.ExecuteNonQuery();											
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
		#endregion

		#region Delete_TrainingFEe
		public static void Delete_TrainingFee(string pTrainingRequestID)
		{
			clsCommon.sExecuteCommandTextTrans("TR_spfrmTrainingRequest @Activity = 'Delete_TrainingFee', @TrainingRequestID = '" + pTrainingRequestID + "'");
		}
		#endregion

		#region Delete_TrainingBudget
		public static void Delete_TrainingBudget(string pTrainingRequestID)
		{
			clsCommon.sExecuteCommandTextTrans("TR_spfrmTrainingRequest @Activity = 'Delete_TrainingBudget', @TrainingRequestID = '" + pTrainingRequestID + "'");
		}
		#endregion

		#region Delete_EmpList
		public static void Delete_EmpList(string pTrainingRequestID)
		{
			clsCommon.sExecuteCommandTextTrans("TR_spfrmTrainingRequest @Activity = 'Delete_EmpList', @TrainingRequestID = '" + pTrainingRequestID + "'");
		}
		#endregion

		#region sImpact_TrainingFee	
		public static void sImpact_TrainingFee(string[] arrTrainingFee, string[] arrAmount , string pTrainingRequestID,string[] arrOthers)
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
				cmd.CommandText = "TR_spfrmTrainingRequest";
			
				for(int i=0;i<arrTrainingFee.Length-1;i++)
				{
					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Save_TrainingFee";				
					cmd.Parameters.Add("@TrainingRequestID",SqlDbType.NVarChar,12).Value = pTrainingRequestID;				
					cmd.Parameters.Add("@Others",SqlDbType.NVarChar,255).Value = arrOthers.GetValue(i).ToString();				
					cmd.Parameters.Add("@Amount",SqlDbType.Money).Value = arrAmount.GetValue(i).ToString();				
					cmd.Parameters.Add("@LSTrainingFeeID",SqlDbType.NVarChar,12).Value = arrTrainingFee.GetValue(i).ToString();
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
		#endregion

		#region sImpact_TrainingFee	
		public static void sImpact_TrainingBudget(string[] arrTrainingBudget, string[] arrAmountBudget , string pTrainingRequestID,string[] arrOthersBudget)
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
				cmd.CommandText = "TR_spfrmTrainingRequest";
			
				for(int i=0;i<arrTrainingBudget.Length-1;i++)
				{
					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Save_TrainingBudget";				
					cmd.Parameters.Add("@TrainingRequestID",SqlDbType.NVarChar,12).Value = pTrainingRequestID;				
					cmd.Parameters.Add("@OthersBudget",SqlDbType.NVarChar,255).Value = arrOthersBudget.GetValue(i).ToString();				
					cmd.Parameters.Add("@AmountBudget",SqlDbType.Money).Value = arrAmountBudget.GetValue(i).ToString();				
					cmd.Parameters.Add("@LSTrainingBudgetID",SqlDbType.NVarChar,12).Value = arrTrainingBudget.GetValue(i).ToString();
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
		#endregion

		#region sImpact_RequestEmpList
		public static void sImpact_RequestEmpList(string strTrainingRequestID, string[] arrEmpID)
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
				cmd.CommandText = "TR_spfrmTrainingRequest";
			
				for(int i=0;i<arrEmpID.Length;i++)
				{
					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "InsertRequestEmp";				
					cmd.Parameters.Add("@TrainingRequestID",SqlDbType.NVarChar,12).Value = strTrainingRequestID;				
					cmd.Parameters.Add("@EmpID",SqlDbType.NVarChar,12).Value = arrEmpID.GetValue(i).ToString();
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
		#endregion

		#region sImpact_RequestEmp
		public static string sImpact_RequestEmp(string strActivity, string strTrainingRequestID,string strEmpID)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "TR_spfrmTrainingRequest";
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = strActivity;
				cmd.Parameters.Add("@EmpID",SqlDbType.NVarChar,12).Value = strEmpID.Trim();
				cmd.Parameters.Add("@TrainingRequestID",SqlDbType.NVarChar,12).Value= strTrainingRequestID.Trim();		
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
		#endregion

		#region Save_RequestData
		public static string Save_RequestData(string strActivity, string strEmpID,string strTrainingCourseID,string strTrainingCourseName,string strDescription,string strFromDate,string strToDate
			,int Venue, string strLSNationalityID,string strLSProvinceID,string strProvider,int Type,string strSubmitDate,int Status, string strCreater,string strLSTrainingCourseID,string strProvider1,string strCourseNameTranfer,string strLSProviderID, string sLSCurrencyTypeID,bool isMakeTRReport
			,string strLineManager1,string strLineManager2, string strLineManager3,string sFullPart,string sOrganizer,string sAddress,string sTelFax,string sWebsite,string sEmail,string sJustification,string sE_Name,string sE_Relation,string sE_Address,string sE_Phone, string sDuration,string sTravelDays )
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "TR_spfrmTrainingRequest";
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = strActivity;
				cmd.Parameters.Add("@EmpID",SqlDbType.NVarChar,12).Value = strEmpID.Trim();
				cmd.Parameters.Add("@Description",SqlDbType.NVarChar,255).Value= strDescription.Trim();
				cmd.Parameters.Add("@Provider1",SqlDbType.NVarChar,200).Value= strProvider1.Trim();
				
				if(strTrainingCourseID.Trim()=="")
				{
					cmd.Parameters.Add("@TrainingCourseID",SqlDbType.NVarChar,12).Value = DBNull.Value;
					
				}
				else
				{
					cmd.Parameters.Add("@TrainingCourseID",SqlDbType.NVarChar,12).Value = strTrainingCourseID.Trim();
					
				}
				if(strLSTrainingCourseID.Trim()=="")
					cmd.Parameters.Add("@LSTrainingCourseID",SqlDbType.NVarChar,12).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@LSTrainingCourseID",SqlDbType.NVarChar,12).Value = strLSTrainingCourseID.Trim();
				if(strLSProviderID.Trim()=="")
					cmd.Parameters.Add("@LSProviderID",SqlDbType.NVarChar,12).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@LSProviderID",SqlDbType.NVarChar,12).Value = strLSProviderID.Trim();
				if(strTrainingCourseName.Trim()=="")
				{
					cmd.Parameters.Add("@TrainingCourseName",SqlDbType.NVarChar,255).Value= DBNull.Value;
					cmd.Parameters.Add("@CourseNameTranfer",SqlDbType.NVarChar,255).Value= DBNull.Value;
				}
				else
				{
					cmd.Parameters.Add("@TrainingCourseName",SqlDbType.NVarChar,255).Value= strTrainingCourseName.Trim();
					cmd.Parameters.Add("@CourseNameTranfer",SqlDbType.NVarChar,255).Value= strCourseNameTranfer.Trim();

				}

				if(strFromDate.Trim()=="")
					cmd.Parameters.Add("@FromDate",SqlDbType.NVarChar,12).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@FromDate",SqlDbType.NVarChar,12).Value = strFromDate.Trim();
				if(strToDate.Trim()=="")
					cmd.Parameters.Add("@ToDate",SqlDbType.NVarChar,12).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@ToDate",SqlDbType.NVarChar,12).Value = strToDate.Trim();
				
				cmd.Parameters.Add("@Venue",SqlDbType.SmallInt).Value= Venue;

				if(strLSNationalityID.Trim()=="")
					cmd.Parameters.Add("@LSNationalityID",SqlDbType.NVarChar,12).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@LSNationalityID",SqlDbType.NVarChar,12).Value = strLSNationalityID.Trim();

				if(strLSProvinceID.Trim()=="")
					cmd.Parameters.Add("@LSProvinceID",SqlDbType.NVarChar,12).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@LSProvinceID",SqlDbType.NVarChar,12).Value = strLSProvinceID.Trim();				
				
				if (sLSCurrencyTypeID.Trim()=="")
					cmd.Parameters.Add("@LSCurrencyTypeID",SqlDbType.NVarChar,12).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@LSCurrencyTypeID",SqlDbType.NVarChar,12).Value = sLSCurrencyTypeID.Trim();				

				cmd.Parameters.Add("@Provider",SqlDbType.NVarChar,255).Value = strProvider.Trim();				
				cmd.Parameters.Add("@LineManager1",SqlDbType.NVarChar,4000).Value = strLineManager1.Trim();				
				cmd.Parameters.Add("@LineManager2",SqlDbType.NVarChar,4000).Value = strLineManager2.Trim();				
				cmd.Parameters.Add("@LineManager3",SqlDbType.NVarChar,4000).Value = strLineManager3.Trim();				
				
				cmd.Parameters.Add("@Type",SqlDbType.SmallInt,10).Value = Type;
				//moi them
				cmd.Parameters.Add("@isMakeTRReport",SqlDbType.Bit).Value = isMakeTRReport;
				
				if (strSubmitDate.Trim()=="")
					cmd.Parameters.Add("@SubmitDate",SqlDbType.NVarChar,12).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@SubmitDate",SqlDbType.NVarChar,12).Value = strSubmitDate.Trim();

				cmd.Parameters.Add("@Status",SqlDbType.SmallInt).Value = Status;
		
				//UPdate Creater /////////////////////////////////////////
				
				if (strCreater.Trim()=="")
					cmd.Parameters.Add("@Creater",SqlDbType.NVarChar,20).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@Creater",SqlDbType.NVarChar,20).Value = strCreater.Trim();
				
					
				
				/////////////////////////////////////////////////////////////////////
				cmd.Parameters.Add("@TrainingRequestID", SqlDbType.NVarChar,12).Direction = ParameterDirection.InputOutput;
				cmd.Parameters.Add("@ReturnMess", SqlDbType.NVarChar,255).Direction = ParameterDirection.InputOutput;

				cmd.Parameters.Add("@FullPart",SqlDbType.NVarChar,2).Value = sFullPart.Trim();	
				cmd.Parameters.Add("@Organizer",SqlDbType.NVarChar,250).Value = sOrganizer.Trim();	
				cmd.Parameters.Add("@Address",SqlDbType.NVarChar,250).Value = sAddress.Trim();	
				cmd.Parameters.Add("@TelFax",SqlDbType.NVarChar,50).Value = sTelFax.Trim();	
				cmd.Parameters.Add("@Website",SqlDbType.NVarChar,250).Value = sWebsite.Trim();	
				cmd.Parameters.Add("@Email",SqlDbType.NVarChar,100).Value = sEmail.Trim();	
				cmd.Parameters.Add("@Justification",SqlDbType.NVarChar,4000).Value = sJustification.Trim();	
				cmd.Parameters.Add("@E_Name",SqlDbType.NVarChar,100).Value = sE_Name.Trim();	
				cmd.Parameters.Add("@E_Relation",SqlDbType.NVarChar,100).Value = sE_Relation.Trim();	
				cmd.Parameters.Add("@E_Address",SqlDbType.NVarChar,250).Value = sE_Address.Trim();	
				cmd.Parameters.Add("@E_Phone",SqlDbType.NVarChar,100).Value = sE_Phone.Trim();	
				cmd.Parameters.Add("@Duration",SqlDbType.Decimal).Value = sDuration.Trim()==""?0:Decimal.Parse(sDuration.Trim());	
				cmd.Parameters.Add("@TravelDays",SqlDbType.Decimal).Value = sTravelDays.Trim()==""?0:Decimal.Parse(sTravelDays.Trim());	
				
				cmd.ExecuteNonQuery();
				string 	strTrainingRequestID = "";
				strTrainingRequestID = cmd.Parameters["@ReturnMess"].Value.ToString();				
				if (strTrainingRequestID.Length<12)
					strTrainingRequestID = cmd.Parameters["@TrainingRequestID"].Value.ToString();				

					
				sqlTran.Commit();
				return strTrainingRequestID;
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
		#endregion

		#region Update_RequestData
		public static string Update_RequestData(string strActivity, string strEmpID,string strTrainingCourseID,string strTrainingCourseName,string strDescription,string strFromDate,string strToDate
			,int Venue, string strLSNationalityID,string strLSProvinceID,string strProvider,int Type,string strSubmitDate,int Status,string strTrainingRequestID, string strCreater, string strLSTrainingCourseID,string strProvider1,string strCourseNameTranfer, string strLSProviderID, string sLSCurrencyTypeID,bool isMakeTRReport 
			,string strLineManager1,string strLineManager2, string strLineManager3,string sFullPart,string sOrganizer,string sAddress,string sTelFax,string sWebsite,string sEmail,string sJustification,string sE_Name,string sE_Relation,string sE_Address,string sE_Phone, string sDuration ,string sTravelDays)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "TR_spfrmTrainingRequest";
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = strActivity;
				cmd.Parameters.Add("@EmpID",SqlDbType.NVarChar,12).Value = strEmpID.Trim();
				cmd.Parameters.Add("@TrainingRequestID",SqlDbType.NVarChar,12).Value = strTrainingRequestID.Trim();
				cmd.Parameters.Add("@Description",SqlDbType.NVarChar,255).Value= strDescription.Trim();
				cmd.Parameters.Add("@Provider1",SqlDbType.NVarChar,200).Value= strProvider1.Trim();
				
				if(strTrainingCourseID.Trim()=="")
					cmd.Parameters.Add("@TrainingCourseID",SqlDbType.NVarChar,12).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@TrainingCourseID",SqlDbType.NVarChar,12).Value = strTrainingCourseID.Trim();
				if(strLSTrainingCourseID.Trim()=="")
					cmd.Parameters.Add("@LSTrainingCourseID",SqlDbType.NVarChar,12).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@LSTrainingCourseID",SqlDbType.NVarChar,12).Value = strLSTrainingCourseID.Trim();
				if(strLSProviderID.Trim()=="")
					cmd.Parameters.Add("@LSProviderID",SqlDbType.NVarChar,12).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@LSProviderID",SqlDbType.NVarChar,12).Value = strLSProviderID.Trim();
			
				if (sLSCurrencyTypeID.Trim()=="")
					cmd.Parameters.Add("@LSCurrencyTypeID",SqlDbType.NVarChar,12).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@LSCurrencyTypeID",SqlDbType.NVarChar,12).Value = sLSCurrencyTypeID.Trim();				

				if(strTrainingCourseName.Trim()=="")
				{
					cmd.Parameters.Add("@TrainingCourseName",SqlDbType.NVarChar,255).Value= DBNull.Value;
					cmd.Parameters.Add("@CourseNameTranfer",SqlDbType.NVarChar,255).Value= DBNull.Value;
				}
				else
				{
					cmd.Parameters.Add("@TrainingCourseName",SqlDbType.NVarChar,255).Value= strTrainingCourseName.Trim();
					cmd.Parameters.Add("@CourseNameTranfer",SqlDbType.NVarChar,255).Value= strCourseNameTranfer.Trim();
				}

				if(strFromDate.Trim()=="")
					cmd.Parameters.Add("@FromDate",SqlDbType.NVarChar,12).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@FromDate",SqlDbType.NVarChar,12).Value = strFromDate.Trim();
				if(strToDate.Trim()=="")
					cmd.Parameters.Add("@ToDate",SqlDbType.NVarChar,12).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@ToDate",SqlDbType.NVarChar,12).Value = strToDate.Trim();
				
				cmd.Parameters.Add("@Venue",SqlDbType.SmallInt).Value= Venue;
				cmd.Parameters.Add("@LineManager1",SqlDbType.NVarChar,4000).Value = strLineManager1.Trim();				
				cmd.Parameters.Add("@LineManager2",SqlDbType.NVarChar,4000).Value = strLineManager2.Trim();				
				cmd.Parameters.Add("@LineManager3",SqlDbType.NVarChar,4000).Value = strLineManager3.Trim();	

				if(strLSNationalityID.Trim()=="")
					cmd.Parameters.Add("@LSNationalityID",SqlDbType.NVarChar,12).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@LSNationalityID",SqlDbType.NVarChar,12).Value = strLSNationalityID.Trim();

				if(strLSProvinceID.Trim()=="")
					cmd.Parameters.Add("@LSProvinceID",SqlDbType.NVarChar,12).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@LSProvinceID",SqlDbType.NVarChar,12).Value = strLSProvinceID.Trim();				
				
				cmd.Parameters.Add("@Provider",SqlDbType.NVarChar,255).Value = strProvider.Trim();				
				
				cmd.Parameters.Add("@Type",SqlDbType.SmallInt,10).Value = Type;
				//moithem
				cmd.Parameters.Add("@isMakeTRReport",SqlDbType.Bit).Value = isMakeTRReport;
			
				if (strSubmitDate.Trim()=="")
					cmd.Parameters.Add("@SubmitDate",SqlDbType.NVarChar,12).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@SubmitDate",SqlDbType.NVarChar,12).Value = strSubmitDate.Trim();

				cmd.Parameters.Add("@Status",SqlDbType.SmallInt).Value = Status;
		
				//UPdate  Edittor/////////////////////////////////////////
				
				if (strCreater.Trim()=="")
					cmd.Parameters.Add("@Creater",SqlDbType.NVarChar,20).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@Creater",SqlDbType.NVarChar,20).Value = strCreater.Trim();
				
				/////////////////////////////////////////////////////////////////////
				///

				cmd.Parameters.Add("@FullPart",SqlDbType.NVarChar,2).Value = sFullPart.Trim();	
				cmd.Parameters.Add("@Organizer",SqlDbType.NVarChar,250).Value = sOrganizer.Trim();	
				cmd.Parameters.Add("@Address",SqlDbType.NVarChar,250).Value = sAddress.Trim();	
				cmd.Parameters.Add("@TelFax",SqlDbType.NVarChar,50).Value = sTelFax.Trim();	
				cmd.Parameters.Add("@Website",SqlDbType.NVarChar,250).Value = sWebsite.Trim();	
				cmd.Parameters.Add("@Email",SqlDbType.NVarChar,100).Value = sEmail.Trim();	
				cmd.Parameters.Add("@Justification",SqlDbType.NVarChar,4000).Value = sJustification.Trim();	
				cmd.Parameters.Add("@E_Name",SqlDbType.NVarChar,100).Value = sE_Name.Trim();	
				cmd.Parameters.Add("@E_Relation",SqlDbType.NVarChar,100).Value = sE_Relation.Trim();	
				cmd.Parameters.Add("@E_Address",SqlDbType.NVarChar,250).Value = sE_Address.Trim();	
				cmd.Parameters.Add("@E_Phone",SqlDbType.NVarChar,100).Value = sE_Phone.Trim();	
				cmd.Parameters.Add("@Duration",SqlDbType.Int).Value = sDuration.Trim()==""?0:Decimal.Parse(sDuration.Trim());	
				cmd.Parameters.Add("@TravelDays",SqlDbType.Int).Value = sTravelDays.Trim()==""?0:Decimal.Parse(sTravelDays.Trim());	
				
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
		#endregion

		#region Verify by Finance
		public static string VerifyByFinance(string strEmpID,int Status,string strTrainingRequestID)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "TR_spfrmTrainingRequest";
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "VerifyByFin";
				cmd.Parameters.Add("@EmpID",SqlDbType.NVarChar,12).Value = strEmpID.Trim();
				cmd.Parameters.Add("@TrainingRequestID",SqlDbType.NVarChar,12).Value = strTrainingRequestID.Trim();
				cmd.Parameters.Add("@Status",SqlDbType.SmallInt).Value = Status;
				
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
		#endregion

		#region Update_LMRequestData

		public static string Update_LMRequestData(string strActivity,string strLMEmpID,string strLMSender,string strLMDate,string strLineManager1,string strLineManager2,string strLineManager3, string strTrainingRequestID,int Status)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "TR_spfrmTrainingRequest";
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@TrainingRequestID",SqlDbType.NVarChar,12).Value = strTrainingRequestID.Trim();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = strActivity;
				cmd.Parameters.Add("@LMEmpID",SqlDbType.NVarChar,12).Value = strLMEmpID.Trim();
				cmd.Parameters.Add("@LMSender",SqlDbType.NVarChar,500).Value= strLMSender.Trim();		
				cmd.Parameters.Add("@LMDate",SqlDbType.NVarChar,12).Value= strLMDate.Trim();		
				cmd.Parameters.Add("@LineManager1",SqlDbType.NVarChar,4000).Value= strLineManager1.Trim();		
				cmd.Parameters.Add("@LineManager2",SqlDbType.NVarChar,4000).Value= strLineManager2.Trim();				
				cmd.Parameters.Add("@LineManager3",SqlDbType.NVarChar,4000).Value= strLineManager3.Trim();
				cmd.Parameters.Add("@Status",SqlDbType.SmallInt).Value= Status;				
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

		#endregion

		#region Update_RequestLMData

		public static string Update_RequestLMData(string strActivity,string strLineManager1,string strLineManager2,string strLineManager3, string strTrainingRequestID)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "TR_spfrmTrainingRequest";
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@TrainingRequestID",SqlDbType.NVarChar,12).Value = strTrainingRequestID.Trim();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = strActivity;	
				cmd.Parameters.Add("@LineManager1",SqlDbType.NVarChar,4000).Value= strLineManager1.Trim();		
				cmd.Parameters.Add("@LineManager2",SqlDbType.NVarChar,4000).Value= strLineManager2.Trim();				
				cmd.Parameters.Add("@LineManager3",SqlDbType.NVarChar,4000).Value= strLineManager3.Trim();
			
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

		#endregion

		#region Update_HRRequestData
	
		public static string Update_HRRequestData(string strActivity,string strHREmpID,string strHRSender,string strHRDate, string strTrainingRequestID,int Status)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "TR_spfrmTrainingRequest";
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@TrainingRequestID",SqlDbType.NVarChar,12).Value = strTrainingRequestID.Trim();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = strActivity;
				cmd.Parameters.Add("@HREmpID",SqlDbType.NVarChar,12).Value = strHREmpID.Trim();
				cmd.Parameters.Add("@HRSender",SqlDbType.NVarChar,500).Value= strHRSender.Trim();		
				cmd.Parameters.Add("@HRDate",SqlDbType.NVarChar,12).Value= strHRDate.Trim();		
				cmd.Parameters.Add("@Status",SqlDbType.SmallInt).Value= Status;		
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

		

		#endregion

		#region Update_ExcoRequestData
		
	
		public static string Update_ExcoRequestData(string strActivity,string strExcoEmpID,string strExcoSender,string strExcoDate, string strTrainingRequestID, int Status)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "TR_spfrmTrainingRequest";
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@TrainingRequestID",SqlDbType.NVarChar,12).Value = strTrainingRequestID.Trim();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = strActivity;
				cmd.Parameters.Add("@ExcoEmpID",SqlDbType.NVarChar,12).Value = strExcoEmpID.Trim();
				cmd.Parameters.Add("@ExcoSender",SqlDbType.NVarChar,500).Value= strExcoSender.Trim();		
				cmd.Parameters.Add("@ExcoDate",SqlDbType.NVarChar,12).Value= strExcoDate.Trim();		
				cmd.Parameters.Add("@Status",SqlDbType.SmallInt).Value= Status;
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

		

		
		#endregion

		#region CheckEmp
		public static bool CheckEmp(string strEmpID)
		{
			try
			{
				DataRow dtData= clsCommon.GetDataRow("HR_clsCommon @Activity='CheckEmp', @EmpID='" + strEmpID + "'");
				bool IsEmp = false;
				if (dtData[0].ToString()=="0")
					IsEmp = true;
					
				return IsEmp;
			}
			catch
			{
				return false;
			}
		}
		#endregion

		#region checkHREmp
		public static bool CheckHREmp(string pAccountLogin)
		{
			try
			{
				DataRow dtData= clsCommon.GetDataRow("HR_clsCommon @Activity='CheckHREmp', @EmpID='" + pAccountLogin + "'");
				bool IsLineManager = false;
				if (dtData[0].ToString()!="0")
					IsLineManager = true;
					
				return IsLineManager;
			}
			catch
			{
				return false;
			}
		}

		#endregion

		#region checkHRReview
		public static bool CheckHRReview(string pAccountLogin)
		{
			try
			{
				DataRow dtData= clsCommon.GetDataRow("HR_clsCommon @Activity='CheckHRReview', @EmpID='" + pAccountLogin + "'");
				bool IsLineManager = false;
				if (dtData[0].ToString()!="0")
					IsLineManager = true;
					
				return IsLineManager;
			}
			catch
			{
				return false;
			}
		}

		#endregion

		#region CheckFinance
		public static bool CheckFinance(string pAccountLogin)
		{
			try
			{
				DataRow dtData= clsCommon.GetDataRow("HR_clsCommon @Activity='CheckFinance', @EmpID='" + pAccountLogin + "'");
				bool IsLineManager = false;
				if (dtData[0].ToString()!="0")
					IsLineManager = true;
					
				return IsLineManager;
			}
			catch
			{
				return false;
			}
		}

		#endregion

		#region checkExcoEmp
		public static bool CheckExcoEmp(string pAccountLogin)
		{
			try
			{
				DataRow dtData= clsCommon.GetDataRow("HR_clsCommon @Activity='CheckExcoEmp', @EmpID='" + pAccountLogin + "'");
				bool IsLineManager = false;
				if (dtData[0].ToString()!="0")
					IsLineManager = true;
					
				return IsLineManager;
			}
			catch
			{
				return false;
			}
		}

		#endregion

		//CangTT - Change Request 2 - check Dept Head - 16102007 - Key:CR2
		#region CheckDeptHeadEmp
		public static bool CheckDeptHeadEmp(string pAccountLogin)
		{
			try
			{
				DataRow dtData= clsCommon.GetDataRow("HR_clsCommon @Activity='CheckDeptHeadEmp', @EmpID='" + pAccountLogin + "'");
				bool IsLineManager = false;
				if (dtData[0].ToString()!="0")
					IsLineManager = true;
					
				return IsLineManager;
			}
			catch
			{
				return false;
			}
		}

		#endregion
		//--CangTT end-------------------------------------

		#region CheckTrainingCourseID
		public static bool CheckTrainingCourseID(string strLSTrainingCourseID)
		{
			try
			{
				DataRow dtData= clsCommon.GetDataRow("TR_spfrmTrainingCourse @Activity='CheckTrainingCourseID', @TrainingCourseID='" + strLSTrainingCourseID + "'");
				bool IsTR = false;
				if (dtData[0].ToString()!="0")
					IsTR = true;
					
				return IsTR;
			}
			catch
			{
				return false;
			}
		}
		#endregion

		#region GetRequestForEmp
		public static DataTable GetRequestForEmp(string pstrEmpID, string pstrLSTrainingCourseID, string pstrFromDate, string pstrToDate)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb= clsCommon.GetDataTable("TR_spfrmTrainingRequest @Activity = 'GetRequestForEmp', @EmpID = '" + pstrEmpID + "', @TrainingCourseID='" + pstrLSTrainingCourseID + 
					"', @FromDate='" + pstrFromDate + "', @ToDate='" + pstrToDate + "'");
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

		#endregion
		
		#region GetRequestForLM
		public static DataTable GetRequestForLM(string pstrEmpID, string pstrLSTrainingCourseID, string pstrFromDate, string pstrToDate,string pstrCompany,string pstrLevel1,string pstrLevel2,string pstrSource,string pstrStatus, string pstrEmpCodeSearch, string pstrEmpNameSearch, string sLSEmpTypeID,string sLevel3ID, string sLocalExpat, string sLSLocationID)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb= clsCommon.GetDataTable("TR_spfrmTrainingRequestApprove @Activity = 'GetRequestForLM', @EmpID = '" + pstrEmpID + "', @TrainingCourseID='" + pstrLSTrainingCourseID + 
					"', @FromDate='" + pstrFromDate + "', @ToDate='" + pstrToDate + "', @LSCompanyID='" + pstrCompany + "', @Level1ID='" + pstrLevel1 + "', @Level2ID='" + pstrLevel2 + "', @LSRESourceID='" + pstrSource +  "', @StatusEmpSearch='" + pstrStatus +  "', @EmpCodeSearch ='" + pstrEmpCodeSearch +  "', @EmpNameSearch= N'" + pstrEmpNameSearch + "', @LSEmpTypeID ='" + sLSEmpTypeID + "',@Level3ID='" + sLevel3ID + "',@LocalExpat='" + sLocalExpat + "', @LSLocationID='" + sLSLocationID + "'");
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

		#endregion

		#region GetRequestForHR
		public static DataTable GetRequestForHR(string pstrEmpID, string pstrLSTrainingCourseID, string pstrFromDate, string pstrToDate,string pstrCompany,string pstrLevel1,string pstrLevel2,string pstrSource,string pstrStatus, string pstrEmpCodeSearch, string pstrEmpNameSearch, string sLSEmpTypeID, string sLevel3ID, string sLocalExpat, string sLSLocationID)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb= clsCommon.GetDataTable("TR_spfrmTrainingRequest @Activity = 'GetRequestForHR', @EmpID = '" + pstrEmpID + "', @TrainingCourseID='" + pstrLSTrainingCourseID + 
					"', @FromDate='" + pstrFromDate + "', @ToDate='" + pstrToDate + "', @LSCompanyID='" + pstrCompany + "', @Level1ID ='" + pstrLevel1 + "', @Level2ID ='" + pstrLevel2  + "', @LSRESourceID='" + pstrSource +  "', @StatusEmpSearch='" + pstrStatus +  "', @EmpCodeSearch ='" + pstrEmpCodeSearch +  "', @EmpNameSearch= N'" + pstrEmpNameSearch +  "', @LSEmpTypeID ='" + sLSEmpTypeID + "', @Level3ID='" + sLevel3ID + "',@LocalExpat='" + sLocalExpat + "',@LSLocationID='" + sLSLocationID + "'");
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

		public static DataTable GetRequestForHRLeader(string pstrEmpID, string pstrLSTrainingCourseID, string pstrFromDate, string pstrToDate,string pstrCompany,string pstrLevel1,string pstrLevel2,string pstrSource,string pstrStatus, string pstrEmpCodeSearch, string pstrEmpNameSearch, string pstrLevel3, string sLSEmpTypeID, string sLocalExpat, string sLSLocationID,string sStatusRequest)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb= clsCommon.GetDataTable("TR_spfrmTrainingRequest @Activity = 'GetRequestForHRLeader', @EmpID = '" + pstrEmpID + "', @TrainingCourseID='" + pstrLSTrainingCourseID + 
					"', @FromDate='" + pstrFromDate + "', @ToDate='" + pstrToDate + "', @LSCompanyID='" + pstrCompany + "', @Level1ID ='" + pstrLevel1 + "', @Level2ID ='" + pstrLevel2  + "', @LSRESourceID='" + pstrSource +  "', @StatusEmpSearch='" + pstrStatus +  "', @EmpCodeSearch ='" + pstrEmpCodeSearch +  "', @EmpNameSearch= N'" + pstrEmpNameSearch +  "', @Level3ID ='" + pstrLevel3 + "', @LSEmpTypeID='" + sLSEmpTypeID + "',@LocalExpat='" + sLocalExpat + "',@LSLocationID='" + sLSLocationID + "',@StatusRequest='" + sStatusRequest + "'");
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

		public static DataTable GetCancelList()
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb= clsCommon.GetDataTable("TR_spfrmTrainingRequest @Activity = 'GetRequestCancelList'");
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
		#endregion

		#region GetRequestForExco
		public static DataTable GetRequestForExco(string pstrEmpID, string pstrLSTrainingCourseID, string pstrFromDate, string pstrToDate,string pstrCompany,string pstrLevel1,string pstrLevel2,string pstrSource, string pstrStatus, string pstrEmpCodeSearch, string pstrEmpNameSearch, string pstrLevel3)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb= clsCommon.GetDataTable("TR_spfrmTrainingRequestApprove @Activity = 'GetRequestForExco', @EmpID = '" + pstrEmpID + "', @TrainingCourseID='" + pstrLSTrainingCourseID + 
					"', @FromDate='" + pstrFromDate + "', @ToDate='" + pstrToDate + "', @LSCompanyID='" + pstrCompany + "', @Level1ID ='" + pstrLevel1 + "', @Level2ID='" + pstrLevel2 + "', @LSRESourceID='" + pstrSource +  "', @StatusEmpSearch='" + pstrStatus +  "', @EmpCodeSearch ='" + pstrEmpCodeSearch +  "', @EmpNameSearch= N'" + pstrEmpNameSearch + "', @Level3ID ='" + pstrLevel3+ "'");
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

		#endregion		

		#region Support sendmail
		public static string getHRReviewID()
		{
			string strHRReviewID="";
			DataRow drData= clsCommon.GetDataRow("TR_spfrmTrainingRequest @Activity='GetHRReviewID'");
			if (drData!=null)
			{
				strHRReviewID=drData["HRReviewID"].ToString();
			}
			return strHRReviewID;
		}
		public static string getFinanceReviewID()
		{
			string strFinanceReviewID="";
			DataRow drData= clsCommon.GetDataRow("TR_spfrmTrainingRequest @Activity='GetFinanceReviewID'");
			if (drData!=null)
			{
				strFinanceReviewID=drData["FinanceID"].ToString();
			}
			return strFinanceReviewID;
		}
		public static string getCourseName(string sTrainingRequestID)
		{
			DataRow drData= clsCommon.GetDataRow("TR_spfrmTrainingRequest @Activity='getCourseName',@TrainingRequestID='" + sTrainingRequestID + "'");
			return drData["CourseName"].ToString();
		}

		public static string getEmpIDbyTRrequest(string sTrainingRequestID)
		{
			DataRow drData= clsCommon.GetDataRow("TR_spfrmTrainingRequest @Activity='getEmpIDByRequest',@TrainingRequestID='" + sTrainingRequestID + "'");
			return drData["EmpID"].ToString();
		}
		public static string getLMIDbyEmpID(string strEmpID)
		{
			DataRow drData= clsCommon.GetDataRow("TR_spfrmTrainingRequest @Activity='getLMIDByEmpID',@EmpID='" + strEmpID + "'");
			return drData["LineManagerID"].ToString();

		}
		public static string getHRID()
		{
			DataRow drData= clsCommon.GetDataRow("TR_spfrmTrainingRequest @Activity='getHRID'");
			return drData["HRID"].ToString();

		}
		public static string getDeptHeadID(string sEmpID)
		{
			DataRow drData = clsCommon.GetDataRow("TR_spfrmTrainingRequest @Activity='getDeptHeadID',@EmpID='" + sEmpID + "'");
			return drData["DeptHeadID"].ToString();
		}
		public static string getLMDeptHeadID(string sEmpID)
		{
			DataRow drData = clsCommon.GetDataRow("TR_spfrmTrainingRequest @Activity='getLMDeptHeadID',@EmpID='" + sEmpID + "'");
			return drData["LMDeptHeadID"].ToString();
		}
		#endregion
	}
	public class clsTRTrainingRequestApprove
	{		

		#region lay thong tin cua phieu yeu cau yheo nhan vien 
		public static DataRow GetDataByEmpID(Object strEmpID)
		{
			DataRow iRow = clsCommon.GetDataRow("TR_spfrmTrainingRequest @Activity='GetDataByEmpID', @EmpID = N'" + strEmpID + "'");
			if(iRow !=null)
				return iRow;
			else
				return null;
		}
		#endregion

		#region Load_dtgRequestFee
		public static DataTable Load_dtgRequestFee(string pTrainingRequestID, string pLanguageID)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb= clsCommon.GetDataTable("TR_spfrmTrainingRequest @Activity='GetRequestFee', @TrainingRequestID='" + pTrainingRequestID +  "', @LanguageID = '" + pLanguageID + "'");
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
		#endregion

		#region getDataByID
		public static DataRow getDataByID(string pTrainingRequestID)
		{
			DataRow drData;
			try
			{
				drData= clsCommon.GetDataRow("TR_spfrmTrainingRequest @Activity='getDataByID', @TrainingRequestID='" + pTrainingRequestID + "'");
				return drData;
			}
			catch
			{
				return null;
			}			
		}
		#endregion

		#region DeleteDetailsTemp
		public static string DeleteDetailsTemp(string strCurUser)
		{
			string strErr = "";
			try
			{
				clsCommon.GetDataTable("TR_spfrmTrainingRequest @Activity='DeleteDetailsTemp', @Creater='" + strCurUser + "'");
			}
			catch (Exception exp)
			{
				strErr = exp.Message.Trim();
			}
			return strErr;
		}
		#endregion

		#region LoadScheduleDetails

		public static DataTable LoadScheduleDetails(string strLanguage, string strEmpID, string strFromDate, string strToDate,string strTrainingRequestID,string strCurUser)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb = clsCommon.GetDataTable("TR_spfrmTrainingRequest @Activity='LoadScheduleDetails', @LanguageID='" 
					+ strLanguage + "', @EmpID=N'" + strEmpID + "', @FromDate='" + strFromDate + "', @ToDate='" + strToDate 
					+ "', @TrainingRequestID=N'" + strTrainingRequestID + "',@Creater='" + strCurUser + "'");
			}
			catch
			{
				dtb = new DataTable();
			}
			return dtb;
		}
		#endregion

		#region SaveScheduleDetailsTemp
		public static string SaveScheduleDetailsTemp(DataTable dtb, string strEmpID, string strFromDate, string strToDate, string strCurUser)
		{
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "TR_spfrmTrainingRequest";
   
				for (int i = 0;i< dtb.Rows.Count;i++)
				{
					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "SaveSDetailsTemp";						
					cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = strEmpID;
					cmd.Parameters.Add("@FromDate", SqlDbType.NVarChar, 12).Value = strFromDate;
					cmd.Parameters.Add("@ToDate", SqlDbType.NVarChar, 12).Value = strToDate;
					cmd.Parameters.Add("@DateID", SqlDbType.NVarChar, 12).Value = dtb.Rows[i]["DateID"].ToString().Trim();
					cmd.Parameters.Add("@LeaveTaken", SqlDbType.Real).Value = dtb.Rows[i]["LeaveTaken"].ToString().Trim();
					cmd.Parameters.Add("@Creater", SqlDbType.NVarChar, 20).Value = strCurUser;
					cmd.ExecuteNonQuery();											
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
		#endregion

		#region Delete_TrainingFEe
		public static void Delete_TrainingFee(string pTrainingRequestID)
		{
			clsCommon.sExecuteCommandTextTrans("TR_spfrmTrainingRequest @Activity = 'Delete_TrainingFee', @TrainingRequestID = '" + pTrainingRequestID + "'");
		}
		#endregion

		#region Delete_EmpList
		public static void Delete_EmpList(string pTrainingRequestID)
		{
			clsCommon.sExecuteCommandTextTrans("TR_spfrmTrainingRequest @Activity = 'Delete_EmpList', @TrainingRequestID = '" + pTrainingRequestID + "'");
		}
		#endregion

		#region sImpact_TrainingFee	
		public static void sImpact_TrainingFee(string[] arrTrainingFee, string[] arrAmount , string pTrainingRequestID)
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
				cmd.CommandText = "TR_spfrmTrainingRequest";
			
				for(int i=0;i<arrTrainingFee.Length-1;i++)
				{
					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Save_TrainingFee";				
					cmd.Parameters.Add("@TrainingRequestID",SqlDbType.NVarChar,12).Value = pTrainingRequestID;				
					cmd.Parameters.Add("@Amount",SqlDbType.Money).Value = arrAmount.GetValue(i).ToString();				
					cmd.Parameters.Add("@LSTrainingFeeID",SqlDbType.NVarChar,12).Value = arrTrainingFee.GetValue(i).ToString();
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
		#endregion

		#region sImpact_RequestEmpList
		public static void sImpact_RequestEmpList(string strTrainingRequestID, string[] arrEmpID)
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
				cmd.CommandText = "TR_spfrmTrainingRequest";
			
				for(int i=0;i<arrEmpID.Length;i++)
				{
					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "InsertRequestEmp";				
					cmd.Parameters.Add("@TrainingRequestID",SqlDbType.NVarChar,12).Value = strTrainingRequestID;				
					cmd.Parameters.Add("@EmpID",SqlDbType.NVarChar,12).Value = arrEmpID.GetValue(i).ToString();
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
		#endregion

		#region sImpact_RequestEmp
		public static string sImpact_RequestEmp(string strActivity, string strTrainingRequestID,string strEmpID)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "TR_spfrmTrainingRequest";
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = strActivity;
				cmd.Parameters.Add("@EmpID",SqlDbType.NVarChar,12).Value = strEmpID.Trim();
				cmd.Parameters.Add("@TrainingRequestID",SqlDbType.NVarChar,12).Value= strTrainingRequestID.Trim();		
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
		#endregion

		#region Save_RequestData
		public static string Save_RequestData(string strActivity, string strEmpID,string strTrainingCourseID,string strTrainingCourseName,string strDescription,string strFromDate,string strToDate
			,int Venue, string strLSNationalityID,string strLSProvinceID,string strProvider,int Type,string strSubmitDate,int Status, string strCreater,string strLSTrainingCourseID)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "TR_spfrmTrainingRequest";
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = strActivity;
				cmd.Parameters.Add("@EmpID",SqlDbType.NVarChar,12).Value = strEmpID.Trim();
				cmd.Parameters.Add("@Description",SqlDbType.NVarChar,255).Value= strDescription.Trim();
				
				if(strTrainingCourseID.Trim()=="")
				{
					cmd.Parameters.Add("@TrainingCourseID",SqlDbType.NVarChar,12).Value = DBNull.Value;
					
				}
				else
				{
					cmd.Parameters.Add("@TrainingCourseID",SqlDbType.NVarChar,12).Value = strTrainingCourseID.Trim();
					
				}
				if(strLSTrainingCourseID.Trim()=="")
					cmd.Parameters.Add("@LSTrainingCourseID",SqlDbType.NVarChar,12).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@LSTrainingCourseID",SqlDbType.NVarChar,12).Value = strLSTrainingCourseID.Trim();
				if(strTrainingCourseName.Trim()=="")
					cmd.Parameters.Add("@TrainingCourseName",SqlDbType.NVarChar,255).Value= DBNull.Value;
				else
					cmd.Parameters.Add("@TrainingCourseName",SqlDbType.NVarChar,255).Value= strTrainingCourseName.Trim();

				if(strFromDate.Trim()=="")
					cmd.Parameters.Add("@FromDate",SqlDbType.NVarChar,12).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@FromDate",SqlDbType.NVarChar,12).Value = strFromDate.Trim();
				if(strToDate.Trim()=="")
					cmd.Parameters.Add("@ToDate",SqlDbType.NVarChar,12).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@ToDate",SqlDbType.NVarChar,12).Value = strToDate.Trim();
				
				cmd.Parameters.Add("@Venue",SqlDbType.SmallInt).Value= Venue;

				if(strLSNationalityID.Trim()=="")
					cmd.Parameters.Add("@LSNationalityID",SqlDbType.NVarChar,12).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@LSNationalityID",SqlDbType.NVarChar,12).Value = strLSNationalityID.Trim();

				if(strLSProvinceID.Trim()=="")
					cmd.Parameters.Add("@LSProvinceID",SqlDbType.NVarChar,12).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@LSProvinceID",SqlDbType.NVarChar,12).Value = strLSProvinceID.Trim();				
				
				cmd.Parameters.Add("@Provider",SqlDbType.NVarChar,255).Value = strProvider.Trim();				
				
				cmd.Parameters.Add("@Type",SqlDbType.SmallInt,10).Value = Type;
				
				if (strSubmitDate.Trim()=="")
					cmd.Parameters.Add("@SubmitDate",SqlDbType.NVarChar,12).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@SubmitDate",SqlDbType.NVarChar,12).Value = strSubmitDate.Trim();

				cmd.Parameters.Add("@Status",SqlDbType.SmallInt).Value = Status;
		
				//UPdate Creater /////////////////////////////////////////
				
				if (strCreater.Trim()=="")
					cmd.Parameters.Add("@Creater",SqlDbType.NVarChar,20).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@Creater",SqlDbType.NVarChar,20).Value = strCreater.Trim();
				
					
				
				/////////////////////////////////////////////////////////////////////
				cmd.Parameters.Add("@TrainingRequestID", SqlDbType.NVarChar,12).Direction = ParameterDirection.InputOutput;
				cmd.Parameters.Add("@ReturnMess", SqlDbType.NVarChar,255).Direction = ParameterDirection.InputOutput;
				cmd.ExecuteNonQuery();
				string 	strTrainingRequestID = "";
				strTrainingRequestID = cmd.Parameters["@ReturnMess"].Value.ToString();				
				if (strTrainingRequestID.Length<12)
					strTrainingRequestID = cmd.Parameters["@TrainingRequestID"].Value.ToString();				

					
				sqlTran.Commit();
				return strTrainingRequestID;
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
		#endregion

		#region Update_RequestData
		public static string Update_RequestData(string strActivity, string strEmpID,string strTrainingCourseID,string strTrainingCourseName,string strDescription,string strFromDate,string strToDate
			,int Venue, string strLSNationalityID,string strLSProvinceID,string strProvider,int Type,string strSubmitDate,int Status,string strTrainingRequestID, string strCreater, string strLSTrainingCourseID)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "TR_spfrmTrainingRequest";
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = strActivity;
				cmd.Parameters.Add("@EmpID",SqlDbType.NVarChar,12).Value = strEmpID.Trim();
				cmd.Parameters.Add("@TrainingRequestID",SqlDbType.NVarChar,12).Value = strTrainingRequestID.Trim();
				cmd.Parameters.Add("@Description",SqlDbType.NVarChar,255).Value= strDescription.Trim();
				
				if(strTrainingCourseID.Trim()=="")
					cmd.Parameters.Add("@TrainingCourseID",SqlDbType.NVarChar,12).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@TrainingCourseID",SqlDbType.NVarChar,12).Value = strTrainingCourseID.Trim();
				if(strLSTrainingCourseID.Trim()=="")
					cmd.Parameters.Add("@LSTrainingCourseID",SqlDbType.NVarChar,12).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@LSTrainingCourseID",SqlDbType.NVarChar,12).Value = strLSTrainingCourseID.Trim();
				if(strTrainingCourseName.Trim()=="")
					cmd.Parameters.Add("@TrainingCourseName",SqlDbType.NVarChar,255).Value= DBNull.Value;
				else
					cmd.Parameters.Add("@TrainingCourseName",SqlDbType.NVarChar,255).Value= strTrainingCourseName.Trim();

				if(strFromDate.Trim()=="")
					cmd.Parameters.Add("@FromDate",SqlDbType.NVarChar,12).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@FromDate",SqlDbType.NVarChar,12).Value = strFromDate.Trim();
				if(strToDate.Trim()=="")
					cmd.Parameters.Add("@ToDate",SqlDbType.NVarChar,12).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@ToDate",SqlDbType.NVarChar,12).Value = strToDate.Trim();
				
				cmd.Parameters.Add("@Venue",SqlDbType.SmallInt).Value= Venue;

				if(strLSNationalityID.Trim()=="")
					cmd.Parameters.Add("@LSNationalityID",SqlDbType.NVarChar,12).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@LSNationalityID",SqlDbType.NVarChar,12).Value = strLSNationalityID.Trim();

				if(strLSProvinceID.Trim()=="")
					cmd.Parameters.Add("@LSProvinceID",SqlDbType.NVarChar,12).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@LSProvinceID",SqlDbType.NVarChar,12).Value = strLSProvinceID.Trim();				
				
				cmd.Parameters.Add("@Provider",SqlDbType.NVarChar,255).Value = strProvider.Trim();				
				
				cmd.Parameters.Add("@Type",SqlDbType.SmallInt,10).Value = Type;
				
				if (strSubmitDate.Trim()=="")
					cmd.Parameters.Add("@SubmitDate",SqlDbType.NVarChar,12).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@SubmitDate",SqlDbType.NVarChar,12).Value = strSubmitDate.Trim();

				cmd.Parameters.Add("@Status",SqlDbType.SmallInt).Value = Status;
		
				//UPdate  Edittor/////////////////////////////////////////
				
				if (strCreater.Trim()=="")
					cmd.Parameters.Add("@Creater",SqlDbType.NVarChar,20).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@Creater",SqlDbType.NVarChar,20).Value = strCreater.Trim();
				
				/////////////////////////////////////////////////////////////////////
				
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
		#endregion

		#region Update_LMRequestData

		public static string Update_LMRequestData(string strActivity,string strLMEmpID,string strLMSender,string strLMDate,string strLineManager1,string strLineManager2,string strLineManager3, string strTrainingRequestID,int Status)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "TR_spfrmTrainingRequest";
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@TrainingRequestID",SqlDbType.NVarChar,12).Value = strTrainingRequestID.Trim();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = strActivity;
				cmd.Parameters.Add("@LMEmpID",SqlDbType.NVarChar,12).Value = strLMEmpID.Trim();
				cmd.Parameters.Add("@LMSender",SqlDbType.NVarChar,500).Value= strLMSender.Trim();		
				cmd.Parameters.Add("@LMDate",SqlDbType.NVarChar,12).Value= strLMDate.Trim();		
				cmd.Parameters.Add("@LineManager1",SqlDbType.NVarChar,4000).Value= strLineManager1.Trim();		
				cmd.Parameters.Add("@LineManager2",SqlDbType.NVarChar,4000).Value= strLineManager2.Trim();				
				cmd.Parameters.Add("@LineManager3",SqlDbType.NVarChar,4000).Value= strLineManager3.Trim();
				cmd.Parameters.Add("@Status",SqlDbType.SmallInt).Value= Status;				
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

		#endregion

	

		#region Update_HRRequestData
	
		public static string Update_HRRequestData(string strActivity,string strHREmpID,string strHRSender,string strHRDate, string strTrainingRequestID,int Status)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "TR_spfrmTrainingRequest";
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@TrainingRequestID",SqlDbType.NVarChar,12).Value = strTrainingRequestID.Trim();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = strActivity;
				cmd.Parameters.Add("@HREmpID",SqlDbType.NVarChar,12).Value = strHREmpID.Trim();
				cmd.Parameters.Add("@HRSender",SqlDbType.NVarChar,500).Value= strHRSender.Trim();		
				cmd.Parameters.Add("@HRDate",SqlDbType.NVarChar,12).Value= strHRDate.Trim();		
				cmd.Parameters.Add("@Status",SqlDbType.SmallInt).Value= Status;		
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

		

		#endregion

		#region Update_ExcoRequestData
		
	
		public static string Update_ExcoRequestData(string strActivity,string strExcoEmpID,string strExcoSender,string strExcoDate, string strTrainingRequestID, int Status)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "TR_spfrmTrainingRequest";
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@TrainingRequestID",SqlDbType.NVarChar,12).Value = strTrainingRequestID.Trim();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = strActivity;
				cmd.Parameters.Add("@ExcoEmpID",SqlDbType.NVarChar,12).Value = strExcoEmpID.Trim();
				cmd.Parameters.Add("@ExcoSender",SqlDbType.NVarChar,500).Value= strExcoSender.Trim();		
				cmd.Parameters.Add("@ExcoDate",SqlDbType.NVarChar,12).Value= strExcoDate.Trim();		
				cmd.Parameters.Add("@Status",SqlDbType.SmallInt).Value= Status;
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

		

		
		#endregion

		#region CheckEmp
		public static bool CheckEmp(string strEmpID)
		{
			try
			{
				DataRow dtData= clsCommon.GetDataRow("HR_clsCommon @Activity='CheckEmp', @EmpID='" + strEmpID + "'");
				bool IsEmp = false;
				if (dtData[0].ToString()=="0")
					IsEmp = true;
					
				return IsEmp;
			}
			catch
			{
				return false;
			}
		}
		#endregion

		#region checkHREmp
		public static bool CheckHREmp(string pAccountLogin)
		{
			try
			{
				DataRow dtData= clsCommon.GetDataRow("HR_clsCommon @Activity='CheckHREmp', @EmpID='" + pAccountLogin + "'");
				bool IsLineManager = false;
				if (dtData[0].ToString()!="0")
					IsLineManager = true;
					
				return IsLineManager;
			}
			catch
			{
				return false;
			}
		}

		#endregion

		#region checkExcoEmp
		public static bool CheckExcoEmp(string pAccountLogin)
		{
			try
			{
				DataRow dtData= clsCommon.GetDataRow("HR_clsCommon @Activity='CheckExcoEmp', @EmpID='" + pAccountLogin + "'");
				bool IsLineManager = false;
				if (dtData[0].ToString()!="0")
					IsLineManager = true;
					
				return IsLineManager;
			}
			catch
			{
				return false;
			}
		}

		#endregion

		#region CheckTrainingCourseID
		public static bool CheckTrainingCourseID(string strLSTrainingCourseID)
		{
			try
			{
				DataRow dtData= clsCommon.GetDataRow("TR_spfrmTrainingCourse @Activity='CheckTrainingCourseID', @TrainingCourseID='" + strLSTrainingCourseID + "'");
				bool IsTR = false;
				if (dtData[0].ToString()!="0")
					IsTR = true;
					
				return IsTR;
			}
			catch
			{
				return false;
			}
		}
		#endregion

		#region GetRequestForEmp
		public static DataTable GetRequestForEmp(string pstrEmpID, string pstrLSTrainingCourseID, string pstrFromDate, string pstrToDate)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb= clsCommon.GetDataTable("TR_spfrmTrainingRequest @Activity = 'GetRequestForEmp', @EmpID = '" + pstrEmpID + "', @TrainingCourseID='" + pstrLSTrainingCourseID + 
					"', @FromDate='" + pstrFromDate + "', @ToDate='" + pstrToDate + "'");
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

		#endregion
		
		#region GetRequestForLM
		public static DataTable GetRequestForLM(string pstrEmpID, string pstrLSTrainingCourseID, string pstrFromDate, string pstrToDate,string pstrCompany,string pstrLevel1,string pstrLevel2,string pstrSource,string pstrStatus, string pstrEmpCodeSearch, string pstrEmpNameSearch, string pstrLevel3, string pstrApprove, string sLSEmpTypeID, string sLocalExpat, string sLSLocationID)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb= clsCommon.GetDataTable("TR_spfrmTrainingRequestApprove @Activity = 'GetRequestForLM', @EmpID = '" + pstrEmpID + "', @TrainingCourseID='" + pstrLSTrainingCourseID + 
					"', @FromDate='" + pstrFromDate + "', @ToDate='" + pstrToDate + "', @LSCompanyID='" + pstrCompany + "', @Level1ID='" + pstrLevel1 + "', @Level2ID='" + pstrLevel2 + "', @LSRESourceID='" + pstrSource +  "', @StatusEmpSearch='" + pstrStatus +  "', @EmpCodeSearch ='" + pstrEmpCodeSearch +  "', @EmpNameSearch= N'" + pstrEmpNameSearch + "', @Level3ID ='" + pstrLevel3 + "',@Approve=" + pstrApprove + ",@LSEmpTypeID='" + sLSEmpTypeID + "',@LocalExpat='" + sLocalExpat + "',@LSLocationID='" + sLSLocationID + "'");
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

		#endregion

		#region GetRequestForLMHR
		public static DataTable GetRequestForLMHR(string pstrEmpID, string pstrLSTrainingCourseID, string pstrFromDate, string pstrToDate,string pstrCompany,string pstrLevel1,string pstrLevel2,string pstrSource,string pstrStatus, string pstrEmpCodeSearch, string pstrEmpNameSearch, string pstrLevel3, string pstrApprove,string sLSEmpTypeID, string sLocalExpat, string LSLocationID)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb= clsCommon.GetDataTable("TR_spfrmTrainingRequestApprove @Activity = 'GetRequestForLMHR', @EmpID = '" + pstrEmpID + "', @TrainingCourseID='" + pstrLSTrainingCourseID + 
					"', @FromDate='" + pstrFromDate + "', @ToDate='" + pstrToDate + "', @LSCompanyID='" + pstrCompany + "', @Level1ID='" + pstrLevel1 + "', @Level2ID='" + pstrLevel2 + "', @LSRESourceID='" + pstrSource +  "', @StatusEmpSearch='" + pstrStatus +  "', @EmpCodeSearch ='" + pstrEmpCodeSearch +  "', @EmpNameSearch= N'" + pstrEmpNameSearch + "', @Level3ID ='" + pstrLevel3 + "',@Approve=" + pstrApprove + ",@LSEmpTypeID='" + sLSEmpTypeID + "',@LocalExpat='" + sLocalExpat + "',@LSLocationID='" + LSLocationID + "'");
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

		#endregion

		#region GetRequestForLMExco
		public static DataTable GetRequestForLMExco(string pstrEmpID, string pstrLSTrainingCourseID, string pstrFromDate, string pstrToDate,string pstrCompany,string pstrLevel1,string pstrLevel2,string pstrSource,string pstrStatus, string pstrEmpCodeSearch, string pstrEmpNameSearch, string pstrLevel3, string pstrApprove,string sLSEmpTypeID, string sLocalExpat, string LSLocationID)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb= clsCommon.GetDataTable("TR_spfrmTrainingRequestApprove @Activity = 'GetRequestForLMExco', @EmpID = '" + pstrEmpID + "', @TrainingCourseID='" + pstrLSTrainingCourseID + 
					"', @FromDate='" + pstrFromDate + "', @ToDate='" + pstrToDate + "', @LSCompanyID='" + pstrCompany + "', @Level1ID='" + pstrLevel1 + "', @Level2ID='" + pstrLevel2 + "', @LSRESourceID='" + pstrSource +  "', @StatusEmpSearch='" + pstrStatus +  "', @EmpCodeSearch ='" + pstrEmpCodeSearch +  "', @EmpNameSearch= N'" + pstrEmpNameSearch + "', @Level3ID ='" + pstrLevel3 + "',@Approve=" + pstrApprove + ",@LSEmpTypeID='" + sLSEmpTypeID + "',@LocalExpat='" + sLocalExpat + "',@LSLocationID='" + LSLocationID + "'");
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

		#endregion

		#region GetRequestForHRExco
		public static DataTable GetRequestForHRExco(string pstrEmpID, string pstrLSTrainingCourseID, string pstrFromDate, string pstrToDate,string pstrCompany,string pstrLevel1,string pstrLevel2,string pstrSource,string pstrStatus, string pstrEmpCodeSearch, string pstrEmpNameSearch, string pstrLevel3, string pstrApprove,string sLSEmpTypeID, string sLocalExpat, string LSLocationID)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb= clsCommon.GetDataTable("TR_spfrmTrainingRequestApprove @Activity = 'GetRequestForHRExco', @EmpID = '" + pstrEmpID + "', @TrainingCourseID='" + pstrLSTrainingCourseID + 
					"', @FromDate='" + pstrFromDate + "', @ToDate='" + pstrToDate + "', @LSCompanyID='" + pstrCompany + "', @Level1ID='" + pstrLevel1 + "', @Level2ID='" + pstrLevel2 + "', @LSRESourceID='" + pstrSource +  "', @StatusEmpSearch='" + pstrStatus +  "', @EmpCodeSearch ='" + pstrEmpCodeSearch +  "', @EmpNameSearch= N'" + pstrEmpNameSearch + "', @Level3ID ='" + pstrLevel3 + "',@Approve=" + pstrApprove + ",@LSEmpTypeID='" + sLSEmpTypeID + "',@LocalExpat='" + sLocalExpat + "',@LSLocationID='" + LSLocationID + "'");
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

		#endregion

		#region GetRequestForLMHRExco
		public static DataTable GetRequestForLMHRExco(string pstrEmpID, string pstrLSTrainingCourseID, string pstrFromDate, string pstrToDate,string pstrCompany,string pstrLevel1,string pstrLevel2,string pstrSource,string pstrStatus, string pstrEmpCodeSearch, string pstrEmpNameSearch, string pstrLevel3, string pstrApprove,string sLSEmpTypeID, string sLocalExpat, string LSLocationID)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb= clsCommon.GetDataTable("TR_spfrmTrainingRequestApprove @Activity = 'GetRequestForLMHRExco', @EmpID = '" + pstrEmpID + "', @TrainingCourseID='" + pstrLSTrainingCourseID + 
					"', @FromDate='" + pstrFromDate + "', @ToDate='" + pstrToDate + "', @LSCompanyID='" + pstrCompany + "', @Level1ID='" + pstrLevel1 + "', @Level2ID='" + pstrLevel2 + "', @LSRESourceID='" + pstrSource +  "', @StatusEmpSearch='" + pstrStatus +  "', @EmpCodeSearch ='" + pstrEmpCodeSearch +  "', @EmpNameSearch= N'" + pstrEmpNameSearch + "', @Level3ID ='" + pstrLevel3 + "',@Approve=" + pstrApprove + ",@LSEmpTypeID='" + sLSEmpTypeID + "',@LocalExpat='" + sLocalExpat + "',@LSLocationID='" + LSLocationID + "'");
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

		#endregion

		#region GetRequestForHR
		public static DataTable GetRequestForHR(string pstrEmpID, string pstrLSTrainingCourseID, string pstrFromDate, string pstrToDate,string pstrCompany,string pstrLevel1,string pstrLevel2,string pstrSource,string pstrStatus, string pstrEmpCodeSearch, string pstrEmpNameSearch, string pstrLevel3, string pstrApprove,string sLSEmpTypeID, string sLocalExpat, string LSLocationID)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb= clsCommon.GetDataTable("TR_spfrmTrainingRequestApprove @Activity = 'GetRequestForHR', @EmpID = '" + pstrEmpID + "', @TrainingCourseID='" + pstrLSTrainingCourseID + 
					"', @FromDate='" + pstrFromDate + "', @ToDate='" + pstrToDate + "', @LSCompanyID='" + pstrCompany + "', @Level1ID ='" + pstrLevel1 + "', @Level2ID ='" + pstrLevel2  + "', @LSRESourceID='" + pstrSource +  "', @StatusEmpSearch='" + pstrStatus +  "', @EmpCodeSearch ='" + pstrEmpCodeSearch +  "', @EmpNameSearch= N'" + pstrEmpNameSearch +  "', @Level3ID ='" + pstrLevel3 + "',@Approve=" + pstrApprove + ",@LSEmpTypeID='" + sLSEmpTypeID + "',@LocalExpat='" + sLocalExpat + "',@LSLocationID='" + LSLocationID + "'");
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

		#endregion

		#region GetRequestForExco
		public static DataTable GetRequestForExco(string pstrEmpID, string pstrLSTrainingCourseID, string pstrFromDate, string pstrToDate,string pstrCompany,string pstrLevel1,string pstrLevel2,string pstrSource, string pstrStatus, string pstrEmpCodeSearch, string pstrEmpNameSearch, string pstrLevel3, string pstrApprove,string sLSEmpTypeID, string sLocalExpat, string LSLocationID)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb= clsCommon.GetDataTable("TR_spfrmTrainingRequestApprove @Activity = 'GetRequestForExco', @EmpID = '" + pstrEmpID + "', @TrainingCourseID='" + pstrLSTrainingCourseID + 
					"', @FromDate='" + pstrFromDate + "', @ToDate='" + pstrToDate + "', @LSCompanyID='" + pstrCompany + "', @Level1ID ='" + pstrLevel1 + "', @Level2ID='" + pstrLevel2 + "', @LSRESourceID='" + pstrSource +  "', @StatusEmpSearch='" + pstrStatus +  "', @EmpCodeSearch ='" + pstrEmpCodeSearch +  "', @EmpNameSearch= N'" + pstrEmpNameSearch + "', @Level3ID ='" + pstrLevel3+ "',@Approve=" + pstrApprove + ",@LSEmpTypeID='" + sLSEmpTypeID + "',@LocalExpat='" + sLocalExpat + "',@LSLocationID='" + LSLocationID + "'");
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

		#endregion		

		//CangTT - Change Request 2 - check Dept Head - 16102007 - Key:CR2
		#region GetRequestForDeptHead
		public static DataTable GetRequestForDeptHead(string pstrEmpID, string pstrLSTrainingCourseID, string pstrFromDate, string pstrToDate,string pstrCompany,string pstrLevel1,string pstrLevel2,string pstrSource,string pstrStatus, string pstrEmpCodeSearch, string pstrEmpNameSearch, string pstrLevel3, string pstrApprove, string sLSEmpTypeID, string sLocalExpat, string sLSLocationID)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb= clsCommon.GetDataTable("TR_spfrmTrainingRequestApprove @Activity = 'GetRequestForDeptHead', @EmpID = '" + pstrEmpID + "', @TrainingCourseID='" + pstrLSTrainingCourseID + 
					"', @FromDate='" + pstrFromDate + "', @ToDate='" + pstrToDate + "', @LSCompanyID='" + pstrCompany + "', @Level1ID='" + pstrLevel1 + "', @Level2ID='" + pstrLevel2 + "', @LSRESourceID='" + pstrSource +  "', @StatusEmpSearch='" + pstrStatus +  "', @EmpCodeSearch ='" + pstrEmpCodeSearch +  "', @EmpNameSearch= N'" + pstrEmpNameSearch + "', @Level3ID ='" + pstrLevel3 + "',@Approve=" + pstrApprove + ",@LSEmpTypeID='" + sLSEmpTypeID + "',@LocalExpat='" + sLocalExpat + "',@LSLocationID='" + sLSLocationID + "'");
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

		#endregion		

		#region GetRequestForLMDeptHead
		public static DataTable GetRequestForLMDeptHead(string pstrEmpID, string pstrLSTrainingCourseID, string pstrFromDate, string pstrToDate,string pstrCompany,string pstrLevel1,string pstrLevel2,string pstrSource,string pstrStatus, string pstrEmpCodeSearch, string pstrEmpNameSearch, string pstrLevel3, string pstrApprove, string sLSEmpTypeID, string sLocalExpat, string sLSLocationID)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb= clsCommon.GetDataTable("TR_spfrmTrainingRequestApprove @Activity = 'GetRequestForLMDeptHead', @EmpID = '" + pstrEmpID + "', @TrainingCourseID='" + pstrLSTrainingCourseID + 
					"', @FromDate='" + pstrFromDate + "', @ToDate='" + pstrToDate + "', @LSCompanyID='" + pstrCompany + "', @Level1ID='" + pstrLevel1 + "', @Level2ID='" + pstrLevel2 + "', @LSRESourceID='" + pstrSource +  "', @StatusEmpSearch='" + pstrStatus +  "', @EmpCodeSearch ='" + pstrEmpCodeSearch +  "', @EmpNameSearch= N'" + pstrEmpNameSearch + "', @Level3ID ='" + pstrLevel3 + "',@Approve=" + pstrApprove + ",@LSEmpTypeID='" + sLSEmpTypeID + "',@LocalExpat='" + sLocalExpat + "',@LSLocationID='" + sLSLocationID + "'");
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

		#endregion

		#region GetRequestForLMHRDeptHead
		public static DataTable GetRequestForLMHRDeptHead(string pstrEmpID, string pstrLSTrainingCourseID, string pstrFromDate, string pstrToDate,string pstrCompany,string pstrLevel1,string pstrLevel2,string pstrSource,string pstrStatus, string pstrEmpCodeSearch, string pstrEmpNameSearch, string pstrLevel3, string pstrApprove, string sLSEmpTypeID, string sLocalExpat, string sLSLocationID)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb= clsCommon.GetDataTable("TR_spfrmTrainingRequestApprove @Activity = 'GetRequestForLMHRDeptHead', @EmpID = '" + pstrEmpID + "', @TrainingCourseID='" + pstrLSTrainingCourseID + 
					"', @FromDate='" + pstrFromDate + "', @ToDate='" + pstrToDate + "', @LSCompanyID='" + pstrCompany + "', @Level1ID='" + pstrLevel1 + "', @Level2ID='" + pstrLevel2 + "', @LSRESourceID='" + pstrSource +  "', @StatusEmpSearch='" + pstrStatus +  "', @EmpCodeSearch ='" + pstrEmpCodeSearch +  "', @EmpNameSearch= N'" + pstrEmpNameSearch + "', @Level3ID ='" + pstrLevel3 + "',@Approve=" + pstrApprove + ",@LSEmpTypeID='" + sLSEmpTypeID + "',@LocalExpat='" + sLocalExpat + "',@LSLocationID='" + sLSLocationID + "'");
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

		#endregion

		#region GetRequestForLMHRExcoDeptHead
		public static DataTable GetRequestForLMHRExcoDeptHead(string pstrEmpID, string pstrLSTrainingCourseID, string pstrFromDate, string pstrToDate,string pstrCompany,string pstrLevel1,string pstrLevel2,string pstrSource,string pstrStatus, string pstrEmpCodeSearch, string pstrEmpNameSearch, string pstrLevel3, string pstrApprove, string sLSEmpTypeID, string sLocalExpat, string sLSLocationID)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb= clsCommon.GetDataTable("TR_spfrmTrainingRequestApprove @Activity = 'GetRequestForLMHRExcoDeptHead', @EmpID = '" + pstrEmpID + "', @TrainingCourseID='" + pstrLSTrainingCourseID + 
					"', @FromDate='" + pstrFromDate + "', @ToDate='" + pstrToDate + "', @LSCompanyID='" + pstrCompany + "', @Level1ID='" + pstrLevel1 + "', @Level2ID='" + pstrLevel2 + "', @LSRESourceID='" + pstrSource +  "', @StatusEmpSearch='" + pstrStatus +  "', @EmpCodeSearch ='" + pstrEmpCodeSearch +  "', @EmpNameSearch= N'" + pstrEmpNameSearch + "', @Level3ID ='" + pstrLevel3 + "',@Approve=" + pstrApprove + ",@LSEmpTypeID='" + sLSEmpTypeID + "',@LocalExpat='" + sLocalExpat + "',@LSLocationID='" + sLSLocationID + "'");
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

		#endregion
		
		#region GetRequestForLMHRExcoDeptHead
		public static DataTable GetRequestForLMDeptHeadExco(string pstrEmpID, string pstrLSTrainingCourseID, string pstrFromDate, string pstrToDate,string pstrCompany,string pstrLevel1,string pstrLevel2,string pstrSource,string pstrStatus, string pstrEmpCodeSearch, string pstrEmpNameSearch, string pstrLevel3, string pstrApprove, string sLSEmpTypeID, string sLocalExpat, string sLSLocationID)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb= clsCommon.GetDataTable("TR_spfrmTrainingRequestApprove @Activity = 'GetRequestForLMDeptHeadExco', @EmpID = '" + pstrEmpID + "', @TrainingCourseID='" + pstrLSTrainingCourseID + 
					"', @FromDate='" + pstrFromDate + "', @ToDate='" + pstrToDate + "', @LSCompanyID='" + pstrCompany + "', @Level1ID='" + pstrLevel1 + "', @Level2ID='" + pstrLevel2 + "', @LSRESourceID='" + pstrSource +  "', @StatusEmpSearch='" + pstrStatus +  "', @EmpCodeSearch ='" + pstrEmpCodeSearch +  "', @EmpNameSearch= N'" + pstrEmpNameSearch + "', @Level3ID ='" + pstrLevel3 + "',@Approve=" + pstrApprove + ",@LSEmpTypeID='" + sLSEmpTypeID + "',@LocalExpat='" + sLocalExpat + "',@LSLocationID='" + sLSLocationID + "'");
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

		#endregion
		//End CangTT----------------------------------------------------------------

		#region GetFeeForHRApprove
		public static DataTable GetFeeForHRApprove(string pstrEmpID, string pstrLSTrainingCourseID, string pstrFromDate, string pstrToDate,string pstrCompany,string pstrLevel1,string pstrLevel2,string pstrSource,string pstrStatus, string pstrEmpCodeSearch, string pstrEmpNameSearch, string pstrLevel3, string pstrApprove, string sLSEmpTypeID, string sLocalExpat, string sLSLocationID)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb= clsCommon.GetDataTable("TR_spfrmTrainingRequestApprove @Activity = 'GetFeeForHRApprove', @EmpID = '" + pstrEmpID + "', @TrainingCourseID='" + pstrLSTrainingCourseID + 
					"', @FromDate='" + pstrFromDate + "', @ToDate='" + pstrToDate + "', @LSCompanyID='" + pstrCompany + "', @Level1ID='" + pstrLevel1 + "', @Level2ID='" + pstrLevel2 + "', @LSRESourceID='" + pstrSource +  "', @StatusEmpSearch='" + pstrStatus +  "', @EmpCodeSearch ='" + pstrEmpCodeSearch +  "', @EmpNameSearch= N'" + pstrEmpNameSearch + "', @Level3ID ='" + pstrLevel3 + "',@Approve=" + pstrApprove + ",@LSEmpTypeID='" + sLSEmpTypeID + "',@LocalExpat='" + sLocalExpat + "',@LSLocationID='" + sLSLocationID + "'");
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

		#endregion
	}
}
