using System.Linq.Expressions;
using ShopManagement.Application.Contacts.ProductCategory;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository
    {
        void Create(ProductCategory entity);
        ProductCategory Get(long id);
        ICollection<ProductCategory> GetAll();
        bool Exists(Expression<Func<ProductCategory, bool>> expression);
        void SaveChanges();
        EditProductCategory GetDetails(long id);
        ICollection<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
    }
}
