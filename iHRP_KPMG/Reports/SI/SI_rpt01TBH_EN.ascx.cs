namespace iHRPCore.Reports
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	
	using iHRPCore.Include;
	//using iHRPCore.HR_V2_Component;
	using iHRPCore.Reports.v11;
	using FPTToolWeb.Report;
	using FPTToolWeb.Control.DataGrids;
	using iHRPCore.Com;
	/****/
	using Aspose.Words;
	using System.Net;
	using System.IO;
	/****/

	/// <summary>
	///		Summary description for HR_V2_rptInHopDong.
	/// </summary>
	public class SI_rpt01TBH_EN : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.TextBox txtFromDate;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtToDate;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.HtmlControls.HtmlTable Table2;
		protected System.Web.UI.WebControls.RadioButtonList rdSelect;
		protected EmpHeaderSearchReport EmpHeaderSearchReport1;
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!Page.IsPostBack)
			{		
				//txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
				txtToDate.Text = DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month).ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
				btnExport.Attributes.Add("OnClick", " return validform()");
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

		void createContact(string EmpCode, string EmpName, string CompanyID,string Level1ID,string Level2ID,string Level3ID, string FromDate, string ToDate,string strType)
		{			
			string strEmpID = Session["EmpID"] == null?"":Session["EmpID"].ToString();
			string sAccountLogin= Session["AccountLogin"]!=null?Session["AccountLogin"].ToString():"admin";
			DataSet ds = clsCommon.GetDataSet("SI_sprpt01TBH @CompanyID='" + CompanyID 
				+ "',@Level1ID='" + Level1ID 
				+ "',@Level2ID='" + Level2ID 
				+ "',@Level3ID='" + Level3ID 
				+ "',@EmpCode='" + EmpCode 
				+ "',@EmpName=N'" + EmpName 
				+ "',@UserGroupID='" + sAccountLogin 
				+ "',@FromDate='" + FromDate 
				+ "',@ToDate='" + ToDate	
				+ "'");			
			
			DataTable dtb = new DataTable();
			/***********************************/
		
			string filePath = this.Server.MapPath("~/Upload/TemplateReport/SI_rpt01TBH.doc");
				
			
			
			Aspose.Words.Document doc = new Aspose.Words.Document(filePath);
			DocumentBuilder builder = new DocumentBuilder(doc);
			
			//			//Them hinh
			//			//http://localhost/PVFCCo_iHRP/Upload/EmpImage/EmptyPhoto.jpg
			//			InsertImageFromURL(builder, "http://localhost/PVFCCo_iHRP/Upload/EmpImage/EmptyPhoto.jpg", 60, 80);

			/***********************************/
			// Thong tin hop dong
			dtb = ds.Tables[0];
			doc.MailMerge.Execute(dtb);
			doc.Save("SI_rpt01TBH.doc", SaveFormat.Doc, SaveType.OpenInWord, Response);
            
		}

		private void btnExport_Click(object sender, System.EventArgs e)
		{
			string EmpCode = EmpHeaderSearchReport1.txtEmpID.Text.ToString();
			string EmpName = EmpHeaderSearchReport1.txtEmpName.Text.ToString();
			string LSCompanyID = EmpHeaderSearchReport1.cboCompany.SelectedValue.ToString();
			string LSLevel1ID = EmpHeaderSearchReport1.cboLevel1.SelectedValue.ToString();
			string LSLevel2ID = EmpHeaderSearchReport1.cboLevel2.SelectedValue.ToString();
			string LSLevel3ID = EmpHeaderSearchReport1.cboLevel3.SelectedValue.ToString();
			string strType = rdSelect.SelectedValue.ToString();

			string strTuNgay = txtFromDate.Text;
			string strDenNgay = txtToDate.Text;
			
			createContact(EmpCode, EmpName, LSCompanyID, LSLevel1ID, LSLevel2ID, LSLevel3ID, strTuNgay, strDenNgay,strType);		
			//createContact("TX7213");		
		}
	}
}

