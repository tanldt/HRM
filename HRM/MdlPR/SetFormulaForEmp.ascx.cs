namespace MdlPR
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.Include;
	using GridSort;
	using System.Data.SqlClient;
	using System.Configuration;
	using iHRPCore;
	using iHRPCore.Com;


	/// <summary>
	///		Summary description for SetFormulaForEmp.
	/// </summary>
	public class SetFormulaForEmp : System.Web.UI.UserControl
	{
		#region Declaration
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnSearch;
		protected System.Web.UI.WebControls.TextBox txtEmpID;
		protected System.Web.UI.WebControls.LinkButton btnCancel;
		protected System.Web.UI.WebControls.TextBox txtTypeDate;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.DropDownList DropDownList1;
		protected System.Web.UI.WebControls.DataGrid dtgFormula;
		public string strLanguage="EN";
		protected System.Web.UI.WebControls.RadioButtonList RadStatus;
		protected EmpHeaderSearch EmpHeaderSearch1;
		clsEmpHeaderSearch EHSearch = new clsEmpHeaderSearch();
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				LoadDefaultData();
			}

			this.btnSave.Attributes.Add("OnClick", "return CheckSave()");
			this.btnSearch.Attributes.Add("OnClick", "return CheckSearch()");
			btnDelete.Attributes.Add("OnClick", "return CheckDelete()");
			//this.btnExport.Attributes.Add("OnClick", "return CheckSearch()");
			//this.btnSubmit.Attributes.Add("OnClick", "return CheckSave()");
			//btnSubmit.Attributes.Add("onclick",clsChangeLang.singleClick("CheckSave",btnSubmit,this.Page)); 
			//this.btnCancel.Attributes.Add("OnClick", "return CheckCancel()");
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
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.dtgFormula.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgFormula_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		private void LoadDefaultData()
		{
			//this.txtMonthYear.Text = DateTime.Today.ToString("MM/yyyy");
			DataTable dt = new DataTable();
			dtgFormula.DataSource = dt;
			dtgFormula.DataBind();
			
		}
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			string sErr = clsPayrollFormula.sImpact(dtgFormula);
			if (sErr=="")
			{
				clsChangeLang.popupWindow(this.Parent,"0044",strLanguage,"",1);
				if (RadStatus.SelectedValue == "0")
					btnDelete.Enabled = false;
				else
					btnDelete.Enabled = true;
				BindDataGrid(true);

				iHRPCore.Com.clsCommon.Exc_CommandText("[UMS_sptblAccessCommon] @Activity='UpdateFormulaPR', @UserGroupID = '" 
					+ Session["AccountLogin"].ToString() + "',@LSCompanyID = '" + Mession.GlbCusGroupID + "'" );
			}
			else
			{
				clsChangeLang.popupWindowCataLog(this.Parent,sErr);
			}
		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			lblErr.Text = "";
			if (RadStatus.SelectedValue == "0")
				btnDelete.Enabled = false;
			else
				btnDelete.Enabled = true;
			BindDataGrid(true);
		}
		private void BindDataGrid(bool Status)
		{	
			this.dtgFormula.DataSource = null;
			try
			{
				DataTable dt = new DataTable();
				if (Status)
				{
					EHSearch.empHeaderSearch = EmpHeaderSearch1;
					EHSearch.EmpSearch();
					string strEmpID = EHSearch.EmpID;
					string strEmpName = EHSearch.EmpName;
					string strLevel1 = EHSearch.LSLevel1ID;
					string strLevel2 = EHSearch.LSLevel2ID;
					string strLevel3 = EHSearch.LSLevel3ID;
					string strLocation = EHSearch.LSLocationID;
					string strJobCode = EHSearch.LSJobcodeID;
					string strPosition = EHSearch.LSEmpTypeID;
					string strCompany = EHSearch.LSCompanyID;
					string strStatus = EHSearch.Status;
					string sEmpTypeID = EHSearch.LSEmpTypeID;
					string StatusSearch = RadStatus.SelectedValue;
					dt = clsPayrollFormula.GetEmpList(strEmpID, strEmpName, strLevel1,strLevel2, strLevel3, strPosition, strJobCode,strLocation
						,strCompany,strStatus,sEmpTypeID,StatusSearch);
					Mession.GlbSearchData = dt;
					this.dtgFormula.CurrentPageIndex = 0;
				}
				else
					dt = Mession.GlbSearchData;
				dtgFormula.DataSource = dt;
				dtgFormula.DataBind();

				for (int i = 0; i<this.dtgFormula.Items.Count; i++)
				{
					DropDownList cboFormula = (DropDownList)this.dtgFormula.Items[i].FindControl("cboFormula");
					
					cboFormula.Items.Clear();	
					clsCommon.LoadDropDownListControl(cboFormula, "sp_GetDataCombo @TableName='PR_tblSetFormula',@Fields='SetFormulaID as [ID],NameFormula as Name'", "ID", "Name",true);
					if (RadStatus.SelectedValue == "1")
					{
						string sSetFormulaID = dtgFormula.Items[i].Cells[1].Text.Trim(); 
						cboFormula.SelectedValue = sSetFormulaID;
					}
				}
			}
			catch(Exception exp)
			{
				this.lblErr.Text = exp.Message.ToString();
			}
		}

		private void dtgFormula_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			if (RadStatus.SelectedValue == "0")
				btnDelete.Enabled = false;
			else
				btnDelete.Enabled = true;

			this.dtgFormula.CurrentPageIndex = e.NewPageIndex;
			BindDataGrid(false);
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			string strSql = "";
			string strMsg = "";
			for(int i=0;i<dtgFormula.Items.Count;i++)
			{
				CheckBox chk = (CheckBox)this.dtgFormula.Items[i].FindControl("chkSelect");
				if(chk.Checked)
				{
					string sEmpID = dtgFormula.Items[i].Cells[0].Text.Trim(); 
					string sSetFormulaID = dtgFormula.Items[i].Cells[1].Text.Trim(); 

					strSql = "PR_clsSetFormulaEmp @Activity='Delete', @EmpID='" + sEmpID + "', @SetFormulaID = '" + sSetFormulaID + "'";	
					strMsg = clsDB.Exc_CommandText(strSql);					
					if (strMsg != "")
						break;

					iHRPCore.Com.clsCommon.Exc_CommandText("[UMS_sptblAccessCommon] @Activity='UpdateFormulaPR', @UserGroupID = '" 
						+ Session["AccountLogin"].ToString() + "',@LSCompanyID = '" + Mession.GlbCusGroupID + "'" );
				}
			}
			if (strMsg == "")
			{
				BindDataGrid(true);
				clsChangeLang.popupWindow(this.Parent,"0044",strLanguage,"",1);
			}
			else
				lblErr.Text = strMsg;
		}
	}
}

