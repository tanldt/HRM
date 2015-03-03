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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using iHRPCore.Com;

namespace iHRPCore.UserServices.Reports
{
	/// <summary>
	/// Summary description for ShowReports.
	/// </summary>
	public class ShowReports : System.Web.UI.Page
	{
		//protected CrystalDecisions.Web.CrystalReportViewer CrystalReportViewer1;
		public CrystalDecisions.Web.CrystalReportViewer CrystalReportViewer1;
			
		private void Page_Load(object sender, System.EventArgs e)
		{
			//if (CrystalReportViewer1 == null)
				//CrystalReportViewer1 = CrystalReportViewer1;

			if(!this.Page.IsPostBack)
			{	
				if(Request.QueryString["pRptName"]!=null)
				{
					Session.Remove("ssReportParams");
					Session.Remove("ssReportValues");
					//Group, Sort
					Session.Remove("ssReportGroupBy");
					Session.Remove("ssReportGroupByText");
					Session.Remove("ssReportSortBy");
					Session.Remove("ssReportSortDirection");
					//End: Group, Sort
					Session["ssReportName"] = Request.QueryString["pRptName"];
					if(Request.QueryString["pStrPara"] == null)
					{
						Response.Redirect("ShowReports.aspx");
						Response.End();
					}
				}
				if(Request.QueryString["pStrPara"] != null)
				{
					Session["ssReportParams"] = Request.QueryString["pStrPara"];
					Session["ssReportValues"] = Request.QueryString["pStrValue"];
					Response.Redirect("ShowReports.aspx");
					Response.End();
				}
				//-----21/07/2005:Them phan group dong va sort dong--------
				if (Request.QueryString["pStrGroupBy"] != null)
				{
					Session["ssReportGroupBy"] = Request.QueryString["pStrGroupBy"];
					Session["ssReportGroupByText"] = Request.QueryString["pStrGroupByText"];
				}
				if (Request.QueryString["pStrSortBy"] != null)
				{
					Session["ssReportSortBy"] = Request.QueryString["pStrSortBy"];
					Session["ssReportSortDirection"] = Request.QueryString["pStrSortDirection"];
				}
				//end
			}			
			LoadDataReport();			
			//LoadDataReport(this.Page);
		}

