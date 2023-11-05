using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using TraversalCore.Areas.Admin.Models;

namespace TraversalCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class GuideController : Controller
    {
        private readonly IGuideService _guideService;

        public GuideController(IGuideService guideService)
        {
            _guideService = guideService;
        }

        public IActionResult Index()
        {
            TempData["adminBreadcrumb"] = "Rehberler";
            var value = _guideService.TGetList();
            return View(value);
        }
        [HttpGet]
        public IActionResult AddGuide()
        {
            TempData["adminBreadcrumb"] = "Rehber Ekle";
            return View();
        }
        [HttpPost]
        public IActionResult AddGuide(AddGuideViewModel p)
        {
            Guide guide = new Guide()
            {
                Name = p.name,
                Description = p.description,
                InstagramUrl = p.instagram,
                TwitterUrl = p.twitter,
                Status = true
            };
            if (p.image != null)
            {
                var directory = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(p.image.FileName);
                var imageName = Guid.NewGuid() + extension;
                var saveLocation = directory + "/wwwroot/guideImages/" + imageName;
                var stream = new FileStream(saveLocation, FileMode.Create);
                p.image.CopyTo(stream);
                guide.Image = imageName;
            }
            _guideService.TAdd(guide);
            return RedirectToAction("Index", "Guide", new { area = "Admin" });
        }
        public IActionResult ChangeToPassive(int id)
        {
            var value = _guideService.TGetById(id);
            value.Status = false;
            _guideService.TUpdate(value);
            return RedirectToAction("Index", "Guide", new { area = "Admin" });
        }
        public IActionResult ChangeToActive(int id)
        {
            var value = _guideService.TGetById(id);
            value.Status = true;
            _guideService.TUpdate(value);
            return RedirectToAction("Index", "Guide", new { area = "Admin" });
        }
    }
}
