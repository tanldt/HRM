namespace iHRPCore.MdlSYS
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for Permissions.
	/// </summary>
	public class Permissions : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.DropDownList cboLevel2;
		protected System.Web.UI.WebControls.DropDownList Dropdownlist1;
		protected System.Web.UI.WebControls.TextBox txtEmpID;
		protected System.Web.UI.WebControls.LinkButton Linkbutton11;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.ListBox ListBox1;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.DropDownList cboGroup;
		protected System.Web.UI.WebControls.LinkButton Linkbutton8;
		protected System.Web.UI.WebControls.LinkButton Linkbutton7;
		protected System.Web.UI.WebControls.DataGrid dtgFunctionSelected;
		protected System.Web.UI.WebControls.CheckBox chkCopy;

		private void Page_Load(object sender, System.EventArgs e)
		{
			//Temp
			DataTable dtb = new DataTable();
			DataColumn cl = new DataColumn("No");
			cl.AutoIncrement = true;
			dtb.Columns.Add(cl);
			for(int i=0; i<10 ;i++)
			{
				DataRow dr = dtb.NewRow();
				dtb.Rows.Add(dr);
			}
			dtgFunctionSelected.DataSource = dtb;
			dtgFunctionSelected.DataBind();
			//Temp
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
