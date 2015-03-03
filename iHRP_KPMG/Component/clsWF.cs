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
namespace iHRPCore.WFComponent
{
	using iHRPCore.Com;
	/// <summary>
	/// Summary description for clsWF.
	/// </summary>
	public class clsWF
	{
		public static DataTable Search(string strCompanyID, string strLevel1ID, string strLevel2ID, string strLevel3ID, 
			string strFromDate, string strToDate, string strYear, string strLSIssueTypeID, string strLSIssueID, 
			string strSizeID, string strUnit, string strCourse,string strReceiver)
		{
			DataTable dtb = new DataTable();
			try
			{
				string strSQL ="";
				strSQL= "WF_spfrmPublicUniform @Activity='Filter', @LSCompanyID = '"+strCompanyID+"', @LSLevel1ID = '"+strLevel1ID+"', @LSLevel2ID = '"+strLevel2ID+"', @LSLevel3ID = '"+strLevel3ID+"'," 
						+" @FromDate = '"+strFromDate+"', @ToDate = '"+strToDate+"', @Year = '"+strYear+"', @IssueTypeID = '"+strLSIssueTypeID+"', @IssueID = '"+strLSIssueID+"', "
					+" @SizeID = '"+strSizeID+"', @Unit = '"+strUnit+"',@Receiver='" + strReceiver + "', @Course = '" + strCourse + "'";
				dtb = clsCommon.GetDataTableHasID(strSQL);
			}
			catch
			{
				dtb = new DataTable();
			}
			return dtb;
		}

		public static void GetDataCbo(DropDownList pCtrl,string pCommandText,string pValueField,string pTextField,bool pRowBlank)
		{
			clsCommon.LoadDropDownListControl(pCtrl, pCommandText, pValueField, pTextField, pRowBlank);
		}

		public static DataTable GetListToRegisterIssue(string strEmpID, string strEmpName, 
			string strLSCompanyID, string strLSLevel1ID, string strLSLevel2ID, 
			string strLSLevel3ID, string strStatus, string strRegister,
			string strYear, string strLSIssueTypeID, string strLSIssueID, string strSizeID, string strUnit, string strQuantity)
		{
			DataTable dtb = new DataTable();
			try
			{
				string strSQL ="";
				strSQL= "WF_spfrmRegisterUniform @Activity='Filter', @EmpID='" 
					+ strEmpID + "', @EmpName='"+ strEmpName +"', @LSCompanyID='"
					+ strLSCompanyID + "', @LSLevel1ID='" + strLSLevel1ID + "', @LSLevel2ID='" + strLSLevel2ID + "', @LSLevel3ID='" + strLSLevel3ID + "', @Status='" 
					+ strStatus + "', @Register='" + strRegister + "', @Year = '" + strYear + "', @LSIssueTypeID = '" + strLSIssueTypeID + "', @LSIssueID = '" + strLSIssueID + "', @LSSizeID = '" + strSizeID + "', @Unit = N'" + strUnit + "', @Quantity = '" + strQuantity + "'";
				dtb = clsCommon.GetDataTableHasID(strSQL);
			}
			catch
			{
				dtb = new DataTable();
			}
			return dtb;
		}

		public static string SavePublicIssueRecord(string strCommand, string strPublicIssueID, string strLSCompanyID, string strLSLevel1ID, string strLSLevel2ID, string strLSLevel3ID, 
			string strIssueTypeID, string strIssueID, string strReceiver, string strCourse, string strYear,
			string strReceiveDate, string strSizeID, string strQuantity, string strUnit, string strNote)
		{
			string strErr = "";
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);		
			SQLconn.Open();
			cmd.Connection = SQLconn;
			cmd.Transaction= SQLconn.BeginTransaction();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "WF_spfrmPublicUniform";

