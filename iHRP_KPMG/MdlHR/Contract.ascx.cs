namespace iHRPCore.MdlHR
{
	using System;
	using System.Data;
		using System.IO;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.HRComponent;
	using iHRPCore.Com;
	using System.Data.SqlClient;
	using System.Configuration;
	using FPTToolWeb.Exports;

	/// <summary>
	///		Summary description for Contract.
	/// </summary>
	public class Contract : System.Web.UI.UserControl
	{
		#region Declare
		protected System.Web.UI.WebControls.Label lblErr;

		static DataTable m_dt=new DataTable();
		public string m_strEmpID;
		public static int mAddNew=0;

		public string strLanguage = "EN";
		protected System.Web.UI.WebControls.Label lblSignDate;
		protected System.Web.UI.WebControls.TextBox txtSignDate;
		protected System.Web.UI.WebControls.Label lblSigner;
		protected System.Web.UI.WebControls.TextBox txtSigner;
		protected System.Web.UI.WebControls.TextBox txtTemp;
		protected System.Web.UI.WebControls.CheckBox chkShowGrid;
		protected System.Web.UI.WebControls.LinkButton btnAddNew;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnList;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.TextBox Textbox7;
		protected System.Web.UI.WebControls.TextBox Textbox8;
		protected System.Web.UI.WebControls.DataGrid dtgList;
		protected System.Web.UI.WebControls.Label lblSalaryOtherData;
		protected System.Web.UI.WebControls.Label lblSalaryOther;
		protected System.Web.UI.WebControls.Label lblPosition;
		protected System.Web.UI.WebControls.Label lblJobTitle;
		protected System.Web.UI.WebControls.LinkButton btnDeleteFile;
		protected System.Web.UI.WebControls.HyperLink hpSelectFile;
		protected System.Web.UI.HtmlControls.HtmlInputFile txtFile;
		protected System.Web.UI.WebControls.Label lblContractType;
		protected System.Web.UI.WebControls.Label lblContractNo;
		protected System.Web.UI.WebControls.Label lblEffDate;
		protected System.Web.UI.WebControls.Label lblEndDate;
		protected System.Web.UI.WebControls.Label lblProbationFrom;
		protected System.Web.UI.WebControls.Label lblProbationto;
		protected System.Web.UI.WebControls.Label lblAttachFile;
		protected System.Web.UI.WebControls.Label lblPosSigner;
		protected System.Web.UI.WebControls.Label lblPositonlbl;
		protected System.Web.UI.WebControls.Label lblJobTitlelbl;
		protected System.Web.UI.WebControls.Label lblLocationlbl;
		protected System.Web.UI.WebControls.Label llblGradeSal;
		protected System.Web.UI.WebControls.Label lblGradeOfSalData;
		protected System.Web.UI.WebControls.Label lblRankSal;
		protected System.Web.UI.WebControls.Label lblRankOfSalData;
		protected System.Web.UI.WebControls.Label lblSalCoef;
		protected System.Web.UI.WebControls.Label lblSalaryCoefData;
		protected System.Web.UI.WebControls.Label lblSalarylbl;
		protected System.Web.UI.WebControls.CheckBox chkAdProbationFrom;
		protected System.Web.UI.HtmlControls.HtmlTableRow trAdProbationFrom;
		protected System.Web.UI.WebControls.DropDownList cboLSContractTypeID;
		protected System.Web.UI.WebControls.TextBox txtContractNo;
		protected System.Web.UI.WebControls.TextBox txtEffectiveDate;
		protected System.Web.UI.WebControls.TextBox txtToDate;
		protected System.Web.UI.WebControls.TextBox txtProbationFrom;
		protected System.Web.UI.WebControls.TextBox txtProbationTo;
		protected System.Web.UI.WebControls.TextBox txtSignerPosition;
		protected System.Web.UI.WebControls.TextBox txtContractID;
		protected System.Web.UI.WebControls.Label lblLocation;
		protected System.Web.UI.WebControls.TextBox txtAttachFile;
		protected System.Web.UI.WebControls.TextBox Textbox1;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtNote;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.TextBox txtDuration;
		protected System.Web.UI.WebControls.TextBox txtOverHead;
		protected System.Web.UI.WebControls.TextBox txtFringeBenefit;
		protected System.Web.UI.WebControls.TextBox txtHousingBenefit;
		protected System.Web.UI.WebControls.CheckBox chkPaySalary;
		protected System.Web.UI.WebControls.TextBox txtContractSalary;
		protected System.Web.UI.WebControls.DropDownList cboLSSupplierID;
		protected System.Web.UI.WebControls.DropDownList cboLSCurrencyTypeID;
		
		static bool m_bAddNew= true;
		#endregion Declare
		
		
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
			this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnList.Click += new System.EventHandler(this.btnList_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			this.dtgList.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgList_ItemCommand);
			this.dtgList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgList_PageIndexChanged);
			this.btnDeleteFile.Click += new System.EventHandler(this.btnDeleteFile_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			Session["CurFunction"] = Request.Params["FunctionID"].Trim();
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";	
			this.lblErr.Text = "";

			if(!Page.IsPostBack)
			{
				LoadDataCombo();
				LoadCurrentInfo();
				LoadDataGrid();
				Session["ssActivityFlag"] = "AddNew";
			}
			txtSigner.Text="";
			txtSignerPosition.Text ="";
			btnSave.Attributes.Add("OnClick", " return validform()");
			btnDelete.Attributes.Add("OnClick", "return checkdelete()");
			this.btnDeleteFile.Attributes.Add("OnClick","confirmDeleteFile()");
		}
		

		/// <summary>
		/// Load current data of emp
		/// </summary>
		private void LoadCurrentInfo()
		{
			DataRow iRow = clsHRContract.GetCurrentInfoByEmpID(Session["EmpID"], strLanguage);
			if (iRow == null) return;
			try
			{
				lblPosition.Text = iRow["Position"].ToString().Trim();
				lblJobTitle.Text = iRow["JobTitle"].ToString().Trim();
				lblLocation.Text = iRow["Location"].ToString().Trim();				
				lblGradeOfSalData.Text  = iRow["GradeOfSalData"].ToString().Trim();
				lblRankOfSalData.Text = iRow["RankOfSalData"].ToString().Trim();
				lblSalaryCoefData.Text = iRow["SalaryCoefData"].ToString().Trim();
				txtContractSalary.Text = iRow["Salary"].ToString().Trim();
				lblSalaryOtherData.Text = iRow["SalaryOtherData"].ToString().Trim();
			}
			catch(Exception ex)
			{
				//lblErr.Text = ex.Message;
				clsChangeLang.popupWindowExp(this.Parent,ex);
			}
		}


		/// <summary>
		/// Get data for combo
		/// </summary>
		private void LoadDataCombo()
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			clsCommon.LoadDropDownListControl(cboLSContractTypeID,"sp_GetDataCombo @TableName='LS_tblContractType',@Fields='LSContractTypeID," + strTextField + " as Name'","LSContractTypeID","Name",true);								
			clsCommon.LoadDropDownListControl(cboLSCurrencyTypeID,"sp_GetDataCombo @TableName='LS_tblCurrencyType',@Fields='LSCurrencyTypeID," + strTextField + " as Name'","LSCurrencyTypeID","Name",true);			
			clsCommon.LoadDropDownListControl(cboLSSupplierID,"sp_GetDataCombo @TableName='LS_tblSupplier',@Fields='LSSupplierID," + strTextField + " as Name'","LSSupplierID","Name",true);			

		}


		/// <summary>
		/// Load all docuemnt records of Employee
		/// </summary>
		private void LoadDataGrid()
		{
			DataTable dtList = new DataTable();
			try
			{				
				dtList = clsHRContract.GetDataByEmpID(Session["EmpID"]);
				dtgList.DataSource = dtList;
				dtgList.CurrentPageIndex = 0;
				dtgList.DataBind();

				if(dtgList.Items.Count>0)
				{
					//((LinkButton)grdWorkingRecord.Items[0].FindControl("btnEdit")).Enabled = true;
					((CheckBox)dtgList.Items[0].FindControl("chkSelect")).Enabled = true;
					//((LinkButton)dtgList.Items[0].FindControl("btnEffectiveDate")).Enabled = true;
				}
			}
			catch(Exception ex)
			{
				//lblErr.Text = ex.Message;
				clsChangeLang.popupWindowExp(this.Parent,ex);
			}
			finally
			{
				dtList.Dispose();
			}
		}

				
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			string strImportErr = "";
			
			if(Session["ssActivityFlag"].ToString().Trim()=="AddNew")
			{
				if (clsHRContract.CheckEffDate(Session["EmpID"], txtEffectiveDate.Text.Trim(), "CheckEffDateAddNew", txtContractID.Text.Trim()))
				{													
					txtAttachFile.Text  = import_file();
					strImportErr =  txtAttachFile.Text;
					if (strImportErr.Equals("") && txtFile.Value!="")
						return;
					if (strImportErr != "E.f")  // error
						clsCommon.ImpactDB(Session["EmpID"].ToString().Trim(),"AddNew",this,"HR_spfrmContract");										
				}
				else
				{
					//lblErr.Text=clsChangeLang.getStringAlert("0029",strLanguage);
					clsChangeLang.popupWindow(this.Parent,"0029",strLanguage,"",0);
					return;
				}
			}
			else
			{
				if (clsHRContract.CheckEffDate(Session["EmpID"], txtEffectiveDate.Text.Trim(), "CheckEffDateUpdate", txtContractID.Text.Trim()))
				{	
					txtAttachFile.Text  = import_file();
					strImportErr =  txtAttachFile.Text;
					if (strImportErr.Equals("") && txtFile.Value!="")
						return;
					if (strImportErr != "E.f")  // error
						clsCommon.ImpactDB(Session["EmpID"].ToString().Trim(),"Update",this,"HR_spfrmContract");
				}
				else
				{
					//lblErr.Text=clsChangeLang.getStringAlert("0029",strLanguage);
					clsChangeLang.popupWindow(this.Parent,"0029",strLanguage,"",0);
					return;
				}
			}
				
			if (strImportErr == "E.f")  return;

			clsCommon.ClearControlValue(this);
			hpSelectFile.Text = "";
			//------------------------------------------------
			lblPosition.Text = "";
			lblJobTitle.Text = "";
			lblLocation.Text = "";				
			lblGradeOfSalData.Text  = "";
			lblRankOfSalData.Text = "";
			lblSalaryCoefData.Text = "";
			txtContractSalary.Text = "";
			lblSalaryOtherData.Text = "";
			//------------------------------------------------

			btnDeleteFile.Visible = false;
			Session["ssActivityFlag"] = "AddNew";
			LoadDataGrid();			
		}

			
		private void dtgList_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string strTextField1 = strLanguage == "VN"?"VNName":"Name";
			try
			{
				if(e.CommandName.Trim().ToUpper() =="EDIT")
				{
					#region Edit
					txtContractID.Text = dtgList.Items[e.Item.ItemIndex].Cells[0].Text.Trim();					
					btnSave.Enabled =  (((CheckBox)dtgList.Items[e.Item.ItemIndex].FindControl("chkSelect")).Enabled);
					DataRow iRow = clsHRContract.GetDataByID(txtContractID.Text.Trim());
					if(iRow != null)
					{
						cboLSContractTypeID.SelectedValue  = iRow["LSContractTypeID"].ToString().Trim();
						txtContractNo.Text  = iRow["ContractNo"].ToString().Trim();
						txtEffectiveDate.Text  = iRow["EffectiveDate"].ToString().Trim();
						txtToDate.Text = iRow["ToDate"].ToString().Trim();
						txtProbationFrom.Text = iRow["ProbationFrom"].ToString().Trim();
						txtProbationTo.Text = iRow["ProbationTo"].ToString().Trim();
						txtSignDate.Text  = iRow["SignDate"].ToString().Trim();
						txtSigner.Text  = iRow["Signer"].ToString().Trim();
						txtSignerPosition.Text  = iRow["SignerPosition"].ToString().Trim();
						txtNote.Text=iRow["Note"].ToString().Trim();
						txtDuration.Text=iRow["Duration"].ToString().Trim();
						txtOverHead.Text=iRow["OverHead"].ToString().Trim();
						txtFringeBenefit.Text=iRow["FringeBenefit"].ToString().Trim();
						txtHousingBenefit.Text=iRow["HouseBenefit"].ToString().Trim();
						cboLSSupplierID.SelectedValue=iRow["LSSupplierID"].ToString().Trim();
						cboLSCurrencyTypeID.SelectedValue=iRow["LSCurrencyTypeID"].ToString().Trim();
						chkPaySalary.Checked=iRow["PaySalary"].ToString().Trim()=="True"?true:false;
						if (iRow["AttachFile"].ToString().Trim()!="")
						{
							hpSelectFile.Visible=true;
							btnDeleteFile.Visible=true;
							hpSelectFile.Text=iRow["AttachFile"].ToString().Trim();
							hpSelectFile.NavigateUrl= ConfigurationSettings.AppSettings["pstrGetContractFolder"].Trim() + iRow["AttachFile"].ToString().Trim();							
						}
						else
						{
							hpSelectFile.Visible=false;	
							btnDeleteFile.Visible=false;
							hpSelectFile.Text="";
							hpSelectFile.NavigateUrl="";							
						}

						
						lblPosition.Text  = iRow["Position"].ToString().Trim();
						lblJobTitle.Text  = iRow["JobTitle"].ToString().Trim();
						lblLocation.Text  = iRow["Location"].ToString().Trim();						
						lblGradeOfSalData.Text  = iRow["GradeOfSalData"].ToString().Trim();
						lblRankOfSalData.Text  = iRow["RankOfSalData"].ToString().Trim();
						lblSalaryCoefData.Text  = iRow["SalaryCoefData"].ToString().Trim();
						txtContractSalary.Text  = iRow["Salary"].ToString().Trim();
						lblSalaryOtherData.Text  = iRow["SalaryOtherData"].ToString().Trim();
					}
					Session["ssActivityFlag"] = "Edit";
					#endregion
				}
				else if(e.CommandName.Trim().ToUpper() =="CMDIN1")
				{
					txtContractID.Text = dtgList.Items[e.Item.ItemIndex].Cells[0].Text.Trim();
					cmdIn_Click(txtContractID.Text, "1");
				}
				else if(e.CommandName.Trim().ToUpper() =="CMDIN5")
				{
					txtContractID.Text = dtgList.Items[e.Item.ItemIndex].Cells[0].Text.Trim();
					cmdIn_Click(txtContractID.Text, "5");
				}
				else if(e.CommandName.Trim().ToUpper() =="CMDIN5PL")
				{
					txtContractID.Text = dtgList.Items[e.Item.ItemIndex].Cells[0].Text.Trim();
					cmdIn_Click(txtContractID.Text, "PL5");
				}
				
			}
			catch(Exception ex)
			{
				//lblErr.Text = ex.Message;
				clsChangeLang.popupWindowExp(this.Parent,ex);
			}
		}
		private void cmdIn_Click(string ContractID, string sType)
		{
			string strFileTemplate = "";
			string strDate = "";
			//string strImage = ConfigurationSettings.AppSettings["pStrimageReport"].Trim();
			//string strImage = "http://localhost/iHRP_SCR/Upload/TemplateReport/Image/image001.jpg";
			DataTable dt = clsCommon.GetDataTable("HR_sprptContract @ContractID = '" + txtContractID.Text + "'");
			if (sType == "1")
				strFileTemplate = "HR_sprptContract1.htm";
			else if (sType == "5")
				strFileTemplate = "HR_sprptContract.htm";
			else if (sType == "PL5")
				strFileTemplate = "HR_sprptContract5_phuluc.htm";
			strFileTemplate = "Upload/TemplateReport/" + strFileTemplate;
			try 
			{
				//Phan khai bao se dung Tool bao cao Excel
				FPTToolWeb.Reports.v10.clsBaocao bc = new FPTToolWeb.Reports.v10.clsBaocao();
				#region Config Basic
				bc.sfileTemplate = strFileTemplate;

				#endregion
				string sReport = bc.strReportPageDoc(dt);
				ExcelExporter myExcelXport=new ExcelExporter(this.Page);
				myExcelXport.ExportHTMLTo(sReport,"doc");
				myExcelXport = null;
				
			}
			catch(Exception ex)
			{										
				lblErr.Text = ex.Message;
			}
			finally
			{	
				dt.Dispose();
			}
		}

		private void btnAddNew_Click(object sender, System.EventArgs e)
		{
			clsCommon.ClearControlValue(this);
			hpSelectFile.Text = "";
			btnDeleteFile.Visible = false;
			Session["ssActivityFlag"] = "AddNew";
		}

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
						DeleteAttachFile(dtgList.Items[i].Cells[1].Text.Trim());
					}					
				}
				clsCommon.DeleteListRecord("HR_spfrmContract","ContractID",SqlDbType.NVarChar ,12,strID);
				LoadDataGrid();
			}
			catch(Exception ex)
			{
				//lblErr.Text = ex.Message;
				clsChangeLang.popupWindowExp(this.Parent,ex);
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
				//string str = ex.Message;
				clsChangeLang.popupWindowExp(this.Parent,ex);
			}
		}

		private void btnList_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(Request.ApplicationPath.Trim() + "/Editpage.aspx?ModuleID=HR&ParentID=8&FunctionID=14&Ascx=MdlHR/EmpList.ascx");
		}

		private void btnExport_Click(object sender, System.EventArgs e)
		{
			iHRPCore.DataGridExcelExporter myExcelXport=new iHRPCore.DataGridExcelExporter(dtgList);
			myExcelXport.Export("");
			myExcelXport = null;
		}

		private void btnDeleteFile_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (!hpSelectFile.Text.Equals(""))
				{
					DeleteAttachFile(hpSelectFile.Text.Trim());
					DataTable dtTemp = new DataTable();
					dtTemp=clsCommon.GetDataTable("HR_spfrmContract @Activity='DeleteFileAttach',@EmpID='" + Session["EmpID"] + "',@AttachFile='" + hpSelectFile.Text + "'");
					dtTemp.Dispose();
					hpSelectFile.Text="";
					hpSelectFile.Visible=false;
					btnDeleteFile.Visible=false;
				}
			}
			catch(Exception exp)
			{
				//this.lblErr.Text=exp.Message;
				clsChangeLang.popupWindowExp(this.Parent,exp);
				return ;
			}
		}
	
		private void DeleteAttachFile(string strAttachFile)
		{
			try
			{
				System.IO.File.Delete(Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrContractFolder"].Trim() + strAttachFile) ;									
			}
			catch(Exception exp)
			{
				//this.lblErr.Text=exp.Message;
				clsChangeLang.popupWindowExp(this.Parent,exp);
				return ;
			}
		}

		private string import_file()
		{
			if (txtFile.Value == "" ) return "";
			//---copy file to server						
			string strFiletmp="";			

			if( txtFile.Value!="")				
			{
				strFiletmp = Path.GetExtension(this.txtFile.Value).Trim();
				if(strFiletmp != ".xls" && strFiletmp!= ".doc" )
				{
					//lblErr.Text=clsChangeLang.getStringAlert("0025",strLanguage);
					clsChangeLang.popupWindow(this.Parent,"0025",strLanguage,"",0);
					return "";
				}
			}
			try 
			{	
				strFiletmp = Session["EmpID"]+ "_"+ txtEffectiveDate.Text.Replace("/","") + "_" + "C" + Path.GetExtension(this.txtFile.Value).Trim();
				//strFiletmp = Path.GetFileName(this.txtFile.Value).Trim();
				if (System.IO.File.Exists(Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrContractFolder"].Trim() + strFiletmp))
					System.IO.File.SetAttributes(Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrContractFolder"].Trim() + strFiletmp, System.IO.FileAttributes.Normal) ;
				this.txtFile.PostedFile.SaveAs(Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrContractFolder"].Trim() + strFiletmp);
				System.IO.File.SetAttributes(Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrContractFolder"].Trim() + strFiletmp, System.IO.FileAttributes.Normal) ;
				return strFiletmp;							
			}
			catch (Exception exp)
			{				
				//lblErr.Text=clsChangeLang.getStringAlert("0028",strLanguage);
				clsChangeLang.popupWindow(this.Parent,"0028",strLanguage,"",0);
				return "E.f";
			}
			//-- end copy file
		}

	}
}
