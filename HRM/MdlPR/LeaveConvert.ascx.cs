namespace iHRPCore.MdlPR
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.PRComponent;
	using iHRPCore.Include;
	using iHRPCore.Com;
	using GridSort;

	/// <summary>
	///		Summary description for LeaveConvert.
	/// </summary>
	public class LeaveConvert : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtYear;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label lblRemain;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.Label lblCarry;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.Label lblConvert;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.Label lblAmount;
		protected System.Web.UI.WebControls.Label lblUnit;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label19;
		protected System.Web.UI.WebControls.LinkButton btnSearch;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.LinkButton btnImport;
		protected System.Web.UI.WebControls.LinkButton Linkbutton2;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.DropDownList cboCol1;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.DropDownList cboCol2;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.DropDownList cboCol3;
		protected System.Web.UI.WebControls.TextBox txtRefMonth;
		protected System.Web.UI.WebControls.TextBox txtPRMonth;
		protected System.Web.UI.WebControls.DropDownList cboMethodID;
		protected System.Web.UI.WebControls.DataGrid grdLeaveConvert;
		protected System.Web.UI.WebControls.TextBox txtNote;
		protected System.Web.UI.WebControls.LinkButton btnGetLeave;
		protected EmpHeaderSearch EmpHeaderSearch1;
		protected ColumnList uctrlColumns;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(!IsPostBack)
			{
				txtYear.Text = DateTime.Today.Year.ToString();

				//Hau
				DataGridSort.AddItemColumn(uctrlColumns, grdLeaveConvert);//
			}
			btnGetLeave.Attributes.Add("OnClick", " return validform()");
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
			this.btnGetLeave.Click += new System.EventHandler(this.btnGetLeave_Click);
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.Linkbutton2.Click += new System.EventHandler(this.Linkbutton2_Click);
			this.grdLeaveConvert.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.grdLeaveConvert_PageIndexChanged);
			this.grdLeaveConvert.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.grdLeaveConvert_SortCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			SearchData();
			btnSave.Enabled = false;
			btnDelete.Enabled = true;
		}
		private void BindDataGrid()
		{
			DataTable dtResult = new DataTable();
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
				string strCompany = EmpHeaderSearch1.cboCompany.SelectedValue.Trim();
				dtResult = clsLeaveConvert.GetConvert(strEmpID, strEmpName, strLevel1, strLevel2, strLevel3,strPosition, strJobCode, strLocation, strCompany
					, txtYear.Text.Trim(),0,txtRefMonth.Text.Trim(),txtPRMonth.Text.Trim(),cboMethodID.SelectedValue.Trim(),txtNote.Text);
				/*
				grdLeaveConvert.DataSource = dtResult;
				grdLeaveConvert.DataBind();
				lblTotalRows.Text = dtResult.Rows.Count.ToString();
				*/

				//Hau
				this.grdLeaveConvert.PageSize = uctrlColumns.iPageRows;
				this.grdLeaveConvert.CurrentPageIndex = 0;
				if(this.uctrlColumns.bAdvMultiSort)
					DataGridSort.AdvancedMultiSort(grdLeaveConvert, dtResult, uctrlColumns);
				else
					DataGridSort.Refresh(grdLeaveConvert, dtResult);
				this.uctrlColumns.iTotalRows = dtResult.Rows.Count;//
			}
			catch(Exception ex)
				{
					lblErr.Text = ex.Message;
				}
				finally
			{
				dtResult.Dispose();
			}
		}
		// SEARCH RESULT
		private void SearchData()
		{
			DataTable dtResult = new DataTable();
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
				string strCompany = EmpHeaderSearch1.cboCompany.SelectedValue.Trim();
				dtResult = clsLeaveConvert.SearchLeaveConvert(strEmpID, strEmpName, strLevel1, strLevel2, strLevel3,strPosition, strJobCode, strLocation, strCompany
					, txtYear.Text.Trim(),cboMethodID.SelectedValue.Trim());
				/*
				grdLeaveConvert.DataSource = dtResult;
				grdLeaveConvert.DataBind();
				lblTotalRows.Text = dtResult.Rows.Count.ToString();
				*/

				//Hau
				this.grdLeaveConvert.PageSize = uctrlColumns.iPageRows;
				if(this.uctrlColumns.bAdvMultiSort)
				{
					try
					{
						DataGridSort.AdvancedMultiSort(grdLeaveConvert, dtResult, uctrlColumns);
					}
					catch
					{
						this.grdLeaveConvert.CurrentPageIndex = 0;
						DataGridSort.AdvancedMultiSort(grdLeaveConvert, dtResult, uctrlColumns);
					}
				}
				else
				{
					try
					{
						DataGridSort.Refresh(grdLeaveConvert, dtResult);
					}
					catch{
						this.grdLeaveConvert.CurrentPageIndex = 0;
						DataGridSort.Refresh(grdLeaveConvert, dtResult);
					}
				}
				this.uctrlColumns.iTotalRows = dtResult.Rows.Count;//

			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
			finally
			{
				dtResult.Dispose();
			}
		}
		private void btnGetLeave_Click(object sender, System.EventArgs e)
		{
			BindDataGrid();
			btnSave.Enabled = true;
			btnDelete.Enabled = false;
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			lblErr.Text =  clsLeaveConvert.SaveLeaveconvert(grdLeaveConvert);
		}

		private void Linkbutton2_Click(object sender, System.EventArgs e)
		{
			iHRPCore.DataGridExcelExporter myExcelXport=new DataGridExcelExporter(grdLeaveConvert);			
			myExcelXport.Export("");
			myExcelXport =null;
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				string strID="";
				for(int i=0;i<grdLeaveConvert.Items.Count;i++)
				{
					if(((CheckBox)grdLeaveConvert.Items[i].FindControl("chkSelect")).Checked==true)
					{
						strID += grdLeaveConvert.Items[i].Cells[0].Text.Trim() + "$";
					}
				}
				clsCommon.DeleteListRecord("PR_spfrmLEAVECONVERT","LeaveConvertID",SqlDbType.Int,4,strID);
				btnSearch_Click(null,null);
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}

		#region Cac su kien xu li thao tac Sort (Hau)

		private void grdLeaveConvert_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			try
			{
				DataGridSort.Grid_Sort(grdLeaveConvert, Read_Data(), e, uctrlColumns);
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

			DataTable dtResult = new DataTable();
			dtResult = clsLeaveConvert.SearchLeaveConvert(strEmpID, strEmpName, strLevel1, strLevel2, strLevel3,strPosition, strJobCode, strLocation, strCompany
				, txtYear.Text.Trim(),cboMethodID.SelectedValue.Trim());
			return dtResult;
		}
		
		private void grdLeaveConvert_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			try
			{
				DataGridSort.Grid_IndexChanged(grdLeaveConvert, Read_Data(), e);
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
				DataGridSort.AdvancedMultiSort(grdLeaveConvert, Read_Data(), uctrlColumns);
			}
			catch{}
		}

		private void ButtonPRChanged_ServerClick(object sender, EventArgs e)
		{
			try
			{
				grdLeaveConvert.PageSize = uctrlColumns.iPageRows;
				grdLeaveConvert.CurrentPageIndex = 0;
				DataView dv = new DataView(Read_Data());
				dv.Sort = DataGridSort.sMulSort;
				grdLeaveConvert.DataSource = dv;
				grdLeaveConvert.DataBind();
			}
			catch{}
		}
		#endregion
	}
}
