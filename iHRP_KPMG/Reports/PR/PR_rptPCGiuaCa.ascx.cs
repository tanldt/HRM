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
	using iHRPCore.SIComponent;
	using iHRPCore.PRComponent;
	using iHRPCore.Com;

	/// <summary>
	///		Summary description for PR_rptPCGiuaCa.
	/// </summary>
	public class PR_rptPCGiuaCa : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtTitle;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtPrintDate;
		protected System.Web.UI.WebControls.LinkButton btnSearch;
		protected System.Web.UI.WebControls.TextBox txtMonth;
		protected EmpHeaderSearch EmpHeaderSearch1;

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				//BindDataGrid();
				this.txtMonth.Text=System.DateTime.Now.ToString("MM/yyyy");
				this.txtPrintDate.Text=System.DateTime.Now.ToString("dd/MM/yyyy");
			}
			btnSearch.Attributes.Add("onclick", "return checkvalidSearch()");
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
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			lblErr.Text = "";
			DataTable dt = new DataTable();
			try
			{
				//Lay gia tri cua EmpSearch
				string strEmpID = clsCommon.SafeDataString(EmpHeaderSearch1.txtEmpID.Text.Replace("'","").Trim());
				string strEmpName = clsCommon.SafeDataString(EmpHeaderSearch1.txtEmpName.Text.Replace("'","").Trim());
				string strLevel1 = EmpHeaderSearch1.cboLevel1.SelectedValue.Trim();
				string strLevel2 = EmpHeaderSearch1.cboLevel2.SelectedValue.Trim();
				string strLevel3 = EmpHeaderSearch1.cboLevel3.SelectedValue.Trim();
				string strCompany = EmpHeaderSearch1.cboCompany.SelectedValue.Trim();
				string strStatus = EmpHeaderSearch1.optStatus.SelectedValue.Trim();
				string strLocation = EmpHeaderSearch1.cboLocation.SelectedValue.Trim();
				string strPosition = EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue.Trim();
				string strJobcode = EmpHeaderSearch1.cboJobcode.SelectedValue.Trim();
				//hanhNTM them vao dieu kien: resource, gender , local/expat. shortname
				string strVFirstName = "";//EmpHeaderSearch1.txtVFirstName.Text.Replace("'","").Trim();
				string strVLastName = "";//EmpHeaderSearch1.txtVLastName.Text.Replace("'","").Trim();				
				string strMonth = txtMonth.Text;
				string strPrintDate = txtPrintDate.Text;
				string strTitle = txtTitle.Text;
				

				dt= ClsPR_Report.GetData_PR_rptPCGiuaCa(strCompany,strLevel1,strLevel2,strLevel3,
					strEmpID,strEmpName,strStatus,strLocation,strPosition,strJobcode,strMonth,this.Page,strVFirstName,strVLastName);

				#region Header Company Info
				
				
				string strHeaderParams = "";
				string strHeaderValues = "";
				
				strHeaderParams = "MMYYYY";
				strHeaderValues = strMonth;
				#endregion
				#region Footer
				string strFooterParams = "PrintDD;PrintMM;PrintYY";
				string strDate = txtPrintDate.Text;
				if (strDate == "")
					strDate = "..../..../20.....";
				string strFooterValues = strDate;
				strFooterValues = strFooterValues.Replace("/",";");
				#endregion

				//Phan khai bao se dung Tool bao cao Excel
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
				bc.sfileTemplate = "PR_rptPCGiuaCa.htm";
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
	}
}
