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
	///		Summary description for DynamicEmpList.
	/// </summary>
	public class DynamicEmpList : System.Web.UI.UserControl
	{
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
		protected System.Web.UI.HtmlControls.HtmlGenericControl divBool;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtColType;
		protected System.Web.UI.WebControls.Button btnRemoveAll;
		protected EmpHeaderSearch EmpHeaderSearch1;
		protected System.Web.UI.WebControls.DataGrid dtgEmp;
		protected System.Web.UI.WebControls.TextBox txtFillConditionText;
		public string strLanguage="VN";

		private void Page_Load(object sender, System.EventArgs e)
		{
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";		
			if(!Page.IsPostBack)
			{
				LoadDefaultValue();
				//this.BindDataGrid();
				Session["DivBool"]=0;
			}
			this.cmdAdd.Attributes.Add("OnClick","return CheckBeforeAddCondition()");
		}

		private void LoadDefaultValue()
		{
			clsCommon.LoadDropDownListControl(this.cboCriteria,"HR_spfrmDynamicEmpList 'GetCriteria'","CriteriaID","Criteria",true);
			clsCommon.LoadDropDownListControl(this.cboCondition,"HR_spfrmDynamicEmpList 'GetOperator'","OperatorID","Operator",true);
			lsbColumnsView.DataSource = clsCommon.GetDataTable("HR_spfrmDynamicEmpList 'GetColumnView',@Language='" + strLanguage + "'");
			lsbColumnsView.DataTextField = "ColumnView";
			lsbColumnsView.DataValueField = "ColumnViewID";
			this.lsbColumnsView.DataBind();
			ViewColumnDefault();
		}

		private void ViewColumnDefault()
		{
			DataTable dtb = clsCommon.GetDataTable("HR_spfrmDynamicEmpList 'GetColumnView'");
			for(int i=0; i<lsbColumnsView.Items.Count; i++)
			{
				if (dtb.Select("ViewDefault = 1 and ColumnViewID='" + lsbColumnsView.Items[i].Value.Trim() + "'").Length > 0)
					lsbColumnsView.Items[i].Selected = true;

			}
		}

		private void BindDataGrid()
		{
			string strEmpID = EmpHeaderSearch1.txtEmpID.Text.Trim().Replace("'", "");			
			string strEmpName = EmpHeaderSearch1.txtEmpName.Text.Trim().Replace("'", "");
			string strLevel1 = EmpHeaderSearch1.cboLevel1.SelectedValue.Trim();
			string strLevel2 = EmpHeaderSearch1.cboLevel2.SelectedValue.Trim();
			string strLevel3 = EmpHeaderSearch1.cboLevel3.SelectedValue.Trim();
			string strLocation = EmpHeaderSearch1.cboLocation.SelectedValue.Trim();
			string strJobCode = EmpHeaderSearch1.cboJobcode.SelectedValue.Trim();
			string strPosition = EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue.Trim();
			string strCompany = EmpHeaderSearch1.cboCompany.SelectedValue.Trim();
			string strStatus = EmpHeaderSearch1.optStatus.SelectedValue.Trim();
			/*if (strStatus=="0") 
				strStatus="2";			
			else*/ if (strStatus=="2") 
				strStatus="";
			
			string strCondition = "(EmpCode like ''%" + strEmpID + "%'' or ''" + strEmpID + "'' = '''')"
								+ " and (EmpName like N''%" + strEmpName + "%'' or ''" + strEmpName + "'' = '''')"
								+ " and (LSLevel1ID = ''" + strLevel1 + "'' or ''" + strLevel1 + "'' = '''')"
								+ " and (LSLevel2ID = ''" + strLevel2 + "'' or ''" + strLevel2 + "'' = '''')"
								+ " and (LSLevel3ID = ''" + strLevel3 + "'' or ''" + strLevel3 + "'' = '''')"
								+ " and (LSLocationID = ''" + strLocation + "'' or ''" + strLocation + "'' = '''')"
								+ " and (LSJobCodeID = ''" + strJobCode + "'' or ''" + strJobCode + "'' = '''')"
								+ " and (LSPositionID = ''" + strPosition + "'' or ''" + strPosition + "'' = '''')"
								+ " and (LSCompanyID = ''" + strCompany + "'' or ''" + strCompany + "'' = '''')"
								+ " and (Status=''" + strStatus + "'' or ''" + strStatus + "''='''')";


			/*string strCondition = "(EmpCode = ''" + strEmpID + "'' or ''" + strEmpID + "'' = '''')"
								+ " and (EmpName = N''" + strEmpName + "'' or ''" + strEmpName + "'' = '''')"
								+ " and (LSLevel1ID = ''" + strLevel1 + "'' or ''" + strLevel1 + "'' = '''')"
								+ " and (LSLevel2ID = ''" + strLevel2 + "'' or ''" + strLevel2 + "'' = '''')"
								+ " and (LSLevel3ID = ''" + strLevel3 + "'' or ''" + strLevel3 + "'' = '''')"
								+ " and (LSLocationID = ''" + strLocation + "'' or ''" + strLocation + "'' = '''')"
								+ " and (LSJobCodeID = ''" + strJobCode + "'' or ''" + strJobCode + "'' = '''')"
								+ " and (LSPositionID = ''" + strPosition + "'' or ''" + strEmpName + "'' = '''')"
								+ " and (LSCompanyID = ''" + strCompany + "'' or ''" + strCompany + "'' = '''')"
								+ " and (Status=''" + strStatus + "'' or ''" + strStatus + "''='''')";*/
			DataTable rsData = new DataTable();
			try
			{
				string strSql = "HR_spfrmDynamicEmpList 'GetListEmpGeneral'";
				if (this.txtFillCondition.Text.Trim() != "")
					strSql += ",@Condition=N'" + strCondition + " and " + this.txtFillCondition.Text.Trim() + "'";
				else
					strSql += ",@Condition=N'" + strCondition + "'";
				Session["SqlExport"] = strSql;
				rsData = clsCommon.GetDataTable(strSql);
				this.dtgEmp.DataSource = rsData;
				this.dtgEmp.CurrentPageIndex = 0;
				this.dtgEmp.DataBind();
				this.lblCountEmp.Text = (strLanguage=="EN"?"Total: ":"Tổng cộng :") + rsData.Rows.Count.ToString() + (strLanguage=="EN"?" (records)":" hàng");
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.cboCriteria.SelectedIndexChanged += new System.EventHandler(this.cboCriteria_SelectedIndexChanged);
			this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
			this.btRemove.Click += new System.EventHandler(this.btRemove_Click);
			this.btnRemoveAll.Click += new System.EventHandler(this.btnRemoveAll_Click);
			this.txtDate.TextChanged += new System.EventHandler(this.txtDate_TextChanged);
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
			//this.txtFillCondition.Text = "";
			this.cboCriteria.SelectedValue = "";
			this.cboInfo.Items.Clear();
			this.cboInfo.Visible = false;
			this.txtInfo.Visible = true;
			//this.txtDate.Visible = false;
			this.txtNumber.Visible = false;
			txtFillCondition.Text="";
			txtFillConditionText.Text="";
			this.divBool.Attributes.Add("style","display:none");
			Session["DivBool"]=0;
		}

		private void cboCriteria_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				DataTable rsData = clsCommon.GetDataTable("HR_spfrmDynamicEmpList 'GetcboLS'");
				DataRow[] arrRow = rsData.Select("ColumnName='" + cboCriteria.SelectedValue.Trim() + "'");
				this.txtColType.Value = "";
				if (arrRow.Length > 0)
				{
					string strType = arrRow[0]["xtypeColumn"].ToString().Trim();
					string strTableLS = arrRow[0]["TableLS"].ToString().Trim();
					string strValueField = arrRow[0]["ValueView"].ToString().Trim();
					string strTextField =  arrRow[0]["TextView"].ToString().Trim();
					if (strType == "datetime")
					{
						this.txtColType.Value = strType;
						this.txtDate.Visible = true;
						//this.txtDate.Attributes.Add("style","display:block");
						this.txtInfo.Visible = false;
						this.txtNumber.Visible = false;
						this.cboInfo.Visible = false;
						this.divBool.Attributes.Add("style","display:none");
						Session["DivBool"]=0;
					}
					else if (strType == "bit")
					{
						this.txtColType.Value = strType;
						this.txtDate.Visible = false;
						//this.txtDate.Attributes.Add("style","display:none");
						this.txtInfo.Visible = false;
						this.txtNumber.Visible = false;
						this.cboInfo.Visible = false;
						this.divBool.Attributes.Remove("style");
						Session["DivBool"]=1;
						string strSelected = cboCriteria.SelectedValue.Trim();
						if (strSelected == "Active")
						{
							rdTrue.Text = strLanguage=="EN"?"Active":"Hiện diện";
							rdFalse.Text = strLanguage=="EN"?"Resign":"Nghỉ việc";
						}
						else if (strSelected == "Gender")
						{
							rdTrue.Text = strLanguage=="EN"?"Male":"Nam";
							rdFalse.Text = strLanguage=="EN"?"Female":"Nữ";
						}
						else if (strSelected == "Foreigner")
						{
							rdTrue.Text = strLanguage=="EN"?"Foreigner":"Nước ngoài";
								rdFalse.Text = strLanguage=="EN"?"VietNamese":"Việt Nam";
						}
					}
					else if (strTableLS != "" && strValueField != "" && strTextField != "")
					{
						this.txtColType.Value = "DropDownList";
						this.txtDate.Visible = false;
						//this.txtDate.Attributes.Add("style","display:none");
						this.txtInfo.Visible = false;
						this.txtNumber.Visible = false;
						this.cboInfo.Visible = true;
						this.divBool.Attributes.Add("style","display:none");
						Session["DivBool"]=0;
						string strPrefix=strLanguage=="VN"? "VN" : "" ;
						strTextField=strPrefix + strTextField;
						clsCommon.LoadDropDownListControl(cboInfo,"Select " + strValueField + ", " + strTextField 
							+ " from " + strTableLS + " Union Select '','' order by " 
							+ strTextField,strValueField,strTextField,true);
					}
					else
					{
						this.txtDate.Visible = false;
						//this.txtDate.Attributes.Add("style","display:none");
						this.txtInfo.Visible = true;
						this.txtNumber.Visible = false;
						this.cboInfo.Visible = false;
						this.divBool.Attributes.Add("style","display:none");
						Session["DivBool"]=0;
					}
				}
			}
			catch(Exception exp)
			{
				clsChangeLang.popupWindowExp(this,exp);
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
			//this.ClearControls();
			if (Session["DivBool"].ToString()=="1") this.divBool.Attributes.Add("style","display:block");
		}

		private void dtgEmp_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.BindDataGrid();
			this.dtgEmp.CurrentPageIndex = e.NewPageIndex;
			this.dtgEmp.DataBind();
			if (Session["DivBool"].ToString()=="1") this.divBool.Attributes.Add("style","display:block");
		}

		private void btRemove_Click(object sender, System.EventArgs e)
		{
			//Xữ lý ẩn bên dưới
			if (txtFillCondition.Text=="") return;
			string strFillCondition=txtFillCondition.Text;
			if (strFillCondition.LastIndexOf("Or")!= -1)
			{
				if (strFillCondition.LastIndexOf("Or")!= -1)
					strFillCondition = strFillCondition.Substring(0, strFillCondition.LastIndexOf("Or"));
				else
					strFillCondition = "";				
			}
			else
			{
				if (strFillCondition.LastIndexOf("And")!= -1)
					strFillCondition = strFillCondition.Substring(0, strFillCondition.LastIndexOf("And"));
				else
					strFillCondition = "";				
			}
			txtFillCondition.Text=strFillCondition;
			//Xữ lý text hiển thị
			string sOrReplace = strLanguage=="EN"? "Or" : "Hoặc" ;
			string sAndReplace = strLanguage=="EN"? "And" : "Và" ;
			if (txtFillConditionText.Text=="") return;
			string strFillConditionText=txtFillConditionText.Text;
			if (strFillConditionText.LastIndexOf(sOrReplace)!= -1)
			{
				if (strFillConditionText.LastIndexOf(sOrReplace)!= -1)
					strFillConditionText = strFillConditionText.Substring(0, strFillConditionText.LastIndexOf(sOrReplace));
				else
					strFillCondition = "";				
			}
			else
			{
				if (strFillConditionText.LastIndexOf(sAndReplace)!= -1)
					strFillConditionText = strFillConditionText.Substring(0, strFillConditionText.LastIndexOf(sAndReplace));
				else
					strFillConditionText = "";				
			}
			txtFillConditionText.Text=strFillConditionText;


			if (Session["DivBool"].ToString()=="1") this.divBool.Attributes.Add("style","display:block");
		}

		private string CheckBeforeAddConditionText()
		{	
			string strColType = this.txtColType.Value.Trim();
			string strReturn = "";
			if (strColType == "datetime")
			{
				string mDateArr = this.txtDate.Text;				
				strReturn = this.cboCriteria.SelectedItem.Text;
				strReturn += " "
					+ this.cboCondition.SelectedValue.Trim() + " '" 
					+ this.txtDate.Text + "'";
			}

			else if (strColType == "bit")
			{
				strReturn = this.cboCriteria.SelectedItem.Text;
				strReturn += " "
					+ this.cboCondition.SelectedValue.Trim() + " "; 				
				if (rdTrue.Checked)
					strReturn = strReturn + rdTrue.Text;
				else strReturn = strReturn + rdFalse.Text;
				
			}

			else if (strColType == "DropDownList")
			{
				/*strReturn = this.cboCriteria.SelectedValue.Trim() + " "
					+ this.cboCondition.SelectedValue.Trim() + " " + "''" 
					+ cboInfo.SelectedValue.Trim() + "''";*/
				strReturn = this.cboCriteria.SelectedItem.Text;
				strReturn += " "
					+ this.cboCondition.SelectedValue.Trim() + " " + "'" 
					+ cboInfo.SelectedItem.Text + "'";
			}

			else 
			{
				strReturn = this.cboCriteria.SelectedItem.Text;
				strReturn += " "
					+ this.cboCondition.SelectedValue.Trim() + " ";
				if (cboCondition.SelectedValue.Trim() == "Like")
					strReturn = strReturn + "'" + txtInfo.Text.Trim() + "%'";
				else
					strReturn = strReturn + "'" + this.txtInfo.Text.Trim() + "''";
			}
			return strReturn;
		}
		private string CheckBeforeAddCondition()
		{
			string strColType = this.txtColType.Value.Trim();
			if ((strColType == "datetime" && txtDate.Text.Trim() == "")
				|| (txtColType.Value.Trim() == "DropDownList" && cboInfo.SelectedValue.Trim() == ""))
			{
				clsChangeLang.popupWindow(this,"CL_0003",strLanguage,"",0);
				return "";
			}
			else if (strColType != "bit" && strColType != "DropDownList" && strColType != "datetime" && txtInfo.Text.Trim() == "")
			{
				clsChangeLang.popupWindow(this,"CL_0003",strLanguage,"",0);
				return "";
			}
			string strReturn = "";
			string mDate;
			if (strColType == "datetime")
			{
				string[] mDateArr = this.txtDate.Text.Split(new char[]{'/'});
				try
				{
					mDate = mDateArr[1].Trim() + "/" + mDateArr[0].Trim() + "/" + mDateArr[2].Trim();
				}
				catch
				{
					clsChangeLang.popupWindow(this,"0030",strLanguage,"",0);
					return "";
				}
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
					strReturn = strReturn + "N''%" + txtInfo.Text.Trim().Replace("'", "") + "%''";
				else
					strReturn = strReturn + "N''" + this.txtInfo.Text.Trim().Replace("'", "") + "''";
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
				string strConditionText="";
				if (txtFillCondition.Text.Trim() == "")
				{
					strCondition = CheckBeforeAddCondition();
					strConditionText=CheckBeforeAddConditionText();
				}
				else
				{
					//xu ly an ben duoi
					strCondition = this.txtFillCondition.Text.Trim(); 
					if (rdAnd.Checked)
						strCondition += " And ";
					else strCondition += " Or ";
					strCondition += this.CheckBeforeAddCondition();
					//xu ly ben tren
					strConditionText=this.txtFillConditionText.Text.Trim();
					if (rdAnd.Checked)
						strConditionText += strLanguage=="EN"?" And ": " Và ";
					else strConditionText += strLanguage=="EN"?" Or ": " Hoặc ";
					strConditionText += this.CheckBeforeAddConditionText();
				}
				//Xữ lý ẩn
				this.txtFillCondition.Text = strCondition.Trim();
				this.txtFillConditionText.Text = strConditionText.Trim();

				if (Session["DivBool"].ToString()=="1") this.divBool.Attributes.Add("style","display:block");
			}
			catch(Exception exp)
			{
				clsChangeLang.popupWindowExp(this,exp);
			}
		}

		private void cmdExport_Click(object sender, System.EventArgs e)
		{
			dtgEmp.AllowPaging = false;
			if (Session["SqlExport"] != null)
			{
				this.dtgEmp.DataSource = clsCommon.GetDataTable(Session["SqlExport"].ToString());
				this.dtgEmp.DataBind();
			}
			else 
				BindDataGrid();
			iHRPCore.DataGridExcelExporter myExcelXport=new iHRPCore.DataGridExcelExporter(dtgEmp);
			myExcelXport.Export("");
			myExcelXport = null;
			dtgEmp.AllowPaging = true;
			//BindDataGrid();
			if (Session["DivBool"].ToString()=="1") this.divBool.Attributes.Add("style","display:block");
		}

		private void btnRemoveAll_Click(object sender, System.EventArgs e)
		{		
			ClearControls();		
		}

		private void txtDate_TextChanged(object sender, System.EventArgs e)
		{
		
		}	

	}
}
