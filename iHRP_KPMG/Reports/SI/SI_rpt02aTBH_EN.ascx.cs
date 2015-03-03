namespace Reports.HR
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using FPTToolWeb.Control.DataGrids;
	using iHRPCore.Include;
	using iHRPCore.SIComponent;
	using iHRPCore.Com;

	/// <summary>
	///		Summary description for SI_rpt02aTBH.
	/// </summary>
	public class SI_rpt02aTBH_EN : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.TextBox txtDate;
		protected System.Web.UI.WebControls.LinkButton btnSearch;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtTitle;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtPrintDate;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList cboLocation;
		protected System.Web.UI.WebControls.Label Label16;
		protected System.Web.UI.WebControls.TextBox txtFromDate;
		protected System.Web.UI.WebControls.Label Label17;
		protected System.Web.UI.WebControls.TextBox txtToDate;
		protected System.Web.UI.WebControls.Label Label4;
		protected EmpHeaderSearchReport EmpHeaderSearchReport1;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			
			if (!Page.IsPostBack)
			{
				//BindDataGrid();
				this.txtDate.Text="11/" + System.DateTime.Now.ToString("yyyy");
				this.txtPrintDate.Text=System.DateTime.Now.ToString("dd/MM/yyyy");
				txtFromDate.Text = "01/"+ DateTime.Today.ToString("MM/yyyy");
				txtToDate.Text = DateTime.Today.AddMonths(1).AddDays(-1 - DateTime.Today.Day).ToString("dd/MM/yyyy");
				LoadComboBox();
			}
			btnSearch.Attributes.Add("onclick", "return checkvalidSearch()");
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void LoadComboBox()
		{
			clsCommon.LoadDropDownListControl(cboLocation,"sp_GetDataCombo @TableName='LS_tblLocationGroup',@Fields='LSLocationGroupID as [ID],VNName as Name'","ID","Name",true);
		}
		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			lblErr.Text = "";
			DataTable dt = new DataTable();
			try
			{
				//Lay gia tri cua EmpSearch
				string strEmpID = clsCommon.SafeDataString(EmpHeaderSearchReport1.txtEmpID.Text.Trim());
				string strEmpName = clsCommon.SafeDataString(EmpHeaderSearchReport1.txtEmpName.Text.Trim());
				string strLevel1 = EmpHeaderSearchReport1.cboLevel1.SelectedValue.Trim();
				string strLevel2 = EmpHeaderSearchReport1.cboLevel2.SelectedValue.Trim();
				string strLevel3 = EmpHeaderSearchReport1.cboLevel3.SelectedValue.Trim();
				string strCompany = EmpHeaderSearchReport1.cboCompany.SelectedValue.Trim();
				string strStatus = EmpHeaderSearchReport1.optStatus.SelectedValue.Trim();
				//string strLocation = EmpHeaderSearchReport1.cboLocation.SelectedValue.Trim();
				string strLocation = cboLocation.SelectedValue.ToString().Trim();
				string strPosition = EmpHeaderSearchReport1.cboPosition.SelectedValue.Trim();
				string strJobcode = EmpHeaderSearchReport1.cboJobcode.SelectedValue.Trim();

				string strMonth = txtDate.Text;
				string strPrintDate = txtPrintDate.Text;
				string strTitle = txtTitle.Text;
				string sAccountLogin= Session["AccountLogin"]!=null?Session["AccountLogin"].ToString():"admin";
		
				dt = clsCommon.GetDataTable("SI_sprpt01aTBH @FromDate ='" + txtFromDate.Text.Trim()+"',@ToDate = '"+txtToDate.Text+"',@EmpName = N'"+ strEmpName+"',@EmpID= '"+strEmpID+"',@CompanyID = '"+ strCompany+"',@Level1ID = '"+ strLevel1+"',@Level2ID = '"+ strLevel2+"',@Level3ID = '"+ strLevel3+"',@UserGroupID = '"+ sAccountLogin+"',@LocationID = '"+ strLocation+"'");

				#region Header Company Info
				DataTable dtSI_ComInfo= ClsSI_Report.GetData_ComInfo(strLevel1,strMonth,"1",this.Page);
				
				string strHeaderParams = "";
				string strHeaderValues = "";
				foreach(DataColumn oDataColumn in dtSI_ComInfo.Columns)	
				{
					strHeaderParams += ";" + oDataColumn.ColumnName;
					strHeaderValues += ";" + dtSI_ComInfo.Rows[0][oDataColumn.ColumnName].ToString();
				}
				#endregion
				

				//Phan khai bao se dung Tool bao cao Excel
				iHRPCore.Reports.v11.clsBaocaoExcel bc = new iHRPCore.Reports.v11.clsBaocaoExcel();
				#region Footer
				string strFooterParams = "PrintDD;PrintMM;PrintYY";
				string strDate = txtPrintDate.Text;
				if (strDate == "")
					strDate = "..../..../20.....";
				string strFooterValues = strDate;
				strFooterValues = strFooterValues.Replace("/",";");

				strFooterParams += ";Cot1";
				strFooterValues += ";"+dt.Rows.Count.ToString();
				strFooterParams += ";Cot2";
				strFooterValues += ";"+bc.SumColumn(dt,"SalaryBH").ToString();
				strFooterParams += ";Cot3";
				strFooterValues += ";"+Convert.ToString(bc.SumColumn(dt,"SalaryBH") * 0.2);
				strFooterParams += ";Cot4";
				strFooterValues += ";"+Convert.ToString(bc.SumColumn(dt,"SalaryBH") * 0);
				strFooterParams += ";Cot4";
				strFooterValues += ";"+Convert.ToString(bc.SumColumn(dt,"SalaryBH") * 0);
				strFooterParams += ";Cot5";
				strFooterValues += ";"+bc.SumColumn(dt,"Salary").ToString();
				strFooterParams += ";Cot6";
				strFooterValues += ";"+Convert.ToString(bc.SumColumn(dt,"Salary") * 0.03);
				strFooterParams += ";Cot7";
				strFooterValues += ";"+Convert.ToString((bc.SumColumn(dt,"SalaryBH") * 0.2) + (bc.SumColumn(dt,"Salary") * 0.03));
				strFooterParams += ";Cot8";
				strFooterValues += ";"+dt.Rows.Count.ToString();
				strFooterParams += ";Cot9";
				strFooterValues += ";"+dt.Rows.Count.ToString();
				#endregion
				#region Config System
				/*
				 * itempheadlv1: id cho Header Group 1
				 * itempheadlv2: id cho Header Group 2
				 * itempsumlv1: id sum cap 1
				 * itempsumlv2: id sum cap 2
				 * itempsumtotal: id sum tong cong tat ca
				 * */
				#endregion
				#region Config Basic
				bc.IsGroupLv1 = false; //Co Group 1 khong?
				bc.GroupLv1 = 1; //Chỉ định Group level 1 ở cột thứ I
				bc.IsGroupLv2 = false; //Co Group 2 khong?
				bc.GroupLv2 = 2; //Chỉ định Group level 2 ở cột thứ II
				bc.IsSum = true; //Có Sum tong hay không? Nếu có thì phải có id template (itempsumtotal) đi kèm
				bc.IsSum1 = false; //Có Sum cap 1 hay không? Nếu có thì phải có id template (itempsumlv1) đi kèm
				bc.IsSum2 = false; //Có Sum cap 2 hay không? Nếu có thì phải có id template (itempsumlv2) đi kèm
				bc.sfileTemplate = "SI_rpt01aTBH_EN.htm";
				bc.sHeaderParams = strHeaderParams;
				bc.sHeaderValues = strHeaderValues;
				bc.sFooterParams = strFooterParams;
				bc.sFooterValues = strFooterValues;
				string strReports = bc.strReportBasic(dt);
				#endregion
				//End

				ExcelExporter myExcelXport=new ExcelExporter(this.Page);
				myExcelXport.ExportHTMLToExcel(strReports,"Excel");
				myExcelXport = null;
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
			finally
			{
				dt.Dispose();
			}
		}
	}
}
