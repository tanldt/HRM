namespace Reports.RE
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Web_DM.Component;
	using System.Collections;
	using iHRPCore.Com;	
	using ExcelReportDll;
	using iHRPCore.Com;
	using System.Configuration;
	using iHRPCore.REComponent;
	/// <summary>
	///		Summary description for RE_frmF04.
	/// </summary>
	public class RE_frmF04 : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label Label18;
		protected System.Web.UI.WebControls.Label Label17;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.HtmlControls.HtmlTableRow RECOGNITION;
		protected System.Web.UI.WebControls.DropDownList cboProjectID;
		protected System.Web.UI.WebControls.LinkButton btnPrint;
		protected System.Web.UI.WebControls.TextBox txtCandidateID;
		protected System.Web.UI.WebControls.TextBox txtCandidateName;
		public string strLanguage = "VN";	
		private void Page_Load(object sender, System.EventArgs e)
		{
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";		
			AddJavaScript();			
			if(!Page.IsPostBack)
			{				
				LoadDataCombo();
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
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		public void AddJavaScript()
		{					
			btnPrint.Attributes.Add("Onclick","return CheckView()");
		}
		private void btnPrint_Click(object sender, System.EventArgs e)
		{	
			string strParams = "";
			string strValues = "";					
			Session["ssReportName"] = "RE/RE_rptF04.rpt";
			strParams = "@Language;@CandidateName;@CandidateID;@ProjectID";	
			strValues = strLanguage + ";" + txtCandidateID.Text + ";" + txtCandidateName.Text + ";" + cboProjectID.SelectedValue.Trim();							
			Session["ssReportParams"] = strParams;
			Session["ssReportValues"] = strValues;
			clsCommon.OpenNewWindow(this.Page,"Reports/ShowReports.aspx","");		
		}
		private void LoadDataCombo()
		{			
			clsCommon.LoadDropDownListControl(cboProjectID,"sp_GetDataCombo @TableName='RE_tblProject',@Fields='ProjectID as [ID]," + "ProjectCode" + " as Name'","ID","Name",true);	
		}
	}
}
