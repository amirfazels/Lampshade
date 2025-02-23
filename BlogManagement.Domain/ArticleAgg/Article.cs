using _0_Framework.Domain;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Domain.ArticleAgg
{
    public class Article : EntityBase
    {
        public string Title { get; private set; }
        public string ShortDescription { get; private set; }
        public string Description { get; private set; }
        public string Pictrue { get; private set; }
        public string PictrueAlt { get; private set; }
        public string PictrueTitle { get; private set; }
        public string Slug { get; private set; }
        public string Keywords { get; private set; }
        public string MetaDescription { get; private set; }
        public string CanonicalAddress { get; private set; }
        public DateTime PublishDate { get; private set; }
        public long CategoryId { get; private set; }
        public ArticleCategory Category { get; private set; }

        public Article(string title, string shortDescription, string description,
            string pictrue, string pictrueAlt, string pictrueTitle, string slug,
            string keywords, string metaDescription, string canonicalAddress, 
            long categoryId, DateTime publishDate)
        {
            Title = title;
            ShortDescription = shortDescription;
            Description = description;
            Pictrue = pictrue;
            PictrueAlt = pictrueAlt;
            PictrueTitle = pictrueTitle;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
            CanonicalAddress = canonicalAddress;
            CategoryId = categoryId;
            PublishDate = publishDate;
        }

        public void Edit(string title, string shortDescription, string description,
            string pictrue, string pictrueAlt, string pictrueTitle, string slug,
            string keywords, string metaDescription, string canonicalAddress,
            long categoryId, DateTime publishDate)
        {
            Title = title;
            ShortDescription = shortDescription;
            Description = description;
            if (!string.IsNullOrWhiteSpace(pictrue))
                Pictrue = pictrue;
            PictrueAlt = pictrueAlt;
            PictrueTitle = pictrueTitle;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
            CanonicalAddress = canonicalAddress;
            CategoryId = categoryId;
            PublishDate = publishDate;
        }
    }
}
