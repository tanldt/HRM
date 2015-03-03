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
	///		Summary description for SalaryAdjustList.
	/// </summary>
	public class SalaryAdjustList : System.Web.UI.UserControl
	{
		protected string strLanguage = "VN";
		protected EmpHeaderSearch EmpHeaderSearch1;
		protected System.Web.UI.WebControls.Label lblErr;
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
		protected System.Web.UI.WebControls.DataGrid grdSalaryAdjust;
		protected System.Web.UI.WebControls.RadioButtonList optAddSubtract;
		protected System.Web.UI.WebControls.DropDownList cboLSSalaryAdjustCode;
		protected System.Web.UI.WebControls.TextBox txtPRMonth;
		protected System.Web.UI.WebControls.HyperLink HyperLink1;
		protected ColumnList uctrlColumns;

		private void Page_Load(object sender, System.EventArgs e)
		{			
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"VN";			
			if(!Page.IsPostBack)
			{
				LoadDataCombo();
				grdSalaryAdjust.DataSource = new DataTable();				
				grdSalaryAdjust.DataBind();

				//Hau
				DataGridSort.AddItemColumn(uctrlColumns, grdSalaryAdjust);//
			}
			btnDelete.Attributes.Add("OnClick", "return checkdelete()");

			//Hau
			ButtonClick();//
		}
		/// <summary>
		/// LOAD DATA TO COMBO
		/// </summary>
		private void LoadDataCombo()
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			clsCommon.LoadDropDownListControl(cboLSSalaryAdjustCode,"sp_GetDataCombo @TableName='LS_tblSalaryAdjust',@Fields='LSSalaryAdjustID as [ID]," + strTextField + " as Name'","ID","Name",true);
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
            this.grdSalaryAdjust.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.grdSalaryAdjust_ItemCommand);
            this.grdSalaryAdjust.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.grdSalaryAdjust_PageIndexChanged);
            this.grdSalaryAdjust.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.grdSalaryAdjust_SortCommand);
            this.Load += new System.EventHandler(this.Page_Load);

        }
		#endregion
		/// <summary>
		/// BIND DATA TO GRID
		/// </summary>
		private void BindDataGrid()
		{
			DataTable dtResult = new DataTable();
			try
			{
				//Lay gia tri cua HeaderEmpSearch
				string strEmpID = EmpHeaderSearch1.txtEmpID.Text.Trim().Replace("'","");;
				string strEmpName = EmpHeaderSearch1.txtEmpName.Text.Trim().Replace("'","");;
				string strLevel1 = EmpHeaderSearch1.cboLevel1.SelectedValue.Trim();
				string strLevel2 = EmpHeaderSearch1.cboLevel2.SelectedValue.Trim();
				string strLevel3 = EmpHeaderSearch1.cboLevel3.SelectedValue.Trim();
				string strLocation = EmpHeaderSearch1.cboLocation.SelectedValue.Trim();
				string strJobCode = EmpHeaderSearch1.cboJobcode.SelectedValue.Trim();
				string strPosition = EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue.Trim();
				string strCompany = EmpHeaderSearch1.cboCompany.SelectedValue.Trim();
				string strStatus = EmpHeaderSearch1.optStatus.SelectedValue.Trim();
				string strLanguageID=Session["LangID"].ToString();
				dtResult = clsPRSalaryAdjust.GetListSearch(strEmpID, strEmpName, strLevel1, strLevel2, strLevel3,strPosition, strJobCode, strLocation, strCompany, strStatus,cboLSSalaryAdjustCode.SelectedValue.Trim()
					,optAddSubtract.SelectedValue.Trim(),txtPRMonth.Text.Trim(),strLanguageID, this.Page);
				//grdSalaryAdjust.DataSource = dtResult;
				//grdSalaryAdjust.DataBind();

				//Hau
				this.grdSalaryAdjust.PageSize = uctrlColumns.iPageRows;
				if(this.uctrlColumns.bAdvMultiSort)
				{
					try
					{
						DataGridSort.AdvancedMultiSort(grdSalaryAdjust, dtResult, uctrlColumns);
					}
					catch
					{
						grdSalaryAdjust.CurrentPageIndex = 0;
						DataGridSort.AdvancedMultiSort(grdSalaryAdjust, dtResult, uctrlColumns);
					}
				}
				else
				{
					try
					{
						DataGridSort.Refresh(grdSalaryAdjust, dtResult);
					}
					catch{
						grdSalaryAdjust.CurrentPageIndex = 0;
						DataGridSort.Refresh(grdSalaryAdjust, dtResult);
					}
				}
				this.uctrlColumns.iTotalRows = dtResult.Rows.Count;
				//
			}
			catch (Exception ex)
			{
				//lblErr.Text=ex.Message;
				clsChangeLang.popupWindowExp(this.Parent,ex);
			}
			finally
			{
				if(dtResult != null)
					dtResult.Dispose();
			}
		}
