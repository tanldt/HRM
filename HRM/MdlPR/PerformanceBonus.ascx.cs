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
	using iHRPCore.Include;
	using iHRPCore.Com;
	using iHRPCore.TSComponent;
	using iHRPCore.HRComponent;
	using iHRPCore.TMSComponent;
	using GridSort;
	using iHRPCore.PRComponent;

	/// <summary>
	///		Summary description for PerformanceBonus.
	/// </summary>
	public class PerformanceBonus : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList cboLSPerformanceTypeID;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnCalculate;
		protected System.Web.UI.WebControls.LinkButton btnSearch;
		protected System.Web.UI.WebControls.TextBox txtBonusPercent;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtSeniority;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.DataGrid dtgList;

		protected EmpHeaderSearch EmpHeaderSearch1;
		protected ColumnList uctrlColumns;
		protected System.Web.UI.WebControls.TextBox txtMM;
		protected System.Web.UI.WebControls.TextBox txtYYYY;		
		private string strLanguage = "EN";

		private void Page_Load(object sender, System.EventArgs e)
		{
			strLanguage = Session["LangID"]!=null ? Session["LangID"].ToString().Trim() : "EN";	

			if (!Page.IsPostBack)
			{
				LoadDataCombo();

				this.dtgList.DataSource= new DataTable();
				this.dtgList.DataBind();

				Session["ssStatus"] = "Search";
			}

			/*
			btnSave.Attributes.Add("OnClick", " return checksave()");		
			btnDelete.Attributes.Add("OnClick", "return checkdelete()");			
			btnSearch.Attributes.Add("OnClick","return validform()");
			btnCalculate.Attributes.Add("OnClick","return validform()");	
			*/
		}

		private void LoadDataCombo()
		{			
			clsCommon.LoadDropDownListControl(cboLSPerformanceTypeID,"sp_GetDataCombo @TableName='LS_tblPerformanceType',@Fields='PercentBonus as [ID],LSPerformanceTypeCode as [Name]'","ID","Name",true);			
		}

		private void BindDataGrid()
		{			
			string strEmpID = this.EmpHeaderSearch1.txtEmpID.Text.Trim().Replace("'","");
			string strEmpName = this.EmpHeaderSearch1.txtEmpName.Text.Trim();
			string strCompanyID = this.EmpHeaderSearch1.cboCompany.SelectedValue.Trim();
			string strLevel1ID = this.EmpHeaderSearch1.cboLevel1.SelectedValue.Trim();
			string strLevel2ID = this.EmpHeaderSearch1.cboLevel2.SelectedValue.Trim();
			string strLevel3ID = this.EmpHeaderSearch1.cboLevel3.SelectedValue.Trim();						
			string strLSEmpTypeID = this.EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue.Trim();
			string strStatus = this.EmpHeaderSearch1.optStatus.SelectedValue.Trim();

			string strMM = this.txtMM.Text.Trim();
			string strYYYY = this.txtYYYY.Text.Trim();
			string strSeniority = this.txtSeniority.Text.Trim();
			string strLSPerformanceTypeID = this.cboLSPerformanceTypeID.SelectedValue.Trim();
			string strBonusPercent = this.txtBonusPercent.Text.Trim();
			
			string strSQL = "PR_spfrmPerformanceBonus @Activity = 'SearchEmp', @LanguageID='"+strLanguage+"'";
			strSQL +=", @EmpID ='" + strEmpID + "', @EmpName ='" + strEmpName +	"', @LSCompanyID = '" + strCompanyID + "', @LSLevel1ID = '" + strLevel1ID +	"', @LSLevel2ID = '" + strLevel2ID + "', @LSLevel3ID = '" + strLevel3ID + "', @LSEmpTypeID = '" + strLSEmpTypeID + "',@Status = '" + strStatus+"'";
			strSQL +=", @MM = '" + strMM +"', @YYYY='"+strYYYY+"', @Seniority='"+strSeniority+"', @Type='"+strLSPerformanceTypeID+"', @BonusPercent='"+strBonusPercent+"'";
			
			DataTable dtList = new DataTable();
			try
			{									
				dtList = clsCommon.GetDataTable(strSQL);
				dtgList.DataSource = dtList;
				dtgList.DataBind();
								
				#region SortGrid
				this.dtgList.PageSize = uctrlColumns.iPageRows;
				if(this.uctrlColumns.bAdvMultiSort)
				{
					try
					{
						DataGridSort.AdvancedMultiSort(this.dtgList, dtList, this.uctrlColumns);
					}
					catch
					{
						this.dtgList.CurrentPageIndex = 0;
						DataGridSort.AdvancedMultiSort(this.dtgList, dtList, this.uctrlColumns);
					}
				}
				else
				{
					try
					{
						DataGridSort.Refresh(dtgList, dtList);
					}
					catch
					{
						dtgList.CurrentPageIndex = 0;
						DataGridSort.Refresh(dtgList, dtList);				
					}
				}
				this.uctrlColumns.iTotalRows = dtList.Rows.Count;
				#endregion
					
			}
			catch(Exception ex)
			{
				clsChangeLang.popupWindowExp_Mess(this.Parent,ex);
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
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		
		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			this.dtgList.CurrentPageIndex = 0;
			BindDataGrid();
			Session["ssStatus"] = "Search";
		}

		private void btnCalculate_Click(object sender, System.EventArgs e)
		{
			Session["ssStatus"] = "Calculate";
			string strEmpID = this.EmpHeaderSearch1.txtEmpID.Text.Trim().Replace("'","");
			string strEmpName = this.EmpHeaderSearch1.txtEmpName.Text.Trim();
			string strCompanyID = this.EmpHeaderSearch1.cboCompany.SelectedValue.Trim();
			string strLevel1ID = this.EmpHeaderSearch1.cboLevel1.SelectedValue.Trim();
			string strLevel2ID = this.EmpHeaderSearch1.cboLevel2.SelectedValue.Trim();
			string strLevel3ID = this.EmpHeaderSearch1.cboLevel3.SelectedValue.Trim();						
			string strLSEmpTypeID = this.EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue.Trim();
			string strStatus = this.EmpHeaderSearch1.optStatus.SelectedValue.Trim();

			string strMM = this.txtMM.Text.Trim();
			string strYYYY = this.txtYYYY.Text.Trim();
			string strSeniority = this.txtSeniority.Text.Trim();
			string strLSPerformanceTypeID = this.cboLSPerformanceTypeID.SelectedValue.Trim();
			string strBonusPercent = this.txtBonusPercent.Text.Trim();
			
			string strSQL = "PR_spfrmPerformanceBonus @Activity = 'Calculate', @LanguageID='"+strLanguage+"'";
			strSQL +=", @EmpID ='" + strEmpID + "', @EmpName ='" + strEmpName +	"', @LSCompanyID = '" + strCompanyID + "', @LSLevel1ID = '" + strLevel1ID +	"', @LSLevel2ID = '" + strLevel2ID + "', @LSLevel3ID = '" + strLevel3ID + "', @LSEmpTypeID = '" + strLSEmpTypeID + "',@Status = '" + strStatus+"'";
			strSQL +=", @MM = '" + strMM +"', @YYYY='"+strYYYY+"', @Seniority='"+strSeniority+"', @Type='"+strLSPerformanceTypeID+"', @BonusPercent='"+strBonusPercent+"'";

			DataTable dtList = new DataTable();
			try
			{									
				dtList = clsCommon.GetDataTable(strSQL);
				dtgList.DataSource = dtList;
				dtgList.DataBind();
								
				#region Sort Grid
				this.dtgList.PageSize = uctrlColumns.iPageRows;
				if(this.uctrlColumns.bAdvMultiSort)
				{
					try
					{
						DataGridSort.AdvancedMultiSort(this.dtgList, dtList, this.uctrlColumns);
					}
					catch
					{
						this.dtgList.CurrentPageIndex = 0;
						DataGridSort.AdvancedMultiSort(this.dtgList, dtList, this.uctrlColumns);
					}
				}
				else
				{
					try
					{
						DataGridSort.Refresh(dtgList, dtList);
					}
					catch
					{
						dtgList.CurrentPageIndex = 0;
						DataGridSort.Refresh(dtgList, dtList);				
					}
				}
				this.uctrlColumns.iTotalRows = dtList.Rows.Count;
				#endregion												
			}
			catch(Exception ex)
			{
				clsChangeLang.popupWindowExp_Mess(this.Parent,ex);
			}
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			/*
			try
			{
				string strReturnMsg = "";
				string strResult="";

				string strFestivalBonusID ="";
				string strEmpID="";		
				string strMMYYYY ="";
				string strNetTakeHome="";
				string strAmountUSD= "";
				string strAmountVND= "";								
				string strNote= "";
				string sAccountLogin="";

				if(Session["AccountLogin"]!=null)
					sAccountLogin = Session["AccountLogin"].ToString();

				for(int i = 0; i < dtgList.Items.Count; i++)
				{
					if(((CheckBox)this.dtgList.Items[i].FindControl("chkSelect")).Checked)//row check chon 
					{
						strFestivalBonusID = dtgList.Items[i].Cells[0].Text.Trim().Replace("&nbsp;","");	
						strEmpID = dtgList.Items[i].Cells[1].Text.Trim().Replace("&nbsp;","");						
						strMMYYYY = this.txtMMYYYY.Text.Trim();
						strNetTakeHome = dtgList.Items[i].Cells[2].Text.Trim().Replace("&nbsp;","");						
																													
						strAmountUSD = dtgList.Items[i].Cells[9].Text.Replace(",","");
						strAmountVND = dtgList.Items[i].Cells[10].Text.Replace(",","");

						strNote=((TextBox)dtgList.Items[i].FindControl("txtdtgListNote")).Text;
						
						string strSQL = "PR_spfrmFestivalBonus @Activity = 'Save', @FestivalBonusID='" +strFestivalBonusID+ "', @EmpID ='" +strEmpID+"',@MMYYYY='" +strMMYYYY+ "',@NetTakeHome='" +strNetTakeHome+ "',@AmountUSD='" +strAmountUSD +"',@AmountVND='" +strAmountVND+ "', @Note=N'"+strNote+"',@ReturnMess='" +strReturnMsg+ "'";
						clsCommon.Exc_CommandText(strSQL);
						if (strReturnMsg!="")
							strResult+= strReturnMsg;				
					}					
				}				
				// kiem tra ket qua execute query va xuat thong bao	
				if(strResult ==  "")
				{																			
					clsChangeLang.popupWindow(this.Parent,"Cập nhật thành công", "",1);
					BindDataGrid();
					Session["ssStatus"] = "Search";
				}
				else
					clsChangeLang.popupWindow(this.Parent, strResult, "", 1);					
					
			}
			catch(Exception ex)
			{
				clsChangeLang.popupWindowExp_Mess(this.Parent,ex);
			}
*/
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				string sReturnMsg = "";
				string sFestivalBonusID = "";
								
				for(int i = 0; i<this.dtgList.Items.Count; i++)
				{
					if(((CheckBox)this.dtgList.Items[i].FindControl("chkSelect")).Checked)
					{		
						sFestivalBonusID += dtgList.Items[i].Cells[0].Text.Trim() + "$";
					}
				}

				bool iResult = clsCommon.DeleteListRecord("PR_spfrmFestivalBonus","FestivalBonusID",SqlDbType.NVarChar,24,sFestivalBonusID);				
				
				if(iResult)
				{
					//clsChangeLang.popupWindow(this.Parent,"Deleted successfull!","",1);
					BindDataGrid();
				}
				else
				{										
					clsChangeLang.popupWindow(this.Parent, "This record is not existed in the system!", "", 0);
				}
			}
			catch(Exception ex)
			{
				clsChangeLang.popupWindowExp_Mess(this.Parent,ex);
			}
		}

		private void btnExport_Click(object sender, System.EventArgs e)
		{
			iHRPCore.DataGridExcelExporter myExcelXport=new iHRPCore.DataGridExcelExporter(dtgList);			
			myExcelXport.Export("");
			myExcelXport =null;
		}


		#region Sort_Hau
		private void ButtonClick()
		{
			this.uctrlColumns.ButtonSort.ServerClick += new EventHandler(ButtonSort_ServerClick);
			this.uctrlColumns.ButtonPRChanged.ServerClick += new EventHandler(ButtonPRChanged_ServerClick);
		}
		private DataTable ReadData()
		{
			string strEmpID = this.EmpHeaderSearch1.txtEmpID.Text.Trim().Replace("'","");
			string strEmpName = this.EmpHeaderSearch1.txtEmpName.Text.Trim();
			string strCompanyID = this.EmpHeaderSearch1.cboCompany.SelectedValue.Trim();
			string strLevel1ID = this.EmpHeaderSearch1.cboLevel1.SelectedValue.Trim();
			string strLevel2ID = this.EmpHeaderSearch1.cboLevel2.SelectedValue.Trim();
			string strLevel3ID = this.EmpHeaderSearch1.cboLevel3.SelectedValue.Trim();						
			string strLSEmpTypeID = this.EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue.Trim();
			string strStatus = this.EmpHeaderSearch1.optStatus.SelectedValue.Trim();

			string strMM = this.txtMM.Text.Trim();
			string strYYYY = this.txtYYYY.Text.Trim();
			string strSeniority = this.txtSeniority.Text.Trim();
			string strLSPerformanceTypeID = this.cboLSPerformanceTypeID.SelectedValue.Trim();
			string strBonusPercent = this.txtBonusPercent.Text.Trim();
			
			string strSQL1 = "PR_spfrmPerformanceBonus @Activity = 'SearchEmp', @LanguageID='"+strLanguage+"'";
			strSQL1 +=", @EmpID ='" + strEmpID + "', @EmpName ='" + strEmpName +	"', @LSCompanyID = '" + strCompanyID + "', @LSLevel1ID = '" + strLevel1ID +	"', @LSLevel2ID = '" + strLevel2ID + "', @LSLevel3ID = '" + strLevel3ID + "', @LSEmpTypeID = '" + strLSEmpTypeID + "',@Status = '" + strStatus+"'";
			strSQL1 +=", @MM = '" + strMM +"', @YYYY='"+strYYYY+"', @Seniority='"+strSeniority+"', @Type='"+strLSPerformanceTypeID+"', @BonusPercent='"+strBonusPercent+"'";
			try
			{
				DataTable dtList = new DataTable();

	
				if (Session["ssStatus"] == "Search")
				{
					dtList = clsCommon.GetDataTable(strSQL1);
				}
				else //(Session["ssStatus"] == "Calculate")
				{
					//dtList = clsCommon.GetDataTable("PR_spfrmSALARY13TH @Activity = 'Calculate_Salary13th', @YYYY = '" + sYYYY +	"', @EmpID ='" + sEmpID + "', @EmpName ='" + sEmpName +	"', @LSCompanyID = '" + sCompanyID + "', @LSLevel1ID = '" + sLevel1ID +	"', @LSLevel2ID = '" + sLevel2ID + "', @LSLevel3ID = '" + sLevel3ID + "', @LSJobTitleID = '" + sJobtitleID + "', @Status = '" + sStatus + "',@LanguageID='"+strLanguage+"'");
				}



				return dtList;
			}
			catch(Exception ex)
			{
				return null;
			}
		}//end ReadData()

		private void dtgList_SortCommand(object source, DataGridSortCommandEventArgs e)
		{
			try
			{
				DataGridSort.AdvancedMultiSort(dtgList, ReadData(), uctrlColumns);
			}
			catch(Exception ex)

			{
				//clsChangeLang.popupWindow(this.Parent, "Sort Error", "", 1);
				clsChangeLang.popupWindowExp_Mess(this.Parent,ex);
			}
			
			try
			{
				DataTable dtList = this.ReadData();
				DataGridSort.Grid_Sort(dtgList, dtList, e, this.uctrlColumns);
				
			}
	
			catch (Exception ex) 
			{
				//clsChangeLang.popupWindow(this.Parent, "Sort Error", "", 1);
			}
			
		}

		private void dtgList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.dtgList.CurrentPageIndex = e.NewPageIndex;
				
			try
			{
				DataTable dtList = this.ReadData();
				DataGridSort.Grid_IndexChanged(dtgList, dtList, e);
			
			}
			catch
			{
			}
		}

		private void ButtonPRChanged_ServerClick(object sender, EventArgs e)
		{
			try
			{
				dtgList.PageSize = uctrlColumns.iPageRows;
				dtgList.CurrentPageIndex = 0;
				DataView dv = new DataView(ReadData());
				//dv.Sort = DataGridSort.sMulSort;
				dtgList.DataSource = dv;
				dtgList.DataBind();
				
				
			}
			catch(Exception ex)
			{
				clsChangeLang.popupWindowExp_Mess(this.Parent,ex);
			}
			
		}

		private void ButtonSort_ServerClick(object sender, EventArgs e)
		{
			try
			{
				DataTable dtList = this.ReadData();
				DataGridSort.AdvancedMultiSort(dtgList, dtList, uctrlColumns);
		
			}
			catch
			{
				clsChangeLang.popupWindow(this.Parent, "Sort Error", "", 1);
			}
		}
		#endregion
	}
}
