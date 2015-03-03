/*
 * Tính năng nâng cấp:
 * thay đổi thiết kế Template theo id như sau:
 * itempheadlv1 ==>itempheadlv2 ==> irow ==> itempsumlv2 ==> itempsumlv1 ==> itempsumtotal ==> ifooter
 * 
 * Tính năng nâng cấp thứ 2: Cho phép Sum để ở trên GroupName Thông qua thuộc tính IsSumGroupHeader = (true,false)
 * Tính năng nâng cấp thứ 3: Cho phép dạng Page
 * */
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Configuration;
using FPTToolWeb.Data;

namespace iHRPCore.Reports.v12
{
	/// <summary>
	/// Summary description for clsBaocaoExcel.
	/// </summary>
	public class clsBaocaoExcel
	{
		#region Phần khai báo biến
		#region Private Variables
		public bool _AutoSort = true;
		public bool _IsSum = false;
		public bool _IsSum1 = false;
		public bool _IsSum2 = false;
		public bool _IsGroupLv1 = false;
		public bool _IsSumGroupHeader1 = false;
		public bool _IsSumGroupHeader2 = false;
		public int _GroupLv1 = 1;
		public bool _IsGroupLv2 = false;
		public int _GroupLv2 = 2;
		public string strTempHeadLv1 = "";
		public string strTempHeadLv2 = "";
		public string strTempSumLv1 = "";
		public string strTempSumLv2 = "";
		public string strTempSumtotal = "";
		public string _sfileTemplate = "";
		public string _sHeaderParams = ""; 
		public string _sHeaderValues = ""; 
		public string _sFooterParams = ""; 
		public string _sFooterValues = ""; 
		public string _SortDetail = "";

		public DataTable _dtSub = null; 
		public string _linkSub = ""; 
		public string idirow = "irow";
		public string idifooter = "ifooter";
		public string itempheadlv1 = "itempheadlv1";
		public string itempheadlv2 = "itempheadlv2";
		public string itempsumlv1 = "itempsumlv1";
		public string itempsumlv2 = "itempsumlv2";
		public string itempsumtotal = "itempsumtotal";
		#endregion
		#region Get/Set Variables
		public bool AutoSort
		{
			get { return _AutoSort; }
			set { _AutoSort = value; }
		}
		public string SortDetail
		{
			get { return _SortDetail; }
			set { _SortDetail = value; }
		}
		public bool IsSum
		{
			get { return _IsSum; }
			set { _IsSum = value; }
		}
		public bool IsSum1
		{
			get { return _IsSum1; }
			set { _IsSum1 = value; }
		}
		public bool IsSum2
		{
			get { return _IsSum2; }
			set { _IsSum2 = value; }
		}
		public bool IsSumGroupHeader1
		{
			get { return _IsSumGroupHeader1; }
			set { _IsSumGroupHeader1 = value; }
		}
		public bool IsSumGroupHeader2
		{
			get { return _IsSumGroupHeader2; }
			set { _IsSumGroupHeader2 = value; }
		}
		public bool IsGroupLv1
		{
			get { return _IsGroupLv1; }
			set { _IsGroupLv1 = value; }
		}
		public bool IsGroupLv2
		{
			get { return _IsGroupLv2; }
			set { _IsGroupLv2 = value; }
		}
		public int GroupLv1
		{
			get { return _GroupLv1; }
			set { _GroupLv1 = value; }
		}
		public int GroupLv2
		{
			get { return _GroupLv2; }
			set { _GroupLv2 = value; }
		}
		public string sfileTemplate
		{
			get { return _sfileTemplate; }
			set { _sfileTemplate = value; }
		}
		public string sHeaderParams
		{
			get { return _sHeaderParams; }
			set { _sHeaderParams = value; }
		}
		public string sHeaderValues
		{
			get { return _sHeaderValues; }
			set { _sHeaderValues = value; }
		}
		public string sFooterParams
		{
			get { return _sFooterParams; }
			set { _sFooterParams = value; }
		}
		public string sFooterValues
		{
			get { return _sFooterValues; }
			set { _sFooterValues = value; }
		}

		public DataTable dtSub
		{
			get { return _dtSub; }
			set { _dtSub = value; }
		}
		public string linkSub
		{
			get { return _linkSub; }
			set { _linkSub = value; }
		}
		#endregion
		#endregion
		#region phần chính của tool
		#region clsBaocaoExcel
		public clsBaocaoExcel()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#endregion
		#region ReadTemplate
		static public string ReadTemplate(string name) 
		{
			string file;
			try
			{
				string appPath = ConfigurationSettings.AppSettings["pStrUploadReport"].ToString();
				if(HttpContext.Current.Cache[name] != null && false) 
				{
					file = HttpContext.Current.Cache[name].ToString();
				} 
				else 
				{
					string templatefile = HttpContext.Current.Server.MapPath(String.Format("{0}{1}",
						appPath,name));
					StreamReader sr = new StreamReader(templatefile,Encoding.ASCII);
					file = sr.ReadToEnd();
					sr.Close();
					HttpContext.Current.Cache[name] = file;
				}
			}
			catch(Exception ex)
			{
				file = ex.Message;
			}
			return file;
		}
		#endregion
		#region getFromFile
		private int getFromFile(string FullPath)
		{
			if (!File.Exists(FullPath)) return 0;
			int retval=1;
			try
			{
				FileStream fs=new FileStream(FullPath,FileMode.Open);
				int len=(int)fs.Length;
				byte[] buffer =new byte[len];
				fs.Read(buffer,0,len);
				fs.Close();
				StringBuilder sb=new StringBuilder();
				for (int t=0; t<len; t++) sb.Append((char)buffer[t]);
				retval=Int32.Parse(sb.ToString());
			}
			catch(Exception) { }

			return retval;
		}
		
