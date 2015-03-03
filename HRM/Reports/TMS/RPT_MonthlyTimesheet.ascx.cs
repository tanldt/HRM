namespace MdlTMS
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.Com;
	using iHRPCore.TMSComponent;
	using iHRPCore.Include;
	using GridSort;
	using iHRPCore.HRComponent; //123
	using System.Data.SqlClient;
	using System.Configuration;
	using iHRPCore.SendMail;	
	using iHRPCore;	
	using FPTToolWeb.Control.DataGrids;

	/// <summary>
	///		Summary description for CollectTimesheet.
	/// </summary>
	public class RPT_MonthlyTimesheet : System.Web.UI.UserControl
	{
		#region Declaration
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.TextBox txtMonthYear;
		protected System.Web.UI.WebControls.LinkButton btnSearch;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.LinkButton btnCancel;
		protected System.Web.UI.WebControls.TextBox txtTypeDate;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.LinkButton btnImport;
		protected System.Web.UI.WebControls.DataGrid dtgList;
		protected System.Web.UI.WebControls.TextBox Textbox1;
		protected System.Web.UI.WebControls.TextBox txtTimeIn;
		protected System.Web.UI.WebControls.TextBox txtTimeOut;
		protected System.Web.UI.WebControls.TextBox txtActualOT;
		protected System.Web.UI.WebControls.TextBox txtClaimOT;
		protected System.Web.UI.WebControls.TextBox Textbox2;
		protected System.Web.UI.WebControls.CheckBox chkSelect;
		public string strLanguage="VN";
		protected EmpHeaderSearch EmpHeaderSearch1;
		protected System.Web.UI.WebControls.RadioButtonList RadStatus;
		protected ColumnList uctrlColumns;

		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				LoadDefaultData();				
			}
			ButtonClick();
			this.btnSave.Attributes.Add("OnClick", "return CheckSave()");
			this.btnSearch.Attributes.Add("OnClick", "return CheckSearch()");
			this.btnExport.Attributes.Add("OnClick", "return CheckSearch()");			
			this.btnDelete.Attributes.Add("OnClick", "return CheckDelete()");
			this.btnExport.Attributes.Add("OnClick", "return CheckExport()");
			setCurrentMonth();			
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
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            this.btnImport.Click += new System.EventHandler(this.btnSubmit_Click);
            this.dtgList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgList_PageIndexChanged);
            this.Load += new System.EventHandler(this.Page_Load);

        }
		#endregion
		private void LoadDefaultData()
		{
			//this.txtMonthYear.Text = DateTime.Today.ToString("MM/yyyy");
			DataTable dt = new DataTable();
			dtgList.DataSource = dt;
			dtgList.DataBind();
		}
        private void setCurrentMonth()
        {
                if(this.txtMonthYear.Text=="")
            {    
                DateTime dt=DateTime.Now; 
                string strMonth;
                if (dt.Month < 10) strMonth = "0" + dt.Month;
                else strMonth = dt.Month + "";
                string curr = strMonth + "/" + dt.Year;
                this.txtMonthYear.Text=curr;
            }
        }
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			string sErr = clsMonthlyTimesheet.sImpact(dtgList, txtMonthYear.Text);
			if (sErr=="")
			{
				clsChangeLang.popupWindow(this.Parent,"0044",strLanguage,"",1);				
				if (RadStatus.SelectedValue == "0")
					btnDelete.Enabled = false;
				else
					btnDelete.Enabled = true;
				BindDataGrid(true);
			}
			else
			{
				clsChangeLang.popupWindowCataLog(this.Parent,sErr);
			}
		}		

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			if (RadStatus.SelectedValue == "0")
				btnDelete.Enabled = false;
			else
				btnDelete.Enabled = true;	
			BindDataGrid(true);			
		}
		
		private void BindDataGrid(bool Status)
		{	
			this.dtgList.DataSource = null;
			try
			{
				DataTable dt = new DataTable();
				if (Status)
				{
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
					string sMonth = txtMonthYear.Text;
					string sTMS = RadStatus.SelectedValue;

					dt = clsMonthlyTimesheet.getCounTimeList(sMonth, sTMS, strEmpID, strEmpName, strCompany, strLevel1, strLevel2, strLevel3,strPosition, strLocation, strStatus, "",sEmpTypeID, this.Page);
					Mession.GlbSearchData = dt;
					this.dtgList.CurrentPageIndex = 0;		
				}
				else
					dt = Mession.GlbSearchData;
				
				dtgList.DataSource = dt;
				dtgList.DataBind();
			}
			catch(Exception exp)
			{
				this.lblErr.Text = exp.Message.ToString();
			}		
		}

		private void dtgList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			if (RadStatus.SelectedValue == "0")
				btnDelete.Enabled = false;
			else
				btnDelete.Enabled = true;
	
			this.dtgList.CurrentPageIndex = e.NewPageIndex;
			BindDataGrid(false);
		}

		private void btnSubmit_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(Request.ApplicationPath.Trim() + "/Editpage.aspx?ModuleID=TMS&ParentID=502&Ascx=MdlImports/Imports.ascx&tabid=10&FunctionID=504");
		}

		private void GetParamOT(string strMonth)
		{
			if (Session["EmpID"] == null)
				return;

			//DataRow drData = clsCommon.GetDataRow("TS_spfrmPARAMETER @Activity='getParamOT',@EmpID='" + txtEmpID.Text + "', @MMYYYY='" + strMonth + "'");
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			string strSql = "";
			string strMsg = "";
			string sMonthYear = txtMonthYear.Text;	
			for(int i=0;i<dtgList.Items.Count;i++)
			{
				CheckBox chk = (CheckBox)this.dtgList.Items[i].FindControl("chkSelect");
				if(chk.Checked)
				{
					string sEmpID = dtgList.Items[i].Cells[0].Text.Trim(); 					

					strSql = "TS_spfrmCountTime_V1 @Activity='Delete', @EmpID='" + sEmpID + "', @Month = '" + sMonthYear + "'";	
					strMsg = clsDB.Exc_CommandText_(strSql);					
					if (strMsg != "")
						break;
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
        public DataTable getTimesheetRPT(string sMonthYear, string sTMS, string sEmpCode, string sEmpName, string sLSCompanyID,
            string sLSLevel1ID, string sLSLevel2ID, string sLSLevel3ID, string sLSPositionID, string sLSLocationID,string strStatus,string strLanguage, string strLSEmpTypeID,System.Web.UI.Page pPage)
        {
            string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
            DataTable dtData= new DataTable();
            try
            {
                string sSQL="TS_spfrmCountTime_V1 @Activity='GetListReport',@isTMS='" + sTMS + "', @EmpCode=N'" + sEmpCode + "',@EmpName=N'" + sEmpName + "',@Month='" + sMonthYear + "', @LSCompanyID=N'" + sLSCompanyID 
                    + "',@LSLevel1ID=N'" + sLSLevel1ID + "',@LSLevel2ID=N'" + sLSLevel2ID + "',@LSLevel3ID=N'" + sLSLevel3ID 
                    + "',@LSLocationID=N'" + sLSLocationID + "', @LSPositionID=N'" + sLSPositionID + "',@UserGroupID=N'" + sAccountLogin + "',@LanguageID='" + strLanguage + "',@LSEmpTypeID='" +strLSEmpTypeID+"'";
                if (strStatus != "")
                    sSQL = sSQL + ", @Status='" + strStatus + "'";
                dtData= clsCommon.GetDataTable(sSQL);				
                return dtData;
            }
            catch
            {
                return null;
            }
            finally
            {
                dtData.Dispose();
            }
        }
        
        private DataTable getMonthlyTimesheetRPT()
        {   DataTable dt = new DataTable();
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
            string sMonth = txtMonthYear.Text;
            string sTMS = RadStatus.SelectedValue;
            dt = clsMonthlyTimesheet.getCounTimeList(sMonth, sTMS, strEmpID, strEmpName, strCompany, strLevel1, strLevel2, strLevel3,strPosition, strLocation, strStatus, "",sEmpTypeID, this.Page);
            return dt;
        }

        private void btnExport_Click(object sender, System.EventArgs e)
        {
        
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
            string sMonth = txtMonthYear.Text;
            string sTMS = RadStatus.SelectedValue;
            DataTable dt = new DataTable();
            dt = getTimesheetRPT(sMonth, sTMS, strEmpID, strEmpName, strCompany, strLevel1, strLevel2, strLevel3,strPosition, strLocation, strStatus, "",sEmpTypeID, this.Page);        
          
            try
            {
                string strHeaderParams = "";
                string strHeaderValues = "";
                string strFooterParams = "";
                string strFooterValues = "";
                //Phan khai bao se dung Tool bao cao Excel
                #region Header 
                //DataTable dtSI_ComInfo = clsCommon.GetDataTable("PR_spfrmPAYROLL @Activity = 'GetExchangeRate', @MMYYYY = '" + txtMonth.Text + "'");               
                string compname = EmpHeaderSearch1.cboLevel1.SelectedItem.ToString();
				   
                    strHeaderParams = "CompanyName;MonthYear";
                    strHeaderValues = compname+";"+ txtMonthYear.Text;
                
                #endregion
                #region Footer
                strFooterParams = "PrintDate";
                string strDate = DateTime.Now.ToShortDateString();
                if (strDate == "")
                    strDate = "..../..../20.....";
                strFooterValues = strDate;
                //strFooterValues = strFooterValues.Replace("/",";");
                #endregion
                iHRPCore.Reports.v11.clsBaocaoExcel bc = new iHRPCore.Reports.v11.clsBaocaoExcel();
                #region Config System
                /*
                 * itempheadlv1: id cho Header Group 1
                 * itempheadlv2: id cho Header Group 2
                 * itempsumlv1: id sum cap 1
                 * itempsumlv2: id sum cap 2
                 * itempsumtotal: id sum tong cong tat ca
                 * */
                #endregion
                #region Config Basic
                bc.IsGroupLv1 = true; //Co Group 1 khong?
                bc.GroupLv1 = 1; //Chỉ định Group level 1 ở cột thứ I
                bc.IsGroupLv2 = false; //Co Group 2 khong?
                bc.GroupLv2 = 2; //Chỉ định Group level 2 ở cột thứ II
                bc.IsSum = false; //Có Sum tong hay không? Nếu có thì phải có id template (itempsumtotal) đi kèm
                bc.IsSum1 = false; //Có Sum cap 1 hay không? Nếu có thì phải có id template (itempsumlv1) đi kèm
                bc.IsSum2 = false; //Có Sum cap 2 hay không? Nếu có thì phải có id template (itempsumlv2) đi kèm				
                bc.sfileTemplate = "TS/RPT_MonthlyTimesheet.htm";
                bc.sHeaderParams = strHeaderParams;
                bc.sHeaderValues = strHeaderValues;
                bc.sFooterParams = strFooterParams;
                bc.sFooterValues = strFooterValues;
                string strReports = bc.strReportBasic(dt);
                #endregion
                //End

                ExcelExporter myExcelXport=new ExcelExporter(this.Page);
                myExcelXport.ExportHTMLToExcel(strReports,"Excel");
                myExcelXport = null;
            }
            catch(Exception ex)
            {
                lblErr.Text = ex.Message;
            }
            finally
            {
                dt.Dispose();
            }
        }		
	
        #region Cac su kien xu li thao tac Sort (Hau)
        private void ButtonClick()
        {
            this.uctrlColumns.ButtonSort.ServerClick += new EventHandler(ButtonSort_ServerClick);
            this.uctrlColumns.ButtonPRChanged.ServerClick += new EventHandler(ButtonPRChanged_ServerClick);
        }
        private void ButtonSort_ServerClick(object sender, EventArgs e)
        {
            try
            {   
                DataGridSort.AdvancedMultiSort(dtgList, getMonthlyTimesheetRPT(), uctrlColumns);				
            }
            catch{}
        }
        private void ButtonPRChanged_ServerClick(object sender, EventArgs e)
        {
            try
            {
                dtgList.PageSize = uctrlColumns.iPageRows;
                dtgList.CurrentPageIndex = 0;
                DataView dv = new DataView(getMonthlyTimesheetRPT());
                dv.Sort = DataGridSort.sMulSort;
                dtgList.DataSource = dv;
                dtgList.DataBind();
            }
            catch{}
			
        }
        private void dtgList_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
        {
            try
            {
                DataGridSort.Grid_Sort(dtgList, getMonthlyTimesheetRPT(), e, uctrlColumns);			}
            catch{}
        }

        #endregion
	
	
	}
}

