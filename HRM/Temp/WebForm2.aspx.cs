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
using iHRPCore;
using iHRPCore.Com;
using FPTToolWeb.Control.DataGrids;

namespace iHRPCore.Temp
{
	/// <summary>
	/// Summary description for WebForm2.
	/// </summary>
	public class WebForm2 : System.Web.UI.Page
	{
		protected FPTToolWeb.Control.DataGrids.FPTDataGrid dgSales;
		

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
//			SqlDataAdapter DA = new SqlDataAdapter("SELECT top 100 * FROM HR_vEmpList", ConfigurationSettings.AppSettings("ConnString"));
//			DataSet DS = new DataSet();
//			DA.Fill(DS, "HR_vEmpList");
//			// TODO: NotImplemented statement: ICSharpCode.SharpRefactory.Parser.AST.VB.ReDimStatement
			
			dgSales.DataSource = clsCommon.GetDataTable("SELECT top 100 * FROM HR_vEmpList");
			dgSales.DataBind();
		}
		private void dgSales_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
//			if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem) 
//			{
//				//NewValues[e.Item.ItemIndex] = "";
//				NewValues.SetValue("",e.Item.ItemIndex);
//				int ias = ((DataRowView)e.Item.DataItem).DataView.Count;
//				string strGroup = ((DataRowView)e.Item.DataItem).Row["LSLevel1ID"].ToString();
//
//				if (strGroup != LastValue) 
//				{
//					LastValue = strGroup;
//					//NewValues[e.Item.ItemIndex] = string.Format("{0:D}", strGroup);
//					NewValues.SetValue(string.Format("{0:D}", strGroup),e.Item.ItemIndex);
//				}
//			}
		}

		private void dgSales_PreRender(object sender, System.EventArgs e)
		{
			FPTDataGrid gird = new FPTDataGrid();
			if (gird.IsGroupby == true)
			{
				DataGrid DG = ((DataGrid)(sender));
				Table Tbl = (Table)DG.Controls[0];
				DataGridItem DGI;
				TableCell Cell;
				int iAdded = 0;
				for (int i = 0; i <= gird.NewValues.GetUpperBound(0); i++) 
				{
					if (gird.NewValues.GetValue(i).ToString() != "") 
					{
						DGI = new DataGridItem(0, 0, ListItemType.Header);
						Cell = new TableCell();
						Cell.ColumnSpan = 3;
						Cell.Text = gird.NewValues.GetValue(i).ToString().ToUpper();
						DGI.Cells.Add(Cell);
						Tbl.Controls.AddAt(i + iAdded + 1, DGI);
						iAdded = iAdded + 1;
					}
				}
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
			this.dgSales.PreRender += new System.EventHandler(this.dgSales_PreRender);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

	}
}
