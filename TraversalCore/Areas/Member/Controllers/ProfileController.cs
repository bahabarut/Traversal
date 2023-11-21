using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using TraversalCore.Areas.Member.Models;

namespace TraversalCore.Areas.Member.Controllers
{
    [Area("Member")]
    [Route("Member/[controller]/[action]")]
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public ProfileController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            UserEditViewModel user = new UserEditViewModel()
            {
                name = currentUser.Name,
                surname = currentUser.Surname,
                phonenumber = currentUser.PhoneNumber,
                mail = currentUser.Email
            };
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Index(UserEditViewModel p)
        {
            if(p.password != null && (p.password != p.confirmpassword))
            {
                ModelState.AddModelError("confirmpassword", "Şifre Uyuşmazlığı");
                return View(p);
            }
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (p.image != null)
            {
                var resource = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(p.image.FileName);
                var imagename = Guid.NewGuid() + extension;
                var savelocation = resource + "/wwwroot/userImages/" + imagename;
                var stream = new FileStream(savelocation, FileMode.Create);
                await p.image.CopyToAsync(stream);
                user.ImageUrl = imagename;
            }
            user.Name = p.name;
            user.Surname = p.surname;
            user.PhoneNumber = p.phonenumber;
            user.Email = p.mail;
            user.PasswordHash = p.password != null ? _userManager.PasswordHasher.HashPassword(user, p.password) : user.PasswordHash;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded) RedirectToAction("SignIn", "Login");
            return View();
        }
    }
}
