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
	public class ProjectParticipation : System.Web.UI.UserControl
	{
		#region Declare
		public string strLanguage = "EN";
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label lblOtherCompanyID;
		protected System.Web.UI.WebControls.Label lblNote;
		protected System.Web.UI.WebControls.TextBox Textbox2;
		protected System.Web.UI.WebControls.CheckBox chkShowGrid;
		protected System.Web.UI.WebControls.LinkButton btnAddNew;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.LinkButton btnList;
		protected System.Web.UI.WebControls.TextBox Textbox4;
		protected System.Web.UI.WebControls.TextBox Textbox6;
		protected System.Web.UI.WebControls.TextBox Textbox7;
		protected System.Web.UI.WebControls.Label lblFromDate;
		protected System.Web.UI.WebControls.Label lblToDate;
		protected System.Web.UI.WebControls.TextBox txtlblToDate;
		protected System.Web.UI.WebControls.ImageButton cmdCalFromDate;
		protected System.Web.UI.WebControls.ImageButton cmdToDate;
		protected System.Web.UI.WebControls.Label lblPosition;
		protected System.Web.UI.WebControls.TextBox txtWorkFor;
		protected System.Web.UI.WebControls.Label lblWorkFor;
		protected System.Web.UI.WebControls.Label lblAddress;
		protected System.Web.UI.WebControls.TextBox txtAddress;
		protected System.Web.UI.WebControls.Label lblPhone;
		protected System.Web.UI.WebControls.Label StartSalary;
		protected System.Web.UI.WebControls.Label lbl;
		protected System.Web.UI.WebControls.TextBox txtDuty;
		protected System.Web.UI.WebControls.Label lblDuty;
		protected System.Web.UI.WebControls.TextBox txt;
		protected System.Web.UI.WebControls.TextBox txtFromDate;
		protected System.Web.UI.WebControls.TextBox txtToDate;
		protected System.Web.UI.WebControls.ImageButton cmdCalToDate;
		protected System.Web.UI.WebControls.TextBox txtPosition;
		protected System.Web.UI.WebControls.Label lblStartSalary;
		protected System.Web.UI.WebControls.TextBox txtStartSalary;
		protected System.Web.UI.WebControls.Label lblLastSalary;
		protected System.Web.UI.WebControls.TextBox txtLastSalary;
		protected System.Web.UI.WebControls.Label lblChangeReason;
		protected System.Web.UI.WebControls.TextBox txtChangeReason;
		protected System.Web.UI.WebControls.TextBox txtNote;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.TextBox txtTelephone;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtWorkingBackgroundID;
		protected System.Web.UI.WebControls.DataGrid grdWorkingBackground;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdGrid;
		protected System.Web.UI.WebControls.Label lblFromMonth;
		protected System.Web.UI.WebControls.TextBox txtFromMonth;
		protected System.Web.UI.WebControls.Label lblToMonth;
		protected System.Web.UI.WebControls.TextBox txtToMonth;
		protected System.Web.UI.WebControls.Label lblProjectName;
		protected System.Web.UI.WebControls.TextBox txtProjectName;
		protected System.Web.UI.WebControls.Label lblSponsoredby;
		protected System.Web.UI.WebControls.Label lblMainWork;
		protected System.Web.UI.WebControls.Label lblNote2;
		protected System.Web.UI.WebControls.DataGrid dtgList;
		protected System.Web.UI.WebControls.TextBox txtMainWork;
		protected System.Web.UI.WebControls.TextBox txtDescription;
		protected System.Web.UI.WebControls.TextBox txtProjectCode;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLSProjectParticipationID;
		protected System.Web.UI.WebControls.Label lblProjectCode;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox FromDate;
		#endregion Declare

		private void Page_Load(object sender, System.EventArgs e)
		{
			//if(!Page.IsPostBack)
			//{
				Session["CurFunction"] = Request.Params["FunctionID"].Trim();
				strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";			
				if(!Page.IsPostBack)
				{				
					LoadDataGrid();
					Session["ssStatusProject"] = "AddNew";
				}
				btnSave.Attributes.Add("OnClick", " return validform()");
				btnDelete.Attributes.Add("OnClick", "return checkdelete()");							
		//	}
					
		}
		/// <summary>
		/// Load all traing record of Employee
		/// </summary>
		private void LoadDataGrid()
		{
			DataTable dtList = new DataTable();
			try
			{				
				dtList = clsHRProjectParticipation.GetDataByEmpID(Session["EmpID"]);
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
	
		private void InitializeComponent()
		{
			this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnList.Click += new System.EventHandler(this.btnList_Click);
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			this.dtgList.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgList_ItemCommand);
			this.dtgList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgList_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

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
		
		#endregion
		private void btnSave_Click(object sender, System.EventArgs e)
		{
		{
			try
			{
				if(Session["ssStatusProject"].ToString().Trim()=="AddNew")
					clsCommon.ImpactDB(Session["EmpID"].ToString().Trim(),"Save",this,"HR_spfrmPROJECTPARTICIPATION");
				else
					clsCommon.UpdateByKey("LSProjectParticipationID",txtLSProjectParticipationID.Value.Trim(),"Update",this,"HR_spfrmPROJECTPARTICIPATION");
				clsCommon.ClearControlValue(this);
				btnAddNew_Click(null,null);
				LoadDataGrid();
			}
			catch(Exception ex)
			{
				clsChangeLang.popupWindowExp(this.Parent,ex);
			}
		}
		}
		private void dtgList_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				if(e.CommandName.Trim().ToUpper() =="EDIT")
				{
					txtLSProjectParticipationID.Value = dtgList.Items[e.Item.ItemIndex].Cells[0].Text.Trim();
					DataRow iRow = clsHRProjectParticipation.GetDataByID(txtLSProjectParticipationID.Value.Trim());
					if(iRow != null)
					{					
						txtProjectCode.Text = iRow["ProjectCode"].ToString();
						txtProjectName.Text = iRow["ProjectName"].ToString();
						txtDescription.Text = iRow["Description"].ToString();
						txtFromMonth.Text = iRow["FromMonth"].ToString();
						txtToMonth.Text = iRow["ToMonth"].ToString();
						txtPosition.Text = iRow["Position"].ToString();
						txtMainWork.Text = iRow["MainWork"].ToString();
						txtNote.Text = iRow["Note"].ToString();
						
					}

				}
				Session["ssStatusProject"] = "Edit";
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
		private void btnAddNew_Click(object sender, System.EventArgs e)
		{
			clsCommon.ClearControlValue(this);
			Session["ssStatusProject"] = "AddNew";
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
				for(int i=0;i<dtgList.Items.Count;i++)
				{
					if(((CheckBox)dtgList.Items[i].FindControl("chkSelect")).Checked==true)
					{
						strID += dtgList.Items[i].Cells[0].Text.Trim() + "$";
					}
				}
				clsCommon.DeleteListRecord("HR_spfrmPROJECTPARTICIPATION","LSProjectParticipationID",SqlDbType.NVarChar,12,strID);
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
				string str = ex.Message;
			}
		}
		private void btnList_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(Request.ApplicationPath.Trim() + "/Editpage.aspx?ModuleID=HR&ParentID=8&FunctionID=14&Ascx=MdlHR/EmpList.ascx");
		}
		private void btnExport_Click(object sender, System.EventArgs e)
		{
			/*iHRPCore.DataGridExcelExporter myExcelXport=new DataGridExcelExporter(dtgList);
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
	}
}
