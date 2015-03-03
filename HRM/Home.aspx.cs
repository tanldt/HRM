using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using iHRPCore.Com;
using iHRPCore.UMSComponent;
using iHRPCore.HRComponent;
using ConnectData;
using iHRPCore.Include;
using GridSort;

namespace iHRPCore
{
	/// <summary>
	/// Summary description for Home.
	/// </summary>
	public class Home : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox TextBox1;
		public DataTable tblModule = new DataTable();
		public int i;
		protected System.Web.UI.WebControls.LinkButton btnLogout;
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
		protected System.Web.UI.WebControls.Image Image1;
		protected System.Web.UI.WebControls.Image Image2;
		protected System.Web.UI.WebControls.Image Image3;
		protected FPTToolWeb.HitCounter.HitCounter HitCounter1;
		string strLanguage = "";
	
		private void Page_Load(object sender, System.EventArgs e)
		{

			/* ThanhNV - Protal*/
			 
			if (Request.Params["key"]!=null)
			{
				clsSYS_tblAppUser myDB = new clsSYS_tblAppUser();
				myDB.KeyChecked = Request.Params["key"].ToString().Trim();
				bool allowed = false;

				//Check userID on SYS_tblAppUser table by keyChecked and this Application which access
				if (myDB.CheckedByKey())
				{
					//Check userID only is existing this database and check Role permission

					/*User userToLogin = new User();
					bool isAdmin = (myDB.isAdmin=="True"?true:false);
					if (isAdmin)
						userToLogin.Username = "administrator";
					else
						userToLogin.Username = myDB.UserID;*/
					

						//Checked done, must be deleted this record with general key
						Session["AccountLogin"]=myDB.UserID;				
						this.txtUserName.Text=myDB.UserID;				
						myDB.Delete();

					if (!this.txtUserName.Text.Trim().Equals("admin"))
					{
						DataTable dtData = clsHREmpList.GetEmpList(this.Page,1);
						if (dtData!=null && dtData.Rows.Count > 0)
						{
							Session["EmpID"]=dtData.Rows[0]["EmpID"].ToString().Trim();
						}
					
					}
					
				}
				myDB = null;
				//Checked this UserID is passed
				//Response.Redirect(Request.ApplicationPath + "/default.aspx");				
                
				Response.Redirect("Home.aspx");
				
			}
			 
			////////////////////////////////////////////////////////////////////////////////// 


			this.btnLogin.Click += new System.Web.UI.ImageClickEventHandler(this.btnLogin_Click);
			this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
			
			Session["LangID"] = "EN";
			strLanguage = Session["LangID"] != null? Session["LangID"].ToString().Trim():"VN";
			if (!Page.IsPostBack)
			{
				string strTextField = strLanguage == "VN"?"VNName":"Name";
				clsHREmpList.LoadComboCompany(cboCompany,strTextField, strLanguage,this.Page);

				tblModule = iHRPCore.Com.clsCommon.GetDataTable("sp_clsCommon 'GetModuleList',@Language='" 
					+ strLanguage + "', @UserGroupID='" + (Session["AccountLogin"] != null?Session["AccountLogin"].ToString():"") + "'");
				if (Session["AccountLogin"] != null)
					this.btnLogout.Text = "Log out (" + Session["AccountLogin"].ToString() + ")";

//				if (Request.Cookies["AccountLogin"] != null)
//					this.txtUserName.Text= Server.HtmlEncode(Request.Cookies["AccountLogin"].Value);

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
            

			//Session["AccountLogin"] = "admin";
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
			this.Load += new System.EventHandler(this.Page_Load);
            //remind
            clsChangeLang.popupWindowReminder(this.Parent,"asasdas","",2);          
            //

		}
		#endregion

		private void btnLogin_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			int intCheckLogined = clsUMS.CheckAccountLogin(this.txtUserName.Text.Trim(), FPTToolWeb.Encrypt.Encryption.Encrypt(this.txtPassword.Text.Trim()),this);
			
			if (intCheckLogined == 1)
			{
				DataCache.RemoveCache("UserPerm_" + this.txtUserName.Text.Trim());

				Session["AccountLogin"] = this.txtUserName.Text.Trim();
				Session["CompanyID"] = this.cboCompany.SelectedValue;
				
				clsTermination.Pre_Update();
				//if (Request.Params["Url"] != null)
				//	Response.Redirect(Request.Params["Url"].Trim());
				//else
				//	Response.Redirect("Home.aspx");
				DataTable dtData = clsHREmpList.GetUserAccount();
				if (dtData!=null && dtData.Rows.Count > 0)
				{
					Session["EmpID"]=dtData.Rows[0]["EmpID"].ToString().Trim();
					Mession.GlbEmpIDLogin = dtData.Rows[0]["EmpID"].ToString().Trim();
					Mession.GlbCusGroupID = dtData.Rows[0]["LSCompanyID"].ToString().Trim();
				}
				// Xu ly chung khi dang nhap
				iHRPCore.Com.clsCommon.Exc_CommandText("[UMS_sptblAccessCommon] @Activity='PermissionData', @UserGroupID = '" 
					+ Session["AccountLogin"].ToString() + "',@LSCompanyID = '" + Mession.GlbCusGroupID + "'" );
				iHRPCore.Com.clsCommon.Exc_CommandText("[UMS_sptblAccessCommon] @Activity='UpdateFormulaPR', @UserGroupID = '" 
					+ Session["AccountLogin"].ToString() + "',@LSCompanyID = '" + Mession.GlbCusGroupID + "'" );
				//end xu ly chung

				Response.Redirect("Home.aspx");
					
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

//			if (Request.Params["Url"] != null)
//				Response.Redirect("Home.aspx");
//			else
//				Response.Redirect("Home.aspx");
		}

		private void btnLogout_Click(object sender, System.EventArgs e)
		{
			Session.RemoveAll();
			Response.Redirect("Home.aspx");
		}

		
	}
}
