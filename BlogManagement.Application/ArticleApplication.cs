using _0_Framework.Application;
using BlogManagement.Application.Contacts.Article;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Application
{
    public class ArticleApplication : IArticleApplication
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        private readonly IFileUploader _fileUploader;

        public ArticleApplication(IArticleRepository articleRepository, IArticleCategoryRepository articleCategoryRepository, IFileUploader fileUploader)
        {
            _articleRepository = articleRepository;
            _articleCategoryRepository = articleCategoryRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateArticle command)
        {
            var operation = new OperationResult();

            if (_articleRepository.Exists(x => x.Title == command.Title))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();

            string picturePath = _articleCategoryRepository.GetSlugById(command.CategoryId);
            picturePath = $"{picturePath}/{slug}";
            picturePath = _fileUploader.Upload(command.Pictrue, picturePath);

            var article = new Article
                (command.Title,
                command.ShortDescription,
                command.Description,
                picturePath,
                command.PictrueAlt,
                command.PictrueTitle,
                slug,
                command.Keywords,
                command.MetaDescription,
                command.CanonicalAddress,
                command.CategoryId,
                command.PublishDate);

            _articleRepository.Create(article);
            _articleRepository.SaveChanges();
            return operation.Succedded(); 
        }

        public OperationResult Edit(EditArticle command)
        {
            throw new NotImplementedException();
        }

        public EditArticle GetDetails(long id)
        {
            return _articleRepository.GetDetails(id);
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            return _articleRepository.Search(searchModel);
        }
    }
}
