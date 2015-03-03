using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Configuration;

namespace iHRPCore.Include
{
	/// <summary>
	/// Summary description for GetLookupData.
	/// </summary>
	public class GetLookupData : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			string sAccountLogin= Session["AccountLogin"]!=null?Session["AccountLogin"].ToString():"admin";
			string strLangID = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
//			string sAccountLogin= "sent";
//			string strLangID = "VN";

			string lookupName = string.Empty;
			string filterName = string.Empty;
			string filterValue = string.Empty;
			
			if (Request.Params["id"] != null && Request.Params["id"] != string.Empty)
			{
				lookupName = Request.Params["id"];

				if (Request.Params["filter"] != null && Request.Params["filter"] != string.Empty)
				{
					string filter = Request.Params["filter"];
					string[] filters = filter.Split(',');
					if (filters.Length == 2)
					{
						filterName = filters[0];
						filterValue = filters[1];
					}
				}

				string query = string.Empty;

				switch(lookupName)
				{
					case "Company":
					{
						query =	"sp_GetDataComboLevel1 @TableName = 'LS_tblCompany', @Language='" + strLangID + "',@UserGroupID='" + sAccountLogin + "'";
						break;
					}
					case "Level1":
					{
						if (filterName == "Company")
						{
							query =	"sp_GetDataComboLevel1 @TableName = 'LS_tblLevel1', @Language='" + strLangID + "',@UserGroupID='" + sAccountLogin + "'";
							query += string.Format(", @LSCompanyID = '{0}'", filterValue);
						}
						break;
					}
					case "Level2":
					{
						if (filterName == "Level1")
						{
							query =	"sp_GetDataComboLevel1 @TableName = 'LS_tblLevel2', @Language='" + strLangID + "',@UserGroupID='" + sAccountLogin + "'";

							query += string.Format(", @LSLevel1ID = '{0}'", filterValue);
						}
						break;
					}
					case "Level3":
					{
						if (filterName == "Level2")
						{
							query =	"sp_GetDataComboLevel1 @TableName = 'LS_tblLevel3', @Language='" + strLangID + "',@UserGroupID='" + sAccountLogin + "'";

							query += string.Format(", @LSLevel2ID = '{0}'", filterValue);
						}
						break;
					}
					default:
					{
						break;
					}
				}

				if (query == "")
					query = "select '' [VALUE], '' [VALUE1], '' [TEXT] ";
				string connString = ConfigurationSettings.AppSettings["pstrConnectionString"];
				SqlConnection conn = new SqlConnection(connString);
				SqlCommand command = new SqlCommand(query, conn);

				StringBuilder sb = new StringBuilder();
				
				conn.Open();
				SqlDataReader reader = command.ExecuteReader();
				while(reader.Read())
				{
					sb.AppendFormat("{{\"value\":\"{0}\",\"name\":\"{1}\"}},", reader["VALUE"], reader["TEXT"]);
				}

				string output = sb.ToString();
				if (output.Substring(output.Length-1, 1) == ",")
					output = output.Substring(0, output.Length-1);

				output = string.Format("[{0}]", output);

				Response.Write(output);
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
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}
