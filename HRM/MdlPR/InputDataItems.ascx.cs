namespace iHRPCore
{
	using System;
	using System.IO;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.Com;
	using iHRPCore.HRComponent;	
	using iHRPCore.TMSComponent; 
	using System.Data.SqlClient;	
	using iHRPCore;
	using GridSort;
	using System.Configuration;
	using iHRPCore.Include;
	using Aspose.Cells;
	/// <summary>
	///		Summary description for InputDataItems.
	/// </summary>
	public class InputDataItems : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label lblErr;
		public string strLanguage = "EN";
		protected System.Web.UI.WebControls.LinkButton btnSearch;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Hidden1;
		protected System.Web.UI.WebControls.TextBox txtMonth;
		protected EmpHeaderSearch EmpHeaderSearch1;
		protected System.Web.UI.WebControls.DataGrid dtgList;
		protected System.Web.UI.WebControls.LinkButton btnImport;
		protected System.Web.UI.HtmlControls.HtmlInputFile txtFile;
		protected System.Web.UI.WebControls.Label Label2;
		clsEmpHeaderSearch EHSearch = new clsEmpHeaderSearch();

		private void Page_Load(object sender, System.EventArgs e)
		{
			strLanguage = Mession.GlbLangID;
			if(!Page.IsPostBack)
			{
				Session["ssAddnew"]="Addnew";
				txtMonth.Text = DateTime.Today.ToString("dd/MM/yyyy").Substring(3,7);
			}
			btnImport.Attributes.Add("OnClick", " return validform()");
			btnSearch.Attributes.Add("OnClick", "return checkdelete()");
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
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		
		
		private void BindUserList()
		{
			EHSearch.empHeaderSearch = EmpHeaderSearch1;
			EHSearch.EmpSearch();
			string strSQL = "[PR_spfrmInputPayrollData] @Activity='SearchInput'";
				strSQL +=",@Month ='"+txtMonth.Text+"', @LSCompanyID = '"+EHSearch.LSCompanyID +"'";
				strSQL +=",@LSLevel1ID ='"+EHSearch.LSLevel1ID+"', @LSLevel2ID = '"+EHSearch.LSLevel2ID+"'";
				strSQL +=",@LSLevel3ID ='"+EHSearch.LSLevel3ID+"'";
			strSQL +=",@EmpCode ='"+EHSearch.EmpID+"'";
			strSQL +=",@EmpName ='"+EHSearch.EmpName+"'";
			strSQL +=",@Status ='"+EHSearch.Status+"'";
			strSQL +=",@UserGroupID ='"+Mession.GlbUser+"'";

			DataTable dt = clsCommon.GetDataTable(strSQL);
			//clsCommon.LoadListBoxControl(lstListUser,dt,"ID","UserGroupName",false);					
		}
		private DataTable getData()
		{
			EHSearch.empHeaderSearch = EmpHeaderSearch1;
			EHSearch.EmpSearch();
			string strSQL = "[PR_spfrmInputPayrollData] @Activity='SearchInput'";
			strSQL +=",@Month ='"+txtMonth.Text+"', @LSCompanyID = '"+EHSearch.LSCompanyID +"'";
			strSQL +=",@LSLevel1ID ='"+EHSearch.LSLevel1ID+"', @LSLevel2ID = '"+EHSearch.LSLevel2ID+"'";
			strSQL +=",@LSLevel3ID ='"+EHSearch.LSLevel3ID+"'";
			strSQL +=",@EmpCode ='"+EHSearch.EmpID+"'";
			strSQL +=",@EmpName ='"+EHSearch.EmpName+"'";
			strSQL +=",@Status ='"+EHSearch.Status+"'";
			strSQL +=",@UserGroupID ='"+Mession.GlbUser+"'";
			DataTable dt = clsCommon.GetDataTable(strSQL);
			return dt;

		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			DataTable dt = getData();
			dtgList.DataSource = dt;
			dtgList.DataBind();
		}

		private void btnImport_Click(object sender, System.EventArgs e)
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
				dataTable= worksheet.Cells.ExportDataTable(0, 0, worksheet.Cells.MaxRow+1,
					worksheet.Cells.MaxColumn+1);	 

				SaveDataTable(dataTable);
				btnSearch_Click(null,null);
				//Bind the DataGrid with DataTable
//				dtgList.DataSource = dataTable;
//				dtgList.ShowHeader = true;
//				dtgList.DataBind();

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
		private void SaveDataTable(DataTable oDataTable)
		{
			string sParam = "";
			string sValue = "";
			int i = 0;
			string sSQL = "";

			foreach(DataRow oDataRow in oDataTable.Rows)	
			{
				if (i == 0 )
				{
					foreach(DataColumn oDataColumn in oDataTable.Columns)	
					{
						sParam += ";" + oDataRow[oDataColumn.ColumnName];
					}
				}
				else
				{
					sValue = "";
					foreach(DataColumn oDataColumn in oDataTable.Columns)	
					{
						sValue += ";" + oDataRow[oDataColumn.ColumnName];
					}
				}

				if (sParam != "" && sValue != "")
				{
					string[] arrParam = sParam.Trim().Split(';');
					string[] arrValue = sValue.Trim().Split(';');
					for (int j = 5; j < arrParam.Length; j++)
					{
						//string sFormula = arrValue[j].ToString();
						string ssValue = arrValue[j].ToString();
						string sMMYYYY = arrValue[4].ToString();
						string sEmpCode = arrValue[1].ToString();
						string sLevel1ShortName = arrValue[3].ToString();
						string sName = arrParam[j].ToString();
						sSQL = "PR_spfrmInputPayrollData @Activity = 'SaveImportParoll',@Month = '"+sMMYYYY
							+ "',@Value='" + ssValue
							+ "',@EmpCode='" + sEmpCode
							+ "',@Level1ShortName='" + sLevel1ShortName
							+ "',@Name='" + sName
							+"'";
//						sSQL = @"Update A Set ";
//					
//						sSQL += " A.Formula = N'"+arrValue[j]+"' , A.Value = N'"+arrValue[j]+"'";
//					
//						sSQL += @"
// from PR_tblPayroll_" + arrValue[4].ToString().Replace("/","") + @" A
// inner join HR_vEmpList E On E.EmpID = A.EmpID
// where E.EmpCode = '" + arrValue[1].ToString() + @"' and E.Level1ShortName = N'"+arrValue[3].ToString()+"' and A.LSSalaryItemDataCode = N'"+arrParam[j]+"'";
						clsCommon.Exc_CommandText(sSQL);
					}
				}
				i++;
				
			}
		}

		private void btnExport_Click(object sender, System.EventArgs e)
		{
			DataTable dt = getData();
			Workbook workbook = new Workbook();
			Worksheet sheet = workbook.Worksheets[0];
			sheet.Cells.ImportDataTable(dt,true,"A1");
			sheet.AutoFitColumns();
			workbook.Save("ExportData.xls",FileFormatType.Default,SaveType.OpenInExcel, this.Response);
		}
	}
}
