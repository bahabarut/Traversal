using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TraversalCore.Areas.Admin.Models;

namespace TraversalCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class VisitorApiController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public VisitorApiController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            TempData["adminBreadcrumb"] = "API ile Ziyaretçi işlemleri";
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44318/api/Visitor");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonVal = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<VisitorViewModel>>(jsonVal);
                return View(values);
            }
            return View();
        }
        [HttpGet]
        public IActionResult AddVisitor()
        {
            TempData["adminBreadcrumb"] = "Ziyaretçi Ekle";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddVisitor(VisitorViewModel p)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(p);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var resutl = await client.PostAsync("https://localhost:44318/api/Visitor", content);
            if (resutl.IsSuccessStatusCode) return RedirectToAction("Index", "VisitorApi", new { area = "Admin" });
            else return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateVisitor(int id)
        {
            TempData["adminBreadcrumb"] = "Ziyaretçi Düzenle";
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44318/api/Visitor/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonVal = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<VisitorViewModel>(jsonVal);
                return View(value);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateVisitor(VisitorViewModel p)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(p);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var result = await client.PutAsync("https://localhost:44318/api/Visitor", content);
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "VisitorApi", new { area = "Admin" });
            }
            return View();
        }

        public async Task<IActionResult> DeleteVisitor(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var result = await client.DeleteAsync("https://localhost:44318/api/Visitor/" + id);
            if (result.IsSuccessStatusCode)
                return RedirectToAction("Index", "VisitorApi", new { area = "Admin" });
            else
                return View();
        }
    }
}
