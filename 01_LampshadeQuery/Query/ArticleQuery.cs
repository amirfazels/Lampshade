using _01_LampshadeQuery.Contracts.Article;
using BlogManagement.Infrastructure.EFCore;

namespace _01_LampshadeQuery.Query
{
    public class ArticleQuery : IArticleQuery
    {
        private readonly BlogContext _blogContext;

        public ArticleQuery(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public ArticleQueryModel GetArticleDetails(string slug)
        {
            throw new NotImplementedException();
        }

        public List<ArticleQueryModel> GetLatestArticles()
        {
            throw new NotImplementedException();
        }

        public List<ArticleQueryModel> Search(string value)
        {
            throw new NotImplementedException();
        }
    }
}
