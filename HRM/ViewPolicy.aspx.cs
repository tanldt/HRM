using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Xml;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using iHRPCore.Com;
using iHRPCore.UMSComponent;
using iHRPCore.HRComponent;

using System.Configuration;

namespace iHRPCore
{
	/// <summary>
	/// Summary description for ViewPolicy.
	/// </summary>
	public class ViewPolicy : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList cboCompany;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.TextBox txtUserName;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.TextBox txtPassword;
		protected System.Web.UI.WebControls.ImageButton btnLogin;
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.LinkButton btnLogout;
		string strLanguage = "";
		public int i;
		protected System.Web.UI.WebControls.DataList ListPolicy;
		protected System.Web.UI.WebControls.Label lblFormPolicy;
		protected System.Web.UI.WebControls.LinkButton btnForm;
		protected System.Web.UI.WebControls.LinkButton btnPolicy;
		public DataTable tblModule = new DataTable();
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			this.btnLogin.Click += new System.Web.UI.ImageClickEventHandler(this.btnLogin_Click);
			this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
			
			if (Request.Params["UID"] != null && Request.Params["PWD"] != null)
			{
				this.txtUserName.Text = Request.Params["UID"].Trim();
				this.txtPassword.Text = Request.Params["PWD"].Trim();
				btnLogin_Click(null,null);
			}

			Session["LangID"] = "EN";
			strLanguage = Session["LangID"] != null? Session["LangID"].ToString().Trim():"VN";
			string strAccountLogin = Session["AccountLogin"] != null? Session["AccountLogin"].ToString().Trim():"admin";
			//tanldt - update system
			DataCache.RemoveCache("TopMenu_" + strAccountLogin);
			DataCache.RemoveCache("UserPerm_" + strAccountLogin);
			DataCache.RemoveCache("UserPermPopup_" + strAccountLogin);
			//tanldt - update system
			if (!Page.IsPostBack)
			{
				LoadDataGrid();
				string strTextField = strLanguage == "VN"?"VNName":"Name";
				clsHREmpList.LoadComboCompany(cboCompany,strTextField, strLanguage,this.Page);

				tblModule = clsCommon.GetDataTable("sp_clsCommon 'GetModuleList',@Language='" 
					+ strLanguage + "', @UserGroupID='" + (Session["AccountLogin"] != null?Session["AccountLogin"].ToString():"") + "'");
				if (Session["AccountLogin"] != null)
					this.btnLogout.Text = "Log out (" + Session["AccountLogin"].ToString() + ")";

				if (Request.Cookies["AccountLogin"] != null)
					this.txtUserName.Text= Server.HtmlEncode(Request.Cookies["AccountLogin"].Value);

				try
				{
					if (Request.Cookies["CompanyID"] != null)
						cboCompany.SelectedValue = Server.HtmlEncode(Request.Cookies["CompanyID"].Value);
				}
				catch(Exception exp)
				{}

			};

			if (cboCompany.Items.Count==2) cboCompany.SelectedIndex=1;
			this.btnLogin.Attributes.Add("OnClick","return CheckLogin()");

