namespace _01_LampshadeQuery.Contracts.Article
{
    public interface IArticleQuery
    {
        ArticleQueryModel? GetArticleDetails(string slug);
        List<ArticleQueryModel> GetLatestArticles();
        List<ArticleQueryModel> Search(string value);
    }
}
