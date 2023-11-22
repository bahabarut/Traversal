using BusinessLayer.Concrete;
using DataAccessLayer.EntityFreamework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCore.Areas.Member.Controllers
{
    [Area("Member")]
    public class DestinationController : Controller
    {
        DestinationManager dm = new DestinationManager(new EfDestinaitonDal());
        public IActionResult Index()
        {
            TempData["breadcrumbTitle"] = "Destination";
            var values = dm.TGetList();
            return View(values);
        }
    }
}
