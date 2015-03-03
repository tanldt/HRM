using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using iHRPCore.Com;
using System.Web.UI.WebControls;
using System.Configuration;
namespace iHRPCore.APPComponent
{
	#region APPRAISAL CLASS
	public class clsAPPAppraisal
	{		
		#region LOAD APPRAISAL BY EMPID
		public static DataTable LoadEmpAppraisal(string strEmpID)
		{
			string strSQL="APP_spfrmAppraisal @Activity='GetDataByEmpID',@EmpID='"+strEmpID+"'";
			return clsCommon.GetDataTableHasID(strSQL);	
		}
		#endregion

		#region LOAD ALL APPRAISAL
		public static DataTable LoadAllAppraisal(string strEmpID,string strName,
			string strDivision,string strDepartment,string strLocation,string strSection,
			string strPosition,string strJobCode,string strLevel,string strFromDate,string strToDate,string strResultRate,string strStatus)
		{			
			string strSQL="APP_spfrmAppraisal @Activity='LoadAllAppraisal', ";
			strSQL+="@EmpID='"+strEmpID+"', ";			
			strSQL+="@EmpName=N'"+strName+"', ";
			strSQL+="@Level1Code='"+strDivision+"', ";
			strSQL+="@Level2Code='"+strDepartment+"', ";
			strSQL+="@Level3Code='"+strSection+"', ";
			strSQL+="@LocationCode='"+strLocation+"', ";
			strSQL+="@PositionCode='"+strPosition+"', ";
			strSQL+="@JobCode='"+strJobCode+"', ";
			strSQL+="@Level='"+strLevel+"', ";
			strSQL+="@ResultRate='"+strResultRate+"', ";
			strSQL+="@FromDate='"+strFromDate+"', ";
			strSQL+="@ToDate='"+strToDate+"',";
			if (strStatus != "")
				strSQL+="@Status='" + strStatus + "'";
			else
				strSQL+="@Status='0'";
			return clsCommon.GetDataTableHasID(strSQL);
		}
		#endregion

		#region CHECK IF AN EMPID IN ONE YEAR ONE PERIOD JUST APPRAISAL ONCE
		public static string strCode="";
		public static bool CheckOnceATime(string strEmpID,string strPeriod,string strYear)
		{
			string strSQL="APP_spfrmAppraisal @Activity='CheckOnceATime',@EmpID='"+strEmpID+"',@Period='"+strPeriod+"',@Year='"+strYear+"'";
			DataRow dr=clsCommon.GetDataRow(strSQL);
			if(dr!=null && dr.IsNull("Code")==false) { strCode=dr["Code"].ToString();return false; }
			else { strCode=""; return true; }
		}

		#endregion

		#region LOAD COMBO YEARS
		public static void GetComboYears(DropDownList pCtrl,bool bBlank)
		{
			string strSQL="APP_spfrmAppraisal @Activity='GetYears'";
			clsCommon.LoadDropDownListControl(pCtrl,strSQL,"Year","Year",bBlank);
			
			int iLastYear;
			try
			{
				if(pCtrl.Items.Count>0)
					iLastYear=int.Parse(pCtrl.Items[pCtrl.Items.Count-1].Value);
				else iLastYear=DateTime.Now.Year;
			}
			catch(Exception ex) { iLastYear=DateTime.Now.Year; }
			int iCurrentYear=DateTime.Now.Year;

			pCtrl.Items.Clear();
			for(int i=iCurrentYear;i>=iLastYear;i--)
				pCtrl.Items.Add(i.ToString());
			if(iLastYear>iCurrentYear)
				for(int i=iLastYear;i>=iCurrentYear;i--)
					pCtrl.Items.Add(i.ToString());
		}
		#endregion

