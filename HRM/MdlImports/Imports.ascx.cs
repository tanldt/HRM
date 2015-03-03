namespace iHRPCore
{
	using System;
	using System.Configuration;
	using System.Collections;
	using System.IO;
	using System.Web;
	using System.Web.Caching;
	using System.Web.Configuration;
	using System.Data;
	using System.Data.SqlClient;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Web.SessionState;
	using System.Data.OleDb; 
	using HRMCore.Com;
	using iHRPCore.Com;
	using iHRPCore.PRComponent;
	using iHRPCore.Component;
	using Aspose.Cells;


	/// <summary>
	///		Summary description for Imports.
	/// </summary>
	public class Imports : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.LinkButton btnViewGrid;
		protected System.Web.UI.WebControls.LinkButton btnSaveGrid;
		protected System.Web.UI.WebControls.LinkButton btnTemplate;
		protected System.Web.UI.WebControls.DataGrid grdList;
		protected System.Web.UI.WebControls.LinkButton btnImport;
		protected System.Web.UI.HtmlControls.HtmlInputFile txtFile;

		public string strDataColumn = "";
		public string strTemplate = "";
		public string strStore = "";
		public string strAct = "";
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.RadioButtonList optTypeImport;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.HtmlControls.HtmlTableRow trTypeImport;		
		public string tabid = "";

		private void Page_Load(object sender, System.EventArgs e)
		{
//			// Put user code to initialize the page here
//			if (Request.Params["tpl"] != null)
//				strTemplate = Request.Params["tpl"].ToString();
//			//strTemplate = "AssignWorkingTour_FileSelect.xls";
//			if (Request.Params["Store"] != null)
//				strStore = Request.Params["Store"].ToString();
//			if (Request.Params["Act"] != null)
//				strAct = Request.Params["Act"].ToString();
			if (Request.Params["tabid"] != null)
				tabid = clsCommon.SafeDataInteger(Request.Params["tabid"]).ToString();
			

			if(!IsPostBack)
			{
				DataRow dr = clsCommon.GetDataRow("[IMP_clsConfig] @Activity = 'GetConfigID',@CodeID = " + tabid);
				if (dr != null)
				{
					strTemplate = "TemplateExcel/"+dr["fileTemplate"].ToString();
					strStore = dr["Stored"].ToString();
					strAct = dr["Activity"].ToString();
				}
				

				if (Request.Params["tabid"].ToString() == "4") 
				{
					optTypeImport.Items.Clear();
					ListAssistant Lst = new ListAssistant();
					Lst.PopulateList(optTypeImport, "Text", "Value", GetDataSource());
					optTypeImport.SelectedValue = "Insert";
				}
				else if (Request.Params["tabid"].ToString() == "5") 
				{
					optTypeImport.Items.Clear();
					//trTypeImport.Style.Clear();
					trTypeImport.Attributes.Add("style","display:none");	
				}

				btnViewGrid.Attributes.Add("onclick","return CheckFileName('"+txtFile.ClientID+"');");
				btnImport.Attributes.Add("onclick","return CheckFileName('"+txtFile.ClientID+"');");
				btnTemplate.Attributes.Add("onclick","return ViewTemplate();");
			}
			btnSaveGrid.Attributes.Add("OnClick","return CheckSave();");
		}
		protected static DataTable GetDataSource()  
		{  
			
			DataTable table = new DataTable();  
			table.Columns.Add("Text", System.Type.GetType("System.String"));  
			table.Columns.Add("Value", System.Type.GetType("System.String"));  

			DataRow row = table.NewRow();  
			row["Text"] = "Insert";  
			row["Value"] = "Insert";  
			table.Rows.Add(row);  

			return table;  
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
			this.btnViewGrid.Click += new System.EventHandler(this.btnViewGrid_Click);
			this.btnSaveGrid.Click += new System.EventHandler(this.btnSaveGrid_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnViewGrid_Click(object sender, System.EventArgs e)
		{
			if(Path.GetExtension(this.txtFile.Value).Trim() != ".xls")
			{
				lblErr.Text = string.Format("This file is {0}, not file Excel",this.txtFile.Value);
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
					sFile = sServerPath + "\\Upload\\" + strFiletmp;
					this.txtFile.PostedFile.SaveAs(sFile);
					System.IO.File.SetAttributes(sFile, System.IO.FileAttributes.Normal) ;
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
				string strErrorID = "";
				//tam thoi bo cai cu
				//strErrorID = clsCommon.ImportExcelTo_DataGrid(grdList,sFile);
				// Get Datatable
				DataTable dt = new DataTable();
				try
				{
					dt = GetDataFromExcel(sFile);
				}
				catch (Exception exp)
				{
					this.lblErr.Text=exp.Message;
					return ;
				}
				strErrorID = clsCommon.ImportExcelTo_DataGrid(grdList,dt);

				if (strErrorID != "Successful!")
				{
					lblErr.Text = strErrorID;
				}
				else
					lblErr.Text = "Download complete!";//GetText("FILESELECT","TAISUCCESSFUL");
			}
			catch (Exception exp)
			{
				this.lblErr.Text=exp.Message;
				this.lblErr.Text=  "Please select file excel!";//GetText("FILESELECT","ERRORVIEWFILE");
				return ;
			}
		}
		private DataTable GetDataFromExcel(string sFile)
		{
			string strErrorID = "";

			//strErrorID = clsCommon.ImportExcelTo_DataGrid(grdList,sFile);
			Workbook workbook = new Workbook();	
			//Open template
			//				string path = MapPath("~");
			//				path = path.Substring(0, path.LastIndexOf("\\"));
			//				path += @"\designer\book1.xls";			
			workbook.Open(sFile);	
	
			Worksheet worksheet = workbook.Worksheets[0];
			DataTable dataTable = new DataTable();
			//Export worksheet data to a DataTable object by calling either ExportDataTable or ExportDataTableAsString method of the Cells class		 	
			dataTable= worksheet.Cells.ExportDataTable(1, 0, worksheet.Cells.MaxRow+1,
				worksheet.Cells.MaxColumn+1);	 

			return dataTable;
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
			// delete old message
			if (grdList.Items.Count > 0)
			{
				for (int c = 0; c < grdList.Items.Count; c++)
				{
					grdList.Columns[1].Visible = true;
					clsCommon.ShowMessageToDataGrid(c.ToString()+ "@$","label","lblErrorDtg",grdList);	
				}
			}

			lblErr.Text = "";
			DataRow dr = clsCommon.GetDataRow("[IMP_clsConfig] @Activity = 'GetConfigID',@CodeID = " + tabid);
			if (dr != null)
			{
				strTemplate = "TemplateExcel/"+dr["fileTemplate"].ToString();
				strStore = dr["Stored"].ToString();
				//strAct = dr["Activity"].ToString();
				strAct = optTypeImport.SelectedValue.Trim();
			}

			try
			{	if (grdList.Items.Count < 0)
					return;
				string strErrorID = "";
				string strID="";
				Table Tbl = (Table)grdList.Controls[0];
				int j = Tbl.Rows[0].Cells.Count;
				for(int i=0;i<j;i++)
				{
					strID += Tbl.Rows[0].Cells[i].Text.Trim() + "$";
				}				

				strErrorID = clsImports.ImpactDB_DataGrid("@Creater",Session["AccountLogin"],grdList,"chkSelect",strAct,strStore,strID,tabid);
				if (strErrorID == "Successful!")
					lblErr.Text = "Successful!";
				else
				{
					grdList.Columns[1].Visible = true;
					clsCommon.ShowMessageToDataGrid(strErrorID,"tanldt_grid","lblErrorDtg",grdList);	
				}
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
				lblErr.Text = "Please select file Excel and view grid before click save!";//GetText("FILESELECT","ERRORSAVEFILE");
			}
		}
	}
}
