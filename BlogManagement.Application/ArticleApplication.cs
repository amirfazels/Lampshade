using _0_Framework.Application;
using BlogManagement.Application.Contacts.Article;
using BlogManagement.Domain.ArticleAgg;

namespace BlogManagement.Application
{
    public class ArticleApplication : IArticleApplication
    {
        private readonly IArticleRepository _articleRepository;

        public ArticleApplication(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public OperationResult Create(CreateArticle command)
        {
            throw new NotImplementedException();
        }

        public OperationResult Edit(EditArticle command)
        {
            throw new NotImplementedException();
        }

        public EditArticle GetDetails(long id)
        {
            throw new NotImplementedException();
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            return _articleRepository.Search(searchModel);
        }
    }
}
