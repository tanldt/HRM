namespace iHRPCore.MdlPR
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.Com;
	using iHRPCore.PRComponent;
	/// <summary>
	///		Summary description for Maternity.
	/// </summary>
	public class MaternityList : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtPRMonth;
		protected System.Web.UI.HtmlControls.HtmlTableRow trGrid;
		protected System.Web.UI.WebControls.DataGrid grdMaternity;
		protected System.Web.UI.WebControls.LinkButton btnSearch;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		public string strLanguage = "EN";
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
			if(!Page.IsPostBack)
			{
				txtPRMonth.Text = DateTime.Today.ToString("MM/yyyy");
				LoadDataGrid();				
			}
			btnSearch.Attributes.Add("OnClick", " return validform()");			
		}
		/// <summary>
		/// Load all Allowance of Employee
		/// </summary>
		private void LoadDataGrid()
		{
			DataTable dtList = new DataTable();
			try
			{
				dtList = clsPRMaternity.GetDataMaternityList(txtPRMonth.Text.Trim());
				grdMaternity.DataSource = dtList;
				grdMaternity.CurrentPageIndex = 0;
				grdMaternity.DataBind();				
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
			finally
			{
				dtList.Dispose();
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
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			this.grdMaternity.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.grdMaternity_ItemCommand);
			this.grdMaternity.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.grdMaternity_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion	
		
		private void grdMaternity_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				if(e.CommandName.Trim().ToUpper() =="EDIT")
				{
					Session["ssMaternityID"] = grdMaternity.Items[e.Item.ItemIndex].Cells[0].Text.Trim();					
					Response.Redirect("Editpage.aspx?ModuleID=PR&ParentID=70&FunctionID=268&Ascx=MdlPR/Maternity.ascx");					
				}				
			}	
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}
		private void grdMaternity_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			try
			{
				LoadDataGrid();
				grdMaternity.CurrentPageIndex=e.NewPageIndex;
				grdMaternity.DataBind();
			}
			catch(Exception ex)
			{
				string str = ex.Message;
			}
		}

		private void btnExport_Click(object sender, System.EventArgs e)
		{
			iHRPCore.DataGridExcelExporter myExcelXport=new DataGridExcelExporter(grdMaternity);			
			myExcelXport.Export("");
			myExcelXport =null;
		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			LoadDataGrid();
		}

	}
}
