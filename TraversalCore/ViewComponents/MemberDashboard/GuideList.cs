using BusinessLayer.Concrete;
using DataAccessLayer.EntityFreamework;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCore.ViewComponents.MemberDashboard
{
    public class GuideList:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            GuideManager gm = new GuideManager(new EfGuideDal());
            var values = gm.TGetList();
            return View(values);
        }
    }
}
