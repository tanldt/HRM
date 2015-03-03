using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web;
using System.Web.Caching;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;
using iHRPCore.Include;
//using System.Data.SqlClient;
//using System.Configuration;

namespace iHRPCore
{
	/// <summary>
	/// Summary description for clsEmpHeaderSearch.
	/// </summary>
	public class clsEmpHeaderSearch
	{
		public EmpHeaderSearch empHeaderSearch
		{
			set { this.EmpHeaderSearch1 = value; }
			get { return this.EmpHeaderSearch1; }
		}
		private EmpHeaderSearch EmpHeaderSearch1;
		public string LSCompanyID
		{
			set { this.lsCompanyID = value; }
			get { return this.lsCompanyID; }
		}
		private string lsCompanyID = "";
		public string LSLevel1ID
		{
			set { this.lsLevel1ID = value; }
			get { return this.lsLevel1ID; }
		}
		private string lsLevel1ID = "";
		public string LSLevel2ID
		{
			set { this.lsLevel2ID = value; }
			get { return this.lsLevel2ID; }
		}
		private string lsLevel2ID = "";
		public string LSLevel3ID
		{
			set { this.lsLevel3ID = value; }
			get { return this.lsLevel3ID; }
		}
		private string lsLevel3ID = "";
		public string EmpID
		{
			set { this.empID = value; }
			get { return this.empID; }
		}
		private string empID = "";
		public string EmpName
		{
			set { this.empName = value; }
			get { return this.empName; }
		}
		private string empName = "";
		public string LSLocationID
		{
			set { this.lsLocationID = value; }
			get { return this.lsLocationID; }
		}
		private string lsLocationID = "";
		public string LSJobcodeID
		{
			set { this.lsJobcodeID = value; }
			get { return this.lsJobcodeID; }
		}
		private string lsJobcodeID = "";
		public string LSEmpTypeID
		{
			set { this.lsEmpTypeID = value; }
			get { return this.lsEmpTypeID; }
		}
		private string lsEmpTypeID = "";
		public string Status
		{
			set { this.status = value; }
			get { return this.status; }
		}
		private string status = "";
		public clsEmpHeaderSearch()
		{
			//
			// TODO: Add constructor logic here
			//

		}
		public void EmpSearch()
		{
			lsLocationID = ((DropDownList)EmpHeaderSearch1.FindControl("cboLocation")).SelectedValue;
			lsJobcodeID = ((DropDownList)EmpHeaderSearch1.FindControl("cboJobcode")).SelectedValue;
			lsEmpTypeID = ((DropDownList)EmpHeaderSearch1.FindControl("cboLSEmpTypeID")).SelectedValue;

			lsCompanyID = ((DropDownList)EmpHeaderSearch1.FindControl("cboCompany")).SelectedValue;
			lsLevel1ID = ((DropDownList)EmpHeaderSearch1.FindControl("cboLevel1")).SelectedValue;
			lsLevel2ID = ((DropDownList)EmpHeaderSearch1.FindControl("cboLevel2")).SelectedValue;
			lsLevel3ID = ((DropDownList)EmpHeaderSearch1.FindControl("cboLevel3")).SelectedValue;
			empID = ((TextBox)EmpHeaderSearch1.FindControl("txtEmpID")).Text;
			empName = ((TextBox)EmpHeaderSearch1.FindControl("txtEmpName")).Text;
			status = ((RadioButtonList)EmpHeaderSearch1.FindControl("optStatus")).SelectedValue;
		}
	}
}
