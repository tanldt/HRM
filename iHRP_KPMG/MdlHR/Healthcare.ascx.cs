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

	/// <summary>
	///		Summary description for Healthcare.
	/// </summary>
	public class Healthcare : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.TextBox txtHeight;
		protected System.Web.UI.WebControls.Label lblWeight;
		protected System.Web.UI.WebControls.TextBox txtWeight;
		protected System.Web.UI.WebControls.Label lblHealth;
		protected System.Web.UI.WebControls.TextBox txtHealth;
		protected System.Web.UI.WebControls.Label lblResultby;
		protected System.Web.UI.WebControls.CheckBox chkSpecial;
		protected System.Web.UI.WebControls.Label lblNote;
		protected System.Web.UI.WebControls.TextBox txtNote;
		protected System.Web.UI.WebControls.CheckBox chkShowGrid;
		protected System.Web.UI.WebControls.LinkButton btnAddNew;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnList;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.DataGrid dtgList;
		protected System.Web.UI.WebControls.Label lblToDate;
		protected System.Web.UI.WebControls.TextBox txtToDate;
		protected System.Web.UI.WebControls.Label lblReason;
		protected System.Web.UI.WebControls.TextBox txtGrade;
		protected System.Web.UI.WebControls.Label lblHeight;
		protected System.Web.UI.WebControls.Label lblOndate;
		protected System.Web.UI.WebControls.TextBox txtOnDate;
		protected System.Web.UI.WebControls.RadioButtonList optStatus;
		protected System.Web.UI.WebControls.TextBox txtResultBy;
		protected System.Web.UI.WebControls.Label lblHospital;
		protected System.Web.UI.WebControls.Label lblGrade;
		protected System.Web.UI.WebControls.DropDownList cboLSHospitalID;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdGrid;
		protected System.Web.UI.WebControls.TextBox txtHealthCareRecordID;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.DropDownList cboLSBloodTypeID;
		public string strLanguage = "EN";


		private void Page_Load(object sender, System.EventArgs e)
		{
			Session["CurFunction"] = Request.Params["FunctionID"].Trim();
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";	
			this.lblErr.Text = "";

			if(!Page.IsPostBack)
			{
				LoadDataCombo();
				LoadDataGrid();
				Session["ssActivityFlag"] = "AddNew";
			}
			btnSave.Attributes.Add("OnClick", " return validform()");
			btnDelete.Attributes.Add("OnClick", "return checkdelete()");
		}
		

		/// <summary>
		/// Get data for combo
		/// </summary>
		private void LoadDataCombo()
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			clsCommon.LoadDropDownListControl(cboLSHospitalID,"sp_GetDataCombo @TableName='LS_tblHospital',@Fields='LSHospitalID," + strTextField + " as Name'","LSHospitalID","Name",true);			
		}


		/// <summary>
		/// Load all docuemnt records of Employee
		/// </summary>
		private void LoadDataGrid()
		{
			DataTable dtList = new DataTable();
			try
			{				
				dtList = clsHRHealthCare.GetDataByEmpID(Session["EmpID"],strLanguage);
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
					if (clsHRHealthCare.CheckExists(Session["EmpID"], txtOnDate.Text.Trim(), "CheckExistsAddNew", txtHealthCareRecordID.Text.Trim()))
					{								
						clsCommon.ImpactDB(Session["EmpID"].ToString().Trim(),"AddNew",this,"HR_spfrmHealthcareRecord");										
					}
					else
					{
						lblErr.Text=clsChangeLang.getStringAlert("0024",strLanguage);
						return;
					}
				}
				else
				{
					if ( clsHRHealthCare.CheckExists(Session["EmpID"], txtOnDate.Text.Trim(),"CheckExistsUpdate", txtHealthCareRecordID.Text.Trim()))
					{				
						clsCommon.ImpactDB(Session["EmpID"].ToString().Trim(),"Update",this,"HR_spfrmHealthcareRecord");
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
		}

			
		private void dtgList_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string strTextField1 = strLanguage == "VN"?"VNName":"Name";
			try
			{
				if(e.CommandName.Trim().ToUpper() =="EDIT")
				{
					txtHealthCareRecordID.Text = dtgList.Items[e.Item.ItemIndex].Cells[0].Text.Trim();
					DataRow iRow = clsHRHealthCare.GetDataByID(txtHealthCareRecordID.Text.Trim());
					if(iRow != null)
					{
						txtOnDate.Text  = iRow["OnDate"].ToString().Trim();
						optStatus.SelectedValue = iRow["Status"].ToString().Trim();
						txtHeight.Text  = iRow["Height"].ToString().Trim();
						txtWeight.Text  = iRow["Weight"].ToString().Trim();
						cboLSHospitalID.SelectedValue  = iRow["LSHospitalID"].ToString().Trim();
						txtHealth.Text  = iRow["Health"].ToString().Trim();
						txtResultBy.Text  = iRow["ResultBy"].ToString().Trim();
						txtGrade.Text  = iRow["Grade"].ToString().Trim();
						chkSpecial.Checked = iRow["Special"].ToString()=="True"?true:false;
						txtNote.Text = iRow["Note"].ToString().Trim();		
						cboLSBloodTypeID.SelectedValue=iRow["LSBloodTypeID"].ToString().Trim();		
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
				clsCommon.DeleteListRecord("HR_spfrmHealthcareRecord","HealthcareRecordID",SqlDbType.NVarChar ,12,strID);
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
			/*iHRPCore.DataGridExcelExporter myExcelXport=new iHRPCore.DataGridExcelExporter(dtgList);
			myExcelXport.Export("");
			myExcelXport = null;
			*/
			dtgList.AllowPaging = false;
			this.LoadDataGrid();
			iHRPCore.DataGridExcelExporter myExcelXport=new DataGridExcelExporter(dtgList);
			myExcelXport.Export("");
			myExcelXport = null;
			this.dtgList.AllowPaging = true;
			this.LoadDataGrid();
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
	}
}
