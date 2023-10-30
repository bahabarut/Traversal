using BusinessLayer.Concrete;
using DataAccessLayer.EntityFreamework;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCore.Controllers
{
    public class DestinationController : Controller
    {
        DestinationManager dm = new DestinationManager(new EfDestinaitonDal());
        public IActionResult Index()
        {
            var values = dm.TGetList();
            return View(values);
        }

        public IActionResult DestinationDetails(int id)
        {
            var value = dm.TGetById(id);
            return View(value);
        }
    }
}
