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
	///		Summary description for MailTemplate.
	/// </summary>
	public class MailTemplate : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblCode;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.HtmlControls.HtmlTableRow trLevelParent;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Title;
		protected System.Web.UI.WebControls.RadioButtonList optStaff;
		protected System.Web.UI.WebControls.RadioButtonList optSupervisor;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnAddnew;
		protected System.Web.UI.WebControls.DataGrid dtgList;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.RadioButtonList Radiobuttonlist3;
		protected System.Web.UI.WebControls.Label lblDescription;
		string strLanguage="EN";
		protected System.Web.UI.WebControls.TextBox txtTemplateID;
		protected System.Web.UI.WebControls.TextBox txtTitle;
		protected ExportTechnologies.WebControls.RTE.RichTextEditor txtContent;
		protected System.Web.UI.WebControls.RadioButtonList optLineManager;
		protected System.Web.UI.WebControls.RadioButtonList optHR;
		protected ColumnList uctrlColumns;

		private void Page_Load(object sender, System.EventArgs e)
		{
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";	
			if(!Page.IsPostBack)
			{
						
				LoadDataGrid();
				DataGridSort.AddItemColumn(uctrlColumns, dtgList);//
			}
			btnSave.Attributes.Add("OnClick", " return validform()");			
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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnAddnew.Click += new System.EventHandler(this.btnAddnew_Click);
			this.dtgList.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgList_ItemCommand);
			this.dtgList.PageIndexChanged+=new DataGridPageChangedEventHandler(dtgList_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		private void LoadDataGrid()
		{
			DataTable dtData= new DataTable();
			try
			{	
			dtData=clsCommon.GetDataTable("LS_spfrmMAILTEMPLATE @Activity='LoadData'");
			dtgList.DataSource=dtData;
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
			//					dtgList.DataBind();
			this.uctrlColumns.iTotalRows = dtData.Rows.Count;//
			}
				catch(Exception ex)
			{
				clsChangeLang.popupWindowExp_Mess(this.Page,ex);
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

			int intTopRow =0;
			

			DataTable dtb = clsCommon.GetDataTable("LS_spfrmMAILTEMPLATE @Activity='LoadData'");
			return dtb;
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

		private void btnAddnew_Click(object sender, System.EventArgs e)
		{
			clsCommon.ClearControlValue(this);
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
					string strErr =clsCommon.sImpactDB("Update",this,"LS_spfrmMAILTEMPLATE");
					if(strErr == "")
					{
						btnAddnew_Click(null,null);
						LoadDataGrid();
						clsChangeLang.popupWindow(this.Parent,"0044", strLanguage, "", 1);
					}
					else
						clsChangeLang.popupWindow(this.Parent,"0092", strLanguage, "", 0);				
				
			}
			catch(Exception ex)
			{
				clsChangeLang.popupWindowExp_Mess(this.Parent,ex);
			}		
		}

		private void dtgList_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				if(e.CommandName.Trim().ToUpper() =="EDIT")
				{
					txtTemplateID.Text = dtgList.Items[e.Item.ItemIndex].Cells[0].Text.Trim();
					//txtEmpID.Text = grdWorkingBackground.Items[e.Item.ItemIndex].Cells[1].Text.Trim();
					DataRow iRow = clsCommon.GetDataRow("LS_spfrmMAILTEMPLATE @Activity='GetDataByID',@TemplateID='" + txtTemplateID.Text + "'");
					if(iRow != null)
					{
						txtContent.Text= iRow["Content"].ToString().Trim();
						txtTitle.Text= iRow["Title"].ToString().Trim();
						lblDescription.Text=iRow["Description"].ToString().Trim();
						optStaff.SelectedValue=iRow["Staff"].ToString().Trim();
						optSupervisor.SelectedValue=iRow["Supervisor"].ToString().Trim();
						optLineManager.SelectedValue=iRow["LineManager"].ToString().Trim();
						optHR.SelectedValue=iRow["HR"].ToString().Trim();						
					}

				}								
			}
			catch(Exception ex)
			{
				clsChangeLang.popupWindowExp_Mess(this.Parent,ex);
			}
		}

		private void dtgList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.LoadDataGrid();
			this.dtgList.CurrentPageIndex = e.NewPageIndex;
			this.dtgList.DataBind();
			
			//Hau
			try
			{
				DataGridSort.Grid_IndexChanged(dtgList, Read_Data(), e);
			}
			catch{}//
		}
	}
}
