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
	///		Summary description for UnfrequentIncomeType.
	/// </summary>
	public class UnfrequentIncomeType: System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.LinkButton btnFilter;
		protected System.Web.UI.WebControls.DataGrid dtgList;
		protected System.Web.UI.WebControls.LinkButton btnSearch;
		protected System.Web.UI.WebControls.LinkButton btnAddnew;
		protected System.Web.UI.WebControls.Label lblCode;
		protected System.Web.UI.WebControls.Label lblName;
		protected System.Web.UI.WebControls.Label lblNote;
		protected System.Web.UI.WebControls.Label lblActive;
		protected System.Web.UI.WebControls.Label lblNameVN;
		protected System.Web.UI.WebControls.TextBox txtCode;
		protected System.Web.UI.WebControls.TextBox txtNote;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected ColumnList uctrlColumns;
		protected System.Web.UI.WebControls.TextBox txtServiceID;
		protected System.Web.UI.WebControls.TextBox txtName;
		protected System.Web.UI.WebControls.CheckBox chkUsed;
		protected System.Web.UI.WebControls.TextBox txtVNName;
		public string strLanguage = "EN";

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";			
			if(!Page.IsPostBack)
			{
				Session["ssStatusWorking"] = "AddNew";
				BindingData();
				DataGridSort.AddItemColumn(uctrlColumns, dtgList);
			}
			btnSave.Attributes.Add("OnClick", " return validform()");
			btnFilter.Attributes["OnClick"]="return CheckDelete();";
			ButtonClick();
			this.dtgList.PageSize = uctrlColumns.iPageRows;	
		}
		private void BindingData()
		{
			DataTable dtResult = new DataTable();	
			this.dtgList.PageSize = uctrlColumns.iPageRows;		
			string sSQL = "SER_spfrmService @Activity=N'getList',@Code='"+txtCode.Text+"',@Name=N'"+txtName.Text+"',@VNName=N'"+txtVNName.Text+"',@Note=N'"+txtNote.Text+"'";			
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
			catch{this.uctrlColumns.iTotalRows=0;}
			return;
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
            this.btnAddnew.Click += new System.EventHandler(this.btnAddnew_Click);
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            this.dtgList.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgList_ItemCommand);
            this.dtgList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgList_PageIndexChanged);
            this.dtgList.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dtgList_SortCommand);
            this.dtgList.SelectedIndexChanged += new System.EventHandler(this.dtgList_SelectedIndexChanged);
            this.Load += new System.EventHandler(this.Page_Load);

        }
		#endregion

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
				BindingData();

			}
			catch{}
			
		}
		private void ButtonClick()
		{
			this.uctrlColumns.ButtonSort.ServerClick += new EventHandler(ButtonSort_ServerClick);
			this.uctrlColumns.ButtonPRChanged.ServerClick += new EventHandler(ButtonPRChanged_ServerClick);
		}
		
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			string result ="";
			if(Session["ssStatusWorking"] == "AddNew" )
			{
				try
				{				
					string sSQL = "SER_spfrmService @Activity=N'AddNew',@Code='"+this.txtCode.Text.Trim()+"',@Name=N'"+this.txtName.Text.Trim()+"',@VNName=N'"+this.txtVNName.Text.Trim()+"',@Note=N'"+this.txtNote.Text.Trim()+"',@Used='"+this.chkUsed.Checked+"'";
					result=clsCommon.Exc_CommandText_(sSQL);					
					
					if(!result.Equals(""))
					{
						clsChangeLang.popupWindow(this.Parent,result,"EN","",0);
						return;
					}
					else
					{
						clsChangeLang.popupWindow(this.Parent,"0046","EN","",1);
					}
				}
				catch  (Exception exp)
				{
					clsChangeLang.popupWindow(this.Parent,"0027","EN","",0);
					return;
				}
			}
			else if(Session["ssStatusWorking"] == "Update" )
			{
				string sSQL = "SER_spfrmService @Activity=N'Update',@ServiceID='"+this.txtServiceID.Text.Trim()+"',@Code='"+this.txtCode.Text.Trim()+"',@Name=N'"+this.txtName.Text.Trim()+"',@VNName=N'"+this.txtVNName.Text.Trim()+"',@Note=N'"+this.txtNote.Text.Trim()+"',@Used='"+this.chkUsed.Checked+"',@Result='null'";
				result=clsCommon.Exc_CommandText_(sSQL);
				if(!result.Equals(""))
				{
						clsChangeLang.popupWindow(this.Parent,result,"EN","",0);					
					return;
				}
				else
				{
					clsChangeLang.popupWindow(this.Parent,"0044","EN","",1);
				}
				
			}
			Session["ssStatusWorking"] = "AddNew";
			btnAddnew_Click(null,null);
			BindingData();
		}
		
		private void Dropdownlist1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			this.BindingData();
		}

		private void dtgList_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
				DataRow dRow;
			try
			{
				if(e.CommandName.Trim().ToUpper() =="EDIT")
				{
					dRow=clsCommon.GetDataRow("SER_spfrmService @Activity='getDataByID',@ServiceID='"+dtgList.Items[e.Item.ItemIndex].Cells[1].Text.Trim()+"'");// loadDataByID(dtgList.Items[e.Item.ItemIndex].Cells[1].Text.Trim());
					this.txtServiceID.Text=dRow["ServiceID"].ToString().Trim();
					this.txtCode.Text=dRow["Code"].ToString().Trim();
					this.txtName.Text=dRow["Name"].ToString().Trim();
					this.txtVNName.Text=dRow["VNName"].ToString().Trim();
					this.txtNote.Text=dRow["Note"].ToString().Trim();
					if(dRow["Used"].ToString().Trim().Equals("True"))
					{
						this.chkUsed.Checked=true;
					}
					else
					{
						this.chkUsed.Checked=false;
					}
					
					
					Session["ssStatusWorking"]="Update";
				}				
			}	
			catch(Exception ex)
			{
				//lblErr.Text = ex.Message;
			}
		}

		private DataRow loadDataByID(string strCusTypeID)
		{
			DataTable result = new DataTable();
			DataRow iRow;
			try
			{
				SqlDataAdapter da = new SqlDataAdapter();				
				SqlCommand cmd = new SqlCommand();
				cmd.Connection = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "SER_spfrmCustomerType";
				cmd.Parameters.Add("@Activity", SqlDbType.NVarChar,50).Value = "getDataByID";
				cmd.Parameters.Add("@CusType", SqlDbType.NVarChar, 20).Value = strCusTypeID.Trim();

				da.SelectCommand = cmd;
				da.Fill( result );	
				iRow= result.Rows[0];
				return iRow;
			}				
			catch ( Exception e )
			{
				return null;
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
			try
			{	
				string sSQL = "SER_spfrmService @Activity='Delete',@StrServiceID='"+strListID+"'";
				clsCommon.Exc_CommandText(sSQL);					
				BindingData();								
				Session["ssStatusWorking"] = "AddNew";
				clsChangeLang.popupWindow(this.Parent,"0104","EN","",1);
			}
			catch  (Exception exp)
			{
					clsChangeLang.popupWindow(this.Parent,"0048","EN","",0);
				return;
			}			
		}

		private void btnAddnew_Click(object sender, System.EventArgs e)
		{
			clsCommon.ClearControlValue(this);
			this.chkUsed.Checked=true;
			//uctrlColumns.tbPageRows.text=uctrlColumns.iPageRows;
			BindingData();
		}

		private void dtgList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			try
			{
				BindingData();
				dtgList.CurrentPageIndex=e.NewPageIndex;
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

		private void dtgList_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			try
			{
				DataTable dtResult = new DataTable();					
				string sSQL = "SER_spfrmService @Activity=N'AddNew',@Code='"+txtCode.Text+"',@Name=N'"+txtName.Text+"',@VNName=N'"+txtVNName.Text+"',@Note=N'"+txtNote.Text+"'";
				//dtResult= clsCustomer_Types.getDataCustomerTypes(txtCode.Text,txtName.Text,txtVNName.Text,txtNote.Text);				
				dtResult = clsCommon.GetDataTable(sSQL);
				DataGridSort.Grid_Sort(dtgList, dtResult, e, uctrlColumns);
			}
			catch{}
		}

		private void dtgList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}
	}
}
