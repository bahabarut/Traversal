using Microsoft.AspNetCore.Mvc;

namespace TraversalCore.ViewComponents.Home
{
    public class SliderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
