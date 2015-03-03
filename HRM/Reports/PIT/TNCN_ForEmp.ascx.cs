namespace iHRPCore.MdlPR
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Data.SqlClient;
	using System.Configuration;

	using iHRPCore.PRComponent;
	using iHRPCore.Com;
	using iHRPCore.Component;
	using iHRPCore.SendMail; 
	using FPTToolWeb.Exports;
	using iHRPCore.Include;
	
	/// <summary>
	///		Summary description for TNCN_ForEmp.
	/// </summary>
	public class TNCN_ForEmp : System.Web.UI.UserControl
	{
		#region Declare
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtEmpList;
		protected System.Web.UI.WebControls.Literal ltlAlert;
		protected System.Web.UI.WebControls.RadioButtonList optCollection;
		protected System.Web.UI.HtmlControls.HtmlTableRow trListEmp;
		protected System.Web.UI.HtmlControls.HtmlTableRow trCondition;
		protected EmpHeaderSearch EmpHeaderSearch1;
		protected System.Web.UI.WebControls.DropDownList cboIssuedPlaceGrid;
		protected System.Web.UI.WebControls.LinkButton btnPrint;
		protected System.Web.UI.WebControls.Label Label35;
		protected System.Web.UI.WebControls.TextBox txtMonth;
		protected System.Web.UI.WebControls.LinkButton btnSendMail;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.LinkButton btnExportMonth;
		protected System.Web.UI.WebControls.TextBox txtYear;
		protected System.Web.UI.WebControls.DropDownList cboReportMonth;
		protected System.Web.UI.WebControls.DropDownList cboReportYear;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.DropDownList cboTypeExport;
		protected System.Web.UI.WebControls.TextBox txtEffDateAtCompGrid;
		
		#endregion Declare

		

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!Page.IsPostBack)
			{
				txtMonth.Text=DateTime.Today.ToString("dd/MM/yyyy").Substring(3,7);
				txtYear.Text=DateTime.Today.ToString("dd/MM/yyyy").Substring(6,4);
				//clsCommon.LoadDropDownListControl(cboTemplate,"[PR_spfrmReportFileTemplate] @Activity = 'GetDataAll', @Reporttype = 'PaySlip'","ID","FileName",true);
			}
			btnPrint.Attributes.Add("OnClick", " return validYear()");
			btnExportMonth.Attributes.Add("OnClick", " return validMonth()");
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
			this.btnExportMonth.Click += new System.EventHandler(this.btnExportMonth_Click);
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		public string GetPaySlipForListEmp_MMYYYY(string EmpID)
		{
			string sReturn = "";
			try
			{
				string sMonth = txtMonth.Text;
				string sLanguageID = "EN";
				string sAccountID = Mession.GlbUser;
				string sTemplate = cboReportMonth.SelectedItem.Text + ".htm";
				DataTable dt = new DataTable();
				if ( cboReportMonth.SelectedItem.Text == "07-KK-TNCN")
				{
					//DataTable dtPayroll = clsPRCollection.GetDataListPayslip(EmpID,sMonth,sLanguageID,sAccountID);
					dt = clsCommon.GetDataTable("[RPT_spfrmReportPIT_KK07] @ListEmpID = '"+EmpID+"',@Language = '"+sLanguageID+"',@MMYYYY = '"+sMonth+"'");
				}
				if(dt.Rows.Count>0)
				{
					string strFileTemplate = "";
					strFileTemplate = "Upload/TemplateReport/PIT/" + sTemplate;

					try 
					{
						//Phan khai bao se dung Tool bao cao Excel
						FPTToolWeb.Reports.v10.clsBaocao bc = new FPTToolWeb.Reports.v10.clsBaocao();
						#region Config Basic
						bc.sfileTemplate = strFileTemplate;
						bc.sItemLink1 = "EmpID";
						bc.dtSubReport1 = clsCommon.GetDataTable("HR_spfrmRelative @Activity='LoadRelativeAll',@languageID = 'EN',@MMYYYY = '"+txtMonth.Text+"'");

						#endregion
						sReturn = bc.strReportPage_V1_1(dt);
					}
					catch(Exception ex)
					{										
						lblErr.Text = ex.Message;
						return "Error";
					}
					
				}

				return sReturn;
			}
			catch(Exception ex)
			{
				return "Error";
			}

		}

		private void btnPrint_Click(object sender, System.EventArgs e)
		{
			string sEmplist = txtEmpList.Text;
//			string[] arrEmpID = sEmplist.Trim().Split(',');
//			for(int k=0;k<arrEmpID.Length-1;k++)
//			{
//				string strEmpID = arrEmpID.GetValue(k).ToString().Trim();
//			}

			string sReport = GetPaySlipForListEmp_MMYYYY(sEmplist);
			ExcelExporter myExcelXport=new ExcelExporter(this.Page);
			myExcelXport.ExportHTMLTo(sReport,"doc");
			myExcelXport = null;
		}

		private void btnExportMonth_Click(object sender, System.EventArgs e)
		{
			string sEmplist = txtEmpList.Text;
			string sReport = GetPaySlipForListEmp_MMYYYY(sEmplist);
			ExcelExporter myExcelXport=new ExcelExporter(this.Page);
			myExcelXport.ExportHTMLTo(sReport,cboTypeExport.SelectedValue.ToString());
			myExcelXport = null;
		}
	}
}
