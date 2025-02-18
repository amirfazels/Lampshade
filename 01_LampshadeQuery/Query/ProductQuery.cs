using _0_Framework.Application;
using _01_LampshadeQuery.Contracts.Product;
using DiscountManagement.Infrastructure.EfCore;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EFCore;

namespace _01_LampshadeQuery.Query
{
    public class ProductQuery : IProductQuery
    {
        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;

        public ProductQuery(ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }

        public ProductQueryModel? GetDetails(string slug)
        {
            var inventory = _inventoryContext.Inventory
                .Select(x => new
                {
                    x.ProductId,
                    x.UnitPrice
                }).ToList();

            var discount = _discountContext.CustomerDiscounts.ToList();

            var product = _shopContext.Products
                .AsNoTracking()
                .Include(x => x.Category)
                .Select(x => new ProductQueryModel
                {
                    Id = x.Id,
                    Category = x.Category.Name,
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                }).FirstOrDefault(x => x.Slug.Equals(slug));

            if (product == null)
                return new ProductQueryModel();

            var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
            var productDiscount = discount.FirstOrDefault(x => x.ProductId == product.Id && x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now);
            if (productInventory != null)
            {
                product.Price = productInventory.UnitPrice.ToMoney();

                if (productDiscount != null)
                {
                    product.DiscountRate = productDiscount.DiscountRate;
                    product.PriceWithDiscount = (productInventory.UnitPrice - ((productInventory.UnitPrice * product.DiscountRate) / 100)).ToMoney();
                }

            }
            product.HasDiscount = product.PriceWithDiscount != null;
            return product;
        }

        public List<ProductQueryModel> GetLatestArrivals()
        {
            var inventory = _inventoryContext.Inventory
                .Select(x => new
                {
                    x.ProductId,
                    x.UnitPrice
                }).ToList();

            var discount = _discountContext.CustomerDiscounts.ToList();

            var products = _shopContext.Products
                .Include(x => x.Category)
                .Select(x => new ProductQueryModel
                {
                    Id = x.Id,
                    Category = x.Category.Name,
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                }).OrderByDescending(x => x.Id).Take(6).ToList();

            products.ForEach(product =>
            {
                var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                var productDiscount = discount.FirstOrDefault(x => x.ProductId == product.Id && x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now);
                if (productInventory != null)
                {
                    product.Price = productInventory.UnitPrice.ToMoney();

                    if (productDiscount != null)
                    {
                        product.DiscountRate = productDiscount.DiscountRate;
                        product.PriceWithDiscount = (productInventory.UnitPrice - ((productInventory.UnitPrice * product.DiscountRate) / 100)).ToMoney();
                    }

                }
                product.HasDiscount = product.PriceWithDiscount != null;
            });
            return products;
        }

        public List<ProductQueryModel> Search(string value)
        {
            var inventory = _inventoryContext.Inventory
                .Select(x => new
                {
                    x.ProductId,
                    x.UnitPrice
                }).ToList();

            var discount = _discountContext.CustomerDiscounts.ToList(); 
            var query = _shopContext.Products
                .Include(x => x.Category)
                .Select(x => new ProductQueryModel
                {
                    Id = x.Id,
                    Category = x.Category.Name,
                    CategorySlug = x.Category.Slug,
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    ShortDescription = x.ShortDescription
                }).AsNoTracking();

            if (!string.IsNullOrWhiteSpace(value))
                query = query.Where(x => x.Name.Contains(value) || x.ShortDescription.Contains(value));

            var products = query.OrderByDescending(x => x.Id).ToList();

            products.ForEach(product =>
            {
                var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                var productDiscount = discount.FirstOrDefault(x => x.ProductId == product.Id && x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now);
                if (productInventory != null)
                {
                    product.Price = productInventory.UnitPrice.ToMoney();

                    if (productDiscount != null)
                    {
                        product.DiscountRate = productDiscount.DiscountRate;
                        product.PriceWithDiscount = (productInventory.UnitPrice - ((productInventory.UnitPrice * product.DiscountRate) / 100)).ToMoney();
                    }

                }
                product.HasDiscount = product.PriceWithDiscount != null;
            });
            return products;
        }
    }
}
