using System;
using System.Data;
using System.Web.UI.WebControls;
using iHRPCore.Com;
using System.Web;
using System.Web.Caching;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;
using System.Web.UI;
using System.IO;
using System.Web.Util;
using System.Data.OleDb;

namespace iHRPCore.HRComponent
{
	public class clsTER
	{
		public static void LoadComboResignType(DropDownList pCtrl,bool bBlank)
		{
			string pCommandText="SELECT * FROM TER_tblResignType";
			clsCommon.LoadDropDownListControl(pCtrl,pCommandText,"TERResignTypeCode","Name",bBlank);
		}

		public static void LoadComboReason(DropDownList pCtrl,bool bBlank)
		{
			string pCommandText="SELECT * FROM TER_tblResignReason";
			clsCommon.LoadDropDownListControl(pCtrl,pCommandText,"TERResignReasonCode","Name",bBlank);
		}

		public static int LoadContractDays(string strEmpID)
		{
			if(strEmpID=="") return 0;
			string strSQL="TER_spfrmInfo @Activity='LoadContractType',@EmpID=N'"+strEmpID+"'";
			DataRow dr=clsCommon.GetDataRow(strSQL);
			if(dr!=null)
			{
				if(dr["ContractType"].ToString()=="CI") return 45;
				if(dr["ContractType"].ToString()=="CD") return 30;
				else return 0;
			}	
			return 0;
		}

		public static DataTable LoadTerminationById(string strEmpID)
		{
			string strSQL="TER_spfrmInfo @Activity='LoadTerminationByID',@EmpID=N'"+strEmpID+"'";
			return clsCommon.GetDataTableHasID(strSQL);			
		}

		public static void Reinstate(string strEmpID, string strKeepEmpID, string strDateReInstate)
		{
			string strSQL="TER_spfrmInfo 'ReInstate', @EmpID='" + strEmpID + "', @KeepEmpID='" + strKeepEmpID
				+ "', @DateReInState='" + strDateReInstate + "'";
			clsCommon.GetDataRow(strSQL);
		}

		public static DataTable LoadTerminationList(string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strPosition, string strJobCode, string strLocation, string strCompany,string strReason, string strStatusPayment, 
			string strStartDateFrom,string strStartDateTo,string strLastDateFrom,string strLastDateTo, string strResignType)
		{
			string strSql = "TER_spfrmInfo 'GetAllData',@EmpID=N'" + strEmpID + "', @EmpName=N'" + strEmpName
				+ "', @LSLevel1Code='" + strLevel1 + "', @LSLevel2Code='" + strLevel2 + "',@LSLevel3Code='" + strLevel3
				+ "',@LSPositionCode='" + strPosition + "',@LSJobCodeCode='" + strJobCode 
				+ "',@LSLocationCode='" + strLocation + "',@LSCompanyCode='" + strCompany 
				+ "',@Reason='" + strReason + "', @StartDateFrom='" + strStartDateFrom
				+ "', @StartDateTo='" + strStartDateTo + "',@LastWorkingDateFrom='" + strLastDateFrom
				+ "',@LastworkingDateTo='" + strLastDateTo
				+ "', @ResignCode='" + strResignType + "'";
				if (strStatusPayment == "") strStatusPayment = "4";
					strSql = strSql + ", @StatusPayment='" + strStatusPayment + "'";
			DataTable dtb = clsCommon.GetDataTable(strSql);
			return dtb;
		}
	}

	public class clsWFTempHop
	{
		public static DataTable LoadHealthCheckByID(string strEmpID)
		{
			string pCommandText="SELECT * FROM WF_tblHealthCheck WHERE EmpID='"+strEmpID+"'";
			return clsCommon.GetDataTableHasID(pCommandText);
		}
	}

	public class clsWFUniformParking
	{
		public static DataTable LoadListByRequestDate(string strRequestDate,string strEmpID,string strEmpName,
			string strDivision,string strDepartment,string strSection,string strLocation, string strCompany,
			string strPosition,string strJobcode, string strStatus)
		{
			string strSQL="WF_spfrmUniform @Activity='LoadListByRequestDate',@RequestDate='"+strRequestDate+"',"
				+ "@EmpID='"+strEmpID+"',"
				+ "@EmpName=N'"+strEmpName+"',"
				+ "@Level1ID='"+strDivision+"',"
				+ "@Level2ID='"+strDepartment+"',"
				+ "@Level3ID='"+strSection+"',"
				+ "@LocationID='"+strLocation+"',"
				+ "@CompanyID='"+strCompany+"',"
				+ "@PositionID='"+strPosition+"',"
				+ "@JobcodeID='"+strJobcode+"',"
				+ "@Status='"+strStatus+"'";
			return clsCommon.GetDataTable(strSQL);
		}

