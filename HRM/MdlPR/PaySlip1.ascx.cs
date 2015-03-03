namespace iHRPCore.MdlPR
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.PRComponent;
	using iHRPCore.Com;
	using iHRPCore.Component;
	using iHRPCore.SendMail; 
	using FPTToolWeb.Exports;
	/// <summary>
	///		Summary description for PaySlip1.
	/// </summary>
	public class PaySlip1 : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblFromSection;
		protected System.Web.UI.WebControls.Label lblFromDepartment;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label lblCompany;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.Label Label14;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.Label Label16;
		protected System.Web.UI.WebControls.Label Label17;
		protected System.Web.UI.WebControls.Label Label18;
		protected System.Web.UI.WebControls.Label Label19;
		protected System.Web.UI.WebControls.Label Label20;
		protected System.Web.UI.WebControls.Label Label21;
		protected System.Web.UI.WebControls.Label lblBasicSalary;
		protected System.Web.UI.WebControls.Label lblParking;
		protected System.Web.UI.WebControls.Label lblOT;
		protected System.Web.UI.WebControls.Label lblBonus;
		protected System.Web.UI.WebControls.Label lblflex;
		protected System.Web.UI.WebControls.Label lblUniform;
		protected System.Web.UI.WebControls.Label lblTransport;
		protected System.Web.UI.WebControls.Label lblTelephone;
		protected System.Web.UI.WebControls.Label lblBiz;
		protected System.Web.UI.WebControls.Label lblOtherAllo;
		protected System.Web.UI.WebControls.Label lblOtherIncome;
		protected System.Web.UI.WebControls.Label lblSI;
		protected System.Web.UI.WebControls.Label lblHI;
		protected System.Web.UI.WebControls.Label lblPIT;
		protected System.Web.UI.WebControls.Label lblCharity;
		protected System.Web.UI.WebControls.Label lblGift;
		protected System.Web.UI.WebControls.Label lblExpense;
		protected System.Web.UI.WebControls.Label lblLoan;
		protected System.Web.UI.WebControls.Label lblBizPaid;
		protected System.Web.UI.WebControls.Label lblOtherDeduction;
		protected System.Web.UI.WebControls.Label Label22;
		protected System.Web.UI.WebControls.Label lblNethome;
		protected System.Web.UI.WebControls.Label Label24;
		protected System.Web.UI.WebControls.Label Label25;
		protected System.Web.UI.WebControls.Label Label35;
		protected System.Web.UI.WebControls.TextBox txtMonth;
		protected System.Web.UI.HtmlControls.HtmlTableRow trGrid;
		public DataTable tblMaster = new DataTable();
		public DataTable dtPayroll = new DataTable();
		public DataRow iRow;
		protected System.Web.UI.WebControls.LinkButton btnView;
		protected System.Web.UI.WebControls.LinkButton btnSendMail;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Export;
		protected System.Web.UI.WebControls.TextBox txtEx;
		protected System.Web.UI.WebControls.LinkButton btnPrint;
		protected System.Web.UI.WebControls.RadioButtonList optIsNET;
		protected System.Web.UI.WebControls.LinkButton btnUpload;
		protected System.Web.UI.WebControls.Label Label23;
		protected System.Web.UI.WebControls.DropDownList cboTemplate;
		public DataTable tblDetail = new DataTable();

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			Ajax.Utility.RegisterTypeForAjax(typeof(PaySlip1)); // Su dung Ajax
			if(!Page.IsPostBack)
			{
				txtMonth.Text=DateTime.Today.ToString("dd/MM/yyyy").Substring(3,7);
				clsCommon.LoadDropDownListControl(cboTemplate,"[PR_spfrmReportFileTemplate] @Activity = 'GetDataAll', @Reporttype = 'PaySlip'","ID","FileName",true);
			}
			btnView.Attributes.Add("OnClick", " return validform()");
			btnPrint.Attributes.Add("OnClick", " return validform()");
			btnSendMail.Attributes.Add("OnClick", " return validform()");
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
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			this.btnSendMail.Click += new System.EventHandler(this.btnSendMail_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region AjaxMethod
		
		[Ajax.AjaxMethod]
		public DataTable GetData(string sMonth, string LSCompanyID, string LSLevel1ID, string LSLevel2ID
			, string LSLevel3ID, string EmpID, string EmpName,string IsNET, string sLanguageID, string sAccountID)
		{
			DataTable dt = new DataTable();
			try
			{
				dt= clsDB.GetDataTable("PR_spPayrollCollection 'GetDataSalary',@LSCompanyID='" + LSCompanyID 
					+ "',@LSLevel1ID='" + LSLevel1ID 
					+ "',@LSLevel2ID='" + LSLevel2ID 
					+ "',@LSLevel3ID='" + LSLevel3ID 
					+ "',@EmpCode='" + EmpID 
					+ "',@EmpName='" + EmpName 
					+ "',@UserGroupID='" + sAccountID 
					+ "',@Month='" + sMonth 
					+ "',@IsNET=" + IsNET 
					+ "");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}	
		}
		
		
		[Ajax.AjaxMethod]
		public string GetPaySlipForEmp(string EmpID, string sMonth,string sLanguageID, string sAccountID,string sIsNET)
		{
			string sReturn = "";
			try
			{
				DataTable dtPayroll = clsPRCollection.GetDataPayslip(EmpID,sMonth,sLanguageID,sAccountID);
				if(dtPayroll.Rows.Count>0)
				{
					string strFileTemplate = "";
					strFileTemplate = "../Upload/TemplateReport/PR/Taxadvice"+sIsNET+".htm";

					try 
					{
						//Phan khai bao se dung Tool bao cao Excel
						FPTToolWeb.Reports.v10.clsBaocao bc = new FPTToolWeb.Reports.v10.clsBaocao();
						#region Config Basic
						bc.sfileTemplate = strFileTemplate;
//						#region Sub report
//						bc.dtSubReport1 = clsDB.GetDataTable("select EmpID, FirstName + ' ' + LastName as [FullName] from HR_tblRelative");
//						bc.sItemLink1 = "EmpID";
//				
//						bc.dtSubReport2 = clsDB.GetDataTable("select EmpID, FirstName + ' ' + LastName as [FullName] from HR_tblRelative");
//						bc.sItemLink2 = "EmpID";
						#endregion
						sReturn = bc.strReportPageDoc(dtPayroll);
						sReturn += bc.sBreakPage;

					}
					catch//(Exception ex)
					{										
						//lblErr.Text = ex.Message;
						return "Error";
					}
					
				}

				return sReturn;
			}
			catch(Exception ex)
			{
				return "Error";
			}
			finally
			{	
				dtPayroll.Dispose();
			}

		}
		[Ajax.AjaxMethod]
		public string GetPaySlipForListEmp(string EmpID, string sMonth,string sLanguageID, string sAccountID,string sIsNET)
		{
			string sReturn = "";
			try
			{
				DataTable dtPayroll = clsPRCollection.GetDataListPayslip(EmpID,sMonth,sLanguageID,sAccountID);
				if(dtPayroll.Rows.Count>0)
				{
					string strFileTemplate = "";
					strFileTemplate = "../Upload/TemplateReport/PR/Taxadvice"+sIsNET+".htm";

					try 
					{
						//Phan khai bao se dung Tool bao cao Excel
						FPTToolWeb.Reports.v10.clsBaocao bc = new FPTToolWeb.Reports.v10.clsBaocao();
						#region Config Basic
						bc.sfileTemplate = strFileTemplate;
						//						#region Sub report
						//						bc.dtSubReport1 = clsDB.GetDataTable("select EmpID, FirstName + ' ' + LastName as [FullName] from HR_tblRelative");
						//						bc.sItemLink1 = "EmpID";
						//				
						//						bc.dtSubReport2 = clsDB.GetDataTable("select EmpID, FirstName + ' ' + LastName as [FullName] from HR_tblRelative");
						//						bc.sItemLink2 = "EmpID";
						#endregion
						sReturn = bc.strReportPageDoc(dtPayroll);
						sReturn += bc.sBreakPage;

					}
					catch//(Exception ex)
					{										
						//lblErr.Text = ex.Message;
						return "Error";
					}
					
				}

				return sReturn;
			}
			catch(Exception ex)
			{
				return "Error";
			}
			finally
			{	
				dtPayroll.Dispose();
			}

		}
		
		[Ajax.AjaxMethod]
		public string SendmailForEmp(string sValue,string Email)
		{
			string sReturn = "";
			try
			{
				string strSubject = "Tax advice2 ";
				clsSendMail.SendMail("taxadvice@KPMG.com.vn",Email,strSubject,sValue,null);

				return "OK";
			}
			catch(Exception ex)
			{
				return "Error";
			}
		}
		
		
		#endregion
		public string GetPaySlipForListEmp(string EmpID)
		{
			string sReturn = "";
			try
			{
				string sMonth = txtMonth.Text;
				string sLanguageID = "EN";
				string sAccountID = Mession.GlbUser;
				string sTemplate = cboTemplate.SelectedItem.Text;
				DataTable dtPayroll = clsPRCollection.GetDataListPayslip(EmpID,sMonth,sLanguageID,sAccountID);
				if(dtPayroll.Rows.Count>0)
				{
					string strFileTemplate = "";
					strFileTemplate = "../Upload/TemplateReport/PIT/Payslip/" + sTemplate;

					try 
					{
						//Phan khai bao se dung Tool bao cao Excel
						FPTToolWeb.Reports.v10.clsBaocao bc = new FPTToolWeb.Reports.v10.clsBaocao();
						#region Config Basic
						bc.sfileTemplate = strFileTemplate;

						#endregion
						sReturn = bc.strReportPageDoc(dtPayroll);
						sReturn += bc.sBreakPage;

					}
					catch//(Exception ex)
					{										
						//lblErr.Text = ex.Message;
						return "Error";
					}
					
				}

				return sReturn;
			}
			catch(Exception ex)
			{
				return "Error";
			}
			finally
			{	
				dtPayroll.Dispose();
			}

		}
		private void btnView_Click(object sender, System.EventArgs e)
		{
		
		}

	
		private void btnPrint_Click(object sender, System.EventArgs e)
		{
			string strReports = Export.Value;
			ExcelExporter myExcelXport=new ExcelExporter(this.Page);
			myExcelXport.ExportHTMLTo(strReports,"doc");
			myExcelXport = null;

		}

		private void btnSendMail_Click(object sender, System.EventArgs e)
		{
			
		}

		private void Linkbutton1_Click(object sender, System.EventArgs e)
		{
			ExcelExporter myExcelXport=new ExcelExporter(this.Page);
			myExcelXport.ExportHTMLTo(txtEx.Text,"doc");
			myExcelXport = null;
		}

}

}
