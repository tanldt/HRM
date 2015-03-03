namespace iHRPCore.MdlHR
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.HRComponent;
	using iHRPCore.Com;
	/// <summary>
	///		Summary description for Qualification.
	/// </summary>
	public class Qualification : System.Web.UI.UserControl
	{
		#region Declare
		public string strLanguage = "EN";
		protected System.Web.UI.WebControls.Label lblCertificateNo;
		protected System.Web.UI.WebControls.Label lblCertificateDate;
		protected System.Web.UI.WebControls.TextBox txtCertificateDate;
		protected System.Web.UI.WebControls.Label lblCertificateName;
		protected System.Web.UI.WebControls.TextBox txtCertificateName;
		protected System.Web.UI.WebControls.Label lblNote;
		protected System.Web.UI.WebControls.TextBox txtNote;
		protected System.Web.UI.WebControls.Label lblIssuedPlace;
		protected System.Web.UI.WebControls.TextBox txtIssuedPlace;
		protected System.Web.UI.WebControls.Label lblTrainingForm;
		protected System.Web.UI.WebControls.TextBox txtFromDate;
		protected System.Web.UI.WebControls.TextBox txtToDate;
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtQualificationID;
		protected System.Web.UI.HtmlControls.HtmlTableRow trGrid;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.RadioButtonList RadioButtonList1;
		protected System.Web.UI.HtmlControls.HtmlTableRow trLocal;
		protected System.Web.UI.HtmlControls.HtmlTableRow trOverseas;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.LinkButton btnList;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnAddNew;
		protected System.Web.UI.WebControls.Label lblSchool;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.TextBox txtCertificateCode;
		protected System.Web.UI.WebControls.Label lblFromMonth;
		protected System.Web.UI.WebControls.Label lblToMonth;
		protected System.Web.UI.WebControls.DropDownList cboLSTrainingFormID;
		protected System.Web.UI.WebControls.DropDownList cboLSNationalityID;
		protected System.Web.UI.WebControls.DataGrid dtgList;
		protected System.Web.UI.WebControls.TextBox txtOtherFaculty;
		protected System.Web.UI.WebControls.DropDownList cboLSFacultyID;
		protected System.Web.UI.WebControls.DropDownList cboLSMajorLevelID;
		protected System.Web.UI.WebControls.TextBox txtOtherMajorLevel;
		protected System.Web.UI.WebControls.CheckBox chkisCompany;
		protected System.Web.UI.WebControls.DropDownList cboLSSchoolID;
		protected System.Web.UI.WebControls.RadioButtonList optionTrainingPlace_;
		protected System.Web.UI.WebControls.DropDownList cboLSProfessionalLevelID;
		protected System.Web.UI.WebControls.RadioButtonList optTrainPlace;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtTrainingPlace_;
		protected System.Web.UI.WebControls.TextBox txtTrainingPlace;
		protected System.Web.UI.WebControls.CheckBox chkAdSchool;
		protected System.Web.UI.WebControls.CheckBox chkAdFaculty;
		protected System.Web.UI.WebControls.CheckBox chkAdMajorLevel;
		protected System.Web.UI.HtmlControls.HtmlTableRow trAdSchool;
		protected System.Web.UI.HtmlControls.HtmlTableRow trAdFaculty;
		protected System.Web.UI.HtmlControls.HtmlTableRow trAdMajorLevel;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtOtherSchool;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.DropDownList cboDegree;
		protected System.Web.UI.WebControls.TextBox txtDegreeText;
		protected System.Web.UI.HtmlControls.HtmlTableRow trDegree;
		protected System.Web.UI.WebControls.TextBox txtQualification;
		protected System.Web.UI.WebControls.Label Label6;
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			Session["CurFunction"] = Request.Params["FunctionID"].Trim();
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
			if(!Page.IsPostBack)
			{
				LoadDataCombo();
				LoadDataGrid();
				Session["ssStatusQual"] = "AddNew";
				TrainingPlace(1);
			}
			btnSave.Attributes.Add("OnClick", " return validform()");
			btnDelete.Attributes.Add("OnClick", "return checkdelete()");			
		}		
		/// <summary>
		/// Load all traing record of Employee
		/// </summary>
		private void LoadDataGrid()
		{
			DataTable dtList = new DataTable();
			try
			{
				dtList = clsHRQualification.GetDataByEmpID(Session["EmpID"]);
				dtgList.DataSource = dtList;
				dtgList.CurrentPageIndex = 0;
				dtgList.DataBind();
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
			finally
			{
				dtList.Dispose();
			}
		}
		/// <summary>
		/// Load data to combo box
		/// </summary>
		private void LoadDataCombo()
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";	
		
			//clsCommon.LoadDropDownListControl(cboMajorID,"sp_GetDataCombo @TableName='LS_tblMajor',@Fields='LSMajorCode," + strTextField + " as Name'","LSMajorCode","Name",true);
			clsCommon.LoadDropDownListControl(cboLSMajorLevelID,"sp_GetDataCombo @TableName='LS_tblMajorLevel',@Fields='LSMajorLevelID as [ID]," + strTextField + " as Name'","ID","Name",true);
			clsCommon.LoadDropDownListControl(cboLSSchoolID,"sp_GetDataCombo @TableName='LS_tblSchool',@Fields='LSSchoolID as [ID],ShortName as Name'","ID","Name",true);
			clsCommon.LoadDropDownListControl(cboDegree,"sp_GetDataCombo @TableName='LS_tblQualifiMajor',@Fields='LSQualifiMajorID as [ID]," + strTextField + " as Name'","ID","Name",true);			
			clsCommon.LoadDropDownListControl(cboLSProfessionalLevelID,"sp_GetDataCombo @TableName='LS_tblProfessionalLevel',@Fields='LSProfessionalLevelID as [ID]," + strTextField + " as Name'","ID","Name",true);			
			clsCommon.LoadDropDownListControl(cboLSFacultyID,"sp_GetDataCombo @TableName='LS_tblFaculty',@Fields='LSFacultyID as [ID]," + strTextField + "  as Name'","ID","Name",true);			
			clsCommon.LoadDropDownListControl(cboLSNationalityID,"sp_GetDataCombo @TableName='LS_tblNationality',@Fields='LSNationalityID as [ID]," + strTextField + " as Name'","ID","Name",true);			
			clsCommon.LoadDropDownListControl(cboLSTrainingFormID,"sp_GetDataCombo @TableName='LS_tblTrainingForm',@Fields='LSTrainingFormID as [ID]," + strTextField + " as Name'","ID","Name",true);			
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
			this.cboLSSchoolID.SelectedIndexChanged += new System.EventHandler(this.cboLSSchoolID_SelectedIndexChanged);
			this.cboLSFacultyID.SelectedIndexChanged += new System.EventHandler(this.cboLSFacultyID_SelectedIndexChanged);
			this.cboLSMajorLevelID.SelectedIndexChanged += new System.EventHandler(this.cboLSMajorLevelID_SelectedIndexChanged);
			this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnList.Click += new System.EventHandler(this.btnList_Click);
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			this.dtgList.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgList_ItemCommand);
			this.dtgList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgList_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
