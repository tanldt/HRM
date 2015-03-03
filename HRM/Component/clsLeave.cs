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
using iHRPCore.LeaveComponent;
using iHRPCore.Com;
using System.Web.Mail;
using System.Net;
using System.IO;

namespace iHRPCore.LeaveComponent
{
	/// <summary>
	/// Summary description for clsLeave.
	/// </summary>
	#region "Leave application"
	public class clsTMSLeaveApplication
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

		public static DataTable LoadLeaveRecordByID(string strEmpID,  string strLeaveRecordID)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb = clsCommon.GetDataTable("TS_spfrmLeaveApplication @Activity='FillEmpLRInfo', @EmpID='" 
					+ strEmpID + "', @LeaveRecordID='" + strLeaveRecordID + "'");
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
				dtb = clsCommon.GetDataTable("TS_spfrmLeaveApplication @Activity='LoadLeaveDetails', @Language='" 
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

		public static DataTable LoadCalendar(string strLanguage, string strEmpID, string strFromDate, string strToDate, string strWorkPointID, string strLeaveRecordID, string strCurUser,string strYear)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb = clsCommon.GetDataTable("TS_spfrmLeaveApplication @Activity='LoadCalendar', @Language='" 
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

		public static DataTable LoadLeaveMaster(string strLanguage, string strEmpID, string strFromDate, string strToDate, string strWorkPointID, string strLeaveRecordID, string strCurUser,string strYear)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb = clsCommon.GetDataTable("TS_spfrmLeaveApplication @Activity='LoadLeaveMaster', @Language='" 
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
			string strFromDate, string strToDate, string strCurUser,bool bStatusRequest)
		{
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "TS_spfrmLeaveApplication";
				
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "DeleteLDetailsTemp";						
				cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = strEmpID;
				cmd.Parameters.Add("@LSWorkPointID", SqlDbType.NVarChar, 12).Value = strLSWorkPointID;
				cmd.Parameters.Add("@FromDate", SqlDbType.NVarChar, 12).Value = strFromDate;
				cmd.Parameters.Add("@ToDate", SqlDbType.NVarChar, 12).Value = strToDate;								
				cmd.Parameters.Add("@Creater", SqlDbType.NVarChar, 20).Value = strCurUser;
				cmd.Parameters.Add("@StatusRequest", SqlDbType.Bit).Value = bStatusRequest;
				cmd.ExecuteNonQuery();											

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
					cmd.Parameters.Add("@StatusRequest", SqlDbType.Bit).Value = bStatusRequest;
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
							cmd.Parameters.Add("@RequestID",SqlDbType.NVarChar,12).Value = ((HtmlInputHidden)dtgList.Items[i].FindControl("txtRequestID")).Value.Trim();
							cmd.Parameters.Add("@FromDate",SqlDbType.NVarChar,12).Value = strFromDate.Trim();
							cmd.Parameters.Add("@ToDate",SqlDbType.NVarChar,12).Value = strToDate.Trim();
							cmd.Parameters.Add("@LSWorkPointID",SqlDbType.NVarChar,12).Value = ((HtmlInputHidden)dtgList.Items[i].FindControl("txtWorkPointID")).Value.Trim();
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
				cmd.CommandText = "TS_spfrmLeaveApplication";
   
				CheckBox obj = new CheckBox();
				for (int i = 0;i<= dtgLeaveRecord.Items.Count - 1;i++)
				{
					obj = (CheckBox) dtgLeaveRecord.Items[i].FindControl("chkSelect");
					if (obj.Checked)
					{
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "Delete";						
						cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = dtgLeaveRecord.Items[i].Cells[1].Text.Trim();
						cmd.Parameters.Add("@LeaveRecordID", SqlDbType.NVarChar, 12).Value = dtgLeaveRecord.Items[i].Cells[0].Text.Trim();						
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
		
	
	/// <summary>
		/// Xoa du lieu leave record
		/// </summary>
		public static string DeleteLeaveRequest(DataGrid dtgLeaveRecord, string strCurUser)
		{
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "TS_spfrmLeaveApplication";
   
				CheckBox obj = new CheckBox();
				for (int i = 0;i<= dtgLeaveRecord.Items.Count - 1;i++)
				{
					obj = (CheckBox) dtgLeaveRecord.Items[i].FindControl("chkSelect");
					if (obj.Checked)
					{
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "DeleteLeaveRequest";						
						cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = dtgLeaveRecord.Items[i].Cells[1].Text.Trim();
						cmd.Parameters.Add("@RequestID", SqlDbType.NVarChar, 12).Value = dtgLeaveRecord.Items[i].Cells[0].Text.Trim();						
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
	#region LeaveRecordDetails_Cancel
	public class clsTMSLeaveRecordDetails_Cancel
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
				dtb = clsCommon.GetDataTable("TS_spfrmLeaveApplication @Activity='FillEmpLRInfo', @Language='" 
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
				dtb = clsCommon.GetDataTable("TS_spfrmLeaveApplication @Activity='LoadLeaveDetails_Cancel', @Language='" 
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
							cmd.Parameters.Add("@RequestID",SqlDbType.NVarChar,12).Value = ((HtmlInputHidden)dtgList.Items[i].FindControl("txtRequestID")).Value.Trim();
							cmd.Parameters.Add("@FromDate",SqlDbType.NVarChar,12).Value = strFromDate.Trim();
							cmd.Parameters.Add("@ToDate",SqlDbType.NVarChar,12).Value = strToDate.Trim();
							cmd.Parameters.Add("@LSWorkPointID",SqlDbType.NVarChar,12).Value = ((HtmlInputHidden)dtgList.Items[i].FindControl("txtWorkPointID")).Value.Trim();
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
	#region clsAnnualLeaveByStaff
	
	public class clsAnnualLeaveByStaff
	{
		public static string UpdateData(DataGrid dtgList, int i, string sEffDate, string sAccountLogin, string type)
		{
			string sErrMess="";
			SqlCommand cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);		
			SQLconn.Open();
			cmd.Connection = SQLconn;
			cmd.Transaction= SQLconn.BeginTransaction();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "TS_spfrmALBYSTAFFLEVEL";

			string sID, sLSLeaveLevelID, sAnnualLeave,sTraveling,sRR, sCarryForward, sNote;

			try
			{
				sID = dtgList.Items[i].Cells[0].Text;
				sLSLeaveLevelID = dtgList.Items[i].Cells[8].Text;

				sAnnualLeave = ((TextBox)dtgList.Items[i].FindControl("txtAnnualLeave")).Text.Trim();
				sTraveling = ((TextBox)dtgList.Items[i].FindControl("txtTraveling")).Text.Trim();
				sRR = ((TextBox)dtgList.Items[i].FindControl("txtRR")).Text.Trim();
				sCarryForward = ((TextBox)dtgList.Items[i].FindControl("txtCarryForward")).Text.Trim();
				sNote = ((TextBox)dtgList.Items[i].FindControl("txtNote")).Text.Trim();

				cmd.Parameters.Clear();						
				cmd.Parameters.Add("@Activity", SqlDbType.NVarChar,50).Value = type;							
				
				cmd.Parameters.Add("@ALByStaffLevelID", SqlDbType.NVarChar, 12).Value = (type == "Insert")? "": sID;	
				cmd.Parameters.Add("@EffDate", SqlDbType.VarChar, 12).Value = sEffDate;
				cmd.Parameters.Add("@LSLeaveLevelID",SqlDbType.NVarChar,12).Value = sLSLeaveLevelID;

				if (sAnnualLeave == "")
					cmd.Parameters.Add("@AnnualLeave", SqlDbType.Decimal).Value = DBNull.Value;	
				else
					cmd.Parameters.Add("@AnnualLeave", SqlDbType.Decimal).Value = sAnnualLeave;	
			
				if (sTraveling == "")
					cmd.Parameters.Add("@Traveling",  SqlDbType.Decimal).Value = DBNull.Value;	
				else
					cmd.Parameters.Add("@Traveling", SqlDbType.Decimal).Value = sTraveling;	
	
				if (sRR == "")
					cmd.Parameters.Add("@RR", SqlDbType.Decimal).Value = DBNull.Value;	
				else
					cmd.Parameters.Add("@RR", SqlDbType.Decimal).Value = sRR;	


				if (sCarryForward == "")
					cmd.Parameters.Add("@CarryForward", SqlDbType.Decimal).Value = DBNull.Value;	
				else
					cmd.Parameters.Add("@CarryForward", SqlDbType.Decimal).Value = sCarryForward;	

				cmd.Parameters.Add("@Note",SqlDbType.NVarChar, 255).Value = sNote;								
				cmd.Parameters.Add("@Creater",sAccountLogin);

				cmd.ExecuteNonQuery();			
				cmd.Transaction.Commit();				
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
			}
			catch (Exception exp)
			{
				sErrMess = exp.Message;
				cmd.Transaction.Rollback();			
			}
			return sErrMess;
		}	
	}

	#endregion
	#region "Leave Forward"
	public class clsLeaveForward
	{
		public static string DeleteConvertALRecord(DataGrid dtgList)
		{
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "TMS_spfrmCarryAL";
   
				CheckBox obj = new CheckBox();
				for (int i = 0;i<= dtgList.Items.Count - 1;i++)
				{
					obj = (CheckBox) dtgList.Items[i].FindControl("chkDelete");
					if (obj.Checked)
					{
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "Delete";						
						cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = dtgList.Items[i].Cells[0].Text.Trim();
						cmd.Parameters.Add("@Year", SqlDbType.NVarChar, 4).Value = dtgList.Items[i].Cells[1].Text.Trim();
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
		/// <summary>
		/// Luu thong tin chuyen ngay phep sang nam sau cua nhan vien
		/// </summary>
		public static string SaveConvertAL(DataGrid dtgList)
		{	
			decimal dMaxCarriedForward=0;
			decimal dALValidConvert = 0;
			decimal dConverts=0;
			decimal dCashOut=0;
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			SQLconn.Open();
			try
			{
				
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "TMS_spfrmCarryAL";
   
				CheckBox obj = new CheckBox();
				for (int i = 0;i<= dtgList.Items.Count - 1;i++)
				{

					
					obj = (CheckBox) dtgList.Items[i].FindControl("chkDelete");
					if (obj.Checked)
					{
						cmd.Parameters.Clear();
						
						dMaxCarriedForward= Decimal.Parse(((TextBox)dtgList.Items[i].FindControl("txtMax")).Text.Trim());   							
						dConverts =Decimal.Parse(((TextBox)dtgList.Items[i].FindControl("txtConvertDays")).Text.Trim());   	
						dALValidConvert =Decimal.Parse(((TextBox)dtgList.Items[i].FindControl("txtALValidConvert")).Text.Trim());   	
						if (dConverts>= dMaxCarriedForward && dMaxCarriedForward!=0)
						{
							dConverts=dMaxCarriedForward;
							
						}
						dCashOut= dALValidConvert-dConverts;
						cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "Save";					

						cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = dtgList.Items[i].Cells[0].Text.Trim();
						cmd.Parameters.Add("@Year", SqlDbType.NVarChar, 4).Value = dtgList.Items[i].Cells[1].Text.Trim();
						cmd.Parameters.Add("@ALDays", SqlDbType.Decimal).Value = Decimal.Parse(dtgList.Items[i].Cells[7].Text.Trim());
						cmd.Parameters.Add("@ConvertDays", SqlDbType.Decimal).Value = dConverts;//Decimal.Parse(((TextBox)dtgList.Items[i].FindControl("txtConvertDays")).Text.Trim());   
						cmd.Parameters.Add("@UseToDate",SqlDbType.NVarChar,12).Value=dtgList.Items[i].Cells[13].Text.Trim();   
						cmd.Parameters.Add("@LeaveTaken",SqlDbType.Decimal).Value=Decimal.Parse(dtgList.Items[i].Cells[8].Text.Trim());
						cmd.Parameters.Add("@Balance",SqlDbType.Decimal).Value=Decimal.Parse(dtgList.Items[i].Cells[9].Text.Trim());
						cmd.Parameters.Add("@CashOutDays",SqlDbType.Decimal).Value=dCashOut;//Decimal.Parse(((TextBox)dtgList.Items[i].FindControl("txtCashOut")).Text.Trim());   
						cmd.Parameters.Add("@Note",SqlDbType.NVarChar,200).Value=DBNull.Value;					
					
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
		public static DataTable GetConvertALList(string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strCompany, string strYear, int iStatus, int IsCashOut, string strStatus, System.Web.UI.Page pPage, string sLSEmpTypeID,string sGender, string sLocalExpat, string strLSLocationID, string sShortName)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string strSql = "TMS_spfrmCarryAL @Activity='GetAllData',@EmpID=N'" + strEmpID + "', @EmpName=N'" + strEmpName
				+ "', @LSLevel1ID='" + strLevel1 + "', @LSLevel2ID='" + strLevel2 + "',@LSLevel3ID='" + strLevel3				
				+ "', @LSCompanyID='" + strCompany + "', @Year='" + strYear + "',@ConvertType=" + iStatus + ", @IsCashOut='" + IsCashOut + "',@Status='" + strStatus + "',@UserGroupID=N'" + sAccountLogin + "',@LSEmpTypeID='" + sLSEmpTypeID + "', @Gender='" + sGender + "',@LocalExpat='" + sLocalExpat + "',@LSLocationID='" + strLSLocationID + "',@ShortName='" + sShortName + "'";
			//strSql = strSql + ",@IsSave='" + Convert.ToString(intIsSave) + "'";
			DataTable dtb = clsCommon.GetDataTable(strSql);
			return dtb;
		}
		public static DataTable GetConvertRRList(string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strCompany, string strYear, int iStatus, int IsCashOut, string strStatus, System.Web.UI.Page pPage, string sLSEmpTypeID,string sGender, string sLocalExpat, string strLSLocationID, string sShortName)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string strSql = "TMS_spfrmCarryRR @Activity='GetAllData',@EmpID=N'" + strEmpID + "', @EmpName=N'" + strEmpName
				+ "', @LSLevel1ID='" + strLevel1 + "', @LSLevel2ID='" + strLevel2 + "',@LSLevel3ID='" + strLevel3				
				+ "', @LSCompanyID='" + strCompany + "', @Year='" + strYear + "',@ConvertType=" + iStatus + ", @IsCashOut='" + IsCashOut + "',@Status='" + strStatus + "',@UserGroupID=N'" + sAccountLogin + "',@LSEmpTypeID='" + sLSEmpTypeID + "', @Gender='" + sGender + "',@LocalExpat='" + sLocalExpat + "',@LSLocationID='" + strLSLocationID + "',@ShortName='" + sShortName + "'";
			//strSql = strSql + ",@IsSave='" + Convert.ToString(intIsSave) + "'";
			DataTable dtb = clsCommon.GetDataTable(strSql);
			return dtb;
		}

	}
	#endregion
	#region clsTMSLeaveRecordEmp
	public class clsTMSLeaveRecordEmp
	{
		public static DataTable LoadLeaveRecordEmp(string strEmpID, string strYear)
		{
			try
			{
				DataTable dtData= clsCommon.GetDataTable("TMS_spfrmCarryAL @Activity='LoadLeaveRecordEmp', @EmpID='" + strEmpID + "', @Year='" + Convert.ToString((int.Parse(strYear)-1)) + "'");
				return dtData;
			}
			catch(Exception ex)
			{
				return null;
			}
		}

		public static DataTable LoadLeaveRecordEmpRR(string strEmpID, string strYear)
		{
			try
			{
				DataTable dtData= clsCommon.GetDataTable("TMS_spfrmCarryRR @Activity='LoadLeaveRecordEmp', @EmpID='" + strEmpID + "', @Year='" + Convert.ToString((int.Parse(strYear)-1)) + "'");
				return dtData;
			}
			catch(Exception ex)
			{
				return null;
			}
		}
		public static DataTable LoadLeaveEmp(string strEmpID, string strYear)
		{
			try
			{
				DataTable dtData= clsCommon.GetDataTable("TMS_spfrmCarryAL @Activity='GetAllData', @EmpID='" + strEmpID + "', @Year='" + strYear + "'");
				return dtData;
			}
			catch(Exception ex)
			{
				return null;
			}
		}
		public static DataTable LoadLeaveEmpRR(string strEmpID, string strYear)
		{
			try
			{
				DataTable dtData= clsCommon.GetDataTable("TMS_spfrmCarryRR @Activity='GetAllData', @EmpID='" + strEmpID + "', @Year='" + strYear + "'");
				return dtData;
			}
			catch(Exception ex)
			{
				return null;
			}
		}
	}
	#endregion
	#region clsLeave Common
	public class clsLeaveParam
	{
		public static bool GetIsUseToDate()
		{
			try
			{
				DataRow drData= clsCommon.GetDataRow("Leave_clsCommon @Activity='GetIsUseToDate'");
				if (drData[0].ToString()=="1")
					return true;
				else
					return false;
			}
			catch(Exception ex)
			{
				return false;
			}
		}
		public static DataTable LoadLeaveEmp(string strEmpID, string strYear)
		{
			try
			{
				DataTable dtData= clsCommon.GetDataTable("TMS_spfrmCarryAL @Activity='GetAllData', @EmpID='" + strEmpID + "', @Year='" + strYear + "'");
				return dtData;
			}
			catch(Exception ex)
			{
				return null;
			}
		}
		public static void ClearControlHeader(iHRPCore.Include.EmpHeaderSearch EmpHeaderSearch1)
		{
			EmpHeaderSearch1.cboCompany.SelectedValue="";
			EmpHeaderSearch1.cboLevel1.SelectedValue="";
			EmpHeaderSearch1.cboLevel2.SelectedValue="";
			EmpHeaderSearch1.cboLevel3.SelectedValue="";
			EmpHeaderSearch1.txtEmpID.Text="";			
			EmpHeaderSearch1.txtEmpName.Text="";			
			EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue="";
		}
		public static string getAccountLoginFromEmpID(string sEmpID)
		{
			DataRow drData = clsCommon.GetDataRow("HR_clsCommon @Activity='getAccountLoginFromEmpID',@EmpID='" + sEmpID + "'");
			if (drData!=null)
			{
				return drData[0].ToString();
			}
			else
			{
				return "";
			}

		}
	}
	#endregion
	#region clsTMSLeaveCancellation
	public class clsTMSLeaveCancellation
	{
		public static DataTable LoadDataCancelDetails (string strEmpID, string strFromDate, string strToDate, 
			string strWorkPointID, string strLeaveRecordID, string strYear, string strIsDetails, string strCurUser)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb = clsCommon.GetDataTable("TS_spfrmLeaveCancellation @Activity='LoadDataCancel', @EmpID='" + strEmpID 
					+ "', @FromDate='" + strFromDate + "', @ToDate='" + @strToDate + "', @LSWorkPointID='" + strWorkPointID
					+ "', @LeaveRecordID='" + strLeaveRecordID + "', @Year='" + strYear + "', @IsDetails='" + strIsDetails 
					+ "', @Creater='" + strCurUser + "'");
			}
			catch
			{
				dtb = new DataTable();
			}
			return dtb;
		}

		
		
		public static string SaveCancelLeaveDetailsTemp(DataTable dtb, string strEmpID, string strLSWorkPointID, 
			string strFromDate, string strToDate, string strCurUser,bool bStatusRequest)
		{
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "TS_spfrmLeaveCancellation";
				
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "DeleteCLDetailsTemp";						
				cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = strEmpID;
				cmd.Parameters.Add("@LSWorkPointID", SqlDbType.NVarChar, 12).Value = strLSWorkPointID;
				cmd.Parameters.Add("@FromDate", SqlDbType.NVarChar, 12).Value = strFromDate;
				cmd.Parameters.Add("@ToDate", SqlDbType.NVarChar, 12).Value = strToDate;								
				cmd.Parameters.Add("@Creater", SqlDbType.NVarChar, 20).Value = strCurUser;
				cmd.Parameters.Add("@StatusRequest", SqlDbType.Bit).Value = bStatusRequest;
				cmd.ExecuteNonQuery();											

				for (int i = 0;i< dtb.Rows.Count;i++)
				{
					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "SaveCLDetailsTemp";						
					cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = strEmpID;
					cmd.Parameters.Add("@LSWorkPointID", SqlDbType.NVarChar, 12).Value = strLSWorkPointID;
					cmd.Parameters.Add("@FromDate", SqlDbType.NVarChar, 12).Value = strFromDate;
					cmd.Parameters.Add("@ToDate", SqlDbType.NVarChar, 12).Value = strToDate;
					cmd.Parameters.Add("@DateID", SqlDbType.NVarChar, 12).Value = dtb.Rows[i]["DateID"].ToString().Trim();
					cmd.Parameters.Add("@LeaveTaken", SqlDbType.Real).Value = dtb.Rows[i]["LeaveTaken"].ToString().Trim();
					cmd.Parameters.Add("@Creater", SqlDbType.NVarChar, 20).Value = strCurUser;
					cmd.Parameters.Add("@StatusRequest", SqlDbType.Bit).Value = bStatusRequest;
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


		public static string SaveData(string strEmpID, string strLeaveRecordID, string strFromDate, string strToDate, 
			string strWorkPoint, string strLeaveTaken, bool bIsSubmit, string strIsDetail, string strCurUser, string sReason)
		{
			string strErr = "";
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);		
			SQLconn.Open();
			cmd.Connection = SQLconn;
			cmd.Transaction= SQLconn.BeginTransaction();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "TS_spfrmLeaveCancellation";
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = strLeaveRecordID == ""?"SaveNew":"Update";
				cmd.Parameters.Add("@EmpID",SqlDbType.NVarChar,12).Value = strEmpID;
				cmd.Parameters.Add("@LeaveRecordID",SqlDbType.NVarChar,12).Value = strLeaveRecordID;					
				cmd.Parameters.Add("@FromDate",SqlDbType.NVarChar,12).Value = strFromDate.Trim();
				cmd.Parameters.Add("@ToDate",SqlDbType.NVarChar,12).Value = strToDate.Trim();
				cmd.Parameters.Add("@LSWorkPointID",SqlDbType.NVarChar,12).Value = strWorkPoint;
				cmd.Parameters.Add("@LeaveTaken",SqlDbType.Real).Value = strLeaveTaken;					
				cmd.Parameters.Add("@Creater",SqlDbType.NVarChar,20).Value = strCurUser.ToString();	
				cmd.Parameters.Add("@Reason",SqlDbType.NVarChar,255).Value = sReason.ToString();
				cmd.Parameters.Add("@IsSubmit",SqlDbType.Bit,1).Value = bIsSubmit;
				cmd.Parameters.Add("@IsDetails",SqlDbType.Bit,1).Value = strIsDetail == "1"?true:false;
				cmd.Parameters.Add("@StatusRequest",SqlDbType.Bit,1).Value = 0;
				cmd.Parameters.Add("@Result", SqlDbType.NVarChar,1000).Direction = ParameterDirection.InputOutput;
				cmd.ExecuteNonQuery();
				strErr= cmd.Parameters["@Result"].Value.ToString();
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
			return strErr;
		}

		public static DataTable LoadLeaveRecordByID(string strEmpID,  string strLeaveRecordID, string strCurUser)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb = clsCommon.GetDataTable("TS_spfrmLeaveCancellation @Activity='FillEmpLRInfo', @EmpID='" 
					+ strEmpID + "', @LeaveRecordID='" + strLeaveRecordID + "', @Creater='" + strCurUser + "'");
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
				clsCommon.GetDataTable("TS_spfrmLeaveCancellation @Activity='DeleteLRDTemp', @Creater='" + strCurUser + "'");
			}
			catch (Exception exp)
			{
				strErr = exp.Message.Trim();
			}
			return strErr;
		}
	}
	#endregion
	#region Leave Cut Off
	public class clsTMSLeaveCutOff
	{
		public static DataTable Load(string strLanguage, string strEmpID,string strCompany, string strLevel1, string strLevel2,string strLevel3,string strEmpName, string strStatus, string strEmpType, string sYear, object sLineManagerID,System.Web.UI.Page pPage, string sUseToDate)
		{
			//cangtt - 01052006 - c?p nh?t ph?n phân quy?n d? li?u
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			//--------------------------------------------------
			DataTable dtb = new DataTable();
			try
			{
				dtb = clsCommon.GetDataTable("TS_spfrmCutOff @Activity='LoadData', @LanguageID='" 
					+ strLanguage + "', @EmpID=N'" + strEmpID + "', @EmpName=N'" + strEmpName
					+ "', @LSLevel1ID=N'" + strLevel1 + "', @LSLevel2ID=N'" + strLevel2
					+ "', @LSLevel3ID=N'" + strLevel3 + "', @LSCompanyID=N'" + strCompany 
					+ "',@LSEmpTypeID=N'" + strEmpType + "',@Status='" + strStatus + "',@UserGroupID='" + sAccountLogin + "',@LineManager='" + sLineManagerID + "',@Year='" + sYear + "', @DateParam='" + sUseToDate + "'");
			}
			catch
			{
				dtb = new DataTable();
			}
			return dtb;
		}
		public static string CutOff(DataGrid dtgList, string sYear)
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
				TextBox txtCutOff = new TextBox();
				CheckBox chk = new CheckBox();
				cmd.CommandText = "TS_spfrmCutOff";				
				for (int i = 0;i< dtgList.Items.Count;i++)
				{
					chk = (CheckBox)dtgList.Items[i].FindControl("chkSelect");
					if (chk.Checked)
					{
						txtCutOff = (TextBox)dtgList.Items[i].FindControl("txtCutOff"); 
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 50).Value = "CutOff";						
						cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = dtgList.Items[i].Cells[0].Text;
						cmd.Parameters.Add("@DateParam", SqlDbType.NVarChar, 12).Value = dtgList.Items[i].Cells[12].Text;
						cmd.Parameters.Add("@Year", SqlDbType.NVarChar, 4).Value = sYear;
						cmd.Parameters.Add("@CutOff", SqlDbType.Decimal).Value = txtCutOff.Text;
						
						cmd.ExecuteNonQuery();	
					}
					
					
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
	}
	#endregion

}
