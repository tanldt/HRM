using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using iHRPCore.Com;
using iHRPCore.HRComponent;
namespace iHRPCore.MdlHR
{
	/// <summary>
	/// Summary description for BuildEmpCode.
	/// </summary>
	public class BuildEmpCode : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label lblLevel1ID;
		protected System.Web.UI.WebControls.Label lblJoinDate;
		protected System.Web.UI.WebControls.Label lblEmpCode;
		protected System.Web.UI.WebControls.DropDownList cboLSCompanyCode;
		protected System.Web.UI.WebControls.TextBox txtStartDate;
		protected System.Web.UI.WebControls.LinkButton btnBuild;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.TextBox txtEmpCode;
		protected string strLanguage = "EN";
		protected string strAction = "";
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			try
			{
				strAction  = Request.Params["action"].ToString().Trim();
				if(!Page.IsPostBack)
				{
					LoadDataDefault();
				}
				btnBuild.Attributes.Add("OnClick", " return validform()");
				btnSave.Attributes.Add("OnClick", " return checkaccept()");
			}
			catch(Exception ex )
			{
				lblErr.Text = ex.Message;
			}
		}
		private void LoadDataDefault()
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			clsCommon.LoadDropDownListControl(cboLSCompanyCode,"sp_GetDataCombo @TableName='LS_tblCompany',@Fields='LSCompanyCode," + strTextField + " as Name'","LSCompanyCode","Name",true);
			if(strAction=="addnew")
			{
				txtEmpCode.ReadOnly = true;
			}
			else
			{
				DataRow iRow = clsHRPersonal.GetDataEmpCodeID(Session["EmpID"]);
				if(iRow != null)
				{
					cboLSCompanyCode.SelectedValue = iRow["LSCompanyCode"].ToString().Trim();
					txtStartDate.Text = iRow["StartDate"].ToString().Trim();
					txtEmpCode.Text = iRow["EmpCode"].ToString().Trim();
				}
				txtEmpCode.ReadOnly = false;
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnBuild_Click(object sender, System.EventArgs e)
		{
			txtEmpCode.Text = clsHRPersonal.BuildEmpCode(cboLSCompanyCode.SelectedValue,txtStartDate.Text);
		}
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			if(strAction=="addnew")
			{
				clsCommon.ImpactDB("","Save",this,"HR_spfrmBuildEmp");
				Session["EmpID"] = txtEmpCode.Text.Trim();
			}
			else
				clsCommon.ImpactDB("@EmpID",Session["EmpID"].ToString().Trim(),"Update",this,"HR_spfrmBuildEmp");
			string strScript = "<script language=JavaScript>";
			strScript += "opener.document.location = opener.document.location;";
			strScript += "window.close();";
			strScript += "</script>";
			Response.Write(strScript);
		}
	}
}
