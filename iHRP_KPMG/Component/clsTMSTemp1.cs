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
using System.Threading;

namespace iHRPCore.TMSComponent
{
	/// <summary>
	/// Summary description for clsTMSTemp1.
	/// </summary>
	public class clsTMSShiftTour
	{
		public static void LoadComboShiftTour(DropDownList pControl, string strMonth)
		{
			clsCommon.LoadDropDownListControl(pControl,"TMS_spfrmAssignShiftTour @Activity='GetDataCombo',@Month='" + strMonth + "'","ShiftTourID","ShiftTourID",true);
		}
		public static string SaveShiftTour(DataGrid dtgList,string strMonth)
		{
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "TMS_spfrmAssignShiftTour";
   
				CheckBox obj = new CheckBox();
				for (int i = 0;i<= dtgList.Items.Count - 1;i++)
				{
					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "Save";						
					cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = dtgList.Items[i].Cells[1].Text.Trim();
					cmd.Parameters.Add("@ShiftTourID", SqlDbType.NVarChar, 12).Value = ((DropDownList)dtgList.Items[i].FindControl("cboTourLeaving")).SelectedValue.Trim();
					cmd.Parameters.Add("@Month",SqlDbType.NVarChar,7).Value= strMonth;  
					cmd.Parameters.Add("@IsSave", SqlDbType.Bit).Value = ((CheckBox)dtgList.Items[i].FindControl("chkSelect")).Checked?1:0;
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

		public static string DeleteShiftTour(DataGrid dtgList)
		{
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "TMS_spfrmAssignShiftTour";
   
				CheckBox obj = new CheckBox();
				for (int i = 0;i<= dtgList.Items.Count - 1;i++)
				{
					obj = (CheckBox) dtgList.Items[i].FindControl("chkSelect");
					if (obj.Checked)
					{
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "Delete";						
						cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = dtgList.Items[i].Cells[1].Text.Trim();
						cmd.Parameters.Add("@Month", SqlDbType.NVarChar, 7).Value = dtgList.Items[i].Cells[2].Text.Trim();
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
	public class clsTMSLeaveTour
	{
		public static void LoadComboLeaveTour(DropDownList pControl, string strMonth)
		{
			clsCommon.LoadDropDownListControl(pControl,"TMS_spfrmAssignLeaveTour @Activity='GetDataCombo',@Month='" + strMonth + "'","LeaveTourID","LeaveTourID",true);
		}
		public static DataTable GetAssignLeaveTourList(string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strPosition, string strJobCode, string strLocation, string strCompany, string strStatus,int intIsSave)
		{
			string strSql = "TMS_spfrmAssignLeaveTour @Activity='GetAllData',@EmpID=N'" + strEmpID + "', @EmpName=N'" + strEmpName
				+ "', @LSLevel1Code=N'" + strLevel1 + "', @LSLevel2Code=N'" + strLevel2 + "',@LSLevel3Code=N'" + strLevel3
				+ "',@LSPositionCode=N'" + strPosition + "',@LSJobCodeCode=N'" + strJobCode 
				+ "',@LSLocationCode=N'" + strLocation + "', @LSCompanyCode=N'" + strCompany + "', @Status=N'" + strStatus +"'" ;
			strSql = strSql + ",@IsSave='" + Convert.ToString(intIsSave) + "'";
			DataTable dtb = clsCommon.GetDataTable(strSql);
			return dtb;
		}
		public static string SaveLeaveTour(DataGrid dtgList,string strMonth)
		{
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "TMS_spfrmAssignLeaveTour";
   
				CheckBox obj = new CheckBox();
				for (int i = 0;i<= dtgList.Items.Count - 1;i++)
				{
					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "Save";						
					cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = dtgList.Items[i].Cells[1].Text.Trim();
					cmd.Parameters.Add("@LeaveTourID", SqlDbType.NVarChar, 12).Value = ((DropDownList)dtgList.Items[i].FindControl("cboTourLeaving")).SelectedValue.Trim();
					cmd.Parameters.Add("@Month",SqlDbType.NVarChar,7).Value= strMonth;  
					cmd.Parameters.Add("@IsSave", SqlDbType.Bit).Value = ((CheckBox)dtgList.Items[i].FindControl("chkSelect")).Checked?1:0;
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
		public static string DeleteLeaveTour(DataGrid dtgList)
		{
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "TMS_spfrmAssignLeaveTour";
   
				CheckBox obj = new CheckBox();
				for (int i = 0;i<= dtgList.Items.Count - 1;i++)
				{
					obj = (CheckBox) dtgList.Items[i].FindControl("chkSelect");
					if (obj.Checked)
					{
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "Delete";						
						cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = dtgList.Items[i].Cells[1].Text.Trim();
						cmd.Parameters.Add("@Month", SqlDbType.NVarChar, 7).Value = dtgList.Items[i].Cells[2].Text.Trim();
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
	
	public class clsEmpTMSInfo
	{
		public static void LoadComboShift(DropDownList pControl)
		{
			clsCommon.LoadDropDownListControl(pControl,"TMS_spfrmEmpInfo @Activity='GetDataCombo'","ShiftID","ShiftName",true);		
		}
		public static DataRow GetDataByID(Object strEmpID)
		{
			DataRow iRow = clsCommon.GetDataRow("TMS_spfrmEmpInfo @Activity='GetDataByID',@EmpID = N'" + strEmpID + "'");
			if(iRow !=null)
				return iRow;
			else
				return null;
		}
		public static string SaveEmpInfo(Object strEmpID,string strScanCode,string strShiftID,string strStandardDays,string strStandardHours,string strALDays,string strScanTime)
		{
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "TMS_spfrmEmpInfo";
   
				CheckBox obj = new CheckBox();
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity", SqlDbType.NVarChar,50).Value = "Save";						
				cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = strEmpID;
				cmd.Parameters.Add("@ScanCode", SqlDbType.NVarChar, 12).Value = strScanCode; 
				cmd.Parameters.Add("@ShiftID",SqlDbType.NVarChar,12).Value= strShiftID; 
				cmd.Parameters.Add("@StandardDays", SqlDbType.Real).Value = strStandardDays; 
				cmd.Parameters.Add("@StandardHours",SqlDbType.Real).Value=strStandardHours; 
				cmd.Parameters.Add("@ALDays",SqlDbType.Real).Value=strALDays; 
				cmd.Parameters.Add("@ScanTime",SqlDbType.TinyInt).Value= strScanTime;  
				
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

	}
	public class clsShiftAssign
	{
		public static void LoadComboLevel1(DropDownList pControl,string strLanguage,string strCompanyCode)
		{
			clsCommon.LoadDropDownListControl(pControl,"sp_GetDataComboLevel @TableName = 'LS_tblLevel1',@Language='" + strLanguage + "',@LSCompanyCode=N'" + strCompanyCode + "'","LSLevel1Code","Name",true);		
		}		
		public static void LoadComboLevel2(DropDownList pControl,string strLanguage,string strLevel1Code)
		{
			clsCommon.LoadDropDownListControl(pControl,"sp_GetDataComboLevel @TableName = 'LS_tblLevel2',@Language='" + strLanguage + "',@LSLevel1Code=N'" + strLevel1Code + "'","LSLevel2Code","Name",true);		
		}		
		public static void LoadComboLevel3(DropDownList pControl,string strLanguage,string strLevel2Code)
		{
			clsCommon.LoadDropDownListControl(pControl,"sp_GetDataComboLevel @TableName = 'LS_tblLevel3',@Language='" + strLanguage + "',@LSLevel2Code=N'" + strLevel2Code + "'","LSLevel3Code","Name",true);		
		}	
		public static void LoadComboPosition(DropDownList pControl,string strLanguage)
		{
			clsCommon.LoadDropDownListControl(pControl,"sp_GetDataComboLevel @TableName = 'LS_tblPosition',@Language='" + strLanguage + "'","LSPositionCode","Name",true);		
		}	
		public static DataTable GetEmpByLevel(string strLevel1,string strLevel2,string strLevel3)
		{
			DataTable dtb = clsCommon.GetDataTable("TMS_spfrmShiftAssign @Activity='GetEmpByLevel',@LSLevel1Code='" + strLevel1 + "',@LSLevel2Code='" + strLevel2 + "',@LSLevel3Code='" + strLevel3 + "'");
			return dtb;
		}


	}

	public class clsTMSTransferFromOldComp
	{
		public static string DeleteRecord(string strEmpID)
		{
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "TMS_spfrmOLDCOMPALDAYS";
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "Delete";						
				cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = strEmpID;
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
		public static string SaveTransfer(string strEmpID,string ConvertDays,string ConvertDate,string Note)
		{
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "TMS_spfrmOLDCOMPALDAYS";
   
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "Save";						
				cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = strEmpID;
				cmd.Parameters.Add("@ConvertDays", SqlDbType.Real).Value = ConvertDays; 
				cmd.Parameters.Add("@ConvertDate",SqlDbType.NVarChar,12).Value=ConvertDate;  
				cmd.Parameters.Add("@Note", SqlDbType.NVarChar,255).Value = Note ;
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
		public static string UpdateTransfer(string strEmpID,string ConvertDays,string ConvertDate,string Note)
		{
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "TMS_spfrmOLDCOMPALDAYS";
   
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "Update";						
				cmd.Parameters.Add("@ID", SqlDbType.NVarChar, 12).Value = strEmpID;
				cmd.Parameters.Add("@ConvertDays", SqlDbType.Real).Value = ConvertDays; 
				cmd.Parameters.Add("@ConvertDate",SqlDbType.NVarChar,12).Value=ConvertDate;  
				cmd.Parameters.Add("@Note", SqlDbType.NVarChar,255).Value = Note ;
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


	}
	public class clsTMSCommon
	{
		public static DataTable GetALByJobCode(string strLanguage, string strYear)
		{
			DataTable dtb = clsCommon.GetDataTable("TMS_spfrmALByJCode 'GetAllData',@Language='" + strLanguage 
				+ "',@Year='" + strYear + "'");
			return dtb;
		}

		public static DataTable GetLeaveRecordByID(string strLanguage, string strEmpID, string strFromDate)
		{
			DataTable dtb = clsCommon.GetDataTable("TMS_spfrmLeaveRecord 'GetDataByID',@Language='" + strLanguage
				+ "',@EmpID='" + strEmpID + "',@FromDate='" + strFromDate + "'");
			return dtb;
		}

		public static DataTable GetAllLRecord(string strLanguage, string strEmpID, string strYear)
		{
			DataTable dtb = clsCommon.GetDataTable("TMS_spfrmLeaveRecord 'GetDataAll',@Language='" + strLanguage
				+ "',@EmpID='" + strEmpID + "', @Year='" + strYear + "'");
			return dtb;
		}

		public static DataTable getLeaveRecordList(string strLanguage, string strFromDate, string strToDate, string strLeaveTypeCode, string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strPosition, string strJobCode, string strLocation, string strCompany, string strStatus)
		{
			string strSql = "TMS_spfrmLeaveRecord 'LeaveRecordList',@Language='" + strLanguage
				+ "',@FromDate='" + strFromDate + "', @ToDate='" + strToDate + "', @LSLeaveTypeCode='" + strLeaveTypeCode + "'"
				+ ",@EmpID=N'" + strEmpID + "', @EmpName=N'" + strEmpName
				+ "', @LSLevel1Code='" + strLevel1 + "', @LSLevel2Code='" + strLevel2 + "',@LSLevel3Code='" + strLevel3
				+ "',@LSPositionCode='" + strPosition + "',@LSJobCodeCode='" + strJobCode 
				+ "',@LSLocationCode='" + strLocation + "',@LSCompanyCode='" + strCompany + "'";
			if (strStatus != "")
				strSql = strSql + ", @Status='" + strStatus + "'";
			DataTable dtb = clsCommon.GetDataTable(strSql);
			return dtb;
		}

		public static DataTable GetDatarptLRByMonth(string strLanguage, string strFromDate, string strToDate, string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strPosition, string strJobCode, string strLocation, string strCompany, string strStatus)
		{
			string strSql = "TMS_sprptLeave_Record_Job_Class @DateFrom='" + strFromDate + "', @DateTo='" + strToDate
				+ "',@EmpID=N'" + strEmpID + "', @EmpName=N'" + strEmpName
				+ "', @LSLevel1Code='" + strLevel1 + "', @LSLevel2Code='" + strLevel2 + "',@LSLevel3Code='" + strLevel3
				+ "',@LSPositionCode='" + strPosition + "',@LSJobCodeCode='" + strJobCode 
				+ "',@LSLocationCode='" + strLocation + "',@LSCompanyCode='" + strCompany + "'";
			if (strStatus != "")
				strSql = strSql + ", @Status='" + strStatus + "'";
			DataTable dtb = clsCommon.GetDataTable(strSql);
			return dtb;
		}

		public static DataTable GetLRValidFromTo(string strEmpID)
		{
			DataTable dtb = clsCommon.GetDataTable("TMS_spfrmLeaveRecord 'GetValidFromToDate',@EmpID='" + strEmpID + "'");
			return dtb;
		}

		public static DataTable GetResignInfo(string strEmpID)
		{
			DataTable dtb = clsCommon.GetDataTable("TMS_spfrmLeaveRecord 'GetResignInfo',@EmpID='" + strEmpID + "'");
			return dtb;
		}

		public static void LoadComboLeaveType(DropDownList pControl, string strTextField)
		{
			clsCommon.LoadDropDownListControl(pControl,"sp_GetDataCombo @TableName='LS_tblLeaveType',@Fields='LSLeaveTypeCode," + strTextField + " as Name'","LSLeaveTypeCode","Name",true);
		}

		public static void LoadListLeaveType(ListBox pControl)
		{
			pControl.DataSource = clsCommon.GetDataTable("TMS_spfrmLeaveRecord 'GetLstLeaveType'");
			pControl.DataTextField = "LeaveTypeName";
			pControl.DataValueField = "LSLeaveTypeCode";
			pControl.DataBind();
		}

		public static string SaveALByJCode(DataGrid dtgLeaveByJobCode, string strYear, UserControl page)
		{
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "TMS_spfrmALByJCode";

				CheckBox obj = new CheckBox();
				for (int i = 0;i<= dtgLeaveByJobCode.Items.Count - 1;i++)
				{
					obj = (CheckBox) dtgLeaveByJobCode.Items[i].FindControl("chkSelect");
					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "Save";						
					cmd.Parameters.Add("@Year", SqlDbType.NVarChar, 4).Value = strYear;//dtgLeaveByJobCode.Items[i].Cells[0].Text.Trim();
					cmd.Parameters.Add("@LSJobCodeCode", SqlDbType.NVarChar,12).Value = dtgLeaveByJobCode.Items[i].Cells[1].Text.Trim();
					if (obj.Checked)
					{
						cmd.Parameters.Add("@ALDays", SqlDbType.Real).Value = ((TextBox) dtgLeaveByJobCode.Items[i].FindControl("txtALDays")).Text.Trim();
						cmd.Parameters.Add("@ConvertDays", SqlDbType.Real).Value = ((TextBox) dtgLeaveByJobCode.Items[i].FindControl("txtConvertDays")).Text.Trim();
					}
					if (page.Session["AccountLogin"] != null)
						cmd.Parameters.Add("@LastUpdUser", SqlDbType.NVarChar,200).Value = page.Session["AccountLogin"].ToString().Trim();
					cmd.Parameters.Add("@IsSave", SqlDbType.Bit).Value = obj.Checked?1:0;
					cmd.ExecuteNonQuery();											
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

		public static string CheckValidALRecord(string strEmpID, string strFromDate, string strToDate, string strIsEdit)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb = clsCommon.GetDataTable("TMS_spfrmLeaveRecord 'CheckValidData', @EmpID='" + strEmpID
					+ "', @FromDate='" + strFromDate + "', @ToDate='" + strToDate + "', @IsEdit='" + strIsEdit + "'");
				if (dtb.Rows.Count > 0)
				{
					if (dtb.Rows[0]["mCount"].ToString().Trim() == "0")
					{
						return "";
					}
					else
						return "Leave date is duplicate! Check again!";
				}
				else
					return "";
			}
			catch(Exception exp)
			{
				return exp.Message.Trim();
			}
			finally
			{
				dtb.Dispose();
			}
		}

		public static string DeleteLeaveRecord(DataGrid dtgLeaveRecord)
		{
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "TMS_spfrmLeaveRecord";
   
				CheckBox obj = new CheckBox();
				for (int i = 0;i<= dtgLeaveRecord.Items.Count - 1;i++)
				{
					obj = (CheckBox) dtgLeaveRecord.Items[i].FindControl("chkSelect");
					if (obj.Checked)
					{
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "Delete";						
						cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = dtgLeaveRecord.Items[i].Cells[0].Text.Trim();
						cmd.Parameters.Add("@FromDate", SqlDbType.NVarChar, 12).Value = dtgLeaveRecord.Items[i].Cells[1].Text.Trim();
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

		public static string SaveLRecordGroup(string strEmpList,string strLSWorkpointID, string strFromDate, Double dblLeaveTaken, string strNote)
		{
			string strReturn = "";
			string strReturnValid ="";
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
					cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = arrTemp[i].Trim();
					cmd.Parameters.Add("@LSWorkpointID", SqlDbType.NVarChar, 12).Value = strLSWorkpointID;
					cmd.Parameters.Add("@FromDate", SqlDbType.NVarChar, 12).Value = strFromDate;					
					cmd.Parameters.Add("@LeaveTaken", SqlDbType.Decimal).Value = dblLeaveTaken;
					if (strNote != "")
						cmd.Parameters.Add("@Note", SqlDbType.NVarChar,200).Value = strNote;
					else
						cmd.Parameters.Add("@Note", SqlDbType.NVarChar,200).Value = DBNull.Value;
					cmd.Parameters.Add("@Result", SqlDbType.NVarChar,2000).Direction = ParameterDirection.InputOutput;					
					cmd.Parameters.Add("@ResultValid", SqlDbType.NVarChar,2000).Direction = ParameterDirection.InputOutput;					
					cmd.ExecuteNonQuery();	
					if (strReturn != "")
						strReturn = strReturn + "," +  cmd.Parameters["@Result"].Value.ToString();	
					else
						strReturn =  cmd.Parameters["@Result"].Value.ToString();	

					if (strReturnValid != "")
						strReturnValid = strReturnValid + "," +  cmd.Parameters["@ResultValid"].Value.ToString();	
					else
						strReturnValid =  cmd.Parameters["@ResultValid"].Value.ToString();	
				}
				cmd.Transaction.Commit();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();	
				if (strReturn!="") strReturn="," + strReturn + ",";
					
				if (strReturnValid!="") strReturnValid=","+ strReturnValid + ",";
					
				return  strReturn + " @" + strReturnValid;
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

		public static string SaveLRecordGroup(string strLSWorkpointID, string strFromDate, Double dblLeaveTaken, string strNote,
			string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strPosition, string strJobCode, string strLocation, string strCompany, string strStatus,System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string strReturn = "";
			string strReturnValid="";
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
				cmd.Parameters.Add("@Status", SqlDbType.TinyInt).Value = strStatus;

				cmd.Parameters.Add("@LSWorkPointID", SqlDbType.NVarChar, 12).Value = strLSWorkpointID;
				cmd.Parameters.Add("@FromDate", SqlDbType.NVarChar, 12).Value = strFromDate;				
				cmd.Parameters.Add("@LeaveTaken", SqlDbType.Decimal).Value = dblLeaveTaken;
				cmd.Parameters.Add("@UserGroupID",SqlDbType.NVarChar,12).Value=sAccountLogin;
				if (strNote != "")
					cmd.Parameters.Add("@Note", SqlDbType.NVarChar,200).Value = strNote;
				else
					cmd.Parameters.Add("@Note", SqlDbType.NVarChar,200).Value = DBNull.Value;
				cmd.Parameters.Add("@Result", SqlDbType.NVarChar,4000).Direction = ParameterDirection.InputOutput;
				cmd.Parameters.Add("@ResultValid", SqlDbType.NVarChar,4000).Direction = ParameterDirection.InputOutput;
				cmd.ExecuteNonQuery();	
				if (strReturn != "")
					strReturn = strReturn + "," +  cmd.Parameters["@Result"].Value.ToString();	
				else
					strReturn = cmd.Parameters["@Result"].Value.ToString();	

				if (strReturnValid != "")
					strReturnValid = strReturnValid + "," +  cmd.Parameters["@ResultValid"].Value.ToString();	
				else
					strReturnValid = cmd.Parameters["@ResultValid"].Value.ToString();	
				
				cmd.Transaction.Commit();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();

				if (strReturn!="") strReturn="," + strReturn + ",";					
				if (strReturnValid!="") strReturnValid=","+ strReturnValid + ",";


				return strReturn + '@' + strReturnValid;
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
		/// Lay nhan vien ShiftTour theo dieu kien loc
		/// </summary>
		public static DataTable GetAssignShiftTourList(string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strPosition, string strJobCode, string strLocation, string strCompany, string strStatus,int intIsSave)
		{
			string strSql = "TMS_spfrmAssignShiftTour @Activity='GetAllData',@EmpID=N'" + strEmpID + "', @EmpName=N'" + strEmpName
				+ "', @LSLevel1Code=N'" + strLevel1 + "', @LSLevel2Code=N'" + strLevel2 + "',@LSLevel3Code=N'" + strLevel3
				+ "',@LSPositionCode=N'" + strPosition + "',@LSJobCodeCode=N'" + strJobCode 
				+ "',@LSLocationCode=N'" + strLocation + "', @LSCompanyCode=N'" + strCompany + "', @Status=N'" + strStatus +"'" ;
			strSql = strSql + ",@IsSave='" + Convert.ToString(intIsSave) + "'";
			DataTable dtb = clsCommon.GetDataTable(strSql);
			return dtb;
		}


		/// <summary>
		/// Luu thong tin chuyen ngay phep sang nam sau cua nhan vien
		/// </summary>
		public static string SaveConvertAL(DataGrid dtgList)
		{		
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
						cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "Save";	
						
						cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = dtgList.Items[i].Cells[0].Text.Trim();
						cmd.Parameters.Add("@Year", SqlDbType.NVarChar, 4).Value = dtgList.Items[i].Cells[1].Text.Trim();
						cmd.Parameters.Add("@ALDays", SqlDbType.Decimal).Value = Decimal.Parse(dtgList.Items[i].Cells[7].Text.Trim());
						cmd.Parameters.Add("@ConvertDays", SqlDbType.Decimal).Value = Decimal.Parse(dtgList.Items[i].Cells[10].Text.Trim());   
						cmd.Parameters.Add("@UseToDate",SqlDbType.NVarChar,12).Value=dtgList.Items[i].Cells[13].Text.Trim();   
						cmd.Parameters.Add("@LeaveTaken",SqlDbType.Decimal).Value=Decimal.Parse(dtgList.Items[i].Cells[8].Text.Trim());   //hpLeaveTaken
						cmd.Parameters.Add("@Balance",SqlDbType.Decimal).Value=Decimal.Parse(dtgList.Items[i].Cells[9].Text.Trim());
						cmd.Parameters.Add("@CashOutDays",SqlDbType.Decimal).Value=Decimal.Parse(dtgList.Items[i].Cells[11].Text.Trim());   
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

		/// <summary>
		/// Luu thong tin chuyen ngay phep sang nam sau cua nhan vien
		/// </summary>
		public static string SaveConvertRR(DataGrid dtgList)
		{		
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			SQLconn.Open();
			try
			{
				
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "TMS_spfrmCarryRR";
   
				CheckBox obj = new CheckBox();
				for (int i = 0;i<= dtgList.Items.Count - 1;i++)
				{

					
					obj = (CheckBox) dtgList.Items[i].FindControl("chkDelete");
					if (obj.Checked)
					{
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "Save";	
						
						cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = dtgList.Items[i].Cells[0].Text.Trim();
						cmd.Parameters.Add("@Year", SqlDbType.NVarChar, 4).Value = dtgList.Items[i].Cells[1].Text.Trim();
						cmd.Parameters.Add("@RRDays", SqlDbType.Decimal).Value = Decimal.Parse(dtgList.Items[i].Cells[7].Text.Trim());
						cmd.Parameters.Add("@ConvertDays", SqlDbType.Decimal).Value = Decimal.Parse(dtgList.Items[i].Cells[10].Text.Trim());   
						cmd.Parameters.Add("@UseToDate",SqlDbType.NVarChar,12).Value=dtgList.Items[i].Cells[13].Text.Trim();   
						cmd.Parameters.Add("@LeaveTaken",SqlDbType.Decimal).Value=Decimal.Parse(dtgList.Items[i].Cells[8].Text.Trim());   //hpLeaveTaken
						cmd.Parameters.Add("@Balance",SqlDbType.Decimal).Value=Decimal.Parse(dtgList.Items[i].Cells[9].Text.Trim());
						cmd.Parameters.Add("@CashOutDays",SqlDbType.Decimal).Value=Decimal.Parse(dtgList.Items[i].Cells[11].Text.Trim());   
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
		
		public static string DeleteConvertRRRecord(DataGrid dtgList)
		{
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "TMS_spfrmCarryRR";
   
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

		//Frm: Leave record online

		public static DataTable GetLROnlineValidFromTo(string strEmpID)
		{
			DataTable dtb = clsCommon.GetDataTable("TMS_spfrmLeaveRecordOnline 'GetValidFromToDate',@EmpID='" + strEmpID + "'");
			return dtb;
		}

		public static DataTable GetAllLRecordOnline(string strLanguage, string strEmpID)
		{
			DataTable dtb = clsCommon.GetDataTable("TMS_spfrmLeaveRecordOnline 'GetDataAll',@Language='" + strLanguage
				+ "',@EmpID='" + strEmpID + "'");
			return dtb;
		}

		public static DataTable GetLROnlineByID(string strLanguage, string strEmpID, string strFromDate)
		{
			DataTable dtb = clsCommon.GetDataTable("TMS_spfrmLeaveRecordOnline 'GetDataByID',@Language='" + strLanguage
				+ "',@EmpID='" + strEmpID + "',@FromDate='" + strFromDate + "'");
			return dtb;
		}

		public static string DeleteLROnline(DataGrid dtgLeaveRecord)
		{
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "TMS_spfrmLeaveRecordOnline";
   
				CheckBox obj = new CheckBox();
				for (int i = 0;i<= dtgLeaveRecord.Items.Count - 1;i++)
				{
					obj = (CheckBox) dtgLeaveRecord.Items[i].FindControl("chkSelect");
					if (obj.Checked)
					{
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "Delete";						
						cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = dtgLeaveRecord.Items[i].Cells[0].Text.Trim();
						cmd.Parameters.Add("@FromDate", SqlDbType.NVarChar, 12).Value = dtgLeaveRecord.Items[i].Cells[1].Text.Trim();
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

		public static string CheckValidALOLRecord(string strEmpID, string strFromDate, string strToDate)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb = clsCommon.GetDataTable("TMS_spfrmLeaveRecordOnline 'CheckValidData', @EmpID='" + strEmpID
					+ "', @FromDate='" + strFromDate + "', @ToDate='" + strToDate + "'");
				if (dtb.Rows.Count > 0)
				{
					if (dtb.Rows[0]["mCount"].ToString().Trim() == "0")
					{
						return "";
					}
					else
						return "Leave date is duplicate! Check again!";
				}
				else
					return "";
			}
			catch(Exception exp)
			{
				return exp.Message.Trim();
			}
			finally
			{
				dtb.Dispose();
			}
		}


		public static DataTable GetListLeaveType(string strLanguage)
		{
			DataTable dtb = clsCommon.GetDataTable("TMS_spfrmLeaveRecordOnline 'GetListLeaveType',@Language='" + strLanguage + "'");
			return dtb;
		}

		public static DataTable GetListLeaveTypeByID(string strLanguage, string strEmpID, string strYear)
		{
			DataTable dtb = clsCommon.GetDataTable("TMS_spfrmLeaveRecordOnline 'GetListLeaveTypeByID',@Language='" 
				+ strLanguage + "', @EmpID='" + strEmpID + "', @Year='" + strYear + "'");
			return dtb;
		}
		//end Frm: Leave record online

		/// <summary>
		/// Send mail
		/// </summary>
		/// <param name="pFrom">Sender</param>
		/// <param name="pCc">list of mail, semicolo-delimited</param>
		/// <param name="pTo">receiver</param>
		/// <param name="pSubject">subject of mail</param>
		/// <param name="pBody">content of mail</param>
		public static void SendMailFromPage(string pFrom, string pTo, string pSubject, string pBody)
		{
			try
			{
				MailMessage objMail = new MailMessage();
				objMail.From = pFrom;
				objMail.To = pTo;
				objMail.Subject = pSubject;
				objMail.Body = pBody;
				objMail.BodyFormat = MailFormat.Html;
				
				/*objMail.UrlContentBase = pUrlContentBase;
				objMail.UrlContentLocation = pUrlContentLocation;*/
				/*objMail.Fields["http://schemas.microsoft.com/cdo/configuration/smtpserver"]= ConfigurationSettings.AppSettings["pstrMailServer"].Trim();
				objMail.Fields["http://schemas.microsoft.com/cdo/configuration/smtpserverport"] = 25;
				objMail.Fields["http://schemas.microsoft.com/cdo/configuration/sendusing"]  = 2;
				objMail.Fields["http://schemas.microsoft.com/cdo/configuration/smtpauthenticate"] = 1;
				objMail.Fields["http://schemas.microsoft.com/cdo/configuration/sendusername"] = ConfigurationSettings.AppSettings["pstrSendUser"].Trim();
			
				objMail.Fields["http://schemas.microsoft.com/cdo/configuration/sendpassword"] = ConfigurationSettings.AppSettings["pstrSendPassword"].Trim();*/
						
				SmtpMail.SmtpServer = ConfigurationSettings.AppSettings["pstrMailServer"].Trim();
				SmtpMail.Send(objMail);
			}
			catch(Exception exp)
			{
				string strError = exp.Message.ToString();
			}
		}

		public static string GetHTML (string strurl)
		{
			WebRequest wReq = System.Net.HttpWebRequest.Create(strurl);
			StreamReader sr = new StreamReader(wReq.GetResponse().GetResponseStream());
			string strResult = sr.ReadToEnd();
			sr.Close();
			return strResult;
		}

		public static string GetTMS_FunctionApprove (UserControl page)
		{
			string strResult = "";
			DataTable dtb = clsCommon.GetDataTable("TMS_spfrmApproveOnline 'GetTMS_FunctionApprove'");
			if (dtb.Rows.Count > 0)
				strResult = dtb.Rows[0]["TMS_FunctionApprove"].ToString().Trim();
			strResult = page.Request.Path.Trim() + "?" + strResult + "&ExactlyUrl=1";
			return strResult;
			
		}

		public static string GetEmail(string strEmpID)
		{	
			string strResult = "";
			DataTable dtb = clsCommon.GetDataTable("HR_clsCommon 'GetEmail', @EmpID='" + strEmpID + "'");
			if (dtb.Rows.Count > 0)
				strResult = dtb.Rows[0]["Email"].ToString();
			return strResult;
		}

		public static string GetSubject(string TypeApprove, string strEmpName, string strFromDate, string strToDate)
		{
			string strSubject = "";
			if (TypeApprove == "Register")
				strSubject = ConfigurationSettings.AppSettings["pstrRegisterTMS"].Trim();
			else if (TypeApprove == "Reject")
				strSubject = ConfigurationSettings.AppSettings["pstrRejectTMS"].Trim();
			else
				strSubject = ConfigurationSettings.AppSettings["pstrApproveTMS"].Trim();
			strSubject = strSubject + " of " + strEmpName + " from " + strFromDate
				+ " to " + strToDate;
			return strSubject;
		}

		public static string GetBody(string strSubject, string strLeaveType, string strNote, string strUrl)
		{
			string strBody = strSubject + ", leave type: " + strLeaveType; 
			if (strNote != "")
				strBody = strBody + ", Note: " +strNote;
			strBody = strBody + "<br>";
			strBody = strBody + "<a href='" + strUrl + "'>" + strUrl + "</a>";
			return strBody;
		}
		
		//Frm: Leave record online approve
		public static void BindGridApprove(DataGrid pControl, string strApprover, string strLanguage, string strYear)
		{
			DataTable dtb = clsCommon.GetDataTable("TMS_spfrmApproveOnline 'GetListEmp',@Language='" 
				+ strLanguage + "',@Approver='" + strApprover + "',@Year='" + strYear + "'");
			pControl.DataSource = dtb;
			pControl.DataBind();
			dtb.Dispose();
		}

		public static string ImpactDBApprove(UserControl page, string strActivity, DataGrid pControl, string strApprover, string strAssignTo, int intApproveType)
		{
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "TMS_spfrmApproveOnline";
   
				CheckBox obj = new CheckBox();
				for (int i = 0;i<= pControl.Items.Count - 1;i++)
				{
					obj = (CheckBox) pControl.Items[i].FindControl("chkSelect");
					if (obj.Checked)
					{
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = strActivity;						
						cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = pControl.Items[i].Cells[0].Text.Trim();
						cmd.Parameters.Add("@FromDate", SqlDbType.NVarChar, 12).Value = pControl.Items[i].Cells[1].Text.Trim();
						if (((TextBox)pControl.Items[i].FindControl("txtComment")).Text.Trim() != "")
							cmd.Parameters.Add("@Note", SqlDbType.NVarChar, 200).Value = ((TextBox)pControl.Items[i].FindControl("txtComment")).Text.Trim();
						else
							cmd.Parameters.Add("@Note", SqlDbType.NVarChar, 200).Value = DBNull.Value;
						cmd.Parameters.Add("@Approver", SqlDbType.NVarChar, 12).Value = strApprover;
						if (strAssignTo.Trim() != "")
							cmd.Parameters.Add("@AssignTo", SqlDbType.NVarChar, 12).Value = strAssignTo;
						else
							cmd.Parameters.Add("@AssignTo", SqlDbType.NVarChar, 12).Value = DBNull.Value;
						cmd.Parameters.Add("@UserType", SqlDbType.TinyInt).Value = intApproveType;
						cmd.Parameters.Add("@Return", SqlDbType.NVarChar,100).Direction = ParameterDirection.InputOutput;
						cmd.ExecuteNonQuery();	

						string strNextApprover = cmd.Parameters["@Return"].Value.ToString().Trim();
						string strMailFrom = clsTMSCommon.GetEmail(strApprover);
						string strMailTo = "";
						if (strNextApprover.Trim() != "" && strActivity == "Approve")
							strMailTo = clsTMSCommon.GetEmail(strNextApprover);
						else if (strActivity == "Reject")
							strMailTo = clsTMSCommon.GetEmail(pControl.Items[i].Cells[0].Text.Trim());
						if (strMailFrom != "" && strMailTo != "")
						{
							string strUrl = "http://" + page.Request.Url.Host.Trim() + GetTMS_FunctionApprove(page);
							string strSubject = GetSubject(strActivity, pControl.Items[i].Cells[4].Text.Trim(),  pControl.Items[i].Cells[1].Text.Trim(), 
								pControl.Items[i].Cells[6].Text.Trim());
							string strBody = clsTMSCommon.GetBody(strSubject, pControl.Items[i].Cells[8].Text.Trim(), 
								((TextBox)pControl.Items[i].FindControl("txtComment")).Text.Trim(), strUrl);
							SendMailFromPage(strMailFrom,strMailTo,strSubject,strBody);
						}
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
		//end Frm: Leave record online approve

		//Frm: Establish special day in year
		public static string ImpactDBWorkDays(UserControl page, DataGrid pControl, string strYear)
		{
			string strResult = "";
			string strReturn = "";
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "TS_spfrmDelWorkDay";
				cmd.Parameters.Add("@Year", SqlDbType.NVarChar, 4).Value = strYear;
				cmd.Parameters.Add("@Result", SqlDbType.NVarChar,12).Direction = ParameterDirection.InputOutput;
				cmd.ExecuteNonQuery();	
				strResult = Convert.ToString(cmd.Parameters["@Result"].Value);
				if (strResult != "-1")
				{
					string strDateID = "";
					int intDateType = 1;
					string strNote = "";
					for (int i=0; i<pControl.Items.Count; i++)
					{
						strDateID = pControl.Items[i].Cells[0].Text.Trim();
						intDateType = Convert.ToInt16(pControl.Items[i].Cells[1].Text);
						strNote = ((TextBox)pControl.Items[i].FindControl("txtNote")).Text.Trim();
						cmd.CommandText = "TS_spfrmWorkDay";
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Action", SqlDbType.NVarChar, 30).Value = "AddNew";
						cmd.Parameters.Add("@Result", SqlDbType.NVarChar,12).Direction = ParameterDirection.InputOutput;
						cmd.Parameters.Add("@DateID", SqlDbType.NVarChar, 12).Value = strDateID;
						cmd.Parameters.Add("@TypeDate", SqlDbType.TinyInt).Value = intDateType;
						if (strNote == "")
							cmd.Parameters.Add("@Note", SqlDbType.NVarChar, 120).Value = DBNull.Value;
						else
							cmd.Parameters.Add("@Note", SqlDbType.NVarChar, 120).Value = strNote;
						cmd.ExecuteNonQuery();
						strResult = Convert.ToString(cmd.Parameters["@Result"].Value);	
						if (strResult == "-1")
						{
							strReturn = "Error occurrence!";
							break;
						}
					}
					if (strResult == "-1")
					{
						cmd.Transaction.Rollback();
						SQLconn.Close();
					}
					else
					{
						cmd.Transaction.Commit();
						SQLconn.Close();
						strReturn = "Update successful!";
					}
				}
				else
				{
					cmd.Transaction.Rollback();
					SQLconn.Close();
				}
				
				cmd.Dispose();
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
		//end Frm: Establish special day in year
	}
	
	public class clsTMSDownloadFile
	{
		public static DataTable GetMaxDateDownload()
		{
			DataTable dtb = clsCommon.GetDataTable("TS_spfrmDownloadFile 'GetMaxCreateTime'");
			return dtb;
		}

		public static DataTable GetDownloadFileDetails()
		{
			DataTable dtb = clsCommon.GetDataTable("TS_spfrmDownloadFile 'GetFileDetail'");
			return dtb;
		}
	
		/*
		public static void ThreadUploadScanTime(string strPathFileName,Page page)
		{
			TMSComponent.clsTMSDownloadFile LoadFile = new clsTMSDownloadFile();
			System.Threading.Thread thread;
			thread = new System.Threading.Thread(new       
				System.Threading.ParameterizedThreadStart(
				LoadFile.UploadScanTime(strPathFileName,page)));
			thread.Name = "Upload Thread";
			thread.Start();
		}
		*/
		public static string UploadScanTime(string strFileName, Page page)
		{
			string strError = "";
			DataView objView = new DataView();
				
			string strPathFileName = page.Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrScanTimeFolder"].Trim() + strFileName; 
			if (!File.Exists(strPathFileName) || strFileName.Substring(strFileName.LastIndexOf(".")) != ".csv")
				return "File không đúng định dạng. Vui lòng xem lại!";
			
			FileStream fs = new FileStream(strPathFileName , FileMode.Open, FileAccess.Read);
			StreamReader m_streamReader = new StreamReader(fs);
			
			DataTable tblTemp = new DataTable();
			tblTemp.Columns.Add(new DataColumn("DateID"));
			tblTemp.Columns.Add(new DataColumn("ScanCode"));
			DataRow rowtemp;
			string strDateID = "";
			string strScanCode = "";
			string mstr = "";
			int index = 0;
			while((mstr = m_streamReader.ReadLine()) != null)
			{
				//Loai bo du lieu hang dau tien
				if (index == 0)
				{
					index ++;
					continue;
				}
				if(mstr.Length < 1 || mstr.IndexOf(":") <= 0 || mstr.IndexOf(":") == mstr.LastIndexOf(":"))
					continue;
				
				string [] arrStr = mstr.Split(',');

				if(arrStr.Length > 0)
				{
					//string[] ArrayItems = mstr.Split(new char[]{'|'});
					//int intItems = ArrayItems.Length;
					strDateID = arrStr[3].Trim() + " " + arrStr[4].Trim();//mstr.Substring(2,2) + "/" + mstr.Substring(4,2) + "/" + mstr.Substring(6,4) + " " + mstr.Substring(10,8);
					//strDateID = strDateID.Replace("-","/");
					strScanCode = arrStr[1].Trim();
					
					DataRow[] rowArr = tblTemp.Select("DateID='" + strDateID + "' and ScanCode='" + strScanCode + "'");
					if (rowArr.Length == 0)
					{
						rowtemp = tblTemp.NewRow();
						rowtemp["DateID"] = strDateID;
						rowtemp["ScanCode"] = strScanCode;
						tblTemp.Rows.Add(rowtemp);
					}
				}  
			}
			
			//End tao bang	
			SqlCommand  cmd = new SqlCommand();
			string strTimeOut = ConfigurationSettings.AppSettings["pstrConnectionString"].Split(';')[1].Replace("Connect Timeout=","");
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"].Replace(strTimeOut,"72000"));
			
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "TS_spfrmDownloadFile";

				if (tblTemp.Rows.Count > 0)
				{
					objView = tblTemp.DefaultView;
					objView.Sort = "DateID";
				}
				for (int i=0; i<objView.Count; i++)
				{
					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity", SqlDbType.NVarChar,50).Value = "AddNew";
					cmd.Parameters.Add("@ScanTime", SqlDbType.NVarChar,255).Value = objView[i]["DateID"].ToString();
					cmd.Parameters.Add("@ScanCode", SqlDbType.NVarChar,255).Value = objView[i]["ScanCode"].ToString();
					cmd.Parameters.Add("@Creater", SqlDbType.NVarChar,20).Value = page.Session["AccountLogin"];
					cmd.Parameters.Add("@CreateTime", SqlDbType.DateTime).Value = DateTime.Today;
					cmd.Parameters.Add("@FileName", SqlDbType.NVarChar,255).Value = strFileName.Trim();
					cmd.ExecuteNonQuery();
				}
				cmd.Transaction.Commit();
			}
			catch (Exception exp)
			{
				cmd.Transaction.Rollback();
				strError = exp.Message;		
			}
			finally
			{
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				objView.Dispose();
				tblTemp.Dispose();
			}
		
			if (strError == "")
				strError = "Tải file quét thẻ thành công!";
			return strError;
		}
		/*public static int GetPermission(System.Web.UI.Page pPage)
		{
			DataRow drData = clsCommon.GetDataRow("");

		}*/
		public static string ManipulateScanTimeData(string strToDate)
		{
			SqlCommand  cmd = new SqlCommand();
			//SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			string strTimeOut = ConfigurationSettings.AppSettings["pstrConnectionString"].Split(';')[1].Replace("Connect Timeout=","");
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"].Replace(strTimeOut,"72000"));
			
			string strError = "";
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "TS_spfrmDownloadFile";
				cmd.CommandTimeout = 800;
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity", SqlDbType.NVarChar,50).Value = "Process";
				cmd.Parameters.Add("@ToDate", SqlDbType.NVarChar,12).Value = strToDate;
				cmd.ExecuteNonQuery();
				cmd.Transaction.Commit();
			}
			catch(Exception exp)
			{
				strError = exp.Message.Trim();
				strError = "Lỗi xử lý!";
				cmd.Transaction.Rollback();
			}
			finally
			{
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
			}
			if (strError == "")
				strError = "Xử lý file thành công!";
			return strError;
		}
	}
	public class clsTMSLSShift // cangtt --11102005
	{
		//cangtt code 07/10/2005
		public static int ConvertInteger(object pValue)
		{
			if (pValue==DBNull.Value )
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(pValue);
			}
									
		}	


		public static DataRow GetShiftBreakType()  
		{
			return clsCommon.GetDataRow("SYS_spParameters @Activity = 'GetByName', @ParamName = 'TMS_ShiftBreakType'");
		}

		public static DataTable GetDataAll()
		{			
			string strSQL = "TMS_spfrmShift @Activity = 'GetForGrid'";
			return  clsCommon.GetDataTable(strSQL);			
		}
		public static DataTable getDataByID( string strID)
		{
			DataTable rsData = new DataTable();
			rsData = clsCommon.GetDataTable("TMS_spfrmShift @Activity = 'GetData_byID', @ShiftID = N'" + strID + "'");
			if (rsData.Rows.Count>0 )
			{
				return rsData;
			}
			else
			{
				return null;
			}
		}

	}
	public class clsTMSTransferALFromOldComp
	{
		public static DataTable LoadGridByFilter(string strEmpID,string strEmpName,string strLevel1,string strLevel2,string strLevel3,string strCompany,string strYear,string sStatus,string slanguage,System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			//Lay gia tri cua HeaderEmpSearch			
			return clsCommon.GetDataTable("TS_spfrmOLDCOMPANYAL @Activity='LoadGridByFilter',@EmpID='" + strEmpID + "',@EmpName=N'" + strEmpName 
				+ "', @LSLevel1ID='" + strLevel1 + "', @LSLevel2ID='" + strLevel2 + "',@LSLevel3ID='" + strLevel3				
				+ "', @LSCompanyID='" + strCompany + "', @Year='" + strYear + "',@Status='" + sStatus + "',@languageID='" + slanguage + "',@UserGroupID='" + sAccountLogin + "'");
		}
		public static DataRow GetDataByID(string strEmpID)
		{
			return clsCommon.GetDataRow("TS_spfrmOLDCOMPANYAL @Activity='GetDataByID',@EmpID='" + strEmpID + "'");
		}
		public static string ImpactDB(string sActivity, string sName,string sEmpID,string sConvertDays,string sConvertDate,string sEffDate,string sNote,string sLanguageID )
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
				cmd.CommandText = sName;				
												
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 50).Value = sActivity;						
				cmd.Parameters.Add("@LanguageID", SqlDbType.NVarChar,2).Value = sLanguageID;				
				cmd.Parameters.Add("@EmpID", SqlDbType.VarChar, 12).Value = sEmpID;
				cmd.Parameters.Add("@ConvertDays", SqlDbType.Decimal).Value = Decimal.Parse(sConvertDays);				
				cmd.Parameters.Add("@ConvertDate", SqlDbType.NVarChar,12).Value = sConvertDate;				
				cmd.Parameters.Add("@EffDate", SqlDbType.NVarChar,12).Value = sEffDate;								
				if (sNote.Equals(""))
					cmd.Parameters.Add("@Note", SqlDbType.NVarChar,200).Value = DBNull.Value;				
				else
					cmd.Parameters.Add("@Note", SqlDbType.NVarChar,200).Value = sNote;				
				cmd.Parameters.Add("@ReturnMess", SqlDbType.NVarChar,500).Direction = ParameterDirection.InputOutput;
						
				cmd.ExecuteNonQuery();	
				if (strReturn != "")
					strReturn = strReturn + "," +  cmd.Parameters["@ReturnMess"].Value.ToString();	
				else
					strReturn = cmd.Parameters["@ReturnMess"].Value.ToString();											               
				
				
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
				return exp.ToString();
			}
		}
	}
	public class clsTMSShiftAssign
	{
		public static void LoadComboShift(DropDownList pControl, string strTextField)
		{
			clsCommon.LoadDropDownListControl(pControl,"sp_GetDataCombo @TableName='TMS_tblShift',@Fields='ShiftID," + strTextField + " as Name'","ShiftID","Name",true);
		}
		public static DataTable GetDataAll()
		{
			return clsCommon.GetDataTable("TMS_spfrmShiftAssign @Activity = 'GetAll'");
		}

		public static DataTable getDataByID(string strID)
		{
			DataTable rsData = new DataTable();
			rsData = clsCommon.GetDataTable("TMS_spfrmShiftAssign 'Getdata_byID', N'" + strID  +"'");
			return rsData;
		}
	}
	public class clsTMSOTRegistration
	{
		public static DataTable GetListToRegisterOT(string sLSLevel1Code, string sLSLevel2Code,string sLSLevel3Code,string sEmpID,string sDateID,string sTimeIn,string sTimeOut )
		{
			return clsCommon.GetDataTable("sp_TMS_tblOTRegistration 'GetListToRegisterOT',@Level1ID=N'" 
				+ sLSLevel1Code + "',@Level2ID=N'" + sLSLevel2Code 
				+ "', @Level3ID=N'" + sLSLevel2Code + "',@EmpID='" + sEmpID 
				+ "',@DateID='" + sDateID + "',@TimeIn='" + sTimeIn
				+ "',@TimeOut='" + sTimeOut + "'");
		}
		public static void SaveData( DataGrid dtgWorkPoint,string sDateOT ,string sAccountLogin )
		{
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "sp_TMS_tblOTRegistration";
   
				CheckBox obj = new CheckBox();
				for (int i = 0;i<= dtgWorkPoint.Items.Count - 1;i++)
				{
					obj = (CheckBox) dtgWorkPoint.Items[i].FindControl("chkRegister");
					if (obj.Enabled)
					{			
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Action", SqlDbType.NVarChar, 50).Value = "AddNew";						
						cmd.Parameters.Add("@EmpID", SqlDbType.VarChar, 12).Value = dtgWorkPoint.Items[i].Cells[0].Text.Trim();
						cmd.Parameters.Add("@DateID", SqlDbType.VarChar, 12).Value = sDateOT;
						if (((TextBox)dtgWorkPoint.Items[i].FindControl("TimeIn")).Text.Trim() != "")
							cmd.Parameters.Add("@TimeIn_Reg", SqlDbType.NVarChar,12).Value = ((TextBox)dtgWorkPoint.Items[i].FindControl("TimeIn")).Text.Trim();
						else
							cmd.Parameters.Add("@TimeIn_Reg", SqlDbType.NVarChar,12).Value = DBNull.Value;
						if (((TextBox)dtgWorkPoint.Items[i].FindControl("TimeOut")).Text.Trim() != "")
							cmd.Parameters.Add("@TimeOut_Reg", SqlDbType.NVarChar,12).Value = ((TextBox)dtgWorkPoint.Items[i].FindControl("TimeOut")).Text.Trim();
						else
							cmd.Parameters.Add("@TimeOut_Reg", SqlDbType.NVarChar,12).Value = DBNull.Value;
						if (((TextBox)dtgWorkPoint.Items[i].FindControl("Note")).Text.Trim() != "")
							cmd.Parameters.Add("@RegisterNote", SqlDbType.NVarChar,200).Value = ((TextBox)dtgWorkPoint.Items[i].FindControl("Note")).Text.Trim();
						else
							cmd.Parameters.Add("@RegisterNote", SqlDbType.NVarChar,200).Value = DBNull.Value;
						if (sAccountLogin != "")
							cmd.Parameters.Add("@EmpRegister", SqlDbType.NVarChar,12).Value = clsTMSMachine.GetEmpIDByAccountLogin(sAccountLogin);
						else
							cmd.Parameters.Add("@EmpRegister", SqlDbType.NVarChar,12).Value = DBNull.Value;
						cmd.Parameters.Add("@IsRegister", SqlDbType.NVarChar,12).Value = obj.Checked?1:0;
						cmd.ExecuteNonQuery();											
					}                
				}
				// Giải phóng object
				obj.Dispose();
				cmd.Transaction.Commit();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();					
			}
			catch  (Exception exp)
			{				
				cmd.Transaction.Rollback();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				return;
			}
		}
	}
	public class clsTMSInputData
	{
		public static DataTable getCountEmp(string sEmpID, string sEmpName, System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			DataTable dtData= clsCommon.GetDataTable("HR_clsCommon @Activity='GetEmpList',@EmpCode=N'" + sEmpID + "',@EmpName=N'" + sEmpName + "',@UserGroupID=N'" + sAccountLogin + "',@TMS=1");
			return dtData;
		}
	}

}
