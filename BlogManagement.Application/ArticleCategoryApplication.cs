using _0_Framework.Application;
using BlogManagement.Application.Contacts.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Application
{
    public class ArticleCategoryApplication : IArticleCategoryApplication
    {
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        private readonly IFileUploader _fileUploader;

        public ArticleCategoryApplication(IArticleCategoryRepository articleCategoryRepository, IFileUploader fileUploader)
        {
            _articleCategoryRepository = articleCategoryRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateArticleCategory command)
        {
            var OperationResult = new OperationResult();

            if(_articleCategoryRepository.Exists(x => x.Name == command.Name))
                return OperationResult.Failed(ApplicationMessages.DuplicatedRecord);

            string slug = command.Slug.Slugify();
            var PicturePath = _fileUploader.Upload(command.Picture, $"ArticleCategory/{slug}");
            var articleCategory = new ArticleCategory(
                command.Name,
                PicturePath, 
                command.PictureAlt,
                command.PictureTitle,
                command.Discription,
                command.ShowOrder,
                slug,
                command.Keyword,
                command.MetaDiscription,
                command.CanonicalAddress
                );

            _articleCategoryRepository.Create(articleCategory);
            _articleCategoryRepository.SaveChanges();

            return OperationResult.Succedded();
        }

        public OperationResult Edit(EditArticleCategory command)
        {
            var OperationResult = new OperationResult();
            var articleCategory = _articleCategoryRepository.Get(command.Id);

            if (articleCategory == null)
                return OperationResult.Failed(ApplicationMessages.RecordNotFound);

            if (_articleCategoryRepository.Exists(x => x.Name == command.Name && x.Id == command.Id))
                return OperationResult.Failed(ApplicationMessages.DuplicatedRecord);

            string slug = command.Slug.Slugify();
            var PicturePath = _fileUploader.Upload(command.Picture, $"ArticleCategory/{slug}");
            articleCategory.Edit(
                command.Name,
                PicturePath,
                command.PictureAlt,
                command.PictureTitle,
                command.Discription,
                command.ShowOrder,
                command.Slug,
                command.Keyword,
                command.MetaDiscription,
                command.CanonicalAddress
                );
            _articleCategoryRepository.SaveChanges();

            return OperationResult.Succedded();
        }

        public EditArticleCategory GetDetails(long id)
        {
            return _articleCategoryRepository.GetDetails(id);
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel command)
        {
            return _articleCategoryRepository.Search(command);
        }
    }
}
