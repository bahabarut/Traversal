using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFreamework;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace TraversalCore.ViewComponents.Default
{
    public class Statistics : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            using (Context c = new Context())
            {
                ViewBag.destinations = c.Destinations.Count();
                ViewBag.guides = c.Guides.Count();
                ViewBag.happeyCustomer = 136;
            }
            return View();
        }
    }
}
