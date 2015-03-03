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
	public class PR_rptSalaryExpat : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtTitle;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.LinkButton btnView;
		protected System.Web.UI.WebControls.RadioButtonList optLanguage;
		protected System.Web.UI.WebControls.TextBox txtMMYYYY;

		protected EmpHeaderSearch EmpHeaderSearch1;

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
							
				this.txtMMYYYY.Text = DateTime.Today.ToString("dd/MM/yyyy").Substring(3,7);
								
				
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
			string EmpID = clsCommon.SafeDataString(EmpHeaderSearch1.txtEmpID.Text.Trim());
			string EmpName = clsCommon.SafeDataString(EmpHeaderSearch1.txtEmpName.Text.Trim());
			string Company = EmpHeaderSearch1.cboCompany.SelectedValue.Trim();
			string Level1 = EmpHeaderSearch1.cboLevel1.SelectedValue.Trim();
			string Level2 = EmpHeaderSearch1.cboLevel2.SelectedValue.Trim();
			string Level3 = EmpHeaderSearch1.cboLevel3.SelectedValue.Trim();				
			string Emptype = EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue.Trim();				
			string Status = EmpHeaderSearch1.optStatus.SelectedValue.Trim();
			string Location = EmpHeaderSearch1.cboLocation.SelectedValue.Trim();
			string JobCode = EmpHeaderSearch1.cboJobcode.SelectedValue.Trim();
			lblErr.Text = "";
			string strLanguage = "VN";
			if (Convert.ToInt32(optLanguage.SelectedValue) != 1)
				strLanguage = "EN";	

			if (!CheckPayrollExists(txtMMYYYY.Text))
				return;
			string sLanguage="VN";
			if (Convert.ToInt32(optLanguage.SelectedValue)!=1)
				sLanguage="EN";
			/*string strView =  "PR_vPayroll_"  + txtMonth.Text.ToString().Substring(0,2)+ txtMonth.Text.ToString().Substring(3,4);
			DataTable dt = clsCommon.GetDataTable("PR_spfrmPAYROLL @Activity = 'PrintPayroll',@LanguageID='" + sLanguage + "',@strView='" + strView + "', @MMYYYY = '" + txtMonth.Text + "', @Condition = N'" + strCondition + "', @Language = '" + strLanguage + "'");*/
			DataTable	dt = clsCommon.GetDataTable("PR_SalaryExpat @Language='" + sLanguage +
				"',@CompanyID=N'" + Company + 
				"',@Level1ID=N'" + Level1 +
				"',@Level2ID=N'" + Level2 + 
				"',@Level3ID=N'" + Level3 + 
				"',@EmpID = N'" + EmpID + 
				"',@EmpName=N'" + EmpName + 
				"',@Status=N'" + Status +
				"',@LocationID=N'" + Location +
				"',@PositionID=N'" + Location +
				"',@JobCodeID=N'" + JobCode +
				"',@LSEmpTypeID=N'" + Emptype +
				"',@MMYYYY=N'" + txtMMYYYY.Text +
				"',@Activity=N'" + "detail" +
				"'");
			
			try
			{
				string strHeaderParams = "";
				string strHeaderValues = "";
				string strFooterParams = "";
				string strFooterValues = "";
				//Phan khai bao se dung Tool bao cao Excel
				#region Header 
				//DataTable dtSI_ComInfo = clsCommon.GetDataTable("PR_spfrmPAYROLL @Activity = 'GetExchangeRate', @MMYYYY = '" + txtMonth.Text + "'");
				DataTable	dtSI_ComInfo = clsCommon.GetDataTable("PR_SalaryExpat @Language='" + sLanguage +
					"',@CompanyID=N'" + Company + 
					"',@Level1ID=N'" + Level1 +
					"',@Level2ID=N'" + Level2 + 
					"',@Level3ID=N'" + Level3 + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +
					"',@LocationID=N'" + Location +
					"',@PositionID=N'" + Location +
					"',@JobCodeID=N'" + JobCode +
					"',@LSEmpTypeID=N'" + Emptype +
					"',@MMYYYY=N'" + txtMMYYYY.Text +
					"',@Activity=N'" + "getheader" +
					"'");
				//DataTable dtSI_ComInfo= dt = clsCommon.GetDataTable("PR_SalaryDirect @Activity='getheader',@CompanyID=N'" + Company + "')";
				
				foreach(DataColumn oDataColumn in dtSI_ComInfo.Columns)	
				{
					strHeaderParams += ";" + oDataColumn.ColumnName;
					strHeaderValues += ";" + dtSI_ComInfo.Rows[0][oDataColumn.ColumnName].ToString();
				}
				/*string compname = Company.Equals("01")==true?"Hoàng Long":"Hoàng Vũ";
				   
					strHeaderParams = "thang;compname";
					strHeaderValues = txtMMYYYY.Text;compname;
					*/
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
				bc.IsSum2 = false; //Có Sum cap 2 hay không? Nếu có thì phải có id template (itempsumlv2) đi kèm				
				bc.sfileTemplate = "PR_SalaryExpat.htm";
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
			DataRow dr = clsCommon.GetDataRow("PR_SalaryExpat @MMYYYY = '" + strMonth + "'");
			if (dr == null)
			{
				lblErr.Text = "Tháng lương nhập vào chưa được tính.";
				return false;
			}
			return true;
		}

	}
}