		#endregion
		#region SumColumn
		public double SumColumn(DataTable dt,string ColumnName)
		{
			double Sum=0.0;
			foreach(DataRow dr in dt.Rows)
			{
				try
				{
					Sum+=System.Convert.ToDouble(dr[ColumnName]);
				}
				catch
				{
					Sum+= 0;
				}
			}
			return Sum;
		}
		#endregion
		#region Sort DataTable
		private DataTable sortTable(DataTable table)
		{
			DataTable t = null;
			t = table.Copy();
			if (SortDetail != "")
				t.DefaultView.Sort= SortDetail+" asc";
			else
                t.DefaultView.Sort=t.Columns[0].ColumnName+" asc";
			return t;
		}
		#endregion
		#region HTMLDataTablePage oDataTable
		public string HTMLDataTablePage(DataTable oDataTable, string strTpl)
		{
			oDataTable = sortTable(oDataTable);
			string strHTML = "";
			int i = 1;
			strHTML = strTpl;
			foreach(DataRow oDataRow in oDataTable.Rows)	
			{
				foreach(DataColumn oDataColumn in oDataTable.Columns)	
				{
					try
					{
						strHTML =  strHTML.Replace("$"+oDataColumn.ColumnName+"$", oDataRow[oDataColumn.ColumnName].ToString());
						strHTML =  strHTML.Replace("IsNum=1", "x:num");
					}
					catch{ strHTML =  strHTML ;}
				}

				i++;
			}
			return strHTML;
		}
		#endregion
		#region HTMLDataTable oDataTable
		public string HTMLDataTable(DataTable oDataTable, string strTpl)
		{
			oDataTable = sortTable(oDataTable);
			string strHTML = "";
			int i = 1;
			foreach(DataRow oDataRow in oDataTable.Rows)	
			{
				strHTML += strTpl;
				try
				{
					strHTML =  strHTML.Replace("$No$", i.ToString());
				}
				catch{ strHTML = strHTML;}

				foreach(DataColumn oDataColumn in oDataTable.Columns)	
				{
					try
					{
						strHTML =  strHTML.Replace("$"+oDataColumn.ColumnName+"$", oDataRow[oDataColumn.ColumnName].ToString());
						strHTML =  strHTML.Replace("IsNum=1", "x:num");
					}
					catch{ strHTML =  strHTML ;}
				}

				i++;
			}
			return strHTML;
		}
		#endregion
		#region HTMLDataTable_SUB oDataTable, isSum
		public string HTMLDataTable_SUB(DataTable oDataTable, string strTpl)
		{
			string strHTML = "";
			int i = 1;
			foreach(DataRow oDataRow in oDataTable.Rows)	
			{
				DataRow[] Rows = dtSub.Select(linkSub + " = '"+oDataRow[linkSub].ToString()+"'", "");
				int nn = 1;
				#region Neu truong hon Sub khong co' du lieu
				if (Rows.Length == 0)
				{
					strHTML += strTpl;
					try
					{
						strHTML =  strHTML.Replace("$No$", i.ToString());
					}
					catch{ strHTML = strHTML;}

					foreach(DataColumn oDataColumn in oDataTable.Columns)	
					{
						try
						{
							strHTML =  strHTML.Replace("$"+oDataColumn.ColumnName+"$", oDataRow[oDataColumn.ColumnName].ToString());
						}
						catch{ strHTML =  strHTML ;}
					}
					foreach(DataColumn oDataColumnSub in dtSub.Columns)	
					{
						try
						{
							strHTML =  strHTML.Replace("$sub"+oDataColumnSub.ColumnName+"$", "");
						}
						catch{ strHTML =  strHTML ;}
					}
				}
				#endregion
				foreach(DataRow oDataRowSub in Rows)	
				{
					strHTML += strTpl;
					if (nn == 1)
					{
						try
						{
							strHTML =  strHTML.Replace("$No$", i.ToString());
						}
						catch{ strHTML = strHTML;}

						foreach(DataColumn oDataColumn in oDataTable.Columns)	
						{
							try
							{
								strHTML =  strHTML.Replace("$"+oDataColumn.ColumnName+"$", oDataRow[oDataColumn.ColumnName].ToString());
							}
							catch{ strHTML =  strHTML ;}
						}
					}
					else
					{
						try
						{
							strHTML =  strHTML.Replace("$No$", "");
						}
						catch{ strHTML = strHTML;}

						foreach(DataColumn oDataColumn in oDataTable.Columns)	
						{
							try
							{
								strHTML =  strHTML.Replace("$"+oDataColumn.ColumnName+"$", "");
							}
							catch{ strHTML =  strHTML ;}
						}
					}
					foreach(DataColumn oDataColumnSub in dtSub.Columns)	
					{
						try
						{
							strHTML =  strHTML.Replace("$sub"+oDataColumnSub.ColumnName+"$", oDataRowSub[oDataColumnSub.ColumnName].ToString());
						}
						catch{ strHTML =  strHTML ;}
					}
					nn++;
				}
				nn = 1;
				i++;
			}
			if (IsSum)
			{
				if (strTempSumLv2 != "")
				{
					//strHTML +=  ("<TR>");
					string strTempSum = strTempSumLv2;
					foreach(DataColumn oDataColumn in oDataTable.Columns)	
					{
						double dbi = 0;
						dbi = SumColumn(oDataTable,oDataColumn.ColumnName);
						if (dbi != 0)
						{
							try
							{
								//strTempSum =  strTempSum.Replace("x:str", "x:num");
								strTempSum =  strTempSum.Replace("$"+oDataColumn.ColumnName+"$", dbi.ToString());
								//strTempSum =  strTempSum.Replace("IsNum=1", "x:num='"+dbi.ToString()+"'");
								strTempSum =  strTempSum.Replace("IsNum=1", "x:num");
							}
							catch{ strTempSum =  strTempSum;}
						}
						else
							strTempSum =  strTempSum.Replace("$"+oDataColumn.ColumnName+"$", "");
					}
					//strHTML =  strHTML + strTempSumLv2 + ("</TR>");
					strHTML += strTempSum;
				}
			}
			return strHTML;
		}
		#endregion
		#region HTMLDataTable
		public string HTMLDataTable(DataTable oDataTable)
		{
			string strHTML = "";
			int i = 1;
			strHTML += ("<table border=1><TR>");
			foreach(DataColumn oDataColumn in oDataTable.Columns)	
			{
				strHTML += ("<TD>" + oDataColumn.ColumnName + "</TD>");
			}
			
			strHTML += ("</TR>");

			foreach(DataRow oDataRow in oDataTable.Rows)	
			{
				strHTML +=  ("<TR>");
				foreach(DataColumn oDataColumn in oDataTable.Columns)	
				{
					strHTML +=  ("<TD>&nbsp;" + oDataRow[oDataColumn.ColumnName] + "</TD>");
				}
				strHTML +=  ("</TR>");
			}
			strHTML +=  ("</table>");
			
			return strHTML;
		}
		#endregion
		#region AddDataHTMLCapHeader
		private string AddDataHTMLCapHeader(DataTable oDataTable, string strTpl)
		{
			string strHTML = "";
			int iGroupLevel1 = GroupLv1 - 1;
			int iGroupLevel2 = GroupLv2 - 1;

			#region khai bao
			int k = 0;
			DataSet ds1 = new DataSet();
			DataSet ds2 = new DataSet();
			DataSet ds;
			DataSetHelper dsHelper;

			// Export the details of specified columns
			ds = new DataSet();
			dsHelper = new DataSetHelper(ref ds);
			// Getting Field Names
			string sColumnName1 = "";
			string sColumnName2 = "";
			
			/********************************************************
			 * Start, check for border width
			 * ******************************************************/
			int borderWidth = 0;
			borderWidth = 1;
			
			/********************************************************
			 * End, Check for border width
			 * ******************************************************/

			/********************************************************
			 * Start, Check for bold heading
			 * ******************************************************/
			string boldTagStart = "";
			string boldTagEnd = "";
			
			boldTagStart = "<B>";
			boldTagEnd = "</B>";
			#endregion
			#region Creating table header
			/*******************************************************************
			 * Start, Creating table header
			 * *****************************************************************/

			
			//strHTML +=  ("<TR>");
			//strHTML +=  ("<TD bgcolor=#FFFF99>"+ boldTagStart +"No."+ boldTagEnd +"</TD>");
			foreach(DataColumn oDataColumn in oDataTable.Columns)	
			{
				if((iGroupLevel1 < 0) || (iGroupLevel1 >= oDataTable.Columns.Count))// Check
					throw new Exception("ExportColumn Number should not exceed Total Columns Range");

				if (k == iGroupLevel1) // Neu nam trong List Group
				{
					sColumnName1 = oDataColumn.ColumnName;
				}
				else if (k == iGroupLevel2) // Neu nam trong List Group
				{
					sColumnName2 = oDataColumn.ColumnName;
				}
				else
				{
					//sColumnName2 = oDataColumn.ColumnName;
				}
				k++;
			}
			k = 0;
			//strHTML +=  ("</TR>");

			/*******************************************************************
			 * End, Creating table header
			 * *****************************************************************/
			#endregion
			if (IsGroupLv1)
			{
				#region Cap 1
				DataTable dtb =  SQLOps.SelectDistinct(oDataTable, sColumnName1);// Lay ra cac String trong Group
				if (AutoSort == true)
                    dtb = SQLOps.Distinct_Auto(dtb);
				else
					dtb = SQLOps.Distinct(dtb);
				foreach(DataRow oDataRow in dtb.Rows)	
				{
					DataTable dtcap1 = CreateGroupByTable(oDataRow[0].ToString(),oDataTable,oDataRow[0].ToString(),sColumnName1);
					#region Header 1
					if (strTempHeadLv1 == "")
					{
						strHTML +=  ("<TR>");
						strHTML +=  (@"<TD style='border-top:.5pt solid windowtext;border-right:.5pt solid windowtext;border-bottom:.5pt solid windowtext;border-left:.5pt solid windowtext;'" 
							+ @" bgcolor=#9A9999 align=left colspan="+(dtcap1.Columns.Count)+">&nbsp;<b>" +oDataRow[0].ToString()+ "</b></TD>");
						strHTML +=  ("</TR>");
					}
					else
					{
						//strHTML += strTempHeadLv1.Replace("$GroupName1$",oDataRow[0].ToString());
						#region Sum GroupHeader 1 Total
						if (IsSumGroupHeader1)
						{
							if (strTempHeadLv1 != "")
							{
								string strTempSum = strTempHeadLv1;
								foreach(DataColumn oDataColumn in dtcap1.Columns)	
								{
									double dbi = 0;
									dbi = SumColumn(dtcap1,oDataColumn.ColumnName);
									if (dbi != 0)
									{
										try
										{
											//strTempSum =  strTempSum.Replace("x:str", "x:num");
											strTempSum =  strTempSum.Replace("$"+oDataColumn.ColumnName+"$", dbi.ToString());
											strTempSum =  strTempSum.Replace("IsNum=1", "x:num");
											//strTempSum =  strTempSum.Replace("IsNum=1", "x:num='"+dbi.ToString()+"'");
										}
										catch{ strTempSum =  strTempSum;}
									}
									else
										strTempSum =  strTempSum.Replace("$"+oDataColumn.ColumnName+"$", "");
								}
								strTempSum = strTempSum.Replace("$GroupName1$", oDataRow[0].ToString());
								strHTML += strTempSum;
							}
						}
						else
						{
							strHTML += strTempHeadLv1.Replace("$GroupName1$",oDataRow[0].ToString());
						}
						#endregion
					}
					#endregion
					//Co group cap 2 khong?
					if (IsGroupLv2)
					{
						#region Cap 2
						DataTable dtb2 =  SQLOps.SelectDistinct(dtcap1,sColumnName2);
						//string a = HTMLDataTable(dtb2);
						if (AutoSort == true)
							dtb2 = SQLOps.Distinct_Auto(dtb2);
						else
							dtb2 = SQLOps.Distinct(dtb2);
						//string b = HTMLDataTable(dtb2);
						foreach(DataRow oDataRow2 in dtb2.Rows)	
						{
							DataTable dtcap2 = CreateGroupByTable(oDataRow2[0].ToString(),dtcap1,oDataRow2[0].ToString(),sColumnName2);
							#region Header Group 2
							if (strTempHeadLv2 == "")
							{
								strHTML +=  ("<TR>");
								strHTML +=  (@"<TD style='border-top:.5pt solid windowtext;border-right:.5pt solid windowtext;border-bottom:.5pt solid windowtext;border-left:.5pt solid windowtext;'" 
									+ @" bgcolor=#B9B8B8 align=left colspan="+(dtcap2.Columns.Count+1)+">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>" +oDataRow2[0].ToString()+ "</b></TD>");
								strHTML +=  ("</TR>");
							}
							else
							{
								//strHTML += strTempHeadLv2.Replace("$GroupName2$",oDataRow2[0].ToString());
								#region Sum GroupHeader 1 Total
								if (IsSumGroupHeader2)
								{
									if (strTempHeadLv2 != "")
									{
										string strTempSum = strTempHeadLv2;
										foreach(DataColumn oDataColumn in dtcap2.Columns)	
										{
											double dbi = 0;
											dbi = SumColumn(dtcap2,oDataColumn.ColumnName);
											if (dbi != 0)
											{
												try
												{
													//strTempSum =  strTempSum.Replace("x:str", "x:num");
													strTempSum =  strTempSum.Replace("$"+oDataColumn.ColumnName+"$", dbi.ToString());
													//strTempSum =  strTempSum.Replace("IsNum=1", "x:num='"+dbi.ToString()+"'");
													strTempSum =  strTempSum.Replace("IsNum=1", "x:num");
												}
												catch{ strTempSum =  strTempSum;}
											}
											else
												strTempSum =  strTempSum.Replace("$"+oDataColumn.ColumnName+"$", "");
										}
										strTempSum = strTempSum.Replace("$GroupName2$",oDataRow2[0].ToString());
										strHTML += strTempSum;
									}
								}
								else
								{
									strHTML += strTempHeadLv2.Replace("$GroupName2$",oDataRow2[0].ToString());
								}
								#endregion
							}
							#endregion
							#region Creating rows
							/*******************************************************************
								* Start, Creating rows
								* *****************************************************************/

							strHTML +=  HTMLDataTable(dtcap2, strTpl);
							/*******************************************************************
								* End, Creating rows
								* *****************************************************************/
							#endregion
							#region Sum2 Total
							if (IsSum2)
							{
								if (strTempSumLv2 != "")
								{
									string strTempSum = strTempSumLv2;
									foreach(DataColumn oDataColumn in dtcap2.Columns)	
									{
										double dbi = 0;
										dbi = SumColumn(dtcap2,oDataColumn.ColumnName);
										if (dbi != 0)
										{
											try
											{
												//strTempSum =  strTempSum.Replace("x:str", "x:num");
												strTempSum =  strTempSum.Replace("$"+oDataColumn.ColumnName+"$", dbi.ToString());
												//strTempSum =  strTempSum.Replace("IsNum=1", "x:num='"+dbi.ToString()+"'");
												strTempSum =  strTempSum.Replace("IsNum=1", "x:num");
											}
											catch{ strTempSum =  strTempSum;}
										}
										else
											strTempSum =  strTempSum.Replace("$"+oDataColumn.ColumnName+"$", "");
									}
									strHTML += strTempSum;
								}
							}
							#endregion
							dtcap2.Dispose();
						}
						#endregion
					}
					else
					{
						#region khong group cap 2
						/*******************************************************************
								* Start, Creating rows
								* *****************************************************************/

						strHTML +=  HTMLDataTable(dtcap1, strTpl);
						/*******************************************************************
								* End, Creating rows
								* *****************************************************************/
						#endregion
					}
					#region Sum1 Total
					if (IsSum1)
					{
						if (strTempSumLv1 != "")
						{
							string strTempSum = strTempSumLv1;
							foreach(DataColumn oDataColumn in dtcap1.Columns)	
							{
								double dbi = 0;
								dbi = SumColumn(dtcap1,oDataColumn.ColumnName);
								if (dbi != 0)
								{
									try
									{
										//strTempSum =  strTempSum.Replace("x:str", "x:num");
										strTempSum =  strTempSum.Replace("$"+oDataColumn.ColumnName+"$", dbi.ToString());
										strTempSum =  strTempSum.Replace("IsNum=1", "x:num");
										//strTempSum =  strTempSum.Replace("IsNum=1", "x:num='"+dbi.ToString()+"'");
									}
									catch{ strTempSum =  strTempSum;}
								}
								else
									strTempSum =  strTempSum.Replace("$"+oDataColumn.ColumnName+"$", "");
							}
							strHTML += strTempSum;
						}
					}
					#endregion
					dtcap1.Dispose();
				}
				#endregion
			}
			else
			{
				#region Khong Group cap 1
				/*******************************************************************
							* Start, Creating rows
							* *****************************************************************/

				strHTML +=  HTMLDataTable(oDataTable, strTpl);
				/*******************************************************************
							* End, Creating rows
							* *****************************************************************/
				#endregion
			}
			
			#region Sum Total
			if (IsSum)
			{
				if (strTempSumtotal != "")
				{
					//strHTML +=  ("<TR>");
					string strTempSum = strTempSumtotal;
					foreach(DataColumn oDataColumn in oDataTable.Columns)	
					{
						double dbi = 0;
						dbi = SumColumn(oDataTable,oDataColumn.ColumnName);
						if (dbi != 0)
						{
							try
							{
								//strTempSum =  strTempSum.Replace("x:str", "x:num");
								strTempSum =  strTempSum.Replace("$"+oDataColumn.ColumnName+"$", dbi.ToString());
								strTempSum =  strTempSum.Replace("IsNum=1", "x:num");
								//strTempSum =  strTempSum.Replace("IsNum=1", "x:num='"+dbi.ToString()+"'");
							}
							catch{ strTempSum =  strTempSum;}
						}
						else
							strTempSum =  strTempSum.Replace("$"+oDataColumn.ColumnName+"$", "");
					}
					strHTML += strTempSum;
				}
			}
			#endregion
			return strHTML;
		}
		#endregion
		#region AddDataHTMLCapHeader_SUB
		private string AddDataHTMLCapHeader_SUB(DataTable oDataTable, string strTpl)
		{
			string strHTML = "";
			int iGroupLevel1 = (GroupLv1 - 1);
			int iGroupLevel2 = (GroupLv2 - 1);
			
			#region khai bao
			int k = 0;
			DataSet ds1 = new DataSet();
			DataSet ds2 = new DataSet();
			DataSet ds;
			DataSetHelper dsHelper;

			// Export the details of specified columns
			ds = new DataSet();
			dsHelper = new DataSetHelper(ref ds);
			// Getting Field Names
			string sColumnName1 = "";
			string sColumnName2 = "";
			
			/********************************************************
			 * Start, check for border width
			 * ******************************************************/
			int borderWidth = 0;
			borderWidth = 1;
			
			/********************************************************
			 * End, Check for border width
			 * ******************************************************/

			/********************************************************
			 * Start, Check for bold heading
			 * ******************************************************/
			string boldTagStart = "";
			string boldTagEnd = "";
			
			boldTagStart = "<B>";
			boldTagEnd = "</B>";
			#endregion
			#region Creating table header
			/*******************************************************************
			 * Start, Creating table header
			 * *****************************************************************/

			
			//strHTML +=  ("<TR>");
			//strHTML +=  ("<TD bgcolor=#FFFF99>"+ boldTagStart +"No."+ boldTagEnd +"</TD>");
			foreach(DataColumn oDataColumn in oDataTable.Columns)	
			{
				if((iGroupLevel1 < 0) || (iGroupLevel1 >= oDataTable.Columns.Count))// Check
					throw new Exception("ExportColumn Number should not exceed Total Columns Range");

				if (k == iGroupLevel1) // Neu nam trong List Group
				{
					sColumnName1 = oDataColumn.ColumnName;
				}
				else if (k == iGroupLevel2) // Neu nam trong List Group
				{
					sColumnName2 = oDataColumn.ColumnName;
				}
				else
				{
					//strHTML +=  ("<TD bgcolor=#FFFF99>" + boldTagStart + oDataColumn.ColumnName + boldTagEnd + "</TD>");
				}
				k++;
			}
			k = 0;
			//strHTML +=  ("</TR>");

			/*******************************************************************
			 * End, Creating table header
			 * *****************************************************************/
			#endregion
			#region Cap 1
			DataTable dtb =  SQLOps.SelectDistinct(oDataTable,sColumnName1);// Lay ra cac String trong Group
			dtb = SQLOps.Distinct(dtb);
			foreach(DataRow oDataRow in dtb.Rows)	
			{
				DataTable dtcap1 = CreateGroupByTable(oDataRow[0].ToString(),oDataTable,oDataRow[0].ToString(),sColumnName1);

				if (strTempHeadLv1 == "")
				{
					strHTML +=  ("<TR>");
					strHTML +=  (@"<TD style='border-top:.5pt solid windowtext;border-right:.5pt solid windowtext;border-bottom:.5pt solid windowtext;border-left:.5pt solid windowtext;'" 
						+ @" bgcolor=#9A9999 align=left colspan="+(dtcap1.Columns.Count)+">&nbsp;<b>" +oDataRow[0].ToString()+ "</b></TD>");
					strHTML +=  ("</TR>");
				}
				else
				{
					strHTML += strTempHeadLv1.Replace("$GroupName1$",oDataRow[0].ToString());
				}
				#region Cap 2
				DataTable dtb2 =  SQLOps.SelectDistinct(dtcap1,sColumnName2);
				//string a = HTMLDataTable(dtb2);
				dtb2 = SQLOps.Distinct(dtb2);
				//string b = HTMLDataTable(dtb2);
				foreach(DataRow oDataRow2 in dtb2.Rows)	
				{
					DataTable dtcap2 = CreateGroupByTable(oDataRow2[0].ToString(),dtcap1,oDataRow2[0].ToString(),sColumnName2);
					if (strTempHeadLv2 == "")
					{
						strHTML +=  ("<TR>");
						strHTML +=  (@"<TD style='border-top:.5pt solid windowtext;border-right:.5pt solid windowtext;border-bottom:.5pt solid windowtext;border-left:.5pt solid windowtext;'" 
							+ @" bgcolor=#B9B8B8 align=left colspan="+(dtcap2.Columns.Count+1)+">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>" +oDataRow2[0].ToString()+ "</b></TD>");
						strHTML +=  ("</TR>");
					}
					else
					{
						strHTML += strTempHeadLv2.Replace("$GroupName2$",oDataRow2[0].ToString());
					}

					#region Creating rows
					/*******************************************************************
						* Start, Creating rows
						* *****************************************************************/

					strHTML +=  HTMLDataTable_SUB(dtcap2, strTpl);
					/*******************************************************************
						* End, Creating rows
						* *****************************************************************/
					#endregion
					dtcap2.Dispose();
				}
				#endregion
				dtcap1.Dispose();
			}
			
			#endregion
			#region Sum Total
			if (IsSum)
			{
				if (strTempSumtotal != "")
				{
					//strHTML +=  ("<TR>");
					string strTempSum = strTempSumtotal;
					foreach(DataColumn oDataColumn in oDataTable.Columns)	
					{
						double dbi = 0;
						dbi = SumColumn(oDataTable,oDataColumn.ColumnName);
						if (dbi != 0)
						{
							try
							{
								//strTempSum =  strTempSum.Replace("x:str", "x:num");
								strTempSum =  strTempSum.Replace("$"+oDataColumn.ColumnName+"$", dbi.ToString());
								strTempSum =  strTempSum.Replace("IsNum=1", "x:num");
								//strTempSum =  strTempSum.Replace("IsNum=1", "x:num='"+dbi.ToString()+"'");
							}
							catch{ strTempSum =  strTempSum;}
						}
						else
							strTempSum =  strTempSum.Replace("$"+oDataColumn.ColumnName+"$", "");
					}
					strHTML += strTempSum;
				}
			}
			#endregion
			return strHTML;
		}
		#endregion
		#region Header Replace
		private string HeaderReplace(string Header)
		{
			try 
			{
				string[] marrPara = sHeaderParams.Split(';');
				string[] marrValue = sHeaderValues.Split(';');

				for(int i=0;i<marrPara.Length;i++)
				{
					Header = Header.Replace("$"+marrPara.GetValue(i).ToString().Trim()+"$",marrValue.GetValue(i).ToString().Trim());
					Header =  Header.Replace("IsNum=1", "x:num");
				}
				return Header;
			}
			catch {
				return Header;
			}
		}
		#endregion
		#region Footer Replace
		private string FooterReplace(string Footer)
		{
			try 
			{
				string[] marrPara = sFooterParams.Split(';');
				string[] marrValue = sFooterValues.Split(';');

				for(int i=0;i<marrPara.Length;i++)
				{
					Footer = Footer.Replace("$"+marrPara.GetValue(i).ToString().Trim()+"$",marrValue.GetValue(i).ToString().Trim());
					Footer =  Footer.Replace("IsNum=1", "x:num");
				}
				return Footer;
			}
			catch 
			{
				return Footer;
			}
		}
		#endregion
		#region Split string
		private string strHeaderSplit
			(string strStringHTML, string strEndTag)
		{
			string strStringLC = strStringHTML.ToLower();
			int nStart = strStringLC.IndexOf (strEndTag);
			if (nStart != -1) 
			{
				string strRet = strStringHTML.Substring (0,nStart);

				nStart = strRet.LastIndexOf ("<", nStart);
				if (nStart != -1) 
				{
					strRet = strStringHTML.Substring (0,nStart);
					return strRet;
				}
			}
			return strStringHTML;
		}
		private string strFooterSplit
			(string strStringHTML, string strEndTag)
		{
			string strStringLC = strStringHTML.ToLower();
			int nStart = strStringLC.IndexOf (strEndTag);
			if (nStart != -1) 
			{
				string strHead = strStringHTML.Substring (0,nStart);
				int nHeadStart = strHead.LastIndexOf ("<", nStart);
				if (nStart != -1) 
				{
					string strRet = strStringHTML.Substring (nHeadStart - 1, strStringHTML.Length - nHeadStart);
					return strRet;
				}
			}
			return strStringHTML;
		}
		private string strSplit
			(string strStringHTML, string idTagTR)
		{
			string strStringLC = strStringHTML.ToLower();
			int nStart = strStringLC.IndexOf ("id=" + idTagTR);
			if (nStart != -1) 
			{
				nStart++;
				nStart = strStringLC.IndexOf (">", nStart);
				if (nStart != -1) 
				{
					nStart++;
					int nEnd = strStringLC.IndexOf ("</tr>", nStart);
					if (nEnd != -1) 
					{
						string strRet = "<tr>" + strStringHTML.Substring (nStart, nEnd - nStart) + "</tr>";
						return strRet;
					}
				}
			}
			return strStringHTML;
		}
		public static string strHeaderSplit
			(string strStringHTML, int iRow)
		{
			string strStringLC = strStringHTML.ToLower();
			int nStart = strStringLC.IndexOf ("<tr");
			if (nStart != -1) 
			{
				nStart++;
				nStart = strStringLC.IndexOf (">", nStart);
				if (nStart != -1) 
				{
					nStart++;
					int nEnd = strStringLC.LastIndexOf ("</a>");
					if (nEnd != -1) 
					{
						string strRet = strStringHTML.Substring (nStart, nEnd - nStart);
						return strRet;
					}
				}
			}
			return strStringHTML;
		}

