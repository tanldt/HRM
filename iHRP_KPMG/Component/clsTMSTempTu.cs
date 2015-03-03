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
//remove this
using iHRPCore.WFComponent;

namespace iHRPCore.TMSComponent
{
	/// <summary>
	/// Summary description for clsTMS.
	/// </summary>

	#region OT
	public class clsPolicyLeaveDay
	{
		public static DataRow GetFiscalYear(string strYear)
		{
			DataRow iRow = clsCommon.GetDataRow("TS_spfrmWorkDay @Action='GetFiscalYear', @Year='"+strYear+"' ");
			if(iRow !=null)
				return iRow;
			else
				return null;
		}
	}
	public class clsWF99
	{
		/// <summary>
		/// Thong ke cap phat cho tung nhan vien
		/// </summary>
		/// <param name="strEmpID"></param>
		/// <param name="strEmpName"></param>
		/// <param name="strLSCompanyID"></param>
		/// <param name="strLSLevel1ID"></param>
		/// <param name="strLSLevel2ID"></param>
		/// <param name="strLSLevel3ID"></param>
		/// <param name="strStatus"></param>
		/// <param name="strRegistered"></param>
		/// <param name="strFromDate"></param>
		/// <param name="strToDate"></param>
		/// <param name="strYear"></param>
		/// <param name="strLSIssueTypeID"></param>
		/// <param name="strLSIssueID"></param>
		/// <param name="strLSSizeID"></param>
		/// <param name="strUnit"></param>
		/// <param name="strCourse"></param>
		/// <returns></returns>
		public static DataTable StatisticUniform(string strEmpID, string strEmpName, 
			string strLSCompanyID, string strLSLevel1ID, string strLSLevel2ID, string strLSLevel3ID, string strStatus, 
			string strRegistered, string strFromDate, string strToDate, string strYear, 
			string strLSIssueTypeID, string strLSIssueID, string strLSSizeID, 
			string strUnit, string strCourse,string strQuantity,string strType)
		{
			DataTable dtb = new DataTable();
			try
			{
				string strSQL ="";
				strSQL= "WF_spfrmIssueUniform @Activity='StatisticUniform', @EmpID=N'" 
					+ strEmpID + "', @EmpName=N'"+ strEmpName +"', @LSCompanyID=N'"
					+ strLSCompanyID + "', @LSLevel1ID=N'" + strLSLevel1ID + "', @LSLevel2ID=N'" + strLSLevel2ID + "', @LSLevel3ID=N'" + strLSLevel3ID + "', @Status='" 
					+ strStatus + "', @Registered='" + strRegistered + "', @FromDate = '" + strFromDate + "', @ToDate = '" + strToDate + "', @Year = '" + strYear + "', "
					+ "@LSIssueTypeID = '" + strLSIssueTypeID + "', @LSIssueID =  '" + strLSIssueID + "', @LSSizeID = '" 
					+ strLSSizeID + "', @Unit = '" + strUnit + "',@Quantity=" + strQuantity + ", @Course = '" + strCourse + "',@Type='" + strType + "'";;
				dtb = clsCommon.GetDataTableHasID(strSQL);
			}
			catch
			{
				dtb = new DataTable();
			}
			return dtb;
		}

		public static DataTable StatisticPublicUniform(string strLSCompanyID, string strLSLevel1ID,
			string strLSLevel2ID, string strLSLevel3ID, string strFromDate, string strToDate, string strYear,
			string strLSIssueTypeID, string strLSIssueID, string strSizeID, string strReceiver, string strCourse, string strUnit)
		{
			DataTable dtb = new DataTable();
			try
			{
				string strSQL ="";
				strSQL= "WF_spfrmPublicUniform @Activity='StatisticPublicUniform',  @LSCompanyID=N'"
					+ strLSCompanyID + "', @LSLevel1ID=N'" + strLSLevel1ID + "', @LSLevel2ID=N'" + strLSLevel2ID + "', @LSLevel3ID='" + strLSLevel3ID + "', @FromDate = '" 
					+ strFromDate + "', @ToDate = '" + strToDate + "', @Year = '" + strYear + "', "
					+ "@IssueTypeID = '" + strLSIssueTypeID + "', @IssueID =  '" + strLSIssueID + "', @SizeID = '" 
					+ strSizeID + "', @Receiver = '" + strReceiver + "', @Unit = '" + strUnit + "', @Course = '" + strCourse + "'";
				dtb = clsCommon.GetDataTableHasID(strSQL);
			}
			catch
			{
				dtb = new DataTable();
			}
			return dtb;
		}

