namespace iHRPCore.MdlHR
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.Com;
	using iHRPCore.HRComponent;
	/// <summary>
	///		Summary description for WorkingBackground.
	/// </summary>
	public class Accident : System.Web.UI.UserControl
	{
		#region Declare
		public string strLanguage = "EN";
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label lblOtherCompanyID;
		protected System.Web.UI.WebControls.TextBox Textbox2;
		protected System.Web.UI.WebControls.CheckBox chkShowGrid;
		protected System.Web.UI.WebControls.TextBox Textbox4;
		protected System.Web.UI.WebControls.TextBox Textbox6;
		protected System.Web.UI.WebControls.TextBox Textbox7;
		protected System.Web.UI.WebControls.Label lblFromDate;
		protected System.Web.UI.WebControls.Label lblToDate;
		protected System.Web.UI.WebControls.TextBox txtlblToDate;
		protected System.Web.UI.WebControls.ImageButton cmdCalFromDate;
		protected System.Web.UI.WebControls.ImageButton cmdToDate;
		protected System.Web.UI.WebControls.TextBox txtWorkFor;
		protected System.Web.UI.WebControls.Label lblWorkFor;
		protected System.Web.UI.WebControls.Label lblAddress;
		protected System.Web.UI.WebControls.TextBox txtAddress;
		protected System.Web.UI.WebControls.Label lblPhone;
		protected System.Web.UI.WebControls.Label StartSalary;
		protected System.Web.UI.WebControls.Label lbl;
		protected System.Web.UI.WebControls.TextBox txtDuty;
		protected System.Web.UI.WebControls.Label lblDuty;
		protected System.Web.UI.WebControls.TextBox txt;
		protected System.Web.UI.WebControls.TextBox txtFromDate;
		protected System.Web.UI.WebControls.TextBox txtToDate;
		protected System.Web.UI.WebControls.ImageButton cmdCalToDate;
		protected System.Web.UI.WebControls.Label lblStartSalary;
		protected System.Web.UI.WebControls.TextBox txtStartSalary;
		protected System.Web.UI.WebControls.Label lblLastSalary;
		protected System.Web.UI.WebControls.TextBox txtLastSalary;
		protected System.Web.UI.WebControls.Label lblChangeReason;
		protected System.Web.UI.WebControls.TextBox txtChangeReason;
		protected System.Web.UI.WebControls.TextBox txtTelephone;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtWorkingBackgroundID;
		protected System.Web.UI.WebControls.DataGrid grdWorkingBackground;
		protected System.Web.UI.WebControls.Label lblNote2;
		protected System.Web.UI.WebControls.Label lblAccidentDate;
		protected System.Web.UI.WebControls.Label lblSituation;
		protected System.Web.UI.WebControls.Label lblDate;
		protected System.Web.UI.WebControls.TextBox txtDocumentNumber;
		protected System.Web.UI.WebControls.Label lblDocumentNumber;
		protected System.Web.UI.WebControls.TextBox txtSituation;
		protected System.Web.UI.WebControls.Label lblLocation;
		protected System.Web.UI.WebControls.Label lblRank;
		protected System.Web.UI.WebControls.TextBox txtRank;
		protected System.Web.UI.WebControls.TextBox txtLocation;
		protected System.Web.UI.WebControls.Label lblNote;
		protected System.Web.UI.WebControls.TextBox txtNote;
		protected System.Web.UI.WebControls.DataGrid dtgList;
		protected System.Web.UI.WebControls.LinkButton btnSearch;
		protected System.Web.UI.WebControls.TextBox Textbox1;
		protected System.Web.UI.WebControls.TextBox Textbox3;
		protected System.Web.UI.WebControls.TextBox Textbox5;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.LinkButton btnList;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnAddNew;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.DropDownList cboLSAccidentTypeID;
		protected System.Web.UI.WebControls.TextBox FromDate;
		#endregion Declare

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!Page.IsPostBack)
			{
				BindDataGrid();
				
			}
					
		}
		/// <summary>
		/// Load all traing record of Employee
		/// </summary>
		private void BindDataGrid()
		{
			DataTable dtb = new DataTable();
			dtb.Columns.Add(new DataColumn("Date"));
			DataRow row;
			for(int i=0; i<5; i++)
			{
				row = dtb.NewRow();
				row["Date"] = DateTime.Today.ToString("dd/MM/yyyy");
				dtb.Rows.Add(row);
			}
			this.dtgList.DataSource = dtb;
			this.dtgList.DataBind();
		}
	
		private void InitializeComponent()
		{
			this.txtNote.TextChanged += new System.EventHandler(this.txtNote_TextChanged);
			this.Load += new System.EventHandler(this.Page_Load);

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

		private void txtNote_TextChanged(object sender, System.EventArgs e)
		{
		
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		
		#endregion
	}
}
