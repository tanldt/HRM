namespace Include
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
	///		Summary description for CreaterFooter.
	/// </summary>
	public class CreaterFooter : System.Web.UI.UserControl
	{
		protected System.Web.UI.HtmlControls.HtmlTableRow trID;
		protected System.Web.UI.WebControls.LinkButton btnAuthor;
		public string strLanguage = "EN";
		protected System.Web.UI.WebControls.Literal ltlAlert;
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
			if (Request["Ascx"]!=null)
			{
				LoadData(Request["Ascx"].ToString());
				RemoveAuthority();
			}
			
		}
		private void LoadData(string sAscx)
		{
			DataRow drData= clsCommon.GetDataRow("HR_clsCommon @Activity='GetFooter',@Ascx='" + sAscx + "'");
			if (drData!=null)
			{				
				trID.Style.Add("DISPLAY","display");							
			}
			else
			{
				trID.Style.Add("DISPLAY","none");
			}		
			
		}
		private void RemoveAuthority()
		{
			if (Session["AccountLogin"]!=null && Request["Ascx"]!=null)
			{
				DataTable dtData = clsCommon.GetDataTable("[SYS_spfrmAUTHORITY] @Activity='getAuthorityByEmp', @UserGroupID='" + Session["AccountLogin"].ToString() + "', @FunctionID='" + Request["Ascx"].ToString() + "'");
				if (dtData.Rows.Count==1)
				{
					if (Session["EmpID_Autho"]!=null)
					{
						Session["EmpID"]=clsChangeLang.getEmpFromAccount(Session["AccountLogin"].ToString(),0);
						Session.Remove("EmpID_Autho");
						this.ltlAlert.Text="Reload()";
					}
				}
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
			this.btnAuthor.Click += new System.EventHandler(this.btnAuthor_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnAuthor_Click(object sender, System.EventArgs e)
		{
			
			clsCommon.OpenNewWindowPopup(this.Page,"FormPage.aspx?Ascx=MdlSYS/SetAuthority.ascx&FunPer=" + Request["Ascx"].ToString(),"");
		}
	}
}
