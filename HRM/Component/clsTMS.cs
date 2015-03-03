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
using iHRPCore.SendMail;

namespace iHRPCore.TMSComponent
{
	/// <summary>
	/// Summary description for clsTMS.
	/// </summary>
	#region TMSCommon
	public class clsTMSMachine
	{
		public static bool IsNumeric(object pValue)
		{
			try
			{
				return Convert.ToBoolean(Convert.ToInt32(pValue));
			}
			catch
			{
				return false;
			}
		}
		public static int ConvertInteger(object pValue)
		{
			try
			{
				return Convert.ToInt32(pValue);
			}
			catch
			{
				return 0;
			}
		}

		public static double ConvertDouble(object pValue)
		{
			try
			{
				return Convert.ToDouble(pValue);
			}
			catch
			{
				return 0;
			}
		}

		//KhanhMT - 17/08/2004 - Formmat so ra dang co phan cach hang ngan
		public static string FormatNumericWithSeparate(double field)
		{
			int DecimalNumber = 2;
			string DecimalChar = ".";
			string SeperateChar = ",";
			string ValueString = field.ToString();
			string ValueStringAfterDecimal = "";
			string ValueStringNew = "";

			if (field < 0) 
				ValueString = ValueString.Substring(2, ValueString.Length - 1); //Bo dau tru

			if (ValueString.IndexOf(DecimalChar) != -1)
			{
				ValueStringAfterDecimal = ValueString.Substring(ValueString.IndexOf(DecimalChar), DecimalNumber + 1); // Tinh luon dau cham
				ValueString = ValueString.Substring(0, ValueString.IndexOf(DecimalChar));
			}

			ValueString = ValueString.Replace(",", "");

			ValueStringNew = "";

			while (ValueString.Length >3)
			{
				ValueStringNew = SeperateChar + ValueString.Substring(ValueString.Length - 3, 3) + ValueStringNew;
				ValueString = ValueString.Substring(0, ValueString.Length - 3);
			}

			if (DecimalNumber != 0)
				ValueStringNew = ValueString + ValueStringNew + ValueStringAfterDecimal;
			else
				ValueStringNew = ValueString + ValueStringNew;

			if (field < 0) 
				ValueStringNew = "-" + ValueStringNew;
	
			return ValueStringNew;
		}

		public static void LoadDropDownList(DropDownList pControl, string pstrSQL, string pstrValueField, string pstrTextField)
		{
			try
			{
				DataTable rsData = new DataTable();
				rsData = clsCommon.GetDataTable(pstrSQL);

				pControl.Items.Clear();
				pControl.DataSource = rsData;

				pControl.DataValueField = pstrValueField;
				pControl.DataTextField = pstrTextField;
				pControl.DataBind();

				rsData.Dispose();
			}
			catch(Exception exp) 
			{
				string strError = exp.Message.ToString();
			}
		}

		/// <summary>
		/// Show message ngay tren server-side
		/// </summary>
		/// <param name="pPage">Trang goi ham</param>
		/// <param name="pMessage">Noi dung message</param>
		public static void ShowMessageBox(System.Web.UI.Page pPage,string pMessage)
		{				
			string strScript = "<script language=JavaScript>";
			strScript += "alert('" + pMessage + "');";				
			strScript += "</script>";
			if (!pPage.IsStartupScriptRegistered("clientScript"))
				pPage.RegisterStartupScript("clientScript", strScript);				
		}	

		/// <summary>
		/// Lanhtd, lay ma nhan vien cua account tuong ung
		/// </summary>
		/// <param name="strUserAccount"></param>
		/// <returns></returns>
		public static string GetEmpIDByAccountLogin(string strUserAccount)
		{
			DataTable tbl = clsCommon.GetDataTable("UMS_sptblUserAccount 'GetEmpIDFromAccount',@result='',@UserGroupID='" + strUserAccount + "'");
			if (tbl.Rows.Count>0)
				return tbl.Rows[0][0].ToString().Trim();
			else return "";
		}
	}
	
	#endregion

