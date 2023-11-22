using BusinessLayer.Abstract.AbstractUow;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using TraversalCore.Areas.Admin.Models;

namespace TraversalCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            TempData["adminBreadcrumb"] = "Unit Of Work Design Pattern İle Hesap İşlemleri";
            ViewBag.accounts = _accountService.TGetList();
            ViewBag.accounts2 = GetAccounts();
            return View();
        }

        [HttpPost]
        public IActionResult Index(AccountViewModel model)
        {
            var sender = _accountService.TGetByID(model.SenderID);
            var receiver = _accountService.TGetByID(model.ReceiverID);

            sender.Balance -= model.Amount;
            receiver.Balance += model.Amount;

            List<Account> accounts = new List<Account>() {
                sender,
                receiver
            };
            _accountService.TMultiUpdate(accounts);
            ViewBag.accounts = _accountService.TGetList();
            ViewBag.accounts2 = GetAccounts();
            return View();
        }

        List<SelectListItem> GetAccounts()
        {
            List<SelectListItem> items = (from x in _accountService.TGetList() select new SelectListItem { Text = x.Name, Value = x.AccountID.ToString() }).ToList();
            return items;
        }
    }
}
