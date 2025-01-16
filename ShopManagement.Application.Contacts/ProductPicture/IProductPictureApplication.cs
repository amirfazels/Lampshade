using _0_Framework.Application;

namespace ShopManagement.Application.Contacts.ProductPicture
{
    public interface IProductPictureApplication
    {
        OperationResult Create(CreateProductPicture command);
        OperationResult Edit(EditProductPicture command);
        OperationResult Remove(long id);
        OperationResult Restore(long id);
        EditProductPicture GetDetails(long id);
        ICollection<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel);
    }
}
