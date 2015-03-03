namespace MdlPR
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using FPTToolWeb.Control.DataGrids;
	using System.Text;
	using iHRPCore.Include;
	using iHRPCore;

	/// <summary>
	///		Summary description for Export.
	/// </summary>
	public class Export : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.DropDownList cboOrderby;
		protected System.Web.UI.WebControls.DropDownList cboSort;
		protected System.Web.UI.WebControls.Button cmdView;
		protected System.Web.UI.WebControls.DropDownList cboFormula;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList cboNameExport;
		protected System.Web.UI.WebControls.TextBox txtMMYYYY;
		protected System.Web.UI.WebControls.Label Label1;
		protected EmpHeaderSearch EmpHeaderSearch1;
		clsEmpHeaderSearch EHSearch = new clsEmpHeaderSearch();

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if (!this.IsPostBack)
			{
				LoadCombo();
				this.txtMMYYYY.Text=System.DateTime.Now.ToString("MM/yyyy");
			}
			cmdView.Attributes.Add("onclick", "return checkvalidSearch()");
		}
		private void LoadCombo()
		{
			DataTable dt = new DataTable();
			dt = clsDB.GetDataTableHasID("PR_spfrmExportCustom @Activity = 'GetSalaryItemDataAdd'");
			clsDB.LoadDropDownListControl(cboOrderby,dt,"Code","Code",true);
			clsDB.LoadDropDownListControl(cboFormula, "sp_GetDataCombo @TableName='PR_tblSetFormula',@Fields='SetFormulaID as [ID],NameFormula as Name'", "ID", "Name",true);
			clsDB.LoadDropDownListControl(cboNameExport, "sp_GetDataCombo @TableName='PR_tblExportCustom',@Fields='ExportCustomID as [ID],NameExport as Name'", "ID", "Name",true);
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
			this.cmdView.Click += new System.EventHandler(this.cmdView_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void cmdView_Click(object sender, System.EventArgs e)
		{
			DataTable dt = new DataTable();
			DataTable tblMaster = new DataTable();
			EHSearch.empHeaderSearch = EmpHeaderSearch1;
			EHSearch.EmpSearch();
			string strEmpID = EHSearch.EmpID;
			string strEmpName = EHSearch.EmpName;
			string strLevel1 = EHSearch.LSLevel1ID;
			string strLevel2 = EHSearch.LSLevel2ID;
			string strLevel3 = EHSearch.LSLevel3ID;
			string strLocation = EHSearch.LSLocationID;
			string strJobCode = EHSearch.LSJobcodeID;
			string strPosition = EHSearch.LSEmpTypeID;
			string strCompany = EHSearch.LSCompanyID;
			string strStatus = EHSearch.Status;
			string sEmpTypeID = EHSearch.LSEmpTypeID;
			string sFormula = cboFormula.SelectedValue;

			string sMonth = txtMMYYYY.Text;
			dt = clsPayroll.GetDataSalaryExport(sMonth,strCompany, strLevel1,strLevel2, strLevel3
				,strEmpID, strEmpName, Mession.GlbLangID,Mession.GlbUser,sFormula);
			//Mession.GlbSearchData = dt;
			tblMaster = clsPayroll.GetCustomExport(cboNameExport.SelectedValue);
			string sPage = GenList(dt,tblMaster);
			ExcelExporter myExcelXport=new ExcelExporter(this.Page);
			myExcelXport.ExportHTMLToExcel(sPage,"Excel");
			myExcelXport = null;
		}
		private string GenList(DataTable dtPayroll, DataTable dtColunm)
		{
			string sReturn = "";
			string boldTagStart = "";
			string boldTagEnd = "";
			boldTagStart = "<B>";
			boldTagEnd = "</B>";
			string sHeader = "";
			string sRow = "";
			//StringBuilder sbH =new StringBuilder();
			
			sHeader += ("<TR>");
			sRow += ("<TR>");

			if(dtColunm.Rows.Count > 0)
			{
				for (int i=0; i<dtColunm.Rows.Count; i++)
				{
					sHeader += ("<TD bgcolor=#D6D7E1>" + boldTagStart + dtColunm.Rows[i]["CustomName"].ToString() + boldTagEnd + "</TD>");
					if (dtColunm.Rows[i]["ViewStyleValue"].ToString() == "1")
                        sRow += (@"<TD>$" + dtColunm.Rows[i]["Code"].ToString() + "$</TD>");
					else
						sRow += (@"<TD style='mso-number-format:\#\,\#\#0\' >$" + dtColunm.Rows[i]["Code"].ToString() + "$</TD>");
				}
			}

			sHeader += ("</TR>");

			sRow += ("</TR>");
			sReturn += "<table border=\"1\">";
			sReturn += sHeader;
			#region Creating rows
			/*******************************************************************
			 * Start, Creating rows
			 * *****************************************************************/
			sReturn += HTMLDataTable(dtPayroll,sRow);

			/*******************************************************************
			 * End, Creating rows
			 * *****************************************************************/
			#endregion
			sReturn += "</table>";
			return sReturn;
		}
		#region HTMLDataTable oDataTable
		public string HTMLDataTable(DataTable oDataTable, string strTpl)
		{
			string strHTML = "";
			StringBuilder sb =new StringBuilder();
			int i = 1;
			foreach(DataRow oDataRow in oDataTable.Rows)	
			{
				strHTML = strTpl;
//				try
//				{
//					strHTML =  strHTML.Replace("$No$", i.ToString());
//				}
//				catch{ strHTML = strHTML;}

				foreach(DataColumn oDataColumn in oDataTable.Columns)	
				{
					try
					{
						strHTML =  strHTML.Replace("$"+oDataColumn.ColumnName+"$", oDataRow[oDataColumn.ColumnName].ToString());
					}
					catch{ strHTML =  strHTML ;}
				}

				i++;
				sb = sb.Append(strHTML);
			}
			return sb.ToString();
		}
		#endregion
	}
}
