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
	public class TestPDF2 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button Button1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
			//Document is inbuilt class, available in iTextSharp
			Document document = new Document(PageSize.A4, 80, 50, 30, 65);
			StringBuilder strData = new StringBuilder(string.Empty);
			//I have provided Path for the HTML which will be generated from GridView content
			string strHTMLpath = Server.MapPath("MyHTML.html");
			//I have provided Path for the PDF file which will be generated from HTML content
			string strPDFpath = Server.MapPath("MyPDF.pdf");
			try
			{
				StringWriter sw = new StringWriter();
				sw.WriteLine(Environment.NewLine);
				sw.WriteLine(Environment.NewLine);
				sw.WriteLine(Environment.NewLine);
				sw.WriteLine(Environment.NewLine);
				HtmlTextWriter htw = new HtmlTextWriter(sw);
				
				StreamWriter strWriter = new StreamWriter(strHTMLpath, false, Encoding.UTF8);
				strWriter.Write("<html><head>meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" /></head><body>Lê Đào Trung Tân</body></html>");
				strWriter.Close();
				//strWriter.Dispose();
				BaseFont bf = BaseFont.CreateFont("c:\\windows\\fonts\\times.ttf", BaseFont.IDENTITY_H, true);
				Font f2 = new iTextSharp.text.Font(bf, 24, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLUE);


				iTextSharp.text.html.simpleparser.
				StyleSheet styles = new iTextSharp.text.html.simpleparser.StyleSheet();
				styles.LoadTagStyle("ol", "leading", "16,0");
				PdfWriter.GetInstance(document, new FileStream(strPDFpath, FileMode.Create));
				document.Add(new Header(iTextSharp.text.html.Markup.HTML_ATTR_STYLESHEET, "Style.css"));
				document.Open();
				ArrayList objects;
				styles.LoadTagStyle("li", "face", "garamond");
				styles.LoadTagStyle("span", "size", "8px");
				styles.LoadTagStyle("body", "font-family", "times new roman");
				styles.LoadTagStyle("body", "font-size", "10px");
				document.NewPage();
				objects = iTextSharp.text.html.simpleparser.
					HTMLWorker.ParseToList(new StreamReader(strHTMLpath, Encoding.Default), styles);
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
				Response.Write(Server.MapPath( strPDFpath));
				Response.ClearContent();
				Response.ClearHeaders();
				Response.AddHeader("Content-Disposition", "attachment; filename=" + strPDFpath);
				Response.ContentType = "application/octet-stream";
				Response.WriteFile(Server.MapPath(strPDFpath));
				Response.Flush();
				Response.Close();
				if (File.Exists(Server.MapPath("~/" + strPDFpath)))
				{
					File.Delete(Server.MapPath("~/" + strPDFpath));
				}
			}
		}
	}
}
