namespace iHRPCore.MdlPR
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Data.SqlClient;
	using System.Configuration;
	using System.Collections;
	using iHRPCore.Com;
	using iHRPCore.HRComponent;
	using iHRPCore.PRComponent;
	using iHRPCore.Component;
	using GridSort;	

	/// <summary>
	///		Summary description for ListOfSalary_Popup.
	/// </summary>
	public class ListOfSalary_Popup : UserControlCommon
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.Label Label14;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.TextBox txtLSSalaryItemCode;
		protected System.Web.UI.WebControls.TextBox txtName;
		protected System.Web.UI.WebControls.TextBox txtValue;
		protected System.Web.UI.WebControls.TextBox txtComputationSeq;
		protected System.Web.UI.WebControls.TextBox txtRank;
		protected System.Web.UI.WebControls.TextBox txtRptOrdinal;
		protected System.Web.UI.WebControls.DropDownList cboLSSalaryItemTypeID;
		protected System.Web.UI.WebControls.CheckBox chkVisible;
		protected System.Web.UI.WebControls.TextBox txtFormula;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.TextBox txtVNName;
		protected System.Web.UI.WebControls.Label Label17;
		protected System.Web.UI.WebControls.TextBox txtNote;
		protected System.Web.UI.WebControls.CheckBox chkUsed;
		protected System.Web.UI.WebControls.TextBox txtFromMonth;
		protected System.Web.UI.WebControls.Label Label16;
		protected System.Web.UI.WebControls.Label Label18;
		protected System.Web.UI.WebControls.DropDownList cboLocation;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.DropDownList cboSalPeriod;
		protected System.Web.UI.WebControls.CheckBox chkAssign;
		protected System.Web.UI.WebControls.LinkButton btnFilter;
		protected System.Web.UI.WebControls.DataGrid grdOvertime;
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.LinkButton btnClose;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLSSalaryItemID;
		protected System.Web.UI.WebControls.TextBox txtFormular;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel2ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel3ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel1ID;
		protected System.Web.UI.WebControls.DropDownList cboCompany;
		protected System.Web.UI.WebControls.DropDownList cboLevel1;
		protected System.Web.UI.WebControls.DropDownList cboLevel2;
		protected System.Web.UI.WebControls.DropDownList cboLevel3;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtChangeStatus;
		protected System.Web.UI.HtmlControls.HtmlTableRow tr1;
		protected System.Web.UI.HtmlControls.HtmlTableRow tr2;
		protected System.Web.UI.HtmlControls.HtmlTableRow tr3;
		protected System.Web.UI.HtmlControls.HtmlTableRow tr4;
		protected System.Web.UI.HtmlControls.HtmlTableRow tr5;
		protected System.Web.UI.WebControls.CheckBox chkIsDefaultAssign;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.TextBox txtEmpID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Com;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Level1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Level2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Level3;
		protected System.Web.UI.WebControls.Label lblUpdate;
		protected System.Web.UI.WebControls.Label Label19;
		protected System.Web.UI.WebControls.DropDownList cboAssignSalPeriod;
		
		protected ColumnList uctrlColumns;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(!Page.IsPostBack)
			{
				Load_cbobox();
				//BindDataGrid();
				if (Request.QueryString["IDCode"] != null)
				{
					LoadDataToEdit(Request.QueryString["IDCode"].ToString());
					Session["ssStatus"] = "Edit";
					chkAssign.Visible = true;
					btnFilter.Visible  =true;
				}
				else
				{
					Session["ssStatus"] = "AddNew";
					chkAssign.Visible = false;
					btnFilter.Visible  =false;
				}
				grdOvertime.DataSource= CreateDataSource();
				grdOvertime.DataBind();
				txtFromMonth.Text  = clsCommon.LookUpTable("select * from SYS_tblParameters where ParamName = 'PR_CurMonth'","ParamValue");
				txtFromMonth.Enabled = false;
				cboSalPeriod.SelectedValue  = clsCommon.LookUpTable("select * from SYS_tblParameters where ParamName = 'PR_CurPeriod'","ParamValue");
				cboSalPeriod.Enabled = false;
				//Hau
				DataGridSort.AddItemColumn(uctrlColumns, grdOvertime);//
			}
			btnSave.Attributes.Add("onclick", "return CheckSave();");
			btnClose.Attributes.Add("OnClick","return doRefreshParent();");
			this.btnFilter.Attributes.Add("OnClick","return CheckFilter();");
			//btnSave.Attributes.Add("onclick", "return CheckSave();");
			//Hau
			ButtonClick();//
		}
		
		ICollection CreateDataSource() 
		{
			DataTable dt = new DataTable();

			dt.Columns.Add(new DataColumn("EmpID", typeof(string)));
			dt.Columns.Add(new DataColumn("EmpCode", typeof(string)));
			dt.Columns.Add(new DataColumn("Level1ShortName", typeof(string)));
			dt.Columns.Add(new DataColumn("Level2ShortName", typeof(string)));
			dt.Columns.Add(new DataColumn("Level3ShortName", typeof(string)));

			DataView dv = new DataView(dt);
			return dv;
		}

		private void LoadDataToEdit(string strID)
		{
			try
			{
				txtLSSalaryItemID.Value = strID;
				DataRow iRow = clsCommon.GetDataRow("LS_spfrmSalaryItem @Activity = 'GetDataByID',@LSSalaryItemID = '" +strID+ "'");
				if(iRow != null)
				{
					txtLSSalaryItemCode.Text = iRow["LSSalaryItemCode"].ToString().Trim();					
					txtName.Text = iRow["Name"].ToString().Trim();
					txtVNName.Text = iRow["VNName"].ToString().Trim();
					cboLSSalaryItemTypeID.SelectedValue = iRow["LSSalaryItemTypeID"].ToString().Trim();
					txtFormula.Text = iRow["Formula"].ToString().Trim();
					txtValue.Text = iRow["Value"].ToString().Trim();
					txtComputationSeq.Text = iRow["ComputationSeq"].ToString().Trim();
					txtNote.Text = iRow["Note"].ToString().Trim();
					txtRptOrdinal.Text = iRow["RptOrdinal"].ToString().Trim();
					txtRank.Text = iRow["Rank"].ToString().Trim();
					chkVisible.Checked = iRow["Visible"].ToString().Trim() == "True"?true:false;
					chkIsDefaultAssign.Checked = iRow["IsDefaultAssign"].ToString().Trim() == "True"?true:false;
					chkUsed.Checked = iRow["Used"].ToString().Trim() == "True"?true:false;
					if(iRow["IsDefaultAssign"].ToString().Trim() == "True")
						cboAssignSalPeriod.SelectedValue = iRow["AssignSalPeriod"].ToString().Trim();
					else
						cboAssignSalPeriod.SelectedIndex = 0;
				}
				Session["ssStatus"] = "Edit";				

			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}

		private void Load_cbobox()
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			clsHREmpList.LoadComboLocation(cboLocation, strTextField);
			clsHREmpList.LoadComboCompany(cboCompany, strTextField, strLanguage,this.Page);
//			clsHREmpList.LoadComboLevel1(cboLevel1, strTextField, strLanguage,this.Page);
//			clsHREmpList.LoadComboLevel2(cboLevel2, strTextField, strLanguage,this.Page);
//			clsHREmpList.LoadComboLevel3(cboLevel3, strTextField, strLanguage,this.Page);

			clsCommon.LoadDropDownListControl(cboLSSalaryItemTypeID,"LS_spfrmSalaryItemType @Activity = 'GetDataCombo'","Ma","Ten",true);
		}
		private void BindDataGrid()
		{
			DataTable dtList = new DataTable();
			try
			{
				string strEmpID = txtEmpID.Text;
				string strLocation = cboLocation.SelectedValue;
				string strCompany = cboCompany.SelectedValue;
				string strLevel1 = cboLevel1.SelectedValue;
				string strLevel2 = cboLevel2.SelectedValue;
				string strLevel3 = cboLevel3.SelectedValue;
				dtList= clsCommon.GetDataTableHasID("PR_spfrmListOfSalary @Activity='SearchDataByEmpList',@LSLocationID = '"+strLocation+
				"',@LSCompanyID = '" + strCompany + 
				"',@LSLevel1ID = '" + strLevel1 + 
				"',@LSLevel2ID = '" + strLevel2 + 
				"',@LSLevel3ID = '" + strLevel3 + 
				"',@EmpID = '" + strEmpID + 
				"'");
				grdOvertime.DataSource = dtList;
				grdOvertime.CurrentPageIndex = 0;
				grdOvertime.DataBind();

				#region Cangtt --- 03/04/2005

				this.grdOvertime.PageSize = uctrlColumns.iPageRows;
				if(this.uctrlColumns.bAdvMultiSort)
				{
					try
					{
						DataGridSort.AdvancedMultiSort(grdOvertime, dtList, uctrlColumns);
					}
					catch
					{
						grdOvertime.CurrentPageIndex = 0;
						DataGridSort.AdvancedMultiSort(grdOvertime, dtList, uctrlColumns);
					}
				}
				else
				{
					try
					{
						DataGridSort.Refresh(grdOvertime, dtList);
					}
					catch
					{
						grdOvertime.CurrentPageIndex = 0;
						DataGridSort.Refresh(grdOvertime, dtList);
					}
				}
				this.uctrlColumns.iTotalRows = dtList.Rows.Count;

				#endregion
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
			this.cboCompany.SelectedIndexChanged += new System.EventHandler(this.cboCompany_SelectedIndexChanged);
			this.cboLevel1.SelectedIndexChanged += new System.EventHandler(this.cboLevel1_SelectedIndexChanged);
			this.cboLevel2.SelectedIndexChanged += new System.EventHandler(this.cboLevel2_SelectedIndexChanged);
			this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.grdOvertime.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.grdOvertime_PageIndexChanged);
			this.grdOvertime.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.grdOvertime_SortCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			lblErr.Text = "";
			try
			{
				bool blSave = false;
				if(Session["ssStatus"].ToString().Trim()=="AddNew")
				{
					blSave = clsCommon.ImpactDB("Save",this,"LS_spfrmSalaryItem");
				}
				else
				{
					//blSave = clsCommon.ImpactDB("Edit",this,"LS_spfrmSalaryItem");
					//clsChangeLang.getStringAlert("PC_0003",sLanguageID);
					if(clsPRSalaryItem.CheckPermissionEdit(txtLSSalaryItemID.Value.Trim())==true)
						blSave = clsCommon.UpdateByKey("LSSalaryItemID",txtLSSalaryItemID.Value.Trim(),"Update",this,"LS_spfrmSalaryItem");					
					else
					{
						lblUpdate.Text = " (" + clsChangeLang.getStringAlert("0047",strLanguage) + ")";
						blSave = false;
					}
					try
					{
						string strID="";
						for(int i=0;i<grdOvertime.Items.Count;i++)
						{
							if(((CheckBox)grdOvertime.Items[i].FindControl("chkSelect")).Checked==true)
							{
								strID += grdOvertime.Items[i].Cells[0].Text.Trim() + "$";
							}
						}
						if (strID != "")
                            blSave = UpdateListRecord("PR_spfrmListOfSalary","EmpID",SqlDbType.NVarChar,12,strID);
					}
					catch(Exception ex)
					{
						lblErr.Text = ex.Message;
					}
				}
				if (blSave == true)
                    lblErr.Text = GetText("COMMON","SUCCESSFUL");
				else
					lblErr.Text = GetText("COMMON","NOTSUCCESSFUL");
				clsCommon.ClearControlValue(this);
				txtFromMonth.Text  = clsCommon.LookUpTable("select * from SYS_tblParameters where ParamName = 'PR_CurMonth'","ParamValue");
				txtFromMonth.Enabled = false;
				cboSalPeriod.SelectedValue  = clsCommon.LookUpTable("select * from SYS_tblParameters where ParamName = 'PR_CurPeriod'","ParamValue");
				cboSalPeriod.Enabled = false;

				chkAssign.Visible = false;
				btnFilter.Visible  =false;
				if(chkAssign.Checked==true)
				{
					tr1.Style.Add("DISPLAY","block");
					tr2.Style.Add("DISPLAY","block");
					tr3.Style.Add("DISPLAY","block");
					tr4.Style.Add("DISPLAY","block");
					tr5.Style.Add("DISPLAY","block");
				}
				else
				{
					tr1.Style.Add("DISPLAY","none");
					tr2.Style.Add("DISPLAY","none");
					tr3.Style.Add("DISPLAY","none");
					tr4.Style.Add("DISPLAY","none");
					tr5.Style.Add("DISPLAY","none");
				}
				DataTable dt = new DataTable();
				grdOvertime.DataSource = dt;
				grdOvertime.DataBind();
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
			finally
			{
				//txtReturnValue.Value = "";
			}
		}
		private bool UpdateListRecord(string  strStoreName,string strKeyField,SqlDbType pType, int intSize, string strListID)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = strStoreName.Trim();
			//-------
			try
			{
				string[] arrID = strListID.Trim().Split('$');
				//--------------
				for(int i=0;i<arrID.Length-1;i++)
				{
					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Update";
					SqlParameter param = new SqlParameter();
					param.ParameterName = "@" + strKeyField.Trim();
					param.SqlDbType = pType;
					param.Size = intSize;				
					param.Value = arrID.GetValue(i).ToString().Trim();
					cmd.Parameters.Add(param);
					cmd.Parameters.Add("@LSSalaryItemID", SqlDbType.NVarChar, 12).Value =txtLSSalaryItemID.Value.Trim();
					cmd.Parameters.Add("@Month", SqlDbType.NVarChar, 7).Value =txtFromMonth.Text;
					cmd.Parameters.Add("@SalPeriod", SqlDbType.Int).Value =cboSalPeriod.SelectedValue;
					if (txtFormula.Text == "")
						cmd.Parameters.Add("@Formula", SqlDbType.NVarChar, 1000).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Formula", SqlDbType.NVarChar, 1000).Value =txtFormula.Text;
					if (txtValue.Text == "")
						cmd.Parameters.Add("@Value", SqlDbType.Float).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Value", SqlDbType.Float).Value =txtValue.Text;
					if (txtComputationSeq.Text == "")
						cmd.Parameters.Add("@ComputationSeq", SqlDbType.Int).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@ComputationSeq", SqlDbType.Int).Value =txtComputationSeq.Text;
					cmd.Parameters.Add("@IsDefaultAssign", SqlDbType.Bit).Value = chkIsDefaultAssign.Checked == true?1:0;
					cmd.Parameters.Add("@Used", SqlDbType.Bit).Value = chkUsed.Checked == true?1:0;
					cmd.Parameters.Add("@Visible", SqlDbType.Bit).Value = chkVisible.Checked == true?1:0;
					cmd.ExecuteNonQuery();
				}				
				sqlTran.Commit();
				return true;
			}
			catch (Exception exp)
			{
				string strErr = exp.Message;
				sqlTran.Rollback();
				return false;
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();
			}			
		}

		private void cboCompany_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			string strCompanyID = cboCompany.SelectedValue.Trim();
			if (this.Com.Value.Trim() == "1")
			{
				clsHREmpList.LoadComboLevel1ByCompany(cboLevel1,strTextField,strCompanyID, strLanguage,this.Page); 
				cboLevel1.SelectedValue = this.txtLevel1ID.Value.Trim();
				cboLevel1_SelectedIndexChanged(null,null);
			}
		}

		private void cboLevel1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			string strLevel1ID = cboLevel1.SelectedValue.Trim();
			if (this.Level1.Value.Trim() == "1")
			{
				clsHREmpList.LoadComboLevel2ByLevel1(cboLevel2,strTextField,strLevel1ID, strLanguage,this.Page); 
				cboLevel2.SelectedValue = this.txtLevel2ID.Value.Trim();
				cboLevel2_SelectedIndexChanged(null,null);
			}
		}

		private void cboLevel2_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			string strLevel2ID = cboLevel2.SelectedValue.Trim();
			if (this.Level2.Value.Trim() == "1")
			{
				clsHREmpList.LoadComboLevel3ByLevel2(cboLevel3,strTextField,strLevel2ID, strLanguage); 
				cboLevel3.SelectedValue = this.txtLevel3ID.Value.Trim();
			}
		}

		private void btnFilter_Click(object sender, System.EventArgs e)
		{
			BindDataGrid();
			if(chkAssign.Checked==true)
			{
				tr1.Style.Add("DISPLAY","block");
				tr2.Style.Add("DISPLAY","block");
				tr3.Style.Add("DISPLAY","block");
				tr4.Style.Add("DISPLAY","block");
				tr5.Style.Add("DISPLAY","block");
			}
			else
			{
				tr1.Style.Add("DISPLAY","none");
				tr2.Style.Add("DISPLAY","none");
				tr3.Style.Add("DISPLAY","none");
				tr4.Style.Add("DISPLAY","none");
				tr5.Style.Add("DISPLAY","none");
			}
		}

		private void grdOvertime_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			try
			{
				BindDataGrid();
				grdOvertime.CurrentPageIndex=e.NewPageIndex;
				grdOvertime.DataBind();
			}
			catch(Exception ex)			
			{
				string str = ex.Message;
			}		

			//Hau
			try
			{
				DataGridSort.Grid_IndexChanged(grdOvertime, Read_Data(), e);
			}
			catch{}//
		}
		#region Cac su kien xu li thao tac Sort (Hau)

		private DataTable Read_Data()
		{			
			DataTable dtb = new DataTable();
			string strEmpID = txtEmpID.Text;
			string strLocation = cboLocation.SelectedValue;
			string strCompany = cboCompany.SelectedValue;
			string strLevel1 = cboLevel1.SelectedValue;
			string strLevel2 = cboLevel2.SelectedValue;
			string strLevel3 = cboLevel3.SelectedValue;
			dtb= clsCommon.GetDataTableHasID("PR_spfrmListOfSalary @Activity='SearchDataByEmpList',@LSLocationID = '"+strLocation+
				"',@LSCompanyID = '" + strCompany + 
				"',@LSLevel1ID = '" + strLevel1 + 
				"',@LSLevel2ID = '" + strLevel2 + 
				"',@LSLevel3ID = '" + strLevel3 + 
				"',@EmpID = '" + strEmpID + 
				"'");
			return dtb;
		}

		private void grdOvertime_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			try
			{
				DataGridSort.Grid_Sort(grdOvertime, Read_Data(), e, uctrlColumns);
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
				DataGridSort.AdvancedMultiSort(grdOvertime, Read_Data(), uctrlColumns);
			}
			catch{}
		}

		private void ButtonPRChanged_ServerClick(object sender, EventArgs e)
		{
			try
			{
				grdOvertime.PageSize = uctrlColumns.iPageRows;
				grdOvertime.CurrentPageIndex = 0;
				DataView dv = new DataView(Read_Data());
				dv.Sort = DataGridSort.sMulSort;
				grdOvertime.DataSource = dv;
				grdOvertime.DataBind();
			}
			catch{}
		}
		#endregion
	}
}
