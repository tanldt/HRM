using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using iHRPCore.Com;

namespace WebServices
{
	/// <summary>
	/// Summary description for Test.
	/// </summary>
	public class Test : System.Web.Services.WebService
	{
		public Test()
		{
			//CODEGEN: This call is required by the ASP.NET Web Services Designer
			//InitializeComponent();
		}

		// WEB SERVICE EXAMPLE
		// The HelloWorld() example service returns the string Hello World
		// To build, uncomment the following lines then save and build the project
		// To test this web service, press F5

		[WebMethod]
		public DataTable dtDataTest()
		{
			DataTable dt = new DataTable();
			dt = clsCommon.GetDataTable("select * from LS_tblProvince");
			return dt;
		}
	}
}
