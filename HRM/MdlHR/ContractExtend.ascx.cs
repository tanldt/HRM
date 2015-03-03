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
	using System.Data.SqlClient;
	using System.Configuration;

	/// <summary>
	///		Summary description for ContractExtend.
	/// </summary>
	public class ContractExtend : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.DropDownList cboYear;
		protected System.Web.UI.WebControls.DropDownList cboMonth;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.LinkButton btnList;
		protected System.Web.UI.WebControls.CheckBox chkShowGrid;
		protected System.Web.UI.WebControls.LinkButton btnFilter;
		protected System.Web.UI.WebControls.Label lblSigner;
		protected System.Web.UI.WebControls.Label lblPosSigner;
		protected System.Web.UI.WebControls.TextBox txtSignerPosition;
		protected System.Web.UI.WebControls.Label lblSignDate;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.TextBox txtSignDate;
		protected System.Web.UI.WebControls.TextBox txtSigner;
		protected System.Web.UI.WebControls.DataGrid dtgList;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel1ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel2ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel3ID;
		protected System.Web.UI.WebControls.DropDownList cboLevel1ID;
		protected System.Web.UI.WebControls.DropDownList cboLevel2ID;
		protected System.Web.UI.WebControls.DropDownList cboCompanyID;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.CheckBox Checkbox1;
		protected System.Web.UI.WebControls.CheckBox Checkbox2;
		protected System.Web.UI.WebControls.CheckBox chkTX7;
		protected System.Web.UI.WebControls.CheckBox chkTX6;
		protected System.Web.UI.WebControls.CheckBox chkTX4;
		protected System.Web.UI.WebControls.CheckBoxList chklistContractType;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList Dropdownlist1;
		public string strLanguage = "EN";

		private void Page_Load(object sender, System.EventArgs e)
		{
			Session["CurFunction"] = Request.Params["FunctionID"].Trim();
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";	
			this.lblErr.Text = "";

			if(!Page.IsPostBack)
			{
				LoadDataCombo();				
				cboMonth.SelectedValue = System.DateTime.Today.ToString("MM");
				cboYear.SelectedValue = System.DateTime.Today.ToString("yyyy");
				btnFilter_Click(null, null);
				loadDataCheckBoxLst();
			}
			btnSave.Attributes.Add("OnClick", " return validform()");
		}

		/// <summary>
		/// Get data for combo
		/// </summary>
		private void LoadDataCombo()
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			clsHREmpList.LoadComboCompany(cboCompanyID, strTextField, strLanguage,this.Page);
			clsHREmpList.LoadComboLevel1(cboLevel1ID,strTextField,strLanguage,this.Page);  			
			clsCommon.LoadMonthToList(cboMonth);
			clsCommon.LoadYearToList(cboYear, System.DateTime.Today.Year - 1,System.DateTime.Today.Year + 1);
			clsCommon.LoadDropDownListControl(Dropdownlist1,"sp_GetDataCombo @TableName='LS_tblContracttype',@Fields='LSContractTypeID as [ID]," + strTextField + " as Name'","ID","Name",true);			
		}

		/// <summary>
		/// Load all docuemnt records of Employee
		/// </summary>
		private void LoadDataGrid()
		{
			/*string strContractType = "";
			if (chkTX4.Checked)
				strContractType += "TX4";
			if (chkTX6.Checked)
				strContractType += ",TX6";
			if (chkTX7.Checked)
				strContractType += ",TX7";
			*/
			string strContractType = "";
			/*if (chkTX4.Checked)
				strContractType += "TX4";
			if (chkTX6.Checked)
				strContractType += ",TX6";
			if (chkTX7.Checked)
				strContractType += ",TX7";*/

			for(int i=0;i<chklistContractType.Items.Count;i++)
			{
				if(chklistContractType.Items[i].Selected==true)
				{
					strContractType += chklistContractType.Items[i].Value.ToString()+ ",";     
				}
			}
			if(strContractType.Length>0)
			{
				strContractType = strContractType.Substring(0,strContractType.Length-1); 
			}

			DataTable dtList = new DataTable();
			try
			{				
				dtList = clsHRContractExtend.GetExpireContract(cboMonth.SelectedValue.Trim(), cboYear.SelectedValue.Trim(), cboCompanyID.SelectedValue.Trim(), txtLevel1ID.Value.Trim(), txtLevel2ID.Value.Trim(), strContractType);
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
		private void btnList_Click(object sender, System.EventArgs e)
			{
				Response.Redirect(Request.ApplicationPath.Trim() + "/Editpage.aspx?ModuleID=HR&ParentID=8&FunctionID=14&Ascx=MdlHR/EmpList.ascx");
			}

		private void btnExport_Click(object sender, System.EventArgs e)
		{
			iHRPCore.DataGridExcelExporter myExcelXport=new iHRPCore.DataGridExcelExporter(dtgList);
			myExcelXport.Export("");
			myExcelXport = null;
		}
		private void loadDataCheckBoxLst()
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			clsCommon.LoadCheckBoxList(chklistContractType,"sp_GetDataCombo @TableName = 'LS_tblContractType',@Fields='LSContractTypeID," + strTextField + " as Name'","LSContractTypeID","Name");
			for(int i=0;i<chklistContractType.Items.Count;i++)
			{
				chklistContractType.Items[i].Selected=true;   
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
			this.cboCompanyID.SelectedIndexChanged += new System.EventHandler(this.cboCompanyID_SelectedIndexChanged);
			this.cboLevel1ID.SelectedIndexChanged += new System.EventHandler(this.cboLevel1ID_SelectedIndexChanged);
			this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnList.Click += new System.EventHandler(this.btnList_Click);
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnFilter_Click(object sender, System.EventArgs e)
		{
			LoadDataGrid();
			cboCompanyID_SelectedIndexChanged(null, null);
		}

		
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			string strEmpIDNotContract = "";
			int i;
			CheckBox obj = new CheckBox();
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				for (i = 0;i<= this.dtgList.Items.Count - 1;i++)
				{
					obj = (CheckBox) this.dtgList.Items[i].FindControl("chkSelect");
					if (obj.Checked)
					{
						string mdtg_EmpID = this.dtgList.Items[i].Cells[0].Text.Trim();

						cmd.CommandType = CommandType.StoredProcedure;
						cmd.CommandText = "HR_spfrmContractExtend";				
						cmd.Parameters.Clear();
						SqlParameter mparam = cmd.Parameters.Add("@Result",SqlDbType.NVarChar,200);
						mparam.Direction = ParameterDirection.Output;

						cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 50).Value = "AddNew_Auto";
						cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar,12).Value = mdtg_EmpID;						
						cmd.Parameters.Add("@SignDate", SqlDbType.NVarChar, 12).Value = txtSignDate.Text.Trim();
						cmd.Parameters.Add("@Signer", SqlDbType.NVarChar, 50).Value = txtSigner.Text.Trim();
						cmd.Parameters.Add("@SignerPosition", SqlDbType.NVarChar, 120).Value = txtSignerPosition.Text.Trim();
						cmd.Parameters.Add("@Creater", SqlDbType.NVarChar, 80).Value = Session["AccountLogin"];
						cmd.Parameters.Add("@CreateTime", SqlDbType.VarChar, 12).Value = DateTime.Today;
						cmd.ExecuteNonQuery();
						///neu xay ra loi voi bat ky hop dong cho nhan nao thi se rollback lai toan bo tien trinh
						if(cmd.Parameters["@result"].Value.ToString().Trim() != "")
						{
							//KhanhMT -18/07/2004
							if (cmd.Parameters["@result"].Value.ToString().IndexOf(mdtg_EmpID) != 0)
								strEmpIDNotContract += cmd.Parameters["@result"].Value.ToString().Trim() + "<br>";
							else
							{
								lblErr.Text = cmd.Parameters["@result"].Value.ToString().Trim();
								cmd.Transaction.Rollback();
								cmd.Dispose();
								SQLconn.Close();
								SQLconn.Dispose();
								return ;
							}
						}
					}
				}
				obj.Dispose();
				cmd.Transaction.Commit();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();

				this.btnFilter_Click(sender,e);
				if (strEmpIDNotContract != "") lblErr.Text = strEmpIDNotContract;
			
			}
			catch(Exception ex)
			{
				obj.Dispose();
				cmd.Transaction.Rollback();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				lblErr.Text = ex.Message;
			}
		}

		private void cboCompanyID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				string strTextField = strLanguage == "VN"?"VNName":"Name";
				string strCompanyID = cboCompanyID.SelectedValue.Trim();
				clsHREmpList.LoadComboLevel1ByCompany(cboLevel1ID,strTextField,strCompanyID, strLanguage,this.Page); 
				cboLevel1ID.SelectedValue = this.txtLevel1ID.Value.Trim();
				cboLevel1ID_SelectedIndexChanged(null,null);
			}
			catch(Exception ex)
			{
				lblErr.Text=ex.ToString();
			}
		}
		private void cboLevel1ID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				string strLevel1ID = cboLevel1ID.SelectedValue.Trim();
				string strTextField = strLanguage == "VN"?"VNName":"Name";
				clsHREmpList.LoadComboLevel2ByLevel1(cboLevel2ID,strTextField,strLevel1ID, strLanguage,this.Page); 
				cboLevel2ID.SelectedValue = this.txtLevel2ID.Value.Trim();
			}
			catch(Exception ex)
			{				
			}
		}

		private void Checkbox2_CheckedChanged(object sender, System.EventArgs e)
		{
		
		}	
	}

}
