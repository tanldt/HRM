using System;
using System.Configuration;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.IO;
using iHRPCore.Com;
using iHRPCore.PRComponent;
using iHRPCore.HRComponent;

namespace iHRPCore.Webforms.HR
{
	/// <summary>
	/// Summary description for HR_frmAllowance.
	/// </summary>
	public class HR_frmImport : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button cmdSave;
		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.WebControls.TextBox txtDecID;
		protected System.Web.UI.WebControls.Button cmdAddNew;
		protected System.Web.UI.WebControls.DataGrid grdList;
		protected System.Web.UI.WebControls.Label lblRowError;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			
			if (!this.IsPostBack )
			{
				LoadDataGrid();
			}
//			cmdSave.Attributes.Add("OnClick", "return Check_ValidInputData()");			
//			cmdAddNew.Attributes.Add("OnClick", "javascript:reset();");
		}
		#region Load datagrid
		/// <summary>
		/// LOAD SALARY ADJUST OF EMP
		/// </summary>
		private void LoadDataGrid()
		{
//			DataTable dtList = new DataTable();
			try
			{
//				dtList = clsCommon.GetDataTable("___Import @Activity='GetDataAll'");
				grdList.DataSource = clsCommon.GetDataTable("___Import @Activity='GetDataAll'");
				grdList.CurrentPageIndex = 0;
				grdList.DataBind();
			}
			catch(Exception ex)
			{
				lblError.Text = ex.Message;
			}
//			finally
//			{
//				dtList.Dispose();
//			}
		}
		#endregion
		#region Huy bo
