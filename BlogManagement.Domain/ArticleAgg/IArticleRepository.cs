using _0_Framework.Domain;
using BlogManagement.Application.Contacts.Article;

namespace BlogManagement.Domain.ArticleAgg
{
    public interface IArticleRepository : IRepository<long, Article>
    {
        EditArticle? GetDetails(long id);
        List<ArticleViewModel> Search(ArticleSearchModel searchModel);
    }
}
