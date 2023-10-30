using BusinessLayer.Concrete;
using DataAccessLayer.EntityFreamework;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCore.ViewComponents.Default
{
    public class SubAbout : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            SubAboutManager sm = new SubAboutManager(new EfSubAboutDal());
            var values = sm.TGetList();
            return View(values);
        }
    }
}
