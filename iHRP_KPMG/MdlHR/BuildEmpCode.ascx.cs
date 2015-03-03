namespace iHRPCore.MdlHR
{
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
	using iHRPCore.Include;

	/// <summary>
	///		Summary description for BuildEmpCode1.
	/// </summary>
	public class BuildEmpCode1 : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label lblLevel1ID;
		protected System.Web.UI.WebControls.DropDownList cboLSCompanyCode;
		protected System.Web.UI.WebControls.Label lblJoinDate;
		protected System.Web.UI.WebControls.TextBox txtStartDate;
		protected System.Web.UI.WebControls.Label lblEmpCode;
		protected System.Web.UI.WebControls.TextBox txtEmpCode;
		protected System.Web.UI.WebControls.LinkButton btnBuild;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected string strLanguage = "VN";
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.DropDownList cboLSLevel1ID;
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

				if (cboLSCompanyCode.Items.Count==2) cboLSCompanyCode.SelectedIndex=1;				
				//cboLSCompanyCode_SelectedIndexChanged(null,null);
			}
			catch(Exception ex )
			{
				lblErr.Text = ex.Message;
			}
		}
		private void LoadDataDefault()
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			//clsCommon.LoadDropDownListControl(cboLSCompanyCode,"sp_GetDataCombo @TableName='LS_tblCompany',@Fields='LSCompanyID as [ID]," + strTextField + " as Name'","ID","Name",true);
			clsHREmpList.LoadComboCompany(cboLSCompanyCode, strTextField, strLanguage,this.Page);
			if(strAction=="addnew")
			{
				txtEmpCode.ReadOnly = true;
			}
			else
			{
				DataRow iRow = clsHRPersonal.GetDataEmpCodeID(Session["EmpID"]);
				if(iRow != null)
				{
					cboLSCompanyCode.SelectedValue = iRow["LSCompanyID"].ToString().Trim();
					cboLSCompanyCode_SelectedIndexChanged(null, null);
					cboLSLevel1ID.SelectedValue = iRow["LSLevel1ID"].ToString().Trim();
					if (Request["StartDate"]==null)
					{
						txtStartDate.Text = iRow["StartDate"].ToString().Trim();
					}
					else
					{
						txtStartDate.Text = Request["StartDate"].Trim();
					}
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.cboLSCompanyCode.SelectedIndexChanged += new System.EventHandler(this.cboLSCompanyCode_SelectedIndexChanged);
			this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnBuild_Click(object sender, System.EventArgs e)
		{
			//txtEmpCode.Text = clsHRPersonal.BuildEmpCode(cboLSCompanyCode.SelectedValue,txtStartDate.Text);
			string sEmpCode=getEmpCode(cboLSLevel1ID.SelectedValue);
			txtEmpCode.Text=sEmpCode;
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

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			string sErr="";
			if (Request["IsTermination"]==null)
			{
				if(strAction=="addnew")
				{
					clsCommon.ImpactDB("","Save",this,"HR_spfrmBuildEmp");
					Session["EmpID"] = txtEmpCode.Text.Trim();
				}
				else
				{	
					DataRow drData = clsCommon.GetDataRow("HR_spfrmBuildEmp @Activity='CheckEmpCode',@EmpCode='" + txtEmpCode.Text + "'");
					if (int.Parse(drData[0].ToString())>0 ) 
					{
						lblErr.Text="EmpCode was existed. Please check again";
						return;
					}
					clsCommon.ImpactDB("@EmpID",Session["EmpID"].ToString().Trim(),"Update",this,"HR_spfrmBuildEmp");
				}
				string strScript = "<script language=JavaScript>";
				strScript += "opener.document.location = 'Editpage.aspx?ModuleID=HR&ParentID=8&FunctionID=19&Ascx=MdlHR/Personal.ascx';";			
				strScript += "window.close();";
				strScript += "</script>";
				Response.Write(strScript);
			}
			else
			{
				DataRow drData = clsCommon.GetDataRow("HR_spfrmBuildEmp @Activity='CheckEmpCode',@EmpCode='" + txtEmpCode.Text + "'");
				if (int.Parse(drData[0].ToString())>0 ) 
				{
					lblErr.Text="Emp ID was existed. Please check again";
					return;
				}
				
				sErr=clsTermination.sReinstate(Session["EmpID"].ToString().Trim(), "False", txtStartDate.Text);
				if (sErr.Equals(""))
				{
					clsCommon.ImpactDB("@EmpID",Session["EmpID"].ToString().Trim(),"Update",this,"HR_spfrmBuildEmp");
					string strScript = "<script language=JavaScript>";
					strScript += "opener.document.location = 'Editpage.aspx?ModuleID=TER&ParentID=136&FunctionID=138&Ascx=MdlHR/TERMINATION.ascx';";			
					strScript += "window.close();";
					strScript += "</script>";
					Response.Write(strScript);
				}
				else
				{
					clsChangeLang.popupWindow(this.Parent,sErr,"",0);
				}
			}
		}

		private void cboLSCompanyCode_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				string strCompanyID = cboLSCompanyCode.SelectedValue.Trim();
				string strTextField = strLanguage == "VN"?"VNName":"Name";			
				if (!strCompanyID.Equals(""))
				{
					clsHREmpList.LoadComboLevel1ByCompany(cboLSLevel1ID,strTextField,strCompanyID, strLanguage,this.Page); 
					//cboLevel1ID.SelectedValue = this.txtLevel1ID.Value.Trim();					
				}
				else
				{
					cboLSLevel1ID.Items.Clear();				
				}
			}
			catch(Exception ex)
			{
				lblErr.Text=ex.ToString();
			}
		}
	}
}
