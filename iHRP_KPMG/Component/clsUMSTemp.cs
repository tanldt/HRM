using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.SessionState;
using iHRPCore.Com;
using System.Collections;
using System.ComponentModel;

namespace iHRPCore.UMSComponent
{
	/// <summary>
	/// Summary description for clsUMS.
	/// </summary>
	public class clsUMS
	{
		//Phan quyen
		public static bool blnFRun = false;//Quyen xem
		public static bool blnFAdd = false;//Quyen them moi
		public static bool blnFEdit = false;//Quyen sua doi
		public static bool blnFDel = false;//Quyen xoa
		public static bool blnFApp = false;//Quyen chap thuan
		public static bool blnFAdm = false;//Quyen admin
		public static bool blnFValid = false;//Quyen thuc thi
		public static bool blnFSpv = false;//Quyen quan ly
		public static bool blnRightModule = false;//Quyen admin tren module
		public static string pstrModuleID = "";//ma phan he
		public static string pstrFunctionID = "";//ma chuc nang
		/// <summary>
		/// Lay thong tin cua account login
		/// </summary>
		/// <param name="strAccount">Account login</param>
		/// <returns>Ma nhan vien</returns>
		public static string GetEmpIDByAccount(string strAccount)
		{
			DataTable dtb = clsCommon.GetDataTable("UMS_spfrmUserAccount 'Getdata_byID', @UserGroupID='" + strAccount + "'");
			if (dtb.Rows.Count > 0)
				return dtb.Rows[0]["EmpID"].ToString().Trim();
			else 
				return null;
		}

		/// <summary>
		/// Creater: LANHTD, CreateDate: 22/04/2005
		/// Des: Kiem tra nguoi dung login
		/// </summary>
		/// <param name="page"></param>
		public static void CheckLogined(System.Web.UI.Page page, string strExactlyUrl)
		{
			if (page.Session["AccountLogin"] == null)
			{
				page.Session.RemoveAll();
				if (strExactlyUrl == "1")
					page.Response.Redirect("Home.aspx?Url=" + page.Request.Url);
				else
					page.Response.Redirect("Home.aspx");
			}
		}

		public static bool Decentralize(System.Web.UI.Page page, string strFormID)
		{
			try
			{
				string strUserName = page.Session["AccountLogin"].ToString().Trim();
				GetPermEmpFunction(strUserName,strFormID);
				if (!blnFRun)
					page.Response.Redirect("Home.aspx");
				foreach(Control ctrl in page.Controls)
				{
					GetStatusOfControl(ctrl,blnFAdd);
				}
				return true;
			}
			catch(Exception exp)
			{
				string strError = exp.Message.ToString();
				return false;
			}
		}

		public static bool Decentralize(System.Web.UI.Page page, System.Web.UI.WebControls.PlaceHolder AscxHolder, string strFormID)
		{
			try
			{
				string strUserName = page.Session["AccountLogin"].ToString().Trim();
				GetPermEmpFunction(strUserName,strFormID);
				if (!blnFRun)
					page.Response.Redirect("ErrorPage.aspx");
				foreach(Control ctrl in AscxHolder.Controls)
				{
					GetStatusOfControl(ctrl,blnFAdd);
				}
				return true;
			}
			catch(SqlException exp)
			{
				string strError = exp.Message.ToString();
				return false;
			}
		}

		public static bool DecentralizePopUp(System.Web.UI.Page page, System.Web.UI.WebControls.PlaceHolder AscxHolder, string strFormID)
		{
			try
			{
				string strUserName = page.Session["AccountLogin"].ToString().Trim();
				GetPermEmpFunctionPopUp(strUserName,strFormID);
				if (!blnFRun)
					page.Response.Redirect("ErrorPage.aspx");

				foreach(Control ctrl in AscxHolder.Controls)
				{
					GetStatusOfControl(ctrl,blnFAdd);
				}

				return true;
			}
			catch(SqlException exp)
			{
				string strError = exp.Message.ToString();
				return false;
			}
		}


