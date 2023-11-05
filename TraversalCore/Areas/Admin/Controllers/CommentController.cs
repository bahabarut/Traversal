using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFreamework;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        public IActionResult Index()
        {
            TempData["adminBreadcrumb"] = "Yorumlar";
            var values = _commentService.GetListWithDestination();
            return View(values);
        }
        public IActionResult DeleteComment(int id)
        {
            var value = _commentService.TGetById(id);
            _commentService.TDelete(value);
            return RedirectToAction("Index", "Comment", new { area = "Admin" });

        }
    }
}
