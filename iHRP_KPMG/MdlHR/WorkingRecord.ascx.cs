namespace iHRPCore.MdlHR
{
	using System;
	using System.IO;
	using System.Configuration;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.Com;
	using iHRPCore.HRComponent;
	using iHRPCore.TMSComponent; 
	using System.Data.SqlClient;
	using iHRPCore.Include;
	/// <summary>
	///		Summary description for WorkingRecord.
	/// </summary>
	public class WorkingRecord : System.Web.UI.UserControl
	{
		#region Declare
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.TextBox txtFromDate;
		protected System.Web.UI.WebControls.TextBox txtToDate;
		protected System.Web.UI.WebControls.Label lblCompany;
		protected System.Web.UI.WebControls.Label lblCompanyData;
		protected System.Web.UI.WebControls.Label lblFromDivision;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label lblFromDepartment;
		protected System.Web.UI.WebControls.Label lblFromSection;
		protected System.Web.UI.WebControls.Label lblFromDepartmentData;
		protected System.Web.UI.WebControls.Label lblFromSectionData;
		protected System.Web.UI.WebControls.Label lblFromPosition;
		protected System.Web.UI.WebControls.Label lblFromPositionData;
		protected System.Web.UI.WebControls.Label lblStatusChange;
		protected System.Web.UI.WebControls.Label lblNote;
		protected System.Web.UI.WebControls.TextBox txtDecisionNo;
		protected System.Web.UI.WebControls.Label lblDecisionNo;
		protected System.Web.UI.WebControls.CheckBox chkShowGrid;
		protected System.Web.UI.WebControls.LinkButton btnAddNew;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnList;
		protected System.Web.UI.WebControls.TextBox txtNote;
		protected System.Web.UI.WebControls.Label lblEffDate;
		protected System.Web.UI.WebControls.Label lblEndDate;
		protected System.Web.UI.WebControls.DropDownList cboCompanyID;
		protected System.Web.UI.WebControls.DropDownList cboLevel1ID;
		protected System.Web.UI.WebControls.DropDownList cboLevel2ID;
		protected System.Web.UI.WebControls.DropDownList cboLevel3ID;
		protected System.Web.UI.WebControls.DropDownList cboPositionID;
		protected System.Web.UI.WebControls.Label Label20;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel2ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel3ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel1ID;
		protected System.Web.UI.WebControls.DropDownList cboLSStatusChangeID;
		protected System.Web.UI.WebControls.TextBox txtSignDate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtWorkingRecordID;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label lblFromLocationData;
		protected System.Web.UI.WebControls.DropDownList cboLocationID;
		protected System.Web.UI.HtmlControls.HtmlTableRow trGrid;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.LinkButton btnImport;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtRankID;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.DataGrid dtgList;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.DropDownList cboGrade;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.DropDownList cboEmpType;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.Label Label14;
		protected System.Web.UI.WebControls.CheckBox ChkDecision;
		protected System.Web.UI.WebControls.TextBox txtSigner;
		protected System.Web.UI.WebControls.TextBox txtSignerPosition;
		protected System.Web.UI.WebControls.DataGrid grdWorkingRecord;
		protected System.Web.UI.WebControls.DropDownList cboJobTitle;
		protected System.Web.UI.WebControls.Label lblFromJobCode;
		protected System.Web.UI.WebControls.Label lblFromJobTitle;
		protected System.Web.UI.WebControls.Label lblFromGrade;
		protected System.Web.UI.WebControls.Label lblFromEmpType;
		protected System.Web.UI.WebControls.DropDownList cboStatusID;		
		protected System.Web.UI.HtmlControls.HtmlTableRow trDecision1;
		protected System.Web.UI.HtmlControls.HtmlTableRow trDecision2;
		protected System.Web.UI.HtmlControls.HtmlTableRow trDecision3;
		protected System.Web.UI.WebControls.Label lblFromRank;
		protected System.Web.UI.WebControls.DropDownList cboRank;
		protected System.Web.UI.HtmlControls.HtmlTableRow GradeRankJC2;
		protected System.Web.UI.HtmlControls.HtmlTableRow GradeRankJC3;
		protected System.Web.UI.HtmlControls.HtmlTableRow GradeRankJC1;
		protected System.Web.UI.WebControls.TextBox lblJobCodeID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtManagerID;
		protected System.Web.UI.HtmlControls.HtmlInputFile txtFile;
		protected System.Web.UI.WebControls.HyperLink hpSelectFile;
		protected System.Web.UI.WebControls.LinkButton btnDeleteFile;
		protected System.Web.UI.WebControls.CheckBox chkAdLocation;
		protected System.Web.UI.HtmlControls.HtmlTableRow trAdLocation;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label lblFromLeaveLevel;
		protected System.Web.UI.WebControls.DropDownList cboLSLeaveLevelID;
		protected System.Web.UI.WebControls.Label lblDialogue;
		protected System.Web.UI.WebControls.TextBox txtDialog;

		public string strLanguage = "VN";
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.HtmlControls.HtmlTableRow Tr1;
		protected EmpHeader HR_EmpHeader;
		#endregion Declare

		private void Page_Load(object sender, System.EventArgs e)
		{
			Session["CurFunction"] = Request.Params["FunctionID"].Trim();
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"VN";			
			if(!Page.IsPostBack)
			{
				LoadDataCombo();
				LoadDataDefault();
				LoadDataGrid();////
				Session["ssStatusWorking"] = "AddNew";

				if(Request.Params["tabid"].ToString().Trim()=="1")
				{
					GradeRankJC1.Style.Add("DISPLAY","block");
					GradeRankJC2.Style.Add("DISPLAY","block");
					GradeRankJC3.Style.Add("DISPLAY","block");
				}
				else
				{
					GradeRankJC1.Style.Add("DISPLAY","none");
					GradeRankJC2.Style.Add("DISPLAY","none");
					GradeRankJC3.Style.Add("DISPLAY","none");
				}

				btnDeleteFile.Visible=false;
				//Hau
				strFieldLog = new String[13];
				////
				DecisionChk(ChkDecision.Checked); 
				if(Session["ssStatusWorking"] != "AddNew")
				{
					txtFromDate.ReadOnly=false;
					txtFromDate.CssClass="input";
				}
				else
				{
					txtFromDate.ReadOnly=true;
					txtFromDate.CssClass="inputReadonly";
				}
			}
			if(Session["ssStatusWorking"] != "AddNew")
			{
				txtFromDate.ReadOnly=true;
				txtFromDate.CssClass="inputReadOnly";
			}
			else
			{
				txtFromDate.ReadOnly=false;
				txtFromDate.CssClass="input";
			}
			btnSave.Attributes.Add("OnClick", " return validform()");
			btnDelete.Attributes.Add("OnClick", "return checkdelete()");
			btnDeleteFile.Attributes.Add("OnClick", "return confirmDeleteFile()");
		}
		/// <summary>
		/// Load all working record of Employee
		/// </summary>
		private void LoadDataGrid()
		{
			DataTable dtList = new DataTable();
			try
			{				
				dtList = clsHRWorkingRecord.GetDataByEmpID(Session["EmpID"]);
				grdWorkingRecord.DataSource = dtList;
				grdWorkingRecord.CurrentPageIndex = 0;
				grdWorkingRecord.DataBind();
				if(grdWorkingRecord.Items.Count>1)
				{
					//((LinkButton)grdWorkingRecord.Items[0].FindControl("btnEdit")).Enabled = true;
					//((CheckBox)grdWorkingRecord.Items[grdWorkingRecord.Items.Count-1].FindControl("chkSelect")).Enabled = false;
					((CheckBox)grdWorkingRecord.Items[0].FindControl("chkSelect")).Enabled = true;
				}
			}
			catch(Exception ex)
			{
				clsChangeLang.popupWindowExp(this.Parent,ex);
			}
			finally
			{
				dtList.Dispose();
			}
		}
		private void BindDataGrid()
		{
			DataTable dtList = new DataTable();
			try
			{				
				dtList = clsHRWorkingRecord.GetDataByEmpID(Session["EmpID"]);
				grdWorkingRecord.DataSource = dtList;
				grdWorkingRecord.CurrentPageIndex = 0;
				grdWorkingRecord.DataBind();
				if(grdWorkingRecord.Items.Count>1)
				{
					//((LinkButton)grdWorkingRecord.Items[0].FindControl("btnEdit")).Enabled = true;
					((CheckBox)grdWorkingRecord.Items[0].FindControl("chkSelect")).Enabled = true;
				}
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
		/// Load emp info
		/// </summary>
		private void LoadDataDefault()
		{
			try
			{
				DataRow iRow = clsHRWorkingRecord.GetCurrentInfo(Session["EmpID"]);
				if(iRow !=null)
				{
					lblCompanyData.Text = iRow["Company"].ToString().Trim();
					lblFromDepartmentData.Text = iRow["Department"].ToString().Trim();
					lblFromLocationData.Text = iRow["Location"].ToString().Trim();
					lblFromPositionData.Text = iRow["Position"].ToString().Trim();
					lblFromSectionData.Text = iRow["Section"].ToString().Trim();
					lblFromDivision.Text = iRow["Division"].ToString().Trim();
					lblFromJobCode.Text=iRow["JobCode"].ToString().Trim();
					lblFromJobTitle.Text=iRow["JobTitle"].ToString().Trim();
					lblFromGrade.Text=iRow["Grade"].ToString().Trim();
					lblFromRank.Text=iRow["Rank"].ToString().Trim();
					lblFromEmpType.Text=iRow["EmpType"].ToString().Trim();
					lblFromLeaveLevel.Text=iRow["LeaveLevel"].ToString().Trim();
					//lblDialogue.Text = iRow["DialogueRating"].ToString().Trim();
					//LOAD CODE
					lblCompanyData.ToolTip = iRow["LSCompanyID"].ToString().Trim();
					lblFromDivision.ToolTip = iRow["LSLevel1ID"].ToString().Trim();					
					lblFromDepartmentData.ToolTip = iRow["LSLevel2ID"].ToString().Trim();
					lblFromSectionData.ToolTip = iRow["LSLevel3ID"].ToString().Trim();
					lblFromLocationData.ToolTip = iRow["LSLocationID"].ToString().Trim();
					lblFromPositionData.ToolTip = iRow["LSPositionID"].ToString().Trim();
					lblFromGrade.ToolTip=iRow["LSGradeID"].ToString().Trim();
					lblFromRank.ToolTip=iRow["LSRankID"].ToString().Trim();
					lblFromJobCode.ToolTip=iRow["LSJobCodeID"].ToString().Trim();
					//lblFromEmpType.ToolTip=iRow["LSEmpTypeID"].ToString().Trim();
					//lblFromLeaveLevel.ToolTip=iRow["LSLeaveTypeID"].ToString().Trim();

					//
					cboCompanyID.SelectedValue = iRow["LSCompanyID"].ToString().Trim();
					cboCompanyID_SelectedIndexChanged(null,null);
				}
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
			finally
			{				
			}
		}
		private void LoadDataCombo()
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			clsCommon.LoadDropDownListControl(cboCompanyID,"sp_GetDataCombo @TableName='LS_tblCompany',@Fields='LSCompanyID as [ID]," + strTextField + " as Name'","ID","Name",true);
			clsCommon.LoadDropDownListControl(cboLSStatusChangeID,"sp_GetDataCombo @TableName='LS_tblStatusChange',@Fields='LSStatusChangeID as [ID]," + strTextField + " as Name'","ID","Name",true);
			clsCommon.LoadDropDownListControl(cboLocationID,"sp_GetDataCombo @TableName='LS_tblLocation',@Fields='LSLocationID as [ID]," + strTextField + " as Name'","ID","Name",true);
			clsCommon.LoadDropDownListControl(cboPositionID,"sp_GetDataCombo @TableName='LS_tblPosition',@Fields='LSPositionID as [ID]," + strTextField + " as Name'","ID","Name",true);			
			clsCommon.LoadDropDownListControl(cboJobTitle,"sp_GetDataCombo @TableName='LS_tblJobTitle',@Fields='LSJobTitleID as [ID]," + strTextField + " as Name'","ID","Name",true);			
			clsCommon.LoadDropDownListControl(cboGrade,"sp_GetDataCombo @TableName='LS_tblGrade',@Fields='LSGradeID as [ID]," + strTextField + " as Name'","ID","Name",true);
			clsCommon.LoadDropDownListControl(cboEmpType,"sp_GetDataCombo @TableName='LS_tblEmpType',@Fields='LSEmpTypeID as [ID]," + strTextField + " as Name'","ID","Name",true);
			clsCommon.LoadDropDownListControl(cboLSLeaveLevelID,"sp_GetDataCombo @TableName='LS_tblLeaveLevel',@Fields='LSLeaveLevelID as [ID]," + strTextField + " as Name'","ID","Name",true);			
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
			this.btnDeleteFile.Click += new System.EventHandler(this.btnDeleteFile_Click);
			this.cboCompanyID.SelectedIndexChanged += new System.EventHandler(this.cboCompanyID_SelectedIndexChanged);
			this.cboLevel1ID.SelectedIndexChanged += new System.EventHandler(this.cboLevel1ID_SelectedIndexChanged);
			this.cboLevel2ID.SelectedIndexChanged += new System.EventHandler(this.cboLevel2ID_SelectedIndexChanged);
			this.cboGrade.SelectedIndexChanged += new System.EventHandler(this.cboGrade_SelectedIndexChanged);
			this.cboRank.SelectedIndexChanged += new System.EventHandler(this.cboRank_SelectedIndexChanged);
			this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnList.Click += new System.EventHandler(this.btnList_Click);
			this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			this.grdWorkingRecord.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.grdWorkingRecord_ItemCommand);
			this.grdWorkingRecord.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.grdWorkingRecord_PageIndexChanged);
			this.grdWorkingRecord.SelectedIndexChanged += new System.EventHandler(this.grdWorkingRecord_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		private void cboLevel1ID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				string strLevel1ID = cboLevel1ID.SelectedValue.Trim();
				string strTextField = strLanguage == "VN"?"VNName":"Name";
				if (!strLevel1ID.Equals(""))
				{
					clsHREmpList.LoadComboLevel2ByLevel1(cboLevel2ID,strTextField,strLevel1ID, strLanguage,this.Page); 
					cboLevel2ID.SelectedValue = this.txtLevel2ID.Value.Trim();
					cboLevel2ID_SelectedIndexChanged(null,null);
				}
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
				if (!strLevel2ID.Equals(""))
				{
					clsHREmpList.LoadComboLevel3ByLevel2(cboLevel3ID,strTextField,strLevel2ID, strLanguage); 
					cboLevel3ID.SelectedValue = this.txtLevel3ID.Value.Trim();
				}
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
				if (!strCompanyID.Equals(""))
				{
					clsHREmpList.LoadComboLevel1ByCompany(cboLevel1ID,strTextField,strCompanyID, strLanguage,this.Page); 
					cboLevel1ID.SelectedValue = this.txtLevel1ID.Value.Trim();
					cboLevel1ID_SelectedIndexChanged(null,null);
				}
			}
			catch(Exception ex)
			{
				lblErr.Text=ex.ToString();
			}
		}
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (Session["ssStatusWorking"].ToString().ToUpper() == "ADDNEW")
				{
					if(clsHRWorkingRecord.CheckValidEffectiveDate(Session["EmpID"].ToString().Trim(),txtFromDate.Text.Trim(),txtWorkingRecordID.Value.Trim(),Session["ssStatusWorking"].ToString().Trim())==false)
					{
						//lblErr.Text = "Effective date must be greater than effective date before";
						//lblErr.Text = "Ngày hiệu lực phải lớn hơn ngày hiệu lực trước.";
						//lblErr.Text=clsChangeLang.getStringAlert("WR_0001",strLanguage);
						clsChangeLang.popupWindow(this.Parent,"WR_0001",strLanguage,"",1);
						DecisionChk(ChkDecision.Checked);
						return;
					}
				}
				//------------
				
					string strEmpID = Session["EmpID"].ToString().Trim();
					string strStatusID = cboLSStatusChangeID.SelectedValue;
					string strFromDate = txtFromDate.Text;
					string strComID = "";
					string strLevel1ID = "";
					string strLevel2ID = "";
					string strLevel3ID = "";
					string strGradeID = "";
					string strRankID = "";
					string strJobCode = "";
				strComID = lblCompanyData.ToolTip;
					//if(cboCompanyID.SelectedValue.Trim()!=""||cboLevel1ID.SelectedValue.Trim()!=""||cboLevel2ID.SelectedValue.Trim()!=""||cboLevel3ID.SelectedValue.Trim()!="")
				if(cboLevel1ID.SelectedValue.Trim()!=""||cboLevel2ID.SelectedValue.Trim()!=""||cboLevel3ID.SelectedValue.Trim()!="")
					{
						//strComID = cboCompanyID.SelectedValue;
						strLevel1ID = cboLevel1ID.SelectedValue;
						strLevel2ID = cboLevel2ID.SelectedValue;
						strLevel3ID = cboLevel3ID.SelectedValue;
					}
					else
					{
						//strComID = lblCompanyData.ToolTip;
						strLevel1ID = lblFromDivision.ToolTip;
						strLevel2ID = lblFromDepartmentData.ToolTip;
						strLevel3ID = lblFromSectionData.ToolTip;
					}
					
				if(cboGrade.SelectedValue.Trim()!=""||cboRank.SelectedValue.Trim()!="")
				{
					strGradeID = cboGrade.SelectedValue.Trim();
					strRankID = cboRank.SelectedValue;
					strJobCode= lblJobCodeID.Text;						
				}
				else
				{
					strGradeID = lblFromGrade.ToolTip;
					strRankID=lblFromRank.ToolTip;
					strJobCode = lblFromJobCode.ToolTip;
				}
					string strLocationID = cboLocationID.SelectedValue.Trim()==""?lblFromLocationData.ToolTip:cboLocationID.SelectedValue;
					string strPosID = cboPositionID.SelectedValue.Trim()==""?lblFromPositionData.ToolTip:cboPositionID.SelectedValue;					 
					string strJobTitle = cboJobTitle.SelectedValue.Trim()==""?lblFromJobTitle.ToolTip:cboJobTitle.SelectedValue;
					string strEmpType= cboEmpType.SelectedValue.Trim()==""?lblFromEmpType.ToolTip:cboEmpType.SelectedValue;
					string strLeaveLevel= cboLSLeaveLevelID.SelectedValue.Trim()==""?lblFromLeaveLevel.ToolTip:cboLSLeaveLevelID.SelectedValue;
					if(strJobCode.Trim() != "")
						strJobCode = clsCommon.GetDataRow("select LSJobCodeID from LS_tblJobCode where VNName = N'" + strJobCode + "'")["LSJobCodeID"].ToString();
					//string strBasicSalary = txtBasicSalary.Text.Trim()==""?lblFromBasicSalaryData.ToolTip:txtBasicSalary.Text;
					string strDialogue = txtDialog.Text.Trim()==""?lblDialogue.Text:txtDialog.Text;

					// -- not check
					string strDecNo = txtDecisionNo.Text;
					string strSignDate = txtSignDate.Text;
					string strNote = txtNote.Text;
					string strSigner=txtSigner.Text;
					string strSignPos=txtSignerPosition.Text;
					int iDecision = ChkDecision.Checked?1:0;	
				if (iDecision==0	)
				{
					strDecNo="";
					strSignDate="";
					strSigner="";
					strSignPos="";
					btnDeleteFile_Click(null,null);					
				}
					string strAttachFile=import_file();
				if (strAttachFile.Equals(""))
				{	
					return ;
				}
				else if(strAttachFile.Equals("null"))
				{
					
					if(hpSelectFile.Visible)
					{
						strAttachFile=hpSelectFile.Text;
					}
					else
					{
						strAttachFile="";
					}
				}
				if(Session["ssStatusWorking"].ToString().Trim()=="AddNew")
					{
						if(cboLSStatusChangeID.SelectedValue.Trim().ToUpper() == "NEWHIRE")
						clsCommon.ShowMessageBox(this.Page,"Not allow add new a NEW HIRE status");
						else
						{
							clsHRWorkingRecord.SaveData(strEmpID,strStatusID,strFromDate,strComID,strLevel1ID,strLevel2ID
								,strLevel3ID,strLocationID,strPosID,strGradeID,strRankID,strJobCode
								,strDecNo,strSignDate,strNote,strSigner,strSignPos,strJobTitle
								,strEmpType,iDecision,strAttachFile,"","Save",strLeaveLevel, strDialogue);
							//clsCommon.ImpactDB(Session["EmpID"].ToString().Trim(),"Save",this,"HR_spfrmWorkingRecord");

						//Hau
							Save_ChangeLog();
						////
						}
					}
				else
					{					
					clsHRWorkingRecord.SaveData(strEmpID,strStatusID,strFromDate,strComID,strLevel1ID,strLevel2ID
						,strLevel3ID,strLocationID,strPosID,strGradeID,strRankID,strJobCode,strDecNo
						,strSignDate,strNote,strSigner,strSignPos,strJobTitle,strEmpType,iDecision
						,strAttachFile,txtWorkingRecordID.Value,"Update",strLeaveLevel, strDialogue);

					//Hau
					Save_ChangeLog();
					////
					}
				lblErr.Text = "";
				clsCommon.ClearControlValue(this,"MdlHR/WorkingRecord.ascx");
				Session["ssStatusWorking"] = "AddNew";
				LoadDataGrid();
				LoadDataDefault();
				btnSave.Enabled = true;
				ChkDecision.Checked=true;
				hpSelectFile.Visible=false;
				btnDeleteFile.Visible=false;
				this.HR_EmpHeader.Page_Load(this.Page,e);
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}
		/// <summary>
		/// Edit Record
		/// </summary>
		private void grdWorkingRecord_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				if(e.CommandName.Trim().ToUpper() =="EDIT")
				{
					txtWorkingRecordID.Value = grdWorkingRecord.Items[e.Item.ItemIndex].Cells[0].Text.Trim();
					
					DataRow iRow = clsHRWorkingRecord.GetDataByID(txtWorkingRecordID.Value.Trim());
					if(iRow != null)
					{
						//Old info
						txtFromDate.ReadOnly=true;
						txtFromDate.CssClass="inputReadOnly";
						lblCompanyData.Text = iRow["Company"].ToString().Trim();
						lblFromDepartmentData.Text = iRow["Department"].ToString().Trim();
						lblFromLocationData.Text = iRow["Location"].ToString().Trim();
						lblFromPositionData.Text = iRow["Position"].ToString().Trim();
						lblFromSectionData.Text = iRow["Section"].ToString().Trim();
						lblFromDivision.Text = iRow["Division"].ToString().Trim();
						lblFromJobCode.Text=iRow["ChargeRateOld"].ToString().Trim();
						lblFromEmpType.Text=iRow["EmpType"].ToString().Trim();
						lblFromLeaveLevel.Text=iRow["LeaveLevel"].ToString().Trim();
						lblFromGrade.Text=iRow["Grade"].ToString().Trim();
						lblFromJobTitle.Text=iRow["JobTitle"].ToString().Trim();
						lblFromRank.Text=iRow["Rank"].ToString().Trim();
						lblDialogue.Text=iRow["DialogueRatingOld"].ToString().Trim();
						//Current info
						txtFromDate.Text = iRow["FromDate"].ToString().Trim();
						txtToDate.Text = iRow["ToDate"].ToString().Trim();
						txtDecisionNo.Text = iRow["DecisionNo"].ToString().Trim();
						txtNote.Text = iRow["Note"].ToString().Trim();
						txtSignDate.Text = iRow["SignDate"].ToString().Trim();
						cboLocationID.SelectedValue = iRow["LSLocationID"].ToString().Trim();						
						cboCompanyID.SelectedValue = iRow["LSCompanyID"].ToString().Trim();
						txtLevel1ID.Value=iRow["LSLevel1ID"].ToString().Trim();
						cboCompanyID_SelectedIndexChanged(null,null);
						cboLevel1ID.SelectedValue = iRow["LSLevel1ID"].ToString().Trim();
						txtLevel2ID.Value=iRow["LSLevel2ID"].ToString().Trim();
						cboLevel1ID_SelectedIndexChanged(null,null);						
						cboLevel2ID.SelectedValue = iRow["LSLevel2ID"].ToString().Trim();
						txtLevel3ID.Value=iRow["LSLevel3ID"].ToString().Trim();	
						cboLevel2ID_SelectedIndexChanged(null,null);
						cboLevel3ID.SelectedValue = iRow["LSLevel3ID"].ToString().Trim();						
						cboPositionID.SelectedValue = iRow["LSPositionID"].ToString().Trim();
						cboLSStatusChangeID.SelectedValue = iRow["LSStatusChangeID"].ToString().Trim();
						cboJobTitle.SelectedValue=iRow["LSJobTitleID"].ToString().Trim();
						cboEmpType.SelectedValue=iRow["LSEmpTypeID"].ToString().Trim();
						cboLSLeaveLevelID.SelectedValue=iRow["LSLeaveLevelID"].ToString().Trim();
						cboGrade.SelectedValue=iRow["LSGradeID"].ToString().Trim();
						cboGrade_SelectedIndexChanged(null,null);
						cboRank.SelectedValue=iRow["LSRankID"].ToString().Trim();
						cboRank_SelectedIndexChanged(null,null);
						lblJobCodeID.Text = iRow["ChargeRateCur"].ToString().Trim();						
						txtSigner.Text=iRow["Signer"].ToString().Trim();
						txtSignerPosition.Text=iRow["SignerPosition"].ToString().Trim();
						txtDialog.Text=iRow["DialogueRatingCur"].ToString().Trim();
						if (iRow["Decision"].ToString().Trim()=="True")
						{
							ChkDecision.Checked=true;
							trDecision1.Style.Add("DISPLAY","block");
							trDecision2.Style.Add("DISPLAY","block");
							trDecision3.Style.Add("DISPLAY","block");
						}
						else
						{
							ChkDecision.Checked=false;
							trDecision1.Style.Add("DISPLAY","none");
							trDecision2.Style.Add("DISPLAY","none");
							trDecision3.Style.Add("DISPLAY","none");
						}
						if (iRow["AttachFile"].ToString().Trim()!="")
						{
							hpSelectFile.Visible=true;
							btnDeleteFile.Visible=true;
							hpSelectFile.Text=iRow["AttachFile"].ToString().Trim();
							hpSelectFile.NavigateUrl= ConfigurationSettings.AppSettings["pstrGetFileFolder"].Trim() + iRow["AttachFile"].ToString().Trim();							
						}
						else
						{
							hpSelectFile.Visible=false;	
							btnDeleteFile.Visible=false;
							hpSelectFile.Text="";
							hpSelectFile.NavigateUrl="";							
						}


						//Hau
						/*strFieldLog[0] = txtFromDate.Text.Trim();
						strFieldLog[1] = txtToDate.Text.Trim();
						strFieldLog[2] = cboLSStatusChangeID.SelectedItem.Text;
						strFieldLog[3] = cboCompanyID.SelectedItem.Text;
						strFieldLog[4] = cboLocationID.SelectedItem.Text;
						strFieldLog[5] = cboLevel1ID.SelectedItem.Text;
						strFieldLog[6] = cboLevel2ID.SelectedItem.Text;
						strFieldLog[7] = cboLevel3ID.SelectedItem.Text;
						strFieldLog[8] = cboPositionID.SelectedItem.Text;*/


						////
					}
					//LanHTD: 17/03/2006: Cho phep luu du lieu qua trinh cu
					/*if(e.Item.ItemIndex == 0)
						btnSave.Enabled = true;
					else
						btnSave.Enabled = false;*/
					//End LanHTD
				}
				Session["ssStatusWorking"] = "Edit";
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}

		private void btnAddNew_Click(object sender, System.EventArgs e)
		{
			LoadDataDefault();
			clsCommon.ClearControlValue(this,"MdlHR/WorkingRecord.ascx");
			lblErr.Text="";
			Session["ssStatusWorking"] = "AddNew";			
			ChkDecision.Checked=true;
			if(chkShowGrid.Checked==true)
				trGrid.Style.Add("DISPLAY","block");
			else				
				trGrid.Style.Add("DISPLAY","none");
			hpSelectFile.Visible=false;	
			btnDeleteFile.Visible=false;
			DecisionChk(ChkDecision.Checked);
			//Hau
			strFieldLog[0] = txtFromDate.Text.Trim();
			strFieldLog[1] = txtToDate.Text;
			strFieldLog[2] = cboLSStatusChangeID.SelectedItem.Text;
			strFieldLog[3] = lblCompanyData.Text;
			strFieldLog[4] = lblFromLocationData.Text;
			strFieldLog[5] = lblFromDivision.Text;
			strFieldLog[6] = lblFromDepartmentData.Text;
			strFieldLog[7] = lblFromSectionData.Text;
			strFieldLog[8] = lblFromPositionData.Text;
			txtFromDate.CssClass = "input";
			txtFromDate.ReadOnly = false;
			////
		}
		/// <summary>
		/// Delete record
		/// </summary>
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				string strID="";
				for(int i=0;i<grdWorkingRecord.Items.Count;i++)
				{
					if(((CheckBox)grdWorkingRecord.Items[i].FindControl("chkSelect")).Checked==true)
					{
						strID += grdWorkingRecord.Items[i].Cells[0].Text.Trim() + "$";
					}
				}
				clsCommon.DeleteListRecord("HR_spfrmWorkingRecord","WorkingRecordID",SqlDbType.NVarChar,12,strID);
				LoadDataGrid();
				LoadDataDefault();
				DecisionChk(ChkDecision.Checked);
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}
		private void btnList_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(Request.ApplicationPath.Trim() + "/Editpage.aspx?ModuleID=HR&ParentID=8&FunctionID=14&Ascx=MdlHR/EmpList.ascx");
		}
		/// <summary>
		/// Change page index
		/// </summary>
		private void grdWorkingRecord_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			try
			{
				LoadDataGrid();
				grdWorkingRecord.CurrentPageIndex=e.NewPageIndex;
				grdWorkingRecord.DataBind();
				if(grdWorkingRecord.Items.Count>1 && grdWorkingRecord.CurrentPageIndex==0)
				{
					((LinkButton)grdWorkingRecord.Items[0].FindControl("btnEdit")).Enabled = true;
					((CheckBox)grdWorkingRecord.Items[0].FindControl("chkSelect")).Enabled = true;
				}
			}
			catch(Exception ex)			
			{
				string str = ex.Message;
			}
		}

		private void btnExport_Click(object sender, System.EventArgs e)
		{
			grdWorkingRecord.AllowPaging = false;
			this.BindDataGrid();
			iHRPCore.DataGridExcelExporter myExcelXport=new DataGridExcelExporter(grdWorkingRecord);
			myExcelXport.Export("");
			myExcelXport = null;
			this.grdWorkingRecord.AllowPaging = true;
			this.BindDataGrid();

			/*iHRPCore.DataGridExcelExporter myExcelXport=new DataGridExcelExporter(grdWorkingRecord);
			myExcelXport.Export("");
			myExcelXport = null;
			*/
		}

		private void btnImport_Click(object sender, System.EventArgs e)
		{
			//---copy file to server
			/*if(Path.GetExtension(this.txtFile.Value).Trim() != ".xls")
			{
				lblErr.Text = "File is not format (.xls)";
				return;
			}
			string strFiletmp="";
			try 
			{
				if (this.txtFile.Value != "")
				{
					strFiletmp = this.txtFile.Value;
					strFiletmp = Path.GetFileName(strFiletmp);
					this.txtFile.PostedFile.SaveAs(Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrImportFileFolder"].Trim() + strFiletmp);
					System.IO.File.SetAttributes(Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrImportFileFolder"].Trim() + strFiletmp, System.IO.FileAttributes.Normal) ;
				}
				else
				{
					lblErr.Text = "Please enter the path of filename!";
					return ;
				}				
			}
			catch (Exception exp)
			{
				this.lblErr.Text=exp.Message;
				return ;
			}
			//-- end copy file
			try{
				lblErr.Text = clsHRWorkingRecord.ImportSalary(Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrImportFileFolder"].Trim() + strFiletmp);
			}
			catch (Exception exp)
			{
				this.lblErr.Text=exp.Message;
				return ;
			}*/
		}

		#region Hau

		static public String[] strFieldLog;

		private void Save_ChangeLog()
		{	

		}

		private void SaveLog(String UserName, String ActionTime, String EmpID, String EmpName, String UserAction, String[][] FieldChanged, int FieldCount)
		{
		
		}

		#endregion

		private void cboGrade_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				string strLevelGradeID = cboGrade.SelectedValue.Trim();
				string strTextField = strLanguage == "VN"?"VNName":"Name";
				clsCommon.LoadDropDownListControl(cboRank,"sp_GetDataCombo @TableName='LS_tblRank',@Fields='LSRankID as [ID]," + strTextField + " as Name',@Where=' and LSGradeID=N''" + strLevelGradeID.Trim() + "'''","ID","Name",true);
				cboRank.SelectedValue = this.txtRankID.Value.Trim();
				cboRank_SelectedIndexChanged(null,null);
			}
			catch(Exception ex)
			{				
			}
		}

		private void cboRank_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				string strRankID = cboRank.SelectedValue.Trim();
				string strTextField = strLanguage == "VN"?"VNName":"Name";
				//clsCommon.LoadDropDownListControl(cboJobCodeID,"sp_GetDataCombo @TableName='LS_tblJobCode',@Fields='LSJobCodeCode," + strTextField + " as Name',@Where=' and LSRankCode=N''" + strRankID.Trim() + "'''","LSJobCodeCode","Name",true);
				//cboJobCodeID.SelectedValue = this.txtJobCodeID.Value.Trim();
			}
			catch(Exception ex)
			{
			}
		}
		private string import_file()
		{
			//---copy file to server			
			string mext = "";
			if( txtFile.Value!="")				
			{
				mext = this.txtFile.Value.Substring(this.txtFile.Value.LastIndexOf("."));
				if(mext != ".xls" && mext!= ".doc" && mext!= ".pdf" )
				{
					//lblErr.Text = "Tập tin phải có định dạng là (.xls) hoặc (.doc)";
					lblErr.Text=clsChangeLang.getStringAlert("0025",strLanguage);
					return "";
				}
			}
			string strFiletmp="";			
			try 
			{
				if (this.txtFile.Value != "")
				{
					strFiletmp = Session["EmpID"]+ "_"+ txtFromDate.Text.Replace("/","") + "_" + "WR" + Path.GetExtension(this.txtFile.Value).Trim();
					if (System.IO.File.Exists(Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrImportFileFolder"].Trim() + strFiletmp))
						System.IO.File.SetAttributes(Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrImportFileFolder"].Trim() + strFiletmp, System.IO.FileAttributes.Normal) ;
					this.txtFile.PostedFile.SaveAs(Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrImportFileFolder"].Trim() + strFiletmp);
					System.IO.File.SetAttributes(Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrImportFileFolder"].Trim() + strFiletmp, System.IO.FileAttributes.Normal) ;
					return strFiletmp;
				}
				else
				{
					//lblErr.Text = "Please enter the path of filename!";
					lblErr.Text=clsChangeLang.getStringAlert("0026",strLanguage);
					return "null";
				}				
			}
			catch (Exception exp)
			{				
				return "";
			}
			//-- end copy file
		}

		private void btnDeleteFile_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (!hpSelectFile.Text.Equals(""))
				{
					System.IO.File.Delete(Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrImportFileFolder"].Trim() + hpSelectFile.Text) ;
					DataTable dtTemp = new DataTable();
					dtTemp=clsCommon.GetDataTable("HR_spfrmWorkingRecord @Activity='DeleteFileAttach',@EmpID='" + Session["EmpID"] + "',@AttachFile='" + hpSelectFile.Text + "'");
					dtTemp.Dispose();
					hpSelectFile.Text="";
					hpSelectFile.Visible=false;
					btnDeleteFile.Visible=false;
				}
			}
			catch(Exception exp)
			{
				this.lblErr.Text=exp.Message;
				return ;
			}
		}
		private void DecisionChk(bool bValue)
		{
			if (bValue)
			{
				trDecision1.Style.Add("DISPLAY","block");
				trDecision2.Style.Add("DISPLAY","block");
				trDecision3.Style.Add("DISPLAY","block");
				ChkDecision.Checked=bValue;
				
			}
			else
			{
				trDecision1.Style.Add("DISPLAY","none");
				trDecision2.Style.Add("DISPLAY","none");
				trDecision3.Style.Add("DISPLAY","none");
				ChkDecision.Checked=bValue;
			}
		}

		private void grdWorkingRecord_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}
	}
}
