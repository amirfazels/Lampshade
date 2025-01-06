namespace ShopManagement.Application.Contacts.ProductCategory
{
    public interface IProductCategoryApplication
    {
        void Create(CreateProductCategory command);
        void Edit(EditProductCategory command);
        Domain.ProductCategoryAgg.ProductCategory GetDetails(long id);
        ICollection<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
    }
}
