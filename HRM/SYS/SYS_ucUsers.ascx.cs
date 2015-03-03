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
	///		Summary description for SYS_ucUsers.
	/// </summary>
	public class SYS_ucUsers : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.DataGrid dgUserAccount;
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.DataGrid dgGroups;
		protected System.Web.UI.WebControls.DataGrid dgGroupsSelected;
		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.WebControls.LinkButton btnDeletePass;
		protected System.Web.UI.WebControls.LinkButton btnValid;
		protected System.Web.UI.WebControls.LinkButton btnAdmin;
		protected System.Web.UI.WebControls.LinkButton btnAddNew;
		protected System.Web.UI.WebControls.LinkButton btnDeleteUser;
		protected System.Web.UI.WebControls.LinkButton btnChoose;
		protected System.Web.UI.WebControls.LinkButton btnSaveUserGroup;
		protected System.Web.UI.WebControls.LinkButton btnDelete;

		#region Page_Load
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				this.BindDataGridUserAccount();
				this.dgGroupsSelected.DataBind();
				DataTable dtb = clsCommon.GetDataTable("UMS_sptblUserAccount 'GetAllGroup', @result=''");
				this.BindDataGridGroups(this.dgGroups,dtb);
				dtb = new DataTable();
				this.dgGroupsSelected.DataSource = dtb;
				this.dgGroupsSelected.DataBind();
				dtb.Dispose();
					
			}
			this.btnDeleteUser.Attributes.Add("OnClick","return CheckGrid(this)");
			this.btnDeletePass.Attributes.Add("OnClick","return CheckGrid(this)");
			this.btnValid.Attributes.Add("OnClick","return CheckGrid(this)");
			this.btnAdmin.Attributes.Add("OnClick","return CheckGrid(this)");
			this.btnChoose.Attributes.Add("OnClick","return CheckGrid(this)");
			this.btnDelete.Attributes.Add("OnClick","return CheckGrid(this)");
			this.btnSaveUserGroup.Attributes.Add("OnClick","return CheckGrid(this)");
			
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.dgUserAccount.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgUserAccount_PageIndexChanged);
			this.btnDeletePass.Click += new System.EventHandler(this.btnDeletePass_Click);
			this.btnValid.Click += new System.EventHandler(this.btnValid_Click);
			this.btnAdmin.Click += new System.EventHandler(this.btnAdmin_Click);
			this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
			this.btnDeleteUser.Click += new System.EventHandler(this.btnDeleteUser_Click);
			this.dgGroups.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgGroups_PageIndexChanged);
			this.btnChoose.Click += new System.EventHandler(this.btnChoose_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnSaveUserGroup.Click += new System.EventHandler(this.btnSaveUserGroup_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	
		#region Creater: LANHTD, 22/09/04, MakeDataToGrid to bind to dgGroups and dgGroupSelected when choose or remove groups
		private void MakeDataToGrid(DataGrid GridDes, DataGrid GridSource)
		{
			DataTable dtb = new DataTable();//Bang ket vao grid GridDes
			DataTable dtbChoose = new DataTable();//Bang ket vao grid GridSource
			try
			{
				dtb.Columns.Add(new DataColumn("UserGroupID"));
				dtb.Columns.Add(new DataColumn("UserGroupName"));
				
				dtbChoose.Columns.Add(new DataColumn("UserGroupID"));
				dtbChoose.Columns.Add(new DataColumn("UserGroupName"));
		
				for (int i=0; i<GridDes.Items.Count; i++)
				{
					DataRow row = dtb.NewRow();
					row["UserGroupID"] = GridDes.Items[i].Cells[0].Text.Trim();
					row["UserGroupName"] = GridDes.Items[i].Cells[1].Text.Trim();
					dtb.Rows.Add(row);
				}
				for (int i=0; i<GridSource.Items.Count; i++)
				{
					CheckBox chk = new CheckBox();
					if (GridSource.ID.Trim() == "dgGroups")
						chk = (CheckBox)this.dgGroups.Items[i].FindControl("chkChose");
					else
						chk = (CheckBox)this.dgGroupsSelected.Items[i].FindControl("chkRemove");
					if (chk.Checked)
					{
						DataRow row = dtb.NewRow();
						row["UserGroupID"] = GridSource.Items[i].Cells[0].Text.Trim();
						row["UserGroupName"] = GridSource.Items[i].Cells[1].Text.Trim();
						dtb.Rows.Add(row);
					}
					else
					{
						DataRow row = dtbChoose.NewRow();
						row["UserGroupID"] = GridSource.Items[i].Cells[0].Text.Trim();
						row["UserGroupName"] = GridSource.Items[i].Cells[1].Text.Trim();
						dtbChoose.Rows.Add(row);
					}
				}
				this.BindDataGridGroups(GridDes,dtb);
				this.BindDataGridGroups(GridSource,dtbChoose);
			}
			catch(Exception exp)
			{
				this.lblErr.Text = exp.Message.ToString();
			}
			finally
			{
				dtb.Dispose();
				dtbChoose.Dispose();
			}
		}
		#endregion

		#region Creater: LANHTD, 22/09/04. Bind data to grid dgUserAccount
		private void BindDataGridUserAccount()
		{
			DataTable dtb = new DataTable();
			LinkButton hpAccount = new LinkButton();
			try
			{
				dtb = clsCommon.GetDataTable("UMS_sptblUserAccount 'GetAllUsers', @result=''");
				dgUserAccount.PageSize = 10;
				dgUserAccount.PagerStyle.Mode = PagerMode.NumericPages;
				dgUserAccount.DataSource = dtb;
				dgUserAccount.CurrentPageIndex = 0;
				dgUserAccount.DataBind();
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
					hpAccount = (LinkButton)this.dgUserAccount.Items[i].FindControl("hpAccount");
					if (hpAccount != null)
						hpAccount.Attributes.Add("OnClick","return OpenDetails('" + hpAccount.Text.Trim() + "')");
				}
			}
			catch(Exception exp)
			{
				this.lblErr.Text = exp.Message.ToString();
			}
			finally
			{
				dtb.Dispose();
				hpAccount.Dispose();
			}
		}
		#endregion

		#region Creater: LANHTD, 22/09/04. Bind data to grid dgGroups
		private void BindDataGridGroups(DataGrid GridBindData, DataTable pTable)
		{
			try
			{
				GridBindData.DataSource = pTable;
				GridBindData.DataBind();
				if (GridBindData.ID == "dgGroups")
				{
					for(int i = 0; i<GridBindData.Items.Count; i++)
					{
						LinkButton lnkGroup = (LinkButton)GridBindData.Items[i].FindControl("lnkGroup");
						string strGroupID = GridBindData.Items[i].Cells[0].Text.Trim();
						if (lnkGroup != null)
						{
							lnkGroup.Attributes.Add("OnClick","return OpenDetails_G('" + strGroupID.Trim() + "')");
						}
					}
				}
			}
			catch(Exception exp)
			{
				this.lblErr.Text = exp.Message.ToString();
			}
		}
		#endregion

		#region Creater: LANHTD, 22/09/04, btnChoose_Click
		private void btnChoose_Click(object sender, System.EventArgs e)
		{
			this.lblErr.Text = "";
			try
			{
				this.lblErr.Text = "";
				this.MakeDataToGrid(this.dgGroupsSelected,this.dgGroups);
			}
			catch(Exception exp)
			{
				this.lblErr.Text = exp.Message.ToString();
				return;
			}
		}
		#endregion

		#region Creater: LANHTD, 22/09/04, btnDeleteUser_Click
		private void btnDeleteUser_Click(object sender, System.EventArgs e)
		{
			this.lblErr.Text = "";
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				this.lblErr.Text = "";
				string strUserID = "";
				string strresult = "";
				
				#region Xoa nguoi dung

				#region Khai bao cau lenh
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "UMS_sptblUserAccount";

				SqlParameter param;
				#endregion
				
				#region Xoa nguoi dung
				for (int i=0; i<this.dgUserAccount.Items.Count; i++)
				{
					if (((CheckBox)this.dgUserAccount.Items[i].FindControl("chkSelect")).Checked)
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
							cmd.Transaction.Rollback();
							return;
						}
					}
				}
				#endregion

				cmd.Transaction.Commit();
				this.BindDataGridUserAccount();
				#endregion
			}
			catch(Exception exp)
			{
				this.lblErr.Text = exp.Message.ToString();
				if (cmd.Transaction != null) cmd.Transaction.Rollback();
			}
			finally
			{
				cmd.Dispose();
				SQLconn.Dispose();
			}
		}
		#endregion

		#region Creater: LANHTD, 22/09/04, btnDelete_Click
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			this.lblErr.Text = "";
			try
			{
				this.lblErr.Text = "";
				this.MakeDataToGrid(dgGroups,dgGroupsSelected);
			}
			catch(Exception exp)
			{
				this.lblErr.Text = exp.Message.ToString();
			}
		}
		#endregion

		#region Creater: LANHTD, 22/09/04, btnDeletePass_Click
		private void btnDeletePass_Click(object sender, System.EventArgs e)
		{
			this.lblErr.Text = "";
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				this.lblErr.Text = "";
				string strUserID = "";
				string strresult = "";
				
				#region Xoa password nguoi dung

				#region Khai bao cau lenh
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "UMS_sptblUserAccount";

				SqlParameter param;
				#endregion
				
				#region Xoa password nguoi dung
				for (int i=0; i<this.dgUserAccount.Items.Count; i++)
				{
					if (((CheckBox)this.dgUserAccount.Items[i].FindControl("chkSelect")).Checked)
					{
						try
						{
							strUserID = this.dgUserAccount.Items[i].Cells[1].Text.Trim();
							cmd.Parameters.Clear();
							param = cmd.Parameters.Add("@Action",SqlDbType.NVarChar,30);
							param.Value = "UpdatePassword";
							param = cmd.Parameters.Add("@result",SqlDbType.NVarChar,20);
							param.Direction = ParameterDirection.Output;
							param = cmd.Parameters.Add("@UserGroupID",SqlDbType.NVarChar,20);
							param.Value = strUserID;
							param = cmd.Parameters.Add("@NewPassword",SqlDbType.NVarChar,50);
							param.Value = strUserID;//mac dinh password la ten user login
							cmd.ExecuteNonQuery();
							strresult = Convert.ToString(cmd.Parameters["@result"].Value);
							if (strresult == "-5")
							{
								if (cmd.Transaction != null) cmd.Transaction.Rollback();
								this.lblErr.Text = "Error occured on deleting password of checked user " + i.ToString();					
								return;
							}
						}
						catch(Exception exp)
						{
							this.lblErr.Text = exp.Message.ToString();
							cmd.Transaction.Rollback();
							return;
						}
					}
				}
				#endregion

				cmd.Transaction.Commit();
				this.BindDataGridUserAccount();
				#endregion
			}
			catch(Exception exp)
			{
				this.lblErr.Text = exp.Message.ToString();
				if (cmd.Transaction != null) cmd.Transaction.Rollback();
			}
			finally
			{
				cmd.Dispose();
				SQLconn.Dispose();
			}
		}

		#endregion

		#region Creater: LANHTD, 22/09/04, btnAdmin_Click
		private void btnAdmin_Click(object sender, System.EventArgs e)
		{
			this.lblErr.Text = "";
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				this.lblErr.Text = "";
				string strUserID = "";
				string strresult = "";
				
				#region Thay doi quyen admin

				#region Khai bao cau lenh
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "UMS_sptblUserAccount";

				SqlParameter param;
				#endregion
				
				#region Thay doi quyen admin
				for (int i=0; i<this.dgUserAccount.Items.Count; i++)
				{
					if (((CheckBox)this.dgUserAccount.Items[i].FindControl("chkSelect")).Checked)
					{
						try
						{
							strUserID = this.dgUserAccount.Items[i].Cells[1].Text.Trim();
							cmd.Parameters.Clear();
							param = cmd.Parameters.Add("@Action",SqlDbType.NVarChar,30);
							param.Value = "UpdateFAdm";
							param = cmd.Parameters.Add("@result",SqlDbType.NVarChar,20);
							param.Direction = ParameterDirection.Output;
							param = cmd.Parameters.Add("@UserGroupID",SqlDbType.NVarChar,20);
							param.Value = strUserID;
							cmd.ExecuteNonQuery();
							strresult = Convert.ToString(cmd.Parameters["@result"].Value);
							if (strresult == "-6")
							{
								if (cmd.Transaction != null) cmd.Transaction.Rollback();
								this.lblErr.Text = "Error occured on updating administrator right of checked user " + i.ToString();					
								return;
							}
						}
						catch(Exception exp)
						{
							this.lblErr.Text = exp.Message.ToString();
							cmd.Transaction.Rollback();
							return;
						}
					}
				}
				#endregion

				cmd.Transaction.Commit();
				this.BindDataGridUserAccount();
				#endregion
			}
			catch(Exception exp)
			{
				this.lblErr.Text = exp.Message.ToString();
				if (cmd.Transaction != null) cmd.Transaction.Rollback();
			}
			finally
			{
				cmd.Dispose();
				SQLconn.Dispose();
			}
		}
		#endregion

		#region Creater: LANHTD, 22/09/04. btnAddNew_Click
		private void btnAddNew_Click(object sender, System.EventArgs e)
		{
			this.lblErr.Text = "";
			Response.Redirect("Editpage.aspx?ModuleID=SYS&ParentID=157&FunctionID=316&Ascx=SYS/SYS_ucAddAccount.ascx");
		}
		#endregion

		#region dgUserAccount_PageIndexChanged
		private void dgUserAccount_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.lblErr.Text = "";
			this.BindDataGridUserAccount();
			this.dgUserAccount.CurrentPageIndex = e.NewPageIndex;
			this.dgUserAccount.DataBind();
			LinkButton hpAccount = new LinkButton();
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
				hpAccount = (LinkButton)this.dgUserAccount.Items[i].FindControl("hpAccount");
				if (hpAccount != null)
					hpAccount.Attributes.Add("OnClick","return OpenDetails('" + hpAccount.Text.Trim() + "')");
			}
			hpAccount.Dispose();
		}
		#endregion

		#region  Creater: LANHTD, 28/04/04, btnValid_Click
		private void btnValid_Click(object sender, System.EventArgs e)
		{
			this.lblErr.Text = "";
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				this.lblErr.Text = "";
				string strUserID = "";
				string strresult = "";
				
				#region Change run web right

				#region Khai bao cau lenh
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "UMS_sptblUserAccount";

				SqlParameter param;
				#endregion
				
				#region Change run web right
				for (int i=0; i<this.dgUserAccount.Items.Count; i++)
				{
					if (((CheckBox)this.dgUserAccount.Items[i].FindControl("chkSelect")).Checked)
					{
						try
						{
							strUserID = this.dgUserAccount.Items[i].Cells[1].Text.Trim();
							cmd.Parameters.Clear();
							param = cmd.Parameters.Add("@Action",SqlDbType.NVarChar,30);
							param.Value = "UpdateFValid";
							param = cmd.Parameters.Add("@result",SqlDbType.NVarChar,20);
							param.Direction = ParameterDirection.Output;
							param = cmd.Parameters.Add("@UserGroupID",SqlDbType.NVarChar,20);
							param.Value = strUserID;
							cmd.ExecuteNonQuery();
							strresult = Convert.ToString(cmd.Parameters["@result"].Value);
							if (strresult == "-7")
							{
								cmd.Transaction.Rollback();
								this.lblErr.Text = "Error occurence on updating runing this site right of user " + i.ToString();					
								return;
							}
						}
						catch(Exception exp)
						{
							this.lblErr.Text = exp.Message.ToString();
							cmd.Transaction.Rollback();
							return;
						}
					}
				}
				#endregion

				cmd.Transaction.Commit();
				this.BindDataGridUserAccount();
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

		private void btnSaveUserGroup_Click(object sender, System.EventArgs e)
		{
			this.lblErr.Text = "";
			string strGroupID = "";
			string strresult = "";
			string strUserID = "";
			#region Luu group user

			#region Khai bao
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "UMS_sptblUserGroup";

				SqlParameter param;
				#endregion
				
				/*#region Xoa tat ca cac group cua user duoc chonj
				for (int i=0; i<this.dgUserAccount.Items.Count; i++)
				{
					if (((CheckBox)this.dgUserAccount.Items[i].FindControl("chkSelect")).Checked)
					{
						strUserID = this.dgUserAccount.Items[i].Cells[1].Text;

						try
						{
							cmd.Parameters.Clear();
							param = cmd.Parameters.Add("@Action",SqlDbType.NVarChar,30);
							param.Value = "DeleteGroupByUser";
							param = cmd.Parameters.Add("@result",SqlDbType.NVarChar,20);
							param.Direction = ParameterDirection.Output;
							param = cmd.Parameters.Add("@UserID",SqlDbType.NVarChar,20);
							param.Value = strUserID;
							cmd.ExecuteNonQuery();
							strresult = Convert.ToString(cmd.Parameters["@result"].Value);
							if (strresult == "-2")
							{
								cmd.Transaction.Rollback();
								this.lblErr.Text = "Error occured on deleting groups of checked user" + i.ToString();					
								return;
							}
						}
						catch(Exception exp)
						{
							this.lblErr.Text = exp.Message.ToString();
							cmd.Transaction.Rollback();
							return;
						}
					}                
				}
				#endregion*/
				
				#region Luu UserID, GroupID tuong ung duoc chon
				for (int i=0; i<this.dgUserAccount.Items.Count; i++)
				{
					if (((CheckBox)this.dgUserAccount.Items[i].FindControl("chkSelect")).Checked)
					{
						for (int j=0; j<this.dgGroupsSelected.Items.Count; j++)
						{
							try
							{
								strUserID = this.dgUserAccount.Items[i].Cells[1].Text.Trim();
								strGroupID = this.dgGroupsSelected.Items[j].Cells[0].Text.Trim();
								cmd.Parameters.Clear();
								param = cmd.Parameters.Add("@Action",SqlDbType.NVarChar,30);
								param.Value = "AddNew";
								param = cmd.Parameters.Add("@result",SqlDbType.NVarChar,20);
								param.Direction = ParameterDirection.Output;
								param = cmd.Parameters.Add("@UserID",SqlDbType.NVarChar,20);
								param.Value = strUserID;
								param = cmd.Parameters.Add("@GroupID",SqlDbType.NVarChar,20);
								param.Value = strGroupID;
								cmd.ExecuteNonQuery();
								strresult = Convert.ToString(cmd.Parameters["@result"].Value);
								if (strresult == "-1")
								{
									cmd.Transaction.Rollback();
									this.lblErr.Text = "Error occured on saving group " + i.ToString();					
									return;
								}
							}
							catch(Exception exp)
							{
								this.lblErr.Text = exp.Message.ToString();
								cmd.Transaction.Rollback();
								return;
							}
						}
					}
				}
				#endregion

				cmd.Transaction.Commit();
				this.MakeDataToGrid(this.dgGroupsSelected,this.dgGroups);
				#endregion
			}
			catch(Exception exp)
			{
				this.lblErr.Text = exp.Message.Trim();
			}
			finally
			{
				if (cmd != null) cmd.Dispose();
				if (SQLconn != null) SQLconn.Dispose();
			}
		}

		private void dgGroups_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			DataTable dtb = clsCommon.GetDataTable("UMS_sptblUserAccount 'GetAllGroup', @result=''");
			this.BindDataGridGroups(this.dgGroups,dtb);
			this.dgGroups.CurrentPageIndex = e.NewPageIndex;
			this.dgGroups.DataBind();
		}
	}
}

