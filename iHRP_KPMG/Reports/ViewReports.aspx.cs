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
using iHRPCore.HRComponent;

namespace iHRPCore.Reports.SI
{
	/// <summary>
	/// Summary description for ViewReports.
	/// </summary>
	public class ViewReports : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtY1;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.TextBox txtLg1;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.TextBox txtLg2;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Button Button3;
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.TextBox txtLg3;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.TextBox txtLg4;
		protected System.Web.UI.WebControls.Button Button4;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.Button Button5;
		protected System.Web.UI.WebControls.Label Label14;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.Button Button6;
		protected System.Web.UI.WebControls.Label Label16;
		protected System.Web.UI.WebControls.TextBox txtM1;
		protected System.Web.UI.WebControls.Label Label17;
		protected System.Web.UI.WebControls.TextBox txtLg5;
		protected System.Web.UI.WebControls.TextBox txtLg6;
		protected System.Web.UI.WebControls.TextBox txtY3;
		protected System.Web.UI.WebControls.Label Label18;
		protected System.Web.UI.WebControls.Label Label19;
		protected System.Web.UI.WebControls.TextBox txtLg7;
		protected System.Web.UI.WebControls.Button Button7;
		protected System.Web.UI.WebControls.TextBox txtY2;
		protected System.Web.UI.WebControls.Label Label20;
		protected System.Web.UI.WebControls.TextBox txtDept;
		protected System.Web.UI.WebControls.Button Button2;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			Session.RemoveAll();
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
			this.Button7.Click += new System.EventHandler(this.Button7_Click);
			this.Button6.Click += new System.EventHandler(this.Button6_Click);
			this.Button5.Click += new System.EventHandler(this.Button5_Click);
			this.Button4.Click += new System.EventHandler(this.Button4_Click);
			this.Button3.Click += new System.EventHandler(this.Button3_Click);
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
			Session["ssReportName"] = "SI/SI_rptAllLeaveDetailReport.rpt";
			Session["ssReportParams"] = "@Language;@Year";
			Session["ssReportValues"] = txtLg1.Text+";"+txtY1.Text;
			clsCommon.OpenNewWindow(this.Page ,"ShowReports.aspx","VIEW_REPORT");
		}

		private void Button2_Click(object sender, System.EventArgs e)
		{
			Session["ssReportName"] = "SI/SI_rptLeaveRecordByDepartment.rpt";
			Session["ssReportParams"] = "@Language;@Year";
			Session["ssReportValues"] = txtLg2.Text+";"+txtY2.Text;
			clsCommon.OpenNewWindow(this.Page ,"ShowReports.aspx","VIEW_REPORT");
		}

		private void Button3_Click(object sender, System.EventArgs e)
		{
			Session["ssReportName"] = "SI/TMS_rptHeadcountBy_Location_Deparment.rpt";
			Session["ssReportParams"] = "@Language";
			Session["ssReportValues"] = txtLg3.Text;
			clsCommon.OpenNewWindow(this.Page ,"ShowReports.aspx","VIEW_REPORT");
		}

		private void Button4_Click(object sender, System.EventArgs e)
		{
			Session["ssReportName"] = "SI/TMS_rptHeadcountBy_Position_Deparment.rpt";
			Session["ssReportParams"] = "@Language";
			Session["ssReportValues"] = txtLg4.Text;
			clsCommon.OpenNewWindow(this.Page ,"ShowReports.aspx","VIEW_REPORT");
		}

		private void Button5_Click(object sender, System.EventArgs e)
		{
			Session["ssReportName"] = "SI/TMS_rptSummaryOfStaffByBuilding.rpt";
			Session["ssReportParams"] = "@Language";
			Session["ssReportValues"] = txtLg5.Text;
			clsCommon.OpenNewWindow(this.Page ,"ShowReports.aspx","VIEW_REPORT");
		}

		private void Button6_Click(object sender, System.EventArgs e)
		{
			Session["ssReportName"] = "SI/TMS_rptWorkPoint.rpt";
			Session["ssReportParams"] = "@Lg;@MM;@YYYY;@Dept";
			Session["ssReportValues"] = txtLg6.Text+";"+txtM1.Text+";"+txtY3.Text+";"+txtDept.Text;
			clsCommon.OpenNewWindow(this.Page ,"ShowReports.aspx","VIEW_REPORT");
		}

		private void Button7_Click(object sender, System.EventArgs e)
		{
			Session["ssReportName"] = "SI/TMS_rptSignatureSalaryTable.rpt";
			Session["ssReportParams"] = "@Language";
			Session["ssReportValues"] = txtLg7.Text;
			clsCommon.OpenNewWindow(this.Page ,"ShowReports.aspx","VIEW_REPORT");
		}
	}
}
