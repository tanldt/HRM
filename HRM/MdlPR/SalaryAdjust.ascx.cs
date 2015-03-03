namespace iHRPCore.MdlPR
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.Com;
	using iHRPCore.PRComponent;
	using System.Data.SqlClient;
	using System.Configuration;
	using iHRPCore.HRComponent;

	/// <summary>
	///		Summary description for SalaryAdjust.
	/// </summary>
	public class SalaryAdjust : System.Web.UI.UserControl
	{
		public string strLanguage = "EN";
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.CheckBox chkShowGrid;
		protected System.Web.UI.WebControls.LinkButton btnAddNew;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.Label Label12;		
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.Label lblTotalRows;
		protected System.Web.UI.WebControls.LinkButton btnImport;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.DataGrid grdSalaryAdjust;
		protected System.Web.UI.WebControls.TextBox txtPageRows;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnRowNumber;
		protected System.Web.UI.HtmlControls.HtmlTableRow trGrid;
		protected System.Web.UI.WebControls.TextBox txtNote;
		protected System.Web.UI.WebControls.TextBox txtSalaryAdjustID;
		protected System.Web.UI.WebControls.TextBox txtAmount;
		protected System.Web.UI.WebControls.DropDownList cboLSSalaryAdjustID;
		protected System.Web.UI.WebControls.TextBox txtMonth;
        protected System.Web.UI.WebControls.DropDownList cboCurrencyTypeID;
        protected System.Web.UI.WebControls.RadioButtonList optAddSubtract;
		protected System.Web.UI.WebControls.CheckBox chkPIT;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
			if(!Page.IsPostBack)
			{
				LoadDataCombo();
				LoadDataGrid();
				Session["ssStatusSalAdjuct"] = "AddNew";
				if(Session["ssSalaryAdjustID"]!=null)
				{
					LoadDataToEdit(Session["ssSalaryAdjustID"].ToString().Trim());
					Session["ssSalaryAdjustID"]= null;					
				}
			}
			btnSave.Attributes.Add("OnClick", " return validform()");
			btnDelete.Attributes.Add("OnClick", "return checkdelete()");
			btnImport.Attributes.Add("onclick","return ShowExcelSelectPage();");
		}
		/// <summary>
		/// LOAD SALARY ADJUST OF EMP
		/// </summary>
		private void LoadDataGrid()
		{
			DataTable dtList = new DataTable();
			try
			{
				dtList = clsPRSalaryAdjust.GetDataByEmpID(Session["EmpID"],Session["LangID"]);				
				grdSalaryAdjust.DataSource = dtList;
				grdSalaryAdjust.CurrentPageIndex = 0;
				grdSalaryAdjust.DataBind();
				lblTotalRows.Text = dtList.Rows.Count.ToString();
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
			finally
			{
				dtList.Dispose();
			}
		}
		/// <summary>
		/// LOAD DATA FOR COMBO
		/// </summary>
		private void LoadDataCombo()
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			clsCommon.LoadDropDownListControl(cboLSSalaryAdjustID,"sp_GetDataCombo @TableName='LS_tblSalaryAdjust',@Fields='LSSalaryAdjustID as [ID]," + strTextField + " as Name'","ID","Name",true);
			clsCommon.LoadDropDownListControl(cboCurrencyTypeID,"LS_spfrmCURRENCYTYPE @Activity=N'GetDataCombo',@LanguageID='"+ strLanguage+ "'","Ma","Ten",false);
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
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            this.grdSalaryAdjust.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.grdSalaryAdjust_ItemCommand);
            this.grdSalaryAdjust.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.grdSalaryAdjust_PageIndexChanged);
            this.btnRowNumber.ServerClick += new System.EventHandler(this.btnRowNumber_ServerClick);
            this.Load += new System.EventHandler(this.Page_Load);

        }
		#endregion

		private void btnAddNew_Click(object sender, System.EventArgs e)
		{
			clsCommon.ClearControlValue(this);
			txtPageRows.Text=grdSalaryAdjust.PageSize.ToString();
			Session["ssStatusSalAdjuct"] = "AddNew";
			if(chkShowGrid.Checked==true)				
				trGrid.Style.Add("DISPLAY","block");
			else
				trGrid.Style.Add("DISPLAY","none");

			//Hau
			iAddSubtract = optAddSubtract.SelectedIndex;
			strLSSalaryAdjustCode = cboLSSalaryAdjustID.SelectedItem.Text;
			bPIT = chkPIT.Checked;
			txtAmount.ToolTip = txtAmount.Text;
			txtMonth.ToolTip = txtMonth.Text;
			////
		}
