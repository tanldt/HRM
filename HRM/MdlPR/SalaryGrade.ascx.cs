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
	using System.Data.SqlClient;
	using System.Configuration;
	using iHRPCore.HRComponent;

	/// <summary>
	///		Summary description for SalaryGrade.
	///		Tanldt
	///		12/09/2005
	/// </summary>
	public class SalaryGrade : System.Web.UI.UserControl
	{
		public string strLanguage = "EN";
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.CheckBox chkShowGrid;
		protected System.Web.UI.WebControls.LinkButton btnAddNew;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.Label Label12;		
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.Label lblTotalRows;
		protected System.Web.UI.WebControls.LinkButton btnImport;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.DataGrid grdSalaryGrade;
		protected System.Web.UI.WebControls.TextBox txtPageRows;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnRowNumber;
		protected System.Web.UI.HtmlControls.HtmlTableRow trGrid;
		protected System.Web.UI.WebControls.TextBox txtNote;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtNoQDCB;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.TextBox txtPCCV;
		protected System.Web.UI.WebControls.TextBox txtHuongCB;
		protected System.Web.UI.WebControls.TextBox txtNangCB;
		protected System.Web.UI.WebControls.Label Label14;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.Label Label16;
		protected System.Web.UI.WebControls.Label Label17;
		protected System.Web.UI.WebControls.Label Label18;
		protected System.Web.UI.WebControls.Label Label19;
		protected System.Web.UI.WebControls.TextBox txtNoQDCD;
		protected System.Web.UI.WebControls.DropDownList cboMaLGCD;
		protected System.Web.UI.WebControls.TextBox txtHesoCD;
		protected System.Web.UI.WebControls.TextBox txtNangCD;
		protected System.Web.UI.WebControls.TextBox txtHuongCD;
		protected System.Web.UI.WebControls.TextBox txtPCVK;
		protected System.Web.UI.WebControls.TextBox txtPCTN;
		protected System.Web.UI.WebControls.Label Label20;
		protected System.Web.UI.WebControls.TextBox txtSalaryOther;
		protected System.Web.UI.WebControls.Label Label21;
		protected System.Web.UI.WebControls.TextBox txtDcHuong;
		protected System.Web.UI.WebControls.TextBox txtPCCL;
		protected System.Web.UI.WebControls.DropDownList cboMaLGCB;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtID;
		protected System.Web.UI.WebControls.RadioButton optCD;
		protected System.Web.UI.WebControls.TextBox txtHesoCB;
		protected System.Web.UI.HtmlControls.HtmlTableRow TrCB;
		protected System.Web.UI.HtmlControls.HtmlTableRow TrCD;
		protected System.Web.UI.WebControls.RadioButton optCB;
		public string strID = "";

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if (Request.Params["ID"] != null)
				strID = Request.Params["ID"].ToString().Trim();
			else
				strID = "1";

			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
			if(!Page.IsPostBack)
			{
				CheckControl(strID);
				Check_Grid(strID);
				LoadDataCombo();
				LoadDataGrid();
				Session["ssStatusSalAdjuct"] = "AddNew";
				if(Session["ssSalaryGradeID"]!=null)
				{
					LoadDataToEdit(Session["ssSalaryGradeID"].ToString().Trim());
					Session["ssSalaryGradeID"]= null;					
				}
			}
			btnSave.Attributes.Add("OnClick", " return validform()");
			btnDelete.Attributes.Add("OnClick", "return checkdelete()");
			btnImport.Attributes.Add("onclick","return ShowExcelSelectPage();");
		}

		#region Kiem tra luoi de cho hien thi CB or CD
		public void Check_Col_Grid_True(DataGrid dtgGrid, int iIndex)
		{
			try
			{
				dtgGrid.Columns[iIndex].Visible = true;
			}
			catch(Exception exp) 
			{
				string strError = exp.Message.ToString();
			}
		}
		public void Check_Col_Grid_False(DataGrid dtgGrid, int iIndex)
		{
			try
			{
				dtgGrid.Columns[iIndex].Visible = false;
			}
			catch(Exception exp) 
			{
				string strError = exp.Message.ToString();
			}
		}
		private void Check_Grid(string strID)
		{
			if (strID == "1")
			{
				Check_Col_Grid_True(grdSalaryGrade,2);
				Check_Col_Grid_True(grdSalaryGrade,3);
				Check_Col_Grid_True(grdSalaryGrade,4);
				Check_Col_Grid_True(grdSalaryGrade,5);
				Check_Col_Grid_True(grdSalaryGrade,6);
				Check_Col_Grid_True(grdSalaryGrade,7);
				Check_Col_Grid_True(grdSalaryGrade,8);

				Check_Col_Grid_False(grdSalaryGrade,9);
				Check_Col_Grid_False(grdSalaryGrade,10);
				Check_Col_Grid_False(grdSalaryGrade,11);
				Check_Col_Grid_False(grdSalaryGrade,12);
				Check_Col_Grid_False(grdSalaryGrade,13);
				Check_Col_Grid_False(grdSalaryGrade,14);
				Check_Col_Grid_False(grdSalaryGrade,15);
				Check_Col_Grid_False(grdSalaryGrade,16);
				Check_Col_Grid_False(grdSalaryGrade,17);
			}
			else
			{
				Check_Col_Grid_False(grdSalaryGrade,2);
				Check_Col_Grid_False(grdSalaryGrade,3);
				Check_Col_Grid_False(grdSalaryGrade,4);
				Check_Col_Grid_False(grdSalaryGrade,5);
				Check_Col_Grid_False(grdSalaryGrade,6);
				Check_Col_Grid_False(grdSalaryGrade,7);
				Check_Col_Grid_False(grdSalaryGrade,8);

				Check_Col_Grid_True(grdSalaryGrade,9);
				Check_Col_Grid_True(grdSalaryGrade,10);
				Check_Col_Grid_True(grdSalaryGrade,11);
				Check_Col_Grid_True(grdSalaryGrade,12);
				Check_Col_Grid_True(grdSalaryGrade,13);
				Check_Col_Grid_True(grdSalaryGrade,14);
				Check_Col_Grid_True(grdSalaryGrade,15);
				Check_Col_Grid_True(grdSalaryGrade,16);
				Check_Col_Grid_True(grdSalaryGrade,17);
			}
		}
		#endregion
		#region Check ID
		private void CheckControl(string strID)
		{
			if (strID == "1")
			{
				this.optCB.Checked = true;
				this.TrCB.Attributes.Add("style","");
				this.TrCD.Attributes.Add("style","DISPLAY: none");
			}
			else
			{
				this.optCD.Checked = true;
				this.TrCB.Attributes.Add("style","DISPLAY: none");
				this.TrCD.Attributes.Add("style","");
			}
		}
		#endregion
		#region Load datagrid
		/// <summary>
		/// LOAD SALARY ADJUST OF EMP
		/// </summary>
		private void LoadDataGrid()
		{
			DataTable dtList = new DataTable();
			try
			{
				dtList = clsPRSalaryGrade.GetDataByEmpID(Session["EmpID"], strID);				
				grdSalaryGrade.DataSource = dtList;
				grdSalaryGrade.CurrentPageIndex = 0;
				grdSalaryGrade.DataBind();
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
		#endregion
		#region Load Combo
		/// <summary>
		/// LOAD DATA FOR COMBO
		/// </summary>
		private void LoadDataCombo()
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			clsCommon.LoadDropDownListControl(cboMaLGCB,"sp_GetDataCombo @TableName='LS_tblSalaryGradeCB',@Fields='MaLGCB, MaLGCB as Name'","MaLGCB","Name",true);
			clsCommon.LoadDropDownListControl(cboMaLGCD,"sp_GetDataCombo @TableName='LS_tblSalaryGradeCD',@Fields='MaLGCD, MaLGCD as Name'","MaLGCD","Name",true);
		}
		#endregion
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
			this.optCB.CheckedChanged += new System.EventHandler(this.optCB_CheckedChanged);
			this.optCD.CheckedChanged += new System.EventHandler(this.optCD_CheckedChanged);
			this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			this.grdSalaryGrade.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.grdSalaryGrade_ItemCommand);
			this.btnRowNumber.ServerClick += new System.EventHandler(this.btnRowNumber_ServerClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		#region tao trang man hinh nhap lieu
		private void btnAddNew_Click(object sender, System.EventArgs e)
		{
			clsCommon.ClearControlValue(this);
			Session["ssStatusSalAdjuct"] = "AddNew";
			if(chkShowGrid.Checked==true)				
				trGrid.Style.Add("DISPLAY","block");
			else
				trGrid.Style.Add("DISPLAY","none");

			//Hau
//			iAddSubtract = optAddSubtract.SelectedIndex;
//			strLSSalaryGradeCode = cboLSSalaryGradeCode.SelectedItem.Text;
//			bPIT = chkPIT.Checked;
//			txtAmount.ToolTip = txtAmount.Text;
//			txtPRMonth.ToolTip = txtPRMonth.Text;
			////
		}
		#endregion
		#region Nut luu du lieu
/// <summary>
/// SAVE DATA TO DB
/// </summary>
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(Session["ssStatusSalAdjuct"].ToString().Trim()=="AddNew")
				{
					if (strID == "1")
						clsCommon.ImpactDB("@Creater",Session["AccountLogin"],Session["EmpID"].ToString().Trim(),"Save_CB",this,"PR_spfrmSALARYGRADE");
					else
						clsCommon.ImpactDB("@Creater",Session["AccountLogin"],Session["EmpID"].ToString().Trim(),"Save_CD",this,"PR_spfrmSALARYGRADE");

					//Hau
					//Save_ChangeLog();
					////
				}
				else
				{
					clsCommon.UpdateByKey("@Editer",Session["AccountLogin"],"ID",txtID.Value.Trim(),"Update",this,"PR_spfrmSALARYGRADE");

					//Hau
					//Save_ChangeLog();
					////
				}
				btnAddNew_Click(null,null);
				LoadDataGrid();
				lblErr.Text = "";
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}
		#endregion
		#region Nut action Edit
		/// <summary>
		/// EDIT COMMAND ACTION
		/// </summary>		
		private void grdSalaryGrade_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				if(e.CommandName.Trim().ToUpper() =="EDIT")
				{
					LoadDataToEdit(grdSalaryGrade.Items[e.Item.ItemIndex].Cells[0].Text.Trim());					

					//Hau
//					iAddSubtract = optAddSubtract.SelectedIndex;
//					strLSSalaryGradeCode = cboLSSalaryGradeCode.SelectedItem.Text;
//					bPIT = chkPIT.Checked;
//					txtAmount.ToolTip = txtAmount.Text;
//					txtPRMonth.ToolTip = txtPRMonth.Text;
					////
				}				
			}	
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}
		#endregion
		#region Load data Edit
		//LOAD RECORD DATA TO EDIT
		private void LoadDataToEdit(string strID)
		{
			try
			{
				txtID.Value = strID;
				DataRow iRow = clsPRSalaryGrade.GetDataByID(strID);
				if(iRow != null)
				{
					txtNoQDCB.Text = iRow["NoQDCB"].ToString().Trim();
					cboMaLGCB.SelectedValue = iRow["MaLGCB"].ToString().Trim();
					txtHesoCB.Text = iRow["HesoCB"].ToString().Trim();
					txtPCCL.Text = iRow["PCCL"].ToString().Trim();
					txtPCCV.Text = iRow["PCCV"].ToString().Trim();
					txtNangCB.Text = iRow["NangCB"].ToString().Trim();
					txtHuongCB.Text = iRow["HuongCB"].ToString().Trim();
					txtNoQDCD.Text = iRow["NoQDCD"].ToString().Trim();
					cboMaLGCD.SelectedValue = iRow["MaLGCD"].ToString().Trim();
					txtHesoCD.Text = iRow["HesoCD"].ToString().Trim();
					txtPCVK.Text = iRow["PCVK"].ToString().Trim();
					txtPCTN.Text = iRow["PCTN"].ToString().Trim();
					txtNangCD.Text = iRow["NangCD"].ToString().Trim();
					txtHuongCD.Text = iRow["HuongCD"].ToString().Trim();
					txtSalaryOther.Text = iRow["SalaryOther"].ToString().Trim();
					txtDcHuong.Text = iRow["DcHuong"].ToString().Trim();
					txtNote.Text = iRow["Note"].ToString().Trim();
				}
				Session["ssStatusSalAdjuct"] = "Edit";
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
		#endregion
		#region Nut xoa du lieu duoi luoi
/// <summary>
/// DELETE CHECKED RECORD
/// </summary>
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				string strID="";
				for(int i=0;i<grdSalaryGrade.Items.Count;i++)
				{
					if(((CheckBox)grdSalaryGrade.Items[i].FindControl("chkSelect")).Checked==true)
					{
						strID += grdSalaryGrade.Items[i].Cells[0].Text.Trim() + "$";
					}
				}
				clsCommon.DeleteListRecord("PR_spfrmSALARYGRADE","ID",SqlDbType.Int,4,strID);
				if(chkShowGrid.Checked==true)
					trGrid.Style.Add("DISPLAY","block");
				else				
					trGrid.Style.Add("DISPLAY","none");
				LoadDataGrid();
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}
		#endregion
		#region Phan dung de thay doi trang duoi luoi
/// <summary>
/// PAGE INDEX CHANGE
/// </summary>
		private void grdSalaryGrade_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			try
			{
				LoadDataGrid();
				grdSalaryGrade.CurrentPageIndex=e.NewPageIndex;
				grdSalaryGrade.DataBind();
			}
			catch(Exception ex)
			{
				string str = ex.Message;
			}
		}
		#endregion
		#region phan dung de xuat du lieu ra Excel