/// <summary>
/// Save or edit Qualification
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
		private void btnSave_Click(object sender, System.EventArgs e)
		{	try
			{				
				if (txtTrainingPlace.Text.Equals("")) txtTrainingPlace.Text="1";
				if(Session["ssStatusQual"].ToString().Trim()=="AddNew")
				{
					if (Session["EmpID"] != null)
					{
						string strErr = clsCommon.sImpactDB(Session["EmpID"].ToString().Trim(),"Save",this,"HR_spfrmQUALIFICATION");
						if(strErr == "")
						{
							btnAddNew_Click(null,null);
							LoadDataGrid();
							clsChangeLang.popupWindow(this.Parent,"0044", strLanguage, "", 1);
						}
						else
							clsChangeLang.popupWindow(this.Parent,"0091", strLanguage, "", 0);
					}
				}
						// strErr=clsCommon.ImpactDB(Session["EmpID"].ToString().Trim(),"Save",this,"HR_spfrmQUALIFICATION");
				else
					//clsCommon.UpdateByKey("QualificationID",txtQualificationID.Value.Trim(),"Update",this,"HR_spfrmQUALIFICATION");
					if (Session["EmpID"] != null)
					{
						string test=txtQualification.Text;
						string test2=txtOtherSchool.Text;
						string test3=txtDegreeText.Text;

						string strErr = clsCommon.sUpdateByKey("QualificationID",txtQualificationID.Value.Trim(),"Update",this,"HR_spfrmQUALIFICATION");
						if(strErr == "")
						{
							btnAddNew_Click(null,null);
							LoadDataGrid();
							clsChangeLang.popupWindow(this.Parent,"0044", strLanguage, "", 1);
						}
						else
							clsChangeLang.popupWindow(this.Parent,"0091", strLanguage, "", 0);
					}
			
			//	btnAddNew_Click(null,null);
			//	LoadDataGrid();				
			}
			catch(Exception ex)
			{
				//lblErr.Text = ex.Message;
				clsChangeLang.popupWindow(this.Parent,"0024",strLanguage,ex.ToString(),0);
			}			
		}

		private void btnAddNew_Click(object sender, System.EventArgs e)
		{
			clsCommon.ClearControlValue(this);
			Session["ssStatusQual"] = "AddNew";
			TrainingPlace(1);
		}
