using _0_Framework.Domain;
using ShopManagement.Application.Contacts.Product;

namespace ShopManagement.Domain.ProductAgg
{
    public interface IProductRepository : IRepository<long, Product>
    {
        EditProduct GetDetails(long id);
        Product? GetProductWithCategory(long id);
        ICollection<ProductViewModel> GetProducts();
        ICollection<ProductViewModel> Search(ProductSearchModel searchModel);
    }
}
