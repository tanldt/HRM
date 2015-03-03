namespace iHRPCore.MdlHR
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.HRComponent;
	using iHRPCore.Com;
	
	public class MonthlyKPI : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.LinkButton btnView;
		protected System.Web.UI.WebControls.DropDownList cboExistingMonths;
		protected System.Web.UI.WebControls.Label lblKPIInfo;
		protected System.Web.UI.WebControls.TextBox txtClass;
		protected System.Web.UI.WebControls.Label lblExistMonth;
		protected System.Web.UI.WebControls.Label lblNewMonth;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.Label lblTotalRows;
		protected System.Web.UI.WebControls.DataGrid dtgKPI;
		protected System.Web.UI.WebControls.LinkButton Linkbutton4;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.TextBox txtNewMonth;
		protected System.Web.UI.WebControls.LinkButton btnCreate;
		protected System.Web.UI.WebControls.CheckBox chkCopy;
		#region Declare

		#endregion Declare


		static DataTable m_dt=new DataTable();

		private void Page_Load(object sender, System.EventArgs e)
		{			
			clsHRMISInput.LoadComboExistingMonths(cboExistingMonths);
			lblTotalRows.Text="";
			if(!IsPostBack)
			{
				btnCreate.Attributes.Add("onclick","return MyCheckDate('"+txtNewMonth.ClientID+"');");
				Linkbutton4.Attributes.Add("onclick","return ConfirmSave('"+chkCopy.ClientID+"','"+txtNewMonth.ClientID+"');");
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
			this.btnView.Click += new System.EventHandler(this.btnView_Click);
			this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
			this.dtgKPI.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dtgKPI_ItemCreated);
			this.Linkbutton4.Click += new System.EventHandler(this.Linkbutton4_Click);
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnView_Click(object sender, System.EventArgs e)
		{			
			string[] strMMYYYY=cboExistingMonths.SelectedValue.Split('/');			
			m_dt=clsHRMISInput.LoadMonthly(strMMYYYY[0],strMMYYYY[1]);            			

			dtgKPI.DataSource=m_dt;
			dtgKPI.DataBind();

			dtgKPI.Height=new Unit("0px");
			dtgKPI.HeaderStyle.Height=new Unit("0px");
			
			lblTotalRows.Text=m_dt.Rows.Count.ToString();
			txtClass.Text=m_dt.Rows[0]["Class"].ToString();
		}

		private void Linkbutton4_Click(object sender, System.EventArgs e)
		{
			bool bResult=true;			
			if(chkCopy.Checked==true)
			{
				string[] strMMYYYY=txtNewMonth.Text.Split('/');	
				clsHRMISInput.EnsureAllKPI(strMMYYYY[0],strMMYYYY[1]);
				m_dt=clsHRMISInput.LoadMonthly(strMMYYYY[0],strMMYYYY[1]);
			}
			
			for(int iIndex=0;iIndex<dtgKPI.Items.Count;iIndex++)
			{
				TextBox mySectionList=(TextBox)dtgKPI.Items[iIndex].FindControl("lblSectionList");
				HtmlInputHidden mySectionCodeList=(HtmlInputHidden)dtgKPI.Items[iIndex].FindControl("txtSectionCodeList");
				TextBox myPKI=(TextBox)dtgKPI.Items[iIndex].FindControl("lblKPIValue");

				if(iIndex<m_dt.Rows.Count)
				{
					if(m_dt.Rows[iIndex]["IsUse"].ToString()=="True")
					{
						if(chkCopy.Checked==true)
						{
							// RE CALCULATE KPI
							string[] strArrSectionCode=mySectionCodeList.Value.Split(',');
							int iEmpNo=0;
							for(int k=0;k<strArrSectionCode.Length;k++)
							{
								if(strArrSectionCode[k]!="")
								{
									DataTable dtTemp=clsHRMISInputKPI.CalculateNumberOfEmp("","","",strArrSectionCode[k]);
									iEmpNo+=dtTemp.Rows.Count;
								}
							}
							myPKI.Text=iEmpNo.ToString();
							///////////////////	
						}
						bResult=clsHRMISInputKPI.UpdateMISInput(m_dt.Rows[iIndex]["MISInputID"].ToString(),
							myPKI.Text,mySectionList.Text,mySectionCodeList.Value,txtClass.Text);
						//bResult=clsCommon.ImpactDB(null,"Update",this,"HR_spfrmMISInput");
					}
				}
				myPKI.Text="";
				mySectionList.Text="";
				mySectionCodeList.Value="";
			}
			if(bResult==true) { btnView_Click(btnView,null);lblErr.Text=""; }
			else 			
				lblErr.Text="Save data failed";			
		}

		private void dtgKPI_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType==ListItemType.Item || e.Item.ItemType==ListItemType.AlternatingItem)
			{						
				Button btnSelectSection=(Button)e.Item.FindControl("btnSelectSection");
				TextBox lblKPIValue=(TextBox)e.Item.FindControl("lblKPIValue");
				TextBox txtSectionList=(TextBox)e.Item.FindControl("lblSectionList");
				HtmlInputHidden txtSectionCodeList=(HtmlInputHidden)e.Item.FindControl("txtSectionCodeList");
				btnSelectSection.Attributes.Add("onclick","return OpenWindowSection('"+dtgKPI.ClientID+"__ctl"+(e.Item.ItemIndex+2).ToString()+"_"+txtSectionList.ClientID+"','"+dtgKPI.ClientID+"__ctl"+(e.Item.ItemIndex+2).ToString()+"_"+lblKPIValue.ClientID+"','"+dtgKPI.ClientID+"__ctl"+(e.Item.ItemIndex+2).ToString()+"_"+txtSectionCodeList.ClientID+"');");

				lblKPIValue.Text=m_dt.Rows[e.Item.ItemIndex]["KPIValue"].ToString();
				txtSectionList.Text=m_dt.Rows[e.Item.ItemIndex]["SectionList"].ToString();
				txtSectionCodeList.Value=m_dt.Rows[e.Item.ItemIndex]["SectionCodeList"].ToString();				

				// CHANGE COLOR DEPEND ON IS_USE
				if(m_dt.Rows[e.Item.ItemIndex]["IsUse"].ToString()=="False")
				{
					for(int i=0;i<e.Item.Cells.Count;i++)
						e.Item.Cells[i].ForeColor=Color.Red;
					lblKPIValue.ForeColor=Color.Red;
					txtSectionList.ForeColor=Color.Red;
				}
			}
		}

		private void btnCreate_Click(object sender, System.EventArgs e)
		{
			if(txtNewMonth.Text.Length<=0) return;

			string[] strMMYYYY=txtNewMonth.Text.Split('/');			
			m_dt=clsHRMISInput.LoadMonthly(strMMYYYY[0],strMMYYYY[1]);            			

			dtgKPI.DataSource=m_dt;
			dtgKPI.DataBind();

			dtgKPI.Height=new Unit("0px");
			dtgKPI.HeaderStyle.Height=new Unit("0px");
			
			lblTotalRows.Text=m_dt.Rows.Count.ToString();
		}

		private void btnExport_Click(object sender, System.EventArgs e)
		{
			iHRPCore.DataGridExcelExporter myExporter=new DataGridExcelExporter(dtgKPI);
			myExporter.Export("KPI Export - "+txtClass.Text+" - "+cboExistingMonths.SelectedValue);
		}
	}
}
