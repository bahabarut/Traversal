using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    [Authorize(Roles = "Admin")]
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public IActionResult Index()
        {
            TempData["adminBreadcrumb"] = "Rezervasyonlar Listesi";
            var values = _reservationService.GetResrevationsWithUserDestination();
            return View(values);
        }

        public IActionResult ReservationDetail(int id)
        {
            var value = _reservationService.GetReservationWithUserDestination(id);
            TempData["adminBreadcrumb"] = "Rezervasyon Detay";
            return View(value);
        }
        public IActionResult DeleteReservation(int id)
        {
            var value = _reservationService.TGetById(id);
            _reservationService.TDelete(value);
            return RedirectToAction("Index", "Reservation", new { area = "Admin" });
        }

        public IActionResult Approve(int id)
        {
            var value = _reservationService.TGetById(id);
            value.Status = "Onaylandı";
            _reservationService.TUpdate(value);
            return RedirectToAction("Index", "Reservation", new { area = "Admin" });
        }
        public IActionResult Reject(int id)
        {
            var value = _reservationService.TGetById(id);
            value.Status = "Geçmiş Rezervasyon";
            _reservationService.TUpdate(value);
            return RedirectToAction("Index", "Reservation", new { area = "Admin" });
        }
    }
}
