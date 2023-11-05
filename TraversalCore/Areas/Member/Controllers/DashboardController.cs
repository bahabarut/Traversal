using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TraversalCore.Areas.Member.Controllers
{
    [Area("Member")]
    public class DashboardController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public DashboardController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            TempData["breadcrumbTitle"] = "Dashboard";
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.nameSurname = user.Name + " " + user.Surname;
            ViewBag.image = user.ImageUrl;
            ViewBag.mail = user.Email;
            return View();
        }
    }
}
