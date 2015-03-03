using System;
using System.Web.Mail;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using iHRPCore.Com;
using iHRPCore.Include;
using FileLogger;

namespace iHRPCore.SendMail
{
	/// <summary>
	/// Summary description for SendMail.
	/// </summary>
	public class clsSendMail
	{
		public static bool SendMail(string strFromMail, string strToEmail,string strSubject,string strBody,ArrayList attachFiles)
		{
			try
			{
				System.Web.Mail.MailMessage Message  = new System.Web.Mail.MailMessage();
				MailAttachment mailFile;
	
				Message.To = strToEmail;
				Message.From = strFromMail;
				Message.Subject = strSubject;
				Message.BodyEncoding = System.Text.Encoding.UTF8;
				Message.BodyFormat = MailFormat.Html;
				Message.Body = strBody;

				if (attachFiles!=null)
				{
					for (int i=0;i<attachFiles.Count;i++)
					{
						mailFile = new MailAttachment((string)attachFiles[i]);
						Message.Attachments.Add(mailFile);
					}
				}
				
//				Message.Fields["http://schemas.microsoft.com/cdo/configuration/smtpserver"]= ConfigurationSettings.AppSettings["pstrMailServer"].Trim();
//				Message.Fields["http://schemas.microsoft.com/cdo/configuration/smtpserverport"] = 25;
//				Message.Fields["http://schemas.microsoft.com/cdo/configuration/sendusing"]  = 2;
//				Message.Fields["http://schemas.microsoft.com/cdo/configuration/smtpauthenticate"] = 1;
//				Message.Fields["http://schemas.microsoft.com/cdo/configuration/sendusername"] = ConfigurationSettings.AppSettings["pstrSendUser"].Trim();
//			
//				Message.Fields["http://schemas.microsoft.com/cdo/configuration/sendpassword"] = ConfigurationSettings.AppSettings["pstrSendPassword"].Trim();
				

				SmtpMail.SmtpServer = ConfigurationSettings.AppSettings["pstrMailServer"].Trim();;
				SmtpMail.Send(Message);
				return true;
			}
			catch(System.Web.HttpException ehttp)
			{
				return false;
			}
		}
		public static string GetEmailAddress(string sEmpID)
		{
			DataRow drData = clsCommon.GetDataRow("HR_clsCommon @Activity='GetEmail',@EmpID='" + sEmpID + "'");
			return drData["Email"].ToString();
		}
		#region send mail CC
		public static bool SendMailApprove__CC(string strEmpIDFromMail, string strEmpIDToMail,string strEmpIDCCMail ,string strSubject,string strBody,ArrayList attachFiles)
		{
			clsLogFile logFile = new clsLogFile();
			string strFromMail=GetEmailAddress(strEmpIDFromMail);
			string strToEmail=GetEmailAddress(strEmpIDToMail);
			string strCCEmail= "";
			string fileLog="";
			try
			{
				string[] strCCToEmpTemp = strEmpIDCCMail.Split(new char [] {';'});				

				strCCEmail= "";//GetEmailAddress(strEmpIDCCMail);
				for(int i = 0; i<strCCToEmpTemp.Length; i++)
				{
					if(i==0)
					{
						strCCEmail = GetEmailAddress(strCCToEmpTemp[i].ToString());
					}
					else
					{
						strCCEmail = strCCEmail + ";" + GetEmailAddress(strCCToEmpTemp[i].ToString());
					}
				
				}
				fileLog=ConfigurationSettings.AppSettings["pstrLogFile"];
				//fileLog="E:\\EmaillogFile.log";
				logFile.OutLogger(fileLog,"From: " + strFromMail + " - To:" + strToEmail + "-CC:" + strCCEmail,strSubject);

				System.Web.Mail.MailMessage Message  = new System.Web.Mail.MailMessage();
				MailAttachment mailFile;
	
				Message.To = strToEmail;
				Message.From = strFromMail;
				Message.Cc = strCCEmail + ";HRPM3@pm3power.com.vn;" + strFromMail; 
				//Message.Cc = strCCEmail + ";cangtt@fpt.com.vn;" + strFromMail; 
				Message.Subject = strSubject;
				Message.BodyEncoding = System.Text.Encoding.UTF8;
				Message.BodyFormat = MailFormat.Html;
				Message.Body = strBody;

				if (attachFiles!=null)
				{
					for (int i=0;i<attachFiles.Count;i++)
					{
						mailFile = new MailAttachment((string)attachFiles[i]);
						Message.Attachments.Add(mailFile);
					}
				}
				/*
				Message.Fields["http://schemas.microsoft.com/cdo/configuration/smtpserver"]= ConfigurationSettings.AppSettings["pstrMailServer"].Trim();
				Message.Fields["http://schemas.microsoft.com/cdo/configuration/smtpserverport"] = 25;
				Message.Fields["http://schemas.microsoft.com/cdo/configuration/sendusing"]  = 2;
				Message.Fields["http://schemas.microsoft.com/cdo/configuration/smtpauthenticate"] = 1;
				Message.Fields["http://schemas.microsoft.com/cdo/configuration/sendusername"] = ConfigurationSettings.AppSettings["pstrSendUser"].Trim();
			
				Message.Fields["http://schemas.microsoft.com/cdo/configuration/sendpassword"] = ConfigurationSettings.AppSettings["pstrSendPassword"].Trim();
				*/

				SmtpMail.SmtpServer = ConfigurationSettings.AppSettings["pstrMailServer"].Trim();;
				SmtpMail.Send(Message);
				return true;
			}
			catch(System.Web.HttpException ehttp)
			{
				logFile.OutLogger(ConfigurationSettings.AppSettings["pstrLogFile"].ToString(),"Error: From: " + strFromMail + " - To:" + strToEmail + "CC: " + strCCEmail  +  " - content:" + ehttp.Message.ToString(),strSubject);
				return false;
			}
		}
		#endregion
		public static bool SendMailApprove(string strEmpIDFromMail, string strEmpIDToMail,string strSubject,string strBody,ArrayList attachFiles)
		{
			clsLogFile logFile = new clsLogFile();
			string strFromMail=GetEmailAddress(strEmpIDFromMail);
			string strToEmail=GetEmailAddress(strEmpIDToMail);
			string fileLog="";

			try
			{
				fileLog=ConfigurationSettings.AppSettings["pstrLogFile"];
				//fileLog="E:\\EmaillogFile.log";
				logFile.OutLogger(fileLog,"From: " + strFromMail + " - To:" + strToEmail,strSubject);
				
				System.Web.Mail.MailMessage Message  = new System.Web.Mail.MailMessage();
				MailAttachment mailFile;
	
				Message.To = strToEmail;
				Message.From = strFromMail;
				Message.Cc="Cangtt@fpt.com.vn";
				Message.Subject = strSubject;
				Message.BodyEncoding = System.Text.Encoding.UTF8;
				Message.BodyFormat = MailFormat.Html;
				Message.Body = strBody;

				if (attachFiles!=null)
				{
					for (int i=0;i<attachFiles.Count;i++)
					{
						mailFile = new MailAttachment((string)attachFiles[i]);
						Message.Attachments.Add(mailFile);
					}
				}
				/*
				Message.Fields["http://schemas.microsoft.com/cdo/configuration/smtpserver"]= ConfigurationSettings.AppSettings["pstrMailServer"].Trim();
				Message.Fields["http://schemas.microsoft.com/cdo/configuration/smtpserverport"] = 25;
				Message.Fields["http://schemas.microsoft.com/cdo/configuration/sendusing"]  = 2;
				Message.Fields["http://schemas.microsoft.com/cdo/configuration/smtpauthenticate"] = 1;
				Message.Fields["http://schemas.microsoft.com/cdo/configuration/sendusername"] = ConfigurationSettings.AppSettings["pstrSendUser"].Trim();
			
				Message.Fields["http://schemas.microsoft.com/cdo/configuration/sendpassword"] = ConfigurationSettings.AppSettings["pstrSendPassword"].Trim();
				*/

				SmtpMail.SmtpServer = ConfigurationSettings.AppSettings["pstrMailServer"].Trim();;
				SmtpMail.Send(Message);
				return true;
			}
			catch(System.Web.HttpException ehttp)
			{
				logFile.OutLogger(ConfigurationSettings.AppSettings["pstrLogFile"].ToString(),"Lỗi: From: " + strFromMail + " - To:" + strToEmail + " - Nội dung:" + ehttp.Message.ToString(),strSubject);
				return false;
			}
		}