		public static DataRow GetRecordByIssueID(Object strIssueID)
		{
			DataRow iRow = clsCommon.GetDataRow("WF_spfrmIssueUniform @Activity='GetDataByIssueID', @IssueUniformID='"+strIssueID+"'");
			if(iRow !=null)
				return iRow;
			else
				return null;
		}
		public static DataTable FilterSpecialIssueList(string strByYear, string strByEmpID)
		{
			DataTable dtb = new DataTable();
			try
			{
				string strSQL ="";
				strSQL= "WF_spfrmIssueUniform @Activity='FilterSpecialIssueListByYear', @EmpID = '" + strByEmpID + "', @Year = '" + strByYear + "'";
				dtb = clsCommon.GetDataTableHasID(strSQL);
			}
			catch
			{
				dtb = new DataTable();
			}
			return dtb;
		}
		public static DataTable FilterList(string strEmpID, string strEmpName, string strLSCompanyID, string strLSLevel1ID, 
			string strLSLevel2ID, string strLSLevel3ID, string strStatus, string strRegistered, 
			string strFromDate, string strToDate, string strYear, string strLSIssueTypeID, string strLSIssueID, 
			string strSizeID, string strUnit, string strCourse,string strQuantity, string sType)
		{
			DataTable dtb = new DataTable();
			try
			{
				string strSQL ="";
				strSQL= "WF_spfrmIssueUniform @Activity='FilterList', @EmpID='" 
					+ strEmpID + "', @EmpName=N'"+ strEmpName +"', @LSCompanyID=N'"
					+ strLSCompanyID + "', @LSLevel1ID=N'" + strLSLevel1ID + "', @LSLevel2ID=N'" + strLSLevel2ID + "', @LSLevel3ID='" + strLSLevel3ID + "', @Status='" 
					+ strStatus + "', @Registered='" + strRegistered + "', @FromDate = '" + strFromDate + "', @ToDate = '" + strToDate + "', @Year = '" + strYear + "', "
					+ "@LSIssueTypeID = N'" + strLSIssueTypeID + "', @LSIssueID =  N'" + strLSIssueID + "', @LSSizeID = N'" 
					+ strSizeID + "', @Unit = N'" + strUnit + "', @Course = N'" + strCourse + "',@Type='" + sType + "',@Quantity=" + strQuantity + "";
				dtb = clsCommon.GetDataTableHasID(strSQL);
			}
			catch
			{
				dtb = new DataTable();
			}
			return dtb;
		}

		public static void FillDataToGridIssueUniform(DataGrid gridIssue, TextBox txtReceiveDate, TextBox txtCourse, TextBox txtQuantity, TextBox txtUnit, TextBox txtNote)
		{
			for(int i=0; i< gridIssue.Items.Count; i++)
			{					
				CheckBox chkSelect = (CheckBox)gridIssue.Items[i].FindControl("chkSelect");
				// CHECK IF SELECTED
				if(chkSelect.Checked == true)
				{						
					((TextBox)gridIssue.Items[i].FindControl("txtGridDate")).Text = txtReceiveDate.Text;
					((TextBox)gridIssue.Items[i].FindControl("txtGridCourse")).Text = txtCourse.Text;
					((TextBox)gridIssue.Items[i].FindControl("txtGridQuantity")).Text = txtQuantity.Text;
					//((TextBox)gridIssue.Items[i].FindControl("txtGridUnit")).Text = txtUnit.Text; - không cho điền bộ vào trong lưới.
					((TextBox)gridIssue.Items[i].FindControl("txtGridNote")).Text = txtNote.Text;
				}
			}
		}

