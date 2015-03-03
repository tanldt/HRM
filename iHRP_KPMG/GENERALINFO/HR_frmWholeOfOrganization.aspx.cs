using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using HRWebServiceC.BusinessServices;
using HRWebServiceC.DataServices;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.IO;
using System.Text;

namespace HRWebServiceC.UserServices.GeneralInfo
{
	/// <summary>
	/// Summary description for HR_frmWholeOfOrganization.
	/// </summary>
	public class HR_frmWholeOfOrganization : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.WebControls.Label myTree;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (Convert.ToInt16(Session["CheckAccountLogin"]) == 1)
			{
				if (!Page.IsPostBack)
				{
					XmlDocument docLevels = MakeLevelsXML("");
					
					XslTransform treeView = new XslTransform();
					treeView.Load(Server.MapPath("XsltFile/treeview.xslt"));
					StringWriter sw = new StringWriter();
					treeView.Transform(docLevels,null, sw);
					
					string result = sw.ToString();
					result = result.Replace("xmlns:asp=\"remove\"", "");
  
					Control ctrl = Page.ParseControl(result);
					myTree.Controls.Add(ctrl);
				}
			}
			else
			{
				Session.RemoveAll();
				Response.Redirect("HR_frmBlank.aspx");
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private XmlNode MakeNode(XmlDocument doc,string peleName,string pTitle, string pLevelMa, string pID)
		{
			string pImg = "EmployeeList.jpg";
			switch(pLevelMa)
			{
				case "1":
					pImg = "HumanResourceSupplierList_a.jpg";
					break;
				case "2":
					pImg = "Members_a.jpg";
					break;
				case "3":
					pImg = "EmployeeList_a.jpg";
					break;
				case "4":
					pImg = "EmployeeList_a.jpg";
					break;
			}
			XmlNode objNode = doc.CreateElement(peleName);
			XmlAttribute objAttr = doc.CreateAttribute("title");
			objAttr.InnerText = pTitle;
			objNode.Attributes.SetNamedItem(objAttr);
			objAttr = doc.CreateAttribute("img");
			objAttr.InnerText = pImg;
			objNode.Attributes.SetNamedItem(objAttr);
			objAttr = doc.CreateAttribute("ID");
			objAttr.InnerText = pID;
			objNode.Attributes.SetNamedItem(objAttr);
			return objNode;
		}

		/// <summary>
		/// Creater: LANHTD
		/// Des: Tao file xml subordinate theo nhân viên
		/// </summary>
		/// <param name="mEmpID">Mã nhân viên</param>
		private XmlDocument MakeLevelsXML(string mEmpID)
		{
			try
			{
				string mType = "folder";//Neu phan tu co phan tu con: mType="folder", khong co pt con: mType="leaf"
				string mSql = "sp_HR_tblCV 'GetAll',@result=''";
				DataTable mTableEmpList = HR_clsCommon.GetDataTable(mSql);
				mSql = "sp_HR_tblCV 'MakeTreeView',@result='',@EmployeeID='" + mEmpID + "'";
				DataTable mTable = HR_clsCommon.GetDataTable(mSql);
				XmlDocument doc = new XmlDocument();
				
				//Tao phan tu goc
				XmlNode objNode = doc.CreateElement("treeview");
				XmlAttribute objAttr = doc.CreateAttribute("title");
				objAttr.InnerText = "Sample tree";
				objNode.Attributes.SetNamedItem(objAttr);
				doc.AppendChild(objNode);
				
				//Tạo cấp 0: 
				XmlDocumentFragment objFrag = doc.CreateDocumentFragment();
				//Định nghĩa các tham số dùng chung
				//Tham so
				objNode = doc.CreateElement("custom-parameters");
				//level 1
				objFrag.AppendChild(objNode);
				if (mTable.Rows.Count > 0)
				{
					if (mEmpID != "")
					{
						objNode = MakeNode(doc,"folder",mTable.Rows[0]["EMPLOYEENAME"].ToString(),mTable.Rows[0]["LevelMa"].ToString(),mTable.Rows[0]["EMPLOYEEID"].ToString());
						objFrag.AppendChild(objNode);
					}
					else
					{
						for (int i=0; i<mTable.Rows.Count; i++)
						{
							objNode = MakeNode(doc,"folder",mTable.Rows[i]["EMPLOYEENAME"].ToString(),mTable.Rows[i]["LevelMa"].ToString(),mTable.Rows[i]["EMPLOYEEID"].ToString());
							objFrag.AppendChild(objNode);
						}
					}
					doc.DocumentElement.AppendChild(objFrag);
				}
				//Các tham số
				objFrag = doc.CreateDocumentFragment();
				objNode = doc.CreateElement("param");
				objAttr = doc.CreateAttribute("name");
				objAttr.InnerText = "shift-width";
				objNode.Attributes.SetNamedItem(objAttr);
				objAttr = doc.CreateAttribute("value");
				objAttr.InnerText = "20";
				objNode.Attributes.SetNamedItem(objAttr);
				objFrag.AppendChild(objNode);

				objNode = doc.CreateElement("param");
				objAttr = doc.CreateAttribute("name");
				objAttr.InnerText = "img-directory";
				objNode.Attributes.SetNamedItem(objAttr);
				objAttr = doc.CreateAttribute("value");
				objAttr.InnerText = "Images/";
				objNode.Attributes.SetNamedItem(objAttr);
				objFrag.AppendChild(objNode);
				doc.ChildNodes.Item(0).ChildNodes.Item(0).AppendChild(objFrag);
				//Level 2
				int CountLevel1 = doc.DocumentElement.ChildNodes.Count - 1;
				for (int i=1; i<= CountLevel1; i++)
				{
					string mEmpID1 = doc.DocumentElement.ChildNodes.Item(i).Attributes.Item(2).InnerText.ToString();
					objFrag = doc.CreateDocumentFragment();
					mSql = "sp_HR_tblCV 'MakeTreeViewSub',@result='',@EmployeeID='" + mEmpID1 + "'";
					mTable = HR_clsCommon.GetDataTable(mSql);
					if (mTable.Rows.Count > 0)
					{
						for (int j=0; j<mTable.Rows.Count; j++)
						{
							string mEmpID2 = mTable.Rows[j]["EMPLOYEEID"].ToString();
							DataRow[] child = mTableEmpList.Select("MANAGEMENT='" + mEmpID2 + "'");
							if (child.Length > 0)
								mType = "folder";
							else
								mType = "leaf";
							objNode = MakeNode(doc,mType,mTable.Rows[j]["EMPLOYEENAME"].ToString(),mTable.Rows[j]["LevelMa"].ToString(),mTable.Rows[j]["EMPLOYEEID"].ToString());
							objFrag.AppendChild(objNode);
						}
					}
					doc.DocumentElement.ChildNodes.Item(i).AppendChild(objFrag);
				}
				//Level 3
				for (int i=1; i<= CountLevel1; i++)
				{
					int CountLevel2 = doc.DocumentElement.ChildNodes.Item(i).ChildNodes.Count - 1;
					for (int j=0; j<= CountLevel2; j++)
					{
						string mEmpID2 = doc.DocumentElement.ChildNodes.Item(i).ChildNodes.Item(j).Attributes.Item(2).InnerText.ToString();
						objFrag = doc.CreateDocumentFragment();
						mSql = "sp_HR_tblCV 'MakeTreeViewSub',@result='',@EmployeeID='" + mEmpID2 + "'";
						mTable = HR_clsCommon.GetDataTable(mSql);
						if (mTable.Rows.Count > 0)
						{
							for (int k=0; k<mTable.Rows.Count; k++)
							{
								string mEmpID3 = mTable.Rows[k]["EMPLOYEEID"].ToString();
								DataRow[] child = mTableEmpList.Select("MANAGEMENT='" + mEmpID3 + "'");
								if (child.Length > 0)
									mType = "folder";
								else
									mType = "leaf";
								objNode = MakeNode(doc,mType,mTable.Rows[k]["EMPLOYEENAME"].ToString(),mTable.Rows[k]["LevelMa"].ToString(),mTable.Rows[k]["EMPLOYEEID"].ToString());
								objFrag.AppendChild(objNode);
							}
						}
						doc.DocumentElement.ChildNodes.Item(i).ChildNodes.Item(j).AppendChild(objFrag);
					}
				}
				//Level 4
				for (int i=1; i<= CountLevel1; i++)
				{
					int CountLevel2 = doc.DocumentElement.ChildNodes.Item(i).ChildNodes.Count - 1;
					for (int j=0; j<= CountLevel2; j++)
					{
						int CountLevel3 = doc.DocumentElement.ChildNodes.Item(i).ChildNodes.Item(j).ChildNodes.Count - 1;
						for (int k=0; k<= CountLevel3; k++)
						{
							string mEmpID3 = doc.DocumentElement.ChildNodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(k).Attributes.Item(2).InnerText.ToString();
							objFrag = doc.CreateDocumentFragment();
							mSql = "sp_HR_tblCV 'MakeTreeViewSub',@result='',@EmployeeID='" + mEmpID3 + "'";
							mTable = HR_clsCommon.GetDataTable(mSql);
							if (mTable.Rows.Count > 0)
							{
								for (int l=0; l<mTable.Rows.Count; l++)
								{
									objNode = MakeNode(doc,"leaf",mTable.Rows[l]["EMPLOYEENAME"].ToString(),mTable.Rows[l]["LevelMa"].ToString(),mTable.Rows[l]["EMPLOYEEID"].ToString());
									objFrag.AppendChild(objNode);
								}
							}
							doc.DocumentElement.ChildNodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(k).AppendChild(objFrag);
						}
					}
				}
				/*XmlTextWriter writer = new XmlTextWriter("D://Sharefull/xxx.xml",null);
				writer.Formatting = Formatting.None;
				doc.Save(writer);*/
				return doc;
			}
			catch(Exception ex)
			{
				Response.Write(ex.Message.ToString());
				XmlDocument doc = new XmlDocument();;
				return doc;
			}
		}
	}
}
