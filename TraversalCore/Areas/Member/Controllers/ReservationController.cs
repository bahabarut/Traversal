using BusinessLayer.Concrete;
using DataAccessLayer.EntityFreamework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace TraversalCore.Areas.Member.Controllers
{
    [Area("Member")]
    [Route("Member/[controller]/[action]")]
    public class ReservationController : Controller
    {
        DestinationManager dm = new DestinationManager(new EfDestinaitonDal());
        ReservationManager rm = new ReservationManager(new EfReservationDal());
        public IActionResult MyCurrentReservation()
        {
            TempData["breadcrumbTitle"] = "Aktif Rezervasyonlarım";
            var values = rm.TGetListByFilter(x => x.AppUserID == 1 && x.Status=="İşlem Yapıldı");
            return View(values);
        }

        public IActionResult MyOldReservation()
        {
            TempData["breadcrumbTitle"] = "Geçmiş Rezervasyon";
            var values = rm.TGetListByFilter(x => x.AppUserID == 1 && x.Status=="İşlem Yapıldı");
            return View(values);
        }
        [HttpGet]
        public IActionResult NewReservation()
        {
            TempData["breadcrumbTitle"] = "Yeni Rezervasyon";
            ViewBag.destinations = GetDestinations();
            return View();
        }
        [HttpPost]
        public IActionResult NewReservation(Reservation p)
        {
            p.AppUserID = 1;
            p.Status = "Onay Bekliyor";
            rm.TAdd(p);
            return RedirectToAction("MyCurrentReservation", "Reservation", new { area = "Member" });
        }

        List<SelectListItem> GetDestinations()
        {
            List<SelectListItem> values = (from x in dm.TGetList()
                                           select new SelectListItem
                                           {
                                               Text = $"{x.City} - {x.DayNight} - {x.Price} - {x.Capacity}",
                                               Value = $"{x.City} - {x.DayNight}"
                                           }).ToList();
            return values;
        }
    }
}