		public static DataTable GetListToIssueUniform(string strEmpID, string strEmpName, 
			string strLSCompanyID, string strLSLevel1ID, string strLSLevel2ID, 
			string strLSLevel3ID, string strStatus,
			string strYear, string strLSIssueTypeID, string strLSIssueID, string strSizeID, string strUnit, string strQuantity)
		{
			DataTable dtb = new DataTable();
			try
			{
				string strSQL ="";
				strSQL= "WF_spfrmIssueUniform @Activity='Filter', @EmpID=N'" 
					+ strEmpID + "', @EmpName=N'"+ strEmpName +"', @LSCompanyID=N'"
					+ strLSCompanyID + "', @LSLevel1ID=N'" + strLSLevel1ID + "', @LSLevel2ID=N'" + strLSLevel2ID + "', @LSLevel3ID='" + strLSLevel3ID + "', @Status='" 
					+ strStatus + "', @Year = '" + strYear + "', @LSIssueTypeID = '" + strLSIssueTypeID + "', @LSIssueID = '" + strLSIssueID + "', @LSSizeID = '" + strSizeID + "', @Unit = N'" + strUnit + "', @Quantity = '" + strQuantity + "'";
				dtb = clsCommon.GetDataTableHasID(strSQL);
			}
			catch
			{
				dtb = new DataTable();
			}
			return dtb;
		}

		public static DataRow GetDataByID(Object strID)
		{
			DataRow iRow = clsCommon.GetDataRow("WF_spfrmPublicUniform @Activity='GetDataByID',@PublicIssueID = '" + strID +"'");
			if(iRow !=null)
				return iRow;
			else
				return null;
		}

		public static DataRow GetDataByIssueRegisterID(Object strIssueRegisterID)
		{
			DataRow iRow = clsCommon.GetDataRow("WF_spfrmRegisterUniform @Activity='GetDataByIssueRegisterID', @IssueRegisterID='"+strIssueRegisterID+"'");
			if(iRow !=null)
				return iRow;
			else
				return null;
		}

		public static void FillDataToGrid(DataGrid gridIssue, DropDownList cboIssueType, DropDownList cboIssue, DropDownList cboSize, TextBox txtQuantity, TextBox txtUnit, string strIssueID)
		{
			for(int i=0; i< gridIssue.Items.Count; i++)
			{					
				CheckBox chkSelect = (CheckBox)gridIssue.Items[i].FindControl("chkSelect");
				// CHECK IF SELECTED
				if(chkSelect.Checked == true)
				{						
					((Label)gridIssue.Items[i].FindControl("lblGridIssue")).Text = (cboIssue.SelectedValue.Equals("") ? "" : cboIssue.SelectedItem.Text);
					((Label)gridIssue.Items[i].FindControl("lblGridSize")).Text = (cboSize.SelectedValue.Equals("") ? "" : cboSize.SelectedItem.Text);
					((Label)gridIssue.Items[i].FindControl("lblGridQuantity")).Text = (txtQuantity.Text.Equals("") ? "" : txtQuantity.Text);
					((Label)gridIssue.Items[i].FindControl("lblGridUnit")).Text = (txtUnit.Text.Equals("") ? "" : txtUnit.Text);
					gridIssue.Items[i].Cells[10].Text = (cboIssueType.SelectedValue.Equals("") ? "" : cboIssueType.SelectedValue);
					clsWF.GetDataCbo(cboIssue, "WF_spfrmRegisterUniform @Activity = 'LoadcboIssue', @LSIssueTypeID = '"+ cboIssueType.SelectedValue +"'", "LSIssueID","VNName",true);
					gridIssue.Items[i].Cells[11].Text = cboIssue.SelectedValue = (strIssueID.Equals("") ? "" : strIssueID);
					gridIssue.Items[i].Cells[12].Text = (cboSize.SelectedValue.Equals("") ? "" : cboSize.SelectedValue);
				}
			}
		}
	}


