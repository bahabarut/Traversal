using BusinessLayer.Concrete;
using DataAccessLayer.EntityFreamework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCore.Controllers
{
    [AllowAnonymous]
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
            var value = dm.GetDestinationByIdWithGuide(id);
            return View(value);
        }
    }
}
