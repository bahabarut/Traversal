using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TraversalCore.ViewComponents.MemberDashboard
{
    public class ProfileInformtion : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public ProfileInformtion(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.nameSurname = user.Name + " " + user.Surname;
            ViewBag.number = user.PhoneNumber;
            ViewBag.mail = user.Email;
            return View();
        }
    }
}