		private string Return_Split(string strFull, int i)
		{
			string strReturn = "";
			string [] marrstrFull = strFull.Split('@');
			try
			{
				strReturn = marrstrFull.GetValue(i).ToString().Trim();
			}
			catch{
				strReturn = "";
			}
			
			return strReturn;
		}
		#endregion
		#region CreateGroupByTable
		public DataTable CreateGroupByTable(string TableName, DataTable SourceTable, string strGroupBy, string ColumnName)
		{
			DataTable dt = new DataTable(TableName);
			int k = 1;
			int[] l = new int[100];
			foreach(DataColumn oDataColumn in SourceTable.Columns)	
			{
				if (ColumnName != oDataColumn.ColumnName) // Neu nam trong List Group
					dt.Columns.Add(oDataColumn.ColumnName);
				else
					l[k] = k;
				k++;
			}
			
			foreach(DataRow oDataRow in SourceTable.Rows)	
			{
				k = 0;
				if (oDataRow[ColumnName].ToString() == strGroupBy)	
				{
					string[] ii = new string[SourceTable.Columns.Count - 1];
					for ( int i = 0; i < SourceTable.Columns.Count; i++)
					{
						if (l[i+1] == 0 || l[i+1].ToString() == null)
						{
							ii[i-k] = oDataRow[i].ToString();
						}
						else
							k++;
					}

					dt.Rows.Add(ii);
				}

			}
			return dt;

		}
		#endregion
		#region Page Replace
		private string PageReplace(DataTable oDataTable, string PageTemp)
		{
			try 
			{
				string[] marrPara = sHeaderParams.Split(';');
				string[] marrValue = sHeaderValues.Split(';');

				for(int i=0;i<marrPara.Length;i++)
				{
					PageTemp = PageTemp.Replace("$"+marrPara.GetValue(i).ToString().Trim()+"$",marrValue.GetValue(i).ToString().Trim());
					PageTemp =  PageTemp.Replace("IsNum=1", "x:num");
				}
				PageTemp = HTMLDataTable(oDataTable,PageTemp);
				return PageTemp;
			}
			catch 
			{
				return PageTemp;
			}
		}
		#endregion
		#endregion
		#region Phần kết nối báo cáo
		public string strReportBasic(System.Data.DataTable oDataTable)
		{
			string strReturn = "";
			string strHeader = "";
			string strBody = "";
			string strBottom = "";
			string strTemp = "";
			
			strTemp = ReadTemplate(sfileTemplate);
			#region strHeader
			string strTag = "";
			if (itempheadlv1 != "")
				strTag = itempheadlv1;
			else
				strTag = idirow;
			strHeader = strHeaderSplit(strTemp,strTag);
			if (strHeader == ""|| strHeader == strTemp)
				strHeader = "<head></head><body><table>";
			#endregion
			#region strBottom
			strBottom = strFooterSplit(strTemp,idifooter);
			if (strBottom == "" || strBottom == strTemp)
				strBottom = "</table></body></html>";
			#endregion
			strBody = strSplit(strTemp,idirow);
			if (strTemp.IndexOf(itempheadlv1) != -1) //Lấy template Head Level1
				strTempHeadLv1 = strSplit(strTemp,itempheadlv1);
			if (strTemp.IndexOf(itempheadlv2) != -1) //Lấy template Head Level2
				strTempHeadLv2 = strSplit(strTemp,itempheadlv2);
			if (strTemp.IndexOf(itempsumlv1) != -1) //Lấy template Sum Level1
				strTempSumLv1 = strSplit(strTemp,itempsumlv1);
			if (strTemp.IndexOf(itempsumlv2) != -1)//Lấy template Sum Level2
				strTempSumLv2 = strSplit(strTemp,itempsumlv2);
			if (strTemp.IndexOf(itempsumtotal) != -1)//Lấy template Sum tổng
				strTempSumtotal = strSplit(strTemp,itempsumtotal);
			
			//strHeader = strHeader.Replace("$Title$",strTitle);
			strHeader = HeaderReplace(strHeader);
			strBody = AddDataHTMLCapHeader(oDataTable,strBody);
			strBottom = FooterReplace(strBottom);

			strReturn = strHeader + strBody+ strBottom;
			return strReturn;
		}

