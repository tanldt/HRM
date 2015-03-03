namespace iHRPCore.MdlHR
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
	///		Summary description for DocumentSimple.
	/// </summary>
	public class DocumentSimple : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.LinkButton btnList;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.Label lblStorePlace_S;
		protected System.Web.UI.WebControls.CheckBoxList ChkDocumentType;
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.TextBox txtStorePlace;
		public string strLanguage = "EN";

		private void Page_Load(object sender, System.EventArgs e)
		{
			Session["CurFunction"] = Request.Params["FunctionID"].Trim();
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";	
			this.lblErr.Text = "";

			if(!Page.IsPostBack)
			{
				LoadCheckBoxList();
			}		
		}

		/// <summary>
		/// Get data for checkboxlist
		/// </summary>
		private void LoadCheckBoxList()
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			clsCommon.LoadCheckBoxList(ChkDocumentType,"sp_GetDataCombo @TableName = 'LS_tblDocument',@Fields='LSDocumentID as [ID]," + strTextField + " as Name'","ID","Name");

			DataTable rsData = new DataTable();
			rsData = clsHREmpDocument.GetDataByEmpID(Session["EmpID"]);
			for(int i=0;i<rsData.Rows.Count;i++)
			{
				for(int j =0; j < ChkDocumentType.Items.Count;j++)
				{
					if(rsData.Rows[i]["LSDocumentID"].ToString().Trim()==ChkDocumentType.Items[j].Value.Trim())
						ChkDocumentType.Items[j].Selected = true;
				}					
			}
			if(rsData.Rows.Count > 0)
				this.txtStorePlace.Text = rsData.Rows[0]["StorePlace"].ToString().Trim();
			rsData.Dispose();
		}


		private void btnSave_Click(object sender, System.EventArgs e)
		{			
			try
			{
				string strLSDocumentID = "";
				for(int i=0;i<ChkDocumentType.Items.Count;i++)
				{
					if(ChkDocumentType.Items[i].Selected)
						strLSDocumentID += ChkDocumentType.Items[i].Value + "$";
				}
				clsHREmpDocument.UpdateEmpDocumentSimple (Session["EmpID"].ToString().Trim(), strLSDocumentID, this.txtStorePlace.Text);				
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}

		private void btnList_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(Request.ApplicationPath.Trim() + "/Editpage.aspx?ModuleID=HR&ParentID=8&FunctionID=14&Ascx=MdlHR/EmpList.ascx");
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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnList.Click += new System.EventHandler(this.btnList_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
