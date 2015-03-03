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
	///		Summary description for AllowanceList.
	/// </summary>
	public class AllowanceList : System.Web.UI.UserControl
	{
		protected string strLanguage = "EN";
		protected EmpHeaderSearch EmpHeaderSearch1;
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.CheckBox chkShowGrid;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.LinkButton Linkbutton2;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label lblStartDate;
		protected System.Web.UI.WebControls.Label Label14;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.DropDownList cboCol1;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.DropDownList cboCol2;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.DropDownList cboCol3;
		protected System.Web.UI.WebControls.LinkButton btnSearch;
		protected System.Web.UI.WebControls.DropDownList cboAllowanceType;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.DataGrid grdAllowance;
		protected System.Web.UI.WebControls.TextBox txtToDate;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtFromDate;
		protected System.Web.UI.HtmlControls.HtmlTable tblSort;
		protected ColumnList uctrlColumns;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";			
			if(!Page.IsPostBack)
			{
				LoadDataCombo();
				grdAllowance.DataSource = new DataTable();				
				grdAllowance.DataBind();

				//Hau
				DataGridSort.AddItemColumn(uctrlColumns, grdAllowance);//
			}
			btnDelete.Attributes.Add("OnClick", "return checkdelete()");
			btnSearch.Attributes.Add("OnClick", "return Search()");

			//Hau
			ButtonClick();//
		}
		private void LoadDataCombo()
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			clsCommon.LoadDropDownListControl(cboAllowanceType,"sp_GetDataCombo @TableName='LS_tblAllowance',@Fields='LSAllowanceID as [ID]," + strTextField + " as Name'","ID","Name",true);			
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
			this.grdAllowance.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.grdAllowance_ItemCommand);
			this.grdAllowance.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.grdAllowance_PageIndexChanged);
			this.grdAllowance.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.grdAllowance_SortCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
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
				
				dtResult = clsPRAllowance.GetListSearch(strEmpID, strEmpName, strLevel1, strLevel2, strLevel3,strPosition, strJobCode, strLocation, strCompany, strStatus,cboAllowanceType.SelectedValue.Trim()
					,txtFromDate.Text.Trim(),txtToDate.Text.Trim(),strLanguageID,this.Page);
				//grdAllowance.DataSource = dtResult;
				//grdAllowance.DataBind();

				//Hau		

					
				
				this.grdAllowance.PageSize = uctrlColumns.iPageRows;
				
				if(this.uctrlColumns.bAdvMultiSort)
				{
					try
					{
						DataGridSort.AdvancedMultiSort(grdAllowance, dtResult, uctrlColumns);
						
					}
					catch
					{
						grdAllowance.CurrentPageIndex = 0;
						DataGridSort.AdvancedMultiSort(grdAllowance, dtResult, uctrlColumns);
					
					}
				}
				else
				{
					try
					{
						DataGridSort.Refresh(grdAllowance, dtResult);
					
					}
					catch
					{
						grdAllowance.CurrentPageIndex = 0;
						DataGridSort.Refresh(grdAllowance, dtResult);
						
					}
				}
				this.uctrlColumns.iTotalRows = dtResult.Rows.Count;
				
				//
			}
			catch (Exception ex)
			{
				//lblErr.Text=ex.Message;
			}
			finally
			{
				if(dtResult != null)
					dtResult.Dispose();
			}
		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			BindDataGrid();
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				string strID="";
				for(int i=0;i<grdAllowance.Items.Count;i++)
				{
					if(((CheckBox)grdAllowance.Items[i].FindControl("chkSelect")).Checked==true)
					{
						strID += grdAllowance.Items[i].Cells[0].Text.Trim() + "$";
					}
				}
				clsCommon.DeleteListRecord("PR_spfrmALLOWANCE","AllowanceID",SqlDbType.NVarChar,12,strID);
				BindDataGrid();
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}		

		private void grdAllowance_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			
			try
			{
				BindDataGrid();
				grdAllowance.CurrentPageIndex=e.NewPageIndex;
				grdAllowance.DataBind();
			}
			catch(Exception ex)
			{
				string str = ex.Message;
			}
			
			//Hau
			try
			{
				DataGridSort.Grid_IndexChanged(grdAllowance, Read_Data(), e);
			}
			catch{}//
		}

		private void btnExport_Click(object sender, System.EventArgs e)
		{
			iHRPCore.DataGridExcelExporter myExcelXport=new DataGridExcelExporter(grdAllowance);			
			myExcelXport.Export("");
			myExcelXport =null;
		}

		private void grdAllowance_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if(e.CommandName.Trim().ToUpper() =="EDIT")
			{
				Session["ssAllowanceID"] = grdAllowance.Items[e.Item.ItemIndex].Cells[0].Text.Trim();
				Session["EmpID"] = grdAllowance.Items[e.Item.ItemIndex].Cells[3].Text.Trim();
				Response.Redirect("Editpage.aspx?ModuleID=PR&ParentID=68&FunctionID=83&Ascx=MdlPR/Allowance.ascx");
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
			dtResult = clsPRAllowance.GetListSearch(strEmpID, strEmpName, strLevel1, strLevel2, strLevel3,strPosition, strJobCode, strLocation, strCompany, strStatus,cboAllowanceType.SelectedValue.Trim()
				,txtFromDate.Text.Trim(),txtToDate.Text.Trim(),strLanguageID, this.Page);

			return dtResult;
		}

		#endregion

		private void grdAllowance_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			try
			{
				DataGridSort.Grid_Sort(grdAllowance, Read_Data(), e, uctrlColumns);
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
				DataGridSort.AdvancedMultiSort(grdAllowance, Read_Data(), uctrlColumns);
			}
			catch{}
		}

		private void ButtonPRChanged_ServerClick(object sender, EventArgs e)
		{
			try
			{
				grdAllowance.PageSize = uctrlColumns.iPageRows;
				grdAllowance.CurrentPageIndex = 0;
				DataView dv = new DataView(Read_Data());
				dv.Sort = DataGridSort.sMulSort;
				grdAllowance.DataSource = dv;
				grdAllowance.DataBind();
			}
			catch{}
		}
	}
}
