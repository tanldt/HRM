using System;
using System.Data;
using System.Web.UI.WebControls;
using iHRPCore.Com;
using System.Web;
using System.Web.Caching;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;
using System.Data.SqlClient;
using System.Configuration;

namespace iHRPCore.REComponent
{
	/// <summary>
	/// Summary description for clsRE.
	/// </summary>
	
	public class clsREProjectCandidate
	{		
		public static DataRow getDataByDemandCode(string sProjectCode,System.Web.UI.Page pPage)
		{
			DataRow drData;
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			try
			{
				drData = clsCommon.GetDataRow("RE_spfrmDemandProject @Activity='getDataByProjectCode',@ProjectCode = N'" + sProjectCode + "',@UserGroupID=N'" + sAccountLogin + "'");
				return drData;
			}
			catch
			{
				return null;
			}
		}		
		public static string AddToProject(DataGrid dtgList, string pProjectID, string pDemandID)
		{
			string strErr = "";
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);		
			SQLconn.Open();
			cmd.Connection = SQLconn;
			cmd.Transaction= SQLconn.BeginTransaction();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "RE_spfrmProjectCandidate";
			CheckBox chkSelect = new CheckBox();
			
			try
			{
				for(int i=0;i<dtgList.Items.Count;i++)
				{
					chkSelect = (CheckBox)dtgList.Items[i].FindControl("chkSelect");			
					// CHECK IF SELECTED
					if(chkSelect.Checked==true)
					{						
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Save";							
						cmd.Parameters.Add("@ProjectID",SqlDbType.NVarChar,12).Value = pProjectID;

						if (pDemandID=="")
							cmd.Parameters.Add("@DemandID",SqlDbType.NVarChar,12).Value = DBNull.Value; 							
						else
							cmd.Parameters.Add("@DemandID",SqlDbType.NVarChar,12).Value = pDemandID; 							

						cmd.Parameters.Add("@CandidateID",SqlDbType.NVarChar,12).Value = dtgList.Items[i].Cells[0].Text.Trim();
						cmd.Parameters.Add("@Especial",SqlDbType.Bit).Value = ((CheckBox)dtgList.Items[i].FindControl("chkEspecial")).Checked==true?1:0;
						cmd.ExecuteNonQuery();												
					}
				}
				cmd.Transaction.Commit();
				return strErr;
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
			}
			catch (Exception exp)
			{
				strErr = exp.Message;
				cmd.Transaction.Rollback();			
			}
			chkSelect.Dispose();
			return strErr;
		}
		