		private void LoadDataReport()
		{
			string frm_strRptName = "";
			string frm_strRptParams = "";
			string frm_strRptValues = "";
			ReportDocument crReportDocument = new ReportDocument();
			ConnectionInfo crConnectionInfo = new ConnectionInfo();
			//--------
						
			//--------
			Database crDatabase;
			Tables crTables ;
			TableLogOnInfo crTableLogOnInfo;
			if(Session["ssReportName"] !=null)
				frm_strRptName= Session["ssReportName"].ToString().Trim();
			CrystalReportViewer1.DisplayGroupTree = false;			
			//--------------			
			crReportDocument.Load(this.Server.MapPath(frm_strRptName),CrystalDecisions.Shared.OpenReportMethod.OpenReportByDefault);
			
			//-------Truyen tham so cho report
			if(Session["ssReportParams"] != null)
			{
				ParameterValues pvCollection;
				ParameterDiscreteValue pdvCanID = new ParameterDiscreteValue();
				frm_strRptParams = Session["ssReportParams"].ToString().Trim();
				frm_strRptValues = Session["ssReportValues"].ToString().Trim();
				string[] marrPara = frm_strRptParams.Split(';');
				string[] marrValue = frm_strRptValues.Split(';');
				for(int i=0;i<marrPara.Length;i++)
				{
					ParameterDiscreteValue pdvTemp = new ParameterDiscreteValue();
					pvCollection = crReportDocument.DataDefinition.ParameterFields[marrPara.GetValue(i).ToString().Trim()].CurrentValues;
					pdvTemp.Value = marrValue.GetValue(i).ToString().Trim();
					pvCollection.Add(pdvTemp);
					crReportDocument.DataDefinition.ParameterFields[clsCommon.SafeDataString(marrPara[i].Trim())].ApplyCurrentValues(pvCollection);
				}
			}			
			
			///------ thong tin connection cho report------
			crDatabase = crReportDocument.Database;
			crTables= crDatabase.Tables;
			crConnectionInfo.ServerName = clsCommon.App_ServerName;
			crConnectionInfo.DatabaseName = clsCommon.App_DatabaseName;
			crConnectionInfo.UserID = clsCommon.App_UserName;
			crConnectionInfo.Password = clsCommon.App_Password;
			///-------------------------------------
			foreach (CrystalDecisions.CrystalReports.Engine.Table rptTable in crTables)
			{
				crTableLogOnInfo = rptTable.LogOnInfo;
				crTableLogOnInfo.ConnectionInfo = crConnectionInfo;
				rptTable.ApplyLogOnInfo(crTableLogOnInfo);
				//rptTable.Location = clsCommon.App_DatabaseName + ".dbo." + rptTable.Location.Substring(rptTable.Location.LastIndexOf(".") + 1);				
				rptTable.Location = rptTable.Location.Substring(rptTable.Location.LastIndexOf(".") + 1);
			}
			//---------end--------------------			
			//-------Truyen vao group by tu dong----

			/*			
						string frm_strRptGroupBy = "";
						string frm_strRptGroupByText = "";
						GroupNameFieldDefinitions crGroupNames = crReportDocument.DataDefinition.GroupNameFields;
						string strPrefixGroup = crReportDocument.Database.Tables[0].Name.Trim() + ".";//crGroupNames.Count > 0?crGroupNames[0].Name.Substring(crGroupNames[0].Name.LastIndexOf("{") + 1,crGroupNames[0].Name.LastIndexOf(".") - crGroupNames[0].Name.LastIndexOf("{")):"";
						if (Session["ssReportGroupBy"] != null && strPrefixGroup.Trim() != "")
						{
							FormulaFieldDefinition crFormulaGroup;
							FormulaFieldDefinition crFormulaGroupText;
							FormulaFieldDefinitions crFormulas;
							crFormulas = crReportDocument.DataDefinition.FormulaFields;
							frm_strRptGroupBy = Session["ssReportGroupBy"].ToString().Trim();
							frm_strRptGroupByText = Session["ssReportGroupByText"].ToString().Trim();
							string[] marrGroupBy = frm_strRptGroupBy.Split(';');
							string[] marrGroupByText = frm_strRptGroupByText.Split(';');
							string strGroupName = "";
							strGroupName = crGroupNames[0].Name.Substring(0,crGroupNames[0].Name.LastIndexOf("{") - 2);
							for(int i=0;i<crGroupNames.Count;i++)
							{
								crFormulaGroup = crFormulas[strGroupName];
								crFormulaGroup.Text	= marrGroupBy[i].ToString().Trim() ==""?"":"{" + strPrefixGroup.Trim() + marrGroupBy[i].ToString() + "}";
								crFormulaGroupText = crFormulas[strGroupName + "Text"];
								crFormulaGroupText.Text = marrGroupByText[i].ToString().Trim() ==""?"":"{" + strPrefixGroup.Trim() + marrGroupByText[i].ToString() + "}";
							}
						}			
						//thanhnd
			*/
			// HOPTD COMMENTED
			// crGroupNames.Dispose();
			// END HOPTD COMMENTED
			//-------End truyen vao group by tu dong----
			//-------Truyen vao sort by tu dong----
			string frm_strRptSortBy = "";
			string frm_strRptSortDirection = "";
			SortFields crSortFields = crReportDocument.DataDefinition.SortFields;
			if (Session["ssReportSortBy"] != null)
			{
				DatabaseFieldDefinition crDatabaseFieldDefinition;
				SortField crSortField;
				frm_strRptSortBy = Session["ssReportSortBy"].ToString().Trim();
				frm_strRptSortDirection = Session["ssReportSortDirection"].ToString().Trim();
				string[] marrSortBy = frm_strRptSortBy.Split(';');
				string[] marrSortDirection = frm_strRptSortDirection.Split(';');
				for(int i=0;i<marrSortBy.Length;i++)
				{
					if (marrSortBy[i].ToString().Trim() != "")
					{
						crDatabaseFieldDefinition = crReportDocument.Database.Tables[0].Fields[marrSortBy[i].Trim()];
						crSortField = crReportDocument.DataDefinition.SortFields[0];
						crSortField.Field = crDatabaseFieldDefinition;
                        //if (marrSortDirection[i].Trim() == "Desc")
                        //{
                        //    crSortField.SortDirection = SortDirection.DescendingOrder;
                        //}
                        //else
                        //{
                        //    crSortField.SortDirection = SortDirection.AscendingOrder;
                        //}
					}
				}
			}
			//-------End Truyen vao sort by tu dong----

			// Set connection to subreports

			if (crReportDocument.Subreports.Count >0 )
			{			
				ReportDocument objSubReport = new ReportDocument();
				for(int i = 0; i<crReportDocument.Subreports.Count; i++)
				{
					objSubReport = crReportDocument.OpenSubreport(crReportDocument.Subreports[i].Name.Trim());						 						
					SetConnectionInfo(objSubReport);
				}
			}
			//---------end--------------------

			
			CrystalReportViewer1.ReportSource = crReportDocument;
			CrystalReportViewer1.DataBind();				
		}

