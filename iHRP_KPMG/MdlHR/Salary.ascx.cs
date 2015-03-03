namespace iHRPCore.MdlHR
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for Salary.
	/// </summary>
	public class Salary : System.Web.UI.UserControl
	{
		#region Declare
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label lblNote;
		protected System.Web.UI.HtmlControls.HtmlInputRadioButton optDec_N;
		protected System.Web.UI.HtmlControls.HtmlInputRadioButton optDec_Y;
		protected System.Web.UI.WebControls.DataGrid dtgSalary;
		protected System.Web.UI.WebControls.TextBox txtEffDate;
		protected System.Web.UI.WebControls.ImageButton cmdCalEffDate;
		protected System.Web.UI.WebControls.Label lblEffDate;
		protected System.Web.UI.WebControls.DropDownList cboScale;
		protected System.Web.UI.WebControls.Label lblScale;
		protected System.Web.UI.WebControls.Label lblGrade;
		protected System.Web.UI.WebControls.DropDownList cboGrade;
		protected System.Web.UI.WebControls.Label lblBasicSalary;
		protected System.Web.UI.WebControls.TextBox txtBasicSalary;
		protected System.Web.UI.WebControls.Label lblContractSalary;
		protected System.Web.UI.WebControls.TextBox txtContractSalary;
		protected System.Web.UI.WebControls.DropDownList cboCurContractSalary;
		protected System.Web.UI.WebControls.DropDownList cboCurBasicSalary;
		protected System.Web.UI.WebControls.Label lblDec_Y;
		protected System.Web.UI.WebControls.Label lblDec_N;
		protected System.Web.UI.WebControls.Label lblDecisionNo;
		protected System.Web.UI.WebControls.TextBox txtDecisionNo;
		protected System.Web.UI.WebControls.Label lblSigner;
		protected System.Web.UI.WebControls.TextBox txtNote;
		protected System.Web.UI.WebControls.Label lblSignDate;
		protected System.Web.UI.WebControls.TextBox txtSignDate;
		protected System.Web.UI.WebControls.ImageButton cmdCalSignDate;
		protected System.Web.UI.WebControls.Label lblSignerPos;
		protected System.Web.UI.WebControls.TextBox txtSignerPos;
		protected System.Web.UI.WebControls.CheckBox chkShowGrid;
		protected System.Web.UI.WebControls.LinkButton btnAddNew;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.LinkButton btnList;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.TextBox txtPageRows;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.Label lblTotalRows;
		protected System.Web.UI.WebControls.LinkButton btnImport;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.TextBox txtSigner;
		#endregion Declare

		private void Page_Load(object sender, System.EventArgs e)
		{
			//Temp
			DataTable dtb = new DataTable();
			DataColumn cl = new DataColumn("No");
			cl.AutoIncrement = true;
			dtb.Columns.Add(cl);
			for(int i=0; i<5 ;i++)
			{
				DataRow dr = dtb.NewRow();
				dtb.Rows.Add(dr);
			}
			dtgSalary.DataSource = dtb;
			dtgSalary.DataBind();
			//Temp
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