		/// <summary>
		/// Creater: LANHTD
		/// Perpose: Lay cac quyen tren chuc nang
		/// </summary>
		/// <param name="strUserName">Ten dang nhap</param>
		/// <param name="intFuncID">Ma chuc nang</param>
		public static void GetPermEmpFunction(string strUserName, string strFormID)
		{
			blnFRun = false;
			blnFAdd = false;
			blnFEdit = false;
			blnFDel = false;
			blnFApp = false;
			if (blnFAdm || GetModuleRight(strUserName))
			{
				blnFRun = true;
				blnFAdd = true;
				blnFEdit = true;
				blnFDel = true;
				blnFApp = true;
			}
			else
			{
				#region tanldt - update system
				//tanldt fix cache cho nay
				//DataTable tbl = clsCommon.GetDataTable("sp_UMS_UserPerm 'GetUPermOnFunction',@UserGroupID='" + strUserName 
				//	+ "',@FormID='" + strFormID + "',@ModuleID='" + pstrModuleID + "'");
				DataTable tbl = new DataTable();
				
				if (DataCache.GetCache("UserPerm_" + strUserName) == null)
				{
					tbl = clsCommon.GetDataTable("sp_UMS_UserPerm 'GetUPermOnFunction',@UserGroupID='" + strUserName + "'");
					DataCache.SetCache("UserPerm_" + strUserName, tbl);
				}
				tbl = (DataTable)DataCache.GetCache("UserPerm_" + strUserName);
				
				//tbl = clsCommon.GetDataTable("sp_UMS_UserPerm 'GetUPermOnFunction',@UserGroupID='" + strUserName + "'");

				foreach(DataRow oDataRow in tbl.Rows)	
				{
					if (oDataRow["FormID"].ToString().IndexOf(strFormID) > -1)	
					{
						if (oDataRow["ModuleID"].ToString() == pstrModuleID && oDataRow["FunctionID"].ToString() == pstrFunctionID)
						{
							blnFRun = Convert.ToBoolean(oDataRow["FRun"]);
							blnFAdd = Convert.ToBoolean(oDataRow["FAdd"]);
							blnFEdit = Convert.ToBoolean(oDataRow["FEdit"]);
							blnFDel = Convert.ToBoolean(oDataRow["FDel"]);
							blnFApp = Convert.ToBoolean(oDataRow["FApp"]);
						}
					}
				}
				#endregion
//				DataTable tbl = clsCommon.GetDataTable("sp_UMS_UserPerm 'GetUPermOnFunction',@UserGroupID='" + strUserName 
//					+ "',@FormID='" + strFormID + "',@ModuleID='" + pstrModuleID + "'");
//				if (tbl.Rows.Count > 0)
//				{
//					blnFRun = Convert.ToBoolean(tbl.Rows[0]["FRun"]);
//					blnFAdd = Convert.ToBoolean(tbl.Rows[0]["FAdd"]);
//					blnFEdit = Convert.ToBoolean(tbl.Rows[0]["FEdit"]);
//					blnFDel = Convert.ToBoolean(tbl.Rows[0]["FDel"]);
//					blnFApp = Convert.ToBoolean(tbl.Rows[0]["FApp"]);
//				}
			}
		}

		/// <summary>
		/// Creater: LANHTD
		/// Perpose: Lay cac quyen tren chuc nang
		/// </summary>
		/// <param name="strUserName">Ten dang nhap</param>
		/// <param name="strFormID">Ascx chuc nang</param>
		public static void GetPermEmpFunctionPopUp(string strUserName, string strFormID)
		{
			//Mac dinh co quyen tren form popup
			blnFRun = true;
			blnFAdd = true;
			blnFEdit = true;
			blnFDel = true;
			blnFApp = true;
			#region tanldt - update system
			//tanldt fix cache cho nay
			//DataTable tbl = clsCommon.GetDataTable("sp_UMS_UserPerm 'GetUPermOnFunction',@UserGroupID='" + strUserName 
			//	+ "',@FormID='" + strFormID + "',@ModuleID='" + pstrModuleID + "'");

			DataTable tbl = new DataTable();
			if (DataCache.GetCache("UserPermPopup_" + strUserName) == null)
			{
				tbl = clsCommon.GetDataTable("sp_UMS_UserPerm 'GetUPermOnFunctionPopup',@UserGroupID='" + strUserName + "'");
				DataCache.SetCache("UserPermPopup_" + strUserName, tbl);
			}
			tbl = (DataTable)DataCache.GetCache("UserPermPopup_" + strUserName);

			foreach(DataRow oDataRow in tbl.Rows)	
			{
				if (oDataRow["FormID"].ToString().IndexOf(strFormID) > -1)	
				{
					if (oDataRow["ModuleID"].ToString() == pstrModuleID)
					{
						blnFRun = Convert.ToBoolean(oDataRow["FRun"]);
						blnFAdd = Convert.ToBoolean(oDataRow["FAdd"]);
						blnFEdit = Convert.ToBoolean(oDataRow["FEdit"]);
						blnFDel = Convert.ToBoolean(oDataRow["FDel"]);
						blnFApp = Convert.ToBoolean(oDataRow["FApp"]);
					}
				}
			}
			#endregion
//			DataTable tbl = clsCommon.GetDataTable("sp_UMS_UserPerm 'GetUPermOnFunctionPopup',@UserGroupID='" + strUserName 
//				+ "',@FormID='" + strFormID + "',@ModuleID='" + pstrModuleID + "'");
//			if (tbl.Rows.Count > 0)
//			{
//				blnFRun = Convert.ToBoolean(tbl.Rows[0]["FRun"]);
//				blnFAdd = Convert.ToBoolean(tbl.Rows[0]["FAdd"]);
//				blnFEdit = Convert.ToBoolean(tbl.Rows[0]["FEdit"]);
//				blnFDel = Convert.ToBoolean(tbl.Rows[0]["FDel"]);
//				blnFApp = Convert.ToBoolean(tbl.Rows[0]["FApp"]);
//			}
		}

