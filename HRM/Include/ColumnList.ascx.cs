//Hau
namespace GridSort
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for ColumnList.
	/// </summary>
	public class ColumnList : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.DropDownList DropDownList1;
		protected System.Web.UI.WebControls.DropDownList DropDownList2;
		protected System.Web.UI.WebControls.DropDownList DropDownList3;
		protected System.Web.UI.WebControls.TextBox tbPageRows;
		protected System.Web.UI.WebControls.Label lblPageRows;
		protected System.Web.UI.WebControls.Label lblTotalRows;
		public System.Web.UI.WebControls.Label lblTotalRow;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnSort;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnPRChanged;
		protected System.Web.UI.HtmlControls.HtmlTable Table1;
		protected System.Web.UI.WebControls.CheckBox chk_AdvMultiSort;
		protected System.Web.UI.WebControls.CheckBox chkMultiSort;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
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
			this.btnSort.ServerClick += new System.EventHandler(this.btnSort_ServerClick);
			this.btnPRChanged.ServerClick += new System.EventHandler(this.btnPRChanged_ServerClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnPRChanged_ServerClick(object sender, System.EventArgs e)
		{
		
		}

		private void btnSort_ServerClick(object sender, System.EventArgs e)
		{
		
		}

		#region Thuoc tinh cua UserControl

		public int iPageRows
		{
			get
			{
				try
				{
					return (int.Parse(tbPageRows.Text));
				}
				catch
				{
				}
				return 50;
			}
			set 
			{
				this.tbPageRows.Text = value.ToString();  
			}
		}

		public int iTotalRows
		{
			set
			{
				lblTotalRow.Text = value.ToString();
			}
		}

		public Boolean bMultiSort
		{
			get
			{
				return (chkMultiSort.Checked);
			}
		}

		public Boolean bAdvMultiSort
		{
			get
			{
				return (chk_AdvMultiSort.Checked);
			}
		}

		public HtmlInputButton ButtonSort
		{
			get
			{
				return btnSort;
			}
		}

		public HtmlInputButton ButtonPRChanged
		{
			get
			{
				return btnPRChanged;
			}
		}

		#endregion
	}
}
