namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository
    {
        void Create(ProductCategory entity);
        ProductCategory Get(long id);
        ICollection<ProductCategory> GetAll();

    }
}