		public static bool SendMail_template(string strEmpIDFromMail, string strEmpIDToMail,string strSubject,string strBody,ArrayList attachFiles,string sMailTemplateID)
		{
			clsLogFile logFile = new clsLogFile();
			DataRow drData = clsCommon.GetDataRow("LS_spfrmMAILTEMPLATE @Activity='GetDataByID', @TemplateID='" + sMailTemplateID + "'");

			
			string strFromMail=GetEmailAddress(strEmpIDFromMail);
			string strToEmail=GetEmailAddress(strEmpIDToMail);
			string fileLog="";

			try
			{
				fileLog=ConfigurationSettings.AppSettings["pstrLogFile"];
				//fileLog="E:\\EmaillogFile.log";
				logFile.OutLogger(fileLog,"From: " + strFromMail + " - To:" + strToEmail,strSubject);
				
				System.Web.Mail.MailMessage Message  = new System.Web.Mail.MailMessage();
				MailAttachment mailFile;				

				
				Message.From = strFromMail;
				//Staff case
				if (drData["Staff"].ToString()=="0")
					Message.To = strFromMail;				
				if (drData["Staff"].ToString()=="1")
					Message.Cc = strFromMail;	
				if (drData["Staff"].ToString()=="2")
					Message.Bcc = strFromMail;	

				//Supervisor case
				string sSupervisor=clsCommon.LoadSupervisorEmail(strEmpIDFromMail);					
				if (drData["Supervisor"].ToString()=="0")
					Message.To = sSupervisor;				
				if (drData["Supervisor"].ToString()=="1")
					Message.Cc = sSupervisor;	
				if (drData["Supervisor"].ToString()=="2")
					Message.Bcc = sSupervisor;	

				//LM case
				string sLineManager=clsCommon.LoadManagerEmail(strEmpIDFromMail);					
				if (drData["LineManager"].ToString()=="0")
					Message.To = sLineManager;				
				if (drData["LineManager"].ToString()=="1")
					Message.Cc = sLineManager;	
				if (drData["LineManager"].ToString()=="2")
					Message.Bcc = sLineManager;	
				
				//LM case
				string sHR=clsCommon.LoadHREmail(strEmpIDFromMail);					
				if (drData["HR"].ToString()=="0")
					Message.To = sHR;				
				if (drData["HR"].ToString()=="1")
					Message.Cc = sHR;	
				if (drData["HR"].ToString()=="2")
					Message.Bcc = sHR;


				Message.To = strToEmail;
				
				Message.Cc="Cangtt@fpt.com.vn";
				Message.Subject = strSubject;
				Message.BodyEncoding = System.Text.Encoding.UTF8;
				Message.BodyFormat = MailFormat.Html;
				Message.Body = strBody;

				if (attachFiles!=null)
				{
					for (int i=0;i<attachFiles.Count;i++)
					{
						mailFile = new MailAttachment((string)attachFiles[i]);
						Message.Attachments.Add(mailFile);
					}
				}
				/*
				Message.Fields["http://schemas.microsoft.com/cdo/configuration/smtpserver"]= ConfigurationSettings.AppSettings["pstrMailServer"].Trim();
				Message.Fields["http://schemas.microsoft.com/cdo/configuration/smtpserverport"] = 25;
				Message.Fields["http://schemas.microsoft.com/cdo/configuration/sendusing"]  = 2;
				Message.Fields["http://schemas.microsoft.com/cdo/configuration/smtpauthenticate"] = 1;
				Message.Fields["http://schemas.microsoft.com/cdo/configuration/sendusername"] = ConfigurationSettings.AppSettings["pstrSendUser"].Trim();
			
				Message.Fields["http://schemas.microsoft.com/cdo/configuration/sendpassword"] = ConfigurationSettings.AppSettings["pstrSendPassword"].Trim();
				*/

				SmtpMail.SmtpServer = ConfigurationSettings.AppSettings["pstrMailServer"].Trim();;
				SmtpMail.Send(Message);
				return true;
			}
			catch(System.Web.HttpException ehttp)
			{
				logFile.OutLogger(ConfigurationSettings.AppSettings["pstrLogFile"].ToString(),"Error: From: " + strFromMail + " - To:" + strToEmail + " - Nội dung:" + ehttp.Message.ToString(),strSubject);
				return false;
			}
		}
	}
}
