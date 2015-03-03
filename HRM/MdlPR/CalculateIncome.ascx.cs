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
	using iHRPCore.Include;
	/// <summary>
	///		Summary description for CalculateIncome.
	/// </summary>
	public class CalculateIncome : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.HtmlControls.HtmlTableRow trGrid;
		protected System.Web.UI.WebControls.TextBox txtMonth;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.DropDownList cboTypeSalary;
		protected System.Web.UI.WebControls.DataGrid grdPayroll;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.LinkButton btnView;
		protected System.Web.UI.WebControls.LinkButton btnCalculate;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.HtmlControls.HtmlTable tblHeader;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.LinkButton btnPrint;
		protected System.Web.UI.WebControls.DropDownList cboCompanyID;
		protected System.Web.UI.WebControls.Label lblCompany;
		protected System.Web.UI.WebControls.DropDownList cboLevel1ID;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.Label lblFromDepartment;
		protected System.Web.UI.WebControls.DropDownList cboLevel2ID;
		protected System.Web.UI.WebControls.Label lblFromSection;
		protected System.Web.UI.WebControls.DropDownList cboLevel3ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel1ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel2ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel3ID;
		protected System.Web.UI.WebControls.DropDownList cboEmpType;
		protected System.Web.UI.WebControls.DropDownList cboCurrencyType;
		protected System.Web.UI.WebControls.Label lblPeriod;
		protected System.Web.UI.WebControls.Label lblType;
		protected System.Web.UI.WebControls.Label lblCurrency;
		protected System.Web.UI.WebControls.TextBox txtEmpID;
		protected System.Web.UI.WebControls.Label EmpID;
		public string strLanguage = "EN";
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
			if(!Page.IsPostBack)
			{
				txtMonth.Text = DateTime.Today.ToString("MM/yyyy");
				string strTextField = strLanguage == "VN"?"VNName":"Name";
				clsCommon.LoadDropDownListControl(cboCompanyID,"sp_GetDataCombo @TableName='LS_tblCompany',@Fields='LSCompanyCode," + strTextField + " as Name'","LSCompanyCode","Name",true);			
			}
			btnCalculate.Attributes.Add("OnClick", " return validform()");			
			btnView.Attributes.Add("OnClick", " return validform()");
			btnDelete.Attributes.Add("OnClick", "return checkdelete()");			
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
			this.cboLevel2ID.SelectedIndexChanged += new System.EventHandler(this.cboLevel2ID_SelectedIndexChanged);
			this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
			this.btnView.Click += new System.EventHandler(this.btnView_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			this.grdPayroll.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.grdPayroll_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnCalculate_Click(object sender, System.EventArgs e)
		{
			DataTable dtPayroll = new DataTable();
			try
			{
				string strEmpID = txtEmpID.Text.Trim();
				string strEmpName = "";
				string strCompany = cboCompanyID.SelectedValue.Trim();
				string strLevel1 = cboLevel1ID.SelectedValue.Trim();
				string strLevel2 = cboLevel2ID.SelectedValue.Trim();
				string strLevel3 = cboLevel3ID.SelectedValue.Trim();
				string strLocation = "";
				string strJobCode = "";
				string strPosition = "";				
				string strStatus = "";
				string strReturn="";
				string strEmpType= cboEmpType.SelectedValue.Trim();
				string strCurType= cboCurrencyType.SelectedValue.Trim();
				switch (cboTypeSalary.SelectedIndex)
				{
					case 0:
						strReturn = clsPayroll.CalculateIncome(txtMonth.Text.Trim(),strEmpID,strLevel1,strLevel2
							,strLevel3,strCompany,strEmpName,strPosition,strJobCode,strLocation,strStatus,strEmpType,strCurType);
						btnView_Click(null,null);
						break;
					case 1:
						strReturn = clsPayroll.CalculateMeternity(txtMonth.Text.Trim(),strEmpID,strLevel1,strLevel2
							,strLevel3,strCompany,strEmpName,strPosition,strJobCode,strLocation,strStatus);						
						btnView_Click(null,null);
						break;
					case 2:
						strReturn = clsPayroll.CalculateInCome13th(txtMonth.Text.Trim(),strEmpID,strLevel1,strLevel2
							,strLevel3,strCompany,strEmpName,strPosition,strJobCode,strLocation,strStatus,strEmpType,strCurType);
						btnView_Click(null,null);
						break;
					case 3:
						strReturn = clsPayroll.CalculateInCome14th(txtMonth.Text.Trim(),strEmpID,strLevel1,strLevel2
							,strLevel3,strCompany,strEmpName,strPosition,strJobCode,strLocation,strStatus,strEmpType,strCurType);						
						btnView_Click(null,null);
						break;
					default:
						break;
				}
				if(strReturn.Trim() =="")
					lblErr.Text = "Calculate completed!";
				else
					lblErr.Text = strReturn;
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
			finally
			{
				dtPayroll.Dispose();
			}
		}
		private void ChangeOption()
		{
		}		
		/// <summary>
		/// VIEW 
		/// </summary>		
		private void btnView_Click(object sender, System.EventArgs e)
		{
			DataTable dtPayroll = new DataTable();
			try
			{
				string strEmpID = txtEmpID.Text.Trim();
				string strEmpName = "";
				string strLevel1 = cboLevel1ID.SelectedValue.Trim();
				string strLevel2 = cboLevel2ID.SelectedValue.Trim();
				string strLevel3 = cboLevel3ID.SelectedValue.Trim();
				string strLocation = "";
				string strJobCode = "";
				string strPosition = "";
				string strCompany = cboCompanyID.SelectedValue.Trim();
				string strStatus = "";
				string strEmpType= cboEmpType.SelectedValue.Trim();
				string strCurType= cboCurrencyType.SelectedValue.Trim();
				switch (cboTypeSalary.SelectedIndex)
				{
					case 0:
						dtPayroll = clsPayroll.GetPayRollSchedule(txtMonth.Text.Trim(),strEmpID,strLevel1,strLevel2
							,strLevel3,strCompany,strEmpName,strPosition,strJobCode,strLocation,strStatus,strEmpType,strCurType);
						grdPayroll.Columns[5].HeaderText = "No. of day worked";
						break;
					case 1:
						dtPayroll = clsPayroll.GetPayRollMeternity(txtMonth.Text.Trim(),strEmpID,strLevel1,strLevel2
							,strLevel3,strCompany,strEmpName,strPosition,strJobCode,strLocation,strStatus);
						grdPayroll.Columns[5].HeaderText = "No. of months";
						break;
					case 2:
						dtPayroll = clsPayroll.GetPayRoll13th(txtMonth.Text.Trim(),strEmpID,strLevel1,strLevel2
							,strLevel3,strCompany,strEmpName,strPosition,strJobCode,strLocation,strStatus,strEmpType,strCurType);
						grdPayroll.Columns[5].HeaderText = "No. of months";
						break;
					case 3:
						dtPayroll = clsPayroll.GetPayRoll14th(txtMonth.Text.Trim(),strEmpID,strLevel1,strLevel2
							,strLevel3,strCompany,strEmpName,strPosition,strJobCode,strLocation,strStatus,strEmpType,strCurType);
						grdPayroll.Columns[5].HeaderText = "No. of day worked";
						break;
					default:
						break;
				}
				grdPayroll.DataSource = dtPayroll;
				grdPayroll.CurrentPageIndex = 0;
				grdPayroll.DataBind();
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
			finally
			{
				dtPayroll.Dispose();
			}
		}
//DELETE RECORD
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				string strID="";
				for(int i=0;i<grdPayroll.Items.Count;i++)
				{
					if(((CheckBox)grdPayroll.Items[i].FindControl("chkSelect")).Checked==true)
					{
						strID += grdPayroll.Items[i].Cells[0].Text.Trim() + "$";
					}
				}
				clsCommon.DeleteListRecord("PR_spfrmGetSalaryIncome","ID",SqlDbType.Int,4,strID);
				btnView_Click(null,null);
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}
		private void grdPayroll_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			try
			{
				btnView_Click(null,null);
				grdPayroll.CurrentPageIndex=e.NewPageIndex;
				grdPayroll.DataBind();
			}
			catch(Exception ex)
			{
				string str = ex.Message;
			}
		}
		private void btnPrint_Click(object sender, System.EventArgs e)
		{	

			//return;
			string strParams ="";
			string strValues = "";

			//////   Get params and Get Value of params
				
			strParams = "@Activity;@SalPeriod;@EmpID;@EmpName;@LSCompanyCode;@LSLevel1Code;@LSLevel2Code;@LSLevel3Code;@LSPositionCode;@LSJobCodeCode;@LSLocationCode;@CurrencyType";

			strValues = txtMonth.Text.Trim();
			strValues += ";" + txtEmpID.Text.Trim();
			strValues += ";" + "";
			strValues += ";" + cboCompanyID.SelectedValue.Trim();
			strValues += ";" + cboLevel1ID.SelectedValue.Trim();
			strValues += ";" + cboLevel2ID.SelectedValue.Trim();
			strValues += ";" + cboLevel3ID.SelectedValue.Trim();
			strValues += ";" + "";
			strValues += ";" + "";
			strValues += ";" + "";			
			//strValues += ";" + cboCurrencyType.SelectedValue.Trim();
			//------- LOAI NHAN VIEN
			string strEmptype = "";
			switch (cboEmpType.SelectedValue.Trim())
			{
				case "0":
					strEmptype = "GetDataNV;";
					break;
				case "1":
					strEmptype = "GetDataQL;";
					break;
				case "2":
					strEmptype = "GetDataRef;";
					break;
				case "3":
					strEmptype = "GetDataFR;";
					break;
			}
			//----
			switch (cboTypeSalary.SelectedIndex)
			{
				case 0: //luong thang
					strParams +=";@Status";
					strValues = strEmptype + strValues;
					strValues += ";" + cboCurrencyType.SelectedValue.Trim() + ";0"; //currency type
					Session["ssReportName"] = "PR/PR_rptPayrollSchedule.rpt";					
					break;
				case 1: // thai san
					strParams = "@ID;@Activity;@SalPeriod;@EmpID;@EmpName;@LSCompanyCode;@LSLevel1Code;@LSLevel2Code;@LSLevel3Code;@LSPositionCode;@LSJobCodeCode;@LSLocationCode;@Status";
					strValues = "0;GetPayRollMaternity;" + strValues + ";0";
					Session["ssReportName"] = "PR_rptPayrollMaternity.rpt";
					break;
				case 2:// thang 13
					strParams +=";@Status";
					strValues = strEmptype + strValues + ";" + cboCurrencyType.SelectedValue.Trim() + ";0"; //currency type
					Session["ssReportName"] = "PR_rptPayrollSchedule13th.rpt";
					break;
				case 3: // thang 14
					strParams +=";@Status";
					strValues = strEmptype + strValues + ";" + cboCurrencyType.SelectedValue.Trim() + ";0"; //currency type
					Session["ssReportName"] = "PR_rptPayrollSchedule14th.rpt";
					break;
				default:
					break;
			}
			Session["ssReportParams"] = strParams;
			Session["ssReportValues"] = strValues;
			clsCommon.OpenNewWindow(this.Page,"Reports/ShowReports.aspx","Payroll");
		}

		private void cboCompanyID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				string strCompanyID = cboCompanyID.SelectedValue.Trim();
				string strTextField = strLanguage == "VN"?"VNName":"Name";			
				clsCommon.LoadDropDownListControl(cboLevel1ID,"sp_GetDataCombo @TableName='LS_tblLevel1',@Fields='LSLevel1Code," + strTextField + " as Name',@Where=' and LSCompanyCode=N''" + strCompanyID.Trim() + "'''","LSLevel1Code","Name",true);
				cboLevel1ID.SelectedValue = this.txtLevel1ID.Value.Trim();
				cboLevel1ID_SelectedIndexChanged(null,null);
			}
			catch(Exception ex)
			{				
			}
		}

		private void cboLevel1ID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				string strLevel1ID = cboLevel1ID.SelectedValue.Trim();
				string strTextField = strLanguage == "VN"?"VNName":"Name";
				clsCommon.LoadDropDownListControl(cboLevel2ID,"sp_GetDataCombo @TableName='LS_tblLevel2',@Fields='LSLevel2Code," + strTextField + " as Name',@Where=' and LSLevel1Code=N''" + strLevel1ID.Trim() + "'''","LSLevel2Code","Name",true);
				cboLevel2ID.SelectedValue = this.txtLevel2ID.Value.Trim();
				cboLevel2ID_SelectedIndexChanged(null,null);
			}
			catch(Exception ex)
			{
			}
		}

		private void cboLevel2ID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				string strLevel2ID = cboLevel2ID.SelectedValue.Trim();
				string strTextField = strLanguage == "VN"?"VNName":"Name";
				clsCommon.LoadDropDownListControl(cboLevel3ID,"sp_GetDataCombo @TableName='LS_tblLevel3',@Fields='LSLevel3Code," + strTextField + " as Name',@Where=' and LSLevel2Code=N''" + strLevel2ID.Trim() + "'''","LSLevel3Code","Name",true);
				cboLevel3ID.SelectedValue = this.txtLevel3ID.Value.Trim();
			}
			catch(Exception ex)
			{				
			}
		}
	}
}
