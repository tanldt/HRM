namespace MdlPR
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore;
	using iHRPCore.Com;
	using iHRPCore.Include;
	using iHRPCore.HRComponent;
	using GridSort;
	using iHRPCore.PRComponent;
	using FPTToolWeb.Control.DataGrids;


	/// <summary>
	///		Summary description for TaxBalance.
	/// </summary>
	public class TaxBalance : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.TextBox txtYear;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.LinkButton btnCalculate;
		protected System.Web.UI.WebControls.Label lblYear;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtMMYYYY;
		protected System.Web.UI.WebControls.Label lblErr;
		private string strLanguage = "VN";

		private void Page_Load(object sender, System.EventArgs e)
		{
			Session["CurFunction"] = Request.Params["FunctionID"].Trim();
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
			if (!Page.IsPostBack)
			{
				DataRow iRow =  clsCommon.GetDataRow("PR_spfrmTaxBalance @Activity='TopData'");
				if(iRow != null)
				{
					txtYear.Text = iRow["Year"].ToString().Trim();
					txtMMYYYY.Text = iRow["MMYYYY"].ToString().Trim();
				}
			}
			btnExport.Attributes.Add("onclick","return CheckData();");
			btnCalculate.Attributes.Add("onclick","return CheckData();");
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
			this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnExport_Click(object sender, System.EventArgs e)
		{
//			DataTable dt = ViewData();
//			iHRPPhonak.Reports.clsBaocaoExcel bc = new iHRPPhonak.Reports.clsBaocaoExcel();
//			string strReports = "";
//			ExcelExporter myExcelXport=new ExcelExporter(this.Page);
//			myExcelXport.ExportHTMLToExcel(strReports,"Excel");
//			myExcelXport = null;

		}
		private DataTable ViewData()
		{
			try
			{
				DataTable dtData = clsCommon.GetDataTableHasID("PR_spfrmTaxBalance @Activity='LoadData',@YYYY='" + txtYear.Text + "'");
				return dtData;
			}
			catch (Exception ex)
			{
				clsChangeLang.popupWindowExp_Mess(this.Parent,ex);
				return null;
			}
		}

		private string checkIsPR()
		{			
			DataTable dtData= clsCommon.GetDataTable("PR_spPayrollCollection 'CheckLock', @MonthSalary = '"+ txtMMYYYY.Text +"'");
			if (dtData.Rows.Count<=0 )
				return clsChangeLang.getStringAlert("PIT_0001",strLanguage);
			else 
				return "";
		}
		private void btnCalculate_Click(object sender, System.EventArgs e)
		{
			lblErr.Text = string.Format(checkIsPR(),txtMMYYYY.Text);
			if (lblErr.Text == "")
			{
				int iYear = Convert.ToInt16(txtMMYYYY.Text.Substring(3,4)) - 1;
				if (iYear.ToString() != txtYear.Text)
					lblErr.Text = string.Format(clsChangeLang.getStringAlert("PIT_0002",strLanguage),Convert.ToInt16(txtYear.Text) + 1);
			}
			if (lblErr.Text == "")
			{
				string sErr= clsDB.Exc_CommandText("PR_spfrmTaxBalance @Activity='Calculate',@YYYY='" + txtYear.Text + "',@MMYYYY='" + txtMMYYYY.Text + "'");
				if (sErr=="")
					clsChangeLang.popupWindow(this.Parent,"0044",strLanguage,"",1);
				else
					clsChangeLang.popupWindow(this.Parent,sErr,strLanguage,0);
			}
		}
	}
}
