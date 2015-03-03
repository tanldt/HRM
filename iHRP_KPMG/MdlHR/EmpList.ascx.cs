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
	public class EmpList : System.Web.UI.UserControl
	{
		#region Declare
		protected System.Web.UI.WebControls.Label lblErr;
		protected FPTToolWeb.Control.DataGrids.FPTDataGrid dtgList;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.TextBox txtPageRows;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.DropDownList cboCol1;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.DropDownList cboCol2;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.DropDownList cboCol3;
		protected System.Web.UI.WebControls.TextBox txtPageLoad;
		protected System.Web.UI.WebControls.CheckBox chkAdvanceSort;
		protected System.Web.UI.WebControls.CheckBox chkMultiSort;		
		protected EmpHeaderSearch EmpHeaderSearch1;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnSort;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label lblTotalRows;
		protected System.Web.UI.WebControls.LinkButton button;
		protected System.Web.UI.WebControls.LinkButton btnSearch;
		protected ColumnList uctrlColumns;
		#endregion
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				txtPageLoad.Text = "1";
				//if (Request.Params["MultiSelect"]
				this.BindDataGrid();
				//Hau
				DataGridSort.AddItemColumn(uctrlColumns, dtgList);//
			}			
			//Hau
			ButtonClick();//
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
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			this.dtgList.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgList_ItemCommand);
			this.dtgList.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dtgList_SortCommand);
			this.dtgList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgList_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion]

		private void BindDataGrid()
		{
			try
			{
				//Lay gia tri cua HeaderEmpSearch
				string strEmpID = EmpHeaderSearch1.txtEmpID.Text.Trim().Replace("'","");
				string strEmpName = EmpHeaderSearch1.txtEmpName.Text.Trim().Replace("'","");
				string strLevel1 = EmpHeaderSearch1.cboLevel1.SelectedValue.Trim();
				string strLevel2 = EmpHeaderSearch1.cboLevel2.SelectedValue.Trim();
				string strLevel3 = EmpHeaderSearch1.cboLevel3.SelectedValue.Trim();
				string strLocation = EmpHeaderSearch1.cboLocation.SelectedValue.Trim();
				string strJobCode = EmpHeaderSearch1.cboJobcode.SelectedValue.Trim();
				string strPosition = EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue.Trim();
				string strCompany = EmpHeaderSearch1.cboCompany.SelectedValue.Trim();
				string strStatus = EmpHeaderSearch1.optStatus.SelectedValue.Trim();
				string sEmpTypeID = EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue.Trim();
				int intTopRow =0;
				if ( txtPageLoad.Text == "1" & strEmpID =="" & strEmpName == "" & strLevel2 == "" & strLocation == "" & strJobCode == "" & strPosition =="" )
				{
					intTopRow = dtgList.PageSize;
					txtPageLoad.Text = "0";
				}

				DataTable dtb = clsHREmpList.GetEmpList(strEmpID, strEmpName, strLevel1, strLevel2, strLevel3,strPosition, strJobCode, strLocation, strCompany, strStatus,intTopRow,this.Page,"0",sEmpTypeID);

				//Hau
				this.dtgList.PageSize = uctrlColumns.iPageRows;
				
				if(this.uctrlColumns.bAdvMultiSort)
				{
					try
					{
						DataGridSort.AdvancedMultiSort(dtgList, dtb, uctrlColumns);
					}
					catch
					{
						this.dtgList.CurrentPageIndex = 0;
						DataGridSort.AdvancedMultiSort(dtgList, dtb, uctrlColumns);
					}
				}
				else
				{
					try
					{
						DataGridSort.Refresh(dtgList, dtb);
					}
					catch
					{
						this.dtgList.CurrentPageIndex = 0;
						DataGridSort.Refresh(dtgList, dtb);
					}
				}
//					dtgList.DataBind();
				this.uctrlColumns.iTotalRows = dtb.Rows.Count;//

				dtb.Dispose();
			}
			catch (Exception ex)
			{
				lblErr.Text=ex.Message;
			}

		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			BindDataGrid();
			Session["CompanyID"] = EmpHeaderSearch1.cboCompany.SelectedValue.Trim();
			Session["Level1ID"] = EmpHeaderSearch1.cboLevel1.SelectedValue.Trim();
			Session["Level2ID"] = EmpHeaderSearch1.cboLevel2.SelectedValue.Trim();
			Session["Level3ID"] = EmpHeaderSearch1.cboLevel3.SelectedValue.Trim();
			
		}

		private void dtgList_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			DataTable tbl = new DataTable();
			if (e.CommandName == "hpLink")
			{
				Session["EmpID"] = e.Item.Cells[1].Text.Trim();

				object aa = Request.Params["CurFunction"];
				object bb = Session["CurFunction"];
				string strFunctionID = Request.Params["CurFunction"] != null? Request.Params["CurFunction"].Trim():
				(Session["CurFunction"] != null? Session["CurFunction"].ToString():"19");
				//string strFunctionID = "19";
				tbl = clsHREmpList.GetFunctionByID(strFunctionID);
				if (tbl.Rows.Count > 0)
				{
					Response.Redirect(tbl.Rows[0]["Url"].ToString() + "?ModuleID=" + tbl.Rows[0]["ModuleID"].ToString() 
						+ "&ParentID=" + tbl.Rows[0]["Parent"].ToString() + "&FunctionID=" 
						+ tbl.Rows[0]["FunctionID"].ToString() + "&Ascx=" + tbl.Rows[0]["Ascx"].ToString());
				}
			}
			tbl.Dispose();
		}

		private void dtgList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.BindDataGrid();
			this.dtgList.CurrentPageIndex = e.NewPageIndex;
			this.dtgList.DataBind();
			
			//Hau
			try
			{
				DataGridSort.Grid_IndexChanged(dtgList, Read_Data(), e);
			}
			catch{}//
		}

		private void btnExport_Click(object sender, System.EventArgs e)
		{
			dtgList.AllowPaging = false;
			this.BindDataGrid();
			iHRPCore.DataGridExcelExporter myExcelXport=new DataGridExcelExporter(dtgList);
			myExcelXport.Export("");
			myExcelXport = null;
			this.dtgList.AllowPaging = true;
			this.BindDataGrid();
			
		/*iHRPCore.DataGridExcelExporter myExcelXport=new DataGridExcelExporter(dtgList);
			myExcelXport.Export("");
			myExcelXport = null;
		*/
		}

		private void btnAddNew_Click(object sender, System.EventArgs e)
		{
		
		}
		#region Cac su kien xu li thao tac Sort (Hau)

		private void dtgList_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			try
			{
				DataGridSort.Grid_Sort(dtgList, Read_Data(), e, uctrlColumns);
			}
			catch{}
		}

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
			string sEmpTypeID = EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue.Trim();
			int intTopRow =0;
			if ( txtPageLoad.Text == "1" & strEmpID =="" & strEmpName == "" & strLevel2 == "" & strLocation == "" & strJobCode == "" & strPosition =="" )
			{
				intTopRow = dtgList.PageSize;
				txtPageLoad.Text = "0";
			}

			DataTable dtb = clsHREmpList.GetEmpList(strEmpID, strEmpName, strLevel1, strLevel2, strLevel3,strPosition, strJobCode, strLocation, strCompany, strStatus,intTopRow,this.Page,"0",sEmpTypeID);
			return dtb;
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
				DataGridSort.AdvancedMultiSort(dtgList, Read_Data(), uctrlColumns);
			}
			catch{}
		}

		private void ButtonPRChanged_ServerClick(object sender, EventArgs e)
		{
			try
			{
				dtgList.PageSize = uctrlColumns.iPageRows;
				dtgList.CurrentPageIndex = 0;
				DataView dv = new DataView(Read_Data());
				dv.Sort = DataGridSort.sMulSort;
				dtgList.DataSource = dv;
				dtgList.DataBind();
			}
			catch{}
		}
		#endregion		

	}
}
