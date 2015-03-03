namespace Reports.PIT
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using iHRPCore.Com;
	using iHRPCore.TMSComponent;
	using iHRPCore.Include;
	using GridSort;
	using iHRPCore.HRComponent; //123
	using System.Data.SqlClient;
	using System.Configuration;
	using iHRPCore.SendMail;	
	using iHRPCore;	
	using FPTToolWeb.Control.DataGrids;

	/****/
	using Aspose.Words;
	using System.Net;
	using System.IO;
	/****/

	/// <summary>
	///		Summary description for TNCN_Khac.
	/// </summary>
	public class TNCN_Khac : System.Web.UI.UserControl
	{
		#region Declare
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label lblContractNo;
		protected System.Web.UI.WebControls.TextBox txtContractNo;
		protected System.Web.UI.WebControls.Label lblEffDate;
		protected System.Web.UI.WebControls.TextBox txtEffectiveDate;
		protected System.Web.UI.WebControls.Label lblEndDate;
		protected System.Web.UI.WebControls.TextBox txtToDate;
		protected System.Web.UI.WebControls.TextBox txtContractID;		
		protected System.Web.UI.WebControls.LinkButton btnAddNew;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.Label cboReportTypeTitle;
		protected System.Web.UI.WebControls.DropDownList cboReportType;
		protected System.Web.UI.WebControls.LinkButton btnEx;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.DropDownList cboLSCompanyCode;
		protected System.Web.UI.WebControls.DropDownList cboLSLevel1ID;
		protected System.Web.UI.WebControls.DropDownList cboYear;
		protected EmpHeaderSearch EmpHeaderSearch1;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.TextBox txtLSLevel1ID;
		protected System.Web.UI.WebControls.TextBox txtLSLevel1Name;
		protected System.Web.UI.WebControls.TextBox txtFromDate;
		protected System.Web.UI.WebControls.LinkButton btnView;		
		protected string strLanguage = "VN";
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if (!Page.IsPostBack)
			{
				this.txtFromDate.Text = "01/01/" + DateTime.Today.ToString("yyyy");
				this.txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
			}
			btnView.Attributes.Add("onclick", "return checkvalidSearch()");
			
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
			this.btnView.Click += new System.EventHandler(this.btnView_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnView_Click(object sender, System.EventArgs e)
		{
//			string CompanyID = "";
//			string Level1ID = "";
//			string Level2ID = "";
//			string Level3ID = "";
//			string EmpCode = "";
//			string EmpName = "";

			string strEmpID = clsCommon.SafeDataString(EmpHeaderSearch1.txtEmpID.Text.Trim());
			string strEmpName = clsCommon.SafeDataString(EmpHeaderSearch1.txtEmpName.Text.Trim());
			string strCompany = EmpHeaderSearch1.cboCompany.SelectedValue.Trim();
			string strLevel1 = EmpHeaderSearch1.cboLevel1.SelectedValue.Trim();
			string strLevel2 = EmpHeaderSearch1.cboLevel2.SelectedValue.Trim();
			string strLevel3 = EmpHeaderSearch1.cboLevel3.SelectedValue.Trim();

			string FromDate = txtFromDate.Text;
			string ToDate = txtToDate.Text;

			string sAccountLogin= Session["AccountLogin"]!=null?Session["AccountLogin"].ToString():"admin";
			DataTable dt = clsCommon.GetDataTable("[PIT_sprpt01-K-TNCN] @LSCompanyID='" + strCompany 
				+ "',@LSLevel1ID='" + strLevel1 
				+ "',@LSLevel2ID='" + strLevel2 
				+ "',@LSLevel3ID='" + strLevel3 
				+ "',@EmpName=N'" + strEmpName 
				+ "',@EmpCode=N'" + strEmpID 
				+ "',@FromDate='" + FromDate 
				+ "',@ToDate='" + ToDate
				//+ "',@Type='" + strType 
				+ "',@UserGroupID='" + sAccountLogin 
				+ "'");			
			
			/***********************************/
			string filePath="";
			
			filePath = this.Server.MapPath("~/Upload/TemplateReport/PIT/01-DK-TNCN.doc");

			Aspose.Words.Document doc = new Aspose.Words.Document(filePath);
			DocumentBuilder builder = new DocumentBuilder(doc);
			
			//			//Them hinh
			//			//http://localhost/PVFCCo_iHRP/Upload/EmpImage/EmptyPhoto.jpg
			//			InsertImageFromURL(builder, "http://localhost/PVFCCo_iHRP/Upload/EmpImage/EmptyPhoto.jpg", 60, 80);

			/***********************************/
			
			doc.MailMerge.Execute(dt);
			
			doc.Save("01-DK-TNCN.doc", SaveFormat.Doc, SaveType.OpenInWord, Response);
			
		}

	}
}
