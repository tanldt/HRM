using System;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Web.Caching;
using System.Web.Configuration;
using System.Data;
using System.Data.OracleClient;
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


namespace iHRPCore.Com.Oracle
{
	/// <summary>
	/// Creater: Tanldt
	/// Create Date: 2006 03 15
	/// Purpose: for common functions, procedures
	/// </summary>	
	public class clsCommonOra
	{
		#region Variables
		private static OracleConnection ORAconn= new OracleConnection(ConfigurationSettings.AppSettings["connString"]);
		public static OracleTransaction OracleTransaction;
		public static bool AutoIncrease = false;//Thêm nhân viên theo mã tự động hay không
		static string strControl;
		
		#endregion
		#region Hàm dùng để Connection đến Data Oracle
		#region Khai báo biến (Connect)
		public static string mConnectionString;
		public static OracleConnection m_Connection;
		public static DataSet m_DataSet;
		public static OracleCommand m_Command = new OracleCommand();
		public static OracleDataAdapter m_DataAdapter;
		#endregion
		public static OracleParameterCollection PARAMETERS 
		{
			get 
			{
				m_Command.Parameters.Clear();
				return m_Command.Parameters;
			}
		}

		public static OracleCommand COMMAND 
		{
			get 
			{
				return m_Command;
			}
			set 
			{
				m_Command = value;
			}
		}

		public static OracleConnection CONNECTION 
		{
			get 
			{
				return m_Connection;
			}
			set 
			{
				m_Connection = value;
			}
		}

		public static bool Connect()
		{
			//Connect = true;
			try 
			{
				mConnectionString = ConfigurationSettings.AppSettings["connString"];
				m_Connection = new OracleConnection(mConnectionString);
				m_Connection.Open();
				return true;
			} 
			catch (Exception ex) 
			{
				throw (ex);
			} 
		}

		public static void CloseConnection()
		{
			try 
			{
				if ((m_Connection.State == ConnectionState.Open)) 
				{
					m_Connection.Close();
					m_Connection.Dispose();
				}
			} 
			catch (Exception ex) 
			{
				throw (ex);
			} 
			finally 
			{
			}
		}


		public static bool Execute(string sql)
		{
			if ((Connect() == false)) 
			{
				return false;
			}
			OracleCommand cmd = new OracleCommand(sql, m_Connection);
			cmd.CommandType = CommandType.Text;
			try 
			{
				cmd.ExecuteNonQuery();
			} 
			catch (Exception e) 
			{
				throw (e);
				//return false;
			}
			CloseConnection();
			return true;
		}

		public static bool ExecuteSQL(string spName)
		{
			//int i;
			if ((Connect() == false)) 
			{
				return false;
			}
			
			m_Command.Connection = m_Connection;
			m_Command.CommandText = spName;
			m_Command.CommandType = CommandType.StoredProcedure;
			try 
			{
				m_Command.ExecuteNonQuery();
			} 
			catch (Exception ex) 
			{
				throw (ex);
				//return false;
			} 
			finally 
			{
				CloseConnection();
			}
			return true;
		}


		public static object SafeDataToClient(object pData, string pConvertType)
		{
			object retvals;
			retvals = pData;
			string strGetType;
			if (pData == null) 
			{
				return pData;
			}
			strGetType = pData.GetType().FullName;
			if (strGetType == "System.DBNull") 
			{
				#region String
				if (pConvertType == "String") 
				{
					retvals = null;
				} 
				else if (pConvertType == "Integer") 
				{
					retvals = null;
				} 
				else if (pConvertType == "DateTime") 
				{
					retvals = null;
				}
				#endregion
			} 
			else if (strGetType == "System.Boolean") 
			{
				#region Boolean
				if (pConvertType == "Boolean") 
				{
					retvals = pData;
				} 
				else if (pConvertType == "Integer") 
				{
					retvals = Convert.ToInt32(pData);
				}
				#endregion
			} 
			else if (strGetType == "System.String") 
			{
				#region String
				if (pConvertType == "String") 
				{
					retvals = pData;
				} 
				else if (pConvertType == "Integer") 
				{
					try 
					{
						if (IsNumeric(pData)) 
						{
							retvals = Convert.ToInt32(pData);
						}
					} 
					catch (Exception ex) 
					{
						//pErr = ex.Message;
						return pData;
					}
				}
				#endregion
			} 
			else if (strGetType == "System.Integer" || strGetType == "System.Int16" || strGetType == "System.Int32" || strGetType == "System.Decimal" || strGetType == "System.Double") 
			{
				#region Integer
				if (pConvertType == "Integer") 
				{
					retvals = pData;
				} 
				else if (pConvertType == "String") 
				{
					retvals = Convert.ToString(pData);
				}
				#endregion
			} 

			return retvals;
		}

		public static object SafeDataToDatabase(object pData, string pConvertType)
		{
			object retval;
			string strGetType;
			retval = pData;
			if (pData is DBNull)
			{
				return Convert.ToString(pData);
			}

