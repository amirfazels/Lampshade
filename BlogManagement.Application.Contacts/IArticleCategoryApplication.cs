using _0_Framework.Application;

namespace BlogManagement.Application.Contacts
{
    public interface IArticleCategoryApplication
    {
        OperationResult Create(CreateArticleCategory command);
        OperationResult Edit(EditArticleCategory command);
        OperationResult Search(ArticleCategorySearchModel command);
    }
}
