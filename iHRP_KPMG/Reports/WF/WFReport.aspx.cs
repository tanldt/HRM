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
using iHRPCore.TRComponent;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using System.Configuration;
using iHRPCore.Com;

namespace iHRPCore.Reports.WF
{
	/// <summary>
	/// Summary description for LOMASuggestionReport.
	/// </summary>
	public class WFReport : System.Web.UI.Page
	{
		protected CrystalDecisions.Web.CrystalReportViewer CrystalReportViewer1;	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Request.Params["RptName"]!=null)
			{
				switch(Request.Params["RptName"].ToString().ToUpper())
				{
					case "CARDKEYISSUE":
						ReportDocument rptDoc=new ReportDocument();						
						rptDoc.Load(Server.MapPath(".")+"\\HR_rptCardIssue.rpt");
						
						string strSQL="WF_sprptIssueCardKey";
						
						/*if(Session["EmpID"]!=null)
						{
							strSQL+=" @EmpID='"+Session["EmpID"].ToString()+"'";
						}
						else strSQL+=" @EmpID=''";
						
						if(Session["EmpName"]!=null)
							if(Session["EmpName"].ToString().Length>0) strSQL+=",@EmpName=N'"+Session["EmpName"].ToString()+"'";						
						if(Session["Level1"]!=null)
							if(Session["Level1"].ToString().Length>0) strSQL+=",@Level1ID="+Session["Level1"].ToString();
						if(Session["Level2"]!=null)
							if(Session["Level2"].ToString().Length>0) strSQL+=",@Level2ID="+Session["Level2"].ToString();
						if(Session["Level3"]!=null)
							if(Session["Level3"].ToString().Length>0) strSQL+=",@Level3ID="+Session["Level3"].ToString();
						if(Session["Position"]!=null)
							if(Session["Position"].ToString().Length>0) strSQL+=",@PositionID="+Session["Position"].ToString();
						if(Session["Location"]!=null)
							if(Session["Location"].ToString().Length>0) strSQL+=",@LocationID="+Session["Location"].ToString();
						/*if(Session["FromDate"]!=null)
							if(Session["FromDate"].ToString().Length>0) strSQL+=",@FromDate='"+Session["FromDate"].ToString()+"'";
						if(Session["ToDate"]!=null)
							if(Session["ToDate"].ToString().Length>0) strSQL+=",@ToDate='"+Session["ToDate"].ToString()+"'";*/
						
						rptDoc.SetDataSource(clsCommon.GetDataTable(strSQL));											
						
						/*rptDoc.Subreports[0].SetDatabaseLogon(clsCommon.App_UserName,clsCommon.App_Password,clsCommon.App_ServerName,clsCommon.App_DatabaseName);
						
						ParameterValues pvCollection;
						ParameterDiscreteValue aParam=new ParameterDiscreteValue();

						pvCollection = rptDoc.DataDefinition.ParameterFields["TranningManager"].CurrentValues;
						aParam.Value=Session["Manager"].ToString();
						pvCollection.Add(aParam);
						rptDoc.DataDefinition.ParameterFields["TranningManager"].ApplyCurrentValues(pvCollection);

						aParam=new ParameterDiscreteValue();
						aParam.Value=Session["Assistant"].ToString();
						pvCollection = rptDoc.DataDefinition.ParameterFields["AssDirector"].CurrentValues;
						pvCollection.Add(aParam);						
						rptDoc.DataDefinition.ParameterFields["AssDirector"].ApplyCurrentValues(pvCollection);*/							
												                        														
						CrystalReportViewer1.ReportSource=rptDoc;
						CrystalReportViewer1.DataBind();

						/*aParam=null;
						pvCollection=null;*/
						break;		
					case "EQUIPMENT":
						rptDoc=null;
						rptDoc=new ReportDocument();						
						rptDoc.Load(Server.MapPath(".")+"\\Report_Moto.rpt");
						
						strSQL="WF_sprptEquip_Moto";
						
						if(Session["EmpID"]!=null)
						{
							strSQL+=" @EmpID='"+Session["EmpID"].ToString()+"'";
						}
						else strSQL+=" @EmpID=''";
						
						if(Session["EmpName"]!=null)
							if(Session["EmpName"].ToString().Length>0) strSQL+=",@EmpName=N'"+Session["EmpName"].ToString()+"'";						
						if(Session["Level1"]!=null)
							if(Session["Level1"].ToString().Length>0) strSQL+=",@Level1ID="+Session["Level1"].ToString();
						if(Session["Level2"]!=null)
							if(Session["Level2"].ToString().Length>0) strSQL+=",@Level2ID="+Session["Level2"].ToString();
						if(Session["Level3"]!=null)
							if(Session["Level3"].ToString().Length>0) strSQL+=",@Level3ID="+Session["Level3"].ToString();
						if(Session["Position"]!=null)
							if(Session["Position"].ToString().Length>0) strSQL+=",@PositionID="+Session["Position"].ToString();
						if(Session["Location"]!=null)
							if(Session["Location"].ToString().Length>0) strSQL+=",@LocationID="+Session["Location"].ToString();
						if(Session["FromDate"]!=null)
							if(Session["FromDate"].ToString().Length>0) strSQL+=",@FromDate='"+Session["FromDate"].ToString()+"'";
						if(Session["ToDate"]!=null)
							if(Session["ToDate"].ToString().Length>0) strSQL+=",@ToDate='"+Session["ToDate"].ToString()+"'";
						if(Session["EquipmentCode"]!=null)
							if(Session["EquipmentCode"].ToString().Length>0) strSQL+=",@EquipmentCode='"+Session["EquipmentCode"].ToString()+"'";
						
						rptDoc.SetDataSource(clsCommon.GetDataTable(strSQL));											
						
						/*rptDoc.Subreports[0].SetDatabaseLogon(clsCommon.App_UserName,clsCommon.App_Password,clsCommon.App_ServerName,clsCommon.App_DatabaseName);*/
						
						ParameterValues pvCollection;
						ParameterDiscreteValue aParam=new ParameterDiscreteValue();

						if(Session["FromDate"]!=null)
						{
							pvCollection = rptDoc.DataDefinition.ParameterFields["FromDate"].CurrentValues;
							aParam.Value=Session["FromDate"].ToString();
							pvCollection.Add(aParam);
							rptDoc.DataDefinition.ParameterFields["FromDate"].ApplyCurrentValues(pvCollection);
						}
						if(Session["ToDate"]!=null)
						{
							aParam=new ParameterDiscreteValue();
							aParam.Value=Session["ToDate"].ToString();
							pvCollection = rptDoc.DataDefinition.ParameterFields["ToDate"].CurrentValues;
							pvCollection.Add(aParam);						
							rptDoc.DataDefinition.ParameterFields["ToDate"].ApplyCurrentValues(pvCollection);							
						}	

						if(Session["EquipmentName"]!=null)
						{
							aParam=new ParameterDiscreteValue();
							aParam.Value=Session["EquipmentName"].ToString();
							pvCollection = rptDoc.DataDefinition.ParameterFields["EquipmentName"].CurrentValues;
							pvCollection.Add(aParam);						
							rptDoc.DataDefinition.ParameterFields["EquipmentName"].ApplyCurrentValues(pvCollection);							
						}
					                        														
						CrystalReportViewer1.ReportSource=rptDoc;
						CrystalReportViewer1.DataBind();

						/*aParam=null;
						pvCollection=null;*/
						break;
					case "UNIFORMRECEIPT":
						
						break;
					case "UNIFORMCHANGECASH":
						rptDoc=null;
						rptDoc=new ReportDocument();						
						rptDoc.Load(Server.MapPath(".")+"\\rpt_UniformCash.rpt");
						
						strSQL="WF_sprpt_UniformCash";
						
						/*if(Session["EmpID"]!=null)
						{
							strSQL+=" @EmpID='"+Session["EmpID"].ToString()+"'";
						}
						else strSQL+=" @EmpID=''";
						
						if(Session["EmpName"]!=null)
							if(Session["EmpName"].ToString().Length>0) strSQL+=",@EmpName=N'"+Session["EmpName"].ToString()+"'";						
						if(Session["Level1"]!=null)
							if(Session["Level1"].ToString().Length>0) strSQL+=",@Level1ID="+Session["Level1"].ToString();
						if(Session["Level2"]!=null)
							if(Session["Level2"].ToString().Length>0) strSQL+=",@Level2ID="+Session["Level2"].ToString();
						if(Session["Level3"]!=null)
							if(Session["Level3"].ToString().Length>0) strSQL+=",@Level3ID="+Session["Level3"].ToString();
						if(Session["Position"]!=null)
							if(Session["Position"].ToString().Length>0) strSQL+=",@PositionID="+Session["Position"].ToString();
						if(Session["Location"]!=null)
							if(Session["Location"].ToString().Length>0) strSQL+=",@LocationID="+Session["Location"].ToString();
						/*if(Session["FromDate"]!=null)
							if(Session["FromDate"].ToString().Length>0) strSQL+=",@FromDate='"+Session["FromDate"].ToString()+"'";
						if(Session["ToDate"]!=null)
							if(Session["ToDate"].ToString().Length>0) strSQL+=",@ToDate='"+Session["ToDate"].ToString()+"'";*/
						
						rptDoc.SetDataSource(clsCommon.GetDataTable(strSQL));											
						
						/*rptDoc.Subreports[0].SetDatabaseLogon(clsCommon.App_UserName,clsCommon.App_Password,clsCommon.App_ServerName,clsCommon.App_DatabaseName);
						
						ParameterValues pvCollection;
						ParameterDiscreteValue aParam=new ParameterDiscreteValue();

						pvCollection = rptDoc.DataDefinition.ParameterFields["TranningManager"].CurrentValues;
						aParam.Value=Session["Manager"].ToString();
						pvCollection.Add(aParam);
						rptDoc.DataDefinition.ParameterFields["TranningManager"].ApplyCurrentValues(pvCollection);

						aParam=new ParameterDiscreteValue();
						aParam.Value=Session["Assistant"].ToString();
						pvCollection = rptDoc.DataDefinition.ParameterFields["AssDirector"].CurrentValues;
						pvCollection.Add(aParam);						
						rptDoc.DataDefinition.ParameterFields["AssDirector"].ApplyCurrentValues(pvCollection);*/							
												                        														
						CrystalReportViewer1.ReportSource=rptDoc;
						CrystalReportViewer1.DataBind();

						/*aParam=null;
						pvCollection=null;*/
						
						break;
					case "PARKINGCHANGECASH":
						rptDoc=null;
						rptDoc=new ReportDocument();						
						rptDoc.Load(Server.MapPath(".")+"\\rpt_PackingCash.rpt");
						
						strSQL="WF_sprpt_PackingCash";
						
						/*if(Session["EmpID"]!=null)
						{
							strSQL+=" @EmpID='"+Session["EmpID"].ToString()+"'";
						}
						else strSQL+=" @EmpID=''";
						
						if(Session["EmpName"]!=null)
							if(Session["EmpName"].ToString().Length>0) strSQL+=",@EmpName=N'"+Session["EmpName"].ToString()+"'";						
						if(Session["Level1"]!=null)
							if(Session["Level1"].ToString().Length>0) strSQL+=",@Level1ID="+Session["Level1"].ToString();
						if(Session["Level2"]!=null)
							if(Session["Level2"].ToString().Length>0) strSQL+=",@Level2ID="+Session["Level2"].ToString();
						if(Session["Level3"]!=null)
							if(Session["Level3"].ToString().Length>0) strSQL+=",@Level3ID="+Session["Level3"].ToString();
						if(Session["Position"]!=null)
							if(Session["Position"].ToString().Length>0) strSQL+=",@PositionID="+Session["Position"].ToString();
						if(Session["Location"]!=null)
							if(Session["Location"].ToString().Length>0) strSQL+=",@LocationID="+Session["Location"].ToString();
						/*if(Session["FromDate"]!=null)
							if(Session["FromDate"].ToString().Length>0) strSQL+=",@FromDate='"+Session["FromDate"].ToString()+"'";
						if(Session["ToDate"]!=null)
							if(Session["ToDate"].ToString().Length>0) strSQL+=",@ToDate='"+Session["ToDate"].ToString()+"'";*/
						
						rptDoc.SetDataSource(clsCommon.GetDataTable(strSQL));											
						
						/*rptDoc.Subreports[0].SetDatabaseLogon(clsCommon.App_UserName,clsCommon.App_Password,clsCommon.App_ServerName,clsCommon.App_DatabaseName);
						
						ParameterValues pvCollection;
						ParameterDiscreteValue aParam=new ParameterDiscreteValue();

						pvCollection = rptDoc.DataDefinition.ParameterFields["TranningManager"].CurrentValues;
						aParam.Value=Session["Manager"].ToString();
						pvCollection.Add(aParam);
						rptDoc.DataDefinition.ParameterFields["TranningManager"].ApplyCurrentValues(pvCollection);

						aParam=new ParameterDiscreteValue();
						aParam.Value=Session["Assistant"].ToString();
						pvCollection = rptDoc.DataDefinition.ParameterFields["AssDirector"].CurrentValues;
						pvCollection.Add(aParam);						
						rptDoc.DataDefinition.ParameterFields["AssDirector"].ApplyCurrentValues(pvCollection);*/							
												                        														
						CrystalReportViewer1.ReportSource=rptDoc;
						CrystalReportViewer1.DataBind();

						/*aParam=null;
						pvCollection=null;*/
						
						break;
					case "STOCKCARD":						
						rptDoc=null;
						rptDoc=new ReportDocument();						
						rptDoc.Load(Server.MapPath(".")+"\\HR_RPPTStockcard.rpt");
						
						strSQL="WF_sprptEquipStkCard";
						
                        if(Session["EmpID"]!=null)
						{
							strSQL+=" @EmpID='"+Session["EmpID"].ToString()+"'";
						}
						else strSQL+=" @EmpID=''";
						
						if(Session["EmpName"]!=null)
							if(Session["EmpName"].ToString().Length>0) strSQL+=",@EmpName=N'"+Session["EmpName"].ToString()+"'";						
						if(Session["Level1"]!=null)
							if(Session["Level1"].ToString().Length>0) strSQL+=",@Level1ID="+Session["Level1"].ToString();
						if(Session["Level2"]!=null)
							if(Session["Level2"].ToString().Length>0) strSQL+=",@Level2ID="+Session["Level2"].ToString();
						if(Session["Level3"]!=null)
							if(Session["Level3"].ToString().Length>0) strSQL+=",@Level3ID="+Session["Level3"].ToString();
						if(Session["Position"]!=null)
							if(Session["Position"].ToString().Length>0) strSQL+=",@PositionID="+Session["Position"].ToString();
						if(Session["Location"]!=null)
							if(Session["Location"].ToString().Length>0) strSQL+=",@LocationID="+Session["Location"].ToString();
						if(Session["FromDate"]!=null)
							if(Session["FromDate"].ToString().Length>0) strSQL+=",@FromDate1='"+Session["FromDate"].ToString()+"'";
						if(Session["ToDate"]!=null)
							if(Session["ToDate"].ToString().Length>0) strSQL+=",@ToDate1='"+Session["ToDate"].ToString()+"'";
						if(Session["EquipmentCode"]!=null)
							if(Session["EquipmentCode"].ToString().Length>0) strSQL+=",@EquipmentCode='"+Session["EquipmentCode"].ToString()+"'";
						
						rptDoc.SetDataSource(clsCommon.GetDataTable(strSQL));											
						
						/*rptDoc.Subreports[0].SetDatabaseLogon(clsCommon.App_UserName,clsCommon.App_Password,clsCommon.App_ServerName,clsCommon.App_DatabaseName);*/
												
						aParam=new ParameterDiscreteValue();

						pvCollection = rptDoc.DataDefinition.ParameterFields["eQUIPMENTNAME"].CurrentValues;
						aParam.Value=Session["EquipmentName"].ToString();
						pvCollection.Add(aParam);
						rptDoc.DataDefinition.ParameterFields["EquipmentName"].ApplyCurrentValues(pvCollection);

						if(Session["FromDate"]!=null)
						{
							aParam=new ParameterDiscreteValue();
							aParam.Value=Session["FromDate"].ToString();
							pvCollection = rptDoc.DataDefinition.ParameterFields["FromDate"].CurrentValues;
							pvCollection.Add(aParam);						
							rptDoc.DataDefinition.ParameterFields["FromDate"].ApplyCurrentValues(pvCollection);
						}
						if(Session["ToDate"]!=null)
						{
							aParam=new ParameterDiscreteValue();
							aParam.Value=Session["ToDate"].ToString();
							pvCollection = rptDoc.DataDefinition.ParameterFields["ToDate"].CurrentValues;
							pvCollection.Add(aParam);						
							rptDoc.DataDefinition.ParameterFields["ToDate"].ApplyCurrentValues(pvCollection);
						}
						                        														
						CrystalReportViewer1.ReportSource=rptDoc;
						CrystalReportViewer1.DataBind();

						/*aParam=null;
						pvCollection=null;*/
						
						break;
					case "BENEFITEMP":
						rptDoc=null;
						rptDoc=new ReportDocument();						
						rptDoc.Load(Server.MapPath(".")+"\\WF_sprptBenefitEmp.rpt");
						
						strSQL="WF_sprptBenefitEmp";
						
						/*if(Session["EmpID"]!=null)
						{
							strSQL+=" @EmpID='"+Session["EmpID"].ToString()+"'";
						}
						else strSQL+=" @EmpID=''";
						
						if(Session["EmpName"]!=null)
							if(Session["EmpName"].ToString().Length>0) strSQL+=",@EmpName=N'"+Session["EmpName"].ToString()+"'";						
						if(Session["Level1"]!=null)
							if(Session["Level1"].ToString().Length>0) strSQL+=",@Level1ID="+Session["Level1"].ToString();
						if(Session["Level2"]!=null)
							if(Session["Level2"].ToString().Length>0) strSQL+=",@Level2ID="+Session["Level2"].ToString();
						if(Session["Level3"]!=null)
							if(Session["Level3"].ToString().Length>0) strSQL+=",@Level3ID="+Session["Level3"].ToString();
						if(Session["Position"]!=null)
							if(Session["Position"].ToString().Length>0) strSQL+=",@PositionID="+Session["Position"].ToString();
						if(Session["Location"]!=null)
							if(Session["Location"].ToString().Length>0) strSQL+=",@LocationID="+Session["Location"].ToString();
						/*if(Session["FromDate"]!=null)
							if(Session["FromDate"].ToString().Length>0) strSQL+=",@FromDate='"+Session["FromDate"].ToString()+"'";
						if(Session["ToDate"]!=null)
							if(Session["ToDate"].ToString().Length>0) strSQL+=",@ToDate='"+Session["ToDate"].ToString()+"'";*/
						
						rptDoc.SetDataSource(clsCommon.GetDataTable(strSQL));											
						
						/*rptDoc.Subreports[0].SetDatabaseLogon(clsCommon.App_UserName,clsCommon.App_Password,clsCommon.App_ServerName,clsCommon.App_DatabaseName);
						
						ParameterValues pvCollection;
						ParameterDiscreteValue aParam=new ParameterDiscreteValue();

						pvCollection = rptDoc.DataDefinition.ParameterFields["TranningManager"].CurrentValues;
						aParam.Value=Session["Manager"].ToString();
						pvCollection.Add(aParam);
						rptDoc.DataDefinition.ParameterFields["TranningManager"].ApplyCurrentValues(pvCollection);

						aParam=new ParameterDiscreteValue();
						aParam.Value=Session["Assistant"].ToString();
						pvCollection = rptDoc.DataDefinition.ParameterFields["AssDirector"].CurrentValues;
						pvCollection.Add(aParam);						
						rptDoc.DataDefinition.ParameterFields["AssDirector"].ApplyCurrentValues(pvCollection);*/							
												                        														
						CrystalReportViewer1.ReportSource=rptDoc;
						CrystalReportViewer1.DataBind();

						/*aParam=null;
						pvCollection=null;*/

						break;
					case "HEALTHCHECKED":
						rptDoc=null;
						rptDoc=new ReportDocument();						
						rptDoc.Load(Server.MapPath(".")+"\\WF_sprptHealthCheck.rpt");
						
						strSQL="WF_sprptHealthCheck";
						
						/*if(Session["EmpID"]!=null)
						{
							strSQL+=" @EmpID='"+Session["EmpID"].ToString()+"'";
						}
						else strSQL+=" @EmpID=''";
						
						if(Session["EmpName"]!=null)
							if(Session["EmpName"].ToString().Length>0) strSQL+=",@EmpName=N'"+Session["EmpName"].ToString()+"'";						
						if(Session["Level1"]!=null)
							if(Session["Level1"].ToString().Length>0) strSQL+=",@Level1ID="+Session["Level1"].ToString();
						if(Session["Level2"]!=null)
							if(Session["Level2"].ToString().Length>0) strSQL+=",@Level2ID="+Session["Level2"].ToString();
						if(Session["Level3"]!=null)
							if(Session["Level3"].ToString().Length>0) strSQL+=",@Level3ID="+Session["Level3"].ToString();
						if(Session["Position"]!=null)
							if(Session["Position"].ToString().Length>0) strSQL+=",@PositionID="+Session["Position"].ToString();
						if(Session["Location"]!=null)
							if(Session["Location"].ToString().Length>0) strSQL+=",@LocationID="+Session["Location"].ToString();
						/*if(Session["FromDate"]!=null)
							if(Session["FromDate"].ToString().Length>0) strSQL+=",@FromDate='"+Session["FromDate"].ToString()+"'";
						if(Session["ToDate"]!=null)
							if(Session["ToDate"].ToString().Length>0) strSQL+=",@ToDate='"+Session["ToDate"].ToString()+"'";*/
						
						rptDoc.SetDataSource(clsCommon.GetDataTable(strSQL));											
						
						/*rptDoc.Subreports[0].SetDatabaseLogon(clsCommon.App_UserName,clsCommon.App_Password,clsCommon.App_ServerName,clsCommon.App_DatabaseName);
						
						ParameterValues pvCollection;
						ParameterDiscreteValue aParam=new ParameterDiscreteValue();

						pvCollection = rptDoc.DataDefinition.ParameterFields["TranningManager"].CurrentValues;
						aParam.Value=Session["Manager"].ToString();
						pvCollection.Add(aParam);
						rptDoc.DataDefinition.ParameterFields["TranningManager"].ApplyCurrentValues(pvCollection);

						aParam=new ParameterDiscreteValue();
						aParam.Value=Session["Assistant"].ToString();
						pvCollection = rptDoc.DataDefinition.ParameterFields["AssDirector"].CurrentValues;
						pvCollection.Add(aParam);						
						rptDoc.DataDefinition.ParameterFields["AssDirector"].ApplyCurrentValues(pvCollection);*/							
												                        														
						CrystalReportViewer1.ReportSource=rptDoc;
						CrystalReportViewer1.DataBind();

						/*aParam=null;
						pvCollection=null;*/

						break;
					default:
						break;
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
