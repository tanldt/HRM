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
	///		Summary description for ApprovedPayroll.
	/// </summary>
	public class ApprovedPayroll : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.DataGrid dtgList;
		protected System.Web.UI.WebControls.Button cmdSearch;
		protected System.Web.UI.WebControls.Button cmdSave;
		protected System.Web.UI.WebControls.Button cmdClose;
		protected System.Web.UI.WebControls.TextBox txtMonth;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if (!Page.IsPostBack)
			{
				string MMYYYY = DateTime.Today.ToString("dd/MM/yyyy").Substring(3,7);
				txtMonth.Text=MMYYYY;
				BinDataGrid();
			}
			cmdSearch.Attributes.Add("OnClick", "return checkvalidSearch()");	
			cmdSave.Attributes.Add("OnClick", "return Lock()");	
			cmdClose.Attributes.Add("OnClick", "javascript:window.close();");	
		}
		private void BinDataGrid()
		{
			string MMYYYY = txtMonth.Text;
			DataTable dt = clsDB.GetDataTable("PR_spfrmApprovedParoll @Activity='GetCompanyLock',@UserGroupID = '"+Mession.GlbUser
				+"',@Month = '"+MMYYYY+"',@LSCompanyID='"+Mession.GlbCusGroupID
				+"'");
			dtgList.DataSource = dt;
			dtgList.DataBind();

			DataTable dtUserID = new DataTable();
			dtUserID = clsCommon.GetDataTable("PR_spfrmApprovedParoll @Activity='cboUserIDApproved',@UserGroupID = '"+Mession.GlbUser+"'");
			for (int i = 0; i<this.dtgList.Items.Count; i++)
			{
				CheckBox chkSelect = (CheckBox)this.dtgList.Items[i].FindControl("chkSelect");
				
				bool blisLock = Convert.ToBoolean(dtgList.Items[i].Cells[2].Text.Trim()); 
				
				//string  sCompanyID = dtgList.Items[i].Cells[0].Text.Trim(); 
				DropDownList cboNextUserID = (DropDownList)this.dtgList.Items[i].FindControl("cboNextUserID");

				cboNextUserID.Items.Clear();	
//				clsCommon.LoadDropDownListControl(cboNextUserID, "PR_spfrmApprovedParoll @Activity='NextApproved', @MMYYYY = '" + MMYYYY +"',@CompanyID = '"+sCompanyID+"'", "ID", "Name",true);

				clsCommon.LoadDropDownListControl(cboNextUserID,dtUserID , "ID", "Name",true);
					
				string sUserID = dtgList.Items[i].Cells[4].Text.Trim().Replace("&nbsp;","");
				if (sUserID != "0")
					cboNextUserID.SelectedValue = sUserID;

				if (blisLock == true)
				{
					chkSelect.Checked = blisLock;
					chkSelect.Enabled = false;

					cboNextUserID.Enabled = false;
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
			string sErr = clsPayroll.sSendApproved(dtgList);
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
