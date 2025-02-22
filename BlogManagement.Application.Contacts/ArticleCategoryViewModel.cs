using Microsoft.AspNetCore.Http;

namespace BlogManagement.Application.Contacts
{
    public class ArticleCategoryViewModel
    {
        public string Name { get; set; }
        public IFormFile Picture { get; set; }
        public string Discription { get; set; }
        public int ShowOrder { get; set; }
    }
}