/// <summary>
/// SAVE DATA TO DB
/// </summary>
		private void btnSave_Click(object sender, System.EventArgs e)
		{   bool f;
		    int type=0;
			try
			{
				if(Session["ssStatusSalAdjuct"].ToString().Trim()=="AddNew")
				{
					f=clsCommon.ImpactDB("@Creater",Session["AccountLogin"],Session["EmpID"].ToString().Trim(),"Save",this,"PR_spfrmSALARYADJUST");
					type=1;    
					//Hau
					Save_ChangeLog();
					////
				}
				else
				{
					f=clsCommon.UpdateByKey("@Editer",Session["AccountLogin"],"ID",txtSalaryAdjustID.Text.Trim(),"Update",this,"PR_spfrmSALARYADJUST");
					type=2;

					//Hau
					Save_ChangeLog();
					////
				}
                if(f==true)
                {
                    if(type==1)
                    {//adnew
                        clsChangeLang.popupWindow(this.Parent,"0046","EN","",1);				        
                    }
                    else
                    {//update
                        clsChangeLang.popupWindow(this.Parent,"0044","EN","",1);
                    }
                }
                else
                {
                    clsChangeLang.popupWindow(this.Parent,"0027","EN","",0);
                }
                
				btnAddNew_Click(null,null);
				LoadDataGrid();
				lblErr.Text = "";
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}
		/// <summary>
		/// EDIT COMMAND ACTION
		/// </summary>		
		private void grdSalaryAdjust_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				if(e.CommandName.Trim().ToUpper() =="EDIT")
				{
					LoadDataToEdit(grdSalaryAdjust.Items[e.Item.ItemIndex].Cells[0].Text.Trim());					

					//Hau
					iAddSubtract = optAddSubtract.SelectedIndex;
					strLSSalaryAdjustCode = cboLSSalaryAdjustID.SelectedItem.Text;
					bPIT = chkPIT.Checked;
					txtAmount.ToolTip = txtAmount.Text;
					txtMonth.ToolTip = txtMonth.Text;
					////
				}				
			}	
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}
		//LOAD RECORD DATA TO EDIT
		private void LoadDataToEdit(string strID)
		{
			try
			{
				txtSalaryAdjustID.Text = strID;
				DataRow iRow = clsPRSalaryAdjust.GetDataByID(strID);
				if(iRow != null)
				{
					txtAmount.Text = iRow["Amount"].ToString().Trim();
					txtMonth.Text = iRow["Month"].ToString().Trim();
					txtNote.Text = iRow["Note"].ToString().Trim();
					chkPIT.Checked = iRow["PIT"].ToString().Trim()=="True"?true:false;
					optAddSubtract.SelectedValue = iRow["AddSubtract"].ToString().Trim()=="True"?"1":"0";
					cboLSSalaryAdjustID.SelectedValue = iRow["LSSalaryAdjustID"].ToString().Trim();					
					cboCurrencyTypeID.SelectedValue =iRow["CurrencyTypeID"].ToString().Trim();
				}
				Session["ssStatusSalAdjuct"] = "Edit";
				if(chkShowGrid.Checked==true)
					trGrid.Style.Add("DISPLAY","block");
				else				
					trGrid.Style.Add("DISPLAY","none");
			}		
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}
/// <summary>
/// DELETE CHECKED RECORD
/// </summary>
		private void btnDelete_Click(object sender, System.EventArgs e)
		{   bool a;
			try
			{
				string strID="";
				for(int i=0;i<grdSalaryAdjust.Items.Count;i++)
				{
					if(((CheckBox)grdSalaryAdjust.Items[i].FindControl("chkSelect")).Checked==true)
					{
						strID += grdSalaryAdjust.Items[i].Cells[0].Text.Trim() + "$";
					}
				}
				a=clsCommon.DeleteListRecord("PR_spfrmSALARYADJUST","SalaryAdjustID",SqlDbType.NVarChar,12,strID);
				if(chkShowGrid.Checked==true)
					trGrid.Style.Add("DISPLAY","block");
				else				
					trGrid.Style.Add("DISPLAY","none");
				LoadDataGrid();
				
                if(a==true)
                {
                    clsChangeLang.popupWindow(this.Parent,"0104","EN","",1);				        
                }
                else
                {
                    clsChangeLang.popupWindow(this.Parent,"0048","EN","",0);
                }
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}
/// <summary>
/// PAGE INDEX CHANGE
/// </summary>
		private void grdSalaryAdjust_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			try
			{
				LoadDataGrid();
				grdSalaryAdjust.CurrentPageIndex=e.NewPageIndex;
				grdSalaryAdjust.DataBind();
			}
			catch(Exception ex)
			{
				string str = ex.Message;
			}
		}
