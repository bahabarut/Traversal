using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCore.Areas.Member.Controllers
{
    [Area("Member")]
    public class CommentController : Controller
    {
        public IActionResult Index()
        {
            TempData["breadcrumbTitle"] = "Comment";
            return View();
        }
    }
}
