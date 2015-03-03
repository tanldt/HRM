namespace Reports.PR
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using FPTToolWeb.Control.DataGrids;
	using iHRPCore.Include;
	using iHRPCore.TMSComponent;
	using iHRPCore.Com;

	/// <summary>
	///		Summary description for TS_rpt001.
	/// </summary>
	public class PR_rptPayroll : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtTitle;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.LinkButton btnView;
		protected System.Web.UI.WebControls.TextBox txtMonth;
		protected System.Web.UI.WebControls.RadioButtonList optLanguage;

		protected EmpHeaderSearch EmpHeaderSearch1;

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				//BindDataGrid();
				DataRow dr = clsCommon.GetDataRow("PR_spfrmPAYROLL @Activity = 'CheckPayrollExists'");
				if (dr == null)
				{
					this.txtMonth.Text = DateTime.Today.ToString("dd/MM/yyyy").Substring(3,7);
				}
				string strCurMonth = dr["CurMonth"].ToString();
				txtMonth.Text = strCurMonth;
				
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

		private string GetEmpHeaderCondition()
		{
			//Lay gia tri cua EmpSearch
			string strEmpID = clsCommon.SafeDataString(EmpHeaderSearch1.txtEmpID.Text.Trim());
			string strEmpName = clsCommon.SafeDataString(EmpHeaderSearch1.txtEmpName.Text.Trim());
			string strCompany = EmpHeaderSearch1.cboCompany.SelectedValue.Trim();
			string strLevel1 = EmpHeaderSearch1.cboLevel1.SelectedValue.Trim();
			string strLevel2 = EmpHeaderSearch1.cboLevel2.SelectedValue.Trim();
			string strLevel3 = EmpHeaderSearch1.cboLevel3.SelectedValue.Trim();				
			string strStatus = EmpHeaderSearch1.optStatus.SelectedValue.Trim();			
			string strCondition =
				"(E.LSCompanyID = ''" + strCompany + "'' or ''" + strCompany + "'' = '''')"
				+ " and (E.EmpCode = ''" + strEmpID + "'' or ''" + strEmpID + "'' = '''')"
				+ " and (E.EmpName like N''%" + strEmpName + "%'' or ''" + strEmpName + "'' = '''')"
				+ " and (B.LSLevel1ID = ''" + strLevel1 + "'' or ''" + strLevel1 + "'' = '''')"
				+ " and (B.LSLevel2ID = ''" + strLevel2 + "'' or ''" + strLevel2 + "'' = '''')"				
				+ " and (B.LSLevel3ID = ''" + strLevel3 + "'' or ''" + strLevel3 + "'' = '''')";	
				//+ " and (E.Active=''" + strStatus + "'' or ''" + strStatus + "''='''')";
			return strCondition;
		}

		private void btnView_Click(object sender, System.EventArgs e)
		{
			string strCondition = GetEmpHeaderCondition();
			lblErr.Text = "";
			string strLanguage = "VN";
			if (Convert.ToInt32(optLanguage.SelectedValue) != 1)
				strLanguage = "EN";	

			if (!CheckPayrollExists(txtMonth.Text))
				return;
			string sLanguage="VN";
			if (Convert.ToInt32(optLanguage.SelectedValue)!=1)
				sLanguage="EN";
			string strView =  "PR_vPayroll_"  + txtMonth.Text.ToString().Substring(0,2)+ txtMonth.Text.ToString().Substring(3,4);
			DataTable dt = clsCommon.GetDataTable("PR_spfrmPAYROLL @Activity = 'PrintPayroll',@LanguageID='" + sLanguage + "',@strView='" + strView + "', @MMYYYY = '" + txtMonth.Text + "', @Condition = N'" + strCondition + "', @Language = '" + strLanguage + "'");
			
			try
			{
				string strHeaderParams = "";
				string strHeaderValues = "";
				string strFooterParams = "";
				string strFooterValues = "";
				//Phan khai bao se dung Tool bao cao Excel
				#region Header 
				//DataTable dtSI_ComInfo = clsCommon.GetDataTable("PR_spfrmPAYROLL @Activity = 'GetExchangeRate', @MMYYYY = '" + txtMonth.Text + "'");
					strHeaderParams = "thang";
					strHeaderValues = txtMonth.Text;
				#endregion
				#region Footer
				strFooterParams = "PrintDate";
				string strDate = DateTime.Now.ToShortDateString();
				if (strDate == "")
					strDate = "..../..../20.....";
				strFooterValues = strDate;
				//strFooterValues = strFooterValues.Replace("/",";");
				#endregion
				iHRPCore.Reports.v11.clsBaocaoExcel bc = new iHRPCore.Reports.v11.clsBaocaoExcel();
				#region Config System
				/*
				 * itempheadlv1: id cho Header Group 1
				 * itempheadlv2: id cho Header Group 2
				 * itempsumlv1: id sum cap 1
				 * itempsumlv2: id sum cap 2
				 * itempsumtotal: id sum tong cong tat ca
				 * */
				#endregion
				#region Config Basic
				bc.IsGroupLv1 = true; //Co Group 1 khong?
				bc.GroupLv1 = 1; //Chỉ định Group level 1 ở cột thứ I
				bc.IsGroupLv2 = false; //Co Group 2 khong?
				bc.GroupLv2 = 2; //Chỉ định Group level 2 ở cột thứ II
				bc.IsSum = true; //Có Sum tong hay không? Nếu có thì phải có id template (itempsumtotal) đi kèm
				bc.IsSum1 = true; //Có Sum cap 1 hay không? Nếu có thì phải có id template (itempsumlv1) đi kèm
				bc.IsSum2 = true; //Có Sum cap 2 hay không? Nếu có thì phải có id template (itempsumlv2) đi kèm				
				if (Convert.ToInt32(optLanguage.SelectedValue)==1)
					bc.sfileTemplate = "PR_rptPayroll_Report_VN.htm";
				else
					bc.sfileTemplate = "PR_rptPayroll_Report.htm";	
				bc.sHeaderParams = strHeaderParams;
				bc.sHeaderValues = strHeaderValues;
				bc.sFooterParams = strFooterParams;
				bc.sFooterValues = strFooterValues;
				string strReports = bc.strReportBasic(dt);
				#endregion
				//End

				ExcelExporter myExcelXport=new ExcelExporter(this.Page);
				myExcelXport.ExportHTMLToExcel(strReports,"Excel");
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

		/// <summary>
		/// Kiem tra xem thang luong nay da ton tai hay chua
		/// </summary>
		/// <param name="strMonth"></param>
		/// <returns></returns>
		private bool CheckPayrollExists(string strMonth)
		{
			DataRow dr = clsCommon.GetDataRow("PR_spfrmPayroll @Activity = 'CheckPayrollExists_1', @MMYYYY = '" + strMonth + "'");
			if (dr == null)
			{
				lblErr.Text = "Lỗi truy cập cơ sở dữ liệu.";
				return false;
			}
			if (dr["Result"].ToString() == "0")
			{				
				lblErr.Text = "Tháng lương nhập vào chưa được tính.";
				return false;
			}
			return true;
		}

	}
}
