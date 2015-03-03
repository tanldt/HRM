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
	using System.Data.SqlClient;

	/// <summary>
	///		Summary description for NewEmployee.
	/// </summary>
	public class NewEmployee : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblJoinDate;
		protected System.Web.UI.WebControls.TextBox txtStartDate;
		protected System.Web.UI.WebControls.Label lblEmpCode;
		protected System.Web.UI.WebControls.TextBox txtEmpCode;
		protected System.Web.UI.WebControls.Label lblGender;
		protected System.Web.UI.WebControls.DropDownList cboGender;
		protected System.Web.UI.WebControls.Label lblLastName;
		protected System.Web.UI.WebControls.TextBox txtVLastName;
		protected System.Web.UI.WebControls.Label lblFirstName;
		protected System.Web.UI.WebControls.TextBox txtVFirstName;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.Label lblCompany;
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList cboCompanyID;
		protected System.Web.UI.WebControls.DropDownList cboLevel1ID;
		protected System.Web.UI.WebControls.DropDownList cboLevel2ID;
		protected System.Web.UI.WebControls.DropDownList cboLevel3ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel1ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel2ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel3ID;
		protected System.Web.UI.WebControls.LinkButton btnGenCode;
		protected System.Web.UI.WebControls.CheckBox chkIsCheckIDNo;
		public string strLanguage ="VN";
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			strLanguage = Session["LangID"] != null? Session["LangID"].ToString().Trim():"VN";
			try
			{				
				if(!Page.IsPostBack)
				{
					LoadDataDefault();
				}
				cboGender.SelectedValue=cboGender.Items[2].Value.Trim();
				string strEmpCode = getEmpCode("");
				txtEmpCode.Text=strEmpCode.Trim();
				txtStartDate.Text=System.DateTime.Now.ToString("dd-MMM-yyyy");
				cboCompanyID_SelectedIndexChanged(null,null);

				//btnBuild.Attributes.Add("OnClick", " return validform()");
				btnSave.Attributes.Add("OnClick", " return validform()");
			}
			catch(Exception ex )
			{
				//lblErr.Text = ex.Message;
			}
		}
		private void LoadDataDefault()
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			//clsCommon.LoadDropDownListControl(cboCompanyID,"sp_GetDataCombo @TableName='LS_tblCompany',@Fields='LSCompanyID as [ID]," + strTextField + " as Name'","ID","Name",true);
			clsHREmpList.LoadComboCompany(cboCompanyID, strTextField, strLanguage,this.Page);
			clsCommon.LoadDropDownListControl(cboLevel1ID,"sp_GetDataCombo @TableName='LS_tbllevel1',@Fields='LSlevel1ID as [ID]," + strTextField + " as Name'","ID","Name",true);
			clsCommon.LoadDropDownListControl(cboLevel2ID,"sp_GetDataCombo @TableName='LS_tbllevel2',@Fields='LSlevel2ID as [ID]," + strTextField + " as Name'","ID","Name",true);
			clsCommon.LoadDropDownListControl(cboGender,"HR_spfrmEMPCV @Activity='getGender',@languageID='" + strLanguage + "' ","ID","Name",true);			
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
			this.cboCompanyID.SelectedIndexChanged += new System.EventHandler(this.cboCompanyID_SelectedIndexChanged);
			this.cboLevel1ID.SelectedIndexChanged += new System.EventHandler(this.cboLevel1ID_SelectedIndexChanged);
			this.cboLevel2ID.SelectedIndexChanged += new System.EventHandler(this.cboLevel2ID_SelectedIndexChanged);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnGenCode.Click += new System.EventHandler(this.btnGenCode_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		private void btnSave_Click(object sender, System.EventArgs e)
		{			
			DataRow drData = clsCommon.GetDataRow("HR_spfrmBuildEmp @Activity='CheckEmpCode',@EmpCode='" + txtEmpCode.Text + "'");
			if (int.Parse(drData[0].ToString())>0 ) 
			{
				//lblErr.Text="Mã nhân viên đã bị trùng. Xin kiểm tra lại";
				lblErr.Text="Code was existed. Please check again.";
					return;
			}
			clsCommon.ImpactDB("","Save",this,"HR_spfrmBuildEmp");
			drData=clsCommon.GetDataRow("HR_spfrmBuildEmp @Activity='getEmpID',@EmpCode='" + txtEmpCode.Text + "'");
            Session["EmpID"] = drData["EmpID"].ToString();						
			string strScript = "<script language=JavaScript>";
			strScript += "document.location = 'Editpage.aspx?ModuleID=HR&ParentID=8&FunctionID=19&Ascx=MdlHR/Personal.ascx';";						
			strScript += "</script>";
			Response.Write(strScript);
		}
		private string getEmpCode(string level)
		{
			//DataRow drData= clsCommon.GetDataRow("HR_spfrmBuildEmp @Activity='BuildEmpCode'");			
			string strSql = "HR_spfrmBuildEmp @Activity='GetEmpCode', @level1id='"+level.Trim()+"'";
												
			DataRow drData= clsCommon.GetDataRow(strSql);
			if (drData!=null)
				return drData["EmpCode"].ToString();
			else
				return "";
		}

		private void cboCompanyID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			string strCompanyID = cboCompanyID.SelectedValue.Trim();
			if (!strCompanyID.Equals(""))
			{
				clsHREmpList.LoadComboLevel1ByCompany(cboLevel1ID,strTextField,strCompanyID, strLanguage,this.Page); 
				cboLevel1ID.SelectedValue = this.txtLevel1ID.Value.Trim();
				cboLevel1ID_SelectedIndexChanged(null,null);
			}
			else
			{
				cboLevel1ID.Items.Clear();
			}			
		}

		private void cboLevel1ID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			string strLevel1ID = cboLevel1ID.SelectedValue.Trim();
			//clsHREmpList.LoadComboLevel2ByLevel1(cboLevel2ID,strTextField,strLevel1ID, strLanguage,this.Page); 
			//cboLevel2ID.SelectedValue = this.txtLevel2ID.Value.Trim();
			//cboLevel2ID_SelectedIndexChanged(null,null);
			if (!strLevel1ID.Equals(""))
			{
				clsHREmpList.LoadComboLevel2ByLevel1(cboLevel2ID,strTextField,strLevel1ID, strLanguage,this.Page); 
				cboLevel2ID.SelectedValue = this.txtLevel2ID.Value.Trim();
				cboLevel2ID_SelectedIndexChanged(null,null);
			}
			else
			{
				//cboLevel2.Items.Clear();
			}
		}

		private void cboLevel2ID_SelectedIndexChanged(object sender, System.EventArgs e)
		{			
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			string strLevel2ID = cboLevel2ID.SelectedValue.Trim();
			//clsHREmpList.LoadComboLevel3ByLevel2(cboLevel3ID,strTextField,strLevel2ID, strLanguage); 
			//cboLevel3ID.SelectedValue = this.txtLevel3ID.Value.Trim();
			if (!strLevel2ID.Equals(""))
			{
				clsHREmpList.LoadComboLevel3ByLevel2(cboLevel3ID,strTextField,strLevel2ID, strLanguage); 
				cboLevel3ID.SelectedValue = this.txtLevel3ID.Value.Trim();
			}
			else
			{
				cboLevel3ID.Items.Clear();
			}
		}

		private void btnGenCode_Click(object sender, System.EventArgs e)
		{
			string sEmpCode=getEmpCode(txtLevel1ID.Value.Trim());
			txtEmpCode.Text=sEmpCode;
		}
	}
}
