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
	using System.Data.SqlClient;
	using System.Configuration;
	using GridSort;


	/// <summary>
	///		Summary description for LevelSetup.
	/// </summary>
	public class LevelSetup : System.Web.UI.UserControl
	{
		#region Declare
		protected System.Web.UI.WebControls.TextBox txtCode;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.TextBox txtName;
		protected System.Web.UI.WebControls.TextBox txtNote;
		protected System.Web.UI.WebControls.TextBox txtVNName;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.CheckBox chkUsed;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnAddNew;
		protected System.Web.UI.WebControls.Label lblCode;
		protected System.Web.UI.WebControls.DataGrid dtgLevel;
		protected System.Web.UI.WebControls.LinkButton btnSearch;
		protected System.Web.UI.WebControls.LinkButton btnAddnew;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected ColumnList uctrlColumns;
		protected System.Web.UI.WebControls.LinkButton btnDetail;
		protected System.Web.UI.WebControls.ListBox lstLevelParent;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtSectionCodeList;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtParentName;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtParentCode;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.TextBox txtShortName;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.TextBox txtRank;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.DropDownList cboLevel3IDParent;
		protected System.Web.UI.HtmlControls.HtmlTableRow trLevelParent;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLSLevel2ID;
		protected string strLanguage="VN";
		protected System.Web.UI.WebControls.TextBox txtAdd;		
		static bool bAdd =true;
		public string str= "Test";
		#endregion
		private void Page_Load(object sender, System.EventArgs e)
		{
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
			txtAdd.Text=bAdd.ToString();
			// Put user code to initialize the page here
			if(!Page.IsPostBack)
			{				
				this.BindDataGridLevel(); 
				DataGridSort.AddItemColumn(uctrlColumns, dtgLevel);
				if (Request.Params["tabid"].ToString().Trim()=="Level3")
				{	
					trLevelParent.Style.Add("DISPLAY","block");					
					clsCommon.LoadDropDownListControl(cboLevel3IDParent,clsLevel3.getComboParent("",strLanguage),"LevelCode","LevelName",true);
				}
				else
				{
					trLevelParent.Style.Add("DISPLAY","none");
				}

			}
			//Hau
			ButtonClick();//

			this.btnSave.Attributes.Add("OnClick","return validform()");
			this.btnDelete.Attributes.Add("OnClick","return CheckDelete()");
			this.btnDetail.Attributes.Add("OnClick","return OpenWindow()");
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
			this.btnAddnew.Click += new System.EventHandler(this.btnAddNew_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			this.dtgLevel.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgLevel_ItemCommand);
			this.dtgLevel.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgLevel_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion		

		private void BindDataGridLevel()
		{
			DataTable dtLevel;
			if (Request.Params["tabid"].ToString().Trim()=="Level2")
				dtLevel=clsLevel2.GetLevel2();
			else
				dtLevel=clsLevel3.GetLevel3();				

			this.dtgLevel.PageSize = uctrlColumns.iPageRows;
			if(this.uctrlColumns.bAdvMultiSort)
			{
				try
				{
					DataGridSort.AdvancedMultiSort(dtgLevel, dtLevel, uctrlColumns);
				}
				catch
				{
					this.dtgLevel.CurrentPageIndex = 0;
					DataGridSort.AdvancedMultiSort(dtgLevel, dtLevel, uctrlColumns);
				}
			}
			else
			{
				try
				{
					DataGridSort.Refresh(dtgLevel, dtLevel);
				}
				catch
				{
					this.dtgLevel.CurrentPageIndex = 0;
					DataGridSort.Refresh(dtgLevel, dtLevel);
				}
			}
			this.uctrlColumns.iTotalRows = dtLevel.Rows.Count;
			dtLevel.Dispose();
		}
		private void dtgLevel_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{		
			DataTable dtList = new DataTable();			
			if (e.CommandName.ToUpper().Trim() == "EDIT")	
			{
				//int iIndex=e.Item.ItemIndex+dtgLevel.PageSize*dtgLevel.CurrentPageIndex;			
				bAdd=false;
				txtAdd.Text=bAdd.ToString();
				txtParentCode.Value="";
				txtParentName.Value="";
				int iIndex=e.Item.ItemIndex;
				if (Request.Params["tabid"].ToString().Trim()=="Level2")	
				{
					string strLevelID=dtgLevel.Items[iIndex].Cells[1].Text.ToString();    
					dtList=clsLevel2.GetDataFromItem_LevelSetup(strLevelID,strLanguage);
					clsCommon.LoadListBoxControl(lstLevelParent,dtList,"ParentLevelCode","ParentLevelName",false);					
					for (int i=0;i<=dtList.Rows.Count-1;i++)
					{
						txtParentCode.Value+=dtList.Rows[i]["ParentLevelCode"].ToString()+",";
						txtParentName.Value+=dtList.Rows[i]["ParentLevelCode"].ToString()+"@"+dtList.Rows[i]["NName"].ToString()+",";
					}
				}
				else
				{
					string strLeve2ID=dtgLevel.Items[iIndex].Cells[1].Text.ToString();    
					DataTable dtData=new DataTable();

					dtData=clsLevel3.getComboParent(strLeve2ID,strLanguage);
					clsCommon.LoadDropDownListControl(cboLevel3IDParent,dtData,"LevelCode","LevelName",true);

					dtList=clsLevel3.GetDataFromItem_LevelSetup(strLeve2ID,strLanguage);
					clsCommon.LoadListBoxControl(lstLevelParent,dtList,"ParentLevelCode","ParentLevelName",false);					
					for (int i=0;i<=dtList.Rows.Count-1;i++)
					{
						txtParentCode.Value+=dtList.Rows[i]["ParentLevelCode"].ToString()+",";
						txtParentName.Value+=dtList.Rows[i]["ParentLevelCode"].ToString()+"@"+dtList.Rows[i]["NName"].ToString()+",";
					}
					cboLevel3IDParent.SelectedValue=dtList.Rows[0]["Level3IDParent"].ToString();
				}
				txtCode.Text=dtList.Rows[0]["Code"].ToString();
				txtShortName.Text=dtList.Rows[0]["ShortName"].ToString();
				txtName.Text=dtList.Rows[0]["Name"].ToString();
				txtVNName.Text=dtList.Rows[0]["VNName"].ToString();
				txtNote.Text=dtList.Rows[0]["Note"].ToString();				
				chkUsed.Checked=dtList.Rows[0]["Used"].ToString()=="True"?true:false; 
				txtRank.Text=dtList.Rows[0]["Rank"].ToString();				
				txtLSLevel2ID.Value=dtList.Rows[0]["ID"].ToString();				
			}			
		}

		private void btnAddNew_Click(object sender, System.EventArgs e)
		{
			clsCommon.ClearControlValue(this);
  			lstLevelParent.Items.Clear();
			txtParentCode.Value="";
			txtParentName.Value="";
			if (Request.Params["tabid"].ToString().Trim()=="Level3") clsCommon.LoadDropDownListControl(cboLevel3IDParent,clsLevel3.getComboParent("",strLanguage),"LevelCode","LevelName",true);
			bAdd=true;
			txtAdd.Text=bAdd.ToString();
		}
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			string strLevel2ID;	
			string strShortName = txtShortName.Text.ToString();
			
			string strLevel2=txtCode.Text.ToString();			

			if (bAdd)
				strLevel2ID=strLevel2;
			else
				strLevel2ID=txtLSLevel2ID.Value;   			

			string strName=txtName.Text.ToString();
			string strVNName=txtVNName.Text.ToString();
			string strNote=txtNote.Text.ToString();
			int iRank;
			if( txtRank.Text.Equals("") )
					iRank= 0;
				else
					iRank=int.Parse(txtRank.Text);
			
			int Used;
			if (chkUsed.Checked==true)
				Used=1;
			else
				Used=0;
			if (Request.Params["tabid"].ToString().Trim()=="Level2")	
			{
				clsLevel2.DeleteUpdateLevel(strLevel2);
				clsLevel2.SaveLevel(txtParentCode.Value,strLevel2ID,strLevel2,strShortName,strName,strVNName,strNote,Used,iRank);
			}
			else
			{
				clsLevel3.DeleteUpdateLevel(strLevel2); 
				clsLevel3.SaveLevel(txtParentCode.Value,strLevel2ID,strLevel2,strName,strVNName,strNote,Used,strShortName,iRank,cboLevel3IDParent.SelectedValue);
			}
				this.BindDataGridLevel(); 
				//cangtt - load lai listBox
				string strLevelID=txtCode.Text;	
				if (Request.Params["tabid"].ToString().Trim()=="Level2")
					clsCommon.LoadListBoxControl(lstLevelParent,clsLevel2.GetDataFromItem_LevelSetup(strLevelID,strLanguage),"ParentLevelCode","ParentLevelName",false);	
				else
					clsCommon.LoadListBoxControl(lstLevelParent,clsLevel3.GetDataFromItem_LevelSetup(strLevelID,strLanguage),"ParentLevelCode","ParentLevelName",false);	
			btnAddNew_Click(null,null);
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			for(int i=0;i<dtgLevel.Items.Count;i++)
				if (((CheckBox)this.dtgLevel.Items[i].FindControl("chkSelect1")).Checked==true)
				{
					string strLevel2ID=dtgLevel.Items[i].Cells[1].Text.ToString();     
					if (Request.Params["tabid"].ToString().Trim()=="Level2")	
					{
						if (!clsLevel2.DeleteLevel(strLevel2ID).Equals(""))
						{
							clsChangeLang.popupWindow(this.Parent,"0032",strLanguage,"",0);
							return;
						}
					}
					else
					{
						if (!clsLevel3.DeleteLevel(strLevel2ID).Equals(""))
						{
							clsChangeLang.popupWindow(this.Parent,"0032",strLanguage,"",0);					
							return;
						}
					}
				}
			this.BindDataGridLevel();
		}

		private void btnExport_Click(object sender, System.EventArgs e)
		{
			iHRPCore.DataGridExcelExporter myExcelXport=new iHRPCore.DataGridExcelExporter(dtgLevel);			
			myExcelXport.Export("");
			myExcelXport =null;
		
		}

		private void dtgLevel_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DataGridSort.Grid_IndexChanged(dtgLevel, this.Read_Data() , e);
		

		}
		#region Cac su kien xu li thao tac Sort (Hau)
		private DataTable Read_Data()
		{
			DataTable dtLevel=clsLevel2.GetLevel2();	
			return dtLevel;
		}
		private void ButtonClick()
		{
			this.uctrlColumns.ButtonSort.ServerClick += new EventHandler(ButtonSort_ServerClick);
			this.uctrlColumns.ButtonPRChanged.ServerClick += new EventHandler(ButtonPRChanged_ServerClick);
		}

		private void ButtonSort_ServerClick(object sender, EventArgs e)
		{
			try
			{
				DataGridSort.AdvancedMultiSort(dtgLevel, Read_Data(), uctrlColumns);
			}
			catch{}
		}

		private void ButtonPRChanged_ServerClick(object sender, EventArgs e)
		{
			try
			{
				dtgLevel.PageSize = uctrlColumns.iPageRows;
				dtgLevel.CurrentPageIndex = 0;
				DataView dv = new DataView(Read_Data());
				dv.Sort = DataGridSort.sMulSort;
				dtgLevel.DataSource = dv;
				dtgLevel.DataBind();
			}
			catch{}
		}
		#endregion		

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			DataTable dtLevel;
				if (Request.Params["tabid"].ToString().Trim()=="Level2")			
					dtLevel=clsLevel2.SearchBy(txtCode.Text.Replace("'",""),txtName.Text.Replace("'",""),txtVNName.Text.Replace("'",""),txtShortName.Text.Replace("'",""),txtNote.Text.Replace("'",""),txtRank.Text.Replace("'",""),txtParentCode.Value,chkUsed.Checked==true?"1":"0");
				else
					dtLevel=clsLevel3.SearchBy(txtCode.Text.Replace("'",""),txtName.Text.Replace("'",""),txtVNName.Text.Replace("'",""),txtShortName.Text.Replace("'",""),txtNote.Text.Replace("'",""),txtRank.Text.Replace("'",""),txtParentCode.Value,chkUsed.Checked==true?"1":"0");

				this.dtgLevel.PageSize = uctrlColumns.iPageRows;
				if(this.uctrlColumns.bAdvMultiSort)
				{
					try
					{
						DataGridSort.AdvancedMultiSort(dtgLevel, dtLevel, uctrlColumns);
					}
					catch
					{
						this.dtgLevel.CurrentPageIndex = 0;
						DataGridSort.AdvancedMultiSort(dtgLevel, dtLevel, uctrlColumns);
					}
				}
				else
				{
					try
					{
						DataGridSort.Refresh(dtgLevel, dtLevel);
					}
					catch
					{
						this.dtgLevel.CurrentPageIndex = 0;
						DataGridSort.Refresh(dtgLevel, dtLevel);
					}
				}
				this.uctrlColumns.iTotalRows = dtLevel.Rows.Count;
				dtLevel.Dispose();			
			
		}
		private string getStringLevel()
		{		
			string return_value="";
			for(int i=0;i<=lstLevelParent.Items.Count-1;i++) 
			{
					return_value+=lstLevelParent.Items[i].Value.ToString()+ ",";
			}
			return return_value;
		}

	}
}
