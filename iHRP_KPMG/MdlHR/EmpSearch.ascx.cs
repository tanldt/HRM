namespace iHRPCore.MdlHR
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Data.SqlClient;
	using iHRPCore.HRComponent;
	using iHRPCore.Com;

	/// <summary>
	///		Summary description for EmpSearch.
	/// </summary>
	public class EmpSearch : System.Web.UI.UserControl
	{
		#region declare
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.Label Label14;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.Label Label16;
		protected System.Web.UI.WebControls.Label Label17;
		protected System.Web.UI.WebControls.DropDownList cboModule;
		protected System.Web.UI.WebControls.ListBox lstFunction;
		protected System.Web.UI.WebControls.TextBox txtCriteria;
		protected System.Web.UI.WebControls.DropDownList cboCondition;
		protected System.Web.UI.WebControls.TextBox txtInfo;
		protected System.Web.UI.WebControls.RadioButton optAnd;
		protected System.Web.UI.WebControls.RadioButton optOr;
		protected System.Web.UI.WebControls.DataGrid dtgListSelected;
		protected System.Web.UI.WebControls.LinkButton btnAddFunction;
		protected System.Web.UI.WebControls.LinkButton btnRemoveFunction;
		protected System.Web.UI.WebControls.RadioButton optBeginQuote;
		protected System.Web.UI.WebControls.RadioButton optEndQuote;
		protected System.Web.UI.WebControls.RadioButton optNone;
		protected System.Web.UI.WebControls.LinkButton btnAddCondition;
		protected System.Web.UI.WebControls.LinkButton btnRemoveCondition;
		protected System.Web.UI.WebControls.DropDownList cboReportList;
		protected System.Web.UI.WebControls.TextBox txtReportName;
		protected System.Web.UI.WebControls.LinkButton btnSaveReport;
		protected System.Web.UI.WebControls.LinkButton btnView;
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label Label18;
		protected System.Web.UI.WebControls.Label Label19;
		protected System.Web.UI.WebControls.Label Label20;
		protected System.Web.UI.WebControls.ListBox lstColumnSelect;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtCriteriaID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtValue;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtColumnName;
		protected System.Web.UI.WebControls.ListBox lstCondition;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtSelectColumn;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtDisplayColumn;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtConditionCode;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtConditionView;
		protected System.Web.UI.WebControls.DropDownList cboSort1;
		protected System.Web.UI.WebControls.DropDownList cboSort2;
		protected System.Web.UI.WebControls.DropDownList cboSort3;
		protected System.Web.UI.WebControls.DropDownList cboSort4;
		protected System.Web.UI.WebControls.DropDownList cboSort5;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtSortCondition;
		public string strLanguage = "EN";
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtAllGridColumn;
		protected System.Web.UI.WebControls.Literal ltlAlert;
		public DataTable tblTemp = new DataTable();
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtMultiSelect;
		Boolean IsSave = true;
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
			if (!Page.IsPostBack)
			{
				LoadComboBox();
			}
			this.btnSaveReport.Attributes.Add("OnClick","return SaveToReport()");
			this.btnAddFunction.Attributes.Add("OnClick","return AddFunction()");
			this.btnRemoveFunction.Attributes.Add("OnClick","return RemoveFunction()");
			this.btnAddCondition.Attributes.Add("OnClick","return AddCondition()");
			this.btnRemoveCondition.Attributes.Add("OnClick","return RemoveCondition()");
			this.btnView.Attributes.Add("OnClick","return CheckView()");
		}

		private void LoadComboBox()
		{
			clsHREmpList.LoadComboModule(cboModule,strLanguage);
			clsHREmpList.LoadComboOperator(cboCondition,strLanguage);
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
			this.cboModule.SelectedIndexChanged += new System.EventHandler(this.cboModule_SelectedIndexChanged);
			this.btnSaveReport.Click += new System.EventHandler(this.btnSaveReport_Click);
			this.btnView.Click += new System.EventHandler(this.btnView_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		#region MakeTableStructure
		private DataTable MakeTableStructure()
		{
			DataTable tbl = new DataTable();
			tbl.Columns.Add("ColumnID");
			tbl.Columns.Add("ColumnName");
			tbl.Columns.Add("Sequence");
			tbl.Columns.Add("Width");
			return tbl;
		}
		#endregion

		private void btnSaveReport_Click(object sender, System.EventArgs e)
		{
			this.lblErr.Text = "";
			DataView vView = new DataView();
			DataTable dtbResult = new DataTable();
			DataTable dtbColumn = clsHREmpList.GetColumnByFunction("",strLanguage);
			try
			{
				#region Thao tac voi giao dien cua form
				//Lay tra tri tra ve tu cac hidden
				string strSelectColumn = this.txtSelectColumn.Value.Trim();//Giu nguyen
				string strDisplayColumn = this.txtDisplayColumn.Value.Trim();
				string strConditionCode = this.txtConditionCode.Value.Trim();
				string strConditionView = this.txtConditionView.Value.Trim();
				string strSortCondition = this.txtSortCondition.Value.Trim();
				string strAllGridColumn = this.txtAllGridColumn.Value.Trim();
				string strDisplaySequence = "";
				string strDisplayWidth = "";
				//tao bang tam luu nhung phan tu dang chon
				tblTemp = MakeTableStructure();
				DataRow rowtemp;
				string[] Arr1 = strAllGridColumn.Split('@');
				for (int i = 0; i<Arr1.Length; i++)
				{
					if (Arr1[i].ToString().Trim() != "")
					{
						rowtemp = tblTemp.NewRow();
						rowtemp["ColumnID"] = Arr1[i].ToString().Trim();
						rowtemp["ColumnName"] = Arr1[i + 2].ToString().Trim();
						rowtemp["Sequence"] = Arr1[i + 4].ToString().Trim();
						rowtemp["Width"] = Arr1[i + 6].ToString().Trim();
						tblTemp.Rows.Add(rowtemp);
					}
					i = i + 7;
				}
				//Lay ra thu tu hien thi cua cac phan tu
				DataRow[] rowArr = tblTemp.Select("not isnull(Sequence,'')=''");
				if (rowArr.Length > 0)
				{
					DataTable tbl = MakeTableStructure();
					tbl.Columns["Sequence"].DataType = System.Type.GetType("System.Double");
					DataRow drTemp;
					for (int i=0; i<rowArr.Length; i++)
					{
						drTemp = tbl.NewRow();
						drTemp["ColumnID"] = rowArr[i]["ColumnID"].ToString().Trim();
						drTemp["ColumnName"] = rowArr[i]["ColumnName"].ToString().Trim();
						drTemp["Sequence"] = rowArr[i]["Sequence"].ToString().Trim();
						drTemp["Width"] = rowArr[i]["Width"].ToString().Trim();
						tbl.Rows.Add(drTemp);
					}
					vView = tbl.DefaultView;
					vView.Sort = "Sequence";
					tbl.Dispose();
				}
				strDisplayColumn = "";
				for(int i=0; i<vView.Count; i++)
				{
					if (strDisplayColumn == "")
					{
						strDisplayColumn = clsHREmpList.GetListColumnByID(vView[i]["ColumnID"].ToString().Trim()) + " ''" 
							+ vView[i]["ColumnName"].ToString().Trim() + "''";
						strDisplaySequence = vView[i]["Sequence"].ToString().Trim();
						strDisplayWidth = vView[i]["Width"].ToString().Trim();
					}
					else
					{
						strDisplayColumn = strDisplayColumn + "," + clsHREmpList.GetListColumnByID(vView[i]["ColumnID"].ToString().Trim())
							+ " ''" + vView[i]["ColumnName"].ToString().Trim() + "''";
						strDisplaySequence = strDisplaySequence + "," + vView[i]["Sequence"].ToString().Trim();
						strDisplayWidth = strDisplayWidth + "," + vView[i]["Width"].ToString().Trim();
					}
				}
				//Tao dieu kien loc
				Arr1 = strConditionView.Split('@');
				string[] Arr2 = strConditionCode.Split('@');
				strConditionCode = "";
				this.lstCondition.Items.Clear();
				for (int i=0; i<Arr1.Length; i++)
				{
					if (Arr1[i].Trim() != "")
					{
						if (strConditionCode == "")
							strConditionCode = Arr2[i].ToString().Trim();
						else
							strConditionCode = strConditionCode + " " + Arr2[i].ToString().Trim();
						this.lstCondition.Items.Add(new ListItem(Arr1[i].ToString().Trim(),Arr2[i].ToString().Trim()));
					}
				}
				//tao dieu kien sort
				Arr1 = strSortCondition.Split('@');
				strSortCondition = "";
				for (int i=2; i<Arr1.Length; i++)
				{
					if (Arr1[i].Trim() != "")
					{
						if (strSortCondition == "")
						{
							if (Arr1[i + 4].Trim() == "")
								strSortCondition = clsHREmpList.GetListColumnByID(Arr1[i].ToString().Trim());
							else
								strSortCondition = clsHREmpList.GetListColumnByID(Arr1[i].ToString().Trim()) 
									+ " " + Arr1[i + 4].Trim();
						}
						else
						{
							if (Arr1[i + 4].Trim() == "")
								strSortCondition = strSortCondition + ", " + clsHREmpList.GetListColumnByID(Arr1[i].ToString().Trim());
							else
								strSortCondition = strSortCondition + ", " + clsHREmpList.GetListColumnByID(Arr1[i].ToString().Trim())
									+ " " + Arr1[i + 4].Trim();;
						}
					}
					i = i + 5;
				}
				LoadData(vView,Arr1[4],Arr1[10],Arr1[16],Arr1[22],Arr1[28],Arr1[6],Arr1[12],Arr1[18],Arr1[24],Arr1[30]);
				string strSort1Value = Arr1[4];
				string strSort2Value = Arr1[10];
				string strSort3Value = Arr1[16];
				string strSort4Value = Arr1[22];
				string strSort5Value = Arr1[28];
				//Lay du lieu
				dtbResult = clsHREmpList.GetListDynamic(strSelectColumn, strDisplayColumn, strConditionCode, strDisplaySequence, strSortCondition);
				if (dtbResult == null)
				{
					this.lblErr.Text = "The condition set up is invalid or overload number of selected column. Try a gain!";
					Session.Remove("SearchResult");
				}
				else
					Session["SearchResult"] = dtbResult;
				
				#endregion

				#region Luu cau truy van
				if (IsSave == true)
				{
					
				}
				#endregion
			}
			catch(Exception exp)
			{
				this.lblErr.Text = exp.Message.ToString();
			}
			finally
			{
				vView.Dispose();
				if(dtbResult != null)
					dtbResult.Dispose();
			}
			this.cboModule_SelectedIndexChanged(null,null);
		}

		private void LoadData(DataView vView, string strSort1Value, string strSort2Value,
			string strSort3Value, string strSort4Value, string strSort5Value, string strTitle1,
			string strTitle2, string strTitle3, string strTitle4, string strTitle5)
		{
			//Ket du lieu vao cac combo sort
			DropDownList combo = new DropDownList();
			try
			{
				for (int i=1; i<=5; i++)
				{
					combo = (DropDownList)this.FindControl("cboSort" + i.ToString());
					combo.Items.Clear();
					combo.Items.Add(new ListItem("",""));
					combo.Items[0].Attributes.Add("title","");
					for (int j=0; j<vView.Count; j++)
					{
						combo.Items.Add(new ListItem(vView[j]["ColumnName"].ToString() + " inc",vView[j]["ColumnID"].ToString()));
						combo.Items.Add(new ListItem(vView[j]["ColumnName"].ToString() + " desc",vView[j]["ColumnID"].ToString()));
					}
					combo.DataBind();
					switch(i)
					{
						case 1: 
							SetSelectedForCombo(combo, strSort1Value, strTitle1);
							break;
						case 2: 
							SetSelectedForCombo(combo, strSort2Value, strTitle2);
							break;
						case 3: 
							SetSelectedForCombo(combo, strSort3Value, strTitle3);
							break;
						case 4: 
							SetSelectedForCombo(combo, strSort4Value, strTitle4);
							break;
						case 5: 
							SetSelectedForCombo(combo, strSort5Value, strTitle5);
							break;
					}
				}
			}
			catch(Exception exp)
			{
				this.lblErr.Text = exp.Message.ToString();
			}
			combo.Dispose();
		}

		//Chon phan tu selected cho combo
		private void SetSelectedForCombo(DropDownList combo, string strSelectedText, string strTitle)
		{
			for (int i = 0; i<combo.Items.Count; i++)
			{
				if (combo.Items[i].Text == strSelectedText)
				{
					combo.SelectedIndex = i;
					combo.Attributes.Add("title",strTitle);
					break;
				}
			}
		}

		private void cboModule_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataTable dtb = clsHREmpList.GetFunctionByMdlID(cboModule.SelectedValue.Trim(),strLanguage);
			this.lstFunction.DataSource = dtb;
			this.lstFunction.DataTextField = "TableName";
			this.lstFunction.DataValueField = "TableID";
			this.lstFunction.DataBind();
		}

		private void btnView_Click(object sender, System.EventArgs e)
		{
			IsSave = false;
			btnSaveReport_Click(null,null);
			if (Session["SearchResult"] != null)
				ltlAlert.Text = "OpenFrmResult()";
		}

	}
}
