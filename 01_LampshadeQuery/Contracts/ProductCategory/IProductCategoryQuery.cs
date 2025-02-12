namespace _01_LampshadeQuery.Contracts.ProductCategory
{
    public interface IProductCategoryQuery
    {
        ProductCategoryQueryModel GetProductCategoryWithProductsBy(string slug);
        ICollection<ProductCategoryQueryModel> GetProductCategories();
        ICollection<ProductCategoryQueryModel> GetProductCategoriesWithProducts();
    }
}
