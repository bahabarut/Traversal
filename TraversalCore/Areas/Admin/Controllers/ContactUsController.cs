using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    [Authorize(Roles = "Admin")]
    public class ContactUsController : Controller
    {
        private readonly IContactUsService _contactUsService;

        public ContactUsController(IContactUsService contactUsService)
        {
            _contactUsService = contactUsService;
        }

        public IActionResult Index()
        {
            TempData["adminBreadcrumb"] = "Bize Ulaşın";
            var values = _contactUsService.TGetListByFilter(x => x.MessageStatus == true);
            return View(values);
        }
        public IActionResult MessageDetail(int id)
        {
            var value = _contactUsService.TGetById(id);
            TempData["adminBreadcrumb"] = "Rehber Düzenle " + "(" + value.Subject + ")";
            return View(value);
        }
        public IActionResult ChangeToFalse(int id)
        {
            _contactUsService.ChangeToFalse(id);
            return RedirectToAction("Index", "ContactUs", new { area = "Admin" });
        }
    }
}
