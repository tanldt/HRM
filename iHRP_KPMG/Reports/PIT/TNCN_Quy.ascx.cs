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

	/// <summary>
	///		Summary description for TNCN_Quy.
	/// </summary>
	public class TNCN_Quy : System.Web.UI.UserControl
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
		protected System.Web.UI.WebControls.TextBox txtLSLevel1ID;
		protected System.Web.UI.WebControls.TextBox txtLSLevel1Name;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.DropDownList cboQuarter;
		protected System.Web.UI.WebControls.Label Label5;		
		protected string strLanguage = "VN";
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(!Page.IsPostBack)
			{
				string strTextField = strLanguage == "VN"?"VNName":"Name";				
				clsHREmpList.LoadComboCompany(cboLSCompanyCode, strTextField, strLanguage,this.Page);				
			}
			btnExport.Attributes.Add("OnClick", " return validform()");

			string sYear = System.DateTime.Now.Year.ToString();
			cboYear.Items.Clear();
			ListAssistant Lst = new ListAssistant();
			Lst.PopulateList(cboYear, "Text", "Value", MakeCombo(sYear, 10, 3));
			cboYear.SelectedValue = sYear;

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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
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
			string sQuarter = cboQuarter.SelectedValue.Trim();
			string sReportName = ID;
			
			string sfileTemplate = "Upload/TemplateReport/PIT/" + sReportName + "_Quarter.htm";
			
			DataTable dt = new DataTable();
			DataTable dt1 = new DataTable();
			DataTable dt2 = new DataTable();
			DataTable dt3 = new DataTable();
			DataTable dt4 = new DataTable();

			if (ID == "02-KK-TNCN")
				dt = clsReportPayroll.getData_Tax_02_KK_Quarter(sYear, sQuarter, sReportName, strEmpCode, strEmpName, strCompany, strLevel1, strLevel2, strLevel3,strPosition, strLocation, strStatus, this.Page);
			else if (ID == "03-KK-TNCN")
				dt = clsReportPayroll.getData_Tax_03_KK(sYear, sQuarter, sReportName, strEmpCode, strEmpName, strCompany, strLevel1, strLevel2, strLevel3,strPosition, strLocation, strStatus, this.Page);
			else if (ID == "04-KK-TNCN")
				dt = clsReportPayroll.getData_Tax_02_KK(sYear, sQuarter, sReportName, strEmpCode, strEmpName, strCompany, strLevel1, strLevel2, strLevel3,strPosition, strLocation, strStatus, this.Page);
			
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

		public DataTable getDataRPT(string sYear, string sReportName, string sEmpCode, string sEmpName, string sLSCompanyID,
								 string sLSLevel1ID, string sLSLevel2ID, string sLSLevel3ID, string sLSPositionID, string sLSLocationID,string strStatus, System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			DataTable dtData= new DataTable();
			try
			{
				string sSQL="RPT_spfrmReportPIT @Activity='" + sReportName + "', @Year='" + sYear 
											 + "', @EmpCode=N'" + sEmpCode + "', @EmpName=N'" + sEmpName + "', @LSCompanyID=N'" + sLSCompanyID 
											 + "',@LSLevel1ID=N'" + sLSLevel1ID + "', @LSLevel2ID=N'" + sLSLevel2ID + "', @LSLevel3ID=N'" + sLSLevel3ID 
											 + "',@LSLocationID=N'" + sLSLocationID + "', @LSPositionID=N'" + sLSPositionID + "', @UserGroupID=N'" + sAccountLogin + "'"; 											 
				if (strStatus != "")
					sSQL = sSQL + ", @Status='" + strStatus + "'";
				dtData= clsCommon.GetDataTable(sSQL);				
				return dtData;
			}
			catch
			{
				return null;
			}
			finally
			{
				dtData.Dispose();
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
	}
}
