using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web;
using System.Web.Caching;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;
//using System.Data.SqlClient;
//using System.Configuration;

namespace iHRPCore
{
	/// <summary>
	/// Summary description for clsPayrollFormula.
	/// </summary>
	public class clsPayrollFormula
	{
		public clsPayrollFormula()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public static void Save_SetFormula(string sName, string sNote, DataGrid dtgList)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();			
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			try
			{
				
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "PR_spfrmSetFormula";
				#region Save Master
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "SaveMaster";
				cmd.Parameters.Add("@NameFormula",SqlDbType.NVarChar,200).Value = sName;
				cmd.Parameters.Add("@Note",SqlDbType.NVarChar,500).Value = sNote;
				
				cmd.Parameters.Add("@ID", SqlDbType.Int).Direction = ParameterDirection.InputOutput;

				cmd.ExecuteNonQuery();
				int iID = clsDB.SafeDataInteger(cmd.Parameters["@ID"].Value);
				#endregion
				#region save detail
				for(int i=0;i<dtgList.Items.Count;i++)
				{
					string LSSalaryItemDataID = dtgList.Items[i].Cells[0].Text.Trim();
					string LSSalaryItemDataCode = dtgList.Items[i].Cells[2].Text.Trim();
					DropDownList cboTaxType = (DropDownList)dtgList.Items[i].FindControl("cboTaxType");
					string iLSSalaryItemTaxID = cboTaxType.SelectedValue;
					string ssName = ((TextBox)dtgList.Items[i].FindControl("txtName")).Text;
					string sSeq = ((TextBox)dtgList.Items[i].FindControl("txtSeq")).Text;
					string sFormula = ((TextBox)dtgList.Items[i].FindControl("txtFormula")).Text;
					string ssNote = ((TextBox)dtgList.Items[i].FindControl("txtsNote")).Text;
					if (sFormula == "")
						sFormula = "0";
					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "SaveFormulaDetail";
					
					cmd.Parameters.Add("@SetFormulaID",SqlDbType.Int).Value = iID;

					if (iLSSalaryItemTaxID == "")
						cmd.Parameters.Add("@LSSalaryItemTaxID",SqlDbType.Int).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@LSSalaryItemTaxID",SqlDbType.Int).Value = Convert.ToInt16( iLSSalaryItemTaxID);

					cmd.Parameters.Add("@LSSalaryItemDataID",SqlDbType.NVarChar,15).Value = LSSalaryItemDataID;
					cmd.Parameters.Add("@LSSalaryItemDataCode",SqlDbType.NVarChar,50).Value = LSSalaryItemDataCode;
					cmd.Parameters.Add("@Name",SqlDbType.NVarChar,1000).Value = ssName;
					cmd.Parameters.Add("@Seq",SqlDbType.Int).Value = sSeq;
					cmd.Parameters.Add("@Formula",SqlDbType.NText).Value = sFormula;
					cmd.Parameters.Add("@Note",SqlDbType.NVarChar,500).Value = ssNote;				
				
					cmd.ExecuteNonQuery();
				}
				#endregion
				#region Update LSSalaryItemDataCode
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "UpdateDataCode";
				
				cmd.ExecuteNonQuery();
				#endregion
				sqlTran.Commit();
			}
			catch//(Exception ex)
			{
				sqlTran.Rollback();
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();
			}
			
		}
		public static void Edit_SetFormula(int iID,string sName, string sNote, DataGrid dtgList)
		{
			SqlTransaction sqlTran;
			SqlConnection cnn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);			
			cnn.Open();			
			sqlTran = cnn.BeginTransaction();
			SqlCommand cmd = new SqlCommand();
			cmd.Transaction = sqlTran;
			cmd.Connection = cnn;
			try
			{
				
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "PR_spfrmSetFormula";
				#region Save Master
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "EditMaster";
				cmd.Parameters.Add("@SetFormulaID",SqlDbType.Int).Value = iID;
				cmd.Parameters.Add("@NameFormula",SqlDbType.NVarChar,200).Value = sName;				
				cmd.Parameters.Add("@Note",SqlDbType.NVarChar,500).Value = sNote;				
				
				cmd.Parameters.Add("@ID", SqlDbType.Int).Direction = ParameterDirection.InputOutput;

				cmd.ExecuteNonQuery();

				#endregion
				#region save detail
				for(int i=0;i<dtgList.Items.Count;i++)
				{
					string LSSalaryItemDataID = dtgList.Items[i].Cells[0].Text.Trim();
					string LSSalaryItemDataCode = dtgList.Items[i].Cells[2].Text.Trim();
					string ssName = ((TextBox)dtgList.Items[i].FindControl("txtName")).Text;
					string sSeq = ((TextBox)dtgList.Items[i].FindControl("txtSeq")).Text;
					string sFormula = ((TextBox)dtgList.Items[i].FindControl("txtFormula")).Text;
					string ssNote = ((TextBox)dtgList.Items[i].FindControl("txtsNote")).Text;
					DropDownList cboTaxType = (DropDownList)dtgList.Items[i].FindControl("cboTaxType");
					string iLSSalaryItemTaxID = cboTaxType.SelectedValue;
					if (sFormula == "")
						sFormula = "0";
					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "SaveFormulaDetail";
					
					cmd.Parameters.Add("@SetFormulaID",SqlDbType.Int).Value = iID;
					if (iLSSalaryItemTaxID == "")
						cmd.Parameters.Add("@LSSalaryItemTaxID",SqlDbType.Int).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@LSSalaryItemTaxID",SqlDbType.Int).Value = Convert.ToInt16( iLSSalaryItemTaxID);

					cmd.Parameters.Add("@LSSalaryItemDataID",SqlDbType.NVarChar,15).Value = LSSalaryItemDataID;
					cmd.Parameters.Add("@LSSalaryItemDataCode",SqlDbType.NVarChar,50).Value = LSSalaryItemDataCode;
					cmd.Parameters.Add("@Name",SqlDbType.NVarChar,1000).Value = ssName;
					cmd.Parameters.Add("@Seq",SqlDbType.Int).Value = sSeq;
					cmd.Parameters.Add("@Formula",SqlDbType.NText).Value = sFormula;
					cmd.Parameters.Add("@Note",SqlDbType.NVarChar,500).Value = ssNote;				
				
					cmd.ExecuteNonQuery();
				}
				#endregion
				#region Update LSSalaryItemDataCode
				cmd.Parameters.Clear();
				cmd.Parameters.Add("@Activity",SqlDbType.NVarChar,50).Value = "UpdateDataCode";
				
				cmd.ExecuteNonQuery();
				#endregion
				sqlTran.Commit();
			}
			catch//(Exception ex)
			{
				sqlTran.Rollback();
			}
			finally
			{
				cmd.Dispose();
				sqlTran.Dispose();
				if(cnn.State == ConnectionState.Open) cnn.Close();
			}
			
		}
		public static DataRow GetDataMasterByID(string ID)
		{
			DataRow dr = clsDB.GetDataRow("PR_spfrmSetFormula @Activity = 'GetDataByID',@SetFormulaID = " +ID );
			return dr;
		}
		public static DataTable GetDataTaxType()
		{
			return clsDB.GetDataTable("select * from LS_tblSalaryItemTax");;
		}
		public static DataTable GetEmpList(string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strPosition, string strJobCode, string strLocation, string strCompany, string strStatus,string sEmpTypeID, string StatusSearch)
		{
			string strSql = "PR_clsSetFormulaEmp @Activity='GetEmpList',@EmpID=N'" + strEmpID + "', @EmpName=N'" + strEmpName
				+ "', @LSLevel1ID=N'" + strLevel1 + "', @LSLevel2ID=N'" + strLevel2 + "',@LSLevel3ID=N'" + strLevel3
				+ "',@LSPositionID='" + strPosition + "',@LSJobCodeID='" + strJobCode 
				+ "',@LSLocationID=N'" + strLocation + "',@LSCompanyID=N'" + strCompany + "',@UserGroupID=N'" + Mession.GlbUser + "',@LSEmpTypeID='" + sEmpTypeID + "',@StatusSearch = " + StatusSearch;
			if (strStatus != "")
				strSql = strSql + ", @Status='" + strStatus + "'";

			DataTable dtb = clsDB.GetDataTable(strSql);
			return dtb;
		}
		public static string sImpact(DataGrid dtgList)
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
				cmd.CommandText = "PR_clsSetFormulaEmp";
   
				CheckBox obj = new CheckBox();
				for (int i = 0;i<= dtgList.Items.Count - 1;i++)
				{
					obj = (CheckBox) dtgList.Items[i].FindControl("chkSelect");					
					if (obj.Checked)
					{
						cmd.Parameters.Clear();
						cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 30).Value = "Save";					

						if (dtgList.Items[i].Cells[0].Text.Trim()=="")
							cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar).Value=DBNull.Value;
						else
							cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar,12).Value= dtgList.Items[i].Cells[0].Text.Trim(); 
		
						if (((DropDownList)dtgList.Items[i].FindControl("cboFormula")).SelectedValue=="")
							cmd.Parameters.Add("@SetFormulaID", SqlDbType.Int).Value=DBNull.Value;
						else
							cmd.Parameters.Add("@SetFormulaID", SqlDbType.Int).Value= ((DropDownList)dtgList.Items[i].FindControl("cboFormula")).SelectedValue;
		
						cmd.Parameters.Add("@ReturnMess", SqlDbType.NVarChar,1000).Direction = ParameterDirection.InputOutput;
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
}
