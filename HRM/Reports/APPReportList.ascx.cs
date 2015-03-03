namespace iHRPCore.Reports
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for WFReportList.
	/// </summary>
	public class APPReportList : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.LinkButton btnRpt03;
		protected System.Web.UI.WebControls.Image Image83;
		protected System.Web.UI.WebControls.LinkButton btnRpt0102;
		protected System.Web.UI.WebControls.Image Image84;
		protected System.Web.UI.WebControls.Panel pnlTR;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtFromDate;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Image Image1;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected System.Web.UI.WebControls.DropDownList cboPeriod;		
		protected iHRPCore.Include.EmpHeaderSearch EmpHeaderSearch1;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
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
			this.btnRpt0102.Click += new System.EventHandler(this.btnRpt0102_Click);
			this.btnRpt03.Click += new System.EventHandler(this.btnRpt03_Click);
			this.LinkButton1.Click += new System.EventHandler(this.LinkButton1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnRpt0102_Click(object sender, System.EventArgs e)
		{
			// CRYSTAL REPORT			
			Session["EmpID"]=EmpHeaderSearch1.txtEmpID.Text;
			Session["EmpName"]=EmpHeaderSearch1.txtEmpName.Text;
			Session["JobClass"]=EmpHeaderSearch1.cboJobcode.SelectedValue;
			Session["Location"]=EmpHeaderSearch1.cboLocation.SelectedValue;
			Session["Level1"]=EmpHeaderSearch1.cboLevel1.SelectedValue;
			Session["Level2"]=EmpHeaderSearch1.cboLevel2.SelectedValue;
			Session["Level3"]=EmpHeaderSearch1.cboLevel3.SelectedValue;
			Session["Position"]=EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue;	
		
			Session["Year"]=txtFromDate.Text;
			Session["Period"]=cboPeriod.SelectedValue;
		
			string strURL="./Reports/APP/APPReport.aspx?RptName=RatingLevel";
			Response.Write("<script>window.open('"+strURL+"','VIEW_REPORT','toolbar=0,status=no,menubar=no,scrollbars=1,resizable=yes,copyhistory=yes,top=10,left=50,width=700,height=550,align=top');</script>");
		}

		private void btnRpt03_Click(object sender, System.EventArgs e)
		{
			Session["EmpID"]=EmpHeaderSearch1.txtEmpID.Text;
			Session["EmpName"]=EmpHeaderSearch1.txtEmpName.Text;
			Session["JobClass"]=EmpHeaderSearch1.cboJobcode.SelectedValue;
			Session["Location"]=EmpHeaderSearch1.cboLocation.SelectedValue;
			Session["Level1"]=EmpHeaderSearch1.cboLevel1.SelectedValue;
			Session["Level2"]=EmpHeaderSearch1.cboLevel2.SelectedValue;
			Session["Level3"]=EmpHeaderSearch1.cboLevel3.SelectedValue;
			Session["Position"]=EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue;	

			Session["Year"]=txtFromDate.Text;
			Session["Period"]=cboPeriod.SelectedValue;

			string strURL="./Reports/APP/APPReport.aspx?RptName=RatingLevelByDept";
			Response.Write("<script>window.open('"+strURL+"','VIEW_REPORT','toolbar=0,status=no,menubar=no,scrollbars=1,resizable=yes,copyhistory=yes,top=10,left=50,width=700,height=550,align=top');</script>");
		}

		private void LinkButton1_Click(object sender, System.EventArgs e)
		{
			Session["EmpID"]=EmpHeaderSearch1.txtEmpID.Text;
			Session["EmpName"]=EmpHeaderSearch1.txtEmpName.Text;
			Session["JobClass"]=EmpHeaderSearch1.cboJobcode.SelectedValue;
			Session["Location"]=EmpHeaderSearch1.cboLocation.SelectedValue;
			Session["Level1"]=EmpHeaderSearch1.cboLevel1.SelectedValue;
			Session["Level2"]=EmpHeaderSearch1.cboLevel2.SelectedValue;
			Session["Level3"]=EmpHeaderSearch1.cboLevel3.SelectedValue;
			Session["Position"]=EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue;	

			Session["Year"]=txtFromDate.Text;
			Session["Period"]=cboPeriod.SelectedValue;

			string strURL="./Reports/APP/APPReport.aspx?RptName=RatingLevelYear";
			Response.Write("<script>window.open('"+strURL+"','VIEW_REPORT','toolbar=0,status=no,menubar=no,scrollbars=1,resizable=yes,copyhistory=yes,top=10,left=50,width=700,height=550,align=top');</script>");		
		}		
	}
}
