using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using ConnectData;
		
public class clsSYS_tblAppUser
{
	private string ConnectionString = ConfigurationSettings.AppSettings["pstrConnectionString"];

	#region Member Variables
	protected String m_UserID;
	protected String m_ApplicationID;
	protected String m_isAdmin;
	protected String m_KeyChecked;
	#endregion

	#region Public Properties
    public String UserID
    {
        get
        {
			return this.m_UserID;
		}
        set
        {
			this.m_UserID = value;
		}
    }
		
    public String ApplicationID
    {
        get
        {
			return this.m_ApplicationID;
		}
        set
        {
			this.m_ApplicationID = value;
		}
    }
		
    public String isAdmin
    {
        get
        {
			return this.m_isAdmin;
		}
        set
        {
			this.m_isAdmin = value;
		}
    }
		
    public String KeyChecked
    {
        get
        {
			return this.m_KeyChecked;
		}
        set
        {
			this.m_KeyChecked = value;
		}
    }
		
	#endregion

	#region Constructors
	public clsSYS_tblAppUser()
	{
		this.Reset();
	}
	#endregion

	#region Public Functions
    public String Add()
    {
        SQLProvider objProvider = new SQLProvider(ConnectionString);
		Int32 ID = 0;
		try
        {
            objProvider.Parameters.Clear();
            objProvider.Parameters.Add("@P_Activity", "Insert");
            
			objProvider.Parameters.Add("@P_UserID", clsCommon.SafeDataToDatabase(this.UserID, "String", ""));
			objProvider.Parameters.Add("@P_ApplicationID", clsCommon.SafeDataToDatabase(this.ApplicationID, "String", ""));
			objProvider.Parameters.Add("@P_isAdmin", clsCommon.SafeDataToDatabase(this.isAdmin, "String", ""));
			objProvider.Parameters.Add("@P_KeyChecked", clsCommon.SafeDataToDatabase(this.KeyChecked, "String", ""));
			ID = objProvider.ExecuteSP("sp_SYS_tblAppUser");
			return (String)objProvider.Parameters["@P_"].Value;
        }
		catch (Exception ex)
		{
			return ID.ToString();
		}
		finally
		{
			objProvider = null;
		}
    }
		
    public Int32 Update()
    {
        SQLProvider objProvider = new SQLProvider(ConnectionString);
		try
        {
            objProvider.Parameters.Clear();
            objProvider.Parameters.Add("@P_Activity", "Update");
            
			objProvider.Parameters.Add("@P_UserID", clsCommon.SafeDataToDatabase(this.UserID, "String", ""));
			objProvider.Parameters.Add("@P_ApplicationID", clsCommon.SafeDataToDatabase(this.ApplicationID, "String", ""));
			objProvider.Parameters.Add("@P_isAdmin", clsCommon.SafeDataToDatabase(this.isAdmin, "String", ""));
			objProvider.Parameters.Add("@P_KeyChecked", clsCommon.SafeDataToDatabase(this.KeyChecked, "String", ""));
			return (Int32)objProvider.ExecuteSP("sp_SYS_tblAppUser");
        }
		catch (Exception ex)
		{
			return -1;
		}
		finally
		{
			objProvider = null;
		}
    }
		
    public DataView GetAll()
    {
        SQLProvider objProvider = new SQLProvider(ConnectionString);
		try
        {
			objProvider.Parameters.Clear();
			objProvider.Parameters.Add("@P_Activity", "GetAll");
			return (DataView)objProvider.spGetDataView("sp_SYS_tblAppUser");
        }
		catch (Exception ex)
		{
			return null;
		}
		finally
		{
			objProvider = null;
		}
    }
		
    public DataView GetSearch(string mWhereMore)
    {
        SQLProvider objProvider = new SQLProvider(ConnectionString);
		try
        {
			objProvider.Parameters.Clear();
			objProvider.Parameters.Add("@P_Activity", "GetSearch");
            
			objProvider.Parameters.Add("@P_UserID", clsCommon.SafeDataToDatabase(this.UserID, "String", ""));
			objProvider.Parameters.Add("@P_ApplicationID", clsCommon.SafeDataToDatabase(this.ApplicationID, "String", ""));
			objProvider.Parameters.Add("@P_isAdmin", clsCommon.SafeDataToDatabase(this.isAdmin, "String", ""));
			objProvider.Parameters.Add("@P_KeyChecked", clsCommon.SafeDataToDatabase(this.KeyChecked, "String", ""));
            objProvider.Parameters.Add("@P_WhereIn", mWhereMore);
			return (DataView)objProvider.spGetDataView("sp_SYS_tblAppUser");
        }
		catch (Exception ex)
		{
			return null;
		}
		finally
		{
			objProvider = null;
		}
    }
		
