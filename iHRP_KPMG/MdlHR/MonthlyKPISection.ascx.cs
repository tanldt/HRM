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
using iHRPCore.HRComponent;
using iHRPCore.Com;

namespace iHRPCore.MdlHR
{
	/// <summary>
	/// Summary description for MonthlyKPISection.
	/// </summary>
	public class MonthlyKPISection : System.Web.UI.UserControl
	{
		#region Declare
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label lblLevel1ID;
		protected System.Web.UI.WebControls.DropDownList cboLevel1ID;
		protected System.Web.UI.WebControls.Label lblLevel2ID;
		protected System.Web.UI.WebControls.DropDownList cboLevel2ID;
		protected System.Web.UI.WebControls.Label lblLevel3ID;
		protected System.Web.UI.WebControls.DropDownList cboLevel3ID;
		protected System.Web.UI.WebControls.Label lblLevel4ID;
		protected System.Web.UI.WebControls.DropDownList cboLevel4ID;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.TextBox txtName;
		protected System.Web.UI.WebControls.TextBox txtCode;
		protected System.Web.UI.WebControls.LinkButton Linkbutton2;
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.DataGrid dtgAll;
		protected System.Web.UI.WebControls.DataGrid dtgSelected;
		protected System.Web.UI.WebControls.LinkButton btnAcce;
		protected System.Web.UI.WebControls.LinkButton btnClose;
		protected System.Web.UI.WebControls.LinkButton btnSearch;
		protected System.Web.UI.WebControls.LinkButton btnAccept;
		protected System.Web.UI.WebControls.LinkButton cmdSelect;
		protected System.Web.UI.WebControls.LinkButton cmdRemove;
		protected System.Web.UI.WebControls.Label lblCode;
		#endregion Declare

		static DataTable m_dt=new DataTable();
		protected System.Web.UI.WebControls.Panel pnlGrid;
		static DataTable m_dtSelected=new DataTable();
		static string strSectionListID,strKPIID,strSectionCodeListID,strCodeList;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtSectionList;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtSectionCodeList;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtKPIValue;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			btnClose.Attributes.Add("onclick", "javascript:window.close();");			
			if(!IsPostBack) 
			{
				strSectionListID=strKPIID=strSectionCodeListID=strCodeList;
				strSectionListID=Request.Params.Get("SectionListID");
				strKPIID=Request.Params.Get("KPIID");
				strSectionCodeListID=Request.Params.Get("CodeListID");
				strCodeList=Request.Params.Get("CodeList");
			
				LoadCombos();			
				btnSearch_Click(btnSearch,null);
				LoadSelectedSections();
				CalculateNumberOfEmp();
				pnlGrid.Visible=true;

				btnAccept.Attributes.Add("onclick","return ReturnKPIValue('"+strSectionListID+"','"+strKPIID+"','"+strSectionCodeListID+"')");
			}
		}

