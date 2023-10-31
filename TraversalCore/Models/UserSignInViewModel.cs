using System.ComponentModel.DataAnnotations;

namespace TraversalCore.Models
{
    public class UserSignInViewModel
    {
        [Required(ErrorMessage = "Bu Alan Boş Geçilemez!")]
        public string username { get; set; }

        [Required(ErrorMessage = "Bu Alan Boş Geçilemez!")]
        public string password { get; set; }
    }
}