/// <summary>
/// EXPORT DATA ON GRID
/// </summary>
		private void btnExport_Click(object sender, System.EventArgs e)
		{
			iHRPCore.DataGridExcelExporter myExcelXport=new DataGridExcelExporter(grdSalaryGrade);			
			myExcelXport.Export("");
			myExcelXport =null;
		}
		#endregion
		#region Nut show so dong duoi luoi
		private void btnRowNumber_ServerClick(object sender, System.EventArgs e)
		{
			grdSalaryGrade.PageSize = Convert.ToInt32(txtPageRows.Text.Trim()==""?"1":txtPageRows.Text.Trim());
			grdSalaryGrade.CurrentPageIndex = 0;
			LoadDataGrid();
		}
		#endregion
		#region Hau

		static public int iAddSubtract;
		static public String strLSSalaryGradeCode;
		static public Boolean bPIT;

		private void Save_ChangeLog()
		{	
			String strUserName = Session[0].ToString().Trim();
			//DateTime dtime = DateTime.Now;
			String strActionTime = DateTime.Now.ToString();
			String strAction = Session["ssStatusSalAdjuct"].ToString().Trim();
			String strEmpID = Session["EmpID"].ToString();
			DataTable tbl = clsHREmpList.GetEmpList(Session["EmpID"].ToString().Trim(),this.Page,"0");
			String strEmpName = tbl.Rows[0]["EmpName"].ToString();
			String[][] strFieldChanged;
			strFieldChanged = new String[3][];
			strFieldChanged[0] = new String[5];
			strFieldChanged[1] = new String[5];
			strFieldChanged[2] = new String[5];
			int i = 0;
//			if((strAction.ToUpper().Trim().Equals("EDIT") && iAddSubtract != optAddSubtract.SelectedIndex)
//				|| (strAction.ToUpper().Trim().Equals("ADDNEW")))
//			{
//				strFieldChanged[0][i] = "Add/Subtract";
//				strFieldChanged[1][i] = optAddSubtract.Items[iAddSubtract].Text;
//				strFieldChanged[2][i] = optAddSubtract.SelectedItem.Text;
//				i++;
//			}
//			if((strAction.ToUpper().Trim().Equals("EDIT") && !strLSSalaryGradeCode.Trim().Equals(cboLSSalaryGradeCode.SelectedItem.Text.Trim())) 
//				|| strAction.ToUpper().Trim().Equals("ADDNEW") && !cboLSSalaryGradeCode.SelectedItem.Text.Trim().Equals(""))
//			{
//				strFieldChanged[0][i] = "Adjustment type";
//				strFieldChanged[1][i] = strLSSalaryGradeCode;
//				strFieldChanged[2][i] = cboLSSalaryGradeCode.SelectedItem.Text;
//				i++;
//			}		
//			if((strAction.ToUpper().Trim().Equals("EDIT") && bPIT != chkPIT.Checked) 
//				|| strAction.ToUpper().Trim().Equals("ADDNEW"))
//			{
//				strFieldChanged[0][i] = "PIT Pay";
//				strFieldChanged[1][i] = bPIT.ToString();
//				strFieldChanged[2][i] = chkPIT.Checked.ToString();
//				i++;
//			}	
//			if((strAction.ToUpper().Trim().Equals("EDIT") && !txtAmount.ToolTip.Trim().Equals(txtAmount.Text.Trim()))
//				|| (strAction.ToUpper().Trim().Equals("ADDNEW") && !txtAmount.Text.Trim().Equals("")))
//			{
//				strFieldChanged[0][i] = "Amount";
//				strFieldChanged[1][i] = txtAmount.ToolTip;
//				strFieldChanged[2][i] = txtAmount.Text;
//				i++;
//			}
//			if((strAction.ToUpper().Trim().Equals("EDIT") && !txtPRMonth.ToolTip.Trim().Equals(txtPRMonth.Text.Trim())) 
//				|| strAction.ToUpper().Trim().Equals("ADDNEW") && !txtPRMonth.Text.Trim().Equals(""))
//			{
//				strFieldChanged[0][i] = "PR month";
//				strFieldChanged[1][i] = txtPRMonth.ToolTip;
//				strFieldChanged[2][i] = txtPRMonth.Text;
//				i++;
//			}		
			
			SaveLog(strUserName, strActionTime, strEmpID, strEmpName, strAction, strFieldChanged, i);
		}

		private void SaveLog(String UserName, String ActionTime, String EmpID, String EmpName, String UserAction, String[][] FieldChanged, int FieldCount)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			
			String CommandText = "insert into ActionLog values(N'";
			CommandText += (UserName + "', '" + ActionTime + "', N'" + UserAction + "', '" + EmpID + "', N'" + EmpName + "', N'");
			try
			{
				for(int i=0; i<FieldCount; i++)
				{
					cmd.CommandText = CommandText + FieldChanged[0][i] + "', N'" + FieldChanged[1][i] + "', N'" + FieldChanged[2][i] + "')";
					cmd.ExecuteNonQuery();
				}
				sqlTran.Commit();
			}
			catch //(Exception exp)
			{
				//lblErr.Text = exp.Message;
				sqlTran.Rollback();
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();
			}
		}

		#endregion

		private void optCB_CheckedChanged(object sender, System.EventArgs e)
		{
			string strPramas = "";
			string strPaths = Request.Url.PathAndQuery.ToString();
			string strPath = Request.Url.PathAndQuery.ToString();

			string[] mArray = strPath.Split(new Char [] {'&',';'});
			
			if (mArray.Length > 1)
			{
				if (mArray.Length > 4)
				{
					for(int i=0;i<(mArray.Length-1);i++)
					{
						if (strPramas != "")
							strPramas += "&";
						strPramas += mArray.GetValue(i).ToString().Trim();
					}
				}
				else
				{
					strPramas = strPaths;
				}
			}
			Response.Redirect(strPramas + "&ID=1");
		}

		private void optCD_CheckedChanged(object sender, System.EventArgs e)
		{
			string strPramas = "";
			string strPaths = Request.Url.PathAndQuery.ToString();
			string strPath = Request.Url.PathAndQuery.ToString();

			string[] mArray = strPath.Split(new Char [] {'&',';'});
			
			if (mArray.Length > 1)
			{
				if (mArray.Length > 4)
				{
					for(int i=0;i<(mArray.Length-1);i++)
					{
						if (strPramas != "")
							strPramas += "&";
						strPramas += mArray.GetValue(i).ToString().Trim();
					}
				}
				else
				{
					strPramas = strPaths;
				}
			}
			Response.Redirect(strPramas + "&ID=0");
		}
	}
}
