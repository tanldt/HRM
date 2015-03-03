using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using System.Web.UI.WebControls;
using iHRPCore.Com;
using System.Web.UI;
using System.Configuration;
using iHRPCore.MdlHR;
using System.Web.UI.HtmlControls;

namespace iHRPCore.HRComponent
{
	
	#region HR - RELATION CLASS
	public class clsHRRelation
	{
		#region CONSTANTS - SEX AND STATUS
		const string strMale="Male";
		const string strFemale="Female";
		const string strAlive="Alive";
		const string strDead="Dead";		
		#endregion
		
		#region LOAD AND CALCULATE AGES DIFFERENT VALUES
		public static DataTable LoadDiffAges(object strEmpID)
		{
			string strSQL="HR_spfrmRelative @Activity='LoadDiffAges',@EmpID='"+strEmpID+"'";
			DataTable dt=clsCommon.GetDataTable(strSQL);
			DataColumn cl=new DataColumn("DiffAgeAndDOB");
			dt.Columns.Add(cl);
			for(int i=0;i<dt.Rows.Count;i++)
				dt.Rows[i]["DiffAgeAndDOB"]=dt.Rows[i]["DiffAge"].ToString()+","+dt.Rows[i]["DOB"].ToString();
			return dt;
		}
		#endregion
		public static bool CheckIdentityCard(string strCardID,string sRelativeID, string sEmpID)
		{
			try
			{
				DataTable DtData = clsCommon.GetDataTable("HR_spfrmRelative @Activity='CheckIdentityCard',@IDNo=N'" + strCardID.Trim() + "',@RelativeID='" + sRelativeID + "',@EmpID='" + sEmpID + "'");
				if(DtData.Rows.Count<=1)
					return true;
				else
					return false;
			}
			catch
			{
				return false;
			}			
		}

		public static string GetDOB(Object strEmpID)
		{			
			DataRow iRow = clsCommon.GetDataRow("HR_spfrmRelative @Activity='GetDOB',@EmpID='" + strEmpID + "'");
			if(iRow == null)
				return "0";
			else
				return iRow[0].ToString();
	}
		#region LOAD RELATIVES BY EMPID
		public static DataTable LoadGridRelative(object strEmpID,string strLanguage)
		{
			string strSQL="HR_spfrmRelative @Activity='LoadRelative',@LanguageID='" + strLanguage + "',@EmpID='"+strEmpID+"'";

			DataTable dt=clsCommon.GetDataTableHasID(strSQL);			
			/*DataColumn clSeq=new DataColumn("GenderText");
			dt.Columns.Add(clSeq);
			clSeq=new DataColumn("StatusText");
			dt.Columns.Add(clSeq);

			for(int iIndexRows=0;iIndexRows<dt.Rows.Count;iIndexRows++)
			{
				if(dt.Rows[iIndexRows]["Gender"].ToString()=="True") dt.Rows[iIndexRows]["GenderText"]=strMale;
				else dt.Rows[iIndexRows]["GenderText"]=strFemale;

				if(dt.Rows[iIndexRows]["Status"].ToString()=="0") dt.Rows[iIndexRows]["StatusText"]=strAlive;
				else dt.Rows[iIndexRows]["StatusText"]=strDead;
			}*/

			return dt;
		}	
		#endregion
		public static int GetForce( string sValue)
		{
			DataRow drData = clsCommon.GetDataRow("HR_spfrmRelative @Activity='GetForce',@LSRelationshipID='" + sValue + "'");
			switch (drData["ForceSex"].ToString())
			{
				case "True":
					return 1;
					break;
				case "False":
					return 0;
					break;
				default:
					return 2;
					break;
			}
		}

		#region LOAD RELATIONSHIP PERSON OF EMPID
		public static DataTable LoadRelationsipPerson(string strEmpID,string strRelativeID)
		{
			string strSQL="HR_spfrmRelative @Activity='LoadRelationshipPerson',@EmpID='"+strEmpID+"',@RelativeID='"+strRelativeID+"'";
			return clsCommon.GetDataTableHasID(strSQL);
		}
		#endregion

