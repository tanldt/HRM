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
using iHRPCore.Component;
using iHRPCore.Com;

namespace iHRPCore.SYS
{
	/// <summary>
	/// Summary description for SYS_frmEmployeeList.
	/// </summary>
	public class SYS_frmEmployeeList : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtEmp_ID;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtEmpName;
		protected System.Web.UI.WebControls.DataGrid dtgEmpList;
		protected System.Web.UI.WebControls.Button cmdWhere;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if (Request.Params["UMS"] != null)
				{
					this.BindDataGrid();
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
			this.cmdWhere.Click += new System.EventHandler(this.cmdWhere_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void BindDataGrid()
		{
			try
			{
				string strSql = "UMS_sptblUserAccount 'getEmpFilter','VI','',@EmpID='" + this.txtEmp_ID.Text.Trim();
				strSql = strSql + "', @EmpName=N'" + this.txtEmpName.Text.Trim() + "'";
				DataTable dt_Emp = clsCommon.GetDataTable(strSql); 
				this.dtgEmpList.DataSource=dt_Emp;
				this.dtgEmpList.DataBind();
				dt_Emp.Dispose();
				int i;
				Button cmdSelect;
				for(i=0;i<dtgEmpList.Items.Count;i++)
				{
					string str = "ChangeParentUMS('" + dtgEmpList.Items[i].Cells[1].Text + "','" + dtgEmpList.Items[i].Cells[2].Text + "','" + dtgEmpList.Items[i].Cells[3].Text + "','" + dtgEmpList.Items[i].Cells[4].Text + "')";				
					cmdSelect = (Button)dtgEmpList.Items[i].Cells[1].FindControl("cmdSelect");
					cmdSelect.Attributes.Add("OnClick",str);
				}
			}
			catch{}
		}

		private void cmdWhere_Click(object sender, System.EventArgs e)
		{
			BindDataGrid();
		}
	}
}
