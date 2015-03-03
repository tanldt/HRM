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
	public class BonusList : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label Label10;		
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.LinkButton btnSearch;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.TextBox txtFromDate;
		protected System.Web.UI.WebControls.TextBox txtToDate;		
		protected string strLanguage = "EN";
		protected System.Web.UI.WebControls.TextBox txtPRMonth;
		protected System.Web.UI.WebControls.DataGrid grdBonus;
		protected EmpHeaderSearch EmpHeaderSearch1;
		protected System.Web.UI.WebControls.DropDownList cboLSBonusID;
		protected System.Web.UI.WebControls.DropDownList cboCol1;
		protected System.Web.UI.WebControls.DropDownList cboCol2;
		protected System.Web.UI.WebControls.DropDownList cboCol3;
		protected System.Web.UI.WebControls.HyperLink HyperLink1;
		protected ColumnList uctrlColumns;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
			if(!Page.IsPostBack)
			{
				strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
				string strTextField = strLanguage == "VN"?"VNName":"Name";
				clsCommon.LoadDropDownListControl(cboLSBonusID,"sp_GetDataCombo @TableName='LS_tblBonus',@Fields='LSBonusID as [ID]," + strTextField + " as Name'","ID","Name",true);
				grdBonus.DataSource = new DataTable();
				grdBonus.DataBind();

				//Hau
				DataGridSort.AddItemColumn(uctrlColumns, grdBonus);//
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
			this.grdBonus.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.grdBonus_ItemCommand);
			this.grdBonus.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.grdBonus_PageIndexChanged);
			this.grdBonus.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.grdBonus_SortCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			LoadDataResult();
		}
		private void LoadDataResult()
		{
			DataTable dt = new DataTable();
			try
			{
				string strEmpID = EmpHeaderSearch1.txtEmpID.Text.Trim().Replace("'","");;
				string strEmpName = EmpHeaderSearch1.txtEmpName.Text.Trim().Replace("'","");;
				string strLevel1 = EmpHeaderSearch1.cboLevel1.SelectedValue.Trim();
				string strLevel2 = EmpHeaderSearch1.cboLevel2.SelectedValue.Trim();
				string strLevel3 = EmpHeaderSearch1.cboLevel3.SelectedValue.Trim();
				string strLocation = EmpHeaderSearch1.cboLocation.SelectedValue.Trim();
				string strJobCode = EmpHeaderSearch1.cboJobcode.SelectedValue.Trim();
				string strPosition = EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue.Trim();
				string strStatus = EmpHeaderSearch1.optStatus.SelectedValue.Trim();
				string strCompanyID=EmpHeaderSearch1.cboCompany.SelectedValue.Trim();
				dt = clsPRBonus.GetListSearch(strEmpID, strEmpName,strCompanyID, strLevel1, strLevel2, strLevel3,strStatus,txtFromDate.Text,txtToDate.Text
					,cboLSBonusID.SelectedValue.Trim(),txtPRMonth.Text.Trim(),strLanguage, this.Page);
//				grdBonus.DataSource = dt;
//				grdBonus.DataBind();

				#region Hau

				this.grdBonus.PageSize = uctrlColumns.iPageRows;
				if(this.uctrlColumns.bAdvMultiSort)
				{
					try
					{
						DataGridSort.AdvancedMultiSort(grdBonus, dt, uctrlColumns);
					}
					catch
					{
						grdBonus.CurrentPageIndex = 0;
						DataGridSort.AdvancedMultiSort(grdBonus, dt, uctrlColumns);
					}
				}
				else
				{
					try
					{
						DataGridSort.Refresh(grdBonus, dt);
					}
					catch
					{
						grdBonus.CurrentPageIndex = 0;
						DataGridSort.Refresh(grdBonus, dt);
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
		private void grdBonus_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			
			try
			{
				LoadDataResult();
				grdBonus.CurrentPageIndex=e.NewPageIndex;
				grdBonus.DataBind();
			}
			catch(Exception ex)			
			{
				string str = ex.Message;
			}		

			//Hau
			try
			{
				DataGridSort.Grid_IndexChanged(grdBonus, Read_Data(), e);
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
			try
			{
				string strID="";
				for(int i=0;i<grdBonus.Items.Count;i++)
				{
					if(((CheckBox)grdBonus.Items[i].FindControl("chkSelect")).Checked==true)
					{
						strID += grdBonus.Items[i].Cells[0].Text.Trim() + "$";
					}
				}
				clsCommon.DeleteListRecord("PR_spfrmBONUS","Delete","BonusID",SqlDbType.NVarChar,12,strID);
				LoadDataResult();
				lblErr.Text = "";
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}

		private void grdBonus_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if(e.CommandName.Trim().ToUpper() =="EDIT")
			{
				Session["ssBonusID"] = grdBonus.Items[e.Item.ItemIndex].Cells[0].Text.Trim();
				Session["EmpID"] = grdBonus.Items[e.Item.ItemIndex].Cells[1].Text.Trim();
				Response.Redirect("EditPage.aspx?ModuleID=PR&ParentID=2452&Ascx=MdlPR/Bonus.ascx&FunctionID=2452");
			}
		}

		private void btnExport_Click(object sender, System.EventArgs e)
		{
			iHRPCore.DataGridExcelExporter myExcelXport=new DataGridExcelExporter(grdBonus);			
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
			dtb = clsPRBonus.GetListSearch(strEmpID, strEmpName,strCompanyID, strLevel1, strLevel2, strLevel3,strStatus,txtFromDate.Text,txtToDate.Text
				,cboLSBonusID.SelectedValue.Trim(),txtPRMonth.Text.Trim(),strLanguage, this.Page);
			return dtb;
		}

		private void grdBonus_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			try
			{
				DataGridSort.Grid_Sort(grdBonus, Read_Data(), e, uctrlColumns);
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
				DataGridSort.AdvancedMultiSort(grdBonus, Read_Data(), uctrlColumns);
			}
			catch{}
		}

		private void ButtonPRChanged_ServerClick(object sender, EventArgs e)
		{
			try
			{
				grdBonus.PageSize = uctrlColumns.iPageRows;
				grdBonus.CurrentPageIndex = 0;
				DataView dv = new DataView(Read_Data());
				dv.Sort = DataGridSort.sMulSort;
				grdBonus.DataSource = dv;
				grdBonus.DataBind();
			}
			catch{}
		}
		#endregion
	}
}
