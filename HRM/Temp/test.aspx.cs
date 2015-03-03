using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
//using Oracle.DataAccess.Client;
//using Oracle.DataAccess.Types;
using System.Data.OracleClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using iHRPCore.Com;
using iHRPCore.Com.Oracle;
using System.Configuration;


namespace iHRPCore.Temp
{
	/// <summary>
	/// Summary description for test.
	/// </summary>
	public class test : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.TextBox txtNote;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Button cmdAddNew;
		protected System.Web.UI.WebControls.Button cmdSave;
		protected System.Web.UI.WebControls.Button cmdDelete;
		protected System.Web.UI.WebControls.TextBox txtID;
		protected System.Web.UI.WebControls.TextBox txtNameVN;
		protected System.Web.UI.WebControls.DataGrid dtgList;
		protected System.Web.UI.WebControls.TextBox txtNameEN;
	

		public static string mConnectionString;
		public static OracleConnection m_Connection;
		public static DataSet m_DataSet;
		public static  OracleCommand m_Command = new OracleCommand();
		protected System.Web.UI.WebControls.Label tblErr;
		public static OracleDataAdapter m_DataAdapter;
		private void Page_Load(object sender, System.EventArgs e)
		{
			//test
			// Put user code to initialize the page here
			BindDataGrid();
			// Create OracleConnection object                     
//			OracleConnection myConnection = new OracleConnection();
//			// Connection String
//			string mConnectionString = ConfigurationSettings.AppSettings["connString"];
//			myConnection.ConnectionString = mConnectionString;
//			try
//			{
//				// Open the connection                             
//				myConnection.Open();
//			}                                                     
//			catch(Exception ex)
//			{                                                     
//				tblErr.Text  = ex.Message;
//			}                                                     
//			finally                                               
//			{                                                     
//				// Close the connection                          
//				myConnection.Close();                            
//			}  
		}
		private void BindDataGrid()
		{
			DataTable dtList = new DataTable();
			try
			{
				//dtList= clsCommonOra.GetDataTableHasID("select * from TEMP");
			
				OracleParameterCollection clcParameters;
				clcParameters = clsCommonOra.PARAMETERS;
				clcParameters.Add("pActivity", clsCommonOra.SafeDataToDatabase("GetDataAll","DBNull"));
				//clcParameters.Add("pComboEmpty", clsCommonOra.SafeDataToDatabase("","DBNull"));
//				clcParameters.Add("pLanguageID", clsCommonOra.SafeDataToDatabase("","DBNull"));
//				clcParameters.Add("pLSAllowanceCode", clsCommonOra.SafeDataToDatabase("","DBNull"));
//				clcParameters.Add("pName", clsCommonOra.SafeDataToDatabase("","DBNull"));
//				clcParameters.Add("pVNName", clsCommonOra.SafeDataToDatabase("","DBNull"));
//				clcParameters.Add("pType", clsCommonOra.SafeDataToDatabase(0,"DBNull"));
//				clcParameters.Add("pNote", clsCommonOra.SafeDataToDatabase("","DBNull"));
//				clcParameters.Add("pRank", clsCommonOra.SafeDataToDatabase(0,"DBNull"));
//				clcParameters.Add("pUsed", clsCommonOra.SafeDataToDatabase(0,"DBNull"));
				clcParameters.Add("pWhere", clsCommonOra.SafeDataToDatabase("","DBNull"));
				clcParameters.Add("pRC1",  OracleType.Cursor).Direction = ParameterDirection.Output;
				clcParameters.Add("pReturnMess",  OracleType.VarChar,200).Direction = ParameterDirection.Output;
				clcParameters.Add("pLSAllowanceID",  OracleType.VarChar,24).Direction = ParameterDirection.InputOutput;

				dtList = clsCommonOra.GetDataTableHasID_ByStore("SQLTOORA.LS_SPFRMALLOWANCE");

//				clcParameters.Add("RS",  OracleType.Cursor).Direction = ParameterDirection.Output;
//				dtList = clsCommonOra.GetDataTableHasID_ByStore("PKG_LS_TBLALLOWANCE.sp_GetAll");
//				
				dtgList.DataSource = dtList;
				dtgList.DataBind();
			}
			catch(Exception ex)
			{
				tblErr.Text = ex.Message;
			}
			finally
			{
				dtList.Dispose();
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.cmdAddNew.Click += new System.EventHandler(this.cmdAddNew_Click);
			this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
			this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void cmdSave_Click(object sender, System.EventArgs e)
		{
			OracleParameterCollection clcParameters;
			//OracleParameter objPmt;
			try
			{
				clcParameters = clsCommonOra.PARAMETERS;

				//clcParameters.Add("pActivity", clsCommonOra.SafeDataToDatabase("Save","DBNull"));
				clcParameters.Add("P_LSALLOWANCEID", clsCommonOra.SafeDataToDatabase(txtID.Text,"DBNull"));
				clcParameters.Add("P_LSALLOWANCECODE", clsCommonOra.SafeDataToDatabase(txtID.Text,"DBNull"));
				clcParameters.Add("P_NAME", clsCommonOra.SafeDataToDatabase(txtNameEN.Text,"DBNull"));
				clcParameters.Add("P_VNNAME", clsCommonOra.SafeDataToDatabase(txtNameVN.Text,"DBNull"));
				clcParameters.Add("P_NOTE", clsCommonOra.SafeDataToDatabase(txtNote.Text,"DBNull"));
				clcParameters.Add("P_TYPE", clsCommonOra.SafeDataToDatabase(1,"DBNull"));
				clcParameters.Add("P_RANK", clsCommonOra.SafeDataToDatabase(1,"DBNull"));
				clcParameters.Add("P_USED", clsCommonOra.SafeDataToDatabase(1,"DBNull"));
				bool a = clsCommonOra.ExecuteSQL("PKG_LS_TBLALLOWANCE.sp_Insert");
//				clcParameters.Add("P_ID", clsCommonOra.SafeDataToDatabase(txtID.Text,"DBNull"));
//				clcParameters.Add("P_NAME", clsCommonOra.SafeDataToDatabase(txtNameEN.Text,"DBNull"));
//				clcParameters.Add("P_VNNAME", clsCommonOra.SafeDataToDatabase(txtNameVN.Text,"DBNull"));
//				clcParameters.Add("P_NOTE", clsCommonOra.SafeDataToDatabase(txtNote.Text,"DBNull"));
				//clcParameters.Add("RS",  OracleType.Cursor).Direction = ParameterDirection.Output;

				//bool a = clsCommonOra.ExecuteSQL("PKG_TEMP.sp_Insert");
				

				BindDataGrid();
			}

			catch (Exception ex)
			{
				throw ex;
			}
		}

		private void cmdAddNew_Click(object sender, System.EventArgs e)
		{
			this.tblErr.Text = "";
			this.txtID.Text = "";
			this.txtNameEN.Text = "";
			this.txtNameVN.Text = "";
			this.txtNote.Text = "";
//			string mConn=ConfigurationSettings.AppSettings["connString"];
//			OracleConnection conn=new OracleConnection(mConn);
//			OracleCommand cmd = new OracleCommand();
//			cmd=conn.CreateCommand();
//			cmd.CommandText="LS_SPFRMALLOWANCE";
//			cmd.CommandType=CommandType.StoredProcedure;

//			m_Command.Parameters.Add("pActivity", OracleType.NVarChar,200).Value = clsCommonOra.SafeDataToDatabase("GetDataByID","DBNull");
//			m_Command.Parameters.Add("pLSAllowanceID", OracleType.NVarChar,30).Value = clsCommonOra.SafeDataToDatabase(txtID.Text,"DBNull");
//			m_Command.Parameters.Add("pLSAllowanceCode", OracleType.NVarChar,30).Value = clsCommonOra.SafeDataToDatabase(txtID.Text,"DBNull");
//			m_Command.Parameters.Add("pName", OracleType.NVarChar,300).Value =clsCommonOra.SafeDataToDatabase(txtNameEN.Text,"DBNull");
//			m_Command.Parameters.Add("pVNName", OracleType.NVarChar,300).Value = clsCommonOra.SafeDataToDatabase(txtNameVN.Text,"DBNull");
//			m_Command.Parameters.Add("pType", OracleType.Int16).Value = clsCommonOra.SafeDataToDatabase(1,"DBNull");
//			m_Command.Parameters.Add("pNote", OracleType.NVarChar,510).Value =  clsCommonOra.SafeDataToDatabase(txtNote.Text,"DBNull");
//			m_Command.Parameters.Add("pRank", OracleType.Int16).Value = clsCommonOra.SafeDataToDatabase(1,"DBNull");
//			m_Command.Parameters.Add("pUsed", OracleType.Int16).Value = clsCommonOra.SafeDataToDatabase(1,"DBNull");
//			m_Command.Parameters.Add("pRC1",  OracleType.Cursor).Direction = ParameterDirection.Output;


//			OracleParameter param_in=cmd.Parameters.Add("pActivity",OracleType.VarChar,200);
//			param_in.Direction=ParameterDirection.Input;
//			param_in.Value="Save";
//
//			OracleParameter param1_in=cmd.Parameters.Add("pLSAllowanceCode",OracleType.VarChar,30);
//			param1_in.Direction=ParameterDirection.Input;
//			param1_in.Value = txtID.Text;
//
//			OracleParameter param2_in=cmd.Parameters.Add("pName",OracleType.VarChar,300);
//			param2_in.Direction=ParameterDirection.Input;
//			param2_in.Value = txtNameEN.Text;
//
//			OracleParameter param3_in=cmd.Parameters.Add("pVNName",OracleType.VarChar,300);
//			param3_in.Direction=ParameterDirection.Input;
//			param3_in.Value = txtNameVN.Text;
//
//			OracleParameter param4_in=cmd.Parameters.Add("pType",OracleType.Int16);
//			param4_in.Direction=ParameterDirection.Input;
//			param4_in.Value = 1;
//
//			OracleParameter param5_in=cmd.Parameters.Add("pNote",OracleType.NVarChar,500);
//			param5_in.Direction=ParameterDirection.Input;
//			param5_in.Value = txtNote.Text;
//
//			OracleParameter param6_in=cmd.Parameters.Add("pRank",OracleType.Int16);
//			param6_in.Direction=ParameterDirection.Input;
//			param6_in.Value = 1;
//			OracleParameter param7_in=cmd.Parameters.Add("pUsed",OracleType.Int16);
//			param7_in.Direction=ParameterDirection.Input;
//			param7_in.Value = 1;
//
//			OracleParameter param_out=cmd.Parameters.Add("PRC1",OracleType.Cursor);
//			param_out.Direction=ParameterDirection.Output;

//			OracleParameter param_inout=cmd.Parameters.Add("paraminout",OracleType.VarChar,20);
//			param_inout.Direction=ParameterDirection.InputOutput;
//			param_inout.Value=m_TxtIO.Text;

//			conn.Open();
//			cmd.ExecuteNonQuery();
//			tblErr.Text="显示："+param_out.Value.ToString();
//			conn.Close(); 
		}

		private void cmdDelete_Click(object sender, System.EventArgs e)
		{
			// Declare database objects such as connection,
			// command and transaction
			string mConn=ConfigurationSettings.AppSettings["connString"];
			
			OracleConnection cnn;
			OracleCommand cmd;
			OracleTransaction tran;

			// Open a connection to Oracle 9i
			cnn=new OracleConnection(mConn);
			cnn.Open();

			// Begin the transaction
			tran=cnn.BeginTransaction();

			// Configure command object to use the transaction
			cmd=new OracleCommand();
			cmd.Connection=cnn;
			cmd.Transaction=tran;

			// Put transaction commands in a try ... catch block
			try
			{

				// UPDATE EMP Table with values from commandline
				string  PLSALLOWANCECODE="TX61";
				string PNAME=txtNameEN.Text;
				string PVNNAME=txtNameVN.Text;

				string query="UPDATE LS_TBLALLOWANCE SET NAME = '"
							+ PNAME + 
					"',VNNAME = '"+ PVNNAME +
							"' WHERE LSALLOWANCEID = '" + PLSALLOWANCECODE + "'";
				cmd.CommandText=query;
				long rows = cmd.ExecuteNonQuery();

				// Commit the transaction ....
				tran.Commit();
				Response.Write(string.Format("Commit complete, {0} ", rows + " Rows updated"));
			}

				// ... or Rollback everything in case of an error
			catch 
			{
				tran.Rollback();
				
				Response.Write("Transaction failed - Rolled Back!");
				//Response.Write(e.Message);
			}
		}
	}
}
