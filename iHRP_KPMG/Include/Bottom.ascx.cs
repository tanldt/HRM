namespace iHRPCore.Include
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.Com;
	using iHRPCore.HRComponent;

	/// <summary>
	///		Summary description for Bottom.
	/// </summary>
	public class Bottom : System.Web.UI.UserControl
	{

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
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

		private void btnGet_Click(object sender, System.EventArgs e)
		{
			string strReturn ="";
			//string str_FrmName = this.Page.ToString().Substring(4,this.Page.ToString().Length-9);
			string str_FrmName = Request.Params["Ascx"];
			foreach(Control Child_ctl in Page.Controls)
			{
				strReturn =  clsChangeLang.GetAllControlOfForm(Child_ctl, str_FrmName);
			}
			
		}

		private void btnSet_Click(object sender, System.EventArgs e)
		{
			//string str_FrmName = this.Page.ToString().Substring(4,this.Page.ToString().Length-9);
			string str_FrmName = Request.Params["Ascx"];
			Session["Link_URL"] = Request.ServerVariables["QUERY_STRING"];
			this.Response.Redirect("UMS_frmCaption.aspx?ID="+str_FrmName);
		}
	}
}
