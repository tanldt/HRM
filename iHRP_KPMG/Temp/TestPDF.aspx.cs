using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;


using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;


namespace Temp
{
	/// <summary>
	/// Summary description for TestPDF.
	/// </summary>
	public class TestPDF : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.Button Button1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}
		public DataTable GetFirstTable()
		{
			DataTable FirstTable = new DataTable();
			// Declare DataColumn and DataRow variables.   
			DataColumn column;
			DataRow row;
			// Create new DataColumn, set DataType, ColumnName and add to DataTable.   
			column = new DataColumn();
			column.DataType = System.Type.GetType("System.Int32");
			column.ColumnName = "id";
				FirstTable.Columns.Add(column);
			// Create second column.          
			column = new DataColumn();
			column.DataType = Type.GetType("System.Int32");
			column.ColumnName = "Amount1";
				FirstTable.Columns.Add(column);
			column = new DataColumn();
			column.DataType = Type.GetType("System.Int32");
			column.ColumnName = "Amount2";
				FirstTable.Columns.Add(column);
			// Create new DataRow objects and add to DataTable. 
			for (int i = 0; i < 15; i++)
			{
				row = FirstTable.NewRow();
				row["id"] = i;
				row["Amount1"] = i.ToString();
				row["Amount2"] = "100" + i.ToString();
					FirstTable.Rows.Add(row);
			}
			return FirstTable;
		}
		public void ConvertHtmlStringToPDF()
		{
			DataGrid grdTemp = new DataGrid();
			grdTemp.DataSource = GetFirstTable();
			grdTemp.DataBind();
			HtmlForm form = new HtmlForm();
			form.Controls.Add(grdTemp);
			StringWriter sw = new StringWriter();
			HtmlTextWriter hTextWriter = new HtmlTextWriter(sw);
			form.Controls[0].RenderControl(hTextWriter);
			string html = sw.ToString();
			string htmlDisplayText = @"<p><strong>Introduction</strong></p><p>Hi Good morning. Thanh niên quê tôi chỉ uống rượu là hay. </p><p>Chi tiết nhân viên</p><p>{EMPLOYEETABLE}</p><p>Thanks…</p>";
			htmlDisplayText = htmlDisplayText.Replace("{EMPLOYEETABLE}", html);         
			

			Document document = new Document();          
			MemoryStream ms = new MemoryStream();
			PdfWriter writer = PdfWriter.GetInstance(document, ms);        
			StringReader se = new StringReader(htmlDisplayText);
			HTMLWorker obj = new HTMLWorker(document);
			//BaseFont baseFnt = new BaseFont();

			document.Open();
			
//			BaseFont bf = BaseFont.CreateFont("c:\\windows\\fonts\\times.ttf", BaseFont.IDENTITY_H, true);
//			Font f2 = new Font(bf);
//
////			BaseFont bfComic = BaseFont.createFont("c:\\winnt\\fonts\\comic.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
////			Font font = new Font(bfComic, 12);
//			String text1 = "Lê đào trung Tân";
//			String text2 = "Some greek characters: \u0393\u0394\u03b6";
//			String text3 = "Some cyrillic characters: \u0418\u044f";
//			document.Add(new Paragraph(text1, f2));
//			document.Add(new Paragraph(text2, font));
//			document.Add(new Paragraph(text3, font));


			obj.Parse(se);
			// step 5: we close the document
			document.Close();      
			Response.Clear();
			Response.AddHeader("content-disposition", "attachment; filename=report.pdf");
			Response.ContentType = "application/pdf";
			Response.Charset = "Windows-1252";
			//Encoding enc = Encoding.GetEncoding(1252);
			Response.Buffer = true;        
			Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
			Response.OutputStream.Flush();
			Response.End();

		}
	
		public static string UniCode2ISCII(string  S)
		{

			if (S == null) { return null; }
			try
			{
				Encoding encFrom = Encoding.GetEncoding(57002);//Hindi
				//Encoding encFrom = Encoding.GetEncoding(57005);//Telugu
           
				Encoding encTo = Encoding.GetEncoding(1252);
				string str = S;
				Byte[] b = encFrom.GetBytes(str);
				return encTo.GetString(b);
			}
			catch { return null; }
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
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
			ConvertHtmlStringToPDF();
		}

		private void Button2_Click(object sender, System.EventArgs e)
		{
//			Document document = new Document(PageSize.A4, 80, 50, 30, 65);
//			string sHTML = Server.MapPath("TestPDF.aspx");
//			string sPDF = Server.MapPath("pdffilename.pdf");
//			try
//			{
//				StreamWriter sWriter = new StreamWriter(sHTML, false, Encoding.UTF8);
//				//sWriter.Write("&lt;link href="Style.css" type="text/css" rel="stylesheet"&gt;" + htw.InnerWriter.ToString() + "");
//				sWriter.Close();
//				sWriter.Dispose();
//				PdfWriter.getInstance(document, new FileStream(sPDF, FileMode.Create));
//				HtmlParser.parse(document, sHTML);
//			}
//			catch (Exception ex)
//			{
//				throw ex;
//			}
//			finally
//			{
//				document.Close();
//				Response.Write(Server.MapPath(
//					"~/" + strPDFpath));
//				Response.ClearContent();
//			}

			Document document = new Document(PageSize.A4);
			MemoryStream m = new MemoryStream();
			try
			{
				PdfWriter pdfWriter = PdfWriter.GetInstance(document, m);
				document.Open();
				string myPath = "D:\\Projects\\iHRP_KPMG\\WIP\\Program\\iHRP_KPMG\\Temp\\aaa.xml";
				
				Encoding enc = Encoding.GetEncoding(1252);
				StreamReader reader = new StreamReader(myPath, enc);
				System.Xml.XmlTextReader _xmlr = new System.Xml.XmlTextReader(reader);
				iTextSharp.text.html.HtmlParser.Parse(document, _xmlr);
				document.Close();
			}
			catch (Exception ex) { string err = ex.Message; }
			byte[] bytes = m.GetBuffer();
			Response.Clear();
			Response.ContentType = "application/pdf";
			Response.AppendHeader("Content-Disposition", "filename=waybill.pdf");
			Response.BinaryWrite(bytes);
			Response.Flush();
			Response.End();
		}
	}
}
