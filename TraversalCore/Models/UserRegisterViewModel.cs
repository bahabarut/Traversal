using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace TraversalCore.Models
{
    public class UserRegisterViewModel
    {
        [Required(ErrorMessage = "Bu Alan Boş Geçilmez!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bu Alan Boş Geçilmez!")]
        public string SurName { get; set; }

        [Required(ErrorMessage = "Bu Alan Boş Geçilmez!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Bu Alan Boş Geçilmez!")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Bu Alan Boş Geçilmez!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Bu Alan Boş Geçilmez!")]
        [Compare("Password", ErrorMessage = "Şifreler Uyuşmıyor!")]
        public string ConfirmPassword { get; set; }
        public IFormFile Image { get; set; }
        public string PhoneNumber { get; set; }
    }
}
