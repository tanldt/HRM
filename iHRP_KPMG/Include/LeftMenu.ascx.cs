namespace iHRPCore.Pagelets
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Data.SqlClient;
	using iHRPCore.Com;

	/// <summary>
	///		Summary description for LeftMenu.
	/// </summary>
	public class LeftMenu : System.Web.UI.UserControl
	{

		public DataTable tbl = new DataTable();
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtCurMenu;
		public int i;
		public string strModuleID;
		public string strParentID;
		public string strFunctionID;
		public string strCssClass;
		string strLanguage = "EN";

		private void Page_Load(object sender, System.EventArgs e)
		{
			//string strLanguage = Session["LangID"] == null?"EN":Session["LangID"].ToString();
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString():"VN";
			strModuleID = Request.Params["ModuleID"];
			if (strModuleID == null)
				strModuleID = "HR";

			strParentID = Request.Params["ParentID"];
			if (strParentID == null)
				strParentID = "";

			strFunctionID = Request.Params["FunctionID"];
			if (strFunctionID == null)
				strFunctionID = "";


			//tbl = clsCommon.GetDataTable("sp_clsCommon 'GetLeftMenu',@Language='" + strLanguage + "', @ModuleID='" + strModuleID.ToString() + "',@Parent='" + strParentID.ToString() + "'");
			try
			{
				tbl = clsCommon.GetDataTable("sp_clsCommon 'GetLeftMenu', @ModuleID='" + strModuleID.ToString() + "',@Parent='" + strParentID.ToString() + "',@Language='" + strLanguage + "',@UserGroupID='" + Session["AccountLogin"].ToString() + "'");
			}
			catch(Exception exp)
			{
				string strErr = exp.Message.ToString();
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
