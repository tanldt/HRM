namespace Reports.TMS
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using FPTToolWeb.Control.DataGrids;
	using iHRPCore.Include;
	using iHRPCore.SIComponent;
	using iHRPCore.PRComponent;
	using iHRPCore.Com;


	/// <summary>
	///		Summary description for PR_rptPCKhoanChuyen.
	/// </summary>
	public class PR_rptPCKhoanChuyen : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.TextBox txtMonth;
		protected System.Web.UI.WebControls.LinkButton btnSearch;
		protected EmpHeaderSearch EmpHeaderSearch1;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if (!Page.IsPostBack)
			{
				//BindDataGrid();
				this.txtMonth.Text=System.DateTime.Now.ToString("dd/MM/yyyy").Substring(3,7);
			}
			btnSearch.Attributes.Add("onclick", "return checkvalidSearch()");
			//EmpHeaderSearch1.Label1.CssClass = "labelRequire";
			//EmpHeaderSearch1.Label3.CssClass = "labelRequire";
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			DataTable dt = new DataTable();
			try
			{
				//Lay gia tri cua EmpSearch
				string strEmpID = clsCommon.SafeDataString(EmpHeaderSearch1.txtEmpID.Text.Replace("'","").Trim());
				string strEmpName = clsCommon.SafeDataString(EmpHeaderSearch1.txtEmpName.Text.Replace("'","").Trim());
				string strLevel1 = EmpHeaderSearch1.cboLevel1.SelectedValue.Trim();
				string strLevel2 = EmpHeaderSearch1.cboLevel2.SelectedValue.Trim();
				string strLevel3 = EmpHeaderSearch1.cboLevel3.SelectedValue.Trim();
				string strCompany = EmpHeaderSearch1.cboCompany.SelectedValue.Trim();
				string strStatus = EmpHeaderSearch1.optStatus.SelectedValue.Trim();
				string strLocation = EmpHeaderSearch1.cboLocation.SelectedValue.Trim();
				string strPosition = EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue.Trim();
				string strJobcode = EmpHeaderSearch1.cboJobcode.SelectedValue.Trim();
				//hanhNTM them vao dieu kien: resource, gender , local/expat. shortname
				string strVFirstName = "";//EmpHeaderSearch1.txtVFirstName.Text.Replace("'","").Trim();
				string strVLastName = "";//EmpHeaderSearch1.txtVLastName.Text.Replace("'","").Trim();				
				string strMonth = txtMonth.Text;
				//string strPrintDate = txtPrintDate.Text;
				//string strTitle = txtTitle.Text;
				string strParams ="";
				string strValues = "";	
				strParams = "@Language;@EmpID;@EmpName;@CompanyID;@Level1ID;@Level2ID;@Level3ID;@PositionID;@JobCodeID;@LocationID;@Status;@MMYYYY";
				strValues = "VN;;;01;;;;;;;1;"+strMonth;

				Session["ssReportName"] = @"PR\TR_RPTKhoanChuyen.rpt";
				Session["ssReportParams"] = strParams;
				Session["ssReportValues"] = strValues;
				Session["ssReportGroupBy"] = "";
				Session["ssReportGroupByText"] = "";
				Session["ssReportSortBy"] = "";
				Session["ssReportSortDirection"] = "";
				clsCommon.OpenNewWindow(this.Page,"Reports/ShowReports.aspx","ManPower");	
				/*
				string strMMYYYY = txtMonth.Text;

				dt= ClsPR_Report.GetData_PR_rptPCKhoanChuyen(strCompany,strLevel1,strLevel2,strLevel3,
					strEmpID,strEmpName,strStatus,strLocation,strPosition,strJobcode,strMonth,this.Page,strVFirstName,strVLastName);

				dt.Columns.Remove("EmpID");

				string strHeader = "";
				strHeader = @"
<table><tr><td></td><td>CÔNG TY TNHH ADC</TD></TR></TABLE>
TỔNG HỢP CHI PHÍ KHOÁN CHUYỂN VÀO LƯƠNG THÁNG " + strMMYYYY + @"<BR>
				";
				string strFooter = "";
				ExcelExporter myExcelXport=new ExcelExporter(this.Page);
				myExcelXport.ExportDataTable(dt,strHeader,strFooter,"xls");
				myExcelXport = null;
				*/
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
			finally
			{
				dt.Dispose();
			}
			
		}
	}
}
