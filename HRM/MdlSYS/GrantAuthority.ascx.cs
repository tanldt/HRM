namespace MdlSYS
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.Com;
	using iHRPCore.HRComponent;	
	using iHRPCore.TMSComponent; 
	using System.Data.SqlClient;	
	using iHRPCore;
	using GridSort;
	using System.Configuration;

	/// <summary>
	///		Summary description for GrantAuthority.
	/// </summary>
	public class GrantAuthority : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblEmpID;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.LinkButton btnAddnew;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.CheckBox chkSelectAll;
		protected System.Web.UI.WebControls.DataGrid dtgList;
		protected System.Web.UI.WebControls.TextBox txtPageLoad;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnSearch_Replace;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		string strLanguage="EN";
		protected System.Web.UI.WebControls.TextBox txtAuthorityID;
		protected System.Web.UI.WebControls.TextBox txtFromDate;
		protected System.Web.UI.WebControls.TextBox txtToDate;
		protected System.Web.UI.WebControls.DropDownList cboAssignTo;
		protected System.Web.UI.WebControls.TextBox txtDescription;
		protected System.Web.UI.WebControls.DropDownList cboAuthorityTypeID;
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.TextBox txtEmpID;
		protected ColumnList uctrlColumns;

		private void Page_Load(object sender, System.EventArgs e)
		{
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
			if (Session["EmpID"]==null || Session["AccountLogin"].ToString().ToLower()=="admin")
			{				
				lblErr.Text=clsChangeLang.getStringAlert("LR_0001",strLanguage);	
				btnSave.Enabled=false;
				btnAddnew.Enabled=false;
				btnDelete.Enabled=false;					
				return;
			}	
			if (!Page.IsPostBack)
			{
				BindDataGrid(Session["EmpID"]);
				LoadComboData();
				Session["ucAccount"]="addnew";				
			}
			btnSave.Attributes.Add("Onclick","return checkSave();");
			btnDelete.Attributes.Add("Onclick","return checkdelete();");			
			ButtonClick();
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
			this.btnAddnew.Click += new System.EventHandler(this.btnAddnew_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			this.dtgList.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgList_ItemCommand);
			this.dtgList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgList_PageIndexChanged);
			this.dtgList.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dtgList_SortCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		
		private void LoadComboData()
		{
			clsCommon.LoadDropDownListControl(cboAssignTo," HR_clsCommon @Activity='getCboEmpHeader_UMS'","ID","Name",true); 
			clsCommon.LoadDropDownListControl(cboAuthorityTypeID," SYS_spfrmAUTHORITY @Activity='GetPermission',@EmpID='" + Session["EmpID"].ToString() + "'","ID","Name",true);
		}
		private void BindDataGrid(object sEmpID)
		{
			DataTable dtData= new DataTable();						
			dtData= clsCommon.GetDataTable("SYS_spfrmAUTHORITY @Activity='GetDataByEmpID',@EmpID='" + sEmpID + "'");			

			try
			{
				dtgList.DataSource= dtData;
				dtgList.DataBind();

				//Hau
				this.dtgList.PageSize = uctrlColumns.iPageRows;
					
				if(this.uctrlColumns.bAdvMultiSort)
				{
					try
					{
						DataGridSort.AdvancedMultiSort(dtgList, dtData, uctrlColumns);
					}
					catch
					{
						this.dtgList.CurrentPageIndex = 0;
						DataGridSort.AdvancedMultiSort(dtgList, dtData, uctrlColumns);
					}
				}
				else
				{
					try
					{
						DataGridSort.Refresh(dtgList, dtData);
					}
					catch
					{
						this.dtgList.CurrentPageIndex = 0;
						DataGridSort.Refresh(dtgList, dtData);
					}
				}
				this.uctrlColumns.iTotalRows = dtData.Rows.Count;//				
			}
			catch(Exception ex)
			{
				clsChangeLang.popupWindowExp(this.Parent,ex);
			}
			finally
			{
				dtData.Dispose();
			}

		}
		#region Cac su kien xu li thao tac Sort (Hau)

		private void dtgList_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			try
			{
				DataGridSort.Grid_Sort(dtgList, Read_Data(), e, uctrlColumns);
			}
			catch{}
		}

		private DataTable Read_Data()
		{	
			DataTable dtData=clsCommon.GetDataTable("SYS_spfrmAUTHORITY @Activity='GetDataByEmpID',@EmpID='" + Session["EmpID"].ToString() + "'");
			return dtData;
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
				DataGridSort.AdvancedMultiSort(dtgList, Read_Data(), uctrlColumns);
			}
			catch{}
		}

		private void ButtonPRChanged_ServerClick(object sender, EventArgs e)
		{
			try
			{
				dtgList.PageSize = uctrlColumns.iPageRows;
				dtgList.CurrentPageIndex = 0;
				DataView dv = new DataView(Read_Data());
				dv.Sort = DataGridSort.sMulSort;
				dtgList.DataSource = dv;
				dtgList.DataBind();
			}
			catch{}
		}
		#endregion		
		private void dtgList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			try
			{
				BindDataGrid(Session["EmpID"]);
				dtgList.CurrentPageIndex=e.NewPageIndex;
				dtgList.DataBind();					
			}
			catch(Exception ex)			
			{
				DataGridSort.Grid_IndexChanged(dtgList, Read_Data(), e);
				//clsChangeLang.popupWindowExp(this.Parent,ex);
			}
		}
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			//Update on Grid
			string sErr="";
			
			
			try
			{
				
				if (Session["ucAccount"].ToString()=="addnew")
				{
					sErr+=clsCommon.sImpactDB("@Creater",Session["AccountLogin"],Session["EmpID"].ToString(),"Save",this,"SYS_spfrmAUTHORITY");						
				}
				else
				{
					sErr+=clsCommon.sImpactDB("Update",this,"SYS_spfrmAUTHORITY");				
				}
					
			}
			catch(Exception ex)
			{
				clsChangeLang.popupWindowExp_Mess(this.Parent,ex);
			}
			
			if (sErr.Length<12)
			{
				btnAddnew_Click(null,null);				
				clsChangeLang.popupWindow(this.Parent,"0044",strLanguage,"",1);
			}
			else
			{
				clsChangeLang.popupWindow(this.Parent,sErr,"",0);
			}
		}
		private void dtgList_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				string sValue;
				DataRow iRow;
				if(e.CommandName.Trim().ToUpper() =="EDIT")
				{
					sValue = dtgList.Items[e.Item.ItemIndex].Cells[0].Text.Trim();					
					iRow = clsCommon.GetDataRow("SYS_spfrmAUTHORITY @Activity='GetDataByID',@AuthorityID='" + sValue + "'");
					if(iRow != null)
					{
						cboAuthorityTypeID.SelectedValue=iRow["AuthorityTypeID"].ToString();						
						txtFromDate.Text=iRow["FromDate"].ToString();						
						txtToDate.Text=iRow["ToDate"].ToString();
						cboAssignTo.SelectedValue=iRow["AssignTo"].ToString();						
						txtDescription.Text=iRow["Description"].ToString();	
						txtEmpID.Text=iRow["EmpID"].ToString();		
						txtAuthorityID.Text=sValue;
					}

				}								
				Session["ucAccount"] = "Edit";

			}
			catch(Exception ex)
			{
				clsChangeLang.popupWindowExp(this.Parent,ex);
			}
		}
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				string strID="";
				for(int i=0;i<dtgList.Items.Count;i++)
				{
					if(((CheckBox)dtgList.Items[i].FindControl("chkSelect")).Checked==true)
					{
						strID += dtgList.Items[i].Cells[0].Text.Trim() + "$";
					}
				}
				clsCommon.DeleteListRecord("SYS_spfrmAUTHORITY","AuthorityID",SqlDbType.NVarChar,20,strID);
				BindDataGrid(Session["EmpID"]);	
				btnAddnew_Click(null, null);
				clsChangeLang.popupWindow(this.Parent,"0044",strLanguage,"",1);			
			}
			catch(Exception ex)
			{
				clsChangeLang.popupWindowExp_Mess(this.Parent,ex);
			}
		}
		private void btnAddnew_Click(object sender, System.EventArgs e)
		{		
			clsCommon.ClearControlValue(this);
			Session["ucAccount"] = "addnew";
			BindDataGrid(Session["EmpID"]);
		}

		private void btnExport_Click(object sender, System.EventArgs e)
		{
			iHRPCore.DataGridExcelExporter myExcelXport=new iHRPCore.DataGridExcelExporter(dtgList);
			myExcelXport.Export("");
			myExcelXport = null;
		}
	}
}