	#region LeaveRecord
	public class clsTMSLeaveRecord
	{
		public static DataTable LoadLeaveTypeList(string strLanguage, string strEmpID, string strYear)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb = clsCommon.GetDataTable("TS_spfrmLeaveRecord @Activity='LoadLeaveTypeList', @Language='" 
					+ strLanguage + "', @EmpID='" + strEmpID + "', @Year='" + strYear + "'");
			}
			catch
			{
				dtb = new DataTable();
			}
			return dtb;
		}

		public static DataTable LoadLeaveRecordByEmp(string strLanguage, string strEmpID, string strYear, string strCurUser)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb = clsCommon.GetDataTable("TS_spfrmLeaveRecord @Activity='GetAllData', @Language='" 
					+ strLanguage + "', @EmpID='" + strEmpID + "', @Year='" + strYear + "', @Creater='" + strCurUser + "'");
			}
			catch
			{
				dtb = new DataTable();
			}
			return dtb;
		}

		public static DataTable LoadLeaveRecordByID(string strLanguage, string strEmpID, string strLeaveRecordID, string strRequestID)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb = clsCommon.GetDataTable("TS_spfrmLeaveRecord @Activity='FillEmpLRInfo', @Language='" 
					+ strLanguage + "', @EmpID='" + strEmpID + "', @RequestID='" + strRequestID + "'");
			}
			catch
			{
				dtb = new DataTable();
			}
			return dtb;
		}

		public static DataTable LoadLeaveDetails(string strLanguage, string strEmpID, string strFromDate, string strToDate, string strWorkPointID, string strLeaveRecordID, string strCurUser,string strYear)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb = clsCommon.GetDataTable("TS_spfrmLeaveRecord @Activity='LoadLeaveDetails', @Language='" 
					+ strLanguage + "', @EmpID=N'" + strEmpID + "', @FromDate='" + strFromDate + "', @ToDate='" + strToDate 
					+ "', @LSWorkPointID=N'" + strWorkPointID + "', @LeaveRecordID=N'" + strLeaveRecordID 
					+ "', @Creater='" + strCurUser + "',@Year='" + strYear + "'");
			}
			catch
			{
				dtb = new DataTable();
			}
			return dtb;
		}

		public static string DeleteLRDetailsTemp(string strCurUser)
		{
			string strErr = "";
			try
			{
				clsCommon.GetDataTable("TS_spfrmLeaveRecord @Activity='DeleteLRDTemp', @Creater='" + strCurUser + "'");
			}
			catch (Exception exp)
			{
				strErr = exp.Message.Trim();
			}
			return strErr;
		}

		/// <summary>
		/// Creater: LanHTD, 24/02/2006
		/// Description: Luu du lieu nghi chi tiet xuong bang tam
		/// </summary>
		public static string SaveLeaveDetailsTemp(DataTable dtb, string strEmpID, string strLSWorkPointID, 
			string strFromDate, string strToDate, string strCurUser)
		{
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "TS_spfrmLeaveRecord";
   
				for (int i = 0;i< dtb.Rows.Count;i++)
				{
					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "SaveLDetailsTemp";						
					cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = strEmpID;
					cmd.Parameters.Add("@LSWorkPointID", SqlDbType.NVarChar, 12).Value = strLSWorkPointID;
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

		public static string SaveLeaveRecord(string strEmpID, string strCurUser, DataGrid dtgList)
		{
			string strErr = "";
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);		
			SQLconn.Open();
			cmd.Connection = SQLconn;
			cmd.Transaction= SQLconn.BeginTransaction();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "TS_spfrmLeaveRecord";
			CheckBox chkSelect = new CheckBox();
			string strFromDate, strToDate;
			try
			{
				for(int i=0;i<dtgList.Items.Count;i++)
				{
					strFromDate =((TextBox)dtgList.Items[i].FindControl("txtFromDate")).Text.Trim();
					strToDate =((TextBox)dtgList.Items[i].FindControl("txtToDate")).Text.Trim();
					
					chkSelect = (CheckBox)dtgList.Items[i].FindControl("chkLeaveType");
					// CHECK IF SELECTED
					if(chkSelect.Checked==true)
					{
						if (strFromDate != "" && strToDate != "")
						{
							cmd.Parameters.Clear();
							cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = ((HtmlInputHidden)dtgList.Items[i].FindControl("txtLeaveRecordID")).Value.Trim() == ""?"Save":"Update";
							cmd.Parameters.Add("@EmpID",SqlDbType.NVarChar,12).Value = strEmpID.Trim();
							cmd.Parameters.Add("@LeaveRecordID",SqlDbType.NVarChar,12).Value = ((HtmlInputHidden)dtgList.Items[i].FindControl("txtLeaveRecordID")).Value.Trim();
							//cmd.Parameters.Add("@RequestID",SqlDbType.NVarChar,12).Value = ((HtmlInputHidden)dtgList.Items[i].FindControl("txtRequestID")).Value.Trim();
							cmd.Parameters.Add("@FromDate",SqlDbType.NVarChar,12).Value = strFromDate.Trim();
							cmd.Parameters.Add("@ToDate",SqlDbType.NVarChar,12).Value = strToDate.Trim();
							cmd.Parameters.Add("@LSWorkPointID",SqlDbType.NVarChar,12).Value = ((HtmlInputHidden)dtgList.Items[i].FindControl("txtWorkPointID")).Value.Trim();
							cmd.Parameters.Add("@Total",SqlDbType.Real).Value = ((TextBox)dtgList.Items[i].FindControl("txtLeaveTaken")).Text.Trim();
							/*
							if (((TextBox)dtgList.Items[i].FindControl("txtEarlyBackDate")).Text.Trim() != "")
								cmd.Parameters.Add("@EarlyBackDate",SqlDbType.NVarChar,12).Value = ((TextBox)dtgList.Items[i].FindControl("txtEarlyBackDate")).Text.Trim();
							else
								cmd.Parameters.Add("@EarlyBackDate",SqlDbType.NVarChar,12).Value = DBNull.Value;
								*/
							cmd.Parameters.Add("@Creater",SqlDbType.NVarChar,20).Value = strCurUser;
							cmd.ExecuteNonQuery();
						}
					}
				}
				cmd.Transaction.Commit();
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
		public static string SaveLeaveRecord(string strCurUser, DataGrid dtgList)
		{
			string strErr = "";
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);		
			SQLconn.Open();
			cmd.Connection = SQLconn;
			cmd.Transaction= SQLconn.BeginTransaction();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "TS_spfrmLeaveRecord";
			CheckBox chkSelect = new CheckBox();
			string strFromDate, strToDate,strEmpID, strWorkPointID;
			try
			{
				for(int i=0;i<dtgList.Items.Count;i++)
				{
					chkSelect = (CheckBox)dtgList.Items[i].FindControl("chkSelect");			
					// CHECK IF SELECTED
					if(chkSelect.Checked==true)
					{
						strFromDate =((TextBox)dtgList.Items[i].FindControl("txtFromDate")).Text.Trim();
						strToDate =((TextBox)dtgList.Items[i].FindControl("txtToDate")).Text.Trim();
						strEmpID=((TextBox)dtgList.Items[i].FindControl("txtEmpIDList")).Text.Trim();
						strWorkPointID=((DropDownList)dtgList.Items[i].FindControl("cboWorkPointID")).SelectedValue;
						strWorkPointID=strWorkPointID.Substring(0,strWorkPointID.LastIndexOf("@@"));
						
						if (strFromDate != "" && strToDate != "")
						{
							cmd.Parameters.Clear();
							cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "SaveInputData";
							cmd.Parameters.Add("@EmpID",SqlDbType.NVarChar,12).Value = strEmpID.Trim();														
							cmd.Parameters.Add("@FromDate",SqlDbType.NVarChar,12).Value = strFromDate.Trim();
							cmd.Parameters.Add("@ToDate",SqlDbType.NVarChar,12).Value = strToDate.Trim();
							cmd.Parameters.Add("@LSWorkPointID",SqlDbType.NVarChar,12).Value = strWorkPointID;
							cmd.Parameters.Add("@Total",SqlDbType.Real).Value = ((TextBox)dtgList.Items[i].FindControl("txtLeaveTaken")).Text.Trim();
							if (((TextBox)dtgList.Items[i].FindControl("txtEarlyBackDate")).Text.Trim() != "")
								cmd.Parameters.Add("@EarlyBackDate",SqlDbType.NVarChar,12).Value = ((TextBox)dtgList.Items[i].FindControl("txtEarlyBackDate")).Text.Trim();
							else
								cmd.Parameters.Add("@EarlyBackDate",SqlDbType.NVarChar,12).Value = DBNull.Value;
							cmd.Parameters.Add("@Creater",SqlDbType.NVarChar,20).Value = strCurUser;
							cmd.ExecuteNonQuery();
						}
					}
				}
				cmd.Transaction.Commit();
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
		/// <summary>
		/// Xoa du lieu leave record
		/// </summary>
		public static string DeleteLeaveRecord(DataGrid dtgLeaveRecord, string strCurUser)
		{
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "TS_spfrmLeaveRecord";
   
				CheckBox obj = new CheckBox();
				for (int i = 0;i<= dtgLeaveRecord.Items.Count - 1;i++)
				{
					obj = (CheckBox) dtgLeaveRecord.Items[i].FindControl("chkSelect");
					if (obj.Checked)
					{
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "Delete";						
						cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = dtgLeaveRecord.Items[i].Cells[0].Text.Trim();
						cmd.Parameters.Add("@LeaveRecordID", SqlDbType.NVarChar, 12).Value = dtgLeaveRecord.Items[i].Cells[1].Text.Trim();
						cmd.Parameters.Add("@RequestID", SqlDbType.NVarChar, 12).Value = dtgLeaveRecord.Items[i].Cells[2].Text.Trim();
						cmd.Parameters.Add("@Creater", SqlDbType.NVarChar, 20).Value = strCurUser;
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
			catch  (Exception exp)
			{								
				cmd.Transaction.Rollback();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				return exp.Message.Trim();
			}
		}
	}
	#endregion	
		#region LeaveRecordList
	public class clsTMSLeaveRecordList
	{
		public static DataTable LoadLeaveRecordList(string strLanguage, string strEmpID, string strFromDate, string strToDate,
			string strLSWorkPointID, string strCompany, string strLevel1, string strLevel2, string strLevel3, string strEmpName, string strStatus,System.Web.UI.Page pPage, string sLSEmpTypeID)
		{
			//cangtt - 01052006 - cập nhật phần phân quyền dữ liệu
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			//--------------------------------------------------
			DataTable dtb = new DataTable();
			try
			{
				dtb = clsCommon.GetDataTable("TS_spfrmLeaveRecord @Activity='GetLeaveRecordList', @Language='" 
					+ strLanguage + "', @EmpID=N'" + strEmpID + "', @EmpName=N'" + strEmpName
					+ "', @LSLevel1ID=N'" + strLevel1 + "', @LSLevel2ID=N'" + strLevel2
					+ "', @LSLevel3ID=N'" + strLevel3 + "', @LSCompanyID=N'" + strCompany 
					+ "',@LSWorkPointID=N'" + strLSWorkPointID + "', @FromDate='" + strFromDate + "', @ToDate='" + strToDate + "',@UserGroupID='" + sAccountLogin + "',@Status='" + strStatus + "',@LSEmpTypeID='" + sLSEmpTypeID + "'");
			}
			catch
			{
				dtb = new DataTable();
			}
			return dtb;
		}

		public static void LoadComboWorkPoint(DropDownList pControl, string strLanguage, int isSpecial)
		{
			clsCommon.LoadDropDownListControl(pControl,"TS_spfrmLeaveRecord @Activity='GetWorkPointList', @Language='" + strLanguage + "',@ByGroup=" + isSpecial + "", "LSWorkPointID","WorkPointName",true);
			
		}

		public static string SaveLRecordGroup(string strEmpList,string strLSLeaveTypeCode, string strFromDate, string strToDate, Double dblLeaveTaken, string strNote)
		{
			string strReturn = "";
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "TS_spfrmLeaveRecord";
				string[] arrTemp = strEmpList.Split(new char[]{','});
				for (int i = 0;i< arrTemp.Length;i++)
				{
					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "SaveList";						
					cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 1200).Value = arrTemp[i].Trim();
					cmd.Parameters.Add("@LSWorkPointID", SqlDbType.NVarChar, 12).Value = strLSLeaveTypeCode;
					cmd.Parameters.Add("@FromDate", SqlDbType.NVarChar, 12).Value = strFromDate;
					cmd.Parameters.Add("@ToDate", SqlDbType.NVarChar, 12).Value = strToDate;
					cmd.Parameters.Add("@LeaveTaken", SqlDbType.Decimal).Value = dblLeaveTaken;
					if (strNote != "")
						cmd.Parameters.Add("@Note", SqlDbType.NVarChar,200).Value = strNote;
					else
						cmd.Parameters.Add("@Note", SqlDbType.NVarChar,200).Value = DBNull.Value;
					cmd.Parameters.Add("@Return", SqlDbType.NVarChar,1000).Direction = ParameterDirection.InputOutput;
					cmd.ExecuteNonQuery();	
					if (strReturn != "")
						strReturn = strReturn + "," +  cmd.Parameters["@Return"].Value.ToString();	
					else
						strReturn = cmd.Parameters["@Return"].Value.ToString();	
				}
				cmd.Transaction.Commit();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();	
				return strReturn;
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

		public static string SaveLRecordGroup(string strLSLeaveTypeCode, string strFromDate, string strToDate, Double dblLeaveTaken, string strNote,
			string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strPosition, string strJobCode, string strLocation, string strCompany, string strStatus)
		{
			string strReturn = "";
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "TS_spfrmLeaveRecord";
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "SaveListByCondition";						
				cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = strEmpID;
				cmd.Parameters.Add("@EmpName", SqlDbType.NVarChar, 50).Value = strEmpName;
				cmd.Parameters.Add("@LSCompanyID", SqlDbType.NVarChar, 12).Value = strCompany;
				cmd.Parameters.Add("@LSLevel1ID", SqlDbType.NVarChar, 12).Value = strLevel1;
				cmd.Parameters.Add("@LSLevel2ID", SqlDbType.NVarChar, 12).Value = strLevel2;
				cmd.Parameters.Add("@LSLevel3ID", SqlDbType.NVarChar, 12).Value = strLevel3;
				cmd.Parameters.Add("@LSPositionID", SqlDbType.NVarChar, 12).Value = strPosition;
				cmd.Parameters.Add("@LSLocationID", SqlDbType.NVarChar, 12).Value = strLocation;
				cmd.Parameters.Add("@Status", SqlDbType.TinyInt).Value = strStatus;

				cmd.Parameters.Add("@LSWorkPointID", SqlDbType.NVarChar, 12).Value = strLSLeaveTypeCode;
				cmd.Parameters.Add("@FromDate", SqlDbType.NVarChar, 12).Value = strFromDate;
				cmd.Parameters.Add("@ToDate", SqlDbType.NVarChar, 12).Value = strToDate;
				cmd.Parameters.Add("@Total", SqlDbType.Decimal).Value = dblLeaveTaken;
				if (strNote != "")
					cmd.Parameters.Add("@Note", SqlDbType.NVarChar,200).Value = strNote;
				else
					cmd.Parameters.Add("@Note", SqlDbType.NVarChar,200).Value = DBNull.Value;
				cmd.Parameters.Add("@Return", SqlDbType.NVarChar,1000).Direction = ParameterDirection.InputOutput;
				cmd.ExecuteNonQuery();	
				if (strReturn != "")
					strReturn = strReturn + "," +  cmd.Parameters["@Return"].Value.ToString();	
				else
					strReturn = cmd.Parameters["@Return"].Value.ToString();	
				cmd.Transaction.Commit();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();	
				return strReturn;
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

	}
	#endregion

	#region WorkSchedule
	public class clsTMSWorkSchedule
	{
		public static void LoadComboWeek(DropDownList pControl, string strLanguage)
		{
			DataTable dtb = clsCommon.GetDataTable("TS_spfrmSchedule @Activity='GetSelectedValue', @Language='" + strLanguage + "'");
			clsCommon.LoadDropDownListControl(pControl, "TS_spfrmSchedule @Activity='GetListOfWeek', @Language='" + strLanguage + "'", "Week","Week",true);
			pControl.SelectedValue = dtb.Rows[0]["Week"].ToString().Trim();
		}

		public static void LoadComboShift(DropDownList pControl, string strLanguage)
		{
			clsCommon.LoadDropDownListControl(pControl,"TS_spfrmSchedule @Activity='GetListShift', @Language='" + strLanguage + "'", "LSShiftID","ShiftName",true);
		}
		public static void LoadComboShiftCode(DropDownList pControl, string strLanguage)
		{
			clsCommon.LoadDropDownListControl(pControl,"TS_spfrmSchedule @Activity='GetListShiftCode', @Language='" + strLanguage + "'", "LSShiftID","ShiftName",true);
		}

		/// <summary>
		/// Lay thong tin tu lich lam viec
		/// </summary>
		/// <returns>DataTable</returns>
		public static DataTable GetWorkScheduleList(string strScheduleCode,string strWeek,string IsDefault)
		{
			IsDefault = IsDefault == "True"?"1":"0";
			DataTable dtb = new DataTable();
			try
			{
				dtb = clsCommon.GetDataTableHasID("TS_spfrmSchedule @Activity='Search',@ScheduleCode=N'" + strScheduleCode +
					"',@Week='" + strWeek +"',@IsDefault = "+IsDefault+"");
				return dtb;
			}
			catch(Exception exp)
			{
				string str = exp.Message;
				return null;
			}
			finally
			{
				dtb.Dispose();
			}
		}

		public static string SaveGrid (DataGrid dtgList)
		{
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);		
			SQLconn.Open();
			cmd.Connection = SQLconn;
			cmd.Transaction= SQLconn.BeginTransaction();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "TS_spfrmSchedule";
			string strError = "";
			try
			{
				for(int i=0;i<dtgList.Items.Count;i++)
				{

					string strScheduleID = dtgList.Items[i].Cells[0].Text.Trim();

					if(((CheckBox)dtgList.Items[i].FindControl("chkSelect")).Checked==true)
					{
						for(int j=1;j<6;j++)
						{
							string strTextBox = "txt" + j.ToString();
							string strTextBoxhd = "txt" + j.ToString() + "_" + j.ToString();
							string strText = ((TextBox)dtgList.Items[i].FindControl(strTextBox)).Text;
							string strTexthd = ((HtmlInputHidden)dtgList.Items[i].FindControl(strTextBoxhd)).Value;
							
							string[] marrstrText = strText.Split(',');
							if (marrstrText.Length > 1)
							{
								string strCoefShift1 = "0.5";
								string strLSShiftID1 = marrstrText.GetValue(1).ToString().Trim();
								string strCoefShift2 = "";
								string strLSShiftID2 = "";

								cmd.Parameters.Clear();
					
								cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Save";
								cmd.Parameters.Add("@ScheduleID",SqlDbType.NVarChar,15).Value = strScheduleID;
								cmd.Parameters.Add("@DateID",SqlDbType.NVarChar,12).Value = strTexthd;
								cmd.Parameters.Add("@CoefShift",SqlDbType.Real).Value = strCoefShift1;
								cmd.Parameters.Add("@LSShiftID",SqlDbType.NVarChar,15).Value = strLSShiftID1;
								cmd.ExecuteNonQuery();

								if (marrstrText.Length > 2 && marrstrText.Length < 4)
								{
									strCoefShift2 = "1";
									strLSShiftID2 = marrstrText.GetValue(2).ToString().Trim();
									
									cmd.Parameters.Clear();
				
									cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Save";
									cmd.Parameters.Add("@ScheduleID",SqlDbType.NVarChar,15).Value = strScheduleID;
									cmd.Parameters.Add("@DateID",SqlDbType.NVarChar,12).Value = strTexthd;
									cmd.Parameters.Add("@CoefShift",SqlDbType.Real).Value = strCoefShift2;
									cmd.Parameters.Add("@LSShiftID",SqlDbType.NVarChar,15).Value = strLSShiftID2;
									cmd.ExecuteNonQuery();

								}
								else if (marrstrText.Length > 3 && marrstrText.Length < 5)
								{
									strCoefShift2 = marrstrText.GetValue(2).ToString().Trim();;
									strLSShiftID2 = marrstrText.GetValue(3).ToString().Trim();
									if (strCoefShift2 != "1/2" ||strCoefShift2 != "0.5" ||strCoefShift2 != ".5")
									{
										strCoefShift2 = "0.5";
										cmd.Parameters.Clear();
					
										cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Save";
										cmd.Parameters.Add("@ScheduleID",SqlDbType.NVarChar,15).Value = strScheduleID;
										cmd.Parameters.Add("@DateID",SqlDbType.NVarChar,12).Value = strTexthd;
										cmd.Parameters.Add("@CoefShift",SqlDbType.Real).Value = strCoefShift2;
										cmd.Parameters.Add("@LSShiftID",SqlDbType.NVarChar,15).Value = strLSShiftID2;
										cmd.ExecuteNonQuery();
									}
								}
							}
							else
							{
								if ( strText != "" )
								{
									cmd.Parameters.Clear();
					
									cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Save";
									cmd.Parameters.Add("@ScheduleID",SqlDbType.NVarChar,15).Value = strScheduleID;
									cmd.Parameters.Add("@DateID",SqlDbType.NVarChar,12).Value = strTexthd;
									cmd.Parameters.Add("@CoefShift",SqlDbType.Real).Value = 1;
									cmd.Parameters.Add("@LSShiftID",SqlDbType.NVarChar,15).Value = strText;
									cmd.ExecuteNonQuery();
								}
							}
						}
					}
				}
				cmd.Transaction.Commit();
				strError = "";
				return strError;
			}
			catch (Exception exp)
			{
				strError = exp.Message.Trim();
				cmd.Transaction.Rollback();		
				return strError;
			}
			finally
			{
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
			}
		}
	}
	#endregion
	#region clsPublicHoliday
	public class clsPublicHoliday
	{
		public static void GetSatSun (string strYear)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb = clsCommon.GetDataTable("TS_spfrmWorkDay @Action='GetSatSun', @Year='" + strYear + "'");
			}
			catch
			{
				dtb = new DataTable();
			}
			dtb.Dispose();
		}
	}
	#endregion
	#region OT
	public class clsTMSOTime
	{
		public static DataTable LoadOTData(string sDate, string sEmpCode, string sEmpName, string sLSCompanyID,
			string sLSLevel1ID, string sLSLevel2ID, string sLSLevel3ID, string sLSPositionID, string sLSLocationID,string strStatus,string strLanguage, string strLSEmpTypeID,System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			DataTable dtData= new DataTable();
			try
			{
				string sSQL="[TS_spfrmOverTime] @Activity='GetList', @EmpCode=N'" + sEmpCode + "',@EmpName=N'" + sEmpName + "', @LSCompanyID=N'" + sLSCompanyID 
					+ "',@LSLevel1ID=N'" + sLSLevel1ID + "',@LSLevel2ID=N'" + sLSLevel2ID + "',@LSLevel3ID=N'" + sLSLevel3ID 
					+ "',@LSLocationID=N'" + sLSLocationID + "', @LSPositionID=N'" + sLSPositionID 
					+ "',@UserGroupID=N'" + sAccountLogin + "',@LanguageID='" + strLanguage 
					+ "',@LSEmpTypeID='" +strLSEmpTypeID + "',@DateID='" +sDate+"'";
				if (strStatus != "")
					sSQL = sSQL + ", @Status='" + strStatus + "'";
				dtData= clsCommon.GetDataTable(sSQL);				
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
											  
	}
	#endregion
	#region Monthly Timesheet
	public class clsMonthlyTimesheet
	{
		public static DataTable getCounTimeList(string sMonthYear, string sTMS, string sEmpCode, string sEmpName, string sLSCompanyID,
			string sLSLevel1ID, string sLSLevel2ID, string sLSLevel3ID, string sLSPositionID, string sLSLocationID,string strStatus,string strLanguage, string strLSEmpTypeID,System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			DataTable dtData= new DataTable();
			try
			{
				string sSQL="TS_spfrmCountTime_V1 @Activity='GetList',@isTMS='" + sTMS + "', @EmpCode=N'" + sEmpCode + "',@EmpName=N'" + sEmpName + "',@Month='" + sMonthYear + "', @LSCompanyID=N'" + sLSCompanyID 
					+ "',@LSLevel1ID=N'" + sLSLevel1ID + "',@LSLevel2ID=N'" + sLSLevel2ID + "',@LSLevel3ID=N'" + sLSLevel3ID 
					+ "',@LSLocationID=N'" + sLSLocationID + "', @LSPositionID=N'" + sLSPositionID + "',@UserGroupID=N'" + sAccountLogin + "',@LanguageID='" + strLanguage + "',@LSEmpTypeID='" +strLSEmpTypeID+"'";
				if (strStatus != "")
					sSQL = sSQL + ", @Status='" + strStatus + "'";
				dtData= clsCommon.GetDataTable(sSQL);				
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

		public static string sImpact(DataGrid dtgList, String strMonthYear)
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
				cmd.CommandText = "TS_spfrmCountTime_V1";
   
				CheckBox obj = new CheckBox();
				for (int i = 0;i<= dtgList.Items.Count - 1;i++)
				{
					obj = (CheckBox) dtgList.Items[i].FindControl("chkSelect");					
					if (obj.Checked)
					{
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "Save";					
						cmd.Parameters.Add("@Month", SqlDbType.NVarChar, 30).Value = strMonthYear;					

						if (dtgList.Items[i].Cells[0].Text.Trim()=="")
							cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar).Value = DBNull.Value;
						else
							cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar,12).Value = dtgList.Items[i].Cells[0].Text.Trim(); 

						string sLeavePayment = ((TextBox)dtgList.Items[i].FindControl("txtLeavePayment")).Text; 
						string sLeaveNoPayment = ((TextBox)dtgList.Items[i].FindControl("txtLeaveNoPayment")).Text; 
						string sAdjustDate = ((TextBox)dtgList.Items[i].FindControl("txtAdjustDate")).Text; 
						string sNTTotal = ((TextBox)dtgList.Items[i].FindControl("txtNTTotal")).Text; 
						string sPayOT150 = ((TextBox)dtgList.Items[i].FindControl("txtOT150")).Text;
						string sPayOT200 = ((TextBox)dtgList.Items[i].FindControl("txtOT200")).Text;
						string sPayOT300 = ((TextBox)dtgList.Items[i].FindControl("txtOT300")).Text;
						string sPayOT30 = ((TextBox)dtgList.Items[i].FindControl("txtOT10")).Text;
						string sTotal = ((TextBox)dtgList.Items[i].FindControl("txtTotal")).Text;

						if (sLeavePayment == "") cmd.Parameters.Add("@LeavePayment", SqlDbType.Float).Value = DBNull.Value;
						else cmd.Parameters.Add("@LeavePayment", SqlDbType.Float).Value = sLeavePayment;

						if (sLeaveNoPayment == "") cmd.Parameters.Add("@LeaveNoPayment", SqlDbType.Float).Value = DBNull.Value;
						else cmd.Parameters.Add("@LeaveNoPayment", SqlDbType.Float).Value = sLeaveNoPayment;

						if (sAdjustDate == "") cmd.Parameters.Add("@AdjustDate", SqlDbType.Float).Value = DBNull.Value;
						else cmd.Parameters.Add("@AdjustDate", SqlDbType.Float).Value = sAdjustDate;

						if (sNTTotal == "") cmd.Parameters.Add("@NTTotal", SqlDbType.Float).Value = DBNull.Value;
						else cmd.Parameters.Add("@NTTotal", SqlDbType.Float).Value = sNTTotal;
						
						if (sPayOT150 == "") cmd.Parameters.Add("@PayOT150", SqlDbType.Float).Value = DBNull.Value;
						else cmd.Parameters.Add("@PayOT150", SqlDbType.Float).Value = sPayOT150; 

						if (sPayOT200 == "") cmd.Parameters.Add("@PayOT200", SqlDbType.Float).Value = DBNull.Value;
						else cmd.Parameters.Add("@PayOT200", SqlDbType.Float).Value = sPayOT200; 

						if (sPayOT300 == "") cmd.Parameters.Add("@PayOT300", SqlDbType.Float).Value = DBNull.Value;
						else cmd.Parameters.Add("@PayOT300", SqlDbType.Float).Value = sPayOT300;

						if (sPayOT30 == "") cmd.Parameters.Add("@PayOT30", SqlDbType.Float).Value = DBNull.Value;
						else cmd.Parameters.Add("@PayOT30", SqlDbType.Float).Value = sPayOT30;
						
						if (sTotal == "") cmd.Parameters.Add("@Total", SqlDbType.Float).Value = DBNull.Value;
						else cmd.Parameters.Add("@Total", SqlDbType.Float).Value = sTotal;

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
#endregion
	
}