		public static DataTable getDataByProjectID(string pProjectID, string sLanguageID) 
		{
			DataTable dt= new DataTable();
			try
			{
				dt= clsCommon.GetDataTable("RE_spfrmPROJECTCANDIDATE @Activity='getDataByProjectID', @ProjectID='" + pProjectID + "',@languageID='" + sLanguageID + "'");
				return dt;
			}
			catch
			{
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		
		public static DataTable getRecordCanInfo(string CanID,string sLanguageID, System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			DataTable dt= new DataTable();
			try
			{
				dt= clsCommon.GetDataTable("RE_clsCommon @Activity='getRecordCanInfo', @CandidateID='" + CanID + "',@language='" + sLanguageID + "',@UserGroupID='" + sAccountLogin + "'");
				return dt;
			}
			catch
			{
				return null;
			}
			finally
			{
				dt.Dispose();
			}
		}
		public static bool checkBtnSave(string pProjectID)
		{
			DataTable dtData= clsCommon.GetDataTable("RE_spfrmPROJECTCANDIDATE @Activity='checkBtnSave',@ProjectID='" + pProjectID + "'");
				if (dtData.Rows.Count>0)
				{
					return false;
				}
				else
				{
					return true;
				}
		}
	}	
	public class clsRERecordShortList
	{
		public static DataRow getDatabyCanID(string CanID, string sProjectID, string sSeq)
		{
			DataRow drData;
			try
			{
				drData= clsCommon.GetDataRow("RE_spfrmCandidateTournament @Activity='getDatabyCanID_0', @CandidateID='" + CanID + "',@ProjectID='" + sProjectID + "',@Seq=" + sSeq + "");
				return drData;
			}
			catch
			{
				return null;
			}
		}		
		public static DataRow getTourNext(string sProjectID, string Seq)
		{
			DataRow drData;
			try
			{
				drData=clsCommon.GetDataRow("RE_spfrmCANDIDATETOURNAMENT @Activity='getTourNext',@Seq=" + Seq + ", @ProjectID='" + sProjectID + "'");
				return drData;
			}
			catch
			{
				return null;
			}
			finally
			{
			}
		}
		public static DataRow getDataByType(string Type, string sProjectID, string sCandidateID)
		{
			try
			{				
				DataRow drData= clsCommon.GetDataRow("RE_spfrmPROJECTTOURNAMENT @Activity='getDataByType',@Type=" + Type + ", @ProjectID='" + sProjectID + "',@CandidateID='" + sCandidateID + "'");
				return drData;
			}
			catch
			{
				return null;
			}
		}
		public static string getTypeBySeq(string pProjectID, string sSeq)
		{
			try
			{
				DataRow Drdata= clsCommon.GetDataRow("RE_spfrmPROJECTTOURNAMENT @Activity='getTypeBySeq',@ProjectID='" + pProjectID + "',@Seq=" + sSeq + "");
					if (Drdata!=null)
					{
						return Drdata["Type"].ToString();
					}
					else
					{
						return "0";
					}
			}
			catch
			{
				return "0";
			}
		}
		public static void SetSessAfterSave(string sPass, bool bEspecial,string sEspecial, string sProjectID, string sSeq, System.Web.UI.Page pPage)
		{
			if (sPass=="1")
			{
				pPage.Session["Seq"]=int.Parse(sSeq);//+1;				
			}
			if (bEspecial)
			{
				pPage.Session["Seq"]=sEspecial;
			}
			if (sPass=="1" || bEspecial==true)
			{
				DataRow drData = clsRERecordShortList.getTourNext(sProjectID,pPage.Session["Seq"].ToString());
				if (drData!=null)
				{
					pPage.Session["LSTournamentID"]=drData["LSTournamentID"].ToString();
				}
			}
		}
		public static int getSeqByProject(string pProjectID)
		{
			try
			{
				DataRow Drdata = clsCommon.GetDataRow("RE_spfrmPROJECTTOURNAMENT @Activity='getSeqByProject',@ProjectID='" + pProjectID + "'");
				if (Drdata!=null)
				{
					return int.Parse(Drdata["seq"].ToString());
				}
				else
				{
					return 0;
				}
			}
			catch
			{
				return 0;
			}
		}
		public static string getSeqLSTournament(string pValue, int pType)
		{
			// 1- Lấy Tournament, 0 lấy Seq
			if (pType==1)
			{
				return pValue.Substring(0,pValue.IndexOf('@'));
			}
			else
			{
				return pValue.Substring(pValue.IndexOf('@'),pValue.Length-pValue.IndexOf('@'));
			}
		}
	}
	public class clsRERecordExamination
	{
		public static DataTable getDatabyCan(string sCandidateTournamentID,string sProjectID, string sSeq)
		{
			DataTable dtData=new DataTable();
			try
			{
				dtData= clsCommon.GetDataTable("RE_spfrmCANDIDATETOURNAMENT @Activity='getDatabyCan_S',@CandidateTournamentID='" + sCandidateTournamentID + "',@ProjectID='" + sProjectID + "', @Seq=" + sSeq + "");
				return dtData;
			}
			catch
			{
				return null;
			}
			finally
			{
				dtData.Dispose();
			}
		}
		public static string getCandidateTournamentID(string sProjectID, string sCandidateID, string sSeq)
		{
			try
			{
				DataRow drData= clsCommon.GetDataRow("RE_spfrmCANDIDATETOURNAMENT @Activity='getCandidateTournamentID', @ProjectID='" + sProjectID + "', @CandidateID='" + sCandidateID + "', @Seq="  + sSeq + "");
				if (drData!=null)				
					return drData["CandidateTournamentID"].ToString();
				else 
					return "";
			}
			catch(Exception ex)
			{
				return ex.ToString();
			}

		}
		public static string sImpact_Subject(DataGrid dtgList, string sCandidateTournamentID)
		{
			string strErr = "";
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);		
			SQLconn.Open();
			cmd.Connection = SQLconn;
			cmd.Transaction= SQLconn.BeginTransaction();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "RE_spfrmCandidateTourSubject";
			CheckBox chkSelect = new CheckBox();
			
			try
			{
				for(int i=0;i<dtgList.Items.Count;i++)
				{						
					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Save";							
					cmd.Parameters.Add("@CandidateTournamentID",SqlDbType.NVarChar,12).Value = sCandidateTournamentID;
					cmd.Parameters.Add("@LSRecruitSubjectID",SqlDbType.NVarChar,12).Value = dtgList.Items[i].Cells[0].Text.Trim();
					cmd.Parameters.Add("@PassPoint",SqlDbType.Decimal).Value = ((TextBox)dtgList.Items[i].FindControl("txtPassPointSub")).Text;								
					cmd.Parameters.Add("@Coefficient",SqlDbType.Decimal).Value = ((TextBox)dtgList.Items[i].FindControl("txtCoefficientSub")).Text;							
					cmd.Parameters.Add("@Point",SqlDbType.Decimal).Value = ((TextBox)dtgList.Items[i].FindControl("txtPointSub")).Text;								
					cmd.Parameters.Add("@Note",SqlDbType.NVarChar,255).Value = ((TextBox)dtgList.Items[i].FindControl("txtNoteSub")).Text;								
					cmd.Parameters.Add("@Result",SqlDbType.TinyInt).Value = ((CheckBox)dtgList.Items[i].FindControl("chkEspeciallySub")).Checked==true?2:((CheckBox)dtgList.Items[i].FindControl("chkPassSub")).Checked==true?1:0;								

					cmd.ExecuteNonQuery();
							
					
				}
				cmd.Transaction.Commit();
				return strErr;
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
			}
			catch (Exception exp)
			{
				strErr = exp.Message;
				cmd.Transaction.Rollback();			
			}
			chkSelect.Dispose();
			return strErr;
		}		
	}

