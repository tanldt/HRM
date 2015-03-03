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
	using iHRPCore.Component;

	/// <summary>
	///		Summary description for Deduction.
	/// </summary>
	public class Deduction : UserControlCommon
	{
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label lblPayToSalary;
		protected System.Web.UI.WebControls.CheckBox chkShowGrid;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.Label lblTotalRows;
		protected System.Web.UI.WebControls.LinkButton Linkbutton3;
		protected System.Web.UI.WebControls.LinkButton btnAddNew;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.LinkButton btnImport;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.DataGrid grdDeduction;
		protected System.Web.UI.WebControls.TextBox txtPageRows;
		protected System.Web.UI.WebControls.TextBox txtToMonth;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtFromMonth;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnRowNumber;
		protected System.Web.UI.HtmlControls.HtmlTableRow trGrid;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtDeductionID;
		protected System.Web.UI.WebControls.Label lblAmountDesc;
		protected System.Web.UI.WebControls.TextBox txtAmountPerMonth;
		protected System.Web.UI.WebControls.Label lblMonth;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdMonth;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdDate;
		protected System.Web.UI.WebControls.CheckBox chkToPR;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.CheckBox chkPIT;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.TextBox txtNote;
		protected System.Web.UI.WebControls.DropDownList cboLSDeductionID;
		protected System.Web.UI.WebControls.DropDownList cboCurrencyTypeID;
		protected System.Web.UI.WebControls.RadioButtonList optIsGross;
		protected System.Web.UI.WebControls.RadioButtonList optLSMethodID;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			if(!Page.IsPostBack)
			{
				LoadDataCombo();
				LoadDataGrid();
				Session["ssStatusDeduction"] = "AddNew";
				if(Session["ssDeductionID"]!=null)
				{
					LoadDataToEdit(Session["ssDeductionID"].ToString().Trim());
					Session["ssDeductionID"]= null;					
				}
			}
			btnSave.Attributes.Add("OnClick", " return validform()");
			btnDelete.Attributes.Add("OnClick", "return checkdelete()");
			btnImport.Attributes.Add("onclick","return ShowExcelSelectPage();");
		}
		/// <summary>
		/// Load all Allowance of Employee
		/// </summary>
		private void LoadDataGrid()
		{
			DataTable dtList = new DataTable();
			try
			{
				dtList = clsPRDeduction.GetDataByEmpID(Session["EmpID"],strLanguage);				
				grdDeduction.DataSource = dtList;
				grdDeduction.CurrentPageIndex = 0;
				grdDeduction.DataBind();
				lblTotalRows.Text = dtList.Rows.Count.ToString();
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
			clsCommon.LoadDropDownListControl(cboLSDeductionID,"sp_GetDataCombo @TableName='LS_tblDeduction',@Fields='LSDeductionID," + strTextField + " as Name'","LSDeductionID","Name",true);
			clsCommon.LoadDropDownListControl(cboCurrencyTypeID,"LS_spfrmCURRENCYTYPE @Activity=N'GetDataCombo',@LanguageID='"+ strLanguage+ "'","Ma","Ten",false);
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
			this.grdDeduction.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.grdDeduction_ItemCommand);
			this.grdDeduction.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.grdDeduction_PageIndexChanged);
			this.btnRowNumber.ServerClick += new System.EventHandler(this.btnRowNumber_ServerClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnAddNew_Click(object sender, System.EventArgs e)
		{
			clsCommon.ClearControlValue(this);
			Session["ssStatusDeduction"] = "AddNew";
			if(chkShowGrid.Checked==true)				
				trGrid.Style.Add("DISPLAY","block");
			else
				trGrid.Style.Add("DISPLAY","none");
			chkPIT.Checked=true;
			chkToPR.Checked=true;
			/*cangtt -- 13102005
			//METHOD TYPE
			if(optLSMethodID.SelectedIndex == 2) //Flex mark
			{
				tdDate.Style.Add("DISPLAY","block");
				tdMonth.Style.Add("DISPLAY","none");
				lblMonth.Text = "Date";
			}
			else
			{
				tdDate.Style.Add("DISPLAY","none");
				tdMonth.Style.Add("DISPLAY","block");
				lblMonth.Text = "From Month";
			}*/			
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{				
				if(Session["ssStatusDeduction"].ToString().Trim()=="AddNew")
				{
					clsCommon.ImpactDB("@Creater",Session["AccountLogin"],Session["EmpID"].ToString().Trim(),"Save",this,"PR_spfrmDEDUCTION");

					/*//Hau
					Save_ChangeLog();
					////*/
				}
				else
				{
					clsCommon.UpdateByKey("@Editer",Session["AccountLogin"],"DeductionID",txtDeductionID.Value.Trim(),"Update",this,"PR_spfrmDEDUCTION");

					/*//Hau
					Save_ChangeLog();
					////*/
				}
				btnAddNew_Click(null,null);
				LoadDataGrid();
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}
/// <summary>
/// load record data
/// </summary>
/// <param name="source"></param>
/// <param name="e"></param>
		private void grdDeduction_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				if(e.CommandName.Trim().ToUpper() =="EDIT")
				{
					LoadDataToEdit(grdDeduction.Items[e.Item.ItemIndex].Cells[0].Text.Trim());					
					
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
				txtDeductionID.Value = strID;
				DataRow iRow = clsPRDeduction.GetDataByID(strID);
				if(iRow != null)
				{
					txtAmountPerMonth.Text = iRow["AmountPerMonth"].ToString().Trim();					
					txtNote.Text = iRow["Note"].ToString().Trim();
					txtFromMonth.Text = iRow["FromMonth"].ToString().Trim();
					txtToMonth.Text = iRow["ToMonth"].ToString().Trim();										
					cboLSDeductionID.SelectedValue = iRow["LSDeductionID"].ToString().Trim();
					cboCurrencyTypeID.SelectedValue = iRow["LSCurrencyTypeID"].ToString().Trim();
					optLSMethodID.SelectedValue = iRow["LSMethodID"].ToString().Trim();
					
					chkPIT.Checked=iRow["PIT"].ToString()=="True"?true:false;
					chkToPR.Checked=iRow["ToPR"].ToString()=="True"?true:false;

					if(optLSMethodID.SelectedIndex == 0)
						lblAmountDesc.Text = " VND";
					else if(optLSMethodID.SelectedIndex == 1)
						lblAmountDesc.Text = " % Luong";

					if (iRow["IsGross"].ToString().Trim()=="True")
						optIsGross.SelectedValue = "1";
					else
						optIsGross.SelectedValue = "0";
				}
				Session["ssStatusDeduction"] = "Edit";				
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
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				string strID="";
				for(int i=0;i<grdDeduction.Items.Count;i++)
				{
					if(((CheckBox)grdDeduction.Items[i].FindControl("chkSelect")).Checked==true)
					{
						strID += grdDeduction.Items[i].Cells[0].Text.Trim() + "$";
					}
				}
				clsCommon.DeleteListRecord("PR_spfrmDEDUCTION","DeductionID",SqlDbType.NVarChar,12,strID);
				btnAddNew_Click(null,null);
				LoadDataGrid();
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}

		private void grdDeduction_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			try
			{
				LoadDataGrid();
				grdDeduction.CurrentPageIndex=e.NewPageIndex;
				grdDeduction.DataBind();
			}
			catch(Exception ex)
			{
				string str = ex.Message;
			}
		}

		private void btnExport_Click(object sender, System.EventArgs e)
		{
			iHRPCore.DataGridExcelExporter myExcelXport=new DataGridExcelExporter(grdDeduction);			
			myExcelXport.Export("");
			myExcelXport =null;
		}

		private void btnRowNumber_ServerClick(object sender, System.EventArgs e)
		{
			grdDeduction.PageSize = Convert.ToInt32(txtPageRows.Text.Trim()==""?"1":txtPageRows.Text.Trim());
			grdDeduction.CurrentPageIndex = 0;
			LoadDataGrid();
		}

		#region Hau

		static public int iMethodID;
		static public String strLSDeductionCode;

		private void Save_ChangeLog()
		{	
			String strUserName = Session[0].ToString().Trim();
			String strActionTime = DateTime.Now.ToString();
			String strAction = Session["ssStatusDeduction"].ToString().Trim();
			String strEmpID = Session["EmpID"].ToString();
			DataTable tbl = clsHREmpList.GetEmpList(Session["EmpID"].ToString().Trim(),this.Page,"0");
			String strEmpName = tbl.Rows[0]["EmpName"].ToString();
			String[][] strFieldChanged;
			strFieldChanged = new String[3][];
			strFieldChanged[0] = new String[7];
			strFieldChanged[1] = new String[7];
			strFieldChanged[2] = new String[7];
			int i = 0;
			if((strAction.ToUpper().Trim().Equals("EDIT") && iMethodID != optLSMethodID.SelectedIndex)
				|| (strAction.ToUpper().Trim().Equals("ADDNEW")))
			{
				strFieldChanged[0][i] = "Method";
				strFieldChanged[1][i] = optLSMethodID.Items[iMethodID].Text;
				strFieldChanged[2][i] = optLSMethodID.SelectedItem.Text;
				i++;
			}
			
			if((strAction.ToUpper().Trim().Equals("EDIT") && !txtFromMonth.ToolTip.Trim().Equals(txtFromMonth.Text.Trim()))
				|| strAction.ToUpper().Trim().Equals("ADDNEW")&& !txtFromMonth.Text.Trim().Equals(""))
			{
				strFieldChanged[0][i] = "From Month";
				strFieldChanged[1][i] = txtFromMonth.ToolTip;
				strFieldChanged[2][i] = txtFromMonth.Text;
				i++;
			}
			if((strAction.ToUpper().Trim().Equals("EDIT") && !txtToMonth.ToolTip.Trim().Equals(txtToMonth.Text.Trim())) 
				|| strAction.ToUpper().Trim().Equals("ADDNEW") && !txtToMonth.Text.Trim().Equals(""))
			{
				strFieldChanged[0][i] = "To Month";
				strFieldChanged[1][i] = txtToMonth.ToolTip;
				strFieldChanged[2][i] = txtToMonth.Text;
				i++;
			}
			
					
			if((strAction.ToUpper().Trim().Equals("EDIT") && !strLSDeductionCode.Trim().Equals(cboLSDeductionID.SelectedItem.Text.Trim())) 
				|| strAction.ToUpper().Trim().Equals("ADDNEW") && !cboLSDeductionID.SelectedItem.Text.Trim().Equals(""))
			{
				strFieldChanged[0][i] = "Type";
				strFieldChanged[1][i] = strLSDeductionCode;
				strFieldChanged[2][i] = cboLSDeductionID.SelectedItem.Text;
				i++;
			}
			if((strAction.ToUpper().Trim().Equals("EDIT") && !txtAmountPerMonth.ToolTip.Trim().Equals(txtAmountPerMonth.Text.Trim()))
				|| (strAction.ToUpper().Trim().Equals("ADDNEW") && !txtAmountPerMonth.Text.Trim().Equals("")))
			{
				strFieldChanged[0][i] = "Amount/month";
				strFieldChanged[1][i] = txtAmountPerMonth.ToolTip;
				strFieldChanged[2][i] = txtAmountPerMonth.Text;
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
