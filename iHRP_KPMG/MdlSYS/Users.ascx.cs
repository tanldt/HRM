namespace iHRPCore.MdlSYS
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for Users.
	/// </summary>
	public class Users : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.LinkButton Linkbutton4;
		protected System.Web.UI.WebControls.LinkButton Linkbutton11;
		protected System.Web.UI.WebControls.DataGrid dtgAccount;
		protected System.Web.UI.WebControls.DataGrid dtgGroup;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.TextBox txtEmpID;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.CheckBox chkAdmin;
		protected System.Web.UI.WebControls.CheckBox chkActivate;
		protected System.Web.UI.WebControls.CheckBox Checkbox1;
		protected System.Web.UI.WebControls.CheckBox Checkbox2;
		protected System.Web.UI.HtmlControls.HtmlTableRow trGroups;
		protected System.Web.UI.WebControls.LinkButton btnAddNew;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton Linkbutton1;

		private void Page_Load(object sender, System.EventArgs e)
		{
			//Temp
			DataTable dtb = new DataTable();
			DataColumn cl = new DataColumn("No");
			cl.AutoIncrement = true;
			dtb.Columns.Add(cl);
			cl = new DataColumn("Content");
            dtb.Columns.Add(cl);
			for(int i=0; i<10 ;i++)
			{
				DataRow dr = dtb.NewRow();
				dr["Content"] = "00" + i.ToString();
				dtb.Rows.Add(dr);
			}
			dtgAccount.DataSource = dtb;
			dtgAccount.DataBind();
			dtgGroup.DataSource = dtb;
			dtgGroup.DataBind();
			//Temp
			btnAddNew.Attributes.Add("OnClick","return OpenWindow()");
			for(int i=0; i<dtgAccount.Items.Count; i++)
			{
				((LinkButton)dtgAccount.Items[i].FindControl("lnkLinkAccount")).Attributes.Add("Onclick","return OpenPermissionDetails()");
			}
			for(int i=0; i<dtgAccount.Items.Count; i++)
			{
				((LinkButton)dtgGroup.Items[i].FindControl("lnkLinkGroup")).Attributes.Add("Onclick","return OpenPermissionDetails()");
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