		private void LoadCombos()
		{
			clsCommon.LoadDropDownListControl(cboLevel1ID,"sp_GetDataCombo @TableName='LS_tblCompany'","LSCompanyCode","Name",true);
			clsCommon.LoadDropDownListControl(cboLevel2ID,"sp_GetDataCombo @TableName='LS_tblLevel1'","LSLevel1Code","Name",true);
			clsCommon.LoadDropDownListControl(cboLevel3ID,"sp_GetDataCombo @TableName='LS_tblLevel2'","LSLevel2Code","Name",true);
			clsCommon.LoadDropDownListControl(cboLevel4ID,"sp_GetDataCombo @TableName='LS_tblLevel3'","LSLevel3Code","Name",true);
			
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
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			this.cmdSelect.Click += new System.EventHandler(this.cmdSelect_Click);
			this.cmdRemove.Click += new System.EventHandler(this.cmdRemove_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			m_dt=clsHRMISInputKPI.Search(cboLevel1ID.SelectedValue,cboLevel2ID.SelectedValue,
				cboLevel3ID.SelectedValue,cboLevel4ID.SelectedValue,txtCode.Text,txtName.Text,strCodeList,false);	

			dtgAll.DataSource=m_dt;
			dtgAll.DataBind();			
		}

		private void LoadSelectedSections()
		{
			if(strCodeList.Length>0)
			{
				m_dtSelected=clsHRMISInputKPI.Search("","","","","","",strCodeList,true);
				dtgSelected.DataSource=m_dtSelected;
				dtgSelected.DataBind();
			}
			else
			{
				m_dtSelected=m_dt.Clone();
				dtgSelected.DataSource=m_dtSelected;
				dtgSelected.DataBind();
			}
		}
		private void cmdSelect_Click(object sender, System.EventArgs e)
		{
			int iNumberRemove=0;
			for(int iIndex=0;iIndex<m_dt.Rows.Count;iIndex++)
			{
				if(((CheckBox)dtgAll.Items[iIndex+iNumberRemove].FindControl("chkSectionSelect")).Checked==true)
				{
					DataRow newRow=m_dtSelected.NewRow();
					newRow.ItemArray=m_dt.Rows[iIndex].ItemArray;					
					m_dtSelected.Rows.Add(newRow);
					strCodeList+=","+m_dt.Rows[iIndex]["LSLevel3Code"].ToString();					
					m_dt.Rows.RemoveAt(iIndex);				
					iIndex--;
					iNumberRemove++;
				}
			}		
	
			dtgAll.DataSource=m_dt;
			dtgAll.DataBind();
			dtgSelected.DataSource=m_dtSelected;
			dtgSelected.DataBind();

			CalculateNumberOfEmp();

			pnlGrid.Visible=true;
		}

		private void cmdRemove_Click(object sender, System.EventArgs e)
		{
			int iNumberRemove=0;
			for(int iIndex=0;iIndex<m_dtSelected.Rows.Count;iIndex++)
			{
				if(((CheckBox)dtgSelected.Items[iIndex+iNumberRemove].FindControl("chkSectionSelected")).Checked==true)
				{
					DataRow newRow=m_dt.NewRow();
					newRow.ItemArray=m_dtSelected.Rows[iIndex].ItemArray;					
					m_dt.Rows.Add(newRow);
					strCodeList=strCodeList.Replace(m_dtSelected.Rows[iIndex]["LSLevel3Code"].ToString(),"");
					strCodeList=strCodeList.Replace(",,",",");
					m_dtSelected.Rows.RemoveAt(iIndex);						
					iIndex--;
					iNumberRemove++;
				}
			}		
	
			dtgAll.DataSource=m_dt;
			dtgAll.DataBind();
			dtgSelected.DataSource=m_dtSelected;
			dtgSelected.DataBind();
			
			CalculateNumberOfEmp();
			
			pnlGrid.Visible=true;
		}

		private void btnAccept_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script>window.close();</script>");
		}

		private void CalculateNumberOfEmp()
		{
			string strTmp1="";
			string strTmp2="";
			int iNoEmp=0;

			for(int iIndex=0;iIndex<dtgAll.Items.Count;iIndex++)
				dtgAll.Items[iIndex].Cells[0].Text=iIndex.ToString();

			for(int iIndex=0;iIndex<dtgSelected.Items.Count;iIndex++)
			{
				DataTable tempDt=clsHRMISInputKPI.CalculateNumberOfEmp(
					m_dtSelected.Rows[iIndex]["LSCompanyCode"].ToString(),
					m_dtSelected.Rows[iIndex]["LSLevel1Code"].ToString(),
					m_dtSelected.Rows[iIndex]["LSLevel2Code"].ToString(),
					m_dtSelected.Rows[iIndex]["LSLevel3Code"].ToString());

				dtgSelected.Items[iIndex].Cells[3].Text=tempDt.Rows.Count.ToString();
				dtgSelected.Items[iIndex].Cells[0].Text=iIndex.ToString();
				strTmp1+=m_dtSelected.Rows[iIndex]["SectionName"].ToString()+",";
				strTmp2+=m_dtSelected.Rows[iIndex]["LSLevel3Code"].ToString()+",";
				iNoEmp+=tempDt.Rows.Count;
			}
			if(strTmp1.Length>0)			
				strTmp1=strTmp1.Remove(strTmp1.Length-1,1);
			if(strTmp2.Length>0)
				strTmp2=strTmp2.Remove(strTmp2.Length-1,1);
			txtSectionList.Value=strTmp1;
			txtKPIValue.Value=iNoEmp.ToString();
			txtSectionCodeList.Value=strCodeList;		
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
		
		}
	}
}
