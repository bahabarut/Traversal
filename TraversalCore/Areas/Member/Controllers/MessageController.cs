using Microsoft.AspNetCore.Mvc;

namespace TraversalCore.Areas.Member.Controllers
{
    [Area("Member")]
    public class MessageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
