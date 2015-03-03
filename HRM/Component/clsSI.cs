using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.SessionState;
using iHRPCore.Com;
using System.Web.Mail;
using System.Net;
using System.IO;
using System.Collections;

namespace iHRPCore.SIComponent
{
	/// <summary>
	/// Summary description for clsSI.
	/// </summary>
	
	#region clsSICommon
	public class clsSI
	{
		/// <summary>
		/// Load thong tin cho combo noi cap
		/// </summary>
		public static void LoadComboIssuedPlace(DropDownList pControl, string strTextField)
		{
			clsCommon.LoadDropDownListControl(pControl,"sp_GetDataCombo @TableName = 'LS_tblProvince', @Fields = 'LSProvinceID," + strTextField + " as Name'","LSProvinceID","Name",true);
		}

		public static void LoadComboLocation(DropDownList pControl, string strTextField)
		{
			clsCommon.LoadDropDownListControl(pControl,"sp_GetDataCombo @TableName = 'LS_tblLocation', @Fields = 'LSLocationID," + strTextField + " as Name'","LSLocationID","Name",true);
		}

		public static void LoadComboHospital(DropDownList pControl,  string strTextField)
		{
			clsCommon.LoadDropDownListControl(pControl,"sp_GetDataCombo @TableName='LS_tblHospital',@Fields='LSHospitalID as [ID]," + strTextField + " as Name'","ID","Name",true);
		}

		public static void LoadComboPlaceTreatment(DropDownList pControl, string strTextField, string strEmpID)
		{
			//clsCommon.LoadDropDownListControl(pControl,"sp_GetDataCombo @TableName = 'LS_tblHospital', @Fields = 'LSHospitalID," + strTextField + " as Name'","LSHospitalID","Name",true);
			clsCommon.LoadDropDownListControl(pControl,"SI_spfrmHI @Activity = 'GetHospital', @EmpID = N'" + strEmpID + "'","LSHospitalID","Name",true);
		}

		public static void LoadComboPlaceTreatmentByLocation(DropDownList pControl, string strTextField, string strLocationID)
		{
			clsCommon.LoadDropDownListControl(pControl,"sp_GetDataCombo @TableName = 'LS_tblHospital', @Fields = 'LSHospitalID," + strTextField + " as Name', @Where = ' and LocationID = N''" + strLocationID.Trim() + "'''","LSHospitalID","Name",true);
		}

		public static string GetEmpIDFromCode(string strEmpCode)
		{
			string strReturn = "";
			try
			{
				string strSql = "SI_spCommon @Activity = 'GetEmpIDFromCode', @EmpCode = '" + strEmpCode + "'";
			
				DataRow dr = clsCommon.GetDataRow(strSql);
				if (dr != null)
					strReturn = dr[0].ToString();

				return strReturn;
			}
			catch (Exception exp)
			{
				return exp.Message.Trim();
			}		
		}

		public static string GetEmpCodeFromID(string strEmpID)
		{
			string strReturn = "";
			try
			{
				string strSql = "SI_spCommon @Activity = 'GetEmpCodeFromID', @EmpID = '" + strEmpID + "'";
			
				DataRow dr = clsCommon.GetDataRow(strSql);
				if (dr != null)
					strReturn = dr[0].ToString();

				return strReturn;
			}
			catch (Exception exp)
			{
				return exp.Message.Trim();
			}		
		}

		#region Bao cao theo chu ky 6 thang
		public static DataTable GetRecruitIntendTable(string RecruitIntendID)
		{
			DataTable dtb = clsCommon.GetDataTable("SI_spfrmRecruitIntend 'Select', @RecruitIntendID = '" + RecruitIntendID + "'");
			return dtb;
		}

		public static DataTable GetRecruitIntendTable()
		{
			DataTable dtb = clsCommon.GetDataTable("SI_spfrmRecruitIntend 'SelectAll'");
			return dtb;
		}