	public class clsRERecordInterview
	{
		public static DataTable getDatabyCan(string sCandidateTournamentID, string sProjectID, string sSeq)
		{
			DataTable dtData=new DataTable();
			try
			{
				dtData= clsCommon.GetDataTable("RE_spfrmCANDIDATETOURNAMENT @Activity='getDatabyCan_T',@CandidateTournamentID='" + sCandidateTournamentID + "',@ProjectID='" + sProjectID + "', @Seq=" + sSeq + "");
				return dtData;
			}
			catch
			{
				return null;
			}
			finally
			{
				dtData.Dispose();
			}
		}
		public static string sImpact_Subject(DataGrid dtgList, string sCandidateTournamentID)
		{
			string strErr = "";
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);		
			SQLconn.Open();
			cmd.Connection = SQLconn;
			cmd.Transaction= SQLconn.BeginTransaction();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "RE_spfrmCANDIDATETOURINTERVIEW";
			CheckBox chkSelect = new CheckBox();
			string strPoint="";
			try
			{
				for(int i=0;i<dtgList.Items.Count;i++)
				{		
					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "Save";							
					cmd.Parameters.Add("@CandidateTournamentID",SqlDbType.NVarChar,12).Value = sCandidateTournamentID;
					cmd.Parameters.Add("@LSInterviewContentID",SqlDbType.NVarChar,12).Value = dtgList.Items[i].Cells[0].Text.Trim();
					cmd.Parameters.Add("@Point",SqlDbType.Float).Value =((TextBox)dtgList.Items[i].FindControl("txtMark")).Text;								
					cmd.Parameters.Add("@Note",SqlDbType.NVarChar,255).Value = ((TextBox)dtgList.Items[i].FindControl("txtNoteSub")).Text;								
					cmd.ExecuteNonQuery();
				}
				cmd.Transaction.Commit();
				return strErr;
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
			}
			catch (Exception exp)
			{
				strErr = exp.Message;
				cmd.Transaction.Rollback();			
			}
			chkSelect.Dispose();
			return strErr;
		}
		private static string getcheckResult(bool A,bool B,bool C,bool D )
		{
			try
			{
				int iReturn = (A==true?1:0) + (B==true?2:0) + (C==true?3:0) + (D==true?4:0);
				return iReturn.ToString();
			}
			catch
			{
				return "0";
			}
		}
	}
	public class clsREListCan
	{
		public static DataTable getListCan(string strCandidateCode, string strCandidateName,string strProjectCode,string strDemandID,string strLanguage , string sType)
		{
			DataTable dtData= new DataTable();
			try
			{
				dtData= clsCommon.GetDataTable("RE_spfrmListCan @Activity='GetCanList',@CandidateCode=N'" + strCandidateCode + "',@CandidateName=N'" + strCandidateName + "',@ProjectCode=N'" + strProjectCode + "',@DemandID='" + strDemandID + "',@LanguageID='" + strLanguage + "',@Type='" + sType + "'");
				return dtData;
			}
			catch
			{
				return null;
			}
			finally
			{
				dtData.Dispose();
			}
		}
		public static string sTranfer(DataGrid dtgList)
		{
			string sErrMess="";
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			SQLconn.Open();
			try
			{
				
				cmd.Connection = SQLconn;
				cmd.Transaction= SQLconn.BeginTransaction();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "RE_spfrmListCan";
   
				CheckBox obj = new CheckBox();
				for (int i = 0;i<= dtgList.Items.Count - 1;i++)
				{
					obj = (CheckBox) dtgList.Items[i].FindControl("chkSelect");					
					if (obj.Checked)
					{
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "Tranfer";					

						cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value = ((TextBox)dtgList.Items[i].FindControl("txtEmpID")).Text.Trim();   
						cmd.Parameters.Add("@CandidateID", SqlDbType.NVarChar, 12).Value = dtgList.Items[i].Cells[0].Text.Trim();
						cmd.Parameters.Add("@Note", SqlDbType.NVarChar, 255).Value = ((TextBox)dtgList.Items[i].FindControl("txtNote")).Text.Trim();   						
						cmd.Parameters.Add("@ReturnMess", SqlDbType.NVarChar,1000).Direction = ParameterDirection.InputOutput;
						cmd.Parameters.Add("@LanguageID", SqlDbType.NChar,2).Value = "VN";
						cmd.ExecuteNonQuery();							
					}
				}
				sErrMess= cmd.Parameters["@ReturnMess"].Value.ToString();
				cmd.Transaction.Commit();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();	
				if (sErrMess!="")
					return sErrMess;
				else
					return "";
				
			}
			catch (Exception exp)
			{
				string strErr = exp.Message;
				cmd.Transaction.Rollback();
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
				return strErr;
			}
		}
	}
	public class clsREDemandCandidate
	{
		public static DataTable getDataSetDemand(string sProjectID, string sDemandID)
		{
			DataTable dtData= new DataTable();
			try
			{
				dtData= clsCommon.GetDataTable("RE_spfrmDEMANDPROJECT @Activity='getDataSetDemand',@ProjectID='" + sProjectID + "',@DemandID='" + sDemandID + "'");
				return dtData;
			}
			catch
			{
				return null;
			}
			finally
			{
				dtData.Dispose();
			}
		}
		public static string ImpactDemandProject(DataGrid dtgList, string sProjectID, string sDemandID, string sActivity,string sCheckName)
		{
			string strErr = "";
			SqlCommand  cmd = new SqlCommand();
			SqlConnection SQLconn= new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);		
			SQLconn.Open();
			cmd.Connection = SQLconn;
			cmd.Transaction= SQLconn.BeginTransaction();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "RE_spfrmDEMANDPROJECT";
			CheckBox chkSelect = new CheckBox();
			//vuonglm: kiem tra co check chon Nhan vien chua
			int dem=1;
			for(int i=0;i<dtgList.Items.Count;i++)
			{
				chkSelect = (CheckBox)dtgList.Items[i].FindControl(sCheckName);					
				if(chkSelect.Checked==true)
					dem++;
			}
			if(dem==1)
				return strErr="Check chon nhan vien!";
			//vuonglm
			try
			{
				for(int i=0;i<dtgList.Items.Count;i++)
				{
					chkSelect = (CheckBox)dtgList.Items[i].FindControl(sCheckName);			
					// CHECK IF SELECTED
					if(chkSelect.Checked==true)
					{						
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = sActivity;							
						cmd.Parameters.Add("@ProjectID",SqlDbType.NVarChar,12).Value = sProjectID;
						cmd.Parameters.Add("@DemandID",SqlDbType.NVarChar,12).Value = sDemandID;
						cmd.Parameters.Add("@CandidateID",SqlDbType.NVarChar,12).Value = dtgList.Items[i].Cells[0].Text.Trim();							
						cmd.ExecuteNonQuery();
							
					}
				}
				cmd.Transaction.Commit();				
				cmd.Dispose();
				SQLconn.Close();
				SQLconn.Dispose();
			}
			catch (Exception exp)
			{
				strErr = exp.Message;
				cmd.Transaction.Rollback();			
			}
			chkSelect.Dispose();
			return strErr;
		}
	}
}