		public static bool GetModuleRight(string strUserName)
		{
			blnRightModule = false;
			DataTable tbl = clsCommon.GetDataTable("sp_UMS_UserPerm 'GetModulePerm',@UserGroupID='" + strUserName + "',@ModuleID='" + pstrModuleID + "'");
			if (tbl.Rows.Count > 0)
				blnRightModule = true;
			return blnRightModule;
		}

		public static void GetStatusOfControl(Control pctrl, bool status)
		{
			foreach (Control child_ctrl in pctrl.Controls)
			{
				if (child_ctrl.HasControls() == true)
				{
					GetStatusOfControl(child_ctrl,status);
				}
				else
				{
					string strCtrlType = child_ctrl.GetType().ToString().Trim();					
					if (strCtrlType == "System.Web.UI.WebControls.Label")
					{
						if (((Label)child_ctrl).CssClass.ToUpper()=="LABELREQUIRE")
						{
							if (((Label)child_ctrl).Text.Substring(0,3).ToString()!="<fo")
								((Label)child_ctrl).Text = @"<font color='#FF0000'>(*)</font>" + ((Label)child_ctrl).Text ;
						}
					}
					
					if (strCtrlType=="System.Web.UI.WebControls.TextBox")
					{
						if(((TextBox)child_ctrl).ID.IndexOf("date")>0 || ((TextBox)child_ctrl).ID.IndexOf("Date")>0)
						{
							((TextBox)child_ctrl).MaxLength=12;
						}
					}
					if (strCtrlType == "System.Web.UI.WebControls.Button" )
						((Button)child_ctrl).Enabled = status;
					else if (strCtrlType == "System.Web.UI.WebControls.ImageButton")
						((ImageButton)child_ctrl).Visible = status;
					else if (strCtrlType == "System.Web.UI.HtmlControls.HtmlInputButton")
						((HtmlInputButton)child_ctrl).Disabled = !status;
					else if (strCtrlType == "System.Web.UI.HtmlControls.HtmlInputButton")
						((HtmlInputButton)child_ctrl).Disabled = !status;
					/*else if (strCtrlType == "System.Web.UI.WebControls.TextBox")
						((TextBox)child_ctrl).Style.Add("position", "absolute");						*/
					else if (strCtrlType == "System.Web.UI.WebControls.LinkButton")
					{
						((LinkButton)child_ctrl).Enabled = status;
						if (((LinkButton)child_ctrl).ID.Trim() == "btnSearch")
							((LinkButton)child_ctrl).Enabled = true;
						if (((LinkButton)child_ctrl).ID.Trim() == "btnView")
							((LinkButton)child_ctrl).Enabled = true;
						if (((LinkButton)child_ctrl).ID.Trim() == "btnList")
							((LinkButton)child_ctrl).Enabled = true;
						if (((LinkButton)child_ctrl).ID.Trim() == "btnPreview")
							((LinkButton)child_ctrl).Enabled = true;
						if (((LinkButton)child_ctrl).ID.Trim() == "btnExport")
							((LinkButton)child_ctrl).Enabled = true;
						//((LinkButton)child_ctrl).Attributes.Add("OnClick","return GetStatusOfControl('" + status + "')");
						((LinkButton)child_ctrl).Attributes.Add("OnClick","return GetStatusOfControl('" + ((LinkButton)child_ctrl).Enabled + "')");
					}
					/*else if (strCtrlType == "System.Web.UI.WebControls.DataGrid")
					{
						System.Web.UI.WebControls.DataGrid dtgGrid = (DataGrid) child_ctrl;
						for(int i=0; i<dtgGrid.Items.Count; i++)
						{
							if (dtgGrid.Items[i].HasControls() == true)
							{
								for (int j=0; j<dtgGrid.Items[i].Controls.Count; j ++)
								{
									GetStatusOfControl(dtgGrid.Items[i].Controls[i],status);
								}
							}
						}
					}*/
				}
			}
		}


		/// <summary>
		/// Creater: LANHTD, CreateDate: 06/06/2005
		/// Des: Kiem tra Username, password
		/// </summary>
		/// <param name="page"></param>
		public static int CheckAccountLogin(string strUserName, string strPassword,Page pPage)
		{
			DataTable dtb = clsCommon.GetDataTable("UMS_spfrmUserAccount 'CheckAccountLogin', @UserGroupID='" + strUserName
				+ "',@Password='" + strPassword + "'");
			int CheckLogined = 0;
			if (dtb.Rows.Count == 1)
			{
				blnFAdm = Convert.ToBoolean(dtb.Rows[0]["FAdm"]);
				blnFValid = Convert.ToBoolean(dtb.Rows[0]["FValid"]);
				blnFSpv = Convert.ToBoolean(dtb.Rows[0]["FSpv"]);
				//cangtt//////get session admin//////////////////
				pPage.Session["bAdm"] = Convert.ToBoolean(dtb.Rows[0]["FAdm"]);

				////////////////////////////////////////////////
				if (!blnFValid) CheckLogined = 0;
				else CheckLogined = 1;
			}
			else
				CheckLogined = 0;
			dtb.Dispose();
			return CheckLogined;
		}
	}
}
