using System.Linq.Expressions;
using _0_Framework.Domain;
using ShopManagement.Application.Contacts.Product;
using ShopManagement.Application.Contacts.ProductCategory;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository : IRepository<long, ProductCategory>
    {
        EditProductCategory GetDetails(long id);
        ICollection<ProductCategoryViewModel> GetProductCategories();
        string GetSlugById(long id);
        ICollection<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
    }
}