/// <summary>
/// Load data to view or edit
/// </summary>
/// <param name="source"></param>
/// <param name="e"></param>
		private void dtgList_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				if(e.CommandName.Trim().ToUpper() =="EDIT")
				{
					txtQualificationID.Value = dtgList.Items[e.Item.ItemIndex].Cells[0].Text.Trim();
					DataRow iRow = clsHRQualification.GetDataByID(txtQualificationID.Value.Trim());
					if(iRow != null)
					{
						txtQualificationID.Value = iRow["QualificationID"].ToString();						
						txtCertificateCode.Text = iRow["CertificateCode"].ToString();
						txtCertificateName.Text = iRow["CertificateName"].ToString();
						txtCertificateDate.Text = iRow["CertificateDate"].ToString();
						txtIssuedPlace.Text = iRow["IssuedPlace"].ToString();
						txtFromDate.Text = iRow["FromDate"].ToString();
						txtToDate.Text = iRow["ToDate"].ToString();
						cboLSSchoolID.SelectedValue = iRow["LSSchoolID"].ToString();
						txtOtherSchool.Text = iRow["OtherSchool"].ToString();
						cboLSFacultyID.SelectedValue = iRow["LSFacultyID"].ToString();
						txtOtherFaculty.Text = iRow["OtherFaculty"].ToString();
						cboLSTrainingFormID.SelectedValue = iRow["LSTrainingFormID"].ToString();
						cboLSMajorLevelID.SelectedValue = iRow["LSMajorLevelID"].ToString();
						txtOtherMajorLevel.Text = iRow["OtherMajorLevel"].ToString();
						cboLSProfessionalLevelID.SelectedValue = iRow["LSProfessionalLevelID"].ToString();
						txtQualification.Text=iRow["Qualification"].ToString();
						cboLSNationalityID.SelectedValue = iRow["LSNationalityID"].ToString();	
						cboDegree.SelectedValue=iRow["Degree"].ToString();
						txtDegreeText.Text=iRow["Degree1"].ToString();
						txtTrainingPlace.Text = iRow["TrainingPlace"].ToString();
						txtNote.Text = iRow["Note"].ToString();
						chkisCompany.Checked =iRow["isCompany"].ToString()=="True"?true:false;
						TrainingPlace(int.Parse(txtTrainingPlace.Text));						
						setSchool(cboLSSchoolID,txtOtherSchool);
						setSchool(cboLSFacultyID,txtOtherFaculty);
						setSchool(cboLSMajorLevelID,txtOtherMajorLevel);
						setSchool(cboDegree,txtDegreeText);
						setSchool(cboLSProfessionalLevelID,txtQualification);
					}

				}
				Session["ssStatusQual"] = "Edit";
			}	
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}

		/// <summary>
		/// Delete qualification of employee
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				string strID="";
				for(int i=0;i<dtgList.Items.Count;i++)
				{
					if(((CheckBox)dtgList.Items[i].FindControl("chkSelect")).Checked==true)
					{
						strID += dtgList.Items[i].Cells[0].Text.Trim() + "$";
					}
				}
				clsCommon.DeleteListRecord("HR_spfrmQUALIFICATION","QualificationID",SqlDbType.NVarChar,12,strID);
				LoadDataGrid();
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}

		private void dtgList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			try
			{
				LoadDataGrid();
				dtgList.CurrentPageIndex=e.NewPageIndex;
				dtgList.DataBind();
			}
			catch(Exception ex)			
			{
				string str = ex.Message;
			}
		}

		private void btnList_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(Request.ApplicationPath.Trim() + "/Editpage.aspx?ModuleID=HR&ParentID=8&FunctionID=14&Ascx=MdlHR/EmpList.ascx");
		}

		private void btnExport_Click(object sender, System.EventArgs e)
		{
			iHRPCore.DataGridExcelExporter myExcelXport=new DataGridExcelExporter(dtgList);			
			myExcelXport.Export("");
			myExcelXport =null;
		}

		private void cboLSSchoolID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataRow drData =clsCommon.GetDataRow("HR_spfrmQUALIFICATION @Activity='getComboQualification',@TableName = N'LS_tblSchool',@Where='LSSchoolID=''" + cboLSSchoolID.SelectedValue + "''',@languageID='" + strLanguage + "'");
			if (drData!=null)
			{
				txtOtherSchool.Text=drData["Name"].ToString();
			}
		}

		private void cboLSFacultyID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataRow drData =clsCommon.GetDataRow("HR_spfrmQUALIFICATION @Activity='getComboQualification',@TableName = N'LS_tblFaculty',@Where='LSFacultyID=''" + cboLSFacultyID.SelectedValue + "''',@languageID='" + strLanguage + "'");
			if (drData!=null)
			{
				txtOtherFaculty.Text=drData["Name"].ToString();
			}
		}

		private void cboLSMajorLevelID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataRow drData =clsCommon.GetDataRow("HR_spfrmQUALIFICATION @Activity='getComboQualification',@TableName = N'LS_tblMajorLevel',@Where='LSMajorLevelID=''" + cboLSMajorLevelID.SelectedValue + "''',@languageID='" + strLanguage + "'");
			if (drData!=null)
			{
				txtOtherMajorLevel.Text=drData["Name"].ToString();
			}
		}
		private void TrainingPlace(int iValue)
		{
			switch (iValue)
			{
				case 0:
					optTrainPlace.Items[1].Selected=true;
					trLocal.Style.Add("DISPLAY","none");
					trOverseas.Style.Add("DISPLAY","block");
					break;
				case 1:
					
					optTrainPlace.SelectedValue="0";
					trLocal.Style.Add("DISPLAY","block");
					trOverseas.Style.Add("DISPLAY","none");
					optionTrainingPlace_.SelectedValue="0";
					break;
				case 2:					
					optTrainPlace.SelectedValue="0";
					trLocal.Style.Add("DISPLAY","block");
					trOverseas.Style.Add("DISPLAY","none");
					optionTrainingPlace_.SelectedValue ="1";
					break;
			}
			
		}
		private void setSchool(DropDownList pCbo, TextBox pTxt)
		{
			if( pCbo.SelectedValue.Equals(""))
			{
				pTxt.Enabled=true;
			}
			else
			{
				pTxt.Enabled=false;
			}
		}		
	}
}
