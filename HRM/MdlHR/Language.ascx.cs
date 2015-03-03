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
	/// <summary>
	///		Summary description for Language.
	/// </summary>
	public class Language : System.Web.UI.UserControl
	{		
		#region  Declare
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label lblNote;
		protected System.Web.UI.WebControls.TextBox txtNote;
		protected System.Web.UI.WebControls.Label lblLanguage;
		protected System.Web.UI.WebControls.Label lblLanguageLevel;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList cboLanguageLevelID;
		protected System.Web.UI.WebControls.LinkButton btnAddNew;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLanguageRecordID;
		protected System.Web.UI.WebControls.LinkButton btnSaveSkill;
		protected System.Web.UI.WebControls.RadioButton optLanguage;
		protected System.Web.UI.WebControls.RadioButton optComputing;
		protected System.Web.UI.WebControls.TextBox txtType;
		protected System.Web.UI.WebControls.CheckBoxList chkSkill;
		protected System.Web.UI.WebControls.DropDownList cboLanguageList;
		protected System.Web.UI.WebControls.TextBox txtLanguageID;
		protected FPTToolWeb.Control.DataGrids.FPTDataGrid grdLanguage;
		protected System.Web.UI.HtmlControls.HtmlGenericControl DIV1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.DropDownList Dropdownlist1;
		public string strLanguage = "EN";
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{		
			Session["CurFunction"] = Request.Params["FunctionID"].Trim();
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"VN";			
			if(!Page.IsPostBack)
			{
				optLanguage.Checked=true;
				LoadDataCombo();
				LoadDataGrid();
				Session["ssStatusLanguage"] = "AddNew";
			}
			btnSave.Attributes.Add("OnClick", " return validform()");
			btnDelete.Attributes.Add("OnClick", "return checkdelete()");
		}
		/// <summary>
		/// Load all working record of Employee
		/// </summary>
		private void LoadDataGrid()
		{
			DataTable dtList = new DataTable();
			try
			{				
				dtList = clsHRLanguageRecord.GetDataByEmpID(Session["EmpID"]);
				grdLanguage.DataSource = dtList;
				grdLanguage.CurrentPageIndex = 0;
				grdLanguage.DataBind();
			}
			catch(Exception ex)
			{
				clsChangeLang.popupWindowExp(this.Parent,ex);
			}
			finally
			{
				dtList.Dispose();
			}
		}
		private void LoadDataCombo()
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			//if (optLanguage.Checked==true)  
			if (Dropdownlist1.SelectedItem.Value=="0")  
				clsCommon.LoadDropDownListControl(cboLanguageList,"sp_GetDataComboLanguage @TableName='LS_tblLanguage',@IsLanguage=1,@Fields='LSLanguageID as [ID]," + strTextField + " as Name'","ID","Name",true);			
			else
				clsCommon.LoadDropDownListControl(cboLanguageList,"sp_GetDataComboLanguage @TableName='LS_tblLanguage',@IsLanguage=0,@Fields='LSLanguageID as [ID]," + strTextField + " as Name'","ID","Name",true);			
			clsCommon.LoadDropDownListControl(cboLanguageLevelID,"sp_GetDataCombo @TableName='LS_tblLanguageLevel',@Fields='LSLanguageLevelID," + strTextField + " as Name'","LSLanguageLevelID","Name",true);
			//clsCommon.LoadCheckBoxList(chkSkill,"sp_GetDataCombo @TableName = 'ls_tblSkill'","LSSkillCode","Name");
			clsCommon.LoadCheckBoxList(chkSkill,"sp_GetDataCombo @TableName = 'ls_tblEmpSkill',@Fields='LSEmpSkillID as [ID]," + strTextField + " as Name'","ID","Name");

			DataTable dtSkill = new DataTable();
			dtSkill = clsHRLanguageRecord.GetSkillsByID(Session["EmpID"]);
			for(int i=0;i<dtSkill.Rows.Count;i++)
			{
				for(int j =0; j < chkSkill.Items.Count;j++)
				{
					if(dtSkill.Rows[i]["LSEmpSkillID"].ToString().Trim()==chkSkill.Items[j].Value.Trim())
						chkSkill.Items[j].Selected = true;
				}					
			}
			dtSkill.Dispose();
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
			this.Dropdownlist1.SelectedIndexChanged += new System.EventHandler(this.Dropdownlist1_SelectedIndexChanged);
			this.grdLanguage.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.grdLanguage_ItemCommand);
			this.grdLanguage.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.grdLanguage_PageIndexChanged);
			this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnSaveSkill.Click += new System.EventHandler(this.btnSaveSkill_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			try
			{
				if (optLanguage.Checked==true)
					txtType.Text="1"; 
				else
					txtType.Text="0";  
				if(Session["ssStatusLanguage"].ToString().Trim()=="AddNew")
				{
					if (clsHRLanguageRecord.CheckSave(Session["EmpID"],cboLanguageList.SelectedValue+cboLanguageLevelID.SelectedValue))
						clsCommon.ImpactDB(Session["EmpID"].ToString().Trim(),"Save",this,"HR_spfrmLANGUAGE");				
					else
						//lblErr.Text="Dữ liệu bị trùng. Kiểm tra lại";
						clsChangeLang.popupWindow(this.Parent,"0024",strLanguage,"",0);
				}
				else
				{
					clsCommon.UpdateByKey("LanguageRecordID",txtLanguageRecordID.Value.Trim(),"Update",this,"HR_spfrmLANGUAGE");									
				}
				clsCommon.ClearControlValue(this);
				Session["ssStatusLanguage"] = "AddNew";
				LoadDataGrid();
				if (optLanguage.Checked==true)  
					clsCommon.LoadDropDownListControl(cboLanguageList,"sp_GetDataComboLanguage @TableName='LS_tblLanguage',@IsLanguage=1,@Fields='LSLanguageID," + strTextField + " as Name'","LSLanguageID","Name",true);			
				else
					clsCommon.LoadDropDownListControl(cboLanguageList,"sp_GetDataComboLanguage @TableName='LS_tblLanguage',@IsLanguage=0,@Fields='LSLanguageID," + strTextField + " as Name'","LSLanguageID","Name",true);							
			}
			catch(Exception ex)
			{
				clsChangeLang.popupWindowExp(this.Parent,ex);
			}
		}

		private void grdLanguage_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string strTextField1 = strLanguage == "VN"?"VNName":"Name";
			try
			{
				if(e.CommandName.Trim().ToUpper() =="EDIT")
				{
					txtLanguageRecordID.Value = grdLanguage.Items[e.Item.ItemIndex].Cells[0].Text.Trim();
					DataRow iRow = clsHRLanguageRecord.GetDataByID(txtLanguageRecordID.Value.Trim());
					if(iRow != null)
					{
						txtNote.Text = iRow["Note"].ToString().Trim();
						cboLanguageLevelID.SelectedValue = iRow["LanguageLevelID"].ToString().Trim();
						txtType.Text=iRow["Type"].ToString();  
						if (txtType.Text=="True")
						{
							clsCommon.LoadDropDownListControl(cboLanguageList,"sp_GetDataComboLanguage @TableName='LS_tblLanguage',@IsLanguage=1,@Fields='LSLanguageCode," + strTextField1 + " as Name'","LSLanguageCode","Name",true);			
							optLanguage.Checked=true;
							optComputing.Checked=false;
						}
						else if (txtType.Text=="False")
						{
							optComputing.Checked=true;
							optLanguage.Checked=false;
							clsCommon.LoadDropDownListControl(cboLanguageList,"sp_GetDataComboLanguage @TableName='LS_tblLanguage',@IsLanguage=0,@Fields='LSLanguageCode," + strTextField1 + " as Name'","LSLanguageCode","Name",true);			
						}
						
						cboLanguageList.SelectedValue = iRow["LanguageID"].ToString().Trim();
					}
				}
				Session["ssStatusLanguage"] = "Edit";
			}
			catch(Exception ex)
			{
				clsChangeLang.popupWindowExp(this.Parent,ex);
			}
		}

		private void btnAddNew_Click(object sender, System.EventArgs e)
		{
			clsCommon.ClearControlValue(this);
			Session["ssStatusLanguage"] = "AddNew";
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				string strID="";
				for(int i=0;i<grdLanguage.Items.Count;i++)
				{
					if(((CheckBox)grdLanguage.Items[i].FindControl("chkSelect")).Checked==true)
					{
						strID += grdLanguage.Items[i].Cells[0].Text.Trim() + "$";
					}
				}
				clsCommon.DeleteListRecord("HR_spfrmLANGUAGE","LanguageRecordID",SqlDbType.Int,4,strID);
				LoadDataGrid();
			}
			catch(Exception ex)
			{
				clsChangeLang.popupWindowExp(this.Parent,ex);
			}
		}

		private void grdLanguage_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			try
			{
				LoadDataGrid();
				grdLanguage.CurrentPageIndex=e.NewPageIndex;
				grdLanguage.DataBind();
			}
			catch(Exception ex)			
			{
				string str = ex.Message;
			}
		}
/// <summary>
/// Update skill for emp
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
		private void btnSaveSkill_Click(object sender, System.EventArgs e)
		{
			try{
				//---Update skill
				string strSkillID="";
				for(int i=0;i<chkSkill.Items.Count;i++)
				{
					if(chkSkill.Items[i].Selected)
						strSkillID += chkSkill.Items[i].Value + "$";
				}
				clsHRLanguageRecord.UpdateSkills(Session["EmpID"].ToString().Trim(),strSkillID);
				//-------
			}
			catch(Exception ex)
			{
				clsChangeLang.popupWindowExp(this.Parent,ex);
			}
		}

		private void Dropdownlist1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			LoadDataCombo();
		}



	}
}
