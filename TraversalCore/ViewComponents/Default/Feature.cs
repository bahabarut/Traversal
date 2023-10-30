using BusinessLayer.Concrete;
using DataAccessLayer.EntityFreamework;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace TraversalCore.ViewComponents.Default
{
    public class Feature : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            FeatureManager fm = new FeatureManager(new EfFeatureDal());
            var values = fm.TGetList().Take(5).ToList();
            return View(values);
        }
    }
}
