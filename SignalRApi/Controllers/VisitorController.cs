using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalRApi.DAL;
using SignalRApi.Models;
using System;
using System.Linq;

namespace SignalRApi.Controllers
{
    /*
- her bir gün 5 saniyede tamamlanacak
- her günde 5 farkllı şehre random değer gelecek
- Totalda 10 ardışık günün değeri tabloya eklenecek
- işlemler toplam 50 saniye sürecek 
- her saniyede tablonun son hali gözükecek
- toplamda 50 satır kayıt eklenmiş olacak
- bu işlemler postmanda gerçekleşecek
 */
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorController : ControllerBase
    {
        private readonly VisitorService _visitorService;

        public VisitorController(VisitorService visitorService)
        {
            _visitorService = visitorService;
        }
        [HttpGet]
        public IActionResult CreateVisitor()
        {
            Random random = new Random();
            Enumerable.Range(1, 10).ToList().ForEach(x =>
            {
                foreach (ECity item in Enum.GetValues(typeof(ECity)))
                {
                    var newVisitor = new Visitor
                    {
                        City = item,
                        CityVisitCount = random.Next(100, 2000),
                        VisitDate = DateTime.Now.AddDays(x)
                    };
                    _visitorService.SaveVisitor(newVisitor).Wait();
                    System.Threading.Thread.Sleep(1000);
                }
            });
            return Ok("Ziyaretçiler başarılı bir şekide eklendi");
        }
    }
}
