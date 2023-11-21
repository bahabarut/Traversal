using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
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
            using (Context c = new Context())
            {
                ViewBag.guideCount = c.Guides.Count();
                ViewBag.destinationCount = c.Destinations.Count();
                ViewBag.activeRes = c.Reservations.Where(x => x.Status == "Onaylandı").Count();
                ViewBag.oldRes = c.Reservations.Where(x => x.Status == "Geçmiş Rezervasyon").Count();
                ViewBag.approvalRes = c.Reservations.Where(x => x.Status == "Onay Bekliyor").Count();

            }
            return View();
        }
    }
}
