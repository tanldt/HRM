namespace iHRPCore.Include
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Data.SqlClient;
	using System.Collections;
	using iHRPCore.Com;
	using iHRPCore.HRComponent;
	using iHRPCore.UMSComponent;


	/// <summary>
	///		Summary description for Bottom_LS.
	/// </summary>
	public class Bottom_LS : System.Web.UI.UserControl
	{

		public DataTable tbl = new DataTable();
		public int i;
		public string strModuleID;
		public string strParentID;
		public string strTemp;

		protected System.Web.UI.WebControls.Label Label1;
		
		
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected System.Web.UI.WebControls.LinkButton btnPreview;
		public string sAscx="";
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			//if (!Page.IsPostBack)
			//{
				strModuleID = Request.Params["ModuleID"];
				if (strModuleID == null)
					strModuleID = "HR";

				strParentID = Request.Params["ParentID"];
				if (strParentID == null)
					strParentID = "";

				/*LanHTD: 23/02/2006
				 * if (Request.Params["FunctionID"] != null)
					tbl = clsCommon.GetDataTable("sp_clsCommon 'GetLSByForm',@FunctionID='" + Request.Params["FunctionID"].ToString() + "'");*/

				tbl = new DataTable();
				
				if (Request.Params["Ascx"] != null)
				sAscx=Request.Params["Ascx"].Trim();

				
			if (!Page.IsPostBack)
			{
				
				//btnSaveAdmin_CheckedChanged(null,null);
			}
				Label1.Visible=false;
				if (tbl != null )
					if (tbl.Rows.Count > 0) Label1.Visible=true;
			//}
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