			try
			{						
				cmd.Parameters.Clear();

				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = strCommand;
				cmd.Parameters.Add("@PublicIssueID", SqlDbType.NVarChar, 12).Value = strPublicIssueID;
				if(!strLSCompanyID.Equals(""))
				{
					cmd.Parameters.Add("@LSCompanyID", SqlDbType.NVarChar, 12).Value = strLSCompanyID;
					if(!strLSLevel1ID.Equals(""))
					{
						cmd.Parameters.Add("@LSLevel1ID", SqlDbType.NVarChar, 12).Value = strLSLevel1ID;
						if(!strLSLevel2ID.Equals(""))
						{
							cmd.Parameters.Add("@LSLevel2ID", SqlDbType.NVarChar, 12).Value = strLSLevel2ID;
							if(!strLSLevel3ID.Equals(""))
								cmd.Parameters.Add("@LSLevel3ID", SqlDbType.NVarChar, 12).Value = strLSLevel3ID;
						}
					}
				}
				if(!strIssueTypeID.Equals(""))
				{
					cmd.Parameters.Add("@IssueTypeID", SqlDbType.NVarChar, 50).Value = strIssueTypeID;
					if(!strIssueID.Equals(""))
						cmd.Parameters.Add("@IssueID", SqlDbType.NVarChar, 50).Value = strIssueID;
				}
				cmd.Parameters.Add("@Receiver", SqlDbType.NVarChar, 50).Value = strReceiver;
				if(!strCourse.Equals(""))
					cmd.Parameters.Add("@Course", SqlDbType.Int).Value =  Convert.ToInt32(strCourse);
				cmd.Parameters.Add("@Year", SqlDbType.VarChar, 4).Value = strYear;
				cmd.Parameters.Add("@ReceiveDate", SqlDbType.NVarChar, 12).Value = strReceiveDate;
				cmd.Parameters.Add("@SizeID", SqlDbType.NVarChar, 50).Value = strSizeID;
				if(!strQuantity.Equals(""))
					cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = Convert.ToInt32(strQuantity);
				cmd.Parameters.Add("@Unit", SqlDbType.NVarChar, 255).Value = strUnit;
				cmd.Parameters.Add("@Note", SqlDbType.NVarChar, 255).Value = strNote;

				cmd.ExecuteNonQuery();

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

		public static string SaveData(DataGrid dtgIssue, string strYear)
		{
			string strErr = "";
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);		
			SQLconn.Open();
			cmd.Connection = SQLconn;
			cmd.Transaction= SQLconn.BeginTransaction();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "WF_spfrmIssue";
			CheckBox chkSelect = new CheckBox();
			try
			{
				for(int i=0;i<dtgIssue.Items.Count;i++)
				{					
					chkSelect = (CheckBox)dtgIssue.Items[i].FindControl("chkSelect");
					// CHECK IF SELECTED
					if(chkSelect.Checked==true)
					{						
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Save";
						cmd.Parameters.Add("@LSIssueID",SqlDbType.NVarChar,12).Value = dtgIssue.Items[i].Cells[11].Text.Trim();
						cmd.Parameters.Add("@EmpID",SqlDbType.NVarChar,12).Value = dtgIssue.Items[i].Cells[2].Text.Trim();
						cmd.Parameters.Add("@Year", SqlDbType.NVarChar, 12).Value = strYear;
						cmd.Parameters.Add("@LSSizeID", SqlDbType.VarChar, 12).Value = ((TextBox)dtgIssue.Items[i].FindControl("txtTimeIn_Grid")).Text.Trim();;
						cmd.Parameters.Add("@Quantity", SqlDbType.VarChar, 12).Value = ((TextBox)dtgIssue.Items[i].FindControl("txtTimeOut_Grid")).Text.Trim();;
						cmd.Parameters.Add("@Unit", SqlDbType.NVarChar, 255).Value = ((TextBox)dtgIssue.Items[i].FindControl("txtNote_Grid")).Text.Trim();
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
			}
			chkSelect.Dispose();
			return strErr;
		}

		public static void FillDataToGrid(DataGrid gridIssue, DropDownList cboIssueType, DropDownList cboIssue, DropDownList cboSize, TextBox txtQuantity, TextBox txtUnit)
		{
			for(int i=0; i< gridIssue.Items.Count; i++)
			{					
				CheckBox chkSelect = (CheckBox)gridIssue.Items[i].FindControl("chkSelect");
				// CHECK IF SELECTED
				if(chkSelect.Checked == true)
				{						
					gridIssue.Items[i].Cells[3].Text = cboIssue.SelectedItem.Text;
					gridIssue.Items[i].Cells[4].Text = cboSize.SelectedItem.Text;
					gridIssue.Items[i].Cells[5].Text = txtUnit.Text;
					gridIssue.Items[i].Cells[6].Text = txtQuantity.Text;
					gridIssue.Items[i].Cells[9].Text = cboIssueType.SelectedValue;
					gridIssue.Items[i].Cells[10].Text = cboIssue.SelectedValue;
					gridIssue.Items[i].Cells[11].Text = cboSize.SelectedValue;
				}
			}
		}

		public static DataTable SearchByYear(string strYear)
		{
			DataTable dtb = new DataTable();
			try
			{
				string strSQL ="";
				strSQL= "WF_spfrmPublicUniform @Activity='FilterByYear', @Year = '" + strYear + "'";
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
			DataRow iRow = clsCommon.GetDataRow("WF_spfrmPublicIssue @Activity='GetDataByID',@PublicIssueID = '" + strID +"'");
			if(iRow !=null)
				return iRow;
			else
				return null;
		}
	}
	public class clsWFEquipment
	{
		public static DataTable GetListSearch(string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strPosition, string strJobCode, string strLocation,string strCompany,string strStatus, string strEquipment, string strFromDate, string strToDate)
		{
			DataTable dt = new DataTable();
			try
			{
				string strSql = "WF_spfrmEquipment @Activity='GetDataListSearch',@EmpID=N'" + strEmpID + "', @EmpName=N'" + strEmpName
					+ "', @LSLevel1Code='" + strLevel1 + "', @LSLevel2Code='" + strLevel2 + "',@LSLevel3Code='" + strLevel3
					+ "',@LSPositionCode='" + strPosition + "',@LSJobCodeCode='" + strJobCode
					+ "',@LSLocationCode='" + strLocation + "',@LSCompanyCode='" + strCompany + "',@FromDate=N'" + strFromDate.Trim() + "',@ToDate=N'" + strToDate + "',@EquipmentCode=N'" + strEquipment + "'";
				if (strStatus != "")
					strSql = strSql + ", @Status='" + strStatus + "'";			
				dt = clsCommon.GetDataTableHasID(strSql);
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
		/// <summary>
		/// Tra ve danh sach cac loai vat dung
		/// </summary>
		/// <returns></returns>
		public static DataTable GetListEquipment()
		{
			DataTable dt = new DataTable();
			try
			{				
				dt = clsCommon.GetDataTableHasID("WF_spfrmEquipment @Activity = 'GetDataAllEquipment'");
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
		/// <summary>
		/// Tra ve chi tiet nhap theo tung loai vat dung
		/// </summary>
		/// <param name="objCode">Ma vat dung</param>
		/// <returns></returns>
		public static DataTable GetReceiptEquipmentByID(Object objCode)
		{
			DataTable dt = new DataTable();
			try
			{				
				dt = clsCommon.GetDataTableHasID("WF_spfrmEquipment @Activity = 'GetDataReceiptEquipmentByID',@EquipmentCode=N'" + objCode + "'");
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
		/// <summary>
		/// Tra ve chi tiet nhap theo tung loai vat dung
		/// </summary>
		/// <param name="objCode">Ma vat dung</param>
		/// <returns></returns>
		public static DataTable GetAllReceiptEquipment()
		{
			DataTable dt = new DataTable();
			try
			{				
				dt = clsCommon.GetDataTableHasID("WF_spfrmEquipment @Activity = 'GetDataAllReceiptEquipment'");
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
		public static DataTable GetIssueEquipment(string strListID,string strEquipmentCode,string strDate)
		{
			DataTable dt = new DataTable();
			try
			{
				strListID = strListID.Replace(",","@@");
				dt = clsCommon.GetDataTableHasID("WF_spfrmEquipment @Activity = 'GetDataIssueByDate',@EmpIDList=N'" + strListID + "',@EquipmentCode=N'" + strEquipmentCode.Trim() + "',@IssueDate=N'" + strDate + "'");
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
		public static bool InStock(string strListID,string strEquipmentCode,string strQuantity)
		{
			try
			{
				string[] arrID = strListID.Trim().Split(',');
				DataRow iRow = clsCommon.GetDataRow("WF_spfrmEquipment @Activity =N'GetInStock',@EquipmentCode=N'" + strEquipmentCode + "'");
				if(iRow != null)
				{
					if((Convert.ToInt32(iRow["InStock"].ToString().Trim())) >= (arrID.Length *Convert.ToInt32(strQuantity)))
						return true;
				}
				return false;
			}
			catch
			{
				return false;
			}
		}
		public static bool SaveIssueEquipment(string strListID,string strEquipmentCode,string strDate,string strQuantity,string strNote)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "WF_spfrmEquipment";
			//-------
			try
			{
				string[] arrID = strListID.Trim().Split(',');
				//--------------
				for(int i=0;i<arrID.Length;i++)
				{
					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "SaveIssue";
					cmd.Parameters.Add("@EmpID",SqlDbType.NVarChar,12).Value = arrID.GetValue(i).ToString().Trim();
					cmd.Parameters.Add("@EquipmentCode",SqlDbType.NVarChar,12).Value = strEquipmentCode.Trim();
					cmd.Parameters.Add("@IssueDate",SqlDbType.NVarChar,12).Value = strDate.Trim();
					cmd.Parameters.Add("@IssueQuantity",SqlDbType.Int).Value = strQuantity.Trim();
					cmd.Parameters.Add("@Note",SqlDbType.NVarChar,255).Value = strNote.Trim();
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
	public class clsWFWelfare
	{
		/// <summary>
		/// Lay thong tin nhan vien sinh nhat trong thang
		/// </summary>
		/// <param name="strMonth"></param>
		/// <returns></returns>
		public static DataTable GetDataBirthdayInMonth(string strMonth,string strEmpID,string strEmpName,
			string strDivision,string strDepartment,string strSection,string strPosition,string strLocation,string strJobCode)
		{
			DataTable dt = new DataTable();
			try
			{
				//dt = clsCommon.GetDataTableHasID("WF_spfrmWELFARE @Activity='GetDataBirthdayInMonth',@Month = " + strMonth);
				string strSQL="WF_spfrmWELFARE @Activity='GetBirthdayInMonth',@Month="+strMonth;
				strSQL+=",@EmpID='"+strEmpID+"'";
				strSQL+=",@EmpName=N'"+strEmpName+"'";
				strSQL+=",@LSLevel1Code='"+strDivision+"'";
				strSQL+=",@LSLevel2Code='"+strDepartment+"'";
				strSQL+=",@LSLevel3Code='"+strSection+"'";
				strSQL+=",@LSPositionCode='"+strPosition+"'";
				strSQL+=",@LSLocationCode='"+strLocation+"'";
				strSQL+=",@LSJobCodeCode='"+strJobCode+"'";
				
				dt = clsCommon.GetDataTableHasID(strSQL);

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

		public static void SaveBirthdayWF(string strEmpID,string strReceiveDate)
		{
			string strSQL="WF_spfrmWELFARE @Activity ='Save',@EmpID=N'"+strEmpID+"',@ReceiveDate='"+strReceiveDate+"',@WelfareCode='WF005'";
			clsCommon.GetDataTable(strSQL);
		}

		public static string ImportWelfare(string strWelfareType,string strDate, string pstrFilename,DataGrid grdResult)
		{
			string mstr_FileName = pstrFilename;
			if (!File.Exists(ConfigurationSettings.AppSettings["pstrImportFileFolder"].Trim() + mstr_FileName))
			{
				return "File not found, Please check path of the filename again!";
			}			
			string mstr_PathFileName = ConfigurationSettings.AppSettings["pstrImportFileFolder"].Trim() + mstr_FileName;
			//------------------

			string strConn;
			strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + mstr_PathFileName + ";Extended Properties=Excel 8.0;";			
			OleDbDataAdapter myCommand = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", strConn);
			DataSet myDataSet = new DataSet();
			myCommand.Fill(myDataSet, "ExcelData");
			grdResult.DataSource = myDataSet.Tables["ExcelData"];
			grdResult.DataBind();
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();			
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "WF_spfrmWELFARE";
			try
			{
				for(int i=0;i<myDataSet.Tables["ExcelData"].Rows.Count;i++)
				{
					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 50).Value = "Save";
					cmd.Parameters.Add("@WelfareCode", SqlDbType.NVarChar,12).Value = strWelfareType.Trim();
					cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar,12).Value = myDataSet.Tables["ExcelData"].Rows[i][1].ToString().Trim();
					cmd.Parameters.Add("@ReceiveDate", SqlDbType.NVarChar,12).Value = strDate.Trim();
					cmd.Parameters.Add("@Amount", SqlDbType.Money).Value = myDataSet.Tables["ExcelData"].Rows[i][4].ToString().Trim();
					cmd.Parameters.Add("@Note", SqlDbType.NVarChar,255).Value = myDataSet.Tables["ExcelData"].Rows[i][5].ToString().Trim();
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
		/// <summary>
		/// Lay thong tin qua trinh phuc loi cua nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataTable</returns>
		public static DataTable GetDataByEmpID(Object strEmpID)
		{
			DataTable dt = new DataTable();
			try
			{
				string strSQL="WF_spfrmWELFARE @Activity='GetDataByEmpID',@EmpID = N'" + strEmpID + "'";								
				dt = clsCommon.GetDataTableHasID(strSQL);
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
		/// Lay thong tin nhan phuc loi cua nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataRow</returns>
		public static DataRow GetDataByID(Object strID)
		{
			DataRow iRow = clsCommon.GetDataRow("WF_spfrmWELFARE @Activity='GetDataByID',@WelfareID = " + strID);
			if(iRow !=null)
				return iRow;
			else
				return null;
		}
		/// <summary>
		/// Lay thong tin nhan vien theo dieu kien loc
		/// </summary>
		public static DataTable GetEmpList(string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strPosition, string strJobCode, string strLocation, string strCompany, string strStatus,
			string strWelfareCode,string strFromDate,string strToDate)
		{
			string strSql = "WF_spfrmWELFARE @Activity='GetDataListSearch',@EmpID=N'" + strEmpID + "', @EmpName=N'" + strEmpName
				+ "', @LSLevel1Code='" + strLevel1 + "', @LSLevel2Code='" + strLevel2 + "',@LSLevel3Code='" + strLevel3
				+ "',@LSPositionCode='" + strPosition + "',@LSJobCodeCode='" + strJobCode 
				+ "',@LSLocationCode='" + strLocation + "',@LSCompanyCode='" + strCompany 
				+ "',@WelfareCode=N'" + strWelfareCode + "',@FromDate=N'" + strFromDate.Trim() + "',@ToDate=N'" + strToDate + "'";
			if (strStatus != "")
				strSql = strSql + ", @Status='" + strStatus + "'";
			DataTable dtb = clsCommon.GetDataTable(strSql);
			return dtb;
		}


		# region List welfare
		public static DataTable LstWelfare(string strEmpID, string strEmpName, string strCompany, string strLevel1, string strLevel2, string strLevel3, string strFromDate, string strToDate, string strYear, string strLSWelfareID)
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
			cmd.CommandText = "WF_spfrmWelfare";
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 50).Value = "LstWelfare";
				cmd.Parameters.Add("@LSCompanyID", SqlDbType.NVarChar,12).Value = strCompany ;
				cmd.Parameters.Add("@LSLevel1ID", SqlDbType.NVarChar,12).Value = strLevel1 ;
				cmd.Parameters.Add("@LSLevel2ID", SqlDbType.NVarChar,12).Value = strLevel2 ;
				cmd.Parameters.Add("@LSLevel3ID", SqlDbType.NVarChar,12).Value = strLevel3 ;
				cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar,12).Value = strEmpID;
				cmd.Parameters.Add("@EmpName", SqlDbType.NVarChar,80).Value = strEmpName;
				cmd.Parameters.Add("@LSWelfareID", SqlDbType.NVarChar, 12).Value = strLSWelfareID;
				cmd.Parameters.Add("@Year", SqlDbType.VarChar, 4).Value = strYear;
				cmd.Parameters.Add("@FromDate", SqlDbType.VarChar, 12).Value = strFromDate ;					
				cmd.Parameters.Add("@ToDate", SqlDbType.VarChar, 12).Value = strToDate;			
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


		public static string ImpactDB(string strActivity, string strWelfareID, string strEmpID, string strLSWelfareID, string strReceiveDate, string strAmount, string strNote, string strCreater, string strLanguage)			
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "WF_spfrmWELFARE";			
			try
			{				
					
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = strActivity;
				cmd.Parameters.Add("@Language",SqlDbType.NVarChar,50).Value = strLanguage ;
				cmd.Parameters.Add("@EmpID",SqlDbType.NVarChar,12).Value = strEmpID;
				cmd.Parameters.Add("@WelfareID",SqlDbType.NVarChar,12).Value = strWelfareID;
				cmd.Parameters.Add("@LSWelfareID",SqlDbType.NVarChar,12).Value = strLSWelfareID;
				cmd.Parameters.Add("@ReceiveDate",SqlDbType.NVarChar,12).Value = strReceiveDate;
				cmd.Parameters.Add("@Amount",SqlDbType.Float).Value = strAmount ;
				if (strNote != "")
					cmd.Parameters.Add("@Note",SqlDbType.NVarChar,200).Value = strNote;
				cmd.Parameters.Add("@Creater",SqlDbType.NVarChar,12).Value = strCreater;
				cmd.Parameters.Add("@ReturnMess",SqlDbType.NVarChar,200).Value = "";
				cmd.Parameters["@ReturnMess"].Direction  = ParameterDirection.InputOutput;
				cmd.ExecuteNonQuery();

				if (sqlTran != null ) sqlTran.Commit();
				cmd.Dispose();		
				return cmd.Parameters["@ReturnMess"].Value.ToString();
			}
			catch (Exception exp)
			{				
				if (sqlTran != null) sqlTran.Rollback();
				cmd.Dispose();
				return exp.Message;
			}			
		}
	}

	public class clsWFCardKey
	{		
		/// <summary>
		/// Lay thong tin qua trinh cap the cua nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataTable</returns>
		public static DataTable GetDataByEmpID(Object strEmpID)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("WF_spfrmCARDKEY @Activity='GetDataByEmpID',@EmpID = N'" + strEmpID + "'");
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
		/// Lay 1 dong thong tin nhan cap the cua nhan vien
		/// </summary>
		/// <param name="strEmpID">Ma so nhan vien</param>
		/// <returns>DataRow</returns>
		public static DataRow GetDataByID(Object strID)
		{
			DataRow iRow = clsCommon.GetDataRow("WF_spfrmCARDKEY @Activity='GetDataByID',@CardKeyID = " + strID);
			if(iRow !=null)
				return iRow;
			else
				return null;
		}
		/// <summary>
		/// Lay thong tin nhan vien theo dieu kien loc
		/// </summary>
		public static DataTable GetEmpList(string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strPosition, string strJobCode, string strLocation, string strCompany, string strStatus,
			string strCardType,string strFromDate,string strToDate)
		{
			string strSql = "WF_spfrmCARDKEY @Activity='GetDataListSearch',@EmpID=N'" + strEmpID + "', @EmpName=N'" + strEmpName
				+ "', @LSLevel1Code='" + strLevel1 + "', @LSLevel2Code='" + strLevel2 + "',@LSLevel3Code='" + strLevel3
				+ "',@LSPositionCode='" + strPosition + "',@LSJobCodeCode='" + strJobCode 
				+ "',@LSLocationCode='" + strLocation + "',@LSCompanyCode='" + strCompany 
				+ "',@CardKeyType=N'" + strCardType + "',@FromDate=N'" + strFromDate.Trim() + "',@ToDate=N'" + strToDate + "'";
			if (strStatus != "")
				strSql = strSql + ", @Status='" + strStatus + "'";
			DataTable dtb = clsCommon.GetDataTable(strSql);
			return dtb;
		}

		public static string InsertCardKey(DataGrid grdCardKey)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "WF_spfrmCARDKEY";			
			try
			{				
				for(int i = 0; i< grdCardKey.Items.Count;i++)
				{
					if(((CheckBox)grdCardKey.Items[i].FindControl("chkSelect")).Checked == true)
					{
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Save";
						cmd.Parameters.Add("@EmpID",SqlDbType.NVarChar,12).Value = grdCardKey.Items[i].Cells[2].Text.Trim();
						cmd.Parameters.Add("@CardKeyType",SqlDbType.NVarChar,12).Value = ((DropDownList)grdCardKey.Items[i].FindControl("cbolstCardType")).SelectedValue.Trim();
						cmd.Parameters.Add("@IssuedDate",SqlDbType.NVarChar,12).Value = ((TextBox)grdCardKey.Items[i].FindControl("txtlstIssuedDate")).Text.Trim();
						cmd.Parameters.Add("@CardNo",SqlDbType.NVarChar,12).Value = ((TextBox)grdCardKey.Items[i].FindControl("txtlstCardNo")).Text.Trim();
						cmd.Parameters.Add("@PinNo",SqlDbType.NVarChar,12).Value = ((TextBox)grdCardKey.Items[i].FindControl("txtlstPinNo")).Text.Trim();
						cmd.Parameters.Add("@CostAmount",SqlDbType.Money).Value = ((TextBox)grdCardKey.Items[i].FindControl("txtlstCostAmount")).Text.Trim();
						cmd.Parameters.Add("@Note",SqlDbType.NVarChar,12).Value = ((TextBox)grdCardKey.Items[i].FindControl("txtlstNote")).Text.Trim();
						cmd.ExecuteNonQuery();
					}
				}
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
	}
}
