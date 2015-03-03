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
using iHRPCore.UMSComponent;
using iHRPCore.Include;
using iHRPCore.HRComponent;

namespace iHRPCore
{
	/// <summary>
	/// Summary description for Editpage.
	/// </summary>
	public class Editpage : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.PlaceHolder acxHolder;
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtCurMenu;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtCurParent;
		protected Bottom_LS Bottom_LS1;
		public string blnShowForm = "";
		public string strLanguageID="VN";
		public string formName ="";
		public int iTotal;
		protected EeekSoft.Web.PopupWinAnchor showTest;
		protected EeekSoft.Web.PopupWin popupTest;
		protected EeekSoft.Web.PopupWin popupWin;
		protected System.Web.UI.HtmlControls.HtmlInputHidden scrollLeft;
		protected System.Web.UI.HtmlControls.HtmlInputHidden scrollTop;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtAcountLogin;
		public static DataTable dtData;
		private void Page_Load(object sender, System.EventArgs e)
		{	
			//cangtt 21032006 //alert multilanguage			
			strLanguageID = Session["LangID"] == null?"EN":Session["LangID"].ToString().Trim();				
			txtAcountLogin.Value = Session["AccountLogin"]!=null?Session["AccountLogin"].ToString():"admin";
			formName=Request.Params["Ascx"];				
			iTotal=0;

			dtData=clsCommon.GetDataTable("LS_spfrmALERTMESSAGE @Activity='GetDataByID',@Ascx='" + formName + "'");
			iTotal=dtData.Rows.Count;							
			//end cangtt-------------------------------------------------------
			// Put user code to initialize the page here
			//if(!IsPostBack)
			//{
			//LanHTD: Truong hop goi Function default
			//blnShowForm="1";	
			DataTable tbl;
			if (Request.Params["FunctionDefault"] != null)
			{
				tbl = new DataTable();
				tbl = clsCommon.GetDataTable("sp_clsCommon @Activity = 'GetByFunctionID', @FunctionID = '" + Request.Params["FunctionDefault"].ToString().Trim() + "'");
				if (tbl.Rows.Count > 0)
					Response.Redirect("Editpage.aspx?ModuleID=" + tbl.Rows[0]["ModuleID"].ToString() + "&ParentID="
						+ tbl.Rows[0]["Parent"].ToString() + "&FunctionID=" + tbl.Rows[0]["FunctionID"].ToString()
						+ "&Ascx=" + tbl.Rows[0]["Ascx"].ToString());
			}
			//End

			///---Change language---------						
			string str_FrmName = Request.Params["Ascx"];
			if(Session["LangID"] != null)
				if(Session["LangID"].ToString().Trim() == "EN")	clsChangeLang.ChangePage(str_FrmName, this.Page, "EN");
				else clsChangeLang.ChangePage(str_FrmName, this.Page, "VN");				
			///---End Change language---------	

			string strAscx =Request.Params["Ascx"];
			if(strAscx != null && strAscx != "")
			{
				Control acx = Page.LoadControl(strAscx);
				acxHolder.Controls.Add(acx);
				//Kiem tra quyen tren function
				clsUMS.pstrModuleID = Request.Params["ModuleID"];
				clsUMS.pstrFunctionID = Request.Params["FunctionID"];

				clsUMS.Decentralize(this, acxHolder, strAscx);
				clsHRParameter.getHR_param(strAscx,acxHolder);
				//cangtt set Parameter tren cac control
				if (!Page.IsPostBack)
				{
						clsHRParameter.setChkAd(this,false);
						clsHRParameter.setPermission(acxHolder,strAscx,false);						
						clsHRParameter.LoadPermission(acxHolder,strAscx,false);					
						
				}
				//end
			}
			else
			{
				Response.Redirect("Errorpage.aspx");
			}
			//}
			string strLanguage = Session["LangID"] != null?Session["LangID"].ToString():"VN";
			string strFunctionID ;
			try
			{
				strFunctionID = Request.Params["FunctionID"];
				
				this.txtCurMenu.Value = strFunctionID;
				if (strFunctionID == null)
					strFunctionID = "";
				//Tieu de cua trang
				string strTitle = "";
				//Gan title bang tham so title truyen vao
				if (Request.Params["TitleE"] != null && Request.Params["TitleV"] != null)
				{
					if (strLanguage == "VN")
						strTitle= Request.Params["TitleE"].ToString().ToUpper();
					else
						strTitle= Request.Params["TitleV"].ToString().ToUpper();
				}
					// Nguoc lai lay tu bang function
				else
				{
					tbl = new DataTable();
					tbl = clsCommon.GetDataTable("sp_clsCommon @Activity = 'GetByFunctionID', @FunctionID = '" + strFunctionID + "'");
					if (tbl != null)
					{
						if (tbl.Rows.Count>0)
						{
							if (strLanguage == "VN")
								strTitle= tbl.Rows[0]["ModuleNameV"].ToString().ToUpper() + " - " + tbl.Rows[0]["TitleV"].ToString().ToUpper();
							else
								strTitle= tbl.Rows[0]["ModuleNameE"].ToString().ToUpper() + " - " + tbl.Rows[0]["TitleE"].ToString().ToUpper();
							
							if (tbl.Rows[0]["HaveSetup"].ToString() == "True")
								Bottom_LS1.Visible=true;
							else
								Bottom_LS1.Visible=false;
						}
					}
					tbl.Dispose();
				}
				

				this.lblTitle.Text = strTitle;
			}
			catch(Exception exp)
			{
				string s = exp.Message;
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
			string strExactlyUrl = "";
			if (Request.Params["ExactlyUrl"] != null)
				strExactlyUrl =  Request.Params["ExactlyUrl"].Trim();
			clsUMS.CheckLogined(this, strExactlyUrl);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
