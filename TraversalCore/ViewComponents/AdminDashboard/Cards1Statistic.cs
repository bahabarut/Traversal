using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace TraversalCore.ViewComponents.AdminDashboard
{
    public class Cards1Statistic : ViewComponent
    {
        Context c = new Context();
        public IViewComponentResult Invoke()
        {
            ViewBag.desCount = c.Destinations.Count();
            ViewBag.usersCount = c.Users.Count();
            return View();
        }
    }
}
