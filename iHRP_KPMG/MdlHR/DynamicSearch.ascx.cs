namespace iHRPCore.MdlHR
{
	using System;
	using System.Configuration;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.Com;
	using iHRPCore.HRComponent;
	using iHRPCore.Include;
	using System.Data.SqlClient;


	/// <summary>
	///		Summary description for DynamicSearch.
	/// </summary>
	public class DynamicSearch : System.Web.UI.UserControl
	{
		#region Declare
		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.DropDownList cboCriteria;
		protected System.Web.UI.WebControls.Button cmdAdd;
		protected System.Web.UI.WebControls.Button btRemove;
		protected System.Web.UI.WebControls.TextBox txtFillCondition;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.DropDownList cboCondition;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList cboInfo;
		protected System.Web.UI.WebControls.TextBox txtDate;
		protected System.Web.UI.WebControls.TextBox txtNumber;
		protected System.Web.UI.WebControls.TextBox txtInfo;
		protected System.Web.UI.WebControls.RadioButton rdTrue;
		protected System.Web.UI.WebControls.RadioButton rdFalse;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.RadioButton rdAnd;
		protected System.Web.UI.WebControls.RadioButton rdOr;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.ListBox lsbColumnsView;
		protected System.Web.UI.WebControls.Button cmdList;
		protected System.Web.UI.WebControls.Button cmdExport;
		protected System.Web.UI.WebControls.Label lblCountEmp;
		protected System.Web.UI.WebControls.DataGrid dtgEmp;
		protected System.Web.UI.HtmlControls.HtmlGenericControl divBool;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtColType;
		protected EmpHeaderSearch EmpHeaderSearch1;
		#endregion
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(!Page.IsPostBack)
			{
				LoadDefaultValue();
				//this.BindDataGrid();
			}
			this.cmdAdd.Attributes.Add("OnClick","return CheckBeforeAddCondition()");
		}

		private void LoadDefaultValue()
		{
			clsCommon.LoadDropDownListControl(this.cboCriteria,"HR_spfrmDynamicSearch 'GetCriteria'","CriteriaID","Criteria",true);
			clsCommon.LoadDropDownListControl(this.cboCondition,"HR_spfrmDynamicSearch 'GetOperator'","OperatorID","Operator",true);
			lsbColumnsView.DataSource = clsCommon.GetDataTable("HR_spfrmDynamicSearch 'GetColumnView'");
			lsbColumnsView.DataTextField = "ColumnView";
			lsbColumnsView.DataValueField = "ColumnViewID";
			this.lsbColumnsView.DataBind();
			ViewColumnDefault();
		}

		private void ViewColumnDefault()
		{
			DataTable dtb = clsCommon.GetDataTable("HR_spfrmDynamicSearch 'GetColumnView'");
			for(int i=0; i<lsbColumnsView.Items.Count; i++)
			{
				if (dtb.Select("ViewDefault = 1 and ColumnViewID='" + lsbColumnsView.Items[i].Value.Trim() + "'").Length > 0)
					lsbColumnsView.Items[i].Selected = true;

			}
		}

		private void BindDataGrid()
		{
			string strEmpID = EmpHeaderSearch1.txtEmpID.Text.Trim();
			string strEmpName = EmpHeaderSearch1.txtEmpName.Text.Trim();
			string strLevel1 = EmpHeaderSearch1.cboLevel1.SelectedValue.Trim();
			string strLevel2 = EmpHeaderSearch1.cboLevel2.SelectedValue.Trim();
			string strLevel3 = EmpHeaderSearch1.cboLevel3.SelectedValue.Trim();
			string strLocation = EmpHeaderSearch1.cboLocation.SelectedValue.Trim();
			string strJobCode = EmpHeaderSearch1.cboJobcode.SelectedValue.Trim();
			string strPosition = EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue.Trim();
			string strCompany = EmpHeaderSearch1.cboCompany.SelectedValue.Trim();
			string strStatus = EmpHeaderSearch1.optStatus.SelectedValue.Trim();

			string strCondition = "(EmpCode = ''" + strEmpID + "'' or ''" + strEmpID + "'' = '''')"
				+ " and (EmpName = N''" + strEmpName + "'' or ''" + strEmpName + "'' = '''')"
				+ " and (LSLevel1Code = N''" + strLevel1 + "'' or ''" + strLevel1 + "'' = '''')"
				+ " and (LSLevel2Code = N''" + strLevel2 + "'' or ''" + strLevel2 + "'' = '''')"
				+ " and (LSLevel3Code = N''" + strLevel3 + "'' or ''" + strLevel3 + "'' = '''')"
				+ " and (LSLocationCode = N''" + strLocation + "'' or ''" + strLocation + "'' = '''')"
				+ " and (LSJobCodeCode = N''" + strJobCode + "'' or ''" + strJobCode + "'' = '''')"
				+ " and (LSPositionCode = N''" + strPosition + "'' or ''" + strEmpName + "'' = '''')"
				+ " and (LSCompanyCode = N''" + strCompany + "'' or ''" + strCompany + "'' = '''')"
				+ " and (Status=''" + strStatus + "'' or ''" + strStatus + "''='''')";
			DataTable rsData = new DataTable();
			try
			{
				string strSql = "HR_spfrmDynamicSearch 'GetListEmpGeneral'";
				if (this.txtFillCondition.Text.Trim() != "")
					strSql += ",@Condition=N'" + strCondition + " and " + this.txtFillCondition.Text.Trim() + "'";
				else
					strSql += ",@Condition=N'" + strCondition + "'";
				Session["SqlExport"] = strSql;
				rsData = clsCommon.GetDataTable(strSql);
				this.dtgEmp.DataSource = rsData;
				this.dtgEmp.DataBind();
				this.lblCountEmp.Text = "Tổng số: " + rsData.Rows.Count.ToString() + " (nhân viên)";
				rsData.Dispose();
			}
			catch(Exception exp)
			{
				string strError = exp.Message.ToString();
				this.lblError.Text = "Filter condition is invalid!";
				this.dtgEmp.DataSource = new DataTable();
				this.dtgEmp.DataBind();
			}
			finally
			{
				rsData.Dispose();
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
			this.cboCriteria.SelectedIndexChanged += new System.EventHandler(this.cboCriteria_SelectedIndexChanged);
			this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
			this.btRemove.Click += new System.EventHandler(this.btRemove_Click);
			this.cmdList.Click += new System.EventHandler(this.cmdList_Click);
			this.cmdExport.Click += new System.EventHandler(this.cmdExport_Click);
			this.dtgEmp.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgEmp_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		private void ClearControls()
		{
			this.txtInfo.Text = "";
			this.txtDate.Text = "";
			this.txtNumber.Text = "";
			this.txtFillCondition.Text = "";
			this.cboCriteria.SelectedValue = "";
			this.cboInfo.Items.Clear();
			this.cboInfo.Visible = false;
			this.txtInfo.Visible = true;
			this.txtDate.Visible = false;
			this.txtNumber.Visible = false;
			this.divBool.Attributes.Add("style","display:none");
		}

		private void cboCriteria_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				DataTable rsData = clsCommon.GetDataTable("HR_spfrmDynamicSearch 'GetcboLS'");
				DataRow[] arrRow = rsData.Select("ColumnName='" + cboCriteria.SelectedValue.Trim() + "'");
				this.txtColType.Value = "";
				if (arrRow.Length > 0)
				{
					string strType = arrRow[0]["xtypeColumn"].ToString().Trim();
					string strTableLS = arrRow[0]["TableLS"].ToString().Trim();
					string strValueField = arrRow[0]["ValueView"].ToString().Trim();
					string strTextField = arrRow[0]["TextView"].ToString().Trim();
					if (strType == "datetime")
					{
						this.txtColType.Value = strType;
						this.txtDate.Visible = true;
						this.txtInfo.Visible = false;
						this.txtNumber.Visible = false;
						this.cboInfo.Visible = false;
						this.divBool.Attributes.Add("style","display:none");
					}
					else if (strType == "bit")
					{
						this.txtColType.Value = strType;
						this.txtDate.Visible = false;
						this.txtInfo.Visible = false;
						this.txtNumber.Visible = false;
						this.cboInfo.Visible = false;
						this.divBool.Attributes.Remove("style");
						string strSelected = cboCriteria.SelectedValue.Trim();
						if (strSelected == "Active")
						{
							rdTrue.Text = "Active";
							rdFalse.Text = "Resign";
						}
						else if (strSelected == "Gender")
						{
							rdTrue.Text = "Nam";
							rdFalse.Text = "Nữ";
						}
						else if (strSelected == "Foreigner")
						{
							rdTrue.Text = "Nước ngoài";
							rdFalse.Text = "Việt Nam";
						}
					}
					else if (strTableLS != "" && strValueField != "" && strTextField != "")
					{
						this.txtColType.Value = "DropDownList";
						this.txtDate.Visible = false;
						this.txtInfo.Visible = false;
						this.txtNumber.Visible = false;
						this.cboInfo.Visible = true;
						clsCommon.LoadDropDownListControl(cboInfo,"Select " + strValueField + ", " + strTextField 
							+ " from " + strTableLS + " Union Select '','' order by " 
							+ strTextField,strValueField,strTextField,true);
					}
					else
					{
						this.txtDate.Visible = false;
						this.txtInfo.Visible = true;
						this.txtNumber.Visible = false;
						this.cboInfo.Visible = false;
						this.divBool.Attributes.Add("style","display:none");
					}
				}
			}
			catch(Exception exp)
			{
				this.lblError.Text = exp.Message.ToString();
			}		
		}

		private void cmdList_Click(object sender, System.EventArgs e)
		{
			int index = 0;
			for(int i=0; i<this.lsbColumnsView.Items.Count; i++)
			{
				this.dtgEmp.Columns[i].Visible = this.lsbColumnsView.Items[i].Selected;	
				if (this.lsbColumnsView.Items[i].Selected)
					index ++;
			}
			if (index == 0)
			{
				ViewColumnDefault();
				for(int i=0; i<this.lsbColumnsView.Items.Count; i++)
				{
					this.dtgEmp.Columns[i].Visible = this.lsbColumnsView.Items[i].Selected;	
				}
			}
			this.BindDataGrid();
			this.ClearControls();		
		}

		private void dtgEmp_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.dtgEmp.CurrentPageIndex = e.NewPageIndex;
			this.BindDataGrid();
			this.dtgEmp.DataBind();		
		}

		private void btRemove_Click(object sender, System.EventArgs e)
		{
			this.lblError.Text = "";
			this.ClearControls();		
		}
		
		private string CheckBeforeAddCondition()
		{
			string strColType = this.txtColType.Value.Trim();
			if ((strColType == "datetime" && txtDate.Text.Trim() == "")
				|| (txtColType.Value.Trim() == "DropDownList" && cboInfo.SelectedValue.Trim() == ""))
			{
				this.lblError.Text = "Input information for filtering!";
				return "";
			}
			else if (strColType != "bit" && strColType != "DropDownList" && strColType != "datetime" && txtInfo.Text.Trim() == "")
			{
				this.lblError.Text = "Input information for filtering!";
				return "";
			}
			string strReturn = "";
			if (strColType == "datetime")
			{
				string[] mDateArr = this.txtDate.Text.Split(new char[]{'/'});
				string mDate = mDateArr[1].Trim() + "/" + mDateArr[0].Trim() + "/" + mDateArr[2].Trim();
				strReturn = this.cboCriteria.SelectedValue.Trim().Substring(cboCriteria.SelectedValue.Trim().LastIndexOf(".") + 1 ,cboCriteria.SelectedValue.Trim().Length - cboCriteria.SelectedValue.Trim().LastIndexOf(".") - 1);
				strReturn += " "
					+ this.cboCondition.SelectedValue.Trim() + " ''" 
					+ mDate + "''";
			}

			else if (strColType == "bit")
			{
				strReturn = this.cboCriteria.SelectedValue.Trim().Substring(cboCriteria.SelectedValue.Trim().LastIndexOf(".") + 1 ,cboCriteria.SelectedValue.Trim().Length - cboCriteria.SelectedValue.Trim().LastIndexOf(".") - 1);
				strReturn += " "
					+ this.cboCondition.SelectedValue.Trim() + " "; 
				if (rdTrue.Checked)
					strReturn = strReturn + "1";
				else strReturn = strReturn + "0";
			}

			else if (strColType == "DropDownList")
			{
				/*strReturn = this.cboCriteria.SelectedValue.Trim() + " "
					+ this.cboCondition.SelectedValue.Trim() + " " + "''" 
					+ cboInfo.SelectedValue.Trim() + "''";*/
				strReturn = this.cboCriteria.SelectedValue.Trim().Substring(cboCriteria.SelectedValue.Trim().LastIndexOf(".") + 1 ,cboCriteria.SelectedValue.Trim().Length - cboCriteria.SelectedValue.Trim().LastIndexOf(".") - 1);
				strReturn += " "
					+ this.cboCondition.SelectedValue.Trim() + " " + "''" 
					+ cboInfo.SelectedValue.Trim() + "''";
			}

			else 
			{
				strReturn = this.cboCriteria.SelectedValue.Trim().Substring(cboCriteria.SelectedValue.Trim().LastIndexOf(".") + 1 ,cboCriteria.SelectedValue.Trim().Length - cboCriteria.SelectedValue.Trim().LastIndexOf(".") - 1);
				strReturn += " "
					+ this.cboCondition.SelectedValue.Trim() + " ";
				if (cboCondition.SelectedValue.Trim() == "Like")
					strReturn = strReturn + "N''%" + txtInfo.Text.Trim() + "%''";
				else
					strReturn = strReturn + "N''" + this.txtInfo.Text.Trim() + "''";
			}
			return strReturn;
		}

		private void cmdAdd_Click(object sender, System.EventArgs e)
		{
			this.lblError.Text = "";
			try
			{
				//CheckBeforeAddCondition();
				string strCondition = "";
				if (txtFillCondition.Text.Trim() == "")
					strCondition = CheckBeforeAddCondition();
				else
				{
					strCondition = this.txtFillCondition.Text.Trim(); 
					if (rdAnd.Checked)
						strCondition += " And ";
					else strCondition += " Or ";
					strCondition += this.CheckBeforeAddCondition();
				}
				this.txtFillCondition.Text = strCondition.Trim();
				
			}
			catch(Exception exp)
			{
				this.lblError.Text = exp.Message.ToString();
			}
		
		}

		private void cmdExport_Click(object sender, System.EventArgs e)
		{
			dtgEmp.AllowPaging = false;
			BindDataGrid();
			iHRPCore.DataGridExcelExporter myExcelXport=new iHRPCore.DataGridExcelExporter(dtgEmp);
			myExcelXport.Export("");
			myExcelXport = null;
			dtgEmp.AllowPaging = true;
			BindDataGrid();		
		}

	}
}
