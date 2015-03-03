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
	using System.Data.SqlClient;
	using System.Configuration;

	/// <summary>
	///		Summary description for SocialActivity.
	/// </summary>
	public class SocialActivity : System.Web.UI.UserControl
	{
		protected string strLanguage = "VN";
		protected string strLangguage="VN";
		protected DataTable tblRelation ;
		protected System.Web.UI.WebControls.Label lblPoliticsAndSociety;
		protected System.Web.UI.WebControls.CheckBox chkShowAbroad;
		protected System.Web.UI.WebControls.Label lblLevel;
		protected System.Web.UI.WebControls.TextBox txtArmyLevel;
		protected System.Web.UI.WebControls.Label lblArmyPosition;
		protected System.Web.UI.WebControls.TextBox txtArmyPosition;
		protected System.Web.UI.WebControls.Label lblArmyDivision;
		protected System.Web.UI.WebControls.TextBox txtArmyDivision;
		protected System.Web.UI.WebControls.Label lblArmyDept;
		protected System.Web.UI.WebControls.TextBox txtArmyDept;
		protected System.Web.UI.WebControls.Label lblDateInArmy;
		protected System.Web.UI.WebControls.TextBox txtDateInArmy;
		protected System.Web.UI.WebControls.Label lblDateOutArmy;
		protected System.Web.UI.WebControls.TextBox txtDateOutArmy;
		protected System.Web.UI.WebControls.Label lblProbationDate;
		protected System.Web.UI.WebControls.Label lblStandard;
		protected System.Web.UI.WebControls.Label lblProcess;
		protected System.Web.UI.WebControls.Label lblTradeUnionist;
		protected System.Web.UI.WebControls.Label lblLabourUnionDescript;
		protected System.Web.UI.WebControls.Label lblSoldierStatus;
		protected System.Web.UI.WebControls.TextBox txtSoldierStatus;
		protected System.Web.UI.WebControls.Label lblProfileSoldier;
		protected System.Web.UI.WebControls.TextBox txtProfileSoldier;
		protected System.Web.UI.WebControls.Label lblLevelSoldier;
		protected System.Web.UI.WebControls.TextBox txtLevelSoldier;
		protected System.Web.UI.WebControls.Label lblWoundedSoldier;
		protected System.Web.UI.WebControls.TextBox txtWoundedSoldierDate;
		protected System.Web.UI.WebControls.Label lblDescription;
		protected System.Web.UI.WebControls.Label lblRegime;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnList;
		protected System.Web.UI.HtmlControls.HtmlTableRow trArmy1;
		protected System.Web.UI.HtmlControls.HtmlTableRow trArmy2;
		protected System.Web.UI.HtmlControls.HtmlTableRow trArmy3;
		protected System.Web.UI.HtmlControls.HtmlTableRow trPartyMember;
		protected System.Web.UI.HtmlControls.HtmlTableRow trProcess;
		protected System.Web.UI.HtmlControls.HtmlTableRow trUnionMember;
		protected System.Web.UI.HtmlControls.HtmlTableRow trLabourUnion;
		protected System.Web.UI.HtmlControls.HtmlTableRow trWoundedSoldier;
		protected System.Web.UI.HtmlControls.HtmlTableRow trWoundedSoldier2;
		protected System.Web.UI.HtmlControls.HtmlTableRow trDescript;
		protected System.Web.UI.HtmlControls.HtmlTableRow trRegime;
		protected System.Web.UI.WebControls.DataGrid dtgList;
		protected System.Web.UI.WebControls.CheckBox chkIsArmy;
		protected System.Web.UI.WebControls.CheckBox chkIsCU;
		protected System.Web.UI.WebControls.TextBox txtCU_Probationdate;
		protected System.Web.UI.WebControls.TextBox txtCU_StandardDate;
		protected System.Web.UI.WebControls.TextBox txtCU_Process;
		protected System.Web.UI.WebControls.CheckBox chkIsCYU;
		protected System.Web.UI.WebControls.TextBox txtCYU_Date;
		protected System.Web.UI.WebControls.CheckBox chkIsLBU;
		protected System.Web.UI.WebControls.TextBox txtLBU_Description;
		protected System.Web.UI.WebControls.CheckBox chkIsWoundedSoldier;
		protected System.Web.UI.WebControls.TextBox txtWoundDescript;
		protected System.Web.UI.WebControls.CheckBox chkIsRegime;
		protected System.Web.UI.WebControls.TextBox txtRegimeDescript;
		protected System.Web.UI.WebControls.LinkButton btnGoAbroad;
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.HtmlControls.HtmlTableRow trGoAbroad1;
		protected System.Web.UI.HtmlControls.HtmlTableRow trGoAbroad2;
		protected DataTable tblCountry ;

		private void Page_Load(object sender, System.EventArgs e)
		{
			Session["CurFunction"] = Request.Params["FunctionID"].Trim();
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"VN";	
			
			if (!Page.IsPostBack)
			{
				//tblCountry = clsCommon.GetDataTable("sp_GetDataCombo @TableName='ls_tblNationality',@Fields='LSNationalityCode,Name'");
				//tblRelation = clsCommon.GetDataTable("sp_GetDataCombo @TableName='LS_tblRelationship',@Fields='LSRelationshipCode, Name'");			
				LoadData();
			}
			btnGoAbroad.Attributes.Add("OnClick","return PopUp_Addnew()");	
			btnSave.Attributes.Add("OnClick","return validform()");
		}
				
		# region  Load data by empid
		private void LoadData()
		{
			if (Session["EmpID"]==null) return;

			DataRow iRow = clsHRSocialActivity.GetDataByEmpID(Session["EmpID"].ToString());

			if (iRow == null) return;
			try
			{
				this.chkIsArmy.Checked = iRow["IsArmy"].ToString()=="True"?true:false;
				this.txtArmyLevel.Text = iRow["ArmyLevel"].ToString();
				this.txtArmyPosition.Text = iRow["ArmyPosition"].ToString();
				this.txtArmyDivision.Text = iRow["ArmyDivision"].ToString(); 
				this.txtArmyDept.Text = iRow["ArmyDept"].ToString(); 
				this.txtDateInArmy.Text = iRow["DateInArmy"].ToString(); 
				this.txtDateOutArmy.Text = iRow["DateOutArmy"].ToString(); 
				this.chkIsCU.Checked = iRow["IsCU"].ToString()=="True"?true:false;
				this.txtCU_Probationdate.Text = iRow["CU_Probationdate"].ToString(); 
				this.txtCU_StandardDate.Text = iRow["CU_StandardDate"].ToString(); 
				this.txtCU_Process.Text = iRow["CU_Process"].ToString(); 
				this.chkIsCYU.Checked= iRow["IsCYU"].ToString()=="True"?true:false;
				this.txtCYU_Date.Text = iRow["CYU_Date"].ToString(); 
				this.chkIsLBU.Checked = iRow["IsLBU"].ToString()=="True"?true:false;
				this.txtLBU_Description.Text = iRow["LBU_Description"].ToString(); 
				this.chkIsWoundedSoldier.Checked= iRow["IsWoundedSoldier"].ToString()=="True"?true:false;
				this.txtSoldierStatus.Text = iRow["SoldierStatus"].ToString(); 
				this.txtProfileSoldier.Text = iRow["ProfileSoldier"].ToString(); 
				this.txtLevelSoldier.Text = iRow["LevelSoldier"].ToString(); 
				this.txtWoundedSoldierDate.Text = iRow["WoundedSoldierDate"].ToString(); 
				this.txtWoundDescript.Text = iRow["WoundDescript"].ToString(); 
				this.chkIsRegime.Checked= iRow["IsRegime"].ToString()=="True"?true:false;
				this.txtRegimeDescript.Text = iRow["RegimeDescript"].ToString(); 				

			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		
			iRow = clsCommon.GetDataRow ("HR_spfrmAbroadRecord @Activity = 'ShowAbroad', @EmpID = '" + clsCommon.SafeDataString(Session["EmpID"].ToString()) + "'");
			this.chkShowAbroad.Checked = iRow["ShowAbroad"].ToString()=="True"?true:false;

			Display();
		}
		#endregion


		private void  Display()
		{
			if (this.chkShowAbroad.Checked)
			{
				trGoAbroad1.Style.Add("Display","Block");
				trGoAbroad2.Style.Add("Display","Block");
			}
			else
			{			
				trGoAbroad1.Style.Add("Display","none");
				trGoAbroad2.Style.Add("Display","none");			
			}

			if (this.chkIsArmy.Checked)
			{
				trArmy1.Style.Add("Display","Block");
				trArmy2.Style.Add("Display","Block");
				trArmy3.Style.Add("Display","Block");
			}
			else
			{
				trArmy1.Style.Add("Display","none");
				trArmy2.Style.Add("Display","none");
				trArmy3.Style.Add("Display","none");
			}

			if (this.chkIsCU.Checked)
			{
				trPartyMember.Style.Add("Display","Block");
				trProcess.Style.Add("Display","Block");
			}
			else
			{
				trPartyMember.Style.Add("Display","none");
				trProcess.Style.Add("Display","none");
			}

			if (this.chkIsCYU.Checked)
			{
				trUnionMember.Style.Add("Display","Block");
			}
			else
			{
				trUnionMember.Style.Add("Display","none");
			}
			if (this.chkIsLBU.Checked)
			{
				trLabourUnion.Style.Add("Display","Block");
			}
			else
			{
				trLabourUnion.Style.Add("Display","none");
			}

			if (this.chkIsWoundedSoldier.Checked)
			{
				trWoundedSoldier.Style.Add("Display","Block");
				trWoundedSoldier2.Style.Add("Display","Block");
				trDescript.Style.Add("Display","Block");
			}
			else
			{
				trWoundedSoldier.Style.Add("Display","none");
				trWoundedSoldier2.Style.Add("Display","none");
				trDescript.Style.Add("Display","none");
			}

			if (this.chkIsRegime.Checked)
			{
				trRegime.Style.Add("Display","Block");
			}
			else
			{
				trRegime.Style.Add("Display","none");
			}
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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnList.Click += new System.EventHandler(this.btnList_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			clsCommon.ImpactDB(Session["EmpID"].ToString().Trim(),"Update",this,"HR_spfrmSocialActivity");			
			Display();
		}

		
		private void btnList_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(Request.ApplicationPath.Trim() + "/Editpage.aspx?ModuleID=HR&ParentID=8&FunctionID=14&Ascx=MdlHR/EmpList.ascx");		
		}

		
		
		
	}
}
