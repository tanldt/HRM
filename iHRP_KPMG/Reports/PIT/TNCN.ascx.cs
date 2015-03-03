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
	///		Summary description for TNCN.
	/// </summary>
	public class TNCN : System.Web.UI.UserControl
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
			// lay du lieu dua len report	
			string strEmpCode = "";
			string strEmpName = "";
			string strCompany = cboLSCompanyCode.SelectedValue.Trim();			
			//string strLevel1 = cboLSLevel1ID.SelectedValue.Trim();  -- ko co gia tri
			string strLevel1 = txtLSLevel1ID.Text;
			string strLevel2 = "";
			string strLevel3 = "";
			string strPosition = "";
			string strLocation = "";
			string strStatus = "";
			string sEmpTypeID = "";			
			string sYear = cboYear.SelectedValue.Trim();						
			string sReportName = cboReportType.SelectedValue.Trim();
			//sReportName = "06TH-BK-TNCN";
			string sfileTemplate = "Upload/TemplateReport/PIT/" + sReportName + ".htm";
			//string sfileTemplate = "PIT/06TH-BK-TNCN.htm";
			DataTable dt = new DataTable();
			DataTable dt1 = new DataTable();
			DataTable dt2 = new DataTable();
			DataTable dt3 = new DataTable();
			DataTable dt4 = new DataTable();

			if (sReportName == "06-KK-TNCN") // report tong hop tu 4 report khac
			{
				dt1 = getDataRPT(sYear, "06A-BK-TNCN", strEmpCode, strEmpName, strCompany, strLevel1, strLevel2, strLevel3,strPosition, strLocation, strStatus, this.Page);             	
				dt2 = getDataRPT(sYear, "06B-BK-TNCN", strEmpCode, strEmpName, strCompany, strLevel1, strLevel2, strLevel3,strPosition, strLocation, strStatus, this.Page);             	
				dt3 = getDataRPT(sYear, "06C-BK-TNCN", strEmpCode, strEmpName, strCompany, strLevel1, strLevel2, strLevel3,strPosition, strLocation, strStatus, this.Page);             	
				dt4 = getDataRPT(sYear, "06D-BK-TNCN", strEmpCode, strEmpName, strCompany, strLevel1, strLevel2, strLevel3,strPosition, strLocation, strStatus, this.Page);             	
			}		
			else if (sReportName == "06A-BK-TNCN" || sReportName == "06B-BK-TNCN" || sReportName == "06C-BK-TNCN" || sReportName == "06D-BK-TNCN")
			{
				dt = getDataRPT(sYear, sReportName, strEmpCode, strEmpName, strCompany, strLevel1, strLevel2, strLevel3,strPosition, strLocation, strStatus, this.Page);
			}
			else if (sReportName == "05-KK-TNCN")
				dt = clsReportPayroll.getData_Tax_05_KK(sYear, strCompany, strLevel1, this.Page);
			else if (sReportName == "05A-BK-TNCN-BDK" || sReportName == "05B-BK-TNCN-BDK")
				dt = clsReportPayroll.getData_Tax_05A_BK(sYear, strCompany, strLevel1, this.Page);
			

			try
			{	
				FPTToolWeb.Reports.v10.clsBaocao bc = new FPTToolWeb.Reports.v10.clsBaocao();
				// Header --------------
				#region Header 				
				string strHeaderParams = "", strHeaderValues = "";
				string strCompanyName = "", strTaxCodeCom = "", strAddress = "", strPhone = "", strFax = "", strEmail = "";		
				if (sReportName == "06-KK-TNCN") // report tong hop tu 4 report khac
				{	
					// tranh truong hop ko lay duoc cac thong tin cty
					if (dt1.Rows.Count > 0) dt = dt1.Copy(); 
					else if (dt2.Rows.Count > 0) dt = dt2.Copy(); 
					else if (dt3.Rows.Count > 0) dt = dt3.Copy(); 
					else if (dt4.Rows.Count > 0) dt = dt4.Copy(); 

					

				}
				if (sReportName == "06-KK-TNCN" || sReportName == "06A-BK-TNCN" || sReportName == "06B-BK-TNCN" || sReportName == "06C-BK-TNCN" 
					|| sReportName == "06D-BK-TNCN" || sReportName == "05A-BK-TNCN-BDK" || sReportName == "05B-BK-TNCN-BDK")
				{
					if (dt.Rows.Count > 0)
					{
						DataRow row = row = dt.Rows[0];
						strCompanyName = row["CompanyName"].ToString();
						strTaxCodeCom = row["TaxCodeCom"].ToString();   	
						strAddress = row["Address"].ToString();   	
						strPhone = row["Phone"].ToString();   	
						strFax = row["Fax"].ToString();   	
						strEmail = row["Email"].ToString();   	
					}
					if (strCompanyName == "") strCompanyName = txtLSLevel1Name.Text;

					strHeaderParams = "CompanyName;TaxCodeCom;Year;Address;Phone;Fax;Email";
					strHeaderValues = strCompanyName+";"+ strTaxCodeCom + ";" + sYear + ";" + strAddress + ";" + strPhone + ";" + strFax + ";" + strEmail;                
				}
				#endregion
				// End Header --------------

				// Footer	
				#region Footer
				string strFooterParams = "";
				string strFooterValues = "";
				strFooterParams = "day;month;year";
				string strDateCur =  Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");
				strFooterValues = strDateCur.Replace("/",";");  			
				if (sReportName == "06-KK-TNCN")
				{
					// thu nhap tu dau tu von
					strFooterParams += ";count_CapitalInvesment;sumAllIncome_CapitalInvesment;sumAllTax_CapitalInvesment";	
					strFooterValues += ";" + dt1.Rows.Count + ";" + bc.SumColumn(dt1,"TotalIncome").ToString() + ";" + bc.SumColumn(dt1,"TaxIncome").ToString();	
					// thu nhap tu chuyen nhuong chung khoan
					strFooterParams += ";count_SecuritiesTransfer;sumAllIncome_SecuritiesTransfer;sumAllTax_SecuritiesTransfer"; 
					strFooterValues += ";" + dt2.Rows.Count + ";" + bc.SumColumn(dt2,"TotalIncome").ToString() + ";" + bc.SumColumn(dt2,"TaxIncome").ToString();	
					// thu nhap tu ban quyen
					strFooterParams += ";count_LoyaltiesFranchising;sumAllIncome_LoyaltiesFranchising;sumAllTax_LoyaltiesFranchising";	
					strFooterValues += ";" + dt3.Rows.Count + ";" + bc.SumColumn(dt3,"TotalIncome").ToString() + ";" + bc.SumColumn(dt3,"TaxIncome").ToString();	
					// thu nhap tu trung thuong
					strFooterParams += ";count_Prizes;sumAllIncome_Prizes;sumAllTax_Prizes";
					strFooterValues += ";" + dt4.Rows.Count + ";" + bc.SumColumn(dt4,"TotalIncome").ToString() + ";" + bc.SumColumn(dt4,"TaxIncome").ToString();	
				}	
				else if (sReportName == "05A-BK-TNCN-BDK")
				{
					strFooterParams += ";sTotalIncome;sSalaryBasic;sBonus;sOtherIncome;sDependants;sDeductMonth;sTaxIncome";		
					strFooterValues += ";" + bc.SumColumn(dt,"TotalIncome").ToString() 
						+ ";" + bc.SumColumn(dt,"SalaryBasic").ToString() 
						+ ";" + bc.SumColumn(dt,"Bonus").ToString() 
						+ ";" + bc.SumColumn(dt,"OtherIncome").ToString() 
						+ ";" + bc.SumColumn(dt,"Dependants").ToString() 
						+ ";" + bc.SumColumn(dt,"DeductMonth").ToString()
						+ ";" + bc.SumColumn(dt,"TaxIncome").ToString();	
				}		
				else
				{
					strFooterParams += ";sumAllIncome;sumAllTax";		
					strFooterValues += ";" + bc.SumColumn(dt,"TotalIncome").ToString() + ";" + bc.SumColumn(dt,"TaxIncome").ToString();	
				}

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
				string strReports = "Don't have data!...";
				if (dt.Rows.Count > 0)
				{
					if (sReportName == "06-KK-TNCN" || sReportName == "06A-BK-TNCN" || sReportName == "06B-BK-TNCN" || sReportName == "06C-BK-TNCN" 
						|| sReportName == "06D-BK-TNCN" || sReportName == "05A-BK-TNCN-BDK" || sReportName == "05B-BK-TNCN-BDK")
					{
						strReports = bc.strReportBasic(dt);
					}
					else
						strReports = bc.strReportPage(dt);
				}
				
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
