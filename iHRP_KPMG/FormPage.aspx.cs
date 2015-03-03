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


namespace iHRPCore
{
	/// <summary>
	/// Summary description for FormPage1.
	/// </summary>
	public class FormPage1 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.WebControls.PlaceHolder acxHolder;
		public string blnShowForm = "";
		public string strLanguageID="VN";
		public string formName ="";
		public int iTotal;
		protected EeekSoft.Web.PopupWinAnchor showTest;
		protected EeekSoft.Web.PopupWin popupWin;
		protected EeekSoft.Web.PopupWin popupTest;
		public static DataTable dtData;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			//cangtt 21032006 //alert multilanguage			
			strLanguageID = Session["LangID"] == null?"EN":Session["LangID"].ToString().Trim();				
			formName=Request.Params["Ascx"];				
			iTotal=0;

			dtData=clsCommon.GetDataTable("LS_spfrmALERTMESSAGE @Activity='GetDataByID',@Ascx='" + formName + "'");
			iTotal=dtData.Rows.Count;							
			//end cangtt-------------------------------------------------------
			// Put user code to initialize the page here
			//if(!IsPostBack)
			//{
			//LanHTD: Truong hop goi Function default
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
			string strAscx =Request.Params["Ascx"];
			if(strAscx != null && strAscx != "")
			{
				Control acx = Page.LoadControl(strAscx);
				acxHolder.Controls.Add(acx);
				//Kiem tra quyen tren function
				clsUMS.pstrModuleID = Request.Params["ModuleID"];
				clsUMS.DecentralizePopUp(this, acxHolder, strAscx);
				//end
			}
			//}
			string strLanguage = "EN";
			string strFunctionID ;
			try
			{
				strFunctionID = Request.Params["FunctionID"];
				if (strFunctionID == null)
					strFunctionID = "";
				//Tiêu đề của trang
				string strTitle = "";
				//Nếu muốn thể hiện title khác với cách lấy title thông thường dựa vào title tự truyền vào
				if (Request.Params["TitleE"] != null && Request.Params["TitleV"] != null)
				{
					if (strLanguage == "VN")
						strTitle= Request.Params["TitleE"].ToString().ToUpper();
					else
						strTitle= Request.Params["TitleV"].ToString().ToUpper();
				}
					// ngựơc lại lấy title từ bảng function
				else
				{
					tbl = new DataTable();
					tbl = clsCommon.GetDataTable("sp_clsCommon @Activity = 'GetByFunctionID', @FunctionID = '" + strFunctionID + "'");
					if (tbl != null)
					{
						if (tbl.Rows.Count>0)
						{
							if (strLanguage == "VN")
								strTitle= tbl.Rows[0]["TitleV"].ToString().ToUpper();
							else
								strTitle= tbl.Rows[0]["TitleE"].ToString().ToUpper();
						}
					}
					tbl.Dispose();
				}
				this.lblTitle.Text = strTitle;
				///---Change language---------						
				string str_FrmName = Request.Params["Ascx"];
				if(Session["LangID"] != null)
					if(Session["LangID"].ToString().Trim() == "EN")	clsChangeLang.ChangePage(str_FrmName, this.Page, "EN");
					else clsChangeLang.ChangePage(str_FrmName, this.Page, "VN");				
				///---End Change language---------	
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
