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
	///		Summary description for SYS_ucGroups.
	/// </summary>
	public class SYS_ucGroups : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.DataGrid dgGroups;
		protected System.Web.UI.WebControls.Label lblGroupIDCaption;
		protected System.Web.UI.WebControls.TextBox txtGroupID;
		protected System.Web.UI.WebControls.Label lblGroupNameCaption;
		protected System.Web.UI.WebControls.TextBox txtGroupName;
		protected System.Web.UI.WebControls.LinkButton btnAddNew;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.LinkButton btnSearch;
		protected System.Web.UI.HtmlControls.HtmlTableRow trAddNew;
		protected ColumnList uctrlColumns;
		protected System.Web.UI.WebControls.Literal ltlAlert;

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				this.BindDataGrid();

				//Hau
				DataGridSort.AddItemColumn(uctrlColumns, dgGroups);//
			}
			this.btnSave.Attributes.Add("OnClick","return CheckSave()");
			this.btnDelete.Attributes.Add("OnClick","return CheckDelete()");

			//Hau
			ButtonClick();//
		}

		private bool CheckValidData()
		{
			DataTable dtb = new DataTable();
			try
			{
				if (this.txtGroupID.ReadOnly == false)
				{
					dtb = clsCommon.GetDataTable("UMS_sptblUserAccount 'GetByID', @result='',@UserGroupID='" + this.txtGroupID.Text.Trim() + "'");
					if (dtb.Rows.Count > 0)
					{
						//this.lblErr.Text = "This group ID has already existed!";
						clsChangeLang.popupWindow(this.Parent,"ucG0004","VN","",0);
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
			finally
			{
				dtb.Dispose();
			}
		}

		private void FillData(DataRow row)
		{
			try
			{
				this.txtGroupID.ReadOnly = true;
				this.txtGroupID.Text = row["UserGroupID"].ToString().Trim();
				this.txtGroupName.Text = row["UserGroupName"].ToString().Trim();
			}
			catch(Exception exp)
			{
				this.lblErr.Text = exp.Message.ToString();
			}
		}

		private void BindDataGrid()
		{
			DataTable dtb = new DataTable();
			try
			{
				dtb = clsCommon.GetDataTable("UMS_sptblUserAccount 'GetAllGroup', @result=''");
				/*
				this.dgGroups.DataSource = dtb;
				this.dgGroups.PageSize = txtPageRows.Text.Trim() != "0"?Convert.ToInt16(txtPageRows.Text.Trim()):Convert.ToInt16(20);
				if (txtPageRows.Text.Trim() == "0")
					this.txtPageRows.Text = "20";
				dgGroups.DataBind();
				this.lblTotalRows.Text = dtb.Rows.Count.ToString();
				*/
				dgGroups.CurrentPageIndex = 0;

				//Hau
				this.dgGroups.PageSize = uctrlColumns.iPageRows;
				if(this.uctrlColumns.bAdvMultiSort)
					DataGridSort.AdvancedMultiSort(dgGroups, dtb, uctrlColumns);
				else
					DataGridSort.Refresh(dgGroups, dtb);
				this.uctrlColumns.iTotalRows = dtb.Rows.Count;//
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
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.dgGroups.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgGroups_ItemCommand);
			this.dgGroups.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgGroups_PageIndexChanged);
			this.dgGroups.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dgGroups_SortCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnAddNew_Click(object sender, System.EventArgs e)
		{
			this.lblErr.Text = "";
			this.txtGroupID.ReadOnly = false;
			this.txtGroupName.Text = "";
			this.txtGroupID.Text = "";
		}

		private void btnSave_Click(object sender, System.EventArgs e)
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
				if (this.txtGroupID.ReadOnly)
					param.Value = "Update";
				else
					param.Value = "AddNew";
				param = cmd.Parameters.Add("@LanguageId",SqlDbType.Char,2);
				param.Value = "VI";
				param = cmd.Parameters.Add("@result",SqlDbType.NVarChar,12);
				param.Direction = ParameterDirection.Output;
				param = cmd.Parameters.Add("@UserGroupID",SqlDbType.NVarChar,20);
				param.Value = this.txtGroupID.Text.Trim();
				param = cmd.Parameters.Add("@UserGroupName",SqlDbType.NVarChar,50);
				param.Value = this.txtGroupName.Text.Trim();
				param = cmd.Parameters.Add("@FGroup",SqlDbType.Bit);
				param.Value = 1;
				cmd.ExecuteNonQuery();

				string strresult = Convert.ToString(cmd.Parameters["@result"].Value);
				if (strresult == "-1" || strresult == "-2")
				{
					this.lblErr.Text = "Error occurence!";
					cmd.Transaction.Rollback();
					return;
				}
				
				cmd.Transaction.Commit();
				this.BindDataGrid();
				this.btnAddNew_Click(sender,e);
			}
			catch(Exception exp)
			{
				this.lblErr.Text = exp.Message.ToString();
				cmd.Transaction.Rollback();
				return;
			}
			finally
			{
				cmd.Dispose();
				SQLconn.Dispose();
			}
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				this.lblErr.Text = "";
				string strUserGroupID = "";
				string strresult = "";
				
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "UMS_sptblUserAccount";

				for (int i=0; i<this.dgGroups.Items.Count; i++)
				{
					CheckBox chkSelect = (CheckBox)this.dgGroups.Items[i].FindControl("chkSelect");
					if (chkSelect.Checked)
					{
						try
						{
							cmd.Parameters.Clear();
							cmd.Parameters.Add("@Action",SqlDbType.NVarChar,30).Value = "Delete";
							cmd.Parameters.Add("@LanguageId",SqlDbType.Char,2).Value = "EN";
							cmd.Parameters.Add("@result",SqlDbType.NVarChar,20).Direction = ParameterDirection.Output;
							cmd.Parameters.Add("@UserGroupID",SqlDbType.NVarChar,20).Value = dgGroups.Items[i].Cells[0].Text.Trim();
							cmd.ExecuteNonQuery();
							strresult = Convert.ToString(cmd.Parameters["@result"].Value);
							if (strresult == "-3" || strresult == "-4")
							{
								cmd.Transaction.Rollback();
								//this.lblErr.Text = "Không thể xoá nhóm này!";					
								clsChangeLang.popupWindow(this.Parent,"ucG0005","VN","",0);
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
				
				cmd.Transaction.Commit();
				this.btnAddNew_Click(sender,e);
				this.BindDataGrid();
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

		private void dgGroups_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			/*
			this.BindDataGrid();
			this.dgGroups.CurrentPageIndex = e.NewPageIndex;
			this.dgGroups.DataBind();
			*/

			//Hau
			try
			{
				DataGridSort.Grid_IndexChanged(dgGroups, Read_Data(), e);
			}
			catch{}//
		}

		private void dgGroups_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			if (e.CommandName == "hpLink")
			{
				string strGroupID = e.Item.Cells[0].Text.Trim();
				DataTable dtb = clsCommon.GetDataTable("UMS_sptblUserAccount 'GetByID', @result='',@UserGroupID='" + strGroupID + "'");
				if (dtb.Rows.Count > 0)
					this.FillData(dtb.Rows[0]);	
				else
				{
					this.lblErr.Text = "Error! Don't have this group on system.";
				}
				dtb.Dispose();
				ltlAlert.Text = "OpenDetails('" + e.Item.Cells[0].Text.Trim() + "')";
			}
		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			this.BindDataGrid();
		}

		#region Cac su kien xu li thao tac Sort (Hau)

		private DataTable Read_Data()
		{
			DataTable dtb = new DataTable();
			dtb = clsCommon.GetDataTable("UMS_sptblUserAccount 'GetAllGroup', @result=''");
			return dtb;
		}
		
		private void dgGroups_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			try
			{
				DataGridSort.Grid_Sort(dgGroups, Read_Data(), e, uctrlColumns);
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
				DataGridSort.AdvancedMultiSort(dgGroups, Read_Data(), uctrlColumns);
			}
			catch{}
		}

		private void ButtonPRChanged_ServerClick(object sender, EventArgs e)
		{
			try
			{
				dgGroups.PageSize = uctrlColumns.iPageRows;
				dgGroups.CurrentPageIndex = 0;
				DataView dv = new DataView(Read_Data());
				dv.Sort = DataGridSort.sMulSort;
				dgGroups.DataSource = dv;
				dgGroups.DataBind();
			}
			catch{}
		}
		#endregion
	}
}
