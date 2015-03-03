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


	/// <summary>
	///		Summary description for LockPayroll1.
	/// </summary>
	public class LockPayroll1 : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.DataGrid dtgList;
		protected System.Web.UI.WebControls.Button cmdSearch;
		protected System.Web.UI.WebControls.Button cmdSave;
		protected System.Web.UI.WebControls.TextBox txtMonth;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if (!Page.IsPostBack)
			{
				string MMYYYY=Request.Params["MMYYYY"];
				txtMonth.Text=MMYYYY;
				BinDataGrid();
			}
			cmdSearch.Attributes.Add("OnClick", "return checkvalidSearch()");	
			cmdSave.Attributes.Add("OnClick", "return Lock()");	
		}
		private void BinDataGrid()
		{
			string MMYYYY = txtMonth.Text;
			DataTable dt = clsDB.GetDataTable("PR_spPayrollCollection @Activity='GetCompanyLock',@UserGroupID = '"+Mession.GlbUser
				+"',@Month = '"+MMYYYY+"',@LSCompanyID='"+Mession.GlbCusGroupID
				+"'");
			dtgList.DataSource = dt;
			dtgList.DataBind();

			for (int i = 0; i<this.dtgList.Items.Count; i++)
			{
				CheckBox chkSelect = (CheckBox)this.dtgList.Items[i].FindControl("chkSelect");
				
				bool blisLock = Convert.ToBoolean(dtgList.Items[i].Cells[2].Text.Trim()); 
				if (blisLock == true)
				{
					chkSelect.Checked = blisLock;
					chkSelect.Enabled = false;
				}
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
			this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
			this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void cmdSearch_Click(object sender, System.EventArgs e)
		{
			BinDataGrid();
		}

		private void cmdSave_Click(object sender, System.EventArgs e)
		{
			string sErr = clsPayroll.sLockSalary(dtgList);
			if (sErr=="")
			{
				clsChangeLang.popupWindow(this.Parent,"0044",Mession.GlbLangID,"",1);
				BinDataGrid();
			}
			else
			{
				clsChangeLang.popupWindowCataLog(this.Parent,sErr);
			}
		}
	}
}
