using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using iHRPCore;
using iHRPCore.Com;
using FPTToolWeb.Exports;

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.IO;
using System.Text;
using System.Xml;


namespace iHRPCore.Temp
{
	/// <summary>
	/// Summary description for TestReportPaySlip.
	/// </summary>
	public class TestReportPaySlip : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button Button1;
		//Word.Application wrdApp;
		//Word._Document wrdDoc;
		Object oMissing = System.Reflection.Missing.Value;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.Button Button4;
		protected System.Web.UI.WebControls.Button cmdCV;
		protected System.Web.UI.WebControls.TextBox txtResult;
		protected System.Web.UI.WebControls.Button cmdExport;
		protected System.Web.UI.WebControls.Button cmdPDF;
		protected System.Web.UI.WebControls.DataGrid dtgList;
		protected System.Web.UI.WebControls.Button cmdExportdtgPdf;
		protected System.Web.UI.WebControls.Button cmdXml;
		protected System.Web.UI.WebControls.Button cmdTestCV2;
		Object oFalse = false;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
//			SqlDataAdapter DA = new SqlDataAdapter("SELECT top 100 * FROM HR_vEmpList", ConfigurationSettings.AppSettings("ConnString"));
//			DataSet DS = new DataSet();
//			DA.Fill(DS, "HR_vEmpList");
//			// TODO: NotImplemented statement: ICSharpCode.SharpRefactory.Parser.AST.VB.ReDimStatement
			if (!Page.IsPostBack)
			{
				dtgList.DataSource = clsCommon.GetDataTable("SELECT top 100 * FROM HR_vEmpList");
				dtgList.DataBind();
				
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
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.cmdXml.Click += new System.EventHandler(this.cmdXml_Click);
			this.cmdExportdtgPdf.Click += new System.EventHandler(this.cmdExportdtgPdf_Click);
			this.cmdPDF.Click += new System.EventHandler(this.cmdPDF_Click);
			this.cmdExport.Click += new System.EventHandler(this.cmdExport_Click);
			this.Button4.Click += new System.EventHandler(this.Button4_Click);
			this.cmdCV.Click += new System.EventHandler(this.Button3_Click);
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.cmdTestCV2.Click += new System.EventHandler(this.cmdTestCV2_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
			string strFileTemplate = "";
			string strDate = "";
			//string strImage = ConfigurationSettings.AppSettings["pStrimageReport"].Trim();
			//string strImage = "http://localhost/iHRP_SCR/Upload/TemplateReport/Image/image001.jpg";
			DataTable dt = clsCommon.GetDataTable("SELECT top 10 * FROM HR_vEmpList");
			
			strFileTemplate = "test.htm";

			try 
			{
				string strHeaderParams = "";
				string strHeaderValues = "";
				string strFooterParams = "";
				string strFooterValues = "";
				//Phan khai bao se dung Tool bao cao Excel
				#region Header 
				strHeaderParams = "";
				strHeaderValues = "";
				#endregion
				#region Footer
				strDate = DateTime.Now.ToString("dd/MM/yyyy");
				strFooterParams = "Date";					
				strFooterValues = strDate;
				#endregion
				
				FPTToolWeb.Reports.v10.clsBaocao bc = new FPTToolWeb.Reports.v10.clsBaocao();
				#region Config Basic
				bc.sfileTemplate = strFileTemplate;
				string strReports = bc.strReportPageDoc(dt);
				#endregion
				//End
				ExcelExporter myExcelXport=new ExcelExporter(this.Page);
				myExcelXport.ExportHTMLTo(strReports,"word");
				myExcelXport = null;
			}
			catch(Exception ex)
			{										
				txtResult.Text = ex.Message;
			}
			finally
			{	
				dt.Dispose();
			}
			// Create an instance of Word  and make it visible.
//			wrdApp = new Word.Application();
//			wrdApp.Visible = true;
//			Word.Document myMergeDocument;
//
//			string strFileName = "c:\\template.doc";
//			string strDataFile = "c:\\datafile.xls";
//
//			object filename = strFileName;
//			object objTrue = true;
//			object objFalse = false;
//			object objMiss = Type.Missing;
//
//
//			//Open merge document
//			myMergeDocument = wrdApp.Documents.Open(ref filename, ref objMiss, ref objMiss, ref objMiss, ref objMiss, ref objMiss, ref objMiss, ref objMiss, ref objMiss, ref objMiss, ref objMiss, ref objMiss, ref objMiss, ref objMiss, ref objMiss, ref objMiss);
//			myMergeDocument.Select();
//
//           
//
//			//Open the data source
//			object source = strDataFile;
//			object format = Word.WdOpenFormat.wdOpenFormatAuto;
//			myMergeDocument.MailMerge.OpenDataSource(strDataFile, ref format, ref objFalse, ref objMiss, ref objTrue, ref objFalse, ref objMiss, ref objMiss, ref objMiss, ref objMiss, ref objMiss, ref objMiss, ref objMiss, ref objMiss, ref objMiss, ref objMiss);
//
//			//Perform Merge
//			myMergeDocument.MailMerge.Destination = Word.WdMailMergeDestination.wdSendToNewDocument;
//
//           
//			myMergeDocument.MailMerge.SuppressBlankLines = true;
//			myMergeDocument.MailMerge.DataSource.FirstRecord = (int)Word.WdMailMergeDefaultRecord.wdDefaultFirstRecord;
//			myMergeDocument.MailMerge.DataSource.LastRecord = (int)Word.WdMailMergeDefaultRecord.wdDefaultLastRecord;
//			myMergeDocument.MailMerge.Execute(ref objFalse);
//
//			// Close the original form document.
//			myMergeDocument.Saved = true;
//			myMergeDocument.Close(ref oFalse,ref oMissing,ref oMissing);
		}

		private void Button2_Click(object sender, System.EventArgs e)
		{
			DataTable dt= clsCommon.GetDataTable("SELECT top 10 CompanyEN, Level1EN, * FROM HR_vEmpList");

			//Phan khai bao se dung Tool bao cao Excel
			FPTToolWeb.Reports.v10.clsBaocao bc = new FPTToolWeb.Reports.v10.clsBaocao();
			#region Header Company Info
				
			string strHeaderParams = "Title;Date";
			string strHeaderValues = "Test;01/01/2009";

			#endregion
			#region Footer
			string strFooterParams = "CountEmp";
			string strFooterValues ="";
			if (dt != null)
				strFooterValues = dt.Rows.Count.ToString();
			else
				strFooterValues = "0";
			#endregion
			#region Config System
			/*
				 * itempheadlv1: id cho Header Group 1
				 * itempheadlv2: id cho Header Group 2
				 * itempsumlv1: id sum cap 1
				 * itempsumlv2: id sum cap 2
				 * itempsumtotal: id sum tong cong tat ca
				 * */
			#endregion
			#region Config Basic
			bc.IsGroupLv1 = true; //Co Group 1 khong?
			bc.GroupLv1 = 1; //Chỉ định Group level 1 ở cột thứ I
			bc.IsGroupLv2 = true; //Co Group 2 khong?
			bc.GroupLv2 = 2; //Chỉ định Group level 2 ở cột thứ II
			bc.IsSum = true; //Có Sum tong hay không? Nếu có thì phải có id template (itempsumtotal) đi kèm
			bc.IsSum1 = true; //Có Sum cap 1 hay không? Nếu có thì phải có id template (itempsumlv1) đi kèm
			bc.IsSum2 = true; //Có Sum cap 2 hay không? Nếu có thì phải có id template (itempsumlv2) đi kèm
			bc.sfileTemplate = "HR_sprptMale.htm";
			bc.sHeaderParams = strHeaderParams;
			bc.sHeaderValues = strHeaderValues;
			bc.sFooterParams = strFooterParams;
			bc.sFooterValues = strFooterValues;
			
			string strReports = bc.strReportBasic(dt);
			#endregion
			
			ExcelExporter myExcelXport=new ExcelExporter(this.Page);
			myExcelXport.ExportHTMLToExcel(strReports,"Excel");
			myExcelXport = null;
		}

		private void Button3_Click(object sender, System.EventArgs e)
		{
			string strFileTemplate = "";
			string strDate = "";
			//string strImage = ConfigurationSettings.AppSettings["pStrimageReport"].Trim();
			//string strImage = "http://localhost/iHRP_SCR/Upload/TemplateReport/Image/image001.jpg";
			DataTable dt = clsCommon.GetDataTable("SELECT top 10 * FROM HR_vEmpList");
			
			strFileTemplate = "testCV.htm";

			try 
			{
				string strHeaderParams = "";
				string strHeaderValues = "";
				string strFooterParams = "";
				string strFooterValues = "";
				//Phan khai bao se dung Tool bao cao Excel
				#region Header 
				strHeaderParams = "";
				strHeaderValues = "";
				#endregion
				#region Footer
				strDate = DateTime.Now.ToString("dd/MM/yyyy");
				strFooterParams = "Date";					
				strFooterValues = strDate;
				#endregion
				
				FPTToolWeb.Reports.v10.clsBaocao bc = new FPTToolWeb.Reports.v10.clsBaocao();
				#region Config Basic
				bc.sfileTemplate = strFileTemplate;
				#region Sub report
				bc.dtSubReport1 = clsDB.GetDataTable("select EmpID, FirstName + ' ' + LastName as [FullName] from HR_tblRelative");
				bc.sItemLink1 = "EmpID";
				
//              bc.dtSubReport2 = clsDB.GetDataTable("select EmpID, FirstName + ' ' + LastName as [FullName] from HR_tblRelative");
//              bc.sItemLink2 = "EmpID";
//
//				bc.dtSubReport3 = clsDB.GetDataTable("select EmpID, FirstName + ' ' + LastName as [FullName] from HR_tblRelative");
//				bc.sItemLink3 = "EmpID";
//
//				bc.dtSubReport4 = clsDB.GetDataTable("select EmpID, FirstName + ' ' + LastName as [FullName] from HR_tblRelative");
//				bc.sItemLink4 = "EmpID";
//
//				bc.dtSubReport5 = clsDB.GetDataTable("select EmpID, FirstName + ' ' + LastName as [FullName] from HR_tblRelative");
//				bc.sItemLink5 = "EmpID";

//				bc.dtSubReport6 = clsDB.GetDataTable("select EmpID, FirstName + ' ' + LastName as [FullName] from HR_tblRelative");
//				bc.sItemLink6 = "EmpID";
//
//				bc.dtSubReport7 = clsDB.GetDataTable("select EmpID, FirstName + ' ' + LastName as [FullName] from HR_tblRelative");
//				bc.sItemLink7 = "EmpID";
//
//				bc.dtSubReport8 = clsDB.GetDataTable("select EmpID, FirstName + ' ' + LastName as [FullName] from HR_tblRelative");
//				bc.sItemLink8 = "EmpID";
//
//				bc.dtSubReport9 = clsDB.GetDataTable("select EmpID, FirstName + ' ' + LastName as [FullName] from HR_tblRelative");
//				bc.sItemLink9 = "EmpID";
//
//				bc.dtSubReport10 = clsDB.GetDataTable("select EmpID, FirstName + ' ' + LastName as [FullName] from HR_tblRelative");
//				bc.sItemLink10 = "EmpID";
//
//				bc.dtSubReport11 = clsDB.GetDataTable("select EmpID, FirstName + ' ' + LastName as [FullName] from HR_tblRelative");
//				bc.sItemLink11 = "EmpID";
//
//				bc.dtSubReport12 = clsDB.GetDataTable("select EmpID, FirstName + ' ' + LastName as [FullName] from HR_tblRelative");
//				bc.sItemLink12 = "EmpID";
//
//				bc.dtSubReport13 = clsDB.GetDataTable("select EmpID, FirstName + ' ' + LastName as [FullName] from HR_tblRelative");
//				bc.sItemLink13 = "EmpID";
//
//				bc.dtSubReport14 = clsDB.GetDataTable("select EmpID, FirstName + ' ' + LastName as [FullName] from HR_tblRelative");
//				bc.sItemLink14 = "EmpID";
//
//				bc.dtSubReport15 = clsDB.GetDataTable("select EmpID, FirstName + ' ' + LastName as [FullName] from HR_tblRelative");
//				bc.sItemLink15 = "EmpID";
//
//				bc.dtSubReport16 = clsDB.GetDataTable("select EmpID, FirstName + ' ' + LastName as [FullName] from HR_tblRelative");
//				bc.sItemLink16 = "EmpID";
//
//				bc.dtSubReport17 = clsDB.GetDataTable("select EmpID, FirstName + ' ' + LastName as [FullName] from HR_tblRelative");
//				bc.sItemLink17 = "EmpID";
//
//				bc.dtSubReport18 = clsDB.GetDataTable("select EmpID, FirstName + ' ' + LastName as [FullName] from HR_tblRelative");
//				bc.sItemLink18 = "EmpID";
				#endregion
				string strReports = bc.strReportPageDoc(dt);
				#endregion
				//End
				txtResult.Text = strReports;
				ExcelExporter myExcelXport=new ExcelExporter(this.Page);
				myExcelXport.ExportHTMLTo(strReports,"doc");
				myExcelXport = null;
			}
			catch(Exception ex)
			{
				txtResult.Text = ex.Message;
			}
			finally
			{	
				dt.Dispose();
			}
		}

		private void Button4_Click(object sender, System.EventArgs e)
		{
			DataTable dt= clsCommon.GetDataTable("SELECT top 10 CompanyEN, Level1EN, * FROM HR_vEmpList");

			//Phan khai bao se dung Tool bao cao Excel
			FPTToolWeb.Reports.v10.clsBaocao bc = new FPTToolWeb.Reports.v10.clsBaocao();
			#region Header Company Info
				
			string strHeaderParams = "Title";
			string strHeaderValues = "Test";

			#endregion
			#region Footer
			string strFooterParams = "CountEmp";
			string strFooterValues ="";
			if (dt != null)
				strFooterValues = dt.Rows.Count.ToString();
			else
				strFooterValues = "0";
			#endregion
			#region Config System
			/*
				 * itempheadlv1: id cho Header Group 1
				 * itempheadlv2: id cho Header Group 2
				 * itempsumlv1: id sum cap 1
				 * itempsumlv2: id sum cap 2
				 * itempsumtotal: id sum tong cong tat ca
				 * */
			#endregion
			#region Config Basic
			bc.IsGroupLv1 = true; //Co Group 1 khong?
			bc.GroupLv1 = 1; //Chỉ định Group level 1 ở cột thứ I
			bc.IsGroupLv2 = true; //Co Group 2 khong?
			bc.GroupLv2 = 2; //Chỉ định Group level 2 ở cột thứ II
			bc.IsSum = true; //Có Sum tong hay không? Nếu có thì phải có id template (itempsumtotal) đi kèm
			bc.IsSum1 = true; //Có Sum cap 1 hay không? Nếu có thì phải có id template (itempsumlv1) đi kèm
			bc.IsSum2 = true; //Có Sum cap 2 hay không? Nếu có thì phải có id template (itempsumlv2) đi kèm
			bc.sfileTemplate = "HR_sprptMale.htm";
			bc.sHeaderParams = strHeaderParams;
			bc.sHeaderValues = strHeaderValues;
			bc.sFooterParams = strFooterParams;
			bc.sFooterValues = strFooterValues;
			
			string strReports = bc.strReportBasic(dt);
			#endregion
			strReports = strReports + bc.sBreakPage + strReports;
			ExcelExporter myExcelXport=new ExcelExporter(this.Page);
			myExcelXport.ExportHTMLToExcel(strReports,"doc");
			myExcelXport = null;
		}

		private void cmdExport_Click(object sender, System.EventArgs e)
		{
			//txtResult.Text = strReports;
			ExcelExporter myExcelXport=new ExcelExporter(this.Page);
			myExcelXport.ExportHTMLTo(txtResult.Text,"doc");
			myExcelXport = null;

			

		}

		private void cmdPDF_Click(object sender, System.EventArgs e)
		{
			DataTable dt= clsCommon.GetDataTable("SELECT top 10 CompanyEN, Level1EN, * FROM HR_vEmpList");

			//Phan khai bao se dung Tool bao cao Excel
			FPTToolWeb.Reports.v10.clsBaocao bc = new FPTToolWeb.Reports.v10.clsBaocao();
			#region Header Company Info
				
			string strHeaderParams = "Title;Date";
			string strHeaderValues = "Test;01/01/2009";

			#endregion
			#region Footer
			string strFooterParams = "CountEmp";
			string strFooterValues ="";
			if (dt != null)
				strFooterValues = dt.Rows.Count.ToString();
			else
				strFooterValues = "0";
			#endregion
			#region Config System
			/*
				 * itempheadlv1: id cho Header Group 1
				 * itempheadlv2: id cho Header Group 2
				 * itempsumlv1: id sum cap 1
				 * itempsumlv2: id sum cap 2
				 * itempsumtotal: id sum tong cong tat ca
				 * */
			#endregion
			#region Config Basic
			bc.IsGroupLv1 = true; //Co Group 1 khong?
			bc.GroupLv1 = 1; //Chỉ định Group level 1 ở cột thứ I
			bc.IsGroupLv2 = true; //Co Group 2 khong?
			bc.GroupLv2 = 2; //Chỉ định Group level 2 ở cột thứ II
			bc.IsSum = true; //Có Sum tong hay không? Nếu có thì phải có id template (itempsumtotal) đi kèm
			bc.IsSum1 = true; //Có Sum cap 1 hay không? Nếu có thì phải có id template (itempsumlv1) đi kèm
			bc.IsSum2 = true; //Có Sum cap 2 hay không? Nếu có thì phải có id template (itempsumlv2) đi kèm
			bc.sfileTemplate = "HR_sprptMale.htm";
			bc.sHeaderParams = strHeaderParams;
			bc.sHeaderValues = strHeaderValues;
			bc.sFooterParams = strFooterParams;
			bc.sFooterValues = strFooterValues;
			
			string strReports = bc.strReportBasic(dt);
			#endregion
			ConvertHtmlStringToPDF(strReports);
			
		}
		public void ConvertHtmlStringToPDF(string sHTML)
		{
			string htmlDisplayText = sHTML;         
			

			Document document = new Document();          
			MemoryStream ms = new MemoryStream();
			PdfWriter writer = PdfWriter.GetInstance(document, ms);        
			StringReader se = new StringReader(htmlDisplayText);
			HTMLWorker obj = new HTMLWorker(document);
			//BaseFont baseFnt = new BaseFont();

			document.Open();
			
			BaseFont bf = BaseFont.CreateFont("c:\\windows\\fonts\\times.ttf", BaseFont.IDENTITY_H, true);
			//BaseFont bf = BaseFont.CreateFont(BaseFont.COURIER,BaseFont.IDENTITY_H, true);
			iTextSharp.text.Font f2 = new iTextSharp.text.Font(bf);

			BaseFont bf1 = BaseFont.CreateFont("c:\\windows\\fonts\\times.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
			iTextSharp.text.Font f21 = new iTextSharp.text.Font(bf1);

			String text1 = "Lê đào trung Tân";
			String text2 = "Lê đào trung Tân";
			
			document.Add(new Paragraph(text1, f2));
			document.Add(new Paragraph(text2, f21));

			obj.Parse(se);
			// step 5: we close the document
			document.Close();      
			Response.Clear();
			Response.AddHeader("content-disposition", "attachment; filename=report.pdf");
			Response.ContentType = "application/pdf";
			//Response.Charset = "Windows-1252";
			//Encoding enc = Encoding.GetEncoding(1252);

			
			//Response.WriteFile(strS);
			Response.Flush();
			Response.Clear();

			Response.Buffer = true;        
			Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
			Response.OutputStream.Flush();
			Response.End();

		}
	
		
		private void ShowPdf(string strS)
		{
			Response.ClearContent();
			Response.ClearHeaders();
			Response.ContentType = "application/pdf";
			Response.AddHeader
				("Content-Disposition","attachment; filename=" + strS);
			Response.TransmitFile(strS);
			Response.End();
			//Response.WriteFile(strS);
//			Response.Flush();
//			Response.Clear();

		}
		protected void cmdExportdtgPdf1_Click(object sender, EventArgs e)
		{
			HtmlForm form = new HtmlForm();
			form.Controls.Add(dtgList);
			StringWriter sw = new StringWriter();
			HtmlTextWriter hTextWriter = new HtmlTextWriter(sw);
			form.Controls[0].RenderControl(hTextWriter);
			string html = sw.ToString();
			Document Doc = new Document();

			//PdfWriter.GetInstance
			//(Doc, new FileStream(Request.PhysicalApplicationPath 
			//+ "\\AmitJain.pdf", FileMode.Create));

			PdfWriter.GetInstance
				(Doc, new FileStream(Environment.GetFolderPath
				(Environment.SpecialFolder.Desktop)
				+ "\\AmitJain.pdf", FileMode.Create));
			Doc.Open();

			Chunk c = new Chunk
				("Export GridView to PDF Using iTextSharp \n",
				FontFactory.GetFont("Verdana", 15));
			Paragraph p = new Paragraph();
			p.Alignment = Element.ALIGN_CENTER;
			p.Add(c);
			Chunk chunk1 = new Chunk
				("By lê đào trung tân, amit_jain_online@yahoo.com \n",
				FontFactory.GetFont("Verdana", 8));
			Paragraph p1 = new Paragraph();
			p1.Alignment = Element.ALIGN_RIGHT;
			p1.Add(chunk1);

			Doc.Add(p);
			Doc.Add(p1);

			System.Xml.XmlTextReader xmlReader =
				new System.Xml.XmlTextReader(new StringReader(html));
			HtmlParser.Parse(Doc, xmlReader);

			Doc.Close();
			string Path = Environment.GetFolderPath
				(Environment.SpecialFolder.Desktop)
				+ "\\AmitJain.pdf";


			ShowPdf(Path);


		}

		private void cmdExportdtgPdf_Click(object sender, System.EventArgs e)
		{
			Response.Clear();

			StringBuilder sb = new StringBuilder();
			StringWriter sw = new StringWriter(sb);
			HtmlTextWriter htw = new HtmlTextWriter(sw);

			dtgList.RenderControl(htw);

			Response.ContentType = "application/pdf";
			//Response.Charset = "Windows-1250";
			Response.AddHeader("content-disposition", "attachment; filename=" 
				+ HttpUtility.UrlEncode("MypdfFile.pdf", System.Text.Encoding.UTF8));

			Document document = new Document();


			PdfWriter.GetInstance(document, Response.OutputStream);
			document.Open();

			string html = sb.ToString();

			// Get different encodings.
			Encoding  u7    = Encoding.UTF7;
			Encoding  u8    = Encoding.UTF8;
			Encoding  u16LE = Encoding.Unicode;
			Encoding  u16BE = Encoding.BigEndianUnicode;
			Encoding  df   = Encoding.Default;

//			html = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + html;
//			StringReader strreader = new StringReader(html);
//			
//			XmlDocument xmlDoc = new XmlDocument();
//			xmlDoc.Load(strreader);
//			string myPath = "D:\\Projects\\iHRP_KPMG\\WIP\\Program\\iHRP_KPMG\\Temp\\aaa.xml";
//			xmlDoc.Save(myPath);
//

			//XmlTextReader reader = new XmlTextReader(new StringReader(html));
			XmlTextReader reader = new XmlTextReader(new StringReader(html));

			//StreamReader readers = new StreamReader(reader,Encoding.Unicode);
			//reader.Encoding = u8;
			HtmlParser.Parse(document, reader);

			document.Close();
			sw.Close();

			Response.Flush();
			Response.End(); 
		}

		private void cmdExportdtgPdf4_Click(object sender, System.EventArgs e)
		{
			Document document = new Document(PageSize.A4, 80, 50, 30, 65); 
			StringBuilder strData = new StringBuilder(string.Empty); 
			string timestamp = "Test";
			string strHTMLpath ="MyHTML.html"; 
			string strPDFpath = "MyPDF.pdf"; 
			try 
			{ 
				StringWriter sw = new StringWriter(); 
				sw.WriteLine(Environment.NewLine); 
				sw.WriteLine(Environment.NewLine); 
				sw.WriteLine(Environment.NewLine); 
				sw.WriteLine(Environment.NewLine); 
				HtmlTextWriter htw = new HtmlTextWriter(sw); 
				//Rendering the HtmlTextWriter 
				dtgList.RenderControl(htw); 
				StreamWriter strWriter = new StreamWriter(strHTMLpath, false, Encoding.Default); 
				//strWriter.Write("<html><head><link href=Style.css rel=stylesheet type=text/css /></head><body>" + htw.InnerWriter.ToString() + "</body></html>"); 
				strWriter.Close(); 
				//strWriter.Dispose(); 
				iTextSharp.text.html.simpleparser.StyleSheet styles = new iTextSharp.text.html.simpleparser.StyleSheet();            
				PdfWriter.GetInstance(document, new FileStream(strPDFpath, FileMode.Create)); 
				document.Add(new Header(iTextSharp.text.html.Markup.HTML_ATTR_STYLESHEET, "Style.css")); 
				document.Open(); 
				ArrayList objects;      
				document.NewPage(); 
				objects = iTextSharp.text.html.simpleparser.HTMLWorker.ParseToList(new StreamReader(strHTMLpath, Encoding.Default), styles); 
				for (int k = 0; k < objects.Count; k++) 
				{ 
					document.Add((IElement)objects[k]); 
				}            
			} 
			catch (Exception ex) 
			{ 
				throw ex; 
			} 
			finally 
			{ 
				document.Close(); 
				Response.Write(strPDFpath); 
				Response.ClearContent(); 
				Response.ClearHeaders(); 
				Response.AddHeader("Content-Disposition", "attachment; filename=" + timestamp + ".pdf"); 
				Response.ContentType = "application/octet-stream"; 
				Response.WriteFile(strPDFpath); 
				Response.Flush(); 
				Response.Close(); 
				if (File.Exists(strPDFpath)) 
				{ 
					File.Delete(strPDFpath); 
				} 
				if (File.Exists(strHTMLpath)) 
				{ 
					File.Delete(strHTMLpath); 
				} 
				Response.End(); 
			}  

		}

		private void cmdXml_Click(object sender, System.EventArgs e)
		{
			MemoryStream stream = new MemoryStream();
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stream, Encoding.GetEncoding("UTF-8"));
			xmlTextWriter.Formatting = Formatting.Indented;
			xmlTextWriter.WriteStartDocument(); //Start doc


			Encoding encoding = Encoding.GetEncoding("UTF-8");
			string myPath = "D:\\Projects\\iHRP_KPMG\\WIP\\Program\\iHRP_KPMG\\Temp\\aaa.xml";
			FileStream myFile = new FileStream(myPath, FileMode.Create);
			StreamWriter sw = new StreamWriter(myFile, encoding);

			stream.Position = 0;
			StreamReader sr = new StreamReader(stream, encoding);
			string content = sr.ReadToEnd();

			sw.Write(content);
			sw.Flush();

			myFile.Flush();
			myFile.Close();

		}

		private void cmdTestCV2_Click(object sender, System.EventArgs e)
		{
			string strFileTemplate = "";
			string strDate = "";
			
			DataTable dt = clsCommon.GetDataTable("RE_sprptCANDIDATEINFO @Activity= 'GetCanInfo', @ProjectID ='TX100'");
			
			strFileTemplate = @"testCV2.htm";

			try 
			{
				string strHeaderParams = "";
				string strHeaderValues = "";
				string strFooterParams = "";
				string strFooterValues = "";
				//Phan khai bao se dung Tool bao cao Excel
				#region Header 
				strHeaderParams = "";
				strHeaderValues = "";
				#endregion

				#region Footer
				strDate = DateTime.Now.ToString("dd/MM/yyyy");
				strFooterParams = "Date";					
				strFooterValues = strDate;
				#endregion
				
				FPTToolWeb.Reports.v10.clsBaocao bc = new FPTToolWeb.Reports.v10.clsBaocao();
				#region Config Basic
				bc.sfileTemplate = strFileTemplate;
				#region Sub report
				bc.dtSubReport1 = clsCommon.GetDataTable("select candidateID, FirstName + ' ' + LastName as [FullName] from RE_tblRelative");
				//bc.dtSubReport1 = clsDB.GetDataTable("select EmpID, FirstName + ' ' + LastName as [FullName] from HR_tblRelative");
				bc.sItemLink1 = "candidateID";

				//bc.dtSubReport2 = clsCommon.GetDataTable("select EmpID, FirstName + ' ' + LastName as [FullName] from HR_tblRelative");
				//bc.dtSubReport2 = clsDB.GetDataTable("select EmpID, FirstName + ' ' + LastName as [FullName] from HR_tblRelative");
				//bc.sItemLink2 = "EmpID";
				#endregion
				string strReports = bc.strReportPageDoc(dt);
				#endregion
				//End
				txtResult.Text = strReports;
//				ExcelExporter myExcelXport=new ExcelExporter(this.Page);
//				myExcelXport.ExportHTMLTo(strReports,"doc");
//				myExcelXport = null;
			}
			catch(Exception ex)
			{										
				string s = ex.Message;
			}
			finally
			{	
				dt.Dispose();
			}
		}
	}
}
