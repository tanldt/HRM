namespace iHRPCore.Pagelets
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Data.SqlClient;
	using iHRPCore.Com;
	using iHRPCore.HRComponent;

	/// <summary>
	///		Summary description for Top.
	/// </summary>
	public class Top : System.Web.UI.UserControl
	{
		public DataTable tbl = new DataTable();
		protected System.Web.UI.WebControls.LinkButton btnEnglish;
		protected System.Web.UI.WebControls.LinkButton btnVietnamese;
		protected System.Web.UI.WebControls.LinkButton btnLogout;
		protected System.Web.UI.WebControls.LinkButton btnHome;
		protected System.Web.UI.WebControls.LinkButton btnVN;
		protected System.Web.UI.WebControls.LinkButton btnEN;
		protected System.Web.UI.WebControls.LinkButton btnLogo;
		protected System.Web.UI.WebControls.LinkButton btnTop02;
		public int i;

		private void Page_Load(object sender, System.EventArgs e)
		{
			btnTop02.Attributes.Add("Onclick","return false;");
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
			this.btnLogo.Click += new System.EventHandler(this.btnLogo_Click);
			this.btnTop02.Click += new System.EventHandler(this.btnTop02_Click);
			this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
			this.btnVN.Click += new System.EventHandler(this.btnVN_Click);
			this.btnEN.Click += new System.EventHandler(this.btnEN_Click);
			this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnHome_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Session.Remove("ModuleID");
			Response.Redirect(Request.ApplicationPath + "/Home.aspx");
		}

		private void btnLogo_Click(object sender, System.EventArgs e)
		{
			string strReturn ="";
			/*string str_FrmName = this.Page.ToString().Substring(4,this.Page.ToString().Length-9);
			if (Request.Params["Ascx"] != null)
				str_FrmName = Request.Params["Ascx"];*/
			string str_FrmName = Request.Params["Ascx"];
			foreach(Control Child_ctl in Page.Controls)
			{
				strReturn =  clsChangeLang.GetAllControlOfForm(Child_ctl, str_FrmName);
			}
		}

		private void btnTop02_Click(object sender, System.EventArgs e)
		{
			//string str_FrmName = this.Page.ToString().Substring(4,this.Page.ToString().Length-9);
			string str_FrmName = Request.Params["Ascx"];
			Session["Link_URL"] = Request.ServerVariables["QUERY_STRING"];
			this.Response.Redirect("UMS_frmCaption.aspx?ID="+str_FrmName);
		}

		private void btnEN_Click(object sender, System.EventArgs e)
		{
			Session["LangID"] = "EN";
			///---Change language---------						
			string str_FrmName = Request.Params["Ascx"];
			if(Session["LangID"] != null)
				if(Session["LangID"].ToString().Trim() == "EN")	clsChangeLang.ChangePage(str_FrmName, this.Page, "EN");
				else clsChangeLang.ChangePage(str_FrmName, this.Page, "VN");				
			///---End Change language---------	
		}

		private void btnVN_Click(object sender, System.EventArgs e)
		{
			Session["LangID"] = "VN";
			///---Change language---------						
			string str_FrmName = Request.Params["Ascx"];
			if(Session["LangID"] != null)
				if(Session["LangID"].ToString().Trim() == "EN")	clsChangeLang.ChangePage(str_FrmName, this.Page, "EN");
				else clsChangeLang.ChangePage(str_FrmName, this.Page, "VN");				
			///---End Change language---------	
		}

		private void btnLogout_Click(object sender, System.EventArgs e)
		{
			Session.RemoveAll();
			Response.Redirect("Home.aspx");
		}

		private void btnHome_Click(object sender, System.EventArgs e)
		{
			Session.Remove("ModuleID");
			Response.Redirect(Request.ApplicationPath + "/Home.aspx");
		}
	}
}
