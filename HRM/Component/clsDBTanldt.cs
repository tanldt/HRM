using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Configuration;

using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Threading;

namespace iHRPCore.Com
{
	public class DB
	{
		private static IsolationLevel m_isoLevel = IsolationLevel.ReadUncommitted;

		private DB() 
		{
		}
		#region DB Access Functions
		static public IsolationLevel IsolationLevel
		{
			get
			{
				return m_isoLevel;
			}
		}

		/// <summary>
		/// Gets Connection out of Web.config
		/// </summary>
		/// <returns>Returns SqlConnection</returns>
		public static SqlConnection GetConnection() 
		{			
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["pstrConnectionString"]);
			conn.Open();
			return conn;
		}
		/// <summary>
		/// Gets data out of the database
		/// </summary>
		/// <param name="cmd">The SQL Command</param>
		/// <returns>DataTable with the results</returns>
		static private DataTable GetData(SqlCommand cmd) 
		{
			QueryCounter qc = new QueryCounter(cmd.CommandText);
			try 
			{
				if(cmd.Connection!=null) 
				{
					using(DataSet ds = new DataSet()) 
					{
						using(SqlDataAdapter da = new SqlDataAdapter()) 
						{
							da.SelectCommand = cmd;
							da.Fill(ds);
							return ds.Tables[0];
						}
					}
				} 
				else 
				{
					using(SqlConnection conn=GetConnection()) 
					{
						using(SqlTransaction trans=conn.BeginTransaction(m_isoLevel)) 
						{
							try
							{
								cmd.Transaction = trans;
								using(DataSet ds = new DataSet()) 
								{
									using(SqlDataAdapter da = new SqlDataAdapter()) 
									{
										da.SelectCommand = cmd;
										da.SelectCommand.Connection = conn;
										da.Fill(ds);
										return ds.Tables[0];
									}
								}
							}
							finally
							{
								trans.Commit();
							}
						}
					}
				}
			}
			finally 
			{
				qc.Dispose();
			}
		}
		/// <summary>
		/// Gets data out of database using a plain text string command
		/// </summary>
		/// <param name="sql">string command to be executed</param>
		/// <returns>DataTable with results</returns>
		static public DataTable GetData(string sql) 
		{
			QueryCounter qc = new QueryCounter(sql);
			try 
			{
				using(SqlConnection conn=GetConnection()) 
				{
					using(SqlTransaction trans=conn.BeginTransaction(m_isoLevel)) 
					{
						try
						{
							using(SqlCommand cmd=conn.CreateCommand())
							{
								cmd.Transaction = trans;
								cmd.CommandType = CommandType.Text;
								cmd.CommandText = sql;
								using(DataSet ds = new DataSet()) 
								{
									using(SqlDataAdapter da = new SqlDataAdapter()) 
									{
										da.SelectCommand = cmd;
										da.SelectCommand.Connection = conn;
										da.Fill(ds);
										return ds.Tables[0];
									}
								}
							}
						}
						finally
						{
							trans.Commit();
						}
					}
				}
			}
			finally 
			{
				qc.Dispose();
			}
		}
		/// <summary>
		/// Executes a NonQuery
		/// </summary>
		/// <param name="cmd">NonQuery to execute</param>
		static public void ExecuteNonQuery(SqlCommand cmd) 
		{
			QueryCounter qc = new QueryCounter(cmd.CommandText);
			try 
			{
				using(SqlConnection conn=GetConnection()) 
				{
					using(SqlTransaction trans=conn.BeginTransaction(m_isoLevel)) 
					{
						cmd.Connection = conn;
						cmd.Transaction = trans;
						cmd.ExecuteNonQuery();
						trans.Commit();
					}
				}
			}
			finally 
			{
				qc.Dispose();
			}
		}


