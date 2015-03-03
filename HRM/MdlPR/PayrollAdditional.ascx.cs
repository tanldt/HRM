namespace iHRPCore.MdlPR
{	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.Com;
	using iHRPCore.HRComponent;
	using iHRPCore.TMSComponent;
	using iHRPCore.PRComponent;	
	using iHRPCore.Include;	
	using GridSort;

	/// <summary>
	///		Summary description for PayrollCollection.
	/// </summary>
	public class PayrollAdditional : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.LinkButton btnCalulate;
		protected System.Web.UI.WebControls.LinkButton btnView;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.LinkButton btnLock;
		protected System.Web.UI.WebControls.LinkButton btnPrint;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel1ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel2ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel3ID;
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.TextBox txtFromDate;
		protected System.Web.UI.WebControls.TextBox txtToDate;
		protected System.Web.UI.WebControls.DataGrid grdPayRoll;	
		protected ColumnList uctrlColumns;
		protected System.Web.UI.WebControls.TextBox txtPageLoad;
		protected System.Web.UI.HtmlControls.HtmlTableRow trColumnList;
		protected System.Web.UI.WebControls.TextBox txtCoef;
		protected System.Web.UI.WebControls.RadioButtonList optCollection;
		protected System.Web.UI.HtmlControls.HtmlTableRow trEmp;
		protected System.Web.UI.HtmlControls.HtmlTableRow trListEmp;
		
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList cboCompanyID;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.DropDownList cboLevel1ID;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.DropDownList cboLevel2ID;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.DropDownList cboLevel3ID;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.TextBox txtYear;
		protected System.Web.UI.WebControls.DropDownList cboPayrollAdditionalType;
		protected System.Web.UI.WebControls.TextBox txtMonth;
		protected System.Web.UI.WebControls.TextBox txtCoefAdditional;
		protected EmpHeader HR_EmpHeader;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		public string strLanguage="VN";
		private void Page_Load(object sender, System.EventArgs e)
		{
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
			// Put user code to initialize the page here	
			if (!Page.IsPostBack)
			{
				LoadCombobox();
				//if (Request.Params["tabid"] != null)
				if (Convert.ToInt32(optCollection.SelectedValue)==1 )
					trListEmp.Attributes.Add("style","display:none");
				else	
					trEmp.Attributes.Add("style","display:none");
			}
			if (cboCompanyID.Items.Count==2) cboCompanyID.SelectedIndex=1;
			cboCompanyID_SelectedIndexChanged(null,null);

			btnDelete.Attributes.Add("OnClick", "return checkdelete()");	
			btnCalulate.Attributes.Add("OnClick", "return Calculate()");	
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
			this.btnCalulate.Click += new System.EventHandler(this.btnCalulate_Click);
			this.btnView.Click += new System.EventHandler(this.btnView_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			this.grdPayRoll.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.grdPayRoll_PageIndexChanged);
			this.grdPayRoll.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.grdPayRoll_SortCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	
		/*private void DisplayRow(HtmlTableRow RowDisplay)
		{
			trEmp.Attributes.Add("style","display:none");
			trListEmp.Attributes.Add("style","display:none");
			RowDisplay.Attributes.Remove("style");
		}*/
		private void LoadCombobox()
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			clsHREmpList.LoadComboCompany(cboCompanyID, strTextField, strLanguage,this.Page);			
			clsHREmpList.LoadComboLevel2(cboLevel2ID, strTextField, strLanguage,this.Page);
		}

	
		public void cboCompanyID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			string strCompanyID = cboCompanyID.SelectedValue.Trim();			
			if (!strCompanyID.Equals(""))
			{
				clsHREmpList.LoadComboLevel1ByCompany(cboLevel1ID,strTextField,strCompanyID, strLanguage,this.Page); 
				cboLevel1ID.SelectedValue = this.txtLevel1ID.Value.Trim();
				cboLevel1ID_SelectedIndexChanged(null,null);
			}
			else
			{
				cboLevel1ID.Items.Clear();
			}
		}

		public void cboLevel1ID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			string strLevel1ID = cboLevel1ID.SelectedValue.Trim();
			if (!strLevel1ID.Equals(""))
			{
				clsHREmpList.LoadComboLevel2ByLevel1(cboLevel2ID,strTextField,strLevel1ID, strLanguage,this.Page); 
				cboLevel2ID.SelectedValue = this.txtLevel2ID.Value.Trim();
				cboLevel2ID_SelectedIndexChanged(null,null);
			}
			else
			{
				//cboLevel2.Items.Clear();
			}
		}

		public void cboLevel2ID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			string strLevel2ID = cboLevel2ID.SelectedValue.Trim();
			if (!strLevel2ID.Equals(""))
			{
				clsHREmpList.LoadComboLevel3ByLevel2(cboLevel3ID,strTextField,strLevel2ID, strLanguage); 
				cboLevel3ID.SelectedValue = this.txtLevel3ID.Value.Trim();
			}
			else
			{
				cboLevel3ID.Items.Clear();
			}
		}
		private void btnCalulate_Click(object sender, System.EventArgs e)
		{
			string sMonth = txtMonth.Text;
			string sYear = txtYear.Text;
			string sCoefAdditional= txtCoefAdditional.Text==""?"0":txtCoefAdditional.Text;
			string PayrollAdditionalType= cboPayrollAdditionalType.SelectedValue.Trim ();
			string sEmpID=HR_EmpHeader.txtEmpID.Text.Trim();//Session["EmpID"].ToString().Trim();
			string sEmpName=HR_EmpHeader.txtEmpName.Text.Trim();
			string strErr="";
			if (optCollection.SelectedValue == "1")
				 strErr=clsPRPayrollAdditional.CalculatePayrollAdditional(sEmpID,sEmpName,sMonth,sYear,sCoefAdditional,PayrollAdditionalType,"","","","",strLanguage);
			else
			{
				string strLevel1 = cboLevel1ID.SelectedValue.Trim();
				string strLevel2 = cboLevel2ID.SelectedValue.Trim();
				string strLevel3 = cboLevel3ID.SelectedValue.Trim();
				string strCompany = cboCompanyID.SelectedValue.Trim();

				strErr=clsPRPayrollAdditional.CalculatePayrollAdditional("","",sMonth,sYear,sCoefAdditional,PayrollAdditionalType,strCompany,strLevel1,strLevel2,strLevel3,strLanguage);
			}
			
			//string strErr=clsPRPayrollAdditional.CalculatePayrollAdditional(sEmpID,sMonth,sYear,Convert.ToInt32(sCoefAdditional),PayrollAdditionalType,cboCompanyID.SelectedValue,cboLevel1ID.SelectedValue,cboLevel2ID.SelectedValue,cboLevel3ID.SelectedValue,strLanguage);
			//clsCommon.ShowMessageBox(this.Page,lblErr.Text);
			clsChangeLang.popupWindow(this.Parent,strErr,"",1);
		}

		private void btnView_Click(object sender, System.EventArgs e)
		{	trColumnList.Style.Add("DISPLAY","block");		 
			BindDataGrid();
			lblErr.Text="";
		}
		private void btnExport_Click(object sender, System.EventArgs e)
		{
			grdPayRoll.AllowPaging = false;
			this.BindDataGrid();
			iHRPCore.DataGridExcelExporter myExcelXport=new DataGridExcelExporter(grdPayRoll);
			myExcelXport.Export("");
			myExcelXport = null;
			this.grdPayRoll.AllowPaging = true;
			this.BindDataGrid();
		}
		private void BindDataGrid()
		{	DataTable dtList = new DataTable();
			
			try
			{
				dtList = new DataTable();
				string sMonth = txtMonth.Text!=null? txtMonth.Text:"";
				string sYear = txtYear.Text!=null? txtYear.Text:"";
				string sCoefAdditional= txtCoefAdditional.Text !=null? txtCoefAdditional.Text:"0";
				string PayrollAdditionalType= cboPayrollAdditionalType.SelectedValue.Trim ();
				string sEmpID=HR_EmpHeader.txtEmpID.Text.Trim();//Session["EmpID"].ToString().Trim();
				string sEmpName=HR_EmpHeader.txtEmpName.Text.Trim();
				string strErr="";
				if (optCollection.SelectedValue == "1")
				{
					dtList = clsPRPayrollAdditional.GetDataSalary(sEmpID,sEmpName,sMonth,sYear,sCoefAdditional,PayrollAdditionalType,"","","","");
					grdPayRoll.DataSource = dtList;
					grdPayRoll.CurrentPageIndex = 0;
					grdPayRoll.DataBind();
					//strErr=clsPRPayrollAdditional.CalculatePayrollAdditional(sEmpID,sEmpName,sMonth,sYear,Convert.ToInt32(sCoefAdditional),PayrollAdditionalType,"","","","",strLanguage);
				}
				else
				{
					string strLevel1 = cboLevel1ID.SelectedValue.Trim();
					string strLevel2 = cboLevel2ID.SelectedValue.Trim();
					string strLevel3 = cboLevel3ID.SelectedValue.Trim();
					string strCompany = cboCompanyID.SelectedValue.Trim();

					dtList = clsPRPayrollAdditional.GetDataSalary("","",sMonth,sYear,sCoefAdditional,PayrollAdditionalType,strCompany,strLevel1,strLevel2,strLevel3);
					grdPayRoll.DataSource = dtList;
					grdPayRoll.CurrentPageIndex = 0;
					grdPayRoll.DataBind();
					//strErr=clsPRPayrollAdditional.CalculatePayrollAdditional("","",sMonth,sYear,Convert.ToInt32(sCoefAdditional),PayrollAdditionalType,strCompany,strLevel1,strLevel2,strLevel3,strLanguage);
				}

				//	dtList = clsPRPayrollAdditional.GetDataSalary(cboCompanyID.SelectedValue,cboLevel1ID.SelectedValue,cboLevel2ID.SelectedValue,cboLevel3ID.SelectedValue);
			
				

				
				//Hau
				this.grdPayRoll.PageSize = uctrlColumns.iPageRows;
				
				if(this.uctrlColumns.bAdvMultiSort)
				{
					try
					{
						DataGridSort.AdvancedMultiSort(grdPayRoll, dtList, uctrlColumns);
					}
					catch
					{
						this.grdPayRoll.CurrentPageIndex = 0;
						DataGridSort.AdvancedMultiSort(grdPayRoll, dtList, uctrlColumns);
					}
				}
				else
				{
					try
					{
						DataGridSort.Refresh(grdPayRoll, dtList);
					}
					catch
					{
						this.grdPayRoll.CurrentPageIndex = 0;
						DataGridSort.Refresh(grdPayRoll, dtList);
					}
				}
				//					dtgList.DataBind();
				this.uctrlColumns.iTotalRows = dtList.Rows.Count;//
			}
			catch(Exception ex)
			{
				clsChangeLang.popupWindow(this.Parent,"0034",strLanguage,ex.ToString(),1);
			}
			finally
			{
				dtList.Dispose();
			}
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				string strID="";
				for(int i=0;i<grdPayRoll.Items.Count;i++)
				{
					if(((CheckBox)grdPayRoll.Items[i].FindControl("chkSelect")).Checked==true)
					{
						strID += grdPayRoll.Items[i].Cells[1].Text.Trim() + "$";
					}
				}
				clsCommon.DeleteListRecord("PR_spfrmPayrollAdditional","PayrollAdditionalID",SqlDbType.NVarChar,12,strID);				
				btnView_Click(null,null);
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}

		
		private void grdPayRoll_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			try
			{
				BindDataGrid();
				grdPayRoll.CurrentPageIndex=e.NewPageIndex;
				grdPayRoll.DataBind();
				//Hau
				try
				{
					DataGridSort.Grid_IndexChanged(grdPayRoll, Read_Data(), e);
				}
				catch{}//
			}
			catch(Exception ex)
			{
				string str = ex.Message;
			}
		}
		
		#region Cac su kien xu li thao tac Sort (Hau)

		private void grdPayRoll_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			try
			{
				DataGridSort.Grid_Sort(grdPayRoll, Read_Data(), e, uctrlColumns);
			}
			catch{}
		}

		private DataTable Read_Data()
		{
			int intTopRow =0;
			if ( txtPageLoad.Text == "1" )
			{
				intTopRow = grdPayRoll.PageSize;
				txtPageLoad.Text = "0";
			}
			

			DataTable dtList = new DataTable();
			string sMonth = txtMonth.Text!=null? txtMonth.Text:"";
			string sYear = txtYear.Text!=null? txtYear.Text:"";
			string sCoefAdditional= txtCoefAdditional.Text !=null? txtCoefAdditional.Text:"0";
			string PayrollAdditionalType= cboPayrollAdditionalType.SelectedValue.Trim ();
			string sEmpID=HR_EmpHeader.txtEmpID.Text.Trim();//Session["EmpID"].ToString().Trim();
			string sEmpName=HR_EmpHeader.txtEmpName.Text.Trim();
			string strErr="";
			if (optCollection.SelectedValue == "1")
			{
				dtList = clsPRPayrollAdditional.GetDataSalary(sEmpID,sEmpName,sMonth,sYear,sCoefAdditional,PayrollAdditionalType,"","","","");

				//strErr=clsPRPayrollAdditional.CalculatePayrollAdditional(sEmpID,sEmpName,sMonth,sYear,Convert.ToInt32(sCoefAdditional),PayrollAdditionalType,"","","","",strLanguage);
			}
			else
			{
				string strLevel1 = cboLevel1ID.SelectedValue.Trim();
				string strLevel2 = cboLevel2ID.SelectedValue.Trim();
				string strLevel3 = cboLevel3ID.SelectedValue.Trim();
				string strCompany = cboCompanyID.SelectedValue.Trim();

				dtList = clsPRPayrollAdditional.GetDataSalary("","",sMonth,sYear,sCoefAdditional,PayrollAdditionalType,strCompany,strLevel1,strLevel2,strLevel3);

				//strErr=clsPRPayrollAdditional.CalculatePayrollAdditional("","",sMonth,sYear,Convert.ToInt32(sCoefAdditional),PayrollAdditionalType,strCompany,strLevel1,strLevel2,strLevel3,strLanguage);
			}

			//DataTable dtb = clsPRCollection.GetDataSalary(cboCompanyID.SelectedValue,cboLevel1ID.SelectedValue,cboLevel2ID.SelectedValue,cboLevel3ID.SelectedValue);
			return dtList;
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
				DataGridSort.AdvancedMultiSort(grdPayRoll, Read_Data(), uctrlColumns);
			}
			catch{}
		}

		private void ButtonPRChanged_ServerClick(object sender, EventArgs e)
		{
			try
			{
				grdPayRoll.PageSize = uctrlColumns.iPageRows;
				grdPayRoll.CurrentPageIndex = 0;
				DataView dv = new DataView(Read_Data());
				dv.Sort = DataGridSort.sMulSort;
				grdPayRoll.DataSource = dv;
				grdPayRoll.DataBind();
			}
			catch{}
		}
		#endregion		

		private void btnPrint_Click(object sender, System.EventArgs e)
		{
		
		}

		

		
		
	}
}
