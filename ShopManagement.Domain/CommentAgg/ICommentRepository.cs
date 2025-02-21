using ShopManagement.Application.Contacts.Comment;

namespace ShopManagement.Domain.CommentAgg
{
    public interface ICommentRepository
    {
        List<CommentViewModel> Search(CommentSearchModel searchModel);
    }
}
