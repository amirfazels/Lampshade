using _0_Framework.Application;
using ShopManagement.Application.Contacts.Product;

namespace ShopManagement.Application.Contacts.ProductCategory
{
    public interface IProductCategoryApplication
    {
        OperationResult Create(CreateProductCategory command);
        OperationResult Edit(EditProductCategory command);
        EditProductCategory GetDetails(long id);
        ICollection<ProductCategoryViewModel> GetProductCategories();
        ICollection<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
    }
}
