namespace Web_DM
{
	using System;
	using System.Data.SqlClient;
	using System.Collections;
	using System.ComponentModel;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.SessionState;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Web_DM.Component;
	using iHRPCore.Com;	
	using iHRPCore;
	using GridSort;
	/// <summary>
	///		Summary description for Catalog1.
	/// </summary>
	public class Catalog1 : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.WebControls.LinkButton btnUpdate;
		protected System.Web.UI.WebControls.TextBox txtSoDong;
		protected System.Web.UI.WebControls.DataGrid grdList;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.LinkButton btnAddNew;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnNumberColumn;
		protected System.Web.UI.HtmlControls.HtmlTable tblControl;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.Label Label1;
		string strTableName = "";
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label lblTotalRows;
		protected System.Web.UI.WebControls.CheckBox chkMultiSort;
		static public String[][] Header_SortExp;
		protected System.Web.UI.WebControls.LinkButton btnSearch;
		protected System.Web.UI.WebControls.CheckBox chkSelectAll;
		protected string strLanguage = "EN";

		private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
				lblErr.Text = "";
				//cangtt- fix so dong la 15 - 28022007
				txtSoDong.Text="15";
				/////end edit
				// Put user code to initialize the page here
				if(!Page.IsPostBack)
				{
					Session["ssclsAction"] = "Save";
				}
				btnDelete.Attributes["OnClick"]="return CheckDelete();";
				btnUpdate.Attributes.Add("OnClick", " return validform()");
				string str_Script="";
				if(Request.Params["tabid"] != null)
				{
					strTableName = Request.Params["tabid"].ToString().Trim();
					str_Script = clsCatalog.CreateControlsTemp(strTableName,tblControl,Request.MapPath("CONTROLS.XML").ToString(),grdList,null,null,null,new Label(),txtSoDong, this);
					
					//Hau
					DataTable dtb = new DataTable();
					//DataTable dtb = GetDataSearch(false);//DM.GetDanhMucListGrid(strTableName);
					if(Session["ssclsAction"].Equals("Search"))
					{
						dtb = GetDataSearch(true);
					}
					else
						dtb = GetDataSearch(false);//dtb = (DataTable)grdList.DataSource;

//					grdList.Columns.Clear();
//					grdList.Dispose();

					clsCatalog.BindDataGrid(Request.MapPath("CONTROLS.XML").ToString(),strTableName,dtb, grdList,true, true, "Select", "Edit",Request.ApplicationPath+ "/Images/edit.gif",null);
					if(!IsPostBack)
					{
						lblTotalRows.Text = dtb.Rows.Count.ToString();
						Header_SortExp = null;
						Header_SortExp = new String[grdList.Columns.Count][];
						int i;
						for(i=0; i<grdList.Columns.Count; i++)
						{
							Header_SortExp[i] = new String[2];
							Header_SortExp[i][0] = grdList.Columns[i].HeaderText;
							Header_SortExp[i][1] = grdList.Columns[i].SortExpression;
						}
						ReloadDropDownList();
						btnSearch_Click(null,null);
					}
					/*try
					{
						grdList.PageSize = Convert.ToInt32(txtSoDong.Text);
						grdList.DataBind();
					}
					catch
					{
						grdList.PageSize = 50;
						grdList.DataBind();
					}*/
					if(str_Script.Trim()!="") Response.Write(str_Script);
				}
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;				
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
			this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
			this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			this.grdList.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.grdList_ItemCommand);
			this.grdList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.grdList_PageIndexChanged);
			this.grdList.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.grdList_SortCommand);
			this.btnNumberColumn.ServerClick += new System.EventHandler(this.btnNumberColumn_ServerClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		private void grdList_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			//--- edit action
			if(e.CommandName.Trim().Equals("EDIT"))
			{	
				try
				{
					if (txtSoDong.Text != "") grdList.PageSize= Int32.Parse(txtSoDong.Text);
					//Hau
					strTableName = Request.Params["tabid"].ToString().Trim();
					DataTable dtb = new DataTable();
					if(!Session["ssclsAction"].Equals("Search"))
					{
						//clsCatalog.BindDataGrid(Request.MapPath("CONTROLS.XML").ToString(),strTableName,DM.GetDanhMucListGrid(strTableName), grdList,true, true, "Delete", "Edit",Request.ApplicationPath+ "/Images/edit.gif",null);
						//dtb = (DataTable)grdList.DataSource;
						dtb = GetDataSearch(false);
					}
					else
						dtb = GetDataSearch(true);
					lblTotalRows.Text = dtb.Rows.Count.ToString();
					try
					{
						for(int i=0; i<grdList.Columns.Count; i++)
						{
							grdList.Columns[i].HeaderText = Header_SortExp[i][0];
							grdList.Columns[i].SortExpression = Header_SortExp[i][1];
						}
						DataView dv = new DataView(dtb);
						dv.Sort = DataGridSort.sMulSort;
						grdList.DataSource = dv;
						grdList.DataBind();
					}
					catch{}
					////
			
					Session["ssclsAction"] = "Update";
					DataTable dtList = new DataTable();
					dtList = DM.GetControlsofCatalog(strTableName,Request.MapPath("CONTROLS.XML").ToString().Trim());
					string strKeyField = DM.GetKeyField(dtList);
					DataRow iRow = clsCatalog.GetDataByID(strTableName,strKeyField,grdList.DataKeys[e.Item.ItemIndex].ToString().Trim());
					//----not Allow edit keyfield
			
					((TextBox)this.FindControl("txt"+strKeyField.Trim())).Enabled = false;
					for(int i=0;i<dtList.Rows.Count;i++)
					{
						string strCtrlName = DM.ReturnControlName(dtList.Rows[i]["ID"].ToString(),dtList.Rows[i]["ControlType"].ToString());
						this.SetValueToControl(strCtrlName,iRow[dtList.Rows[i]["ID"].ToString().Trim()].ToString());
					}
					
					dtList.Dispose();
					//clsCatalog.ShowMessageBox(Page,grdList.Items[e.Item.ItemIndex].Cells[1].Text.Trim());			
				}
				catch(Exception ee)
				{
					string strErr = ee.Message;
					
				}
			}
		}		
		private void ResetControl(Control pRootCtl)
		{
			try
			{
				foreach(Control Child_ctl in pRootCtl.Controls)
				{
					if(Child_ctl.HasControls()==true)
					{
						ResetControl(Child_ctl);
					}
					else
					{						
						string mCtlType = Child_ctl.GetType().ToString().Trim().Substring(Child_ctl.GetType().ToString().Trim().LastIndexOf(".")+1).ToString().Trim();
						switch(mCtlType.ToUpper())
						{								
							case "TEXTBOX":
								if (((TextBox)Child_ctl).ID !="txtSoDong")
								{
									((TextBox)Child_ctl).Text = "";
									((TextBox)Child_ctl).Enabled = true;
								}
								break;
							case "DROPDOWNLIST":
								if (((DropDownList)Child_ctl).AutoPostBack == false)  
									((DropDownList)Child_ctl).SelectedIndex = -1;
								break;
							case "RADIOBUTTONLIST":
								((RadioButtonList)Child_ctl).SelectedIndex = 0;
								break;
							case "CHECKBOX":
								if(Child_ctl.ID.Trim().ToUpper() != "CHKSELECT")
								{
									if ( ((CheckBox)Child_ctl).ToolTip == "0")
										((CheckBox)Child_ctl).Checked = false;
									else
										((CheckBox)Child_ctl).Checked = true;

								}
								break;
							case "RADIOBUTTON":
								((RadioButton)Child_ctl).Checked = true;
								break;
							default :
								break;
						}
					}
				}
			}
			catch(Exception exp)
			{
				//lblErr.Text = exp.Message.ToString();
				clsChangeLang.popupWindowExp(this.Parent,exp);
			}
		}
		
		private void btnUpdate_Click(object sender, System.EventArgs e)
		{			
			try
			{
				if (txtSoDong.Text != "") grdList.PageSize= Int32.Parse(txtSoDong.Text);
				for(int i=0; i<grdList.Columns.Count; i++ )
				{
					Header_SortExp[i][0] = grdList.Columns[i].HeaderText;
					Header_SortExp[i][1] = grdList.Columns[i].SortExpression;
					DataGridSort.sMulSort = "";
				}
				if(Session["ssclsAction"]==null || !Session["ssclsAction"].Equals("Update") ) Session["ssclsAction"] = "Save";			
				DataTable dtList = new DataTable();
				dtList = DM.GetControlsofCatalog(strTableName,Request.MapPath("CONTROLS.XML").ToString().Trim());
				string[] arrParams = new string[dtList.Rows.Count];
				string[] arrDataType = new string[dtList.Rows.Count];
				string[] arrValues = new string[dtList.Rows.Count];			
				for(int i=0;i<dtList.Rows.Count;i++)
				{
					arrParams.SetValue("@"+dtList.Rows[i]["ID"].ToString().Trim(),i);
					arrDataType.SetValue(dtList.Rows[i]["DataType"].ToString().Trim(),i);
					string strCtrlName = DM.ReturnControlName(dtList.Rows[i]["ID"].ToString(),dtList.Rows[i]["ControlType"].ToString());
					arrValues.SetValue(GetValueControl(strCtrlName),i);
				}	
				string strReturn =clsCatalog.ExecuteCommand(strTableName,Session["ssclsAction"].ToString(),arrParams,arrDataType,arrValues,this.Page,strLanguage);
				if(strReturn =="")
				{
					if (Session["LangID"].ToString() == "EN")
					{
						if(Session["ssclsAction"].Equals("Update"))
							//clsCatalog.ShowMessageBox(Page,"Save Update Completed!");
							clsChangeLang.popupWindow(this.Parent,"Save/Update Completed!","",1);
						else
							//clsCatalog.ShowMessageBox(Page,"Save Add New Completed!");
							clsChangeLang.popupWindow(this.Parent,"Save/Update Completed!","",1);
					}
					else
					{
						if(Session["ssclsAction"].Equals("Update"))
							//clsCatalog.ShowMessageBox(Page,"Cập nhật thành công!");
							clsChangeLang.popupWindow(this.Parent,"Cập nhật thành công!","",1);
						else
							//clsCatalog.ShowMessageBox(Page,"Thêm mới thành công!");
							clsChangeLang.popupWindow(this.Parent,"Thêm mới thành công!","",1);
					}
					//cangtt từ dưới chuyển lên
					Session["ssclsAction"] = "Save";
					//Session["ssclsAction"] = "Update";
					dtList.Dispose();

					//clsCatalog.BindDataGrid(Request.MapPath("CONTROLS.XML").ToString(),strTableName,DM.GetDanhMucListGrid(strTableName), grdList,true, true, "Delete", "Edit",Request.ApplicationPath+ "/Images/edit.gif",null);
					DataView dv = new DataView(DM.GetDanhMucListGrid(strTableName));
					grdList.DataSource = dv;
					grdList.DataBind();

					this.ResetControl(this);
					//Response.Redirect(Request.ApplicationPath + "/Catalog.aspx?tabid=" + strTableName);

					GetDataGrid();
					if (strReturn != "" ) 
						//lblErr.Text=strReturn;
						clsChangeLang.popupWindow(this.Parent,"0034","VN",strReturn.Substring(0,strReturn.LastIndexOf(".")),0);
					else
						lblErr.Text="";
				}
				else
				{
					//clsCatalog.ShowMessageBox(Page,strReturn);
					//clsChangeLang.popupWindow(this.Parent,strReturn,"",0);
					clsChangeLang.popupWindowCataLog(this.Parent,strReturn);
				}
				
			}
			catch(Exception ex)
			{
				clsChangeLang.popupWindowExp(this.Parent,ex);
			}
		}
		/// <summary>
		/// Get value of control
		/// </summary>
		/// <param name="strControlName">Control name</param>
		/// <returns>Value</returns>
		private string GetValueControl(string strControlName)
		{
			string strReturnValue="";
			try
			{
				Control ctrFound = this.FindControl(strControlName);
				if(ctrFound!=null)
				{
					string strType = ctrFound.GetType().ToString().Trim().Substring(ctrFound.GetType().ToString().Trim().LastIndexOf(".")+1).ToString().Trim();
					switch(strType)
					{
						case "TextBox":
							strReturnValue = ((TextBox)ctrFound).Text;
							break;
						case "DropDownList":
							strReturnValue = ((DropDownList)ctrFound).SelectedValue.Trim();
							break;
						case "RadioButtonList":
							strReturnValue = ((RadioButtonList)ctrFound).SelectedValue.Trim();
							break;
						case "CheckBox":
							strReturnValue = ((CheckBox)ctrFound).Checked.ToString();
							break;
						case "RadioButton":
							strReturnValue = ((RadioButton)ctrFound).Checked.ToString();
							break;												
						default :
							break;
					}
				}
			}				
			catch(Exception ex)
			{
				return "";
				//lblErr.Text = ex.Message;
				clsChangeLang.popupWindowExp(this.Parent,ex);
			}
			return strReturnValue;
		}
		/// <summary>
		/// Assign value to control
		/// </summary>
		/// <param name="strControlName">Control Name</param>
		/// <param name="strValue">Value</param>
		private void SetValueToControl(string strControlName,string strValue)
		{			
			try
			{
				Control ctrFound = this.FindControl(strControlName);
				if(ctrFound!=null)
				{
					string strType = ctrFound.GetType().ToString().Trim().Substring(ctrFound.GetType().ToString().Trim().LastIndexOf(".")+1).ToString().Trim();
					switch(strType)
					{
						case "TextBox":
							((TextBox)ctrFound).Text = Server.HtmlDecode(strValue.Trim());
							break;
						case "DropDownList":
							((DropDownList)ctrFound).SelectedValue = strValue.Trim();
							break;
						case "RadioButtonList":
							((RadioButtonList)ctrFound).SelectedValue = strValue.Trim();
							break;
						case "CheckBox":
							((CheckBox)ctrFound).Checked = strValue.Trim()=="True"?true:false;
							break;
						case "RadioButton":
							((RadioButton)ctrFound).Checked = strValue.Trim()=="True"?true:false;
							break;												
						default :
							break;
					}
				}
			}				
			catch(Exception ex)
			{				
				//lblErr.Text = ex.Message;
				clsChangeLang.popupWindowExp(this.Parent,ex);
			}			
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (txtSoDong.Text != "") grdList.PageSize= Int32.Parse(txtSoDong.Text);
				//Hau
				strTableName = Request.Params["tabid"].ToString().Trim();
				DataTable dtb = new DataTable();
				if(Session["ssclsAction"].Equals("Search"))
				{
					dtb = GetDataSearch(true);
				}
				else
					dtb = GetDataSearch(false);//dtb = (DataTable)grdList.DataSource;
				
				Boolean[] DelCheck;
				DelCheck = new Boolean[grdList.Items.Count];
				for(int i=0; i<grdList.Items.Count; i++)
					DelCheck[i] = ((CheckBox)this.grdList.Items[i].FindControl("chkSelect")).Checked;

				DataView dv = new DataView(dtb);
				dv.Sort = DataGridSort.sMulSort;
				grdList.DataSource = dv;
				grdList.DataBind();
				////

				string strReturn ="";
				DataTable dtList = new DataTable();
				dtList = DM.GetControlsofCatalog(strTableName,Request.MapPath("CONTROLS.XML").ToString().Trim());				
				string strKeyField = DM.GetKeyField(dtList);
				dtList.Dispose();			
				for(int i=0;i<grdList.Items.Count;i++)
				{
					//if(((CheckBox)this.grdList.Items[i].FindControl("chkSelect")).Checked)	//Hau
					if(DelCheck[i])		//Hau
					{
						string[] arrParams = new string[1];
						string[] arrDataType = new string[1];
						string[] arrValues = new string[1];
						arrParams.SetValue("@"+strKeyField,0);
						arrDataType.SetValue("Nvarchar",0);
						arrValues.SetValue(grdList.DataKeys[i].ToString().Trim(),0);

						strReturn = clsCatalog.ExecuteCommand(strTableName,"Delete",arrParams,arrDataType,arrValues,this.Page,strLanguage);						
						//if(clsCatalog.ExecuteCommand(strTableName,"Delete",arrParams,arrDataType,arrValues)!= "")
						if(strReturn.Trim() !="")
						{						
							break;
						}
					}
				}
				//clsCatalog.BindDataGrid(Request.MapPath("CONTROLS.XML").ToString(),strTableName,DM.GetDanhMucListGrid(strTableName), grdList,true, true, "Delete", "Edit",Request.ApplicationPath+ "/Images/edit.gif",null);
				
				//Hau
				if(Session["ssclsAction"].Equals("Search"))
				{
					dtb = GetDataSearch(true);
				}
				else
				{
					dtb = GetDataSearch(false);//dtb = (DataTable)grdList.DataSource;
				}
					
				for(int i=0; i<grdList.Columns.Count; i++)
				{
					grdList.Columns[i].HeaderText = Header_SortExp[i][0];
					grdList.Columns[i].SortExpression = Header_SortExp[i][1];
				}
				dv = new DataView(dtb);
				dv.Sort = DataGridSort.sMulSort;
				grdList.DataSource = dv;
				grdList.DataBind();
				this.lblTotalRows.Text = dv.Table.Rows.Count.ToString() ;
				////

				//Response.Redirect(Request.ApplicationPath + "/Catalog.aspx?tabid=" + strTableName);
				if (strReturn != "" )
					//lblErr.Text =strReturn;
					clsChangeLang.popupWindowCataLog(this.Parent,strReturn);
				else
					lblErr.Text ="";
			}
			catch(SqlException  ex)
			{
				//lblErr.Text = "Record is using! Can not delete!";
				//clsChangeLang.popupWindow(this.Parent,"Record is using! Can not delete!","",0);
				clsChangeLang.popupWindowExp(this.Parent,ex);
			}
			catch(Exception ex)
			{
				clsChangeLang.popupWindowExp(this.Parent,ex);
			}
		}

		/*private void btnCancel_Click(object sender, System.EventArgs e)
		{
			Session["ssclsAction"] = "Save";
			this.ResetControl(this);
		}*/
		private void grdList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			try
			{
				//grdList.CurrentPageIndex = e.NewPageIndex;
				strTableName = Request.Params["tabid"].ToString().Trim();
				if (txtSoDong.Text != "") grdList.PageSize= Int32.Parse(txtSoDong.Text);
				//Hau
				DataTable dtb = new DataTable();
				if(!Session["ssclsAction"].Equals("Search"))
				{	
					//				clsCatalog.BindDataGrid(Request.MapPath("CONTROLS.XML").ToString(),strTableName,DM.GetDanhMucListGrid(strTableName), grdList,true, true, "Delete", "Edit",Request.ApplicationPath+ "/Images/edit.gif",null);
					dtb = GetDataSearch(false);//dtb = (DataTable)grdList.DataSource;
				}
				else
					dtb = GetDataSearch(true);//dtb = GetDataSearch();
				for(int i=0; i<grdList.Columns.Count; i++)
				{
					grdList.Columns[i].HeaderText = Header_SortExp[i][0];
					grdList.Columns[i].SortExpression = Header_SortExp[i][1];
				}
				try
				{
					DataGridSort.Grid_IndexChanged(grdList, dtb, e);
				}
				catch(Exception ee)
				{
					string strErr =  ee.Message;
				}
				Session["ssclsAction"] ="Search";
			
				////
			}
			catch(Exception ee)
			{
				lblErr.Text = ee.Message;
			}
		}

		private void btnExport_Click(object sender, System.EventArgs e)
		{

			//btnSearch_Click(null,null);
			iHRPCore.DataGridExcelExporter myExcelXport=new DataGridExcelExporter(grdList);
			myExcelXport.Export("");
			myExcelXport = null;
		}

		private void btnAddNew_Click(object sender, System.EventArgs e)
		{
			try
			{

				//Hau
				for (int i=0; i<grdList.Columns.Count; i++) 
				{
					Header_SortExp[i][0] = grdList.Columns[i].HeaderText;
					Header_SortExp[i][1] = grdList.Columns[i].SortExpression;
					DataGridSort.sMulSort = "";
				}
				////
				lblErr.Text="";
				Session["ssclsAction"] = "Save";
				this.ResetControl(this);
				if (txtSoDong.Text != "") grdList.PageSize= Int32.Parse(txtSoDong.Text);
				GetDataGrid();
			}
			catch(Exception ee)
			{
				lblErr.Text = ee.Message;
			}
		}

		private void btnNumberColumn_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				//Hau
				try
				{
					grdList.PageSize = Convert.ToInt32(txtSoDong.Text.Trim());
				}
				catch
				{
					grdList.PageSize = 50;
				}
				grdList.CurrentPageIndex = 0;
				strTableName = Request.Params["tabid"].ToString().Trim();			

				DataTable dtb = new DataTable();
				if(!Session["ssclsAction"].Equals("Search"))
				{
					//clsCatalog.BindDataGrid(Request.MapPath("CONTROLS.XML").ToString(),strTableName,DM.GetDanhMucListGrid(strTableName), grdList,true, true, "Delete", "Edit",Request.ApplicationPath+ "/Images/edit.gif",null);
					dtb = GetDataSearch(false);//dtb = (DataTable)grdList.DataSource;
				}
				else
					dtb = GetDataSearch(true);//dtb = GetDataSearch();

				for(int i=0; i<grdList.Columns.Count; i++)
				{
					grdList.Columns[i].HeaderText = Header_SortExp[i][0];
					grdList.Columns[i].SortExpression = Header_SortExp[i][1];
				}
				DataView dv = new DataView(dtb);
				dv.Sort = DataGridSort.sMulSort;
				grdList.DataSource = dv;
				grdList.DataBind();
			}
			catch(Exception ee)
			{
				lblErr.Text = ee.Message;
			}//
		}

		#region Cac thao tac xu li su kien sort, search (Hau)

		private void grdList_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			if (txtSoDong.Text != "") grdList.PageSize= Int32.Parse(txtSoDong.Text);
			DataTable dtb = new DataTable();
			if(!Session["ssclsAction"].Equals("Search"))
			{
				//strTableName = Request.Params["tabid"].ToString().Trim();
				//clsCatalog.BindDataGrid(Request.MapPath("CONTROLS.XML").ToString(),strTableName,DM.GetDanhMucListGrid(strTableName), grdList,true, true, "Delete", "Edit",Request.ApplicationPath+ "/Images/edit.gif",null);
				dtb = GetDataSearch(false);//dtb = (DataTable)grdList.DataSource;
			}
			else
				dtb = GetDataSearch(true);//dtb = GetDataSearch();
			lblTotalRows.Text = dtb.Rows.Count.ToString();
			try
			{
				int i;
				String SortExp = e.SortExpression;
				for(i=0; i<grdList.Columns.Count; i++)
				{
					grdList.Columns[i].HeaderText = Header_SortExp[i][0];
					grdList.Columns[i].SortExpression = Header_SortExp[i][1];
					if(SortExp.Equals(RemoveEnd(RemoveEnd(Header_SortExp[i][1], " ASC"), " DESC")))
						SortExp = Header_SortExp[i][1];
				}
				DataGridSort.Grid_Sort(grdList, dtb, SortExp, chkMultiSort.Checked);
				for(i=0; i<grdList.Columns.Count; i++)
				{
					Header_SortExp[i][0] = grdList.Columns[i].HeaderText;
					Header_SortExp[i][1] = grdList.Columns[i].SortExpression;
				}
			}
			catch{}
		}

		private String RemoveEnd(String Src, String EndStr)
		{
			if(!Src.EndsWith(EndStr))
				return Src;
			String tmp = "";
			tmp = Src.Remove(Src.Length - EndStr.Length, EndStr.Length);
			return tmp;
		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{	
			try
			{
				for (int i=0; i<grdList.Columns.Count; i++) {
					Header_SortExp[i][0] = grdList.Columns[i].HeaderText;
					Header_SortExp[i][1] = grdList.Columns[i].SortExpression;
					DataGridSort.sMulSort = "";
				}
				DataTable dtb = new DataTable();
				
				//ReloadDropDownList();
				dtb = GetDataSearch(true);//dtb = GetDataSearch();
				//clsCatalog.BindDataGrid(Request.MapPath("CONTROLS.XML").ToString(),strTableName,dtb, grdList,true, true, "Delete", "Edit",Request.ApplicationPath+ "/Images/edit.gif",null);
				Session["ssclsAction"] = "Search";
				lblTotalRows.Text = dtb.Rows.Count.ToString();
				lblErr.Text = "";
				try
				{
					grdList.PageSize = Convert.ToInt32(txtSoDong.Text.Trim());
				}
				catch{
					grdList.PageSize = 50;
				}
				grdList.DataSource = dtb;
				try
				{
					grdList.DataBind();
				}
				catch
				{
					grdList.CurrentPageIndex = 0;
					grdList.DataBind();
				}
			}
			catch (Exception ex){
				lblErr.Text = ex.Message;
			}
		}

		private DataTable GetDataSearch(bool blnSearch)
		{
			DataTable dtList = new DataTable();
			dtList = DM.GetControlsofCatalog(strTableName,Request.MapPath("CONTROLS.XML").ToString().Trim());
			String CommandText = "ls_spfrm" + strTableName.Trim().Replace("tbl","");
			CommandText += " @Activity='GetDataAll', @Where=N'";
			for(int i=0;i<dtList.Rows.Count;i++)
			{
				string strCtrlName = DM.ReturnControlName(dtList.Rows[i]["ID"].ToString(),dtList.Rows[i]["ControlType"].ToString());
				string strDataType = dtList.Rows[i]["DataType"].ToString().Trim();
				string strValue = GetValueControl(strCtrlName);
				
				if( !blnSearch )
				{
					if(strValue != "" && dtList.Rows[i]["IsNull"].ToString().Trim() == "0" && dtList.Rows[i]["ControlType"].ToString().Trim().ToUpper().Equals("DROPDOWNLIST"))
						CommandText += (" AND "+dtList.Rows[i]["ID"].ToString().Trim()+" = N''"+strValue+"''");
					else if(strValue != "" && dtList.Rows[i]["IsNull"].ToString().Trim() == "0" && dtList.Rows[i]["ControlType"].ToString().Trim().ToUpper().Equals("BIT"))
					{
						if (strValue.ToUpper().Equals("TRUE") || strValue.ToUpper().Equals("1"))
							CommandText += (" AND " + dtList.Rows[i]["ID"].ToString().Trim() + " = 1 ");
						else
							CommandText += (" AND " + dtList.Rows[i]["ID"].ToString().Trim() + " = 0 ");
					}
				}
				else if(strValue != "" && dtList.Rows[i]["IsNull"].ToString().Trim() == "0")
				{
					if(strDataType.ToUpper().Equals("DATETIME"))
					{
						CommandText += (" AND " + dtList.Rows[i]["ID"].ToString().Trim() + " >= convert(datetime, ''" + strValue + "'', 103)");
					}
					else if(strDataType.ToUpper().Equals("BIT"))
					{
						if (strValue.ToUpper().Equals("TRUE") || strValue.ToUpper().Equals("1"))
							CommandText += (" AND " + dtList.Rows[i]["ID"].ToString().Trim() + " = 1 ");
						else
							CommandText += (" AND " + dtList.Rows[i]["ID"].ToString().Trim() + " = 0 ");
					}
					else
					{ 
						if (dtList.Rows[i]["ControlType"].ToString().Trim().ToUpper().Equals("DROPDOWNLIST"))
							CommandText += (" AND "+dtList.Rows[i]["ID"].ToString().Trim()+" = N''"+strValue+"''");
						else
							CommandText += (" AND "+dtList.Rows[i]["ID"].ToString().Trim()+" LIKE N''%"+strValue+"%''");
					}
				}
			}
			CommandText += "'";
			DataTable dtb = new DataTable();
			dtb = clsCatalog.GetDataTable(CommandText);
			
			dtList.Dispose();
			return dtb;
		}

		private void GetDataGrid()
		{
			DataTable dtList = new DataTable();
			dtList = DM.GetControlsofCatalog(strTableName,Request.MapPath("CONTROLS.XML").ToString().Trim());
			String CommandText = "ls_spfrm" + strTableName.Trim().Replace("tbl","");
			CommandText += " @Activity='GetDataAll', @Where=N'";
			for(int i=0;i<dtList.Rows.Count;i++)
			{
				string strCtrlName = DM.ReturnControlName(dtList.Rows[i]["ID"].ToString(),dtList.Rows[i]["ControlType"].ToString());
				string strDataType = dtList.Rows[i]["DataType"].ToString().Trim();
				string strValue = GetValueControl(strCtrlName);

				if(strValue != "" && dtList.Rows[i]["IsNull"].ToString().Trim() == "0" && dtList.Rows[i]["ControlType"].ToString().Trim().ToUpper().Equals("DROPDOWNLIST"))
					CommandText += (" AND "+dtList.Rows[i]["ID"].ToString().Trim()+" = N''"+strValue+"''");
				else if(strValue != "" && dtList.Rows[i]["IsNull"].ToString().Trim() == "0" && dtList.Rows[i]["ControlType"].ToString().Trim().ToUpper().Equals("BIT"))
				{
					if (strValue.ToUpper().Equals("TRUE") || strValue.ToUpper().Equals("1"))
						CommandText += (" AND " + dtList.Rows[i]["ID"].ToString().Trim() + " = 1 ");
					else
						CommandText += (" AND " + dtList.Rows[i]["ID"].ToString().Trim() + " = 0 ");
				}
			}
			CommandText += "'";
			DataTable dtb = new DataTable();
			dtb = clsCatalog.GetDataTable(CommandText);
			
			try
			{
				for(int i=0; i<grdList.Columns.Count; i++)
				{
					grdList.Columns[i].HeaderText = Header_SortExp[i][0];
					grdList.Columns[i].SortExpression = Header_SortExp[i][1];
				}
				DataView dv = new DataView(dtb);
				dv.Sort = DataGridSort.sMulSort;
				grdList.DataSource = dv;
				grdList.DataBind();
			}
			catch{}//

			lblTotalRows.Text = dtb.Rows.Count.ToString();
			lblErr.Text = "";
			try
			{
				grdList.PageSize = Convert.ToInt32(txtSoDong.Text.Trim());
			}
			catch
			{
				grdList.PageSize = 50;
			}
			grdList.DataSource = dtb;
			try
			{
				grdList.DataBind();
			}
			catch
			{
				grdList.CurrentPageIndex = 0;
				grdList.DataBind();
			}
			//return dtb;
			dtList.Dispose();
		}


		private void ReloadDropDownList()
		{
			try
			{
				DataTable dtList = new DataTable();
				dtList = DM.GetControlsofCatalog(strTableName,Request.MapPath("CONTROLS.XML").ToString().Trim());
				string strDataSource ;
				int intParamIndex ;
				string strParam;
				string strParamValue;
				string strControlName;

				for(int i=0;i<dtList.Rows.Count;i++)
				{

					if(dtList.Rows[i]["ControlType"].ToString().Trim() == "DropDownList")
					{
						strDataSource = dtList.Rows[i]["DataSource"].ToString().Trim();
						intParamIndex = strDataSource.IndexOf("@@");
						if(intParamIndex >= 0)
						{
							strParam = strDataSource.Substring(intParamIndex);
							strParamValue = " N'" + GetValueControl("cbo" + strParam.Substring(2)) + "'";

							strDataSource = strDataSource.Replace(strParam,strParamValue) + ", ";
							strControlName = DM.ReturnControlName(dtList.Rows[i]["ID"].ToString(),dtList.Rows[i]["ControlType"].ToString());

							BindControl.BindDropDownList((DropDownList)this.FindControl(strControlName),strDataSource,((DropDownList)this.FindControl(strControlName)).SelectedValue,dtList.Rows[i]["Isnull"].ToString().Trim()=="1"?true:false,-1);
							
						}

					}

				}
			
				dtList.Dispose();
			}
			catch(Exception e)
			{
				string strErr = e.Message;
			}
		}

		#endregion

		public void DropDownList1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				ReloadDropDownList();
				GetDataGrid();
			}
			catch(Exception ee)
			{
				lblErr.Text = ee.Message;
			}
		}

		public void CheckBox1_CheckedChanged(object sender, System.EventArgs e)
		{
			try
			{
				GetDataGrid();
			}
			catch(Exception ee)
			{
				lblErr.Text = ee.Message;
			}
		}
	}
}
