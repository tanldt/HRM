﻿namespace Reports.PR
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
	///		Summary description for PR_rptPayrollOT.
	/// </summary>
	public class PR_rptPayrollOT : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtTitle;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.TextBox txtMonth;
		protected System.Web.UI.WebControls.LinkButton btnView;
		protected EmpHeaderSearch EmpHeaderSearch1;
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				this.txtMonth.Text = DateTime.Today.ToString("dd/MM/yyyy").Substring(3,7);
			}
			btnView.Attributes.Add("onclick", "return checkvalidSearch()");
		}
		private void btnView_Click(object sender, System.EventArgs e)
		{			
			//Lay gia tri cua EmpSearch
			string strEmpID = clsCommon.SafeDataString(EmpHeaderSearch1.txtEmpID.Text.Trim());
			string strEmpName = clsCommon.SafeDataString(EmpHeaderSearch1.txtEmpName.Text.Trim());
			string strCompany = EmpHeaderSearch1.cboCompany.SelectedValue.Trim();
			string strLevel1 = EmpHeaderSearch1.cboLevel1.SelectedValue.Trim();
			string strLevel2 = EmpHeaderSearch1.cboLevel2.SelectedValue.Trim();
			string strLevel3 = EmpHeaderSearch1.cboLevel3.SelectedValue.Trim();				
			string strStatus = EmpHeaderSearch1.optStatus.SelectedValue.Trim();


			DataTable dt = clsCommon.GetDataTable("PR_spfrmPAYROLL @Activity = 'PrintPayrollOT'," + 
												 "@MMYYYY = '" + txtMonth.Text +  "'," +
												 "@EmpID='" + strEmpID + "'," + 	
												 "@EmpName='" + strEmpName + "'," + 	
												 "@LSCompanyID ='" + strCompany + "'," + 	
												 "@LSLevel1ID ='" + strLevel1 + "'," + 				
												 "@LSLevel2ID ='" + strLevel2 + "'," +
 												 "@LSLevel3ID ='" + strLevel3 + "'");
			try
			{
				string strHeaderParams = "";
				string strHeaderValues = "";
				string strFooterParams = "";
				string strFooterValues = "";
				//Phan khai bao se dung Tool bao cao Excel
				#region Header 
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
				bc.sfileTemplate = "PR_rptPayrollOT_Report.htm";
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
	}
}
