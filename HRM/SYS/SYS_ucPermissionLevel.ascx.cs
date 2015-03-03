namespace iHRPCore.SYS
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
	using iHRPCore;
	using GridSort;
	using System.Configuration;
	/// <summary>
	///		Summary description for SYS_ucPermissionLevel.
	/// </summary>
	public class SYS_ucPermissionLevel : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnAddNew;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Button cmdGetUserAccount;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel3ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel2ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel1ID;
		protected System.Web.UI.WebControls.DropDownList cboLSCompanyID;
		protected System.Web.UI.WebControls.DropDownList cboLSLevel1ID;
		protected System.Web.UI.WebControls.DropDownList cboLSLevel2ID;
		protected System.Web.UI.WebControls.DropDownList cboLSLevel3ID;
		protected System.Web.UI.WebControls.DataGrid dtgList;
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtPermissionLevelID;
		protected System.Web.UI.WebControls.CheckBox chkFChk;
		protected System.Web.UI.WebControls.CheckBox chkFApp;
		protected System.Web.UI.WebControls.TextBox txtUserGroupID;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label lblUserName;		
		protected System.Web.UI.WebControls.CheckBox chkFTK;
		protected System.Web.UI.WebControls.CheckBox chkFHR;
		public string strLanguage = "EN";
		protected System.Web.UI.WebControls.TextBox txtPageLoad;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.ListBox lstListUser;
		protected System.Web.UI.WebControls.ListBox lstListGroup;
		protected System.Web.UI.WebControls.TextBox txtLstListGroup;
		protected System.Web.UI.WebControls.TextBox txtListLen;
		protected System.Web.UI.WebControls.LinkButton btnSearch;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Hidden1;
		protected System.Web.UI.WebControls.TextBox txtHidenInfo;
		protected ColumnList uctrlColumns;


		private void Page_Load(object sender, System.EventArgs e)
		{
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";	
			if(!Page.IsPostBack)
			{
				txtPageLoad.Text = "1";
				LoadDataCombo();	
				LoadDataGrid(this.Page);
				Session["ssAddnew"]="Addnew";
				DataGridSort.AddItemColumn(uctrlColumns, dtgList);//
			}
			btnSave.Attributes.Add("OnClick", " return validform()");
			btnSearch.Attributes.Add("OnClick", "return checkdelete()");
			ButtonClick();
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
			this.cmdGetUserAccount.Click += new System.EventHandler(this.cmdGetUserAccount_Click);
			this.cboLSCompanyID.SelectedIndexChanged += new System.EventHandler(this.cboLSCompanyID_SelectedIndexChanged);
			this.cboLSLevel1ID.SelectedIndexChanged += new System.EventHandler(this.cboLSLevel1ID_SelectedIndexChanged);
			this.cboLSLevel2ID.SelectedIndexChanged += new System.EventHandler(this.cboLSLevel2ID_SelectedIndexChanged);
			this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
			this.btnSearch.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			this.dtgList.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgList_ItemCommand);
			this.dtgList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgList_PageIndexChanged);
			this.dtgList.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dtgList_SortCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		private void LoadDataCombo()
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";		
			//clsCommon.LoadDropDownListControl(cboLSCompanyID,"sp_GetDataCombo @TableName='LS_tblCompany',@Fields='LSCompanyID as [ID]," + strTextField + " as Name'","ID","Name",true);			
			//string strTextField = strLanguage == "VN"?"VNName":"Name";
			clsHREmpList.LoadComboCompany(cboLSCompanyID, strTextField, strLanguage,this.Page);
		}

		private void cboLSCompanyID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				string strCompanyID = cboLSCompanyID.SelectedValue.Trim();
				string strTextField = strLanguage == "VN"?"VNName":"Name";			
				clsHREmpList.LoadComboLevel1ByCompany(cboLSLevel1ID,strTextField,strCompanyID, strLanguage,this.Page); 
				cboLSLevel1ID.SelectedValue = this.txtLevel1ID.Value.Trim();
				cboLSLevel1ID_SelectedIndexChanged(null,null);
			}
			catch(Exception ex)
			{
				lblErr.Text=ex.ToString();
			}
		}

		private void cboLSLevel1ID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				string strLevel1ID = cboLSLevel1ID.SelectedValue.Trim();
				string strTextField = strLanguage == "VN"?"VNName":"Name";
				clsHREmpList.LoadComboLevel2ByLevel1(cboLSLevel2ID,strTextField,strLevel1ID, strLanguage,this.Page); 
				cboLSLevel2ID.SelectedValue = this.txtLevel2ID.Value.Trim();
				cboLSLevel2ID_SelectedIndexChanged(null,null);
			}
			catch(Exception ex)
			{				
			}
		}

		private void cboLSLevel2ID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				string strLevel2ID = cboLSLevel2ID.SelectedValue.Trim();
				string strTextField = strLanguage == "VN"?"VNName":"Name";
				clsHREmpList.LoadComboLevel3ByLevel2(cboLSLevel3ID,strTextField,strLevel2ID, strLanguage); 
				cboLSLevel3ID.SelectedValue = this.txtLevel3ID.Value.Trim();
			}
			catch(Exception ex)
			{
			}
		}

		private void LoadDataGrid(System.Web.UI.Page pPage)
		{
			DataTable dtList = new DataTable();
			try
			{	
				string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
				dtList = clsCommon.GetDataTable("SYS_spfrmPERMISSIONLEVEL @Activity='GetDataAll',@LanguageID='" + strLanguage + "',@UserGroupID='" + sAccountLogin + "'");
				dtgList.DataSource = dtList;
				dtgList.CurrentPageIndex = 0;
				dtgList.DataBind();
				//Hau
				this.dtgList.PageSize = uctrlColumns.iPageRows;
				
				if(this.uctrlColumns.bAdvMultiSort)
				{
					try
					{
						DataGridSort.AdvancedMultiSort(dtgList, dtList, uctrlColumns);
					}
					catch
					{
						this.dtgList.CurrentPageIndex = 0;
						DataGridSort.AdvancedMultiSort(dtgList, dtList, uctrlColumns);
					}
				}
				else
				{
					try
					{
						DataGridSort.Refresh(dtgList, dtList);
					}
					catch
					{
						this.dtgList.CurrentPageIndex = 0;
						DataGridSort.Refresh(dtgList, dtList);
					}
				}
				//					dtgList.DataBind();
				this.uctrlColumns.iTotalRows = dtList.Rows.Count;//
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
			
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				string []sList=txtLstListGroup.Text.Split(',');
				cmd.CommandText = "SYS_spfrmPermissionUser";
				cmd.Parameters.Clear();

				cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 50).Value = "Delete";				
				cmd.Parameters.Add("@UserGroupID", SqlDbType.VarChar, 12).Value=txtUserGroupID.Text.Trim(); 
				cmd.Parameters.Add("@LSCompanyID", SqlDbType.VarChar, 12).Value=cboLSCompanyID.SelectedValue.Trim(); 
				cmd.Parameters.Add("@LSLevel1ID", SqlDbType.VarChar, 12).Value=cboLSLevel1ID.SelectedValue.Trim(); 
				cmd.Parameters.Add("@LSLevel2ID", SqlDbType.VarChar, 12).Value=cboLSLevel2ID.SelectedValue.Trim(); 
				cmd.Parameters.Add("@LSLevel3ID", SqlDbType.VarChar, 12).Value=cboLSLevel3ID.SelectedValue.Trim(); 
				cmd.ExecuteNonQuery();

				for(int i=0;i<sList.Length;i++)
				{
					cmd.Parameters.Clear();

					cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 50).Value = "Save";				
					cmd.Parameters.Add("@UserGroupID", SqlDbType.VarChar, 12).Value=txtUserGroupID.Text.Trim(); 
					cmd.Parameters.Add("@EmpID", SqlDbType.VarChar, 12).Value=sList[i].ToString();
					cmd.ExecuteNonQuery();
				}
				
				//cmd.CommandText = "UMS_sptblUserAccount";
				//string sEmpID = txtLstListGroup.Text;
				

				cmd.Transaction.Commit();			
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();			
				clsChangeLang.popupWindow(this.Parent,"0044",strLanguage,"",1);
				BindUserList();
				BindUserGroup();
				lblUserName.Text = GetUserNameText();
				//clsChangeLang.popupWindow(this.Parent,"0044",strLanguage,"",1);
				
			}
			catch(Exception ex)
			{
				
				cmd.Transaction.Rollback();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				clsChangeLang.popupWindowExp_Mess(this.Parent,ex);
			}
			/*try
			{	
				if(Session["ssAddnew"].ToString().Trim()=="Addnew")			
				{
					lblErr.Text=clsCommon.sImpactDB(txtPermissionLevelID.Value,"Save",this,"SYS_spfrmPERMISSIONLEVEL");
						
				}
				else				
				{
				 	lblErr.Text=clsCommon.sImpactDB("@PermissionLevelID",txtPermissionLevelID.Value,"Update",this,"SYS_spfrmPERMISSIONLEVEL");						
				}					
				if (lblErr.Text=="")
				{
					LoadDataGrid(this.Page);
					AddNew();
				}
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
			finally
			{
				//txtReturnValue.Value = "";
			}*/
		}

		private void dtgList_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				if(e.CommandName.Trim().ToUpper() =="EDIT")
				{
					txtPermissionLevelID.Value = dtgList.Items[e.Item.ItemIndex].Cells[0].Text.Trim();
					
					DataRow iRow = clsCommon.GetDataRow("SYS_spfrmPERMISSIONLEVEL @Activity='GetDataByID',@PermissionLevelID='" + txtPermissionLevelID.Value + "'");
					if(iRow != null)
					{						
						cboLSCompanyID.SelectedValue = iRow["LSCompanyID"].ToString().Trim();
						txtLevel1ID.Value=iRow["LSLevel1ID"].ToString().Trim();
						cboLSCompanyID_SelectedIndexChanged(null,null);
						cboLSLevel1ID.SelectedValue = iRow["LSLevel1ID"].ToString().Trim();
						txtLevel2ID.Value=iRow["LSLevel2ID"].ToString().Trim();
						cboLSLevel1ID_SelectedIndexChanged(null,null);						
						cboLSLevel2ID.SelectedValue = iRow["LSLevel2ID"].ToString().Trim();
						txtLevel3ID.Value=iRow["LSLevel3ID"].ToString().Trim();						
						cboLSLevel2ID_SelectedIndexChanged(null,null);
						cboLSLevel3ID.SelectedValue = iRow["LSLevel3ID"].ToString().Trim();						
						txtUserGroupID.Text=iRow["UserGroupID"].ToString().Trim();

						if (iRow["Fchk"].ToString().Trim()=="True")
							chkFChk.Checked=true;
						else
							chkFChk.Checked=false;

						if (iRow["FApp"].ToString().Trim()=="True")
							chkFApp.Checked=true;
						else
							chkFApp.Checked=false;	
						if (iRow["FHR"].ToString().Trim()=="True")
							chkFHR.Checked=true;
						else
							chkFHR.Checked=false;
						if (iRow["FTK"].ToString().Trim()=="True")
							chkFTK.Checked=true;
						else
							chkFTK.Checked=false;
						lblUserName.Text=iRow["UserName"].ToString().Trim();

					}					
				}
				Session["ssAddnew"] = "Edit";
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}
		private void cmdGetUserAccount_Click(object sender, System.EventArgs e)
		{
			//Session["ModuleID"] = this.dlModule.SelectedItem.Value.Trim();
			clsCommon.OpenNewWindow(this.Page,"Formpage.aspx?Ascx=SYS/SYS_ucUserAccountList.ascx","");
		}

		private void btnAddNew_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("Editpage.aspx?ModuleID=SYS&ParentID=157&FunctionID=565&Ascx=SYS/SYS_ucPermissionLevel.ascx");
			lblErr.Text="";
			AddNew();		
		
		}
		private void BindUserList()
		{
			string strSQL = "SYS_spfrmPermissionUser @Activity='GetEmpIDNP'";
				strSQL +=",@UserGroupID ='"+txtUserGroupID.Text+"', @LSCompanyID = '"+cboLSCompanyID.SelectedValue +"'";
				strSQL +=",@LSLevel1ID ='"+cboLSLevel1ID.SelectedValue+"', @LSLevel2ID = '"+cboLSLevel2ID.SelectedValue+"'";
				strSQL +=",@LSLevel3ID ='"+cboLSLevel3ID.SelectedValue+"'";

			DataTable dt = clsCommon.GetDataTable(strSQL);
			clsCommon.LoadListBoxControl(lstListUser,dt,"ID","UserGroupName",false);					
		}
		private void BindUserGroup()
		{
			string strSQL = "SYS_spfrmPermissionUser @Activity='GetEmpIDP'";
			strSQL +=",@UserGroupID ='"+txtUserGroupID.Text+"', @LSCompanyID = '"+cboLSCompanyID.SelectedValue +"'";
			strSQL +=",@LSLevel1ID ='"+cboLSLevel1ID.SelectedValue+"', @LSLevel2ID = '"+cboLSLevel2ID.SelectedValue+"'";
			strSQL +=",@LSLevel3ID ='"+cboLSLevel3ID.SelectedValue+"'";

			DataTable dt = clsCommon.GetDataTable(strSQL);
			clsCommon.LoadListBoxControl(lstListGroup,dt,"ID","UserGroupName",false);
			txtListLen.Text=lstListGroup.Items.Count.ToString();
		}
		private string GetUserNameText()
		{
			string strSQL = "UMS_sptblUserAccount 'GetUserAccountByID',@UserGroupID=N'" + txtUserGroupID.Text.Trim() + "'";

			DataTable dt = clsCommon.GetDataTable(strSQL);
			if(dt.Rows.Count > 0)
				return dt.Rows[0]["Name"].ToString();
			return "";
		}
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			
			BindUserList();
			BindUserGroup();
			lblUserName.Text = GetUserNameText();
			//BindUserGroup(cboGroupListID.SelectedValue);

			/*try
			{
				string strID="";
				for(int i=0;i<dtgList.Items.Count;i++)
				{
					if(((CheckBox)dtgList.Items[i].FindControl("chkSelect")).Checked==true)
					{
						strID += dtgList.Items[i].Cells[0].Text.Trim() + "$";
					}
				}
				clsCommon.DeleteListRecord("SYS_spfrmPERMISSIONLEVEL","PermissionLevelID",SqlDbType.NVarChar,12,strID);
				LoadDataGrid(this.Page);	
				AddNew();
				lblErr.Text="";
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}*/
		}

		private void dtgList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			try
			{
				LoadDataGrid(this.Page);
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
		private void AddNew()
		{
			cboLSLevel1ID.Items.Clear();
			cboLSLevel2ID.Items.Clear();
			clsCommon.ClearControlValue(this);			
			lblUserName.Text="";
			Session["ssAddnew"] = "Addnew";				
			btnSave.Enabled = true;
			chkFApp.Checked = true;
			chkFChk.Checked = true;
			chkFHR.Checked = true;
			chkFTK.Checked = true;
		}
		
		#region Cac su kien xu li thao tac Sort (Hau)

		private void dtgList_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			try
			{
				DataGridSort.Grid_Sort(dtgList, Read_Data(), e, uctrlColumns);
			}
			catch{}
		}

		private DataTable Read_Data()
		{			

			int intTopRow =0;
			if ( txtPageLoad.Text == "1" )
			{
				intTopRow = dtgList.PageSize;
				txtPageLoad.Text = "0";
			}

			DataTable dtb = clsCommon.GetDataTable("SYS_spfrmPERMISSIONLEVEL @Activity='GetDataAll',@LanguageID='" + strLanguage + "'");
			return dtb;
		}

		private void ButtonClick()
		{
			this.uctrlColumns.ButtonSort.ServerClick += new EventHandler(ButtonSort_ServerClick);
			this.uctrlColumns.ButtonPRChanged.ServerClick += new EventHandler(ButtonPRChanged_ServerClick);
		}

		private void ButtonSort_ServerClick(object sender, EventArgs e)
		{
			try
			{
				DataGridSort.AdvancedMultiSort(dtgList, Read_Data(), uctrlColumns);
			}
			catch{}
		}

		private void ButtonPRChanged_ServerClick(object sender, EventArgs e)
		{
			try
			{
				dtgList.PageSize = uctrlColumns.iPageRows;
				dtgList.CurrentPageIndex = 0;
				DataView dv = new DataView(Read_Data());
				dv.Sort = DataGridSort.sMulSort;
				dtgList.DataSource = dv;
				dtgList.DataBind();
			}
			catch{}
		}
		#endregion		

	}
}
