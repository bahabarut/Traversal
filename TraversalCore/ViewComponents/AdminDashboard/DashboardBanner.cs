using Microsoft.AspNetCore.Mvc;

namespace TraversalCore.ViewComponents.AdminDashboard
{
    public class DashboardBanner : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
