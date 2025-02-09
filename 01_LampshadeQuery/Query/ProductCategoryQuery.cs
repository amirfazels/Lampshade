using _01_LampshadeQuery.Contracts.Product;
using _01_LampshadeQuery.Contracts.ProductCategory;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure.EFCore;

namespace _01_LampshadeQuery.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext _context;

        public ProductCategoryQuery(ShopContext context)
        {
            _context = context;
        }

        public ICollection<ProductCategoryQueryModel> GetProductCategories()
        {
            return _context.ProductCategories.Select(x => new ProductCategoryQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug
            }).ToList();
        }

        public ICollection<ProductCategoryQueryModel> GetProductCategoriesWithProducts()
        {
            return _context.ProductCategories
                .Include(x => x.Products)
                .ThenInclude(x => x.Category)
                .Select(x => new ProductCategoryQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug,
                Products = MapProducts(x.Products)
                }).ToList();
        }
        private static List<ProductQueryModel> MapProducts(ICollection<Product> products)
        {
            //var result = new HashSet<ProductQueryModel>();
            var result = new List<ProductQueryModel>();
            foreach (var product in products) 
            {
                var item = new ProductQueryModel
                {
                    Id = product.Id,
                    Picture = product.Picture,
                    PictureAlt = product.PictureAlt,
                    PictureTitle = product.PictureTitle,
                    Name = product.Name,
                    //Price = product.Price,
                    //PriceWithDiscount = product.PriceWithDiscount,
                    //DiscountRate = product.DiscountRate,
                    Category = product.Category.Name,
                    Slug = product.Slug,
                };
                result.Add(item);
            }
            return result;
        }
    }
}
