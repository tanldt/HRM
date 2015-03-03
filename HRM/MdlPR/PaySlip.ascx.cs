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
	///		Summary description for Payslip.
	/// </summary>
	public class Payslip : System.Web.UI.UserControl
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
		protected System.Web.UI.WebControls.LinkButton btnSendMail;
		protected System.Web.UI.WebControls.Label Label35;
		protected System.Web.UI.WebControls.TextBox txtMonth;
		protected System.Web.UI.WebControls.Label Label23;
		protected System.Web.UI.WebControls.DropDownList cboTemplate;
		protected System.Web.UI.WebControls.TextBox txtEffDateAtCompGrid;
		
		#endregion Declare

		

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!Page.IsPostBack)
			{
				txtMonth.Text=DateTime.Today.ToString("dd/MM/yyyy").Substring(3,7);
				clsCommon.LoadDropDownListControl(cboTemplate,"[PR_spfrmReportFileTemplate] @Activity = 'GetDataAll', @Reporttype = 'PaySlip'","ID","FileName",true);
			}
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
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			this.btnSendMail.Click += new System.EventHandler(this.btnSendMail_Click);
			this.Load += new System.EventHandler(this.Page_Load);

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
					strFileTemplate = "Upload/TemplateReport/PIT/Payslip/" + sTemplate;

					try 
					{
						//Phan khai bao se dung Tool bao cao Excel
						FPTToolWeb.Reports.v10.clsBaocao bc = new FPTToolWeb.Reports.v10.clsBaocao();
						#region Config Basic
						bc.sfileTemplate = strFileTemplate;

						#endregion
						sReturn = bc.strReportPageDoc(dtPayroll);
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

			string sReport = GetPaySlipForListEmp(sEmplist);
			ExcelExporter myExcelXport=new ExcelExporter(this.Page);
			myExcelXport.ExportHTMLTo(sReport,"doc");
			myExcelXport = null;
		}

		private void btnSendMail_Click(object sender, System.EventArgs e)
		{
			string strFileTemplate = "";
			string sTemplate = cboTemplate.SelectedItem.Text;
			strFileTemplate = "Upload/TemplateReport/PIT/Payslip/" + sTemplate;
			string sSourceFile = FPTToolWeb.Reports.v10.clsBaocao.ReadTemplate(strFileTemplate);

			
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			SqlCommand cmd = new SqlCommand();
			cnn.Open();
			sqlTran = cnn.BeginTransaction();				
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PR_spfrmPayslip";
			cmd.CommandTimeout = 8000;
			try
			{			
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "SendMail";
				cmd.Parameters.Add("@EmpList",SqlDbType.NVarChar,500).Value = txtEmpList.Text;
				cmd.Parameters.Add("@Content",SqlDbType.NText).Value = sSourceFile;
				cmd.Parameters.Add("@sError", SqlDbType.NVarChar,4000).Direction = ParameterDirection.InputOutput;
				cmd.ExecuteNonQuery();
				string sReturnValue = cmd.Parameters["@sError"].Value.ToString().Trim();
				sqlTran.Commit();
				if (sReturnValue=="")
					lblErr.Text = "Send mail complete!";
				else
					lblErr.Text = sReturnValue;
			}
			catch (SqlException ex)
			{
				lblErr.Text = "Error";
			}
			catch (Exception exp)
			{				
				sqlTran.Rollback();
				lblErr.Text = exp.Message;
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();		
			}

		}
	}
}
