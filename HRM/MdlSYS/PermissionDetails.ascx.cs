namespace iHRPCore.MdlSYS
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for PermissionDetails.
	/// </summary>
	public class PermissionDetails : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox Textbox16;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox Textbox1;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.DataGrid dtgList;

		private void Page_Load(object sender, System.EventArgs e)
		{
			//Temp
			DataTable dtb = new DataTable();
			DataColumn cl = new DataColumn("No");
			cl.AutoIncrement = true;
			dtb.Columns.Add(cl);
			for(int i=0; i<5 ;i++)
			{
				DataRow dr = dtb.NewRow();
				dtb.Rows.Add(dr);
			}
			dtgList.DataSource = dtb;
			dtgList.DataBind();
			//Temp
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
