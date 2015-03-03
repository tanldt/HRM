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
	using System.Data.SqlClient;
	using System.Configuration;
	using iHRPCore.HRComponent;


	/// <summary>
	///		Summary description for BoardingFeeList.
	/// </summary>
	public class BoardingFeeList : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.DropDownList cboCol3;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.DropDownList cboCol2;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.DropDownList cboCol1;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.CheckBox chkShowGrid;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.TextBox txtPRMonth;
		protected System.Web.UI.WebControls.DataGrid grdList;
		protected System.Web.UI.WebControls.LinkButton btnSearch;
		public string strLanguage = "VN";

		private void Page_Load(object sender, System.EventArgs e)
		{
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"VN";			
			if(!Page.IsPostBack)
			{
				txtPRMonth.Text=System.DateTime.Now.Month.ToString() +"/"+ System.DateTime.Now.Year.ToString();
				//LoadDataGrid();
				
			}
			btnSave.Attributes.Add("OnClick", " return validform()");
			btnDelete.Attributes.Add("OnClick", "return checkdelete()");			
			btnSearch.Attributes.Add("OnClick", "return checkSearch()");			
		}

		private void LoadDataGrid()
		{
			try
			{
				string sPRMonth=this.txtPRMonth.Text.ToString().Trim();			
				DataTable dtData = new DataTable();
				dtData=clsBoardingFeeList.GetListByMonth(sPRMonth,strLanguage);
				this.grdList.DataSource = dtData;
				this.grdList.DataBind();
				dtData.Dispose();
			}
			catch(Exception ex)
			{
				lblErr.Text=ex.ToString();
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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			this.grdList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.grdList_PageIndexChanged);
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{	
				DataRow drData=clsBoardingFeeList.CheckDataExist(Session["EmpID"].ToString().Trim(),txtPRMonth.Text.Trim());
				if (drData!=null)
				{
					lblErr.Text="Mã nhân viên " + Session["EmpID"].ToString().Trim() + " đã nằm trong danh sách tháng " + txtPRMonth.Text.Trim() + ".";
					return;
				}
				clsCommon.ImpactDB("@Creater",Session["AccountLogin"],Session["EmpID"].ToString().Trim(),"Save",this,"PR_spfrmNONBOARDINGLIST");								
				LoadDataGrid();
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				string strID="";
				for(int i=0;i<grdList.Items.Count;i++)
				{
					if(((CheckBox)grdList.Items[i].FindControl("chkSelect")).Checked==true)
					{
						strID += grdList.Items[i].Cells[0].Text.Trim() + "$";
					}
				}
				clsCommon.DeleteListRecord("PR_spfrmNONBOARDINGLIST","ID",SqlDbType.Int,4,strID);				
				LoadDataGrid();
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}	

		private void grdList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			try
			{
				LoadDataGrid();
				grdList.CurrentPageIndex=e.NewPageIndex;
				grdList.DataBind();
			}
			catch(Exception ex)
			{
				string str = ex.Message;
			}
		}

		private void btnExport_Click(object sender, System.EventArgs e)
		{
			iHRPCore.DataGridExcelExporter myExcelXport=new iHRPCore.DataGridExcelExporter(grdList);			
			myExcelXport.Export("");
			myExcelXport =null;
		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			LoadDataGrid();
		}
		
	
	}
}
