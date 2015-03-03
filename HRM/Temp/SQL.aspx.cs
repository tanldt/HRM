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
using System.Configuration;
using iHRPCore.Com;
using System.Data.SqlClient;

namespace iHRPCore.Temp
{
	/// <summary>
	/// Summary description for SQL.
	/// </summary>
	public class SQL : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button btnExc;
		protected System.Web.UI.WebControls.TextBox txtSQL;
		protected System.Web.UI.WebControls.Label lblResult;
		protected System.Web.UI.WebControls.DataGrid dgEnum;
		protected System.Web.UI.WebControls.Label Label1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			Label1.Text = ConfigurationSettings.AppSettings["pstrConnectionString"];
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
			this.btnExc.Click += new System.EventHandler(this.btnExc_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnExc_Click(object sender, System.EventArgs e)
		{
			try
			{
				DataTable dt = new DataTable();
				string pstrSQL = txtSQL.Text;
				SqlCommand cmd = new SqlCommand();
				SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
				DataTable rsData = new DataTable();
				SqlDataAdapter adpAdapter = new SqlDataAdapter();

				try
				{

					SQLconn.Open();
					cmd.Connection = SQLconn;
					cmd.CommandType = CommandType.Text;
					cmd.CommandText = pstrSQL;
					cmd.CommandTimeout = 20000;
			
					adpAdapter.SelectCommand = cmd;				
					adpAdapter.Fill(rsData);

					cmd.Dispose();
					SQLconn.Close();
					SQLconn.Dispose();
					adpAdapter.Dispose();
				}
				catch(SqlException exp)
				{                
					cmd.Dispose();
					SQLconn.Close();
					SQLconn.Dispose();
					adpAdapter.Dispose();
					lblResult.Text = exp.Message;
					rsData = null;
				}
				dt = rsData;
				if (dt != null)
				{
					if (dt.Columns.Count != 0)
					{
						dgEnum.DataSource = dt;
						dgEnum.DataBind();
					}
					lblResult.Text = "The command(s) completed successfully.";
				}
			}
			catch (Exception exp)
			{
				lblResult.Text = exp.Message;
			}
		}
	}
}
