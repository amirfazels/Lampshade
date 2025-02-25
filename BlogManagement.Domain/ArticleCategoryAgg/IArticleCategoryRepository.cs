using _0_Framework.Domain;
using BlogManagement.Application.Contacts.ArticleCategory;

namespace BlogManagement.Domain.ArticleCategoryAgg
{
    public interface IArticleCategoryRepository : IRepository<long, ArticleCategory>
    {
        List<ArticleCategoryViewModel> GetArticleCategories();
        EditArticleCategory? GetDetails(long id);
        string? GetSlugById(long id);
        List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel);
    }
}
