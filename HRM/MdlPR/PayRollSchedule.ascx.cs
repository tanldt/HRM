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
	/// <summary>
	///		Summary description for CalculateIncome.
	/// </summary>
	public class PayRollSchedule : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label lblNote;
		protected System.Web.UI.WebControls.Label lblCompany;
		protected System.Web.UI.WebControls.DropDownList cboCompanyID;
		protected System.Web.UI.WebControls.Label lblFromSection;
		protected System.Web.UI.WebControls.DropDownList cboLevel3ID;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList cboLevel1ID;
		protected System.Web.UI.WebControls.Label lblFromDepartment;
		protected System.Web.UI.WebControls.DropDownList cboLevel2ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel1ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel2ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel3ID;
		protected System.Web.UI.WebControls.TextBox txtMonth;
		protected System.Web.UI.WebControls.LinkButton btnCalculate;
		protected System.Web.UI.WebControls.LinkButton btnPaySlip;
		protected System.Web.UI.WebControls.LinkButton btnSchedule;
		protected System.Web.UI.HtmlControls.HtmlTableRow trGrid;
		protected System.Web.UI.WebControls.Label EmpID;
		protected System.Web.UI.WebControls.TextBox txtEmpID;
		public string strLanguage = "EN";
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";			
			if(!Page.IsPostBack)
			{
				LoadDataCombo();								
			}
			btnCalculate.Attributes.Add("OnClick", " return validform()");
			btnPaySlip.Attributes.Add("OnClick", " return validform()");
			btnSchedule.Attributes.Add("OnClick", " return validform()");
		}
		private void LoadDataCombo()
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			clsCommon.LoadDropDownListControl(cboCompanyID,"sp_GetDataCombo @TableName='LS_tblCompany',@Fields='LSCompanyCode," + strTextField + " as Name'","LSCompanyCode","Name",true);			
			//cboCompanyID_SelectedIndexChanged(null,null);
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
			this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
			this.btnPaySlip.Click += new System.EventHandler(this.btnPaySlip_Click);
			this.btnSchedule.Click += new System.EventHandler(this.btnSchedule_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

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

		private void btnCalculate_Click(object sender, System.EventArgs e)
		{
			try
			{
//				string strReturn = clsPayroll.CalculateIncome(txtMonth.Text.Trim(),txtEmpID.Text.Trim(),cboLevel1ID.SelectedValue.Trim(),cboLevel2ID.SelectedValue.Trim()
//					,cboLevel3ID.SelectedValue.Trim(),cboCompanyID.SelectedValue.Trim());
//				if(strReturn.Trim() =="")
//					lblErr.Text = "Calculate completed!";
//				else
//					lblErr.Text = strReturn ;
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}

		private void btnPaySlip_Click(object sender, System.EventArgs e)
		{
		
		}

		private void btnSchedule_Click(object sender, System.EventArgs e)
		{
			DataTable dtPayroll = new DataTable();
			try
			{
//				dtPayroll = clsPayroll.GetPayRollSchedule(txtMonth.Text.Trim(),txtEmpID.Text.Trim(),cboLevel1ID.SelectedValue.Trim(),cboLevel2ID.SelectedValue.Trim()
//					,cboLevel3ID.SelectedValue.Trim(),cboCompanyID.SelectedValue.Trim());
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}
	}
}
