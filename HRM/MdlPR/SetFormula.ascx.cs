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
	///		Summary description for SetFormula.
	/// </summary>
	public class SetFormula : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Button cmdAdd;
		protected System.Web.UI.WebControls.Button cmdDelete;
		protected System.Web.UI.WebControls.DataGrid dtgListMaster;
		protected System.Web.UI.WebControls.Button cmdSave;
		protected System.Web.UI.WebControls.Button cmdCancel;
		protected System.Web.UI.WebControls.Label cursteplabel;
		protected System.Web.UI.HtmlControls.HtmlTable stepList;
		protected System.Web.UI.HtmlControls.HtmlTable stepAdd;
		protected System.Web.UI.WebControls.DataGrid dtgListFormula;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtNote;
		protected System.Web.UI.WebControls.TextBox txtNameFormula;
		protected System.Web.UI.WebControls.DataGrid dtgSysItem;
		protected System.Web.UI.WebControls.Label lblDescriptionSys;
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label lblDescription;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.TextBox txtSetFormulaID;
		protected System.Web.UI.WebControls.Button cmdGetFor;
		protected System.Web.UI.WebControls.DropDownList cboFormula;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.DataGrid dtgUserItem;
		protected System.Web.UI.WebControls.Button btnSaveC;

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
				BinItemDataSys();
				BinItemDataUser();
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
					btnSaveC.Visible = true;
				}
				else
					btnSaveC.Visible = false;
                
			}
			else
			{
				CurStep = (Step)int.Parse(cursteplabel.Text);
			}
			cmdSave.Attributes.Add("OnClick", " return validform()");
			btnSaveC.Attributes.Add("OnClick", " return validform()");
			cmdDelete.Attributes.Add("OnClick", "return checkdelete()");
		}
		private void LoadDataToEdit(string strID)
		{
			DataRow iRow =  clsPayrollFormula.GetDataMasterByID(strID);
			if(iRow != null)
			{
				txtSetFormulaID.Text = iRow["SetFormulaID"].ToString().Trim();
				txtNameFormula.Text = iRow["NameFormula"].ToString().Trim();
				txtNote.Text = iRow["Note"].ToString().Trim();
				BinItemDataEdit(strID);
			}
		}
		private void BinItemDataEdit(string strID)
		{
			DataTable dt = new DataTable();
			dt = clsDB.GetDataTableHasID("PR_spfrmSetFormula @Activity = 'GetDataDetailByID', @SetFormulaID = "+strID);
			dtgListFormula.DataSource = dt;
			dtgListFormula.DataBind();
			clsDB.LoadDropDownListControl(cboFormula, "sp_GetDataCombo @TableName='PR_tblSetFormula',@Fields='SetFormulaID as [ID],NameFormula as Name'", "ID", "Name",true);

			DataTable dtCbo = new DataTable();
			dtCbo = clsPayrollFormula.GetDataTaxType();
			for (int i = 0; i<this.dtgListFormula.Items.Count; i++)
			{
				DropDownList cboTaxType = (DropDownList)this.dtgListFormula.Items[i].FindControl("cboTaxType");
					
				cboTaxType.Items.Clear();	
				clsCommon.LoadDropDownListControl(cboTaxType,dtCbo,"LSSalaryItemTaxID","NameEN",true);

				string LSSalaryItemTaxID = dtgListFormula.Items[i].Cells[1].Text.Trim().Replace("&nbsp;",""); 
				cboTaxType.SelectedValue = LSSalaryItemTaxID;
			}
		}
		private void BinItemDataAdd()
		{
			DataTable dt = new DataTable();
			dt = clsDB.GetDataTableHasID("PR_spfrmSetFormula @Activity = 'GetSalaryItemDataAdd'");
			dtgListFormula.DataSource = dt;
			dtgListFormula.DataBind();

			clsDB.LoadDropDownListControl(cboFormula, "sp_GetDataCombo @TableName='PR_tblSetFormula',@Fields='SetFormulaID as [ID],NameFormula as Name'", "ID", "Name",true);

			DataTable dtCbo = new DataTable();
			dtCbo = clsPayrollFormula.GetDataTaxType();
			for (int i = 0; i<this.dtgListFormula.Items.Count; i++)
			{
				DropDownList cboTaxType = (DropDownList)this.dtgListFormula.Items[i].FindControl("cboTaxType");
					
				cboTaxType.Items.Clear();	
				clsCommon.LoadDropDownListControl(cboTaxType,dtCbo,"LSSalaryItemTaxID","NameEN",true);

				string LSSalaryItemTaxID = dtgListFormula.Items[i].Cells[1].Text.Trim().Replace("&nbsp;",""); 
				cboTaxType.SelectedValue = LSSalaryItemTaxID;
			}
		}
		private void BinDataFormula()
		{
			DataTable dt = new DataTable();
			dt = clsDB.GetDataTable("PR_spfrmSetFormula @Activity = 'GetDataAllFormula'");
			dtgListMaster.DataSource = dt;
			dtgListMaster.DataBind();
		}
		
		
		private void BinItemDataSys()
		{
			DataTable dt = new DataTable();
			dt = clsDB.GetDataTableHasID("PR_spfrmSetFormula @Activity = 'GetSalaryItemDataSys'");
			dtgSysItem.DataSource = dt;
			dtgSysItem.DataBind();
		}
		private void BinItemDataUser()
		{
			DataTable dt = new DataTable();
			dt = clsDB.GetDataTableHasID("PR_spfrmSetFormula @Activity = 'GetSalaryItemDataUser'");
			dtgUserItem.DataSource = dt;
			dtgUserItem.DataBind();
		}
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
			this.cmdGetFor.Click += new System.EventHandler(this.cmdGetFor_Click);
			this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			this.btnSaveC.Click += new System.EventHandler(this.btnSaveC_Click);
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
			BinItemDataAdd();
		}

		private void cmdSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (CurStep == Step.Add)
				{
					clsPayrollFormula.Save_SetFormula(txtNameFormula.Text, txtNote.Text, dtgListFormula);
				}
				else if (CurStep == Step.Edit)
				{
					clsPayrollFormula.Edit_SetFormula(clsDB.SafeDataInteger(txtSetFormulaID.Text)
						, txtNameFormula.Text, txtNote.Text, dtgListFormula);
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
						clsDB.Exc_CommandText("PR_spfrmSetFormula @Activity = 'Delete',@SetFormulaID="+strID);
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
				Response.Redirect("Editpage.aspx?ModuleID=PR&ParentID=71&FunctionID=581&Ascx=MdlPR/SetFormula.ascx&NotLeft=true&ID="+sID);
			}
		}

		private void cmdGetFor_Click(object sender, System.EventArgs e)
		{
			string strID = cboFormula.SelectedValue;
			DataTable dt = new DataTable();
			dt = clsDB.GetDataTableHasID("PR_spfrmSetFormula @Activity = 'GetDataDetailByID', @SetFormulaID = "+strID);
			dtgListFormula.DataSource = dt;
			dtgListFormula.DataBind();


			DataTable dtCbo = new DataTable();
			dtCbo = clsPayrollFormula.GetDataTaxType();
			for (int i = 0; i<this.dtgListFormula.Items.Count; i++)
			{
				DropDownList cboTaxType = (DropDownList)this.dtgListFormula.Items[i].FindControl("cboTaxType");
					
				cboTaxType.Items.Clear();	
				clsCommon.LoadDropDownListControl(cboTaxType,dtCbo,"LSSalaryItemTaxID","NameEN",true);

				string LSSalaryItemTaxID = dtgListFormula.Items[i].Cells[1].Text.Trim().Replace("&nbsp;",""); 
				cboTaxType.SelectedValue = LSSalaryItemTaxID;
			}
		}

		private void btnSaveC_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (CurStep == Step.Add)
				{
					clsPayrollFormula.Save_SetFormula(txtNameFormula.Text, txtNote.Text, dtgListFormula);
				}
				else if (CurStep == Step.Edit)
				{
					clsPayrollFormula.Edit_SetFormula(clsDB.SafeDataInteger(txtSetFormulaID.Text)
						, txtNameFormula.Text, txtNote.Text, dtgListFormula);
				}
				LoadDataToEdit(Request.Params["id"].ToString());
			}
			catch (Exception ex)
			{
				lblErr.Text = ex.Message.ToString();
			}
		}
	}
}
