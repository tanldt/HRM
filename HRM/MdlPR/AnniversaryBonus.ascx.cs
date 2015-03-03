namespace MdlPR
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Data.SqlClient;
	using System.Configuration;
	using iHRPCore.Include;
	using iHRPCore.Com;
	using iHRPCore.TSComponent;
	using iHRPCore.HRComponent;
	using iHRPCore.TMSComponent;
	using GridSort;
	using iHRPCore.PRComponent;

	/// <summary>
	///		Summary description for AnniversaryBonus.
	/// </summary>
	public class AnniversaryBonus : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.LinkButton btnSearch;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.DataGrid dtgList;

		protected EmpHeaderSearch EmpHeaderSearch1;
		protected ColumnList uctrlColumns;
		protected System.Web.UI.WebControls.Label lblMM;
		protected System.Web.UI.WebControls.TextBox txtMM;
		protected System.Web.UI.WebControls.Label lblYYYY;
		protected System.Web.UI.WebControls.TextBox txtYYYY;		
		private string strLanguage = "EN";

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				LoadDefaultYear();

				this.dtgList.DataSource= new DataTable();
				this.dtgList.DataBind();				
			}
									
			btnSearch.Attributes.Add("OnClick","return validform()");						

			ButtonClick();			
		}


		private void LoadDefaultYear()
		{
			this.txtYYYY.Text = DateTime.Today.ToString("yyyy");
		}

		private void BindDataGrid()
		{
			string strEmpID = this.EmpHeaderSearch1.txtEmpID.Text.Trim().Replace("'","");
			string strEmpName = this.EmpHeaderSearch1.txtEmpName.Text.Trim();
			string strCompanyID = this.EmpHeaderSearch1.cboCompany.SelectedValue.Trim();
			string strLevel1ID = this.EmpHeaderSearch1.cboLevel1.SelectedValue.Trim();
			string strLevel2ID = this.EmpHeaderSearch1.cboLevel2.SelectedValue.Trim();
			string strLevel3ID = this.EmpHeaderSearch1.cboLevel3.SelectedValue.Trim();						
			string strLSEmpTypeID = this.EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue.Trim();
			string strStatus = this.EmpHeaderSearch1.optStatus.SelectedValue.Trim();

			string strMM = this.txtMM.Text.Trim();
			string strYYYY = this.txtYYYY.Text.Trim();
			
			string strSQL = "PR_spfrmAnnivesaryBonus @Activity = 'SearchEmp', @LanguageID='"+strLanguage+"'";
			strSQL +=", @EmpID ='" + strEmpID + "', @EmpName ='" + strEmpName +	"', @LSCompanyID = '" + strCompanyID + "', @LSLevel1ID = '" + strLevel1ID +	"', @LSLevel2ID = '" + strLevel2ID + "', @LSLevel3ID = '" + strLevel3ID + "', @LSEmpTypeID = '" + strLSEmpTypeID + "',@Status = '" + strStatus+"'";
			strSQL +=", @MM = '" + strMM +"', @YYYY='"+strYYYY+"'";

			DataTable dtList = new DataTable();
			try
			{									
				dtList = clsCommon.GetDataTable(strSQL);
				dtgList.DataSource = dtList;
				dtgList.DataBind();
				
				
				//hau
				this.dtgList.PageSize = uctrlColumns.iPageRows;
				if(this.uctrlColumns.bAdvMultiSort)
				{
					try
					{
						DataGridSort.AdvancedMultiSort(this.dtgList, dtList, this.uctrlColumns);
					}
					catch
					{
						this.dtgList.CurrentPageIndex = 0;
						DataGridSort.AdvancedMultiSort(this.dtgList, dtList, this.uctrlColumns);
					}
				}
				else
				{
					try
					{
						DataGridSort.Refresh(dtgList, dtList);
					}
					catch
					{
						dtgList.CurrentPageIndex = 0;
						DataGridSort.Refresh(dtgList, dtList);				
					}
				}
				this.uctrlColumns.iTotalRows = dtList.Rows.Count;
			
				dtgList.Dispose();			
			}
			catch(Exception ex)
			{
				clsChangeLang.popupWindowExp_Mess(this.Parent,ex);
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
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
		
		}
		
		private void btnExport_Click(object sender, System.EventArgs e)
		{
			iHRPCore.DataGridExcelExporter myExcelXport=new iHRPCore.DataGridExcelExporter(dtgList);			
			myExcelXport.Export("");
			myExcelXport =null;
		}


		#region Sort Grid
		private void ButtonClick()
		{
			this.uctrlColumns.ButtonSort.ServerClick += new EventHandler(ButtonSort_ServerClick);
			this.uctrlColumns.ButtonPRChanged.ServerClick += new EventHandler(ButtonPRChanged_ServerClick);
		}
		private DataTable ReadData()
		{
			string strEmpID = this.EmpHeaderSearch1.txtEmpID.Text.Trim().Replace("'","");
			string strEmpName = this.EmpHeaderSearch1.txtEmpName.Text.Trim();
			string strCompanyID = this.EmpHeaderSearch1.cboCompany.SelectedValue.Trim();
			string strLevel1ID = this.EmpHeaderSearch1.cboLevel1.SelectedValue.Trim();
			string strLevel2ID = this.EmpHeaderSearch1.cboLevel2.SelectedValue.Trim();
			string strLevel3ID = this.EmpHeaderSearch1.cboLevel3.SelectedValue.Trim();						
			string strLSEmpTypeID = this.EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue.Trim();
			string strStatus = this.EmpHeaderSearch1.optStatus.SelectedValue.Trim();

			string strMM = this.txtMM.Text.Trim();
			string strYYYY = this.txtYYYY.Text.Trim();
			
			string strSQL = "PR_spfrmAnnivesaryBonus @Activity = 'SearchEmp', @LanguageID='"+strLanguage+"'";
			strSQL +=", @EmpID ='" + strEmpID + "', @EmpName ='" + strEmpName +	"', @LSCompanyID = '" + strCompanyID + "', @LSLevel1ID = '" + strLevel1ID +	"', @LSLevel2ID = '" + strLevel2ID + "', @LSLevel3ID = '" + strLevel3ID + "', @LSEmpTypeID = '" + strLSEmpTypeID + "',@Status = '" + strStatus+"'";
			strSQL +=", @MM = '" + strMM +"', @YYYY='"+strYYYY+"'";


			try
			{
				DataTable dtList = new DataTable();
				dtList = clsCommon.GetDataTable(strSQL);									

				return dtList;
			}
			catch(Exception ex)
			{
				return null;
			}
		}

		private void dtgList_SortCommand(object source, DataGridSortCommandEventArgs e)
		{
			try
			{
				DataGridSort.AdvancedMultiSort(dtgList, ReadData(), uctrlColumns);
			}
			catch(Exception ex)

			{
				//clsChangeLang.popupWindow(this.Parent, "Sort Error", "", 1);
				clsChangeLang.popupWindowExp_Mess(this.Parent,ex);
			}
			
			try
			{
				DataTable dtList = this.ReadData();
				DataGridSort.Grid_Sort(dtgList, dtList, e, this.uctrlColumns);
				
			}
	
			catch (Exception ex) 
			{
				//clsChangeLang.popupWindow(this.Parent, "Sort Error", "", 1);
			}
			
		}

		private void dtgList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.dtgList.CurrentPageIndex = e.NewPageIndex;
				
			try
			{
				DataTable dtList = this.ReadData();
				DataGridSort.Grid_IndexChanged(dtgList, dtList, e);
			
			}
			catch
			{
			}
		}

		private void ButtonPRChanged_ServerClick(object sender, EventArgs e)
		{
			try
			{
				dtgList.PageSize = uctrlColumns.iPageRows;
				dtgList.CurrentPageIndex = 0;
				DataView dv = new DataView(ReadData());
				//dv.Sort = DataGridSort.sMulSort;
				dtgList.DataSource = dv;
				dtgList.DataBind();
				
				
			}
			catch(Exception ex)
			{
				clsChangeLang.popupWindowExp_Mess(this.Parent,ex);
			}
			
		}

		private void ButtonSort_ServerClick(object sender, EventArgs e)
		{
			try
			{
				DataTable dtList = this.ReadData();
				DataGridSort.AdvancedMultiSort(dtgList, dtList, uctrlColumns);
		
			}
			catch
			{
				clsChangeLang.popupWindow(this.Parent, "Sort Error", "", 1);
			}
		}
		#endregion


	}
}
