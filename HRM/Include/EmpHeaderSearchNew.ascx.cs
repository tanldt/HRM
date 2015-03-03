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
	///		Summary description for EmpHeaderSearch.
	/// </summary>
	public class EmpHeaderSearchNew : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label Label7;
		public System.Web.UI.WebControls.TextBox txtEmpID;
		public System.Web.UI.WebControls.TextBox txtEmpName;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label9;
		public System.Web.UI.WebControls.DropDownList cboLocation;
		public System.Web.UI.WebControls.DropDownList cboJobcode;
		public System.Web.UI.WebControls.DropDownList cboPosition;
		public System.Web.UI.WebControls.DropDownList cboLevel1;
		public System.Web.UI.WebControls.DropDownList cboLevel2;
		public System.Web.UI.WebControls.DropDownList cboLevel3;
		public System.Web.UI.HtmlControls.HtmlInputHidden txtLevel2ID;
		public System.Web.UI.HtmlControls.HtmlInputHidden txtLevel3ID;
		protected System.Web.UI.WebControls.Label Label2;
		public System.Web.UI.WebControls.DropDownList cboCompany;
		public System.Web.UI.WebControls.RadioButtonList optStatus;
		protected System.Web.UI.WebControls.Label Label10;
		public System.Web.UI.HtmlControls.HtmlTableRow trStatus;
		public System.Web.UI.HtmlControls.HtmlInputHidden txtLevel1ID;
		protected System.Web.UI.WebControls.Label Label11;
		public System.Web.UI.WebControls.TextBox txtOldEmpCode;
		public string strLanguage = "EN";

		public void Page_Load(object sender, System.EventArgs e)
		{
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
			if (!Page.IsPostBack)
			{
				LoadComboBox();
				if (Request.Params["Ascx"] != null)
				{
					if ((Request.Params["UseStatus"]!= null && Request.Params["UseStatus"].Trim() == "0")
						|| (Request.Params["Ascx"].Trim() == "Reports\\Reports.ascx" && Request.Params["UseStatus"] == null))
					{
						trStatus.Attributes.Add("style","display:none");	
						//this.optStatus.SelectedValue = "";
					}
				}
				if (Request.Params["IsTermination"] != null)
				{
					optStatus.SelectedValue = "2";
					//trStatus.Attributes.Add("style","display:none");	
				}
			}
		}

		public void LoadComboBox()
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			clsHREmpList.LoadComboLevel1(cboLevel1, strTextField,strLanguage,this.Page);
			clsHREmpList.LoadComboLocation(cboLocation, strTextField);
			clsHREmpList.LoadComboPosition(cboPosition, strTextField);
			clsHREmpList.LoadComboJobCode(cboJobcode, strTextField);
			clsHREmpList.LoadComboCompany(cboCompany, strTextField);
			if (Session["CompanyID"] != null)
				cboCompany.SelectedValue = Session["CompanyID"].ToString().Trim();
			if (Session["Level1ID"] != null)
				txtLevel1ID.Value = Session["Level1ID"].ToString().Trim();
			if (Session["Level2ID"] != null)
				txtLevel2ID.Value = Session["Level2ID"].ToString().Trim();
			cboCompany_SelectedIndexChanged(null,null);
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
		public void InitializeComponent()
		{
			this.cboLevel1.SelectedIndexChanged += new System.EventHandler(this.cboLevel1_SelectedIndexChanged);
			this.cboCompany.SelectedIndexChanged += new System.EventHandler(this.cboCompany_SelectedIndexChanged);
			this.cboLevel2.SelectedIndexChanged += new System.EventHandler(this.cboLevel2_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		public void cboCompany_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string strCompanyID = cboCompany.SelectedValue.Trim();
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			clsHREmpList.LoadComboLevel1ByCompany(cboLevel1, strTextField,strCompanyID);
			cboLevel1.SelectedValue = this.txtLevel1ID.Value.Trim();
			cboLevel1_SelectedIndexChanged(null,null);
		}

		public void cboLevel1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string strCompanyID = cboCompany.SelectedValue.Trim();
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			clsHREmpList.LoadComboLevel1ByCompany(cboLevel1, strTextField,strCompanyID);
			cboLevel1.SelectedValue = this.txtLevel1ID.Value.Trim();
			string strLevel1ID = cboLevel1.SelectedValue.Trim();
			clsHREmpList.LoadComboLevel2ByLevel1(cboLevel2, strTextField,strLevel1ID);
			cboLevel2.SelectedValue = this.txtLevel2ID.Value.Trim();
			cboLevel2_SelectedIndexChanged(null,null);
		}

		public void cboLevel2_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string strLevel2ID = cboLevel2.SelectedValue.Trim();
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			clsHREmpList.LoadComboLevel3ByLevel2(cboLevel3, strTextField,strLevel2ID);
			cboLevel3.SelectedValue = this.txtLevel3ID.Value.Trim();
		}

		

	}
}