			//pData = IIf(IsNothing(pData) == true, "", pData);
			if (pData.ToString().Trim() == string.Empty) 
			{
				return DBNull.Value;
			}
			strGetType = pData.GetType().FullName;
			if (strGetType == "System.DBNull") 
			{
				if (pConvertType == "String") 
				{
					retval = "";
				} 
				else if (pConvertType == "DBNull") 
				{
					retval = DBNull.Value;
				} 
				else if (pConvertType == "Integer" | pConvertType == "Double" | pConvertType == "Decimal") 
				{
					retval = 0;
				} 
				else if (pConvertType == "DateTime") 
				{
					retval = DBNull.Value;
				}
				return retval;
			} 
			else if (strGetType == "System.String") 
			{
				if (pConvertType == "String") 
				{
					retval = pData;
				} 
				else if (pConvertType == "Integer") 
				{
					try 
					{
						if (IsNumeric(pData)) 
						{
							retval = Convert.ToInt32(pData);
						}
					} 
					catch (Exception ex) 
					{
						//pErr = ex.Message;
						//return pData;
						return DBNull.Value;
					}
				} 
				else if (pConvertType == "DateTime") 
				{
					try 
					{
						retval = Convert.ToDateTime(pData);
					} 
					catch (Exception ex) 
					{
						//pErr = ex.Message;
						//return pData;
						return DBNull.Value;
					}
				} 
				else if (pConvertType == "DBNull") 
				{
					if (pData.ToString() == "") 
					{
						retval = DBNull.Value;
					}
				}
				return retval;
			} 
			else if (strGetType == "System.Int32") 
			{
				if (pConvertType == "Integer") 
				{
					retval = pData;
				} 
				else if (pConvertType == "String") 
				{
					retval = Convert.ToString(pData);
				} 
				else if (pConvertType == "DBNull") 
				{
					if (Convert.ToInt16(pData.ToString()) == 0) 
					{
						retval = DBNull.Value;
					}
				}
				return retval;
			} 
			else if (strGetType == "System.Double") 
			{
				if (pConvertType == "Double") 
				{
					retval = pData;
				} 
				else if (pConvertType == "String") 
				{
					retval = Convert.ToString(pData);
				}
				return retval;
			} 
			else if (strGetType == "System.Decimal") 
			{
				if (pConvertType == "Decimal") 
				{
					retval = pData;
				} 
				else if (pConvertType == "String") 
				{
					retval = Convert.ToString(pData);
				}
				return retval;
			} 

			return retval;
		}
		public static bool IsNumeric(Object pValue)
		{
			if ( (pValue is DBNull))
			{
				return false;
			}
			else if(Convert.ToString(pValue) == "")
			{
				return false;
			}
			try
			{
				int i =  Convert.ToInt32(pValue);
				return true;
			}
			catch
			{
				return false;
			}
		}
		#endregion
		#region Get Data OracleDataReader
		public static OracleDataReader GetDataFrom(string spName)
		{
			if ((Connect() == false)) 
			{
				return null;
			}
			OracleCommand cmd = new OracleCommand(spName, m_Connection);
			cmd.CommandType = CommandType.StoredProcedure;

			try 
			{
				OracleDataReader DataReader = cmd.ExecuteReader();
				DataReader.Read();
				return DataReader;
				//return DataReader = cmd.ExecuteScalar();
			} 
			catch (Exception ex) 
			{
				throw (ex);
			} 
			finally 
			{
				
				CloseConnection();
			}
		}
		
