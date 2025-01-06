using ShopManagement.Application.Contacts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        public void Create(CreateProductCategory command)
        {
            throw new NotImplementedException();
        }

        public void Edit(EditProductCategory command)
        {
            throw new NotImplementedException();
        }

        public ProductCategory GetDetails(long id)
        {
            throw new NotImplementedException();
        }

        public ICollection<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            throw new NotImplementedException();
        }
    }
}
