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

namespace iHRPCore.SerComponent
{
	/// <summary>
	/// Summary description for clsServices.
	/// </summary>
	public class clsServices
	{
		public clsServices()
		{
			//
			// TODO: Add constructor logic here
			//
		}

	}
	public class clsCustomer_Types
	{
		private string code = "";
		public string Code
		{
			get
			{
				return code;
			}
			set
			{
				code = value;
			}
		}
		public clsCustomer_Types()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public static DataTable getDataCustomerTypes(string sCode, string sName, string sNameVN, string sNote)
		{
			DataTable dt = new DataTable();
			string sSQL = "SER_spfrmCustomerType @Activity=N'getList',@Code='"+sCode+"',@Name=N'"+sName+"',@NameVN=N'"+sNameVN+"',@Note=N'"+sNote+"'";
			dt = clsCommon.GetDataTable(sSQL);
			return dt;
		}
		
		public DataTable getDataCustomerTypes___()
		{
			DataTable dt = new DataTable();
			string sSQL = "SER_spfrmCustomerType @Activity=N'getList',@Code='"+code + "'";//+"',@Name=N'"+sName+"',@NameVN=N'"+sNameVN+"',@Note=N'"+sNote+"'";
			dt = clsCommon.GetDataTable(sSQL);
			return dt;
		}

	}
}
