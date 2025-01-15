using _0_Framework.Application;

namespace ShopManagement.Application.Contacts.Product
{
    public interface IProductApplication
    {
        OperationResult Create(CreateProduct command);
        OperationResult Edit(EditProduct command);
        OperationResult IsStock(long id);
        OperationResult NotInStock(long id);
        EditProduct GetDetails(long id);
        ICollection<ProductViewModel> Search(ProductSearchModel searchModel);
    }
}
