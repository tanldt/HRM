namespace iHRPCore.MdlPR
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for CalculateIncomeTax.
	/// </summary>
	public class CalculateIncomeTax : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.DataGrid grdPayroll;
		protected System.Web.UI.WebControls.LinkButton btnPrint;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.LinkButton btnView;
		protected System.Web.UI.WebControls.LinkButton btnCalculate;
		protected System.Web.UI.WebControls.DropDownList cboCurrencyType;
		protected System.Web.UI.WebControls.Label lblCurrency;
		protected System.Web.UI.WebControls.TextBox txtEmpID;
		protected System.Web.UI.WebControls.Label EmpID;
		protected System.Web.UI.WebControls.TextBox txtMonth;
		protected System.Web.UI.WebControls.Label lblPeriod;
		protected System.Web.UI.WebControls.DropDownList cboEmpType;
		protected System.Web.UI.WebControls.Label lblType;
		protected System.Web.UI.WebControls.DropDownList cboLevel3ID;
		protected System.Web.UI.WebControls.Label lblFromSection;
		protected System.Web.UI.WebControls.DropDownList cboLevel2ID;
		protected System.Web.UI.WebControls.Label lblFromDepartment;
		protected System.Web.UI.WebControls.DropDownList cboLevel1ID;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.DropDownList cboCompanyID;
		protected System.Web.UI.WebControls.Label lblCompany;
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel1ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel2ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel3ID;
		protected System.Web.UI.HtmlControls.HtmlTableRow trGrid;

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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
