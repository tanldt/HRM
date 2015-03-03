namespace iHRPCore.MdlPR
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.Com;	
	using iHRPCore.PRComponent;	
	/// <summary>
	///		Summary description for IndividualSalary.
	/// </summary>
	public class CreatePayRoll : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtMonth;
		protected System.Web.UI.WebControls.TextBox txtFromMonth;
		protected System.Web.UI.WebControls.TextBox txtSalPeriod;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdFromDate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdToDate;
		protected System.Web.UI.WebControls.LinkButton btnNewPayroll;
		protected System.Web.UI.WebControls.LinkButton btnCopyPayroll;
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.DropDownList cboSalPeriod;
		public string strLanguage="VN";
		private void Page_Load(object sender, System.EventArgs e)
		{
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
			// Put user code to initialize the page here
			if (! Page.IsPostBack)
			{
				DataRow drData= clsPRCreateNewPayroll.LoadCurrentPeriod();								
				if (drData!=null)
				{
					txtMonth.Text=drData["Month"].ToString();
					txtSalPeriod.Text = drData["SalPeriod"].ToString();
					hdFromDate.Value = drData["FromDate"].ToString();
					hdToDate.Value = drData["ToDate"].ToString();
					clsCommon.LoadDropDownListControl(cboSalPeriod,"PR_spfrmCreatePayroll @Activity='LoadSalPeriod'","ID","Name",true);
				}
			}
			btnCopyPayroll.Attributes["OnClick"] = "return CheckData()";
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
			this.btnNewPayroll.Click += new System.EventHandler(this.btnNewPayroll_Click);
			this.btnCopyPayroll.Click += new System.EventHandler(this.btnCopyPayroll_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion				

		private void btnCopyPayroll_Click(object sender, System.EventArgs e)
		{
			string strReturn = "";
			try
			{				
				string strCheck = string.Format(clsPRCollection.CheckIsPRForCopy(strLanguage,cboSalPeriod.SelectedValue.Trim(),txtFromMonth.Text.Trim()),txtSalPeriod.Text,txtMonth.Text);
				if(strCheck.Trim()!= "")
				{
					clsChangeLang.popupWindow(this.Parent,strCheck,"",0);
					return;
				}
			
				strReturn = clsPRCreateNewPayroll.CopyPayroll(hdFromDate.Value,hdToDate.Value,txtMonth.Text,txtFromMonth.Text,cboSalPeriod.SelectedValue);
				if(strReturn.Trim()!="")
				{
					lblErr.Text = "";
					//clsCommon.ShowMessageBox(this.Page,clsChangeLang.getStringAlert("CP_0001",strLanguage));
					clsChangeLang.popupWindow(this.Parent,"CP_0001",strLanguage,"",0);

				}
				else
					//clsCommon.ShowMessageBox(this.Page,clsChangeLang.getStringAlert("CP_0002",strLanguage));
					clsChangeLang.popupWindow(this.Parent,"CP_0002",strLanguage,"",1);
			}
			catch
			{
				lblErr.Text = strReturn;
			}
			finally
			{
			}
		}

		private void btnNewPayroll_Click(object sender, System.EventArgs e)
		{
			string strReturn = "";
			try
			{		
				strReturn = clsPRCreateNewPayroll.CreatePayroll(hdFromDate.Value,hdToDate.Value,txtMonth.Text,txtFromMonth.Text,cboSalPeriod.SelectedValue);
				if(strReturn.Trim()!="")
				{
					lblErr.Text = "";
					//clsCommon.ShowMessageBox(this.Page,clsChangeLang.getStringAlert("CP_0001",strLanguage));
					clsChangeLang.popupWindow(this.Parent,"CP_0001",strLanguage,"",0);

				}
				else
					//clsCommon.ShowMessageBox(this.Page,clsChangeLang.getStringAlert("CP_0002",strLanguage));
					clsChangeLang.popupWindow(this.Parent,"CP_0002",strLanguage,"",1);
			}
			catch
			{
				lblErr.Text = strReturn;
			}
			finally
			{
			}
		}
		
	}
}
