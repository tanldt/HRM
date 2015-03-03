namespace iHRPCore
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
	///		Summary description for SYS_ucPermission.
	/// </summary>
	public class SYS_ucPermission : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.LinkButton Linkbutton1;
		protected System.Web.UI.WebControls.RadioButton optGroup;
		protected System.Web.UI.WebControls.RadioButton optUser;
		protected System.Web.UI.HtmlControls.HtmlTableRow trListGroup;
		protected System.Web.UI.HtmlControls.HtmlTableRow trListUser;
		protected System.Web.UI.WebControls.ListBox lstListGroup;
		protected System.Web.UI.WebControls.ListBox lsListUser;
		protected System.Web.UI.WebControls.DropDownList dlModule;
		protected System.Web.UI.WebControls.DataGrid dgFunction;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnAddnew;
		protected System.Web.UI.WebControls.LinkButton btnSearchData;
		protected System.Web.UI.WebControls.CheckBox chkSelectAllView;
		protected System.Web.UI.WebControls.CheckBox chkSelectAllEdit;
		string strLanguage = "EN";

		private void Page_Load(object sender, System.EventArgs e)
		{
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";			
			if (!Page.IsPostBack)
			{
				LoadComboData();			
				BindListUser();
				BindListGroup();
			}
			//btnSave.Attributes.Add("Onclick","return checkSave();");
			btnAddnew.Attributes.Add("Onclick","return checkAddnew();");
		}	
		private void LoadComboData()
		{
			clsCommon.LoadDropDownListControl(this.dlModule,"sp_clsCommon 'GetModuleList','"+strLanguage+"'","ModuleID","ModuleName",true); 
		}
		private void BindListUser()
		{
			DataTable dtData= clsCommon.GetDataTable("UMS_sptblUserAccount @Activity='GetAllUsers_', @LSCompanyID = '"+Mession.GlbCusGroupID+"'");
			clsCommon.LoadListBoxControl(lsListUser,dtData,"UserGroupID","UserGroupName",false);
		}
		private void BindListGroup()
		{
			DataTable dtData= clsCommon.GetDataTable("UMS_sptblUserAccount @Activity='GetAllGroup'");
			clsCommon.LoadListBoxControl(lstListGroup,dtData,"UserGroupID","UserGroupName",false);
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
			this.dlModule.SelectedIndexChanged += new System.EventHandler(this.dlModule_SelectedIndexChanged);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnAddnew.Click += new System.EventHandler(this.btnAddnew_Click);
			this.lstListGroup.SelectedIndexChanged += new System.EventHandler(this.lstListGroup_SelectedIndexChanged);
			this.lsListUser.SelectedIndexChanged += new System.EventHandler(this.lsListUser_SelectedIndexChanged);
			this.dgFunction.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgFunction_PageIndexChanged);
			this.btnSearchData.Click += new System.EventHandler(this.btnSearchData_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void dlModule_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			BindDataGrid();
		}
		private void BindDataGrid()
		{
			string sUserGroupID="";
			if (optGroup.Checked)				
				sUserGroupID=lstListGroup.SelectedValue;
			else
				sUserGroupID=lsListUser.SelectedValue;
			try
			{
				string strModuleID = this.dlModule.SelectedItem.Value.Trim();
				DataTable dtb = clsCommon.GetDataTable("sp_clsCommon 'GetFunctionByMdlIDDetails_','" + strLanguage + "',@ModuleID = '" + strModuleID + "',@UserGroupID='" + sUserGroupID + "'");
				this.dgFunction.PageSize = 20;
				this.dgFunction.PagerStyle.Mode = PagerMode.NumericPages;
				this.dgFunction.CurrentPageIndex = 0;
				this.dgFunction.DataSource = dtb;
				dgFunction.DataBind();
				dtb.Dispose();
				this.FillData();
			}
			catch(Exception ex)
			{
				clsChangeLang.popupWindowExp_Mess(this.Parent,ex);
			}
		}
		private void FillData()
		{			
			for(int i=0; i<this.dgFunction.Items.Count; i++)
			{
				if (this.dgFunction.Items[i].Cells[2].Text.Trim() == "1")
					((CheckBox)this.dgFunction.Items[i].FindControl("chkView")).Checked = true;
				else
					((CheckBox)this.dgFunction.Items[i].FindControl("chkView")).Checked = false;

				if (this.dgFunction.Items[i].Cells[1].Text.Trim() == "1")
					((CheckBox)this.dgFunction.Items[i].FindControl("chkEdit")).Checked = true;
				else
					((CheckBox)this.dgFunction.Items[i].FindControl("chkEdit")).Checked = false;
			}
		}
		private void dgFunction_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			try
			{
				BindDataGrid();
				dgFunction.CurrentPageIndex=e.NewPageIndex;
				dgFunction.DataBind();
				this.FillData();
			}
			catch(Exception ex) 
			{
				//string str = ex.Message;
				clsChangeLang.popupWindowExp_Mess(this.Parent,ex);
			}
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			string sReturn="";
			CheckBox chkFRun= new CheckBox();
			CheckBox chkFEdit= new CheckBox();			
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				string sUserGroupID="";
				if (optGroup.Checked)				
					sUserGroupID=lstListGroup.SelectedValue;
				else
					sUserGroupID=lsListUser.SelectedValue;
				
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				//Luu quyen cho user/group user dang chon	
				for(int i=0;i<dgFunction.Items.Count;i++)
				{
					chkFRun=(CheckBox)dgFunction.Items[i].FindControl("chkView");
					chkFEdit=(CheckBox)dgFunction.Items[i].FindControl("chkEdit");					
					cmd.CommandText = "UMS_sptblUserAccount";
					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 50).Value = "SavePermission";	
					cmd.Parameters.Add("@UserGroupID", SqlDbType.NVarChar, 20).Value = sUserGroupID;
					cmd.Parameters.Add("@FunctionID", SqlDbType.NVarChar, 20).Value = dgFunction.Items[i].Cells[0].Text;
					cmd.Parameters.Add("@FRun",SqlDbType.Bit).Value=chkFRun.Checked;
					cmd.Parameters.Add("@FEdit",SqlDbType.Bit).Value=chkFEdit.Checked;
					cmd.ExecuteNonQuery();					
				}	
			
				//LanHTD: Neu la nhom user, ra quyen cho cac user tuong ung
				if (optGroup.Checked)
				{
					cmd.CommandText = "UMS_sptblUserAccount";
					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 50).Value = "AssignPerForUser";			
					cmd.Parameters.Add("@GroupID", SqlDbType.NVarChar, 20).Value = sUserGroupID;
					cmd.ExecuteNonQuery();
				}
				//End LanHTD
				cmd.Transaction.Commit();			
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				clsChangeLang.popupWindow(this.Parent,"0044",strLanguage,"",1);
				// SonPQ : Delete cache 
				DataCache.RemoveCache("UserPerm_" + sUserGroupID);
				// End SonPQ
			}
			catch(Exception ex)
			{				
				cmd.Transaction.Rollback();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				clsChangeLang.popupWindowExp_Mess(this.Parent,ex);
				
			}
		}

		private void lstListGroup_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			BindDataGrid();
		}

		private void lsListUser_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			BindDataGrid();
		}

		private void btnSearchData_Click(object sender, System.EventArgs e)
		{
			BindDataGrid();
		}

		private void btnAddnew_Click(object sender, System.EventArgs e)
		{
			BindDataGrid();
		}	
		
	}
}
