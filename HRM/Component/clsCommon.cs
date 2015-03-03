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
//using MyDropDownList;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using iHRPCore.HRComponent;
using EeekSoft.Web;
using ExportTechnologies.WebControls.RTE;


namespace iHRPCore.Com
{
	/// <summary>
	/// Creater: 
	/// Create Date: 
	/// Purpose: for common functions, procedures
	/// </summary>	
	public class clsCommon
	{

		private static SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
		public static SqlTransaction SQLTransaction;
		//public static CrystalDecisions.Web.CrystalReportViewer CrystalReportViewer2;
		//public static ReportDocument crReportDocument = new ReportDocument();
		//public static ConnectionInfo crConnectionInfo = new ConnectionInfo();
	
		
		//Thêm nhân viên theo mã tự động hay không
		public static bool AutoIncrease = false;
		
		//Phan quyen
		public static bool blnFRun = false;//Quyền Xem
		public static bool blnFAdd = false;//Quyền thêm mới
		public static bool blnFEdit = false;//Quyền sửa đổi
		public static bool blnFDel = false;//Quyền xoá
		public static bool blnFApp = false;//Quyền chấp thuận
		public static bool blnFAdm = false;//Quyền Admin
		public static bool blnFValid = false;//Quyền thực thi
		public static bool blnFSpv = false;//Quyền quản lý
		public static bool blnRightModule = false;//Quyền admin trên module
		public static string pstrModuleID = "";//Mã phân hệ

		public static string strControl;
		public static string App_ServerName = ConfigurationSettings.AppSettings["App_ServerName"].Trim();
		public static string App_DatabaseName = ConfigurationSettings.AppSettings["App_DatabaseName"].Trim();
		public static string App_UserName = ConfigurationSettings.AppSettings["App_UserName"].Trim();
		public static string App_Password = ConfigurationSettings.AppSettings["App_Password"].Trim();
		
		public static string UploadFile(HtmlInputFile pControl,Page page, string strServerFolder, string strExtension)
		{
			if(Path.GetExtension(pControl.Value).ToUpper().Trim() != strExtension.ToUpper().Trim())
			{
				return "File phải được định dạng là file " + strExtension;
			}
			string strFiletmp="";
			try 
			{
				if (pControl.Value != "")
				{
					strFiletmp = pControl.Value.Trim();
					strFiletmp = Path.GetFileName(strFiletmp);
					

					if (File.Exists(page.Server.MapPath(".") + strServerFolder + strFiletmp))
					{
						return "File đã được tải. Vui lòng chọn file khác!";
					}
					pControl.PostedFile.SaveAs(page.Server.MapPath(".") + strServerFolder + strFiletmp);
					System.IO.File.SetAttributes(page.Server.MapPath(".") + strServerFolder + strFiletmp, System.IO.FileAttributes.Normal) ;
				}
				else
				{
					return "Vui lòng chọn file!";
				}				
			}
			catch (Exception exp)
			{
				string strErr = exp.Message;
				return "Đường dẫn chứa file quét thẻ không hợp lệ. Vui lòng xem lại.";
			}
			return "";
		}
		
        public static string reminderContract(string sUserGroupID)
        {
            string sReturn_value="";
            DataTable dtData= clsCommon.GetDataTable("HR_spfrmContractExtend @Activity='ReminderContract',@UserGroupID='" + sUserGroupID + "'");
			
            if (dtData.Rows.Count>0)
            {
                int iReminderType = int.Parse(dtData.Rows[0]["ReminderType"].ToString());
                int isShowReminder = int.Parse(dtData.Rows[0]["isShowReminder"].ToString());
                if (isShowReminder==1)
                {
                    sReturn_value=clsChangeLang.getStringAlert("0111","VN");
                    clsCommon.sExecuteCommandTextTrans("HR_spfrmContractExtend @Activity='UpdateStatus'");
                }
            }
            return sReturn_value;
        }

		public static void SetConnectionInfo( ReportDocument objReport)
		{
			if ( objReport ==  null) return;
        
			ConnectionInfo crConnectionInfo = new ConnectionInfo();
			Database crDatabase;
			Tables crTables ;
			TableLogOnInfo crTableLogOnInfo;
			
			crDatabase = objReport.Database;
			crTables= crDatabase.Tables;
			crConnectionInfo.ServerName = clsCommon.App_ServerName;
			crConnectionInfo.DatabaseName = clsCommon.App_DatabaseName;
			crConnectionInfo.UserID = clsCommon.App_UserName;
			crConnectionInfo.Password = clsCommon.App_Password;
			
			///-------------------------------------
			foreach (CrystalDecisions.CrystalReports.Engine.Table rptTable in crTables)
			{
				crTableLogOnInfo = rptTable.LogOnInfo;
				crTableLogOnInfo.ConnectionInfo = crConnectionInfo;
				rptTable.ApplyLogOnInfo(crTableLogOnInfo);
				rptTable.Location = rptTable.Location.Substring(rptTable.Location.LastIndexOf(".") + 1);
			}			
		}


