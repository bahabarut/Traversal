using Microsoft.AspNetCore.Http;

namespace TraversalCore.Areas.Admin.Models
{
    public class UpdateGuideViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public IFormFile image { get; set; }
        public string twitter { get; set; }
        public string instagram { get; set; }
    }
}
