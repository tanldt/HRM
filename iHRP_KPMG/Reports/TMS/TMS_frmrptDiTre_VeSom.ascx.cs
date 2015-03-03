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
	///		Summary description for TMS_frmrptDiTre_VeSom.
	/// </summary>
	public class TMS_frmrptDiTre_VeSom : System.Web.UI.UserControl
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
				string strEmpID = EmpHeaderSearch1.txtEmpID.Text.Trim();
				string strEmpName = EmpHeaderSearch1.txtEmpName.Text.Trim();
				string strLevel1 = EmpHeaderSearch1.cboLevel1.SelectedValue.Trim();
				string strLevel2 = EmpHeaderSearch1.cboLevel2.SelectedValue.Trim();
				string strLevel3 = EmpHeaderSearch1.cboLevel3.SelectedValue.Trim();
				string strCompany = EmpHeaderSearch1.cboCompany.SelectedValue.Trim();
				string strFirstName	= "";//EmpHeaderSearch1.txtVFirstName.Text.Trim();
				string strLastName	= "";// EmpHeaderSearch1.txtVLastName.Text.Trim();
				string strPosition = EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue.Trim();
				string strStatus = EmpHeaderSearch1.optStatus.SelectedValue.Trim();

				string strMMYYYY = txtMonth.Text;

				dt= clsTSEmpList.GetData_DiTre_VeSom(strCompany,strLevel1,strLevel2,strLevel3,
					strEmpID,strEmpName,strFirstName,strLastName,strPosition,strStatus,strMMYYYY,this.Page);
				dt.Columns.Remove("EmpID");

				string strHeader = "";
				strHeader = @"
<table><tr><td></td><td>CÔNG TY ADC</TD></TR></TABLE>
DS ĐI TRỄ VỀ SỚM THÁNG " + strMMYYYY + @"<BR>
				";
				string strFooter = "";
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
