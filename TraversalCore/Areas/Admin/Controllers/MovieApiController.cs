using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using TraversalCore.Areas.Admin.Models;
using System.Collections.Generic;

namespace TraversalCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MovieApiController : Controller
    {
        public async Task<IActionResult> Index()
        {
            TempData["adminBreadcrumb"] = "IMDB API Top 100 Listesi";
            List<MovieViewModel> movies = new List<MovieViewModel>();
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://imdb-top-100-movies.p.rapidapi.com/"),
                Headers =
                {
                    { "X-RapidAPI-Key", "af84d54702msh7e93cfb4765362ep126576jsnf8498b80f673" },
                    { "X-RapidAPI-Host", "imdb-top-100-movies.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                movies = JsonConvert.DeserializeObject<List<MovieViewModel>>(body);
                return View(movies);
            }
        }
    }
}
