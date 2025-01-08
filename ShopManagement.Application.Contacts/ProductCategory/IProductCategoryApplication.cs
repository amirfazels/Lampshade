using _0_Framework.Application;

namespace ShopManagement.Application.Contacts.ProductCategory
{
    public interface IProductCategoryApplication
    {
        OperationResult Create(CreateProductCategory command);
        OperationResult Edit(EditProductCategory command);
        EditProductCategory GetDetails(long id);
        ICollection<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
    }
}