//		private void ClearControls()
//		{
//
//			this.DataGrid1.DataSource= null;
//			this.DataGrid1.DataBind();
//
//			this.lblError.Text="";
//			this.lblRowError.Text = "";
//		}
//
//		private void Load_cbobox()
//		{
//		}
//
//		#region Tat ca cac ham can thiet cho viec tach chuoi
//		/// <summary>
//		/// Ham lay True/False Dang vien
//		/// </summary>
//		/// <param name="e">X</param>
//		private bool Func_Return_DVien(string strDVien)
//		{
//			bool blnDVien;
//			if (strDVien == "")
//			{
//				blnDVien = false;
//			}
//			else
//				blnDVien = true;
//			return blnDVien;
//		}
//		/// <summary>
//		/// Ham lay Ten
//		/// </summary>
//		/// <param name="e">Fullname</param>
//		private string Func_Return_LastName(string strFullName)
//		{
//			string strLastName = "";
//			string[] marrstrFullName = strFullName.Split(' ');
//			if (marrstrFullName.Length > 1)
//			{
//				strLastName = marrstrFullName.GetValue(marrstrFullName.Length - 1).ToString().Trim();
//			}
//			return strLastName;
//		}
//		/// <summary>
//		/// Ham lay Ho
//		/// </summary>
//		/// <param name="e">Fullname</param>
//		private string Func_Return_FirstName(string strFullName)
//		{
//			string strFirstName = "";
//			string[] marrstrFullName = strFullName.Split(' ');
//			if (marrstrFullName.Length > 1)
//			{
//				for(int i=0;i<(marrstrFullName.Length-1);i++)
//				{
//					if (strFirstName == "")
//						strFirstName = marrstrFullName.GetValue(i).ToString().Trim();
//					else
//						strFirstName = strFirstName + ' ' + marrstrFullName.GetValue(i).ToString().Trim();
//				}
//			}
//			return strFirstName;
//		}
//		/// <summary>
//		/// Ham format chuoi ngay
//		/// </summary>
//		/// <param name="e">date</param>
//		private string Func_Return_Date(string strDate)
//		{
//			string mDate = "";
//			if (strDate.Trim() == "")
//				return "";
//			string[] mArray = strDate.Split(new Char [] {' ', '/', '-'});
//			
//			if (mArray.Length == 1)
//				return "01/01/" + strDate;
//			if (mArray.Length == 2)
//			{
//				mDate = "01";
//
//				mDate = mDate + '/' + mArray.GetValue(0).ToString().Trim();
//				if (Convert.ToInt16(mArray.GetValue(1).ToString().Trim()) < 20)
//					mDate = mDate + "/20" + mArray.GetValue(1).ToString().Trim();
//				else if (Convert.ToInt16(mArray.GetValue(1).ToString().Trim()) > 20 && Convert.ToInt16(mArray.GetValue(1).ToString().Trim()) < 100)
//					mDate = mDate + "/19" + mArray.GetValue(1).ToString().Trim();
//				else
//					mDate = mDate + "/" + mArray.GetValue(1).ToString().Trim();
//			}
//			else if (mArray.Length == 3)
//			{
//				mDate = mArray.GetValue(0).ToString().Trim();
//				mDate = mDate + '/' + mArray.GetValue(1).ToString().Trim();
//				if (Convert.ToInt16(mArray.GetValue(2).ToString().Trim()) < 20)
//					mDate = mDate + "/20" + mArray.GetValue(2).ToString().Trim();
//				else if (Convert.ToInt16(mArray.GetValue(2).ToString().Trim()) > 20 && Convert.ToInt16(mArray.GetValue(2).ToString().Trim()) < 100)
//					mDate = mDate + "/19" + mArray.GetValue(2).ToString().Trim();
//				else
//					mDate = mDate + "/" + mArray.GetValue(2).ToString().Trim();
//			}
//			return mDate;
//		}
//		/// <summary>
//		/// Ham Format He So
//		/// </summary>
//		/// <param name="e">Heso</param>
//		private string Func_Return_Heso(string strHeso)
//		{
//			string strHSo = "";
//			string[] mArray = strHeso.Split(new Char [] {' ', ',', '.'});
//			if (mArray.Length > 1)
//			{
//				for(int i=0;i<mArray.Length;i++)
//				{
//					if (strHSo == "")
//						strHSo = mArray.GetValue(i).ToString().Trim();
//					else
//						strHSo = strHSo + '.' + mArray.GetValue(i).ToString().Trim();
//				}
//			}
//			return strHSo;
//		}
//		/// <summary>
//		/// Ham lay Ma Hop dong
//		/// </summary>
//		/// <param name="e">LoaiHD</param>
//		private string Func_Return_ContractID(string strLoaiHD)
//		{
//			string strContractID = "";
//			if (strLoaiHD.Substring(1,2) == "12")
//				strContractID = "12T";
//			else if (strLoaiHD.Substring(1,2) == "24")
//				strContractID = "24T";
//			else if (strLoaiHD.Substring(1,2) == "36")
//				strContractID = "36T";
//			else if (strLoaiHD.Substring(1,2) == "KX")
//				strContractID = "12T";
//			//string[] mArray = strLoaiHD.Split(new Char [] {' '});
////			if (mArray.Length > 1)
////			{
////				for(int i=0;i<mArray.Length;i++)
////				{
////					if (strHSo == "")
////						strHSo = mArray.GetValue(i).ToString().Trim();
////					else
////						strHSo = strHSo + '.' + mArray.GetValue(i).ToString().Trim();
////				}
////			}
//			return strContractID;
//		}
//		#endregion
		#endregion
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void cmdAddNew_Click(object sender, System.EventArgs e)
		{
			LoadDataGrid();
		}

		private void cmdSave_Click(object sender, System.EventArgs e)
		{
		
		}
		
		#region Huy bo
