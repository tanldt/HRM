namespace iHRPCore.MdlPR
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.Com;
	using iHRPCore.PRComponent;
	/// <summary>
	///		Summary description for Maternity.
	/// </summary>
	public class Maternity : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label EmpID;
		protected System.Web.UI.WebControls.TextBox txtEmpID;
		protected System.Web.UI.WebControls.Label lblNote;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.TextBox txtPRMonth;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.TextBox txtMonths;
		protected System.Web.UI.HtmlControls.HtmlTableRow trGrid;
		protected System.Web.UI.WebControls.LinkButton btnAddNew;
		protected System.Web.UI.WebControls.DataGrid grdMaternity;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtMaternityID;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnReportTo;
		public string strLanguage = "EN";
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
			if(!Page.IsPostBack)
			{				
				LoadDataGrid();
				Session["ssStatusMaternity"] = "AddNew";
				if(Session["ssMaternityID"]!=null)
				{
					txtEmpID.Text = Session["ssMaternityID"].ToString().Trim();
					Session["ssMaternityID"]= null;
				}
			}
			btnSave.Attributes.Add("OnClick", " return validform()");
			btnDelete.Attributes.Add("OnClick", "return checkdelete()");			
		}
		/// <summary>
		/// Load all Allowance of Employee
		/// </summary>
		private void LoadDataGrid()
		{
			DataTable dtList = new DataTable();
			try
			{
				dtList = clsPRMaternity.GetDataMaternity();
				grdMaternity.DataSource = dtList;
				grdMaternity.CurrentPageIndex = 0;
				grdMaternity.DataBind();				
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
            this.grdMaternity.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.grdMaternity_ItemCommand);
            this.grdMaternity.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.grdMaternity_PageIndexChanged);
            this.btnReportTo.ServerClick += new System.EventHandler(this.btnReportTo_ServerClick);
            this.Load += new System.EventHandler(this.Page_Load);

        }
		#endregion

		private void btnAddNew_Click(object sender, System.EventArgs e)
		{
			clsCommon.ClearControlValue(this);
			txtEmpID.Text = "";
			Session["ssStatusMaternity"] = "AddNew";
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(Session["ssStatusMaternity"].ToString().Trim()=="AddNew")
					clsCommon.ImpactDB("Save",this,"PR_spfrmMATERNITY");
				else
					clsCommon.UpdateByKey("ID",txtMaternityID.Value.Trim(),"Update",this,"PR_spfrmMATERNITY");
				btnAddNew_Click(null,null);
				LoadDataGrid();
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}
		private void grdMaternity_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				if(e.CommandName.Trim().ToUpper() =="EDIT")
				{
					LoadDataToEdit(grdMaternity.Items[e.Item.ItemIndex].Cells[0].Text.Trim());					
				}				
			}	
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}
		//LOAD RECORD DATA TO EDIT
		private void LoadDataToEdit(string strID)
		{
			txtMaternityID.Value = strID;
			DataRow iRow = clsPRMaternity.GetDataByID(strID);
			if(iRow != null)
			{
				txtEmpID.Text = iRow["EmpID"].ToString().Trim();
				txtMonths.Text = iRow["Months"].ToString().Trim();
				txtPRMonth.Text = iRow["PRMonth"].ToString().Trim();
			}
			Session["ssStatusMaternity"] = "Edit";			
		}
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				string strID="";
				for(int i=0;i<grdMaternity.Items.Count;i++)
				{
					if(((CheckBox)grdMaternity.Items[i].FindControl("chkSelect")).Checked==true)
					{
						strID += grdMaternity.Items[i].Cells[0].Text.Trim() + "$";
					}
				}
				clsCommon.DeleteListRecord("PR_spfrmMATERNITY","ID",SqlDbType.Int,4,strID);				
				LoadDataGrid();
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}

		private void grdMaternity_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			try
			{
				LoadDataGrid();
				grdMaternity.CurrentPageIndex=e.NewPageIndex;
				grdMaternity.DataBind();
			}
			catch(Exception ex)
			{
				string str = ex.Message;
			}
		}

		private void btnExport_Click(object sender, System.EventArgs e)
		{
			iHRPCore.DataGridExcelExporter myExcelXport=new DataGridExcelExporter(grdMaternity);			
			myExcelXport.Export("");
			myExcelXport =null;
		}

		private void btnReportTo_ServerClick(object sender, System.EventArgs e)
		{
			Response.Redirect("Editpage.aspx?ModuleID=PR&ParentID=70&FunctionID=450&Ascx=MdlPR/Maternitylist.ascx");
		}

	}
}
