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
	///		Summary description for OtherInfo.
	/// </summary>
	public class OtherInfo : System.Web.UI.UserControl
	{
		#region Declare
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.CheckBox chkAdDistrinct;
		protected System.Web.UI.WebControls.Label lblT_Distrinct;
		protected System.Web.UI.WebControls.CheckBox chkAdProvince;
		protected System.Web.UI.WebControls.Label lblT_Province;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.CheckBox chkAdPermanent;
		protected System.Web.UI.WebControls.Label lblPermanentAddress;
		protected System.Web.UI.WebControls.TextBox txtP_Address;
		protected System.Web.UI.WebControls.TextBox txtP_District;
		protected System.Web.UI.WebControls.DropDownList cboP_LSProvinceID;
		protected System.Web.UI.WebControls.TextBox txtP_Phone;
		protected System.Web.UI.WebControls.CheckBox chkAdTemporary;
		protected System.Web.UI.WebControls.Label lblTemporaryAddress;
		protected System.Web.UI.WebControls.TextBox txtT_Address;
		protected System.Web.UI.WebControls.TextBox txtT_District;
		protected System.Web.UI.WebControls.DropDownList cboT_LSProvinceID;
		protected System.Web.UI.WebControls.TextBox txtT_Phone;
		protected System.Web.UI.WebControls.CheckBox chkAdEmergency;
		protected System.Web.UI.WebControls.Label lblEmergencyAddress;
		protected System.Web.UI.WebControls.TextBox txtE_Address;
		protected System.Web.UI.WebControls.TextBox txtE_District;
		protected System.Web.UI.WebControls.DropDownList cboE_LSProvinceID;
		protected System.Web.UI.WebControls.TextBox txtE_Phone;
		protected System.Web.UI.WebControls.Label lblEmergencyContact;
		protected System.Web.UI.WebControls.TextBox txtE_ContactName;
		protected System.Web.UI.WebControls.Label lblProvince;
		protected System.Web.UI.WebControls.DropDownList cboLSProvinceID;
		protected System.Web.UI.WebControls.Label lblDistrict;
		protected System.Web.UI.WebControls.CheckBox chkAdSkill;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtSkill;
		protected System.Web.UI.WebControls.CheckBox chkAdHobby;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.TextBox txtHobby;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.RadioButtonList rdHealthStatus;
		protected System.Web.UI.WebControls.CheckBox chkHealthMalformation;
		protected System.Web.UI.WebControls.TextBox txtHealthMalformationNote;
		protected System.Web.UI.WebControls.CheckBox chkHealthInjury;
		protected System.Web.UI.WebControls.TextBox txtHealthInjuryNote;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtNote;
		protected System.Web.UI.WebControls.CheckBox chkPayByBank;
		protected System.Web.UI.WebControls.Label lblBank;
		protected System.Web.UI.WebControls.DropDownList cboLSBankID;
		protected System.Web.UI.WebControls.Label lblAccountNo;
		protected System.Web.UI.WebControls.TextBox txtAccountNo;
		protected System.Web.UI.WebControls.Label lblAccountName;
		protected System.Web.UI.WebControls.TextBox txtAccountName;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label17;
		protected System.Web.UI.WebControls.TextBox txtAccountCode;
		protected System.Web.UI.WebControls.Label lblOtherInfomationTitle;
		protected System.Web.UI.WebControls.CheckBox chkAdJobBeforeRecruitment;
		protected System.Web.UI.WebControls.Label lblJobBeforeRecruitment;
		protected System.Web.UI.WebControls.TextBox txtJobBeforeRecruitment;
		protected System.Web.UI.WebControls.CheckBox chkAdLBNo;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.TextBox txtLabourBookNo;
		protected System.Web.UI.WebControls.CheckBox chkAdPoliticalArgument;
		protected System.Web.UI.WebControls.Label lblPoliticalArgument;
		protected System.Web.UI.WebControls.DropDownList cboLSPoliticalLevelID;
		protected System.Web.UI.WebControls.CheckBox chkAdCurrencyType;
		protected System.Web.UI.WebControls.Label cboCurrencyType;
		protected System.Web.UI.WebControls.DropDownList cboLSCurrencyTypeID;
		protected System.Web.UI.WebControls.CheckBox chkAdConferedTitle;
		protected System.Web.UI.WebControls.Label lblConferedTitle;
		protected System.Web.UI.WebControls.DropDownList cboLSHeroTitleID;
		protected System.Web.UI.WebControls.CheckBox chkInsurance;
		protected System.Web.UI.WebControls.CheckBox chkAdTax;
		protected System.Web.UI.WebControls.CheckBox ChkLifeInsurance;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnList;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdDistrinctchk;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdProvincechk;
		protected System.Web.UI.HtmlControls.HtmlTableRow trAdPermanent;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdDistrincttxt;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdProvincelbl;
		protected System.Web.UI.HtmlControls.HtmlTableRow trAdTemporary;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdDistrinctcbo;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdProvincetxt;
		protected System.Web.UI.HtmlControls.HtmlTableRow trAdEmergency;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdDistrinctlbl;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdProvincecbo;
		protected System.Web.UI.HtmlControls.HtmlTableRow trAdSkill;
		protected System.Web.UI.HtmlControls.HtmlTableRow trAdHobby;
		protected System.Web.UI.HtmlControls.HtmlTableRow trAdHealthStatus;
		protected System.Web.UI.HtmlControls.HtmlTableRow trbank;
		protected System.Web.UI.HtmlControls.HtmlTableRow trbankbranch;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdJobBeforeRecruitmentlbl;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdJobBeforeRecruitmenttxt;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdLBNolbl;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdLBNotxt;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdPoliticalArgumentlbl;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdPoliticalArgumentcbo;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdCurrencyTypelbl;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdCurrencyTypecbo;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdConferedTitlelbl;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdConferedTitlecbo;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdTaxlbl;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdTaxtxt;
		protected System.Web.UI.WebControls.DropDownList cboDistrictID;
		protected System.Web.UI.WebControls.TextBox txtLSDistrictID;
		protected System.Web.UI.WebControls.TextBox txtLSBankBranchID;
		protected System.Web.UI.WebControls.DropDownList cboBankBranchID;
		protected System.Web.UI.WebControls.CheckBox chkAdHealthStatus;
		protected System.Web.UI.WebControls.Label lblTaxcode1;
		protected System.Web.UI.WebControls.TextBox txtTaxCode;
		protected System.Web.UI.WebControls.CheckBox ChkLabourUnion;
		protected string strLanguage = "EN";
		#endregion Declare

		private void Page_Load(object sender, System.EventArgs e)
		{
			Session["CurFunction"] = Request.Params["FunctionID"].Trim();
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";			
			if(!Page.IsPostBack)
			{
				this.LoadDataCombo();
				this.LoadData();
			}
			btnSave.Attributes.Add("OnClick", " return validform()");
		}
		/// <summary>
		/// Get data for combo
		/// </summary>
		private void LoadDataCombo()
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			clsCommon.LoadDropDownListControl(cboP_LSProvinceID,"sp_GetDataCombo @TableName='LS_tblProvince',@Fields='LSProvinceID," + strTextField + " as Name'","LSProvinceID","Name",true);
			clsCommon.LoadDropDownListControl(cboT_LSProvinceID,"sp_GetDataCombo @TableName='LS_tblProvince',@Fields='LSProvinceID," + strTextField + " as Name'","LSProvinceID","Name",true);
			clsCommon.LoadDropDownListControl(cboE_LSProvinceID,"sp_GetDataCombo @TableName='LS_tblProvince',@Fields='LSProvinceID," + strTextField + " as Name'","LSProvinceID","Name",true);
			clsCommon.LoadDropDownListControl(cboLSProvinceID,"sp_GetDataCombo @TableName='LS_tblProvince',@Fields='LSProvinceID," + strTextField + " as Name'","LSProvinceID","Name",true);
			clsCommon.LoadDropDownListControl(cboLSBankID,"sp_GetDataCombo @TableName='LS_tblBank',@Fields='LSBankID," + strTextField + " as Name'","LSBankID","Name",true);			
			clsCommon.LoadDropDownListControl(cboLSPoliticalLevelID ,"sp_GetDataCombo @TableName='LS_tblPoliticalLevel',@Fields='LSPoliticalLevelID," + strTextField + " as Name'","LSPoliticalLevelID","Name",true);		
			clsCommon.LoadDropDownListControl(cboLSCurrencyTypeID,"sp_GetDataCombo @TableName='LS_tblCurrencyType' ,@Fields='LSCurrencyTypeID," + strTextField + " as Name'","LSCurrencyTypeID","Name",true);
			clsCommon.LoadDropDownListControl(cboLSHeroTitleID,"sp_GetDataCombo @TableName='LS_tblHeroTitle',@Fields='LSHeroTitleID," + strTextField + " as Name'","LSHeroTitleID","Name",true);		
		}
		/// <summary>
		/// Load employee info
		/// </summary>
		
		private void Display()
		{
			if(chkPayByBank.Checked)
			{
				trbank.Style.Add("Display","Block");
				trbankbranch.Style.Add("Display","Block");
			}
			else
			{
				trbank.Style.Add("Display","none");
				trbankbranch.Style.Add("Display","none");
			}
 
			if (chkHealthMalformation.Checked)
				this.txtHealthMalformationNote.Style.Add("Display","Block");
			else
				this.txtHealthMalformationNote.Style.Add("Display","none");

			if (chkHealthInjury.Checked)
				this.txtHealthInjuryNote.Style.Add("Display","Block");
			else
				this.txtHealthInjuryNote.Style.Add("Display","none");
		}
		
		private void LoadData()
		{
			try
			{
				DataRow iRow = clsHROtherInfo.GetDataByID(Session["EmpID"]);
				if(iRow !=null)
				{
					txtP_Address.Text = iRow["P_Address"].ToString();
					cboP_LSProvinceID.SelectedValue = iRow["P_LSProvinceID"].ToString();
					txtP_District.Text = iRow["P_District"].ToString();
					txtP_Phone.Text = iRow["P_Phone"].ToString();
					
					txtT_Address.Text = iRow["T_Address"].ToString();
					cboT_LSProvinceID.SelectedValue = iRow["T_LSProvinceID"].ToString();
					txtT_District.Text = iRow["T_District"].ToString();
					txtT_Phone.Text = iRow["T_Phone"].ToString();

					txtE_Address.Text = iRow["E_Address"].ToString();
					cboE_LSProvinceID.SelectedValue = iRow["E_LSProvinceID"].ToString();
					txtE_District.Text = iRow["E_District"].ToString();
					txtE_Phone.Text = iRow["E_Phone"].ToString();
					
					txtE_ContactName.Text = iRow["E_ContactName"].ToString();
					cboLSProvinceID.SelectedValue = iRow["LSProvinceID"].ToString();
					cboLSProvinceID_SelectedIndexChanged(null,null);					
					cboDistrictID.SelectedValue = iRow["LSDistrictID"].ToString();
					txtLSDistrictID.Text = iRow["LSDistrictID"].ToString();

					txtSkill.Text = iRow["Skill"].ToString();
					txtHobby.Text = iRow["Hobby"].ToString();

					chkHealthMalformation.Checked = iRow["HealthMalformation"].ToString()=="True"?true:false;
					txtHealthMalformationNote.Text = iRow["HealthMalformationNote"].ToString();
					chkHealthInjury.Checked = iRow["HealthInjury"].ToString()=="True"?true:false;
					txtHealthInjuryNote.Text = iRow["HealthInjuryNote"].ToString();
					txtNote.Text = iRow["Note"].ToString();
					
					chkPayByBank.Checked = iRow["PayByBank"].ToString()=="True"?true:false;
					cboLSBankID.SelectedValue = iRow["LSBankID"].ToString();					
					cboLSBankID_SelectedIndexChanged(null,null);
					cboBankBranchID.SelectedValue = iRow["LSBankBranchID"].ToString();
					txtLSBankBranchID.Text = iRow["LSBankBranchID"].ToString();
					txtAccountNo.Text = iRow["AccountNo"].ToString();
					txtAccountName.Text = iRow["AccountName"].ToString();
					txtAccountCode.Text = iRow["AccountCode"].ToString();
					
					txtJobBeforeRecruitment.Text= iRow["JobBeforeRecruitment"].ToString(); 
					txtLabourBookNo.Text = iRow["LabourBookNo"].ToString();
					cboLSPoliticalLevelID.SelectedValue=  iRow["LSPoliticalLevelID"].ToString(); 
					cboLSCurrencyTypeID.SelectedValue=  iRow["LSCurrencyTypeID"].ToString(); 
					cboLSHeroTitleID.SelectedValue=iRow["LSHeroTitleID"].ToString();   
					txtTaxCode.Text=iRow["TaxCode"].ToString();	
					chkInsurance.Checked = iRow["Insurance"].ToString()=="1"?true:false;
					ChkLifeInsurance.Checked = iRow["LifeInsurance"].ToString()=="True"?true:false;

					ChkLabourUnion.Checked = iRow["LabourUnion"].ToString()=="1"?true:false;
					
					/*
					//Hau
					strBankID = iRow["lsBankID"].ToString();					

					txtAccountNo.ToolTip = iRow["AccountNo"].ToString();
					txtAccountName.ToolTip = iRow["AccountName"].ToString();
					//txtBankBranch.ToolTip = iRow["BankBranch"].ToString();

					bSI_HI = chkInsurance.Checked;
					//bLifeIns = chkLifeInsurance.Checked;
					
					strCurrType = cboCurrentcyType.SelectedItem.Text;
					////
					///
					*/
					Display(); 
				}
				
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
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
			this.cboLSProvinceID.SelectedIndexChanged += new System.EventHandler(this.cboLSProvinceID_SelectedIndexChanged);
			this.cboLSBankID.SelectedIndexChanged += new System.EventHandler(this.cboLSBankID_SelectedIndexChanged);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnList.Click += new System.EventHandler(this.btnList_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			
			clsCommon.ImpactDB(Session["EmpID"].ToString().Trim(),"Update",this,"HR_spfrmEmpOther");
			//ImpactEmpOther();
			Display();			
		}

		private void ImpactEmpOther()
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "HR_spfrmEmp";

			try
			{				
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "UpdateEmpOther";
				cmd.Parameters.Add("@EmpID",SqlDbType.NVarChar,12).Value = Session["EmpID"].ToString().Trim();

				if (this.chkPayByBank.Checked)
				{
					cmd.Parameters.Add("@PayByBank",SqlDbType.Bit ).Value = 1;
					cmd.Parameters.Add("@LSBankID",SqlDbType.NVarChar, 12 ).Value = this.cboLSBankID.SelectedValue.Trim();
					cmd.Parameters.Add("@LSBankBranchID",SqlDbType.NVarChar, 12 ).Value = this.txtLSBankBranchID.Text.Trim();
					cmd.Parameters.Add("@AccountNo",SqlDbType.NVarChar, 20 ).Value = this.txtAccountNo.Text.Trim();
					cmd.Parameters.Add("@AccountName",SqlDbType.NVarChar, 80 ).Value = this.txtAccountName.Text.Trim();
					cmd.Parameters.Add("@AccountCode",SqlDbType.NVarChar, 20 ).Value = this.txtAccountCode.Text.Trim();					
				}
				else
				{
					cmd.Parameters.Add("@PayByBank",SqlDbType.Bit ).Value = 1;
					cmd.Parameters.Add("@LSBankID",SqlDbType.NVarChar, 12 ).Value = this.cboLSBankID.SelectedValue.Trim();
					cmd.Parameters.Add("@LSBankBranchID",SqlDbType.NVarChar, 12 ).Value = this.txtLSBankBranchID.Text.Trim();
					cmd.Parameters.Add("@AccountNo",SqlDbType.NVarChar, 20 ).Value = this.txtAccountNo.Text.Trim();
					cmd.Parameters.Add("@AccountName",SqlDbType.NVarChar, 80 ).Value = this.txtAccountName.Text.Trim();
					cmd.Parameters.Add("@AccountCode",SqlDbType.NVarChar, 20 ).Value = this.txtAccountCode.Text.Trim();
				}

				cmd.Parameters.Add("@LabourBookNo",SqlDbType.NVarChar, 20 ).Value = this.txtLabourBookNo.Text.Trim();
				cmd.Parameters.Add("@Insurance",SqlDbType.Bit ).Value = (this.chkInsurance.Checked == true)?1:0;
				cmd.Parameters.Add("@LifeInsurance",SqlDbType.Bit ).Value = (this.ChkLifeInsurance.Checked == true)?1:0;
				cmd.Parameters.Add("@LabourUnion",SqlDbType.Bit ).Value = (this.ChkLabourUnion.Checked == true)?1:0;
				cmd.ExecuteNonQuery();
				
				sqlTran.Commit();
				//return true;
			}
			catch (Exception exp)
			{
				string strErr = exp.Message;
				sqlTran.Rollback();
				//return false;
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();
			}
		}

		private void btnList_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(Request.ApplicationPath.Trim() + "/Editpage.aspx?ModuleID=HR&ParentID=8&FunctionID=14&Ascx=MdlHR/EmpList.ascx");		
		}

		
		#region Hau

		static public String strBankID = "";
		static public String strCurrType = "";
		static public Boolean bSI_HI = false;
		static public Boolean bLifeIns = false;
		
		static public Boolean bFlex = false;

		private void Save_ChangeLog()
		{	
			String strUserName = Session[0].ToString().Trim();
			String strActionTime = DateTime.Now.ToString();
			String strAction = "Edit";
			String strEmpID = Session["EmpID"].ToString().Trim();
			DataTable tbl = clsHREmpList.GetEmpList(Session["EmpID"].ToString().Trim(),this.Page,"0");
			String strEmpName = tbl.Rows[0]["EmpName"].ToString();
			String[][] strFieldChanged;
			strFieldChanged = new String[3][];
			strFieldChanged[0] = new String[8];
			strFieldChanged[1] = new String[8];
			strFieldChanged[2] = new String[8];
			int i = 0;
			if(!strBankID.Trim().Equals(cboLSBankID.SelectedItem.Text.Trim()))
			{
				strFieldChanged[0][i] = "Bank";
				strFieldChanged[1][i] = strBankID;
				strFieldChanged[2][i] = cboLSBankID.SelectedItem.Text;
				i++;
			}
			if(!txtAccountNo.ToolTip.Trim().Equals(txtAccountNo.Text.Trim()))
			{
				strFieldChanged[0][i] = "Account No";
				strFieldChanged[1][i] = txtAccountNo.ToolTip;
				strFieldChanged[2][i] = txtAccountNo.Text;
				i++;
			}
			if(!txtAccountName.ToolTip.Trim().Equals(txtAccountName.Text.Trim()))
			{
				strFieldChanged[0][i] = "Acc Name";
				strFieldChanged[1][i] = txtAccountName.ToolTip;
				strFieldChanged[2][i] = txtAccountName.Text;
				i++;
			}
			/*if(!txtBankBranch.ToolTip.Trim().Equals(txtBankBranch.Text.Trim()))
			{
				strFieldChanged[0][i] = "Address";
				strFieldChanged[1][i] = txtBankBranch.ToolTip;
				strFieldChanged[2][i] = txtBankBranch.Text;
				i++;
			}*/

			if(bSI_HI != chkInsurance.Checked)
			{
				strFieldChanged[0][i] = "SI & HI";
				strFieldChanged[1][i] = bSI_HI.ToString();;
				strFieldChanged[2][i] = chkInsurance.Checked.ToString();
				i++;
			}
			if(!strCurrType.Trim().Equals(cboLSCurrencyTypeID.SelectedItem.Text.Trim()))
			{
				strFieldChanged[0][i] = "Currency Type";
				strFieldChanged[1][i] = strCurrType;
				strFieldChanged[2][i] = cboLSCurrencyTypeID.SelectedItem.Text;
				i++;
			}

			SaveLog(strUserName, strActionTime, strEmpID, strEmpName, strAction, strFieldChanged, i);
		}

		private void SaveLog(String UserName, String ActionTime, String EmpID, String EmpName, String UserAction, String[][] FieldChanged, int FieldCount)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			String CommandText = "insert into ActionLog values(N'";
			CommandText += (UserName + "', '" + ActionTime + "', N'" + UserAction + "', '" + EmpID + "', N'" + EmpName + "', N'");
			try
			{
				for(int i=0; i<FieldCount; i++)
				{
					cmd.CommandText = CommandText + FieldChanged[0][i] + "', N'" + FieldChanged[1][i] + "', N'" + FieldChanged[2][i] + "')";
					cmd.ExecuteNonQuery();
				}
				sqlTran.Commit();
			}
			catch //(Exception exp)
			{
				//lblErr.Text = exp.Message;
				sqlTran.Rollback();
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();
			}
		}

		#endregion
		
		private void cboLSBankID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				string strBankID = cboLSBankID.SelectedValue.Trim();
				string strTextField = strLanguage == "VN"?"VNName":"Name";
				clsCommon.LoadDropDownListControl(cboBankBranchID,"sp_GetDataCombo @TableName='LS_tblBankBranch',@Fields='LSBankBranchID," + strTextField + " as Name',@Where=' and LSBankID=N''" + strBankID.Trim() + "'''","LSBankBranchID","Name",true);
				cboBankBranchID.SelectedValue = this.txtLSBankBranchID.Text.Trim();
			}
			catch(Exception ex)
			{				
			}
		}

		private void cboLSProvinceID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				string strProvincID = cboLSProvinceID.SelectedValue.Trim();
				string strTextField = strLanguage == "VN"?"VNName":"Name";
				clsCommon.LoadDropDownListControl(cboDistrictID,"sp_GetDataCombo @TableName='LS_tblDistrict',@Fields='LSDistrictID," + strTextField + " as Name',@Where=' and LSProvinceID=N''" + strProvincID.Trim() + "'''","LSDistrictID","Name",true);
				cboDistrictID.SelectedValue = this.txtLSDistrictID.Text.Trim();				
			}
			catch(Exception ex)
			{
			}
		}

	}
}
