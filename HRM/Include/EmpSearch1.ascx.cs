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
	///		Summary description for EmpSearch1.
	/// </summary>
	public class EmpSearch1 : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label Label7;
		public System.Web.UI.WebControls.TextBox txtEmpID;
		protected System.Web.UI.WebControls.Label Label2;
		public System.Web.UI.WebControls.TextBox txtEmpName;
		protected System.Web.UI.WebControls.Label Label1;
		public System.Web.UI.WebControls.DropDownList cboCompany;
		protected System.Web.UI.WebControls.Label Label3;
		public System.Web.UI.WebControls.DropDownList cboLevel1;
		protected System.Web.UI.WebControls.Label Label4;
		public System.Web.UI.WebControls.DropDownList cboLevel2;
		protected System.Web.UI.WebControls.Label Label6;
		public System.Web.UI.WebControls.DropDownList cboLevel3;
		public System.Web.UI.HtmlControls.HtmlInputHidden txtLevel2ID;
		public System.Web.UI.HtmlControls.HtmlInputHidden txtLevel3ID;
		protected System.Web.UI.WebControls.Label Label10;
		public System.Web.UI.WebControls.RadioButtonList optStatus;
		public System.Web.UI.HtmlControls.HtmlInputHidden txtLevel1ID;
		public string strLanguage = "VN";
		string strCo = "";
		string strL1= "";
		string strL2= "";
		string strL3= "";

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
			if (Session["Co"] != null && Session["L1"] != null && Session["L2"] != null&& Session["L3"] != null) 
			{
				strCo = Session["Co"].ToString();
				strL1 = Session["L1"].ToString();
				strL2 = Session["L2"].ToString();
				strL3 = Session["L3"].ToString();
			}

			LoadComboBox();
			cboCompany.SelectedValue = strCo.Trim();
			cboLevel1.SelectedValue = strL1.Trim();
			cboLevel2.SelectedValue = strL2.Trim();
			cboLevel3.SelectedValue = strL3.Trim();

		}

		public void LoadComboBox()
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			clsHREmpList.LoadComboCompany(cboCompany, strTextField, strLanguage,this.Page);
			//clsHREmpList.LoadComboLevel1(cboLevel1,strTextField,strLanguage,this.Page);  
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
			this.cboCompany.SelectedIndexChanged+=new EventHandler(cboCompany_SelectedIndexChanged);
			this.cboLevel1.SelectedIndexChanged+=new EventHandler(cboLevel1_SelectedIndexChanged);
			this.cboLevel2.SelectedIndexChanged+=new EventHandler(cboLevel2_SelectedIndexChanged);
		}
		#endregion
		private void cboLevel1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			string strLevel1ID = cboLevel1.SelectedValue.Trim();
			if (!strLevel1ID.Equals(""))
			{
				clsHREmpList.LoadComboLevel2ByLevel1(cboLevel2,strTextField,strLevel1ID, strLanguage,this.Page); 
				cboLevel2.SelectedValue = this.txtLevel2ID.Value.Trim();
				cboLevel2_SelectedIndexChanged(null,null);
			}
			else
			{
				cboLevel2.Items.Clear();
			}
		}

		private void cboLevel2_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			string strLevel2ID = cboLevel2.SelectedValue.Trim();
			if (!strLevel2ID.Equals(""))
			{
				clsHREmpList.LoadComboLevel3ByLevel2(cboLevel3,strTextField,strLevel2ID, strLanguage); 
				cboLevel3.SelectedValue = this.txtLevel3ID.Value.Trim();
			}
			else
			{
				cboLevel3.Items.Clear();
			}
		}

		private void cboCompany_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			string strCompanyID = cboCompany.SelectedValue.Trim();
			if (!strCompanyID.Equals(""))
			{
				clsHREmpList.LoadComboLevel1ByCompany(cboLevel1,strTextField,strCompanyID, strLanguage,this.Page); 
				cboLevel1.SelectedValue = this.txtLevel1ID.Value.Trim();
				cboLevel1_SelectedIndexChanged(null,null);
			}
			else
			{
				cboLevel1.Items.Clear();
			}
		}
	}
}
