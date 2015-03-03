using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Data;
using System.Xml;
using System.Web;
using System.Threading;
using System.Globalization;
using System.Configuration;
using iHRPCore.TMSComponent;

namespace iHRPCore.Component
{
	/// <summary>
	/// Summary description for UserControlCommon.
	/// </summary>
	public class UserControlCommon : System.Web.UI.UserControl
	{
		
		#region Variables

//		private string		m_strRefreshURL		= null;
//		private int			m_nRefreshTime		= 2;
//		private bool		m_bNoDataBase		= false;
//		private bool		m_bShowToolBar		= true;
		private bool		m_checkSuspended	= true;
		//private DataRow		m_pageinfo			= null;
		public string strLanguage = "VN";
		
		#endregion
		#region Constructor and events
		/// <summary>
		/// Constructor
		/// </summary>
		public UserControlCommon()
		{
			//TransPage = transPage;
			this.Load += new System.EventHandler(this.UserControlCommon_Load);
			this.Unload += new EventHandler(UserControlCommon_Unload);
		}

		private void UserControlCommon_Unload(object sender,EventArgs e)
		{
		}

		static public object ValidInt(object o)
		{
			try
			{
				if(o==null)
					return null;

				return int.Parse(o.ToString());
			}
			catch(Exception) 
			{
				return null;
			}
		}

		/// <summary>
		/// Called when page is loaded
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UserControlCommon_Load(object sender, System.EventArgs e) 
		{
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
		}

		#endregion


		public bool CheckSuspended
		{
			set
			{
				m_checkSuspended = value;
			}
		}

		static public object IsNull(string value)
		{
			if(value==null || value.ToLower()==string.Empty)
				return DBNull.Value;
			else
				return value;
		}

		#region Localizing
		private Localizer	m_localizer = null;
		private	Localizer	m_defaultLocale	= null;

		//private	string		m_transPage = null;
		
		/// <summary>
		/// What section of the xml is used to translate this page
		/// </summary>
//		public string TransPage
//		{
//			get
//			{
//				if(m_transPage!=null)
//					return m_transPage;
//
//				throw new ApplicationException(string.Format("Missing TransPage property for {0}",GetType()));
//			}
//			set
//			{
//				m_transPage = value;
//			}
//		}
//
//		public string GetText(string text) 
//		{
//			return GetText(TransPage,text);
//		}

		private string LoadTranslation() 
		{
			if(m_localizer!=null) 
				return m_localizer.LanguageCode;
			
			string filename = null;

			//filename = (string)m_pageinfo["LanguageFile"];

			if(filename==null)
				filename = "vietnam.xml";

#if !DEBUG
			if(m_localizer==null && HttpContext.Current.Cache["Localizer." + filename]!=null)
				m_localizer = (Localizer)HttpContext.Current.Cache["Localizer." + filename];
#endif
			if(m_localizer==null) 
			{

				//m_localizer = new Localizer(HttpContext.Current.Server.MapPath(String.Format("{0}languages/{1}","/iHRPCore/",filename)));
				m_localizer = new Localizer(Server.MapPath(".") + ConfigurationSettings.AppSettings["pStrLanguage"].ToString() + filename);
#if !DEBUG
				HttpContext.Current.Cache["Localizer." + filename] = m_localizer;
#endif
			}
			// If not using default language load that too
			if(filename.ToLower()!="vietnam.xml") 
			{
#if !DEBUG
				if(m_defaultLocale==null && HttpContext.Current.Cache["DefaultLocale"]!=null)
					m_defaultLocale = (Localizer)HttpContext.Current.Cache["DefaultLocale"];
#endif

				if(m_defaultLocale==null) 
				{
					m_defaultLocale = new Localizer(HttpContext.Current.Server.MapPath(String.Format("{0}languages/vietnam.xml","/eYourkey/Admin/")));
#if !DEBUG
					HttpContext.Current.Cache["DefaultLocale"] = m_defaultLocale;
#endif
				}
			}
			return m_localizer.LanguageCode;
		}

		public string GetText(string page,string text) 
		{
			LoadTranslation();
			string str = m_localizer.GetText(page,text);
			// If not default language, try to use that instead
			if(str==null && m_defaultLocale!=null) 
			{
				str = m_defaultLocale.GetText(page,text);
				if(str!=null) str = '[' + str + ']';
			}
			if(str==null) 
			{
#if !DEBUG
				string filename = null;

//				if(m_pageinfo==null || m_pageinfo.IsNull("LanguageFile") || !BoardSettings.AllowUserLanguage)
//					filename = Config.ConfigSection["language"];
//				else
//					filename = (string)m_pageinfo["LanguageFile"];

				if(filename==null)
					filename = "vietnam.xml";

				HttpContext.Current.Cache.Remove("Localizer." + filename);
#endif
				throw new Exception(String.Format("Missing translation for {1}.{0}",text.ToUpper(),page.ToUpper()));
			}
			str = str.Replace("[b]","<b>");
			str = str.Replace("[/b]","</b>");
			str = str.Replace("[","<");
			str = str.Replace("]",">");
			return str;
		}
		#endregion
		
	}
}