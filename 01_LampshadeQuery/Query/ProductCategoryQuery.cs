using _0_Framework.Application;
using _01_LampshadeQuery.Contracts.Product;
using _01_LampshadeQuery.Contracts.ProductCategory;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure.EFCore;

namespace _01_LampshadeQuery.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext _context;
        private readonly InventoryContext _inventoryContext;

        public ProductCategoryQuery(ShopContext context, InventoryContext inventoryContext)
        {
            _context = context;
            _inventoryContext = inventoryContext;
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
            var inventory = _inventoryContext.Inventory
                .Select(x => new 
                { 
                    x.ProductId, 
                    x.UnitPrice 
                }).ToList();


            var categories =  _context.ProductCategories
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

            categories.ForEach(categories =>
            {
                foreach (var product in categories.Products)
                {
                    var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                    if (productInventory != null)
                        product.Price = productInventory.UnitPrice.ToMoney();                    
                }
            });

            return categories;
        }
        private static List<ProductQueryModel> MapProducts(ICollection<Product> products)
        {
            return products.Select(x => new ProductQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug,
                //Price = x.Price.ToString("N0"),
                //PriceWithDiscount = x.PriceWithDiscount.ToString("N0"),
                //DiscountRate = x.DiscountRate,
                Category = x.Category.Name
            }).ToList();
        }
    }
}
