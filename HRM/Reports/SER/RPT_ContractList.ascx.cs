namespace MdlSER
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.IO;
	using System.Configuration;
	using iHRPCore.Com;
	using iHRPCore.HRComponent;
	using iHRPCore.TMSComponent; 
	using System.Data.SqlClient;
	using iHRPCore.Include;
	using System.Data.SqlClient;
	using iHRPCore.SerComponent;
	using GridSort;	
	using iHRPCore.PRComponent;
	using FPTToolWeb.Control.DataGrids;
	using FPTToolWeb.Exports;

	/// <summary>
	///		Summary description for Company.
	/// </summary>
	public class RPT_ContractList : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.DataGrid dtgList;
		protected System.Web.UI.WebControls.LinkButton btnAddNew;
		protected System.Web.UI.WebControls.LinkButton btnSearch;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.Label txtContract;
		protected System.Web.UI.WebControls.Label txtDOSign;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.DropDownList cboCustomerTypeID;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.TextBox txtLSLevel1Code;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.TextBox txtContractExpireTo;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.TextBox txtContractExpireFrom;
		protected System.Web.UI.WebControls.Label lblSanTo;
		protected System.Web.UI.WebControls.Label lblCode;
		protected System.Web.UI.WebControls.Label lblName;
		protected System.Web.UI.WebControls.Label lblGroup;
		protected System.Web.UI.WebControls.TextBox txtLSLevel1Name;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		
		protected System.Web.UI.WebControls.DropDownList cboContractStatus;
		protected System.Web.UI.WebControls.TextBox txtSignDateFrom;
		protected System.Web.UI.WebControls.TextBox txtSignDateTo;
		protected System.Web.UI.WebControls.TextBox txtSANExpireFrom;
		protected System.Web.UI.WebControls.TextBox txtSANExpireTo;
		protected System.Web.UI.WebControls.DropDownList cboLSCompanyID;
		protected System.Web.UI.WebControls.TextBox txtStrListID;
		protected ColumnList uctrlColumns;
		public string strLanguage = "EN";
		public string strUserGroupID="";

		private void Page_Load(object sender, System.EventArgs e)
		{
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
			strUserGroupID=Session["AccountLogin"].ToString();			
			Session["ssStatusWorking"] = "AddNew" ;
			// Put user code to initialize the page here
			loadDataCombo();
			if(!Page.IsPostBack)
			{
				BindDataGrid();				
			}			
			ButtonClick();
			btnDelete.Attributes["OnClick"]="return CheckDelete();";
			if(Session["ssCuccess"]!=null)
			{
				clsChangeLang.popupWindow(this.Parent,"0044","EN","",1);
			}			
			
		}
		private void ButtonSort_ServerClick(object sender, EventArgs e)
		{
			try
			{
				DataGridSort.AdvancedMultiSort(dtgList, null, uctrlColumns);			
				
			}
			catch{}
		}
		private void ButtonPRChanged_ServerClick(object sender, EventArgs e)
		{
			try
			{
				dtgList.PageSize = uctrlColumns.iPageRows;
				dtgList.CurrentPageIndex = 0;
				BindDataGrid();

			}
			catch{}
			
		}
		private void ButtonClick()
		{
			this.uctrlColumns.ButtonSort.ServerClick += new EventHandler(ButtonSort_ServerClick);
			this.uctrlColumns.ButtonPRChanged.ServerClick += new EventHandler(ButtonPRChanged_ServerClick);
		}

		private void BindDataGrid()
		{	
			DataTable dt= new DataTable();			
			this.dtgList.PageSize = uctrlColumns.iPageRows;		
			dt=getListContract();
			dtgList.DataSource = dt;
			//dtgList.CurrentPageIndex = 0;dtgList.CurrentPageIndex
			dtgList.DataBind();
			if(this.uctrlColumns.bAdvMultiSort)
			{
				try
				{
					DataGridSort.AdvancedMultiSort(dtgList, dt, uctrlColumns);						
				}
				catch
				{
					dtgList.CurrentPageIndex = 0;
					DataGridSort.AdvancedMultiSort(dtgList, dt, uctrlColumns);					
				}
			}
			else
			{
				try
				{
					DataGridSort.Refresh(dtgList, dt);					
				}
				catch
				{
					dtgList.CurrentPageIndex = 0;
					DataGridSort.Refresh(dtgList, dt);						
				}
			}
			try
			{
				this.uctrlColumns.iTotalRows = dt.Rows.Count;			
			}
			catch{}
			return;
		}

		private DataTable getListContract()
		{
			DataTable dtResult= new DataTable();
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			string sSQL = "SER_spfrmContract @Activity=N'getList',@Language=N'"+strLanguage+"',@LSCompanyID=N'"+this.cboLSCompanyID.SelectedValue.Trim()+"',@CustomerTypeID=N'"+this.cboCustomerTypeID.SelectedValue.Trim()+"',@LSLevel1Code=N'"+this.txtLSLevel1Code.Text.Trim()+"',@LSLevel1Name=N'"+this.txtLSLevel1Name.Text.Trim()+"',@ContractStatus=N'"+this.cboContractStatus.SelectedValue.Trim()+"',@SignDateFrom=N'"+this.txtSignDateFrom.Text.Trim()+"',@SignDateTo=N'"+this.txtSignDateTo.Text.Trim()+"',@ContractExpireFrom=N'"+this.txtContractExpireFrom.Text.Trim()+"',@ContractExpireTo=N'"+this.txtContractExpireTo.Text.Trim()+"',@SANExpireFrom=N'"+this.txtSANExpireFrom.Text.Trim()+"',@SANExpireTo=N'"+this.txtSANExpireTo.Text.Trim()+"',@UserGroupID=N'"+strUserGroupID+"'";
			try
			{
				dtResult = clsCommon.GetDataTable(sSQL);
				return dtResult;
			}
			catch
			{
				return dtResult;
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
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            this.dtgList.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgList_ItemCommand);
            this.dtgList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgList_PageIndexChanged);
            this.dtgList.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dtgList_SortCommand);
            this.dtgList.SelectedIndexChanged += new System.EventHandler(this.dtgList_SelectedIndexChanged);
            this.Load += new System.EventHandler(this.Page_Load);
            
            //remind
            string sReminder="";
            //string sUserGroupID=iHRPCore.Com.clsCommon.reminderContract(Session["AccountLogin"]!=null?Session["AccountLogin"].ToString():"admin");
            string strUserID=Session["AccountLogin"]!=null?Session["AccountLogin"].ToString():"admin";
            int countContract=0;
            
            if (Session["ReminderContract"]!="0")
            {  
                sReminder="";//iHRPCore.Com.clsCommon.reminderContract(Session["AccountLogin"]!=null?Session["AccountLogin"].ToString():"admin");	
                DataTable dtData= clsCommon.GetDataTable("SER_spfrmContract @Activity='getListRemind',@UserGroupID='" + strUserID + "'");
                if(dtData!=null){
                    countContract=dtData.Rows.Count;
                }
                if(countContract==1){
                    sReminder="There is ";
                }else{
                    sReminder="There are ";
                }
                if (countContract!=0){
                    sReminder=sReminder+ countContract.ToString() + " contracts to be expired! <br>";
                    for(int i=0;i<dtData.Rows.Count;i++){
                        sReminder+="<br>Company: " + dtData.Rows[i]["LSLevel1Name"].ToString();   
                        sReminder+=", SAN Date: "+dtData.Rows[i]["SANDate"].ToString();   
                    }
                    clsChangeLang.popupWindowReminder(this.Parent,sReminder,"",2);	
                }
            }
            Session["ReminderContract"]="0";
            //

        }
		#endregion

		private void btnAddNew_Click(object sender, System.EventArgs e)
		{	Session["ssContractID"]=null;
			Response.Redirect(Request.ApplicationPath.Trim() + "/Editpage.aspx?ModuleID=SER&ParentID=500000&Ascx=MdlSER/Contract.ascx&FunctionID=5000004");
		}

		private void dtgList_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataRow dRow;
			string idCus="";
			try
			{
				if(e.CommandName.Trim().ToUpper() =="EDIT")
				{					
					Session["ssStatusWorking"]="Update";
					Session["ssContractID"] = dtgList.Items[e.Item.ItemIndex].Cells[1].Text.Trim();
					Session["ssCusIDFromListContract"]=dtgList.Items[e.Item.ItemIndex].Cells[2].Text.Trim();
					this.Response.Redirect(@"Editpage.aspx?ModuleID=SER&ParentID=500000&FunctionID=5000004&Ascx=MdlSER/Contract.ascx");
				}				
			}	
			catch(Exception ex)
			{
				//lblErr.Text = ex.Message;
			}		
		
		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			BindDataGrid();
		}
		private void loadDataCombo()
		{
			string strTextField = strLanguage == "VN"?"NameVN":"Name";
			clsHREmpList.LoadComboCompany(cboLSCompanyID, strTextField, strLanguage,this.Page);			
			clsCommon.LoadDropDownListControl(cboCustomerTypeID,"SER_spfrmCustomerType  @Activity=N'getList',@Language='" + strLanguage + "',@UserGroupID='" + strUserGroupID + "'","CusTypeID",strTextField,true);
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			string strListID="@";
			for(int i=0;i< dtgList.Items.Count;i++)
			{
				if( ((CheckBox)dtgList.Items[i].FindControl("chkSelect")).Checked==true)
				{
					strListID+=dtgList.Items[i].Cells[1].Text.Trim()+"@";
				}
			}
			this.txtStrListID.Text=strListID;
			string result="";
			try
			{	
				string sSQL = "SER_spfrmContract @Activity='Delete',@StrListID='"+strListID+"'";
				clsCommon.Exc_CommandText(sSQL);					
				BindDataGrid();								
				Session["ssStatusWorking"] = "AddNew";
				clsChangeLang.popupWindow(this.Parent,"0104","EN","",1);
			}
			catch  (Exception exp)
			{
					clsChangeLang.popupWindow(this.Parent,"0048","EN","",0);
				return;
			}			
			
		}

		private void dtgList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			try
			{
				dtgList.CurrentPageIndex=e.NewPageIndex;
				BindDataGrid();					
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

		private void dtgList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		private void dtgList_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			try
			{	DataTable dtResult= new DataTable();
				dtResult = getListContract();					
				DataGridSort.Grid_Sort(dtgList, dtResult, e, uctrlColumns);			}
			catch{}
		}

		private void btnExport_Click(object sender, System.EventArgs e)
		{
		
		    //Parent-> Lấy thông tin parent các dòng của hợp đồng
		    //Sub là Service và payment, lấy 2 sub con có link là mã hợp đồng		    
		    string strFileTemplate = "Upload/TemplateReport/SER/RPT_Contract.htm";
            string strListID="@";
            for(int i=0;i< dtgList.Items.Count;i++)
            {
                if( ((CheckBox)dtgList.Items[i].FindControl("chkSelect")).Checked==true)
                {
                    strListID+=dtgList.Items[i].Cells[1].Text.Trim()+"@";                   
                }
            }
            this.txtStrListID.Text=strListID;            
            DataTable dt = new DataTable();
            string sAccountLogin= this.Page.Session["AccountLogin"]!=null?this.Page.Session["AccountLogin"].ToString():"admin";
            string sSQL_C = "SER_spfrmContract @Activity='getDataByID_Report',@Language='"+strLanguage+"',@strListID='" +strListID+ "'";
            dt= clsCommon.GetDataTable(sSQL_C);
          
            try
            {
                string strHeaderParams = "";
                string strHeaderValues = "";
                string strFooterParams = "";
                string strFooterValues = "";
                //Phan khai bao se dung Tool bao cao Excel
                #region Header 
                strHeaderParams = "";
                strHeaderValues = "";
                #endregion
                #region Footer
                string strDate = DateTime.Now.ToString("dd/MM/yyyy");
                strFooterParams = "Date";					
                strFooterValues = strDate;
                #endregion
				
                FPTToolWeb.Reports.v10.clsBaocao bc = new FPTToolWeb.Reports.v10.clsBaocao();
                #region Config Basic
                bc.sfileTemplate = strFileTemplate;
                #region Sub report
                //Lấy danh sách Service của các hợp đồng trên
                DataTable dt1 = new DataTable();
                string sSQL = "SER_spfrmContract @Activity='getListService_Report',@Language='"+strLanguage+"',@strListID='" +strListID+ "'";
                dt1=clsCommon.GetDataTable(sSQL);                
                bc.dtSubReport1 = clsCommon.GetDataTable(sSQL);
                bc.sItemLink1 = "ContractID";
				
				//Danh danh sách thanh toán
				sSQL = "SER_spfrmContract @Activity='getListPayment_Report',@Language='"+strLanguage+"',@strListID='" +strListID+ "'";
                bc.dtSubReport2 = clsCommon.GetDataTable(sSQL);
                bc.sItemLink2 = "ContractID";
                #endregion
                string strReports = bc.strReportPageDoc(dt);
                #endregion
                //End
                
               // txtResult.Text = strReports;
                FPTToolWeb.Exports.ExcelExporter myExcelXport=new FPTToolWeb.Exports.ExcelExporter(this.Page);
                myExcelXport.ExportHTMLTo(strReports,"doc");
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
