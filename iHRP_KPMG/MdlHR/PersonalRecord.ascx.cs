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
	using iHRPCore.Include;

	/// <summary>
	///		Summary description for PersonalRecord.
	/// </summary>
	public class PersonalRecord : System.Web.UI.UserControl
	{
		#region Declare
		protected string strLanguage = "EN";
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected System.Web.UI.WebControls.LinkButton LinkButton2;
		protected System.Web.UI.WebControls.LinkButton Linkbutton3;
		protected System.Web.UI.WebControls.Label lblLockerNum;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.Label lblStartDate;
		protected System.Web.UI.WebControls.TextBox txtStartDate;
		protected System.Web.UI.WebControls.Label lblCompany;
		protected System.Web.UI.WebControls.Label lblCompanyData;
		protected System.Web.UI.WebControls.Label lblDivision;
		protected System.Web.UI.WebControls.Label lblDivisionData;
		protected System.Web.UI.WebControls.Label lblSection;
		protected System.Web.UI.WebControls.Label lblSectionData;
		protected System.Web.UI.WebControls.Label lblPosition;
		protected System.Web.UI.WebControls.Label lblPositionData;
		protected System.Web.UI.WebControls.Label lblLocation;
		protected System.Web.UI.WebControls.Label lblLocationData;
		protected System.Web.UI.WebControls.Label lblDepartment;
		protected System.Web.UI.WebControls.Label lblDepartmentData;
		protected System.Web.UI.WebControls.Label lblStatus;
		protected System.Web.UI.WebControls.Label lblReportTo;
		protected System.Web.UI.WebControls.Label lblOldEmpCode;
		protected System.Web.UI.WebControls.Label lblOldEmpCodeData;
		protected System.Web.UI.WebControls.Label lblEmail;
		protected System.Web.UI.WebControls.Label lblTelephone;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtRankID;
		protected System.Web.UI.WebControls.Label lblJobTitle;
		protected System.Web.UI.WebControls.Label lblJobCodeData;
		protected System.Web.UI.WebControls.Label lblGradeData;
		protected System.Web.UI.WebControls.Label lblEmpTypeData;
		protected System.Web.UI.WebControls.DropDownList cboLSJobClassID;
		protected System.Web.UI.WebControls.DropDownList cboLSWorkTypeID;
		protected System.Web.UI.WebControls.CheckBox chkAdLevelGrade;
		protected System.Web.UI.HtmlControls.HtmlTableRow trAdLevelGrade;
		protected System.Web.UI.WebControls.Label lblSalaryOtherData;
		protected System.Web.UI.WebControls.Label lblSalaryOther;
		protected System.Web.UI.WebControls.Label lblSalary;
		protected System.Web.UI.WebControls.Label lblSalarylbl;
		protected System.Web.UI.WebControls.Label lblSalaryCoefData;
		protected System.Web.UI.WebControls.Label lblSalCoef;
		protected System.Web.UI.WebControls.Label lblRankOfSalData;
		protected System.Web.UI.WebControls.Label lblRankSal;
		protected System.Web.UI.WebControls.Label lblGradeOfSalData;
		protected System.Web.UI.WebControls.Label llblGradeSal;
		protected System.Web.UI.WebControls.Label lblJobClass;
		protected System.Web.UI.WebControls.Label lblWorkType;
		protected System.Web.UI.WebControls.Label lblJobCode;
		protected System.Web.UI.WebControls.Label lblGrade;
		protected System.Web.UI.WebControls.Label lblRank;
		protected System.Web.UI.WebControls.Label lblEmpType;
		protected System.Web.UI.WebControls.DropDownList cboLSStatusChangeID;
		protected System.Web.UI.WebControls.TextBox txtEmailComp;
		protected System.Web.UI.WebControls.TextBox txtTelephoneComp;
		protected System.Web.UI.WebControls.Label lblRankData;
		protected System.Web.UI.WebControls.Label lblJobTitleData;
		protected System.Web.UI.WebControls.TextBox txtEmpIDReportToName;
		protected System.Web.UI.WebControls.TextBox txtEmpCode_ReportTo;
		protected System.Web.UI.WebControls.TextBox txtEmpID_ReportTo;
		protected System.Web.UI.WebControls.TextBox txtScanCode;
		protected System.Web.UI.WebControls.CheckBox chkIsScan;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.RadioButtonList optBoardingFeeRank;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.DropDownList cboLSExpenseID;
		protected System.Web.UI.WebControls.CheckBox chkSendMail;
		protected System.Web.UI.WebControls.Label lblCurrent;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.CheckBox chkPayroll;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label lblPayRoll;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.TextBox txtDeptHeadName;
		protected System.Web.UI.WebControls.TextBox txtJobDescription;
		protected System.Web.UI.WebControls.LinkButton btnList;
		protected EmpHeader HR_EmpHeader;

		#endregion Declare

		private void Page_Load(object sender, System.EventArgs e)
		{
			Session["CurFunction"] = Request.Params["FunctionID"].Trim();
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";			
			if(!Page.IsPostBack)
			{	
				this.LoadDataCombo();
				this.LoadData();

				//Hau
				//strFieldLog = new String[9];
				//GetOldValue();
				////
			}
			txtScanCode.Attributes["onChange"] = "javascript:this.value=this.value.toUpperCase();";
			txtScanCode.Attributes["onChange"] = "javascript:this.value=this.value.toUpperCase();";
			btnSave.Attributes.Add("OnClick", " return validform()");
			
		}
		/// <summary>
		/// Load all info of employee
		/// </summary>
		private void LoadData()
		{
			try
			{
				DataRow iRow = clsHRPersonalRecord.GetPersonalRecordByEmpID(Session["EmpID"]);				
				if(iRow !=null)
				{
					cboLSStatusChangeID.SelectedValue = iRow["LSStatusChangeID"].ToString();	
					cboLSExpenseID.SelectedValue=iRow["LSExpenseID"].ToString();
					txtScanCode.Text = iRow["ScanCode"].ToString();	
					lblOldEmpCodeData.Text = iRow["EmpCodeOld"].ToString();	
					txtStartDate.Text = iRow["StartDate"].ToString();	
					txtStartDate.ToolTip=iRow["StartDate"].ToString();
					cboLSJobClassID.SelectedValue = iRow["LSJobClassID"].ToString();	
					cboLSWorkTypeID.SelectedValue = iRow["LSWorkTypeID"].ToString();	
					lblCompanyData.Text =iRow["CompanyData"].ToString();	
					lblDepartmentData.Text = iRow["DepartmaneData"].ToString();	
					lblLocationData.Text = iRow["LocationData"].ToString();	
					lblSectionData.Text = iRow["SectionData"].ToString();						
					lblDivisionData.Text = iRow["DivisionData"].ToString();	
					lblPositionData.Text = iRow["PositionData"].ToString();	
					lblJobCodeData.Text = iRow["JobCodeData"].ToString();	
					lblJobTitleData.Text = iRow["JobTitleData"].ToString();	
					
					lblGradeData.Text = iRow["GradeData"].ToString();	
					lblRankData.Text = iRow["RankData"].ToString();	
					lblEmpTypeData.Text = iRow["EmpTypeData"].ToString();	                    
					lblGradeOfSalData.Text = iRow["GradeOfSalData"].ToString();	
					lblRankOfSalData.Text = iRow["RankOfSalData"].ToString();	
					lblSalaryCoefData.Text = iRow["SalaryCoefData"].ToString();	
					lblSalary.Text = iRow["Salary"].ToString();	
					lblSalaryOtherData.Text = iRow["SalaryOtherData"].ToString();	
					
					txtEmpID_ReportTo.Text = iRow["EmpID_ReportTo"].ToString();
					txtEmpIDReportToName.Text = iRow["EmpIDReportToName"].ToString();
					txtEmpCode_ReportTo.Text=iRow["EmpCode_ReportTo"].ToString();
					txtEmailComp.Text = iRow["EmailComp"].ToString();
					txtTelephoneComp.Text = iRow["TelephoneComp"].ToString();
					chkIsScan.Checked=iRow["IsScan"].ToString()=="1"?true:false;
					optBoardingFeeRank.SelectedValue=iRow["BoardingFeeRank"].ToString();

					chkSendMail.Checked=iRow["SendMail"].ToString()=="1"?true:false;
					lblCurrent.Text=iRow["Current"].ToString();

					chkPayroll.Checked=iRow["Payroll"].ToString()=="1"?true:false;
					lblPayRoll.Text=iRow["CurrentPayRoll"].ToString();

					txtDeptHeadName.Text=iRow["DeptHeadName"].ToString();
					txtJobDescription.Text=iRow["JobDescription"].ToString();

				}
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}
		/// <summary>
		/// Get data for combo
		/// </summary>
		/// 
		private void LoadDataCombo()
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";			
			clsCommon.LoadDropDownListControl(cboLSStatusChangeID,"sp_GetDataCombo @TableName='LS_tblStatusChange',@Fields='LSStatusChangeID as [ID]," + strTextField + " as Name'","ID","Name",true);
			clsCommon.LoadDropDownListControl(cboLSExpenseID,"sp_GetDataCombo @TableName='LS_tblExpense',@Fields='LSExpenseID as [ID]," + strTextField + " as Name'","ID","Name",true);
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
			try{
/*				if(cboLSStatusChangeID.SelectedValue.Trim() != txtStartDate.ToolTip.Trim())
				{
					if(cboLSStatusChangeID.SelectedValue.Trim() != "RO" && cboLSStatusChangeID.SelectedValue.Trim() != "")
					{
						lblErr.Text = "Not allow to change to this Status";
						return;
					}
				}  */
				string sMess = clsCommon.sImpactDB(Session["EmpID"].ToString().Trim(),"UpdatePersonalRecord",this,"HR_spfrmEmp");				
				this.HR_EmpHeader.Page_Load(this.Page,e);
				
				if (sMess != "")
				{
					clsChangeLang.popupWindow(this.Parent,sMess,"",0);
				}
			}
			catch(Exception ex)
			{
				clsChangeLang.popupWindowExp(this.Parent,ex);
			}
		}
		private void btnList_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(Request.ApplicationPath.Trim() + "/Editpage.aspx?ModuleID=HR&ParentID=8&FunctionID=14&Ascx=MdlHR/EmpList.ascx");
		}		

		#region Hau

		static public String[] strFieldLog;

		private void Save_ChangeLog()
		{	
			String strUserName = Session[0].ToString().Trim();
			//DateTime dtime = DateTime.Now;
			String strActionTime = DateTime.Now.ToString();
			String strAction = "Edit";
			String strEmpID = Session["EmpID"].ToString();
			DataTable tbl = clsHREmpList.GetEmpList(Session["EmpID"].ToString().Trim(),this.Page,"0");
			String strEmpName = tbl.Rows[0]["EmpName"].ToString();
			String[][] strFieldChanged;
			strFieldChanged = new String[3][];
			strFieldChanged[0] = new String[9];
			strFieldChanged[1] = new String[9];
			strFieldChanged[2] = new String[9];
			int i = 0;
			if(!strFieldLog[0].Equals(cboLSStatusChangeID.SelectedItem.Text.Trim()))
			{
				strFieldChanged[0][i] = "Status";
				strFieldChanged[1][i] = strFieldLog[0];
				strFieldChanged[2][i] = cboLSStatusChangeID.SelectedItem.Text.Trim();
				i++;
			}
			if(!strFieldLog[1].Equals(txtStartDate.Text.Trim()))
			{
				strFieldChanged[0][i] = "Start Date";
				strFieldChanged[1][i] = strFieldLog[1];
				strFieldChanged[2][i] = txtStartDate.Text.Trim();
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

		private void GetOldValue()
		{
			strFieldLog[0] = cboLSStatusChangeID.SelectedItem.Text;
			strFieldLog[1] = txtStartDate.Text;
		}

		#endregion
	}
}