	public class clsTMSOT
	{
		public static DataTable GetListToRegisterOT(string strEmpID, string strEmpName, 
			string strLSCompanyID, string strLSLevel1ID, string strLSLevel2ID, 
			string strLSLevel3ID, string strStatus, string strDateID, 
			string strTimeIn,string strTimeOut, string strNote,System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			DataTable dtb = new DataTable();
			try
			{
				dtb = clsCommon.GetDataTable("TS_spfrmOTRegistration @Activity='LoadData', @EmpID='" 
					+ strEmpID + "', @EmpName= N'"+ strEmpName +"', @LSCompanyID=N'"
					+ strLSCompanyID + "', @LSLevel1ID=N'" + strLSLevel1ID + "', @LSLevel2ID=N'"
					+ strLSLevel2ID + "', @LSLevel3ID=N'" + strLSLevel3ID + "', @Status='" 
					+ strStatus + "', @DateID='" + strDateID + "', @TimeIn='" + strTimeIn + "', @TimeOut='" 
					+ strTimeOut + "', @Note=N'" + strNote + "',@UserGroupID='" + sAccountLogin + "'");
			}
			catch
			{
				dtb = new DataTable();
			}
			return dtb;
		}

		public static string SaveData(string strDateID, DataGrid dtgSchedule)
		{
			string strErr = "";
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);		
			SQLconn.Open();
			cmd.Connection = SQLconn;
			cmd.Transaction= SQLconn.BeginTransaction();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "TS_spfrmOTRegistration";
			CheckBox chkSelect = new CheckBox();
			try
			{
				for(int i=0;i<dtgSchedule.Items.Count;i++)
				{					
					chkSelect = (CheckBox)dtgSchedule.Items[i].FindControl("chkSelect");
					// CHECK IF SELECTED
					if(chkSelect.Checked==true)
					{						
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Save";
						cmd.Parameters.Add("@EmpID",SqlDbType.NVarChar,12).Value = dtgSchedule.Items[i].Cells[0].Text.Trim();
						cmd.Parameters.Add("@DateID", SqlDbType.NVarChar, 12).Value = strDateID;
						cmd.Parameters.Add("@TimeIn", SqlDbType.VarChar, 12).Value = ((TextBox)dtgSchedule.Items[i].FindControl("txtTimeIn_Grid")).Text.Trim();;
						cmd.Parameters.Add("@TimeOut", SqlDbType.VarChar, 12).Value = ((TextBox)dtgSchedule.Items[i].FindControl("txtTimeOut_Grid")).Text.Trim();;
						cmd.Parameters.Add("@Note", SqlDbType.NVarChar, 255).Value = ((TextBox)dtgSchedule.Items[i].FindControl("txtNote_Grid")).Text.Trim();
						cmd.ExecuteNonQuery();
						
					}
				}
				cmd.Transaction.Commit();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				strErr = "Lưu thành công!.";
			}
			catch (Exception exp)
			{
				strErr = exp.Message;
				cmd.Transaction.Rollback();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
			}
			chkSelect.Dispose();
			return strErr;
		}

		public static DataTable GetListToCheckOTRecord(string strEmpID, string strEmpName, 
			string strLSCompanyID, string strLSLevel1ID, string strLSLevel2ID, 
			string strLSLevel3ID, string strStatus, string strDateID,System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			DataTable dtb = new DataTable();
			try
			{
				dtb = clsCommon.GetDataTable("TS_spfrmOTRegistration @Activity='LoadDataOT', @EmpID=N'" 
					+ strEmpID + "', @EmpName= N'"+ strEmpName +"', @LSCompanyID=N'"
					+ strLSCompanyID + "', @LSLevel1ID=N'" + strLSLevel1ID + "', @LSLevel2ID=N'"
					+ strLSLevel2ID + "', @LSLevel3ID=N'" + strLSLevel3ID + "', @Status='" 
					+ strStatus + "', @DateID='" + strDateID + "',@UserGroupID=N'" + sAccountLogin + "'");
			}
			catch
			{
				dtb = new DataTable();
			}
			return dtb;
		}

