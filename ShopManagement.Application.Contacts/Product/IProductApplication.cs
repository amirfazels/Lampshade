using _0_Framework.Application;

namespace ShopManagement.Application.Contacts.Product
{
    public interface IProductApplication
    {
        OperationResult Create(CreateProduct command);
        OperationResult Edit(EditProduct command);
        EditProduct GetDetails(long id);
        ICollection<ProductViewModel> GetProducts();
        ICollection<ProductViewModel> Search(ProductSearchModel searchModel);
    }
}