		#region LOAD RELATIVE EMP BY EMPID
		public static DataTable LoadEmpByID(Object strEmpID)
		{
			string strSQL="HR_spfrmRelative @Activity='RealativeLoadEmpByID',@EmpID='"+strEmpID+"'";			
			return clsCommon.GetDataTableHasID(strSQL);
		}
		public static DataRow GetDataByID(Object strID)
		{
			DataRow iRow = clsCommon.GetDataRow("HR_spfrmRelative @Activity='GetDataByID',@RelativeID = '" + strID + "'");
			if(iRow !=null)
				return iRow;
			else
				return null;
		}
		#endregion

		
		#region CHECK IF RELATIONSHIP AVAIBLE
		public static bool CheckRelationshipAvaible(string strEmpID,string strRelationshipID,int iAmount)
		{
			// GET MAXIMUM
			string strSQL="HR_spfrmRelative @Activity='CheckRelationshipAvaible',@LSRelationshipID='"+strRelationshipID+"'";
			DataRow dr=clsCommon.GetDataRow(strSQL);
			if(dr["Maximum"]!=System.DBNull.Value)
			{
				int iMaximum=int.Parse(dr["Maximum"].ToString());
				// GET CURRENT AMOUNT
				strSQL="HR_spfrmRelative @Activity='GetCurrentAmount',@EmpID='"+strEmpID+"',@LSRelationshipID='"+strRelationshipID+"'";
				dr=clsCommon.GetDataRow(strSQL);
				int iCurrentAmount=int.Parse(dr["CurrentAmount"].ToString());
				if((iCurrentAmount+iAmount)<=iMaximum) return true;
				else return false;
			}
			else return true;
		}
		public static string CheckEmpRelative(string strEmpID, string strEmpRelative,string sRelativeID,string strLanguage)
		{
			DataRow dr = clsCommon.GetDataRow("HR_spfrmRelative @Activity='CheckEmpRelative',@EmpID='" + strEmpID + "',@RelativeEmpID='" + strEmpRelative + "',@RelativeID='" + sRelativeID + "'");
			if (dr["Return_value"]!=System.DBNull.Value)
			{				
					if (int.Parse(dr["Return_value"].ToString()) == 1)
					{
						return clsChangeLang.getStringAlert("RE_0012",strLanguage);
					}
					else
					{
						return "";
					}				
			}
			else 
			{
				return "";
			}
			
		}
		public static bool CheckRelationshipAvaible(string strEmpID,string strRelationshipID,string sRelationshipIDCurrent, int iAmount)
		{
			if (strRelationshipID==sRelationshipIDCurrent)
			{
				return true;
			}
			else
			{
				// GET MAXIMUM
				string strSQL="HR_spfrmRelative @Activity='CheckRelationshipAvaible',@LSRelationshipID='"+strRelationshipID+"'";
				DataRow dr=clsCommon.GetDataRow(strSQL);
				if(dr["Maximum"]!=System.DBNull.Value)
				{
					int iMaximum=int.Parse(dr["Maximum"].ToString());
					// GET CURRENT AMOUNT
					strSQL="HR_spfrmRelative @Activity='GetCurrentAmount',@EmpID='"+strEmpID+"',@LSRelationshipID='"+strRelationshipID+"'";
					dr=clsCommon.GetDataRow(strSQL);
					int iCurrentAmount=int.Parse(dr["CurrentAmount"].ToString());
					if((iCurrentAmount+iAmount)<=iMaximum) return true;
					else return false;
				}
				else return true;
			}
		}
		#endregion
	}
	#endregion

	#region HR - MIS INPUT CLASS
	public class clsHRMISInput
	{
		#region LOAD MIS INPUT BY MONTHLY
		public static DataTable LoadMonthly(string strMonth,string strYear)
		{			
			string strSQL="HR_spfrmMISInput @Activity='LoadMonthly',@Month='"+strMonth+"',@Year='"+strYear+"'";
			return clsCommon.GetDataTableHasID(strSQL);
		}		
		#endregion