		public static string CheckSaveOTRecord(string strDateID, DataGrid dtgList)
		{
			string strErr = "";
//			SqlCommand  cmd = new SqlCommand();
//			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);		
//			SQLconn.Open();
//			cmd.Connection = SQLconn;
//			cmd.Transaction= SQLconn.BeginTransaction();
//			cmd.CommandType = CommandType.StoredProcedure;
//			cmd.CommandText = "TS_spfrmOTRegistration";
			CheckBox chkIsApprove = new CheckBox();
			CheckBox chkIsPolicy = new CheckBox();
			CheckBox chkSal = new CheckBox();
			DropDownList cboCalOT = new DropDownList();
			
			try
			{
				for(int i=0; i<dtgList.Items.Count; i++)
				{					
					string strTimeIn = ((TextBox)dtgList.Items[i].FindControl("txtTimeIn")).Text.Trim();
					string strTimeOut = ((TextBox)dtgList.Items[i].FindControl("txtTimeOut")).Text.Trim();
					string strScanTimeIn = ((TextBox)dtgList.Items[i].FindControl("txtScanTimeIn")).Text.Trim();
					string strScanTimeOut = ((TextBox)dtgList.Items[i].FindControl("txtScanTimeOut")).Text.Trim();
					string strTimeIn_Grid = ((TextBox)dtgList.Items[i].FindControl("txtTimeIn_Grid")).Text.Trim();
					string strTimeOut_Grid = ((TextBox)dtgList.Items[i].FindControl("txtTimeOut_Grid")).Text.Trim();
					string strCalOT = ((DropDownList)dtgList.Items[i].FindControl("cboCalOT")).SelectedValue.Trim();
					//if(strTimeIn == "" || strTimeOut == "")
					//	break;
					if (strCalOT == "1")
					{
						strTimeIn = strTimeIn_Grid;
						strTimeOut = strTimeOut_Grid;
					}
					else if (strCalOT == "2")
					{
						strTimeIn = strScanTimeIn;
						strTimeOut = strScanTimeOut;
					}

					chkIsApprove = (CheckBox)dtgList.Items[i].FindControl("chkApprove");
					chkIsPolicy = (CheckBox)dtgList.Items[i].FindControl("chkIsPolicy");
					chkSal = (CheckBox)dtgList.Items[i].FindControl("chkSal");
					// CHECK IF SELECTED
					bool blnIsApprove = chkIsApprove.Checked;
					bool blnIsPolicy = chkIsPolicy.Checked;
					bool blnIsSal = chkSal.Checked;
					if (!blnIsApprove)
						continue;

					if (blnIsPolicy)
					{
						string strCheck = clsCommon.LookUpTable("select top 1 LSOTWorkHourID from LS_tblOTWorkHour where '1900-01-01 "+strTimeIn+":00.000' between FromTime and ToTime","LSOTWorkHourID");
						if (strCheck.Equals(""))
						{
							strErr = "Chưa thiết lập giờ OT theo diện chính sách";
							break;
						}
						strCheck = clsCommon.LookUpTable("select top 1 LSOTWorkHourID from LS_tblOTWorkHour where '1900-01-01 "+strTimeOut+":00.000' between FromTime and ToTime","LSOTWorkHourID");
						if (strCheck.Equals(""))
						{
							strErr = "Chưa thiết lập giờ OT theo diện chính sách";
							break;
						}
					}
						
				}
//				cmd.Transaction.Commit();
//				cmd.Dispose();
//				SQLconn.Close();
//				SQLconn.Dispose();
			}
			catch (Exception exp)
			{
				strErr = exp.Message;
//				cmd.Transaction.Rollback();			
			}
			return strErr;
		}

		public static string SaveOTRecord(string strDateID, DataGrid dtgList)
		{
			string strErr = "";
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);		
			SQLconn.Open();
			cmd.Connection = SQLconn;
			cmd.Transaction= SQLconn.BeginTransaction();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "TS_spfrmOTRegistration";
			CheckBox chkIsApprove = new CheckBox();
			CheckBox chkIsPolicy = new CheckBox();
			CheckBox chkSal = new CheckBox();
			DropDownList cboCalOT = new DropDownList();
			
