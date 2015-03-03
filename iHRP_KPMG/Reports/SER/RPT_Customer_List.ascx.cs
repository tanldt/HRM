namespace MdlSER
{
	using System;
	using System.IO;
	using System.Configuration;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.Com;
	using iHRPCore.HRComponent;
	using iHRPCore.TMSComponent; 
	using System.Data.SqlClient;
	using iHRPCore.Include;
	using iHRPCore.SerComponent;
	using GridSort;	
	using iHRPCore.Com;
	using iHRPCore.Include;
	using iHRPCore.PRComponent;
	using GridSort;
	using FPTToolWeb.Control.DataGrids;

	/// <summary>
	///		Summary description for Company.
	/// </summary>
	public class RPT_Customer_List : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.DataGrid dtgList;
		protected System.Web.UI.WebControls.LinkButton btnSearch;
		protected System.Web.UI.WebControls.LinkButton btnAddnew;
		protected System.Web.UI.WebControls.DropDownList cboCustomerTypeID;
		protected System.Web.UI.WebControls.TextBox txtShortName;
		protected System.Web.UI.WebControls.TextBox txtName;
		protected System.Web.UI.WebControls.Label lblCustomerGroup;
		protected System.Web.UI.WebControls.Label lblCustomertype;
		protected System.Web.UI.WebControls.Label lblCustomerCode;
		protected System.Web.UI.WebControls.Label lblShortName;
		protected System.Web.UI.WebControls.Label lblName;
		protected System.Web.UI.WebControls.Label lblNameVN;
		protected ColumnList uctrlColumns;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.DropDownList cboLSComapyID;
		protected System.Web.UI.WebControls.TextBox txtLSLevel1Code;
		protected System.Web.UI.WebControls.TextBox txtVNName;
		protected System.Web.UI.WebControls.TextBox txtStrLSLevel1ID;
		public string strLanguage = "EN";
		protected System.Web.UI.WebControls.LinkButton btnExport;
		public string strUserGroupID="";

		private void Page_Load(object sender, System.EventArgs e)
		{			
			Session["ssStatusWorking"] = "AddNew";
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
			strUserGroupID=Session["AccountLogin"].ToString();
			if(!Page.IsPostBack)
			{	
				BindData();
			}
			LoadDataCombo();
			ButtonClick();
			btnDelete.Attributes["OnClick"]="return CheckDelete();";
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
            this.cboLSComapyID.SelectedIndexChanged += new System.EventHandler(this.Dropdownlist1_SelectedIndexChanged);
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            this.btnAddnew.Click += new System.EventHandler(this.btnAddnew_Click);
            this.btnDelete.Click += new System.EventHandler(this.btnFilter_Click);
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            this.dtgList.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgList_ItemCommand);
            this.dtgList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgList_PageIndexChanged);
            this.dtgList.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dtgList_SortCommand);
            this.Load += new System.EventHandler(this.Page_Load);

        }
		#endregion

		
		
		private object CheckDBNull( string info )
		{
			if ( info == String.Empty || info == null )
			{
				return DBNull.Value;
			}
			return info;
		}
				
		private void Dropdownlist1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}
		private void LoadDataCombo()
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			clsHREmpList.LoadComboCompany(cboLSComapyID, strTextField, strLanguage,this.Page);
			//clsCommon.LoadDropDownListControl(cboCustomerTypeID,"sp_GetDataCombo @TableName='SER_tblCustomerType',@Fields='CusTypeID as [ID],[Code]+''- '' + [Name] as [Name]'","ID","Name",true);
			strTextField = strLanguage == "VN"?"NameVN":"Name";
			clsCommon.LoadDropDownListControl(cboCustomerTypeID,"SER_spfrmCustomerType  @Activity=N'getList',@Language='" + strLanguage + "',@UserGroupID='" + strUserGroupID + "'","CusTypeID",strTextField,true);
		}
		private void BindData()
		{
			DataTable dtResult = new DataTable();	
			this.dtgList.PageSize = uctrlColumns.iPageRows;		
			string sAccountLogin= this.Page.Session["AccountLogin"]!=null?this.Page.Session["AccountLogin"].ToString():"admin";
			string sSQL = "SER_spfrmLevel1 @Activity=N'getList',@LSCompanyID='"+this.cboLSComapyID.SelectedValue.Trim()+"',@CustomerTypeID='"+this.cboCustomerTypeID.SelectedValue.Trim()+"',@LSLevel1Code='"+this.txtLSLevel1Code.Text.Trim()+"',@Name=N'"+this.txtName.Text.Trim()+"',@VNName=N'"+this.txtVNName.Text.Trim()+"',@ShortName=N'"+this.txtShortName.Text.Trim()+"',@UserGroupID=N'"+sAccountLogin+"'";
			dtResult = clsCommon.GetDataTable(sSQL);			
			this.dtgList.DataSource =dtResult;
			this.dtgList.DataBind();						
			if(this.uctrlColumns.bAdvMultiSort)
			{
				try
				{
					DataGridSort.AdvancedMultiSort(dtgList, dtResult, uctrlColumns);						
				}
				catch
				{
					dtgList.CurrentPageIndex = 0;
					DataGridSort.AdvancedMultiSort(dtgList, dtResult, uctrlColumns);					
				}
			}
			else
			{
				try
				{
					DataGridSort.Refresh(dtgList, dtResult);					
				}
				catch
				{
					dtgList.CurrentPageIndex = 0;
					DataGridSort.Refresh(dtgList, dtResult);						
				}
			}
			try
			{
				this.uctrlColumns.iTotalRows = dtResult.Rows.Count;			
			}
			catch{}
			return;
			
		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			BindData();
		}

		private void dtgList_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataRow dRow;
			try
			{
				if(e.CommandName.Trim().ToUpper() =="EDIT")
				{
					//dRow=clsCommon.GetDataRow("SER_spfrmCustomer @Activity='getDataByID',@CustomerID='"+dtgList.Items[e.Item.ItemIndex].Cells[1].Text.Trim()+"'");
					Session["ssStatusWorking"]="Update";
					Session["ssCustomerID"] = dtgList.Items[e.Item.ItemIndex].Cells[1].Text.Trim();
					this.Response.Redirect(@"Editpage.aspx?ModuleID=SER&ParentID=5000001&FunctionID=5000006&Ascx=MdlSER/Customer.ascx");
				}				
			}	
			catch(Exception ex)
			{
				//lblErr.Text = ex.Message;
			}		
		}

		private void btnFilter_Click(object sender, System.EventArgs e)
		{
			string strListID="@";
			for(int i=0;i< dtgList.Items.Count;i++)
			{
				if( ((CheckBox)dtgList.Items[i].FindControl("chkSelect")).Checked==true)
				{
					strListID+=dtgList.Items[i].Cells[1].Text.Trim()+"@";
				}
			}
			this.txtStrLSLevel1ID.Text=strListID;
			string result="";
			try
			{
				result=clsCommon.sImpactDB(null,"Delete",this,"SER_spfrmLevel1");
				if(!result.Equals(""))
				{
					clsChangeLang.popupWindow(this.Parent,result,"EN","",0);					
					return;
				}
				else
				{
					clsChangeLang.popupWindow(this.Parent,"0104","EN","",1);
				}
				BindData();
			}
			catch  (Exception exp)
			{	clsChangeLang.popupWindow(this.Parent,"0048","EN","",0);
				return;
			}	
			/*try
			{	
				string sSQL = "SER_spfrmLevel1 @Activity='Delete',@strLSLevel1ID='"+strListID+"'";
				clsCommon.Exc_CommandText(sSQL);					
				BindData();								
				Session["ssStatusWorking"] = "AddNew" ;
				clsChangeLang.popupWindow(this.Parent,"0104","EN","",1);
			}
			catch  (Exception exp)
			{	
				return;
			}		*/	
		}
		private DataTable getListCustomer()
		{
			DataTable dtResult = new DataTable();
			try
			{
				string sAccountLogin= this.Page.Session["AccountLogin"]!=null?this.Page.Session["AccountLogin"].ToString():"admin";
				string sSQL = "SER_spfrmLevel1 @Activity=N'getList',@LSCompanyID='"+this.cboLSComapyID.SelectedValue.Trim()+"',@CustomerTypeID='"+this.cboCustomerTypeID.SelectedValue.Trim()+"',@LSLevel1Code='"+this.txtLSLevel1Code.Text.Trim()+"',@Name=N'"+this.txtName.Text.Trim()+"',@VNName=N'"+this.txtVNName.Text.Trim()+"',@ShortName=N'"+this.txtShortName.Text.Trim()+"',@UserGroupID=N'"+sAccountLogin+"'";
				dtResult = clsCommon.GetDataTable(sSQL);
			}
			catch
			{
				return dtResult;
			}
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
				DataGridSort.AdvancedMultiSort(dtgList, getListCustomer(), uctrlColumns);				
			}
			catch{}
		}
		private void ButtonPRChanged_ServerClick(object sender, EventArgs e)
		{
			try
			{
				dtgList.PageSize = uctrlColumns.iPageRows;
				dtgList.CurrentPageIndex = 0;
				DataView dv = new DataView(getListCustomer());
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
				DataGridSort.Grid_Sort(dtgList, getListCustomer(), e, uctrlColumns);			}
			catch{}
		}

		private void dtgList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			try
			{
				dtgList.CurrentPageIndex=e.NewPageIndex;
				BindData();					
				dtgList.DataBind();
			}
			catch(Exception ex)
			{
				string str = ex.Message;
			}				
			try
			{
				//DataGridSort.Grid_IndexChanged(dtgList,null, e);
			}
			catch{}//
		}

		private void btnAddnew_Click(object sender, System.EventArgs e)
		{
			//Editpage.aspx?ModuleID=SER&ParentID=5000001&FunctionID=5000006&Ascx=MdlSER/Customer.ascx
			Session["ssCustomerID"]=null;
			Response.Redirect(Request.ApplicationPath.Trim() + "/Editpage.aspx?ModuleID=SER&ParentID=5000001&FunctionID=5000006&Ascx=MdlSER/Customer.ascx");
		}

		private void btnExport_Click(object sender, System.EventArgs e)
		{
			
			#region Data
			/*
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
            */
            #endregion
            
            DataTable dt = new DataTable();	
            string sAccountLogin= this.Page.Session["AccountLogin"]!=null?this.Page.Session["AccountLogin"].ToString():"admin";
            string sSQL = "SER_spfrmLevel1 @Activity=N'getListReport',@LSCompanyID='"+this.cboLSComapyID.SelectedValue.Trim()+"',@CustomerTypeID='"+this.cboCustomerTypeID.SelectedValue.Trim()+"',@LSLevel1Code='"+this.txtLSLevel1Code.Text.Trim()+"',@Name=N'"+this.txtName.Text.Trim()+"',@VNName=N'"+this.txtVNName.Text.Trim()+"',@ShortName=N'"+this.txtShortName.Text.Trim()+"',@LanguageID='" + strLanguage + "',@UserGroupID=N'"+sAccountLogin+"'";
            dt= clsCommon.GetDataTable(sSQL);            
          
            try
            {
                string strHeaderParams = "";
                string strHeaderValues = "";
                string strFooterParams = "";
                string strFooterValues = "";
                //Phan khai bao se dung Tool bao cao Excel
                #region Header 
                //DataTable dtSI_ComInfo = clsCommon.GetDataTable("PR_spfrmPAYROLL @Activity = 'GetExchangeRate', @MMYYYY = '" + txtMonth.Text + "'");               
                //string compname = EmpHeaderSearch1.cboLevel1.SelectedItem.ToString();				   
                strHeaderParams = "CompanyName;MonthYear";                
                strHeaderValues = "AAAAAAAAA;BBBBBBBBBBB";
                //strHeaderValues = compname+";"+ txtMonthYear.Text;                
                #endregion
                
                #region Footer
                strFooterParams = "PrintDate;Signer";
//                string strDate = DateTime.Now.ToShortDateString();
//                if (strDate == "")
//                    strDate = "..../..../20.....";
                strFooterValues = "CCCCCC;Mr David";
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
                bc.IsGroupLv1 = false; //Co Group 1 khong?
                bc.GroupLv1 = 1; //Chỉ định Group level 1 ở cột thứ I
                bc.IsGroupLv2 = false; //Co Group 2 khong?
                bc.GroupLv2 = 2; //Chỉ định Group level 2 ở cột thứ II
                bc.IsSum = false; //Có Sum tong hay không? Nếu có thì phải có id template (itempsumtotal) đi kèm
                bc.IsSum1 = false; //Có Sum cap 1 hay không? Nếu có thì phải có id template (itempsumlv1) đi kèm
                bc.IsSum2 = false; //Có Sum cap 2 hay không? Nếu có thì phải có id template (itempsumlv2) đi kèm				
                bc.sfileTemplate = "SER/RPT_CustomerList.htm";
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
                //lblErr.Text = ex.Message;
            }
            finally
            {
                dt.Dispose();
            }

		}
	}
}
