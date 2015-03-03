namespace iHRPCore.MdlHR
{
	
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.HRComponent;
	using iHRPCore.Com;
	using iHRPCore.Include;
	using GridSort;
	

	/// <summary>
	///		Summary description for Insuarance.
	/// </summary>
	public class Insuarance : System.Web.UI.UserControl
	{
		#region Declare
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label lblFromDate;
		protected System.Web.UI.WebControls.TextBox txtFromDate;
		protected System.Web.UI.WebControls.Label lblToDate;
		protected System.Web.UI.WebControls.TextBox txtToDate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtQualificationID;
		protected System.Web.UI.WebControls.Label lblMoney;
		protected System.Web.UI.WebControls.DropDownList cboInsMoney;
		protected System.Web.UI.WebControls.RadioButton optAfter;
		protected EmpHeaderSearch EmpHeaderSearch1;
		protected System.Web.UI.WebControls.RadioButton optBefore;
		protected System.Web.UI.WebControls.RadioButton optAll;
		protected string strLanguage ="EN";
		protected System.Web.UI.WebControls.TextBox Textbox1;
		protected System.Web.UI.WebControls.TextBox txtEmpName;
		protected System.Web.UI.WebControls.TextBox Textbox2;
		protected System.Web.UI.WebControls.TextBox Textbox3;
		protected System.Web.UI.WebControls.TextBox Textbox4;
		protected System.Web.UI.WebControls.TextBox Textbox5;
		protected System.Web.UI.WebControls.CheckBox chkNotSetup;
		protected System.Web.UI.WebControls.CheckBox chkPropose;
		protected System.Web.UI.WebControls.CheckBox chkBought;
		protected System.Web.UI.WebControls.CheckBox chkAll;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtTitle;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.TextBox txtText3;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtText1;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.TextBox txtText4;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.TextBox txtText2;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.TextBox txtReportDate;
		protected System.Web.UI.WebControls.TextBox txtTEmpID;
		protected System.Web.UI.WebControls.TextBox txtTEmpName;
		protected System.Web.UI.WebControls.TextBox txtCompanyID;
		protected System.Web.UI.WebControls.TextBox txtLevel1ID;
		protected System.Web.UI.WebControls.TextBox txtLevel2ID;
		protected System.Web.UI.WebControls.TextBox txtLevel3ID;
		protected System.Web.UI.WebControls.TextBox txtPositionID;
		protected System.Web.UI.WebControls.TextBox txtJobcodeID;
		protected System.Web.UI.WebControls.TextBox txtLocationID;
		protected System.Web.UI.WebControls.TextBox txtTStatus;
		protected System.Web.UI.WebControls.CheckBox chkShowGrid;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.LinkButton btnPrint;
		protected System.Web.UI.WebControls.DataGrid GrdInsuarance;
		protected System.Web.UI.HtmlControls.HtmlTableRow trGrid;
		protected System.Web.UI.WebControls.TextBox txtFlag;
		string strEmpID = "";
		string strEmpName = "";
		string strLevel1 = "";
		string strLevel2 = "";
		string strLevel3 = "";
		string strLocation = "";
		string strJobCode = "";
		string strPosition = "";
		string strCompany = "";
		string strStatus = "";
		string strFromDate = "";
		string strToDate="";
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.TextBox txtContractNo;
		protected System.Web.UI.WebControls.LinkButton btnSearch;
		string strInsMoney="";
		protected ColumnList uctrlColumns;
		#endregion
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			Session["CurFunction"] = Request.Params["FunctionID"].Trim();
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
			if(!Page.IsPostBack)
			{
				LoadDataCombo();
				Session["ssStatusQual"] = "AddNew";
			}
			chkAll.Attributes.Add("OnClick", " return FillCheck()");
			btnSave.Attributes.Add("OnClick", " return checkvalidSave()");
			btnSearch.Attributes.Add("OnClick", " return checkvalidSearch()");
			btnDelete.Attributes.Add("OnClick", "return checkdelete()");	
			btnPrint.Attributes.Add("OnClick","return ShowReport()");
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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			this.GrdInsuarance.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.GrdInsuarance_PageIndexChanged);
			this.GrdInsuarance.SortCommand+=new DataGridSortCommandEventHandler(GrdInsuarance_SortCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void GetHeaderValue()
		{
			strEmpID = EmpHeaderSearch1.txtEmpID.Text.Trim();
			strEmpName = EmpHeaderSearch1.txtEmpName.Text.Trim();
			strLevel1 = EmpHeaderSearch1.cboLevel1.SelectedValue.Trim();
			strLevel2 = EmpHeaderSearch1.cboLevel2.SelectedValue.Trim();
			strLevel3 = EmpHeaderSearch1.cboLevel3.SelectedValue.Trim();
			strLocation = EmpHeaderSearch1.cboLocation.SelectedValue.Trim();
			strJobCode = EmpHeaderSearch1.cboJobcode.SelectedValue.Trim();
			strPosition = EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue.Trim();
			strCompany = EmpHeaderSearch1.cboCompany.SelectedValue.Trim();
			strStatus = EmpHeaderSearch1.optStatus.SelectedValue.Trim();
			strFromDate=txtFromDate.Text.ToString().Trim();
			strToDate=txtToDate.Text.ToString().Trim();
			strInsMoney=cboInsMoney.SelectedValue.Trim();  
		}
		
		private void BindDataGrid()
		{
			//Lay gia tri cua HeaderEmpSearch
			GetHeaderValue();
			string strType ="4";
			if (chkNotSetup.Checked==true)
				strType = "1";
			else if(chkPropose.Checked==true && chkBought.Checked==false)
				strType= "2";
			else if(chkBought.Checked==true && chkPropose.Checked==false)
				strType= "3";
			else if(chkAll.Checked==true)
				strType= "4";
			else if (chkPropose.Checked==true && chkBought.Checked==true)
				strType= "5";
			DataTable dtList = new DataTable();
			try
			{
				dtList=clsHRLifeInsuarance.GetDataAll(strType,strFromDate,strToDate,strEmpID,strEmpName,strCompany,strLevel1,strLevel2,strLevel3,strPosition,strJobCode,strLocation,strStatus,strInsMoney);  
				GrdInsuarance.DataSource = dtList;
				GrdInsuarance.CurrentPageIndex = 0;
				GrdInsuarance.DataBind();
				//add code - 19112006 - thêm columnList ----------------------
				//Hau
				this.GrdInsuarance.PageSize = uctrlColumns.iPageRows;
				if(this.uctrlColumns.bAdvMultiSort)
				{
					try
					{
						DataGridSort.AdvancedMultiSort(GrdInsuarance, dtList, uctrlColumns);
					}
					catch
					{
						GrdInsuarance.CurrentPageIndex = 0;
						DataGridSort.AdvancedMultiSort(GrdInsuarance, dtList, uctrlColumns);
					}
				}
				else
				{
					try
					{
						DataGridSort.Refresh(GrdInsuarance, dtList);				
					}
					catch
					{
						GrdInsuarance.CurrentPageIndex = 0;
						DataGridSort.Refresh(GrdInsuarance, dtList);
					}
				}
				this.uctrlColumns.iTotalRows = dtList.Rows.Count;
				//end add code 19112006------------------------------------------------------					
				for(int i = 0; i<this.GrdInsuarance.Items.Count; i++)
				{
					((CheckBox)this.GrdInsuarance.Items[i].FindControl("chkIsProposed")).Checked = 
						((HtmlInputHidden)this.GrdInsuarance.Items[i].FindControl("txtIsProposed")).Value == "1"?true:false;
					((CheckBox)this.GrdInsuarance.Items[i].FindControl("chkIsBought")).Checked = 
						((HtmlInputHidden)this.GrdInsuarance.Items[i].FindControl("txtIsBought")).Value == "1"?true:false;
					((CheckBox)this.GrdInsuarance.Items[i].FindControl("chkIsProposed")).Enabled=false;
					((CheckBox)this.GrdInsuarance.Items[i].FindControl("chkIsBought")).Enabled=false;
				}
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
			finally
			{
				dtList.Dispose();
			}
			
		}
		
		private void LoadDataCombo()
		{
		//	string strTextField1 = strLanguage == "VN"?"VNName":"Name";
			clsCommon.LoadDropDownListControl(cboInsMoney,"sp_GetDataComboInsMoney","ParamValue","ParamNumber",true);						
		}
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			GetHeaderValue();
			string strType="1";
			if (chkBought.Checked==true)
				strType="2";
			try
			{				
				for(int i=0;i<GrdInsuarance.Items.Count;i++)
					{
						if(((CheckBox)GrdInsuarance.Items[i].FindControl("chkSelect")).Checked==true)
						{
							string EmpID=GrdInsuarance.Items[i].Cells[2].Text.Trim();
							string InsuranceID=GrdInsuarance.Items[i].Cells[1].Text.Trim();
							try
							{
								clsHRLifeInsuarance.SaveFromGrid(strType,strFromDate,strToDate,EmpID,strInsMoney,InsuranceID); 
							}
							catch(Exception ex)
							{
								lblErr.Text = ex.Message;
							}
						}
					}
				BindDataGrid();
				if(chkShowGrid.Checked==true)
					trGrid.Style.Add("DISPLAY","block");
				else				
					trGrid.Style.Add("DISPLAY","none");
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}					
		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			this.BindDataGrid();
			chkShowGrid.Checked = true;
				
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				GetHeaderValue();
				string strType="0";
				if (chkPropose.Checked==true)
					strType="1";
				else if (chkBought.Checked==true)
					strType="2";
				for(int i=0;i<GrdInsuarance.Items.Count;i++)
					{
						if(((CheckBox)GrdInsuarance.Items[i].FindControl("chkSelect")).Checked==true)
						{
							string InsuranceID=GrdInsuarance.Items[i].Cells[1].Text.Trim();
							try
							{
								clsHRLifeInsuarance.Delete(strType,InsuranceID) ; 
							}
							catch(Exception ex)
							{
								lblErr.Text = ex.Message;
							}
						}
					}
				BindDataGrid();
				if(chkShowGrid.Checked==true)
					trGrid.Style.Add("DISPLAY","block");
				else				
					trGrid.Style.Add("DISPLAY","none");
	
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}

		private void GrdInsuarance_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			try
			{
				BindDataGrid();
				GrdInsuarance.CurrentPageIndex=e.NewPageIndex;
				GrdInsuarance.DataBind(); 
				for(int i = 0; i<this.GrdInsuarance.Items.Count; i++)
				{
					((CheckBox)this.GrdInsuarance.Items[i].FindControl("chkIsProposed")).Checked = 
						((HtmlInputHidden)this.GrdInsuarance.Items[i].FindControl("txtIsProposed")).Value == "1"?true:false;
					((CheckBox)this.GrdInsuarance.Items[i].FindControl("chkIsBought")).Checked = 
						((HtmlInputHidden)this.GrdInsuarance.Items[i].FindControl("txtIsBought")).Value == "1"?true:false;
					((CheckBox)this.GrdInsuarance.Items[i].FindControl("chkIsProposed")).Enabled=false;
					((CheckBox)this.GrdInsuarance.Items[i].FindControl("chkIsBought")).Enabled=false;
				}

			}
			catch(Exception ex)			
			{
				string str = ex.Message;
			}	
		}

		private void btnExport_Click(object sender, System.EventArgs e)
		{
			iHRPCore.DataGridExcelExporter myExcelXport=new iHRPCore.DataGridExcelExporter(GrdInsuarance);			
			myExcelXport.Export("");
			myExcelXport =null;
		}
		#region Cac su kien xu li thao tac Sort (Hau)

		
		private DataTable Read_Data()
		{
			GetHeaderValue();
			string strType ="4";
			if (chkNotSetup.Checked==true)
				strType = "1";
			else if(chkPropose.Checked==true && chkBought.Checked==false)
				strType= "2";
			else if(chkBought.Checked==true && chkPropose.Checked==false)
				strType= "3";
			else if(chkAll.Checked==true)
				strType= "4";
			else if (chkPropose.Checked==true && chkBought.Checked==true)
				strType= "5";
			DataTable dtList = new DataTable();
			dtList=clsHRLifeInsuarance.GetDataAll(strType,strFromDate,strToDate,strEmpID,strEmpName,strCompany,strLevel1,strLevel2,strLevel3,strPosition,strJobCode,strLocation,strStatus,strInsMoney);  
			return dtList;
		}
		
		private void GrdInsuarance_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			try
			{							
					DataGridSort.Grid_Sort(GrdInsuarance, Read_Data(), e, uctrlColumns);				
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
				DataGridSort.AdvancedMultiSort(GrdInsuarance, Read_Data(), uctrlColumns);
				
				
			}
			catch{}
		}

		private void ButtonPRChanged_ServerClick(object sender, EventArgs e)
		{
			try
			{
				DataView dv;
				GrdInsuarance.PageSize = uctrlColumns.iPageRows;
				GrdInsuarance.CurrentPageIndex = 0;
				dv = new DataView(Read_Data());
				
				
				
				dv.Sort = DataGridSort.sMulSort;
				GrdInsuarance.DataSource = dv;
				GrdInsuarance.DataBind();

				for(int i = 0; i<this.GrdInsuarance.Items.Count; i++)
				{
					((CheckBox)this.GrdInsuarance.Items[i].FindControl("chkIsProposed")).Checked = 
						((HtmlInputHidden)this.GrdInsuarance.Items[i].FindControl("txtIsProposed")).Value == "1"?true:false;
					((CheckBox)this.GrdInsuarance.Items[i].FindControl("chkIsBought")).Checked = 
						((HtmlInputHidden)this.GrdInsuarance.Items[i].FindControl("txtIsBought")).Value == "1"?true:false;
					((CheckBox)this.GrdInsuarance.Items[i].FindControl("chkIsProposed")).Enabled=false;
					((CheckBox)this.GrdInsuarance.Items[i].FindControl("chkIsBought")).Enabled=false;
				}
			}
			catch{}
		}

		#endregion		
	}
}
