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
	/// <summary>
	///		Summary description for Bonus.
	/// </summary>
	public class Bonus : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.CheckBox chkShowGrid;
		protected System.Web.UI.WebControls.LinkButton btnAddNew;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.LinkButton btnImport;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label lblTotalRows;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.DropDownList cboCol1;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.DropDownList cboCol2;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.DropDownList cboCol3;
		protected System.Web.UI.WebControls.TextBox txtPageRows;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnRowNumber;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.DataGrid grdBonus;
		protected System.Web.UI.HtmlControls.HtmlTableRow trGrid;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdBonusID;
		protected System.Web.UI.WebControls.RadioButtonList optMethodID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtBonusID;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.DropDownList cboLSBonusID;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.TextBox txtBonusDate;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.TextBox txtAmount;
		protected System.Web.UI.WebControls.Label Label14;
		protected System.Web.UI.WebControls.TextBox txtRefMonth;
		protected System.Web.UI.WebControls.Label Label16;
		protected System.Web.UI.WebControls.TextBox txtPRMonth;
		protected System.Web.UI.WebControls.Label Label19;
		protected System.Web.UI.WebControls.TextBox txtNote;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label lblPayToSalary;
		protected System.Web.UI.WebControls.CheckBox chkPIT;
		protected System.Web.UI.WebControls.CheckBox chkToPR;
        protected System.Web.UI.WebControls.DropDownList cboCurrencyTypeID;
		protected System.Web.UI.WebControls.RadioButtonList optIsGross;
		public string strLanguage = "EN";

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
			if(!Page.IsPostBack)
			{
				LoadDataCombo();
				LoadDataGrid();
				Session["ssStatusBonus"] = "AddNew";
				if(Session["ssBonusID"]!=null)
				{
					LoadDataToEdit(Session["ssBonusID"].ToString().Trim());
					Session["ssBonusID"]= null;					
				}
			}
			btnSave.Attributes.Add("OnClick", " return validform()");
			btnDelete.Attributes.Add("OnClick", "return checkdelete()");
			btnImport.Attributes.Add("onclick","return ShowExcelSelectPage();");
		}
		/// <summary>
		/// Load all Allowance of Employee
		/// </summary>
		private void LoadDataGrid()
		{
			DataTable dtList = new DataTable();
			try
			{
				dtList = clsPRBonus.GetDataByEmpID(Session["EmpID"], strLanguage);				
				grdBonus.DataSource = dtList;
				grdBonus.CurrentPageIndex = 0;
				grdBonus.DataBind();
				lblTotalRows.Text = dtList.Rows.Count.ToString();
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
		/// <summary>
		/// Load data to combo box
		/// </summary>
		private void LoadDataCombo()
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			clsCommon.LoadDropDownListControl(cboLSBonusID,"sp_GetDataCombo @TableName='LS_tblBonus',@Fields='LSBonusID as [ID]," + strTextField + " as Name'","ID","Name",true);
			clsCommon.LoadDropDownListControl(cboCurrencyTypeID,"LS_spfrmCURRENCYTYPE @Activity=N'GetDataCombo',@LanguageID='"+ strLanguage+ "'","Ma","Ten",false);
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
			this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			this.grdBonus.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.grdBonus_ItemCommand);
			this.grdBonus.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.grdBonus_PageIndexChanged);
			this.btnRowNumber.ServerClick += new System.EventHandler(this.btnRowNumber_ServerClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		//ADD NEW RECORD
		private void btnAddNew_Click(object sender, System.EventArgs e)
		{
			clsCommon.ClearControlValue(this);
			Session["ssStatusBonus"] = "AddNew";
			if(chkShowGrid.Checked==true)				
				trGrid.Style.Add("DISPLAY","block");
			else
				trGrid.Style.Add("DISPLAY","none");			
		}
		//SAVE OR EDIT RECORD
		private void btnSave_Click(object sender, System.EventArgs e)
		{   bool fl;
		    int type=0;
			try
			{				
				if(Session["ssStatusBonus"].ToString().Trim()=="AddNew"){
					fl=clsCommon.ImpactDB("@Creater",Session["AccountLogin"],Session["EmpID"].ToString().Trim(),"Save",this,"PR_spfrmBonus");
					type=1;
				}
				else{
					fl=clsCommon.UpdateByKey("@Editer",Session["AccountLogin"],"BonusID",txtBonusID.Value.Trim(),"Update",this,"PR_spfrmBonus");
					type=2;
				}
				if(fl==true){
				    if(type==1){//adnew
				        clsChangeLang.popupWindow(this.Parent,"0046","EN","",1);				        
				    }
				    else{//update
				        clsChangeLang.popupWindow(this.Parent,"0044","EN","",1);
				    }
				}else{
				    clsChangeLang.popupWindow(this.Parent,"0027","EN","",0);
				}
				btnAddNew_Click(null,null);
				LoadDataGrid();
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}
		//ITEM COMMAND <VIEW AND EDIT RECORD>
		private void grdBonus_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				if(e.CommandName.Trim().ToUpper() =="EDIT")
				{
					LoadDataToEdit(grdBonus.Items[e.Item.ItemIndex].Cells[0].Text.Trim());					
				}				
			}	
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}
		//LOAD RECORD DATA TO EDIT
		private void LoadDataToEdit(string strID)
		{
			try
			{
				hdBonusID.Value = strID;
				DataRow iRow = clsPRBonus.GetDataByID(strID);
				if(iRow != null)
				{
					txtBonusID.Value=iRow["BonusID"].ToString().Trim();
					txtAmount.Text = iRow["Amount"].ToString().Trim();
					txtBonusDate.Text = iRow["BonusDate"].ToString().Trim();
					txtNote.Text = iRow["Note"].ToString().Trim();					
					txtPRMonth.Text = iRow["PRMonth"].ToString().Trim();
					txtRefMonth.Text = iRow["RefMonth"].ToString().Trim();					
					cboLSBonusID.SelectedValue = iRow["LSBonusID"].ToString().Trim();
					//optLSMethodID.SelectedValue = iRow["LSMethodID"].ToString().Trim();
					cboCurrencyTypeID.SelectedValue=iRow["CurrencyTypeID"].ToString().Trim();
				
					if (iRow["ToPR"].ToString().Trim()=="True")
						chkToPR.Checked=true;
					else
						chkToPR.Checked=false;

					if (iRow["PIT"].ToString().Trim()=="True")
						chkPIT.Checked=true;
					else
						chkPIT.Checked=false;

					if (iRow["IsGross"].ToString().Trim()=="True")
						optIsGross.SelectedValue = "1";
					else
						optIsGross.SelectedValue = "0";

				}
				Session["ssStatusBonus"] = "Edit";				
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
		//DELETE CHECKED RECORD
		private void btnDelete_Click(object sender, System.EventArgs e)
		{   bool a;
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
				a=clsCommon.DeleteListRecord("PR_spfrmBONUS","BonusID",SqlDbType.NVarChar,12,strID);				
				if(chkShowGrid.Checked==true)
					trGrid.Style.Add("DISPLAY","block");
				else				
					trGrid.Style.Add("DISPLAY","none");
				LoadDataGrid();
                
                if(a==true){
                    clsChangeLang.popupWindow(this.Parent,"0104","EN","",1);				        
                }
                else{//update
                    clsChangeLang.popupWindow(this.Parent,"0048","EN","",0);
                }
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}

		private void grdBonus_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			try
			{
				LoadDataGrid();
				grdBonus.CurrentPageIndex=e.NewPageIndex;
				grdBonus.DataBind();
			}
			catch(Exception ex)
			{
				string str = ex.Message;
			}
		}
		//EXPORT GRID DATA
		private void btnExport_Click(object sender, System.EventArgs e)
		{
			iHRPCore.DataGridExcelExporter myExcelXport=new DataGridExcelExporter(grdBonus);			
			myExcelXport.Export("");
			myExcelXport =null;
		}
		// CHANGE ROW NUMBER
		private void btnRowNumber_ServerClick(object sender, System.EventArgs e)
		{
			grdBonus.PageSize = Convert.ToInt32(txtPageRows.Text.Trim()==""?"1":txtPageRows.Text.Trim());
			grdBonus.CurrentPageIndex = 0;
			LoadDataGrid();
		}

		private void btnImport_Click(object sender, System.EventArgs e)
		{
		
		}
		
	}
}
