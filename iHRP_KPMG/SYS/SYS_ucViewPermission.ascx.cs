namespace iHRPCore.SYS
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

	/// <summary>
	///		Summary description for SYS_ucViewPermission.
	/// </summary>
	public class SYS_ucViewPermission : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.WebControls.Label lblUserIDCaption;
		protected System.Web.UI.WebControls.TextBox txtEmpID;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtEmpName;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.DataGrid dgUserAccount;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.DataGrid dgFunction;
		public Array NewValues = Array.CreateInstance(typeof(String),100);
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.HtmlControls.HtmlTableRow trAccountInfo;		
		public string LastDateValue  = "";

		private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				if (!Page.IsPostBack)
				{
					if(Request.Params["UserGroupID"] != null && (Request.Params["IsUser"] != null || Request.Params["IsGroup"] != null))
					{
						string strCurUser=Request.Params["UserGroupID"];
						string strtail = Request.Params["IsUser"] != null?"USER ":" GROUP ";
						this.lblTitle.Text += " " + strtail + strCurUser;
						DataTable tbl = clsCommon.GetDataTable("UMS_sptblUserAccount 'GetByCurUser',@result='',@UserGroupID ='"+ strCurUser +"'");
						if(tbl.Rows.Count>0 )
						{
							txtEmpID.Text=tbl.Rows[0]["UserGroupID"].ToString();
							txtEmpName.Text = tbl.Rows[0]["UserGroupName"].ToString();
						}
						else 
						{
							Response.Write("<script>window.close();</script>");
						}
					}
					else
						Response.Write("<script>window.close();</script>");
					BindDataGrid(Request.Params["UserGroupID"].Trim());
				}
			}
			catch
			{}
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
			this.dgUserAccount.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgUserAccount_DeleteCommand);
			this.dgUserAccount.SelectedIndexChanged += new System.EventHandler(this.dgUserAccount_SelectedIndexChanged);
			this.dgFunction.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgFunction_CancelCommand);
			this.dgFunction.PreRender += new System.EventHandler(this.dgFunction_PreRender);
			this.dgFunction.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgFunction_EditCommand);
			this.dgFunction.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgFunction_UpdateCommand);
			this.dgFunction.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgFunction_DeleteCommand);
			this.dgFunction.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgFunction_ItemDataBound);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		/// <summary>
		/// Ket du lieu vao datagrid
		/// </summary>
		private void BindDataGrid(string strUserAccount)
		{
			try
			{
				DataTable tbl = clsCommon.GetDataTable("UMS_sptblUserAccount 'GetGroups',@result='', @UserGroupID='" + strUserAccount + "'");
				this.dgUserAccount.DataSource = tbl;
				this.dgUserAccount.DataBind();
				tbl = clsCommon.GetDataTable("UMS_sptblUserAccount 'GetPermissionU',@result = '',@UserGroupID = '" + strUserAccount + "'");
				this.dgFunction.DataSource = tbl;
				this.dgFunction.DataBind();
				for(int i = 0; i<dgFunction.Items.Count; i++)
				{
					string strSpecific = dgFunction.Items[i].Cells[10].Text.ToString().Trim();
					if (strSpecific == "0")
					{
						if (dgFunction.Items[i].Cells[0].HasControls())
						{
							for (int j = 0; j<dgFunction.Items[i].Cells[0].Controls.Count; j++)
							{
								Control ctrl = (Control)dgFunction.Items[i].Cells[0].Controls[j];
								if (!ctrl.HasControls())
								{
									string strCtrlType = ctrl.GetType().ToString().Trim();
									if (strCtrlType == "System.Web.UI.WebControls.DataGridLinkButton")
									{
										ctrl.Visible = false;
									}
								}
							}
						}
						if (dgFunction.Items[i].Cells[9].HasControls())
						{
							for (int j = 0; j<dgFunction.Items[i].Cells[9].Controls.Count; j++)
							{
								Control ctrl = (Control)dgFunction.Items[i].Cells[9].Controls[j];
								if (!ctrl.HasControls())
								{
									string strCtrlType = ctrl.GetType().ToString().Trim();
									if (strCtrlType == "System.Web.UI.WebControls.DataGridLinkButton")
									{
										ctrl.Visible = false;
									}
								}
							}
						}
					}
				}
			}
			catch(Exception exp)
			{
				string strError = exp.Message.ToString();
			}
		}

		private void dgUserAccount_DeleteCommand(object source, DataGridCommandEventArgs e)
		{
			try
			{
				string strCurUser=Request.Params["UserGroupID"].ToString().Trim(); 
				string str_ID = e.Item.Cells[0].Text.Trim();
				string sql = "Exec UMS_sptblUserAccount 'DeleteUser/Group',@UserID='" + str_ID + "',@GroupID='" + strCurUser + "'";
				clsCommon.GetDataTable(sql);
				dgUserAccount.EditItemIndex = -1;
				BindDataGrid(strCurUser);
			}
			catch(Exception exp)
			{
				string strError = exp.Message.ToString();
			}
		}

		private void dgFunction_EditCommand(object source, DataGridCommandEventArgs e)
		{
			try
			{
				string strCurUser=Request.Params["UserGroupID"]; 
				this.dgFunction.EditItemIndex = e.Item.ItemIndex;
				BindDataGrid(strCurUser);
				this.dgFunction.Items[e.Item.ItemIndex].BackColor = System.Drawing.Color.YellowGreen;
			}
			catch(Exception exp)
			{
				string strError = exp.Message.ToString();
			}
		}

		private void dgFunction_UpdateCommand(object source, DataGridCommandEventArgs e)
		{
			try
			{
				string strCurUser=Request.Params["UserGroupID"]; 
				string str_ID = this.dgFunction.DataKeys[e.Item.ItemIndex].ToString();
				string strFRun = ((TextBox)this.dgFunction.Items[e.Item.ItemIndex].FindControl("txtFRun")).Text.Trim();
				string strFAdd = ((TextBox)this.dgFunction.Items[e.Item.ItemIndex].FindControl("txtFAdd")).Text.Trim();
				string strFEdit = "0";
				string strFApp = "0";
				string strFDel = "0";
				if (strFRun != "1") strFRun = "1";//Luon dam bao it nhat user/group user co quyen run, neu khong thi delete han
				if (strFAdd != "1") strFAdd = "0";//ngoai tru gia tri 1, con lai xem nhu khong co quyen
				else 
				{
					strFEdit = "1";
					strFApp = "1";
					strFDel = "1";
				}
				string sql = "Exec UMS_sptblUserAccount 'EditFunctionRight',@FAdd='" + strFAdd + "',@FEdit='"
					+ strFEdit + "',@FRun='" + strFRun + "',@FDel='" + strFDel + "',@FApp='" + strFApp + "',@UserGroupID='" + strCurUser + "',@FunctionID='" + str_ID + "'";
				clsCommon.GetDataTable(sql);
				dgFunction.EditItemIndex = -1;
				BindDataGrid(strCurUser);
			}
			catch(Exception exp)
			{
				string strError = exp.Message.ToString();
			}
		}

		private void dgFunction_CancelCommand(object source, DataGridCommandEventArgs e)
		{
			try
			{
				string strCurUser=Request.Params["UserGroupID"].ToString().Trim(); 
				dgFunction.EditItemIndex = -1;
				BindDataGrid(strCurUser);
			}
			catch(Exception exp)
			{
				string strError = exp.Message.ToString();
			}
		}

		private void dgFunction_DeleteCommand(object source, DataGridCommandEventArgs e)
		{
			try
			{
				string strCurUser=Request.Params["UserGroupID"].ToString().Trim(); 
				string str_ID = this.dgFunction.DataKeys[e.Item.ItemIndex].ToString();
				string sql = "Exec UMS_sptblUserAccount 'DeleteFunctionRight',@UserGroupID='" + strCurUser + "',@FunctionID='" + str_ID + "'";
				clsCommon.GetDataTable(sql);
				dgFunction.EditItemIndex = -1;
				BindDataGrid(strCurUser);
			}
			catch(Exception exp)
			{
				string strError = exp.Message.ToString();
			}
		}

		private void dgFunction_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
