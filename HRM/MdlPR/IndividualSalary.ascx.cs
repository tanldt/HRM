namespace iHRPCore.MdlPR
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Data.SqlClient;
	using System.Configuration;
	using iHRPCore.Include;
	using iHRPCore.Com;
	using iHRPCore.WFComponent;
	using iHRPCore.Component;

	/// <summary>
	///		Summary description for IndividualSalary.
	/// </summary>
	public class IndividualSalary : UserControlCommon
	{
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlGenericControl Div1;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.LinkButton Linkbutton2;
		protected System.Web.UI.WebControls.TextBox txtFromMonth;
		protected System.Web.UI.WebControls.Label Label3;
		protected EmpHeader HR_EmpHeader;
		protected System.Web.UI.WebControls.TextBox txtSalPeriod;
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.LinkButton btnView;
		protected System.Web.UI.WebControls.DataGrid dtgList;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		public string strEmpID = "";

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			//strEmpID = HR_EmpHeader.txtEmpID.Text.Trim();
			strEmpID = Request.Params["EmpID"] != null? Request.Params["EmpID"].Trim():
				(Session["EmpID"] != null? Session["EmpID"].ToString():"");
			txtFromMonth.Text  = clsCommon.LookUpTable("select * from SYS_tblParameters where ParamName = 'PR_CurMonth'","ParamValue");
			txtFromMonth.Enabled = false;
			txtSalPeriod.Text  = clsCommon.LookUpTable("select * from SYS_tblParameters where ParamName = 'PR_CurPeriod'","ParamValue");
			txtSalPeriod.Enabled = false;
			if (! Page.IsPostBack)
			{
				BindDataGrid(strEmpID);
				//Cal ();
				//txtSalaryPeriod.Text = clsCommon.LookUpTable("select top 1 dbo.fnConvertNumber(TongSoTienConLai) as TongSoTienConLai from PR_vPayroll_"+txtFromMonth.Text.Replace("/","")+" where EmpID = '" +strEmpID+ "' order by ToDate DESC","TongSoTienConLai");
			}
			btnSave.Attributes.Add("onclick", "return CheckSave();");
			btnView.Attributes.Add("onclick","return ShowPage();");
			btnDelete.Attributes.Add("onclick","return checkdelete();");
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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion		
		private void BindDataGrid(string EmpID)
		{			
			DataTable dtb = new DataTable();
			dtb=clsCommon.GetDataTable("PR_spfrmPayroll @Activity = 'GetDataSalaryDetail',@LanguageID = '"+strLanguage+"',@EmpID='"+EmpID+"',@Month = '"+ txtFromMonth.Text +
				"',@SalPeriod = " +this.txtSalPeriod.Text + "");
			this.dtgList.DataSource = dtb;
			this.dtgList.DataBind();
			for(int i=0;i<dtgList.Items.Count;i++)
			{
				string strEditID = dtgList.Items[i].Cells[5].Text.Trim();
				string strDelID = dtgList.Items[i].Cells[6].Text.Trim();
				TextBox txtFormular = ((TextBox)this.dtgList.Items[i].FindControl("txtFormular"));
				TextBox txtValue = ((TextBox)this.dtgList.Items[i].FindControl("txtValue"));
				CheckBox chkSelect = ((CheckBox)this.dtgList.Items[i].FindControl("chkSelect"));
				if (strEditID == "False")
				{
					txtFormular.ReadOnly = true;
					txtValue.ReadOnly = true;
				}
				if (strEditID == "False")
				{
					chkSelect.Enabled = false;
				}

			}
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			lblErr.Text = "";
			try
			{
				
				string strLSSalaryItemID;
				string strFormular;
				string strValue;
				string strActivity = "UpdateSalaryItem";
				for (int i=0; i<this.dtgList.Items.Count; i++)
				{
					bool blSave = false;
					if ( ! ((CheckBox)this.dtgList.Items[i].FindControl("chkSelect")).Checked) continue;
				
					strLSSalaryItemID = this.dtgList.Items[i].Cells[0].Text.Trim();

					strFormular = ((TextBox)this.dtgList.Items[i].FindControl("txtFormular")).Text;
					strValue = ((TextBox)this.dtgList.Items[i].FindControl("txtValue")).Text;

					blSave = this.UpdateListRecord(strActivity,strLSSalaryItemID, strFormular,strValue);

					if (blSave == true)
						lblErr.Text = GetText("COMMON","SUCCESSFUL");
					else
						lblErr.Text = GetText("COMMON","NOTSUCCESSFUL");
				}
				Cal ();
				//txtSalaryPeriod.Text = clsCommon.LookUpTable("select top 1 dbo.fnConvertNumber(TongSoTienConLai) as TongSoTienConLai from PR_vPayroll_"+txtFromMonth.Text.Replace("/","")+" where EmpID = '" +strEmpID+ "' order by ToDate DESC","TongSoTienConLai");
				string strErr = lblErr.Text;
				lblErr.Text="";
				BindDataGrid(strEmpID);
				clsChangeLang.popupWindow(this.Parent,strErr,"",1);
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
			finally
			{
				//txtReturnValue.Value = "";
			}
		}
		private void Cal ()
		{
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "PR_spPayrollCollection";

				cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 20).Value = "CalculateSalary";
				cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = strEmpID;
				cmd.Parameters.Add("@MM", SqlDbType.NVarChar, 2).Value = txtFromMonth.Text.Substring(0,2);
				cmd.Parameters.Add("@YYYY", SqlDbType.NVarChar, 4).Value = txtFromMonth.Text.Substring(3,4);
				cmd.Parameters.Add("@SalPeriod", SqlDbType.Int).Value =txtSalPeriod.Text;

				cmd.ExecuteNonQuery();

				cmd.Transaction.Commit();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
			}
			catch
			{
				cmd.Transaction.Rollback();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
			}
		}
		private bool UpdateListRecord(string strActivity,string strLSSalaryItemID,string strFormular,string strValue)
		{
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "PR_spfrmPayroll";

				cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 20).Value = strActivity;
				cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = strEmpID;
				cmd.Parameters.Add("@LSSalaryItemID", SqlDbType.NVarChar, 12).Value = strLSSalaryItemID;
				cmd.Parameters.Add("@Month", SqlDbType.NVarChar, 7).Value =txtFromMonth.Text;
				cmd.Parameters.Add("@SalPeriod", SqlDbType.Int).Value =txtSalPeriod.Text;
				
				if(strFormular == "")
					cmd.Parameters.Add("@Formula", SqlDbType.NVarChar, 1000).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@Formula", SqlDbType.NVarChar, 1000).Value = Server.HtmlEncode(strFormular);
				if(strValue == "")
					cmd.Parameters.Add("@Value", SqlDbType.Float).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@Value", SqlDbType.Float).Value = Convert.ToDouble(strValue);

