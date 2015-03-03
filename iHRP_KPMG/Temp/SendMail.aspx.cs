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
using System.Configuration ;
using FPTToolWeb.Utility.Web ;
using FPTToolWeb.Encrypt;

namespace filemanagement
{
	/// <summary>
	/// Summary description for SendMail.
	/// </summary>
	public class SendMail : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button ButtonSearch;
		protected System.Web.UI.WebControls.TextBox TextBoxSearch;
		protected System.Web.UI.WebControls.Label LabelMsg;
		protected System.Web.UI.WebControls.Label LabelSearch;
		protected System.Web.UI.WebControls.TextBox TextBox2;
		protected System.Web.UI.WebControls.TextBox TextBox3;
		protected System.Web.UI.WebControls.TextBox TextBox4;
		protected System.Web.UI.WebControls.TextBox TextBox5;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected System.Web.UI.WebControls.TextBox TextBoxFrom;
		protected System.Web.UI.WebControls.TextBox TextBoxTo;
		protected System.Web.UI.WebControls.TextBox TextBoxCC;
		protected System.Web.UI.WebControls.TextBox TextBoxSubject;
		protected System.Web.UI.WebControls.Label LabelAttachment;
		protected System.Web.UI.WebControls.TextBox TextBoxBody;
		protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

		private string Referrer
		{
			get 
			{
				// Request.UrlReferrer
				if ( ViewState["Referrer"] != null )
					return ViewState["Referrer"] as string ;
				else 
					return "SendMail.aspx" ;
			}
			set
			{
				ViewState["Referrer"] = value ;
			}
		}
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			LabelMsg.Text = string.Empty ;
			if ( !IsPostBack)
			{
				this.Referrer = "SendMail.aspx" ; 

				LabelAttachment.Text = Request["attachment"] ;
				TextBoxSubject.Text = Request["attachment"] ;

				TextBoxFrom.Text = ConfigurationSettings.AppSettings["EmailFromUser"] ;


				if ( Request["Error"] != null )
					LabelMsg.Text = string.Format("{0}", Request["Error"]) ;
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Submit1.ServerClick += new System.EventHandler(this.Submit1_ServerClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Submit1_ServerClick(object sender, System.EventArgs e)
		{
			Email email = new Email() ;
			email.SMTPServer = ConfigurationSettings.AppSettings["SMTPServer"] ;  
			if ( ConfigurationSettings.AppSettings["SMTPUser"] != null )
			{
				email.SMTPUser = ConfigurationSettings.AppSettings["SMTPUser"] ;  
				email.SMTPPassword = Encryption.Decrypt(ConfigurationSettings.AppSettings["SMTPPwd"]) ;  
			}

			email.Attachment = LabelAttachment.Text ;

			string LogFilename = FPTToolWeb.AppSetting.GetLogFilename() ;
			FPTToolWeb.Utility.ILogger logger = new FPTToolWeb.Utility.FileLogger(LogFilename) ;
			email.Logger = logger ;
			bool SendInBackgroupThread = FPTToolWeb.AppSetting.SendByThreadInPool ;

			try 
			{
				email.Send(TextBoxTo.Text
					, TextBoxFrom.Text
					, TextBoxSubject.Text
					, TextBoxBody.Text
					, TextBoxCC.Text
					, ""
					, SendInBackgroupThread) ;

				WebUI.TellCompletedAndBack(this, "Email sent.", this.Referrer) ;
			}
			catch(Exception excpt)
			{
				this.LabelMsg.Text = excpt.Message ;
			}


		}


	}
}