			string url = Request.MapPath("XMLTree.xml").ToString();
			DataTable dt = new DataTable();
			if (Request["GroupID"]!=null)
			{
				 dt = clsCommon.GetDataTable("SYS_spfrmPOLICY @Activity = 'GetForGrid_',@Type='1',@LSGroupPolicyID='" + Request["GroupID"].ToString() + "'");
			}
			else
			{
				 dt = clsCommon.GetDataTable("SYS_spfrmPOLICY @Activity = 'GetForGrid_',@Type='1'");
			}
			ListPolicy.DataSource=dt;
			ListPolicy.DataBind();
			
		}

		private void LoadDataGrid()
		{
			
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{  
			this.btnForm.Click += new System.EventHandler(this.btnForm_Click);
			this.btnPolicy.Click += new System.EventHandler(this.btnPolicy_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		private void btnLogin_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			int intCheckLogined = clsUMS.CheckAccountLogin(this.txtUserName.Text.Trim(), FPTToolWeb.Encrypt.Encryption.Encrypt(this.txtPassword.Text.Trim()),this);
			if (intCheckLogined == 1)
			{
				Session["AccountLogin"] = this.txtUserName.Text.Trim();
				Session["CompanyID"] = this.cboCompany.SelectedValue;

				//if (Request.Params["Url"] != null)
				//	Response.Redirect(Request.Params["Url"].Trim());
				//else
				//	Response.Redirect("Home.aspx");

				if (!this.txtUserName.Text.Trim().Equals("admin"))
				{
					DataTable dtData = clsHREmpList.GetEmpList(this.Page,1);
					if (dtData.Rows.Count > 0)
					{
						Session["EmpID"]=dtData.Rows[0]["EmpID"].ToString().Trim();
					}
				}
			}
			else
			{
				this.lblErr.Text = "Username or password is invalid!";
			}

			if (Response.Cookies["CompanyID"] != null)
			{
				HttpCookie aCookie = new HttpCookie("CompanyID",this.cboCompany.SelectedValue);
				aCookie.Expires = DateTime.Now.AddMonths(1);
				Response.Cookies.Add(aCookie);
				aCookie=null;
			}
			else
				Response.Cookies ["CompanyID"].Value =this.cboCompany.SelectedValue;

			if (Request.Params["Url"] != null)
				Response.Redirect(Request.Params["Url"].Trim());
			else
				Response.Redirect("Home.aspx");
		}

		private void btnLogout_Click(object sender, System.EventArgs e)
		{
			Session.RemoveAll();
			Response.Redirect("Home.aspx");
		}

		private void btnForm_Click(object sender, System.EventArgs e)
		{
			DataTable dt = clsCommon.GetDataTable("SYS_spfrmPOLICY @Activity = 'GetForGrid_',@Type='0'");
			ListPolicy.DataSource=dt;
			ListPolicy.DataBind();

			LoadDataGrid();
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			clsHREmpList.LoadComboCompany(cboCompany,strTextField, strLanguage,this.Page);

			tblModule = clsCommon.GetDataTable("sp_clsCommon 'GetModuleList',@Language='" 
				+ strLanguage + "', @UserGroupID='" + (Session["AccountLogin"] != null?Session["AccountLogin"].ToString():"") + "'");
			if (Session["AccountLogin"] != null)
				this.btnLogout.Text = "Log out (" + Session["AccountLogin"].ToString() + ")";

			if (Request.Cookies["AccountLogin"] != null)
				this.txtUserName.Text= Server.HtmlEncode(Request.Cookies["AccountLogin"].Value);

			try
			{
				if (Request.Cookies["CompanyID"] != null)
					cboCompany.SelectedValue = Server.HtmlEncode(Request.Cookies["CompanyID"].Value);
			}
			catch(Exception exp)
			{}
		}

		private void btnPolicy_Click(object sender, System.EventArgs e)
		{
			DataTable dt = clsCommon.GetDataTable("SYS_spfrmPOLICY @Activity = 'GetForGrid_',@Type='1'");
			ListPolicy.DataSource=dt;
			ListPolicy.DataBind();

				

			LoadDataGrid();
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			clsHREmpList.LoadComboCompany(cboCompany,strTextField, strLanguage,this.Page);

			tblModule = clsCommon.GetDataTable("sp_clsCommon 'GetModuleList',@Language='" 
				+ strLanguage + "', @UserGroupID='" + (Session["AccountLogin"] != null?Session["AccountLogin"].ToString():"") + "'");
			if (Session["AccountLogin"] != null)
				this.btnLogout.Text = "Log out (" + Session["AccountLogin"].ToString() + ")";

			if (Request.Cookies["AccountLogin"] != null)
				this.txtUserName.Text= Server.HtmlEncode(Request.Cookies["AccountLogin"].Value);

			try
			{
				if (Request.Cookies["CompanyID"] != null)
					cboCompany.SelectedValue = Server.HtmlEncode(Request.Cookies["CompanyID"].Value);
			}
			catch(Exception exp)
			{}
		}

	}
}
