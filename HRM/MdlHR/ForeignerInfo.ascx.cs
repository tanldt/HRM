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
	///		Summary description for ForeignerInfo.
	/// </summary>
	public class ForeignerInfo : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected System.Web.UI.WebControls.LinkButton LinkButton2;
		protected System.Web.UI.WebControls.Label lblWorkPermitTitle;
		protected System.Web.UI.WebControls.Label lblPassportTitle;
		protected System.Web.UI.WebControls.Label lblWorkPermitNumber;
		protected System.Web.UI.WebControls.TextBox txtPassportNumber;
		protected System.Web.UI.WebControls.Label lblVisaNumber;
		protected System.Web.UI.WebControls.Label lblWorkPermitIssusedDate;
		protected System.Web.UI.WebControls.TextBox txtWorkPermitIssusedDate;
		protected System.Web.UI.WebControls.Label lblPassportIssusedDate;
		protected System.Web.UI.WebControls.TextBox txtPassportIssusedDate;
		protected System.Web.UI.WebControls.Label lblVisaIssusedDate;
		protected System.Web.UI.WebControls.TextBox txtVisaIssusedDate;
		protected System.Web.UI.WebControls.Label lblWorkPermitEffectDate;
		protected System.Web.UI.WebControls.Label lblPassportEffectDate;
		protected System.Web.UI.WebControls.TextBox txtPassportEffectDate;
		protected System.Web.UI.WebControls.Label lblVisaEffectDate;
		protected System.Web.UI.WebControls.TextBox txtVisaEffectDate;
		protected System.Web.UI.WebControls.Label lblWorkPermitExpiredDate;
		protected System.Web.UI.WebControls.TextBox txtWorkPermitExpiredDate;
		protected System.Web.UI.WebControls.Label lblPassportExpiredDate;
		protected System.Web.UI.WebControls.Label lblVisaExpiredDate;
		protected System.Web.UI.WebControls.Label lblWorkPermitIssuedPlace;
		protected System.Web.UI.WebControls.Label lblPassportIssuedPlace;
		protected System.Web.UI.WebControls.Label lblVisaIssuedPlace;
		protected System.Web.UI.WebControls.Label lblViseTitle;
		protected System.Web.UI.WebControls.TextBox txtWorkPermitNumber;
		protected System.Web.UI.WebControls.Label lblPassportNumber;
		protected System.Web.UI.WebControls.TextBox txtVisaNumber;
		protected System.Web.UI.WebControls.TextBox txtWorkPermitEffectDate;
		protected System.Web.UI.WebControls.TextBox txtPassportExpiredDate;
		protected System.Web.UI.WebControls.TextBox txtVisaExpiredDate;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnList;
		protected System.Web.UI.WebControls.TextBox txtVisaIssuedPlace;
		protected System.Web.UI.WebControls.TextBox txtPassportIssuedPlace;
		protected System.Web.UI.WebControls.TextBox txtWorkPermitIssuedPlace;
		protected System.Web.UI.WebControls.ImageButton cmdWPEffectDate;
		protected System.Web.UI.WebControls.ImageButton cmdCalWPermitIssusedDate;
		protected System.Web.UI.WebControls.ImageButton Visa_ExpiredDate;
		protected System.Web.UI.WebControls.Label lblWP_Title;
		protected System.Web.UI.WebControls.Label lblWP_Number;
		protected System.Web.UI.WebControls.TextBox txtWP_Number;
		protected System.Web.UI.WebControls.Label lblWP_IssusedDate;
		protected System.Web.UI.WebControls.Label lblWP_EffectDate;
		protected System.Web.UI.WebControls.Label lblWP_ExpiredDate;
		protected System.Web.UI.WebControls.TextBox txtWP_ExpiredDate;
		protected System.Web.UI.WebControls.Label lblWP_IssuedPlace;
		protected System.Web.UI.WebControls.TextBox txtWP_IssuedPlace;
		protected System.Web.UI.WebControls.Label lblPass_Title;
		protected System.Web.UI.WebControls.Label lblPass_Number;
		protected System.Web.UI.WebControls.TextBox txtPass_Number;
		protected System.Web.UI.WebControls.Label lblPass_IssusedDate;
		protected System.Web.UI.WebControls.Label lblPass_EffectDate;
		protected System.Web.UI.WebControls.Label lblPass_ExpiredDate;
		protected System.Web.UI.WebControls.TextBox txtPass_ExpiredDate;
		protected System.Web.UI.WebControls.Label lblPass_IssuedPlace;
		protected System.Web.UI.WebControls.TextBox txtPass_IssuedPlace;
		protected System.Web.UI.WebControls.Label lblVisa_Title;
		protected System.Web.UI.WebControls.Label lblVisa_Number;
		protected System.Web.UI.WebControls.TextBox txtVisa_Number;
		protected System.Web.UI.WebControls.Label lblVisa_IssusedDate;
		protected System.Web.UI.WebControls.Label lblVisa_EffectDate;
		protected System.Web.UI.WebControls.Label lblVisa_ExpiredDate;
		protected System.Web.UI.WebControls.TextBox txtVisa_ExpiredDate;
		protected System.Web.UI.WebControls.Label lblVisa_IssuedPlace;
		protected System.Web.UI.WebControls.TextBox txtVisa_IssuedPlace;
		protected System.Web.UI.WebControls.TextBox txtWP_EffDate;
		protected System.Web.UI.WebControls.TextBox txtPass_EffDate;
		protected System.Web.UI.WebControls.TextBox txtVisa_EffDate;
		protected System.Web.UI.WebControls.TextBox txtWP_IssuedDate;
		protected System.Web.UI.WebControls.TextBox txtPass_IssuedDate;
		protected System.Web.UI.WebControls.TextBox txtVisa_IssuedDate;
		protected System.Web.UI.HtmlControls.HtmlTableRow abc;
		protected System.Web.UI.WebControls.LinkButton Linkbutton3;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			Session["CurFunction"] = Request.Params["FunctionID"].Trim();
			if(!Page.IsPostBack)
			{				
				this.LoadDataDefault();
			}
			btnSave.Attributes.Add("OnClick", "return validform()");
		}

		/// <summary>
		/// Load employee info
		/// </summary>
		private void LoadDataDefault()
		{
			try
			{
				DataRow iRow = clsHROtherInfo.GetDataByID(Session["EmpID"]);
				if(iRow !=null)
				{
					txtPass_EffDate.Text = iRow["Pass_EffDate"].ToString().Trim();
					txtPass_ExpiredDate.Text = iRow["Pass_ExpiredDate"].ToString().Trim();
					txtPass_IssuedPlace.Text = iRow["Pass_IssuedPlace"].ToString().Trim();					
					txtPass_IssuedDate.Text = iRow["Pass_IssuedDate"].ToString().Trim();
					txtPass_Number.Text = iRow["Pass_Number"].ToString().Trim();

					txtWP_EffDate.Text = iRow["WP_EffDate"].ToString().Trim();
					txtWP_ExpiredDate.Text = iRow["WP_ExpiredDate"].ToString().Trim();
					txtWP_IssuedPlace.Text = iRow["WP_IssuedPlace"].ToString().Trim();					
					txtWP_IssuedDate.Text = iRow["WP_IssuedDate"].ToString().Trim();
					txtWP_Number.Text = iRow["WP_Number"].ToString().Trim();

					txtVisa_EffDate.Text = iRow["Visa_EffDate"].ToString().Trim();
					txtVisa_ExpiredDate.Text = iRow["Visa_ExpiredDate"].ToString().Trim();
					txtVisa_IssuedDate.Text = iRow["Visa_IssuedDate"].ToString().Trim();					
					txtVisa_IssuedPlace.Text = iRow["Visa_IssuedPlace"].ToString().Trim();
					txtVisa_Number.Text = iRow["Visa_Number"].ToString().Trim();
				}
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnList.Click += new System.EventHandler(this.btnList_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				clsCommon.ImpactDB(Session["EmpID"].ToString().Trim(),"UpdateForeign",this,"HR_spfrmEmpOther");
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
	}
}
