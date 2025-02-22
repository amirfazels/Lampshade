using Microsoft.AspNetCore.Http;

namespace BlogManagement.Application.Contacts
{
    public class CreateArticleCategory
    {
        public string Name { get; set; }
        public IFormFile Picture { get; set; }
        public string Discription { get; set; }
        public int ShowOrder { get; set; }
        public string Slug { get; set; }
        public string Keyword { get; set; }
        public string MetaDiscription { get; set; }
        public string CanonicalAddress { get; set; }
    }
}
