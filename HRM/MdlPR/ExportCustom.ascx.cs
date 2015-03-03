namespace MdlPR
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore;
	using iHRPCore.Com;

	/// <summary>
	///		Summary description for ExportCustom.
	/// </summary>
	public class ExportCustom : System.Web.UI.UserControl
	{

		protected System.Web.UI.WebControls.Button cmdAdd;
		protected System.Web.UI.WebControls.Button cmdDelete;
		protected System.Web.UI.WebControls.DataGrid dtgListMaster;
		protected System.Web.UI.WebControls.Button cmdSave;
		protected System.Web.UI.WebControls.Button cmdCancel;
		protected System.Web.UI.WebControls.Label cursteplabel;
		protected System.Web.UI.HtmlControls.HtmlTable stepList;
		protected System.Web.UI.HtmlControls.HtmlTable stepAdd;
		protected System.Web.UI.WebControls.DataGrid dtgList;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtNote;
		protected System.Web.UI.WebControls.TextBox txtNameExport;
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label lblDescription;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.TextBox txtExportCustomID;
		//protected System.Web.UI.WebControls.TextBox txtExportCustomID;
		protected System.Web.UI.WebControls.DataGrid dtgListFormula;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.DropDownList cboOrderby;
		protected System.Web.UI.WebControls.DropDownList cboSort;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.ListBox lstListComp;
		protected System.Web.UI.WebControls.ListBox lstListGroup;
		protected System.Web.UI.WebControls.TextBox txtLstListGroup;
		protected System.Web.UI.WebControls.TextBox txtListLen;
		protected System.Web.UI.WebControls.TextBox txtLstListGroupText;

		enum Step
		{
			List = 0,
			Add, Edit
		};
		private Step CurStep = Step.List;
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if (!this.IsPostBack)
			{
				BinDataFormula();
				
				cursteplabel.Text = ((int)CurStep).ToString();

				//If co id thi` do' la` chinh sua
				if (clsDB.SafeDataInteger(Request.Params["id"]).ToString() != "0")
				{
					LeaveStep(CurStep);
					if (CurStep == Step.List)
						CurStep = Step.Edit;
					EnterStep(CurStep);
					cursteplabel.Text = ((int)CurStep).ToString();
					LoadDataToEdit(Request.Params["id"].ToString());
				}
                
			}
			else
			{
				CurStep = (Step)int.Parse(cursteplabel.Text);
			}
			cmdSave.Attributes.Add("OnClick", " return validform()");
			cmdDelete.Attributes.Add("OnClick", "return checkdelete()");
		}
		#region Bin load Data
		private void LoadDataToEdit(string strID)
		{
			DataRow iRow =  clsPRCustomExport.GetDataMasterByID(strID);
			if(iRow != null)
			{
				txtExportCustomID.Text = iRow["ExportCustomID"].ToString().Trim();
				txtNameExport.Text = iRow["NameExport"].ToString().Trim();
				txtNote.Text = iRow["Note"].ToString().Trim();
				//BinItemDataEdit(strID);
				BindUserListEdit(strID);
				BindUserGroupEdit(strID);
			}
		}
		private void BinItemDataEdit(string strID)
		{
			DataTable dt = new DataTable();
			dt = clsDB.GetDataTableHasID("PR_spfrmExportCustom @Activity = 'GetDataDetailByID', @ExportCustomID = "+strID);
			dtgList.DataSource = dt;
			dtgList.DataBind();

			for (int i = 0; i<this.dtgList.Items.Count; i++)
			{
				CheckBox chkVisible = (CheckBox)this.dtgList.Items[i].FindControl("chkVisible");
				string sVisible = dtgList.Items[i].Cells[1].Text.Trim(); 
				chkVisible.Checked = Convert.ToBoolean(sVisible);
			}
		}
		private void BinDataFormula()
		{
			DataTable dt = new DataTable();
			dt = clsDB.GetDataTable("PR_spfrmExportCustom @Activity = 'GetDataAllExport'");
			dtgListMaster.DataSource = dt;
			dtgListMaster.DataBind();
		}
		private void BinItemDataAdd()
		{
			DataTable dt = new DataTable();
			dt = clsDB.GetDataTableHasID("PR_spfrmExportCustom @Activity = 'GetSalaryItemDataAdd'");
			dtgList.DataSource = dt;
			dtgList.DataBind();
		}
		
		#endregion
		#region Step
		private void LeaveStep(Step step)
		{
			switch (step)
			{
				case Step.List:
					stepList.Visible = false;
					break;
				case Step.Add:
					stepAdd.Visible = false;
					break;
				case Step.Edit:
					stepAdd.Visible = false;
					break;
			}
		}

		private void EnterStep(Step step)
		{
			switch (step)
			{
				case Step.List:
					stepList.Visible = true;
					break;
				case Step.Add:
					stepAdd.Visible = true;
					break;
				case Step.Edit:
					stepAdd.Visible = true;
					break;
			}
		}
		#endregion
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
			this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
			this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
			this.dtgListMaster.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgListMaster_ItemCommand);
			this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void cmdAdd_Click(object sender, System.EventArgs e)
		{
			LeaveStep(CurStep);
			if (CurStep == Step.List)
				CurStep = Step.Add;
			EnterStep(CurStep);
			cursteplabel.Text = ((int)CurStep).ToString();
			//BinItemDataAdd();
			BindUserListAdd();
			BindUserGroupAdd();
		}
		private void BindUserListAdd()
		{
			string strSQL = "PR_spfrmExportCustom @Activity = 'GetSalaryItemDataAdd'";

			DataTable dt = clsCommon.GetDataTable(strSQL);
			clsCommon.LoadListBoxControl(lstListComp,dt,"Code","Name",false);					
		}
		private void BindUserGroupAdd()
		{
			//string strSQL = "PR_spfrmExportCustom @Activity = 'GetSalaryItemDataAdd'";

			//DataTable dt = clsCommon.GetDataTable(strSQL);
			DataTable dt = new DataTable();
			clsCommon.LoadListBoxControl(lstListGroup,dt,"ID","Level1Name",false);
			txtListLen.Text=lstListGroup.Items.Count.ToString();
		}
		private void BindUserListEdit(string ID)
		{
			string strSQL = "PR_spfrmExportCustom @Activity='GetFieldIDNP'";
			strSQL +=",@ExportCustomID ="+ID;

			DataTable dt = clsCommon.GetDataTable(strSQL);
			clsCommon.LoadListBoxControl(lstListComp,dt,"Code","Name",false);					
		}
		private void BindUserGroupEdit(string ID)
		{
			string strSQL = "PR_spfrmExportCustom @Activity='GetFieldIDP'";
			strSQL +=",@ExportCustomID ="+ID;

			DataTable dt = clsCommon.GetDataTable(strSQL);
			clsCommon.LoadListBoxControl(lstListGroup,dt,"Code","Name",false);
			txtListLen.Text=lstListGroup.Items.Count.ToString();
		}

		private void cmdSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				string[] sList=txtLstListGroup.Text.Split(',');
				string[] sListText=txtLstListGroupText.Text.Split(',');

				if (CurStep == Step.Add)
				{
					clsPRCustomExport.Save_ExportCustom1(txtNameExport.Text, txtNote.Text, sList,sListText);
				}
				else if (CurStep == Step.Edit)
				{
					clsPRCustomExport.Edit_ExportCustom1(clsDB.SafeDataInteger(txtExportCustomID.Text)
						, txtNameExport.Text, txtNote.Text, sList,sListText);
				}
				LeaveStep(CurStep);
				EnterStep(0);
				cursteplabel.Text = "0";
				BinDataFormula();
			}
			catch (Exception ex)
			{
				lblErr.Text = ex.Message.ToString();
			}
		}

		private void cmdCancel_Click(object sender, System.EventArgs e)
		{
			LeaveStep(CurStep);
			EnterStep(0);
			cursteplabel.Text = "0";
		}

		private void cmdDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				string strID="";
				for(int i=0; i<dtgListMaster.Items.Count; i++)
				{
					if(((CheckBox)dtgListMaster.Items[i].FindControl("chkSelect")).Checked==true)
					{
						strID = dtgListMaster.Items[i].Cells[0].Text.Trim();
						clsDB.Exc_CommandText("PR_spfrmExportCustom @Activity = 'Delete',@ExportCustomID="+strID);
					}
				}
				
				BinDataFormula();
			}
			catch(Exception ex)
			{
				lblErr.Text = ex.Message;
			}
		}

		private void dtgListMaster_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if(e.CommandName.Trim().ToUpper() =="EDIT")
			{
				string sID = dtgListMaster.Items[e.Item.ItemIndex].Cells[0].Text.Trim();
				Response.Redirect("Editpage.aspx?ModuleID=PR&ParentID=90&FunctionID=1000&Ascx=MdlPR/ExportCustom.ascx&ID="+sID);
			}
		}
	}
}
