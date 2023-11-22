using BusinessLayer.Abstract;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using TraversalCore.Models;

namespace TraversalCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CityController : Controller
    {
        private readonly IDestinationService _destinationService;

        public CityController(IDestinationService destinationService)
        {
            _destinationService = destinationService;
        }

        public IActionResult Index()
        {
            TempData["adminBreadcrumb"] = "Şehir - Ülke İşlemleri (Ajax-jquery)";
            return View();
        }

        public IActionResult CityList()
        {
            var jsonCity = JsonConvert.SerializeObject(cities);
            return Json(jsonCity);
        }
        public IActionResult DestinationList()
        {
            var values = _destinationService.TGetList();
            var jsonDest = JsonConvert.SerializeObject(values);
            return Json(jsonDest);
        }
        public IActionResult CityById(int id)
        {
            var city = cities.Find(x => x.CityID == id);
            var jsonCity = JsonConvert.SerializeObject(city);
            return Json(jsonCity);
        }
        public IActionResult CityAdd(CityClass city)
        {
            CityClass newCity = new CityClass()
            {
                CityID = cities.Count + 1,
                CityName = city.CityName,
                CityCountry = city.CityCountry
            };
            cities.Add(newCity);
            var jsonData = JsonConvert.SerializeObject(newCity);
            return Json(jsonData);
        }
        public IActionResult CityDelete(int id)
        {
            var city = cities.Find(x => x.CityID == id);
            cities.Remove(city);
            var jsonData = JsonConvert.SerializeObject(cities);
            return Json(jsonData);
        }

        public IActionResult CityUpdate(CityClass city)
        {
            var ct = cities.Find(x => x.CityID == city.CityID);
            ct.CityCountry = city.CityCountry;
            ct.CityName = city.CityName;
            var jsonData = JsonConvert.SerializeObject(ct);
            return Json(jsonData);
        }
        public static List<CityClass> cities = new List<CityClass>() {
            new CityClass{ CityID = 1, CityName = "Üküp", CityCountry = "Makedonya" },
            new CityClass{ CityID = 2, CityName = "Roma", CityCountry = "İtalya" },
            new CityClass{ CityID = 3, CityName = "Londra", CityCountry = "İngiltere" },
        };
    }
}
