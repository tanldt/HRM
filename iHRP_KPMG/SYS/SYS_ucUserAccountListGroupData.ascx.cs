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
	///		Summary description for SYS_ucUserAccountListGroupData.
	/// </summary>
	public class SYS_ucUserAccountListGroupData : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtUserID;
		protected System.Web.UI.WebControls.LinkButton btnSearch;
		protected System.Web.UI.WebControls.DataGrid dgUserAccountList;
		protected System.Web.UI.WebControls.Literal ltlAlert;

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				this.BindDataGrid();
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.dgUserAccountList.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgUserAccountList_ItemCommand);
			this.dgUserAccountList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgUserAccountList_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region Creater: LANHTD, 23/09/04, BindDataGrid
		private void BindDataGrid()
		{
			try
			{
				DataTable dtb = clsCommon.GetDataTable("UMS_sptblUserAccount 'GetAllGroupData',@result='',@UserGroupID=N'" + this.txtUserID.Text.Trim() + "', @LSCompanyID = '"+Mession.GlbCusGroupID+"'");
				dgUserAccountList.PageSize = 15;
				dgUserAccountList.PagerStyle.Mode = PagerMode.NumericPages;
				dgUserAccountList.DataSource = dtb;
				dgUserAccountList.CurrentPageIndex = 0;
				dgUserAccountList.DataBind();
				dtb.Dispose();
			}
			catch(Exception exp)
			{
				Response.Write(exp.Message.ToString());
			}
		}
		#endregion

		private void dgUserAccountList_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			try
			{
				if (e.CommandName == "hpLink")
				{
					string strUserID = e.Item.Cells[1].Text.Trim();
					string strUserName = e.Item.Cells[1].Text.Trim() + " - " + e.Item.Cells[2].Text.Trim();
					string strFGroup = e.Item.Cells[3].Text.Trim();

					this.ltlAlert.Text = "ReturnEmpPopUp('" + strUserID	+ "','" + strUserName + "','" + strFGroup + "')"; 
				}
			}
			catch
			{
				return;
			}
		}

		private void dgUserAccountList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.BindDataGrid();
			this.dgUserAccountList.CurrentPageIndex = e.NewPageIndex;
			dgUserAccountList.DataBind();
		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			BindDataGrid();
		}
	}
}
