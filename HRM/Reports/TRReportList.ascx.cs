namespace iHRPCore.Reports
{
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
	using Web_DM.Component;
	using iHRPCore.Com;	
	using ExcelReportDll;
	using System.Configuration;
	using System.Data.OleDb;
	using iHRPCore.Include;

	/// <summary>
	///		Summary description for TRReportList1.
	/// </summary>
	public class TRReportList1 : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.LinkButton btnRpt21;
		protected System.Web.UI.WebControls.Image Image25;
		protected System.Web.UI.WebControls.LinkButton btnRpt20;
		protected System.Web.UI.WebControls.Image Image24;
		protected System.Web.UI.WebControls.LinkButton btnRpt19;
		protected System.Web.UI.WebControls.Image Image23;
		protected System.Web.UI.WebControls.LinkButton btnRpt18;
		protected System.Web.UI.WebControls.Image Image22;
		protected System.Web.UI.WebControls.LinkButton btnRpt17;
		protected System.Web.UI.WebControls.Image Image21;
		protected System.Web.UI.WebControls.ImageButton btnCalFromDate;
		protected System.Web.UI.WebControls.TextBox txtFromDate;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtToDate;
		protected System.Web.UI.WebControls.ImageButton btnCalToDate;
		protected System.Web.UI.WebControls.Label lblErr;
		protected iHRPCore.Include.EmpHeaderSearch EmpHeaderSearch1;
		protected System.Web.UI.WebControls.Panel pnlTR;
		protected System.Web.UI.WebControls.Panel pnlPruU;
		protected System.Web.UI.WebControls.Image Image41;
		protected System.Web.UI.WebControls.Image Image42;
		protected System.Web.UI.WebControls.Image Image43;
		protected System.Web.UI.WebControls.Image Image44;
		protected System.Web.UI.WebControls.Image Image46;
		protected System.Web.UI.WebControls.Image Image47;
		protected System.Web.UI.WebControls.Image Image76;
		protected System.Web.UI.WebControls.Image Image77;
		protected System.Web.UI.WebControls.Image Image78;
		protected System.Web.UI.WebControls.Image Image79;
		protected System.Web.UI.WebControls.Image Image80;
		protected System.Web.UI.WebControls.Image Image81;
		protected System.Web.UI.WebControls.Image Image82;
		protected System.Web.UI.WebControls.Image Image83;
		protected System.Web.UI.WebControls.Image Image84;
		protected System.Web.UI.WebControls.Panel pnlLOMA;
		protected System.Web.UI.WebControls.Panel pnlGE;
		protected System.Web.UI.WebControls.Image Image40;
		protected System.Web.UI.WebControls.Image Image39;
		protected System.Web.UI.WebControls.Image Image38;
		protected System.Web.UI.WebControls.Image Image37;
		protected System.Web.UI.WebControls.Image Image36;
		protected System.Web.UI.WebControls.Image Image35;
		protected System.Web.UI.WebControls.LinkButton btnRpt0102;
		protected System.Web.UI.WebControls.LinkButton btnRpt03;
		protected System.Web.UI.WebControls.LinkButton btnRpt04;
		protected System.Web.UI.WebControls.LinkButton btnRpt051;
		protected System.Web.UI.WebControls.LinkButton btnRpt052;
		protected System.Web.UI.WebControls.LinkButton btnRpt061;
		protected System.Web.UI.WebControls.LinkButton btnRpt062;
		protected System.Web.UI.WebControls.LinkButton btnRpt063;
		protected System.Web.UI.WebControls.LinkButton btnRpt064;
		protected System.Web.UI.WebControls.LinkButton btnRpt07;
		protected System.Web.UI.WebControls.LinkButton btnRpt081;
		protected System.Web.UI.WebControls.LinkButton btnRpt083;
		protected System.Web.UI.WebControls.LinkButton btnRpt091;
		protected System.Web.UI.WebControls.LinkButton btnRpt092;
		protected System.Web.UI.WebControls.LinkButton btnRpt12;
		protected System.Web.UI.WebControls.LinkButton btnRpt131;
		protected System.Web.UI.WebControls.LinkButton btnRpt132;
		protected System.Web.UI.WebControls.LinkButton btnRpt133;
		protected System.Web.UI.WebControls.LinkButton btnRpt141;
		protected System.Web.UI.WebControls.LinkButton btnRpt142;
		protected System.Web.UI.WebControls.LinkButton btnRpt15;
		protected System.Web.UI.WebControls.LinkButton btnRpt16;
		protected System.Web.UI.WebControls.TextBox txtCourseName;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList cboSupplier;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.TextBox txtTrainer;
		protected System.Web.UI.WebControls.TextBox txtSubject;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.DropDownList cboCertificate;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.DropDownList cboLomaCourseID;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.DropDownList cboLOMACertificateID;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.DropDownList cboCourseType;
		protected System.Web.UI.WebControls.Image Image1;
		protected System.Web.UI.WebControls.LinkButton btnRpt10;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.TextBox txtCourseID;
		protected System.Web.UI.WebControls.TextBox txtManager;
		protected System.Web.UI.WebControls.LinkButton btnRpt101;
		protected System.Web.UI.WebControls.Image Image2;
		protected System.Web.UI.WebControls.Image Image3;
		protected System.Web.UI.WebControls.LinkButton btnRpt102;
		protected System.Web.UI.WebControls.TextBox txtPosition1;
		protected System.Web.UI.WebControls.TextBox txtPosition2;
		protected System.Web.UI.WebControls.Label Label14;
		protected System.Web.UI.WebControls.TextBox txtAssManager;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.Image Image34;

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				txtFromDate.Attributes.Add("onblur","return MyCheckDate('"+txtFromDate.ClientID+"');");
				btnCalFromDate.Attributes.Add("onclick","return MyShowCalendar('"+txtFromDate.ClientID+"');");
				txtToDate.Attributes.Add("onblur","return MyCheckDate('"+txtToDate.ClientID+"');");
				btnCalToDate.Attributes.Add("onclick","return MyShowCalendar('"+txtToDate.ClientID+"');");
				btnRpt10.Attributes.Add("onclick","return InputName('"+txtManager.ClientID+"','"+txtAssManager.ClientID+"','"+txtPosition1.ClientID+"','"+txtPosition2.ClientID+"');");

				if(Request.Params.Get("Rpt")!=null)
				{
					switch(Request.Params.Get("Rpt"))
					{
						case "TR":
							pnlTR.Visible=true;	
							LoadComboSupplier();
							break;
						case "PruU":
							LoadComboCertificate();
							pnlPruU.Visible=true;
							break;
						case "LOMA":
							LoadComboCourseID();
							LoadComboLOMACertificate();
							pnlLOMA.Visible=true;
							break;
						case "GE":
							pnlGE.Visible=true;
							break;
						default:break;
					}
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnRpt0102.Click += new System.EventHandler(this.btnRpt0102_Click);
			this.btnRpt03.Click += new System.EventHandler(this.btnRpt03_Click);
			this.btnRpt04.Click += new System.EventHandler(this.btnRpt04_Click);
			this.btnRpt051.Click += new System.EventHandler(this.btnRpt051_Click);
			this.btnRpt052.Click += new System.EventHandler(this.btnRpt052_Click);
			this.btnRpt061.Click += new System.EventHandler(this.btnRpt061_Click);
			this.btnRpt062.Click += new System.EventHandler(this.btnRpt062_Click);
			this.btnRpt063.Click += new System.EventHandler(this.btnRpt063_Click);
			this.btnRpt064.Click += new System.EventHandler(this.btnRpt064_Click);
			this.btnRpt07.Click += new System.EventHandler(this.btnRpt07_Click);
			this.btnRpt081.Click += new System.EventHandler(this.btnRpt081_Click);
			this.btnRpt083.Click += new System.EventHandler(this.btnRpt083_Click);
			this.btnRpt091.Click += new System.EventHandler(this.btnRpt091_Click);
			this.btnRpt092.Click += new System.EventHandler(this.btnRpt092_Click);
			this.btnRpt12.Click += new System.EventHandler(this.btnRpt12_Click);
			this.btnRpt133.Click += new System.EventHandler(this.btnRpt133_Click);
			this.btnRpt131.Click += new System.EventHandler(this.btnRpt131_Click);
			this.btnRpt132.Click += new System.EventHandler(this.btnRpt132_Click);
			this.btnRpt141.Click += new System.EventHandler(this.btnRpt141_Click);
			this.btnRpt142.Click += new System.EventHandler(this.btnRpt142_Click);
			this.btnRpt15.Click += new System.EventHandler(this.btnRpt15_Click);
			this.btnRpt16.Click += new System.EventHandler(this.btnRpt16_Click);
			this.btnRpt10.Click += new System.EventHandler(this.btnRpt10_Click);
			this.btnRpt101.Click += new System.EventHandler(this.btnRpt101_Click);
			this.btnRpt102.Click += new System.EventHandler(this.btnRpt102_Click);
			this.btnRpt17.Click += new System.EventHandler(this.btnRpt17_Click);
			this.btnRpt18.Click += new System.EventHandler(this.btnRpt18_Click);
			this.btnRpt19.Click += new System.EventHandler(this.btnRpt19_Click);
			this.btnRpt20.Click += new System.EventHandler(this.btnRpt20_Click);
			this.btnRpt21.Click += new System.EventHandler(this.btnRpt21_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private string GetValueControl(string strControlName)
		{
			string strReturnValue="";
			try
			{
				Control ctrFound = this.FindControl(strControlName);
				if(ctrFound!=null)
				{
					string strType = ctrFound.GetType().ToString().Trim().Substring(ctrFound.GetType().ToString().Trim().LastIndexOf(".")+1).ToString().Trim();
					switch(strType)
					{
						case "TextBox":
							strReturnValue = ((TextBox)ctrFound).Text;
							break;
						case "DropDownList":
							strReturnValue = ((DropDownList)ctrFound).SelectedValue.Trim();
							break;
						case "RadioButtonList":
							strReturnValue = ((RadioButtonList)ctrFound).SelectedValue.Trim();
							break;
						case "CheckBox":
							strReturnValue = ((CheckBox)ctrFound).Checked.ToString();
							break;
						case "RadioButton":
							strReturnValue = ((RadioButton)ctrFound).Checked.ToString();
							break;												
						default :
							break;
					}
				}
			}				
			catch(Exception ex)
			{
				return "";
			}
			return strReturnValue;
		}	


		private void ShowReport(string strTableName)
		{			
			DataTable dtList;
			string strCtrlName="", strTemp ="",strStoreInputs="";
			dtList = DM.GetControlsofCatalog(strTableName,Server.MapPath(".") +   ConfigurationSettings.AppSettings["pstrPathReportsXML"]);
			
			#region Load report xls
	
			string strFileName = dtList.Rows[dtList.Rows.Count-1]["ReportName"].ToString().Trim();
			
			if (strFileName.IndexOf(".xls") != -1)
			{

				ArrayList arrParams = new ArrayList();
				ArrayList arrValues = new ArrayList();
				
				for(int i=0;i<dtList.Rows.Count-1;i++)
				{				
					// CHECK IF IT'S PARAM
					if(dtList.Rows[i]["IsParam"].ToString().Trim().ToLower()=="true")
					{			
						arrParams.Add(dtList.Rows[i]["ID"].ToString().Trim());
						strCtrlName = DM.ReturnControlName(dtList.Rows[i]["ID"].ToString(),dtList.Rows[i]["ControlType"].ToString());
						arrValues.Add(GetValueControl(strCtrlName));					
					}
					if(dtList.Rows[i]["IsWhere"].ToString().Trim().ToLower()=="true")
					{			
						strCtrlName = DM.ReturnControlName(dtList.Rows[i]["ID"].ToString(),dtList.Rows[i]["ControlType"].ToString());					
						if(GetValueControl(strCtrlName).Trim()!="")
							strStoreInputs+=" ,@"+dtList.Rows[i]["ID"].ToString()+" = '"+GetValueControl(strCtrlName).Trim()+"' ";					
					}
				}
			
				if (EmpHeaderSearch1.txtEmpID.Text.Trim() !="" )
					strStoreInputs+=" ,@EmpID = N'" + EmpHeaderSearch1.txtEmpID.Text.Trim() + "' ";

				if (EmpHeaderSearch1.txtEmpName.Text.Trim() !="" )
					strStoreInputs+=" ,@EmpName = N'" + EmpHeaderSearch1.txtEmpName.Text.Trim() + "' ";

				if (EmpHeaderSearch1.cboLevel1.SelectedValue.Trim() !="" )
					strStoreInputs+=" ,@Level1ID = N'" + EmpHeaderSearch1.cboLevel1.SelectedValue.Trim() + "' ";

				if (EmpHeaderSearch1.cboLevel2.SelectedValue.Trim() !="" )
					strStoreInputs+=" ,@Level2ID = N'" + EmpHeaderSearch1.cboLevel2.SelectedValue.Trim() + "' ";

				if (EmpHeaderSearch1.cboLevel3.SelectedValue.Trim() !="" )
					strStoreInputs+=" ,@Level3ID = N'" + EmpHeaderSearch1.cboLevel3.SelectedValue.Trim() + "' ";

				if (EmpHeaderSearch1.cboLocation.SelectedValue.Trim() !="" )
					strStoreInputs+=" ,@LocationID = N'" + EmpHeaderSearch1.cboLocation.SelectedValue.Trim() + "' ";

				if (EmpHeaderSearch1.cboJobcode.SelectedValue.Trim() !="" )
					strStoreInputs+=" ,@JobcodeID = N'" + EmpHeaderSearch1.cboJobcode.SelectedValue.Trim() + "' ";

				if (EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue.Trim() !="" )
					strStoreInputs+=" ,@PositionID = N'" + EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue.Trim() + "' ";

				if (EmpHeaderSearch1.cboCompany.SelectedValue.Trim() !="" )
					strStoreInputs+=" ,@CompanyID = N'" + EmpHeaderSearch1.cboCompany.SelectedValue.Trim() + "' ";

				if (EmpHeaderSearch1.optStatus.SelectedValue.Trim() !="" )
					strStoreInputs+=" ,@Status = " + EmpHeaderSearch1.optStatus.SelectedValue.Trim();

				
				///////////////////////////////////////////////			
			
				clsExcelReport exr1 = new clsExcelReport();
				exr1.setSQLConnString(ConfigurationSettings.AppSettings["pstrConnSQLExcelReport"]);
				exr1.setExcelConnString(ConfigurationSettings.AppSettings["pstrConnExcel"]);

				strTemp = Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrReportFolder"];
				strTemp += dtList.Rows[dtList.Rows.Count-1]["Description"].ToString().Trim();
				exr1.setTemplatesFolder(strTemp);
				exr1.setTempFolder(Server.MapPath(".") + ConfigurationSettings.AppSettings["pstrTemplatesFolder"]);
			
				exr1.setParams(arrParams,arrValues);				
				exr1.setSorts("");
				
				string strStoreName = dtList.Rows[dtList.Rows.Count-1]["ID"].ToString().Trim();
			
				if(strStoreInputs!="") strStoreInputs=" "+strStoreInputs.Substring(2);
				strStoreName+=strStoreInputs;

				lblErr.Text = exr1.mToExcel(strFileName,strStoreName,this.Page );
				
				exr1 = null;
			}
			#endregion Load report xls				
		}
		

		private void LoadComboSupplier()
		{
			string strSQL="LS_spfrmTRSUPPLIER @Activity = 'GetDataAll'";
			clsCommon.LoadDropDownListControl(cboSupplier,strSQL,"TRSupplierCode","Name",true);
		}

		private void LoadComboCertificate()
		{
			string strSQL="LS_spfrmTRCERTIFICATE @Activity = 'GetDataAll'";
			clsCommon.LoadDropDownListControl(cboCertificate,strSQL,"TRCertificateCode","Name",true);
		}

		private void LoadComboCourseID()
		{
			string strSQL="LS_spfrmTRLOMACOURSE @Activity = 'GetDataAll'";
			clsCommon.LoadDropDownListControl(cboLomaCourseID,strSQL,"TRLOMACourseCode","Name",true);
		}

		private void LoadComboLOMACertificate()
		{
			string strSQL="LS_spfrmTRLOMACERTIFICATE  @Activity = 'GetDataAll'";
			clsCommon.LoadDropDownListControl(cboLOMACertificateID,strSQL,"TRLOMACertificateCode","Name",true);
		}

		private void btnRpt131_Click(object sender, System.EventArgs e)
		{
			ShowReport("rptTRLOMATestingHistory");
		}

		private void btnRpt132_Click(object sender, System.EventArgs e)
		{
			ShowReport("rptTRLOMACertificate");
		}

		private void btnRpt133_Click(object sender, System.EventArgs e)
		{
			ShowReport("rptTRLOMAEnrolment");
		}

		private void btnRpt141_Click(object sender, System.EventArgs e)
		{
			ShowReport("rptTRLOMATestingFlexCredit");
		}

		private void btnRpt142_Click(object sender, System.EventArgs e)
		{
			ShowReport("rptTRLOMATestingFlexDebit");
		}

		private void btnRpt15_Click(object sender, System.EventArgs e)
		{
			ShowReport("rptTRLOMAYearlyReport");
		}

		private void btnRpt16_Click(object sender, System.EventArgs e)
		{
			// CRYSTAL REPORT			
			Session["EmpID"]=EmpHeaderSearch1.txtEmpID.Text;
			Session["EmpName"]=EmpHeaderSearch1.txtEmpName.Text;
			Session["JobClass"]=EmpHeaderSearch1.cboJobcode.SelectedValue;
			Session["Location"]=EmpHeaderSearch1.cboLocation.SelectedValue;
			Session["Level1"]=EmpHeaderSearch1.cboLevel1.SelectedValue;
			Session["Level2"]=EmpHeaderSearch1.cboLevel2.SelectedValue;
			Session["Level3"]=EmpHeaderSearch1.cboLevel3.SelectedValue;
			Session["Position"]=EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue;
			Session["Certificate"]=cboLOMACertificateID.SelectedValue;
		
			string strURL="./Reports/TR/LOMASuggestionReport.aspx?RptName=LOMASugestion";
			Response.Write("<script>window.open('"+strURL+"','LOMASuggestion','toolbar=0,status=no,menubar=no,scrollbars=1,resizable=yes,copyhistory=yes,top=10,left=50,width=700,height=550,align=top');</script>");
		}

		private void btnRpt17_Click(object sender, System.EventArgs e)
		{
			ShowReport("rptTRTotalTrainingHistoryByEmp");
		}

		private void btnRpt18_Click(object sender, System.EventArgs e)
		{
			ShowReport("rptTRTotalReport");
		}

		private void btnRpt19_Click(object sender, System.EventArgs e)
		{
			ShowReport("rptTRWorkingDayForTraining");
		}

		private void btnRpt20_Click(object sender, System.EventArgs e)
		{
			ShowReport("rptHeadCountTrained");
		}

		private void btnRpt21_Click(object sender, System.EventArgs e)
		{
			// CRYSTAL REPORT			
//			Session["EmpID"]=EmpHeaderSearch1.txtEmpID.Text;
//			Session["EmpName"]=EmpHeaderSearch1.txtEmpName.Text;
//			Session["JobClass"]=EmpHeaderSearch1.cboJobcode.SelectedValue;
//			Session["Location"]=EmpHeaderSearch1.cboLocation.SelectedValue;
//			Session["Level1"]=EmpHeaderSearch1.cboLevel1.SelectedValue;
			Session["Level2"]=EmpHeaderSearch1.cboLevel2.SelectedValue;
//			Session["Level3"]=EmpHeaderSearch1.cboLevel3.SelectedValue;
//			Session["Position"]=EmpHeaderSearch1.cboPosition.SelectedValue;
//			Session["Certificate"]=cboLOMACertificateID.SelectedValue;
		
			string strURL="./Reports/TR/LOMASuggestionReport.aspx?RptName=DeptDevPlan";
			Response.Write("<script>window.open('"+strURL+"','DepartmentDevPlan','toolbar=0,status=no,menubar=no,scrollbars=1,resizable=yes,copyhistory=yes,top=10,left=50,width=700,height=550,align=top');</script>");
		}

		private void btnRpt0102_Click(object sender, System.EventArgs e)
		{		
			ShowReport("rptTRTrainingHistoryByEmp");			
		}

		private void btnRpt03_Click(object sender, System.EventArgs e)
		{
			ShowReport("rptTRClassroomHistory");		
		}

		private void btnRpt04_Click(object sender, System.EventArgs e)
		{
			ShowReport("rptTRTrainingSupplierHistory");		
		}

		private void btnRpt051_Click(object sender, System.EventArgs e)
		{
			ShowReport("rptTRTrainingParticipants");		
		}

		private void btnRpt052_Click(object sender, System.EventArgs e)
		{
			ShowReport("rptTRPublicCourseParticipants");
		}

		private void btnRpt061_Click(object sender, System.EventArgs e)
		{
			ShowReport("rptTRWorkshopHistory");
		}

		private void btnRpt062_Click(object sender, System.EventArgs e)
		{
			ShowReport("rptTROutingHistory");		
		}

		private void btnRpt063_Click(object sender, System.EventArgs e)
		{
			ShowReport("rptTRWorkshopParticipants");			
		}

		private void btnRpt064_Click(object sender, System.EventArgs e)
		{
			ShowReport("rptTROutingParticipants");	
		}

		private void btnRpt07_Click(object sender, System.EventArgs e)
		{
			ShowReport("rptTRPruUTestingEnrolment");		
		}

		private void btnRpt081_Click(object sender, System.EventArgs e)
		{
			ShowReport("rptTRPruUTestingHistory");
		}

		private void btnRpt083_Click(object sender, System.EventArgs e)
		{
			ShowReport("rptTRPruCertCompletion");
		}

		private void btnRpt091_Click(object sender, System.EventArgs e)
		{
			ShowReport("rptTRPruUTestingFlexCredit");
		}

		private void btnRpt092_Click(object sender, System.EventArgs e)
		{
			ShowReport("rptTRPruUTestingFlexDebit");
		}

		private void btnRpt12_Click(object sender, System.EventArgs e)
		{
			ShowReport("rptTRPruUTestingFlexQuartYear");
		}

		private void btnRpt10_Click(object sender, System.EventArgs e)
		{
			// CRYSTAL REPORT
			Session["Signer1"]=txtManager.Text;
			Session["Signer2"]=txtAssManager.Text;
			Session["Pos1"]=txtPosition1.Text;
			Session["Pos2"]=txtPosition2.Text;

			Session["EmpID"]=EmpHeaderSearch1.txtEmpID.Text;
			Session["EmpName"]=EmpHeaderSearch1.txtEmpName.Text;
			Session["JobClass"]=EmpHeaderSearch1.cboJobcode.SelectedValue;
			Session["Location"]=EmpHeaderSearch1.cboLocation.SelectedValue;
			Session["Level1"]=EmpHeaderSearch1.cboLevel1.SelectedValue;
			Session["Level2"]=EmpHeaderSearch1.cboLevel2.SelectedValue;
			Session["Level3"]=EmpHeaderSearch1.cboLevel3.SelectedValue;
			Session["Position"]=EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue;
			
			Session["FromDate"]=txtFromDate.Text;
			Session["ToDate"]=txtToDate.Text;
			
			string strURL="./Reports/TR/LOMASuggestionReport.aspx?RptName=PruCertificate";
			Response.Write("<script>window.open('"+strURL+"','PruUCertificate','toolbar=0,status=no,menubar=no,scrollbars=1,resizable=yes,copyhistory=yes,top=10,left=50,width=700,height=550,align=top');</script>");
		}

		private void btnRpt101_Click(object sender, System.EventArgs e)
		{
			// CRYSTAL REPORT			
			string strURL="./Reports/TR/LOMASuggestionReport.aspx?RptName=PruGrade";
			Response.Write("<script>window.open('"+strURL+"','PruUGrade','toolbar=0,status=no,menubar=no,scrollbars=1,resizable=yes,copyhistory=yes,top=10,left=50,width=700,height=550,align=top');</script>");
		}

		private void btnRpt102_Click(object sender, System.EventArgs e)
		{
			// CRYSTAL REPORT			
			string strURL="./Reports/TR/LOMASuggestionReport.aspx?RptName=LOMAGrade";
			Response.Write("<script>window.open('"+strURL+"','LOMAGrade','toolbar=0,status=no,menubar=no,scrollbars=1,resizable=yes,copyhistory=yes,top=10,left=50,width=700,height=550,align=top');</script>");
		}
	}
}
