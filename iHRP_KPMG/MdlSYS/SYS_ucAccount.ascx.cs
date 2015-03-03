namespace iHRPCore
{
	using System;
	using System.Collections;
	using System.ComponentModel;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.SessionState;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Data.SqlClient;
	using System.Configuration;
	using iHRPCore.Com;
	using iHRPCore.Component;
	using iHRPCore.HRComponent;
	using GridSort;
	using FPTToolWeb.imgver;
	using iHRPCore.SendMail;

	/// <summary>
	///		Summary description for SYS_ucAccount.
	/// </summary>
	public class SYS_ucAccount : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.DataGrid dtgList;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.DropDownList cboEmpID;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label lblEmpID;
		protected ColumnList uctrlColumns;
		protected System.Web.UI.WebControls.LinkButton btnSearch;
		protected System.Web.UI.WebControls.LinkButton btnAddnew;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.LinkButton btnResetPass;
		protected System.Web.UI.WebControls.DropDownList cboCopyPer;
		protected System.Web.UI.WebControls.TextBox txtPageLoad;
		protected System.Web.UI.WebControls.TextBox txtUserGroupName;
		protected System.Web.UI.WebControls.TextBox txtUserGroupID;
		protected System.Web.UI.WebControls.TextBox txtPassword;
		protected System.Web.UI.WebControls.TextBox txtUserAccountID;
		protected System.Web.UI.WebControls.TextBox txtConfirmPass;
		protected System.Web.UI.HtmlControls.HtmlTableRow trPass;
		protected System.Web.UI.WebControls.TextBox txtFlag;
		protected System.Web.UI.WebControls.CheckBox chkSelectAll;
		protected System.Web.UI.WebControls.CheckBox ChkPassAuto;
		protected System.Web.UI.HtmlControls.HtmlTableRow trPassAuto;
		string strLanguage = "EN";
		protected System.Web.UI.HtmlControls.HtmlInputButton btnSearch_Replace;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.DropDownList cboLSCompanyID;
		public RandomText rd = new RandomText();
		private void Page_Load(object sender, System.EventArgs e)
		{
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
			if (!Page.IsPostBack)
			{
				BindDataGrid();
				LoadComboData();
				Session["ucAccount"]="addnew";
				this.txtUserGroupID.ReadOnly=false;
				this.txtUserGroupID.CssClass="InputReadOnly";
			}
			btnSave.Attributes.Add("Onclick","return checkSave();");
			btnDelete.Attributes.Add("Onclick","return checkdelete();");
			btnResetPass.Attributes.Add("Onclick","return checkReset();");
			btnAddnew.Attributes.Add("Onclick","return checkAddnew();");
			ButtonClick();
		}
		private void LoadComboData()
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			clsCommon.LoadDropDownListControl(cboEmpID," HR_clsCommon @Activity='getCboEmpHeader_UMS'","ID","Name",true); 
			clsCommon.LoadDropDownListControl(cboCopyPer," UMS_sptblUserAccount @Activity='GetCopy', @LSCompanyID = '"+Mession.GlbCusGroupID+"'","ID","UserGroupName",true);
			clsHREmpList.LoadComboCompany(cboLSCompanyID, strTextField, strLanguage,this.Page);
		}
		private void BindDataGrid()
		{
			DataTable dtData= new DataTable();
			string strEmpID = cboEmpID.SelectedValue.Trim();
			string strUserGroupName = txtUserGroupName.Text;
			string strUserGroupID = txtUserGroupID.Text;

			//dtData= clsCommon.GetDataTable("UMS_sptblUserAccount 'GetAllUsers', @result=''");
			dtData= clsCommon.GetDataTable("UMS_sptblUserAccount 'GetAllUsersNew', @result='',@EmpID='" + strEmpID + "',@UserGroupName=N'" + strUserGroupName + "',@UserGroupID='" + strUserGroupID + "',@LSCompanyID = '"+Mession.GlbCusGroupID+"'");

			try
			{
				dtgList.DataSource= dtData;
				dtgList.DataBind();

				//Hau
				this.dtgList.PageSize = uctrlColumns.iPageRows;
					
				if(this.uctrlColumns.bAdvMultiSort)
				{
					try
					{
						DataGridSort.AdvancedMultiSort(dtgList, dtData, uctrlColumns);
					}
					catch
					{
						this.dtgList.CurrentPageIndex = 0;
						DataGridSort.AdvancedMultiSort(dtgList, dtData, uctrlColumns);
					}
				}
				else
				{
					try
					{
						DataGridSort.Refresh(dtgList, dtData);
					}
					catch
					{
						this.dtgList.CurrentPageIndex = 0;
						DataGridSort.Refresh(dtgList, dtData);
					}
				}
				this.uctrlColumns.iTotalRows = dtData.Rows.Count;//
				for(int i=0; i<this.dtgList.Items.Count; i++)
				{
					if (this.dtgList.Items[i].Cells[2].Text.Trim() == "True")
						((CheckBox)this.dtgList.Items[i].FindControl("chkValid")).Checked = true;
					else
						((CheckBox)this.dtgList.Items[i].FindControl("chkValid")).Checked = false;

					if (this.dtgList.Items[i].Cells[1].Text.Trim() == "True")
						((CheckBox)this.dtgList.Items[i].FindControl("chkAdm")).Checked = true;
					else
						((CheckBox)this.dtgList.Items[i].FindControl("chkAdm")).Checked = false;
				}
			}
			catch(Exception ex)
			{
				clsChangeLang.popupWindowExp(this.Parent,ex);
			}
			finally
			{
				dtData.Dispose();
			}

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

			int intTopRow =0;
			if ( txtPageLoad.Text == "1" )
			{
				intTopRow = dtgList.PageSize;
				txtPageLoad.Text = "0";
			}			
			DataTable dtData=clsCommon.GetDataTable("UMS_sptblUserAccount 'GetAllUsers', @result=''");
			return dtData;
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
			}
			catch{}
		}
		#endregion		

		private void dtgList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			try
			{
				BindDataGrid();
				dtgList.CurrentPageIndex=e.NewPageIndex;
				dtgList.DataBind();	
				for(int i=0; i<this.dtgList.Items.Count; i++)
				{
					if (this.dtgList.Items[i].Cells[2].Text.Trim() == "True")
						((CheckBox)this.dtgList.Items[i].FindControl("chkValid")).Checked = true;
					else
						((CheckBox)this.dtgList.Items[i].FindControl("chkValid")).Checked = false;

					if (this.dtgList.Items[i].Cells[1].Text.Trim() == "True")
						((CheckBox)this.dtgList.Items[i].FindControl("chkAdm")).Checked = true;
					else
						((CheckBox)this.dtgList.Items[i].FindControl("chkAdm")).Checked = false;
				}
			}
			catch(Exception ex)			
			{
				DataGridSort.Grid_IndexChanged(dtgList, Read_Data(), e);
				//clsChangeLang.popupWindowExp(this.Parent,ex);
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
			this.btnAddnew.Click += new System.EventHandler(this.btnAddnew_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnResetPass.Click += new System.EventHandler(this.btnResetPass_Click);
			this.dtgList.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgList_ItemCommand);
			this.dtgList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgList_PageIndexChanged);
			this.dtgList.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dtgList_SortCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private int getStatusSave()
		{
			int iReturn=0;
			for(int i=0;i<dtgList.Items.Count;i++)
			{
				if(((CheckBox)dtgList.Items[i].FindControl("chkSelect")).Checked==true)
				{
					iReturn=1;
					break;
				}
			}
			return iReturn;
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			//Update on Grid
			string sErr="";
			if (getStatusSave()==1)
			{	
				sErr=SaveGrid();
			}
			//Update 
			if (txtFlag.Text=="1" || txtFlag.Text=="2")
			{
				try
				{
					if (ChkPassAuto.Checked==true)
					{
						txtPassword.Text=txtUserGroupID.Text.Trim();//rd.GenerateRandomCode();
						txtConfirmPass.Text=txtPassword.Text;
					}					
					txtPassword.Text=FPTToolWeb.Encrypt.Encryption.Encrypt(txtPassword.Text);
					txtConfirmPass.Text=txtPassword.Text;

					if (Session["ucAccount"].ToString()=="addnew")
					{
						sErr+=clsCommon.sImpactDB("Save",this,"UMS_sptblUserAccount");						
					}
					else
					{
						sErr+=clsCommon.sImpactDB("Update",this,"UMS_sptblUserAccount");					
					}
					
				}
				catch(Exception ex)
				{
					clsChangeLang.popupWindowExp_Mess(this.Parent,ex);
				}
			}
			if (sErr.Length<12)
			{
				btnAddnew_Click(null,null);				
				clsChangeLang.popupWindow(this.Parent,"0044",strLanguage,"",1);
			}
			else
			{
				clsChangeLang.popupWindow(this.Parent,sErr,"",0);
			}
		}
		private string SaveGrid()
		{
			string sReturn="";
			CheckBox chkFAdmin= new CheckBox();
			CheckBox chkFValid= new CheckBox();
			CheckBox obj= new CheckBox();
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
					
				for(int i=0;i<dtgList.Items.Count;i++)
				{
					chkFAdmin=(CheckBox)dtgList.Items[i].FindControl("chkAdm");
					chkFValid=(CheckBox)dtgList.Items[i].FindControl("chkValid");
					obj=(CheckBox)dtgList.Items[i].FindControl("chkSelect");
					if (obj.Checked)
					{
						cmd.CommandText = "UMS_sptblUserAccount";
						cmd.Parameters.Clear();

						cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 50).Value = "UpdateGrid";
						cmd.Parameters.Add("@UserGroupID", SqlDbType.NVarChar, 20).Value = dtgList.Items[i].Cells[0].Text;
						cmd.Parameters.Add("@FAdm",SqlDbType.Bit).Value=chkFAdmin.Checked;
						cmd.Parameters.Add("@FValid",SqlDbType.Bit).Value=chkFValid.Checked;
						cmd.ExecuteNonQuery();
					}
				}

				obj.Dispose();
				cmd.Transaction.Commit();			
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				return sReturn;
			}
			catch(Exception ex)
			{
				obj.Dispose();
				cmd.Transaction.Rollback();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				return ex.ToString();
			}									
		}

		private void dtgList_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				string sValue;
				DataRow iRow;
				if(e.CommandName.Trim().ToUpper() =="EDIT")
				{
					sValue = dtgList.Items[e.Item.ItemIndex].Cells[0].Text.Trim();
					clsCommon.LoadDropDownListControl(cboCopyPer," UMS_sptblUserAccount @Activity='GetCopy',@UserGroupID='" + sValue + "'","ID","UserGroupName",true);
					iRow = clsUMS.getAccountByID(sValue);					
					if(iRow != null)
					{
						cboEmpID.SelectedValue=iRow["EmpID"].ToString();						
						txtUserGroupName.Text = iRow["UserGroupName"].ToString();
						txtUserGroupName.ToolTip = iRow["UserGroupName"].ToString();
						txtUserGroupID.Text = iRow["UserGroupID"].ToString();
						txtUserGroupID.ToolTip=iRow["UserGroupID"].ToString();
						cboCopyPer.SelectedValue = iRow["CopyPer"].ToString();	
						cboLSCompanyID.SelectedValue = iRow["LSCompanyID"].ToString();	
						
					}

				}				
				this.txtUserGroupID.ReadOnly=true;
				this.txtUserGroupID.CssClass="InputReadOnly";
				//trPass.Style.Add("DISPLAY","none");
				trPassAuto.Style.Add("DISPLAY","none");
				Session["ucAccount"] = "Edit";

			}
			catch(Exception ex)
			{
				clsChangeLang.popupWindowExp(this.Parent,ex);
			}
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				string strID="";
				for(int i=0;i<dtgList.Items.Count;i++)
				{
					if(((CheckBox)dtgList.Items[i].FindControl("chkSelect")).Checked==true)
					{
						strID += dtgList.Items[i].Cells[0].Text.Trim() + "$";
					}
				}
				clsCommon.DeleteListRecord("UMS_sptblUserAccount","UserGroupID",SqlDbType.NVarChar,20,strID);
				BindDataGrid();	
				btnAddnew_Click(null, null);
				clsChangeLang.popupWindow(this.Parent,"0044",strLanguage,"",1);			
			}
			catch(Exception ex)
			{
				clsChangeLang.popupWindowExp_Mess(this.Parent,ex);
			}
		}

		private void btnResetPass_Click(object sender, System.EventArgs e)
		{
			try
			{
				string strID="";
				string strPass="";
				string strSubject = clsChangeLang.getStringAlert("SYS_SM001",strLanguage);
				//string strBody=clsChangeLang.getStringAlert("SYS_SM002",strLanguage);
				string strEmpIDToMail="";
				string strEmpIDFromMail="";
				for(int i=0;i<dtgList.Items.Count;i++)
				{
					if(((CheckBox)dtgList.Items[i].FindControl("chkSelect")).Checked==true)
					{
						strID += dtgList.Items[i].Cells[0].Text.Trim() + "$";
						strPass += FPTToolWeb.Encrypt.Encryption.Encrypt(dtgList.Items[i].Cells[0].Text.Trim()) + "$";
						strEmpIDToMail=dtgList.Items[i].Cells[3].Text.Trim();
						strEmpIDFromMail=clsCommon.LoadHRInfo();					
						//strBody = string.Format(strBody,clsChangeLang.getEmpName(strEmpIDToMail),dtgList.Items[i].Cells[0].Text.Trim(),FPTToolWeb.Encrypt.Encryption.Decrypt(clsUMS.getPassDefault(dtgList.Items[i].Cells[0].Text.Trim())));
						string strBody=clsChangeLang.getStringAlert("SYS_SM002",strLanguage) + dtgList.Items[i].Cells[7].Text.Trim() + clsChangeLang.getStringAlert("SYS_SM004",strLanguage) + dtgList.Items[i].Cells[0].Text.Trim() + clsChangeLang.getStringAlert("SYS_SM005",strLanguage) + dtgList.Items[i].Cells[0].Text.Trim() + clsChangeLang.getStringAlert("SYS_SM006",strLanguage);
						strBody = string.Format(strBody,clsChangeLang.getEmpName(strEmpIDToMail),txtUserGroupID.Text,txtPassword.Text);
						if (strEmpIDFromMail!="" && strEmpIDToMail!="")
						{
							clsSendMail.SendMailApprove(strEmpIDFromMail,strEmpIDToMail,strSubject,strBody,null);
						}
					}
				}
				clsUMS.ResetPass("UMS_sptblUserAccount","UserGroupID",SqlDbType.NVarChar,12,strID,strPass);											
				btnAddnew_Click(null, null);
				clsChangeLang.popupWindow(this.Parent,"0044",strLanguage,"",1);			
			}
			catch(Exception ex)
			{
				clsChangeLang.popupWindowExp_Mess(this.Parent,ex);
			}
		}

		private void btnAddnew_Click(object sender, System.EventArgs e)
		{			
			this.txtUserGroupID.ReadOnly=false;
			this.txtUserGroupID.CssClass="InputReadOnly";
			this.txtUserGroupName.ToolTip="";
			txtUserGroupID.ToolTip="";
			//trPass.Style.Add("DISPLAY","none");
			trPassAuto.Style.Add("DISPLAY","none");
			clsCommon.ClearControlValue(this,"MdlSYS/SYS_ucAccount.ascx");
			Session["ucAccount"] = "addnew";
			BindDataGrid();
			clsCommon.LoadDropDownListControl(cboCopyPer," UMS_sptblUserAccount @Activity='GetCopy'","ID","UserGroupName",true);

		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			BindDataGrid();
		}

		

	}
}
