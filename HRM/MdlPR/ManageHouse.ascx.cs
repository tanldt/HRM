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
	///		Summary description for ManageHouse.
	/// </summary>
	public class ManageHouse : System.Web.UI.UserControl
	{
#region Declare
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.CheckBox chkShowGrid;
		protected System.Web.UI.WebControls.LinkButton btnAddNew;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label lblTotalRows;
		protected System.Web.UI.WebControls.LinkButton Linkbutton1;
		protected System.Web.UI.WebControls.LinkButton Linkbutton2;
		protected System.Web.UI.WebControls.LinkButton Linkbutton3;
		protected System.Web.UI.WebControls.LinkButton btnImport;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.HtmlControls.HtmlTableRow trGrid;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnRowNumber;
		protected System.Web.UI.WebControls.TextBox txtPageRows;
		protected System.Web.UI.WebControls.TextBox txtNote;
		protected System.Web.UI.WebControls.Label lblMonth;		
		#endregion Declare

		protected System.Web.UI.WebControls.TextBox txtAmount;
        protected System.Web.UI.WebControls.DropDownList cboCurrencyTypeID;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.CheckBox chkPIT;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLangID;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.TextBox txtAddress;
        protected System.Web.UI.WebControls.DropDownList Dropdownlist1;
        protected System.Web.UI.HtmlControls.HtmlInputHidden txtManageHouseID;
        protected System.Web.UI.WebControls.TextBox txtFromMonth;
        protected System.Web.UI.WebControls.TextBox txtToMonth;
        protected System.Web.UI.WebControls.TextBox txtAllowance;
        protected System.Web.UI.WebControls.DataGrid grdData;
        protected System.Web.UI.WebControls.TextBox txtStrListID;
        protected System.Web.UI.WebControls.Label Label1;
        protected System.Web.UI.WebControls.Label txtPaySource;
        protected System.Web.UI.WebControls.DropDownList cboPaySource;
		/*protected System.Web.UI.WebControls.Label lblEffectivedate;
		protected System.Web.UI.WebControls.TextBox txtEffectivedate;*/

		public string strLanguage = "EN";
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
			txtLangID.Value= strLanguage;
			if(!Page.IsPostBack)
			{				
				LoadDataCombo();
				LoadDataGrid();
				
				Session["ssStatusAllowance"] = "AddNew";
				if(Session["ssManageHouseID"]!=null)
				{
					LoadDataToEdit(Session["ssManageHouseID"].ToString().Trim());
					Session["ssManageHouseID"]= null;					
				}
			}
			btnSave.Attributes.Add("OnClick", " return validform()");
			btnDelete.Attributes.Add("OnClick", "return checkdelete()");	
			//btnImport.Attributes.Add("onclick","return ShowExcelSelectPage();");
			//MethodChange(optLSMethodID.SelectedIndex);
		}
		/// <summary>
		/// Load all Allowance of Employee
		/// </summary>
        public static DataTable GetDataByEmpID(Object strEmpID,Object strLangID)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = clsCommon.GetDataTable("PR_spfrmManageHouse @Activity='GetDataByEmpID',@EmpID = N'" + strEmpID + "',@languageID='" + strLangID + "'");
                return dt;
            }
            catch(Exception ex){
                string str = ex.Message;
                return null;
            }
            finally{
                dt.Dispose();
            }
        }
        
		private void LoadDataGrid()
		{
			DataTable dtList = new DataTable();
			try
			{
				dtList = GetDataByEmpID(Session["EmpID"],Session["LangID"]);				
				grdData.DataSource = dtList;
				grdData.CurrentPageIndex = 0;
				grdData.DataBind();
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

/*			DataTable dtb = new DataTable();
			dtb.Columns.Add(new DataColumn("AllowanceID"));
			dtb.Columns.Add(new DataColumn("FromMonth"));
			dtb.Columns.Add(new DataColumn("EmpID"));
			dtb.Columns.Add(new DataColumn("ToMonth"));
			dtb.Columns.Add(new DataColumn("AllowanceType"));
			dtb.Columns.Add(new DataColumn("MethodID"));
			dtb.Columns.Add(new DataColumn("Amount"));
			dtb.Columns.Add(new DataColumn("Unit"));
			dtb.Columns.Add(new DataColumn("ToPR"));
			dtb.Columns.Add(new DataColumn("PIT"));
			dtb.Columns.Add(new DataColumn("Note"));			

			DataRow row;
			for(int i=0; i<5; i++)
			{
				row = dtb.NewRow();
				row["AllowanceID"] = "25" + i.ToString();
				row["FromMonth"] = "01/12/2005";
				row["EmpID"]="0000" + i.ToString();
				row["ToMonth"]="01/01/2006";
				row["AllowanceType"]="Trách nhiệm";
				row["MethodID"]="Tiền mặt";
				row["Amount"]="200" + i.ToString();
				row["Unit"]="VND";
				row["ToPR"]="X";
				row["PIT"]="";
				row["Note"]="";
				dtb.Rows.Add(row);
			}
			this.grdData.DataSource = dtb;
			this.grdData.DataBind();
*/
		}
		/// <summary>
		/// Load data to combo box
		/// </summary>
		private void LoadDataCombo()
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";			
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
            this.grdData.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.grdData_ItemCommand);
            this.grdData.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.grdData_PageIndexChanged);
            this.btnRowNumber.ServerClick += new System.EventHandler(this.btnRowNumber_ServerClick);
            this.Load += new System.EventHandler(this.Page_Load);

        }
		#endregion

		private void btnAddNew_Click(object sender, System.EventArgs e)
		{
			clsCommon.ClearControlValue(this);
			txtPageRows.Text=grdData.PageSize.ToString();
			Session["ssStatusAllowance"] = "AddNew";
			if(chkShowGrid.Checked==true)
				trGrid.Style.Add("DISPLAY","block");
			else
				trGrid.Style.Add("DISPLAY","none");
			//MethodChange(optLSMethodID.SelectedIndex);
			chkPIT.Checked=true;
			//chkToPR.Checked=true;
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{   bool f;
		    int type=0;
			try
			{				
				if(Session["ssStatusAllowance"].ToString().Trim()=="AddNew"){					
					f=clsCommon.ImpactDB("@Creater",Session["AccountLogin"],Session["EmpID"].ToString().Trim(),"Save",this,"PR_spfrmManageHouse");
					type=1;
				}
				else{
					f=clsCommon.UpdateByKey("@Editer",Session["AccountLogin"],"ManageHouseID",txtManageHouseID.Value.Trim(),"Update",this,"PR_spfrmManageHouse");
					type=2;
				}
				
                if(f==true){
                    if(type==1){//adnew
                        clsChangeLang.popupWindow(this.Parent,"0046","EN","",1);				        
                    }
                    else{//update
                        clsChangeLang.popupWindow(this.Parent,"0044","EN","",1);
                    }
                }else{
                    clsChangeLang.popupWindow(this.Parent,"0027","EN","",0);
                }
				
				btnAddNew_Click(null,null);
				LoadDataGrid();
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}
/// <summary>
/// Load data to edit
/// </summary>
/// <param name="source"></param>
/// <param name="e"></param>
		private void grdData_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				if(e.CommandName.Trim().ToUpper() =="EDIT")
				{
					LoadDataToEdit(grdData.Items[e.Item.ItemIndex].Cells[0].Text.Trim());					
					
				}
				
			}	
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}
		
        public static DataRow GetDataByID(Object strID)
        {
            DataRow iRow = clsCommon.GetDataRow("PR_spfrmManageHouse @Activity='GetDataByID',@ManageHouseID = " + strID);
            if(iRow !=null)
                return iRow;
            else
                return null;
        }
		//LOAD RECORD DATA TO EDIT
		private void LoadDataToEdit(string strID)
		{
			txtManageHouseID.Value = strID;
			DataRow iRow = GetDataByID(strID);
			if(iRow != null)
			{
				txtAmount.Text = iRow["Amount"].ToString().Trim();
				txtAllowance.Text=iRow["Allowance"].ToString().Trim();
				txtNote.Text = iRow["Note"].ToString().Trim();
				txtAddress.Text=iRow["Address"].ToString().Trim();
				txtFromMonth.Text = iRow["FromMonth"].ToString().Trim();
				txtToMonth.Text = iRow["ToMonth"].ToString().Trim();					
				chkPIT.Checked=iRow["PIT"].ToString()=="True"?true:false;
				cboCurrencyTypeID.SelectedValue=iRow["CurrencyTypeID"].ToString();
				try{
				    cboPaySource.SelectedValue=iRow["PaySource"].ToString()=="True"?"1":"0";
                }
                catch(Exception ex)
                {
                    cboPaySource.SelectedValue="";
                }
			}
			Session["ssStatusAllowance"] = "Edit";				
			if(chkShowGrid.Checked==true)
				trGrid.Style.Add("DISPLAY","block");
			else				
				trGrid.Style.Add("DISPLAY","none");


		}