		static public object ExecuteScalar(SqlCommand cmd) 
		{
			QueryCounter qc = new QueryCounter(cmd.CommandText);
			try 
			{
				using(SqlConnection conn=GetConnection()) 
				{
					using(SqlTransaction trans=conn.BeginTransaction(m_isoLevel)) 
					{
						cmd.Connection = conn;
						cmd.Transaction = trans;
						object res = cmd.ExecuteScalar();
						trans.Commit();
						return res;
					}
				}
			}
			finally
			{
				qc.Dispose();
			}
		}
		/// <summary>
		/// Gets the database size
		/// </summary>
		/// <returns>intager value for database size</returns>
		static public int DBSize() 
		{
			using(SqlCommand cmd = new SqlCommand("select sum(cast(size as integer))/128 from sysfiles")) 
			{
				cmd.CommandType = CommandType.Text;
				return (int)ExecuteScalar(cmd);
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <returns>Isnull</returns>
		public object IsNull(object value)
		{
			if(value==null || value.ToString().ToLower()==string.Empty)
				return DBNull.Value;
			else
				return value;
		}
		#endregion
		#region DataTable LS_spfrmOTWORKHOUR
		static public DataTable LS_spfrmOTWORKHOUR_Table (
			object Activity,object LanguageID,object FromTime,object ToTime,object LSWorkHourID,object where)
		{
			using(SqlCommand cmd = new SqlCommand("LS_spfrmOTWORKHOUR")) 
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add("@Activity",Activity);
				cmd.Parameters.Add("@LanguageID",LanguageID);
 
				cmd.Parameters.Add("@FromTime",FromTime);
				cmd.Parameters.Add("@ToTime",ToTime);
				cmd.Parameters.Add("@LSWorkHourID",LSWorkHourID);
				cmd.Parameters.Add("@where",where);

				return GetData(cmd);
			}
		}
		#endregion

		#region DataRow LS_spfrmOTWORKHOUR
		static public DataRow LS_spfrmOTWORKHOUR_row (
			object Activity,object LanguageID,object FromTime,object ToTime,object LSWorkHourID,object where)
		{
			using(SqlCommand cmd = new SqlCommand("LS_spfrmOTWORKHOUR")) 
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add("@Activity",Activity);
				cmd.Parameters.Add("@LanguageID",LanguageID);
 				cmd.Parameters.Add("@FromTime",FromTime);
				cmd.Parameters.Add("@ToTime",ToTime);
				cmd.Parameters.Add("@LSWorkHourID",LSWorkHourID);
				cmd.Parameters.Add("@where",where);

				using(DataTable dt = GetData(cmd)) 
				{
					return dt.Rows[0];
				}
			}
		}
		#endregion


	}

	#region QueryCounter class
	public sealed class QueryCounter : System.IDisposable
	{
#if DEBUG
		private HiPerfTimer	hiTimer				= new HiPerfTimer(true);
		private string m_cmd;
#endif

		public QueryCounter(string sql) 
		{
#if DEBUG
			m_cmd = sql;

			if(HttpContext.Current.Items["NumQueries"]==null)
				HttpContext.Current.Items["NumQueries"] = (int)1;
			else
				HttpContext.Current.Items["NumQueries"] = 1 + (int)HttpContext.Current.Items["NumQueries"];
#endif
		}

		public void Dispose() 
		{
#if DEBUG
			hiTimer.Stop();

			m_cmd = String.Format("{0}: {1:N3}",m_cmd,hiTimer.Duration);

			if(HttpContext.Current.Items["TimeQueries"]==null)
				HttpContext.Current.Items["TimeQueries"] = hiTimer.Duration;
			else
				HttpContext.Current.Items["TimeQueries"] = hiTimer.Duration + (double)HttpContext.Current.Items["TimeQueries"];

			if(HttpContext.Current.Items["CmdQueries"]==null)
				HttpContext.Current.Items["CmdQueries"] = m_cmd;
			else
				HttpContext.Current.Items["CmdQueries"] += "<br/>" + m_cmd;
#endif
		}

#if DEBUG
		static public void Reset() 
		{
			HttpContext.Current.Items["NumQueries"] = 0;
			HttpContext.Current.Items["TimeQueries"] = (double)0;
			HttpContext.Current.Items["CmdQueries"] = "";
		}

		static public int Count 
		{
			get 
			{
				return (int)HttpContext.Current.Items["NumQueries"];
			}
		}
		static public double Duration 
		{
			get 
			{
				return (double)HttpContext.Current.Items["TimeQueries"];
			}
		}
		static public string Commands 
		{
			get 
			{
				return (string)HttpContext.Current.Items["CmdQueries"];
			}
		}
#endif
	}
	#endregion
	#region HiPerfTimer
	public class HiPerfTimer 
	{
		[DllImport("Kernel32.dll")]
		private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

		[DllImport("Kernel32.dll")]
		private static extern bool QueryPerformanceFrequency(out long lpFrequency);

		private long startTime, stopTime;
		private long freq;

		// Constructor
		public HiPerfTimer(bool bStart) 
		{
			startTime = 0;
			stopTime  = 0;

			if (QueryPerformanceFrequency(out freq) == false) 
			{
				// high-performance counter not supported
				throw new Win32Exception();
			}

			if(bStart) Start();
		}

		// Start the timer
		public void Start() 
		{
			// lets do the waiting threads there work
			Thread.Sleep(0);

			QueryPerformanceCounter(out startTime);
		}

		// Stop the timer
		public void Stop() 
		{
			QueryPerformanceCounter(out stopTime);
		}

		// Returns the duration of the timer (in seconds)
		public double Duration 
		{
			get 
			{
				return (double)(stopTime - startTime) / (double) freq;
			}
		}
		
	}
	#endregion
}
