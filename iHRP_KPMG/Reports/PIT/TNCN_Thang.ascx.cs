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
	using Aspose.Cells;

	/// <summary>
	///		Summary description for TNCN_Thang.
	/// </summary>
	public class TNCN_Thang : System.Web.UI.UserControl
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
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.DropDownList cboMonth;
		protected System.Web.UI.WebControls.LinkButton btnExport2;		
		protected string strLanguage = "VN";
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(!Page.IsPostBack)
			{
				string strTextField = strLanguage == "VN"?"VNName":"Name";				
				clsHREmpList.LoadComboCompany(cboLSCompanyCode, strTextField, strLanguage,this.Page);				
				clsCommon.LoadMonthToList(cboMonth);
				try 
				{

					cboMonth.SelectedValue = Convert.ToInt16( System.DateTime.Now.Month.ToString()).ToString();
				}
				catch
				{
					
				}

				string sYear = System.DateTime.Now.Year.ToString();
				cboYear.Items.Clear();
				ListAssistant Lst = new ListAssistant();
				Lst.PopulateList(cboYear, "Text", "Value", MakeCombo(sYear, 10, 3));
				cboYear.SelectedValue = sYear;

			}
			btnExport.Attributes.Add("OnClick", " return validform()");

			

			//if (cboLSCompanyCode.Items.Count==2) cboLSCompanyCode.SelectedIndex=1;				
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
			this.cboLSCompanyCode.SelectedIndexChanged += new System.EventHandler(this.cboLSCompanyCode_SelectedIndexChanged);
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			this.btnExport2.Click += new System.EventHandler(this.btnExport2_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		protected static DataTable MakeCombo(string sYear, int iFirst, int iLast)  
		{  	
			int iYear = Int32.Parse(sYear);
			DataTable table = new DataTable();  
			table.Columns.Add("Text", System.Type.GetType("System.String"));  
			table.Columns.Add("Value", System.Type.GetType("System.String"));  

			DataRow row;
			for (int i=iYear-iFirst; i<=iYear+iLast; i++)
			{
				row = table.NewRow();  
				row["Text"] = i + "";  
				row["Value"] = i + "";  
				table.Rows.Add(row);  	
			}		
			return table;  
		}
		private void KK(string ID)
		{
			// lay du lieu dua len report	
			string strEmpCode = "";
			string strEmpName = "";
			string strCompany = cboLSCompanyCode.SelectedValue.Trim();			
			string strLevel1 = txtLSLevel1ID.Text;
			string strLevel2 = "";
			string strLevel3 = "";
			string strPosition = "";
			string strLocation = "";
			string strStatus = "";
			string sEmpTypeID = "";			
			string sYear = cboYear.SelectedValue.Trim();
			string sMonth = cboMonth.SelectedValue.Trim();
			string sReportName = ID;
			
			string sfileTemplate = "Upload/TemplateReport/PIT/" + sReportName + ".htm";
			
			DataTable dt = new DataTable();
			DataTable dt1 = new DataTable();
			DataTable dt2 = new DataTable();
			DataTable dt3 = new DataTable();
			DataTable dt4 = new DataTable();

			if (ID == "02-KK-TNCN")
				dt = clsReportPayroll.getData_Tax_02_KK(sYear, sMonth, sReportName, strEmpCode, strEmpName, strCompany, strLevel1, strLevel2, strLevel3,strPosition, strLocation, strStatus, this.Page);
			else if (ID == "03-KK-TNCN")
				dt = clsReportPayroll.getData_Tax_03_KK(sYear, sMonth, sReportName, strEmpCode, strEmpName, strCompany, strLevel1, strLevel2, strLevel3,strPosition, strLocation, strStatus, this.Page);
			else if (ID == "04-KK-TNCN")
				dt = clsReportPayroll.getData_Tax_02_KK(sYear, sMonth, sReportName, strEmpCode, strEmpName, strCompany, strLevel1, strLevel2, strLevel3,strPosition, strLocation, strStatus, this.Page);
			
			try
			{	
				FPTToolWeb.Reports.v10.clsBaocao bc = new FPTToolWeb.Reports.v10.clsBaocao();
				// Header --------------
				#region Header 				
				string strHeaderParams = "", strHeaderValues = "";
				#endregion
				// End Header --------------

				// Footer	
				#region Footer
				string strFooterParams = "";
				string strFooterValues = "";				
				strFooterParams = "";
				string strDateCur =  Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");
				strFooterValues = strDateCur.Replace("/",";");  			
				//}

				#endregion
				// End Footer --------------
				
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
				/* not use
				bc.AutoSort = false; //Co' can Auto Sort cac group khong
				bc.IsGroupLv1 = false; //Co Group 1 khong?
				bc.GroupLv1 = 1; //Chi dinh group level 1 o cot thu 1
				bc.IsGroupLv2 = false; //Co Group 2 khong?
				bc.GroupLv2 = 2; // Chi dinh group level 2 o cot thu 2
				bc.IsSum = true; //co sum tong hay khong? Neu co thi phai co id template (itempsumtotal) di kem
				bc.IsSum1 = false; //Co sum cap 1 khong? Neu co thi phai co id template (itempsumlv1) di kem
				bc.IsSum2 = false; //Co sum cap 2 khong? Neu co thi phai co id template (itempsumlv2) di kem
				*/

				bc.sfileTemplate = sfileTemplate;
				bc.sHeaderParams = strHeaderParams;
				bc.sHeaderValues = strHeaderValues;
				bc.sFooterParams = strFooterParams;
				bc.sFooterValues = strFooterValues;
				string strReports = bc.strReportPage(dt);
				#endregion
				//End
				if (strReports == "")
					strReports = "Don't have Data";

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
		
		private void btnExport_Click(object sender, System.EventArgs e)
		{
			string sReport = cboReportType.SelectedValue;
			KK(sReport);
		}
		private void cboLSCompanyCode_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				string strCompanyID = cboLSCompanyCode.SelectedValue.Trim();
				string strTextField = strLanguage == "VN"?"VNName":"Name";			
				if (!strCompanyID.Equals(""))
				{
					clsHREmpList.LoadComboLevel1ByCompany(cboLSLevel1ID,strTextField,strCompanyID, strLanguage,this.Page); 					
				}
				else
				{
					cboLSLevel1ID.Items.Clear();				
				}
			}
			catch(Exception ex)
			{
				lblErr.Text=ex.ToString();
			}
		}
		private void KK2()
		{
			// lay du lieu dua len report	
			string strEmpCode = "";
			string strEmpName = "";
			string strCompany = cboLSCompanyCode.SelectedValue.Trim();			
			string strLevel1 = txtLSLevel1ID.Text;
			string strLevel2 = "";
			string strLevel3 = "";
			string strPosition = "";
			string strLocation = "";
			string strStatus = "";
			string sEmpTypeID = "";			
			string sYear = cboYear.SelectedValue.Trim();
			string sMonth = cboMonth.SelectedValue.Trim();
			string sReportName = cboReportType.SelectedValue.Trim();
			
			string sfileTemplate = @"PIT\" + sReportName + ".xls";
			
			DataTable dt = new DataTable();
			
			dt = clsReportPayroll.getData_Tax_02_KK(sYear, sMonth, sReportName, strEmpCode, strEmpName, strCompany, strLevel1, strLevel2, strLevel3,strPosition, strLocation, strStatus, this.Page);             
			
			try
			{	
				//Open template
				string path = MapPath(".");
				path = path.Substring(0, path.LastIndexOf("\\"));		
				path += @"\..\Upload\TemplateReport\" + sfileTemplate;
				Workbook workbook =  new Workbook();
				workbook.Open(path);

				//workbook.Save(sReportName + ".xls", SaveType.OpenInBrowser, FileFormatType.Default, this.Response);
				workbook.Save(sReportName + ".xls",FileFormatType.Default,SaveType.OpenInExcel,System.Web.HttpContext.Current.Response);

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
		
		private void btnExport2_Click(object sender, System.EventArgs e)
		{
			KK2();
		}

		
	}
}
