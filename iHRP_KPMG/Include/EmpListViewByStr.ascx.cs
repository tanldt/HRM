namespace iHRPCore.Include
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.Com;
	using iHRPCore.Include;
	using iHRPCore.HRComponent;

	/// <summary>
	///		Summary description for EmpListViewByStr.
	/// </summary>
	public class EmpListViewByStr : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.DataGrid dtgGridSelected;

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				this.BindDataGrid();
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
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region BindDataGrid
		private void BindDataGrid()
		{
			DataTable dtb = new DataTable();
			try
			{
				if (Request.Params["LastValue"] != null)
				{
					if (Request.Params["LastValue"].Trim() != "")
					{
						string strLastValue = Request.Params["LastValue"].Trim();
						string[] ArrayStr = strLastValue.Split(new char[]{','});
						strLastValue = "";
						for (int i = 0; i<ArrayStr.Length; i++)
						{
							if (ArrayStr[i].Trim() != "")
							{
								if (strLastValue.Trim() == "")
									strLastValue = "''" + ArrayStr[i] + "''";
								else
									strLastValue = strLastValue + "," + "''" + ArrayStr[i] + "''";
							}
						}
						dtb = clsHREmpList.GetEmpList("","","","","","","","","",""," and EmpID in (" + strLastValue + ")",this.Page,"0","");
						this.dtgGridSelected.DataSource = dtb;
						dtgGridSelected.DataBind();
					}
				}
			}
			catch(Exception exp)
			{
				this.lblErr.Text = exp.Message.ToString();
			}
			if (dtb != null)
				dtb.Dispose();
		}
		#endregion

		private void btnExport_Click(object sender, System.EventArgs e)
		{
			iHRPCore.DataGridExcelExporter myExcelXport=new DataGridExcelExporter(dtgGridSelected);			
			myExcelXport.Export();	
		}
	}
}

		