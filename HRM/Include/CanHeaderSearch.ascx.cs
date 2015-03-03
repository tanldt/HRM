namespace iHRPCore.Include
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.Com;
	using iHRPCore.Include;
	using iHRPCore.HRComponent;

	/// <summary>
	///		Summary description for CanHeaderSearch.
	/// </summary>
	public class CanHeaderSearch : System.Web.UI.UserControl
	{
		public System.Web.UI.WebControls.RadioButton rdOr;
		public System.Web.UI.WebControls.RadioButton rdAnd;
		public System.Web.UI.WebControls.Label Label4;
		public System.Web.UI.WebControls.RadioButton rdFalse;
		public System.Web.UI.WebControls.RadioButton rdTrue;
		public System.Web.UI.WebControls.TextBox txtInfo;
		public System.Web.UI.WebControls.TextBox txtNumber;
		public System.Web.UI.WebControls.TextBox txtDate;
		public System.Web.UI.WebControls.DropDownList cboInfo;
		public System.Web.UI.WebControls.Label Label3;
		public System.Web.UI.WebControls.DropDownList cboCondition;
		public System.Web.UI.WebControls.Label Label2;
		public System.Web.UI.WebControls.TextBox txtFillCondition;
		public System.Web.UI.WebControls.Button btnRemoveAll;
		public System.Web.UI.WebControls.Button btRemove;
		public System.Web.UI.WebControls.Button cmdAdd;
		public System.Web.UI.WebControls.DropDownList cboCriteria;
		public System.Web.UI.WebControls.Label Label1;
		public System.Web.UI.WebControls.DropDownList cboDocumentStatus;
		public System.Web.UI.WebControls.Label lblDocumentStatus;
		public System.Web.UI.WebControls.DropDownList cboLSJobTitleID;
		public System.Web.UI.WebControls.Label lblJobTitle;
		public System.Web.UI.WebControls.TextBox txtCandidateName;
		public System.Web.UI.WebControls.Label lblCandidateName;
		public System.Web.UI.WebControls.TextBox txtCandidateCode;
		public System.Web.UI.WebControls.Label lblCandidateID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtColType;
		protected System.Web.UI.HtmlControls.HtmlGenericControl divBool;
		public string strLanguage="EN";

		private void Page_Load(object sender, System.EventArgs e)
		{
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";		
			if(!Page.IsPostBack)
			{				
				LoadDefaultValue();
			}
			this.cmdAdd.Attributes.Add("OnClick","return CheckBeforeAddCondition()");
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
			this.cboLSJobTitleID.SelectedIndexChanged += new System.EventHandler(this.cboLSJobTitleID_SelectedIndexChanged);
			this.cboCriteria.SelectedIndexChanged += new System.EventHandler(this.cboCriteria_SelectedIndexChanged);
			this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
			this.btRemove.Click += new System.EventHandler(this.btRemove_Click);
			this.btnRemoveAll.Click += new System.EventHandler(this.btnRemoveAll_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void LoadDefaultValue()
		{
			string strTextField = strLanguage == "EN"?"Name":"Name";
			clsCommon.LoadDropDownListControl(this.cboCriteria,"HR_spfrmDynamicCanList 'GetCriteria'","CriteriaID","Criteria",true);
			clsCommon.LoadDropDownListControl(this.cboCondition,"HR_spfrmDynamicEmpList 'GetOperator'","OperatorID","Operator",true);			
			clsCommon.LoadDropDownListControl(cboLSJobTitleID,"sp_GetDataCombo @TableName='LS_tblJobTitle',@Fields='LSJobTitleID as [ID]," + strTextField + " as Name'","ID","Name",true);	
			
		}

		private void cboCriteria_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				DataTable rsData = clsCommon.GetDataTable("HR_spfrmDynamicCanList 'GetcboLS'");
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
							rdTrue.Text = "Male";
							rdFalse.Text = "Female";
						}
						else if (strSelected == "Foreigner")
						{
							rdTrue.Text = "Foreigner";
							rdFalse.Text = "VietNamese";
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
				clsChangeLang.popupWindowExp(this,exp);
			}
		}

		private void btRemove_Click(object sender, System.EventArgs e)
		{
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
		}
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
			this.txtDate.Visible = false;
			this.txtNumber.Visible = false;
			txtFillCondition.Text="";
			this.divBool.Attributes.Add("style","display:none");
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
			if (strColType == "datetime")
			{
				
				string[] mDateArr = this.txtDate.Text.Split(new char[]{'-'});
				//string[] mDateArr = this.txtDate.Text.Split(new char[]{'/'});				
				//string mDate = mDateArr[0].Trim() + "/" + mDateArr[1].Trim() + "/" + mDateArr[2].Trim();
				string mDate = mDateArr[0].Trim() + "-" + mDateArr[1].Trim() + "-" + mDateArr[2].Trim();
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
				clsChangeLang.popupWindowExp(this,exp);
			}
		}

		private void btnRemoveAll_Click(object sender, System.EventArgs e)
		{
			ClearControls();
		}

		private void cboLSJobTitleID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}
	}
}
