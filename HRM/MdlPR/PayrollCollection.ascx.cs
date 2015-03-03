namespace iHRPCore
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
	public class PayrollCollection : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtMonth;
		protected System.Web.UI.WebControls.LinkButton btnView;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.LinkButton btnLock;
		protected System.Web.UI.WebControls.LinkButton btnPrint;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel1ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel2ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel3ID;
		protected System.Web.UI.WebControls.TextBox txtSalPeriod;
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.TextBox txtFromDate;
		protected System.Web.UI.WebControls.TextBox txtToDate;
		protected System.Web.UI.WebControls.DataGrid grdPayRoll;	
		protected ColumnList uctrlColumns;
		protected System.Web.UI.WebControls.TextBox txtPageLoad;
		protected System.Web.UI.HtmlControls.HtmlTableRow trColumnList;
		public string strLanguage="VN", strUserGroupID = "";
		protected EmpHeaderSearch EmpHeaderSearch1;
		protected System.Web.UI.WebControls.LinkButton btnCalulate;
		clsEmpHeaderSearch EHSearch = new clsEmpHeaderSearch();

		private void Page_Load(object sender, System.EventArgs e)
		{
			Ajax.Utility.RegisterTypeForAjax(typeof(PayrollCollection)); // Su dung Ajax
			strLanguage = Mession.GlbLangID;
			strUserGroupID=Mession.GlbUser;
			// Put user code to initialize the page here	
			if (!Page.IsPostBack)
			{
				txtMonth.Text=DateTime.Today.ToString("dd/MM/yyyy").Substring(3,7);
				DataGridSort.AddItemColumn(uctrlColumns, grdPayRoll);//
			}
			btnDelete.Attributes.Add("OnClick", "return checkdelete()");	
			btnCalulate.Attributes.Add("OnClick", "return Calculate()");	
			btnLock.Attributes.Add("OnClick", "return Lock()");	
			btnPrint.Attributes.Add("onclick","return ShowPage();");
			ButtonClick();//
//			DataRow dr = clsCommon.GetDataRow("PR_spfrmPAYROLL @Activity = 'CheckPayrollExists'");
//			if (dr == null)
//			{
//				this.txtMonth.Text = DateTime.Today.ToString("dd/MM/yyyy").Substring(3,7);
//			}
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
			this.btnView.Click += new System.EventHandler(this.btnView_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnLock.Click += new System.EventHandler(this.btnLock_Click);
			this.grdPayRoll.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.grdPayRoll_PageIndexChanged);
			this.grdPayRoll.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.grdPayRoll_SortCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region AjaxMethod
		[Ajax.AjaxMethod]
		public bool CheckPayrollExists( string sMMYYYY)
		{
			//dt= clsDB.GetDataTable("PR_spfrmPAYROLL @Activity = 'CheckPayrollExists', @MMYYYY = '"+sMMYYYY+"'");
			DataRow dr = clsCommon.GetDataRow("PR_spfrmPAYROLL @Activity = 'CheckPayrollExists', @MMYYYY = '"+sMMYYYY+"'");
			if (dr != null)
			{
				if (dr["Result"].ToString() == "Yes")
					return true;
				else
					return false;
			}
			else
            	return false;
			
			
		}
		[Ajax.AjaxMethod]
		public DataTable GetDataFormula()
		{
			DataTable dt = new DataTable();
			try
			{
				dt= clsDB.GetDataTable("select * from PR_tblSetFormula");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}	
		}
		[Ajax.AjaxMethod]
		public DataTable GetData(string sMonth, string LSCompanyID, string LSLevel1ID, string LSLevel2ID
			, string LSLevel3ID, string EmpID, string EmpName, string sLanguageID, string sAccountID)
		{
			DataTable dt = new DataTable();
			try
			{
				dt= clsDB.GetDataTable("PR_spPayrollCollection 'GetDataSalary',@LSCompanyID='" + LSCompanyID 
					+ "',@LSLevel1ID='" + LSLevel1ID 
					+ "',@LSLevel2ID='" + LSLevel2ID 
					+ "',@LSLevel3ID='" + LSLevel3ID 
					+ "',@EmpCode='" + EmpID 
					+ "',@EmpName='" + EmpName 
					+ "',@UserGroupID='" + sAccountID 
					+ "',@Month='" + sMonth 
					+ "'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}	
		}
		[Ajax.AjaxMethod]
		public string CalPayroll(string sMonth, string EmpID, string sAccountID)
		{
			//System.Threading.Thread.Sleep(5000); 
			string str = "";
			try
			{
				str = clsPayroll.CalculateSalary(sMonth,EmpID,sAccountID);
				if (str == "")
					str = "Successful";
				return str;
			}
			catch(Exception ex)
			{
				str = ex.Message;
				return str;
			}

		}
		[Ajax.AjaxMethod]
		public string Calulate(string sMonth, string LSCompanyID, string LSLevel1ID, string LSLevel2ID
			, string LSLevel3ID, string EmpID, string EmpName, string sLanguageID, string sAccountID, string SetFormuleID)
		{
			string sErr = "";
			try
			{
				sErr = clsPayroll.CalculateSalary(sMonth,LSCompanyID,LSLevel1ID,LSLevel2ID
					,LSLevel3ID,EmpID, EmpName,sLanguageID,sAccountID,SetFormuleID);

			}
			catch(Exception ex)
			{
				sErr = ex.Message;
			}
			return sErr;
		}
		[Ajax.AjaxMethod]
		public string CalulateItemSys(string sMonth, string LSCompanyID, string LSLevel1ID, string LSLevel2ID
			, string LSLevel3ID, string EmpID, string EmpName, string sLanguageID, string sAccountID)
		{
			string sErr = "";
			try
			{
				sErr = clsPayroll.CalulateItemSYS(sMonth,LSCompanyID,LSLevel1ID,LSLevel2ID
					,LSLevel3ID,EmpID, EmpName,sLanguageID,sAccountID);

			}
			catch(Exception ex)
			{
				sErr = ex.Message;
			}
			return sErr;
		}
		[Ajax.AjaxMethod]
		public string CalulateItemUser(string sMonth, string LSCompanyID, string LSLevel1ID, string LSLevel2ID
			, string LSLevel3ID, string EmpID, string EmpName, string sLanguageID, string sAccountID, string SetFormuleID)
		{
			string sErr = "";
			try
			{
				sErr = clsPayroll.CalulateItemUser(sMonth,LSCompanyID,LSLevel1ID,LSLevel2ID
					,LSLevel3ID,EmpID, EmpName,sLanguageID,sAccountID,SetFormuleID);

			}
			catch(Exception ex)
			{
				sErr = ex.Message;
			}
			return sErr;
		}
		[Ajax.AjaxMethod]
		public DataTable GetComputationSeq(string sMonth)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsPayroll.GetComputationSeq(sMonth);

			}
			catch//(Exception ex)
			{
				dt = null;
			}
			return dt;
		}
		[Ajax.AjaxMethod]
		public DataTable GetSalaryItemUser(string sMonth, string LSCompanyID, string LSLevel1ID, string LSLevel2ID
			, string LSLevel3ID, string EmpID, string EmpName, string sLanguageID, string sAccountID, string SetFormuleID, string sComputationSeq)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsPayroll.GetSalaryItemUser(sMonth,LSCompanyID,LSLevel1ID,LSLevel2ID
					,LSLevel3ID,EmpID, EmpName,sLanguageID,sAccountID,SetFormuleID,sComputationSeq);

			}
			catch//(Exception ex)
			{
				dt = null;
			}
			return dt;
		}
		[Ajax.AjaxMethod]
		public string CalulateItemUsers(string sMonth, string LSCompanyID, string LSLevel1ID, string LSLevel2ID
			, string LSLevel3ID, string EmpID, string EmpName, string sLanguageID, string sAccountID, string SetFormuleID
			,string sLSSalaryItemDataID, string sFormula)
		{
			string sErr = "";
			try
			{
				sErr = clsPayroll.CalulateItemUser(sMonth,LSCompanyID,LSLevel1ID,LSLevel2ID
					,LSLevel3ID,EmpID, EmpName,sLanguageID,sAccountID,SetFormuleID,sLSSalaryItemDataID,sFormula);

			}
			catch(Exception ex)
			{
				sErr = ex.Message;
			}
			return sErr;
		}
		#endregion
		private void btnCalulate_Click(object sender, System.EventArgs e)
		{
			EHSearch.empHeaderSearch = EmpHeaderSearch1;
			EHSearch.EmpSearch();
			string sFromDate = txtFromDate.Text;
			string sToDate = txtToDate.Text;
			string sMonth = txtMonth.Text;
			string strErr="";
			strErr=clsPayroll.CalculateSalary(sMonth,EHSearch.LSCompanyID,EHSearch.LSLevel1ID,EHSearch.LSLevel2ID
				,EHSearch.LSLevel3ID,EHSearch.EmpID, EHSearch.EmpName,strLanguage,strUserGroupID);
			
			if (strErr=="")
				strErr= clsChangeLang.getStringAlert("PC_0004",strLanguage);
			else
				strErr= clsChangeLang.getStringAlert("PC_0003",strLanguage);
			clsChangeLang.popupWindow(this.Parent,strErr,"",1);
		}

		private void btnView_Click(object sender, System.EventArgs e)
		{	trColumnList.Style.Add("DISPLAY","block");		 
			BindDataGrid(true);
			lblErr.Text="";
		}
		private void BindDataGrid(bool Status)
		{
			DataTable dtList = new DataTable();
			try
			{
				EHSearch.empHeaderSearch = EmpHeaderSearch1;
				EHSearch.EmpSearch();
				if (Status)
				{
					dtList = clsPayroll.GetDataSalary(txtMonth.Text, EHSearch.LSCompanyID,EHSearch.LSLevel1ID,EHSearch.LSLevel2ID,EHSearch.LSLevel3ID
						,EHSearch.EmpID,EHSearch.EmpName,Mession.GlbLangID,strUserGroupID);
					Mession.GlbSearchData = dtList;
					this.grdPayRoll.CurrentPageIndex = 0;
				}
				else
					dtList = Mession.GlbSearchData;
				
				grdPayRoll.DataSource = dtList;
				grdPayRoll.DataBind();
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
				if (btnDelete.Enabled == true)
				{
					string strID="";
					for(int i=0;i<grdPayRoll.Items.Count;i++)
					{
						if(((CheckBox)grdPayRoll.Items[i].FindControl("chkSelect")).Checked==true)
						{
							string EmpID = grdPayRoll.Items[i].Cells[0].Text.Trim();
							string sMonth = grdPayRoll.Items[i].Cells[1].Text.Trim();
							clsDB.Exc_CommandText("PR_spPayrollCollection @Activity='Delete',@Month='"+sMonth+"',@EmpID = '"+EmpID+ "'");
						}
					}
					
					btnView_Click(null,null);
				}
				else
				{
					Response.Redirect("Editpage.aspx?ModuleID=PR&ParentID=70&Ascx=MdlPR/PayrollCollection.ascx&FunctionID=548");
				}
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}

		private void btnLock_Click(object sender, System.EventArgs e)
		{
			//Mession.GlbMMYYYY = txtMonth.Text;
		}

		private void grdPayRoll_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			try
			{
				BindDataGrid(false);
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
			DataTable dtb = new DataTable();
			dtb = Mession.GlbSearchData;

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
		
	}
}