//				cmd.Parameters.Add("@Creater", SqlDbType.NVarChar,80).Value = Session["AccountLogin"];	
//				cmd.Parameters.Add("@CreateTime", SqlDbType.DateTime).Value = DBNull.Value ;	
//				cmd.Parameters.Add("@Editer", SqlDbType.NVarChar,80).Value = Session["AccountLogin"];
//				cmd.Parameters.Add("@EditTime", SqlDbType.DateTime).Value = DBNull.Value ;	

				cmd.Parameters.Add("@ReturnMess", SqlDbType.NVarChar, 200).Value = "";
				cmd.Parameters[cmd.Parameters.Count - 1].Direction = ParameterDirection.InputOutput;

				cmd.ExecuteNonQuery();

				if (cmd.Parameters[cmd.Parameters.Count-1].Value.ToString().Trim() != "")
				{
					cmd.Transaction.Rollback();
					cmd.Dispose();
					SQLconn.Close();
					SQLconn.Dispose();
					return false;
				}
				cmd.Transaction.Commit();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				return true;
			}
			catch(Exception exp)
			{
				cmd.Transaction.Rollback();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				return false;
			}
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			lblErr.Text = "";
			try
			{
				
				string strLSSalaryItemID;
//				string strFormular;
//				string strValue;
				string strActivity = "DeleteSalaryItem";
				for (int i=0; i<this.dtgList.Items.Count; i++)
				{
					bool blDelete = false;
					if ( ! ((CheckBox)this.dtgList.Items[i].FindControl("chkSelect")).Checked) continue;
					if ( ! ((CheckBox)this.dtgList.Items[i].FindControl("chkSelect")).Enabled) continue;
				
					strLSSalaryItemID = this.dtgList.Items[i].Cells[0].Text.Trim();

					blDelete = this.DeleteListRecord(strActivity,strLSSalaryItemID);

					if (blDelete == true)
						clsChangeLang.popupWindow(this.Parent,"IS_0001",strLanguage,"",1);
					else
						clsChangeLang.popupWindow(this.Parent,"IS_0002",strLanguage,"",0);
				}
				Cal ();
				//txtSalaryPeriod.Text = clsCommon.LookUpTable("select top 1 dbo.fnConvertNumber(TongSoTienConLai) as TongSoTienConLai from PR_vPayroll_"+txtFromMonth.Text.Replace("/","")+" where EmpID = '" +strEmpID+ "' order by ToDate DESC","TongSoTienConLai");
				BindDataGrid(strEmpID);
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
			finally
			{
				//txtReturnValue.Value = "";
			}
		}
		private bool DeleteListRecord(string strActivity,string strLSSalaryItemID)
		{
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "PR_spfrmPayroll";

				cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 20).Value = strActivity;
				cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = strEmpID;
				cmd.Parameters.Add("@LSSalaryItemID", SqlDbType.NVarChar, 12).Value = strLSSalaryItemID;
				cmd.Parameters.Add("@Month", SqlDbType.NVarChar, 7).Value = this.txtFromMonth.Text;

				cmd.Parameters.Add("@ReturnMess", SqlDbType.NVarChar, 200).Value = "";
				cmd.Parameters[cmd.Parameters.Count - 1].Direction = ParameterDirection.InputOutput;

				cmd.ExecuteNonQuery();

				if (cmd.Parameters[cmd.Parameters.Count-1].Value.ToString().Trim() != "")
				{
					cmd.Transaction.Rollback();
					cmd.Dispose();
					SQLconn.Close();
					SQLconn.Dispose();
					return false;
				}
				cmd.Transaction.Commit();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				return true;
			}
			catch(Exception exp)
			{
				cmd.Transaction.Rollback();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				return false;
			}
		}

	}
}
