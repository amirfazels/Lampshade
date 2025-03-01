using _01_LampshadeQuery.Contracts.ArticleCategory;
using _01_LampshadeQuery.Contracts.ProductCategory;

namespace _01_LampshadeQuery
{
    public class MenuModel
    {
        public ICollection<ArticleCategoryQueryModel> ArticleCategories { get; set; }
        public ICollection<ProductCategoryQueryModel> ProductCategories { get; set; }
    }
}
