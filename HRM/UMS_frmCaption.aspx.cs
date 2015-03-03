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

namespace iHRPCore
{
	/// <summary>
	/// Summary description for UMS_frmCaption.
	/// </summary>
	public class UMS_frmCaption : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden scrollLeft;
		protected System.Web.UI.HtmlControls.HtmlInputHidden scrollTop;
		protected System.Web.UI.WebControls.DataGrid dtgDish;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(!Page.IsPostBack)
			{
				if(Request.Params["ID"]!=null)
				Load_dtg(Request.Params["ID"]);
				LinkButton1.Text=Request.Params["ID"]+".aspx";

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
			this.LinkButton1.Click += new System.EventHandler(this.LinkButton1_Click);
			this.dtgDish.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgDish_PageIndexChanged);
			this.dtgDish.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgDish_CancelCommand);
			this.dtgDish.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgDish_EditCommand);
			this.dtgDish.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgDish_UpdateCommand);
			this.dtgDish.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgDish_DeleteCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		private void Load_dtg(string strID)
		{
			try
			{
				//load all to dtgDish
				if (strID!="")
				{
					this.dtgDish.DataSource =clsCommon.GetDataTable("SELECT * FROM UMS_tblCaptionFrmCtl WHERE FormID='"+ strID +"' ORDER BY ControlID,ColumnIndex");
					this.dtgDish.DataBind();
					
				}
				
			}		
			catch
			{}
		}
		private void dtgDish_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			this.dtgDish.EditItemIndex = -1;
			this.Load_dtg(Request.Params["ID"]);
		}

		private void dtgDish_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			this.dtgDish.EditItemIndex = e.Item.ItemIndex;
			this.Load_dtg(Request.Params["ID"]);
		}

		private void dtgDish_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string str_ID = this.dtgDish.DataKeys[e.Item.ItemIndex].ToString();
			string str_capVN = ((TextBox)this.dtgDish.Items[e.Item.ItemIndex].FindControl("txtCapVN")).Text;
			string str_capEN = ((TextBox)this.dtgDish.Items[e.Item.ItemIndex].FindControl("txtCapEN")).Text;
			string sql = "Exec UMS_spfrmCaptionFrmCtl 'Update',N'"+str_capVN+"',N'" + str_capEN + "',"+str_ID;
		
			clsCommon.GetDataTable(sql);
			dtgDish.EditItemIndex = -1;
			Load_dtg(Request.Params["ID"]);
        
		}

		private void dtgDish_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.dtgDish.CurrentPageIndex = e.NewPageIndex;	
			this.Load_dtg(Request.Params["ID"]);
		}

		private void LinkButton1_Click(object sender, System.EventArgs e)
		{
//			string str_back;
//
//			if (Session["Link_URL"] != null)
//				str_back = Session["Link_URL"].ToString().Trim() + ".aspx";
//			else
//				str_back =Request.Params["ID"]+".aspx";
//
//			Response.Redirect(str_back);
//			Session["Link_URL"] = "";
//
			
//			str_back=Request.Params["ID"].ToString()+".aspx";
			
			Response.Redirect("Editpage.aspx?" + Session["Link_URL"]);
		}

		private void dtgDish_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string str_ID = this.dtgDish.DataKeys[e.Item.ItemIndex].ToString();
			string sql = "Exec UMS_spfrmCaptionFrmCtl @para='Delete', @autoID = " + str_ID;
		
			clsCommon.GetDataTable(sql);
			dtgDish.EditItemIndex = -1;
			Load_dtg(Request.Params["ID"]);
		}					
	}
}
