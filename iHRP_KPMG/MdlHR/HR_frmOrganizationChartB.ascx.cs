
 namespace iHRPCore.MdlHR
{
	 using System;
	 using System.Collections;
	 using System.ComponentModel;
	 using System.Data;
	 using System.Data.SqlClient;
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
	 using iHRPCore.Com;
	 using iHRPCore.HRComponent;
	 using iHRPCore.TMSComponent; 

	 /// <summary>
	/// Summary description for HR_frmOrganizationChart.
	/// </summary>
	public class HR_frmOrganizationChartB : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label myTree;
		protected System.Web.UI.WebControls.Label lblErr;
		public string strLanguage = "VN";
		private void Page_Load(object sender, System.EventArgs e)
		{
			//if (Convert.ToInt16(Session["CheckAccountLogin"]) == 1)
			//{
				/*if (!Page.IsPostBack)
				{*/
			//Session["CurFunction"] = Request.Params["FunctionID"].Trim();
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"VN";

			XmlDocument docLevels = MakeLevelsXML("");
			
			XslTransform treeView = new XslTransform();
			treeView.Load(Server.MapPath("GeneralInfo/XsltFile/treeview1B.xslt"));
			StringWriter sw = new StringWriter();
			treeView.Transform(docLevels,null, sw);
			
			string result = sw.ToString();
			result = result.Replace("xmlns:asp=\"remove\"", "");
			string strApp = Request.ApplicationPath.Trim();
			result = result.Replace("@@@",strApp);
			result = result.Replace("@@","&");

			Control ctrl = Page.ParseControl(result);
			myTree.Controls.Add(ctrl);
				//}
			//}
			//else
			/*{
				Session.RemoveAll();
				Response.Redirect("HR_frmBlank.aspx");
			}
			*/
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

		private XmlNode MakeNode(XmlDocument doc,string peleName,string pTitle, string pLevel, string pID)
		{
			string pImg = "EmployeeList_a.jpg";
			switch(pLevel)
			{
				case "1":
					pImg = "EmployeeList.jpg";
					break;
				case "2":
					pImg = "HumanResourceSupplierList.jpg";
					break;
				case "3":
					pImg = "HumanResourceSupplierList.jpg";
					break;
				case "4":
					pImg = "HumanResourceSupplierList.jpg";
					break;
				case "0":
					pImg = "Members_a.jpg";
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
				string mSql = "sp_HR_frmOrganizationChartB 1";
				//DataTable mTable = HR_clsCommon.GetDataTable(mSql);
				DataTable mTable = clsCommon.GetDataTable(mSql);
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
					for (int i=0; i<mTable.Rows.Count; i++)
					{
						objNode = MakeNode(doc,"folder",mTable.Rows[i]["Name"].ToString(),mTable.Rows[i]["status"].ToString(),mTable.Rows[i]["ID"].ToString());
						objFrag.AppendChild(objNode);
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
				objAttr.InnerText = "GeneralInfo/Images/";
				objNode.Attributes.SetNamedItem(objAttr);
				objFrag.AppendChild(objNode);
				doc.ChildNodes.Item(0).ChildNodes.Item(0).AppendChild(objFrag);
				//Level 2
				int CountLevel1 = doc.DocumentElement.ChildNodes.Count - 1;
				for (int i=1; i<= CountLevel1; i++)
				{
					string strID1 = doc.DocumentElement.ChildNodes.Item(i).Attributes.Item(2).InnerText.ToString();
					objFrag = doc.CreateDocumentFragment();
					mSql = "sp_HR_frmOrganizationChartB 2,@Parent='" + strID1 + "'";
					//mTable = HR_clsCommon.GetDataTable(mSql);
					mTable = clsCommon.GetDataTable(mSql);
					if (mTable.Rows.Count > 0)
					{
						for (int j=0; j<mTable.Rows.Count; j++)
						{
							mType = "folder";
							objNode = MakeNode(doc,mType,mTable.Rows[j]["Name"].ToString(),mTable.Rows[j]["status"].ToString(),mTable.Rows[j]["ID"].ToString());
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
						string strID2 = doc.DocumentElement.ChildNodes.Item(i).ChildNodes.Item(j).Attributes.Item(2).InnerText.ToString();
						objFrag = doc.CreateDocumentFragment();
						mSql = "sp_HR_frmOrganizationChartB 3,@Parent='" + strID2 + "'";
						//mTable = HR_clsCommon.GetDataTable(mSql);
						mTable = clsCommon.GetDataTable(mSql);
						if (mTable.Rows.Count > 0)
						{
							for (int k=0; k<mTable.Rows.Count; k++)
							{
								mType = "leaf";
								objNode = MakeNode(doc,mType,mTable.Rows[k]["Name"].ToString(),mTable.Rows[k]["status"].ToString(),mTable.Rows[k]["ID"].ToString());
								objFrag.AppendChild(objNode);
							}
						}
						doc.DocumentElement.ChildNodes.Item(i).ChildNodes.Item(j).AppendChild(objFrag);
					}
				}
/*
				//Level 4
				for (int i=1; i<= CountLevel1; i++)
				{
					int CountLevel2 = doc.DocumentElement.ChildNodes.Item(i).ChildNodes.Count - 1;
					for (int j=0; j<= CountLevel2; j++)
					{
						int CountLevel3 = doc.DocumentElement.ChildNodes.Item(i).ChildNodes.Item(j).ChildNodes.Count - 1;
						string strID3=doc.DocumentElement.ChildNodes.Item(i).ChildNodes.Item(j).Attributes.Item(2).InnerText.ToString();
						for (int k=0; k<= CountLevel3; k++)
						{
							string strID4 = doc.DocumentElement.ChildNodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(k).Attributes.Item(2).InnerText.ToString();
							objFrag = doc.CreateDocumentFragment();
							mSql = "sp_HR_frmOrganizationChartB 4,@Parent='" + strID4 + "',@Parent1='" + strID3 + "'";
							//mTable = HR_clsCommon.GetDataTable(mSql);
							mTable = clsCommon.GetDataTable(mSql);
							if (mTable.Rows.Count > 0)
							{
								for (int l=0; l<mTable.Rows.Count; l++)
								{
									objNode = MakeNode(doc,"leaf",mTable.Rows[l]["Name"].ToString(),mTable.Rows[l]["status"].ToString(),mTable.Rows[l]["ID"].ToString());
									objFrag.AppendChild(objNode);
								}
							}
							doc.DocumentElement.ChildNodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(k).AppendChild(objFrag);
						}
					}
				}
*/
			
				return doc;
			}
			catch(Exception exp)
			{
				Response.Write(exp.Message.ToString());
				XmlDocument doc = new XmlDocument();
				return doc;
			}
		}
	}
}
