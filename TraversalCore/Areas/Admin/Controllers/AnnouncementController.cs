using AutoMapper;
using BusinessLayer.Abstract;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DTOLayer.DTOs.AnnouncementDTOs;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TraversalCore.Mapping.AutoMapperProfile;

namespace TraversalCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class AnnouncementController : Controller
    {
        private readonly IAnnouncementService _announcementService;
        private readonly IMapper _mapper;

        public AnnouncementController(IAnnouncementService announcementService, IMapper mapper)
        {
            _announcementService = announcementService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            TempData["adminBreadcrumb"] = "Duyurular";
            var values = _mapper.Map<List<AnnouncementListDTO>>(_announcementService.TGetList());
            return View(values);
        }

        [HttpGet]
        public IActionResult AddAnnouncement()
        {
            TempData["adminBreadcrumb"] = "Yeni Duyuru";
            return View();
        }
        [HttpPost]
        public IActionResult AddAnnouncement(AnnouncementAddDTO p)
        {
            if (ModelState.IsValid)
            {
                _announcementService.TAdd(new Announcement()
                {
                    Content = p.Content,
                    Title = p.Title,
                    Date = Convert.ToDateTime(DateTime.Now.ToShortDateString())
                });
                return RedirectToAction("Index", "Announcement", new { area = "Admin" });
            }
            return View(p);
        }
        public IActionResult DeleteAnnouncement(int id)
        {
            var val = _announcementService.TGetById(id);
            _announcementService.TDelete(val);
            return RedirectToAction("Index", "Announcement", new { area = "Admin" });
        }
        [HttpGet]
        public IActionResult UpdateAnnouncement(int id)
        {
            TempData["adminBreadcrumb"] = "Duyuru Düzenle";
            var value = _mapper.Map<AnnouncementUpdateDTO>(_announcementService.TGetById(id));
            return View(value);
        }
        [HttpPost]
        public IActionResult UpdateAnnouncement(AnnouncementUpdateDTO p)
        {
            if (ModelState.IsValid)
            {
                _announcementService.TUpdate(new Announcement()
                {
                    AnnouncementID = p.AnnouncementID,
                    Content = p.Content,
                    Title = p.Title,
                    Date = Convert.ToDateTime(DateTime.Now.ToShortDateString())
                });
                return RedirectToAction("Index", "Announcement", new { area = "Admin" });
            }
            return View(p);
        }
    }
}
