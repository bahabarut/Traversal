using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TraversalCore.Areas.Admin.Models;

namespace TraversalCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SearchHotelApiController : Controller
    {
        public async Task<IActionResult> Index()
        {
            TempData["adminBreadcrumb"] = "Booking API Hotel Listesi";
            var client = new HttpClient();
            int destId = 553173;
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://booking-com.p.rapidapi.com/v2/hotels/search?order_by=popularity&adults_number=2&checkin_date=2023-11-27&filter_by_currency=EUR&dest_id=-{destId}&locale=en-gb&checkout_date=2023-11-28&units=metric&room_number=1&dest_type=city&include_adjacency=true&children_number=2&page_number=0&children_ages=5%2C0&categories_filter_ids=class%3A%3A2%2Cclass%3A%3A4%2Cfree_cancellation%3A%3A1"),
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
                var values = JsonConvert.DeserializeObject<SearchHotelViewModel>(body);
                return View(values.results);
            }
        }

    }
}
