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

namespace iHRPCore.HRComponent
{
	/// <summary>
	/// Summary description for clsHRTempLan.
	/// </summary>
	public class clsHREmpList
	{
		/// <summary>
		/// Lay thong tin cua mot function
		/// </summary>
		public static DataTable GetFunctionByID(string strFunctionID)
		{
			DataTable dtb = clsCommon.GetDataTable("sp_clsCommon 'GetByFunctionID', @FunctionID='" + strFunctionID + "'");
			return dtb;
		}

		/// <summary>
		/// Lay thong tin cua mot column: su dung trong man hinh search thong tin nhan vien
		/// </summary>
		public static DataTable GetColumnByID(string strColumnID)
		{
			DataTable dtb = clsCommon.GetDataTable("sp_frmDynamic 'GetColumnByID', @ColumnID='" + strColumnID + "'");
			return dtb;
		}

		/// <summary>
		/// Lay thong tin cua mot column: su dung trong man hinh search thong tin nhan vien
		/// </summary>
		public static DataTable GetLSTable(string strTableName)
		{
			DataTable dtb = clsCommon.GetDataTable("sp_frmDynamic 'GetLSTable', @TableName='" + strTableName + "'");
			return dtb;
		}

		/// <summary>
		/// Load thong tin cho combo danh sach module
		/// </summary>
		public static void LoadComboModule(DropDownList pControl, string strLanguage)
		{
			clsCommon.LoadDropDownListControl(pControl,"sp_frmDynamic 'GetModuleList',@Language='" + strLanguage + "'", "ModuleID","ModuleName",true);
		}

		/// <summary>
		/// Load thong tin cho combo toan tu dieu kien
		/// </summary>
		public static void LoadComboOperator(DropDownList pControl, string strLanguage)
		{
			clsCommon.LoadDropDownListControl(pControl,"sp_frmDynamic 'GetOperator',@Language='" + strLanguage + "'", "OperatorID","Operator",false);
		}


		/// <summary>
		/// Danh sach function theo phan he: Trang du lieu dong, function = bang du lieu
		/// </summary>
		public static DataTable GetFunctionByMdlID(string strMdlID, string strLanguage)
		{
			DataTable dtb = clsCommon.GetDataTable("sp_frmDynamic 'GetFunctionByMdlID',@Language='" 
				+ strLanguage + "',@ModuleID='" + strMdlID + "'");
			return dtb;
		}

		/// <summary>
		/// Danh sach cac cot theo tinh nang (bang)
		/// </summary>
		public static DataTable GetColumnByFunction(string strTableID, string strLanguage)
		{
			DataTable dtb = clsCommon.GetDataTable("sp_frmDynamic 'GetColumnByFunction',@TableID='" + strTableID
				+ "', @Language='" + strLanguage + "'");
			return dtb;
		}

		/// <summary>
		/// Lay chieu dai cua truong EmpID tu SYS_Parameters
		/// </summary>
		public static DataTable GetEmpIDLength()
		{
			DataTable dtb = clsCommon.GetDataTable("HR_clsCommon 'GetEmpIDLength'");
			return dtb;
		}
		
		/// <summary>
		/// Lay thong tin nhan vien
		/// </summary>
		public static DataTable GetEmpList(string strEmpID,System.Web.UI.Page pPage,string sGetAll)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			if (sGetAll=="1")
				sAccountLogin="admin";

			DataTable dtb = clsCommon.GetDataTable("HR_clsCommon 'GetEmpList',@EmpID=N'" + strEmpID + "',@UserGroupID=N'" + sAccountLogin + "'");
			return dtb;
		}

