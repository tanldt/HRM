using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using System.Web.UI.WebControls;
using iHRPCore.Com;

namespace iHRPCore.STRComponent
{
	#region STAFF RELATION AWARDS CLASS
	public class clsSTRAwards
	{
		const string strInProgress="In progress";
		const string strPending="Pending";
		const string strDone="Done";

		public static DataTable LoadAwards(string strEmpID)
		{
			string strSQL="STR_spfrmAward @Activity='LoadAwards',@EmpID='"+strEmpID+"'";
			DataTable dt=clsCommon.GetDataTableHasID(strSQL);
			DataColumn cl=new DataColumn("StatusName");			
			dt.Columns.Add(cl);
			for(int i=0;i<dt.Rows.Count;i++) 
			{				
				if(dt.Rows[i]["Status"].ToString()=="0") dt.Rows[i]["StatusName"]=strInProgress;
				if(dt.Rows[i]["Status"].ToString()=="1") dt.Rows[i]["StatusName"]=strDone;
				if(dt.Rows[i]["Status"].ToString()=="2") dt.Rows[i]["StatusName"]=strPending;
			}
			return dt;
		}

		public static string GetNumAwards(string strEmpID)
		{
			string strSQL="STR_spfrmAward @Activity='GetNumAwards',@EmpID='"+strEmpID+"'";
			DataRow dr=clsCommon.GetDataRow(strSQL);
			if(dr.IsNull("NumAward")==true) return "0";
			else return dr["NumAward"].ToString();
		}
	}
	#endregion

	#region STAFF RELATION COMPLAIN CLASS
	public class clsSTRComplain
	{
		const string strProgress="Progressing";
		const string strDone="Done";
		const string strClosed="Pending";
		const string strCompany="Company";
		const string strCompaiant="Compainant";

		public static DataTable LoadComplains(string strEmpID)
		{
			string strSQL="STR_spfrmComplain @Activity='LoadComplains',@EmpID='"+strEmpID+"'";
			DataTable dt=clsCommon.GetDataTableHasID(strSQL);

			DataColumn cl=new DataColumn("StatusName");			
			dt.Columns.Add(cl);
			cl=new DataColumn("FavourName");			
			dt.Columns.Add(cl);
			for(int i=0;i<dt.Rows.Count;i++) 
			{				
				if(dt.Rows[i]["Status"].ToString()=="0") dt.Rows[i]["StatusName"]=strProgress;
				else if(dt.Rows[i]["Status"].ToString()=="1") dt.Rows[i]["StatusName"]=strDone;
				else dt.Rows[i]["StatusName"]=strClosed;

				if(dt.Rows[i]["Favour"].ToString()=="False") dt.Rows[i]["FavourName"]=strCompany;
				else dt.Rows[i]["FavourName"]=strCompaiant;
			}
			return dt;
		}
	}
	#endregion

	#region STAFF RELATION DISCIPLINE CLASS
	public class clsSTRDiscipline
	{
		const string strProgress="Progressing";
		const string strDone="Done";
		const string strClosed="Pending";
		
		public static DataTable LoadDisciplines(string strEmpID)
		{
			string strSQL="STR_spfrmDiscipline @Activity='LoadDisciplines',@EmpID='"+strEmpID+"'";			
			DataTable dt=clsCommon.GetDataTableHasID(strSQL);
			
			DataColumn cl=new DataColumn("StatusName");			
			dt.Columns.Add(cl);
			
			for(int i=0;i<dt.Rows.Count;i++) 
			{				
				if(dt.Rows[i]["Status"].ToString()=="0") dt.Rows[i]["StatusName"]=strProgress;
				else if(dt.Rows[i]["Status"].ToString()=="1") dt.Rows[i]["StatusName"]=strDone;
				else dt.Rows[i]["StatusName"]=strClosed;
			}
			return dt;
		}

		public static string GetNumOfViolations(string strEmpID)
		{
			string strSQL="STR_spfrmDiscipline @Activity='GetNum',@EmpID='"+strEmpID+"'";
			DataRow dr=clsCommon.GetDataRow(strSQL);
			if(dr.IsNull("Num")==true) return "0";
			else return dr["Num"].ToString();
		}
	}
	#endregion

	#region STAFF RELATION AWARDS/DISCIPLINES/COMPLAINS LIST CLASS
	public class clsSTRList
	{
		public static DataTable Search(string strEmpID,string strName,string strDivision,string strDepartment,
			string strLocation,string strSection,string strPosition,string strJobCode,
			string strStatus,string strType,string strFromDate,string strToDate)
		{			
			string strSQL="STR_spfrmList @Activity='Search', ";
			strSQL+="@EmpID='"+strEmpID+"', ";			
			strSQL+="@EmpName='"+strName+"', ";
			strSQL+="@Level1Code='"+strDivision+"', ";
			strSQL+="@Level2Code='"+strDepartment+"', ";
			strSQL+="@Level3Code='"+strSection+"', ";
			strSQL+="@LocationCode='"+strLocation+"', ";
			strSQL+="@PositionCode='"+strPosition+"', ";
			strSQL+="@JobCode='"+strJobCode+"', ";
			strSQL+="@StatusID='"+strStatus+"', ";
			strSQL+="@Type='"+strType+"', ";
			strSQL+="@FromDate='"+strFromDate+"', ";
			strSQL+="@ToDate='"+strToDate+"'";

			return clsCommon.GetDataTableHasID(strSQL);
		}
	}

	#endregion
}
