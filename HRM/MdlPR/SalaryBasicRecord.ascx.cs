namespace iHRPCore.MdlPR
{
	#region Using
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
	using System.Configuration;
	using System.IO;
	using System.Globalization;
	using GridSort;
	#endregion
	/// <summary>
	///		Summary description for SalaryBasicRecord.
	/// </summary>
	public class SalaryBasicRecord : System.Web.UI.UserControl
	{
		#region Variables

		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label Label19;
		protected System.Web.UI.WebControls.Label Label25;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label26;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label32;
		protected System.Web.UI.WebControls.Label Label33;
		protected System.Web.UI.WebControls.Label Label23;
		protected System.Web.UI.WebControls.LinkButton btnAddNew;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.LinkButton btnImport;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.TextBox txtEffDate;
		protected System.Web.UI.WebControls.TextBox txtEndDate;
		protected System.Web.UI.WebControls.TextBox txtOldSalary;
		protected System.Web.UI.WebControls.TextBox txtRaisePercent;
		protected System.Web.UI.WebControls.Label lblEffDate;
		protected System.Web.UI.WebControls.Label lblEndDate;
		protected System.Web.UI.WebControls.CheckBox chkAdAll2;
		protected System.Web.UI.WebControls.Label lblAllowanceCoef2;
		protected System.Web.UI.WebControls.CheckBox chkAdAll1;
		protected System.Web.UI.WebControls.Label lblAllowanceCoef1;
		protected System.Web.UI.WebControls.Label lblNote;
		protected System.Web.UI.WebControls.TextBox txtNote;
		protected System.Web.UI.WebControls.DataGrid dtgList;
		protected System.Web.UI.WebControls.TextBox txtAttachFile;
		protected System.Web.UI.HtmlControls.HtmlTableRow trGrid;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtBasicSalaryID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLSBSRankID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLSBSGradeID;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtNewSalary;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtNewSalary_Hidden;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtSelectedIndex;
		protected System.Web.UI.WebControls.CheckBox chkSelectAll;		
		public string strLanguage = "VN";
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtActualDate;
		protected ColumnList uctrlColumns;

		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!Page.IsPostBack)
			{
				BindDataGrid();
				//txtEffDate.Text = DateTime.Now.ToString("dd/MM/yyyy", DateTimeFormatInfo.InvariantInfo);
				Session["Status"] = "AddNew";
				//Hau
				DataGridSort.AddItemColumn(uctrlColumns, dtgList);//				
			}
			//Hau
			ButtonClick();//
			btnSave.Attributes.Add("onclick","return CheckSave()");
			btnDelete.Attributes.Add("onclick","return CheckDelete()");	
			btnImport.Attributes.Add("onclick","return ShowExcelSelectPage();");
		}
		private void BindDataGrid()
		{
			DataTable dtb = new DataTable();
			string strEmpID = (Session["EmpID"] == null)? "": Session["EmpID"].ToString();
			string strSQL = "PR_spfrmBASICSALARY @Activity='GetForGrid', @EmpID = '" + strEmpID + "'";

			dtb = clsCommon.GetDataTable(strSQL);
			if (dtb != null)
			{
				this.dtgList.DataSource = dtb;
				this.dtgList.DataBind();
				
				//Hau
				this.dtgList.PageSize = uctrlColumns.iPageRows;
				
				if(this.uctrlColumns.bAdvMultiSort)
				{
					try
					{
						DataGridSort.AdvancedMultiSort(dtgList, dtb, uctrlColumns);
					}
					catch
					{
						this.dtgList.CurrentPageIndex = 0;
						DataGridSort.AdvancedMultiSort(dtgList, dtb, uctrlColumns);
					}
				}
				else
				{
					try
					{
						DataGridSort.Refresh(dtgList, dtb);
					}
					catch
					{
						this.dtgList.CurrentPageIndex = 0;
						DataGridSort.Refresh(dtgList, dtb);
					}
				}
				//					dtgList.DataBind();
				this.uctrlColumns.iTotalRows = dtb.Rows.Count;//

				if (dtb.Rows.Count == 0)
				{
					txtOldSalary.Text = "0";
					//txtNewSalary_Hidden.Value = "0";
					Session["LastNewSalary"] = "0";
					EnableControls(false);
				}
				else
				{
					txtOldSalary.Text = dtb.Rows[0]["NewSalary"].ToString();
					//txtNewSalary_Hidden.Value = dtb.Rows[0]["NewSalary"].ToString();
					Session["LastNewSalary"] = dtb.Rows[0]["NewSalary"].ToString();
					EnableControls(true);
					if(dtgList.Items.Count > 0)
					{
						((CheckBox)dtgList.Items[0].FindControl("chkSelect")).Enabled = true;
					}

				}
				txtSelectedIndex.Value = "";
			}
			else
				this.lblErr.Text = "Load data unsuccessfully !";
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
			this.dtgList.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgList_ItemCommand);
			this.dtgList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgList_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnAddNew_Click(object sender, System.EventArgs e)
		{
			clsCommon.ClearControlValue(this);			
			txtOldSalary.Text = (Session["LastNewSalary"] == null)? "0": Session["LastNewSalary"].ToString();
			DisableControls(false);
			lblErr.Text="";
			txtSelectedIndex.Value = "";
			Session["Status"] = "AddNew";						
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			string strMsg = "";			

			if (Session["Status"] == "AddNew")
				strMsg = clsCommon.sImpactDB("@Creater",Session["AccountLogin"].ToString(), Session["EmpID"].ToString(), "Save", this, "PR_spfrmBASICSALARY");				
			else if (Session["Status"] == "Edit")
			{
				strMsg = clsCommon.sUpdateByKey("@Creater",Session["AccountLogin"].ToString(), "BasicSalaryID" , txtID.Value, "Update", this, "PR_spfrmBASICSALARY");
			}

			if (strMsg == "")
			{
				clsChangeLang.popupWindow(this.Parent,"0044","EN","",1);
				BindDataGrid();
				txtSelectedIndex.Value = "";
				//txtNewSalary_Hidden.Value = txtNewSalary.Text;
				btnAddNew_Click(null, null);
			}
			else
				clsChangeLang.popupWindowCataLog(this.Parent, strMsg);
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			string strID="";
			for(int i=0;i<dtgList.Items.Count;i++)
			{
				if(((CheckBox)dtgList.Items[i].FindControl("chkSelect")).Checked==true)
				{
					strID += dtgList.Items[i].Cells[0].Text.Trim() + "$";
				}
			}

			if (clsCommon.DeleteListRecord("PR_spfrmBasicSalary", "BasicSalaryID", SqlDbType.NVarChar, 12, strID))
			{
				BindDataGrid();
				clsChangeLang.popupWindow(this.Parent,"0104","EN","",1);
			}
			else
				clsChangeLang.popupWindow(this.Parent,"Error occurs when deleting data !!","Notice",1);
			btnAddNew_Click(null, null);
		}

		private void btnImport_Click(object sender, System.EventArgs e)
		{
		
		}

		private void btnExport_Click(object sender, System.EventArgs e)
		{
			iHRPCore.DataGridExcelExporter myExcelXport = new DataGridExcelExporter(dtgList);			
			myExcelXport.Export();						
		}

		private void dtgList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			BindDataGrid();
			dtgList.CurrentPageIndex = e.NewPageIndex;
			dtgList.DataBind();
			if(dtgList.Items.Count > 0 && dtgList.CurrentPageIndex==0)
			{
				((CheckBox)dtgList.Items[0].FindControl("chkSelect")).Enabled = true;
			}
		}

		private void dtgList_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				if (e.CommandName.Trim().ToUpper() == "EDIT")
				{
					txtID.Value = dtgList.Items[e.Item.ItemIndex].Cells[0].Text;
					string sql = "PR_spfrmBASICSALARY @Activity = 'GetDataByID', @BasicSalaryID = '" + dtgList.Items[e.Item.ItemIndex].Cells[0].Text + "'";
					DataRow iRow = clsCommon.GetDataRow(sql);
					if (iRow != null)
					{
						txtEffDate.Text = iRow["EffDate"].ToString();
						txtEndDate.Text	= iRow["EndDate"].ToString();
						txtOldSalary.Text = iRow["OldSalary"].ToString();
						txtRaisePercent.Text = iRow["RaisePercent"].ToString();
						txtNewSalary.Text = iRow["NewSalary"].ToString();
						txtNote.Text = iRow["Note"].ToString();

						txtSelectedIndex.Value = e.Item.ItemIndex.ToString();
						txtNewSalary_Hidden.Value = iRow["OldSalary"].ToString();
						if (e.Item.ItemIndex > 0)
							DisableControls(true);
						else
							DisableControls(false);
					}					
				}
				Session["Status"] = "Edit";
			}
			catch(Exception ex)
			{
				clsChangeLang.popupWindowCataLog(this.Parent, ex.Message);
			}
		}

			private string import_file()
		{
			return "";
		//---copy file to server
/*			string mext = "";
			mext = this.txtFile.Value.Substring(this.txtFile.Value.LastIndexOf("."));
			if(mext != ".xls" && mext!= ".doc" )
			{
				//lblErr.Text = "File phải có đuôi là (.xls) hoặc (.doc).";
				clsChangeLang.popupWindow(this.Parent,"0025",strLanguage,"",0);
				return "";
			}
			string strFiletmp="";			
			try 
			{
				if (this.txtFile.Value != "")
				{
					strFiletmp = Session["EmpID"]+ "_"+ txtSignDate.Text.Replace("/","") + "_" + "SalRec" + Path.GetExtension(this.txtFile.Value).Trim();
					if (System.IO.File.Exists(Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrImportFileFolder"].Trim() + strFiletmp))
						System.IO.File.SetAttributes(Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrImportFileFolder"].Trim() + strFiletmp, System.IO.FileAttributes.Normal) ;
					this.txtFile.PostedFile.SaveAs(Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrImportFileFolder"].Trim() + strFiletmp);
					System.IO.File.SetAttributes(Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrImportFileFolder"].Trim() + strFiletmp, System.IO.FileAttributes.Normal) ;
					return strFiletmp;
				}
				else
				{
					lblErr.Text = "Nhập đường dẫn của file!";
					return "null";
				}				
			}
			catch (Exception exp)
			{
				this.lblErr.Text=exp.Message;
				return "";
			}
			//-- end copy file*/
		}

		private void EnableControls(bool flag)
		{
			btnDelete.Enabled = flag;
			btnExport.Enabled = flag;
		}

		private void DisableControls(bool flag)
		{
			txtEffDate.Enabled = !flag;
			txtRaisePercent.Enabled = !flag;
			txtNewSalary.Enabled = !flag;
			txtNote.Enabled = !flag;
			btnSave.Enabled = !flag;
		}
		#region Cac su kien xu li thao tac Sort (Hau)

		private void dtgList_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			try
			{
				DataGridSort.Grid_Sort(dtgList, Read_Data(), e, uctrlColumns);
			}
			catch{}
		}

		private DataTable Read_Data()
		{
			string strEmpID = (Session["EmpID"] == null)? "": Session["EmpID"].ToString();
			string strSQL = "PR_spfrmBASICSALARY @Activity='GetForGrid', @EmpID = '" + strEmpID + "'";

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
				DataGridSort.AdvancedMultiSort(dtgList, Read_Data(), uctrlColumns);
			}
			catch{}
		}

		private void ButtonPRChanged_ServerClick(object sender, EventArgs e)
		{
			try
			{
				dtgList.PageSize = uctrlColumns.iPageRows;
				dtgList.CurrentPageIndex = 0;
				DataView dv = new DataView(Read_Data());
				dv.Sort = DataGridSort.sMulSort;
				dtgList.DataSource = dv;
				dtgList.DataBind();
				if(dtgList.Items.Count > 0 && dtgList.CurrentPageIndex==0)
				{
					((CheckBox)dtgList.Items[0].FindControl("chkSelect")).Enabled = true;
				}
			}
			catch{}
		}
		#endregion		
	}
}
