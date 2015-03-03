namespace MdlPR
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.IO;

	using HRMCore.Com;
	using iHRPCore.Com;
	using iHRPCore.Component;

	/// <summary>
	///		Summary description for UploadTempPayslip.
	/// </summary>
	public class UploadTempPayslip : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Button cmdDelete;
		protected System.Web.UI.WebControls.DataGrid dtgList;
		protected System.Web.UI.WebControls.Button cmdUpload;
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.HtmlControls.HtmlInputFile txtFile;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(!IsPostBack)
			{
				LoadDataGrid();

			}

			cmdUpload.Attributes.Add("onclick","return CheckFileName('"+txtFile.ClientID+"');");
			cmdDelete.Attributes.Add("OnClick", "return checkdelete()");
		}
		private void LoadDataGrid()
		{
			DataTable dtList = new DataTable();
			try
			{
				dtList = clsCommon.GetDataTable("[PR_spfrmReportFileTemplate] @Activity = 'GetDataAll', @Reporttype = 'PaySlip'");
				dtgList.DataSource = dtList;
				dtgList.CurrentPageIndex = 0;
				dtgList.DataBind();
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
			finally
			{
				dtList.Dispose();
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
			this.cmdUpload.Click += new System.EventHandler(this.cmdUpload_Click);
			this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
			this.dtgList.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgList_ItemCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void cmdUpload_Click(object sender, System.EventArgs e)
		{
			lblErr.Text = "";
			if(Path.GetExtension(this.txtFile.Value).Trim() != ".htm")
			{
				lblErr.Text = string.Format("This file is {0}, not file word (type Web Page",this.txtFile.Value);
				return;
			}
			string strFiletmp="";
			string sFile = "";
			try 
			{
				if (this.txtFile.Value != "")
				{
					strFiletmp = this.txtFile.Value;
					strFiletmp = Path.GetFileName(strFiletmp);
					string sServerPath = Request.PhysicalApplicationPath;
					sFile = sServerPath + "\\Upload\\TemplateReport\\PIT\\Payslip\\" + strFiletmp;
					this.txtFile.PostedFile.SaveAs(sFile);
					System.IO.File.SetAttributes(sFile, System.IO.FileAttributes.Normal) ;

					// Save file xuong DB
					string sSQL = "[PR_spfrmReportFileTemplate] @Activity = 'Save', @Reporttype = 'PaySlip',@FileName = '"+strFiletmp+"'";
					clsCommon.Exc_CommandText(sSQL);

					LoadDataGrid();
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
		}

		private void dtgList_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{

		}

		private void cmdDelete_Click(object sender, System.EventArgs e)
		{
			bool a;
			try
			{
				lblErr.Text = "";
				string strID="";
				for(int i=0;i<dtgList.Items.Count;i++)
				{
					if(((CheckBox)dtgList.Items[i].FindControl("chkSelect")).Checked==true)
					{
						string ID = dtgList.Items[i].Cells[0].Text.Trim();
						string sFile = dtgList.Items[i].Cells[1].Text.Trim();

						try 
						{
							string s = MapPath(".") + "\\..\\Upload\\TemplateReport\\PIT\\Payslip\\" + sFile;
							FileInfo TheFile = new FileInfo(s);
							if (TheFile.Exists) 
							{
								
								File.Delete(s);

								string sSQL = "[PR_spfrmReportFileTemplate] @Activity = 'Delete',@ID = "+ID;
								clsCommon.Exc_CommandText(sSQL);
							}
							else 
							{
								throw new FileNotFoundException();
							}
						}

						catch (FileNotFoundException ex) 
						{
							lblErr.Text += ex.Message;
						}
						catch (Exception ex) 
						{
							lblErr.Text += ex.Message;
						}
					}
				}
				LoadDataGrid();
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}

	}
}
