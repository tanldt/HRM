using System;
using System.Data;
using iHRPCore.Com;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;
using System.Web.UI;
using iHRPCore.Com.Oracle;
 using System.IO; 
using iHRPCore.HRComponent;

namespace iHRPCore.TRComponent
{
	

	#region TRAINING PLAN
	public class clsTRPlan
	{
		public static DataTable GetTrainingList(string strLanguage, string strWhere)
		{
			try
			{
				string strSql= "TR_spfrmPLAN @Activity='TrainingList', @LanguageID ='" + strLanguage +"',"
					+ "@where = '" + strWhere + "'";

				DataTable dtb = new DataTable();
				dtb =  clsCommon.GetDataTable(strSql);
				return dtb;
			}
			catch(Exception exp)
			{
				DataTable dtb = new DataTable();
				return dtb;
			}
			
		}
		public static DataTable GetTrainingSubject(string strLanguage, string strTrainingPlanID)
		{
			try
			{
				string strSQL= "TR_spfrmTRAININGPLANSUBJECT @Activity='GetByTrainingPlanID', @LanguageID = '" + strLanguage + "', @TrainingPlanID = '" + strTrainingPlanID + "'";
				return clsCommon.GetDataTableHasID(strSQL);
			}
			catch(Exception exp)
			{
				return new DataTable(); 
			}
		}
		/// <summary>
		/// Get by Plan ID
		/// </summary>
		public static DataRow GetDataByID(string strSpName,string strKeyField,string strValue)
		{
			DataRow iRow = clsCommon.GetDataRow(strSpName + " @Activity='GetDataByID', @" + strKeyField + " = '" + strValue +"'");
			if(iRow !=null)
				return iRow;
			else
				return null;
		}

		/// <summary>
		/// AddNew mot ke hoach dao tao
		/// </summary>
		/// <param name="Page">Trang màn hình kế hoạch đào tạo</param>
		/// <param name="lstSubjectID">Danh sách môn học được chọn cho kế hoạch đào tạo</param>
		/// <param name="CuorseSub">Các môn hoc theo khóa học hay chọn từng môn học</param>
		/// <returns>True : thành công</returns>
		public static bool AddNewPlan(Control Page,string lstSubjectID,string CuorseSub)
		{
			string strProcedureName = "TR_spfrmPLAN";
			DataTable dtObj = new DataTable() ;
			dtObj = clsCommon.GetDataTable("sp_Common @ObjName=N'" + strProcedureName + "'");			
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
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "AddNew";				
				
				//------------
				clsCommon.strControl = "";
				clsCommon.GetAllControl(Page);
				string[] arrControl = clsCommon.strControl.Trim().Split('$');
				//--------------
				for(int i=0;i<arrControl.Length-1;i++)
				{
					DataRow[] iRow = dtObj.Select("columnname ='@" + arrControl.GetValue(i).ToString().Trim().Substring(3) + "'");
					
					if(iRow.Length > 0)
					{
						SqlParameter param = new SqlParameter();
						param.ParameterName = iRow[0]["columnname"].ToString().Trim();
						param.SqlDbType = clsCommon.ConvertDataType(iRow[0]["datatype"].ToString().Trim());
						param.Size = clsCommon.SafeDataInteger(iRow[0]["size"].ToString().Trim());						
						string strValue = clsCommon.GetValueControl(Page,arrControl.GetValue(i).ToString().Trim());
						if(param.SqlDbType==SqlDbType.Real)
							strValue = strValue.Replace(",","");
						if(strValue.Trim()=="")
							param.Value = DBNull.Value ;
						else
						{
							if(param.SqlDbType == SqlDbType.Bit)
								param.Value = (strValue=="True" || strValue=="1")?1:0; //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim()).Trim()=="True"?1:0;
							else
								param.Value = strValue.Replace(@"'",""); //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim());
						}
						cmd.Parameters.Add(param);
					}
				}
				cmd.Parameters.Add("@ReturnMess", SqlDbType.NVarChar,1000).Direction = ParameterDirection.InputOutput;
				cmd.ExecuteNonQuery();
				string sErrMess= cmd.Parameters["@ReturnMess"].Value.ToString().Trim();
				if (sErrMess != "" && sErrMess != "2")
				{
					if(CuorseSub.Trim()=="1")
					{
						if(lstSubjectID.Trim() !="")
						{
							string[] arrIDSubject = lstSubjectID.Trim().Split('$');
							cmd.CommandText = "TR_spfrmTRAININGPLANSUBJECT";
							for(int i=0;i<arrIDSubject.Length;i++)
							{
								//AddNew Training plan subject
								
								cmd.Parameters.Clear(); 
									
								cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 20).Value = "AddNew";
								cmd.Parameters.Add("@TrainingPlanID",SqlDbType.NVarChar,12).Value=sErrMess;
								cmd.Parameters.Add("@LSTrainingSubjectID",SqlDbType.NVarChar,12).Value=	arrIDSubject[i];			
								cmd.ExecuteNonQuery();
							}
						}
					}
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
		public static bool UpdatePlan(Control Page,string lstSubjectID,string CuorseSub,string strKeyID,string strKeyValue)
		{
			string strProcedureName = "TR_spfrmPLAN";
			DataTable dtObj = new DataTable();
			dtObj = clsCommon.GetDataTable("sp_Common @ObjName=N'" + strProcedureName + "'");
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
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Update";				
				//------------
				clsCommon.strControl="";
				clsCommon.GetAllControl(Page);
				clsCommon.strControl += "txt" + strKeyID + "$";

				string[] arrControl = clsCommon.strControl.Trim().Split('$');
				//--------------
				for(int i=0;i<arrControl.Length-1;i++)
				{
					DataRow[] iRow = dtObj.Select("columnname ='@" + arrControl.GetValue(i).ToString().Trim().Substring(3) + "'");
					if(iRow.Length > 0)
					{
						SqlParameter param = new SqlParameter();
						param.ParameterName = iRow[0]["columnname"].ToString().Trim();
						param.SqlDbType = clsCommon.ConvertDataType(iRow[0]["datatype"].ToString().Trim());
						param.Size = clsCommon.SafeDataInteger(iRow[0]["size"].ToString().Trim());
						string strValue = clsCommon.GetValueControl(Page,arrControl.GetValue(i).ToString().Trim());						
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
								else
									param.Value = strValue; //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim());
							}
						}
						cmd.Parameters.Add(param);
					}
				}
				cmd.Parameters.Add("@ReturnMess", SqlDbType.NVarChar,1000).Direction = ParameterDirection.InputOutput;
				cmd.ExecuteNonQuery();
				string sErrMess= cmd.Parameters["@ReturnMess"].Value.ToString().Trim();
				if (sErrMess == "")
				{
					
					if(CuorseSub.Trim()=="1")
					{
						if(lstSubjectID.Trim() !="")
						{
							//Delete all Training plan subject
							cmd.CommandText = "TR_spfrmTRAININGPLANSUBJECT";
							cmd.Parameters.Clear();
							cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 20).Value = "Delete";
							cmd.Parameters.Add("@TrainingPlanID",SqlDbType.NVarChar,12).Value=strKeyValue;
							cmd.ExecuteNonQuery();
							//Insert all again Training plan suject
							string[] arrIDSubject = lstSubjectID.Trim().Split('$');
							
							for(int i=0;i<arrIDSubject.Length;i++)
							{
								//AddNew Training plan subject
								
								cmd.Parameters.Clear(); 
									
								cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 20).Value = "AddNew";
								cmd.Parameters.Add("@TrainingPlanID",SqlDbType.NVarChar,12).Value=strKeyValue;
								cmd.Parameters.Add("@LSTrainingSubjectID",SqlDbType.NVarChar,12).Value=	arrIDSubject[i];			
								cmd.ExecuteNonQuery();
							}							
						}
					}
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
		public static void LoadComboCourse(DropDownList pCtl,string strType)
		{
			string strSQL="TR_spfrmDeptDevPlan @Activity='LoadComboCourse',@Type="+strType;
			clsCommon.BindDropDownListControl(pCtl,strSQL,"ID","CourseName",true);
		}
		/// <summary>
		/// Get data for List on form
		/// </summary>
		public static DataTable GetForGrid(string strIsCompany, string strLanguage)
		{
			try
			{
				string strSQL= "TR_spfrmPLAN @Activity='GetAllGrid', @LanguageID = '" + strLanguage + "', @IsCompany = '" + strIsCompany + "'";
				return clsCommon.GetDataTableHasID(strSQL);
			}
			catch(Exception exp)
			{
				return new DataTable();
			}
		}

//////////////////////////////////////////////////////////////////////////////////////

