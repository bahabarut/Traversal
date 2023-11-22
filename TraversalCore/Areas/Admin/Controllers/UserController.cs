using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using TraversalCore.Areas.Member.Models;
using TraversalCore.Models;

namespace TraversalCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        readonly IAppUserService _appUserService;
        readonly IReservationService _rezervasyonService;
        readonly UserManager<AppUser> _userManager;
        readonly ICommentService _commentService;
        public UserController(IAppUserService appUserService, UserManager<AppUser> userManager, IReservationService rezervasyonService, ICommentService commentService)
        {
            _appUserService = appUserService;
            _userManager = userManager;
            _rezervasyonService = rezervasyonService;
            _commentService = commentService;
        }
        public IActionResult Index()
        {
            TempData["adminBreadcrumb"] = "Kullanıcılar";
            var values = _appUserService.TGetList();
            return View(values);
        }
        [HttpGet]
        public IActionResult AddUser()
        {
            TempData["adminBreadcrumb"] = "Kullanıcı Ekle";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(UserRegisterViewModel p)
        {
            if (ModelState.IsValid)
            {
                AppUser newUser = new AppUser()
                {
                    Name = p.Name,
                    Surname = p.SurName,
                    UserName = p.UserName,
                    Email = p.Mail,
                    PhoneNumber = p.PhoneNumber
                };
                if (p.Image != null)
                {
                    var directory = Directory.GetCurrentDirectory();
                    var extension = Path.GetExtension(p.Image.FileName);
                    var imageName = Guid.NewGuid() + extension;
                    var saveLocation = directory + "/wwwroot/userImages/" + imageName;
                    var stream = new FileStream(saveLocation, FileMode.Create);
                    p.Image.CopyTo(stream);
                    newUser.ImageUrl = imageName;
                }
                var result = await _userManager.CreateAsync(newUser, p.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "User", new { area = "Admin" });
                }
                else
                {
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "");
            }
            return View(p);
        }
        [HttpGet]
        public IActionResult UpdateUser(int id)
        {
            TempData["adminBreadcrumb"] = "Kullanıcı Düzenle";
            var user = _appUserService.TGetById(id);
            UserEditViewModel val = new UserEditViewModel()
            {
                id = user.Id,
                name = user.Name,
                surname = user.Surname,
                mail = user.Email,
                phonenumber = user.PhoneNumber
            };
            return View(val);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserEditViewModel p)
        {
            var user = _appUserService.TGetById(p.id);
            if (p.image != null)
            {
                var directory = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(p.image.FileName);
                var imageName = Guid.NewGuid() + extension;
                var saveLocation = directory + "/wwwroot/userImages/" + imageName;
                var stream = new FileStream(saveLocation, FileMode.Create);
                p.image.CopyTo(stream);
                user.ImageUrl = imageName;
            }
            user.Name = p.name;
            user.Surname = p.surname;
            user.Email = p.mail;
            user.PhoneNumber = p.phonenumber;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
                return RedirectToAction("Index", "User", new { area = "Admin" });
            return View();
        }
        public IActionResult UserDestination(int id)
        {
            var user = _appUserService.TGetById(id);
            TempData["adminBreadcrumb"] = user.UserName + " Rezervasyonları";
            var values = _rezervasyonService.TGetListByFilterWithDestination(x => x.AppUserID == id && x.Status == "Onaylandı");
            return View(values);
        }
        public IActionResult UserComments(int id)
        {
            var user = _appUserService.TGetById(id);
            TempData["adminBreadcrumb"] = user.UserName + " Yorumları";
            var values = _commentService.TGetListByFilterWithDestination(x => x.CommentState == true);
            return View("~/Areas/Admin/Views/Comment/Index.cshtml", values);
        }

    }
}
