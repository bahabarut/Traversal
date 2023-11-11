using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
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
        public IActionResult ChangeToFalse(int id)
        {
            _contactUsService.ChangeToFalse(id);
            return RedirectToAction("Index", "ContactUs", new { area = "Admin" });
        }
    }
}
