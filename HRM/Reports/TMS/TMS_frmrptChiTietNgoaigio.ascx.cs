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
	using iHRPCore.TSComponent;
	using iHRPCore.Com;

	/// <summary>
	///		Summary description for TMS_frmrptChiTietNgoaigio.
	/// </summary>
	public class TMS_frmrptChiTietNgoaigio : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.TextBox txtMonth;
		protected System.Web.UI.WebControls.LinkButton btnSearch;
		protected System.Web.UI.WebControls.LinkButton tblView;
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
//			EmpHeaderSearch1.Label1.CssClass = "labelRequire";
//			EmpHeaderSearch1.Label3.CssClass = "labelRequire";
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
			this.tblView.Click += new System.EventHandler(this.tblView_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			DataTable dt = new DataTable();
			try
			{
				//Lay gia tri cua EmpSearch
				string strEmpID = EmpHeaderSearch1.txtEmpID.Text.Trim();
				string strEmpName = EmpHeaderSearch1.txtEmpName.Text.Trim();
				string strLevel1 = EmpHeaderSearch1.cboLevel1.SelectedValue.Trim();
				string strLevel2 = EmpHeaderSearch1.cboLevel2.SelectedValue.Trim();
				string strLevel3 = EmpHeaderSearch1.cboLevel3.SelectedValue.Trim();
				string strCompany = EmpHeaderSearch1.cboCompany.SelectedValue.Trim();
				string strStatus = EmpHeaderSearch1.optStatus.SelectedValue.Trim();

				string strMMYYYY = txtMonth.Text;

				//dt= clsTSEmpList.GetData_LamOT(strCompany,strLevel1,strLevel2,strLevel3,
				//	strEmpID,strEmpName,strStatus,strMMYYYY,this.Page);

				string strHeader = "";
				strHeader = @"
<table><tr><td></td><td colSpan=3><b>NGÂN HÀNG Á CHÂU<b></TD></TR>
<tr><td></td><td colSpan=3><b>Đơn vị:<b> "+ EmpHeaderSearch1.cboLevel1.SelectedItem +@"</TD></TR></TABLE>
<BR>
BẢNG KÊ CHI TIẾT LÀM NGOÀI GIỜ THÁNG " + strMMYYYY + @"<BR>
				";
				string strFooter = "";
				strFooter = @"<br>
<table><tr><td></td></tr>
<tr>
<td colSpan=4><b>Trưởng đơn vị</b></TD>
<td colSpan=4></td>
<td colSpan=4><b>Người chấm công</b></TD>
</TR></TABLE>
				";

				int iColumns = 0;

				ExcelExporter myExcelXport=new ExcelExporter(this.Page);
				myExcelXport.ExportGroupLevel(dt,strHeader,strFooter,iColumns,"xls");
				myExcelXport = null;
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

		private void tblView_Click(object sender, System.EventArgs e)
		{
			DataTable dt = new DataTable();
			try
			{
				//Lay gia tri cua EmpSearch
				string strEmpID = EmpHeaderSearch1.txtEmpID.Text.Trim();
				string strEmpName = EmpHeaderSearch1.txtEmpName.Text.Trim();
				string strLevel1 = EmpHeaderSearch1.cboLevel1.SelectedValue.Trim();
				string strLevel2 = EmpHeaderSearch1.cboLevel2.SelectedValue.Trim();
				string strLevel3 = EmpHeaderSearch1.cboLevel3.SelectedValue.Trim();
				string strCompany = EmpHeaderSearch1.cboCompany.SelectedValue.Trim();
				string strStatus = EmpHeaderSearch1.optStatus.SelectedValue.Trim();

				string strMMYYYY = txtMonth.Text;

				//dt= clsTSEmpList.GetData_LamOT_Sum(strCompany,strLevel1,strLevel2,strLevel3,
				//	strEmpID,strEmpName,strStatus,strMMYYYY,this.Page);
				#region strHeader
				string strHeader = "";
				strHeader = @"
<table><tr><td colSpan=3><b>NGÂN HÀNG Á CHÂU<b></TD></TR>
<tr><td colSpan=3><b>Đơn vị:<b> "+ EmpHeaderSearch1.cboLevel1.SelectedItem +@"</TD></TR></TABLE>
<BR>
BẢNG TỔNG HỢP NGOÀI GIỜ THÁNG " + strMMYYYY + @"<BR>
				";
				#endregion
				#region strFooter
				string strFooter = "";
				strFooter = @"<br>
<table><tr><td></td></tr>
<tr>
<td colSpan=2><b>Trưởng đơn vị</b></TD>
<td colSpan=3><b>Lập biểu</b></TD>
</TR></TABLE>
				";
				#endregion
				ExcelExporter myExcelXport=new ExcelExporter(this.Page);
				myExcelXport.ExportDataTable(dt,strHeader,strFooter,"xls");
				myExcelXport = null;
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
