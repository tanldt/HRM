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
	///		Summary description for Restructure.
	/// </summary>
	public class Restructure : System.Web.UI.UserControl
	{
		#region Declare
		public string strLanguage = "EN";
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.Label lblRelationID;
		protected System.Web.UI.WebControls.Label lblRelationEmpID;
		protected System.Web.UI.WebControls.Label lblOtherCompanyID;
		protected System.Web.UI.WebControls.Label lblLastName;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label lblStatusChange;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.Label Label16;
		protected System.Web.UI.WebControls.Label Label17;
		protected System.Web.UI.WebControls.Label Label18;
		protected System.Web.UI.WebControls.Label Label19;
		protected System.Web.UI.WebControls.Label Label20;
		protected System.Web.UI.WebControls.TextBox txtFromDate;
		protected System.Web.UI.WebControls.DropDownList cboStatusID;
		protected System.Web.UI.WebControls.DropDownList cboCompanyID;
		protected System.Web.UI.WebControls.DropDownList cboLevel1ID;
		protected System.Web.UI.WebControls.DropDownList cboLevel2ID;
		protected System.Web.UI.WebControls.DropDownList cboLevel3ID;
		protected System.Web.UI.WebControls.DropDownList cboFCompanyID;
		protected System.Web.UI.WebControls.DropDownList cboFLevel1ID;
		protected System.Web.UI.WebControls.DropDownList cboFLevel2ID;
		protected System.Web.UI.WebControls.DropDownList cboFLevel3ID;
		protected System.Web.UI.WebControls.DropDownList cboCJobClassID;
		protected System.Web.UI.WebControls.DropDownList cboCRegionalJobCodeID;
		protected System.Web.UI.WebControls.DropDownList cboRegionalJobCodeID;
		protected System.Web.UI.WebControls.DropDownList cboCLevelGradeID;
		protected System.Web.UI.WebControls.DropDownList cboCBuildingBlockID;
		protected System.Web.UI.WebControls.DropDownList cboBuildingBlockID;
		protected System.Web.UI.WebControls.DropDownList cboCRankID;
		protected System.Web.UI.HtmlControls.HtmlInputRadioButton optRestructure;
		protected System.Web.UI.HtmlControls.HtmlInputRadioButton optQuickUpdate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel1ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel2ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel3ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtFLevel1ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtFLevel2ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtFLevel3ID;
		protected System.Web.UI.WebControls.DropDownList cboJobClassID;
		protected System.Web.UI.WebControls.DropDownList cboLevelGradeID;
		protected System.Web.UI.WebControls.DropDownList cboRankID;
		protected System.Web.UI.WebControls.Label Label21;
		protected System.Web.UI.WebControls.DropDownList cboLocationID;
		protected System.Web.UI.WebControls.Label lblFromPosition;
		protected System.Web.UI.WebControls.DropDownList cboPositionID;
		protected System.Web.UI.WebControls.Label lblFromJobCode;
		protected System.Web.UI.WebControls.DropDownList cboJobCodeID;
		protected System.Web.UI.WebControls.Label Label14;
		#endregion Declare

		private void Page_Load(object sender, System.EventArgs e)
		{
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
			if(!Page.IsPostBack)
			{
				LoadDataCombo();
			}
			btnSave.Attributes.Add("OnClick", " return validform()");
			//btnDelete.Attributes["OnClick"]="return confirm('Delete checked records, are you sure?');";
		}
		private void LoadDataCombo()
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			clsCommon.LoadDropDownListControl(cboCompanyID,"sp_GetDataCombo @TableName='LS_tblCompany',@Fields='LSCompanyCode," + strTextField + " as Name'","LSCompanyCode","Name",true);
			clsCommon.LoadDropDownListControl(cboLocationID,"sp_GetDataCombo @TableName='LS_tblLocation',@Fields='LSLocationCode," + strTextField + " as Name'","LSLocationCode","Name",true);
			clsCommon.LoadDropDownListControl(cboPositionID,"sp_GetDataCombo @TableName='LS_tblPosition',@Fields='LSPositionCode," + strTextField + " as Name'","LSPositionCode","Name",true);
			clsCommon.LoadDropDownListControl(cboJobCodeID,"sp_GetDataCombo @TableName='LS_tblJobCode',@Fields='LSJobCodeCode," + strTextField + " as Name'","LSJobCodeCode","Name",true);			
			//---------------------------
			clsCommon.LoadDropDownListControl(cboFCompanyID,"sp_GetDataCombo @TableName='LS_tblCompany',@Fields='LSCompanyCode," + strTextField + " as Name'","LSCompanyCode","Name",true);
			clsCommon.LoadDropDownListControl(cboStatusID,"sp_GetDataCombo @TableName='LS_tblStatusChange',@Fields='LSStatusChangeCode," + strTextField + " as Name'","LSStatusChangeCode","Name",true);			
			clsCommon.LoadDropDownListControl(cboRegionalJobCodeID,"sp_GetDataCombo @TableName='LS_tblRegionalJobCode',@Fields='LSRegionalJobCodeCode," + strTextField + " as Name'","LSRegionalJobCodeCode","Name",true);
			clsCommon.LoadDropDownListControl(cboCRegionalJobCodeID,"sp_GetDataCombo @TableName='LS_tblRegionalJobCode',@Fields='LSRegionalJobCodeCode," + strTextField + " as Name'","LSRegionalJobCodeCode","Name",true);
			clsCommon.LoadDropDownListControl(cboJobClassID,"sp_GetDataCombo @TableName='LS_tblJobClass',@Fields='LSJobClassCode," + strTextField + " as Name'","LSJobClassCode","Name",true);
			clsCommon.LoadDropDownListControl(cboCJobClassID,"sp_GetDataCombo @TableName='LS_tblJobClass',@Fields='LSJobClassCode," + strTextField + " as Name'","LSJobClassCode","Name",true);
			clsCommon.LoadDropDownListControl(cboBuildingBlockID,"sp_GetDataCombo @TableName='LS_tblBuildingBlock',@Fields='LSBuildingBlockCode," + strTextField + " as Name'","LSBuildingBlockCode","Name",true);
			clsCommon.LoadDropDownListControl(cboCBuildingBlockID,"sp_GetDataCombo @TableName='LS_tblBuildingBlock',@Fields='LSBuildingBlockCode," + strTextField + " as Name'","LSBuildingBlockCode","Name",true);
			clsCommon.LoadDropDownListControl(cboCLevelGradeID,"sp_GetDataCombo @TableName='LS_tblLevelGrade',@Fields='LSLevelGradeCode," + strTextField + " as Name'","LSLevelGradeCode","Name",true);
			clsCommon.LoadDropDownListControl(cboLevelGradeID,"sp_GetDataCombo @TableName='LS_tblLevelGrade',@Fields='LSLevelGradeCode," + strTextField + " as Name'","LSLevelGradeCode","Name",true);
			clsCommon.LoadDropDownListControl(cboCRankID,"sp_GetDataCombo @TableName='LS_tblRank',@Fields='LSRankCode," + strTextField + " as Name'","LSRankCode","Name",true);
			clsCommon.LoadDropDownListControl(cboRankID,"sp_GetDataCombo @TableName='LS_tblRank',@Fields='LSRankCode," + strTextField + " as Name'","LSRankCode","Name",true);
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
			this.cboFCompanyID.SelectedIndexChanged += new System.EventHandler(this.cboFCompanyID_SelectedIndexChanged);
			this.cboFLevel1ID.SelectedIndexChanged += new System.EventHandler(this.cboFLevel1ID_SelectedIndexChanged);
			this.cboFLevel2ID.SelectedIndexChanged += new System.EventHandler(this.cboFLevel2ID_SelectedIndexChanged);
			this.cboCompanyID.SelectedIndexChanged += new System.EventHandler(this.cboCompanyID_SelectedIndexChanged);
			this.cboLevel1ID.SelectedIndexChanged += new System.EventHandler(this.cboLevel1ID_SelectedIndexChanged);
			this.cboLevel2ID.SelectedIndexChanged += new System.EventHandler(this.cboLevel2ID_SelectedIndexChanged);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

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
		private void cboFCompanyID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				string strCompanyID = cboFCompanyID.SelectedValue.Trim();
				string strTextField = strLanguage == "VN"?"VNName":"Name";
				clsCommon.LoadDropDownListControl(cboFLevel1ID,"sp_GetDataCombo @TableName='LS_tblLevel1',@Fields='LSLevel1Code," + strTextField + " as Name',@Where=' and LSCompanyCode=N''" + strCompanyID.Trim() + "'''","LSLevel1Code","Name",true);
				cboFLevel1ID.SelectedValue = this.txtFLevel1ID.Value.Trim();
				cboFLevel1ID_SelectedIndexChanged(null,null);
			}
			catch(Exception ex)
			{				
			}
		}

		private void cboFLevel1ID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				string strLevel1ID = cboFLevel1ID.SelectedValue.Trim();
				string strTextField = strLanguage == "VN"?"VNName":"Name";
				clsCommon.LoadDropDownListControl(cboFLevel2ID,"sp_GetDataCombo @TableName='LS_tblLevel2',@Fields='LSLevel2Code," + strTextField + " as Name',@Where=' and LSLevel1Code=N''" + strLevel1ID.Trim() + "'''","LSLevel2Code","Name",true);
				cboFLevel2ID.SelectedValue = this.txtFLevel2ID.Value.Trim();
				cboFLevel2ID_SelectedIndexChanged(null,null);
			}
			catch(Exception ex)
			{				
			}
		}

		private void cboFLevel2ID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				string strLevel2ID = cboFLevel2ID.SelectedValue.Trim();
				string strTextField = strLanguage == "VN"?"VNName":"Name";
				clsCommon.LoadDropDownListControl(cboFLevel3ID,"sp_GetDataCombo @TableName='LS_tblLevel3',@Fields='LSLevel3Code," + strTextField + " as Name',@Where=' and LSLevel2Code=N''" + strLevel2ID.Trim() + "'''","LSLevel3Code","Name",true);
				cboFLevel3ID.SelectedValue = this.txtFLevel3ID.Value.Trim();
			}
			catch(Exception ex)
			{				
			}
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{				
				if(optRestructure.Checked==true)
					clsCommon.ImpactDB("Restructure",this,"HR_spfrmWorkingRecord");
				else if(optQuickUpdate.Checked==true)
					clsCommon.ImpactDB("UpdateListEmp",this,"HR_spfrmWorkingRecord");
				optRestructure.Checked = true;
				optQuickUpdate.Checked = false;
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}
	}
}
