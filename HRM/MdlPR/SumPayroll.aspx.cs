using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using iHRPCore.Com;
using iHRPCore.PRComponent;	

namespace iHRPCore.MdlPR
{
	/// <summary>
	/// Summary description for SumPayroll.
	/// </summary>
	public class SumPayroll : System.Web.UI.Page
	{
		public DataTable tblMaster = new DataTable();
		public DataTable tblDetail = new DataTable();
		public DataTable dtList = new DataTable();
		protected System.Web.UI.WebControls.Label SalPeriod;
		protected System.Web.UI.WebControls.Label Comp;
		protected System.Web.UI.WebControls.Label L1;
		protected System.Web.UI.WebControls.Label L2;
		protected System.Web.UI.WebControls.Label L3;
		protected System.Web.UI.WebControls.Label MMYYYY;
		protected System.Web.UI.WebControls.DataGrid dtgList;
		public string strLanguage="VN";
		public string strDetails = "";
		protected System.Web.UI.HtmlControls.HtmlGenericControl lblTitle;
		public int iDetails = 0;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"VN";
			//string strTextField = strLanguage == "VN"?"VNName":"Name";
//			MMYYYY.Text = Request.QueryString["MMYYYY"].ToString();
//			SalPeriod.Text = Request.QueryString["SalPeriod"].ToString();
//			Comp.Text = Request.QueryString["Comp"].ToString();
//			L1.Text = Request.QueryString["L1"].ToString();
//			L2.Text = Request.QueryString["L2"].ToString();
//			L3.Text = Request.QueryString["L3"].ToString();

			string strMMYYYY = "";
			string strSalPeriod = "";
			string strComp = "";
			string strL1 = "";
			string strL2 = "";
			string strL3 = "";
			string strEmpCode = "";

			if (Request.QueryString["MMYYYY"] != null)
				strMMYYYY = Request.QueryString["MMYYYY"].ToString();
			if (Request.QueryString["SalPeriod"] != null)
				strSalPeriod = Request.QueryString["SalPeriod"].ToString();
			if ( Request.QueryString["Comp"] != null)
				strComp = Request.QueryString["Comp"].ToString();
			if (Request.QueryString["L1"] != null)
				strL1 = Request.QueryString["L1"].ToString();
			if (Request.QueryString["L2"] != null)
				strL2 = Request.QueryString["L2"].ToString();
			if (Request.QueryString["L3"] != null)
                strL3 = Request.QueryString["L3"].ToString();
			if (Request.QueryString["EmpCode"] != null)
				strEmpCode = Request.QueryString["EmpCode"].ToString();
			lblTitle.InnerText = string.Format("BẢNG TỔNG HỢP LƯƠNG THÁNG {0}  ",strMMYYYY,strSalPeriod);
			if(!Page.IsPostBack)
			{
				tblMaster = clsCommon.GetDataTable("LS_spfrmSalaryItem  @Activity = 'AllDataPayroll_Master',@LanguageID = '" +strLanguage+ "'");
				BindData();
				
				//dtList = clsPRCollection.GetDataSalaryAll(strComp,strL1,strL2,strL3,strEmpCode);
			}
			
		}
		private void BindData()
		{
			DataTable dtList = new DataTable();
			try
			{				
				dtList = clsCommon.GetDataTable("LS_spfrmSalaryItem  @Activity = 'All_note',@LanguageID = '" +strLanguage+ "'");
				dtgList.DataSource = dtList;
				dtgList.DataBind();				
			}
			catch(Exception ex)
			{
				//lblErr.Text = ex.Message;
			}
			finally
			{
				dtList.Dispose();
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
