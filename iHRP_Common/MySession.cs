using System;
using System.Collections;
using System.Web;
using System.Data;

namespace iHRPCore
{
	/// <summary>
	/// All references to session should go into this class
	/// </summary>
	public class Mession
    {
        #region GlbUser
        static public string GlbUser
        {
            get
            {
                if (HttpContext.Current.Session["AccountLogin"] != null)
                    return (string)HttpContext.Current.Session["AccountLogin"];
                else
                    return "";
            }
            set
            {
                HttpContext.Current.Session["AccountLogin"] = value;
            }
        }
        #endregion GlbUser
		#region GlbEmpIDLogin
		static public string GlbEmpIDLogin
		{
			get
			{
				if (HttpContext.Current.Session["GlbEmpIDLogin"] != null)
					return (string)HttpContext.Current.Session["GlbEmpIDLogin"];
				else
					return "";
			}
			set
			{
				HttpContext.Current.Session["GlbEmpIDLogin"] = value;
			}
		}
		#endregion GlbEmpIDLogin
        #region LangID
        static public string GlbLangID
        {
            get
            {
				if (HttpContext.Current.Session["LangID"] != null)
					return (string)HttpContext.Current.Session["LangID"];
				else
					return "VN";
                
            }
            set
            {
                HttpContext.Current.Session["LangID"] = value;
            }
        }
        #endregion GlbLangID
		#region Cus Group ID
		static public string GlbCusGroupID
		{
			get
			{
				if (HttpContext.Current.Session["GlbCusGroupID"] != null)
					return (string)HttpContext.Current.Session["GlbCusGroupID"];
				else
					return "";
                
			}
			set
			{
				HttpContext.Current.Session["GlbCusGroupID"] = value;
			}
		}
		#endregion GlbCusGroupID
		#region Search Data
		static public DataTable GlbSearchData
		{
			get
			{
				if (HttpContext.Current.Session["GlbSearchData"] != null)
					return (DataTable)HttpContext.Current.Session["GlbSearchData"];
				else
					return null;
                
			}
			set
			{
				HttpContext.Current.Session["GlbSearchData"] = value;
			}
		}
		#endregion Search Data
		#region GlbMMYYYY
		static public string GlbMMYYYY
		{
			get
			{
				if (HttpContext.Current.Session["GlbMMYYYY"] != null)
					return (string)HttpContext.Current.Session["GlbMMYYYY"];
				else
					return DateTime.Today.ToString("dd/MM/yyyy").Substring(3,7);;
			}
			set
			{
				HttpContext.Current.Session["GlbMMYYYY"] = value;
			}
		}
		#endregion GlbUser
    }
}