//		private void cmdSave_Click(object sender, System.EventArgs e)
//		{
//
//		}
//
//		private bool ImportList()
//		{
//			if(this.CopyFile_ToServer()==false)	return false;
//			string mstr_FileName = Path.GetFileName(txtFile.Value.Trim()).Trim();
//			if (!File.Exists(ConfigurationSettings.AppSettings["pstrImportFileFolder"].Trim() + mstr_FileName))
//			{
//				lblError.Text = "File not found, Please check path of the filename again!";
//				return false;
//			}			
//			string mstr_PathFileName = ConfigurationSettings.AppSettings["pstrImportFileFolder"].Trim() + mstr_FileName;
//			//------------------
//
//			string strConn;
//				//strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtFile.Value.Trim() + ";Extended Properties=Excel 8.0;";			
//			strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + mstr_PathFileName + ";Extended Properties=Excel 8.0;";			
//				OleDbDataAdapter myCommand = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", strConn);
//				DataSet myDataSet = new DataSet();	
//			try
//			{
//				string strEmpID = "";
//				string strFullName = "";
//				string strLastName = "";
//				string strFirstName = "";
//				string strDOB1 = "";
//				string strDOB0 = "";
//				string strNgaysinh = "";
//				bool blnGender;
//				string strNguyenQuan = "";
//				string strTrinhdoVH = "";
//				string strNganhDT = "";
//				string strTrinhdoDT = "";
//				string strThoigianDT = "";
//				string strCDCV = "";
//				string strLuongBac = "";
//				string strLuongHeso = "";
//				string strLuongNgayNang = "";
//				string strNamvaoNN = "";
//				string strNamvaoDK = "";
//				string strHDLDLoai = "";
//				string strHDLDTGian = "";
//				bool blnDangVien;
//				string strDiachithuongtru = "";
//				string strDiachihientai = "";
//
//				myCommand.Fill(myDataSet, "ExcelData");
//				SqlCommand  cmd = new SqlCommand();
//				clsCommon.SQLTransaction = clsCommon.SQLConnection().BeginTransaction();
//				cmd.Connection = clsCommon.SQLConnection();
//				cmd.Transaction= clsCommon.SQLTransaction;
//				cmd.CommandType = CommandType.StoredProcedure;
//				cmd.CommandText = "HR_spfrmImport";			
//				for(int i=3;i<myDataSet.Tables["ExcelData"].Rows.Count;i++)
//				{
//					// gán string cho tất cả giá trị cần lấy
//					Session["ID_Error"] = "";
//
//					strFullName = "";
//
//					strEmpID = myDataSet.Tables["ExcelData"].Rows[i][1].ToString().Trim();
//					Session["ID_Error"] = strEmpID.ToString();
//					strFullName = myDataSet.Tables["ExcelData"].Rows[i][2].ToString().Trim();
//					strLastName = Func_Return_LastName(strFullName);
//					strFirstName = Func_Return_FirstName(strFullName);
//					strDOB1 = myDataSet.Tables["ExcelData"].Rows[i][4].ToString().Trim();
//					strDOB0 = myDataSet.Tables["ExcelData"].Rows[i][5].ToString().Trim();
//					if (strDOB1.Trim() != "")
//					{
//						strNgaysinh = Func_Return_Date(strDOB1);
//						//blnGender = true;
//					}
//					else
//					{
//						strNgaysinh = Func_Return_Date(strDOB0);
//						//blnGender = false;
//					}
//					strNguyenQuan = myDataSet.Tables["ExcelData"].Rows[i][6].ToString().Trim();
//					strTrinhdoVH = myDataSet.Tables["ExcelData"].Rows[i][7].ToString().Trim();
//					strNganhDT = myDataSet.Tables["ExcelData"].Rows[i][8].ToString().Trim();
//					strTrinhdoDT = myDataSet.Tables["ExcelData"].Rows[i][9].ToString().Trim();
//					strThoigianDT = myDataSet.Tables["ExcelData"].Rows[i][10].ToString().Trim();
//					strCDCV = myDataSet.Tables["ExcelData"].Rows[i][11].ToString().Trim();
//					strLuongBac = myDataSet.Tables["ExcelData"].Rows[i][12].ToString().Trim();
//					strLuongHeso = Func_Return_Heso(myDataSet.Tables["ExcelData"].Rows[i][13].ToString().Trim());
//					strLuongNgayNang = Func_Return_Date(myDataSet.Tables["ExcelData"].Rows[i][14].ToString().Trim());
//					strNamvaoNN = Func_Return_Date(myDataSet.Tables["ExcelData"].Rows[i][15].ToString().Trim());
//					strNamvaoDK = Func_Return_Date(myDataSet.Tables["ExcelData"].Rows[i][16].ToString().Trim());
//					strHDLDLoai = myDataSet.Tables["ExcelData"].Rows[i][17].ToString().Trim();
//					strHDLDTGian = Func_Return_Date(myDataSet.Tables["ExcelData"].Rows[i][18].ToString().Trim());
//					blnDangVien = Func_Return_DVien(myDataSet.Tables["ExcelData"].Rows[i][19].ToString().Trim());
//					strDiachithuongtru = myDataSet.Tables["ExcelData"].Rows[i][20].ToString().Trim();
//					strDiachihientai = myDataSet.Tables["ExcelData"].Rows[i][21].ToString().Trim();
//					if (myDataSet.Tables["ExcelData"].Rows[i][22].ToString().Trim() == "Nam")
//						blnGender = true;
//					else
//						blnGender = false;
//
//					cmd.Parameters.Clear();
//					cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 50).Value = "Import_Emp_Detail";
//					cmd.Parameters.Add("@EmpID", SqlDbType.NVarChar, 12).Value =  strEmpID;
//					cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 100).Value =  strFirstName;
//					cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 100).Value =  strLastName;
//
//					if (strNgaysinh.ToString() == "")
//						cmd.Parameters.Add("@Ngaysinh", SqlDbType.NVarChar, 12).Value =  DBNull.Value;
//					else
//						cmd.Parameters.Add("@Ngaysinh", SqlDbType.NVarChar, 12).Value =  strNgaysinh.ToString();
//
//					cmd.Parameters.Add("@Gender", SqlDbType.Bit).Value =  blnGender;
//
//					if (strNguyenQuan.ToString() == "")
//                        cmd.Parameters.Add("@NguyenQuan", SqlDbType.NVarChar, 100).Value =  DBNull.Value;
//					else
//						cmd.Parameters.Add("@NguyenQuan", SqlDbType.NVarChar, 100).Value =  strNguyenQuan.ToString();
//
//					if (strTrinhdoVH.ToString() == "")
//						cmd.Parameters.Add("@TrinhdoVH", SqlDbType.NVarChar, 100).Value =  DBNull.Value;
//					else
//						cmd.Parameters.Add("@TrinhdoVH", SqlDbType.NVarChar, 100).Value =  strTrinhdoVH.ToString();
//
//					if (strNganhDT.ToString() == "")
//						cmd.Parameters.Add("@NganhDT", SqlDbType.NVarChar, 100).Value =  DBNull.Value;
//					else
//						cmd.Parameters.Add("@NganhDT", SqlDbType.NVarChar, 100).Value =  strNganhDT.ToString();
//
//					if (strTrinhdoDT.ToString() == "")
//						cmd.Parameters.Add("@TrinhdoDT", SqlDbType.NVarChar, 100).Value =  DBNull.Value;
//					else
//						cmd.Parameters.Add("@TrinhdoDT", SqlDbType.NVarChar, 100).Value =  strTrinhdoDT.ToString();
//
//					if (strThoigianDT.ToString() == "")
//						cmd.Parameters.Add("@ThoigianDT", SqlDbType.NVarChar, 100).Value =  DBNull.Value;
//					else
//						cmd.Parameters.Add("@ThoigianDT", SqlDbType.NVarChar, 100).Value =  strThoigianDT.ToString();
//
//					if (strCDCV.ToString() == "")
//						cmd.Parameters.Add("@CDCV", SqlDbType.NVarChar, 100).Value =  DBNull.Value;
//					else
//						cmd.Parameters.Add("@CDCV", SqlDbType.NVarChar, 100).Value =  strCDCV.ToString();
//
//					if (strLuongBac.ToString() == "")
//						cmd.Parameters.Add("@LuongBac", SqlDbType.NVarChar, 100).Value =  DBNull.Value;
//					else
//						cmd.Parameters.Add("@LuongBac", SqlDbType.NVarChar, 100).Value =  strLuongBac.ToString();
//
//					if (strLuongHeso.ToString() == "")
//						cmd.Parameters.Add("@LuongHeso", SqlDbType.NVarChar, 100).Value =  DBNull.Value;
//					else
//						cmd.Parameters.Add("@LuongHeso", SqlDbType.NVarChar, 100).Value =  strLuongHeso.ToString();
//
//					if (strLuongNgayNang.ToString() == "")
//						cmd.Parameters.Add("@LuongNgayNang", SqlDbType.NVarChar, 100).Value =  DBNull.Value;
//					else
//						cmd.Parameters.Add("@LuongNgayNang", SqlDbType.NVarChar, 100).Value =  strLuongNgayNang.ToString();
//
//					if (strNamvaoNN.ToString() == "")
//						cmd.Parameters.Add("@NamvaoNN", SqlDbType.NVarChar, 100).Value =  DBNull.Value;
//					else
//						cmd.Parameters.Add("@NamvaoNN", SqlDbType.NVarChar, 100).Value =  strNamvaoNN.ToString();
//
//					if (strNamvaoDK.ToString() == "")
//						cmd.Parameters.Add("@NamvaoDK", SqlDbType.NVarChar, 100).Value =  DBNull.Value;
//					else
//						cmd.Parameters.Add("@NamvaoDK", SqlDbType.NVarChar, 100).Value =  strNamvaoDK.ToString();
//
//					if (strHDLDLoai.ToString() == "")
//						cmd.Parameters.Add("@HDLDLoai", SqlDbType.NVarChar, 100).Value =  DBNull.Value;
//					else
//						cmd.Parameters.Add("@HDLDLoai", SqlDbType.NVarChar, 100).Value =  strHDLDLoai.ToString();
//
//					if (strHDLDTGian.ToString() == "")
//						cmd.Parameters.Add("@HDLDTGian", SqlDbType.NVarChar, 100).Value =  DBNull.Value;
//					else
//						cmd.Parameters.Add("@HDLDTGian", SqlDbType.NVarChar, 100).Value =  strHDLDTGian.ToString();
//
//					cmd.Parameters.Add("@DangVien", SqlDbType.Bit).Value =  blnDangVien;
//
//					if (strDiachithuongtru.ToString() == "")
//						cmd.Parameters.Add("@Diachithuongtru", SqlDbType.NVarChar, 150).Value =  DBNull.Value;
//					else
//						cmd.Parameters.Add("@Diachithuongtru", SqlDbType.NVarChar, 150).Value =  strDiachithuongtru.ToString();
//
//					if (strDiachihientai.ToString() == "")
//						cmd.Parameters.Add("@Diachihientai", SqlDbType.NVarChar, 150).Value =  DBNull.Value;
//					else
//						cmd.Parameters.Add("@Diachihientai", SqlDbType.NVarChar, 150).Value =  strDiachihientai.ToString();
//
//					cmd.Parameters.Add("@Result", SqlDbType.NVarChar,2000).Value = "";
//					cmd.Parameters[cmd.Parameters.Count-1].Direction= ParameterDirection.InputOutput;
//					cmd.ExecuteNonQuery();
//
//					if (cmd.Parameters[cmd.Parameters.Count-1].Value.ToString().Trim() != "")				
//					{
//						myCommand.Dispose();
//						myDataSet.Dispose();
//						this.lblError.Text= this.lblError.Text + cmd.Parameters[cmd.Parameters.Count-1].Value.ToString().Trim() + ' '+ (i+2) + ',';
//					}
//				}// for
//				
//				if (clsCommon.SQLTransaction!= null ) clsCommon.SQLTransaction.Commit();
//				myCommand.Dispose();
//				myDataSet.Dispose();					
//				
//				return true;
//			}
//			catch(Exception exp)
//			{
//				this.lblError.Text= exp.StackTrace;// exp.Message;
//				this.lblRowError.Text = Session["ID_Error"].ToString().Trim();
//				if (clsCommon.SQLTransaction != null) clsCommon.SQLTransaction.Rollback();
//				myCommand.Dispose();
//				myDataSet.Dispose();
//				return false;
//			}
//		}
		/*
		private bool ImportSalary()
		{
			string strConn;
			strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtFile.Value.Trim() + ";Extended Properties=Excel 8.0;";			
			OleDbDataAdapter myCommand = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", strConn);
			DataSet myDataSet = new DataSet();
			myCommand.Fill(myDataSet, "ExcelData");
			DataGrid1.DataSource = myDataSet.Tables["ExcelData"];
			DataGrid1.DataBind();
			SqlCommand  cmd = new SqlCommand();
			clsCommon.SQLTransaction = clsCommon.SQLConnection().BeginTransaction();
			cmd.Connection = clsCommon.SQLConnection();
			cmd.Transaction= clsCommon.SQLTransaction;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "HR_spfrmSalary";
			try
			{
				for(int i=0;i<myDataSet.Tables["ExcelData"].Rows.Count;i++)
				{
					cmd.Parameters.Clear();
					cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 50).Value = "exportdata";
					cmd.Parameters.Add("@EmpID", SqlDbType.VarChar, 6).Value = myDataSet.Tables["ExcelData"].Rows[i][0].ToString().Trim();
					cmd.Parameters.Add("@BasicSalary", SqlDbType.Money).Value = myDataSet.Tables["ExcelData"].Rows[i][1].ToString().Trim();
					cmd.Parameters.Add("@Is_Dec", SqlDbType.Bit ).Value = 0;
					cmd.Parameters.Add("@Creater", SqlDbType.NVarChar,80).Value = Session["AccountLogin"];
					cmd.Parameters.Add("@Result", SqlDbType.NVarChar,2000).Value = "";
					cmd.Parameters[cmd.Parameters.Count-1].Direction= ParameterDirection.InputOutput;
					cmd.ExecuteNonQuery();

					if (cmd.Parameters[cmd.Parameters.Count-1].Value.ToString().Trim() != "")				
					{
						myCommand.Dispose();
						myDataSet.Dispose();
						this.lblError.Text= cmd.Parameters[cmd.Parameters.Count-1].Value.ToString().Trim();
						if (clsCommon.SQLTransaction!= null ) clsCommon.SQLTransaction.Rollback();	
						return false;
					}
				}
				if (clsCommon.SQLTransaction!= null ) clsCommon.SQLTransaction.Commit();
				myCommand.Dispose();				
				myDataSet.Dispose();

				return true;
			}
			catch(Exception exp)
			{
				this.lblError.Text= exp.Message;
				if (clsCommon.SQLTransaction != null) clsCommon.SQLTransaction.Rollback();
				myCommand.Dispose();
				myDataSet.Dispose();
				return false;
			}
		}*/

//		private void cmdAddNew_Click(object sender, System.EventArgs e)
//		{
//			ClearControls();
//		}
//		/// <summary>
//		/// Copy file len server
//		/// </summary>
//		/// <returns></returns>
//		private bool CopyFile_ToServer()
//		{
//			string strFiletmp="";
//			try 
//			{
//				if (this.txtFile.Value != "")				
//				{
//					strFiletmp = this.txtFile.Value;					
//					strFiletmp = Path.GetFileName(strFiletmp);
//
//					this.txtFile.PostedFile.SaveAs(ConfigurationSettings.AppSettings["pstrImportFileFolder"].Trim() + strFiletmp);
//					System.IO.File.SetAttributes(ConfigurationSettings.AppSettings["pstrImportFileFolder"].Trim() + strFiletmp, System.IO.FileAttributes.Normal) ;
//				}
//				else
//				{
//					lblError.Text = "Please enter the path of filename!";
//					return false;
//				}
//				return true;
//			}
//			catch (Exception exp)
//			{
//				this.lblError.Text=exp.Message;
//				return false;
//			}
//		}
		#endregion
	}
}
