namespace iHRPCore.Include
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for Rank.
	/// </summary>
	public class Rank : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.LinkButton btnRemove;
		protected System.Web.UI.WebControls.LinkButton btnSelect;
		protected System.Web.UI.WebControls.ListBox ListBox1;
		public string strLanguage = "VN";
		public string strTableName="";
		public string strField="";


		private void Page_Load(object sender, System.EventArgs e)
		{
			/*strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
			strTableName=Session[""]
			if (!Page.IsPostBack)
			{
				string strSQL;

            strSQL = "sp_Rank @Activity='get_list',@LanguageID='" & strLanguage & "',@TableName='" & pv_sMa & "'"

            Dim drData As DataTable = clsCommon.GetDataTable(strSQL)
            Dim i As Integer
            For i = 0 To drData.Rows.Count - 1
                lst_sapxep.Items.Add(New MyItem_(drData.Rows(i)("Ten"), drData.Rows(i)("STT"), drData.Rows(i)("Ma")))
            Next
			}*/
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
