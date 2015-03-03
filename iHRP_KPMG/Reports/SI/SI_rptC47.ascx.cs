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
	//using FPTToolWeb.Data;

	/// <summary>
	///		Summary description for SI_rpt03TBH.
	/// </summary>
	public class SI_rptC47 : System.Web.UI.UserControl
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
		protected System.Web.UI.WebControls.TextBox txtStage;
		protected EmpHeaderSearchReport EmpHeaderSearchReport1;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			
			if (!Page.IsPostBack)
			{
				//BindDataGrid();
				this.txtDate.Text=System.DateTime.Now.ToString("MM/yyyy");
				this.txtPrintDate.Text=System.DateTime.Now.ToString("dd/MM/yyyy");
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
				string strLocation = EmpHeaderSearchReport1.cboLocation.SelectedValue.Trim();
				string strPosition = EmpHeaderSearchReport1.cboPosition.SelectedValue.Trim();
				string strJobcode = EmpHeaderSearchReport1.cboJobcode.SelectedValue.Trim();

				string strMonth =txtDate.Text;
				string strPrintDate = txtPrintDate.Text;
				string strTitle = txtTitle.Text;
				int strStage = Convert.ToInt32(txtStage.Text);
				string strFromDate= txtDate.Text;
				 
				dt= ClsSI_Report.GetData_C47(strCompany,strLevel1,strLevel2,strLevel3,
					strEmpID,strEmpName,strStatus,strLocation,strPosition,strJobcode,strFromDate,strStage,this.Page);

				#region Header Company Info
				DataTable dtSI_ComInfo= ClsSI_Report.GetData_ComInfo(strCompany,strMonth,strStage,this.Page);
				
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
				DataTable dtSI_Info= ClsSI_Report.GetData_InfoFooter03TBH(strCompany,strMonth,this.Page,txtStage.Text.ToString());
				DateTime date = Convert.ToDateTime(clsCommon.ConvertDMYtoMDY("01/" + strMonth));
				string strMonthbef = date.AddMonths(-1).ToString("MM/yyyy");
				//DataTable dtbef= ClsSI_Report.GetData_03TBH(strCompany,strLevel1,strLevel2,strLevel3,
				//	strEmpID,strEmpName,strStatus,strLocation,strPosition,strJobcode,strMonthbef,strStage,this.Page);
				
				foreach(DataColumn oColumn in dtSI_Info.Columns)	
				{
					strFooterParams += ";" + oColumn.ColumnName;
					strFooterValues += ";" + dtSI_Info.Rows[0][oColumn.ColumnName].ToString();
				}
				DataTable dttang = bc.CreateGroupByTable("Tăng lao động",dt,"Tăng lao động","GroupName");
				//string strTang = bc.HTMLDataTable(dttang);
				DataTable dtGiamT = bc.CreateGroupByTable("Giảm lao động (trả thẻ)",dt,"Giảm lao động (trả thẻ)","GroupName");
				//string strGiamT = bc.HTMLDataTable(dtGiamT);
				DataTable dtGiamK = bc.CreateGroupByTable("Giảm lao động (không trả thẻ)",dt,"Giảm lao động (không trả thẻ)","GroupName");
				//string strGiamK = bc.HTMLDataTable(dtGiamK);

				strFooterParams += ";EmpInc1";
				strFooterValues += ";"+dttang.Rows.Count.ToString();
				strFooterParams += ";EmpO1";
				strFooterValues += ";"+Convert.ToString(dtGiamT.Rows.Count + dtGiamK.Rows.Count);
				strFooterParams += ";EmpCuoi1";
				strFooterValues += ";"+Convert.ToString(+bc.SumColumn(dtSI_Info,"EmpF") + dttang.Rows.Count - (dtGiamT.Rows.Count + dtGiamK.Rows.Count));


				strFooterParams += ";SumCot17";
				strFooterValues += ";"+bc.SumColumn(dt,"IncDCAmountBHXH").ToString();
				strFooterParams += ";SCot19";
				strFooterValues += ";"+bc.SumColumn(dt,"DecDCAmountBHXH").ToString();
				strFooterParams += ";SumLast7";
				strFooterValues += ";"+Convert.ToString(bc.SumColumn(dt,"IncDCAmountBHXH") - bc.SumColumn(dt,"DecDCAmountBHXH"));
				strFooterParams += ";SumCot18";
				strFooterValues += ";"+bc.SumColumn(dt,"IncDCAmountBHYT").ToString();
				strFooterParams += ";SCot20";
				strFooterValues += ";"+bc.SumColumn(dt,"DecDCAmountBHYT").ToString();
				strFooterParams += ";SumLast8";
				strFooterValues += ";"+Convert.ToString(bc.SumColumn(dt,"IncDCAmountBHYT") - bc.SumColumn(dt,"DecDCAmountBHYT"));
				strFooterParams += ";Total";
				strFooterValues += ";"+Convert.ToString(
					bc.SumColumn(dtSI_Info,"XH18Last") + //4
					bc.SumColumn(dtSI_Info,"BHYTLast") + //6
					(bc.SumColumn(dt,"IncDCAmountBHXH") - bc.SumColumn(dt,"DecDCAmountBHXH")) + //7
					(bc.SumColumn(dt,"IncDCAmountBHYT") - bc.SumColumn(dt,"DecDCAmountBHYT")));
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
				bc.AutoSort = false; //Co' can Auto Sort cac group khong
				bc.IsGroupLv1 = true; //Co Group 1 khong?
				bc.GroupLv1 = 1; //Chỉ định Group level 1 ở cột thứ I
				bc.IsGroupLv2 = false; //Co Group 2 khong?
				bc.GroupLv2 = 2; //Chỉ định Group level 2 ở cột thứ II
				bc.IsSum = true; //Có Sum tong hay không? Nếu có thì phải có id template (itempsumtotal) đi kèm
				bc.IsSum1 = false; //Có Sum cap 1 hay không? Nếu có thì phải có id template (itempsumlv1) đi kèm
				bc.IsSum2 = false; //Có Sum cap 2 hay không? Nếu có thì phải có id template (itempsumlv2) đi kèm
				bc.sfileTemplate = "SI_rpt03TBH.htm";
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
