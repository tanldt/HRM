namespace iHRPCore.SYS
{
	using System;
	using System.Collections;
	using System.ComponentModel;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.SessionState;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Data.SqlClient;
	using System.Configuration;
	using iHRPCore.Com;
	using iHRPCore.Component;

	/// <summary>
	///		Summary description for SYS_ucPermission.
	/// </summary>
	public class SYS_ucPermission : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.DropDownList dlModule;
		protected System.Web.UI.WebControls.TextBox txtUserAccount;
		protected System.Web.UI.WebControls.Button cmdGetUserAccount;
		protected System.Web.UI.WebControls.DataGrid dgFunction;
		protected System.Web.UI.WebControls.Label lblRightTitle;
		protected System.Web.UI.WebControls.Label lblCurUserCaption;
		protected System.Web.UI.WebControls.Label lblCurUser;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtUserID;
		protected System.Web.UI.HtmlControls.HtmlInputRadioButton rdRightMod;
		protected System.Web.UI.HtmlControls.HtmlInputRadioButton rdRightFunc;
		protected System.Web.UI.HtmlControls.HtmlTableRow trFunction;
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label lbl1;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtChoseUserID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtChoseFGroup;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtChoseUserName;
		protected System.Web.UI.WebControls.LinkButton btnSearch;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtFuncID;
		protected System.Web.UI.WebControls.LinkButton btnAddRight;
		protected System.Web.UI.WebControls.LinkButton btnDeleteAllRight;
		protected System.Web.UI.WebControls.LinkButton btnSetupAgain;
		protected System.Web.UI.WebControls.LinkButton btnPermission;
		protected System.Web.UI.WebControls.RadioButton rdGroup;
		protected System.Web.UI.WebControls.RadioButton rdUser;
		protected System.Web.UI.HtmlControls.HtmlInputCheckBox chkView;
		protected System.Web.UI.HtmlControls.HtmlInputCheckBox chkDelete;
		protected System.Web.UI.HtmlControls.HtmlInputCheckBox chkUpdate;
		protected System.Web.UI.HtmlControls.HtmlInputCheckBox chkAddNew;
		protected System.Web.UI.HtmlControls.HtmlInputCheckBox chkApprove;
		bool IsAddAllRight = false;
		public string strLanguage = "VN";

		#region Creater: LANHTD, 23/09/2004. Page_Load
		private void Page_Load(object sender, System.EventArgs e)
		{
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"VN";
			this.lblCurUser.Text = Convert.ToString(Session["AccountLogin"]);
			if (!Page.IsPostBack)
			{
				clsCommon.LoadDropDownListControl(this.dlModule,"sp_clsCommon 'GetModuleList','"+strLanguage+"'","ModuleID","ModuleName",true); 
				this.trFunction.Attributes.Remove("style");
				this.dlModule_SelectedIndexChanged(null,null);
			}
			this.btnAddRight.Attributes.Add("OnClick","return checkForm()");
			this.btnDeleteAllRight.Attributes.Add("OnClick","return checkForm()");
			//this.btnSetupAgain.Attributes.Add("OnClick","return checkForm()");
			cmdGetUserAccount.Attributes.Add("OnClick","return OpenNewWindow()");
			btnPermission.Attributes.Add("OnClick","return OpenDetails()");
		}
		#endregion

		#region InitControls
		private void MakeCheckBoxControls(bool check)
		{
			this.chkAddNew.Checked = check;
			this.chkApprove.Checked = check;
			this.chkDelete.Checked = check;
			this.chkUpdate.Checked = check;
			this.chkView.Checked = check;
		}
		#endregion

		#region Creater: LANHTD, 23/09/04, FillData
		private void FillData()
		{
			try
			{
				if (this.txtUserID.Value.Trim() != "")
				{
					string strModuleID = this.dlModule.SelectedItem.Value.Trim();
					DataTable dtb = clsCommon.GetDataTable("UMS_sptblPermisstion 'GetUserPermission',@result='',@UserGroupID=N'" + this.txtUserID.Value.Trim() + "',@ModuleID='" + strModuleID + "'");
					if (dtb.Rows.Count > 0)
					{
						if(strModuleID != "")
						{
							if (dtb.Rows[0]["RightModule"].ToString() == "1")
							{
								this.rdRightMod.Checked = true;
								this.rdRightFunc.Checked = false;
								this.trFunction.Attributes.Add("style","DISPLAY: none");
								//this.btnAddRight.Attributes.Add("style","DISPLAY: none");
								this.chkAddNew.Checked = true;
								this.chkApprove.Checked = true;
								this.chkDelete.Checked = true;
								this.chkUpdate.Checked = true;
								this.chkView.Checked = true;
								for(int i=0; i<this.dgFunction.Items.Count; i++)
								{
									((CheckBox)this.dgFunction.Items[i].FindControl("chkSelect")).Checked = true;
								}
								return;
							}
							else
							{
								this.rdRightMod.Checked = false;
								this.rdRightFunc.Checked = true;
								//btnAddRight.Attributes.Remove("style");
								for (int i=0; i<dtb.Rows.Count; i++)
								{
									string strFuncModID = dtb.Rows[i]["FuncModID"].ToString().Trim();
									string strRightModule = dtb.Rows[i]["RightModule"].ToString().Trim();
									if (strRightModule == "0")
									{
										for (int j=0; j<this.dgFunction.Items.Count; j++)
										{
											string strFuncID = this.dgFunction.Items[j].Cells[1].Text.Trim();
											if (strFuncModID == strFuncID)
											{
												this.dgFunction.Items[j].Cells[3].Text = dtb.Rows[i]["FApp"].ToString().Trim();
												this.dgFunction.Items[j].Cells[4].Text = dtb.Rows[i]["FRun"].ToString().Trim();
												this.dgFunction.Items[j].Cells[5].Text = dtb.Rows[i]["FDel"].ToString().Trim();
												this.dgFunction.Items[j].Cells[6].Text = dtb.Rows[i]["FEdit"].ToString().Trim();
												this.dgFunction.Items[j].Cells[7].Text = dtb.Rows[i]["FAdd"].ToString().Trim();
												((CheckBox)this.dgFunction.Items[j].FindControl("chkSelect")).Checked = true;
												break;
											}
										}
									}

								}
							}
						}
					}
					else
					{
						if (this.rdRightFunc.Checked)
							this.trFunction.Attributes.Remove("style");
						else
							this.trFunction.Attributes.Add("style","DISPLAY: none");
						for(int i=0; i<this.dgFunction.Items.Count; i++)
						{
							((CheckBox)this.dgFunction.Items[i].FindControl("chkSelect")).Checked = false;
						}
					}
					dtb.Dispose();
				}
			}
			catch(Exception exp)
			{
				this.lblErr.Text = exp.Message.ToString();
			}
		}
		#endregion

		#region Creater: LANHTD, 23/09/04, BindDataGrid
		private void BindDataGrid()
		{
			string strModuleID = this.dlModule.SelectedItem.Value.Trim();
			DataTable dtb = clsCommon.GetDataTable("sp_clsCommon 'GetFunctionByMdlIDDetails','" + strLanguage + "',@ModuleID = '" + strModuleID + "'");
			this.dgFunction.PageSize = 10;
			this.dgFunction.PagerStyle.Mode = PagerMode.NumericPages;
			this.dgFunction.DataSource = dtb;
			dgFunction.DataBind();
			dtb.Dispose();
			this.FillData();
		}
		#endregion

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
			this.dlModule.SelectedIndexChanged += new System.EventHandler(this.dlModule_SelectedIndexChanged);
			this.cmdGetUserAccount.Click += new System.EventHandler(this.cmdGetUserAccount_Click);
			this.btnPermission.Click += new System.EventHandler(this.btnPermission_Click);
			this.dgFunction.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgFunction_ItemCommand);
			this.dgFunction.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgFunction_PageIndexChanged);
			this.btnAddRight.Click += new System.EventHandler(this.btnAddRight_Click);
			this.btnDeleteAllRight.Click += new System.EventHandler(this.btnDeleteAllRight_Click);
			this.btnSetupAgain.Click += new System.EventHandler(this.btnSetupAgain_Click);
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region Creater: LANHTD, 23/09/04. dgFunction_PageIndexChanged
		private void dgFunction_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.BindDataGrid();
			this.dgFunction.CurrentPageIndex = e.NewPageIndex;
			this.dgFunction.DataBind();
		}
		#endregion

		#region  Creater: LANHTD, 23/09/04. dlModule_SelectedIndexChanged
		private void dlModule_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				this.BindDataGrid();
				if (rdRightMod.Checked)
				{
					this.trFunction.Attributes.Add("style","display:none");
					this.MakeCheckBoxControls(true);
				}
				else
					this.MakeCheckBoxControls(false);
			}
			catch(Exception exp)
			{
				this.lblErr.Text = exp.Message.ToString();
			}
		}

		#endregion

		#region Creater: LANHTD, 23/09/04. cmdGetUserAccount_Click
		private void cmdGetUserAccount_Click(object sender, System.EventArgs e)
		{
			Session["ModuleID"] = this.dlModule.SelectedItem.Value.Trim();
			Response.Redirect("SYS_frmUserAccountList.aspx");
		}

		#endregion

		#region Creater: LANHTD, 23/09/04. Delete all permission of this user on moduleID
		private string Impact_UMS_tblPermission_DeletePermission(SqlCommand cmd, SqlParameter param)
		{
			try
			{
				cmd.Parameters.Clear();
				param = cmd.Parameters.Add("@Action",SqlDbType.NVarChar,30);
				//Neu chi xoa quyen tren mot tinh nang
				if (this.txtFuncID.Value.Trim() != "")
				{
					param.Value = "DeleteFuncPermission";
					param = cmd.Parameters.Add("@FunctionID",SqlDbType.Int);
					param.Value = this.txtFuncID.Value.Trim();
				}
				else
				{
					//Nguoc lai neu xoa tat ca cac quyen tren module
					param.Value = "DeletePermission";
					param = cmd.Parameters.Add("@ModuleID",SqlDbType.NVarChar,5);
					param.Value = this.dlModule.SelectedItem.Value.Trim();
				}
				param = cmd.Parameters.Add("@result",SqlDbType.NVarChar,20);
				param.Direction = ParameterDirection.Output;
				param = cmd.Parameters.Add("@UserGroupID",SqlDbType.NVarChar,20);
				param.Value = this.txtUserID.Value.Trim();
				cmd.ExecuteNonQuery();
				string strresult = Convert.ToString(cmd.Parameters["@result"].Value);
				if (strresult == "-3" || strresult == "-4")
				{
					cmd.Transaction.Rollback();
					this.lblErr.Text = "Error occurence!";
					return "";
				}
				return strresult;
			}
			catch(Exception exp)
			{
				this.lblErr.Text = exp.Message.ToString();
				cmd.Transaction.Rollback();
				return "";
			}
		}
		#endregion

		#region Creater: LANHTD, 23/09/04. Insert UserModulePermission
		private string Impact_UMS_tblPermission_AddNewModulePer(SqlCommand cmd, SqlParameter param)
		{
			try
			{
				cmd.Parameters.Clear();
				param = cmd.Parameters.Add("@Action",SqlDbType.NVarChar,30);
				param.Value = "AddNewModulePer";
				param = cmd.Parameters.Add("@result",SqlDbType.NVarChar,20);
				param.Direction = ParameterDirection.Output;
				param = cmd.Parameters.Add("@ModuleID",SqlDbType.NVarChar,5);
				param.Value = this.dlModule.SelectedItem.Value.Trim();
				param = cmd.Parameters.Add("@UserGroupID",SqlDbType.NVarChar,20);
				param.Value = this.txtUserID.Value.Trim();
				cmd.ExecuteNonQuery();
				string strresult = Convert.ToString(cmd.Parameters["@result"].Value);
				if (strresult == "-4")
				{
					cmd.Transaction.Rollback();
					this.lblErr.Text = "Error occurence!";
					return "";
				}
				return strresult;
			}
			catch(Exception exp)
			{
				this.lblErr.Text = exp.Message.ToString();
				cmd.Transaction.Rollback();
				return "";
			}
		}
		#endregion

		#region Creater: LANHTD, 23/09/04. AddNewFunctionPer
		private string Impact_UMS_tblPermission_AddNewFunctionPer(SqlCommand cmd, SqlParameter param, bool All)//All: khi thiết lập tất cả các quyền
		{
			string strFunctionID = "";
			string strresult = "";

			#region Cac quyen tren tinh nang
			int intFApp = 0;
			int intFAdd = 0;
			int intFDel = 0;
			int intFEdit = 0;
			int intFRun = 0;
			if(All)
			{
				intFApp = 1;
				intFAdd = 1;
				intFDel = 1;
				intFEdit = 1;
				intFRun = 1;
			}
			else
			{
				intFApp = 0;
				if (this.chkApprove.Checked)
					intFApp	= 1;
				intFAdd = 0;
				if (this.chkAddNew.Checked)
					intFAdd	= 1;
				intFDel = 0;
				if (this.chkDelete.Checked)
					intFDel	= 1;
				intFEdit = 0;
				if (this.chkUpdate.Checked)
					intFEdit	= 1;
				intFRun = 0;
				if (this.chkView.Checked)
					intFRun	= 1;
			}
			#endregion

			for (int i=0; i<this.dgFunction.Items.Count; i++)
			{
				if (((CheckBox)this.dgFunction.Items[i].FindControl("chkSelect")).Checked)
				{
					try
					{
						strFunctionID = this.dgFunction.Items[i].Cells[1].Text.Trim();
						cmd.Parameters.Clear();
						param = cmd.Parameters.Add("@Action",SqlDbType.NVarChar,30);
						param.Value = "AddNew";
						param = cmd.Parameters.Add("@result",SqlDbType.NVarChar,20);
						param.Direction = ParameterDirection.Output;
						param = cmd.Parameters.Add("@UserGroupID",SqlDbType.NVarChar,20);
						param.Value = this.txtUserID.Value.Trim();
						param = cmd.Parameters.Add("@FunctionID",SqlDbType.NVarChar,12);
						param.Value = strFunctionID;
						param = cmd.Parameters.Add("@FRun",SqlDbType.Bit);
						param.Value = intFRun;
						param = cmd.Parameters.Add("@FEdit",SqlDbType.Bit);
						param.Value = intFEdit;
						param = cmd.Parameters.Add("@FDel",SqlDbType.Bit);
						param.Value = intFDel;
						param = cmd.Parameters.Add("@FAdd",SqlDbType.Bit);
						param.Value = intFAdd;
						param = cmd.Parameters.Add("@FApp",SqlDbType.Bit);
						param.Value = intFApp;
						cmd.ExecuteNonQuery();
						strresult = Convert.ToString(cmd.Parameters["@result"].Value);
						if (strresult == "-1" || strresult == "-2")
						{
							cmd.Transaction.Rollback();
							this.lblErr.Text = "Error occurence!";
							return "";
						}
					}
					catch(Exception exp)
					{
						this.lblErr.Text = exp.Message.ToString();
						cmd.Transaction.Rollback();
						return "";
					}
				}
			}
			return this.txtUserID.Value.Trim();
		}
		#endregion

		#region Creater: LANHTD,23/09/04. dgFunction_ItemCommand
		private void dgFunction_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			try
			{	
				if (e.CommandName == "cmdSelect")
				{
					string strFunctionName = e.Item.Cells[2].Text.Trim();
					this.MakeCheckBoxControls(false);
					this.lblRightTitle.Text = "Thiết lập quyền cho chức năng " + strFunctionName + " ";
					this.txtFuncID.Value = e.Item.Cells[1].Text.Trim();
					CheckBox chk = (CheckBox)e.Item.FindControl("chkSelect");
				
					if (chk.Checked)
					{
						string strFApp = e.Item.Cells[3].Text.Trim();
						string strFRun = e.Item.Cells[4].Text.Trim();
						string strFDel = e.Item.Cells[5].Text.Trim();
						string strFEdit = e.Item.Cells[6].Text.Trim();
						string strFAdd = e.Item.Cells[7].Text.Trim();
						if (strFApp == "1")
							this.chkApprove.Checked = true;
						else
							this.chkApprove.Checked = false;
						if (strFRun == "1")
							this.chkView.Checked = true;
						else
							this.chkView.Checked = false;
						if (strFDel == "1")
							this.chkDelete.Checked = true;
						else
							this.chkDelete.Checked = false;
						if (strFEdit == "1")
							this.chkUpdate.Checked = true;
						else
							this.chkUpdate.Checked = false;
						if (strFAdd == "1")
							this.chkAddNew.Checked = true;
						else
							this.chkAddNew.Checked = false;
					}
				}
			}
			catch(Exception exp)
			{
				this.lblErr.Text = exp.Message.ToString();
			}
		}

		#endregion

		#region Creater: LANHTD, 23/09/04. Impact_UMS_tblPermission_Update
		private string Impact_UMS_tblPermission_Update(SqlCommand cmd, SqlParameter param, bool All)//All: Khi thiet lap tat ca cac quyen
		{
			string strFunctionID = "";
			string strresult = "";

			#region Cac quyen tren tinh nang
			int intFApp = 0;
			int intFAdd = 0;
			int intFDel = 0;
			int intFEdit = 0;
			int intFRun = 0;
			if(All)
			{
				intFApp = 1;
				intFAdd = 1;
				intFDel = 1;
				intFEdit = 1;
				intFRun = 1;
			}
			else
			{
				intFApp = 0;
				if (this.chkApprove.Checked)
					intFApp	= 1;
				intFAdd = 0;
				if (this.chkAddNew.Checked)
					intFAdd	= 1;
				intFDel = 0;
				if (this.chkDelete.Checked)
					intFDel	= 1;
				intFEdit = 0;
				if (this.chkUpdate.Checked)
					intFEdit	= 1;
				intFRun = 0;
				if (this.chkView.Checked)
					intFRun	= 1;
			}
			#endregion

			try
			{
				strFunctionID = this.txtFuncID.Value.Trim();
				cmd.Parameters.Clear();
				param = cmd.Parameters.Add("@Action",SqlDbType.NVarChar,30);
				param.Value = "AddNew";
				param = cmd.Parameters.Add("@result",SqlDbType.NVarChar,20);
				param.Direction = ParameterDirection.Output;
				param = cmd.Parameters.Add("@UserGroupID",SqlDbType.NVarChar,20);
				param.Value = this.txtUserID.Value.Trim();
				param = cmd.Parameters.Add("@FunctionID",SqlDbType.NVarChar,12);
				param.Value = strFunctionID;
				param = cmd.Parameters.Add("@FRun",SqlDbType.Bit);
				param.Value = intFRun;
				param = cmd.Parameters.Add("@FEdit",SqlDbType.Bit);
				param.Value = intFEdit;
				param = cmd.Parameters.Add("@FDel",SqlDbType.Bit);
				param.Value = intFDel;
				param = cmd.Parameters.Add("@FAdd",SqlDbType.Bit);
				param.Value = intFAdd;
				param = cmd.Parameters.Add("@FApp",SqlDbType.Bit);
				param.Value = intFApp;
				cmd.ExecuteNonQuery();
				strresult = Convert.ToString(cmd.Parameters["@result"].Value);
				if (strresult == "-1" || strresult == "-2")
				{
					cmd.Transaction.Rollback();
					this.lblErr.Text = "Error occurence!";
					return "";
				}
			}
			catch(Exception exp)
			{
				cmd.Dispose();
				this.lblErr.Text = exp.Message.ToString();
				cmd.Transaction.Rollback();
				return "";
			}
			return this.txtUserID.Value.Trim();
		}
		#endregion

		#region Creater: LANHTD, 29/04/04, btnAddRight_Click
		private void btnAddRight_Click(object sender, System.EventArgs e)
		{
			SqlCommand cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				this.lblErr.Text = "";
				string strUserID = this.txtUserID.Value.Trim();
				
				#region Khai bao cau lenh
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "UMS_sptblPermisstion";

				SqlParameter param = new SqlParameter();
				#endregion
				
				//Neu chi cap nhat quyen cho mot tinh nang duoc chon, khi chon item command
				if (this.txtFuncID.Value.Trim() != "")
				{
					if (Impact_UMS_tblPermission_Update(cmd,param,IsAddAllRight)== "")
						return;
				}
					//Neu thiet lap cho tap hop tinh nag
				else
				{
					//Xoa tat ca quyen cua nguoi dung tren module
					if (Impact_UMS_tblPermission_DeletePermission(cmd,param) == "")
						return;
				
					//Neu la quyen tren module
					if (rdRightMod.Checked)
					{
						if (Impact_UMS_tblPermission_AddNewModulePer(cmd,param) == "")
							return;
					}
					else//Cap quyen theo tinh nang
					{
						if (Impact_UMS_tblPermission_AddNewFunctionPer(cmd,param,IsAddAllRight) == "")
							return;
					}
				}

				#region Hoan tat cau lenh
				cmd.Transaction.Commit();
				this.FuncModRight();
				this.BindDataGrid();
				this.btnSetupAgain_Click(sender,e);
				#endregion
				
			}
			catch(Exception exp)
			{
				this.lblErr.Text = exp.Message.ToString();
				if (cmd.Transaction != null)cmd.Transaction.Rollback();
			}
			finally
			{
				cmd.Dispose();
				SQLconn.Dispose();
			}
		}
		#endregion

		#region Creater: LANHTD, 23/09/04,FuncModRight//View grid functionID ?
		private void FuncModRight()
		{
			if (rdRightMod.Checked)
				this.trFunction.Attributes.Add("style","DISPLAY: none");
			else
				this.trFunction.Attributes.Remove("style");
		}
		#endregion

		#region Creater: LANHTD, 23/09/04. btnDeleteAllRight_Click
		private void btnDeleteAllRight_Click(object sender, System.EventArgs e)
		{
			SqlCommand cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				this.lblErr.Text = "";
				string strUserID = this.txtUserID.Value.Trim();
				
				#region Khai bao cau lenh
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "UMS_sptblPermisstion";

				SqlParameter param = new SqlParameter();
				#endregion

				//Xoa tat ca quyen cua nguoi dung tren module
				if (Impact_UMS_tblPermission_DeletePermission(cmd,param) == "")
					return;
				
				#region Hoan tat cau lenh
				cmd.Transaction.Commit();
				
				this.FuncModRight();
				this.BindDataGrid();
				this.MakeCheckBoxControls(false);
				#endregion
				
			}
			catch(Exception exp)
			{
				this.lblErr.Text = exp.Message.ToString();
				cmd.Transaction.Rollback();
			}
			finally
			{
				cmd.Dispose();
				SQLconn.Dispose();
			}
		}
		#endregion

		#region Creater Lanhtd, 23/09/04, btnSetupAgain_Click
		private void btnSetupAgain_Click(object sender, System.EventArgs e)
		{
			this.txtFuncID.Value = "";
			this.rdRightFunc.Checked = true;
			this.lblRightTitle.Text = "Thiết lập quyền";
		}
		#endregion

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			this.txtUserAccount.Text = txtChoseUserName.Value.Trim();
			this.txtUserID.Value = txtChoseUserID.Value.Trim();
			if(txtChoseFGroup.Value.Trim() == "True")
			{
				this.rdGroup.Checked = true;
				this.rdUser.Checked = false;
			}
			else
			{
				this.rdGroup.Checked = false;
				this.rdUser.Checked = true;
			}
			this.FillData();
		}

		private void btnPermission_Click(object sender, System.EventArgs e)
		{
		
		}

	}
}
