namespace iHRPCore.MdlPR
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Data.SqlClient;
	using System.Configuration;
	using iHRPCore.PRComponent;
	using iHRPCore.Com;
	using iHRPCore.Component;

	/// <summary>
	///		Summary description for ParameterCalculateIncome.
	/// </summary>
	public class ParameterCalculateIncome : UserControlCommon
	{
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel1ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel2ID;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.Label lblBoardingFee;
		protected System.Web.UI.WebControls.Label lblBasicSalary;
		protected System.Web.UI.WebControls.Label lblMinSalary;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.LinkButton btnAddNew;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.DataGrid grdOvertime;
		protected System.Web.UI.WebControls.TextBox txtFromDate;
		protected System.Web.UI.WebControls.TextBox txtBasicSalary;
		protected System.Web.UI.WebControls.TextBox txtMinSalary;
		protected System.Web.UI.WebControls.TextBox txtHygienceFee1;
		protected System.Web.UI.WebControls.TextBox txtHygienceFee;
		protected System.Web.UI.WebControls.TextBox txtBoardingFeeFix;
		protected System.Web.UI.WebControls.TextBox txtBoardingFeeRank2;
		protected System.Web.UI.WebControls.TextBox txtBoardingFeeRank1;
		protected System.Web.UI.WebControls.Label lblBoardingFeeRank2;
		protected System.Web.UI.WebControls.Label lblBoardingFeeRank1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel3ID;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			
			if(!Page.IsPostBack)
			{
				BindDataGrid();
				Session["ssActivityFlag"] = "AddNew";
			}
			btnSave.Attributes.Add("OnClick", " return validform()");
			btnDelete.Attributes.Add("OnClick", "return checkdelete()");
		}
		#region Bind Data Grid
		private void BindDataGrid()
		{
			DataTable dtList = new DataTable();
			try
			{
				dtList= clsPROvertime.GetAllDataParamByID();
				grdOvertime.DataSource = dtList;
				grdOvertime.DataBind();
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
				grdOvertime.CurrentPageIndex=0;
				grdOvertime.DataBind();
				lblErr.Text = "";
			}
			finally
			{
				dtList.Dispose();
			}
		}
		#endregion

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
			this.grdOvertime.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.grdOvertime_ItemCommand);
			this.grdOvertime.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.grdOvertime_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnAddNew_Click(object sender, System.EventArgs e)
		{
			clsCommon.ClearControlValue(this);
			Session["ssActivityFlag"] = "AddNew";
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			
			try
			{
				string strAct = Session["ssActivityFlag"].ToString().Trim();
				if(strAct=="AddNew")
				{
					//bool a = clsCommon.ImpactDB("Save",this,"PR_spfrmPARAMETER");										
					string sErr="";
					sErr=clsCommon.sImpactDB("Save",this,"PR_spfrmPARAMETER");
					if (sErr !="")
						clsChangeLang.popupWindow(this.Parent,"PR_0099",strLanguage,"",0);
					else
					{
						lblErr.Text=sErr;						
						clsCommon.ClearControlValue(this);
						Session["ssActivityFlag"] = "AddNew";
						BindDataGrid();
					}	
				}
				else
				{
					//bool a = clsCommon.ImpactDB("Update",this,"PR_spfrmPARAMETER");
					string sErr="";
					sErr=clsCommon.sImpactDB("Update",this,"PR_spfrmPARAMETER");
					if (sErr !="")
						clsChangeLang.popupWindow(this.Parent,"PR_0099",strLanguage,"",0);
					else
					{
						lblErr.Text=sErr;						
						clsCommon.ClearControlValue(this);
						Session["ssActivityFlag"] = "AddNew";
						BindDataGrid();
					}	
				}

				//clsCommon.ClearControlValue(this);
				//Session["ssActivityFlag"] = "AddNew";
				//BindDataGrid();
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
			
		}

		private void grdOvertime_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			//string strTextField1 = strLanguage == "VN"?"VNName":"Name";
			try
			{
				if(e.CommandName.Trim().ToUpper() =="EDIT")
				{
					txtFromDate.Text = grdOvertime.Items[e.Item.ItemIndex].Cells[0].Text.Trim();
					DataRow iRow = clsPROvertime.GetDataByID_param(txtFromDate.Text.Trim());
					if(iRow != null)
					{
						txtMinSalary.Text  = iRow["MinSalary"].ToString().Trim();
						txtBasicSalary.Text  = iRow["BasicSalary"].ToString().Trim();
						//txtBoardingFee.Text  = iRow["BoardingFee"].ToString().Trim();
						txtHygienceFee.Text  = iRow["HygienceFee"].ToString().Trim();
						txtHygienceFee1.Text  = iRow["HygienceFee1"].ToString().Trim();
						txtBoardingFeeFix.Text = iRow["BoardingFeeFix"].ToString().Trim();
						txtBoardingFeeRank1.Text = iRow["BoardingFeeRank1"].ToString().Trim();
						txtBoardingFeeRank2.Text = iRow["BoardingFeeRank2"].ToString().Trim();
					}
				}
				Session["ssActivityFlag"] = "Edit";
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				string strID="";
				for(int i=0;i<grdOvertime.Items.Count;i++)
				{
					if(((CheckBox)grdOvertime.Items[i].FindControl("chkSelect")).Checked==true)
					{
						strID += grdOvertime.Items[i].Cells[0].Text.Trim() + "$";
					}
				}
				clsCommon.DeleteListRecord("PR_spfrmPARAMETER","FromDate",SqlDbType.NVarChar ,10,strID);
				BindDataGrid();
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}

		private void grdOvertime_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			try
			{
				BindDataGrid();
				grdOvertime.CurrentPageIndex=e.NewPageIndex;
				grdOvertime.DataBind();
			}
			catch(Exception ex)			
			{
				string str = ex.Message;
			}
		}
	}
}
