namespace iHRPCore.MdlHR
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.Com;
	using iHRPCore.Include;
	using iHRPCore.HRComponent;
	using GridSort;

	/// <summary>
	///		Summary description for EmpPopup.
	/// </summary>
	public class EmpPopup : System.Web.UI.UserControl
	{
		#region declare
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.LinkButton btnSearch;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnClose1;
		protected System.Web.UI.HtmlControls.HtmlGenericControl divList;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBtn;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.DropDownList cboCol1;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.DropDownList cboCol2;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.DropDownList cboCol3;
		protected System.Web.UI.WebControls.DataGrid dtgGridSelect;
		protected System.Web.UI.WebControls.DataGrid dtgGridSelected;
		protected System.Web.UI.WebControls.LinkButton butExport;
		protected System.Web.UI.HtmlControls.HtmlTableRow trListSelected;
		protected System.Web.UI.WebControls.LinkButton btnClose;
		protected EmpHeaderSearch EmpHeaderSearch1;
		protected System.Web.UI.WebControls.LinkButton btnSelect;
		protected System.Web.UI.WebControls.LinkButton btnSelectAll;
		protected System.Web.UI.WebControls.LinkButton btnRemove;
		protected System.Web.UI.WebControls.LinkButton btnRemoveAll;
		protected System.Web.UI.WebControls.LinkButton btnChoose;
		protected System.Web.UI.WebControls.Literal ltlAlert;
		protected ColumnList uctrlColumns;
		string strEmpID = "";
		string strEmpName = "";
		string strLevel1 = "";
		string strLevel2 = "";
		string strLevel3 = "";
		string strLocation = "";
		string strJobCode = "";
		string strPosition = "";
		string strCompany = "";
		string sGetAll="";
		string sEmpTypeID="";
		protected System.Web.UI.WebControls.TextBox txtPageLoad;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtWhereCondition;
		string strStatus = "";
		#endregion
		
		#region Page_Load
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				txtPageLoad.Text = "1";
				//Session luu giu hai bang tam gan du lieu vao hai grid select va selected
				Session.Remove("tblSelect");
				Session.Remove("tblSelected");
				//end
				if (Request.Params["MultiSelect"] != null)
				{
					trBtn.Attributes.Remove("style");
					trListSelected.Attributes.Remove("style");
					this.dtgGridSelect.Columns[dtgGridSelect.Columns.Count - 1].Visible = true;
					btnClose1.Visible=false;
					divList.Style["HEIGHT"]= "140px";
				}
				else
				{
					btnClose1.Visible=true;
					divList.Style["HEIGHT"]= "430px";
					trBtn.Attributes.Add("style","display:none");
					trListSelected.Attributes.Add("style","display:none");
					this.dtgGridSelect.Columns[dtgGridSelect.Columns.Count - 1].Visible = false;
				}
				//Neu trang goi co truyen MaNV hoac TenNV
				if (Request.Params["EmpID"] != null)
					EmpHeaderSearch1.txtEmpID.Text = Request.Params["EmpID"].Trim();
				
				if (Request.Params["EmpName"] != null)
					EmpHeaderSearch1.txtEmpName.Text = Request.Params["EmpName"].Trim();
				//Cac form chi goi popup co truyen dieu kien where, truyen qua params["WhereCondition"]
				if (Request.Params["WhereCondition"] != null)
					txtWhereCondition.Value = Request.Params["WhereCondition"].Trim();
				else
				{
					if (Session["Where"] != null)
					{
						txtWhereCondition.Value = Session["Where"].ToString().Trim();
						Session.Remove("Where");
					}
					else
						txtWhereCondition.Value = "";
				}
				//end truyen dieu kien where
				//Hau
				try
				{
					DataGridSort.AddItemColumn(uctrlColumns, dtgGridSelect);
				}
				catch{}//
			}
			if (Request.Params["getAll"]!=null)
				sGetAll="1";
			//Hau
			ButtonClick();//

			btnClose.Attributes.Add("OnClick","window.close()");
			this.btnSelect.Attributes.Add("OnClick","return CheckGrid('dtgGridSelect')");
			this.btnRemove.Attributes.Add("OnClick","return CheckGrid('dtgGridSelected')");
			this.btnChoose.Attributes.Add("OnClick","return CheckGrid('dtgGridSelected')");
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
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.butExport.Click += new System.EventHandler(this.butExport_Click);
			this.dtgGridSelect.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgGridSelect_ItemCommand);
			this.dtgGridSelect.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgGridSelect_PageIndexChanged);
			this.dtgGridSelect.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dtgGridSelect_SortCommand);
			this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
			this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			this.btnRemoveAll.Click += new System.EventHandler(this.btnRemoveAll_Click);
			this.btnChoose.Click += new System.EventHandler(this.btnChoose_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region Tao bang co cau truc
		private void SetUpStructureTable(string strSession, DataTable tblData)
		{
			DataTable tbl = new DataTable();
			try
			{
				tbl.Columns.Add("EmpID");
				tbl.Columns.Add("EmpName");
				tbl.Columns.Add("StartDateStr");
				tbl.Columns.Add("EmpCode");
				tbl.Columns.Add("VFirstName");
				tbl.Columns.Add("VLastName");
				tbl.Columns.Add("LocationName");
				tbl.Columns.Add("CompanyName");
				tbl.Columns.Add("LSLevel1Code");
				tbl.Columns.Add("Level1Name");
				tbl.Columns.Add("Level2Name");
				tbl.Columns.Add("Level3Name");
				tbl.Columns.Add("PositionName");
				for (int i=0; i<tblData.Rows.Count; i++)
				{
					DataRow dr = tbl.NewRow();
					dr["EmpID"] = tblData.Rows[i]["EmpID"].ToString().Trim();
					dr["EmpName"] = tblData.Rows[i]["EmpName"].ToString().Trim();
					dr["StartDateStr"] = tblData.Rows[i]["StartDateStr"].ToString().Trim();
					dr["EmpCode"] = tblData.Rows[i]["EmpCode"].ToString().Trim();
					dr["VFirstName"] = tblData.Rows[i]["VFirstName"].ToString().Trim();
					dr["VLastName"] = tblData.Rows[i]["VLastName"].ToString().Trim();
					dr["LocationName"] = tblData.Rows[i]["LocationName"].ToString().Trim();
					dr["CompanyName"] = tblData.Rows[i]["CompanyName"].ToString().Trim();
					dr["LSLevel1Code"] = tblData.Rows[i]["LSLevel1Code"].ToString().Trim();
					dr["Level1Name"] = tblData.Rows[i]["Level1Name"].ToString().Trim();
					dr["Level2Name"] = tblData.Rows[i]["Level2Name"].ToString().Trim();
					dr["Level3Name"] = tblData.Rows[i]["Level3Name"].ToString().Trim();
					dr["PositionName"] = tblData.Rows[i]["PositionName"].ToString().Trim();
					tbl.Rows.Add(dr);
				}
				Session[strSession] = tbl;
			}
			catch(Exception exp)
			{
				this.lblErr.Text = exp.Message.ToString();
			}
			tbl.Dispose();
		}
		#endregion

		#region Ket du lieu ban dau va search khi chon 1
		private void BindDataGrid()
		{
			try
			{
				//Lay gia tri cua HeaderEmpSearch
				strEmpID = EmpHeaderSearch1.txtEmpID.Text.Trim().Replace("'","");
				strEmpName = EmpHeaderSearch1.txtEmpName.Text.Trim().Replace("'","");
				strLevel1 = EmpHeaderSearch1.cboLevel1.SelectedValue.Trim();
				strLevel2 = EmpHeaderSearch1.cboLevel2.SelectedValue.Trim();
				strLevel3 = EmpHeaderSearch1.cboLevel3.SelectedValue.Trim();
				strLocation = EmpHeaderSearch1.cboLocation.SelectedValue.Trim();
				strJobCode = EmpHeaderSearch1.cboJobcode.SelectedValue.Trim();
				strPosition = EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue.Trim();
				strCompany = EmpHeaderSearch1.cboCompany.SelectedValue.Trim();
				strStatus = EmpHeaderSearch1.optStatus.SelectedValue.Trim();
				sEmpTypeID = EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue.Trim();
				int intTopRow =0;
				if ( txtPageLoad.Text == "1" & strEmpID =="" & strEmpName == "" & strLevel2 == "" & strLocation == "" & strJobCode == "" & strPosition =="" )
				{
					intTopRow = dtgGridSelect.PageSize;
					txtPageLoad.Text = "0";
				}

				DataTable dtb = new DataTable();
				//Cac form neu muon truyen menh de where khi goi: 
				if (txtWhereCondition.Value.Trim() != "")
					dtb = clsHREmpList.GetEmpList(strEmpID, strEmpName, strLevel1, strLevel2, strLevel3,strPosition, strJobCode, strLocation, strCompany, strStatus, this.txtWhereCondition.Value.Trim(),this.Page,sGetAll,sEmpTypeID);
				else
					if(Request.Params["pCourseID"] != null)
						dtb = clsHREmpList.GetEmpListByCourse(Request.Params["pCourseID"].ToString().Replace("'","''").Trim(),strEmpID,strEmpName,strLevel1, strLevel2, strLevel3,strPosition,strJobCode, strLocation,strCompany,strStatus, this.txtWhereCondition.Value.Trim(),this.Page);
					else
						dtb = clsHREmpList.GetEmpList(strEmpID, strEmpName, strLevel1, strLevel2, strLevel3,strPosition, strJobCode, strLocation, strCompany, strStatus,intTopRow,this.Page,sGetAll,sEmpTypeID);

			//strEmpID, strEmpName, strLevel1, strLevel2, strLevel3,strPosition, strJobCode, strLocation, strCompany, strStatus,intTopRow,this.Page);
				//Neu nguoi dung nhap ma nhan vien chinh xac va co du lieu tuong ung
				//if (Request.Params["Return"] != null && strEmpID != "" && dtb.Rows.Count == 1)
				if (Request.Params["Return"] != null && dtb.Rows.Count == 1)
				{
					Session["EmpID"] = dtb.Rows[0][0].ToString(); //strEmpID;
					this.ltlAlert.Text = "ReloadOpener('" + dtb.Rows[0]["EmpID"].ToString().Trim() //EmpID
						+ "','" + dtb.Rows[0]["EmpName"].ToString().Trim() //EmpName
						+ "','" + dtb.Rows[0]["StartDateStr"].ToString().Trim() //StartDate
						+ "','" + dtb.Rows[0]["EmpCode"].ToString().Trim() //EmpCode
						+ "','" + dtb.Rows[0]["VLastName"].ToString().Trim() //LastName
						+ "','" + dtb.Rows[0]["VFirstName"].ToString().Trim() //FirstName
						+ "','" + dtb.Rows[0]["LocationName"].ToString().Trim() //Location
						+ "','" + dtb.Rows[0]["CompanyName"].ToString().Trim() //Company
						+ "','" + dtb.Rows[0]["Level1Name"].ToString().Trim() //Level1
						+ "','" + dtb.Rows[0]["Level2Name"].ToString().Trim() //Level2
						+ "','" + dtb.Rows[0]["Level3Name"].ToString().Trim() //Level3
						+ "','" + dtb.Rows[0]["PositionName"].ToString().Trim() + "')"; //Position
					dtb.Dispose();
				}
				else 
				{	
					//dtgGridSelect.DataSource = dtb;
					//dtgGridSelect.DataBind();
					
					//Hau
					dtgGridSelect.PageSize = uctrlColumns.iPageRows;
					if(this.uctrlColumns.bAdvMultiSort)
					{
						try
						{
							DataGridSort.AdvancedMultiSort(dtgGridSelect, dtb, uctrlColumns);
						}
						catch
						{
							this.dtgGridSelect.CurrentPageIndex = 0;
							DataGridSort.AdvancedMultiSort(dtgGridSelect, dtb, uctrlColumns);
						}
					}
					else
					{
						try
						{
							DataGridSort.Refresh(dtgGridSelect, dtb);
						}
						catch
						{
							this.dtgGridSelect.CurrentPageIndex = 0;
							DataGridSort.Refresh(dtgGridSelect, dtb);
						}
					}
					this.uctrlColumns.iTotalRows = dtb.Rows.Count;//
					dtb.Dispose();
				}
			}
			catch(Exception exp)
			{
				this.lblErr.Text = exp.Message.ToString();
			}
		}
		#endregion

		#region Ket du lieu ban dau va search khi chon nhieu
		private void BindDataGridMulti()
		{
			try
			{
				#region GetStr
				//Lay gia tri cua HeaderEmpSearch
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
				sEmpTypeID = EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue.Trim();
				#endregion

				int intTopRow =0;
				if ( txtPageLoad.Text == "1" & strEmpID =="" & strEmpName == "" & strLevel2 == "" & strLocation == "" & strJobCode == "" & strPosition =="" )
				{
					intTopRow = dtgGridSelect.PageSize;
					txtPageLoad.Text = "0";
				}

				DataTable dtb = new DataTable();
				//Cac form neu muon truyen menh de where khi goi:
				if (this.txtWhereCondition.Value.Trim() != "")
					dtb = clsHREmpList.GetEmpList(strEmpID, strEmpName, strLevel1, strLevel2, strLevel3,strPosition, strJobCode, strLocation, strCompany, strStatus, txtWhereCondition.Value.Trim(),this.Page,sGetAll,sEmpTypeID);
				else
					dtb = clsHREmpList.GetEmpList(strEmpID, strEmpName, strLevel1, strLevel2, strLevel3,strPosition, strJobCode, strLocation, strCompany, strStatus,intTopRow,this.Page,sGetAll,sEmpTypeID);
				
				this.SetUpStructureTable("tblSelect",dtb);
				if (Session["tblSelect"] != null)
					dtb = (DataTable)Session["tblSelect"];
				dtgGridSelect.DataSource = dtb;
				this.dtgGridSelect.CurrentPageIndex = 0;
				dtgGridSelect.DataBind();

				//Hau
				this.uctrlColumns.iTotalRows = dtb.Rows.Count;//

				dtb.Dispose();
				/*for (int i=0; i<this.dtgGridSelect.Items.Count; i++)
				{
					((LinkButton)this.dtgGridSelect.Items[i].FindControl("hpLink")).Enabled = false;
				}*/
				this.BindDataGridSelected();
			}
			catch(Exception exp)
			{
				this.lblErr.Text = exp.Message.ToString();
			}
		}

		private void BindDataGridSelect()
		{
			DataTable dtb = new DataTable();
			try
			{
				if (Session["tblSelect"] != null)
					dtb = (DataTable)Session["tblSelect"];
				else
				{
					SetUpStructureTable("tblSelect",dtb);
					dtb = (DataTable)Session["tblSelect"];
				}
				dtgGridSelect.DataSource = dtb;
				this.dtgGridSelect.CurrentPageIndex = 0;

				//Hau
				this.uctrlColumns.iTotalRows = dtb.Rows.Count;//
				dtgGridSelect.DataBind();
			}
			catch(Exception exp)
			{
				this.lblErr.Text = exp.Message.ToString();
			}
			dtb.Dispose();
		}

		private void BindDataGridSelected()
		{
			DataTable dtb = new DataTable();
			try
			{
				if (Session["tblSelected"] == null && Request.Params["LastValue"] != null)
				{
					if (Request.Params["LastValue"].Trim() != "")
					{
						string strLastValue = Request.Params["LastValue"].Trim();
						string[] ArrayStr = strLastValue.Split(new char[]{','});
						strLastValue = "";
						for (int i = 0; i<ArrayStr.Length; i++)
						{
							if (ArrayStr[i].Trim() != "")
							{
								if (strLastValue.Trim() == "")
									strLastValue = "''" + ArrayStr[i] + "''";
								else
									strLastValue = strLastValue + "," + "''" + ArrayStr[i] + "''";
							}
						}
						dtb = clsHREmpList.GetEmpList("","","","","","","","","",strStatus," and EmpID in (" + strLastValue + ")",this.Page,sGetAll,sEmpTypeID);
						this.SetUpStructureTable("tblSelected",dtb);
						//Ket lai du lieu cho dtgGridSelect khi dtgGridSelected lay gia tri mac dinh
						dtb = clsHREmpList.GetEmpList(strEmpID, strEmpName, strLevel1, strLevel2, strLevel3,strPosition, strJobCode, strLocation, strCompany, strStatus," and EmpID not in (" + strLastValue + ")",this.Page,sGetAll,sEmpTypeID);
						this.SetUpStructureTable("tblSelect",dtb);
						if (Session["tblSelect"] != null)
							dtb = (DataTable)Session["tblSelect"];
						dtgGridSelect.DataSource = dtb;
						this.dtgGridSelect.CurrentPageIndex = 0;
						dtgGridSelect.DataBind();

						//Hau
						this.uctrlColumns.iTotalRows = dtb.Rows.Count;//
//						this.lblTotalRows.Text = dtb.Rows.Count.ToString();
					}
				}
				if (Session["tblSelected"] != null)
					dtb = (DataTable)Session["tblSelected"];
				else
				{
					this.SetUpStructureTable("tblSelected",dtb);
					dtb = (DataTable)Session["tblSelected"];
				}
				dtgGridSelected.DataSource = dtb;
				dtgGridSelected.DataBind();
				for(int i=0; i<dtgGridSelected.Items.Count; i++)
				{
					((CheckBox)this.dtgGridSelected.Items[i].FindControl("chkSelect1")).Checked = true;
				}
			}
			catch(Exception exp)
			{
				this.lblErr.Text = exp.Message.ToString();
			}
			dtb.Dispose();
		}
		#endregion

		#region btnSearch_Click, dtgGridSelect_ItemCommand, dtgGridSelect_PageIndexChanged
		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			if (Request.Params["MultiSelect"] == null)
			{
				BindDataGrid();
				this.dtgGridSelect.Columns[13].Visible = true;
				this.dtgGridSelect.Columns[14].Visible = false;
			}
			else
			{
				BindDataGridMulti();
				this.dtgGridSelect.Columns[13].Visible = false;
				this.dtgGridSelect.Columns[14].Visible = true;
			}
		}

		private void dtgGridSelect_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			if (e.CommandName == "hpLink")
			{
				if (Request.Params["Params"] != null)
				{
					this.ltlAlert.Text = "ReturnEmpPopUp('" + Request.Params["Params"].Trim()
						+ "','" + e.Item.Cells[0].Text.Trim() //EmpID
						+ "','" + e.Item.Cells[2].Text.Trim() //EmpName
						+ "','" + e.Item.Cells[3].Text.Trim() //StartDate
						+ "','" + e.Item.Cells[1].Text.Trim() //EmpCode
						+ "','" + e.Item.Cells[4].Text.Trim() //LastName
						+ "','" + e.Item.Cells[5].Text.Trim() //FirstName
						+ "','" + e.Item.Cells[8].Text.Trim() + "')"; //Level1Name
				}
				else
				{
					 Session["EmpID"] = e.Item.Cells[0].Text.Trim();
					this.ltlAlert.Text = "ReloadOpener('" + e.Item.Cells[0].Text.Trim() //EmpID
						+ "','" + e.Item.Cells[2].Text.Trim() //EmpName
						+ "','" + e.Item.Cells[3].Text.Trim() //StartDate
						+ "','" + e.Item.Cells[1].Text.Trim() //EmpCode
						+ "','" + e.Item.Cells[4].Text.Trim() //LastName
						+ "','" + e.Item.Cells[5].Text.Trim() //FirstName
						+ "','" + e.Item.Cells[6].Text.Trim() //Location
						+ "','" + e.Item.Cells[7].Text.Trim() //Company
						+ "','" + e.Item.Cells[8].Text.Trim() //Level1
						+ "','" + e.Item.Cells[9].Text.Trim() //Level2
						+ "','" + e.Item.Cells[10].Text.Trim() //Level3
						+ "','" + e.Item.Cells[11].Text.Trim() + "')"; //Position
				}
			}
		}

		private void dtgGridSelect_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			//this.BindDataGrid();
			//this.dtgGridSelect.CurrentPageIndex = e.NewPageIndex;
			//this.dtgGridSelect.DataBind();

			//Hau
			try
			{
				DataGridSort.Grid_IndexChanged(dtgGridSelect, Read_Data(), e);
			}
			catch{}//
		}
		#endregion

		private void MoveData(DataGrid gridSelect, DataGrid gridSelected, string strCtlSelectName, int intIsAll)
		{
			DataTable dtbSelected = new DataTable();
			DataTable dtbSelect = new DataTable();
			try
			{
				if (Session["tblSelected"] != null)
					dtbSelected = (DataTable)Session["tblSelected"];
				else
				{
					this.SetUpStructureTable("tblSelected",dtbSelected);
					dtbSelected = (DataTable)Session["tblSelected"];
				}
				if (Session["tblSelect"] != null)
					dtbSelect = (DataTable)Session["tblSelect"];
				else
				{
					this.SetUpStructureTable("tblSelect",dtbSelect);
					dtbSelect = (DataTable)Session["tblSelect"];
				}

				if (gridSelect.ID == "dtgGridSelected")
				{
					DataTable dtbTemp = new DataTable();
					dtbTemp = dtbSelect;
					dtbSelect = dtbSelected;
					dtbSelected = dtbTemp;
				}

				for (int i=0; i<gridSelect.Items.Count; i++)
				{
					if (intIsAll == 0)
					{
						if (((CheckBox)gridSelect.Items[i].FindControl(strCtlSelectName)).Checked)
						{
							DataRow[] row = dtbSelected.Select("EmpID='" + gridSelect.Items[i].Cells[0].Text.Trim() + "'");
							if (row.Length == 0)
							{
								/*
								 tbl.Columns.Add("EmpID");
									tbl.Columns.Add("EmpName");
									tbl.Columns.Add("StartDateStr");
									tbl.Columns.Add("EmpCode");
									tbl.Columns.Add("VFirstName");
									tbl.Columns.Add("VLastName");
									tbl.Columns.Add("LocationName");
									tbl.Columns.Add("CompanyName");
									tbl.Columns.Add("LSLevel1Code");
									tbl.Columns.Add("Level1Name");
									tbl.Columns.Add("Level2Name");
									tbl.Columns.Add("Level3Name");
									tbl.Columns.Add("PositionName");
								 */
								DataRow mRowTemp = dtbSelected.NewRow();
								mRowTemp["EmpID"] = gridSelect.Items[i].Cells[0].Text.Trim();
								mRowTemp["EmpCode"] = gridSelect.Items[i].Cells[1].Text.Trim();
								mRowTemp["EmpName"] = gridSelect.Items[i].Cells[2].Text.Trim();
								mRowTemp["CompanyName"]=gridSelect.Items[i].Cells[7].Text.Trim();
								mRowTemp["Level1Name"]=gridSelect.Items[i].Cells[8].Text.Trim();
								mRowTemp["Level2Name"]=gridSelect.Items[i].Cells[9].Text.Trim();
								mRowTemp["Level3Name"]=gridSelect.Items[i].Cells[10].Text.Trim();
								//mRowTemp["JobRelated"] = gridSelect.Items[i].Cells[2].Text.Trim();
								mRowTemp["StartDateStr"] = gridSelect.Items[i].Cells[3].Text.Trim();
								
								mRowTemp["VFirstName"] = gridSelect.Items[i].Cells[5].Text.Trim();
								mRowTemp["VLastName"] = gridSelect.Items[i].Cells[6].Text.Trim();

								dtbSelected.Rows.Add(mRowTemp);
								DataRow[] row1 = dtbSelect.Select("EmpID='" + gridSelect.Items[i].Cells[0].Text.Trim() + "'");
								if (row1.Length > 0)
									dtbSelect.Rows.Remove(row1[0]);
							}
							else
							{
								DataRow[] row1 = dtbSelect.Select("EmpID='" + gridSelect.Items[i].Cells[0].Text.Trim() + "'");
								if (row1.Length > 0)
									dtbSelect.Rows.Remove(row1[0]);
							}
						}
					}
					else
					{
						DataRow[] row = dtbSelected.Select("EmpID='" + gridSelect.Items[i].Cells[0].Text.Trim() + "'");
						if (row.Length == 0)
						{
							DataRow mRowTemp = dtbSelected.NewRow();
							mRowTemp["EmpID"] = gridSelect.Items[i].Cells[0].Text.Trim();
							mRowTemp["EmpCode"] = gridSelect.Items[i].Cells[1].Text.Trim();
							mRowTemp["EmpName"] = gridSelect.Items[i].Cells[2].Text.Trim();
							mRowTemp["CompanyName"]=gridSelect.Items[i].Cells[7].Text.Trim();
							mRowTemp["Level1Name"]=gridSelect.Items[i].Cells[8].Text.Trim();
							mRowTemp["Level2Name"]=gridSelect.Items[i].Cells[9].Text.Trim();
							mRowTemp["Level3Name"]=gridSelect.Items[i].Cells[10].Text.Trim();
							//mRowTemp["JobRelated"] = gridSelect.Items[i].Cells[2].Text.Trim();
							mRowTemp["StartDateStr"] = gridSelect.Items[i].Cells[3].Text.Trim();
								
							mRowTemp["VFirstName"] = gridSelect.Items[i].Cells[5].Text.Trim();
							mRowTemp["VLastName"] = gridSelect.Items[i].Cells[6].Text.Trim();

							dtbSelected.Rows.Add(mRowTemp);
							DataRow[] row1 = dtbSelect.Select("EmpID='" + gridSelect.Items[i].Cells[0].Text.Trim() + "'");
							if (row1.Length > 0)
								dtbSelect.Rows.Remove(row1[0]);
						}
					}
				}
				if (gridSelect.ID == "dtgGridSelected")
				{
					Session["tblSelected"] = dtbSelect;
					Session["tblSelect"] = dtbSelected;
				}
				else
				{
					Session["tblSelected"] = dtbSelected;
					Session["tblSelect"] = dtbSelect;
				}
				this.BindDataGridSelected();
				this.BindDataGridSelect();
			}
			catch(Exception exp)
			{
				this.lblErr.Text = exp.Message.ToString();
			}
			dtbSelected.Dispose();
			dtbSelect.Dispose();
		}

		private void btnSelect_Click(object sender, System.EventArgs e)
		{
			this.lblErr.Text = "";
			try
			{
				this.MoveData(dtgGridSelect,dtgGridSelected, "chkSelect",0);
				/*for (int i=0; i<this.dtgGridSelect.Items.Count; i++)
				{
					((LinkButton)this.dtgGridSelect.Items[i].FindControl("hpLink")).Enabled = false;
				}*/
			}
			catch(Exception exp)
			{
				this.lblErr.Text = exp.Message.ToString();
			}
		}

		private void btnSelectAll_Click(object sender, System.EventArgs e)
		{
			this.lblErr.Text = "";
			try
			{
				this.MoveData(dtgGridSelect,dtgGridSelected, "chkSelect",1);
			}
			catch(Exception exp)
			{
				this.lblErr.Text = exp.Message.ToString();
			}
		}

		private void btnRemove_Click(object sender, System.EventArgs e)
		{
			this.lblErr.Text = "";
			try
			{
				this.MoveData(dtgGridSelected,dtgGridSelect, "chkSelect1",0);
			}
			catch(Exception exp)
			{
				this.lblErr.Text = exp.Message.ToString();
			}
		}

		private void btnRemoveAll_Click(object sender, System.EventArgs e)
		{
			this.lblErr.Text = "";
			try
			{
				this.MoveData(dtgGridSelected,dtgGridSelect, "chkSelect1",1);
			}
			catch(Exception exp)
			{
				this.lblErr.Text = exp.Message.ToString();
			}
		}

		private void btnChoose_Click(object sender, System.EventArgs e)
		{
			string strEmpList = "";
			string strEmpListID="";
			for(int i=0; i<this.dtgGridSelected.Items.Count; i++)
			{
				if(((CheckBox)this.dtgGridSelected.Items[i].FindControl("chkSelect1")).Checked)
				{
					if (strEmpList == "")
					{
						strEmpList = this.dtgGridSelected.Items[i].Cells[1].Text.Trim();
						strEmpListID = this.dtgGridSelected.Items[i].Cells[0].Text.Trim();
					}
					else
					{
						strEmpList = strEmpList + "," + this.dtgGridSelected.Items[i].Cells[1].Text.Trim();
						strEmpListID = strEmpListID + "," + this.dtgGridSelected.Items[i].Cells[0].Text.Trim();
					}
				}
			}
			this.ltlAlert.Text = "ReturnDataToOpener('" + strEmpList + "','" + strEmpListID + "')";
		}

		private void butExport_Click(object sender, System.EventArgs e)
		{
			iHRPCore.DataGridExcelExporter myExcelXport=new DataGridExcelExporter(dtgGridSelect);
			myExcelXport.Export("");
			myExcelXport = null;
		}

		#region Cac su kien xu li thao tac Sort (Hau)

		private void dtgGridSelect_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			try
			{
				DataGridSort.Grid_Sort(dtgGridSelect, Read_Data(), e, uctrlColumns);
			}
			catch{}
		}

		private DataTable Read_Data()
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
			sEmpTypeID = EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue.Trim();

			int intTopRow =0;
			if ( txtPageLoad.Text == "1" & strEmpID =="" & strEmpName == "" & strLevel2 == "" & strLocation == "" & strJobCode == "" & strPosition =="" )
			{
				intTopRow = dtgGridSelect.PageSize;
				txtPageLoad.Text = "0";
			}

			DataTable dtb = new DataTable();
			//Cac form neu muon truyen menh de where khi goi
			if (this.txtWhereCondition.Value.Trim() != "")
				dtb = clsHREmpList.GetEmpList(strEmpID, strEmpName, strLevel1, strLevel2, strLevel3,strPosition, strJobCode, strLocation, strCompany, strStatus, txtWhereCondition.Value.Trim(),this.Page,sGetAll,sEmpTypeID);
			else
				dtb = clsHREmpList.GetEmpList(strEmpID, strEmpName, strLevel1, strLevel2, strLevel3,strPosition, strJobCode, strLocation, strCompany, strStatus,intTopRow,this.Page,sGetAll,sEmpTypeID);
			
			return dtb;
		}

		private void ButtonClick()
		{
			this.uctrlColumns.ButtonPRChanged.ServerClick += new EventHandler(ButtonPRChanged_ServerClick);
			this.uctrlColumns.ButtonSort.ServerClick += new EventHandler(ButtonSort_ServerClick);
		}

		private void ButtonPRChanged_ServerClick(object sender, EventArgs e)
		{
			try
			{
				dtgGridSelect.PageSize = uctrlColumns.iPageRows;
				dtgGridSelect.CurrentPageIndex = 0;
				DataView dv = new DataView(Read_Data());
				dv.Sort = DataGridSort.sMulSort;
				dtgGridSelect.DataSource = dv;
				dtgGridSelect.DataBind();
			}
			catch{}
		}

		private void ButtonSort_ServerClick(object sender, EventArgs e)
		{
			try
			{
				DataGridSort.AdvancedMultiSort(dtgGridSelect, Read_Data(), uctrlColumns);
			}
			catch{}
		}

		#endregion
	}
}
