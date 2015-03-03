namespace iHRPCore.Include
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.Com;
	using iHRPCore.HRComponent;

	/// <summary>
	///		Summary description for EmpHeader.
	/// </summary>
	public class EmpHeader : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label Label7;
		public System.Web.UI.WebControls.TextBox txtEmpID;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label lblStartDate;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label lblPosition;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label lblLevel1Name;
		protected System.Web.UI.WebControls.Label lblLevel2Name;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLengthEmpID;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label lblLevel3Name;
		protected System.Web.UI.WebControls.Label lblCompanyName;
		protected System.Web.UI.WebControls.Label lblLocationName;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label lblEmpType;
		public System.Web.UI.WebControls.TextBox txtEmpName;


		public void Page_Load(object sender, System.EventArgs e)
		{
			//if (!Page.IsPostBack)
			{
				DataTable tbl = new DataTable();
				tbl = clsHREmpList.GetEmpIDLength();
				if (tbl.Rows.Count > 0)
					this.txtLengthEmpID.Value = tbl.Rows[0][0].ToString().Trim();
				string strEmpID = Request.Params["EmpID"] != null? Request.Params["EmpID"].Trim():
					(Session["EmpID"] != null? Session["EmpID"].ToString():"");
				if (strEmpID != "")
				{
					tbl = clsHREmpList.GetEmpList(strEmpID,this.Page,"0");
					if (tbl.Rows.Count > 0)
					{
						this.txtEmpID.Text = tbl.Rows[0]["EmpCode"].ToString().Trim();
						this.txtEmpID.ToolTip = tbl.Rows[0]["EmpCode"].ToString().Trim();
						this.txtEmpName.Text = tbl.Rows[0]["EmpName"].ToString().Trim();
						this.txtEmpName.ToolTip = tbl.Rows[0]["EmpName"].ToString().Trim();
						this.lblStartDate.Text = tbl.Rows[0]["StartDateStr"].ToString().Trim();
						this.lblLevel1Name.Text = tbl.Rows[0]["Level1Name"].ToString().Trim();
						this.lblLevel2Name.Text = tbl.Rows[0]["Level2Name"].ToString().Trim();
						this.lblPosition.Text = tbl.Rows[0]["PositionName"].ToString().Trim();
						this.lblLocationName.Text = tbl.Rows[0]["LocationName"].ToString().Trim();
						this.lblCompanyName.Text = tbl.Rows[0]["CompanyName"].ToString().Trim();
						this.lblLevel3Name.Text = tbl.Rows[0]["Level3Name"].ToString().Trim();
						this.lblEmpType.Text=tbl.Rows[0]["EmpTypeName"].ToString().Trim();
					}
				}
				tbl.Dispose();
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
