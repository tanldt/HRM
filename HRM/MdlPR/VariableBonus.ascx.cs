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
	///		Summary description for VariableBonus.
	/// </summary>
	public class VariableBonus : System.Web.UI.UserControl
	{
		public string strLanguage = "EN";
		protected EmpHeaderSearch EmpHeaderSearch1;
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.DropDownList cboYear;
		protected System.Web.UI.WebControls.CheckBox chkSetup;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DataGrid grdAppraisal;
		protected System.Web.UI.WebControls.LinkButton Linkbutton3;
		protected System.Web.UI.WebControls.CheckBox chkShowGrid;
		protected System.Web.UI.WebControls.LinkButton btnSearch;
		protected System.Web.UI.WebControls.LinkButton btnCalculate;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.DropDownList cboCol1;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.DropDownList cboCol2;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.DropDownList cboCol3;
		protected System.Web.UI.WebControls.DataGrid grdStaffLevel;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.DataGrid grdBonus;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtID;
		protected System.Web.UI.WebControls.LinkButton btnUpdate;
		protected System.Web.UI.HtmlControls.HtmlTableRow trSetup;
		protected ColumnList uctrlColumns;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
			if(!Page.IsPostBack)
			{
				clsCommon.LoadYearToList(cboYear,DateTime.Today.Year - 2,DateTime.Today.Year);
				cboYear.SelectedValue = DateTime.Today.Year.ToString();
				cboYear_SelectedIndexChanged(null,null);
				grdBonus.DataSource = new DataTable();
				grdBonus.DataBind();

				//Hau
				DataGridSort.AddItemColumn(uctrlColumns, grdBonus);//
			}
			btnDelete.Attributes.Add("OnClick", "return checkdelete()");
			btnSave.Attributes.Add("OnClick", "return checksave()");

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
			this.cboYear.SelectedIndexChanged += new System.EventHandler(this.cboYear_SelectedIndexChanged);
			this.grdStaffLevel.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.grdStaffLevel_ItemCommand);
			this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
			this.Linkbutton3.Click += new System.EventHandler(this.Linkbutton3_Click);
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			this.grdBonus.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.grdBonus_PageIndexChanged);
			this.grdBonus.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.grdBonus_SortCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void cboYear_SelectedIndexChanged(object sender, System.EventArgs e)
		{			
			LoadDataStaffLevel();
		}		
		//LOAD RATING OF STAFF LEVEL
		private void CheckOption()
		{
			if(chkSetup.Checked==true)
				trSetup.Style.Add("DISPLAY","block");
			else
				trSetup.Style.Add("DISPLAY","none");
		}
		private void LoadDataStaffLevel()
		{
			DataTable dtList = new DataTable();
			try
			{
				dtList = clsPRVariableBonus.GetDataStaffLevelByYear(cboYear.SelectedValue);
				grdStaffLevel.DataSource = dtList;
				grdStaffLevel.CurrentPageIndex = 0;
				grdStaffLevel.DataBind();				
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
			finally
			{
				dtList.Dispose();
			}
		}
		//SELECT ITEM OF STAFF LEVEL
		private void grdStaffLevel_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				if(e.CommandName.Trim().ToUpper() =="DETAILS")
				{
					for(int i=0;i<grdStaffLevel.Items.Count;i++)
						grdStaffLevel.Items[i].BackColor = System.Drawing.Color.White;
					e.Item.BackColor = System.Drawing.Color.LightSkyBlue;
					txtID.Value = e.Item.ItemIndex.ToString();
					LoadDataAppraisal();
				}	
				CheckOption();
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}
		//LOAD RESULT APPRAISAL
		private void LoadDataAppraisal()
		{
			DataTable dtList = new DataTable();
			try
			{
				dtList = clsPRVariableBonus.GetDataAppByYear(cboYear.SelectedValue,grdStaffLevel.Items[Convert.ToInt32(txtID.Value.Trim())].Cells[0].Text.Trim());
				grdAppraisal.DataSource = dtList;
				grdAppraisal.CurrentPageIndex = 0;
				grdAppraisal.DataBind();				
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
			finally
			{
				dtList.Dispose();
			}
		}
		// UPDATE CRITERIA BONUS
		private void btnUpdate_Click(object sender, System.EventArgs e)
		{
			string strStaffLevel = grdStaffLevel.Items[Convert.ToInt32(txtID.Value.Trim())].Cells[0].Text.Trim();
			string strMonths = ((TextBox)grdStaffLevel.Items[Convert.ToInt32(txtID.Value.Trim())].FindControl("txtNoOfMonth")).Text.Trim();
			lblErr.Text = clsPRVariableBonus.UpdateCriteria(cboYear.SelectedValue,strStaffLevel,strMonths,grdAppraisal);
			CheckOption();
		}
		//CALCULATE BONUS
		private void btnCalculate_Click(object sender, System.EventArgs e)
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
				dtResult = clsPRVariableBonus.CalculateBonus(strEmpID, strEmpName, strLevel1, strLevel2, strLevel3,strPosition, strJobCode, strLocation, strCompany,strStatus,cboYear.SelectedValue);
				grdBonus.DataSource = dtResult;
				grdBonus.CurrentPageIndex = 0;
				grdBonus.DataBind();
				//this.lblTotalRows.Text = dtResult.Rows.Count.ToString();				
			}
			catch (Exception ex)
			{
				lblErr.Text=ex.Message;
			}
			finally
			{
				CheckOption();
				if(dtResult != null)
					dtResult.Dispose();
			}
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			lblErr.Text = clsPRVariableBonus.SaveDataVariableBonus(cboYear.SelectedValue,grdBonus);
			CheckOption();
		}
		//DELETE CHECKED RECORD
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				string strID="";
				for(int i=0;i<grdBonus.Items.Count;i++)
				{
					if(((CheckBox)grdBonus.Items[i].FindControl("chkSelect")).Checked==true)
					{
						strID += grdBonus.Items[i].Cells[0].Text.Trim() + "$";
					}
				}
				clsCommon.DeleteListRecord("PR_spfrmVARIABLEBONUS","VariableBonusID",SqlDbType.Int,4,strID);
				btnSearch_Click(null,null);
				CheckOption();
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}

		private void btnSearch_Click(object sender, System.EventArgs e)
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
				dtResult = clsPRVariableBonus.SearchDataBonus(strEmpID, strEmpName, strLevel1, strLevel2, strLevel3,strPosition, strJobCode, strLocation, strCompany,strStatus,cboYear.SelectedValue);

				/*
				grdBonus.DataSource = dtResult;
				grdBonus.DataBind();
				this.lblTotalRows.Text = dtResult.Rows.Count.ToString();
				*/
				grdBonus.CurrentPageIndex = 0;

				//Hau
				this.grdBonus.PageSize = uctrlColumns.iPageRows;
				if(this.uctrlColumns.bAdvMultiSort)
					DataGridSort.AdvancedMultiSort(grdBonus, dtResult, uctrlColumns);
				else
					DataGridSort.Refresh(grdBonus, dtResult);
				this.uctrlColumns.iTotalRows = dtResult.Rows.Count;//
			}
			catch (Exception ex)
			{
				lblErr.Text=ex.Message;
			}
			finally
			{
				CheckOption();
				if(dtResult != null)
					dtResult.Dispose();
			}
		}

		private void btnExport_Click(object sender, System.EventArgs e)
		{
			iHRPCore.DataGridExcelExporter myExcelXport=new DataGridExcelExporter(grdBonus);
			myExcelXport.Export("");
			myExcelXport =null;
		}

		#region Cac su kien xu li thao tac Sort (Hau)

		private DataTable Read_Data()
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

			DataTable dtb = new DataTable();
			dtb = clsPRVariableBonus.SearchDataBonus(strEmpID, strEmpName, strLevel1, strLevel2, strLevel3,strPosition, strJobCode, strLocation, strCompany,strStatus,cboYear.SelectedValue);
			return dtb;
		}
		
		private void grdBonus_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			try
			{
				DataGridSort.Grid_IndexChanged(grdBonus, Read_Data(), e);
			}
			catch{}
		}

		private void grdBonus_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			try
			{
				DataGridSort.Grid_Sort(grdBonus, Read_Data(), e, uctrlColumns);
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
				DataGridSort.AdvancedMultiSort(grdBonus, Read_Data(), uctrlColumns);
			}
			catch{}
		}

		private void ButtonPRChanged_ServerClick(object sender, EventArgs e)
		{
			try
			{
				grdBonus.PageSize = uctrlColumns.iPageRows;
				grdBonus.CurrentPageIndex = 0;
				DataView dv = new DataView(Read_Data());
				dv.Sort = DataGridSort.sMulSort;
				grdBonus.DataSource = dv;
				grdBonus.DataBind();
			}
			catch{}
		}
		#endregion

		private void Linkbutton3_Click(object sender, System.EventArgs e)
		{
		
		}
	}
}
