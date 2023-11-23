
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using TraversalCore.Areas.Admin.Models;
using TraversalCore.Models;

namespace TraversalCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MailController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            TempData["adminBreadcrumb"] = "Mail Gönderme Sayfası";
            return View();
        }
        [HttpPost]
        public IActionResult Index(MailRequest p)
        {
            MimeMessage mimeMessage = new MimeMessage();

            //gondericinin bilgileri
            MailboxAddress mailBoxAddressFrom = new MailboxAddress("Admin", "senderMail");
            mimeMessage.From.Add(mailBoxAddressFrom);

            //alıcinin bilgileri
            MailboxAddress mailBoxAddressTo = new MailboxAddress("User", p.ReceiverMail);
            mimeMessage.To.Add(mailBoxAddressTo);

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = p.Body;
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            mimeMessage.Subject = p.Subject;

            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            //client.Authenticate("bahabarutps@gmail.com", "ba5Ha5A9"); 2 parametre alıyor göndericinin maili ve passwordu

            //bu şekilde kullanırken hata verdiğinde 2 faktörlü doğrulamayı açıp bir de şifreli anahtar alcaz bu anahtarı 2.parametreye yazacaz
            client.Authenticate("senderMail", "******");//2 parametre alıyor göndericinin maili ve passwordu
            client.Send(mimeMessage);
            client.Disconnect(true);
            return View();
        }
    }
}
