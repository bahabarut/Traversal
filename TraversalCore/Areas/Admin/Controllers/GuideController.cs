using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Xml.Schema;
using TraversalCore.Areas.Admin.Models;

namespace TraversalCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    [Authorize(Roles = "Admin")]
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

        [HttpGet]
        public IActionResult UpdateGuide(int id)
        {
            var value = _guideService.TGetById(id);
            TempData["adminBreadcrumb"] = "Rehber Düzenle " + "(" + value.Name + ")";
            UpdateGuideViewModel guide = new UpdateGuideViewModel()
            {
                id = value.GuideID,
                name = value.Name,
                description = value.Description,
                twitter = value.TwitterUrl,
                instagram = value.InstagramUrl
            };
            return View(guide);
        }
        [HttpPost]
        public IActionResult UpdateGuide(UpdateGuideViewModel p)
        {
            Guide value = _guideService.TGetById(p.id);
            value.Name = p.name;
            value.Description = p.description;
            value.InstagramUrl = p.instagram;
            value.TwitterUrl = p.twitter;
            if (p.image != null)
            {
                var directory = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(p.image.FileName);
                var imageName = Guid.NewGuid() + extension;
                var saveLocation = directory + "/wwwroot/guideImages/" + imageName;
                var stream = new FileStream(saveLocation, FileMode.Create);
                p.image.CopyTo(stream);
                value.Image = imageName;
            }
            _guideService.TUpdate(value);
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
