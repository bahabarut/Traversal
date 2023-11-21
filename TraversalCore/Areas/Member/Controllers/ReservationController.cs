using BusinessLayer.Concrete;
using DataAccessLayer.EntityFreamework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TraversalCore.Areas.Member.Controllers
{
    [Area("Member")]
    [Route("Member/[controller]/[action]")]
    public class ReservationController : Controller
    {
        DestinationManager dm = new DestinationManager(new EfDestinaitonDal());
        ReservationManager rm = new ReservationManager(new EfReservationDal());
        private readonly UserManager<AppUser> _userManager;
        private AppUser currentUser;
        public ReservationController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> MyCurrentReservation()
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var values = rm.TGetListByFilterWithDestination(x => x.AppUserID == currentUser.Id && x.Status == "Işlem Yapıldı");
            return View(values);
        }

        public async Task<IActionResult> MyOldReservation()
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var values = rm.TGetListByFilterWithDestination(x => x.AppUserID == currentUser.Id && x.Status == "Geçmiş Rezervasyon");
            return View(values);
        }
        public async Task<IActionResult> MyApprovalReservation()
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var values = rm.TGetListByFilterWithDestination(x => x.AppUserID == currentUser.Id && x.Status == "Onay Bekliyor");
            return View(values);
        }
        [HttpGet]
        public IActionResult NewReservation()
        {
            ViewBag.destinations = GetDestinations();
            return View();
        }
        [HttpPost]
        public IActionResult NewReservation(Reservation p)
        {
            p.AppUserID = 1;
            p.Status = "Onay Bekliyor";
            rm.TAdd(p);
            return RedirectToAction("MyApprovalReservation", "Reservation", new { area = "Member" });
        }

        List<SelectListItem> GetDestinations()
        {
            List<SelectListItem> values = (from x in dm.TGetList()
                                           select new SelectListItem
                                           {
                                               Text = $"{x.City} - {x.DayNight} - {x.Price} - {x.Capacity}",
                                               Value = x.DestinationID.ToString()
                                           }).ToList();
            return values;
        }
    }
}
