namespace iHRPCore.MdlHR
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for BorrowPassport.
	/// </summary>
	public class BorrowPassport : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label lblFromDate;
		protected System.Web.UI.WebControls.TextBox txtFromDate;
		protected System.Web.UI.WebControls.Label lblReason;
		protected System.Web.UI.WebControls.TextBox txtHealth;
		protected System.Web.UI.WebControls.Label lblNote;
		protected System.Web.UI.WebControls.TextBox txtNote;
		protected System.Web.UI.WebControls.CheckBox chkShowGrid;
		protected System.Web.UI.WebControls.LinkButton btnAddNew;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnList;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.DataGrid dtgList;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtWorkingBackgroundID;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdGrid;
		protected System.Web.UI.WebControls.Label lblToDate;
		protected System.Web.UI.WebControls.TextBox txtToDate;

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!Page.IsPostBack)
			{
				BindDataGrid();
				
			}
			
		}
		private void BindDataGrid()
		{
			DataTable dtb = new DataTable();
			dtb.Columns.Add(new DataColumn("FromDate"));
			DataRow row;
			for(int i=0; i<5; i++)
			{
				row = dtb.NewRow();
				row["FromDate"] = DateTime.Today.ToString("dd/MM/yyyy");
				dtb.Rows.Add(row);
			}
			this.dtgList.DataSource = dtb;
			this.dtgList.DataBind();
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
