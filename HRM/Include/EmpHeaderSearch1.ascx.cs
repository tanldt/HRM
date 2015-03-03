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
	using iHRPCore.TMSComponent;
	using iHRPCore.Include;

	/// <summary>
	///		Summary description for EmpHeaderSearch1.
	/// </summary>
	public class EmpHeaderSearch1 : System.Web.UI.UserControl
	{
		public System.Web.UI.WebControls.Label Label7;
		public System.Web.UI.WebControls.TextBox txtEmpID;
		public System.Web.UI.WebControls.Label Label2;
		public System.Web.UI.WebControls.TextBox txtEmpName;
		public System.Web.UI.WebControls.Label Label1;
		public System.Web.UI.WebControls.DropDownList cboCompany;
		public System.Web.UI.WebControls.Label Label3;
		public System.Web.UI.WebControls.DropDownList cboLevel1;
		public System.Web.UI.WebControls.Label Label4;
		public System.Web.UI.WebControls.DropDownList cboLevel2;
		public System.Web.UI.WebControls.Label lblSource;
		public System.Web.UI.WebControls.DropDownList cboLSRESourceID;
		public System.Web.UI.WebControls.Label Label5;
		public System.Web.UI.WebControls.DropDownList cboLocation;
		public System.Web.UI.WebControls.Label Label8;
		public System.Web.UI.WebControls.DropDownList cboPosition;
		public System.Web.UI.WebControls.Label Label9;
		public System.Web.UI.WebControls.DropDownList cboJobcode;
		public System.Web.UI.WebControls.Label Label6;
		public System.Web.UI.WebControls.DropDownList cboLevel3;
		public System.Web.UI.WebControls.Label Label10;
		public System.Web.UI.WebControls.RadioButtonList optStatus;
		public System.Web.UI.HtmlControls.HtmlTableRow trStatus;
		public System.Web.UI.HtmlControls.HtmlInputHidden txtLevel2ID;
		public System.Web.UI.HtmlControls.HtmlInputHidden txtLevel3ID;
		public System.Web.UI.HtmlControls.HtmlInputHidden txtLevel1ID;
		public System.Web.UI.WebControls.TextBox txtShortName;
		public System.Web.UI.WebControls.Label Label11;
		public string strLanguage = "VN";
		public string strAscx="";
		public System.Web.UI.WebControls.Label Label12;
		public System.Web.UI.WebControls.Label Label13;
		public System.Web.UI.WebControls.DropDownList cboLocalExpat;
		public System.Web.UI.WebControls.DropDownList cboGender;
		public string strUserGroupID="";

		public void Page_Load(object sender, System.EventArgs e)
		{
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
			strAscx=Request.Params["Ascx"];	
			strUserGroupID=Session["AccountLogin"].ToString();
			
			if (!Page.IsPostBack)
			{
				LoadComboBox();
				if (Request.Params["Ascx"] != null)
				{
					if (Request.Params["Ascx"].Trim() == "Reports\\Reports.ascx")
					{
						trStatus.Attributes.Add("style","display:none");	
						this.optStatus.SelectedValue = "";
					}

					int index = Request.Params["Ascx"].Trim().IndexOf("MdlWF");
					if ( index >= 0)
					{

						trStatus.Attributes.Add("style","display:none");	
						this.optStatus.SelectedValue = "";
					}
				}
				if (Request.Params["IsTermination"] != null)
				{
					optStatus.SelectedValue = "";
					trStatus.Attributes.Add("style","display:none");	
				}
			}
			//if (cboCompany.Items.Count==2) cboCompany.SelectedIndex=1;
		}

		public void LoadComboBox()
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			clsHREmpList.LoadComboCompany(cboCompany, strTextField, strLanguage,this.Page);
			clsHREmpList.LoadComboLevel1(cboLevel1,strTextField,strLanguage,this.Page);  
			clsHREmpList.LoadComboPosition(cboPosition,strTextField);  
			clsHREmpList.LoadComboLocation(cboLocation, strTextField);
			clsHREmpList.LoadComboJobCode(cboJobcode, strTextField);
			clsCommon.LoadDropDownListControl(cboLSRESourceID,"sp_GetDataCombo @TableName='LS_tblRESource',@Fields='LSRESourceID as [ID]," + strTextField + " as Name'","ID","Name",true);

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
			this.cboLevel2.SelectedIndexChanged += new System.EventHandler(this.cboLevel2_SelectedIndexChanged);
			this.cboCompany.SelectedIndexChanged += new System.EventHandler(this.cboCompany_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		public void cboCompany_SelectedIndexChanged(object sender, System.EventArgs e)
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

		public void cboLevel1_SelectedIndexChanged(object sender, System.EventArgs e)
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

		public void cboLevel2_SelectedIndexChanged(object sender, System.EventArgs e)
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

		

	}
}