		public string strReportBasic_Sub(System.Data.DataTable oDataTable)
		{
			string strReturn = "";
			string strHeader = "";
			string strBody = "";
			string strBottom = "";
			string strTemp = "";
			
			strTemp = ReadTemplate(sfileTemplate);
			#region strHeader
			strHeader = strHeaderSplit(strTemp,idirow);
			if (strHeader == "")
				strHeader = "<head></head><body><table>";
			#endregion
			#region strBottom
			strBottom = strFooterSplit(strTemp,idifooter);
			if (strBottom == "")
				strBottom = "</table></body></html>";
			#endregion
			strBody = strSplit(strTemp,idirow);
			if (strTemp.IndexOf(itempheadlv1) != -1) //Lấy template Head Level1
				strTempHeadLv1 = strSplit(strTemp,itempheadlv1);
			if (strTemp.IndexOf(itempheadlv2) != -1) //Lấy template Head Level2
				strTempHeadLv2 = strSplit(strTemp,itempheadlv2);
			if (strTemp.IndexOf(itempsumlv1) != -1) //Lấy template Sum Level1
				strTempSumLv1 = strSplit(strTemp,itempsumlv1);
			if (strTemp.IndexOf(itempsumlv2) != -1)//Lấy template Sum Level2
				strTempSumLv2 = strSplit(strTemp,itempsumlv2);
			if (strTemp.IndexOf(itempsumtotal) != -1)//Lấy template Sum tổng
				strTempSumtotal = strSplit(strTemp,itempsumtotal);
			
			//strHeader = strHeader.Replace("$Title$",strTitle);
			strHeader = HeaderReplace(strHeader);
			strBody = AddDataHTMLCapHeader_SUB(oDataTable,strBody);
			strBottom = FooterReplace(strBottom);

			strReturn = strHeader + strBody+ strBottom;
			return strReturn;
		}

		public string strReportPage(System.Data.DataTable oDataTable)
		{
			string strReturn = "";
			string strTemp = "";
			
			strTemp = ReadTemplate(sfileTemplate);
			strReturn = PageReplace(oDataTable,strTemp);
			return strReturn;
		}

		#endregion
	}
}