			try
			{
				for(int i=0; i<dtgList.Items.Count; i++)
				{					
					string strTimeIn = ((TextBox)dtgList.Items[i].FindControl("txtTimeIn")).Text.Trim();
					string strTimeOut = ((TextBox)dtgList.Items[i].FindControl("txtTimeOut")).Text.Trim();
					string strScanTimeIn = ((TextBox)dtgList.Items[i].FindControl("txtScanTimeIn")).Text.Trim();
					string strScanTimeOut = ((TextBox)dtgList.Items[i].FindControl("txtScanTimeOut")).Text.Trim();
					string strTimeIn_Grid = ((TextBox)dtgList.Items[i].FindControl("txtTimeIn_Grid")).Text.Trim();
					string strTimeOut_Grid = ((TextBox)dtgList.Items[i].FindControl("txtTimeOut_Grid")).Text.Trim();
					string strCalOT = ((DropDownList)dtgList.Items[i].FindControl("cboCalOT")).SelectedValue.Trim();
					//if(strTimeIn == "" || strTimeOut == "")
					//	break;
					if (strCalOT == "1")
					{
						strTimeIn = strTimeIn_Grid;
						strTimeOut = strTimeOut_Grid;
					}
					else if (strCalOT == "2")
					{
						strTimeIn = strScanTimeIn;
						strTimeOut = strScanTimeOut;
					}

					chkIsApprove = (CheckBox)dtgList.Items[i].FindControl("chkApprove");
					chkIsPolicy = (CheckBox)dtgList.Items[i].FindControl("chkIsPolicy");
					chkSal = (CheckBox)dtgList.Items[i].FindControl("chkSal");
					// CHECK IF SELECTED
					bool blnIsApprove = chkIsApprove.Checked;
					bool blnIsPolicy = chkIsPolicy.Checked;
					bool blnIsSal = chkSal.Checked;
					//if (!blnIsApprove)
					//	continue;

					cmd.Parameters.Clear(); 

					cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "SaveDataOT";
					cmd.Parameters.Add("@EmpID",SqlDbType.NVarChar,12).Value = dtgList.Items[i].Cells[dtgList.Items[i].Cells.Count-1].Text.Trim();
					cmd.Parameters.Add("@DateID", SqlDbType.NVarChar, 12).Value = strDateID;
					cmd.Parameters.Add("@TimeIn", SqlDbType.VarChar, 12).Value = strTimeIn;
					cmd.Parameters.Add("@TimeOut", SqlDbType.VarChar, 12).Value = strTimeOut;
					cmd.Parameters.Add("@Coefficient", SqlDbType.Decimal, 10).Value = Convert.ToDecimal(blnIsPolicy == false ? ((TextBox)dtgList.Items[i].FindControl("txtCoefficient")).Text.Trim() : "0");
					cmd.Parameters.Add("@IsSal", SqlDbType.Bit).Value = blnIsSal;
					cmd.Parameters.Add("@IsPolicy", SqlDbType.Bit).Value = blnIsPolicy;
					cmd.Parameters.Add("@Approve", SqlDbType.Bit).Value = blnIsApprove;
					cmd.Parameters.Add("@Note", SqlDbType.NVarChar, 255).Value = ((TextBox)dtgList.Items[i].FindControl("txtNote_Grid")).Text.Trim();
					cmd.Parameters.Add("@CalculateOT", SqlDbType.Int, 4).Value = Convert.ToInt32(strCalOT);

					cmd.ExecuteNonQuery();
						
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
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
			}
			return strErr;
		}

