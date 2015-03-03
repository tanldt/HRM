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
	public class ManageHouseList : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.LinkButton btnSearch;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label15;		
		protected string strLanguage = "EN";
		protected System.Web.UI.WebControls.DataGrid grdData;
		protected EmpHeaderSearch EmpHeaderSearch1;
		protected System.Web.UI.WebControls.DropDownList cboCol1;
		protected System.Web.UI.WebControls.DropDownList cboCol2;
		protected System.Web.UI.WebControls.DropDownList cboCol3;
		protected System.Web.UI.WebControls.HyperLink HyperLink1;
        protected System.Web.UI.WebControls.Label lblToDate;
        protected System.Web.UI.WebControls.TextBox txtStrListID;
        protected System.Web.UI.WebControls.Label lblFromMonth;
        protected System.Web.UI.WebControls.TextBox txtFromMonth;
        protected System.Web.UI.WebControls.TextBox txtToMonth;
		protected ColumnList uctrlColumns;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
			if(!Page.IsPostBack)
			{
				strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
				string strTextField = strLanguage == "VN"?"VNName":"Name";
				//clsCommon.LoadDropDownListControl(cboUnfrequentIncomeTypeID,"sp_GetDataCombo @TableName='LS_tblUnfrequentIncomeType',@Fields='UnfrequentIncomeTypeID as [ID]," + strTextField + " as Name'","ID","Name",true);
				grdData.DataSource = new DataTable();
				grdData.DataBind();

				//Hau
				DataGridSort.AddItemColumn(uctrlColumns, grdData);//
			}
			btnDelete.Attributes.Add("OnClick", "return checkdelete()");
			btnSearch.Attributes.Add("OnClick", "return goSearch()");
			
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
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            this.grdData.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.grdData_ItemCommand_1);
            this.grdData.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.grdData_PageIndexChanged);
            this.grdData.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.grdData_SortCommand_1);
            this.Load += new System.EventHandler(this.Page_Load);

        }
		#endregion

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			LoadDataResult();
		}
		
        public static DataTable GetListSearch(string strEmpID, string strEmpName,string strCompanyID ,string strLevel1, string strLevel2, 
                      string strLevel3,string strStatus, string strFromMonth, string strToMonth,string strLanguage,System.Web.UI.Page pPage)
        
        {            
            string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
            //--------------------------------------------------
            DataTable dt = new DataTable();
            try
            {
                string strSql = "PR_spfrmManageHouse @Activity='GetDataListSearch',@EmpID=N'" + strEmpID + "', @EmpName=N'" + strEmpName
                    + "',@LSCompanyID='" + strCompanyID + "', @LSLevel1ID='" + strLevel1 + "', @LSLevel2ID='" + strLevel2 + "',@LSLevel3ID='" + strLevel3
                    + "',@FromMonth=N'" + strFromMonth.Trim() + "',@ToMonth=N'" + strToMonth + "',@UserGroupID='" + sAccountLogin + "',@LanguageID='" + strLanguage + "'";
                if (strStatus != "")
                    strSql = strSql + ", @Status='" + strStatus + "'";
                dt = clsCommon.GetDataTable(strSql);
                return dt;
            }
            catch
            {
                return null;
            }
            finally
            {
                dt.Dispose();
            }
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
				string strCompanyID=EmpHeaderSearch1.cboCompany.SelectedValue.Trim();
				dt =GetListSearch(strEmpID, strEmpName,strCompanyID, strLevel1, strLevel2, strLevel3,strStatus,txtFromMonth.Text,txtToMonth.Text,strLanguage, this.Page);
//				grdData.DataSource = dt;
//				grdData.DataBind();

				#region Hau

				this.grdData.PageSize = uctrlColumns.iPageRows;
				if(this.uctrlColumns.bAdvMultiSort)
				{
					try
					{
						DataGridSort.AdvancedMultiSort(grdData, dt, uctrlColumns);
					}
					catch
					{
						grdData.CurrentPageIndex = 0;
						DataGridSort.AdvancedMultiSort(grdData, dt, uctrlColumns);
					}
				}
				else
				{
					try
					{
						DataGridSort.Refresh(grdData, dt);
					}
					catch
					{
						//grdData.CurrentPageIndex = 0;
						//DataGridSort.Refresh(grdData, dt);
					}
				}
				this.uctrlColumns.iTotalRows = dt.Rows.Count;

				#endregion

				lblErr.Text = "";
			}
			catch (Exception ex)
			{
				clsChangeLang.popupWindowExp(this.Parent,ex);
			}
			finally
			{
				dt.Dispose();
			}
		}
		private void grdData_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			
			try
			{
				LoadDataResult();
				grdData.CurrentPageIndex=e.NewPageIndex;
				grdData.DataBind();
			}
			catch(Exception ex)			
			{
				string str = ex.Message;
			}		

			//Hau
			try
			{
				DataGridSort.Grid_IndexChanged(grdData, Read_Data(), e);
			}
			catch{}//
		}
		/// <summary>
		/// delete checked record
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
            bool a;
            try
            {   
                string strID="@";
                for(int i=0;i<grdData.Items.Count;i++)
                {
                    if(((CheckBox)grdData.Items[i].FindControl("chkSelect")).Checked==true)
                    {
                        strID += grdData.Items[i].Cells[0].Text.Trim() + "@";
                    }
                }
                txtStrListID.Text=strID;
                a=clsCommon.ImpactDB("@UserGroupID",Session["AccountLogin"],Session["EmpID"].ToString().Trim(),"DeleteList",this,"PR_spfrmUNFREQUENTINCOME");
                
                if(a==true)
                {
                    clsChangeLang.popupWindow(this.Parent,"0104","EN","",1);
                    LoadDataResult();
                }
                else
                {//update
                    clsChangeLang.popupWindow(this.Parent,"0048","EN","",0);
                }
            }
            catch(Exception ex)
            {
                lblErr.Text = ex.Message;
            }      
		}

		private void btnExport_Click(object sender, System.EventArgs e)
		{
			iHRPCore.DataGridExcelExporter myExcelXport=new DataGridExcelExporter(grdData);			
			myExcelXport.Export("");
			myExcelXport =null;
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
			string strStatus = EmpHeaderSearch1.optStatus.SelectedValue.Trim();
			string strCompanyID =EmpHeaderSearch1.cboCompany.SelectedValue.Trim();
			DataTable dtb = new DataTable();
			dtb = GetListSearch(strEmpID, strEmpName,strCompanyID, strLevel1, strLevel2, strLevel3,strStatus,txtFromMonth.Text.Trim(),txtToMonth.Text.Trim(),strLanguage, this.Page);
			return dtb;
		}

		private void grdData_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			try
			{
				DataGridSort.Grid_Sort(grdData, Read_Data(), e, uctrlColumns);
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
				DataGridSort.AdvancedMultiSort(grdData, Read_Data(), uctrlColumns);
			}
			catch{}
		}

		private void ButtonPRChanged_ServerClick(object sender, EventArgs e)
		{
			try
			{
				grdData.PageSize = uctrlColumns.iPageRows;
				grdData.CurrentPageIndex = 0;
				DataView dv = new DataView(Read_Data());
				dv.Sort = DataGridSort.sMulSort;
				grdData.DataSource = dv;
				grdData.DataBind();
			}
			catch{}
		}
		private void grdData_SortCommand_1(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
        {
            try
            {
                DataGridSort.Grid_Sort(grdData, Read_Data(), e, uctrlColumns);
            }
            catch{}
        }
		#endregion
		

        private void grdData_ItemCommand_1(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            if(e.CommandName.Trim().ToUpper() =="EDIT")
            {
                Session["ssManageHouseID"] = grdData.Items[e.Item.ItemIndex].Cells[0].Text.Trim();
                Session["EmpID"] = grdData.Items[e.Item.ItemIndex].Cells[1].Text.Trim();
                Session["ssStatusBonus"]="Edit";
                Response.Redirect("Editpage.aspx?ModuleID=PR&ParentID=68&FunctionID=5000020&Ascx=MdlPR/ManageHouse.ascx");
            }
        }

        
	}
}
