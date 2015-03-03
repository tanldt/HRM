namespace iHRPCore.MdlPR
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.Com;
	using iHRPCore.PRComponent;
	using iHRPCore.Include;
	/// <summary>
	///		Summary description for CalculateIncome.
	/// </summary>
	public class AttendanceIncome : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.HtmlControls.HtmlTableRow trGrid;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.HtmlControls.HtmlTable tblHeader;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.LinkButton btnPrint;
		protected System.Web.UI.WebControls.DropDownList cboCompanyID;
		protected System.Web.UI.WebControls.Label lblCompany;
		protected System.Web.UI.WebControls.DropDownList cboLevel1ID;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.Label lblFromDepartment;
		protected System.Web.UI.WebControls.DropDownList cboLevel2ID;
		protected System.Web.UI.WebControls.Label lblFromSection;
		protected System.Web.UI.WebControls.DropDownList cboLevel3ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel1ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel2ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel3ID;
		protected System.Web.UI.WebControls.TextBox txtMonth;
		protected System.Web.UI.WebControls.Label lblPeriod;
		public string strLanguage = "EN";
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
			if(!Page.IsPostBack)
			{
				txtMonth.Text = DateTime.Today.ToString("MM/yyyy");
				string strTextField = strLanguage == "VN"?"VNName":"Name";
				clsCommon.LoadDropDownListControl(cboCompanyID,"sp_GetDataCombo @TableName='LS_tblCompany',@Fields='LSCompanyCode," + strTextField + " as Name'","LSCompanyCode","Name",true);			
			}
			
			btnPrint.Attributes.Add("OnClick", " return validform()");		
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
			this.cboCompanyID.SelectedIndexChanged += new System.EventHandler(this.cboCompanyID_SelectedIndexChanged);
			this.cboLevel1ID.SelectedIndexChanged += new System.EventHandler(this.cboLevel1ID_SelectedIndexChanged);
			this.cboLevel2ID.SelectedIndexChanged += new System.EventHandler(this.cboLevel2ID_SelectedIndexChanged);
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		private void btnPrint_Click(object sender, System.EventArgs e)
		{		
			string strParams ="";
			string strValues = "";
			//////   Get params and Get Value of params
			strParams = "@Language;@EmpID;@EmpName;@CompanyID;@Level1ID;@Level2ID;@Level3ID;@PositionID;@JobCodeID;@LocationID;@Status;@SalPeriod";
			strParams += ";@Language1;@EmpID1;@EmpName1;@CompanyID1;@Level1ID1;@Level2ID1;@Level3ID1;@PositionID1;@JobCodeID1;@LocationID1;@Status1;@SalPeriod1";
			//---

			strValues += "EN";
			strValues += ";" + "";
			strValues += ";" + "";
			strValues += ";" + cboCompanyID.SelectedValue.Trim();
			strValues += ";" + cboLevel1ID.SelectedValue.Trim();
			strValues += ";" + cboLevel2ID.SelectedValue.Trim();
			strValues += ";" + cboLevel3ID.SelectedValue.Trim();
			strValues += ";" + "";
			strValues += ";" + "";
			strValues += ";" + "";
			strValues += ";" + "1";
			strValues += ";" + txtMonth.Text.Trim();
			strValues += ";" + strValues;
			Session["ssReportName"] = "PR_rptAttendancePayroll.rpt";
			Session["ssReportParams"] = strParams;
			Session["ssReportValues"] = strValues;
			clsCommon.OpenNewWindow(this.Page,"Reports/ShowReports.aspx","Payroll");
		}
		private void cboCompanyID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				string strCompanyID = cboCompanyID.SelectedValue.Trim();
				string strTextField = strLanguage == "VN"?"VNName":"Name";			
				clsCommon.LoadDropDownListControl(cboLevel1ID,"sp_GetDataCombo @TableName='LS_tblLevel1',@Fields='LSLevel1Code," + strTextField + " as Name',@Where=' and LSCompanyCode=N''" + strCompanyID.Trim() + "'''","LSLevel1Code","Name",true);
				cboLevel1ID.SelectedValue = this.txtLevel1ID.Value.Trim();
				cboLevel1ID_SelectedIndexChanged(null,null);
			}
			catch(Exception ex)
			{				
			}
		}

		private void cboLevel1ID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				string strLevel1ID = cboLevel1ID.SelectedValue.Trim();
				string strTextField = strLanguage == "VN"?"VNName":"Name";
				clsCommon.LoadDropDownListControl(cboLevel2ID,"sp_GetDataCombo @TableName='LS_tblLevel2',@Fields='LSLevel2Code," + strTextField + " as Name',@Where=' and LSLevel1Code=N''" + strLevel1ID.Trim() + "'''","LSLevel2Code","Name",true);
				cboLevel2ID.SelectedValue = this.txtLevel2ID.Value.Trim();
				cboLevel2ID_SelectedIndexChanged(null,null);
			}
			catch(Exception ex)
			{
			}
		}

		private void cboLevel2ID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				string strLevel2ID = cboLevel2ID.SelectedValue.Trim();
				string strTextField = strLanguage == "VN"?"VNName":"Name";
				clsCommon.LoadDropDownListControl(cboLevel3ID,"sp_GetDataCombo @TableName='LS_tblLevel3',@Fields='LSLevel3Code," + strTextField + " as Name',@Where=' and LSLevel2Code=N''" + strLevel2ID.Trim() + "'''","LSLevel3Code","Name",true);
				cboLevel3ID.SelectedValue = this.txtLevel3ID.Value.Trim();
			}
			catch(Exception ex)
			{				
			}
		}
	}
}