		public static DataTable GetListTotalOT(string strEmpID, string strEmpName, 
			string strLSCompanyID, string strLSLevel1ID, string strLSLevel2ID, 
			string strLSLevel3ID, string strStatus, string strDateID,System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			DataTable dtb = new DataTable();
			try
			{
				dtb = clsCommon.GetDataTable("TS_spfrmOTRegistration @Activity='LoadSumOT', @EmpID=N'" 
					+ strEmpID + "', @EmpName=N'"+ strEmpName +"', @LSCompanyID=N'"
					+ strLSCompanyID + "', @LSLevel1ID=N'" + strLSLevel1ID + "', @LSLevel2ID=N'"
					+ strLSLevel2ID + "', @LSLevel3ID=N'" + strLSLevel3ID + "', @Status='" 
					+ strStatus + "', @DateID='" + strDateID + "',@UserGroupID=N'" + sAccountLogin + "'");
			}
			catch
			{
				dtb = new DataTable();
			}
			return dtb;
		}

		public static DataTable GetListManualHTK(string strEmpID, string strEmpName, 
			string strLSCompanyID, string strLSLevel1ID, string strLSLevel2ID, 
			string strLSLevel3ID, string strStatus, string strMonth, string strYear, System.Web.UI.Page pPage)
		{
			DataTable dtb = new DataTable();
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";

			try
			{
				dtb = clsCommon.GetDataTableHasID("TS_spfrmWorkPointRecord @Activity='LoadData', @EmpID=N'" 
					+ strEmpID + "', @EmpName=N'"+ strEmpName +"', @LSCompanyID=N'"
					+ strLSCompanyID + "', @LSLevel1ID=N'" + strLSLevel1ID + "', @LSLevel2ID=N'"
					+ strLSLevel2ID + "', @LSLevel3ID=N'" + strLSLevel3ID + "', @Status='" 
					+ strStatus + "', @Month='" + strMonth + "', @Year='" + strYear + "',@UserGroupID=N'" + sAccountLogin + "'");
			}
			catch
			{
				dtb = new DataTable();
			}
			return dtb;
		} 

		public static string SaveManualHTK(string strMonth, string strYear, DataGrid dtgList)
		{
			string strErr = "";
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);		
			SQLconn.Open();
			cmd.Connection = SQLconn;
			cmd.Transaction= SQLconn.BeginTransaction();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "TS_spfrmWorkPointRecord";

			int intChosenMonth = Convert.ToInt32(strMonth);
			int intChosenYear = Convert.ToInt32(strYear);

			int intBefMonth,intBefYear;
			intBefMonth = Convert.ToDateTime(strMonth+"/01/"+strYear).AddMonths(-1).Month;
			intBefYear = Convert.ToDateTime(strMonth+"/01/"+strYear).AddMonths(-1).Year;

			int intTotalDays = DateTime.DaysInMonth(intChosenYear, intBefMonth);
			
			try
			{
				for(int i=0; i<dtgList.Items.Count; i++)
				{					
					for(int j=1; j<=intTotalDays; j++)
					{
						float fltHours = 0.0f;
						try
						{
							fltHours = Convert.ToSingle(((TextBox)dtgList.Items[i].FindControl("txtDay"+j)).Text.Trim());
						}
						catch(FormatException)
						{
							string strDay = ((TextBox)dtgList.Items[i].FindControl("txtDay"+j)).Text.Trim();
							if ((strDay.IndexOf("0.5") != -1 ) || (strDay.IndexOf(".5") != -1 ))
								fltHours = 50.0f; // qui uoc de tinh nua ngay
							else
								fltHours = 0.0f;
							
						}
						cmd.Parameters.Clear(); 
						string DateID;
						if (j > 20)
							DateID = "0"+j+"/"+intBefMonth.ToString()+"/"+intBefYear.ToString();
						else
							DateID = "0"+j+"/"+strMonth+"/"+strYear;

						cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Save";
						cmd.Parameters.Add("@EmpID",SqlDbType.NVarChar,12).Value = dtgList.Items[i].Cells[0].Text.Trim();
						cmd.Parameters.Add("@DateID", SqlDbType.NVarChar, 12).Value = DateID.Substring(DateID.Length-10);
						cmd.Parameters.Add("@Hours", SqlDbType.VarChar, 12).Value = ""+fltHours;

						cmd.ExecuteNonQuery();
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
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
			}
			return strErr;
		}
	}
	#endregion
}
