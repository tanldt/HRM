namespace iHRPCore.MdlHR
{
	using System;
	using System.Configuration;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.Com;
	using iHRPCore.HRComponent;
	using System.Data.SqlClient;	

	/// <summary>
	///		Summary description for Personal.
	/// </summary>
	public class Personal : System.Web.UI.UserControl
	{
		#region Declare

		protected System.Web.UI.WebControls.LinkButton btnList;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnChangID;
		protected System.Web.UI.WebControls.DropDownList cboLSMajorCode;
		protected System.Web.UI.WebControls.Label lblMajorID;		
		protected System.Web.UI.WebControls.Label lblMajorLevelID;
		protected System.Web.UI.WebControls.TextBox txtEmail;
		protected System.Web.UI.WebControls.Label lblEmail;
		protected System.Web.UI.WebControls.Label lblCultureLevelID;
		protected System.Web.UI.WebControls.TextBox txtIDIssuedDate;
		protected System.Web.UI.WebControls.Label lblIssuedDate;
		protected System.Web.UI.WebControls.DropDownList cboIDIssuedPlace;
		protected System.Web.UI.WebControls.Label lblIssuedPlace;
		protected System.Web.UI.WebControls.TextBox txtIDNo;
		protected System.Web.UI.WebControls.Label lblIDCardNo;
		protected System.Web.UI.WebControls.Label lblReligionID;
		protected System.Web.UI.WebControls.Label lblEthnicID;
		protected System.Web.UI.WebControls.Label lblNationID;
		protected System.Web.UI.WebControls.Label lblMaritalID;		
		protected System.Web.UI.WebControls.Label lblNative_ProvinceID;		
		protected System.Web.UI.WebControls.Label lblBorn_ProvinceID;
		protected System.Web.UI.WebControls.DropDownList cboGender;
		protected System.Web.UI.WebControls.Label lblGender;
		protected System.Web.UI.WebControls.TextBox txtAge;
		protected System.Web.UI.WebControls.Label lblAge;
		protected System.Web.UI.WebControls.TextBox txtDOB;
		protected System.Web.UI.WebControls.Label lblDOB;
		protected System.Web.UI.WebControls.TextBox txtVFirstName;
		protected System.Web.UI.WebControls.Label lblFirstName;
		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Button btnUpload;
		protected System.Web.UI.WebControls.Image imgEmp;
		protected System.Web.UI.WebControls.TextBox txtVLastName;
		protected System.Web.UI.WebControls.Label lblLastName;
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.HtmlControls.HtmlInputFile fileUpload;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtCard;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtReturnValue;
		protected System.Web.UI.WebControls.Label lblNickName;
		protected System.Web.UI.WebControls.TextBox txtNickName;
		protected System.Web.UI.WebControls.Label lblDistrict;
		protected System.Web.UI.WebControls.Label lblCityProvince;
		protected System.Web.UI.WebControls.Label lblDistrict2;
		protected System.Web.UI.WebControls.Label lblCityProvince2;
		protected System.Web.UI.WebControls.Label lblSocialClass;
		protected System.Web.UI.WebControls.Label lblPersonalElement;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtOldCardID;
		protected System.Web.UI.WebControls.TextBox txtMobifone;
		protected System.Web.UI.WebControls.TextBox txtMajor;
		protected System.Web.UI.WebControls.DropDownList cboLSCultureLevelID;
		protected System.Web.UI.WebControls.DropDownList cboLSEthnicID;
		protected System.Web.UI.WebControls.DropDownList cboLSMaritalID;
		protected System.Web.UI.WebControls.DropDownList cboLSReligionID;
		protected System.Web.UI.WebControls.DropDownList cboLSNationalityID;
		protected System.Web.UI.WebControls.DropDownList cboLSFamilyBackgroundID;
		protected System.Web.UI.WebControls.DropDownList cboLSQualifiMajorID;
		protected System.Web.UI.WebControls.TextBox txtBorn_District;
		protected System.Web.UI.WebControls.TextBox txtNative_District;
		protected System.Web.UI.WebControls.DropDownList cboLSPersonalBackgroundID;
		protected System.Web.UI.WebControls.DropDownList cboBorn_LSProvinceID;
		protected System.Web.UI.WebControls.DropDownList cboNative_LSProvinceID;
		protected System.Web.UI.WebControls.DropDownList cboLSCurrencyTypeID;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.DropDownList cboLSShiftID;
		protected System.Web.UI.WebControls.TextBox txtScanCode;
		protected System.Web.UI.WebControls.TextBox txtScanTimeNo;
		protected System.Web.UI.WebControls.Button btnTemp;
		protected System.Web.UI.WebControls.CheckBox chkAdBorn_District;
		protected System.Web.UI.WebControls.CheckBox chkAdBorn_LSProvinceID;
		protected System.Web.UI.WebControls.CheckBox chkAdNative_District;
		protected System.Web.UI.WebControls.CheckBox chkAdNative_LSProvinceID;
		public string strLanguage = "VN";
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdNative_Districtlbl;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdNative_Districttxt;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdNative_LSProvinceIDlbl;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdNative_LSProvinceIDcbo;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdBorn_Districtlbl;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdBorn_Districtcbo;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdBorn_LSProvinceIDlbl;
		protected System.Web.UI.WebControls.CheckBox chkAdPermanent;
		protected System.Web.UI.WebControls.Label lblPermanentAddress;
		protected System.Web.UI.WebControls.TextBox txtP_Address;
		protected System.Web.UI.WebControls.TextBox txtP_District;
		protected System.Web.UI.WebControls.DropDownList cboP_LSProvinceID;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.CheckBox chkAdDistrinct;
		protected System.Web.UI.WebControls.Label lblT_Distrinct;
		protected System.Web.UI.WebControls.CheckBox chkAdProvince;
		protected System.Web.UI.WebControls.Label lblT_Province;
		protected System.Web.UI.WebControls.CheckBox chkAdTemporary;
		protected System.Web.UI.WebControls.Label lblTemporaryAddress;
		protected System.Web.UI.WebControls.TextBox txtT_Address;
		protected System.Web.UI.WebControls.TextBox txtT_District;
		protected System.Web.UI.WebControls.DropDownList cboT_LSProvinceID;
		protected System.Web.UI.WebControls.CheckBox ChkAdAddress;
		protected System.Web.UI.HtmlControls.HtmlTableRow trAdAddress;
		protected System.Web.UI.HtmlControls.HtmlTableRow trAdPermanent;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdDistrincttxt;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdProvincelbl;
		protected System.Web.UI.HtmlControls.HtmlTableRow trAdTemporary;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdDistrinctcbo;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdProvincetxt;
		protected System.Web.UI.WebControls.CheckBox chkResidence;
		protected System.Web.UI.WebControls.LinkButton btnAddnew_Residence;
		protected System.Web.UI.WebControls.DataGrid dtgList;
		protected System.Web.UI.WebControls.CheckBox chkAdResidence;
		protected System.Web.UI.WebControls.CheckBox chkAdResidence2;
		protected System.Web.UI.WebControls.CheckBox chkAdResidence3;
		protected System.Web.UI.HtmlControls.HtmlTableRow trAdResidence;
		protected System.Web.UI.HtmlControls.HtmlTableRow trAdResidence2;
		protected System.Web.UI.HtmlControls.HtmlTableRow trAdResidence3;
		protected System.Web.UI.HtmlControls.HtmlTableRow trAdResidence4;
		protected System.Web.UI.WebControls.CheckBox chkAdResidence4;		
		protected System.Web.UI.WebControls.Label lblMobile;		
		protected System.Web.UI.WebControls.DropDownList cboPassWithLevel;
		protected System.Web.UI.WebControls.TextBox txtHomePhone;		
		protected System.Web.UI.WebControls.TextBox txtAccountNo;		
		protected System.Web.UI.WebControls.TextBox txtAccountName;
		protected System.Web.UI.WebControls.TextBox txtTaxCode;		
		protected System.Web.UI.WebControls.DropDownList Dropdownlist1;		
		protected System.Web.UI.WebControls.TextBox txtCostCode;		
		protected System.Web.UI.WebControls.DropDownList cboLSBankBranchID;

		protected System.Web.UI.WebControls.TextBox txtPass_Number;		
		protected System.Web.UI.WebControls.TextBox txtPass_IssuedDate;		
		protected System.Web.UI.WebControls.TextBox txtPass_ExpiredDate;		
		protected System.Web.UI.WebControls.DropDownList cboPass_IssuedPlace;		
		protected System.Web.UI.WebControls.TextBox txtEmergencyContact;		
		protected System.Web.UI.WebControls.DropDownList cboLSBloodTypeID;		
		protected System.Web.UI.WebControls.TextBox txtMentor;		
		protected System.Web.UI.WebControls.TextBox txtPerformanceManager;		
		protected System.Web.UI.WebControls.TextBox txtFaculty;			
		protected System.Web.UI.WebControls.TextBox txtELastName;		
		protected System.Web.UI.WebControls.TextBox txtEFirstName;		
		protected System.Web.UI.WebControls.Label Label24;
		protected System.Web.UI.WebControls.Label Label25;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.Label Label16;
		protected System.Web.UI.WebControls.Label Label17;
		protected System.Web.UI.WebControls.Label Label18;
		protected System.Web.UI.WebControls.Label Label19;
		protected System.Web.UI.WebControls.Label Label20;
		protected System.Web.UI.WebControls.Label Label21;
		protected System.Web.UI.WebControls.Label Label22;
		protected System.Web.UI.WebControls.Label Label23;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.Label Label14;
		protected System.Web.UI.WebControls.Label Label10;	
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdBorn_LSProvinceIDcbo;		
		protected System.Web.UI.WebControls.TextBox txtStartDate;		
		protected System.Web.UI.WebControls.CheckBox chkInsurance;		
		protected System.Web.UI.WebControls.CheckBox chkLifeInsurance;		
		protected System.Web.UI.WebControls.CheckBox chkLabourUnion;
		protected System.Web.UI.WebControls.Label Label26;
		protected System.Web.UI.WebControls.Label lblOtherInfomationTitle;
		protected System.Web.UI.WebControls.Label Label27;
		protected System.Web.UI.WebControls.DropDownList cboLSQualifiMajorID2;
		protected System.Web.UI.WebControls.Label Label28;
		protected System.Web.UI.WebControls.DropDownList cboPassWithLevel2;
		protected System.Web.UI.WebControls.Label Label29;
		protected System.Web.UI.WebControls.Label Label30;
		protected System.Web.UI.WebControls.Label Label31;
		protected System.Web.UI.WebControls.TextBox txtAccountNo2;
		protected System.Web.UI.WebControls.DropDownList cboLSBankBranchID2;
		protected System.Web.UI.WebControls.TextBox txtAccountName2;
		protected System.Web.UI.WebControls.Label Label32;
		protected System.Web.UI.WebControls.TextBox txtEmpCode2;
		protected System.Web.UI.WebControls.CheckBox chkBHYT;		
		protected System.Web.UI.WebControls.CheckBox chkBHTN;		
		#endregion Declare
		private void Page_Load(object sender, System.EventArgs e)
		{
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"VN";
			Session["CurFunction"] = Request.Params["FunctionID"].Trim();						
			if(Request.Params["EmpID"]!=null)
				Session["EmpID"] = Request.Params["EmpID"].Trim();

			if(!Page.IsPostBack)
			{				
				this.LoadDataCombo();//Load for All combobox in Personal Form
				this.LoadDataDefault();
				LoadDataGrid_Residence(Session["EmpID"]);
			}			
			btnSave.Attributes.Add("OnClick", " return validform()");
			btnChangID.Attributes.Add("onclick", "return OpenEditCode()");
			//txtVLastName.Attributes["onChange"] = "javascript:this.value=this.value.toUpperCase();";
			//txtVFirstName.Attributes["onChange"] = "javascript:this.value=this.value.toUpperCase();";				
			btnAddnew_Residence.Attributes.Add("OnClick","return PopUp_Addnew()");			
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
			this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
			this.btnTemp.Click += new System.EventHandler(this.btnTemp_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnList.Click += new System.EventHandler(this.btnList_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		/// <summary>
		/// Load employee info
		/// </summary>
		private void LoadDataDefault()
		{
			try
			{
				DataRow iRow = clsHRPersonal.GetDataByID(Session["EmpID"]);
				if(iRow !=null)
				{
					txtEmpCode2.Text = iRow["EmpCode2"].ToString();
					txtDOB.Text = iRow["DOB"].ToString();
					if(iRow["DOB"].ToString().Trim() != "")
					{						
						txtAge.Text = Convert.ToString(DateTime.Today.Year - Convert.ToInt32(iRow["DOB"].ToString().Substring(7)));
					}
					txtEmail.Text = iRow["Email"].ToString();
					txtIDIssuedDate.Text = iRow["IDIssuedDate"].ToString();
					cboGender.SelectedValue=iRow["Gender"].ToString();
					txtIDNo.Text = iRow["IDNo"].ToString();
					txtIDNo.ToolTip = iRow["IDNo"].ToString();
					txtMobifone.Text = iRow["Mobifone"].ToString();
					txtVFirstName.Text = iRow["VFirstName"].ToString().Trim();
					txtVLastName.Text = iRow["VLastName"].ToString().Trim();
					txtNickName.Text= iRow["NickName"].ToString().Trim();
					txtBorn_District.Text=iRow["Born_District"].ToString();
					try
					{
						cboBorn_LSProvinceID.SelectedValue=iRow["Born_LSProvinceID"].ToString();
					} 
					catch {}
					txtNative_District.Text=iRow["Native_District"].ToString();
					try {
						cboNative_LSProvinceID.SelectedValue=iRow["Native_LSProvinceID"].ToString();
					} 
					catch {}
					try {
					cboIDIssuedPlace.SelectedValue = iRow["IDIssuedPlace"].ToString();
					} 
					catch {}
					try 
					{
						cboLSCultureLevelID.SelectedValue = iRow["LSCultureLevelID"].ToString();
					}
					catch {}
					try 
					{
						cboLSEthnicID.SelectedValue = iRow["LSEthnicID"].ToString();				
					}catch {}
					try 
					{
						cboLSPersonalBackgroundID.SelectedValue=iRow["LSPersonalBackgroundID"].ToString();			
					}catch {}
					txtMajor.Text = iRow["Major"].ToString();
					try 
					{
						cboLSQualifiMajorID.SelectedValue = iRow["LSQualifiMajorID"].ToString();
					}catch {}
					try 
					{
						cboLSQualifiMajorID2.SelectedValue = iRow["LSQualifiMajorID2"].ToString();
					}catch {}
					try 
					{
						cboLSMaritalID.SelectedValue = iRow["LSMaritalID"].ToString();
					}catch {}
					try 
					{
						cboLSReligionID.SelectedValue = iRow["LSReligionID"].ToString();
					}catch {}
					try 
					{
						cboLSNationalityID.SelectedValue = iRow["LSNationalityID"].ToString();					
					}catch {}
					try 
					{
						cboLSFamilyBackgroundID.SelectedValue=iRow["LSFamilyBackgroundID"].ToString();
					}
					catch {}
					this.imgEmp.ImageUrl = ConfigurationSettings.AppSettings["pstrEmpPic"].Trim() + iRow["FilePhoto"].ToString();
					/*txtScanCode.Text=iRow["ScanCode"].ToString();
					txtScanTimeNo.Text=iRow["ScanTimeNo"].ToString();
					cboLSShiftID.SelectedValue=iRow["LSShiftID"].ToString();*/
					try 
					{
						cboT_LSProvinceID.SelectedValue=iRow["T_LSProvinceID"].ToString();
					}catch {}
					txtT_Address.Text= iRow["T_Address"].ToString();					
					txtT_District.Text=iRow["T_District"].ToString();					
					try 
					{
						cboP_LSProvinceID.SelectedValue=iRow["P_LSProvinceID"].ToString();
					}catch {}
					txtP_Address.Text= iRow["P_Address"].ToString();					
					txtP_District.Text=iRow["P_District"].ToString();
					try 
					{
						cboPassWithLevel.SelectedValue=iRow["PassWithLevel"].ToString();
					}
					catch {}
					try 
					{
						cboPassWithLevel2.SelectedValue=iRow["PassWithLevel2"].ToString();
					} catch {}
					txtHomePhone.Text=iRow["HomePhone"].ToString();

					txtTaxCode.Text=iRow["TaxCode"].ToString();	
					txtCostCode.Text=iRow["CostCode"].ToString();	
					try 
					{
						cboLSCurrencyTypeID.SelectedValue=iRow["LSCurrencyTypeID"].ToString();
					}catch {}
					try 
					{
						cboLSBankBranchID.SelectedValue=iRow["LSBankBranchID"].ToString();
					}catch {}
					txtAccountNo.Text=iRow["AccountNo"].ToString();	
					txtAccountName.Text=iRow["AccountName"].ToString();	

					try 
					{
						cboLSBankBranchID2.SelectedValue=iRow["LSBankBranchID2"].ToString();
					}catch {}
					txtAccountNo2.Text=iRow["AccountNo2"].ToString();	
					txtAccountName2.Text=iRow["AccountName2"].ToString();	

					txtPass_Number.Text = iRow["Pass_Number"].ToString();	
					txtPass_IssuedDate.Text = iRow["Pass_IssuedDate"].ToString();	
					txtPass_ExpiredDate.Text = iRow["Pass_ExpiredDate"].ToString();	
					try 
					{
						cboPass_IssuedPlace.SelectedValue = iRow["Pass_IssuedPlace"].ToString();	
					}catch {}
					txtEmergencyContact.Text = iRow["EmergencyContact"].ToString();	
					try 
					{
						cboLSBloodTypeID.SelectedValue = iRow["LSBloodTypeID"].ToString();	
					}catch {}
					txtMentor.Text = iRow["Mentor"].ToString();	
					txtPerformanceManager.Text = iRow["PerformanceManager"].ToString();	
					txtFaculty.Text = iRow["Faculty"].ToString();	
					txtELastName.Text = iRow["ELastName"].ToString().Trim();
					txtEFirstName.Text = iRow["EFirstName"].ToString().Trim();
					txtStartDate.Text = iRow["StartDate"].ToString();
					chkInsurance.Checked = iRow["Insurance"].ToString()=="True"?true:false;
					chkLifeInsurance.Checked = iRow["LifeInsurance"].ToString()=="True"?true:false;
					chkLabourUnion.Checked = iRow["LabourUnion"].ToString()=="True"?true:false;
					chkBHTN.Checked = iRow["BHTN"].ToString()=="True"?true:false;
					chkBHYT.Checked = iRow["BHYT"].ToString()=="True"?true:false;
					
					//Hau
					txtVFirstName.ToolTip = txtVFirstName.Text;
					txtVLastName.ToolTip = txtVLastName.Text;
					txtDOB.ToolTip = txtDOB.Text;
					////
				}
			}
			catch(Exception ex)
			{
				//clsChangeLang.popupWindow(this.Parent,"0034",strLanguage,ex.ToString(),0);
			}
		}		
		/// <summary>
		/// Get data for combo
		/// </summary>
		private void LoadDataCombo()
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";						
			clsCommon.LoadDropDownListControl(cboBorn_LSProvinceID,"sp_GetDataCombo @TableName='ls_tblProvince',@Fields='LSProvinceID as [ID]," + strTextField + " as Name'","ID","Name",true);
			clsCommon.LoadDropDownListControl(cboNative_LSProvinceID,"sp_GetDataCombo @TableName='ls_tblProvince',@Fields='LSProvinceID as [ID]," + strTextField + " as Name'","ID","Name",true);
			clsCommon.LoadDropDownListControl(cboIDIssuedPlace,"sp_GetDataCombo @TableName='ls_tblProvince',@Fields='LSProvinceID as [ID]," + strTextField + " as Name'","ID","Name",true);			
			clsCommon.LoadDropDownListControl(cboLSCultureLevelID,"sp_GetDataCombo @TableName='LS_tblCultureLevel',@Fields='LSCultureLevelID as [ID]," + strTextField + " as Name'","ID","Name",true);
			clsCommon.LoadDropDownListControl(cboGender,"HR_spfrmEMPCV @Activity='getGender',@languageID='" + strLanguage + "' ","ID","Name",true);
			clsCommon.LoadDropDownListControl(cboLSEthnicID,"sp_GetDataCombo @TableName='ls_tblEthnic',@Fields='LSEthnicID as [ID]," + strTextField + " as Name'","ID","Name",true);
			clsCommon.LoadDropDownListControl(cboLSQualifiMajorID,"sp_GetDataCombo @TableName='LS_tblQualifiMajor',@Fields='LSQualifiMajorID as [ID]," + strTextField + " as Name'","ID","Name",true);
			clsCommon.LoadDropDownListControl(cboLSQualifiMajorID2,"sp_GetDataCombo @TableName='LS_tblQualifiMajor',@Fields='LSQualifiMajorID as [ID]," + strTextField + " as Name'","ID","Name",true);
			clsCommon.LoadDropDownListControl(cboLSMaritalID,"sp_GetDataCombo @TableName='ls_tblMarital',@Fields='LSMaritalID as [ID]," + strTextField + " as Name'","ID","Name",true);						
			clsCommon.LoadDropDownListControl(cboLSReligionID,"sp_GetDataCombo @TableName='ls_tblReligion',@Fields='LSReligionID as [ID]," + strTextField + " as Name'","ID","Name",true);
			clsCommon.LoadDropDownListControl(cboLSFamilyBackgroundID,"sp_GetDataCombo @TableName='LS_tblFamilyBackground',@Fields='LSFamilyBackgroundID as [ID]," + strTextField + " as Name'","ID","Name",true);			
			clsCommon.LoadDropDownListControl(cboLSPersonalBackgroundID,"sp_GetDataCombo @TableName='LS_tblPersonalBackground',@Fields='LSPersonalBackgroundID as [ID]," + strTextField + " as Name'","ID","Name",true);
			clsCommon.LoadDropDownListControl(cboLSShiftID,"sp_GetDataCombo @TableName='LS_tblShift',@Fields='LSShiftID as [ID]," + strTextField + " as Name'","ID","Name",true);
			clsCommon.LoadDropDownListControl(cboLSNationalityID,"sp_GetDataCombo @TableName='LS_tblNationality',@Fields='LSNationalityID as [ID]," + strTextField + " as Name'","ID","Name",true);
			clsCommon.LoadDropDownListControl(cboPassWithLevel,"sp_GetDataCombo @TableName='LS_tblPassWithLevel',@Fields='LSPassWithLevelID as [ID]," + strTextField + " as Name'","ID","Name",true);
			clsCommon.LoadDropDownListControl(cboPassWithLevel2,"sp_GetDataCombo @TableName='LS_tblPassWithLevel',@Fields='LSPassWithLevelID as [ID]," + strTextField + " as Name'","ID","Name",true);

			clsCommon.LoadDropDownListControl(cboT_LSProvinceID,"sp_GetDataCombo @TableName='ls_tblProvince',@Fields='LSProvinceID as [ID]," + strTextField + " as Name'","ID","Name",true);
			clsCommon.LoadDropDownListControl(cboP_LSProvinceID,"sp_GetDataCombo @TableName='ls_tblProvince',@Fields='LSProvinceID as [ID]," + strTextField + " as Name'","ID","Name",true);

			clsCommon.LoadDropDownListControl(cboLSCurrencyTypeID,"sp_GetDataCombo @TableName='LS_tblCurrencyType',@Fields='LSCurrencyTypeID as [ID]," + strTextField + " as Name'","ID","Name",true);
			clsCommon.LoadDropDownListControl(cboLSBankBranchID,"sp_GetDataCombo @TableName='LS_TBLBANKBRANCH',@Fields='LSBankBranchID as [ID]," + strTextField + " as Name'","ID","Name",true);
			clsCommon.LoadDropDownListControl(cboLSBankBranchID2,"sp_GetDataCombo @TableName='LS_TBLBANKBRANCH',@Fields='LSBankBranchID as [ID]," + strTextField + " as Name'","ID","Name",true);
			clsCommon.LoadDropDownListControl(cboPass_IssuedPlace,"sp_GetDataCombo @TableName='ls_tblProvince',@Fields='LSProvinceID as [ID]," + strTextField + " as Name'","ID","Name",true);			
		}		
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			
			try
			{		
				
				//if(txtIDNo.Text.Trim().ToLower() != txtIDNo.ToolTip.Trim().ToLower())
				//{
					if(clsHRPersonal.CheckIdentityCard(Session["EmpID"],txtIDNo.Text)== false)
					{
						clsCommon.ImpactDB(Session["EmpID"].ToString().Trim(),"Update",this,"HR_spfrmEmpCV");
						
						//Hau
						/*Save_ChangeLog();
						txtVFirstName.ToolTip = txtVFirstName.Text;
						txtVLastName.ToolTip = txtVLastName.Text;
						txtDOB.ToolTip = txtDOB.Text;*/
						////
						
						lblErr.Text = "";
						//txtCard.Value = "";
					}
					else if(clsHRPersonal.CheckIdentityCard(Session["EmpID"],txtIDNo.Text)== false)//&& txtReturnValue.Value == "True")
					{
						clsCommon.ImpactDB(Session["EmpID"].ToString().Trim(),"Update",this,"HR_spfrmEmpCV");
						
						//Hau
						/*Save_ChangeLog();
						txtVFirstName.ToolTip = txtVFirstName.Text;
						txtVLastName.ToolTip = txtVLastName.Text;
						txtDOB.ToolTip = txtDOB.Text;*/
						////
						
						lblErr.Text = "";
						txtCard.Value = "";
						//lblErr.Text = "This Identity Card existed! Please check again!";
					}
					else if(clsHRPersonal.CheckIdentityCard(Session["EmpID"],txtIDNo.Text)== true)
					{
						//txtReturnValue.Value = "";
						//txtCard.Value = "duplicate";
						lblErr.Text="Mã CMND bị trùng. Xin kiểm tra lại";
						lblErr.Text=clsChangeLang.getStringAlert("P_0001",strLanguage);
						clsChangeLang.popupWindow(this.Parent,"P_0001",strLanguage,"",0);
						
					}
				//}
				else
				{
					clsCommon.ImpactDB(Session["EmpID"].ToString().Trim(),"Update",this,"HR_spfrmEmpCV");
					
//					//Hau
//					Save_ChangeLog();
//					txtVFirstName.ToolTip = txtVFirstName.Text;
//					txtVLastName.ToolTip = txtVLastName.Text;
//					txtDOB.ToolTip = txtDOB.Text;
//					////
						
					lblErr.Text = "";
					//txtCard.Value = "";
				}
				if(txtDOB.Text.Trim() != "")
				{
					txtAge.Text = Convert.ToString(DateTime.Today.Year - Convert.ToInt32(txtDOB.Text.Trim().Substring(7)));
				}
				else
				{
					txtAge.Text = "";
				}
			}
			catch(Exception ex)
			{
				clsChangeLang.popupWindow(this.Parent,"0034",strLanguage,ex.ToString(),0);
			}
			finally
			{
				//txtReturnValue.Value = "";
			}
		}
		/// <summary>
		/// Upload photo of employee
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnUpload_Click(object sender, System.EventArgs e)
		{
			this.lblError.Text="";
			if(this.fileUpload.PostedFile.FileName== "") return;
			

			if (this.fileUpload.Value.LastIndexOf(".")==-1)
			{
				//this.lblError.Text= "Đường dẫn không hợp lệ.";
				clsChangeLang.popupWindow(this.Parent,"P_0002",strLanguage,"",0);
				return;
			}
			try
			{
				string mext = "";
				mext = this.fileUpload.Value.Substring(this.fileUpload.Value.LastIndexOf("."));
				if (mext !=".gif" && mext != ".jpeg" && mext != ".jpg" && mext != ".bmp" && mext !=".GIF" && mext != ".JPEG" && mext != ".JPG" && mext != ".BMP")
				{
					clsChangeLang.popupWindow(this.Parent,"P_0003",strLanguage,"",0);
					return;
				}
				string strFile = Session["EmpID"] + mext;
				if (System.IO.File.Exists(Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrImageFolder"].Trim() + strFile))
					System.IO.File.SetAttributes(Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrImageFolder"].Trim() + strFile, System.IO.FileAttributes.Normal) ;
				this.fileUpload.PostedFile.SaveAs(Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrImageFolder"].Trim() + strFile );
				System.IO.File.SetAttributes(Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrImageFolder"].Trim() + strFile, System.IO.FileAttributes.ReadOnly) ;				
				DataTable dtTemp = new DataTable();
				dtTemp = clsCommon.GetDataTable("HR_spfrmEmpCV @Activity = 'UpdateFilePhoto',@EmpID=N'" + Session["EmpID"] + "',@FilePhoto=N'" + strFile + "'");
				dtTemp.Dispose();
				this.imgEmp.ImageUrl = ConfigurationSettings.AppSettings["pstrEmpPic"].Trim() + strFile;
				LoadDataDefault();				
			}
			catch(Exception exp)
			{
				clsChangeLang.popupWindow(this.Parent,"0034",strLanguage,exp.ToString(),0);
			}
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

		private void btnList_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(Request.ApplicationPath.Trim() + "/Editpage.aspx?ModuleID=HR&ParentID=8&FunctionID=14&Ascx=MdlHR/EmpList.ascx");
		}			
		private void LoadDataGrid_Residence(Object sEmpID)
		{
			DataTable dtList = new DataTable();
			try
			{				
				dtList=clsHRResident.GetDataByEmpID(sEmpID,strLanguage);
				if (dtList.Rows.Count>0)
				{
					dtgList.DataSource = dtList;
					dtgList.CurrentPageIndex = 0;
					dtgList.DataBind();
					chkResidence.Checked=true;
					trAdResidence2.Style.Add("DISPLAY","block");
					trAdResidence3.Style.Add("DISPLAY","block");
				}
				else
				{
					dtgList.DataSource=null;
					dtgList.DataBind();
					chkResidence.Checked=false;
					trAdResidence2.Style.Add("DISPLAY","none");
					trAdResidence3.Style.Add("DISPLAY","none");
				}

				
			}
			catch(Exception ex)
			{
				clsChangeLang.popupWindow(this.Parent,"0034",strLanguage,ex.ToString(),0);
			}
			finally
			{
				dtList.Dispose();
			}
		}

		private void btnTemp_Click(object sender, EventArgs e)
		{
			LoadDataGrid_Residence(Session["EmpID"]);
		}

		
	}
}
