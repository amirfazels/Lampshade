namespace _01_LampshadeQuery.Contracts.ProductCategory
{
    public interface IProductCategoryQuery
    {
        ICollection<ProductCategoryQueryModel> GetProductCategories();
        ICollection<ProductCategoryQueryModel> GetProductCategoriesWithProducts();
    }
}