//			if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
//			{
//				NewValues.SetValue("",e.Item.ItemIndex);
//				if (e.Item.Cells[11].Text.Trim() != LastDateValue)
//				{
//					LastDateValue = e.Item.Cells[11].Text.Trim();
//					NewValues.SetValue(e.Item.Cells[11].Text.Trim(),e.Item.ItemIndex);
//				}
//			}
		}

		private void dgFunction_PreRender(object sender, EventArgs e)
		{
//			try
//			{
//				DataGrid DG = (DataGrid)sender;
//				Table Tbl = (Table)DG.Controls[0];
//				DataGridItem DGI ;
//				TableCell Cell;
//				int i ; 
//				int iAdded =0;
//			
//
//				for(i=0;i<NewValues.GetUpperBound(0);i++)
//				{
//					if(NewValues.GetValue(i).ToString()!="")
//					{
//						//Just so it picks up the formatting class for my Header, could have used ListItemType.Item
//						DGI= new DataGridItem(0,0,ListItemType.Header);
//						Cell=new TableCell();
//						Cell.ColumnSpan=6;
//						Cell.Width=500;
//						Cell.BackColor= System.Drawing.Color.Beige;
//						Cell.Text=NewValues.GetValue(i).ToString();
//						Cell.Font.Bold = true;
//						DGI.Cells.Add(Cell);
//						Tbl.Controls.AddAt(i+iAdded+1,DGI);
//						iAdded = iAdded + 1;
//					}
//				}
//			}
//			catch
//			{
//			}
		}

		private void dgUserAccount_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}
	}
}
