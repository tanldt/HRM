namespace MdlTMS
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.Include;
	using GridSort;
	using System.Data.SqlClient;
	using System.Configuration;
	using iHRPCore;
	using iHRPCore.Com;


	/// <summary>
	///		Summary description for ExchangeRate.
	/// </summary>
	public class ExchangeRate : System.Web.UI.UserControl
	{
		#region Declaration
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnSearch;
		protected System.Web.UI.WebControls.TextBox txtEmpID;
		protected System.Web.UI.WebControls.LinkButton btnCancel;
		protected System.Web.UI.WebControls.TextBox txtTypeDate;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.DropDownList DropDownList1;
		public string strLanguage="EN";
		protected System.Web.UI.WebControls.RadioButtonList RadStatus;
		protected System.Web.UI.WebControls.DataGrid dtg;
		protected System.Web.UI.WebControls.DataGrid dtgList;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.DropDownList cboCompanyID;
		protected EmpHeaderSearch EmpHeaderSearch1;
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				clsCommon.LoadDropDownListControl(cboCompanyID,"PR_spfrmExchangeRate @Activity='GetCompanyList',@UserGroupID=N'" + Mession.GlbUser + "'","LSLevel1ID","Name",true);
				LoadDefaultData();
			}

			this.btnSave.Attributes.Add("OnClick", "return CheckSave()");
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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.cboCompanyID.SelectedIndexChanged += new System.EventHandler(this.cboCompanyID_SelectedIndexChanged);
			this.dtgList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgList_PageIndexChanged_1);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		private void LoadDefaultData()
		{
			//this.txtMonthYear.Text = DateTime.Today.ToString("MM/yyyy");
			DataTable dt = new DataTable();
			dtgList.DataSource = dt;
			dtgList.DataBind();
			
		}
		public static string sImpact(DataGrid dtgList)
		{
			string sErrMess="";
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			SQLconn.Open();
			try
			{
				
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "PR_spfrmExchangeRate";
   
				CheckBox obj = new CheckBox();
				for (int i = 0;i<= dtgList.Items.Count - 1;i++)
				{
//					obj = (CheckBox) dtgList.Items[i].FindControl("chkSelect");					
//					if (obj.Checked)
//					{
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "Save";					
						
						cmd.Parameters.Add("@LSLevel1ID", SqlDbType.NVarChar,12).Value = dtgList.Items[i].Cells[0].Text.Trim(); 
						cmd.Parameters.Add("@LSCurrencyTypeID", SqlDbType.NVarChar,12).Value = dtgList.Items[i].Cells[1].Text.Trim(); 

						string Value = ((TextBox)dtgList.Items[i].FindControl("txtValue")).Text; 
						
						if (Value == "") cmd.Parameters.Add("@Value", SqlDbType.Float).Value = DBNull.Value;
						else cmd.Parameters.Add("@Value", SqlDbType.Float).Value = Value;

					string ValueSI = ((TextBox)dtgList.Items[i].FindControl("txtValueSI")).Text; 
						
					if (ValueSI == "") cmd.Parameters.Add("@ValueSI", SqlDbType.Float).Value = DBNull.Value;
					else cmd.Parameters.Add("@ValueSI", SqlDbType.Float).Value = ValueSI;
						
						cmd.Parameters.Add("@ReturnMess", SqlDbType.NVarChar,1000).Direction = ParameterDirection.InputOutput;
						cmd.ExecuteNonQuery();							
					//}
				}
				sErrMess= cmd.Parameters["@ReturnMess"].Value.ToString();
				cmd.Transaction.Commit();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();	
				if (sErrMess!="")
					return sErrMess;
				else
					return "";
				
			}
			catch (Exception exp)
			{
				string strErr = exp.Message;
				cmd.Transaction.Rollback();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				return strErr;
			}
		}
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			string sErr = sImpact(dtgList);
			if (sErr=="")
			{
				clsChangeLang.popupWindow(this.Parent,"0044",strLanguage,"",1);
				BindDataGrid(true);
			}
			else
			{
				clsChangeLang.popupWindowCataLog(this.Parent,sErr);
			}
		}

		private void BindDataGrid(bool Status)
		{	
			this.dtgList.DataSource = null;
			try
			{
				DataTable dt = new DataTable();
				if (Status)
				{

					dt = clsCommon.GetDataTable("[PR_spfrmExchangeRate] @Activity = 'GetListCurrency',@LSLevel1ID = '"+cboCompanyID.SelectedValue+"'");
					Mession.GlbSearchData = dt;
					this.dtgList.CurrentPageIndex = 0;
				}
				else
					dt = Mession.GlbSearchData;
				dtgList.DataSource = dt;
				dtgList.DataBind();
			}
			catch(Exception exp)
			{
				this.lblErr.Text = exp.Message.ToString();
			}
		}

		private void dtgList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.dtgList.CurrentPageIndex = e.NewPageIndex;
			BindDataGrid(false);
		}

		private void cboCompanyID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		BindDataGrid(true);
		}

		private void dtgList_PageIndexChanged_1(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.dtgList.CurrentPageIndex = e.NewPageIndex;
			BindDataGrid(false);
		}
	}
}

