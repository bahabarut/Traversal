using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace TraversalCore.Areas.Admin.Models
{
    public class AddDestinationViewModel
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Bu Alan Boş Geçilemez!")]
        public string city { get; set; }

        [Required(ErrorMessage = "Bu Alan Boş Geçilemez!")]
        public int day { get; set; }

        [Required(ErrorMessage = "Bu Alan Boş Geçilemez!")]
        public int night { get; set; }

        [Required(ErrorMessage = "Bu Alan Boş Geçilemez!")]
        public int price { get; set; }

        [Required(ErrorMessage = "Bu Alan Boş Geçilemez!")]
        public IFormFile image { get; set; }

        [Required(ErrorMessage = "Bu Alan Boş Geçilemez!")]
        public string description { get; set; }

        [Required(ErrorMessage = "Bu Alan Boş Geçilemez!")]
        public int capacity { get; set; }
        public string details1 { get; set; }
        public string details2 { get; set; }
    }
}
