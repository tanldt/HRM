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
	using System.Data.SqlClient;
	using System.Configuration;
	using iHRPCore.HRComponent;

	/// <summary>
	///		Summary description for Overtime.
	/// </summary>
	public class Overtime : System.Web.UI.UserControl
	{
		#region Declare
		public string strLanguage = "EN";
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtTimeFrom;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.TextBox txtTimeTo;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.TextBox txtPurpose;
		protected System.Web.UI.WebControls.TextBox txtNote;
		protected System.Web.UI.WebControls.CheckBox chkShowGrid;
		protected System.Web.UI.WebControls.LinkButton btnAddNew;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.TextBox txtPageRows;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.Label Label14;
		protected System.Web.UI.WebControls.Label Label16;
		protected System.Web.UI.WebControls.TextBox txtHours;
		protected System.Web.UI.WebControls.LinkButton btnSearch;
		protected System.Web.UI.WebControls.LinkButton butImport;
		protected System.Web.UI.WebControls.LinkButton butExport;
		protected System.Web.UI.WebControls.Label lblTotalRows;
		protected System.Web.UI.WebControls.TextBox txtTodate;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtOTDate;
		protected System.Web.UI.WebControls.DropDownList cboOTTypeCode;
		protected System.Web.UI.WebControls.DropDownList cboChargeBudgetCode;
		protected System.Web.UI.WebControls.TextBox txtPRMonth;
		protected System.Web.UI.WebControls.TextBox txtAmount;
		protected System.Web.UI.WebControls.DataGrid grdOvertime;
		protected System.Web.UI.HtmlControls.HtmlTableRow trGrid;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtOTID;
		protected System.Web.UI.WebControls.TextBox txtFromDate;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtBasicSalary;
		protected System.Web.UI.WebControls.Label lblErr;
		#endregion
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
			if(!Page.IsPostBack)
			{
				txtTodate.Text = DateTime.Today.ToString("dd/MM/yyyy");
				LoadDataCombo();
				LoadDataGrid();
				Session["ssStatusOvertime"] = "AddNew";
				if(Session["ssOvertimeID"]!=null)
				{
					LoadDataToEdit(Session["ssOvertimeID"].ToString().Trim());
					Session["ssOvertimeID"]= null;					
				}
				txtBasicSalary.Value = clsCommon.LookUpTable("PR_spfrmOT @Activity = 'GetSalaryByEmpID',@EmpID='" + Session["EmpID"] + "'","BasicSalary");
			}
			btnSave.Attributes.Add("OnClick", " return validform()");
			btnDelete.Attributes.Add("OnClick", "return checkdelete()");
		}
		/// <summary>
		/// Load all Overtime record of Employee
		/// </summary>
		private void LoadDataGrid()
		{
			DataTable dtList = new DataTable();
			try
			{				
				dtList = clsPROvertime.GetDataByDate(Session["EmpID"],txtFromDate.Text.Trim(),txtTodate.Text.Trim());
				grdOvertime.DataSource = dtList;
				grdOvertime.CurrentPageIndex = 0;
				grdOvertime.DataBind();
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
		/// <summary>
		/// Load data to combo box
		/// </summary>
		private void LoadDataCombo()
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";			
			clsCommon.LoadDropDownListControl(cboOTTypeCode,"sp_GetDataCombo @TableName='LS_tblOTType',@Fields='LSOTTypeCode," + strTextField + " as Name'","LSOTTypeCode","Name",true);
			clsCommon.LoadDropDownListControl(cboChargeBudgetCode,"sp_GetDataCombo @TableName='LS_tblChargeBudget',@Fields='LSChargeBudgetCode," + strTextField + " as Name'","LSChargeBudgetCode","Name",true);
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
            this.butExport.Click += new System.EventHandler(this.butExport_Click);
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            this.grdOvertime.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.grdOvertime_ItemCommand);
            this.grdOvertime.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.grdOvertime_PageIndexChanged);
            this.Load += new System.EventHandler(this.Page_Load);

        }
		#endregion

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				//KIEM TRA GIOI HAN THOI GIAN LAM OT
				if(clsPROvertime.InvalidHours(txtOTDate.Text.Trim(),txtTimeFrom.Text.Trim())==true)
				{
					lblErr.Text = "Time OT must be after 18:30, Please check again!";
					return;
				}
				// KIEM TRA TRUNG THOI GIAN LAM OT
				if(clsPROvertime.ExistsHours(Session["EmpID"],txtOTID.Value.Trim(),txtOTDate.Text.Trim(),txtTimeFrom.Text.Trim(),txtTimeTo.Text.Trim())==true)
				{
					lblErr.Text = "The interval between " + txtTimeFrom.Text + " and " + txtTimeTo.Text + " has been existing";
					return;
				}
				//KIEM TRA SO GIO LAM OT VUOT GOI HAN <NGAY THUONG 4H: NGAY THU 7 LE 12H>
				string str_Return = clsPROvertime.UnRegularHours(Session["EmpID"],txtOTID.Value.Trim(),txtOTDate.Text.Trim(),txtHours.Text.Trim()).Trim();
				if(str_Return!="")
				{
					lblErr.Text = str_Return;
					return;
				}
				if(Session["ssStatusOvertime"].ToString().Trim()=="AddNew")
				{
					clsCommon.ImpactDB(Session["EmpID"].ToString().Trim(),"Save",this,"PR_spfrmOT");

					//Hau
					Save_ChangeLog();
					////
				}
				else
				{
					clsCommon.UpdateByKey("OTID",txtOTID.Value.Trim(),"Update",this,"PR_spfrmOT");
					
					//Hau
					Save_ChangeLog();
					////
				}
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
			lblErr.Text = "";
			clsCommon.ClearControlValue(this);
			Session["ssStatusOvertime"] = "AddNew";
			if(chkShowGrid.Checked==true)
				trGrid.Style.Add("DISPLAY","block");
			else
				trGrid.Style.Add("DISPLAY","none");
			txtTodate.Text = DateTime.Today.ToString("dd/MM/yyyy");

			//Hau
			txtOTDate.ToolTip = txtOTDate.Text;
			txtTimeFrom.ToolTip = txtTimeFrom.Text;
			txtTimeTo.ToolTip = txtTimeTo.Text;
			txtHours.ToolTip = txtHours.Text;
			strTypeCode = cboOTTypeCode.SelectedItem.Text;
			strChargeBudgetCode = cboChargeBudgetCode.SelectedItem.Text;
			txtPRMonth.ToolTip = txtPRMonth.Text;
			txtPurpose.ToolTip = txtPurpose.Text;
			////
		}

		private void grdOvertime_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				if(e.CommandName.Trim().ToUpper() =="EDIT")
				{					
					LoadDataToEdit(grdOvertime.Items[e.Item.ItemIndex].Cells[0].Text.Trim());

					//Hau
					txtOTDate.ToolTip = txtOTDate.Text;
					txtTimeFrom.ToolTip = txtTimeFrom.Text;
					txtTimeTo.ToolTip = txtTimeTo.Text;
					txtHours.ToolTip = txtHours.Text;
					strTypeCode = cboOTTypeCode.SelectedItem.Text;
					strChargeBudgetCode = cboChargeBudgetCode.SelectedItem.Text;
					txtPRMonth.ToolTip = txtPRMonth.Text;
					txtPurpose.ToolTip = txtPurpose.Text;
					////
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
			try
			{
				txtOTID.Value = strID;
				DataRow iRow = clsPROvertime.GetDataByID(strID);
				if(iRow != null)
				{
					txtAmount.Text = iRow["Amount"].ToString().Trim();
					txtHours.Text = iRow["Hours"].ToString().Trim();
					txtNote.Text = iRow["Note"].ToString().Trim();
					txtPRMonth.Text = iRow["PRMonth"].ToString().Trim();
					txtPurpose.Text = iRow["Purpose"].ToString().Trim();
					txtTimeFrom.Text = iRow["TimeFrom"].ToString().Trim();
					txtTimeTo.Text = iRow["TimeTo"].ToString().Trim();
					txtOTDate.Text = iRow["OTDate"].ToString().Trim();
					cboOTTypeCode.SelectedValue = iRow["OTTypeCode"].ToString().Trim();
					cboChargeBudgetCode.SelectedValue = iRow["ChargeBudgetCode"].ToString().Trim();
				}
				Session["ssStatusOvertime"] = "Edit";
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
		//DELETE CHECKED RECORD
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
				clsCommon.DeleteListRecord("PR_spfrmOT","OTID",SqlDbType.Int,4,strID);
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

		private void grdOvertime_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			try
			{
				LoadDataGrid();
				grdOvertime.CurrentPageIndex=e.NewPageIndex;
				grdOvertime.DataBind();
			}
			catch(Exception ex)			
			{
				string str = ex.Message;
			}
		}

		private void butExport_Click(object sender, System.EventArgs e)
		{
			iHRPCore.DataGridExcelExporter myExcelXport=new DataGridExcelExporter(grdOvertime);			
			myExcelXport.Export("");
			myExcelXport =null;
		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			LoadDataGrid();			
		}

		#region Hau

		static public String strTypeCode;
		static public String strChargeBudgetCode;

		private void Save_ChangeLog()
		{	
			String strUserName = Session[0].ToString().Trim();
			//DateTime dtime = DateTime.Now;
			String strActionTime = DateTime.Now.ToString();
			String strAction = Session["ssStatusOvertime"].ToString().Trim();
			String strEmpID = Session["EmpID"].ToString();
			DataTable tbl = clsHREmpList.GetEmpList(Session["EmpID"].ToString().Trim(),this.Page,"0");
			String strEmpName = tbl.Rows[0]["EmpName"].ToString();
			String[][] strFieldChanged;
			strFieldChanged = new String[3][];
			strFieldChanged[0] = new String[8];
			strFieldChanged[1] = new String[8];
			strFieldChanged[2] = new String[8];
			int i = 0;
			if((strAction.ToUpper().Trim().Equals("EDIT") && !txtOTDate.ToolTip.Trim().Equals(txtOTDate.Text.Trim()))
				|| (strAction.ToUpper().Trim().Equals("ADDNEW") && !txtOTDate.Text.Trim().Equals("")))
			{
				strFieldChanged[0][i] = "Date";
				strFieldChanged[1][i] = txtOTDate.ToolTip;
				strFieldChanged[2][i] = txtOTDate.Text;
				i++;
			}
			if((strAction.ToUpper().Trim().Equals("EDIT") && !txtTimeFrom.ToolTip.Trim().Equals(txtTimeFrom.Text.Trim()))
				|| (strAction.ToUpper().Trim().Equals("ADDNEW") && !txtTimeFrom.Text.Trim().Equals("")))
			{
				strFieldChanged[0][i] = "Time from";
				strFieldChanged[1][i] = txtTimeFrom.ToolTip;
				strFieldChanged[2][i] = txtTimeFrom.Text;
				i++;
			}
			if((strAction.ToUpper().Trim().Equals("EDIT") && !txtTimeTo.ToolTip.Trim().Equals(txtTimeTo.Text.Trim()))
				|| strAction.ToUpper().Trim().Equals("ADDNEW")&& !txtTimeTo.Text.Trim().Equals(""))
			{
				strFieldChanged[0][i] = "Time to";
				strFieldChanged[1][i] = txtTimeTo.ToolTip;
				strFieldChanged[2][i] = txtTimeTo.Text;
				i++;
			}
			if((strAction.ToUpper().Trim().Equals("EDIT") && !txtHours.ToolTip.Trim().Equals(txtHours.Text.Trim())) 
				|| strAction.ToUpper().Trim().Equals("ADDNEW") && !txtHours.Text.Trim().Equals(""))
			{
				strFieldChanged[0][i] = "No of Hours";
				strFieldChanged[1][i] = txtHours.ToolTip;
				strFieldChanged[2][i] = txtHours.Text;
				i++;
			}
			if((strAction.ToUpper().Trim().Equals("EDIT") && !strTypeCode.Trim().Equals(cboOTTypeCode.SelectedItem.Text.Trim())) 
				|| strAction.ToUpper().Trim().Equals("ADDNEW") && !cboOTTypeCode.SelectedItem.Text.Trim().Equals(""))
			{
				strFieldChanged[0][i] = "Type";
				strFieldChanged[1][i] = strTypeCode;
				strFieldChanged[2][i] = cboOTTypeCode.SelectedItem.Text;
				i++;
			}
			if((strAction.ToUpper().Trim().Equals("EDIT") && !strChargeBudgetCode.Trim().Equals(cboChargeBudgetCode.SelectedItem.Text.Trim())) 
				|| strAction.ToUpper().Trim().Equals("ADDNEW") && !cboChargeBudgetCode.SelectedItem.Text.Trim().Equals(""))
			{
				strFieldChanged[0][i] = "Charge budget";
				strFieldChanged[1][i] = strChargeBudgetCode;
				strFieldChanged[2][i] = cboChargeBudgetCode.SelectedItem.Text;
				i++;
			}		
			if((strAction.ToUpper().Trim().Equals("EDIT") && !txtPRMonth.ToolTip.Trim().Equals(txtPRMonth.Text.Trim())) 
				|| strAction.ToUpper().Trim().Equals("ADDNEW") && !txtPRMonth.Text.Trim().Equals(""))
			{
				strFieldChanged[0][i] = "PR month";
				strFieldChanged[1][i] = txtPRMonth.ToolTip;
				strFieldChanged[2][i] = txtPRMonth.Text;
				i++;
			}		
			if((strAction.ToUpper().Trim().Equals("EDIT") && !txtPurpose.ToolTip.Trim().Equals(txtPurpose.Text.Trim())) 
				|| strAction.ToUpper().Trim().Equals("ADDNEW") && !txtPurpose.Text.Trim().Equals(""))
			{
				strFieldChanged[0][i] = "Purpose";
				strFieldChanged[1][i] = txtPurpose.ToolTip;
				strFieldChanged[2][i] = txtPurpose.Text;
				i++;
			}		
			
			SaveLog(strUserName, strActionTime, strEmpID, strEmpName, strAction, strFieldChanged, i);
		}

		private void SaveLog(String UserName, String ActionTime, String EmpID, String EmpName, String UserAction, String[][] FieldChanged, int FieldCount)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			
			String CommandText = "insert into ActionLog values(N'";
			CommandText += (UserName + "', '" + ActionTime + "', N'" + UserAction + "', '" + EmpID + "', N'" + EmpName + "', N'");
			try
			{
				for(int i=0; i<FieldCount; i++)
				{
					cmd.CommandText = CommandText + FieldChanged[0][i] + "', N'" + FieldChanged[1][i] + "', N'" + FieldChanged[2][i] + "')";
					cmd.ExecuteNonQuery();
				}
				sqlTran.Commit();
			}
			catch //(Exception exp)
			{
				//lblErr.Text = exp.Message;
				sqlTran.Rollback();
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();
			}
		}

		#endregion
	}
}