		public static DataTable GetEmpList(string strEmpID)
		{			
			DataTable dtb = clsCommon.GetDataTable("HR_clsCommon 'GetEmpList',@EmpID=N'" + strEmpID + "'");
			return dtb;
		}
		//vuonglm:rat chuoi chi ap dung cho HLHV
		public static DataTable GetEmpListChoice(string strEmpID,string projectID)
		{			
			DataTable dtb = clsCommon.GetDataTable("HR_clsCommon 'GetEmpListChoice',@EmpID=N'" + strEmpID + ",@ProjectID=N'" + projectID +"'");
			return dtb;
		}
		//vuonglm
		public static DataTable GetEmpList(System.Web.UI.Page pPage, int iIndividual)
		{			
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			DataTable dtb = clsCommon.GetDataTable("HR_clsCommon 'GetEmpList',@Individual=1,@UserGroupID=N'" + sAccountLogin + "'");
			return dtb;
		}
		/// <summary>
		/// Lay thong tin nhan vien
		/// </summary>
		public static DataTable GetEmpListByString(string strEmpIDString)
		{
			DataTable dtb = clsCommon.GetDataTable("HR_clsCommon 'GetEmpList_ByEmpIDString', @EmpIDString=N'" + strEmpIDString + "'");
			return dtb;
		}
		/// <summary>
		/// Get thong tin cua user dang nhap
		/// </summary>
		/// <param name="strEmpID"></param>
		/// <param name="pPage"></param>
		/// <param name="sGetAll"></param>
		/// <returns></returns>
		public static DataTable GetUserAccount()
		{
			string sAccountLogin= Mession.GlbUser;

			DataTable dtb = clsCommon.GetDataTable("select * from UMS_tblUserAccount where UserGroupID=N'" + sAccountLogin + "'");
			return dtb;
		}
		/// <summary>
		/// Lay thong tin nhan vien theo dieu kien loc
		/// </summary>
		public static DataTable GetEmpList(string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strPosition, string strJobCode, string strLocation, string strCompany, string strStatus,System.Web.UI.Page pPage,string sGetAll,string sEmpTypeID)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			if (sGetAll=="1")
				sAccountLogin="admin";

			string strSql = "HR_clsCommon @Activity='GetEmpList',@EmpID=N'" + strEmpID + "', @EmpName=N'" + strEmpName
				+ "', @LSLevel1ID=N'" + strLevel1 + "', @LSLevel2ID=N'" + strLevel2 + "',@LSLevel3ID=N'" + strLevel3
				+ "',@LSPositionID='" + strPosition + "',@LSJobCodeID='" + strJobCode 
				+ "',@LSLocationID=N'" + strLocation + "',@LSCompanyID=N'" + strCompany + "',@UserGroupID=N'" + sAccountLogin + "',@LSEmpTypeID='" + sEmpTypeID + "'";
			if (strStatus != "")
				strSql = strSql + ", @Status='" + strStatus + "'";

