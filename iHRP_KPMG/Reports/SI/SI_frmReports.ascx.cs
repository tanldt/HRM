namespace iHRPCore.Reports.SI
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.HRComponent;
	using iHRPCore.SIComponent;
	using System.Configuration; 
	using Web_DM.Component;
	using System.Collections;  
	using ExcelReportDll;
	using iHRPCore.Com;
	using System.Web.UI;   
	using iHRPCore.APPComponent;
	using System.IO;

	/// <summary>
	///		Summary description for SI_frmReports.
	/// </summary>
	public class SI_frmReports : System.Web.UI.UserControl
	{
		#region Declare
		protected System.Web.UI.HtmlControls.HtmlTable Table2;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.HtmlControls.HtmlTableRow trC47;
		protected System.Web.UI.HtmlControls.HtmlTableRow trC47a;
		protected System.Web.UI.HtmlControls.HtmlTableRow trC45;
		protected System.Web.UI.HtmlControls.HtmlTableRow trC02SBH;
		protected System.Web.UI.HtmlControls.HtmlTableRow trToKhaiBH;
		protected System.Web.UI.WebControls.LinkButton btnPrint;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.HtmlControls.HtmlTableRow trC45c;
		protected System.Web.UI.WebControls.Label lblFromDate;
		protected System.Web.UI.WebControls.TextBox txtFromDate;
		protected System.Web.UI.WebControls.Label lblToDate;
		protected System.Web.UI.WebControls.DropDownList cboLocation;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.TextBox txtToDate;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel2ID;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.TextBox txtFromDate2;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.TextBox txtToDate2;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.DropDownList cboLevel1_1;
		protected System.Web.UI.WebControls.DropDownList cboLevel2_1;
		protected System.Web.UI.WebControls.DropDownList cboLevel1;
		protected System.Web.UI.WebControls.Label Label14;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.Label Label19;
		protected System.Web.UI.HtmlControls.HtmlTableRow trSalProcess;
		protected System.Web.UI.WebControls.TextBox txtYear1;
		protected System.Web.UI.WebControls.Label Label20;
		protected System.Web.UI.WebControls.DropDownList cboLocation1;
		protected System.Web.UI.HtmlControls.HtmlTableRow trC04BH;
		protected System.Web.UI.HtmlControls.HtmlTableRow trC04BH_NDS;
		protected System.Web.UI.WebControls.TextBox txtFromDate3;
		protected System.Web.UI.WebControls.TextBox txtToDate3;
		protected System.Web.UI.HtmlControls.HtmlTableRow trSIProcess;
		protected System.Web.UI.WebControls.Label Label18;
		protected System.Web.UI.WebControls.Label Label21;
		protected System.Web.UI.WebControls.TextBox txtEmpID1;
		protected System.Web.UI.WebControls.TextBox txtEmpID2;
		protected System.Web.UI.WebControls.Label Label22;
		protected System.Web.UI.WebControls.TextBox txtYear4;
		protected System.Web.UI.WebControls.Label Label23;
		protected System.Web.UI.HtmlControls.HtmlTableRow trRegisterLB;
		protected System.Web.UI.WebControls.TextBox txtQuarter4;
		protected System.Web.UI.WebControls.TextBox txtYear5;
		protected System.Web.UI.WebControls.Label Label24;
		protected System.Web.UI.WebControls.Label Label25;
		protected System.Web.UI.HtmlControls.HtmlTableRow trStateOfLabour;
		protected System.Web.UI.WebControls.Label Label26;
		protected System.Web.UI.HtmlControls.HtmlInputRadioButton optStage1;
		protected System.Web.UI.HtmlControls.HtmlInputRadioButton optStage0;
		protected System.Web.UI.WebControls.Label Label27;
		protected System.Web.UI.HtmlControls.HtmlTableRow trLeaveRecordByDepartment;
		protected System.Web.UI.HtmlControls.HtmlInputHidden remainfield;
		protected System.Web.UI.WebControls.Label Label28;
		protected System.Web.UI.HtmlControls.HtmlInputRadioButton optLag0;
		protected System.Web.UI.HtmlControls.HtmlInputRadioButton optLag1;
		protected System.Web.UI.WebControls.TextBox txtYear6;
		protected System.Web.UI.WebControls.Label Label29;
		protected System.Web.UI.WebControls.TextBox txtQuarter;
		protected System.Web.UI.WebControls.RadioButtonList optIsType;
		protected System.Web.UI.WebControls.TextBox txtYear7;
		protected System.Web.UI.WebControls.Label Lbl16;
		protected System.Web.UI.WebControls.Label lbl17;
		protected System.Web.UI.WebControls.TextBox txtYear;
		protected System.Web.UI.WebControls.TextBox txtQuarter2;
		protected System.Web.UI.WebControls.Label Label16;
		protected System.Web.UI.WebControls.Label Label17;
		protected System.Web.UI.WebControls.Label Label30;
		protected System.Web.UI.WebControls.TextBox txtEmpID8;
		protected System.Web.UI.WebControls.TextBox txtEmpID;
		protected System.Web.UI.WebControls.Label Label31;
		protected System.Web.UI.HtmlControls.HtmlTableRow trEmpRegisterLB;
		protected System.Web.UI.WebControls.Label Label32;
		protected System.Web.UI.WebControls.Label Label33;
		protected System.Web.UI.WebControls.TextBox txtByOther;
		protected System.Web.UI.WebControls.TextBox txtBySelf;
		protected System.Web.UI.WebControls.Label Label34;
		protected System.Web.UI.WebControls.Label Label35;
		protected System.Web.UI.WebControls.Label Label36;
		protected System.Web.UI.WebControls.TextBox txtFromDate4;
		protected System.Web.UI.WebControls.Label Label37;
		protected System.Web.UI.WebControls.TextBox txtToDate4;
		protected System.Web.UI.WebControls.RadioButtonList optIsType4;
		protected System.Web.UI.WebControls.DropDownList cboCompany1;
		protected System.Web.UI.HtmlControls.HtmlTableRow trCompany;
		protected System.Web.UI.HtmlControls.HtmlTableRow trOther;
		protected System.Web.UI.WebControls.Label Label38;
		protected System.Web.UI.WebControls.TextBox txtYear3;
		protected System.Web.UI.WebControls.TextBox txtQuarter3;
		protected System.Web.UI.WebControls.TextBox txtMonth3;
		protected System.Web.UI.WebControls.Label Label41;
		protected System.Web.UI.HtmlControls.HtmlTableRow trC47b;
		protected System.Web.UI.WebControls.DropDownList cboCompany3;
		protected System.Web.UI.WebControls.Label Label45;
		protected System.Web.UI.WebControls.Label Label46;
		protected System.Web.UI.WebControls.TextBox txtToDate5;
		protected System.Web.UI.WebControls.DropDownList cboCompany5;
		protected System.Web.UI.WebControls.Label Label47;
		protected System.Web.UI.WebControls.Label Label48;
		protected System.Web.UI.WebControls.TextBox txtFromDate6;
		protected System.Web.UI.WebControls.Label Label49;
		protected System.Web.UI.WebControls.TextBox txtToDate6;
		protected System.Web.UI.WebControls.DropDownList cboCompany6;
		protected System.Web.UI.WebControls.DropDownList cboCompany;
		protected System.Web.UI.WebControls.Label Label42;
		protected System.Web.UI.WebControls.Label Label43;
		protected System.Web.UI.WebControls.DropDownList cboGroup3;
		protected System.Web.UI.HtmlControls.HtmlTableRow trC46;
		protected System.Web.UI.WebControls.Label Label44;
		protected System.Web.UI.WebControls.TextBox txtFromMonth4;
		protected System.Web.UI.WebControls.TextBox txtToMonth4;
		protected System.Web.UI.WebControls.DropDownList cboLSCompanyID3;
		protected System.Web.UI.WebControls.DropDownList cboLSCompanyID8;
		protected System.Web.UI.WebControls.Label Label56;
		protected System.Web.UI.WebControls.Label Label58;
		protected System.Web.UI.WebControls.Label Label59;
		protected System.Web.UI.WebControls.TextBox txtMonth7;
		protected System.Web.UI.WebControls.TextBox txtStage7;
		protected System.Web.UI.WebControls.TextBox txtMonth9;
		protected System.Web.UI.WebControls.TextBox txtSumEmpPrev7;
		protected System.Web.UI.WebControls.TextBox txtWageFundPrev7;
		protected System.Web.UI.WebControls.DropDownList cboLSCompanyID7;
		protected System.Web.UI.WebControls.Label Label60;
		protected System.Web.UI.WebControls.Label Label61;
		protected System.Web.UI.WebControls.TextBox txtYear8;
		protected System.Web.UI.WebControls.Label Label68;
		protected System.Web.UI.HtmlControls.HtmlTableRow C04BH_NDS;
		protected System.Web.UI.WebControls.Label Label66;
		protected System.Web.UI.WebControls.TextBox txtFromDate10;
		protected System.Web.UI.WebControls.TextBox txtToDate10;
		protected System.Web.UI.WebControls.Label Label62;
		protected System.Web.UI.WebControls.DropDownList cboLSCompanyID10;
		protected System.Web.UI.WebControls.DropDownList cboLSLevel1ID10;
		protected System.Web.UI.WebControls.DropDownList cboLSLevel2ID10;
		#endregion Declare

		public string strLanguage = "EN";
		public string strUserName = "Admin";
		protected System.Web.UI.WebControls.Label Label40;
		protected System.Web.UI.WebControls.Label Label39;
		protected System.Web.UI.WebControls.TextBox txtMonth2;
		protected System.Web.UI.WebControls.DropDownList cboLSCompanyID9;
		protected System.Web.UI.WebControls.Label Label67;
		protected System.Web.UI.WebControls.Label Label63;
		protected System.Web.UI.WebControls.DropDownList cboLSCompanyID2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel3ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtLevel1ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdbtnText;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdCopy;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hd131;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Hidden1;
		protected System.Web.UI.WebControls.DropDownList cboLSLevel3ID10;
		protected System.Web.UI.WebControls.TextBox txtEmpCode;
		protected System.Web.UI.WebControls.TextBox txtToDate8;
		protected System.Web.UI.WebControls.Label lblToDate8;
		protected System.Web.UI.WebControls.TextBox txtQuarter8;
		protected System.Web.UI.WebControls.TextBox txtDelayFine4;
		protected System.Web.UI.WebControls.TextBox txtTransferLack4;
		protected System.Web.UI.WebControls.TextBox txtTransferExcess4;
		protected System.Web.UI.WebControls.TextBox txtFund4;
		protected System.Web.UI.WebControls.DropDownList cboLSCompanyID4;
		protected System.Web.UI.WebControls.Label Label51;
		protected System.Web.UI.WebControls.Label Label54;
		protected System.Web.UI.WebControls.Label Label57;
		protected System.Web.UI.WebControls.Label Label52;
		protected System.Web.UI.WebControls.Label Label64;
		protected System.Web.UI.WebControls.Label Label55;
		protected System.Web.UI.WebControls.Label Label50;
		protected System.Web.UI.WebControls.Label Label53;
		protected System.Web.UI.WebControls.Label Label65;
		protected System.Web.UI.WebControls.TextBox txtApplyAmount4;
		protected System.Web.UI.WebControls.Label Label70;
		protected System.Web.UI.WebControls.Label Label71;
		protected System.Web.UI.WebControls.TextBox txtNotYetAmount8;
		protected System.Web.UI.WebControls.Label lblNotYetAmount8;
		protected System.Web.UI.WebControls.TextBox txtApplyAmount8;
		protected System.Web.UI.WebControls.Label Label69;
		protected System.Web.UI.WebControls.LinkButton btnExcel;
		protected System.Web.UI.WebControls.Label lblPositionSigner;
		protected System.Web.UI.WebControls.TextBox txtPositionSigner;
		protected System.Web.UI.WebControls.Label lblSigner;
		protected System.Web.UI.WebControls.TextBox txtSignerBH;
		protected System.Web.UI.WebControls.Label lblPosSigner;
		protected System.Web.UI.WebControls.TextBox txtPosSigner;
		protected System.Web.UI.WebControls.Label lblSigner2;
		protected System.Web.UI.WebControls.TextBox txtSigner2;
		protected System.Web.UI.WebControls.Label lblPosSign;
		protected System.Web.UI.WebControls.TextBox txtPosSigner1;
		protected System.Web.UI.WebControls.Label Label73;
		protected System.Web.UI.WebControls.TextBox txtSigner3;
		protected System.Web.UI.WebControls.TextBox txtSigner;
		protected System.Web.UI.WebControls.Label Label72;
		protected System.Web.UI.WebControls.DropDownList cboLSLevel1ID8;
		
		string strTableName = "";
		private void Page_Load(object sender, System.EventArgs e)
		{
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"VN";
			strUserName = Session["AccountLogin"] != null?Session["AccountLogin"].ToString().Trim():"Admin";
			string str_Script="";
			try
			{
				if(Request.Params["tabid"] != null)
				{					
					strTableName = Request.Params["tabid"].ToString().Trim();
					//Edit date : 
					//truong hop report khong co params
					DataSet ds = new DataSet();
					ds.ReadXml(Server.MapPath(".") +  ConfigurationSettings.AppSettings["pstrPathReportsXML"]);
					DataView dv  = new DataView(ds.Tables[strTableName]);
					if(dv.Count > 1)
						// End
					{
						str_Script = clsCatalog.CreateControlsReport(strTableName,null, Server.MapPath(".") +  ConfigurationSettings.AppSettings["pstrPathReportsXML"],new DataGrid(),null,null,null,new Label(),null,null);
						if(str_Script.Trim()!="") Response.Write(str_Script);
						btnPrint.Attributes.Add("OnClick", " return validform()");
					}
					if(strTableName.Equals("Tokhai"))
					{
						btnExcel.Visible=false;
					}
				}
			}
			catch(Exception ex)
			{
			}
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
			if (!Page.IsPostBack)
			{
				LoadComboBox();
				LoadDefaultValue();
				if (Request.Params["tabid"] != null)
				{
					string strTabID = Request.Params["tabid"].ToString().Trim();
					if (strTabID == "C47b")
						DisplayRow(trC47b);
					else if (strTabID == "C47")
						DisplayRow(trC47);
					else if (strTabID == "C47a")
						DisplayRow(trC47a);
					else if (strTabID == "C45")
						DisplayRow(trC45);
					else if (strTabID == "C45c")
						DisplayRow(trC45c);
					else if (strTabID == "C46")
						DisplayRow(trC46);
					else if (strTabID == "C02SBH")
						DisplayRow(trC02SBH);
					else if (strTabID == "Tokhai")
						DisplayRow(trToKhaiBH);
					else if (strTabID == "SalProcess")
						DisplayRow(trSalProcess);
					else if (strTabID == "C04BH")
						DisplayRow(trC04BH);
					else if (strTabID == "C04BH_NDS")
						DisplayRow(trC04BH_NDS);
					else if (strTabID == "SIProcess")
						DisplayRow(trSIProcess);
					else if (strTabID == "RegisterLB")
						DisplayRow(trRegisterLB);
					else if (strTabID == "StateOfLabour")
						DisplayRow(trStateOfLabour);
					else if (strTabID == "LeaveRecordByDepartment")
						DisplayRow(trLeaveRecordByDepartment);
					else if (strTabID == "EmpRegisterLB")
						DisplayRow(trEmpRegisterLB);
					else
						DisplayRow(trOther);
				}
			}
			btnPrint.Attributes.Add("OnClick", "return CheckReport()");
			cboLSCompanyID10_SelectedIndexChanged(null,null);
		}

		private void LoadDefaultValue()
		{
			txtMonth2.Text = DateTime.Today.ToString("MM/yyyy");
			txtMonth3.Text = "";
			txtMonth7.Text = DateTime.Today.ToString("MM/yyyy");
			txtMonth9.Text = DateTime.Today.ToString("MM/yyyy");
			txtYear.Text = DateTime.Today.ToString("yyyy");
			txtYear1.Text = DateTime.Today.ToString("yyyy");
			txtYear3.Text = DateTime.Today.ToString("yyyy");
			txtYear4.Text = DateTime.Today.ToString("yyyy");
			txtYear5.Text = DateTime.Today.ToString("yyyy");
			txtYear6.Text = DateTime.Today.ToString("yyyy");
			txtYear7.Text = DateTime.Today.ToString("yyyy");
			txtYear8.Text = DateTime.Today.ToString("yyyy");
			txtToDate5.Text = "30/11/" + DateTime.Today.ToString("yyyy");
			txtFromDate6.Text = DateTime.Today.ToString("MM/yyyy");
			txtToDate6.Text = DateTime.Today.AddMonths(1).AddDays(-1 - DateTime.Today.Day).ToString("MM/yyyy");
			txtToDate8.Text = "31/12/" + DateTime.Today.ToString("yyyy");
			//txtFromDate10.Text = "01/01/" + DateTime.Today.ToString("yyyy");
			txtFromDate10.Text = "01-Jan-" + DateTime.Today.ToString("yyyy");
			txtToDate10.Text = "31-Dec-" + DateTime.Today.ToString("yyyy");
		}

		private void DisplayRow(HtmlTableRow RowDisplay)
		{
			trToKhaiBH.Attributes.Add("style","display:none");
			trC46.Attributes.Add("style","display:none");
			trC47.Attributes.Add("style","display:none");
			trC47a.Attributes.Add("style","display:none");
			trC47b.Attributes.Add("style","display:none");
			trC45.Attributes.Add("style","display:none");
			trC45c.Attributes.Add("style","display:none");
			trC02SBH.Attributes.Add("style","display:none");
			trSalProcess.Attributes.Add("style","display:none");
			trC04BH.Attributes.Add("style","display:none");
			trC04BH_NDS.Attributes.Add("style","display:none");
			trSalProcess.Attributes.Add("style","display:none");
			trSIProcess.Attributes.Add("style","display:none");
			trRegisterLB.Attributes.Add("style","display:none");
			trStateOfLabour.Attributes.Add("style","display:none");
			trLeaveRecordByDepartment.Attributes.Add("style","display:none");
			trEmpRegisterLB.Attributes.Add("style","display:none");
			trOther.Attributes.Add("style","display:none");
			RowDisplay.Attributes.Remove("style");
		}

		private void LoadComboBox()
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			clsHREmpList.LoadComboLocation(cboLocation, strTextField);
			clsHREmpList.LoadComboLocation(cboLocation1, strTextField);
			//clsHREmpList.LoadComboLevel1(cboLSLevel1ID10, strTextField,strLanguage,this.Page);
			//clsHREmpList.LoadComboLevel2(cboLSLevel2ID10, strTextField,strLanguage,this.Page);
			clsHREmpList.LoadComboLevel1(cboLevel1, strTextField,strLanguage,this.Page);
			clsHREmpList.LoadComboLevel1(cboLevel1_1, strTextField,strLanguage,this.Page);
			clsHREmpList.LoadComboCompany(cboCompany3,strTextField,strLanguage,this.Page);    
			clsHREmpList.LoadComboCompany(cboCompany5,strTextField,strLanguage,this.Page);    
			clsHREmpList.LoadComboCompany(cboCompany6,strTextField,strLanguage,this.Page);    
			clsHREmpList.LoadComboCompany(cboLSCompanyID2,strTextField,strLanguage,this.Page);    
			clsHREmpList.LoadComboCompany(cboLSCompanyID3,strTextField,strLanguage,this.Page);    
			clsHREmpList.LoadComboCompany(cboLSCompanyID4,strTextField,strLanguage,this.Page);    	
			clsHREmpList.LoadComboCompany(cboLSCompanyID7,strTextField,strLanguage,this.Page);    		
			clsHREmpList.LoadComboCompany(cboLSCompanyID8,strTextField,strLanguage,this.Page);    		
			clsHREmpList.LoadComboCompany(cboLSCompanyID9,strTextField,strLanguage,this.Page);    		
			clsHREmpList.LoadComboCompany(cboLSCompanyID10,strTextField,strLanguage,this.Page);    		
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
			this.cboLSCompanyID10.SelectedIndexChanged += new System.EventHandler(this.cboLSCompanyID10_SelectedIndexChanged);
			this.cboLSLevel1ID10.SelectedIndexChanged += new System.EventHandler(this.cboLSLevel1ID10_SelectedIndexChanged);
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region btnPrint_Click - Khong dung

//		private void btnPrint_Click(object sender, System.EventArgs e)
//		{
//			DataTable dtList = new DataTable();
//			string strCtrlName="", strTemp ="";
//			dtList = DM.GetControlsofCatalog(strTableName,Server.MapPath(".") +   ConfigurationSettings.AppSettings["pstrPathReportsXML"]);
//			
//			#region Load report xls
//
//			string strFileName = dtList.Rows[dtList.Rows.Count-1]["ReportName"].ToString().Trim();
//			
//			if (strFileName.IndexOf(".xls") != -1)
//			{
//
//				ArrayList arrParams = new ArrayList();
		//				ArrayList arrValues = new ArrayList();
//				ArrayList arrGroups = new ArrayList();
//				ArrayList arrGroupDescripts = new ArrayList();
//				string arrGroupSorts="";
//				string strStoreInputs="";
//
//				for(int i=0;i<dtList.Rows.Count-1;i++)
//				{				
//					// CHECK IF IT'S PARAM
//					if(dtList.Rows[i]["IsParam"].ToString().Trim().ToLower()=="true")
//					{			
//						arrParams.Add(dtList.Rows[i]["ID"].ToString().Trim());
//						strCtrlName = DM.ReturnControlName(dtList.Rows[i]["ID"].ToString(),dtList.Rows[i]["ControlType"].ToString());
//						arrValues.Add(GetValueControl(strCtrlName));					
//					}
//					if(dtList.Rows[i]["IsWhere"].ToString().Trim().ToLower()=="true")
//					{			
//						strCtrlName = DM.ReturnControlName(dtList.Rows[i]["ID"].ToString(),dtList.Rows[i]["ControlType"].ToString());					
//						if(GetValueControl(strCtrlName).Trim()!="")
//							strStoreInputs+=" ,@"+dtList.Rows[i]["ID"].ToString()+" = '"+GetValueControl(strCtrlName).Trim()+"' ";					
//					}
//				}
//			
//			/*	if (EmpHeaderSearch1.txtEmpID.Text.Trim() !="" )
//					strStoreInputs+=" ,@EmpID = N'" + EmpHeaderSearch1.txtEmpID.Text.Trim() + "' ";
//
//				if (EmpHeaderSearch1.txtEmpName.Text.Trim() !="" )
//					strStoreInputs+=" ,@EmpName = N'" + EmpHeaderSearch1.txtEmpName.Text.Trim() + "' ";
//
//				if (EmpHeaderSearch1.cboLevel1.SelectedValue.Trim() !="" )
//					strStoreInputs+=" ,@Level1ID = N'" + EmpHeaderSearch1.cboLevel1.SelectedValue.Trim() + "' ";
//
//				if (EmpHeaderSearch1.cboLevel2.SelectedValue.Trim() !="" )
//					strStoreInputs+=" ,@Level2ID = N'" + EmpHeaderSearch1.cboLevel2.SelectedValue.Trim() + "' ";
//
//				if (EmpHeaderSearch1.cboLevel3.SelectedValue.Trim() !="" )
//					strStoreInputs+=" ,@Level3ID = N'" + EmpHeaderSearch1.cboLevel3.SelectedValue.Trim() + "' ";
//
//				if (EmpHeaderSearch1.cboLocation.SelectedValue.Trim() !="" )
//					strStoreInputs+=" ,@LocationID = N'" + EmpHeaderSearch1.cboLocation.SelectedValue.Trim() + "' ";
//
//				if (EmpHeaderSearch1.cboJobcode.SelectedValue.Trim() !="" )
//					strStoreInputs+=" ,@JobcodeID = N'" + EmpHeaderSearch1.cboJobcode.SelectedValue.Trim() + "' ";
//
//				if (EmpHeaderSearch1.cboPosition.SelectedValue.Trim() !="" )
//					strStoreInputs+=" ,@PositionID = N'" + EmpHeaderSearch1.cboPosition.SelectedValue.Trim() + "' ";
//
//				if (EmpHeaderSearch1.cboCompany.SelectedValue.Trim() !="" )
//					strStoreInputs+=" ,@CompanyID = N'" + EmpHeaderSearch1.cboCompany.SelectedValue.Trim() + "' ";
//
//				if (EmpHeaderSearch1.optStatus.SelectedValue.Trim() !="" )
//					strStoreInputs+=" ,@Status = '" + EmpHeaderSearch1.optStatus.SelectedValue.Trim() + "' ";*/
//
//				//Session["ssReportName"] = dtList.Rows[dtList.Rows.Count-1]["ID"].ToString().Trim(); //"HR_rptTest.rpt";
//				//Session["ssReportParams"] = arrParams.Substring(0,arrParams.Length-1);
//				//Session["ssReportValues"] = arrValues.Substring(0,arrValues.Length-1);
//				//--------------
//
//				// GROUPS AND SORTS
//		/*		if(grp1.SelectedValue!="") 
//				{
//					arrGroups.Add(grp1.SelectedValue);
//					arrGroupDescripts.Add(des1.SelectedValue);				
//				}
//				if(grp2.SelectedValue!="") 
//				{
//					arrGroups.Add(grp2.SelectedValue);
//					arrGroupDescripts.Add(des2.SelectedValue);				
//				}
//				if(grp3.SelectedValue!="") 
//				{
//					arrGroups.Add(grp3.SelectedValue);
//					arrGroupDescripts.Add(des3.SelectedValue);				
//				}
//				if(grp4.SelectedValue!="") 
//				{
//					arrGroups.Add(grp4.SelectedValue);
//					arrGroupDescripts.Add(des4.SelectedValue);				
//				}
//				if(grp5.SelectedValue!="") 
//				{
//					arrGroups.Add(grp5.SelectedValue);
//					arrGroupDescripts.Add(des5.SelectedValue);				
//				}
//
//				// SORTS
//				if(sort1.SelectedValue!="") arrGroupSorts+=sort1.SelectedValue+",";			
//				if(sort2.SelectedValue!="") arrGroupSorts+=sort2.SelectedValue+",";
//				if(sort3.SelectedValue!="") arrGroupSorts+=sort3.SelectedValue+",";
//				if(sort4.SelectedValue!="") arrGroupSorts+=sort4.SelectedValue+",";
//				if(sort5.SelectedValue!="") arrGroupSorts+=sort5.SelectedValue+","; */
//			
//				if(arrGroupSorts!="") arrGroupSorts=arrGroupSorts.Remove(arrGroupSorts.Length-1,1);
//				///////////////////////////////////////////////			
//			
//				clsExcelReport exr1 = new clsExcelReport();
//				exr1.setSQLConnString(ConfigurationSettings.AppSettings["pstrConnSQLExcelReport"]);
//				exr1.setExcelConnString(ConfigurationSettings.AppSettings["pstrConnExcel"]);
//
//				strTemp = Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrReportFolder"];
//				strTemp += dtList.Rows[dtList.Rows.Count-1]["Description"].ToString().Trim();
//				exr1.setTemplatesFolder(strTemp);
//				exr1.setTempFolder(Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrTemplatesFolder"]);
//			
//				exr1.setParams(arrParams,arrValues);
//				exr1.setGroups(arrGroups,arrGroupDescripts);
//				exr1.setSorts(arrGroupSorts);
//
//				string strStoreName = dtList.Rows[dtList.Rows.Count-1]["ID"].ToString().Trim();
//			
//				if(strStoreInputs!="") strStoreInputs=" "+strStoreInputs.Substring(2);
//				strStoreName+=strStoreInputs;
//
//				lblErr.Text = exr1.mToExcel(strFileName,strStoreName,this.Page );
//				//exr1.Dispose();
//				exr1 = null;
//			}
//				#endregion Load report xls
//
//			# region Load report crystal
//
//			else if (strFileName.IndexOf(".rpt") != -1)
//			{
//				string strParams ="";
//				string strValues = "";			
//
//				//////   Get params and Get Value of params
//				string Language=Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
//				strParams = "@LangID;@ModuleID;@Level1ID;@GroupID";
//				
//				strValues += Language;	
//				strValues += ";" + "SI";
//				strValues += ";" + "";
//				strValues += ";" + "";			
//				if(dtList.Rows[dtList.Rows.Count-1]["ID"].ToString() =="SI_sprptC45")
//				{
//					strParams += ";@Year;@CompanyID;@ToDate";
//					strValues += ";" + txtYear.Text.Trim();
//   					strValues += ";" + cboCompany3.SelectedItem.Value;
//					strValues += ";" + txtToDate5.Text.Trim();   
//				}
//				if(dtList.Rows[dtList.Rows.Count-1]["ID"].ToString() =="SI_sprptC45c")
//				{
//					strParams += ";@LocationID;@FromDate;@ToDate;@IsType;@CompanyID" ;
//					strValues += ";" + cboLocation.SelectedItem.Value;
//					strValues += ";" + txtFromDate.Text.Trim();
//					strValues += ";" + txtToDate.Text.Trim();
//					strValues += ";" + optIsType.SelectedValue;
//					strValues += ";" + cboCompany6.SelectedItem.Value ;
//					
//				}
//				if(dtList.Rows[dtList.Rows.Count-1]["ID"].ToString() =="SI_sprptC47b")
//				{
//					strParams += ";@FromDate;@ToDate;@Language;@CompanyID" ;
//					strValues += ";" + txtFromDate.Text.Trim();
//					strValues += ";" + txtToDate.Text.Trim();
//					strValues += ";" + Language;
//					strValues += ";" + cboCompany5.SelectedValue;
//				}
//				
//			/*	for(int i=0;i<dtList.Rows.Count-1;i++)
//				{
//					if(dtList.Rows[i]["IsWhere"].ToString().Trim().ToLower()=="true")
//					{			
//						strCtrlName = DM.ReturnControlName(dtList.Rows[i]["ID"].ToString(),dtList.Rows[i]["ControlType"].ToString());
//						strParams += ";@" + strCtrlName.Substring(3);
//						strValues += ";" + GetValueControl(strCtrlName);
//					}
//				}*/
//
//				Session["ssReportName"] = dtList.Rows[dtList.Rows.Count-1]["ReportName"].ToString().Trim(); //"HR_rptTest.rpt";
//				Session["ssReportParams"] = strParams;
//				Session["ssReportValues"] = strValues;
//				
//				// GROUPS AND SORTS
//				string strGroup = "";
//				string strGroupText = "";
//				string strSort = "";
//				string strSortDirection = "";
//				/*if(grp1.SelectedValue!="") 
//				{
//					strGroup = grp1.SelectedValue.Trim();
//					strGroupText = des1.SelectedValue.Trim() != ""? des1.SelectedValue.Trim():grp1.SelectedValue.Trim();
//				}
//				if(grp2.SelectedValue!="") 
//				{
//					strGroup = (strGroup != ""?(strGroup + ";"):"") + grp2.SelectedValue.Trim();
//					strGroupText = (strGroupText != ""?(strGroupText + ";"):"") + (des2.SelectedValue.Trim() != ""? des2.SelectedValue.Trim():grp2.SelectedValue.Trim());
//				}
//				if(grp3.SelectedValue!="") 
//				{
//					strGroup = (strGroup != ""?(strGroup + ";"):"") + grp3.SelectedValue.Trim();
//					strGroupText = (strGroupText != ""?(strGroupText + ";"):"") + (des3.SelectedValue.Trim() != ""? des3.SelectedValue.Trim():grp3.SelectedValue.Trim());			
//				}
//				if(grp4.SelectedValue!="") 
//				{
//					strGroup = (strGroup != ""?(strGroup + ";"):"") + grp4.SelectedValue.Trim();
//					strGroupText = (strGroupText != ""?(strGroupText + ";"):"") + (des4.SelectedValue.Trim() != ""? des4.SelectedValue.Trim():grp4.SelectedValue.Trim());				
//				}
//				if(grp5.SelectedValue!="") 
//				{
//					strGroup = (strGroup != ""?(strGroup + ";"):"") + grp5.SelectedValue.Trim();
//					strGroupText = (strGroupText != ""?(strGroupText + ";"):"") + (des5.SelectedValue.Trim() != ""? des5.SelectedValue.Trim():grp5.SelectedValue.Trim());			
//				}
//
//				// SORTS
//				string strDirection = "";
//				if(sort1.SelectedValue!="") 
//				{
//					strSort = (strSort != ""?(strSort + ";"):"") + sort1.SelectedValue.Trim().Substring(0,sort1.SelectedValue.Trim().LastIndexOf(" "));		
//					if (((float)sort1.SelectedIndex / (2.0) * 2) == (sort1.SelectedIndex/2*2))
//						strDirection = "Desc";
//					else
//						strDirection = "";
//					strSortDirection = strDirection;
//				}
//				if(sort2.SelectedValue!="") 
//				{
//					strSort = (strSort != ""?(strSort + ";"):"") + sort2.SelectedValue.Trim().Substring(0,sort2.SelectedValue.Trim().LastIndexOf(" "));		
//					if (((float)sort2.SelectedIndex / (2.0) * 2) == (sort2.SelectedIndex/2*2))
//						strDirection = "Desc";
//					else
//						strDirection = "";
//					strSortDirection = strSortDirection + ";" + strDirection;	
//				}
//				if(sort3.SelectedValue!="") 
//				{
//					strSort = (strSort != ""?(strSort + ";"):"") + sort3.SelectedValue.Trim().Substring(0,sort3.SelectedValue.Trim().LastIndexOf(" "));		
//					if (((float)sort3.SelectedIndex / (2.0) * 2) == (sort3.SelectedIndex/2*2))
//						strDirection = "Desc";
//					else
//						strDirection = "";
//					strSortDirection = strSortDirection + ";" + strDirection;	
//				}
//				if(sort4.SelectedValue!="") 
//				{
//					strSort = (strSort != ""?(strSort + ";"):"") + sort4.SelectedValue.Trim().Substring(0,sort4.SelectedValue.Trim().LastIndexOf(" "));		
//					if (((float)sort4.SelectedIndex / (2.0) * 2) == (sort4.SelectedIndex/2*2))
//						strDirection = "Desc";
//					else
//						strDirection = "";
//					strSortDirection = strSortDirection + ";" + strDirection;	
//				}
//				if(sort5.SelectedValue!="") 
//				{
//					strSort = (strSort != ""?(strSort + ";"):"") + sort5.SelectedValue.Trim().Substring(0,sort5.SelectedValue.Trim().LastIndexOf(" "));		
//					if (((float)sort5.SelectedIndex / (2.0) * 2) == (sort5.SelectedIndex/2*2))
//						strDirection = "Desc";
//					else
//						strDirection = "";
//					strSortDirection = strSortDirection + ";" + strDirection;
//				}*/
//
//				Session["ssReportGroupBy"] = strGroup;
//				Session["ssReportGroupByText"] = strGroupText;
//				Session["ssReportSortBy"] = strSort;
//				Session["ssReportSortDirection"] = strSortDirection;
//				clsCommon.OpenNewWindow(this.Page,"Reports/ShowReports.aspx","ManPower");							
//			}
//			# endregion Load report crystal
//
//			dtList.Dispose();
//
//			#region back up
//			/*
//			 *		Back up by HaLT 06/07/2005
//  
//  
//						clsExcelReport exr1 = new clsExcelReport();
//						exr1.setSQLConnString(ConfigurationSettings.AppSettings["pstrConnSQLExcelReport"]);
//						exr1.setExcelConnString(ConfigurationSettings.AppSettings["pstrConnExcel"]);
//
//						strTemp = Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrReportFolder"];
//						strTemp += dtList.Rows[dtList.Rows.Count-1]["Description"].ToString().Trim();
//						exr1.setTemplatesFolder(strTemp);
//						exr1.setTempFolder(Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrTemplatesFolder"]);
//			
//						exr1.setParams(arrParams,arrValues);
//						exr1.setGroups(arrGroups,arrGroupDescripts);
//						exr1.setSorts(arrGroupSorts);
//
//						string strFileName = dtList.Rows[dtList.Rows.Count-1]["ReportName"].ToString().Trim();
//						string strStoreName = dtList.Rows[dtList.Rows.Count-1]["ID"].ToString().Trim();
//			
//						if(strStoreInputs!="") strStoreInputs=" "+strStoreInputs.Substring(2);
//						strStoreName+=strStoreInputs;
//
//						lblErr.Text = exr1.mToExcel(strFileName,strStoreName,this.Page );
//						//exr1.Dispose();
//						exr1 = null;
//			
//			*/
//
//			/*
//						DataTable dtList = new DataTable();
//						dtList = DM.GetControlsofCatalog(strTableName,Request.MapPath("REPORTS.XML").ToString().Trim());
//						string arrParams ="";
//						string arrValues = "";			
//						for(int i=0;i<dtList.Rows.Count-1;i++)
//						{
//							arrParams += "@" + dtList.Rows[i]["ID"].ToString().Trim() + ";";
//							string strCtrlName = DM.ReturnControlName(dtList.Rows[i]["ID"].ToString(),dtList.Rows[i]["ControlType"].ToString());
//							arrValues += GetValueControl(strCtrlName) + ";" ;
//						}
//						Session["ssReportName"] = dtList.Rows[dtList.Rows.Count-1]["ID"].ToString().Trim(); //"HR_rptTest.rpt";
//						Session["ssReportParams"] = arrParams.Substring(0,arrParams.Length-1);
//						Session["ssReportValues"] = arrValues.Substring(0,arrValues.Length-1);
//						clsCommon.OpenNewWindow(this.Page,"Reports/ShowReports.aspx","ManPower");
//			//			if(clsCatalog.ExecuteCommand(strTableName,Session["ssclsAction"].ToString(),arrParams,arrDataType,arrValues))
//			//				clsCatalog.ShowMessageBox(Page,"Action completed!");
//			//			else
//			//				clsCatalog.ShowMessageBox(Page,"Action error, Please check again!");			
//						dtList.Dispose();
//						//this.ResetControl(this);
//						//Response.Redirect(Request.ApplicationPath + "/Catalog.aspx?tabid=" + strTableName);
//			
//			*/
//
//			#endregion back up
//		}

//		private string GetValueControl(string strControlName)
//		{
//			string strReturnValue="";
//			try
//			{
//				Control ctrFound = this.FindControl(strControlName);
//				if(ctrFound!=null)
//				{
//					string strType = ctrFound.GetType().ToString().Trim().Substring(ctrFound.GetType().ToString().Trim().LastIndexOf(".")+1).ToString().Trim();
//					switch(strType)
//					{
//						case "TextBox":
//							strReturnValue = ((TextBox)ctrFound).Text;
//							break;
//						case "DropDownList":
//							strReturnValue = ((DropDownList)ctrFound).SelectedValue.Trim();
//							break;
//						case "RadioButtonList":
//							strReturnValue = ((RadioButtonList)ctrFound).SelectedValue.Trim();
//							break;
//						case "CheckBox":
//							strReturnValue = ((CheckBox)ctrFound).Checked.ToString();
//							break;
//						case "RadioButton":
//							strReturnValue = ((RadioButton)ctrFound).Checked.ToString();
//							break;												
//						default :
//							break;
//					}
//				}
//			}				
//			catch(Exception ex)
//			{
//				return "";
//			}
//			return strReturnValue;
//		}
		#endregion btnPrint_Click - Khong dung

		private void btnPrint_Click(object sender, System.EventArgs e)
		{
			string strTabID = Request.Params["tabid"].ToString().Trim();
			string strParams = "";
			string strValues = "";
			string strCompany = "";
			string strGroup = "";
			string strLevel1ID = "";
			string strModuleID = "SI";
			
			string strApplyAmount = "";
			string strNotYetAmount = "";

			switch (strTabID)
			{
				case "C45":
					strCompany = cboCompany3.SelectedItem.Value;
					strParams ="@Year;@LSCompanyID;@ToDate";
					strValues = txtYear.Text.Trim() 
						+ ";" + strCompany +  ";" + txtToDate5.Text.Trim() ;
					Session["ssReportName"] = "SI/SI_rptC45.rpt";					
					break;

					//DataTable1 dt = 
					// DataTable 2
					// Xu ly DataTable -> 
					//DataView dv1 = get(c45)
					//dataview2 =
					
					// ExcelApp EzuatExcel(dataView)
					

				case "C45c":
					strCompany = cboCompany6.SelectedItem.Value;
					strParams = "@Location;@FromDate;@ToDate;@IsType;@LSCompanyID";
					strValues = cboLocation.SelectedItem.Value + ";" + txtFromDate.Text.Trim() 
						+ ";" + txtToDate.Text.Trim() + ";" + optIsType.SelectedValue + ";" + strCompany;
					Session["ssReportName"] = "SI/SI_rptC45c.rpt";
					break;
				case "C46":
					strCompany = cboLSCompanyID4.SelectedValue.Trim();
					string strTransferExcess = txtTransferExcess4.Text.Trim()==""?"0":txtTransferExcess4.Text.Trim();
					string strTransferLack = txtTransferLack4.Text.Trim()==""?"0":txtTransferLack4.Text.Trim();
					string strFund = txtFund4.Text.Trim()==""?"0":txtFund4.Text.Trim();
					string strDelayFine = txtDelayFine4.Text.Trim()==""?"0":txtDelayFine4.Text.Trim();
					strApplyAmount = txtApplyAmount4.Text.Trim()==""?"0":txtApplyAmount4.Text.Trim();
					strParams ="@FromMonth;@ToMonth;@LSCompanyID;@TransferExcess;@TransferLack;@Fund;@DelayFine;@ApplyAmount;@PrintDate";
					strValues = txtFromMonth4.Text.Trim() + ";" + txtToMonth4.Text.Trim() + ";" + strCompany
						+ ";" + strTransferExcess + ";" + strTransferLack + ";" + strFund + ";" + strDelayFine  + ";" + strApplyAmount+ ";";
					Session["ssReportName"] = "SI/SI_rptC46.rpt";
					break;

				case "C47":
					strCompany = cboLSCompanyID7.SelectedValue.Trim();
					string strSumEmpPrev = txtSumEmpPrev7.Text.Trim()==""?"0":txtSumEmpPrev7.Text.Trim();
					string strWageFundPrev = txtWageFundPrev7.Text.Trim()==""?"0":txtWageFundPrev7.Text.Trim();
					strParams ="@Date;@PrevLabourCount;@PrevSumSal;@Stage;@LSCompanyID;@PositionSigner;@Signer";
					strValues = "01/" + txtMonth7.Text.Trim() + ";" + strSumEmpPrev
						+ ";" + strWageFundPrev + ";" + txtStage7.Text.Trim() + ";" + strCompany +";" + txtPosSigner.Text.Trim() + ";" + txtSigner2.Text.Trim() ;
					Session["ssReportName"] = "SI/SI_rptC47.rpt";
					break;

				case "C47a":
					strCompany = cboLSCompanyID9.SelectedValue.Trim();
					strParams ="@MM;@YYYY;@FromDate;@ToDate;@LSCompanyID;@PositionSigner;@Signer";
					strValues = txtMonth9.Text.Trim().Substring(0, 2) + ";" + txtMonth9.Text.Trim().Substring(3, 4)
						+ ";01/" + txtMonth9.Text.Trim() + ";01/" + txtMonth9.Text.Trim() + ";" + strCompany +";" + txtPosSigner1.Text.Trim() + ";" + txtSigner3.Text.Trim() ;
					Session["ssReportName"] = "SI/SI_rptC47a.rpt";
					break;
				case "C47b":
					strCompany = cboCompany5.SelectedValue;
					strParams = "@FromDate;@ToDate;@Language;@LSCompanyID";
					strValues = txtFromDate6.Text.Trim() + ";" + txtToDate6.Text.Trim() + ";" + strLanguage
						+ ";" + strCompany;
					Session["ssReportName"] = "SI/SI_rptC47b.rpt";
					break;
				case "C04BH":
					strCompany = cboLSCompanyID3.SelectedValue.Trim();
					strParams ="@Quarter;@Month;@Year;@FromDate;@ToDate;@XGroup;@Status;@LSCompanyID";
					strValues = txtQuarter3.Text.Trim() + ";" + txtMonth3.Text.Trim() + ";" + txtYear3.Text.Trim() 
						+ ";" + txtFromDate3.Text.Trim() + ";" + txtToDate3.Text.Trim()  + ";" + cboGroup3.SelectedValue.Trim() + ";1" 
						+ ";" + strCompany;
					Session["ssReportName"] = "SI/SI_rptC04BH.rpt";
					break;
				case "C04BH_NDS":
					strCompany = cboLSCompanyID8.SelectedValue.Trim();
					strLevel1ID = cboLSLevel1ID8.SelectedValue.Trim();
					strApplyAmount = txtApplyAmount8.Text.Trim()==""?"0":txtApplyAmount8.Text.Trim();
					strNotYetAmount = txtNotYetAmount8.Text.Trim()==""?"0":txtNotYetAmount8.Text.Trim();
					strParams ="@Quarter;@Year;@FromDate;@ToDate;@Status;@LSCompanyID;@LSLevel1ID;@ApplyAmount;@NotYetAmount";
					strValues = txtQuarter8.Text.Trim() + ";" + txtYear8.Text.Trim() 
						+ ";;" + txtToDate8.Text.Trim() 
						+ ";1" 
						+ ";" + strCompany + ";" + strLevel1ID+";"+ strApplyAmount + ";" + strNotYetAmount;
					Session["ssReportName"] = "SI/SI_rptC04BH_NDS.rpt";
					break;
				case "Tokhai":
					strCompany = cboLSCompanyID10.SelectedValue.Trim();
					strParams = "@FromDate;@ToDate;@LSCompanyID;@Level1ID;@Level2ID;@EmpID;@UserGroupID";
					strValues = txtFromDate10.Text.Trim() + ";" + txtToDate10.Text.Trim() 
						+ ";" + strCompany + ";" + cboLSLevel1ID10.SelectedValue.Trim() 
						+ ";" + cboLSLevel2ID10.SelectedValue.Trim() + ";" + clsSI.GetEmpIDFromCode(txtEmpCode.Text.Trim())+ ";" + strUserName;
					Session["ssReportName"] = "SI/SI_rpt01_TBH.rpt";
					break;
				case "C02SBH":
					strCompany = cboLSCompanyID2.SelectedValue.Trim();
					
					strParams = "@MMYYYY;@LSCompanyID;@Year;@Quarter;@PositionSigner;@Signer";
					strValues = txtMonth2.Text.Trim() + ";" + strCompany  + ";;;" + txtPositionSigner.Text.Trim() + ";" + txtSignerBH.Text.Trim() ;
					Session["ssReportName"] = "SI/SI_rpt02_SBH.rpt";
					break;
				default:
					break;
			}

			if (strTabID == "C45" || strTabID == "C45c"|| strTabID == "C46" || strTabID == "C47"  || strTabID == "C47a" || strTabID == "C47b"  || strTabID == "C04BH" || strTabID == "C04BH_NDS" || strTabID == "C02SBH")
			{
				strParams = strParams + ";@CompanyID;@LangID;@ModuleID;@GroupID;@Level1ID";
				strValues = strValues + ";" + strCompany + ";" + strLanguage + ";" + strModuleID + ";" + strGroup + ";" + strLevel1ID;
			}
			Session["ssReportParams"] = strParams;
			Session["ssReportValues"] = strValues;
			clsCommon.OpenNewWindow(this.Page,"Reports/ShowReports.aspx","");
		}

		private void cboLSCompanyID10_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			string strCompanyID = cboLSCompanyID10.SelectedValue.Trim();			
			if (!strCompanyID.Equals(""))
			{
				clsHREmpList.LoadComboLevel1ByCompany(cboLSLevel1ID10,strTextField,strCompanyID, strLanguage,this.Page); 
				cboLSLevel1ID10.SelectedValue = this.txtLevel1ID.Value.Trim();
				cboLSLevel1ID10_SelectedIndexChanged(null,null);
			}
			else
			{
				cboLSLevel1ID10.Items.Clear();
			}



		}

		private void cboLSLevel1ID10_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";
			string strLevel1ID = cboLSLevel1ID10.SelectedValue.Trim();
			if (!strLevel1ID.Equals(""))
			{
				clsHREmpList.LoadComboLevel2ByLevel1(cboLSLevel2ID10,strTextField,strLevel1ID, strLanguage,this.Page); 
				cboLSLevel2ID10.SelectedValue = this.txtLevel2ID.Value.Trim();
				//cboLSLevel2ID10_SelectedIndexChanged(null,null);
			}
			else
			{
				cboLSLevel2ID10.Items.Clear();
			}
		}

		private void btnExcel_Click(object sender, System.EventArgs e)
		{
			string strTabID = Request.Params["tabid"].ToString().Trim();
			string strParams = "";
			string strValues = "";
			string strCompany = "";
			string strGroup = "";
			string strLevel1ID = "";
			string strLevel2ID="";
			string strModuleID = "SI";
			
			string strApplyAmount = "";
			string strNotYetAmount = "";

			
			string strYear="";
			string strToDate="";
			string strMonth="";
			string strFromDate="";
			string strLocation="";
			string strIsType="";
			string strQuarter="";
			int  intStatus;
			string strFilename="";
			DataView dataView=null;
			ExcelReportUtils excelReport = new ExcelReportUtils();
			ArrayList arrayList = new ArrayList();
			
			Hashtable hstHeading=null;
			Hashtable hstEnding=null;
			Hashtable hstTmp = null;

			switch (strTabID)
			{
				case "C45":
					strCompany = cboCompany3.SelectedItem.Value;
					strYear =txtYear.Text.Trim();
					strToDate = txtToDate5.Text.Trim();
					hstHeading = new Hashtable();
					hstEnding  = new Hashtable();
					hstHeading = clsExcel.getTitleReport(strLanguage,strCompany,strGroup,strLevel1ID,strModuleID);
					dataView = clsExcel.getDataViewC45(strYear, strCompany, strToDate, ref hstHeading, ref hstEnding);
					strFilename = excelReport.exportExcel("baohiem","C45_BH.xls",dataView,hstHeading,hstEnding);			
					break;
				case "C47":
					strCompany = cboLSCompanyID7.SelectedValue.Trim();
					string strSumEmpPrev = txtSumEmpPrev7.Text.Trim()==""?"0":txtSumEmpPrev7.Text.Trim();
					string strWageFundPrev = txtWageFundPrev7.Text.Trim()==""?"0":txtWageFundPrev7.Text.Trim();
					string strThang = "01/"+txtMonth7.Text.Trim();
					string strStage = txtStage7.Text.Trim();
					//khoi tao lai cac hstHeading, drTmp, hstEnding
					hstHeading = new Hashtable();
					hstEnding  = new Hashtable();
				
					hstHeading = clsExcel.getTitleReport(strLanguage,strCompany,strGroup,strLevel1ID,strModuleID);
					
					dataView = clsExcel.getDataViewC47(strCompany,strSumEmpPrev,strWageFundPrev,
						strThang,strStage, ref hstHeading, ref hstEnding);
										
					strFilename = excelReport.exportExcel("baohiem","C47_BH.xls",dataView,hstHeading,hstEnding);
					break;
				case "C47a":
					 strCompany = cboLSCompanyID9.SelectedValue.Trim();
					 strMonth = txtMonth9.Text.Trim().Substring(0, 2);
					 strYear = txtMonth9.Text.Trim().Substring(3, 4);
					 strFromDate = "01/" + txtMonth9.Text.Trim();
					 strToDate = "01/" + txtMonth9.Text.Trim();
					//khoi tao lai cac 
					hstHeading = new Hashtable();
					hstEnding  = new Hashtable();
					
					hstHeading = clsExcel.getTitleReport(strLanguage,strCompany,strGroup,strLevel1ID,strModuleID);

					dataView = clsExcel.getDataViewC47A(Convert.ToInt32(strMonth),Convert.ToInt32(strYear),
									strFromDate,strToDate,strCompany, ref hstHeading, ref hstEnding);
									
					strFilename = excelReport.exportExcel("baohiem","C47a_BH.xls",dataView,hstHeading,hstEnding);
				break;
				case "C45c":
					strCompany = cboCompany6.SelectedItem.Value;
					strLocation = cboLocation.SelectedItem.Value;
					strFromDate = txtFromDate.Text.Trim();
					strToDate = txtToDate.Text.Trim();
					strIsType = optIsType.SelectedValue;
					
					hstHeading = new Hashtable();
					hstEnding  = new Hashtable();
					
					hstHeading = clsExcel.getTitleReport(strLanguage,strCompany,strGroup,strLevel1ID,strModuleID);
				
					 dataView = clsExcel.getDataViewC45C(strLocation,strFromDate,strToDate,Convert.ToInt32(strIsType),strCompany, ref hstHeading, ref hstEnding);
					
					strFilename = excelReport.exportExcel("baohiem","C45c_BH.xls",dataView,hstHeading,hstEnding);
					break;
				case "C04BH":
					strCompany = cboLSCompanyID3.SelectedValue.Trim();
					strQuarter = txtQuarter3.Text.Trim();
					strMonth = txtMonth3.Text.Trim();
					strYear = txtYear3.Text.Trim();
					strFromDate = txtFromDate3.Text.Trim();
					strToDate = txtToDate3.Text.Trim();
					strGroup = cboGroup3.SelectedValue.Trim();
					intStatus = 1;
					
					hstHeading = new Hashtable();
					hstEnding  = new Hashtable();
					
					hstHeading = clsExcel.getTitleReport(strLanguage,strCompany,strGroup,strLevel1ID,strModuleID);
												
					dataView = clsExcel.getDataViewC04BH(strQuarter,strMonth,strYear,strGroup,strFromDate,strToDate,intStatus,strCompany,ref hstHeading, ref hstEnding);
					
					strFilename = excelReport.exportExcel("baohiem","C04BH_BH.xls",dataView,hstHeading,hstEnding);
					break;
				case "C04BH_NDS":
					strCompany = cboLSCompanyID8.SelectedValue.Trim();
					strApplyAmount = txtApplyAmount8.Text.Trim()==""?"0":txtApplyAmount8.Text.Trim();
					strNotYetAmount = txtNotYetAmount8.Text.Trim()==""?"0":txtNotYetAmount8.Text.Trim();
					strQuarter = txtQuarter8.Text.Trim();
					strYear = txtYear8.Text.Trim();
					strFromDate = "";
					strToDate = txtToDate8.Text.Trim();
					intStatus = 1;
					

					hstHeading = new Hashtable();
					hstEnding  = new Hashtable();
					ExcelReportUtils objReport = new ExcelReportUtils();
					hstHeading = clsExcel.getTitleReport(strLanguage,strCompany,strGroup,strLevel1ID,strModuleID);
					dataView = clsExcel.getDataViewC04BH_NDS(strQuarter,strYear,strFromDate,strToDate,intStatus,
								strCompany,objReport.parseToDouble(strApplyAmount.Replace(",",""),0),objReport.parseToDouble(strNotYetAmount.Replace(",",""), 0),
						ref hstHeading, ref hstEnding);
					
					strFilename = excelReport.exportExcel("baohiem","C04BH_NDS_BH.xls",dataView,hstHeading,hstEnding);
					break;
				case "C02SBH":
					strCompany = cboLSCompanyID2.SelectedValue.Trim();
					strMonth = txtMonth2.Text.Trim();
					strYear = "";
					strQuarter = "";
					
					hstHeading = new Hashtable();
					hstEnding  = new Hashtable();
					
					hstHeading = clsExcel.getTitleReport(strLanguage,strCompany,strGroup,strLevel1ID,strModuleID);
					
					dataView = clsExcel.getDataViewC02SBH(strYear,strQuarter,strMonth,strCompany,
								ref hstHeading, ref hstEnding);
					strFilename = excelReport.exportExcel("baohiem","C02SBH_BH.xls",dataView,hstHeading,hstEnding);
					break;
				case "C46":
					strCompany = cboLSCompanyID4.SelectedValue.Trim();
					string strTransferExcess = txtTransferExcess4.Text.Trim()==""?"0":txtTransferExcess4.Text.Trim();
					string strTransferLack = txtTransferLack4.Text.Trim()==""?"0":txtTransferLack4.Text.Trim();
					string strFund = txtFund4.Text.Trim()==""?"0":txtFund4.Text.Trim();
					string strDelayFine = txtDelayFine4.Text.Trim()==""?"0":txtDelayFine4.Text.Trim();
					strApplyAmount = txtApplyAmount4.Text.Trim()==""?"0":txtApplyAmount4.Text.Trim();
					
					string strFromMonth = txtFromMonth4.Text.Trim();
					string strToMonth = txtToMonth4.Text.Trim();
					
					hstHeading = new Hashtable();
					hstEnding  = new Hashtable();
					
					hstHeading = clsExcel.getTitleReport(strLanguage,strCompany,strGroup,strLevel1ID,strModuleID);

					dataView = clsExcel.getDataViewC46(strFromMonth,strToMonth,strCompany,Convert.ToInt64(strTransferExcess.Replace(",","")),
									Convert.ToInt64(strTransferLack.Replace(",","")),Convert.ToInt64(strFund.Replace(",","")),
									Convert.ToInt64(strDelayFine.Replace(",","")),Convert.ToInt64(strApplyAmount.Replace(",","")),"",
									ref hstHeading, ref hstEnding);			
					
					strFilename = excelReport.exportExcel("baohiem","C46_BH.xls",dataView,hstHeading,hstEnding);
					break;
				case "C47b":
					strCompany = cboCompany5.SelectedValue;
					strFromDate = txtFromDate6.Text.Trim();
					strToDate = txtToDate6.Text.Trim();

					hstHeading = new Hashtable();
					hstEnding  = new Hashtable();
					
					hstHeading = clsExcel.getTitleReport(strLanguage,strCompany,strGroup,strLevel1ID,strModuleID);
					dataView = clsExcel.getDataViewC47B(strCompany,strFromDate,strToDate,strLanguage,ref hstHeading, ref hstEnding );
					
					strFilename = excelReport.exportExcel("baohiem","C47b_BH.xls",dataView,hstHeading,hstEnding);
					break;
				//Khoi xuat Excel
				/*case "Tokhai":
					strCompany = cboLSCompanyID10.SelectedValue.Trim();
					strFromDate = txtFromDate10.Text.Trim();
					strToDate = txtToDate10.Text.Trim();
					strLevel1ID = cboLSLevel1ID10.SelectedValue.Trim();
					strLevel2ID = cboLSLevel2ID10.SelectedValue.Trim();
					string strEmpID = clsSI.GetEmpIDFromCode(txtEmpCode.Text.Trim());
					hstHeading = clsExcel.getTitleReport(strLanguage,strCompany,strGroup,strLevel1ID,strModuleID);
					dataView = clsExcel.getDataViewToKhai(strFromDate,strToDate,strCompany,strLevel1ID,strLevel2ID,strEmpID);		
					break;*/
				default:
					break;
				

			}
			Page.Response.AppendHeader( "Content-Disposition", "attachment;filename=tmp.xls");
			Page.Response.ContentType = "application/vnd.ms-excel"; 
			Page.Response.WriteFile(strFilename);
			Page.Response.Flush();
			Page.Response.End();
		}
	}
}