		#region ENSURE ALL KPI HAS RECORDS
		public static void EnsureAllKPI(string strMonth,string strYear)
		{
			// CHECK IF THIS MONTHYEAR HAS ALL KPI
			string strSQL="HR_spfrmMISInput @Activity='EnsureAllKPI',@Month='"+strMonth+"',@Year='"+strYear+"'";
			clsCommon.GetDataTable(strSQL);
		}
		#endregion

		#region LOAD EXISTING MONTHS COMBO
		public static void LoadComboExistingMonths(DropDownList pCtrl)
		{
			string strSQL="HR_spfrmMISInput @Activity='LoadComboExistingMonths'";
			DataTable dt=clsCommon.GetDataTable(strSQL);
			DataColumn cl=new DataColumn("Month");
			dt.Columns.Add(cl);

			for(int i=0;i<dt.Rows.Count;i++)
				dt.Rows[i]["Month"]=DateTime.Parse(dt.Rows[i]["Months"].ToString()).ToString("MM/yyyy");
				
			pCtrl.DataSource=dt;
			pCtrl.DataTextField="Month";
			pCtrl.DataValueField="Month";
			pCtrl.DataBind();
		}
		#endregion
	}
	#endregion

	#region HR - MIS INPUT - KPI CLASS
	public class clsHRMISInputKPI
	{
		#region SEARCH - CONVERT TO STORE PROCEDURE - LATER LATER ...
		public static DataTable Search(string strCompanyCode,string strDivisionCode,string strDepartmentCode,string strSectionCode,string strCode,string strName,string strSectionCodeList,bool bSelected)
		{
			string pCommandText="SELECT LS_tblCompany.LSCompanyCode,LS_tblCompany.Name AS CompanyName,";
			pCommandText+="LS_tblLevel1.LSLevel1Code,LS_tblLevel1.Name AS DivisionName,";
			pCommandText+="LS_tblLevel2.LSLevel2Code,LS_tblLevel2.Name AS DepartmentName,";
			pCommandText+="LS_tblLevel3.LSLevel3Code,LS_tblLevel3.Name AS SectionName ";
			pCommandText+="FROM LS_tblCompany ";
			pCommandText+="INNER JOIN LS_tblLevel1 ON LS_tblCompany.LSCompanyCode=LS_tblLevel1.LSCompanyCode ";
			pCommandText+="INNER JOIN LS_tblLevel2 ON LS_tblLevel1.LSLevel1Code=LS_tblLevel2.LSLevel1Code ";
			pCommandText+="INNER JOIN LS_tblLevel3 ON LS_tblLevel2.LSLevel2Code=LS_tblLevel3.LSLevel2Code ";			

			string strCondition="";

			if(strCompanyCode.Length>0)
				strCondition="WHERE LS_tblCompany.LSCompanyCode='"+strCompanyCode+"'";
			
			if(strDivisionCode.Length>0)
				if(strCondition=="") strCondition="WHERE LS_tblLevel1.LSLevel1Code='"+strDivisionCode+"'";
				else strCondition+=" AND LS_tblLevel1.LSLevel1Code='"+strDivisionCode+"'";

			if(strDepartmentCode.Length>0)
				if(strCondition=="") strCondition="WHERE LS_tblLevel2.LSLevel2Code='"+strDepartmentCode+"'";
				else strCondition+=" AND LS_tblLevel2.LSLevel2Code='"+strDepartmentCode+"'";

			if(strSectionCode.Length>0)
				if(strCondition=="") strCondition="WHERE LS_tblLevel3.LSLevel3Code='"+strSectionCode+"'";
				else strCondition+=" AND LS_tblLevel3.LSLevel3Code='"+strSectionCode+"'";

			if(strSectionCodeList.Length>0)
			{
				string[] arrStr=strSectionCodeList.Split(',');
				if(arrStr.Length==1)
				{
					if(bSelected==true)
					{
						if(strCondition=="") strCondition="WHERE LS_tblLevel3.LSLevel3Code='"+arrStr[0]+"'";
						else strCondition+=" AND LS_tblLevel3.LSLevel3Code='"+arrStr[0]+"'";
					}
					else
					{
						if(strCondition=="") strCondition="WHERE LS_tblLevel3.LSLevel3Code<>'"+arrStr[0]+"'";
						else strCondition+=" AND LS_tblLevel3.LSLevel3Code<>'"+arrStr[0]+"'";
					}
				}
				else
				{
					if(bSelected==true)
					{
						if(strCondition=="") 
						{
							strCondition="WHERE LS_tblLevel3.LSLevel3Code IN (";
							for(int i=0;i<arrStr.Length;i++) strCondition+="'"+arrStr[i]+"',";
							strCondition=strCondition.Remove(strCondition.Length-1,1);
							strCondition+=")";
						}
						else
						{
							strCondition="AND LS_tblLevel3.LSLevel3Code IN (";
							for(int i=0;i<arrStr.Length;i++) strCondition+="'"+arrStr[i]+"',";
							strCondition=strCondition.Remove(strCondition.Length-1,1);
							strCondition+=")";
						}
					}
					else
					{
						if(strCondition=="") 
						{
							strCondition="WHERE LS_tblLevel3.LSLevel3Code NOT IN (";
							for(int i=0;i<arrStr.Length;i++) strCondition+="'"+arrStr[i]+"',";
							strCondition=strCondition.Remove(strCondition.Length-1,1);
							strCondition+=")";
						}
						else
						{
							strCondition="AND LS_tblLevel3.LSLevel3Code NOT IN (";
							for(int i=0;i<arrStr.Length;i++) strCondition+="'"+arrStr[i]+"',";
							strCondition=strCondition.Remove(strCondition.Length-1,1);
							strCondition+=")";
						}
					}
				}
			}

			pCommandText+=strCondition;

			return clsCommon.GetDataTableHasID(pCommandText);			
		}
		#endregion

