namespace iHRPCore.MdlHR
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.HRComponent;
	using iHRPCore.Com;
	using System.Data.SqlClient;
	using System.Configuration;
	using GridSort;

	/// <summary>
	///		Summary description for LevelSetup_popup.
	/// </summary>
	public class LevelSetup_popup : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.DataGrid dtgLevelParent;
		protected System.Web.UI.WebControls.DataGrid dtgLevelParent_select;
		protected System.Web.UI.WebControls.LinkButton btnSelectAll;
		protected System.Web.UI.WebControls.LinkButton btnSelect;
		protected System.Web.UI.WebControls.LinkButton btnRemove;
		protected System.Web.UI.WebControls.LinkButton btnRemoveAll;
		protected string strLanguage="VN";
		static DataTable dtLevelParent_Select=new DataTable();
		static DataTable m_dt=new DataTable();
		protected DataTable dtLevelParent=new DataTable();
		protected string strLSLevelCode ;
		static string strCodeList,strCodeName;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtParentCode;
		protected System.Web.UI.WebControls.LinkButton btnAccept;
		protected System.Web.UI.WebControls.ImageButton imgbtnTop02;
		protected System.Web.UI.WebControls.ImageButton imgbtnLogo;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtParentName;
		

		private void Page_Load(object sender, System.EventArgs e)
		{
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
			// Put user code to initialize the page here
			if(!Page.IsPostBack)
			{	
				strLSLevelCode=Request.Params["LSLevelCode"].ToString().Trim();
				strCodeList=Request.Params["strCodeList"].ToString().Trim();
				if (Request.Params["tabid"].ToString().Trim()=="Level2")
				{
					strCodeName=clsLevel2.getStrCodeName("," + strCodeList);
				}
				else
				{
					strCodeName=clsLevel3.getStrCodeName("," + strCodeList);
				}
				strCodeName+=",";
				this.txtParentName.Value=strCodeName;
				this.txtParentCode.Value=strCodeList;
				this.BindDataGridLevelParent();		
				this.BindDataGridLevelParent_selected();
				//this.GetCode();
				//DataGridSort.AddItemColumn(uctrlColumns, dtgLevel);
			}
			this.btnAccept.Attributes.Add("OnClick","return ReturnLevelValue()");
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
			this.imgbtnTop02.Click += new System.Web.UI.ImageClickEventHandler(this.imgbtnTop02_Click);
			this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
			this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		private void BindDataGridLevelParent()
		{			
			if (Request.Params["tabid"].ToString().Trim()=="Level2")
				m_dt=clsLevel2.GetLevel1(strLanguage);  				
			else
				m_dt=clsLevel3.GetLevel2(strLanguage);  

			//cangtt 
			string[] arrLevel;
			 arrLevel= strCodeList.Split(',');
			for(int z=0;z<=arrLevel.Length-1;z++)				
			{
				for (int i=0;i<=m_dt.Rows.Count-1;i++)	
				{
					if (m_dt.Rows[i]["ParentLevelCode"].ToString()==arrLevel.GetValue(z).ToString())
					{
							m_dt.Rows.RemoveAt(i);
							
					}
				}
			}

			dtgLevelParent.DataSource=m_dt;
			dtgLevelParent.DataBind();
		}

		private void BindDataGridLevelParent_selected()
		{
			if (strLSLevelCode.Length>0 )
			{
				if (Request.Params["tabid"].ToString().Trim()=="Level2")					
					dtLevelParent_Select=clsLevel2.GetDataFromItem_popup(strLSLevelCode,strLanguage);
				else
					dtLevelParent_Select=clsLevel3.GetDataFromItem_popup(strLSLevelCode,strLanguage);
				
			}
			else
			{
				dtLevelParent_Select=m_dt.Clone();
				dtgLevelParent_select.DataSource=dtLevelParent_Select;
				dtgLevelParent_select.DataBind();
			}
			
			dtgLevelParent_select.DataSource=dtLevelParent_Select;
			dtgLevelParent_select.DataBind();
			

			
		}

		private void btnSelect_Click(object sender, System.EventArgs e)
		{
			int iNumberRemove=0;
			for(int iIndex=0;iIndex<m_dt.Rows.Count;iIndex++)
			{
				if(((CheckBox)dtgLevelParent.Items[iIndex+iNumberRemove].FindControl("chkSelect")).Checked==true)
				{
					DataRow newRow=dtLevelParent_Select.NewRow();
					newRow.ItemArray=m_dt.Rows[iIndex].ItemArray;					
					dtLevelParent_Select.Rows.Add(newRow);
					strCodeName+=m_dt.Rows[iIndex]["ParentLevelCode"].ToString()+"@"+m_dt.Rows[iIndex]["ParentLevelName"].ToString()+",";					
					strCodeName=strCodeName.Replace(",,",",");
					strCodeList+=m_dt.Rows[iIndex]["ParentLevelCode"].ToString()+",";
					strCodeList=strCodeList.Replace(",,",",");
					//if (strCodeList.Substring(0,1).ToString().Equals(",")) strCodeList=strCodeList.Remove(0,1);					
					m_dt.Rows.RemoveAt(iIndex);				
					iIndex--;
					iNumberRemove++;
				}
			}		
	
			dtgLevelParent.DataSource=m_dt;
			dtgLevelParent.DataBind();
			dtgLevelParent_select.DataSource=dtLevelParent_Select;
			dtgLevelParent_select.DataBind();
			this.txtParentName.Value=strCodeName;
			this.txtParentCode.Value=strCodeList;

			//CalculateNumberOfEmp();

			//pnlGrid.Visible=true;
		}

		private void btnRemove_Click(object sender, System.EventArgs e)
		{
			int iNumberRemove=0;
			for(int iIndex=0;iIndex<dtLevelParent_Select.Rows.Count;iIndex++)
			{
				if(((CheckBox)dtgLevelParent_select.Items[iIndex+iNumberRemove].FindControl("chkSelect_")).Checked==true)
				{
					DataRow newRow=m_dt.NewRow();
					newRow.ItemArray=dtLevelParent_Select.Rows[iIndex].ItemArray;					
					m_dt.Rows.Add(newRow);
					strCodeName=strCodeName.Replace(dtLevelParent_Select.Rows[iIndex]["ParentLevelCode"].ToString()+"@"+dtLevelParent_Select.Rows[iIndex]["ParentLevelName"].ToString(),"");					
					strCodeName=strCodeName.Replace(",,",",");
					strCodeList=strCodeList.Replace(dtLevelParent_Select.Rows[iIndex]["ParentLevelCode"].ToString(),"");
					strCodeList=strCodeList.Replace(",,",",");
					dtLevelParent_Select.Rows.RemoveAt(iIndex);						
					iIndex--;
					iNumberRemove++;
				}
			}		
	
			dtgLevelParent.DataSource=m_dt;
			dtgLevelParent.DataBind();
			dtgLevelParent_select.DataSource=dtLevelParent_Select;
			dtgLevelParent_select.DataBind();
			this.txtParentName.Value=strCodeName;
			this.txtParentCode.Value=strCodeList;
			
			//CalculateNumberOfEmp();
			
			//pnlGrid.Visible=true;
		}

		private void GetCode()
		{
			if  (strCodeList.Trim() == "") 
			{			
				for(int iIndex=0;iIndex<dtLevelParent_Select.Rows.Count;iIndex++)
				{				
					strCodeName+=dtLevelParent_Select.Rows[iIndex]["ParentLevelCode"].ToString()+"@"+dtLevelParent_Select.Rows[iIndex]["ParentLevelName"].ToString()+",";					
					strCodeList+=dtLevelParent_Select.Rows[iIndex]["ParentLevelCode"].ToString()+",";			
				}
			}
	
			this.txtParentName.Value=strCodeName;
			this.txtParentCode.Value=strCodeList;		

		}

		private void btnSelectAll_Click(object sender, System.EventArgs e)
		{
		
		}

		private void imgbtnLogo_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			string strReturn ="";
			/*string str_FrmName = this.Page.ToString().Substring(4,this.Page.ToString().Length-9);
			if (Request.Params["Ascx"] != null)
				str_FrmName = Request.Params["Ascx"];*/
			string str_FrmName = Request.Params["Ascx"];
			foreach(Control Child_ctl in Page.Controls)
			{
				strReturn =  clsChangeLang.GetAllControlOfForm(Child_ctl, str_FrmName);
			}
		}

		private void imgbtnTop02_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			string str_FrmName = Request.Params["Ascx"];
			Session["Link_URL"] = Request.ServerVariables["QUERY_STRING"];
			this.Response.Redirect("UMS_frmCaption.aspx?ID="+str_FrmName);
		}	
	}
}
