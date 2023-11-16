using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFreamework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using TraversalCore.Areas.Admin.Models;

namespace TraversalCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class DestinationController : Controller
    {
        private readonly IDestinationService _destinationService;

        public DestinationController(IDestinationService destinationService)
        {
            _destinationService = destinationService;
        }

        public IActionResult Index()
        {

            TempData["adminBreadcrumb"] = "Destinasyonlar";
            var values = _destinationService.TGetList();
            return View(values);
        }
        [HttpGet]
        public IActionResult AddDestination()
        {
            TempData["adminBreadcrumb"] = "Yeni Destinasyon";
            return View();
        }
        [HttpPost]
        public IActionResult AddDestination(AddDestinationViewModel p)
        {
            if (ModelState.IsValid)
            {
                Destination newDes = new Destination()
                {
                    City = p.city,
                    DayNight = p.night + " Gece " + p.day + " Gün",
                    Price = p.price,
                    Capacity = p.capacity,
                    Description = p.description,
                    Details1 = p.details1,
                    Details2 = p.details2,
                    Status = true
                };

                if (p.image != null)
                {
                    var extension = Path.GetExtension(p.image.FileName);
                    var imageName = Guid.NewGuid() + extension;
                    var resource = Directory.GetCurrentDirectory();
                    var location = resource + "/wwwroot/destinationImages/" + imageName;
                    var stream = new FileStream(location, FileMode.Create);
                    p.image.CopyTo(stream);
                    newDes.Image = imageName;
                    newDes.CoverImage = imageName;
                }
                _destinationService.TAdd(newDes);
                return RedirectToAction("Index", "Destination", new { area = "Admin" });
            }
            else
            {
                ModelState.AddModelError("", "");
                return View(p);
            }
        }
        [HttpGet]
        public IActionResult UpdateDestination(int id)
        {
            TempData["adminBreadcrumb"] = "Destination Düzenle";
            var val = _destinationService.TGetById(id);
            var dest = new AddDestinationViewModel()
            {
                id = val.DestinationID,
                city = val.City,
                capacity = val.Capacity,
                description = val.Description,
                day = dayNightSplit(val.DayNight, "Gün"),
                night = dayNightSplit(val.DayNight, "Gece"),
                details1 = val.Details1,
                details2 = val.Details2,
                price = val.Price
            };
            return View(dest);
        }

        [HttpPost]
        public IActionResult UpdateDestination(AddDestinationViewModel p)
        {
            ModelState.Remove("image");
            if (ModelState.IsValid)
            {
                var val = _destinationService.TGetById(p.id);
                val.City = p.city;
                val.Capacity = p.capacity;
                val.Description = p.description;
                val.DayNight = p.night + " Gece " + p.day + " Gün";
                val.Price = p.price;

                if (p.image != null)
                {
                    var extension = Path.GetExtension(p.image.FileName);
                    var imageName = Guid.NewGuid() + extension;
                    var resource = Directory.GetCurrentDirectory();
                    var location = resource + "/wwwroot/destinationImages/" + imageName;
                    var stream = new FileStream(location, FileMode.Create);
                    p.image.CopyTo(stream);
                    val.Image = imageName;
                    val.CoverImage = imageName;
                }
                _destinationService.TUpdate(val);
                return RedirectToAction("Index", "Destination", new { area = "Admin" });
            }
            else
            {
                ModelState.AddModelError("", "");
                return View(p);
            }
        }
        public IActionResult DeleteDestination(int id)
        {
            var val = _destinationService.TGetById(id);
            _destinationService.TDelete(val);
            return RedirectToAction("Index", "Destination", new { area = "Admin" });
        }

        int dayNightSplit(string p, string p2)
        {
            var vals = p.Split(" ");
            for (int i = 0; i < vals.Length; i++)
            {
                if (vals[i] == p2 && i > 0)
                {
                    return int.Parse(vals[i - 1]);
                }
            }
            return 0;
        }
    }
}
