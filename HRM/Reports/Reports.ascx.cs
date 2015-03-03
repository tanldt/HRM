namespace iHRPCore.Reports
{
	using System;
	using System.Collections;
	using System.ComponentModel;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.SessionState;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Web_DM.Component;
	using iHRPCore.Com;	
	using ExcelReportDll;
	using System.Configuration;
	using System.Data.OleDb;
	using iHRPCore.Include;
	using iHRPCore.Reports.v11;
	using FPTToolWeb.Control.DataGrids;

	/// <summary>
	///		Summary description for Catalog1.
	/// </summary>
	public class Reports : System.Web.UI.UserControl
	{
		protected EmpHeaderSearch EmpHeaderSearch1;
		
		public System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.HtmlControls.HtmlTable Table2;
		protected System.Web.UI.WebControls.Label lblErr;
		public System.Web.UI.WebControls.LinkButton btnPreview;
		public System.Web.UI.WebControls.LinkButton Linkbutton1;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.Label Label14;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.DropDownList grp1;
		protected System.Web.UI.WebControls.DropDownList grp3;
		protected System.Web.UI.WebControls.DropDownList grp4;
		protected System.Web.UI.WebControls.DropDownList grp5;
		protected System.Web.UI.WebControls.DropDownList des1;
		protected System.Web.UI.WebControls.DropDownList des2;
		protected System.Web.UI.WebControls.DropDownList des3;
		protected System.Web.UI.WebControls.DropDownList des4;
		protected System.Web.UI.WebControls.DropDownList des5;
		protected System.Web.UI.WebControls.DropDownList sort1;
		protected System.Web.UI.WebControls.DropDownList sort2;
		protected System.Web.UI.WebControls.DropDownList sort3;
		protected System.Web.UI.WebControls.DropDownList sort4;
		protected System.Web.UI.WebControls.DropDownList sort5;
		protected System.Web.UI.WebControls.DropDownList grp2;
		protected System.Web.UI.WebControls.CheckBox chkShowSort;
		protected System.Web.UI.WebControls.CheckBox chkShowGroup;
		string strTableName = "";
		private void Page_Load(object sender, System.EventArgs e)
		{
			
			lblErr.Text="";
			try
			{
				// Put user code to initialize the page here
				string str_Script="";
				if(Request.Params["tabid"] != null)
				{					
					strTableName = Request.Params["tabid"].ToString().Trim();
					//Edit date : 
					//truong hop report khong co params
					DataSet ds = new DataSet();
					ds.ReadXml(Server.MapPath(".") +  ConfigurationSettings.AppSettings["pstrPathReportsXML"]);
					DataView dv  = new DataView(ds.Tables[strTableName]);
					if(dv.Count > 1)
						// End
					{
						str_Script = clsCatalog.CreateControlsReport(strTableName,Table2, Server.MapPath(".") +  ConfigurationSettings.AppSettings["pstrPathReportsXML"],new DataGrid(),null,null,null,new Label(),null,this);
						if(str_Script.Trim()!="") Response.Write(str_Script);
						btnPreview.Attributes.Add("OnClick", " return validform()");
					}
				}
				if (! IsPostBack)
				{			
					LoadCombos();			
					ReloadDropDownList();
				}
				
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				//cangtt bỏ phần hiện lỗi -- 10/04/2006
				//lblErr.Text = ex.Message ;//"Report not existed!";
			}
		}
		public void DropDownList1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				ReloadDropDownList();
			}
			catch(Exception ee)
			{
				lblErr.Text = ee.Message;
			}
		}
		private void ReloadDropDownList()
		{
			try
			{
				DataTable dtList = new DataTable();
				dtList = DM.GetControlsofCatalog(strTableName,Server.MapPath(".") +  ConfigurationSettings.AppSettings["pstrPathReportsXML"]);
				string strDataSource ;
				int intParamIndex ;
				string strParam;
				string strParamValue;
				string strControlName;

				for(int i=0;i<dtList.Rows.Count;i++)
				{

					if(dtList.Rows[i]["ControlType"].ToString().Trim() == "DropDownList")
					{
						strDataSource = dtList.Rows[i]["DataSource"].ToString().Trim();
						intParamIndex = strDataSource.IndexOf("@@");
						if(intParamIndex >= 0)
						{
							strParam = strDataSource.Substring(intParamIndex);							
							strParamValue = " N'" + GetValueControl("cbo" + strParam.Substring(2)) + "'";

							strDataSource = strDataSource.Replace(strParam,strParamValue) + ", ";
							strControlName = DM.ReturnControlName(dtList.Rows[i]["ID"].ToString(),dtList.Rows[i]["ControlType"].ToString());

							BindControl.BindDropDownList((DropDownList)this.FindControl(strControlName),strDataSource,((DropDownList)this.FindControl(strControlName)).SelectedValue,dtList.Rows[i]["Isnull"].ToString().Trim()=="1"?true:false,-1);
							
						}

					}

				}
			
				dtList.Dispose();
			}
			catch(Exception e)
			{
				string strErr = e.Message;
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion		
		private void ResetControl(Control pRootCtl)
		{
			try
			{
				foreach(Control Child_ctl in pRootCtl.Controls)
				{
					if(Child_ctl.HasControls()==true)
					{
						ResetControl(Child_ctl);
					}
					else
					{						
						string mCtlType = Child_ctl.GetType().ToString().Trim().Substring(Child_ctl.GetType().ToString().Trim().LastIndexOf(".")+1).ToString().Trim();
						switch(mCtlType.ToUpper())
						{								
							case "TEXTBOX":
								((TextBox)Child_ctl).Text = "";
								((TextBox)Child_ctl).Enabled = true;
								break;
							case "DROPDOWNLIST":
								((DropDownList)Child_ctl).SelectedIndex = -1;
								break;
							case "RADIOBUTTONLIST":
								((RadioButtonList)Child_ctl).SelectedIndex = -1;
								break;
							case "CHECKBOX":
								if(Child_ctl.ID.Trim().ToUpper() != "CHKSELECT")
									((CheckBox)Child_ctl).Checked = true;
								break;
							case "RADIOBUTTON":
								((RadioButton)Child_ctl).Checked = true;
								break;
							default :
								break;
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
		/// Get value of control
		/// </summary>
		/// <param name="strControlName">Control name</param>
		/// <returns>Value</returns>
		private string GetValueControl(string strControlName)
		{
			string strReturnValue="";
			try
			{
				Control ctrFound = this.FindControl(strControlName);
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
		
		private void LoadCombos()
		{
			// GET STORE NAME
			string XMLFileName=Server.MapPath(".") +  ConfigurationSettings.AppSettings["pstrPathReportsXML"];
			DataSet ds = new DataSet();
			ds.ReadXml(XMLFileName);
			string strStoreProcedureName="EXEC ";
			strStoreProcedureName+=ds.Tables[strTableName].Rows[ds.Tables[strTableName].Rows.Count-1]["ID"].ToString();
			// LOAD GROUPS, DESCRIPTIONS, SORTS COMBOS
			string strOleConnString=ConfigurationSettings.AppSettings["pstrConnSQLExcelReport"];
			
			DataTable tblSchema=GetTableSchema(strOleConnString,strStoreProcedureName);

			// CREATE GROUPS - DESCRIPTIONS - SORTS COMBOS
			grp1.Items.Clear();grp2.Items.Clear();grp3.Items.Clear();grp4.Items.Clear();grp5.Items.Clear();
			des1.Items.Clear();des2.Items.Clear();des3.Items.Clear();des4.Items.Clear();des5.Items.Clear();
			sort1.Items.Clear();sort2.Items.Clear();sort3.Items.Clear();sort4.Items.Clear();sort5.Items.Clear();

			grp1.Items.Add("");grp2.Items.Add("");grp3.Items.Add("");grp4.Items.Add("");grp5.Items.Add("");
			des1.Items.Add("");des2.Items.Add("");des3.Items.Add("");des4.Items.Add("");des5.Items.Add("");
			sort1.Items.Add("");sort2.Items.Add("");sort3.Items.Add("");sort4.Items.Add("");sort5.Items.Add("");
			for(int index=0;index<tblSchema.Rows.Count;index++)
			{
				// GROUPS				
				grp1.Items.Add(tblSchema.Rows[index]["ColumnName"].ToString());
				grp2.Items.Add(tblSchema.Rows[index]["ColumnName"].ToString());
				grp3.Items.Add(tblSchema.Rows[index]["ColumnName"].ToString());
				grp4.Items.Add(tblSchema.Rows[index]["ColumnName"].ToString());
				grp5.Items.Add(tblSchema.Rows[index]["ColumnName"].ToString());

				des1.Items.Add(tblSchema.Rows[index]["ColumnName"].ToString());
				des2.Items.Add(tblSchema.Rows[index]["ColumnName"].ToString());
				des3.Items.Add(tblSchema.Rows[index]["ColumnName"].ToString());
				des4.Items.Add(tblSchema.Rows[index]["ColumnName"].ToString());
				des5.Items.Add(tblSchema.Rows[index]["ColumnName"].ToString());

				sort1.Items.Add(tblSchema.Rows[index]["ColumnName"].ToString()+" ASC");
				sort1.Items.Add(tblSchema.Rows[index]["ColumnName"].ToString()+" DESC");
				sort2.Items.Add(tblSchema.Rows[index]["ColumnName"].ToString()+" ASC");
				sort2.Items.Add(tblSchema.Rows[index]["ColumnName"].ToString()+" DESC");
				sort3.Items.Add(tblSchema.Rows[index]["ColumnName"].ToString()+" ASC");
				sort3.Items.Add(tblSchema.Rows[index]["ColumnName"].ToString()+" DESC");
				sort4.Items.Add(tblSchema.Rows[index]["ColumnName"].ToString()+" ASC");
				sort4.Items.Add(tblSchema.Rows[index]["ColumnName"].ToString()+" DESC");
				sort5.Items.Add(tblSchema.Rows[index]["ColumnName"].ToString()+" ASC");
				sort5.Items.Add(tblSchema.Rows[index]["ColumnName"].ToString()+" DESC");				
			}

			tblSchema.Dispose();
			tblSchema=null;
			ds.Dispose();
			ds=null;
		}			

		private DataTable GetTableSchema(string strConn, string strSQL)
		{
			OleDbConnection conn=null;;
			OleDbCommand cmd = null;
			OleDbDataReader myReader;
			try
			{
				conn = new OleDbConnection(strConn);
				conn.Open();
				cmd = new OleDbCommand(strSQL,conn);
				myReader = cmd.ExecuteReader(CommandBehavior.SchemaOnly);				
				return myReader.GetSchemaTable();				
			}
			catch(Exception ex)
			{
				return null;
			}//release onbject
			finally
			{				
				myReader = null;				
				cmd.Dispose();
				conn.Close();
				conn.Dispose();
			}
		}

		private void btnPreview_Click(object sender, System.EventArgs e)
		{	
			
			DataTable dtList = new DataTable();
			string strCtrlName="", strTemp ="";
			dtList = DM.GetControlsofCatalog(strTableName,Server.MapPath(".") +   ConfigurationSettings.AppSettings["pstrPathReportsXML"]);
			
			#region Load report xls

			string strFileName = dtList.Rows[dtList.Rows.Count-1]["ReportName"].ToString().Trim();
			
			if (strFileName.IndexOf(".xls") != -1)
			{

				ArrayList arrParams = new ArrayList();
				ArrayList arrValues = new ArrayList();
				ArrayList arrGroups = new ArrayList();
				ArrayList arrGroupDescripts = new ArrayList();
				string arrGroupSorts="";
				string strStoreInputs="";

				for(int i=0;i<dtList.Rows.Count-1;i++)
				{				
					// CHECK IF IT'S PARAM
					if(dtList.Rows[i]["IsParam"].ToString().Trim().ToLower()=="true")
					{			
						arrParams.Add(dtList.Rows[i]["ID"].ToString().Trim());
						strCtrlName = DM.ReturnControlName(dtList.Rows[i]["ID"].ToString(),dtList.Rows[i]["ControlType"].ToString());
						arrValues.Add(GetValueControl(strCtrlName));					
					}
					if(dtList.Rows[i]["IsWhere"].ToString().Trim().ToLower()=="true")
					{			
						strCtrlName = DM.ReturnControlName(dtList.Rows[i]["ID"].ToString(),dtList.Rows[i]["ControlType"].ToString());					
						if(GetValueControl(strCtrlName).Trim()!="")
							strStoreInputs+=" ,@"+dtList.Rows[i]["ID"].ToString()+" = '"+GetValueControl(strCtrlName).Trim()+"' ";					
					}
				}
			
				if (EmpHeaderSearch1.txtEmpID.Text.Trim() !="" )
					strStoreInputs+=" ,@EmpID = N'" + EmpHeaderSearch1.txtEmpID.Text.Trim() + "' ";

				if (EmpHeaderSearch1.txtEmpName.Text.Trim() !="" )
					strStoreInputs+=" ,@EmpName = N'" + EmpHeaderSearch1.txtEmpName.Text.Trim() + "' ";

				if (EmpHeaderSearch1.cboLevel1.SelectedValue.Trim() !="" )
					strStoreInputs+=" ,@Level1ID = N'" + EmpHeaderSearch1.cboLevel1.SelectedValue.Trim() + "' ";

				if (EmpHeaderSearch1.cboLevel2.SelectedValue.Trim() !="" )
					strStoreInputs+=" ,@Level2ID = N'" + EmpHeaderSearch1.cboLevel2.SelectedValue.Trim() + "' ";

				if (EmpHeaderSearch1.cboLevel3.SelectedValue.Trim() !="" )
					strStoreInputs+=" ,@Level3ID = N'" + EmpHeaderSearch1.cboLevel3.SelectedValue.Trim() + "' ";

				if (EmpHeaderSearch1.cboLocation.SelectedValue.Trim() !="" )
					strStoreInputs+=" ,@LocationID = N'" + EmpHeaderSearch1.cboLocation.SelectedValue.Trim() + "' ";

				if (EmpHeaderSearch1.cboJobcode.SelectedValue.Trim() !="" )
					strStoreInputs+=" ,@JobcodeID = N'" + EmpHeaderSearch1.cboJobcode.SelectedValue.Trim() + "' ";

				if (EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue.Trim() !="" )
					strStoreInputs+=" ,@PositionID = N'" + EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue.Trim() + "' ";

				if (EmpHeaderSearch1.cboCompany.SelectedValue.Trim() !="" )
					strStoreInputs+=" ,@CompanyID = N'" + EmpHeaderSearch1.cboCompany.SelectedValue.Trim() + "' ";

				if (EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue.Trim() !="" )
					strStoreInputs+=" ,@LSEmpTypeID = N'" + EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue.Trim() + "' ";

				if (EmpHeaderSearch1.optStatus.SelectedValue.Trim() !="" )
					strStoreInputs+=" ,@Status = '" + EmpHeaderSearch1.optStatus.SelectedValue.Trim() + "' ";

				//Session["ssReportName"] = dtList.Rows[dtList.Rows.Count-1]["ID"].ToString().Trim(); //"HR_rptTest.rpt";
				//Session["ssReportParams"] = arrParams.Substring(0,arrParams.Length-1);
				//Session["ssReportValues"] = arrValues.Substring(0,arrValues.Length-1);
				//--------------

				// GROUPS AND SORTS
				if(grp1.SelectedValue!="") 
				{
					arrGroups.Add(grp1.SelectedValue);
					arrGroupDescripts.Add(des1.SelectedValue);				
				}
				if(grp2.SelectedValue!="") 
				{
					arrGroups.Add(grp2.SelectedValue);
					arrGroupDescripts.Add(des2.SelectedValue);				
				}
				if(grp3.SelectedValue!="") 
				{
					arrGroups.Add(grp3.SelectedValue);
					arrGroupDescripts.Add(des3.SelectedValue);				
				}
				if(grp4.SelectedValue!="") 
				{
					arrGroups.Add(grp4.SelectedValue);
					arrGroupDescripts.Add(des4.SelectedValue);				
				}
				if(grp5.SelectedValue!="") 
				{
					arrGroups.Add(grp5.SelectedValue);
					arrGroupDescripts.Add(des5.SelectedValue);				
				}

				// SORTS
				if(sort1.SelectedValue!="") arrGroupSorts+=sort1.SelectedValue+",";			
				if(sort2.SelectedValue!="") arrGroupSorts+=sort2.SelectedValue+",";
				if(sort3.SelectedValue!="") arrGroupSorts+=sort3.SelectedValue+",";
				if(sort4.SelectedValue!="") arrGroupSorts+=sort4.SelectedValue+",";
				if(sort5.SelectedValue!="") arrGroupSorts+=sort5.SelectedValue+",";
			
				if(arrGroupSorts!="") arrGroupSorts=arrGroupSorts.Remove(arrGroupSorts.Length-1,1);
				///////////////////////////////////////////////			
			
				clsExcelReport exr1 = new clsExcelReport();
				exr1.setSQLConnString(ConfigurationSettings.AppSettings["pstrConnSQLExcelReport"]);
				exr1.setExcelConnString(ConfigurationSettings.AppSettings["pstrConnExcel"]);

				strTemp = Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrReportFolder"];
				strTemp += dtList.Rows[dtList.Rows.Count-1]["Description"].ToString().Trim();
				exr1.setTemplatesFolder(strTemp);
				exr1.setTempFolder(Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrTemplatesFolder"]);
			
				exr1.setParams(arrParams,arrValues);
				exr1.setGroups(arrGroups,arrGroupDescripts);
				exr1.setSorts(arrGroupSorts);

				string strStoreName = dtList.Rows[dtList.Rows.Count-1]["ID"].ToString().Trim();
			
				if(strStoreInputs!="") strStoreInputs=" "+strStoreInputs.Substring(2);
				strStoreName+=strStoreInputs;

				lblErr.Text = exr1.mToExcel(strFileName,strStoreName,this.Page );
				//exr1.Dispose();
				exr1 = null;
			}
	#endregion Load report xls

			#region Load report xls_2
			
			if (strFileName.IndexOf(".htm") != -1)
			{
				ArrayList arrParams = new ArrayList();
				ArrayList arrValues = new ArrayList();
				ArrayList arrGroups = new ArrayList();
				ArrayList arrGroupDescripts = new ArrayList();
				string arrGroupSorts="";
				string strStoreInputs="";

				for(int i=0;i<dtList.Rows.Count-1;i++)
				{				
					// CHECK IF IT'S PARAM
					if(dtList.Rows[i]["IsParam"].ToString().Trim().ToLower()=="true")
					{			
						arrParams.Add(dtList.Rows[i]["ID"].ToString().Trim());
						strCtrlName = DM.ReturnControlName(dtList.Rows[i]["ID"].ToString(),dtList.Rows[i]["ControlType"].ToString());
						arrValues.Add(GetValueControl(strCtrlName));					
					}
					if(dtList.Rows[i]["IsWhere"].ToString().Trim().ToLower()=="true")
					{			
						strCtrlName = DM.ReturnControlName(dtList.Rows[i]["ID"].ToString(),dtList.Rows[i]["ControlType"].ToString());					
						if(GetValueControl(strCtrlName).Trim()!="")
							strStoreInputs+=" ,@"+dtList.Rows[i]["ID"].ToString()+" = '"+GetValueControl(strCtrlName).Trim()+"' ";					
					}
				}
			
				if (EmpHeaderSearch1.txtEmpID.Text.Trim() !="" )
					strStoreInputs+=" ,@EmpID = N'" + EmpHeaderSearch1.txtEmpID.Text.Trim() + "' ";

				if (EmpHeaderSearch1.txtEmpName.Text.Trim() !="" )
					strStoreInputs+=" ,@EmpName = N'" + EmpHeaderSearch1.txtEmpName.Text.Trim() + "' ";

				if (EmpHeaderSearch1.cboLevel1.SelectedValue.Trim() !="" )
					strStoreInputs+=" ,@Level1ID = N'" + EmpHeaderSearch1.cboLevel1.SelectedValue.Trim() + "' ";

				if (EmpHeaderSearch1.cboLevel2.SelectedValue.Trim() !="" )
					strStoreInputs+=" ,@Level2ID = N'" + EmpHeaderSearch1.cboLevel2.SelectedValue.Trim() + "' ";

				if (EmpHeaderSearch1.cboLevel3.SelectedValue.Trim() !="" )
					strStoreInputs+=" ,@Level3ID = N'" + EmpHeaderSearch1.cboLevel3.SelectedValue.Trim() + "' ";

				if (EmpHeaderSearch1.cboLocation.SelectedValue.Trim() !="" )
					strStoreInputs+=" ,@LocationID = N'" + EmpHeaderSearch1.cboLocation.SelectedValue.Trim() + "' ";

				if (EmpHeaderSearch1.cboJobcode.SelectedValue.Trim() !="" )
					strStoreInputs+=" ,@JobcodeID = N'" + EmpHeaderSearch1.cboJobcode.SelectedValue.Trim() + "' ";

				if (EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue.Trim() !="" )
					strStoreInputs+=" ,@PositionID = N'" + EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue.Trim() + "' ";

				if (EmpHeaderSearch1.cboCompany.SelectedValue.Trim() !="" )
					strStoreInputs+=" ,@CompanyID = N'" + EmpHeaderSearch1.cboCompany.SelectedValue.Trim() + "' ";

				if (EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue.Trim() !="" )
					strStoreInputs+=" ,@LSEmpTypeID = N'" + EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue.Trim() + "' ";

				if (EmpHeaderSearch1.optStatus.SelectedValue.Trim() !="" )
					strStoreInputs+=" ,@Status = '" + EmpHeaderSearch1.optStatus.SelectedValue.Trim() + "' ";

				//Session["ssReportName"] = dtList.Rows[dtList.Rows.Count-1]["ID"].ToString().Trim(); //"HR_rptTest.rpt";
				//Session["ssReportParams"] = arrParams.Substring(0,arrParams.Length-1);
				//Session["ssReportValues"] = arrValues.Substring(0,arrValues.Length-1);
				//--------------		

				string strStoreName = dtList.Rows[dtList.Rows.Count-1]["ID"].ToString().Trim();
			
				if(strStoreInputs!="") strStoreInputs=" "+strStoreInputs.Substring(2);
				strStoreName+=strStoreInputs;

				DataTable dt = new DataTable();
				DataTable dtHeader = new DataTable();
				DataTable dtFooter = new DataTable();
				DataTable dtConfig = new DataTable();
				try
				{		
					dt= clsCommon.GetDataTable(strStoreName);
					dtHeader = clsCommon.GetDataTable("SYS_spReportUtils @Activity = 'GetHeader',@ReportName = '" + strTableName + "'");
					dtFooter = clsCommon.GetDataTable("SYS_spReportUtils @Activity = 'GetFooter',@ReportName = '" + strTableName + "'");
					dtConfig = clsCommon.GetDataTable("SYS_spReportUtils @Activity = 'GetConfig',@ReportName = '" + strTableName + "'");

					#region Header Company Info
				
					string strHeaderParams = "";
					string strHeaderValues = "";
				
					if(dtHeader != null)
					{
						for(int i = 0 ; i < dtHeader.Rows.Count ; i++)
						{
							strHeaderParams += dtHeader.Rows[i]["ParamName"].ToString() + ";";
							strHeaderValues += dtHeader.Rows[i]["ParamValue"].ToString() + ";";
						}
						strHeaderParams = strHeaderParams.Substring(0,strHeaderParams.Length - 1);
						strHeaderValues = strHeaderValues.Substring(0,strHeaderValues.Length - 1);
					}

					#endregion
					#region Footer

					string strFooterParams = "";
					string strFooterValues = "";
				
					if(dtFooter != null)
					{
						for(int i = 0 ; i < dtFooter.Rows.Count ; i++)
						{
							strFooterParams += dtFooter.Rows[i]["ParamName"].ToString() + ";";
							strFooterValues += dtFooter.Rows[i]["ParamValue"].ToString() + ";";
						}
						strFooterParams = strFooterParams.Substring(0,strFooterParams.Length - 1);
						strFooterValues = strFooterValues.Substring(0,strFooterValues.Length - 1);
					}

					#endregion

					//Phan khai bao se dung Tool bao cao Excel
					iHRPCore.Reports.v11.clsBaocaoExcel bc = new iHRPCore.Reports.v11.clsBaocaoExcel();

					#region Config Basic
					if(dtConfig != null && dtConfig.Rows.Count > 0)
					{
						bc.IsGroupLv1 = dtConfig.Rows[0]["IsGroupLv1"].ToString() == "1"?true:false; //Co Group 1 khong?
						bc.GroupLv1 = Convert.ToInt32(dtConfig.Rows[0]["ColGroupLv1"].ToString()); //Chỉ định Group level 1 ở cột thứ I
						bc.IsGroupLv2 = dtConfig.Rows[0]["IsGroupLv2"].ToString() == "1"?true:false; //Co Group 2 khong?
						bc.GroupLv2 = Convert.ToInt32(dtConfig.Rows[0]["ColGroupLv2"].ToString()); //Chỉ định Group level 2 ở cột thứ II
						bc.IsSum = dtConfig.Rows[0]["IsSum"].ToString() == "1"?true:false; //Có Sum tong hay không? Nếu có thì phải có id template (itempsumtotal) đi kèm
						bc.IsSum1 = dtConfig.Rows[0]["IsSum1"].ToString() == "1"?true:false; //Có Sum cap 1 hay không? Nếu có thì phải có id template (itempsumlv1) đi kèm
						bc.IsSum2 = dtConfig.Rows[0]["IsSum2"].ToString() == "1"?true:false; //Có Sum cap 2 hay không? Nếu có thì phải có id template (itempsumlv2) đi kèm
					}
				
					bc.sfileTemplate = strFileName;
					bc.sHeaderParams = strHeaderParams;
					bc.sHeaderValues = strHeaderValues;
					bc.sFooterParams = strFooterParams;
					bc.sFooterValues = strFooterValues;
					string strReports = bc.strReportBasic(dt);
					#endregion
					//End

					ExcelExporter myExcelXport=new ExcelExporter(this.Page);
					myExcelXport.ExportHTMLToExcel(strReports,"Excel");
					myExcelXport = null;
				}
				catch(Exception ex)
				{
					clsChangeLang.popupWindowExp(this.Parent,ex);
					return;
				}
				finally
				{
					dt.Dispose();
				}
			}
	#endregion Load report xls_2
			# region Load report crystal

			else if (strFileName.IndexOf(".rpt") != -1)
			{
				string strParams ="";
				string strValues = "";			

				//////   Get params and Get Value of params
				
				strParams = "@Language;@EmpID;@EmpName;@CompanyID;@Level1ID;@Level2ID;@Level3ID;@PositionID;@JobCodeID;@LocationID;@LSEmpTypeID;@Status";
				
				strValues = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
				strValues += ";" + EmpHeaderSearch1.txtEmpID.Text.Trim();
				strValues += ";" + EmpHeaderSearch1.txtEmpName.Text.Trim();
				strValues += ";" + EmpHeaderSearch1.cboCompany.SelectedValue.Trim();
				strValues += ";" + EmpHeaderSearch1.cboLevel1.SelectedValue.Trim();
				strValues += ";" + EmpHeaderSearch1.cboLevel2.SelectedValue.Trim();
				strValues += ";" + EmpHeaderSearch1.cboLevel3.SelectedValue.Trim();
				strValues += ";" + EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue.Trim();				
				strValues += ";" + EmpHeaderSearch1.cboJobcode.SelectedValue.Trim();
				strValues += ";" + EmpHeaderSearch1.cboLocation.SelectedValue.Trim();
				strValues += ";" + EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue.Trim();
				strValues += ";" + (EmpHeaderSearch1.optStatus.SelectedValue.Trim() == ""?"1":EmpHeaderSearch1.optStatus.SelectedValue.Trim());

				for(int i=0;i<dtList.Rows.Count-1;i++)
				{
					if(dtList.Rows[i]["IsWhere"].ToString().Trim().ToLower()=="true")
					{			
						strCtrlName = DM.ReturnControlName(dtList.Rows[i]["ID"].ToString(),dtList.Rows[i]["ControlType"].ToString());
						strParams += ";@" + strCtrlName.Substring(3);
						strValues += ";" + GetValueControl(strCtrlName);
					}
				}

				Session["ssReportName"] = dtList.Rows[dtList.Rows.Count-1]["ReportName"].ToString().Trim(); //"HR_rptTest.rpt";
				Session["ssReportParams"] = strParams;
				Session["ssReportValues"] = strValues;
				
				// GROUPS AND SORTS
				string strGroup = "";
				string strGroupText = "";
				string strSort = "";
				string strSortDirection = "";
				if(grp1.SelectedValue!="") 
				{
					strGroup = grp1.SelectedValue.Trim();
					strGroupText = des1.SelectedValue.Trim() != ""? des1.SelectedValue.Trim():grp1.SelectedValue.Trim();
				}
				if(grp2.SelectedValue!="") 
				{
					strGroup = (strGroup != ""?(strGroup + ";"):"") + grp2.SelectedValue.Trim();
					strGroupText = (strGroupText != ""?(strGroupText + ";"):"") + (des2.SelectedValue.Trim() != ""? des2.SelectedValue.Trim():grp2.SelectedValue.Trim());
				}
				if(grp3.SelectedValue!="") 
				{
					strGroup = (strGroup != ""?(strGroup + ";"):"") + grp3.SelectedValue.Trim();
					strGroupText = (strGroupText != ""?(strGroupText + ";"):"") + (des3.SelectedValue.Trim() != ""? des3.SelectedValue.Trim():grp3.SelectedValue.Trim());			
				}
				if(grp4.SelectedValue!="") 
				{
					strGroup = (strGroup != ""?(strGroup + ";"):"") + grp4.SelectedValue.Trim();
					strGroupText = (strGroupText != ""?(strGroupText + ";"):"") + (des4.SelectedValue.Trim() != ""? des4.SelectedValue.Trim():grp4.SelectedValue.Trim());				
				}
				if(grp5.SelectedValue!="") 
				{
					strGroup = (strGroup != ""?(strGroup + ";"):"") + grp5.SelectedValue.Trim();
					strGroupText = (strGroupText != ""?(strGroupText + ";"):"") + (des5.SelectedValue.Trim() != ""? des5.SelectedValue.Trim():grp5.SelectedValue.Trim());			
				}

				// SORTS
				string strDirection = "";
				if(sort1.SelectedValue!="") 
				{
					strSort = (strSort != ""?(strSort + ";"):"") + sort1.SelectedValue.Trim().Substring(0,sort1.SelectedValue.Trim().LastIndexOf(" "));		
					if (((float)sort1.SelectedIndex / (2.0) * 2) == (sort1.SelectedIndex/2*2))
						strDirection = "Desc";
					else
						strDirection = "";
					strSortDirection = strDirection;
				}
				if(sort2.SelectedValue!="") 
				{
					strSort = (strSort != ""?(strSort + ";"):"") + sort2.SelectedValue.Trim().Substring(0,sort2.SelectedValue.Trim().LastIndexOf(" "));		
					if (((float)sort2.SelectedIndex / (2.0) * 2) == (sort2.SelectedIndex/2*2))
						strDirection = "Desc";
					else
						strDirection = "";
					strSortDirection = strSortDirection + ";" + strDirection;	
				}
				if(sort3.SelectedValue!="") 
				{
					strSort = (strSort != ""?(strSort + ";"):"") + sort3.SelectedValue.Trim().Substring(0,sort3.SelectedValue.Trim().LastIndexOf(" "));		
					if (((float)sort3.SelectedIndex / (2.0) * 2) == (sort3.SelectedIndex/2*2))
						strDirection = "Desc";
					else
						strDirection = "";
					strSortDirection = strSortDirection + ";" + strDirection;	
				}
				if(sort4.SelectedValue!="") 
				{
					strSort = (strSort != ""?(strSort + ";"):"") + sort4.SelectedValue.Trim().Substring(0,sort4.SelectedValue.Trim().LastIndexOf(" "));		
					if (((float)sort4.SelectedIndex / (2.0) * 2) == (sort4.SelectedIndex/2*2))
						strDirection = "Desc";
					else
						strDirection = "";
					strSortDirection = strSortDirection + ";" + strDirection;	
				}
				if(sort5.SelectedValue!="") 
				{
					strSort = (strSort != ""?(strSort + ";"):"") + sort5.SelectedValue.Trim().Substring(0,sort5.SelectedValue.Trim().LastIndexOf(" "));		
					if (((float)sort5.SelectedIndex / (2.0) * 2) == (sort5.SelectedIndex/2*2))
						strDirection = "Desc";
					else
						strDirection = "";
					strSortDirection = strSortDirection + ";" + strDirection;
				}

				Session["ssReportGroupBy"] = strGroup;
				Session["ssReportGroupByText"] = strGroupText;
				Session["ssReportSortBy"] = strSort;
				Session["ssReportSortDirection"] = strSortDirection;
				clsCommon.OpenNewWindow(this.Page,"Reports/ShowReports.aspx","ManPower");							
			}
			# endregion Load report crystal

			dtList.Dispose();

			#region back up
/*
 *		Back up by HaLT 06/07/2005
  
  
			clsExcelReport exr1 = new clsExcelReport();
			exr1.setSQLConnString(ConfigurationSettings.AppSettings["pstrConnSQLExcelReport"]);
			exr1.setExcelConnString(ConfigurationSettings.AppSettings["pstrConnExcel"]);

			strTemp = Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrReportFolder"];
			strTemp += dtList.Rows[dtList.Rows.Count-1]["Description"].ToString().Trim();
			exr1.setTemplatesFolder(strTemp);
			exr1.setTempFolder(Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrTemplatesFolder"]);
			
			exr1.setParams(arrParams,arrValues);
			exr1.setGroups(arrGroups,arrGroupDescripts);
			exr1.setSorts(arrGroupSorts);

			string strFileName = dtList.Rows[dtList.Rows.Count-1]["ReportName"].ToString().Trim();
			string strStoreName = dtList.Rows[dtList.Rows.Count-1]["ID"].ToString().Trim();
			
			if(strStoreInputs!="") strStoreInputs=" "+strStoreInputs.Substring(2);
			strStoreName+=strStoreInputs;

			lblErr.Text = exr1.mToExcel(strFileName,strStoreName,this.Page );
			//exr1.Dispose();
			exr1 = null;
			
*/

			/*
						DataTable dtList = new DataTable();
						dtList = DM.GetControlsofCatalog(strTableName,Request.MapPath("REPORTS.XML").ToString().Trim());
						string arrParams ="";
						string arrValues = "";			
						for(int i=0;i<dtList.Rows.Count-1;i++)
						{
							arrParams += "@" + dtList.Rows[i]["ID"].ToString().Trim() + ";";
							string strCtrlName = DM.ReturnControlName(dtList.Rows[i]["ID"].ToString(),dtList.Rows[i]["ControlType"].ToString());
							arrValues += GetValueControl(strCtrlName) + ";" ;
						}
						Session["ssReportName"] = dtList.Rows[dtList.Rows.Count-1]["ID"].ToString().Trim(); //"HR_rptTest.rpt";
						Session["ssReportParams"] = arrParams.Substring(0,arrParams.Length-1);
						Session["ssReportValues"] = arrValues.Substring(0,arrValues.Length-1);
						clsCommon.OpenNewWindow(this.Page,"Reports/ShowReports.aspx","ManPower");
			//			if(clsCatalog.ExecuteCommand(strTableName,Session["ssclsAction"].ToString(),arrParams,arrDataType,arrValues))
			//				clsCatalog.ShowMessageBox(Page,"Action completed!");
			//			else
			//				clsCatalog.ShowMessageBox(Page,"Action error, Please check again!");			
						dtList.Dispose();
						//this.ResetControl(this);
						//Response.Redirect(Request.ApplicationPath + "/Catalog.aspx?tabid=" + strTableName);
			
			*/

#endregion back up
		}


	}
}
