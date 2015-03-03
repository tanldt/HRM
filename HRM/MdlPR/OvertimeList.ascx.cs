namespace iHRPCore.MdlPR
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.Com;
	using iHRPCore.PRComponent;
	using iHRPCore.Include;
	using GridSort;
	/// <summary>
	///		Summary description for HR_frmOvertime1.
	/// </summary>
	public class OvertimeList : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.DropDownList cboCol1;
		protected System.Web.UI.WebControls.DropDownList cboCol2;
		protected System.Web.UI.WebControls.DropDownList cboCol3;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.LinkButton btnAddNew;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.LinkButton btnSearch;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.DropDownList cboChargeBudgetCode;
		protected System.Web.UI.WebControls.TextBox txtFromDate;
		protected System.Web.UI.WebControls.TextBox txtToDate;
		protected System.Web.UI.WebControls.DropDownList cboOTTypeCode;		
		protected string strLanguage = "EN";
		protected System.Web.UI.WebControls.TextBox txtPRMonth;
		protected System.Web.UI.WebControls.DataGrid grdOvertime;
		protected EmpHeaderSearch EmpHeaderSearch1;
		protected ColumnList uctrlColumns;
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(!Page.IsPostBack)
			{
				string strTextField = strLanguage == "VN"?"VNName":"Name";			
				clsCommon.LoadDropDownListControl(cboOTTypeCode,"sp_GetDataCombo @TableName='LS_tblOTType',@Fields='LSOTTypeCode," + strTextField + " as Name'","LSOTTypeCode","Name",true);
				clsCommon.LoadDropDownListControl(cboChargeBudgetCode,"sp_GetDataCombo @TableName='LS_tblChargeBudget',@Fields='LSChargeBudgetCode," + strTextField + " as Name'","LSChargeBudgetCode","Name",true);
				grdOvertime.DataSource = new DataTable();
				grdOvertime.DataBind();

				//Hau
				DataGridSort.AddItemColumn(uctrlColumns, grdOvertime);//
			}
			btnDelete.Attributes.Add("OnClick", "return checkdelete()");
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
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.grdOvertime.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.grdOvertime_ItemCommand);
			this.grdOvertime.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.grdOvertime_PageIndexChanged_1);
			this.grdOvertime.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.grdOvertime_SortCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			LoadDataResult();
		}
		private void LoadDataResult()
		{
			DataTable dt = new DataTable();
			try
			{
				string strEmpID = EmpHeaderSearch1.txtEmpID.Text.Trim();
				string strEmpName = EmpHeaderSearch1.txtEmpName.Text.Trim();
				string strLevel1 = EmpHeaderSearch1.cboLevel1.SelectedValue.Trim();
				string strLevel2 = EmpHeaderSearch1.cboLevel2.SelectedValue.Trim();
				string strLevel3 = EmpHeaderSearch1.cboLevel3.SelectedValue.Trim();
				string strLocation = EmpHeaderSearch1.cboLocation.SelectedValue.Trim();
				string strJobCode = EmpHeaderSearch1.cboJobcode.SelectedValue.Trim();
				string strPosition = EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue.Trim();
				string strStatus = EmpHeaderSearch1.optStatus.SelectedValue.Trim();
				dt = clsPROvertime.GetListSearch(strEmpID, strEmpName, strLevel1, strLevel2, strLevel3,strPosition, strJobCode, strLocation,strStatus,cboOTTypeCode.SelectedValue,txtFromDate.Text,txtToDate.Text
					,cboChargeBudgetCode.SelectedValue.Trim(),txtPRMonth.Text.Trim());
				
				//grdOvertime.DataSource = dt;
				//grdOvertime.DataBind();

				//Hau
				this.grdOvertime.PageSize = uctrlColumns.iPageRows;
				if(this.uctrlColumns.bAdvMultiSort)
				{
					try
					{
						DataGridSort.AdvancedMultiSort(grdOvertime, dt, uctrlColumns);
					}
					catch
					{
						grdOvertime.CurrentPageIndex = 0;
						DataGridSort.AdvancedMultiSort(grdOvertime, dt, uctrlColumns);
					}
				}
				else
				{
					try
					{
						DataGridSort.Refresh(grdOvertime, dt);
					}
					catch{
						grdOvertime.CurrentPageIndex = 0;
						DataGridSort.Refresh(grdOvertime, dt);
					}
				}
				this.uctrlColumns.iTotalRows = dt.Rows.Count;
				//
				lblErr.Text = "";
			}
			catch (Exception ex)
			{
				lblErr.Text = ex.Message;
			}
			finally
			{
				dt.Dispose();
			}
		}
		private void grdOvertime_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			try
			{
				LoadDataResult();
				grdOvertime.CurrentPageIndex=e.NewPageIndex;
				grdOvertime.DataBind();
			}
			catch(Exception ex)			
			{
				string str = ex.Message;
			}
		}
		/// <summary>
		/// delete checked record
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				string strID="";
				for(int i=0;i<grdOvertime.Items.Count;i++)
				{
					if(((CheckBox)grdOvertime.Items[i].FindControl("chkSelect")).Checked==true)
					{
						strID += grdOvertime.Items[i].Cells[0].Text.Trim() + "$";
					}
				}
				clsCommon.DeleteListRecord("PR_spfrmOT","Delete","OTID",SqlDbType.Int,4,strID);				
				LoadDataResult();
				lblErr.Text = "";
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}

		private void grdOvertime_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if(e.CommandName.Trim().ToUpper() =="EDIT")
			{
				Session["ssOvertimeID"] = grdOvertime.Items[e.Item.ItemIndex].Cells[0].Text.Trim();
				Session["EmpID"] = grdOvertime.Items[e.Item.ItemIndex].Cells[2].Text.Trim();
				Response.Redirect("Editpage.aspx?ModuleID=PR&ParentID=68&FunctionID=74&Ascx=MdlPR/OverTime.ascx");
			}
		}
		#region Cac su kien xu li thao tac Sort (Hau)
		#endregion
		private void grdOvertime_PageIndexChanged_1(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			try{
				DataGridSort.Grid_IndexChanged(grdOvertime, Read_Data(), e);
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
			string strStatus = EmpHeaderSearch1.optStatus.SelectedValue.Trim();

			DataTable dt = new DataTable();
			dt = clsPROvertime.GetListSearch(strEmpID, strEmpName, strLevel1, strLevel2, strLevel3,strPosition, strJobCode, strLocation,strStatus,cboOTTypeCode.SelectedValue,txtFromDate.Text,txtToDate.Text
				,cboChargeBudgetCode.SelectedValue.Trim(),txtPRMonth.Text.Trim());

			return dt;
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
				DataGridSort.AdvancedMultiSort(grdOvertime, Read_Data(), uctrlColumns);
			}
			catch{}
		}

		private void ButtonPRChanged_ServerClick(object sender, EventArgs e)
		{
			try
			{
				grdOvertime.PageSize = uctrlColumns.iPageRows;
				grdOvertime.CurrentPageIndex = 0;
				DataView dv = new DataView(Read_Data());
				dv.Sort = DataGridSort.sMulSort;
				grdOvertime.DataSource = dv;
				grdOvertime.DataBind();
			}
			catch{}
		}

		private void grdOvertime_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			try
			{
				DataGridSort.Grid_Sort(grdOvertime, Read_Data(), e, uctrlColumns);
			}
			catch{}
		}
	}
}
