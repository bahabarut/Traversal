using BusinessLayer.Concrete;
using DataAccessLayer.EntityFreamework;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCore.ViewComponents.Home
{
    public class PopularDestination : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            DestinationManager dm = new DestinationManager(new EfDestinaitonDal());
            var values = dm.TGetList();
            return View(values);
        }
    }
}
