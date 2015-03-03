namespace iHRPCore.MdlHR
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.HRComponent;
	using iHRPCore.Com;
	using iHRPCore.Include;


	/// <summary>
	///		Summary description for AccidentInsurance.
	/// </summary>
	public class AccidentInsurance : System.Web.UI.UserControl
	{
		#region Declare
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.CheckBox chkShowGrid;
		protected System.Web.UI.WebControls.LinkButton btnAddNew;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnList;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.Label lblEmpID;
		protected System.Web.UI.WebControls.TextBox txtEmpID;
		protected System.Web.UI.WebControls.Label lblName;
		protected System.Web.UI.WebControls.Label lblDivision;
		protected System.Web.UI.WebControls.TextBox txtDivision;
		protected System.Web.UI.WebControls.Label lblCost;
		protected System.Web.UI.WebControls.TextBox txtCost;
		protected System.Web.UI.WebControls.Label lblSumMoney;
		protected System.Web.UI.WebControls.TextBox txtSumMoney;
		protected System.Web.UI.WebControls.Label lblType;
		protected System.Web.UI.WebControls.Label lblAccidentDate;
		protected System.Web.UI.WebControls.Label lblReason;
		protected System.Web.UI.WebControls.TextBox txtReason;
		protected System.Web.UI.WebControls.TextBox txtEmpName;
		protected System.Web.UI.WebControls.RadioButton optLabour;
		protected System.Web.UI.WebControls.RadioButton optTraffic;
		protected System.Web.UI.HtmlControls.HtmlTableRow trGrid;
		protected System.Web.UI.WebControls.TextBox txtType;
		protected System.Web.UI.WebControls.TextBox txtAccInsuranceID;
		protected System.Web.UI.WebControls.TextBox txtAccDate;
		protected System.Web.UI.WebControls.TextBox txtTEmpID;
		protected System.Web.UI.WebControls.DataGrid GrdAccInsurance;
		protected System.Web.UI.WebControls.LinkButton btnPrint;
		protected System.Web.UI.WebControls.Label lblReportDate;
		protected System.Web.UI.WebControls.TextBox Textbox1;
		protected System.Web.UI.WebControls.TextBox txtReportDate;
		protected System.Web.UI.WebControls.Label lblFromDate;
		protected System.Web.UI.WebControls.TextBox txtFromDate;
		protected System.Web.UI.WebControls.TextBox txtToDate;
		protected System.Web.UI.WebControls.Label lblToDate;
		protected string strLanguage="EN";
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			Session["CurFunction"] = Request.Params["FunctionID"].Trim();
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
			if(!Page.IsPostBack)
			{
				Session["ssStatusQual"] = "AddNew";
				this.optLabour.Checked=true;  
				LoadDataGrid(); 
			}
			btnSave.Attributes.Add("OnClick", " return validform()");
			//btnView.Attributes.Add("OnClick", " return checkOption()");
			btnDelete.Attributes.Add("OnClick", "return checkdelete()");	
			btnPrint.Attributes.Add("OnClick","return ShowReport()");
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
			this.GrdAccInsurance.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.GrdAccInsurance_ItemCommand);
			this.GrdAccInsurance.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.GrdAccInsurance_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void LoadDataGrid()
		{
			DataTable dtList = new DataTable();
			try
			{
				dtList = clsHRAccInsuarance.GetDataAll();
				GrdAccInsurance.DataSource = dtList;
				GrdAccInsurance.CurrentPageIndex = 0;
				GrdAccInsurance.DataBind();
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
			if (optLabour.Checked==true)  
				txtType.Text="0";
			else
				txtType.Text="1";  
			try
			{				
				if(Session["ssStatusQual"].ToString().Trim()=="AddNew")
					clsCommon.ImpactDB("","Save",this,"HR_spfrmAccInsurance");
				else
					clsCommon.ImpactDB("","Update",this,"HR_spfrmAccInsurance");				
					//clsCommon.UpdateByKey("AccInsuranceID",txtAccInsuranceID.Text.Trim(),"Update",this,"HR_spfrmAccInsurance");
				btnAddNew_Click(null,null);
				LoadDataGrid();
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}					
		}

		private void btnAddNew_Click(object sender, System.EventArgs e)
		{
			clsCommon.ClearControlValue(this);
			Session["ssStatusQual"] = "AddNew";
			txtEmpName.Text="";
			txtDivision.Text=""; 
			optLabour.Checked=true;  
			if(chkShowGrid.Checked==true)
				trGrid.Style.Add("DISPLAY","block");
			else				
				trGrid.Style.Add("DISPLAY","none");

		}

		private void GrdAccInsurance_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				if(e.CommandName.Trim().ToUpper() =="EDIT")
				{
					txtAccInsuranceID.Text= GrdAccInsurance.Items[e.Item.ItemIndex].Cells[1].Text.Trim();
					DataRow iRow = clsHRAccInsuarance.GetDataByID(txtAccInsuranceID.Text.Trim());
					if(iRow != null)
					{
						txtTEmpID.Text = iRow["EmpID"].ToString().Trim();
						txtSumMoney.Text= iRow["SumMoney"].ToString().Trim();
						txtCost.Text= iRow["Cost"].ToString().Trim();
						txtReason.Text= iRow["Reason"].ToString().Trim();
						txtAccDate.Text= iRow["AccDate"].ToString().Trim();
						txtType.Text=iRow["Type"].ToString().Trim();
						if (txtType.Text=="False") 
						{
							optLabour.Checked=true;
							optTraffic.Checked=false;  
						}
						else
						{
							optTraffic.Checked=true;  
							optLabour.Checked=false;
						}
						FillInformation(txtTEmpID.Text); 
					}

				}
				Session["ssStatusQual"] = "Edit";
				if(chkShowGrid.Checked==true)
					trGrid.Style.Add("DISPLAY","block");
				else				
					trGrid.Style.Add("DISPLAY","none");
			}	
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}

		}

		private void FillInformation(string strEmpID)
		{
			DataTable dtList = new DataTable();
			dtList=clsHRAccInsuarance.GetEmpInfo(strEmpID);  
			if(dtList.Rows.Count!=0)
			{
				txtEmpName.Text=dtList.Rows[0]["EmpName"].ToString();
				txtDivision.Text=dtList.Rows[0]["LSLevel1Code"].ToString();   
			}
				
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				string strID="";
				for(int i=0;i<GrdAccInsurance.Items.Count;i++)
				{
					if(((CheckBox)GrdAccInsurance.Items[i].FindControl("chkSelect")).Checked==true)
					{
						strID += GrdAccInsurance.Items[i].Cells[1].Text.Trim() + "$";
					}
				}
				clsCommon.DeleteListRecord("HR_spfrmAccInsurance","AccInsuranceID",SqlDbType.Int,4,strID);
				if(chkShowGrid.Checked==true)
					trGrid.Style.Add("DISPLAY","block");
				else				
					trGrid.Style.Add("DISPLAY","none");
				LoadDataGrid();
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		
		}

		private void GrdAccInsurance_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			try
			{
				LoadDataGrid();
				GrdAccInsurance.CurrentPageIndex=e.NewPageIndex;
				GrdAccInsurance.DataBind();
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

		
	}
}