		public static DataTable LoadUniformList(string strEmpID,string strEmpName,
			string strDivision,string strDepartment,string strSection,string strLocation, string strCompany,
			string strPosition,string strJobcode, string strStatus, 
			string strFromDate,string strToDate,string strReceiveType,
			string strServeYears,bool bBypass, bool bOnlyReceived, int intType)
		{
			string strSQL = "WF_spfrmUniform @Activity='View',"
				+ "@EmpID='"+strEmpID+"',"
				+ "@EmpName=N'"+strEmpName+"',"
				+ "@Level1ID='"+strDivision+"',"
				+ "@Level2ID='"+strDepartment+"',"
				+ "@Level3ID='"+strSection+"',"
				+ "@LocationID='"+strLocation+"',"
				+ "@CompanyID='"+strCompany+"',"
				+ "@PositionID='"+strPosition+"',"
				+ "@JobcodeID='"+strJobcode+"',"
				+ "@Status='"+strStatus+"',"
				+ "@FromDate='"+strFromDate+"',"
				+ "@ToDate='"+strToDate+"',"
				+ "@Type='" + intType + "',"
				+ "@ReceiveType='"+strReceiveType+"',";

			if(strServeYears!=null && strServeYears.Length>0)
				strSQL+="@ServeYear="+strServeYears+",";
			if(bBypass==true) strSQL+="@Bypass=1";
			else strSQL+="@Bypass=0";
			if(bOnlyReceived==true) strSQL+=",@OnlyReceived=1";
			else strSQL+=",@OnlyReceived=0";
			
			return clsCommon.GetDataTable(strSQL);
		}

		public static DataTable GetUniformID(string strEmpID, string strFromDate, int intType)
		{
			DataTable dtb = clsCommon.GetDataTable("WF_spfrmUniform 'GetByID', @EmpID='" + strEmpID + "', @FromDate='" + strFromDate + "', @Type='" + intType + "'");
			return dtb;
		}

		public static DataTable GetUniformReceiptID(string strEmpID, string strReceiveDate)
		{
			DataTable dtb = clsCommon.GetDataTable("WF_spfrmUniformVoucher 'GetByID', @EmpID='" + strEmpID + "', @ReceiveDate='" + strReceiveDate + "'");
			return dtb;
		}

