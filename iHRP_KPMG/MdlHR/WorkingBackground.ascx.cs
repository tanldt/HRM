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
	///		Summary description for WorkingBackground.
	/// </summary>
	public class WorkingBackground : System.Web.UI.UserControl
	{
		#region Declare
		public string strLanguage = "VN";
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label lblOtherCompanyID;
		protected System.Web.UI.WebControls.CheckBox chkShowGrid;
		protected System.Web.UI.WebControls.LinkButton btnAddNew;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.LinkButton btnList;
		protected System.Web.UI.WebControls.TextBox txtlblToDate;
		protected System.Web.UI.WebControls.ImageButton cmdCalFromDate;
		protected System.Web.UI.WebControls.ImageButton cmdToDate;
		protected System.Web.UI.WebControls.Label StartSalary;
		protected System.Web.UI.WebControls.Label lbl;
		protected System.Web.UI.WebControls.TextBox txt;
		protected System.Web.UI.WebControls.ImageButton cmdCalToDate;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdGrid;
		protected System.Web.UI.WebControls.TextBox txtFromDate;
		protected System.Web.UI.WebControls.Label lblNote;
		protected System.Web.UI.WebControls.TextBox txtNote;
		protected System.Web.UI.WebControls.TextBox txtStartSalary;
		protected System.Web.UI.WebControls.Label lblFromDate;
		protected System.Web.UI.WebControls.Label lblToDate;
		protected System.Web.UI.WebControls.TextBox txtToDate;
		protected System.Web.UI.WebControls.Label lblWorkFor;
		protected System.Web.UI.WebControls.TextBox txtWorkFor;
		protected System.Web.UI.WebControls.Label lblPosition;
		protected System.Web.UI.WebControls.TextBox txtPosition;
		protected System.Web.UI.WebControls.Label lblAddress;
		protected System.Web.UI.WebControls.TextBox txtAddress;
		protected System.Web.UI.WebControls.Label lblPhone;
		protected System.Web.UI.WebControls.TextBox txtTelephone;
		protected System.Web.UI.WebControls.Label lblStartSalary;
		protected System.Web.UI.WebControls.Label lblLastSalary;
		protected System.Web.UI.WebControls.TextBox txtLastSalary;
		protected System.Web.UI.WebControls.Label lblDuty;
		protected System.Web.UI.WebControls.TextBox txtDuty;
		protected System.Web.UI.WebControls.Label lblChangeReason;
		protected System.Web.UI.WebControls.TextBox txtChangeReason;
		protected System.Web.UI.WebControls.DataGrid grdWorkingBackground;
		protected System.Web.UI.WebControls.TextBox txtWorkingBackgroundID;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtContactName;
		protected System.Web.UI.WebControls.TextBox txtContactPosition;
		protected System.Web.UI.WebControls.CheckBox chkAdContact;
		protected System.Web.UI.HtmlControls.HtmlTableRow trAdContact;
		protected System.Web.UI.WebControls.TextBox txtEmpID;
		protected System.Web.UI.WebControls.TextBox FromDate;
		#endregion Declare

		private void Page_Load(object sender, System.EventArgs e)
		{
			Session["CurFunction"] = Request.Params["FunctionID"].Trim();
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"VN";			
			if(!Page.IsPostBack)
			{				
				LoadDataGrid();
				Session["ssStatusWorkingBG"] = "AddNew";
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
				dtList = clsHRWorkingBackground.GetDataByEmpID(Session["EmpID"]);
				grdWorkingBackground.DataSource = dtList;
				grdWorkingBackground.CurrentPageIndex = 0;
				grdWorkingBackground.DataBind();				
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
			this.btnList.Click += new System.EventHandler(this.btnList_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			this.grdWorkingBackground.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgWorkingBackground_ItemCommand);
			this.grdWorkingBackground.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgWorkingBackground_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSave_Click(object sender, System.EventArgs e)
		{		
			try
			{
				if(Session["ssStatusWorkingBG"].ToString().Trim()=="AddNew")
					//clsCommon.ImpactDB(Session["EmpID"].ToString().Trim(),"Save",this,"HR_spfrmWORKINGBACKGROUND");
				{
					if (Session["EmpID"] != null)
					{
						string strErr =clsCommon.sImpactDB(Session["EmpID"].ToString().Trim(),"Save",this,"HR_spfrmWORKINGBACKGROUND");
						if(strErr == "")
						{
							btnAddNew_Click(null,null);
							LoadDataGrid();
							clsChangeLang.popupWindow(this.Parent,"0044", strLanguage, "", 1);
						}
						else
							clsChangeLang.popupWindow(this.Parent,"0092", strLanguage, "", 0);
					}
				}
				else
					//clsCommon.UpdateByKey("WorkingBackgroundID",txtWorkingBackgroundID.Value.Trim(),"Update",this,"HR_spfrmWORKINGBACKGROUND");
					if (Session["EmpID"] != null)
					{

						string strErr =clsCommon.sImpactDB(Session["EmpID"].ToString().Trim(),"Update",this,"HR_spfrmWORKINGBACKGROUND");
						if(strErr == "")
						{
							btnAddNew_Click(null,null);
							LoadDataGrid();
							clsChangeLang.popupWindow(this.Parent,"0044", strLanguage, "", 1);
						}
						else
							clsChangeLang.popupWindow(this.Parent,"0092", strLanguage, "", 0);
					}
			//	clsCommon.ClearControlValue(this);
			//	btnAddNew_Click(null,null);
		//		LoadDataGrid();
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}		
		}
		/// <summary>
		/// load info to Edit
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		private void dtgWorkingBackground_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				if(e.CommandName.Trim().ToUpper() =="EDIT")
				{
					txtWorkingBackgroundID.Text = grdWorkingBackground.Items[e.Item.ItemIndex].Cells[0].Text.Trim();
					//txtEmpID.Text = grdWorkingBackground.Items[e.Item.ItemIndex].Cells[1].Text.Trim();
					DataRow iRow = clsHRWorkingBackground.GetDataByID(txtWorkingBackgroundID.Text.Trim());
					if(iRow != null)
					{
						txtFromDate.Text = iRow["FromDate"].ToString().Trim();
						txtToDate.Text = iRow["ToDate"].ToString().Trim();
						txtStartSalary.Text = iRow["StartSalary"].ToString().Trim();
						txtLastSalary.Text = iRow["LastSalary"].ToString().Trim();
						txtAddress.Text = iRow["Address"].ToString().Trim();
						txtNote.Text = iRow["Note"].ToString().Trim();
						txtContactName.Text=iRow["ContactName"].ToString().Trim();
						txtContactPosition.Text=iRow["ContactPosition"].ToString().Trim();
						txtChangeReason.Text = iRow["ChangeReason"].ToString().Trim();
						txtDuty.Text = iRow["Duty"].ToString().Trim();
						txtPosition.Text = iRow["Position"].ToString().Trim();
						txtTelephone.Text = iRow["Telephone"].ToString().Trim();
						txtWorkFor.Text = iRow["WorkFor"].ToString().Trim();
						//txtEmpID.Text = iRow["EmpID"].ToString().Trim();						
					}

				}
				Session["ssStatusWorkingBG"] = "Edit";
				if(chkShowGrid.Checked==true)
					tdGrid.Style.Add("DISPLAY","block");
				else				
					tdGrid.Style.Add("DISPLAY","none");
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}

		private void btnAddNew_Click(object sender, System.EventArgs e)
		{
			clsCommon.ClearControlValue(this);
			Session["ssStatusWorkingBG"] = "AddNew";
			if(chkShowGrid.Checked==true)
				tdGrid.Style.Add("DISPLAY","block");
			else				
				tdGrid.Style.Add("DISPLAY","none");
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				string strID="";
				for(int i=0;i<grdWorkingBackground.Items.Count;i++)
				{
					if(((CheckBox)grdWorkingBackground.Items[i].FindControl("chkSelect")).Checked==true)
					{
						strID += grdWorkingBackground.Items[i].Cells[0].Text.Trim() + "$";
					}
				}
				clsCommon.DeleteListRecord("HR_spfrmWORKINGBACKGROUND","WorkingBackgroundID",SqlDbType.NVarChar,12,strID);
				if(chkShowGrid.Checked==true)
					tdGrid.Style.Add("DISPLAY","block");
				else				
					tdGrid.Style.Add("DISPLAY","none");
				LoadDataGrid();
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}

		private void dtgWorkingBackground_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			try
			{
				LoadDataGrid();
				grdWorkingBackground.CurrentPageIndex=e.NewPageIndex;
				grdWorkingBackground.DataBind();
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
			/*iHRPCore.DataGridExcelExporter myExcelXport=new DataGridExcelExporter(grdWorkingBackground);
			myExcelXport.Export("");
			myExcelXport = null;
			*/
			grdWorkingBackground.AllowPaging = false;
			this.LoadDataGrid();
			iHRPCore.DataGridExcelExporter myExcelXport=new DataGridExcelExporter(grdWorkingBackground);
			myExcelXport.Export("");
			myExcelXport = null;
			this.grdWorkingBackground.AllowPaging = true;
			this.LoadDataGrid();
		}
	}
}
