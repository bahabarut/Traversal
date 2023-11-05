using Microsoft.AspNetCore.Mvc;

namespace TraversalCore.ViewComponents.AdminDashboard
{
    public class TotalRevenue : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
