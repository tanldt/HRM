namespace iHRPCore.MdlPR
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.IO;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.Com;
	using iHRPCore.HRComponent;
	using iHRPCore.TMSComponent; 
	using iHRPCore.PRComponent;
	using System.Data.SqlClient;
	using System.Configuration;
	using iHRPCore.Include;

	/// <summary>
	///		Summary description for SalaryRecord.
	/// </summary>
	public class SalaryRecord : System.Web.UI.UserControl
	{
		#region Variables

		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label Label19;
		protected System.Web.UI.WebControls.TextBox txtEffDate_Basic;
		protected System.Web.UI.WebControls.Label Label25;
		protected System.Web.UI.WebControls.TextBox txtEndDate_Basic;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtDecision_Basic;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.TextBox txtSignDate_Basic;
		protected System.Web.UI.WebControls.Label Label26;
		protected System.Web.UI.WebControls.TextBox txtSigner_Basic;
		protected System.Web.UI.WebControls.Label Label27;
		protected System.Web.UI.WebControls.TextBox txtPosOfSigner_Basic;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.DropDownList cboVerOfSal_Basic;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.DropDownList cboRankOfSal_Basic;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.TextBox txtScaleOfSal_Basic;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtGradeOfSal_Basic;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.TextBox txtPromotion_Basic;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.TextBox txtSalStandard_Basic;
		protected System.Web.UI.WebControls.Label Label32;
		protected System.Web.UI.WebControls.TextBox txtAllCofe1_Basic;
		protected System.Web.UI.WebControls.Label Label33;
		protected System.Web.UI.WebControls.TextBox txtAllCofe2_Basic;
		protected System.Web.UI.WebControls.Label Label23;
		protected System.Web.UI.WebControls.TextBox txtNote_Basic;
		protected System.Web.UI.WebControls.CheckBox chkShowGrid;
		protected System.Web.UI.WebControls.LinkButton btnAddNew;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.LinkButton btnImport;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.HtmlControls.HtmlTableRow trGrid;
		protected System.Web.UI.WebControls.Label Label29;
		protected System.Web.UI.WebControls.TextBox txtFromDate;
		protected System.Web.UI.WebControls.TextBox txtToDate;
		protected System.Web.UI.WebControls.TextBox txtDecisionNo;
		protected System.Web.UI.WebControls.TextBox txtSignDate;
		protected System.Web.UI.WebControls.TextBox txtSigner;
		protected System.Web.UI.WebControls.TextBox txtSignPosition;
		protected System.Web.UI.WebControls.RadioButton opt2;
		protected System.Web.UI.WebControls.RadioButton opt1;
		protected System.Web.UI.WebControls.Label lblVerOfSal;
		protected System.Web.UI.WebControls.Label lblDecisionNo;
		protected System.Web.UI.WebControls.Label lblSignDate;
		protected System.Web.UI.WebControls.Label lblSigner;
		protected System.Web.UI.WebControls.Label lblSignPosition;
		protected System.Web.UI.WebControls.Label lblAttachFile;
		protected System.Web.UI.WebControls.Label lblEffDate;
		protected System.Web.UI.WebControls.Label lblOtherSalary1;
		protected System.Web.UI.WebControls.TextBox txtOtherSalary1;
		protected System.Web.UI.WebControls.Label lblOtherSalary2;
		protected System.Web.UI.WebControls.TextBox txtOtherSalary2;
		protected System.Web.UI.WebControls.TextBox txtOtherSalary3;
		protected System.Web.UI.WebControls.Label lblOtherSalary3;
		protected System.Web.UI.WebControls.Label lblSalStandard;
		protected System.Web.UI.WebControls.Label lblAllowanceCoef1;
		protected System.Web.UI.WebControls.TextBox txtAllowanceCoef1;
		protected System.Web.UI.WebControls.Label lblAllowanceCoef2;
		protected System.Web.UI.WebControls.TextBox txtAllowanceCoef2;
		protected System.Web.UI.WebControls.Label lblAllowanceCoef3;
		protected System.Web.UI.WebControls.TextBox txtAllowanceCoef3;
		protected System.Web.UI.WebControls.Label lblAllowanceCoef4;
		protected System.Web.UI.WebControls.TextBox txtAllowanceCoef4;
		protected System.Web.UI.WebControls.Label lblAllowanceCoef5;
		protected System.Web.UI.WebControls.TextBox txtAllowanceCoef5;
		protected System.Web.UI.WebControls.Label lblAllowanceCoef6;
		protected System.Web.UI.WebControls.TextBox txtAllowanceCoef6;
		protected System.Web.UI.WebControls.Label lblAllowanceCoef7;
		protected System.Web.UI.WebControls.TextBox txtAllowanceCoef7;
		protected System.Web.UI.WebControls.Label lblAllowanceCoef8;
		protected System.Web.UI.WebControls.TextBox txtAllowanceCoef8;
		protected System.Web.UI.WebControls.Label lblAllowanceCoef9;
		protected System.Web.UI.WebControls.TextBox txtAllowanceCoef9;
		protected System.Web.UI.WebControls.Label lblAllowanceCoef10;
		protected System.Web.UI.WebControls.TextBox txtAllowanceCoef10;
		protected System.Web.UI.WebControls.Label lblNote;
		protected System.Web.UI.WebControls.TextBox txtNote;
		protected System.Web.UI.HtmlControls.HtmlTableRow trAdAll1;
		protected System.Web.UI.WebControls.CheckBox chkAdAll2;
		protected System.Web.UI.WebControls.CheckBox chkAdOtherSalary2;
		protected System.Web.UI.WebControls.CheckBox chkAdOtherSalary3;
		protected System.Web.UI.WebControls.CheckBox chkAdAll1;
		protected System.Web.UI.WebControls.CheckBox chkAdAll3;
		protected System.Web.UI.WebControls.CheckBox chkAdAll4;
		protected System.Web.UI.WebControls.CheckBox chkAdAll5;
		protected System.Web.UI.WebControls.CheckBox chkAdAll6;
		protected System.Web.UI.WebControls.CheckBox chkAdAll7;
		protected System.Web.UI.WebControls.CheckBox chkAdAll8;
		protected System.Web.UI.WebControls.CheckBox chkAdAll9;
		protected System.Web.UI.WebControls.CheckBox chkAdAll10;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdOtherSalary2lbl;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdOtherSalary2txt;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdOtherSalary2cbo;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdOtherSalary3lbl;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdOtherSalary3txt;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdAll1lbl;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdAll1txt;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdAll1cbo;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdAll2lbl;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdAll2txt;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdAll3lbl;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdAll3txt;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdAll3cbo;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdAll4lbl;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdAll4txt;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdAll5lbl;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdAll5txt;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdAll5cbo;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdAll6lbl;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdAll6txt;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdAll7lbl;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdAll7txt;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdAll7cbo;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdAll8lbl;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdAll8txt;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdAll9lbl;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdAll9txt;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdAll9cbo;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdAll10lbl;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdAll10txt;
		protected System.Web.UI.WebControls.Label lblSalaryPct;
		protected System.Web.UI.WebControls.TextBox txtBasicSalary;
		protected System.Web.UI.WebControls.TextBox txtSalaryPct;
		protected System.Web.UI.WebControls.TextBox txtAttachFile;
		protected System.Web.UI.WebControls.DataGrid dtgList;
		protected System.Web.UI.WebControls.LinkButton btnDeleteFile;
		protected System.Web.UI.WebControls.HyperLink hpSelectFile;
		protected System.Web.UI.HtmlControls.HtmlInputFile txtFile;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtSalaryRecordID;
		protected System.Web.UI.HtmlControls.HtmlTableRow trDecision1;
		protected System.Web.UI.HtmlControls.HtmlTableRow trDecision2;
		protected System.Web.UI.HtmlControls.HtmlTableRow trDecision3;
		protected System.Web.UI.WebControls.DropDownList cboLSSalaryVersionID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLSSalaryRankID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLSSalaryGradeID;
		protected System.Web.UI.WebControls.RadioButton Radiobutton1;
		protected System.Web.UI.WebControls.RadioButton Radiobutton2;
		protected System.Web.UI.WebControls.TextBox txtActualDate;
		protected System.Web.UI.WebControls.TextBox txtIncreaseDate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLSRankID;
		protected System.Web.UI.WebControls.Label lblActualDate;
		protected System.Web.UI.WebControls.Label lblIncreaseDate;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label lblGradeOfSal;
		protected System.Web.UI.WebControls.TextBox txtMonthIncreaseSal;
		protected System.Web.UI.WebControls.DropDownList cboLSSalaryGradeID;
		protected System.Web.UI.WebControls.RadioButtonList optIsAfterTax;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.TextBox txtSalarySI;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.DropDownList cboLSCurrencyTypeID;
		protected System.Web.UI.WebControls.CheckBox chkIsUI;
        protected System.Web.UI.WebControls.TextBox txtEmpID1;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.Label Label14;
		protected System.Web.UI.WebControls.DropDownList cboLSCurrencyTypeID1;
		protected System.Web.UI.WebControls.DropDownList cboLSCurrencyTypeID2;
		protected System.Web.UI.WebControls.DropDownList cboLSCurrencyTypeID3;
		public string strLanguage = "VN";

		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"VN";
			if(!Page.IsPostBack)
			{
				LoadDataCombo();
				BindDataGrid();
				Session["ssStatus"] = "AddNew";
				setDecision(1);
				
			}
			opt1.Attributes.Add("onclick","return showtable(this.id)");
			opt2.Attributes.Add("onclick","return showtable(this.id)");
			btnSave.Attributes.Add("onclick","return checkSave()");
			btnDelete.Attributes.Add("onclick","return checkdelete()");			
			btnDeleteFile.Attributes.Add("onclick", "return checkDeleteAtt()");
		}
		private void BindDataGrid()
		{
			DataTable dtList = new DataTable();
			try
			{				
				dtList = clsSalaryRecord.GetDataByEmpID(Session["EmpID"],strLanguage);
				dtgList.DataSource = dtList;
				dtgList.CurrentPageIndex = 0;
				dtgList.DataBind();
				if(dtgList.Items.Count>1)
				{
					//((LinkButton)grdWorkingRecord.Items[0].FindControl("btnEdit")).Enabled = true;
					((CheckBox)dtgList.Items[0].FindControl("chkSelect")).Enabled = true;
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
		private void LoadDataCombo()
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";			
			/*clsCommon.LoadDropDownListControl(cboLSSalaryRankID,"sp_GetDataCombo @TableName='LS_tblSalaryRank',@Fields='LSSalaryRankID as [ID]," + strTextField + " as Name'","ID","Name",true);*/
			clsCommon.LoadDropDownListControl(cboLSSalaryVersionID,"sp_GetDataCombo @TableName='LS_tblSalaryVersion',@Fields='LSSalaryVersionID as [ID]," + strTextField + " as Name',@Where=' and Type=1'","ID","Name",true);
			clsCommon.LoadDropDownListControl(cboLSCurrencyTypeID,"sp_GetDataCombo @TableName='LS_tblCurrencyType',@Fields='LSCurrencyTypeID as [ID]," + strTextField + " as Name'","ID","Name",true);

			clsCommon.LoadDropDownListControl(cboLSCurrencyTypeID1,"sp_GetDataCombo @TableName='LS_tblCurrencyType',@Fields='LSCurrencyTypeID as [ID]," + strTextField + " as Name'","ID","Name",true);
			clsCommon.LoadDropDownListControl(cboLSCurrencyTypeID2,"sp_GetDataCombo @TableName='LS_tblCurrencyType',@Fields='LSCurrencyTypeID as [ID]," + strTextField + " as Name'","ID","Name",true);
			clsCommon.LoadDropDownListControl(cboLSCurrencyTypeID3,"sp_GetDataCombo @TableName='LS_tblCurrencyType',@Fields='LSCurrencyTypeID as [ID]," + strTextField + " as Name'","ID","Name",true);

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
			this.cboLSSalaryVersionID.SelectedIndexChanged += new System.EventHandler(this.cboLSSalaryVersionID_SelectedIndexChanged);
			this.cboLSSalaryGradeID.SelectedIndexChanged += new System.EventHandler(this.cboLSSalaryGradeID_SelectedIndexChanged);
			this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			this.dtgList.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgList_ItemCommand);
			this.dtgList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgList_PageIndexChanged);
			this.dtgList.SelectedIndexChanged += new System.EventHandler(this.dtgList_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSave_Click(object sender, System.EventArgs e)
		{   string f="";
		    int type=0;
			try
			{
				/*if (clsSalaryRecord.CheckValidEffectiveDate(Session["EmpID"].ToString().Trim(),txtFromDate.Text.Trim(),txtSalaryRecordID.Value.Trim(),Session["ssStatus"].ToString().Trim())==false)
				{
					lblErr.Text="Ngày hiệu lực phải lớn hơn ngày hiệu lực trước.";
					return;
				}*/
				string strAttachFile="";
				if(Session["ssStatus"].ToString().Trim().ToUpper()=="ADDNEW")
				{
					if (!txtFile.Value.Equals("")) strAttachFile=import_file();	
					if (strAttachFile=="" && txtFile.Value!="") return;
					if (strAttachFile.Equals("null"))
					{
						txtAttachFile.Text="";
					}
					else
					{
						txtAttachFile.Text=strAttachFile;
					}
				}				
				else
				{
					if (!txtFile.Value.Equals(""))
					{				
						strAttachFile=import_file();
						if (strAttachFile.Equals("null"))
						{
							txtAttachFile.Text="";
						}
						else
						{
							if (!strAttachFile.Equals(""))
								txtAttachFile.Text=strAttachFile;
							else
								return;							
						}
					}			
				}

				if(Session["ssStatus"].ToString().Trim().ToUpper()=="ADDNEW"){
					f=clsCommon.sImpactDB("@Creater",Session["AccountLogin"],Session["EmpID"].ToString().Trim(),"Save",this,"PR_spfrmSalary");
					type=1;
				}
				else{
					//f=clsCommon.UpdateByKey("@Creater",Session["AccountLogin"],"SalaryRecordID",txtSalaryRecordID.Value.Trim(),"Update",this,"PR_spfrmSalary");
					//EmpHeaderSearch1.txtEmpID.Text.Trim()=Session["EmpID"].ToString().Trim();
					f=clsCommon.sUpdateByKey("@Creater",Session["AccountLogin"],"SalaryRecordID",txtSalaryRecordID.Value.Trim(),"Update",this,"PR_spfrmSalary");
					type=2;
				}

				lblErr.Text="";
				clsCommon.ClearControlValue(this,"mdlPR/SalaryRecord.ascx");
				Session["ssStatus"]="AddNew";
				BindDataGrid();
				btnSave.Enabled=true;
				hpSelectFile.Visible=false;
				btnDeleteFile.Visible=false;
				
                if(f!="")//fail
                {
                   clsChangeLang.popupWindow(this.Parent,f,"EN","",0);
                }
                else//success
                {
                     if(type==1)
                    {//adnew
                        clsChangeLang.popupWindow(this.Parent,"0046","EN","",1);				        
                    }
                    else
                    {//update
                        clsChangeLang.popupWindow(this.Parent,"0044","EN","",1);
                    }
                }
			}
			catch(Exception ex)
			{
				lblErr.Text=ex.ToString();
			}			
		}
		private string import_file()
		{
			//---copy file to server
			string mext = "";
			mext = this.txtFile.Value.Substring(this.txtFile.Value.LastIndexOf("."));
			if(mext != ".xls" && mext!= ".doc" )
			{
				clsChangeLang.popupWindow(this.Parent,"0025",strLanguage,"",0);
				return "";
			}
			string strFiletmp="";			
			try 
			{
				if (this.txtFile.Value != "")
				{
					strFiletmp = Session["EmpID"]+ "_"+ txtSignDate.Text.Replace("/","") + "_" + "SalRec" + Path.GetExtension(this.txtFile.Value).Trim();
					if (System.IO.File.Exists(Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrImportFileFolder"].Trim() + strFiletmp))
						System.IO.File.SetAttributes(Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrImportFileFolder"].Trim() + strFiletmp, System.IO.FileAttributes.Normal) ;
					this.txtFile.PostedFile.SaveAs(Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrImportFileFolder"].Trim() + strFiletmp);
					System.IO.File.SetAttributes(Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrImportFileFolder"].Trim() + strFiletmp, System.IO.FileAttributes.Normal) ;
					return strFiletmp;
				}
				else
				{
					lblErr.Text = "Nhập đường dẫn của file!";
					return "null";
				}				
			}
			catch (Exception exp)
			{
				this.lblErr.Text=exp.Message;
				return "";
			}
			//-- end copy file
		}

		private void dtgList_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				if (e.CommandName.Trim().ToUpper()=="EDIT")
				{
					txtSalaryRecordID.Value=dtgList.Items[e.Item.ItemIndex].Cells[0].Text.Trim();
					DataRow iRow=clsSalaryRecord.GetDataByID(txtSalaryRecordID.Value);
					if (iRow!=null)
					{
						txtSalaryRecordID.Value = iRow["SalaryRecordID"].ToString();						
						txtFromDate.Text = iRow["FromDate"].ToString();
						txtIncreaseDate.Text=iRow["IncreaseDate"].ToString();
						txtActualDate.Text=iRow["ActualDate"].ToString();
						txtToDate.Text = iRow["ToDate"].ToString();
						txtDecisionNo.Text = iRow["DecisionNo"].ToString();
						txtSignDate.Text = iRow["SignDate"].ToString();
						txtSigner.Text = iRow["Signer"].ToString();
						txtSignPosition.Text = iRow["SignPosition"].ToString();
						txtAttachFile.Text = iRow["AttachFile"].ToString();	
                        //*lamtd 1902
						if (iRow["IsAfterTax"].ToString() == "False")
							optIsAfterTax.SelectedValue = "0";
						else
							optIsAfterTax.SelectedValue = "1";
						if (iRow["IsUI"].ToString() == "False")
							chkIsUI.Checked = false;
						else
							chkIsUI.Checked = true;


						txtAllowanceCoef1.Text = iRow["AllowanceCoef1"].ToString();
						txtAllowanceCoef2.Text = iRow["AllowanceCoef2"].ToString();
						txtAllowanceCoef3.Text = iRow["AllowanceCoef3"].ToString();
						txtAllowanceCoef4.Text = iRow["AllowanceCoef4"].ToString();
						txtAllowanceCoef5.Text = iRow["AllowanceCoef5"].ToString();
						txtAllowanceCoef6.Text = iRow["AllowanceCoef6"].ToString();
						txtAllowanceCoef7.Text = iRow["AllowanceCoef7"].ToString();
						txtAllowanceCoef8.Text = iRow["AllowanceCoef8"].ToString();
						txtAllowanceCoef9.Text = iRow["AllowanceCoef9"].ToString();
						txtAllowanceCoef10.Text = iRow["AllowanceCoef10"].ToString();
						txtBasicSalary.Text = iRow["BasicSalary"].ToString();
						txtOtherSalary1.Text = iRow["OtherSalary1"].ToString();
						txtOtherSalary2.Text = iRow["OtherSalary2"].ToString();
						txtOtherSalary3.Text = iRow["OtherSalary3"].ToString();
						txtSalaryPct.Text = iRow["SalaryPct"].ToString();
						txtSalarySI.Text = iRow["SalarySI"].ToString();
						cboLSCurrencyTypeID.SelectedValue = iRow["LSCurrencyTypeID"].ToString();
						cboLSCurrencyTypeID1.SelectedValue = iRow["LSCurrencyTypeID1"].ToString();
						cboLSCurrencyTypeID2.SelectedValue = iRow["LSCurrencyTypeID2"].ToString();
						cboLSCurrencyTypeID3.SelectedValue = iRow["LSCurrencyTypeID3"].ToString();
						txtNote.Text = iRow["Note"].ToString();
						if (iRow["DecisionNo"].ToString()!="")
						{
							opt2.Checked=true;
							setDecision(2);
						}
						else
						{
							opt1.Checked=true;
							setDecision(1);
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
					}
					/*if(e.Item.ItemIndex == 0)
						btnSave.Enabled = true;
					else
						btnSave.Enabled = false;*/
				}
				Session["ssStatus"] = "Edit";
			}
			catch(Exception ex)
			{
				lblErr.Text=ex.ToString();
			}
		}
		private void setDecision(int sValue)
		{
			if (sValue==1)
			{
				opt1.Checked=true;
				opt2.Checked=false;
				trDecision1.Style.Add("DISPLAY","none");
				trDecision2.Style.Add("DISPLAY","none");
				trDecision3.Style.Add("DISPLAY","none");
			}
			else
			{
				opt2.Checked=true;
				opt1.Checked=false;
				trDecision1.Style.Add("DISPLAY","block");
				trDecision2.Style.Add("DISPLAY","block");
				trDecision3.Style.Add("DISPLAY","block");
			}
		}

		private void btnAddNew_Click(object sender, System.EventArgs e)
		{
			clsCommon.ClearControlValue(this,"MdlPR/SalaryRecord.ascx");
			cboLSSalaryGradeID.Items.Clear();
			//cboLSSalaryRankID.Items.Clear();

			lblErr.Text="";
			Session["ssStatus"] = "AddNew";						
			if(chkShowGrid.Checked==true)
				trGrid.Style.Add("DISPLAY","block");
			else				
				trGrid.Style.Add("DISPLAY","none");
			hpSelectFile.Visible=false;	
			btnDeleteFile.Visible=false;
			chkIsUI.Checked = true;
			setDecision(1);
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{   bool f;
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
				f=clsCommon.DeleteListRecord("PR_spfrmSalary","SalaryRecordID",SqlDbType.NVarChar,12,strID);
				BindDataGrid();
				clsCommon.ClearControlValue(this,"MdlPR/SalaryRecord.ascx");
				setDecision(1);
				
                if(f==true)
                {
                    clsChangeLang.popupWindow(this.Parent,"0104","EN","",1);				        
                }
                else
                {
                    clsChangeLang.popupWindow(this.Parent,"0048","EN","",0);
                }
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}

		private void btnExport_Click(object sender, System.EventArgs e)
		{
			iHRPCore.DataGridExcelExporter myExcelXport=new DataGridExcelExporter(dtgList);
			myExcelXport.Export("");
			myExcelXport = null;		
		}

		private void btnDeleteFile_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (!txtAttachFile.Text.Equals(""))
				{
					System.IO.File.Delete(Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrImportFileFolder"].Trim() + txtAttachFile.Text) ;
					DataTable dtTemp = new DataTable();
					dtTemp=clsCommon.GetDataTable("PR_spfrmSalary @Activity='DeleteFileAttach',@SalaryRecordID='" + txtSalaryRecordID.Value + "',@AttachFile='" + txtAttachFile.Text + "'");
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

		private void dtgList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			try
			{
				BindDataGrid();
				dtgList.CurrentPageIndex=e.NewPageIndex;
				dtgList.DataBind();
				if(dtgList.Items.Count>1 && dtgList.CurrentPageIndex==0)
				{					
					((CheckBox)dtgList.Items[0].FindControl("chkSelect")).Enabled = true;
				}
			}
			catch(Exception ex)			
			{
				string str = ex.Message;
			}
		}

		private void cboLSSalaryVersionID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			string strSalaryVersionID = cboLSSalaryVersionID.SelectedValue.Trim();			
			if (!strSalaryVersionID.Equals(""))
			{
				clsCommon.LoadDropDownListControl(cboLSSalaryGradeID,"sp_GetDataCombo @TableName='LS_tblSalaryGrade',@Fields='LSSalaryGradeID as [ID]," + strTextField + " as Name',@Where='and LSSalaryVersionID=''" + strSalaryVersionID + "'''","ID","Name",true);
				cboLSSalaryGradeID.SelectedValue = this.txtLSSalaryGradeID.Value.Trim();
				cboLSSalaryGradeID_SelectedIndexChanged(null,null);
			}
			else
			{
				cboLSSalaryGradeID.Items.Clear();
			}
		}

		private void cboLSSalaryGradeID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
//			string strTextField = strLanguage == "VN"?"VNName":"Name";
//			string strSalaryGradeID = cboLSSalaryGradeID.SelectedValue.Trim();			
//			if (!strSalaryGradeID.Equals(""))
//			{
//				clsCommon.LoadDropDownListControl(cboLSSalaryRankID,"sp_GetDataCombo @TableName='LS_tblSalaryRank',@Fields='LSSalaryRankID as [ID]," + strTextField + " as Name',@Where='and LSSalaryGradeID=''" + strSalaryGradeID + "'''","ID","Name",true);
//				cboLSSalaryRankID.SelectedValue = this.txtLSSalaryRankID.Value.Trim();				
//			}
//			else
//			{
//				cboLSSalaryRankID.Items.Clear();
//			}
		}

        private void dtgList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        
        }
		
	}
}