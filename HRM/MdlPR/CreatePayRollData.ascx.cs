namespace iHRPCore
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
	using iHRPCore.Include;
	/// <summary>
	///		Summary description for CreatePayRollData.
	/// </summary>
	public class CreatePayRollData : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label lblErr;
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
		protected System.Web.UI.WebControls.TextBox txtMonth;
		protected EmpHeaderSearch EmpHeaderSearch1;
		protected System.Web.UI.WebControls.TextBox txtLstListUser;
		clsEmpHeaderSearch EHSearch = new clsEmpHeaderSearch();

		private void Page_Load(object sender, System.EventArgs e)
		{
			strLanguage = Mession.GlbLangID;
			if(!Page.IsPostBack)
			{
				txtPageLoad.Text = "1";
				Session["ssAddnew"]="Addnew";
				txtMonth.Text = DateTime.Today.ToString("dd/MM/yyyy").Substring(3,7);
				btnSave.Enabled = false;
			}
			btnSave.Attributes.Add("OnClick", " return validform()");
			btnSearch.Attributes.Add("OnClick", "return checkdelete()");
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
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		
		private void btnSave_Click(object sender, System.EventArgs e)
		{			
			//Kiem tra table, neu chua co' thi tao table luong
			clsPayroll.MaketablePayroll(txtMonth.Text);
			//end
			EHSearch.empHeaderSearch = EmpHeaderSearch1;
			EHSearch.EmpSearch();
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				string []sList=txtLstListGroup.Text.Split(',');
				string []sListRemove=txtLstListUser.Text.Split(',');
				cmd.CommandText = "PR_spfrmCreatePayrollData";
				cmd.Parameters.Clear();

//				cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 50).Value = "Delete";				
//				cmd.Parameters.Add("@Month", SqlDbType.VarChar, 12).Value=txtMonth.Text.Trim(); 
//				cmd.Parameters.Add("@LSCompanyID", SqlDbType.VarChar, 12).Value=EHSearch.LSCompanyID; 
//				cmd.Parameters.Add("@LSLevel1ID", SqlDbType.VarChar, 12).Value=EHSearch.LSLevel1ID; 
//				cmd.Parameters.Add("@LSLevel2ID", SqlDbType.VarChar, 12).Value=EHSearch.LSLevel2ID; 
//				cmd.Parameters.Add("@LSLevel3ID", SqlDbType.VarChar, 12).Value=EHSearch.LSLevel3ID; 
//				cmd.Parameters.Add("@EmpCode", SqlDbType.VarChar, 20).Value=EHSearch.EmpID; 
//				cmd.Parameters.Add("@EmpName", SqlDbType.NVarChar, 200).Value=EHSearch.EmpName; 
//				cmd.Parameters.Add("@Status", SqlDbType.Int).Value=EHSearch.Status; 
//
//				cmd.ExecuteNonQuery();

				//Save & update formula of Employee
				for(int i=0;i<sList.Length;i++)
				{
					cmd.Parameters.Clear();

					cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 50).Value = "Save";				
					cmd.Parameters.Add("@Month", SqlDbType.VarChar, 12).Value=txtMonth.Text.Trim(); 
					cmd.Parameters.Add("@EmpID", SqlDbType.VarChar, 12).Value=sList[i].ToString();
					cmd.ExecuteNonQuery();
				}

				//Remove Employee
				for(int i=0;i<sListRemove.Length;i++)
				{
					cmd.Parameters.Clear();

					cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 50).Value = "DeleteByEmpID";				
					cmd.Parameters.Add("@Month", SqlDbType.VarChar, 12).Value=txtMonth.Text.Trim(); 
					cmd.Parameters.Add("@EmpID", SqlDbType.VarChar, 12).Value=sListRemove[i].ToString();
					cmd.ExecuteNonQuery();
				}
				// Reset View Payroll
				cmd.CommandText = "PR_spGenerateViewPayroll";
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Month", SqlDbType.VarChar, 12).Value=txtMonth.Text.Trim(); 
				cmd.ExecuteNonQuery();

				// Reset View Add Salary
				cmd.CommandText = "PR_spGenerateViewAllow";
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Month", SqlDbType.VarChar, 12).Value=txtMonth.Text.Trim(); 
				cmd.ExecuteNonQuery();

				// Reset View Add Bonus
				cmd.CommandText = "PR_spGenerateViewBonus";
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Month", SqlDbType.VarChar, 12).Value=txtMonth.Text.Trim(); 
				cmd.ExecuteNonQuery();

				// Reset View Add Deduction
				cmd.CommandText = "PR_spGenerateViewDeduct";
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Month", SqlDbType.VarChar, 12).Value=txtMonth.Text.Trim(); 
				cmd.ExecuteNonQuery();

				// Reset View Add Salary
				cmd.CommandText = "PR_spGenerateViewSalary";
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Month", SqlDbType.VarChar, 12).Value=txtMonth.Text.Trim(); 
				cmd.ExecuteNonQuery();
				

				cmd.Transaction.Commit();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();			
				clsChangeLang.popupWindow(this.Parent,"0044",strLanguage,"",1);
				BindUserList();
				BindUserGroup();
				
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

		
		private void BindUserList()
		{
			EHSearch.empHeaderSearch = EmpHeaderSearch1;
			EHSearch.EmpSearch();
			string strSQL = "PR_spfrmCreatePayrollData @Activity='GetEmpIDNP'";
				strSQL +=",@Month ='"+txtMonth.Text+"', @LSCompanyID = '"+EHSearch.LSCompanyID +"'";
				strSQL +=",@LSLevel1ID ='"+EHSearch.LSLevel1ID+"', @LSLevel2ID = '"+EHSearch.LSLevel2ID+"'";
				strSQL +=",@LSLevel3ID ='"+EHSearch.LSLevel3ID+"'";
			strSQL +=",@EmpCode ='"+EHSearch.EmpID+"'";
			strSQL +=",@EmpName ='"+EHSearch.EmpName+"'";
			strSQL +=",@Status ='"+EHSearch.Status+"'";
			strSQL +=",@UserGroupID ='"+Mession.GlbUser+"'";

			DataTable dt = clsCommon.GetDataTable(strSQL);
			clsCommon.LoadListBoxControl(lstListUser,dt,"ID","UserGroupName",false);					
		}
		private void BindUserGroup()
		{
			EHSearch.empHeaderSearch = EmpHeaderSearch1;
			EHSearch.EmpSearch();
			string strSQL = "PR_spfrmCreatePayrollData @Activity='GetEmpIDP'";
			strSQL +=",@Month ='"+txtMonth.Text+"', @LSCompanyID = '"+EHSearch.LSCompanyID +"'";
			strSQL +=",@LSLevel1ID ='"+EHSearch.LSLevel1ID+"', @LSLevel2ID = '"+EHSearch.LSLevel2ID+"'";
			strSQL +=",@LSLevel3ID ='"+EHSearch.LSLevel3ID+"'";
			strSQL +=",@EmpCode ='"+EHSearch.EmpID+"'";
			strSQL +=",@EmpName ='"+EHSearch.EmpName+"'";
			strSQL +=",@Status ='"+EHSearch.Status+"'";
			strSQL +=",@UserGroupID ='"+Mession.GlbUser+"'";

			DataTable dt = clsCommon.GetDataTable(strSQL);
			clsCommon.LoadListBoxControl(lstListGroup,dt,"ID","UserGroupName",false);
			txtListLen.Text=lstListGroup.Items.Count.ToString();
		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			//Kiem tra table, neu chua co' thi tao table luong
			clsPayroll.MaketablePayroll(txtMonth.Text);
			//end
			BindUserList();
			BindUserGroup();
		}

	}
}
