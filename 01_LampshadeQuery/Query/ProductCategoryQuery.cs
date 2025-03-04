using _0_Framework.Application;
using _01_LampshadeQuery.Contracts.Product;
using _01_LampshadeQuery.Contracts.ProductCategory;
using DiscountManagement.Infrastructure.EfCore;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure.EFCore;

namespace _01_LampshadeQuery.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext _shopcontext;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;

        public ProductCategoryQuery(ShopContext shopcontext, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            _shopcontext = shopcontext;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }

        public ICollection<ProductCategoryQueryModel> GetProductCategories()
        {
            return _shopcontext.ProductCategories.Select(x => new ProductCategoryQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug,
                Keywords = x.Keywords
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

            var discount = _discountContext.CustomerDiscounts.ToList();


            var categories = _shopcontext.ProductCategories
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
                    Products = MapProducts(x.Products),
                }).ToList();

            categories.ForEach(category =>
            {
                foreach (var product in category.Products)
                {
                    var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                    var productDiscount = discount.FirstOrDefault(x => x.ProductId == product.Id && x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now);
                    if (productInventory != null)
                    {
                        product.Price = productInventory.UnitPrice.ToMoney();

                        if (productDiscount != null)
                        {
                            product.DiscountRate = productDiscount.DiscountRate;
                            product.PriceWithDiscount = (productInventory.UnitPrice - ((productInventory.UnitPrice *  product.DiscountRate) / 100)).ToMoney();
                        }

                    }
                    product.HasDiscount = product.PriceWithDiscount != null;
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

        public ProductCategoryQueryModel GetProductCategoryWithProductsBy(string slug)
        {
            var inventory = _inventoryContext.Inventory
                .Select(x => new
                {
                    x.ProductId,
                    x.UnitPrice
                }).ToList();

            var discount = _discountContext.CustomerDiscounts.ToList();


            var category = _shopcontext.ProductCategories
                .Include(x => x.Products)
                .ThenInclude(x => x.Category)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    MetaDescription = x.MetaDescription,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    Products = MapProducts(x.Products),
                    Keywords = x.Keywords
                }).FirstOrDefault(x => x.Slug == slug);

            foreach (var product in category.Products) 
            {
                var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                var productDiscount = discount.FirstOrDefault(x => x.ProductId == product.Id && x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now);
                if (productInventory != null)
                {
                    product.Price = productInventory.UnitPrice.ToMoney();

                    if (productDiscount != null)
                    {
                        product.DiscountExpireDate = productDiscount.EndDate.ToDiscountFormat();
                        product.DiscountRate = productDiscount.DiscountRate;
                        product.PriceWithDiscount = (productInventory.UnitPrice - ((productInventory.UnitPrice * product.DiscountRate) / 100)).ToMoney();
                    }

                }
                product.HasDiscount = product.PriceWithDiscount != null;
            }

            return category;
        }
    }
}
