namespace iHRPCore.MdlHR
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.Com;
	using iHRPCore.Include;
	using iHRPCore.HRComponent;
	using GridSort;

	/// <summary>
	///		Summary description for EmpList.
	/// </summary>
	public class Reminder : System.Web.UI.UserControl
	{
		#region Declare
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.TextBox txtPageRows;
		protected System.Web.UI.WebControls.DropDownList cboCol1;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.DropDownList cboCol2;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.DropDownList cboCol3;
		protected System.Web.UI.WebControls.CheckBox chkAdvanceSort;
		protected System.Web.UI.WebControls.CheckBox chkMultiSort;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnSort;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label lblTotalRows;
		protected System.Web.UI.WebControls.LinkButton button;
		protected System.Web.UI.WebControls.LinkButton btnSearch;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.TextBox txtMonthBirth;
		protected System.Web.UI.WebControls.DataGrid dtgListBirth;
		protected System.Web.UI.HtmlControls.HtmlTable tblBirth;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.LinkButton btnView;
		protected System.Web.UI.WebControls.TextBox txtToDate;
		protected System.Web.UI.WebControls.TextBox txtFromDate;
		protected System.Web.UI.WebControls.TextBox txtPageLoad;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.LinkButton btnExtend;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtFromDateTemp;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtToDateTemp;
		protected System.Web.UI.WebControls.DataGrid dtgListContract;
		protected EmpHeaderSearch EmpHeaderSearch1;

		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				txtPageLoad.Text = "1";
				LoadDefaultValue();
				LoadGridBirthday();
				LoadDataGrid();
			}			

			btnView.Attributes.Add("OnClick", "return ValidFormBirth()");
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
			this.btnExtend.Click += new System.EventHandler(this.btnExtend_Click);
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			this.btnView.Click += new System.EventHandler(this.btnView_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion]

		private void LoadDefaultValue()
		{
			txtFromDate.Text = DateTime.Today.ToString("dd/MM");
			txtToDate.Text = DateTime.Today.AddDays(2).ToString("dd/MM");
		}

		private void LoadDataGrid()
		{
			try
			{
				//Lay gia tri cua HeaderEmpSearch
				string strCompany = EmpHeaderSearch1.cboCompany.SelectedValue.Trim();
				string strLevel1 = EmpHeaderSearch1.cboLevel1.SelectedValue.Trim();
				string strLevel2 = EmpHeaderSearch1.cboLevel2.SelectedValue.Trim();
				string strLevel3 = EmpHeaderSearch1.cboLevel3.SelectedValue.Trim();
				string strLocation = EmpHeaderSearch1.cboLocation.SelectedValue.Trim();
				
				DataTable dtList = clsHRContractExtend.GetReminderContract(strCompany, strLevel1, strLevel2, strLevel3, strLocation, this.Page);
				dtgListContract.DataSource = dtList;
				dtgListContract.DataBind();

			}
			catch (Exception ex)
			{
				lblErr.Text=ex.Message;
			}

		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			LoadDataGrid();
			Session["CompanyID"] = EmpHeaderSearch1.cboCompany.SelectedValue.Trim();
			Session["Level1ID"] = EmpHeaderSearch1.cboLevel1.SelectedValue.Trim();
			Session["Level2ID"] = EmpHeaderSearch1.cboLevel2.SelectedValue.Trim();
		}

		private void btnExport_Click(object sender, System.EventArgs e)
		{
			//this.dtgList.AllowPaging = false;
			//this.BindDataGrid();
            iHRPCore.DataGridExcelExporter myExcelXport=new DataGridExcelExporter(dtgListContract);
			myExcelXport.Export("");
			myExcelXport = null;
		}

		private void btnExtend_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(Request.ApplicationPath.Trim() + "/Editpage.aspx?ModuleID=HR&ParentID=8&FunctionID=506&Ascx=MdlHR/ContractExtend.ascx");
		}

		private void btnView_Click(object sender, System.EventArgs e)
		{
			LoadGridBirthday();
		}

		private void LoadGridBirthday()
		{
			//Lay gia tri cua HeaderEmpSearch
			string strCompany = EmpHeaderSearch1.cboCompany.SelectedValue.Trim();
			string strLevel1 = EmpHeaderSearch1.cboLevel1.SelectedValue.Trim();
			string strLevel2 = EmpHeaderSearch1.cboLevel2.SelectedValue.Trim();
			string strLevel3 = EmpHeaderSearch1.cboLevel3.SelectedValue.Trim();
			string strLocation = EmpHeaderSearch1.cboLocation.SelectedValue.Trim();

			dtgListBirth.DataSource = LoadBirthday(txtFromDate.Text, txtToDate.Text, txtMonthBirth.Text, strCompany, strLevel1, strLevel2, strLevel3, strLocation, this.Page);
			dtgListBirth.DataBind();
		}

		private DataTable LoadBirthday(string strFromDate, string strToDate, string strMonth, string strCompany, string strLevel1, string strLevel2, string strLevel3, string strLocation,  System.Web.UI.Page pPage)
		{
			DataTable dtData = new DataTable();
			try
			{
				string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
				string strSQL  = "";
				strSQL = "NEWS_spfrmNEWS @Activity='getBirthInMonth', @UserGroupID=N'" + sAccountLogin + "', @Month='" + strMonth + "', @FromDDMM='" + strFromDate + "',@ToDDMM='" + strToDate + "', @LSLevel1ID=N'" + strLevel1 + "', @LSLevel2ID=N'" + strLevel2 + "', @LSLevel3ID=N'" + strLevel3 + "', @LSLocationID=N'" + strLocation + "'";
				dtData = clsCommon.GetDataTable(strSQL);
				return dtData;
			}
			catch
			{
				return null;
			}
		}
	}
}