		// Get the ConnectionInfo Object.
		public void SetConnectionInfo( ReportDocument objReport)
		{
			if ( objReport ==  null) return;
        
			ConnectionInfo crConnectionInfo = new ConnectionInfo();
			Database crDatabase;
			Tables crTables ;
			TableLogOnInfo crTableLogOnInfo;
			
			crDatabase = objReport.Database;
			crTables= crDatabase.Tables;
			crConnectionInfo.ServerName = clsCommon.App_ServerName;
			crConnectionInfo.DatabaseName = clsCommon.App_DatabaseName;
			crConnectionInfo.UserID = clsCommon.App_UserName;
			crConnectionInfo.Password = clsCommon.App_Password;
			
			///-------------------------------------
			foreach (CrystalDecisions.CrystalReports.Engine.Table rptTable in crTables)
			{
				crTableLogOnInfo = rptTable.LogOnInfo;
				crTableLogOnInfo.ConnectionInfo = crConnectionInfo;
				rptTable.ApplyLogOnInfo(crTableLogOnInfo);
				rptTable.Location = rptTable.Location.Substring(rptTable.Location.LastIndexOf(".") + 1);
			}			
		}

		// Get the ConnectionInfo Object.
//		public void SetConnectionInfo( ReportDocument objReport)
//		{
//			if ( objReport ==  null) return;
//        
//			ConnectionInfo crConnectionInfo = new ConnectionInfo();
//			Database crDatabase;
//			Tables crTables ;
//			TableLogOnInfo crTableLogOnInfo;
//			
//			crDatabase = objReport.Database;
//			crTables= crDatabase.Tables;
//			crConnectionInfo.ServerName = clsCommon.App_ServerName;
//			crConnectionInfo.DatabaseName = clsCommon.App_DatabaseName;
//			crConnectionInfo.UserID = clsCommon.App_UserName;
//			crConnectionInfo.Password = clsCommon.App_Password;
//			
//			///-------------------------------------
//			foreach (CrystalDecisions.CrystalReports.Engine.Table rptTable in crTables)
//			{
//				crTableLogOnInfo = rptTable.LogOnInfo;
//				crTableLogOnInfo.ConnectionInfo = crConnectionInfo;
//				rptTable.ApplyLogOnInfo(crTableLogOnInfo);
//				rptTable.Location = rptTable.Location.Substring(rptTable.Location.LastIndexOf(".") + 1);
//			}			
//		}
//
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
