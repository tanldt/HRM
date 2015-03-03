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
	public class Top1 : System.Web.UI.UserControl
	{
		public DataTable tbl = new DataTable();
		protected System.Web.UI.WebControls.LinkButton btnEnglish;
		protected System.Web.UI.WebControls.LinkButton btnVietnamese;
		protected System.Web.UI.WebControls.ImageButton btn;
		protected System.Web.UI.WebControls.ImageButton imgbtnLogo;
		protected System.Web.UI.WebControls.ImageButton imgbtnTop02;
		protected System.Web.UI.WebControls.ImageButton imgbtnHome;
		protected System.Web.UI.WebControls.ImageButton imgbtnLogout;
		protected System.Web.UI.WebControls.ImageButton imgbtnEN;
		protected System.Web.UI.WebControls.ImageButton imgbtnVN;
		protected System.Web.UI.WebControls.LinkButton btnLogout;
		protected System.Web.UI.WebControls.LinkButton btnChangePass;
		public int i;

		private void Page_Load(object sender, System.EventArgs e)
		{
			btn.Attributes["OnClick"] = "return silent()";
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
			this.imgbtnHome.Click += new System.Web.UI.ImageClickEventHandler(this.imgbtnHome_Click);
			this.imgbtnEN.Click += new System.Web.UI.ImageClickEventHandler(this.imgbtnEN_Click);
			this.imgbtnVN.Click += new System.Web.UI.ImageClickEventHandler(this.imgbtnVN_Click);
			this.btnChangePass.Click += new System.EventHandler(this.btnChangePass_Click);
			this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
			this.imgbtnLogo.Click += new System.Web.UI.ImageClickEventHandler(this.imgbtnLogo_Click);
			this.imgbtnTop02.Click += new System.Web.UI.ImageClickEventHandler(this.imgbtnTop02_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnHome_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Session.Remove("ModuleID");
			Response.Redirect(Request.ApplicationPath + "/Home.aspx");
		}

		private void imgbtnEN_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Session["LangID"] = "EN";
			///---Change language---------						
			string str_FrmName = Request.Params["Ascx"];
			if(Session["LangID"] != null)
				if(Session["LangID"].ToString().Trim() == "EN")	clsChangeLang.ChangePage(str_FrmName, this.Page, "EN");
				else clsChangeLang.ChangePage(str_FrmName, this.Page, "VN");				
			///---End Change language---------	
		}

		private void imgbtnHome_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Session.Remove("ModuleID");
			Response.Redirect(Request.ApplicationPath + "/Home.aspx");
		}

		private void imgbtnTop02_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			//string str_FrmName = this.Page.ToString().Substring(4,this.Page.ToString().Length-9);
			string str_FrmName = Request.Params["Ascx"];
			Session["Link_URL"] = Request.ServerVariables["QUERY_STRING"];
			this.Response.Redirect("UMS_frmCaption.aspx?ID="+str_FrmName);
		}

		private void imgbtnLogo_Click(object sender, System.Web.UI.ImageClickEventArgs e)
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
			clsCommon.Exc_CommandText("UpdateCaptionGeneral");
		}


		private void btnLogout_Click(object sender, System.EventArgs e)
		{
			Session.RemoveAll();
			Response.Redirect("Home.aspx");
		}

		private void imgbtnVN_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Session["LangID"] = "VN";
			///---Change language---------						
			string str_FrmName = Request.Params["Ascx"];
			if(Session["LangID"] != null)
				if(Session["LangID"].ToString().Trim() == "EN")	clsChangeLang.ChangePage(str_FrmName, this.Page, "EN");
				else clsChangeLang.ChangePage(str_FrmName, this.Page, "VN");				
			///---End Change language---------	
		}

		private void imgbtnLogout_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Session.RemoveAll();
			Response.Redirect("Home.aspx");
		}

		private void btnChangePass_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("Formpage.aspx?Ascx=SYS/SYS_ucChangePassword.ascx");
		}
	}
}
