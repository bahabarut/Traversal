using BusinessLayer.Concrete;
using DataAccessLayer.EntityFreamework;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCore.ViewComponents.Default
{
    public class Testimonial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            TestimonialManager tm = new TestimonialManager(new EfTestimonialDal());
            var values = tm.TGetList();
            return View(values);
        }
    }
}