/// <summary>
/// SEARCH SALARY ADJUST
/// </summary>
		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			BindDataGrid();
		}
/// <summary>
/// DELETE CHECKED RECORD
/// </summary>
		private void btnDelete_Click(object sender, System.EventArgs e)
		{   bool a;
			try
			{
				string strID="";
				for(int i=0;i<grdSalaryAdjust.Items.Count;i++)
				{
					if(((CheckBox)grdSalaryAdjust.Items[i].FindControl("chkSelect")).Checked==true)
					{
						strID += grdSalaryAdjust.Items[i].Cells[0].Text.Trim() + "$";
					}
				}
				a=clsCommon.DeleteListRecord("PR_spfrmSALARYADJUST","SalaryAdjustID",SqlDbType.NVarChar,12,strID);
				BindDataGrid();
                if(a==true)
                {
                    clsChangeLang.popupWindow(this.Parent,"0104","EN","",1);				        
                }
                else
                {
                    clsChangeLang.popupWindow(this.Parent,"0048","EN","",0);
                }
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}
/// <summary>
/// PAGE INDEX CHANGE
/// </summary>
		private void grdSalaryAdjust_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			/*
			try
			{
				BindDataGrid();
				grdSalaryAdjust.CurrentPageIndex=e.NewPageIndex;
				grdSalaryAdjust.DataBind();
			}
			catch(Exception ex)
			{
				string str = ex.Message;
			}
			*/

			//Hau
			try
			{
				DataGridSort.Grid_IndexChanged(grdSalaryAdjust, Read_Data(), e);
			}
			catch{}//
		}
/// <summary>
/// EXPORT DATA ON GRID
/// </summary>
		private void btnExport_Click(object sender, System.EventArgs e)
		{
			iHRPCore.DataGridExcelExporter myExcelXport=new DataGridExcelExporter(grdSalaryAdjust);
			myExcelXport.Export("");
			myExcelXport =null;
		}

/// <summary>
/// SELECT ITEM TO EDIT
/// </summary>
		private void grdSalaryAdjust_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if(e.CommandName.Trim().ToUpper() =="EDIT")
			{
				Session["ssSalaryAdjustID"] = grdSalaryAdjust.Items[e.Item.ItemIndex].Cells[0].Text.Trim();
				Session["EmpID"] = grdSalaryAdjust.Items[e.Item.ItemIndex].Cells[3].Text.Trim();
				Response.Redirect("Editpage.aspx?ModuleID=PR&ParentID=68&FunctionID=104&Ascx=MdlPR/SalaryAdjust.ascx");
			}
		}

		#region Cac su kien xu li thao tac Sort (Hau)
		
		private void grdSalaryAdjust_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			try
			{
				DataGridSort.Grid_Sort(grdSalaryAdjust, Read_Data(), e, uctrlColumns);
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
			string strLanguageID =Session["LangID"].ToString();
			DataTable dtResult = new DataTable();
			dtResult = clsPRSalaryAdjust.GetListSearch(strEmpID, strEmpName, strLevel1, strLevel2, strLevel3,strPosition, strJobCode, strLocation, strCompany, strStatus,cboLSSalaryAdjustCode.SelectedValue.Trim()
				,optAddSubtract.SelectedValue.Trim(),txtPRMonth.Text.Trim(),strLanguageID, this.Page);
			return dtResult;
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
				DataGridSort.AdvancedMultiSort(grdSalaryAdjust, Read_Data(), uctrlColumns);
			}
			catch{}
		}

		private void ButtonPRChanged_ServerClick(object sender, EventArgs e)
		{
			try
			{
				grdSalaryAdjust.PageSize = uctrlColumns.iPageRows;
				grdSalaryAdjust.CurrentPageIndex = 0;
				DataView dv = new DataView(Read_Data());
				dv.Sort = DataGridSort.sMulSort;
				grdSalaryAdjust.DataSource = dv;
				grdSalaryAdjust.DataBind();
			}
			catch{}
		}
		#endregion
	}
}
