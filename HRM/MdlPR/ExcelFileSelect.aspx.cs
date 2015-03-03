using System;
using System.IO;
using System.Configuration;
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
using iHRPCore.PRComponent;

namespace iHRPCore.MdlPR
{
	/// <summary>
	/// Summary description for ExcelFileSelect.
	/// </summary>
	public class ExcelFileSelect : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.LinkButton btnImport;
		protected System.Web.UI.WebControls.LinkButton btnTemplate;
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Panel Panel1;
		protected System.Web.UI.HtmlControls.HtmlInputFile txtFile;
		protected System.Web.UI.HtmlControls.HtmlInputFile inputFile;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				string strID = Request.Params["ID"].ToString().Trim();
				//lblErr.Text = strID;
				btnImport.Attributes.Add("onclick","return CheckFileName('"+txtFile.ClientID+"');");
				btnTemplate.Attributes.Add("onclick","return ViewTemplate();");
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
			this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnImport_Click(object sender, System.EventArgs e)
		{
//			int iRecordAffectd=0;
//			try
//			{
//				lblErr.Text="";				
//				if(inputFile.Value.Length>0)
//					iRecordAffectd+=clsExcelImports.ExcelFileImport(inputFile.Value,"PR_spfrmSALARYGRADE","Save");
//
//				if(iRecordAffectd<0) iRecordAffectd=0;
//				lblErr.Text=iRecordAffectd.ToString()+" records affected.";
//			}
//			catch(Exception ex) {lblErr.Text="Error, "+ex.Message+" "; lblErr.Text+=iRecordAffectd.ToString()+" records affected.";}
			if(Path.GetExtension(this.txtFile.Value).Trim() != ".xls")
			{
				lblErr.Text = "File is not format (.xls)";
				return;
			}
			string strFiletmp="";
			try 
			{
				if (this.txtFile.Value != "")
				{
					strFiletmp = this.txtFile.Value;
					strFiletmp = Path.GetFileName(strFiletmp);
					string a = Server.MapPath("..") + "\\Upload\\" + strFiletmp;
					this.txtFile.PostedFile.SaveAs(Server.MapPath("..") + "\\Upload\\" + strFiletmp);
					System.IO.File.SetAttributes(Server.MapPath("..") + "\\Upload\\" + strFiletmp, System.IO.FileAttributes.Normal) ;
				}
				else
				{
					lblErr.Text = "Please enter the path of filename!";
					return ;
				}
			}
			catch (Exception exp)
			{
				this.lblErr.Text=exp.Message;
				return ;
			}
			//-- end copy file
			try
			{
				//lblErr.Text = clsPRSalaryGrade.ImportSalaryGrade(Server.MapPath("..") + "\\Upload\\" + strFiletmp);
				lblErr.Text = clsCommon.ImpactDB_ImportExcel("@Creater",Session["AccountLogin"],Server.MapPath("..") + "\\Upload\\" + strFiletmp,"SaveImport","PR_spfrmSALARYGRADE");
			}
			catch (Exception exp)
			{
				this.lblErr.Text=exp.Message;
				return ;
			}
		}
	}
}
