namespace iHRPCore.MdlPR
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
	using iHRPCore.PRComponent;	
	using System.Data.SqlClient;
	using GridSort;

	/// <summary>
	///		Summary description for PayrollCollection_Secondee.
	/// </summary>
	public class PayrollCollection_Secondee : System.Web.UI.UserControl
	{
		#region Declare Controls
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.TextBox txtMonth;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtSalPeriod;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtEmpID;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.TextBox txtEmpName;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList cboCompanyID;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.DropDownList cboLevel1ID;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.DropDownList cboLevel2ID;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.DropDownList cboLevel3ID;
		protected System.Web.UI.WebControls.LinkButton btnNewPayroll;
		protected System.Web.UI.WebControls.LinkButton btnCalulate;
		protected System.Web.UI.WebControls.LinkButton btnView;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.LinkButton btnLock;
		protected System.Web.UI.WebControls.LinkButton btnPrint;
		protected System.Web.UI.WebControls.DataGrid grdPayRoll;
		protected System.Web.UI.WebControls.TextBox txtFromDate;
		protected System.Web.UI.WebControls.TextBox txtToDate;
		protected System.Web.UI.WebControls.TextBox txtPageLoad;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel1ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel2ID;
		protected System.Web.UI.HtmlControls.HtmlTableRow trColumnList;
		protected System.Web.UI.WebControls.LinkButton btnReCalculate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel3ID;
		#endregion
		protected ColumnList uctrlColumns;
		public string strLanguage="EN";

		private void Page_Load(object sender, System.EventArgs e)
		{
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
			
			if (!Page.IsPostBack)
			{
				LoadCombobox();
				
				DataRow drData= clsPRCollection.LoadDatePR3();
				if (drData!=null)
				{
					txtMonth.Text=drData["Month"].ToString();
					txtSalPeriod.Text=drData["SalPeriod"].ToString();
					txtFromDate.Text=drData["FromDate"].ToString();
					txtToDate.Text=drData["ToDate"].ToString();
					checkIsPR();
					grdPayRoll.DataSource=new DataTable();
					grdPayRoll.DataBind();
				}
				DataGridSort.AddItemColumn(uctrlColumns, grdPayRoll);//
			}
			if (cboCompanyID.Items.Count==2) cboCompanyID.SelectedIndex=1;
			cboCompanyID_SelectedIndexChanged(null,null);
			
			btnDelete.Attributes.Add("OnClick", "return checkdelete()");	
			btnCalulate.Attributes.Add("OnClick", "return Calculate()");	
			btnLock.Attributes.Add("OnClick", "return Lock()");	
			btnPrint.Attributes.Add("onclick","return ShowPage();");
			//ButtonClick();		
		}

		private void LoadCombobox()
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			clsHREmpList.LoadComboCompany(cboCompanyID, strTextField, strLanguage,this.Page);			
			clsHREmpList.LoadComboLevel2(cboLevel2ID, strTextField, strLanguage,this.Page);
		}

		private void cboCompanyID_SelectedIndexChanged(object sender, System.EventArgs e)
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

		private void cboLevel1ID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			string strLevel1ID = cboLevel1ID.SelectedValue.Trim();
			//clsHREmpList.LoadComboLevel2ByLevel1(cboLevel2ID,strTextField,strLevel1ID, strLanguage,this.Page); 
			//cboLevel2ID.SelectedValue = this.txtLevel2ID.Value.Trim();
			//cboLevel2ID_SelectedIndexChanged(null,null);
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

		private void cboLevel2ID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			string strLevel2ID = cboLevel2ID.SelectedValue.Trim();
			//clsHREmpList.LoadComboLevel3ByLevel2(cboLevel3ID,strTextField,strLevel2ID, strLanguage); 
			//cboLevel3ID.SelectedValue = this.txtLevel3ID.Value.Trim();
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


		private void BindDataGrid()
		{
			DataTable dtList = new DataTable();
			try
			{
				dtList = clsPRCollection.GetDataSalary_HLHV3(cboCompanyID.SelectedValue,cboLevel1ID.SelectedValue,cboLevel2ID.SelectedValue,cboLevel3ID.SelectedValue,txtEmpID.Text.Trim(),txtEmpName.Text.Trim(),txtMonth.Text.Trim());
				grdPayRoll.DataSource = dtList;
				grdPayRoll.CurrentPageIndex = 0;
				grdPayRoll.DataBind();

				#region SortGrid
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
				#endregion

				this.uctrlColumns.iTotalRows = dtList.Rows.Count;//
			}
			catch(Exception ex)
			{
				clsChangeLang.popupWindow(this.Parent,"0034",strLanguage,ex.ToString(),1);
			}
			finally
			{
				//dtList.Dispose();
			}
		}

		private void checkIsPR()
		{			
			lblErr.Text=string.Format(clsPRCollection.CheckIsPR3(strLanguage),txtSalPeriod.Text,txtMonth.Text);

			if (!lblErr.Text.Equals("")) 
			{
				btnCalulate.Enabled=false;
				btnLock.Enabled=false;
				btnView.Enabled=false;
				btnDelete.Enabled=false;
				btnReCalculate.Enabled=false;
				btnPrint.Enabled=false;
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
			this.cboCompanyID.SelectedIndexChanged += new System.EventHandler(this.cboCompanyID_SelectedIndexChanged);
			this.cboLevel1ID.SelectedIndexChanged += new System.EventHandler(this.cboLevel1ID_SelectedIndexChanged);
			this.cboLevel2ID.SelectedIndexChanged += new System.EventHandler(this.cboLevel2ID_SelectedIndexChanged);

			this.btnNewPayroll.Click += new System.EventHandler(this.btnNewPayroll_Click);
			this.btnCalulate.Click += new System.EventHandler(this.btnCalulate_Click);
			this.btnView.Click += new System.EventHandler(this.btnView_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnLock.Click += new System.EventHandler(this.btnLock_Click);
			this.btnReCalculate.Click += new System.EventHandler(this.btnReCalculate_Click);
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnNewPayroll_Click(object sender, System.EventArgs e)
		{
			string strReturn = "";
			txtSalPeriod.Text="1";
			try
			{		
				strReturn = clsPRCreateNewPayroll.CreatePayroll3(txtFromDate.Text, txtToDate.Text,txtMonth.Text,txtMonth.Text,txtSalPeriod.Text);
				if(strReturn.Trim()!="")
				{
					lblErr.Text = "";
					clsChangeLang.popupWindow(this.Parent,"CP_0001",strLanguage,"",0);

				}
				else
				{
					clsChangeLang.popupWindow(this.Parent,"CP_0002",strLanguage,"",1);
				}
			}
			catch
			{
				lblErr.Text = strReturn;
			}
			finally
			{
			}
		}

		private void btnCalulate_Click(object sender, System.EventArgs e)
		{
			string sFromDate = txtFromDate.Text;
			string sToDate = txtToDate.Text;
			
			string strErr=clsPRCollection.CalculateSalary3(sFromDate,sToDate,cboCompanyID.SelectedValue,cboLevel1ID.SelectedValue,cboLevel2ID.SelectedValue,cboLevel3ID.SelectedValue,strLanguage);
			clsChangeLang.popupWindow(this.Parent,strErr,"",1);
		}

		private void btnView_Click(object sender, System.EventArgs e)
		{
			trColumnList.Style.Add("DISPLAY","block");		 
			BindDataGrid();
			lblErr.Text="";
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
						strID += grdPayRoll.Items[i].Cells[0].Text.Trim() + "$";
					}
				}
				clsCommon.DeleteListRecord("PR_spPayrollCollection3","EmpID",SqlDbType.NVarChar,12,strID);				
				btnView_Click(null,null);
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}

		private void btnLock_Click(object sender, System.EventArgs e)
		{
			clsChangeLang.popupWindow(this.Parent,string.Format(clsPRCollection.LockPR3(strLanguage),txtSalPeriod.Text,txtMonth.Text),"",1);
			DataRow drData= clsPRCollection.LoadDatePR3();
			if (drData!=null)
			{
				txtMonth.Text=drData["Month"].ToString();
				txtSalPeriod.Text=drData["SalPeriod"].ToString();
				txtFromDate.Text=drData["FromDate"].ToString();
				txtToDate.Text=drData["ToDate"].ToString();
			}
			//cangtt -- 09052006 -- ki?m tra b?ng luong có t?n t?i hay chua
			checkIsPR();
			//-------------------------------------------------------------			
			lblErr.Text="";
		}

		private void btnReCalculate_Click(object sender, System.EventArgs e)
		{
		
		}

		private void btnPrint_Click(object sender, System.EventArgs e)
		{
		
		}
	}
}
