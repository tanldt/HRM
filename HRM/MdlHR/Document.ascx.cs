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
	public class Document : System.Web.UI.UserControl
	{
		#region Declare
		public string strLanguage = "EN";
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label lblOtherCompanyID;
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
		protected System.Web.UI.WebControls.DataGrid dtgList;
		protected System.Web.UI.WebControls.Label lblDecisionNumber;
		protected System.Web.UI.WebControls.Label lblContent;
		protected System.Web.UI.WebControls.Label lblToStamp;
		protected System.Web.UI.WebControls.Label lblNote;
		protected System.Web.UI.WebControls.DropDownList cboToStamp;
		protected System.Web.UI.WebControls.CheckBox chkAdToStamp;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdToStamplbl;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdToStamptxt;
		protected System.Web.UI.WebControls.Label lblEstablishPlace;
		protected System.Web.UI.WebControls.TextBox txtEstablishPlace;
		protected System.Web.UI.WebControls.TextBox txtDocumentNo;
		protected System.Web.UI.WebControls.Label lblStorePlace;
		protected System.Web.UI.WebControls.Label lblSubmitDate;
		protected System.Web.UI.WebControls.TextBox txtSubmitDate;
		protected System.Web.UI.WebControls.Label lblDocumentType;
		protected System.Web.UI.WebControls.Label lblSignDate;
		protected System.Web.UI.WebControls.TextBox txtSignDate;
		protected System.Web.UI.WebControls.TextBox txtContent;
		protected System.Web.UI.WebControls.CheckBoxList ChkDocumentType;
		protected System.Web.UI.WebControls.Label lblStorePlace_S;
		protected System.Web.UI.WebControls.TextBox txtStorePlace_S;
		protected System.Web.UI.WebControls.RadioButtonList optType;
		protected System.Web.UI.WebControls.TextBox txtNote;
		protected System.Web.UI.WebControls.LinkButton btnAddnew;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.LinkButton btnList;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.DropDownList cboLSDocumentID;
		protected System.Web.UI.WebControls.TextBox txtStorePlace;
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
			dtb.Columns.Add(new DataColumn("Type"));
			DataRow row;
			for(int i=0; i<5; i++)
			{
				row = dtb.NewRow();
				row["Type"] = "Type" + (i + 1).ToString();
				dtb.Rows.Add(row);
			}
			this.dtgList.DataSource = dtb;
			this.dtgList.DataBind();
		}
	
		private void InitializeComponent()
		{
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
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		
		#endregion
	}
}
