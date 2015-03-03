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
	///		Summary description for TopModule.
	/// </summary>
	public class TopModule : System.Web.UI.UserControl
	{
		public DataTable tblModule = new DataTable();
		public DataTable tbl = new DataTable();
		public int i,j;
		public int so;
		public string strModuleID ;
		public string strParentID ;
		public string strCssClass ;
		string strLanguage = "EN";
		public string strModuleCaption = "";

		private void Page_Load(object sender, System.EventArgs e)
		{
			strLanguage = Session["LangID"] != null? Session["LangID"].ToString().Trim():"VN";
			strModuleCaption = (Session["LangID"] == null || Session["LangID"].ToString() == "EN")? "Modules":"Phân hệ";
			tblModule = clsCommon.GetDataTable("sp_clsCommon 'GetModuleListExcept',@Language='" 
				+ strLanguage + "', @UserGroupID='" + Session["AccountLogin"].ToString() + "'");
			so = tblModule.Rows.Count;  
			//////
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString():"VN";
			strModuleID = Request.Params["ModuleID"]==null?"":Request.Params["ModuleID"].Trim();
			if (strModuleID == null)
				strModuleID = "HR";

			strParentID = Request.Params["ParentID"]==null?"":Request.Params["ParentID"].Trim();
			if (strParentID == null)
				strParentID = "";
			
				
			
			//Response.Cookies["ModuleID"].Value=strModuleID;
			tbl = clsCommon.GetDataTable("sp_clsCommon @Activity = 'GetHorizontal', @ModuleID = '" 
				+ strModuleID + "',@Language='" + strLanguage + "', @UserGroupID='" + Session["AccountLogin"].ToString() + "'");
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