			DataTable dtb = clsCommon.GetDataTable(strSql);
			return dtb;
		}
		/// <summary>
		/// Lay thong tin nhan vien theo dieu kien loc va theo khoa hoc
		/// </summary>
		/*public static DataTable GetEmpList(string strCourseCode, string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strPosition, string strJobCode, string strLocation, string strCompany, string strStatus,System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";

			string strSql = "HR_clsCommon @Activity='GetEmpList',@EmpID=N'" + strEmpID + "', @EmpName=N'" + strEmpName
				+ "', @LSLevel1ID=N'" + strLevel1 + "', @LSLevel2ID=N'" + strLevel2 + "',@LSLevel3ID=N'" + strLevel3
				+ "',@LSPositionID='" + strPosition + "',@LSJobCodeID='" + strJobCode 
				+ "',@LSLocationID=N'" + strLocation + "',@LSCompanyID=N'" + strCompany + "',@UserGroupID=N'" + sAccountLogin + "'";
			if (strStatus != "")
				strSql = strSql + ", @Status='" + strStatus + "'";

			DataTable dtb = clsCommon.GetDataTable(strSql);
			return dtb;
		}*/
		/// <summary>
		/// Lay thong tin nhan vien theo dieu kien loc
		/// </summary>
		public static DataTable GetEmpList(string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strPosition, string strJobCode, string strLocation, string strCompany, string strStatus, int intTopRow,System.Web.UI.Page pPage,string sGetAll, string sEmpTypeID)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			if (sGetAll=="1")
				sAccountLogin="admin";

			string strSql = "HR_clsCommon @Activity='GetEmpList',@EmpCode=N'" + strEmpID + "', @EmpName=N'" + strEmpName
				+ "', @LSLevel1ID=N'" + strLevel1 + "', @LSLevel2ID=N'" + strLevel2 + "',@LSLevel3ID=N'" + strLevel3
				+ "',@LSPositionID=N'" + strPosition + "',@LSJobCodeID=N'" + strJobCode 
				+ "',@LSLocationID='" + strLocation + "',@LSCompanyID=N'" + strCompany + "',@UserGroupID=N'" + sAccountLogin + "',@LSEmpTypeID='" + sEmpTypeID + "'";
			if (strStatus != "")
				strSql = strSql + ", @Status='" + strStatus + "'";

			if (intTopRow != 0)
				strSql = strSql + ", @TopRow='" + intTopRow.ToString() + "'";

			DataTable dtb = clsCommon.GetDataTable(strSql);
			return dtb;
		}

		public static DataTable GetEmpList(string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strPosition, string strJobCode, string strLocation, string strCompany, string strStatus, string strWhere,System.Web.UI.Page pPage,string sGetAll,string sEmpTypeID)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			if (sGetAll=="1")
				sAccountLogin="admin";
			string strSql = "HR_clsCommon @Activity='GetEmpList',@EmpID=N'" + strEmpID + "', @EmpName=N'" + strEmpName
				+ "', @LSLevel1ID='" + strLevel1 + "', @LSLevel2ID='" + strLevel2 + "',@LSLevel3ID='" + strLevel3
				+ "',@LSPositionID='" + strPosition + "',@LSJobCodeID='" + strJobCode 
				+ "',@LSLocationID='" + strLocation + "',@LSCompanyID='" + strCompany
				+ "',@Where=N'" + strWhere + "',@UserGroupID='" + sAccountLogin + "', @LSEmpTypeID='" + sEmpTypeID + "'";
			if (strStatus != "")
				strSql = strSql + ", @Status='" + strStatus + "'";
			DataTable dtb = clsCommon.GetDataTable(strSql);
			return dtb;
		}
		/// <summary>
		/// Lay thong tin nhan vien theo dieu kien loc
		/// </summary>
		public static DataTable GetEmpListByCourse(string strCourseID, string strEmpID, string strEmpName, string strLevel1, string strLevel2, 
			string strLevel3, string strPosition, string strJobCode, string strLocation, string strCompany, string strStatus, string strWhere,System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			string strSql = "HR_clsCommon @Activity='GetEmpListByCourse', @TrainingCourseID=N'" + strCourseID + "' ,@EmpID=N'" + strEmpID + "', @EmpName=N'" + strEmpName
				+ "', @LSLevel1ID='" + strLevel1 + "', @LSLevel2ID='" + strLevel2 + "',@LSLevel3ID='" + strLevel3
				+ "',@LSPositionID='" + strPosition + "',@LSJobCodeID='" + strJobCode 
				+ "',@LSLocationID='" + strLocation + "',@LSCompanyID='" + strCompany
				+ "',@Where=N'" + strWhere + "',@UserGroupID='" + sAccountLogin + "'";
			if (strStatus != "")
				strSql = strSql + ", @Status='" + strStatus + "'";
			DataTable dtb = clsCommon.GetDataTable(strSql);
			return dtb;
		}

		public static void LoadComboLevel1(DropDownList pControl, string strTextField,string sLanguageID, System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			//clsCommon.LoadDropDownListControl(pControl,"sp_GetDataCombo @TableName='LS_tblLevel1',@Fields='LSCompanyID, LSLevel1ID as [ID]," + strTextField + " as Name',@Where=N' and LSCompanyID=N''" + strCompanyID.Trim() + "'''","ID","Name",true);
			clsCommon.LoadDropDownListControl(pControl,"sp_GetDataComboLevel @TableName = 'LS_tblLevel1', @Language='" + sLanguageID + "',@UserGroupID='" + sAccountLogin + "'","LSLevel1ID","Name",true);
		}

		public static void LoadComboLevel2(DropDownList pControl, string strTextField,string sLanguageID, System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			//clsCommon.LoadDropDownListControl(pControl,"sp_GetDataCombo @TableName='LS_tblLevel1',@Fields='LSCompanyID, LSLevel1ID as [ID]," + strTextField + " as Name',@Where=N' and LSCompanyID=N''" + strCompanyID.Trim() + "'''","ID","Name",true);
			clsCommon.LoadDropDownListControl(pControl,"sp_GetDataComboLevel @TableName = 'LS_tblLevel2', @Language='" + sLanguageID + "',@UserGroupID='" + sAccountLogin + "'","LSLevel2ID","Name",true);
		}

		public static void LoadComboLevel2(DropDownList pControl, string strTextField)
		{			
			clsCommon.LoadDropDownListControl(pControl,"sp_GetDataCombo @TableName='LS_tblLevel2',@Fields='LSLevel2ID as [ID]," + strTextField + " as Name'","ID","Name",true);
		}

		public static void LoadComboLevel3(DropDownList pControl, string strTextField,string sLanguageID, System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			//clsCommon.LoadDropDownListControl(pControl,"sp_GetDataCombo @TableName='LS_tblLevel1',@Fields='LSCompanyID, LSLevel1ID as [ID]," + strTextField + " as Name',@Where=N' and LSCompanyID=N''" + strCompanyID.Trim() + "'''","ID","Name",true);
			clsCommon.LoadDropDownListControl(pControl,"sp_GetDataComboLevel @TableName = 'LS_tblLevel3', @Language='" + sLanguageID + "',@UserGroupID='" + sAccountLogin + "'","LSLevel3ID","Name",true);
		}

		public static void LoadComboLevel3(DropDownList pControl, string strTextField)
		{
			clsCommon.LoadDropDownListControl(pControl,"sp_GetDataCombo @TableName='LS_tblLevel3',@Fields='LSLevel3ID as [ID]," + strTextField + " as Name'","ID","Name",true);
		}

		public static void LoadComboCompany(DropDownList pControl, string strTextField, string strLangID,System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			if (sAccountLogin == "admin")
                clsCommon.LoadDropDownListControl(pControl,"sp_GetDataComboLevel @TableName = 'LS_tblCompany', @Language='" + strLangID + "',@UserGroupID='" + sAccountLogin + "'","LSCompanyID","Name",true);
			else
				clsCommon.LoadDropDownListControl(pControl,"sp_GetDataComboLevel @TableName = 'LS_tblCompany', @Language='" + strLangID + "',@UserGroupID='" + sAccountLogin + "'","LSCompanyID","Name",false);
		}

		public static void LoadComboLocation(DropDownList pControl, string strTextField)
		{
			clsCommon.LoadDropDownListControl(pControl,"sp_GetDataCombo @TableName='LS_tblLocation',@Fields='LSLocationID as [ID],DisplayName as Name'","ID","Name",true);
		
		}

		public static void LoadComboPosition(DropDownList pControl, string strTextField)
		{
			clsCommon.LoadDropDownListControl(pControl,"sp_GetDataCombo @TableName='LS_tblPosition',@Fields='LSPositionID as [ID]," + strTextField + " as Name'","ID","Name",true);
		}
		public static void LoadComboEmpType(DropDownList pControl, string strTextField)
		{
			clsCommon.LoadDropDownListControl(pControl,"sp_GetDataCombo @TableName='LS_tblEmpType',@Fields='LSEmpTypeID as [ID]," + strTextField + " as Name'","ID","Name",true);
		}

		public static void LoadComboJobCode(DropDownList pControl,  string strTextField)
		{
			clsCommon.LoadDropDownListControl(pControl,"sp_GetDataCombo @TableName='LS_tblJobCode',@Fields='LSJobCodeID as [ID]," + strTextField + " as Name'","ID","Name",true);
		}
		
		
		public static void LoadComboLevel1ByCompany(DropDownList pControl, string strTextField, string strCompanyID, string strLangID,System.Web.UI.Page pPage)
		{

			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			//clsCommon.LoadDropDownListControl(pControl,"sp_GetDataCombo @TableName='LS_tblLevel1',@Fields='LSCompanyID, LSLevel1ID as [ID]," + strTextField + " as Name',@Where=N' and LSCompanyID=N''" + strCompanyID.Trim() + "'''","ID","Name",true);
			if (sAccountLogin == "admin")
				clsCommon.LoadDropDownListControl(pControl,"sp_GetDataComboLevel @TableName = 'LS_tblLevel1', @Language='" + strLangID + "',@LSCompanyID=N'" + strCompanyID.Trim() + "',@UserGroupID='" + sAccountLogin + "'","LSLevel1ID","Name",true);
			else
				clsCommon.LoadDropDownListControl(pControl,"sp_GetDataComboLevel @TableName = 'LS_tblLevel1', @Language='" + strLangID + "',@LSCompanyID=N'" + strCompanyID.Trim() + "',@UserGroupID='" + sAccountLogin + "'","LSLevel1ID","Name",true);

			
		}

		public static void LoadComboLevel2ByLevel1(DropDownList pControl, string strTextField, string strLevel1ID, string strLangID,System.Web.UI.Page pPage)
		{
			string sAccountLogin= pPage.Session["AccountLogin"]!=null?pPage.Session["AccountLogin"].ToString():"admin";
			
			//clsCommon.LoadDropDownListControl(pControl,"sp_GetDataCombo @TableName='LS_tblLevel2',@Fields='LSLevel1ID, LSLevel2ID as [ID]," + strTextField + " as Name',@Where=N' and LSLevel1ID=N''" + strLevel1ID.Trim() + "'''","ID","Name",true);
			clsCommon.LoadDropDownListControl(pControl,"sp_GetDataComboLevel @TableName = 'LS_tblLevel2', @Language='" + strLangID + "',@LSLevel1ID=N'" + strLevel1ID.Trim() + "',@UserGroupID='" + sAccountLogin + "'","LSLevel2ID","Name",true);
		}

		public static void LoadComboLevel3ByLevel2(DropDownList pControl, string strTextField, string strLevel2ID, string strLangID)
		{
			//clsCommon.LoadDropDownListControl(pControl,"sp_GetDataCombo @TableName='LS_tblLevel3',@Fields='LSLevel2ID, LSLevel3ID as [ID]," + strTextField + " as Name',@Where=N' and LSLevel2ID=N''" + strLevel2ID.Trim() + "'''","ID","Name",true);
			clsCommon.LoadDropDownListControl(pControl,"sp_GetDataComboLevel @TableName = 'LS_tblLevel3', @Language='" + strLangID + "',@LSLevel2ID=N'" + strLevel2ID.Trim() + "'","LSLevel3ID","Name",true);
		}
		
		
		/// <summary>
		/// Lay thong tin nhan vien
		/// </summary>
		public static string GetListColumnByID(string strListID)
		{
			DataTable dtb = clsCommon.GetDataTable("sp_frmDynamic 'GetListColumnByID',@whereListColumnID=N'" + strListID + "'");
			string strResult = "";
			if (dtb.Rows.Count > 0)
			{
				for (int i=0; i<dtb.Rows.Count; i++)
				{
					if (strResult == "")
						strResult = dtb.Rows[i]["ColumnID"].ToString().Trim();
					else
						strResult = strResult + ", " + dtb.Rows[i]["ColumnID"].ToString().Trim();
				}
			}
			dtb.Dispose();
			return strResult;
		}

		/// <summary>
		/// Hien thi ket qua cuoi cung trong man hinh search nhan vien
		/// </summary>
		public static DataTable GetListDynamic(string strSelectColumn, string strDisplayColumn, string strConditionCode, string strDisplaySequence, string strSortCondition)
		{
			DataTable dtb = clsCommon.GetDataTable("sp_frmDynamic 'ViewResult', @SelectColumn='" + strSelectColumn 
				+ "', @DisplayColumn='" + strDisplayColumn + "', @ConditionCode='" + strConditionCode 
				+ "', @DisplaySequence='" + strDisplaySequence + "', @SortCondition='" + strSortCondition + "'");
			return dtb;
		}

		public static void LoadListFunction(string strModule)
		{
			
		}
		/*LanHTD dua tam, co the xoa sau khi lam SI*/
		public static void LoadComboCompany(DropDownList pControl, string strTextField)
		{
			clsCommon.LoadDropDownListControl(pControl,"sp_GetDataCombo @TableName='LS_tblCompany',@Fields='LSCompanyCode," + strTextField + " as Name'","LSCompanyCode","Name",true);
		}

		public static void LoadComboLevel1ByCompany(DropDownList pControl, string strTextField, string strCompanyID)
		{
			clsCommon.LoadDropDownListControl(pControl,"sp_GetDataCombo @TableName='LS_tblLevel1',@Fields='LSLevel1Code," + strTextField + " as Name',@Where=' and LSCompanyCode=N''" + strCompanyID.Trim() + "'''","LSLevel1Code","Name",true);
		}

		public static void LoadComboLevel2ByLevel1(DropDownList pControl, string strTextField, string strLevel1ID)
		{
			clsCommon.LoadDropDownListControl(pControl,"sp_GetDataCombo @TableName='LS_tblLevel2',@Fields='LSLevel2Code," + strTextField + " as Name',@Where=' and LSLevel1Code=N''" + strLevel1ID.Trim() + "'''","LSLevel2Code","Name",true);
		}

		public static void LoadComboLevel3ByLevel2(DropDownList pControl, string strTextField, string strLevel2ID)
		{
			clsCommon.LoadDropDownListControl(pControl,"sp_GetDataCombo @TableName='LS_tblLevel3',@Fields='LSLevel3Code," + strTextField + " as Name',@Where=' and LSLevel2Code=N''" + strLevel2ID.Trim() + "'''","LSLevel3Code","Name",true);
		}
		/*End Lanhtd*/
	}
}
