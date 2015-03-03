namespace MdlTR
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.Com;
	using iHRPCore.HRComponent;
	using System.Data.SqlClient;
	using System.Configuration;
	using iHRPCore.APPComponent;
	using iHRPCore.TRComponent;
	using System.IO;	
	using iHRPCore.Include;
	using GridSort;

	/// <summary>
	///		Summary description for SetDeptHead.
	/// </summary>
	public class SetDeptHead : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.CheckBox chkSelectAll;
		protected System.Web.UI.WebControls.DataGrid dtgList;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnExport;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if (!Page.IsPostBack)
			{
				LoadDataGird();
				
			}
			btnSave.Attributes.Add("OnClick", " return checkSave()");
			
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
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		private void LoadDataGird()
		{
			DataTable dtData = clsCommon.GetDataTable("HR_spfrmDEPTHEAD @Activity='GetDataAll'");
			dtgList.DataSource=dtData;
			dtgList.DataBind();
			DropDownList cbo = new DropDownList();
			for(int i =0 ; i<dtgList.Items.Count;i++)
			{
				cbo = (DropDownList)dtgList.Items[i].FindControl("cboEmpID");
				clsCommon.LoadDropDownListControl(cbo,"HR_spfrmDEPTHEAD @Activity='getCboEmpHeader_UMS'","ID","Name",true);
				try
				{
					if (dtData.Rows[i]["EmpID"].ToString()!="")
						cbo.SelectedValue=dtData.Rows[i]["EmpID"].ToString();
				}
				catch
				{
					cbo.SelectedValue="";
				}
			}
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			
			
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			SQLconn.Open();
			try
			{
				
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "HR_spfrmDEPTHEAD";
   
				CheckBox obj = new CheckBox();
				for (int i = 0;i<= dtgList.Items.Count - 1;i++)
				{					
					obj = (CheckBox) dtgList.Items[i].FindControl("chkSelect");
					
					if (obj.Checked)
					{
						cmd.Parameters.Clear();						
						cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "Save";											
						cmd.Parameters.Add("@EmpID",SqlDbType.NVarChar,12).Value=((DropDownList)dtgList.Items[i].FindControl("cboEmpID")).SelectedValue;
						cmd.Parameters.Add("@LSLevel1ID",SqlDbType.NVarChar,12).Value=dtgList.Items[i].Cells[0].Text.ToString();
						cmd.Parameters.Add("@LSLevel2ID",SqlDbType.NVarChar,12).Value=dtgList.Items[i].Cells[1].Text.ToString();
						cmd.ExecuteNonQuery();		
					}
				}
				cmd.Transaction.Commit();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();	
				
			}
			catch  (Exception exp)
			{								
				cmd.Transaction.Rollback();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				
			}
			LoadDataGird();			
		}

		private void btnExport_Click(object sender, System.EventArgs e)
		{
			iHRPCore.DataGridExcelExporter myExcelXport=new iHRPCore.DataGridExcelExporter(dtgList);
			myExcelXport.Export("");
			myExcelXport = null;
		}

	}
}
