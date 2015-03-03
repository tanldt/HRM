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
	using iHRPCore.Component;
	/// <summary>
	///		Summary description for DeductionList.
	/// </summary>
	public class DeductionList : UserControlCommon
	{
		protected EmpHeaderSearch EmpHeaderSearch1;
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.LinkButton btnSearch;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.CheckBox Checkbox1;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.DropDownList cboCol1;
		protected System.Web.UI.WebControls.DropDownList cboCol2;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.TextBox txtToDate;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtFromDate;
		protected System.Web.UI.WebControls.DropDownList cboLSDeductionCode;
		protected System.Web.UI.WebControls.DataGrid grdDeduction;
		protected System.Web.UI.WebControls.DropDownList cboCol3;
		protected ColumnList uctrlColumns;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here		
			if(!Page.IsPostBack)
			{
				LoadDataCombo();
				grdDeduction.DataSource = new DataTable();				
				grdDeduction.DataBind();

				//Hau
				DataGridSort.AddItemColumn(uctrlColumns, grdDeduction);//
			}
			btnDelete.Attributes.Add("OnClick", "return checkdelete()");
			btnSearch.Attributes.Add("OnClick", "return Search()");

			//Hau
			ButtonClick();//
		}
		private void LoadDataCombo()
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			clsCommon.LoadDropDownListControl(cboLSDeductionCode,"sp_GetDataCombo @TableName='LS_tblDeduction',@Fields='LSDeductionID," + strTextField + " as Name'","LSDeductionID","Name",true);
		}
		private void BindDataGrid()
		{
			DataTable dtResult = new DataTable();
			try
			{
				//Lay gia tri cua HeaderEmpSearch
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
				string strLanguageID=Session["LangID"].ToString();
				dtResult = clsPRDeduction.GetListSearch(strEmpID, strEmpName, strLevel1, strLevel2, strLevel3,strPosition, strJobCode, strLocation, strCompany, strStatus,cboLSDeductionCode.SelectedValue.Trim()
					,txtFromDate.Text.Trim(),txtToDate.Text.Trim(),strLanguageID, this.Page);
				//grdDeduction.DataSource = dtResult;
				//grdDeduction.DataBind();

				//Hau
				this.grdDeduction.PageSize = uctrlColumns.iPageRows;
				if(this.uctrlColumns.bAdvMultiSort)
				{
					try
					{
						DataGridSort.AdvancedMultiSort(grdDeduction, dtResult, uctrlColumns);
					}
					catch
					{
						grdDeduction.CurrentPageIndex = 0;
						DataGridSort.AdvancedMultiSort(grdDeduction, dtResult, uctrlColumns);
					}
				}
				else
				{
					try
					{
						DataGridSort.Refresh(grdDeduction, dtResult);				
					}
					catch{
						grdDeduction.CurrentPageIndex = 0;
						DataGridSort.Refresh(grdDeduction, dtResult);
					}
				}
				this.uctrlColumns.iTotalRows = dtResult.Rows.Count;
				//
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
			this.grdDeduction.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.grdDeduction_ItemCommand);
			this.grdDeduction.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.grdDeduction_PageIndexChanged);
			this.grdDeduction.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.grdDeduction_SortCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			BindDataGrid();
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				string strID="";
				for(int i=0;i<grdDeduction.Items.Count;i++)
				{
					if(((CheckBox)grdDeduction.Items[i].FindControl("chkSelect")).Checked==true)
					{
						strID += grdDeduction.Items[i].Cells[0].Text.Trim() + "$";
					}
				}
				clsCommon.DeleteListRecord("PR_spfrmDEDUCTION","DeductionID",SqlDbType.NVarChar,12,strID);
				BindDataGrid();
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}
		//EXPORT GRID DATA
		private void btnExport_Click(object sender, System.EventArgs e)
		{
			iHRPCore.DataGridExcelExporter myExcelXport=new DataGridExcelExporter(grdDeduction);			
			myExcelXport.Export("");
			myExcelXport =null;
		}

		//EDIT RECORD
		private void grdDeduction_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if(e.CommandName.Trim().ToUpper() =="EDIT")
			{
				Session["ssDeductionID"] = grdDeduction.Items[e.Item.ItemIndex].Cells[0].Text.Trim();
				Session["EmpID"] = grdDeduction.Items[e.Item.ItemIndex].Cells[2].Text.Trim();
				Response.Redirect("Editpage.aspx?ModuleID=PR&ParentID=68&FunctionID=85&Ascx=MdlPR/Deduction.ascx");
			}
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
			string strLanguageID=Session["LangID"].ToString();
			DataTable dtResult = new DataTable();
			dtResult = clsPRDeduction.GetListSearch(strEmpID, strEmpName, strLevel1, strLevel2, strLevel3,strPosition, strJobCode, strLocation, strCompany, strStatus,cboLSDeductionCode.SelectedValue.Trim()
				,txtFromDate.Text.Trim(),txtToDate.Text.Trim(),strLanguageID, this.Page);
			return dtResult;
		}

		private void grdDeduction_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			try
			{
				DataGridSort.Grid_Sort(grdDeduction, Read_Data(), e, uctrlColumns);
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
				DataGridSort.AdvancedMultiSort(grdDeduction, Read_Data(), uctrlColumns);
			}
			catch{}
		}

		private void ButtonPRChanged_ServerClick(object sender, EventArgs e)
		{
			try
			{
				grdDeduction.PageSize = uctrlColumns.iPageRows;
				grdDeduction.CurrentPageIndex = 0;
				DataView dv = new DataView(Read_Data());
				dv.Sort = DataGridSort.sMulSort;
				grdDeduction.DataSource = dv;
				grdDeduction.DataBind();
			}
			catch{}
		}
		
		private void grdDeduction_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			//Hau
			try
			{
				DataGridSort.Grid_IndexChanged(grdDeduction, Read_Data(), e);
			}
			catch{}//
		}

		#endregion
	}
}
