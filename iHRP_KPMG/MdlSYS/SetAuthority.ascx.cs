namespace MdlSYS
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
	using GridSort;
	using FPTToolWeb.imgver;
	using iHRPCore.SendMail;

	/// <summary>
	///		Summary description for SetAuthority.
	/// </summary>
	public class SetAuthority : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label Label32;
		protected System.Web.UI.WebControls.Label Label19;
		protected System.Web.UI.WebControls.DataGrid dtgList;
		protected System.Web.UI.WebControls.Literal ltlAlert;


		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if (Session["AccountLogin"]!=null && Request["FunPer"]!=null)
			{
				DataTable dtData = clsCommon.GetDataTable("[SYS_spfrmAUTHORITY] @Activity='getAuthorityByEmp', @UserGroupID='" + Session["AccountLogin"].ToString() + "', @FunctionID='" + Request["FunPer"].ToString() + "'");
				dtgList.DataSource=dtData;
				dtgList.DataBind();
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
			this.dtgList.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgList_ItemCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void dtgList_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				
				if(e.CommandName.Trim().ToUpper() =="EDIT")
				{	
					Session["EmpID"] = e.Item.Cells[0].Text.Trim();
					Session["EmpID_Autho"]= e.Item.Cells[0].Text.Trim();
					this.ltlAlert.Text="ReloadOpener()";
				}							

			}
			catch(Exception ex)
			{
				clsChangeLang.popupWindowExp(this.Parent,ex);
			}
		}
	}
}
