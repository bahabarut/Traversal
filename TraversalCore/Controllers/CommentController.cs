using BusinessLayer.Concrete;
using DataAccessLayer.EntityFreamework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Threading.Tasks;

namespace TraversalCore.Controllers
{
    public class CommentController : Controller
    {
        CommentManager cm = new CommentManager(new EfCommentDal());
        private readonly UserManager<AppUser> _usermanager;

        public CommentController(UserManager<AppUser> usermanager)
        {
            _usermanager = usermanager;
        }

        [HttpGet]
        public PartialViewResult AddComment()
        {
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> AddComment(Comment p)
        {
            var user = await _usermanager.FindByNameAsync(User.Identity.Name);
            p.AppUserID = user.Id;
            p.CommentState = true;
            p.CommentDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            cm.TAdd(p);
            return RedirectToAction("Index", "Destination");
        }
    }
}
