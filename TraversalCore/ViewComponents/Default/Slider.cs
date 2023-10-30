using Microsoft.AspNetCore.Mvc;

namespace TraversalCore.ViewComponents.Home
{
    public class Slider : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
