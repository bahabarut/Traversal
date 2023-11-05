using ClosedXML.Excel;
using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TraversalCore.Models;

namespace TraversalCore.Controllers
{
    public class ExcelController : Controller
    {
        public IActionResult Index()
        {
            ExcelPackage excel = new ExcelPackage(); //error license !!
            var workSheet = excel.Workbook.Worksheets.Add("Sayfa1");
            workSheet.Cells[1, 1].Value = "Rota";
            workSheet.Cells[1, 1].Value = "Rehber";
            workSheet.Cells[1, 1].Value = "Kontenjan";

            workSheet.Cells[2, 1].Value = "Gürcistan Btum";
            workSheet.Cells[2, 2].Value = "Kadi Yıldız";
            workSheet.Cells[2, 3].Value = "50";

            workSheet.Cells[3, 1].Value = "Antalya - karadeniz";
            workSheet.Cells[3, 2].Value = "Zeynep öztürk";
            workSheet.Cells[3, 3].Value = "35";
            var byts = excel.GetAsByteArray();

            return File(byts, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "dosya1.xlsx");
        }


        public List<DestinationModel> DestinationList()
        {
            List<DestinationModel> DestinationModels = new List<DestinationModel>();
            using (Context c = new Context())
            {
                DestinationModels = c.Destinations.Select(x => new DestinationModel
                {
                    City = x.City,
                    DayNight = x.DayNight,
                    Capacity = x.Capacity,
                    Price = x.Price
                }).ToList();
            }
            return DestinationModels;
        }


        public IActionResult DestinationExcelReport()
        {
            using (var workbook = new XLWorkbook())
            {
                var workSheet = workbook.Worksheets.Add("Tur Listesi");
                workSheet.Cell(1, 1).Value = "Şehir";
                workSheet.Cell(1, 2).Value = "Konaklama Süresi";
                workSheet.Cell(1, 3).Value = "Kapasite";
                workSheet.Cell(1, 4).Value = "Fiyat";

                var rowCount = 2;

                foreach (var item in DestinationList())
                {
                    workSheet.Cell(rowCount, 1).Value = item.City;
                    workSheet.Cell(rowCount, 2).Value = item.DayNight;
                    workSheet.Cell(rowCount, 3).Value = item.Capacity;
                    workSheet.Cell(rowCount, 4).Value = item.Price;
                    rowCount++;
                }
                using (var steram = new MemoryStream())
                {
                    workbook.SaveAs(steram);
                    var content = steram.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Yeni liste.xlsx");

                }
            }
            
        }
    }
}
