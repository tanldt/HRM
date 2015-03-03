namespace iHRPCore.SYS
{
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
	using System.Data.SqlClient;
	using System.Configuration;
	using iHRPCore.Com;
	using iHRPCore.Component;

	/// <summary>
	///		Summary description for SYS_ucChangePassword.
	/// </summary>
	public class SYS_ucChangePassword : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblCurUserCaption;
		protected System.Web.UI.WebControls.Label lblCurUser;
		protected System.Web.UI.WebControls.Label lblOldPassCaption;
		protected System.Web.UI.WebControls.TextBox txtOldPass;
		protected System.Web.UI.WebControls.Label lblNewPassCaption;
		protected System.Web.UI.WebControls.TextBox txtNewPass;
		protected System.Web.UI.WebControls.Label lblConfirmPassCaption;
		protected System.Web.UI.WebControls.TextBox txtConfirmPass;
		protected System.Web.UI.WebControls.Panel Panel1;
		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnCancel;
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label lblUserName;
		public string strLanguage="VN";

		private void Page_Load(object sender, System.EventArgs e)
		{
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";	
			if (!Page.IsPostBack)
			{
				this.lblCurUser.Text = Convert.ToString(Session["AccountLogin"]);
				//this.lblUserName.Text= clsChangeLang.getEmpFromAccount(lblCurUser.Text,2);
			}
			this.btnSave.Attributes.Add("OnClick","return Checkform()");
			//this.btnClose.Attributes.Add("OnClick","Close();");
			//btnClose.Attributes.Add("OnClick","window.close()");
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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region Creater: LANHTD, 23/09/04, CheckValidData
		private bool CheckValidData()
		{
			try
			{
				DataTable mTable = clsCommon.GetDataTable("UMS_sptblUserAccount 'GetPass', @result='',@UserGroupID='" + this.lblCurUser.Text.Trim() + "',@Password='" + FPTToolWeb.Encrypt.Encryption.Encrypt(this.txtOldPass.Text.Trim()) + "'");
				if (mTable.Rows.Count == 0)
				{
					clsChangeLang.popupWindow(this.Parent,"SCP_0001",strLanguage,"",0);
					return false;
				}
				return true;
			}
			catch(Exception exp)
			{
				clsChangeLang.popupWindowExp(this.Parent,exp);
				return false;
			}
		}
		#endregion

		#region Creater: LANHTD, 23/09/04, btnSave_Click
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			string strresult = "";
			try
			{
				if (!this.CheckValidData())
					return;
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "UMS_sptblUserAccount";
				cmd.Parameters.Clear();
				SqlParameter param;
			
				try
				{
					cmd.Parameters.Clear();
					param = cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,30);
					param.Value = "UpdatePassword";
					param = cmd.Parameters.Add("@result",SqlDbType.NVarChar,20);
					param.Direction = ParameterDirection.Output;
					param = cmd.Parameters.Add("@UserGroupID",SqlDbType.NVarChar,20);
					param.Value = this.lblCurUser.Text.Trim();
					param = cmd.Parameters.Add("@NewPassword",SqlDbType.NVarChar,50);
					param.Value = FPTToolWeb.Encrypt.Encryption.Encrypt(this.txtNewPass.Text.Trim());
					//string sPassword = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(this.txtNewPass.Text,"md5");
					//param.Value = sPassword;
					cmd.ExecuteNonQuery();
					strresult = Convert.ToString(cmd.Parameters["@result"].Value);
					if (strresult == "-5")
					{
						cmd.Transaction.Rollback();
						this.lblError.Text = "Error occurance!";
						clsChangeLang.popupWindow(this.Parent,"0034",strLanguage,"",0);
						return;
					}
				}
				catch(Exception exp)
				{
					clsChangeLang.popupWindowExp(this.Parent,exp);
					cmd.Transaction.Rollback();
					return;
				}
				cmd.Transaction.Commit();
				
				Session.RemoveAll();
				Response.Redirect("Home.aspx");
			}
			catch(Exception exp)
			{
				clsChangeLang.popupWindowExp(this.Parent,exp);
			}
			finally
			{
				cmd.Dispose();
				SQLconn.Dispose();
			}
		}
		#endregion

		#region Creater: LANHTD, 23/09/04, btnCancel_Click
		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("Home.aspx");
		}
		#endregion

		private void btnBack_Click(object sender, System.EventArgs e)
		{
			//Session.RemoveAll();
			Response.Redirect("Home.aspx");
		}
	}
}
