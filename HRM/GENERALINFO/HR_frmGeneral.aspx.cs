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

namespace HRWebServiceC.UserServices.GeneralInfo
{
	/// <summary>
	/// Summary description for HR_frmGeneral.
	/// </summary>
	public class HR_frmGeneral : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.ImageButton lbtBirthday;
		protected System.Web.UI.WebControls.ImageButton lbtChangeJobPos;
		protected System.Web.UI.WebControls.ImageButton lbtQuitEmployee;
		protected System.Web.UI.WebControls.ImageButton lbtNewEmployee;
		protected System.Web.UI.WebControls.ImageButton lbtSubOrdinate;
		protected System.Web.UI.WebControls.ImageButton lbtOrganization;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (Session["CurUser"] != null)
			{
				this.lbtSubOrdinate.Visible = true;
				this.lbtOrganization.Visible = true;
			}
			else
			{
				this.lbtSubOrdinate.Visible = false;
				this.lbtOrganization.Visible = false;
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
			this.lbtBirthday.Click += new System.Web.UI.ImageClickEventHandler(this.lbtBirthday_Click);
			this.lbtChangeJobPos.Click += new System.Web.UI.ImageClickEventHandler(this.lbtChangeJobPos_Click);
			this.lbtQuitEmployee.Click += new System.Web.UI.ImageClickEventHandler(this.lbtQuitEmployee_Click);
			this.lbtSubOrdinate.Click += new System.Web.UI.ImageClickEventHandler(this.lbtSubOrdinate_Click);
			this.lbtOrganization.Click += new System.Web.UI.ImageClickEventHandler(this.lbtOrganization_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void lbtBirthday_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../HR_frmEmpBirthday.aspx");
		}

		private void lbtChangeJobPos_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../HR_frmMonthChangeJobPos.aspx");
		}

		private void lbtQuitEmployee_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../HR_frmMonthQuitEmployee.aspx");
		}

		private void lbtNewEmployee_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../HR_frmMonthNewEmployee.aspx");
		}

		private void lbtSubOrdinate_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("HR_frmSubOrdinate.aspx");
		}

		private void lbtOrganization_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("HR_frmWholeOfOrganization.aspx");
		}

	}
}
