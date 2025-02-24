using _0_Framework.Application;
using _0_Framework.Domain;
using _0_Framework.Infrastructure;
using BlogManagement.Application.Contacts.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;
using System.Linq.Expressions;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace BlogManagement.Infrastructure.EFCore.Repository
{
    public class ArticleCategoryRepository : RepositoryBase<long, ArticleCategory>, IArticleCategoryRepository
    {
        private readonly BlogContext _blogContext;

        public ArticleCategoryRepository(BlogContext blogContext) : base(blogContext)
        {
            _blogContext = blogContext;
        }

        public EditArticleCategory? GetDetails(long id)
        {
            return _blogContext.ArticleCategories.Select(x => new EditArticleCategory
            {
                Id = id,
                Name = x.Name,
                Description = x.Discription,
                ShowOrder = x.ShowOrder,
                Slug = x.Slug,
                Keywords = x.Keyword,
                MetaDescription = x.MetaDiscription,
                CanonicalAddress = x.CanonicalAddress,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,

            }).FirstOrDefault(x => x.Id == id);
        }

        public string? GetSlugById(long id)
        {
            return _blogContext.ArticleCategories.FirstOrDefault(x => x.Id == id).Slug;
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            var query = _blogContext.ArticleCategories.Select(x => new ArticleCategoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,
                Discription = x.Discription,
                ShowOrder = x.ShowOrder,
                CreationDate = x.CreationDate.ToFarsi(),
                ArticlesCount = 0,
                
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            return query.OrderByDescending(x => x.ShowOrder).ToList();
        }
    }
}
