using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using System.Web.UI.WebControls;
using iHRPCore.Com;
using System.Web.UI;
using System.Configuration;
using iHRPCore.MdlHR;
using System.Web.UI.HtmlControls;

namespace iHRPCore.News
{
	/// <summary>
	/// Summary description for clsNews.
	/// </summary>
	public class clsNews
	{
		public static DataTable GetDataAll()
		{
			DataTable dtData= new DataTable();
			try
			{
				dtData=clsCommon.GetDataTable("NEWS_spfrmNEWS @Activity='GetDataAll'");
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
		public static DataRow getDataByID(string sID)
		{
			DataRow drData= clsCommon.GetDataRow("NEWS_spfrmNews @Activity='getDataByID',@NewsID='" + sID + "'");
			return drData;
		}
	}
}
