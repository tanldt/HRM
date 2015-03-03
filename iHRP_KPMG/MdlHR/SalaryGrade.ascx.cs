namespace iHRPCore.MdlHR
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.Com;
	using iHRPCore.PRComponent;
	using System.Data.SqlClient;
	using System.Configuration;
	using iHRPCore.HRComponent;

	/// <summary>
	///		Summary description for SalaryGrade.
	///		Tanldt
	///		12/09/2005
	/// </summary>
	public class SalaryGrade : System.Web.UI.UserControl
	{
		public string strLanguage = "EN";
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.CheckBox chkShowGrid;
		protected System.Web.UI.WebControls.LinkButton btnAddNew;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.LinkButton btnImport;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.HtmlControls.HtmlTableRow trGrid;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.HtmlControls.HtmlTableRow TrCB;
		protected System.Web.UI.HtmlControls.HtmlTableRow TrCD;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label22;
		protected System.Web.UI.WebControls.Label Label23;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.Label Label16;
		protected System.Web.UI.WebControls.Label Label17;
		protected System.Web.UI.WebControls.Label Label20;
		protected System.Web.UI.WebControls.Label Label21;
		protected System.Web.UI.WebControls.TextBox txtPosition;
		protected System.Web.UI.WebControls.Label Label24;
		protected System.Web.UI.WebControls.Label Label18;
		protected System.Web.UI.WebControls.RadioButton opt1;
		protected System.Web.UI.WebControls.RadioButton opt2;
		protected System.Web.UI.WebControls.DataGrid dtgList;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.Label Label14;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.Label Label19;
		protected System.Web.UI.WebControls.Label Label25;
		protected System.Web.UI.WebControls.Label Label26;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label27;
		protected System.Web.UI.WebControls.Label Label28;
		protected System.Web.UI.WebControls.Label Label29;
		protected System.Web.UI.WebControls.Label Label30;
		protected System.Web.UI.WebControls.Label Label31;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.TextBox txtDecision_Pos;
		protected System.Web.UI.WebControls.TextBox txtSignDate_Pos;
		protected System.Web.UI.WebControls.DropDownList cboVerOfSal_Pos;
		protected System.Web.UI.WebControls.DropDownList cboRankOfSal_Pos;
		protected System.Web.UI.WebControls.TextBox txtScaleOfSal_Pos;
		protected System.Web.UI.WebControls.TextBox txtGradeOfSal_Pos;
		protected System.Web.UI.WebControls.TextBox txtProDate_Pos;
		protected System.Web.UI.WebControls.TextBox txtEffDate_Pos;
		protected System.Web.UI.WebControls.TextBox txtEndDate_pos;
		protected System.Web.UI.WebControls.TextBox txtSigner_pos;
		protected System.Web.UI.WebControls.TextBox txtPosSign_Pos;
		protected System.Web.UI.WebControls.TextBox txtSalStandard_Pos;
		protected System.Web.UI.WebControls.TextBox txtSalPercent_Pos;
		protected System.Web.UI.WebControls.TextBox txtOtherSal_Pos;
		protected System.Web.UI.WebControls.Label Label32;
		protected System.Web.UI.WebControls.Label Label33;
		protected System.Web.UI.WebControls.Label Label34;
		protected System.Web.UI.WebControls.Label Label35;
		protected System.Web.UI.WebControls.TextBox txtDecision_Basic;
		protected System.Web.UI.WebControls.TextBox txtSignDate_Basic;
		protected System.Web.UI.WebControls.DropDownList cboVerOfSal_Basic;
		protected System.Web.UI.WebControls.DropDownList cboRankOfSal_Basic;
		protected System.Web.UI.WebControls.TextBox txtScaleOfSal_Basic;
		protected System.Web.UI.WebControls.TextBox txtGradeOfSal_Basic;
		protected System.Web.UI.WebControls.TextBox txtPromotion_Basic;
		protected System.Web.UI.WebControls.TextBox txtNote_Basic;
		protected System.Web.UI.WebControls.TextBox txtNote_Pos;
		protected System.Web.UI.WebControls.TextBox txtEffDate_Basic;
		protected System.Web.UI.WebControls.TextBox txtEndDate_Basic;
		protected System.Web.UI.WebControls.TextBox txtSigner_Basic;
		protected System.Web.UI.WebControls.TextBox txtPosOfSigner_Basic;
		protected System.Web.UI.WebControls.TextBox txtSalStandard_Basic;
		protected System.Web.UI.WebControls.TextBox txtAllCofe1_Basic;
		protected System.Web.UI.WebControls.TextBox txtAllCofe2_Basic;
		protected System.Web.UI.WebControls.TextBox txtAllCoef1_Pos;
		protected System.Web.UI.WebControls.TextBox txtAllCofe2_Pos;
		public string strID = "";

		

	
		
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
			this.opt2.CheckedChanged += new System.EventHandler(this.opt2_CheckedChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!Page.IsPostBack)
			{
				BindDataGrid();
				
			}
			opt1.Attributes.Add("onclick","return showtable(this.id)");
			opt2.Attributes.Add("onclick","return showtable(this.id)");

		}
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

		private void opt2_CheckedChanged(object sender, System.EventArgs e)
		{
		
		}		
	
	}
}