		/// <summary>
		/// Params: string pstrSQl
		/// Purpose: Exce from SQL string to string (Rerurn pstrSQL)
		/// </summary>
		public static string Exc_CommandText(string pstrSQL)
		{
			SqlCommand cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = pstrSQL;
				cmd.CommandTimeout = 20000;
				cmd.ExecuteNonQuery();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();

				return "";
			}
			catch(Exception exp)
			{                
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();

				string strErr = exp.Message;
				return strErr;
			}
		}
		public static string Exc_CommandText_(string pstrSQL)
		{
			SqlCommand cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = pstrSQL;
				cmd.CommandTimeout = 20000;
				//cmd.Parameters["@Result"].Direction = ParameterDirection.Output;
				//cmd.ExecuteNonQuery();				
				string res=(string) cmd.ExecuteScalar();
				//object result = cmd.Parameters["@Result"].Value;
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();	
				return res==null?"":res;//result.ToString();
			}
			catch(Exception exp)
			{                
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();

				string strErr = exp.Message;
				return strErr;
			}
		}
		//
		/// <summary>
		/// Params: string Stored Proc
		/// Purpose: Execute command text using Transaction 
		/// </summary>
		public static string sExecuteCommandTextTrans(string pstrSQL)
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
			try
			{
				cmd.ExecuteNonQuery();
				sqlTran.Commit();
				return "";
			}
			catch (Exception exp)
			{
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

		public static DataSet GetDataSet(string pstrSQL)

		{

			
			/* 20/05: TrucCM bo thay bang SQLHelper Microsoft */

			SqlCommand cmd = new SqlCommand();

			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);

			DataSet rsData = new DataSet();

			SqlDataAdapter adpAdapter = new SqlDataAdapter();

			try

			{

				SQLconn.Open();

				cmd.Connection = SQLconn;

				cmd.CommandType = CommandType.Text;

				cmd.CommandText = pstrSQL;

				cmd.CommandTimeout = 20000;


				adpAdapter.SelectCommand = cmd; 

				adpAdapter.Fill(rsData);

				cmd.Dispose();

				SQLconn.Close();

				SQLconn.Dispose();

				adpAdapter.Dispose();

				return rsData;

			}

			catch(Exception exp)

			{ 

				cmd.Dispose();

				SQLconn.Close();

				SQLconn.Dispose();

				adpAdapter.Dispose();

				string strErr = exp.Message;

				return null;

			}


		}

		/// <summary>
		/// Params: string pstrSQl
		/// Purpose: get Records from SQL string to Datatable
		/// </summary>
		public static DataTable GetDataTable(string pstrSQL)
		{
			SqlCommand cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			DataTable rsData = new DataTable();
			SqlDataAdapter adpAdapter = new SqlDataAdapter();

			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = pstrSQL;
				cmd.CommandTimeout = 20000;
			
				adpAdapter.SelectCommand = cmd;				
				adpAdapter.Fill(rsData);

				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				adpAdapter.Dispose();
				return rsData;
			}
			catch(Exception exp)
			{                
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				adpAdapter.Dispose();
				string strErr = exp.Message;
				return null;
			}
		}
		/// <summary>
		/// Params: string pstrSQl
		/// Purpose: get Records from SQL string to Datatable
		/// </summary>
		public static DataTable GetDataTableHasID(string pstrSQL)
		{
			SqlCommand cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			DataTable rsData = new DataTable();
			SqlDataAdapter adpAdapter = new SqlDataAdapter();
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = pstrSQL;
				cmd.CommandTimeout = 20000;
			
				adpAdapter.SelectCommand = cmd;
				DataColumn cl = new DataColumn("No");				
				cl.AutoIncrement = true;
				cl.AutoIncrementSeed = 1;
				rsData.Columns.Add(cl);
				adpAdapter.Fill(rsData);

				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				adpAdapter.Dispose();
				return rsData;
			}
			catch(Exception exp)
			{                
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				adpAdapter.Dispose();
				string strErr = exp.Message;
				return null;
			}
		}
		/// <summary>
		/// Params: string pstrSQl
		/// Purpose: get Records from SQL string to Datatable
		/// </summary>
		public static DataTable GetDataTableTimeOut(string pstrSQL)
		{
			SqlCommand cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			DataTable rsData = new DataTable();
			SqlDataAdapter adpAdapter = new SqlDataAdapter();
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = pstrSQL;
				cmd.CommandTimeout = 8000;
			
				adpAdapter.SelectCommand = cmd;
				DataColumn cl = new DataColumn("No");				
				cl.AutoIncrement = true;
				cl.AutoIncrementSeed = 1;
				rsData.Columns.Add(cl);
				adpAdapter.Fill(rsData);

				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				adpAdapter.Dispose();
				return rsData;
			}
			catch(Exception exp)
			{                
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				adpAdapter.Dispose();
				string strErr = exp.Message;
				return null;
			}
		}
		/// <summary>
		/// creater : thanhnd
		/// CreateDate : 05/05/04			
		/// </summary>
		/// <param name="pCommandText">command string</param>
		/// <returns>tra ve mot datarow, neu khong co du lieu tra ve null</returns>
		
		public static DataRow GetDataRow(string pCommandText)
		{	
			try
			{
				DataTable  dt = new DataTable();
				DataRow mRow;
				dt = GetDataTable(pCommandText);
				if(dt.Rows.Count != 0)
				{
					mRow = dt.Rows[0];					
				}
				else
				{
					mRow = null;
				}
				dt.Dispose();				
				return mRow;
			}
			catch
			{
				return null;
			}
		}
		public static int CheckSupLikeLM(string sEmpID)
		{
			try
			{
				DataRow drData= clsCommon.GetDataRow("HR_clsCommon @Activity='CheckSupLikeLM',@EmpID='" + sEmpID + "'");
				return int.Parse(drData[0].ToString());
			}
			catch(Exception ex)
			{
				return 0;
			}
		}
		public static int CheckDeptHead(string sEmpID)
		{
			try
			{
				DataRow drData= clsCommon.GetDataRow("HR_clsCommon @Activity='CheckDeptHead',@EmpID='" + sEmpID + "'");
				return int.Parse(drData[0].ToString());
			}
			catch(Exception ex)
			{
				return 0;
			}
		}
		public static void setGridPageIndex( DataGrid dtgList)
		{
			if((dtgList.Items.Count % dtgList.PageSize == 1) && (dtgList.CurrentPageIndex == dtgList.PageCount - 1) && (dtgList.CurrentPageIndex != 0))
			{
				dtgList.CurrentPageIndex = dtgList.CurrentPageIndex -1;
			}
		}
		public static string ConvertDMYtoMDY(string pDDMMYY)
		{
			string temp_Date = pDDMMYY.Trim();
			string mstr_ShortDate = temp_Date.Substring(0,temp_Date.LastIndexOf("/")+5) ;
			return mstr_ShortDate.Substring(mstr_ShortDate.IndexOf("/")+1,mstr_ShortDate.LastIndexOf("/")-mstr_ShortDate.IndexOf("/")-1) + "/" + mstr_ShortDate.Substring(0,mstr_ShortDate.IndexOf("/")) + "/" +  mstr_ShortDate.Substring(mstr_ShortDate.LastIndexOf("/")+1,mstr_ShortDate.Length-mstr_ShortDate.LastIndexOf("/")-1);			
		}			
		public static string ConvertDMYtoMMMddyyyy(string pDDMMYY)
		{
			System.DateTime mDateID = new System.DateTime();
			mDateID = Convert.ToDateTime(ConvertDMYtoMDY(pDDMMYY));
			
			return mDateID.ToString("MMM dd yyyy");
		}
		public static string ConvertMDYtoDMY(object pMMDDYY)
		{
			if(pMMDDYY.ToString().Trim() != "")
			{
				return Convert.ToDateTime(pMMDDYY.ToString().Trim()).ToString("dd/MM/yy");
			}
			else
				return "";
		}
		public static string ConvertMDYtoDMY2(object pMMDDYY)
		{
			if(pMMDDYY.ToString().Trim() != "")
			{
				return Convert.ToDateTime(pMMDDYY.ToString().Trim()).ToString("dd/MM/yyyy");
			}
			else
				return "";
		}

		public static string ConvertDMYtoMDY(object pDDMMYY)
		{
			if(pDDMMYY.ToString().Trim() != "")
			{
				return Convert.ToDateTime(pDDMMYY.ToString().Trim()).ToString("MM/dd/yy");
			}
			else
				return "";
		}
		public static string ConvertDMYtoMDY2(object pDDMMYY)
		{
			if(pDDMMYY.ToString().Trim() != "")
			{
				return Convert.ToDateTime(pDDMMYY.ToString().Trim()).ToString("MM/dd/yyyy");
			}
			else
				return "";
		}
		public static string ConvertDMYtoDDMMYYYY(object pDDMMYY)
		{
			if(pDDMMYY.ToString().Trim() != "")
			{
				return Convert.ToDateTime(pDDMMYY.ToString().Trim()).ToString("dd-MMM-yyyy");
			}
			else
				return "";
		}
		public static string ConvertDDMMYYYYtoDMY(object pDDMMMYYYY)
		{
			if(pDDMMMYYYY.ToString().Trim() != "")
			{
				return Convert.ToDateTime(pDDMMMYYYY.ToString().Trim()).ToString("dd/MM/yyyy");
			}
			else
				return "";
		}
		/// <summary>
		/// tra ve gia tri string cua fieldname duoc dua vao, neu khong co se tra ve chuoi rong
		/// creater : thanhnd
		/// CreateDate : 05/05/04			
		/// </summary>
		/// <param name="pSQLCommand">Command string</param>
		/// <param name="pFieldReturn">Ten cot lay gia tri traa ve</param>
		/// <returns>tra ve gia tri string cua fieldname duoc dua vao, neu khong co se tra ve chuoi rong</returns>
		public static string LookUpTable(string pSQLCommand, string pFieldReturn)
		{
			try
			{
				DataRow dtRow;
				dtRow = GetDataRow(pSQLCommand);
				if(dtRow != null)
				{
					return Convert.ToString(dtRow[pFieldReturn]).Trim();
				}
				return "";
			}
			catch(SqlException Ex)
			{
				return "";
			}
		}
		
		/// <summary>
		/// Tra ve gia tri lay tu bang tham so
		/// creater : khanhmt
		/// CreateDate : 21 Jun 2006
		/// </summary>
		/// <param name="pParamName">Tham so can lay gia tri</param>
		/// <returns>tra ve gia tri string cua fieldname duoc dua vao, neu khong co se tra ve chuoi rong</returns>
		public static string GetSYSParamValue(string pParamName)
		{
			try
			{
				string strSql = "SYS_spParameters @ParamName = '" + pParamName + "'";
				string pParamValue = clsCommon.LookUpTable(strSql, "ParamValue");
				return Convert.ToString(pParamValue).Trim();
			}
			catch(SqlException Ex)
			{
				return "";
			}
		}		

		/// <summary>
		/// LOAD YEAR TO COMBOBOX
		/// </summary>
		/// <param name="pCtrlName">TEN CONTROL</param>
		/// <param name="pMinYear">BAT DAU TU NAM</param>
		/// <param name="pMaxYear">DEN NAM</param>
		public static void LoadYearToList(DropDownList pCtrlName,int pMinYear,int pMaxYear)
		{
			for(int i=pMinYear;i<=pMaxYear;i++)
			{
				pCtrlName.Items.Add(new ListItem(Convert.ToString(i).Trim(),Convert.ToString(i).Trim()));
			}				
		}
		/// <summary>
		/// creater : thanhnd
		/// CreateDate : 05/05/04
		/// Load toan bo cac thang trong nam vao comboxbox
		/// </summary>
		/// <param name="pCtrlName">Dropdownlist Name</param>
		public static void LoadMonthToList(DropDownList pCtrlName)		
		{
			pCtrlName.Items.Add(new ListItem("1", "01"));
			pCtrlName.Items.Add(new ListItem("2", "02"));
			pCtrlName.Items.Add(new ListItem("3", "03"));
			pCtrlName.Items.Add(new ListItem("4", "04"));
			pCtrlName.Items.Add(new ListItem("5", "05"));
			pCtrlName.Items.Add(new ListItem("6", "06"));
			pCtrlName.Items.Add(new ListItem("7", "07"));
			pCtrlName.Items.Add(new ListItem("8", "08"));
			pCtrlName.Items.Add(new ListItem("9", "09"));
			pCtrlName.Items.Add(new ListItem("10", "10"));
			pCtrlName.Items.Add(new ListItem("11", "11"));
			pCtrlName.Items.Add(new ListItem("12", "12"));
		}
		/// <summary>
		/// creater : Tanldt
		/// CreateDate : 20060223
		/// Load tuan vao comboxbox su dung cho Com
		/// </summary>
		/// <param name="pCtrlName">Dropdownlist Name</param>
		public static void LoadWeekToList(DropDownList pControl, string strLanguage)
		{
			DataTable dtb = clsCommon.GetDataTable("TS_spfrmShiftAssign @Activity='GetSelectedValue', @Language='" + strLanguage + "'");
			clsCommon.LoadDropDownListControl(pControl, "TS_spfrmShiftAssign @Activity='GetListOfWeek', @Language='" + strLanguage + "'", "Week","Week",false);
			pControl.SelectedValue = dtb.Rows[0]["Week"].ToString().Trim();
		}
		/// <summary>
		/// creater : Tanldt
		/// CreateDate : 20060223
		/// Load tuan vao comboxbox su dung cho Emp
		/// </summary>
		/// <param name="pCtrlName">Dropdownlist Name</param>
		public static void LoadWeekToListForEmp(DropDownList pControl, string strLanguage)
		{
			DataTable dtb = clsCommon.GetDataTable("TS_spfrmShiftAssign @Activity='GetSelectedValue', @Language='" + strLanguage + "'");
			clsCommon.LoadDropDownListControl(pControl, "TS_spfrmShiftAssign @Activity='GetListOfWeekEmp', @Language='" + strLanguage + "'", "Week","Week",false);
			pControl.SelectedValue = dtb.Rows[0]["Week"].ToString().Trim();
		}
		/// <summary>
		/// creater : Tanldt
		/// CreateDate : 20060223
		/// Load tuan vao comboxbox
		/// </summary>
		/// <param name="pCtrlName">Dropdownlist Name</param>
		public static void LoadWeekToList(DropDownList pCtrlName)		
		{
			System.DateTime today = 
				new System.DateTime(System.DateTime.Today.Ticks);
			System.DateTime Dautuan = new System.DateTime();
			System.DateTime Cuoituan = new System.DateTime();
			System.DateTime ThangTuan = new System.DateTime();

			
			int iDay = today.Day;
			ThangTuan = today.AddDays(7*4);
			//int iDauTuan;
			//int iCuoiTuan;
			string strDate;
			string DayOfWeek = today.DayOfWeek.ToString();
			int iDayofWeek = GetThucuatuan(DayOfWeek);

			//			double i;
			//			i = iDay - 30;
			for (int i=0; i < 60;i+=7)
			{
				Dautuan = ThangTuan.AddDays(-(iDayofWeek- 2) - i);
				Cuoituan = Dautuan.AddDays(6);

				strDate = Dautuan.Date.ToString("dd/MM/yyyy") + "->" + Cuoituan.Date.ToString("dd/MM/yyyy");
				pCtrlName.Items.Add(new ListItem(strDate, strDate));
			}
		}
		public static string LoadHRInfo()
		{
			string return_value="";
			DataRow drData= clsCommon.GetDataRow("HR_clsCommon @Activity='LoadHRInfo'");
			if (drData!=null)
			{
				return_value=drData["HRManagerEmpID"].ToString();
			}			
			return return_value;
		}
		public static string LoadPRInfo()
		{
			string return_value="";
			DataRow drData= clsCommon.GetDataRow("HR_clsCommon @Activity='LoadPRInfo'");
			if (drData!=null)
			{
				return_value=drData["PRManagerEmpID"].ToString();
			}			
			return return_value;
		}
		public static string LoadWeekFocus()
		{
			System.DateTime today = 
				new System.DateTime(System.DateTime.Today.Ticks);
			System.DateTime Dautuan = new System.DateTime();
			System.DateTime Cuoituan = new System.DateTime();
			
			int iDay = today.Day;
			string strDate;
			string DayOfWeek = today.DayOfWeek.ToString();
			int iDayofWeek = GetThucuatuan(DayOfWeek);

		
			Dautuan = today.AddDays(-(iDayofWeek- 2));
			Cuoituan = Dautuan.AddDays(6);

			strDate = Dautuan.Date.ToString("dd/MM/yyyy") + "->" + Cuoituan.Date.ToString("dd/MM/yyyy");
			return strDate;
		}
		public static string LoadManagerInfo(string sEmpID)
		{
			string return_value="";
			DataRow drData= clsCommon.GetDataRow("HR_clsCommon @Activity='LoadManagerInfo',@EmpID='" + sEmpID + "'");
			if (drData!=null)
			{
				return_value=drData["LineManagerEmpID"].ToString();
			}			
			return return_value;
		}

		public static string LoadManagerEmail(string sEmpID)
		{
			string return_value="";
			DataRow drData= clsCommon.GetDataRow("LS_spfrmMAILTEMPLATE @Activity='LoadManagerInfo',@EmpID='" + sEmpID + "'");
			if (drData!=null)
			{
				return_value=drData["LineManagerEmail"].ToString();
			}			
			return return_value;
		}

		public static string LoadLineManagerID(string sEmpID)
		{
			string return_value="";
			DataRow drData= clsCommon.GetDataRow("LS_spfrmMAILTEMPLATE @Activity='LoadManagerInfo',@EmpID='" + sEmpID + "'");
			if (drData!=null)
			{
				return_value=drData["LineManagerEmpID"].ToString();
			}			
			return return_value;
		}

		public static string LoadSupervisorEmail(string sEmpID)
		{
			string return_value="";
			DataRow drData= clsCommon.GetDataRow("LS_spfrmMAILTEMPLATE @Activity='LoadSupervisorInfo',@EmpID='" + sEmpID + "'");
			if (drData!=null)
			{
				return_value=drData["SupervisorEmail"].ToString();
			}			
			return return_value;
		}

		public static string LoadHREmail(string sEmpID)
		{
			string return_value="";
			DataRow drData= clsCommon.GetDataRow("LS_spfrmMAILTEMPLATE @Activity='LoadHRInfo',@EmpID='" + sEmpID + "'");
			if (drData!=null)
			{
				return_value=drData["HREmail"].ToString();
			}			
			return return_value;
		}

		public static int GetThucuatuan(string strinput)
		{
			switch(strinput) 
			{
				case "Sunday":
					return 1;
				case "Monday":
					return 2;
				case "Tuesday":
					return 3;
				case "Wednesday":
					return 4;
				case "Thursday":
					return 5;
				case "Friday":
					return 6;
				case "Saturday":
					return 7;
			}
			return 0;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pValue"></param>
		/// <returns></returns>
		public static int SafeDataInteger(Object pValue)
		{
			if ( (pValue is DBNull))
			{
				return 0;
			}
			else if(Convert.ToString(pValue) == "")
			{
				return 0;
			}				
			return Convert.ToInt32(pValue);
		}
		/// <summary>
		/// mo mot popup window ngay tren server-side
		/// </summary>
		/// <param name="pPage">Page goi ham nay</param>
		/// <param name="pURL">Dia chi muon mo</param>
		/// <param name="pTitle">Tieu de page</param>
		public static void OpenNewWindow(System.Web.UI.Page pPage,string pURL,string pTitle)
		{
			string strScript = "<script language=JavaScript>";				
			strScript += "window.open('" + pURL + "','" + pTitle + "')";
			strScript += "</script>";
			if (!pPage.IsStartupScriptRegistered("clientScript"))
				pPage.RegisterStartupScript("clientScript", strScript);			
		}
		public static void OpenNewWindowPopup(System.Web.UI.Page pPage,string pURL,string pTitle)
		{
			string strScript = "<script language=JavaScript>";				
			strScript += "ShowDialog('" + pURL + "','" + pTitle + "')";
			strScript += "</script>";
			if (!pPage.IsStartupScriptRegistered("clientScript"))
				pPage.RegisterStartupScript("clientScript", strScript);			
		}
		public static void ShowMsgBox(Literal pliteral,string Message)
		{
			pliteral.Text = "alert('" + Message + "')";
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
		/// Show message to DataGrid
		/// Tanldt
		/// </summary>
		/// <param name="pPage">Trang goi ham</param>
		/// <param name="pMessage">Noi dung message</param>
		public static void ShowMessageToDataGrid(string strErrorID,string strCssClass, string lblErrorDtg,DataGrid dtgGrid)
		{				
			string[] arrID = strErrorID.Trim().Split('$');
			for(int k=0;k<arrID.Length-1;k++)
			{
				string strErr = arrID.GetValue(k).ToString().Trim();
				string[] arrIDs = strErr.Trim().Split('@');
					
				string strIDErr = arrIDs.GetValue(0).ToString().Trim();
				string strMessageErr = arrIDs.GetValue(1).ToString().Trim();

				dtgGrid.Items[Convert.ToInt16(strIDErr)].CssClass = strCssClass;
				((Label)dtgGrid.Items[Convert.ToInt16(strIDErr)].FindControl(lblErrorDtg)).Text = strMessageErr;
			}
		}	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pValue"></param>
		/// <returns></returns>
		public static string SafeDataString(Object pValue)
		{
			if (pValue is DBNull)
			{
				return Convert.ToString(pValue);
			}
			int i;
			string str_Temp = Convert.ToString(pValue);
			string str_return="";
			for(i=0;i<str_Temp.Length;i++)
			{
				if(Convert.ToString(str_Temp[i])=="'")
				{
					str_return += "''";
				}
				else
				{
					str_return += Convert.ToString(str_Temp[i]);
				}
			}
			return str_return;//Convert.ToString(pValue);//Replace(Convert.ToString(pValue), "'", "''");				
		}
		#region Insert du lieu tu file import
		/// <summary>
		/// Tanldt
		/// 16/09/2005
		/// Insert or update data
		/// </summary>
		/// <param name="strActivity">'Save' or 'Update'</param>
		/// <param name="Page">Page</param>
		/// <param name="strProcedureName">Store Prodedure Name</param>
		/// <returns>string</returns>		
		public static string ImpactDB_ImportExcel(string strParamUserAction,Object strUserAction,string pstrFilename,string strActivity,string strProcedureName)
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
			OleDbConnection Connection = new OleDbConnection(strConn);
			Connection.Open();
			DataTable dt = Connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
			Connection.Close();
			string strName = "";
			
			if (dt.Rows.Count > 0)
			{
				strName = dt.Rows[0]["TABLE_NAME"].ToString().Trim();
			}
					
			OleDbDataAdapter myCommand = new OleDbDataAdapter("SELECT * FROM " + strName, strConn);
			//OleDbDataAdapter myCommand = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", strConn);
			DataSet myDataSet = new DataSet();
			myCommand.Fill(myDataSet, "ExcelData");
			
			DataTable dtObj = new DataTable() ;
			dtObj = GetDataTable("sp_Common @ObjName=N'" + strProcedureName + "'");
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();			
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = strProcedureName;
			int iCom = myDataSet.Tables["ExcelData"].Columns.Count;
			try
			{
				#region Phan lay Ten Parameters tu dong dau cua file Excel
				for(int i=1;i<myDataSet.Tables["ExcelData"].Rows.Count;i++)
				{
					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = strActivity.Trim();
					cmd.Parameters.Add(strParamUserAction,SqlDbType.NVarChar,12).Value = strUserAction;
					for(int j=0;j<iCom;j++)
					{
						//string[] strParam[i] = myDataSet.Tables["ExcelData"].Rows[0][i].ToString().Trim();
						string a = myDataSet.Tables["ExcelData"].Rows[0][j].ToString().Trim();
						DataRow[] iRow = dtObj.Select("columnname ='@" + myDataSet.Tables["ExcelData"].Rows[0][j].ToString().Trim() + "'");
						if(iRow.Length > 0)
						{
							SqlParameter param = new SqlParameter();
							param.ParameterName = iRow[0]["columnname"].ToString().Trim();
							param.SqlDbType = ConvertDataType(iRow[0]["datatype"].ToString().Trim());
							param.Size = SafeDataInteger(iRow[0]["size"].ToString().Trim());
							//Value
							string strValue = myDataSet.Tables["ExcelData"].Rows[i][j].ToString().Trim();

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
				}
				
				#endregion
				if (sqlTran != null ) sqlTran.Commit();
				myCommand.Dispose();				
				myDataSet.Dispose();
				return "Successful!";
			}
			catch(Exception exp)
			{
				if (sqlTran != null) sqlTran.Rollback();
				myCommand.Dispose();
				myDataSet.Dispose();
				return exp.Message;
			}
		}
		#endregion
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
		public static string ImpactDB_DataGrid(string strParamUserAction,Object strUserAction,DataGrid dtgGrid,string strSelect,string strActivity,string strProcedureName, string strListID)
		{
			string strErrorID = "";
			DataTable dtObj = new DataTable() ;
			dtObj = GetDataTable("sp_Common @ObjName=N'" + strProcedureName + "'");
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
							for(int j=0;j<arrID.Length-1;j++)
							{
								DataRow[] iRow = dtObj.Select("columnname ='@" + arrID.GetValue(j).ToString().Trim() + "'");
								if(iRow.Length > 0)
								{
									SqlParameter param = new SqlParameter();
									param.ParameterName = iRow[0]["columnname"].ToString().Trim();
									param.SqlDbType = ConvertDataType(iRow[0]["datatype"].ToString().Trim());
									param.Size = SafeDataInteger(iRow[0]["size"].ToString().Trim());
									//Value
									string strValue = dtgGrid.Items[i].Cells[j].Text.Trim();

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
						}
						catch(Exception exp)
						{
							string a = exp.Message;
							strErrorID += i.ToString() + "@" + "Có lỗi xãy ra! không thể lưu." + "$";
						}
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
				if (sqlTran != null) sqlTran.Rollback();
				return exp.Message;
			}
		}
		#endregion
		#region Import du lieu to Datagird
		/// <summary>
		/// Tanldt
		/// 16/09/2005
		/// Insert or update data
		/// </summary>
		/// <param name="dtgGrid"></param>
		/// <param name="pstrFilename"></param>
		/// <returns></returns>
		public static string ImportExcelTo_DataGrid(DataGrid dtgGrid,string pstrFilename)
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
			OleDbConnection Connection = new OleDbConnection(strConn);
			Connection.Open();
			DataTable dt = Connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
			Connection.Close();
			string strName = "";
			
			if (dt.Rows.Count > 0)
			{
				strName = dt.Rows[0]["TABLE_NAME"].ToString().Trim();
			}
					
			OleDbDataAdapter myCommand = new OleDbDataAdapter("SELECT * FROM [" + strName + "]", strConn);
			DataSet myDataSet = new DataSet();
			myCommand.Fill(myDataSet, "ExcelData");

			dt = new DataTable();
			DataRow dr;

			int iCom = myDataSet.Tables["ExcelData"].Columns.Count;

			try
			{
				for(int j=0;j<iCom;j++)
				{
					string strDataColumn = myDataSet.Tables["ExcelData"].Rows[0][j].ToString().Trim();
					dt.Columns.Add(new DataColumn(strDataColumn, typeof(string)));
				}
				for(int i=1;i<myDataSet.Tables["ExcelData"].Rows.Count;i++)
				{
					dr = dt.NewRow();
					for(int j=0;j<iCom;j++)
					{
						string strValue = myDataSet.Tables["ExcelData"].Rows[i][j].ToString().Trim();

						dr[j] = strValue;
					}
					dt.Rows.Add(dr);
				}
				DataView dv = new DataView(dt);
				dtgGrid.DataSource= dv;
				dtgGrid.DataBind();
				dtgGrid.Columns[1].Visible = false;

				myDataSet.Dispose();
				return "Successful!";
			}
			catch(Exception exp)
			{
				myDataSet.Dispose();
				return exp.Message;
			}
		}
		public static string ImportExcelTo_DataGrid(DataGrid dtgGrid,DataTable dt)
		{
			try {
				dtgGrid.DataSource= dt;
				dtgGrid.DataBind();
				dtgGrid.Columns[1].Visible = false;


				return "Successful!";
			}
			catch(Exception exp)
			{
				return exp.Message;
			}
		}
		#endregion
		/// <summary>
		/// Insert or update data
		/// </summary>
		/// <param name="strActivity">'Save' or 'Update'</param>
		/// <param name="Page">Page</param>
		/// <param name="strProcedureName">Store Prodedure Name</param>
		/// <returns>true/false</returns>		
		public static bool ImpactDB(string strEmpID,string strActivity,Control Page,string strProcedureName)
		{
			DataTable dtObj = new DataTable() ;
			dtObj = GetDataTable("sp_Common @ObjName=N'" + strProcedureName + "'");			
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = strProcedureName.Trim();
			//-------
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = strActivity.Trim();				
				//cmd.Parameters.Add("@EmpID",SqlDbType.NVarChar,12).Value = strEmpID.Trim();
				//------------
				strControl="";
				GetAllControl(Page);
				string[] arrControl = strControl.Trim().Split('$');
				//--------------
				for(int i=0;i<arrControl.Length-1;i++)
				{
					DataRow[] iRow = dtObj.Select("columnname ='@" + arrControl.GetValue(i).ToString().Trim().Substring(3) + "'");
					
					if(iRow.Length > 0)
					{
						SqlParameter param = new SqlParameter();
						param.ParameterName = iRow[0]["columnname"].ToString().Trim();
						param.SqlDbType = ConvertDataType(iRow[0]["datatype"].ToString().Trim());
						param.Size = SafeDataInteger(iRow[0]["size"].ToString().Trim());						
						string strValue = GetValueControl(Page,arrControl.GetValue(i).ToString().Trim());
						if(param.SqlDbType==SqlDbType.Real)
							strValue = strValue.Replace(",","");
						if(param.ParameterName.Trim()=="@EmpID")
							param.Value = strEmpID.Trim();
							//if(param.ParameterName.Trim()=="@CandidateCode")
							//	param.Value=strEmpID.Trim();
						else
						{
							if(strValue.Trim()=="")
								param.Value = DBNull.Value ;
							else
							{
								if(param.SqlDbType == SqlDbType.Bit)
									param.Value = (strValue=="True" || strValue=="1")?1:0; //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim()).Trim()=="True"?1:0;
								else if(param.SqlDbType == SqlDbType.Int)
									param.Value = strValue.Replace(",","");
								else
									param.Value = strValue.Replace(@"'",""); //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim());
							}
						}
						cmd.Parameters.Add(param);
					}
				}
				/*vuonglm: dung cho Loan trong PR
				cmd.Parameters.Add("@Deduction",SqlDbType.Bit,1).Value = ((CheckBox)Page.FindControl("Checkbox1")).Checked.ToString()=="True"?1:0;
				*/
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
		//cangtt -- impact trả về string
		public static string sImpactDB(string strEmpID,string strActivity,Control Page,string strProcedureName)
		{
			string sErrMess ="";
			DataTable dtObj = new DataTable() ;
			dtObj = GetDataTable("sp_Common @ObjName=N'" + strProcedureName + "'");			
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();			
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = strProcedureName.Trim();
			//-------
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = strActivity.Trim();				
				//cmd.Parameters.Add("@EmpID",SqlDbType.NVarChar,12).Value = strEmpID.Trim();
				//------------
				strControl="";
				GetAllControl(Page);
				string[] arrControl = strControl.Trim().Split('$');
				//--------------
				for(int i=0;i<arrControl.Length-1;i++)
				{
					DataRow[] iRow = dtObj.Select("columnname ='@" + arrControl.GetValue(i).ToString().Trim().Substring(3) + "'");
					if(iRow.Length > 0)
					{
						SqlParameter param = new SqlParameter();
						param.ParameterName = iRow[0]["columnname"].ToString().Trim();
						param.SqlDbType = ConvertDataType(iRow[0]["datatype"].ToString().Trim());
						param.Size = SafeDataInteger(iRow[0]["size"].ToString().Trim());						
						string strValue = GetValueControl(Page,arrControl.GetValue(i).ToString().Trim());
						if(param.SqlDbType==SqlDbType.Real)
							strValue = strValue.Replace(",","");
						if(param.ParameterName.Trim()=="@EmpID")
							param.Value = strEmpID.Trim();
						else
						{
							if(strValue.Trim()=="")
								param.Value = DBNull.Value ;
							else
							{
								if(param.SqlDbType == SqlDbType.Bit)
									param.Value = (strValue=="True" || strValue=="1")?1:0; //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim()).Trim()=="True"?1:0;
								else if(param.SqlDbType == SqlDbType.Int)
									param.Value = strValue.Replace(",","");
								else
									param.Value = strValue; //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim());
							}
						}
						cmd.Parameters.Add(param);
					}
				}
				cmd.Parameters.Add("@ReturnMess", SqlDbType.NVarChar,1000).Direction = ParameterDirection.InputOutput;
				cmd.Parameters.Add("@LanguageID", SqlDbType.NChar,2).Value = "EN";
				cmd.ExecuteNonQuery();
				sErrMess= cmd.Parameters["@ReturnMess"].Value.ToString();
				sqlTran.Commit();
				if (sErrMess!="")
					return sErrMess;
				else
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
		//vuonglm
	
		public static string sImpactDB(string strValuess,string strActivity,string arrParam,string strProcedureName)
		{
			string sErrMess ="";
			DataTable dtObj = new DataTable() ;
			dtObj = GetDataTable("sp_Common @ObjName=N'" + strProcedureName + "'");			
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();			
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = strProcedureName.Trim();
			//-------
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = strActivity.Trim();				
				//cmd.Parameters.Add("@EmpID",SqlDbType.NVarChar,12).Value = strEmpID.Trim();
				//------------
				
				//--------------
				string [] arrParams=arrParam.Trim().Split('$');
				string [] strValues=strValuess.Trim().Split('$');
				for(int i=0;i<arrParams.Length-1;i++)
				{
					DataRow[] iRow = dtObj.Select("columnname ='" + arrParams[i].ToString() + "'");
					if(iRow.Length > 0)
					{
						SqlParameter param = new SqlParameter();
						param.ParameterName = iRow[0]["columnname"].ToString().Trim();
						param.SqlDbType = ConvertDataType(iRow[0]["datatype"].ToString().Trim());
						param.Size = SafeDataInteger(iRow[0]["size"].ToString().Trim());						
						string strValue = strValues[i].ToString();
						if(param.SqlDbType==SqlDbType.Real)
							strValue = strValue.Replace(",","");
						if(param.ParameterName.Trim()=="@ProjectID")
							param.Value = strValue.Trim();
						else
						{
							if(strValue.Trim()=="")
								param.Value = DBNull.Value ;
							else
							{
								if(param.SqlDbType == SqlDbType.Bit)
									param.Value = (strValue=="True" || strValue=="1")?1:0; //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim()).Trim()=="True"?1:0;
								else if(param.SqlDbType == SqlDbType.Int)
									param.Value = strValue.Replace(",","");
								else
									param.Value = strValue; //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim());
							}
						}
						cmd.Parameters.Add(param);
					}
				}
				cmd.Parameters.Add("@ReturnMess", SqlDbType.NVarChar,1000).Direction = ParameterDirection.InputOutput;
				cmd.Parameters.Add("@LanguageID", SqlDbType.NChar,2).Value = "EN";
				cmd.ExecuteNonQuery();
				sErrMess= cmd.Parameters["@ReturnMess"].Value.ToString();
				sqlTran.Commit();
				if (sErrMess!="")
					return sErrMess;
				else
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
		//vuonglm
		/// <summary>
		/// Insert or update data
		/// </summary>
		/// <param name="strActivity">'Save' or 'Update'</param>
		/// <param name="Page">Page</param>
		/// <param name="strProcedureName">Store Prodedure Name</param>
		/// <returns>true/false</returns>		
		public static bool ImpactDB(string strParaEmpID,string strEmpID,string strActivity,Control Page,string strProcedureName)
		{
			DataTable dtObj = new DataTable();
			dtObj = GetDataTable("sp_Common @ObjName=N'" + strProcedureName + "'");			
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = strProcedureName.Trim();
			//-------
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = strActivity.Trim();
				cmd.Parameters.Add(strParaEmpID,SqlDbType.NVarChar,12).Value = strEmpID.Trim();
				//------------
				strControl="";
				GetAllControl(Page);
				string[] arrControl = strControl.Trim().Split('$');
				//--------------
				for(int i=0;i<arrControl.Length-1;i++)
				{
					DataRow[] iRow = dtObj.Select("columnname ='@" + arrControl.GetValue(i).ToString().Trim().Substring(3) + "'");
					if(iRow.Length > 0)
					{
						SqlParameter param = new SqlParameter();
						param.ParameterName = iRow[0]["columnname"].ToString().Trim();
						param.SqlDbType = ConvertDataType(iRow[0]["datatype"].ToString().Trim());
						param.Size = SafeDataInteger(iRow[0]["size"].ToString().Trim());
						string strValue = GetValueControl(Page,arrControl.GetValue(i).ToString().Trim());
						if(param.ParameterName.Trim()=="@EmpID")
							param.Value = strEmpID.Trim();
						else
						{
							if(strValue.Trim()=="")
								param.Value = DBNull.Value ;
							else
							{
								if(param.SqlDbType == SqlDbType.Bit)
									param.Value = strValue=="True"?1:0; //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim()).Trim()=="True"?1:0;
								else if(param.SqlDbType == SqlDbType.Int)
									param.Value = strValue.Replace(",","");
								else
									param.Value = strValue; //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim());
							}
						}
						cmd.Parameters.Add(param);
					}
				}
				

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
		//cangtt -- impact tra ve string
		public static string sImpactDB(string strParaEmpID,string strEmpID,string strActivity,Control Page,string strProcedureName)
		{
			string sErrMess ="";
			DataTable dtObj = new DataTable();
			dtObj = GetDataTable("sp_Common @ObjName=N'" + strProcedureName + "'");			
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = strProcedureName.Trim();
			//-------
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = strActivity.Trim();
				cmd.Parameters.Add(strParaEmpID,SqlDbType.NVarChar,12).Value = strEmpID.Trim();
				//------------
				strControl="";
				GetAllControl(Page);
				string[] arrControl = strControl.Trim().Split('$');
				//--------------
				for(int i=0;i<arrControl.Length-1;i++)
				{
					DataRow[] iRow = dtObj.Select("columnname ='@" + arrControl.GetValue(i).ToString().Trim().Substring(3) + "'");
					if(iRow.Length > 0)
					{
						SqlParameter param = new SqlParameter();
						param.ParameterName = iRow[0]["columnname"].ToString().Trim();
						param.SqlDbType = ConvertDataType(iRow[0]["datatype"].ToString().Trim());
						param.Size = SafeDataInteger(iRow[0]["size"].ToString().Trim());
						string strValue = GetValueControl(Page,arrControl.GetValue(i).ToString().Trim());
						if(param.ParameterName.Trim()=="@EmpID")
							param.Value = strEmpID.Trim();
						else
						{
							if(strValue.Trim()=="")
								param.Value = DBNull.Value ;
							else
							{
								if(param.SqlDbType == SqlDbType.Bit)
									param.Value = strValue=="True"?1:0; //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim()).Trim()=="True"?1:0;									
								else if(param.SqlDbType == SqlDbType.Int)
									param.Value = strValue.Replace(",","");
								else
									param.Value = strValue; //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim());
							}
						}
						cmd.Parameters.Add(param);
					}
				}
				cmd.Parameters.Add("@ReturnMess", SqlDbType.NVarChar,1000).Direction = ParameterDirection.InputOutput;
				cmd.Parameters.Add("@LanguageID", SqlDbType.NChar,2).Value = "VN";
				cmd.ExecuteNonQuery();				
				sErrMess= cmd.Parameters["@ReturnMess"].Value.ToString();				
				sqlTran.Commit();
				if (sErrMess!="")
					return sErrMess;
				else
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
		/// <summary>
		/// Insert or update data
		/// </summary>
		/// <param name="strActivity">'Save' or 'Update'</param>
		/// <param name="Page">Page</param>
		/// <param name="strProcedureName">Store Prodedure Name</param>
		/// <returns>true/false</returns>
		public static bool ImpactDB(string strParamUserAction,Object strUserAction, string strEmpID,string strActivity,Control Page,string strProcedureName)
		{
			DataTable dtObj = new DataTable() ;
			dtObj = GetDataTable("sp_Common @ObjName=N'" + strProcedureName + "'");			
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = strProcedureName.Trim();
			//-------
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = strActivity.Trim();				
				cmd.Parameters.Add(strParamUserAction,SqlDbType.NVarChar,12).Value = strUserAction;
				//------------
				strControl="";
				GetAllControl(Page);
				string[] arrControl = strControl.Trim().Split('$');
				//--------------
				for(int i=0;i<arrControl.Length-1;i++)
				{
					DataRow[] iRow = dtObj.Select("columnname ='@" + arrControl.GetValue(i).ToString().Trim().Substring(3) + "'");
					if(iRow.Length > 0)
					{
						SqlParameter param = new SqlParameter();
						param.ParameterName = iRow[0]["columnname"].ToString().Trim();
						param.SqlDbType = ConvertDataType(iRow[0]["datatype"].ToString().Trim());
						param.Size = SafeDataInteger(iRow[0]["size"].ToString().Trim());						
						string strValue = GetValueControl(Page,arrControl.GetValue(i).ToString().Trim());
						if(param.SqlDbType==SqlDbType.Real || param.SqlDbType==SqlDbType.Money || param.SqlDbType==SqlDbType.Decimal
							|| param.SqlDbType==SqlDbType.BigInt || param.SqlDbType==SqlDbType.Float || param.SqlDbType==SqlDbType.Int)
							strValue = strValue.Replace(",","");
						if(param.ParameterName.Trim()=="@EmpID")
							param.Value = strEmpID.Trim();
						else
						{
							if(strValue.Trim()=="")
								param.Value = DBNull.Value ;
							else
							{
								if(param.SqlDbType == SqlDbType.Bit)
									param.Value = (strValue=="True" || strValue=="1")?1:0; //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim()).Trim()=="True"?1:0;
								else if(param.SqlDbType == SqlDbType.Int)
									param.Value = strValue.Replace(",","");
								else
									param.Value = strValue; //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim());
							}
						}
						cmd.Parameters.Add(param);
					}
				}
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
		//cangtt - impact tra về string
		public static string sImpactDB(string strParamUserAction,Object strUserAction, string strEmpID,string strActivity,Control Page,string strProcedureName)
		{
			string sErrMess ="";
			DataTable dtObj = new DataTable() ;
			dtObj = GetDataTable("sp_Common @ObjName=N'" + strProcedureName + "'");			
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = strProcedureName.Trim();
			//-------
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = strActivity.Trim();				
				cmd.Parameters.Add(strParamUserAction,SqlDbType.NVarChar,12).Value = strUserAction;
				//------------
				strControl="";
				GetAllControl(Page);
				string[] arrControl = strControl.Trim().Split('$');
				//--------------
				for(int i=0;i<arrControl.Length-1;i++)
				{
					DataRow[] iRow = dtObj.Select("columnname ='@" + arrControl.GetValue(i).ToString().Trim().Substring(3) + "'");
					if(iRow.Length > 0)
					{
						SqlParameter param = new SqlParameter();
						param.ParameterName = iRow[0]["columnname"].ToString().Trim();
						param.SqlDbType = ConvertDataType(iRow[0]["datatype"].ToString().Trim());
						param.Size = SafeDataInteger(iRow[0]["size"].ToString().Trim());						
						string strValue = GetValueControl(Page,arrControl.GetValue(i).ToString().Trim());
						if(param.SqlDbType==SqlDbType.Real || param.SqlDbType==SqlDbType.Money || param.SqlDbType==SqlDbType.Decimal
							|| param.SqlDbType==SqlDbType.BigInt || param.SqlDbType==SqlDbType.Float || param.SqlDbType==SqlDbType.Int)
							strValue = strValue.Replace(",","");
						if(param.ParameterName.Trim()=="@EmpID")
							param.Value = strEmpID.Trim();
						else
						{
							if(strValue.Trim()=="")
								param.Value = DBNull.Value ;
							else
							{
								if(param.SqlDbType == SqlDbType.Bit)
									param.Value = (strValue=="True" || strValue=="1")?1:0; //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim()).Trim()=="True"?1:0;
								else if(param.SqlDbType == SqlDbType.Int)
									param.Value = strValue.Replace(",","");
								else
									param.Value = strValue; //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim());
							}
						}
						cmd.Parameters.Add(param);
					}
				}
				cmd.Parameters.Add("@ReturnMess", SqlDbType.NVarChar,1000).Direction = ParameterDirection.InputOutput;
				cmd.Parameters.Add("@LanguageID", SqlDbType.NChar,2).Value = "VN";
				cmd.ExecuteNonQuery();				
				sErrMess= cmd.Parameters["@ReturnMess"].Value.ToString();				
				sqlTran.Commit();
				if (sErrMess!="")
					return sErrMess;
				else
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

		/// <summary>
		/// Luu toan bo du lieu tren page vao bang tuong ung
		/// </summary>
		/// <param name="strActivity">Save/Update</param>
		/// <param name="Page">Page lay du lieu</param>
		/// <param name="strProcedureName">Ten store procedure dung de thuc hien activity</param>
		/// <returns>True: thanh cong, false: thuc hien co loi</returns>
		public static bool ImpactDB(string strActivity,Control Page,string strProcedureName)
		{
			DataTable dtObj = new DataTable() ;
			dtObj = GetDataTable("sp_Common @ObjName=N'" + strProcedureName + "'");			
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = strProcedureName.Trim();
			//-------
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = strActivity.Trim();				
				//------------
				strControl="";
				GetAllControl(Page);
				string[] arrControl = strControl.Trim().Split('$');
				//--------------
				for(int i=0;i<arrControl.Length-1;i++)
				{
					DataRow[] iRow = dtObj.Select("columnname ='@" + arrControl.GetValue(i).ToString().Trim().Substring(3) + "'");
					if(iRow.Length > 0)
					{
						SqlParameter param = new SqlParameter();
						param.ParameterName = iRow[0]["columnname"].ToString().Trim();
						param.SqlDbType = ConvertDataType(iRow[0]["datatype"].ToString().Trim());
						param.Size = SafeDataInteger(iRow[0]["size"].ToString().Trim());
						string strValue = GetValueControl(Page,arrControl.GetValue(i).ToString().Trim());						
						if(strValue.Trim()=="")
							param.Value = DBNull.Value ;
						else
						{
							if(param.SqlDbType == SqlDbType.Bit)
								param.Value = strValue=="True"?1:0; //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim()).Trim()=="True"?1:0;
							else if(param.SqlDbType == SqlDbType.Int)
								param.Value = strValue.Replace(",","");
							else
								param.Value = strValue; //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim());
						}
						cmd.Parameters.Add(param);
					}
				}
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
		//cangtt- impact return string
		public static string sImpactDB(string strActivity,Control Page,string strProcedureName)
		{
			string sErrMess="";
			DataTable dtObj = new DataTable() ;
			dtObj = GetDataTable("sp_Common @ObjName=N'" + strProcedureName + "'");			
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = strProcedureName.Trim();
			//-------
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = strActivity.Trim();				
				//------------
				strControl="";
				GetAllControl(Page);
				string[] arrControl = strControl.Trim().Split('$');
				//--------------
				for(int i=0;i<arrControl.Length-1;i++)
				{
					DataRow[] iRow = dtObj.Select("columnname ='@" + arrControl.GetValue(i).ToString().Trim().Substring(3) + "'");
					if(iRow.Length > 0)
					{
						SqlParameter param = new SqlParameter();
						param.ParameterName = iRow[0]["columnname"].ToString().Trim();
						param.SqlDbType = ConvertDataType(iRow[0]["datatype"].ToString().Trim());
						param.Size = SafeDataInteger(iRow[0]["size"].ToString().Trim());
						string strValue = GetValueControl(Page,arrControl.GetValue(i).ToString().Trim());						
						if(strValue.Trim()=="")
							param.Value = DBNull.Value ;
						else
						{
							if(param.SqlDbType == SqlDbType.Bit)
								param.Value = strValue=="True"?1:0; //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim()).Trim()=="True"?1:0;
							else if(param.SqlDbType == SqlDbType.Int)
								param.Value = strValue.Replace(",","");
							else
								param.Value = strValue; //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim());
						}
						cmd.Parameters.Add(param);
					}
				}
				cmd.Parameters.Add("@ReturnMess", SqlDbType.NVarChar,1000).Direction = ParameterDirection.InputOutput;
				cmd.Parameters.Add("@LanguageID", SqlDbType.NChar,2).Value = "VN";
				cmd.ExecuteNonQuery();				
				sErrMess= cmd.Parameters["@ReturnMess"].Value.ToString();				
				sqlTran.Commit();
				if (sErrMess!="")
					return sErrMess;
				else
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
		/// <summary>
		/// Update data 
		/// </summary>
		/// <param name="strKeyID"></param>
		/// <param name="strKeyValue"></param>
		/// <param name="strActivity"></param>
		/// <param name="Page"></param>
		/// <param name="strProcedureName"></param>
		/// <returns></returns>
		public static bool UpdateByKey(string strKeyID,string strKeyValue,string strActivity,Control Page,string strProcedureName)
		{
			DataTable dtObj = new DataTable();
			dtObj = GetDataTable("sp_Common @ObjName=N'" + strProcedureName + "'");
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = strProcedureName.Trim();
			//-------
			try
			{				
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = strActivity.Trim();				
				//------------
				strControl="";
				GetAllControl(Page);
				strControl +="txt" + strKeyID + "$";

				string[] arrControl = strControl.Trim().Split('$');
				//--------------
				for(int i=0;i<arrControl.Length-1;i++)
				{
					DataRow[] iRow = dtObj.Select("columnname ='@" + arrControl.GetValue(i).ToString().Trim().Substring(3) + "'");
					if(iRow.Length > 0)
					{
						SqlParameter param = new SqlParameter();
						param.ParameterName = iRow[0]["columnname"].ToString().Trim();
						param.SqlDbType = ConvertDataType(iRow[0]["datatype"].ToString().Trim());
						param.Size = SafeDataInteger(iRow[0]["size"].ToString().Trim());
						string strValue = GetValueControl(Page,arrControl.GetValue(i).ToString().Trim());						
						if(param.ParameterName.Trim()=="@"+ strKeyID.Trim())
							param.Value = strKeyValue.Trim();
						else
						{
							if(strValue.Trim()=="")
								param.Value = DBNull.Value ;
							else
							{
								if(param.SqlDbType == SqlDbType.Bit)
									param.Value = (strValue=="True" || strValue=="1")?1:0; //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim()).Trim()=="1"?true:false;
								else if(param.SqlDbType == SqlDbType.Int)
									param.Value = strValue.Replace(",","");
								else
									param.Value = strValue; //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim());
							}
						}
						cmd.Parameters.Add(param);
					}
				}
				//vuonglm
//				SqlParameter param1 = new SqlParameter();
//				param1.ParameterName = "@"+ strKeyID.Trim();
//				param1.SqlDbType = SqlDbType.NVarChar ;
//				param1.Size = 12;
//				param1.Value=strKeyValue.Trim();
//				cmd.Parameters.Add(param1);
//				cmd.Parameters.Add("@Deduction",SqlDbType.Bit,1).Value = ((CheckBox)Page.FindControl("Checkbox1")).Checked.ToString()=="True"?1:0;
				//vuonglm
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
		//cangtt - updatebyKey return string
		public static string sUpdateByKey(string strKeyID,string strKeyValue,string strActivity,Control Page,string strProcedureName)
		{
				string sErrMess="";
			DataTable dtObj = new DataTable();
			dtObj = GetDataTable("sp_Common @ObjName=N'" + strProcedureName + "'");
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = strProcedureName.Trim();
			//-------
			try
			{				
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = strActivity.Trim();				
				//------------
				strControl="";
				GetAllControl(Page);
				strControl += "txt" + strKeyID + "$";

				string[] arrControl = strControl.Trim().Split('$');
				//--------------
				for(int i=0;i<arrControl.Length-1;i++)
				{
					DataRow[] iRow = dtObj.Select("columnname ='@" + arrControl.GetValue(i).ToString().Trim().Substring(3) + "'");
					if(iRow.Length > 0)
					{
						SqlParameter param = new SqlParameter();
						param.ParameterName = iRow[0]["columnname"].ToString().Trim();
						param.SqlDbType = ConvertDataType(iRow[0]["datatype"].ToString().Trim());
						param.Size = SafeDataInteger(iRow[0]["size"].ToString().Trim());
						string strValue = GetValueControl(Page,arrControl.GetValue(i).ToString().Trim());						
						if(param.ParameterName.Trim()=="@"+strKeyID.Trim())
							param.Value = strKeyValue.Trim();
						else
						{
							if(strValue.Trim()=="")
								param.Value = DBNull.Value ;
							else
							{
								if(param.SqlDbType == SqlDbType.Bit)
									param.Value = (strValue=="True" || strValue=="1")?1:0; //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim()).Trim()=="1"?true:false;
								else if(param.SqlDbType == SqlDbType.Int)
									param.Value = strValue.Replace(",","");
								else
									param.Value = strValue; //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim());
							}
						}
						cmd.Parameters.Add(param);
					}
				}
				cmd.Parameters.Add("@ReturnMess", SqlDbType.NVarChar,1000).Direction = ParameterDirection.InputOutput;
				cmd.Parameters.Add("@LanguageID", SqlDbType.NChar,2).Value = "VN";
				cmd.ExecuteNonQuery();				
				sErrMess= cmd.Parameters["@ReturnMess"].Value.ToString();				
				sqlTran.Commit();
				if (sErrMess!="")
					return sErrMess;
				else
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
		//UPDATE <CO LUU LAI USER EDIT>
		public static bool UpdateByKey(string strParamUserAction,Object strUserAction,string strKeyID,string strKeyValue,string strActivity,Control Page,string strProcedureName)
		{
			DataTable dtObj = new DataTable();
			dtObj = GetDataTable("sp_Common @ObjName=N'" + strProcedureName + "'");
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = strProcedureName.Trim();
			//-------
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = strActivity.Trim();				
				cmd.Parameters.Add(strParamUserAction,SqlDbType.NVarChar,12).Value = strUserAction;
				//------------
				strControl="";
				GetAllControl(Page);
				strControl += "txt" + strKeyID + "$";
				string[] arrControl = strControl.Trim().Split('$');
				//--------------
				for(int i=0;i<arrControl.Length-1;i++)
				{
					DataRow[] iRow = dtObj.Select("columnname ='@" + arrControl.GetValue(i).ToString().Trim().Substring(3) + "'");
					if(iRow.Length > 0)
					{
						SqlParameter param = new SqlParameter();
						param.ParameterName = iRow[0]["columnname"].ToString().Trim();
						param.SqlDbType = ConvertDataType(iRow[0]["datatype"].ToString().Trim());
						param.Size = SafeDataInteger(iRow[0]["size"].ToString().Trim());
						string strValue = GetValueControl(Page,arrControl.GetValue(i).ToString().Trim());						
						if(param.ParameterName.Trim()=="@"+strKeyID.Trim())
							param.Value = strKeyValue.Trim();
						else
						{
							if(strValue.Trim()=="")
								param.Value = DBNull.Value ;
							else
							{
								if(param.SqlDbType == SqlDbType.Bit)
									param.Value = (strValue=="True" || strValue=="1")?1:0; //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim()).Trim()=="1"?true:false;
								else if(param.SqlDbType == SqlDbType.Int)
									param.Value = strValue.Replace(",","");
								else
									param.Value = strValue; //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim());
							}
						}
						cmd.Parameters.Add(param);
					}
				}
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
		//cangtt - updateByKey return string
		public static string sUpdateByKey(string strParamUserAction,Object strUserAction,string strKeyID,string strKeyValue,string strActivity,Control Page,string strProcedureName)
		{
			string sErrMess="";
			DataTable dtObj = new DataTable();
			dtObj = GetDataTable("sp_Common @ObjName=N'" + strProcedureName + "'");
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = strProcedureName.Trim();
			//-------
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = strActivity.Trim();				
				cmd.Parameters.Add(strParamUserAction,SqlDbType.NVarChar,12).Value = strUserAction;
				//------------
				strControl="";
				GetAllControl(Page);
				strControl += "txt" + strKeyID + "$";
				string[] arrControl = strControl.Trim().Split('$');
				//--------------
				for(int i=0;i<arrControl.Length-1;i++)
				{
					DataRow[] iRow = dtObj.Select("columnname ='@" + arrControl.GetValue(i).ToString().Trim().Substring(3) + "'");
					if(iRow.Length > 0)
					{
						SqlParameter param = new SqlParameter();
						param.ParameterName = iRow[0]["columnname"].ToString().Trim();
						param.SqlDbType = ConvertDataType(iRow[0]["datatype"].ToString().Trim());
						param.Size = SafeDataInteger(iRow[0]["size"].ToString().Trim());
						string strValue = GetValueControl(Page,arrControl.GetValue(i).ToString().Trim());						
						if(param.ParameterName.Trim()=="@"+strKeyID.Trim())
							param.Value = strKeyValue.Trim();
						else
						{
							if(strValue.Trim()=="")
								param.Value = DBNull.Value ;
							else
							{
								if(param.SqlDbType == SqlDbType.Bit)
									param.Value = (strValue=="True" || strValue=="1")?1:0; //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim()).Trim()=="1"?true:false;
								else if(param.SqlDbType == SqlDbType.Int)
									param.Value = strValue.Replace(",","");
								else
									param.Value = strValue; //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim());
							}
						}
						cmd.Parameters.Add(param);
					}
				}
				cmd.Parameters.Add("@ReturnMess", SqlDbType.NVarChar,1000).Direction = ParameterDirection.InputOutput;
				cmd.Parameters.Add("@LanguageID", SqlDbType.NChar,2).Value = "VN";
				cmd.ExecuteNonQuery();				
				sErrMess= cmd.Parameters["@ReturnMess"].Value.ToString();				
				sqlTran.Commit();
				if (sErrMess!="")
					return sErrMess;
				else
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
		public static bool DeleteListRecord(string  strStoreName,string strKeyField,SqlDbType pType, int intSize, string strListID)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = strStoreName.Trim();
			//-------
			try
			{
				string[] arrID = strListID.Trim().Split('$');
				//--------------
				for(int i=0;i<arrID.Length-1;i++)
				{
					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Delete";
					SqlParameter param = new SqlParameter();
					param.ParameterName = "@" + strKeyField.Trim();
					param.SqlDbType = pType;
					param.Size = intSize;				
					param.Value = arrID.GetValue(i).ToString().Trim();
					cmd.Parameters.Add(param);
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
		public static string sDeleteListRecord(string  strStoreName,string strKeyField,SqlDbType pType, int intSize, string strListID)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = strStoreName.Trim();
			//-------
			try
			{
				string[] arrID = strListID.Trim().Split('$');
				//--------------
				for(int i=0;i<arrID.Length-1;i++)
				{
					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Delete";
					SqlParameter param = new SqlParameter();
					param.ParameterName = "@" + strKeyField.Trim();
					param.SqlDbType = pType;
					param.Size = intSize;				
					param.Value = arrID.GetValue(i).ToString().Trim();
					cmd.Parameters.Add(param);
					cmd.ExecuteNonQuery();
				}				
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
		/// <summary>
		/// Delete hang loat record 
		/// </summary>
		/// <param name="strStoreName">Ten procedure</param>
		/// <param name="strActivity">Ten activity</param>
		/// <param name="strKeyField">Ten Param trong store (dieu kien xoa) </param>
		/// <param name="pType">Kieu du lieu cua param</param>
		/// <param name="intSize">kich thuoc param</param>
		/// <param name="strListID">chuoi cac record se bi xoa phan cach nhau bang dau ";"</param>
		/// <returns></returns>
		public static bool DeleteListRecord(string  strStoreName,string strActivity,string strKeyField,SqlDbType pType, int intSize, string strListID)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = strStoreName.Trim();
			//-------
			try
			{
				string[] arrID = strListID.Trim().Split('$');
				//--------------
				for(int i=0;i<arrID.Length-1;i++)
				{
					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = strActivity.Trim();
					SqlParameter param = new SqlParameter();
					param.ParameterName = "@" + strKeyField.Trim();
					param.SqlDbType = pType;
					param.Size = intSize;				
					param.Value = arrID.GetValue(i).ToString().Trim();
					cmd.Parameters.Add(param);
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
		/// 
		/// </summary>
		/// <param name="strTypeName"></param>
		/// <returns></returns>
		public static SqlDbType ConvertDataType(string strTypeName)
		{
			SqlDbType TypeReturn = new SqlDbType();
			switch(strTypeName.Trim().ToUpper())
			{
				case "NVARCHAR":
					TypeReturn = SqlDbType.NVarChar;
					break;
				case "DATETIME":
					TypeReturn = SqlDbType.NVarChar;
					break;
				case "VARCHAR":
					TypeReturn = SqlDbType.VarChar;
					break;
				case "BIT":
					TypeReturn = SqlDbType.Bit;
					break;
				case "INT":
					TypeReturn = SqlDbType.Int;
					break;
				case "MONEY":
					TypeReturn = SqlDbType.Money;
					break;
				case "REAL":
					TypeReturn = SqlDbType.Real;
					break;
				case "NUMERIC":
					TypeReturn = SqlDbType.Real;
					break;
				case "NUMBER":
					TypeReturn = SqlDbType.Real;
					break;
				case "SMALLINT":
					TypeReturn = SqlDbType.SmallInt;
					break;
				case "FLOAT":
					TypeReturn = SqlDbType.Float;
					break;
				default:
					TypeReturn = SqlDbType.NVarChar;
					break;
			}
			return TypeReturn;
		}
		/// <summary>
		/// GetValueControl
		/// </summary>
		/// <param name="Page"></param>
		/// <param name="strControlName"></param>
		/// <returns></returns>
		public static string GetValueControl(Control Page, string strControlName)
		{			
			string strReturnValue="";
			try
			{
				Control ctrFound = Page.FindControl(strControlName);
				if(ctrFound!=null)
				{
					
					string strType = ctrFound.GetType().ToString().Trim().Substring(ctrFound.GetType().ToString().Trim().LastIndexOf(".")+1).ToString().Trim();
					switch(strType)
					{
						case "TextBox":
							strReturnValue = ((TextBox)ctrFound).Text;
							break;
						case "DropDownList":
							strReturnValue = ((DropDownList)ctrFound).SelectedValue.Trim();
							break;
						case "RadioButtonList":
							strReturnValue = ((RadioButtonList)ctrFound).SelectedValue.Trim();
							break;
						case "CheckBox":
							strReturnValue = ((CheckBox)ctrFound).Checked.ToString();
							break;
						case "RadioButton":
							strReturnValue = ((RadioButton)ctrFound).Checked.ToString();
							break;
						case "RichTextEditor":
							strReturnValue = ((RichTextEditor)ctrFound).Text.ToString();
							break;
							//case "MyDropDownListControl":
							//	strReturnValue=((MyDropDownListControl)ctrFound).SelectedValue.Trim();
							//	break;
						case "ComboBox":
							strReturnValue=((ProgStudios.WebControls.ComboBox)ctrFound).Value.Trim();
							break;
						default :
							break;
					}
				}
			}				
			catch(Exception ex)
			{
				return "";
			}
			return strReturnValue;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pRootCtl"></param>
		public static void GetAllControl(Control pRootCtl)
		{
			try
			{				
				foreach(Control Child_ctl in pRootCtl.Controls)
				{
					if(Child_ctl.HasControls()==true)
					{
						GetAllControl(Child_ctl);
					}
					else
					{						//"HtmlInputHidden"
						string mCtlType = Child_ctl.GetType().ToString().Trim().Substring(Child_ctl.GetType().ToString().Trim().LastIndexOf(".")+1).ToString().Trim();
						if((mCtlType.ToUpper() == "TEXTBOX" || mCtlType.ToUpper() == "CHECKBOX" || mCtlType.ToUpper() == "RADIOBUTTONLIST" || mCtlType.ToUpper()=="RICHTEXTEDITOR" ||mCtlType.ToUpper() == "DROPDOWNLIST" || mCtlType.ToUpper() == "RADIOBUTTON" || mCtlType.ToUpper()=="MYDROPDOWNLISTCONTROL" || mCtlType.ToUpper()=="COMBOBOX") && (Child_ctl.ID.Trim().ToUpper()!="CHKSELECT") )
							if(Child_ctl.ID.Trim().Length > 3)
								strControl += Child_ctl.ID.Trim() + "$";
					}
				}
			}
			catch(Exception exp)
			{
				string strError = exp.Message.ToString();
			}
		}
		public static void ClearControlValue(Control pRootCtl,string sAscx)
		{
			ClearControlValue(pRootCtl);
			clsHRParameter.getHR_param(sAscx,pRootCtl);
		}
		/// <summary>
		/// Xoa toan bo gia tri tren control 
		/// Co loai tru mot so control
		/// </summary>
		/// <param name="pRootCtl">ten contrl goc</param>
		public static void ClearControlValue(Control pRootCtl)
		{
			try
			{				
				foreach(Control Child_ctl in pRootCtl.Controls)
				{
					if(Child_ctl.HasControls()==true)
					{
						ClearControlValue(Child_ctl);
					}
					else
					{
						string mCtlType = Child_ctl.GetType().ToString().Trim().Substring(Child_ctl.GetType().ToString().Trim().LastIndexOf(".")+1).ToString().Trim();
						// loai tru cac control khong can thiet
						if((mCtlType.ToUpper() == "TEXTBOX" ||mCtlType.ToUpper() == "CHECKBOX" ||mCtlType.ToUpper() == "DROPDOWNLIST" || mCtlType.ToUpper() == "RADIOBUTTON") && (Child_ctl.ID.Trim().ToUpper()!="CHKSELECT"))
						{
							if(Child_ctl.ID.Trim().ToUpper()!="CHKSHOWGRID" && Child_ctl.ID.Trim().ToUpper()!="CHKSELECT" && Child_ctl.ID.Trim().ToUpper()!="TXTEMPID" && Child_ctl.ID.Trim().ToUpper()!="TXTEMPNAME" && Child_ctl.ID.Trim().ToUpper()!="TXTCANDIDATECODE" && Child_ctl.ID.Trim().ToUpper()!="TXTCANDIDATENAME")
							{							
								switch(mCtlType.ToUpper())
								{
									case "TEXTBOX":
										((TextBox)Child_ctl).Text = "";
										break;
									case "DROPDOWNLIST":
										((DropDownList)Child_ctl).SelectedIndex = -1;
										break;
									case "CHECKBOX":									
										((CheckBox)Child_ctl).Checked = false;
										break;
									case "RADIOBUTTONLIST":
										((RadioButtonList)Child_ctl).SelectedIndex = 0;
										break;
								}
							}							
						}
					}
				}
			}
			catch(Exception exp)
			{
				string strError = exp.Message.ToString();
			}
		}
		/// <summary>		
		/// Params: string strParent
		/// Purpose: 
		/// </summary>
		public static ArrayList GetArrayFromString(string strParent)
		{
			ArrayList arr = new ArrayList();
			string[] strArr = strParent.Split(new char[]{'@'});
			for (int i=0; i<strArr.Length; i++)
			{
				if (strArr[i].Trim() != "")
					arr.Add(strArr[i].Trim());
			}
			return arr;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pCtrl"></param>
		/// <param name="pCommandText"></param>
		/// <param name="pValueField"></param>
		/// <param name="pTextField"></param>
		/// <param name="pRowBlank"></param>
		public static string LoadDropDownListControl(DropDownList pCtrl,string pCommandText,string pValueField,string pTextField,bool pRowBlank)
		{
			try
			{
				pCtrl.Items.Clear();
				DataTable dt = new DataTable();				
				dt = GetDataTable(pCommandText);
				int i;
				if(pRowBlank) 
					pCtrl.Items.Add("");				
			
				if (dt != null)
					for(i=0;i<dt.Rows.Count;i++)
					{
						/*string strText = "";
						if(dt.Rows[i]["IsUse"].ToString() == "True")
						{
							strText = SafeDataString(dt.Rows[i][pTextField]);
						}
						else
						{
							strText = SafeDataString(dt.Rows[i][pTextField]) + " - Not in use";
						}*/
						ListItem iTem = new ListItem(SafeDataString(dt.Rows[i][pTextField]), SafeDataString(dt.Rows[i][pValueField]));
						// style= "COLOR: red"
						pCtrl.Items.Add(iTem);
						pCtrl.Attributes.Add("onclick","checkCboValue(this)");
					}
				return "";
			}
			catch(Exception ex)
			{
				return ex.Message;
			}
		}
		//cangtt
		public static string LoadListBoxControl(ListBox pCtrl,DataTable dt,string pValueField,string pTextField,bool pRowBlank)
		{
			try
			{
				pCtrl.Items.Clear();				
				
				int i;
				if(pRowBlank) 
					pCtrl.Items.Add("");				
			
				if (dt != null)
					for(i=0;i<dt.Rows.Count;i++)
					{
						ListItem iTem = new ListItem(SafeDataString(dt.Rows[i][pTextField]), SafeDataString(dt.Rows[i][pValueField]));
						pCtrl.Items.Add(iTem);
					}
				return "";
			}
			catch(Exception ex)
			{
				return ex.Message;
			}
		}
		//CANGTT
		public static string LoadDropDownListControl(DropDownList pCtrl,DataTable dt,string pValueField,string pTextField,bool pRowBlank)
		{
			try
			{
				pCtrl.Items.Clear();		
				
				int i;
				if(pRowBlank) 
					pCtrl.Items.Add("");				
			
				if (dt != null)
					for(i=0;i<dt.Rows.Count;i++)
					{
						ListItem iTem = new ListItem(SafeDataString(dt.Rows[i][pTextField]), SafeDataString(dt.Rows[i][pValueField]));
						pCtrl.Items.Add(iTem);
					}
				return "";
			}
			catch(Exception ex)
			{
				return ex.Message;
			}
		}

		// HOPTD - MYDROPDOWNLIST USING DATABIND EVENTS
		public static string BindDropDownListControl(DropDownList pCtrl,string pCommandText,string pValueField,string pTextField,bool pRowBlank)
		{
			try
			{
				DataTable dt=clsCommon.GetDataTable(pCommandText);
				pCtrl.DataSource=dt;
				pCtrl.DataTextField=pTextField;
				pCtrl.DataValueField=pValueField;
				pCtrl.DataBind();
				pCtrl.Items.Insert(0,"");
				dt=null;
				return "";
			}
			catch(Exception ex)
			{ return ex.Message; }
		}
		///////////////////////////////////////////////
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pCtrl"></param>
		/// <param name="pCommandText"></param>
		/// <param name="pValueField"></param>
		/// <param name="pTextField"></param>
		/// <param name="pRowBlank"></param>
		public static string LoadDropDownListControl(HtmlSelect pCtrl,string pCommandText,string pValueField,string pTextField,bool pRowBlank)
		{
			try
			{

				DataTable dt = new DataTable();				
				dt = GetDataTable(pCommandText);
				int i;
				if(pRowBlank) 
					pCtrl.Items.Add("");				
				for(i=0;i<dt.Rows.Count;i++)
				{
					ListItem iTem = new ListItem(SafeDataString(dt.Rows[i][pTextField]), SafeDataString(dt.Rows[i][pValueField]));
					pCtrl.Items.Add(iTem);
				}
				return "";
			}
			catch(Exception ex)
			{
				return ex.Message;
			}

		}
		public static string LoadDropDownListControl(ProgStudios.WebControls.ComboBox pCtrl,string pCommandText,string pValueField,string pTextField,bool pRowBlank)
		{
			try
			{
				pCtrl.Items.Clear();
				DataTable dt = new DataTable();				
				dt = GetDataTable(pCommandText);
				int i;
				if(pRowBlank) 
					pCtrl.Items.Add("");				
			
				if (dt != null)
					for(i=0;i<dt.Rows.Count;i++)
					{
						ListItem iTem = new ListItem(SafeDataString(dt.Rows[i][pTextField]), SafeDataString(dt.Rows[i][pValueField]));
						pCtrl.Items.Add(iTem);
					}
				return "";
			}
			catch(Exception ex)
			{
				return ex.Message;
			}
		}
		public static void LoadDropDownList(DropDownList pCtrl, string pstrSQL, string pstrValueField, string pstrTextField)
		{
			try
			{
				DataTable rsData = new DataTable();
				rsData = GetDataTable(pstrSQL);

				pCtrl.Items.Clear();
				pCtrl.DataSource = rsData;

				pCtrl.DataValueField = pstrValueField;
				pCtrl.DataTextField = pstrTextField;
				pCtrl.DataBind();

				rsData.Dispose();
			}
			catch(Exception exp) 
			{
				string strError = exp.Message.ToString();
			}
		}

		/// <summary>
		/// creater : thanhnd
		/// CreateDate : 05/05/04
		/// </summary>
		/// <param name="pCtrl">Control Name</param>
		/// <param name="pCommandText">Command text</param>
		/// <param name="pValueField">Value Field Name of Radio button</param>
		/// <param name="pTextField">Text Field Name of Radio button</param>
		public static string LoadCheckBoxList(CheckBoxList pCtrl,string pCommandText,string pValueField,string pTextField)
		{
			try
			{

				DataTable dt = new DataTable();
				dt = GetDataTable(pCommandText);
				pCtrl.DataSource = dt;
				pCtrl.DataValueField = pValueField;
				pCtrl.DataTextField = pTextField;
				pCtrl.DataBind();
				return "";
			}
			catch(Exception ex)
			{
				return ex.Message;
			}

		}
		public static string FormatNumericWithSeparate(double field)
		{
			int DecimalNumber = 2;
			string DecimalChar = ".";
			string SeperateChar = ",";
			string ValueString = field.ToString();
			string ValueStringAfterDecimal = "";
			string ValueStringNew = "";
			int iLength = ValueString.Length;

			if (field < 0) 
				ValueString = ValueString.Substring(1, iLength - 1); //Bo dau tru

			int iIndexOfDecimalChar = ValueString.IndexOf(DecimalChar);
			if (iIndexOfDecimalChar != -1)
			{
				string[] mArray = ValueString.Split(new Char [] {'.'});
			
				if (mArray.Length > 1)
				{
					ValueStringAfterDecimal = DecimalChar + mArray.GetValue(1).ToString().Trim();
					if (ValueStringAfterDecimal.Length>3)
						ValueStringAfterDecimal = ValueStringAfterDecimal.Substring(0, DecimalNumber + 1); // Tinh luon dau cham
					ValueString = mArray.GetValue(0).ToString().Trim();
				}
				
				//ValueString = ValueString.Substring(0, iIndexOfDecimalChar);
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
		public static string FormatNumericNonDecimal(double field)
		{
			int DecimalNumber = 0;
			string DecimalChar = ".";
			string SeperateChar = ",";
			string ValueString = field.ToString();
			string ValueStringAfterDecimal = "";
			string ValueStringNew = "";
			int iLength = ValueString.Length;

			if (field < 0) 
				ValueString = ValueString.Substring(1, iLength - 1); //Bo dau tru

			int iIndexOfDecimalChar = ValueString.IndexOf(DecimalChar);
			if (iIndexOfDecimalChar != -1)
			{
				string[] mArray = ValueString.Split(new Char [] {'.'});
			
				if (mArray.Length > 1)
				{
					ValueStringAfterDecimal = DecimalChar + mArray.GetValue(1).ToString().Trim();
					ValueString = mArray.GetValue(0).ToString().Trim();
				}
				//ValueStringAfterDecimal = ValueString.Substring(iIndexOfDecimalChar, DecimalNumber + 1); // Tinh luon dau cham
				//ValueString = ValueString.Substring(0, iIndexOfDecimalChar);
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
	}



	//-------------------------------------------//


	public class clsChangeLang : System.Web.UI.Page
	{		
		public static string singleClick(string sFunctionname,LinkButton btnButton,Page p)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			//Confirm user action before processing.
			sb.Append("if (" + sFunctionname + "() == false) { return false; } ");
			sb.Append("if (typeof(Page_ClientValidate) == 'function') { ");
			sb.Append("if (Page_ClientValidate() == false) { return false; }} ");
			sb.Append("this.value = 'Please wait...';");
			sb.Append("this.disabled = true;");            
			sb.Append(p.GetPostBackEventReference(btnButton));
			sb.Append(";");
			return sb.ToString();
		}
		public static string getCourseName(string sCourseEmpID)
		{
			DataRow drData= clsCommon.GetDataRow("TR_spfrmTrainingReport @Activity='getCourseName',@CourseEmpID='" + sCourseEmpID + "'");
			return drData["CourseName"].ToString();
		}
		public static Control getControl(string ControlID, string ParentControl, Page p, string acxName)
		{
			Control acxHolder = p.FindControl("acxHolder");
			Control acxCtrl = acxHolder.Controls[0];
			Control ctrl = null;

			if(ParentControl == null || ParentControl == "")
			{
				ctrl = acxCtrl.FindControl(ControlID);
			}
			else
			{
				acxCtrl = acxCtrl.FindControl(ParentControl);
				ctrl = acxCtrl.FindControl(ControlID);
			}

			return ctrl;
		}

		/// <summary>
		/// Change language on pfrmpage page
		/// </summary>
		/// <param name="pfrmName">Page name</param>
		/// <param name="pclsfrm">class Page</param>
		
		public static string getEmpFromAccount(string sAccount,int iType) //0 - Name, 1- EmpID
		{
			DataRow drData= clsCommon.GetDataRow("HR_clsCommon @Activity='getEmpCodebyAccount',@UserGroupID='" + sAccount + "'");
			if (iType==0)
				return drData["EmpCode"].ToString();	
			else if (iType==1)
				return drData["UserGroupName"].ToString();	
			else
				return drData["Code"].ToString();	
		}
		public static string getEmpName_FullName(string sEmpID)
		{
			DataRow drData= clsCommon.GetDataRow("HR_clsCommon @Activity='getEmpName_FullName',@EmpID='" + sEmpID + "'");
			return drData["EmpName"].ToString();
		}
		public static string getGender_hisher(string sEmpID)
		{
			DataRow drData= clsCommon.GetDataRow("HR_clsCommon @Activity='getGender_hisher',@EmpID='" + sEmpID + "'");
			return drData["Gender"].ToString();
		}
		public static void ChangePage(string pfrmName, Page pclsfrm, string pLangID)
		{
			string str_FieldName = "Caption" + pLangID.Trim() + "Ctl";
			DataTable dtCtl = clsCommon.GetDataTable("Select * from UMS_tblCaptionfrmCtl where FormID = N'" + pfrmName.Trim() + "' and bShow=0 Order by Parent, ControlID ");
			Control mCtl = null;
			string a;
			string mCtlName ;

			for(int i=0;i<dtCtl.Rows.Count;i++)
			{	
				try
				{
					a = dtCtl.Rows[i]["ControlID"].ToString().Trim();
					mCtl = getControl(dtCtl.Rows[i]["ControlID"].ToString().Trim(),dtCtl.Rows[i]["Parent"].ToString().Trim(), pclsfrm, pfrmName);
					//mCtl = pclsfrm.FindControl(dtCtl.Rows[i]["ControlID"].ToString().Trim());
					//Control mCtl = pclsfrm.FindControl("Label1");
					//string str=dtCtl.Rows[i]["ControlID"].ToString().Trim();
					if(mCtl != null)
					{
						mCtlName = mCtl.GetType().ToString().Trim().Substring(mCtl.GetType().ToString().Trim().LastIndexOf(".")+1).ToString().Trim();					
						//string mCtlName = mCtl.GetType().ToString().Trim().Substring(mCtl.GetType().ToString().Trim().LastIndexOf(".")+1);

						switch(mCtlName)
						{
							case "Label":
								Label lbl = (Label)mCtl;
								lbl.Text = dtCtl.Rows[i][str_FieldName].ToString().Trim();							
								break;
							case "Button":
								Button btn = (Button)mCtl;
								btn.Text = dtCtl.Rows[i][str_FieldName].ToString().Trim();
								break;//
							case "RadioButtonList":
								RadioButtonList optlst = (RadioButtonList)mCtl;
								optlst.Items[Convert.ToInt32(dtCtl.Rows[i]["ColumnIndex"])].Text = dtCtl.Rows[i][str_FieldName].ToString().Trim();						
								break;
							case "DataGrid":							
								DataGrid grd = (DataGrid)mCtl;							
								grd.Columns[Convert.ToInt32(dtCtl.Rows[i]["ColumnIndex"])].HeaderText = dtCtl.Rows[i][str_FieldName].ToString().Trim();							
								//grd.DataBind();
								break;
							case "FPTDataGrid":							
								FPTToolWeb.Control.DataGrids.FPTDataGrid grd1 = (FPTToolWeb.Control.DataGrids.FPTDataGrid)mCtl;							
								grd1.Columns[Convert.ToInt32(dtCtl.Rows[i]["ColumnIndex"])].HeaderText = dtCtl.Rows[i][str_FieldName].ToString().Trim();							
								//grd.DataBind();
								break;
								//cangtt them vao cac control khac
							case "HyperLink":
								HyperLink hp = (HyperLink)mCtl;
								hp.Text = dtCtl.Rows[i][str_FieldName].ToString().Trim();							
								break;
							case "LinkButton":
								LinkButton lb = (LinkButton)mCtl;
								lb.Text = dtCtl.Rows[i][str_FieldName].ToString().Trim();							
								lb.Dispose();
								break;
							case "CheckBox":
								CheckBox ck = (CheckBox)mCtl;
								ck.Text = dtCtl.Rows[i][str_FieldName].ToString().Trim();							
								break;
							case "RadioButton":
								RadioButton rd = (RadioButton)mCtl;
								rd.Text = dtCtl.Rows[i][str_FieldName].ToString().Trim();							
								break;
							case "HtmlInputButton":
								System.Web.UI.HtmlControls.HtmlInputButton InputButton=(System.Web.UI.HtmlControls.HtmlInputButton)mCtl;
								InputButton.Value=dtCtl.Rows[i][str_FieldName].ToString().Trim();
								break;
							case "HtmlGenericControl":
								System.Web.UI.HtmlControls.HtmlGenericControl InputGenericControl=(System.Web.UI.HtmlControls.HtmlGenericControl)mCtl;
								InputGenericControl.InnerHtml = dtCtl.Rows[i][str_FieldName].ToString().Trim();
								break;
						
								//end-----------------------------------
							default :
								break;
						}
					}
				}
				catch (Exception exc)
				{
				}
			}
		}
		/// <summary>
		/// Luu control xuong DB 
		/// </summary>
		/// <param name="pForm">Form Name</param>
		/// <param name="pControl">ControlID of From</param>
		/// <param name="pColumnIndex">Column Index</param>
		/// <param name="pCaptionCtl">Caption default of Control</param>
		/// <param name="pCaptionVNCtl">Caption VN of Control</param>
		
		public static void Exec_Command(string pForm,string pControl,int pColumnIndex,string pCaptionCtl,string pCaptionVNCtl, string pParent)
		{				
			
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "UMS_spfrmControlSystem";
				cmd.Parameters.Clear();
				
				cmd.Parameters.Add("@FromID",SqlDbType.NVarChar,100).Value = pForm.Trim();
				cmd.Parameters.Add("@ControlID",SqlDbType.NVarChar,30).Value = pControl.Trim();
				cmd.Parameters.Add("@ColumnIndex",SqlDbType.Int).Value = pColumnIndex;
				cmd.Parameters.Add("@CaptionCtl",SqlDbType.NVarChar,200).Value = pCaptionCtl.Trim();
				cmd.Parameters.Add("@CaptionVNCtl",SqlDbType.NVarChar,200).Value = pCaptionVNCtl.Trim();
				cmd.Parameters.Add("@Parent",SqlDbType.NVarChar,100).Value = pParent.Trim();
				cmd.ExecuteNonQuery();

				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
			}
			catch ( Exception exp)
			{				

				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
			}
		}

		
		/// <summary>
		/// UMS-Disable control from page 
		/// </summary>
		/// <param name="pRootCtl">Root control</param>
		/// <param name="pFormName">From Name</param>
		public static void GetUMSForm(Control pRootCtl, string pFormName)
		{
			try
			{
				foreach(Control Child_ctl in pRootCtl.Controls)
				{
					if(Child_ctl.HasControls()==true)
					{
						GetUMSForm(Child_ctl,pFormName);
					}
					else
					{
						switch(Child_ctl.GetType().ToString().Trim())
						{
								//Web control
							case "System.Web.UI.WebControls.DropDownList":								
								DropDownList cbo=(DropDownList)Child_ctl;
								cbo.Enabled = clsCommon.blnFEdit;
								break;		
							case "System.Web.UI.WebControls.CheckBox":								
								CheckBox chk=(CheckBox)Child_ctl;
								chk.Enabled = clsCommon.blnFEdit;
								break;
							case "System.Web.UI.WebControls.TextBox":								
								TextBox txt=(TextBox)Child_ctl;
								txt.Enabled = clsCommon.blnFEdit;
								break;
							case "System.Web.UI.WebControls.RadioButton":								
								RadioButton rd=(RadioButton)Child_ctl;
								rd.Enabled = clsCommon.blnFEdit;								
								break;
							case "System.Web.UI.WebControls.ImageButton":								
								ImageButton imgBtn=(ImageButton)Child_ctl;
								imgBtn.Enabled = clsCommon.blnFEdit;
								break;
							case "System.Web.UI.WebControls.RadioButtonList":
								RadioButtonList optlst = (RadioButtonList)Child_ctl;
								optlst.Enabled = clsCommon.blnFEdit;								
								break;
							case "System.Web.UI.WebControls.CheckBoxList":
								CheckBoxList chklst = (CheckBoxList)Child_ctl;
								chklst.Enabled = clsCommon.blnFEdit;
								break;
								//Html Control																		
							case "System.Web.UI.HtmlControls.HtmlSelect":
								System.Web.UI.HtmlControls.HtmlSelect InputSelect=(System.Web.UI.HtmlControls.HtmlSelect)Child_ctl;
								InputSelect.Disabled=!clsCommon.blnFEdit;
								break;
							case "System.Web.UI.HtmlControls.HtmlInputCheckBox":
								System.Web.UI.HtmlControls.HtmlInputCheckBox InputChk=(System.Web.UI.HtmlControls.HtmlInputCheckBox)Child_ctl;
								InputChk.Disabled=!clsCommon.blnFEdit;
								break;
							case "System.Web.UI.HtmlControls.HtmlInputRadioButton":
								System.Web.UI.HtmlControls.HtmlInputRadioButton InputRd=(System.Web.UI.HtmlControls.HtmlInputRadioButton)Child_ctl;
								InputRd.Disabled=!clsCommon.blnFEdit;
								break;
							case "System.Web.UI.HtmlControls.HtmlInputFile":
								System.Web.UI.HtmlControls.HtmlInputFile InputFile=(System.Web.UI.HtmlControls.HtmlInputFile)Child_ctl;
								InputFile.Disabled=!clsCommon.blnFEdit;
								break;													
								//ket thuc-----------------------------							
						}				
					}
				}
			}
			catch(Exception)
			{
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

		/// <summary>
		/// Get all child control of form
		/// </summary>
		/// <param name="pRootCtl">Root control</param>
		/// <param name="pFormName">From Name</param>
		public static string GetAllControlOfForm(Control pRootCtl, string pFormName)
		{
			string strParent = "";
			try
			{
				foreach(Control Child_ctl in pRootCtl.Controls)
				{
					string str=Child_ctl.ToString();
					//if((!Child_ctl.ToString().Equals("ASP.RightMenu_ascx")||(!Child_ctl.ToString().Equals("ASP.LeftMenu_ascx"))||(!Child_ctl.ToString().Equals("ASP.TopMenu_ascx")))if(!Child_ctl.ToString().Equals("ASP.LeftMenu_ascx"))
					//cập nhậ cho DataGrid
					
					if(!Child_ctl.ToString().Substring(0,7).Equals("ASP.Top_ascx"))						
						if(!Child_ctl.ToString().Substring(0,11).Equals("ASP.LeftMenu_ascx"))
							if(!Child_ctl.ToString().Equals("ASP.Bottom_ascx"))
									
								if(Child_ctl.HasControls()==true)											
								{
						
									GetAllControlOfForm(Child_ctl, pFormName);
									
								}
								else
								{
									try
									{
										//strParent = Child_ctl.Parent.ID;
										strParent = Child_ctl.NamingContainer.ID;
									}
									catch(Exception ee1 )
									{
										strParent ="";
									}
									
									if(strParent == null) strParent = "";

									switch(Child_ctl.GetType().ToString())
									{
											//Cangtt them vao mot so control khac
										case "System.Web.UI.HtmlControls.HtmlGenericControl":
											System.Web.UI.HtmlControls.HtmlGenericControl gctrl = (System.Web.UI.HtmlControls.HtmlGenericControl)Child_ctl;
											Exec_Command(pFormName.Trim(),gctrl.ID.Trim(),0,gctrl.InnerHtml.Trim(),gctrl.InnerHtml.Trim(), strParent);
											break;
										case "System.Web.UI.WebControls.HyperLink":								
											HyperLink hp=(HyperLink)Child_ctl;
											Exec_Command(pFormName.Trim(),hp.ID.Trim(),0,hp.Text.Trim(),hp.Text.Trim(), strParent);
											break;														
										case "System.Web.UI.HtmlControls.HtmlInputButton":
											System.Web.UI.HtmlControls.HtmlInputButton InputButton=(System.Web.UI.HtmlControls.HtmlInputButton)Child_ctl;
											Exec_Command(pFormName.Trim(),InputButton.ID.Trim(),0,InputButton.Value.Trim(),InputButton.Value.Trim(), strParent);
											break;							
										case "System.Web.UI.WebControls.LinkButton":								
											LinkButton hpbtn=(LinkButton)Child_ctl;
											Exec_Command(pFormName.Trim(),hpbtn.ID.Trim(),0,hpbtn.Text.Trim(),hpbtn.Text.Trim(), strParent);
											break;														
											//ket thuc-----------------------------
										case "System.Web.UI.WebControls.Label":
											Label lbl = (Label)Child_ctl;							
											Exec_Command(pFormName.Trim(),lbl.ID.Trim(),0,lbl.Text.Trim(),lbl.Text.Trim(), strParent);
											break;
										case "System.Web.UI.WebControls.Button":
											Button btn = (Button)Child_ctl;
											Exec_Command(pFormName.Trim(),btn.ID.Trim(),0,btn.Text.Trim(),btn.Text.Trim(), strParent);
											break;
										case "System.Web.UI.WebControls.RadioButton":
											RadioButton opt = (RadioButton)Child_ctl;
											Exec_Command(pFormName.Trim(),opt.ID.Trim(),0,opt.Text.Trim(),opt.Text.Trim(), strParent);
											break;					
										case "System.Web.UI.WebControls.RadioButtonList":
											RadioButtonList optlst = (RadioButtonList)Child_ctl;
											int i = 0;						
											foreach(ListItem xITem in optlst.Items)
											{
												Exec_Command(pFormName.Trim(),optlst.ID.Trim(),i,xITem.Text.Trim(),xITem.Text.Trim(), strParent);
												i++;
											}
											break;
										case "System.Web.UI.WebControls.CheckBox":
											CheckBox chk = (CheckBox)Child_ctl;
											Exec_Command(pFormName.Trim(),chk.ID.Trim(),0,chk.Text.Trim(),chk.Text.Trim(), strParent);
											break;
										case "System.Web.UI.WebControls.CheckBoxList":
											CheckBoxList chklst = (CheckBoxList)Child_ctl;
											int iIndex = 0;
											foreach(ListItem xITem in chklst.Items)
											{
												Exec_Command(pFormName.Trim(),chklst.ID.Trim(),iIndex,xITem.Text.Trim(),xITem.Text.Trim(), strParent);
												iIndex++;
											}
											break;
										case "System.Web.UI.WebControls.DataGrid":
											DataGrid dtg = (DataGrid)Child_ctl;
											int icol = 0;
											foreach(DataGridColumn xColumn in dtg.Columns)
											{
												Exec_Command(pFormName.Trim(),dtg.ID.Trim(),icol,xColumn.HeaderText.Trim(),xColumn.HeaderText.Trim(), strParent);
												icol++;
											}
											break;
											
										case "FPTToolWeb.Control.DataGrids.FPTDataGrid":
											FPTToolWeb.Control.DataGrids.FPTDataGrid dtg1 = (FPTToolWeb.Control.DataGrids.FPTDataGrid)Child_ctl;
											int icol1 = 0;
											foreach(DataGridColumn xColumn in dtg1.Columns)
											{
												Exec_Command(pFormName.Trim(),dtg1.ID.Trim(),icol1,xColumn.HeaderText.Trim(),xColumn.HeaderText.Trim(), strParent);
												icol1++;
											}
											break;
						
									}				
								}
					if(Child_ctl.ToString().Equals("System.Web.UI.WebControls.DataGrid"))
					{
						DataGrid dtg = (DataGrid)Child_ctl;
						int icol = 0;
						foreach(DataGridColumn xColumn in dtg.Columns)
						{
							Exec_Command(pFormName.Trim(),dtg.ID.Trim(),icol,xColumn.HeaderText.Trim(),xColumn.HeaderText.Trim(), strParent);
							icol++;
						}
					}
					if(Child_ctl.ToString().Equals("FPTToolWeb.Control.DataGrids.FPTDataGrid"))
					{
						FPTToolWeb.Control.DataGrids.FPTDataGrid dtg = (FPTToolWeb.Control.DataGrids.FPTDataGrid)Child_ctl;
						int icol = 0;
						foreach(DataGridColumn xColumn in dtg.Columns)
						{
							Exec_Command(pFormName.Trim(),dtg.ID.Trim(),icol,xColumn.HeaderText.Trim(),xColumn.HeaderText.Trim(), strParent);
							icol++;
						}
					}
				}

				return "";
			}
			catch(Exception ex)
			{
				return ex.StackTrace;
			}		
			
		}
		

		public static string getStringAlert(string sMess,string sLanguageID)
		{					
			DataRow[] dtRow=Editpage.dtData.Select("MessCode='" + sMess + "'");			
			DataRow iRow = (DataRow)dtRow.GetValue(0);
			return iRow[sLanguageID].ToString();
		}
		public static void UpdateLog(string sAscx,string sAccountLogin)
		{
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "UMS_spfrmTIMELOG";
				cmd.Parameters.Clear();
				
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Save";
				cmd.Parameters.Add("@Ascx",SqlDbType.NVarChar,100).Value = sAscx;
				cmd.Parameters.Add("@AccountLogin",SqlDbType.NVarChar,20).Value = sAccountLogin;				
				cmd.Parameters.Add("@TimeLog",SqlDbType.NVarChar,12).Value = System.DateTime.Now.ToString("dd/MM/yyyy");				
				cmd.ExecuteNonQuery();

				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
			}
			catch ( Exception exp)
			{
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
			}
		
		}
		public static string getEmpName(string sEmpID)
		{
			DataRow drData= clsCommon.GetDataRow("HR_clsCommon @Activity='getEmpName',@EmpID='" + sEmpID + "'");
			return drData["EmpName"].ToString();
		}
		public static string getGender(string sEmpID)
		{
			DataRow drData= clsCommon.GetDataRow("HR_clsCommon @Activity='getGender',@EmpID='" + sEmpID + "'");
			return drData["EmpGender"].ToString();
		}
		
		#region popup window for alert message

		public static void popupWindow(Control pControl, string MessID,string sLanguageID,string Text, int iType)
		{
			
			DataRow[] dtRow=Editpage.dtData.Select("MessCode='" + MessID + "'");			
			DataRow iRow = (DataRow)dtRow.GetValue(0);			
			
			PopupWin popupWin=new PopupWin();
			PlaceHolder PH = new PlaceHolder();	
			PH.Controls.Add(popupWin);						
			pControl.Controls.Add(PH);
			// Add controls to page
			
			if (MessID=="0034")
			{
				Text=Text.Substring(0,Text.LastIndexOf(".")) + ".";
			}
			
			switch(iType)
			{
				case 0: popupWin.ColorStyle=EeekSoft.Web.PopupColorStyle.Red;break;
				case 1: popupWin.ColorStyle=EeekSoft.Web.PopupColorStyle.Green;break;
				case 2: popupWin.ColorStyle=EeekSoft.Web.PopupColorStyle.Blue;break;
				case 3: popupWin.ColorStyle=EeekSoft.Web.PopupColorStyle.Violet;break;
			}
			
			// Set popup win properties
			popupWin.ActionType=EeekSoft.Web.PopupAction.MessageWindow;
			popupWin.ShowLink=false;
			popupWin.Title="Thông báo - Alert";
			popupWin.Message=iRow[sLanguageID].ToString();
			popupWin.Text=Text;
			popupWin.HideAfter=10000;
			popupWin.ShowAfter=1;

			// Show popup
			popupWin.Visible=true;
			popupWin.AutoShow=true;
		}		
		public static void popupWindow(Control pControl, string MessText,string Text,int iType)
		{
			PopupWin popupWin=new PopupWin();
			PlaceHolder PH = new PlaceHolder();	
			PH.Controls.Add(popupWin);						
			pControl.Controls.Add(PH);
			// Add controls to page
			
			switch(iType)
			{
				case 0: popupWin.ColorStyle=EeekSoft.Web.PopupColorStyle.Red;break;
				case 1: popupWin.ColorStyle=EeekSoft.Web.PopupColorStyle.Green;break;
				case 2: popupWin.ColorStyle=EeekSoft.Web.PopupColorStyle.Blue;break;
				case 3: popupWin.ColorStyle=EeekSoft.Web.PopupColorStyle.Violet;break;
			}
			
			// Set popup win properties
			popupWin.ActionType=EeekSoft.Web.PopupAction.MessageWindow;			
			popupWin.ShowLink=false;
			popupWin.Title="Alert";
			popupWin.Message=MessText;
			popupWin.Text=Text;
			popupWin.HideAfter=10000;
			popupWin.ShowAfter=1;

			// Show popup
			popupWin.Visible=true;
			popupWin.AutoShow=true;
		}
        	
		public static void popupWindowReminder(Control pControl, string MessText,string Text,int iType)
		{
			PopupWin popupWin=new PopupWin();	
			popupWin.Width=500;
			popupWin.Height=200;
			PlaceHolder PH = new PlaceHolder();	
			PH.Controls.Add(popupWin);
			string a="";
			try{			
			    if ( pControl != null )
			    {
			        pControl.Controls.Add(PH);
			    }			
			    
			}catch(Exception exp)
			{
				a=exp.Message;				
				return ;
			}
			// Add controls to page
			
			switch(iType)
			{
				case 0: popupWin.ColorStyle=EeekSoft.Web.PopupColorStyle.Red;break;
				case 1: popupWin.ColorStyle=EeekSoft.Web.PopupColorStyle.Green;break;
				case 2: popupWin.ColorStyle=EeekSoft.Web.PopupColorStyle.Blue;break;
				case 3: popupWin.ColorStyle=EeekSoft.Web.PopupColorStyle.Violet;break;
			}
			
			// Set popup win properties
			popupWin.ActionType=EeekSoft.Web.PopupAction.MessageWindow;			
			popupWin.ShowLink=false;
			popupWin.Title="Reminder";
			popupWin.Message=MessText;
			popupWin.Text=Text;
			popupWin.HideAfter=10000;
			popupWin.ShowAfter=1;

			// Show popup
			popupWin.Visible=true;
			popupWin.AutoShow=true;
		}				
		public static void popupWindowExp(Control pControl, Exception ex)
		{
			string Text="";
			DataRow[] dtRow=Editpage.dtData.Select("MessCode='0034'");			
			DataRow iRow = (DataRow)dtRow.GetValue(0);			
			
			PopupWin popupWin=new PopupWin();
			PlaceHolder PH = new PlaceHolder();	
			PH.Controls.Add(popupWin);						
			pControl.Controls.Add(PH);
			// Add controls to page				
			Text=ex.Message.Substring(0,ex.Message.LastIndexOf(".")) + ". <br>";
			Text+=ex.StackTrace.Replace("\r\n","<br>");
			
			
			popupWin.ColorStyle=EeekSoft.Web.PopupColorStyle.Red;
			
			
			// Set popup win properties
			popupWin.ActionType=EeekSoft.Web.PopupAction.MessageWindow;
			popupWin.Title="Alert";
			popupWin.Message=iRow["EN"].ToString();
			popupWin.Text=Text;
			popupWin.HideAfter=10000;
			popupWin.ShowAfter=1;

			// Show popup
			popupWin.Visible=true;
			popupWin.AutoShow=true;
		}
		public static void popupWindowCataLog(Control pControl, string sLink )
		{
			string Text="";
			DataRow[] dtRow=Editpage.dtData.Select("MessCode='0034'");			
			DataRow iRow = (DataRow)dtRow.GetValue(0);			
			
			PopupWin popupWin=new PopupWin();
			PlaceHolder PH = new PlaceHolder();	
			PH.Controls.Add(popupWin);						
			pControl.Controls.Add(PH);
			// Add controls to page				
			
			
			
			popupWin.ColorStyle=EeekSoft.Web.PopupColorStyle.Red;
			
			
			// Set popup win properties
			popupWin.ActionType=EeekSoft.Web.PopupAction.MessageWindow;
			popupWin.Title="Alert";
			popupWin.Message=iRow["EN"].ToString();
			popupWin.Text=sLink.Replace("\r\n","<br>").Replace("'","");
			popupWin.HideAfter=10000;
			popupWin.ShowAfter=1;

			// Show popup
			popupWin.Visible=true;
			popupWin.AutoShow=true;
		}
		#region Tanldt tao ham ung dung khac 
		public static void popupWindowExp_Mess(Control pControl, Exception ex)
		{
			string Text="";
			DataRow[] dtRow=Editpage.dtData.Select("MessCode='0034'");			
			DataRow iRow = (DataRow)dtRow.GetValue(0);			
			
			PopupWin popupWin=new PopupWin();
			PlaceHolder PH = new PlaceHolder();	
			PH.Controls.Add(popupWin);						
			pControl.Controls.Add(PH);
			// Add controls to page				
			Text+=ex.Message.Replace("\r\n"," ") + "<br>";
			if(ex.StackTrace != null)
				Text+=ex.StackTrace.Replace("\r\n"," ") + "<br>";
			if(ex.Source != null)
				Text+=ex.Source + "<br>";
			
			
			
			popupWin.ColorStyle=EeekSoft.Web.PopupColorStyle.Red;
			
			
			// Set popup win properties
			popupWin.ActionType=EeekSoft.Web.PopupAction.MessageWindow;
			popupWin.Title="Message";
			popupWin.Message=iRow["VN"].ToString();
			popupWin.Text=Text;
			popupWin.HideAfter=10000;
			popupWin.ShowAfter=1;

			// Show popup
			popupWin.Visible=true;
			popupWin.AutoShow=true;
		}
		public static void popupWindowExp_Mess(Control pControl, Exception ex,string MessCode)
		{
			string Text="";
			DataRow[] dtRow=Editpage.dtData.Select("MessCode='"+MessCode+"'");			
			DataRow iRow = (DataRow)dtRow.GetValue(0);			
			
			PopupWin popupWin=new PopupWin();
			PlaceHolder PH = new PlaceHolder();	
			PH.Controls.Add(popupWin);						
			pControl.Controls.Add(PH);
			// Add controls to page				
			Text+=ex.Message.Replace("\r\n"," ") + "<br>";
			Text+=ex.StackTrace.Replace("\r\n"," ") + "<br>";
			Text+=ex.Source + "<br>";
			
			
			
			popupWin.ColorStyle=EeekSoft.Web.PopupColorStyle.Red;
			
			
			// Set popup win properties
			popupWin.ActionType=EeekSoft.Web.PopupAction.MessageWindow;
			popupWin.Title="Message";
			popupWin.Message=iRow["VN"].ToString();
			popupWin.Text=Text;
			popupWin.HideAfter=10000;
			popupWin.ShowAfter=1;

			// Show popup
			popupWin.Visible=true;
			popupWin.AutoShow=true;
		}
		#endregion
		public static void popupWindowExp(Control pControl, Exception ex, string mErrUser)
		{
			string Text="";
			DataRow[] dtRow=Editpage.dtData.Select("MessCode='0034'");			
			DataRow iRow = (DataRow)dtRow.GetValue(0);			
			
			PopupWin popupWin=new PopupWin();
			PlaceHolder PH = new PlaceHolder();	
			PH.Controls.Add(popupWin);						
			pControl.Controls.Add(PH);
			// Add controls to page				
			Text=ex.Message.Substring(0,ex.Message.LastIndexOf(".")) + ". <br>";
			Text+=ex.StackTrace.Replace("\r\n","<br>");
			
			
			popupWin.ColorStyle=EeekSoft.Web.PopupColorStyle.Red;
			
			
			// Set popup win properties
			popupWin.ActionType=EeekSoft.Web.PopupAction.MessageWindow;
			popupWin.Title="Message";
			popupWin.Message=iRow["VN"].ToString() + "<br>" + mErrUser;
			popupWin.Text=Text;
			popupWin.HideAfter=10000;
			popupWin.ShowAfter=1;

			// Show popup
			popupWin.Visible=true;
			popupWin.AutoShow=true;
		}
		#endregion

		#region "import file"
		public static string import_file(System.Web.UI.HtmlControls.HtmlInputFile txtFile,string strLanguage,string sFileName, Label lblErr,System.Web.UI.Page Page )
		{
			//---copy file to server			
			string mext = "";
			if( txtFile.Value!="")				
			{
				mext = txtFile.Value.Substring(txtFile.Value.LastIndexOf("."));
				if(mext != ".xls" && mext!= ".doc" && mext!= ".pdf" )
				{
					//lblErr.Text = "Tập tin phải có định dạng là (.xls) hoặc (.doc)";
					lblErr.Text=clsChangeLang.getStringAlert("0025",strLanguage);					
					return "";
				}
			}
			string strFiletmp="";			
			try 
			{
				if (txtFile.Value != "")
				{
					strFiletmp = Path.GetFileNameWithoutExtension(txtFile.Value).Trim() + "_"  +  sFileName + Path.GetExtension(txtFile.Value).Trim();
					if (System.IO.File.Exists(Page.Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrImportFileOnline"].Trim() + strFiletmp))
						System.IO.File.SetAttributes(Page.Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrImportFileOnline"].Trim() + strFiletmp, System.IO.FileAttributes.Normal) ;
					txtFile.PostedFile.SaveAs(Page.Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrImportFileOnline"].Trim() + strFiletmp);
					System.IO.File.SetAttributes(Page.Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrImportFileOnline"].Trim() + strFiletmp, System.IO.FileAttributes.Normal) ;
					return strFiletmp;
				}
				else
				{
					//lblErr.Text = "Please enter the path of filename!";
					lblErr.Text=clsChangeLang.getStringAlert("0026",strLanguage);
					return "null";
				}				
			}
			catch (Exception exp)
			{				
				return "";
			}
			//-- end copy file
		}
		#endregion

	}	
	#region tanldt - update system
	/// <summary>
	/// Cache
	/// </summary>
	public class DataCache 
	{ 

		public static object GetCache(string CacheKey) 
		{ 
			System.Web.Caching.Cache objCache = HttpRuntime.Cache; 
			return objCache.Get(CacheKey); 
		} 

		public static void SetCache(string CacheKey, object objObject) 
		{ 
			System.Web.Caching.Cache objCache = HttpRuntime.Cache; 
			objCache.Insert(CacheKey, objObject); 
		} 

		public static void SetCache(string CacheKey, object objObject, System.Web.Caching.CacheDependency objDependency) 
		{ 
			System.Web.Caching.Cache objCache = HttpRuntime.Cache; 
			objCache.Insert(CacheKey, objObject, objDependency); 
		} 

		public static void SetCache(string CacheKey, object objObject, int SlidingExpiration) 
		{ 
			System.Web.Caching.Cache objCache = HttpRuntime.Cache; 
			objCache.Insert(CacheKey, objObject, null, DateTime.MaxValue, TimeSpan.FromSeconds(SlidingExpiration)); 
		} 

		public static void SetCache(string CacheKey, object objObject, System.DateTime AbsoluteExpiration) 
		{ 
			System.Web.Caching.Cache objCache = HttpRuntime.Cache; 
			objCache.Insert(CacheKey, objObject, null, AbsoluteExpiration, TimeSpan.Zero); 
		} 

		public static void RemoveCache(string CacheKey) 
		{ 
			System.Web.Caching.Cache objCache = HttpRuntime.Cache; 
			if (!(objCache.Get(CacheKey) == null)) 
			{ 
				objCache.Remove(CacheKey); 
			} 
		} 
	}
	#endregion
	
}