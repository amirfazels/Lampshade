using _0_Framework.Domain;

namespace BlogManagement.Domain.ArticleCategoryAgg
{
    public class ArticleCategory : EntityBase
    {
        public string Name { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string Discription { get; private set; }
        public int ShowOrder { get; private set; }
        public string Slug { get; private set; }
        public string Keyword { get; private set; }
        public string MetaDiscription { get; private set; }
        public string CanonicalAddress { get; private set; }

        public ArticleCategory(string name, string picture, string pictureAlt,
            string pictureTitle, string discription,
            int showOrder, string slug, string keyword,
            string metaDiscription, string canonicalAddress)
        {
            Name = name;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Discription = discription;
            ShowOrder = showOrder;
            Slug = slug;
            Keyword = keyword;
            MetaDiscription = metaDiscription;
            CanonicalAddress = canonicalAddress;
        }
        public void Edit(string name, string picture, string pictureAlt,
            string pictureTitle, string discription,
            int showOrder, string slug, string keyword,
            string metaDiscription, string canonicalAddress)
        {
            Name = name;
            if (string.IsNullOrWhiteSpace(picture)) 
                Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Discription = discription;
            ShowOrder = showOrder;
            Slug = slug;
            Keyword = keyword;
            MetaDiscription = metaDiscription;
            CanonicalAddress = canonicalAddress;
        }
    }
}
