using _0_Framework.Domain;
using ShopManagement.Application.Contacts.ProductPicture;

namespace ShopManagement.Domain.ProductPictureAgg
{
    public interface IProductPictureRepository : IRepository<long, ProductPicture>
    {
        EditProductPicture GetDetails(long id);
        ICollection<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel);
    }
}
