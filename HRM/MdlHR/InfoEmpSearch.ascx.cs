namespace iHRPCore.MdlHR
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.HRComponent;
	using iHRPCore.Com;

	/// <summary>
	///		Summary description for InfoEmpSearch.
	/// </summary>
	public class InfoEmpSearch : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.HtmlControls.HtmlInputRadioButton optActive;
		protected System.Web.UI.HtmlControls.HtmlInputRadioButton optResign;
		protected System.Web.UI.HtmlControls.HtmlInputRadioButton optMale;
		protected System.Web.UI.HtmlControls.HtmlInputRadioButton optFemale;
		protected System.Web.UI.WebControls.TextBox txtDate;
		protected System.Web.UI.WebControls.ImageButton cmdCal;
		protected System.Web.UI.WebControls.TextBox txtInfo;
		protected System.Web.UI.WebControls.ListBox lstList;
		public string strTextField = "";
		public string strTypeInfo = "txt";
		public DataTable dtbLS = new DataTable();
		protected System.Web.UI.WebControls.LinkButton btnClose;
		protected System.Web.UI.WebControls.LinkButton btnSelect;
		public string strValueField = "";
		protected System.Web.UI.WebControls.Label lblActive;
		protected System.Web.UI.WebControls.Label lblResign;
		protected System.Web.UI.WebControls.Label lblMale;
		protected System.Web.UI.WebControls.Label lblFemale;
		public string strColumnID = "";
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (Request.Params["ColumnID"] != null)
			{
				DataTable dtb = clsHREmpList.GetColumnByID(Request.Params["ColumnID"].Trim());
				if (dtb.Rows.Count > 0)
				{
					strTypeInfo = dtb.Rows[0]["TypeInfo"].ToString();
					strTextField = dtb.Rows[0]["TextField"].ToString();
					strValueField = dtb.Rows[0]["ValueField"].ToString();
					if (dtb.Rows[0]["TableLink"].ToString().Trim() != "")
					{
						dtbLS = clsHREmpList.GetLSTable(dtb.Rows[0]["TableLink"].ToString().Trim());
						this.lstList.DataSource = dtbLS;
						this.lstList.DataTextField = strTextField;
						this.lstList.DataValueField = strValueField;
						this.lstList.DataBind();
					}
					strColumnID = dtb.Rows[0]["Alias"].ToString().Trim() + "." + dtb.Rows[0]["ColumnIDSelect"].ToString().Trim();
				}
				dtb.Dispose();
			}
			btnClose.Attributes.Add("OnClick","window.close()");
			btnSelect.Attributes.Add("OnClick","return SelectItems()");
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
