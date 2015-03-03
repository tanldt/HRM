using System;
using System.Data;
using System.Web.UI.WebControls;
using iHRPCore.Com;
namespace iHRPCore.HRComponent
{
	/// <summary>
	/// Summary description for clsHRTempCuong.
	/// </summary>
	public class clsHRContract1
	{
		public static DataTable GetDataByEmpID(Object strEmpID)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = clsCommon.GetDataTableHasID("HR_spfrmContract @Activity='GetDataByEmpID',@EmpID = N'" + strEmpID + "'");
				return dt;
			}
			catch(Exception ex)
			{
				string str = ex.Message;
				return null;
			}
			finally
			{
				dt.Dispose();
			}

		}

		public static DataRow GetDataByID(Object strID)
		{
			DataRow iRow = clsCommon.GetDataRow("HR_spfrmContract @Activity='GetDataByID',@ContractID = " + strID);
			if(iRow !=null)
				return iRow;
			else
				return null;
		}
		public static bool IsDuplicate(string strEmpID,string strContractNo)
		{
			string pCommandText="SELECT ContractNo FROM HR_tblContract WHERE EmpID='"+strEmpID+"' AND ContractNo='"+strContractNo+"' ";
			DataTable dt=clsCommon.GetDataTable(pCommandText);
			if(dt.Rows.Count>0) return true;
			else return false;
		}		

		public static string GetLastSalary(string strEmpID)
		{
			string pCommandText="SELECT dbo.fnConvertNumber(BasicSalary) as BasicSalary FROM HR_tblWorkingRecord WHERE EmpID='"+strEmpID+"' ";
			pCommandText+="ORDER BY ToDate DESC";
			DataRow dr=clsCommon.GetDataRow(pCommandText);
			if(dr.IsNull("BasicSalary")==true) return "";
			else return dr["BasicSalary"].ToString();
		}

		public static string GetCurrencyName()
		{
			string pCommandText="SELECT LSCurrencytypeCode,Name FROM LS_tblCurrencytype WHERE Used=1";
			DataRow dr=clsCommon.GetDataRow(pCommandText);
			if(dr.IsNull("LSCurrencytypeCode")==true) return "";
			else return dr["Name"].ToString();
		}

		public static string GetServiceYear(string strEmpID,string strLanguage)
		{
			string pCommandText="SELECT FromDate FROM HR_tblWorkingRecord WHERE EmpID='"+strEmpID+"' ";
			pCommandText+=" ORDER BY FromDate ASC";
			DataRow dr=clsCommon.GetDataRow(pCommandText);
			if(dr==null) return "N/A";
			if(dr.IsNull("FromDate")==true) return "N/A";
			else
			{
				DateTime dFromDate=DateTime.Parse(dr["FromDate"].ToString());
				TimeSpan tsServiceYear=DateTime.Now.Subtract(dFromDate);
				if (strLanguage=="EN")
					return ((int)(tsServiceYear.Days/365)).ToString()+" years "+((int)((tsServiceYear.Days%365)/30)).ToString()+" months.";
				else
					return ((int)(tsServiceYear.Days/365)).ToString()+" năm "+((int)((tsServiceYear.Days%365)/30)).ToString()+" tháng.";
			}
		}
	}

	
}
