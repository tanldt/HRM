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
	using GridSort;
	/// <summary>
	///		Summary description for SYS_ucAddAccount.
	/// </summary>
	public class SYS_ucAddAccount : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblUserIDCaption;
		protected System.Web.UI.WebControls.TextBox txtEmpID;
		protected System.Web.UI.WebControls.Label lblUserNameCaption;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.TextBox txtUserID;
		protected System.Web.UI.WebControls.Label lblPasswordCaption;
		protected System.Web.UI.WebControls.TextBox txtPassword;
		protected System.Web.UI.WebControls.Label lblConfirmPassCaption;
		protected System.Web.UI.WebControls.TextBox txtConfirmPass;
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.TextBox txtUserName;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.LinkButton btnSaveRecord;
		protected System.Web.UI.WebControls.LinkButton btnAddNew;
		protected System.Web.UI.WebControls.LinkButton btnSearch;
		protected System.Web.UI.WebControls.DataGrid dgUserAccount;
		protected ColumnList uctrlColumns;

		#region Page_Load
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				this.BindDataGridUserAccount();

				//Hau
				DataGridSort.AddItemColumn(uctrlColumns, dgUserAccount);//
			}
			this.btnSaveRecord.Attributes.Add("OnClick","return CheckSave()");
			this.btnDelete.Attributes.Add("OnClick","return CheckDelete()");

			//Hau
			ButtonClick();//
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
			this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
			this.btnSaveRecord.Click += new System.EventHandler(this.btnSaveRecord_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.dgUserAccount.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgUserAccount_PageIndexChanged);
			this.dgUserAccount.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dgUserAccount_SortCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region Creater: LANHTD, 22/09/04. Bind data to grid dgUserAccount
		private void BindDataGridUserAccount()
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb = clsCommon.GetDataTable("UMS_sptblUserAccount 'GetAllUsers', @result=''");
				/*
				dgUserAccount.DataSource = dtb;
				this.dgUserAccount.PageSize = txtPageRows.Text.Trim() != "0"?Convert.ToInt16(txtPageRows.Text.Trim()):Convert.ToInt16(20);
				if (txtPageRows.Text.Trim() == "0")
					this.txtPageRows.Text = "20";
				dgUserAccount.DataBind();
				this.lblTotalRows.Text = dtb.Rows.Count.ToString();
				*/
				dgUserAccount.CurrentPageIndex = 0;

				//Hau
				this.dgUserAccount.PageSize = uctrlColumns.iPageRows;
				if(this.uctrlColumns.bAdvMultiSort)
					DataGridSort.AdvancedMultiSort(dgUserAccount, dtb, uctrlColumns);
				else
					DataGridSort.Refresh(dgUserAccount, dtb);
				this.uctrlColumns.iTotalRows = dtb.Rows.Count;//

				for(int i=0; i<this.dgUserAccount.Items.Count; i++)
				{
					if (this.dgUserAccount.Items[i].Cells[4].Text.Trim() == "True")
						((CheckBox)this.dgUserAccount.Items[i].FindControl("chkValid")).Checked = true;
					else
						((CheckBox)this.dgUserAccount.Items[i].FindControl("chkValid")).Checked = false;
					if (this.dgUserAccount.Items[i].Cells[6].Text.Trim() == "True")
						((CheckBox)this.dgUserAccount.Items[i].FindControl("chkAdm")).Checked = true;
					else
						((CheckBox)this.dgUserAccount.Items[i].FindControl("chkAdm")).Checked = false;
				}
			}
			catch(Exception exp)
			{
				this.lblErr.Text = exp.Message.ToString();
			}
			finally
			{
				dtb.Dispose();
			}
		}
		#endregion

		#region Creater: LANHTD, 22/09/04, CheckValidData
		private bool CheckValidData()
		{
			try
			{
				string strUserGroupID = this.txtUserID.Text.Trim();
				if (Session["UserGroupID"] == null)
				{
					DataTable dtb = clsCommon.GetDataTable("UMS_sptblUserAccount 'GetByID', @result='',@UserGroupID='" + strUserGroupID + "'");
					if (dtb.Rows.Count > 0)
					{
						//this.lblErr.Text = "This username has been used by another person. Please choose another username.";
						clsChangeLang.popupWindow(this.Parent,"Add0006","VN","",0);
						return false;
					}
				}
				return true;
			}
			catch(Exception exp)
			{
				this.lblErr.Text = exp.Message.ToString();
				return false;
			}
		}
		#endregion

		#region dgUserAccount_PageIndexChanged
		private void dgUserAccount_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			/*
			this.lblErr.Text = "";
			this.BindDataGridUserAccount();
			this.dgUserAccount.CurrentPageIndex = e.NewPageIndex;
			dgUserAccount.DataBind();
			*/

			//Hau
			try
			{
				DataGridSort.Grid_IndexChanged(dgUserAccount, Read_Data(), e);
			}
			catch{}//

			for(int i=0; i<this.dgUserAccount.Items.Count; i++)
			{
				if (this.dgUserAccount.Items[i].Cells[4].Text.Trim() == "True")
					((CheckBox)this.dgUserAccount.Items[i].FindControl("chkValid")).Checked = true;
				else
					((CheckBox)this.dgUserAccount.Items[i].FindControl("chkValid")).Checked = false;
				if (this.dgUserAccount.Items[i].Cells[6].Text.Trim() == "True")
					((CheckBox)this.dgUserAccount.Items[i].FindControl("chkAdm")).Checked = true;
				else
					((CheckBox)this.dgUserAccount.Items[i].FindControl("chkAdm")).Checked = false;
			}
		}
		#endregion

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				this.lblErr.Text = "";
				string strUserID = "";
				string strresult = "";
				
				#region Xoá người dùng

				#region Khai báo câu lệnh
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "UMS_sptblUserAccount";

				SqlParameter param;
				#endregion
				
				#region Xoá người dùng
				for (int i=0; i<this.dgUserAccount.Items.Count; i++)
				{
					CheckBox chkSelect = (CheckBox)this.dgUserAccount.Items[i].FindControl("chkSelect");
					if (chkSelect.Checked)
					{
						try
						{
							strUserID = this.dgUserAccount.Items[i].Cells[1].Text.Trim();
							cmd.Parameters.Clear();
							param = cmd.Parameters.Add("@Action",SqlDbType.NVarChar,30);
							param.Value = "Delete";
							param = cmd.Parameters.Add("@result",SqlDbType.NVarChar,20);
							param.Direction = ParameterDirection.Output;
							param = cmd.Parameters.Add("@UserGroupID",SqlDbType.NVarChar,20);
							param.Value = strUserID;
							cmd.ExecuteNonQuery();
							strresult = Convert.ToString(cmd.Parameters["@result"].Value);
							if (strresult == "-3")
							{
								cmd.Transaction.Rollback();
								this.lblErr.Text = "Error occured on deleting user " + i.ToString();					
								return;
							}
						}
						catch(Exception exp)
						{
							this.lblErr.Text = exp.Message.ToString();
							if (cmd.Transaction != null) cmd.Transaction.Rollback();
							return;
						}
					}
				}
				#endregion

				#region Hoàn tất câu lệnh thay đổi quyền quản trị
				if (cmd.Transaction != null) cmd.Transaction.Commit();
				///end
				
				this.btnAddNew_Click(sender,e);
				this.BindDataGridUserAccount();
				#endregion
				#endregion
			}
			catch(Exception exp)
			{
				this.lblErr.Text = exp.Message.ToString();
			}
			finally
			{
				cmd.Dispose();
				SQLconn.Dispose();
			}
		}

		private void btnSaveRecord_Click(object sender, System.EventArgs e)
		{
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
						
			try
			{
				if (!this.CheckValidData())
					return;
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "UMS_sptblUserAccount";
				cmd.Parameters.Clear();
				SqlParameter param;
				
				param = cmd.Parameters.Add("@Action",SqlDbType.NVarChar,30);
				if (Session["UserGroupID"] != null)
					param.Value = "Update";
				else
					param.Value = "AddNew";
				param = cmd.Parameters.Add("@LanguageId",SqlDbType.Char,2);
				param.Value = "VI";
				param = cmd.Parameters.Add("@result",SqlDbType.NVarChar,12);
				param.Direction = ParameterDirection.Output;
				param = cmd.Parameters.Add("@UserGroupID",SqlDbType.NVarChar,20);
				param.Value = this.txtUserID.Text.Trim();
				param = cmd.Parameters.Add("@Password",SqlDbType.NVarChar,50);
				param.Value = this.txtPassword.Text.Trim();
				param = cmd.Parameters.Add("@UserGroupName",SqlDbType.NVarChar,50);
				param.Value = this.txtUserName.Text.Trim();
				param = cmd.Parameters.Add("@FGroup",SqlDbType.Bit);
				param.Value = 0;
				param = cmd.Parameters.Add("@EmployeeID",SqlDbType.NVarChar,12);
				if (this.txtEmpID.Text.Trim() != "")
					param.Value = this.txtEmpID.Text.Trim();
				else
					param.Value = DBNull.Value;
				cmd.ExecuteNonQuery();
				
				string strresult = Convert.ToString(cmd.Parameters["@result"].Value);
				if (strresult == "-1" || strresult == "-2")
				{
					this.lblErr.Text = "Error occurence!";
					if (cmd.Transaction != null) cmd.Transaction.Rollback();
					return;
				}
				if (cmd.Transaction != null) cmd.Transaction.Commit();
				this.BindDataGridUserAccount();
				this.btnAddNew_Click(sender,e);
			}
			catch(Exception exp)
			{
				//this.lblErr.Text = exp.Message.ToString();
				clsChangeLang.popupWindow(this.Parent,"Add0006","VN","",0);
				if (cmd.Transaction != null) cmd.Transaction.Rollback();
				return;
			}
			finally
			{
				cmd.Dispose();
				SQLconn.Dispose();
			}
		}

		private void btnAddNew_Click(object sender, System.EventArgs e)
		{
			this.lblErr.Text = "";
			this.txtUserID.Text = "";
			this.txtUserName.Text = "";
			this.txtPassword.Text = "";
			this.txtConfirmPass.Text = "";
			this.txtEmpID.Text = "";
		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			this.BindDataGridUserAccount();
		}

		#region Cac su kien xu li thao tac Sort (Hau)

		private DataTable Read_Data()
		{
			DataTable dtb = new DataTable();
			dtb = clsCommon.GetDataTable("UMS_sptblUserAccount 'GetAllUsers', @result=''");
			return dtb;
		}
		
		private void dgUserAccount_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			try
			{
				DataGridSort.Grid_Sort(dgUserAccount, Read_Data(), e, uctrlColumns);
			}
			catch{}
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
				DataGridSort.AdvancedMultiSort(dgUserAccount, Read_Data(), uctrlColumns);
			}
			catch{}
		}

		private void ButtonPRChanged_ServerClick(object sender, EventArgs e)
		{
			try
			{
				dgUserAccount.PageSize = uctrlColumns.iPageRows;
				dgUserAccount.CurrentPageIndex = 0;
				DataView dv = new DataView(Read_Data());
				dv.Sort = DataGridSort.sMulSort;
				dgUserAccount.DataSource = dv;
				dgUserAccount.DataBind();
			}
			catch{}
		}
		#endregion
	}
}

