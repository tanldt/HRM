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
	///		Summary description for SetFormulaForCom.
	/// </summary>
	public class SetFormulaForCom : System.Web.UI.UserControl
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
		protected EmpHeaderSearch EmpHeaderSearch1;
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				BindDataGrid(true);
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
			this.dtgList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgList_PageIndexChanged);
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
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			string sErr = clsSetFormula.sImpact(dtgList);
			if (sErr=="")
			{
				clsChangeLang.popupWindow(this.Parent,"0044",Mession.GlbLangID,"",1);
				BindDataGrid(true);
				iHRPCore.Com.clsCommon.Exc_CommandText("[UMS_sptblAccessCommon] @Activity='UpdateFormulaPR', @UserGroupID = '" 
					+ Session["AccountLogin"].ToString() + "',@LSCompanyID = '" + Mession.GlbCusGroupID + "'" );
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

					dt = clsSetFormula.GetCompanyList(Mession.GlbUser);
					Mession.GlbSearchData = dt;
					this.dtgList.CurrentPageIndex = 0;
				}
				else
					dt = Mession.GlbSearchData;
				dtgList.DataSource = dt;
				dtgList.DataBind();

				for (int i = 0; i<this.dtgList.Items.Count; i++)
				{
					DropDownList cboFormula = (DropDownList)this.dtgList.Items[i].FindControl("cboFormula");
					
					cboFormula.Items.Clear();	
					clsCommon.LoadDropDownListControl(cboFormula, "sp_GetDataCombo @TableName='PR_tblSetFormula',@Fields='SetFormulaID as [ID],NameFormula as Name'", "ID", "Name",true);
					
					string sSetFormulaID = dtgList.Items[i].Cells[1].Text.Trim().Replace("&nbsp;","");
					if (sSetFormulaID != "0")
						cboFormula.SelectedValue = sSetFormulaID;
					
				}
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
	}
}

