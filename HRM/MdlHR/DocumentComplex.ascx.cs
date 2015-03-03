namespace iHRPCore.MdlHR
{
	using System;
	using System.Data;
	using System.IO;
	using System.Configuration;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.Com;
	using iHRPCore.HRComponent;

	/// <summary>
	///		Summary description for DocumentComplex.
	/// </summary>
	public class DocumentComplex : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.DataGrid dtgList;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.LinkButton btnList;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnAddnew;
		protected System.Web.UI.WebControls.TextBox txtNote;
		protected System.Web.UI.WebControls.Label lblNote;
		protected System.Web.UI.WebControls.TextBox txtStorePlace;
		protected System.Web.UI.WebControls.Label lblStorePlace;
		protected System.Web.UI.WebControls.TextBox txtSubmitDate;
		protected System.Web.UI.WebControls.Label lblSubmitDate;
		protected System.Web.UI.WebControls.TextBox txtContent;
		protected System.Web.UI.WebControls.Label lblContent;
		protected System.Web.UI.WebControls.TextBox txtSignDate;
		protected System.Web.UI.WebControls.Label lblSignDate;
		protected System.Web.UI.WebControls.TextBox txtDocumentNo;
		protected System.Web.UI.WebControls.Label lblDecisionNumber;
		protected System.Web.UI.WebControls.TextBox txtEstablishPlace;
		protected System.Web.UI.WebControls.Label lblEstablishPlace;
		protected System.Web.UI.WebControls.DropDownList cboLSDocumentID;
		protected System.Web.UI.WebControls.Label lblDocumentType;
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdToStamplbl;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdAdToStamptxt;
		protected System.Web.UI.WebControls.TextBox txtEmpDocumentID;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.HtmlControls.HtmlInputFile txtFile;
		protected System.Web.UI.WebControls.LinkButton btnDeleteFile;
		protected System.Web.UI.WebControls.HyperLink hpSelectFile;
		protected System.Web.UI.WebControls.TextBox txtAttachFile;
		public string strLanguage = "VN";

		private void Page_Load(object sender, System.EventArgs e)
		{
			Session["CurFunction"] = Request.Params["FunctionID"].Trim();
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"VN";	
			this.lblErr.Text = "";

			if(!Page.IsPostBack)
			{
				this.LoadDataCombo();
				LoadDataGrid();
				Session["ssActivityFlag"] = "AddNew";
			}
			btnSave.Attributes.Add("OnClick", " return validform()");
			btnDelete.Attributes.Add("OnClick", "return checkdelete()");
			btnDeleteFile.Attributes.Add("OnClick", "return confirmDeleteFile()");
		}


		/// <summary>
		/// Get data for combo
		/// </summary>
		private void LoadDataCombo()
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			clsCommon.LoadDropDownListControl(cboLSDocumentID,"sp_GetDataCombo @TableName='LS_tblDocument',@Fields='LSDocumentID," + strTextField + " as Name'","LSDocumentID","Name",true);			
		}


		/// <summary>
		/// Load all docuemnt records of Employee
		/// </summary>
		private void LoadDataGrid()
		{
			DataTable dtList = new DataTable();
			try
			{				
				dtList = clsHREmpDocument.GetDataByEmpID(Session["EmpID"]);
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

				
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			try
			{
				
				if(Session["ssActivityFlag"].ToString().Trim()=="AddNew")
				{	
					if(txtFile.Value!="")
					{
						//vuonglm
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
						txtAttachFile.Text=strAttachFile;
						//vuonglm
					}					
					if ( clsHREmpDocument.CheckExists(Session["EmpID"], cboLSDocumentID.SelectedValue.Trim(), "CheckExistsAddNew", txtEmpDocumentID.Text.Trim()))
					{
						clsCommon.ImpactDB(Session["EmpID"].ToString().Trim(),"AddNewComplex",this,"HR_spfrmEmpDocument");
					}
					else
					{
						lblErr.Text=clsChangeLang.getStringAlert("0024",strLanguage);
						return;
					}
				}
				else
				{
					if(txtFile.Value!="")
					{
						//vuonglm
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
						txtAttachFile.Text=strAttachFile;
						//vuonglm
					}
					if ( clsHREmpDocument.CheckExists(Session["EmpID"], cboLSDocumentID.SelectedValue.Trim(), "CheckExistsUpdate", txtEmpDocumentID.Text.Trim()))
					{
						clsCommon.ImpactDB(Session["EmpID"].ToString().Trim(),"UpdateComplex",this,"HR_spfrmEmpDocument");
					}
					else
					{
						lblErr.Text=clsChangeLang.getStringAlert("0024",strLanguage);
						return;
					}
				}

				clsCommon.ClearControlValue(this);
				Session["ssActivityFlag"] = "AddNew";
				LoadDataGrid();
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
			hpSelectFile.Visible=false;
			btnDeleteFile.Visible=false;
		}

			
		private void dtgList_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string strTextField1 = strLanguage == "VN"?"VNName":"Name";
			try
			{
				if(e.CommandName.Trim().ToUpper() =="EDIT")
				{
					txtEmpDocumentID.Text = dtgList.Items[e.Item.ItemIndex].Cells[0].Text.Trim();
					DataRow iRow = clsHREmpDocument.GetDataByID(txtEmpDocumentID.Text.Trim());
					if(iRow != null)
					{
						cboLSDocumentID.SelectedValue = iRow["LSDocumentID"].ToString().Trim();
						txtEstablishPlace.Text =  iRow["EstablishPlace"].ToString().Trim();
						txtDocumentNo.Text =  iRow["DocumentNo"].ToString().Trim();
						txtSignDate.Text =  iRow["SignDate"].ToString().Trim();
						txtContent.Text =  iRow["Content"].ToString().Trim();
						txtSubmitDate.Text = iRow["SubmitDate"].ToString().Trim();
						txtStorePlace.Text =  iRow["StorePlace"].ToString().Trim();						
						txtNote.Text = iRow["Note"].ToString().Trim();	
						//vuonglm
							hpSelectFile.Text=iRow["AttachFile"].ToString().Trim();	
						if(hpSelectFile.Text!="")
						{
							hpSelectFile.Visible=true;
							btnDeleteFile.Visible=true;
							hpSelectFile.NavigateUrl= ConfigurationSettings.AppSettings["pstrGetFileFolder"].Trim() + iRow["AttachFile"].ToString().Trim();							
						}
						//vuonglm
					}
				}
				Session["ssActivityFlag"] = "Edit";
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}

		private void btnAddNew_Click(object sender, System.EventArgs e)
		{
			clsCommon.ClearControlValue(this);
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
					}
				}
				clsCommon.DeleteListRecord("HR_spfrmEmpDocument","EmpDocumentID",SqlDbType.NVarChar ,12,strID);
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
			this.btnAddnew.Click += new System.EventHandler(this.btnAddNew_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnList.Click += new System.EventHandler(this.btnList_Click);
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			this.dtgList.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgList_ItemCommand);
			this.dtgList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgList_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnList_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(Request.ApplicationPath.Trim() + "/Editpage.aspx?ModuleID=HR&ParentID=8&FunctionID=14&Ascx=MdlHR/EmpList.ascx");
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
			/*iHRPCore.DataGridExcelExporter myExcelXport=new iHRPCore.DataGridExcelExporter(dtgList);
			myExcelXport.Export("");
			myExcelXport = null;
			*/
		}
		//vuonglm
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
		private string import_file()
		{
			//---copy file to server			
			string mext = "";
			if( txtFile.Value!="")				
			{
				mext = this.txtFile.Value.Substring(this.txtFile.Value.LastIndexOf("."));
				if(mext != ".xls" && mext!= ".doc" )
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
					strFiletmp = Session["EmpID"]+ "_"+ Path.GetExtension(this.txtFile.Value).Trim();
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
		//vuonglm
	}
}
