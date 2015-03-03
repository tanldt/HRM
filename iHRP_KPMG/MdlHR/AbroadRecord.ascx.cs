namespace iHRPCore.MdlHR
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.Com;
	using iHRPCore.HRComponent ;

	/// <summary>
	///		Summary description for AbroadRecord.
	/// </summary>
	public class AbroadRecord : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.TextBox txtToDate;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.DropDownList cboLSNationalityID;
		protected System.Web.UI.WebControls.TextBox txtReason;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.TextBox txtNote;
		protected System.Web.UI.WebControls.DataGrid dtgList;
		protected System.Web.UI.WebControls.TextBox txtFromDate;
		protected System.Web.UI.WebControls.LinkButton btnClose;
		protected System.Web.UI.WebControls.LinkButton btnAddNew;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.TextBox txtAbroadRecordID;
		protected System.Web.UI.WebControls.TextBox txtEmpID;
		public string strLanguage = "EN";


		private void Page_Load(object sender, System.EventArgs e)
		{	
			///---Change language---------						
			string str_FrmName = Request.Params["Ascx"];
			if(Session["LangID"] != null)
				if(Session["LangID"].ToString().Trim() == "EN")	clsChangeLang.ChangePage(str_FrmName, this.Page, "EN");
				else clsChangeLang.ChangePage(str_FrmName, this.Page, "VN");				
			///---End Change language---------	
			
			Session["CurFunction"] = Request.Params["FunctionID"].Trim();
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";	
			this.lblErr.Text = "";

			if(!Page.IsPostBack)
			{
				this.LoadDataCombo();
				this.LoadDataGrid();				
				Session["ssActivityFlag"] = "AddNew";
			}

			btnSave.Attributes.Add("OnClick", " return validform()");
			btnDelete.Attributes.Add("OnClick", "return checkdelete()");
			btnClose.Attributes.Add("OnClick","return CloseWindow()");
		}

		/// <summary>
		/// Get data for combo
		/// </summary>
		private void LoadDataCombo()
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			clsCommon.LoadDropDownListControl(cboLSNationalityID,"sp_GetDataCombo @TableName='LS_tblNationality',@Fields='LSNationalityID," + strTextField + " as Name'","LSNationalityID","Name",true);			
		}
		
		private void btnSave_Click(object sender, System.EventArgs e)
		{			
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			try
			{
				if(Session["ssActivityFlag"].ToString().Trim()=="AddNew")
				{					
					clsCommon.ImpactDB(Session["EmpID"].ToString().Trim(),"AddNew",this,"HR_spfrmAbroadRecord");
				}
				else
				{
					clsCommon.ImpactDB(Session["EmpID"].ToString().Trim(),"Update",this,"HR_spfrmAbroadRecord");
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
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			this.dtgList.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgList_ItemCommand);
			this.dtgList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgList_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		
	
		private void LoadDataGrid()
		{
			DataTable dtList = new DataTable();
			try
			{				
				dtList = clsHRAbroadRecord.GetDataByEmpID(Session["EmpID"]);
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
				clsCommon.DeleteListRecord("HR_spfrmAbroadRecord","AbroadRecordID",SqlDbType.NVarChar ,12,strID);
				LoadDataGrid();
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}

		private void btnExport_Click(object sender, System.EventArgs e)
		{
			iHRPCore.DataGridExcelExporter myExcelXport=new iHRPCore.DataGridExcelExporter(dtgList);
			myExcelXport.Export("");
			myExcelXport = null;
		}

		private void dtgList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
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

		private void dtgList_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			string strTextField1 = strLanguage == "VN"?"VNName":"Name";
			try
			{
				if(e.CommandName.Trim().ToUpper() =="EDIT")
				{
					txtAbroadRecordID.Text = dtgList.Items[e.Item.ItemIndex].Cells[0].Text.Trim();
					DataRow iRow = clsHRAbroadRecord.GetDataByID(txtAbroadRecordID.Text.Trim());
					if(iRow != null)
					{
						txtFromDate.Text = iRow["FromDate"].ToString().Trim();
						txtToDate.Text = iRow["ToDate"].ToString().Trim();
						cboLSNationalityID.SelectedValue = iRow["LSNationalityID"].ToString().Trim();
						txtReason.Text = iRow["Reason"].ToString().Trim();
						txtNote.Text = iRow["Note"].ToString().Trim();						
					}
				}
				Session["ssActivityFlag"] = "Edit";
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			
		}
	}
}
