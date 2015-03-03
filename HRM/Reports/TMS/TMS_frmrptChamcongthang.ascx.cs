namespace Reports.TMS
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using FPTToolWeb.Control.DataGrids;
	using iHRPCore.Include;
	using iHRPCore.TSComponent;
	using iHRPCore.Com;

	/// <summary>
	///		Summary description for TMS_frmrptChamcongthang.
	/// </summary>
	public class TMS_frmrptChamcongthang : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.TextBox txtMonth;
		protected System.Web.UI.WebControls.LinkButton btnSearch;
		protected System.Web.UI.WebControls.LinkButton btnSearch1;
		protected EmpHeaderSearch EmpHeaderSearch1;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if (!Page.IsPostBack)
			{
				//BindDataGrid();
				this.txtMonth.Text=System.DateTime.Now.ToString("dd/MM/yyyy").Substring(3,7);
			}
			btnSearch.Attributes.Add("onclick", "return checkvalidSearch()");
			btnSearch1.Attributes.Add("onclick", "return checkvalidSearch()");
			//EmpHeaderSearch1.Label1.CssClass = "labelRequire";
			//EmpHeaderSearch1.Label3.CssClass = "labelRequire";
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
			this.btnSearch1.Click += new System.EventHandler(this.btnExport2_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			string strEmpID = EmpHeaderSearch1.txtEmpID.Text.Trim();
			string strEmpName = EmpHeaderSearch1.txtEmpName.Text.Trim();
			string strLevel1 = EmpHeaderSearch1.cboLevel1.SelectedValue.Trim();
			string strLevel2 = EmpHeaderSearch1.cboLevel2.SelectedValue.Trim();
			string strLevel3 = EmpHeaderSearch1.cboLevel3.SelectedValue.Trim();
			string strCompany = EmpHeaderSearch1.cboCompany.SelectedValue.Trim();
			//string strFirstName	= EmpHeaderSearch1.txtVFirstName.Text.Trim();
			//string strLastName		= EmpHeaderSearch1.txtVLastName.Text.Trim();
			string strPosition = EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue.Trim();
			string strStatus = EmpHeaderSearch1.optStatus.SelectedValue.Trim();
			clsCommon.OpenNewWindowPopup(this.Page,"Reports/TMS/TMS_rptChamCongThang1.aspx?MMYYYY="+txtMonth.Text+"&EmpID="+strEmpID
				+"&EmpName="+strEmpName+"&Company="+strCompany+"&Level1="+strLevel1+"&Level2="+strLevel2+"&Level3="+strLevel3+"&Position="+strPosition+"&Status="+strStatus,"");
			//clsCommon.OpenNewWindow(this.Page,"Reports/TMS/TMS_rptChamCongThang1.aspx","Bang cham cong thang");
			/*
			DataTable dt = new DataTable();
			try
			{
				//Lay gia tri cua EmpSearch
				string strEmpID = EmpHeaderSearch1.txtEmpID.Text.Trim();
				string strEmpName = EmpHeaderSearch1.txtEmpName.Text.Trim();
				string strLevel1 = EmpHeaderSearch1.cboLevel1.SelectedValue.Trim();
				string strLevel2 = EmpHeaderSearch1.cboLevel2.SelectedValue.Trim();
				string strLevel3 = EmpHeaderSearch1.cboLevel3.SelectedValue.Trim();
				string strCompany = EmpHeaderSearch1.cboCompany.SelectedValue.Trim();
				string strStatus = EmpHeaderSearch1.optStatus.SelectedValue.Trim();

				string strMMYYYY = txtMonth.Text;

				dt= clsTSEmpList.GetData_ChamCongThang(strCompany,strLevel1,strLevel2,strLevel3,
					strEmpID,strEmpName,strStatus,strMMYYYY,this.Page);

				string strHeader = "";
				strHeader = @"
					<table><tr><td colSpan=3>NGÂN HÀNG PHƯƠNG ĐÔNG</TD></TR></TABLE>
					BẢNG CHẤM CÔNG THÁNG " + strMMYYYY + @"<BR>
				";
				string strFooter = "";
				strFooter = @"<br>
					<table><tr><td></td></tr>
					<tr>
					<td colSpan=7></td>
					<td colSpan=7><b>Trưởng đơn vị</b></TD>
					<td colSpan=9></td>
					<td colSpan=7><b>Người chấm công</b></TD>
					</TR></TABLE>
				";
				ExcelExporter myExcelXport=new ExcelExporter(this.Page);
				myExcelXport.ExportDataTable(dt,strHeader,strFooter,"xls");
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
			*/
		}

		private void btnExport2_Click(object sender, System.EventArgs e)
		{
			DataTable dt = new DataTable();
			try
			{
				//Lay gia tri cua EmpSearch
				string strEmpID = EmpHeaderSearch1.txtEmpID.Text.Trim();
				string strEmpName = EmpHeaderSearch1.txtEmpName.Text.Trim();
				string strLevel1 = EmpHeaderSearch1.cboLevel1.SelectedValue.Trim();
				string strLevel2 = EmpHeaderSearch1.cboLevel2.SelectedValue.Trim();
				string strLevel3 = EmpHeaderSearch1.cboLevel3.SelectedValue.Trim();
				string strCompany = EmpHeaderSearch1.cboCompany.SelectedValue.Trim();
				//string strFirstName	= EmpHeaderSearch1.txtVFirstName.Text.Trim();
				//string strLastName		= EmpHeaderSearch1.txtVLastName.Text.Trim();
				string strPosition = EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue.Trim();
				string strStatus = EmpHeaderSearch1.optStatus.SelectedValue.Trim();

				string strMMYYYY = txtMonth.Text;
				int YYYY = Convert.ToInt32(strMMYYYY.Substring(3,4));
				int MM = Convert.ToInt32(strMMYYYY.Substring(0,2));

				//dt= clsTSEmpList.GetData_ChamCongThang2(strCompany,strLevel1,strLevel2,strLevel3,
				//	strEmpID,strEmpName,strFirstName,strLastName,strPosition,strStatus,strMMYYYY,this.Page);
				dt.Columns.Remove("EmpID");
					
				string strHeader = "";
				string[] RowList = {"1","2"};
				strHeader = @"
					<table><tr><td></td><td></td><td>NGÂN HÀNG PHƯƠNG ĐÔNG</TD></TR></TABLE>
					BẢNG CHẤM CÔNG THÁNG " + strMMYYYY + @"<BR>
				";
				#region tao header cho bao cao
				string TableHeader = "";
				string strDateID = "";
				
				string strFromDate = "";
				string strToDate = "";

				if(Convert.ToInt32(dt.Columns[4].Caption.Substring(1)) > Convert.ToInt32(dt.Columns[dt.Columns.Count-2].Caption.Substring(2)))
				{
					if(MM!=1)
						strFromDate = Convert.ToString(MM-1)+"/"+ Convert.ToInt32(dt.Columns[4].Caption.Substring(1)) + "/" + YYYY.ToString(); 
					else
						strFromDate = "12"+"/"+ Convert.ToInt32(dt.Columns[4].Caption.Substring(1)) + "/" + (YYYY-1).ToString(); 
				}
				else 
				{
					strFromDate = MM.ToString()+"/"+dt.Columns[4].Caption.Substring(1)+"/"+YYYY.ToString();
				}
				strToDate =  MM.ToString()+"/"+dt.Columns[dt.Columns.Count-2].Caption.Substring(2)+"/"+YYYY.ToString();
				
				DateTime mDateID = Convert.ToDateTime(strFromDate);
				DateTime mDateEnd = Convert.ToDateTime(strToDate);
				
				TableHeader = @"
					<td rowSpan=2><b>STT</b></td>
					<td rowSpan=2><b>Mã NV</b></td>
					<td rowSpan=2><b>Tên NV</b></td>";
				
				while(mDateID <= mDateEnd)
				{
					TableHeader += @"
					<td colSpan=3>
						<div align=center><b>"+mDateID.ToString("dd/MM/yyyy")+@"</b></div>
					</td>";
					mDateID = mDateID.AddDays(1);
				}
				TableHeader += @"<td rowSpan=2><b>Ghi chú</b></td>";
				TableHeader += @"</tr><tr>";
				mDateID = Convert.ToDateTime(strFromDate);
				while(mDateID <= mDateEnd)
				{
					TableHeader += @"
					<td><b>Vào<b></td>
					<td><b>Ra<b></td>
					<td><b>Ngoài giờ<b></td>";
					mDateID = mDateID.AddDays(1);
				}
				
				#endregion
				string strFooter = "";
				ExcelExporter myExcelXport=new ExcelExporter(this.Page);
				myExcelXport.ExportDataTable(dt,strHeader,"xls",TableHeader,RowList);
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
