namespace iHRPCore.Include
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.Com;
	using iHRPCore.HRComponent;
	using iHRPCore.REComponent;

	/// <summary>
	///		Summary description for CanHeader.
	/// </summary>
	public class CanHeader : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblCandidateID;
		protected System.Web.UI.WebControls.Label lblCandidateName;
		protected System.Web.UI.WebControls.TextBox txtCandidateName;
		protected System.Web.UI.WebControls.Label lblDOB;
		protected System.Web.UI.WebControls.Label lblApplyForJobDate;
		protected System.Web.UI.WebControls.Label lblExpectationJobTitle;
		protected System.Web.UI.WebControls.Label lblStatus;
		protected System.Web.UI.WebControls.Label lblMajor;
		protected System.Web.UI.WebControls.Label lblExperience;
		protected System.Web.UI.WebControls.Label lblDOB_Data;
		protected System.Web.UI.WebControls.Label lblApplyForJobDate_Data;
		protected System.Web.UI.WebControls.Label lblExpectationJobTitle_Data;
		protected System.Web.UI.WebControls.Label lblStatus_Data;
		protected System.Web.UI.WebControls.Label lblMajor_Data;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLengthCanID;
		protected System.Web.UI.WebControls.TextBox txtCandidateCode;
		protected System.Web.UI.WebControls.Label lblExperience_Data;
		public string strLanguage = "EN";

		private void Page_Load(object sender, System.EventArgs e)
		{
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
			if (!Page.IsPostBack)
			{
					DataTable tbl = new DataTable();
					tbl = clsRECanList.GetEmpIDLength();
					if (tbl.Rows.Count > 0)
						this.txtLengthCanID.Value = tbl.Rows[0][0].ToString().Trim();
					string strCanID = Request.Params["CanID"] != null? Request.Params["CanID"].Trim():(Session["CanID"] != null? Session["CanID"].ToString():"");
					if (strCanID != "")
					{
						tbl = clsRECanList.GetCanList(strCanID,this.Page);
						if (tbl.Rows.Count > 0)
						{
							this.txtCandidateCode.Text = tbl.Rows[0]["CandidateCode"].ToString().Trim();
							this.txtCandidateCode.ToolTip = tbl.Rows[0]["CandidateCode"].ToString().Trim();
							this.txtCandidateName.Text = tbl.Rows[0]["CandidateName"].ToString().Trim();
							this.txtCandidateName.ToolTip = tbl.Rows[0]["CandidateName"].ToString().Trim();
							this.lblDOB_Data.Text = tbl.Rows[0]["DOB_D"].ToString().Trim();
							this.lblApplyForJobDate_Data.Text = tbl.Rows[0]["ApplyForJobDate_D"].ToString().Trim();
							this.lblExpectationJobTitle_Data.Text = tbl.Rows[0]["JobTitle"].ToString().Trim();
							this.lblMajor_Data.Text = tbl.Rows[0]["Major"].ToString().Trim();
							string str=strLanguage=="VN"? "tháng":"months";
							this.lblExperience_Data.Text = tbl.Rows[0]["Experience"].ToString().Trim()+ ' ' + 	str	;				
							this.lblStatus_Data.Text=tbl.Rows[0]["Status_D"].ToString().Trim();						
						}
					}
					tbl.Dispose();
							
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
