using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using iHRPCore.Include;
using iHRPCore.Com;
using iHRPCore.TSComponent;
using System.Configuration;
using iHRPCore.MdlTMS;

namespace Reports.TMS
{
	/// <summary>
	/// Summary description for TMS_rptChamCongThang1.
	/// </summary>
	public class TMS_rptChamCongThang1 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DataGrid dtgList;
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		public ArrayList arrSat;
		public ArrayList arrSun;
		public ArrayList arrHol;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			//Response.ContentType="application/excel";
			//Response.AddHeader("Content-Disposition", "directView;filename=Chamxong1.xls");
			//Response.ContentType = "application/excel";
			//Response.AddHeader("content-disposition", "attachment;filename=chamcong.xls");
			//Response.WriteFile("chamcong.xls");

			//Response.ClearContent();
			//Response.AddHeader("content-disposition", "attachment; filename=\"" + System.DateTime.Now.ToString("yyyy-MM-dd") + System.Web.HttpUtility.UrlEncode("???", System.Text.Encoding.UTF8) + ".xls\"");
			//Response.ContentType = "application/excel";
			// Put user code to initialize the page here
			string strMMYYYY = Request.Params["MMYYYY"];
			lblErr.Text += " " + strMMYYYY;
			string strEmpID = Request.Params["EmpID"];
			string strEmpName = Request.Params["EmpName"];
			string strCompany = Request.Params["Company"];
			string strLevel1 = Request.Params["Level1"];
			string strLevel2 = Request.Params["Level2"];
			string strLevel3 = Request.Params["Level3"];
			string strFirstName = "";//Request.Params["FirstName"];
			string strLastName = "";//Request.Params["LastName"];
			string strPosition = Request.Params["Position"];
			string strStatus = Request.Params["Status"];
			BindDataGrid(strMMYYYY,strEmpID,strEmpName,strCompany,strLevel1,strLevel2,strLevel3,strFirstName,strLastName,strPosition,strStatus);
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
			this.dtgList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dtgList_ItemDataBound);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void BindDataGrid(string strMMYYYY,string strEmpID,string strEmpName,string strCompany,string strLevel1,string strLevel2,string strLevel3,string strFirstName,string strLastName,string strPosition,string strStatus)
		{
			//Lay gia tri cua EmpSearch
			
			DataTable dtList = new DataTable();			
				
			try
			{
				//if(strLevel1 != "")
				{
					dtList= clsTSEmpList.GetDataScan_Collect_Report(strCompany,strLevel1,strLevel2,strLevel3,
						strEmpID,strEmpName,strFirstName,strLastName,strPosition,strStatus,strMMYYYY,this.Page);
					//Process cho dtList
					int YYYY = Convert.ToInt32(strMMYYYY.Substring(3,4));
					int MM = Convert.ToInt32(strMMYYYY.Substring(0,2));

					arrSat = GetSaturday(dtList,MM,YYYY);
					arrSun = GetSunday(dtList,MM,YYYY);
					arrHol = GetHoliday(dtList,MM,YYYY);
				}
				if(dtList.Rows.Count > 0)
				{
					dtgList.DataSource = dtList;
					//dtgList.CurrentPageIndex = 0;
					dtgList.DataBind();
				}
			}
			catch(Exception ex)
			{
				//lblErr.Text = ex.Message;
				clsChangeLang.popupWindowExp(this.Parent,ex);
			}
			finally
			{
				dtList.Dispose();
			}
			
		}
		private ArrayList GetSaturday(DataTable dt,int MM,int YYYY)
		{
			ArrayList arr = new ArrayList();
			string strFromDate = "";
			string strToDate = "";

			if(Convert.ToInt32(dt.Columns[4].Caption) > Convert.ToInt32(dt.Columns[dt.Columns.Count-2].Caption))
			{
				if(MM!=1)
					strFromDate = Convert.ToString(MM-1)+"/"+ Convert.ToInt32(dt.Columns[4].Caption) + "/" + YYYY.ToString(); 
				else
					strFromDate = "12"+"/"+ Convert.ToInt32(dt.Columns[4].Caption) + "/" + (YYYY-1).ToString(); 
			}
			else 
			{
				strFromDate = MM.ToString()+"/"+dt.Columns[4].Caption+"/"+YYYY.ToString();
			}
			strToDate =  MM.ToString()+"/"+dt.Columns[dt.Columns.Count-2].Caption+"/"+YYYY.ToString();
			
			DateTime dtCheckDate = Convert.ToDateTime(strFromDate);
			DateTime dtEndDate = Convert.ToDateTime(strToDate);
			while(dtCheckDate <= dtEndDate)
			{
				if(dtCheckDate.DayOfWeek == DayOfWeek.Saturday)
					arr.Add(GetColumnBelong(dtCheckDate,dt));					
				dtCheckDate = dtCheckDate.AddDays(1);
			}		
			return arr;
		}
		private ArrayList GetSunday(DataTable dt,int MM,int YYYY)
		{
			ArrayList arr = new ArrayList();
			string strFromDate = "";
			string strToDate = "";

			if(Convert.ToInt32(dt.Columns[4].Caption) > Convert.ToInt32(dt.Columns[dt.Columns.Count-2].Caption))
			{
				if(MM!=1)
					strFromDate = Convert.ToString(MM-1)+"/"+ Convert.ToInt32(dt.Columns[4].Caption) + "/" + YYYY.ToString(); 
				else
					strFromDate = "12"+"/"+ Convert.ToInt32(dt.Columns[4].Caption) + "/" + (YYYY-1).ToString(); 
			}
			else 
			{
				strFromDate = MM.ToString()+"/"+dt.Columns[4].Caption+"/"+YYYY.ToString();
			}
			strToDate =  MM.ToString()+"/"+dt.Columns[dt.Columns.Count-2].Caption+"/"+YYYY.ToString();
			
			DateTime dtCheckDate = Convert.ToDateTime(strFromDate);
			DateTime dtEndDate = Convert.ToDateTime(strToDate);
			while(dtCheckDate <= dtEndDate)
			{
				if(dtCheckDate.DayOfWeek == DayOfWeek.Sunday)
					arr.Add(GetColumnBelong(dtCheckDate,dt));					
				dtCheckDate = dtCheckDate.AddDays(1);
			}		
			return arr;
		}

		private ArrayList GetHoliday(DataTable dt,int MM,int YYYY)
		{
			ArrayList arr = new ArrayList();
			string strFromDate = "";
			string strToDate = "";

			DataTable dtData_Holiday=clsCommon.GetDataTable("TS_spfrmSCHEDULEDETAIL @Activity = 'LoadHoliday',@YYYY='" + YYYY + "'");
			
			if(Convert.ToInt32(dt.Columns[4].Caption) > Convert.ToInt32(dt.Columns[dt.Columns.Count-2].Caption))
			{
				if(MM!=1)
					strFromDate = Convert.ToString(MM-1)+"/"+ Convert.ToInt32(dt.Columns[4].Caption) + "/" + YYYY.ToString(); 
				else
					strFromDate = "12"+"/"+ Convert.ToInt32(dt.Columns[4].Caption) + "/" + (YYYY-1).ToString(); 
			}
			else 
			{
				strFromDate = MM.ToString()+"/"+dt.Columns[4].Caption+"/"+YYYY.ToString();
			}
			strToDate =  MM.ToString()+"/"+dt.Columns[dt.Columns.Count-2].Caption+"/"+YYYY.ToString();
			
			DateTime dtCheckDate = Convert.ToDateTime(strFromDate);
			DateTime dtEndDate = Convert.ToDateTime(strToDate);
			while(dtCheckDate <= dtEndDate)
			{
				for (int ii=0;ii<dtData_Holiday.Rows.Count;ii++ )
				{
					if( Convert.ToDateTime(dtData_Holiday.Rows[ii]["DateID"].ToString())==dtCheckDate)
						arr.Add(GetColumnBelong(dtCheckDate,dt));					
				}
				dtCheckDate = dtCheckDate.AddDays(1);
			}		
			return arr;
		}

		private int GetColumnBelong(DateTime dtCheckDate,DataTable dtList)
		{
			for(int i = 4 ; i<dtList.Columns.Count-1 ; i++)
				if(dtCheckDate.Day == Convert.ToInt32(dtList.Columns[i].Caption))
					return i;
			return 0;
		}
		private void dtgList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			e.Item.Cells[0].Visible=false;	
			//e.Item.Cells[1].Visible=false;	
			//e.Item.Cells[2].Visible=false;	
			if (e.Item.ItemType != ListItemType.Header) 
			{
				foreach(Object obj in arrSat)
					e.Item.Cells[Convert.ToInt32(obj)].BackColor = Color.Turquoise;
				foreach(Object obj in arrSun)
					e.Item.Cells[Convert.ToInt32(obj)].BackColor = Color.Yellow;
				foreach(Object obj in arrHol)
					e.Item.Cells[Convert.ToInt32(obj)].BackColor = Color.Red;
			} 
			for(int i = 4 ; i < e.Item.Cells.Count-1;i++)
			{
				e.Item.Cells[i].Width = 20;
				//if (e.Item.ItemType == ListItemType.Item)
				//	e.Item.Cells[i].Text = "'" +  e.Item.Cells[i].Text;
			}
			//dtgList.Columns[0].Visible=false;
		}
	}
}
