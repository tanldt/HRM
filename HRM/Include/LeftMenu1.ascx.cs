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
	using ComponentArt.Web.UI; 
	using System.Configuration;

	/// <summary>
	///		Summary description for LeftMenu.
	/// </summary>
	public class LeftMenu1 : System.Web.UI.UserControl
	{

		protected DataTable tblModule = new DataTable();
		protected DataTable tbl = new DataTable();
		//		protected DataTable tbl_ = new DataTable();
		public int i,j,k;
		public int so;
		public string strModuleID ;
		public string strParentID ;
		public string strFunctionID;
		public string strCssClass ;
		public string strLanguage = "EN";
		protected ComponentArt.Web.UI.Menu Menu1;
		public string strModuleCaption = "";

		private void Page_Load(object sender, System.EventArgs e)
		{
			strLanguage = Session["LangID"] != null? Session["LangID"].ToString().Trim():"VN";
			strModuleID = Request.Params["ModuleID"]==null?"":Request.Params["ModuleID"].Trim();
			if (strModuleID == null)
				strModuleID = "HR";

			buildMenu();
		}
		private void buildMenu()
		{
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				DataSet ds = new DataSet();
				DataTable  rsData = new DataTable();
				SqlDataAdapter adpAdapter = new SqlDataAdapter();
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "sp_clsCommon";

				cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 50).Value = "GetLeftMenu1";
				cmd.Parameters.Add("@ModuleID", SqlDbType.NVarChar, 12).Value = strModuleID;
				cmd.Parameters.Add("@Language", SqlDbType.NVarChar, 12).Value = strLanguage;
				
				adpAdapter.SelectCommand = cmd;
				adpAdapter.Fill(ds);
				ds.Relations.Add("NodeRelation", ds.Tables[0].Columns["NodeId"], ds.Tables[0].Columns["ParentNodeId"]);

				foreach(DataRow dbRow in ds.Tables[0].Rows)
				{
					if(dbRow.IsNull("ParentNodeId"))
					{                                             
						ComponentArt.Web.UI.MenuItem newItem = CreateItem(dbRow);
						Menu1.Items.Add(newItem);
						PopulateSubMenu(dbRow, newItem);
					}
				}
				cmd.Transaction.Commit();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				adpAdapter.Dispose();
				
				rsData.Dispose();
			}
			catch(Exception exp)
			{
				string str = exp.Message;
			}

		}

		private void PopulateSubMenu(DataRow dbRow, ComponentArt.Web.UI.MenuItem item)
		{
			foreach (DataRow childRow in dbRow.GetChildRows("NodeRelation"))
			{                                    
				ComponentArt.Web.UI.MenuItem childItem = CreateItem(childRow);
				item.Items.Add(childItem);
				PopulateSubMenu(childRow, childItem);
			}
		}
		private ComponentArt.Web.UI.MenuItem CreateItem(DataRow dbRow)
		{
			ComponentArt.Web.UI.MenuItem item = new ComponentArt.Web.UI.MenuItem();
			item.Text = dbRow["FunctionName"].ToString(); 
			item.NavigateUrl = dbRow["Url"].ToString() + "?ModuleID=" + strModuleID+ "&ParentID=" +
				dbRow["FunctionID"].ToString() + "&Ascx=" + dbRow["Ascx"].ToString() +
				"&FunctionID=" + dbRow["FunctionDefault"].ToString(); 
			//<%=tbl.Rows[i]["Url"]%>?ModuleID=<%=strModuleID%>&amp;ParentID=<%=tbl.Rows[i]["FunctionID"]%>&amp;Ascx=<%=tbl.Rows[i]["Ascx"]%>&amp;FunctionID=<%=tbl.Rows[i]["FunctionDefault"]%>
			item.LookId = dbRow["LookId"].ToString(); 
			//			item.Look.LeftIconUrl = dbRow["LeftIcon"].ToString(); 
			//			item.Look.HoverLeftIconUrl = dbRow["LeftIconHover"].ToString(); 
			//			item.Look.RightIconUrl = dbRow["RightIcon"].ToString(); 
			//			item.Look.HoverRightIconUrl = dbRow["RightIconHover"].ToString(); 
			return item;
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
