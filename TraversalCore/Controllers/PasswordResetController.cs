using DocumentFormat.OpenXml.Spreadsheet;
using EntityLayer.Concrete;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using OfficeOpenXml.FormulaParsing.LexicalAnalysis;
using System.Threading.Tasks;
using TraversalCore.Models;

namespace TraversalCore.Controllers
{
    [AllowAnonymous]
    public class PasswordResetController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public PasswordResetController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Mail);
            //create unique key for user (token)
            var resetPassToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            //create token link 1. action name 2. controller 3.param user ıdentity
            var resetPassTokenLink = Url.Action("NewPassword", "PasswordReset", new
            {
                userId = user.Id,
                token = resetPassToken
            }, HttpContext.Request.Scheme);

            //post token with mail process

            //MimeMessage mimeMessage = new MimeMessage();

            ////gondericinin bilgileri
            //MailboxAddress mailBoxAddressFrom = new MailboxAddress("Admin", "senderMail");
            //mimeMessage.From.Add(mailBoxAddressFrom);

            ////alıcinin bilgileri
            //MailboxAddress mailBoxAddressTo = new MailboxAddress("User", model.Mail);
            //mimeMessage.To.Add(mailBoxAddressTo);

            //var bodyBuilder = new BodyBuilder();
            //bodyBuilder.TextBody = resetPassTokenLink;
            //mimeMessage.Body = bodyBuilder.ToMessageBody();

            //mimeMessage.Subject = "Şifre Sıfırlama Linki";

            //SmtpClient client = new SmtpClient();
            //client.Connect("smtp.gmail.com", 587, false);
            ////client.Authenticate("bahabarutps@gmail.com", "ba5Ha5A9"); 2 parametre alıyor göndericinin maili ve passwordu

            ////bu şekilde kullanırken hata verdiğinde 2 faktörlü doğrulamayı açıp bir de şifreli anahtar alcaz bu anahtarı 2.parametreye yazacaz
            //client.Authenticate("senderMail", "******");//2 parametre alıyor göndericinin maili ve passwordu
            //client.Send(mimeMessage);
            //client.Disconnect(true);

            return View();
        }

        [HttpGet]
        public IActionResult NewPassword(string userId, string token)
        {
            TempData["userid"] = userId;
            TempData["token"] = token;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NewPassword(NewPasswordViewModel model)
        {
            var userId = TempData["userid"];
            var tkn = TempData["token"];

            if (userId == null || tkn == null)
            {
                //error message
            }
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var result = await _userManager.ResetPasswordAsync(user, tkn.ToString(), model.confirmPassword);
            if (result.Succeeded)
                return RedirectToAction("SignIn", "Login");
            return View();
        }
    }
}
