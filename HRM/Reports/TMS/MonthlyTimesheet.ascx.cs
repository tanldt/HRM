namespace iHRPCore.MdlTMS
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Data.SqlClient;
	using System.Configuration;
	using iHRPCore.Com;
	using iHRPCore.TMSComponent;
	using iHRPCore.Include;
	using GridSort;

	/// <summary>
	///		Summary description for LeaveRecordList.
	/// </summary>
	public class RPT_MonthlyTimesheet : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.TextBox txtFromDate;
		protected System.Web.UI.WebControls.TextBox txtToDate;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.Label lblLeaveType;
		protected System.Web.UI.WebControls.DropDownList cboLSLeaveTypeCode;
		protected System.Web.UI.WebControls.LinkButton btnSearch;
		protected System.Web.UI.WebControls.Label lbl;
		protected System.Web.UI.WebControls.DataGrid dtgList_Approval;
		protected EmpHeaderSearch EmpHeaderSearch1;
		protected ColumnList uctrlColumns;
		protected System.Web.UI.WebControls.RadioButtonList optLeave;
		protected System.Web.UI.WebControls.LinkButton Linkbutton1;
		protected System.Web.UI.WebControls.LinkButton Linkbutton2;
		protected System.Web.UI.WebControls.LinkButton btnImport;
		public string strLanguage = "EN";

		private void Page_Load(object sender, System.EventArgs e)
		{
			
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
			//set ngay 
			txtFromDate.Text="01-Jan-" + System.DateTime.Now.Year.ToString();
			txtToDate.Text="31-Dec-" + System.DateTime.Now.Year.ToString();//System.DateTime.Now.ToString("dd/MM/yyyy");
			///////////////////////////////
			if(!Page.IsPostBack)
			{
				LoadComboBox();
				

				//Hau
				DataGridSort.AddItemColumn(uctrlColumns, dtgList_Approval);//
				btnSearch_Click(null, null);
			}
			this.btnSearch.Attributes.Add("OnClick","return CheckValid()");
			btnImport.Attributes.Add("onclick","return ShowExcelSelectPage();");
			lblErr.Text = "";
			Session["EmpID"]=null;

			//Hau
			ButtonClick();//
		}

		private void LoadComboBox()
		{
			clsCommon.LoadDropDownListControl(cboLSLeaveTypeCode,"sp_GetDataCombo @TableName='LS_tblWorkPoint', @Fields='LSWorkPointID as [ID], Name'", "ID", "Name",true);			
		}
		private string GetEmpHeaderCondition()
		{
			string strEmpID = EmpHeaderSearch1.txtEmpID.Text;
			string strEmpName = EmpHeaderSearch1.txtEmpName.Text;
			string strCompany = EmpHeaderSearch1.cboCompany.SelectedValue;
			string strLevel1 = EmpHeaderSearch1.cboLevel1.SelectedValue;
			string strLevel2 = EmpHeaderSearch1.cboLevel2.SelectedValue;
			string strSource = "";//EmpHeaderSearch1.cboLSRESourceID.SelectedValue;
			string strStatus = EmpHeaderSearch1.optStatus.SelectedValue=="2"?"":EmpHeaderSearch1.optStatus.SelectedValue;
			string strGender = "";//EmpHeaderSearch1.cboGender.SelectedValue.Trim();
			string strLocalExpat = "";//EmpHeaderSearch1.cboLocalExpat.SelectedValue.Trim();
			string strShortName = "";//EmpHeaderSearch1.txtShortName.Text.Trim();
			string sLSEmpTypeID = EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue.Trim();

			
			string strCondition = "(E.EmpCode = ''" + strEmpID + "'' or ''" + strEmpID + "'' = '''')"
				+ " and (E.EmpName like N''%" + strEmpName + "%'' or ''" + strEmpName + "'' = '''')"
				+ " and (E.LSCompanyID = ''" + strCompany + "'' or ''" + strCompany + "'' = '''')"
				+ " and (E.LSLevel1ID = ''" + strLevel1 + "'' or ''" + strLevel1 + "'' = '''')"
				+ " and (E.LSLevel2ID = ''" + strLevel2 + "'' or ''" + strLevel2 + "'' = '''')"
				+ " and (E.LSRESourceID = ''" + strSource + "'' or ''" + strSource + "'' = '''')"
				+ " and (E.LSEmpTypeID = ''" + sLSEmpTypeID + "'' or ''" + sLSEmpTypeID + "'' = '''')"
				+ " and (E.Status=''" + strStatus + "'' or ''" + strStatus + "''='''')"
				+ " and (E.Gender=''" + strGender + "'' or ''" + strGender + "''='''')"
				+ " and (E.LocalExpat=''" + strLocalExpat + "'' or ''" + strLocalExpat + "''='''')"
				+ " and (E.NickName=''" + strShortName + "'' or ''" + strShortName + "''='''')";			
			return strCondition;
		}
		private void BindDataGrid(int type)
		{
			DataTable dtb = new DataTable();
			if (type == 1)
			{
				string sCondition = GetEmpHeaderCondition();
				string strSQL = "TS_spfrmLeaveRecord @Activity='Search', @FromDate = '" + txtFromDate.Text + "', @ToDate = '" + txtToDate.Text + 
					"', @LSWorkPointID = '" + cboLSLeaveTypeCode.SelectedValue + "', @StatusRequest = '" + optLeave.SelectedValue.ToString() + "', @Condition = N'" + sCondition + "'";
				dtb = clsCommon.GetDataTable(strSQL);				
			}
			
			if (dtb != null)
			{
				dtgList_Approval.CurrentPageIndex=0;
				this.dtgList_Approval.DataSource = dtb;
				this.dtgList_Approval.DataBind();
				
				//Hau
				this.dtgList_Approval.PageSize = uctrlColumns.iPageRows;
				
				if(this.uctrlColumns.bAdvMultiSort)
				{
					try
					{
						DataGridSort.AdvancedMultiSort(dtgList_Approval, dtb, uctrlColumns);
					}
					catch
					{
						this.dtgList_Approval.CurrentPageIndex = 0;
						DataGridSort.AdvancedMultiSort(dtgList_Approval, dtb, uctrlColumns);
					}
				}
				else
				{
					try
					{
						DataGridSort.Refresh(dtgList_Approval, dtb);
					}
					catch
					{
						this.dtgList_Approval.CurrentPageIndex = 0;
						DataGridSort.Refresh(dtgList_Approval, dtb);
					}
				}
				//					dtgList.DataBind();
				this.uctrlColumns.iTotalRows = dtb.Rows.Count;//				
			}
		}
		private void LoadDataCombo()
		{
			clsCommon.LoadDropDownListControl(cboLSLeaveTypeCode,"sp_GetDataCombo @TableName='LS_tblWorkPoint', @Fields='LSWorkPointID as [ID], Name',@Where=' and LSWorkPointID not in (''B'',''H'') and isnull(IsEdited,0)=0'", "ID", "Name",true);			
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
			this.Linkbutton2.Click += new System.EventHandler(this.Linkbutton2_Click);
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			this.dtgList_Approval.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgList_Approval_ItemCommand);
			this.dtgList_Approval.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgList_Approval_PageIndexChanged);
			this.dtgList_Approval.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dtgList_Approval_SortCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			BindDataGrid(1);
		}		

		private void btnExport_Click(object sender, System.EventArgs e)
		{
			iHRPCore.DataGridExcelExporter myExcelXport=new DataGridExcelExporter(dtgList_Approval);			
			myExcelXport.Export();
		}	
		private void RedirectToLeaveApp(string strParams,string sFunctionID)
		{
			string strFunctionID = sFunctionID;	// Leave application 123489
			DataTable dt = clsCommon.GetDataTable("sp_clsCommon 'GetByFunctionID', @FunctionID='" + strFunctionID + "'");
			if ((dt != null) && (dt.Rows.Count > 0))
			{
				string strUrl = dt.Rows[0]["Url"].ToString() + "?ModuleID=" + dt.Rows[0]["ModuleID"].ToString() 
					+ "&ParentID=" + dt.Rows[0]["Parent"].ToString() + "&FunctionID=" 
					+ dt.Rows[0]["FunctionID"].ToString() + "&Ascx=" + dt.Rows[0]["Ascx"].ToString()
					+ strParams;

				Response.Redirect(strUrl);
			}		
		}
		private void dtgList_Approval_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			if (e.CommandName == "EDIT")
			{
				//Session["EmpID_Request"]=e.Item.Cells[1].Text;
				///Editpage.aspx?ModuleID=TMS&FunctionID=108&ParentID=107&Ascx=MdlTMS/TMS_LeaveRecord.ascx
				Session["EmpID"]=e.Item.Cells[1].Text;
				Session["LeaveRecordID"]=e.Item.Cells[0].Text;
				this.Response.Redirect(@"Editpage.aspx?ModuleID=TMS&FunctionID=108&ParentID=107&Ascx=MdlTMS/TMS_LeaveRecord.ascx");
				string strParams = "&TypeLog=2&ER=" + e.Item.Cells[1].Text + "&LR=" + e.Item.Cells[0].Text + "&Status=" + e.Item.Cells[2].Text + " &LM=" + e.Item.Cells[3].Text;				
			}
			else if (e.CommandName.ToUpper()=="DETAILS")
			{
				string strFromDate=e.Item.Cells[9].Text;
				string strToDate=e.Item.Cells[10].Text;
				strFromDate=clsCommon.ConvertDDMMYYYYtoDMY(strFromDate);
				strToDate=clsCommon.ConvertDDMMYYYYtoDMY(strToDate);
				string strLeaveRecordID=e.Item.Cells[0].Text;
				string strWorkPointID=e.Item.Cells[15].Text;
				string Prefix="";
				string strYear=e.Item.Cells[9].Text.Substring(7,4);
				string strEmpID=e.Item.Cells[1].Text ;
				string strStatus="1";
				string strUrl = "FormPage.aspx?Ascx=MdlTMS/TMS_LeaveRecordDetails.ascx" + "&FromDate=" + strFromDate + "&ToDate=" + strToDate 
					+ "&LeaveRecordID=" + strLeaveRecordID + "&LSWorkPointID=" + strWorkPointID + "&Prefix=" + Prefix + "&Year=" + strYear + "&EmpID=" + strEmpID + "&status=" + strStatus;
				clsCommon.OpenNewWindowPopup(this.Page,strUrl,"");
				
			}
		}
		private void dtgList_Approval_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			BindDataGrid(1);
			this.dtgList_Approval.CurrentPageIndex = e.NewPageIndex;
			this.dtgList_Approval.DataBind();
		}

		#region Cac su kien xu li thao tac Sort (Hau)

		private void dtgList_Approval_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			try
			{
				DataGridSort.Grid_Sort(dtgList_Approval, Read_Data(), e, uctrlColumns);
			}
			catch{}
		}

		private DataTable Read_Data()
		{
			string sCondition = GetEmpHeaderCondition();
			string strSQL = "TS_spfrmLeaveRecord @Activity='Search', @FromDate = '" + txtFromDate.Text + "', @ToDate = '" + txtToDate.Text + 
				"', @LSWorkPointID = '" + cboLSLeaveTypeCode.SelectedValue + "', @StatusRequest = '" + optLeave.SelectedValue.ToString() + "', @Condition = '" + sCondition + "'";
			DataTable dtb = clsCommon.GetDataTable(strSQL);
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
				DataGridSort.AdvancedMultiSort(dtgList_Approval, Read_Data(), uctrlColumns);
			}
			catch{}
		}

		private void ButtonPRChanged_ServerClick(object sender, EventArgs e)
		{
			try
			{
				dtgList_Approval.PageSize = uctrlColumns.iPageRows;
				dtgList_Approval.CurrentPageIndex = 0;
				DataView dv = new DataView(Read_Data());
				dv.Sort = DataGridSort.sMulSort;
				dtgList_Approval.DataSource = dv;
				dtgList_Approval.DataBind();
			}
			catch{}
		}
		#endregion		

		private void Linkbutton2_Click(object sender, System.EventArgs e)
		{
			string strListLeaveRecordID="@";
			for(int i=0;i< dtgList_Approval.Items.Count;i++)
			{
				if( ((CheckBox)dtgList_Approval.Items[i].FindControl("chkSelect")).Checked==true)
				{
					strListLeaveRecordID+=dtgList_Approval.Items[i].Cells[0].Text.Trim()+"@";
				}
			}
			//this.txtStrListID.Text=strListID;
			
			string result="";
			try
			{	
				string sSQL = "TS_spfrmLeaveRecord @Activity='DeleteList',@strListLeaveRecordID='"+strListLeaveRecordID+"'";
				clsCommon.Exc_CommandText(sSQL);					
				BindDataGrid(1);								
				Session["ssStatusWorking"] = "AddNew";
				clsChangeLang.popupWindow(this.Parent,"0104","EN","",1);
			}
			catch  (Exception exp)
			{
				clsChangeLang.popupWindow(this.Parent,"0048","EN","",0);
				return;
			}			
			
		}
	}
}
