using System;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using iHRPCore;

namespace Temp
{
	/// <summary>
	/// Summary description for TestSQLtoExcel.
	/// </summary>
	public class TestSQLtoExcel : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button Button1;
	
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
			DataTable dt = clsDB.GetDataTable("select * from LS_tblHospital");
			CreateExcelFromDataTable(@"F:\Projects\iHRP_KPMG\WIP\Program\iHRP_KPMG\Temp\Test.xls",dt);
		}
		public static void CreateExcelFromDataTable(string filename, DataTable dt) 
		{

			DataGrid grid = new DataGrid();
			grid.HeaderStyle.Font.Bold = true;
			grid.DataSource = dt;
			grid.DataMember = dt.TableName;

			grid.DataBind();

			// render the DataGrid control to a file

			using (StreamWriter sw = new StreamWriter(filename)) 
			{
				using (HtmlTextWriter hw = new HtmlTextWriter(sw)) 
				{
					grid.RenderControl(hw);
				}
			}
		}

	}
}