/// <summary>
/// EXPORT DATA ON GRID
/// </summary>
		private void btnExport_Click(object sender, System.EventArgs e)
		{
			iHRPCore.DataGridExcelExporter myExcelXport=new DataGridExcelExporter(grdSalaryAdjust);			
			myExcelXport.Export("");
			myExcelXport =null;
		}

		private void btnRowNumber_ServerClick(object sender, System.EventArgs e)
		{
			grdSalaryAdjust.PageSize = Convert.ToInt32(txtPageRows.Text.Trim()==""?"1":txtPageRows.Text.Trim());
			grdSalaryAdjust.CurrentPageIndex = 0;
			LoadDataGrid();
		}

		#region Hau

		static public int iAddSubtract;
		static public String strLSSalaryAdjustCode;
		static public Boolean bPIT;

		private void Save_ChangeLog()
		{	
			String strUserName = Session[0].ToString().Trim();
			//DateTime dtime = DateTime.Now;
			String strActionTime = DateTime.Now.ToString();
			String strAction = Session["ssStatusSalAdjuct"].ToString().Trim();
			String strEmpID = Session["EmpID"].ToString();
			DataTable tbl = clsHREmpList.GetEmpList(Session["EmpID"].ToString().Trim(),this.Page,"0");
			String strEmpName = tbl.Rows[0]["EmpName"].ToString();
			String[][] strFieldChanged;
			strFieldChanged = new String[3][];
			strFieldChanged[0] = new String[5];
			strFieldChanged[1] = new String[5];
			strFieldChanged[2] = new String[5];
			int i = 0;
			if((strAction.ToUpper().Trim().Equals("EDIT") && iAddSubtract != optAddSubtract.SelectedIndex)
				|| (strAction.ToUpper().Trim().Equals("ADDNEW")))
			{
				strFieldChanged[0][i] = "Add/Subtract";
				strFieldChanged[1][i] = optAddSubtract.Items[iAddSubtract].Text;
				strFieldChanged[2][i] = optAddSubtract.SelectedItem.Text;
				i++;
			}
			if((strAction.ToUpper().Trim().Equals("EDIT") && !strLSSalaryAdjustCode.Trim().Equals(cboLSSalaryAdjustID.SelectedItem.Text.Trim())) 
				|| strAction.ToUpper().Trim().Equals("ADDNEW") && !cboLSSalaryAdjustID.SelectedItem.Text.Trim().Equals(""))
			{
				strFieldChanged[0][i] = "Adjustment type";
				strFieldChanged[1][i] = strLSSalaryAdjustCode;
				strFieldChanged[2][i] = cboLSSalaryAdjustID.SelectedItem.Text;
				i++;
			}		
			if((strAction.ToUpper().Trim().Equals("EDIT") && bPIT != chkPIT.Checked) 
				|| strAction.ToUpper().Trim().Equals("ADDNEW"))
			{
				strFieldChanged[0][i] = "PIT Pay";
				strFieldChanged[1][i] = bPIT.ToString();
				strFieldChanged[2][i] = chkPIT.Checked.ToString();
				i++;
			}	
			if((strAction.ToUpper().Trim().Equals("EDIT") && !txtAmount.ToolTip.Trim().Equals(txtAmount.Text.Trim()))
				|| (strAction.ToUpper().Trim().Equals("ADDNEW") && !txtAmount.Text.Trim().Equals("")))
			{
				strFieldChanged[0][i] = "Amount";
				strFieldChanged[1][i] = txtAmount.ToolTip;
				strFieldChanged[2][i] = txtAmount.Text;
				i++;
			}
			if((strAction.ToUpper().Trim().Equals("EDIT") && !txtMonth.ToolTip.Trim().Equals(txtMonth.Text.Trim())) 
				|| strAction.ToUpper().Trim().Equals("ADDNEW") && !txtMonth.Text.Trim().Equals(""))
			{
				strFieldChanged[0][i] = "PR month";
				strFieldChanged[1][i] = txtMonth.ToolTip;
				strFieldChanged[2][i] = txtMonth.Text;
				i++;
			}		
			
			SaveLog(strUserName, strActionTime, strEmpID, strEmpName, strAction, strFieldChanged, i);
		}

		private void SaveLog(String UserName, String ActionTime, String EmpID, String EmpName, String UserAction, String[][] FieldChanged, int FieldCount)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			
			String CommandText = "insert into ActionLog values(N'";
			CommandText += (UserName + "', '" + ActionTime + "', N'" + UserAction + "', '" + EmpID + "', N'" + EmpName + "', N'");
			try
			{
				for(int i=0; i<FieldCount; i++)
				{
					cmd.CommandText = CommandText + FieldChanged[0][i] + "', N'" + FieldChanged[1][i] + "', N'" + FieldChanged[2][i] + "')";
					cmd.ExecuteNonQuery();
				}
				sqlTran.Commit();
			}
			catch //(Exception exp)
			{
				//lblErr.Text = exp.Message;
				sqlTran.Rollback();
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();
			}
		}

		#endregion
	}
}