		public static string SaveUniformSet(DataGrid dtgControl ,string strFromDate, string strToDate, string strReceiveType, string strAmount, string strNote, string strAction, int intType)
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
				cmd.CommandText = "WF_spfrmUniform";
				for (int i = 0;i<dtgControl.Items.Count;i++)
				{
					if (((CheckBox)dtgControl.Items[i].FindControl("chkSelect")).Checked)
					{
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = strAction;						
						cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 1000).Value = dtgControl.Items[i].Cells[1].Text.Trim();
						if (strAction != "Delete")
                            cmd.Parameters.Add("@FromDate", SqlDbType.NVarChar, 12).Value = strFromDate;
                        else
							cmd.Parameters.Add("@FromDate", SqlDbType.NVarChar, 12).Value = dtgControl.Items[i].Cells[8].Text.Trim();

						cmd.Parameters.Add("@ToDate", SqlDbType.NVarChar, 12).Value = strToDate;
						cmd.Parameters.Add("@Type", SqlDbType.Int).Value = intType;
						
						cmd.Parameters.Add("@ReceiveType", SqlDbType.NVarChar,12).Value = strReceiveType;
						if (strNote != "")
							cmd.Parameters.Add("@Note", SqlDbType.NVarChar,200).Value = strNote;
						else
							cmd.Parameters.Add("@Note", SqlDbType.NVarChar,200).Value = DBNull.Value;

						if (strAmount != "")
							cmd.Parameters.Add("@Amount", SqlDbType.Real).Value = strAmount;
						else
							cmd.Parameters.Add("@Amount", SqlDbType.Real).Value = DBNull.Value;

						cmd.Parameters.Add("@Return", SqlDbType.NVarChar,1000).Direction = ParameterDirection.InputOutput;
						cmd.ExecuteNonQuery();	
						if (cmd.Parameters["@Return"].Value.ToString().Trim() != "")
						{
							if (strReturn != "")
								strReturn = strReturn + "," +  cmd.Parameters["@Return"].Value.ToString();	
							else
								strReturn = cmd.Parameters["@Return"].Value.ToString();	
						}
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

		public static string SaveUniformReceipt(DataGrid dtgControl ,string strReceiveDate, string strQuantity, string strReceiptNo,
			string strRequestNo, string strRequestDate, string strPaymentInfo, string strAmount, string strAction, int intType)
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
				cmd.CommandText = "WF_spfrmUniformVoucher";
				for (int i = 0;i<dtgControl.Items.Count;i++)
				{
					if (((CheckBox)dtgControl.Items[i].FindControl("chkSelect")).Checked)
					{
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = strAction;						
						cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 1200).Value = dtgControl.Items[i].Cells[1].Text.Trim();
						if (strAction != "Delete")
							cmd.Parameters.Add("@ReceiveDate", SqlDbType.NVarChar, 12).Value = strReceiveDate;
						else
							cmd.Parameters.Add("@ReceiveDate", SqlDbType.NVarChar, 12).Value = dtgControl.Items[i].Cells[5].Text.Trim();
						cmd.Parameters.Add("@RequestDate", SqlDbType.NVarChar, 12).Value = strRequestDate;
						cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = strQuantity;
						                        				
						if (strReceiptNo != "")
							cmd.Parameters.Add("@ReceiptNo", SqlDbType.NVarChar,20).Value = strReceiptNo;
						else
							cmd.Parameters.Add("@ReceiptNo", SqlDbType.NVarChar,20).Value = DBNull.Value;
						
						if (strRequestNo != "")
							cmd.Parameters.Add("@RequestNo", SqlDbType.NVarChar,20).Value = strRequestNo;
						else
							cmd.Parameters.Add("@RequestNo", SqlDbType.NVarChar,20).Value = DBNull.Value;

						if (strPaymentInfo != "")
							cmd.Parameters.Add("@PaymentInfo", SqlDbType.NVarChar,255).Value = strPaymentInfo;
						else
							cmd.Parameters.Add("@PaymentInfo", SqlDbType.NVarChar,255).Value = DBNull.Value;


						if (strAmount != "")
							cmd.Parameters.Add("@Amount", SqlDbType.Real).Value = strAmount;
						else
							cmd.Parameters.Add("@Amount", SqlDbType.Real).Value = DBNull.Value;

						cmd.Parameters.Add("@Return", SqlDbType.NVarChar,1000).Direction = ParameterDirection.InputOutput;
						cmd.ExecuteNonQuery();	
						if (cmd.Parameters["@Return"].Value.ToString().Trim() != "")
						{
							if (strReturn != "")
								strReturn = strReturn + "," +  cmd.Parameters["@Return"].Value.ToString();	
							else
								strReturn = cmd.Parameters["@Return"].Value.ToString();	
						}
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

	public class clsWFUniformHop
	{
		public static DataTable LoadUniformList(string strEmpID,string strEmpName,
			string strDivision,string strDepartment,string strSection,string strLocation,
			string strPosition,string strJobcode,
			string strFromDate,string strToDate,string strReceiveType,
			string strServeYears,bool bBypass, bool bOnlyReceived)
		{
			//string strSQL="WF_spfrmUniform @Activity='Search',";
			string strSQL="WF_spfrmUniform @Activity='View',";
			strSQL+="@EmpID='"+strEmpID+"',";
			strSQL+="@EmpName=N'"+strEmpName+"',";
			strSQL+="@Level1ID='"+strDivision+"',";
			strSQL+="@Level2ID='"+strDepartment+"',";
			strSQL+="@Level3ID='"+strSection+"',";
			strSQL+="@LocationID='"+strLocation+"',";
			strSQL+="@PositionID='"+strPosition+"',";
			strSQL+="@JobcodeID='"+strJobcode+"',";
			strSQL+="@FromDate='"+strFromDate+"',";
			strSQL+="@ToDate='"+strToDate+"',";
			strSQL+="@ReceiveType='"+strReceiveType+"',";
			if(strServeYears!=null && strServeYears.Length>0)
				strSQL+="@ServeYear="+strServeYears+",";
			if(bBypass==true) strSQL+="@Bypass=1";
			else strSQL+="@Bypass=0";
			if(bOnlyReceived==true) strSQL+=",@OnlyReceived=1";
			else strSQL+=",@OnlyReceived=0";
			
			return clsCommon.GetDataTable(strSQL);
		}

		public static string GetCostPerOne(string strEmpID)
		{
			string strSQL="WF_spfrmUniform @Activity='LoadCostPerOne',@EmpID=N'"+strEmpID+"'";
			DataRow dr=clsCommon.GetDataRow(strSQL);
			if(dr==null) return "";
			else if(dr.IsNull("UniformCost")==false) return dr["UniformCost"].ToString();
				 else return "";
		}

		public static DataTable LoadLastToDate(string strEmpID,string strReceiveType)
		{
			string strSQL="WF_spfrmUniform @Activity='LoadLastToDate',@EmpID='"+strEmpID+"',@ReceiveType='"+strReceiveType+"'";
			return clsCommon.GetDataTableHasID(strSQL);
		}

		public static bool CheckChangeFromReceiptToCash(string strEmpID,string strFromDate,string strToDate)
		{
			string strSQL="WF_spfrmUniform @Activity='CheckChangeFromReceiptToCash',@EmpID='"+strEmpID+"',@FromDate='"+strFromDate+"',@ToDate='"+strToDate+"'";
			DataTable dt=clsCommon.GetDataTable(strSQL);
			if(dt.Rows.Count>0)
				if(dt.Rows[0].IsNull("EmpID")==false) return false;
				else return true;

			return true;
		}

		public static DataTable LoadListByRequestDate(string strRequestDate,string strEmpID,string strEmpName,string strDivision,
			string strDepartment,string strLocation,string strSection,string strPosition,string strJobCode)
		{
			string strSQL="WF_spfrmUniform @Activity='LoadListByRequestDate',@RequestDate='"+strRequestDate+"',";
			strSQL+="@EmpID='"+strEmpID+"',";
			strSQL+="@EmpName=N'"+strEmpName+"',";
			strSQL+="@Level1ID='"+strDivision+"',";
			strSQL+="@Level2ID='"+strDepartment+"',";
			strSQL+="@Level3ID='"+strSection+"',";
			strSQL+="@LocationID='"+strLocation+"',";
			strSQL+="@PositionID='"+strPosition+"',";
			strSQL+="@JobcodeID='"+strJobCode+"'";
			return clsCommon.GetDataTableHasID(strSQL);
		}

		public static DataTable LoadUniformByVoucherList(string strEmpID,string strEmpName,string strDivision,
			string strDepartment,string strLocation,string strSection,string strPosition,string strJobCode)
		{
			string pCommandSelect="SELECT WF_tblUniformVoucher.*,HR_vEmpList.EmpName ";
			pCommandSelect+="FROM WF_tblUniformVoucher INNER JOIN HR_vEmpList ON WF_tblUniformVoucher.EmpID=HR_vEmpList.EmpID ";
			
			string pCommandCondition="";

			if(strEmpID!="") pCommandCondition="WHERE HR_vEmpList.EmpID='"+strEmpID+"' ";

			if(strEmpName!="")
				if(pCommandCondition=="") pCommandCondition="WHERE HR_vEmpList.EmpName LIKE N'%"+strEmpName+"%' ";
				else pCommandCondition="AND HR_vEmpList.EmpName LIKE N'%"+strEmpName+"%' ";

			if(strDivision!="")
				if(pCommandCondition=="") pCommandCondition="WHERE HR_vEmpList.LSLevel1Code='"+strDivision+"' ";
				else pCommandCondition+=" AND HR_vEmpList.LSLevel1Code='"+strDivision+"' ";

			if(strDepartment!="")
				if(pCommandCondition=="") pCommandCondition="WHERE HR_vEmpList.LSLevel2Code='"+strDepartment+"' ";
				else pCommandCondition+=" AND HR_vEmpList.LSLevel2Code='"+strDepartment+"' ";

			if(strSection!="")
				if(pCommandCondition=="") pCommandCondition="WHERE HR_vEmpList.LSLevel3Code='"+strSection+"' ";
				else pCommandCondition+=" AND HR_vEmpList.LSLevel3Code='"+strSection+"' ";

			if(strLocation!="")
				if(pCommandCondition=="") pCommandCondition="WHERE HR_vEmpList.LSLocationCode='"+strLocation+"' ";
				else pCommandCondition+=" AND HR_vEmpList.LSLocationCode='"+strLocation+"' ";

			if(strPosition!="")
				if(pCommandCondition=="") pCommandCondition="WHERE HR_vEmpList.LSPositionCode='"+strPosition+"' ";
				else pCommandCondition+=" AND HR_vEmpList.LSPositionCode='"+strPosition+"' ";

			if(strJobCode!="")
				if(pCommandCondition=="") pCommandCondition="WHERE HR_vEmpList.LSJobClassCode='"+strJobCode+"' ";
				else pCommandCondition+=" AND HR_vEmpList.LSJobClassCode='"+strJobCode+"' ";

			string pCommandText=pCommandSelect+pCommandCondition;
			pCommandText+=" ORDER BY WF_tblUniformVoucher.RequestDate";

			return clsCommon.GetDataTableHasID(pCommandText);
		}

		public static DataTable CheckDateVoucherAndGetAmount(string strEmpID,string strDateVoucherDDMMYYY,string strReceiptType)
		{
			string pCommandText="SELECT Amount,FromDate,ToDate FROM WF_tblUniform ";
			pCommandText+="WHERE ReceiveType='"+strReceiptType+"' ";
			pCommandText+=" AND FromDate<='"+strDateVoucherDDMMYYY+"' ";
			pCommandText+=" AND ToDate>='"+strDateVoucherDDMMYYY+"' ";
			pCommandText+=" AND EmpID='"+strEmpID+"' ";

			DataTable dt=clsCommon.GetDataTable(pCommandText);
			return dt;
		}

		public static int GetDeliveredAmount(string strEmpID,string strFromDate,string strToDate)
		{
			string pCommandText="SELECT SUM(Quantity) AS TotalDelivered FROM WF_tblUniformVoucher ";
			pCommandText+=" WHERE EmpID='"+strEmpID+"' ";
			pCommandText+=" AND ReceiveDate>='"+strFromDate+"' AND ReceiveDate<='"+strToDate+"' ";
			DataTable dt=clsCommon.GetDataTable(pCommandText);
			if(dt.Rows.Count>0)
				if(dt.Rows[0].IsNull("TotalDelivered")==false)
					return int.Parse(dt.Rows[0]["TotalDelivered"].ToString());
				else return 0;
			else return 0;
		}
	}


	#region EXCEL IMPORTS CLASS	
	public class clsExcelImports
	{
		#region EXCEL FILE IMPORTS BY DATATABLE
		private static int DataTableImport(DataTable dt,string strStoreName,string strAction)
		{
			// GET STORE PROCEDURE PARAMETERS LIST			
			string strSQL="SELECT PARAMETER_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH,PARAMETER_MODE FROM Information_Schema.PARAMETERS Where Specific_name = '"+strStoreName+"'"; 
			DataTable dtParameters=clsCommon.GetDataTable(strSQL);

			// CREATE COMMAND AND ITS PARAMETERS
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			SQLconn.Open();
			SqlCommand myCommand=new SqlCommand();
			myCommand.Connection=SQLconn;
			myCommand.CommandType=CommandType.StoredProcedure;
			myCommand.CommandText=strStoreName;

			// ADD ITS PARAMETERS
			string strParamName;
			string strParamType;						
			SqlDbType paramType;
			
			for(int iIndexParams=0;iIndexParams<dtParameters.Rows.Count;iIndexParams++)
			{
				strParamName=dtParameters.Rows[iIndexParams].ItemArray[0].ToString();
				strParamType=dtParameters.Rows[iIndexParams].ItemArray[1].ToString();
				
				// DETECT DATATYPE
				paramType=GetSqlDbType(strParamType);
				
				myCommand.Parameters.Add(strParamName,paramType);
				myCommand.Parameters[strParamName].SourceColumn="";
	
				if(dtParameters.Rows[iIndexParams].ItemArray[3].ToString()!="IN")
					myCommand.Parameters[strParamName].Direction=ParameterDirection.Output;
			}

			// MAP PARAMETERS AND DATATABLE COLUMNS
			for(int iIndexParams=0;iIndexParams<myCommand.Parameters.Count;iIndexParams++)
			{
				//CHECK IF DATATABLE HAVE THE SAME COLUMN NAME
				strParamName=myCommand.Parameters[iIndexParams].ParameterName;
				if(strParamName!="@Activity")
				{
					strParamName=strParamName.Substring(1,strParamName.Length-1);
				
					for(int iIndexColumns=0;iIndexColumns<dt.Columns.Count;iIndexColumns++)
					{					
						if(dt.Columns[iIndexColumns].ColumnName.ToLower()==strParamName.ToLower())
						{						
							myCommand.Parameters[iIndexParams].SourceColumn=iIndexColumns.ToString();
							break;
						}
					}
				}
			}

			// REMOVE COLUMN NOT MATCH FOUND IN PARAMETERS
			for(int iIndexParams=0;iIndexParams<myCommand.Parameters.Count;iIndexParams++)
			{
				if(myCommand.Parameters[iIndexParams].SourceColumn=="" 
					&& myCommand.Parameters[iIndexParams].ParameterName!="@Activity"
					&& myCommand.Parameters[iIndexParams].Direction==ParameterDirection.Input)
				{
					myCommand.Parameters.RemoveAt(iIndexParams);
					iIndexParams--;
				}
			}

			// EXECUTE STORE FOR EACH ROWS IN DATATABLE
			int iResult=0;
			foreach(DataRow row in dt.Rows)
			{
				for(int iIndexParams=0;iIndexParams<myCommand.Parameters.Count;iIndexParams++)
				{					
					if(myCommand.Parameters[iIndexParams].ParameterName=="@Activity")						
					{
						myCommand.Parameters[iIndexParams].Value=strAction;
					}
					else
					{
						if(myCommand.Parameters[iIndexParams].Direction==ParameterDirection.Input)
						{
							myCommand.Parameters[iIndexParams].Value=row.ItemArray[int.Parse(myCommand.Parameters[iIndexParams].SourceColumn)].ToString();							
						}
					}					
				}
				try
				{
					iResult=myCommand.ExecuteNonQuery();
				}
				catch(SqlException ex) {}
			}

			SQLconn.Close();
			myCommand.Dispose();
			myCommand=null;

			return iResult;
		}
		#endregion

		#region GET SQL DATABASE TYPE BY STRING TYPE
		private static SqlDbType GetSqlDbType(string strType)
		{
			SqlDbType paramType;
			switch(strType)
			{
				case "nvarchar":
					paramType=SqlDbType.NVarChar;
					break;
				case "bigint":
					paramType=SqlDbType.BigInt;
					break;
				case "binary":
					paramType=SqlDbType.Binary;
					break;
				case "bit":
					paramType=SqlDbType.Bit;
					break;
				case "char":
					paramType=SqlDbType.Char;
					break;
				case "datetime":
					paramType=SqlDbType.DateTime;
					break;
				case "decimal":
					paramType=SqlDbType.Decimal;
					break;
				case "float":
					paramType=SqlDbType.Float;
					break;
				case "image":
					paramType=SqlDbType.Image;
					break;
				case "int":
					paramType=SqlDbType.Int;
					break;
				case "money":
					paramType=SqlDbType.Money;
					break;
				case "nchar":
					paramType=SqlDbType.NChar;
					break;
				case "ntext":
					paramType=SqlDbType.NText;
					break;
				case "real":
					paramType=SqlDbType.Real;
					break;
				case "smalldatetime":
					paramType=SqlDbType.SmallDateTime;
					break;
				case "smallmoney":
					paramType=SqlDbType.SmallMoney;
					break;
				case "text":
					paramType=SqlDbType.Text;
					break;
				case "timestamp":
					paramType=SqlDbType.Timestamp;
					break;
				case "tinyint":
					paramType=SqlDbType.TinyInt;
					break;
				case "uniqueidentifier":
					paramType=SqlDbType.UniqueIdentifier;
					break;
				case "varbinary":
					paramType=SqlDbType.VarBinary;
					break;
				case "varchar":
					paramType=SqlDbType.VarChar;
					break;
				case "variant":
					paramType=SqlDbType.Variant;
					break;
				case "smallint":
					paramType=SqlDbType.SmallInt;
					break;
				default:
					paramType=SqlDbType.NVarChar;
					break;
			}
			return paramType;
		}
		#endregion

		#region PUBLIC EXCEL FILE IMPORTS FUNCTION
		public static int ExcelFileImport(string strExcelFileName,string strStoreProcedure,string strActivity)
		{
			int iRecordAffected=0;

			string strConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source="+strExcelFileName+@";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""" ;
			OleDbConnection  oleConnection =new OleDbConnection (strConnectionString);
			oleConnection.Open();
			
			// RETRIEVE EXCEL SHEET LIST
			DataTable dt = new DataTable ();
			dt=oleConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,null);
			if(dt==null)
				return 0; // THERE'S NOT ANY SHEET AVAILBLE IN EXCEL FILE
			string[] excelSheets=new string[dt.Rows.Count];
			int iIndexSheet=0;
			foreach(DataRow row in dt.Rows)
			{
				excelSheets[iIndexSheet]=row["TABLE_NAME"].ToString();
				iIndexSheet++;
			}

			// UPDATE DATA IN ALL SHEETS
			OleDbCommand  cmdSelect=new OleDbCommand();;
			OleDbDataAdapter oleAdapter=new OleDbDataAdapter();
			for(iIndexSheet=0;iIndexSheet<excelSheets.Length;iIndexSheet++)
			{
				cmdSelect = new OleDbCommand (@"SELECT * FROM ["+excelSheets[iIndexSheet]+"]", oleConnection);
				oleAdapter = new OleDbDataAdapter();
				oleAdapter.SelectCommand  = cmdSelect;
				dt.Clear();
				try {oleAdapter.Fill(dt); } 
				catch(Exception ex) {}
				
				// IMPORT TO SQL
				if(dt!=null && dt.Rows.Count>0)
					iRecordAffected+=DataTableImport(dt,strStoreProcedure,strActivity);				
			}	
			// RELEASE OBJECTS
			oleConnection.Close();
			cmdSelect.Dispose();
			cmdSelect=null;
			oleAdapter.Dispose();
			oleAdapter=null;
			
			return iRecordAffected;
		}
		#endregion
	}	
	#endregion
}

namespace iHRPCore
{
	public abstract class DataGridExporterBase
	{
		/// <summary>
		/// Holds a reference to the datagrid being exported
		/// </summary>
		protected DataGrid MyDataGrid;

		/// <summary>
		/// Holds a reference to the page where the datagrid locates
		/// </summary>
		protected Page CurrentPage;

		/// <summary>
		/// Overloaded. Initializes a new instance of the DataGridExporterBase class.
		/// </summary>
		/// <param name="dg">The datagrid to be exported</param>
		/// <param name="pg">The page to which the datagrid is to be exported</param>
		public DataGridExporterBase(DataGrid dg, Page pg)
		{
			MyDataGrid = dg;
			CurrentPage = pg;
		}

		/// <summary>
		/// Overloaded. Initializes a new instance of the DataGridExporterBase class.
		/// </summary>
		/// <param name="dg">The datagrid to be exported</param>
		public DataGridExporterBase(DataGrid dg):this(dg, dg.Page)
		{
		}

		/// <summary>
		/// Exports the current datagrid
		/// </summary>
		public abstract void Export();
	}

	/// <summary>
	/// Exports a datagrid to a excel file. 
	/// </summary>
	/// <requirements>Microsoft Excel 97 or above should be installed on the client machine in order to make 
	/// this function work
	/// </requirements>
	public class DataGridExcelExporter:DataGridExporterBase
	{

		/// <summary>
		/// CSS file for decoration, se it if any or dont use it
		/// </summary>
		private const string MY_CSS_FILE = "./css/MDF.css";

		/// <summary>
		/// Overloaded. Initializes a new instance of the DataGridExcelExporter class.
		/// </summary>
		/// <param name="dg">The datagrid to be exported</param>
		/// <param name="pg">The page to which the datagrid is to be exported</param>
		public DataGridExcelExporter(DataGrid dg, Page pg):base(dg, pg)
		{
		}

		/// <summary>
		/// Overloaded. Initializes a new instance of the DataGridExcelExporter class.
		/// </summary>
		/// <param name="dg">The datagrid to be exported</param>
		public DataGridExcelExporter(DataGrid dg):base(dg)
		{
		}

		/// <summary>
		/// Overloaded. Exports a datagrid to an excel file, the title of which is empty
		/// </summary>
		public override void Export()
		{
			Export(String.Empty);
		}

		/// <summary>
		/// Renders the html text before the datagrid.
		/// </summary>
		/// <param name="writer">A HtmlTextWriter to write html to output stream</param>
		protected virtual void FrontDecorator(HtmlTextWriter writer)
		{
			writer.WriteFullBeginTag("HTML");
			writer.WriteFullBeginTag("meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\"");
			writer.WriteFullBeginTag("Head");
			writer.RenderBeginTag(HtmlTextWriterTag.Style);
			writer.Write("<!--");
			
			if(File.Exists(CurrentPage.MapPath(MY_CSS_FILE)))
			{
				StreamReader sr = File.OpenText(CurrentPage.MapPath(MY_CSS_FILE));			
				String input;
				while ((input=sr.ReadLine())!=null)
				{
					writer.WriteLine(input);
				}
				sr.Close();
			}
			writer.Write("-->");
			writer.RenderEndTag();
			writer.WriteEndTag("Head");
			writer.WriteFullBeginTag("Body");
		}
		/// <summary>
		/// Renders the html text after the datagrid.
		/// </summary>
		/// <param name="writer">A HtmlTextWriter to write html to output stream</param>
		protected virtual void RearDecorator(HtmlTextWriter writer)
		{
			writer.WriteEndTag("Body");
			writer.WriteEndTag("HTML");
		}

		/// <summary>
		/// Exports the datagrid to an Excel file with the name of the datasheet provided by the passed in parameter
		/// </summary>
		/// <param name="reportName">Name of the datasheet.
		/// </param>
		public virtual void Export(string reportName)
		{
			ClearChildControls(MyDataGrid);
			AddSpaceControls(MyDataGrid);
			MyDataGrid.EnableViewState = false;//Gets rid of the viewstate of the control. The viewstate may make an excel file unreadable.

		
			CurrentPage.Response.Clear();
			CurrentPage.Response.Buffer = true; 

			//This will make the browser interpret the output as an Excel file
			//CurrentPage.Response.AddHeader( "Content-Disposition", "filename="+reportName);
			CurrentPage.Response.AddHeader( "Content-Disposition", "attachment; filename=export.xls");
			CurrentPage.Response.ContentType="application/vnd.ms-excel";


			//Prepares the html and write it into a StringWriter
			StringWriter stringWriter = new StringWriter();
			HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);
			FrontDecorator(htmlWriter);
			MyDataGrid.RenderControl(htmlWriter);
			RearDecorator(htmlWriter);

			//Write the content to the web browser
			CurrentPage.Response.Write(stringWriter.ToString());
			CurrentPage.Response.End();
		}

		/// <summary>
		/// Iterates a control and its children controls, ensuring they are all LiteralControls
		/// <remarks>
		/// Only LiteralControl can call RenderControl(System.Web.UI.HTMLTextWriter htmlWriter) method. Otherwise 
		/// a runtime error will occur. This is the reason why this method exists.
		/// </remarks>
		/// </summary>
		/// <param name="control">The control to be cleared and verified</param>
		private void RecursiveClear(System.Web.UI.Control control)
		{
			//Clears children controls
			for (int i=control.Controls.Count -1; i>=0; i--)
			{
				RecursiveClear(control.Controls[i]);
			}

			//
			//If it is a LinkButton, convert it to a LiteralControl
			//
			if (control is LinkButton) 
			{
				LiteralControl literal = new LiteralControl();
				control.Parent.Controls.Add(literal);
				literal.Text = "&nbsp;" + ((LinkButton)control).Text;
				control.Parent.Controls.Remove(control);
			}
				//We don't need a button in the excel sheet, so simply delete it
			else if(control is Button)
			{
				control.Parent.Controls.Remove(control);
			}

				//If it is a ListControl, copy the text to a new LiteralControl
			else if(control is ListControl)
			{
				LiteralControl literal = new LiteralControl();
				control.Parent.Controls.Add(literal);
				try
				{
					literal.Text = "&nbsp;" + ((ListControl)control).SelectedItem.Text;
				}
				catch
				{
				}
				control.Parent.Controls.Remove(control);
						
			}
			else if(control is CheckBox)
			{
				control.Parent.Controls.Remove(control);
			}
			else if(control is TextBox)
			{
				LiteralControl literal = new LiteralControl();
				control.Parent.Controls.Add(literal);
				try
				{
					literal.Text = "&nbsp;" + ((TextBox)control).Text;
				}
				catch
				{
				}
				control.Parent.Controls.Remove(control);
			}
			else if(control is ImageButton)
			{				
				control.Parent.Controls.Remove(control);
			}

			//You may add more conditions when necessary

			return;
		}

		
		/// <summary>
		/// Clears the child controls of a Datagrid to make sure all controls are LiteralControls
		/// </summary>
		/// <param name="dg">Datagrid to be cleared and verified</param>
		protected void ClearChildControls(DataGrid dg)
		{
			
			for(int i = dg.Columns.Count -1 ; i>=0; i--)
			{
				DataGridColumn column = dg.Columns[i];
				if (column is ButtonColumn)
				{
					dg.Columns.Remove(column);
				}
			}
			
			this.RecursiveClear(dg);
			
		}

		protected void AddSpaceControls(DataGrid dg)
		{
			for (int i =0; i < dg.Items.Count;i++)
			{
				System.Web.UI.WebControls.TableCellCollection cells = (System.Web.UI.WebControls.TableCellCollection)dg.Items[i].Cells;
				foreach (System.Web.UI.WebControls.TableCell cell in cells)
				{
					if (cell.Text.ToString().Trim() != "")
						cell.Text = "&nbsp;" + cell.Text;
					string a = cell.Text;
				}
			}
		}

	}

	/// <summary>
	/// HTML Encodes an entire DataGrid. 
	/// It iterates through each cell in the TableRow, ensuring that all 
	/// the text being displayed is HTML Encoded, irrespective of whether 
	/// they are just plain text, buttons, hyperlinks, multiple controls etc..
	/// </summary>
	public class CellFormater
	{
		/// <summary>
		/// Constructs an instance of the CellFormater class.
		/// </summary>
		public CellFormater()
		{
			//
			// TODO: Add constructor logic here
			//
			
		}

		/// <summary>
		/// Method that HTML Encodes an entire DataGrid. 
		/// It iterates through each cell in the TableRow, ensuring that all 
		/// the text being displayed is HTML Encoded, irrespective of whether 
		/// they are just plain text, buttons, hyperlinks, multiple controls etc..
		/// <seealso cref="System.Web.UI.WebControls.DataGrid.ItemDataBound">DataGrid.ItemDataBound Event</seealso>
		/// </summary>
		/// <param name="item">
		/// The DataGridItem that is currently being bound in the calling Web 
		/// Page's DataGrid.ItemDataBound Event.
		/// </param>
		/// <remarks>
		/// This method should be called from the 
		/// <c>DataGrid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)</c> 
		/// event in the respective Web View Codebehind.
		/// </remarks>
		/// <example>
		///          We want to HTMLEncode a complete DataGrid (all columns and all 
		///          rows that may/do contain characters that will require encoding 
		///          for display in HTML) called dgIssues.
		///          Use the following code for the ItemDataBound Event:
		///          <code>
		///               private void dgIssues_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		///               {
		///                    WebMethod wm = new WebMethod();
		///                    wm.DataGrid_ItemDataBound_HTMLEncode((DataGridItem) e.Item);
		///               }//dgIssues_ItemDataBound
		///          </code>
		/// </example>
		public void AdHocHTMLEncode(System.Web.UI.WebControls.DataGridItem item)
		{
			bool doHTMLEncode = false;
			switch (item.ItemType)
			{                         
					#region DataBound
					//The following case statements are in ascending TableItemStyle order.
					//See ms-help://MS.VSCC/MS.MSDNVS/cpref/html/frlrfsystemwebuiwebcontrolsdatagridclassitemstyletopic.htm for details.
				case System.Web.UI.WebControls.ListItemType.Item:
				{
					doHTMLEncode = true;
					break;
				}//ListItemType.Item
				case System.Web.UI.WebControls.ListItemType.AlternatingItem:
				{					
					doHTMLEncode = true;
					break;
				}//ListItemType.AlternatingItem
				case System.Web.UI.WebControls.ListItemType.SelectedItem:
				{
					doHTMLEncode = true;
					break;
				}//ListItemType.SelectedItem					
				case System.Web.UI.WebControls.ListItemType.EditItem:
				{
					//These should not be prone to this as TextBoxes aren't.
					doHTMLEncode = false;
					break;
				}//ListItemType.EditItem
					#endregion DataBound
					#region Non-DataBound
					//The remainder are the other ListItemTypes that are non-Data-bound.
				case System.Web.UI.WebControls.ListItemType.Header:
				{
					//We might have specified Headers like "<ID>".
					doHTMLEncode = true;
					break;
				}//ListItemType.Header
				case System.Web.UI.WebControls.ListItemType.Footer:
				{
					//Similarly for the Footer as with the Header.
					doHTMLEncode = true;

					break;
				}//ListItemType.Footer
				case System.Web.UI.WebControls.ListItemType.Pager:
				{
					//With just numbers or buttons, none is required.
					//However, for buttons, this is not strictly true as you 
					//need to specify the text on the buttons. But the Property 
					//Builder for the DataGrid hints in its defaults that these 
					//need to be HTMLencoded anyway.
					doHTMLEncode = false;
					break;
				}//ListItemType.Pager
				case System.Web.UI.WebControls.ListItemType.Separator:
				{
					doHTMLEncode = false;
					break;
				}//ListItemType.Separator
					#endregion Non-DataBound
				default:
				{
					//This will never be executed as all ItemTypes are listed above.
					break;
				}//default
			}//switch

			if (doHTMLEncode)
			{
				//Encode the cells dependent on the type of content 
				//within (e.g. BoundColumn, Hyperlink), taking into account 
				//that there may be more than one (or even zero) control in 
				//each cell.
				System.Web.UI.WebControls.TableCellCollection cells = (System.Web.UI.WebControls.TableCellCollection)item.Cells;
				foreach (System.Web.UI.WebControls.TableCell cell in cells)
				{
					if (cell.Controls.Count != 0)
					{
						foreach (System.Web.UI.Control ctrl in cell.Controls)
						{
							if (ctrl is Button)
							{
								Button btn = (Button) ctrl;
								btn.Text = HttpUtility.HtmlEncode(btn.Text);
							}//if
							else if (ctrl is HyperLink)
							{
								HyperLink hyp = (HyperLink) ctrl;
								hyp.Text = HttpUtility.HtmlEncode(hyp.Text);
								//hyp.NavigateUrl = HttpUtility.UrlEncode(hyp.NavigateUrl);
							}//else if
							else if (ctrl is LinkButton)
							{
								LinkButton lb = (LinkButton) ctrl;
								lb.Text = HttpUtility.HtmlEncode(lb.Text);
							}//else if
								// this check is for to change the forecolor of REJECTED activities to red
							else if(ctrl is Label)
							{
								Label objL = (Label)ctrl;
								if(objL.Text == "REJECTED")
									objL.ForeColor = System.Drawing.Color.Red;
							}//else if
						}//foreach
					}//if
					else
					{
						//The cell is a BoundColumn.
						if (cell.Text.ToLower().Trim()!="&nbsp;") 
							cell.Text = HttpUtility.HtmlEncode(cell.Text);
						
					}//else
				}//foreach
			}//if
		}//DataGrid_ItemDataBound_HTMLEncode
	}
	
	public class clsTRTrainingCourse
	{
		public static void LoadComboCourseType(DropDownList pCtl,bool bBlank,bool m_bAddNew,bool bIsPublic)
		{
			string pCommandText="SELECT TRCourseTypeCode,Name,Used AS IsUsed FROM LS_tblTRCourseType";
			if(bIsPublic==true)
				pCommandText+=" WHERE CourseType=1";
			else pCommandText+=" WHERE CourseType=0";

			if(m_bAddNew==false)
				clsCommon.LoadDropDownListControl(pCtl,pCommandText,"TRCourseTypeCode","Name",bBlank);
			else clsCommon.BindDropDownListControl(pCtl,pCommandText,"TRCourseTypeCode","Name",bBlank);
		}

		public static void LoadComboSupplier(DropDownList pCtl,bool bBlank,bool m_bAddNew)
		{
			string pCommandText="SELECT TRSupplierCode,Name,Used AS IsUsed FROM LS_tblTRSupplier";
			if(m_bAddNew==false)
				clsCommon.LoadDropDownListControl(pCtl,pCommandText,"TRSupplierCode","Name",bBlank);
			else clsCommon.BindDropDownListControl(pCtl,pCommandText,"TRSupplierCode","Name",bBlank);
		}

		public static DataTable LoadTrainingCourses()
		{
			string pCommandText="SELECT TR_tblTrainingCourse.*,LS_tblSupplier.LSSupplierCode AS SupplierCode,LS_tblSupplier.Name AS SupplierName,";
			pCommandText+="LS_tblCourseType.CourseID AS CourseCode,LS_tblCourseType.Name AS CourseName ";
			pCommandText+="FROM TR_tblTrainingCourse INNER JOIN LS_tblSupplier ON TR_tblTrainingCourse.SupplierID=LS_tblSupplier.LSSupplierCode ";
			pCommandText+="INNER JOIN LS_tblCourseType ON TR_tblTrainingCourse.CourseID=LS_tblCourseType.CourseID ";
			return clsCommon.GetDataTableHasID(pCommandText);
		}
	}

	
}
