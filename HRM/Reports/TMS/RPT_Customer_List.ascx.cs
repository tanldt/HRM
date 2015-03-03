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
			dtgList.AllowPaging=false;
			this.BindData();
			dtgList.Columns[7].Visible=false;
			iHRPCore.DataGridExcelExporter myExcelXport=new iHRPCore.DataGridExcelExporter(dtgList);
			myExcelXport.Export("");
			myExcelXport = null;
			dtgList.AllowPaging=true;

		}
	}
}
