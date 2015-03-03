using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Web.Mail;
using iHRPCore.Include;
using iHRPCore.Com;
using Microsoft.VisualBasic ;
using System.Web.SessionState;

namespace Web_DM.Component
{
	/// <summary>
	/// Summary description for clsCatalog.
	/// </summary>
	
	public class clsCatalog
	{
		static string strDataKeyField="";
		public static string str_Script="";
		public static string strLangID="";
		
		public clsCatalog()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		/// <summary>
		/// creater : thanhnd
		/// CreateDate : 05/05/04
		/// Ham tra ve datatable chua du lieu (phu thuoc vao tham so SQL)
		/// </summary>
		/// <param name="pCommandText">Cau lenh loc du lieu</param>
		/// <returns>Datatable</returns>
		public static DataTable GetDataTable(string pCommandText)
		{			
		   
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			SqlDataAdapter da_ = new SqlDataAdapter(pCommandText, cnn);
			DataTable dt = new DataTable();
			DataColumn col = new DataColumn("colNum",System.Type.GetType("System.Int32"));				
			col.AutoIncrement = true;
			dt.Columns.Add(col);
			da_.Fill(dt);
			da_.Dispose();
			cnn.Close();
			cnn.Dispose();
			return dt;
		}
		/// <summary>
		/// creater : thanhnd
		/// CreateDate : 05/05/04			
		/// </summary>
		/// <param name="pCommandText">command string</param>
		/// <returns>tra ve mot datarow, neu khong co du lieu tra ve null</returns>
		public static DataRow GetDataRow(String pCommandText)
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
		/// <summary>
		/// Show message ngay tren server-side
		/// </summary>
		/// <param name="pPage">Trang goi ham</param>
		/// <param name="pMessage">Noi dung message</param>
		public static DataRow GetDataByID(string strTableName,string strKeyField,string strValue)
		{
			string strSQLCommand = "Exec ls_spfrm" + strTableName.Replace("tbl","") + " @Activity = 'GetDataByID',@" + strKeyField + " =N'" + strValue + "'";
			DataRow iRow = clsCatalog.GetDataRow(strSQLCommand);
			return iRow;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="strTableName"></param>
		/// <param name="strDataType"></param>
		/// <param name="strValues"></param>
		/// <returns></returns>
		public static string ExecuteCommand(string strTableName,string strActivity,string[] strParams,string[] strDataType,string[] strValues, System.Web.UI.Page pPage,string strlanguageID)
		{ 
			SqlTransaction sqlTran;
			string strValue;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "ls_spfrm" + strTableName.Trim().Replace("tbl","");			
			//-------
			try
			{
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = strActivity.Trim();
				//cangtt - add - 20060522 - thêm phần ngôn ngữ cho danh mục
				cmd.Parameters.Add("@LanguageID",SqlDbType.NVarChar,2).Value=strlanguageID;
				//-------------------------//
				for(int i=0;i<strValues.Length;i++)
				{
					SqlParameter param = new SqlParameter();
					param.ParameterName = strParams.GetValue(i).ToString().Trim();
					param.SqlDbType = ConvertDataType(strDataType.GetValue(i).ToString().Trim());
					
					strValue = strValues.GetValue(i).ToString();
					if(param.SqlDbType==SqlDbType.Real || param.SqlDbType==SqlDbType.Int || param.SqlDbType==SqlDbType.Money 
						|| param.SqlDbType==SqlDbType.BigInt || param.SqlDbType==SqlDbType.Decimal 
						|| param.SqlDbType==SqlDbType.Float || param.SqlDbType==SqlDbType.SmallInt)
						strValue = strValue.Replace(",","");

					if(strValue.Trim()=="")
						param.Value = DBNull.Value;
					else
					{
						if(param.SqlDbType == SqlDbType.Bit)
							param.Value = (strValue=="True" || strValue=="1")?1:0; //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim()).Trim()=="True"?1:0;
						else
							param.Value =  strValue.Replace("<","&lt;").Replace(">","&gt;").Replace("'","'"); //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim());
						//param.Value =  pPage.Server.HtmlEncode(strValue); //GetValueControl(Page,arrControl.GetValue(i).ToString().Trim());
					}					
					//param.Value = strValue;
					cmd.Parameters.Add(param);
				}
				cmd.Parameters.Add("@ReturnMess", SqlDbType.NVarChar,4000).Direction = ParameterDirection.InputOutput;				
				cmd.ExecuteNonQuery();
				string sReturnValue = cmd.Parameters["@ReturnMess"].Value.ToString().Trim();
				sqlTran.Commit();
				return sReturnValue;				
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

		private static SqlDbType ConvertDataType(string strTypeName)
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
				case "BIGINT":
					TypeReturn = SqlDbType.BigInt;
					break;
				case "SMALLINT":
					TypeReturn = SqlDbType.SmallInt;
					break;
				case "DECIMAL":
					TypeReturn = SqlDbType.Decimal;
					break;
				case "FLOAT":
					TypeReturn = SqlDbType.Float;
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
				default:
					TypeReturn = SqlDbType.NVarChar;
					break;
			}
			return TypeReturn;
		}
		public static void ShowMessageBox(System.Web.UI.Page pPage,string pMessage)
		{				
			string strScript = "<script language=JavaScript>";
			strScript += "alert('" + pMessage + "');";				
			strScript += "</script>";
			if (!pPage.IsStartupScriptRegistered("clientScript"))
				pPage.RegisterStartupScript("clientScript", strScript);				
		}		
		/// <summary>
		/// Create control of catalog
		/// </summary>
		/// <param name="strTableName"></param>
		/// <param name="objHtmlTable"></param>
		/// <param name="XMLFileName"></param>
		/// <param name="grdList"></param>
		/// <param name="btnUpdate"></param>
		/// <param name="btnCancel"></param>
		/// <param name="btnTroVe"></param>
		/// <param name="lblSoDong"></param>
		/// <param name="txtSoDong"></param>
		public static string CreateControlsReport(string strTableName, HtmlTable objHtmlTable,string XMLFileName,DataGrid grdList,
			Button btnUpdate,Button btnCancel,Button btnTroVe,Label lblSoDong,TextBox txtSoDong,iHRPCore.Reports.Reports pPage
			)
		{
			
			//---
			 str_Script = "<script language=javascript> function validform(){";			
			//---
			//int row = 0;
			//'Generate rows and cells.
			//int numrows = 5;
			//int numcells = 2;

			DataSet ds = new DataSet();
			ds.ReadXml(XMLFileName);
			DataView dv  = new DataView(ds.Tables[strTableName]);
			HtmlTableRow r;
			HtmlTableCell c;            

			for(int j = 0;j<dv.Count;j++)
			{
				r = new HtmlTableRow();
				DataRowView drw = dv[j];
				int i;

				c = new HtmlTableCell();
				c.Controls.Add(CreateLabel(drw));
				c.Width = "25%";
				c.VAlign = "middle";


				r.Cells.Add(c);
				c = null;
				c = new HtmlTableCell();
				c.Controls.Add(CreateInputControlsReport(drw,pPage));
				c.Width = "75%";
				c.VAlign = "top";
				r.Cells.Add(c);

				c = null;

				objHtmlTable.Rows.Add(r);
			}			

			ds.Dispose();
			dv.Dispose();
				r = new HtmlTableRow();
				c = new HtmlTableCell();
				c.Controls.Add(new Literal());
				r.Cells.Add(c);
				objHtmlTable.Rows.Add(r);

				r = new HtmlTableRow();
				c = new HtmlTableCell();
				//c.Controls.Add(btnUpdate);
				//c.Controls.Add(btnCancel);
				if( btnTroVe != null ) c.Controls.Add(btnTroVe);

				c.ColSpan = 2;
				c.VAlign = "middle";				
				c.Align = "center";
				r.Cells.Add(c);
				objHtmlTable.Rows.Add(r);

				r = new HtmlTableRow();
				c = new HtmlTableCell();
					
				c.Controls.Add(new Literal());
				r.Cells.Add(c);
				objHtmlTable.Rows.Add(r);

/*				if(txtSoDong != null)
				{
					r = new HtmlTableRow();
					c = new HtmlTableCell();
					//c.Controls.Add(lblSoDong);
					//c.Controls.Add(txtSoDong);
					c.Align = "right";
					c.ColSpan = 2;
					r.Cells.Add(c);
					objHtmlTable.Rows.Add(r);
				}*/
				//r = new HtmlTableRow();
				//c = new HtmlTableCell();				

				grdList.DataKeyField = strDataKeyField;
				grdList.CssClass = "grid";
				grdList.AllowPaging = true;
				grdList.CellPadding = 0;
				grdList.ItemStyle.CssClass = "gridItem";
				grdList.AlternatingItemStyle.CssClass = "gridAlter";
				grdList.HeaderStyle.CssClass = "GridHeader";
				grdList.PagerStyle.HorizontalAlign = HorizontalAlign.Right;
				grdList.PagerStyle.Mode = PagerMode.NumericPages;
				grdList.PagerStyle.CssClass = "gridPage";
				//c.Controls.Add(grdList);			
				//c.ColSpan = 2;
				//c.Align = "center";
				//c.VAlign = "middle";
				//r.Cells.Add(c);
				//objHtmlTable.Rows.Add(r);
			str_Script += "}</script>";
			return str_Script.Trim();
		}

		public static string CreateControlsTemp(string strTableName, HtmlTable objHtmlTable,string XMLFileName,DataGrid grdList,
			Button btnUpdate,Button btnCancel,Button btnTroVe,Label lblSoDong,TextBox txtSoDong,Catalog1 pPage
			)
		{	
			//---
			strLangID = pPage.Session["LangID"] != null?pPage.Session["LangID"].ToString().Trim():"EN";
			str_Script = "<script language=javascript> function validform(){";			
			//---
			//int row = 0;
			//'Generate rows and cells.
			//int numrows = 5;
			//int numcells = 2;

			DataSet ds = new DataSet();
			ds.ReadXml(XMLFileName);
			DataView dv  = new DataView(ds.Tables[strTableName]);
			HtmlTableRow r;
			HtmlTableCell c;            

			for(int j = 0;j<dv.Count;j++)
			{
				r = new HtmlTableRow();
				DataRowView drw = dv[j];
				int i;

				c = new HtmlTableCell();
				c.Controls.Add(CreateLabel(drw));
				c.Width = "25%";
				c.VAlign = "middle";


				r.Cells.Add(c);
				c = null;
				c = new HtmlTableCell();
				c.Controls.Add(CreateInputControlsTemp(drw,pPage));
				c.Width = "75%";
				c.VAlign = "top";
				r.Cells.Add(c);

				c = null;

				objHtmlTable.Rows.Add(r);
			}			

			ds.Dispose();
			dv.Dispose();
			r = new HtmlTableRow();
			c = new HtmlTableCell();
			c.Controls.Add(new Literal());
			r.Cells.Add(c);
			objHtmlTable.Rows.Add(r);

			r = new HtmlTableRow();
			c = new HtmlTableCell();
			//c.Controls.Add(btnUpdate);
			//c.Controls.Add(btnCancel);
			if( btnTroVe != null ) c.Controls.Add(btnTroVe);

			c.ColSpan = 2;
			c.VAlign = "middle";				
			c.Align = "center";
			r.Cells.Add(c);
			objHtmlTable.Rows.Add(r);

			r = new HtmlTableRow();
			c = new HtmlTableCell();
					
			c.Controls.Add(new Literal());
			r.Cells.Add(c);
			objHtmlTable.Rows.Add(r);

			/*				if(txtSoDong != null)
							{
								r = new HtmlTableRow();
								c = new HtmlTableCell();
								//c.Controls.Add(lblSoDong);
								//c.Controls.Add(txtSoDong);
								c.Align = "right";
								c.ColSpan = 2;
								r.Cells.Add(c);
								objHtmlTable.Rows.Add(r);
							}*/
			//r = new HtmlTableRow();
			//c = new HtmlTableCell();				

			grdList.DataKeyField = strDataKeyField;
			grdList.CssClass = "grid";
			grdList.AllowPaging = true;
			grdList.CellPadding = 0;
			grdList.ItemStyle.CssClass = "gridItem";
			grdList.AlternatingItemStyle.CssClass = "gridAlter";
			grdList.HeaderStyle.CssClass = "GridHeader";
			grdList.PagerStyle.HorizontalAlign = HorizontalAlign.Right;
			grdList.PagerStyle.Mode = PagerMode.NumericPages;
			grdList.PagerStyle.CssClass = "gridPage";
			//c.Controls.Add(grdList);			
			//c.ColSpan = 2;
			//c.Align = "center";
			//c.VAlign = "middle";
			//r.Cells.Add(c);
			//objHtmlTable.Rows.Add(r);
			str_Script += "}</script>";
			return str_Script.Trim();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="drw"></param>
		/// <returns></returns>
		/// 
		private static Label CreateLabel(DataRowView drw)
		{
			Label lbl =new Label();
            lbl.ID = "lbl" + drw["ID"].ToString() + "1";
            lbl.Text = drw["Description"].ToString();
			if(drw["IsNull"].ToString().Trim()=="1")
				lbl.CssClass = "Label";
			else
				lbl.CssClass = "Labelrequire_List";
            lbl.Visible = drw["Visible"].ToString().Trim()=="1"?true:false;
			return lbl;
		}		
		/// <summary>
		/// new instance of input control
		/// </summary>
		/// <param name="drw">Datarowview</param>
		/// <returns>control</returns>
		private static Control CreateInputControls(DataRowView drw,Catalog1 pPage)
		{
			Control obj = new Control();
			switch (drw["ControlType"].ToString().ToUpper())
			{
                case "TEXTBOX":
                    TextBox obj1 = new TextBox();					
					if(drw["IsNull"].ToString().Trim()=="0")	str_Script += " if(checkisnull('txt" + drw["ID"].ToString().Trim() + "')==false)  return false;";
					if(drw["TextMode"].ToString().Trim()=="1")
						obj1.TextMode = TextBoxMode.MultiLine;
					else if(drw["TextMode"].ToString().Trim()=="2")
						obj1.TextMode = TextBoxMode.Password;
                    obj1.ID = "txt" + drw["ID"].ToString();
                    obj1.Attributes.Add("runat", "server");
					if(drw["DataType"].ToString().Trim().ToUpper() == "DATETIME")
						obj1.Attributes.Add("Onblur","JavaScript:CheckDate(this)");
					if(drw["DataType"].ToString().Trim().ToUpper() == "DATETIME_FROMTO")
						obj1.Attributes.Add("Onblur","JavaScript:CheckDate_FromTo(document.getElementById('_ctl0_txtFromDate'),document.getElementById('_ctl0_txtToDate'))");
					else if(drw["DataType"].ToString().Trim().ToUpper() == "MONTHYEAR")
						obj1.Attributes.Add("Onblur","JavaScript:CheckMonthYear(this)");
					else if(drw["DataType"].ToString().Trim().ToUpper() == "MONTH")
						obj1.Attributes.Add("Onblur","JavaScript:CheckValidMonth(this)");
					else if(drw["DataType"].ToString().Trim().ToUpper() == "BIGINT" || 
						drw["DataType"].ToString().Trim().ToUpper() == "DECIMAL" || 
						drw["DataType"].ToString().Trim().ToUpper() == "FLOAT" || 
						drw["DataType"].ToString().Trim().ToUpper() == "REAL" || 
						drw["DataType"].ToString().Trim().ToUpper() == "NUMERIC" || 
						drw["DataType"].ToString().Trim().ToUpper() == "MONEY" ||
						drw["DataType"].ToString().Trim().ToUpper() == "SMALLINT")

						obj1.Attributes.Add("Onblur","JavaScript:IsNumeric(this)");	
					else if (drw["DataType"].ToString().Trim().ToUpper() == "INT")
						obj1.Attributes.Add("Onblur","JavaScript:checkInt(this)");	
					else if (drw["DataType"].ToString().Trim().ToUpper()=="TIME")
						obj1.Attributes.Add("Onblur","JavaScript:CheckFillTime(this)");
					if(drw["Width"].ToString() != "" )
							obj1.Width = new Unit(drw["Width"].ToString());                    
                    //obj1.IsNull = Convert.ToBoolean(drw["IsNull"]);
                    //obj1.IsKey = Convert.ToBoolean(drw["IsKey"]);
					if(drw["IsKey"].ToString().Trim()=="1") strDataKeyField = drw["ID"].ToString().Trim();					
                    obj1.Visible = drw["Visible"].ToString().Trim()=="1"?true:false;
                    //obj1.IsNumber = Convert.ToBoolean(drw["IsNumber"]);
                    obj1.Enabled = drw["Enabled"].ToString().Trim()=="1"?true:false;;
                    obj1.Text = drw["DefaultValue"].ToString();
                    obj1.CssClass = drw["CssClass"].ToString();
                    if( Convert.ToInt32(drw["MaxLength"]) != 0)
                        obj1.MaxLength = Convert.ToInt32(drw["MaxLength"]);
					obj = obj1;
                break;
                case "DROPDOWNLIST":
                    DropDownList obj2 = new DropDownList();									
                    obj2.ID = "cbo" + drw["ID"].ToString();
                    obj2.Attributes.Add("runat", "server");
					obj2.CssClass="combo";

					if(drw["TextMode"].ToString().Trim() =="1") 
					{
							//obj2.SelectedIndexChanged
						obj2.SelectedIndexChanged += new System.EventHandler(pPage.DropDownList1_SelectedIndexChanged);
						obj2.AutoPostBack =true;
					}
					if(drw["IsNull"].ToString().Trim()=="0")	str_Script += " if(checkisnull('cbo" + drw["ID"].ToString().Trim() + "')==false)  return false;";
					if(drw["IsKey"].ToString().Trim()=="1") strDataKeyField = drw["ID"].ToString().Trim();
                    if(drw["Width"].ToString() != "")
                        obj2.Width = new  Unit(drw["Width"].ToString());     
					obj = obj2;					
                    BindControl.BindDropDownList(obj2,drw["DataSource"].ToString().Trim(),drw["DefaultValue"].ToString().Trim(),drw["Isnull"].ToString().Trim()=="1"?true:false,-1);
					break;
                case "CHECKBOXLIST":
                    CheckBoxList obj3 = new CheckBoxList();					
                    obj3.ID = "cbl" + drw["ID"].ToString();
                    obj3.Attributes.Add("runat", "server");
					obj3.CssClass="combo";
					if(drw["IsKey"].ToString().Trim()=="1") strDataKeyField = drw["ID"].ToString().Trim();
                    obj3.RepeatDirection = RepeatDirection.Vertical;
                    obj3.RepeatColumns = 2;
                    if(drw["Width"].ToString().Trim() != "")
                        obj3.Width = new Unit(drw["Width"].ToString());
					obj = obj3;
					BindControl.BindCheckBoxList(obj3,drw["DataSource"].ToString().Trim(),drw["DefaultValue"].ToString().Trim(),drw["Isnull"].ToString().Trim()=="1"?true:false,-1);					
					break;
                case "CHECKBOX":
                    CheckBox obj4 = new CheckBox();					
                    obj4.ID = "chk" + drw["ID"].ToString();
                    obj4.Attributes.Add("runat", "server");
					if(drw["IsKey"].ToString().Trim()=="1") strDataKeyField = drw["ID"].ToString().Trim();
                    obj4.Enabled = drw["Enabled"].ToString().Trim()=="1"?true:false;;;
					obj4.Checked = true;
					obj = obj4;
					break;
				case "RADIOBUTTON":
					RadioButton obj5 = new RadioButton();
					obj5.ID = "opt" + drw["ID"].ToString();
					obj5.Attributes.Add("runat", "server");
					if(drw["IsKey"].ToString().Trim()=="1") strDataKeyField = drw["ID"].ToString().Trim();
					obj5.Enabled = drw["Enabled"].ToString().Trim()=="1"?true:false;;;
					obj5.Checked = true;
					obj = obj5;
					break;
				case "RADIOBUTTONLIST":
					RadioButtonList obj6 = new RadioButtonList();
					obj6.ID = "opt" + drw["ID"].ToString();
					obj6.Attributes.Add("runat", "server");
					obj6.CssClass="combo";
					if(drw["IsKey"].ToString().Trim()=="1") strDataKeyField = drw["ID"].ToString().Trim();
					obj6.RepeatDirection = RepeatDirection.Vertical;
					obj6.RepeatColumns = 3;
					obj6.SelectedIndex=1;
					if(drw["Width"].ToString().Trim() != "")
						obj6.Width = new Unit(drw["Width"].ToString());
					obj = obj6;
					BindControl.BindRadioButtonList(obj6,drw["DataSource"].ToString().Trim(),drw["DefaultValue"].ToString().Trim(),drw["Isnull"].ToString().Trim()=="1"?true:false,-1);					
					break;
				}			
            return obj;
		}
		/**
		 *dfd		  
		 * */
		private static Control CreateInputControlsReport(DataRowView drw,iHRPCore.Reports.Reports pPage)
		{
			Control obj = new Control();
			switch (drw["ControlType"].ToString().ToUpper())
			{
				case "TEXTBOX":
					TextBox obj1 = new TextBox();					
					if(drw["IsNull"].ToString().Trim()=="0")	str_Script += " if(checkisnull('txt" + drw["ID"].ToString().Trim() + "')==false)  return false;";
					if(drw["TextMode"].ToString().Trim()=="1")
						obj1.TextMode = TextBoxMode.MultiLine;
					else if(drw["TextMode"].ToString().Trim()=="2")
						obj1.TextMode = TextBoxMode.Password;
					obj1.ID = "txt" + drw["ID"].ToString();
					obj1.Attributes.Add("runat", "server");
					if(drw["DataType"].ToString().Trim().ToUpper() == "DATETIME")
						obj1.Attributes.Add("Onblur","JavaScript:CheckDate(this)");
					if(drw["DataType"].ToString().Trim().ToUpper() == "DATETIME_FROMTO")
						obj1.Attributes.Add("Onblur","JavaScript:CheckDate_FromTo(document.getElementById('_ctl0_txtFromDate'),document.getElementById('_ctl0_txtToDate'))");
					else if(drw["DataType"].ToString().Trim().ToUpper() == "MONTHYEAR")
						obj1.Attributes.Add("Onblur","JavaScript:CheckMonthYear(this)");
					else if( drw["DataType"].ToString().Trim().ToUpper() == "BIGINT" || 
						drw["DataType"].ToString().Trim().ToUpper() == "DECIMAL" || 
						drw["DataType"].ToString().Trim().ToUpper() == "FLOAT" || 
						drw["DataType"].ToString().Trim().ToUpper() == "REAL" || 
						drw["DataType"].ToString().Trim().ToUpper() == "NUMERIC" || 
						drw["DataType"].ToString().Trim().ToUpper() == "MONEY" ||
						drw["DataType"].ToString().Trim().ToUpper() == "SMALLINT")

						obj1.Attributes.Add("Onblur","JavaScript:IsNumeric(this)");	
					else if (drw["DataType"].ToString().Trim().ToUpper() == "INT" )
						obj1.Attributes.Add("Onblur","JavaScript:checkInt(this)");	
					else if (drw["DataType"].ToString().Trim().ToUpper() == "YEAR" )
						obj1.Attributes.Add("Onblur","javascript:CheckYear(this)");
					else if(drw["DataType"].ToString().Trim().ToUpper() == "MONTH")
						obj1.Attributes.Add("Onblur","JavaScript:CheckValidMonth(this)");
					else if (drw["DataType"].ToString().Trim().ToUpper()=="TIME")
						obj1.Attributes.Add("Onblur","JavaScript:CheckFillTime(this)");
					if(drw["Width"].ToString() != "" )
						obj1.Width = new Unit(drw["Width"].ToString());                    
					//obj1.IsNull = Convert.ToBoolean(drw["IsNull"]);
					//obj1.IsKey = Convert.ToBoolean(drw["IsKey"]);
					if(drw["IsKey"].ToString().Trim()=="1") strDataKeyField = drw["ID"].ToString().Trim();					
					obj1.Visible = drw["Visible"].ToString().Trim()=="1"?true:false;
					//obj1.IsNumber = Convert.ToBoolean(drw["IsNumber"]);
					obj1.Enabled = drw["Enabled"].ToString().Trim()=="1"?true:false;;
					obj1.Text = drw["DefaultValue"].ToString();
					obj1.CssClass = drw["CssClass"].ToString();
					if( Convert.ToInt32(drw["MaxLength"]) != 0)
						obj1.MaxLength = Convert.ToInt32(drw["MaxLength"]);
					obj = obj1;
					break;
				case "DROPDOWNLIST":
					DropDownList obj2 = new DropDownList();									
					obj2.ID = "cbo" + drw["ID"].ToString();
					obj2.Attributes.Add("runat", "server");
					obj2.CssClass="combo";

					/*if(drw["TextMode"].ToString().Trim() =="1")
					{
						//obj2.SelectedIndexChanged
						obj2.SelectedIndexChanged += new System.EventHandler(pPage.DropDownList1_SelectedIndexChanged);
						obj2.AutoPostBack =true;
					}*/
					if(drw["IsNull"].ToString().Trim()=="0")	str_Script += " if(checkisnull('cbo" + drw["ID"].ToString().Trim() + "')==false)  return false;";
					if(drw["IsKey"].ToString().Trim()=="1") strDataKeyField = drw["ID"].ToString().Trim();
					if(drw["Width"].ToString() != "")
						obj2.Width = new  Unit(drw["Width"].ToString());     
					obj = obj2;					
					if (drw["DataSource"].ToString().Trim()=="rpt_clsCommon")
					{
						BindControl.BindDropDownList(obj2,drw["DataSource"].ToString().Trim() + " @UserGroupID='" + pPage.Session["AccountLogin"].ToString() + "',",drw["DefaultValue"].ToString().Trim(),drw["Isnull"].ToString().Trim()=="1"?true:false,-1);
					}
					else
					{
						BindControl.BindDropDownList(obj2,drw["DataSource"].ToString().Trim(),drw["DefaultValue"].ToString().Trim(),drw["Isnull"].ToString().Trim()=="1"?true:false,-1);
					}
					break;
				case "CHECKBOXLIST":
					CheckBoxList obj3 = new CheckBoxList();					
					obj3.ID = "cbl" + drw["ID"].ToString();
					obj3.Attributes.Add("runat", "server");
					obj3.CssClass="combo";
					if(drw["IsKey"].ToString().Trim()=="1") strDataKeyField = drw["ID"].ToString().Trim();
					obj3.RepeatDirection = RepeatDirection.Vertical;
					obj3.RepeatColumns = 2;
					if(drw["Width"].ToString().Trim() != "")
						obj3.Width = new Unit(drw["Width"].ToString());
					obj = obj3;
					BindControl.BindCheckBoxList(obj3,drw["DataSource"].ToString().Trim(),drw["DefaultValue"].ToString().Trim(),drw["Isnull"].ToString().Trim()=="1"?true:false,-1);					
					break;
				case "CHECKBOX":
					CheckBox obj4 = new CheckBox();					
					obj4.ID = "chk" + drw["ID"].ToString();
					obj4.Attributes.Add("runat", "server");
					if(drw["IsKey"].ToString().Trim()=="1") strDataKeyField = drw["ID"].ToString().Trim();
					obj4.Enabled = drw["Enabled"].ToString().Trim()=="1"?true:false;;;
					obj4.Checked = true;
					obj = obj4;
					break;
				case "RADIOBUTTON":
					RadioButton obj5 = new RadioButton();
					obj5.ID = "opt" + drw["ID"].ToString();
					obj5.Attributes.Add("runat", "server");
					if(drw["IsKey"].ToString().Trim()=="1") strDataKeyField = drw["ID"].ToString().Trim();
					obj5.Enabled = drw["Enabled"].ToString().Trim()=="1"?true:false;;;
					obj5.Checked = true;
					obj = obj5;
					break;
				case "RADIOBUTTONLIST":
					RadioButtonList obj6 = new RadioButtonList();
					obj6.ID = "opt" + drw["ID"].ToString();
					obj6.Attributes.Add("runat", "server");
					obj6.CssClass="combo";
					if(drw["IsKey"].ToString().Trim()=="1") strDataKeyField = drw["ID"].ToString().Trim();
					obj6.RepeatDirection = RepeatDirection.Vertical;
					obj6.RepeatColumns = 3;
					obj6.SelectedIndex=1;
					if(drw["Width"].ToString().Trim() != "")
						obj6.Width = new Unit(drw["Width"].ToString());
					obj = obj6;
					BindControl.BindRadioButtonList(obj6,drw["DataSource"].ToString().Trim(),drw["DefaultValue"].ToString().Trim(),drw["Isnull"].ToString().Trim()=="1"?true:false,-1);					
					break;
			}			
			return obj;
		}
		private static Control CreateInputControlsTemp(DataRowView drw, Catalog1 pPage)
		{			
			Control obj = new Control();
			switch (drw["ControlType"].ToString().ToUpper())
			{
				case "TEXTBOX":
					TextBox obj1 = new TextBox();					
					if(drw["IsNull"].ToString().Trim()=="0")	
					{
						str_Script += " if(checkisnull('txt" + drw["ID"].ToString().Trim() + "')==false)  return false;";
						obj1.Attributes.Add("onkeypress","JavaScript:checkKey('_ctl0_btnSearch')");	
					}
					if(drw["TextMode"].ToString().Trim()=="1")
						obj1.TextMode = TextBoxMode.MultiLine;
					else if(drw["TextMode"].ToString().Trim()=="2")
						obj1.TextMode = TextBoxMode.Password;
					obj1.ID = "txt" + drw["ID"].ToString();
					obj1.Attributes.Add("runat", "server");
					if(drw["DataType"].ToString().Trim().ToUpper() == "DATETIME")
						obj1.Attributes.Add("Onblur","JavaScript:CheckDate(this)");
					if(drw["DataType"].ToString().Trim().ToUpper() == "DATETIME_FROMTO")
						obj1.Attributes.Add("Onblur","JavaScript:CheckDate_FromTo(document.getElementById('_ctl0_txtFromDate'),document.getElementById('_ctl0_txtToDate'))");
					else if(drw["DataType"].ToString().Trim().ToUpper() == "MONTHYEAR")
						obj1.Attributes.Add("Onblur","JavaScript:CheckMonthYear(this)");
					else if (drw["DataType"].ToString().Trim().ToUpper() == "YEAR" )
						obj1.Attributes.Add("Onblur","javascript:CheckYear(this)");
					else if( drw["DataType"].ToString().Trim().ToUpper() == "BIGINT" || 
						drw["DataType"].ToString().Trim().ToUpper() == "DECIMAL" || 
						drw["DataType"].ToString().Trim().ToUpper() == "FLOAT" || 
						drw["DataType"].ToString().Trim().ToUpper() == "REAL" || 
						drw["DataType"].ToString().Trim().ToUpper() == "NUMERIC" || 
						drw["DataType"].ToString().Trim().ToUpper() == "MONEY" ||
						drw["DataType"].ToString().Trim().ToUpper() == "SMALLINT")
						obj1.Attributes.Add("Onblur","JavaScript:checkNumeric(this)");							
					else if (drw["DataType"].ToString().Trim().ToUpper() == "NUMERIC(5,2)" )
						obj1.Attributes.Add("Onblur","JavaScript:checkNumeric(this,100)");
					// <PhucVD 08/01/07 : Kiem tra he so luong trong danh muc Leave Type - TMS trong khoang (0, 1)>
					else if (drw["DataType"].ToString().Trim().ToUpper() == "NUMERIC_0_1" )
						obj1.Attributes.Add("Onblur","JavaScript:checkNumeric(this,1)");
					// <18/01/07 : Kiem tra so gio lam viec cua ca trong danh muc Shift - TMS trong khoang (0, 24)>
					else if (drw["DataType"].ToString().Trim().ToUpper() == "NUMERIC_0_24" )
						obj1.Attributes.Add("Onblur","JavaScript:checkNumeric(this,24)");	
					// <18/01/07 : Kiem tra so ngay nghi phep toi da tren nam trong danh muc Leave type - TMS trong khoang (0, 28)>
					else if (drw["DataType"].ToString().Trim().ToUpper() == "NUMERIC_0_28" )
						obj1.Attributes.Add("Onblur","JavaScript:checkNumeric(this,28)");	
					// </PhucVD>
					else if (drw["DataType"].ToString().Trim().ToUpper() == "INT" || drw["DataType"].ToString().Trim().ToUpper() == "INTEGER" )
						obj1.Attributes.Add("Onblur","JavaScript:checkInt(this)");
					else if (drw["DataType"].ToString().Trim().ToUpper()=="TIME")
						obj1.Attributes.Add("Onblur","JavaScript:CheckFormat_HHMM(this)");
					if(drw["Width"].ToString() != "" )
						obj1.Width = new Unit(drw["Width"].ToString());                    
					//obj1.IsNull = Convert.ToBoolean(drw["IsNull"]);
					//obj1.IsKey = Convert.ToBoolean(drw["IsKey"]);
					if(drw["IsKey"].ToString().Trim()=="1") strDataKeyField = drw["ID"].ToString().Trim();					
					obj1.Visible = drw["Visible"].ToString().Trim()=="1"?true:false;
					//obj1.IsNumber = Convert.ToBoolean(drw["IsNumber"]);
					obj1.Enabled = drw["Enabled"].ToString().Trim()=="1"?true:false;;
					obj1.Text = drw["DefaultValue"].ToString();
					obj1.CssClass = drw["CssClass"].ToString();
					if( Convert.ToInt32(drw["MaxLength"]) != 0)
						obj1.MaxLength = Convert.ToInt32(drw["MaxLength"]);
					obj = obj1;
					break;
				case "DROPDOWNLIST":
					DropDownList obj2 = new DropDownList();									
					obj2.ID = "cbo" + drw["ID"].ToString();
					obj2.Attributes.Add("runat", "server");
					obj2.CssClass="combo";
					if(drw["TextMode"].ToString().Trim() =="1") 
					{
						//obj2.SelectedIndexChanged
						obj2.SelectedIndexChanged += new System.EventHandler(pPage.DropDownList1_SelectedIndexChanged);
						obj2.AutoPostBack =true;
					}
					if(drw["IsNull"].ToString().Trim()=="0")	
					{
						str_Script += " if(checkisnull('cbo" + drw["ID"].ToString().Trim() + "')==false)  return false;";
					}
					if(drw["IsKey"].ToString().Trim()=="1") strDataKeyField = drw["ID"].ToString().Trim();
					if(drw["Width"].ToString() != "")
						obj2.Width = new  Unit(drw["Width"].ToString());     

					obj2.Visible = drw["Visible"].ToString().Trim()=="1"?true:false;
					obj = obj2;					
					BindControl.BindDropDownList(obj2,drw["DataSource"].ToString().Trim(),drw["DefaultValue"].ToString().Trim(),drw["Isnull"].ToString().Trim()=="1"?true:false,-1);
					break;
				case "CHECKBOXLIST":
					CheckBoxList obj3 = new CheckBoxList();					
					obj3.ID = "cbl" + drw["ID"].ToString();
					obj3.Attributes.Add("runat", "server");
					obj3.CssClass="combo";
					if(drw["IsKey"].ToString().Trim()=="1") strDataKeyField = drw["ID"].ToString().Trim();
					obj3.RepeatDirection = RepeatDirection.Vertical;
					obj3.RepeatColumns = 2;
					if(drw["Width"].ToString().Trim() != "")
						obj3.Width = new Unit(drw["Width"].ToString());
					obj = obj3;
					BindControl.BindCheckBoxList(obj3,drw["DataSource"].ToString().Trim(),drw["DefaultValue"].ToString().Trim(),drw["Isnull"].ToString().Trim()=="1"?true:false,-1);					
					break;
				case "CHECKBOX":
					CheckBox obj4 = new CheckBox();					
					obj4.ID = "chk" + drw["ID"].ToString();
					obj4.Attributes.Add("runat", "server");
					if(drw["TextMode"].ToString().Trim() =="1") 
					{
						//obj2.SelectedIndexChanged
						obj4.CheckedChanged += new System.EventHandler(pPage.CheckBox1_CheckedChanged);
						obj4.AutoPostBack =true;
					}

					obj4.ToolTip=drw["DefaultValue"].ToString().Trim();
					if (obj4.ToolTip == "0")
						obj4.Checked=false;
					else
						obj4.Checked=true;
					if(drw["IsKey"].ToString().Trim()=="1") strDataKeyField = drw["ID"].ToString().Trim();
					obj4.Enabled = drw["Enabled"].ToString().Trim()=="1"?true:false;;;
					obj4.Visible = drw["Visible"].ToString().Trim()=="1"?true:false;
					//obj4.Checked = true;
					obj = obj4;
					break;
				case "RADIOBUTTON":
					RadioButton obj5 = new RadioButton();
					obj5.ID = "opt" + drw["ID"].ToString();
					obj5.Attributes.Add("runat", "server");
					if(drw["IsKey"].ToString().Trim()=="1") strDataKeyField = drw["ID"].ToString().Trim();
					obj5.Enabled = drw["Enabled"].ToString().Trim()=="1"?true:false;;;
					obj5.Checked = true;
					obj = obj5;
					break;
				case "RADIOBUTTONLIST":
					RadioButtonList obj6 = new RadioButtonList();
					obj6.ID = "opt" + drw["ID"].ToString();
					obj6.Attributes.Add("runat", "server");
					obj6.CssClass="combo";
					if(drw["IsKey"].ToString().Trim()=="1") strDataKeyField = drw["ID"].ToString().Trim();
					obj6.RepeatDirection = RepeatDirection.Vertical;
					obj6.SelectedIndex=1;
					obj6.RepeatColumns = 2;
					if(drw["Width"].ToString().Trim() != "")
						obj6.Width = new Unit(drw["Width"].ToString());
					obj = obj6;
					BindControl.BindRadioButtonList(obj6,drw["DataSource"].ToString().Trim(),drw["DefaultValue"].ToString().Trim(),drw["Isnull"].ToString().Trim()=="1"?true:false,-1);					
					break;			
			}			
			return obj;
		}

		public static void BindDataGrid(string XMLFileName,string strTableName,DataTable ds,DataGrid grdDataGrid,bool colDel,bool colEdit,
			string colDelName,string colEditName,string ImageUrl,Object[] ColWidth)
		{
			try
			{
				int i ;
				TemplateColumn objtc;
				BoundColumn objbc;
				for(i = 0;grdDataGrid.Columns.Count>1;i++) //Hau (thay i = 0 thanh i = 1)
				{
					grdDataGrid.Columns.RemoveAt(1); //Hau (thay (0) thanh (1))
				}
				DataTable dsTemp;
				int[] arrColWidth= new int[20];
				//Default			
				for(i = 0;i<ds.Columns.Count;i++)
				{
					arrColWidth.SetValue(200,i);
				}			
				DataSet dts = new DataSet();
				dts.ReadXml(XMLFileName);
				DataTable dtList  = dts.Tables[strTableName];
				//-------
				dsTemp = ds;
				grdDataGrid.AutoGenerateColumns = false;			
				grdDataGrid.DataSource = dsTemp;
				//----------------
				if(colDel)
				{
					objtc = new TemplateColumn();
					objtc.ItemTemplate = new clsDataGrid.DataGridTemplate(ListItemType.Item,"Select","Checkbox","");
					objtc.HeaderText = colDelName;
					objtc.HeaderStyle.Width = new Unit(40);
					objtc.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
					grdDataGrid.Columns.Add(objtc);
					objtc = null;
				}
				//----------------
				for(i = 1;i<ds.Columns.Count;i++) //Hau (thay i = 0 thanh i = 1)
				{
					if((ds.Columns[i].ToString().Trim().ToUpper()) != "URL")
					{
						objbc = new BoundColumn();
						objbc.DataField = ds.Columns[i].ToString().Trim();
						string strHeaderName;
						switch (ds.Columns[i].ToString())
						{
							case "Ma":
								strHeaderName = "ID";
								objbc.SortExpression = ds.Columns[i].ToString();
								break;
							case "Ten":
								strHeaderName = "Name";
								objbc.SortExpression = ds.Columns[i].ToString();
								break;
							default:							
								DataRow[] iRow = dtList.Select("ID ='" + ds.Columns[i].ToString().Trim()+"'");
								if(iRow.Length == 0)
									strHeaderName = ds.Columns[i].ToString();
								else
								{
									strHeaderName = iRow[0][1].ToString();							
									if(iRow[0]["Visible"].ToString().Trim()=="0")
										objbc.Visible = false;
								}
								break;
						}
						objbc.HeaderText = strHeaderName; //ds.Columns[i].ToString
						if(Convert.ToInt32(arrColWidth.GetValue(i).ToString().Trim()) == 0)
							objbc.Visible = false;

						//Hau
						if(i>0)
						{
							objbc.SortExpression = ds.Columns[i].ColumnName;
						}//

						grdDataGrid.Columns.Add(objbc);
						objbc = null;
					}
				}
				dtList.Dispose();
				dts.Dispose();
				if(colEdit)
				{
					if( colEditName.Substring(colEditName.Length-3).ToUpper() != "URL")
					{
						objtc = new TemplateColumn();					
						objtc.ItemTemplate = new clsDataGrid.DataGridTemplate(ListItemType.Item,"","LinkButton",ImageUrl);
						objtc.HeaderText = colEditName;
						objtc.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
						grdDataGrid.Columns.Add(objtc);
						objtc = null;
					}
					else
					{
						objtc = new TemplateColumn();
						//objtc.ItemTemplate = New DataGridTemplate(ds.Tables(0).Columns("URL").ToString, "HYPERLINK", ImageUrl)
						objtc.HeaderText = colEditName.Substring(colEditName.Length-4);
						objtc.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
						grdDataGrid.Columns.Add(objtc);
						objtc = null;
					}
				}
				/*
				if(colDel)
				{
					objtc = new TemplateColumn();
					objtc.ItemTemplate = new clsDataGrid.DataGridTemplate(ListItemType.Item,"Select to delete","Checkbox","");
					objtc.HeaderText = colDelName;
					objtc.HeaderStyle.Width = new Unit(40);
					objtc.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
					grdDataGrid.Columns.Add(objtc);
					objtc = null;
				}
				*/
				grdDataGrid.DataBind();
				dsTemp.Dispose();
				ds.Dispose();
			}
			catch(Exception ee)
			{
				string strErr = ee.Message;
			}
		}
	}
	/// <summary>
	/// Class gan datasourse for control
	/// </summary>
	public class BindControl
	{
		public static void BindDropDownList(DropDownList obj,string strTableName,string strDefaultValue, bool IsNull,int intDefaultIndex)
		{
			//Dim dm As New DanhMucController
            obj.DataSource = DM.GetDanhMucListCBO(strTableName, IsNull);
            obj.DataTextField = "Ten";
            obj.DataValueField = "Ma";
            obj.DataBind();
			if( strDefaultValue != "" )
			{
				obj.SelectedValue = strDefaultValue.Trim();
			}
            if(intDefaultIndex != -1 && obj.Items.Count > intDefaultIndex)
			{
                obj.SelectedIndex = intDefaultIndex;
            }
		}
		public static void BindCheckBoxList(CheckBoxList obj,string strTableName,string strDefaultValue, bool IsNull,int intDefaultIndex)
		{
			//Dim dm As New DanhMucController
			obj.DataSource = DM.GetDanhMucListCBO(strTableName, IsNull);
			obj.DataTextField = "Ten";
			obj.DataValueField = "Ma";
			obj.DataBind();
			if( strDefaultValue != "" )
			{
				obj.SelectedValue = strDefaultValue.Trim();
			}
			if(intDefaultIndex != -1 && obj.Items.Count > intDefaultIndex)
			{
				obj.SelectedIndex = intDefaultIndex;
			}
		}
		//BIND DATA TO RADIOBUTTONLIST
		public static void BindRadioButtonList(RadioButtonList obj,string strTableName,string strDefaultValue, bool IsNull,int intDefaultIndex)
		{
			//Dim dm As New DanhMucController
			obj.DataSource = DM.GetDanhMucListCBO(strTableName, IsNull);
			obj.DataTextField = "Ten";
			obj.DataValueField = "Ma";
			obj.DataBind();
			if( strDefaultValue != "" )
			{
				obj.SelectedValue = strDefaultValue.Trim();
			}
			if(intDefaultIndex != -1 && obj.Items.Count > intDefaultIndex)
			{
				obj.SelectedIndex = intDefaultIndex;
			}
		}
	}
	/// <summary>
	/// 
	/// </summary>
	
/// <summary>
/// class: get datasourse for control
/// </summary>
	public class DM
	{	
		/// <summary>
		/// return keyfield name of catalog
		/// </summary>
		/// <param name="dtList"></param>
		/// <returns></returns>
		public static string GetKeyField(DataTable dtList)
		{
			for(int i=0;i<dtList.Rows.Count;i++)
			{				
				if(dtList.Rows[i]["IsKey"].ToString().Trim()=="1")
					return dtList.Rows[i]["ID"].ToString().Trim();				
			}
			return "";
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="strName"></param>
		/// <param name="strType"></param>
		/// <returns></returns>
		public static string ReturnControlName(string strName,string strType)
		{
			string strCtrlName="";
			switch (strType.Trim().ToUpper())
			{
				case "TEXTBOX":					
					strCtrlName = "txt" + strName.Trim();
					break;
				case "DROPDOWNLIST":
					strCtrlName = "cbo" + strName.Trim();
					break;
				case "CHECKBOXLIST":
					strCtrlName = "chk" + strName.Trim();
					break;
				case "CHECKBOX":
					strCtrlName = "chk" + strName.Trim();
					break;			
				case "OPTION":
					strCtrlName = "opt" + strName.Trim();
					break;	
				case "RADIOBUTTONLIST":
					strCtrlName = "opt" + strName.Trim();
					break;	
			}
			return strCtrlName;
		}
		/// <summary>
		/// Get all controls of catalog
		/// </summary>
		/// <param name="strTableName"></param>
		/// <param name="XMLFileName"></param>
		/// <returns></returns>
		public static DataTable GetControlsofCatalog(string strTableName,string XMLFileName)
		{
			try
			{
				DataSet ds = new DataSet();
				ds.ReadXml(XMLFileName);
				return ds.Tables[strTableName];				
			}				
			catch(Exception ex)
			{				
				return null;
			}			
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="strTableName"></param>
		/// <param name="isNull"></param>
		/// <returns></returns>
		public static DataTable GetDanhMucListCBO(string strTableName,bool isNull)
		{   
            string pCommandText=strTableName + " @Activity='GetDataCombo', @LanguageID='"+clsCatalog.strLangID+"'";
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			cnn.Open();
			SqlDataAdapter da_ = new SqlDataAdapter(pCommandText, cnn);
			DataTable dt = new DataTable();
			DataColumn col = new DataColumn("colNum",System.Type.GetType("System.Int32"));				
			col.AutoIncrement = true;			
			dt.Columns.Add(col);
			try
			{
				da_.Fill(dt);
			}
			catch(SqlException sqlEx)
			{
				string strTmp=sqlEx.Message;
			}
			da_.Dispose();
			cnn.Close();
			cnn.Dispose();
			return dt;
		}
		public static DataTable GetDanhMucListGrid(string strTableName)
		{
			string pCommandText= "ls_spfrm" +strTableName.Trim().Replace("tbl","") + " @Activity='GetDataAll'";
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			cnn.Open();
			SqlDataAdapter da_ = new SqlDataAdapter(pCommandText, cnn);
			DataTable dt = new DataTable();
			DataColumn col = new DataColumn("Seq",System.Type.GetType("System.Int32"));
			col.AutoIncrement = true;
			col.AutoIncrementSeed=1;
			dt.Columns.Add(col);
			da_.Fill(dt);
			da_.Dispose();
			cnn.Close();
			cnn.Dispose();
			return dt;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pRootCtl"></param>
		/// <param name="pFormName"></param>
		public static string GetControl(Page strPageName,string strControlName)
		{
			string strReturnValue="";
			try
			{
				Control ctrFound = strPageName.FindControl(strControlName);
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
//							RadioButtonList optlst = (RadioButtonList)mCtl;
//							optlst.Items[Convert.ToInt32(dtCtl.Rows[i]["ColumnIndex"])].Text = dtCtl.Rows[i][str_FieldName].ToString().Trim();						
							break;
						case "CheckBox":
							strReturnValue = ((CheckBox)ctrFound).Checked.ToString();
//							CheckBox ck = (CheckBox)mCtl;
//							ck.Text = dtCtl.Rows[i][str_FieldName].ToString().Trim();							
							break;
						case "RadioButton":
							strReturnValue = ((RadioButton)ctrFound).Checked.ToString();
//							RadioButton rd = (RadioButton)mCtl;
//							rd.Text = dtCtl.Rows[i][str_FieldName].ToString().Trim();							
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
	}
	//
}




