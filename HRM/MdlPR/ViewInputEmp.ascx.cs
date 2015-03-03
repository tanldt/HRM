namespace iHRPCore.MdlPR
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.Com;
	//using iHRPCore.PRComponent;
	using System.Data.SqlClient;
	using System.Configuration;
	//using iHRPCore.HRComponent;
	using iHRPCore.Include;
	using iHRPCore;

	/// <summary>
	///		Summary description for ViewInputEmp.
	/// </summary>
	public class ViewInputEmp : System.Web.UI.UserControl
	{
#region Declare
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.LinkButton Linkbutton1;
		protected System.Web.UI.WebControls.LinkButton Linkbutton2;
		protected System.Web.UI.WebControls.LinkButton Linkbutton3;
		protected System.Web.UI.HtmlControls.HtmlTableRow trGrid;		
		#endregion Declare

		protected System.Web.UI.WebControls.TextBox txtAmountPerMonthPerMonth;
		protected System.Web.UI.WebControls.DataGrid dtgListFormula;
		protected System.Web.UI.WebControls.Label lblDescriptionSys;
		protected System.Web.UI.WebControls.DataGrid dtgSysItem;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.DataGrid dtgUserItem;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtMonth;
		protected System.Web.UI.WebControls.LinkButton btnView;
		protected System.Web.UI.WebControls.LinkButton btnCreate;
		protected System.Web.UI.WebControls.LinkButton btnCal;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label lblFormulaType;
		protected EmpHeader HR_EmpHeader;
		protected System.Web.UI.WebControls.TextBox Textbox1;
		/*protected System.Web.UI.WebControls.Label lblEffectivedate;
		protected System.Web.UI.WebControls.TextBox txtEffectivedate;*/

		public string strLanguage = "EN";
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			Ajax.Utility.RegisterTypeForAjax(typeof(ViewInputEmp)); // Su dung Ajax
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
			//txtLangID.Value= strLanguage;
			if(!Page.IsPostBack)
			{	
				txtMonth.Text = DateTime.Today.ToString("dd/MM/yyyy").Substring(3,7);
				lblFormulaType.Text = clsCommon.LookUpTable("PR_spCalPayroll @Activity = 'GetFormulabyEmp', @EmpID = '"+Session["EmpID"].ToString()+"'","NameFormula");
				BinItemDataSys();
				BinItemDataUser();
				string EmpCode = HR_EmpHeader.txtEmpID.Text;
				if (EmpCode != "")
					LoadDataGrid();
			}
			btnCal.Attributes.Add("OnClick", "return validform()");
			btnCreate.Attributes.Add("OnClick", "return validform()");
			btnDelete.Attributes.Add("OnClick", "return DeletePIT()");
			btnSave.Attributes.Add("OnClick", "return validform()");
			btnView.Attributes.Add("OnClick", "return validform()");
		}
		[Ajax.AjaxMethod]
		public bool CheckPayrollExists( string sMMYYYY)
		{
			//dt= clsDB.GetDataTable("PR_spfrmPAYROLL @Activity = 'CheckPayrollExists', @MMYYYY = '"+sMMYYYY+"'");
			DataRow dr = clsCommon.GetDataRow("PR_spfrmPAYROLL @Activity = 'CheckPayrollExists', @MMYYYY = '"+sMMYYYY+"'");
			if (dr != null)
			{
				if (dr["Result"].ToString() == "Yes")
					return true;
				else
					return false;
			}
			else
				return false;
			
			
		}
		private void BinItemDataSys()
		{
			DataTable dt = new DataTable();
			dt = clsDB.GetDataTableHasID("PR_spfrmSetFormula @Activity = 'GetSalaryItemDataSys'");
			dtgSysItem.DataSource = dt;
			dtgSysItem.DataBind();
		}
		private void BinItemDataUser()
		{
			DataTable dt = new DataTable();
			dt = clsDB.GetDataTableHasID("PR_spfrmSetFormula @Activity = 'GetSalaryItemDataUser'");
			dtgUserItem.DataSource = dt;
			dtgUserItem.DataBind();
		}
		private void LoadDataGrid()
		{
			lblErr.Text = "";
			DataTable dtList = new DataTable();
			try
			{
				dtList = clsPayroll.ViewDetailbyEmpID(txtMonth.Text, Session["EmpID"].ToString());
				dtgListFormula.DataSource = dtList;
				dtgListFormula.DataBind();

				for (int i = 0; i<this.dtgListFormula.Items.Count; i++)
				{
					TextBox txtValue = (TextBox)this.dtgListFormula.Items[i].FindControl("txtValue");
					string IsInput = dtgListFormula.Items[i].Cells[2].Text.Trim().Replace("&nbsp;",""); 
					if (IsInput == "1")
					{
						txtValue.Enabled = true;
					}
					else
						txtValue.Enabled = false;
					//cboTaxType.SelectedValue = LSSalaryItemTaxID;
				}
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
			this.btnView.Click += new System.EventHandler(this.btnView_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
			this.btnCal.Click += new System.EventHandler(this.btnCal_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnView_Click(object sender, System.EventArgs e)
		{
			LoadDataGrid();
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();			
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			try
			{
				
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "PR_spfrmInputPayrollData";
				#region save detail
				for(int i=0;i<dtgListFormula.Items.Count;i++)
				{
					string LSSalaryItemDataID = dtgListFormula.Items[i].Cells[0].Text.Trim();
					string IsInput = dtgListFormula.Items[i].Cells[2].Text.Trim().Replace("&nbsp;",""); 
					string sValue = ((TextBox)dtgListFormula.Items[i].FindControl("txtValue")).Text;
					string PRMonth = dtgListFormula.Items[i].Cells[4].Text.Trim();
					if (sValue == "")
						sValue = "0";
					if (IsInput == "1")
					{
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "SaveDetailParoll";
						
						cmd.Parameters.Add("@Month",SqlDbType.NVarChar,7).Value = PRMonth;
						cmd.Parameters.Add("@EmpID",SqlDbType.NVarChar,15).Value = Session["EmpID"].ToString();
						cmd.Parameters.Add("@LSSalaryItemDataID",SqlDbType.NVarChar,15).Value = LSSalaryItemDataID;
						cmd.Parameters.Add("@Value",SqlDbType.NVarChar,4000).Value = sValue;
				
						cmd.ExecuteNonQuery();
					}
				}
				#endregion
				sqlTran.Commit();
				clsChangeLang.popupWindow(this.Parent,"0044",strLanguage,"",1);
			}
			catch//(Exception ex)
			{
				sqlTran.Rollback();
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();
			}
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			clsCommon.Exc_CommandText( "PR_spfrmCreatePayrollData @Activity = 'DeleteByEmpID',@Month = '"+txtMonth.Text+"',@EmpID = '"+Session["EmpID"].ToString()+"'");
			clsChangeLang.popupWindow(this.Parent,"0044",strLanguage,"",1);
			LoadDataGrid();
		}

		private void btnCreate_Click(object sender, System.EventArgs e)
		{
			clsCommon.Exc_CommandText( "PR_spfrmCreatePayrollData @Activity = 'CreateNewPayroll',@Month = '"+txtMonth.Text+"'");
			clsCommon.Exc_CommandText( "PR_spfrmCreatePayrollData @Activity = 'Save',@Month = '"+txtMonth.Text+"',@EmpID = '"+Session["EmpID"].ToString()+"'");
			clsChangeLang.popupWindow(this.Parent,"0044",strLanguage,"",1);
			LoadDataGrid();
		}

		private void btnCal_Click(object sender, System.EventArgs e)
		{
			try
			{
			string sMonth = "",sErr = "";
			sMonth = txtMonth.Text;
			string EmpCode = HR_EmpHeader.txtEmpID.Text;

			sErr = clsPayroll.CalulateItemSYS(sMonth,"","","","",EmpCode, "","EN",Mession.GlbUser);

			if (sErr == "")
				sErr = clsPayroll.CalulateItemUser(sMonth,"","","","",EmpCode, "","EN",Mession.GlbUser,"");

			clsChangeLang.popupWindow(this.Parent,"0044",strLanguage,"",1);
			LoadDataGrid();

			lblErr.Text = sErr;
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}

		
	}
}
