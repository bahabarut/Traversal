using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace TraversalCore.Areas.Member.Controllers
{
    [Area("Member")]
    [Route("Member/[controller]/[action]/{id?}")]
    public class CommentController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ICommentService _commentService;

        public CommentController(UserManager<AppUser> userManager, ICommentService commentService)
        {
            _userManager = userManager;
            _commentService = commentService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var comments = _commentService.TGetListByFilterWithDestination(x => x.AppUserID == user.Id);
            return View(comments);
        }

        public IActionResult DeleteComment(int id)
        {
            var val = _commentService.TGetById(id);
            _commentService.TDelete(val);
            return RedirectToAction("Index", "Comment", new { area = "Member" });
        }
    }
}
