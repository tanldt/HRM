using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.IO;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Configuration;
using iHRPCore.Com;

namespace iHRPCore.Include
{
	/// <summary>
	/// Summary description for WebForm1.
	/// </summary>
	public class WebForm1 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label lblCompany;
		protected System.Web.UI.WebControls.TextBox txtURL;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Button butDowloadFile;
		protected System.Web.UI.WebControls.Button butUndo;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.CheckBox CheckBox2;
		protected System.Web.UI.WebControls.LinkButton btnUpload;
		protected System.Web.UI.WebControls.CheckBox CheckBox1;
		protected System.Web.UI.WebControls.TextBox txtDesc;
		protected System.Web.UI.WebControls.Button butSQL;
		protected System.Web.UI.WebControls.TextBox txtSQL;
		protected System.Web.UI.WebControls.Button btnExport;
		protected System.Web.UI.WebControls.DataGrid dtgGrid;
		protected System.Web.UI.HtmlControls.HtmlInputFile txtFile;
		protected System.Web.UI.HtmlControls.HtmlTableRow trGrid;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.HtmlControls.HtmlTable tblLogin;
		protected System.Web.UI.WebControls.TextBox txtUserName;
		protected System.Web.UI.HtmlControls.HtmlInputText txtPassword;
		protected System.Web.UI.WebControls.Button btnLogin;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label lblstrSQL;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			lblstrSQL.Text = ConfigurationSettings.AppSettings["pstrConnectionString"];
			if (Session["WebFrom_login"]==null)
			{
			}
			else
			{
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
			this.butDowloadFile.Click += new System.EventHandler(this.butDowloadFile_Click);
			this.butUndo.Click += new System.EventHandler(this.butUndo_Click);
			this.CheckBox1.CheckedChanged += new System.EventHandler(this.CheckBox1_CheckedChanged);
			this.butSQL.Click += new System.EventHandler(this.butSQL_Click);
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnUpload_Click(object sender, System.EventArgs e)
		{
			//---copy file to server			
			try 
			{
				if (this.txtFile.Value != "")
				{
					//Backup file
					string strSource = Server.MapPath("../") + txtURL.Text;

					string strDesc = Server.MapPath("../") + txtURL.Text.Substring(0,txtURL.Text.Length-4) + "_Backup.xls";
					
					if(CheckBox1.Checked)
						strDesc = Server.MapPath("../") + txtDesc.Text;

					if (CheckBox2.Checked) File.Copy(strSource,strDesc , true);

					if(File.Exists(strSource))
					{
						System.IO.File.SetAttributes(strSource, System.IO.FileAttributes.Normal);
					}

					this.txtFile.PostedFile.SaveAs(strSource);
					//File.SetAttributes(desName, FileAttributes.Normal);
					//System.IO.File.SetAttributes(Server.MapPath("../") + txtURL.Text.Trim() + strFiletmp, System.IO.FileAttributes.Normal);
				}
				else
				{
					lblErr.Text = "Please enter the path of filename!";
					return ;
				}	
				lblErr.Text = "Upload file sucessful!";
			}
			catch (Exception exp)
			{
				this.lblErr.Text=exp.Message;
				return ;
			}
		}

		//'open popup window on server-side
		public static void OpenFile(System.Web.UI.Page pPage,string pURL)
		{
			string strFileName;
			string strServerName = pPage.Request.ServerVariables["SERVER_NAME"];
			string strInvalid = pPage.Server.MapPath(".");
			
			strFileName = pPage.Request.Url.AbsoluteUri;
			strFileName = strFileName.Substring(0,strFileName.LastIndexOf("/"));
			strFileName = pURL.Replace(strInvalid, strFileName );
			strFileName = strFileName.Replace(@"\","/");

			string strScript = "<script language=JavaScript>";				
			strScript += "var win=window.open('" + strFileName + "','Recipient','status=1,toolbar=0, menubar=1,scrollbars=1,resizable=1,alwaysRaised=Yes, top=10, left=10, width=' + (screen.width - 50)  + ', height=' + (screen.height - 50) + ',1 ,align=center');";
			//strScript += " win.close(); win = null; </script>";
			strScript += " </script>";
			if (!pPage.IsStartupScriptRegistered("clientScript"))
				pPage.RegisterStartupScript("clientScript", strScript);			
		}

		private void butDowloadFile_Click(object sender, System.EventArgs e)
		{
			OpenFile(this.Page, Server.MapPath("../") + txtURL.Text);
		}

		private void butUndo_Click(object sender, System.EventArgs e)
		{
			//---copy file to server			
			try 
			{
				if (this.txtURL.Text != "")
				{
					//Backup file
					string strSource = Server.MapPath("../") + txtURL.Text.Substring(0,txtURL.Text.Length-4) + "_Backup.xls";

					if(CheckBox1.Checked)
						strSource = Server.MapPath("../") + txtDesc.Text;

					string strDesc = Server.MapPath("../") + txtURL.Text;

					File.Copy(strSource,strDesc , true);
				}
				else
				{
					lblErr.Text = "Please enter the path of filename!";
					return ;
				}			
				lblErr.Text = "Undo sucessful!";
			}
			catch (Exception exp)
			{
				this.lblErr.Text=exp.Message;
				return ;
			}
		
		}

		private void CheckBox1_CheckedChanged(object sender, System.EventArgs e)
		{

			if(CheckBox1.Checked)
				txtDesc.Visible=true;
			else
				txtDesc.Visible=false;
		}		

		private void butSQL_Click(object sender, System.EventArgs e)
		{
			SqlCommand  cmd = new SqlCommand();
			string strConn =ConfigurationSettings.AppSettings["pstrConnectionString"];
			SqlConnection SQLconn= new SqlConnection(strConn);
			DataTable dtb = new DataTable();
			lblErr.Text="";
			try
			{
				SQLconn.Open();
				cmd.Connection = SQLconn;
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = txtSQL.Text;

				cmd.ExecuteNonQuery();											

				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();	

				lblErr.Text="Run SQL script successful!";
				BindDataGrid();

			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
			finally
			{
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				dtb.Dispose();
			}
		
		}

		private void BindDataGrid()
		{
			DataTable dtb = new DataTable();
			//LanHTD: Temp: Ket du lieu vao grid
			dtb = clsCommon.GetDataTable(txtSQL.Text.Trim());
			dtgGrid.DataSource = dtb;
			dtgGrid.DataBind();
			dtb.Dispose();
			//
		}

		private void btnExport_Click(object sender, System.EventArgs e)
		{
			this.dtgGrid.AllowPaging = false;
			BindDataGrid();
			iHRPCore.DataGridExcelExporter myExcelXport=new DataGridExcelExporter(dtgGrid);			
			myExcelXport.Export();	
			this.dtgGrid.AllowPaging = true;
			BindDataGrid();
		}

		private void dtgGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			BindDataGrid();
			this.dtgGrid.CurrentPageIndex = e.NewPageIndex;
			this.dtgGrid.DataBind();
		}

		private void btnLogin_Click(object sender, System.EventArgs e)
		{
			if (txtUserName.Text=="iHRP" && txtPassword.Value=="FPT.iHRP")
			{
				Session["WebFrom_login"]="1";
			}
		}
	}
}
