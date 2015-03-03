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
	using iHRPCore.Include;
	using iHRPCore.HRComponent;
	using GridSort;

	/// <summary>
	///		Summary description for SYS_ucAssignUserGroup.
	/// </summary>
	public class SYS_ucAssignUserGroup : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList cboGroupListID;
		protected System.Web.UI.WebControls.ListBox lstListUser;
		protected System.Web.UI.WebControls.ListBox lstListGroup;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnAddnew;
		protected System.Web.UI.WebControls.TextBox txtLstListGroup;
		protected System.Web.UI.WebControls.TextBox txtListLen;
		protected System.Web.UI.WebControls.LinkButton btnSearch;
		protected EmpHeaderSearch EmpHeaderSearch1;
		string strLanguage = "EN";

		private void Page_Load(object sender, System.EventArgs e)
		{
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";				
			if(!Page.IsPostBack)
				if (!Page.IsPostBack)
				{
					LoadComboData();					
					Session["ucAccount"]="addnew";
				}
			/*btnAdd.Attributes.Add("Onclick","return addAttribute();");
			btnAddAll.Attributes.Add("Onclick","return addAll();");
			btnRemove.Attributes.Add("Onclick","return delAttribute();");
			btnRemoveAll.Attributes.Add("Onclick","return delAll();");*/
			btnSearch.Attributes.Add("Onclick","return checkvalidSearch();");
			btnSave.Attributes.Add("Onclick","return checkSave();");
			btnAddnew.Attributes.Add("Onclick","return checkAddnew();");
		}

		private void LoadComboData()
		{
			clsCommon.LoadDropDownListControl(cboGroupListID,"UMS_sptblUserAccount 'GetAllGroup'","UserGroupID","UserGroupName",true);
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
			this.cboGroupListID.SelectedIndexChanged += new System.EventHandler(this.cboGroupListID_SelectedIndexChanged);
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnAddnew.Click += new System.EventHandler(this.btnAddnew_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void cboGroupListID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			BindUserList(cboGroupListID.SelectedValue,0);
			BindUserGroup(cboGroupListID.SelectedValue,0);
		}
		private void BindUserList(string sGroupID, int iEmpSearch)
		{
			DataTable dt=new DataTable();	
			if(iEmpSearch==1 )
			{
				string strEmpID = EmpHeaderSearch1.txtEmpID.Text.Trim().Replace("'","");
				string strEmpName = EmpHeaderSearch1.txtEmpName.Text.Trim().Replace("'","");
				string strLevel1 = EmpHeaderSearch1.cboLevel1.SelectedValue.Trim();
				string strLevel2 = EmpHeaderSearch1.cboLevel2.SelectedValue.Trim();
				string strLevel3 = EmpHeaderSearch1.cboLevel3.SelectedValue.Trim();						
				string strCompany = EmpHeaderSearch1.cboCompany.SelectedValue.Trim();
				string strStatus = EmpHeaderSearch1.optStatus.SelectedValue.Trim();
				string strEmpType=EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue.Trim();

				strCompany = Mession.GlbCusGroupID;

				dt = clsCommon.GetDataTable("UMS_sptblUserAccount @Activity='GetUserList',@GroupID='" + sGroupID + 
				"',@EmpID='" + strEmpID + "',@EmpName=N'" + strEmpName + "',@LSCompanyID='" +strCompany+ "',@LSLevel1ID='" + strLevel1 + 
				"', @LSLevel2ID='" + strLevel2 + "',@LSLevel3ID='" + strLevel3 +
				"',@Status='"+ strStatus +"',@LSEmpTypeID='" + strEmpType + "',@UserGroupID = '"+Mession.GlbUser+"'");
			}
			else
			{
				dt = clsCommon.GetDataTable("UMS_sptblUserAccount @Activity='GetUserList',@GroupID='" + sGroupID + "',@LSCompanyID= '"+Mession.GlbCusGroupID+"'");
			}
			clsCommon.LoadListBoxControl(lstListUser,dt,"ID","UserGroupName",false);					
		}
		private void BindUserGroup(string sGroupID, int iEmpSearch)
		{
			DataTable dt=new DataTable();
			if(iEmpSearch==1 )
			{
				string strEmpID = EmpHeaderSearch1.txtEmpID.Text.Trim().Replace("'","");
				string strEmpName = EmpHeaderSearch1.txtEmpName.Text.Trim().Replace("'","");
				string strLevel1 = EmpHeaderSearch1.cboLevel1.SelectedValue.Trim();
				string strLevel2 = EmpHeaderSearch1.cboLevel2.SelectedValue.Trim();
				string strLevel3 = EmpHeaderSearch1.cboLevel3.SelectedValue.Trim();						
				string strCompany = EmpHeaderSearch1.cboCompany.SelectedValue.Trim();
				string strStatus = EmpHeaderSearch1.optStatus.SelectedValue.Trim();
				string strEmpType=EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue.Trim();

				strCompany = Mession.GlbCusGroupID;

			 dt = clsCommon.GetDataTable("UMS_sptblUserAccount @Activity='GetUserGroup',@GroupID='" + sGroupID + 
				"',@EmpID='" + strEmpID + "',@EmpName=N'" + strEmpName + "',@LSCompanyID='" +strCompany+ "',@LSLevel1ID='" + strLevel1 + 
				"', @LSLevel2ID='" + strLevel2 + "',@LSLevel3ID='" + strLevel3 +
				"',@Status='"+ strStatus +"',@LSEmpTypeID='" + strEmpType + "',@UserGroupID = '"+Mession.GlbUser+"'");
			}
			else
			{
				dt = clsCommon.GetDataTable("UMS_sptblUserAccount @Activity='GetUserGroup',@GroupID='" + sGroupID + "',@LSCompanyID= '"+Mession.GlbCusGroupID+"'");
			}
			clsCommon.LoadListBoxControl(lstListGroup,dt,"ID","UserGroupName",false);
			txtListLen.Text=lstListGroup.Items.Count.ToString();
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

				/*LanHTD: 2007 01 26: Gom lai thanh mot lan ket noi, sua activity
				 * cmd.CommandText = "UMS_sptblUserAccount";
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 50).Value = "DeleteUserPermission";			
				cmd.Parameters.Add("@GroupID", SqlDbType.NVarChar, 20).Value=cboGroupListID.SelectedValue;
				cmd.Parameters.Add("@sListUserAccID",SqlDbType.NVarChar, 4000).Value=txtLstListGroup.Text;	
				cmd.ExecuteNonQuery();
				
				cmd.CommandText = "UMS_sptblUserAccount";
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 50).Value = "DeleteUserGroup";			
				cmd.Parameters.Add("@GroupID", SqlDbType.NVarChar, 20).Value=cboGroupListID.SelectedValue;
				cmd.ExecuteNonQuery();
				
				for(int i=0;i<sList.Length-1;i++)
				{
					cmd.CommandText = "UMS_sptblUserAccount";
					cmd.Parameters.Clear();

					cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 50).Value = "AssignUserGroup";				
					cmd.Parameters.Add("@UserID", SqlDbType.NVarChar, 20).Value=sList.GetValue(i).ToString();
					cmd.Parameters.Add("@GroupID", SqlDbType.NVarChar, 20).Value=cboGroupListID.SelectedValue;
					cmd.ExecuteNonQuery();
				}
				*/
				cmd.CommandText = "UMS_sptblUserAccount";
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 50).Value = "SaveUserPerFollowGroup";			
				cmd.Parameters.Add("@GroupID", SqlDbType.NVarChar, 20).Value=cboGroupListID.SelectedValue;
				if (Mession.GlbUser != "admin")
                    cmd.Parameters.Add("@LSCompanyID", SqlDbType.NVarChar, 20).Value=Mession.GlbCusGroupID;
				cmd.Parameters.Add("@sListUserAccID",SqlDbType.NVarChar, 4000).Value=txtLstListGroup.Text;	 

				// PhucVD_05/10/2007: Chia chuoi ID khi lon hon 4000 char
/*				string strIDList = txtLstListGroup.Text;
				int numParams = (txtLstListGroup.Text.Length / 4000) + 1;
				int startIndex = 0;
				for (int i=0; i<numParams; i++)
				{
					int length = (txtLstListGroup.Text.Length - (4000 * i) > 4000)? 4000: txtLstListGroup.Text.Length - (4000 * i);
					string strListTemp = txtLstListGroup.Text.Substring(startIndex, length);
					int index = txtLstListGroup.Text.LastIndexOf(',');
					cmd.Parameters.Add("@sListUserAccID_" + Convert.ToString(i + 1), SqlDbType.NVarChar, 4000).Value = txtLstListGroup.Text;	 
					startIndex = 4000 * (i + 1);
				}
*/
				cmd.ExecuteNonQuery();
				//End LanHTD


				cmd.Transaction.Commit();			
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();			
				clsChangeLang.popupWindow(this.Parent,"0044",strLanguage,"",1);
				BindUserList(cboGroupListID.SelectedValue,0);
				BindUserGroup(cboGroupListID.SelectedValue,0);
				clsChangeLang.popupWindow(this.Parent,"0044",strLanguage,"",1);
				
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

		private void btnAddnew_Click(object sender, System.EventArgs e)
		{
			BindUserList(cboGroupListID.SelectedValue,0);
			BindUserGroup(cboGroupListID.SelectedValue,0);
		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			
			BindUserList(cboGroupListID.SelectedValue,1);
			BindUserGroup(cboGroupListID.SelectedValue,1);
			
		}
		
	}
}
