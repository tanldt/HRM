namespace iHRPCore.MdlHR
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.Component;
	using iHRPCore.Include;
	using iHRPCore.Com;
	using iHRPCore.HRComponent;

	/// <summary>
	///		Summary description for Termination.
	/// </summary>
	public class Termination : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.LinkButton btnSaveTurnOver;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.LinkButton btnTurnOver;
		protected System.Web.UI.WebControls.LinkButton btnList;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.TextBox txtSignDate;
		protected System.Web.UI.WebControls.TextBox txtDecisionNo;
		protected System.Web.UI.WebControls.TextBox txtLastWorkingDate;
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.HtmlControls.HtmlTableRow trTurnOver;
		protected System.Web.UI.WebControls.TextBox txtSignerPosition;
		protected System.Web.UI.WebControls.Label lblTerminationTypeID;
		protected System.Web.UI.WebControls.Label lblReason;
		protected System.Web.UI.WebControls.TextBox txtReason;
		protected System.Web.UI.WebControls.Label lblNotificationDate;
		protected System.Web.UI.WebControls.Label lblLastWorkingDate;
		protected System.Web.UI.WebControls.Label lblLastSalDate;
		protected System.Web.UI.WebControls.Label lblDecision;
		protected System.Web.UI.WebControls.Label lblSignDate;
		protected System.Web.UI.WebControls.Label lblSigner;
		protected System.Web.UI.WebControls.TextBox txtSigner;
		protected System.Web.UI.WebControls.Label lblSignerPosition;
		protected System.Web.UI.WebControls.Label lblNote;
		protected System.Web.UI.WebControls.Label lblInsuranceInfoDate;
		protected System.Web.UI.WebControls.TextBox txtInsuranceInfoDate;
		protected System.Web.UI.WebControls.TextBox txtInformDate;
		protected System.Web.UI.WebControls.TextBox txtLastSalDate;
		protected System.Web.UI.WebControls.DropDownList cboLSTerminationTypeID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtTerminationID;
		protected System.Web.UI.WebControls.TextBox txtActive;
		protected System.Web.UI.WebControls.TextBox txtCreater;
		protected System.Web.UI.WebControls.TextBox txtNote;
		protected System.Web.UI.WebControls.CheckBox chkKeepEmpID;
		protected System.Web.UI.WebControls.TextBox txtDateReInstate;
		protected System.Web.UI.WebControls.TextBox txtIsReInstate;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtReturnInsCardDate;
		protected System.Web.UI.WebControls.TextBox txtFromDateMax;
		protected System.Web.UI.WebControls.LinkButton Linkbutton1;
		public string strLanguage="VN";
		private void Page_Load(object sender, System.EventArgs e)
		{
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";			
			if (! Page.IsPostBack)
			{
				LoadDataCombo();				
				AllowSaveCommand(true);
				Session["ssStatusTermination"]="AddNew";
				LoadTerInfo();
			}
			btnSave.Attributes.Add("onclick","return CheckSave();");						
			btnDelete.Attributes.Add("onclick","return CheckDelete();");
			btnTurnOver.Attributes.Add("onclick","return Reinstate();");
			btnSaveTurnOver.Attributes.Add("onclick","return SaveTurnOver();");
			//this.lnkEmpInfo.Attributes.Add("OnClick","return OpenNewWindow()");
		}	
		private void LoadTerInfo()
		{
			try
			{
				DataTable dtData= clsTermination.LoadTerminationByEmpID(Session["EmpID"]);
				
				if (dtData.Rows.Count>0)
				{
					txtFromDateMax.Text = dtData.Rows[0]["FromDateMax"].ToString();
					if (dtData.Rows[0]["Active"].ToString() == "False")
					{
						txtTerminationID.Value = dtData.Rows[0]["TerminationID"].ToString();					
						txtLastWorkingDate.Text = dtData.Rows[0]["LastWorkingDate"].ToString();
						txtReason.Text = dtData.Rows[0]["Reason"].ToString();
						cboLSTerminationTypeID.SelectedValue = dtData.Rows[0]["LSTerminationTypeID"].ToString();
						txtInformDate.Text = dtData.Rows[0]["InformDate"].ToString();
						txtLastSalDate.Text = dtData.Rows[0]["LastSalDate"].ToString();
						txtDecisionNo.Text = dtData.Rows[0]["DecisionNo"].ToString();
						txtSignDate.Text = dtData.Rows[0]["SignDate"].ToString();
						txtSigner.Text = dtData.Rows[0]["Signer"].ToString();
						txtSignerPosition.Text = dtData.Rows[0]["SignerPosition"].ToString();
						txtInsuranceInfoDate.Text = dtData.Rows[0]["InsuranceInfoDate"].ToString();
					
						//txtIsReInstate.Text = dtData.Rows[0]["IsReInstate"].ToString();
						chkKeepEmpID.Checked = dtData.Rows[0]["KeepEmpID"].ToString()=="True"?true:false;
						//txtNewEmpID.Text = dtData.Rows[0]["NewEmpID"].ToString();
						txtDateReInstate.Text = dtData.Rows[0]["DateReInstate"].ToString();
						//ChkIsInsNotice.Text = dtData.Rows[0]["IsInsNotice"].ToString();
						txtNote.Text = dtData.Rows[0]["Note"].ToString();
						txtReturnInsCardDate.Text = dtData.Rows[0]["ReturnInsCardDate"].ToString();
					
						Session["ssStatusTermination"]="Edit";	
					}
				}
				else
				{
					Session["ssStatusTermination"]="AddNew";
				}
			}
			catch(Exception ex)
			{
				clsChangeLang.popupWindowExp(this.Parent,ex);
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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnList.Click += new System.EventHandler(this.btnList_Click);
			this.btnTurnOver.Click += new System.EventHandler(this.btnTurnOver_Click);
			this.btnSaveTurnOver.Click += new System.EventHandler(this.btnSaveTurnOver_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		private void LoadDataCombo()
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			clsCommon.LoadDropDownListControl(cboLSTerminationTypeID,"sp_GetDataCombo @TableName='LS_tblTERMINATIONTYPE',@Fields='LSTERMINATIONTYPEID as [ID]," + strTextField + " as Name'","ID","Name",true);
		}
		private void btnTurnOver_Click(object sender, System.EventArgs e)
		{
			StartReInstateEmployee(this.btnTurnOver);
		}
		private void btnList_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(Request.ApplicationPath.Trim() + "/Editpage.aspx?ModuleID=TER&ParentID=136&FunctionID=137&Ascx=MdlTER/TERList.ascx&IsTermination=1");
		}
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{				
				//clsCommonOra.DeleteListRecord("HR_PKG.HR_SPFRMTERMINATION","LASTWORKINGDATE",OracleType.NVarChar,12,strItemToDeleteID,clsstrEmpID);
				clsCommon.DeleteListRecord("TER_SPFRMTERMINATION","TerminationID",SqlDbType.NVarChar,12,txtTerminationID.Value + '$');
				clsCommon.ClearControlValue(this);
				Session["ssStatusTermination"]="AddNew";
			}
			catch(Exception ex)
			{
				clsChangeLang.popupWindowExp(this.Parent,ex);
			}
		}
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				string sAccountLogin= Session["AccountLogin"]!=null? Session["AccountLogin"].ToString():"admin";				
				this.txtCreater.Text = sAccountLogin;
				string sErr="";

				if(Session["ssStatusTermination"].ToString().Trim()=="AddNew")
					
					sErr = clsCommon.sImpactDB(Session["EmpID"].ToString().Trim(),"Save",this,"TER_SPFRMTERMINATION");
				else
				{					
					sErr = clsCommon.sUpdateByKey("TerminationID",txtTerminationID.Value.Trim(),"Update",this,"TER_SPFRMTERMINATION");					
				}
				if (sErr.Length<=12)
				{
					clsChangeLang.popupWindow(this.Parent,"0044",strLanguage,"",1);	
					txtTerminationID.Value=sErr;
					Session["ssStatusTermination"]="Edit";	
					txtActive.Text="1";
				}
				else
				{
					clsChangeLang.popupWindowCataLog(this.Parent,sErr);
				}				
				LoadTerInfo();
			}
			catch(Exception ex)
			{
				clsChangeLang.popupWindowExp(this.Parent,ex);
			}			
		}
		private void btnSaveTurnOver_Click(object sender, System.EventArgs e)
		{
			string sErr="";
			if(this.chkKeepEmpID.Checked == true)
			{
				sErr=clsTermination.sReinstate(Session["EmpID"].ToString().Trim(), (this.chkKeepEmpID.Checked == true ? "True" : "False"), this.txtDateReInstate.Text);
				/*clsTER.Reinstate(m_strEmpID, (chkKeepOldEmpID.Checked?"1":"0"), txtTurnOverDate.Text.Trim());
				this.btnSaveTurnOver.Enabled = false;*/
				if (sErr=="")
				{
					Session["ssStatusTermination"]="AddNew";
					AllowSaveCommand(true);
					clsCommon.ClearControlValue(this);
					txtActive.Text="0";
				}
				else
				{
					clsChangeLang.popupWindowCataLog(this.Parent,sErr);
				}
			}
			else
			{
				//Tao Ma NV moi				
				clsCommon.OpenNewWindowPopup(this.Page,"FormPage.aspx?ModuleID=HR&ParentID=8&FunctionID=204&Ascx=MdlHR/BuildEmpCode.ascx&action=edit&IsTermination=1&empid=" + Session["EmpID"].ToString() + "&StartDate=" + txtDateReInstate.Text,"");	
			}
		}
		private void AllowSaveCommand(bool isSavable)
		{			
			if (clsTermination.checkEmpTermination(Session["EmpID"]))
			{
				txtActive.Text="0";
				cboLSTerminationTypeID.Enabled=true;
				txtInformDate.Enabled=true;
				txtLastWorkingDate.Enabled=true;
				txtLastSalDate.Enabled=true;
			}
			else
			{
				txtActive.Text="1";			
				cboLSTerminationTypeID.Enabled=false;
				txtInformDate.Enabled=false;
				txtLastWorkingDate.Enabled=false;
				txtLastSalDate.Enabled=false;
				
			}
		}
		private void StartReInstateEmployee(LinkButton aLinkButton)
		{
			if(this.chkKeepEmpID.Checked == true)
			{
				//Giu lai Ma NV
				
			}
			else
			{
				//Tao Ma NV moi
				aLinkButton.Attributes.Add("onclick","return OpenEditCode()");
			}
		}

	}
}