		public static void SI_spfrmRecruitIntend(string ActionMode, string RecruitIntendID, string Year, string Ky, string Total, string TuTuyen, string QuaTT)
		{
			SqlCommand cmd = new SqlCommand("SI_spfrmRecruitIntend");
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				conn.Open();
				cmd.Connection = conn;
				cmd.Transaction = conn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Clear();
				
				cmd.Parameters.Add("@ActionMode",SqlDbType.NVarChar,50).Value = ActionMode;
				cmd.Parameters.Add("@RecruitIntendID",SqlDbType.Int).Value = (RecruitIntendID == "")?"0":RecruitIntendID;
				cmd.Parameters.Add("@Year",SqlDbType.Int).Value = (Year == "")?"0":Year;
				cmd.Parameters.Add("@Ky",SqlDbType.SmallInt).Value = (Ky == "")?"0":Ky;
				cmd.Parameters.Add("@Total",SqlDbType.Int).Value = (Total == "")?"0":Total;
				cmd.Parameters.Add("@TuTuyen",SqlDbType.Int).Value = (TuTuyen == "")?"0":TuTuyen;
				cmd.Parameters.Add("@QuaTT",SqlDbType.Int).Value = (QuaTT == "")?"0":QuaTT;

				cmd.ExecuteNonQuery();
				cmd.Transaction.Commit();
			}
			catch(Exception ex)
			{
				cmd.Transaction.Rollback();
				throw ex;
			}
			finally
			{
				cmd.Dispose();
				if(conn.State == ConnectionState.Open) conn.Close();
				conn.Dispose();
			}
		}
		#endregion Bao cao theo chu ky 6 thang
	}
	#endregion

	#region Frm: Social Insurance
	public class clsSISocialIns
	{
		public static DataTable GetSIList(string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strEmpType, string strJobCode, string strLocation, string strCompany, 
			string strStatus, string strFromDate, string strToDate, string strStartFromDate, string strStartToDate, 
			string strLastFromDate, string strLastToDate, string strLSContractTypeID,string strAccountLogin)
		{
			string strSql = "SI_spfrmSI @Activity = 'GetSIList', @EmpID = N'" + strEmpID + "', @EmpName = N'" + strEmpName
				+ "', @LSLevel1ID = '" + strLevel1 + "', @LSLevel2ID = '" + strLevel2 + "', @LSLevel3ID = '" + strLevel3
				+ "', @LSEmpTypeID = '" + strEmpType + "', @LSJobCodeID = '" + strJobCode 
				+ "', @LSLocationID = '" + strLocation + "', @LSCompanyID = '" + strCompany 
				+ "', @FromDate = '" + strFromDate+ "', @ToDate = '" + strToDate 
				+ "', @StartFromDate = '" + strStartFromDate+ "', @StartToDate = '" + strStartToDate 
				+ "', @LastFromDate = '" + strLastFromDate + "', @LastToDate = '" + strLastToDate 
				+ "', @UserGroupID = '" + strAccountLogin
				+ "', @LSContractTypeID = '" + strLSContractTypeID + "'";
			
			if (strStatus != "")
				strSql = strSql + ", @Status = '" + strStatus + "'";
			
			DataTable dtb = clsCommon.GetDataTable(strSql);
			return dtb;
		}

		public static DataTable GetSIRecord(string strEmpID)
		{
			DataTable dtb = clsCommon.GetDataTable("SI_spfrmSIRecord 'GetAllData', @EmpID = '" + strEmpID + "'");
			return dtb;
		}

		public static DataTable GetSIRecordAtComp(string strEmpID)
		{
			DataTable dtb = clsCommon.GetDataTable("SI_spfrmSIRecord 'GetRecordAtComp', @EmpID = '" + strEmpID + "'");
			return dtb;
		}

		public static DataTable GetSIContractDate(string strEmpID)
		{
			DataTable dtb = clsCommon.GetDataTable("SI_spfrmSI 'GetSIContractDate', @EmpID = '" + strEmpID + "'");
			return dtb;
		}
		public static DataTable GetAllSIRecord(string strEmpID)
		{
			DataTable dtb = clsCommon.GetDataTable("SI_spfrmSIRecord 'GetAllRecord', @EmpID = '" + strEmpID + "'");
			return dtb;
		}

		public static DataTable GetEmpSIBook(string strEmpID, string strLanguage)
		{
			DataTable dtb = clsCommon.GetDataTable("SI_spfrmSI 'GetAllData', @EmpID = '" + strEmpID + "', @LanguageID = '" + strLanguage + "'");
			return dtb;
		}

		public static DataTable CheckValidData(string strEmpID, string strFromDate, string strToDate, string strLanguage,string strDeleteDate)
		{
			DataTable dtb = clsCommon.GetDataTable("SI_spfrmSIRecord 'CheckValidData', @EmpID = '" + strEmpID + "', @FromDate = '"
				+ strFromDate + "', @ToDate = '" + strToDate + "', @LanguageID = '" + strLanguage + "',@DeleteDate='"+ strDeleteDate +"'");
			return dtb;
		}

		public static DataTable GetSIStartDateAtPru(string strEmpID)
		{
			DataTable dtb = clsCommon.GetDataTable("SI_spfrmSI 'GetSIStartDateAtPru', @EmpID = '" + strEmpID + "'");
			return dtb;
		}

		public static string DeleteSIRecord(DataGrid dtgSIRecord)
		{
			SqlCommand cmd = new SqlCommand();
			SqlConnection SQLconn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction = SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "SI_spfrmSIRecord";
 
				CheckBox obj = new CheckBox();
				for (int i = 0;i<= dtgSIRecord.Items.Count - 1;i++)
				{
					obj = (CheckBox) dtgSIRecord.Items[i].FindControl("chkSelect");
					if (obj.Checked)
					{
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "Delete";						
						cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = dtgSIRecord.Items[i].Cells[0].Text.Trim();
						cmd.Parameters.Add("@FromDate", SqlDbType.NVarChar, 12).Value = dtgSIRecord.Items[i].Cells[1].Text.Trim();
						cmd.ExecuteNonQuery();											
					}
				}
				obj.Dispose();
				cmd.Transaction.Commit();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();	
				return "";
			}
			catch (Exception exp)
			{								
				cmd.Transaction.Rollback();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				return exp.Message.Trim();
			}
		}


		public static string SaveSIRecord(string strEmpID, string strFromDate, string strToDate,
			string strNoOfMonth, string strCompanyName, string strSalary, string strLanguage,string strDeleteDate)
		{
			SqlCommand cmd = new SqlCommand();
			SqlConnection SQLconn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction = SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "SI_spfrmSIRecord";
 
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "Save";	

				if (strEmpID != "")
					cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = strEmpID;
				else
					cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = DBNull.Value;
				if (strFromDate != "")
					cmd.Parameters.Add("@FromDate", SqlDbType.NVarChar, 12).Value = strFromDate;
				else
					cmd.Parameters.Add("@FromDate", SqlDbType.NVarChar, 12).Value = DBNull.Value;
				if (strToDate != "")
					cmd.Parameters.Add("@ToDate", SqlDbType.NVarChar, 12).Value = strToDate;
				else
					cmd.Parameters.Add("@ToDate", SqlDbType.NVarChar, 12).Value = DBNull.Value;
				if (strNoOfMonth != "")
					cmd.Parameters.Add("@NoOfMonth", SqlDbType.Float).Value = strNoOfMonth;
				else
					cmd.Parameters.Add("@NoOfMonth", SqlDbType.Float).Value = DBNull.Value;
				if (strCompanyName != "")
					cmd.Parameters.Add("@CompanyName", SqlDbType.NVarChar, 200).Value = strCompanyName;
				else
					cmd.Parameters.Add("@CompanyName", SqlDbType.NVarChar, 200).Value = DBNull.Value;
				if (strSalary != "")
					cmd.Parameters.Add("@Salary", SqlDbType.Money).Value = strSalary;
				else
					cmd.Parameters.Add("@Salary", SqlDbType.Money).Value = DBNull.Value;
				if(strDeleteDate !="" )
					cmd.Parameters.Add("@DeleteDate",SqlDbType.NVarChar,20).Value=strDeleteDate;
				else
					cmd.Parameters.Add("@DeleteDate",SqlDbType.NVarChar,20).Value=DBNull.Value;
				cmd.Parameters.Add("@LanguageID", SqlDbType.VarChar, 2).Value = strLanguage;

				cmd.ExecuteNonQuery();											

				cmd.Transaction.Commit();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();	
				return "";
			}
			catch (Exception exp)
			{								
				cmd.Transaction.Rollback();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				return exp.Message.Trim();
			}
		}
	}

	#endregion Frm: Social Insurance

	#region Frm: Health insurance
	public class clsSIHealthIns
	{
		public static DataTable GetHIList(string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strJobCode, string strEmpType, string strLocation, string strCompany, string strStatus, string sAccountLogin)
		{
			string strSql = "SI_spfrmHI @Activity = 'GetHIList', @EmpID = N'" + strEmpID + "', @EmpName = N'" + strEmpName
				+ "', @LSLevel1ID = '" + strLevel1 + "', @LSLevel2ID = '" + strLevel2 + "', @LSLevel3ID = '" + strLevel3
				+ "', @LSJobCodeID = '" + strJobCode + "', @LSEmpTypeID = '" + strEmpType
				+ "', @UserGroupID = '" + sAccountLogin 
				+ "', @LSLocationID = '" + strLocation + "', @LSCompanyID = '" + strCompany + "'";
			if (strStatus != "")
				strSql = strSql + ", @Status = '" + strStatus + "'";

			DataTable dtb = clsCommon.GetDataTable(strSql);
			return dtb;
		}

		public static string DeleteHIRecord(DataGrid dtgHIRecord)
		{
			SqlCommand cmd = new SqlCommand();
			SqlConnection SQLconn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction = SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "SI_spfrmHICard";
 
				CheckBox obj = new CheckBox();
				for (int i = 0;i<= dtgHIRecord.Items.Count - 1;i++)
				{
					obj = (CheckBox) dtgHIRecord.Items[i].FindControl("chkSelect");
					if (obj.Checked)
					{
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "Delete";						
						cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = dtgHIRecord.Items[i].Cells[0].Text.Trim();
						cmd.Parameters.Add("@FromDate", SqlDbType.NVarChar, 12).Value = dtgHIRecord.Items[i].Cells[1].Text.Trim();
						cmd.ExecuteNonQuery();											
					}
				}
				obj.Dispose();
				cmd.Transaction.Commit();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();	
				return "";
			}
			catch (Exception exp)
			{								
				cmd.Transaction.Rollback();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				return exp.Message.Trim();
			}
		}

		public static DataTable GetHIRecord(string strEmpID)
		{
			DataTable dtb = clsCommon.GetDataTable("SI_spfrmHICard 'GetAllData', @EmpID = '" + strEmpID + "'");
			return dtb;
		}

		public static DataTable GetEmpHIBook(string strEmpID)
		{
			DataTable dtb = clsCommon.GetDataTable("SI_spfrmHI 'GetAllData', @EmpID = '" + strEmpID + "'");
			return dtb;
		}

		/*public static DataTable CheckValidData_HI(string strEmpID, string strFromDate, string strToDate, string strLanguage, string strDeleteDate)
		{
			DataTable dtb = clsCommon.GetDataTable("SI_spfrmHICard @Activity = 'CheckValidData', @EmpID = '" + strEmpID + "', @FromDate = '"
				+ strFromDate + "', @ToDate = '" + strToDate + "', @LanguageID = '" + strLanguage + "',@DeleteDate='"+ strDeleteDate+"'");
			return dtb;
		}*/
		public static DataTable CheckValidData_HI(string strEmpID, string strFromDate, string strToDate, string strLanguage, string strDeleteDate, string strPlace)
		{
			DataTable dtb = clsCommon.GetDataTable("SI_spfrmHICard @Activity = 'CheckValidData', @EmpID = '" + strEmpID + "', @FromDate = '"
				+ strFromDate + "', @ToDate = '" + strToDate + "', @PlaceExaminingCard = '" + strPlace + "', @LanguageID = '" + strLanguage + "',@DeleteDate='"+ strDeleteDate+ "'");
			return dtb;
		}
		
		public static string SaveData_HI(DataGrid dtgList, string strCreater, string strLanguage)
		{
			string strEmpID = "";
			string strEmpCode ="";
			string strFromDate = "";
			string strToDate = "";
			string strExaminingPlace = "";
			string strChangePlace = "";
			string strNote = "";
			string strReturn = "";
			SqlCommand cmd = new SqlCommand();
			SqlConnection SQLconn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction = SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "SI_spfrmHICard";
 
				for (int i = 0;i<= dtgList.Items.Count - 1;i++)
				{
					if (((CheckBox)dtgList.Items[i].FindControl("chkSelect")).Checked)
					{
						strEmpID = dtgList.Items[i].Cells[1].Text.Trim();
						strEmpCode = dtgList.Items[i].Cells[3].Text.Trim();
						strFromDate = ((TextBox)dtgList.Items[i].FindControl("txtFromDateGrid")).Text.Trim();
						strToDate = ((TextBox)dtgList.Items[i].FindControl("txtToDateGrid")).Text.Trim();
						strExaminingPlace = ((DropDownList)dtgList.Items[i].FindControl("cboExaminingPlaceGrid")).SelectedValue.Trim();
						if(strExaminingPlace !="")
						{
							strChangePlace = "1";

						}
						else  strChangePlace = "0";
						
						strNote = ((TextBox)dtgList.Items[i].FindControl("txtNoteGrid")).Text.Trim();
						
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 255).Value = "SaveList_HICard";	
						cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = strEmpID;				
						cmd.Parameters.Add("@FromDate",SqlDbType.NVarChar,12).Value = strFromDate;
						cmd.Parameters.Add("@ToDate",SqlDbType.NVarChar,12).Value = strToDate;
						cmd.Parameters.Add("@PlaceExaminingCard",SqlDbType.NVarChar,12).Value = strExaminingPlace;
						cmd.Parameters.Add("@Note",SqlDbType.NVarChar,255).Value = strNote;
						cmd.Parameters.Add("@LanguageID",SqlDbType.NVarChar,12).Value = strLanguage;
						cmd.Parameters.Add("@Creater",SqlDbType.NVarChar,80).Value = strCreater;
						cmd.ExecuteNonQuery();
					}			
				}
				cmd.Transaction.Commit();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();	
				return "";
			}
			catch (Exception exp)
			{								
				cmd.Transaction.Rollback();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				return exp.Message.Trim();
			}
			
			//DBManager dbManager ; 
		/*	Hashtable hashParam ;
			string strEmpID = "";
			string strEmpCode ="";
			string strFromDate = "";
			string strToDate = "";
			string strExaminingPlace = "";
			string strChangePlace = "";
			string strNote = "";
			string strReturn = "";
			CheckBox chkobj;
			
			DBManager dbManager = new DBManager(clsCommon.sDBType,clsCommon.sODBC); 
			try
			{
				dbManager.Open();
				dbManager.BeginTransaction();		
				for(int i=0;i<dtgList.Items.Count;i++)
				{
					
					chkobj=(CheckBox)dtgList.Items[i].FindControl("chkSelect");
					if(chkobj.Checked==true)
					{
						strEmpID = dtgList.Items[i].Cells[1].Text.Trim();
						strEmpCode = dtgList.Items[i].Cells[3].Text.Trim();
						strFromDate = ((TextBox)dtgList.Items[i].FindControl("txtFromDateGrid")).Text.Trim();
						strToDate = ((TextBox)dtgList.Items[i].FindControl("txtToDateGrid")).Text.Trim();
						strExaminingPlace = ((DropDownList)dtgList.Items[i].FindControl("cboExaminingPlaceGrid")).SelectedValue.Trim();
						if(strExaminingPlace !="")
						{
							strChangePlace = "1";

						}
						else  strChangePlace = "0";
						
						strNote = ((TextBox)dtgList.Items[i].FindControl("txtNoteGrid")).Text.Trim();
						
						//DBManager dbManager = new DBManager(clsCommon.sDBType,clsCommon.sODBC); 
						 
							
						hashParam = new Hashtable();
						hashParam.Add("Activity".ToUpper(),"SaveList_HICard");
						hashParam.Add("EmpID".ToUpper(),strEmpID);
						hashParam.Add("FromDate".ToUpper(),strFromDate);
						hashParam.Add("ToDate".ToUpper(),strToDate);
						hashParam.Add("PlaceExaminingCard".ToUpper(),strExaminingPlace);
						hashParam.Add("ChangePlace".ToUpper(),strChangePlace);
						hashParam.Add("Note".ToUpper(),strNote);
						hashParam.Add("Creater".ToUpper(),strCreater);
						hashParam.Add("LanguageID".ToUpper(),strLanguage);
							
						//dbManager.PrepareParametersFrom("SI_spfrmSI",hashParam);
						//dbManager.ExecuteNonQuery(CommandType.StoredProcedure,"SI_spfrmSI",true);  
						dbManager.PrepareParametersFrom("SI_spfrmHICard",hashParam);
						dbManager.ExecuteNonQuery(CommandType.StoredProcedure,"SI_spfrmHICard"); 
						string sOut=dbManager.ParameterOutput("ReturnMess");
						if (sOut!="") 
						{
							if (strReturn != "")
								strReturn = strReturn + "," + strEmpCode;	
							else
								strReturn =strEmpCode;// arrTemp[i].Trim();	
						}						
					}
					
				}
				dbManager.CommitTransaction();
				return strReturn;

			}
			catch(Exception ex)
			{
				dbManager.Transaction.Rollback();   	
				return ex.Message;
			}
			finally
			{
				dbManager.Close();
				dbManager.Dispose(); 
			}
		*/	

		}

		public static string SaveHIGroup(string strEmpList, string strFromDate, string strToDate, int intChangePlace,
			string strExaminingPlace, string strNoteHICard)
		{
			string strReturn = "";
			SqlCommand cmd = new SqlCommand();
			SqlConnection SQLconn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction = SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "SI_spfrmHICard";
				string[] arrTemp = strEmpList.Split(new char[]{','});
				for (int i = 0;i< arrTemp.Length;i++)
				{
					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "SaveList";						
					string strEmpID = clsSI.GetEmpIDFromCode(arrTemp[i].Trim());
					cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = strEmpID;
					cmd.Parameters.Add("@FromDate", SqlDbType.NVarChar, 12).Value = strFromDate;
					cmd.Parameters.Add("@ToDate", SqlDbType.NVarChar, 12).Value = strToDate;
					cmd.Parameters.Add("@ChangePlace", SqlDbType.Bit).Value = intChangePlace;
					if (strExaminingPlace != "")
						cmd.Parameters.Add("@PlaceExaminingCard", SqlDbType.NVarChar,12).Value = strExaminingPlace;
					else
						cmd.Parameters.Add("@PlaceExaminingCard", SqlDbType.NVarChar,12).Value = DBNull.Value;
					if (strNoteHICard != "")
						cmd.Parameters.Add("@NoteHICard", SqlDbType.NVarChar,200).Value = strNoteHICard;
					else
						cmd.Parameters.Add("@NoteHICard", SqlDbType.NVarChar,200).Value = DBNull.Value;
					
					cmd.Parameters.Add("@ReturnMess", SqlDbType.NVarChar,1000).Direction = ParameterDirection.InputOutput;
					cmd.ExecuteNonQuery();	
					if (cmd.Parameters["@ReturnMess"].Value.ToString() != "")
					{
						if (strReturn != "")
							strReturn = strReturn + "," + arrTemp[i].Trim();	
						else
							strReturn = arrTemp[i].Trim();	
					}
				}
				cmd.Transaction.Commit();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();	
				return strReturn;
			}
			catch (Exception exp)
			{								
				cmd.Transaction.Rollback();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				return exp.Message.Trim();
			}
		}
		public static string SaveHIGroup(string strEmpList, string strFromDate, string strToDate, int intChangePlace,
			string strExaminingPlace, string strNoteHICard,
			string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strPosition, string strJobCode, string strLocation, string strCompany, string strStatus,string strExaminingPlaceSearch)
		{
			string strReturn = "";
			SqlCommand cmd = new SqlCommand();
			SqlConnection SQLconn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction = SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "SI_spfrmHICard";
				cmd.Parameters.Clear();

				cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "SaveListByCondition";						
				//cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = arrTemp[i].Trim();
				cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = strEmpID;
				cmd.Parameters.Add("@EmpName", SqlDbType.NVarChar, 50).Value = strEmpName;
				cmd.Parameters.Add("@LSCompanyID", SqlDbType.NVarChar, 12).Value = strCompany;
				cmd.Parameters.Add("@LSLevel1ID", SqlDbType.NVarChar, 12).Value = strLevel1;
				cmd.Parameters.Add("@LSLevel2ID", SqlDbType.NVarChar, 12).Value = strLevel2;
				cmd.Parameters.Add("@LSLevel3ID", SqlDbType.NVarChar, 12).Value = strLevel3;
				cmd.Parameters.Add("@LSPositionID", SqlDbType.NVarChar, 12).Value = strPosition;
				cmd.Parameters.Add("@LSJobCodeID", SqlDbType.NVarChar, 12).Value = strJobCode;
				cmd.Parameters.Add("@LSLocationID", SqlDbType.NVarChar, 12).Value = strLocation;
				cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = strStatus;

				cmd.Parameters.Add("@FromDate", SqlDbType.NVarChar, 12).Value = strFromDate;
				cmd.Parameters.Add("@ToDate", SqlDbType.NVarChar, 12).Value = strToDate;
				cmd.Parameters.Add("@ChangePlace", SqlDbType.Bit).Value = intChangePlace;
				if (strExaminingPlace != "")
					cmd.Parameters.Add("@PlaceExaminingCard", SqlDbType.NVarChar,12).Value = strExaminingPlace;
				else
					cmd.Parameters.Add("@PlaceExaminingCard", SqlDbType.NVarChar,12).Value = DBNull.Value;
				if (strNoteHICard != "")
					cmd.Parameters.Add("@NoteHICard", SqlDbType.NVarChar,200).Value = strNoteHICard;
				else
					cmd.Parameters.Add("@NoteHICard", SqlDbType.NVarChar,200).Value = DBNull.Value;
				if (strExaminingPlaceSearch != "")
					cmd.Parameters.Add("@ExaminingPlaceSearch", SqlDbType.NVarChar,12).Value = strExaminingPlaceSearch;
				else
					cmd.Parameters.Add("@ExaminingPlaceSearch", SqlDbType.NVarChar,12).Value = DBNull.Value;

				cmd.Parameters.Add("@ReturnMess", SqlDbType.NVarChar,1000).Direction = ParameterDirection.InputOutput;
				cmd.ExecuteNonQuery();
				if (strReturn != "")
					strReturn = strReturn + "," + cmd.Parameters["@ReturnMess"].Value.ToString();	
				else
					strReturn = cmd.Parameters["@ReturnMess"].Value.ToString();	
				cmd.Transaction.Commit();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();	
				return strReturn;
			}
			catch (Exception exp)
			{								
				cmd.Transaction.Rollback();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				return exp.Message.Trim();
			}
		}

		public static string SaveHIRecord(string strEmpID, string strFromDate, string strToDate, 
			string strChangePlace, string strPlaceExaminingCard, string strLSLocationID, string strNoteHICard, string strLanguage,string strDeleteDate)
		{
			SqlCommand cmd = new SqlCommand();
			SqlConnection SQLconn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction = SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "SI_spfrmHICard";
 
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "Save";	

				if (strEmpID != "")
					cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = strEmpID;
				else
					cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = DBNull.Value;
				if (strFromDate != "")
					cmd.Parameters.Add("@FromDate", SqlDbType.NVarChar, 12).Value = strFromDate;
				else
					cmd.Parameters.Add("@FromDate", SqlDbType.NVarChar, 12).Value = DBNull.Value;
				if (strToDate != "")
					cmd.Parameters.Add("@ToDate", SqlDbType.NVarChar, 12).Value = strToDate;
				else
					cmd.Parameters.Add("@ToDate", SqlDbType.NVarChar, 12).Value = DBNull.Value;
				cmd.Parameters.Add("@ChangePlace", SqlDbType.Bit).Value = strChangePlace=="1"?true:false;
				if (strPlaceExaminingCard != "")
					cmd.Parameters.Add("@PlaceExaminingCard", SqlDbType.NVarChar, 12).Value = strPlaceExaminingCard;
				else
					cmd.Parameters.Add("@PlaceExaminingCard", SqlDbType.NVarChar, 12).Value = DBNull.Value;
				if (strLSLocationID != "")
					cmd.Parameters.Add("@LSLocationID", SqlDbType.NVarChar, 12).Value = strLSLocationID;
				else
					cmd.Parameters.Add("@LSLocationID", SqlDbType.NVarChar, 12).Value = DBNull.Value;

				if (strNoteHICard != "")
					cmd.Parameters.Add("@NoteHICard", SqlDbType.NVarChar, 200).Value = strNoteHICard;
				else
					cmd.Parameters.Add("@NoteHICard", SqlDbType.NVarChar, 200).Value = DBNull.Value;
				if(strDeleteDate !="" )
					cmd.Parameters.Add("@DeleteDate",SqlDbType.NVarChar,20).Value=strDeleteDate; 
				else
					cmd.Parameters.Add("@DeleteDate",SqlDbType.NVarChar,20).Value=DBNull.Value;  
  
				cmd.Parameters.Add("@LanguageID", SqlDbType.VarChar, 2).Value = strLanguage;

				cmd.ExecuteNonQuery();											

				cmd.Transaction.Commit();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();	
				return "";
			}
			catch (Exception exp)
			{								
				cmd.Transaction.Rollback();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				return exp.Message.Trim();
			}
		}
	}
	#endregion Frm: Health insurance

	#region Frm: Medical expenses
	public class clsSIMedicalExpenses
	{
		//Frm: Medical expense
		public static DataTable GetMEValidFromTo(string strEmpID, int intSickLeaveType)
		{
			DataTable dtb = clsCommon.GetDataTable("SI_spfrmMedicalExpense 'GetValidFromToDate', @EmpID = '" + strEmpID + "', @SickLeaveType = '" + intSickLeaveType.ToString() + "'");
			return dtb;
		}

		public static DataTable GetOtherInfo(string strEmpID, string strTreatmentFrom)
		{
			DataTable dtb = clsCommon.GetDataTable("SI_spfrmMedicalExpense 'GetOtherInfo', @EmpID = '" + strEmpID + "', @TreatmentFrom = '" + strTreatmentFrom + "'");
			return dtb;
		}

		public static DataTable GetAllRecord(string strLanguage, string strEmpID, int intSickLeaveType)
		{
			DataTable dtb = clsCommon.GetDataTable("SI_spfrmMedicalExpense 'GetDataAll', @LanguageID = '" + strLanguage
				+ "', @EmpID = '" + strEmpID + "', @SickLeaveType = '" + intSickLeaveType.ToString() + "'" );
			return dtb;
		}

		public static DataTable GetMEByID(string strLanguage, string strEmpID, string strFromDate, int intSickLeaveType)
		{
			DataTable dtb = clsCommon.GetDataTable("SI_spfrmMedicalExpense 'GetDataByID', @LanguageID = '" + strLanguage
				+ "', @EmpID = '" + strEmpID + "', @TreatmentFrom = '" + strFromDate + "', @SickLeaveType = '" + intSickLeaveType.ToString() + "'" );
			return dtb;
		}

		public static string DeleteMedicalExpense(DataGrid dtgList)
		{
			SqlCommand cmd = new SqlCommand();
			SqlConnection SQLconn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction = SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "SI_spfrmMedicalExpense";
 
				CheckBox obj = new CheckBox();
				for (int i = 0;i<= dtgList.Items.Count - 1;i++)
				{
					obj = (CheckBox) dtgList.Items[i].FindControl("chkSelect");
					if (obj.Checked)
					{
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "Delete";						
						cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = dtgList.Items[i].Cells[0].Text.Trim();
						cmd.Parameters.Add("@TreatmentFrom", SqlDbType.NVarChar, 12).Value = dtgList.Items[i].Cells[1].Text.Trim();
						cmd.ExecuteNonQuery();											
					}
				}
				obj.Dispose();
				cmd.Transaction.Commit();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();	
				return "";
			}
			catch (Exception exp)
			{								
				cmd.Transaction.Rollback();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				return exp.Message.Trim();
			}
		}

		public static DataTable GetAllMEData(string strLanguage, int intSickLeaveType, string strFromDate, string strToDate, string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strPosition, string strJobCode, string strLocation, string strCompany, string strStatus)
		{
			string strSql = "SI_spfrmMedicalExpense 'GetData', @LanguageID = '" + strLanguage
				+ "', @SickLeaveType = '" + intSickLeaveType.ToString() + "', @TreatmentFrom = '" + strFromDate + "', @TreatmentTo = '" + strToDate + "', @EmpID = N'" + strEmpID + "', @EmpName = N'" + strEmpName
				+ "', @LSLevel1Code = '" + strLevel1 + "', @LSLevel2Code = '" + strLevel2 + "', @LSLevel3Code = '" + strLevel3
				+ "', @LSPositionCode = '" + strPosition + "', @LSJobCodeCode = '" + strJobCode 
				+ "', @LSLocationCode = '" + strLocation + "', @LSCompanyCode = '" + strCompany + "'";
			if (strStatus != "")
				strSql = strSql + ", @Status = '" + strStatus + "'";
			DataTable dtb = clsCommon.GetDataTable(strSql);
			return dtb;
		}

		//end Frm: Medical expense
	}
	#endregion Frm: Medical expenses

	#region Frm: Accident Insurance --> Khong dung
	public class clsSIAccidentIns
	{
		//Frm: Accident insurance
		public static DataTable CheckExisted(string strEmpID, string strAccidentDate)
		{
			DataTable dtb = clsCommon.GetDataTable("SI_spfrmAccidentIns 'CheckExisted', @EmpID = '" + strEmpID + "', @AccidentDate = '" + strAccidentDate + "'");
			return dtb;
		}

		public static DataTable GetOtherInfo(string strEmpID, string strAccidentDate)
		{
			DataTable dtb = clsCommon.GetDataTable("SI_spfrmAccidentIns 'GetOtherInfo', @EmpID = '" + strEmpID + "', @AccidentDate = '" + strAccidentDate + "'");
			return dtb;
		}

		public static DataTable GetAllRecord(string strLanguage, string strEmpID)
		{
			DataTable dtb = clsCommon.GetDataTable("SI_spfrmAccidentIns 'GetDataAll', @LanguageID = '" + strLanguage
				+ "', @EmpID = '" + strEmpID + "'" );
			return dtb;
		}

		public static DataTable GetAc_InsByID(string strLanguage, string strEmpID, string strAccidentDate)
		{
			DataTable dtb = clsCommon.GetDataTable("SI_spfrmAccidentIns 'GetDataByID', @LanguageID = '" + strLanguage
				+ "', @EmpID = '" + strEmpID + "', @AccidentDate = '" + strAccidentDate + "'" );
			return dtb;
		}

		public static string DeleteAc_Ins(DataGrid dtgList)
		{
			SqlCommand cmd = new SqlCommand();
			SqlConnection SQLconn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction = SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "SI_spfrmAccidentIns";
 
				CheckBox obj = new CheckBox();
				for (int i = 0;i<= dtgList.Items.Count - 1;i++)
				{
					obj = (CheckBox) dtgList.Items[i].FindControl("chkSelect");
					if (obj.Checked)
					{
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "Delete";						
						cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = dtgList.Items[i].Cells[0].Text.Trim();
						cmd.Parameters.Add("@AccidentDate", SqlDbType.NVarChar, 12).Value = dtgList.Items[i].Cells[1].Text.Trim();
						cmd.ExecuteNonQuery();											
					}
				}
				obj.Dispose();
				cmd.Transaction.Commit();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();	
				return "";
			}
			catch (Exception exp)
			{								
				cmd.Transaction.Rollback();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				return exp.Message.Trim();
			}
		}

		public static DataTable GetAllAc_InsData(string strLanguage, int intDamagePerson, string strFromDate, string strToDate, string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strPosition, string strJobCode, string strLocation, string strCompany, string strStatus)
		{
			string strSql = "SI_spfrmAccidentIns 'GetData', @LanguageID = '" + strLanguage
				+ "', @DamagePerson = '" + intDamagePerson.ToString() + "', @FromDate = '" + strFromDate + "', @ToDate = '" + strToDate + "', @EmpID = N'" + strEmpID + "', @EmpName = N'" + strEmpName
				+ "', @LSLevel1Code = '" + strLevel1 + "', @LSLevel2Code = '" + strLevel2 + "', @LSLevel3Code = '" + strLevel3
				+ "', @LSPositionCode = '" + strPosition + "', @LSJobCodeCode = '" + strJobCode 
				+ "', @LSLocationCode = '" + strLocation + "', @LSCompanyCode = '" + strCompany + "'";
			if (strStatus != "")
				strSql = strSql + ", @Status = '" + strStatus + "'";
			DataTable dtb = clsCommon.GetDataTable(strSql);
			return dtb;
		}

		//end Frm: Accident insurance
	}
	#endregion Frm: Accident Insurance

	#region Insurance notice data + supplement
	public class clsSIInsNoticeData
	{
		#region Frm: Insurance notice data
		public static void LoadComboSource(DropDownList pControl, string strLanguage)
		{
			clsCommon.LoadDropDownList(pControl, "SI_spfrmInsNoticeData @Activity = 'GetSource', @LanguageID = '" + strLanguage + "'", "SourceID", "SourceName");
		}
		
		public static DataTable GetNoticeData(string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strPosition, string strJobCode, string strLocation, string strCompany, string strStatus,
			string strAriseFrom, string strAriseTo, string strInsNoticeFrom, string strInsNoticeTo, string strSourceID, 
			string strIsNotice, string strIsSupplement, string strStage,string strUsername)
		{
			string strSql = "SI_spfrmInsNoticeData @Activity = 'GetAll', @EmpID = N'" + strEmpID + "', @EmpName = N'" + strEmpName
				+ "', @LSLevel1ID = N'" + strLevel1 + "', @LSLevel2ID = N'" + strLevel2 + "', @LSLevel3ID = N'" + strLevel3
				+ "', @LSPositionID = N'" + strPosition + "', @LSJobCodeID = N'" + strJobCode 
				+ "', @LSLocationID = N'" + strLocation + "', @LSCompanyID = N'" + strCompany 
				+ "', @AriseFrom = N'" + strAriseFrom + "', @AriseTo = N'" + strAriseTo + "', @SourceID = '" + strSourceID 
				+ "', @InsNoticeFrom = N'" + strInsNoticeFrom + "', @InsNoticeTo = N'" + strInsNoticeTo
				+ "', @IsNoticed = '" + strIsNotice + "', @UserGroupID = '" + strUsername
				+ "', @Stage = '" + strStage 
				+ "', @IsSupplement = '" + strIsSupplement + "'";
			if (strStatus != "")
				strSql = strSql + ", @Status = '" + strStatus + "'";

			DataTable dtb = clsCommon.GetDataTable(strSql);
			return dtb;
		}

		public static DataTable GetNoticeDataForAddNew(string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strPosition, string strJobCode, string strLocation, string strCompany, string strStatus,
			string strAriseFrom, string strAriseTo, string strSourceID, string strStage)
		{
			string strSql = "SI_spfrmInsNoticeData @Activity = 'GetForAddNew', @EmpID = N'" + strEmpID + "', @EmpName = N'" + strEmpName
				+ "', @LSLevel1ID = N'" + strLevel1 + "', @LSLevel2ID = N'" + strLevel2 + "', @LSLevel3ID = N'" + strLevel3
				+ "', @LSPositionID = N'" + strPosition + "', @LSJobCodeID = N'" + strJobCode 
				+ "', @LSLocationID = N'" + strLocation + "', @LSCompanyID = N'" + strCompany 
				+ "', @AriseFrom = N'" + strAriseFrom + "', @AriseTo = N'" + strAriseTo + "', @SourceID = '" + strSourceID 
				+ "', @Stage = '" + strStage + "'";
			if (strStatus != "")
				strSql = strSql + ", @Status = '" + strStatus + "'";

			DataTable dtb = clsCommon.GetDataTable(strSql);
			return dtb;
		}

		public static string SaveInsData(DataGrid dtgList)
		{
			SqlCommand cmd = new SqlCommand();
			SqlConnection SQLconn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction = SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "SI_spfrmInsNoticeData";
 
				string strEmpID = "";
				string strSourceID = "";
				string strAriseDate = "";
				string strToDate = "";
				string strRightNoticeDate = "";
				string strOldSalaryCoef = "";
				string strOldSalary = "";
				string strOldAllowanceCoef = "";
				string strOldAllowance = "";
				string strNewSalaryCoef = "";
				string strNewSalary = "";
				string strNewAllowanceCoef = "";
				string strNewAllowance = "";
				string strNoticeDate = "";
				string strNoteDesc = "";
				string strIncNoticeDate = "";
				string strNoteInc = "";
				string strStage = "";
				string strPercentSupplement = "";
				string strTotalMonth = "";
				string strSourceType = "";
				string strOldSalary_FromDate = "";
				string strNewSalary_FromDate = "";

				for (int i = 0;i<= dtgList.Items.Count - 1;i++)
				{
					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "Save";	

					strEmpID = dtgList.Items[i].Cells[2].Text.Trim();
					//string strSourceID = dtgList.Items[i].Cells[7].Text.Trim();
					strSourceID = ((Label)dtgList.Items[i].FindControl("txtSourceID")).Text.Trim();
					strAriseDate = dtgList.Items[i].Cells[9].Text.Trim();
					strToDate = dtgList.Items[i].Cells[10].Text.Trim();
					//string strRightNoticeDate = dtgList.Items[i].Cells[15].Text.Trim();
					strRightNoticeDate = ((Label)dtgList.Items[i].FindControl("txtRightNoticeDate")).Text.Trim();
					strOldSalaryCoef = dtgList.Items[i].Cells[17].Text.Trim();
					strOldSalary = dtgList.Items[i].Cells[18].Text.Trim();
					strOldAllowanceCoef = dtgList.Items[i].Cells[19].Text.Trim();
					strOldAllowance = dtgList.Items[i].Cells[20].Text.Trim();
					strNewSalaryCoef = dtgList.Items[i].Cells[21].Text.Trim();
					strNewSalary = dtgList.Items[i].Cells[22].Text.Trim();
					strNewAllowanceCoef = dtgList.Items[i].Cells[23].Text.Trim();
					strNewAllowance = dtgList.Items[i].Cells[24].Text.Trim();
					strSourceType = dtgList.Items[i].Cells[25].Text.Trim();
					if(strSourceType == "True")
					{
						strSourceType = "1";
					}
					strNoticeDate = ((TextBox)dtgList.Items[i].FindControl("txtNoticeDate")).Text.Trim();
					strNoteDesc = ((TextBox)dtgList.Items[i].FindControl("txtNoteDesc")).Text.Trim();
					strIncNoticeDate = ((TextBox)dtgList.Items[i].FindControl("txtIncNoticeDate")).Text.Trim();
					strNoteInc = ((TextBox)dtgList.Items[i].FindControl("txtNoteInc")).Text.Trim();
					strStage = ((TextBox)dtgList.Items[i].FindControl("txtStageGrid")).Text.Trim();
					strPercentSupplement = ((TextBox)dtgList.Items[i].FindControl("txtPercentSupplement")).Text.Trim();
					strTotalMonth = ((TextBox)dtgList.Items[i].FindControl("txtTotalMonth")).Text.Trim();					
					strOldSalary_FromDate = dtgList.Items[i].Cells[28].Text.Trim();;					
					strNewSalary_FromDate = dtgList.Items[i].Cells[29].Text.Trim();;					

					if (strEmpID != "")
						cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = strEmpID;
					else
						cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = DBNull.Value;
					if (strAriseDate != "")
						cmd.Parameters.Add("@AriseDate", SqlDbType.NVarChar, 12).Value = strAriseDate;
					else
						cmd.Parameters.Add("@AriseDate", SqlDbType.NVarChar, 12).Value = DBNull.Value;
					if (strToDate.Trim().Replace("&nbsp;", "") != "")
						cmd.Parameters.Add("@ToDate", SqlDbType.NVarChar, 12).Value = strToDate;
					else
						cmd.Parameters.Add("@ToDate", SqlDbType.NVarChar, 12).Value = DBNull.Value;
					if (strSourceID != "")
						cmd.Parameters.Add("@SourceID", SqlDbType.NVarChar, 50).Value = strSourceID;
					else 
						cmd.Parameters.Add("@SourceID", SqlDbType.NVarChar, 50).Value = DBNull.Value;
					cmd.Parameters.Add("@IsInsNotice", SqlDbType.Bit).Value = ((CheckBox)dtgList.Items[i].FindControl("chkSelect")).Checked?1:0;
					if (strNoticeDate != "")
						cmd.Parameters.Add("@InsNoticeDate", SqlDbType.NVarChar, 12).Value = strNoticeDate;
					else
						cmd.Parameters.Add("@InsNoticeDate", SqlDbType.NVarChar, 12).Value = DBNull.Value;
					if (strNoteDesc != "")
						cmd.Parameters.Add("@NoteDesc", SqlDbType.NVarChar, 200).Value = strNoteDesc;
					else
						cmd.Parameters.Add("@NoteDesc", SqlDbType.NVarChar, 200).Value = DBNull.Value;
					if (strIncNoticeDate != "")
						cmd.Parameters.Add("@IncNoticeDate", SqlDbType.NVarChar, 12).Value = strIncNoticeDate;
					else
						cmd.Parameters.Add("@IncNoticeDate", SqlDbType.NVarChar, 12).Value = DBNull.Value;
					if (strNoteInc != "")
						cmd.Parameters.Add("@NoteInc", SqlDbType.NVarChar, 200).Value = strNoteInc;
					else
						cmd.Parameters.Add("@NoteInc", SqlDbType.NVarChar, 200).Value = DBNull.Value;
					if (strStage != "")
						cmd.Parameters.Add("@Stage", SqlDbType.NVarChar, 200).Value = strStage;
					else
						cmd.Parameters.Add("@Stage", SqlDbType.NVarChar, 200).Value = DBNull.Value;
					
					if (strRightNoticeDate != "")
						cmd.Parameters.Add("@RightNoticeDate", SqlDbType.NVarChar, 12).Value = strRightNoticeDate;
					else
						cmd.Parameters.Add("@RightNoticeDate", SqlDbType.NVarChar, 12).Value = DBNull.Value;
					if (strOldSalaryCoef != "")
						cmd.Parameters.Add("@OldSalaryCoef", SqlDbType.Real).Value = strOldSalaryCoef;
					else
						cmd.Parameters.Add("@OldSalaryCoef", SqlDbType.Real).Value = DBNull.Value;
					if (strOldSalary != "")
						cmd.Parameters.Add("@OldSalary", SqlDbType.Money).Value = strOldSalary;
					else
						cmd.Parameters.Add("@OldSalary", SqlDbType.Money).Value = DBNull.Value;
					if (strOldAllowanceCoef != "")
						cmd.Parameters.Add("@OldAllowanceCoef", SqlDbType.Real).Value = strOldAllowanceCoef;
					else
						cmd.Parameters.Add("@OldAllowanceCoef", SqlDbType.Real).Value = DBNull.Value;
					if (strOldAllowance != "")
						cmd.Parameters.Add("@OldAllowance", SqlDbType.Money).Value = strOldAllowance;
					else
						cmd.Parameters.Add("@OldAllowance", SqlDbType.Money).Value = DBNull.Value;

					if (strNewSalaryCoef != "")
						cmd.Parameters.Add("@NewSalaryCoef", SqlDbType.Real).Value = strNewSalaryCoef;
					else
						cmd.Parameters.Add("@NewSalaryCoef", SqlDbType.Real).Value = DBNull.Value;
					if (strNewSalary != "")
						cmd.Parameters.Add("@NewSalary", SqlDbType.Money).Value = strNewSalary;
					else
						cmd.Parameters.Add("@NewSalary", SqlDbType.Money).Value = DBNull.Value;
					if (strNewAllowanceCoef != "")
						cmd.Parameters.Add("@NewAllowanceCoef", SqlDbType.Real).Value = strNewAllowanceCoef;
					else
						cmd.Parameters.Add("@NewAllowanceCoef", SqlDbType.Real).Value = DBNull.Value;
					if (strNewAllowance != "")
						cmd.Parameters.Add("@NewAllowance", SqlDbType.Money).Value = strNewAllowance;
					else
						cmd.Parameters.Add("@NewAllowance", SqlDbType.Money).Value = DBNull.Value;
					if (strSourceType != "")
						cmd.Parameters.Add("@SourceType", SqlDbType.Bit).Value = strSourceType=="1"?1:0;
					else
						cmd.Parameters.Add("@SourceType", SqlDbType.Bit).Value = DBNull.Value;
					

					
					if (strPercentSupplement != "")
						cmd.Parameters.Add("@PercentSupplement", SqlDbType.Int).Value = strPercentSupplement;
					else
						cmd.Parameters.Add("@PercentSupplement", SqlDbType.Int).Value = DBNull.Value;
					if (strTotalMonth != "")
						cmd.Parameters.Add("@TotalMonth", SqlDbType.Int).Value = strTotalMonth;
					else
						cmd.Parameters.Add("@TotalMonth", SqlDbType.Int).Value = DBNull.Value;
					if (strOldSalary_FromDate != "")
						cmd.Parameters.Add("@OldSalary_FromDate", SqlDbType.NVarChar).Value = strOldSalary_FromDate.Replace("&nbsp;","");
					else
						cmd.Parameters.Add("@OldSalary_FromDate", SqlDbType.NVarChar).Value = DBNull.Value;
					if (strNewSalary_FromDate != "")
						cmd.Parameters.Add("@NewSalary_FromDate", SqlDbType.NVarChar).Value = strNewSalary_FromDate.Replace("&nbsp;","");
					else
						cmd.Parameters.Add("@NewSalary_FromDate", SqlDbType.NVarChar).Value = DBNull.Value;

					cmd.ExecuteNonQuery();											
				}
				cmd.Transaction.Commit();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();	
				return "";
			}
			catch (Exception exp)
			{								
				cmd.Transaction.Rollback();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				return exp.Message.Trim();
			}
		}
		#endregion Frm: Insurance notice data

		#region Frm: Insurance notice data supplement - Khong dung
		public static DataTable GetNoticeDataSup(string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strPosition, string strJobCode, string strLocation, string strCompany, string strStatus,
			string strAriseFrom, string strAriseTo, string strInsNoticeFrom, string strInsNoticeTo, string strSourceID, string strIsNotice)
		{//Khong dung nua
			string strSql = "SI_spfrmInsNoticeDataSup @Activity = 'GetAll', @EmpID = N'" + strEmpID + "', @EmpName = N'" + strEmpName
				+ "', @LSLevel1ID = N'" + strLevel1 + "', @LSLevel2ID = N'" + strLevel2 + "', @LSLevel3ID = N'" + strLevel3
				+ "', @LSPositionID = N'" + strPosition + "', @LSJobCodeID = N'" + strJobCode 
				+ "', @LSLocationID = N'" + strLocation + "', @LSCompanyID = N'" + strCompany 
				+ "', @AriseFrom = N'" + strAriseFrom + "', @AriseTo = N'" + strAriseTo + "', @SourceID = '" + strSourceID 
				+ "', @InsNoticeFrom = N'" + strInsNoticeFrom + "', @InsNoticeTo = N'" + strInsNoticeTo
				+ "', @IsNoticed = '" + strIsNotice + "'";
			if (strStatus != "")
				strSql = strSql + ", @Status = '" + strStatus + "'";

			DataTable dtb = clsCommon.GetDataTable(strSql);
			return dtb;
		}
		#endregion Frm: Insurance notice data supplement
	}
	#endregion Insurance notice data + supplement

	

	#region Frm: Update convalescence
	public class clsSIUpdateConvalescence
	{
		public static DataTable GetAll(string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strPosition, string strJobCode, string strLocation, string strCompany, string strStatus,
			string strTerm, string strYear, string strMM, string strRegulations, string strToDate, string strSITime, string strType, string strLanguage, string strVFirstName, string strVLastName, string strUserAccount)
		{
			string strSql = "SI_spfrmUpdateConvalescence @Activity = 'Search', @EmpID = N'" + strEmpID + "', @EmpName = N'" + strEmpName
				+ "', @LSLevel1ID = N'" + strLevel1 + "', @LSLevel2ID = N'" + strLevel2 + "', @LSLevel3ID = N'" + strLevel3
				+ "', @LSPositionID = N'" + strPosition + "', @LSJobCodeID = N'" + strJobCode 
				+ "', @LSLocationID = N'" + strLocation + "', @LSCompanyID = N'" + strCompany 
				+ "', @Stage = N'" + strTerm + "', @YYYY = N'" + strYear + "', @MM = N'" + strMM + "', @ToDate = N'" + strToDate
				+ "', @Regulations = '" + strRegulations + "', @SITime = '" + strSITime + "', @ViewType = '" + strType
				+ "', @LanguageID = '" + strLanguage 
				+ "', @FirstName = N'" + strVFirstName  + "', @UserGroupID = '" + strUserAccount
				+ "', @LastName = N'" + strVLastName + "'";
			if (strStatus != "")
				strSql = strSql + ", @Status = '" + strStatus + "'";

			DataTable dtb = clsCommon.GetDataTable(strSql);
			return dtb;
		}

		public static DataTable GetCreate(string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strPosition, string strJobCode, string strLocation, string strCompany, string strStatus,
			string strStage, string strYear, string strMonth, string strToDate, string strLeaveDate, string strSITime, string strDays, string strAmount, 
			string strPlace, string strConcentrate, string strRegulations, string strPer, string strNote, string strLanguage,string strVFirstName, string strVLastName, string strUserAccount)
		{
			string strSql = "SI_spfrmUpdateConvalescence @Activity = 'Create', @EmpID = N'" + strEmpID + "', @EmpName = N'" + strEmpName
				+ "', @LSLevel1ID = N'" + strLevel1 + "', @LSLevel2ID = N'" + strLevel2 + "', @LSLevel3ID = N'" + strLevel3
				+ "', @LSPositionID = N'" + strPosition + "', @LSJobCodeID = N'" + strJobCode 
				+ "', @LSLocationID = N'" + strLocation + "', @LSCompanyID = N'" + strCompany 
				+ "', @Stage = N'" + strStage + "', @YYYY = N'" + strYear + "', @MM = N'" + strMonth + "', @ToDate = N'" + strToDate + "', @LeaveDate = N'" + strLeaveDate
				+ "', @SITime = " + strSITime + ", @Days = '" + strDays + "', @AmountText = '" + strAmount
				+ "', @Place = N'" + strPlace + "', @Concentrate = '" + strConcentrate  + "', @UserGroupID = '" + strUserAccount
				+ "', @Note = N'" + strNote + "', @Regulations = N'" + strRegulations + "', @Per = N'" + strPer + "', @LanguageID = '" + strLanguage
				+ "', @FirstName = N'" + strVFirstName 
				+ "', @LastName = N'" + strVLastName + "'";
			if (strStatus != "")
				strSql = strSql + ", @Status = '" + strStatus + "'";

			DataTable dtb = clsCommon.GetDataTable(strSql);
			return dtb;
		}

		public static string Save(Page page,DataGrid dtgList, string strStage, string strYYYY, 
			string strDays, string strConcentrate, string strAmount, string strSalaryMonth, string strNote, string strPlace)
		{
			SqlCommand cmd = new SqlCommand();
			SqlConnection SQLconn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction = SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "SI_spfrmUpdateConvalescence";
 
				for (int i = 0;i<= dtgList.Items.Count - 1;i++)
				{
					if (((CheckBox)dtgList.Items[i].FindControl("chkSelect")).Checked)
					{
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "Save";	
						if (dtgList.Items[i].Cells[4].Text.Trim() != "")
							cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = dtgList.Items[i].Cells[5].Text.Trim();
						else
							cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = DBNull.Value;
						if (strStage != "")
							cmd.Parameters.Add("@Stage", SqlDbType.Int).Value = strStage;
						else
							cmd.Parameters.Add("@Stage", SqlDbType.Int).Value = DBNull.Value;
						cmd.Parameters.Add("@YYYY",SqlDbType.NVarChar,4).Value = strYYYY;
						cmd.Parameters.Add("@Days",SqlDbType.Real).Value = strDays;
						cmd.Parameters.Add("@Concentrate",SqlDbType.Bit).Value = strConcentrate == "1"?1:0;
						if(strAmount != "") 
							cmd.Parameters.Add("@Amount",SqlDbType.Float).Value = Double.Parse(strAmount);
						else
							cmd.Parameters.Add("@Amount",SqlDbType.Float).Value = DBNull.Value;
						if(strSalaryMonth != "") 
							cmd.Parameters.Add("@SalaryMonth",SqlDbType.NVarChar,8).Value = strSalaryMonth;
						else
							cmd.Parameters.Add("@SalaryMonth",SqlDbType.NVarChar,8).Value = DBNull.Value;
						if(strNote != "") 
							cmd.Parameters.Add("@Note",SqlDbType.NVarChar,200).Value = strNote;
						else
							cmd.Parameters.Add("@Note",SqlDbType.NVarChar,200).Value = DBNull.Value;
						if(strPlace != "") 
							cmd.Parameters.Add("@Place",SqlDbType.NVarChar,200).Value = strPlace;
						else
							cmd.Parameters.Add("@Place",SqlDbType.NVarChar,200).Value = DBNull.Value;
						cmd.Parameters.Add("@Creater",SqlDbType.NVarChar,80).Value = page.Session["AccountLogin"];
						cmd.ExecuteNonQuery();
					}			
				}
				cmd.Transaction.Commit();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();	
				return "";
			}
			catch (Exception exp)
			{								
				cmd.Transaction.Rollback();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				return exp.Message.Trim();
			}
		}

		public static string Save(Page page,DataGrid dtgList, string strStage, string strYYYY, string strConcerntrate)
		{
			SqlCommand cmd = new SqlCommand();
			SqlConnection SQLconn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction = SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "SI_spfrmUpdateConvalescence";
 
				string strDay = "";
				string strLeaveDate = "";
				string strAmount = "";
				string strSalaryMonth = "";
				string strPer = "";
				string strNote = "";
				string strPlace = "";

				for (int i = 0;i<= dtgList.Items.Count - 1;i++)
				{
					if (((CheckBox)dtgList.Items[i].FindControl("chkSelect")).Checked)
					{
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "Save";	
						if (dtgList.Items[i].Cells[5].Text.Trim() != "")
							cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = dtgList.Items[i].Cells[5].Text.Trim();
						else
							cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = DBNull.Value;
						cmd.Parameters.Add("@YYYY",SqlDbType.NVarChar,4).Value = dtgList.Items[i].Cells[6].Text.Trim();
						cmd.Parameters.Add("@MM",SqlDbType.NVarChar,2).Value = dtgList.Items[i].Cells[7].Text.Trim();
						cmd.Parameters.Add("@Regulations",SqlDbType.NVarChar,12).Value = dtgList.Items[i].Cells[8].Text.Trim();
						cmd.Parameters.Add("@Stage", SqlDbType.Int).Value = dtgList.Items[i].Cells[9].Text.Trim();

						strDay = ((TextBox)dtgList.Items[i].FindControl("txtGridDay")).Text;
						cmd.Parameters.Add("@Days",SqlDbType.Real).Value = strDay ;

						cmd.Parameters.Add("@Concentrate",SqlDbType.Bit).Value = strConcerntrate == "1"?1:0;

						strAmount = ((TextBox)dtgList.Items[i].FindControl("txtGridAmount")).Text;
						if(strAmount != "") 
							cmd.Parameters.Add("@Amount",SqlDbType.Float).Value = Double.Parse(strAmount);
						else
							cmd.Parameters.Add("@Amount",SqlDbType.Float).Value = DBNull.Value;

						strLeaveDate = ((TextBox)dtgList.Items[i].FindControl("txtGridLeaveDate")).Text;
						if(strLeaveDate != "") 
							cmd.Parameters.Add("@LeaveDate",SqlDbType.NVarChar,12).Value = strLeaveDate;
						else
							cmd.Parameters.Add("@LeaveDate",SqlDbType.NVarChar,12).Value = DBNull.Value;

						strSalaryMonth = "";
						if(strSalaryMonth != "") 
							cmd.Parameters.Add("@SalaryMonth",SqlDbType.NVarChar,8).Value = strSalaryMonth;
						else
							cmd.Parameters.Add("@SalaryMonth",SqlDbType.NVarChar,8).Value = DBNull.Value;
						
						strPer = ((TextBox)dtgList.Items[i].FindControl("txtGridPer")).Text;
						if(strPer != "") 
							cmd.Parameters.Add("@Per",SqlDbType.Int).Value = strPer;
						else
							cmd.Parameters.Add("@Per",SqlDbType.Int).Value = DBNull.Value;

						strNote = ((TextBox)dtgList.Items[i].FindControl("txtGridNote")).Text;
						if(strNote != "") 
							cmd.Parameters.Add("@Note",SqlDbType.NVarChar,200).Value = strNote;
						else
							cmd.Parameters.Add("@Note",SqlDbType.NVarChar,200).Value = DBNull.Value;

						strPlace = ((TextBox)dtgList.Items[i].FindControl("txtGridPlace")).Text;
						if(strPlace != "") 
							cmd.Parameters.Add("@Place",SqlDbType.NVarChar,200).Value = strPlace;
						else
							cmd.Parameters.Add("@Place",SqlDbType.NVarChar,200).Value = DBNull.Value;
						cmd.Parameters.Add("@Creater",SqlDbType.NVarChar,80).Value = page.Session["AccountLogin"];
						cmd.ExecuteNonQuery();
					}			
				}
				cmd.Transaction.Commit();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();	
				return "";
			}
			catch (Exception exp)
			{								
				cmd.Transaction.Rollback();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				return exp.Message.Trim();
			}
		}
		public static string Delete(DataGrid dtgList)
		{
			SqlCommand cmd = new SqlCommand();
			SqlConnection SQLconn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction = SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "SI_spfrmUpdateConvalescence";
 
				CheckBox obj = new CheckBox();
				for (int i = 0;i<= dtgList.Items.Count - 1;i++)
				{
					obj = (CheckBox) dtgList.Items[i].FindControl("chkSelect");
					if (obj.Checked)
					{
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "Delete";						
						cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = dtgList.Items[i].Cells[0].Text.Trim();
						cmd.ExecuteNonQuery();											
					}
				}
				obj.Dispose();
				cmd.Transaction.Commit();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();	
				return "";
			}
			catch (Exception exp)
			{								
				cmd.Transaction.Rollback();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				return exp.Message.Trim();
			}
		}


	}
	#endregion Frm: Update convalescence


	#region clsUpdateSalarySI
	public class clsUpdateSalarySI
	{
		public static DataTable GetPRData(string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strEmpType, string strJobCode, string strLocation, string strCompany, string strStatus,
			string strFromDate, string strToDate, string strIsTransfer, string sUserGroupID)
		{
			string strSql = "SI_spfrmUpdateSalarySI @Activity = 'GetAll', @EmpID = N'" + strEmpID + "', @EmpName = N'" + strEmpName
				+ "', @LSLevel1ID = N'" + strLevel1 + "', @LSLevel2ID = N'" + strLevel2 + "', @LSLevel3ID = N'" + strLevel3
				+ "', @LSEmpTypeID = N'" + strEmpType + "', @LSJobCodeID = N'" + strJobCode 
				+ "', @LSLocationID = N'" + strLocation + "', @LSCompanyID = N'" + strCompany 
				+ "', @FromDate = N'" + strFromDate 
				+ "', @ToDate = N'" + strToDate
				+ "', @UserGroupID = N'" + sUserGroupID
				+ "', @IsTransfer = '" + strIsTransfer + "'";
			if (strStatus != "")
				strSql = strSql + ", @Status = '" + strStatus + "'";

			DataTable dtb = clsCommon.GetDataTable(strSql);
			return dtb;
		}	

		public static string SaveData(DataGrid dtgList, string strCreater)
		{
			SqlCommand cmd = new SqlCommand();
			SqlConnection SQLconn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction = SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "SI_spfrmUpdateSalarySI";
 
				for (int i = 0;i<= dtgList.Items.Count - 1;i++)
				{
					// Kiem tra dong co duoc chon khong
					if(((CheckBox)dtgList.Items[i].FindControl("chkSelect")).Checked==true)
					{
						string strTransfered = dtgList.Items[i].Cells[16].Text.Trim();
						if (strTransfered == "" || strTransfered == "&nbsp;") // Kiem tra da chuyen du lieu PR --> SI chua
						{
							string strPRSalaryID = dtgList.Items[i].Cells[0].Text.Trim();
							string strEmpID = dtgList.Items[i].Cells[1].Text.Trim();
							string strLastFromDate = dtgList.Items[i].Cells[2].Text.Trim();
							string strType = dtgList.Items[i].Cells[3].Text.Trim();
							string strLSCurrencyTypeID = dtgList.Items[i].Cells[4].Text.Trim();

							string strFromDateSI = ((TextBox)dtgList.Items[i].FindControl("txtFromDateSI")).Text.Trim();
							string strActualDateSI = ((TextBox)dtgList.Items[i].FindControl("txtActualDateSI")).Text.Trim();
							string strSalaryCoef = ((TextBox)dtgList.Items[i].FindControl("txtSalaryCoefSI")).Text.Trim();
							string strSalary = ((TextBox)dtgList.Items[i].FindControl("txtSalarySI")).Text.Trim();
							string strAllowanceCoef = ((TextBox)dtgList.Items[i].FindControl("txtAllowanceCoefSI")).Text.Trim();
							string strAllowance = ((TextBox)dtgList.Items[i].FindControl("txtAllowanceSI")).Text.Trim();
							string strNote = ((TextBox)dtgList.Items[i].FindControl("txtNoteSI")).Text.Trim();
							

							cmd.Parameters.Clear();
							cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "Save";	
							cmd.Parameters.Add("@PRSalaryID", SqlDbType.NVarChar, 12).Value = strPRSalaryID;
							cmd.Parameters.Add("@LSCurrencyTypeID", SqlDbType.NVarChar, 12).Value = strLSCurrencyTypeID;
							cmd.Parameters.Add("@EmpIDSI", SqlDbType.NVarChar, 12).Value = strEmpID;
							cmd.Parameters.Add("@FromDateSI", SqlDbType.NVarChar, 12).Value = strFromDateSI;
							cmd.Parameters.Add("@Type", SqlDbType.NVarChar, 20).Value = strType;
							cmd.Parameters.Add("@NoteSI", SqlDbType.NVarChar, 255).Value = strNote;
							cmd.Parameters.Add("@CreaterSI", SqlDbType.NVarChar, 20).Value = strCreater;
							if (strActualDateSI != "")
								cmd.Parameters.Add("@ActualDateSI", SqlDbType.NVarChar, 12).Value = strActualDateSI;
							else
								cmd.Parameters.Add("@ActualDateSI", SqlDbType.NVarChar, 12).Value = DBNull.Value;

							if (strLastFromDate != "" && strLastFromDate != "&nbsp;")
								cmd.Parameters.Add("@LastFromDate", SqlDbType.NVarChar, 12).Value = strLastFromDate;
							else
								cmd.Parameters.Add("@LastFromDate", SqlDbType.NVarChar, 12).Value = DBNull.Value;

							if (strSalaryCoef != "")
								cmd.Parameters.Add("@SalaryCoefSI", SqlDbType.Float).Value = strSalaryCoef;
							else
								cmd.Parameters.Add("@SalaryCoefSI", SqlDbType.Float).Value = DBNull.Value;

							if (strSalary != "")
								cmd.Parameters.Add("@SalarySI", SqlDbType.Money).Value = strSalary;
							else
								cmd.Parameters.Add("@SalarySI", SqlDbType.Money).Value = DBNull.Value;

							if (strAllowanceCoef != "")
								cmd.Parameters.Add("@AllowanceCoefSI", SqlDbType.Float).Value = strAllowanceCoef;
							else
								cmd.Parameters.Add("@AllowanceCoefSI", SqlDbType.Float).Value = DBNull.Value;

							if (strAllowance != "")
								cmd.Parameters.Add("@AllowanceSI", SqlDbType.Money).Value = strAllowance;
							else
								cmd.Parameters.Add("@AllowanceSI", SqlDbType.Money).Value = DBNull.Value;

							cmd.ExecuteNonQuery();		
						}// Kiem tra da chuyen du lieu PR --> SI chua
					}// Kiem tra dong duoc chon
				}//for
				cmd.Transaction.Commit();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();	
				return "";
			}//try
			catch (Exception exp)
			{								
				cmd.Transaction.Rollback();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				return exp.Message.Trim();
			}//catch
		}
	}
	#endregion clsUpdateSalarySI

	#region InsNoticeFund
	public class clsInsNoticeFund
	{
		public static DataRow GetInsNoticeFund(string strSource, string strMonth, string strStage, 
			string strLSCompanyID, string strCompanyType,string strLSLevel1ID)
		{
			//strCompanyType = "0": Khong phai loai tong cong ty, 1: loai tong cong ty: tong tat ca cac cong ty - lam sau

			DataRow dr;
			string strSql = "SI_spGetInsNoticeFund @Activity = '" + strSource + "', @Month = '" + strMonth 
				+ "', @Stage = '" + strStage + "', @LSCompanyID = '" + strLSCompanyID + "', @LSLevel1ID = '" + strLSLevel1ID 
				+ "', @CompanyType = '" + strCompanyType + "'";
			dr = clsCommon.GetDataRow(strSql);

			return dr;
		}
		public static DataRow GetInsNoticeFund_UI(string strSource, string strMonth, string strStage, 
			string strLSCompanyID, string strCompanyType,string strLSLevel1ID)
		{
			//strCompanyType = "0": Khong phai loai tong cong ty, 1: loai tong cong ty: tong tat ca cac cong ty - lam sau

			DataRow dr;
			string strSql = "SI_spGetInsNoticeFund_UI @Activity = '" + strSource + "', @Month = '" + strMonth 
				+ "', @Stage = '" + strStage + "', @LSCompanyID = '" + strLSCompanyID + "', @LSLevel1ID = '" + strLSLevel1ID 
				+ "', @CompanyType = '" + strCompanyType + "'";
			dr = clsCommon.GetDataRow(strSql);

			return dr;
		}

		public static DataRow GetInsNoticeFund_HI(string strSource, string strMonth, string strStage, 
			string strLSCompanyID, string strCompanyType,string strLSLevel1ID)
		{
			//strCompanyType = "0": Khong phai loai tong cong ty, 1: loai tong cong ty: tong tat ca cac cong ty - lam sau

			DataRow dr;
			string strSql = "SI_spGetInsNoticeFund_HI @Activity = '" + strSource + "', @Month = '" + strMonth 
				+ "', @Stage = '" + strStage + "', @LSCompanyID = '" + strLSCompanyID + "', @LSLevel1ID = '" + strLSLevel1ID 
				+ "', @CompanyType = '" + strCompanyType + "'";
			dr = clsCommon.GetDataRow(strSql);

			return dr;
		}
		public static DataTable GetDataSearch(string sCompanyID,  string sLevel1ID)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_spfrmSalaryFund @Activity = 'GetDataSearch', @LSCompanyID = '" + sCompanyID + "',@LSLevel1ID = '" + sLevel1ID + "'");
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
		public static DataTable GetDataSearch_UI(string sCompanyID,  string sLevel1ID)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_spfrmSalaryFund_UI @Activity = 'GetDataSearch', @LSCompanyID = '" + sCompanyID + "',@LSLevel1ID = '" + sLevel1ID + "'");
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
		public static DataTable GetDataSearch_HI(string sCompanyID,  string sLevel1ID)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_spfrmSalaryFund_HI @Activity = 'GetDataSearch', @LSCompanyID = '" + sCompanyID + "',@LSLevel1ID = '" + sLevel1ID + "'");
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
		public static DataTable GetAll(string sLanguageID)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_spfrmSalaryFund @Activity = 'GetAll', @LanguageID = '" + sLanguageID + "'");
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
		public static DataTable GetAll_UI(string sLanguageID)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_spfrmSalaryFund_UI @Activity = 'GetAll', @LanguageID = '" + sLanguageID + "'");
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

		public static DataTable GetAll_HI(string sLanguageID)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_spfrmSalaryFund_HI @Activity = 'GetAll', @LanguageID = '" + sLanguageID + "'");
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
			DataRow iRow = clsCommon.GetDataRow("SI_spfrmSalaryFund @Activity = 'GetDataByID', @SalaryFundID = '" + strID + "'");
			if(iRow != null)
				return iRow;
			else
				return null;
		}
		public static DataRow GetDataByID_UI(Object strID)
		{
			DataRow iRow = clsCommon.GetDataRow("SI_spfrmSalaryFund_UI @Activity = 'GetDataByID', @SalaryFundID = '" + strID + "'");
			if(iRow != null)
				return iRow;
			else
				return null;
		}
		public static DataRow GetDataByID_HI(Object strID)
		{
			DataRow iRow = clsCommon.GetDataRow("SI_spfrmSalaryFund_HI @Activity = 'GetDataByID', @SalaryFundID = '" + strID + "'");
			if(iRow != null)
				return iRow;
			else
				return null;
		}	
		public static string GetSysParameters(String param)
		{
			String str;
			str=clsCommon.GetSYSParamValue(param);  	
			return str;
		}

	}
	#endregion InsNoticeFund

	#region clsSIBookList
	public class clsSIBookList
	{
		public static DataTable GetExaminingPlace(string strLanguage)
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_spfrmHICard @Activity = 'GetExaminingPlaceCombo', @LanguageID = '" + strLanguage + "'");
			}
			catch(Exception ex)
			{

			}
			return dt;
		}
		public static DataTable GetIssuedPlace(string strLanguage)
		{
			string strTextField = "EN";
			DataTable dt = new DataTable();
			try
			{
				//dt = clsCommon.GetDataTable("sp_GetDataCombo @TableName = 'LS_tblProvince', @Fields = 'LSProvinceID IssuedPlace," + strTextField + " as Name'");   
				dt = clsCommon.GetDataTable("SI_spfrmSIBookList @Activity = 'GetIssuedPlaceCombo', @LanguageID = '" + strLanguage + "'");
			}
			catch(Exception ex)
			{

			}
			return dt;
		}
		public static DataTable CreateList_HI(string strEmpCodeList, string strAccountLogin)
		{
			string [] EmpList ;
			EmpList = strEmpCodeList.Split(new char [] {','});
			string strEmpList = "";
			string strEmpID = "";
			for(int i=0; i<EmpList.Length; i++)
			{
				strEmpID = clsSI.GetEmpIDFromCode(EmpList[i].ToString());
				strEmpList = strEmpList + "," + strEmpID;
			}
			int intLen = strEmpList.Length - 1;
			strEmpList = strEmpList.Substring(1, intLen);
				
			DataTable dt = new DataTable();
			
			string strSql = "SI_spfrmHICard @Activity = 'CreateHIBookListByEmpList', @EmpList = '" + strEmpList + "' ,@UserGroupID = N'" + strAccountLogin + "'";
			dt = clsCommon.GetDataTable(strSql);

			return dt;
		}	

		public static DataTable CreateList_DangKyKCB(string strEmpCodeList, string strAccountLogin)
		{
			string [] EmpList ;
			EmpList = strEmpCodeList.Split(new char [] {','});
			string strEmpList = "";
			string strEmpID = "";
			for(int i=0; i<EmpList.Length; i++)
			{
				strEmpID = clsSI.GetEmpIDFromCode(EmpList[i].ToString());
				strEmpList = strEmpList + "," + strEmpID;
			}
			int intLen = strEmpList.Length - 1;
			strEmpList = strEmpList.Substring(1, intLen);
			DataTable dt = new DataTable();
			string strSql = "SI_spfrmHICard @Activity = 'CreateListDangKyKCB_List',@EmpList = '" + strEmpList + "' ,@UserGroupID = N'" + strAccountLogin + "' ";
			dt = clsCommon.GetDataTable(strSql);
			return dt;
		}	
		public static DataTable CreateList(string strEmpCodeList)
		{
			string [] EmpList ;
			EmpList = strEmpCodeList.Split(new char [] {','});
			string strEmpList = "";
			string strEmpID = "";
			for(int i=0; i<EmpList.Length; i++)
			{
				strEmpID = clsSI.GetEmpIDFromCode(EmpList[i].ToString());
				strEmpList = strEmpList + "," + strEmpID;
			}
			int intLen = strEmpList.Length - 1;
			strEmpList = strEmpList.Substring(1, intLen);
				
			DataTable dt = new DataTable();
			/*string strSql = "SI_spfrmSIBookList @Activity = 'GetSIBookListByEmpList', @EmpList = '" + strEmpList 
				+ "', @SIBookNo = '" + strSIBookNo + "', @IssuedDate = '" + strIssuedDate
				+ "', @EffDateBegin = '" + strEffDateBegin + "', @IssuedPlace = '" + strIssuedPlace
				+ "', @EffDateAtComp = '" + strEffDateAtComp + "'";
			*/
			string strSql = "SI_spfrmSIBookList @Activity = 'CreateSIBookListByEmpList', @EmpList = '" + strEmpList + "'";
			dt = clsCommon.GetDataTable(strSql);

			return dt;
		}	

		public static DataTable FilterList(string strEmpCodeList)
		{
			string [] EmpList ;
			EmpList = strEmpCodeList.Split(new char [] {','});
			string strEmpList = "";
			string strEmpID = "";
			for(int i=0; i<EmpList.Length; i++)
			{
				strEmpID = clsSI.GetEmpIDFromCode(EmpList[i].ToString());
				strEmpList = strEmpList + "," + strEmpID;
			}
			int intLen = strEmpList.Length - 1;
			strEmpList = strEmpList.Substring(1, intLen);
				
			DataTable dt = new DataTable();
			string strSql = "SI_spfrmSIBookList @Activity = 'GetSIBookListByEmpList', @EmpList = '" + strEmpList + "'";
			dt = clsCommon.GetDataTable(strSql);

			return dt;
		}	
		
		public static DataTable CreateListByCondition(string strHaveSIBook, string strFromDate, string strToDate,
			string strEmpID, string strEmpName, string strLevel1, string strLevel2, string strLevel3, 
			string strPosition, string strJobCode, string strLocation, string strCompany, string strStatus,string strAccountLogin)
		{
			DataTable dt = new DataTable();
			string strSql = "SI_spfrmSIBookList @Activity = 'CreateSIBookListByCondition"	
				+ "', @HaveSIBook = '" + strHaveSIBook
				+ "', @FromDate = '" + strFromDate
				+ "', @ToDate = '" + strToDate + "'";
			strSql = strSql + ", @EmpID = N'" + strEmpID + "', @EmpName = N'" + strEmpName
				+ "', @LSLevel1ID = N'" + strLevel1 + "', @LSLevel2ID = N'" + strLevel2 + "', @LSLevel3ID = N'" + strLevel3
				+ "', @LSPositionID = N'" + strPosition + "', @LSJobCodeID = N'" + strJobCode 
				+ "', @UserGroupID = N'" + strAccountLogin 
				+ "', @LSLocationID = N'" + strLocation + "', @LSCompanyID = N'" + strCompany + "'";
			if (strStatus != "")
				strSql = strSql + ", @Status = '" + strStatus + "'";
			/*string strSql = "SI_spfrmSIBookList @Activity = 'GetSIBookListByCondition"				+ "', @SIBookNo = '" + strSIBookNo + "', @IssuedDate = '" + strIssuedDate
				+ "', @EffDateBegin = '" + strEffDateBegin + "', @IssuedPlace = '" + strIssuedPlace
				+ "', @EffDateAtComp = '" + strEffDateAtComp
				+ "', @HaveSIBook = '" + strHaveSIBook
				+ "', @FromDate = '" + strFromDate
				+ "', @ToDate = '" + strToDate + "'";
			strSql = strSql + ", @EmpID = N'" + strEmpID + "', @EmpName = N'" + strEmpName
				+ "', @LSLevel1ID = N'" + strLevel1 + "', @LSLevel2ID = N'" + strLevel2 + "', @LSLevel3ID = N'" + strLevel3
				+ "', @LSPositionID = N'" + strPosition + "', @LSJobCodeID = N'" + strJobCode 
				+ "', @LSLocationID = N'" + strLocation + "', @LSCompanyID = N'" + strCompany + "'";
			if (strStatus != "")
				strSql = strSql + ", @Status = '" + strStatus + "'";
			*/
			dt = clsCommon.GetDataTable(strSql);
			return dt;
		}
		public static DataTable CreateListByCondition_HICard(string strFromDate ,string strToDate,string intChangePlace, string strExaminingCardPlace, string strNote,string  strEmpID,string strEmpName,string strLevel1,string strLevel2,string strLevel3,string strPosition,string strJobCode,string strLocation,string strCompany,string strStatus,string strExaminingPlaceSearch,string strAccountLogin)
		{
			DataTable dt = new DataTable();
			
			string strSql = "SI_spfrmHICard @Activity = 'CreateByCondition_HICard"	
				+ "', @FromDate = '" + strFromDate
				+ "', @ToDate = '" + strToDate + "'";
			strSql = strSql + ", @EmpID = N'" + strEmpID + "', @EmpName = N'" + strEmpName
				+ "', @LSLevel1ID = N'" + strLevel1 + "', @LSLevel2ID = N'" + strLevel2 + "', @LSLevel3ID = N'" + strLevel3
				+ "', @LSPositionID = N'" + strPosition + "', @LSJobCodeID = N'" + strJobCode 
				+ "', @UserGroupID = N'" + strAccountLogin
				+ "', @LSLocationID = N'" + strLocation + "', @LSCompanyID = N'" + strCompany + "'";
			if (strStatus != "")
				strSql = strSql + ", @Status = '" + strStatus + "'";
			
			dt = clsCommon.GetDataTable(strSql);
			return dt;
		}

		public static DataTable CreateList_DangKyKCB_2(string strFromDate ,string strToDate, string strExaminingCardPlace, string strNote,string  strEmpID,string strEmpName,string strLevel1,string strLevel2,string strLevel3,string strPosition,string strJobCode,string strLocation,string strCompany,string strStatus,string strExaminingPlaceSearch,string strAccountLogin)
		{
			DataTable dt = new DataTable();
			
			string strSql = "SI_spfrmHICard @Activity = 'CreateListDangKyKCB_Condition"	
				+ "', @FromDate = '" + strFromDate
				+ "', @ToDate = '" + strToDate + "'";
			strSql = strSql + ", @EmpID = N'" + strEmpID + "', @EmpName = N'" + strEmpName
				+ "', @LSLevel1ID = N'" + strLevel1 + "', @LSLevel2ID = N'" + strLevel2 + "', @LSLevel3ID = N'" + strLevel3
				+ "', @LSPositionID = N'" + strPosition + "', @LSJobCodeID = N'" + strJobCode 
				+ "', @UserGroupID = N'" + strAccountLogin
				+ "', @LSLocationID = N'" + strLocation + "', @LSCompanyID = N'" + strCompany + "'";
			if (strStatus != "")
				strSql = strSql + ", @Status = '" + strStatus + "'";
			
			dt = clsCommon.GetDataTable(strSql);
			return dt;
		}
		public static DataTable FilterListByCondition(string strHaveSIBook, string strFromDate, string strToDate,
			string strEmpID, string strEmpName, string strLevel1, string strLevel2, string strLevel3, 
			string strPosition, string strJobCode, string strLocation, string strCompany, string strStatus, string strAccountLogin)
		{
			DataTable dt = new DataTable();
			string strSql = "SI_spfrmSIBookList @Activity = 'GetSIBookListByCondition"	
				+ "', @HaveSIBook = '" + strHaveSIBook
				+ "', @FromDate = '" + strFromDate
				+ "', @ToDate = '" + strToDate + "'";
			strSql = strSql + ", @EmpID = N'" + strEmpID + "', @EmpName = N'" + strEmpName
				+ "', @LSLevel1ID = N'" + strLevel1 + "', @LSLevel2ID = N'" + strLevel2 + "', @LSLevel3ID = N'" + strLevel3
				+ "', @LSPositionID = N'" + strPosition + "', @LSJobCodeID = N'" + strJobCode 
				+ "', @UserGroupID = N'" + strAccountLogin
				+ "', @LSLocationID = N'" + strLocation + "', @LSCompanyID = N'" + strCompany + "'";
			if (strStatus != "")
				strSql = strSql + ", @Status = '" + strStatus + "'";
			dt = clsCommon.GetDataTable(strSql);
			return dt;
		}
		
		public static string SaveData(DataGrid dtgList, string strCreater, string strLanguage)
		{
			string strErr = "";
			SqlCommand cmd = new SqlCommand();
			SqlConnection SQLconn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction = SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "SI_spfrmSI";
 
				string strEmpID = "";
				string strSIBookNo = "";
				string strIssuedDate = "";
				string strEffDateBegin = "";
				string strEffDateAtComp = "";
				string strIssuedPlace = "";
				string strNote = "";

				for (int i = 0;i<= dtgList.Items.Count - 1;i++)
				{
					// Kiem tra dong co duoc chon khong
					if(((CheckBox)dtgList.Items[i].FindControl("chkSelect")).Checked==true)
					{
						strEmpID = dtgList.Items[i].Cells[1].Text.Trim();
						strSIBookNo = ((TextBox)dtgList.Items[i].FindControl("txtSIBookNoGrid")).Text.Trim();
						strIssuedDate = ((TextBox)dtgList.Items[i].FindControl("txtIssuedDateGrid")).Text.Trim();
						strEffDateBegin = ((TextBox)dtgList.Items[i].FindControl("txtEffDateBeginGrid")).Text.Trim();
						strEffDateAtComp = ((TextBox)dtgList.Items[i].FindControl("txtEffDateAtCompGrid")).Text.Trim();
						strIssuedPlace = ((DropDownList)dtgList.Items[i].FindControl("cboIssuedPlaceGrid")).SelectedValue.Trim();
						strNote = ((TextBox)dtgList.Items[i].FindControl("txtNoteGrid")).Text.Trim();

						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "Save";	
						cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = strEmpID;
						cmd.Parameters.Add("@SIBookNo", SqlDbType.NVarChar, 20).Value = strSIBookNo;
						cmd.Parameters.Add("@IssuedPlace", SqlDbType.NVarChar, 12).Value = strIssuedPlace;
						cmd.Parameters.Add("@Note", SqlDbType.NVarChar, 200).Value = strNote;
						if (strIssuedDate != "")
							cmd.Parameters.Add("@IssuedDate", SqlDbType.NVarChar, 12).Value = strIssuedDate;
						else
							cmd.Parameters.Add("@IssuedDate", SqlDbType.NVarChar, 12).Value = DBNull.Value;
						if (strEffDateBegin != "")
							cmd.Parameters.Add("@EffDateBegin", SqlDbType.NVarChar, 12).Value = strEffDateBegin;
						else
							cmd.Parameters.Add("@EffDateBegin", SqlDbType.NVarChar, 12).Value = DBNull.Value;
						if (strEffDateAtComp != "")
							cmd.Parameters.Add("@EffDateAtComp", SqlDbType.NVarChar, 12).Value = strEffDateAtComp;
						else
							cmd.Parameters.Add("@EffDateAtComp", SqlDbType.NVarChar, 12).Value = DBNull.Value;
						
						cmd.Parameters.Add("@Creater", SqlDbType.NVarChar, 20).Value = strCreater;
						cmd.Parameters.Add("@ReturnMess", SqlDbType.NVarChar,1000).Direction = ParameterDirection.InputOutput;
						cmd.Parameters.Add("@LanguageID", SqlDbType.NChar,2).Value = strLanguage;
						
						cmd.ExecuteNonQuery();		
						strErr = cmd.Parameters["@ReturnMess"].Value.ToString();

						if (strErr != "")
						{
							cmd.Transaction.Rollback();
							cmd.Dispose();
							SQLconn.Close();
							SQLconn.Dispose();
							return strErr;
						}


					}// Kiem tra dong duoc chon
				}//for
				cmd.Transaction.Commit();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();	
				return "";
			}//try
			catch (Exception exp)
			{								
				cmd.Transaction.Rollback();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				return exp.Message.Trim();
			}//catch
		}
	}


	#endregion clsSIBookList

	#region  ClsGetData47B
	/// <summary>
	/// MinhNT
	/// </summary>
	public class clsSI_Data47B_info
	{
		public clsSI_Data47B_info()
		{
		}
		public static DataTable  GetData47b_infor(string Activity,string FromDate,string ToDate,string Language,string strEmpID,string strEmpName,string strLevel1,string strLevel2,string strLevel3,string strCompany,string strStatus, string strEmpType,string IsNotice,string strReportMonth,string strChangeDataID,string empID,string NewData,string OldData,string LSChangeDataTypeID,string ConTent, string Reason, string strUserAccount)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_spfrmGetDataC47b_Info @Activity = '"+ Activity +"', @FromDate = '" + FromDate + "', @ToDate = '" + ToDate  + "',@Language='" + Language  + "',@strEmpID='"+ strEmpID +"',@strEmpName='"+ strEmpName +"',@strLevel1='" +strLevel1  + "',@strLevel2='" + strLevel2 + "',@strLevel3='"+ strLevel3  +"',@strCompany='"+ strCompany  +"',@strStatus='" + strStatus  + "',@UserGroupID='" + strUserAccount  + "',@strEmpType='" + strEmpType  + "',@IsNotice='"+ IsNotice +"',@ReportMonth='" + strReportMonth  + "',@ChangeDataID='"+strChangeDataID+"',@EmpID='"+ empID +"',@NewData='"+ NewData +"',@OldData='"+ OldData  +"',@LSChangeDataTypeID='"+ LSChangeDataTypeID +"',@ConTent='"+ ConTent +"',@Reason='"+ Reason +"'");
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
		public static void  SaveData47b_infor(string Activity,string FromDate,string ToDate,string Language,string strEmpID,string strEmpName,string strLevel1,string strLevel2,string strLevel3,string strCompany,string strStatus,string IsNotice,string strReportMonth,string strChangeDataID,string empID,string NewData,string OldData,string LSChangeDataTypeID,string ConTent, string Reason)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_spfrmGetDataC47b_Info @Activity = '"+ Activity +"', @FromDate = '" + FromDate + "', @ToDate = '" + ToDate  + "',@Language='" + Language  + "',@strEmpID='"+ strEmpID +"',@strEmpName='"+ strEmpName +"',@strLevel1='" +strLevel1  + "',@strLevel2='" + strLevel2 + "',@strLevel3='"+ strLevel3  +"',@strCompany='"+ strCompany  +"',@strStatus='" + strStatus  + "',@IsNotice='"+ IsNotice +"',@ReportMonth='" + strReportMonth  + "',@ChangeDataID='"+strChangeDataID+"',@EmpID='"+ empID +"',@NewData=N'"+ NewData +"',@OldData=N'"+ OldData  +"',@LSChangeDataTypeID='"+ LSChangeDataTypeID +"',@ConTent=N'"+ ConTent +"',@Reason=N'"+ Reason +"'");
				
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				
			}
			finally
			{
				dt.Dispose();
			}
		}
	}
		
	#endregion

	#region clsSalarySI
	public class clsSalarySI
	{
		public static DataTable GetDataByEmpID(object sEmpID,string sLanguageID)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("SI_spfrmSalarySI @Activity = 'GetDataByEmpID', @EmpID = N'" + sEmpID + "', @LanguageID = '" + sLanguageID + "'");
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
		public static bool CheckValidEffectiveDate(Object strID,string strEffDate,string strSalaryRecordID,string strActivity)
		{			
			if(strActivity == "AddNew")
			{
				DataRow iRow = clsCommon.GetDataRow("SI_spfrmSalarySI @Activity = 'CheckValidSave', @EmpID = N'" + strID + "', @FromDate = '" + strEffDate + "'");
				if(iRow != null)
					return false; 
				else
					return true;
			}
			else
			{
				DataRow iRow = clsCommon.GetDataRow("SI_spfrmSalarySI @Activity = 'CheckValidUpdate', @EmpID = N'" + strID + "', @FromDate = '" + strEffDate + "', @SalaryRecordID = '" + strSalaryRecordID+"'");
				if(iRow != null)
					return false; 
				else
					return true;
			}
			return false;
		}
		public static DataRow GetDataByID(Object strID)
		{
			DataRow iRow = clsCommon.GetDataRow("SI_spfrmSalarySI @Activity = 'GetDataByID', @SalarySIID = '" + strID + "'");
			if(iRow != null)
				return iRow;
			else
				return null;
		}	
		public static string GetSysParameters(String param)
		{
			String str;
			str=clsCommon.GetSYSParamValue(param);  	
			return str;
		}
	}
	#endregion clsSalarySI

	#region clsConvalescence
	/// <summary>
	/// KhanhMT
	/// </summary>
	public class clsConvalescence
	{
		public static DataTable GetDataByEmpID(object sEmpID,string sLanguageID)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("SI_spfrmConvalescence @Activity = 'GetDataByEmpID', @EmpID = N'" + sEmpID + "', @LanguageID = '" + sLanguageID + "'");
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
			DataRow iRow = clsCommon.GetDataRow("SI_spfrmSalarySI @Activity = 'GetDataByID', @SalarySIID = '" + strID + "'");
			if(iRow != null)
				return iRow;
			else
				return null;
		}	
		public static string GetSysParameters(String param)
		{
			String str;
			str=clsCommon.GetSYSParamValue(param);  	
			return str;
		}
	}
	#endregion clsConvalescence

	#region ClsSI_ConvalescenceProcess
	/// <summary>
	/// MinhNT 
	/// </summary>
	public class clsSI_ConvalescenceProcess
	{
		public clsSI_ConvalescenceProcess()
		{
		}
		public static DataTable GetDataConvalescenceProcess(string id,string EmpID,string LeaveDate,string Days,string Concentrate,string Amount,string Note,string Language,string strEmpID,string strEmpName,string strLevel1,string strLevel2,string strLevel3,string strCompany,string strStatus,string EmpCode,string Activity)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_spfrmConvalescenceProcess @ID = '"+ id  +
					"', @EmpID = '" + EmpID  + "', @LeaveDate = '" + LeaveDate  + 
					"',@Days='" + Days   + "',@Concentrate='"+ Concentrate  +"',@Amount='"+ Amount  +
					"',@Note='" + Note   + "',@Language='" + Language  + "',@strEmpID='"+ strEmpID  +
					"',@strEmpName='"+ strEmpName  +"',@strLevel1='" + strLevel1   + 
					"',@strLevel2='"+ strLevel2  +"',@strLevel3='" + strLevel3   + 
					"',@strCompany='"+ strCompany +"',@strStatus='"+ strStatus  +
					"',@EmpCode='"+ EmpCode  +"',@Activity='"+ Activity  +"'");
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
		public static void DeleteData(string id, string Activity)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_spfrmConvalescenceProcess @ID = '"+ id  +
					"', @EmpID = '', @LeaveDate = '',@Days='',@Concentrate='',@Amount='',@Note='',@Language='',@strEmpID='',@strEmpName='',@strLevel1='',@strLevel2='',@strLevel3='',@strCompany='',@strStatus='',@EmpCode='',@Activity='"+ Activity  +"'"); 
				
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				
			}
			finally
			{
				dt.Dispose();
			}
		}
		public static void SavaData(string id,string Empcode,string LeaveDate,string Days,string Concentrate,string Amount,string note,string Activity)
		{
			
			DataTable dt = new DataTable();
			try
			{
				if(Amount=="&nbsp;" )
				{
					Amount="0";	
				}
				dt = clsCommon.GetDataTable( "SI_spfrmConvalescenceProcess @ID = '"+ id +"', @EmpID = '', @LeaveDate = '"+ LeaveDate +"',@Days='"+ Days +"',@Concentrate='"+ Concentrate  +"',@Amount='"+ Amount  +"',@Note=N'"+ note +"',@Language='',@strEmpID='',@strEmpName='',@strLevel1='',@strLevel2='',@strLevel3='',@strCompany='',@strStatus='',@EmpCode='" + Empcode  + "',@Activity='"+ Activity  +"'"); 
				
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				
			}
			finally
			{
				dt.Dispose();
			}

		}
	}
	#endregion
	#region ClsSI_BookList
	/// <summary>
	/// MinhNT
	/// </summary>
	public class ClsSI_BookList
	{
		public ClsSI_BookList()
		{
		}
		public static void SaveSIGroup( string EmpID, string SiBookNo, string IssuedDate, string IssuedPlace,string EffDateBegin, string EffDateAtComp,string Note,string Activity)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb=clsCommon.GetDataTable("SI_spfrmSIBookList @EmpID='"+ EmpID +"',@SiBookNo='"+ SiBookNo  +"',@IssuedDate='"+ IssuedDate  +"',@IssuedPlace='"+ IssuedPlace  +"',@EffDateBegin='"+ EffDateBegin   +"',@EffDateAtComp='"+ EffDateAtComp  +"',@Note=N'"+ Note +"',@Activity='"+ Activity  +"'");  
			}
			catch(Exception ex) 
			{
				
			}

		}
		public static DataTable SaveSIByListCondition(string EmpID,string SiBookNo,string IssuedDate,string IssuedPlace,string EffDateBegin,string EffDateAtComp,string Note,string LSLevel1ID,string LSLevel2ID,string LSLevel3ID,string LSCompanyID,string Status,string EmpName,string BeginNumber ,string Activity )
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb=clsCommon.GetDataTable("SI_spfrmSIBookList @EmpID='"+ EmpID +"',@SiBookNo='"+ SiBookNo  +"',@IssuedDate='"+ IssuedDate  +"',@IssuedPlace='"+ IssuedPlace  +"',@EffDateBegin='"+ EffDateBegin   +"',@EffDateAtComp='"+ EffDateAtComp  +"',@Note=N'"+ Note +"',@LSLevel1ID='"+ LSLevel1ID   +"',@LSLevel2ID='"+ LSLevel2ID  +"',@LSLevel3ID='"+ LSLevel3ID  +"',@LSCompanyID='"+ LSCompanyID  +"',@Status='"+ Status  +"',@EmpName='"+ EmpName  +"',@BeginNumber='"+ BeginNumber +"',@Activity='"+ Activity +"'");  
				
			}
			catch(Exception ex) 
			{
				
			}
			return dtb;
		}
		public static string GetAutoGenarateNumber(string BeginNumber,string TotalNumber)
		{
			string Result;
			DataTable dtb=new DataTable(); 
			dtb=clsCommon.GetDataTable("SI_spfrmSIBookList @BeginNumber='" + BeginNumber + "',@TotalNumber='" + TotalNumber +"',@Activity='GetAutoNumber'");
			Result =dtb.Rows[0].ItemArray[0].ToString();
			return Result; 
		}
		public static DataTable CheckSave(string EmpID, string Activity,string Language,string SINo)
		{
			DataTable dtb=new DataTable();
			try
			{
				dtb=clsCommon.GetDataTable("SI_spfrmSIBookList @EmpID='"+ EmpID +"',@Language='"+ Language  +"',@Activity='"+ Activity +"',@SiBookNo='"+ SINo  +"'");   
			}
			catch(Exception ex)
			{

			}
			return dtb;
		}
	}
	#endregion 
	#region DataForC47b
	public class ClsSi_DataforC47b
	{
		public ClsSi_DataforC47b()
		{
		}
		public static DataTable GetDataForC47B(string strChangeType,string strFromMonth,string strToMonth,string strLanguage,string strEmp,string strEmpName,string strLevel1,string strLevel2,string strLevel3,string strCompany,string strStatus,string strEmpType,string strKBC,string strActivity,string strUserAccount )
		{
				DataTable dtb = new DataTable();
				try
				{
								dtb=clsCommon.GetDataTable("SI_spfrmDataForC47b @ChangeType=N'"+ strChangeType +"',@FromMonth='"+ strFromMonth +"',@ToMonth='"+ strToMonth +"',@Language='"+ strLanguage +"',@strEmpID='"+ strEmp +"',@strEmpName='"+ strEmpName +"',@strLevel1='"+ strLevel1 +"',@strLevel2='"+ strLevel2 +"',@strLevel3='"+ strLevel3  +"',@strCompany='"+strCompany+"',@Status='"+ strStatus +"',@UserGroupID='" + strUserAccount  + "',@strEmpType='"+ strEmpType +"',@KCB='"+ strKBC +"',@Activity='"+ strActivity +"'");  
				}
			catch(Exception ex) 
			{
					
			}
				return dtb;
		}
		public static DataTable GetDataForCombo(string strLanguage,string strActivity)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb=clsCommon.GetDataTable("SI_spfrmDataForC47b @Language=N'"+ strLanguage +"',@Activity='"+ strActivity +"'");  
			}
			catch(Exception ex) 
			{
					
			}
			return dtb;
		}
		public static void DeleteDataC47b(string strChangeType,string strFromMonth,string strToMonth,string strLanguage,string strEmp,string strActivity)
		{
			DataTable dtb = new DataTable();
			try
			{
					dtb=clsCommon.GetDataTable("SI_spfrmDataForC47b @ChangeType=N'"+ strChangeType +"',@FromMonth='"+ strFromMonth +"',@ToMonth='"+ strToMonth +"',@Language='"+ strLanguage +"',@strEmpID='"+ strEmp +"',@Activity='"+ strActivity +"'");  
			}
			catch(Exception ex) 
			{
					
			}
		}
	}
	#endregion
	#region ClsSi_GetDataC47bPlace
	public class ClsSI_GetDataC47bPlace
	{
		public ClsSI_GetDataC47bPlace()
		{
		}
		public static DataTable GetDataC47bPlace(string strLanguage,string strReportMonth,string strEmpID,string strName,string strLevel1,string strLevel2,string strLevel3,string strCompany,string strStatus, string strEmpType,string strActivityKCB,string strIsNotice,string strChangeDataId,string strLShospitalID_OLD,string strLShospitalID_New,string strIsNoticeDate,string strActivity, string strUserAccount )
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb=clsCommon.GetDataTable("SI_spfrmGetDataC47b_TPlace @Language=N'"+ strLanguage +"',@ReportedMonth='"+ strReportMonth +"',@strEmpID='"+ strEmpID +"',@strEmpName='"+ strName +"',@strLevel1='"+ strLevel1 +"',@strLevel2='"+ strLevel2 +"',@strLevel3='"+ strLevel3 +"',@strCompany='"+ strCompany  +"',@Status='"+ strStatus +"',@strEmpType='"+ strEmpType +"',@ActivityKCB='"+ strActivityKCB  +"',@IsNotice='"+ strIsNotice +"',@ChangDataID='"+ strChangeDataId  +"',@LShospitalID_OLD='"+strLShospitalID_OLD +"',@LShospitalID_New='"+ strLShospitalID_New  +"',@IsNoticeDate='"+ strIsNoticeDate +"',@UserGroupID='" + strUserAccount  + "',@Activity='" +strActivity+"'");  
			}
			catch(Exception ex) 
			{
					
			}
			return dtb;
		}
		public static DataTable GetDataForCombo(string strLanguage,string strActivity)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb=clsCommon.GetDataTable("SI_spfrmGetDataC47b_TPlace @Language=N'"+ strLanguage +"',@Activity='" +strActivity+"'");  
			}
			catch(Exception ex) 
			{
					
			}
			return dtb;
		}
		public static void SaveData(string strLanguage,string strEmpID,string strStatus ,string strChangeDataId,string strLShospitalID_OLD,string strLShospitalID_New,string strIsNoticeDate,string strActivity)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb=clsCommon.GetDataTable("SI_spfrmGetDataC47b_TPlace @Language=N'"+ strLanguage +"',@strEmpID='"+ strEmpID +"',@Status='"+ strStatus +"',@ChangDataID='"+ strChangeDataId  +"',@LShospitalID_OLD='"+strLShospitalID_OLD +"',@LShospitalID_New='"+ strLShospitalID_New  +"',@IsNoticeDate='"+ strIsNoticeDate +"',@Activity='" +strActivity+"'");  
			}
			catch(Exception ex) 
			{
					
			}
			
		}
	}
	#endregion
	#region ClsSI_TimeSheetSI
	public class ClsSI_TimeSheetSI
	{
		public ClsSI_TimeSheetSI()
		{
		}
		public static DataTable GetDataTimeSheetSI(string FromDate,string ToDate,string LeaveType,string strLanguage,string strEmpID,string strEmpName,string strLevel1,string strLevel2,string strLevel3,string strCompany,string strStatus)
		{
			DataTable dtb = new DataTable();
			try
			{
				 dtb = clsCommon.GetDataTable("SI_spfrmTimeSheetSI @FromDate='"+ FromDate +"',@ToDate='"+ ToDate +"',@LeaveType='"+ LeaveType + "',@Language='"+ strLanguage +"',@strEmpID='"+ strEmpID +"',@strEmpName='"+ strEmpName  +"',@strLevel1='"+ strLevel1 +"',@strLevel2='"+ strLevel2 +"',@strLevel3='"+ strLevel3 +"',@strCompany='"+ strCompany  +"',@strStatus='"+ strStatus +"',@Activity='loaddata'");
				
			}
			catch(Exception ex) 
			{
					
			}
			return dtb;
		}
		public static DataTable LoadCombo(string strLanguage)
		{
			DataTable dtb = new DataTable();
			try
			{			
				 dtb=clsCommon.GetDataTable("SI_spfrmTimeSheetSI @Language='"+ strLanguage +"',@Activity='loadcombo'");  	
			}
			catch(Exception ex) 
			{
					
			}
			return dtb;
		}
		public static DataTable GetDataFromTimeSheet(string FromDate,string ToDate,string LeaveType,string strLanguage,string strEmpID,string strEmpName,string strLevel1,string strLevel2,string strLevel3,string strCompany,string strStatus)
		{
			DataTable dtb = new DataTable();
			try
			{
				//dtb = clsCommon.GetDataTable("SI_spfrmTimeSheetSI @FromDate='"+ FromDate +"',@ToDate='"+ ToDate +"',@LeaveType='"+ LeaveType + "',@Language='"+ strLanguage +"',@strEmpID='"+ strEmpID +"',@strEmpName='"+ strEmpName  +"',@strLevel1='"+ strLevel1 +"',@strLevel2='"+ strLevel2 +"',@strLevel3='"+ strLevel3 +"',@strCompany='"+ strCompany  +"',@strStatus='"+ strStatus +"',@Activity='loaddata'");
				 dtb = clsCommon.GetDataTable("SI_spfrmTimeSheetSI @FromDate='"+ FromDate +"',@ToDate='"+ ToDate +"',@LeaveType='"+ LeaveType + "',@Language='"+ strLanguage +"',@strEmpID='"+strEmpID +"',@strEmpName='"+strEmpName +"',@strLevel1='"+strLevel1 +"',@strLevel2='"+strLevel2 +"',@strLevel3='"+strLevel3 +"',@strCompany='"+strCompany+"',@strStatus='"+strStatus +"',@Activity='loaddatats'");
				
			}
			catch(Exception ex) 
			{
					
			}
			return dtb;
		}
		public static DataTable AddNew(string FromDate,string ToDate,string LeaveType,string strLanguage,string strEmpID,string strEmpName,string strLevel1,string strLevel2,string strLevel3,string strCompany,string strStatus)
		{
			DataTable dtb = new DataTable();
			try
			{
				
				dtb = clsCommon.GetDataTable("SI_spfrmTimeSheetSI @FromDate='"+ FromDate +"',@ToDate='"+ ToDate +"',@LeaveType='"+ LeaveType + "',@Language='"+ strLanguage +"',@strEmpID='"+strEmpID +"',@strEmpName='"+strEmpName +"',@strLevel1='"+strLevel1 +"',@strLevel2='"+strLevel2 +"',@strLevel3='"+strLevel3 +"',@strCompany='"+strCompany+"',@strStatus='"+strStatus +"',@Activity='addnew'");
				  
				
			}
			catch(Exception ex) 
			{
					
			}
			return dtb;
		}
			
		
	}
	#endregion
	/*#region ClsSI_Report
	public class ClsSI_Report
	{
		public ClsSI_Report()
		{
		}
		#region Company Info
		public static DataTable GetData_ComInfo (string CompanyID, string strMonth, System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_sprptCommon @Activity = 'CompanyInfo', @Language='" + sLanguage +
					"',@LSCompanyID=N'" + CompanyID + 
					"',@MMYYYY=N'" + strMonth +
					"'");
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
		#endregion
		#region C66a
		public static DataTable GetData_C66a (string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strMonth, System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_sprptC66a @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@Month=N'" + strMonth +
					"'");
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
		#endregion
		#region C67a
		public static DataTable GetData_C67a (string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strMonth, System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_sprptC67a @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@Month=N'" + strMonth +
					"'");
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
		#endregion
		#region C68a
		public static DataTable GetData_C68a (string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strMonth,string strStage, System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_sprptC68a @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@Stage=" + strStage +
					",@Month=N'" + strMonth +
					"'");
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
		#endregion
		#region C69a
		public static DataTable GetData_C69a (string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strMonth, string strStage,System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_sprptC69a @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@Stage=" + strStage +
					",@Month=N'" + strMonth +
					"'");
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
		#endregion
		#region C70a
		public static DataTable GetData_C70a (string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strMonth, string strStage,System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_sprptC70a @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@Stage=" + strStage +
					",@Month=N'" + strMonth +
					"'");
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
		#endregion
		#region 02aTBH
		public static DataTable GetData_02aTBH (string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strMonth, System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_sprpt02aTBH @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@Month=N'" + strMonth +
					"'");
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
		#endregion
		#region 03TBH
		#region Info Footer
		public static DataTable GetData_InfoFooter03TBH (string CompanyID, string strMonth, System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_sprptCommon @Activity = 'InfoFooter03TBH', @Language='" + sLanguage +
					"',@LSCompanyID=N'" + CompanyID + 
					"',@MMYYYY=N'" + strMonth +
					"'");
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
		#endregion
		public static DataTable GetData_03TBH (string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strMonth,string strStage, System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_sprpt03TBH @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@Stage= " + strStage +
					",@Month=N'" + strMonth +
					"'");
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
		#endregion
		#region 04TBH
		public static DataTable GetData_04TBH (string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strMonth,string strStage, System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_sprpt04TBH @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@Stage= " + strStage +
					",@Month=N'" + strMonth +
					"'");
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
		#endregion
	}
	#endregion
*/

	#region ClsSI_Report
	public class ClsSI_Report
	{
		public ClsSI_Report()
		{
		}

		public static DataTable GetMaxStage(string strMonth)
		{
			DataTable dtb = clsCommon.GetDataTable("SI_sprptCommon 'GetMaxStage', @MMYYYY = '" + strMonth + "'");
			return dtb;
		}
		#region Company Info
		public static DataTable GetData_ComInfo (string CompanyID, string strMonth,string strStage, System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_sprptCommon @Activity = 'CompanyInfo', @Language='" + sLanguage +
					"',@LSCompanyID=N'" + CompanyID + 
					"',@MMYYYY=N'" + strMonth +
					"',@Stage=N'" + strStage +
					"'");
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

		public static DataTable GetData_ComInfo_02aTBH (string CompanyID, string strMonth,string strStage, System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_sprptCommon @Activity = 'CompanyInfo_02aTBH', @Language='" + sLanguage +
					"',@LSCompanyID=N'" + CompanyID + 
					"',@MMYYYY=N'" + strMonth +
					"',@Stage=N'" + strStage +
					"'");
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
		public static DataTable GetData_ComInfo(string CompanyID, int strYear, System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_sprptCommon @Activity = 'CompanyInfo', @Language='" + sLanguage +
					"',@LSCompanyID=N'" + CompanyID + 
					"',@YEAR=N'" + strYear +
					"'");
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

		#region Company Info
		public static DataTable GetData_ComInfo (string CompanyID, string strMonth, System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_sprptCommon @Activity = 'CompanyInfo', @Language='" + sLanguage +
					"',@LSCompanyID=N'" + CompanyID + 
					"',@MMYYYY=N'" + strMonth +
					"'");
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
		#endregion
		public static DataTable GetData_ComInfo (string CompanyID, string strMonth,int Stage, System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_sprptCommon @Activity = 'CompanyInfo', @Language='" + sLanguage +
					"',@LSCompanyID=N'" + CompanyID + 
					"',@MMYYYY=N'" + strMonth +
					"',@Stage='" + Stage +
					"'");
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
		public static DataTable GetData_ComInfo (string CompanyID, string strMonth, string strFromDate,string strToDate,  System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_sprptCommon @Activity = 'CompanyInfo', @Language='" + sLanguage +
					"',@LSCompanyID=N'" + CompanyID + 
					"',@MMYYYY=N'" + strMonth +
					"',@FromDate=N'" + strFromDate +
					"',@ToDate=N'" + strToDate +
					"'");
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
		#endregion
		#region C66a
		public static DataTable GetData_C66a (string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strMonth, System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_sprptC66a @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@Month=N'" + strMonth +
					"'");
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
		#endregion
		#region C67a
		public static DataTable GetData_C67a (string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strMonth, System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_sprptC67a @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@Month=N'" + strMonth +
					"'");
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
		#endregion
		#region C68a
		public static DataTable GetData_C68a (string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strMonth,string strStage, System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_sprptC68a @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@Stage=" + strStage +
					",@Month=N'" + strMonth +
					"'");
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
		#endregion
		#region C69a
		public static DataTable GetData_C69a (string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strMonth, string strStage,System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_sprptC69a @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@Stage=" + strStage +
					",@Month=N'" + strMonth +
					"'");
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
		#endregion
		#region C70a
		public static DataTable GetData_C70a (string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strMonth, string strStage,System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_sprptC70a @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@Stage=" + strStage +
					",@Month=N'" + strMonth +
					"'");
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
		#endregion
		#region C45
		public static DataTable GetData_C45 (string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strMonth, System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_sprptC45 @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@ToDate=N'" + strMonth +
					"'");
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
		#endregion
		#region 02SBH
		public static DataTable GetData_02SBH (string strYear, string strQuarter,string strMMYYYY, string CompanyID)
		{
			//string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			//string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_sprpt02_SBH @CompanyID=N'" + CompanyID + 
					"',@Year=N'" + strYear +
					"',@Quarter=N'" + strQuarter +
					"',@MMYYYY=N'" + strMMYYYY +
					"'");
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
		#endregion

		#region C47b
		public static DataTable GetData_C47b (string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strFromMonth,string strToMonth, System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_sprptC47b @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@FromMonth=N'" + strFromMonth +
					"',@ToMonth=N'" + strToMonth +
					"'");
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
		#endregion
		#region C04BH_75
		public static DataTable GetData_C04BH_75 (string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strMonth, System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_sprptC04BH_75 @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@Month=N'" + strMonth +
					"'");
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
		#endregion
		#region C04BH_100
		public static DataTable GetData_C04BH_100 (string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strMonth, System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			string strXGroup="MTL";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_sprptC04BH_100 @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@Month=N'" + strMonth +
					"'");
					
				/*		dt = clsCommon.GetDataTable("SI_sprptC04BH_100_bk @Language='" + sLanguage +
						"',@LSCompanyID=N'" + CompanyID + 
						"',@Level1ID=N'" + Level1ID +
						"',@Level2ID=N'" + Level2ID + 
						"',@Level3ID=N'" + Level3ID + 
						"',@EmpID = N'" + EmpID + 
						"',@EmpName=N'" + EmpName + 
						"',@Status=N'" + Status +
						"',@LocationID=N'" + LocationID +
						"',@PositionID=N'" + PositionID +
						"',@JobCodeID=N'" + JobCodeID +
						"',@Month=N'" + strMonth +
						"',@XGroup=N'" + strXGroup +
						"'");
				*/

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
		#endregion
		#region C46
		public static DataTable GetData_C46 (string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strFromMonth,string strToMonth,string  strTransferExcess,string strTransferLack,
			string strFund, string strDelayFine, string strApplyAmount, System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_sprptC46 @Language='" + sLanguage +
					"',@LSCompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@FromMonth=N'" + strFromMonth +
					"',@ToMonth=N'" + strToMonth +
					"',@TransferExcess='" + strTransferExcess +
					"',@TransferLack='" + strTransferLack +
					"',@Fund='" + strFund +
					"',@DelayFine='" + strDelayFine +
					"',@ApplyAmount='" + strApplyAmount +
					"'");
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
		#endregion
				

		#region 02aTBH
		public static DataTable GetData_02aTBH (string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strMonth, System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_sprpt02aTBH @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@Month=N'" + strMonth +
					"'");
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
		public static DataTable GetData_02a_TBH (string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strMonth,string strStage,System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_sprpt02aTBH_New @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@Stage= " + strStage +
					",@Month=N'" + strMonth +
					"'");
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
		#endregion

		#region 03TBH
		#region Info Footer
		public static DataTable GetData_InfoFooter03TBH (string CompanyID, string strMonth, System.Web.UI.Page pPage, string strStage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_sprptCommon @Activity = 'InfoFooter03TBH', @Language='" + sLanguage +
					"',@LSCompanyID=N'" + CompanyID + 
					"',@MMYYYY=N'" + strMonth +
					"',@Stage=N'" + strStage +
					"'");
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
		#endregion
		public static DataTable GetData_03TBH (string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strMonth,string strStage, System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_sprpt03TBH_New @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +
					"',@UserGroupID='" + sAccountLogin +
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +

					"',@Stage= " + strStage +
					",@Month=N'" + strMonth +
					"'");
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
		#endregion
		#region 04TBH
		public static DataTable GetData_04TBH (string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strMonth,string strStage, System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_sprpt04TBH @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@Stage= " + strStage +
					",@Month=N'" + strMonth +
					"'");
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
		#endregion
		#region C47a
		public static DataTable GetData_C47a (string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string Month,int Year,int Stage,string strFromDate, string strToDate, System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_sprpt04TBH @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@Stage= '" + Stage +
					"',@Month='" + Month +
					//"',@YYYY='" + Year +
					//"',@FromDate='" + strFromDate +
					//"',@ToDate='" + strToDate +
					"'");
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
		#endregion
		#region C47
		#region Info Footer
		public static DataTable GetData_InfoFooterC47 (string CompanyID, string strMonth, System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_sprptCommon @Activity = 'InfoFooterC47', @Language='" + sLanguage +
					"',@LSCompanyID=N'" + CompanyID + 
					"',@MMYYYY=N'" + strMonth +
					"'");
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
		public static DataTable GetData_InfoFooterC47 (string CompanyID, string strMonth,int Stage, System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_sprptCommon @Activity = 'InfoFooterC47', @Language='" + sLanguage +
					"',@LSCompanyID=N'" + CompanyID + 
					"',@MMYYYY=N'" + strMonth +
					"',@Stage= '" + Stage +
					"'");
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
		#endregion
		public static DataTable GetData_C47 (string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string LocationID, string PositionID, string JobCodeID,
			string strDate,int strStage, System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string sLanguage= pPage.Session["LangID"]!=null?pPage.Session["LangID"].ToString():"VN";
			//---------------------------------------------
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("SI_sprpt03TBH @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@Stage= " + strStage +
					",@Month=N'" + strDate +
					"'");
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
		#endregion

	}
	#endregion
	}