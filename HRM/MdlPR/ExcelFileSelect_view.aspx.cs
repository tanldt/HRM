using System;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Web.Caching;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.OleDb; 
using iHRPCore.Com;
using iHRPCore.PRComponent;

namespace iHRPCore.MdlPR
{
	/// <summary>
	/// Summary description for ExcelFileSelect_view.
	/// </summary>
	public class ExcelFileSelect_view : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.LinkButton btnImport;
		protected System.Web.UI.WebControls.LinkButton btnTemplate;
		protected System.Web.UI.WebControls.Panel Panel1;
		protected System.Web.UI.HtmlControls.HtmlInputFile txtFile;
		protected System.Web.UI.WebControls.LinkButton btnViewGrid;
		protected System.Web.UI.HtmlControls.HtmlInputFile inputFile;
		protected System.Web.UI.WebControls.LinkButton btnSaveGrid;
		protected System.Web.UI.WebControls.DataGrid grdList;
		protected System.Web.UI.WebControls.Label lblErrorDtg;
		protected System.Web.UI.WebControls.Label lblErr;
		public string strDataColumn = "";
		public string strID = "";
		public string strTemplate = "";
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			strID = Request.Params["ID"].ToString().Trim();
			if (strID == "1")
				strTemplate = "SalaryGradeImportTemplate_CB.xls";
			else
				strTemplate = "SalaryGradeImportTemplate_CD.xls";

			if(!IsPostBack)
			{
				btnViewGrid.Attributes.Add("onclick","return CheckFileName('"+txtFile.ClientID+"');");
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
			this.btnViewGrid.Click += new System.EventHandler(this.btnViewGrid_Click);
			this.btnSaveGrid.Click += new System.EventHandler(this.btnSaveGrid_Click);
			this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnImport_Click(object sender, System.EventArgs e)
		{
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

		private void btnViewGrid_Click(object sender, System.EventArgs e)
		{
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
				//lblErr.Text = Import(Server.MapPath("..") + "\\Upload\\" + strFiletmp);
				lblErr.Text = clsCommon.ImportExcelTo_DataGrid(grdList,Server.MapPath("..") + "\\Upload\\" + strFiletmp);
				//lblErr.Text = clsCommon.ImpactDB_ImportExcel("@Creater",Session["AccountLogin"],Server.MapPath("..") + "\\Upload\\" + strFiletmp,"SaveImport","PR_spfrmSALARYGRADE");
			}
			catch (Exception exp)
			{
				this.lblErr.Text=exp.Message;
				return ;
			}
		}
		#region Import
		public string Import(string pstrFilename)
		{
			string mstr_FileName = pstrFilename;
			
			if (!File.Exists(pstrFilename))
			{
				return "File not found, Please check path of the filename again!";
			}
			string mstr_PathFileName = mstr_FileName;
			//------------------

			string strConn;
			strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + mstr_PathFileName + ";Extended Properties=Excel 8.0;";			
			OleDbDataAdapter myCommand = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", strConn);
			DataSet myDataSet = new DataSet();
			myCommand.Fill(myDataSet, "ExcelData");

			DataTable dt = new DataTable();
			DataRow dr;

			int iCom = myDataSet.Tables["ExcelData"].Columns.Count;

			try
			{
				for(int j=0;j<iCom;j++)
				{
					strDataColumn = myDataSet.Tables["ExcelData"].Rows[0][j].ToString().Trim();
					//strDataCol = myDataSet.Tables["ExcelData"].Rows[0][j].ToString().Trim();
					dt.Columns.Add(new DataColumn(strDataColumn, typeof(string)));
				}
				for(int i=1;i<myDataSet.Tables["ExcelData"].Rows.Count;i++)
				{
					dr = dt.NewRow();
					for(int j=0;j<iCom;j++)
					{
						string strValue = myDataSet.Tables["ExcelData"].Rows[i][j].ToString().Trim();

						dr[j] = strValue;
					}
					dt.Rows.Add(dr);
				}
				DataView dv = new DataView(dt);
				grdList.DataSource= dv;
				grdList.DataBind();

				myDataSet.Dispose();
				return "Successful!";
			}
			catch(Exception exp)
			{
				myDataSet.Dispose();
				return exp.Message;
			}
		}

		#endregion
		private void btnSaveGrid_Click(object sender, System.EventArgs e)
		{
			lblErr.Text = "";
			try
			{
				if (grdList.Items.Count < 0)
					return;
				string strErrorID = "";
				string strID="";
				Table Tbl = (Table)grdList.Controls[0];
				int j = Tbl.Rows[0].Cells.Count;
				for(int i=0;i<j;i++)
				{
					strID += Tbl.Rows[0].Cells[i].Text.Trim() + "$";
				}
				strErrorID = clsCommon.ImpactDB_DataGrid("@Creater",Session["AccountLogin"],grdList,"chkSelect","SaveImport","PR_spfrmSALARYGRADE",strID);
				if (strErrorID != "Successful!")
				{
					grdList.Columns[1].Visible = true;
					clsCommon.ShowMessageToDataGrid(strErrorID,"tanldt_grid","lblErrorDtg",grdList);
//					string[] arrID = strErrorID.Trim().Split('$');
//					for(int k=0;k<arrID.Length-1;k++)
//					{
//						string strErr = arrID.GetValue(k).ToString().Trim();
//						string[] arrIDs = strErr.Trim().Split('@');
//					
//						string strIDErr = arrIDs.GetValue(0).ToString().Trim();
//						string strMessageErr = arrIDs.GetValue(1).ToString().Trim();
//						grdList.Items[Convert.ToInt16(strIDErr)].CssClass = "tanldt_grid";
//						((Label)grdList.Items[Convert.ToInt16(strIDErr)].FindControl("lblErrorDtg")).Text = strMessageErr;
//					}
				}
				else
                    lblErr.Text = strErrorID;
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}
	}
}
