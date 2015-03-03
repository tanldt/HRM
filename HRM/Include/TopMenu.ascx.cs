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
	///		Summary description for TopModule.
	/// </summary>
	public class TopMenu : System.Web.UI.UserControl
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
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString():"VN";
			strModuleID = Request.Params["ModuleID"]==null?"":Request.Params["ModuleID"].Trim();
			buildMenu();
		}
		/// <summary>
		/// Hàm tạo Menu
		/// </summary>
		private void buildMenu()
		{
			try
			{
				DataSet ds = new DataSet();
				DataTable  rsData = new DataTable();

				rsData = clsCommon.GetDataTable("sp_clsCommon @Activity = 'GetTopMenu', @ModuleID = '" 
					+ strModuleID + "',@Language='" + strLanguage + "', @UserGroupID='" + Session["AccountLogin"].ToString() + "'");
				ds.Tables.Add(rsData); // Chuyển DataTable qua DataSet

				// So sánh quan hệ FunctionID và Parent
				ds.Relations.Add("NodeRelation", ds.Tables[0].Columns["FunctionID"], ds.Tables[0].Columns["Parent"]);

				foreach(DataRow dbRow in ds.Tables[0].Rows)
				{
					if(dbRow.IsNull("Parent"))
					{           
						ComponentArt.Web.UI.MenuItem newItem = CreateItem(dbRow);// Gọi hàm CreateItem để tạo MenuItem
						Menu1.Items.Add(newItem);// Add MenuItem vào menu
						PopulateSubMenu(dbRow, newItem);// Gọi hàm PopulateSubMenu dùng đệ qui
					}
				}
				
				rsData.Dispose();
			}
			catch(Exception exp)
			{
				string str = exp.Message;
			}

		}

		/// <summary>
		/// Dùng để thêm sub menu
		/// </summary>
		/// <param name="dbRow">DataRow</param>
		/// <param name="item">MenuItem</param>
		private void PopulateSubMenu(DataRow dbRow, ComponentArt.Web.UI.MenuItem item)
		{
			foreach (DataRow childRow in dbRow.GetChildRows("NodeRelation"))
			{                                    
				ComponentArt.Web.UI.MenuItem childItem = CreateItem(childRow);// Gọi hàm CreateItem để tạo MenuItem con
				item.Items.Add(childItem);// Add MenuItem vào menu con
				PopulateSubMenu(childRow, childItem);// Gọi hàm PopulateSubMenu dùng đệ qui
			}
		}
		/// <summary>
		/// Hàm tạo MenuItem
		/// </summary>
		/// <param name="dbRow"></param>
		/// <returns></returns>
		private ComponentArt.Web.UI.MenuItem CreateItem(DataRow dbRow)
		{
			ComponentArt.Web.UI.MenuItem item = new ComponentArt.Web.UI.MenuItem();
			string strFunctionDefault = dbRow["FunctionDefault"].ToString();
			string strFunctionID = dbRow["FunctionID"].ToString();
			string strUrl = dbRow["Url"].ToString();
			string strAscx = dbRow["Ascx"].ToString();
			string strAscxDefault = dbRow["AscxDefault"].ToString();
			if (strFunctionDefault != "")
			{
				strAscx = strAscxDefault;// gán Ascx default vào Ascx
			}
			else
			{
				strFunctionDefault = strFunctionID;
			}
			if (strUrl == "")
				strUrl = "Editpage.aspx";

			/// <summary>
			/// cắt , ghép chuổi
			/// VD:
			/// Func_Return_ASCXReport("Reports\TRReportList.ascx")
			/// Out: Reports\TRReportList.ascx
			/// - Nếu không dùng hàm này
			/// str=Reports\TRReportList.ascx
			/// out: ReportsTRReportList.ascx
			/// </summary>
			strAscx = Func_Return_ASCXReport(strAscx);

			item.Text = dbRow["FunctionName"].ToString(); 

			if (strAscx != "")
                item.NavigateUrl = strUrl + "?ModuleID=" +  dbRow["ModuleID"].ToString() + "&ParentID=" +
				 dbRow["Parent"].ToString() + "&Ascx=" + strAscx +	"&FunctionID=" + strFunctionDefault; 
			item.LookId = dbRow["LookId"].ToString(); 

			return item;
		}
		/// <summary>
		/// tạo chuổi đúng
		/// </summary>
		/// <param name="strUrl">"Reports\TRReportList.ascx"</param>
		/// <returns>Reports\TRReportList.ascx</returns>
		private string Func_Return_ASCXReport(string strUrl)
		{
			string strReturn = "";
			string[] marrstrUrl = strUrl.Split(new Char [] {'\\'});
			if (marrstrUrl.Length > 1)
			{
				for(int i=0;i<marrstrUrl.Length;i++)
				{
					if (strReturn == "")
						strReturn = marrstrUrl.GetValue(i).ToString().Trim();
					else
						strReturn = strReturn + '\\'+ '\\' + marrstrUrl.GetValue(i).ToString().Trim();
				}
			}
			else
				strReturn = strUrl;

			return strReturn;
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
