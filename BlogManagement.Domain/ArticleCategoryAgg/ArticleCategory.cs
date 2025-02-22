using _0_Framework.Domain;

namespace BlogManagement.Domain.ArticleCategoryAgg
{
    public class ArticleCategory : EntityBase
    {
        public string Name { get; private set; }
        public string Picture { get; private set; }
        public string Discription { get; private set; }
        public int ShowOrder { get; private set; }
        public string Slug { get; private set; }
        public string Keyword { get; private set; }
        public string MetaDiscription { get; private set; }
        public string CanonicalAddress { get; private set; }
    }
}
