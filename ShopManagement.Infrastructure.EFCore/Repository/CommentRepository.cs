using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contacts.Comment;
using ShopManagement.Domain.CommentAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class CommentRepository : RepositoryBase<long, Comment>, ICommentRepository
    {
        private readonly ShopContext _shopContext;
        public CommentRepository(ShopContext shopContext) : base(shopContext)
        {
            _shopContext = shopContext;
        }
        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            var query = _shopContext.Comments.Select(x => new CommentViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Message = x.Message,
                ProductId = x.ProductId,
                ProductName = x.Product.Name,
                IsConfirmed = x.IsConfirmed,
                IsCanceled = x.IsCanceled,
                CommentDate = x.CreationDate.ToFarsi()
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            if (!string.IsNullOrWhiteSpace(searchModel.Email))
                query = query.Where(x => x.Email.Contains(searchModel.Email));

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
