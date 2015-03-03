namespace iHRPCore.MdlHR
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.IO;
	using System.Configuration;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.HRComponent;
	using iHRPCore.Com;

	/// <summary>
	///		Summary description for Discipline.
	/// </summary>
	public class Discipline : System.Web.UI.UserControl
	{
		protected FPTToolWeb.Control.DataGrids.FPTDataGrid dtgList;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.LinkButton btnList;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnAddNew;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.Label Label22;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label23;
		protected System.Web.UI.WebControls.CheckBox ChkDecision;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.TextBox txtEffectiveDate;
		protected System.Web.UI.WebControls.TextBox txtSignDate;
		protected System.Web.UI.WebControls.TextBox txtDecisionNo;
		protected System.Web.UI.WebControls.TextBox txtSigner;
		protected System.Web.UI.WebControls.TextBox txtNote;
		protected System.Web.UI.WebControls.RadioButton optReward;
		protected System.Web.UI.WebControls.RadioButton optDiscipline;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.DropDownList cboLSAwardMethodID;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.DropDownList cboLSAwardLevelID;
		protected System.Web.UI.HtmlControls.HtmlTableRow trDiscipline;
		protected System.Web.UI.HtmlControls.HtmlTableRow trAward;
		protected System.Web.UI.WebControls.DropDownList cboLSDisciplineMethodID;
		protected System.Web.UI.WebControls.DropDownList cboLSDisciplineLevelID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtAwardRecordID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtDisciplineRecordID;
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.TextBox txtReason;
		protected System.Web.UI.WebControls.CheckBox chkShowGrid;
		protected System.Web.UI.HtmlControls.HtmlTableRow tdGrid;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.TextBox txtDescription;
		public int iID;
		protected System.Web.UI.WebControls.TextBox txtSignerPosition;
		protected System.Web.UI.HtmlControls.HtmlInputFile txtFile;
		protected System.Web.UI.WebControls.TextBox txtAttachFile;
		protected System.Web.UI.HtmlControls.HtmlTableRow trDecision1;
		protected System.Web.UI.HtmlControls.HtmlTableRow trDecision2;
		protected System.Web.UI.WebControls.HyperLink hpSelectFile;
		protected System.Web.UI.WebControls.LinkButton btnDeleteFile;
		protected System.Web.UI.HtmlControls.HtmlTableRow trDecision3;
		protected System.Web.UI.HtmlControls.HtmlTableRow trDecision4;
		public string strLanguage = "EN";

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if (Request.Params["ID"] != null)
				iID = int.Parse(Request.Params["ID"].ToString().Trim());
			else
				iID = 1;
			Session["CurFunction"] = Request.Params["FunctionID"].Trim();
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";			
			if(!Page.IsPostBack)
			{
				LoadCombo();
				LoadDataGrid();
				ShowControl(iID);
				Session["ssStatusAddnew"]="AddNew";
			}
			optDiscipline.Attributes.Add("OnClick","return checkOpt()");
			optReward.Attributes.Add("OnClick","return checkOpt()");
			btnSave.Attributes.Add("OnClick", " return validform()");
			btnDelete.Attributes.Add("OnClick", "return checkdelete()");
			btnDeleteFile.Attributes.Add("Onclick","return CheckDeleteFile()");
		}
		private void LoadDataGrid()
		{
			DataTable dtList = new DataTable();
			try
			{				
				dtList = clsHRDiscipline.GetDataByEmpID(Session["EmpID"],strLanguage);
				dtgList.DataSource = dtList;
				dtgList.CurrentPageIndex = 0;
				dtgList.DataBind();				
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
		private void LoadCombo()
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			clsCommon.LoadDropDownListControl(cboLSAwardMethodID,"sp_GetDataCombo @TableName='LS_tblAwardMethod',@Fields='LSAwardMethodID as [ID]," + strTextField + " as Name'","ID","Name",true);
			clsCommon.LoadDropDownListControl(cboLSAwardLevelID,"sp_GetDataCombo @TableName='LS_tblAwardLevel',@Fields='LSAwardLevelID as [ID]," + strTextField + " as Name'","ID","Name",true);
			clsCommon.LoadDropDownListControl(cboLSDisciplineLevelID,"sp_GetDataCombo @TableName='LS_tblDisciplineLevel',@Fields='LSDisciplineLevelID as [ID]," + strTextField + " as Name'","ID","Name",true);
			clsCommon.LoadDropDownListControl(cboLSDisciplineMethodID,"sp_GetDataCombo @TableName='LS_tblDisciplineMethod',@Fields='LSDisciplineMethodID as [ID]," + strTextField + " as Name'","ID","Name",true);			
			
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
			this.optReward.CheckedChanged += new System.EventHandler(this.optReward_CheckedChanged);
			this.optDiscipline.CheckedChanged += new System.EventHandler(this.optDiscipline_CheckedChanged);
			this.btnDeleteFile.Click += new System.EventHandler(this.btnDeleteFile_Click);
			this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnList.Click += new System.EventHandler(this.btnList_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			this.dtgList.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgList_ItemCommand);
			this.dtgList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgList_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		private void btnSave_Click(object sender, System.EventArgs e)
		{		
			try
			{
				string strAttachFile="";
				
				if(Session["ssStatusAddnew"].ToString().Trim()=="AddNew")
				{
					if (!txtFile.Value.Equals("")) 
					{
						strAttachFile=import_file();						
						if (strAttachFile.Equals("null"))
						{
							txtAttachFile.Text="";
						}
						else if (strAttachFile.Equals(""))
						{
							return;
						}
						else
						{
							txtAttachFile.Text=strAttachFile;
						}
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
						else if (strAttachFile.Equals(""))
						{
							return;
						}
						else
						{
							txtAttachFile.Text=strAttachFile;
						}
					}			
				}
							
				if (optDiscipline.Checked)
				{
					if(Session["ssStatusAddnew"].ToString().Trim()=="AddNew")
						clsCommon.ImpactDB(Session["EmpID"].ToString().Trim(),"Save",this,"HR_spfrmDISCIPLINERECORD");
					else
						clsCommon.UpdateByKey("DisciplineRecordID",txtDisciplineRecordID.Value.Trim(),"Update",this,"HR_spfrmDISCIPLINERECORD");
				}
				else
				{
					if(Session["ssStatusAddnew"].ToString().Trim()=="AddNew")
						clsCommon.ImpactDB(Session["EmpID"].ToString().Trim(),"Save",this,"HR_spfrmAWARDRECORD");
					else
						clsCommon.UpdateByKey("AwardRecordID",txtAwardRecordID.Value.Trim(),"Update",this,"HR_spfrmAWARDRECORD");
				}
				clsCommon.ClearControlValue(this);
				btnAddNew_Click(null,null);
				LoadDataGrid();
			}
			catch(Exception ex)
			{
				clsChangeLang.popupWindowExp(this.Parent,ex);
			}		
		}
		/// <summary>
		/// load info to Edit
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		private void dtgList_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				if(e.CommandName.Trim().ToUpper() =="EDIT")
				{
					DataRow iRow ;
					string[] sArr = dtgList.Items[e.Item.ItemIndex].Cells[0].Text.Trim().Split('@');
					if (sArr.GetValue(1).ToString()=="0")
					{
						txtAwardRecordID.Value=sArr.GetValue(0).ToString().Trim();
						txtDisciplineRecordID.Value="";
						iRow = clsHRDiscipline.GetDataByIDAward(txtAwardRecordID.Value.Trim());
					}
					else
					{
						txtDisciplineRecordID.Value=sArr.GetValue(0).ToString().Trim();					
						txtAwardRecordID.Value="";
						iRow = clsHRDiscipline.GetDataByIDDiscipline(txtDisciplineRecordID.Value.Trim());
					}
					
					if(iRow != null)
					{
						if (txtAwardRecordID.Value!="")
						{
							cboLSAwardLevelID.SelectedValue =  iRow["LSAwardLevelID"].ToString();
							cboLSAwardMethodID.SelectedValue =iRow["LSAwardMethodID"].ToString();
							ShowControl(0);
						}
						else
						{
							cboLSDisciplineLevelID.SelectedValue =  iRow["LSDisciplineLevelID"].ToString();
							cboLSDisciplineMethodID.SelectedValue =iRow["LSDisciplineMethodID"].ToString();						
							ShowControl(1);
						}

						txtEffectiveDate.Text = iRow["EffectiveDate"].ToString();
						txtReason.Text = iRow["Reason"].ToString();
						txtDescription.Text = iRow["Description"].ToString();						
						txtDecisionNo.Text = iRow["Decision"].ToString();
						txtSignDate.Text = iRow["SignDate"].ToString();
						txtDecisionNo.Text = iRow["DecisionNo"].ToString();
						txtSigner.Text = iRow["Signer"].ToString();
						txtSignerPosition.Text = iRow["SignerPosition"].ToString();												
						txtNote.Text = iRow["Note"].ToString();
						if (iRow["Decision"].ToString().Trim()=="True")
						{
							ChkDecision.Checked=true;
							trDecision1.Style.Add("DISPLAY","block");
							trDecision2.Style.Add("DISPLAY","block");
							trDecision3.Style.Add("DISPLAY","block");
							trDecision4.Style.Add("DISPLAY","block");
						}
						else
						{
							ChkDecision.Checked=false;
							trDecision1.Style.Add("DISPLAY","none");
							trDecision2.Style.Add("DISPLAY","none");
							trDecision3.Style.Add("DISPLAY","none");
							trDecision4.Style.Add("DISPLAY","none");
						}
						if (iRow["AttachFile"].ToString().Trim()!="")
						{
							hpSelectFile.Visible=true;
							btnDeleteFile.Visible=true;
							hpSelectFile.Text=iRow["AttachFile"].ToString().Trim();
							hpSelectFile.NavigateUrl= ConfigurationSettings.AppSettings["pstrGetFileAwardUploadFiles"].Trim() + iRow["AttachFile"].ToString().Trim();							
							txtAttachFile.Text=iRow["AttachFile"].ToString().Trim();
						}
						else
						{
							hpSelectFile.Visible=false;	
							btnDeleteFile.Visible=false;
							//hpSelectFile.Text="";
							hpSelectFile.NavigateUrl="";							
						}
						optDiscipline.Enabled=false;
						optReward.Enabled=false;
					}

				}
				Session["ssStatusAddnew"] = "Edit";
				if(chkShowGrid.Checked==true)
					tdGrid.Style.Add("DISPLAY","block");
				else				
					tdGrid.Style.Add("DISPLAY","none");
			}
			catch(Exception ex)
			{
				clsChangeLang.popupWindowExp(this.Parent,ex);
			}
		}
		public void ShowControl(int sType)
		{
			//0<=> Award ; 1<=>Discipline
			if (sType==0)
			{
				optReward.Checked=true;
				optDiscipline.Checked=false;
				trAward.Style.Add("DISPLAY","block");
				trDiscipline.Style.Add("DISPLAY","none");				
			}
			else
			{
				optDiscipline.Checked=true;
				trAward.Style.Add("DISPLAY","none");
				trDiscipline.Style.Add("DISPLAY","block");
			}
		}
		private void btnAddNew_Click(object sender, System.EventArgs e)
		{
			clsCommon.ClearControlValue(this);
			Session["ssStatusAddnew"] = "AddNew";
			if(chkShowGrid.Checked==true)
				tdGrid.Style.Add("DISPLAY","block");
			else				
				tdGrid.Style.Add("DISPLAY","none");
			ChkDecision.Checked=true;
			hpSelectFile.Visible=false;	
			btnDeleteFile.Visible=false;
			optDiscipline.Enabled=true;
			optReward.Enabled=true;
			ShowControl(1);
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
					}
				}
				clsCommon.DeleteListRecord("HR_spfrmAWARDRECORD","ID",SqlDbType.NVarChar,12,strID);
				if(chkShowGrid.Checked==true)
					tdGrid.Style.Add("DISPLAY","block");
				else				
					tdGrid.Style.Add("DISPLAY","none");
				LoadDataGrid();
			}
			catch(Exception ex)
			{
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
			try
			{
				Response.Redirect(Request.ApplicationPath.Trim() + "/Editpage.aspx?ModuleID=HR&ParentID=8&FunctionID=14&Ascx=MdlHR/EmpList.ascx");
			}
			catch(Exception exp ) 
			{
				clsChangeLang.popupWindowExp(this.Parent,exp);
			}
		}

		private void btnExport_Click(object sender, System.EventArgs e)
		{
			dtgList.AllowPaging = false;
			this.LoadDataGrid();
			iHRPCore.DataGridExcelExporter myExcelXport=new DataGridExcelExporter(dtgList);
			myExcelXport.Export("");
			myExcelXport = null;
			this.dtgList.AllowPaging = true;
			this.LoadDataGrid();
			/*iHRPCore.DataGridExcelExporter myExcelXport=new DataGridExcelExporter(dtgList);
			myExcelXport.Export("");
			myExcelXport = null;
			*/
		}

		private void optReward_CheckedChanged(object sender, System.EventArgs e)
		{
			//ShowControl(0);
		}

		private void optDiscipline_CheckedChanged(object sender, System.EventArgs e)
		{
			//ShowControl(1);
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
					strFiletmp = Session["EmpID"]+ "_"+ txtSignDate.Text.Replace("/","") + "_" + "DisArw" + Path.GetExtension(this.txtFile.Value).Trim();
					if (System.IO.File.Exists(Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrAwardUploadFiles"].Trim() + strFiletmp))
						System.IO.File.SetAttributes(Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrAwardUploadFiles"].Trim() + strFiletmp, System.IO.FileAttributes.Normal) ;
					this.txtFile.PostedFile.SaveAs(Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrAwardUploadFiles"].Trim() + strFiletmp);
					System.IO.File.SetAttributes(Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrAwardUploadFiles"].Trim() + strFiletmp, System.IO.FileAttributes.Normal) ;
					return strFiletmp;
				}
				else
				{
					clsChangeLang.popupWindow(this.Parent,"0026",strLanguage,"",0);
					return "null";
				}				
			}
			catch (Exception exp)
			{
				clsChangeLang.popupWindowExp(this.Parent,exp);
				return "";
			}
			//-- end copy file
		}
		private void btnDeleteFile_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (!txtAttachFile.Text.Equals(""))
				{
					System.IO.File.Delete(Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrAwardUploadFiles"].Trim() + txtAttachFile.Text) ;
					string sID;
					if (optDiscipline.Checked)
					{
						sID=txtDisciplineRecordID.Value+"@1";
					}
					else
					{
						sID=txtAwardRecordID.Value+"@0";
					}
					clsHRDiscipline.DeleteFileAttach(sID);
					//hpSelectFile.Text="";
					hpSelectFile.Visible=false;
					btnDeleteFile.Visible=false;
					txtAttachFile.Text="";
				}
			}
			catch(Exception exp)
			{
				clsChangeLang.popupWindowExp(this.Parent,exp);
				return ;
			}
		}
	}
}
