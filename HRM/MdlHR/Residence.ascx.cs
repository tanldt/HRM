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
	using iHRPCore.TMSComponent; 
	using System.Data.SqlClient;
	

	/// <summary>
	///		Summary description for Residence.
	/// </summary>
	public class Residence : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.LinkButton btnExit;
		protected System.Web.UI.WebControls.TextBox txtToMonth;
		protected System.Web.UI.WebControls.TextBox txtFromMonth;
		protected System.Web.UI.WebControls.DataGrid dtgList;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnAddNew;
		protected System.Web.UI.WebControls.TextBox txtAddress;
		protected System.Web.UI.WebControls.TextBox txtDistrict;
		protected System.Web.UI.WebControls.DropDownList cboLSProvinceID;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtResidenceID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtflag;
		protected System.Web.UI.WebControls.Label lblTitle;
		public string strLanguage = "EN";

		private void Page_Load(object sender, System.EventArgs e)
		{
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";			
			if(!Page.IsPostBack)
			{
				LoadDataCombo();	
				LoadDataGrid();
				Session["ssAddnew"]="Addnew";
			}
			btnSave.Attributes.Add("OnClick", " return validform()");
			btnDelete.Attributes.Add("OnClick", "return checkdelete()");
			btnExit.Attributes.Add("OnClick","return exitWindow()");
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
			this.dtgList.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgList_ItemCommand);
			this.dtgList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgList_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		private void LoadDataCombo()
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";		
			clsCommon.LoadDropDownListControl(cboLSProvinceID,"sp_GetDataCombo @TableName='LS_tblProvince',@Fields='LSProvinceID as [ID]," + strTextField + " as Name'","ID","Name",true);			
		}
		private void LoadDataGrid()
		{
			DataTable dtList = new DataTable();
			try
			{				
				string strEmpID=Session["EmpID"].ToString().Trim();
				dtList=clsHRResident.GetDataByEmpID(strEmpID,strLanguage);
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
			try
			{	
				if(Session["ssAddnew"].ToString().Trim()=="Addnew")			
					clsCommon.ImpactDB("@EmpID",Session["EmpID"].ToString().Trim(),"Save",this,"HR_spfrmRESIDENCE");					
				else				
					clsCommon.ImpactDB("@ResidenceID",txtResidenceID.Value,"Update",this,"HR_spfrmRESIDENCE");
					
						
				lblErr.Text = "";
				LoadDataGrid();
				btnAddNew_Click(null,null);
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
			finally
			{
				//txtReturnValue.Value = "";
			}
		}
		private void dtgList_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				if(e.CommandName.Trim().ToUpper() =="EDIT")
				{
					txtResidenceID.Value = dtgList.Items[e.Item.ItemIndex].Cells[0].Text.Trim();
					
					DataRow iRow = clsHRResident.GetDataByID(txtResidenceID.Value);
					if(iRow != null)
					{						
						txtFromMonth.Text=iRow["FromMonth"].ToString().Trim();
						txtToMonth.Text=iRow["ToMonth"].ToString().Trim();
						txtAddress.Text=iRow["Address"].ToString().Trim();
						txtDistrict.Text=iRow["District"].ToString().Trim();
						cboLSProvinceID.SelectedValue=iRow["LSProvinceID"].ToString().Trim();												
					}					
				}
				Session["ssAddnew"] = "Edit";
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}
		private void btnAddNew_Click(object sender, System.EventArgs e)
		{
			clsCommon.ClearControlValue(this);
			Session["ssAddnew"] = "Addnew";
			btnSave.Enabled = true;
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
				clsCommon.DeleteListRecord("HR_spfrmRESIDENCE","ResidenceID",SqlDbType.NVarChar,12,strID);
				LoadDataGrid();	
				btnAddNew_Click(null,null);
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

		private void btnExport_Click(object sender, System.EventArgs e)
		{
			iHRPCore.DataGridExcelExporter myExcelXport=new DataGridExcelExporter(dtgList);			
			myExcelXport.Export("");
			myExcelXport =null;
		}

	}
}
