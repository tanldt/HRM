namespace iHRPCore.MdlPR
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.Com;
	using iHRPCore.WFComponent;
	using iHRPCore.PRComponent;
	using iHRPCore.Component;

	/// <summary>
	///		Summary description for ListOfSalary.
	/// </summary>
	public class ListOfSalary : UserControlCommon
	{
		protected System.Web.UI.WebControls.DataGrid dtgList;
		protected System.Web.UI.WebControls.LinkButton Linkbutton1;
		protected System.Web.UI.WebControls.LinkButton btnAddnew;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.HtmlControls.HtmlGenericControl Div1;
		protected System.Web.UI.WebControls.Button btnRefresh;

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				BindDataGrid();			
			}
			this.btnAddnew.Attributes.Add("OnClick","return PopUp_Addnew();");
			this.btnDelete.Attributes.Add("OnClick","return checkdelete();");
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
			this.dtgList.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgList_ItemCommand);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		private void BindDataGrid()
		{
			
			DataTable dtb = new DataTable();
			dtb=clsCommon.GetDataTable("PR_spfrmListOfSalary @Activity = 'GetDataAll'");			
			this.dtgList.DataSource = dtb;
			this.dtgList.DataBind();
		}

		private void dtgList_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
//			if ( ((LinkButton)e.CommandSource).ID == "hpLink" )
//			{
//				//Session("UserId") = e.Ite-+
//				//m.Cells(0).Text.Trim();				
//				string strID = dtgList.Items[e.Item.ItemIndex].Cells[0].Text.Trim();
//				OpenNewWindow(this.Page,"FormPage.aspx?ModuleID=PR&ParentID=68&FunctionID=546&Ascx=MdlPR/ListOfSalary_Popup.ascx&IDCode=" + strID,"");              
//
//			}            
		}
		private void OpenNewWindow(System.Web.UI.Page pPage,string pURL,string pTitle)
		{
			string strScript = "<script language=JavaScript>";				
			strScript += "window.open('" + pURL + "','" + pTitle + "','toolbar=0,status=no,menubar=no,scrollbars=1,resizable=yes,copyhistory=yes,width=800,height=650,align=top')";
			strScript += "</script>";
			if (!pPage.IsStartupScriptRegistered("clientScript"))
				pPage.RegisterStartupScript("clientScript", strScript);			
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
						if(clsPRSalaryItem.CheckPermissionDelete(dtgList.Items[i].Cells[0].Text.Trim())==false)
						{							
							clsChangeLang.popupWindow(this.Parent,clsChangeLang.getStringAlert("0048",strLanguage),strLanguage,1);
							return;
						}
						strID += dtgList.Items[i].Cells[0].Text.Trim() + "$";
					}
				}
				clsCommon.DeleteListRecord("LS_spfrmSalaryItem","LSSalaryItemID",SqlDbType.NVarChar,12,strID);
				BindDataGrid();
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}

		private void btnRefresh_Click(object sender, System.EventArgs e)
		{
			BindDataGrid();
		}
	}
}
