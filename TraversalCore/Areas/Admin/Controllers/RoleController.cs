using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DTOLayer.DTOs.RoleDTOs;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TraversalCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class RoleController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManger;

        public RoleController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManger)
        {
            _roleManager = roleManager;
            _userManger = userManger;
        }

        public IActionResult Index()
        {
            TempData["adminBreadcrumb"] = "Roller";
            var values = _roleManager.Roles.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult AddRole()
        {

            TempData["adminBreadcrumb"] = "Yeni Rol";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(RoleAddDTO role)
        {
            AppRole newRole = new AppRole()
            {
                Name = role.name
            };
            var result = await _roleManager.CreateAsync(newRole);
            if (result.Succeeded)
                return RedirectToAction("Index", "Role", new { area = "Admin" });
            return View();
        }

        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = _roleManager.Roles.FirstOrDefault(x => x.Id == id);
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
                return RedirectToAction("Index", "Role", new { area = "Admin" });
            return View();
        }
        [HttpGet]
        public IActionResult UpdateRole(int id)
        {
            TempData["adminBreadcrumb"] = "Rol Düzenle";
            var role = _roleManager.Roles.FirstOrDefault(x => x.Id == id);
            RoleUpdateDTO rl = new RoleUpdateDTO()
            {
                id = role.Id,
                name = role.Name
            };
            return View(rl);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(RoleUpdateDTO p)
        {
            var role = _roleManager.Roles.FirstOrDefault(x => x.Id == p.id);
            role.Name = p.name;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
                return RedirectToAction("Index", "Role", new { area = "Admin" });
            return View();
        }

        public IActionResult UserList()
        {
            TempData["adminBreadcrumb"] = "Kullanıcılar";
            var values = _userManger.Users.ToList();
            return View(values);
        }
        [HttpGet]
        public async Task<IActionResult> AssignRole(int id)
        {
            var user = _userManger.Users.FirstOrDefault(x => x.Id == id);
            TempData["adminBreadcrumb"] = user.UserName + " Kullanıcı Rolleri";
            TempData["currentUser"] = user.Id;
            var roles = _roleManager.Roles.ToList();
            var userRoles = await _userManger.GetRolesAsync(user);
            List<UserRolesDTO> newRoles = new List<UserRolesDTO>();
            foreach (var item in roles)
            {
                UserRolesDTO rl = new UserRolesDTO();
                rl.roleId = item.Id; ;
                rl.roleName = item.Name;
                rl.roleExist = userRoles.Contains(rl.roleName);
                newRoles.Add(rl);
            }
            return View(newRoles);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(List<UserRolesDTO> p)
        {
            var userId = (int)TempData["currentUser"];
            var user = _userManger.Users.FirstOrDefault(x => x.Id == userId);
            foreach (var item in p)
            {
                if (item.roleExist)
                    await _userManger.AddToRoleAsync(user, item.roleName);
                else
                    await _userManger.RemoveFromRoleAsync(user, item.roleName);
            }
            return RedirectToAction("UserList");
        }
    }
}
