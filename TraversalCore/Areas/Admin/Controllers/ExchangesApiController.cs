using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using TraversalCore.Areas.Admin.Models;
using Newtonsoft.Json;

namespace TraversalCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ExchangesApiController : Controller
    {
        public async Task<IActionResult> Index()
        {
            TempData["adminBreadcrumb"] = "Booking API Kur Verileri";
            ExchangesViewModel exchange = new ExchangesViewModel();
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://booking-com.p.rapidapi.com/v1/metadata/exchange-rates?currency=TRY&locale=en-gb"),
                Headers =
                {
                    { "X-RapidAPI-Key", "af84d54702msh7e93cfb4765362ep126576jsnf8498b80f673" },
                    { "X-RapidAPI-Host", "booking-com.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                exchange = JsonConvert.DeserializeObject<ExchangesViewModel>(body);
                return View(exchange);
            }
        }
    }
}