		public static DataTable SearchPlan(string strLevel2ID,string strLevel3ID,string strPositionCode,
		string strLocationCode,string strFromDate,string strToDate,string strType,string strCourseID)
		{
			string strSQL="TR_spfrmDeptDevPlan @Activity='SearchPlan',@Level2ID='"+strLevel2ID+"',";
			strSQL+="@Level3ID='"+strLevel3ID+"',";
			strSQL+="@PositionCode='"+strPositionCode+"',";
			strSQL+="@LocationCode='"+strLocationCode+"',";
			strSQL+="@FromDate='"+strFromDate+"',";
			strSQL+="@ToDate='"+strToDate+"',";
			if(strType.Length>0)
				strSQL+="@Type="+strType+",";
			strSQL+="@CourseID='"+strCourseID+"'";

			return clsCommon.GetDataTableHasID(strSQL);
		}
//////////////////////////////////////////////////////////////////////////////////////
		public static void LoadComboExistingYears(DropDownList pCtrl)
		{
			string strSQL="TR_spfrmDeptDevPlan @Activity='LoadComboExistingYear'";			
			clsCommon.BindDropDownListControl(pCtrl,strSQL,"Years","Years",true);
		}

		public static DataTable SearchPlanYear(string strLevel2ID,string strLevel3ID,string strPositionCode,
			string strLocationCode,string strYear,string strType)
		{
			string strSQL="TR_spfrmDeptDevPlan @Activity='SearchPlanYear',@Level2ID='"+strLevel2ID+"'";
			
			if(strLevel3ID.Length>0) strSQL+=",@Level3ID='"+strLevel3ID+"'";		

			if(strPositionCode.Length>0) strSQL+=",@PositionCode='"+strPositionCode+"'";			

			if(strLocationCode.Length>0) strSQL+=",@LocationCode='"+strLocationCode+"'";
			
			if(strType.Length>0) strSQL+=",@Type="+strType;
 
			if(strYear.Length>0) strSQL+=",@Years="+strYear;			

			return clsCommon.GetDataTableHasID(strSQL);
		}

		public static string IsComplete(string strEmpID,string strCourseID,string strType)
		{
			string strSQL="TR_spfrmDeptDevPlan @Activity='IsComplete',@EmpID='"+strEmpID+"',@CourseID='"+strCourseID+"',@Type="+strType;
			DataRow dr=clsCommon.GetDataRow(strSQL);
			if(dr!=null)
				if(dr["Complete"].ToString()=="x") return "YES";
				else return "NO";
			else return "NO";	
		}

		public static DataTable ReportDeptDevPlan(string strLevel2ID,string strLevel3ID,string strPositionCode,
			string strLocationCode,string strYear,string strType)
		{
			DataTable dt=SearchPlanYear(strLevel2ID,strLevel3ID,strPositionCode,strLocationCode,strYear,strType);
			for(int i=0;i<dt.Rows.Count;i++)
				dt.Rows[i]["Status"]=IsComplete(dt.Rows[i]["EmpID"].ToString(),dt.Rows[i]["CourseID"].ToString(),dt.Rows[i]["Type"].ToString());

			// TEST
			dt.Rows[0]["Status"]="YES";
			return dt;
		}

		// PLAN NEW
		public static DataTable LoadDivision()
		{
			string strSQL="TR_spfrmDeptDevPlan @Activity='LoadDivision'";
			return clsCommon.GetDataTable(strSQL);
		}

		public static DataTable LoadDepartment(string strDivisionCode)
		{
			string strSQL="TR_spfrmDeptDevPlan @Activity='LoadDepartment',@DivisionCode='"+strDivisionCode+"'";
			return clsCommon.GetDataTableHasID(strSQL);
		}

		public static DataTable LoadSection(string strDeptCode)
		{
			string strSQL="TR_spfrmDeptDevPlan @Activity='LoadSection',@DeptCode='"+strDeptCode+"'";
			return clsCommon.GetDataTableHasID(strSQL);
		}

		public static DataTable LoadPosition()
		{
			string strSQL="TR_spfrmDeptDevPlan @Activity='LoadPosition'";
			return clsCommon.GetDataTableHasID(strSQL);
		}

		public static DataTable LoadTR()
		{
			string strSQL="TR_spfrmDeptDevPlan @Activity='LoadTR'";
			return clsCommon.GetDataTableHasID(strSQL);
		}

		public static DataTable LoadLOMA()
		{
			string strSQL="TR_spfrmDeptDevPlan @Activity='LoadLOMA'";
			return clsCommon.GetDataTableHasID(strSQL);
		}
		
		public static DataTable LoadPruU()
		{
			string strSQL="TR_spfrmDeptDevPlan @Activity='LoadPruU'";
			return clsCommon.GetDataTableHasID(strSQL);
		}

		public static DataTable SearchPlanAll()
		{
			string strSQL="TR_spfrmDeptDevPlan @Activity='SearchPlanAll'";
			return clsCommon.GetDataTableHasID(strSQL);
		}

		public static bool AddNew(string strLevel2ID,string strLevel3ID,string strPositionCode,
			string strLocationCode,string strFromDate,string strToDate,string strType,string strCourseID,string strRemark)
		{
			string strSQL="TR_spfrmDeptDevPlan @Activity='Save',@Level2ID='"+strLevel2ID+"',";
			strSQL+="@Level3ID='"+strLevel3ID+"',";
			strSQL+="@PositionCode='"+strPositionCode+"',";
			strSQL+="@LocationCode='"+strLocationCode+"',";
			strSQL+="@FromDate='"+strFromDate+"',";
			if(strToDate.Length>0)
				strSQL+="@ToDate='"+strToDate+"',";
			else strSQL+="@ToDate=NULL,";
			if(strType.Length>0)
				strSQL+="@Type="+strType+",";
			strSQL+="@CourseID='"+strCourseID+"',";
			strSQL+="@Remark=N'"+strRemark+"'";

			clsCommon.GetDataTableHasID(strSQL);
			return true;
		}