    public Int32 Delete()
    {
        SQLProvider objProvider = new SQLProvider(ConnectionString);
		try
        {
            objProvider.Parameters.Clear();
            objProvider.Parameters.Add("@P_Activity", "Delete");
            
			objProvider.Parameters.Add("@P_UserID", clsCommon.SafeDataToDatabase(this.UserID, "String", ""));
			objProvider.Parameters.Add("@P_ApplicationID", clsCommon.SafeDataToDatabase(this.ApplicationID, "String", ""));
			return (Int32)objProvider.ExecuteSP("sp_SYS_tblAppUser");
        }
		catch (Exception ex)
		{
			return -1;
		}
		finally
		{
			objProvider = null;
		}
    }

    public Int32 DeleteInList(string IDlist)
    {
        SQLProvider objProvider = new SQLProvider(ConnectionString);
		try
        {
            objProvider.Parameters.Clear();
            objProvider.Parameters.Add("@P_Activity", "Delete");
            objProvider.Parameters.Add("@P_WhereIn", IDlist);
            return (Int32)objProvider.ExecuteSP("sp_SYS_tblAppUser");
        }
		catch (Exception ex)
		{
			return -1;
		}
		finally
		{
			objProvider = null;
		}
    }
		
    public DataView GetByKey()
    {
        SQLProvider objProvider = new SQLProvider(ConnectionString);
		try
        {
			objProvider.Parameters.Clear();
            objProvider.Parameters.Add("@P_Activity", "GetByKey");
            
			objProvider.Parameters.Add("@P_KeyChecked", clsCommon.SafeDataToDatabase(this.KeyChecked, "String", ""));
            return (DataView)objProvider.spGetDataView("sp_SYS_tblAppUser");
        }
		catch (Exception ex)
		{
			return null;
		}
		finally
		{
			objProvider = null;
		}
    }
		
    public bool CheckedByKey()
    {
        DataView objDataView = GetByKey();
        if (objDataView != null && objDataView.Table.Rows.Count > 0)
        {
            this.FillData(objDataView.Table.Rows[0]);
            return true;
        }
        else
        {
            this.Reset();
            return false;
        }
    }
		
	public void ImpactData(System.Web.UI.Control ctrl, bool isCtr2Field)
	{
		if (isCtr2Field)
		{
			Hashtable ht = clsCommon.webGetValueControl(ref ctrl);
			
			if (ht.ContainsKey("UserID")) UserID = ht["UserID"].ToString();
			if (ht.ContainsKey("ApplicationID")) ApplicationID = ht["ApplicationID"].ToString();
			if (ht.ContainsKey("isAdmin")) isAdmin = ht["isAdmin"].ToString();
			if (ht.ContainsKey("KeyChecked")) KeyChecked = ht["KeyChecked"].ToString();
		}
		else
		{
			
			clsCommon.webSetValue2Control(ref ctrl, "UserID", UserID);
			clsCommon.webSetValue2Control(ref ctrl, "ApplicationID", ApplicationID);
			clsCommon.webSetValue2Control(ref ctrl, "isAdmin", isAdmin);
			clsCommon.webSetValue2Control(ref ctrl, "KeyChecked", KeyChecked);
		}
	}
		
	#endregion

	#region Private Functions
    private void FillData(DataRow row)
    {
		
        UserID = (row["UserID"] == DBNull.Value? String.Empty: clsCommon.SafeDataToClient(row["UserID"], "String", "").ToString());
        ApplicationID = (row["ApplicationID"] == DBNull.Value? String.Empty: clsCommon.SafeDataToClient(row["ApplicationID"], "String", "").ToString());
        isAdmin = (row["isAdmin"] == DBNull.Value? String.Empty: clsCommon.SafeDataToClient(row["isAdmin"], "String", "").ToString());
        KeyChecked = (row["KeyChecked"] == DBNull.Value? String.Empty: clsCommon.SafeDataToClient(row["KeyChecked"], "String", "").ToString());
    }
		
    private void Reset()
	{
		
        UserID = String.Empty;
        ApplicationID = String.Empty;
        isAdmin = String.Empty;
        KeyChecked = String.Empty;
	}
		
	#endregion

	#region User Functions
	#endregion

}