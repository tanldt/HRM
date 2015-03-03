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
	///	Summary description for LoadDetail.
	/// </summary>
	public class LoadDetail : System.Web.UI.UserControl
	{
		public string strLanguage = "EN";
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label lblCurrency;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnList;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label lblStatus;
		protected System.Web.UI.WebControls.Label Label17;
		protected System.Web.UI.WebControls.LinkButton btnPayDetail;
		protected System.Web.UI.WebControls.CheckBox chkLastPay;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label lblRate;
		protected System.Web.UI.WebControls.Label lblSupport;
		protected System.Web.UI.WebControls.DropDownList cboLoanTime;
		protected System.Web.UI.WebControls.TextBox txtPRMonth;
		protected System.Web.UI.WebControls.TextBox txtPayInCash;
		protected System.Web.UI.WebControls.Label Note;
		protected System.Web.UI.WebControls.TextBox txtNote;
		protected System.Web.UI.WebControls.DataGrid grdLoanDetail;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdLoanDetailID;
		protected System.Web.UI.WebControls.Label lblPaid;
		protected System.Web.UI.WebControls.Label lblRemainedAmount;
		protected System.Web.UI.WebControls.Label lblTotal;
		protected System.Web.UI.WebControls.TextBox txtMonthToPay;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdLastPay;
		protected System.Web.UI.WebControls.Label lblLoanAmount;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
			if(!Page.IsPostBack)
			{
				LoadDataCombo();
				LoadDataGrid();
			}
			btnSave.Attributes.Add("OnClick", " return validform()");			
		}
		/// <summary>
		///	LOAD DATA OF EMPLOYEE
		/// </summary>
		private void LoadDataGrid()
		{
			DataTable dtList = new DataTable();
			try
			{
				dtList = clsPRLoan.GetDataByLoanID(cboLoanTime.SelectedValue.Trim());
				grdLoanDetail.DataSource = dtList;
				grdLoanDetail.CurrentPageIndex = 0;
				grdLoanDetail.DataBind();

				lblLoanAmount.Text = dtList.Rows[0]["LoanAmount"].ToString();
				lblRate.Text = dtList.Rows[0]["InterestRate"].ToString();
				lblSupport.Text = dtList.Rows[0]["SupportRate"].ToString();
				lblPaid.Text = dtList.Rows[0]["PaidAmount"].ToString().Trim();				
				lblRemainedAmount.Text = dtList.Rows[0]["RemainedAmount"].ToString().Trim();
				lblStatus.Text = (dtList.Rows[0]["Status"].ToString().Trim().Equals("1")) ? "Đã hết nợ" : "Còn nợ";
				if(lblStatus.Text == "Đã hết nợ")
					this.btnSave.Enabled = false;
				else
					this.btnSave.Enabled = true;

				DataRow iRow = clsPRLoan.GetDataRemainedByID(cboLoanTime.SelectedValue.Trim());
				if(iRow != null)
				{
					lblPaid.Text = iRow["PaidAmount"].ToString().Trim();				
					lblRemainedAmount.Text = iRow["RemainedAmount"].ToString().Trim();
					lblStatus.Text = iRow["Status"].ToString().Trim();				
				}
			}
			catch(Exception ex)
			{
				//lblErr.Text = ex.Message;
			}
			finally
			{
				dtList.Dispose();
			}
		}
		/// <summary>
		/// LOAD DATA COMBO
		/// </summary>
		private void LoadDataCombo()
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";				
			clsCommon.LoadDropDownListControl(this.cboLoanTime,"PR_spfrmLoan @Activity = 'GetDataComboByEmpID',@EmpID=N'" + Session["EmpID"] + "'","LoanID","LoanTime",false);
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
			this.cboLoanTime.SelectedIndexChanged += new System.EventHandler(this.cboLoanTime_SelectedIndexChanged);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnPayDetail.Click += new System.EventHandler(this.btnPayDetail_Click);
			this.btnList.Click += new System.EventHandler(this.btnList_Click);
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			this.grdLoanDetail.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.grdLoanDetail_ItemCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void cboLoanTime_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ClearControlValue();
			LoadDataGrid();
			
		}
		private void ClearControlValue()
		{
			txtMonthToPay.Text="";
			txtPayInCash.Text="";
			txtPRMonth.Text="";
			txtNote.Text="";
			lblLoanAmount.Text="";
			lblPaid.Text="";
			lblRate.Text="";
			lblStatus.Text="";
			lblTotal.Text="";
			lblSupport.Text="";
			lblRemainedAmount.Text="";			
		}
		private void btnPayDetail_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("Editpage.aspx?ModuleID=PR&ParentID=68&FunctionID=120&Ascx=MdlPR/Loan.ascx");
		}
		//VIEW AND EDIT RECORD
		private void grdLoanDetail_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				if(e.CommandName.Trim().ToUpper() =="EDIT")
				{
					this.txtPayInCash.Text = "0";
					if(e.Item.ItemIndex == grdLoanDetail.Items.Count-1)
						chkLastPay.Checked = true;
					else
						chkLastPay.Checked = false;
					LoadDataToEdit(grdLoanDetail.Items[e.Item.ItemIndex].Cells[0].Text.Trim());					
				}				
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}
		//LOAD RECORD DATA TO EDIT
		private void LoadDataToEdit(string strLoanDetailID)
		{
			hdLoanDetailID.Value = strLoanDetailID;
			DataRow iRow = clsPRLoan.GetDataByLoanDetailID(strLoanDetailID);
			if(iRow != null)
			{
				bool blnIsPaid = Convert.ToBoolean(iRow["IsPay"].ToString().Trim());
				txtMonthToPay.Text = (blnIsPaid == false ? iRow["MonthToPay"].ToString().Trim() : iRow["PayToPR"].ToString().Trim());
				hdLastPay.Value=(blnIsPaid == false ? iRow["MonthToPay"].ToString().Trim() : iRow["PayToPR"].ToString().Trim());
				txtNote.Text = iRow["Note"].ToString().Trim();
				txtPRMonth.Text = iRow["MonthID"].ToString().Trim();
				txtPayInCash.Text = iRow["PayInOther"].ToString().Trim();				
				if((iRow["IsAllow"].ToString().Trim()!="1") || (lblStatus.Text == "Đã hết nợ"))
					btnSave.Enabled = false;
				else
					btnSave.Enabled = true;
			}
			iRow = clsPRLoan.GetDataRemainedByID(cboLoanTime.SelectedValue.Trim());
		
			if(iRow != null)
			{
				lblPaid.Text = iRow["PaidAmount"].ToString().Trim();				
				lblRemainedAmount.Text = iRow["RemainedAmount"].ToString().Trim();
				lblStatus.Text = iRow["Status"].ToString().Trim();				
			}
			Session["ssStatusLoan"] = "Edit";
		}
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				clsCommon.UpdateByKey("LoanDetailID",hdLoanDetailID.Value.Trim(),"Update",this,"PR_spfrmLoanDetail");
				//clsCommon.UpdateByKey("LoanDetailID",hdLoanDetailID.Value.Trim(),"Update",this,"PR_spfrmLoanDetail");
				clsCommon.ClearControlValue(this);
				LoadDataGrid();
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}

		private void btnExport_Click(object sender, System.EventArgs e)
		{
			iHRPCore.DataGridExcelExporter myExcelXport=new DataGridExcelExporter(grdLoanDetail);			
			myExcelXport.Export("");
			myExcelXport =null;
		}

		private void btnList_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("Editpage.aspx?ModuleID=PR&ParentID=68&FunctionID=119&Ascx=MdlPR/LoanList.ascx");
		}
	}
}