namespace iHRPCore.MdlPR
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.Com;
	using iHRPCore.Include;
	using iHRPCore.PRComponent;
	using GridSort;
	/// <summary>
	///		Summary description for LoadList.
	/// </summary>
	public class LoadList : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.CheckBox chkShowGrid;
		protected System.Web.UI.WebControls.LinkButton btnSearch;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.DropDownList cboCol1;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.DropDownList cboCol2;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.DropDownList cboCol3;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList cboLSLoanPurposeCode;
		protected string strLanguage = "EN";
		protected System.Web.UI.WebControls.DataGrid grdLoan;
		protected EmpHeaderSearch EmpHeaderSearch1;
		protected System.Web.UI.WebControls.TextBox txtFromMonth;
		protected System.Web.UI.WebControls.TextBox txtToMonth;
		protected ColumnList uctrlColumns;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here			
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"VN";			
			if(!Page.IsPostBack)
			{
				LoadDataCombo();
				grdLoan.DataSource = new DataTable();
				grdLoan.DataBind();
				DataGridSort.AddItemColumn(uctrlColumns, grdLoan);
			}
			btnDelete.Attributes.Add("OnClick", "return checkdelete()");
			btnSearch.Attributes.Add("OnClick", "return Search()");
			ButtonClick();
		}
		private void LoadDataCombo()
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";				
			clsCommon.LoadDropDownListControl(cboLSLoanPurposeCode,"sp_GetDataCombo @TableName='LS_tblLoanPurpose',@Fields='LSLoanPurposeID," + strTextField + " as Name'","LSLoanPurposeID","Name",true);
		}
		private void BindDataGrid()
		{
			
			DataTable dtResult = new DataTable();
			try
			{
				#region Lay gia tri cua HeaderEmpSearch
				string strEmpID = EmpHeaderSearch1.txtEmpID.Text.Trim();
				string strEmpName = EmpHeaderSearch1.txtEmpName.Text.Trim();
				string strLevel1 = EmpHeaderSearch1.cboLevel1.SelectedValue.Trim();
				string strLevel2 = EmpHeaderSearch1.cboLevel2.SelectedValue.Trim();
				string strLevel3 = EmpHeaderSearch1.cboLevel3.SelectedValue.Trim();
				string strLocation = EmpHeaderSearch1.cboLocation.SelectedValue.Trim();
				string strJobCode = EmpHeaderSearch1.cboJobcode.SelectedValue.Trim();
				string strPosition = EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue.Trim();
				string strCompany = EmpHeaderSearch1.cboCompany.SelectedValue.Trim();
				string strStatus = EmpHeaderSearch1.optStatus.SelectedValue.Trim();
				#endregion

				dtResult = clsPRLoan.GetListSearch(strEmpID, strEmpName, strLevel1, strLevel2, strLevel3, strCompany, strStatus,cboLSLoanPurposeCode.SelectedValue.Trim()
					,txtFromMonth.Text.Trim(),txtToMonth.Text.Trim(), this.Page);
				grdLoan.DataSource = dtResult;
				grdLoan.DataBind();

				
				this.grdLoan.PageSize = uctrlColumns.iPageRows;
				if(this.uctrlColumns.bAdvMultiSort)
				{
					try
					{
						DataGridSort.AdvancedMultiSort(grdLoan, dtResult, uctrlColumns);
					}
					catch
					{
						grdLoan.CurrentPageIndex = 0;
						DataGridSort.AdvancedMultiSort(grdLoan, dtResult, uctrlColumns);
					}
				}
				else
				{
					try
					{
						DataGridSort.Refresh(grdLoan, dtResult);
					}
					catch{
						grdLoan.CurrentPageIndex = 0;
						DataGridSort.Refresh(grdLoan, dtResult);
					}
				}
				this.uctrlColumns.iTotalRows = dtResult.Rows.Count;
			}
			catch (Exception ex)
			{
				lblErr.Text=ex.Message;
			}
			finally
			{
				if(dtResult != null)
					dtResult.Dispose();
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
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			this.grdLoan.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.grdLoan_ItemCommand);
			this.grdLoan.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.grdLoan_PageIndexChanged);
			this.grdLoan.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.grdLoan_SortCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			this.grdLoan.CurrentPageIndex = 0;
			BindDataGrid();
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				string strID="";
				for(int i=0; i<grdLoan.Items.Count; i++)
				{
					if(((CheckBox)grdLoan.Items[i].FindControl("chkSelect")).Checked==true)
					{
						strID += grdLoan.Items[i].Cells[0].Text.Trim() + "$";
					}
				}
				clsCommon.DeleteListRecord("PR_spfrmLoan","LoanID",SqlDbType.VarChar,12,strID);
				clsCommon.ClearControlValue(this);
				BindDataGrid();
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}

		private void btnExport_Click(object sender, System.EventArgs e)
		{
			this.grdLoan.AllowPaging = false;
			BindDataGrid();
			iHRPCore.DataGridExcelExporter myExcelXport=new DataGridExcelExporter(grdLoan);			
			myExcelXport.Export("");
			myExcelXport =null;
			this.grdLoan.AllowPaging = true;
			BindDataGrid();
		}

		#region Cac su kien xu li thao tac Sort (Hau)

		private DataTable Read_Data()
		{
			string strEmpID = EmpHeaderSearch1.txtEmpID.Text.Trim();
			string strEmpName = EmpHeaderSearch1.txtEmpName.Text.Trim();
			string strLevel1 = EmpHeaderSearch1.cboLevel1.SelectedValue.Trim();
			string strLevel2 = EmpHeaderSearch1.cboLevel2.SelectedValue.Trim();
			string strLevel3 = EmpHeaderSearch1.cboLevel3.SelectedValue.Trim();
			string strLocation = EmpHeaderSearch1.cboLocation.SelectedValue.Trim();
			string strJobCode = EmpHeaderSearch1.cboJobcode.SelectedValue.Trim();
			string strPosition = EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue.Trim();
			string strCompany = EmpHeaderSearch1.cboCompany.SelectedValue.Trim();
			string strStatus = EmpHeaderSearch1.optStatus.SelectedValue.Trim();

			DataTable dtResult = new DataTable();
			dtResult = clsPRLoan.GetListSearch(strEmpID, strEmpName, strLevel1, strLevel2, strLevel3, strCompany, strStatus, cboLSLoanPurposeCode.SelectedValue.Trim()
				,txtFromMonth.Text.Trim(),txtToMonth.Text.Trim(),this.Page);
			return dtResult;
		}
		
		private void grdLoan_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			try
			{
				DataGridSort.Grid_Sort(grdLoan, Read_Data(), e, uctrlColumns);
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
				DataGridSort.AdvancedMultiSort(grdLoan, Read_Data(), uctrlColumns);
			}
			catch{}
		}

		private void ButtonPRChanged_ServerClick(object sender, EventArgs e)
		{
			try
			{
				grdLoan.PageSize = uctrlColumns.iPageRows;
				grdLoan.CurrentPageIndex = 0;
				DataView dv = new DataView(Read_Data());
				dv.Sort = DataGridSort.sMulSort;
				grdLoan.DataSource = dv;
				grdLoan.DataBind();
			}
			catch{}
		}
		#endregion

		private void grdLoan_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.grdLoan.CurrentPageIndex = e.NewPageIndex;
			BindDataGrid();
			try
			{
				DataGridSort.Grid_IndexChanged(grdLoan, Read_Data(), e);
			}
			catch{}
		}

		private void grdLoan_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if(e.CommandName.Trim().ToUpper() =="EDIT")
			{
				Session["ssLoanID"] = grdLoan.Items[e.Item.ItemIndex].Cells[0].Text.Trim();
				Session["EmpID"] = grdLoan.Items[e.Item.ItemIndex].Cells[1].Text.Trim();
				Response.Redirect("Editpage.aspx?ModuleID=PR&ParentID=68&FunctionID=120&Ascx=MdlPR/Loan.ascx");
			}
		}
	}
}
