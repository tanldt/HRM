namespace MdlPR
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore;
	using iHRPCore.Com;
	using iHRPCore.PRComponent;

	/// <summary>
	///		Summary description for unfrequentincome.
	/// </summary>
	public class unfrequentincome : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.TextBox txtAmount;
		protected System.Web.UI.WebControls.Label Label14;
		protected System.Web.UI.WebControls.Label Label19;
		protected System.Web.UI.WebControls.TextBox txtNote;
		protected System.Web.UI.WebControls.CheckBox chkShowGrid;
		protected System.Web.UI.WebControls.LinkButton btnAddNew;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.TextBox txtPageRows;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label lblTotalRows;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.DropDownList cboCol1;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.DropDownList cboCol2;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.DropDownList cboCol3;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdBonusID;
		protected System.Web.UI.HtmlControls.HtmlTableRow trGrid;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnRowNumber;
        protected System.Web.UI.WebControls.DropDownList cboUnfrequentIncomeTypeID;
        protected System.Web.UI.WebControls.TextBox txtMonth;
        protected System.Web.UI.WebControls.TextBox txtDate;
        protected System.Web.UI.HtmlControls.HtmlInputHidden txtUnfrequentIncomeID;
        protected System.Web.UI.WebControls.DataGrid grdData;
        protected System.Web.UI.WebControls.TextBox txtStrListID;
        protected System.Web.UI.WebControls.DropDownList cboCurrencyTypeID;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(!Page.IsPostBack)
			{
				LoadDataCombo();
				LoadDataGrid();
				Session["ssStatusBonus"] = "AddNew";
				if(Session["ssUnfrequentIncomeID"]!=null)
				{
					LoadDataToEdit(Session["ssUnfrequentIncomeID"].ToString().Trim());
					Session["ssUnfrequentIncomeID"]= null;					
				}
			}
			btnSave.Attributes.Add("OnClick", " return validform()");
			btnDelete.Attributes.Add("OnClick", "return checkdelete()");
			//btnImport.Attributes.Add("onclick","return ShowExcelSelectPage();");
		}
		/// <summary>
		/// Load all Allowance of Employee
		/// </summary>
		
        public static DataTable GetDataByEmpID(Object strEmpID, string strLanguage)
        {
            DataTable dt = new DataTable();
            try{
                dt = clsCommon.GetDataTableHasID("PR_spfrmUNFREQUENTINCOME @Activity='GetDataByEmpID',@EmpID = N'" + strEmpID + "',@languageID='" + strLanguage + "'");
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
				dtList = GetDataByEmpID(Session["EmpID"], Mession.GlbLangID);				
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
		}
		/// <summary>
		/// Load data to combo box
		/// </summary>
		private void LoadDataCombo()
		{
			string strTextField = Mession.GlbLangID == "VN"?"VNName":"Name";
			clsCommon.LoadDropDownListControl(cboUnfrequentIncomeTypeID,"sp_GetDataCombo @TableName='LS_tblUnfrequentIncomeType',@Fields='UnfrequentIncomeTypeID as [ID]," + strTextField + " as Name'","ID","Name",true);
			clsCommon.LoadDropDownListControl(cboCurrencyTypeID,"sp_GetDataCombo @TableName='LS_tblCurrencyType',@Fields='LSCurrencyTypeID as [ID]," + strTextField + " as Name'","ID","Name",true);			
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

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            bool fl;
            int type=0;
            try
            {				
                if(Session["ssStatusBonus"].ToString().Trim()=="AddNew")
                {
                    fl=clsCommon.ImpactDB("@UserGroupID",Session["AccountLogin"],Session["EmpID"].ToString().Trim(),"Save",this,"PR_spfrmUNFREQUENTINCOME");
                    type=1;
                }
                else
                {
                    fl=clsCommon.UpdateByKey("@UserGroupID",Session["AccountLogin"],"UnfrequentIncomeID",txtUnfrequentIncomeID.Value.Trim(),"Update",this,"PR_spfrmUNFREQUENTINCOME");
                    type=2;
                }
                
                //show alert
                if(fl==true){
                    if(type==1){//adnew
                        clsChangeLang.popupWindow(this.Parent,"0046","EN","",1);				        
                    }
                    else{//update
                        clsChangeLang.popupWindow(this.Parent,"0044","EN","",1);
                    }
                }
                else{
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

        private void btnAddNew_Click(object sender, System.EventArgs e)
        {
            clsCommon.ClearControlValue(this);
            txtPageRows.Text=grdData.PageSize.ToString();
            Session["ssStatusBonus"] = "AddNew";
            if(chkShowGrid.Checked==true)				
                trGrid.Style.Add("DISPLAY","block");
            else
                trGrid.Style.Add("DISPLAY","none");		
        }
        
        public static DataRow GetDataByID(Object strID)
        {
            DataRow iRow = clsCommon.GetDataRow("PR_spfrmUNFREQUENTINCOME @Activity='GetDataByID',@UnfrequentIncomeID = " + strID);
            if(iRow !=null)
                return iRow;
            else
                return null;
        }
        
        private void LoadDataToEdit(string strID)
        {
            try
            {
                txtUnfrequentIncomeID.Value = strID;
                DataRow iRow = GetDataByID(strID);
                if(iRow != null)
                {
                    cboUnfrequentIncomeTypeID.SelectedValue=iRow["UnfrequentIncomeTypeID"].ToString().Trim();
                    txtAmount.Text = iRow["Amount"].ToString().Trim();
                    cboCurrencyTypeID.SelectedValue=iRow["CurrencyTypeID"].ToString().Trim();
                    txtDate.Text = iRow["Date"].ToString().Trim();
                    txtNote.Text = iRow["Note"].ToString().Trim();					
                    txtMonth.Text = iRow["Month"].ToString().Trim();
                }
                Session["ssStatusBonus"] = "Edit";				
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

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            bool a;
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
                txtStrListID.Text=strID;
                a=clsCommon.ImpactDB("@UserGroupID",Session["AccountLogin"],Session["EmpID"].ToString().Trim(),"DeleteList",this,"PR_spfrmUNFREQUENTINCOME");				
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
                {//update
                    clsChangeLang.popupWindow(this.Parent,"0048","EN","",0);
                }
            }
            catch(Exception ex)
            {
                lblErr.Text = ex.Message;
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
	}
}
