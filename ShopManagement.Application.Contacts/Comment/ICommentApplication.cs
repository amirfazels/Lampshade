using _0_Framework.Application;

namespace ShopManagement.Application.Contacts.Comment
{
    public interface ICommentApplication
    {
        OperationResult Create(AddComment command);
        OperationResult Cancel(long id);
        OperationResult Confirm(long id);
        List<CommentViewModel> Search(CommentSearchModel searchModel);
    }
}
