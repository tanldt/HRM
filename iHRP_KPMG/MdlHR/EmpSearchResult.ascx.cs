namespace iHRPCore.MdlHR
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.DataGridTool;

	/// <summary>
	///		Summary description for EmpSearchResult.
	/// </summary>
	public class EmpSearchResult : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.DataGrid dtgList;
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			this.BindDataGrid();
		}

		private void BindDataGrid()
		{
			if(Session["SearchResult"] != null)
			{
				DataTable dtb = (DataTable)Session["SearchResult"];
				this.dtgList.DataSource = dtb;
				this.dtgList.DataBind();
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
			this.dtgList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgList_PageIndexChanged);
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void dtgList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.BindDataGrid();
			dtgList.CurrentPageIndex = e.NewPageIndex;
			this.dtgList.DataBind();
		}

		private void btnExport_Click(object sender, System.EventArgs e)
		{
			string strTitle = "put the grid title here";
			DataGridTool objj = new DataGridTool();
			new DataGridTool.DataGridExcelExporter(dtgList,this.Page).Export(strTitle);
		}
	}
}
