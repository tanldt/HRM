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
	/// <summary>
	///		Summary description for Loan.
	/// </summary>
	public class Loan : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label lblCurrency;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.CheckBox chkShowGrid;
		protected System.Web.UI.WebControls.LinkButton btnAddNew;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label14;
		protected System.Web.UI.WebControls.Label lblToMonth;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.Label Label17;
		protected System.Web.UI.WebControls.Label lblStatus;
		protected System.Web.UI.WebControls.LinkButton btnPayDetail;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.Label lblMonthly;
		protected System.Web.UI.WebControls.TextBox txtReason;
		protected System.Web.UI.WebControls.DataGrid grdLoan;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdLoanID;
		protected System.Web.UI.WebControls.TextBox txtFromMonth;
		protected System.Web.UI.WebControls.TextBox txtInterestRate;
		protected System.Web.UI.WebControls.TextBox txtNoOfMonth;
		protected System.Web.UI.WebControls.TextBox txtLoanAmount;
		protected System.Web.UI.WebControls.TextBox txtNote;
		protected System.Web.UI.WebControls.Label Note;
		protected System.Web.UI.WebControls.Label lblPaid;
		protected System.Web.UI.WebControls.Label lblRemainedAmount;
		protected System.Web.UI.WebControls.Label lblRemainedMonth;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.DropDownList cboLSLoanPurposeID;
		protected System.Web.UI.WebControls.LinkButton btnList;
		protected System.Web.UI.WebControls.TextBox txtSupportRate;
		protected System.Web.UI.WebControls.Label Label16;
		protected System.Web.UI.WebControls.TextBox txtPageRows;
		protected System.Web.UI.WebControls.Label Label18;
		protected System.Web.UI.WebControls.Label lblTotalRows;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnRowNumber;
		protected System.Web.UI.WebControls.Label Label19;
		protected System.Web.UI.WebControls.CheckBox chkDeduction;
		public string strLanguage = "EN";

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
			if(!Page.IsPostBack)
			{
				LoadDataCombo();
				BindDataGrid();
				Session["ssStatusLoan"] = "AddNew";
				if(Session["ssLoanID"]!=null)
				{
					LoadDataToEdit(Session["ssLoanID"].ToString().Trim());
					Session["ssLoanID"]= null;
				}
				//vuonglm: NV thuoc Emptype la expt gi do thi an cac nut button lai.
				string strValue = clsPRLoan.GetAmounAllowByEmpID(Session["EmpID"]);
				if(strValue.Trim()=="TX47")
				{
					btnAddNew.Enabled=false;
					btnSave.Enabled=false;
					btnDelete.Enabled=false;
					btnPayDetail.Enabled=false;
				}				
			}
			btnSave.Attributes.Add("OnClick", " return validform()");
			btnDelete.Attributes.Add("OnClick", "return checkdelete()");
		}
		/// <summary>
		///	LOAD DATA OF EMPLOYEE
		/// </summary>
		private void BindDataGrid()
		{
			DataTable dtList = new DataTable();
			try
			{
				dtList = clsPRLoan.GetDataByEmpID(Session["EmpID"]);
				grdLoan.DataSource = dtList;
				grdLoan.CurrentPageIndex = 0;
				grdLoan.DataBind();
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
			string strTextField = strLanguage == "VN"?"VNName":"Name";				
			clsCommon.LoadDropDownListControl(cboLSLoanPurposeID,"sp_GetDataCombo @TableName='LS_tblLoanPurpose',@Fields='LSLoanPurposeID," + strTextField + " as Name'","LSLoanPurposeID","Name",true);
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
            this.btnPayDetail.Click += new System.EventHandler(this.btnPayDetail_Click);
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            this.btnList.Click += new System.EventHandler(this.btnList_Click);
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            this.grdLoan.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.grdLoan_ItemCommand);
            this.grdLoan.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.grdLoan_PageIndexChanged);
            this.grdLoan.SelectedIndexChanged += new System.EventHandler(this.grdLoan_SelectedIndexChanged);
            this.btnRowNumber.ServerClick += new System.EventHandler(this.btnRowNumber_ServerClick);
            this.Load += new System.EventHandler(this.Page_Load);

        }
		#endregion

		private void btnAddNew_Click(object sender, System.EventArgs e)
		{
			clsCommon.ClearControlValue(this);
			Session["ssStatusLoan"] = "AddNew";
			string strValue = clsPRLoan.GetAmounAllowByEmpID(Session["EmpID"]);
			if(strValue.Trim()=="Error")
			{
				lblErr.Text = "This employee not allow loan due to No of month worked less than 12.";
			}
			else
				txtLoanAmount.Text = strValue.Trim();
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{				
				if(Session["ssStatusLoan"].ToString().Trim()=="AddNew")
					clsCommon.ImpactDB(Session["EmpID"].ToString().Trim(),"Save",this,"PR_spfrmLoan");
				else
					clsCommon.UpdateByKey("LoanID",hdLoanID.Value.Trim(),"Update",this,"PR_spfrmLoan");
				btnAddNew_Click(null,null);
				BindDataGrid();
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}

		private void grdLoan_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				if(e.CommandName.Trim().ToUpper() =="EDIT")
				{
					LoadDataToEdit(grdLoan.Items[e.Item.ItemIndex].Cells[0].Text.Trim());					
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
			hdLoanID.Value = strID;
			DataRow iRow =  clsPRLoan.GetDataByID(strID);
			if(iRow != null)
			{
				txtLoanAmount.Text = iRow["LoanAmount"].ToString().Trim();
				txtNote.Text = iRow["Note"].ToString().Trim();
				txtFromMonth.Text = iRow["FromMonth"].ToString().Trim();				
				cboLSLoanPurposeID.SelectedValue = iRow["LSLoanPurposeID"].ToString().Trim();
				txtInterestRate.Text = iRow["InterestRate"].ToString().Trim();
				txtSupportRate.Text = iRow["SupportRate"].ToString().Trim();
				txtNoOfMonth.Text = iRow["NoOfMonth"].ToString().Trim();
				txtReason.Text = iRow["Reason"].ToString().Trim();
				lblPaid.Text = iRow["PaidAmount"].ToString().Trim();
				lblRemainedMonth.Text = iRow["RemainedMonth"].ToString().Trim();
				lblStatus.Text = (iRow["Status"].ToString().Trim().Equals("1")) ? "Đã hết nợ" : "Còn nợ";	
				//vuonglm				
				chkDeduction.Checked=(iRow["Deduction"].ToString().Trim().Equals("1")) ? true : false;
				if((iRow["IsAllow"].ToString().Trim()!="1") || (lblStatus.Text == "Đã hết nợ"))
					btnSave.Enabled = false;
				else
					btnSave.Enabled = true;

			}
		
			iRow = clsPRLoan.GetDataRemainedByID(strID);
			if(iRow != null)
			{
				lblPaid.Text = iRow["PaidAmount"].ToString().Trim();
				lblRemainedMonth.Text = iRow["RemainedMonth"].ToString().Trim();
				lblRemainedAmount.Text = iRow["RemainedAmount"].ToString().Trim();
				lblStatus.Text = iRow["Status"].ToString().Trim();				
			}
			Session["ssStatusLoan"] = "Edit";			
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				string strID="";
				for(int i=0;i<grdLoan.Items.Count;i++)
				{
					if(((CheckBox)grdLoan.Items[i].FindControl("chkSelect")).Checked==true)
					{
						strID += grdLoan.Items[i].Cells[0].Text.Trim() + "$";
					}
				}
				clsCommon.DeleteListRecord("PR_spfrmLOAN","LoanID",SqlDbType.VarChar,12,strID);				
				clsCommon.ClearControlValue(this);
				BindDataGrid();
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}

		private void btnExport_Click(object sender, System.EventArgs e)
		{
			this.grdLoan.AllowPaging = false;
			BindDataGrid();
			iHRPCore.DataGridExcelExporter myExcelXport=new DataGridExcelExporter(grdLoan);			
			myExcelXport.Export("");
			myExcelXport =null;
			this.grdLoan.AllowPaging = true;
			BindDataGrid();
		}

		private void btnPayDetail_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("Editpage.aspx?ModuleID=PR&ParentID=68&FunctionID=121&Ascx=MdlPR/LoanDetail.ascx");
		}

		private void btnList_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("Editpage.aspx?ModuleID=PR&ParentID=68&FunctionID=119&Ascx=MdlPR/LoanList.ascx");
		}

		private void btnRowNumber_ServerClick(object sender, System.EventArgs e)
		{
			grdLoan.PageSize = Convert.ToInt32(txtPageRows.Text.Trim()==""?"1":txtPageRows.Text.Trim());
			grdLoan.CurrentPageIndex = 0;
			BindDataGrid();
		}

		private void grdLoan_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			try
			{
				BindDataGrid();
				grdLoan.CurrentPageIndex=e.NewPageIndex;
				grdLoan.DataBind();
			}
			catch(Exception ex)
			{
				string str = ex.Message;
			}		
		}

		private void grdLoan_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

	}
}