		#region CALCULATE NUMBER OF EMP - KPI VALUE
		public static DataTable CalculateNumberOfEmp(string strCompanyCode,string strDivisionCode,string strDepartmentCode,string strSectionCode)
		{
			string strSQL="HR_spfrmMISInput @Activity='CalculateNumberOfEmp',@LSCompanyCode='"+strCompanyCode+"',@LSLevel1Code='"+strDivisionCode+"',@LSLevel2Code='"+strDepartmentCode+"',@LSLevel3Code='"+strSectionCode+"'";
			return clsCommon.GetDataTableHasID(strSQL);			
		}
		#endregion

		#region UPDATE MIS INPUT
		public static bool UpdateMISInput(string strID,string strKPIValue,string strSectionList,string strSectionCodeList,string strClass)
		{
			string strSQL="HR_spfrmMISInput @Activity='Update',@MISInputID='"+strID+"',@KPIValue='"+strKPIValue+"',@SectionList='"+strSectionList+"',@SectionCodeList='"+strSectionCodeList+"',@Class='"+strClass+"'";
			clsCommon.GetDataTable(strSQL);
			return true;
		}
		#endregion
	}
	#endregion
	#region TER - TERMINATION
	public class clsTermination
	{
		public static DataTable LoadTerminationByEmpID(object strEmpID)
		{
			DataTable dtb=new DataTable();
			try
			{
				dtb = clsCommon.GetDataTable("TER_spfrmTERMINATION @Activity='LoadTerminationByID',@EmpID='"  + strEmpID + "'");
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
		public static DataTable LoadStatusEmp(object strEmpID)
		{
			DataTable dtb=new DataTable();
			try
			{	
				dtb = clsCommon.GetDataTable("TER_spfrmFINALPAYMENT @Activity='CheckStatus',@EmpID='"  + strEmpID + "'");
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

		public static DataTable LoadPaymentByEmpID(object strEmpID)
		{
			DataTable dtb=new DataTable();
			try
			{	
				dtb = clsCommon.GetDataTable("TER_spfrmFINALPAYMENT @Activity='FillData',@EmpID='"  + strEmpID + "'");
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

		public static DataTable LoadEmpID(object strEmpID)
		{
			DataTable dtb=new DataTable();
			try
			{	
				dtb = clsCommon.GetDataTable("TER_spfrmFINALPAYMENT @Activity='GetEmpID',@EmpID='"  + strEmpID + "'");
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
		public static DataTable LoadCurrentPaymentByEmpID(object strEmpID)
		{
			DataTable dtb=new DataTable();
			try
			{
				dtb = clsCommon.GetDataTable("TER_spfrmFINALPAYMENT @Activity='GetData',@EmpID='"  + strEmpID + "'");
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
		public static bool checkEmpTermination(object strEmpID)
		{
			DataTable dtb = clsCommon.GetDataTable("TER_spfrmTERMINATION @Activity='checkEmpTermination',@EmpID='"  + strEmpID + "'");
			if (dtb.Rows.Count>0 )
				return false;
			else
				return true;
		}
		public static string sReinstate(string strEmpID, string strKeepOldEmpID, string strTurnOverDate)
		{
			string sErr="";
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();			
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;			
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText ="TER_SPFRMTERMINATION";
			try
			{
				cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 50).Value = "REINSTATE";
				cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar,12).Value= strEmpID;
				cmd.Parameters.Add("@KEEPEMPID", SqlDbType.Bit).Value= strKeepOldEmpID;
				cmd.Parameters.Add("@DateReInstate", SqlDbType.NVarChar).Value= strTurnOverDate;
				cmd.Parameters.Add("@ReturnMess", SqlDbType.NVarChar,1000).Direction = ParameterDirection.InputOutput;
				cmd.ExecuteNonQuery();	
				sErr= cmd.Parameters["@ReturnMess"].Value.ToString();
				if (sqlTran != null ) sqlTran.Commit();				
				if (sErr!="")
					return sErr;
				else
					return "";
			}
			catch (Exception ex)
			{
				
				cmd.Dispose();				
				sqlTran.Rollback();
				if(cnn.State == ConnectionState.Open) cnn.Close();
				return ex.ToString();
			}
		}
		public static void Pre_Update()
		{
			clsCommon.Exc_CommandText("[TER_spfrmTERMINATION] @Activity='Pre_Update'");
		}
	}
	//them strLSEmpTypeID:Honglk(10-9-2008)
	public class clsTerminationList
	{
		public static DataTable getTerminationList(string sTerminationTypeID, string sFromDate, string sToDate,string sEmpCode, string sEmpName, string sLSCompanyID,
				string sLSLevel1ID, string sLSLevel2ID, string sLSLevel3ID, string sLSPositionID, string sLSLocationID,string strStatus,string strLanguage, string strLSEmpTypeID,System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			DataTable dtData= new DataTable();
			try
			{
				string sSQL="TER_spfrmTERMINATION @Activity='getTerminationList',@EmpCode=N'" + sEmpCode + "',@EmpName=N'" + sEmpName + "',@LSTerminationTypeID='" + sTerminationTypeID + "', @FromDate='" + sFromDate + "', @ToDate='" + sToDate + "',@LSCompanyID=N'" + sLSCompanyID 
					+ "',@LSLevel1ID=N'" + sLSLevel1ID + "',@LSLevel2ID=N'" + sLSLevel2ID + "',@LSLevel3ID=N'" + sLSLevel3ID 
					+ "',@LSLocationID=N'" + sLSLocationID + "', @LSPositionID=N'" + sLSPositionID + "',@AccountLogin=N'" + sAccountLogin + "',@LanguageID='" + strLanguage + "',@LSEmpTypeID='" +strLSEmpTypeID+"'";
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

	public class clsTerminationItem
	{
		public static bool CheckPermissionDelete(string strLSTerminationItemID)
		{
			try
			{
				DataRow drData= clsCommon.GetDataRow("LS_spfrmTERMINATIONITEM @Activity='GetDataByID',@LSTerminationItemID='" + strLSTerminationItemID + "'");
				if(drData != null)
				{
					if(drData["AllowDel"].ToString().Trim().ToLower()=="true")
						return true;
					else
						return false;
				}
				else
					return false;
			}
			catch(Exception exp)
			{
				return false;
			}
		}
		public static bool CheckPermissionEdit(string strSalaryItemID)
		{
			try
			{
				DataRow drData= clsCommon.GetDataRow("LS_spfrmTERMINATIONITEM @Activity='GetDataByID',@LSTerminationItemID='" + strSalaryItemID + "'");
				if(drData != null)
				{
					if(drData["AllowEdit"].ToString().Trim().ToLower()=="true")
						return true;
					else
						return false;
				}
				else
					return false;
			}
			catch(Exception exp)
			{
				return false;
			}
		}
	}
	#endregion

	public class clsHRParameter
	{
		public static string return_value="";
		public static string check_value="";
		public static void setChkAd(Control pRootCtl,bool bAdmin)
		{
			try
			{				
				foreach(Control Child_ctl in pRootCtl.Controls)
				{
					if(Child_ctl.HasControls()==true)
					{
						setChkAd(Child_ctl,bAdmin);
					}
					else
					{						
						string mCtlType = Child_ctl.GetType().ToString().Trim().Substring(Child_ctl.GetType().ToString().Trim().LastIndexOf(".")+1).ToString().Trim();
						if(mCtlType.ToUpper() == "CHECKBOX")
							if(Child_ctl.ID.Trim().Substring(0,5).ToUpper() == "CHKAD")
							{
								Child_ctl.Visible=bAdmin;
								
							}
					}
				}
			}
			catch(Exception exp)
			{
				string strError = exp.Message.ToString();
			}
		}
		public static void getAdmin(Control pRootCtl)
		{
			try
			{				
				foreach(Control Child_ctl in pRootCtl.Controls)
				{
					if(Child_ctl.HasControls()==true)
					{
						getAdmin(Child_ctl);
					}
					else
					{
						string mCtlType = Child_ctl.GetType().ToString().Trim().Substring(Child_ctl.GetType().ToString().Trim().LastIndexOf(".")+1).ToString().Trim();						
						if (Child_ctl.ID != null)
						{
							if( Child_ctl.ID.Length >5 )
							{
								if( Child_ctl.ID.Trim().Substring(0,5).ToUpper() == "CHKAD")
								{	
									return_value+=Child_ctl.ID.Trim()+ "$";
								}
										
							}
								
						}
					}
				}				
			}
			catch(Exception exp)
			{
				
			}
		}
		public static void impact(Control ctl,string sAscx)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();			
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			getAdmin(ctl);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText ="SYS_spfrmPERMISSIONADMIN_";
			

			try
			{
				cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 50).Value = "Delete";
				cmd.Parameters.Add("@Ascx", SqlDbType.NVarChar,50).Value= sAscx;
				cmd.ExecuteNonQuery();

				string[] arrID = return_value.Trim().Split('$');
				for (int i=0;i<=arrID.Length-1;i++)
				{
					if (!arrID.GetValue(i).ToString().Trim().Equals(""))
					{
						if (arrID.GetValue(i).ToString().Trim().Substring(0,5).ToUpper()=="CHKAD")
						{						
							cmd.Parameters.Clear();
							cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 50).Value = "Save";
							cmd.Parameters.Add("@Ascx", SqlDbType.NVarChar,50).Value= sAscx;
							cmd.Parameters.Add("@CheckID", SqlDbType.NVarChar,50).Value= arrID.GetValue(i).ToString().Trim();								
							cmd.Parameters.Add("@Value", SqlDbType.Bit).Value=clsCommon.GetValueControl(ctl.Controls[0],arrID.GetValue(i).ToString().Trim());
							cmd.ExecuteNonQuery();	
						
						}
					}
				}
			
				if (sqlTran != null ) sqlTran.Commit();				
			}
			catch(Exception exp)
			{
				cmd.Dispose();				
				sqlTran.Rollback();
				if(cnn.State == ConnectionState.Open) cnn.Close();
			}
		}

		public static void setPermission(Control pctl,string sAscx,bool bAdmin)
		{
			//if (bAdmin) return;

			DataTable dtData= clsCommon.GetDataTable("SYS_spfrmPERMISSIONADMIN_ @Activity='GetPermission',@Ascx='" + sAscx + "'");
			if (dtData.Rows.Count>0 )
			{
				for (int i=0;i<=dtData.Rows.Count-1;i++)
				{
					CheckControlPermission(dtData.Rows[i]["CheckID"].ToString(),bAdmin,pctl);
				}
			}

		}
		
		/// <summary>
		/// Get parameter of HR
		/// </summary>
		/// <param name="pRootCtl">Root control</param>
		/// <param name="pFormName">From Name</param>
		public static void getHR_param(string pName, Control pCtl)	
		{
			try
			{
				DataTable dtData= clsCommon.GetDataTable("LS_spfrmHRPARAMETER @Activity='getValue', @Ascx='" + pName + "'");
				if (dtData.Rows.Count>0)
				{
					for (int i=0;i<=dtData.Rows.Count-1;i++)
					{
						Object obj = pCtl.Controls[0].FindControl(dtData.Rows[i]["ParameterName"].ToString());
						((TextBox)obj).Text=dtData.Rows[i]["Value"].ToString();
					}
				}
			}
			catch (Exception ex)
			{
			}

		}
		public static void CheckControlPermission(string sCheckID,bool bAdmin, Control pctl)
		{
			string sControlName=sCheckID.Substring(5,sCheckID.Length-5);
			//xữ lý phần td			
			Object obj=pctl.Controls[0].FindControl("tdAd"+sControlName+"lbl");
			set_object(obj,"td",bAdmin);
			obj=pctl.Controls[0].FindControl("tdAd"+sControlName+"cbo");
			set_object(obj,"td",bAdmin);
			obj=pctl.Controls[0].FindControl("tdAd"+sControlName+"txt");
			set_object(obj,"td",bAdmin);
			obj=pctl.Controls[0].FindControl("tdAd"+sControlName+"chk");
			set_object(obj,"td",bAdmin);
			obj=pctl.Controls[0].FindControl("tdAd"+sControlName+"Co5");
			set_object(obj,"Co5",bAdmin);
			//xữ lý phần tr
			obj=pctl.Controls[0].FindControl("trAd"+sControlName);
			set_object(obj,"tr",bAdmin);

		}
		public static void set_object(Object obj,string sType,bool bAdmin)
		{			
			if( obj !=null)
			{		
				switch (sType)
				{
					case "td":
						if (bAdmin)
						((HtmlTableCell)obj).Style.Add("DISPLAY","block");														
							else
						((HtmlTableCell)obj).Style.Add("DISPLAY","none");														
						break;
					case "tr":
						if(bAdmin)
						((HtmlTableRow)obj).Style.Add("DISPLAY","block");
							else
						((HtmlTableRow)obj).Style.Add("DISPLAY","none");
						break;
					case "Co5":
						if(bAdmin)
						//((HtmlTableCell)obj).Style.Add("DISPLAY","block");														
						((HtmlTableCell)obj).ColSpan=0;
							else
						//((HtmlTableCell)obj).Style.Add("DISPLAY","none");														
						((HtmlTableCell)obj).ColSpan=5;
						break;
					default:
						break;
				}			
			}			
		}

		public static void LoadPermission(Control pCtl,string Ascx,bool bAdmin)
		{
			if (!bAdmin) return;
			DataTable dtData= clsCommon.GetDataTable("SYS_spfrmPERMISSIONADMIN_ @Activity='GetPermission',@Ascx='" + Ascx + "'");
			if (dtData.Rows.Count>0 )
			{
				for (int i=0;i<=dtData.Rows.Count-1;i++)
				{					
					Object obj=pCtl.Controls[0].FindControl(dtData.Rows[i]["CheckID"].ToString());
					((CheckBox)obj).Checked=false;
				}
			}
		}		
		private static void GetValueControl(Control pctl, string strControlName)
		{			
			foreach( Control child_ctl in pctl.Controls )

				if (child_ctl.HasControls()==true)
				{
					GetValueControl(child_ctl,strControlName);
				}
				else
				{
					try
					{
						Control ctrFound = child_ctl.FindControl(strControlName);
						if(ctrFound!=null)
						{
							string strType = ctrFound.GetType().ToString().Trim().Substring(ctrFound.GetType().ToString().Trim().LastIndexOf(".")+1).ToString().Trim();
							if (strType=="CheckBox")								
							{
								check_value =((CheckBox)ctrFound).Checked.ToString();
								return;
							}
						}
					}				
					catch(Exception ex)
					{
						
					}
				}				
		}
		
	}
}