/// <summary>
/// Delete checked record
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
		private void btnDelete_Click(object sender, System.EventArgs e)
		{   bool f;
			try
			{   
				string strID="@";
				for(int i=0;i<grdData.Items.Count;i++)
				{
					if(((CheckBox)grdData.Items[i].FindControl("chkSelect")).Checked==true)
					{
						strID += grdData.Items[i].Cells[0].Text.Trim() + "@";
					}
				}
				//f=clsCommon.DeleteListRecord("PR_spfrmManageHouse","ManageHouseID",SqlDbType.NVarChar,12,strID);				
                txtStrListID.Text=strID;
                f=clsCommon.ImpactDB("@UserGroupID",Session["AccountLogin"],Session["EmpID"].ToString().Trim(),"DeleteList",this,"PR_spfrmManageHouse");				
				
				btnAddNew_Click(null,null);
				LoadDataGrid();
				
                if(f==true){
                    clsChangeLang.popupWindow(this.Parent,"0104","EN","",1);				        
                }
                else{
                    clsChangeLang.popupWindow(this.Parent,"0048","EN","",0);
                }
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}
		private void grdData_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			try
			{
				LoadDataGrid();
				grdData.CurrentPageIndex=e.NewPageIndex;
				grdData.DataBind();
			}
			catch(Exception ex)
			{
				string str = ex.Message;
			}
		}
		private void btnExport_Click(object sender, System.EventArgs e)
		{   grdData.AllowPaging=false;
			iHRPCore.DataGridExcelExporter myExcelXport=new DataGridExcelExporter(grdData);			
			myExcelXport.Export("");
			myExcelXport =null;
			grdData.AllowPaging=true;
		}

		private void btnRowNumber_ServerClick(object sender, System.EventArgs e)
		{
			grdData.PageSize = Convert.ToInt32(txtPageRows.Text.Trim()==""?"1":txtPageRows.Text.Trim());
			grdData.CurrentPageIndex = 0;			
			LoadDataGrid();
		}

		private void MethodChange(int iValue)
		{/*
			if (iValue==0)
			{
				//lblAmountDesc.Text="USD";				
				tdMonth.Style.Add("DISPLAY","block");								
			}
			else if (iValue==1)
			{
				//lblAmountDesc.Text = Session["LangID"] != null?" Percent":" % Lương";							
				tdMonth.Style.Add("DISPLAY","block");								
			}
			else
			{
				//lblAmountDesc.Text = Session["LangID"] != null?" Coef":" h.số lương";				
				tdMonth.Style.Add("DISPLAY","block");								
			}
			*/

		}
		#region Hau

		static public int iMethodID;
		static public String strLSAllowanceCode;
		static public Boolean bToPR;

		private void Save_ChangeLog()
		{	
			String strUserName = Session[0].ToString().Trim();
			//DateTime dtime = DateTime.Now;
			String strActionTime = DateTime.Now.ToString();
			String strAction = Session["ssStatusAllowance"].ToString().Trim();
			String strEmpID = Session["EmpID"].ToString();
			DataTable tbl = clsHREmpList.GetEmpList(Session["EmpID"].ToString().Trim(),this.Page,"0");
			String strEmpName = tbl.Rows[0]["EmpName"].ToString();
			String[][] strFieldChanged;
			strFieldChanged = new String[3][];
			strFieldChanged[0] = new String[8];
			strFieldChanged[1] = new String[8];
			strFieldChanged[2] = new String[8];
			int i = 0;			
			if((strAction.ToUpper().Trim().Equals("EDIT") && !txtAmount.ToolTip.Trim().Equals(txtAmount.Text.Trim()))
				|| (strAction.ToUpper().Trim().Equals("ADDNEW") && !txtAmount.Text.Trim().Equals("")))
			{
				strFieldChanged[0][i] = "Amount";
				strFieldChanged[1][i] = txtAmount.ToolTip;
				strFieldChanged[2][i] = txtAmount.Text;
				i++;
			}
			if((strAction.ToUpper().Trim().Equals("EDIT") && !txtFromMonth.ToolTip.Trim().Equals(txtFromMonth.Text.Trim()))
				|| strAction.ToUpper().Trim().Equals("ADDNEW")&& !txtFromMonth.Text.Trim().Equals(""))
			{
				strFieldChanged[0][i] = "From Month";
				strFieldChanged[1][i] = txtFromMonth.ToolTip;
				strFieldChanged[2][i] = txtFromMonth.Text;
				i++;
			}
			if((strAction.ToUpper().Trim().Equals("EDIT") && !txtToMonth.ToolTip.Trim().Equals(txtToMonth.Text.Trim())) 
				|| strAction.ToUpper().Trim().Equals("ADDNEW") && !txtToMonth.Text.Trim().Equals(""))
			{
				strFieldChanged[0][i] = "To Month";
				strFieldChanged[1][i] = txtToMonth.ToolTip;
				strFieldChanged[2][i] = txtToMonth.Text;
				i++;
			}
			/*
			if((strAction.ToUpper().Trim().Equals("EDIT") && !txtDateFlex.ToolTip.Trim().Equals(txtDateFlex.Text.Trim())) 
				|| strAction.ToUpper().Trim().Equals("ADDNEW") && !txtDateFlex.Text.Trim().Equals(""))
			{
				strFieldChanged[0][i] = "Date";
				strFieldChanged[1][i] = txtDateFlex.ToolTip;
				strFieldChanged[2][i] = txtDateFlex.Text;
				i++;
			}*/			
//			if((strAction.ToUpper().Trim().Equals("EDIT") && !strLSAllowanceCode.Trim().Equals(cboLSAllowanceID.SelectedItem.Text.Trim())) 
//				|| strAction.ToUpper().Trim().Equals("ADDNEW") && !cboLSAllowanceID.SelectedItem.Text.Trim().Equals(""))
//			{
//				strFieldChanged[0][i] = "Type";
//				strFieldChanged[1][i] = strLSAllowanceCode;
//				strFieldChanged[2][i] = cboLSAllowanceID.SelectedItem.Text;
//				i++;
//			}		
//			if((strAction.ToUpper().Trim().Equals("EDIT") && bToPR != chkToPR.Checked) 
//				|| strAction.ToUpper().Trim().Equals("ADDNEW"))
//			{
//				strFieldChanged[0][i] = "Pay to salary";
//				strFieldChanged[1][i] = bToPR.ToString();
//				strFieldChanged[2][i] = chkToPR.Checked.ToString();
//				i++;
//			}		
			
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

		private void optLSMethodID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}
	}
}
