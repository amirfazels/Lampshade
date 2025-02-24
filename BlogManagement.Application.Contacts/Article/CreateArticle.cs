using Microsoft.AspNetCore.Http;

namespace BlogManagement.Application.Contacts.Article
{
    public class CreateArticle
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public IFormFile Pictrue { get; set; }
        public string PictrueAlt { get; set; }
        public string PictrueTitle { get; set; }
        public string Slug { get; set; }
        public string Keywords { get; set; }
        public string MetaDescription { get; set; }
        public string CanonicalAddress { get; set; }
        public DateTime PublishDate { get; set; }
        public long CategoryId { get; set; }
    }
}