		public static OracleDataReader GetDataReaderFrom(string spName)
		{
			OracleDataReader myDataReader;
			if ((Connect() == false)) 
			{
				return null;
			}
			m_Command.Connection = m_Connection;
			m_Command.CommandText = spName;
			m_Command.CommandType = CommandType.StoredProcedure;
			try 
			{
				myDataReader = m_Command.ExecuteReader();
				return myDataReader;
			} 
			catch (Exception ex) 
			{
				throw (ex);
			} 
			finally 
			{
			}
		}
		#endregion
		#region Get DataTable
		/// <summary>
		/// Tanldt
		/// Ngay: 2006 03 16
		/// </summary>
		/// <param name="spName">Ten Store hoac Packages</param>
		/// <returns>DataTable</returns>
		public static DataTable GetDataTable_ByStore(string spName)
		{
			DataTable table = new DataTable();
			if ((Connect() == false)) 
			{
				return null;
			}
			m_Command.Connection = m_Connection;
			m_Command.CommandText = spName;
			m_Command.CommandType = CommandType.StoredProcedure;
			try 
			{
				m_DataAdapter = new OracleDataAdapter(m_Command);
				m_DataAdapter.Fill(table);
				return table;
			} 
			catch (Exception ex) 
			{
				return null;
				throw (ex);
			} 
			finally 
			{
				CloseConnection();
			}
		}
		/// <summary>
		/// Tanldt
		/// Ngay: 2006 03 16
		/// </summary>
		/// <param name="spName">Ten Store hoac Packages</param>
		/// <returns>DataTable co cot No</returns>
		public static DataTable GetDataTableHasID_ByStore(string spName)
		{
			DataTable table = new DataTable();
			if ((Connect() == false)) 
			{
				return null;
			}
			m_Command.Connection = m_Connection;
			m_Command.CommandText = spName;
			m_Command.CommandType = CommandType.StoredProcedure;
			try 
			{
				m_DataAdapter = new OracleDataAdapter(m_Command);
				DataColumn cl = new DataColumn("No");
				cl.AutoIncrement = true;
				cl.AutoIncrementSeed = 1;
				table.Columns.Add(cl);
				m_DataAdapter.Fill(table);

				return table;
			} 
			catch (Exception ex) 
			{
				return null;
				throw (ex);
			} 
			finally 
			{
				CloseConnection();
			}
		}
		/// <summary>
		/// Params: string pstrSQl
		/// Purpose: get Records from SQL string to Datatable
		/// </summary>
		public static DataTable GetDataTable_ByText(string pstrSQL)
		{
			OracleCommand cmd = new OracleCommand();
			OracleConnection ORAconn= new OracleConnection(ConfigurationSettings.AppSettings["connString"]);
			DataTable rsData = new DataTable();
			OracleDataAdapter adpAdapter = new OracleDataAdapter();

			try
			{
				ORAconn.Open();
				cmd.Connection = ORAconn;
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = pstrSQL;

				adpAdapter.SelectCommand = cmd;
				adpAdapter.Fill(rsData);

				cmd.Dispose();
				ORAconn.Close();
				ORAconn.Dispose();
				adpAdapter.Dispose();
				return rsData;
			}
			catch(Exception exp)
			{                
				cmd.Dispose();
				ORAconn.Close();
				ORAconn.Dispose();
				adpAdapter.Dispose();
				string strErr = exp.Message;
				return null;
			}
		}
		/// <summary>
		/// Params: string pstrSQl
		/// Purpose: get Records from SQL string to Datatable
		/// </summary>
		public static DataTable GetDataTableHasID_ByText(string pstrSQL)
		{
			OracleCommand cmd = new OracleCommand();
			OracleConnection ORAconn= new OracleConnection(ConfigurationSettings.AppSettings["connString"]);
			DataTable rsData = new DataTable();
			OracleDataAdapter adpAdapter = new OracleDataAdapter();
			try
			{
				ORAconn.Open();
				cmd.Connection = ORAconn;
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = pstrSQL;
			
				adpAdapter.SelectCommand = cmd;
				DataColumn cl = new DataColumn("No");
				cl.AutoIncrement = true;
				cl.AutoIncrementSeed = 1;
				rsData.Columns.Add(cl);
				adpAdapter.Fill(rsData);

				cmd.Dispose();
				ORAconn.Close();
				ORAconn.Dispose();
				adpAdapter.Dispose();
				return rsData;
			}
			catch(Exception exp)
			{                
				cmd.Dispose();
				ORAconn.Close();
				ORAconn.Dispose();
				adpAdapter.Dispose();
				string strErr = exp.Message;
				return null;
			}
		}
		/// <summary>
		/// Params: string pstrSQl
		/// Purpose: get Records from SQL string to Datatable
		/// </summary>
		public static DataTable GetDataTableTimeOut_ByText(string pstrSQL)
		{
			OracleCommand cmd = new OracleCommand();
			OracleConnection ORAconn= new OracleConnection(ConfigurationSettings.AppSettings["connString"]);
			DataTable rsData = new DataTable();
			OracleDataAdapter adpAdapter = new OracleDataAdapter();
			try
			{
				ORAconn.Open();
				cmd.Connection = ORAconn;
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = pstrSQL;
				//cmd.CommandTimeout = 8000;
			
				adpAdapter.SelectCommand = cmd;
				DataColumn cl = new DataColumn("No");				
				cl.AutoIncrement = true;
				cl.AutoIncrementSeed = 1;
				rsData.Columns.Add(cl);
				adpAdapter.Fill(rsData);

				cmd.Dispose();
				ORAconn.Close();
				ORAconn.Dispose();
				adpAdapter.Dispose();
				return rsData;
			}
			catch(Exception exp)
			{                
				cmd.Dispose();
				ORAconn.Close();
				ORAconn.Dispose();
				adpAdapter.Dispose();
				string strErr = exp.Message;
				return null;
			}
		}
		#endregion
		#region Get DataRow
			/// <summary>
			/// creater : Tanldt
			/// CreateDate : 2006 03 15			
			/// </summary>
			/// <param name="pCommandText">command string</param>
			/// <returns>tra ve mot datarow, neu khong co du lieu tra ve null</returns>
			public static DataRow GetDataRow(string pCommandText)
			{				
				DataTable  dt = new DataTable();
				DataRow mRow;
				dt = GetDataTable_ByText(pCommandText);
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
			/// <summary>
			/// creater : Tanldt
			/// CreateDate : 2006 03 16			
			/// </summary>
			/// <param name="pCommandStore">command string</param>
			/// <returns>tra ve mot datarow, neu khong co du lieu tra ve null</returns>
			public static DataRow GetDataRow_ByStore(string pCommandStore)
			{				
				DataTable  dt = new DataTable();
				DataRow mRow;
				dt = GetDataTable_ByStore(pCommandStore);
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
			#endregion
		#region Get DataSet
			/// <summary>
			/// creater : Tanldt
			/// CreateDate : 2006 03 15			
			/// </summary>
			/// <param name="pCommandText">command string</param>
			/// <returns>tra ve mot dataSet, neu khong co du lieu tra ve null</returns>
			public static DataSet GetDataSet(string pCommandText)
			{
				try
				{
					DataSet ds = new DataSet();
					DataTable  dt = new DataTable();
					dt = GetDataTable_ByText(pCommandText);
					ds.Tables.Add(dt);
					dt.Dispose();				
					return ds;
				}
				catch
				{
					return null;
				}
			}
			/// <summary>
			/// creater : Tanldt
			/// CreateDate : 2006 03 16
			/// </summary>
			/// <param name="pCommandStore">command string</param>
			/// <returns>tra ve mot dataSet, neu khong co du lieu tra ve null</returns>
			public static DataSet GetDataSet_ByStore(string pCommandStore)
			{
				try
				{
					DataSet ds = new DataSet();
					DataTable  dt = new DataTable();
					dt = GetDataTable_ByStore(pCommandStore);
					ds.Tables.Add(dt);
					dt.Dispose();				
					return ds;
				}
				catch
				{
					return null;
				}
			}
			#endregion
		#region Phần liên quan đến Tools Import : Chua Kiem tra ben Oracle Tanldt
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
				OleDbDataAdapter myCommand = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", strConn);
				DataSet myDataSet = new DataSet();
				myCommand.Fill(myDataSet, "ExcelData");
				
				DataTable dtObj = new DataTable() ;
				dtObj = GetDataTable_ByText("sp_Common @ObjName=N'" + strProcedureName + "'");
				OracleTransaction OracleTran;
				OracleConnection cnn = new OracleConnection(ConfigurationSettings.AppSettings["connString"]);			
				cnn.Open();
				OracleTran = cnn.BeginTransaction();			
				OracleCommand cmd = new OracleCommand();
				cmd.Transaction = OracleTran;
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
						cmd.Parameters.Add("@Activity", OracleType.NVarChar,50).Value = strActivity.Trim();
						cmd.Parameters.Add(strParamUserAction,OracleType.NVarChar,10).Value = strUserAction;
						for(int j=0;j<iCom;j++)
						{
							//string[] strParam[i] = myDataSet.Tables["ExcelData"].Rows[0][i].ToString().Trim();
							string a = myDataSet.Tables["ExcelData"].Rows[0][j].ToString().Trim();
							DataRow[] iRow = dtObj.Select("columnname ='@" + myDataSet.Tables["ExcelData"].Rows[0][j].ToString().Trim() + "'");
							if(iRow.Length > 0)
							{
								OracleParameter param = new OracleParameter();
								param.ParameterName = iRow[0]["columnname"].ToString().Trim();
								param.OracleType = ConvertDataType(iRow[0]["datatype"].ToString().Trim());
								param.Size = SafeDataInteger(iRow[0]["size"].ToString().Trim());
								//Value
								string strValue = myDataSet.Tables["ExcelData"].Rows[i][j].ToString().Trim();

								if(param.OracleType== OracleType.Float) // Tanldt: Kieu nay chua biet cach chuyen qua Oracle
									strValue = strValue.Replace(",","");
							
								if(strValue.Replace("&nbsp;","").Trim()=="")
									param.Value = DBNull.Value ;
								else
								{
									if(param.OracleType == OracleType.Int16)
										param.Value = (strValue=="True" || strValue=="1")?1:0;
									else
										param.Value = strValue; 
								}
							
								cmd.Parameters.Add(param);
							}
						}
						cmd.ExecuteNonQuery();
					}
					
					#endregion
					if (OracleTran != null ) OracleTran.Commit();
					myCommand.Dispose();				
					myDataSet.Dispose();
					return "Successful!";
				}
				catch(Exception exp)
				{
					if (OracleTran != null) OracleTran.Rollback();
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
				dtObj = GetDataTable_ByText("sp_Common @ObjName=N'" + strProcedureName + "'");
				OracleTransaction OracleTran;
				OracleConnection cnn = new OracleConnection(ConfigurationSettings.AppSettings["connString"]);			
				cnn.Open();
				OracleTran = cnn.BeginTransaction();			
				OracleCommand cmd = new OracleCommand();
				cmd.Transaction = OracleTran;
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
								cmd.Parameters.Add("@Activity",OracleType.NVarChar,50).Value = strActivity.Trim();
								cmd.Parameters.Add(strParamUserAction,OracleType.NVarChar,10).Value = strUserAction;
								for(int j=0;j<arrID.Length-1;j++)
								{
									DataRow[] iRow = dtObj.Select("columnname ='@" + arrID.GetValue(j).ToString().Trim() + "'");
									if(iRow.Length > 0)
									{
										OracleParameter param = new OracleParameter();
										param.ParameterName = iRow[0]["columnname"].ToString().Trim();
										param.OracleType = ConvertDataType(iRow[0]["datatype"].ToString().Trim());
										param.Size = SafeDataInteger(iRow[0]["size"].ToString().Trim());
										//Value
										string strValue = dtgGrid.Items[i].Cells[j].Text.Trim();

									
										if(param.OracleType==OracleType.Float)
											strValue = strValue.Replace(",","");

										if(strValue.Replace("&nbsp;","").Trim()=="")
											param.Value = DBNull.Value ;
										else
										{
											if(param.OracleType == OracleType.Int16)
												param.Value = (strValue=="True" || strValue=="1")?1:0;
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
								strErrorID += i.ToString() + "@" + exp.Message + "$";
							}
						}
					}
					
					if (OracleTran != null ) OracleTran.Commit();
					if (strErrorID == "")
						return "Successful!";
					else
						return strErrorID;
				}
				catch(Exception exp)
				{
					if (OracleTran != null) OracleTran.Rollback();
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
			/// <param name="strActivity">'Save' or 'Update'</param>
			/// <param name="Page">Page</param>
			/// <param name="strProcedureName">Store Prodedure Name</param>
			/// <returns>string</returns>		
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
				OleDbDataAdapter myCommand = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", strConn);
				DataSet myDataSet = new DataSet();
				myCommand.Fill(myDataSet, "ExcelData");

				DataTable dt = new DataTable();
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
			#endregion
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
			#endregion
		#region Các hàm liên qua đến lưu dữ liệu (ImpactDB,Update) Auto: Chua Kiem tra ben Oracle Tanldt
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
			dtObj = GetDataTable_ByText("sp_Common @ObjName=N'" + strProcedureName + "'");			
			OracleTransaction OracleTran;
			OracleConnection cnn = new OracleConnection(ConfigurationSettings.AppSettings["connString"]);			
			cnn.Open();
			OracleTran = cnn.BeginTransaction();
			OracleCommand cmd = new OracleCommand();
			cmd.Transaction = OracleTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = strProcedureName.Trim();
			//-------
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",OracleType.NVarChar,50).Value = strActivity.Trim();				
				//cmd.Parameters.Add("@EmpID",OracleType.NVarChar,10).Value = strEmpID.Trim();
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
						OracleParameter param = new OracleParameter();
						param.ParameterName = iRow[0]["columnname"].ToString().Trim();
						param.OracleType = ConvertDataType(iRow[0]["datatype"].ToString().Trim());
						param.Size = SafeDataInteger(iRow[0]["size"].ToString().Trim());						
						string strValue = GetValueControl(Page,arrControl.GetValue(i).ToString().Trim());
						if(param.OracleType==OracleType.Float)
							strValue = strValue.Replace(",","");
						if(param.ParameterName.Trim()=="@EmpID")
							param.Value = strEmpID.Trim();
						else{
							if(strValue.Trim()=="")
								param.Value = DBNull.Value ;
							else
							{
								if(param.OracleType == OracleType.Int16)
									param.Value = (strValue=="True" || strValue=="1")?1:0; //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim()).Trim()=="True"?1:0;
								else
									param.Value = strValue; //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim());
							}
						}
						cmd.Parameters.Add(param);
					}
				}
				cmd.ExecuteNonQuery();
				OracleTran.Commit();
				return true;
			}
			catch (Exception exp)
			{
				string strErr = exp.Message;
				OracleTran.Rollback();
				return false;
			}
			finally
			{
				cmd.Dispose();
				OracleTran.Dispose();
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
		public static bool ImpactDB(string strParaEmpID,string strEmpID,string strActivity,Control Page,string strProcedureName)
		{
			DataTable dtObj = new DataTable();
			dtObj = GetDataTable_ByText("sp_Common @ObjName=N'" + strProcedureName + "'");			
			OracleTransaction OracleTran;
			OracleConnection cnn = new OracleConnection(ConfigurationSettings.AppSettings["connString"]);			
			cnn.Open();
			OracleTran = cnn.BeginTransaction();
			OracleCommand cmd = new OracleCommand();
			cmd.Transaction = OracleTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = strProcedureName.Trim();
			//-------
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",OracleType.NVarChar,50).Value = strActivity.Trim();
				cmd.Parameters.Add(strParaEmpID,OracleType.NVarChar,10).Value = strEmpID.Trim();
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
						OracleParameter param = new OracleParameter();
						param.ParameterName = iRow[0]["columnname"].ToString().Trim();
						param.OracleType = ConvertDataType(iRow[0]["datatype"].ToString().Trim());
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
								if(param.OracleType == OracleType.Int16)
									param.Value = strValue=="True"?1:0; //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim()).Trim()=="True"?1:0;
								else
									param.Value = strValue; //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim());
							}
						}
						cmd.Parameters.Add(param);
					}
				}
				cmd.ExecuteNonQuery();
				OracleTran.Commit();
				return true;
			}
			catch (Exception exp)
			{
				string strErr = exp.Message;
				OracleTran.Rollback();
				return false;
			}
			finally
			{
				cmd.Dispose();
				OracleTran.Dispose();
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
			dtObj = GetDataTable_ByText("sp_Common @ObjName=N'" + strProcedureName + "'");			
			OracleTransaction OracleTran;
			OracleConnection cnn = new OracleConnection(ConfigurationSettings.AppSettings["connString"]);			
			cnn.Open();
			OracleTran = cnn.BeginTransaction();
			OracleCommand cmd = new OracleCommand();
			cmd.Transaction = OracleTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = strProcedureName.Trim();
			//-------
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",OracleType.NVarChar,50).Value = strActivity.Trim();				
				cmd.Parameters.Add(strParamUserAction,OracleType.NVarChar,10).Value = strUserAction;
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
						OracleParameter param = new OracleParameter();
						param.ParameterName = iRow[0]["columnname"].ToString().Trim();
						param.OracleType = ConvertDataType(iRow[0]["datatype"].ToString().Trim());
						param.Size = SafeDataInteger(iRow[0]["size"].ToString().Trim());						
						string strValue = GetValueControl(Page,arrControl.GetValue(i).ToString().Trim());
						if(param.OracleType==OracleType.Float || param.OracleType==OracleType.Int16 || param.OracleType==OracleType.Int16
							|| param.OracleType==OracleType.Int16 || param.OracleType==OracleType.Float || param.OracleType==OracleType.Int16)
							strValue = strValue.Replace(",","");
						if(param.ParameterName.Trim()=="@EmpID")
							param.Value = strEmpID.Trim();
						else
						{
							if(strValue.Trim()=="")
								param.Value = DBNull.Value ;
							else
							{
								if(param.OracleType == OracleType.Int16)
									param.Value = (strValue=="True" || strValue=="1")?1:0; //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim()).Trim()=="True"?1:0;
								else
									param.Value = strValue; //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim());
							}
						}
						cmd.Parameters.Add(param);
					}
				}
				cmd.ExecuteNonQuery();
				OracleTran.Commit();
				return true;
			}
			catch (Exception exp)
			{
				string strErr = exp.Message;
				OracleTran.Rollback();
				return false;
			}
			finally
			{
				cmd.Dispose();
				OracleTran.Dispose();
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
				dtObj = GetDataTable_ByText("sp_Common @ObjName=N'" + strProcedureName + "'");			
				OracleTransaction OracleTran;
				OracleConnection cnn = new OracleConnection(ConfigurationSettings.AppSettings["connString"]);			
				cnn.Open();
				OracleTran = cnn.BeginTransaction();
				OracleCommand cmd = new OracleCommand();
				cmd.Transaction = OracleTran;
				cmd.Connection = cnn;
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = strProcedureName.Trim();
				//-------
				try
				{
					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity",OracleType.NVarChar,50).Value = strActivity.Trim();				
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
							OracleParameter param = new OracleParameter();
							param.ParameterName = iRow[0]["columnname"].ToString().Trim();
							param.OracleType = ConvertDataType(iRow[0]["datatype"].ToString().Trim());
							param.Size = SafeDataInteger(iRow[0]["size"].ToString().Trim());
							string strValue = GetValueControl(Page,arrControl.GetValue(i).ToString().Trim());						
							if(strValue.Trim()=="")
								param.Value = DBNull.Value ;
							else
							{
								if(param.OracleType == OracleType.Int16)
									param.Value = strValue=="True"?1:0; //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim()).Trim()=="True"?1:0;
								else
									param.Value = strValue; //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim());
							}
							cmd.Parameters.Add(param);
						}
					}
					cmd.ExecuteNonQuery();
					OracleTran.Commit();
					return true;
				}
				catch (Exception exp)
				{
					string strErr = exp.Message;
					OracleTran.Rollback();
					return false;
				}
				finally
				{
					cmd.Dispose();
					OracleTran.Dispose();
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
			dtObj = GetDataTable_ByText("sp_Common @ObjName=N'" + strProcedureName + "'");
			OracleTransaction OracleTran;
			OracleConnection cnn = new OracleConnection(ConfigurationSettings.AppSettings["connString"]);			
			cnn.Open();
			OracleTran = cnn.BeginTransaction();
			OracleCommand cmd = new OracleCommand();
			cmd.Transaction = OracleTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = strProcedureName.Trim();
			//-------
			try
			{				
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",OracleType.NVarChar,50).Value = strActivity.Trim();				
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
						OracleParameter param = new OracleParameter();
						param.ParameterName = iRow[0]["columnname"].ToString().Trim();
						param.OracleType = ConvertDataType(iRow[0]["datatype"].ToString().Trim());
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
								if(param.OracleType == OracleType.Int16)
									param.Value = (strValue=="True" || strValue=="1")?1:0; //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim()).Trim()=="1"?true:false;
								else
									param.Value = strValue; //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim());
							}
						}
						cmd.Parameters.Add(param);
					}
				}
				cmd.ExecuteNonQuery();
				OracleTran.Commit();
				return true;
			}
			catch (Exception exp)
			{
				string strErr = exp.Message;
				OracleTran.Rollback();
				return false;
			}
			finally
			{
				cmd.Dispose();
				OracleTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();
			}
		}
		//UPDATE <CO LUU LAI USER EDIT>
		public static bool UpdateByKey(string strParamUserAction,Object strUserAction,string strKeyID,string strKeyValue,string strActivity,Control Page,string strProcedureName)
		{
			DataTable dtObj = new DataTable();
			dtObj = GetDataTable_ByText("sp_Common @ObjName=N'" + strProcedureName + "'");
			OracleTransaction OracleTran;
			OracleConnection cnn = new OracleConnection(ConfigurationSettings.AppSettings["connString"]);			
			cnn.Open();
			OracleTran = cnn.BeginTransaction();
			OracleCommand cmd = new OracleCommand();
			cmd.Transaction = OracleTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = strProcedureName.Trim();
			//-------
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",OracleType.NVarChar,50).Value = strActivity.Trim();				
				cmd.Parameters.Add(strParamUserAction,OracleType.NVarChar,10).Value = strUserAction;
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
						OracleParameter param = new OracleParameter();
						param.ParameterName = iRow[0]["columnname"].ToString().Trim();
						param.OracleType = ConvertDataType(iRow[0]["datatype"].ToString().Trim());
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
								if(param.OracleType == OracleType.Int16)
									param.Value = (strValue=="True" || strValue=="1")?1:0; //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim()).Trim()=="1"?true:false;
								else
									param.Value = strValue; //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim());
							}
						}
						cmd.Parameters.Add(param);
					}
				}
				cmd.ExecuteNonQuery();
				OracleTran.Commit();
				return true;
			}
			catch (Exception exp)
			{
				string strErr = exp.Message;
				OracleTran.Rollback();
				return false;
			}
			finally
			{
				cmd.Dispose();
				OracleTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();
			}
		}
		public static bool DeleteListRecord(string  strStoreName,string strKeyField,OracleType pType, int intSize, string strListID)
		{
			OracleTransaction OracleTran;
			OracleConnection cnn = new OracleConnection(ConfigurationSettings.AppSettings["connString"]);			
			cnn.Open();
			OracleTran = cnn.BeginTransaction();
			OracleCommand cmd = new OracleCommand();
			cmd.Transaction = OracleTran;
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
					cmd.Parameters.Add("@Activity",OracleType.NVarChar,50).Value = "Delete";
					OracleParameter param = new OracleParameter();
					param.ParameterName = "@" + strKeyField.Trim();
					param.OracleType = pType;
					param.Size = intSize;				
					param.Value = arrID.GetValue(i).ToString().Trim();
					cmd.Parameters.Add(param);
					cmd.ExecuteNonQuery();
				}				
				OracleTran.Commit();
				return true;
			}
			catch (Exception exp)
			{
				string strErr = exp.Message;
				OracleTran.Rollback();
				return false;
			}
			finally
			{
				cmd.Dispose();
				OracleTran.Dispose();
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
		public static bool DeleteListRecord(string  strStoreName,string strActivity,string strKeyField,OracleType pType, int intSize, string strListID)
		{
			OracleTransaction OracleTran;
			OracleConnection cnn = new OracleConnection(ConfigurationSettings.AppSettings["connString"]);			
			cnn.Open();
			OracleTran = cnn.BeginTransaction();
			OracleCommand cmd = new OracleCommand();
			cmd.Transaction = OracleTran;
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
					cmd.Parameters.Add("@Activity",OracleType.NVarChar,50).Value = strActivity.Trim();
					OracleParameter param = new OracleParameter();
					param.ParameterName = "@" + strKeyField.Trim();
					param.OracleType = pType;
					param.Size = intSize;				
					param.Value = arrID.GetValue(i).ToString().Trim();
					cmd.Parameters.Add(param);
					cmd.ExecuteNonQuery();
				}				
				OracleTran.Commit();
				return true;
			}
			catch (Exception exp)
			{
				string strErr = exp.Message;
				OracleTran.Rollback();
				return false;
			}
			finally
			{
				cmd.Dispose();
				OracleTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();
			}			
		}
		#endregion
		#region Các hàm về Control
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
						//case "MyDropDownListControl":
						//	strReturnValue=((MyDropDownListControl)ctrFound).SelectedValue.Trim();
						//	break;
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
					{						
						string mCtlType = Child_ctl.GetType().ToString().Trim().Substring(Child_ctl.GetType().ToString().Trim().LastIndexOf(".")+1).ToString().Trim();
						if((mCtlType.ToUpper() == "TEXTBOX" ||mCtlType.ToUpper() == "CHECKBOX" || mCtlType.ToUpper() == "RADIOBUTTONLIST" ||mCtlType.ToUpper() == "DROPDOWNLIST" || mCtlType.ToUpper() == "RADIOBUTTON" || mCtlType.ToUpper()=="MYDROPDOWNLISTCONTROL" ) && (Child_ctl.ID.Trim().ToUpper()!="CHKSELECT"))
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
								if(Child_ctl.ID.Trim().ToUpper()!="CHKSHOWGRID" && Child_ctl.ID.Trim().ToUpper()!="CHKSELECT" && Child_ctl.ID.Trim().ToUpper()!="TXTEMPID" && Child_ctl.ID.Trim().ToUpper()!="TXTEMPNAME")
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
		#endregion
		#region Hàm liên quan đến Combo
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
				dt = GetDataTable_ByText(pCommandText);
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
				DataTable dt=GetDataTable_ByText(pCommandText);
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
				dt = GetDataTable_ByText(pCommandText);
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

		public static void LoadDropDownList(DropDownList pCtrl, string pstrSQL, string pstrValueField, string pstrTextField)
		{
			try
			{
				DataTable rsData = new DataTable();
				rsData = GetDataTable_ByText(pstrSQL);

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

		#endregion
		#region Các ham khác
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
			/// tra ve gia tri string cua fieldname duoc dua vao, neu khong co se tra ve chuoi rong
			/// creater : Tanldt
			/// CreateDate : 2006 03 16
			/// </summary>
			/// <param name="pSQLCommand">CommandText string </param>
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
				catch( OracleException Ex)
				{
					return "";
				}
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

			/// <summary>
			/// Tanldt
			/// Ngay 2006 03 16
			/// </summary>
			/// <param name="val">Chuoi truyen vao</param>
			/// <returns>OracleType</returns>
		
			private static OracleType ReturnType(string val)
			{
				switch(val.ToLower())
				{
					case "bfile" : return OracleType.BFile;
					case "blob" : return OracleType.Blob;
					case "byte" : return OracleType.Byte;
					case "char" : return OracleType.Char;
					case "datetime" : return OracleType.DateTime;
					case "clob" : return OracleType.Clob;
					case "float" : return OracleType.Float;
					case "cursor" : return OracleType.Cursor;
					case "int16" : return OracleType.Int16;
					case "int32" : return OracleType.Int32;
					case "double" : return OracleType.Double;
					case "intervaldaytosecond" : return OracleType.IntervalDayToSecond;
					case "nchar" : return OracleType.NChar;
					case "nclob" : return OracleType.NClob;
					case "nvarchar" : return OracleType.NVarChar;
					case "Raw" : return OracleType.Raw;
					case "intervalyeartomonth" : return OracleType.IntervalYearToMonth;
					case "longraw" : return OracleType.LongRaw;
					case "longvarchar" : return OracleType.LongVarChar;
					case "number" : return OracleType.Number;
					case "rowid" : return OracleType.RowId;
					case "sbyte" : return OracleType.SByte;
					case "timestamp" : return OracleType.Timestamp;
					case "timestamplocal" : return OracleType.TimestampLocal;
					case "timestampwithtz" : return OracleType.TimestampWithTZ;
					case "uint16" : return OracleType.UInt16;
					case "uint32" : return OracleType.UInt32;
					case "varchar" : return OracleType.VarChar;
				}return OracleType.VarChar;
			}
			/// <summary>
			/// 
			/// </summary>
			/// <param name="strTypeName"></param>
			/// <returns></returns>
			private static OracleType ConvertDataType(string strTypeName)
			{
				OracleType TypeReturn = new OracleType();
				switch(strTypeName.Trim().ToUpper())
				{
					case "NVARCHAR":
						TypeReturn = OracleType.NVarChar;
						break;
					case "DATETIME":
						TypeReturn = OracleType.NVarChar;
						break;
					case "VARCHAR":
						TypeReturn = OracleType.VarChar;
						break;
					case "BIT":
						TypeReturn = OracleType.Int16;
						break;
					case "INT":
						TypeReturn = OracleType.Int16;
						break;
					case "MONEY":
						TypeReturn = OracleType.Int16;
						break;
					case "REAL":
						TypeReturn = OracleType.Float;
						break;
					case "NUMERIC":
						TypeReturn = OracleType.Float;
						break;
					case "SMALLINT":
						TypeReturn = OracleType.Int16;
						break;
					default:
						TypeReturn = OracleType.NVarChar;
						break;
				}
				return TypeReturn;
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
					dt = GetDataTable_ByText(pCommandText);
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

			#endregion
	}
}