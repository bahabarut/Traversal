using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace TraversalCore.Controllers
{
    public class PdfReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult StaticPdfReport()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pdfReports/" + "dosya1.pdf");
            var stream = new FileStream(path, FileMode.Create);

            Document document = new Document(PageSize.A4);
            PdfWriter.GetInstance(document, stream);
            document.Open();

            Paragraph paragraph = new Paragraph("Traversal Rezervasyon Pdf Raporu");

            document.Add(paragraph);
            document.Close();
            return File("/pdfReports/dosya1.pdf", "application/pdf", "dosya1.pdf");
        }

        public IActionResult StaticCustomerReport()
        {

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pdfReports/" + "dosya2.pdf");
            var stream = new FileStream(path, FileMode.Create);

            Document document = new Document(PageSize.A4);
            PdfWriter.GetInstance(document, stream);
            document.Open();

            PdfPTable pdfTable = new PdfPTable(3);

            pdfTable.AddCell("Misafir Adı");
            pdfTable.AddCell("Misafir Soyadı");
            pdfTable.AddCell("Misafir TC");

            pdfTable.AddCell("Baha");
            pdfTable.AddCell("Barut");
            pdfTable.AddCell("111111");


            pdfTable.AddCell("Ahmet");
            pdfTable.AddCell("Akay");
            pdfTable.AddCell("33333");

            pdfTable.AddCell("Ali");
            pdfTable.AddCell("Yalçın");
            pdfTable.AddCell("333");
            document.Add(pdfTable);

            document.Close();
            return File("/pdfReports/dosya2.pdf", "application/pdf", "dosya2.pdf");
        }
    }
}
