using _01_LampshadeQuery.Contracts.Article;

namespace _01_LampshadeQuery.Query
{
    public class ArticleQuery : IArticleQuery
    {
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