		#region CALCULATE BONUS		
		public static string CalculateBonus(string strYear,string strPeriod, string LSCompanyID, string LSLevel1ID, string LSLevel2ID, string LSLevel3ID, string sLanguageID)
		{
			string sReturnValue="";
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			SqlCommand cmd = new SqlCommand();
			cnn.Open();
			sqlTran = cnn.BeginTransaction();				
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "APP_spfrmCalculateBonus";
			cmd.CommandTimeout = 8000;
			try
			{			
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "CalculateBonus";
				cmd.Parameters.Add("@Year",SqlDbType.NVarChar,4).Value = strYear.Trim();
				cmd.Parameters.Add("@LSPeriodID",SqlDbType.NVarChar,12).Value = strPeriod.Trim();
				cmd.Parameters.Add("@LSCompanyID",SqlDbType.NVarChar,12).Value = LSCompanyID;
				cmd.Parameters.Add("@LSLevel1ID",SqlDbType.NVarChar,12).Value = LSLevel1ID;
				cmd.Parameters.Add("@LSLevel2ID",SqlDbType.NVarChar,12).Value = LSLevel2ID;					
				cmd.Parameters.Add("@LSLevel3ID",SqlDbType.NVarChar,12).Value = LSLevel3ID;
				cmd.Parameters.Add("@sError", SqlDbType.NVarChar,4000).Direction = ParameterDirection.InputOutput;
				cmd.ExecuteNonQuery();
				sReturnValue = cmd.Parameters["@sError"].Value.ToString().Trim();
				sqlTran.Commit();
				if (sReturnValue=="")
					return clsChangeLang.getStringAlert("PC_0004",sLanguageID);
				else
					return clsChangeLang.getStringAlert("PC_0003",sLanguageID);
			}
			catch (SqlException ex)
			{
				return clsChangeLang.getStringAlert("PC_0003",sLanguageID);
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
		public static DataTable GetDataBonus(string strYear,string strPeriod,string sLSCompanyID, string sLSLevel1ID,string sLSLevel2ID, string sLSLevel3ID)
		{
			try
			{
				DataTable dtData= clsCommon.GetDataTable("APP_spfrmCalculateBonus 'GetDataBonus',@Year ='" + strYear + "',@LSPeriodID = '" +strPeriod+ "',@LSCompanyID='" + sLSCompanyID + "',@LSLevel1ID='" + sLSLevel1ID + "',@LSLevel2ID='" + sLSLevel2ID + "',@LSLevel3ID='" + sLSLevel3ID + "'");
				return dtData;
			}
			catch (Exception ex)
			{
				return null;
			}			
		}		
#endregion
		public static DataRow GetDataByID(string strSpName,string strKeyField,string strValue)
		{
			DataRow iRow = clsCommon.GetDataRow(strSpName + " @Activity='GetDataByID', @" + strKeyField + " = '" + strValue +"'");
			if(iRow !=null)
				return iRow;
			else
				return null;
		}
		public static DataTable GetDataBonusAll(string sYYYY, string sPeriod, string sLSCompanyID, string sLSLevel1ID,string sLSLevel2ID, string sLSLevel3ID, string sEmpCode)
		{
			try
			{
				DataTable dtData= clsCommon.GetDataTable("APP_spfrmCalculateBonus 'GetDataBonusAll',@Year=N'" + sYYYY + "',@LSPeriodID = '" + sPeriod + "',@LSCompanyID='" + sLSCompanyID + "',@LSLevel1ID='" + sLSLevel1ID + "',@LSLevel2ID='" + sLSLevel2ID + "',@LSLevel3ID='" + sLSLevel3ID + "',@EmpCode = '"+sEmpCode+"'");
				return dtData;
			}
			catch (Exception ex)
			{
				return null;
			}			
		}		
		public static bool CheckPermissionDelete(string strBonusItemID)
		{
			try
			{
				DataRow drData= clsCommon.GetDataRow("LS_spfrmBonusItem @Activity='GetDataByID',@LSBonusItemID='" + strBonusItemID + "'");
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

		public static DataTable getAppraisalList(string sYear,string sPeriod, string sAPPTypeID,string sEmpCode, string sEmpName, string sLSCompanyID,
			string sLSLevel1ID, string sLSLevel2ID, string sLSLevel3ID,string strStatus,string strLanguage)
		{			
			DataTable dtData= new DataTable();
			try
			{
				string strSQL= "APP_spfrmBONUSCOEF @Activity='GetDataList', @LanguageID = '" + strLanguage + "',@LSCompanyID=N'" + sLSCompanyID 
					+ "',@LSLevel1ID=N'" + sLSLevel1ID + "',@LSLevel2ID=N'" + sLSLevel2ID + "',@LSLevel3ID=N'" + sLSLevel3ID +  "', @Year = '" + sYear
					+ "', @EmpCode = '" + sEmpCode + "',@EmpName=N'" + sEmpName + "',@LSAppPeriodID=N'" + sPeriod + "',@LSAppTypeID=N'" + sAPPTypeID + "'";
				if (strStatus != "")
					strSQL = strSQL + ", @Status='" + strStatus + "'";

				
				dtData= clsCommon.GetDataTable(strSQL);				
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
	#region BONUS ITEM
	public class clsLSBonusItem
	{
		public static bool CheckPermissionEdit(string strBonusItemID)
		{
			try
			{
				DataRow drData= clsCommon.GetDataRow("LS_spfrmBonusItem @Activity='GetDataByID',@LSBonusItemID='" + strBonusItemID + "'");
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
}
