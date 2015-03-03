namespace Reports.PIT
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for PIT_05A_BK_TNCN_BDK.
	/// </summary>
	public class PIT_05A_BK_TNCN_BDK : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtMMYYYY;
		protected System.Web.UI.WebControls.Button cmdView;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if (!this.IsPostBack)
			{
				this.txtMMYYYY.Text=System.DateTime.Now.ToString("MM/yyyy");
			}
			cmdView.Attributes.Add("onclick", "return checkvalidSearch()");
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