		public static bool Update(string strID,string strLevel2ID,string strLevel3ID,string strPositionCode,
			string strLocationCode,string strFromDate,string strToDate,string strType,string strCourseID,string strRemark)
		{
			string strSQL="TR_spfrmDeptDevPlan @Activity='Update',@ID="+strID+",@Level2ID='"+strLevel2ID+"',";
			strSQL+="@Level3ID='"+strLevel3ID+"',";
			strSQL+="@PositionCode='"+strPositionCode+"',";
			strSQL+="@LocationCode='"+strLocationCode+"',";
			strSQL+="@FromDate='"+strFromDate+"',";
			if(strToDate.Length>0)
				strSQL+="@ToDate='"+strToDate+"',";
			else strSQL+="@ToDate=NULL,";
			if(strType.Length>0)
				strSQL+="@Type="+strType+",";
			strSQL+="@CourseID='"+strCourseID+"',";
			strSQL+="@Remark=N'"+strRemark+"'";

			clsCommon.GetDataTableHasID(strSQL);
			return true;
		}
	
	}
	
	#endregion

	#region TRAINING ADC
	class clsTrainingPlanADC
	{
		public static DataTable getEmpForPlan(string strEmpCode, string strEmpName, string strCompanyID, string strLevel1ID, string strLevel2ID, string strLevel3ID, string strStatus,
												string strFromDate, string strToDate,
												string strEmpTypeID,
												string strJobTitleID,
												string Year,string strTrainingCourseID, System.Web.UI.Page pPage)
		{
			DataTable dtData= new DataTable();
			try
			{
				dtData = clsCommon.GetDataTable("TR_spfrmTRAININGPLANADC @Activity='getEmpForPlan',	@EmpID=N'" + strEmpCode + "', @EmpName=N'" + strEmpName 
					+ "',@LSCompanyID='" + strCompanyID + "',@LSLevel1ID='" + strLevel1ID + "',@LSLevel2ID='" + strLevel2ID + "',@LSLevel3ID='" + strLevel3ID + "',@Status='" + strStatus 
					+"',@Fromdate='" + strFromDate + "',@ToDate='" + strToDate 
					+ "',@LSScopeAbilityID='" + strEmpTypeID 
					+ "',@LSJobTitleID='" + strJobTitleID 
					+ "',@PlanYear='" + Year 
					+ "',@TrainingCourseID='" + strTrainingCourseID + "'");

				return dtData;
			}
			catch(Exception ex)
			{
				return null;
			}
			finally
			{
				dtData.Dispose();
			}
		}

		public static DataTable getEmpForCourse(string strEmpCode, string strEmpName, string strCompanyID, string strLevel1ID, string strLevel2ID, string strLevel3ID, string strScopeAbilityID
			,string strJobTitleID,string strFromDate,string strToDate,string strStatus, string Year,string strTrainingExpectCode,string strSaveStatus,System.Web.UI.Page pPage)
		{
			DataTable dtData= new DataTable();
			try
			{
				dtData = clsCommon.GetDataTable("TR_spfrmTRAININGCOURSEEMP @Activity='getEmpForCourse',@EmpID=N'" + 
					strEmpCode + "', @EmpName=N'" + strEmpName + "',@LSCompanyID='" + strCompanyID + "',@LSLevel1ID='" + 
					strLevel1ID + "',@LSLevel2ID='" + strLevel2ID + "',@LSLevel3ID='" + 
					strLevel3ID + "',@Status='" + strStatus + "',@Fromdate='" + strFromDate + "',@ToDate='" + strToDate + "',@LSScopeAbilityID='" + strScopeAbilityID + "',@LSJobTitleID='" + strJobTitleID + "',@PlanYear='" + Year + "',@TrainingCourseID='" + strTrainingExpectCode + "',@SaveStatus=" + strSaveStatus );
				return dtData;
			}
			catch(Exception ex)
			{
				return null;
			}
			finally
			{
				dtData.Dispose();
			}
		}

		public static string sImpact(DataGrid dtgList, string strYear)
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
				cmd.CommandText = "TR_spfrmTRAININGPLANACB";
   
				CheckBox obj = new CheckBox();
				for (int i = 0;i<= dtgList.Items.Count - 1;i++)
				{
					obj = (CheckBox) dtgList.Items[i].FindControl("chkSelect");					
					if (obj.Checked)
					{
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "Save";					

						/*cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = ((TextBox)dtgList.Items[i].FindControl("txtEmpID")).Text.Trim();   
						cmd.Parameters.Add("@PlanYear", SqlDbType.NVarChar, 12).Value = dtgList.Items[i].Cells[0].Text.Trim();
						cmd.Parameters.Add("@Note", SqlDbType.NVarChar, 255).Value = ((TextBox)dtgList.Items[i].FindControl("txtNote")).Text.Trim();   						
						cmd.Parameters.Add("@ReturnMess", SqlDbType.NVarChar,1000).Direction = ParameterDirection.InputOutput;
						cmd.Parameters.Add("@LanguageID", SqlDbType.NChar,2).Value = "VN";*/		

						if (strYear.Trim()=="")
							cmd.Parameters.Add("@PlanYear", SqlDbType.NVarChar).Value=DBNull.Value;
						else
							cmd.Parameters.Add("@PlanYear", SqlDbType.NVarChar,4).Value= strYear.Trim();
		

						if (dtgList.Items[i].Cells[0].Text.Trim()=="")
							cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar).Value=DBNull.Value;
						else
							cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar,12).Value= dtgList.Items[i].Cells[0].Text.Trim(); 
		

						if (dtgList.Items[i].Cells[4].Text.Trim()=="")
							cmd.Parameters.Add("@LSJobTitleID", SqlDbType.NVarChar).Value=DBNull.Value;
						else
							cmd.Parameters.Add("@LSJobTitleID", SqlDbType.NVarChar,12).Value= dtgList.Items[i].Cells[1].Text.Trim();
		

						cmd.Parameters.Add("@IsComplement", SqlDbType.Bit).Value=((CheckBox)dtgList.Items[i].FindControl("chkIsComplement")).Checked;
							
						if (dtgList.Items[i].Cells[6].Text.Trim()=="")
							cmd.Parameters.Add("@Achieve", SqlDbType.NVarChar).Value=DBNull.Value;
						else
							cmd.Parameters.Add("@Achieve", SqlDbType.NVarChar,255).Value= dtgList.Items[i].Cells[6].Text.Trim();
		

						if (dtgList.Items[i].Cells[7].Text.Trim()=="")
							cmd.Parameters.Add("@Require", SqlDbType.NVarChar).Value=DBNull.Value;
						else
							cmd.Parameters.Add("@Require", SqlDbType.NVarChar,255).Value= dtgList.Items[i].Cells[7].Text.Trim();
		

						if (((DropDownList)dtgList.Items[i].FindControl("cboLSTrainingFormID")).SelectedValue=="")
							cmd.Parameters.Add("@LSTrainingFormID", SqlDbType.NVarChar).Value=DBNull.Value;
						else
							cmd.Parameters.Add("@LSTrainingFormID", SqlDbType.NVarChar,12).Value= ((DropDownList)dtgList.Items[i].FindControl("cboLSTrainingFormID")).SelectedValue;
		

						if (((DropDownList)dtgList.Items[i].FindControl("cboLSTrainingContentID")).SelectedValue=="")
							cmd.Parameters.Add("@LSTrainingContentID", SqlDbType.NVarChar).Value=DBNull.Value;
						else
							cmd.Parameters.Add("@LSTrainingContentID", SqlDbType.NVarChar,12).Value= ((DropDownList)dtgList.Items[i].FindControl("cboLSTrainingContentID")).SelectedValue;
		

						if (((TextBox)dtgList.Items[i].FindControl("txtReason")).Text.Trim()=="")
							cmd.Parameters.Add("@Reason", SqlDbType.NVarChar).Value=DBNull.Value;
						else
							cmd.Parameters.Add("@Reason", SqlDbType.NVarChar,255).Value= ((TextBox)dtgList.Items[i].FindControl("txtReason")).Text.Trim();
		

						if (((TextBox)dtgList.Items[i].FindControl("txtTimePlan")).Text.Trim()=="")
							cmd.Parameters.Add("@TimePlan", SqlDbType.NVarChar).Value=DBNull.Value;
						else
							cmd.Parameters.Add("@TimePlan", SqlDbType.NVarChar,8).Value= ((TextBox)dtgList.Items[i].FindControl("txtTimePlan")).Text.Trim();
		

						if (((TextBox)dtgList.Items[i].FindControl("txtCost")).Text.Trim() == "")
							cmd.Parameters.Add("@Cost", SqlDbType.Decimal).Value=DBNull.Value;
						else
							cmd.Parameters.Add("@Cost", SqlDbType.Decimal).Value=((TextBox)dtgList.Items[i].FindControl("txtCost")).Text.Trim();

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
	class clsTrainingPlanAppove
	{
		public static DataTable TrainingPlanByYear(string strYear)
		{
			DataTable dtData= new DataTable();
			try
			{
				dtData= clsCommon.GetDataTable("TR_spfrmTRAININGPLANACB @Activity='TrainingPlanByYear',@PlanYear='" + strYear + "'");
				return dtData;
			}
			catch (Exception ex)
			{
				return null;
			}
			finally
			{
				dtData.Dispose();
			}
		}
		public static DataRow getDataByID(string TrainingPlanID)
		{
			DataRow drData= clsCommon.GetDataRow("TR_spfrmTRAININGPLANACB @Activity='GetDataByID',@TrainingPlanID='" + TrainingPlanID + "'");
			if (drData!=null)
			{
				return drData;
			}
			else
			{
				return null;
			}
		}
		public static bool Tranfer(string  strStoreName,string strKeyField,SqlDbType pType, int intSize, string strListID)
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
					cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Tranfer";
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
	}
	class clsTrainingRation
	{
		//cangtt - updatebyKey return string
		public static string sUpdateByKey(string strCourseID,bool bMorning,
			string MorningPart,string MorningPrice,
			bool bNoon, string NoonPart, string NoonPrice,
			bool bAfternoon, string AfternoonPart, string AfternoonPrice)
		{
			string sErrMess="";
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "TR_spfrmTRAININGCOURSE";
			//-------
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "UpdateRation";
				cmd.Parameters.Add("@TrainingCourseID",SqlDbType.NVarChar,12).Value=strCourseID;
				cmd.Parameters.Add("@Morning",SqlDbType.Bit).Value=bMorning;
				cmd.Parameters.Add("@Noon",SqlDbType.Bit).Value=bNoon;
				cmd.Parameters.Add("@Afternoon",SqlDbType.Bit).Value=bAfternoon;
				cmd.Parameters.Add("@MorningPart",SqlDbType.Int).Value=MorningPart.Replace(",","")==""?"0":MorningPart.Replace(",","");
				cmd.Parameters.Add("@NoonPart",SqlDbType.Int).Value=NoonPart.Replace(",","")==""?"0":NoonPart.Replace(",","");
				cmd.Parameters.Add("@AfternoonPart",SqlDbType.Int).Value=AfternoonPart.Replace(",","")==""?"0":AfternoonPart.Replace(",","");

				cmd.Parameters.Add("@MorningPrice",SqlDbType.Decimal).Value=MorningPrice.Replace(",","")==""?"0":MorningPrice.Replace(",","");
				cmd.Parameters.Add("@NoonPrice",SqlDbType.Decimal).Value=NoonPrice.Replace(",","")==""?"0":NoonPrice.Replace(",","");
				cmd.Parameters.Add("@AfternoonPrice",SqlDbType.Decimal).Value=AfternoonPrice.Replace(",","")==""?"0":AfternoonPrice.Replace(",","");

				cmd.Parameters.Add("@ReturnMess", SqlDbType.NVarChar,1000).Direction = ParameterDirection.InputOutput;
				cmd.Parameters.Add("@LanguageID", SqlDbType.NChar,2).Value = "VN";
					
				cmd.ExecuteNonQuery();
				sErrMess= cmd.Parameters["@ReturnMess"].Value.ToString();				
				sqlTran.Commit();
				if (sErrMess!="")
					return sErrMess;
				else
					return "";				
				sqlTran.Commit();				
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
	}
	class clsCourseAppraisal
	{
		public static string DeleteCourseAppraisal(string strID)
		{
			string strErr = "";
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);		
			SQLconn.Open();
			cmd.Connection = SQLconn;
			cmd.Transaction= SQLconn.BeginTransaction();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "TR_spfrmCOURSEAPPRAISAL";
			try
			{				
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Delete";							
				cmd.Parameters.Add("@CourseAppraisalID",SqlDbType.NVarChar,12).Value = strID;
				
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
		public static DataRow GetDataByID(string strTrainingCourseID,string strLanguage)
		{
			DataRow iRow = clsCommon.GetDataRow("TR_spfrmCOURSEAPPRAISAL @Activity='GetDataByID',@CourseAppraisalID = N'" + strTrainingCourseID + "',@LanguageID = N'" + strLanguage + "'");
			if(iRow !=null)
				return iRow;
			else
				return null;
		}
		public static DataTable LstCourseAppraisalByTrainingCourse(string strTrainingCourseID,string strLanguage)
		{
			DataTable dtData= new DataTable();
			try
			{
				dtData= clsCommon.GetDataTable("TR_spfrmCOURSEAPPRAISAL @Activity='LstDataByTrainingCourse',@TrainingCourseID=N'" + strTrainingCourseID + "',@LanguageID = N'" + strLanguage + "'");
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

	class clsTrainingDocument
	{
		public static string DeleteTrainingDocument(string strID)
		{
			string strErr = "";
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);		
			SQLconn.Open();
			cmd.Connection = SQLconn;
			cmd.Transaction= SQLconn.BeginTransaction();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "TR_spfrmTRAININGDOCUMENT";
			try
			{				
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Delete";							
				cmd.Parameters.Add("@TrainingDocumentID",SqlDbType.NVarChar,12).Value = strID;
				
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
		public static DataRow GetDataByID(string strTrainingDocumentID,string strLanguage)
		{
			DataRow iRow = clsCommon.GetDataRow("TR_spfrmTRAININGDOCUMENT @Activity='GetDataByID',@TrainingDocumentID = N'" + strTrainingDocumentID + "',@LanguageID = N'" + strLanguage + "'");
			if(iRow !=null)
				return iRow;
			else
				return null;
		}
		public static DataTable LstTrainingDocumentByTrainingCourse(string strTrainingDocumentID,string strLanguage)
		{
			DataTable dtData= new DataTable();
			try
			{
				dtData= clsCommon.GetDataTable("TR_spfrmTRAININGDOCUMENT @Activity='LstTrainingDocumentByTrainingCourse',@TrainingCourseID=N'" + strTrainingDocumentID + "',@LanguageID = N'" + strLanguage + "'");
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
	class clsRollCall
	{
		public static DataTable getRollCall(string strTrainingCourseID)
		{
			DataTable dtData= new DataTable();
			try
			{
				dtData= clsCommon.GetDataTable("TR_spfrmTRAININGROLLCALL @Activity='BindDatagird',@TrainingCourseID='" + strTrainingCourseID + "'");
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
		/////////////////////////////////////////////////////
		///Save Update mark for training course
		////////////////////////////////////////////////////
		public static string SaveRollCall(string sSaveData, string strTrainingCourseID, string strLanguage)
		{			
					
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "TR_spfrmTRAININGROLLCALL";
			string []Step1 = sSaveData.Split('@');
			try
			{	
				for(int i=0;i< Step1.Length-1;i++)
				{
					
					string temp=Step1.GetValue(i).ToString();
					string [] Step2=temp.Split('$');
					cmd.Parameters.Clear(); 
					cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 20).Value = "Save";			
					cmd.Parameters.Add("@TrainingCourseID",SqlDbType.NVarChar,12).Value=strTrainingCourseID;				
					cmd.Parameters.Add("@DateID", SqlDbType.NVarChar).Value=Step2.GetValue(1).ToString();
					cmd.Parameters.Add("@Type",SqlDbType.NVarChar,2).Value=Step2.GetValue(2).ToString();
					cmd.Parameters.Add("@EmpID",SqlDbType.NVarChar,12).Value=Step2.GetValue(0).ToString();
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
	}
	#endregion

	#region TRAINING COURSE
	class clsTrainingCourse
	{
		public static DataTable Load_dtgCourseFee(string pTrainingCourseID, string pLanguageID)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb= clsCommon.GetDataTable("TR_spfrmTrainingCourse @Activity='GetCourseFee', @TrainingCourseID='" + pTrainingCourseID +  "', @LanguageID = '" + pLanguageID + "'");
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
		public static DataTable Load_dtgCourseBudget(string pTrainingCourseID, string pLanguageID)
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb= clsCommon.GetDataTable("TR_spfrmTrainingCourse @Activity='GetCourseBudget', @TrainingCourseID='" + pTrainingCourseID +  "', @LanguageID = '" + pLanguageID + "'");
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
		public static DataRow getDataByID(string pTrainingCourseID)
		{
			DataRow drData;
			try
			{
				drData= clsCommon.GetDataRow("TR_spfrmTrainingCourse @Activity='getDataByID', @TrainingCourseID='" + pTrainingCourseID + "'");
				return drData;
			}
			catch
			{
				return null;
			}			
		}

		public static bool UpdateOutLine(string strTrainingCourseID)
		{
					
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "TR_spfrmTRAININGCOURSE";
			try
			{	
				cmd.Parameters.Clear(); 
						
				cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 20).Value = "UpdateOutLine";
				cmd.Parameters.Add("@TrainingCourseID",SqlDbType.NVarChar,255).Value= strTrainingCourseID;
				
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
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Page"></param>
		/// <param name="lstSubjectID"></param>
		/// <param name="CuorseSub"></param>
		/// <param name="strKeyID"></param>
		/// <param name="strKeyValue"></param>
		/// <param name="strPath"></param>
		/// <returns></returns>
		public static string UpdateTraining(Control Page,string lstSubjectID,string CuorseSub,string strKeyID,string strKeyValue,string strPath,string strRealPath)
		{
			
			string strProcedureName = "TR_spfrmTRAININGCOURSE";
			DataTable dtObj = new DataTable();
			dtObj = clsCommon.GetDataTable("sp_Common @ObjName=N'" + strProcedureName + "'");
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
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Update";				
				//------------
				clsCommon.strControl="";
				clsCommon.GetAllControl(Page);
				clsCommon.strControl += "txt" + strKeyID + "$";

				string[] arrControl = clsCommon.strControl.Trim().Split('$');
				//--------------
				for(int i=0;i<arrControl.Length-1;i++)
				{
					DataRow[] iRow = dtObj.Select("columnname ='@" + arrControl.GetValue(i).ToString().Trim().Substring(3) + "'");
					if(iRow.Length > 0)
					{
						SqlParameter param = new SqlParameter();
						param.ParameterName = iRow[0]["columnname"].ToString().Trim();
						param.SqlDbType = clsCommon.ConvertDataType(iRow[0]["datatype"].ToString().Trim());
						param.Size = clsCommon.SafeDataInteger(iRow[0]["size"].ToString().Trim());
						string strValue = clsCommon.GetValueControl(Page,arrControl.GetValue(i).ToString().Trim());						
						if(param.ParameterName.Trim()=="@"+strKeyID.Trim())
							param.Value = strKeyValue.Trim();
						else if(param.ParameterName.Trim()== "@OutLine")
						{
							if (strValue!="")
							{
								if (Path.GetExtension(strValue).ToLower().Trim()== ".xls" || Path.GetExtension(strValue).ToLower().Trim()== ".doc")
								{
									//string strFiletmp = strKeyValue.Trim() + Path.GetExtension(strValue).Trim();
									string strFiletmp = strKeyValue.Trim() + Path.GetFileName(strValue).Trim();
									param.Value = strPath + strFiletmp;
								}
								else
								{
									param.Value=DBNull.Value;
								}
							}
							else if(strPath != "")
							{
								param.Value = strRealPath;
							}
							else
							{
								param.Value=DBNull.Value;
							}
						}
						else
						{
							if(strValue.Trim()=="")
								param.Value = DBNull.Value ;
							else
							{
								if(param.SqlDbType == SqlDbType.Bit)
									param.Value = (strValue=="True" || strValue=="1")?1:0; //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim()).Trim()=="1"?true:false;
								else
									param.Value = strValue; //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim());
							}
						}
						cmd.Parameters.Add(param);
					}
				}
				cmd.Parameters.Add("@ReturnMess", SqlDbType.NVarChar,1000).Direction = ParameterDirection.InputOutput;
				cmd.ExecuteNonQuery();
				string sErrMess= cmd.Parameters["@ReturnMess"].Value.ToString().Trim();
				//Khong xoa danh sach cac mon hoc
				
				if (sErrMess.Length<12)
				{
					
					if(CuorseSub.Trim()=="1")
					{
						if(lstSubjectID.Trim() !="")
						{
							//Delete all Training plan subject
							cmd.CommandText = "TR_spfrmCOURSESUBJECT";
							cmd.Parameters.Clear();
							cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 20).Value = "Delete";
							cmd.Parameters.Add("@TrainingCourseID",SqlDbType.NVarChar,12).Value=strKeyValue;
							cmd.ExecuteNonQuery();
							//Insert all again Training plan suject
							string[] arrIDSubject = lstSubjectID.Trim().Split('$');
							
							for(int i=0;i<arrIDSubject.Length;i++)
							{
								//AddNew Training plan subject
								
								cmd.Parameters.Clear(); 
									
								cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 20).Value = "AddNew";
								cmd.Parameters.Add("@TrainingCourseID",SqlDbType.NVarChar,12).Value=strKeyValue;
								cmd.Parameters.Add("@LSTrainingSubjectID",SqlDbType.NVarChar,12).Value=	arrIDSubject[i];			
								cmd.ExecuteNonQuery();
							}							
						}
					}
				}
				
				sqlTran.Commit();
				return sErrMess;
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
		/// 
		/// </summary>
		/// <param name="Page"></param>
		/// <param name="lstSubjectID"></param>
		/// <param name="CuorseSub"></param>
		/// <returns></returns>
		public static string AddNewTrainingCourse(Control Page,string lstSubjectID,string CuorseSub,string strPath)
		{
			string strReturn = "";
			string strProcedureName = "TR_spfrmTRAININGCOURSE";
			DataTable dtObj = new DataTable() ;
			dtObj = clsCommon.GetDataTable("sp_Common @ObjName=N'" + strProcedureName + "'");			
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
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "AddNew";				
				
				//------------
				clsCommon.strControl = "";
				clsCommon.GetAllControl(Page);
				clsCommon.strControl += "txtWhere$";
				string[] arrControl = clsCommon.strControl.Trim().Split('$');
				//--------------
				for(int i=0;i<arrControl.Length-1;i++)
				{
					DataRow[] iRow = dtObj.Select("columnname ='@" + arrControl.GetValue(i).ToString().Trim().Substring(3) + "'");
					
					if(iRow.Length > 0)
					{
						SqlParameter param = new SqlParameter();
						param.ParameterName = iRow[0]["columnname"].ToString().Trim();
						param.SqlDbType = clsCommon.ConvertDataType(iRow[0]["datatype"].ToString().Trim());
						param.Size = clsCommon.SafeDataInteger(iRow[0]["size"].ToString().Trim());						
						string strValue = clsCommon.GetValueControl(Page,arrControl.GetValue(i).ToString().Trim());
						if (param.ParameterName.Trim()== "@Where")
						{
							param.Value =strPath;
						}
						else if (param.ParameterName.Trim()== "@OutLine")
						{							
							if (strValue!="")
							{
								if (Path.GetExtension(strValue).ToLower().Trim()== ".xls" || Path.GetExtension(strValue).ToLower().Trim()== ".doc")
								{
									//string strFiletmp = Path.GetExtension(strValue).Trim();
									string strFiletmp = Path.GetFileName(strValue).Trim();
									param.Value = strFiletmp;
								}
								else
								{
									param.Value=DBNull.Value;
								}
							}
							else
							{
								param.Value=DBNull.Value;
							}

						}
						else
						{
							if(param.SqlDbType==SqlDbType.Real)
								strValue = strValue.Replace(",","");
							if(strValue.Trim()=="")
								param.Value = DBNull.Value ;
							else
							{
								if(param.SqlDbType == SqlDbType.Bit)
									param.Value = (strValue=="True" || strValue=="1")?1:0; //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim()).Trim()=="True"?1:0;
								else
									param.Value = strValue.Replace(@"'",""); //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim());
							}
						}
						cmd.Parameters.Add(param);
					}
				}
				cmd.Parameters.Add("@ReturnMess", SqlDbType.NVarChar,1000).Direction = ParameterDirection.InputOutput;
				cmd.ExecuteNonQuery();
				string sErrMess= cmd.Parameters["@ReturnMess"].Value.ToString().Trim();
				strReturn=sErrMess;
				if (sErrMess.Length<12)
				{
					strReturn = sErrMess;
					if(CuorseSub.Trim()=="1")
					{
						if(lstSubjectID.Trim() !="")
						{
							string[] arrIDSubject = lstSubjectID.Trim().Split('$');
							cmd.CommandText = "TR_spfrmCOURSESUBJECT";
							for(int i=0;i<arrIDSubject.Length;i++)
							{
								//AddNew Training plan subject
								
								cmd.Parameters.Clear(); 
									
								cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 20).Value = "AddNew";
								cmd.Parameters.Add("@TrainingCourseID",SqlDbType.NVarChar,12).Value=sErrMess;
								cmd.Parameters.Add("@LSTrainingSubjectID",SqlDbType.NVarChar,12).Value=	arrIDSubject[i];			
								cmd.ExecuteNonQuery();
							}
						}
					}
				}
				sqlTran.Commit();
				return strReturn;
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
		///////////////////////////////////////////////////////////////////
		///Update Danh gia khoa hoc
		///
		public static bool UpdateAppraisal(string strTrainingCourseID, string strAppraisalType, string strAppraisal)
		{
					
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "TR_spfrmTRAININGCOURSE";
			try
			{	
				cmd.Parameters.Clear(); 
						
				cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 20).Value = "UpdateAppraisal";
				cmd.Parameters.Add("@TrainingCourseID",SqlDbType.NVarChar,255).Value= strTrainingCourseID;
				cmd.Parameters.Add("@Appraisal",SqlDbType.NVarChar,255).Value= strAppraisal;
				cmd.Parameters.Add("@AppraisalType", SqlDbType.Decimal).Value=strAppraisalType;
				
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
		
		////////////////////////////////////////////////////////////////////
		///Get data for Training plan collect by Amount
		/// Tham so: RadioButtonList
		/// RadioButtonList.value = 1: Theo cong ty
		///							2: Theo don vi
		///							3: Theo khoa hoc
		///							4: theo hinh thuc dao tao
		public static DataTable GetdataForCollect(RadioButtonList rbtCollect, string strYear, string strLanguage)
		{
			 
			string CollectType =  rbtCollect.SelectedValue.ToString().Trim(); 
			string strSql= "TR_spfrmPLAN @Activity='GetEmpAmount',@where='" + CollectType  +"', @LanguageID = '"+ strLanguage + "',@TrainingYear = '"+strYear+"'";
			DataTable dtb = clsCommon.GetDataTable(strSql);
			return dtb;
		}
		public static DataRow GetTrainingCourseByID(string strTrainingCourseID)
		{	
			DataRow iRow = clsCommon.GetDataRow("TR_spfrmTRAININGCOURSE" + " @Activity='GetDataByID', @TrainingCourseID ='" + strTrainingCourseID +"'");
			if(iRow !=null)
				return iRow;
			else
				return null;
		}
		/// <summary>
		/// strOrder: 0: Order by JobTitle
		///			  1: Order by Emp 	 
		/// </summary>
		
		public static DataTable GetTrainingForce(string strEmpCode,string strEmpName,string strLSCompanyID, 
			string strLSLevel1ID, string strLSLevel2ID, string strLSLevel3ID, string strStatus,string P_strOrder, string strLanguage)
		{
			string strCondition = "(A.EmpCode like ''%" + strEmpCode + "%'' or ''" + strEmpCode + "'' = '''')"
				+ " and (A.EmpName like N''%" + strEmpName + "%'' or ''" + strEmpName + "'' = '''')"
				+ " and (A.LSLevel1ID = ''" + strLSLevel1ID + "'' or ''" + strLSLevel1ID + "'' = '''')"
				+ " and (A.LSLevel2ID = ''" + strLSLevel2ID + "'' or ''" + strLSLevel2ID + "'' = '''')"
				+ " and (A.LSLevel3ID = ''" + strLSLevel3ID + "'' or ''" + strLSLevel3ID + "'' = '''')"
				+ " and (A.Status = ''" + strStatus + "'' or ''" + strStatus + "'' = '''')"
				+ " and (A.LSCompanyID = ''" + strLSCompanyID + "'' or ''" + strLSCompanyID + "'' = '''')";

			string strSql= "TR_spfrmTRAININGCOURSE @Activity='NeededSubject', @LanguageID ='" + strLanguage +"' ";
			strSql += ",@where=N'" + strCondition ;
			
			string strOrder ="";
			if(P_strOrder == "0")
				strOrder = " Order by JobTitleName ";
			else
				strOrder = " Order by A.EmpCode ";


			strSql += strOrder + "'";
			DataTable dtb = new DataTable();
			try
			{
				dtb = clsCommon.GetDataTable(strSql);
				return dtb;

			}
			catch(Exception exp)
			{
				return dtb;
			}
		}
	}
	#endregion
	public class clsTRAttendance
	{
		public static DataTable LoadCourseTR(string strTrainType)
		{
			string strSQL="TR_spfrmTrainingCourse @Activity='LoadCourseTR',@TrainType="+strTrainType;
			return clsCommon.GetDataTable(strSQL);
		}
		public static DataTable LoadCourseDate(string strCourseID,string strTrainType)
		{
			string strSQL="TR_spfrmTrainingCourse @Activity='LoadCourseDate',@TrainType="+strTrainType+",@CourseID='"+strCourseID+"'";
			return clsCommon.GetDataTable(strSQL);
		}

		public static DataTable LoadCourseDatePruUTraining(string strCourseID)
		{
			string strSQL="TR_spfrmPruTR @Activity='LoadCourseDate',@CourseID='"+strCourseID+"'";
			return clsCommon.GetDataTable(strSQL);
		}

		public static DataTable LoadCourseByCode(string strCourseCode)
		{
			string strSQL="TR_spfrmTRAININGCOURSE @Activity='GetDataByCode',@TrainingCourseCode=N'" + strCourseCode + "'";
			return clsCommon.GetDataTable(strSQL);
		}
		
		public static DataTable LoadSubjectByCourseID(string strCourseID, string sLanguageID)
		{
			string strSQL="TR_spfrmCOURSESUBJECT @Activity='GetSubjectByCourseID',@TrainingCourseID='" + strCourseID + "',@LanguageID='" + sLanguageID + "'";
			return clsCommon.GetDataTable(strSQL);
		}	

	}


	#region "UPDATE MARKS FOR TRAINING COURSE"
	class clsTrainingSubjectPassMarks
	{
		/////////////////////////////////////////////////////
		///Save Update mark for training course
		////////////////////////////////////////////////////
		public static bool SaveMarks(DataGrid dtgList, string strTrainingCourseID, string strLanguage)
		{
			TextBox txtPassMark;
			TextBox txtTimeLimit;
			CheckBox chkPeriod;
			TextBox txtScale;
			TextBox txtLecturer;
			DropDownList txtLecturer1;
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "TR_spfrmCOURSESUBJECT";
			try
			{	
				for(int i=0;i< dtgList.Items.Count;i++)
				{
					txtPassMark = (TextBox)dtgList.Items[i].FindControl("txtPassMarks");
					txtPassMark.Text  = txtPassMark.Text.Trim() != ""?txtPassMark.Text.Trim():"0";

					txtTimeLimit=(TextBox)dtgList.Items[i].FindControl("txtTimeLimit");
					
					chkPeriod = (CheckBox)dtgList.Items[i].FindControl("chkPeriod");

					txtScale=(TextBox)dtgList.Items[i].FindControl("txtScale");
					txtScale.Text=txtScale.Text.Trim() != ""?txtScale.Text.Trim():"100";

					txtLecturer=(TextBox)dtgList.Items[i].FindControl("txtExternalLecturer");
					txtLecturer1=(DropDownList)dtgList.Items[i].FindControl("cboEmpIDLecturer");
					cmd.Parameters.Clear(); 
					cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 20).Value = "UpdatePassmarks";			
					cmd.Parameters.Add("@CourseSubjectID",SqlDbType.NVarChar,12).Value=dtgList.Items[i].Cells[0].Text.Trim();				
					cmd.Parameters.Add("@PassMarks", SqlDbType.Decimal).Value=txtPassMark.Text.Trim();
					if (txtTimeLimit.Text=="")
						cmd.Parameters.Add("@TimeLimit",SqlDbType.Int).Value=DBNull.Value;
					else
						cmd.Parameters.Add("@TimeLimit",SqlDbType.Int).Value=txtTimeLimit.Text;

					cmd.Parameters.Add("@Period",SqlDbType.Bit).Value=chkPeriod.Checked;
					cmd.Parameters.Add("@Scale",SqlDbType.Decimal).Value=txtScale.Text;
					if (txtLecturer.Text.Trim()!="")
						cmd.Parameters.Add("@Lecturer",SqlDbType.NVarChar,255).Value=txtLecturer.Text;				
					else
						cmd.Parameters.Add("@Lecturer",SqlDbType.NVarChar,255).Value=txtLecturer1.SelectedItem.Text;				
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
	#endregion

	#region ADD EMP FOR TRAINING COURSE
	class clsTrainingCourseEmp
	{
		/////////////////////////////////////////////
		///Save list emp to table TR_tblTrainingCourseEmp
		//////////////////////////////////////////////
		///
		public static bool SaveEmp(string strID, string strTrainingCourseID)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "TR_spfrmTRAININGCOURSEEMP";
			try
			{	
				string[] arrIDEmp = strID.Trim().Split('$');
				//--------------
				for(int i=0;i<arrIDEmp.Length-1;i++)
				{
					cmd.Parameters.Clear(); 
					cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 12).Value = "AddNew";			
					
					cmd.Parameters.Add("@TrainingCourseID", SqlDbType.NVarChar,12).Value= strTrainingCourseID;
					cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar,12).Value= arrIDEmp[i];
					
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
		/// Delete Emps from Tr_tblTrainingCourseEmp
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public static bool TrainingEmpRemove(string strListID,string strTrainingCourseID)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "TR_spfrmTRAININGCOURSEEMP";
			//-------
			try
			{
				string[] arrID = strListID.Trim().Split('$');
				//--------------
				for(int i=0;i<arrID.Length-1;i++)
				{
					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Delete";
					cmd.Parameters.Add("@EmpID",SqlDbType.NVarChar,12).Value = arrID[i];
					cmd.Parameters.Add("@TrainingCourseID",SqlDbType.NVarChar,12).Value = strTrainingCourseID;
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
#endregion

	#region "ADD EMP NOT PASS TO LIST"
	class clsAddEmpToList
	{
		public static DataTable GetEmpNotPass(string strTrainingCourseID, string strLanguageID)
		{
			string strSQL="TR_spfrmTRAININGCOURSE @Activity='GetTraineeListNotPass',@TrainingCourseID='" + strTrainingCourseID + "',@LanguageID='" + strLanguageID + "'";
			return clsCommon.GetDataTable(strSQL);
		}
		/////////////////////////////////////////////////////
		///Add list Emp to table TR_tblTrainingResult
		////////////////////////////////////////////////////
		public static bool AddToList(DataGrid dtgList, string strTrainingCourseID, string strLanguage)
		{
			TextBox txtNote;
			CheckBox chkSelect;
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "TR_spfrmTRAININGRESULT";
			try
			{	
				for(int i=0;i< dtgList.Items.Count;i++)
				{
					chkSelect = (CheckBox)dtgList.Items[i].FindControl("chkSelect");
					if (chkSelect.Checked)
					{
						txtNote = (TextBox)dtgList.Items[i].FindControl("txtNote");
						cmd.Parameters.Clear(); 
								
						cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 20).Value = "Save";
						cmd.Parameters.Add("@CourseSubjectID",SqlDbType.NVarChar,12).Value=dtgList.Items[i].Cells[1].Text.Trim();
						cmd.Parameters.Add("@EmpID",SqlDbType.NVarChar,12).Value=dtgList.Items[i].Cells[0].Text.Trim();				
						cmd.Parameters.Add("@NoExam", SqlDbType.Decimal).Value=0;
						//cmd.Parameters.Add("@EmpID",SqlDbType.NVarChar,255).Value=txtNote.Text.Trim();
						cmd.Parameters.Add("@Note",SqlDbType.NVarChar,255).Value=txtNote.Text.Trim();

					}
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
	#endregion

	#region TRAINING REPORT
	class clsTrainingReport
	{
		public static bool CheckLineManager(string pAccountLogin)
		{
			try
			{
				DataRow dtData= clsCommon.GetDataRow("HR_clsCommon @Activity='CheckManager', @EmpID='" + pAccountLogin + "'");
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

		public static DataTable GetCourseForEmp(string pstrEmpID, string pstrLSTrainingCourseID, string pstrFromDate, string pstrToDate)
		{
			DataTable dtb = new DataTable();
			try
			{
				string strSql = "TR_spfrmTrainingReport @Activity = 'GetCourseForEmp', @EmpID = '" + pstrEmpID + "', @LSTrainingCourseID='" + pstrLSTrainingCourseID + 
					"', @FromDate='" + pstrFromDate + "', @ToDate='" + pstrToDate + "'";
				dtb= clsCommon.GetDataTable(strSql);
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
		//HanhNTM: Support send mail 
		public static string getEmpIDFromCourseEmp(string sCourseEmpID)
		{
			string strEmpID="";
			DataRow drData= clsCommon.GetDataRow("TR_spfrmTrainingReport @Activity='GetEmpIDFromCourseEmp',@CourseEmpID='" + sCourseEmpID + "'");
			if (drData!=null)
			{
				strEmpID=drData["EmpID"].ToString();
			}
			return strEmpID;
		}
		public static string getLMIDFromEmp(string sEmpID)
		{
			string strLMID="";
			DataRow drData= clsCommon.GetDataRow("TR_spfrmTrainingReport @Activity='GetLMIDFromEmpID',@EmpID='" + sEmpID + "'");
			if (drData!=null)
			{
				strLMID=drData["LineManagerID"].ToString();
			}
			return strLMID;
		}
		public static DataTable GetCourseForLineManager(string pstrEmpID, string pstrLSTrainingCourseID, string pstrFromDate, string pstrToDate, string pstrCompany, string pstrLevel1, string pstrLevel2, string pstrLevel3, string sEmpID, string sEmpName, string sShortName, string sStatus, string sLSRESourceID)
		{
			DataTable dtb = new DataTable();
			try
			{
				string strSql = "TR_spfrmTrainingReport @Activity = 'GetCourseForLineManager', @EmpID = '" + pstrEmpID + "', @LSTrainingCourseID='" + pstrLSTrainingCourseID + 
					"', @FromDate='" + pstrFromDate + "', @ToDate='" + pstrToDate + 
					"', @LSCompanyID='" + pstrCompany + "', @Level1ID='" + pstrLevel1 + 
					"', @Level2ID='" + pstrLevel2 + "', @Level3ID='" + pstrLevel3 + "',@EmpCode=N'" + sEmpID + "',@EmpName=N'" + sEmpName + "',@ShortName=N'" + sShortName + "',@Status=" + sStatus + ",@LSRESourceID=N'" + sLSRESourceID + "'";
				dtb= clsCommon.GetDataTable(strSql);
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

		public static DataRow GetDataByID(string pCourseEmpID)
		{
			DataRow drData;
			try
			{
				drData= clsCommon.GetDataRow("TR_spfrmTrainingReport @Activity='getDataByID', @CourseEmpID='" + pCourseEmpID + "'");
				return drData;
			}
			catch
			{
				return null;
			}			
		}
	
		
		public static DataTable GetReportMethod(string pstrCourseEmpID)
		{
			DataTable dtb = new DataTable();
			try
			{
				string strSql = "TR_spfrmTrainingReport @Activity = 'GetReportMethod', @CourseEmpID = '" + pstrCourseEmpID + "'";
				dtb = clsCommon.GetDataTable(strSql);
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

		public static DataTable GetReportLogistics(string pstrCourseEmpID)
		{
			DataTable dtb = new DataTable();
			try
			{
				string strSql = "TR_spfrmTrainingReport @Activity = 'GetReportLogistics', @CourseEmpID = '" + pstrCourseEmpID + "'";
				dtb = clsCommon.GetDataTable(strSql);
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

		public static string sImpact_Method(DataGrid pdtgList, string pstrCourseEmpID)
		{
			string strErr = "";
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);		
			SQLconn.Open();
			cmd.Connection = SQLconn;
			cmd.Transaction= SQLconn.BeginTransaction();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "TR_spfrmTrainingReport";
			CheckBox chkSelect = new CheckBox();
			string strMark="";
			bool Method1;
			bool Method2;
			bool Method3;
			bool Method4;
			try
			{
				for(int i=0;i<pdtgList.Items.Count;i++)
				{		
					Method1=((RadioButton)pdtgList.Items[i].FindControl("chkMethod1")).Checked;
					Method2=((RadioButton)pdtgList.Items[i].FindControl("chkMethod2")).Checked;
					Method3=((RadioButton)pdtgList.Items[i].FindControl("chkMethod3")).Checked;
					Method4=((RadioButton)pdtgList.Items[i].FindControl("chkMethod4")).Checked;
						
					strMark=getcheckResult(Method1,Method2,Method3,Method4,false, false);
					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Save_Method";							
					cmd.Parameters.Add("@CourseEmpID",SqlDbType.NVarChar,12).Value = pstrCourseEmpID;
					cmd.Parameters.Add("@LSTrainingMethodID",SqlDbType.NVarChar,12).Value = pdtgList.Items[i].Cells[0].Text.Trim();
					if (strMark!="0")
						cmd.Parameters.Add("@Mark",SqlDbType.Decimal).Value = strMark;
					else
						cmd.Parameters.Add("@Mark",SqlDbType.Decimal).Value = DBNull.Value;
					cmd.Parameters.Add("@Note",SqlDbType.NVarChar,255).Value = ((TextBox)pdtgList.Items[i].FindControl("txtNoteMethod")).Text;
					cmd.ExecuteNonQuery();
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

		public static string sImpact_Logistics(DataGrid pdtgList, string pstrCourseEmpID)
		{
			string strErr = "";
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);		
			SQLconn.Open();
			cmd.Connection = SQLconn;
			cmd.Transaction= SQLconn.BeginTransaction();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "TR_spfrmTrainingReport";
			CheckBox chkSelect = new CheckBox();
			string strMark="";
			bool Logistics1;
			bool Logistics2;
			bool Logistics3;
			bool Logistics4;
			bool Logistics5;
			bool Logistics6;
			try
			{
				for(int i=0;i<pdtgList.Items.Count;i++)
				{		
					Logistics1=((RadioButton)pdtgList.Items[i].FindControl("chkLogistics1")).Checked;
					Logistics2=((RadioButton)pdtgList.Items[i].FindControl("chkLogistics2")).Checked;
					Logistics3=((RadioButton)pdtgList.Items[i].FindControl("chkLogistics3")).Checked;
					Logistics4=((RadioButton)pdtgList.Items[i].FindControl("chkLogistics4")).Checked;
					Logistics5=((RadioButton)pdtgList.Items[i].FindControl("chkLogistics5")).Checked;
					Logistics6=((RadioButton)pdtgList.Items[i].FindControl("chkLogistics6")).Checked;
						
					strMark=getcheckResult(Logistics1,Logistics2,Logistics3,Logistics4,Logistics5,Logistics6);
					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Save_Logistics";							
					cmd.Parameters.Add("@CourseEmpID",SqlDbType.NVarChar,12).Value = pstrCourseEmpID;
					cmd.Parameters.Add("@LSTrainingLogisticsID",SqlDbType.NVarChar,12).Value = pdtgList.Items[i].Cells[0].Text.Trim();
					if (strMark!="0")
						cmd.Parameters.Add("@Mark",SqlDbType.Decimal).Value = strMark;
					else
						cmd.Parameters.Add("@Mark",SqlDbType.Decimal).Value = DBNull.Value;
					cmd.Parameters.Add("@Note",SqlDbType.NVarChar,255).Value = ((TextBox)pdtgList.Items[i].FindControl("txtNoteLogistics")).Text;
					cmd.ExecuteNonQuery();
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

		private static string getcheckResult(bool Item1,bool Item2, bool Item3, bool Item4, bool Item5, bool Item6)
		{
			try
			{
				int iReturn = (Item1==true?1:0) + (Item2==true?2:0) + (Item3==true?3:0) + (Item4==true?4:0) + (Item5==true?5:0) + (Item6==true?6:0);
				return iReturn.ToString();
			}
			catch
			{
				return "999";
			}
		}
		public static DataTable LoadTrainingReport(string pstrLSTrainingCourseID, string pstrFromDate, string pstrToDate, string pstrCompany, string pstrLevel1, string pstrLevel2, string sEmpID, string sEmpName, string sShortName, string sStatus, string sLSRESourceID,string sGender, string sLocalExpat, string sLSLocationID,string sStatusRequest, string sEmpType)
		{
			DataTable dtb = new DataTable();
			try
			{
				string strSql = "TR_spfrmTrainingReport @Activity = 'LoadTrainingReport', @LSTrainingCourseID='" + pstrLSTrainingCourseID + 
					"', @FromDate='" + pstrFromDate + "', @ToDate='" + pstrToDate + 
					"', @LSCompanyID='" + pstrCompany + "', @Level1ID='" + pstrLevel1 + 
					"', @Level2ID='" + pstrLevel2 + "',@EmpCode=N'" + sEmpID + "',@EmpName=N'" + sEmpName + "',@ShortName=N'" + sShortName + "',@Status='" + sStatus + "',@LSRESourceID=N'" + sLSRESourceID + "',@Gender='" + sGender + "',@LocalExpat='" + sLocalExpat + "', @LSLocationID='" + sLSLocationID + "',@LSEmpTypeID='" + sEmpType + "',@StatusReport='" + sStatusRequest + "'";
				dtb= clsCommon.GetDataTable(strSql);
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
	}
	
	#endregion TRAINING REPORT

	#region TRAINING COURSE STANDARDLIZE
	class clsCourseStandardlize
	{
		public static DataTable GetDataAll()
		{
			DataTable dtData= new DataTable();
			try
			{
				dtData= clsCommon.GetDataTable("TR_spfrmCourseStandardlize @Activity='GetDataAll'");
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

		
		public static string Execute(string strTrainingRequestID, string strTrainingCourseName, string strCreater)
		{
			try
			{
				string strSql = "TR_spfrmCourseStandardlize @Activity='Standardlize', @TrainingRequestID = '" + strTrainingRequestID + "', @TrainingCourseName='" + strTrainingCourseName + "', @Creater='" + strCreater + "'";
				DataRow dr = clsCommon.GetDataRow(strSql);
				string strTrainingCourseID = dr[0].ToString();
				return strTrainingCourseID;
			}
			catch
			{
				return null;
			}
		}
	}
	#endregion TRAINING COURSE STANDARDLIZE

	
}
