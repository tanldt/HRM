namespace Reports.HR
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using FPTToolWeb.Control.DataGrids;
	using iHRPCore.Include;
	using iHRPCore.TSComponent;
	using iHRPCore.Com;
	using System.IO;
	using System.Text;
	using System.Web.UI;

	/// <summary>
	///		Summary description for HR_rptMaster.
	/// </summary>
	public class HR_rptMaster : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.TextBox txtDate;
		protected EmpHeaderSearch EmpHeaderSearch1;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.LinkButton btnSearch1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Export;
		protected System.Web.UI.WebControls.TextBox txtTitle;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			Ajax.Utility.RegisterTypeForAjax(typeof(HR_rptMaster)); // Su dung Ajax
			if (!Page.IsPostBack)
			{
				//BindDataGrid();
				this.txtDate.Text=System.DateTime.Now.ToString("dd/MM/yyyy");
			}
			btnSearch1.Attributes.Add("onclick", "return checkvalidSearch()");
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
			this.btnSearch1.Click += new System.EventHandler(this.btnSearch1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		[Ajax.AjaxMethod]
		public string GetData(string CompanyID,string Level1ID,string Level2ID,string Level3ID, 
			string EmpID,string EmpName,string Status, string strMonth, string strTitle,string LocationID
			,string PositionID, string JobCodeID,string sLanguage)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTable("HR_sprptMaster_Excel @Language='" + sLanguage +
					"',@CompanyID=N'" + CompanyID + 
					"',@Level1ID=N'" + Level1ID +
					"',@Level2ID=N'" + Level2ID + 
					"',@Level3ID=N'" + Level3ID + 
					"',@EmpID = N'" + EmpID + 
					"',@EmpName=N'" + EmpName + 
					"',@Status=N'" + Status +
					"',@LocationID=N'" + LocationID +
					"',@PositionID=N'" + PositionID +
					"',@JobCodeID=N'" + JobCodeID +
					"',@ToDate=N'" + strMonth +
					"'");
				
				//Phan khai bao se dung Tool bao cao Excel
				iHRPCore.Reports.v11.clsBaocaoExcel bc = new iHRPCore.Reports.v11.clsBaocaoExcel();
				#region Header Company Info
				
				string strHeaderParams = "Title";
				string strHeaderValues = strTitle + " " + strMonth;

				#endregion
				#region Footer
				string strFooterParams = "CountEmp";
				string strFooterValues ="";
				if (dt != null)
					strFooterValues = dt.Rows.Count.ToString();
				else
					strFooterValues = "0";
				#endregion
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
				bc.IsGroupLv2 = true; //Co Group 2 khong?
				bc.GroupLv2 = 2; //Chỉ định Group level 2 ở cột thứ II
				bc.IsSum = true; //Có Sum tong hay không? Nếu có thì phải có id template (itempsumtotal) đi kèm
				bc.IsSum1 = false; //Có Sum cap 1 hay không? Nếu có thì phải có id template (itempsumlv1) đi kèm
				bc.IsSum2 = false; //Có Sum cap 2 hay không? Nếu có thì phải có id template (itempsumlv2) đi kèm
				bc.sfileTemplate = "HR_sprptMaster.htm";
				bc.sHeaderParams = strHeaderParams;
				bc.sHeaderValues = strHeaderValues;
				bc.sFooterParams = strFooterParams;
				bc.sFooterValues = strFooterValues;
				string strReports = bc.strReportBasic(dt);
				#endregion

				return strReports;
			}
			catch(Exception ex)
			{
				return ex.Message;
			}
			finally
			{
				dt.Dispose();
			}
		}
		private void btnSearch1_Click(object sender, System.EventArgs e)
		{
			string strReports = Export.Value;
			ExcelExporter myExcelXport=new ExcelExporter(this.Page);
			myExcelXport.ExportHTMLToExcel(strReports,"Excel");
			myExcelXport = null;
		}
	}
}
